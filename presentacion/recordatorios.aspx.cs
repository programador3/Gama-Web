using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;

namespace presentacion
{
    public partial class recordatorios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {

            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                QuejasENT entidad = new QuejasENT();
                QuejasCOM com = new QuejasCOM();
                entidad.Pobservaciones = txtasunto_rec.Text;
                entidad.Pencargado = txtcorreo_rec.Text;
                entidad.Pobservaciones_satisfecho = txtdesc_rec.Text;
                entidad.Pfecha = Convert.ToDateTime(txtfecha_rec.Text);
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = new DataSet();
                string vmensaje = "";
                switch (caso)
                {
                    case "Guardar":
                        ds = com.AgregarRecordatorio(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;
                }
                if (vmensaje == "")
                {
                    string url = Session["post"] == null ? "menu.aspx" : Session["post"] as string;
                    Alert.ShowGiftMessage("Estamos procesando la solicitud", "Espere un Momento", url, "imagenes/loading.gif", "2000", "Recordatorio Agregado Correctamente", this);
                }
                else {

                    Alert.ShowAlertError(vmensaje, this.Page);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtasunto_rec.Text == "" || txtfecha_rec.Text == "" || txtdesc_rec.Text == "")
            {
                Alert.ShowAlertError("Complete los campos", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar este Recordatorio?','modal fade modal-info');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string url = Session["post"] == null ? "menu.aspx" : Session["post"] as string;
            Response.Redirect(url);
        }
    }
}