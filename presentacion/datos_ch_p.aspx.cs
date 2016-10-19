using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class datos_ch_p : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string idc_cliente = Request.QueryString["cdi"];
                string idc_dirchekplus = Request.QueryString["cdi_dir"];
                cargar_cliente_checkplus(Convert.ToInt32(idc_cliente), Convert.ToInt32(idc_dirchekplus));
            }

        }
        public void cargar_cliente_checkplus(int idc_cliente, int idc_dirchekplus)
        {
           
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_clientes_chekplus(idc_cliente, idc_dirchekplus);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow rows = ds.Tables[0].Rows[0];
                    txtpais.Text = rows["pais"].ToString();
                    txtnombrepersona.Text = rows["nombre"].ToString();
                    txtclave.Text = rows["clave_elector"].ToString();
                    txtfolio2.Text = rows["folio_elector"].ToString();
                    txtcalle.Text = rows["calle"].ToString();
                    txtnumero.Text = rows["numero"].ToString();
                    txtcolonia.Text = rows["colonia"].ToString();
                    txtmunicipio.Text = rows["mpio"].ToString();
                    txtestado.Text = rows["edo"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('No Existen Datos')</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "dededededed", "<script>window.close();</script>", false);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.ToString() + "')</script>");
            }
        }

    }
}