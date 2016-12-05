using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web;

namespace presentacion
{
    public partial class Reproductor_llamadas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idc_llamada"] != null)//si no hay session logeamos
            {
                //string id = string.Format("{0},{1},{2},{3},{4}", funciones.deTextoa64("5454312"), funciones.deTextoa64("5454313"), funciones.deTextoa64("5454314"), funciones.deTextoa64("5454315"), funciones.deTextoa64("1"));

                if (!IsPostBack)
                {

                    string idc_llamada = funciones.de64aTexto(Request.QueryString["idc_llamada"].ToString());
                    validar_llamada(Convert.ToInt32(idc_llamada));
                    h_idc_Usuario.Value = funciones.de64aTexto(Request.QueryString["sidc_usuario"].ToString());
                    h_idc_llamada.Value = idc_llamada;

                }
            }
            else
            {
                //div_reproductor.Visible = false;
                lblPista.Text = "No existe audio.";
                div_Observaciones.Visible = false;
                div_marcar.Visible = false;
                div_tipo.Visible = false;
                Alert.ShowAlertInfo("Parametros Incorrectos.","Mensaje del Sistema",this);
            }

        }
        private void validar_llamada(int idc_llamada)
        {
            reproductor_llamadasCOM com = new reproductor_llamadasCOM();
            reproductor_llamadasENT ent = new reproductor_llamadasENT();

            ent.Pidc_llamada = idc_llamada;
            DataSet ds = com.Consulta_llamada(ent);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string path_Audio = ds.Tables[0].Rows[0]["ruta_Audio"].ToString();
                string Audio = ds.Tables[0].Rows[0]["Audio"].ToString();
                //string Audio = "20160419-125753-1461088673.15001.wav";
                string url = HttpContext.Current.Server.MapPath("/Audios/") + Audio;
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/Audios/"));//path local
                string Nueva_Ruta = dirInfo + Audio;
                bool correct = funciones.CopiarArchivos(path_Audio + Audio, Nueva_Ruta, this);

                if (correct)
                {
                    string str_script = string.Format("pista('Audios/{0}','{0}');", Audio);
                    lblPista.Text = Audio;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", str_script, true);

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        
                        h_idc_llamadamarcar.Value = ds.Tables[1].Rows[0]["idc_llamadamarcar"].ToString();
                        txtObservaciones.Text = ds.Tables[1].Rows[0]["observaciones"].ToString();
                        string tipo = ds.Tables[1].Rows[0]["tipo"].ToString();
                        chkBueno.Checked = tipo == "C" ? true : false;
                        chkMalo.Checked = tipo == "M" ? true : false;
                        txtObservaciones.Enabled = false;
                        btnMarcar.Text = "Desmarcar";
                        // = "btn-warning";
                    }
                    else
                    {
                        btnMarcar.Text = "Marcar";
                        txtObservaciones.Enabled = true;
                    }
                }
            }
            else {
                div_reproductor.Visible = false;
                div_Observaciones.Visible = false;
                div_marcar.Visible = false;
                div_tipo.Visible = false;
            }
        }


        protected void Marcar_Click(object sender, EventArgs e)
        {

            if (btnMarcar.Text == "Marcar" && txtObservaciones.Text != "" && (chkBueno.Checked == true || chkMalo.Checked == true))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea guardar las observaciones?','modal fade modal-info');", true);
            }
            else if (btnMarcar.Text == "Desmarcar")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Borrar las observaciones?','modal fade modal-info');", true);
            }
            else
            {
                Alert.ShowAlertInfo("Agregue una Observacion y Seleccione la calidad de la llamada.", "Mensaje del Sistema", this);
            }




        }

        protected void Guardar_Click(object sender, EventArgs e)
        { 
            string caso = btnMarcar.Text;
            try
            {
                reproductor_llamadasENT ent = new reproductor_llamadasENT();
                reproductor_llamadasCOM com = new reproductor_llamadasCOM();
                Datos_Usuario_logENT dul = new Datos_Usuario_logENT();

                dul.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                dul.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                dul.Pusuariopc = funciones.GetUserName();//usuario pc
                dul.Pidc_usuario = Convert.ToInt32(h_idc_Usuario.Value);

                ent.Pidc_llamada = Convert.ToInt32(h_idc_llamada.Value);


                DataSet ds = new DataSet();
                string vmensaje = "";
                switch (caso)
                {
                    case "Marcar":
                        ent.PObservaciones = txtObservaciones.Text;
                        ent.Ptipo = chkBueno.Checked == true ? "C" :  "M" ;
                        
                        
                        ds = com.Marcar(dul, ent);
                        break;

                    case "Desmarcar":
                        ent.Pidc_llamadamarcar = Convert.ToInt32(h_idc_llamadamarcar.Value);
                        ds = com.Marcar(dul, ent);

                        break;

                    


                }
                vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();

                if (vmensaje == "")
                {
                    string pagina = string.Format("Reproductor_llamadas.aspx?sidc_usuario={0}&idc_llamada={1}", funciones.deTextoa64(h_idc_Usuario.Value), funciones.deTextoa64(h_idc_llamada.Value));
                    Alert.ShowGiftMessage("Estamos procesando el registro.", "Espere un Momento", pagina, "imagenes/loading.gif", "2000", "Procesada Correctamente", this);
                }
                else
                {
                    Alert.ShowAlertInfo(vmensaje, "Mensaje del Sistema", this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void chkBueno_Click(object sender, EventArgs e)
        {
            chkMalo.Checked = !chkBueno.Checked;
        }

        protected void chkMalo_Click(object sender, EventArgs e)
        {
            chkBueno.Checked = !chkMalo.Checked;
        }
    }
}