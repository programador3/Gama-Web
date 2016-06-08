using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class herramientas_preparacion : System.Web.UI.Page
    {
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
                idc_puestobaja = Convert.ToInt32(Request.QueryString["idc_puesto"].ToString());
            }
            else
            {
                idc_puesto = Convert.ToInt32(Request.QueryString["idc_puesto"].ToString());
                idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_puestobaja = Convert.ToInt32(Request.QueryString["idc_puestobaja"].ToString());
                btnCancelar.Visible = false;
                btnGuardar.Visible = false;
                Alert.ShowAlertInfo("Esta pagina esta en modo informativo, NO PODRA guardar la Revisión. Si desea hacerlo, verifique el apartado de notificaciones", "Mensaje del sistema", this);
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
                Herramientas_PreparacionENT entidad = new Herramientas_PreparacionENT();
                Herramientas_PreparacionCOM componente = new Herramientas_PreparacionCOM();
                entidad.Pidc_puestoprep = idc_puestoprep;//puesto que prepara
                entidad.Pidc_puesto = idc_puesto;
                DataSet ds = componente.CargaHerramientasPrep(entidad);
                Session["Tabla_DatosEmpleado"] = ds.Tables[0];
                Session["Tabla_Herramientas"] = ds.Tables[1];
                Session["Tabla_Herramientas_det"] = ds.Tables[2];
                repeatRevision.DataSource = ds.Tables[1];
                repeatRevision.DataBind();
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

        /// <summary>
        /// Genera cadena con resultado de preparaciones
        /// </summary>
        /// <returns></returns>
        public string GeneraCadena()
        {
            string list = null;
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Herramientas"];
            foreach (RepeaterItem item in repeatRevision.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblactivo");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_activo"]) == Convert.ToInt32(lblidc.Text))
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
            DataTable tabla = (DataTable)Session["Tabla_Herramientas"];
            foreach (RepeaterItem item in repeatRevision.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblactivo");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_activo"]) == Convert.ToInt32(lblidc.Text))
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
                    Herramientas_PreparacionENT entidad = new Herramientas_PreparacionENT();
                    Herramientas_PreparacionCOM componente = new Herramientas_PreparacionCOM();
                    entidad.Idc_prepara = Convert.ToInt32(Session["idc_prepara"]);
                    entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                    entidad.Pidc_puestoprep = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                    entidad.Cadherr = GeneraCadena();
                    entidad.Totalcadherr = TotalCadena();
                    try
                    {
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

        protected void cbx_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string Cadena = GeneraCadena();
            if (Cadena == null)
            {
                Alert.ShowAlertError("Para Guardar, debe Preparar al menos una herramienta.", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de guardar la Preparación. Solo se guardaran las Herramientas que haya marcado, pero PODRA GUARDAR LAS DEMAS mas adelante. Todos sus datos son correctos?');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
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

        protected void lbkdestodo_Click(object sender, EventArgs e)
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