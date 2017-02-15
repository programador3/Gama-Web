using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class puestos_alta_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarInformacionInicial();
                if (Request.QueryString["idc_pre_puesto"] != null)
                {
                    CargarInformacionPrePuesto(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_pre_puesto"])));
                }
                //solo para autorizar
                if (Request.QueryString["aut"] != null)
                {
                    lnkguardar.Visible = false;
                    OcultarFormulario(false);
                    lnkautorizar.Visible = true;
                    lnkcancelarsolicitud.Visible = true;
                    lnkeditar.Visible = true;
                    lnkrechazarsolicitud.Visible = true;
                    lnkcancelar.Text = "Regresar";
                }

                //solo para ver
                if (Request.QueryString["view"] != null)
                {
                    OcultarFormulario(false);
                    lnkguardar.Visible = false;
                    lnkcancelar.Text = "Regresar";
                }
            }
        }

        /// <summary>
        /// Oculta los datos de Captura de un formulario
        /// </summary>
        /// <param name="Mostrar"></param>
        private void OcultarFormulario(bool Mostrar)
        {
            if (Mostrar)
            {
                lnkbuscarpuestos.Visible = true;
                txtClave.ReadOnly = false;
                txtnombre.ReadOnly = false;
                txtpuesto_filtro.Visible = true;
                ddldepto.Enabled = true;
                ddlperfil.Enabled = true;
                ddlPuesto.Enabled = true;
                ddlsucursal.Enabled = true;
                ddluniforme.Enabled = true;
                cbxClasificacion.Enabled = true;
                ddlhorarios.Enabled = true;
                ddlsueldo.Enabled = true;
                div_buscar_puesto.Visible = true;
                txtdias_reclu.ReadOnly = false;
            }
            else {
                lnkbuscarpuestos.Visible = false;
                txtClave.ReadOnly = true;
                txtnombre.ReadOnly = true;
                txtpuesto_filtro.Visible = false;
                ddldepto.Enabled = false;
                ddlperfil.Enabled = false;
                ddlPuesto.Enabled = false;
                ddlsucursal.Enabled = false;
                ddluniforme.Enabled = false;
                ddlhorarios.Enabled = false;
                ddlsueldo.Enabled = false;
                cbxClasificacion.Enabled = false;
                txtdias_reclu.ReadOnly = true;
                div_buscar_puesto.Visible = false;
            }
        }
        private void CargarInformacionInicial()
        {
            try
            {
                //PERFILES
                PuestosCOM ComPuesto = new PuestosCOM();
                DataSet ds = new DataSet();
                ds = ComPuesto.combo_puestos_perfil();
                ddlperfil.DataSource = ds.Tables[0];
                ddlperfil.DataTextField = "descripcion";
                ddlperfil.DataValueField = "idc_puestoperfil";
                ddlperfil.DataBind();
                ddlperfil.Items.Insert(0, new ListItem("--Seleccionar Perfil--", "0"));
                //jefe
                CargaPuestos("");
                //deptos
                ds = ComPuesto.SP_COMBO_DEPTOS();
                ddldepto.DataSource = ds.Tables[0];
                ddldepto.DataTextField = "nombre";
                ddldepto.DataValueField = "idc_depto";
                ddldepto.DataBind();
                ddldepto.Items.Insert(0, new ListItem("--Seleccionar Depto--", "0"));
                //sucursales
                ds = ComPuesto.sp_sucursales_combo();
                ddlsucursal.DataSource = ds.Tables[0];
                ddlsucursal.DataTextField = "nombre";
                ddlsucursal.DataValueField = "idc_sucursal";
                ddlsucursal.DataBind();
                ddlsucursal.Items.Insert(0, new ListItem("--Seleccionar Sucursal--", "0"));
                //uniformes
                ds = ComPuesto.sp_combo_tipos_uniformes();
                ddluniforme.DataSource = ds.Tables[0];
                ddluniforme.DataTextField = "descripcion";
                ddluniforme.DataValueField = "idc_uniforme";
                ddluniforme.DataBind();
                ddluniforme.Items.Insert(0, new ListItem("--Seleccionar Uniforme--", "0"));
                //uniformes
                ds = ComPuesto.sp_combo_puestos_clasificacion();
                cbxClasificacion.DataSource = ds.Tables[0];
                cbxClasificacion.DataTextField = "descripcion";
                cbxClasificacion.DataValueField = "idc_puestoclas";
                cbxClasificacion.DataBind();
                //horarios grupos
                ds = ComPuesto.sp_combo_horariosg();
                ddlhorarios.DataTextField = "descripcion";
                ddlhorarios.DataValueField = "idc_horariog";
                ddlhorarios.DataSource = ds.Tables[0];
                ddlhorarios.DataBind();
                ddlhorarios.Items.Insert(0, new ListItem("--Seleccione un Horario--", "0"));
                Session["HORARIO"] = ds.Tables[0];
                //TABULADORES DE SUELDO
                ds = ComPuesto.SP_COMBO_TABULADORES_SUELDOS();
                ddlsueldo.DataTextField = "sueldo";
                ddlsueldo.DataValueField = "idc_tabulador";
                ddlsueldo.DataSource = ds.Tables[0];
                ddlsueldo.DataBind();
                ddlsueldo.Items.Insert(0, new ListItem("--Seleccione un Sueldo--", "0"));
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        private void CargarInformacionPrePuesto(int idc)
        {
            try
            {
                //PERFILES
                PuestosCOM ComPuesto = new PuestosCOM();
                DataSet ds = new DataSet();
                ds = ComPuesto.sp_pre_puestos(idc,1);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    txtnombre.Text = row["descripcion"].ToString();
                    txtClave.Text = row["clave"].ToString();
                    ddldepto.SelectedValue =  row["idc_depto"].ToString();
                    ddlperfil.SelectedValue = row["idc_puestoperfil"].ToString();
                    ddlPuesto.SelectedValue =  row["idc_puesto_jefe"].ToString();
                    ddlsucursal.SelectedValue =  row["idc_sucursal"].ToString();
                    ddluniforme.SelectedValue =  row["idc_uniforme"].ToString();
                    ddlhorarios.SelectedValue = row["idc_horariog"].ToString();
                    ddlsueldo.SelectedValue = row["idc_tabulador"].ToString();
                    txtdias_reclu.Text = row["dias_reclutar"].ToString();
                    DataTable dt_clas = ds.Tables[1];
                    foreach (ListItem cbx in cbxClasificacion.Items)
                    {
                        DataRow[] existe = dt_clas.Select("idc_puestoclas = "+cbx.Value.Trim()+"");
                        cbx.Selected = existe.Length > 0;
                    }
                }
                else
                {
                    Alert.ShowAlertError("NO SE ENCONTRO NINGUN DATO.", this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        public void CargaPuestos(string filtro)
        {
            try
            {
                PuestosCOM ComPuesto = new PuestosCOM();
                DataSet ds = new DataSet();
                ds = ComPuesto.sp_combo_puestos(txtpuesto_filtro.Text);
                ddlPuesto.DataValueField = "idc_puesto";
                ddlPuesto.DataTextField = "descripcion_puesto_completa";
                ddlPuesto.DataSource = ds.Tables[0];
                ddlPuesto.DataBind();
                //si no hay filtro insertamos una etiqueta inicial
                if (filtro == "")
                {
                    ddlPuesto.Items.Insert(0, new ListItem("Seleccione un Puesto", "0")); //updated code}
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }
        private String CadenaClasificaciones()
        {
            try
            {
                string cadena = "";
                foreach (ListItem cbx in cbxClasificacion.Items)
                {
                    if (cbx.Selected)
                    {
                        cadena = cadena + cbx.Value.Trim() + ";";
                    }
                }
                return cadena;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                return "";
            }
        }
        private int TotalCadenaClasificaciones()
        {
            try
            {
                int cadena = 0;
                foreach (ListItem cbx in cbxClasificacion.Items)
                {
                    if (cbx.Selected)
                    {
                        cadena++;
                    }
                }
                return cadena;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                return 0;
            }
        }


        private bool PuestoCompleto()
        {
            try
            {
                if (ddldepto.SelectedValue == "0")
                {
                    return false;
                }
                else if (ddlperfil.SelectedValue == "0")
                {
                    return false;
                }
                else if (ddlPuesto.SelectedValue == "0")
                {
                    return false;
                }
                else if (ddluniforme.SelectedValue == "0")
                {
                    return false;
                }
                else if (ddlsucursal.SelectedValue == "0")
                {
                    return false;
                }
                else if (ddlsueldo.SelectedValue == "0")
                {
                    return false;
                }
                else if (ddlhorarios.SelectedValue == "0")
                {
                    return false;
                }
                else if (txtdias_reclu.Text=="")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                return false;
            }
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            CargaPuestos(txtpuesto_filtro.Text);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            div_observaciones.Visible = false;
            if (txtnombre.Text == "")
            {
                Alert.ShowAlertError("INGRESE EL NOMBRE DEL PUESTO", this);
            }
            else
            {
                Session["confirma"] = Request.QueryString["idc_pre_puesto"] == null? "GUARDAR":"EDITAR";
                string mensaje = PuestoCompleto() ? "LA SOLICITUD ESTA COMPLETA Y PUEDE SOLICITAR AUTORIZACIÓN" : "LA SOLICITUD NO ESTA COMPLETA, SE GUARDARA PERO NO PODRA SOLICITAR AUTORIZACIÓN";
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "ModalConfirm('Mensaje del Sistema','¿Desea Guardar esta Solicitud? \\n" + mensaje + "','modal fade modal-info');", true);
            }
        }


        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            div_observaciones.Visible = false;
            Session["confirma"] = "CANCELAR";
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar y Regresar?','modal fade modal-danger');", true);
        }

        protected void lnkguardaEdicion_Click(object sender, EventArgs e)
        {
            if (txtnombre.Text == "")
            {
                Alert.ShowAlertError("INGRESE EL NOMBRE DEL PUESTO", this);
            }
            else if (!PuestoCompleto())
            {
                Alert.ShowAlertError("LOS DATOS DEBEN DE ESTAR COMPLETOS PARA EDITAR.", this);
            }
            else
            {
                div_observaciones.Visible = false;
                Session["confirma"] = "EDITAR";
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "ModalConfirm('Mensaje del Sistema','¿Desea Editar esta Solicitud?','modal fade modal-info');", true);
            }
        }

        protected void lnkrechazarsolicitud_Click(object sender, EventArgs e)
        {
            div_observaciones.Visible = true;
            txtobservaciones.Text = "";
            Session["confirma"] = "RECHAZAR SOLICITUD";
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "ModalConfirm('Mensaje del Sistema','¿Desea Eliminar Esta Solcitud?','modal fade modal-danger');", true);
        }
        protected void lnkautorizar_Click(object sender, EventArgs e)
        {
            
            div_observaciones.Visible = true;
            txtobservaciones.Text = "";
            Session["confirma"] = "AUTORIZAR SOLICITUD";
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "ModalConfirm('Mensaje del Sistema','¿Desea Autorizar Esta Solcitud?','modal fade modal-success');", true);
        }

        protected void lnkrechazarsolicitud_Click1(object sender, EventArgs e)
        {

            div_observaciones.Visible = true;
            txtobservaciones.Text = "";
            Session["confirma"] = "REGRESAR SOLICITUD";
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                  "ModalConfirm('Mensaje del Sistema','¿Desea Regresar Esta Solcitud?. La solicitud Regresara al Usuario.','modal fade modal-info');", true);
        }
        protected void LinkButton2_Click1(object sender, EventArgs e)
        {
            //perfil
            int idc_perfil = Convert.ToInt32(ddlperfil.SelectedValue);
            if (idc_perfil > 0)
            {
                string url = "perfiles_detalle.aspx?perfiles=true&borrador=0&uidc_puestoperfil=" + idc_perfil.ToString();
                ScriptManager.RegisterStartupScript(this,GetType(),Guid.NewGuid().ToString(),"window.open('"+url+"');",true);
            }
            else
            {
                Alert.ShowAlertError("Seleccione un Perfil valido", this);
            }
        }

        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            //horariog
            try
            {
                div_details_horario.Visible = div_details_horario.Visible ? false : true;
                int id_horario = Convert.ToInt32(ddlhorarios.SelectedValue);
                if (id_horario > 0)
                {
                    CandidatosENT entidad = new CandidatosENT();
                    CandidatosCOM componente = new CandidatosCOM();
                    DataSet ds = componente.sp_datos_horarios_grupos_det(id_horario);
                    DataTable dt_Details = ds.Tables[1];
                    grid_Detalles.DataSource = dt_Details;
                    grid_Detalles.DataBind();
                }
                else
                {
                    div_details_horario.Visible = false;
                    Alert.ShowAlertError("Seleccione un horario valido", this);
                }
            }
            catch (Exception ex)
            {
                div_details_horario.Visible = false;
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }
        protected void ddlhorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                div_details_horario.Visible = false;
                int id_horario = Convert.ToInt32(ddlhorarios.SelectedValue);
                if (id_horario > 0)
                {
                    CandidatosENT entidad = new CandidatosENT();
                    CandidatosCOM componente = new CandidatosCOM();
                    DataSet ds = componente.sp_datos_horarios_grupos_det(id_horario);
                    DataTable dt_Details = ds.Tables[1];
                    grid_Detalles.DataSource = dt_Details;
                    grid_Detalles.DataBind();
                }
                else
                {
                    Alert.ShowAlertError("Seleccione un horario valido", this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }
        protected void lnkeditar_Click(object sender, EventArgs e)
        {
            OcultarFormulario(true);
            lnkautorizar.Visible = false;
            lnkrechazarsolicitud.Visible = false;
            lnkguardaEdicion.Visible = true;
            lnkeditar.Visible = false;
            lnkcancelaredicion.Visible = true;
        }

        protected void lnkcancelaredicion_Click(object sender, EventArgs e)
        {

            OcultarFormulario(false);
            lnkautorizar.Visible = true;
            lnkrechazarsolicitud.Visible = true;
            lnkguardaEdicion.Visible = false;
            lnkeditar.Visible = true;
            lnkcancelaredicion.Visible = false;
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmacion = Session["confirma"] as string;
                PuestosCOM componente = new PuestosCOM();
                string descripcion = txtnombre.Text.Trim().ToUpper();
                string clave = txtClave.Text.Trim().ToUpper();
                int idc_puesto_jefe = ddlPuesto.Items.Count > 0 ? Convert.ToInt32(ddlPuesto.SelectedValue) : 0;
                int idc_depto = Convert.ToInt32(ddldepto.SelectedValue);
                int idc_sucursal = Convert.ToInt32(ddlsucursal.SelectedValue);
                int idc_uniforme = Convert.ToInt32(ddluniforme.SelectedValue);
                int idc_horariog = Convert.ToInt32(ddlhorarios.SelectedValue);
                int idc_tabulador = Convert.ToInt32(ddlsueldo.SelectedValue);
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                string cadena = CadenaClasificaciones();
                int total_cadena = TotalCadenaClasificaciones();
                int idc_perfil = Convert.ToInt32(ddlperfil.SelectedValue);
                DataSet ds = new DataSet();
                string status = "";
                string vmensaje = "";
                string observaciones = txtobservaciones.Text.ToUpper();
                int dias_reclu = txtdias_reclu.Text == "" ? 0 : Convert.ToInt32(txtdias_reclu.Text);
                DataTable dt_error = new DataTable();
                int IDC_PRE_PUESTO = Request.QueryString["idc_pre_puesto"] == null?0: Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_pre_puesto"]));
                switch (confirmacion)
                {
                    case "GUARDAR":
                        ds = componente.sp_apre_puestos(descripcion, clave, idc_puesto_jefe, idc_depto, idc_sucursal, idc_uniforme, idc_usuario, cadena, total_cadena, idc_perfil, 
                            idc_horariog,idc_tabulador, dias_reclu);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "EDITAR":
                        ds = componente.sp_mpre_puestos(IDC_PRE_PUESTO, descripcion, clave, idc_puesto_jefe, idc_depto, idc_sucursal, idc_uniforme, idc_usuario, cadena, total_cadena, 
                            idc_perfil, idc_horariog, idc_tabulador, dias_reclu);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;
                    case "REGRESAR SOLICITUD":
                        status = "P";//STATUS PENDIENTE
                        ds = componente.sp_status_pre_puestos(IDC_PRE_PUESTO, status, idc_usuario);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;
                        
                    case "CANCELAR":
                        if (Request.QueryString["aut"] != null)
                        {
                            Response.Redirect("puestos_alta_autorizar.aspx");
                        }
                        else
                        {
                            Response.Redirect("puestos_alta.aspx");
                        }
                        break;
                    case "RECHAZAR SOLICITUD":
                        //lo mandamos en cadena con status C CANCELADO: NOTA: EL SP ESTA PREPARADO PARA REALIZAR MOVIMIENTOS MASIVOS
                        cadena = funciones.de64aTexto(Request.QueryString["idc_pre_puesto"]).Trim() + ";C;";
                        total_cadena = 1;
                        ds = componente.sp_pre_puestos_autorizar(cadena, total_cadena, idc_usuario, observaciones);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        dt_error = ds.Tables[1];
                        if (vmensaje == "" && dt_error.Rows.Count > 0)
                        {
                            vmensaje = dt_error.Rows[0]["error"].ToString();
                        }
                        break;
                    case "AUTORIZAR SOLICITUD":
                        //lo mandamos en cadena con status T TERMINADO: NOTA: EL SP ESTA PREPARADO PARA REALIZAR MOVIMIENTOS MASIVOS
                        cadena = funciones.de64aTexto(Request.QueryString["idc_pre_puesto"]).Trim() + ";T;" ;
                        total_cadena = 1;
                        ds = componente.sp_pre_puestos_autorizar(cadena,total_cadena, idc_usuario, observaciones);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        dt_error = ds.Tables[1];
                        if (vmensaje == "" && dt_error.Rows.Count > 0)
                        {
                            vmensaje = dt_error.Rows[0]["error"].ToString();
                        }
                        break;
                }
                if (vmensaje == "")
                {
                    string mensaje = PuestoCompleto() ? "LA SOLICITUD FUE GUARDADA Y PUEDE SOLICITAR AUTORIZACIÓN" : "LA SOLICITUD FUE GUARDADA PERO NO ESTA COMPLETA, NO PODRA SOLICITAR AUTORIZACIÓN";
                    string url = "puestos_alta.aspx";
                    //si es autorizar
                    if (Request.QueryString["aut"] != null)
                    {
                        url = "puestos_alta_autorizar.aspx";
                        switch (confirmacion)
                        {
                            case "AUTORIZAR SOLICITUD":
                                mensaje = "Solicitud Autorizada Correctamente";
                                break;
                            case "EDITAR":
                                mensaje = "Solicitud Editada Correctamente";
                                url = "puestos_alta_captura.aspx?aut=1&idc_pre_puesto=" + Request.QueryString["idc_pre_puesto"] + "";
                                break;
                            case "RECHAZAR SOLICITUD":
                                mensaje = "Solicitud Rechazada Correctamente";
                                break;
                            case "REGRESAR SOLICITUD":
                                mensaje = "Solicitud Regresada Correctamente";
                                break;
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('" + mensaje + "','" + url + "');", true);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this.Page);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

    

    }
}