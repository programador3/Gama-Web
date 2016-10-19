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
    public partial class Pedidos7 : System.Web.UI.Page
    {
        public AgentesCOM componente = new AgentesCOM();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            btnconsignado.Attributes["onclick"] = "return popup_consignado();";
            HttpContext.Current.Response.AddHeader("P3P", "CP=\"CAO PSA OUR\"");
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                ViewState["dt_clientes"] = null;
                ViewState["dt_productos"] = null;
                ViewState["tx_pedido_gratis"] = null;
                this.txtcantidad.Attributes.Add("onkeydown", "return cambiaFoco(event);");
                txtbuscar.Attributes["onkeydown"] = "return buscar_cliente(event);";
                txtcodigoarticulo.Attributes["onkeydown"] = "return buscarart(event);";
                txtFolio.Attributes["onChange"] = "cambiotexto();";
                if (!Page.IsPostBack)
                {
                    Session["dt_productos_lista"] = null;
                    imgupcroquis.Attributes["onClick"] = "return up_files(1);";
                    imgupllamada.Attributes["onClick"] = "return up_files(2);";

                    //RadioButtons
                    estado_rd(true);

                    //Cambio de Precios Sucursales
                    Session["pxidc_sucursal"] = Session["idc_sucursal"];
                    ivasucursal();
                    fecha_txt();
                    cedis();

                    //Para Cambios de IVA/Frontera
                    Session["ivaant"] = Session["Xiva"];
                    Session["idc_ivaant"] = Session["Xidc_iva"];
                    Session["NuevoIva"] = Session["Xiva"];
                    Session["nuevo_idc_iva"] = Session["Xidc_iva"];
                    //*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*

                    txtcreditodisponible.Text = "0.00";
                    int tipo = 0;
                    tipo = Convert.ToInt32(Request.QueryString["tipo"]);
                    cargar_consecutivo_folio();
                    txtprecio.Attributes["onkeydown"] = "return agregararticulo(event)";
                    ///TabContainer1.ActiveTabIndex = 0
                    if (tipo == 1)
                    {
                        txtid.Text = funciones.de64aTexto(Request.QueryString["idc_cliente"]);
                        ViewState["dt"] = Session["dt_pedido_lista"];
                        Estado_controles_captura(false);
                        buscar_confirmacion_lista();
                        tbnguardarPP.Enabled = true;
                        btnnuevoprepedido.Enabled = true;
                        etiqueta_cheque();
                        estado_rd(true);
                        agregar_columnas_tabla_promociones();
                    }
                    else
                    {
                        agregar_columnas_dataset();
                        agregar_columnas_tabla_promociones();
                        txtcodigoarticulo.Enabled = false;
                        etiqueta_cheque();
                        txtbuscar.Focus();
                        botones_pedido();
                    }
                    if (Request.QueryString["idc_cliente"] != null)
                    {
                        DataSet ds = new DataSet();
                        DataRow row = default(DataRow);
                        AgentesENT entidad = new AgentesENT();
                        entidad.Pidc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
                        ds = componente.sp_datos_cliente(entidad);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            row = ds.Tables[0].Rows[0];
                            txtid.Text = funciones.de64aTexto(Request.QueryString["idc_cliente"]);
                            txtrfc.Text = row["rfccliente"].ToString();
                            txtnombre.Text = row["nombre"].ToString();
                            Session["Clave_Adi"] = row["cveadi"].ToString();
                            txtstatus.Text = row["idc_bloqueoc"].ToString().Trim();
                            colores(txtstatus.Text);
                            txtbuscar.Enabled = false;
                            lblconfirmacion.Visible = Confirmacion_de_Pago();
                            btnOC.Attributes.Add("onclick", "window.open('OC_Digitales_Pendientes.aspx?idc_cliente=" + funciones.deTextoa64(txtid.Text.Trim()) + "');");
                            btnOC.Enabled = true;
                            lkverdatoscliente.NavigateUrl = "window.open('Ficha_cliente_m.aspx?idc_cliente=" + txtid.Text.Trim() + "&b=1');";
                            lkverdatoscliente.Enabled = true;
                            txtcodigoarticulo.Focus();
                            etiqueta_Iva(Session["NuevoIva"] as string);
                            requiere_oc_croquis();
                            tipo_croquis_cliente();
                        }

                        //Para la Lista de Precios cliente
                        lista_p(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"])));
                        controles_busqueda_cliente_cancel_selecc(false);
                        controles_busqueda_cliente(false);
                        lblbuscar_cliente.Visible = false;
                        Div1.Visible = false;
                    }
                    else
                    {
                        txtbuscar.Enabled = true;
                        txtbuscar.Focus();
                    }
                    cboentrega.Attributes["onchange"] = "return tipo_entrega(this);";
                    tbnguardarPP.Attributes["onclick"] = "return confirmacion_pago();";
                }
                lblrestriccion.Text = txtrestriccion.Text.Trim();
            }
        }

        public void lista_p(int idc_cliente)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = componente.sp_listap_cliente(idc_cliente).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtlistap.Text = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error. \\n \\u000B \\n" + ex.Message); throw;
            }
        }

        public void tipo_croquis_cliente()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtid.Text.Trim()))
                {
                    string tipoC = "";
                    DataSet ds = componente.sp_fn_cliente_tipo_croquis(Convert.ToInt32(txtid.Text.Trim()));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        tipoC = ds.Tables[0].Rows[0][0].ToString();
                        if (tipoC.Trim() == "P")
                        {
                            fucroquis.Visible = false;
                        }
                        else
                        {
                            fucroquis.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error. \\n \\u000B \\n" + ex.Message); throw;
            }
        }

        public void requiere_oc_croquis()
        {
            //Para revisar si es necesario cargar OC y Croquis....
            DataRow row = default(DataRow);
            try
            {
                DataSet ds = componente.sp_req_oc_croquis(Convert.ToInt32(txtid.Text.Trim()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    oc.Text = row[0].ToString();
                    croquis.Text = row[1].ToString();
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error. \\n \\u000B \\n" + ex.Message);
            }
        }

        public void controles_busqueda_cliente_cancel_selecc(bool estado)
        {
            btncan_bus.Visible = estado;
            btnacep_bus.Visible = estado;
            cboclientes.Visible = estado;
        }

        public void controles_busqueda_cliente(bool estado)
        {
            txtbuscar.Visible = estado;
            btnbuscarcliente.Visible = estado;
        }

        public void etiqueta_Iva(string iva)
        {
            if (txtrfc.Text.StartsWith("*"))
            {
                lbliva.Text = "I.V.A.";
            }
            else
            {
                lbliva.Text = "I.V.A.(" + Convert.ToInt32(iva).ToString() + "%)";
            }
        }

        public void colores(string valor)
        {
            int value = valor == "" ? 0 : Convert.ToInt16(valor);
            switch (value)
            {
                case 0:
                    //Verde
                    txtstatus.BackColor = System.Drawing.Color.Green;
                    //txtstatus.Attributes["style"] = "background-color:#009245;border-color:#009245;"
                    txtstatus.ForeColor = System.Drawing.Color.White;
                    //txtnombre.Attributes["style"] = "background-color:#009245;border-color:#009245;"
                    txtnombre.BackColor = System.Drawing.Color.Green;
                    txtnombre.ForeColor = System.Drawing.Color.White;
                    //imgstatus.ImageUrl = "~/Iconos/sverde.png"
                    //imgstatus.Visible = True
                    txtcodigoarticulo.Attributes.Remove("onfocus");
                    txtbuscar.Enabled = false;
                    break;

                case 4:
                    //Amarillo
                    txtstatus.BackColor = System.Drawing.Color.Yellow;
                    //txtstatus.Attributes["style"] = "background-color:#fcee21;border-color:#fcee21;"
                    txtstatus.ForeColor = System.Drawing.Color.Black;
                    //txtnombre.Attributes["style"] = "background-color:#fcee21;border-color:#fcee21;"
                    txtnombre.BackColor = System.Drawing.Color.Yellow;
                    txtnombre.ForeColor = System.Drawing.Color.Black;
                    txtcodigoarticulo.Attributes["onfocus"] = "this.blur();";
                    txtbuscar.Enabled = true;
                    //ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "<script>alert('" + ex.Message.Replace("'", "") + "');</script>", false);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>alert('El Cliente Esta Bloqueado por Cheques Devueltos'); </script>", false);
                    //El Cliente Esta Bloqueado por Cheques Devueltos...
                    txtbuscar.Focus();
                    txtbuscar.Enabled = true;
                    break;

                case 1:
                case 2:
                case 3:
                    //Rojo
                    txtstatus.BackColor = System.Drawing.Color.Red;
                    //txtstatus.Attributes["style"] = "background-color:#c1272d;border-color:#c1272d;"
                    txtstatus.ForeColor = System.Drawing.Color.White;
                    //txtnombre.Attributes["style"] = "background-color:#c1272d;border-color:#c1272d;"
                    txtnombre.BackColor = System.Drawing.Color.Red;
                    txtnombre.ForeColor = System.Drawing.Color.White;
                    //imgstatus.ImageUrl = "~/Iconos/srojo.png"
                    //imgstatus.Visible = True
                    txtcodigoarticulo.Attributes.Remove("onfocus");
                    txtbuscar.Enabled = false;
                    break;
            }
        }

        public void botones_pedido()
        {
            btnescucharll.Attributes["onClick"] = "reproducir_llamada();";
            txtobservaciones.Attributes.Add("onkeypress", " validarmaxlength(this, 100);");
            txtobservaciones.Attributes.Add("onkeyup", " validarmaxlength(this, 100);");
            btnseleccionarOC.Attributes["onClick"] = "AbreHija();";
            // Para llamar a la pantalla de OC Pendientes del Cliente.
            btnverOc.Attributes["onClick"] = "return AbreIMGOC();";
            // Para ver la Orden de Compra seleccionada.
            fucroquis.Enabled = true;
            fullamada.Enabled = true;
            txtfolioCHP.Attributes["onblur"] = "return validar_chekplus();";
            controlesG(false);
        }

        public void controlesG(bool estado)
        {
            txtcodigoarticulo.Enabled = estado;
        }

        public void agregar_columnas_dataset()
        {
            int contador = 0;
            if (contador == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_articulo");
                //0
                dt.Columns.Add("Codigo");
                //1
                dt.Columns.Add("Descripcion");
                //2
                dt.Columns.Add("UM");
                //3
                dt.Columns.Add("Cantidad");
                //4
                dt.Columns.Add("Precio");
                //5
                dt.Columns.Add("Importe");
                //6
                dt.Columns.Add("PrecioReal");
                //7
                dt.Columns.Add("Descuento");
                //8
                dt.Columns.Add("Decimales");
                //9
                dt.Columns.Add("Paquete");
                //10
                dt.Columns.Add("precio_libre");
                //11
                dt.Columns.Add("comercial");
                //12
                dt.Columns.Add("fecha");
                //13
                dt.Columns.Add("obscotiza");
                //14
                dt.Columns.Add("vende_exis");
                //15
                dt.Columns.Add("minimo_venta");
                //16
                dt.Columns.Add("Master");
                //17
                dt.Columns.Add("mensaje");
                //18
                dt.Columns.Add("Calculado");
                //19
                dt.Columns.Add("Porcentaje");
                //20
                dt.Columns.Add("Nota_Credito");
                //21
                dt.Columns.Add("Anticipo");
                //22
                dt.Columns.Add("Costo");
                //23
                dt.Columns.Add("Existencia");
                //24
                dt.Columns.Add("idc_promocion");
                //25
                dt.Columns.Add("precio_lista");
                //26
                dt.Columns.Add("precio_minimo");
                //27
                dt.Columns.Add("tiene_especif");
                //28
                dt.Columns.Add("especif");
                //29
                dt.Columns.Add("num_especif");
                //30
                dt.Columns.Add("ids_especif");
                //31
                dt.Columns.Add("g_especif");
                //32
                dt.Columns.Add("costo_o");
                dt.Columns.Add("precio_o");
                dt.Columns.Add("precio_lista_o");
                dt.Columns.Add("precio_minimo_o");
                contador = 1;
                ViewState["dt"] = dt;
            }
        }

        public void agregar_columnas_tabla_promociones()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("idc_articulo");
            dt.Columns.Add("cantidad");
            dt.Columns.Add("codigo");
            dt.Columns.Add("unimed");
            dt.Columns.Add("desart");
            dt.Columns.Add("idc_promociond");
            dt.Columns.Add("idc_promocion");
            Session["tx_pedido_gratis"] = dt;
        }

        public void etiqueta_cheque()
        {
            bool visible = false;
            try
            {
                DataSet ds = componente.SP_Cargar_ChekPlus();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["cargo_cheque"]);
                    lblcheckplus.Visible = visible;
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.Message);
            }
        }

        public void buscar_confirmacion_lista()
        {
            DataTable dt = new DataTable();
            dt = ViewState["dt"] as DataTable;
            int count = 0;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (dt.Rows[i][22].ToString() != "")
                {
                    count = count + 1;
                }
            }
            if (count > 0 | Confirmacion_de_Pago() == true)
            {
                lblconfirmacion.Visible = true;
                btnconfirmar.Visible = lblconfirmacion.Visible;
            }
            else if (count <= 0 & Confirmacion_de_Pago() == false)
            {
                lblconfirmacion.Visible = false;
                btnconfirmar.Visible = lblconfirmacion.Visible;
            }
        }

        public bool Confirmacion_de_Pago()
        {
            DataSet ds = new DataSet();
            DataRow row = default(DataRow);
            try
            {
                ds = componente.sp_cliente_confirmacion_pago(Convert.ToInt32(txtid.Text.Trim()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    return Convert.ToBoolean(row["confirmacion"]);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error al Cargar ID Cedis. \\n \\u000B \\n" + ex.Message);
                return false;
            }
        }

        public void aportaciones()
        {
            try
            {
                string sbt = txttotal.Text;
                string txtflete = txtmaniobras.Text;
                DataSet tx_pedido = new DataSet();
                DataTable tx_comi = new DataTable();
                DataTable tx_comiv = new DataTable();
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
                    //15-05-2015
                    decimal vdistancia = 0;

                    object[] datos_clientes = Session["datos_clientes_pedidos"] as object[];

                    string rfccliente = "";
                    if ((datos_clientes != null))
                    {
                        rfccliente = datos_clientes[1].ToString();
                    }

                    vventasub = Math.Round(Convert.ToDecimal(sbt) / (1 + (Convert.ToDecimal(Session["nuevoiva"]) / 100)), 2);

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

                    if (!(Convert.ToInt32(Session["tipo_de_entrega"]) == 1))
                    {
                        vgastooperativo = 0;
                    }

                    //11-05-2015
                    if (!(Session["distanciaentrega"] == null))
                    {
                        vdistancia = Convert.ToDecimal(Session["distanciaentrega"]);
                    }
                    else
                    {
                        vdistancia = 1000;
                        //para que salga poca comision
                    }

                    //obtener el porcentaje de aportacion segun la distancia
                    DataTable dt_rpc = default(DataTable);
                    AgentesCOM com = new AgentesCOM();
                    dt_rpc = com.sp_fn_porcentaje_comision(1, vdistancia).Tables[0];
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
                    //hasta aqui 11-05-2015

                    //dt.Rows[i][5) / (1 + (iva_ant / 100))
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

                                //11-05-2015 quite linea siguiente para que no sume el costo operativo
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
                                aportacionven = Math.Round(aportacionven + Convert.ToDecimal(tx_comi.Rows[i]["aportacionven"]), 4);
                                apovenlis = Math.Round(apovenlis + Convert.ToDecimal(tx_comi.Rows[i]["apovenlis"]), 4);
                            }
                            txtaportacion.Text = aportacionven.ToString("N4");
                            txtaportacionl.Text = apovenlis.ToString("N4");
                        }
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error al Calcular Aportaciones.\\n \\u000B \\nError:\\n" + ex.Message);
            }
        }

        private void WriteMsgBox(string msg)
        {
            Alert.ShowAlertInfo(msg, "Mensaje del Sistema", this);
        }

        public void estado_rd(bool estado)
        {
            if (estado == true)
            {
                cboentrega.Enabled = true;
            }
            else
            {
                cboentrega.Enabled = false;
            }
        }

        public void ivasucursal()
        {
            DataSet ds = new DataSet();
            try
            {
                AgentesCOM com = new AgentesCOM();
                ds = com.sp_datosticketsuc(Convert.ToInt32(Session["idc_sucursal"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["lidc_iva"] = Convert.ToInt32(ds.Tables[0].Rows[0]["idc_iva"]);
                    Session["liva"] = Convert.ToDecimal(ds.Tables[0].Rows[0]["iva"]);

                    Session["pidc_iva"] = Convert.ToInt32(ds.Tables[0].Rows[0]["idc_iva"]);
                    Session["piva"] = Convert.ToDecimal(ds.Tables[0].Rows[0]["iva"]);
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
            }
        }

        public void fecha_txt()
        {
            DataTable dt_fechas_vendedores = default(DataTable);

            try
            {
                AgentesCOM com = new AgentesCOM();
                dt_fechas_vendedores = com.sp_fn_maximo_programar_vendedor().Tables[0];
                if (dt_fechas_vendedores.Rows.Count > 0)
                {
                    cbofechas.DataSource = dt_fechas_vendedores;
                    cbofechas.DataTextField = "fechamostrar";
                    cbofechas.DataValueField = "fecha";
                    cbofechas.DataBind();
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error al Cargar Fechas. \\n \\u000B \\n Error: \\n" + ex.Message);
            }
        }

        public int cedis()
        {
            int vidc_cedis = 0;
            try
            {
                AgentesCOM com = new AgentesCOM();
                DataTable dt = com.sp_fn_cedis_sucursal(Convert.ToInt32(Session["idc_sucursal"])).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    vidc_cedis = Convert.ToInt32(dt.Rows[0]["pxid"]);
                    Session["pidc_cedis"] = vidc_cedis;
                    Session["lidc_cedis"] = vidc_cedis;
                    Session["sucursalprecios"] = Session["idc_sucursal"];
                    Session["cedisprecios"] = vidc_cedis;
                }

                return vidc_cedis;
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error al Cargar ID Cedis. \\n \\u000B \\n" + ex.Message);
                return 0;
            }
        }

        public void Estado_controles_captura(bool estado)
        {
            //txtcodigoarticulo.Enabled = estado
            btnagregar.Enabled = estado;
            //btncancelar.Enabled = estado
            txtprecio.Enabled = estado;
            txtcantidad.Enabled = estado;
            btncancelararticulo.Enabled = estado;
        }

        public void cargar_consecutivo_folio()
        {
            try
            {
                DataSet ds = componente.sp_folio_preped_pedidos();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string folio = ds.Tables[0].Rows[0]["no_folio"].ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(folio)))
                    {
                        lblfolio.Text = folio.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error al Cargar ID Cedis. \\n \\u000B \\n" + ex.Message);
            }
        }

        protected void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            cargarbusquedaclientes();
        }
        protected void txtcantidadgrid_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void cargarbusquedaclientes()
        {
            DataSet ds = new DataSet();
            try
            {
                ds = componente.sp_bclientes_ventas(txtbuscar.Text.Trim());
                if (ds.Tables[0].Rows.Count >= 1)
                {
                    cboclientes.DataSource = ds.Tables[0];
                    cboclientes.DataValueField = "idc_cliente";
                    cboclientes.DataTextField = "nombre3";
                    cboclientes.DataBind();

                    txtbuscar.Text = "";
                    //txtbuscar.Visible = False
                    //txtbuscar.Attributes["style"] = "display:none;"

                    cboclientes.Visible = true;
                    cboclientes.Attributes["style"] = "display:inline;";
                    cboclientes.Attributes["style"] = "width:100%;";
                    btnacep_bus.Visible = true;
                    btncan_bus.Visible = true;
                    btnbuscarcliente.CssClass = "Ocultar";
                    cboclientes.Focus();
                    ////seleccioncliente.Show()
                    ViewState["dt_clientes"] = ds.Tables[0];
                }
                else
                {
                    WriteMsgBox("No se encontro cliente con esa descripcion.");
                    txtbuscar.Text = "";
                    txtbuscar.Focus();
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
            }
        }

        public void limpiar_campos_cliente()
        {
            try
            {
                txtsucursalr.Text = "0";
                txtidOc.Text = "0";
                txtbuscar.Enabled = true;
                txtbuscar.Text = "";
                txtstatus.BackColor = System.Drawing.Color.White;
                txtnombre.BackColor = System.Drawing.Color.White;
                txtrfc.Text = "";
                txtnombre.Text = "";
                txtstatus.Text = "";
                txtid.Text = "";
                //txttotalarticulos.Text = ""
                limpiar_campos();
                DataTable dt = new DataTable();
                dt = ViewState["dt"] as DataTable;
                dt.Rows.Clear();
                ViewState["dt2"] = dt;
                DataRow rowprincipal = default(DataRow);
                rowprincipal = Session["rowprincipal"] as DataRow;
                rowprincipal = null;
                Session["rowprincipal"] = rowprincipal;
                carga_productos_seleccionadas();
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
            }
        }

        public void carga_productos_seleccionadas()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ViewState["dt"] as DataTable;
                //Dim dataset As New DataSet
                //dataset.Tables.Add(dt)
                if (dt.Rows.Count >= 1)
                {
                    grdproductos2.DataSource = dt;
                    grdproductos2.DataBind();
                    //txttotalarticulos.Text = dt.Rows.Count
                }
                else
                {
                    grdproductos2.DataSource = null;
                    grdproductos2.DataBind();
                    tbnguardarPP.Enabled = false;
                    //txttotalarticulos.Text = ""
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
            }
        }

        public void limpiar_campos()
        {
            txtcodigoarticulo.Text = "";
            txtdescripcion.Text = "";
            txtprecio.Text = "";
            txtUM.Text = "";
            txtcantidad.Text = "";
            txtcodigoarticulo.Enabled = true;
        }

        protected void btncancelararticulo_Click(object sender, EventArgs e)
        {
            Session["rowprincipal"] = null;
            limpiar_campos();
            txtcodigoarticulo.Enabled = true;
            txtcodigoarticulo.Focus();
            Estado_controles_captura(false);
        }

        protected void txtcodigoarticulo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                DataRow row = default(DataRow);
                if ((txtcodigoarticulo.Text.Trim().Length < 3))
                {
                    WriteMsgBox("Ingrese Minimo 3 Caracteres Para Realizar la Busqueda.");
                    return;
                }
                if (funciones.isNumeric(txtcodigoarticulo.Text))
                {
                    ds = componente.sp_buscar_articulo_VENTAS_existencias(txtcodigoarticulo.Text, "A",
                        Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(Session["sidc_usuario"]));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            cboproductos.DataSource = ds.Tables[0];
                            cboproductos.DataTextField = "desart";
                            cboproductos.DataValueField = "idc_articulo";
                            cboproductos.DataBind();
                            txtcodigoarticulo.Text = "";

                            cboproductos.Attributes["style"] = "width:100%";
                            cboproductos.Visible = true;
                            ViewState["dt_productos"] = ds.Tables[0];
                            Session["dt_productos_busqueda"] = ds.Tables[0];
                        }
                        else
                        {
                            ds = componente.sp_buscar_articulo_VENTAS_existencias(txtcodigoarticulo.Text, "A",
                       Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(Session["sidc_usuario"]));
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                row = ds.Tables[0].Rows[0];
                            }
                            else
                            {
                                WriteMsgBox("No se encontro articulo con esa descripción");
                                txtcodigoarticulo.Focus();
                            }
                        }
                    }
                    else
                    {
                        ds = componente.sp_buscar_articulo_VENTAS_existencias(txtcodigoarticulo.Text, "A",
                        Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(Session["sidc_usuario"]));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            cboproductos.DataSource = ds.Tables[0];
                            cboproductos.DataTextField = "desart";
                            cboproductos.DataValueField = "idc_articulo";
                            cboproductos.DataBind();
                            txtcodigoarticulo.Text = "";
                            cboproductos.Attributes["style"] = "width:100%";
                            cboproductos.Visible = true;
                            ViewState["dt_productos"] = ds.Tables[0];
                            Session["dt_productos_busqueda"] = ds.Tables[0];
                        }
                        else
                        {
                            WriteMsgBox("No se Encontro Articulo con esa Descripción.");
                        }
                    }
                }
                else
                {
                    DataTable dt = new DataTable();
                    ds = componente.sp_buscar_articulo_VENTAS_existencias(txtcodigoarticulo.Text, "C",
                        Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(Session["sidc_usuario"]));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //gridresultadosbusqueda.DataSource = ds
                        //gridresultadosbusqueda.DataBind()
                        dt = ds.Tables[0];
                        dt.Columns.Add("desart2");
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            dt.Rows[i]["desart2"] = dt.Rows[i]["desart"].ToString() + "  ||  " + dt.Rows[i]["unimed"].ToString();
                        }
                        //txtcodigoarticulo.Visible = False
                        txtcodigoarticulo.Text = "";

                        cboproductos.Attributes["style"] = "width:100%";
                        cboproductos.Visible = true;
                        cboproductos.DataSource = dt;
                        cboproductos.DataTextField = "desart2";
                        cboproductos.DataValueField = "idc_articulo";
                        cboproductos.DataBind();
                        ViewState["dt_productos"] = ds.Tables[0];
                        //mpeSeleccion.Show()
                    }
                    else
                    {
                        ds = componente.sp_buscar_articulo_VENTAS_existencias(txtcodigoarticulo.Text, "B",
                       Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(Session["sidc_usuario"]));

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dt = ds.Tables[0];
                            dt.Columns.Add("desart2");
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                dt.Rows[i]["desart2"] = dt.Rows[i]["desart"].ToString() + "  ||  " + dt.Rows[i]["unimed"].ToString();
                            }
                            txtcodigoarticulo.Text = "";

                            cboproductos.DataSource = dt;
                            cboproductos.DataTextField = "desart2";
                            cboproductos.DataValueField = "idc_articulo";
                            cboproductos.DataBind();
                            cboproductos.Visible = true;
                            ViewState["dt_productos"] = ds.Tables[0];
                        }
                        else
                        {
                            WriteMsgBox("No se Encontro Articulo con esa Descripción.");
                        }
                    }
                    Session["dt_productos_busqueda"] = ViewState["dt_productos"] as DataTable;
                }
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ceerar_busq", "<script>myStopFunction_busq();</script>", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ceerar_busq", "<script>myStopFunction_busq();</script>", false);
                throw ex;
            }
        }

        public bool validar_multiplos(int idc_articulo, decimal cantidad)
        {
            DataSet ds = new DataSet();
            DataRow row = default(DataRow);
            try
            {
                AgentesCOM com = new AgentesCOM();
                bool ret = false;
                ds = com.SP_arti_conv_int(cantidad, idc_articulo);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    if (Convert.ToBoolean(row["pconv"]) == false)
                    {
                        WriteMsgBox("Cantidad invalida...Solo multiplos de: " + row["rconversion"].ToString());
                        ret = false;
                    }
                    else
                    {
                        ret = true;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
                return false;
            }
        }

        public bool buscar_articulos_duplicados(int idc_articulo)
        {
            bool functionReturnValue = false;
            DataTable dt = new DataTable();
            dt = ViewState["dt"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                DataRow row = default(DataRow);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    row = dt.Rows[i];
                    if (row["idc_articulo"].ToString() == idc_articulo.ToString())
                    {
                        return true;
                        return functionReturnValue;
                    }
                }
                return false;
            }
            return functionReturnValue;
        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            try
            {
                //'Que todos los TextBox tengan datos.
                if (Validar_Campos() == true)
                {
                    DataRow rowprincipal = default(DataRow);
                    rowprincipal = Session["rowprincipal"] as DataRow;
                    if (buscar_articulos_duplicados(Convert.ToInt32(rowprincipal["idc_articulo"])) == true)
                    {
                        WriteMsgBox("Ya Haz Seleccionado Este Articulo.");
                        rowprincipal = null;
                        limpiar_campos();
                        return;
                    }
                    if (validar_multiplos(Convert.ToInt32(rowprincipal["idc_articulo"]), Convert.ToDecimal(txtcantidad.Text.Trim())) == false)
                    {
                        txtcantidad.Text = "";
                        txtcantidad.Focus();
                        return;
                    }
                    if (funciones.isNumeric(txtcantidad.Text.Trim()) == false)
                    {
                        WriteMsgBox("La cantidad no es correcta, ['0' o '0.000']");
                        return;
                    }
                    else if (funciones.isNumeric(txtprecio.Text.Trim()) == false)
                    {
                        WriteMsgBox("El precio no es correcto, ['1,000.0000']");
                        return;
                    }

                    //Checar la existencia...
                    if (Convert.ToBoolean(rowprincipal["vende_exis"]) == true & Convert.ToBoolean(rowprincipal["comercial"]) == true)
                    {
                        if (No_Vender_Mas_De_Existencia(Convert.ToInt32(rowprincipal["idc_articulo"]), Convert.ToDouble(txtcantidad.Text.Trim())) == false)
                        {
                            var _with1 = txtcantidad;
                            _with1.Text = "";
                            _with1.Focus();
                            return;
                        }
                    }
                    else if (Convert.ToBoolean(rowprincipal["vende_exis"]) == false & Convert.ToBoolean(rowprincipal["comercial"]) == true)
                    {
                        DataRow row = default(DataRow);
                        DataSet ds = new DataSet();
                        try
                        {
                            ds = componente.sp_bexistencia_disponible(Convert.ToInt32(Session["xidc_almacen"]), Convert.ToInt32(rowprincipal["idc_articulo"]), 0);
                            row = ds.Tables[0].Rows[0];
                            if (Convert.ToDecimal(row["EXISTENCIA_DISPONIBLE"]) < Convert.ToDecimal(txtcantidad.Text.Trim()))
                            {
                                Session["Continuar"] = true;
                                WriteMsgBox("Solo hay en existencia: " + row["EXISTENCIA_DISPONIBLE"].ToString() + "<br/>" + "¿Desea Continuar?");
                            }
                        }
                        catch (Exception ex)
                        {
                            WriteMsgBox(ex.Message);
                        }
                    }
                    //Agregar articulo en la TABLA TEMPORAL
                    rowprincipal["Codigo"] = txtcodigoarticulo.Text.Trim();
                    rowprincipal["Descripcion"] = txtdescripcion.Text.Trim();
                    rowprincipal["UM"] = txtUM.Text.Trim();
                    rowprincipal["Cantidad"] = txtcantidad.Text.Trim();

                    if (!(Convert.ToBoolean(rowprincipal["nota_credito"]) == false))
                    {
                        if (Convert.ToDouble(rowprincipal["Precio"]) != Math.Round(Convert.ToDouble(txtprecio.Text.Trim()), 4))
                        {
                            rowprincipal["precioreal"] = Math.Round(Convert.ToDouble(txtprecio.Text), 4);
                        }
                    }
                    else
                    {
                        rowprincipal["precioreal"] = Math.Round(Convert.ToDouble(txtprecio.Text), 4);
                    }

                    rowprincipal["Precio"] = Convert.ToDecimal(txtprecio.Text.Trim());
                    rowprincipal["Importe"] = Convert.ToDecimal(Convert.ToDecimal(rowprincipal["precio"]) * Convert.ToDecimal(rowprincipal["cantidad"]));
                    rowprincipal["Descuento"] = Convert.ToDecimal((Convert.ToDecimal(rowprincipal["precio"]) - Convert.ToDecimal(rowprincipal["PRECIOREAL"])));
                    DataTable dt = default(DataTable);
                    dt = ViewState["dt"] as DataTable;
                    Session["rowprincipal"] = rowprincipal;
                    dt.Rows.Add(rowprincipal.ItemArray);
                    ViewState["DT"] = dt;
                    Productos_Calculados();
                    Calcular_Valores_DataTable();
                    carga_productos_seleccionadas();
                    cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
                    rowprincipal = null;
                    limpiar_campos();
                    Estado_controles_captura(false);
                    buscar_confirmacion_lista();
                    formar_cadenas();
                    tbnguardarPP.Enabled = true;
                    btnnuevoprepedido.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.Message);
            }
        }

        public void cargar_subtotal_iva_total(double t_iva)
        {
            t_iva = t_iva / 100;
            //Se divide para sacar el %.
            DataTable dt = new DataTable();
            dt = ViewState["dt"] as DataTable;
            if (!txtrfc.Text.StartsWith("*"))
            {
                decimal[] subtotal = new decimal[3];
                decimal[] iva = new decimal[3];
                decimal[] total = new decimal[3];
                DataRow row = default(DataRow);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    row = dt.Rows[i];
                    subtotal[0] = Convert.ToDecimal(subtotal[0]) + Convert.ToDecimal(row["Importe"]);
                    subtotal[1] = Convert.ToDecimal(subtotal[1]) + (Convert.ToDecimal(row["Descuento"].ToString()==""?"0": row["Descuento"]) * Convert.ToDecimal(row["cantidad"]));
                }
                subtotal[1] = Math.Round(subtotal[1], 2, MidpointRounding.AwayFromZero);
                //Redondeo a dos decimales
                subtotal[0] = Math.Round(subtotal[0], 2, MidpointRounding.AwayFromZero);
                //Redondeo a dos decimales
                subtotal[2] = subtotal[0] - subtotal[1];

                iva[0] = Convert.ToDecimal(Convert.ToDouble(subtotal[0]) * t_iva);
                iva[0] = Math.Round(iva[0], 2);
                //Redondeo a dos decimales
                iva[1] = Convert.ToDecimal(Convert.ToDouble(subtotal[1]) * t_iva);
                iva[1] = Math.Round(iva[1], 2);
                //Redondeo a dos decimales
                iva[2] = iva[0] - iva[1];

                total[0] = subtotal[0] + iva[0];
                total[0] = Math.Round(total[0], 2, MidpointRounding.AwayFromZero);
                //Redondeo a dos decimales

                total[1] = subtotal[1] + iva[1];
                total[1] = Math.Round(total[1], 2, MidpointRounding.AwayFromZero);
                //Redondeo a dos decimales
                total[2] = subtotal[2] + iva[2];

                txtsubt.Text = Formato_moneda(Convert.ToDouble(subtotal[0]));
                txtsubtotaldescuento.Text = Formato_moneda(Convert.ToDouble(subtotal[1]));
                txtsubtotal.Text = Formato_moneda(Convert.ToDouble(subtotal[2]));
                txtsubiva.Text = Formato_moneda(Convert.ToDouble(iva[0]));
                txtivadescuento.Text = Formato_moneda(Convert.ToDouble(iva[1]));
                txtiva.Text = Formato_moneda(Convert.ToDouble(iva[2]));
                txtpretotal.Text = Formato_moneda(Convert.ToDouble(total[0]));
                txttotaldescuento.Text = Formato_moneda(Convert.ToDouble(total[1]))==""?"0.00": Formato_moneda(Convert.ToDouble(total[1]));
                txttotal.Text = Formato_moneda(Convert.ToDouble(total[2]));
            }
            else
            {
                decimal total = default(decimal);
                decimal descuento = default(decimal);
                DataRow row = default(DataRow);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    row = dt.Rows[i];
                    total = total + Convert.ToDecimal(row["Importe"]);
                    descuento = descuento + (Convert.ToDecimal(row["Descuento"]) * Convert.ToDecimal(row["cantidad"]));
                }
                total = Math.Round(total, 2, MidpointRounding.AwayFromZero);
                descuento = decimal.Round(descuento, 2, MidpointRounding.AwayFromZero);
                txtsubt.Text = Formato_moneda(Convert.ToDouble(total));
                txtsubtotaldescuento.Text = Formato_moneda(Convert.ToDouble(descuento));
                //"$   " & descuento
                txtsubtotal.Text = Formato_moneda(Convert.ToDouble(total - descuento));
                //"$   " & total - descuento
                txtsubiva.Text = Formato_moneda(0.0);
                //"$   0.00"
                txtivadescuento.Text = Formato_moneda(0.0);
                // "$   0.00"
                txtiva.Text = Formato_moneda(0.0);
                //"$   0.00"
                txtpretotal.Text = Formato_moneda(Convert.ToDouble(total));
                //"$   " & total
                txttotaldescuento.Text = Formato_moneda(Convert.ToDouble(descuento))==""?"0.00":Formato_moneda(Convert.ToDouble(descuento)); ;
                //"$   " & descuento
                txttotal.Text = Formato_moneda(Convert.ToDouble(total - descuento));
                //"$   " & (total - descuento)
            }
        }

        public string Formato_moneda(double valor)
        {
            try
            {
                //Return FormatNumber(valor, 2, -2, -2, -1)
                valor = Math.Round(valor, 2);
                return valor.ToString("#,###.##");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Calcular_Valores_DataTable()
        {
            DataTable dt = new DataTable();
            dt = ViewState["dt"] as DataTable;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dt.Rows[i][6] = Convert.ToDecimal(dt.Rows[i][4]) * Convert.ToDecimal(dt.Rows[i][5]);
                //Importe = Cantidad * Precio
                if (Convert.ToBoolean(dt.Rows[i]["nota_credito"]) == false)
                {
                    dt.Rows[i][8] = Convert.ToDecimal(dt.Rows[i]["precio"]) - Convert.ToDecimal(dt.Rows[i][7]);
                    dt.Rows[i][8] = Convert.ToDecimal(dt.Rows[i][8]).ToString("#,###.####");
                }
            }
        }

        public void formar_cadenas()
        {
            if ((cboentrega.SelectedValue == 1.ToString() | cboentrega.SelectedValue == 4.ToString()) & !string.IsNullOrEmpty(txtidc_colonia.Text))
            {
                DataTable dt = new DataTable();
                dt = ViewState["dt"] as DataTable;
                string cadena1 = "";
                string cadena2 = "";
                DataTable dt_r = default(DataTable);
                DataRow row = default(DataRow);
                int total = 0;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    row = dt.Rows[i];
                    cadena1 = cadena1 + row["idc_articulo"].ToString() + ";" + row["Cantidad"].ToString() + ";" + row["Precio"].ToString() + ";" + row["PrecioReal"].ToString() + ";"
                        + row["ids_especif"].ToString() + ";" + row["num_especif"].ToString() + ";";
                    cadena2 = cadena2 + row["idc_articulo"].ToString() + ";" + row["Cantidad"].ToString() + ";";
                    total = total + 1;
                }
                bool desg_iva = false;
                if (txtrfc.Text.StartsWith("*") == true)
                {
                    desg_iva = false;
                }
                else
                {
                    desg_iva = true;
                }
                try
                {

                    string query = "select * from dbo.fn_cadenas_fletes_preped_sumar_pedidos_tabla_1_esp('" + cadena1 + "'," + total + "," + 
                        Convert.ToInt32(Session["idc_sucursal"]).ToString() +","+txtidc_colonia.Text.Trim()+
                        "," + txtid.Text.Trim() + "," + (desg_iva == true ? "1" : "0") + "," + Convert.ToInt32(Session["Xiva"]).ToString() +
                        ",'" + cadena2 + "',0)";
                    dt_r = funciones.ExecQuery(query);
                    //dt_r = componente.SP_Cadenas_Fletes_Preped(cadena1, total, 18, Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(txtidc_colonia.Text.Trim()),
                    //    Convert.ToInt32(txtid.Text.Trim()), desg_iva, Convert.ToInt32(Session["Xiva"]), cadena2).Tables[0];

                    if (dt_r.Rows.Count > 0)
                    {
                        if (txtrfc.Text.Trim().StartsWith("*"))
                        {
                            txtmaniobras.Text = (Convert.ToDecimal(dt_r.Rows[0]["flete"]) * ((Convert.ToDecimal(Session["NuevoIva"]) / 100) + 1)).ToString("#,###.####");
                        }
                        else
                        {
                            txtmaniobras.Text = (Convert.ToDecimal(dt_r.Rows[0]["flete"])).ToString("#,###.####")==""?"0.00" : (Convert.ToDecimal(dt_r.Rows[0]["flete"])).ToString("#,###.####");
                        }
                        Session["gastooperativo"] = dt_r.Rows[0]["gastos"];
                        //11-05-2015 agergue variable de sesion distanciaentrega
                        Session["distanciaentrega"] = dt_r.Rows[0]["DISTANCIA"];
                    }
                    else
                    {
                        txtmaniobras.Text = "0.00";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Productos_Calculados()
        {
            DataTable dt = new DataTable();
            dt = ViewState["dt"] as DataTable;
            double valor_calculado = 0;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i][19]) == true)
                {
                    for (int x = 0; x <= dt.Rows.Count - 1; x++)
                    {
                        if (Convert.ToBoolean(dt.Rows[x][19]) == false)
                        {
                            valor_calculado = valor_calculado + Convert.ToDouble(dt.Rows[x][6]);
                        }
                    }
                    valor_calculado = valor_calculado * (Convert.ToDouble(dt.Rows[i][20]) / 100);
                    dt.Rows[i][5] = valor_calculado;
                    dt.Rows[i][6] = (Convert.ToDouble(dt.Rows[i][4]) * Convert.ToDouble(dt.Rows[i][5]));
                    dt.Rows[i][7] = Convert.ToDouble(dt.Rows[i][5]);
                }
            }
        }

        public bool No_Vender_Mas_De_Existencia(int idc_articulo, double cantidad)
        {
            DataRow rowprincipal = Session["rowprincipal"] as DataRow;
            DataRow row = default(DataRow);
            DataSet ds = new DataSet();
            try
            {
                AgentesCOM com = new AgentesCOM();
                ds = com.sp_bexistencia_disponible(Convert.ToInt16(Session["xidc_almacen"]), idc_articulo, 0);
                row = ds.Tables[0].Rows[0];
                rowprincipal["Existencia"] = row["EXISTENCIA_DISPONIBLE"].ToString();
                if ((cantidad <= Convert.ToDouble(rowprincipal["Existencia"])) == false)
                {
                    WriteMsgBox("No puedes vender mas de la existencia, existencia: " + row["EXISTENCIA_DISPONIBLE"].ToString());
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
                return false;
            }
        }

        public bool Validar_Campos()
        {
            bool functionReturnValue = false;

            if (string.IsNullOrEmpty(txtcodigoarticulo.Text))
            {
                WriteMsgBox("Indicar el codigo del articulo");
                return false;
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(txtdescripcion.Text))
            {
                WriteMsgBox("Indicar la descricpción del articulo");
                return false;
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(txtUM.Text))
            {
                WriteMsgBox("Indicar Unidad de Medida del articulo");
                return false;
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(txtcantidad.Text))
            {
                WriteMsgBox("Es necesario indicar la cantidad");
                return false;
                return functionReturnValue;
            }
            else if (txtcantidad.Text == 0.ToString())
            {
                WriteMsgBox("La cantidad no puede ser Cero...");
                return false;
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(txtprecio.Text))
            {
                WriteMsgBox("Es necesario capturar el precio del articulo");
                return false;
                return functionReturnValue;
            }
            else if (txtprecio.Text == 0.ToString())
            {
                WriteMsgBox("El precio debera ser mayor a Cero...");
                return false;
                return functionReturnValue;
            }
            else
            {
                return true;
            }
            return functionReturnValue;
        }

        public void cargar_credito_disponible(int idc_cliente)
        {
            DataSet ds = new DataSet();
            DataRow row = default(DataRow);
            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pidc_cliente = idc_cliente;
                ds = componente.credito_disponible(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    txtcreditodisponible.Text = Convert.ToDecimal(row["disponible"]).ToString("#,###.##");
                    if (Convert.ToInt32(row["disponible"]) < 0)
                    {
                        txtcreditodisponible.BackColor = System.Drawing.Color.Red;
                        txtcreditodisponible.ForeColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        txtcreditodisponible.BackColor = System.Drawing.Color.Green;
                        txtcreditodisponible.ForeColor = System.Drawing.Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.Message);
            }
        }

        protected void grdproductos2_EditCommand(object source, DataGridCommandEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = ViewState["dt"] as DataTable;
            DataRow rowprincipal = default(DataRow);
            Session["EditandoGrid"] = e.Item.ItemIndex;
            Session["PrecioAntes"] = dt.Rows[e.Item.ItemIndex][5];
            grdproductos2.EditItemIndex = e.Item.ItemIndex;
            carga_productos_seleccionadas();
            //grdproductos2.Items(grdproductos2.EditItemIndex).Cells(1).Enabled = False
            //grdproductos2.Items(grdproductos2.EditItemIndex).Cells(2).Enabled = False
            rowprincipal = dt.Rows[e.Item.ItemIndex];
            TextBox txtprecioreal = grdproductos2.Items[e.Item.ItemIndex].FindControl("txtprecioreal") as TextBox;
            TextBox preciogrid = grdproductos2.Items[e.Item.ItemIndex].FindControl("txtpreciogrid") as TextBox;
            TextBox txtcantidadgrid = grdproductos2.Items[e.Item.ItemIndex].FindControl("txtcantidadgrid") as TextBox;
            //preciogrid.Attributes.Add("onblur", "LostFocus();")
            //txtcantidadgrid.Attributes.Add("onblur", "LostFocus();")
            //txtprecioreal.Attributes.Add("onblur", "LostFocus();")
            Session["Precio"] = preciogrid.Text;
            Session["PrecioReal"] = txtprecioreal.Text;
            Session["Cantidad"] = txtcantidadgrid.Text;
            if (Convert.ToBoolean(dt.Rows[e.Item.ItemIndex][19]) == true)
            {
                grdproductos2.Items[grdproductos2.EditItemIndex].Cells[4].Enabled = false;
                grdproductos2.Items[grdproductos2.EditItemIndex].Cells[6].Enabled = false;
            }
            if (Convert.ToBoolean(dt.Rows[e.Item.ItemIndex][9]) == true)
            {
                txtcantidad.MaxLength = 11;
            }
            else
            {
                txtcantidadgrid.MaxLength = 7;
            }
            if (Convert.ToBoolean(dt.Rows[e.Item.ItemIndex][21]) == true)
            {
                txtprecioreal.Enabled = false;
                preciogrid.Enabled = false;
            }
        }

        protected void grdproductos2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = ViewState["dt"] as DataTable;

            if (e.CommandName == "Editar")
            {
                Label lbl = new Label();
                lbl = e.Item.FindControl("lblid") as Label;
                Session["dt_productos_lista"] = ViewState["dt"] as DataTable;
                txtidc_articulo.Text = lbl.Text.Trim();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "editar_articulo", "editar_precios_cantidad_1(" + txtidc_articulo.Text.Trim() + ");", true);
            }

            if (e.CommandName == "eliminar")
            {
                int index = 0;
                index = e.Item.ItemIndex;
                int id = (dt.Rows[index]["idc_promocion"].ToString()=="" ? 0 : Convert.ToInt32(dt.Rows[index]["idc_promocion"]));
                eliminar_promocion(id);
                dt.Rows[index].Delete();
                Session["dt_productos_lista"] = dt;
                carga_productos_seleccionadas();
                cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
                formar_cadenas();
                if (!(dt.Columns.Count > 0))
                {
                    tbnguardarPP.Enabled = false;
                }
            }

            if (e.CommandName == "Guardar")
            {
                int index = e.Item.ItemIndex;
                // obtiene el numero del Row que se esta editando.
                DataRow row = default(DataRow);
                TextBox cantidad = default(TextBox);
                TextBox precio = default(TextBox);
                TextBox precioreal = default(TextBox);
                cantidad = (e.Item.Cells[3].FindControl("txtcantidadgrid") as TextBox);
                precio = e.Item.Cells[4].FindControl("txtpreciogrid") as TextBox;
                precioreal = e.Item.Cells[6].FindControl("txtprecioreal") as TextBox;
                row = dt.Rows[index];

                if (precioreal.Text == Session["PrecioAntes"] as string)
                {
                    precioreal.Text = precio.Text;
                }

                //Si la cantidad=0 eliminar articulo de la lista
                if (cantidad.Text == 0.ToString())
                {
                    dt.Rows[e.Item.ItemIndex].Delete();
                    grdproductos2.EditItemIndex = -1;
                    cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
                    buscar_confirmacion_lista();
                    // Checa si el cliente requiere conficmacion de pago.
                    Productos_Calculados();
                    carga_productos_seleccionadas();
                    formar_cadenas();
                    return;
                }

                //----
                if (Convert.ToBoolean(row["vende_exis"]) == true & Convert.ToBoolean(row["comercial"]) == true)
                {
                    if (No_Vender_Mas_De_Existencia(Convert.ToInt32(row["idc_articulo"]), Convert.ToDouble(cantidad.Text.Trim())) == false)
                    {
                        return;
                    }
                }
                if (validar_multiplos(Convert.ToInt32(row["idc_articulo"]), Convert.ToDecimal(cantidad.Text.Trim())) == false)
                {
                    return;
                }                
                if (Convert.ToDecimal(precio.Text) == 0)
                {
                    CargarMsgbox("", "El precio no puede ser IGUAL a Cero", false, 2);
                    return;
                }
                if (Convert.ToDecimal(precioreal.Text) == 0)
                {
                    precioreal.Text = precio.Text;
                }
                if (Convert.ToDecimal(precioreal.Text) > Convert.ToDecimal(precio.Text) == true)
                {
                    CargarMsgbox("", "El Precio Real no puede ser mayor al precio", false, 2);
                    return;
                }
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    row["precio"] = precio.Text;
                    row["cantidad"] = cantidad.Text;
                    row["precioreal"] = precioreal.Text;
                }
                grdproductos2.EditItemIndex = -1;
                Calcular_Valores_DataTable();
                Productos_Calculados();
                cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
                carga_productos_seleccionadas();
                formar_cadenas();
            }

            if (e.CommandName == "Cancelar")
            {
                dt.Rows[Convert.ToInt32(Session["EditandoGrid"])][4] = Session["cantidad"];
                dt.Rows[Convert.ToInt32(Session["EditandoGrid"])][5] = Session["PrecioReal"];
                dt.Rows[Convert.ToInt32(Session["EditandoGrid"])][7] = Session["Precio"];
                Calcular_Valores_DataTable();
                cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
                grdproductos2.EditItemIndex = -1;
                carga_productos_seleccionadas();
                formar_cadenas();
            }
        }

        private void CargarMsgbox(string Titulo, string Mesaje, bool Condicion, int tipo)
        {
            WriteMsgBox(Mesaje);
        }


        public void eliminar_promocion(int idc_promocion)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Session["tx_pedido_gratis"] as DataTable;
                if (dt.Rows.Count > 0)
                {
                    for (int i = dt.Rows.Count - 1; i >= 0; i += -1)
                    {
                        if (idc_promocion.ToString() == dt.Rows[i]["idc_promocion"].ToString())
                        {
                            dt.Rows[i].Delete();
                        }
                    }

                    if (dt.Rows.Count == 0)
                    {
                        imgpromocion.Attributes["style"] = "display:none;";
                        imgpromocion.Attributes.Remove("onclick");
                    }
                }
                Session["tx_pedido_gratis"] = dt;
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error al Tratar de Eliminar Articulo Gratis x Promocion.");
            }
        }

        protected void btnnuevoprepedido_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["idc_cliente"] != null)
            {
                Response.Redirect("pedidos7.aspx?idc_cliente=" + Request.QueryString["idc_cliente"]);
            }
            else {
                Response.Redirect("pedidos7.aspx");
            }
       }

        public void CleanControls(ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                }
                else if (control is DropDownList)
                {
                    ((DropDownList)control).ClearSelection();
                }
                else if (control is RadioButtonList)
                {
                    ((RadioButtonList)control).ClearSelection();
                }
                else if (control is CheckBoxList)
                {
                    ((CheckBoxList)control).ClearSelection();
                }
                else if (control is RadioButton)
                {
                    ((RadioButton)control).Checked = false;
                }
                else if (control is CheckBox)
                {
                    ((CheckBox)control).Checked = false;
                }
                else if (control is DataGrid)
                {
                    ((DataGrid)control).DataSource = null;
                    ((DataGrid)control).DataBind();
                }
                else if (control is GridView)
                {
                    ((GridView)control).DataSource = null;
                    ((GridView)control).DataBind();
                }
                else if (control.HasControls())
                {
                    CleanControls(control.Controls);
                }
            }
        }

        public string Redondeo_Dos_Decimales(decimal valor)
        {

            try
            {
                valor = Math.Round(valor, 2);
                return valor.ToString("#,###.##");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Ajustes_iva(decimal nuevo_iva, decimal iva_ant)
        {
            DataTable dt = new DataTable();
            dt = ViewState["dt"] as DataTable;
            if (txtrfc.Text.StartsWith("*"))
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    dt.Rows[i][5] = Convert.ToDecimal(dt.Rows[i][5]) / (1 + (iva_ant / 100)) * (1 + (nuevo_iva / 100));
                    dt.Rows[i][6] = Redondeo_Dos_Decimales(Convert.ToDecimal(dt.Rows[i][5]) * Convert.ToDecimal(dt.Rows[i][4]));
                    dt.Rows[i][7] = Redondeo_Dos_Decimales(Convert.ToDecimal(dt.Rows[i][5]) - Convert.ToDecimal(dt.Rows[i][8]));
                }
                cargar_subtotal_iva_total(Convert.ToDouble(nuevo_iva));
            }
            else
            {
                cargar_subtotal_iva_total(Convert.ToDouble(nuevo_iva));
            }
            ViewState["dt"] = dt;
            carga_productos_seleccionadas();
        }

        public void Agregar_maniobras()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = ViewState["dt"] as DataTable;
            DataRow row = default(DataRow);
            DataRow rowdt = dt.NewRow();
            try
            {
                bool existe = false;
                int item = 0;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (dt.Rows[i][0].ToString() == 4454.ToString())
                    {
                        existe = true;
                        item = i;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if (existe == true)
                {
                    dt.Rows[item][5] = Redondeo_cuatro_decimales(Convert.ToDouble(dt.Rows[item][5]) + Convert.ToDouble(txtmaniobras.Text.Trim()));
                    dt.Rows[item][7] = Redondeo_cuatro_decimales(Convert.ToDouble(dt.Rows[item][7]) + Convert.ToDouble(txtmaniobras.Text.Trim()));


                    ViewState["dt"] = dt;
                    Productos_Calculados();
                    Calcular_Valores_DataTable();
                    cargar_subtotal_iva_total(Convert.ToInt32(Session["NuevoIva"]));
                    carga_productos_seleccionadas();
                }
                else
                {
                    string codigo = null;
                    DataTable dt2 = new DataTable();
                    dt2 = componente.sp_codigo_articulo(4454).Tables[0];
                    if (dt2.Rows.Count > 0)
                    {
                        codigo = dt2.Rows[0]["codigo"].ToString().Trim();
                    }
                    else
                    {
                        return;
                    }
                    ds = componente.sp_buscar_articulo_VENTAS_existencias(codigo, "A", Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(Session["sidc_usuario"]));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //cargar_datos_productob_x_codigo(ds.Tables(0).Rows(0))
                        row = ds.Tables[0].Rows[0];
                        rowdt["idc_articulo"] = row["idc_articulo"];
                        rowdt["Codigo"] = row["codigo"];
                        rowdt["Descripcion"] = row["desart"];
                        rowdt["UM"] = row["unimed"];
                        rowdt["Decimales"] = row["decimales"];
                        rowdt["Paquete"] = row["paquete"];
                        rowdt["precio_libre"] = row["precio_libre"];
                        rowdt["comercial"] = row["comercial"];
                        rowdt["fecha"] = row["fecha"];
                        rowdt["obscotiza"] = row["obscotiza"];
                        rowdt["vende_exis"] = row["vende_exis"];
                        rowdt["minimo_venta"] = row["minimo_venta"];
                        rowdt["costo"] = costo_maniobras();
                        rowdt["calculado"] = false;
                        rowdt["porcentaje"] = 0;
                        rowdt["Anticipo"] = row["Anticipo"];
                        rowdt["Precio"] = Redondeo_cuatro_decimales(Convert.ToDouble(txtmaniobras.Text.Trim()));
                        rowdt["Importe"] = Redondeo_Dos_Decimales(Convert.ToDecimal(txtmaniobras.Text.Trim()));
                        rowdt["PrecioReal"] = Redondeo_cuatro_decimales(Convert.ToDouble(txtmaniobras.Text.Trim()));
                        rowdt["nota_credito"] = false;
                        rowdt["descuento"] =0.0000;
                        rowdt["Cantidad"] = 1;
                        rowdt["Existencia"] = existencia_maniobras();
                        dt.Rows.InsertAt(rowdt, dt.Rows.Count);
                        ViewState["dt"] = dt;
                        carga_productos_seleccionadas();
                        cargar_subtotal_iva_total(Convert.ToInt32(Session["NuevoIva"]));
                        Calcular_Valores_DataTable();
                        Productos_Calculados();
                    }
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox("", ex.Message, false, 1);
            }
        }
        public double costo_maniobras()
        {
            double costo = 0;
            DataSet ds = new DataSet();
            DataRow row = default(DataRow);
            try
            {
                ds = componente.sp_precio_cliente_cedis(4454, Convert.ToInt32(txtid.Text.Trim()), Convert.ToInt32(Session["idc_sucursal"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    costo = Convert.ToDouble(row["costo"]);
                    return costo;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
                return 0;
            }
        }

        public double existencia_maniobras()
        {
            DataSet ds = new DataSet();
            DataRow row = default(DataRow);
            double existencia = 0;
            try
            {
                ds = componente.sp_bexistencia_disponible(Convert.ToInt32(Session["xidc_almacen"]), 4454, 0);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    existencia = Convert.ToDouble(row["EXISTENCIA_DISPONIBLE"]);
                    return existencia;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
                return 0;
            }
        }
        public string Redondeo_cuatro_decimales(double valor)
        {
            try
            {
                valor = Math.Round(valor, 4);
                return valor.ToString("#,###.####");
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
                return "";
            }
        }

        public double calculado(int idc_articulo)
        {

            try
            {
                DataSet datos = new DataSet();
                DataRow row = default(DataRow);
                datos = componente.sp_bgastos_chqseg(idc_articulo);
                if (datos.Tables[0].Rows.Count > 0)
                {
                    row = datos.Tables[0].Rows[0];
                    return Convert.ToDouble(row["porcentaje"]);
                }
                else {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
                return 0;
            }
        }

        public void cargar_datos_productob_x_codigo(ref DataRow row)
        {
            //Recopilar la informacion de un producto seleccionado
            DataTable dt = new DataTable();
            dt = ViewState["dt"] as DataTable;
            DataRow rowprincipal = default(DataRow);
            rowprincipal = dt.NewRow();
            Session["rowprincipal"] = rowprincipal;
            try
            {
                if (calculado(Convert.ToInt32(row["idc_articulo"])) == 0)
                {
                    buscar_precio_producto(Convert.ToInt32(row["idc_articulo"]));
                    txtcodigoarticulo.Text = row["codigo"].ToString();
                    txtdescripcion.Text = row["desart"].ToString();
                    txtUM.Text = row["unimed"].ToString();
                    //Decimales(CBool(row("decimales")))
                    rowprincipal["idc_articulo"] = row["idc_articulo"];
                    rowprincipal["Codigo"] = row["codigo"];
                    rowprincipal["Descripcion"] = row["desart"];
                    rowprincipal["UM"] = row["unimed"];
                    rowprincipal["Decimales"] = row["decimales"];
                    rowprincipal["Paquete"] = row["paquete"];
                    rowprincipal["precio_libre"] = row["precio_libre"];
                    rowprincipal["comercial"] = row["comercial"];
                    rowprincipal["fecha"] = row["fecha"];
                    rowprincipal["obscotiza"] = row["obscotiza"];
                    rowprincipal["vende_exis"] = row["vende_exis"];
                    rowprincipal["minimo_venta"] = row["minimo_venta"];
                    rowprincipal["calculado"] = false;
                    rowprincipal["porcentaje"] = 0;
                    rowprincipal["Anticipo"] = row["Anticipo"];
                    rowprincipal["Existencia"] = buscar_Existencia_Articulo(Convert.ToInt32(rowprincipal["idc_articulo"]));
                    Estado_controles_captura(true);
                    if (Convert.ToBoolean(rowprincipal["Decimales"]) == true)
                    {
                        txtcantidad.MaxLength = 11;
                    }
                    else
                    {
                        txtcantidad.MaxLength = 7;
                    }
                    txtcantidad.Focus();
                    if (Convert.ToBoolean(rowprincipal["nota_credito"]) == true)
                    {
                        txtprecio.Enabled = false;
                    }
                    //Es un articulo calculado, se agrega a la lista automaticamente...
                }
                else
                {
                    rowprincipal["idc_articulo"] = row["idc_articulo"];
                    rowprincipal["Codigo"] = row["codigo"];
                    rowprincipal["Descripcion"] = row["desart"];
                    rowprincipal["UM"] = row["unimed"];
                    rowprincipal["Decimales"] = row["decimales"];
                    rowprincipal["Paquete"] = row["paquete"];
                    rowprincipal["precio_libre"] = row["precio_libre"];
                    rowprincipal["comercial"] = row["comercial"];
                    rowprincipal["fecha"] = row["fecha"];
                    rowprincipal["obscotiza"] = row["obscotiza"];
                    rowprincipal["vende_exis"] = row["vende_exis"];
                    rowprincipal["minimo_venta"] = row["minimo_venta"];
                    rowprincipal["calculado"] = true;
                    rowprincipal["Porcentaje"] = calculado(Convert.ToInt32(row["idc_articulo"]));
                    rowprincipal["cantidad"] = 1;
                    rowprincipal["precioreal"] = Redondeo_cuatro_decimales(0);
                    rowprincipal["precio"] = Redondeo_cuatro_decimales(0);
                    dt.Rows.Add(rowprincipal);
                    limpiar_campos();
                    Productos_Calculados();
                    Calcular_Valores_DataTable();
                    Productos_Calculados();
                    cargar_subtotal_iva_total(Convert.ToInt32(Session["NuevoIva"]));
                    carga_productos_seleccionadas();
                    rowprincipal = null;
                    var _with1 = txtcodigoarticulo;
                    _with1.Enabled = true;
                    _with1.Focus();
                    Estado_controles_captura(false);
                }
                Session["rowprincipal"] = rowprincipal;
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
            }
        }
        public void buscar_precio_producto(int codigo)
        {
            //If txtidc_colonia.Text = "0" Or txtidc_colonia.Text.Trim = "" Then
            //    Session["pxidc_sucursal"] = Session["idc_sucursal"]
            //End If
            //Dim row As DataRow
            DataRow rowprincipal = default(DataRow);
            rowprincipal = Session["rowprincipal"] as DataRow;
            rowprincipal["nota_credito"] = false;

            int vidc = codigo;
            int vidcli = Convert.ToInt32(txtid.Text.Trim());
            int vidc_clonia = Convert.ToInt32(txtidc_colonia.Text.Trim());
            dynamic dt = default(DataTable);
            dynamic dt1 = default(DataTable);
            dynamic dt2 = default(DataTable);
            DataTable dt3 = new DataTable();
            decimal vprecio = 0;

            int vidc_listap = Convert.ToInt32(txtlistap.Text.Trim());
            int zidc_sucursal = Convert.ToInt32(Session["pxidc_sucursal"]);


            //Cambios



            /////

            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pcadenaarti = "exec sp_precio_cliente_cedis @pidc_articulo = "
                    + vidc.ToString() + ",@pidc_cliente=" + vidcli.ToString() + ",@pidc_sucursal=" +
                    Session["idc_sucursal"] as string;
                dt = componente.sp_exec_query_web(entidad);

            }
            catch (Exception ex)
            {
                WriteMsgBox("Error al Cargar Precio del Producto.  \\n \\u000b \\n Error: \\n" + ex.Message);
                return;
            }


            if (dt.Rows.Count > 0)
            {
                if (dt.Rows(0).Item("precio") <= 0)
                {
                    //Limpiar campos del articulo
                    WriteMsgBox("No se Encontro el Precio de Producto. \\n");
                    return;
                }
            }
            else
            {
                //Limpiar campos del articulo
                WriteMsgBox("No se Encontro el Precio de Producto. \\n");
                return;
            }
            vprecio = dt.Rows(0).Item("precio");


            if (lblroja.Visible == true)
            {
                try
                {
                    if (zidc_sucursal > 0)
                    {
                        AgentesENT entidad = new AgentesENT();
                        entidad.Pcadenaarti = "select dbo.fn_ver_precio_cliente_esp_cambio_lista(" + 
                            vidc.ToString() + "," + vidcli.ToString() + "," + zidc_sucursal.ToString() + ") as pxprecio";
                        dt1 = componente.sp_exec_query_web(entidad);
                    }
                    else
                    {
                        AgentesENT entidad = new AgentesENT();
                        entidad.Pcadenaarti = "select dbo.fn_ver_precio_cliente_esp_lp_SUC(" 
                            + vidc.ToString() + "," + vidcli.ToString() + "," + vidc_listap.ToString() + "," +
                            Session["idc_sucursal"] as string + ") as pxprecio";
                        dt1 = componente.sp_exec_query_web(entidad);
                    }
                    vprecio = dt1.Rows(0).Item("pxprecio");
                }
                catch (Exception ex)
                {
                    WriteMsgBox("No Se Procedio a Verificar Precios. \\n- \\n" + ex.Message);
                }


                try
                {
                    AgentesENT entidad = new AgentesENT();
                    entidad.Pcadenaarti = "select dbo.fn_ver_precio_real_cliente_esp_cambio_lista(" 
                        + vidc.ToString() + "," + vidcli.ToString() + "," + zidc_sucursal.ToString() + ") as pxprecior";
                    dt2 = componente.sp_exec_query_web(entidad);
                    rowprincipal["PrecioReal"] = dt2.Rows[0]["pxprecior"];
                    Session["pprecio_real"] = dt2.Rows[0]["pxprecior"];
                    rowprincipal["descuento"] = Math.Round(vprecio -Convert.ToDecimal(rowprincipal["PrecioReal"]), 4);

                    if (Convert.ToDecimal(rowprincipal["descuento"]) > 0)
                    {
                        rowprincipal["nota_credito"] = true;
                    }
                    else
                    {
                        rowprincipal["nota_credito"] = false;
                    }


                }
                catch (Exception ex)
                {
                    WriteMsgBox("No Se procedio a Verificar Precios. \\n - \\n" + ex.Message);
                }
            }




            rowprincipal["Costo"] = dt.Rows[0]["costo"];
            int viva = Convert.ToInt32(Session["NuevoIva"]);

            if (txtrfc.Text.StartsWith("*"))
            {
                rowprincipal["precio"] = Redondeo_cuatro_decimales(Convert.ToDouble(vprecio * (1 + (viva / 100))));
            }
            else
            {
                rowprincipal["precio"] = Redondeo_cuatro_decimales(Convert.ToDouble(vprecio));
            }




            try
            {
                DataSet ds = new DataSet();
                DataRow row = default(DataRow);
                decimal vprecio_real = 0;
                ds = componente.SP_nc_auto_CLIENTE_articulo( codigo, Convert.ToInt32(txtid.Text.Trim()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    if (lblroja.Visible == false)
                    {
                        vprecio_real = Convert.ToDecimal(Session["precio_real"]);
                    }
                    else
                    {
                        vprecio_real = Convert.ToDecimal(Session["precio_real"]);
                    }
                    txtprecio.Enabled = false;
                    rowprincipal["Costo"] = row[8];
                    rowprincipal["descuento"] = row["descuento"];
                    rowprincipal["nota_credito"] = true;
                    rowprincipal["PrecioReal"] = Redondeo_cuatro_decimales(Convert.ToDouble(Convert.ToDecimal(rowprincipal["precio"]) - Convert.ToDecimal(row["descuento"])));
                    if (txtrfc.Text.StartsWith("*"))
                    {
                        vprecio_real = Math.Round(vprecio_real * (1 + (Convert.ToDecimal(Session["nuevoiva"]) / 100)), 4);
                        vprecio = Math.Round(Convert.ToDecimal(row["precio"]) * (1 + (Convert.ToDecimal(Session["nuevoiva"]) / 100)), 4);
                    }
                    else
                    {
                        vprecio = Math.Round(Convert.ToDecimal(row["precio"]), 4);
                    }
                    rowprincipal["Precio"] = vprecio;
                }
                else
                {
                    rowprincipal["nota_credito"] = false;
                    //rowprincipal("PrecioReal") = txtprecio.Text
                    //rowprincipal("Precio") = vprecio
                }

            }
            catch (Exception ex)
            {
                WriteMsgBox("No se procedio a buscar Nota de Credito Automatica de este Articulo. \\n \\u000B \\n" + ex.Message);
                //btncancelar_Click(Nothing, EventArgs.Empty)
                Session["rowprincipal"] = null;
            }
            Session["rowprincipal"] = rowprincipal;



        }


        public double buscar_Existencia_Articulo(int idc_articulo)
        {
            
            DataRow row = default(DataRow);
            DataSet ds = new DataSet();
            try
            {
                ds = componente.sp_bexistencia_disponible(Convert.ToInt32(Session["xidc_almacen"]), idc_articulo, 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    return Convert.ToDouble(row["EXISTENCIA_DISPONIBLE"]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.Message);
                return 0;

            }
        }

        protected void grdproductos2_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblid = new Label();
                lblid = e.Item.FindControl("lblid") as Label;
                e.Item.Cells[1].Attributes["onclick"] = "return ver_ficha(" + lblid.Text + ");";
                e.Item.Cells[1].Attributes["onmouseover"] = "cursor(this);";
                e.Item.Cells[1].Attributes["onmouseout"] = "cursor_fuera(this);";

                ImageButton btnmobile = new ImageButton();
                btnmobile = e.Item.FindControl("imgmobile") as ImageButton;
                btnmobile.Attributes["onclick"] = "return editar_precios_cantidad_1(" + lblid.Text.Trim() + ");";

                if (e.Item.Cells[10].Text == "True")
                {
                    e.Item.Attributes["style"] = "color:red;";

                }
            }
        }

        public void cargar_datos_colonia_proyecto(int idc_colonia)
        {
            DataSet ds = new DataSet();
            DataRow row = default(DataRow);
            try
            {
                ds = componente.sp_datos_colonia(idc_colonia);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    txtidc_colonia.Text = row["idc_colonia"].ToString();
                    txtmunicipio.Text = row["mpio"].ToString();
                    txtestado.Text = row["edo"].ToString();
                    txtpais.Text = row["pais"].ToString();
                    chkton.Checked = Convert.ToBoolean(row["capacidad_maxima"]);
                    txttoneladas.Text = row["toneladas"].ToString();
                    txtCP.Text = row["cod_postal"].ToString();
                    txtcolonia.Text = row["nombre"].ToString();
                    lblrestriccion.Text = row["restriccion"].ToString();
                }
                else
                {
                    CargarMsgbox("", "La colonia no existe", false, 1);

                }

            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
            }
        }

        protected void btncambiarcroquis_Click(object sender, EventArgs e)
        {
            fucroquis.Attributes["style"] = "display:inline;";
            //imgupcroquis.Attributes["style"] = "display:;"
            var _with1 = lblruta;
            //
            _with1.Attributes["style"] = "Display:none";
            //Oculta Label con el nombre img croquis
            _with1.Text = "";
            //     
            btncambiarcroquis.Attributes["style"] = "Display:none";
            //Oculta el botón "Cambiar Croquis"
            //Oculta el botón "Cambiar Croquis"
            btnvercroquis.Attributes["style"] = "Display:none";
        }

        protected void btnagregarcroquis_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>alert('Es Necesario Seleccionar Cliente'); </script>", false);
                return;
            }
            if (validar_tipo_archivo_img(System.IO.Path.GetExtension(fucroquis.FileName)) == true)
            {
                lblruta.Attributes["Style"] = "Display:";
                lblruta.Text = fucroquis.FileName;
                string ext = System.IO.Path.GetExtension(lblruta.Text).ToUpper();
                if (fucroquis.HasFile == true)
                {
                    guardar_temp_croquis(ext);
                    imgloading.Attributes["Style"] = "display:none";
                    btnvercroquis.Enabled = true;
                    btnvercroquis.Attributes["OnClick"] = "return verCroquis();";
                }
            }
            else
            {
                CargarMsgbox("", "El tipo de imagen seleccionado no es valido.", false, 4);
            }

        }
        public void guardar_temp_croquis(string ext)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/files/"));//path local
                fucroquis.SaveAs(dirInfo + txtid.Text.Trim() + ext);
                imgupcroquis.Attributes["style"] = "display:none;";
                btncambiarcroquis.Attributes["style"] = "display:";
                btnvercroquis.Attributes["style"] = "display:";
                fucroquis.Attributes["style"] = "display:none;";
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
            }

        }
        public bool validar_tipo_archivo_img(string ext)
        {
            try
            {
                ext = ext.ToUpper();
                if (ext == ".JPEG")
                {
                    return true;
                }
                else if (ext == ".JPG")
                {
                    return true;

                }
                else if (ext == ".BMP")
                {
                    return true;

                }
                else if (ext == ".DIB")
                {
                    return true;

                }
                else if (ext == ".GIF")
                {
                    return true;

                }
                else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox("", ex.ToString(), false, 4);
                return false;
            }
        }
        public bool validar_tipo_archivo_audio(string ext)
        {           
            try
            {
                if (ext == ".MP3" )
                {
                    return true;
                }
                else if (ext == ".WMA")
                {
                    return true;

                }
                else if (ext == ".ACC")
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox("", ex.ToString(), false, 4);
                return false;
            }

        }

        protected void btnagregarllamada_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtid.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>alert('Es Necesario Seleccionar Cliente'); </script>", false);
                    return;
                }
                string extension = Path.GetExtension(fullamada.FileName.ToUpper());

                if (validar_tipo_archivo_audio(Path.GetExtension(extension)) == true)
                {
                    if (fullamada.HasFile)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/files/"));//path local
                        fullamada.SaveAs(dirInfo + txtid.Text.Trim() + extension);
                        fullamada.Attributes["style"] = "display:none";
                        imgupllamada.Attributes["style"] = "display:none";
                        btnescucharll.Enabled = true;
                        lblllamada.Attributes["style"] = "display:";
                        lblllamada.Text = fullamada.FileName;
                        btnquitarll.Attributes["style"] = "display:";
                        btnescucharll.Attributes["style"] = "display:";
                    }

                }
                else
                {
                    CargarMsgbox("", "El tipo de archivo seleccionado no es valido.", false, 2);
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
            }
        }

        public string unidades(string clave)
        {
            try
            {
                return funciones.GenerarRuta(clave,"UNIDAD"); 
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
                return "";
            }
        }

        public bool Verificar_Limite(int idc_cliente, double monto)
        {
            try
            {
                DataSet ds = componente.SP_Saldo_Total_Cliente(idc_cliente, monto);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                }
                else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox("", ex.Message, false, 1);
                return false;
            }
        }

        public bool Unica_Venta(string cadena, int totalarticulos)
        {           
            try
            {
                DataSet ds = componente.SP_VALIDAR_UVENCOM(cadena, totalarticulos);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox("", ex.Message, false, 1);
                return false;
            }
        }

        public bool Cambios_Precios(string cadena, int totalarticulos, int idc_cliente, int idc_sucursal, bool vcambio_lista)
        {
            try
            {
                DataSet ds = componente.SP_Cambio_Precios(cadena, totalarticulos, idc_cliente, idc_sucursal, vcambio_lista);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox("", ex.Message, false, 1);
                return false;
            }
        }

        public double volumen_carga(string cadenapeso, int tipocamion, int totalarticulos)
        {            
            try
            {
                DataSet ds = componente.SP_VSp_Carga_VolmumenALIDAR_UVENCOM(cadenapeso, tipocamion, totalarticulos);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToDouble(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox("", ex.Message, false, 1);
                return 0;
            }
        }

        public double Monto_Minimo_Venta(int idc_cliente)
        {
            try
            {
                DataSet ds = componente.SP_Monto_Minimo_Venta(idc_cliente);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToDouble(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox("", ex.Message, false, 1);
                return 0;
            }

        }
        public bool articulos_entrega_directa(string peso, int idc_cliente, double totalarticulos)
        {
            try
            {
                DataSet ds = componente.SP_Articulos_Entrega_Directa(peso, idc_cliente, totalarticulos);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox("", ex.Message, false, 1);
                return false;
            }
        }

        public bool articulos_limite_de_venta(string cadena, int idc_cliente, int total, int idc_alamacen)
        {           
            try
            {
                DataSet ds = componente.SP_Articulos_Cantidad_Permitida(cadena, idc_cliente, total, idc_alamacen);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox("", ex.Message, false, 1);
                return false;
            }
        }

        public bool validar_occli()
        {
            try
            {
                DataSet ds = componente.sp_occ_valida_cliente(Convert.ToInt32(txtidOc.Text.Trim()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox("", ex.Message, false, 1);
                return false;
            }
        }

        public bool validar_campos_direccion()
        {
            if (txtidc_colonia.Text == "0")
            {
                CargarMsgbox("", "Faltan de Completar Datos en el Consignado...Es Obligatorio.", false, 1);
                return false;
            }
            else if (cbofechas.SelectedIndex < 0)
            {
                CargarMsgbox("", "Es Necesario Ingresar la Fecha de Entrega", false, 1);
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool validar_fecha_entrega(System.DateTime fecha_seleccionada)
        {
            System.DateTime fecha_maxima = default(System.DateTime);
            try
            {
                DataTable dt = componente.SP_FechaMaxima().Tables[0];
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                else {

                    fecha_maxima = Convert.ToDateTime(dt.Rows[0][0]);
                    if (fecha_maxima > fecha_seleccionada & fecha_seleccionada >= DateTime.Today)
                    {
                        return true;
                    }
                    else
                    {
                        if (fecha_seleccionada <= DateTime.Today)
                        {
                            CargarMsgbox("", "La Fecha debe ser mayor o igual al Día de Hoy", false, 2);
                        }
                        else if (fecha_seleccionada > fecha_maxima)
                        {
                            WriteMsgBox("La Fecha Maxima de Entrega es: " + fecha_maxima.ToString());
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox("", ex.Message, false, 1);
                return false;
            }
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
            txtzm.Text = null;
            ///imgcroquis.Attributes.Remove("onClick")  'Remove image´s croquis on Tabcontainer.
            txtmaniobras.Text = "0.00";
        }

        public void txt_codigo()
        {
            txtcodigoarticulo.Enabled = true;
            txtcodigoarticulo.Focus();
        }
        public void limpiar_pedido_especial()
        {
            txtformaP.Text = "";
            txtplazo.Text = "";
            txtotro.Text = "";
            txtcminima.Text = "";
            txtcontacto.Text = "";
            txttelefono.Text = "";
            txtcorreo.Text = "";
        }
        
        public void limpiar_recoge_cliente()
        {
            txtnombrerecoge.Text = "";
            txtpaternor.Text = "";
            txtmaternor.Text = "";
            txtsucursalr.Text = "0";
        }

        protected void btnbuscarcol_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtid.Text.Trim()))
            {
                formar_cadenas();
            }
        }

        protected void btnbuscarcliente_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text) & !string.IsNullOrEmpty(txtbuscar.Text))
            {
                cargarbusquedaclientes();
            }
        }

        protected void btnbuscarart_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtcodigoarticulo.Text.Trim()))
            {
                txtcodigoarticulo_TextChanged(null, EventArgs.Empty);
            }
        }

        protected void btnquitarll_Click(object sender, EventArgs e)
        {
            fullamada.Attributes["style"] = "display:none";
            //Muestra FileUpload.
            imgupllamada.Attributes["style"] = "display:";
            var _with1 = lblllamada;
            //
            _with1.Attributes["style"] = "Display:none";
            //Oculta Label con el nombre de audio.
            _with1.Text = "";
            //     
            btnquitarll.Attributes["style"] = "Display:none";
            //Oculta el botón "Quitar".
            btnescucharll.Attributes["style"] = "Display:none";

        }

        protected void btnredirecting_Click(object sender, EventArgs e)
        {
            Response.Redirect("ficha_cliente_m.aspx");
        }

        public bool validar_colonia()
        {
            try
            {
                DataTable dt = componente.sp_fn_validar_entrega_colonia(Convert.ToInt32(txtidc_colonia.Text.Trim())).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string valida = dt.Rows[0][""].ToString();
                    if (!string.IsNullOrEmpty(valida))
                    {
                        WriteMsgBox("Colonia No Permitida.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else {
                    WriteMsgBox("Colonia No Permitida.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.ToString());
                return false;
            }
        }
        public bool Cliente_Bloqueado(int idc_cliente)
        {
            try
            {
                DataSet ds = componente.SP_Cliente_Bloqueado(idc_cliente);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox("", ex.Message, false, 1);
                return false;
            }
        }
        protected void btncalc_iva_Click(object sender, EventArgs e)
        {
            validar_cambio_iva_Frontera();
            actualizar_precios(Convert.ToInt32(txtid.Text.Trim()), Convert.ToInt32(txtidc_colonia.Text.Trim()));
            formar_cadenas();
        }

        public bool validar_cambio_iva_Frontera()
        {
            bool ret = false;
            if (!string.IsNullOrEmpty(txtidc_colonia.Text.Trim()) & txtidc_colonia.Text.Trim() != "0")
            {
                DataRow row = default(DataRow);
                try
                {
                    DataSet ds = componente.sp_cambiar_iva_frontera(Session["idc_sucursal"] == null ? 0 : Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(txtidc_colonia.Text));
                    row = ds.Tables[0].Rows[0];
                    if (Convert.ToBoolean(row[0]) == false)
                    {
                        if (!(Session["NuevoIva"].ToString() == row[2].ToString()))
                        {
                            Session["ivaant"] = Session["NuevoIva"];
                            Session["idc_ivaant"] = Session["nuevo_idc_iva"];
                            Session["NuevoIva"] = row[2];
                            Session["nuevo_idc_iva"] = row[1];
                            Session["pidc_iva"] = row[2];
                            WriteMsgBox("El IVA no Corresponde a la Zona de Entrega se Recalculara el IVA al " + Convert.ToInt32(Session["NuevoIva"]).ToString() + "%");
                            Ajustes_iva(Convert.ToDecimal(Session["NuevoIva"]), Convert.ToDecimal(Session["ivaant"]));
                            etiqueta_Iva(Session["NuevoIva"] as string);
                            ret = true;
                        }
                    }
                    else if (Convert.ToBoolean(row[0]) == true)
                    {
                        string lidc_iva = Session["lidc_iva"].ToString().Trim();
                        string pidc_iva = Session["pidc_iva"].ToString().Trim();
                        if (lidc_iva != pidc_iva)
                        {
                            Session["pidc_iva"] = Session["lidc_iva"];
                            Session["ivaant"] = Session["NuevoIva"];
                            Session["idc_ivaant"] = Session["nuevo_idc_iva"];
                            Session["NuevoIva"] = row[2].ToString();
                            Session["nuevo_idc_iva"] = row[1].ToString();
                            WriteMsgBox("El IVA no Corresponde a la Zona de Entrega se Recalculara el IVA al " + Convert.ToDecimal(Session["NuevoIva"]).ToString() + "%");
                            Ajustes_iva(Convert.ToDecimal(Session["NuevoIva"]), Convert.ToDecimal(Session["ivaant"]));
                            etiqueta_Iva(Session["NuevoIva"] as string);
                            ret = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    WriteMsgBox(ex.ToString());
                    return false;
                }
            }
            else
            {
                if (!(Session["NuevoIva"] == Session["Xiva"]))
                {
                    Session["ivaant"] = Session["NuevoIva"];
                    Session["NuevoIva"] = Session["Xiva"];
                    WriteMsgBox("El IVA no Corresponde a la Zona de Entrega se Recalculara el IVA al " + Convert.ToInt32(Session["NuevoIva"]).ToString() + "%");
                    Ajustes_iva(Convert.ToDecimal(Session["NuevoIva"]), Convert.ToDecimal(Session["ivaant"]));
                    etiqueta_Iva(Session["NuevoIva"] as string);
                    ret = true;
                }

            }
            return ret;
        }

        public void actualizar_precios(int idc_cliente, int idc_colonia)
        {
            //txtid
            //txtidc_colonia
            int vidc_listap = 0;
            bool vetiqueta = false;
            string vmotivo = "";
            bool vccambio = false;
            bool voriginal = false;
            int iva_rec_suc = Convert.ToInt32(txtsucursalr.Text);
            int vidc_sucursal = 0;

            if (string.IsNullOrEmpty(txtidc_colonia.Text) | txtidc_colonia.Text == "0")
            {
                vidc_sucursal = Convert.ToInt32(Session["idc_sucursal"]);

                if (Convert.ToInt32(Session["pxidc_sucursal"]) != vidc_sucursal)
                {
                    vmotivo = "(Regresa a su Lista de Precios.)";
                }



                if (Convert.ToInt32(txtsucursalr.Text.Trim()) == 0)
                {
                    Session["pxidc_sucursal"] = Session["idc_sucursal"];
                    voriginal = true;
                }
                else
                {
                    Session["pxidc_sucursal"] = txtsucursalr.Text.Trim();
                }




                vidc_sucursal = Convert.ToInt32(Session["pxidc_sucursal"]);

                int cedis_sucursal = cedis_prg(vidc_sucursal);
                if (cedis_sucursal !=Convert.ToInt32(Session["cedisprecios"]) & Convert.ToInt32(txtsucursalr.Text.Trim()) > 0)
                {
                    vetiqueta = true;
                    vmotivo = "(La Sucursal de Entrega no corresponde a la Lista de Precios del Cliente.)";
                }
                vccambio = true;
            }
            else
            {
                AgentesENT entidad = new AgentesENT();
                int pxsuc = 0;
                if (Convert.ToInt32(txtlistap.Text) > 0)
                {
                    entidad.Pcadenaarti = "select dbo.fn_corresponde_colonia_cliente_sin_publico(" + txtid.Text + "," + txtidc_colonia.Text.Trim() + ") as pxsuc";
                 
                }
                else
                {
                    entidad.Pcadenaarti = "select dbo.fn_corresponde_colonia_cliente_publico(" + Session["idc_sucursal"] as string + "," + txtidc_colonia.Text.Trim() + ") as pxsuc";
                }
                DataSet ds = componente.sp_exec_query_web(entidad);
                pxsuc = Convert.ToInt32(ds.Tables[0].Rows[0]["pxsuc"]);
                vidc_sucursal = pxsuc;

                if (vidc_sucursal > 0)
                {
                    Session["pxidc_sucursal"] = vidc_sucursal;
                    vccambio = true;
                }
                else
                {
                    if (lblroja.Visible == true)
                    {
                        Session["pxidc_sucursal"] = Session["idc_sucursal"];
                        vccambio = true;
                    }
                    else
                    {
                        Session["pxidc_sucursal"] = suc_cercana();
                    }
                }
                if (vidc_sucursal > 0)
                {
                    vetiqueta = true;
                    vmotivo = "(La Sucursal de Entrega no corresponde a la Lista de Precios del Cliente.)";
                }
                else
                {
                    vmotivo = "(Regresa a su Lista de Precios.)";
                }

            }
            string varticulos = "";
            int vnum = 0;
            DataTable dt = new DataTable();
            dt = ViewState["dt"] as DataTable;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    vnum = vnum + 1;
                    varticulos = varticulos + dt.Rows[i]["idc_articulo"].ToString() + ";";
                }
            }

            if (vccambio == false)
            {
                return;
            }
            AgentesENT entidad2 = new AgentesENT();
            DataTable dt_precios = new DataTable();
            if (vidc_sucursal > 0)
            {
                if (voriginal == false)
                {
                    entidad2.Pcadenaarti = "select * from dbo.fn_precios_articulos_LISTA('" + varticulos.ToString() + "'," + vnum.ToString() + "," + vidc_sucursal.ToString() + "," + txtid.Text.Trim() + ")";
                }
                else
                {
                    entidad2.Pcadenaarti = ("select * from dbo.fn_precios_articulos_LISTA_lp_suc('" + varticulos.ToString() + "'," + vnum.ToString() + ","
                        + txtlistap.Text.Trim() + "," + txtid.Text.Trim() + "," + Session["idc_sucursal"] as string + ")");
                }

            }
            else
            {
                vidc_listap = Convert.ToInt32(txtlistap.Text);
                if (vidc_listap == 0)
                {
                    entidad2.Pcadenaarti = ("select * from dbo.fn_precios_articulos_LISTA_lp_suc('" + varticulos.ToString() + "'," + vnum.ToString() + "," 
                        + vidc_listap.ToString() + "," + txtid.Text.Trim() + "," + Session["idc_sucursal"] as string + ")");


                }
                else
                {
                    entidad2.Pcadenaarti = ("select * from dbo.fn_precios_articulos_LISTA_lp('" + varticulos.ToString() + "'," + vnum.ToString() + "," 
                        + vidc_listap.ToString() + "," + txtid.Text.Trim() + ")");

                }
            }
            DataSet ds2 = componente.sp_exec_query_web(entidad2);
            dt_precios = ds2.Tables[0];
            if (dt_precios.Rows.Count > 0)
            {
                int piva = Convert.ToInt32(Session["NuevoIva"]);
                if (txtrfc.Text.StartsWith("*"))
                {
                    for (int i = 0; i <= dt_precios.Rows.Count - 1; i++)
                    {
                        dt_precios.Rows[i]["precio"] = Math.Round(Convert.ToDouble(dt_precios.Rows[i]["precio"]) * (1 + piva / 100), 4);
                        dt_precios.Rows[i]["precio_real"] = Math.Round(Convert.ToDouble(dt_precios.Rows[i]["precio_real"]) * (1 + piva / 100), 4);
                    }
                }
            }

            bool actualizar = false;
            DataRow[] rows = null;
            if (dt_precios.Rows.Count > 0)
            {
                for (int i = 0; i <= dt_precios.Rows.Count - 1; i++)
                {
                    rows = dt.Select("idc_articulo=" + dt_precios.Rows[i]["idc_articulo"].ToString());
                    if (rows.Length > 0)
                    {
                        if (rows[0]["precio"].ToString() != dt_precios.Rows[i]["precio"].ToString())
                        {
                            WriteMsgBox("Se Actualizarán los Precios... \\n-\\n " + vmotivo);
                            actualizar = true;
                            rows = null;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    rows = null;
                }
            }

            if (actualizar == true)
            {

                for (int i = 0; i <= dt_precios.Rows.Count - 1; i++)
                {

                    for (int ii = 0; ii <= dt.Rows.Count - 1; ii++)
                    {

                        if (dt.Rows[ii]["idc_articulo"].ToString() == dt_precios.Rows[i]["idc_articulo"].ToString())
                        {
                            if (dt.Rows[ii]["precio"].ToString() != dt_precios.Rows[i]["precio"].ToString())
                            {
                                dt.Rows[ii]["precio"] = dt_precios.Rows[i]["precio"];
                                dt.Rows[ii]["precioreal"] = dt_precios.Rows[i]["precio_real"];
                                if (dt.Rows[ii]["precioreal"].ToString() == dt.Rows[ii]["precio"].ToString())
                                {
                                    dt.Rows[ii]["Nota_Credito"] = false;
                                }
                                else
                                {
                                    dt.Rows[ii]["Nota_Credito"] = true;
                                }
                            }

                            break; // TODO: might not be correct. Was : Exit For
                        }

                    }

                }
            }
            ViewState["dt"] = dt;
            lblroja.Visible = vetiqueta;
            Calcular_Valores_DataTable();
            Productos_Calculados();
            Ajustes_iva(Convert.ToDecimal(Session["NuevoIva"]), Convert.ToDecimal(Session["ivaant"]));
            cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
        }
        public int suc_cercana()
        {
            int sucent = 0;
            try
            {
                DataSet ds = componente.sp_fn_sucursal_mas_cercana(Convert.ToInt32(txtidc_colonia.Text.Trim()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox("No se ha procedido a Verificar Sucursal mas Cercana \\n \\u000B \\n" + ex.Message + "\\n \\u000B \\n");
                return 0;
            }
        }

        public int cedis_prg(int idc_sucursal)
        {
            int pxid = 0;
            try
            {
                DataSet ds = componente.sp_fn_cedis_sucursal(idc_sucursal);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error al Tratar de Buscar la Informacion de Cedis de Sucursal. \\n \\u000B \\n" + ex.Message);
                return pxid;
            }
        }

        public DataTable c_precios_art(string consulta)
        {
            DataTable dt = new DataTable();
            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pcadenaarti = consulta;
                dt = componente.sp_exec_query_web(entidad).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                WriteMsgBox("No se Procedio a Consultar Precios. \\n-\\n" + ex.Message);
                return dt;
            }
        }

        public int m_pxsuc(string consulta)
        {
            DataTable dt = new DataTable();
            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pcadenaarti = consulta;
                dt = componente.sp_exec_query_web(entidad).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0][0]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox("No se Procedio a Validar la Colonia. \\n-\\n" + ex.Message);
                return 0;
            }
        }

        protected void btncaptArt_Click(object sender, EventArgs e)
        {
            try
            {
                int idccboentrega = Convert.ToInt32(cboentrega.SelectedValue);
                if (idccboentrega == 1)
                {

                    btnconsignado.Text = "Consignado";

                }
                else if (idccboentrega == 2)
                {
                    btnconsignado.Text = "Consignado";
                }
                else if (idccboentrega == 3)
                {
                    btnconsignado.Text = "Detalle Recoge Cliente";
                }
                else if (idccboentrega == 4)
                {
                    btnconsignado.Text = "Detalle Anticipos";
                }
                if (string.IsNullOrEmpty(txtid.Text.Trim()))
                {
                    WriteMsgBox("Es Necesario Seleccionar el Cliente.");
                    return;
                }
                
                if (idccboentrega == 1)
                {
                    if (validar_datos_dir() == false)
                    {
                        WriteMsgBox("Faltan de Completar Datos en el Consignado...Es Obligatorio.");
                        //ScriptManager.RegisterStartupScript(Me, GetType(Page), "JS_con", "<script>popup_consignado();</script>", False)
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "js_clear", "<script>myStopFunction();</script>", false);
                        txtcodigoarticulo.Text = "";
                        return;
                    }
                }
                else if (idccboentrega == 3)
                {
                    if (txtsucursalr.Text == "0" | string.IsNullOrEmpty(txtsucursalr.Text))
                    {
                        WriteMsgBox("Falta Completar Datos de Donde va Recoger el Cliente.");
                        txtcodigoarticulo.Text = "";
                        //ScriptManager.RegisterStartupScript(Me, GetType(Page), "JS_con", "<script>RecogeCliente();</script>", False)
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "js_clear", "<script>myStopFunction();</script>", false);
                        return;
                    }
                }
                else if (idccboentrega == 4)
                {
                    if (validar_pedido_especial() == false)
                    {
                        WriteMsgBox("Falta Completar Datos del Pre-Pedido Especial.");
                        txtcodigoarticulo.Text = "";
                        //ScriptManager.RegisterStartupScript(Me, GetType(Page), "JS_con", "<script>PedidoEspecial();</script>", False)
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "js_clear", "<script>myStopFunction();</script>", false);
                        return;
                    }
                }
                
                //ScriptManager.RegisterStartupScript(Me, GetType(Page), "JS_con", "<script>mostrar_procesar();</script>", False)


                int tipo = 0;
                tipo = Convert.ToInt32(Request.QueryString["tipo"]);


                if (tipo == 1)
                {
                    txtid.Text = Session["idc_cliente"] as string;
                    ViewState["dt"] = proces_ped_lista(Session["dt_pedido_lista"] as DataTable);
                    Session["dt_productos_lista"] = ViewState["dt"] as DataTable;
                    Productos_Calculados();
                    Calcular_Valores_DataTable();
                    carga_productos_seleccionadas();
                    cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
                    limpiar_campos();
                    Estado_controles_captura(false);
                    buscar_confirmacion_lista();
                    formar_cadenas();
                    tbnguardarPP.Enabled = true;
                    btnnuevoprepedido.Enabled = true;
                    etiqueta_cheque();
                    txtbuscar.Focus();
                    botones_pedido();
                    carga_productos_seleccionadas();
                    Calcular_Valores_DataTable();
                    cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
                }

                estado_rd(false);
                txt_codigo();
                txt_consignado.Text = "1";
                controles_busqueda_prod(false);
                carga_productos_seleccionadas();
                controles_busqueda_master(true);
                prep_cargar_grid_prod_master_cliente(Convert.ToInt32(txtid.Text.Trim()));
                validar_cambio_iva_Frontera();
                actualizar_precios(Convert.ToInt32(txtid.Text.Trim()), Convert.ToInt32(txtidc_colonia.Text.Trim()));




                object[] datos_clientes_pedidos = {
                    txtid.Text.Trim(),
                    txtrfc.Text.Trim(),
                    txtlistap.Text.Trim(),
                    lblroja.Visible,
                    (!string.IsNullOrEmpty(txtidc_colonia.Text.Trim()) ? txtidc_colonia.Text : "0")
                };
                Session["datos_clientes_pedidos"] = datos_clientes_pedidos;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "js_clear", "<script>myStopFunction();</script>", false);
                labeliva.Visible = true;
                labeliva.Text = "IVA: " + Session["NuevoIva"] as string + "%";

                Session["tipo_de_entrega"] = cboentrega.SelectedValue;
               
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error \\n \\u000b \\n" + ex.Message);
            }

        }

        public void controles_busqueda_prod(bool estado)
        {
            txtcodigoarticulo.Visible = estado;
            txtcodigoarticulo.Enabled = estado;
            if (estado == true)
            {
                btnbuscar_codigo.Visible = false;
            }
            else
            {
                btnbuscar_codigo.Visible = true;
            }
        }

        public void controles_busqueda_master(bool estado)
        {
            cbomaster.Visible = estado;
            btn_seleccionar_master.Visible = estado;
            if (estado == true)
            {
                btnmaster.Visible = false;
            }
            else
            {
                btnmaster.Visible = true;
            }
        }

        public DataTable proces_ped_lista(DataTable dt)
        {
            DataRow row = default(DataRow);
            if ((dt != null))
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    row = dt.Rows[i];
                    buscar_precio_producto_lista(row);
                    //dt.Rows.Add(row)
                    //dt.Rows.RemoveAt(i)
                }
                return dt;

            }
            else
            {
                return null;
            }
        }



        public void buscar_precio_producto_lista(DataRow rowprincipal)
        {
            rowprincipal["nota_credito"] = false;

            int vidc =Convert.ToInt32(rowprincipal["idc_articulo"]);
            int vidcli = Convert.ToInt32(txtid.Text.Trim());
            int vidc_clonia = Convert.ToInt32(txtidc_colonia.Text.Trim());
            dynamic dt = default(DataTable);
            dynamic dt1 = default(DataTable);
            dynamic dt2 = default(DataTable);
            DataTable dt3 = new DataTable();

            decimal vprecio = 0;

            int vidc_listap = Convert.ToInt32(txtlistap.Text.Trim());
            int zidc_sucursal = Convert.ToInt32(Session["pxidc_sucursal"]);
            //Cambios el 15/02/2013
            DataSet ds1 = new DataSet();
            try
            {
                ds1 = componente.sp_precio_cliente_cedis_rangos1(vidc, vidcli, Convert.ToInt32(Session["idc_sucursal"]), 
                    Convert.ToDecimal(rowprincipal["cantidad"]), true, rowprincipal["ids_especif"] as string, Convert.ToInt32(rowprincipal["num_especif"]));
                //MIC 12-05-2015
                dt = ds1.Tables[0];
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error al Cargar Precio del Producto.  \\n \\u000b \\n Error: \\n" + ex.Message);
                return;
            }


            if (dt.Rows.Count > 0)
            {
                if (dt.Rows(0).Item("precio") <= 0)
                {
                    //Limpiar campos del articulo
                    WriteMsgBox("No se Encontro el Precio de Producto. \\n");
                    return;
                }
            }
            else
            {
                //Limpiar campos del articulo
                WriteMsgBox("No se Encontro el Precio de Producto. \\n");
                return;
            }
            rowprincipal["precio"] = dt.Rows[0]["precio"];
            if (lblroja.Visible == true)
            {
                try
                {
                    AgentesENT entidad = new AgentesENT();

                    if (zidc_sucursal > 0)
                    {
                        entidad.Pcadenaarti =("select dbo.fn_ver_precio_cliente_esp_cambio_lista(" + vidc.ToString() + "," + vidcli.ToString() + "," + zidc_sucursal.ToString() + ") as pxprecio");
                    }
                    else
                    {
                        entidad.Pcadenaarti = ("select dbo.fn_ver_precio_cliente_esp_lp_SUC(" + vidc.ToString() + "," + vidcli.ToString() + "," + vidc_listap.ToString() + "," + Session["idc_sucursal"]  as string+ ") as pxprecio");
                    }

                    dt1 = componente.sp_exec_query_web(entidad).Tables[0];
                    vprecio = Convert.ToDecimal(dt1.Rows[0]["pxprecio"]);
                }
                catch (Exception ex)
                {
                    WriteMsgBox("No Se Procedio a Verificar Precios. \\n- \\n" + ex.Message);
                }


                try
                {
                    AgentesENT entidad2 = new AgentesENT();
                    entidad2.Pcadenaarti = ("select dbo.fn_ver_precio_real_cliente_esp_cambio_lista(" + vidc.ToString() + "," + vidcli.ToString() + "," + zidc_sucursal.ToString() + ") as pxprecior");
                    dt2 = componente.sp_exec_query_web(entidad2).Tables[0];

                    rowprincipal["PrecioReal"] = dt2.Rows[0]["pxprecior"];
                    Session["pprecio_real"] = dt2.Rows[0]["pxprecior"];
                    rowprincipal["descuento"] = Math.Round(vprecio - Convert.ToDecimal(rowprincipal["PrecioReal"]), 4);

                    if (Convert.ToDecimal(rowprincipal["descuento"]) > 0)
                    {
                        rowprincipal["nota_credito"] = true;
                    }
                    else
                    {
                        rowprincipal["nota_credito"] = false;
                    }


                }
                catch (Exception ex)
                {
                    WriteMsgBox("No Se procedio a Verificar Precios. \\n - \\n" + ex.Message);
                }
            }
            rowprincipal["costo"] = dt.Rows[0]["costo"];
            int viva = Convert.ToInt32(Session["NuevoIva"]);
            vprecio = Convert.ToDecimal(rowprincipal["precio"].ToString().Trim());
            if (txtrfc.Text.Trim().StartsWith("*"))
            {
                rowprincipal["precio"] = Redondeo_cuatro_decimales(Convert.ToDouble(vprecio * (1 + (viva / 100))));
                rowprincipal["precioreal"] = rowprincipal["precio"];
            }
            else
            {
                rowprincipal["precio"] = vprecio;
                rowprincipal["precioreal"] = rowprincipal["precio"];
            }
            try
            {
                DataSet ds = new DataSet();
                DataRow row = default(DataRow);
                decimal vprecio_real = 0;
                ds = componente.SP_nc_auto_CLIENTE_articulo(Convert.ToInt32(txtid.Text.Trim()), vidc);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    rowprincipal["Costo"] = row[8];
                    rowprincipal["descuento"] = row["descuento"];
                    rowprincipal["nota_credito"] = true;
                    if (txtrfc.Text.StartsWith("*"))
                    {
                        vprecio_real = Math.Round(vprecio_real * (1 + (Convert.ToDecimal(Session["nuevoiva"]) / 100)), 4);
                        vprecio = Math.Round(Convert.ToDecimal(row["precio"]) * (1 + Convert.ToDecimal(Session["nuevoiva"])), 4);
                    }
                    else
                    {
                        vprecio = Math.Round(Convert.ToDecimal(row["precio"]), 4);
                    }
                    rowprincipal["Precio"] = vprecio;
                    rowprincipal["PrecioReal"] = Convert.ToDecimal(rowprincipal["precio"]);
                    txtprecio.Enabled = false;
                    txtprecio.Text = vprecio.ToString();
                }
                else
                {
                    rowprincipal["nota_credito"] = false;
                    rowprincipal["descuento"] = 0;
                }

            }
            catch (Exception ex)
            {
                WriteMsgBox("No se procedio a buscar Nota de Credito Automatica de este Articulo. \\n \\u000B \\n" + ex.Message);
                Session["rowprincipal"] = null;
            }
            Session["rowprincipal"] = rowprincipal;

        }


        public bool validar_datos_dir()
        {
            if (txtcalle.Text.Trim() == "" & (txtproy.Text == "" | txtproy.Text == 0.ToString()))
            {
                return false;
            }
            else if (txtnumero.Text.Trim()== "" & (txtproy.Text == "" | txtproy.Text == 0.ToString()))
            {
                return false;
            }
            else if (txtidc_colonia.Text.Trim() == "" & (txtproy.Text == "" | txtproy.Text == 0.ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool validar_pedido_especial()
        {
            if (string.IsNullOrEmpty(txtplazo.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(txtformaP.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(txtotro.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(txtcminima.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(txtcontacto.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(txttelefono.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void prep_cargar_grid_prod_master_cliente(int idc_cliente)
        {
            DataSet ds = new DataSet();

            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pidc_cliente = idc_cliente;
                ds = componente.SP_clientes_articulos_master(entidad);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = default(DataRow);
                        DataRow dtrow = default(DataRow);
                        DataTable dt = new DataTable();
                        dt = agregar_columnas_dataset2();
                        dt.Columns.Add("nombre2");
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            row = ds.Tables[0].Rows[i];
                            dtrow = dt.NewRow();
                            dtrow["nombre2"] = row["desart"].ToString() + " || " + row["unimed"].ToString();
                            dtrow["Codigo"] = row["codigo"];
                            dtrow["Descripcion"] = row["desart"];
                            ///dtrow["incluir"] = False
                            ///dtrow["precio_modelo"] = row["precio_modelo"]
                            dtrow["precio"] = row["precio_cliente"];
                            ///dtrow["dias"] = row["dias"]
                            dtrow["Master"] = row["Master"];
                            ///dtrow["id_grupo"] = row["id_grupo"]
                            dtrow["decimales"] = row["decimales"];
                            string precio_re = row["precio_real"].ToString();
                            if ( precio_re != "")
                            {
                                if (Convert.ToDecimal(row["precio_real"]) > 0)
                                {
                                    dtrow["PrecioReal"] = row["precio_real"];
                                    dtrow["Descuento"] = Convert.ToDecimal(dtrow["precio"]) - Convert.ToDecimal(dtrow["precio_real"]);
                                }
                                else
                                {
                                    dtrow["PrecioReal"] = dtrow["precio_cliente"];
                                    dtrow["Descuento"] = 0;
                                }
                            }
                            else
                            {
                                dtrow["PrecioReal"] = dtrow["precio"];
                                dtrow["Descuento"] = 0;
                            }
                            dtrow["idc_articulo"] = row["idc_articulo"];
                            ///dtrow["precio"] = row["precio_cliente"]
                            dtrow["UM"] = row["unimed"];
                            dtrow["costo"] = row["costo"];
                            ///dtrow["precio"] = row["precio_cliente"]
                            dtrow["Paquete"] = row["paquete"];
                            dtrow["precio_libre"] = 0;
                            dtrow["comercial"] = row["comercial"];
                            dtrow["fecha"] = row["fecha"];
                            dtrow["obscotiza"] = row["obscotiza"];
                            dtrow["vende_exis"] = row["vende_exis"];
                            dtrow["minimo_venta"] = 0;
                            dtrow["mensaje"] = row["mensaje"];
                            dtrow["porcentaje"] = row["porcentaje"];

                            if (Convert.ToDecimal(row["porcentaje"]) > 0)
                            {
                                dtrow["calculado"] = true;
                            }
                            else
                            {
                                dtrow["calculado"] = false;
                            }

                            dtrow["Anticipo"] = row["anticipo"];
                            dtrow["Existencia"] = 0;

                            if (Convert.ToBoolean(row["decimales"]) == true)
                            {
                                dtrow["cantidad"] = "0.00";
                            }
                            else
                            {
                                dtrow["cantidad"] = "0";
                            }
                            dt.Rows.Add(dtrow);
                        }
                        cbomaster.DataSource = dt;
                        cbomaster.DataTextField = "nombre2";
                        cbomaster.DataValueField = "idc_articulo";
                        cbomaster.DataBind();
                        cbomaster.Attributes["style"] = "width:100%;";
                        controles_busqueda_prod(false);
                        controles_busqueda_prod_sel_cancel(false);
                        controles_busqueda_master(true);
                        txtcodigoarticulo.Visible = false;
                        dt.Columns.Remove("nombre2");
                        ViewState["dt_master_cotizacion"] = dt;
                        Session["dt_productos_busqueda"] = ds.Tables[0];
                    }
                    else
                    {
                        cbomaster.Attributes["style"] = "width:100%;";
                    }
                }
                btn_seleccionar_master.Attributes["onclick"] = "return editar_precios_cantidad(2);";
            }
            catch (Exception ex)
            {
                WriteMsgBox(ex.Message);
            }
        }


        public DataTable agregar_columnas_dataset2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("idc_articulo", typeof(int));
            //0
            dt.Columns.Add("Codigo");
            //1
            dt.Columns.Add("Descripcion");
            //2
            dt.Columns.Add("UM");
            //3
            dt.Columns.Add("Cantidad");
            //4
            dt.Columns.Add("Precio", typeof(double));
            //5
            dt.Columns.Add("Importe", typeof(double));
            //6
            dt.Columns.Add("PrecioReal", typeof(double));
            //7
            dt.Columns.Add("Descuento", typeof(double));
            //8
            dt.Columns.Add("Decimales");
            //9
            dt.Columns.Add("Paquete");
            //10
            dt.Columns.Add("precio_libre");
            //11
            dt.Columns.Add("comercial");
            //12
            dt.Columns.Add("fecha");
            //13
            dt.Columns.Add("obscotiza");
            //14
            dt.Columns.Add("vende_exis");
            //15 
            dt.Columns.Add("minimo_venta");
            //16 
            dt.Columns.Add("Master");
            //17 
            dt.Columns.Add("mensaje");
            //18 
            dt.Columns.Add("Calculado");
            //19 
            dt.Columns.Add("Porcentaje");
            //20 
            dt.Columns.Add("Nota_Credito");
            //21 
            dt.Columns.Add("Anticipo", typeof(double));
            //22
            dt.Columns.Add("Costo", typeof(double));
            //23
            dt.Columns.Add("Existencia");
            //24
            dt.Columns.Add("precio_minimo", typeof(double));
            //25
            //dt.Columns.Add("ultm_precio")      '26
            //dt.Columns.Add("fecha_ult_precio") '27
            dt.Columns.Add("tiene_especif", typeof(bool));
            //28 
            dt.Columns.Add("especif", typeof(string));
            //29  
            dt.Columns.Add("num_especif", typeof(int));
            //30 
            dt.Columns.Add("ids_especif", typeof(int));
            //30 
            dt.Columns.Add("g_especif", typeof(int));
            //30 
            dt.Columns.Add("costo_o", typeof(decimal));
            dt.Columns.Add("precio_o", typeof(decimal));
            dt.Columns.Add("precio_lista_o", typeof(decimal));
            dt.Columns.Add("precio_minimo_o", typeof(decimal));
            return dt;
        }

        public void controles_busqueda_prod_sel_cancel(bool estado)
        {
            cboproductos.Visible = estado;
        }

        protected void btncan_bus_Click(object sender, EventArgs e)
        {
            controles_busqueda_cliente(true);
            controles_busqueda_cliente_cancel_selecc(false);
            cboclientes.Items.Clear();
            cboclientes.Visible = true;
        }

        protected void btnacep_bus_Click(object sender, EventArgs e)
        {
            if (cboclientes.Items.Count <= 0)
            {
                return;
            }

            DataTable dt = new DataTable();
            DataRow[] rows = null;
            try
            {
                dt = ViewState["dt_clientes"] as DataTable;
                rows = dt.Select("idc_cliente=" + cboclientes.SelectedValue);
                if (rows.Length > 0)
                {
                    txtrfc.Text = rows[0]["rfccliente"].ToString();
                    txtnombre.Text = rows[0]["nombre"].ToString();
                    txtid.Text = rows[0]["idc_cliente"].ToString();
                    txtstatus.Text = rows[0]["idc_bloqueoc"].ToString();
                    colores(txtstatus.Text);
                    controles_busqueda_cliente(true);
                    controles_busqueda_cliente_cancel_selecc(false);
                    int index = 0;
                    index = 1;

                    //cargar_credito_disponible(txtid.Text)//// Motivo de Comentarizar: Tarda +6 segundos en cargar datos Cliente.

                    Session["Clave_Adi"] = rows[0]["cveadi"];
                    //Session["cad_prod"] = gridbuscar_clientes.Items(index).Cells(6).Text
                    //Session["credito"] = gridbuscar_clientes.Items(index).Cells(7).Text
                    colores(txtstatus.Text);
                    //txtbuscar.Enabled = False
                    //Estado_controles_captura(True)
                    //txtcodigoarticulo.Enabled = True
                    lblconfirmacion.Visible = Confirmacion_de_Pago();
                    btnconfirmar.Visible = lblconfirmacion.Visible;
                    btnOC.Attributes.Add("onclick", "window.open('OC_Digitales_Pendientes.aspx?idc_cliente=" + funciones.deTextoa64(txtid.Text.Trim()) + "');return false;");
                    btnOC.Enabled = true;
                    lkverdatoscliente.Attributes["onclick"] = "window.open('Ficha_cliente_m.aspx?idc_cliente=" + txtid.Text.Trim() + "&b=1'); return false;";
                    lkverdatoscliente.Enabled = true;
                    //txtcodigoarticulo.Focus()
                    etiqueta_Iva(Session["NuevoIva"] as string);
                    requiere_oc_croquis();
                    ///cargar_proyectos_cliente(txtid.Text.Trim())
                    btnnuevoprepedido.Enabled = true;
                    tbnguardarPP.Enabled = true;
                    tipo_croquis_cliente();
                    //Para la Lista de Precios cliente
                    lista_p(Convert.ToInt32(txtid.Text.Trim()));
                    Session["cedisprecios"] = rows[0]["idc_cedis"];
                    //AgregarJS()
                    estado_rd(true);
                    //RDRC.Attributes("onclick") = "RecogeCliente();"
                    //rdSF.Attributes("onclick") = "PedidoEspecial();"
                    //cboentrega.Attributes("onchange") = "return tipo_entrega(this);"

                }
                else
                {
                    WriteMsgBox("Error al Cargar Informacion del Cliente Seleccionado.");
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error al Cargar Informacion del Cliente Seleccionado. \\n \\u000B \\n" + ex.Message);
            }
        }

        protected void btnref_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = Session["rowprincipal"] as DataRow;
                DataRow dt_row = default(DataRow);
                DataTable dt = default(DataTable);
                dt = ViewState["dt"] as DataTable;
                dt_row = dt.NewRow();
                for (int i = 0; i <= row.Table.Columns.Count - 1; i++)
                {
                    dt_row[i] = row[i];
                }
                dt.Rows.Add(dt_row);
                ViewState["dt"] = dt;
                Session["dt_productos_lista"] = dt;
                promociones_cliente(1, 0);
                Productos_Calculados();
                Calcular_Valores_DataTable();
                carga_productos_seleccionadas();
                string s = Session["NuevoIva"].ToString();
                cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
                limpiar_campos();
                Estado_controles_captura(false);
                buscar_confirmacion_lista();
                formar_cadenas();
                tbnguardarPP.Enabled = true;
                btnnuevoprepedido.Enabled = true;
                Session["rowprincipal"] = null;
                //imgcancelar.Attributes.Remove("onclick")
                //imgaceptar.Attributes.Remove("onclick")
                if (!(cbomaster.Visible == true))
                {
                    txtcodigoarticulo.Attributes.Remove("onfocus");
                    txtcodigoarticulo.Text = "";
                    txtcodigoarticulo.Focus();
                    controles_busqueda_prod(true);
                    controles_busqueda_prod_sel_cancel(false);
                    cboproductos.Visible = true;
                    cboproductos.Items.Clear();
                }
                aportaciones();
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error al Refrescar Lista de Articulos. \\n \\u000B \\n Error: \\n" + ex.Message);
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_loading", "<script>myStopFunction_grid();</script>", false);
            }

        }


        public void promociones_cliente(int tipo, int idc_articulo)
        {
            //Se le Quito lo de Convenio :
            //Table de Promociones.
            try
            {
                DataTable dt = new DataTable();
                dt = Session["tx_pedido_gratis"] as DataTable;
                //Table de Articulos Capturados.
                DataTable dt_lista = new DataTable();
                dt_lista = ViewState["dt"] as DataTable;
                DataSet ds = new DataSet();
                int cantidad = 0;

                DataRow row = default(DataRow);
                if (tipo == 1)
                {
                    idc_articulo = Convert.ToInt32(dt_lista.Rows[dt_lista.Rows.Count - 1]["idc_articulo"]);
                    cantidad = Convert.ToInt32(dt_lista.Rows[dt_lista.Rows.Count - 1]["cantidad"]);
                    ds = componente.sp_promocion_articulo_cliente(idc_articulo, Convert.ToInt32(txtid.Text.Trim()), cantidad, Convert.ToInt32(txtlistap.Text.Trim()));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        eliminar_promocion(Convert.ToInt32(ds.Tables[0].Rows[0]["idc_promocion"]));
                        //porque esta esto aqui MIC 01-06-2015

                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {

                            row = dt.NewRow();
                            row["idc_articulo"] = ds.Tables[0].Rows[i]["idc_articulo"];
                            row["cantidad"] = ds.Tables[0].Rows[i]["cantidad"];
                            row["codigo"] = ds.Tables[0].Rows[i]["codigo"];
                            row["unimed"] = ds.Tables[0].Rows[i]["unimed"];
                            row["desart"] = ds.Tables[0].Rows[i]["desart"];
                            row["idc_promociond"] = ds.Tables[0].Rows[i]["idc_promociond"];
                            row["idc_promocion"] = ds.Tables[0].Rows[i]["idc_promocion"];
                            dt.Rows.Add(row);
                        }
                        dt_lista.Rows[dt_lista.Rows.Count - 1]["idc_promocion"] = ds.Tables[0].Rows[0]["idc_promocion"];
                    }
                }
                else if (tipo == 2)
                {
                    DataRow[] rows2 = null;
                    rows2 = dt_lista.Select("idc_articulo=" + idc_articulo);
                    if (rows2[0]["idc_promocion"].ToString()=="")
                    {
                        rows2[0]["idc_promocion"]= 0;
                    }
                    if (rows2.Length > 0)
                    {
                        cantidad = Convert.ToInt32(rows2[0]["cantidad"]);
                        ds = componente.sp_promocion_articulo_cliente(idc_articulo, Convert.ToInt32(txtid.Text.Trim()), cantidad, Convert.ToInt32(txtlistap.Text.Trim()));

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToInt32(rows2[0]["idc_promocion"]) > 0)
                            {
                                eliminar_promocion(Convert.ToInt32(ds.Tables[0].Rows[0]["idc_promocion"]));

                                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                                {
                                    row = dt.NewRow();
                                    row["idc_articulo"] = ds.Tables[0].Rows[i]["idc_articulo"];
                                    row["cantidad"] = ds.Tables[0].Rows[i]["cantidad"];
                                    row["codigo"] = ds.Tables[0].Rows[i]["codigo"];
                                    row["unimed"] = ds.Tables[0].Rows[i]["unimed"];
                                    row["desart"] = ds.Tables[0].Rows[i]["desart"];
                                    row["idc_promociond"] = ds.Tables[0].Rows[i]["idc_promociond"];
                                    row["idc_promocion"] = ds.Tables[0].Rows[i]["idc_promocion"];
                                    dt.Rows.Add(row);
                                }



                            }
                            else
                            {
                                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                                {
                                    row = dt.NewRow();
                                    row["idc_articulo"] = ds.Tables[0].Rows[i]["idc_articulo"];
                                    row["cantidad"] = ds.Tables[0].Rows[i]["cantidad"];
                                    row["codigo"] = ds.Tables[0].Rows[i]["codigo"];
                                    row["unimed"] = ds.Tables[0].Rows[i]["unimed"];
                                    row["desart"] = ds.Tables[0].Rows[i]["desart"];
                                    row["idc_promociond"] = ds.Tables[0].Rows[i]["idc_promociond"];
                                    row["idc_promocion"] = ds.Tables[0].Rows[i]["idc_promocion"];
                                    dt.Rows.Add(row);
                                }


                                for (int i = 0; i <= dt_lista.Rows.Count - 1; i++)
                                {
                                    if (dt_lista.Rows[i]["idc_promocion"].ToString() == idc_articulo.ToString())
                                    {
                                        //dt_lista.Rows[i]["idc_promocion") = ds.Tables(0).Rows(0).Item("idc_promociond")
                                        dt_lista.Rows[i]["idc_promocion"] = ds.Tables[0].Rows[0]["idc_promocion"];
                                        //modificada MIC 03-06-2015
                                        break; // TODO: might not be correct. Was : Exit For
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (rows2[0]["idc_promocion"].ToString() !="")
                            {
                                if (Convert.ToInt32(rows2[0]["idc_promocion"]) > 0)
                                {
                                    eliminar_promocion(Convert.ToInt32(rows2[0]["idc_promocion"]));
                                    for (int i = 0; i <= dt_lista.Rows.Count - 1; i++)
                                    {
                                        //if nuevo 02-06-2015 MIC
                                        if (dt_lista.Rows[i]["idc_promocion"].ToString() != "")
                                        {
                                            if (dt_lista.Rows[i]["idc_promocion"].ToString()== rows2[0]["idc_promocion"].ToString())
                                            {
                                                dt_lista.Rows[i]["idc_promocion"] = 0;
                                                break; // TODO: might not be correct. Was : Exit For
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    imgpromocion.Attributes["onclick"] = "window.open('productos_g.aspx');";
                    imgpromocion.Attributes["style"] = "display:inline;";
                }
                else
                {
                    imgpromocion.Attributes.Remove("onclick");
                    imgpromocion.Attributes["style"] = "display:none;";
                }

                //Table de Promociones
                Session["tx_pedido_gratis"] = dt;


                //Table de Articulos Capturados.
                ViewState["dt"] = dt_lista;
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error en promociones clientes. \\n \\u000B \\n Error: \\n" + ex.Message);
            }
        }

        protected void btnvalidaChP_Click(object sender, EventArgs e)
        {
            ValidarCHP(false);
        }

        public bool ValidarCHP(bool agregar)
        {
            bool ret = false;
            string resultado = null;
            try
            {
                if (txtfolioCHP.Text != "0" & !string.IsNullOrEmpty(txttotal.Text.Trim()))
                {
                    DataSet ds = componente.sp_comprobar_chekplus_PRE(Convert.ToInt32(txtfolioCHP.Text.Trim()), Convert.ToInt32(txttotal.Text.Trim()), Convert.ToInt32(txtid.Text.Trim()));
                    resultado = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (!string.IsNullOrEmpty(resultado))
                    {
                        //CargarMsgbox("", resultado, False, 1)  ' Valida el check Plus y muestra msj en caso de que lo contenga.
                        WriteMsgBox(resultado);
                        txtfolioCHP.Text = (agregar == true ? txtfolioCHP.Text : "");
                        txtfolioCHP.Focus();
                        ret = false;
                    }
                    else
                    {
                        //Agregar el Check Plus a la lista de productos, y hacer los calculos necesarios.
                        if (agregar == true)
                        {
                            Agregar_CheckPlus();
                        }
                        else
                        {
                            WriteMsgBox("No. Preautorizacion Correcto.");
                        }
                        ret = true;
                    }
                }
                return ret;

            }
            catch (Exception ex)
            {
                WriteMsgBox("Error al Validar Check Plus. \\n \\u000B \\n Error: \\n" + ex.Message);
                return false;
            }
        }

        public void Agregar_CheckPlus()
        {
            if (buscar_articulos_duplicados(4406) == true)
            {
                //True cuando ya este agregado el cargo por pago con cheque...y termina el ciclo.
                return;
            }
            else
            {
                string codigo = null;
                DataTable dt2 = new DataTable();
                dt2 = componente.sp_codigo_articulo(4406).Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    codigo = dt2.Rows[0][0].ToString();
                }
                else
                {
                    return;
                }
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = ViewState["dt"] as DataTable;
                DataRow row = default(DataRow);
                DataRow rowdt = dt.NewRow();
                try
                {
                    ds = componente.sp_buscar_articulo_VENTAS_existencias(codigo, "A", Convert.ToInt32(Session["idc_sucursal"]),Convert.ToInt32(Session["idc_usuario"]));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //cargar_datos_productob_x_codigo(ds.Tables(0).Rows(0))
                        row = ds.Tables[0].Rows[0];
                        rowdt["idc_articulo"] = row["idc_articulo"];
                        rowdt["Codigo"] = row["codigo"];
                        rowdt["Descripcion"] = row["desart"];
                        rowdt["UM"] = row["unimed"];
                        rowdt["Decimales"] = row["decimales"];
                        rowdt["Paquete"] = row["paquete"];
                        rowdt["precio_libre"] = row["precio_libre"];
                        rowdt["comercial"] = row["comercial"];
                        rowdt["fecha"] = row["fecha"];
                        rowdt["obscotiza"] = row["obscotiza"];
                        rowdt["vende_exis"] = row["vende_exis"];
                        rowdt["minimo_venta"] = row["minimo_venta"];
                        rowdt["calculado"] = true;
                        rowdt["porcentaje"] = calculado(Convert.ToInt32(row["idc_articulo"]));
                        rowdt["Anticipo"] = row["Anticipo"];
                        //rowdt["Precio"] = Redondeo_cuatro_decimales(txtmaniobras.Text.Trim())
                        //rowdt["Importe"] = Redondeo_Dos_Decimales(txtmaniobras.Text.Trim())
                        //rowdt["PrecioReal"] = Redondeo_cuatro_decimales(txtmaniobras.Text.Trim())
                        rowdt["nota_credito"] = false;
                        //rowdt("descuento") = Redondeo_cuatro_decimales("0.00")
                        rowdt["Cantidad"] = 1;
                        dt.Rows.InsertAt(rowdt, dt.Rows.Count);
                        ViewState["dt"] = dt;
                        Productos_Calculados();
                        Calcular_Valores_DataTable();
                        cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
                        carga_productos_seleccionadas();
                    }
                }
                catch (Exception ex)
                {
                    WriteMsgBox(ex.ToString());
                }
            }
        }

        protected void btneditar_art_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, typeof(Page), "editar_articulo", "editar_precios_cantidad_1(" + txtidc_articulo.Text.Trim() + ");", true);
        }

        protected void btnguardar_edit_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["dt"] = Session["dt_productos_lista"];
                promociones_cliente(2, Convert.ToInt32(txtidc_articulo.Text.Trim()));
                Productos_Calculados();
                Calcular_Valores_DataTable();
                carga_productos_seleccionadas();
                cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
                limpiar_campos();
                Estado_controles_captura(false);
                buscar_confirmacion_lista();
                formar_cadenas();
                tbnguardarPP.Enabled = true;
                btnnuevoprepedido.Enabled = true;
                aportaciones();
                if (!(cbomaster.Visible == true))
                {
                    txtcodigoarticulo.Attributes.Remove("onfocus");
                    txtcodigoarticulo.Text = "";
                    txtcodigoarticulo.Focus();
                    controles_busqueda_prod(true);
                    controles_busqueda_prod_sel_cancel(false);
                }
            }
            catch (Exception ex)
            {
                WriteMsgBox("Error al Actualizar Lista de Articulos. \\n \\u000B \\n Error: \\n" + ex.Message);
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_loading2", "<script>myStopFunction_grid();</script>", false);
            }

        }

        protected void btncancelar_edit_Click(object sender, EventArgs e)
        {
            if (!(cbomaster.Visible == true))
            {
                txtcodigoarticulo.Attributes.Remove("onfocus");
                txtcodigoarticulo.Text = "";
                txtcodigoarticulo.Focus();
                controles_busqueda_prod(true);
                controles_busqueda_prod_sel_cancel(false);
            }
        }

        protected void cboentrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(cboentrega.SelectedValue))
            {
                case 1:
                    btnconsignado.Enabled = true;
                    break;
                case 3:
                    btnconsignado.Enabled = true;
                    limpiar_pedido_especial();
                    break;
                case 2:
                    btnconsignado.Enabled = false;
                    limpiar_recoge_cliente();
                    limpiar_pedido_especial();
                    limpiar_datos_consignado();
                    break;
                case 4:
                    limpiar_recoge_cliente();
                    btnconsignado.Enabled = true;
                    break;
                default:
                    WriteMsgBox("¿Que Fregados Estas Haciendo? \\n \\u000B \\n ¿Que Seleccionaste?");
                    break;
            }
        }

        protected void btnmaster_Click(object sender, EventArgs e)
        {
            prep_cargar_grid_prod_master_cliente(Convert.ToInt32(txtid.Text));
            controles_busqueda_master(true);
            controles_busqueda_prod(false);
            controles_busqueda_prod_sel_cancel(false);

        }
        protected void btn_seleccionar_master_Click1(object sender, EventArgs e)
        {
            if (cbomaster.Items.Count <= 0)
            {
                return;
            }
            dynamic dt = default(DataTable);
            DataTable dt2 = new DataTable();
            dt = ViewState["dt_master_cotizacion"] as DataTable;
            DataRow[] rows = null;
            string sel = "";



            sel = "idc_articulo=" + cbomaster.SelectedValue.ToString();

            dt2 = ViewState["dt"] as DataTable;
            rows = dt2.Select(sel);

            if (rows.Length > 0)
            {
                WriteMsgBox("El Articulo Seleccionado Ya Se Encuentra En La Lista.");
                return;
            }

            rows = dt.Select(sel);
            if (rows.Length > 0)
            {
                DataRow rowprincipal = default(DataRow);
                rowprincipal = dt.NewRow();
                rowprincipal["idc_articulo"] = rows[0]["idc_articulo"];
                rowprincipal["Codigo"] = rows[0]["codigo"];
                rowprincipal["Descripcion"] = rows[0]["descripcion"];
                rowprincipal["UM"] = rows[0]["um"];
                rowprincipal["Decimales"] = rows[0]["decimales"];
                rowprincipal["Paquete"] = rows[0]["paquete"];
                rowprincipal["precio_libre"] = rows[0]["precio_libre"];
                rowprincipal["comercial"] = rows[0]["comercial"];
                rowprincipal["vende_exis"] = rows[0]["vende_exis"];
                rowprincipal["minimo_venta"] = rows[0]["minimo_venta"];
                rowprincipal["Master"] = rows[0]["master"];
                rowprincipal["fecha"] = rows[0]["fecha"];
                rowprincipal["obscotiza"] = rows[0]["obscotiza"];
                rowprincipal["mensaje"] = rows[0]["mensaje"];
                rowprincipal["existencia"] = buscar_Existencia_Articulo(Convert.ToInt32(rowprincipal["idc_articulo"]));

                if (calculado(Convert.ToInt32(rowprincipal["idc_articulo"])) == 0)
                {
                    rowprincipal["calculado"] = false;
                    rowprincipal["porcentaje"] = 0.0;
                    Session["rowprincipal"] = rowprincipal;
                    buscar_precio_producto(Convert.ToInt32(rows[0]["idc_articulo"]));
                    txtcodigoarticulo.Enabled = false;
                    Estado_controles_captura(true);
                    Session["rowprincipal"] = rowprincipal;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "selecciona_mast", "<script>editar_precios_cantidad();</script>", false);
                }
                else
                {
                    rowprincipal["precio"] = Redondeo_cuatro_decimales(0.0);
                    rowprincipal["cantidad"] = 1;
                    rowprincipal["precioreal"] = Redondeo_cuatro_decimales(Convert.ToDouble(rowprincipal["precio"]));
                    rowprincipal["calculado"] = true;
                    rowprincipal["porcentaje"] = calculado(Convert.ToInt32(rowprincipal["idc_articulo"]));
                    rowprincipal["nota_credito"] = false;
                    dt.Rows.Add(rowprincipal);
                    ViewState["dt"] = dt;
                    rowprincipal = null;
                    Estado_controles_captura(false);
                    controles_busqueda_prod(true);
                    controles_busqueda_prod_sel_cancel(false);
                    txtcodigoarticulo.Enabled = true;
                    cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
                    txtcodigoarticulo.Attributes.Remove("onfocus");
                    txtcodigoarticulo.Focus();
                    formar_cadenas();
                    controles_busqueda_prod(true);
                    controles_busqueda_prod_sel_cancel(false);
                    grdproductos2.DataSource = ViewState["dt"] as DataTable;
                    grdproductos2.DataBind();
                    tbnguardarPP.Enabled = true;
                    btnnuevoprepedido.Enabled = true;
                }
            }
            else
            {
                WriteMsgBox("Error al Cargar Informacion del Producto Seleccionado.");
                return;
            }
        }

        protected void btnbuscar_codigo_Click1(object sender, EventArgs e)
        {
            controles_busqueda_master(false);
            controles_busqueda_prod(true);
            txtcodigoarticulo.Enabled = true;
            txtcodigoarticulo.Attributes.Remove("onfocus");
            txtcodigoarticulo.Focus();
            cboproductos.Items.Clear();
            cboproductos.Visible = true;
            btn_seleccionar_master.Visible = true;
            btn_seleccionar_master.Attributes["onclick"] = "return editar_precios_cantidad(1);";
        }

        public void colores_clear()
        {
            txtnombre.Attributes.Remove("style");
            txtstatus.Attributes.Remove("style");
        }

        protected void btncargarflete_Click(object sender, EventArgs e)
        {
            Agregar_maniobras();
            formar_cadenas();
            WriteMsgBox("Se Cargo Monto del Flete Correctamente. \\n \\u000B \\n Para Continuar de Click en Generar Pre-Pedido.");
        }

        protected void btnref_especif_Click(object sender, EventArgs e)
        {
            if ((Session["dt_productos_lista"] != null))
            {
                try
                {
                    ViewState["dt"] = Session["dt_productos_lista"];
                    Productos_Calculados();
                    Calcular_Valores_DataTable();
                    carga_productos_seleccionadas();
                    cargar_subtotal_iva_total(Convert.ToInt32(Session["NuevoIva"]));
                    limpiar_campos();
                    Estado_controles_captura(false);
                    buscar_confirmacion_lista();
                    formar_cadenas();
                    tbnguardarPP.Enabled = true;
                    btnnuevoprepedido.Enabled = true;
                    aportaciones();
                    if (!(cbomaster.Visible == true))
                    {
                        txtcodigoarticulo.Attributes.Remove("onfocus");
                        txtcodigoarticulo.Text = "";
                        txtcodigoarticulo.Focus();
                        controles_busqueda_prod(true);
                        controles_busqueda_prod_sel_cancel(false);
                    }
                }
                catch (Exception ex)
                {
                    WriteMsgBox("Error al Actualizar Lista de Articulos. \\n \\u000B \\n Error: \\n" + ex.Message);
                }
                finally
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_loading2", "<script>myStopFunction_grid();</script>", false);
                }
            }

        }

        protected void tbnguardarPP_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    if (txtstatus.Text == "4")
                    {
                        WriteMsgBox("El Cliente esta Bloqueado Por Cheques Devueltos.");
                        return;
                    }
                    int sipasa2 = 0;
                    string vrecoge = "";
                    int idccboentrega = Convert.ToInt32(cboentrega.SelectedValue);
                    //Si es una entrega a Almacén.
                    if (idccboentrega == 1)
                    {
                        if (validar_campos_direccion() == true)
                        {
                            if (txtmaniobras.Text != "0.00" & txtFolio.Text == "-1")
                            {
                                sipasa2 = 1;
                            }
                            else
                            {
                                sipasa2 = 0;
                            }


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                            return;

                        }
                    }
                    else if (idccboentrega == 2)
                    {
                    }
                    else if (idccboentrega == 3)
                    {
                        if (string.IsNullOrEmpty(txtnombrerecoge.Text) | string.IsNullOrEmpty(txtpaternor.Text) | txtsucursalr.Text == "0")
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>alert('Falta Completar Datos de Donde va Recoger el Cliente.'); </script>", false);

                            ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                            return;
                        }
                        else
                        {
                            vrecoge = txtnombrerecoge.Text.Trim() + " " + txtpaternor.Text.Trim() + " " + txtmaternor.Text.Trim();
                        }
                        // Validar que los campos tengan datos...("campo otro cuando este seleccionado el RDOtro.")
                    }
                    else if (idccboentrega == 4)
                    {
                        if (!string.IsNullOrEmpty(txtformaP.Text) & txtformaP.Text.Trim() == "1")
                        {
                            if (string.IsNullOrEmpty(txtplazo.Text.Trim()) | string.IsNullOrEmpty(txttelefono.Text.Trim())
                                | string.IsNullOrEmpty(txtcontacto.Text.Trim()) | string.IsNullOrEmpty(txtcminima.Text.Trim()))
                            {
                                WriteMsgBox("Falta Completar Datos del Pre-Pedido Especial.");
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>alert('Falta Completar Datos del Pre-Pedido Especial.'); </script>", false);
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrarpagina", "<script>PedidoEspecial();</script>", false);
                                //Llamar la pantalla de folios.
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                                return;
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtplazo.Text.Trim()) | string.IsNullOrEmpty(txttelefono.Text.Trim())
                                | string.IsNullOrEmpty(txtcontacto.Text.Trim()) | string.IsNullOrEmpty(txtcminima.Text.Trim()) | string.IsNullOrEmpty(txtotro.Text.Trim()))
                            {
                                WriteMsgBox("Falta Completar Datos del Pre-Pedido Especial.");
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>alert('Falta Completar Datos del Pre-Pedido Especial.'); </script>", false);
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrarpagina", "<script>PedidoEspecial();</script>", false);
                                //Llamar la pantalla de folios.
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                                return;
                            }
                        }
                    }
                    string mensaje = "";
                    string mensaje_bd = string.Empty;
                    if (!string.IsNullOrEmpty(oc.Text))
                    {
                        //Si la orden de compra es obligatoria para realiza el pedido.
                        if (Convert.ToBoolean(oc.Text) == true)
                        {
                            //Si no hay un numero de OC
                            if (txtnumeroOC.Text == null)
                            {
                                mensaje = "* Es Requerido Ingresar la Orden de Compra del Cliente, Es necesario tambien Anexar la O.C. \\n";
                                mensaje_bd = "* Es Requerido Ingresar la Orden de Compra del Cliente, Es necesario tambien Anexar la O.C.";
                                //Si se capturo un numero de OC
                            }
                            else
                            {
                                //Valida que el num de OC aun este pendiente("False").
                                if (validar_occli() == false)
                                {
                                    WriteMsgBox("* La Orden de Compra YA NO ESTA PENDIENTE, Verifica el Numero de la Orden de Compra.");
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                                    return;
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(croquis.Text))
                    {
                        //Si el croquis es neceseario para el pedido.
                        if (Convert.ToBoolean(croquis.Text) == true)
                        {
                            if (string.IsNullOrEmpty(lblruta.Text))
                            {
                                mensaje = mensaje + "* Es Requerido Anexar el Croquis. \\n";
                                mensaje_bd = mensaje_bd + "Es Requerido Anexar el Croquis.";
                            }
                        }
                    }

                    //Validar Check-Plus
                    if (!string.IsNullOrEmpty(txtfolioCHP.Text))
                    {
                        if (ValidarCHP(true) == false)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                            return;
                        }
                    }

                    //-------------------------------------------------------------------------------------------

                    int ventdir = 0;
                    if (idccboentrega == 1)
                    {
                        ventdir = 1;
                    }
                    else if (idccboentrega == 2)
                    {
                        ventdir = 2;
                    }
                    else if (idccboentrega == 3)
                    {
                        ventdir = 3;
                    }
                    else
                    {
                        ventdir = 4;
                    }


                    //Crear las Cadenas
                    DataTable dt = new DataTable();
                    dt = ViewState["dt"] as DataTable;
                    if (!(dt.Rows.Count > 0))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                        return;
                    }
                    string vcadena_check = "";
                    string vdarti = "";
                    dynamic vdarti_nueva = "";
                    string vdarti2 = "";
                    dynamic vdarti2_nueva = "";
                    int vtotal = 0;
                    string vartiuv = "";
                    double vprer = 0;
                    double vprecio = 0;
                    string vid = "0";
                    string vcadenapeso = "";
                    string vcantidad = "";
                    double vexistencia = 0;
                    string vcodigo = "";
                    string vdescart = "";
                    string vsin_exis = "";
                    string vunimed = "";
                    double vpre = 0;
                    double vcosto = 0;
                    DataRow row = default(DataRow);
                    bool vcomercial = false;
                    //Dim v_IVA As Double
                    string vcosbajo = "";
                    double vztotal = 0;
                    int num_especif = 0;
                    string especif = "";
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        row = dt.Rows[i];
                        vtotal = vtotal + 1;
                        vdescart = row["Descripcion"].ToString();
                        vunimed = row["UM"].ToString();
                        vid = row["idc_articulo"].ToString();
                        vcantidad = row["cantidad"].ToString();
                        vprecio = Convert.ToDouble(row["precio"]);
                        vcosto = row["costo"].ToString() == "" ? 0 : Convert.ToDouble(row["costo"]);
                        vexistencia = row["existencia"].ToString() == "" ? 0 : Convert.ToDouble(row["existencia"]);
                        vcodigo = row["codigo"].ToString();
                        vcadenapeso = vcadenapeso + vcadenapeso + vid.ToString() + ";" + vcantidad.ToString() + ";";
                        vcomercial = Convert.ToBoolean(row["comercial"]);
                        vprer = Convert.ToDouble(row["PrecioReal"]);
                        vpre = Convert.ToDouble(row["precio"]);
                        especif = row["g_especif"].ToString() == "" ? "" : row["g_especif"].ToString();
                        num_especif = row["num_especif"].ToString() == "" ? 0 : Convert.ToInt32(row["num_especif"]);

                        //**** Aviso Material sin existencia...

                        if (vexistencia < Convert.ToDouble(vcantidad) & vcomercial == true)
                        {
                            vsin_exis = vsin_exis + vcodigo + "  " + (vdescart.Length > 40 ? vdescart.Substring(0, 40) : vdescart) + "  " +
                               (vunimed.Length > 3 ? vunimed.Substring(0, 3) : vunimed)
                                + "  " + Convert.ToString(vcantidad);
                        }

                        //--------------------------------------------


                        //****Verificar si el precio es muy bajo
                        if (txtrfc.Text.StartsWith("*"))
                        {
                            if (Convert.ToDecimal(Session["NuevoIva"]) != 0)
                            {
                                //v_IVA = Session["NuevoIva"]
                                vprecio = Convert.ToDouble(Convert.ToDecimal(row["Precio"]) / (1 + (Convert.ToDecimal(Session["NuevoIva"]) / 100)));
                            }
                            else
                            {
                                vprecio = Convert.ToDouble(Convert.ToDecimal(row["Precio"]) / (1 + (Convert.ToDecimal(Session["xiva"]) / 100)));
                            }
                        }
                        else
                        {
                            vprecio = Convert.ToDouble(row["Precio"]);
                        }


                        double vporciento = (((vprecio - vcosto) / vprecio) * 100);
                        if (vporciento < 5)
                        {
                            vcosbajo = vcosbajo + vcodigo + "  " + (vdescart.Length > 40 ? vdescart.Substring(0, 40) : vdescart) + "  " +
                                (vunimed.Length > 3 ? vunimed.Substring(0, 3) : vunimed) + System.Environment.NewLine + "Precio:" + Convert.ToString(vprecio)
                                + "Costo:" + vcosto.ToString() + "  " + vporciento.ToString()
                                + "%" + System.Environment.NewLine + System.Environment.NewLine;
                        }
                        //
                        //If txtrfc.Text.StartsWith("*") Then
                        //    vdarti = vdarti + Trim(CStr(vid)) + ";" + Trim(CStr(vcantidad)) + ";" + Trim(CStr(vprecio)) + ";" + Trim(CStr(vprer)) + ";"
                        //Else
                        vdarti = vdarti + vid.ToString() + ";" +
                            vcantidad.ToString() + ";" +
                            vpre.ToString() + ";" +
                            vprer.ToString() + ";";

                        //End If

                        vdarti_nueva = vdarti_nueva +
                            vid.ToString() + ";" +
                            vcantidad.ToString() + ";" +
                            vpre.ToString() + ";" +
                            vprer.ToString() + ";0;" +
                            especif.Replace(";", "&") + ";" + num_especif.ToString() + ";";


                        vdarti2 = vdarti2 +
                           vid.ToString() + ";" +
                            vcantidad.ToString() + ";" +
                            vpre.ToString() + ";";
                        vdarti2_nueva = vdarti2_nueva +
                            vid.ToString() + ";" +
                            vcantidad.ToString() + ";" +
                            vpre.ToString() + ";0;" +
                            especif.Replace(";", "&") + ";" + num_especif + ";";
                        //vdarti2_nueva = vdarti2_nueva + ALLTRIM(Str(vid)) + ";" + ALLTRIM(Str(vcan, 14, 4)) + ";" + ALLTRIM(Str(vpre, 14, 4)) + ";0;"

                        vcadena_check = vcadena_check +
                           vid.ToString() + ";" +
                            vdescart.ToString() + ";" +
                            vcantidad.ToString() + ";" +
                            vpre.ToString() + ";";

                        vartiuv = vartiuv + vid.ToString() + ";";

                    }
                    //-------------------------------------------------------------------------------------------


                    dynamic vdarti_prom = "";
                    dynamic vnum_prom = 0;
                    dynamic VTOTA_NUEVO = 0;
                    DataTable tx_pedido_gratis = new DataTable();
                    tx_pedido_gratis = Session["tx_pedido_gratis"] as DataTable;

                    if ((tx_pedido_gratis != null))
                    {
                        if (tx_pedido_gratis.Rows.Count > 0)
                        {
                            for (int i = 0; i <= tx_pedido_gratis.Rows.Count - 1; i++)
                            {
                                vdarti_nueva = vdarti_nueva +
                                   Convert.ToString(tx_pedido_gratis.Rows[i]["idc_articulo"]) + ";" +
                                    Convert.ToString(tx_pedido_gratis.Rows[i]["cantidad"]) + ";0;0;" +
                                    Convert.ToString(tx_pedido_gratis.Rows[i]["idc_promociond"]) + ";" + ";" + "0;";

                                vdarti2_nueva = vdarti2_nueva +
                                    Convert.ToString(tx_pedido_gratis.Rows[i]["idc_articulo"]) + ";" +
                                    Convert.ToString(tx_pedido_gratis.Rows[i]["cantidad"]) + ";0;" +
                                    Convert.ToString(tx_pedido_gratis.Rows[i]["idc_promociond"]) + ";" + ";" + "0;";

                                VTOTA_NUEVO = VTOTA_NUEVO + 1;
                            }
                        }
                    }
                    //****Articulos con limite de venta...
                    //'Continuar aki, el Metodo esta abajito :))))

                    if (articulos_limite_de_venta(vcadenapeso, Convert.ToInt32(txtid.Text.Trim()), vtotal, Convert.ToInt32(Session["xidc_almacen"])) == false)
                    {
                        WriteMsgBox("Capturaste Articulos que ya NO estan Permitidos para Venta.");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                        return;
                    }
                    //-------------------------------------------------------------------------------------------
                    dynamic vziva = Session["NuevoIva"];
                    vztotal = (txtrfc.Text.Trim().StartsWith("*") ? Convert.ToDouble(txtsubtotal.Text.Trim()) / (1 + vziva / 100) : Convert.ToDouble(txtsubtotal.Text.Trim()));

                    //******Entrega Directa******
                    if (articulos_entrega_directa(vcadenapeso, Convert.ToInt32(txtid.Text.Trim()), vtotal) == true &
                        !(cboentrega.SelectedValue == "2") & vztotal < 5000 & string.IsNullOrEmpty(txtFolioOc.Text))
                    {
                        mensaje = mensaje + "* Hay Articulos que Tienen la Condición de Entrega Directa de Proveedor y el Monto Minimo es de $5,000 pesos. \\n";
                        mensaje_bd = mensaje_bd + "* Hay Articulos que Tienen la Condición de Entrega Directa de Proveedor y el Monto Minimo es de $5,000 pesos.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                        return;
                    }
                    //-------------------------------------------------------------------------------------------

                    //******Monto Minimo de Ventas*******
                    double monto_minimo = Monto_Minimo_Venta(Convert.ToInt32(txtid.Text.Trim()));
                    double capacidad = 0;
                    if (monto_minimo < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                        return;
                    }
                    if (vztotal < monto_minimo & cboentrega.SelectedValue == "1")
                    {
                        capacidad = volumen_carga(vcadenapeso, 11, vtotal);
                        if (capacidad == null)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                            return;
                        }
                        if (capacidad < 90)
                        {
                            //CargarMsgbox("", "", False, 1)
                            mensaje = mensaje + "* El Monto Minimo de Venta para este Cliente es de: " + Formato_moneda(monto_minimo) + "Pesos + I.V.A. o bien carga completa. \\n";
                            mensaje_bd = mensaje_bd + "* El Monto Minimo de Venta para este Cliente es de: " + Formato_moneda(monto_minimo) + "Pesos + I.V.A. o bien carga completa.";
                        }
                    }
                    //-------------------------------------------------------------------------------------------




                    //***************
                    string vtipom = "A";
                    string vcambios = "";
                    bool vdesiva = (txtrfc.Text.Trim().StartsWith("*") ? false : true);
                    System.DateTime vFechaEntrega = Convert.ToDateTime(cbofechas.SelectedValue);
                    string vocc = txtnumeroOC.Text.Trim();
                    int vidpro = 0;
                    bool vpro = false;

                    if (Convert.ToDecimal(txtproy.Text.Trim()) > 0)
                    {
                        vidpro = Convert.ToInt32(txtproy.Text.Trim());
                        // idc_proyeco
                        vpro = true;
                    }
                    else
                    {
                        vidpro = 0;
                        vpro = false;
                    }


                    int idc_colonia = Convert.ToInt32(txtidc_colonia.Text.Trim());
                    string calle = txtcalle.Text.Trim();
                    string numero = txtnumero.Text.Trim();
                    string cp = txtCP.Text.Trim();
                    string observaciones = txtobservaciones.Text.Trim();
                    int folioCHP = 0;
                    if (!string.IsNullOrEmpty(txtfolioCHP.Text))
                    {
                        folioCHP = Convert.ToInt32(txtfolioCHP.Text.Trim());
                    }
                    bool direntrega = (Convert.ToInt16(cboentrega.SelectedValue) == 1 ? true : false);
                    //rdEA.Checked
                    bool bitocc = (!string.IsNullOrEmpty(txtnumeroOC.Text.Trim()) ? true : false);
                    bool bitcroquis = (!string.IsNullOrEmpty(lblruta.Text.Trim()) ? true : false);
                    bool bitllamada = (!string.IsNullOrEmpty(lblllamada.Text.Trim()) ? true : false);

                    if (vidpro == 0 & Convert.ToInt16(cboentrega.SelectedValue) == 1)
                    {
                        bool vfalta = false;
                        if (calle.Trim() == null)
                        {
                            vfalta = true;
                        }
                        if (bitocc == false)
                        {
                            //vfalta = True
                        }
                        if (idc_colonia == null)
                        {
                            vfalta = true;
                        }
                        if (vfalta == true)
                        {
                            WriteMsgBox("Faltan de Completar Datos en el Consignado...Es Obligatorio");
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                            return;
                        }
                    }
                    //-------------------------------------------------------------------------------------------
                    //'Poner Aki Articulo de UnicaVenta
                    //'///////////////////////////////////////////////////////////////////////////////////////////////
                    //'///////////////////////////////////////////////////////////////////////////////////////////////
                    //'///////////////////////////////////////////////////////////////////////////////////////////////
                    //'///////////////////////////////////////////////////////////////////////////////////////////////
                    bool unicaventa = false;
                    unicaventa = Unica_Venta(vartiuv, vtotal);
                    if (unicaventa == false)
                    {
                        WriteMsgBox("Capturaste un Producto de Unica Venta, Que ya Fue Utilizado en Otra Venta");
                        return;
                    }
                    //-------------------------------------------------------------------------------------------


                    //**Verfificar Limite....
                    bool limite = Verificar_Limite(Convert.ToInt32(txtid.Text.Trim()), Convert.ToDouble(txttotal.Text.Trim()));
                    bool pasa_limite = false;
                    if (limite == null)
                    {
                        pasa_limite = true;
                        // True si SobrePasa el limite.
                        //CargarMsgbox("", "El monto de este Pedido Sobrepasa el Limite de Credito Permitido para el Cliente <BR/> No se Puede Generar el Pre-Pedido...Verificalo con Creditos...", False, 1)
                        //Return
                    }
                    else if (limite == false)
                    {
                        pasa_limite = true;
                        // True si SobrePasa el limite.
                        //CargarMsgbox("", "El monto de este Pedido Sobrepasa el Limite de Credito Permitido para el Cliente <BR/> No se Puede Generar el Pre-Pedido...Verificalo con Creditos...", False, 1)
                        //Return
                    }
                    else
                    {
                        pasa_limite = false;
                        //False si no Sobrepasa el limite.
                    }
                    //-------------------------------------------------------------------------------------------


                    //**Checar donde va entrar
                    int vidc_listap = Convert.ToInt32(txtlistap.Text.Trim());
                    bool vcambio_lista = (Convert.ToInt32(cboentrega.SelectedValue) == 2 ? false : true);


                    int ventrar = 0;
                    bool camb_precios = false;
                    //camb_precios = Cambios_Precios(vdarti, vtotal, txtid.Text.Trim(), Session["pxidc_sucursal"], vcambio_lista)
                    camb_precios = false;
                    //If camb_precios =   Then
                    //    CargarMsgbox("", "No se procedio a Verificar si el Cliente esta Bloqueado", False, 1)
                    //    Return
                    //End If
                    if (camb_precios == true)
                    {
                        ventrar = 1;
                    }
                    else
                    {
                        bool vblo = Cliente_Bloqueado(Convert.ToInt32(txtid.Text.Trim()));
                        //Aquiiiii le falta "Pasa"
                        if ((vblo == true | pasa_limite == true) & string.IsNullOrEmpty(txtfolioCHP.Text))
                        {
                            ventrar = 2;
                        }
                        else
                        {
                            ventrar = 3;
                        }
                    }
                    //-------------------------------------------------------------------------------------------


                    //'
                    if (Convert.ToInt16(cboentrega.SelectedValue) == 4)
                    {
                        ventrar = 4;
                    }
                    int sipasa = 0;
                    // And txtFolioOc.Text = "" 
                    if (!string.IsNullOrEmpty(mensaje))
                    {
                        //CargarMsgbox("", "El Pedido Tiene Varios Detalles: <BR/>" & mensaje & "<BR/><strong>Completa la Información Necesaria o Pide un Folio de Autorización</strong> <BR/> ¿Deseas Capturar el Folio de Autorización?", True, 1)
                        WriteMsgBox("El Pedido Tiene Varios Detalles: \\n \\u000B \\n" + mensaje + "\\n \\u000B \\n El Pre-Pedido Requerira Folio de Autorización.");
                        //Session["FolioOC"] = True
                        //Session["cargarFlete"] = False
                        //Session["TipoAutorizacion"] = IIf(ventrar = 3, 4, 15)
                        //ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                        //Return
                        sipasa = 1;
                        txtFolioOc.Text = "1";
                    }
                    else
                    {
                        sipasa = 0;
                    }
                    //If txtFolioOc.Text <> "" Then
                    //    sipasa = txtFolioOc.Text
                    //End If
                    if (ventrar <= 4 & (string.IsNullOrEmpty(txtformapago.Text) & (string.IsNullOrEmpty(txtfolioCHP.Text) | txtfolioCHP.Text == "0")))
                    {
                        if (lblconfirmacion.Visible == true)
                        {
                            //ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>window.open('confirmacion_de_pago_mobile.aspx','null', 'width=360px,height=308px,left=300,top=250,Menubar=no,Scrollbars=no,location=no'); </script>", False)
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>alert('Es Necesario Confirmar Pago.')</script>", false);
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                            return;
                        }
                    }

                    string[] formapago = null;
                    int tipopago = 0;
                    int idc_banco = 0;
                    System.DateTime fecha_deposito = default(DateTime);
                    double monto = 0;
                    string observacionesP = "";
                    bool vconfirmar_pago = lblconfirmacion.Visible;

                    if (vconfirmar_pago == true & ventrar == 2)
                    {
                        ventrar = 3;
                    }

                    if (!string.IsNullOrEmpty(txtfolioCHP.Text))
                    {
                        txtformapago.Text = "";
                    }

                    if (!string.IsNullOrEmpty(txtformapago.Text) & lblconfirmacion.Visible == true)
                    {
                        formapago = txtformapago.Text.Split('%');
                        tipopago = Convert.ToInt32(formapago[0]);
                        idc_banco = Convert.ToInt32(formapago[1]);
                        monto = Convert.ToDouble(formapago[2]);
                        fecha_deposito = Convert.ToDateTime(formapago[3]);
                        observacionesP = formapago[4];
                    }
                    else
                    {
                        fecha_deposito = DateTime.Now;
                    }
                    if (string.IsNullOrEmpty(txtidOc.Text))
                    {
                        txtidOc.Text = "0";
                    }
                    if (string.IsNullOrEmpty(txtsucursalr.Text))
                    {
                        txtsucursalr.Text = "0";
                    }
                    int intentos = 0;
                    string vtip = "";
                    string vleyo = (ventrar == 3 ? "Pedido" : "Pre-Pedido");

                    //'Guardar el pedido
                    DataSet ds = new DataSet();
                    string ip = funciones.GetLocalIPAddress();
                    string pc = funciones.GetPCName(); ;
                    //System.Net.Dns.GetHostEntry(Request.ServerVariables("remote_addr")).HostName
                    string usuariopc = funciones.GetUserName();
                    int resultado = 0;
                    string vcadena = null;
                    string vfecha = null;
                    System.DateTime date1 = DateTime.Now;
                    bool vagenda = false;
                    DateTimeOffset dateOffset = new DateTimeOffset(date1, TimeZoneInfo.Local.GetUtcOffset(date1));
                    vfecha = date1.ToString("o");
                    vfecha = vfecha.Replace("T", " ");
                    vfecha = vfecha.Split('.')[0] + "." + vfecha.Split('.')[1].Substring(0, 3);
                    int vtacti = 0;


                    if (Session["actividad_agente"] != null)
                    {
                        vcadena = Session["actividad_agente"] + ";" + vfecha + ";" + (vagenda ? 1 : 0) + ";" + 1 + ";;;" + Session["vfecha2"] + ";";
                        vtacti = 1;
                    }
                    else
                    {
                        vcadena = "";
                        vtacti = 0;
                    }
                    string vtipog = "";
                    vtipog = (ventrar == 2 | ventrar == 4 ? "C" : "V");
                    vtipog = (ventrar == 3 ? "" : vtipog);



                    //Para lo de Promociones

                    VTOTA_NUEVO = VTOTA_NUEVO + vtotal;

                    string pdetalles = string.Empty;
                    decimal pfleteaut = 0;
                    if (sipasa2 == 1)
                    {
                        ventrar = 1;
                        pfleteaut = Convert.ToDecimal(txtmaniobras.Text.Trim());
                        sipasa2 = 0;
                        vleyo = "Pre-Pedido";
                        vtipog = "V";
                    }

                    if (sipasa == 1)
                    {
                        ventrar = 1;
                        pdetalles = Convert.ToString(mensaje_bd);
                        sipasa = 0;
                        vleyo = "Pre-Pedido";
                        vtipog = "V";
                    }
                    string vmensaje = "";
                    switch (ventrar)
                    {
                        case 1:
                            ds = componente.sp_apreped_CAMBIO_PRECIOS3_nuevo(Convert.ToInt32(txtid.Text.Trim()), Convert.ToInt32(txtpretotal.Text.Trim()), 
                                Convert.ToInt32(Session["idc_sucursal"]), vdesiva,  Convert.ToInt32(Session["Xidc_iva"]), 
                                Convert.ToInt32(Session["idc_usuario"]), Convert.ToInt32(Session["xidc_almacen"]), ip, pc, usuariopc,
                            Convert.ToChar(vtipom), vcambios, vdarti, vtotal, Convert.ToDateTime(cbofechas.SelectedValue), vpro, vidpro, txtnumeroOC.Text.Trim(), idc_colonia, calle,
                            numero, Convert.ToInt32(cp), txtobservaciones.Text.Trim(), folioCHP, sipasa, ventdir, bitcroquis, bitocc, vsin_exis, vcosbajo,
                            0, bitllamada, mensaje, sipasa2, Convert.ToChar(vtipog), Convert.ToDouble(txtmaniobras.Text.Trim()), vrecoge, Convert.ToInt32(txtidOc.Text.Trim()), 
                            Convert.ToInt32(txtsucursalr.Text.Trim()), tipopago,
                            idc_banco, fecha_deposito, monto, observacionesP, vconfirmar_pago, vcadena, vtacti.ToString().Trim(), 0, null, vdarti_nueva,
                            VTOTA_NUEVO, (!string.IsNullOrEmpty(pdetalles) ? pdetalles : ""), (pfleteaut > 0 ? pfleteaut : 0));
                            break;
                        case 2:
                            ds = componente.sp_apreped_creditos4_nuevo(Convert.ToInt32(txtid.Text.Trim()), Convert.ToInt32(txtpretotal.Text.Trim()),
                                Convert.ToInt32(Session["idc_sucursal"]), vdesiva, Convert.ToInt32(Session["Xidc_iva"]),
                                Convert.ToInt32(Session["idc_usuario"]), Convert.ToInt32(Session["xidc_almacen"]), ip, pc, usuariopc,
                            Convert.ToChar(vtipom), vcambios, vdarti, vtotal, Convert.ToDateTime(cbofechas.SelectedValue), vpro, vidpro, txtnumeroOC.Text.Trim(), idc_colonia, calle,
                            numero, Convert.ToInt32(cp), txtobservaciones.Text.Trim(), folioCHP, sipasa, ventdir, bitcroquis, bitocc, vsin_exis, vcosbajo,
                            0, bitllamada, mensaje, Convert.ToChar(vtipog), sipasa2, Convert.ToDouble(txtmaniobras.Text.Trim()), vrecoge, Convert.ToInt32(txtidOc.Text.Trim()),
                            Convert.ToInt32(txtsucursalr.Text.Trim()), tipopago,
                            idc_banco, fecha_deposito, monto, observacionesP, vconfirmar_pago, vcadena, vtacti.ToString().Trim(), 0, null, vdarti_nueva,
                            VTOTA_NUEVO);
                            break;
                        case 3:
                            ds = componente.sp_apreped_creditos4_nuevo(Convert.ToInt32(txtid.Text.Trim()), Convert.ToInt32(txtpretotal.Text.Trim()),
                                Convert.ToInt32(Session["idc_sucursal"]), vdesiva, Convert.ToInt32(Session["Xidc_iva"]),
                                Convert.ToInt32(Session["idc_usuario"]), Convert.ToInt32(Session["xidc_almacen"]), ip, pc, usuariopc,
                            Convert.ToChar(vtipom), vcambios, vdarti, vtotal, Convert.ToDateTime(cbofechas.SelectedValue), vpro, vidpro, txtnumeroOC.Text.Trim(), idc_colonia, calle,
                            numero, Convert.ToInt32(cp), txtobservaciones.Text.Trim(), folioCHP, sipasa, ventdir, bitcroquis, bitocc, vsin_exis, vcosbajo,
                            0, bitllamada, mensaje, Convert.ToChar(vtipog), sipasa2, Convert.ToDouble(txtmaniobras.Text.Trim()), vrecoge, Convert.ToInt32(txtidOc.Text.Trim()),
                            Convert.ToInt32(txtsucursalr.Text.Trim()), tipopago,
                            idc_banco, fecha_deposito, monto, observacionesP, vconfirmar_pago, vcadena, vtacti.ToString().Trim(), 0, null, vdarti_nueva,
                            VTOTA_NUEVO);
                            break;
                        case 4:
                            ds = componente.sp_apreped_creditos_especial_nc3_ESP_nuevo(Convert.ToInt32(txtid.Text.Trim()), Convert.ToDouble(txtpretotal.Text.Trim()),
                                Convert.ToInt32(Session["idc_sucursal"]), vdesiva, Convert.ToInt32(Session["Xidc_iva"]), Convert.ToInt32(Session["idc_usuario"]), 
                                Convert.ToInt32(Session["xidc_almacen"]),   ip, pc, usuariopc, Convert.ToChar(vtipom), vcambios, vdarti, vtotal, 
                                Convert.ToDateTime(cbofechas.SelectedValue), vpro, vidpro, txtnumeroOC.Text.Trim(), idc_colonia, calle,
                            numero, Convert.ToInt32(cp), observaciones, folioCHP, sipasa, ventdir, bitcroquis, bitocc, vsin_exis, vcosbajo,
                            0, bitllamada, mensaje, Convert.ToChar(vtipog), sipasa2, Convert.ToDouble(txtmaniobras.Text.Trim()), vrecoge, 
                            Convert.ToInt32(txtplazo.Text.Trim()), Convert.ToInt32(txtformaP.Text.Trim()), txtotro.Text.Trim(),
                            txtcminima.Text.Trim(), txtcontacto.Text.Trim(), txttelefono.Text.Trim(), txtcorreo.Text.Trim(), 
                            Convert.ToInt32(txtidOc.Text.Trim()), Convert.ToInt32(txtsucursalr.Text.Trim()), vcadena, vtacti.ToString(), 0, null,
                            vdarti_nueva, VTOTA_NUEVO);
                           
                            break;
                    }
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        resultado = Convert.ToInt32(ds.Tables[0].Rows[0]["CODIGOP"]);
                    }

                    //UpdateProgress5.Visible = False
                    if (vmensaje == "")
                    {

                        if (resultado > 0)
                        {
                            //AQUI ME QUEDE
                            GuardarArchivos(resultado, bitllamada, bitocc, bitcroquis, vleyo);
                            cargar_consecutivo_folio();
                            colores_clear();
                            btnnuevoprepedido_Click(null, EventArgs.Empty);
                            cboentrega.SelectedValue = "1";
                            if (Convert.ToInt16(Request.QueryString["tipo"]) == 1)
                            {
                                Session["actividad_agente"] = null;
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>alert('Se Guardo el " + vleyo 
                                    + " con Exito \\n No. " + vleyo + ": " + resultado.ToString() + "'); </script>", false);
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrarpagina", "<script>redirecting();</script>", false);
                            }
                            else
                            {
                                //CargarMsgbox("", "Se Guardo el " & vleyo & " con Exito <BR/> No. " & vleyo & ": " & resultado, False, 2)
                                string msj = "Se Guardo el " + vleyo + " con Exito \\n \\u000b \\n No. " + vleyo + ": " + resultado.ToString();
                                WriteMsgBox(msj);
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);
                            }
                        }
                    }
                    else {
                        Alert.ShowAlertError(vmensaje,this);
                    }
                }
                catch (Exception ex)
                {
                    //CargarMsgbox("", ex.Message, False, 4)
                    WriteMsgBox("Error: \\n" + ex.Message.Replace("'", ""));
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", false);

                }

            }

        }

        public void GuardarArchivos(int NoPedido, bool bitllamada, bool bitoc, bool bitcroquis, string tipop)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/files/"));//path local
                string[] ext2 = new string[4];
                //Arreglo .ext imagenes.
                ext2[0] = ".JPG";
                ext2[1] = ".GIF";
                ext2[2] = ".DIB";
                ext2[3] = ".BMP";

                string[] ext = new string[3];
                //Arreglo .ext audio.
                ext[0] = ".MP3";
                ext[1] = ".ACC";
                ext[2] = ".WMA";
                string ruta = null;
                string unidad = "";


                if (!string.IsNullOrEmpty(txtnumeroOC.Text) & bitoc == true)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        ruta = dirInfo + txtidOc.Text + ext2[i];
                        if (File.Exists(ruta))
                        {
                            if (tipop == "Pedido")
                            {
                                unidad = unidades("PED_OC");
                            }
                            else
                            {
                                unidad = unidades("PPED_OC");
                            }
                            if (!string.IsNullOrEmpty(unidad))
                            {
                                unidad = unidad + NoPedido + ext2[i];
                                funciones.CopiarArchivos(unidad, ruta, this.Page);
                                unidad = "";
                                break; // TODO: might not be correct. Was : Exit For
                            }

                        }
                    }
                }


                if (!string.IsNullOrEmpty(lblllamada.Text) & bitllamada == true)
                {
                    for (int i = 0; i <= ext.Length - 1; i++)
                    {
                        ruta = dirInfo + txtid.Text + ext[i];
                        if (File.Exists(ruta))
                        {
                            if (tipop == "Pedido")
                            {
                                unidad = unidades("PED_LLA");
                            }
                            else
                            {
                                unidad = unidades("PPD_LLA");
                            }
                            if (!string.IsNullOrEmpty(unidad))
                            {
                                unidad = unidad + NoPedido + ext[i];
                                funciones.CopiarArchivos(unidad, ruta, this.Page);
                                unidad = "";
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(lblruta.Text))
                {
                    for (int i = 0; i <= ext.Length - 1; i++)
                    {
                        ruta = dirInfo + txtid.Text + ext2[i];
                        if (File.Exists(ruta))
                        {
                            if (tipop == "Pedido")
                            {
                                unidad = unidades("PED_CRO");
                            }
                            else
                            {
                                unidad = unidades("PPED_CR");
                            }
                            if (!string.IsNullOrEmpty(unidad))
                            {
                                unidad = unidad + NoPedido + ext2[i];
                                funciones.CopiarArchivos(unidad, ruta,this.Page);
                                unidad = "";
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                    fucroquis.Attributes["style"] = "display:inline;";
                    var _with1 = lblruta;
                    //
                    _with1.Attributes["style"] = "Display:none";
                    //Oculta Label con el nombre img croquis
                    _with1.Text = "";
                    //     
                    btncambiarcroquis.Attributes["style"] = "Display:none";
                    //Oculta el botón "Cambiar Croquis"
                    btnvercroquis.Attributes["style"] = "Display:none";
                    //Oculta el botón "Cambiar Croquis"
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}