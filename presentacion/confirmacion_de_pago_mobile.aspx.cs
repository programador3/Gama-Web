using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;

namespace presentacion
{
    public partial class confirmacion_de_pago_mobile : System.Web.UI.Page
    {
        public AgentesCOM componente = new AgentesCOM();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargar_combo_bancos();
                estado_controles(false);
                btnaceptar.Attributes["onClick"] = "return pago(" + cbotipopago.ClientID + ".options[" + cbotipopago.ClientID + ".selectedIndex]," + cbobancos.ClientID + ".options[" + cbobancos.ClientID + ".selectedIndex]);";
                btncancelar.Attributes["onClick"] = "return cerrar();";
                txtmonto.Attributes["onblur"] = "return validarnumero(this);";
                txtmonto.Attributes["onClick"] = "this.select();";
                txtfecha.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                cbotipopago.Attributes["onchange"] = "return change_cbotipo(this);";
                //cbotipopago.Attributes["onClick"] = "return ddl_focus(this);"
                txtmonto.Attributes["onfocus"] = "this.blur();";
                txtmonto.Attributes["onClick"] = "window.open('teclado.aspx?ctrl=" + txtmonto.ClientID + "&dc= 4');";
            }
        }

        public void cargar_combo_bancos()
        {
            try
            {
                DataSet ds = componente.sp_combo_bancos();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbobancos.DataSource = ds;
                    cbobancos.DataTextField = "nom_corto";
                    cbobancos.DataValueField = "idc_banco";
                    cbobancos.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void estado_controles(bool estado)
        {
            txtmonto.Enabled = estado;
            //txtfecha.Enabled = estado
            cbobancos.Enabled = estado;


        }
    }
}