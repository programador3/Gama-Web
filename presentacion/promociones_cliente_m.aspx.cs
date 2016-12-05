using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class promociones_cliente_m : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    int idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["cdi"]));
                    int idc_listap = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["listap"]));
                    cargar_promociones(idc_cliente, idc_listap);
                }
                catch (Exception ex)
                {
                    CargarMsgBox("Error al Cargar Promociones Cliente. \\n \\u000b \\n Error: \\n" + ex.Message);
                }
            }
        }
        public void cargar_promociones(int idc_cliente, int idc_listap)
        {
            DataSet ds = new DataSet();
            AgentesCOM componente = new AgentesCOM();
            try
            {
                ds = componente.sp_promociones_cliente(idc_cliente, idc_listap);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridpromo.DataSource = ds.Tables[0];
                    gridpromo.DataBind();

                    gridregalos.DataSource = ds.Tables[1];
                    gridregalos.DataBind();

                    txtcliente.Text = ds.Tables[2].Rows[0][0].ToString().Trim();
                    txtcliente.Attributes["onclick"] = "alert('" + txtcliente.Text.Trim() + "');";

                    ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), "<script>datos(1);</script>", false);

                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarMsgBox(string msj)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), "<script>alert('" + msj.Replace("'", "") + "');</script>", false);
        }

        protected void gridpromo_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem | e.Item.ItemType == ListItemType.Item)
            {
                e.Item.Cells[0].Attributes["onclick"] = "return datos(" + e.Item.ItemIndex + 1 + ");";
            }

        }
    }
}