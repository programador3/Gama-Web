using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class rendimiento_tareas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            DEPTO.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 340);
            if (!IsPostBack)
            {
                txtfechafin.Text = DateTime.Now.ToString("yyyy-MM-dd").Replace(' ', 'T');
                txtfechainicio.Text = DateTime.Now.ToString("yyyy-MM-dd").Replace(' ', 'T');
                CargaPuestos("");
                CargaDeptos();
            }
        }

        /// <summary>
        /// Carga Puestos en Filtro
        /// </summary>
        public void CargaPuestos(string filtro)
        {
            try
            {
                Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
                Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
                entidad.Filtro = filtro;
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = componente.CargaComboDinamicoOrgn(entidad);
                ddlPuestoAsigna.DataValueField = "idc_puesto";
                ddlPuestoAsigna.DataTextField = "descripcion_puesto_completa";
                ddlPuestoAsigna.DataSource = ds.Tables[0];
                ddlPuestoAsigna.DataBind();
                if (filtro == "")
                {
                    ddlPuestoAsigna.Items.Insert(0, new ListItem("Seleccione un Puesto", "0")); //updated code}
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void CargaDeptos()
        {
            try
            {
                Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
                Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
                DataSet ds = componente.CargaComboDeptos(entidad);
                ddldeptos.DataValueField = "idc_depto";
                ddldeptos.DataTextField = "nombre";
                ddldeptos.DataSource = ds.Tables[0];
                ddldeptos.DataBind();
                ddldeptos.Items.Insert(0, new ListItem("Seleccione un Departamento", "0")); //updated code}
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void ddlPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc_puesto = Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
            if (idc_puesto == 0)
            {
                Alert.ShowAlertError("Seleccione un Puesto Valido, o intente buscando uno.", this);
            }
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            CargaPuestos(txtpuesto_filtro.Text);
        }

        protected void lnkir_Click(object sender, EventArgs e)
        {
            bool error = false;
            bool permiso = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 340);
            if (txtfechainicio.Text == "" || txtfechafin.Text == "")
            {
                Alert.ShowAlertError("Debe seleccionar una fecha de inicio y una fecha de fin", this);
                error = true;
            }
            int pidc_puesto = ddlPuestoAsigna.SelectedValue == "0" || ddlPuestoAsigna.SelectedValue == "" ? 0 : Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
            if (ddlPuestoAsigna.SelectedValue == "0" && permiso == false)
            {
                Alert.ShowAlertError("Debe seleccionar un puesto", this);
                error = true;
            }
            if (error == false)
            {
                string idc_depto = funciones.deTextoa64(ddldeptos.SelectedValue);
                String url = HttpContext.Current.Request.Url.AbsoluteUri;
                String path_actual = url.Substring(url.LastIndexOf("/") + 1);
                url = url.Replace(path_actual, "");
                url = url + "grafica.aspx?pidc_depto=" + idc_depto + "&idc_puesto=" + funciones.deTextoa64(pidc_puesto.ToString()) + "&fecha_inicio=" + txtfechainicio.Text + "&fecha_fin=" + txtfechafin.Text;
                ScriptManager.RegisterStartupScript(this, GetType(), "noti533ssW3", "window.open('" + url + "');", true);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            bool error = false;
            bool permiso = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 340);
            if (txtfechainicio.Text == "" || txtfechafin.Text == "")
            {
                Alert.ShowAlertError("Debe seleccionar una fecha de inicio y una fecha de fin", this);
                error = true;
            }
            if (ddlPuestoAsigna.SelectedValue == "0" && permiso == false)
            {
                Alert.ShowAlertError("Debe seleccionar un puesto", this);
                error = true;
            }

            if (error == false)
            {
                String url = HttpContext.Current.Request.Url.AbsoluteUri;
                String path_actual = url.Substring(url.LastIndexOf("/") + 1);
                url = url.Replace(path_actual, "");
                url = url + "grafica.aspx?idc_puesto=" + funciones.deTextoa64(ddlPuestoAsigna.SelectedValue) + "&fecha_inicio=" + txtfechainicio.Text + "&fecha_fin=" + txtfechafin.Text;
                string id_puesto = ddlPuestoAsigna.SelectedValue == "0" ? funciones.deTextoa64("0") : funciones.deTextoa64(ddlPuestoAsigna.SelectedValue);
                string idc_depto = funciones.deTextoa64(ddldeptos.SelectedValue);
                Response.Redirect("rendimiento_tareas_detalles.aspx?pidc_depto=" + idc_depto + "&pidc_puesto=" + id_puesto + "&inicio=" + funciones.deTextoa64(txtfechainicio.Text) + "&fin=" + funciones.deTextoa64(txtfechafin.Text));
            }
        }

        /// <summary>
        /// Devuele todos los datos de la tabla puestos sin filtrar
        /// </summary>
        /// <returns></returns>
        public DataTable TablaGlobal()
        {
            DataTable table = new DataTable();
            Nuevas_AprobacionesENT entidad = new Nuevas_AprobacionesENT();
            Nuevas_AprobacionesCOM componente = new Nuevas_AprobacionesCOM();
            try
            {
                table = componente.getDataPuestos(entidad).Tables[0];
                return table;
            }
            catch (Exception EX)
            {
                Alert.ShowAlertError(EX.ToString(), this);
            }
            return table;
        }

        protected void ddldeptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int depto = Convert.ToInt32(ddldeptos.SelectedValue);
            Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
            Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
            entidad.Filtro = "";
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            DataSet ds = componente.CargaComboDinamicoOrgn(entidad);
            DataView view = ds.Tables[0].DefaultView;
            view.RowFilter = "idc_depto = " + depto + "";
            ddlPuestoAsigna.DataValueField = "idc_puesto";
            ddlPuestoAsigna.DataTextField = "descripcion_puesto_completa";
            ddlPuestoAsigna.DataSource = view.ToTable();
            ddlPuestoAsigna.DataBind();
            ddlPuestoAsigna.Items.Insert(0, new ListItem("Seleccione un Puesto", "0")); //updated code}
        }
    }
}