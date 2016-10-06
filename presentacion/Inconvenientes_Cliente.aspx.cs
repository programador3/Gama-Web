using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio.Componentes;
using negocio.Entidades;
using System.Data;

namespace presentacion
{
    public partial class Inconvenientes_Cliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idc_cliente = Request.QueryString["idc_cliente"] != null ? Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"])) : 0;
                cargar_datos_cliente(idc_cliente);
                cargar_inconvenientes(idc_cliente);
            }
        }

        private void cargar_datos_cliente(int idc_cliente)
        {
            try
            {
                Inconvenientes_ClienteCOM COMP = new Inconvenientes_ClienteCOM();
                Inconvenientes_ClienteENT ENTI = new Inconvenientes_ClienteENT();
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

        private void cargar_inconvenientes(int idc_cliente)
        {
            try
            {
                Inconvenientes_ClienteCOM COMP = new Inconvenientes_ClienteCOM();
                Inconvenientes_ClienteENT entidad = new Inconvenientes_ClienteENT();
                entidad.Pidc_cliente = idc_cliente;
                DataSet ds = COMP.Inconvenientes(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridinconv.DataSource = ds.Tables[0];
                    gridinconv.DataBind();
                    
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

        }

        protected void lnkNuevo_Click(object sender, EventArgs e)
        {

            txtInconveniente.Text = "";
            Session["Caso_Confirmacion"] = "Guardar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalEditar('Mensaje del Sistema','Nuevo Inconveniente','modal fade modal-info');", true);
        }

        protected void btnCancelarTodo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ficha_cliente_m.aspx");
        }

        protected void guardar_click(object sender, EventArgs e)
        {
            
            try
            {
                string vmensaje = "";
                if (txtInconveniente.Text == "")
                {
                    vmensaje = "Campo Vacio.";
                }
                else
                {
                    string confirma = (string)Session["Caso_Confirmacion"];
                    Inconvenientes_ClienteENT ent = new Inconvenientes_ClienteENT();
                    Datos_Usuario_logENT dul = new Datos_Usuario_logENT();

                    dul.Pdirecip = funciones.GetLocalIPAddress();       //direccion ip de usuario
                    dul.Pnombrepc = funciones.GetPCName();              //nombre pc usuario
                    dul.Pusuariopc = funciones.GetUserName();           //usuario pc
                    dul.Pidc_usuario = (int)Session["sidc_usuario"];    //usuario idc_usuario                

                    Inconvenientes_ClienteCOM COMP = new Inconvenientes_ClienteCOM();
                    DataSet ds = new DataSet();
                    switch (confirma)
                    {
                        case "Guardar":
                            ent.Pidc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
                            ent.Pinconveniente = txtInconveniente.Text;
                            ds = COMP.guardar_compromiso_cliente(ent, dul);
                            break;

                    }

                    vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                }

                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos procesando el registro.", "Espere un Momento", string.Format("Inconvenientes_Cliente.aspx?idc_cliente={0}", Request.QueryString["idc_cliente"]), "imagenes/loading.gif", "2000", "Procesada Correctamente", this);
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