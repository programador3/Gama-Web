using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class bono_presupuesto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["agente"] != null)
                {
                    txtnumagente.Text = funciones.de64aTexto(Request.QueryString["agente"]);
                    txtpresupuesto.Text = funciones.de64aTexto(Request.QueryString["presupuesto"]);
                    txtventa_modal.Text = funciones.de64aTexto(Request.QueryString["venta"]);
                    txtbono_presupuesto.Text = funciones.de64aTexto(Request.QueryString["bono"]);
                    txtbono_presupuesto.ForeColor = System.Drawing.Color.White;
                    txtbono_presupuesto.BackColor = System.Drawing.Color.FromName(funciones.de64aTexto(Request.QueryString["color"]));
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(),this);
            }
        }
    }
}