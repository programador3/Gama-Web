using Gios.Pdf;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Text;


namespace presentacion
{
    public partial class agregar_clientes_chekplus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtcliente.Text = Request.QueryString["cliente"];
                txtrfc.Text = Request.QueryString["rfc"];
                txtcve.Text = Request.QueryString["cve"];
                txtidc_cliente.Text = funciones.de64aTexto(Request.QueryString["id"]);
                btnaceptar.Attributes["onclick"] = "return guardar(" + txtnombre.ClientID + "," + txtclave.ClientID + "," + txtfolio.ClientID + "," + txtcalle.ClientID + "," + txtnumero.ClientID + "," + txtidc_colonia.ClientID + ");";
                txtclave.Attributes["onkeyup"] = "if(this.value.length>=18){" + txtfolio.ClientID + ".focus();this.value = this.value.substr(0,18);}";
                txtfolio.Attributes["onkeydown"] = "return soloNumeros2(event,false);";
                txtfolio.Attributes["onkeyup"] = "if(this.value.length>=13){" + txtcalle.ClientID + ".focus(); this.value=this.value.substr(0,13);}";
                txtnumero.Attributes["onkeydown"] = "return soloNumeros2(event,false);";
                imgbusc_colonia.Attributes["onclick"] = "return buscar_colonia();";
            }

        }

        protected void btnaceptar_Click(object sender, EventArgs e)
        {

            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_aclientes_chekplus_nuevo(Convert.ToInt32(Session["sidc_usuario"]),
                      funciones.GetLocalIPAddress(), funciones.GetPCName(), funciones.GetUserName(), " ", txtnombre.Text.Trim(), txtfolio.Text.Trim(), 
                      txtclave.Text.Trim(), txtcalle.Text.Trim(), txtnumero.Text.Trim(),
            Convert.ToInt32(txtidc_colonia.Text.Trim()), Convert.ToInt32(txtidc_cliente.Text.Trim()));
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje == "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Cerrar", "<script>AlertGO('Se Guardo el Registro con exito.');</script>", false);
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