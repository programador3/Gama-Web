using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class grafica_ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["u"] != null && Request.QueryString["v"] != null)
            {
                decimal u = Convert.ToDecimal(funciones.de64aTexto(Request.QueryString["u"]));
                decimal v = Convert.ToDecimal(funciones.de64aTexto(Request.QueryString["v"]));
                Label1.Text = v.ToString("#.##")+"%";
                Label2.Text = u.ToString("#.##")+"%";
                if (v > 100) { v = 100; }
                decimal totalu = 100 - u;
                decimal totalv = 100 - v;
                if (v == 100)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "CargarGraficaVentas0('', " + v + ", " + totalv + ");", true);
                } else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "CargarGraficaVentas('', " + v + ", " + totalv + ");", true);
                }
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "CargarGraficaUtilidad('', "+u+", "+totalu+");", true);
            }

        }
    }
}