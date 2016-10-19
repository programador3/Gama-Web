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
    public partial class busc_Cliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtbuscar.Attributes["onkeypress"] = "return buscar(" + txtbuscar.ClientID + "," + btnbuscar.ClientID + ",event); ";
                txtbuscar.Focus();
                string tipo = (Request.QueryString["tipo"] == null ? "" : Request.QueryString["tipo"]);
                txttipo.Text = tipo;
            }
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
                        gridclientes.DataSource = ds.Tables[0];
                        gridclientes.DataBind();
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

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            buscar_clientes();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtbuscar.Text) & txtbuscar.Text != "Buscar...")
            {
                buscar_clientes();
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
    }
}