using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class recoge_cliente_mobile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnaceptar.Attributes["OnClick"] = "return RegresaDatos();";
                //btncancelar.Attributes("OnClick") = "return cancelar();"
                cargar_sucursales_entrega();
                string idc_sucursal = Request.QueryString["idc_sucursal"];
                if (!string.IsNullOrEmpty(idc_sucursal))
                {
                    cbosucursales.SelectedValue = idc_sucursal;
                }
            }

        }

        public void cargar_sucursales_entrega()
        {
            try
            {
                AgentesCOM componente = new AgentesCOM();
                DataSet ds = componente.sp_sucursales_combo_entregas(); ;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbosucursales.DataSource = ds.Tables[0];
                    cbosucursales.DataValueField = "idc_sucursal";
                    cbosucursales.DataTextField = "nombre";
                    cbosucursales.DataBind();
                    cbosucursales.Items.Insert(0, "--- Seleccionar ---");
                }
                else
                {
                    Response.WriteFile("<Script> alert('No existen sucursales'); </Script>");
                }
            }
            catch (Exception ex)
            {
                Response.WriteFile("<Script> alert('"+ex.ToString()+"'); </Script>");
            }
        }
    }
}