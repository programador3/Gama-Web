using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;

using System.Globalization;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class comisiones_Detalles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //comisiones_detalles.aspx?p_ventad={0}&p_ventac={1}&p_comisiond={2}&p_comisionc={3}

            if (!IsPostBack)
            {
                txtventa_d.Text = funciones.de64aTexto(Request.QueryString["p_ventad"].ToString());
                txtventa_c.Text = funciones.de64aTexto(Request.QueryString["p_ventac"].ToString());

                txtcomision_d.Text = funciones.de64aTexto(Request.QueryString["p_comisiond"].ToString());
                txtcomision_c.Text = funciones.de64aTexto(Request.QueryString["p_comisionc"].ToString());
                txtapo.Text = funciones.de64aTexto(Request.QueryString["apo"].ToString());

                DataTable ds = (DataTable)Session["ds"];
                gridv2.DataSource = ds;
                gridv2.DataBind();
                
            }

            //gridv2.HeaderRow.TableSection = TableRowSection.TableHeader;
            //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalEditar('Detalle de Aportaciones');", true);
        }

        protected void gridv2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            DataKey datos = gridv2.DataKeys[index];
            string codfac, Venta, apo, tipod;
            codfac = datos.Values["codfac"].ToString();
            Venta = gridv2.DataKeys[index].Values["Venta"].ToString();
            apo = gridv2.DataKeys[index].Values["apo"].ToString();
            tipod = gridv2.DataKeys[index].Values["tipod"].ToString();

            h_idc_factura.Value = codfac;
            lbldoc.Text = codfac;

            DataTable dt = new DataTable();
            dt = (DataTable)Session["dv_com"];
            if (!dt.Columns.Contains("apo_factura"))
            {
                dt.Columns.Add("apo_factura", typeof(decimal));
                DataView dv = new DataView();
                dv = dt.DefaultView;
                for (int i = 0; i <= dv.Table.Rows.Count - 1; i++)
                {
                    dv[i]["apo_factura"] = Convert.ToDecimal(dv.Table.Compute("SUM(apo)", "idc_factura=" + dv[i]["idc_factura"]));
                }
            }
            if ((dt != null) & dt.Rows.Count > 0)
            {
                //dt.Select("Documento=" + codfac);
                DataRow[] result = dt.Select("codfac='" + codfac.Trim() + "'");
                DataTable dt1 = new DataTable();
                foreach (DataRow row in result)
                {
                    dt1 = result.CopyToDataTable();
                    //dt1.ImportRow(row);
                }
                Grid_fac.DataSource = dt1;
                Grid_fac.DataBind();
                //div_grid1.Visible = false;
                //div_fac.Visible = true;

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalEditar('Detalle de Aportaciones');", true);
            }



            //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "filtrar_grid();", true);
        }

        protected void cerrar_click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this,GetType(),Guid.NewGuid().ToString(),"window.close();",true);

        } 
    }
}