using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class candidatos_preparar_captura : System.Web.UI.Page
    {
        public string ruta = "";
        public bool encurso = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (Request.QueryString["idc_puesto"] == null)
            {
                Response.Redirect("revisones_preparacion.aspx");
            }
            Session["redirect"] = "candidatos_preparar_captura.aspx?idc_puesto=" + Request.QueryString["idc_puesto"].ToString() + "&idc_prepara=" + Request.QueryString["idc_prepara"].ToString();
            lnknuevocandidato.Visible = Request.QueryString["view"] == null ? true : false;
            //bajo valores
            int idc_puesto = 0;
            int idc_usuario = 0;
            int idc_puestobaja = 0;
            int idc_prepara = 0;
            if (Request.QueryString["view"] == null)//si no viene del administrador
            {
                idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_puestobaja = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"].ToString()));
                idc_prepara = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_prepara"].ToString()));
            }
            else
            {
                idc_puesto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto_reclutador"]));
                idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_puestobaja = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"]));
                idc_prepara = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_prepara"]));
                btnCancelar.Visible = false;
                btnGuardar.Visible = false;
                Alert.ShowAlertInfo("Esta pagina esta en modo informativo, NO PODRA guardar la Revisión. Si desea hacerlo, verifique el apartado de notificaciones", "Mensaje del sistema", this);
            }
            if (!Page.IsPostBack)
            {
                //cargo datos a tablas
                DataPrep(idc_puestobaja, idc_prepara, idc_puesto);
                DataTable tabla_archivos = new DataTable();
                tabla_archivos.Columns.Add("ruta");
                tabla_archivos.Columns.Add("descripcion");
                tabla_archivos.Columns.Add("tipo_archi");
                Session["tabla_archivos"] = tabla_archivos;
                btnActualizar.Visible = false;
            }
            lnkcambiarfecha.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"].ToString()), 348);
        }

        /// <summary>
        /// Genera ruta de archivos
        /// </summary>
        public void GenerarRuta(string codigo_imagen)
        {
            var Entidad = new UsuariosE();
            Entidad.Cod_arch = codigo_imagen;
            var Componente = new OrganigramaBL();
            var ds = new DataSet();
            ds = Componente.CargaPath(Entidad);
            if (ds.Tables[0].Rows.Count != 0)
            {
                var tablaGrupos = new DataTable();
                //Paso dataset ala tabla
                tablaGrupos = ds.Tables[0];
                var row = tablaGrupos.Rows[0];
                var carpeta = row["rw_carpeta"].ToString();
                var domn = Request.Url.Host;
                switch (codigo_imagen)
                {
                    case "CANDID":
                        ruta = carpeta;
                        break;
                }
            }
        }

        /// <summary>
        /// Carga los puestos pendientes de preparar
        /// </summary>
        public void DataPrep(int idc_puestosbaja, int idc_prepara, int idc_puesto_reclutador)
        {
            CandidatosENT entidad = new CandidatosENT();
            CandidatosCOM componente = new CandidatosCOM();
            entidad.Pidc_puesto = idc_puestosbaja;
            entidad.Pidc_prepara = idc_prepara;
            entidad.Pidc_puestobaja = idc_puesto_reclutador;
            DataSet ds = componente.CargaPuestos(entidad);
            //verificamos que no exceda el maximo de candidatos permitidos
            DataRow maximo = ds.Tables[0].Rows[0];
            int maxi = Convert.ToInt32(maximo["num_total"]);
            int actual = ds.Tables[1].Rows.Count;
            if (actual >= maxi)
            {
                lnknuevocandidato.Visible = false;
            }
            lblPuesto.Text = ds.Tables[0].Rows[0]["descripcion"].ToString();
            lblfecha_compro.Text = ds.Tables[1].Rows[0]["fecha_compromiso"].ToString();
            DataRow row = ds.Tables[1].Rows[0];
            Session["idc_prepara"] = Convert.ToInt32(row["idc_prepara"].ToString());
            Session["idc_puesto"] = Convert.ToInt32(row["idc_puesto"].ToString());
            Session["data"] = ds.Tables[2];
            DataTable data = (DataTable)Session["data"];
            foreach (DataRow row_data in data.Rows)
            {
                if (Convert.ToInt32(row_data["seleccionado"]) != 0)
                {
                    encurso = true;
                }
            }
            gridCatalogo.DataSource = ds.Tables[2];
            gridCatalogo.DataBind();
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataRow row_n = ds.Tables[0].Rows[0];
                Session["num_total"] = Convert.ToInt32(row_n["num_total"]);
                Session["tabla_candidatos"] = ds.Tables[2];
            }
            else { nocandidatos.Visible = true; }
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        protected void lnkGuardarArchivo_Click(object sender, EventArgs e)
        {
            bool error = false;

            if (txtNombres.Text == string.Empty || txtPaterno.Text == string.Empty) { error = true; Alert.ShowAlertError("Debe colocar el nombre del candidato.", this); }
            if (error != true)
            {
                Session["Caso_Confirmacion"] = "Guardar Archivo";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea guardar este Archivo?');", true);
            }
        }

        protected void gridArchivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridArchivos.DataKeys[index].Values["ruta"].ToString();

            DataTable tabla_archivos = (DataTable)Session["tabla_archivos"];
            foreach (DataRow row in tabla_archivos.Rows)
            {
                if (row["ruta"].ToString().Equals(ruta)) { row.Delete(); break; }
            }
            Session["tabla_archivos"] = tabla_archivos;
            gridArchivos.DataSource = tabla_archivos;
            gridArchivos.DataBind();
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            CandidatosENT entidad = new CandidatosENT();
            CandidatosCOM componente = new CandidatosCOM();
            DataTable tabla_archivos = (DataTable)Session["tabla_archivos"];
            entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
            entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
            entidad.Pusuariopc = funciones.GetUserName();//usuario pc
            GenerarRuta("CANDID");//generamos ruta de subida
            switch (Confirma_a)
            {
                case "Eliminar Candidato":
                    entidad.Pidc_candidato = Convert.ToInt32(Session["idc_pre_empleado"]);
                    entidad.Pidc_prepara = Convert.ToInt32(Session["idc_prepara"]);
                    entidad.Pidc_puestobaja = Convert.ToInt32(Session["idc_puesto"].ToString());
                    try
                    {
                        DataSet ds = componente.BorrarCandidato(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        string mensaje = row["mensaje"].ToString();
                        if (mensaje != "")//no hay errores retornamos true
                        {
                            Alert.ShowAlertError(mensaje, this);
                        }
                        else
                        {//mostramos error
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('El candidato fue Eliminado correctamente.','" + HttpContext.Current.Request.Url.AbsoluteUri + "','success');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.Message, this);
                    }
                    break;

                case "Capacitar":
                    entidad.Pidc_prepara = Convert.ToInt32(Session["idc_prepara"]);
                    entidad.Pidc_candidato = Convert.ToInt32(Session["idc_candidato"].ToString());
                    entidad.Pidc_puestobaja = Convert.ToInt32(Session["idc_puesto"].ToString());
                    try
                    {
                        DataSet ds = componente.TodoPreparado(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        bool listo = Convert.ToBoolean(row["listo"]);
                        bool capacitacion = Convert.ToBoolean(row["capacitacion"]);
                        string mensaje = row["mensaje"].ToString();
                        if (listo == false)//no hay errores retornamos true
                        {
                            Session["Previus"] = HttpContext.Current.Request.Url.AbsoluteUri;
                            Alert.ShowAlertConfirm(mensaje, "No puede continuar", "administrador_preparaciones.aspx", this);
                        }
                        else
                        {
                            Session["Previus"] = HttpContext.Current.Request.Url.AbsoluteUri;
                            Response.Redirect("pre_empleados_captura.aspx?idc_candidato=" + Session["idc_candidato"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.Message, this);
                    }
                    break;

                case "Compromiso":
                    if (txtnueva_fecha.Text == "" || txtmotivo.Text == "")
                    {
                        Alert.ShowAlertError("Llene el formulario de cambio de fecha completamente", this);
                    }
                    else
                    {
                        entidad.Pidc_prepara = Convert.ToInt32(Session["idc_prepara"]);
                        try
                        {
                            entidad.Pfecha = Convert.ToDateTime(txtnueva_fecha.Text);
                            entidad.Pmotivo = txtmotivo.Text.ToUpper();
                            DataSet ds = componente.CambiarFechaCompromiso(entidad);
                            DataRow row = ds.Tables[0].Rows[0];
                            //verificamos que no existan errores

                            string mensaje = row["mensaje"].ToString();
                            if (mensaje == "")
                            {
                                Alert.ShowAlert("Fecha Compromiso Guardada Correctamente", "Mensaje del sistema", this);
                            }
                            else
                            {
                                Alert.ShowAlertError(mensaje, this);
                            }
                            int idc_puestobaja = Convert.ToInt32(Request.QueryString["idc_puesto"].ToString());
                            int idc_prepara = Convert.ToInt32(Request.QueryString["idc_prepara"].ToString());

                            //cargo datos a tablas
                            DataPrep(idc_puestobaja, idc_prepara, Convert.ToInt32(Session["sidc_puesto_login"].ToString()));
                            DataTable tabla_archivoss = new DataTable();
                            tabla_archivoss.Columns.Add("ruta");
                            tabla_archivoss.Columns.Add("descripcion");
                            tabla_archivoss.Columns.Add("tipo_archi");
                            Session["tabla_archivos"] = tabla_archivoss;
                            btnActualizar.Visible = false;
                            txtmotivo.Text = "";
                            txtnueva_fecha.Text = "";
                        }
                        catch (Exception ex)
                        {
                            Alert.ShowAlertError(ex.Message, this);
                        }
                    }

                    break;
            }
            gridCatalogo.DataSource = (DataTable)Session["data"];
            gridCatalogo.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //bajamos los archivos actuales a una tabla temporal
            DataTable tabla_archivos = (DataTable)Session["tabla_archivos"];
            bool error = false;
            if (txtMaterno.Text == string.Empty) { error = true; Alert.ShowAlertError("Ingrese el Apellido Materno del Candidato", this); }
            if (txtPaterno.Text == string.Empty) { error = true; Alert.ShowAlertError("Ingrese el Apellido Paterno del Candidato", this); }
            if (txtNombres.Text == string.Empty) { error = true; Alert.ShowAlertError("Ingrese el Nombre Completo del Candidato", this); }
            if (ExistCurriculum() == false) { error = true; Alert.ShowAlertError("Debe subir el curriculum de " + txtNombres.Text.ToUpper(), this); }
            if (tabla_archivos.Rows.Count == 0) { error = true; Alert.ShowAlertError("Debe Subir el Curriculum como minimo.", this); }
            if (error != true)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar este candidato?');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        /// <summary>
        /// Genera Cadena con archivos
        /// </summary>
        /// <returns></returns>
        public string GeneraCadena()
        {
            string cadena = "";
            DataTable tabla_archi = (DataTable)Session["tabla_archivos"];
            foreach (DataRow row in tabla_archi.Rows)
            {
                cadena = cadena + row["ruta"].ToString() + ";" + row["descripcion"].ToString() + ";" + row["tipo_archi"].ToString() + ";";
            }
            return cadena;
        }

        /// <summary>
        /// Sube archivos a ruta, retorna en forma de Bool si hubo un error
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public bool UploadFile(String ruta)
        {
            try
            {
                FileUpload.PostedFile.SaveAs(ruta);
                return false;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
                return true;
            }
        }

        /// <summary>
        /// Comprueba que no exista el curriculum
        /// </summary>
        /// <returns></returns>
        public bool ExistCurriculum()
        {
            bool exists = false;
            DataTable tabla_archivos = (DataTable)Session["tabla_archivos"];
            foreach (DataRow row in tabla_archivos.Rows)
            {
                if (row["tipo_archi"].ToString().Equals("C"))
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        protected void lnkNuevo_Click(object sender, EventArgs e)
        {
            PanelCatalogo.Visible = false;
            PanelCaptura.Visible = true;
        }

        protected void gridCatalogo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_puestobaja"] = Convert.ToInt32(gridCatalogo.DataKeys[index].Values["idc_puesto"].ToString());
            Session["idc_prepara"] = Convert.ToInt32(gridCatalogo.DataKeys[index].Values["idc_prepara"].ToString());
            Session["idc_pre_empleado"] = Convert.ToInt32(gridCatalogo.DataKeys[index].Values["idc_pre_empleado"].ToString());
            switch (e.CommandName)
            {
                //case "Ver":
                //    btnGuardar.Visible = false;
                //    btnActualizar.Visible = true;
                //    break;

                case "Eliminar":

                    cambiar_fecha.Visible = false;
                    Session["Caso_Confirmacion"] = "Eliminar Candidato";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta seguro de Eliminar este Candidato?');", true);
                    break;

                case "Capacitacion":

                    cambiar_fecha.Visible = false;
                    int num_total = Convert.ToInt32(Session["num_total"]);
                    DataTable table = (DataTable)Session["tabla_candidatos"];

                    if (table.Rows.Count < num_total)
                    {
                        Alert.ShowAlertError("Requiere un minimo de " + num_total.ToString() + " candidatos para realizar esta operación.", this);
                    }
                    else
                    {
                        Session["Caso_Confirmacion"] = "Capacitar";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta seguro de realizar este movimiento? Esto hara que este puesto se bloquee hasta que el candidato pase el tiempo de capacitación.');", true);
                    }
                    break;

                case "Puesto":
                    int idc_prepara = Convert.ToInt32(gridCatalogo.DataKeys[index].Values["idc_prepara"].ToString());
                    int idc_pre_empleado = Convert.ToInt32(gridCatalogo.DataKeys[index].Values["idc_pre_empleado"].ToString());
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), 
                        "window.open('pre_empleados_info.aspx?idc_pre_empleado="+funciones.deTextoa64(idc_pre_empleado.ToString().Trim())+ "&idc_prepara="+funciones.deTextoa64(idc_prepara.ToString().Trim())+"');", true);
                    break;
            }
            gridCatalogo.DataSource = (DataTable)Session["data"];
            gridCatalogo.DataBind();
        }

        public void DataPrep2(int idc_puestosbaja, int idc_prepara, int idc_pre_empleado)
        {
            CandidatosENT entidad = new CandidatosENT();
            CandidatosCOM componente = new CandidatosCOM();
            if (idc_pre_empleado != 0)
            {
                entidad.Pidc_puesto = idc_puestosbaja;
                entidad.Pidc_prepara = idc_prepara;
                entidad.Pidc_pre_empleado = idc_pre_empleado;
                DataSet ds = componente.CargaCandidatos(entidad);
                txtobservaciones2.Text = ds.Tables[1].Rows[0]["observaciones"].ToString();
                gridDetalles.DataSource = ds.Tables[1];
                gridDetalles.DataBind();
                repeat_telefonos.DataSource = ds.Tables[2];
                repeat_telefonos.DataBind();
                repeat_papeleria.DataSource = ds.Tables[3];
                repeat_papeleria.DataBind();
            }
        }

        protected void lnkdownload_Click(object sender, EventArgs e)
        {
            LinkButton lnkdownload = (LinkButton)sender;
            string ruta = lnkdownload.CommandName.ToString();
            string archivo = lnkdownload.CommandArgument.ToString();
            Download(ruta, archivo);
        }

        protected void repeat_papeleria_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            LinkButton lnkdownload = (LinkButton)e.Item.FindControl("lnkdownload");
            string ruta = Convert.ToString(DataBinder.Eval(dbr, "ruta"));
            string archivo = Convert.ToString(DataBinder.Eval(dbr, "archivo"));
            lnkdownload.CommandName = ruta;
            lnkdownload.CommandArgument = archivo;
        }

        /// <summary>
        /// Descarga un archivo
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file_name"></param>
        public void Download(string path, string file_name)
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
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
        }

        protected void gridCatalogo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                lnknuevocandidato.Visible = true;
                if (Convert.ToInt32(rowView["seleccionado"]) != 0)
                {
                    e.Row.BackColor = Color.FromName("#FACC2E");
                    e.Row.ForeColor = Color.FromName("#000000");
                }
                if (encurso == true) { e.Row.Cells[0].Controls.Clear(); e.Row.Cells[1].Controls.Clear(); ExisteProce.Visible = true; lnknuevocandidato.Visible = false; }
            }
        }

        protected void lnknuevocandidato_Click(object sender, EventArgs e)
        {
            Response.Redirect("pre_empleados_captura.aspx?idc_puesto=" + Request.QueryString["idc_puesto"].ToString());
        }

        protected void lnkcambiarfecha_Click(object sender, EventArgs e)
        {
            cambiar_fecha.Visible = true;
            Session["Caso_Confirmacion"] = "Compromiso";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Ingerese la Nueva Fecha Compromiso');", true);
        }

        protected void btnocul_Click(object sender, EventArgs e)
        {
            view.Visible = false;
        }
    }
}