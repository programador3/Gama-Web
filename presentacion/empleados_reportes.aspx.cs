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
            if (!IsPostBack && Request.QueryString["termina"] != null)//contraparte
            {
                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("descripcion");
                papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("extension");
                Session["papeleria"] = papeleria;
                Session["idc_empleado"] = null;
                int idc_emplado = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleado"]));
                int pidc_empleadorep = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleadorep"]));
                CargarReportes(idc_emplado);
                CargaPuestos("");
                CargarGridPrincipal(idc_emplado);
                CargarPendientes(pidc_empleadorep);
                btnGuardar.Visible = false;
                btnterminar.Visible = true;
                obsrvobo.Visible = true;
                div_empleadoalta.Visible = false;
                div_busqueda.Visible = false;
                FILTRO.Visible = true;
                txtpuesto_filtro.Visible = true;
                lnkbuscarpuestos.Visible = true;
                txtcomentarios.ReadOnly = true;
                txtobservaciones_auto.Visible = true;
                obsrva.Visible = true;
                lnlvalido.Visible = true;
                lbltitle.Attributes["style"] = "color:orangered;";
                lbltitle.Text = "Puede Reasignar el Reporte a un empleado";
                FILTRO.Visible = false;
                archi.Visible = false;
                if (Request.QueryString["view"] != null)
                {
                    ddlPuestoAsigna.Enabled = false;
                    txtpuesto_filtro.ReadOnly = true;
                    lnkbuscarpuestos.Visible = false;
                    btnterminar.Visible = false;
                    btnCancelar.Text = "Regresar";
                    if (Request.QueryString["WINOPENER"] != null)
                    {
                        btnCancelar.Visible = false;
                        BTNCERRAR.Visible = true;
                    }
                }
                else {
                    string queyr = "idc_empleado = " + idc_emplado + " and idc_usuario = " + Convert.ToInt32(Session["sidc_usuario"]).ToString().Trim() + "";
                    if (TengoHistorialConEsteBato(DateTime.Now.AddDays(-1000), DateTime.Today, queyr))
                    {
                        lblmihis.Visible = true;
                        lnkverhisto.Visible = true;
                        lnkverhisto.OnClientClick = "window.open('empleados_incidencias_reporte.aspx?desglo=1&report=JBAKJSBKJSBJABSJABSJABS&query=" + funciones.deTextoa64(queyr) + "')";
                    }
                }
               
            }
            else
            {
                if (!IsPostBack && Request.QueryString["autoriza"] == null)//nuevo reporte
                {
                    if (Request.QueryString["backurl"] != null)
                    {
                        txturl_back.Text = funciones.de64aTexto(Request.QueryString["backurl"]);
                    }
                    DataTable papeleria = new DataTable();
                    papeleria.Columns.Add("descripcion");
                    papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
                    papeleria.Columns.Add("ruta");
                    papeleria.Columns.Add("extension");
                    Session["papeleria"] = papeleria;
                    Session["idc_empleado"] = null;
                    CargaPuestos("");
                    btnGuardar.Visible = true;
                    btnvistobueno.Visible = false;
                    obsrvobo.Visible = false;
                    int idc_empleado = Convert.ToInt32(Session["sidc_empleado"]);
                    if (idc_empleado == 0)//quiere decir que es un usuario sin empleado
                    {
                        div_empleadoalta.Visible = true;
                        txtbuscar.Focus();
                    }
                    obsrva.Visible = true;
                    if (Request.QueryString["idc_empleado"] != null)
                    {
                        int idc_empleadoe = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleado"]));
                        CargarGridPrincipal(idc_empleadoe);
                        txtpuesto_filtro.ReadOnly = true;
                        lnkbuscarpuestos.Visible = false;
                        ddlPuestoAsigna.Enabled = false;
                        string queyr = "idc_empleado = "+idc_empleadoe+" and idc_usuario = "+Convert.ToInt32(Session["sidc_usuario"]).ToString().Trim()+"";
                        if (TengoHistorialConEsteBato(DateTime.Now.AddDays(-1000), DateTime.Today, queyr))
                        {
                            lblmihis.Visible = true;
                            lnkverhisto.Visible = true;
                            lnkverhisto.OnClientClick = "window.open('empleados_incidencias_reporte.aspx?desglo=1&report=JBAKJSBKJSBJABSJABSJABS&query=" + funciones.deTextoa64(queyr)+"')";
                        }

                    }
                    archi.Visible = true;

                }
                if (!IsPostBack && Request.QueryString["autoriza"] != null)//vobo reporte
                {
                    DataTable papeleria = new DataTable();
                    papeleria.Columns.Add("descripcion");
                    papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
                    papeleria.Columns.Add("ruta");
                    papeleria.Columns.Add("extension");
                    Session["papeleria"] = papeleria;
                    Session["idc_empleado"] = null;
                    int idc_emplado = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleado"]));
                    int pidc_empleadorep = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleadorep"]));
                    CargaPuestos("");
                    CargarGridPrincipal(idc_emplado);
                    CargarPendientes(pidc_empleadorep);
                    btnGuardar.Visible = false;
                    btnvistobueno.Visible = true;
                    obsrvobo.Visible = true;
                    lnlvalido.Visible = true;

                    archi.Visible = false;
                    lnlvalido.Text = "Estoy Deacuerdo con el Reporte";
                    string queyr = "idc_empleado = " + idc_emplado + " and idc_usuario = " + Convert.ToInt32(Session["sidc_usuario"]).ToString().Trim() + "";
                    if (TengoHistorialConEsteBato(DateTime.Now.AddDays(-1000), DateTime.Today, queyr))
                    {
                        lblmihis.Visible = true;
                        lnkverhisto.Visible = true;
                        lnkverhisto.OnClientClick = "window.open('empleados_incidencias_reporte.aspx?desglo=1&report=JBAKJSBKJSBJABSJABSJABS&query=" + funciones.deTextoa64(queyr) + "')";
                    }
                }
            }
            
        }

        private bool TengoHistorialConEsteBato(DateTime f1, DateTime f2, string query)
        {
            try
            {
                ReportesENT entidad = new ReportesENT();
                entidad.Pfecha = f1;
                entidad.Pfechafin = f2;
                ReportesCOM componente = new ReportesCOM();
                DataTable DT = componente.CargaJefe(entidad).Tables[0];
                DataView dv = DT.DefaultView;
                dv.RowFilter = query;
                return dv.ToTable().Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
                return false;
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
                entidad.Ptipo = "";
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = componente.CargaComboDinamicoOrgn(entidad);
                ddlPuestoAsigna.DataValueField = "idc_empleado";
                ddlPuestoAsigna.DataTextField = "descripcion_puesto_completa";
                ddlPuestoAsigna.DataSource = ds.Tables[0];
                ddlPuestoAsigna.DataBind();
                ddlPuestoAsigna.Items.Insert(0, new ListItem("--Seleccione un Empleado", "0")); //updated code}
                if (Request.QueryString["idc_empleado"] != null && filtro == "")
                {
                    int idc_empleadoe = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleado"]));
                    ddlPuestoAsigna.SelectedValue = idc_empleadoe.ToString();
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
                if (Request.QueryString["termina"] == null)
                {
                    CargarReportes(idc_empleado);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga Combo con los tipos de reportes
        /// </summary>
        private void CargarReportes(int IDC_EMPLEADO)
        {
            try
            {
                ReportesENT entidad = new ReportesENT();
                entidad.Pidc_empleado = IDC_EMPLEADO; 
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
                DataSet ds = componente.CargaJefe(entidad);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblfecha.Visible = true;
                    lblfecha.Text = "Reporte Realizado el dia " + row["fecha_reporte"].ToString();
                    txtobservaciones_auto.Text = row["usuario"].ToString().Trim()+": "+ row["observaciones_completa"].ToString();
                    txtcomentarios.Text = row["usuario_vobo"].ToString().Trim() + ": " + row["observaciones_vobo"].ToString().Replace("Sin Visto Bueno:", "");
                    txtcomentarios.Text = txtcomentarios.Text.Replace("Sin Visto Bueno: ","");
                    FILTRO.Visible = false;
                    txtobservaciones_auto.Visible = true;
                    lnkbuscarpuestos.Visible = false;
                    txtpuesto_filtro.Visible = false;
                    ddltiporeporte.SelectedValue = row["IDC_TIPOREP"].ToString();
                    ddltiporeporte.Enabled = false;
                    txtobservaciones_auto.ReadOnly = true;
                    if (Request.QueryString["view"] != null)
                    {
                        lnlvalido.CssClass = row["estado"].ToString().Trim() == "T" ? "btn btn-success btn-block" : "btn btn-default btn-block";
                    }
                    int idc_original = Convert.ToInt32(row["idc_empleadorep_orig"]);
                    if (idc_original > 0)
                    {
                        lnkoriginal.Visible = true;
                        lblreportereasignado.Visible = true;
                        txtidoriginal.Text = idc_original.ToString().Trim();
                    }//es reasignada
                    DataTable dt2 = ds.Tables[1];
                    foreach (DataRow rows in dt2.Rows)
                    {
                        string ruta = rows["ruta"].ToString();
                        string extension = rows["extension"].ToString();
                        string descripcion = rows["descripcion"].ToString();
                        AddPapeleriaToTable(ruta,extension,descripcion);
                      
                    }
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
                entiddad.Pcadena = CadenaArchivos();
                entiddad.Ptotal_cadena = TotalCadenaArchivos();
                ReportesCOM componente = new ReportesCOM();
                DataSet ds = new DataSet();
                string vmensaje = "";
                string caso = (string)Session["Caso_Confirmacion"];
                string url = Session["backurl"] != null ? Session["backurl"] as string : "empleados_reportes.aspx";
                if (txturl_back.Text != "")
                {
                    url = txturl_back.Text;
                }
                switch (caso)
                {
                    case "Guardar":

                        entiddad.Pidc_empleado = Convert.ToInt32(Session["idc_empleado"]);
                        entiddad.Pidc_tiporep = Convert.ToInt32(ddltiporeporte.SelectedValue);
                        entiddad.PObservaciones = txtobservaciones_auto.Text.ToUpper();
                        entiddad.Pcadena = CadenaArchivos();
                        entiddad.Ptotal_cadena = TotalCadenaArchivos();
                        entiddad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entiddad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entiddad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entiddad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        int idc_empleado = Convert.ToInt32(Session["sidc_empleado"]);
                        if (idc_empleado == 0)//quiere decir que es un usuario sin empleado
                        {
                            if (txtidc_empleado.Text  == "")//quiere decir que es un usuario sin empleado
                            {
                                Alert.ShowAlertError("Para Guardar el reporte, Debe seleccionar quien es usted",this);
                                return;
                            }
                            else {
                                idc_empleado = Convert.ToInt32(txtidc_empleado.Text.Trim());
                            }
                        }
                        entiddad.Pidc_empleadoalta = idc_empleado;
                        ds = componente.AgregarReporte(entiddad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
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
                            Alert.ShowGiftMessage("Estamos Guardando el Reporte.", "Espere un Momento", url, "imagenes/loading.gif", t, "El Reporte fue Guardado correctamente", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                        break;

                    case "Cancelar":

                        Session["backurl"] = null;
                        Response.Redirect(url);
                        break;

                    case "Revizar":
                        entiddad.PCERRADO = lnlvalido.CssClass == "btn btn-success btn-block" ? "T" : "R";
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
                            Alert.ShowGiftMessage("Estamos Autorizando el Reporte.", "Espere un Momento", "empleados_reportes_pendientes.aspx", "imagenes/loading.gif", "2000", "El Reporte fue Revisado correctamente ", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                        break;
                    case "Terminar":
                        int idc_empleado_nuevo = Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
                        string masstring = idc_empleado_nuevo > 0 ? " y Reasignando" : "";
                        entiddad.PCERRADO = lnlvalido.CssClass == "btn btn-success btn-block" ? "T" : "R";
                        entiddad.preasigna = idc_empleado_nuevo > 0 ? true : false;
                        entiddad.Pidc_empleado = idc_empleado_nuevo > 0 ? idc_empleado_nuevo: Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleado"])); 
                        entiddad.Pidc_empleadorep = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleadorep"]));
                        entiddad.Pidc_empleadoalta = Convert.ToInt32(Session["sidc_empleado"]);
                        entiddad.PObservaciones = txtcomentarios.Text.ToUpper();
                        entiddad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entiddad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entiddad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entiddad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        ds = componente.TerminarReporte(entiddad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {                           
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
                            Alert.ShowGiftMessage("Estamos Terminando "+ masstring + " el Reporte.", "Espere un Momento", "empleados_reportes_contra.aspx", "imagenes/loading.gif", t, "El Reporte fue Terminado "+ masstring + " correctamente ", this);
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
            int idc_empleado = Session["idc_empleado"] == null ? 0: Convert.ToInt32(Session["idc_empleado"]);
          
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
            if (Request.QueryString["view"] != null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cerrar este Reporte?','modal fade modal-danger');", true);

            }
            else {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar este Reporte?','modal fade modal-danger');", true);

            }
        }

        protected void lnkGuardarPape_Click(object sender, EventArgs e)
        {
          
            Random random = new Random();
            int randomNumber = random.Next(0, 100000);
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
                string mensaje = AddPapeleriaToTable(dirInfo + randomNumber.ToString() + fupPapeleria.FileName, Path.GetExtension(fupPapeleria.FileName).ToString(), txtNombreArchivo.Text.ToUpper());
                if (mensaje.Equals(string.Empty))
                {
                    bool pape = funciones.UploadFile(fupPapeleria, dirInfo + randomNumber.ToString() + fupPapeleria.FileName, this.Page);
                    if (pape == true)
                    {
                        Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "2000", "Archivo Guardardo Correctamente", this);
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
        public string AddPapeleriaToTable(string ruta, string extension, string descripcion)
        {
            string mensaje = "";
            bool exists = false;
            try
            {
                DataTable papeleria = (DataTable)Session["papeleria"];
                foreach (DataRow check in papeleria.Rows)
                {
                    if (check["ruta"].Equals(ruta) && check["descripcion"].Equals(descripcion))
                    {
                        check["ruta"] = ruta;
                        check["extension"] = extension;
                        check["descripcion"] = descripcion;
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
                Alert.ShowAlertError("Seleccione un Puesto con Empleado Activo, o intente buscando uno.", this);
            }
            else
            {
                if (Request.QueryString["termina"] != null) {
                    Alert.ShowAlertInfo("SI SELECCIONA UN EMPLEADO, EL REPORTE SERA REASIGNADO.","Mensaje del Sistema", this);
                }
                CargarGridPrincipal(idc_puesto);
                string queyr = "idc_empleado = " + idc_puesto + " and idc_usuario = " + Convert.ToInt32(Session["sidc_usuario"]).ToString().Trim() + "";
                if (TengoHistorialConEsteBato(DateTime.Now.AddDays(-1000), DateTime.Today, queyr))
                {
                    lblmihis.Visible = true;
                    lnkverhisto.Visible = true;
                    lnkverhisto.OnClientClick = "window.open('empleados_incidencias_reporte.aspx?desglo=1&report=JBAKJSBKJSBJABSJABSJABS&query=" + funciones.deTextoa64(queyr) + "')";
                }

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

        void BuscarEmpleadoYo(string value)
        {
            try
            {
                TareasCOM componenete = new TareasCOM();
                DataSet ds = componenete.sp_combo_empleados_nomina();
                DataTable dt = ds.Tables[0];
                ddlseleccionar.DataTextField = "nombre";
                ddlseleccionar.DataValueField = "idc_empleado";
                if (dt.Rows.Count > 0)
                {
                    DataView view = dt.DefaultView;
                    if (funciones.isNumeric(value.Trim()))
                    {
                        view.RowFilter = "nombre like '%" + value + "%' or num_nomina = " + value.Trim() + "";

                    }
                    else {


                        view.RowFilter = "nombre like '%" + value + "%'";
                    }
                    if (view.ToTable().Rows.Count > 0)
                    {
                        ddlseleccionar.DataSource = view.ToTable();
                        ddlseleccionar.DataBind();
                    }
                    else {
                        txtbuscar.Text = "";
                        Alert.ShowAlertInfo("La Busqueda no Encontro Resultados. Intentelo Nuevamente","Mensaje del Sistema", this);
                    }
                }
                ViewState["dt_yo"] = dt;
                ddlseleccionar.Items.Insert(0,new ListItem("--Seleccione un Empleado", "0"));
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarEmpleadoYo(txtbuscar.Text);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            BuscarEmpleadoYo(txtbuscar.Text);
        }

        protected void ddlseleccionar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc = Convert.ToInt32(ddlseleccionar.SelectedValue);
            if (idc > 0)
            {
                DataTable dt = ViewState["dt_yo"] as DataTable;
                DataView view = dt.DefaultView;
                view.RowFilter = "idc_empleado = " + idc + "";
                if (view.ToTable().Rows.Count > 0)
                {
                    txtidc_empleado.Text = idc.ToString().Trim();
                    lblnombremepleadoalta.Text = view.ToTable().Rows[0]["nombre"].ToString();
                    lblnomina.Text = view.ToTable().Rows[0]["num_nomina"].ToString();
                    div_busqueda.Visible = false;
                    string rutaimagen = funciones.GenerarRuta("fot_emp", "rw_carpeta");
                    var domn = Request.Url.Host;
                    if (domn == "localhost")
                    {
                        var url = "imagenes/btn/default_employed.png";
                        imgempleadoalta.ImageUrl = url;
                    }
                    else
                    {
                        var url = "http://" + domn + rutaimagen + idc.ToString() + ".jpg";
                        ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                        imgempleadoalta.ImageUrl = url;
                    }
                }
                else
                {
                    txtbuscar.Text = "";
                    Alert.ShowAlertInfo("La Busqueda no Encontro Resultados. Intentelo Nuevamente", "Mensaje del Sistema", this);
                }
            }
            else {

                Alert.ShowAlertError("Seleccione un Empleado",this);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            imgempleadoalta.ImageUrl = "";
            lblnomina.Text = "";
            txtidc_empleado.Text = "";
            div_busqueda.Visible = true;
            txtbuscar.Text = "";
            txtbuscar.Focus();
        }

        protected void btnterminar_Click(object sender, EventArgs e)
        {
            int idc_tiporev = Convert.ToInt32(ddltiporeporte.SelectedValue);
            int idc_empleado = Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
            int idc_emplado = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleado"]));
            if (idc_empleado > 0 && idc_tiporev == 0)
            {
                Alert.ShowAlertError("Si desea reasignar el Reporte, seleccione un Tipo de Reporte", this);
            }
            else if (idc_emplado == idc_empleado)
            {
                Alert.ShowAlertError("No puede reasignar el reporte al mismo empleado. ("+ddlPuestoAsigna.SelectedItem.ToString()+")", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Terminar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Terminar este Reporte?','modal fade modal-info');", true);
            }
        }

        protected void lnlvalido_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["view"] == null)
            {
                archi.Visible = false;
                lnlvalido.CssClass = lnlvalido.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
                if (Request.QueryString["termina"] != null && lnlvalido.CssClass == "btn btn-default btn-block")
                {
                    archi.Visible = true;
                    Alert.ShowAlertInfo("Puede Reasignar el Reporte", "Mensaje del Sistema", this);
                }
                if (Request.QueryString["termina"] != null)
                {
                    FILTRO.Visible = lnlvalido.CssClass == "btn btn-default btn-block" ? true : false;
                }
            }
           
          
           
        }

        protected void lnkoriginal_Click(object sender, EventArgs e)
        {
            if (txtidoriginal.Text != "")
            {
                ReportesCOM componente = new ReportesCOM();
                ReportesENT wntida = new ReportesENT();
                DataSet ds = componente.CargaJefe(wntida);
                DataTable dt = ds.Tables[0];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "idc_empleadorep = "+ txtidoriginal.Text.Trim() + "";
                if (dv.ToTable().Rows.Count > 0)
                {
                    string idc2 = dv.ToTable().Rows[0]["idc_empleado"].ToString().Trim();
                    string url = "empleados_reportes.aspx?WINOPENER=JSJQSBJQBSJQBSJQBSQ&view=KASKJOKXXBKQMBXOQKBXOQBXQJBXQJBJBKJ&termina=KJBKJBWQOWJBOQKBWDOQBOWKOKQNKOOKBAOBQDOKQND&idc_empleadorep=" +
                        funciones.deTextoa64(txtidoriginal.Text.Trim()) + "&idc_empleado=" + funciones.deTextoa64(idc2);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMewswswsssage", "window.open('" + url + "');", true);
                }

            }
        }
    }
}