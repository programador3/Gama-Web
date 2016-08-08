using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class candidatos_preparar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!Page.IsPostBack)
            {
                Session["temp_table_id_reclu"] = null;
                DataPrep();
            }
            H1.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"].ToString()), 348);
        }

        /// <summary>
        /// Carga los puestos pendientes de preparar
        /// </summary>
        public void DataPrep()
        {
            CandidatosENT entidad = new CandidatosENT();
            CandidatosCOM componente = new CandidatosCOM();
            entidad.Pidc_puesto = 0;
            entidad.Pidc_prepara = 0;
            entidad.Pidc_puestobaja = Convert.ToInt32(Session["sidc_puesto_login"]);
            DataSet ds = componente.CargaPuestos(entidad);
            repeatpendientes.DataSource = ds.Tables[0];
            repeatpendientes.DataBind();
            gridreclu.DataSource = ds.Tables[0];
            gridreclu.DataBind();
            Session["temp_table_id_reclu"] = ds.Tables[0];
            lblto.Text = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count == 0) { Noempleados.Visible = true; }
        }

        protected void lnkGO_Click(object sender, EventArgs e)
        {
            LinkButton lnkGO = (LinkButton)sender;
            //PanelDetalles.Visible = true;
            // DataPrebajasDetalles(Convert.ToInt32(lnkGO.CommandName.ToString()), Convert.ToInt32(lnkGO.CommandArgument.ToString()));
            // lnklista.Visible = true;
            Response.Redirect("candidatos_preparar_captura.aspx?idc_puesto=" + funciones.deTextoa64(lnkGO.CommandArgument.ToString()) + "&idc_prepara=" + funciones.deTextoa64(lnkGO.CommandName.ToString()));
        }

        protected void repeatpendientes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            //Panel Panel = (Panel)e.Item.FindControl("panel_revision");
            string idc_puesto = Convert.ToString(DataBinder.Eval(dbr, "idc_puesto"));
            string idc_prepara = Convert.ToString(DataBinder.Eval(dbr, "idc_prepara"));
            LinkButton lnkGOdET = (LinkButton)e.Item.FindControl("lnkGOdET");
            LinkButton lnkGO = (LinkButton)e.Item.FindControl("lnkGO");
            //asiganmos datos
            lnkGO.CommandArgument = idc_puesto;
            lnkGOdET.CommandArgument = idc_puesto;
            lnkGO.CommandName = idc_prepara;
            lnkGOdET.CommandName = idc_prepara;
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] == null)
            {
                Response.Redirect("administrador_prebajas.aspx");
            }
            else
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        protected void lnkexcel_Click(object sender, EventArgs e)
        {
            Export Export = new Export();
            //array de DataTables
            List<DataTable> ListaTables = new List<DataTable>();
            DataTable dt = (DataTable)Session["temp_table_id_reclu"];
            dt.Columns.Remove("idc_puesto");
            dt.Columns.Remove("idc_prepara");
            dt.Columns.Remove("num_total");
            dt.Columns.Remove("css_class");
            dt.Columns.Remove("fecha_compromiso_reclu");
            dt.Columns.Remove("fecha_compromiso_reclutamiento");
            dt.Columns["descripcion"].ColumnName = "Puesto";
            dt.Columns["fecha_registro"].ColumnName = "Fecha de Solicitud";
            ListaTables.Add(dt);
            string mensaje = "";
            string[] Nombres = new string[] { "Detalles de Reclutamiento" };
            mensaje = Export.toExcel("Reporte de Reclutamiento", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")), XLColor.White,
                              XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 1,
                              "Listado_de_Reclutamiento.xlsx", Page.Response);

            if (mensaje != "")
            {
                Alert.ShowAlertError(mensaje, this);
            }
        }

        protected void gridreclu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string idc_puesto = gridreclu.DataKeys[index].Values["idc_puesto"].ToString();
            string idc_prepara = gridreclu.DataKeys[index].Values["idc_prepara"].ToString();
            Response.Redirect("candidatos_preparar_captura.aspx?idc_puesto=" + funciones.deTextoa64(idc_puesto) + "&idc_prepara=" + funciones.deTextoa64(idc_prepara));
        }
    }
}