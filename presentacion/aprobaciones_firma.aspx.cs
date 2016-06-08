using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class autorizaciones_firma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //guardamos el id de la solicitud de aprobacion en un campo oculto
                if (Session["sidc_aprobacion_soli"] != null)
                {
                    oc_idc_aprobacion_soli.Value = Session["sidc_aprobacion_soli"].ToString();
                }
                else
                {
                    oc_idc_aprobacion_soli.Value = "0";
                }
                cargarFirmas();

                //add 18-09-2015
                if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_REFERER"]))
                {
                    oc_paginaprevia.Value = Request.ServerVariables["HTTP_REFERER"].ToString();
                }
                else
                {
                    oc_paginaprevia.Value = "menu.aspx";
                }
            }
            int id_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            PanelCancelar.Visible = funciones.autorizacion(id_usuario, 330);////AQUI cambia
        }

        protected void cargarFirmas()
        {
            //instanciar la entidad
            //AprobacionesENT entidad = new AprobacionesENT();
            //entidad.Idc_puestoperfil = 12;
            //componente
            DataSet ds = new DataSet();
            AprobacionesCOM componente = new AprobacionesCOM();
            int idc_aprobacion_soli = Convert.ToInt32(Session["sidc_aprobacion_soli"]);
            ds = componente.usuarios_firma(idc_aprobacion_soli);
            //llenamos el repeater
            repit_usuarios.DataSource = ds.Tables[0];
            repit_usuarios.DataBind();
        }

        protected void repit_usuarios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView dbr = (DataRowView)e.Item.DataItem;
                Label lblusuario = (Label)e.Item.FindControl("lblusuario");
                //lblusuario.Text = DataBinder.Eval(dbr, "nombre").ToString();
                HiddenField oc_idc_usuario = (HiddenField)e.Item.FindControl("oc_idc_usuario");
                oc_idc_usuario.Value = DataBinder.Eval(dbr, "idc_usuario").ToString();
                //oculto
                HiddenField oc_idc_aprobacion_reg = (HiddenField)e.Item.FindControl("oc_idc_aprobacion_reg");
                oc_idc_aprobacion_reg.Value = DataBinder.Eval(dbr, "idc_aprobacion_reg").ToString();
                //txt
                TextBox txtusuario = (TextBox)e.Item.FindControl("txtusuario");
                txtusuario.Text = DataBinder.Eval(dbr, "usuario").ToString();
                //10-10-2015
                string aprobado = DataBinder.Eval(dbr, "aprobado").ToString();
                Panel panelbtn = (Panel)e.Item.FindControl("panelbtn");
                Panel panelmensj = (Panel)e.Item.FindControl("panelmensaje");
                Label lblmensaje = (Label)e.Item.FindControl("lblmensaje");
                string comentarios = DataBinder.Eval(dbr, "comentarios").ToString();
                if (aprobado == "False" && comentarios == "")
                { //es null debe aparecer los botones
                    panelbtn.Visible = true;
                    panelmensj.Visible = false;
                }
                if (aprobado == "True")
                { // no aparecer botones y si mensaje
                    panelbtn.Visible = false;
                    panelmensj.Visible = true;
                    lblmensaje.Text = (comentarios == "") ? "Aprobado" : "Aprobado: " + comentarios;
                }
                if (aprobado == "False" && comentarios != "")
                {
                    panelbtn.Visible = false;
                    panelmensj.Visible = true;
                    lblmensaje.Text = (comentarios == "") ? "No Aprobado (sin comentarios)" : "No Aprobado: " + comentarios;
                }
            }
        }

        protected void btnFirmar_Click(object sender, EventArgs e)
        {
            string aprobar_val = modal_ocaprobado.Value;
            if (aprobar_val == "False" && txtobs.Text == "")
            {
                Alert.ShowAlertError("Debe colocar comentarios para rechazar", this);
            }
            else
            {
                try
                {
                    //recuperamos los valores
                    int vidc_aprobacion_reg = Convert.ToInt32(modal_ocidc_aprobacion_reg.Value);
                    string vusuario = modal_ocusuario.Value;
                    bool vaprobado = Convert.ToBoolean(modal_ocaprobado.Value);
                    string vcontraseña = txtpass.Text.ToUpper();
                    string vcomentarios = txtobs.Text;
                    int Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                    string Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    string Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    string Pusuariopc = funciones.GetUserName();//usuario pc
                                                                //llamamos al componente
                    AprobacionesCOM componente = new AprobacionesCOM();
                    DataSet ds = new DataSet();
                    ds = componente.validar_firma(vusuario, vcontraseña, vaprobado, vidc_aprobacion_reg, vcomentarios, Idc_usuario, Pdirecip, Pnombrepc, Pusuariopc);
                    //mesaje del sp
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    bool tabla = Convert.ToBoolean(ds.Tables[0].Rows[0]["copia_archi"]);
                    if (string.IsNullOrEmpty(vmensaje) && tabla == true) // si esta vacio todo bien
                    {
                        DataTable tabla_archivos = ds.Tables[1];
                        bool correct = true;
                        int T = tabla_archivos.Rows.Count;
                        foreach (DataRow row_archi in tabla_archivos.Rows)
                        {
                            correct = CopiarArchivos(row_archi["origen"].ToString(), row_archi["destino"].ToString());
                            if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo " + row_archi["origen"].ToString() + ", verifiquelo con el Departamento de Sistemas", this); }
                        }
                        if (correct == true)
                        {
                            int t_archivos_eti = 0;
                            foreach (DataRow row in tabla_archivos.Rows)
                            {
                                string path = row["origen"].ToString();
                                t_archivos_eti = t_archivos_eti + (File.Exists(path) ? 1 : 0);
                            }
                            int total = ((t_archivos_eti * 1) + 1) * 1000;
                            string t = total.ToString();
                            cargarFirmas();
                            limpiarModal();
                            List<String> listas_url = (List<String>)Session["lista"];
                            int index = listas_url.Count;
                            String url = listas_url[index - 1];

                            if (url == null || url == "") { url = "menu.aspx"; }
                            url = url.Replace("%20", "+");
                            String path_actual_COMPLETO = HttpContext.Current.Request.Url.AbsoluteUri;
                            string PreviousPage = Request.ServerVariables["HTTP_REFERER"];
                            path_actual_COMPLETO = path_actual_COMPLETO.Replace("%20", "+");
                            if (url == path_actual_COMPLETO)
                            {
                                listas_url.RemoveAt(index - 1);
                                Session["lista"] = listas_url;
                                index = listas_url.Count;
                                url = listas_url[index - 1];
                                if (url == null || url == "") { url = "menu.aspx"; }
                                url = url.Replace("%20", "+");
                                Alert.ShowGiftRedirect("Estamos Generando el Perfil e Producción y copiando los archivos anexos.", "Espere un Momento", "imagenes/loading.gif", t, url, this);
                            }
                            else
                            {
                                Alert.ShowGiftRedirect("Estamos Generando el Perfil e Producción y copiando los archivos anexos.", "Espere un Momento", "imagenes/loading.gif", t, url, this);
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(vmensaje) && tabla == false) // si esta vacio todo bien
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('Movimiento Realiuzado Correctamente','aprobaciones_pendiente.aspx');", true);
                    }
                    if (!string.IsNullOrEmpty(vmensaje))
                    {
                        //limpiarModal();
                        string aprobar = modal_ocaprobado.Value;
                        string titulo = "";
                        string aprobado = "";
                        if (aprobar == "True")
                        {
                            titulo = "Aprobar";
                            aprobado = "1";
                        }
                        else if (aprobar == "False")
                        {
                            titulo = "No Aprobar";
                            aprobado = "0";
                        }
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('" + titulo + "', '" + aprobado + "', '"+vmensaje+"');", true);
                        Alert.ShowAlertError(vmensaje, this.Page);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    //msgbox.show(ex.Message, this.Page);
                    Alert.ShowAlertError(ex.Message, this.Page);
                }
            }
        }

        public bool CopiarArchivos(string sourcefilename, string destfilename)
        {
            try
            {
                if (!File.Exists(sourcefilename))
                {
                    Alert.ShowAlertError("No existe la ruta " + sourcefilename, this);
                    return false;
                }
                if (File.Exists(sourcefilename) && sourcefilename != destfilename)
                {
                    File.Copy(sourcefilename, destfilename, true);
                    return true;
                }

                return true;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
                return false;
            }
        }

        protected void repit_usuarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //ejecuta el script manager
            //recuperamos valores que necesitamos mandar al sp para firmar por ejemplo
            //usuario, idc_aprobacion_reg, password, si es para firma o no
            TextBox textusuario = (TextBox)e.Item.FindControl("txtusuario");
            modal_ocusuario.Value = textusuario.Text;
            //----
            HiddenField ocultoidc_aprobacion_reg = (HiddenField)e.Item.FindControl("oc_idc_aprobacion_reg");
            modal_ocidc_aprobacion_reg.Value = ocultoidc_aprobacion_reg.Value.ToString();
            string titulo = "";
            string aprobado = "";
            //----
            if (e.CommandName == "aprobar")
            {
                //true
                modal_ocaprobado.Value = "True";
                titulo = "Aprobar";
                aprobado = "1";
            }
            else if (e.CommandName == "noaprobar")
            {
                //false
                modal_ocaprobado.Value = "False";
                titulo = "No aprobar";
                aprobado = "0";
            }
            else
            {
                Alert.ShowAlertError("Acción no controlada ID 5422. Consulta al depto de Sistemas.", this.Page);
                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('" + titulo + "', '" + aprobado + "');", true);
        }

        protected void limpiarModal()
        {
            modal_ocusuario.Value = "";
            modal_ocaprobado.Value = "";
            modal_ocidc_aprobacion_reg.Value = "";
            txtpass.Text = "";
            txtobs.Text = "";
            oc_idc_aprobacion_soli.Value = "0";
        }

        protected void No_Click(object sender, EventArgs e)
        {
            limpiarModal();
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            limpiarModal();
            Response.Redirect(oc_paginaprevia.Value);
        }

        protected void btnCancelarAprob_Click(object sender, EventArgs e)
        {
            if (txtcoments.Text == "") { Alert.ShowAlertError("Para Cancelar la Aprobación debe colocar comentarios", this); }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar esta Aprobación?');", true);
            }
        }

        protected void btnYesCo_Click(object sender, EventArgs e)
        {
            int idc_aprobacion_soli = Convert.ToInt32(Session["sidc_aprobacion_soli"]);
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
            if (string.IsNullOrEmpty(vmensaje)) //si esta vacio todo bien
            {
                Alert.ShowAlert("Listo solicitud de aprobacion cancelada", "Mensaje", this.Page);
            }
            else
            {
                //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                Alert.ShowAlertError(vmensaje, this.Page);
                return;
            }
        }
    }
}