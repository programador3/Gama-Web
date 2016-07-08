using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class herramientas_revision : System.Web.UI.Page
    {
        public int idc_usuarioprebaja = 0;
        public int idc_prebaja = 0;
        public bool cbxselecte = false;
        public bool permiso_no_escribir_folio = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            permiso_no_escribir_folio = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 332);
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
                //genero datos del empleado
                GenerarDatosEmpleado(idc_puestoprebaja);
                //verificamos que no venga del adminsitrador para visualizar
            }
            else
            {//si viene del administradpor
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
                //genero datos del empleado
                GenerarDatosEmpleado(idc_puestoprebaja);
                Alert.ShowAlertInfo("Esta pagina esta en modo informativo, NO PODRA guardar la Revisión. Si desea hacerlo, verifique el apartado de notificaciones", "Mensaje del sistema", this);
                lnkRevision.Visible = false;
                lnkDetalles.Visible = false;
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
                Herramientas_RevisionENT entidad = new Herramientas_RevisionENT();
                Herramientas_RevisionCOM componente = new Herramientas_RevisionCOM();
                entidad.Idc_puestorevi = idc_puestorevi;
                entidad.Idc_puestoprebaja = idc_puestoprebaja;
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                DataSet ds = componente.CargaHerramientasRevision(entidad);
                Session["Tabla_DatosEmpleado"] = ds.Tables[0];
                Session["Tabla_Herramientas"] = ds.Tables[1];
                Session["Tabla_Herramientas_det"] = ds.Tables[2];
                gridHerramientas.DataSource = ds.Tables[1];
                gridHerramientas.DataBind();
                repeatRevision.DataSource = ds.Tables[1];
                repeatRevision.DataBind();
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
        /// Genera ruta de imagen
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

        protected void lnkRevision_Click(object sender, EventArgs e)
        {
            lblSelectedHerramientas.Text = "Revisión de Herramientas <i class='fa fa-check-square-o'></i>";
            lnkRevision.CssClass = "btn btn-primary active";
            lnkDetalles.CssClass = "btn btn-link";
            PanelActivos.Visible = false;
            PanelRevisionActivos.Visible = true;
        }

        protected void lnkDetalles_Click(object sender, EventArgs e)
        {
            lblSelectedHerramientas.Text = "Detalles de Herramientas <i class='fa fa-list-alt'></i>";
            lnkDetalles.CssClass = "btn btn-primary active";
            lnkRevision.CssClass = "btn btn-link";
            PanelActivos.Visible = true;
            PanelRevisionActivos.Visible = false;
        }

        protected void gridHerramientas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER MEDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            int id_categoria = Convert.ToInt32(gridHerramientas.DataKeys[index].Values["idc_actscategoria"].ToString());
            int id_folio = Convert.ToInt32(gridHerramientas.DataKeys[index].Values["folio"].ToString());
            string subcat = gridHerramientas.DataKeys[index].Values["subcat"].ToString();
            switch (e.CommandName)
            {
                case "Ver"://Ver Modal con detalles
                    lblMSubcat.Text = subcat;
                    lblMDetalles.Text = id_folio.ToString();
                    CargarGridDetalles(id_categoria);
                    ScriptManager.RegisterStartupScript(this, GetType(), "preherr", "ModalPreviewHeramienta();", true);
                    break;

                default:
                    break;
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            DataTable tabla = (DataTable)Session["Tabla_DatosEmpleado"];
            DataTable tabla_herramientas = (DataTable)Session["Tabla_Herramientas"];
            int idc_puestoprebaja = Convert.ToInt32(Request.QueryString["idc_puestoprebaja"].ToString());
            DataRow[] result = tabla.Select("idc_puesto = " + idc_puestoprebaja + "");
            //VALIDAMOS QUE SI NO ENTREGO, TENGA UN COSTO ASIGNADO
            Boolean error = false;
            foreach (RepeaterItem item in repeatRevision.Items)
            {
                Label lblerror = (Label)item.FindControl("lblerror");
                TextBox txtMoney = (TextBox)item.FindControl("txtMoney");
                CheckBox cbx = (CheckBox)item.FindControl("cbx"); TextBox txtFolio = (TextBox)item.FindControl("txtFolio");
                Label lblfolioactv = (Label)item.FindControl("lblfolioactv");
                Label lblerrorfolio = (Label)item.FindControl("lblerrorfolio");
                Label lblfoliocorrecto = (Label)item.FindControl("lblfoliocorrecto");
                lblerrorfolio.Visible = false;
                lblerrorfolio.Text = "DEBE INSERTAR EL FOLIO";
                string folio = lblfolioactv.Text;

                lblfoliocorrecto.Text = "0";
                //COMENTADO HUMBERTO: DETERMINA SI NO SE ESCRIBE EL FOLIO
                if (txtFolio.Text == "" && permiso_no_escribir_folio == false)
                {
                    lblfoliocorrecto.Text = "1";
                    lblerrorfolio.Visible = true;
                    error = true;
                }
                if (txtFolio.Text != "" && folio != txtFolio.Text && permiso_no_escribir_folio == false)
                {
                    lblerrorfolio.Text = "EL FOLIO ES INCORRECTO, SE REVISARA";
                    lblfoliocorrecto.Text = "1";
                    lblerrorfolio.Visible = true;
                }
                lblerror.Visible = false;
                decimal cantidad = Convert.ToDecimal(txtMoney.Text);
                if (txtMoney.Text != "")
                {
                    if (cantidad < 0)
                    {
                        lblerror.Visible = true;
                        lblerror.Text = "NO PUEDE TENER un valor negativo";
                        error = true;
                    }
                }
                if (cbx.Checked == false)
                {
                    if (cantidad == 0)
                    {
                        lblerror.Visible = true;
                        lblerror.Text = "Debe colocar un COSTO";
                        error = true;
                    }
                }
            }
            if (error == true)
            {
                Alert.ShowAlertError("Existen uno o mas errores. Verifique en el formulario los errores MARCADOS EN COLOR ROJO.", this);
            }
            else//no hay error
            {
                Session["Caso_Confirmacion"] = "Guardar";
                string d = SumaCosto().ToString();
                string mensaje = SumaCosto().ToString() == "0.00" ? "¿Esta a punto de guardar la Revisión. Si esto sucede no podra ser modificada. Todos sus datos son correctos?" : "¿Esta a punto de guardar la Revisión Y Generar un vale por un total de " + SumaCosto().ToString() + ". Si esto sucede no podra ser modificada. Todos sus datos son correctos?";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','" + mensaje + "');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            int idc_puestoprebaja = Convert.ToInt32(Request.QueryString["idc_puestoprebaja"].ToString());
            Response.Redirect("herramientas_revision.aspx?idc_puestoprebaja=" + idc_puestoprebaja);
        }

        protected void txtMoney_TextChanged(object sender, EventArgs e)
        {
            TextBox txtMoneysend = (TextBox)sender;
            foreach (RepeaterItem item in repeatRevision.Items)
            {
                Label lblerror = (Label)item.FindControl("lblerror");
                TextBox txtMoney = (TextBox)item.FindControl("txtMoney");
                lblerror.Visible = false;
                if (txtMoney.Text != "")
                {
                    decimal cantidad = Convert.ToDecimal(txtMoney.Text);
                    if (cantidad < 0)
                    {
                        lblerror.Visible = true;
                        lblerror.Text = "NO PUEDE TENER un valor negativo";
                        break;
                    }
                }
            }
            SumaCosto();
        }

        /// <summary>
        /// Genera el costo total acumulado
        /// </summary>
        public decimal SumaCosto()
        {
            decimal Total = 0;
            foreach (RepeaterItem item in repeatRevision.Items)
            {
                Label lblerror = (Label)item.FindControl("lblerror");
                TextBox txtMoney = (TextBox)item.FindControl("txtMoney");
                lblerror.Visible = false;
                if (txtMoney.Text != "")
                {
                    decimal cantidad = Convert.ToDecimal(txtMoney.Text);
                    if (cantidad < 0)
                    {
                        lblerror.Visible = true;
                        lblerror.Text = "NO PUEDE TENER un valor negativo";
                        txtMoney.Focus();
                    }
                    Total = Total + cantidad;
                }
            }
            txtTotal.Text = Total.ToString();
            return Total;
        }

        /// <summary>
        /// Genera cadena con resultado de revisiones
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
                TextBox txt = (TextBox)item.FindControl("txtMoney");
                TextBox txtFolio = (TextBox)item.FindControl("txtFolio");
                Label lblfolioactv = (Label)item.FindControl("lblfolioactv");
                Label lblidc = (Label)item.FindControl("lblactivo");
                Label lblfoliocorrecto = (Label)item.FindControl("lblfoliocorrecto");
                foreach (DataRow row in tabla.Rows)
                {
                    if (Convert.ToInt32(row["idc_activo"]) == Convert.ToInt32(lblidc.Text))
                    {
                        int resultcbx = 0;
                        string value = lblfoliocorrecto.Text == "1" ? txtFolio.Text : "0";
                        if (cbx.Checked == true) { resultcbx = 1; }
                        list = list + (lblidc.Text + ";" + resultcbx.ToString() + ";" + txt.Text + ";" + value + ";");
                    }
                }
            }
            return list;
        }

        protected void repeatRevision_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            LinkButton lnkDetallesHerrRevision = (LinkButton)e.Item.FindControl("lnkDetallesHerrRevision");
            string idc_activo = Convert.ToString(DataBinder.Eval(dbr, "idc_activo"));
            lnkDetallesHerrRevision.CommandName = Convert.ToString(DataBinder.Eval(dbr, "idc_actscategoria"));
            lnkDetallesHerrRevision.CommandArgument = Convert.ToString(DataBinder.Eval(dbr, "folio"));
            Label lblidc = (Label)e.Item.FindControl("lblactivo");
            CheckBox cbx = (CheckBox)e.Item.FindControl("cbx");
            TextBox txtMoney = (TextBox)e.Item.FindControl("txtMoney");
            Label fecha = (Label)e.Item.FindControl("fecha");
            lblidc.Text = idc_activo;
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    DataTable tabla = (DataTable)Session["Tabla_DatosEmpleado"];
                    DataTable tabla_herramientas = (DataTable)Session["Tabla_Herramientas"];
                    int idc_puestoprebaja = Convert.ToInt32(Request.QueryString["idc_puestoprebaja"].ToString());
                    DataRow[] result = tabla.Select("idc_puesto = " + idc_puestoprebaja + "");
                    Herramientas_RevisionENT entidad = new Herramientas_RevisionENT();
                    Herramientas_RevisionCOM componente = new Herramientas_RevisionCOM();
                    entidad.PIDC_prebaja = idc_prebaja;//idc prebaja
                    entidad.Idc_usuario = idc_usuarioprebaja;//idc usuario prebaja
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.PCADTEL = GeneraCadena();//cadena de revision
                    entidad.PNUMTEL = tabla_herramientas.Rows.Count; ;//numero de filas de cadena
                    SqlMoney costo = SqlMoney.Parse(SumaCosto().ToString());
                    entidad.PMONTO = costo;//monto total
                    try
                    {
                        DataSet ds = componente.InsertarRevision(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        string mensaje = row["mensaje"].ToString();
                        if (mensaje == "")//no hay errores retornamos true
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La revisión fue guardada correctamente.','revisiones.aspx');", true);
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

                default:
                    break;
            }
        }

        protected void lbkseltodo_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeatRevision.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtMoney");
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                cbx.Checked = true;
                txt.ReadOnly = true;
                txt.Text = "0.00";
            }
            lbkseltodo.Visible = false;
            lnkDes.Visible = true;
        }

        protected void lbkdestodo_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeatRevision.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtMoney");
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                cbx.Checked = false;
                txt.ReadOnly = false;
            }
            lbkseltodo.Visible = true;
            lnkDes.Visible = false;
        }

        protected void cbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbxse = (CheckBox)sender;

            foreach (RepeaterItem item in repeatRevision.Items)
            {
                Label lblerror = (Label)item.FindControl("lblerror");
                TextBox txt = (TextBox)item.FindControl("txtMoney");
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                if (cbx.Checked == true) { txt.ReadOnly = true; txt.Text = "0.00"; }
                if (cbx.Checked == false) { txt.ReadOnly = false; }
                lblerror.Visible = false;
            }
            //ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" +cbxse.ClientID.ToString()+ "');", true);
        }

        protected void lnkDetallesHerrRevision_Click(object sender, EventArgs e)
        {
            LinkButton lnkDetallesHerrRevision = (LinkButton)sender;
            int id_categoria = Convert.ToInt32(lnkDetallesHerrRevision.CommandName.ToString());
            lblMDetalles.Text = lnkDetallesHerrRevision.CommandArgument.ToString();
            CargarGridDetalles(id_categoria);
            ScriptManager.RegisterStartupScript(this, GetType(), "PREVIEW", "ModalPreview('" + "#" + lnkDetallesHerrRevision.ClientID.ToString() + "');", true);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            SumaCosto();
        }

        protected void txtFolio_TextChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeatRevision.Items)
            {
                TextBox txtFolio = (TextBox)item.FindControl("txtFolio");
                Label lblfolioactv = (Label)item.FindControl("lblfolioactv");
                Label lblerrorfolio = (Label)item.FindControl("lblerrorfolio");
                Label lblfoliocorrecto = (Label)item.FindControl("lblfoliocorrecto");
                lblerrorfolio.Visible = false;
                string folio = lblfolioactv.Text;

                lblfoliocorrecto.Text = "0";
                if (txtFolio.Text == "" && permiso_no_escribir_folio == false)
                {
                    lblerrorfolio.Text = "DEBE INSERTAR EL FOLIO";
                    lblfoliocorrecto.Text = "1";
                    lblerrorfolio.Visible = true;
                }

                if (txtFolio.Text != "" && folio != txtFolio.Text && permiso_no_escribir_folio == false)
                {
                    lblerrorfolio.Text = "EL FOLIO ES INCORRECTO, SE REVISARA";
                    lblfoliocorrecto.Text = "1";
                    lblerrorfolio.Visible = true;
                }
            }
        }
    }
}