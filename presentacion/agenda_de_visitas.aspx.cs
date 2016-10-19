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
    public partial class agenda_de_visitas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (Session["sidc_usuario"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "edeededded", "Giftq('Estamos Cargando TODA la información');", false);
                cargar_combo_agentes_usuario();
                ImageButton imgregresar = new ImageButton();
                imgregresar = Page.Master.FindControl("imgregresar") as ImageButton;
                if ((imgregresar != null))
                {
                    imgregresar.PostBackUrl = "menu ventas.aspx";
                }

                cboagentes.Attributes["onchange"] = "mostrar_procesar_guard();";
                drpDays.Attributes["onchange"] = "mostrar_procesar_guard();";
            }
        }


        public void cargar_combo_agentes_usuario() { 
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = com.cargar_agentesusuarios(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        cboagentes.DataSource = ds.Tables[0];
                        cboagentes.DataValueField = "idc_agente";
                        cboagentes.DataTextField = "nombre3";
                        cboagentes.DataBind();
                        cargar_agenda(0, Convert.ToInt32(cboagentes.SelectedValue));
                    }
                    else
                    {
                        cboagentes.DataSource = ds.Tables[0];
                        cboagentes.DataValueField = "idc_agente";
                        cboagentes.DataTextField = "nombre3";
                        cboagentes.DataBind();
                        cargar_agenda(0, Convert.ToInt32(cboagentes.SelectedValue));
                    }
                }
                else
                {
                    cboagentes.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void cargar_agenda(int dia, int idc_agente)
        {
            try
            {
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_reporte_agenda_visitas(idc_agente, dia);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdClientes.DataSource = ds.Tables[0];
                    grdClientes.DataBind();
                }
                else
                {
                    grdClientes.DataSource = null;
                    grdClientes.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>alert('" + ex.Message + "'); </script>", false);
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>myStopFunction_guard();</script>", false);
            }
        }

        protected void drpDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboagentes.Items.Count > 0)
            {
                cargar_agenda(Convert.ToInt32(drpDays.SelectedValue), Convert.ToInt32(cboagentes.SelectedValue));
            }

        }

        protected void cboagentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargar_agenda(0, Convert.ToInt32(cboagentes.SelectedValue));
        }

        protected void btncerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("menu.aspx");
        }

        protected void grdClientes_Sorting(object sender, GridViewSortEventArgs e)
        {           
            //string SortField = e.SortExpression;
            //bool sortascending = false;
            //try
            //{
            //    AgentesENT entidad = new AgentesENT();
            //    AgentesCOM com = new AgentesCOM();
            //    DataSet ds = com.sp_reporte_agenda_visitas(Convert.ToInt32(cboagentes.SelectedValue), Convert.ToInt32(drpDays.SelectedValue));
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        DataView dv = new DataView();
            //        string strOrden = null;
            //        dv = ds.Tables[0].DefaultView;
            //        if (string.IsNullOrEmpty(SortField))
            //        {
            //            SortField = "DescripcionSubArea";
            //            sortascending = true;
            //        }
            //        strOrden = SortField + " " + (sortascending == true ? "ASC" : "DESC");
            //        dv.Sort = strOrden;
            //        grdClientes.DataSource = dv;
            //        grdClientes.DataBind();
            //    }
            //    else
            //    {
            //        grdClientes.DataSource = null;
            //        grdClientes.DataBind();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>alert('" + ex.Message + "'); </script>", false);
            //}

        }
    }
}