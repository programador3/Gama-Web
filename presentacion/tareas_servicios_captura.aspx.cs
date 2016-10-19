using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class tareas_servicios_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("puesto");
                dt.Columns.Add("idc_puesto");
                ViewState["dt_puestos_rel"] = dt;
                if (Request.QueryString["idc_tareaser"] != null)
                {
                    if (Request.QueryString["solo_lista"] != null)
                    {
                        txtdescripcion.ReadOnly = true;
                        txthoras.ReadOnly = true;
                        txtobservaciones.ReadOnly = true;
                        cbxeditable.Enabled = false;
                    }
                    if (Request.QueryString["view"] != null)
                    {
                        btnCancelar.Visible = false;
                        btnGuardar.Visible = false;
                        btncrr.Visible = true;
                        txtbuscar.Attributes["placeholder"] = "PAGINA SOLO INFORMATIVA, NO PUEDE BUSCAR";
                        txtbuscar.ReadOnly = true;
                        div_puestos.Visible = false;
                        gridpuestos.Columns[0].Visible = false;
                    }
                    int idc_tareaser = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tareaser"]));
                    TareasServicios(idc_tareaser);
                }
                else {
                    CargarCombosPuestos("");
                }

            }
        }


        private void AddDt(string puesto, int idc_puesto)
        {
            try
            {
                DataTable dt = ViewState["dt_puestos_rel"] as DataTable;
                DataRow row = dt.NewRow();
                row["puesto"] = puesto;
                row["idc_puesto"] = idc_puesto;
                dt.Rows.Add(row);
                ViewState["dt_puestos_rel"] = dt;
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
            gridpuestos.DataSource = dtc;
            gridpuestos.DataBind();
        }

        private void DeleteDT(int idc_puesto)
        {
            try
            {
                DataTable dt = ViewState["dt_puestos_rel"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    int idc_p = Convert.ToInt32(row["idc_puesto"]);
                    if (idc_p == idc_puesto)
                    {
                        row.Delete();
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

        private void CargarCombosPuestos(string filtro)
        {
            try
            {
                Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
                Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
                entidad.Filtro = filtro;
                DataSet ds = componente.CargaComboDinamico(entidad);
                repeat_pues.DataSource = ds.Tables[0];
                repeat_pues.DataBind();
                if (ds.Tables[0].Rows.Count > 100)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Gifts('Estamos Cargando " + ds.Tables[0].Rows.Count.ToString() + " Puestos');", true);
                }
                MarcarItemsCblist();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void MarcarItemsCblist()
        {
            try
            {
                DataTable dt = ViewState["dt_puestos_rel"] as DataTable;
                DataView dv = dt.DefaultView;
                foreach (RepeaterItem item in repeat_pues.Items)
                {
                    LinkButton btn = (LinkButton)item.FindControl("lnkpuesto");
                    int idc_puesto = Convert.ToInt32(btn.CommandName);
                    dv.RowFilter = "idc_puesto = " + idc_puesto + "";
                    btn.CssClass = dv.ToTable().Rows.Count > 0 ? "btn btn-success btn-block" : "btn btn-default btn-block";
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void TareasServicios(int idc_tareaser)
        {
            try
            {
                TareasCOM componente = new TareasCOM();
                DataSet ds = componente.sp_tareas_servicios_puestos_SERVICO(idc_tareaser);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtdescripcion.Text = dt.Rows[0]["descripcion"].ToString();
                    txthoras.Text = dt.Rows[0]["intervalo_tiempo"].ToString();
                    txtobservaciones.Text = dt.Rows[0]["observaciones"].ToString();
                    cbxeditable.Checked = Convert.ToBoolean(dt.Rows[0]["editable"]);

                }
                if (ds.Tables.Count > 1)
                {
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        string puesto = row["puesto"].ToString();
                        int idc_puesto = Convert.ToInt32(row["idc_puesto"]);
                        DeleteDT(idc_puesto);
                        AddDt(puesto, idc_puesto);
                    }
                    DataTable dtinfo = ViewState["dt_puestos_rel"] as DataTable;
                    if (dtinfo.Rows.Count > 0)
                    {
                        DataTable dtc = dtinfo.Copy();
                        dtc.Columns["puesto"].ColumnName = "descripcion_puesto_completa";
                        repeat_pues.DataSource = dtc;
                        repeat_pues.DataBind();
                        if (dtinfo.Rows.Count > 100)
                        {
                            if (Request.QueryString["view"] == null)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Gifts('Estamos Cargando " + ds.Tables[0].Rows.Count.ToString() + " Puestos');", true);

                            }
                            else {
                                Alert.ShowAlertInfo("El servicio no tiene ningun puesto relacionado.","Mensaje del Sistema",this);
                            }
                        }
                        MarcarItemsCblist();
                        lnksele.CssClass = "btn btn-success";
                    }
                    else {
                        if (Request.QueryString["view"] == null)
                        {

                            CargarCombosPuestos("");
                        }
                        else
                        {
                            Alert.ShowAlertInfo("El servicio no tiene ningun puesto relacionado.", "Mensaje del Sistema", this);
                        }
                    }
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
                cadena = cadena + row["idc_puesto"].ToString().Trim() + ";";
            }
            return cadena;
        }

        private int totalcadena()
        {
            DataTable dt = ViewState["dt_puestos_rel"] as DataTable;
            return dt.Rows.Count;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("catalogo_tareas_servicios.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtdescripcion.Text == "")
            {
                Alert.ShowAlertError("Escriba una descripcion",this);
            }
            else if (txthoras.Text == "" || txthoras.Text =="0")
            {
                Alert.ShowAlertError("Escriba un tiempo de respuesta que se encuentre entre 1 - 240 horas", this);
            }
            else 
            {
                Session["Caso_Confirmacion"] = Request.QueryString["idc_tareaser"] == null ? "Guardar":"Editar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Desea Guardar el Servicio ?','modal fade modal-info');", true);

            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                TareasCOM componente = new TareasCOM();

                DataSet ds = new DataSet();
                switch (caso)
                {
                    case "Editar":
                        ds = componente.sp_mtareas_servicios(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tareaser"])), txtdescripcion.Text.Trim().ToUpper(), txtobservaciones.Text, cbxeditable.Checked, Convert.ToInt32(txthoras.Text.Trim()),
                           Cadena(), totalcadena(), Convert.ToInt32(Session["sidc_usuario"]),
                       funciones.GetLocalIPAddress(), funciones.GetPCName(), funciones.GetUserName());
                        break;
                    case "Guardar":
                       ds = componente.sp_atareas_servicios(txtdescripcion.Text.Trim().ToUpper(),txtobservaciones.Text, cbxeditable.Checked, Convert.ToInt32(txthoras.Text.Trim()),
                            Cadena(),totalcadena(), Convert.ToInt32(Session["sidc_usuario"]),
                        funciones.GetLocalIPAddress(), funciones.GetPCName(), funciones.GetUserName());
                        break;
                }
                
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos procesando la solicitud.", "Espere un Momento", "catalogo_tareas_servicios.aspx", "imagenes/loading.gif", "1000", "Información Procesada Correctamente", this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
           
        }

        protected void txtbuscar_TextChanged(object sender, EventArgs e)
        {

            lnksele.CssClass = "btn btn-default";
            CargarCombosPuestos(txtbuscar.Text);
        }

        protected void lnkpuesto_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            btn.CssClass = btn.CssClass == "btn btn-success btn-block" ? "btn btn-default btn-block" : "btn btn-success btn-block";
            int idc_puesto = Convert.ToInt32(btn.CommandName);
            string puesto = btn.CommandArgument;

            DeleteDT(idc_puesto);
            if (btn.CssClass == "btn btn-success btn-block")//agregado
            {
                AddDt(puesto, idc_puesto);
            }
            CargarGrid();
        }

        protected void gridpuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable dt = ViewState["dt_puestos_rel"] as DataTable;
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_puesto = Convert.ToInt32(gridpuestos.DataKeys[index].Values["idc_puesto"]);
            DeleteDT(idc_puesto);

            if (dt.Rows.Count <= 0)
            {
                foreach (RepeaterItem item in repeat_pues.Items)
                {
                    LinkButton btn = (LinkButton)item.FindControl("lnkpuesto");
                    btn.CssClass = "btn btn-default btn-block";
                }
            }
            else
            {
                foreach (RepeaterItem item in repeat_pues.Items)
                {
                    LinkButton btn = (LinkButton)item.FindControl("lnkpuesto");
                    int idc_puestor = Convert.ToInt32(btn.CommandName);
                    if (idc_puestor == idc_puesto)
                    {
                        btn.CssClass = "btn btn-default btn-block";
                    }
                }
            }
            CargarGrid();
        }

        protected void lnksele_Click(object sender, EventArgs e)
        {
            lnksele.CssClass = lnksele.CssClass == "btn btn-default" ? "btn btn-success" : "btn btn-default";
            if (repeat_pues.Items.Count > 100)
            {
                if (lnksele.CssClass == "btn btn-success")//agregado
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Gifts('Estamos Agreganado los " + repeat_pues.Items.Count.ToString() + " Puestos');", true);
                }
                else {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Gifts('Estamos Eliminando los " + repeat_pues.Items.Count.ToString() + " Puestos');", true);
                }
            }
            foreach (RepeaterItem item in repeat_pues.Items)
            {
                LinkButton btn = (LinkButton)item.FindControl("lnkpuesto");
                int idc_puesto = Convert.ToInt32(btn.CommandName);
                string puesto = btn.CommandArgument;
                btn.CssClass = lnksele.CssClass + " btn-block";
                DeleteDT(idc_puesto);
                if (btn.CssClass == "btn btn-success btn-block")//agregado
                {
                    AddDt(puesto, idc_puesto);
                }
                CargarGrid();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            CargarGrid();
        }
    }
}