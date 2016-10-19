using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class consignado_mobile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                int idc_cliente = 0;
                if (Request.QueryString["id"] != null)
                {
                    idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id"]));
                }
                if (!(idc_cliente <= 0))
                {
                    txtid.Text = idc_cliente.ToString();
                }

                int x_ref = Request.QueryString["ref"]==null?0: Convert.ToInt32(Request.QueryString["ref"]);
                if (x_ref > 0)
                {
                    @ref.Text = 1.ToString();
                }


                //btnbuscarcol.Attributes("onclick") = "return colonia();"
                chkton.Attributes["onclick"] = "return false;";
                btnaceptar.Attributes["onclick"] = "return regresar_datos();";
                txtid.Text = funciones.de64aTexto(Request.QueryString["id"]);
                if (!string.IsNullOrEmpty(txtid.Text))
                {
                    Ver_Proyectos_Cliente(Convert.ToInt32(txtid.Text.Trim()));
                }
                else
                {
                    btnDDF.Attributes["onclick"] = "return false;";
                }
                if (funciones.de64aTexto(Request.QueryString["consignado"]) == 1.ToString())
                {
                    btnaceptar.Visible = false;
                    lblcap.Visible = true;
                    txtCP.Attributes["onfocus"] = "this.blur();";
                    txtcalle.Attributes["onfocus"] = "this.blur();";
                    txtnumero.Attributes["onfocus"] = "this.blur();";
                }
                else
                {
                    lblcap.Visible = false;
                }
                ScriptManager.RegisterStartupScript(this, typeof(Page), "dts", "<script>cargar_datos();</script>", false);
            }

        
        }


        public void Ver_Proyectos_Cliente(int idc_cliente)
        {
            DataSet ds = new DataSet();
            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pidc_cliente = idc_cliente;
                AgentesCOM com = new AgentesCOM();
                ds = com.SP_VER_PROYECTOS(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["XProyectos"] = ds.Tables[0];
                    cboproyectos.DataSource = ds;
                    cboproyectos.DataValueField = "idc_proyec";
                    cboproyectos.DataTextField = "nombre";
                    cboproyectos.DataBind();
                    cboproyectos.Items.Insert(0, "-Seleccionar Proyecto-");
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void btnQC_Click(object sender, EventArgs e)
        {
            limpiar_datos_consignado();
        }
        public void limpiar_datos_consignado()
        {
            txtidc_colonia.Text = "0";
            txtmunicipio.Text = null;
            txtestado.Text = null;
            txtpais.Text = null;
            chkton.Checked = false;
            txttoneladas.Text = "0.00";
            txtCP.Text = "0";
            txtcolonia.Text = null;
            lblrestriccion.Text = null;
            txtcalle.Text = null;
            txtnumero.Text = null;
            chkzm.Checked = false;
            imgcroquis.Attributes.Remove("onClick");
            //Remove image´s croquis on Tabcontainer.
            txtproy.Text = "0";
            txtcalle.Attributes.Remove("onfocus");
            txtCP.Attributes.Remove("onfocus");
            txtnumero.Attributes.Remove("onfocus");
        }

        protected void btnDDF_Click(object sender, EventArgs e)
        {

            //Datos_Direccion_Fiscal

            {
                if (!string.IsNullOrEmpty(txtid.Text))
                {
                    if (cboproyectos.Items.Count > 0)
                    {
                        cboproyectos.SelectedIndex = 0;
                        txtproy.Text = "0";
                    }
                    DataSet ds = new DataSet();
                    DataRow row = default(DataRow);
                   
                    try
                    {
                        AgentesENT entidad = new AgentesENT();
                        entidad.Pidc_cliente = Convert.ToInt32(txtid.Text.Trim());
                        AgentesCOM com = new AgentesCOM();
                        ds = com.sp_consignado_df(entidad);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            row = ds.Tables[0].Rows[0];
                            txtidc_colonia.Text = row["idc_colonia"].ToString().Trim();
                            txtmunicipio.Text = row["mpio"].ToString().Trim();
                            txtestado.Text = row["estado"].ToString().Trim();
                            txtpais.Text = row["pais"].ToString().Trim();
                            chkton.Checked =Convert.ToBoolean(row["capacidad_maxima"]);
                            txttoneladas.Text = row["toneladas"].ToString().Trim();
                            txtCP.Text = row["cod_postal"].ToString().Trim();
                            txtcolonia.Text = row["colonia"].ToString().Trim();
                            lblrestriccion.Text = row["restriccion"].ToString().Trim();
                            txtcalle.Text = row["calle"].ToString().Trim();
                            txtnumero.Text = row["numero"].ToString().Trim();
                            chkzm.Checked = Convert.ToBoolean(row["zm"]);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.ToString(), this.Page);
                    }
                }
            }
            

        }

        protected void cboproyectos_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable table = new DataTable();
            DataRow[] row = null;
            int idc_colonia = 0;
            if (cboproyectos.SelectedIndex > 0)
            {
                table = ViewState["XProyectos"] as DataTable;
                row = table.Select("idc_proyec=" + cboproyectos.SelectedValue);
                idc_colonia =Convert.ToInt32(row[0][2]);
                txtcalle.Text = "";
                txtnumero.Text = "";
                txtCP.Text = "";
                cargar_datos_colonia_proyecto(idc_colonia);
                imgcroquis.Attributes["onClick"] = "return verCroquis2();";
                txtCP.Attributes["onfocus"] = "this.blur();";
                txtcalle.Attributes["onfocus"] = "this.blur();";
                txtnumero.Attributes["onfocus"] = "this.blur();";
                txtproy.Text = cboproyectos.SelectedValue;
            }
            else
            {
                txtcolonia.Attributes.Remove("onclick");
                txtcalle.Attributes.Remove("onclick");
                txtnumero.Attributes.Remove("onclick");
                txtproy.Text = "0";
                limpiar_datos_consignado();
            }
        }

        public void cargar_datos_colonia_proyecto(int idc_colonia)
        {
            DataSet ds = new DataSet();
           
            DataRow row = default(DataRow);
            try
            {
                AgentesCOM com = new AgentesCOM();
                ds = com.sp_datos_colonia(idc_colonia);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    
                    txtidc_colonia.Text = row["idc_colonia"].ToString().Trim();
                    txtmunicipio.Text = row["mpio"].ToString().Trim();
                    txtestado.Text = row["estado"].ToString().Trim();
                    txtpais.Text = row["pais"].ToString().Trim();
                    chkton.Checked = Convert.ToBoolean(row["capacidad_maxima"]);
                    txttoneladas.Text = row["toneladas"].ToString().Trim();
                    txtCP.Text = row["cod_postal"].ToString().Trim();
                    txtcolonia.Text = row["colonia"].ToString().Trim();
                    lblrestriccion.Text = row["restriccion"].ToString().Trim();
                    chkzm.Checked = Convert.ToBoolean(row["zm"]);
                }
                else
                {
                    Alert.ShowAlertError("La colonia no existe.",this);
                }

            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }


    }
}