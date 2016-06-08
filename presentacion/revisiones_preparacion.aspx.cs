using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class revisiones_preparacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            // si nop trae valores regreso
            if (Request.QueryString["idc_puesto"] == null)
            {
                Response.Redirect("preparaciones.aspx");
            }
            //bajo valores
            int idc_puesto = 0;
            int idc_usuario = 0;
            int idc_puestobaja = 0;

            if (Request.QueryString["preview"] == null)//si no viene del administrador
            {
                idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_puestobaja = Convert.ToInt32(Request.QueryString["idc_puesto"].ToString());
            }
            else
            {
                idc_puesto = Convert.ToInt32(Request.QueryString["idc_puesto"].ToString());
                idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_puestobaja = Convert.ToInt32(Request.QueryString["idc_puestobaja"].ToString());
                btnCancelar.Visible = false;
                btnAceptar.Visible = false;
                Alert.ShowAlertInfo("Esta pagina esta en modo informativo, NO PODRA guardar la Revisión. Si desea hacerlo, verifique el apartado de notificaciones", "Mensaje del sistema", this);
            }
            if (!Page.IsPostBack)
            {
                //cargo datos a tablas
                CargarGridPrincipal(idc_puestobaja, idc_puesto);
            }
        }

        /// <summary>
        /// Carga los datos en Tablas de sesion y carga el grid principal
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal(int idc_puesto, int idc_puestoprp)
        {
            Servicios_PreparacionENT entidad = new Servicios_PreparacionENT();
            Revisiones_PreparacionCOM componente = new Revisiones_PreparacionCOM();
            entidad.Pidc_puesto = idc_puesto;
            entidad.Pidc_puestoprep = idc_puestoprp;
            DataSet ds = componente.CargaPrep(entidad);
            Session["Tabla_DatosServicio"] = ds.Tables[1];
            Session["Tabla_DatosDetalles"] = ds.Tables[2];
            repeatServicios.DataSource = ds.Tables[1];
            repeatServicios.DataBind();
            DataRow row = ds.Tables[0].Rows[0];
            lblPuesto.Text = row["descripcion"].ToString();
            lblfechasoli.Text = row["fecha_registro"].ToString();
            Session["idc_prepara"] = Convert.ToInt32(row["idc_prepara"].ToString());
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

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de guardar la Revisión. Si esto sucede no podra ser modificada. Todos sus datos son correctos?');", true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    Servicios_PreparacionENT entidad = new Servicios_PreparacionENT();
                    Revisiones_PreparacionCOM componente = new Revisiones_PreparacionCOM();
                    entidad.Idc_prepara = Convert.ToInt32(Session["idc_prepara"]);
                    entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                    entidad.Pidc_puestoprep = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                    entidad.Cadser = GeneraCadena();
                    entidad.Totalcadser = TotalCadena();

                    DataSet ds = componente.InsertarPrep(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    //verificamos que no existan errores
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")//no hay errores retornamos true
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La Preparación Total o Parcial fue guardada correctamente.','preparaciones.aspx','success');", true);
                    }
                    else
                    {//mostramos error
                        Alert.ShowAlertError(mensaje, this);
                    }
                    break;

                case "Cancelar":
                    Response.Redirect("preparaciones.aspx");
                    break;
            }
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