using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class herramientas_entrega : System.Web.UI.Page
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
                btnGuardar.Visible = false;
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
            try
            {
                Herramientas_EntregasENT entidad = new Herramientas_EntregasENT();
                Herramientas_EntregasCOM componente = new Herramientas_EntregasCOM();
                entidad.Pidc_Entrega = idc_entrega;
                entidad.Pidc_puestoent = idc_puestoent;
                DataSet ds = componente.CargaHerramientas(entidad);
                Session["Tabla_DatosEmpleado"] = ds.Tables[0];
                Session["Tabla_Herramientas"] = ds.Tables[1];
                Session["Tabla_Herramientas_det"] = ds.Tables[2];
                DataRow row = ds.Tables[0].Rows[0];
                repeatRevision.DataSource = ds.Tables[1];
                repeatRevision.DataBind();
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
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga Modal con grid detalles filtrado por idc_actscategoria
        /// </summary>
        public void CargarGridDetalles(int idc_actscategoria)
        {
            DataTable tabla_detalles = (DataTable)Session["Tabla_Herramientas_det"];
            DataTable tabla_temp = new DataTable();
            tabla_temp.Columns.Add("idc_actscategoria");
            tabla_temp.Columns.Add("descripcion");
            tabla_temp.Columns.Add("observaciones");
            tabla_temp.Columns.Add("valor");
            //sacamos los datos que sean del mismo id
            foreach (DataRow row_details in tabla_detalles.Rows)
            {
                if (Convert.ToInt32(row_details["idc_actscategoria"]) == idc_actscategoria)
                {
                    DataRow new_row = tabla_temp.NewRow();
                    new_row["idc_actscategoria"] = row_details["idc_actscategoria"].ToString();
                    new_row["descripcion"] = row_details["descripcion"].ToString() + ":";
                    new_row["observaciones"] = row_details["observaciones"].ToString();
                    new_row["valor"] = row_details["valor"].ToString();
                    tabla_temp.Rows.Add(new_row);
                }
            }
            //cargamos repeat
            gridHerramientasDetalles.DataSource = tabla_temp;
            gridHerramientasDetalles.DataBind();
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
        /// Retorna cadena de entregas
        /// </summary>
        /// <returns></returns>
        public string GeneraCadena()
        {
            string cadena = "";
            foreach (RepeaterItem Item in repeatRevision.Items)
            {
                TextBox txtObservaciones = (TextBox)Item.FindControl("txtObservaciones");
                CheckBox cbx = (CheckBox)Item.FindControl("cbx");
                Label lblactivo = (Label)Item.FindControl("lblactivo");
                if (cbx.Checked == true && cbx.Enabled == true)
                {
                    cadena = cadena + lblactivo.Text + ";" + txtObservaciones.Text.ToUpper() + ";";
                }
            }
            return cadena;
        }

        public int TotalCadena()
        {
            int total = 0;
            foreach (RepeaterItem Item in repeatRevision.Items)
            {
                TextBox txtObservaciones = (TextBox)Item.FindControl("txtObservaciones");
                CheckBox cbx = (CheckBox)Item.FindControl("cbx");
                Label lblactivo = (Label)Item.FindControl("lblactivo");
                if (cbx.Checked == true && cbx.Enabled == true)
                {
                    total = total + 1;
                }
            }
            return total;
        }

        protected void repeatRevision_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            LinkButton lnkDetallesHerrRevision = (LinkButton)e.Item.FindControl("lnkDetallesHerrRevision");
            TextBox txtObservaciones = (TextBox)e.Item.FindControl("txtObservaciones");
            CheckBox cbx = (CheckBox)e.Item.FindControl("cbx");
            string idc_activo = Convert.ToString(DataBinder.Eval(dbr, "idc_activo"));
            int revisado = Convert.ToInt32(DataBinder.Eval(dbr, "revisado"));
            lnkDetallesHerrRevision.CommandName = Convert.ToString(DataBinder.Eval(dbr, "idc_actscategoria"));
            lnkDetallesHerrRevision.CommandArgument = Convert.ToString(DataBinder.Eval(dbr, "folio"));
            Label lblidc = (Label)e.Item.FindControl("lblactivo");
            lblidc.Text = idc_activo;
            if (revisado == 1)
            {
                cbx.Checked = true;
                cbx.Enabled = false;
                cbx.Text = Convert.ToString(DataBinder.Eval(dbr, "fecha_revisado")).ToUpper();
                txtObservaciones.Text = Convert.ToString(DataBinder.Eval(dbr, "observaciones_revisado")).ToUpper();
                txtObservaciones.ReadOnly = true;
            }
        }

        protected void lnkDetallesHerrRevision_Click(object sender, EventArgs e)
        {
            LinkButton lnkDetallesHerrRevision = (LinkButton)sender;

            int id_categoria = Convert.ToInt32(lnkDetallesHerrRevision.CommandName.ToString());
            lblMDetalles.Text = lnkDetallesHerrRevision.CommandArgument.ToString();
            CargarGridDetalles(id_categoria);
            ScriptManager.RegisterStartupScript(this, GetType(), "PREVIEW", "ModalPreview('" + "#" + lnkDetallesHerrRevision.ClientID.ToString() + "');", true);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    Herramientas_EntregasENT entidad = new Herramientas_EntregasENT();
                    Herramientas_EntregasCOM componente = new Herramientas_EntregasCOM();
                    entidad.Pidc_Entrega = Convert.ToInt32(Session["idc_entrega"]);
                    entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                    entidad.Pidc_puesto = Convert.ToInt32(Session["idc_puesto"].ToString());
                    entidad.Pidc_empleado = Convert.ToInt32(Session["idc_empleado"].ToString());
                    entidad.Cadherr = GeneraCadena();
                    entidad.Numcadherr = TotalCadena();
                    try
                    {
                        DataSet ds = componente.InsertarPrep(entidad);
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (TotalCadena() < 1) { error = true; Alert.ShowAlertError("Debe Guardar al menos 1 Activo", this); }
            if (error == false)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Registrar la Entrega de estos Activos?');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        protected void lbkseltodo_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeatRevision.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                cbx.Checked = true;
            }
            lbkseltodo.Visible = false;
            lnkDes.Visible = true;
        }

        protected void lnkDes_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeatRevision.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                cbx.Checked = false;
            }
            lbkseltodo.Visible = true;
            lnkDes.Visible = false;
        }
    }
}