using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class tareas_clientes_rev : System.Web.UI.Page
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
                txtfecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }


        public void cargar_tareas(int idc_cliente, int idc_agente)
        {
            int idc_clientetarea = 0;
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                entidad.Pidc_agente = idc_agente;
                entidad.pfecha = Convert.ToDateTime(txtfecha.Text);
                DataSet ds = com.sp_ver_tareas_agente_detalles(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataView dv = new DataView();
                    dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = "idc_cliente = " + idc_cliente;
                    if (dv.ToTable().Rows.Count > 0)
                    {
                        txtcliente.Text = dv.ToTable().Rows[0]["nombre"].ToString().Trim();
                        txtrfc.Text = dv.ToTable().Rows[0]["rfccliente"].ToString().Trim();
                        txtcve.Text = dv.ToTable().Rows[0]["cveadi"].ToString().Trim();
                        txtobservaciones.Text = dv.ToTable().Rows[0]["obs_venta"].ToString().Trim();
                        idc_clientetarea = Convert.ToInt32(dv.ToTable().Rows[0]["idc_clientetarea"].ToString().Trim());
                    }
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (idc_clientetarea > 0)
                    {
                        DataView dv_arts = new DataView();
                        dv_arts = ds.Tables[1].DefaultView;
                        dv_arts.RowFilter = "idc_clientetarea = " + idc_clientetarea.ToString();

                        if (dv_arts.ToTable().Rows.Count > 0)
                        {
                            grdarts.DataSource = dv_arts;
                            grdarts.DataBind();
                        }
                        else
                        {
                            grdarts.DataSource = null;
                            grdarts.DataBind();
                        }
                    }
                }
                else
                {
                    Alert.ShowAlertError("No Hay Tareas Registradas para la Fecha Seleccionada.", this.Page);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int idc_agente = 0;
            int idc_cliente = 0;
            idc_agente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_agente"]));
            idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
            if (idc_agente > 0 & idc_cliente > 0)
            {
                if (!string.IsNullOrEmpty(txtfecha.Text))
                {
                    cargar_tareas(idc_cliente, idc_agente);
                }
                else
                {
                    Alert.ShowAlertError("seleccione fecha", this.Page);
                    return;
                }
            }
        }

        protected void grdarts_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Guardar")
                {
                    int idc_clientetareadet = Convert.ToInt32(e.Item.Cells[0].Text);
                    string obs = "";
                    string cumplida = "0";
                    string articulos = "";
                    TextBox txtobs = e.Item.FindControl("txtobs") as TextBox;
                    if ((txtobs != null))
                    {
                        obs = txtobs.Text;
                    }

                    CheckBox chkcumplida = e.Item.FindControl("chkcumplida") as CheckBox;
                    if ((chkcumplida != null))
                    {
                        cumplida = (chkcumplida.Checked == true ? "1" : "0");
                    }

                    articulos = articulos + idc_clientetareadet + ";" + cumplida + ";" + obs + ";";
                    AgentesENT entidad = new AgentesENT();
                    AgentesCOM com = new AgentesCOM();
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    entidad.Pidc_agente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_agente"])); ;
                    entidad.pfecha = Convert.ToDateTime(txtfecha.Text);
                    entidad.Pcadenaarti = articulos;
                    entidad.Ptotalcadenaarti = 1;
                    DataSet ds = com.sp_atareas_clientes_rev_nuevo(entidad);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (vmensaje == "")
                    {
                        int idc_agente = 0;
                        int idc_cliente = 0;
                        idc_agente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_agente"]));
                        idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
                        cargar_tareas(idc_cliente, idc_agente);
                        Alert.ShowAlert("Tarea Actualizada", "Mensaje del Sistema", this);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje, this);
                    }
                }
                else if (e.CommandName == "negociar")
                {
                    int idc_articulo = Convert.ToInt32(e.Item.Cells[1].Text);
                    Response.Redirect("cotizacion_clientes2_m.aspx?cdi=" + idc_articulo);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ficha_cliente_m.aspx");           
        }
    }
}