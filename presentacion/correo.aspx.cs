using negocio.Componentes;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class correo : System.Web.UI.Page
    {
        private string username = "";

        //password
        private string password = "";

        //el puerto para pop de gmail es el 995
        private int port;

        private int portsmtp;

        //el host de pop de gmail es pop.gmail.com
        private string hostname = "";

        private string hostnamesmtp = "";

        //esta opción debe ir en true
        private bool useSsl = false;

        //correo quien recibira copias
        private string correo_copia = "";

        //correo quien recibira copias si hay un error
        private string correo_error = "programador3@gamamateriales.com.mx";

        private List<string> ccerror = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("date");
                dt.Columns.Add("mail");
                dt.Columns.Add("title");
                dt.Columns.Add("body");
                dt.Columns.Add("id");
                Session["dt_mail"] = dt;
                DataTable dtf = new DataTable();
                dtf.Columns.Add("id");
                dtf.Columns.Add("path");
                dtf.Columns.Add("name");
                Session["dt_mail_file"] = dtf;
                CargarInformacion();
                CheckMail();
                CargarRepeat();
            }
        }

        private void CargarRepeat()
        {
            DataTable dt = Session["dt_mail"] as DataTable;
            DataTable dt2 = dt.Copy();
            Repeater1.DataSource = dt2;
            Repeater1.DataBind();
        }

        private void CargarInformacion()
        {
            try
            {
                UsuariosBL componente = new UsuariosBL();
                DataSet ds = componente.Informacion_Correo(Convert.ToInt32(Session["sidc_usuario"]));
                DataRow row = ds.Tables[0].Rows[0];
                username = row["USERNAME"].ToString();
                correo_error = row["USERNAME"].ToString();
                password = row["PASS"].ToString();
                hostname = row["HOST"].ToString();
                hostnamesmtp = row["HOSTSMTP"].ToString();
                port = Convert.ToInt32(row["PORT"]);
                portsmtp = Convert.ToInt32(row["PORTSMTP"]);
                useSsl = Convert.ToBoolean(row["USESSL"]);
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        private bool Exists(DataTable dt, string query)
        {
            bool re = false;
            DataView view = dt.DefaultView;
            view.RowFilter = query;
            if (view.ToTable().Rows.Count > 0)
            {
                return true;
            }
            else
            { return false; }
        }

        /// <summary>
        /// Descarga listado de correos y los reenvia
        /// </summary>
        private void CheckMail()
        {
            try
            {
                using (Pop3Client client = new Pop3Client())
                {
                    // conectamos al servidor
                    client.Connect(hostname, port, useSsl);

                    // Autentificación
                    client.Authenticate(username, password);
                    // Obtenemos los Uids mensajes
                    if (client.Connected)
                    {
                        int messageCount = client.GetMessageCount();
                        if (messageCount > 0)
                        {
                            DataTable dt = Session["dt_mail"] as DataTable;
                            DataTable dtf = Session["dt_mail_file"] as DataTable;
                            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/files/"));//path local
                            for (int i = messageCount; i > 0; i--)
                            {
                                DataRow row = dt.NewRow();
                                OpenPop.Mime.Message msg = client.GetMessage(i);
                                string value = msg.Headers.Subject;
                                var att = msg.FindAllAttachments();
                                Random random = new Random();
                                int randomNumber = random.Next(0, 1000000);
                                string date = msg.Headers.DateSent.ToString("dd MMMM, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX"));
                                string mail = msg.Headers.From.ToString();
                                string title = msg.Headers.Subject;
                                string body = "";
                                string id = msg.Headers.MessageId == null ? randomNumber.ToString() : msg.Headers.MessageId.ToString();
                                OpenPop.Mime.MessagePart plainText = msg.FindFirstPlainTextVersion();
                                if (plainText != null)
                                {
                                    body = plainText.GetBodyAsText();
                                }
                                else
                                {
                                    // Might include a part holding html instead
                                    OpenPop.Mime.MessagePart html = msg.FindFirstHtmlVersion();
                                    if (html != null)
                                    {
                                        body = html.GetBodyAsText();
                                    }
                                }
                                row["date"] = date;
                                row["mail"] = mail;
                                row["title"] = title;
                                row["body"] = body;
                                row["id"] = id;
                                foreach (var ado in att)
                                {
                                    DataRow rowf = dtf.NewRow();
                                    rowf["id"] = id;
                                    rowf["name"] = ado.FileName;
                                    rowf["path"] = dirInfo.ToString() + ado.FileName;
                                    string extension = System.IO.Path.GetExtension(ado.FileName.ToString().ToUpper());
                                    ado.Save(new System.IO.FileInfo(System.IO.Path.Combine(dirInfo.ToString(), ado.FileName)));
                                    if (Exists(dtf, "path = '" + dirInfo.ToString() + ado.FileName + "'") == false)
                                    {
                                        dtf.Rows.Add(rowf);
                                    }
                                }
                                //client.DeleteMessage(i);
                                if (Exists(dt, "date = '" + date + "' and mail = '" + mail + "'") == false)
                                {
                                    dt.Rows.Add(row);
                                }
                            }
                            Session["dt_mail"] = dt;
                            Session["dt_mail_file"] = dtf;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void lnk_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            string date = lnk.CommandName;
            string mail = lnk.CommandArgument.ToString();
            DataTable dt = Session["dt_mail"] as DataTable;
            DataTable dtf = Session["dt_mail_file"] as DataTable;
            DataView vdt = dt.DefaultView;
            vdt.RowFilter = "date = '" + date + "' and mail = '" + mail + "'";
            DataRow row = vdt.ToTable().Rows[0];
            string title = row["title"].ToString();
            string body = row["body"].ToString();
            string id = row["id"].ToString();
            lbldate.Text = date;
            lblbody.Text = body;
            DataView vdtf = dtf.DefaultView;
            vdtf.RowFilter = "id like '%" + id + "%'";
            foreach (DataRow rowaa in dtf.Rows)
            {
                string idd = rowaa["id"].ToString();
                idd = idd;
            }
            int t2 = dtf.Rows.Count;
            int t = vdtf.ToTable().Rows.Count;
            repeat_file.DataSource = vdtf.ToTable();
            repeat_file.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('" + title.Replace("'", "") + "','" + mail + "','modal fade modal-info');", true);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            CargarRepeat();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            string path = lnk.CommandName;

            Download(path);
        }

        public void Download(string path)
        {
            if (!File.Exists(path))
            {
                Alert.ShowAlertError("No tiene archivo relacionado", this);
            }
            else
            {
                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(path));
                // Escribimos el fichero a enviar
                Response.WriteFile(path);
                // volcamos el stream
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
                // Response.End();
            }
        }
    }
}