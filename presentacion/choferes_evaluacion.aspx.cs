 using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;

using System.Globalization;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ClosedXML.Excel;

namespace presentacion
{  
    public partial class choferes_evaluacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtguid.Text = Guid.NewGuid().ToString().Trim();
                Session[txtguid.Text+"dt_reporchoferes"] = null;
                IniciarCombos();
                ddlmeses.SelectedValue = (DateTime.Today.Month - 1).ToString().Trim();
                ddlaño.SelectedValue = DateTime.Today.Year.ToString().Trim();
            }
        }

        private void CargarGrid(int mes, int año)
        {
            try
            {
                ReportesCOM componete = new ReportesCOM();
                DataSet ds = componete.SP_REPORTE_EVALUACION_CHOFERES(mes, año);
               // Session[txtguid.Text + "dt_reporchoferes"] = ds.Tables[0];
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                dt.Columns.RemoveAt(dt.Columns.Count - 1);
                gridchoferes.DataSource = dt;
                gridchoferes.DataBind();
                LinkButton2.Visible = dt.Rows.Count > 0;
                Session[txtguid.Text + "dt_reporchoferes"] = componete.SP_REPORTE_EVALUACION_CHOFERES(mes, año).Tables[0];
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }
        private void IniciarCombos()
        {
            ddlmeses.DataTextField = "name";
            ddlmeses.DataValueField = "value";
            ddlmeses.DataSource = funciones.meses();
            ddlmeses.DataBind();
            ddlmeses.Items.Insert(0, new ListItem("--Seleccione un Mes", "0"));
            ddlaño.DataTextField = "value";
            ddlaño.DataValueField = "name";
            ddlaño.DataSource = funciones.años();
            ddlaño.DataBind();
            ddlaño.Items.Insert(0, new ListItem("--Seleccione un Año", "0"));

        }

        protected void lnkbuscar_Click(object sender, EventArgs e)
        {
            int mes = Convert.ToInt32(ddlmeses.SelectedValue);
            int año = Convert.ToInt32(ddlaño.SelectedValue);
            if (mes == 0)
            {
                Alert.ShowAlertError("Seleccione un Mes",this); 
            }
            else if (año == 0)
            {
                Alert.ShowAlertError("Seleccione un Año", this);
            }
            else {
                CargarGrid(mes,año);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            DataTable dt = Session[txtguid.Text + "dt_reporchoferes"] as DataTable;
            Export Export = new Export();
            //array de DataTables
            List<DataTable> ListaTables = new List<DataTable>();
            ListaTables.Add(dt);
            //array de nombre de sheets
            string[] Nombres = new string[] { "Listado" };
            if (dt.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("Este Reporte no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
            }
            else
            {
                string mensaje = Export.toExcel("Lista de Evaluacion de Choferes", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString("dd MMMM, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")), XLColor.White,
                                   XLColor.Black, 10, ListaTables, XLColor.DimGray, XLColor.White, Nombres, 1,
                                   "choferes.xlsx", Page.Response);
                if (mensaje != "")
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }

        protected void gridchoferes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string EMPLEADO = gridchoferes.DataKeys[index].Values["EMPLEADO"].ToString().Trim();
            DataTable dt = Session[txtguid.Text + "dt_reporchoferes"] as DataTable;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "empleado = '"+EMPLEADO+"'";
            DataTable dtr = dv.ToTable();
            if (dtr.Rows.Count > 0)
            {
                DateTime f2 = Convert.ToDateTime(ddlaño.SelectedValue.Trim()+"-"+ddlmeses.SelectedValue.Trim()+"-01");
                DateTime f1 = Convert.ToDateTime(ddlaño.SelectedValue.Trim() + "-" + ddlmeses.SelectedValue.Trim() + "-01");
                string idc_empleado = dtr.Rows[0]["idc_empleado"].ToString().Trim();
                string url = "tareas_informacion_adicional.aspx?idc_tipoi=" + funciones.deTextoa64("9") +
                    "&idc_proceso=" + funciones.deTextoa64(idc_empleado) + "&f1=" + funciones.deTextoa64(f1.ToString("yyyy-MM-dd"))
                    + "&f2=" + funciones.deTextoa64(f2.ToString("yyyy-MM-dd")); 
                ScriptManager.RegisterStartupScript(this, GetType(),Guid.NewGuid().ToString(),"window.open('"+url+"');",true);
            }
        }
    }
}