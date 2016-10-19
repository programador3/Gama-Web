using negocio.Componentes;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Services;

namespace presentacion
{
    /// <summary>
    /// Descripción breve de WebService
    /// </summary>
    [WebService(Namespace = "http://gama.website/puestos/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        ///////////////////////////////////////////////////
        //utilizado para ws
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

        private string path = "";

        private List<string> ccerror = new List<string>();

        public DataTable tabla_extensiones = new DataTable();

    

        [WebMethod]
        public string EjecutarCheckMail()
        {
            CargaDatos();
            return CheckMail();
        }

        public void CargaDatos()
        {
            try
            {
                UsuariosBL componente = new UsuariosBL();
                DataSet ds = componente.CargaInformacionInicial();
                DataRow row = ds.Tables[0].Rows[0];
                username = row["USERNAME"].ToString();
                string[] CCId = row["ERRORCC"].ToString().Split(';');
                foreach (string CCEmail in CCId)
                {
                    ccerror.Add(CCEmail);
                }
                correo_error = row["USERNAME"].ToString();
                password = row["PASS"].ToString();
                hostname = row["HOST"].ToString();
                hostnamesmtp = row["HOSTSMTP"].ToString();
                path = row["RUTA"].ToString();
                port = Convert.ToInt32(row["PORT"]);
                portsmtp = Convert.ToInt32(row["PORTSMTP"]);
                useSsl = Convert.ToBoolean(row["USESSL"]);
                tabla_extensiones = ds.Tables[1];
            }
            catch (Exception ex)
            {
                SendMail("Error en CheckMail el dia " + DateTime.Now.ToString(), "LA APLICACIÓN CHECKMAIL REALIZO UNA EJECUCION ARROJANDO LA SIGUIENTE EXCEPCIÓN;" + System.Environment.NewLine + ex.ToString(), correo_error, new List<string>());
            }
        }

        public int Exist(string filtro)
        {
            int total = 0;
            try
            {
                int i = tabla_extensiones.Rows.Count;
                DataView view = tabla_extensiones.DefaultView;
                view.RowFilter = "V1 LIKE '%" + filtro + "%'";
                total = view.ToTable().Rows.Count;
                return total;
            }
            catch (Exception ex)
            {
                SendMail("Error en CheckMail el dia " + DateTime.Now.ToString(), "LA APLICACIÓN CHECKMAIL REALIZO UNA EJECUCION ARROJANDO LA SIGUIENTE EXCEPCIÓN;" + System.Environment.NewLine + ex.ToString(), correo_error, new List<string>());
                return 0;
            }
        }

        /// <summary>
        /// Envia un correo
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="to"></param>
        public void SendMail(string subject, string body, string to, List<string> listadeadjuntos)
        {
            try
            {
                MailMessage message = new MailMessage(username, to);
                message.Subject = subject;
                message.Body = body;
                SmtpClient mailer = new SmtpClient(hostnamesmtp, portsmtp);
                mailer.Credentials = new NetworkCredential(username, password);
                mailer.EnableSsl = useSsl;
                if (ccerror.Count > 0)
                {
                    foreach (string cc in ccerror)
                    {
                        if (cc != "")
                        {
                            message.CC.Add(cc);
                        }
                    }
                }
                message.CC.Add("programador3@gamamateriales.com.mx");
                foreach (string adjunto in listadeadjuntos)
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(adjunto);
                    attachment.Name = Path.GetFileName(adjunto);
                    message.Attachments.Add(attachment);
                }
                mailer.Send(message);
            }
            catch (Exception ex)
            {
                SendMail("Error en CheckMail el dia " + DateTime.Now.ToString(), "LA APLICACIÓN CHECKMAIL REALIZO UNA EJECUCION ARROJANDO LA SIGUIENTE EXCEPCIÓN;" + System.Environment.NewLine + ex.ToString(), correo_error, new List<string>());

            }
        }

        /// <summary>
        /// Descarga listado de correos y los reenvia
        /// </summary>
        public string CheckMail()
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
                            for (int i = messageCount; i > 0; i--)
                            {
                                OpenPop.Mime.Message msg = client.GetMessage(i, null);
                                string value = msg.Headers.Subject;
                                var att = msg.FindAllAttachments();
                                foreach (var ado in att)
                                {
                                    string resultado = ado.FileName.ToString().Replace("/", " ");
                                    resultado = resultado.Replace(@"\", " ");
                                    resultado = resultado.Replace(":", " ");
                                    resultado = resultado.Replace("*", " ");
                                    resultado = resultado.Replace("?", " ");
                                    resultado = resultado.Replace(@"|", " ");
                                    resultado = resultado.Replace("<", " ");
                                    resultado = resultado.Replace(">", " ");
                                    resultado = resultado.Trim();
                                    string extension = Path.GetExtension(resultado);
                                    if (Exist(extension) > 0)
                                    {
                                        ado.Save(new System.IO.FileInfo(System.IO.Path.Combine(path, resultado)));
                                    }
                                }
                                client.DeleteMessage(i);
                            }
                        }
                        return "";
                    }
                    else
                    {
                        return "El Cliente no Pudo conectarse a CORREO:" + username;
                    }
                }
            }
            catch (Exception ex)
            {
                SendMail("Error en CheckMail el dia " + DateTime.Now.ToString(), "LA APLICACIÓN CHECKMAIL REALIZO UNA EJECUCION ARROJANDO LA SIGUIENTE EXCEPCIÓN;" + System.Environment.NewLine + ex.ToString(), correo_error, new List<string>());
                return ex.ToString();
            }
        }

















        /// <summary>
        /// utlizado para ajax
        /// </summary>
        /// <param name="aData"></param>

        [System.Web.Services.WebMethod(EnableSession = true)]
        public void SetSessionValue(List<string> aData)
        {
            String Value = aData[0];
            Session["date_noti"] = Value;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetSessionValue()
        {
            string sessionVal = String.Empty;
            if (Session["date_noti"] != null)
            {
                sessionVal = Session["date_noti"].ToString();
            }
            return sessionVal;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public void SetSessionValueHTML(List<string> aData)
        {
            String Value = aData[0];
            Session["list_not"] = "";
            Session["list_not"] = Value;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetSessionValueHTML()
        {
            string sessionVal = String.Empty;
            if (Session["list_not"] != null)
            {
                sessionVal = Session["list_not"].ToString();
            }
            return sessionVal;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public void SetSessionValueTotal(List<string> aData)
        {
            String Value = aData[0];
            Session["tot_not"] = Value;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetSessionValueTotal()
        {
            string sessionVal = String.Empty;
            if (Session["tot_not"] != null)
            {
                sessionVal = Session["tot_not"].ToString();
            }
            return sessionVal;
        }
    }
}