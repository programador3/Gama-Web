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
    public partial class ver_preped_x_autorizar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!Page.IsPostBack)
            {
                cargar_combo_agentes_usuario();

            }
        }

        public void cargar_combo_agentes_usuario()
        {
            
            DataSet ds = new DataSet();
            try
            {
                AgentesCOM com = new AgentesCOM();
                ds = com.sp_combo_agentes_usu(Convert.ToInt32(Session["sidc_usuario"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("nombre2");
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][0].ToString() + " .- " + ds.Tables[0].Rows[i][1].ToString();
                    }
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        cboagentes.DataSource = ds.Tables[0];
                        cboagentes.DataValueField = "idc_agente";
                        cboagentes.DataTextField = "nombre2";
                        cboagentes.DataBind();
                        cargar_grid(Convert.ToInt32(cboagentes.SelectedValue));
                    }
                    else
                    {
                        cboagentes.DataSource = ds.Tables[0];
                        cboagentes.DataValueField = "idc_agente";
                        cboagentes.DataTextField = "nombre2";
                        cboagentes.DataBind();
                        cargar_grid(Convert.ToInt32(cboagentes.SelectedValue));
                    }
                }
                else
                {
                    cboagentes.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(),this);
            }
        }

        public void cargar_grid(int idc_agente)
        {
            DataSet ds = new DataSet();
            try
            {
                AgentesCOM com = new AgentesCOM();
                ds = com.sp_preped_x_autorizar(0, 0, idc_agente);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridpedidos.DataSource = ds.Tables[0];
                    gridpedidos.DataBind();
                }
                else
                {
                    Alert.ShowAlertError("No Existen Datos.", this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void cboagentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargar_grid(Convert.ToInt32(cboagentes.SelectedValue));
        }

        protected void gridpedidos_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem | e.Item.ItemType == ListItemType.Item)
            {
                e.Item.Cells[0].Attributes["onclick"] = "return ver_preped(" + e.Item.Cells[0].Text + "," + gridpedidos.ClientID + "," + e.Item.ItemIndex.ToString().Trim() + ");";
            }

        }
    }
}