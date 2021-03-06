﻿using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace presentacion
{
    public partial class tareas_asignadas_listado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaTareas("");
            }
            if (Request.QueryString["lectura"] == null)
            {
                Session["redirect_pagedet"] = "tareas_listado.aspx";
            }
            else
            {
                Session["redirect_pagedet"] = "tareas_listado.aspx?lectura=lKJJHSJQHVXQHSVXVHJAKJBKJbkjJlkboqwdknwqknd";
            }
        }

        private void CargaTareas(string filtro)
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
                entidad.Pcorrecto = true;
                entidad.Parchivo = Request.QueryString["lectura"] != null ? true : false;
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
                if (Request.QueryString["lectura"] != null)
                {
                    tareas_todas.Visible = true;
                    repeatglobal.DataSource = ds.Tables[0];
                    repeatglobal.DataBind();
                    lbltotaltt.Text = " Todas las Tareas";
                    lbtot_global.Text = " Visualizando " + filter.Rows.Count.ToString() + " Tarea(s)";
                }
                else
                {
                    TAREAS_IND.Visible = true;
                    repeat_tareas.DataSource = ds.Tables[0];
                    repeat_tareas.DataBind();
                    lbltotaltt.Text = " Tiene un total de " + filter.Rows.Count.ToString() + " Tarea(s)";
                }
                Session["tabla_excel"] = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void txtpuesto_filtro_TextChanged(object sender, EventArgs e)
        {
            CargaTareas(txtpuesto_filtro.Text);
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            CargaTareas(txtpuesto_filtro.Text);
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

        protected void txt1_TextChanged(object sender, EventArgs e)
        {
            CargaTareas(txt1.Text);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            CargaTareas(txt1.Text);
        }
    }
}