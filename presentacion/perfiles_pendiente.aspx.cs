using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class perfiles_pendiente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_opcion = 1807;  //pertenece al modulo de grupos backend
                //if (funciones.permiso(idc_usuario, idc_opcion) == false)
                //{
                //    Response.Redirect("menu.aspx");
                //    return;
                //}
                //variables de session
                Session.Add("sidc_puestoperfil_borr", 0);
                Session.Add("scomando", "");
                Session.Add("PuestoPerfilBorr", "");
                cargar_perfil_pendiente();
            }
        }

        private void cargar_perfil_pendiente()
        {
            //entidad
            PerfilesE entidad = new PerfilesE();
            //componente
            try
            {
                PerfilesBL componente = new PerfilesBL();
                DataSet ds = new DataSet();
                ds = componente.perfiles_pendientes(entidad);
                grid_perfilpendiente.DataSource = ds.Tables[0];
                grid_perfilpendiente.DataBind();
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                Session["PuestoPerfilBorr"] = dt;
            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message, this.Page);
            }
        }

        private String Cadena()
        {
            string cadena = "";
            int tcadena = 0;
            if (lnktodo.CssClass != "btn btn-info btn-block")
            {
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    LinkButton lnk = (LinkButton)item.FindControl("lnkgrupo");
                    if (lnk.CssClass == "btn btn-success btn-block")
                    {
                        cadena = cadena + lnk.CommandName + ";";
                        tcadena = tcadena + 1;
                    }
                }
            }
            if (Repeater1.Items.Count == tcadena)
            {
                cadena = "";
            }
            return cadena;
        }

        private int TotalCadena()
        {
            int cadena = 0;
            if (lnktodo.CssClass != "btn btn-info btn-block")
            {
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    LinkButton lnk = (LinkButton)item.FindControl("lnkgrupo");
                    if (lnk.CssClass == "btn btn-success btn-block")
                    {
                        cadena = cadena + 1;
                    }
                }
            }
            if (Repeater1.Items.Count == cadena)
            {
                cadena = 0;
            }
            return cadena;
        }

        /// <summary>
        /// Evento agregado de forma manual, se encadena cuando el usuario confirma modal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //Your Void() or Event()
            int id = Convert.ToInt32(Session["sidc_puestoperfil_borr"]);
            string comando = Session["scomando"].ToString();
            switch (comando)
            {
                case "comparar":
                    Response.Redirect("perfiles_detalle.aspx?uidc_puestoperfil=" + idPerfilProd(id) + "&uidc_puestoperfil_borr=" + id);
                    break;

                case "aprobar":
                    //levantamos la solicitud
                    string res = solicitudAprobacion(Convert.ToInt32(Session["sidc_puestoperfil_borr"]));
                    if (string.IsNullOrEmpty(res.Trim().Replace(" ", "")))
                    {
                        if (TotalCadena() > 0)
                        {
                            Response.Redirect("aprobaciones_firma.aspx?cadena=" + funciones.deTextoa64(Cadena()) + "&total=" + TotalCadena().ToString());
                        }
                        else
                        {
                            Response.Redirect("aprobaciones_firma.aspx?cadena=&total=" + TotalCadena().ToString());
                        }
                    }
                    else if (res != "")
                    {
                        Alert.ShowAlertError(res, this.Page);
                    }
                    break;

                case "regresar":
                    regresaBorrador(id);
                    cargar_perfil_pendiente();
                    break;

                case "cancelar":
                    //cancelar la solicitud de aprobacion
                    //recuperar el valor del datatable
                    DataTable dt = (DataTable)Session["PuestoPerfilBorr"];
                    DataRow[] fila;
                    fila = dt.Select("idc_puestoperfil_borr = " + id);
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
                    cargar_perfil_pendiente();
                    break;
            }
            //reset a las sessiones
            Session["sidc_puestoperfil_borr"] = 0;
            Session["scomando"] = "";
        }

        protected void grid_perfilpendiente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            panel_comentarios.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid_perfilpendiente.DataKeys[index].Value);
            Session["sidc_puestoperfil_borr"] = vidc;
            Session["scomando"] = e.CommandName;
            //necesito mandar el id de la solicitud de aprobacion si no tiene se manda un cero

            autori.Visible = false;
            switch (e.CommandName)
            {
                case "comparar":
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Desea comparar este perfil con el actualmente publicado?');", true);
                    break;

                case "aprobar":

                    try
                    {
                        //llamamos la entidad
                        Aprobaciones_solicitudE entidad = new Aprobaciones_solicitudE();
                        entidad.Idc_registro = vidc;
                        //llamamos al componente
                        DataSet ds = new DataSet();
                        Aprobaciones_solicitudBL componente = new Aprobaciones_solicitudBL();
                        ds = componente.CargarGrupos(entidad);
                        Repeater1.DataSource = ds.Tables[0];
                        Repeater1.DataBind();
                        autori.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.Message, this.Page);
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Está seguro que desea autorizar este borrador? Seleccione la informacion que desea que sea publicada');", true);
                    break;

                case "regresar":
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','Esta opción permite volver hacer cambios sobre el borrador. ¿Desea continuar?');", true);
                    break;

                case "cancelar":
                    panel_comentarios.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Desea cancelar esta aprobacion?.');", true);
                    break;
            }
        }

        public void regresaBorrador(int id)
        {
            try
            {
                PerfilesE entidad = new PerfilesE();
                entidad.Idc_puestoperfil_borr = id;
                entidad.Usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = new DataSet();
                PerfilesBL componente = new PerfilesBL();
                ds = componente.perfiles_pendientes_acciones(entidad, "regresar");
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    //todo bien
                    Alert.ShowAlert("Listo borrador desbloqueado", "Mensaje", this.Page);
                }
                else
                {
                    //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                    Alert.ShowAlertError(vmensaje, this.Page);
                    return;
                }
            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message, this.Page);
            }
        }

        /// <summary>
        /// Retorna el valor de id del registro de produccion segun el id de borrador que reciba
        /// </summary>
        /// <returns></returns>
        private int idPerfilProd(int idc_puestoperfil_borr)
        {
            //bajar la tabla de session
            DataTable dt = (DataTable)Session["PuestoPerfilBorr"];
            DataRow[] fila;
            fila = dt.Select("idc_puestoperfil_borr = " + idc_puestoperfil_borr);

            int idc_puestoperfil = Convert.ToInt32(fila[0]["idc_puestoperfil"]);

            return idc_puestoperfil;
        }

        protected void autorizarBorrador(int idc_puestoperfil_borr)
        {
            try
            {
                PerfilesE entidad = new PerfilesE();
                entidad.Idc_puestoperfil_borr = idc_puestoperfil_borr;
                entidad.Usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = new DataSet();
                PerfilesBL componente = new PerfilesBL();
                ds = componente.perfiles_pendientes_acciones(entidad, "autorizar");
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    //todo bien
                    Alert.ShowAlert("Listo Perfil autorizado", "Mensaje", this.Page);
                }
                else
                {
                    //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                    Alert.ShowAlertError(vmensaje, this.Page);
                    return;
                }
            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message, this.Page);
            }
        }

        protected string solicitudAprobacion(int id_row)
        {
            try
            {
                //recuperar el valor del datatable
                DataTable dt = (DataTable)Session["PuestoPerfilBorr"];
                DataRow[] fila;
                fila = dt.Select("idc_puestoperfil_borr = " + id_row);
                int idc_aprobacion_soli = Convert.ToInt32(fila[0]["idc_aprobacion_soli"]);
                //ALERTA REVISAR ESTA SESISON
                Session.Add("sidc_aprobacion_soli", idc_aprobacion_soli);
                //SI NO TIENE SOLICITUD SE CREA
                if (idc_aprobacion_soli == 0)
                { //proseguimos a insertar la solicitud
                  //nevesitamos mandar los sig datos
                    int idc_aprobacion = 1; //TIPO DE SOLICITUD APROBACION, CHECAR TABLA APROBACIONES
                    int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString()); //que usuario hace la solicitud
                    int idc_registro = id_row; // el id del registro que se quiere aprobar EN ESTE CASO EL ID DEL BORRADOR

                    //llamamos la entidad
                    Aprobaciones_solicitudE entidad = new Aprobaciones_solicitudE();
                    entidad.Idc_aprobacion = idc_aprobacion;
                    entidad.Idc_usuario = idc_usuario;
                    entidad.Idc_registro = idc_registro;
                    entidad.Idc_aprobacion_soli = idc_aprobacion_soli; // SE MANDA CERO NO PASA NADA
                                                                       //llamamos al componente
                    DataSet ds = new DataSet();
                    Aprobaciones_solicitudBL componente = new Aprobaciones_solicitudBL();
                    ds = componente.nueva_solicitud_aprobacion(entidad, idc_registro);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    int vfolio = Convert.ToInt32(ds.Tables[0].Rows[0]["folio"].ToString()); //id de la solicitud de aprobacion
                                                                                            //ALERTA REVISAR ESTA SESISON
                    Session.Add("sidc_aprobacion_soli", vfolio);

                    return vmensaje;// +" " + vmensaje_no2;
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

        protected string solicitudAprobacionAdicional(int idc_aprobacion_soli, int id_row)
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
                ds = componente.solicitud_aprobacion_adicional(entidad);
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

        protected void grid_perfilpendiente_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void lnkgrupo_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            lnk.CssClass = lnk.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
        }

        protected void lnktodo_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            lnk.CssClass = lnk.CssClass == "btn btn-default btn-block" ? "btn btn-info btn-block" : "btn btn-default btn-block";
            Repeater1.Visible = lnk.CssClass == "btn btn-default btn-block" ? true : false;
        }
    }
}