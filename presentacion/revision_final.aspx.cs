using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class revision_final : System.Web.UI.Page
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
            //bajo valores
            if (!Page.IsPostBack && Request.QueryString["preview"] == null)
            {
                idc_puestoprebaja = Convert.ToInt32(Request.QueryString["idc_puestoprebaja"].ToString());
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_usuarioprebaja = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_puestorev = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                Session["total"] = 0.00;
                //cargo datos a tablas
                CargarGridPrincipal(idc_usuario, idc_puestorev, idc_puestoprebaja, false);
                Session["idc_prebaja_vale"] = null;
                GenerarDatosEmpleado(idc_puestoprebaja);
            }
            if (!Page.IsPostBack && Request.QueryString["preview"] != null)
            {
                Session["total"] = 0.00;
                //cargo datos a tablas
                CargarGridPrincipal(Convert.ToInt32(Session["sidc_usuario"].ToString()), Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puestorev"])), Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puestoprebaja"])), true);
                btnAceptar.Visible = false;
                Alert.ShowAlertInfo("Esta pagina esta en modo informativo, NO PODRA guardar la Revisión. Si desea hacerlo, verifique el apartado de notificaciones", "Mensaje del sistema", this);

                Session["idc_prebaja_vale"] = null;
                GenerarDatosEmpleado(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puestoprebaja"])));
            }
            Decimal tot = Convert.ToDecimal(Session["total"]);
            string t = string.Format(String.Format("{0:C2}", tot));
            txtTotal.Text = t;
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
        public void CargarGridPrincipal(int idc_usuario, int idc_puestorevi, int idc_puestoprebaja, bool consulta)
        {
            Revision_FinalENT entidad = new Revision_FinalENT();
            Revision_FinalCOM componente = new Revision_FinalCOM();
            entidad.Pidc_puestorevi = idc_puestorevi;
            entidad.Pidc_puestoprebaja = idc_puestoprebaja;
            entidad.Idc_usuario = idc_usuario;
            entidad.Pconsulta = consulta;
            DataSet ds = componente.CargaRevision(entidad);
            Session["Tabla_DatosEmpleado"] = ds.Tables[0];
            gridVales.DataSource = ds.Tables[1];
            gridVales.DataBind();
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
                    lblfechasoli.Text = row["fecha"].ToString();
                    Session["idc_prebaja_vale"] = Convert.ToInt32(row["idc_prebaja"].ToString());
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
                            imgEmpleado.ImageUrl = url;
                        }
                        break;
                }
            }
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
                    Revision_FinalENT entidad = new Revision_FinalENT();
                    Revision_FinalCOM componente = new Revision_FinalCOM();
                    entidad.Idc_prebaja = Convert.ToInt32(Session["idc_prebaja_vale"]);//ID DE PREBAJA
                    entidad.Idc_usuario = idc_usuarioprebaja;//USUARIO QUE REALIZA LA PREBAJA
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Observaciones = txtObservaciones.Text.ToUpper();
                    entidad.Concepto = txtObsrv.Text.ToUpper();
                    entidad.Monto = (txtCosto.Text == "" ? 0 : Convert.ToInt32(Convert.ToDecimal(txtCosto.Text)));
                    DataSet ds = componente.InsertarRevision(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    //verificamos que no existan errores
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")//no hay errores retornamos true
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La revisión fue guardada correctamente.','revisiones.aspx','success');", true);
                    }
                    else
                    {//mostramos error
                        Alert.ShowAlertError(mensaje, this);
                    }
                    break;

                case "Cancelar":
                    Response.Redirect("revisiones.aspx");
                    break;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (txtObservaciones.Text == "") { Alert.ShowAlertError("Debe colocar una Observacion", this); error = true; }
            decimal cantidad = (txtCosto.Text == "" ? 0 : Convert.ToDecimal(txtCosto.Text));
            if (txtCosto.Visible == true)
            {
                if (cantidad < 0)
                {
                    lblerrorCantidadReal.Visible = true;
                    Alert.ShowAlertError("Debe colocar un Costo real y no Negativo.", this);
                    error = true;
                }
                if (txtObsrv.Text == string.Empty && cantidad > 0)
                {
                    lblerrorobsr.Visible = true;
                    Alert.ShowAlertError("Escriba un Concepto por el vale.", this);
                    error = true;
                }
            }
            if (error == false)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de guardar la Revisión. Si esto sucede no podra ser modificada. Todos sus datos son correctos?');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de cancelar la revision, de todos modos, tendra que realizarla en algun momento, Desea Salir?');", true);
        }

        protected void gridVales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Decimal total = Convert.ToDecimal(Session["total"]);
            Image nomina = (Image)e.Row.FindControl("nomina");
            Image bono = (Image)e.Row.FindControl("bono");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                if (Convert.ToInt32(rowView["bono"]) == 1)
                {
                    bono.ImageUrl = "imagenes/btn/checked.png";
                }
                if (Convert.ToInt32(rowView["activo_nomina"]) == 1)
                {
                    nomina.ImageUrl = "imagenes/btn/checked.png";
                }
                total = total + (Convert.ToDecimal(rowView["saldo"]));
            }
            Session["total"] = total;
        }

        protected void txtCosto_TextChanged(object sender, EventArgs e)
        {
            decimal cantidad = (txtCosto.Text == "" ? 0 : Convert.ToDecimal(txtCosto.Text));
            lblerrorCantidadReal.Visible = false;
            if (cantidad < 0)
            {
                lblerrorCantidadReal.Visible = true;
            }
        }

        protected void txtObsrv_TextChanged(object sender, EventArgs e)
        {
            lblerrorobsr.Visible = false;
        }
    }
}