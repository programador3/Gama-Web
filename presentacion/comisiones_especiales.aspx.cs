using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using negocio.Componentes;
using negocio.Entidades;
using System.Drawing;

namespace presentacion
{
    public partial class comisiones_especiales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //fecha al dia de hoy 
                DateTime hoy = DateTime.Today;

                txtfecha1.Text = hoy.AddDays(-15).ToString("yyyy-MM-dd");
                txtfecha2.Text = hoy.ToString("yyyy-MM-dd");
                //usuario
                int idc_user = Convert.ToInt32(Session["sidc_usuario"].ToString());
                //llamamos al metodo
                cargar_dataset(txtfecha1.Text, txtfecha2.Text, 127);
            }
        }

        protected void btnfiltrar_Click(object sender, EventArgs e)
        {
            limpiar();
            string fechai = txtfecha1.Text;
            string fechaf = txtfecha2.Text;
            int idc_user = Convert.ToInt32(Session["sidc_usuario"].ToString());
            cargar_dataset(fechai, fechaf, 127);
           
        }

        protected void cargar_dataset(string fechai, string fechaf, int idc_usuario) {
            if (funciones.EsFecha(fechai) == false || funciones.EsFecha(fechaf) == false)
            {
                msgbox.show("Solo se aceptan fechas validas.", this.Page);
                return;
            }
            //cargar datos de la tabla

            DataSet ds = new DataSet();
            prospectos_ventasE llenar_datos = new prospectos_ventasE();
            Prospectos_ventasBL datos = new Prospectos_ventasBL();

            llenar_datos.Idc_usuario = idc_usuario;
            llenar_datos.Fechai = fechai.Replace("-", "");
            llenar_datos.Fechaf = fechaf.Replace("-", "");
            try
            {
                ds = datos.reporte_comisiones_prospectos(llenar_datos);
                DataTable tbl_primario = ds.Tables[1];
                DataTable tbl_detalle = ds.Tables[0];
                DataTable tbl_comision_final = ds.Tables[2];

                //cargamos el grid principal
                grid_primario.DataSource = tbl_primario;
                grid_primario.DataBind();

                //mandamos a session las tablas
                Session.Add("TablaPrimario",tbl_primario);
                Session.Add("TablaDetalle", tbl_detalle);
                Session.Add("TablaComisionFinal", tbl_comision_final);
                

            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message, this.Page);
            }
        }

        protected void grid_primario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //me causaba error cuando usaba el link ver..
            //recuperamos el indice del registro seleccionado del grid de Telefono
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid_primario.DataKeys[index].Value);
            //Session["sidc_perfilgpo"] = Convert.ToInt32(vidc.ToString().Trim());
            switch (e.CommandName)
            {
                case "select_registro":
                    //hacemos el filtro
                    //filtramos y llenamos el GridView
                    DataTable tbl_detalle = (DataTable)Session["TablaDetalle"];
                    DataView dv = new DataView(tbl_detalle);
                    //HACEMOS FILTRO
                    dv.RowFilter = "idc_usuario ="+vidc;
                    //CARGAMOS DATOS
                    grid_detalle.DataSource = dv;
                    grid_detalle.DataBind();

                    gridse.Visible = true;
                    //comision total
                    DataTable tbl_comision_final = (DataTable)Session["TablaComisionFinal"];
                    lblcomisiontotal.Text =String.Format("{0:c}",tbl_comision_final.Rows[0]["total_comision"].ToString()); //string.Format(tbl_comision_final.Rows[0]["total_comision"].ToString(), "{0:c}");
                    //
                    GridViewRow row = grid_primario.Rows[index];
                    row.BackColor = Color.FromName("#C7EED8");
                    break;
               
            }
        }

        protected void grid_detalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                Label gridrevisado = (Label)e.Row.FindControl("lblrevisado");
                bool vrevisado = Convert.ToBoolean(rowView["revisado"]);
                gridrevisado.Text = (vrevisado == true) ? "Si" : "No";
            }
        }

        protected void limpiar() {
            //limpiar primero el grid de residuos
            DataTable tbl_clean = new DataTable();
            grid_detalle.DataSource = tbl_clean;
            grid_detalle.DataBind();
            lblcomisiontotal.Text = "";
            gridse.Visible = false;
        }

       
    }
}