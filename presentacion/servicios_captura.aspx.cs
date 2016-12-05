using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class servicios_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("descripcion");
                dt2.Columns.Add("idc_tareaser");
                dt2.Columns.Add("seleccionado");
                ViewState["dt_puestos_rel"] = dt2;
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_puesto");
                Session["tabla_puestos"] = dt;
                CargarGridPrincipal(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"])));
                
                lnktodo.Visible = Request.QueryString["val"] != null ? false : true;
                TareasServiciosTodos();
                TareasServicios(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"])));
            }
            if (Request.QueryString["val"] != null)
            {
                lbltext.Text = "Seleccione los puestos que le daran servicio";
                lnktodo.Visible = false;
            }
        }

        public void TareasServiciosTodos()
        {
            try
            {
                TareasCOM componente = new TareasCOM();
                DataSet ds = componente.sp_tareas_servicios_puestos(0);
                DataTable dt = ds.Tables[0];
                DataTable dt2 = ViewState["dt_puestos_rel"] as DataTable;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string puesto = row["descripcion"].ToString();
                        int idc_tareaser = Convert.ToInt32(row["idc_tareaser"]);
                        DataRow rowa = dt2.NewRow();
                        rowa["descripcion"] = puesto;
                        rowa["idc_tareaser"] = idc_tareaser;
                        rowa["seleccionado"] = 0;
                        dt2.Rows.Add(rowa);
                    }
                    ViewState["dt_puestos_rel"] = dt2;
                    CargarGrid();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
        private string Cadena()
        {
            string cadena = "";
            DataTable dt = ViewState["dt_puestos_rel"] as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                cadena = cadena + row["idc_tareaser"].ToString().Trim() + ";";
            }
            return cadena;
        }

        private int totalcadena()
        {
            DataTable dt = ViewState["dt_puestos_rel"] as DataTable;
            return dt.Rows.Count;
        }
        public void TareasServicios(int idc_puesto)
        {
            try
            {
                TareasCOM componente = new TareasCOM();
                DataSet ds = componente.sp_tareas_servicios_puestos(idc_puesto);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string puesto = row["descripcion"].ToString();
                        int idc_tareaser = Convert.ToInt32(row["idc_tareaser"]);
                        DeleteDT(idc_tareaser);
                        AddDt(puesto, idc_tareaser);
                    }
                }
                CargarGrid();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
        private void CargarGrid()
        {
            DataTable dt = ViewState["dt_puestos_rel"] as DataTable;
            DataTable dtc = dt.Copy();
            grisservicios.DataSource = dtc;
            grisservicios.DataBind();

            //ScriptManager.RegisterStartupScript(this, GetType(), "noti533swswW3", "DataTa2();", true);
        }
        private void AddDt(string puesto, int idc_puesto)
        {
            try
            {
                DataTable dt = ViewState["dt_puestos_rel"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    int idc_p = Convert.ToInt32(row["idc_tareaser"]);
                    if (idc_p == idc_puesto)
                    {
                        row["descripcion"] = puesto;
                        row["idc_tareaser"] = idc_puesto;
                        row["seleccionado"] = 1;
                        break;
                    }
                }
                ViewState["dt_puestos_rel"] = dt;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void DeleteDT(int idc_puesto)
        {
            try
            {
                DataTable dt = ViewState["dt_puestos_rel"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    int idc_p = Convert.ToInt32(row["idc_tareaser"]);
                    if (idc_p == idc_puesto)
                    {
                        row["seleccionado"] = 0;
                        break;
                    }
                }
                ViewState["dt_puestos_rel"] = dt;
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
        public void CargarGridPrincipal(int idc_puesto)
        {
            try
            {
                PuestosServiciosENT entidad = new PuestosServiciosENT();
                PuestosServiciosCOM componente = new PuestosServiciosCOM();
                entidad.Idc_Puesto = idc_puesto;
                entidad.PReves = Request.QueryString["val"] != null ? true : false;
                DataSet ds = componente.CargaPuestos(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridPuestos.DataSource = ds.Tables[1];
                    gridPuestos.DataBind();
                   // ScriptManager.RegisterStartupScript(this, GetType(), "noti533W3", "DataTa1();", true);
                    lnktodo.CssClass = Convert.ToBoolean(ds.Tables[0].Rows[0]["todos"]) == true ? "btn btn-success btn-block" : "btn btn-default btn-block";
                    panel_puestos.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["todos"]) == true ? false : true;
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["todos"]) == false)
                    {
                        DataTable dt = Session["tabla_puestos"] as DataTable;
                        foreach (DataRow row in ds.Tables[1].Rows)
                        {
                            DataRow nrow = dt.NewRow();
                            nrow["idc_puesto"] = row["idc_puesto"];
                            dt.Rows.Add(nrow);
                        }
                        Session["tabla_puestos"] = dt;
                    }

                    lblPuesto.Text = ds.Tables[0].Rows[0]["descripcion"].ToString();
                    lblEmpleado.Text = ds.Tables[0].Rows[0]["empleado"].ToString();
                    string rutaimagen = funciones.GenerarRuta("fot_emp", "rw_carpeta");
                    var domn = Request.Url.Host;
                    if (domn == "localhost" || Convert.ToInt32(ds.Tables[0].Rows[0]["idc_empleado"]) == 0)
                    {
                        var url = "imagenes/btn/default_employed.png";
                        imgEmpleado.ImageUrl = url;
                    }
                    else
                    {
                        var url = "http://" + domn + rutaimagen + ds.Tables[0].Rows[0]["idc_empleado"].ToString() + ".jpg";
                        ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                        imgEmpleado.ImageUrl = url;
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void cargarpuestos(string filtro)
        {
            try
            {
                PuestosServiciosCOM componente = new PuestosServiciosCOM();
                Asignacion_RevisionesENT enti = new Asignacion_RevisionesENT();
                enti.Filtro = filtro;
                DataSet dss = componente.CargaComboDinamico(enti);
                repeat_pues.DataSource = dss.Tables[0];
                repeat_pues.DataBind();
                DataTable dt = Session["tabla_puestos"] as DataTable;
                if (dss.Tables[0].Rows.Count > 100)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Gifts('Estamos Cargando " + dss.Tables[0].Rows.Count.ToString() + " Puestos');", true);
                }
                foreach (RepeaterItem item in repeat_pues.Items)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        LinkButton btn = (LinkButton)item.FindControl("lnkpuesto");
                        if (row["idc_puesto"].ToString() == (btn.CommandName) && lnktodo.CssClass == "btn btn-default btn-block")
                        { btn.CssClass = "btn btn-success btn-block"; }
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private string CadenaPuestos()
        {
            string cadena = "";
            DataTable dt = Session["tabla_puestos"] as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                cadena = cadena + row["idc_puesto"].ToString() + ";";
            }

            return cadena;
        }

        private int TotalCadenaPuestos()
        {
            DataTable dt = Session["tabla_puestos"] as DataTable;
            return dt.Rows.Count;
        }

        protected void lnkpuesto_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            btn.CssClass = btn.CssClass == "btn btn-success btn-block" ? "btn btn-default btn-block" : "btn btn-success btn-block";

            if (btn.CssClass == "btn btn-success btn-block")
            {
                DataTable dt = Session["tabla_puestos"] as DataTable;
                DataRow nrow = dt.NewRow();
                nrow["idc_puesto"] = btn.CommandName;
                dt.Rows.Add(nrow);
                Session["tabla_puestos"] = dt;
            }
            else
            {
                DataTable dt = Session["tabla_puestos"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["idc_puesto"].ToString() == btn.CommandName)
                    {
                        row.Delete();
                        break;
                    }
                }
                Session["tabla_puestos"] = dt;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string Confirma_a = (string)Session["Caso_Confirmacion"];
                PuestosServiciosENT entidad = new PuestosServiciosENT();
                PuestosServiciosCOM componente = new PuestosServiciosCOM();
                entidad.Idc_Puesto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"]));
                entidad.Ptotal_cadena = TotalCadenaPuestos();
                entidad.Pcadena = CadenaPuestos();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Ptodos = lnktodo.CssClass == "btn btn-success btn-block" ? true : false;
                entidad.PReves = Request.QueryString["val"] != null ? true : false;
                entidad.Ptotal_cadenaser = totalcadena();
                entidad.Pcadenaser = Cadena();
                string mensaje = "";
                DataSet ds = new DataSet();
                switch (Confirma_a)
                {
                    case "Guardar":
                        ds = componente.AgregarPuesto(entidad);
                        mensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;
                }

                if (mensaje == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "AlertGO('Puestos Guardados correctamente','puestos_catalogo.aspx');", true);
                }
                else
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (TotalCadenaPuestos() == 0)
            {
                Alert.ShowAlertError("Para Guardar, debe seleccionar al menos un puesto", this);
                error = true;
            }
            if (error == false)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Desea Guardar Los Cambios?');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("puestos_catalogo.aspx");
        }

        protected void gridPuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void txtfiltro_TextChanged(object sender, EventArgs e)
        {
            cargarpuestos(txtfiltro.Text);
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            cargarpuestos(txtfiltro.Text);
        }

        protected void lnktodo_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["val"] != null)
            {
                Alert.ShowAlertInfo("No Aplica", "Mensaje del Sistema", this);
            }
            else
            {
                lnktodo.CssClass = lnktodo.CssClass == "btn btn-success btn-block" ? "btn btn-default btn-block" : "btn btn-success btn-block";
                panel_puestos.Visible = lnktodo.CssClass == "btn btn-success btn-block" ? false : true;

                foreach (RepeaterItem item in repeat_pues.Items)
                {
                    LinkButton btn = (LinkButton)item.FindControl("lnkpuesto");
                    btn.CssClass = lnktodo.CssClass;
                }
            }
        }

        protected void btnsele_Click(object sender, EventArgs e)
        {
            btnsele.Text = btnsele.Text == "Seleccionar Todos" ? "Deseleccionar Todos" : "Seleccionar Todos";
            btnsele.CssClass = btnsele.CssClass == "btn btn-success btn-block" ? "btn btn-default btn-block" : "btn btn-success btn-block";

            foreach (RepeaterItem item in repeat_pues.Items)
            {
                LinkButton btn = (LinkButton)item.FindControl("lnkpuesto");
                btn.CssClass = btnsele.CssClass;
            }
        }

        protected void grisservicios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                CheckBox cbx = e.Row.FindControl("cbxeditable") as CheckBox;
                int empleado = Convert.ToInt32(rowView["seleccionado"]);
                int idc_tareaser = Convert.ToInt32(rowView["idc_tareaser"]);
                string descripcion = rowView["descripcion"].ToString();
                cbx.Checked = empleado == 1 ? true : false;
                cbx.Attributes["idc"] = idc_tareaser.ToString();
                cbx.Attributes["descripcion"] = descripcion.ToString();
            }
        }
        protected void CheckBoxProcess_OnCheckedChanged(object sender, EventArgs args)
        {
            CheckBox cbx = sender as CheckBox;
            bool check = cbx.Checked;
            int idc = Convert.ToInt32(cbx.Attributes["idc"].ToString());
            string descripcion = cbx.Attributes["descripcion"].ToString();
            DeleteDT(idc);
            if (check == true)
            {
                AddDt(descripcion,idc);
            }
            CargarGrid();
        }

        protected void grisservicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_tareaser = Convert.ToInt32(grisservicios.DataKeys[index].Values["idc_tareaser"]);
            string url = "tareas_servicios_captura.aspx?view=HEHEHASISEVE&solo_lista=KNWODBWODBWOEBDOWDOWKDBOWEKDBEWBDOWEPOP&idc_tareaser=" + funciones.deTextoa64(idc_tareaser.ToString());
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMesededsage", "window.open('"+url+"');", true);
        }
    }
}