using System;
using System.IO;
using System.Web.UI;

namespace presentacion
{
    public partial class view_files : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)
            {
                Response.Redirect("menu.aspx");
            }

            if (!string.IsNullOrEmpty(Request.QueryString["url"]))
            {
                string url = Request.QueryString["url"];
                url = funciones.ConvertHexToString(url);
                string new_url = BajarArchivo(url);
                PanelVisor.Visible = true;
                ifrma.Attributes["src"] = "https://docs.google.com/viewer?url=" + new_url;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["file"]))
            {
                string path = Request.QueryString["file"];
                path = funciones.ConvertHexToString(path);
                PanelHTML.Visible = true;
                LeerArchivo(path);
            }
        }

        private string BajarArchivo(string path)
        {
            DateTime localDate = DateTime.Now;
            string date = localDate.ToString();
            string user = Convert.ToInt32(Session["sidc_usuario"]).ToString();
            date = date.Replace("/", "_");
            date = date.Replace(":", "_");
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/files/"));//path local
            File.Copy(path, dirInfo + user + date + Path.GetExtension(path), true);
            return "www.gamamateriales.com/puestos/temp/files/" + user + date + Path.GetExtension(path);
        }

        private void LeerArchivo(string path)
        {
            StreamReader reader = new StreamReader(path);
            string content = "";
            @content = @reader.ReadToEnd();
            reader.Close();
            Literal1.Text = @content;
        }

        protected void lnkCerrar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "noti53SSSSS3W3", "window.close();", true);
        }
    }
}