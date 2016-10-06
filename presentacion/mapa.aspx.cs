using System;
using System.Web.UI;

namespace presentacion
{
    public partial class mapa : System.Web.UI.Page
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
                //fin
                lbldetalles.Visible = Request.QueryString["detalle"] == null ? false : true;
                close.Visible= Request.QueryString["detalle"] == null ? false : true;
                lbldetalles.Text = funciones.de64aTexto(Request.QueryString["detalle"]);
                string lat, lon;
                lat = funciones.de64aTexto(Request.QueryString["latitud"]);
                lon = funciones.de64aTexto(Request.QueryString["longitud"]);
                //ejectuar funcion
                ClientScript.RegisterStartupScript(GetType(), "verMapa", "ver('" + lat + "','" + lon + "');", true);
            }
        }

        protected void close_Click(object sender, EventArgs e)
        {
            string urlback = Session["back_url"] as string;
            Response.Redirect(urlback);
        }
    }
}