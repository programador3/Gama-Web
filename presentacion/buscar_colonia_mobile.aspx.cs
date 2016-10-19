using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class buscar_colonia_mobile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtvalor.Attributes["onkeydown"] = "return buscar_colonia(event);";
                btnaceptar.Attributes["onclick"] = "return regresa_valores();";
                txttipo.Text = Request.QueryString["tipo"];
            }
        }
        public void controles_buscar_col(bool estado)
        {
            imgaceptar.Visible = estado;
            imgcancelar.Visible = estado;
            cbocolonias.Visible = estado;
        }
        public void controles_buscar_col_val(bool estado)
        {
            txtvalor.Visible = estado;
        }

        protected void imgaceptar_Click(object sender, ImageClickEventArgs e)
        {
            controles_buscar_col(false);
            controles_buscar_col_val(true);
            txtvalor.Text = "";
            txtvalor.Attributes["onfocus"] = "this.blur();";
            DataTable dt = new DataTable();
            DataRow[] rows = null;
            try
            {
                dt = ViewState["dt_colonias"] as DataTable;
                if (dt.Rows.Count > 0)
                {
                    rows = dt.Select("idc_colonia=" + cbocolonias.SelectedValue);
                    if (rows.Length > 0)
                    {
                        txtidc_colonia.Text = rows[0]["idc_colonia"].ToString();
                        txtcolonia.Text = rows[0]["nombre"].ToString();
                        txtcp.Text = rows[0]["cod_postal"].ToString();
                        txtmunicipio.Text = rows[0]["mpio"].ToString();
                        txtestado.Text = rows[0]["edo"].ToString();
                        txtpais.Text = rows[0]["pais"].ToString();
                        txtrestriccion.Text = rows[0]["restriccion"].ToString();
                        txtcapacidad.Text = rows[0]["capacidad_maxima"].ToString();
                        txttoneladas.Text = rows[0]["toneladas"].ToString();
                        chkzm.Checked = Convert.ToBoolean(rows[0]["zm"]);
                    }
                    else
                    {
                        Response.Write("<script>alert('Error al Intentar Cargar Informacion de Colonia Seleccionada.')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Error al Intentar Cargar Informacion de Colonia Seleccionada.')</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.ToString() + "')</script>");
            }

        }

        protected void imgcancelar_Click(object sender, ImageClickEventArgs e)
        {
            controles_buscar_col(false);
            controles_buscar_col_val(true);
        }

        protected void txtvalor_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtvalor.Text))
            {
                try
                {
                    AgentesCOM com = new AgentesCOM();
                    DataSet ds = com.sp_bcolonias(txtvalor.Text.Trim());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ds.Tables[0].Columns.Add("nombre2");
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            ds.Tables[0].Rows[i]["nombre2"] = ds.Tables[0].Rows[i]["nombre"].ToString() + " || "
                                + ds.Tables[0].Rows[i]["mpio"].ToString() + " || "
                                + ds.Tables[0].Rows[i]["edo"].ToString();
                        }
                        cbocolonias.DataSource = ds.Tables[0];
                        cbocolonias.DataTextField = "nombre2";
                        cbocolonias.DataValueField = "idc_colonia";
                        cbocolonias.DataBind();
                        controles_buscar_col(true);
                        controles_buscar_col_val(false);
                        ViewState["dt_colonias"] = ds.Tables[0];
                    }
                    else
                    {
                        Response.Write("<script>alert('No existe colonia con esa decsripcion')</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.ToString() + "')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Introduce una condición de busqueda...')</script>");
                return;
            }
        }
    }
}