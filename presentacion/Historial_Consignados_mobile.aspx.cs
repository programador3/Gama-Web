using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace presentacion
{
    public partial class Historial_Consignados_mobile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id"]));
                cargar_datos_cliente(idc_cliente);
                cargar_grid_h_consignados(idc_cliente);
            }
        }

        public void cargar_datos_cliente(int idc_cliente)
        {            
            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.DataRow row = default(System.Data.DataRow);
            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pidc_cliente = idc_cliente;
                AgentesCOM com = new AgentesCOM();
                ds = com.sp_datos_cliente(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    txtcliente.Text = row["nombre"].ToString();
                    txtrfc.Text = row["rfccliente"].ToString();
                    txtcve.Text = row["cveadi"].ToString();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }
        public void cargar_grid_h_consignados(int idc_cliente)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pidc_cliente = idc_cliente;
                AgentesCOM com = new AgentesCOM();
                ds = com.sp_selecciona_consignado_cliente(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdconsignados.DataSource = ds.Tables[0];
                    grdconsignados.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void grdconsignados_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem | e.Item.ItemType == ListItemType.Item)
            {
                ImageButton imgselec = new ImageButton();
                imgselec = e.Item.FindControl("imgseleccionar") as ImageButton;
                imgselec.Attributes["onclick"] = "return regresa_valores( " + e.Item.ItemIndex + 1 + " );";
            }
        }
    }
}