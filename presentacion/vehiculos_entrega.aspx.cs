using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class vehiculos_entrega : System.Web.UI.Page
    {
        public string rutaimagen = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            //si nop trae valores regreso
            if (Request.QueryString["idc_entrega"] == null)
            {
                Response.Redirect("entregas.aspx");
            }
            //bajo valores

            int idc_entrega = 0;
            int idc_puestoent = 0;
            int idc_usuario = 0;
            int idc_puesto = 0;

            if (Request.QueryString["preview"] == null)//si no viene del administrador
            {
                idc_puestoent = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_entrega = Convert.ToInt32(Request.QueryString["idc_entrega"].ToString());
            }
            else
            {
                idc_puestoent = Convert.ToInt32(Request.QueryString["idc_puestoent"].ToString());
                idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_entrega = Convert.ToInt32(Request.QueryString["idc_entrega"].ToString());
                btnCancelar.Enabled = false;
                btnGuardar.Enabled = false;
                Alert.ShowAlertInfo("Esta pagina esta en modo informativo, NO PODRA guardar la Revisión. Si desea hacerlo, verifique el apartado de notificaciones", "Mensaje del sistema", this);
            }
            if (!Page.IsPostBack)
            {
                Session["Tabla_DatosEmpleado"] = null;
                Session["Tabla_Vehiculos"] = null;
                Session["Tabla_Vehiculos_Herr"] = null;
                Session["carta_resp_file"] = null;
                Session["formato_file"] = null;
                DataTable dt = new DataTable();
                dt.Columns.Add("ruta");
                dt.Columns.Add("idc_vehiculo");
                dt.Columns.Add("vehiculo");
                dt.Columns.Add("tipo");
                Session["formato_file"] = dt;
                //cargo datos a tablas
                CargarGridPrincipal(idc_entrega, idc_puestoent);
            }
            gridPapeleria.DataSource = (DataTable)Session["formato_file"];
            gridPapeleria.DataBind();
        }

        /// <summary>
        /// Carga los datos en Tablas de sesion y carga el grid principal
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal(int idc_entrega, int idc_puestoent)
        {
            try
            {
                Vehiculos_EntregaENT entidad = new Vehiculos_EntregaENT();
                Vehiculos_EntregarCOM componente = new Vehiculos_EntregarCOM();
                entidad.Pidc_puestoentrega = idc_puestoent;
                entidad.Pidc_entrega = idc_entrega;
                DataSet ds = componente.CargaVehPrep(entidad);
                Session["Tabla_DatosEmpleado"] = ds.Tables[0];
                Session["Tabla_Vehiculos"] = ds.Tables[1];
                Session["Tabla_Vehiculos_Herr"] = ds.Tables[2];
                RepeatVehiculos.DataSource = ds.Tables[1];
                RepeatVehiculos.DataBind();
                ddlvehiculo.DataTextField = "descripcion_vehiculo";
                ddlvehiculo.DataValueField = "idc_vehiculo";
                ddlvehiculo.DataSource = ds.Tables[1];
                ddlvehiculo.DataBind();
                ddlvehiculo.Items.Insert(0, new ListItem("Seleccione un Vehiculo", "0")); //updated code}
                repearVehiculos_rev.DataSource = ds.Tables[1];
                repearVehiculos_rev.DataBind();
                repeat_vehiculos_herramientas_rev.DataSource = ds.Tables[2];
                repeat_vehiculos_herramientas_rev.DataBind();
                DataRow row = ds.Tables[0].Rows[0];
                lblPuesto.Text = row["descripcion"].ToString();
                lblEmpleado.Text = row["empleado"].ToString();
                Session["idc_entrega"] = Convert.ToInt32(row["idc_entrega"].ToString());
                Session["idc_empleado"] = Convert.ToInt32(row["idc_empleado"].ToString());
                Session["idc_puesto"] = Convert.ToInt32(row["idc_puesto"].ToString());
                lblfecha.Text = row["fecha"].ToString();
                string rutaimagen = funciones.GenerarRuta("fot_emp", "rw_carpeta");
                var domn = Request.Url.Host;
                if (domn == "localhost")
                {
                    var url = "imagenes/btn/default_employed.png";
                    imgEmpleado.ImageUrl = url;
                }
                else
                {
                    var url = "http://" + domn + rutaimagen + row["idc_empleado"].ToString() + ".jpg";
                    ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                    imgEmpleado.ImageUrl = url;
                }
                lnkDescargarFormato.CommandName = funciones.GenerarRuta("imarev", "unidad") + "responsiva_vehiculos.pdf";
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga el grid de gerramientas por vehiculo
        /// </summary>
        /// <param name="idc_puesto"></param>
        public void CargarGridVehiculosHerramientas(int idc_vehiculo)
        {
            herramientasENT entidad = new herramientasENT();
            entidad.Idc_vehiculo = idc_vehiculo;
            herramientasCOM componente = new herramientasCOM();
            DataSet ds = componente.CargaHerramientasVehculo(entidad);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                PanelsinHVehiculo.Visible = true;
                PanelconHVehiculo.Visible = false;
            }
            Session["Tabla_Vehiculos_Herr"] = ds.Tables[0];
            gridHerramientasVehiculo.DataSource = ds.Tables[0];
            gridHerramientasVehiculo.DataBind();
        }

        /// <summary>
        /// Genera cadena con resultado de preparaciones
        /// </summary>
        /// <returns></returns>
        public string GeneraCadenaVehiculos()
        {
            string list = "";
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Vehiculos"];
            foreach (RepeaterItem item in repearVehiculos_rev.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblidc_vehiculo");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_vehiculo"]) == Convert.ToInt32(lblidc.Text))
                        {
                            list = list + (lblidc.Text + ";" + txt.Text + ";");
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Genera cadena con resultado de preparaciones
        /// </summary>
        /// <returns></returns>
        public int TotalCadenaVehiculos()
        {
            int list = 0;
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Vehiculos"];
            foreach (RepeaterItem item in repearVehiculos_rev.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblidc_vehiculo");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_vehiculo"]) == Convert.ToInt32(lblidc.Text))
                        {
                            list = list + 1;
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Genera cadena con resultado de preparaciones
        /// </summary>
        /// <returns></returns>
        public string GeneraCadenaAccesoriosVehiculos_Herr()
        {
            string list = "";
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Vehiculos_Herr"];
            foreach (RepeaterItem item in repeat_vehiculos_herramientas_rev.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones1");
                Label lblidc = (Label)item.FindControl("lblaccesorio");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_tipoherramienta"]) == Convert.ToInt32(lblidc.Text))
                        {
                            list = list + (lblidc.Text + ";" + txt.Text + ";");
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Genera cadena con resultado de preparaciones
        /// </summary>
        /// <returns></returns>
        public int TotalCadenaVehiculos_Herr()
        {
            int list = 0;
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Vehiculos_Herr"];
            foreach (RepeaterItem item in repeat_vehiculos_herramientas_rev.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones1");
                Label lblidc = (Label)item.FindControl("lblaccesorio");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_tipoherramienta"]) == Convert.ToInt32(lblidc.Text))
                        {
                            list = list + 1;
                        }
                    }
                }
            }
            return list;
        }

        private bool ComprobacionHerramientaVehiculo()
        {
            bool correcto = true;
            foreach (RepeaterItem item in repeat_vehiculos_herramientas_rev.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones1");
                Label lblidc = (Label)item.FindControl("lblaccesorio");
                Label idc_vehiculo = (Label)item.FindControl("lblcelular");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (RepeaterItem itemh in repearVehiculos_rev.Items)
                    {
                        CheckBox cbhx = (CheckBox)itemh.FindControl("cbx");
                        Label lblidch = (Label)itemh.FindControl("lblidc_vehiculo");
                        if (cbhx.Enabled == true && cbhx.Checked == false && lblidch.Text == idc_vehiculo.Text)
                        {
                            correcto = false;
                            break;
                        }
                    }
                }
            }
            return correcto;
        }

        /// <summary>
        /// Retorna el total de vehiculos que no se han verificado
        /// </summary>
        /// <returns></returns>
        private int TotalVehiculos()
        {
            int total = 0;
            foreach (RepeaterItem itemh in repearVehiculos_rev.Items)
            {
                CheckBox cbhx = (CheckBox)itemh.FindControl("cbx");
                Label lblidch = (Label)itemh.FindControl("lblidc_vehiculo");
                if (cbhx.Enabled == true)
                {
                    total = total + 1;
                }
            }
            return total;
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        protected void RepeatVehiculos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //buscamos los controles dentro de repeat para asignar valores
            LinkButton lnkVerHVehiculos = (LinkButton)e.Item.FindControl("lnkVerHVehiculos");
            LinkButton lnkDescargarFormatoAce = (LinkButton)e.Item.FindControl("lnkDescargarFormatoAce");
            Image img = (Image)e.Item.FindControl("imgVehiculos");
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string idc_vehiculo = Convert.ToString(DataBinder.Eval(dbr, "idc_vehiculo"));
            int idc_formatorev = Convert.ToInt32(DataBinder.Eval(dbr, "idc_formatorev"));
            //idc_behiculo => boton editar
            lnkVerHVehiculos.CommandName = idc_vehiculo;
            //generamos ruta de imagen
            GenerarRuta(Convert.ToInt32(idc_vehiculo), "VEHIC");
            img.ImageUrl = rutaimagen;
            rutaimagen = "";//limpiamos variable global
            lnkDescargarFormatoAce.Visible = true;
            lnkDescargarFormatoAce.CommandName = idc_vehiculo;
            //  formato.Visible = Convert.ToInt32(ds.Tables[1].Rows[0]["idc_formatorev"]) > 0 ? true : false;
        }

        /// <summary>
        /// Genera ruta de archivos
        /// </summary>
        public void GenerarRuta(int id_comprobar, string codigo_imagen)
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
                    case "VEHIC"://fotos de vehiculos
                        if (domn == "localhost")//si es localhost generamos imagen default
                        {
                            var url = "imagenes/btn/car_default.png";
                            rutaimagen = url;
                        }
                        else
                        {//si pagina esta en srvidor
                            var url = "http://" + domn + carpeta + id_comprobar + ".jpg";
                            rutaimagen = url;
                        }
                        break;
                }
            }
        }

        protected void lnkVerHVehiculos_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            //BAJO ID DEL ELEMENTO
            string idc_vehiculo = btn.CommandName.ToString();
            CargarGridVehiculosHerramientas(Convert.ToInt32(idc_vehiculo));
            PanelHerramientasVehiculo.Visible = true;
        }

        protected void lnkDetalles_Click(object sender, EventArgs e)
        {
            lblSelectedCel.Text = "Detalles de Vehiculos <i class='fa fa-list-alt'></i>";
            lnkDetalles.CssClass = "btn btn-primary active";
            lnkRevision.CssClass = "btn btn-link";
            PanelHerramientasVehiculo.Visible = false;
            PanelRevision.Visible = false;
            PanelVehiculos.Visible = true;
            btnCancelar.Visible = false;
            btnGuardar.Visible = false;
            archi.Visible = false;
        }

        protected void lnkRevision_Click(object sender, EventArgs e)
        {
            lblSelectedCel.Text = "Detalles de Vehiculos <i class='fa fa-list-alt'></i>";
            lnkRevision.CssClass = "btn btn-primary active";
            lnkDetalles.CssClass = "btn btn-link";
            PanelHerramientasVehiculo.Visible = false;
            PanelRevision.Visible = true;
            PanelVehiculos.Visible = false;
            btnCancelar.Visible = true;
            btnGuardar.Visible = true;
            archi.Visible = true;
        }

        protected void repearVehiculos_rev_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            TextBox txtObservaciones = (TextBox)e.Item.FindControl("txtObservaciones");
            CheckBox cbx = (CheckBox)e.Item.FindControl("cbx");
            int revisado = Convert.ToInt32(DataBinder.Eval(dbr, "revisado"));
            if (revisado == 1)
            {
                cbx.Checked = true;
                cbx.Enabled = false;
                cbx.Text = Convert.ToString(DataBinder.Eval(dbr, "fecha")).ToUpper();
                txtObservaciones.Text = Convert.ToString(DataBinder.Eval(dbr, "observaciones")).ToUpper();
                txtObservaciones.ReadOnly = true;
            }
        }

        protected void repeat_vehiculos_herramientas_rev_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            TextBox txtObservaciones = (TextBox)e.Item.FindControl("txtObservaciones1");
            CheckBox cbx = (CheckBox)e.Item.FindControl("cbx1");
            int revisado = Convert.ToInt32(DataBinder.Eval(dbr, "revisado"));
            if (revisado == 1)
            {
                cbx.Checked = true;
                cbx.Enabled = false;
                cbx.Text = Convert.ToString(DataBinder.Eval(dbr, "fecha")).ToUpper();
                txtObservaciones.Text = Convert.ToString(DataBinder.Eval(dbr, "observaciones")).ToUpper();
                txtObservaciones.ReadOnly = true;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    Vehiculos_EntregaENT entidad = new Vehiculos_EntregaENT();
                    Vehiculos_EntregarCOM componente = new Vehiculos_EntregarCOM();
                    entidad.Pidc_entrega = Convert.ToInt32(Session["idc_entrega"]);
                    entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                    entidad.Pidc_puesto = Convert.ToInt32(Session["idc_puesto"].ToString());
                    entidad.Pidc_empleado = Convert.ToInt32(Session["idc_empleado"].ToString());
                    entidad.Cadveh = GeneraCadenaVehiculos();
                    entidad.Totalcadveh = TotalCadenaVehiculos();
                    entidad.Cadveh_herr = GeneraCadenaAccesoriosVehiculos_Herr();
                    entidad.Totalcadveh_herr = TotalCadenaVehiculos_Herr();
                    try
                    {
                        DataSet ds = componente.InsertarEntrega(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        DataTable dt2 = (DataTable)Session["formato_file"];
                        DataTable dt = ds.Tables[1];
                        //verificamos que no existan errores
                        string mensaje = row["mensaje"].ToString();
                        if (mensaje == "")//no hay errores retornamos true
                        {
                            foreach (DataRow roww in dt2.Rows)
                            {
                                foreach (DataRow rows in dt.Rows)
                                {
                                    if (rows["idc_vehiculo"].ToString() == roww["idc_vehiculo"].ToString())
                                    {
                                        if (roww["tipo"].ToString() == "Formato Revision")
                                        {
                                            File.Copy(roww["ruta"].ToString(), funciones.GenerarRuta("PEV_REV ", "unidad") + rows["ids_cons"].ToString() + ".jpg", true);
                                        }
                                        if (roww["tipo"].ToString() == "Carta Responsiva")
                                        {
                                            File.Copy(roww["ruta"].ToString(), funciones.GenerarRuta("PEV_RES ", "unidad") + rows["ids_cons"].ToString() + ".jpg", true);
                                        }
                                    }
                                }
                            }
                            Alert.ShowGiftMessage("Estamos procesando la cantidad de " + dt2.Rows.Count.ToString() + " archivo(s) al Servidor.", "Espere un Momento", "entregas.aspx", "imagenes/loading.gif", "3000", "La Entrega Total o Parcial fue guardada correctamente", this);
                        }
                        else
                        {//mostramos error
                            Alert.ShowAlertError(mensaje, this);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.ToString(), this.Page);
                        Global.CreateFileError(ex.ToString(), this);
                    }
                    break;

                case "Cancelar":
                    Response.Redirect("entregas.aspx");
                    break;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool error = false;
            //int CadenaCEL = TotalCadenaCelular();
            if (TotalCadenaVehiculos() == 0 && TotalCadenaVehiculos_Herr() == 0)
            {
                Alert.ShowAlertError("Para Guardar, debe Entregar al menos un Vehiculo.", this);
                error = true;
            }
            if (ComprobacionHerramientaVehiculo() == false)
            {
                Alert.ShowAlertError("Para Guardar, debe Entregar al menos un Vehiculo por herramienta", this);
                error = true;
            }
            DataTable dt = (DataTable)Session["formato_file"];
            //total de documentos cartas reponsivas

            //total de vehiculos
            int total = TotalVehiculos();
            DataView rv = dt.DefaultView;
            rv.RowFilter = "tipo = 'Carta Responsiva'";
            int responsiva = rv.ToTable().Rows.Count;
            if ((responsiva) < (total))
            {
                Alert.ShowAlertError("Debe subir el formato de responsiva para cada vehiculo asignado.", this);
                error = true;
            }
            //total de cartas revision
            int total_revision = 0;
            foreach (RepeaterItem item in RepeatVehiculos.Items)
            {
                LinkButton lnk = (LinkButton)item.FindControl("lnkDescargarFormatoAce");
                if (lnk.Visible == true) { total_revision = total_revision + 1; }
            }
            DataView rv2 = dt.DefaultView;
            rv2.RowFilter = "tipo = 'Formato Revision'";
            int revision = rv2.ToTable().Rows.Count;
            if ((revision) < (total_revision))
            {
                Alert.ShowAlertError("Debe subir el formato de revision para cada vehiculo en el que aplique", this);
                error = true;
            }

            if (error != true)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de guardar la Preparación. Solo se guardaran los Vehiculos o Herramientas que haya marcado, pero PODRA GUARDAR LAS DEMAS mas adelante. Todos sus datos son correctos?');", true);
            }
        }

        protected void lnkDescargarFormato_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string ruta = lnk.CommandName;
            string file = Path.GetFileName(ruta);
            if (File.Exists(ruta))
            {
                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file);
                // Escribimos el fichero a enviar
                Response.WriteFile(ruta);
                // volcamos el stream
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
            }
            else
            {
                Alert.ShowAlertError("No existe el archivo", this);
            }
        }

        protected void lnkDescargarFormatoAce_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            String url = HttpContext.Current.Request.Url.AbsoluteUri;
            String path_actual = url.Substring(url.LastIndexOf("/") + 1);
            url = url.Replace(path_actual, "");
            string idc_vehiculo = lnk.CommandName;
            int idc_puestoent = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
            int idc_entrega = Convert.ToInt32(Request.QueryString["idc_entrega"].ToString());

            url = url + "documento.aspx?idc_puestoent=" + idc_puestoent.ToString() + "&idc_entrega=" + idc_entrega.ToString() + "&idc_vehiculo=" + idc_vehiculo;
            ScriptManager.RegisterStartupScript(this, GetType(), "noti533W3", "window.open('" + url + "');", true);
        }

        protected void lnkGuardarPape_Click(object sender, EventArgs e)
        {
            string extension = Path.GetExtension(fupPapeleria.FileName);
            bool error = false;
            if (fupPapeleria.HasFile)
            {
                if (extension != ".jpg")
                {
                    error = true;
                    Alert.ShowAlertError("El formato de archivo debe ser IMAGEN .JPG", this);
                }
                if (ddlvehiculo.SelectedValue == "0")
                {
                    error = true;
                    Alert.ShowAlertError("Seleccione un vehiculo", this);
                }
                if (error == false)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/carta_resp/"));//path local
                    Random random = new Random();
                    int randominteger = random.Next(0, 10000000);
                    if (addtable(ddlvehiculo.SelectedValue, dirInfo + randominteger.ToString() + fupPapeleria.FileName, ddlvehiculo.SelectedItem.Text, "Carta Responsiva") == true)
                    {
                        funciones.UploadFile(fupPapeleria, dirInfo + randominteger.ToString() + fupPapeleria.FileName, this);
                        Alert.ShowGiftSimple("Estamos subiendo el archivo", "Espere un momento", "imagenes/loading.gif", "3000", this);
                    }
                }
            }
        }

        protected void lnkFrmato_Click(object sender, EventArgs e)
        {
            bool error = false;
            string extension = Path.GetExtension(fupFormato.FileName);
            if (fupFormato.HasFile)
            {
                if (extension != ".jpg")
                {
                    error = true;
                    Alert.ShowAlertError("El formato de archivo debe ser IMAGEN .JPG", this);
                }
                if (ddlvehiculo.SelectedValue == "0")
                {
                    error = true;
                    Alert.ShowAlertError("Seleccione un vehiculo", this);
                }
                if (error == false)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/formato_revision/"));//path local
                    Random random = new Random();
                    int randominteger = random.Next(0, 10000000);
                    if (addtable(ddlvehiculo.SelectedValue, dirInfo + randominteger.ToString() + fupFormato.FileName, ddlvehiculo.SelectedItem.Text, "Formato Revision") == true)
                    {
                        funciones.UploadFile(fupFormato, dirInfo + randominteger.ToString() + fupFormato.FileName, this);
                        Alert.ShowGiftSimple("Estamos subiendo el archivo", "Espere un momento", "imagenes/loading.gif", "3000", this);
                    }
                }
            }
        }

        private bool addtable(string id, string ruta, string vehiculo, string tipo)
        {
            DataTable dt = (DataTable)Session["formato_file"];
            foreach (DataRow roww in dt.Rows)
            {
                if (roww["idc_vehiculo"].ToString() == id && roww["tipo"].ToString() == tipo)
                {
                    roww.Delete();
                    break;
                }
            }
            DataRow row = dt.NewRow();
            row["idc_vehiculo"] = id;
            row["ruta"] = ruta;
            row["vehiculo"] = vehiculo;
            row["tipo"] = tipo;
            dt.Rows.Add(row);
            Session["formato_file"] = dt;
            gridPapeleria.DataSource = dt;
            gridPapeleria.DataBind();
            return true;
        }

        protected void gridPapeleria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            string ruta = gridPapeleria.DataKeys[index].Values["ruta"].ToString();
            string idc_vehiculo = gridPapeleria.DataKeys[index].Values["idc_vehiculo"].ToString();
            string tipo = gridPapeleria.DataKeys[index].Values["tipo"].ToString();
            string vehiculo = gridPapeleria.DataKeys[index].Values["vehiculo"].ToString();
            DataTable dt = (DataTable)Session["formato_file"];
            switch (e.CommandName)
            {
                case "Eliminar":
                    foreach (DataRow roww in dt.Rows)
                    {
                        if (roww["idc_vehiculo"].ToString() == idc_vehiculo)
                        {
                            roww.Delete();
                            break;
                        }
                    }
                    gridPapeleria.DataSource = dt;
                    gridPapeleria.DataBind();

                    break;

                case "Descargar":
                    string file = Path.GetFileName(ruta);
                    if (File.Exists(ruta))
                    {
                        // Limpiamos la salida
                        Response.Clear();
                        // Con esto le decimos al browser que la salida sera descargable
                        Response.ContentType = "application/octet-stream";
                        // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + file);
                        // Escribimos el fichero a enviar
                        Response.WriteFile(ruta);
                        // volcamos el stream
                        Response.Flush();
                        // Enviamos todo el encabezado ahora
                        Response.End();
                    }
                    else
                    {
                        Alert.ShowAlertError("No existe el archivo", this);
                    }
                    break;
            }
        }

        private bool Huella()
        {
            try
            {
                //NBioAPI m_NBioAPI = new NBioAPI();
                //short DeviceID = 255;
                //var ret = m_NBioAPI.OpenDevice(DeviceID);
                //if (ret == NBioAPI.Error.NONE)
                //{
                //}
                //else {
                //}

                //uint ret;
                //bool result;
                //NBioAPI.Type.FIR biFIR ="";
                //NBioAPI.Type.FIR_PAYLOAD myPayload = new NBioAPI.Type.FIR_PAYLOAD();
                //// Verify with binary FIR
                //ret = m_NBioAPI.Device(biFIR, out result, myPayload);
                //if (ret != NBioAPI.Error.NONE)
                //{
                //    // Verify Success

                //    // Check payload
                //    if (myPayload.Data != null)
                //    {
                //    }
                //}
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError("No se Encuentra Dispositivo para Leer Huella Digital " + ex.ToString(), this);
            }

            return true;
        }
    }
}