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
                string lat, lon;
                lat = funciones.de64aTexto(Request.QueryString["latitud"]);
                lon = funciones.de64aTexto(Request.QueryString["longitud"]);
                //ejectuar funcion
                ClientScript.RegisterStartupScript(GetType(), "verMapa", "ver('" + lat + "','" + lon + "');", true);
            }
        }
    }
}