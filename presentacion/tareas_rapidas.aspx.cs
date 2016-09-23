using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class tareas_rapidas : System.Web.UI.Page
    {
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
                Session["papeleria"] = papeleria;
                CargaPuestos("");
                //iniciamos tabla en session
                //iniciamos textbox de fecha
                txtfecha_solicompromiso.Text = DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
            }
        }

        /// <summary>
        /// Carga Puestos en ComboBox con Filtro
        /// </summary>
        public void CargaPuestos(string filtro)
        {
            try
            {
                Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
                Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
                entidad.Filtro = filtro;
                entidad.Idc_puesto_revisa = Convert.ToInt32(Session["sidc_puesto_login"]);
                DataSet ds = componente.CargaComboDinamicoServiciosRapida(entidad);
                ddlPuesto.DataValueField = "idc_puesto";
                ddlPuesto.DataTextField = "descripcion_puesto_completa";
                ddlPuesto.DataSource = ds.Tables[0];
                ddlPuesto.DataBind();
                //si no hay filtro insertamos una etiqueta inicial
                if (filtro == "")
                {
                    ddlPuesto.Items.Insert(0, new ListItem("Seleccione un Puesto", "0")); //updated code}
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            CargaPuestos(txtpuesto_filtro.Text);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool error = false;          
            if (txtfecha_solicompromiso.Text == "")
            {
                error = true;
                Alert.ShowAlertError("Escriba la fecha en el formato correcto.", this);
                txtfecha_solicompromiso.Text = DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
            }
            if (txtfecha_solicompromiso.Text != "")
            {
                DateTime caption = Convert.ToDateTime(txtfecha_solicompromiso.Text.Replace('T', ' '));
                if (caption < DateTime.Now)
                {
                    error = true;
                    Alert.ShowAlertError("No puede estipular una fecha menor a hoy.", this);
                    txtfecha_solicompromiso.Text = DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                }
            }

            if (txtdescripcion.Text == "")
            {
                error = true;
                Alert.ShowAlertError("Coloque una descripcion para la Tarea", this);
            }
            if (txtfecha_solicompromiso.Text == "")
            {
                error = true;
                Alert.ShowAlertError("Coloque una fecha de compromiso para la Tarea", this);
            }
            if (Convert.ToInt32(ddlPuesto.SelectedValue) == 0)
            {
                error = true;
                Alert.ShowAlertError("Agregre un puesto para la Tarea", this);
            }
          
            if (error == false)
            {
                if (fupPapeleria.HasFile)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(0, 100000);
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/tareas/"));//path local
                    string mensaje = AddPapeleriaToTable(dirInfo + randomNumber.ToString() + fupPapeleria.FileName, Path.GetExtension(fupPapeleria.FileName).ToString(), fupPapeleria.FileName.ToString().ToUpper(), "0");
                    if (mensaje.Equals(string.Empty))
                    {
                        bool pape = funciones.UploadFile(fupPapeleria, dirInfo + randomNumber.ToString() + fupPapeleria.FileName, this.Page);
                        if (pape == true)
                        {
                            Session["Caso_Confirmacion"] = "Guardar";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar esta Tarea. Una vez Guardada NO PODRA SER MODIFICADA?','modal fade modal-info');", true);
                        }
                    }
                    else
                    {
                        Alert.ShowAlertError(mensaje, this);
                    }
                }
                else {
                    Alert.ShowAlertError("Agrege una imagen para la Tarea", this);
                }
              
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("tareas.aspx");
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso = (string)Session["Caso_Confirmacion"];
            switch (caso)
            {
                case "Guardar":
                    try
                    {
                        TareasENT entidad = new TareasENT();
                        TareasCOM componente = new TareasCOM();
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        entidad.Pidc_puesto_asigna = Convert.ToInt32(Session["sidc_puesto_login"]);
                        entidad.Pdescripcion = txtdescripcion.Text.ToUpper();
                        entidad.Pidc_puesto = Convert.ToInt32(ddlPuesto.SelectedValue);
                        int msec = Convert.ToDateTime(txtfecha_solicompromiso.Text).Millisecond;
                        int second = Convert.ToDateTime(txtfecha_solicompromiso.Text).Second;
                        entidad.Pfecha = (Convert.ToDateTime(txtfecha_solicompromiso.Text).AddMilliseconds(-msec)).AddSeconds(-second);
                        entidad.Ptotal_cadena_arch = TotalCadenaArchivos();
                        entidad.Pcadena_arch = CadenaArchivos();
                        DataSet ds;
                        ds = componente.AgregarTarea(entidad);
                        //mensaje de error o abortado
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            string FECHA = ds.Tables[0].Rows[0]["FECHA"].ToString();
                            string url_back = Request.QueryString["idc_tarea"] != null ? (string)Session["Back_Page"] : "tareas.aspx"; Alert.ShowGiftMessage("Estamos procesando la información.", "Espere un Momento", url_back, "imagenes/loading.gif", "1000", "La tarea fue Guardada Correctamente con una Fecha de compromiso para el dia " + FECHA, this);
                            DataTable tabla_archivos = ds.Tables[1];
                            bool correct = true;
                            foreach (DataRow row_archi in tabla_archivos.Rows)
                            {
                                string ruta_det = row_archi["ruta_destino"].ToString();
                                string ruta_origen = row_archi["ruta_origen"].ToString();
                                correct = funciones.CopiarArchivos(ruta_origen, ruta_det, this.Page);
                                if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo " + ruta_origen + "a la ruta " + ruta_det, this); }
                                Alert.ShowGiftMessage("Estamos procesando la cantidad de " + tabla_archivos.Rows.Count.ToString() + " archivo(s) al Servidor.", "Espere un Momento", url_back, "imagenes/loading.gif", "2000", "La tarea fue Guardada Correctamente con una Fecha de compromiso para el dia " + FECHA, this);
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

                    break;
            }
        }

        /// <summary>
        /// Agrega filas tabla de papeleria global
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="nombre"></param>
        public string AddPapeleriaToTable(string ruta, string extension, string descripcion, string id_archi)
        {
            string mensaje = "";
            bool exists = false;
            try
            {
                DataTable papeleria = (DataTable)Session["papeleria"];
                foreach (DataRow check in papeleria.Rows)
                {
                    if (check["ruta"].Equals(ruta) && check["id_archi"].Equals(id_archi))
                    {
                        check["ruta"] = ruta;
                        check["extension"] = extension;
                        check["descripcion"] = descripcion;
                        check["id_archi"] = id_archi;
                        exists = true;
                        break;
                    }
                }
                if (exists == false)
                {
                    DataRow new_row = papeleria.NewRow();
                    new_row["ruta"] = ruta;
                    new_row["extension"] = extension;
                    new_row["descripcion"] = descripcion;
                    new_row["id_archi"] = id_archi;
                    papeleria.Rows.Add(new_row);
                    Session["papeleria"] = papeleria;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }

            return mensaje;
        }

        /// <summary>
        /// Regresa una cadena de los archivos
        /// </summary>
        /// <returns></returns>
        private string CadenaArchivos()
        {
            string cadena = "";
            DataTable papeleria = (DataTable)Session["papeleria"];
            foreach (DataRow check in papeleria.Rows)
            {
                cadena = cadena + check["descripcion"] + ";" + check["extension"] + ";" + check["ruta"] + ";";
            }
            return cadena;
        }

        /// <summary>
        /// Regresa uel total de la cadena de los archivos
        /// </summary>
        /// <returns></returns>
        private int TotalCadenaArchivos()
        {
            return ((DataTable)Session["papeleria"]).Rows.Count;
        }
    }
}