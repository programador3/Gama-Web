using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class vehiculos_revision : System.Web.UI.Page
    {
        public string rutaimagen = "";//para controles dinamicos
        public int idc_usuarioprebaja = 0;
        public int idc_prebaja = 0;
        public int idc_revision = 0;
        public List<TextBox> list_textbox_nodinamics = new List<TextBox>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            //si nop trae valores regreso
            if (Request.QueryString["idc_puestoprebaja"] == null)
            {
                Response.Redirect("revisiones.aspx");
            }
            if (Request.QueryString["preview"] == null)//si no viene del administrador
            {
                //bajo valores
                int idc_puestoprebaja = Convert.ToInt32(Request.QueryString["idc_puestoprebaja"].ToString());
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_usuarioprebaja = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_puestorev = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                if (!Page.IsPostBack)
                {
                    //cargo datos a tablas
                    CargarGridPrincipal(idc_usuario, idc_puestorev, idc_puestoprebaja);
                }
                list_textbox_nodinamics.Add(txtCostoAccesorios);
                list_textbox_nodinamics.Add(txtCostoCarroceria);
                list_textbox_nodinamics.Add(txtCostoFocos);
                list_textbox_nodinamics.Add(txtCostoInterior);
                list_textbox_nodinamics.Add(txtCostollantas);
                list_textbox_nodinamics.Add(txtCostoPintura);
                list_textbox_nodinamics.Add(txtCostoVidrios);
                //genero datos del empleado
                GenerarDatosEmpleado(idc_puestoprebaja);
            }
            else
            {//VIENE DEL ADMINISTRADOR
                //bajo valores
                int idc_puestoprebaja = Convert.ToInt32(Request.QueryString["idc_puestoprebaja"].ToString());
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_usuarioprebaja = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_puestorev = Convert.ToInt32(Request.QueryString["idc_puestorev"].ToString());
                if (!Page.IsPostBack)
                {
                    //cargo datos a tablas
                    CargarGridPrincipal(idc_usuario, idc_puestorev, idc_puestoprebaja);
                    Alert.ShowAlertInfo("Esta pagina esta en modo informativo, NO PODRA guardar la Revisión. Si desea hacerlo, verifique el apartado de notificaciones", "Mensaje del sistema", this);
                }
                list_textbox_nodinamics.Add(txtCostoAccesorios);
                list_textbox_nodinamics.Add(txtCostoCarroceria);
                list_textbox_nodinamics.Add(txtCostoFocos);
                list_textbox_nodinamics.Add(txtCostoInterior);
                list_textbox_nodinamics.Add(txtCostollantas);
                list_textbox_nodinamics.Add(txtCostoPintura);
                list_textbox_nodinamics.Add(txtCostoVidrios);
                //genero datos del empleado
                GenerarDatosEmpleado(idc_puestoprebaja);
                btnGuardar.Visible = false;
            }
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        /// <summary>
        /// Carga los datos en Tablas de sesion y carga el grid principal
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal(int idc_usuario, int idc_puestorevi, int idc_puestoprebaja)
        {
            try
            {
                Vehiculos_RevisionENT entidad = new Vehiculos_RevisionENT();
                Vehiculos_RevisionCOM componente = new Vehiculos_RevisionCOM();
                entidad.Idc_puestorevi = idc_puestorevi;
                entidad.Idc_puestoprebaja = idc_puestoprebaja;
                DataSet ds = componente.CargaVehiculosRevision(entidad);
                Session["Tabla_DatosEmpleado"] = ds.Tables[0];
                RepeatVehiculos.DataSource = ds.Tables[1];
                RepeatVehiculos.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga los datos en Tablas de sesion y carga el grid principal
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarRevisionesVehiculos(int idc_vehiculo, int idc_puestorevi, int idc_puestoprebaja)
        {
            try
            {
                Vehiculos_RevisionENT entidad = new Vehiculos_RevisionENT();
                Vehiculos_RevisionCOM componente = new Vehiculos_RevisionCOM();
                entidad.Idc_puestorevi = idc_puestorevi;
                entidad.Idc_puestoprebaja = idc_puestoprebaja;
                DataSet ds = componente.CargaVehiculosRevision(entidad);
                Session["Tabla_DatosEmpleado"] = ds.Tables[0];
                //Session["Tabla_Vehiculos"] = ds.Tables[1];
                //Session["Tabla_Vehiculos_herr"] = ds.Tables[2];
                DataTable table_vehiculo = new DataTable();
                table_vehiculo.Columns.Add("idc_vehiculo");
                table_vehiculo.Columns.Add("descripcion_vehiculo");
                table_vehiculo.Columns.Add("placas");
                table_vehiculo.Columns.Add("num_economico");
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    if (Convert.ToInt32(row["idc_vehiculo"]) == idc_vehiculo)
                    {
                        DataRow new_row = table_vehiculo.NewRow();
                        new_row["idc_vehiculo"] = row["idc_vehiculo"];
                        new_row["descripcion_vehiculo"] = row["descripcion_vehiculo"];
                        new_row["placas"] = row["placas"];
                        new_row["num_economico"] = row["num_economico"];
                        table_vehiculo.Rows.Add(new_row);
                    }
                }
                Repeat_Revision.DataSource = table_vehiculo;
                Repeat_Revision.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }

            //if (ds.Tables[2].Rows.Count == 0) { NonHerramientas.Visible = true; }
        }

        /// <summary>
        /// Carga el grid de gerramientas por vehiculo
        /// </summary>
        /// <param name="idc_puesto"></param>
        public void CargarGridVehiculosHerramientas(int idc_vehiculo)
        {
            try
            {
                herramientasENT entidad = new herramientasENT();
                entidad.Idc_vehiculo = idc_vehiculo;
                herramientasCOM componente = new herramientasCOM();
                DataSet ds = componente.CargaHerramientasVehculo(entidad);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    PanelconHVehiculo.Visible = false;
                    PanelsinHVehiculo.Visible = true;
                }
                Session["Tabla_HerramientasVehiculo"] = ds.Tables[0];
                gridHerramientasVehiculo.DataSource = ds.Tables[0];
                gridHerramientasVehiculo.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Genera los datos personales del empleado actual(prebaja)
        /// </summary>
        /// <param name="idc_puestoprebaja"></param>
        private void GenerarDatosEmpleado(int idc_puestoprebaja)
        {
            DataTable tabla = (DataTable)Session["Tabla_DatosEmpleado"];
            //si el id de empleado es igaul saco los datos
            foreach (DataRow row in tabla.Rows)
            {
                if (Convert.ToInt32(row["IDC_PUESTO"]) == idc_puestoprebaja)
                {
                    lblNombre.Text = row["empleado"].ToString();
                    lblPuesto.Text = row["descripcion"].ToString();
                    lblnomina.Text = row["num_nomina"].ToString();
                    lblmotivo.Text = row["motivo"].ToString();
                    idc_prebaja = Convert.ToInt32(row["idc_prebaja"].ToString());
                    GenerarRuta(Convert.ToInt32(row["idc_empleado"].ToString()), "fot_emp");
                }
            }
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

                var unidad = row["unidad"].ToString();
                var domn = Request.Url.Host;
                switch (codigo_imagen)
                {
                    case "fot_emp"://fotos de empleados
                        if (domn == "localhost")
                        {
                            var url = "imagenes/btn/default_employed.png";
                            imgEmpleado.ImageUrl = url;
                        }
                        else
                        {
                            var url = "http://" + domn + carpeta + id_comprobar + ".jpg";
                            ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                            imgEmpleado.ImageUrl = url;
                        }
                        break;

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

                    case "TIPCAM"://fotos de vehiculos
                        var urlt = "http://" + domn + carpeta + id_comprobar + ".pdf";
                        rutaimagen = urlt;
                        break;

                    case "REVHERV"://imagen scaneo formato revision
                        var url_upload = unidad;
                        rutaimagen = url_upload;
                        break;

                    case "FORREV"://FORMATO DE REVISION DE VEHICULOS
                        var up = unidad;
                        rutaimagen = up;
                        rutaimagen = up;
                        break;
                }
            }
        }

        protected void RepeatVehiculos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //buscamos los controles dentro de repeat para asignar valores
            LinkButton lnkVerHVehiculos = (LinkButton)e.Item.FindControl("lnkVerHVehiculos");
            LinkButton lnkRevision = (LinkButton)e.Item.FindControl("lnkRevision");
            Image img = (Image)e.Item.FindControl("imgVehiculos");
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string idc_vehiculo = Convert.ToString(DataBinder.Eval(dbr, "idc_vehiculo"));
            string idc_vehiculorev = Convert.ToString(DataBinder.Eval(dbr, "idc_formatorev"));
            int revisado = Convert.ToInt32(DataBinder.Eval(dbr, "idc_vehiculo_revisado"));
            //idc_behiculo => boton editar
            lnkVerHVehiculos.CommandName = idc_vehiculo;
            lnkRevision.CommandName = idc_vehiculo;
            lnkRevision.CommandArgument = idc_vehiculorev;
            //generamos ruta de imagen
            GenerarRuta(Convert.ToInt32(idc_vehiculo), "VEHIC");
            img.ImageUrl = rutaimagen;
            rutaimagen = "";//limpiamos variable global
            if (Request.QueryString["preview"] != null)//si no viene del administrador
            {
                lnkRevision.Visible = false;
            }
            if (revisado > 0)
            {
                lnkRevision.Visible = false;
            }
        }

        protected void lnkVerHVehiculos_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            //BAJO ID DEL ELEMENTO
            string idc_vehiculo = btn.CommandName.ToString();
            CargarGridVehiculosHerramientas(Convert.ToInt32(idc_vehiculo));
            PanelHerramientasVehiculo.Visible = true;

            ScriptManager.RegisterStartupScript(this, GetType(), "gogridherr", "GoSection('" + btn.ClientID.ToString() + "');", true);
        }

        protected void lnkRevision_Click(object sender, EventArgs e)
        {
            lblSelectedCel.Text = "Revisión de Vehiculos y Herramientas <i class='fa fa-check-square-o'></i>";
            PanelVehiculos.Visible = false;
            PanelRevisaVehiculos.Visible = true;
            PanelRevision_herr.Visible = true;
            PanelHerramientasVehiculo.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
            LinkButton btn = (LinkButton)sender;
            //BAJO ID DEL ELEMENTO
            int idc_vehiculo = Convert.ToInt32(btn.CommandName.ToString());
            int idc_puestoprebaja = Convert.ToInt32(Request.QueryString["idc_puestoprebaja"].ToString());
            int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
            int idc_puestorev = Convert.ToInt32(Session["sidc_puesto_login"].ToString());

            CargarRevisionesVehiculos(idc_vehiculo, idc_puestorev, idc_puestoprebaja);
            lblidc_vehiculo.Text = btn.CommandName.ToString();
            //cargamos hheramientas del idc vehiculo
            herramientasENT entidad = new herramientasENT();
            entidad.Idc_vehiculo = idc_vehiculo;
            herramientasCOM componente = new herramientasCOM();
            DataSet ds = componente.CargaHerramientasVehculo(entidad);
            Session["Tabla_Vehiculos_herr"] = ds.Tables[0];
            repeat_herramientas.DataSource = ds.Tables[0];
            repeat_herramientas.DataBind();
            if (ds.Tables[0].Rows.Count == 0) { NonHerramientas.Visible = true; }
            int idc_formatorev = Convert.ToInt32(btn.CommandArgument.ToString());
            GenerarRuta(idc_formatorev, "FORREV");
            if (idc_formatorev == 0)
            {
                lnkDescargarFormato.Visible = false;
                fileupload.Visible = false;
                lblFileUP.Visible = false;
                REV.Enabled = false;
                RFV.Enabled = false;
                //lnkDescargarFormato.CommandName = rutaimagen;
            }
            else
            {
                lnkDescargarFormato.Visible = true;
                GenerarRuta(idc_formatorev, "FORREV");
                lnkDescargarFormato.CommandName = GenerarDoc(idc_formatorev.ToString() + ".pdf"); ;
                //lnkDescargarFormato.CommandName = rutaimagen;
            }
            PanelPiedePagina.Visible = true;
        }

        protected void lnkDetalles_Click(object sender, EventArgs e)
        {
            lblSelectedCel.Text = "Detalles de Vehiculos y Herramientas <i class='fa fa-list-alt'></i>";
            PanelVehiculos.Visible = true;
            PanelRevision_herr.Visible = false;
            PanelRevisaVehiculos.Visible = false;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            lnkDescargarFormato.Visible = false;
            PanelPiedePagina.Visible = false;
        }

        /// <summary>
        /// Validamos que si cel CBX etsa en true el TextBox debe permitir ingresar un costo
        /// </summary>
        /// <param name="cbx"></param>
        /// <param name="txt"></param>
        public void ValidateCheckBoxandText(CheckBox cbx, TextBox txt)
        {
            if (cbx.Checked == true) { txt.ReadOnly = false; txt.Text = "0.00"; }
            if (cbx.Checked == false) { txt.ReadOnly = true; }
        }

        /// <summary>
        /// Valida que si hay daños, debe existir un comentario
        /// </summary>
        /// <param name="cbx"></param>
        /// <param name="txt"></param>
        public Boolean ValidateComenatrios()
        {
            Boolean status = false;
            if (cbxAccesorios.Checked == true)
            {
                if (txtDescripcionAccesorios.Text == "" || txtDescripcionAccesorios.Equals(string.Empty))
                {
                    Alert.ShowAlertError("Si existe daño en el vehiculo, debe marcar un comentario", this);
                    txtDescripcionAccesorios.Focus();
                    status = true;
                }
            }
            if (cbxCarroceria.Checked == true)
            {
                if (txtObservacionesCarroceria.Text == "" || txtObservacionesCarroceria.Equals(string.Empty))
                {
                    Alert.ShowAlertError("Si existe daño en el vehiculo, debe marcar un comentario", this);
                    txtObservacionesCarroceria.Focus();
                    status = true;
                }
            }
            if (cbxFocos.Checked == true)
            {
                if (txtObservacionesFocos.Text == "" || txtObservacionesFocos.Equals(string.Empty))
                {
                    Alert.ShowAlertError("Si existe daño en el vehiculo, debe marcar un comentario", this);
                    txtObservacionesFocos.Focus();
                    status = true;
                }
            }
            if (cbxInterior.Checked == true)
            {
                if (txtObservacionesInterior.Text == "" || txtObservacionesInterior.Equals(string.Empty))
                {
                    Alert.ShowAlertError("Si existe daño en el vehiculo, debe marcar un comentario", this);
                    txtObservacionesInterior.Focus();
                    status = true;
                }
            }
            if (cbxLlantas.Checked == true)
            {
                if (txtComentariosllantas.Text == "" || txtComentariosllantas.Equals(string.Empty))
                {
                    Alert.ShowAlertError("Si existe daño en el vehiculo, debe marcar un comentario", this);
                    txtComentariosllantas.Focus();
                    status = true;
                }
            }
            if (cbxPintura.Checked == true)
            {
                if (txtObservacionesPintura.Text == "" || txtObservacionesPintura.Equals(string.Empty))
                {
                    Alert.ShowAlertError("Si existe daño en el vehiculo, debe marcar un comentario", this);
                    txtObservacionesPintura.Focus();
                    status = true;
                }
            }
            if (cbxVidrios.Checked == true)
            {
                if (txtObservacionesVidrios.Text == "" || txtObservacionesVidrios.Equals(string.Empty))
                {
                    Alert.ShowAlertError("Si existe daño en el vehiculo, debe marcar un comentario", this);
                    txtObservacionesVidrios.Focus();
                    status = true;
                }
            }
            return status;
        }

        /// <summary>
        /// Valida que si un cbx esta en check el txt deba contener una cantidad mayor que 0 cero
        /// </summary>
        /// <param name="cbx"></param>
        /// <param name="txt"></param>
        /// <returns></returns>
        public Boolean ValidaCosto(CheckBox cbx, TextBox txt)
        {
            if (cbx.Checked == true)
            {
                Decimal cantidad = Convert.ToDecimal(txt.Text);
                if (cantidad <= 0 | cantidad == 0)
                {
                    Alert.ShowAlertError("Si existe algun daño debe tener un costo de aproximación MAYOR A 0 CERO", this);
                    txt.Focus();
                    return true;
                }
                txt.Focus();
            }
            return false;
        }

        /// <summary>
        /// Genera cadena con resultado de Equipos Celulares
        /// </summary>
        /// <returns></returns>
        public string GeneraCadenaHerramientas()
        {
            string list = null;
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            foreach (RepeaterItem item in repeat_herramientas.Items)
            {
                TextBox txtcantidad = (TextBox)item.FindControl("txtCantidadSistema");
                TextBox txtreviso = (TextBox)item.FindControl("txtCantidadReal");
                TextBox txtcosto = (TextBox)item.FindControl("txtCostoHerramienta");
                Label lbltipoherramientas = (Label)item.FindControl("lbltipoherramientas");
                //TextBox txtComentariosHerramientas = (TextBox)item.FindControl("txtComentariosHerramientas");
                list = list + (lbltipoherramientas.Text + ";" + txtcantidad.Text + ";" + txtreviso.Text + ";" + txtcosto.Text + ";");
            }
            return list;
        }

        protected void txtMoney_TextChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeat_herramientas.Items)
            {
                TextBox txtCantidadSistema = (TextBox)item.FindControl("txtCantidadSistema");
                TextBox txtCantidadReal = (TextBox)item.FindControl("txtCantidadReal");
                TextBox txtMoney = (TextBox)item.FindControl("txtCostoHerramienta");
                Label lblerrorcostoherr = (Label)item.FindControl("lblerrorcostoherr");
                lblerrorcostoherr.Visible = false;
                decimal cantidad = Convert.ToDecimal(txtMoney.Text);
                if (cantidad < 0)
                {
                    lblerrorcostoherr.Visible = true;
                    lblerrorcostoherr.Text = "No puede colocar numeros negativos.";
                }
                if (Convert.ToDecimal(txtCantidadReal.Text) < Convert.ToDecimal(txtCantidadSistema.Text))
                {
                    if (cantidad <= 0)
                    {
                        lblerrorcostoherr.Visible = true;
                        lblerrorcostoherr.Text = "Debe colocar un costo real.";
                    }
                    else
                    {
                        SumaCosto(list_textbox_nodinamics);
                    }
                }
            }
            SumaCosto(list_textbox_nodinamics);
        }

        protected void cbxPintura_CheckedChanged(object sender, EventArgs e)
        {
            ValidateCheckBoxandText(cbxPintura, txtCostoPintura);
        }

        protected void cbxLlantas_CheckedChanged(object sender, EventArgs e)
        {
            ValidateCheckBoxandText(cbxLlantas, txtCostollantas);
        }

        protected void cbxAccesorios_CheckedChanged(object sender, EventArgs e)
        {
            ValidateCheckBoxandText(cbxAccesorios, txtCostoAccesorios);
        }

        protected void cbxCarroceria_CheckedChanged(object sender, EventArgs e)
        {
            ValidateCheckBoxandText(cbxCarroceria, txtCostoCarroceria);
        }

        protected void cbxInterior_CheckedChanged(object sender, EventArgs e)
        {
            ValidateCheckBoxandText(cbxInterior, txtCostoInterior);
        }

        protected void cbxVidrios_CheckedChanged(object sender, EventArgs e)
        {
            ValidateCheckBoxandText(cbxVidrios, txtCostoVidrios);
        }

        protected void cbxFocos_CheckedChanged(object sender, EventArgs e)
        {
            ValidateCheckBoxandText(cbxFocos, txtCostoFocos);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    try
                    {
                        DataTable table = (DataTable)Session["Tabla_Vehiculos_herr"];
                        Vehiculos_RevisionENT entidad = new Vehiculos_RevisionENT();
                        Vehiculos_RevisionCOM componente = new Vehiculos_RevisionCOM();
                        entidad.PIDC_prebaja = idc_prebaja;//ID DE PREBAJA
                        entidad.Idc_usuario = idc_usuarioprebaja;//USUARIO QUE REALIZA LA PREBAJA
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.PCADHERR = GeneraCadenaHerramientas();//CADENA DE HERRAMIENTAS Y RESULTADO
                        entidad.PNUMHERR = table.Rows.Count;//Numero de herramientas
                                                            //genero la suma de todo
                        list_textbox_nodinamics.Add(txtCostoAccesorios);
                        list_textbox_nodinamics.Add(txtCostoCarroceria);
                        list_textbox_nodinamics.Add(txtCostoFocos);
                        list_textbox_nodinamics.Add(txtCostoInterior);
                        list_textbox_nodinamics.Add(txtCostollantas);
                        list_textbox_nodinamics.Add(txtCostoPintura);
                        list_textbox_nodinamics.Add(txtCostoVidrios);
                        SqlMoney costo = SqlMoney.Parse(SumaCosto(list_textbox_nodinamics).ToString());
                        entidad.PMONTO = costo;
                        entidad.Idc_vehiculo = Convert.ToInt32(lblidc_vehiculo.Text);//id de vehiculo
                                                                                     //buenas condiciones
                        int buenas_condiciones = 0;
                        if (cbxBuenasCondiciones.Checked == true) { buenas_condiciones = 1; }
                        entidad.Buenas_condiciones = buenas_condiciones;//buenas condiciones?'
                                                                        //
                                                                        //verificamos los cbx estaticos
                                                                        //PINTURA
                        int pintura = 0;
                        if (cbxPintura.Checked == true) { pintura = 1; }
                        entidad.Pintura = pintura;//PINTURA DAÑADA
                        SqlMoney Pintura_costo = SqlMoney.Parse(txtCostoPintura.Text);
                        entidad.Pintura_costo = Pintura_costo;//pintura costo
                        entidad.Pintura_obs = txtObservacionesPintura.Text.ToUpper();//observaciones de pintura
                                                                                     //LLANTAS
                        int llantas = 0;
                        if (cbxLlantas.Checked == true) { llantas = 1; }
                        entidad.Llantas = llantas;//LLANTAS DAÑADAS?
                        SqlMoney llantas_costo = SqlMoney.Parse(txtCostollantas.Text);
                        entidad.Llantas_costo = llantas_costo;//costo
                        entidad.Llantas_obs = txtComentariosllantas.Text.ToUpper();//observaciones
                                                                                   //ACCESORIOS
                        int accesorios = 0;
                        if (cbxAccesorios.Checked == true) { accesorios = 1; }
                        entidad.Accesorios = accesorios;// DAÑADAS?
                        SqlMoney accesorios_costo = SqlMoney.Parse(txtCostoAccesorios.Text);
                        entidad.Accesorios_costo = accesorios_costo;//costo
                        entidad.Accesorios_obs = txtDescripcionAccesorios.Text.ToUpper();//observaciones
                                                                                         //CARROCERIA
                        int carroceria = 0;
                        if (cbxCarroceria.Checked == true) { carroceria = 1; }
                        entidad.Carroceria = carroceria;//DAÑADAS?
                        SqlMoney carroceria_costo = SqlMoney.Parse(txtCostoCarroceria.Text);
                        entidad.Carroceria_costo = carroceria_costo;//costo
                        entidad.Carroceria_obs = txtObservacionesCarroceria.Text.ToUpper();//observaciones
                                                                                           //CARROCERIA
                        int interior = 0;
                        if (cbxInterior.Checked == true) { interior = 1; }
                        entidad.Interior = interior;//DAÑADAS?
                        SqlMoney interior_costo = SqlMoney.Parse(txtCostoInterior.Text);
                        entidad.Interior_costo = interior_costo;//costo
                        entidad.Interior_obs = txtObservacionesInterior.Text.ToUpper();//observaciones
                                                                                       //VIDRIOS
                        int vidrios = 0;
                        if (cbxVidrios.Checked == true) { vidrios = 1; }
                        entidad.Vidrios = vidrios;//DAÑADAS?
                        SqlMoney vidrios_costo = SqlMoney.Parse(txtCostoVidrios.Text);
                        entidad.Vidrios_costo = vidrios_costo;//costo
                        entidad.Vidrios_obs = txtObservacionesVidrios.Text.ToUpper();//observaciones
                                                                                     //FOCOS
                        int focos = 0;
                        if (cbxFocos.Checked == true) { focos = 1; }
                        entidad.Focos = focos;//DAÑADAS?
                        SqlMoney focos_costo = SqlMoney.Parse(txtCostoFocos.Text);
                        entidad.Focos_costo = focos_costo;//costo
                        entidad.Focos_obs = txtObservacionesFocos.Text.ToUpper();//observaciones
                        DataSet ds = componente.InsertarRevisionVehiculos(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        string mensaje = row["mensaje"].ToString();
                        if (mensaje == "")//no hay errores retornamos true
                        {
                            DataRow row_rew = ds.Tables[0].Rows[0];
                            //verificamos que no existan errores
                            idc_revision = Convert.ToInt32(row_rew["idc_revision"].ToString());
                            GenerarRuta(idc_revision, "REVHERV");
                            bool status_upload = UploadFile(rutaimagen, row_rew["idc_revision"].ToString() + ".jpg");
                            if (fileupload.Visible == true)
                            {
                                if (!status_upload == false)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La revisión fue guardada correctamente.','revisiones.aspx');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La revisión fue guardada correctamente.','revisiones.aspx');", true);
                            }
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
            }
        }

        /// <summary>
        /// Genera el costo total acumulado
        /// </summary>
        public decimal SumaCosto(List<TextBox> listtxt)
        {
            decimal Total = 0;

            foreach (TextBox txtMoney in listtxt)
            {
                if (txtMoney.Text != "")
                {
                    decimal cantidad = Convert.ToDecimal(txtMoney.Text);
                    if (cantidad < 0)
                    {
                        Alert.ShowAlertError("El Costo NO PUEDE TENER un valor negativo.", this);
                        txtMoney.Focus();
                    }
                    Total = Total + cantidad;
                }
            }
            list_textbox_nodinamics.Clear();
            foreach (RepeaterItem item in repeat_herramientas.Items)
            {
                TextBox txtMoney = (TextBox)item.FindControl("txtCostoHerramienta");
                if (txtMoney.Text != "")
                {
                    decimal cantidad = Convert.ToDecimal(txtMoney.Text);
                    if (cantidad < 0)
                    {
                        Alert.ShowAlertError("El Costo NO PUEDE TENER un valor negativo.", this);
                        txtMoney.Focus();
                    }
                    Total = Total + cantidad;
                }
            }
            txtTotal.Text = Total.ToString();
            return Total;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool error_herr = false;
            bool error_costo = false;
            bool error = ValidaCosto(cbxAccesorios, txtCostoAccesorios);
            bool error2 = ValidaCosto(cbxPintura, txtCostoPintura);
            bool error3 = ValidaCosto(cbxLlantas, txtCostollantas);
            bool error4 = ValidaCosto(cbxCarroceria, txtCostoCarroceria);
            bool error5 = ValidaCosto(cbxInterior, txtCostoInterior);
            bool error6 = ValidaCosto(cbxVidrios, txtCostoVidrios);
            bool error7 = ValidaCosto(cbxFocos, txtCostoFocos);
            bool error_comentarios = ValidateComenatrios();
            foreach (RepeaterItem item in repeat_herramientas.Items)
            {
                Label lblcosto = (Label)item.FindControl("lblcosto_herr");
                Label lblerrorCantidadReal = (Label)item.FindControl("lblerrorCantidadReal");
                TextBox txtCantidadSistema = (TextBox)item.FindControl("txtCantidadSistema");
                TextBox txtCantidadReal = (TextBox)item.FindControl("txtCantidadReal");
                TextBox txtMoney = (TextBox)item.FindControl("txtCostoHerramienta");
                TextBox txt = (TextBox)item.FindControl("txt");
                decimal cantidad = Convert.ToDecimal(txtCantidadReal.Text);
                lblerrorCantidadReal.Visible = false;
                if (cantidad < 0)
                {
                    lblerrorCantidadReal.Visible = true;
                    error_costo = true;
                }
            }
            foreach (RepeaterItem item in repeat_herramientas.Items)
            {
                TextBox txtCantidadSistema = (TextBox)item.FindControl("txtCantidadSistema");
                TextBox txtCantidadReal = (TextBox)item.FindControl("txtCantidadReal");
                TextBox txtMoney = (TextBox)item.FindControl("txtCostoHerramienta");
                Label lblerrorcostoherr = (Label)item.FindControl("lblerrorcostoherr");
                lblerrorcostoherr.Visible = false;
                if (Convert.ToDecimal(txtCantidadReal.Text) < Convert.ToDecimal(txtCantidadSistema.Text))
                {
                    decimal cantidad = Convert.ToDecimal(txtMoney.Text);
                    if (cantidad <= 0)
                    {
                        txtMoney.Text = "0.00";
                        lblerrorcostoherr.Visible = true;
                        lblerrorcostoherr.Text = "Debe colocar un costo real.";
                        error_herr = true;
                    }
                }
            }

            if (error_costo == true || error_herr == true || error_comentarios == true || error == true || error2 == true || error3 == true || error4 == true || error5 == true || error6 == true || error7 == true)
            {
                Alert.ShowAlertError("Existen uno o mas errores. Verifique en el formulario los errores MARCADOS EN COLOR ROJO.", this);
            }
            else//no hay error
            {
                Session["Caso_Confirmacion"] = "Guardar";
                string d = SumaCosto(list_textbox_nodinamics).ToString();
                string mensaje = SumaCosto(list_textbox_nodinamics).ToString() == "0.00" ? "¿Esta a punto de guardar la Revisión. Si esto sucede no podra ser modificada. Todos sus datos son correctos?" : "¿Esta a punto de guardar la Revisión Y Generar un vale por un total de " + SumaCosto(list_textbox_nodinamics).ToString() + ". Si esto sucede no podra ser modificada. Todos sus datos son correctos?";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','" + mensaje + "');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string idc_puestoprebaja = (Request.QueryString["idc_puestoprebaja"].ToString());
            Response.Redirect("vehiculos_revision.aspx?idc_puestoprebaja=" + idc_puestoprebaja);
        }

        protected void txtCantidadReal_TextChanged(object sender, EventArgs e)
        {
            TextBox txtMoneysend = (TextBox)sender;

            foreach (RepeaterItem item in repeat_herramientas.Items)
            {
                Label lblcosto = (Label)item.FindControl("lblcosto_herr");
                Label lblerrorCantidadReal = (Label)item.FindControl("lblerrorCantidadReal");
                TextBox txtCantidadSistema = (TextBox)item.FindControl("txtCantidadSistema");
                TextBox txtCantidadReal = (TextBox)item.FindControl("txtCantidadReal");
                TextBox txtMoney = (TextBox)item.FindControl("txtCostoHerramienta");
                TextBox txt = (TextBox)item.FindControl("txt");
                decimal cantidad = Convert.ToDecimal(txtCantidadReal.Text);
                lblerrorCantidadReal.Visible = false;
                if (Convert.ToInt32(txtCantidadReal.Text) == Convert.ToInt32(txtCantidadSistema.Text))
                {
                    txtMoney.Text = "0.00";
                }
                else
                {
                    if (Convert.ToDecimal(lblcosto.Text) == 0) { txtMoney.Text = "1.00"; }
                    else { txtMoney.Text = lblcosto.Text; }
                }
                if (cantidad < 0)
                {
                    lblerrorCantidadReal.Visible = true;
                }
            }
            SumaCosto(list_textbox_nodinamics);
        }

        protected void lnkVerTotal_Click(object sender, EventArgs e)
        {
            SumaCosto(list_textbox_nodinamics);
        }

        public bool UploadFile(String ruta, string namefile)
        {
            try
            {
                fileupload.PostedFile.SaveAs(ruta + namefile);
                return true;
            }
            catch (Exception ex)
            {
                lblFileUP.Text = ex.ToString();
                return false;
            }
        }

        public void DownloadFile()
        {
        }

        public string GenerarDoc(string filename)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/"));
            if (File.Exists(rutaimagen + filename))
            {
                File.Copy(rutaimagen + filename, dirInfo + filename, true);
                return dirInfo + filename;
            }
            else
            {
                return "";
            }
        }

        protected void lnkDescargarFormato_Click(object sender, EventArgs e)
        {
            string pdf = "";
            // Limpiamos la salida
            Response.Clear();
            // Con esto le decimos al browser que la salida sera descargable
            Response.ContentType = "application/octet-stream";
            // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
            Response.AddHeader("Content-Disposition", "attachment; filename=Revision.pdf");
            // Escribimos el fichero a enviar
            Response.WriteFile(lnkDescargarFormato.CommandName.ToString());
            // volcamos el stream
            Response.Flush();
            // Enviamos todo el encabezado ahora
            Response.End();
        }
    }
}