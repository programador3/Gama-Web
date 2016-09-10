using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using negocio.Componentes;
namespace presentacion
{
    public partial class reporte_filtro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["sidc_usuario"] == null)
                {
                    Response.Redirect("login.aspx");
                    return;
                }
                //valida si tiene permiso de ver esta pagina//
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());

                //fecha al dia de hoy 
                DateTime hoy = DateTime.Today;
                txtfecha1.Text = hoy.ToString("yyyy-MM-dd");
                txtfecha2.Text = hoy.ToString("yyyy-MM-dd");
                

            }
        }

        protected void btnfiltrar_Click(object sender, EventArgs e)
        {
            if (funciones.EsFecha(txtfecha1.Text) == false || funciones.EsFecha(txtfecha2.Text) == false)
            {
                msgbox.show("Solo se aceptan fechas validas.", this.Page);
                return;
            }
            try{
                string path, pagina;
                List<ReportParameter> parametros = new List<ReportParameter>();
                string host = HttpContext.Current.Request.Url.Host;
                //debo mandar fechas
                parametros.Add(new ReportParameter("pfechai", txtfecha1.Text));
                parametros.Add(new ReportParameter("pfechaf", txtfecha2.Text));
                //parametros en general.
                string idc_usuario = HttpContext.Current.Session["sidc_usuario"].ToString();
                parametros.Add(new ReportParameter("pidc_usuario", idc_usuario));
                parametros.Add(new ReportParameter("phost", host));
                //mas parametros
                string cadena = funciones.obten_cadena_con("conexion");
                parametros.Add(new ReportParameter("cadconexion", cadena));
                 //le mandamos a la clase reporting que informe queremos
                 //en este caso 270 y nos devuelve la pagina a donde debe ir --informe.aspx--
                 path = reporting.get_reporte(270);
                //redireccionamos
                //sesion
                 string indice = funciones.id_aleatorio();
                 Session[indice] = parametros;
                 Session["reportpath"] = path;

                 pagina = "informe.aspx?indice="+indice;
                 ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + pagina + "', '_blank');", true);
            }
            catch (Exception ex)
                {
                    msgbox.show(ex.Message, this.Page);
                }
        }

        protected void btnregresar_Click(object sender, ImageClickEventArgs e)
        {
        }

        

        

        
    }
}