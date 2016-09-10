using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio.Componentes;
using negocio.Entidades;

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