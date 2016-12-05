using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class empleados_incidencias_reporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {

                Session["backurl"] = "empleados_incidencias_reporte.aspx";
                txtfechainicio.Text = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
                txtfechafin.Text = DateTime.Today.ToString("yyyy-MM-dd");
                if (Request.QueryString["desglo"] != null)
                {
                    if (Request.QueryString["report"] != null)
                    {
                        Session["backurl"] = "empleados_incidencias_reporte.aspx?desglo=1&report=JBSKJXBKJQBXKJQBKJQBKJBXJQBXKJSBXK&query=" + Request.QueryString["query"];
                        txtfechainicio.Text = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
                        LinkButton1.Visible = false;
                        txtfechainicio.ReadOnly = true;
                        txtfechafin.ReadOnly = true;
                        lnkcerrar.Visible = true;
                        CargarPendientesFiltro(DateTime.Now.AddDays(-1000), DateTime.Today, funciones.de64aTexto(Request.QueryString["query"]));
                    }
                }
                else {
                    concentrado.Visible = true;
                    LinkButton1.Visible = false;
                    LinkButton2.Visible = false;
                    lnkconcentrado.Visible = true;
                    if (Request.QueryString["report"] != null)
                    {
                        Session["backurl"] = "empleados_incidencias_reporte.aspx?report=JBSKJXBKJQBXKJQBKJQBKJBXJQBXKJSBXK&query=" + Request.QueryString["query"];
                        txtfechainicio.Text = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");                        
                        txtfechainicio.ReadOnly = true;
                        txtfechafin.ReadOnly = true;
                        lnkcerrar.Visible = true;
                       
                    }
                }
            }
        }

        private void CargarConcentradosFiltro(DateTime f1, DateTime f2, string query)
        {
            try
            {
                ReportesENT entidad = new ReportesENT();
                entidad.Pfecha = f1;
                entidad.Pfechafin = f2;
                ReportesCOM componente = new ReportesCOM();
                DataTable DT = componente.sp_reporte_incidencias_concentrado(entidad).Tables[0];
                DataView dv = DT.DefaultView;
                dv.RowFilter = query;
                repeat_principal.DataSource = dv.ToTable();
                repeat_principal.DataBind();
                ViewState["tabla_det"] = componente.sp_reporte_incidencias_concentrado(entidad).Tables[1];
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }


        private void CargarPendientesFiltro(DateTime f1, DateTime f2, string query)
        {
            try
            {
                ReportesENT entidad = new ReportesENT();
                entidad.Pfecha = f1;
                entidad.Pfechafin = f2;
                ReportesCOM componente = new ReportesCOM();
                DataTable DT = componente.CargaJefe(entidad).Tables[0];
                DataView dv = DT.DefaultView;
                dv.RowFilter = query;
                gridservicios.DataSource = dv.ToTable();
                gridservicios.DataBind();
                ViewState["dt_toexcel"] = dv.ToTable();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
        /// <summary>
        /// Carga los pendientes de mi puesto
        /// </summary>
        private void CargarPendientes(DateTime f1, DateTime f2)
        {
            try
            {
                ReportesENT entidad = new ReportesENT();
                entidad.Pfecha = f1;
                entidad.Pfechafin = f2;
                ReportesCOM componente = new ReportesCOM();
                DataTable DT = componente.CargaJefeFechas(entidad).Tables[0];
                gridservicios.DataSource = DT;
                gridservicios.DataBind();
                ViewState["dt_toexcel"] = DT;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }


        protected void gridservicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_empleadorep = Convert.ToInt32(gridservicios.DataKeys[index].Values["idc_empleadorep"].ToString());
            ViewState["idc_empleadorep"] = idc_empleadorep;
            ViewState["idc_empleado"] = Convert.ToInt32(gridservicios.DataKeys[index].Values["idc_empleado"].ToString());
            switch (e.CommandName)
            {
                case "Editar":
                    int idc = Convert.ToInt32(ViewState["idc_empleadorep"]);
                    int idc2 = Convert.ToInt32(ViewState["idc_empleado"]);
                    Session["backurl"] = "empleados_incidencias_reporte.aspx";
                    if (Request.QueryString["report"] != null)
                    {
                        Session["backurl"] = "empleados_incidencias_reporte.aspx?report=JBSKJXBKJQBXKJQBKJQBKJBXJQBXKJSBXK&query=" + Request.QueryString["query"];
                    }
                    string url = "empleados_reportes.aspx?view=KASKJOKXXBKQMBXOQKBXOQBXQJBXQJBJBKJ&termina=KJBKJBWQOWJBOQKBWDOQBOWKOKQNKOOKBAOBQDOKQND&idc_empleadorep=" + funciones.deTextoa64(idc.ToString()) + "&idc_empleado=" + funciones.deTextoa64(idc2.ToString());
                    Response.Redirect(url);
                    break;

            }
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso = (string)Session["Caso_Confirmacion"];
            int idc = Convert.ToInt32(ViewState["idc_empleadorep"]);
            int idc2 = Convert.ToInt32(ViewState["idc_empleado"]);
            switch (caso)
            {
                case "Editar":
                    Session["backurl"] = "empleados_incidencias_reporte.aspx";
                    if (Request.QueryString["report"] != null)
                    {
                        Session["backurl"] = "empleados_incidencias_reporte.aspx?report=JBSKJXBKJQBXKJQBKJQBKJBXJQBXKJSBXK&query=" + Request.QueryString["query"];
                    }
                    string url = "empleados_reportes.aspx?view=KASKJOKXXBKQMBXOQKBXOQBXQJBXQJBJBKJ&termina=KJBKJBWQOWJBOQKBWDOQBOWKOKQNKOOKBAOBQDOKQND&idc_empleadorep=" + funciones.deTextoa64(idc.ToString()) + "&idc_empleado=" + funciones.deTextoa64(idc2.ToString());
                    Response.Redirect(url);
                    break;
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (txtfechafin.Text == "")
            {
                Alert.ShowAlertError("Escriba una Fecha de Inicio",this);
            }
            else if (txtfechainicio.Text == "")
            {
                Alert.ShowAlertError("Escriba una Fecha de Fin",this);
            }
            else {
                DateTime f1 = Convert.ToDateTime(txtfechainicio.Text);
                DateTime f2 = Convert.ToDateTime(txtfechafin.Text);
                CargarPendientes(f1,f2);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (ViewState["dt_toexcel"] != null)
            {
                DataTable dt = ViewState["dt_toexcel"] as DataTable;
                dt.Columns.Remove("idc_empleadorep");
                dt.Columns.Remove("idc_empleado");
                dt.Columns.Remove("idc_tiporep");
                dt.Columns.Remove("fecha");
                dt.Columns.Remove("idc_usuario");
                dt.Columns.Remove("idc_usuariovobo");
                dt.Columns.Remove("idc_empleadovobo");
                dt.Columns.Remove("estado");
                dt.Columns["reporte"].ColumnName = "Reporte";
                dt.Columns["empleado"].ColumnName = "Empleado";
                dt.Columns["puesto"].ColumnName = "Puesto";
                dt.Columns["fecha_reporte"].ColumnName = "Fecha";
                dt.Columns["Empleado"].SetOrdinal(0);
                dt.Columns["Puesto"].SetOrdinal(1);
                dt.Columns["Reporte"].SetOrdinal(2);
                dt.Columns["Fecha"].SetOrdinal(3);
                Export Export = new Export();
                //array de DataTables
                List<DataTable> ListaTables = new List<DataTable>();
                ListaTables.Add(dt);
                //array de nombre de sheets
                string[] Nombres = new string[] { "Incidencias" };
                if (dt.Rows.Count == 0)
                {
                    Alert.ShowAlertInfo("Esta Tabla no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
                }
                else
                {
                    string mensaje = Export.toExcel("Incidencias", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                                       XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 1,
                                       "incidencias.xlsx", Page.Response);
                    if (mensaje != "")
                    {
                        Alert.ShowAlertError(mensaje, this);
                    }
                }
            }
            else {
                Alert.ShowAlertError("Seleccione un filtro.",this);
            }
        }

        protected void lnkcerrar_Click(object sender, EventArgs e)
        {
            Session["backurl"] = null;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMerfrfrssage", "window.close();", true);

        }

        protected void lnkconcentrado_Click(object sender, EventArgs e)
        {
            if (txtfechafin.Text == "")
            {
                Alert.ShowAlertError("Escriba una Fecha de Inicio", this);
            }
            else if (txtfechainicio.Text == "")
            {
                Alert.ShowAlertError("Escriba una Fecha de Fin", this);
            }
            else
            {
                DateTime f1 = Convert.ToDateTime(txtfechainicio.Text);
                DateTime f2 = Convert.ToDateTime(txtfechafin.Text);
                CargarConcentradosFiltro(f1, f2,"");
            }
        }

        protected void txtbuscar_TextChanged(object sender, EventArgs e)
        {

            if (txtfechafin.Text == "")
            {
                Alert.ShowAlertError("Escriba una Fecha de Inicio", this);
            }
            else if (txtfechainicio.Text == "")
            {
                Alert.ShowAlertError("Escriba una Fecha de Fin", this);
            }
            else
            {
                DateTime f1 = Convert.ToDateTime(txtfechainicio.Text);
                DateTime f2 = Convert.ToDateTime(txtfechafin.Text);
                string queyr = "empleado like '%"+txtbuscar.Text.Trim()+"%'";
                CargarConcentradosFiltro(f1, f2, queyr);
            }
        }

        protected void repeat_principal_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HtmlGenericControl detalles = e.Item.FindControl("detalles") as HtmlGenericControl;
            GridView griddet = (GridView)e.Item.FindControl("griddet");
            LinkButton lnkview = (LinkButton)e.Item.FindControl("lnkview");
            switch (e.CommandName)
            {
                case "View":
                    if (detalles.Visible)
                    {
                        detalles.Visible = false;
                        lnkview.CssClass = "list-group-item";
                    }
                    else {
                        DataTable dt = ViewState["tabla_det"] as DataTable;
                        DataView dv = dt.DefaultView;
                        string idc = lnkview.CommandArgument.ToCamel().Trim();
                        dv.RowFilter = "idc_empleado = " + idc + "";
                        griddet.DataSource = dv.ToTable();
                        griddet.DataBind();
                        detalles.Visible = dv.ToTable().Rows.Count > 0 ? true : false;
                        lnkview.CssClass= dv.ToTable().Rows.Count > 0 ? "list-group-item list-group-item-info" : "list-group-item";
                    }
                    break;
            }
        }

        protected void griddet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridView grid = sender as GridView;
            ViewState["idc_empleado"] = Convert.ToInt32(grid.DataKeys[index].Values["idc_empleado"].ToString());
            int idc_tiporep = Convert.ToInt32(grid.DataKeys[index].Values["idc_tiporep"].ToString());
            switch (e.CommandName)
            {
                case "Editar":
                    int idc2 = Convert.ToInt32(ViewState["idc_empleado"]);
                    string queyr = "idc_tiporep = "+ idc_tiporep .ToInvariantString().Trim()+ " and idc_empleado = " + idc2 + "";

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMrfrfrferfrfrssage", "window.open('empleados_incidencias_reporte.aspx?desglo=1&report=JBAKJSBKJSBJABSJABSJABS&query=" + funciones.deTextoa64(queyr) + "')", true);
                    break;

            }
        }
    }
}