using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class tareas_detalles : System.Web.UI.Page
    {
        public bool solicitud_fechaextra = false;
        private string url_back = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("descripcion");
                papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("extension");
                papeleria.Columns.Add("id_archi");
                papeleria.Columns.Add("archivo");
                Session["tarea_auto"] = null;
                Session["papeleria"] = papeleria;
                Session["id_archi"] = null;
                Session["no_redirect"] = null;
                Session["idc_tarea_historial"] = null;
                Session["integrante_tarea"] = null;
                row_vbno.Visible = false;
                Session["idc_puesto_tarea"] = null;
                btnTerminar.Visible = false;
                btnTerminarVBNO.Visible = false;
                btnCancelarTodo.Visible = false;
            }
            //si es quien asigno la tarea
            if (!IsPostBack && Request.QueryString["idc_tarea"] != null && Request.QueryString["termina"] != null)
            {
                Session["fecha"] = null;
                Session["tabla_movimientos"] = null;
                Session["fecha_termi_mov"] = null;
                panel_captura_fecha.Visible = false;
                btnTerminarVBNO.Visible = false;
                btnGuardar.Visible = true;
                nueva.Visible = false;
                btnCancelarTodo.Visible = true;
                file_guardar.Visible = true;
                row_desccambio.Visible = false;
                lnkCambiarFechaF.Visible = true;
                CargarTareas(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"])));
                url_back = "tareas_detalles.aspx?termina=1&idc_tarea=" + Request.QueryString["idc_tarea"];
            }
            //si es quien realiza la tarea
            if (!IsPostBack && Request.QueryString["idc_tarea"] != null && Request.QueryString["acepta"] != null)
            {
                Session["Back_Page"] = null;
                Session["fecha"] = null;
                Session["tabla_movimientos"] = null;
                panel_captura_fecha.Visible = true;
                btnTerminar.Visible = true;
                btnGuardar.Visible = true;
                nueva.Visible = true;
                file_guardar.Visible = true;
                row_desccambio.Visible = false;
                CargarTareas(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"])));
                Session["idc_tarea_hist"] = 0;
                url_back = "tareas_detalles.aspx?acepta=1&idc_tarea=" + Request.QueryString["idc_tarea"];
                panel_avance.Visible = true;
            }
            if (!IsPostBack && Request.QueryString["termina"] == null && Request.QueryString["acepta"] == null)
            {
                Session["Back_Page"] = null;
                Session["fecha"] = null;
                Session["tabla_movimientos"] = null;
                CargarTareas(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"])));
                btnCancelar.Visible = false;
                lnkGO.Visible = false;
                nueva.Visible = false;
            }
            //si no es integrante, no es tarea automatica, la tarea esta terminada
            if (Convert.ToBoolean(Session["integrante_tarea"]) == false || Convert.ToBoolean(Session["tarea_auto"]) == false || Convert.ToBoolean(Session["tarea_terminada"]) == true)
            {
                btnTerminar.Visible = false;
                btnTerminarVBNO.Visible = false;
                btnCancelarTodo.Visible = false;
            }
            if (Convert.ToBoolean(Session["tarea_terminada"]) == true)
            {
                Alert.ShowAlertInfo("Esta Tarea fue Cancelada o Terminada. \n NO PODRA REALIZAR NINGUN CAMBIO.", "Mensaje del Sistema", this);
            }
        }

        /// <summary>
        /// Carga las tareas pendientes
        /// </summary>
        private void CargarTareas(int idc_tarea)
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
                entidad.Pidc_tarea = idc_tarea;
                DataSet ds = componente.CargarTareas(entidad);
                Session["integrante_tarea"] = IntegrantedeTarea(Convert.ToInt32(ds.Tables[0].Rows[0]["idc_puesto"]), Convert.ToInt32(ds.Tables[0].Rows[0]["idc_puesto_asigna"]));
                int id_proceso = Convert.ToInt32(ds.Tables[0].Rows[0]["id_proceso"]);
                Session["id_proceso"] = Convert.ToInt32(ds.Tables[0].Rows[0]["id_proceso"]);
                Session["tarea_auto"] = id_proceso == 0 ? true : false;
                Session["tarea_terminada"] = Convert.ToBoolean(ds.Tables[0].Rows[0]["terminado"]);
                lbltitle.Visible = id_proceso == 0 ? false : true;
                lblprogress.Text = ds.Tables[0].Rows[0]["avance"].ToString() + "% de avance.";
                ScriptManager.RegisterStartupScript(this, GetType(), "DsssswwwE", "AsignaProgress('" + ds.Tables[0].Rows[0]["avance"].ToString() + "%');", true);
                txtavance.Text = "0";
                txtdescripcion.Text = ds.Tables[0].Rows[0]["descripcion"].ToString();
                txtfecha_solicompromiso.Text = ds.Tables[0].Rows[0]["fecha_compromiso"].ToString();
                txtpuesto.Text = ds.Tables[0].Rows[0]["empleado"].ToString() + " || " + ds.Tables[0].Rows[0]["puesto"].ToString();
                txtpuesto_asigna.Text = ds.Tables[0].Rows[0]["empleado_asigna"].ToString() + " || " + ds.Tables[0].Rows[0]["puesto_asigna"].ToString();
                repeat_mis_tareas_asignadas.DataSource = ds.Tables[1];
                repeat_mis_tareas_asignadas.DataBind();
                no_apen.Visible = ds.Tables[1].Rows.Count == 0 ? true : false;
                repe_archivos.DataSource = ds.Tables[2];
                repe_archivos.DataBind();
                noarchivos.Visible = ds.Tables[2].Rows.Count == 0 ? true : false;
                Session["fecha"] = Convert.ToDateTime(ds.Tables[0].Rows[0]["fecha_sinformato"]);
                Session["idc_puesto_tarea"] = Convert.ToInt16(ds.Tables[0].Rows[0]["idc_puesto"]);
                repeat_movimiento.DataSource = ds.Tables[3];
                repeat_movimiento.DataBind();
                Session["tarea_sin_f"] = Convert.ToDateTime(ds.Tables[0].Rows[0]["fecha_sinformato"]);
                hmov.Visible = ds.Tables[3].Rows.Count == 0 ? true : false;
                Session["tabla_movimientos"] = ds.Tables[3];
                repeat_proovedore_info.DataSource = ds.Tables[4];
                repeat_proovedore_info.DataBind();
                proveedores.Visible = ds.Tables[4].Rows.Count == 0 ? false : true;
                gridPapeleria.DataSource = ds.Tables[2];
                gridPapeleria.DataBind();
                repeat_proovedores.DataSource = componente.CargarProveedores(entidad);
                repeat_proovedores.DataBind();
                if (Request.QueryString["acepta"] != null)
                {
                    col_camfechacom.Visible = ds.Tables[4].Rows.Count == 0 ? false : true;
                    txtfecha_compromiso_externo.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private bool IntegrantedeTarea(int idc_puesto, int idc_puesto_asigna)
        {
            bool ret = false;
            int idc_puestousuario = Convert.ToInt32(Session["sidc_puesto_login"]);
            //puesto que asigna la tarea
            if (idc_puestousuario == idc_puesto_asigna)
            {
                panel_captura_fecha.Visible = true;
                btnTerminar.Visible = false;
                btnGuardar.Visible = true;
                nueva.Visible = true;
                file_guardar.Visible = true;
                row_desccambio.Visible = false;
                btnCancelarTodo.Visible = true;
                lnkCambiarFechaF.Visible = true;
                ret = true;
            }
            //puesto que hace la tarea
            if (idc_puestousuario == idc_puesto)
            {
                btnTerminar.Visible = true;
                panel_captura_fecha.Visible = true;
                btnTerminar.Visible = true;
                btnGuardar.Visible = true;
                nueva.Visible = true;
                btnCancelarTodo.Visible = false;
                lnkCambiarFechaF.Visible = false;
                ret = true;
            }
            //puesto que hace la tarea pero el se la asigna
            if (idc_puestousuario == idc_puesto && idc_puestousuario == idc_puesto_asigna)
            {
                btnTerminar.Visible = true;
                panel_captura_fecha.Visible = true;
                btnTerminar.Visible = true;
                btnGuardar.Visible = true;
                nueva.Visible = true;
                btnCancelarTodo.Visible = true;
                lnkCambiarFechaF.Visible = true;
                ret = true;
            }
            return ret;
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

        protected void lnkGuardarPape_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["integrante_tarea"]) == false)
            {
                Alert.ShowAlertError("No puede agregar comentarios ni Archivos, debido a que usted no esta relacionado a la tarea", this);
            }
            else if (Convert.ToBoolean(Session["tarea_terminada"]) == true)
            {
                Alert.ShowAlertError("No puede agregar comentarios ni Archivos, debido a que esta tarea fue TERMINADA O CANCELADA", this);
            }
            else if (txtNombreArchivo.Text == "")
            {
                Alert.ShowAlertError("Ingrese un comentario.", this);
            }
            else
            {
                string id_archi = "0";
                id_archi = Session["id_archivo"] != null ? (string)Session["id_archivo"] : "0";
                bool archivo = fupPapeleria.HasFile ? true : false;
                try
                {
                    TareasENT entidad = new TareasENT();
                    TareasCOM componente = new TareasCOM();
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    entidad.Ptipo = "A";
                    entidad.Pdescripcion = txtNombreArchivo.Text.ToUpper();
                    entidad.Pidc_tarea = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"]));
                    entidad.Pidc_tarea_h = Session["id_archivo"] != null ? Convert.ToInt32(Session["id_archivo"]) : 0;
                    entidad.Pextension = archivo == true ? Path.GetExtension(fupPapeleria.FileName) : "";
                    entidad.Parchivo = archivo;
                    DataSet ds = new DataSet();
                    string vmensaje = "";
                    ds = componente.AgregarComentario(entidad);
                    vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (vmensaje == "")
                    {
                        if (archivo == true)
                        {
                            bool pape = funciones.UploadFile(fupPapeleria, ds.Tables[1].Rows[0]["ruta_destino"].ToString(), this.Page);
                            if (pape == true)
                            {
                                string url = ds.Tables[0].Rows[0]["url"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "noti533sededW3", "SendSlack('" + url + "');", true);
                                Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "3000", "Comentario Agregado Correctamente", this);
                                fupPapeleria.Visible = true;
                                Session["id_archivo"] = null;
                            }
                        }
                        else
                        {
                            string url = ds.Tables[0].Rows[0]["url"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "noti533sededW3", "SendSlack('" + url + "');", true);
                            Alert.ShowAlert("Comentario Agregado Correctamente", "Mensaje del sistema", this);
                        }
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje, this);
                    }
                }
                catch (Exception ex)
                {
                    Alert.ShowAlertError(ex.ToString(), this.Page);
                    Global.CreateFileError(ex.ToString(), this);
                }

                CargarTareas(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"])));
                txtNombreArchivo.Text = "";
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
                case "Eliminar":
                    try
                    {
                        TareasENT entidad = new TareasENT();
                        TareasCOM componente = new TareasCOM();
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        entidad.Ptipo = "B";
                        entidad.Pdescripcion = txtNombreArchivo.Text.ToUpper();
                        entidad.Pidc_tarea = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"]));
                        entidad.Pidc_tarea_h = Convert.ToInt32(id_archi);
                        entidad.Pextension = archivo == true ? Path.GetExtension(fupPapeleria.FileName) : "";
                        entidad.Parchivo = archivo;
                        DataSet ds = new DataSet();
                        string vmensaje = "";
                        ds = componente.AgregarComentario(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowAlert("Comentario Eliminado Correctamente", "Mensaje del sistema", this);
                            Session["id_archivo"] = null;
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                    }
                    catch (Exception EX)
                    {
                        Alert.ShowAlertError(EX.ToString(), this);
                    }

                    CargarTareas(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"])));
                    break;

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

                case "Editar":
                    txtNombreArchivo.Text = descripcion;
                    break;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + gridPapeleria.ClientID.ToString() + "');", true);
        }

        private string CadenaProveedores()
        {
            string cadena = "";
            foreach (RepeaterItem item in repeat_proovedores.Items)
            {
                LinkButton lnk = (LinkButton)item.FindControl("lnk_provee");
                //si esta seleccionado
                if (lnk.CssClass == "btn btn-success btn-block")
                {
                    cadena = cadena + lnk.CommandName + ";" + txtcomentarios_proo.Text.ToUpper() + ";";
                }
            }
            return cadena;
        }

        private int TotalCadenaProveedores()
        {
            int cadena = 0;
            foreach (RepeaterItem item in repeat_proovedores.Items)
            {
                LinkButton lnk = (LinkButton)item.FindControl("lnk_provee");
                //si esta seleccionado
                if (lnk.CssClass == "btn btn-success btn-block")
                {
                    cadena = cadena + 1;
                }
            }
            return cadena;
        }

        protected void lnk_provee_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            lnk.CssClass = lnk.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
        }

        protected void lnkmistarea_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string value = funciones.deTextoa64(lnk.CommandName.ToString());
            Response.Redirect("tareas_detalles.aspx?termina=1&idc_tarea=" + value);
        }

        protected void lnkmistarea_asig_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string ruta = lnk.CommandName.ToString();
            string name = lnk.CommandArgument.ToString();
            Download(ruta, name);
        }

        protected void btnTerminarVBNO_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMesededessssdesage", "ModalClose();", true);
            Session["Caso_Confirmacion"] = "Reasignar";
            Session["tipo_conf"] = "D";
            row_desccambio.Visible = true;
            txtfecha_pasada.Visible = true;
            DateTime DT = (DateTime)Session["fecha"];
            txtfecha_pasada.Text = DT.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Se Reasignara la Tarea a " + txtpuesto.Text + ",'modal fade modal-info');", true);
        }

        protected void btncorrectvbno_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.CssClass = btn.CssClass == "btn btn-success btn-block" ? "btn btn-default btn-block" : "btn btn-success btn-block";
            if (btncorrectvbno.CssClass == "btn btn-success btn-block")
            {
                btnincorrectovbno.CssClass = "btn btn-default btn-block";
            }
        }

        protected void btnincorrectovbno_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.CssClass = btn.CssClass == "btn btn-success btn-block" ? "btn btn-default btn-block" : "btn btn-success btn-block";
            if (btnincorrectovbno.CssClass == "btn btn-success btn-block")
            {
                btncorrectvbno.CssClass = "btn btn-default btn-block";
            }
        }

        protected void lnksolicitar_cam_Click(object sender, EventArgs e)
        {
            if (txtnueva_fecha.Text == "")
            {
                Alert.ShowAlertError("Ingrese la fecha en el formato establecido.", this);
            }
            else
            {
                bool error = false;
                DateTime caption = Convert.ToDateTime(txtnueva_fecha.Text.Replace('T', ' '));
                if (caption < DateTime.Now)
                {
                    error = true;
                    Alert.ShowAlertError("No puede estipular una fecha menor a hoy.", this);
                    txtnueva_fecha.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                }
                if (caption <= Convert.ToDateTime(Session["fecha"]))
                {
                    error = true;
                    Alert.ShowAlertError("No puede estipular una fecha menor o igual a la original.", this);
                    txtnueva_fecha.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                }

                if (error == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMesededessssdesage", "ModalClose();", true);
                    Session["Caso_Confirmacion"] = "Fecha Extra";
                    Session["tipo_conf"] = "K";
                    row_desccambio.Visible = true;
                    txtfecha_pasada.Visible = false;

                    bool vmensaje = false;
                    try
                    {
                        TareasENT entidad = new TareasENT();
                        TareasCOM componente = new TareasCOM();
                        entidad.Pfecha = txtnueva_fecha.Text == "" ? ((DateTime)Session["fecha"]) : Convert.ToDateTime(txtnueva_fecha.Text);
                        entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
                        DataSet ds = new DataSet();
                        ds = componente.ValidarFechas(entidad);
                        vmensaje = Convert.ToBoolean(ds.Tables[0].Rows[0]["fechas_pendientes"]);
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.ToString(), this);
                    }
                    if (vmensaje == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Ha sobrepasado la fecha compromiso " + txtfecha_solicompromiso.Text + ", se capturara una nueva fecha de compromiso.  Pero tiene tareas con una fecha delante de la estipulada, a estas tareas se les agregara un intervalo de tiempo. ','modal fade modal-info');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Ha sobrepasado la fecha compromiso " + txtfecha_solicompromiso.Text + ", se capturara una nueva fecha de compromiso.','modal fade modal-info');", true);
                    }
                }
            }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMesededessssdesage", "ModalClose();", true);
            Session["Caso_Confirmacion"] = "Terminar";
            Session["tipo_conf"] = "T";
            row_desccambio.Visible = true;
            txtfecha_pasada.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Se solicitara el Visto Bueno de la tarea, Desea Continuar?','modal fade modal-info');", true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMesededessssdesage", "ModalClose();", true);
            string tipo = (string)Session["tipo_movimiento"];
            string mensaje = "";
            if (Request.QueryString["termina"] != null)
            {
                if (tipo == "T")
                {
                    mensaje = "Esta a punto de rechazar la terminacion de la tarea. La Tarea con Fecha estipulada para el dia " + txtfecha_solicompromiso.Text + " fue terminada el dia " + lbldinamico.Text + ". Debe Ingresar un motivo para el cambio.";
                }
                if (tipo == "C")
                {
                    mensaje = "Esta a punto de rechazar LA SOLICITUD DE CAMBIO DE FECHA, Desea Continuar? Debe Ingresar un motivo para el cambio";
                }
            }
            if (Request.QueryString["acepta"] != null)
            {
                if (tipo == "N")
                {
                    mensaje = "Esta a punto de rechazar la tarea con fecha de compromiso " + txtfecha_solicompromiso.Text + ", Desea Continuar? Debe Ingresar un motivo para el cambio";
                }
            }
            Session["Caso_Confirmacion"] = "Rechazar";
            Session["tipo_conf"] = "R";
            row_desccambio.Visible = true;
            txtfecha_pasada.Visible = false;
            row_desccambio.Visible = true;
            txtfecha_pasada.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessassssge", "disbalebutton();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','" + mensaje + "','modal fade modal-info');", true);
        }

        protected void btnCancelarTodo_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            Session["tipo_conf"] = "Q";
            row_desccambio.Visible = true;
            btnmalcancelada.CssClass = "btn btn-default btn-block";
            canceladas.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Esta a punto de Cancelar el proceso de esta tarea. Debe Ingresar un motivo para el cambio.','modal fade modal-info');", true);
        }

        protected void btnLeida_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["idc_tarea_hist"]) == 0 && Request.QueryString["idc_tarea_hist"] == null)
            {
                Alert.ShowAlertInfo("No hay Movimiento que Marcar como Leido", "Mensaje del Sistema", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Confirma Leida";
                Session["tipo_conf"] = "L";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Esta accion solo marcara como leido el cambio, Desea continuar?','modal fade modal-info');", true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            CONETNIDO.Visible = true;
            bool error = false;
            DateTime old_fecha = Convert.ToDateTime(Session["fecha"]);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMesededessssdesage", "ModalClose();", true);
            if (Request.QueryString["idc_tarea"] != null && Request.QueryString["termina"] != null)
            {
                Session["redirect"] = "tareas_detalles.aspx?termina=1&idc_tarea=" + Request.QueryString["idc_tarea"];
            }
            if (Request.QueryString["idc_tarea"] != null && Request.QueryString["acepta"] != null)
            {
                Session["redirect"] = "tareas_detalles.aspx?acepta=1&idc_tarea=" + Request.QueryString["idc_tarea"];
            }
            if (panel_captura_fecha.Visible == true)
            {
                if (txtnueva_fecha.Text != "")
                {
                    DateTime caption = Convert.ToDateTime(txtnueva_fecha.Text.Replace('T', ' '));
                    if (caption < DateTime.Now)
                    {
                        Alert.ShowAlertError("No puede estipular una fecha menor a hoy.", this);
                        txtnueva_fecha.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                        error = true;
                    }
                    if (caption <= Convert.ToDateTime(Session["fecha"]))
                    {
                        Alert.ShowAlertError("No puede estipular una fecha menor o igual a la original.", this);
                        txtnueva_fecha.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                        error = true;
                    }
                    if (funciones.FechaCorrecta(Convert.ToDateTime(txtnueva_fecha.Text.Replace('T', ' '))) == false)
                    {
                        Alert.ShowAlertError("La FECHA COMPROMISO de estar dentro de un horario laboral valido.", this);
                        txtnueva_fecha.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                        error = true;
                    }
                }
            }
            if ((string)Session["Caso_Confirmacion"] == "Fecha Extra")
            {
                //aqui me quede
                if (txtfecha_pasada.Text == "")
                {
                    txtfecha_pasada.Text = DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                    Alert.ShowAlertError("Ingrese la fecha en el formato establecido.", this);
                    error = true;
                }
                else
                {
                    DateTime caption = Convert.ToDateTime(txtfecha_pasada.Text.Replace('T', ' '));
                    if (caption < DateTime.Now)
                    {
                        txtfecha_pasada.Text = DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                        Alert.ShowAlertError("No puede estipular una fecha menor a hoy..", this);
                        error = true;
                    }
                }
            }

            bool vmensaje = false;
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pfecha = txtnueva_fecha.Text == "" ? ((DateTime)Session["fecha"]) : Convert.ToDateTime(txtnueva_fecha.Text);
                entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
                DataSet ds = new DataSet();
                ds = componente.ValidarFechas(entidad);
                vmensaje = Convert.ToBoolean(ds.Tables[0].Rows[0]["fechas_pendientes"]);
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
            //lalalele
            //CASOS PARA QUIEN REALIZARA LA TAREA
            if (old_fecha < DateTime.Now && Request.QueryString["acepta"] != null && txtnueva_fecha.Text == "")
            {
                error = true;
                Alert.ShowAlertError("No puede aceptar la fecha estipulada por que se sobrepaso el limite. Cambie la fecha y realize una solicitud de cambio.", this);
            }

            if (txtnueva_fecha.Text == "" && Request.QueryString["acepta"] != null && error == false)
            {
                Session["Caso_Confirmacion"] = "Confirma Fecha Igual";
                Session["tipo_conf"] = "A";

                if (vmensaje == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta aceptando la fecha estipulada " + txtfecha_solicompromiso.Text + " para realizar la tarea. Esta seguro de continuar?','modal fade modal-info');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessassssge", "disbalebutton();", true);
                    row_desccambio.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta aceptando la fecha estipulada " + txtfecha_solicompromiso.Text + " para realizar la tarea. Esta seguro de continuar?','modal fade modal-info');", true);
                }
            }
            if (txtnueva_fecha.Text != "" && error == false && Request.QueryString["acepta"] != null)
            {
                Session["Caso_Confirmacion"] = "Confirma Fecha";
                Session["tipo_conf"] = "C";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessassssge", "disbalebutton();", true);
                row_desccambio.Visible = true;
                if (vmensaje == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta cambiando la fecha estipulada de " + txtfecha_solicompromiso.Text + " a la nueva fecha " + Convert.ToDateTime(txtnueva_fecha.Text).ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")) + ". La solicitud tendra que ser confirmada por " + txtpuesto_asigna.Text + ". ','modal fade modal-info');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta cambiando la fecha estipulada de " + txtfecha_solicompromiso.Text + " a la nueva fecha" + Convert.ToDateTime(txtnueva_fecha.Text).ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")) + ".Tambien tiene tareas con una fecha delante de la estipulada, a estas tareas se les agregara un intervalo de tiempo. ','modal fade modal-info');", true);
                }
            }

            //CASOS PARA QUIEN SOLICITA TAREA
            if (Request.QueryString["termina"] != null)
            {
                if ((string)Session["Caso_Confirmacion"] == "Visto Bueno")
                {
                    CONETNIDO.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMesededessssdesage", "ModalClose();", true);
                    Session["Caso_Confirmacion"] = "Visto Bueno";
                    Session["tipo_conf"] = "B";
                    row_vbno.Visible = true;
                    btnfecha_est.Text = "Estipulada: " + txtfecha_solicompromiso.Text;
                    btnfecha_term.Text = lbldinamico.Text;
                    DateTime TIMEST = Convert.ToDateTime(Session["tarea_sin_f"]);
                    if (TIMEST < Convert.ToDateTime(Session["fecha_termi_mov"]))
                    {
                        btnfecha_term.CssClass = "btn btn-danger btn-block";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Se Dara el Visto Bueno para la Terminacion de la tarea. Puede Ingresar una descripcion o comentarios','','modal fade modal-danger');", true);
                    }
                    else
                    {
                        btnfecha_term.CssClass = "btn btn-success btn-block";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Se Dara el Visto Bueno para la Terminacion de la tarea. Puede Ingresar una descripcion o comentarios.','','modal fade modal-success');", true);
                    }
                }
                else
                {
                    Session["Caso_Confirmacion"] = "Confirma Solicitante";
                    Session["tipo_conf"] = "A";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Esta Aceptando el cambio en la tarea, Desea continuar?','modal fade modal-info');", true);
                }
            }
        }

        protected void lnkGO_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["integrante_tarea"]) == false)
            {
                Alert.ShowAlertError("No puede anidar Tareas debido a que usted no esta involucrado en esta tarea", this);
            }
            else if (Convert.ToBoolean(Session["tarea_terminada"]) == true)
            {
                Alert.ShowAlertError("No puede anidar Tareas , debido a que esta tarea fue TERMINADA O CANCELADA", this);
            }
            else if (Convert.ToBoolean(Session["tarea_auto"]) == false)
            {
                Alert.ShowAlertError("No puede anidar Tareas debido a que es una tarea automatica.", this);
            }
            else
            {
                Session["Back_Page"] = "tareas_detalles.aspx?acepta=1&idc_tarea=" + Request.QueryString["idc_tarea"];
                Response.Redirect("tareas_captura.aspx?idc_tarea=" + Request.QueryString["idc_tarea"]);
            }
        }

        protected void txtnueva_fecha_TextChanged(object sender, EventArgs e)
        {
            if (txtnueva_fecha.Text == "")
            {
                Alert.ShowAlertError("Ingrese la fecha en el formato establecido.", this);
            }
            else
            {
                DateTime caption = Convert.ToDateTime(txtnueva_fecha.Text.Replace('T', ' '));
                if (caption < DateTime.Now)
                {
                    Alert.ShowAlertError("No puede estipular una fecha menor a hoy.", this);
                    txtnueva_fecha.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                }
                if (caption <= Convert.ToDateTime(Session["fecha"]))
                {
                    Alert.ShowAlertError("No puede estipular una fecha menor o igual a la original.", this);
                    txtnueva_fecha.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                }
            }
        }

        protected void lnkarchi_Click(object sender, EventArgs e)
        {
            row_vbno.Visible = false;
            LinkButton lnk = (LinkButton)sender;
            int idc_hist = Convert.ToInt32(lnk.CommandArgument.ToString());
            Session["idc_tarea_historial"] = idc_hist;
            String TIPO = lnk.CommandName.ToString();
            Session["tipo_movimiento"] = TIPO;
            int idc_usuario = Convert.ToInt32(lnk.Attributes["idc_usuario"].ToString());
            bool leido = Convert.ToBoolean(lnk.Attributes["leido"]);

            CargarTareas(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"])));
            DataTable dt = (DataTable)Session["tabla_movimientos"];
            foreach (DataRow row in dt.Rows)
            {
                int idc_tarea_historial = Convert.ToInt32(row["idc_tarea_historial"]);
                if (idc_hist == idc_tarea_historial)
                {
                    lbldes.Text = row["descripcion"].ToString();
                    lblempleado.Text = row["empleado"].ToString();
                    lblfecha.Text = txtnueva_fecha.Text == "" ? row["fecha_compromiso"].ToString() : Convert.ToDateTime(txtnueva_fecha.Text).ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX"));
                    lblfecha_original.Text = row["fecha_original"].ToString();
                    lbltipo.Text = row["tipo_completo"].ToString();
                    lblpuesto.Text = row["puesto"].ToString();
                    lbldinamico.Text = row["columna_dinamica"].ToString();
                    lbldinamico.Visible = row["columna_dinamica"].ToString() == "" ? false : true;
                    ldinamico.Visible = row["columna_dinamica"].ToString() == "" ? false : true;
                    //  Session["fecha"] = Convert.ToDateTime(row["fecha_sin_f"]);
                    Session["fecha_termi_mov"] = Convert.ToDateTime(row["fecha_terminado"]);
                    DateTime DT = Convert.ToDateTime(Session["fecha"]);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMesededessssdesage", "ModalClose();", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMesedededesage", "ModalMov('Mensaje del Sistema');", true);
                    break;
                }
            }
            if (idc_usuario == Convert.ToInt32(Session["sidc_usuario"]) || leido == true)
            {
                BTNCANCELARGUARDAR.Visible = false;
                btnCancelar.Visible = false;
                btnGuardar.Visible = false;
            }
            panel_captura_fecha.Visible = false;
            upda_proovc.Visible = false;
            if (TIPO == "N" || TIPO == "R" || TIPO == "G" && Request.QueryString["acepta"] != null)
            {
                panel_captura_fecha.Visible = true;
                upda_proovc.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 355);
                upda_proovc.Visible = repeat_proovedores.Items.Count == 0 ? false : true;
                txtcomentarios_proo.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 355);
                txtcomentarios_proo.Visible = repeat_proovedores.Items.Count == 0 ? false : true;
            }

            if (TIPO == "A" && Request.QueryString["acepta"] != null && Convert.ToBoolean(Session["integrante_tarea"]) == true && idc_usuario != Convert.ToInt32(Session["sidc_usuario"]))
            {
                try
                {
                    TareasENT entidad = new TareasENT();
                    TareasCOM componente = new TareasCOM();
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    entidad.Ptipo = "L";
                    entidad.Pfecha = txtnueva_fecha.Text == "" ? ((DateTime)Session["fecha"]) : Convert.ToDateTime(txtnueva_fecha.Text);
                    entidad.Pdescripcion = txtcambio_desc.Text;
                    entidad.Pidc_tarea = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"]));
                    DataSet ds = new DataSet();
                    string vmensaje = "";
                    entidad.Pidc_tarea_h = idc_hist;
                    ds = componente.AgregarMovimiento(entidad);
                    vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (vmensaje != "")
                    {
                        Alert.ShowAlertError(vmensaje, this);
                    }
                }
                catch (Exception ex)
                {
                    Alert.ShowAlertError(ex.ToString(), this.Page);
                    Global.CreateFileError(ex.ToString(), this);
                }
            }

            if (TIPO == "A")
            {
                BTNCANCELARGUARDAR.Visible = false;
                btnCancelar.Visible = false;
                btnGuardar.Visible = false;
            }
            if (TIPO == "R" && idc_usuario != Convert.ToInt32(Session["sidc_usuario"]) && Request.QueryString["termina"] != null)
            {
                BTNCANCELARGUARDAR.Visible = true;
                btnCancelar.Visible = false;
                btnGuardar.Visible = false;
            }
            if (TIPO == "G" && idc_usuario != Convert.ToInt32(Session["sidc_usuario"]) && Request.QueryString["termina"] != null)
            {
                BTNCANCELARGUARDAR.Visible = false;
                btnCancelar.Visible = false;
                btnGuardar.Visible = false;
            }
            if (TIPO == "G" && idc_usuario == Convert.ToInt32(Session["sidc_usuario"]) && Request.QueryString["acepta"] != null)
            {
                BTNCANCELARGUARDAR.Visible = false;
                btnCancelar.Visible = true;
                btnGuardar.Visible = true;
            }
            if (TIPO == "T" && idc_usuario != Convert.ToInt32(Session["sidc_usuario"]))
            {
                btnCancelar.Visible = true;
                btnGuardar.Visible = true;
                Session["Caso_Confirmacion"] = "Visto Bueno";
                Session["tipo_conf"] = "B";
            }

            if (Convert.ToBoolean(Session["integrante_tarea"]) == false || Convert.ToBoolean(Session["tarea_auto"]) == false || Convert.ToBoolean(Session["tarea_terminada"]) == true)
            {
                btnCancelar.Visible = false;
                btnGuardar.Visible = false;
                BTNCANCELARGUARDAR.Visible = false;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Ptipo = (string)Session["tipo_conf"];
                int msec = 0;
                int second = 0;
                if (txtnueva_fecha.Text != "")
                {
                    msec = Convert.ToDateTime(txtnueva_fecha.Text).Millisecond;
                    second = Convert.ToDateTime(txtnueva_fecha.Text).Second;
                }
                entidad.Pfecha = txtnueva_fecha.Text == "" ? ((DateTime)Session["fecha"]) : (Convert.ToDateTime(txtnueva_fecha.Text).AddMilliseconds(-msec)).AddSeconds(-second);
                entidad.Pdescripcion = txtcambio_desc.Text;
                entidad.Pidc_tarea = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"]));
                entidad.Pcadena_pro = CadenaProveedores();
                entidad.Ptotal_cadena_pro = TotalCadenaProveedores();
                DataSet ds = new DataSet();
                string vmensaje = "";

                string caso = (string)Session["Caso_Confirmacion"];
                switch (caso)
                {
                    case "Confirma Fecha":
                        entidad.Pidc_tarea_h = Convert.ToInt32(Session["idc_tarea_historial"]);
                        ds = componente.AgregarMovimiento(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Confirma Fecha Igual":

                        entidad.Pidc_tarea_h = Convert.ToInt32(Session["idc_tarea_historial"]);
                        ds = componente.AgregarMovimiento(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Confirma Solicitante":
                        entidad.Pidc_tarea_h = Convert.ToInt32(Session["idc_tarea_historial"]);

                        ds = componente.AgregarMovimiento(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Rechazar":
                        entidad.Pidc_tarea_h = Convert.ToInt32(Session["idc_tarea_historial"]);
                        ds = componente.AgregarMovimiento(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Confirma Leida":
                        entidad.Pidc_tarea_h = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea_hist"]));
                        ds = componente.AgregarMovimiento(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Terminar":
                        ds = componente.AgregarMovimiento(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Visto Bueno":
                        if (btnincorrectovbno.CssClass == "btn btn-default btn-block" && btncorrectvbno.CssClass == "btn btn-default btn-block")
                        {
                            Alert.ShowAlertError("Indique si la tarea fue realizada satisfactoriamente y en tiempo acordado", this);
                        }
                        else
                        {
                            entidad.Pcorrecto = btncorrectvbno.CssClass == "btn btn-success btn-block" ? true : false;
                            entidad.Pcomentarios = txtcomentarios_vbno.Text;
                            ds = componente.AgregarMovimiento(entidad);
                            vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        }
                        break;

                    case "Cancelar":

                        entidad.Pcorrecto = btnmalcancelada.CssClass == "btn btn-success btn-block" ? false : true;
                        if (txtcambio_desc.Text == "") { Alert.ShowAlertError("Debe Ingresar un motivo de la cancelación", this); }
                        else
                        {
                            ds = componente.AgregarMovimiento(entidad);
                            vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        }
                        break;

                    case "Agregar Comentario":
                        ds = componente.AgregarMovimiento(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Fecha Extra":
                        entidad.Pfecha = Convert.ToDateTime(txtnueva_fecha.Text.Replace('T', ' '));
                        ds = componente.AgregarMovimiento(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        row_desccambio.Visible = false;
                        txtfecha_pasada.Visible = false;
                        lnksolicitar_cam.Visible = false;
                        break;

                    case "Reasignar":
                        entidad.Pfecha = Convert.ToDateTime(txtfecha_pasada.Text.Replace('T', ' '));
                        ds = componente.AgregarMovimiento(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        row_desccambio.Visible = false;
                        txtfecha_pasada.Visible = false;
                        lnksolicitar_cam.Visible = false;
                        break;

                    case "Fecha Externo":
                        entidad.Pdescripcion = "SOLICITUD PERMITIDA DEBIDO A RELACION CON PROOVEDOR EXTERNO." + System.Environment.NewLine + txtobsrfechaexternoi.Text.ToUpper();
                        entidad.Pfecha = Convert.ToDateTime(txtfecha_compromiso_externo.Text.Replace('T', ' '));
                        ds = componente.AgregarMovimiento(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        row_desccambio.Visible = false;
                        txtfecha_pasada.Visible = false;
                        lnksolicitar_cam.Visible = false;
                        break;

                    case "Fecha Cambio Directo":
                        entidad.Pdescripcion = "CAMBIO DE FECHA REALIZADO DIRECTAMENTE POR EL EMPLEADO QUE SOLICITO LA TAREA." + System.Environment.NewLine + txtobsrcfd.Text.ToUpper();
                        entidad.Pfecha = Convert.ToDateTime(txtfechacompromisodirecto.Text.Replace('T', ' '));
                        ds = componente.AgregarMovimiento(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        row_desccambio.Visible = false;
                        txtfecha_pasada.Visible = false;
                        lnksolicitar_cam.Visible = false;
                        break;
                }

                if (vmensaje == "")
                {
                    string url = ds.Tables[0].Rows[0]["url"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "noti533sededW3", "SendSlack('" + url + "');", true);
                    if (Session["redirect"] != null)
                    {
                        Alert.ShowGiftMessage("Estamos guardando los cambios", "Espere un Momento", (string)Session["redirect"], "imagenes/loading.gif", "2000", "El movimiento fue Guardado Correctamente", this);
                    }
                    else
                    {
                        Alert.ShowGiftMessage("Estamos guardando los cambios", "Espere un Momento", "tareas.aspx", "imagenes/loading.gif", "2000", "El movimiento fue Guardado Correctamente", this);
                    }

                    Session["Caso_Confirmacion"] = null;
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void repeat_movimiento_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            LinkButton btn = (LinkButton)e.Item.FindControl("lnkarchi2");
            btn.Attributes.Add("idc_usuario", Convert.ToInt32(DataBinder.Eval(dbr, "idc_usuario")).ToString());
            btn.Attributes.Add("leido", Convert.ToString(DataBinder.Eval(dbr, "leido")));
            int idc_movimiento = Request.QueryString["idc_tarea_movimiento"] == null ? 0 : Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea_movimiento"]));
            int idc_movimientoastcual = Convert.ToInt32(DataBinder.Eval(dbr, "idc_tarea_historial"));
            if (idc_movimiento == idc_movimientoastcual)
            {
                btn.CssClass = "btn btn-default btn-block";
            }
            if (Convert.ToBoolean(DataBinder.Eval(dbr, "leido")))
            {
                btn.CssClass = "btn btn-success btn-block";
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

        protected void lnkavanceadd_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["integrante_tarea"]) == false)
            {
                Alert.ShowAlertError("No puede modificar el avance debido a que usted no esta involucrado en esta tarea", this);
            }
            else if (txtavance.Text == "" || txtavance.Text == "0")
            {
                Alert.ShowAlertError("Ingrese un valor mayor a 0", this);
            }
            else if (Convert.ToBoolean(Session["tarea_terminada"]) == true)
            {
                Alert.ShowAlertError("No puede agregar avances, debido a que esta tarea fue TERMINADA O CANCELADA", this);
            }
            else
            {
                try
                {
                    TareasENT entidad = new TareasENT();
                    TareasCOM componente = new TareasCOM();
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    entidad.Pidc_tarea = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"]));
                    entidad.Pavance = Convert.ToInt32(txtavance.Text);
                    DataSet ds = new DataSet();
                    string vmensaje = "";
                    ds = componente.AgregarAvance(entidad);
                    vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (vmensaje != "")
                    {
                        Alert.ShowAlertError(vmensaje, this);
                    }
                    else
                    {
                        string url = ds.Tables[0].Rows[0]["url"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "noti533sededW3", "SendSlack('" + url + "');", true);
                        Alert.ShowAlert("Avance añadido de forma correcta", "Mensaje del sistema", this);
                        CargarTareas(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"])));
                    }
                }
                catch (Exception ex)
                {
                    Alert.ShowAlertError(ex.ToString(), this.Page);
                    Global.CreateFileError(ex.ToString(), this);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button BTN = (Button)sender;
            string obs = BTN.CommandName;
            if (obs != "")
            {
                Alert.ShowAlertInfo(obs, "Información Acerca del Proveedor", this);
            }
        }

        protected void BTNCANCELARGUARDAR_Click(object sender, EventArgs e)
        {
        }

        protected void lnkcambiof_Click(object sender, EventArgs e)
        {
            if (txtfecha_compromiso_externo.Text != "")
            {
                DateTime caption = Convert.ToDateTime(txtfecha_compromiso_externo.Text.Replace('T', ' '));
                if (caption < DateTime.Now)
                {
                    Alert.ShowAlertError("No puede estipular una fecha menor a hoy.", this);
                    txtfecha_compromiso_externo.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                }
                else if (caption <= Convert.ToDateTime(Session["fecha"]))
                {
                    Alert.ShowAlertError("No puede estipular una fecha menor o igual a la original.", this);
                    txtfecha_compromiso_externo.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                }
                else if (txtobsrfechaexternoi.Text == "")
                {
                    Alert.ShowAlertError("Escriba un Motivo para el cambio.", this);
                }
                else
                {
                    Session["redirect"] = "tareas_detalles.aspx?acepta=1&idc_tarea=" + Request.QueryString["idc_tarea"];
                    Session["Caso_Confirmacion"] = "Fecha Externo";
                    Session["tipo_conf"] = "C";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Esta Solicitando un Cambio de Fecha por un Proveedor Externo, Desea continuar','modal fade modal-info');", true);
                }
            }
        }

        protected void lnkCambiarFechaF_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalCF();", true);
        }

        protected void btnsifecdirecto_Click(object sender, EventArgs e)
        {
            if (txtfechacompromisodirecto.Text != "")
            {
                DateTime caption = Convert.ToDateTime(txtfechacompromisodirecto.Text.Replace('T', ' '));
                if (caption < DateTime.Now)
                {
                    Alert.ShowAlertError("No puede estipular una fecha menor a hoy.", this);
                    txtfechacompromisodirecto.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                }
                else if (caption <= Convert.ToDateTime(Session["fecha"]))
                {
                    Alert.ShowAlertError("No puede estipular una fecha menor o igual a la original.", this);
                    txtfechacompromisodirecto.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                }
                else if (txtobsrcfd.Text == "")
                {
                    Alert.ShowAlertError("Escriba un Motivo para el cambio.", this);
                }
                else if (Convert.ToBoolean(Session["tarea_terminada"]) == true)
                {
                    Alert.ShowAlertError("No puede cambiar la Fecha Compromiso fue TERMINADA O CANCELADA", this);
                }
                else
                {
                    Session["redirect"] = "tareas_detalles.aspx?termina=1&idc_tarea=" + Request.QueryString["idc_tarea"];
                    Session["Caso_Confirmacion"] = "Fecha Cambio Directo";
                    Session["tipo_conf"] = "Z";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Esta Cambiando la Fecha de Compromiso, Desea continuar','modal fade modal-info');", true);
                }
            }
            else
            {
                Alert.ShowAlertError("Escriba una Fecha", this);
            }
        }
    }
}