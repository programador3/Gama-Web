using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class solicitar_asistencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                CargarReloj();
                txtfecha.Text = DateTime.Now.ToString("yyyy-MM-dd").Replace(' ', 'T');                
                if (Request.QueryString["fecha"] != null)
                {
                    txtfecha.Text = funciones.de64aTexto(Request.QueryString["fecha"]);
                    DateTime dt = Convert.ToDateTime(txtfecha.Text);
                    CargarFaltas(dt);
                }
            }
        }
        private void CargarReloj()
        {
            for (int i = 23; i >= 0; i--)
            {
                ddlhora.Items.Insert(0,new ListItem(i.ToString(),i.ToString()));
            }
            for (int i = 59; i >= 0; i--)
            {
                ddlminutos.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
            }
        }
        public void CargarFaltas(DateTime dtime)
        {
            try
            {
                AsistenciaCOM componente = new AsistenciaCOM();
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                int idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
                DataSet ds = componente.sp_reporte_asistencia_web(dtime,idc_usuario,idc_puesto);
                DataTable dt = ds.Tables[0];
                gridservicios.DataSource = dt;
                gridservicios.DataBind();
                lbltotal.Text = dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (txtfecha.Text == "")
            {
                Alert.ShowAlertInfo("Seleccione una Fecha","Mensaje del Sistema",this);
            }
            else {
                DateTime fechastring = Convert.ToDateTime(txtfecha.Text);
                AgentesCOM componente = new AgentesCOM();
                DataTable dt = componente.sp_fn_validar_fechas_pagada(fechastring).Tables[0];
                bool pagado = Convert.ToBoolean(dt.Rows[0][0]);
                if (pagado == true)
                {
                    txtfecha.Text = DateTime.Now.ToString("yyyy-MM-dd").Replace(' ', 'T');
                    Alert.ShowAlertInfo("No puede solicitar un permiso para esta fecha por que el PERIODO DE NOMINA AL QUE PERTENECE YA FUE PAGADO ", "Mensaje del Sistema", this);
                }
                else if (fechastring < DateTime.Today)
                {
                    txtfecha.Text = DateTime.Now.ToString("yyyy-MM-dd").Replace(' ', 'T');
                    Alert.ShowAlertInfo("No puede solicitar un permiso para una fecha menor a hoy.", "Mensaje del Sistema", this);

                }
                else
                {
                    DateTime dtDD = Convert.ToDateTime(txtfecha.Text);
                    CargarFaltas(dtDD);
                }
            }
        }

        protected void gridservicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            //IDC_EMPLEADO,IDC_PUESTO,num_nomina,empleado,puesto,depto,horario,hora_asistencia
            string IDC_EMPLEADO = gridservicios.DataKeys[index].Values["IDC_EMPLEADO"].ToString();
            string IDC_PUESTO = gridservicios.DataKeys[index].Values["IDC_PUESTO"].ToString();
            string num_nomina = gridservicios.DataKeys[index].Values["num_nomina"].ToString();
            string empleado = gridservicios.DataKeys[index].Values["empleado"].ToString();
            string puesto = gridservicios.DataKeys[index].Values["puesto"].ToString();
            string depto = gridservicios.DataKeys[index].Values["depto"].ToString();
            string horario = gridservicios.DataKeys[index].Values["horario"].ToString();
            string hora_asistencia = gridservicios.DataKeys[index].Values["hora_asistencia"].ToString();
            DateTime asistencia = Convert.ToDateTime(gridservicios.DataKeys[index].Values["asistencia"]);
            bool tuvo_asistencia = gridservicios.DataKeys[index].Values["hora_checo_real"].ToString() != "";
            switch (e.CommandName)
            {
                case "Puestos":
                    ddlhora.Enabled = !tuvo_asistencia;
                    ddlminutos.Enabled = !tuvo_asistencia;
                    ddlhora.SelectedValue = tuvo_asistencia ? asistencia.Hour.ToString() : "0";
                    ddlminutos.SelectedValue = tuvo_asistencia ? asistencia.Minute.ToString() : "0";
                    txtfechaview.Text= Convert.ToDateTime(txtfecha.Text).ToString("dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                    txtnomina.Text = num_nomina.Trim();
                    txtidc_empleado.Text = IDC_EMPLEADO.Trim();
                    txtempleado.Text = empleado;
                    txthoracheco.Text = hora_asistencia;
                    txtmotivo.Text = "";
                    cbxllegadatemprano.Checked = false;
                    Session["Caso_Confirmacion"] = e.CommandName;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alewswsrtMessage", 
                        "ModalConfirm('Nueva Solicitud','modal fade modal-info');", true);

                    break;
                case "asistencia":
                    string url = "asistencia_detalle.aspx?top=" + funciones.deTextoa64("15") + "&num_nomina=" + funciones.deTextoa64(num_nomina)+"&idc_empleado="+funciones.deTextoa64(IDC_EMPLEADO);
                    ScriptManager.RegisterStartupScript(this, GetType(), "KWDOIQNDW9929929H",
                        "window.open('"+url+"');", true);
                    break;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                switch (caso)
                {
                    case "Puestos":
                        if (txtmotivo.Text == "")
                        {
                            error_modal.Visible = true;
                            lblerror.Text = "Ingrese el Motivo";
                        }
                        else if (ddlhora.Enabled && ddlhora.SelectedValue == "0" && ddlminutos.Enabled && ddlminutos.SelectedValue == "0")
                        {
                            error_modal.Visible = true;
                            lblerror.Text = "Ingrese la hora de llegada del empleado";
                        }

                        //else if (Convert.ToDateTime(txtfecha.Text).Day != Convert.ToDateTime(txthora.Text).Day || Convert.ToDateTime(txtfecha.Text).Month != Convert.ToDateTime(txthora.Text).Month
                        //    || Convert.ToDateTime(txtfecha.Text).Year != Convert.ToDateTime(txthora.Text).Year)
                        //{
                        //    error_modal.Visible = true;
                        //    lblerror.Text = "La fecha de llegada del empleado no es valida";
                        //}
                        else {
                            DateTime PFECHA = Convert.ToDateTime(txtfecha.Text);
                            DateTime PFECHA2 = Convert.ToDateTime(txtfecha.Text).AddHours(Convert.ToInt32(ddlhora.SelectedValue)).AddMinutes(Convert.ToInt32(ddlminutos.SelectedValue));
                            int IDC_EMPLEADO = Convert.ToInt32(txtidc_empleado.Text.Trim());
                            string obser = txtmotivo.Text.ToUpper().Trim();
                            bool aviso = false;
                            bool llegada = cbxllegadatemprano.Checked;
                            bool trabajo = true;
                            string fecha_str = PFECHA2.ToString("yyyy-dd-MM HH:mm:ss");
                            AsistenciaCOM componente = new AsistenciaCOM();
                            DataSet ds = componente.sp_masistencia_observ_nuevo(PFECHA, fecha_str, 
                                Convert.ToInt32(Session["sidc_usuario"]),                                
                                IDC_EMPLEADO, trabajo, obser, llegada, aviso);
                            string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                            if (vmensaje == "")
                            {
                                txtfechaview.Text = "";
                                txtnomina.Text = "";
                                txtidc_empleado.Text = "";
                                txtempleado.Text = "";
                                txthoracheco.Text = "";
                                CargarReloj();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alewswedededsrtMessage",
                                 "ModalClose();", true);
                                string url = "solicitar_asistencia.aspx?fecha="+funciones.deTextoa64(txtfecha.Text);
                                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                                 "AlertGO('Solicitud Agregada Correctamente','"+url+"');", true);
                            }
                            else
                            {
                                error_modal.Visible = true;
                                lblerror.Text = vmensaje;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                error_modal.Visible = true;
                lblerror.Text = ex.ToString();
                Global.CreateFileError(ex.ToString(), this);
            }
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            txtidc_empleado.Text = "";
            error_modal.Visible = false;
        }

       
    }
}