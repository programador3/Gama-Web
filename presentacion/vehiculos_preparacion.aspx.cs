using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class vehiculos_preparacion : System.Web.UI.Page
    {
        public string rutaimagen = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            //si nop trae valores regreso
            if (Request.QueryString["idc_puesto"] == null)
            {
                Response.Redirect("revisiones.aspx");
            }
            //bajo valores
            int idc_puesto = 0;
            int idc_usuario = 0;
            int idc_puestobaja = 0;
            if (Request.QueryString["preview"] == null)//si no viene del administrador
            {
                idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                string idc_pb = Request.QueryString["idc_puesto"].ToString();
                idc_puestobaja = Convert.ToInt32(idc_pb);
            }
            else
            {
                idc_puesto = Convert.ToInt32(Request.QueryString["idc_puesto"].ToString());
                idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_puestobaja = Convert.ToInt32(Request.QueryString["idc_puestobaja"].ToString());
                Alert.ShowAlertInfo("Esta pagina esta en modo informativo, NO PODRA guardar la Revisión. Si desea hacerlo, verifique el apartado de notificaciones", "Mensaje del sistema", this);
                lnkRevision.Visible = false;
            }
            if (!Page.IsPostBack)
            {
                //cargo datos a tablas
                CargarGridPrincipal(idc_puesto, idc_puestobaja);
            }
        }

        /// <summary>
        /// Carga los datos en Tablas de sesion y carga el grid principal
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal(int idc_puestoprep, int idc_puesto)
        {
            try
            {
                Vehiculos_PreparacionENT entidad = new Vehiculos_PreparacionENT();
                Vehiculos_PreparacionCOM componente = new Vehiculos_PreparacionCOM();
                entidad.Pidc_puesto = idc_puesto;
                entidad.Pidc_puestoprep = idc_puestoprep;
                DataSet ds = componente.CargaVehPrep(entidad);
                Session["Tabla_DatosEmpleado"] = ds.Tables[0];
                Session["Tabla_Vehiculos"] = ds.Tables[1];
                Session["Tabla_Vehiculos_Herr"] = ds.Tables[2];
                RepeatVehiculos.DataSource = ds.Tables[1];
                RepeatVehiculos.DataBind();
                repearVehiculos_rev.DataSource = ds.Tables[1];
                repearVehiculos_rev.DataBind();
                repeat_vehiculos_herramientas_rev.DataSource = ds.Tables[2];
                repeat_vehiculos_herramientas_rev.DataBind();
                DataRow row = ds.Tables[0].Rows[0];
                lblPuesto.Text = row["descripcion"].ToString();
                lblfechasoli.Text = row["fecha_registro"].ToString();
                Session["idc_prepara"] = Convert.ToInt32(row["idc_prepara"].ToString());
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
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

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
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
        }

        protected void RepeatVehiculos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //buscamos los controles dentro de repeat para asignar valores
            LinkButton lnkVerHVehiculos = (LinkButton)e.Item.FindControl("lnkVerHVehiculos");
            Image img = (Image)e.Item.FindControl("imgVehiculos");
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string idc_vehiculo = Convert.ToString(DataBinder.Eval(dbr, "idc_vehiculo"));
            //idc_behiculo => boton editar
            lnkVerHVehiculos.CommandName = idc_vehiculo;
            //generamos ruta de imagen
            GenerarRuta(Convert.ToInt32(idc_vehiculo), "VEHIC");
            img.ImageUrl = rutaimagen;
            rutaimagen = "";//limpiamos variable global
        }

        protected void lnkVerHVehiculos_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            //BAJO ID DEL ELEMENTO
            string idc_vehiculo = btn.CommandName.ToString();
            CargarGridVehiculosHerramientas(Convert.ToInt32(idc_vehiculo));
            PanelHerramientasVehiculo.Visible = true;
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

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":

                    try
                    {
                        Vehiculos_PreparacionENT entidad = new Vehiculos_PreparacionENT();
                        Vehiculos_PreparacionCOM componente = new Vehiculos_PreparacionCOM();
                        entidad.Idc_prepara = Convert.ToInt32(Session["idc_prepara"]);
                        entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                        entidad.Pidc_puestoprep = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                        entidad.Cadveh = GeneraCadenaVehiculos();
                        entidad.Totalcadveh = TotalCadenaVehiculos();
                        entidad.Cadveh_herr = GeneraCadenaAccesoriosVehiculos_Herr();
                        entidad.Totalcadveh_herr = TotalCadenaVehiculos_Herr();
                        DataSet ds = componente.InsertarPrep(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        string mensaje = row["mensaje"].ToString();
                        if (mensaje == "")//no hay errores retornamos true
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La Preparación Total o Parcial fue guardada correctamente.','preparaciones.aspx');", true);
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
                    Response.Redirect("preparaciones.aspx");
                    break;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool error = false;
            //int CadenaCEL = TotalCadenaCelular();
            if (TotalCadenaVehiculos() == 0 && TotalCadenaVehiculos_Herr() == 0)
            {
                Alert.ShowAlertError("Para Guardar, debe Preparar al menos un Activo.", this);
                error = true;
            }
            if (error != true)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de guardar la Preparación. Solo se guardaran los Vehiculos o Herramientas que haya marcado, pero PODRA GUARDAR LAS DEMAS mas adelante. Todos sus datos son correctos?');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
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
    }
}