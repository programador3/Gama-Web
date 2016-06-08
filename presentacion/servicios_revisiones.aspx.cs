using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class servicios_revisiones : System.Web.UI.Page
    {
        public string rutaimagen = "";//para controles dinamicos
        public int idc_usuarioprebaja = 0;
        public int idc_prebaja = 0;
        public int idc_revision = 0;
        public int idc_revisionser = 0;
        private int idc_puestoprebaja = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            // si nop trae valores regreso
            if (Request.QueryString["idc_puestoprebaja"] == null)
            {
                Response.Redirect("revisiones.aspx");
            }
            if (Request.QueryString["preview"] == null)//si no viene del administrador
            {
                //bajo valores
                idc_puestoprebaja = Convert.ToInt32(Request.QueryString["idc_puestoprebaja"].ToString());
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_usuarioprebaja = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_puestorev = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                if (!Page.IsPostBack)
                {
                    //cargo datos a tablas
                    CargarGridPrincipal(idc_usuario, idc_puestorev, idc_puestoprebaja);
                }
                GenerarDatosEmpleado(idc_puestoprebaja);
                //generamos validacion
            }
            else
            {
                //bajo valores
                idc_puestoprebaja = Convert.ToInt32(Request.QueryString["idc_puestoprebaja"].ToString());
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_usuarioprebaja = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_puestorev = Convert.ToInt32(Request.QueryString["idc_puestorev"].ToString());
                if (!Page.IsPostBack)
                {
                    //cargo datos a tablas
                    CargarGridPrincipal(idc_usuario, idc_puestorev, idc_puestoprebaja);
                    Alert.ShowAlertInfo("Esta pagina esta en modo informativo, NO PODRA guardar la Revisión. Si desea hacerlo, verifique el apartado de notificaciones", "Mensaje del sistema", this);
                }
                GenerarDatosEmpleado(idc_puestoprebaja);
                btnAceptar.Visible = false;
                btnCancelar.Visible = false;
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
                Servicioes_RevisionENT entidad = new Servicioes_RevisionENT();
                Servicios_RevisionCOM componente = new Servicios_RevisionCOM();
                entidad.Pidc_puesto_prerevisa = idc_puestorevi;
                entidad.Pidc_puestoprebaja = idc_puestoprebaja;
                DataSet ds = componente.CargaVehiculosRevision(entidad);
                Session["Tabla_DatosEmpleado"] = ds.Tables[0];
                Session["Tabla_DatosServicio"] = ds.Tables[1];
                Session["Tabla_DatosDetalles"] = ds.Tables[2];
                RepeaterServicios.DataSource = ds.Tables[0];
                RepeaterServicios.DataBind();
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
            DataTable tabla_servicios = (DataTable)Session["Tabla_DatosServicio"];
            DataRow row_ser = tabla_servicios.Rows[0];
            lblNombreEncargado.Text = row_ser["empleado"].ToString();
            //txtDescSer.Text= row_ser["descripcion"].ToString();
            //idc_revisionser=Convert.ToInt32(row_ser["idc_revisionser"].ToString());
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
                }
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

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Boolean status = false;
            foreach (RepeaterItem item in RepeaterServicios.Items)
            {
                Label lblerrorobsr = (Label)item.FindControl("lblerrorobsr");
                Label lblerrorCantidadReal = (Label)item.FindControl("lblerrorCantidadReal");
                TextBox txtCosto = (TextBox)item.FindControl("txtCosto");
                TextBox txtObservaciones = (TextBox)item.FindControl("txtObservaciones");
                decimal cantidad = Convert.ToDecimal(txtCosto.Text);
                if (txtCosto.Visible == true)
                {
                    if (cantidad < 0)
                    {
                        lblerrorCantidadReal.Visible = true;
                        status = true;
                        Alert.ShowAlertError("Debe colocar un Costo real y no Negativo.", this);
                        break;
                    }
                    if (txtObservaciones.Text == string.Empty)
                    {
                        lblerrorobsr.Visible = true;
                        status = true;
                        Alert.ShowAlertError("Escriba una Observación.", this);
                        break;
                    }
                }
            }
            if (status != true)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                int t = 0;
                foreach (RepeaterItem item in RepeaterServicios.Items)
                {
                    TextBox idc_reviser = (TextBox)item.FindControl("idc_reviser");
                    TextBox txtCosto = (TextBox)item.FindControl("txtCosto");
                    TextBox txtObservaciones = (TextBox)item.FindControl("txtObservaciones");
                    decimal g = txtCosto.Text == "" ? 0 : Convert.ToDecimal(txtCosto.Text);
                    t = t + Convert.ToInt32(g);
                }
                string mensaje = t == 0 ? "¿Esta a punto de guardar la Revisión. Si esto sucede no podra ser modificada. Todos sus datos son correctos?" : "¿Esta a punto de guardar la Revisión Y Generar un vale por un total de " + t.ToString() + ". Si esto sucede no podra ser modificada. Todos sus datos son correctos?";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','" + mensaje + "');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de cancelar la revision, de todos modos, tendra que realizarla en algun momento, Desea Salir?');", true);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    DataTable table = (DataTable)Session["Tabla_DatosEmpleado"];
                    string strHostName = Dns.GetHostName();
                    IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                    Servicioes_RevisionENT entidad = new Servicioes_RevisionENT();
                    Servicios_RevisionCOM componente = new Servicios_RevisionCOM();
                    entidad.Idc_prebaja = idc_prebaja;//ID DE PREBAJA
                    entidad.Idc_usuario = idc_usuarioprebaja;//USUARIO QUE REALIZA LA PREBAJA
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                                                                 //pruebas

                    Boolean error = false;
                    foreach (RepeaterItem item in RepeaterServicios.Items)
                    {
                        TextBox idc_reviser = (TextBox)item.FindControl("idc_reviser");
                        TextBox txtCosto = (TextBox)item.FindControl("txtCosto");
                        TextBox txtObservaciones = (TextBox)item.FindControl("txtObservaciones");
                        entidad.Pidc_revisionser = Convert.ToInt32(idc_reviser.Text.ToString());
                        entidad.Observaciones = txtObservaciones.Text;
                        SqlMoney monto = SqlMoney.Parse(txtCosto.Text);
                        entidad.Monto = monto;
                        //entidad.Cadtotal = table.Rows.Count;//id de revision de servicio
                        //entidad.Cadobservciones = GeneraCadena();
                        DataSet ds = componente.InsertarRevision(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        string mensaje = row["mensaje"].ToString();
                        if (mensaje != "")//no hay errores retornamos true
                        {//mostramos error
                            Alert.ShowAlertError(mensaje, this);
                            error = true;
                        }
                    }
                    //entidad.Cadtotal = table.Rows.Count;//id de revision de servicio
                    //entidad.Cadobservciones = GeneraCadena();
                    //DataSet ds = componente.InsertarRevision(entidad);
                    //DataRow row = ds.Tables[0].Rows[0];
                    ////verificamos que no existan errores
                    //string mensaje = row["mensaje"].ToString();
                    if (error == false)//no hay errores retornamos true
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La revisión fue guardada correctamente.','revisiones.aspx','success');", true);
                    }
                    //else
                    //{//mostramos error
                    //    Alert.ShowAlertError(mensaje, this);
                    //}
                    break;

                case "Cancelar":
                    Response.Redirect("revisiones.aspx");
                    break;
            }
        }

        /// <summary>
        /// Genera cadena con resultado de Servicios
        /// </summary>
        /// <returns></returns>
        public string GeneraCadena()
        {
            string list = null;
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            foreach (RepeaterItem item in RepeaterServicios.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                TextBox txtcosto = (TextBox)item.FindControl("txtCosto");
                Label idc_revisionser = (Label)item.FindControl("idc_revisionser");
                list = list + (idc_revisionser.Text + ";" + txt.Text + ";" + txtcosto.Text + ";");
            }
            return list;
        }

        protected void RepeaterServicios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataTable table = (DataTable)Session["Tabla_DatosDetalles"];
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            TextBox txtDetalles = (TextBox)e.Item.FindControl("txtDetalles");
            Label lbltpodetalle = (Label)e.Item.FindControl("lbltpodetalle");
            Panel txtCosto = (Panel)e.Item.FindControl("panelcosto");

            string tipo_apli = Convert.ToString(DataBinder.Eval(dbr, "tipo_aplica"));
            int generar_vales = Convert.ToInt32(DataBinder.Eval(dbr, "generar_vales"));
            if (tipo_apli.Equals("I") || tipo_apli.Equals("C") || tipo_apli.Equals("S"))
            {
                txtDetalles.Visible = true;
                foreach (DataRow row in table.Rows)
                {
                    string TIPO = row["tipo"].ToString();
                    if (row["tipo"].ToString().Equals(tipo_apli))
                    {
                        lbltpodetalle.Visible = true;
                        lbltpodetalle.Text = row["descripcion"].ToString();
                        txtDetalles.Text = row["valores"].ToString();
                    }
                }
            }
            if (generar_vales == 1)
            {
                txtCosto.Visible = true;
            }
        }

        protected void txtCosto_TextChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in RepeaterServicios.Items)
            {
                Label lblerrorCantidadReal = (Label)item.FindControl("lblerrorCantidadReal");
                TextBox txtCosto = (TextBox)item.FindControl("txtCosto");
                decimal cantidad = Convert.ToDecimal(txtCosto.Text);
                lblerrorCantidadReal.Visible = false;
                if (cantidad < 0)
                {
                    lblerrorCantidadReal.Visible = true;
                }
            }
        }

        protected void txtObservaciones_TextChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in RepeaterServicios.Items)
            {
                Label lblerrorobsr = (Label)item.FindControl("lblerrorobsr");
                lblerrorobsr.Visible = false;
            }
        }
    }
}