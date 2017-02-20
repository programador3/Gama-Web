using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class cursos_a_capacitar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["correo_a_cand"] = null;
                cargar_info();
            }
        }

        protected void cargar_info()
        {
            try
            {
                //componente
                Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
                //ds
                DataSet ds = new DataSet();
                ds = ComCursoHist.cursos_mandar_capacitar(Convert.ToInt32(Session["sidc_puesto_login"]));
                //llenar en session
                //Session.Add("TablaCursoCapacitar", ds.Tables[0]);
                //llenar grid view
                if (ds.Tables[0].Rows.Count == 0)
                {
                    panel_mensaje.Visible = true;
                }
                grid_cursos_pre_alta_pendientes.DataSource = ds.Tables[0];
                grid_cursos_pre_alta_pendientes.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        protected void grid_cursos_pre_alta_pendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //panel_comentarios.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid_cursos_pre_alta_pendientes.DataKeys[index].Value);

            switch (e.CommandName)
            {
                case "clic_programar":
                    //cargar los detalles de ese pre empleado como lo son los cursos que tiene, como contactarlo etc
                    cargar_info_detalle(vidc);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema');", true);
                    break;
            }
        }

        protected void modal_btnaceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //revisar si marco programar o rechazar capacitacion de empleado
                bool programar;
                if (rbtnlist_programar_rechazar.SelectedIndex > -1)
                {
                    programar = (Convert.ToInt32(rbtnlist_programar_rechazar.SelectedValue) == 1) ? true : false;

                    if (programar)
                    {
                        if (txtfecha_tentativa.Text == "")
                        {
                            Alert.ShowAlertError("Debe ingresar una fecha valida", this.Page);
                            return;
                        }
                        if (comentarios.Visible == true && txtComentarios.Text == "")
                        {
                            Alert.ShowAlertError("Si se rechaza de escribir observaciones", this.Page);
                            return;
                        }
                        DateTime thisDay = DateTime.Today;
                        DateTime fechaingresada = Convert.ToDateTime(txtfecha_tentativa.Text);
                        if (fechaingresada <= thisDay)
                        {
                            Alert.ShowAlertError("La fecha ingresada debe ser mayor al dia de hoy", this.Page);
                            return;
                        }
                        if (txtcorreo.Text == "")
                        {
                            Alert.ShowAlertError("INGRESE UN CORREO PARA ENVIAR EL LINK DE RECLUTAMIENTO", this.Page);
                            return;
                        }
                       
                        //entidad
                        Cursos_HistorialENT EntCursoHist = new Cursos_HistorialENT();
                        EntCursoHist.Idc_curso_historial = 0;
                        //EntCursoHist.Idc_curso = Convert.ToInt32(oc_modal_idc_curso.Value);
                        EntCursoHist.Idc_pre_empleado = Convert.ToInt32(oc_modal_idc_pre_empleado.Value);
                        EntCursoHist.Idc_puesto = Convert.ToInt32(oc_modal_idc_puesto.Value);
                        EntCursoHist.Estatus = Convert.ToChar("P");
                        SqlDateTime fecha = SqlDateTime.Parse(txtfecha_tentativa.Text);
                        EntCursoHist.Fecha_tentativa = fecha;
                        EntCursoHist.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        EntCursoHist.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        EntCursoHist.Pusuariopc = funciones.GetUserName();//usuario pc
                        EntCursoHist.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        //ds
                        DataSet ds = new DataSet();
                        //componente
                        //return;
                        Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
                        string correorh = "";
                        string correocandidato = "";
                        string guid = Guid.NewGuid().ToString();
                        if (Convert.ToBoolean(ViewState["correo_a_cand"])==false)
                        {
                            correorh = txtcorreo.Text.Trim();
                        }
                        else {
                            correocandidato = txtcorreo.Text.Trim();
                        }
                        ds = ComCursoHist.cursos_programar_capturar(EntCursoHist,correorh,correocandidato,txtObservaciones.Text,guid);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                        {
                            limpiar();
                            cargar_info();
                            int curso = Convert.ToInt32(ds.Tables[0].Rows[0]["cursos"].ToString());
                            string mess = curso > 0 ? "Programación de curso guardada correctamente" : "El empleado no tiene cursos disponibles. El proceso de ingreso continua.";
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('" + mess + "','menu.aspx');", true);
                        }
                        else
                        {
                            limpiar();
                            Alert.ShowAlertError(vmensaje, this.Page);
                            return;
                        }
                    }
                    else
                    { //rechazar
                        //mandar cancelar al pre empleado y la seleccion.
                        Cursos_HistorialCOM Componente = new Cursos_HistorialCOM();
                        Cursos_HistorialENT EntCursoHist = new Cursos_HistorialENT();
                        EntCursoHist.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        EntCursoHist.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        EntCursoHist.Pusuariopc = funciones.GetUserName();//usuario pc
                        EntCursoHist.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        EntCursoHist.Observaciones = txtComentarios.Text;
                        //ds
                        DataSet ds_cancelar = new DataSet();
                        ds_cancelar = Componente.cursos_cancelar_capacitacion(EntCursoHist, Convert.ToInt32(oc_modal_idc_pre_empleado.Value));
                        string vmensaje = ds_cancelar.Tables[0].Rows[0]["mensaje"].ToString();
                        if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                        {
                            Alert.ShowAlert("Pre empleado Rechazado correctamente", "Mensaje", this.Page);
                            limpiar();
                            cargar_info();
                        }
                        else
                        {
                            limpiar();
                            Alert.ShowAlertError(vmensaje, this.Page);
                            return;
                        }
                    }
                }
                else
                {
                    Alert.ShowAlertError("Debe seleccionar programar o rechazar candidato", this.Page);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        protected void limpiar()
        {
            //limpiar los campos ocultos y txt
            oc_modal_idc_puesto.Value = "";
            oc_modal_idc_pre_empleado.Value = "";
            oc_modal_idc_curso.Value = "";
            rbtnlist_programar_rechazar.ClearSelection();
            //
        }

        protected void modal_btncancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void cargar_info_detalle(int idc_pre_empleado)
        {
            txtComentarios.Text = "";
            txtObservaciones.Text = "BIENVENIDO, FUISTE SELECCIONADO PARA FORMAR PARTE DEL EQUIPO GAMA SOLO NECESITAMOS TU PAPELERIA COMPLETA Y ESPERO PUEDAS APOYARNOS LO ANTES POSIBLE CON EL LLENADO CORRECTO DE TUS DATOS.NOS VEMOS PRONTO.GRACIAS";
            txtfecha_tentativa.Text = "";
            //componente
            Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
            //ds
            DataSet ds = new DataSet();
            ds = ComCursoHist.cursos_mandar_capacitar_detalle(idc_pre_empleado);
            //crear tabla
            DataTable tabla_telefonos = ds.Tables[0];
            DataTable tabla_cursos = ds.Tables[1];
            //llenar los registros
            lista_cursos.DataSource = tabla_cursos;
            lista_cursos.DataTextField = "cursos";
            lista_cursos.DataBind();
            //telefonos
            lista_tels.DataSource = tabla_telefonos;
            lista_tels.DataTextField = "telefonos";
            lista_tels.DataBind();
            //correo
            DataTable tabla_correos = ds.Tables[2];
            modal_lblcorreo.Text = tabla_correos.Rows[0]["correo"].ToString();
            //pre empleado y puesto
            DataTable tabla_pre_empleados = ds.Tables[3];
            modal_lblpuesto.Text = tabla_pre_empleados.Rows[0]["puesto"].ToString();
            //empleado
            modal_pre_empleado.Text = tabla_pre_empleados.Rows[0]["pre_empleado"].ToString();
            //idc
            oc_modal_idc_pre_empleado.Value = idc_pre_empleado.ToString();
            //puesto
            oc_modal_idc_puesto.Value = tabla_pre_empleados.Rows[0]["idc_puesto"].ToString();

            bool aplica_enviar_correo = Convert.ToBoolean(tabla_pre_empleados.Rows[0]["SE_ENVIARA_CORREO"]);
            if (aplica_enviar_correo)
            {
                lbltextocorreo.Text = "Se enviara un correo al candidato, con un link al sistema de RECLUTAMIENTO, para que este complete su informacion y documentos. SE RECOMIENDA REALIZAR UNA LLAMADA DE AVISO AL CANDIDATO.";
                
                txtcorreo.Text = modal_lblcorreo.Text.Trim();
                txtcorreo.ReadOnly = modal_lblcorreo.Text != "";
                ViewState["correo_a_cand"] = true;
            }
            else
            {
                ViewState["correo_a_cand"] = false;
                string query = "select top 1 ltrim(rtrim(correo)) as correo,a.idc_correo from usuarios with(nolock) inner join correos_gama as a on a.idc_correo = usuarios.idc_correo where a.borrado = 0 and usuarios.idc_usuario = " + Convert.ToInt32(Session["sidc_usuario"]).ToString().Trim() + "";
                DataTable dt_correos_reclu = funciones.ExecQuery(query);
                lbltextocorreo.Text = "Se enviara un link a su correo externo, para que USTED complete la informacion y documentos del candidato.";
                txtcorreo.ReadOnly = Convert.ToInt32(dt_correos_reclu.Rows[0]["idc_correo"]) > 0 ? true : false;
                txtcorreo.Text = Convert.ToInt32(dt_correos_reclu.Rows[0]["idc_correo"]) > 0 ? dt_correos_reclu.Rows[0]["correo"].ToString().Trim() : "";
            }

        }

        protected void grid_cursos_pre_alta_pendientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                ImageButton programar = (ImageButton)e.Row.FindControl("btnprogramar");
                if (Convert.ToBoolean(rowView["seleccionado"]))
                {
                    programar.Visible = true;
                }
                else
                {
                    programar.Visible = false;
                }
            }
        }

        protected void rbtnlist_programar_rechazar_SelectedIndexChanged(object sender, EventArgs e)
        {
            fecha.Visible = Convert.ToInt32(rbtnlist_programar_rechazar.SelectedValue) == 0 ? false : true;
            comentarios.Visible = Convert.ToInt32(rbtnlist_programar_rechazar.SelectedValue) == 0 ? true : false;
            div_correo.Visible = fecha.Visible;
        }
    }
}