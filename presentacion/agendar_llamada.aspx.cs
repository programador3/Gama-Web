using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class agendar_llamada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (Session["sidc_usuario"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {

                txtfecha.Text = DateTime.Now.AddMinutes(-5).ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtfecha.Text == "")
            {

                Alert.ShowAlertError("Ingrese una Fecha y Hora", this.Page);
            }
            else if (Convert.ToDateTime(txtfecha.Text) < DateTime.Now)
            {
                Alert.ShowAlertError("La Fecha debe ser mayor a hoy", this.Page);
            }
            else if (txtobservaciones.Text != "" && txtobservaciones.Text.Length > 8000)
            {
                Alert.ShowAlertError("Sobrepaso el numero de caracteres para las observaciones(8000)", this.Page);
            }
            else {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar?','modal fade modal-info');", true);
            }
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                switch (caso)
                {
                    case "Guardar":
                        
                        AgentesENT entidad = new AgentesENT();
                        AgentesCOM com = new AgentesCOM();
                        entidad.Pidc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));                       
                        entidad.pfecha = Convert.ToDateTime(txtfecha.Text);
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        entidad.pfecha = Convert.ToDateTime(txtfecha.Text);
                        entidad.Pobsr = txtobservaciones.Text;                  
                        DataSet ds = com.sp_arellamar_ven_nuevo(entidad);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos procesando la asignacion.", "Espere un Momento", "ficha_cliente_m.aspx", "imagenes/loading.gif", "2000", "Información Guardada Correctamente", this);
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
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ficha_cliente_m.aspx");

        }
    }
}