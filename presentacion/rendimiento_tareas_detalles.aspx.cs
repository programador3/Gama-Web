using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class rendimiento_tareas_detalles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Random randon = new Random();
                int value_session = randon.Next(1, 500000000);
                lblsession.Text = value_session.ToString();
                panel_repeat.Visible = true;
                panel_detalles.Visible = false;
                DateTime start = Convert.ToDateTime(funciones.de64aTexto(Request.QueryString["inicio"]));
                DateTime end = Convert.ToDateTime(funciones.de64aTexto(Request.QueryString["fin"]));
                int pidc_puesto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["pidc_puesto"]));
                int pidc_depto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["pidc_depto"]));
                CargaTareas(start, end, pidc_puesto, 0, pidc_depto);
            }
        }

        /// <summary>
        /// Carga Tareas
        /// </summary>
        public void CargaTareas(DateTime start, DateTime end, int pidc_puesto, int pidc_tarea, int idc_depto)
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pfecha = start;
                entidad.Pfecha_fin = end;
                entidad.Pidc_puesto = pidc_puesto;
                entidad.Pidc_tarea = pidc_tarea;
                entidad.Pidc_depto = idc_depto;
                entidad.Idc_usuario = Request.QueryString["ver_solo_asignadas"] != null ? 0 : Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pidc_puesto_asigna = Convert.ToInt32(Session["sidc_puesto_login"]);
                if (Request.QueryString["tipofiltro"] != null)
                {
                    entidad.Ptipof = Request.QueryString["tipofiltro"];
                }
                if (Request.QueryString["tipofiltrosistema"] != null)
                {
                    entidad.Ptipofs = Request.QueryString["tipofiltrosistema"];
                }
                DataSet ds = componente.TareasResultadoDetalles(entidad);
                lblhead.Text = ds.Tables[0].Rows[0]["encabezado"].ToString();
                gridtareas.DataSource = ds.Tables[1];
                gridtareas.DataBind();
                DataTable dt = ds.Tables[1];
                Session[lblsession.Text + "tareas"] = dt;
                if (pidc_tarea == 0)
                {
                    panel_detalles.Visible = false;
                }
                else
                {
                    lblprogress.Text = ds.Tables[1].Rows[0]["avance"].ToString() + "% de avance.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "DsssswwwE", "AsignaProgress('" + ds.Tables[1].Rows[0]["avance"].ToString() + "%');", true);
                    panel_detalles.Visible = true;
                    grid_detalles.DataSource = ds.Tables[2];
                    grid_detalles.DataBind();
                    DataTable dt2 = ds.Tables[2];
                    Session[lblsession.Text + "tareas_det"] = dt2;
                    gridproveedires.DataSource = ds.Tables[3];
                    gridproveedires.DataBind();
                    gridPapeleria.DataSource = ds.Tables[4];
                    gridPapeleria.DataBind();
                    proveedores.Visible = ds.Tables[3].Rows.Count == 0 ? false : true;
                    comentarios.Visible = ds.Tables[4].Rows.Count == 0 ? false : true;
                    Session["papeleria"] = ds.Tables[4];
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void lnkmistarea_Click(object sender, EventArgs e)
        {
            panel_repeat.Visible = false;
            panel_detalles.Visible = true;
        }

        protected void lnkexcel_Click(object sender, EventArgs e)
        {
            Export Export = new Export();
            //array de DataTables
            List<DataTable> ListaTables = new List<DataTable>();
            DataTable dt = (DataTable)Session[lblsession.Text + "tareas"];
            dt.Columns.Remove("idc_tarea");
            dt.Columns.Remove("idc_puesto");
            dt.Columns.Remove("idc_puesto_asigna");
            dt.Columns.Remove("descripcion");
            dt.Columns.Remove("estado");
            dt.Columns.Remove("css_class");
            dt.Columns.Remove("terminacion_sistema");
            dt.Columns.Remove("terminacion");
            dt.Columns["desc_completa"].SetOrdinal(0);
            ListaTables.Add(dt);
            string mensaje = "";
            if (Session[lblsession.Text + "tareas_det"] != null)
            {
                DataTable dt2 = (DataTable)Session[lblsession.Text + "tareas_det"];
                dt2.Columns.Remove("idc_tarea_historial");
                dt2.Columns["descripcion"].SetOrdinal(0);
                ListaTables.Add(dt2);
                //array de nombre de sheets
                string[] Nombres = new string[] { "Tareas", "Detalles" };
                mensaje = Export.toExcel(lblhead.Text, XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")), XLColor.White,
                                  XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 2,
                                  "Listado_de_Tareas.xlsx", Page.Response);
            }
            else
            {
                //array de nombre de sheetsConvert.ToDateTime(txtnueva_fecha.Text)
                string[] Nombres = new string[] { "Tareas" };
                mensaje = Export.toExcel(lblhead.Text, XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")), XLColor.White,
                                  XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 1,
                                  "Listado_de_Tareas.xlsx", Page.Response);
            }

            if (dt.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("Este Puesto no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
            }
            else
            {
                if (mensaje != "")
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }

        protected void lnkpdf_Click(object sender, EventArgs e)
        {
            Export Export = new Export();
            //array de DataTables
            List<DataTable> ListaTables = new List<DataTable>();
            DataTable dt = (DataTable)Session[lblsession.Text + "tareas"];
            dt.Columns.Remove("idc_tarea");
            dt.Columns.Remove("idc_puesto");
            dt.Columns.Remove("idc_puesto_asigna");
            dt.Columns.Remove("descripcion");
            dt.Columns["desc_completa"].SetOrdinal(0);
            ListaTables.Add(dt);
            string mensaje = "";
            if (Session[lblsession.Text + "tareas_det"] != null)
            {
                DataTable dt2 = (DataTable)Session[lblsession.Text + "tareas_det"];
                dt2.Columns.Remove("idc_tarea_historial");
                dt2.Columns["descripcion"].SetOrdinal(0);
                ListaTables.Add(dt2);
                //array de nombre de sheets
                string[] Nombres = new string[] { "Tareas", "Detalles" };
                Export.ToPdf("puestos", ListaTables, 2, Nombres, Page.Response);
            }
            else
            {
                //array de nombre de sheets
                string[] Nombres = new string[] { "Tareas" };
                Export.ToPdf("puestos", ListaTables, 1, Nombres, Page.Response);
            }

            if (dt.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("Este Puesto no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
            }
            else
            {
                if (mensaje != "")
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }

        protected void gridPapeleria_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                bool archi = Convert.ToBoolean(rowView["archivo"]);
                if (archi == false)
                {
                    e.Row.Cells[0].Controls.Clear();
                }
            }
        }

        protected void gridPapeleria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridPapeleria.DataKeys[index].Values["ruta"].ToString();
            string extension = gridPapeleria.DataKeys[index].Values["extension"].ToString();
            string descripcion = gridPapeleria.DataKeys[index].Values["descripcion"].ToString();
            string id_archi = gridPapeleria.DataKeys[index].Values["idc_tarea_archivo"].ToString();
            bool archivo = Convert.ToBoolean(gridPapeleria.DataKeys[index].Values["archivo"]);
            Session["id_archivo"] = id_archi;
            DataTable papeleria = (DataTable)Session["papeleria"];
            int papeleriat = papeleria.Rows.Count;
            switch (e.CommandName)
            {
                case "Descargar":
                    if (archivo == true)
                    {
                        Download(ruta, Path.GetFileName(ruta));
                    }
                    else
                    {
                        Alert.ShowAlertInfo("Este comentario no contiene archivo", "Mensaje del sistema", this);
                    }
                    break;
            }
        }

        /// <summary>
        /// Descarga un archivo
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file_name"></param>
        public void Download(string path, string file_name)
        {
            if (!File.Exists(path))
            {
                Alert.ShowAlertError("No tiene archivo relacionado", this);
            }
            else
            {
                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file_name);
                // Escribimos el fichero a enviar
                Response.WriteFile(path);
                // volcamos el stream
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
                // Response.End();
            }
        }

        protected void gridtareas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_tarea = Convert.ToInt32(gridtareas.DataKeys[index].Values["idc_tarea"].ToString());
            DateTime start = Convert.ToDateTime(funciones.de64aTexto(Request.QueryString["inicio"]));
            DateTime end = Convert.ToDateTime(funciones.de64aTexto(Request.QueryString["fin"]));
            int pidc_puesto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["pidc_puesto"]));
            switch (e.CommandName)
            {
                case "Detalles":
                    lblheaddet.Text = gridtareas.DataKeys[index].Values["desc_completa"].ToString();

                    CargaTareas(start, end, pidc_puesto, idc_tarea, Convert.ToInt32(funciones.de64aTexto(Request.QueryString["pidc_depto"])));
                    break;

                case "Ver Desc":
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "CargarTabla();", true);

                    ////Alert.ShowAlertInfo(gridtareas.DataKeys[index].Values["desc_completa"].ToString(), "Descripcion de la Tarea", this);

                    break;

                case "Arbol":
                    Response.Redirect("tareas_arbol.aspx?idc_tarea=" + funciones.deTextoa64(idc_tarea.ToString()));
                    break;
            }
        }

        protected void lnkmostrar_Click(object sender, EventArgs e)
        {
            panel_repeat.Visible = true;
            panel_detalles.Visible = false;
            DateTime start = Convert.ToDateTime(funciones.de64aTexto(Request.QueryString["inicio"]));
            DateTime end = Convert.ToDateTime(funciones.de64aTexto(Request.QueryString["fin"]));
            int pidc_puesto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["pidc_puesto"]));
            CargaTareas(start, end, pidc_puesto, 0, Convert.ToInt32(funciones.de64aTexto(Request.QueryString["pidc_depto"])));
        }

        protected void gridtareas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Image usuario = (Image)e.Row.FindControl("r_usuarios");
            Image sistema = (Image)e.Row.FindControl("r_sistema");
            Image edo = (Image)e.Row.FindControl("edo");
            Image externo = (Image)e.Row.FindControl("externo");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string usuario_value = rowView["TERMINACION"].ToString();
                string sistema_value = rowView["TERMINACION_SISTEMA"].ToString();
                string estado = rowView["estado"].ToString();
                string externostr = rowView["externo"].ToString();
                string css_class_arbol = rowView["css_class_arbol"].ToString();
                if (css_class_arbol == "")
                {
                    e.Row.Cells[1].Controls.Clear();
                }

                externo.ImageUrl = externostr;
                usuario.ImageUrl = usuario_value;
                sistema.ImageUrl = sistema_value;
                edo.ImageUrl = estado.TrimEnd();
                e.Row.Cells[10].CssClass = rowView["css_class"].ToString();
            }
        }
    }
}