using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class reportes_empleados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void lnkhorarios_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            string tipo = lnk.CommandName;
            string idc = funciones.de64aTexto(Request.QueryString["idc_empleado"]);
            String url = HttpContext.Current.Request.Url.AbsoluteUri;
            String path_actual = url.Substring(url.LastIndexOf("/") + 1);
            url = url.Replace(path_actual, "");
            url = url + "tareas_informacion_adicional.aspx?idc_tipoi=" + funciones.deTextoa64(tipo) + "&idc_proceso=" + funciones.deTextoa64(idc);
            ScriptManager.RegisterStartupScript(this, GetType(), "noti533W3", "window.open('" + url + "');", true);
        }
    }
}