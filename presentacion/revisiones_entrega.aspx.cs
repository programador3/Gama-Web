using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class revisiones_entrega : System.Web.UI.Page
    {
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
                btnCancelar.Visible = false;
                btnAceptar.Visible = false;
                Alert.ShowAlertInfo("Esta pagina esta en modo informativo, NO PODRA guardar la Revisión. Si desea hacerlo, verifique el apartado de notificaciones", "Mensaje del sistema", this);
            }
            if (!Page.IsPostBack)
            {
                //cargo datos a tablas
                CargarGridPrincipal(idc_entrega, idc_puestoent);
            }
        }

        /// <summary>
        /// Carga los datos en Tablas de sesion y carga el grid principal
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal(int idc_entrega, int idc_puestoent)
        {
            Servicios_EntregaENT entidad = new Servicios_EntregaENT();
            Revisones_EntregaCOM componente = new Revisones_EntregaCOM();
            entidad.Pidc_puestoentrega = idc_puestoent;
            entidad.Pidc_entrega = idc_entrega;
            DataSet ds = componente.CargaPrep(entidad);
            Session["Tabla_DatosServicio"] = ds.Tables[1];
            Session["Tabla_DatosDetalles"] = ds.Tables[2];
            repeatServicios.DataSource = ds.Tables[1];
            repeatServicios.DataBind();
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
        /// Genera cadena con resultado de preparaciones
        /// </summary>
        /// <returns></returns>
        public string GeneraCadena()
        {
            string list = "";
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_DatosServicio"];
            foreach (RepeaterItem item in repeatServicios.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblidc_revisionser");
                if (txt.ReadOnly != true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_revisionser"]) == Convert.ToInt32(lblidc.Text))
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
        public int TotalCadena()
        {
            int list = 0;
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_DatosServicio"];
            foreach (RepeaterItem item in repeatServicios.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblidc_revisionser");
                if (txt.ReadOnly != true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_revisionser"]) == Convert.ToInt32(lblidc.Text))
                        {
                            list = list + 1;
                        }
                    }
                }
            }
            return list;
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    Servicios_EntregaENT entidad = new Servicios_EntregaENT();
                    Revisones_EntregaCOM componente = new Revisones_EntregaCOM();
                    entidad.Pidc_entrega = Convert.ToInt32(Session["idc_entrega"]);
                    entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                    entidad.Pidc_empleado = Convert.ToInt32(Session["idc_empleado"].ToString());
                    entidad.Cadser = GeneraCadena();
                    entidad.Totalcadser = TotalCadena();

                    DataSet ds = componente.InsertarEntrrega(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    //verificamos que no existan errores
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")//no hay errores retornamos true
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La Entrega Total o Parcial fue guardada correctamente.','entregas.aspx');", true);
                    }
                    else
                    {//mostramos error
                        Alert.ShowAlertError(mensaje, this);
                    }

                    break;

                case "Cancelar":
                    Response.Redirect("entregas.aspx");
                    break;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Confirmar la Entrega?');", true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        protected void repeatServicios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            TextBox txtObservaciones = (TextBox)e.Item.FindControl("txtObservaciones");
            Label lblfecharev = (Label)e.Item.FindControl("lblfecharev");
            int revisado = Convert.ToInt32(DataBinder.Eval(dbr, "revisado"));
            if (revisado == 1)
            {
                // cbx.Text = Convert.ToString(DataBinder.Eval(dbr, "fecha")).ToUpper();
                txtObservaciones.Text = Convert.ToString(DataBinder.Eval(dbr, "observaciones")).ToUpper();
                txtObservaciones.ReadOnly = true;
                lblfecharev.Visible = true;
                lblfecharev.Text = "Preparado desde " + Convert.ToString(DataBinder.Eval(dbr, "fecha")).ToUpper();
            }
            DataTable table = (DataTable)Session["Tabla_DatosDetalles"];
            TextBox txtDetalles = (TextBox)e.Item.FindControl("txtDetalles");
            Label lbltpodetalle = (Label)e.Item.FindControl("lbltpodetalle");
            string tipo_apli = Convert.ToString(DataBinder.Eval(dbr, "tipo_aplica"));
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
        }
    }
}