using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Globalization;

namespace presentacion
{
    public partial class pre_empleados_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idc_pre_empleado"] == null || Request.QueryString["idc_prepara"] ==null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "window.close();", true);
                return;
            }
            try
            {
                int idc_prepara = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_prepara"]));
                int idc_pre_empleado = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_pre_empleado"]));
                DataPrep2(0, idc_prepara, idc_pre_empleado);
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/pre_alta/"));//path local
                string url_original = funciones.GenerarRuta("FOT_CAN", "UNIDAD");
                url_original = url_original + idc_pre_empleado.ToString() + ".jpg";
                string url = dirInfo + randomNumber.ToString() + "_" + idc_pre_empleado.ToString() + ".jpg";
                if (File.Exists(url_original))
                {
                    File.Copy(url_original, url, true);
                    imgempleado.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["server"] + "\\temp\\pre_alta\\" + randomNumber.ToString() + "_" + idc_pre_empleado.ToString() + ".jpg";
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(),this);
            }
        }

        public void DataPrep2(int idc_puestosbaja, int idc_prepara, int idc_pre_empleado)
        {
            CandidatosENT entidad = new CandidatosENT();
            CandidatosCOM componente = new CandidatosCOM();
            if (idc_pre_empleado != 0)
            {
                entidad.Pidc_puesto = idc_puestosbaja;
                entidad.Pidc_prepara = idc_prepara;
                entidad.Pidc_pre_empleado = idc_pre_empleado;
                DataSet ds = componente.CargaCandidatos(entidad);
                txtobservaciones2.Text = ds.Tables[1].Rows[0]["observaciones"].ToString();
                gridDetalles.DataSource = ds.Tables[1];
                gridDetalles.DataBind();
                repeat_telefonos.DataSource = ds.Tables[2];
                repeat_telefonos.DataBind();
                repeat_papeleria.DataSource = ds.Tables[3];
                repeat_papeleria.DataBind();

                DataTable audio = ds.Tables[4];
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/files/"));//path local
                string pageName = HttpContext.Current.Request.ApplicationPath + "/";
                //foreach (DataRow row in audio.Rows)
                //{
                //    string path = row["audiopath"].ToString();
                //    string file = row["audio"].ToString();
                //    File.Copy(path, dirInfo + file, true);
                //    row["audio"] = System.Configuration.ConfigurationManager.AppSettings["server"] + "/temp/files/"  + file;
                //}
                repeater_referencias.DataSource = audio;
                repeater_referencias.DataBind();
                gridreferencias.DataSource = audio;
                gridreferencias.DataBind();


            }
        }


        protected void lnkdownload_Click(object sender, EventArgs e)
        {
            LinkButton lnkdownload = (LinkButton)sender;
            string ruta = lnkdownload.CommandName.ToString();
            string archivo = lnkdownload.CommandArgument.ToString();
            string extension = Path.GetExtension(archivo).Trim().ToUpper();
            if (extension == ".PDF" || extension == ".MP3" || extension == ".M4A" || extension == ".WMA" || extension == ".MP4" || extension == ".JPG" || extension == ".JPEG")
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/files/"));//path local
                string Domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host;
                string pageName = HttpContext.Current.Request.ApplicationPath + "/";
                File.Copy(ruta, dirInfo + Path.GetFileName(ruta), true);
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "window.open('" + pageName + "temp/files/" + Path.GetFileName(ruta) + "');", true);

            }
            else {

                Download(ruta, archivo);
            }
        }

        protected void repeat_papeleria_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            HtmlGenericControl div_details = e.Item.FindControl("div_details") as HtmlGenericControl;
            LinkButton lnkdownload = (LinkButton)e.Item.FindControl("lnkdownload");
            string ruta = Convert.ToString(DataBinder.Eval(dbr, "ruta"));
            string archivo = Convert.ToString(DataBinder.Eval(dbr, "archivo"));
            lnkdownload.CommandName = ruta;
            lnkdownload.CommandArgument = archivo;
            div_details.Visible = File.Exists(ruta);
        }

        /// <summary>
        /// Descarga un archivo
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file_name"></param>
        public void Download(string path, string file_name)
        {
            // Limpiamos la salida
            Response.Clear();
            // Con esto le decimos al browser que la salida sera descargable
            Response.ContentType = "application/octet-stream";
            // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file_name);
            // Escribimos el fichero a enviar
            Response.WriteFile(path);
            // volcamos el stream
            Response.Flush();
            // Enviamos todo el encabezado ahora
            Response.End();
        }

    }
}