using Gios.Pdf;
using Microsoft.Reporting.WebForms;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
   
    public partial class informe_ventas_x_pedidos : System.Web.UI.Page
    {
        public AgentesCOM componente = new AgentesCOM();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)
            {
                Response.Redirect("login.aspx");
            }

            if (!Page.IsPostBack)
            {
                txtdesde.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                txthasta.Text = DateTime.Now.ToString("yyyy-MM-dd");
                btnaceptar.Attributes["onclick"] = "return ver_informe(" + txtdesde.ClientID + "," + txthasta.ClientID + "," + txtidc_cliente.ClientID + "," + chktodos.ClientID + "," + cbosuc.ClientID + "," + chktodas.ClientID + "," + chktmk.ClientID + "," + cbogrupo.ClientID + "," + btna.ClientID + ");";
                sucursales();

                imgcliente.Visible = false;
                cargar_combo_grupos();
                cbogrupo.SelectedValue = "[TODOS]";
            }

        }

        private void sucursales()
        {
            DataSet ds = new DataSet();
            try
            {
                ds = componente.sp_sucursales_combo();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbosuc.DataSource = ds.Tables[0];
                    cbosuc.DataTextField = "nombre";
                    cbosuc.DataValueField = "idc_sucursal";
                    cbosuc.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError("No se ha procedido a cargar sucursales \\n \\u000B \\n" + ex.Message,this);
            }
        }

        public void cargar_combo_grupos()
        {
            DataSet ds = new DataSet();       
            try
            {
                ds = componente.sp_combo_proytmk_gpo_usuarios();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbogrupo.DataSource = ds.Tables[0];
                    cbogrupo.DataTextField = "grupo";
                    cbogrupo.DataValueField = "grupo";
                    cbogrupo.DataBind();
                    cbogrupo.Items.Insert(cbogrupo.Items.Count, "[OTROS]");
                    cbogrupo.Items.Insert(cbogrupo.Items.Count, "[TODOS]");
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError("Error al Intentar Cargar Grupos. \\n  \\u000B \\n" + ex.Message,this);
            }
        }

        protected void btna_Click(object sender, EventArgs e)
        {

            DataSet ds = new DataSet();
            int idc_sucursal = (chktodas.Checked ? 0 : Convert.ToInt32(cbosuc.SelectedValue));
            string idc_cliente = "";
            int idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            string grupo = "";
            int idc_reporting = 0;
            int vagrupado = 0;
            System.DateTime vfechai = default(System.DateTime);
            System.DateTime vfechaf = default(System.DateTime);
            int vtipo = 0;
            try
            {
                if (Convert.ToInt32(option2.SelectedValue) == 1)
                {
                    switch (Convert.ToInt32(option1.SelectedValue))
                    {
                        case 1:
                            idc_reporting = 217;
                            break;
                        case 2:
                            idc_reporting = 218;
                            break;
                        case 3:
                            idc_reporting = 219;
                            break;
                        case 4:
                            idc_reporting = 220;
                            break;
                    }
                }
                else
                {
                    switch (Convert.ToInt32(option1.SelectedValue))
                    {
                        case 1:
                            idc_reporting = 221;
                            break;
                        case 2:
                            idc_reporting = 222;
                            break;
                        case 3:
                            idc_reporting = 223;
                            break;
                        case 4:
                            if (chktmk.Checked == true)
                            {
                                idc_reporting = 225;
                            }
                            else
                            {
                                idc_reporting = 224;
                            }
                            break;
                    }
                }

                vagrupado = Convert.ToInt32(option1.SelectedValue);
                vtipo = Convert.ToInt32(option2.SelectedValue);
                vfechai =Convert.ToDateTime(txtdesde.Text);
                vfechaf = Convert.ToDateTime(txthasta.Text);
                idc_cliente = txtidc_cliente.Text;
                grupo = cbogrupo.SelectedValue;
                string vcliente = (txtidc_cliente.Text == "0" ? "TODOS" : txtcliente.Text.Trim());
                string vsucursal = (idc_sucursal == 0 ? "TODOS" : cbosuc.SelectedItem.Text);


                ReportParameter @params = new ReportParameter();
                @params.Name = "pfechai";
                @params.Values.Add(vfechai.ToString());


                ReportParameter params1 = new ReportParameter();
                params1.Name = "pfechaf";
                params1.Values.Add(vfechaf.ToString());

                ReportParameter params2 = new ReportParameter();
                params2.Name = "pidc_sucursal";
                params2.Values.Add(idc_sucursal.ToString());

                ReportParameter params3 = new ReportParameter();
                params3.Name = "pidc_cliente";
                params3.Values.Add(idc_cliente);

                ReportParameter params4 = new ReportParameter();
                params4.Name = "pidc_usuario";
                params4.Values.Add(idc_usuario.ToString());

                ReportParameter params5 = new ReportParameter();
                params5.Name = "pgrupo";
                params5.Values.Add(grupo);

                ReportParameter params6 = new ReportParameter();
                params6.Name = "pcliente";
                params6.Values.Add(vcliente);

                ReportParameter params7 = new ReportParameter();
                params7.Name = "psucursal";
                params7.Values.Add(vsucursal);

                ReportParameter[] parametros = {
                    @params,
                    params1,
                    params2,
                    params3,
                    params4,
                    params5,
                    params6,
                    params7
                };
                string vcaption = "REPORTE DE VENTAS X PEDIDO";
                string ruta = "reporting.aspx?idc=" + idc_reporting + "&caption=" + vcaption;
                Session["reporte_parametros"] = parametros;
                Response.Redirect(ruta);
                //ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>window.open('" & ruta & "');</script>", False)
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError("Error al Generar Reporte \\n \\u000B \\n" + ex.Message,this);
            }
        }
        protected void gridclientes_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem | e.Item.ItemType == ListItemType.Item)
            {
                ImageButton imgselec = new ImageButton();
                imgselec = e.Item.FindControl("imgselec") as ImageButton;
                imgselec.Attributes["onclick"] = "return regresar(" + e.Item.ItemIndex + 1 + ");";
            }
        }
        protected void chktodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chktodos.Checked == true)
            {
                imgcliente.Visible = false;
            }
            else
            {
                imgcliente.Visible = true;
            }
            txtcliente.Text = "";
            txtidc_cliente.Text = "0";

        }

        protected void option2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(option2.SelectedValue) == 2 & Convert.ToInt32(option1.SelectedValue) == 4)
            {
                chktmk.Enabled = true;
            }
            else
            {
                chktmk.Enabled = false;
            }
        }

        protected void option1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(option1.SelectedValue) == 4 & Convert.ToInt32(option2.SelectedValue) == 2)
            {
                chktmk.Enabled = true;
            }
            else
            {
                chktmk.Enabled = false;
            }

        }

        protected void chktodas_CheckedChanged(object sender, EventArgs e)
        {
            cbosuc.Enabled = false;
            if (chktodas.Checked == false)
            {
                cbosuc.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                cbosuc.ForeColor = System.Drawing.Color.White;
            }
        }

        protected void imgcliente_Click(object sender, ImageClickEventArgs e)
        {
            buscar_cliente.Visible = buscar_cliente.Visible ? false : true;
        }

        protected void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            buscar_clientes();
        }

        public void buscar_clientes()
        {

            DataSet ds = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(txtbuscar.Text.Trim()))
                {
                    AgentesCOM com = new AgentesCOM();
                    ds = com.sp_bclientes_ventas(txtbuscar.Text.Trim());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        drclientes.DataSource = ds.Tables[0];
                        drclientes.DataBind();
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.ToString() + "')</script>");
            }

        }

        protected void drclientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_cliente = Convert.ToInt32(drclientes.DataKeys[index].Values["idc_cliente"].ToString());
            string nombre = drclientes.DataKeys[index].Values["nombre"].ToString();
            txtidc_cliente.Text = idc_cliente.ToString().Trim();
            txtcliente.Text = nombre;
            buscar_cliente.Visible = false;
        }

        protected void btncerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("menu.aspx");
        }
    }
}