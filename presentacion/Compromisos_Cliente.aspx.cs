using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class Compromisos_Cliente : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)//ds.Tables[0].Rows[0]["mensaje"].ToString()
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                int idc_cliente = Request.QueryString["idc_cliente"] != null ?  Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"])):0;
                cargar_datos_cliente(idc_cliente);//funciones.de64aTexto( Request.QueryString["idc_cliente"])
                cargar_compromisos_cliente(idc_cliente);
            }
        }

        private void cargar_datos_cliente(int idc_cliente)
        {
            try
            {
                Compromisos_ClienteCOM COMP = new Compromisos_ClienteCOM();
                Compromisos_ClienteENT ENTI = new Compromisos_ClienteENT();
                ENTI.Pidc_cliente = idc_cliente;
                DataSet ds = COMP.Datos_Cliente(ENTI);
                DataRow row = ds.Tables[0].Rows[0];
                txtcliente.Text = row["nombre"].ToString();
                txtrfc.Text = row["rfccliente"].ToString();
                txtcve.Text = row["cveadi"].ToString();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }

        }

        public void cargar_compromisos_cliente(int idc_cliente)
        {
            try
            {
                Compromisos_ClienteCOM  com = new Compromisos_ClienteCOM();
                Compromisos_ClienteENT entidad = new Compromisos_ClienteENT();
                Datos_Usuario_logENT Datos_ent = new Datos_Usuario_logENT();
                entidad.Pidc_cliente = idc_cliente;
                Datos_ent.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = com.Clientes_Compromisos(entidad,Datos_ent);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridcompromisos.DataSource = ds.Tables[0];
                    gridcompromisos.DataBind();
                    gridcompromisos.Columns[5].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["autorizado"]);
                }
                else
                {
                    NO_Hay.Visible = true;
                }  
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
            //------------
        }
        
        protected void gridcompromisos_RowCommand(object sender, GridViewCommandEventArgs e)
        {        
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_clicompromiso"]  = gridcompromisos.DataKeys[index].Values["idc_clicompromiso"];
            string fecha = gridcompromisos.DataKeys[index].Values["fecha"].ToString();
            string usuario = gridcompromisos.DataKeys[index].Values["usuario"].ToString();
            string desc = gridcompromisos.DataKeys[index].Values["descripcion"].ToString();
            Session["Caso_Confirmacion"] = e.CommandName;
            switch (e.CommandName)
            {
                case "Revisar":
                    if ((int)Session["idc_clicompromiso"] > 0) 
                    {                        
                        txtDescripcion.Text = desc;
                        txtFecha.Text = fecha;
                        txtUsuario.Text = usuario;
                        ddlCumplido.SelectedIndex = 0;
                        txtObservaciones.Text = "";
                        contenido_RevisarCompromiso.Visible = true;                        
                        Contenido_NuevoCompromiso.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalEditar('Mensaje del Sistema','Revisar Compromiso','modal fade modal-info');", true);
                    }
                    break;

            }
        }

        protected void lnkNuevo_Click(object sender, EventArgs e)
        {
            txtcompromiso.Text = "";
            Cargr_Combo_Tipo_Compromiso();
            Session["Caso_Confirmacion"] = "Guardar";
            Contenido_NuevoCompromiso.Visible = true;
            contenido_RevisarCompromiso.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalEditar('Mensaje del Sistema','Nuevo Compromiso','modal fade modal-info');", true);
        }

        protected void btnCancelarTodo_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("ficha_cliente_m.aspx");
        }
        private void Cargr_Combo_Tipo_Compromiso()
        {
            //sp_combo_tipos_compromisos_cli
            try
            {                
                Compromisos_ClienteCOM comp = new Compromisos_ClienteCOM();
                DataSet ds = comp.Carga_T_Compromisos();
                ddlT_Compromisos.DataValueField = "idc_tipocomcli";
                ddlT_Compromisos.DataTextField = "descripcion";
                ddlT_Compromisos.DataSource = ds.Tables[0];
                ddlT_Compromisos.DataBind();
                ddlT_Compromisos.Items.Insert(0, new ListItem("--Seleccione una Opción", "0")); //updated code}

            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }

        }

        /* ------------guardar_compromiso_cliente--------------*/
        protected void guardar_click(object sender, EventArgs e)
        {
            try
            {
                string confirma = (string)Session["Caso_Confirmacion"];
                Compromisos_ClienteENT ent = new Compromisos_ClienteENT();
                Datos_Usuario_logENT dul = new Datos_Usuario_logENT();

                dul.Pdirecip = funciones.GetLocalIPAddress();       //direccion ip de usuario
                dul.Pnombrepc = funciones.GetPCName();              //nombre pc usuario
                dul.Pusuariopc = funciones.GetUserName();           //usuario pc
                dul.Pidc_usuario = (int)Session["sidc_usuario"];    //usuario idc_usuario                
                string vmensaje = "";
                Compromisos_ClienteCOM COMP = new Compromisos_ClienteCOM();
                DataSet ds = new DataSet();
                if (ddlT_Compromisos.SelectedValue.ToString() == "0")
                {
                    vmensaje = "Seleccione un Tipo de Compromiso";
                }
                else {
                    switch (confirma)
                    {
                        case "Guardar":
                            ent.Pidc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
                            ent.Pidc_tipocomcli = Convert.ToInt32(ddlT_Compromisos.SelectedValue);
                            ent.Pcompromiso = txtcompromiso.Text;
                            ds = COMP.guardar_compromiso_cliente(ent, dul);
                            break;

                        case "Revisar":
                            ent.Pidc_clicompromiso = (int)Session["idc_clicompromiso"];
                            ent.Pobserv = txtObservaciones.Text;           //-txtObservaciones
                            ent.Pcompletada = ddlCumplido.SelectedIndex == 0 ? false : true;
                            ds = COMP.clientes_compromisos_revisar(ent, dul);
                            break;
                    }

                    vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();

                }

                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos procesando el registro.", "Espere un Momento", string.Format("Compromisos_Cliente.aspx?idc_cliente={0}",Request.QueryString["idc_cliente"]), "imagenes/loading.gif", "2000", "Procesada Correctamente", this);
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

    }
}