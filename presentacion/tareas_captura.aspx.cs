﻿using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

namespace presentacion
{
    public partial class tareas_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                ViewState["tabla_servicios"] = null;
                CargaPuestos("");
                //iniciamos tabla en session
                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("descripcion");
                papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("extension");
                papeleria.Columns.Add("id_archi");
                Session["papeleria"] = papeleria;
                DataTable puestos = new DataTable();
                puestos.Columns.Add("idc_puesto");
                puestos.Columns.Add("descripcion_puesto_completa");
                Session["puestos_tareas"] = puestos;
                Session["id_archi"] = null;
                //iniciamos textbox de fecha
                txtfecha_solicompromiso.Text = DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                div_puestoasigna.Visible= funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 386);
                CargaPuestosAsigna("");
            }
        }

        public void TareasServicios(int idc_puesto)
        {
            try
            {
                TareasCOM componente = new TareasCOM();
                DataSet ds = componente.sp_tareas_servicios_puestos(idc_puesto);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ViewState["tabla_servicios"] = dt;
                    tareaservicios.Visible = true;
                    ddlservicios.DataTextField = "desc_corta";
                    ddlservicios.DataValueField = "idc_tareaser";
                    ddlservicios.DataSource = dt;
                    ddlservicios.DataBind();
                    ddlservicios.Items.Insert(0, new ListItem("--Ningun Servicio Seleccionado", "0"));
                }
                else
                {
                    ViewState["tabla_servicios"] = null;
                    ddlservicios.Items.Clear();
                    tareaservicios.Visible = false;
                    //txtdescripcion.ReadOnly = false;
                    //txtfecha_solicompromiso.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
        public void TareasServiciosDetalles(int idc_tareaser)
        {
            try
            {
                DataTable dt = ViewState["tabla_servicios"] as DataTable;
                DataView view = dt.DefaultView;
                view.RowFilter = "idc_tareaser = "+idc_tareaser+"";
                DataTable dt2 = view.ToTable();
                if (dt2.Rows.Count > 0)
                {
                    DataRow row = dt2.Rows[0];
                    titleserv.Visible = true;
                    lblhoras_tarea_serv.Text= row["intervalo_tiempo"].ToString();
                    lblobservacionesser.Text = row["observaciones"].ToString();
                    txtdescripcion.Text = row["descripcion"].ToString();
                    txtfecha_solicompromiso.Text = Convert.ToDateTime(row["fecha_compromiso"]).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                    bool editable = Convert.ToBoolean(row["editable"]);
                    //txtdescripcion.ReadOnly = editable == true ? false : true;
                    //txtfecha_solicompromiso.ReadOnly = editable == true ? false : true;
                    txtidc_tareaser.Text = idc_tareaser.ToString().Trim();
                    ScriptManager.RegisterStartupScript(this, GetType(), "DededededE", "Gifts('Estamos Cargando la Tarea');", true);
                }
                else
                {
                    titleserv.Visible = false;
                    txtidc_tareaser.Text = "";

                    lblhoras_tarea_serv.Text = "";
                    lblobservacionesser.Text = "";
                    ddlservicios.Items.Clear();
                    tareaservicios.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
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
                DataSet ds = componente.CargaComboDinamicoServicios(entidad);
                ddlPuesto.DataValueField = "idc_puesto";
                ddlPuesto.DataTextField = "descripcion_puesto_completa";
                ddlPuesto.DataSource = ds.Tables[0];
                ddlPuesto.DataBind();
                //si no hay filtro insertamos una etiqueta inicial
                if (filtro == "")
                {
                    ddlPuesto.Items.Insert(0, new ListItem("Seleccione un Puesto", "0")); //updated code}
                }
                int idc_puesto = ddlPuesto.SelectedValue == "" ? 0 : Convert.ToInt32(ddlPuesto.SelectedValue);
                if (idc_puesto > 0)
                {
                    CargarTareas(idc_puesto);
                    TareasServicios(idc_puesto);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
        public void CargaPuestosAsigna(string filtro)
        {
            try
            {
                Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
                Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
                entidad.Filtro = filtro;
                entidad.Idc_puesto_revisa = Convert.ToInt32(Session["sidc_puesto_login"]);
                DataSet ds = componente.CargaComboDinamico(entidad);
                ddlpuestoasigna.DataValueField = "idc_puesto";
                ddlpuestoasigna.DataTextField = "descripcion_puesto_completa";
                ddlpuestoasigna.DataSource = ds.Tables[0];
                ddlpuestoasigna.DataBind();
                //si no hay filtro insertamos una etiqueta inicial
                if (filtro == "")
                {
                    ddlpuestoasigna.Items.Insert(0, new ListItem("Seleccione un Puesto", "0")); //updated code}
                }               
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
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
                if (!exists)
                {
                    DataRow new_row = papeleria.NewRow();
                    new_row["ruta"] = ruta;
                    new_row["extension"] = extension;
                    new_row["descripcion"] = descripcion;
                    new_row["id_archi"] = id_archi;
                    papeleria.Rows.Add(new_row);
                    gridPapeleria.DataSource = papeleria;
                    gridPapeleria.DataBind();
                    Session["papeleria"] = papeleria;
                    txtNombreArchivo.Text = "";
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
        /// Carga las tareas pendientes
        /// </summary>
        private void CargarTareas(int idc_puesto)
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pidc_puesto = idc_puesto;
                entidad.Pidc_puesto_asigna = Convert.ToInt32(Session["sidc_puesto_login"]);
                DataSet ds = componente.CargarTareasAsigne(entidad);
                repeat_mis_tareas.DataSource = ds.Tables[1];
                repeat_mis_tareas.DataBind();
                no_mias.Visible = ds.Tables[1].Rows.Count == 0 ? true : false;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
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

        protected void lnkmistarea_Click1(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string value = funciones.deTextoa64(lnk.CommandName.ToString());
            Response.Redirect("tareas_detalles.aspx?idc_tarea=" + value);
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            CargaPuestos(txtpuesto_filtro.Text);
        }

        protected void lnkGuardarPape_Click(object sender, EventArgs e)
        {
            bool error = false;

            string id_archi = "0";
            //si hay en session un  id de archivo quiere decir que lo estamos editando
            id_archi = Session["id_archi"] != null ? (string)Session["id_archi"] : "0";
            if (txtNombreArchivo.Text == "") { error = true; Alert.ShowAlertError("Debe ingresar un comentario.", this); }
            Random random = new Random();
            int randomNumber = random.Next(0, 100000);
            //si no subio archivo, solo esta subiendo un comentario
            if (fupPapeleria.HasFile && !error)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/tareas/"));//path local
                string mensaje = AddPapeleriaToTable(dirInfo + randomNumber.ToString() + fupPapeleria.FileName, Path.GetExtension(fupPapeleria.FileName).ToString(), txtNombreArchivo.Text.ToUpper(), id_archi);
                if (mensaje.Equals(string.Empty))
                {
                    bool pape = funciones.UploadFile(fupPapeleria, dirInfo + randomNumber.ToString() + fupPapeleria.FileName, this.Page);
                    if (pape == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + lnkGuardarPape.ClientID.ToString() + "');", true);
                        Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "2000", "Comentario Guardardo Correctamente", this);
                        //agregamos a tabla global de papelera
                        fupPapeleria.Visible = true;
                        Session["id_archi"] = null;
                    }
                }
                else
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
            if (!fupPapeleria.HasFile && !error)
            {
                string mensaje = AddPapeleriaToTable("", "", txtNombreArchivo.Text.ToUpper(), id_archi);
                if (mensaje.Equals(string.Empty))
                {
                    bool pape = true;
                    if (pape == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + lnkGuardarPape.ClientID.ToString() + "');", true);
                        Alert.ShowGift("Estamos subiendo el comentario.", "Espere un Momento", "imagenes/loading.gif", "2000", "Comentario Guardardo Correctamente", this);
                        //agregamos a tabla global de papelera
                        fupPapeleria.Visible = true;
                        Session["id_archi"] = null;
                    }
                }
                else
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }

        protected void gridPapeleria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridPapeleria.DataKeys[index].Values["ruta"].ToString();
            string extension = gridPapeleria.DataKeys[index].Values["extension"].ToString();
            string descripcion = gridPapeleria.DataKeys[index].Values["descripcion"].ToString();
            string id_archi = gridPapeleria.DataKeys[index].Values["id_archi"].ToString();
            Session["id_archivo"] = id_archi;
            DataTable papeleria = (DataTable)Session["papeleria"];
            int papeleriat = papeleria.Rows.Count;
            switch (e.CommandName)
            {
                case "Eliminar":
                    List<DataRow> rowsToDelete = new List<DataRow>();
                    foreach (DataRow row in papeleria.Rows)
                    {
                        if (row["ruta"].ToString().Equals(ruta) || row["descripcion"].ToString().Equals(descripcion))
                        {
                            rowsToDelete.Add(row);
                        }
                    }
                    foreach (DataRow rowde in rowsToDelete)
                    {
                        papeleria.Rows.Remove(rowde);
                    }
                    Session["papeleria"] = papeleria;
                    gridPapeleria.DataSource = papeleria;
                    gridPapeleria.DataBind();

                    break;

                case "Descargar":
                    funciones.Download(ruta, Path.GetFileName(ruta), this);
                    break;

                case "Editar":
                    // List<DataRow> rowsToDelete2 = new List<DataRow>();
                    foreach (DataRow row in papeleria.Rows)
                    {
                        if (row["ruta"].ToString().Equals(ruta) || row["descripcion"].ToString().Equals(descripcion))
                        {
                            //rowsToDelete2.Add(row);
                            txtNombreArchivo.Text = row["descripcion"].ToString();
                            break;
                        }
                    }
                    break;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + gridPapeleria.ClientID.ToString() + "');", true);
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
            if (txtdescripcion.Text.Length > 1000)
            {
                error = true;
                Alert.ShowAlertError("Solo se permite hasta 1000 caracteres en la descripcion", this);
            }
            if (txtfecha_solicompromiso.Text == "")
            {
                error = true;
                Alert.ShowAlertError("Coloque una fecha de compromiso para la Tarea", this);
            }
            if (TotalCadenaPuestos() == 0 && Convert.ToInt32(ddlPuesto.SelectedValue) == 0)
            {
                error = true;
                Alert.ShowAlertError("Agregre un puesto para la Tarea", this);
            }
            if (!error)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar esta Tarea. Una vez Guardada NO PODRA SER MODIFICADA?','modal fade modal-info');", true);
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
                        int idc_puestoasigna = div_puestoasigna.Visible ? Convert.ToInt32(ddlpuestoasigna.SelectedValue) : 0;
                        entidad.Pidc_puesto_asigna = idc_puestoasigna > 0 ? idc_puestoasigna:Convert.ToInt32(Session["sidc_puesto_login"]);
                        entidad.Pdescripcion = txtdescripcion.Text.ToUpper();
                        int TO = TotalCadenaPuestos();
                        bool OTROPUESTO = false;
                        if (div_puestoasigna.Visible == true && Convert.ToInt32(ddlPuesto.SelectedValue) != Convert.ToInt32(Session["sidc_puesto_login"]))
                        {
                            OTROPUESTO = true;
                        }
                        if (Convert.ToInt32(ddlpuestoasigna.SelectedValue) == Convert.ToInt32(Session["sidc_puesto_login"]))
                        {
                            OTROPUESTO = false;
                        }
                        if (TO > 1)
                        {
                            entidad.Ptotal_cadena_pro = TotalCadenaPuestos();
                            entidad.Pcadena_pro = CadenaPuestos();
                        }
                        entidad.POTROPUESTO = OTROPUESTO;
                        entidad.Pidc_puesto = Convert.ToInt32(ddlPuesto.SelectedValue);
                        int msec = Convert.ToDateTime(txtfecha_solicompromiso.Text).Millisecond;
                        int second = Convert.ToDateTime(txtfecha_solicompromiso.Text).Second;
                        entidad.Pfecha = (Convert.ToDateTime(txtfecha_solicompromiso.Text).AddMilliseconds(-msec)).AddSeconds(-second);
                        entidad.Ptotal_cadena_arch = TotalCadenaArchivos();
                        entidad.Pcadena_arch = CadenaArchivos();
                        if (txtidc_tareaser.Text != "")
                        {
                            entidad.Pidc_tareaser = Convert.ToInt32(txtidc_tareaser.Text.Trim());
                        }
                        if (Request.QueryString["idc_tarea"] != null)
                        {
                            entidad.Pidc_tarea = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"]));
                        }
                        DataSet ds;
                        if (TO > 1)
                        {
                            ds = componente.AgregarTareaMulti(entidad);
                        }
                        else
                        {
                            ds = componente.AgregarTarea(entidad);
                        }
                        //mensaje de error o abortado
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            string url = ds.Tables[0].Rows[0]["url"].ToString();
                            string FECHA = ds.Tables[0].Rows[0]["FECHA"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "noti533sededW3", "SendSlack('" + url + "');", true);
                            DataTable tabla_archivos = ds.Tables[1];
                            bool correct = true;
                            foreach (DataRow row_archi in tabla_archivos.Rows)
                            {
                                string ruta_det = row_archi["ruta_destino"].ToString();
                                string ruta_origen = row_archi["ruta_origen"].ToString();
                                correct = funciones.CopiarArchivos(ruta_origen, ruta_det, this.Page);
                                if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo " + ruta_origen + "a la ruta " + ruta_det, this); }
                            }
                            int total = (((tabla_archivos.Rows.Count) * 1) + 1) * 1000;
                            string t = total.ToString();
                            string url_back = Request.QueryString["idc_tarea"] != null ? (string)Session["Back_Page"] : "tareas.aspx";
                            ScriptManager.RegisterStartupScript(this,GetType(),Guid.NewGuid().ToString(), 
                                "AlertGO('"+ "La tarea fue Guardada Correctamente con una Fecha de compromiso para el dia " + FECHA+"','"+url_back+"');", true);
                         
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.ToString(), this.Page);
                    }

                    break;
            }
        }

        protected void ddlPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc_puesto = Convert.ToInt32(ddlPuesto.SelectedValue);
            if (idc_puesto == 0)
            {
                Alert.ShowAlertError("Seleccione un Puesto Valido, o intente buscando uno.", this);
            }
            else
            {
                CargarTareas(idc_puesto);
                TareasServicios(idc_puesto);
            }
        }

        protected void txtfecha_solicompromiso_TextChanged(object sender, EventArgs e)
        {
            if (txtfecha_solicompromiso.Text == "")
            {
                Alert.ShowAlertError("Escriba la fecha en el formato correcto.", this);
                txtfecha_solicompromiso.Text = DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
            }
            else
            {
                DateTime caption = Convert.ToDateTime(txtfecha_solicompromiso.Text.Replace('T', ' '));
                if (caption < DateTime.Today)
                {
                    Alert.ShowAlertError("No puede estipular una fecha menor a hoy.", this);
                    txtfecha_solicompromiso.Text = DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                }
            }
        }

        protected void grid_puestpos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc = Convert.ToInt32(grid_puestpos.DataKeys[index].Values["idc_puesto"]);
            DataTable dt = (DataTable)Session["puestos_tareas"];
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt32(row["idc_puesto"]) == idc)
                {
                    row.Delete();
                    break;
                }
            }
            Session["puestos_tareas"] = dt;
            CARGAR();
        }

        protected void lnkadd_Click(object sender, EventArgs e)
        {
            int idc_puesto = Convert.ToInt32(ddlPuesto.SelectedValue);
            if (idc_puesto == 0)
            {
                Alert.ShowAlertError("Seleccione un Puesto Valido, o intente buscando uno.", this);
            }
            else
            {
                DataTable dt = (DataTable)Session["puestos_tareas"];
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row["idc_puesto"]) == idc_puesto)
                    {
                        row.Delete();
                        break;
                    }
                }
                DataRow newer = dt.NewRow();
                newer["idc_puesto"] = idc_puesto;
                newer["descripcion_puesto_completa"] = ddlPuesto.SelectedItem;
                dt.Rows.Add(newer);
                Session["puestos_tareas"] = dt;
                CARGAR();
            }
        }

        private void CARGAR()
        {
            DataTable dt = (DataTable)Session["puestos_tareas"];
            grid_puestpos.DataSource = dt;
            grid_puestpos.DataBind();
        }

        private string CadenaPuestos()
        {
            string cadena = "";
            DataTable dt = (DataTable)Session["puestos_tareas"];
            foreach (DataRow row in dt.Rows)
            {
                cadena = cadena + row["idc_puesto"] + ";";
            }
            return cadena;
        }

        private int TotalCadenaPuestos()
        {
            DataTable dt = (DataTable)Session["puestos_tareas"];
            return dt.Rows.Count;
        }

        protected void ddlservicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc_tareaser = Convert.ToInt32(ddlservicios.SelectedValue);
            if (idc_tareaser == 0)
            {
                ViewState["tabla_servicios"] = null;
                ddlservicios.Items.Clear();
                titleserv.Visible = false;
                tareaservicios.Visible = false;
                //txtdescripcion.ReadOnly = false;
                //txtfecha_solicompromiso.ReadOnly = false;
                lblhoras_tarea_serv.Text = "";
                lblobservacionesser.Text = "";
                txtfecha_solicompromiso.Text = DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
            }
            else
            {
                TareasServiciosDetalles(idc_tareaser);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int idc_tareaser = Convert.ToInt32(ddlservicios.SelectedValue);
            if (idc_tareaser > 0)
            {
                string url = "tareas_servicios_captura.aspx?view=HEHEHASISEVE&solo_lista=KNWODBWODBWOEBDOWDOWKDBOWEKDBEWBDOWEPOP&idc_tareaser=" + funciones.deTextoa64(idc_tareaser.ToString());
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMesededsage", "window.open('" + url + "');", true);
            }            
     
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            CargaPuestosAsigna(txtpuesto_asigna.Text);
        }
    }
}