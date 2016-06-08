using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class cursos_revisar_gerencia_detalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //recibo el parametro de la url
                //recibimos mediante url si mostrar los empleados o pre empleados
                if (Request.QueryString["uidc"] != null & Request.QueryString["tipoemp"] != null)// si el request viene vacio iniciamos en borrador
                {
                    int vidc = Convert.ToInt32(Request.QueryString["uidc"]);
                    bool tipoemp = Convert.ToBoolean(Request.QueryString["tipoemp"]);
                    oc_tipoemp.Value = tipoemp.ToString();
                    oc_idc.Value = vidc.ToString();
                    cargar_info(vidc, tipoemp);
                    lit_titulo.Text = "Detalles de la Capacitación o Cursos tomados";
                }
                else
                {
                    Alert.ShowAlertError("Información no disponible", this.Page);
                }

                //add 18-09-2015
                if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_REFERER"]))
                {
                    oc_paginaprevia.Value = Request.ServerVariables["HTTP_REFERER"].ToString();
                }
                else
                {
                    oc_paginaprevia.Value = "cursos_historial.aspx";
                }
            }
        }

        protected void cargar_info(int idc, bool tipoemp)
        {
            try
            {
                //entidad
                Cursos_HistorialENT EntCursoHist = new Cursos_HistorialENT();
                if (tipoemp)
                {//es empleado
                    EntCursoHist.Idc_empleado = idc;
                }
                else
                {
                    EntCursoHist.Idc_pre_empleado = idc;
                }
                EntCursoHist.Empleado = tipoemp;
                //dataset
                DataSet ds = new DataSet();
                //componente
                Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
                //resultado
                ds = ComCursoHist.cursos_revisar_gerencia_detalle(EntCursoHist);
                //cargar datos en los datatable
                DataTable tbl_cursos = ds.Tables[0];
                DataTable tbl_aprobocap = ds.Tables[1];
                DataTable tbl_cursos_exam = ds.Tables[2];
                //subir a session
                Session.Add("TablaAproboCap", tbl_aprobocap);
                Session.Add("TablaCursoExam", tbl_cursos_exam);
                //solo se llena la principal y en base a esa se hace el filtro
                //liberar mensaje
                if (tbl_cursos.Rows.Count == 0)
                {
                    panel_mensaje.Visible = true;
                    lblmensaje_quienaprobo.Text = "No existen registros";
                    lblmensaje_examenes.Text = "No existen registros";
                }
                else
                {
                    lblmensaje_quienaprobo.Text = "Seleccione un curso";
                    lblmensaje_examenes.Text = "Seleccione un curso";
                }
                grid_cursos.DataSource = tbl_cursos;
                grid_cursos.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        protected void filtrar_detalle(int idc_curso_historial)
        {
            //cargar datos en los datatable

            DataTable tbl_aprobocap = (DataTable)Session["TablaAproboCap"];
            DataTable tbl_cursos_exam = (DataTable)Session["TablaCursoExam"];
            //meterlos en un dataview para el filtrado
            DataView dv_aprobocap = tbl_aprobocap.DefaultView;
            DataView dv_cursos_exam = tbl_cursos_exam.DefaultView;
            //
            dv_aprobocap.RowFilter = "idc_curso_historial=" + idc_curso_historial;
            dv_cursos_exam.RowFilter = "idc_curso_historial=" + idc_curso_historial;
            //llenar los datos correspondientes
            grid_cursos_aprobocap.DataSource = dv_aprobocap;
            grid_cursos_aprobocap.DataBind();
            //
            grid_cursos_exam.DataSource = dv_cursos_exam;
            grid_cursos_exam.DataBind();
            //aprobo
            //lanzar los mensajes
            if (dv_aprobocap.Count == 0)
            {
                panel_mensaje_quien_aprobo.Visible = true;
                lblmensaje_quienaprobo.Text = "No existen registros";
            }
            else
            {
                panel_mensaje_quien_aprobo.Visible = false;
            }

            if (dv_cursos_exam.Count == 0)
            {
                panel_mensaje_examenes.Visible = true;
                lblmensaje_examenes.Text = "No existen registros";
            }
            else
            {
                panel_mensaje_examenes.Visible = false;
            }
        }

        protected void grid_cursos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //panel_comentarios.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid_cursos.DataKeys[index].Value);
            //oc_modal_idc_curso_historial.Value = vidc.ToString();
            switch (e.CommandName)
            {
                case "clic_detalle":
                    filtrar_detalle(vidc);
                    break;
            }
        }

        protected void grid_cursos_exam_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //panel_comentarios.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "metodo_descarga":
                    // Retrieve the row that contains the button clicked
                    // by the user from the Rows collection.
                    GridViewRow row = grid_cursos_exam.Rows[index];

                    LinkButton linkeo = (LinkButton)row.FindControl("linkdescarga");
                    string ruta_descarga = linkeo.Attributes["ruta_descarga"].ToString();
                    string nombre_archivo = linkeo.Attributes["nombre_archivo"].ToString();
                    DescargarArchivo(ruta_descarga, nombre_archivo);
                    break;
            }
        }

        protected void DescargarArchivo(string path_archivo, string nombre_archivo)
        {
            // Limpiamos la salida
            Response.Clear();
            // Con esto le decimos al browser que la salida sera descargable
            Response.ContentType = "application/octet-stream";
            // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nombre_archivo);//Revision.pptx");
            // Escribimos el fichero a enviar
            Response.WriteFile(path_archivo);
            // volcamos el stream
            Response.Flush();
            // Enviamos todo el encabezado ahora
            Response.End();
        }

        protected void grid_cursos_exam_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView fila = (DataRowView)e.Row.DataItem;
                string extension = fila["extension"].ToString();
                string archivo = "";
                string dir_descarga = "";
                int idc_curso_exam_arc = Convert.ToInt32(fila["idc_curso_hist_exam"]);

                dir_descarga = fila["dir_descarga"].ToString();
                //utiliza la referencia del id del archivo que esta en produccion
                archivo = idc_curso_exam_arc.ToString() + extension;

                //instanciamos el link
                //HyperLink link = (HyperLink)e.Row.FindControl("linkdescarga");
                LinkButton link = (LinkButton)e.Row.FindControl("linkdescarga");
                link.Attributes["ruta_descarga"] = dir_descarga + "\\" + archivo;
                link.Attributes["nombre_archivo"] = archivo;
                //link.NavigateUrl = "http:"+dir_descarga+"\\"+archivo;
            }
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            //revisar si selecciono el radiobtn o no
            if (rbtnaprobar_jefe.SelectedIndex < 0)
            {
                Alert.ShowAlertError("Debe seleccionar una opción Aprobar o Rechazar", this.Page);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar el registro?');", true);
            }
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(oc_paginaprevia.Value);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":

                    try
                    {
                        //entidad
                        Cursos_HistorialENT EntCursoHist = new Cursos_HistorialENT();
                        bool tipoent = Convert.ToBoolean(oc_tipoemp.Value);
                        if (tipoent)
                        { //es empleado
                            EntCursoHist.Idc_empleado = Convert.ToInt32(oc_idc.Value);
                        }
                        else
                        { // es pre empleado
                            EntCursoHist.Idc_pre_empleado = Convert.ToInt32(oc_idc.Value);
                        }

                        EntCursoHist.Aprobado_jefe = rbtnaprobar_jefe.SelectedValue;
                        EntCursoHist.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        EntCursoHist.Observaciones = txtobs.Text;
                        //dataset
                        DataSet ds = new DataSet();
                        //componente
                        Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
                        if (tipoent)
                        { // es empleado
                            ds = ComCursoHist.cursos_aprobar_revision_gerencia_empleado(EntCursoHist);
                        }
                        else
                        {
                            ds = ComCursoHist.cursos_aprobar_revision_gerencia(EntCursoHist);
                            // es pre empleado
                        }

                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                        {
                            if (rbtnaprobar_jefe.SelectedValue == "True")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('Candidato Aprobado Correctamente','menu.aspx');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('Candidato Rechazado Correctamente','menu.aspx');", true);
                            }
                            rbtnaprobar_jefe.ClearSelection();
                            txtobs.Text = "";
                            cargar_info(0, Convert.ToBoolean(oc_tipoemp.Value));
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this.Page);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.Message, this.Page);
                    }

                    break;
            }
        }
    }
}