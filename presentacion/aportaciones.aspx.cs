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
    public partial class aportaciones : System.Web.UI.Page
    {
        public AgentesCOM componente = new AgentesCOM();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack) {
                try
                {
                    string sbt = Request.QueryString["sbt"];
                    string txtflete = Request.QueryString["ft"];
                    DataSet tx_pedido = new DataSet();
                    DataTable tx_comi = new DataTable();
                    DataTable tx_comifin = new DataTable();
                    DataTable tx_maniobras = new DataTable();
                    DataTable tx_ped = new DataTable();

                    if ((Session["dt_productos_lista"] != null))
                    {
                        tx_pedido.Tables.Add(((DataTable)Session["dt_productos_lista"]).Copy());
                    }

                    tx_comi.Columns.Add("codigo");
                    tx_comi.Columns.Add("desart");
                    tx_comi.Columns.Add("vendedor");
                    tx_comi.Columns.Add("tmk");
                    tx_comi.Columns.Add("margen");
                    tx_comi.Columns.Add("venmastmk");
                    tx_comi.Columns.Add("precio_lista");
                    tx_comi.Columns.Add("apovenlis");
                    tx_comi.Columns.Add("aportacionven");
                    tx_comi.Columns.Add("ven");
                    tx_comi.Columns.Add("venlis");




                    if ((tx_pedido != null))
                    {
                        tx_ped = tx_pedido.Tables[0].DefaultView.ToTable(false, "idc_articulo", "cantidad", "precioreal", "costo", "descripcion", "codigo", "comercial", "precio_lista");

                        decimal vidc_articulo = 0;
                        double vcantidad = 0;
                        decimal VPRECIO = 0;
                        decimal vcosto = 0;
                        string vdesart = string.Empty;
                        string vcodigo = string.Empty;
                        bool vcomercial = false;
                        decimal vventaart = decimal.Zero;
                        decimal vxcosto = decimal.Zero;
                        decimal vmargen = decimal.Zero;
                        decimal vmargentmk = decimal.Zero;
                        decimal vmargenven = decimal.Zero;
                        decimal vpl = decimal.Zero;
                        decimal vprecio_lista = decimal.Zero;
                        decimal vventasub = decimal.Zero;
                        decimal vgastooperativo = 0;
                        decimal vventaartlis = decimal.Zero;
                        decimal vxventa = decimal.Zero;
                        decimal vxventalis = decimal.Zero;
                        //Dim vgastooperativo As Decimal = 0
                        decimal vmargenlis = decimal.Zero;
                        decimal vmargencompartido = decimal.Zero;
                        decimal vmargencompartidolis = decimal.Zero;
                        decimal vaportacionven = decimal.Zero;
                        decimal vaportacionvenlis = decimal.Zero;
                        decimal vmargenvenlis = decimal.Zero;
                        decimal vdistancia = 0;

                        object[] datos_clientes = Session["datos_clientes_pedidos"] as object[];

                        string rfccliente = "";
                        if ((datos_clientes != null))
                        {
                            rfccliente = datos_clientes[1].ToString();
                        }

                        vventasub = Math.Round(Convert.ToDecimal(sbt) / (1 + (Convert.ToDecimal(Session["nuevoiva"]) / 100)), 4);

                        if (rfccliente.StartsWith("*"))
                        {
                            if (!(Session["gastooperativo"] == null))
                            {
                                vgastooperativo = Convert.ToDecimal(Session["gastooperativo"]);
                                vgastooperativo = Math.Round(vgastooperativo / (1 + (Convert.ToDecimal(Session["nuevoiva"]) / 100)), 4);
                            }
                            else
                            {
                                vgastooperativo = 0;
                            }


                        }
                        else
                        {
                            if (!(Session["gastooperativo"] == null))
                            {
                                vgastooperativo = Convert.ToDecimal(Session["gastooperativo"]);
                            }
                            else
                            {
                                vgastooperativo = 0;
                            }

                        }

                        if (!(Session["tipo_de_entrega"].ToString() =="1"))
                        {
                            vgastooperativo = 0;
                            vdistancia = 0;
                        }


                        //11-05-2015
                        if (!(Session["distanciaentrega"] == null))
                        {
                            vdistancia =Convert.ToDecimal(Session["distanciaentrega"]);
                        }
                        else
                        {
                            vdistancia = 1000;
                            //para que salga poca comision
                        }


                        //obtener el porcentaje de aportacion segun la distancia
                        DataTable dt_rpc = default(DataTable);
                        dt_rpc = componente.sp_fn_porcentaje_comision(1, vdistancia).Tables[0];
                        // 1 es vendedor directo


                        decimal vporcecomi = default(decimal);
                        if (dt_rpc.Rows.Count > 0)
                        {
                            vporcecomi = Convert.ToDecimal(dt_rpc.Rows[0]["porcentaje"]);
                        }
                        else
                        {
                            vporcecomi = 0;
                        }
                        // hasta aqui 11-05-2015


                        //dt.Rows(i).Item(5) / (1 + (iva_ant / 100))
                        DataRow row_tx_comi = default(DataRow);
                        if (tx_ped.Rows.Count > 0)
                        {
                            for (int i = 0; i <= tx_ped.Rows.Count - 1; i++)
                            {
                                if (Convert.ToBoolean(tx_ped.Rows[i]["comercial"]) == true)
                                {
                                    vidc_articulo = Convert.ToDecimal(tx_ped.Rows[i]["idc_articulo"]);
                                    vcantidad = Convert.ToDouble(tx_ped.Rows[i]["cantidad"]);
                                    vpl = Convert.ToDecimal(tx_ped.Rows[i]["precio_lista"]);

                                    if (rfccliente.StartsWith("*"))
                                    {
                                        VPRECIO = Convert.ToDecimal(tx_ped.Rows[i]["precioreal"]) / (1 + (Convert.ToDecimal(Session["NuevoIva"]) / 100));
                                        vprecio_lista = vpl / (1 + (Convert.ToDecimal(Session["NuevoIva"]) / 100));
                                    }
                                    else
                                    {
                                        VPRECIO = Convert.ToDecimal(tx_ped.Rows[i]["precioreal"]);
                                        vprecio_lista = vpl;
                                    }

                                    vcosto = Convert.ToDecimal(tx_ped.Rows[i]["costo"]);
                                    vdesart = tx_ped.Rows[i]["descripcion"].ToString();
                                    vcodigo = tx_ped.Rows[i]["codigo"].ToString();
                                    vcomercial = Convert.ToBoolean(tx_ped.Rows[i]["comercial"]);
                                    vventaart = Math.Round(Convert.ToDecimal(vcantidad) * VPRECIO, 2);
                                    vventaartlis = Math.Round(Convert.ToDecimal(vcantidad) * vprecio_lista, 2);
                                    //15-05-2015 para no considerar el costo operativo
                                    //vcosto = Math.Round(vcosto + ((vgastooperativo * (vventaart / vventasub)) / vcantidad), 4)
                                    vcosto = Math.Round(vcosto, 4);
                                    vxventa = vxventa + Math.Round(Convert.ToDecimal(vcantidad) * VPRECIO, 4);

                                    vxventalis = vxventalis + Math.Round(Convert.ToDecimal(vcantidad) * vprecio_lista, 4);

                                    vxcosto = vxcosto + Math.Round(Convert.ToDecimal(vcantidad) * vcosto, 4);

                                    vmargen = Math.Round((1 - (vcosto / VPRECIO)) * 100, 2);

                                    vmargenlis = Math.Round((1 - (vcosto / vprecio_lista)) * 100, 2);

                                    vmargen = (vmargen < 4 ? vmargen : (vmargen < 6 ? 4 : (vmargen < 8 ? 6 : (vmargen < 10 ? 8 : (vmargen < 12 ? 10 : vmargen)))));
                                    vmargenlis = (vmargenlis < 4 ? vmargenlis : (vmargenlis < 6 ? 4 : (vmargenlis < 8 ? 6 : (vmargenlis < 10 ? 8 : (vmargenlis < 12 ? 10 : vmargenlis)))));
                                    vmargencompartido = Math.Round(vmargen * vporcecomi, 4);
                                    vmargencompartidolis = Math.Round(vmargenlis * vporcecomi, 4);



                                    if (vmargen >= 12)
                                    {
                                        vmargenven = Math.Round(vmargencompartido * 1, 4);
                                    }
                                    else if (vmargen >= 10)
                                    {
                                        vmargenven = Math.Round(vmargencompartido * Convert.ToDecimal(0.75), 4);
                                    }
                                    else if (vmargen >= 8)
                                    {
                                        vmargenven = Math.Round(vmargencompartido * Convert.ToDecimal(0.5), 4);
                                    }
                                    else if (vmargen >= 6)
                                    {
                                        vmargenven = Math.Round(vmargencompartido * Convert.ToDecimal(0.25), 4);
                                    }
                                    else if (vmargen < 6)
                                    {
                                        vmargenven = Math.Round(vmargencompartido * Convert.ToDecimal(0.1), 4);
                                    }


                                    if (vmargenlis >= 12)
                                    {
                                        vmargenvenlis = Math.Round(vmargencompartidolis * 1, 4);
                                    }
                                    else if (vmargenlis >= 10)
                                    {
                                        vmargenvenlis = Math.Round(vmargencompartidolis * Convert.ToDecimal(0.75), 4);
                                    }
                                    else if (vmargenlis >= 8)
                                    {
                                        vmargenvenlis = Math.Round(vmargencompartidolis * Convert.ToDecimal(0.5), 4);
                                    }
                                    else if (vmargenlis >= 6)
                                    {
                                        vmargenvenlis = Math.Round(vmargencompartidolis * Convert.ToDecimal(0.25), 4);
                                    }
                                    else
                                    {
                                        vmargenvenlis = Math.Round(vmargencompartidolis * Convert.ToDecimal(0.1), 4);
                                    }

                                    vaportacionven = Math.Round(vventaart * vmargenven / 100, 2);
                                    vaportacionvenlis = Math.Round(vventaartlis * vmargenvenlis / 100, 2);

                                    //INSERT INTO tx_comi (codigo,desart,vendedor,tmk,margen,venmastmk,aportaciontmk,apotmklis,precio_lista) VALUES 
                                    //                    (vcodigo,vdesart,vmargenven ,vmargentmk,vmargen,vmargenven +vmargentmk,vaportaciontmk,vaportaciontmklis,vpl)

                                    row_tx_comi = tx_comi.NewRow();
                                    row_tx_comi["codigo"] = vcodigo;
                                    row_tx_comi["desart"] = vdesart;
                                    row_tx_comi["vendedor"] = vmargenven;
                                    row_tx_comi["tmk"] = vmargentmk;
                                    row_tx_comi["margen"] = vmargen;
                                    row_tx_comi["venmastmk"] = vmargentmk + vmargenven;
                                    row_tx_comi["aportacionven"] = vaportacionven;
                                    row_tx_comi["apovenlis"] = vaportacionvenlis;
                                    row_tx_comi["precio_lista"] = vpl;
                                    row_tx_comi["venlis"] = vmargenvenlis;
                                    row_tx_comi["ven"] = vmargenven;
                                    tx_comi.Rows.Add(row_tx_comi);
                                }
                                else
                                {
                                    tx_ped.Rows.RemoveAt(i);
                                }
                                if (tx_ped.Rows[i]["idc_articulo"].ToString() == 4454.ToString())
                                {
                                    tx_maniobras.ImportRow(tx_ped.Rows[i]);
                                }
                            }

                            decimal aportacionven = decimal.Zero;
                            decimal apovenlis = decimal.Zero;
                            if (tx_comi.Rows.Count > 0)
                            {
                                for (int i = 0; i <= tx_comi.Rows.Count - 1; i++)
                                {
                                    aportacionven = aportacionven + Convert.ToDecimal(tx_comi.Rows[i]["aportacionven"]);
                                    apovenlis = apovenlis + Convert.ToDecimal(tx_comi.Rows[i]["apovenlis"]);
                                }
                                txtapo.Text = aportacionven.ToString("#,###.####");
                                txtapolis.Text = apovenlis.ToString("#,###.####"); 
                            }



                            gridprod.DataSource = tx_comi;
                            gridprod.DataBind();
                            gridprod.UseAccessibleHeader = true;
                            ViewState["aportaciones"] = tx_comi;
                        }

                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    CargarMsgBox("Error:\\n \\u000B \\n" + ex.Message);
                    ejecuta_JS("window.close();");
                }
            }
        }

        public void CargarMsgBox(string msj)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), "<script>alert('" + msj.Replace("'", "") + "');</script>", false);
        }

        public void ejecuta_JS(string script)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), "<script>" + script + "</script>", false);
        }

        protected void Page_LoadComplete(object sender, System.EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), "<script>divs();</script>", false);
        }

        public void direction(string exp)
        {
            if (ViewState["exp"] as string == exp)
            {
                if (ViewState["direction"] as string == "desc")
                {
                    ViewState["direction"] = "asc";
                }
                else
                {
                    ViewState["direction"] = "desc";
                }
            }
            else if (ViewState["exp"] == null)
            {
                ViewState["direction"] = "asc";
                ViewState["exp"] = exp;
            }
            else
            {
                ViewState["direction"] = "asc";
                ViewState["exp"] = exp;
            }
        }

    }
}