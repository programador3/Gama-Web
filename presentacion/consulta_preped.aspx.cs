using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
	public partial class consulta_preped1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!Page.IsPostBack)
            {
                int idc_preped =Convert.ToInt32(Request.QueryString["idc_preped"]);
                if (!(idc_preped == null) | idc_preped <= 0)
                {
                    cargar_datos(idc_preped);
                }
            }
        }

        public void cargar_datos(int idc_prepedido)
        {
            DataSet ds = new DataSet();
            AgentesCOM gweb = new AgentesCOM();
            try
            {
                ds = gweb.sp_ver_PREped(idc_prepedido);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    int vidc_pedido = (ds.Tables[0].Rows[0]["idc_pedido"] != System.DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["idc_pedido"]) : 0);
                    string vfecha_ped = (ds.Tables[0].Rows[0]["fecha_ped"] != System.DBNull.Value ? ds.Tables[0].Rows[0]["fecha_ped"].ToString() : "");
                    string vmotivo_can = (ds.Tables[0].Rows[0]["motivo_can"] != System.DBNull.Value ? ds.Tables[0].Rows[0]["motivo_can"].ToString() : "");
                    txtprepedido.Text = ds.Tables[0].Rows[0]["idc_preped"].ToString();
                    txtnombre.Text = ds.Tables[0].Rows[0]["nombre"].ToString();
                    txtrfc.Text = ds.Tables[0].Rows[0]["rfccliente"].ToString();
                    txtcve.Text = ds.Tables[0].Rows[0]["cveadi"].ToString();
                    txtidc_cli.Text = ds.Tables[0].Rows[0]["idc_cliente"].ToString();
                    txtagente.Text = ds.Tables[0].Rows[0]["idc_agente"].ToString();
                    txtagenten.Text = ds.Tables[0].Rows[0]["agente"].ToString();
                    txtfeha2.Text = ds.Tables[0].Rows[0]["fecha"].ToString();
                    txtfechae.Text = ds.Tables[0].Rows[0]["fecha_entrega"].ToString();
                    txtusu.Text = ds.Tables[0].Rows[0]["usuario"].ToString();
                    txtsuci.Text = ds.Tables[0].Rows[0]["idc_sucursal"].ToString();
                    txtsucn.Text = ds.Tables[0].Rows[0]["sucursal"].ToString();

                    txtmonto.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["monto"]).ToString("#,###.##");
                    txtfecha.Text = (ds.Tables[0].Rows[0]["fecha_ct"].ToString() == "" ? ds.Tables[0].Rows[0]["fecha_ct"].ToString() : ""); 
                    chkoc.Visible =Convert.ToBoolean(ds.Tables[0].Rows[0]["occ"]);
                    chkcroquis.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["croquis"]);
                    txtobservaciones.Text = ds.Tables[0].Rows[0]["observaciones_ct"].ToString();
                    txtfechapanel.Text =ds.Tables[0].Rows[0]["fecha_ct"].ToString(); 
                    txtusuariopanel.Text = ds.Tables[0].Rows[0]["usuario_ct"].ToString(); 

                    string sta_creditos = ds.Tables[0].Rows[0]["sta_creditos"].ToString(); 
                    switch (sta_creditos)
                    {
                        case "E":
                            rde.Checked = true;
                            Panel2.Visible = false;
                            break;
                        case "A":
                            rda.Checked = true;
                            txtpedido.Text = vidc_pedido.ToString();
                            txtfecha.Text = vfecha_ped;
                            txtusuario.Text = ds.Tables[0].Rows[0]["usuario_aplico"].ToString(); 
                            txtobs.Text = ds.Tables[0].Rows[0]["obsaut"].ToString(); 
                            Panel2.Visible = true;
                            Panel3.Visible = false;
                            break;
                        case "N":
                            rdno.Checked = true;
                            Panel2.Visible = false;
                            txtmotivo.Text = vmotivo_can;
                            txtusuario.Text = ds.Tables[0].Rows[0]["usuario_aplico"].ToString(); 
                            Panel3.Visible = true;
                            break;
                    }
                    string[] ext = {
                        ".jpg",
                        ".dib",
                        ".bmp",
                        ".gif"
                    };
                    string ruta = "";
                    if (ds.Tables[0].Rows[0]["no_occ"].ToString() != "")
                    {
                        ruta = funciones.GenerarRuta("pPED_OC","UNIDAD");
                        if (!string.IsNullOrEmpty(ruta))
                        {
                            for (int i = 0; i <= ext.Length - 1; i++)
                            {
                                if (File.Exists(ruta + txtprepedido.Text + ext[i]))
                                {
                                    if (File.Exists(Server.MapPath("temp\\files\\" + txtprepedido.Text + ext[i])))
                                    {
                                        File.Delete(Server.MapPath("temp\\files\\" + txtprepedido.Text + ext[i]));
                                    }
                                    File.Copy(ruta + txtprepedido.Text + ext[i], Server.MapPath("temp\\files\\" + txtprepedido.Text + ext[i]));
                                    chkoc.Attributes["onclick"] = "window.open('temp/files/" + txtprepedido.Text + ext[i] + "');return false;";
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                        }
                        else
                        {
                            chkoc.Attributes["onclick"] = "alert('No se Cargo la Ruta.');return false;";
                        }
                    }

                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["croquis"]))
                    {
                        ruta = funciones.GenerarRuta("pPED_CRO","UNIDAD");
                        if (!string.IsNullOrEmpty(ruta))
                        {
                            for (int i = 0; i <= ext.Length - 1; i++)
                            {
                                if (File.Exists(ruta + txtprepedido.Text + ext[i]))
                                {
                                    if (File.Exists(Server.MapPath("temp\\lugares_captura\\" + txtprepedido.Text + ext[i])))
                                    {
                                        File.Delete(Server.MapPath("temp\\lugares_captura\\" + txtprepedido.Text + ext[i]));
                                    }
                                    File.Copy(ruta + txtprepedido.Text + ext[i], Server.MapPath("temp\\lugares_captura\\" + txtprepedido.Text + ext[i]));
                                    chkcroquis.Attributes["onclick"] = "window.open('temp/lugares_captura/" + txtprepedido.Text + ext[i] + "');return false;";
                                    break; // TODO: might not be correct. Was : Exit For
                                }

                            }
                        }
                        else
                        {
                            chkcroquis.Attributes["onclick"] = "alert('No se Cargo la Ruta.');return false;";
                        }
                    }








                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        //Columna de cantidad
                        DataColumn cantidad = new DataColumn();
                        cantidad.DataType = typeof(decimal);
                        cantidad.ColumnName = "Importe";
                        cantidad.Expression = "precio * cantidad";
                        cantidad.ReadOnly = true;
                        cantidad.AllowDBNull = true;
                        ds.Tables[1].Columns.Add(cantidad);


                        //Columna de Precio Real
                        DataColumn precior = new DataColumn();
                        precior.DataType = typeof(decimal);
                        precior.ColumnName = "precioreal";
                        precior.Expression = "precio - descuento";
                        precior.ReadOnly = true;
                        precior.AllowDBNull = true;
                        ds.Tables[1].Columns.Add(precior);
                        gridprod.DataSource = ds.Tables[1];
                        gridprod.DataBind();
                        calcular_iva(ds.Tables[1],Convert.ToDecimal(ds.Tables[0].Rows[0]["iva"]));
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError("Error al Cargar Información. \\n-\\n" + ex.Message,this);
            }

        }

        public void calcular_iva(DataTable dt, decimal porciento)
        {
            decimal iva = 0;
            decimal total = 0;
            decimal nota = 0;
            decimal nota_iva = 0;
            decimal nota_tot = 0;
            lbliva.Text = "I.V.A.(" + Convert.ToString(Convert.ToInt32(porciento)) + "%)";
            porciento = porciento / 100;
            if (dt.Rows.Count > 0)
            {
                if (!txtrfc.Text.StartsWith("*"))
                {
                    for (int I = 0; I <= dt.Rows.Count - 1; I++)
                    {
                        if (!(dt.Rows[I]["descuento"].ToString() == "0"))
                        {
                            nota = nota + (Convert.ToDecimal(dt.Rows[I]["descuento"]) * Convert.ToDecimal(dt.Rows[I]["cantidad"]));
                        }
                        total = total + Convert.ToDecimal(dt.Rows[I]["importe"]);
                    }
                    nota_iva = nota * porciento;
                    nota_tot = nota_iva + nota;

                    txtsub1.Text = FormatNumber(total, 2);
                    txtnota1.Text = FormatNumber(nota, 2);
                    txtnota1.Text = txtnota1.Text == "" ? 0.ToString() : txtnota1.Text;
                    txtsub1.Text = txtsub1.Text == "" ? 0.ToString() : txtsub1.Text;
                    txtsub2.Text = FormatNumber(Convert.ToDecimal(txtsub1.Text.Replace(",","")) - Convert.ToDecimal(txtnota1.Text.Replace(",", "")), 2);

                    txtiva1.Text = FormatNumber(total * porciento, 2);
                    txtnota2.Text = FormatNumber(nota * porciento, 2);

                    txtnota2.Text = txtnota2.Text == "" ? 0.ToString() : txtnota2.Text;
                    txtiva1.Text = txtnota2.Text == "" ? 0.ToString() : txtiva1.Text;
                    txtiva2.Text = FormatNumber(Convert.ToDecimal(txtiva1.Text.Replace(",", "")) - Convert.ToDecimal(txtnota2.Text.Replace(",", "")), 2);

                    txttotal1.Text = FormatNumber(total + Convert.ToDecimal(txtiva1.Text), 2);
                    txtnota3.Text = FormatNumber(nota + Convert.ToDecimal(txtnota2.Text), 2);

                    txttotal1.Text = txttotal1.Text == "" ? 0.ToString() : txttotal1.Text;
                    txtnota3.Text = txtnota3.Text == "" ? 0.ToString() : txtnota3.Text;
                    txttotal2.Text = FormatNumber(Convert.ToDecimal(txttotal1.Text.Replace(",", "")) - Convert.ToDecimal(txtnota3.Text.Replace(",", "")), 2);
                    lbliva.Text = "I.V.A.";
                }
                else
                {
                    for (int I = 0; I <= dt.Rows.Count - 1; I++)
                    {
                        if (!(dt.Rows[I]["descuento"].ToString() == "0"))
                        {
                            nota = nota + (Convert.ToDecimal(dt.Rows[I]["descuento"]) * Convert.ToDecimal(dt.Rows[I]["cantidad"]));
                        }
                        total = total + Convert.ToDecimal(dt.Rows[I]["importe"]);
                    }
                    txtsub1.Text = FormatNumber(total, 2);
                    txtnota1.Text = FormatNumber(nota, 2);
                    txtsub2.Text = FormatNumber(total, 2);
                    txtiva1.Text = FormatNumber(Convert.ToDecimal("0.00"), 2);
                    txtnota2.Text = FormatNumber(Convert.ToDecimal("0.00"), 2);
                    txtiva2.Text = FormatNumber(Convert.ToDecimal("0.00"), 2);
                    txttotal1.Text = FormatNumber(Convert.ToDecimal(txtsub1.Text.Trim().Replace(",", "")) + Convert.ToDecimal(txtiva1.Text.Trim().Replace(",", "")), 2);
                    txtnota3.Text = FormatNumber(Convert.ToDecimal(txtnota2.Text.Trim().Replace(",", "")) + Convert.ToDecimal(txtnota1.Text.Trim().Replace(",", "")), 2);
                    txtiva2.Text = FormatNumber(Convert.ToDecimal(txtiva1.Text.Trim().Replace(",", "")) + Convert.ToDecimal(txtnota2.Text.Trim().Replace(",", "")), 2);
                    txttotal2.Text = FormatNumber(total - Convert.ToDecimal(txtnota3.Text.Replace(",", "")), 2);
                }


            }
        }
        String FormatNumber(decimal valor, int decimales)
        {
           return valor.ToString("#,###.##");
        }
    }
}