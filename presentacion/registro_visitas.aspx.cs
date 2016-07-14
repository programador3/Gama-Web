using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class registro_visitas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["idc_visitap"] = null;
                Session["idc_visitaemp"] = null;
                Session["idc_visitareg"] = null;
                CargaPuestos("");
                CargarVisitas(0);
            }
        }

        /// <summary>
        /// Regresa tabla con coincidencias de visitantes
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public DataTable CargaPersonas(string filtro)
        {
            try
            {
                VisitasENT entidad = new VisitasENT();
                VisitasCOM componente = new VisitasCOM();
                entidad.Pnombre = filtro;
                DataSet ds = componente.CargaPerfiles(entidad);
                DataTable dt = new DataTable();
                return ds.Tables.Count > 0 ? ds.Tables[0] : dt;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
                return null;
            }
        }

        /// <summary>
        /// Regresa tabla con coincidencias de empresa
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public DataTable CargaEmpresa(string filtro)
        {
            try
            {
                VisitasENT entidad = new VisitasENT();
                VisitasCOM componente = new VisitasCOM();
                entidad.Pnombre = filtro;
                DataSet ds = componente.CargaeMPRESA(entidad);
                DataTable dt = new DataTable();
                return ds.Tables.Count > 0 ? ds.Tables[0] : dt;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
                return null;
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
                ddlPuestoAsigna.DataValueField = "idc_empleado";
                ddlPuestoAsigna.DataTextField = "descripcion_puesto_completa";
                ddlPuestoAsigna.DataSource = ds.Tables[0];
                ddlPuestoAsigna.DataBind();
                if (filtro == "")
                {
                    ddlPuestoAsigna.Items.Insert(0, new ListItem("--Seleccione un Empleado", "0")); //updated code}
                }
                else
                {
                    int IDC = ds.Tables[0].Rows.Count > 0 ? Convert.ToInt32(ddlPuestoAsigna.SelectedValue) : 0;
                    CargarVisitas(IDC);
                    lbltitle.Text = ddlPuestoAsigna.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void CargarVisitas(int idc_empleado)
        {
            try
            {
                VisitasENT entidad = new VisitasENT();
                VisitasCOM componente = new VisitasCOM();
                entidad.Pidc_empleado = idc_empleado;
                DataSet ds = componente.CragarVisitas(entidad);
                gridvisitas.DataSource = ds.Tables[0];
                gridvisitas.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void txtnombre_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtnombre.Text;
            if (filtro != "")
            {
                DataTable dt = CargaPersonas(filtro);
                if (dt.Rows.Count > 0)
                {
                    repeatpersona.DataSource = dt;
                    repeatpersona.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirmPero('Se encontraron " + dt.Rows.Count.ToString() + " coincidencia(s) del visitante " + filtro.ToUpper() + ". Seleccione uno de la lista o indique que sera un nuevo registro.','modal fade modal-info');", true);
                }
                else
                {
                    Session["idc_visitap"] = null;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                }
            }
        }

        protected void lnkpersona_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            Session["idc_visitap"] = lnk.CommandName;
            txtnombre.Text = lnk.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
        }

        protected void lnkpersonas_Click(object sender, EventArgs e)
        {
            Session["idc_visitap"] = null;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
        }

        protected void txtempresa_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtempresa.Text;
            if (filtro != "")
            {
                DataTable dt = CargaEmpresa(filtro);
                if (dt.Rows.Count > 0)
                {
                    repeatempresa.DataSource = dt;
                    repeatempresa.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirmEmpresa('Se encontraron " + dt.Rows.Count.ToString() + " coincidencia(s) de la empresa " + filtro.ToUpper() + ". Seleccione uno de la lista o indique que sera un nuevo registro.','modal fade modal-info');", true);
                }
                else
                {
                    Session["idc_visitaemp"] = null;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                }
            }
        }

        protected void lnkempresa_Click(object sender, EventArgs e)
        {
            Session["idc_visitaemp"] = null;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
        }

        protected void lnkemp_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            Session["idc_visitaemp"] = lnk.CommandName;
            txtempresa.Text = lnk.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            CargaPuestos(txtpuesto_filtro.Text);
        }

        protected void ddlPuestoAsigna_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc = ddlPuestoAsigna.SelectedValue == "" ? 0 : Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
            if (idc == 0)
            {
                Alert.ShowAlertError("Seleccione un Empleado", this);
            }
            else
            {
                CargarVisitas(idc);
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso = (string)Session["Caso_Confirmacion"];
            try
            {
                VisitasENT entidad = new VisitasENT();
                VisitasCOM componente = new VisitasCOM();
                entidad.Pidc_empleado = Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
                int idc_visitap = Session["idc_visitap"] == null ? 0 : Convert.ToInt32(Session["idc_visitap"]);
                int idc_visitaemp = Session["idc_visitaemp"] == null ? 0 : Convert.ToInt32(Session["idc_visitaemp"]);
                entidad.Pidc_visitaemp = idc_visitaemp;
                entidad.Pidc_visitap = idc_visitap;
                entidad.Pmotivo = txtmotivo.Text;
                entidad.Pnombre = txtnombre.Text;
                entidad.Pnombreempresa = txtempresa.Text;
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = new DataSet();
                string vmensaje = "";
                switch (caso)
                {
                    case "Guardar":
                        ds = componente.AgregarVisita(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Terminar":
                        entidad.Pidc_visitareg = Convert.ToInt32(Session["idc_visitareg"]);
                        ds = componente.TerminarVisita(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;
                }
                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos procesando la visita.", "Espere un Momento", "registro_visitas.aspx", "imagenes/loading.gif", "2000", "Visita Procesada Correctamente ", this);
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idc = Convert.ToInt32(ddlPuestoAsigna.SelectedValue);

            if (idc == 0)
            {
                Alert.ShowAlertError("Seleccione un Empleado", this);
            }
            else if (txtnombre.Text == "")
            {
                Alert.ShowAlertError("Escriba el Nombre del Visitante", this);
            }
            else if (txtmotivo.Text == "")
            {
                Alert.ShowAlertError("Escriba el Motivo del Visitante", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar esta Visita.? Se le enviara un correo al Empleado " + ddlPuestoAsigna.SelectedItem + " de manera automatica.','modal fade modal-info');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("registro_visitas.aspx");
        }

        protected void gridvisitas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc = Convert.ToInt32(gridvisitas.DataKeys[index].Values["idc_visitareg"]);
            switch (e.CommandName)
            {
                case "Terminar":
                    Session["idc_visitareg"] = idc;
                    Session["Caso_Confirmacion"] = "Terminar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Terminar esta Visita.? ','modal fade modal-info');", true);

                    break;
            }
        }
    }
}