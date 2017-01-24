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
    public partial class tareas_asignadas_lista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_tarea");
                ViewState["dt_tareas_cancelar"] = dt;
                lnkcancelar.Visible = false;
                lnkseltodo.CssClass = "btn btn-default btn-block";
                CargaTareas("");
                Session["redirect_pagedet"] = "tareas_asignadas_lista.aspx";
            }
        }

        private void CargaTareas(string filtro)
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pidc_puesto_asigna = Convert.ToInt32(Session["sidc_puesto_login"]);
                entidad.Pcorrecto = true;
                entidad.Parchivo = false;
                DataSet ds = componente.CargarPendientesHoy(entidad);
                DataTable dt = ds.Tables[0];
                DataTable filter = new DataTable();
                DataView view = dt.DefaultView;
                if (filtro != "")
                {
                    filtro = filtro.TrimStart();
                    string primercaracter = filtro.Substring(0, 1);
                    if (primercaracter == "/")
                    {
                        filtro = filtro.Replace(primercaracter, "");
                        view.RowFilter = "depto Like '%" + filtro.TrimStart() + "%'";
                    }
                    else if (primercaracter == "+")
                    {
                        filtro = filtro.Replace(primercaracter, "");
                        view.RowFilter = "empleado Like '%" + filtro.TrimStart() + "%'";
                    }
                    else if (primercaracter == "*")
                    {
                        filtro = filtro.Replace(primercaracter, "");
                        view.RowFilter = "tipo Like '%" + filtro.TrimStart() + "%'";
                    }
                    else
                    {
                        view.RowFilter = "descripcion Like '%" + filtro.TrimStart() + "%' or fecha_compromiso Like '%" + filtro.TrimStart() + "%' or empleado Like '%" + filtro.TrimStart() + "%' or depto Like '%" + filtro.TrimStart() + "%' or tipo Like '%" + filtro.TrimStart() + "%'";
                    }
                    filter = view.ToTable();
                }
                else
                {
                    filter = dt;
                }
                TAREAS_IND.Visible = true;
                repeat_tareas.DataSource = ds.Tables[0];
                repeat_tareas.DataBind();
                lbltotaltt.Text = " Tiene un total de " + filter.Rows.Count.ToString() + " Tarea(s)";
                Session["tabla_excel"] = ds.Tables[0];
                //buscamos las tareas que esta canceladas para checearles
                DataTable dt_tareas_can = ViewState["dt_tareas_cancelar"] as DataTable;
                DataView dv = dt_tareas_can.DefaultView;
                foreach (RepeaterItem item in repeat_tareas.Items)
                {
                    Button btn = item.FindControl("lnkseleccionar") as Button;
                    int idc_tarea = Convert.ToInt32(btn.CommandArgument);
                    dv.RowFilter = "idc_tarea = "+idc_tarea.ToString()+"";
                    btn.CssClass = dv.ToTable().Rows.Count > 0 ? "btn btn-primary btn-block" : "btn btn-default btn-block";
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void txtpuesto_filtro_TextChanged(object sender, EventArgs e)
        {
            CargaTareas(TextBox1.Text);
        }
        protected void lnkselected_Click(object sender, EventArgs e)
        {
            try
            {

                Button lnk = sender as Button;
                lnk.CssClass = lnk.CssClass == "btn btn-default btn-block" ? "btn btn-primary btn-block" : "btn btn-default btn-block";
                int tipo = lnk.CssClass == "btn btn-default btn-block" ? 0 : 1;
                DataTable dt =
                    ViewState["dt_tareas_cancelar"] as DataTable;
                int idc_tarea = Convert.ToInt32(lnk.CommandArgument);
                switch (tipo)
                {
                    //eliminar
                    case 0:
                        foreach (DataRow row in dt.Rows)
                        {
                            int idc_tareafor = Convert.ToInt32(row["idc_tarea"]);
                            if (idc_tarea == idc_tareafor)
                            {
                                row.Delete();
                                break;
                            }
                        }
                        break;
                    case 1:
                        DataRow new_row = dt.NewRow();
                        new_row["idc_tarea"] = idc_tarea;
                        dt.Rows.Add(new_row);
                        break;

                }
                lnkcancelar.Visible = dt.Rows.Count > 0;
                lnkcancelar.OnClientClick = "ModalConfirm('Mensaje del Sistema', '¿Desea Cancelar un total de " + dt.Rows.Count.ToString() + " Tarea(s). ?', 'modal fade modal-danger');";
                ViewState["dt_tareas_cancelar"] = dt;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(),this);
            }
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            CargaTareas(TextBox1.Text);
        }
        protected void lnkborrar_Click(object sender, EventArgs e)
        {
            try
            {
                string cadena = "";
                int total = 0;
                foreach (RepeaterItem item in repeat_tareas.Items)
                {
                    Button btn = item.FindControl("lnkseleccionar") as Button;
                    if (btn.CssClass == "btn btn-primary btn-block")
                    {
                        int idc_tarea = Convert.ToInt32(btn.CommandArgument);
                        cadena = cadena + idc_tarea.ToString() + ";";
                        total++;
                    }
                }
                
                TareasCOM componente = new TareasCOM();
                DataSet ds = componente.sp_cantareas_masivo(cadena,total,Convert.ToInt32(Session["sidc_usuario"]), 
                    cbxcontarbien.Checked ?false:true,txtobservaciones.Text.ToUpper().Trim());
                DataRow row = ds.Tables[0].Rows[0];
                //verificamos que no existan errores
                string mensaje = row["mensaje"].ToString();
                if (mensaje == "")
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("idc_tarea");
                    ViewState["dt_tareas_cancelar"] = dt;
                    lnkcancelar.Visible = false;
                    lnkseltodo.CssClass = "btn btn-default btn-block";
                    CargaTareas("");
                    txtobservaciones.Text = "";
                    Alert.ShowAlert("Tareas Canceladas de Manera Correcta","Mensaje del Sistema",this);
                }
                else
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }

        }
        protected void lnksel_Click(object sender, EventArgs e)
        {
            lnkseltodo.CssClass= lnkseltodo.CssClass == "btn btn-default btn-block" ? "btn btn-primary btn-block" : "btn btn-default btn-block";
            int tipo = lnkseltodo.CssClass == "btn btn-default btn-block" ? 0 : 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("idc_tarea");
            foreach (RepeaterItem item in repeat_tareas.Items)
            {
                Button btn = item.FindControl("lnkseleccionar") as Button;
                btn.CssClass = lnkseltodo.CssClass;
                if (tipo == 1)
                {
                    DataRow new_row = dt.NewRow();
                    int idc_tarea = Convert.ToInt32(btn.CommandArgument);
                    new_row["idc_tarea"] = idc_tarea;
                    dt.Rows.Add(new_row);
                }
            }

            lnkcancelar.Visible = dt.Rows.Count > 0;            
            lnkcancelar.OnClientClick = "ModalConfirm('Mensaje del Sistema', '¿Desea Cancelar un total de " + dt.Rows.Count.ToString() + " Tarea(s). ?', 'modal fade modal-danger');";
            ViewState["dt_tareas_cancelar"] = dt;
        }
        protected void lnkexcel_Click(object sender, EventArgs e)
        {
            CargaTareas("");
            Export Export = new Export();
            //array de DataTables
            List<DataTable> ListaTables = new List<DataTable>();
            DataTable dt = (DataTable)Session["tabla_excel"];
            dt.Columns.Remove("idc_tarea");
            dt.Columns.Remove("idc_puesto");
            dt.Columns.Remove("idc_puesto_asigna");
            dt.Columns.Remove("descripcion");
            dt.Columns.Remove("fo");
            dt.Columns.Remove("url");
            dt.Columns.Remove("icono");
            dt.Columns.Remove("css_class");
            dt.Columns["empleado"].SetOrdinal(0);
            dt.Columns["empleado_asigna"].SetOrdinal(1);
            dt.Columns["empleado"].ColumnName = "Empleado";
            dt.Columns["empleado_asigna"].ColumnName = "Empleado Asigno";
            dt.Columns["tipo"].ColumnName = "Estado de la Tarea";
            dt.Columns["desc_completa"].ColumnName = "Descripcion";
            ListaTables.Add(dt);
            string mensaje = "";
            //array de nombre de sheetsConvert.ToDateTime(txtnueva_fecha.Text)
            if (dt.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("Este Puesto no cuenta con ningun dato para crear un reporte.", "", this);
            }
            else
            {
                string[] Nombres = new string[] { "Tareas" };
                mensaje = Export.toExcel("Tareas Pendientes", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")), XLColor.White,
                                  XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 1,
                                  "Listado_de_Tareas.xlsx", Page.Response);
                if (mensaje != "")
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }
    }
}