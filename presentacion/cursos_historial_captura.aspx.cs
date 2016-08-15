using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class cursos_historial_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int vidc_curso_historial = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["uidc_curso_historial"]))
                {
                    vidc_curso_historial = Convert.ToInt32(Request.QueryString["uidc_curso_historial"].ToString());
                    oc_empleado.Value = Request.QueryString["uemp"].ToString();
                }
                else
                {
                    vidc_curso_historial = 0;
                }

                //guardamos en campo oculto
                oc_idc_curso_historial.Value = vidc_curso_historial.ToString();
                //cargo los cbox
                cursos_cbox();
                // cursos_empleados_cbox();
                cursos_puestos_cbox();
                //crear tabla
                crearTabla();
                //captura
                if (vidc_curso_historial == 0)
                {
                    lit_titulo.Text = "Captura";
                }
                else
                {
                    //edicion
                    lit_titulo.Text = "Aplicación de Cursos";
                    if (vidc_curso_historial > 0)
                    {
                        //entidad
                        Cursos_HistorialENT EntCursoHist = new Cursos_HistorialENT();
                        EntCursoHist.Idc_curso_historial = vidc_curso_historial;
                        EntCursoHist.Empleado = Convert.ToBoolean(oc_empleado.Value);
                        //falta definir si es empleado o pre empleado
                        //EntCursoHist.Empleado = false; //esto esta estatico pero debe ser dinamico mediante la url
                        try
                        {
                            //ds
                            DataSet ds = new DataSet();
                            //componente
                            Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
                            ds = ComCursoHist.cursos_historial_detalle(EntCursoHist);
                            int total = ds.Tables[0].Rows.Count;
                            if (total == 0)
                            {
                                Alert.ShowAlertError("No se encontraron datos.", this.Page);
                            }
                            else
                            {
                                DataRow registro = ds.Tables[0].Rows[0];
                                //llenamos los controles
                                int idc_curso = Convert.ToInt32(registro["idc_curso"].ToString());
                                cboxcurso.SelectedValue = idc_curso.ToString();
                                //cboxempleados.SelectedValue = registro["idc_empleado"].ToString();
                                cboxestatus.SelectedValue = registro["estatus"].ToString();
                                cboxpuesto.SelectedValue = registro["idc_puesto"].ToString();
                                cursos_examenes_cbox(idc_curso);
                                cursos_examenes(idc_curso);
                                //cursos_examenes(Convert.ToInt32(registro["idc_curso"]));
                                if (registro["aprobado_capacitacion"] is DBNull)
                                {
                                    cboxaprob_capacitacion.SelectedIndex = 0;
                                }
                                else
                                {
                                    cboxaprob_capacitacion.SelectedValue = registro["aprobado_capacitacion"].ToString();
                                }

                                //if (registro["aprobado_gerencia"] is DBNull)
                                //{
                                //    cboxaprob_gerencia.SelectedIndex = 0;
                                //}
                                //else
                                //{
                                //    cboxaprob_gerencia.SelectedValue = registro["aprobado_gerencia"].ToString();
                                //}
                                txtresultado.Text = registro["resultado"].ToString();
                                txtobservaciones.Text = registro["observaciones"].ToString();

                                //llenamos el grid view

                                Session["TablaCursoHistorialExamen"] = ds.Tables[1];
                                grid_cursos_hist_exam_archivos.DataSource = Session["TablaCursoHistorialExamen"];//ds.Tables[2];
                                grid_cursos_hist_exam_archivos.DataBind();
                                solo_lectura();
                                if (oc_empleado.Value == "False")
                                { //solo aplica para los pre empleados
                                    oc_cursos_x_dar.Value = registro["cursos_x_dar"].ToString();
                                }

                                validar_examenes_ultimo_curso();
                            }
                        }
                        catch (Exception ex)
                        {
                            Alert.ShowAlertError(ex.Message, this.Page);
                        }
                    }
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

        /// <summary>
        /// valida si ya subieron todos los examenes para que puedan habilitarse los controles de resultado y observaciones y si es el ultimo curso que se habilite
        /// la opcion para mandar o no con el jefe directo
        /// </summary>
        protected void validar_examenes_ultimo_curso()
        {
            ////VALIDACION PARA HABILITAR / INHABILITAR CONTROLES
            //if (examenes_completos())
            //{
            //    txtresultado.Enabled = true;
            //    txtobservaciones.Enabled = true;
            //    cboxaprob_capacitacion.Enabled = true;
            //    cboxaprob_capacitacion.Enabled = true;
            //    if(oc_empleado.Value=="False"){
            //        //VALIDAMOS SI ES EL ULTIMO CURSO
            //        int ultimocurso = (oc_cursos_x_dar.Value == "") ? 0 : Convert.ToInt32(oc_cursos_x_dar.Value);
            //        if (ultimocurso <= 1)
            //        {
            //            //habilitar
            //            mensajealerta.InnerText = "Advertencia: Aqui se define si se manda al jefe directo los cursos que aplicaron al candidato. Recuerda subir los examenes que corresponden a este curso.";
            //        }
            //    }

            //}
            //else
            //{
            //    //solo_lectura();
            //    txtresultado.Enabled = false;
            //    txtobservaciones.Enabled = false;
            //    cboxaprob_capacitacion.Enabled = false;
            //}
            //FIN FIN VALIDACION PARA HABILITAR / INHABILITAR CONTROLES
        }

        protected void cursos_cbox()
        {
            //componente
            CursosCOM ComCurso = new CursosCOM();
            //ds
            DataSet ds = new DataSet();
            ds = ComCurso.cursos_cbox();
            cboxcurso.DataSource = ds.Tables[0];
            cboxcurso.DataTextField = "descripcion";
            cboxcurso.DataValueField = "idc_curso";
            cboxcurso.DataBind();
            cboxcurso.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        //protected void cursos_empleados_cbox() {
        //    Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
        //    //ds
        //    DataSet ds = new DataSet();
        //    ds = ComCursoHist.cursos_empleados_cbox();
        //    cboxempleados.DataSource = ds.Tables[0];
        //    cboxempleados.DataTextField = "nombre";
        //    cboxempleados.DataValueField = "idc_empleado";
        //    cboxempleados.DataBind();
        //    cboxempleados.Items.Insert(0, new ListItem("Seleccionar", "0"));
        //}

        protected void cursos_puestos_cbox()
        {
            Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
            //ds
            DataSet ds = new DataSet();
            ds = ComCursoHist.cursos_puestos_cbox();
            cboxpuesto.DataSource = ds.Tables[0];
            cboxpuesto.DataTextField = "descripcion";
            cboxpuesto.DataValueField = "idc_puesto";
            cboxpuesto.DataBind();
            cboxpuesto.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            DataTable tbl_cursos_hist_exam = (DataTable)Session["TablaCursoHistorialExamen"];
            DataTable tabla_examenes = (DataTable)Session["tabla_examenes"];
            int total_tabla_examenes = tabla_examenes.Rows.Count;
            int total_tbl_cursos_hist_exam = tbl_cursos_hist_exam.Rows.Count;
            if (total_tabla_examenes > total_tbl_cursos_hist_exam)
            {
                Alert.ShowAlertError("Debe ingresar los " + total_tabla_examenes.ToString() + " examenes.", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea guardar los cambios?');", true);
            }
        }

        protected bool validar()
        {
            //que seleccione un curso

            if (cboxcurso.SelectedIndex == 0)
            {
                Alert.ShowAlertError("Debe seleccionar el  curso", this.Page);
                return false;
            }

            //Seleecione el empleado
            //if (cboxempleados.SelectedIndex == 0)
            //{
            //    Alert.ShowAlertError("Debe seleccionar el empleado", this.Page);
            //    return false;
            //}
            if (cboxpuesto.SelectedIndex == 0)
            {
                Alert.ShowAlertError("Debe seleccionar el empleado", this.Page);
                return false;
            }

            //debe seleccionar el estatus
            if (cboxestatus.SelectedIndex == 0)
            {
                Alert.ShowAlertError("Debe seleccionar el estatus", this.Page);
                return false;
            }

            //aplicar la validacion si tiene tods los examenes que meta un resultado
            if (examenes_completos())
            {
                //haz la validacion
                if (cboxaprob_capacitacion.SelectedIndex > 0)
                {
                    if (txtresultado.Text == "")
                    {
                        Alert.ShowAlertError("Debe ingresar un valor en el resultado del curso", this.Page);
                        return false;
                    }
                    int vresultado = Convert.ToInt32(txtresultado.Text);
                    if (vresultado < 0 || vresultado > 100)
                    {
                        Alert.ShowAlertError("Debe ingresar un valor de cero a 100 (0 a 100)", this.Page);
                        return false;
                    }
                }
            }

            return true;
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(oc_paginaprevia.Value);
        }

        protected void limpiar()
        {
            //limpiamos el formulario
            cboxcurso.SelectedIndex = 0;
            //cboxempleados.SelectedIndex = 0;
            cboxpuesto.SelectedIndex = 0;
            cboxestatus.SelectedIndex = 0;
            cboxaprob_capacitacion.SelectedIndex = 0;
            // cboxaprob_gerencia.SelectedIndex = 0;
            txtresultado.Text = "";
            txtobservaciones.Text = "";
            oc_idc_curso_historial.Value = "0";
        }

        protected void cursos_examenes(int idc_curso)
        {
            //componente
            Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
            //ds
            DataSet ds = new DataSet();
            ds = ComCursoHist.cursos_examenes(idc_curso);
            Session["tabla_examenes"] = ds.Tables[0];
            gridExamenes.DataSource = ds.Tables[0];
            gridExamenes.DataBind();
        }

        protected void cursos_examenes_cbox(int idc_curso)
        {
            //componente
            Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
            //ds
            DataSet ds = new DataSet();
            ds = ComCursoHist.cursos_examenes(idc_curso);

            cboxcurso_examen.DataSource = ds.Tables[0];
            cboxcurso_examen.DataTextField = "descripcion";
            cboxcurso_examen.DataValueField = "idc_curso_exam";
            cboxcurso_examen.DataBind();
            cboxcurso_examen.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void cboxcurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            int vidc_curso = Convert.ToInt32(cboxcurso.SelectedValue);
            cursos_examenes_cbox(vidc_curso);
            cursos_examenes(vidc_curso);
        }

        //sube los archivos a la tabl temporal
        protected void btnaddfile_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboxcurso_examen.SelectedIndex == 0)
                {
                    Alert.ShowAlertError("Debe seleccionar un examen", this.Page);
                    return;
                }
                if (txtresexam.Text == "")
                {
                    Alert.ShowAlertError("Debe colocar un resultado para el examen", this.Page);
                    return;
                }
                string nombre_archivo = subirArchivo();
                if (nombre_archivo != "")
                {
                    int idc_curso_exam = Convert.ToInt32(cboxcurso_examen.SelectedValue);
                    int resultado = Convert.ToInt32(txtresexam.Text);
                    string obsexamen = txtobsexam.Text;
                    //lo agregamos al datatable
                    DataTable tbl_cursos_hist_exam = (DataTable)Session["TablaCursoHistorialExamen"];
                    DataRow nuevafila = tbl_cursos_hist_exam.NewRow();

                    nuevafila["idc_curso_hist_exam"] = IdMaxOpcion() + 1;
                    nuevafila["idc_curso_historial"] = Convert.ToInt32(oc_idc_curso_historial.Value);
                    nuevafila["idc_cursoe"] = idc_curso_exam;
                    nuevafila["resultado"] = resultado;
                    nuevafila["observaciones"] = obsexamen;
                    //
                    nuevafila["path"] = nombre_archivo;
                    nuevafila["extension"] = Path.GetExtension(nombre_archivo);
                    nuevafila["nuevo"] = true;
                    nuevafila["borrado"] = false;
                    nuevafila["examen"] = cboxcurso_examen.SelectedItem;
                    tbl_cursos_hist_exam.Rows.Add(nuevafila);
                    tbl_cursos_hist_exam.AcceptChanges();
                    //subimos a session
                    Session["TablaCursoHistorialExamen"] = tbl_cursos_hist_exam;
                    //actualizar el grid
                    update_grid_archivos();
                    limpiar_form_archivos();

                    Alert.ShowAlert("Archivo subido correctamente", "Mensaje", this.Page);
                }
                validar_examenes_ultimo_curso();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }

            //txtdescarchivo_1.Text = "";
            //irSeccion(txtdescarchivo_1.ClientID);
        }

        protected void limpiar_form_archivos()
        {
            cboxcurso_examen.SelectedIndex = 0;
            txtresexam.Text = "";
            txtobsexam.Text = "";
        }

        protected void update_grid_archivos()
        {
            //

            grid_cursos_hist_exam_archivos.DataSource = (DataTable)Session["TablaCursoHistorialExamen"];
            grid_cursos_hist_exam_archivos.DataBind();
        }

        protected int IdMaxOpcion()
        {
            int idmax = 0;
            //bajamos nuestra tabla de la session
            DataTable tbl_opciones = (System.Data.DataTable)(Session["TablaCursoHistorialExamen"]);
            if (tbl_opciones.Rows.Count > 0)
            {
                //revisamos
                int idval;
                foreach (DataRow fila in tbl_opciones.Rows)
                {
                    idval = Convert.ToInt32(fila["idc_curso_hist_exam"]);
                    //idval = Convert.ToInt32(fila["idc_curso_arc"]);
                    if (idval > idmax)
                    {
                        idmax = idval;
                    }
                }
            }
            else
            {
                idmax = 0;
            }
            return idmax;
        }

        protected void crearTabla()
        {
            DataTable workTable = new DataTable("cursos_historial_examenes");
            workTable.Columns.Add("idc_curso_hist_exam", typeof(String));
            workTable.Columns.Add("idc_curso_historial", typeof(String));
            workTable.Columns.Add("idc_cursoe", typeof(String));
            workTable.Columns.Add("path", typeof(String));
            workTable.Columns.Add("resultado", typeof(String));
            workTable.Columns.Add("observaciones", typeof(String));
            workTable.Columns.Add("extension", typeof(String));
            workTable.Columns.Add("nuevo", typeof(Boolean));
            workTable.Columns.Add("borrado", typeof(Boolean));
            workTable.Columns.Add("examen", typeof(String));
            //workTable.Columns.Add("dir_descarga", typeof(String));

            //Session
            Session.Add("TablaCursoHistorialExamen", workTable);
        }

        protected string subirArchivo()
        {
            Boolean fileOK = false;
            // String path = Server.MapPath("~/temp/cursos/");

            if (fileup_curso_arch_1.HasFile)
            {
                //String fileExtension =
                //    System.IO.Path.GetExtension(fileup_curso_arch_1.FileName).ToLower();
                //String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                //for (int i = 0; i < allowedExtensions.Length; i++)
                //{
                //    if (fileExtension == allowedExtensions[i])
                //    {
                fileOK = true;
                //    }
                //}
            }

            if (fileOK)
            {
                try
                {
                    //creamos la carpeta para guardar las imagenes
                    DirectoryInfo path = new DirectoryInfo(Path.Combine(Server.MapPath("~/temp/cursos_exam/"), Session["sidc_usuario"].ToString()));

                    //Path.Combine(Server.MapPath("~/temp/cursos/"));//, Session["sidc_usuario"].ToString()));
                    if (!path.Exists)
                    {
                        path.Create();
                    }
                    //Directory.CreateDirectory(path);
                    //revisar que no exista ya
                    if (File.Exists(path + Session["sidc_usuario"].ToString() + "_" + fileup_curso_arch_1.FileName))
                    {
                        //si existe no guardes
                        Alert.ShowAlertError("El archivo ya existe. Renombre el archivo  y vuelva a intentarlo.", this.Page);
                        return "";
                    }
                    else
                    {
                        fileup_curso_arch_1.PostedFile.SaveAs(path + "/" + Session["sidc_usuario"].ToString() + "_" + fileup_curso_arch_1.FileName);

                        return Session["sidc_usuario"].ToString() + "_" + fileup_curso_arch_1.FileName;
                    }
                }
                catch (Exception ex)
                {
                    Alert.ShowAlertError(ex.Message, this.Page);
                    return "";
                }
            }
            else
            {
                Alert.ShowAlertError("El archivo no cumple con el tipo de extensión válida", this.Page);
                return "";
            }
        }

        /// <summary>
        /// elimina los archivos y el directorio especificado en la carpeta temporal.
        /// </summary>
        /// <param name="target_dir"></param>
        public static void DeleteDirectory(string target_dir)
        {
            if (Directory.Exists(target_dir))
            {
                string[] files = Directory.GetFiles(target_dir);
                string[] dirs = Directory.GetDirectories(target_dir);

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                }

                foreach (string dir in dirs)
                {
                    DeleteDirectory(dir);
                }

                Directory.Delete(target_dir, false);
            }
        }

        protected void grid_cursos_hist_exam_archivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //me causaba error cuando usaba el link ver..
            //recuperamos el indice del registro seleccionado del grid de Telefono
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid_cursos_hist_exam_archivos.DataKeys[index].Value);
            //Session["sidc_perfilgpo"] = Convert.ToInt32(vidc.ToString().Trim());
            switch (e.CommandName)
            {
                //case "editar":
                //    Response.Redirect("grupos_backend_captura.aspx?uidc_perfilgpo=" + vidc);
                //    break;
                case "elimina_archivo":
                    //eliminar archivo de temp y de datatable
                    eliminaCursoArchivo(vidc);
                    update_grid_archivos();
                    validar_examenes_ultimo_curso();

                    break;

                case "metodo_descarga":
                    // Retrieve the row that contains the button clicked
                    // by the user from the Rows collection.
                    GridViewRow row = grid_cursos_hist_exam_archivos.Rows[index];

                    LinkButton linkeo = (LinkButton)row.FindControl("linkdescarga");
                    string ruta_descarga = linkeo.Attributes["ruta_descarga"].ToString();
                    string nombre_archivo = linkeo.Attributes["nombre_archivo"].ToString();
                    DescargarArchivo(ruta_descarga, nombre_archivo);
                    break;
            }
        }

        protected void DescargarArchivo(string path_archivo, string nombre_archivo)
        {
            if (File.Exists(path_archivo))
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
            else
            {
                Alert.ShowAlertError("NO EXISTE EL ARCHIVO " + nombre_archivo, this);
            }
        }

        public void eliminaCursoArchivo(int id)
        {
            try
            {
                //bajamos el datatable
                DataTable tbl_exam_arc = (DataTable)Session["TablaCursoHistorialExamen"];
                //buscamos el registro
                //string llave = (cbxTipo.Checked == true) ? "idc_curso_arc_borr" : "idc_curso_arc";
                string llave = "idc_curso_hist_exam";
                DataRow[] fila = tbl_exam_arc.Select(llave + "=" + id);
                //eliminamos la imagen
                string ruta = Path.Combine(Server.MapPath("~/temp/cursos_exam/"), Session["sidc_usuario"].ToString()) + "/" + fila[0]["path"].ToString();

                fila[0].Delete();
                fila[0].AcceptChanges();

                //subimos a session
                Session["TablaCursoHistorialExamen"] = tbl_exam_arc;
                irSeccion(grid_cursos_hist_exam_archivos.ClientID);
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        protected void irSeccion(string control_id)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "seccion", "irSeccion('#" + control_id + "');", true);
        }

        protected string[] cadena_curso_exam_archivos()
        {
            //recorrer el gridview y ver cuantos fueron seleccionados
            string[] array = new string[2];
            string cadena = "";
            int total = 0;
            //recorrer el datatable
            DataTable tbl_cursos_arc = (DataTable)Session["TablaCursoHistorialExamen"];
            if (tbl_cursos_arc.Rows.Count > 0)
            {
                foreach (DataRow fila in tbl_cursos_arc.Rows)
                {
                    cadena = cadena + fila["idc_curso_hist_exam"] + ";" + fila["idc_curso_historial"].ToString() + ";" + fila["idc_cursoe"].ToString() + ";" + fila["resultado"].ToString() + ";" + fila["observaciones"].ToString() + ";" + fila["extension"].ToString() + ";0;" + fila["nuevo"] + ";";
                    total = total + 1;
                }
            }

            array[0] = cadena;
            array[1] = total.ToString();
            return array;
        }

        /// <summary>
        /// inabilita los controles para que no los puedan modificar
        /// </summary>
        protected void solo_lectura()
        {
            cboxcurso.Enabled = false;
            cboxpuesto.Enabled = false;
            cboxestatus.Enabled = false;
            // cboxaprob_capacitacion.Enabled = false;
            //cboxaprob_gerencia.Enabled = false;
            //txtresultado.Enabled = false;
            //txtobservaciones.Enabled = false;
        }

        /// <summary>
        /// revisa si ya subio todos los examenes que debe
        /// </summary>
        protected bool examenes_completos()
        {
            bool resultado = false;
            //los que tengo subidos
            DataTable tbl_subidos = (DataTable)Session["TablaCursoHistorialExamen"];
            //los que debo subir
            //componente
            Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
            //ds
            DataSet ds = new DataSet();
            ds = ComCursoHist.cursos_examenes(Convert.ToInt32(cboxcurso.SelectedValue));
            DataTable tbl_a_subir = ds.Tables[0];
            int total_a_subir = tbl_a_subir.Rows.Count;
            if (total_a_subir > 0)
            {
                //coinciden
                int contador = 0;
                foreach (DataRow fila in tbl_a_subir.Rows)
                {
                    foreach (DataRow fila2 in tbl_subidos.Rows)
                    {
                        if (fila["idc_curso_exam"].ToString() == fila2["idc_cursoe"].ToString())
                        {
                            contador = contador + 1;
                            continue;
                        }
                    }
                }

                if (total_a_subir == contador)
                {
                    resultado = true;
                }
            }
            else
            {
                //quiere decir que el curso no tiene examenes y por ende puede darle un resultado y observaciones directo
                resultado = true;
            }

            return resultado;
        }

        protected void grid_cursos_hist_exam_archivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView fila = (DataRowView)e.Row.DataItem;
                string extension = fila["extension"].ToString();
                string archivo = "";
                string dir_descarga = "";
                int idc_curso_exam_arc = Convert.ToInt32(fila["idc_curso_hist_exam"]);

                if (fila["nuevo"].ToString() == "0")
                {
                    dir_descarga = fila["dir_descarga"].ToString();
                    //utiliza la referencia del id del archivo que esta en produccion
                    archivo = idc_curso_exam_arc.ToString() + extension;
                }
                else
                {
                    dir_descarga = Path.Combine(Server.MapPath("~/temp/cursos_exam/"), Session["sidc_usuario"].ToString());// +"/" + fila["path"].ToString();
                    archivo = fila["path"].ToString();
                }
                //instanciamos el link
                //HyperLink link = (HyperLink)e.Row.FindControl("linkdescarga");
                LinkButton link = (LinkButton)e.Row.FindControl("linkdescarga");
                link.Attributes["ruta_descarga"] = dir_descarga + "\\" + archivo;
                link.Attributes["nombre_archivo"] = archivo;
                //link.NavigateUrl = "http:"+dir_descarga+"\\"+archivo;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    //validar
                    if (validar() == false)
                    {
                        return;
                    }

                    //todo bien
                    //llamamos la entidad
                    Cursos_HistorialENT EntCursoHist = new Cursos_HistorialENT();
                    EntCursoHist.Idc_curso_historial = Convert.ToInt32(oc_idc_curso_historial.Value);
                    EntCursoHist.Idc_curso = Convert.ToInt32(cboxcurso.SelectedValue);
                    //EntCursoHist.Idc_empleado = Convert.ToInt32(cboxempleados.SelectedValue);
                    EntCursoHist.Idc_puesto = Convert.ToInt32(cboxpuesto.SelectedValue);
                    EntCursoHist.Estatus = Convert.ToChar(cboxestatus.SelectedValue);
                    EntCursoHist.Aprobado = (cboxaprob_capacitacion.SelectedValue == "Seleccionar") ? "" : cboxaprob_capacitacion.SelectedValue;
                    // EntCursoHist.Aprobado_jefe = (cboxaprob_gerencia.SelectedValue == "Seleccionar") ? "" : cboxaprob_gerencia.SelectedValue;

                    int vresultado = (txtresultado.Text == "") ? 0 : Convert.ToInt32(txtresultado.Text);
                    EntCursoHist.Resultado = vresultado;
                    EntCursoHist.Observaciones = txtobservaciones.Text;
                    //cadenas
                    string[] curso_exam_arch = cadena_curso_exam_archivos();
                    string cad_curso_exam_arch = curso_exam_arch[0];
                    int cad_curso_exam_arch_total = Convert.ToInt32(curso_exam_arch[1]);
                    EntCursoHist.Cad_curso_exam_archivo = cad_curso_exam_arch;
                    EntCursoHist.Cad_curso_exam_archivo_tot = cad_curso_exam_arch_total;
                    EntCursoHist.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    //entidad

                    try
                    {
                        //componente
                        Cursos_HistorialCOM ComCursohist = new Cursos_HistorialCOM();
                        //dataset
                        DataSet ds = new DataSet();
                        ds = ComCursohist.cursosHistorialCaptura(EntCursoHist);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                        {
                            // Alert.ShowAlert("Movimiento correcto", "Mensaje", this.Page);
                            //limpiar();
                            //es borrador
                            if (ds.Tables.Count > 1)
                            {
                                DataTable tabla_bd = ds.Tables[1]; //este viene de la base de datos
                                DataTable tabla_temp = (DataTable)Session["TablaCursoHistorialExamen"]; //este viene de la interfaz
                                DataRow[] fila_temp = tabla_temp.Select("nuevo=1");
                                //en teoria deben ser los mismos registros ordenados
                                //recorremos un datatable de la BD
                                int indice = 0;
                                foreach (DataRow fila in tabla_bd.Rows)
                                {
                                    string fileName = fila_temp[indice]["path"].ToString(); //archivo que se va a copiar se saca de la tabla temporal
                                    string sourcePath = Path.Combine(Server.MapPath("~/temp/cursos_exam/"), Session["sidc_usuario"].ToString());// Server.MapPath("~/temp/cursos/"); //carpeta temporal
                                    string targetPath = fila["ruta"].ToString(); //carpeta de red de destino este lo retorna la BD
                                    if (!System.IO.Directory.Exists(sourcePath))
                                    {
                                        Alert.ShowAlertError("no existe el path temporal", this.Page);
                                        return;
                                    }
                                    if (!System.IO.Directory.Exists(targetPath))
                                    {
                                        Alert.ShowAlertError("no existe el path de destino", this.Page);
                                        return;
                                    }
                                    //
                                    // Use Path class to manipulate file and directory paths.
                                    string fileNameNuevo = fila["idc_curso_hist_exam"].ToString() + fila["extension"].ToString(); //el nombre de archivo de destino en la unidad de red es el id del registro+ la extension

                                    string sourceFile = Path.Combine(sourcePath, fileName);
                                    string destFile = Path.Combine(targetPath, fileNameNuevo);
                                    //se copia el archivo
                                    try
                                    {
                                        File.Copy(sourceFile, destFile, true);
                                        indice = indice + 1;
                                    }
                                    catch (IOException copyError)
                                    {
                                        Alert.ShowAlertError(copyError.Message, this.Page);
                                        return;
                                    }

                                    //fin
                                }
                            }
                            DataTable tabla_bd2 = ds.Tables[1];
                            int total = (1 + (tabla_bd2.Rows.Count * 2)) * 1000;
                            string t = total.ToString();
                            Alert.ShowGiftMessage("Estamos procesando la cantidad de " + tabla_bd2.Rows.Count.ToString() + " archivo(s) al Servidor.", "Espere un Momento", "cursos_historial.aspx?uempleado=True", "imagenes/loading.gif", t, "El Curso fue Guardado Correctamente", this);
                        }
                        else
                        {
                            //limpiar();
                            Alert.ShowAlertError(vmensaje, this.Page);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.Message, this.Page);
                    }
                    break;
            }
        }

        protected void gridExamenes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridExamenes.DataKeys[index].Values["RUTA"].ToString();
            string name = Path.GetFileName(ruta);
            DescargarArchivo(ruta, name);
        }
    }
}