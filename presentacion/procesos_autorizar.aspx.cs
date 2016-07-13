using ClosedXML.Excel;
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
    public partial class procesos_autorizar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session.Add("sidc_puestoperfil_borr", 0);
                Session.Add("scomando", "");
                Session.Add("PuestoPerfilBorr", "");
                cargar_manuales_pendientes();
            }
        }

        private void cargar_manuales_pendientes()
        {
            try
            {
                ProcesosENT enti = new ProcesosENT();
                ProcesosCOM com = new ProcesosCOM();
                DataSet ds = com.ManualesPendientesxAutorizar(enti);
                grid_perfilpendiente.DataSource = ds.Tables[0];
                grid_perfilpendiente.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void lnkgrupo_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            lnk.CssClass = lnk.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
        }

        /// <summary>
        /// Inserta una solicitud de aprobacion
        /// </summary>
        /// <param name="idc_registro"></param>
        /// <param name="idc_aprobacion_soli"></param>
        /// <returns></returns>
        protected string solicitudAprobacion(int idc_registro)
        {
            try
            {
                int idc_aprobacion_soli = 0;
                Session.Add("sidc_aprobacion_soli", idc_aprobacion_soli);
                //SI NO TIENE SOLICITUD SE CREA
                if (idc_aprobacion_soli == 0)
                { //proseguimos a insertar la solicitud
                  //nevesitamos mandar los sig datos
                    int idc_aprobacion = 4; //TIPO DE SOLICITUD APROBACION, CHECAR TABLA APROBACIONES
                    int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString()); //que usuario hace la solicitud

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

        protected void Yes_Click(object sender, EventArgs e)
        {
            //Your Void() or Event()
            int id = Convert.ToInt32(Session["sidc_puestoperfil_borr"]);
            string comando = Session["scomando"].ToString();
            string url = Session["url"] as string;
            switch (comando)
            {
                case "preview":

                    Response.Redirect(url);
                    break;
                case "comparar":
                    // Response.Redirect("perfiles_detalle.aspx?uidc_puestoperfil=" + idPerfilProd(id) + "&uidc_puestoperfil_borr=" + id);
                    break;

                case "aprobar":
                    //levantamos la solicitud
                    string res = solicitudAprobacion(Convert.ToInt32(Session["sidc_puestoperfil_borr"]));
                    //creamos cadenas
                    if (TotalCadena() == 0 && lnktodo.CssClass == "btn btn-default btn-block")
                    {
                        Alert.ShowAlertError("Seleccione una opcion", this.Page);
                    }
                    else if (string.IsNullOrEmpty(res.Trim().Replace(" ", "")))
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
                    //regresaBorrador(id);
                    //cargar_perfil_pendiente();
                    break;

                case "cancelar":
                    try
                    {
                        ProcesosENT entidad = new ProcesosENT();
                        ProcesosCOM com = new ProcesosCOM();
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        entidad.Pidc_proceso = Convert.ToInt32(Session["sidc_puestoperfil_borr"]);
                        DataSet ds = new DataSet();
                        string vmensaje = "";
                        ds = com.CancelarSolicitud(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos procesando la SOLICITUD.", "Espere un Momento", "catalogo_procesos.aspx", "imagenes/loading.gif", "2000", "Proceso Cancelado correctamente ", this);
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
                case "preview":
                    Session["Caso_Confirmacion"] = "preview";
                    Session["url"] = "subprocesos_captura.aspx?preview=VSBIUVSBXOJQWSBXOIWJSBXIWJSBXOIJBQSIOXBQIXSBQSX&type=B&idc_proceso=" + funciones.deTextoa64(vidc.ToString()) + "&urlback=" + funciones.deTextoa64("procesos_autorizar.aspx");
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Desea Visualizar el Manual de Procesos','modal fade modal-info');", true);
                    break;
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
                        ds = componente.CargarSubProcesos(entidad);
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

        protected void grid_perfilpendiente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
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

        protected void lnktodo_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            lnk.CssClass = lnk.CssClass == "btn btn-default btn-block" ? "btn btn-info btn-block" : "btn btn-default btn-block";
            Repeater1.Visible = lnk.CssClass == "btn btn-default btn-block" ? true : false;
        }

        private String Cadena()
        {
            string cadena = "";
            if (lnktodo.CssClass != "btn btn-info btn-block")
            {
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    LinkButton lnk = (LinkButton)item.FindControl("lnkgrupo");
                    if (lnk.CssClass == "btn btn-success btn-block")
                    {
                        cadena = cadena + lnk.CommandName + ";";
                    }
                }
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
            return cadena;
        }
    }
}