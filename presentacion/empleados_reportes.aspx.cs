using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class empleados_reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            //SI ES UN ALTA
            if (!IsPostBack && Request.QueryString["autoriza"] == null)
            {
                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("descripcion");
                papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("extension");
                papeleria.Columns.Add("id_archi");
                Session["papeleria"] = papeleria;
                Session["idc_empleado"] = null;
                CargarReportes();
                CargaPuestos("");
                btnGuardar.Visible = true;
                btnvistobueno.Visible = false;
                obsrva.Visible = false;
            }
            if (!IsPostBack && Request.QueryString["autoriza"] != null)
            {
                Session["idc_empleado"] = null;
                int idc_emplado = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleado"]));
                int pidc_empleadorep = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleadorep"]));
                CargaPuestos("");
                CargarReportes();
                CargarGridPrincipal(idc_emplado);
                CargarPendientes(pidc_empleadorep);
                btnGuardar.Visible = false;
                btnvistobueno.Visible = true;
                obsrva.Visible = true;
            }
        }

        /// <summary>
        /// Carga Puestos en Filtro
        /// </summary>
        public void CargaPuestos(string filtro)
        {
            try
            {
                Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
                Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
                entidad.Filtro = filtro;
                entidad.Ptipo = "R";
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = componente.CargaComboDinamicoOrgn(entidad);
                ddlPuestoAsigna.DataValueField = "idc_empleado";
                ddlPuestoAsigna.DataTextField = "descripcion_puesto_completa";
                ddlPuestoAsigna.DataSource = ds.Tables[0];
                ddlPuestoAsigna.DataBind();
                if (filtro == "")
                {
                    ddlPuestoAsigna.Items.Insert(0, new ListItem("--Seleccione un Empleado", "0")); //updated code}
                }
                else
                {
                    int idc_emplado = Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
                    CargarGridPrincipal(idc_emplado);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga los datos del empleado
        /// </summary>
        /// <param name="idc_puesto"></param>
        public void CargarGridPrincipal(int idc_empleado)
        {
            try
            {
                PuestosServiciosENT entidad = new PuestosServiciosENT();
                PuestosServiciosCOM componente = new PuestosServiciosCOM();
                entidad.Pidc_pre_empleado = idc_empleado;
                entidad.PReves = false;
                DataSet ds = componente.CargaPuestos(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblPuesto.Text = ds.Tables[0].Rows[0]["descripcion"].ToString();
                    lblEmpleado.Text = ds.Tables[0].Rows[0]["empleado"].ToString();
                    lbldepto.Text = ds.Tables[0].Rows[0]["depto"].ToString();
                    Session["idc_empleado"] = Convert.ToInt32(ds.Tables[0].Rows[0]["idc_empleado"]);
                    string rutaimagen = funciones.GenerarRuta("fot_emp", "rw_carpeta");
                    var domn = Request.Url.Host;
                    if (domn == "localhost" || Convert.ToInt32(ds.Tables[0].Rows[0]["idc_empleado"]) == 0)
                    {
                        var url = "imagenes/btn/default_employed.png";
                        imgEmpleado.ImageUrl = url;
                    }
                    else
                    {
                        var url = "http://" + domn + rutaimagen + ds.Tables[0].Rows[0]["idc_empleado"].ToString() + ".jpg";
                        ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                        imgEmpleado.ImageUrl = url;
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void CargarReportes()
        {
            try
            {
                ReportesENT entidad = new ReportesENT();
                ReportesCOM componentes = new ReportesCOM();
                ddltiporeporte.DataValueField = "idc_tiporep";
                ddltiporeporte.DataTextField = "descripcion";
                ddltiporeporte.DataSource = componentes.Carga(entidad);
                ddltiporeporte.DataBind();
                ddltiporeporte.Items.Insert(0, new ListItem("--Seleccione un Reporte", "0")); //updated code}
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void CargarPendientes(int pidc_empleadorep)
        {
            try
            {
                ReportesENT entidad = new ReportesENT();
                entidad.Pidc_empleadorep = pidc_empleadorep;
                ReportesCOM componente = new ReportesCOM();
                DataTable dt = componente.CargaJefe(entidad).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblfecha.Visible = true;
                    lblfecha.Text = "Reporte Realizado el dia " + row["fecha_reporte"].ToString();
                    txtobservaciones_auto.Text = row["observaciones_completa"].ToString();
                    FILTRO.Visible = false;
                    txtobservaciones_auto.Enabled = false;
                    lnkbuscarpuestos.Visible = false;
                    txtpuesto_filtro.Visible = false;
                    ddltiporeporte.SelectedValue = row["IDC_TIPOREP"].ToString();
                    ddltiporeporte.Enabled = false;
                }
                else
                {
                    Alert.ShowAlertError("No se encontro ninguna Fila", this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                ReportesENT entiddad = new ReportesENT();
                ReportesCOM componente = new ReportesCOM();
                DataSet ds = new DataSet();
                string vmensaje = "";
                string caso = (string)Session["Caso_Confirmacion"];
                switch (caso)
                {
                    case "Guardar":

                        entiddad.Pidc_empleado = Convert.ToInt32(Session["idc_empleado"]);
                        entiddad.Pidc_tiporep = Convert.ToInt32(ddltiporeporte.SelectedValue);
                        entiddad.PObservaciones = txtcomentarios.Text.ToUpper();
                        entiddad.Pcadena = CadenaArchivos();
                        entiddad.Ptotal_cadena = TotalCadenaArchivos();
                        entiddad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entiddad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entiddad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entiddad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        ds = componente.AgregarReporte(entiddad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos Guardando el Reporte.", "Espere un Momento", "menu.aspx", "imagenes/loading.gif", "2000", "El Reporte fue Guardado correctamente ", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                        break;

                    case "Cancelar":
                        Response.Redirect("menu.aspx");
                        break;

                    case "Revizar":
                        entiddad.Pidc_empleado = Convert.ToInt32(Session["sidc_empleado"]);
                        entiddad.Pidc_empleadorep = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleadorep"]));
                        entiddad.PObservaciones = txtcomentarios.Text.ToUpper();
                        entiddad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entiddad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entiddad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entiddad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        ds = componente.VoboReporte(entiddad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos Autorizando el Reporte.", "Espere un Momento", "menu.aspx", "imagenes/loading.gif", "2000", "El Reporte fue Revisado correctamente ", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idc_tiporev = Convert.ToInt32(ddltiporeporte.SelectedValue);
            int idc_empleado = Convert.ToInt32(ddltiporeporte.SelectedValue);
            if (idc_tiporev == 0)
            {
                Alert.ShowAlertError("Seleccione un Tipo de Reporte", this);
            }
            else if (idc_empleado == 0)
            {
                Alert.ShowAlertError("Seleccione un Empleado", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar este Reporte?','modal fade modal-info');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar este Reporte?','modal fade modal-danger');", true);
        }

        protected void lnkGuardarPape_Click(object sender, EventArgs e)
        {
            string id_archi = "0";
            id_archi = Session["id_archi"] != null ? (string)Session["id_archi"] : "0";
            Random random = new Random();
            int randomNumber = random.Next(0, 1000);
            if (!fupPapeleria.HasFile)
            {
                Alert.ShowAlertError("Seleccione un archivo", this);
            }
            else if (txtNombreArchivo.Text == "")
            {
                Alert.ShowAlertError("Ingrese una descripcion para el archivo", this);
            }
            else
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
                    if (!File.Exists(ruta))
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
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(ruta));
                        // Escribimos el fichero a enviar
                        Response.WriteFile(ruta);
                        // volcamos el stream
                        Response.Flush();
                        // Enviamos todo el encabezado ahora
                        Response.End();
                        // Response.End();
                    }
                    break;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + gridPapeleria.ClientID.ToString() + "');", true);
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

        protected void ddlPuestoAsigna_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc_puesto = Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
            if (idc_puesto == 0)
            {
                Alert.ShowAlertError("Seleccione un Puesto Valido, o intente buscando uno.", this);
            }
            else
            {
                CargarGridPrincipal(idc_puesto);
            }
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            CargaPuestos(txtpuesto_filtro.Text);
        }

        protected void btnvistobueno_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Revizar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea dar el Visto Bueno a este Reporte?','modal fade modal-info');", true);
        }
    }
}