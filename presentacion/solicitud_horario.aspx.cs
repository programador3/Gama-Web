﻿using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class solicitud_horario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            //SI ES UN ALTA
            if (!IsPostBack && Request.QueryString["autoriza"] == null)
            {
                Session["idc_horario_perm"] = null;
                Session["checacomida"] = null;
                solicita.Visible = true;
                lblobsr.Visible = false;
                lnkedit.Visible = false;
                edicion.Visible = false;
                autoriza.Visible = false;
                Session["pidc_empleado_solic_horario"] = null;
                CargarGridPrincipal(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"])));
            }
            //SI ES UNA AUTORIZACION
            if (!IsPostBack && Request.QueryString["autoriza"] != null)
            {
                Session["idc_horario_perm"] = null;
                Session["checacomida"] = null;
                Session["pidc_empleado_solic_horario"] = null;
                CargarGridPrincipal(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"])));
                CargarDetallesl(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_horario_perm"])));
                solicita.Visible = false;
                lblobsr.Visible = true;
                lnkedit.Visible = true;
                edicion.Visible = false;
                autoriza.Visible = true;
            }
            //SI ES UNA VISTA
            if (!IsPostBack && Request.QueryString["view"] != null)
            {
                Session["idc_horario_perm"] = null;
                Session["checacomida"] = null;
                Session["pidc_empleado_solic_horario"] = null;
                CargarGridPrincipal(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"])));
                CargarDetallesl(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_horario_perm"])));
                solicita.Visible = false;
                lblobsr.Visible = false;
                lnkedit.Visible = false;
                edicion.Visible = false;
                autoriza.Visible = false;

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "DE", "Charge();", true);
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
                entidad.PReves = false;
                DataSet ds = componente.CargaPuestos(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblPuesto.Text = ds.Tables[0].Rows[0]["descripcion"].ToString();
                    lblEmpleado.Text = ds.Tables[0].Rows[0]["empleado"].ToString();
                    lbldepto.Text = ds.Tables[0].Rows[0]["depto"].ToString();
                    Session["pidc_empleado_solic_horario"] = Convert.ToInt32(ds.Tables[0].Rows[0]["idc_empleado"]);
                    Session["checacomida"] = Convert.ToBoolean(ds.Tables[0].Rows[0]["CHECACOMIDA"]);
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
                //sucursales
                Asignacion_RevisionesENT ent = new Asignacion_RevisionesENT();
                Asignacion_RevisionesCOM com = new Asignacion_RevisionesCOM();
                ddlsucursales.DataTextField = "nombre";
                ddlsucursales.DataValueField = "idc_sucursal";
                ddlsucursales.DataSource = com.CargaSucursales(ent).Tables[0];
                ddlsucursales.DataBind();
                ddlsucursales.Items.Insert(0, new ListItem("Seleccione una Sucursal", "0")); //updated code}
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void CargarDetallesl(int idc_horario_perm)
        {
            try
            {
                Solicitud_HorarioENT entidad = new Solicitud_HorarioENT();
                SolicitudHorarioCOM componente = new SolicitudHorarioCOM();
                entidad.Pidc_horario_erm = idc_horario_perm;
                DataSet ds = componente.SolcitudDetalles(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    txtfecha.Text = Convert.ToDateTime(row["fecha_textbox"]).ToString("yyyy-MM-dd");
                    txthoraentrada.Text = row["hora_entrada"].ToString() == "00:00" ? "" : row["hora_entrada"].ToString();
                    txthorasalida.Text = row["hora_salida"].ToString() == "00:00" ? "" : row["hora_salida"].ToString();
                    txthoraentradac.Text = row["hora_entrada_comida"].ToString() == "00:00" ? "" : row["hora_entrada_comida"].ToString();
                    txthorasalidac.Text = row["hora_salida_comida"].ToString() == "00:00" ? "" : row["hora_salida_comida"].ToString();
                    ddlsucursales.SelectedValue = Convert.ToInt32(row["idc_sucursal"]).ToString();
                    lblobsr.Text = "Observaciones de la Solicitud: " + row["observaciones"].ToString();
                    lnkno_horacomida.CssClass = Convert.ToBoolean(row["no_comida"]) == true ? "btn btn-success btn-block" : "btn btn-default btn-block";
                    lnkno_Salida.CssClass = Convert.ToBoolean(row["no_salida"]) == true ? "btn btn-success btn-block" : "btn btn-default btn-block";
                    txtfecha.Enabled = false;
                    ddlsucursales.Enabled = false;
                    txthoraentrada.Enabled = false;
                    txthoraentradac.Enabled = false;
                    txthorasalida.Enabled = false;
                    txthorasalidac.Enabled = false;

                    if (txthoraentrada.Text == "" &&
                        txthorasalida.Text == "" &&
                        txthorasalidac.Text == "" &&
                        txthoraentradac.Text == "" &&
                        Convert.ToBoolean(row["no_comida"]) == true &&
                        Convert.ToBoolean(row["no_salida"]) == true)
                    {
                        btntot.CssClass = "btn btn-success btn-block";
                        cuerpo.Visible = false;
                    }
                }
                else
                {
                    Alert.ShowAlertError("No hay DATOS", this.Page);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Verifica si existe una solicitud para un determinado dia
        /// </summary>
        /// <param name="idc_empleado"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        private bool ExisteSolicituenDia(int idc_empleado, DateTime fecha)
        {
            try
            {
                bool ret = false;
                Solicitud_HorarioENT entidad = new Solicitud_HorarioENT();
                SolicitudHorarioCOM componente = new SolicitudHorarioCOM();
                entidad.Pidc_empleado = idc_empleado;
                entidad.Pfecha = fecha;
                DataSet ds = componente.ComprobaciondeDia(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ret = true;
                    DataRow row = ds.Tables[0].Rows[0];
                    txtfecha.Text = Convert.ToDateTime(row["fecha_textbox"]).ToString("yyyy-MM-dd");
                    txthoraentrada.Text = row["hora_entrada"].ToString() == "00:00" ? "" : row["hora_entrada"].ToString();
                    txthorasalida.Text = row["hora_salida"].ToString() == "00:00" ? "" : row["hora_salida"].ToString();
                    txthoraentradac.Text = row["hora_entrada_comida"].ToString() == "00:00" ? "" : row["hora_entrada_comida"].ToString();
                    txthorasalidac.Text = row["hora_salida_comida"].ToString() == "00:00" ? "" : row["hora_salida_comida"].ToString();
                    ddlsucursales.SelectedValue = Convert.ToInt32(row["idc_sucursal"]).ToString();
                    lblobsr.Text = "Observaciones de la Solicitud: " + row["observaciones"].ToString();
                    int idc = Convert.ToInt32(row["idc_horario_perm"]);
                    Session["idc_horario_perm"] = idc;
                    ddlsucursales.Enabled = false;
                    txthoraentrada.Enabled = false;
                    txthoraentradac.Enabled = false;
                    txthorasalida.Enabled = false;
                    txthorasalidac.Enabled = false;
                    solicita.Visible = true;
                    lblobsr.Visible = true;
                    lnkedit.Visible = true;
                    edicion.Visible = false;
                    btncancelarsol.Visible = row["STATUS"].ToString().Trim().ToUpper() == "P";
                    lnkno_horacomida.CssClass = "btn btn-default btn-block";
                    lnkno_Salida.CssClass = "btn btn-default btn-block";
                    lnkno_horacomida.CssClass = Convert.ToBoolean(row["no_comida"]) == true ? "btn btn-success btn-block" : "btn btn-default btn-block";
                    lnkno_Salida.CssClass = Convert.ToBoolean(row["no_salida"]) == true ? "btn btn-success btn-block" : "btn btn-default btn-block";
                    if (!Convert.ToBoolean(row["no_comida"]) && !Convert.ToBoolean(row["no_salida"]) && txthoraentrada.Text == "" && txthoraentradac.Text == "" && txthorasalida.Text == "" && txthorasalidac.Text == "" && ddlsucursales.SelectedValue == "0")
                    {
                        btntot.CssClass = "btn btn-success btn-block";
                        cuerpo.Visible = false;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                return false;
            }
        }

        protected void txtfecha_TextChanged(object sender, EventArgs e)
        {
            btncancelarsol.Visible = false;
            if (txtfecha.Text != "")
            {
                DateTime fechastring = Convert.ToDateTime(txtfecha.Text);
                AgentesCOM componente = new AgentesCOM();
                DataTable dt = componente.sp_fn_validar_fechas_pagada(fechastring).Tables[0];
                bool pagado = Convert.ToBoolean(dt.Rows[0][0]);
                if (pagado==true)
                {
                    txtfecha.Text = DateTime.Now.ToString("yyyy-MM-dd").Replace(' ', 'T');
                    Alert.ShowAlertInfo("No puede solicitar un permiso para esta fecha por que el PERIODO DE NOMINA AL QUE PERTENECE YA FUE PAGADO ","Mensaje del Sistema", this);
                }

                else
                {
                    btnguardar.Visible = true;
                    btncancelarsol.Visible = false;
                    if (ExisteSolicituenDia(Convert.ToInt32(Session["pidc_empleado_solic_horario"]), Convert.ToDateTime(txtfecha.Text)))
                    {
                        btnguardar.Visible = false;
                        Alert.ShowAlertInfo("Este Empleado ya tiene una Solicitud Pendiente o Autorizada el dia " + Convert.ToDateTime(txtfecha.Text).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX")), "Mensaje", this);
                    }
                }
            }
        }

        protected void ddlsucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc = Convert.ToInt32(ddlsucursales.SelectedValue);
            if (idc == 0)
            {
                Alert.ShowAlertError("Seleccione una Sucursal.", this);
            }
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            if (txthorasalidac.Text == "" && txthoraentradac.Text != "")
            {
                Alert.ShowAlertError("Ingrese la hora de salida a comer", this);
            }
            else if (txthorasalidac.Text != "" && txthoraentradac.Text == "")
            {
                Alert.ShowAlertError("Ingrese la hora de entrada a comer", this);
            }
            else if (txtobservaciones.Text == "" && Request.QueryString["autoriza"] == null)
            {
                Alert.ShowAlertError("Ingrese Observaciones", this);
            }
            else if (txtobservaciones.Text.Contains("asistencia de salida") && Request.QueryString["autoriza"] == null)
            {
                Alert.ShowAlertError("No existe la ASISTENCIA DE SALIDA. \\n Cambie sus Observaciones", this);
            }
            else if (txtfecha.Text == "")
            {
                Alert.ShowAlertError("Ingrese la fecha de aplicación.", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar esta Solicitud de Cambio de Fecha?','modal fade modal-info');", true);
            }
        }

        protected void txthoraentrada_TextChanged(object sender, EventArgs e)
        {
            if (txthoraentrada.Text == "")
            {
                ddlsucursales.SelectedValue = "0";
                ddlsucursales.Enabled = false;
            }
            else
            {
                ddlsucursales.Enabled = true;
                string t = txthoraentrada.Text.Replace(" ", "");
                int ter = funciones.ReturnMinutesfromString(t);
                if (ter > 1260 || ter < 360)
                {
                    Alert.ShowAlertInfo("Los Horarios deben ser entre 6 de la mañana (6:00) y 9 de la noche(21:00) .", "Mensaje del Sistema", this);
                }
                int he = txthoraentrada.Text == "" ? 0 : Convert.ToInt32(txthoraentrada.Text.Replace(":", ""));
                int hc1 = txthoraentradac.Text == "" ? 0 : Convert.ToInt32(txthoraentradac.Text.Replace(":", ""));
                int hc2 = txthorasalidac.Text == "" ? 0 : Convert.ToInt32(txthorasalidac.Text.Replace(":", ""));
                int hs = txthorasalida.Text == "" ? 0 : Convert.ToInt32(txthorasalida.Text.Replace(":", ""));
                if (hc1 > 0 && hc2 > 0)
                {
                    if (hc2 < he || hc1 < he)
                    {
                        divchecacomida.Visible = false;
                        txthoraentrada.Text = "";
                        Alert.ShowAlertInfo("El horario de Entrada debe ser el menor a los demas.", "Mensaje del sistema", this);
                    }
                }
                if (he > hs && hs > 0)
                {
                    divchecacomida.Visible = false;
                    txthoraentrada.Text = "";
                    Alert.ShowAlertInfo("El horario de Salida debe ser el menor a los demas.", "Mensaje del sistema", this);
                }
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                Solicitud_HorarioENT entidad = new Solicitud_HorarioENT();
                SolicitudHorarioCOM componente = new SolicitudHorarioCOM();
                entidad.Pobservaciones = txtobservaciones.Text;
                entidad.Pidc_empleado = Convert.ToInt32(Session["pidc_empleado_solic_horario"]);
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pidc_sucursal = Convert.ToInt32(ddlsucursales.SelectedValue);
                entidad.Phora_entrada = txthoraentrada.Text == "" ? 0 : funciones.ReturnMinutesfromString(txthoraentrada.Text.Replace(" ", ""));
                entidad.Phora_salida = txthorasalida.Text == "" ? 0 : funciones.ReturnMinutesfromString(txthorasalida.Text.Replace(" ", ""));
                entidad.Phora_entrada_comida = txthoraentradac.Text == "" ? 0 : funciones.ReturnMinutesfromString(txthoraentradac.Text.Replace(" ", ""));
                entidad.Phora_salida_comida = txthorasalidac.Text == "" ? 0 : funciones.ReturnMinutesfromString(txthorasalidac.Text.Replace(" ", ""));
                entidad.Pfecha = Convert.ToDateTime(txtfecha.Text);
                entidad.Pno_comida = lnkno_horacomida.CssClass == "btn btn-success btn-block" ? true : false;
                entidad.Pno_salida = lnkno_Salida.CssClass == "btn btn-success btn-block" ? true : false;
                DataSet ds;
                string vmensaje = "";
                switch (caso)
                {
                    case "Guardar":

                        entidad.Pstatus = "N";
                        ds = componente.Solcitud(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            string MESS = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 363) == true ? "Estamos Autorizando Automaticamente la Solicitud" : "Estamos Guardando los cambios.";
                            Alert.ShowGiftMessage(MESS, "Espere un Momento", "solicitud_horario.aspx?idc_puesto="+Request.QueryString["idc_puesto"], "imagenes/loading.gif", "2000", "Solicitud Guardada correctamente ", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                        break;

                    case "Autorizar":
                        entidad.Pidc_horario_erm = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_horario_perm"]));
                        entidad.Pstatus = "A";
                        ds = componente.Autorizar(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos Guardando los cambios.", "Espere un Momento", "permisos_pendientes.aspx", "imagenes/loading.gif", "2000", "La Solicitud fue Autorizada correctamente ", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                        break;
                    case "Cancelar":
                        entidad.Pidc_horario_erm = Convert.ToInt32(Session["idc_horario_perm"]);
                        entidad.Pstatus = "R";
                        ds = componente.Autorizar(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos Guardando los cambios.", "Espere un Momento", "solicitud_horario.aspx?idc_puesto=" + Request.QueryString["idc_puesto"], "imagenes/loading.gif", "2000", "La Solicitud fue cANCELADA correctamente ", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                        break;
                    case "Rechazar":
                        entidad.Pidc_horario_erm = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_horario_perm"]));
                        entidad.Pstatus = "R";
                        ds = componente.Autorizar(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos Guardando los cambios.", "Espere un Momento", "permisos_pendientes.aspx", "imagenes/loading.gif", "2000", "La Solicitud fue Rechazada correctamente ", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                        break;

                    case "Editar":
                        entidad.Pidc_horario_erm = Request.QueryString["idc_horario_perm"] == null ? Convert.ToInt32(Session["idc_horario_perm"]) : Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_horario_perm"]));
                        entidad.Pstatus = "E";
                        ds = componente.SolcitudEdicion(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            string url = Request.QueryString["idc_horario_perm"] == null ? "solicitud_horario.aspx?idc_puesto=" + Request.QueryString["idc_puesto"] : "solicitud_horario.aspx?autoriza=JKCWKJBOJBObjBHvucv67c7C7c7TC7tc7TC7c7TCcxCJHjhVjhJCjhhcjCJcJCjhcJcjCJHCJHCJCJCjcJHCjhcHC&idc_puesto=" + Request.QueryString["idc_puesto"] + "&idc_horario_perm=" + Request.QueryString["idc_horario_perm"];
                            Alert.ShowGiftMessage("Estamos Guardando los cambios.", "Espere un Momento", url, "imagenes/loading.gif", "2000", "La Solicitud fue Editada correctamente ", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("puestos_catalogo.aspx");
        }

        protected void btnautoriza_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Autorizar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Autorizar esta Solicitud?','modal fade modal-info');", true);
        }

        protected void btnrechaza_Click(object sender, EventArgs e)
        {
            if (txtobservaciones.Text == "")
            {
                Alert.ShowAlertError("Debe Ingresar Observaciones para Rechazar", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Rechazar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Rechazar esta Solicitud?','modal fade modal-info');", true);
            }
        }

        protected void btnrechaza2_Click(object sender, EventArgs e)
        {
            if (txtobservaciones.Text == "")
            {
                Alert.ShowAlertError("Debe Ingresar Observaciones para Cancelar", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Cancelar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar esta Solicitud?','modal fade modal-info');", true);
            }
        }
        protected void btnguardaredicio_Click(object sender, EventArgs e)
        {
            if (txthorasalidac.Text == "" && txthoraentradac.Text != "")
            {
                Alert.ShowAlertError("Ingrese la hora de salida a comer", this);
            }
            else if (txthorasalidac.Text != "" && txthoraentradac.Text == "")
            {
                Alert.ShowAlertError("Ingrese la hora de entrada a comer", this);
            }
            else if (txtobservaciones.Text == "" && Request.QueryString["autoriza"] == null)
            {
                Alert.ShowAlertError("Ingrese Observaciones", this);
            }
            else if (txtfecha.Text == "")
            {
                Alert.ShowAlertError("Ingrese la fecha de aplicación.", this);
            }
            else
            {
                if (btntot.CssClass == "btn btn-success btn-block")
                {
                    txthoraentrada.Text = "";
                    txthoraentradac.Text = "";
                    txthorasalida.Text = "";
                    txthorasalidac.Text = "";
                    txthorasalida.Enabled = true;
                    txthoraentradac.Enabled = true;
                    txthorasalidac.Enabled = true;
                    ddlsucursales.SelectedValue = "0";
                }
                Session["Caso_Confirmacion"] = "Editar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Editar esta Solicitud de Cambio de Fecha?','modal fade modal-info');", true);
            }
        }

        protected void btncancelaredicion_Click(object sender, EventArgs e)
        {
            string url = Request.QueryString["idc_horario_perm"] == null ? "solicitud_horario.aspx?idc_puesto=" + Request.QueryString["idc_puesto"] : "solicitud_horario.aspx?autoriza=JKCWKJBOJBObjBHvucv67c7C7c7TC7tc7TC7c7TCcxCJHjhVjhJCjhhcjCJcJCjhcJcjCJHCJHCJCJCjcJHCjhcHC&idc_puesto=" + Request.QueryString["idc_puesto"] + "&idc_horario_perm=" + Request.QueryString["idc_horario_perm"];
            Response.Redirect(url);
        }

        protected void lnkedit_Click(object sender, EventArgs e)
        {
            txtfecha.Enabled = true;
            ddlsucursales.Enabled = txthoraentrada.Text == "" ? false : true;
            txthoraentrada.Enabled = true;
            txthoraentradac.Enabled = lnkno_horacomida.CssClass == "btn btn-default btn-block" ? true : false;
            txthorasalida.Enabled = lnkno_Salida.CssClass == "btn btn-default btn-block" ? true : false; ;
            txthorasalidac.Enabled = lnkno_horacomida.CssClass == "btn btn-default btn-block" ? true : false; ;
            edicion.Visible = true;
            autoriza.Visible = false;
            if (Session["idc_horario_perm"] != null)
            {
                solicita.Visible = false;
            }
            if (lnkno_Salida.CssClass == "btn btn-default btn-block" && lnkno_horacomida.CssClass == "btn btn-default btn-block" && txthoraentrada.Text == "" && txthoraentradac.Text == "" && txthorasalida.Text == "" && txthorasalidac.Text == "" && ddlsucursales.SelectedValue == "0")
            {
                btntot.CssClass = "btn btn-success btn-block";
                cuerpo.Visible = false;
            }
        }

        protected void btntot_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["autoriza"] != null && btnguardaredicio.Visible == true)
            {
                btntot.CssClass = btntot.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
                cuerpo.Visible = btntot.CssClass == "btn btn-default btn-block" ? true : false;
            }
            else if (Request.QueryString["autoriza"] == null)
            {
                btntot.CssClass = btntot.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
                cuerpo.Visible = btntot.CssClass == "btn btn-default btn-block" ? true : false;
                if (btntot.CssClass == "btn btn-default btn-block" && txthoraentrada.Enabled == true)
                {
                    txthoraentrada.Text = "";
                    txthoraentradac.Text = "";
                    txthorasalida.Text = "";
                    txthorasalidac.Text = "";
                    txthorasalida.Enabled = true;
                    txthoraentradac.Enabled = true;
                    txthorasalidac.Enabled = true;
                    ddlsucursales.SelectedValue = "0";
                    lnkno_horacomida.CssClass = "btn btn-default btn-block";
                    lnkno_Salida.CssClass = "btn btn-default btn-block";
                }
            }
        }

        protected void lnkno_horacomida_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            if (Request.QueryString["autoriza"] != null && btnguardaredicio.Visible == true)
            {
                lnk.CssClass = lnk.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
                if (lnk.CssClass == "btn btn-success btn-block")
                {
                    txthoraentradac.Text = "";
                    txthorasalidac.Text = "";
                    txthoraentradac.Enabled = false;
                    txthorasalidac.Enabled = false;
                }
                else
                {
                    txthoraentradac.Enabled = true;
                    txthorasalidac.Enabled = true;
                }
            }
            else if (Request.QueryString["autoriza"] == null)
            {
                lnk.CssClass = lnk.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
                if (lnk.CssClass == "btn btn-success btn-block")
                {
                    txthoraentradac.Text = "";
                    txthorasalidac.Text = "";
                    txthoraentradac.Enabled = false;
                    txthorasalidac.Enabled = false;
                }
                else
                {
                    txthoraentradac.Enabled = true;
                    txthorasalidac.Enabled = true;
                }
            }
        }

        protected void lnkno_Salida_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            if (Request.QueryString["autoriza"] != null && btnguardaredicio.Visible == true)
            {
                lnk.CssClass = lnk.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
                if (lnk.CssClass == "btn btn-success btn-block")
                {
                    txthorasalida.Text = "";
                    txthorasalida.Enabled = false;
                }
                else
                {
                    txthorasalida.Enabled = true;
                }
            }
            else if (Request.QueryString["autoriza"] == null)
            {
                lnk.CssClass = lnk.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
                if (lnk.CssClass == "btn btn-success btn-block")
                {
                    txthorasalida.Text = "";
                    txthorasalida.Enabled = false;
                }
                else
                {
                    txthorasalida.Enabled = true;
                }
            }
        }

        protected void txthorasalida_TextChanged(object sender, EventArgs e)
        {
            bool checacomida = Convert.ToBoolean(Session["checacomida"]);
            string value = txthorasalida.Text;
            divchecacomida.Visible = false;
            if (checacomida == true && value != "")
            {
                divchecacomida.Visible = true;
            }
            int he = txthoraentrada.Text == "" ? 0 : Convert.ToInt32(txthoraentrada.Text.Replace(":", ""));
            int hc1 = txthoraentradac.Text == "" ? 0 : Convert.ToInt32(txthoraentradac.Text.Replace(":", ""));
            int hc2 = txthorasalidac.Text == "" ? 0 : Convert.ToInt32(txthorasalidac.Text.Replace(":", ""));
            int hs = txthorasalida.Text == "" ? 0 : Convert.ToInt32(txthorasalida.Text.Replace(":", ""));
            if (hs > 0)
            {
                if (he > hs || hc1 > hs || hc2 > hs)
                {
                    divchecacomida.Visible = false;
                    txthorasalida.Text = "";
                    Alert.ShowAlertInfo("El horario de Salida debe ser Mayor a los demas.", "Mensaje del sistema", this);
                }
            }
        }
    }
}