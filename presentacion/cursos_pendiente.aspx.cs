using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class cursos_pendiente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //variables de session
                //Session.Add("sidc_puestoperfil_borr", 0);
                //Session.Add("scomando", "");
                Session.Add("CursoBorr", "");
                cargar_curso_pendiente();
            }
        }

        private void cargar_curso_pendiente()
        {
            //entidad
            CursosE EntCurso = new CursosE();
            //componente
            try
            {
                CursosCOM ComCurso = new CursosCOM();
                DataSet ds = new DataSet();
                ds = ComCurso.cursos_pendientes_por_aprobar(EntCurso);
                grid_cursopendiente.DataSource = ds.Tables[0];
                grid_cursopendiente.DataBind();
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                Session["CursoBorr"] = dt;
            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message, this.Page);
            }
        }

        protected void grid_cursopendiente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //panel_comentarios.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid_cursopendiente.DataKeys[index].Value);
            Session["sidc_curso_borr"] = vidc;
            Session["scomando"] = e.CommandName;
            //necesito mandar el id de la solicitud de aprobacion si no tiene se manda un cero
            panel_comentarios.Visible = false;
            switch (e.CommandName)
            {
                case "comparar":
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Desea comparar este curso con el actualmente publicado?');", true);
                    break;

                case "aprobar":
                    //levantamos la solicitud
                    string res = solicitudAprobacion(vidc);

                    if (string.IsNullOrEmpty(res.Trim().Replace(" ", "")))
                    {
                        //todo bien

                        Response.Redirect("aprobaciones_firma.aspx");
                    }
                    else
                    {
                        Alert.ShowAlertError(res, this.Page);
                        return;
                    }

                    // ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Está seguro que desea autorizar este borrador? Ahora esta información podrá verse en el organigrama');", true);
                    break;

                case "regresar":
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','Esta opción permite volver hacer cambios sobre el borrador. ¿Desea continuar?');", true);
                    break;

                case "cancelar":
                    panel_comentarios.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Desea cancelar este borrador?.');", true);
                    break;
            }
        }

        protected string solicitudAprobacion(int id_row)
        {
            try
            {
                //recuperar el valor del datatable
                DataTable dt = (DataTable)Session["CursoBorr"];
                DataRow[] fila;
                fila = dt.Select("idc_curso_borr = " + id_row);
                int idc_aprobacion_soli = Convert.ToInt32(fila[0]["idc_aprobacion_soli"]);
                //ALERTA REVISAR ESTA SESISON
                Session.Add("sidc_aprobacion_soli", idc_aprobacion_soli);
                //SI NO TIENE SOLICITUD SE CREA
                if (idc_aprobacion_soli == 0)
                { //proseguimos a insertar la solicitud
                    //nevesitamos mandar los sig datos
                    int idc_aprobacion = 2; //antes 10 que aprobacion queremos solicitar 10 PERTENECE A CURSOS
                    int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString()); //que usuario hace la solicitud
                    int idc_registro = id_row; // el id del registro que se quiere aprobar EN ESTE CASO EL ID DEL BORRADOR
                    //llamamos la entidad
                    Aprobaciones_solicitudE entidad = new Aprobaciones_solicitudE();
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_aprobacion = idc_aprobacion;
                    entidad.Idc_usuario = idc_usuario;
                    entidad.Idc_registro = idc_registro;
                    entidad.Idc_aprobacion_soli = idc_aprobacion_soli; // SE MANDA CERO NO PASA NADA
                    //llamamos al componente
                    DataSet ds = new DataSet();
                    Aprobaciones_solicitudBL componente = new Aprobaciones_solicitudBL();
                    ds = componente.nueva_solicitud_aprobacion(entidad);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    int vfolio = Convert.ToInt32(ds.Tables[0].Rows[0]["folio"].ToString()); //id de la solicitud de aprobacion
                    //ALERTA REVISAR ESTA SESISON
                    Session.Add("sidc_aprobacion_soli", vfolio);
                    string vmensaje_no2 = "";
                    if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                    {
                        //ahora sigue los adicionales
                    }

                    return vmensaje + " " + vmensaje_no2;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                //Alert.ShowAlertError(ex.Message,this.Page);
                return ex.Message;
            }
        }

        protected string solicitudAprobacionAdicionalCursos(int idc_aprobacion_soli, int id_row)
        {
            //nevesitamos mandar los sig datos

            try
            {
                //llamamos la entidad
                Aprobaciones_solicitudE entidad = new Aprobaciones_solicitudE();
                entidad.Idc_aprobacion_soli = idc_aprobacion_soli;
                entidad.Idc_registro = id_row;
                //llamamos al componente
                DataSet ds = new DataSet();
                Aprobaciones_solicitudBL componente = new Aprobaciones_solicitudBL();
                ds = componente.solicitud_aprobacion_adicional_cursos(entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();

                //todo bien
                return vmensaje;
            }
            catch (Exception ex)
            {
                //Alert.ShowAlertError(ex.Message,this.Page);
                return ex.Message;
            }
        }

        protected void grid_cursopendiente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //revisar cada registro si tiene una solicitud o no
            //si tiene una el boton dira (Aprobacion) (pendiente)
            //cuano no el boton dira (Aprobacion)(aprobar)
            //para eso el grid view debe traer la columna idc_aprobacion_soli
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView fila = (DataRowView)e.Row.DataItem;
                int idc_aprobacion_soli = Convert.ToInt32(fila["idc_aprobacion_soli"]);
                Button btnsolicitar = (Button)e.Row.FindControl("btnaprobar");
                ImageButton btnregresar = (ImageButton)e.Row.FindControl("imgbtn_regresar");
                ImageButton btncancelar = (ImageButton)e.Row.FindControl("imgbtncancelar");
                if (idc_aprobacion_soli > 0)
                { //tiene solicitud
                    btnsolicitar.Text = "En proceso";
                    btnregresar.Visible = false;
                    btnsolicitar.CssClass = "btn btn-warning";
                    btncancelar.Visible = true;
                }
                else
                {
                    //no tiene solicitud
                    btnsolicitar.Text = "Aprobar";
                    btnregresar.Visible = true;
                    btnsolicitar.CssClass = "btn btn-default";
                    btncancelar.Visible = false;
                }
            }
        }

        /// <summary>
        /// Evento agregado de forma manual, se encadena cuando el usuario confirma modal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //Your Void() or Event()
            int id = Convert.ToInt32(Session["sidc_curso_borr"]);
            string comando = Session["scomando"].ToString();
            switch (comando)
            {
                case "cancelar":
                    //cancelar la solicitud de aprobacion
                    //recuperar el valor del datatable
                    //cancelar la solicitud de aprobacion
                    //recuperar el valor del datatable
                    DataTable dt = (DataTable)Session["CursoBorr"];
                    DataRow[] fila;
                    fila = dt.Select("idc_curso_borr = " + id);
                    int idc_aprobacion_soli = Convert.ToInt32(fila[0]["idc_aprobacion_soli"]);
                    //la entidad
                    Aprobaciones_solicitudE AprobSolicitudE = new Aprobaciones_solicitudE();
                    AprobSolicitudE.Idc_aprobacion_soli = idc_aprobacion_soli;
                    AprobSolicitudE.Comentarios = txtcoments.Text;
                    AprobSolicitudE.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                    AprobSolicitudE.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    AprobSolicitudE.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    AprobSolicitudE.Pusuariopc = funciones.GetUserName();//usuario pc
                    //el componente
                    DataSet ds = new DataSet();
                    Aprobaciones_solicitudBL AprobSolicitudBL = new Aprobaciones_solicitudBL();
                    ds = AprobSolicitudBL.solicitud_aprobacion_cancelar(AprobSolicitudE);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    //resultado
                    if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                    {
                        //todo bien
                        Alert.ShowAlert("Listo solicitud de aprobacion cancelada", "Mensaje", this.Page);
                    }
                    else
                    {
                        //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                        Alert.ShowAlertError(vmensaje, this.Page);
                        return;
                    }

                    cargar_curso_pendiente();
                    break;

                case "regresar":
                    regresaBorrador(id);
                    cargar_curso_pendiente();
                    break;

                case "comparar":
                    //recuperar el valor del datatable
                    DataTable dt_comparar = (DataTable)Session["CursoBorr"];
                    DataRow[] fila_comparar;
                    fila_comparar = dt_comparar.Select("idc_curso_borr = " + id);
                    Response.Redirect("cursos_detalle.aspx?uidc_curso=" + fila_comparar[0]["idc_curso"] + "&uidc_curso_borr=" + fila_comparar[0]["idc_curso_borr"]);
                    break;
            }

            //reset a las sessiones
            Session["sidc_curso_borr"] = 0;
            Session["scomando"] = "";
        }

        /// <summary>
        /// cambia el estatus de pendiente a no pendiente el borrador de cursos segun el id mandado
        /// </summary>
        /// <param name="id"></param>
        public void regresaBorrador(int id)
        {
            try
            {
                CursosE EntCurso = new CursosE();

                //componente
                CursosCOM ComCurso = new CursosCOM();
                //ds
                DataSet ds = new DataSet();
                EntCurso.Idc_curso = Convert.ToInt32(Session["idc_curso_borr"]);
                EntCurso.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                EntCurso.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuarioFC
                EntCurso.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                EntCurso.Pusuariopc = funciones.GetUserName();//usuario pc
                                                              //componente
                ds = ComCurso.CancelarSolicitud(EntCurso);
                string vmensaje3 = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje3)) // si esta vacio todo bien
                {
                    cargar_curso_pendiente();
                    Alert.ShowAlert("El Curso fue Desbloqueado Correctamente", "Mensaje del Sistema", this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje3, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }
    }
}