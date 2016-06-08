using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class tareas_automaticas_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack && Request.QueryString["idc_tarea_auto"] == null)
            {
                CargaPuestos("", ddlPuestoAsigna);
                CargaPuestos("", ddlpuestorealiza);
            }
            if (!IsPostBack && Request.QueryString["idc_tarea_auto"] != null)
            {
                CargaPuestos("", ddlPuestoAsigna);
                CargaPuestos("", ddlpuestorealiza);
                CargaPuestosedicion(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea_auto"])));
            }
        }

        /// <summary>
        /// Carga Puestos en Filtro
        /// </summary>
        public void CargaPuestos(string filtro, DropDownList ddl)
        {
            try
            {
                Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
                Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
                entidad.Filtro = filtro;
                entidad.Idc_puesto_revisa = Convert.ToInt32(Session["sidc_puesto_login"]);
                DataSet ds = componente.CargaComboDinamico(entidad);
                ddl.DataValueField = "idc_puesto";
                ddl.DataTextField = "descripcion_puesto_completa";
                ddl.DataSource = ds.Tables[0];
                ddl.DataBind();
                if (filtro == "")
                {
                    ddl.Items.Insert(0, new ListItem("Seleccione un Puesto", "0")); //updated code}
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Session["Error_Mensaje"] = ex.ToString();
            }
        }

        /// <summary>
        /// Carga Puestos en Filtro
        /// </summary>
        public void CargaPuestosedicion(int idc_tarea_auto)
        {
            try
            {
                TareasAutomaticasENT entidad = new TareasAutomaticasENT();
                entidad.Pidc_tarea_auto = idc_tarea_auto;
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                TareasAutomaticasCOM componente = new TareasAutomaticasCOM();
                DataSet ds = componente.CargaTareasEdicion(entidad);
                DataTable dt = ds.Tables[0];
                txtdescripcion.Text = dt.Rows[0]["descripcion"].ToString();
                ddltipo.SelectedValue = dt.Rows[0]["tipo"].ToString().TrimEnd();
                ddlpuestorealiza.SelectedValue = dt.Rows[0]["idc_puesto_realiza"].ToString();
                ddlPuestoAsigna.SelectedValue = dt.Rows[0]["idc_puesto_asigna"].ToString();
                txtfecha_inicio.Text = Convert.ToDateTime(dt.Rows[0]["fecha_empieza"]).ToString("yyyy-MM-dd");
                txtfecha_fin.Text = dt.Rows[0]["fecha_termina"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["fecha_termina"]).ToString("yyyy-MM-dd");
                txthoras_terminar.Text = dt.Rows[0]["horas_terminar"].ToString();
                //si no tiene hora especifica es una rango d efechas
                if (dt.Rows[0]["hora_especifica"].ToString() == "")
                {
                    ddlhorario.SelectedValue = "R";
                    ddl_rango_fin.SelectedValue = dt.Rows[0]["numero_horas_termina"].ToString();
                    ddl_rango_inicio.SelectedValue = dt.Rows[0]["numero_horas_comienza"].ToString();
                    txtnumhora.Text = dt.Rows[0]["numero_horas"].ToString();
                    panel_rango.Visible = true;
                }
                else
                {
                    ddlhorario.SelectedValue = "E";
                    ddl_hora_esp.SelectedValue = dt.Rows[0]["hora_especifica"].ToString();
                    panel_hora_Esp.Visible = true;
                }
                switch (ddltipo.SelectedValue)
                {
                    case "D":
                        txtfrecuencia_d.Text = dt.Rows[0]["frecuencia"].ToString();
                        break;

                    case "S":
                        txtfrecuencia_s.Text = dt.Rows[0]["frecuencia"].ToString();
                        break;

                    case "M":
                        txtfrecuencia_m.Text = dt.Rows[0]["frecuencia"].ToString();
                        txtdiames.Text = dt.Rows[0]["dia_mes"].ToString();
                        break;
                }
                lnklunes.CssClass = Convert.ToBoolean(dt.Rows[0]["lunes"]) == true ? "btn btn-success btn-block" : "btn btn-default btn-block";
                lnkmartes.CssClass = Convert.ToBoolean(dt.Rows[0]["martes"]) == true ? "btn btn-success btn-block" : "btn btn-default btn-block";
                lnkmiercoles.CssClass = Convert.ToBoolean(dt.Rows[0]["miercoles"]) == true ? "btn btn-success btn-block" : "btn btn-default btn-block";
                lnkjueves.CssClass = Convert.ToBoolean(dt.Rows[0]["jueves"]) == true ? "btn btn-success btn-block" : "btn btn-default btn-block";
                lnkviernes.CssClass = Convert.ToBoolean(dt.Rows[0]["viernes"]) == true ? "btn btn-success btn-block" : "btn btn-default btn-block";
                lnksabado.CssClass = Convert.ToBoolean(dt.Rows[0]["sabado"]) == true ? "btn btn-success btn-block" : "btn btn-default btn-block";
                lnkdomingo.CssClass = Convert.ToBoolean(dt.Rows[0]["domingo"]) == true ? "btn btn-success btn-block" : "btn btn-default btn-block";
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void ddltipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_d.Visible = false;
            panel_s.Visible = false;
            panel_m.Visible = false;
            string value = ddltipo.SelectedValue;
            if (value == "0")
            {
                Alert.ShowAlertError("Seleccione una opcion valida", this);
            }
            else if (value == "D")
            {
                panel_d.Visible = true;
            }
            else if (value == "S")
            {
                panel_s.Visible = true;
            }
            else if (value == "M")
            {
                panel_m.Visible = true;
            }
        }

        protected void txtfrecuencia_d_TextChanged(object sender, EventArgs e)
        {
            int value = txtfrecuencia_d.Text == "" ? 0 : Convert.ToInt32(txtfrecuencia_d.Text);
            if (value < 1 || value > 8)
            {
                txtfrecuencia_d.Text = "1";
                Alert.ShowAlertError("El valor de frecuencia  debe ser de 1 a 8 dias", this);
            }
        }

        protected void txtfrecuencia_s_TextChanged(object sender, EventArgs e)
        {
            int value = txtfrecuencia_s.Text == "" ? 0 : Convert.ToInt32(txtfrecuencia_s.Text);
            if (value < 1 || value > 52)
            {
                txtfrecuencia_s.Text = "1";
                Alert.ShowAlertError("El valor de frecuencia  debe ser de 1 a 52 semanas", this);
            }
        }

        protected void txtfrecuencia_m_TextChanged(object sender, EventArgs e)
        {
            int value = txtfrecuencia_m.Text == "" ? 0 : Convert.ToInt32(txtfrecuencia_m.Text);
            if (value < 1 || value > 24)
            {
                txtfrecuencia_m.Text = "1";
                Alert.ShowAlertError("El valor de frecuencia  debe ser de 1 a 24 meses", this);
            }
        }

        protected void lnklunes_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            lnk.CssClass = lnk.CssClass == "btn btn-success btn-block" ? "btn btn-default btn-block" : "btn btn-success btn-block";
        }

        protected void ddlhorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_hora_Esp.Visible = false;
            panel_rango.Visible = false;
            string value = ddlhorario.SelectedValue;
            if (value == "0")
            {
                Alert.ShowAlertError("Seleccione una opcion valida", this);
            }
            else if (value == "E")
            {
                panel_hora_Esp.Visible = true;
            }
            else if (value == "R")
            {
                panel_rango.Visible = true;
            }
        }

        protected void ddl_hora_esp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            string value = ddl.SelectedValue;
            if (value == "S")
            {
                Alert.ShowAlertError("Seleccione una opcion valida", this);
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
            CargaPuestos(txtpuesto_filtro.Text, ddlPuestoAsigna);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            CargaPuestos(txtfiltro.Text, ddlpuestorealiza);
        }

        protected void ddlpuestorealiza_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc_puesto = Convert.ToInt32(ddlpuestorealiza.SelectedValue);
            if (idc_puesto == 0)
            {
                Alert.ShowAlertError("Seleccione un Puesto Valido, o intente buscando uno.", this);
            }
        }

        private String MessError(string tipo)
        {
            string error = "";
            switch (tipo)
            {
                case "0":
                    error = "Seleccione un tipo de frecuencia";
                    break;

                case "D":
                    int value = txtfrecuencia_d.Text == "" ? 0 : Convert.ToInt32(txtfrecuencia_d.Text);
                    if (value < 1 || value > 8)
                    {
                        error = "El valor de frecuencia  debe ser de 1 a 8 dias";
                        txtfrecuencia_d.Text = "1";
                    }
                    if (txtfrecuencia_d.Text == "") { error = "Escriba un rango de frecuencia"; }
                    break;

                case "S":
                    int value2 = txtfrecuencia_s.Text == "" ? 0 : Convert.ToInt32(txtfrecuencia_s.Text);
                    if (value2 < 1 || value2 > 52)
                    {
                        error = "El valor de frecuencia  debe ser de 1 a 52 semanas";
                        txtfrecuencia_s.Text = "1";
                    }
                    if (txtfrecuencia_s.Text == "") { error = "Seleccione una fecha de inicio para la tarea"; }
                    break;

                case "M":
                    int value3 = txtfrecuencia_m.Text == "" ? 0 : Convert.ToInt32(txtfrecuencia_m.Text);
                    if (value3 < 1 || value3 > 24)
                    {
                        error = "El valor de frecuencia  debe ser de 1 a 24 meses";
                        txtfrecuencia_m.Text = "1";
                    }
                    int value4 = txtdiames.Text == "" ? 0 : Convert.ToInt32(txtdiames.Text);
                    if (value4 < 1 || value4 > 30)
                    {
                        error = "El valor de dia del mes  debe ser de 1 a 30 dias";
                        txtfrecuencia_m.Text = "1";
                    }
                    if (txtdiames.Text == "") { error = "Seleccione un dia del mes en el que se repeitra la tarea"; }
                    if (txtfrecuencia_m.Text == "") { error = "Seleccione una fecha de inicio para la tarea"; }
                    break;
            }
            if (ddlPuestoAsigna.SelectedValue == "0") { error = "Seleccione el Puesto que Revisara y dara el Visto Bueno a la tarea."; }
            if (ddlpuestorealiza.SelectedValue == "0") { error = "Seleccione el Puesto que realizara la tarea"; }
            if (txtdescripcion.Text == "") { error = "Inserte una Descripción"; }
            if (txtfecha_inicio.Text == "") { error = "Seleccione una fecha de inicio para la tarea"; }

            if (txthoras_terminar.Text == "" || txthoras_terminar.Text == "0") { error = "Inserte un valor de duracion."; }
            if (ddlhorario.SelectedValue == "0") { error = "Seleccione un tipo de horario de repetición."; }
            else
            {
                if (ddlhorario.SelectedValue == "E")
                {
                    if (ddl_hora_esp.SelectedValue == "S")
                    {
                        error = "Seleccione una hora especifica en la que se ejecutara la tarea.";
                    }
                }
                if (ddlhorario.SelectedValue == "R")
                {
                    if (ddl_rango_inicio.SelectedValue == "S")
                    {
                        error = "Seleccione una hora DE INICIO en la que se ejecutara la tarea.";
                    }
                    if (ddl_rango_fin.SelectedValue == "S")
                    {
                        error = "Seleccione una hora hasta la que se podra ejecutar la tarea.";
                    }
                    if (txtnumhora.Text == "0" || txtnumhora.Text == "")
                    {
                        error = "Escriba el numero de horas de repetición.";
                    }
                }
            }
            return error;
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            if (MessError(ddltipo.SelectedValue) != "")
            {
                Alert.ShowAlertError(MessError(ddltipo.SelectedValue), this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar la tarea con las siguientes caracteristicas?','modal fade modal-info');", true);
                lblvar.Text = "La Tarea se ejecutara apartir de " + (Convert.ToDateTime(txtfecha_inicio.Text).ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX"))).ToString() + " y la realizara " +
                    ddlpuestorealiza.SelectedItem + " se ejecutara cada ";
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                TareasAutomaticasENT entidad = new TareasAutomaticasENT();
                TareasAutomaticasCOM componente = new TareasAutomaticasCOM();
                if (Request.QueryString["idc_tarea_auto"] != null)
                {
                    entidad.Pidc_tarea_auto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea_auto"]));
                }
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pidc_puesto_asigna = Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
                entidad.Pidc_puesto_realiza = Convert.ToInt32(ddlpuestorealiza.SelectedValue);
                entidad.Pdescripcion = txtdescripcion.Text.ToUpper();
                entidad.Pfecha_empieza = Convert.ToDateTime(txtfecha_inicio.Text);
                //SI NO TIENE FECHA FIN NO PASAMOS EL PARAMETRO
                if (txtfecha_fin.Text != "") { entidad.Pfecha_termina = Convert.ToDateTime(txtfecha_fin.Text); }
                entidad.Phoras_terminar = Convert.ToInt32(txthoras_terminar.Text);
                entidad.Ptipo = ddltipo.SelectedValue;
                //SI EL HORARIO ES ESPECIFICO SOLO PASAMOS VALORES DE HORA ESPECIFICA
                if (ddlhorario.SelectedValue == "E")
                {
                    entidad.Phora_especifica = Convert.ToInt32(ddl_hora_esp.SelectedValue);
                }
                //SI EL HORARIO TIENE RANGO SOLO PASAMOS VALORES DE NUMERO DE HORAS
                if (ddlhorario.SelectedValue == "R")
                {
                    entidad.Pnumero_horas = Convert.ToInt32(txtnumhora.Text);
                    entidad.Pnumero_horas_comienza = Convert.ToInt32(ddl_rango_inicio.SelectedValue);
                    entidad.Pnumero_horas_termina = Convert.ToInt32(ddl_rango_fin.SelectedValue);
                }
                switch (ddltipo.SelectedValue)
                {
                    case "D":
                        entidad.Pfrecuencia = Convert.ToInt32(txtfrecuencia_d.Text);
                        break;

                    case "S":
                        entidad.Pfrecuencia = Convert.ToInt32(txtfrecuencia_s.Text);
                        break;

                    case "M":
                        entidad.Pfrecuencia = Convert.ToInt32(txtfrecuencia_m.Text);
                        entidad.Pdia_mes = Convert.ToInt32(txtdiames.Text);
                        break;
                }
                entidad.Plunes = lnklunes.CssClass == "btn btn-success btn-block" ? true : false;
                entidad.Pmartes = lnkmartes.CssClass == "btn btn-success btn-block" ? true : false;
                entidad.Pmiercole = lnkmiercoles.CssClass == "btn btn-success btn-block" ? true : false;
                entidad.Pjueves = lnkjueves.CssClass == "btn btn-success btn-block" ? true : false;
                entidad.Pviernes = lnkviernes.CssClass == "btn btn-success btn-block" ? true : false;
                entidad.Psabado = lnksabado.CssClass == "btn btn-success btn-block" ? true : false;
                entidad.Pdomingo = lnkdomingo.CssClass == "btn btn-success btn-block" ? true : false;
                DataSet ds = new DataSet();
                ds = componente.AgregarTarea(entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos Generando la Tarea automatica.", "Espere un Momento", "tareas_automaticas.aspx", "imagenes/loading.gif", "3000", "La tarea fue Guardada Correctamente y estara en funcionamiento a partir del dia " + (Convert.ToDateTime(txtfecha_inicio.Text).ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX"))).ToString(), this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }
    }
}