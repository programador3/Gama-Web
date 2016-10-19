using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;

namespace presentacion
{
    public partial class editar_precios_cantidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                object[] datos_clientes_pedidos = Session["datos_clientes_pedidos"] as object[];
                if ((datos_clientes_pedidos != null))
                {
                    txtlistap.Text = datos_clientes_pedidos[2].ToString();
                    txtid.Text = datos_clientes_pedidos[0].ToString();
                    lblroja.Visible = Convert.ToBoolean(datos_clientes_pedidos[3]);
                    txtrfc.Text = datos_clientes_pedidos[1].ToString();
                    txtcol.Text = datos_clientes_pedidos[4].ToString();
                }

                agregar_columnas_row();
                int edit = Convert.ToInt32(Request.QueryString["edit"]);
                DataRow row = Session["rowprincipal"] as DataRow;
                int idc_articulo = Convert.ToInt32(Request.QueryString["cdi"]);
                if (Request.QueryString["cdi"] == null)
                {
                    return;
                }
                DataTable dt = new DataTable();
                ultimo_precio(idc_articulo);
                if (edit == 1)
                {
                    btnaceptar.Attributes["onclick"] = "return validar_campos_vacios(txtprecio,txtcantidad,1);";
                    editar(idc_articulo);
                }
                else
                {
                    if (validar_si_existe(idc_articulo) == false)
                    {
                        dt = Session["dt_productos_busqueda"] as DataTable;
                        if (!dt.Columns.Contains("precio_libre"))
                        {
                            dt.Columns.Add("precio_libre");
                        }
                        if ((dt != null))
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                if (Convert.ToInt32(dt.Rows[i]["idc_articulo"]) == idc_articulo)
                                {
                                    row = dt.Rows[i];
                                    txtdescripcion.Text = row["desart"].ToString();
                                    txtcodigoarticulo.Text = row["Codigo"].ToString();
                                    txtum.Text = row["unimed"].ToString();
                                    btnaceptar.Attributes["onclick"] = "return validar_campos_vacios(txtprecio,txtcantidad,0);";
                                    if (Convert.ToBoolean(row["Decimales"]) == true)
                                    {
                                        txtcantidad.Attributes["min"] = "0.001";
                                        txtcantidad.Attributes["step"] = "0.001";
                                    }
                                    else
                                    {
                                        txtcantidad.Attributes["min"] = "1";
                                        txtcantidad.Attributes["step"] = "1";
                                    }
                                    datos(idc_articulo, row);
                                }
                            }
                        }
                    }
                    else
                    {
                        CargarMsgBox("El Articulo Seleccionado Ya se Encuentra En la Lista.");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_pagina", "<script>YaExiste();</script>", false);
                    }
                }
                txtcantidad.Attributes["onblur"] = "validarnumero(this);";
                txtcantidad.Attributes["onchange"] = "validarnumero(this);precio_cantidad();";
            }
        }

        public void agregar_columnas_row()
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
            dt.Columns.Add("Precio", typeof(decimal));
            //5
            dt.Columns.Add("Importe");
            //6
            dt.Columns.Add("PrecioReal", typeof(decimal));
            //7
            dt.Columns.Add("Descuento", typeof(decimal));
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
            dt.Columns.Add("idc_promocion", typeof(int));
            //25
            dt.Columns.Add("precio_lista", typeof(decimal));
            //26
            dt.Columns.Add("precio_minimo", typeof(decimal));
            //27
            dt.Columns.Add("tiene_especif", typeof(bool));
            //28
            dt.Columns.Add("especif", typeof(string));
            //29
            dt.Columns.Add("num_especif", typeof(int));
            //30
            dt.Columns.Add("ids_especif", typeof(string));
            //31
            dt.Columns.Add("g_especif", typeof(string));
            //32

            dt.Columns.Add("costo_o", typeof(decimal));
            dt.Columns.Add("precio_o", typeof(decimal));
            dt.Columns.Add("precio_lista_o", typeof(decimal));
            dt.Columns.Add("precio_minimo_o", typeof(decimal));
            DataRow row = default(DataRow);
            row = dt.NewRow();
            Session["row_prod"] = row;
        }

        public object[] ultimo_precio_cliente(int idc_articulo)
        {
            object[] ufecha_precio = new object[3];
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_ultimo_precio_cliente(idc_articulo, Convert.ToInt32(txtid.Text));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    bool cambios = false;
                    DataRow row = default(DataRow);
                    row = ds.Tables[0].Rows[0];
                    cambios = cambio_ult_precio(idc_articulo, Convert.ToInt32(row["idc_ced"]), Convert.ToDateTime(row["fecha"]));
                    if (cambios == false)
                    {
                        ufecha_precio[0] = Convert.ToDecimal(ds.Tables[0].Rows[0]["precio"]);
                    }
                    else
                    {
                        ufecha_precio[0] = Convert.ToDecimal(ds.Tables[0].Rows[0]["precio"]);
                    }
                    ufecha_precio[1] = ds.Tables[0].Rows[0]["FECHA"];
                    ufecha_precio[2] = Convert.ToDecimal(ds.Tables[0].Rows[0]["precio_real"]);
                }
                else
                {
                    ufecha_precio[0] = "";
                    ufecha_precio[1] = "";
                    ufecha_precio[2] = "";
                }
                return ufecha_precio;
            }
            catch (Exception ex)
            {
                CargarMsgBox(ex.Message);
                return ufecha_precio;
            }
        }

        public bool aplica_sug(int idc_articulo)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = funciones.ExecQuery("SELECT DBO.fn_conversion_articulo(" + Convert.ToString(idc_articulo) + ") AS TCONV");
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToBoolean(dt.Rows[0]["TCONV"]);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox(ex.Message);
                return false;
            }
        }

        public bool cantidad_sugerida()
        {
            DataTable dt = new DataTable();
            string idc_articulo = Request.QueryString["cdi"];
            string msj = "Cantidad Sugerida Permitida:\\n \\u000b \\n";
            try
            {
                string query = "select * from dbo.fn_validar_conversiones(" + idc_articulo + "," + (txtcantidad.Text.Trim() == "" ? "0" : txtcantidad.Text.Trim() )+ ")";
                dt = funciones.ExecQuery(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        msj = msj + row["cantidad"].ToString() + " (" + row["piezas"].ToString() + " piezas)\\n \\u000b \\n";
                    }
                    CargarMsgBox(msj);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Validar Cantidad Sugerida Permitida.\\n \\u000b \\nError:\\n" + ex.Message);
                return false;
            }
        }

        public bool cambio_ult_precio(int idc, int ced, System.DateTime fecha)
        {
            try
            {
                AgentesCOM componente = new AgentesCOM();
                DataTable dt = new DataTable();
                dt = componente.sp_fn_cambio_costo_arti_cedis_fecha(ced, idc, fecha).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToBoolean(dt.Rows[0]["cambio"]);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox(ex.Message);
                return false;
            }
        }

        public void ultimo_precio(int idc_articulo)
        {
            try
            {
                object[] ult_precios = null;
                ult_precios = ultimo_precio_cliente(idc_articulo);

                string mensaje = "";
                if (ult_precios[0] != null && ult_precios[0].ToString() != "")
                {
                    mensaje = (ult_precios[0] != null ? "Ultimo Precio Facturado:\\n" + ult_precios[0].ToString() : "");
                }
                if (ult_precios[1] != null && ult_precios[1].ToString() != "")
                {
                    mensaje = mensaje + (ult_precios[1] != null ? "\\n \\u000b \\nFecha Ultimo Precio:\\n" + ult_precios[1].ToString() : "");
                }

                if (ult_precios[2] != null && ult_precios[2].ToString() != "")
                {
                    if (!string.IsNullOrEmpty((ult_precios[2].ToString())))
                    {
                        if (Convert.ToDecimal(ult_precios[0].ToString().Replace("*", "")) != Convert.ToDecimal(ult_precios[2]))
                        {
                            mensaje = mensaje + (ult_precios[2] != null ? "\\n \\u000b \\nPrecio Real:\\n" + ult_precios[2].ToString() : "");
                        }
                    }
                }

                if (!string.IsNullOrEmpty(mensaje))
                {
                    imgultimo.Attributes["onClick"] = "alert('" + mensaje + "');return false;";
                }
                else
                {
                    imgultimo.Attributes["onClick"] = "alert('Sin Historial de Venta.');return false;";
                    imgultimo.ImageUrl = "imagenes/calendar3.gif";
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Erro al Cargar Ultimo Precio.." + ex.Message.Replace("'", ""));
            }
        }

        public void CargarMsgBox(string msg)
        {
            Alert.ShowAlertError(msg, this);
        }

        public void editar(int idc)
        {
            DataTable dt = new DataTable();
            dt = Session["dt_productos_lista"] as DataTable;
            decimal viva = Session["NuevoIva"] == null ? 0 : Convert.ToDecimal(Session["NuevoIva"]);
            viva = (viva / 100);
            viva = 1 + viva;
            if (dt == null)
            {
                return;
            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["idc_articulo"]) == idc)
                    {
                        txtdescripcion.Text = dt.Rows[i]["Descripcion"].ToString();
                        txtum.Text = dt.Rows[i]["um"].ToString();

                        if (Convert.ToInt32(dt.Rows[i]["num_especif"]) > 0)
                        {
                            DataSet ds = new DataSet();
                            try
                            {
                                AgentesCOM com = new AgentesCOM();
                                ds = com.sp_precio_cliente_cedis_rangos1(Convert.ToInt32(dt.Rows[i]["idc_articulo"]), Convert.ToInt32(txtid.Text),
                                    Convert.ToInt32(Session["pxidc_sucursal"]), Convert.ToDecimal(dt.Rows[i]["cantidad"]), true,
                                    dt.Rows[i]["ids_especif"].ToString(), Convert.ToInt32(dt.Rows[i]["num_especif"]));
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow row in ds.Tables[0].Rows)
                                    {
                                        if (txtrfc.Text.StartsWith("*"))
                                        {
                                            txtprecio.Text = Convert.ToDecimal(ds.Tables[0].Rows[i]["precio"]).ToString("#.####");
                                            txtprecioreal.Text = Convert.ToDecimal(ds.Tables[0].Rows[i]["precio_real"]).ToString("#.####");
                                            txtpreciominimo.Text = Convert.ToDecimal(ds.Tables[0].Rows[i]["precio_minimo"]).ToString("#.####");
                                        }
                                        else
                                        {
                                            txtprecio.Text = ds.Tables[0].Rows[i]["precio"].ToString();
                                            txtprecioreal.Text = ds.Tables[0].Rows[i]["precio_real"].ToString();
                                            txtpreciominimo.Text = Convert.ToDecimal(ds.Tables[0].Rows[i]["precio_minimo"]).ToString("#.####");
                                        }
                                    }
                                }
                                else
                                {
                                    CargarMsgBox("No se Encontro el Precio del Producto.");
                                }
                            }
                            catch (Exception ex)
                            {
                                CargarMsgBox("No se Encontro el Precio del Producto.");
                            }
                        }
                        else
                        {
                            txtprecio.Text = dt.Rows[i]["precio"].ToString();
                            // FormatNumber(dt.Rows[i]["precio"), 4)
                            txtprecioreal.Text = dt.Rows[i]["precioreal"].ToString();
                            // FormatNumber(dt.Rows[i]["precioreal"), 4)
                            txtpreciominimo.Text = Convert.ToDecimal(dt.Rows[i]["precio_minimo"]).ToString("#.####");
                        }

                        txtult_pre.Text = txtprecio.Text;
                        txtcantidad.Text = dt.Rows[i]["Cantidad"].ToString();
                        txtcodigoarticulo.Text = dt.Rows[i]["codigo"].ToString();
                        if (Convert.ToBoolean(dt.Rows[i]["Decimales"]) == true)
                        {
                            txtcantidad.Attributes["min"] = "0.001";
                            txtcantidad.Attributes["step"] = "0.001";
                        }
                        else
                        {
                            txtcantidad.Attributes["min"] = "1";
                            txtcantidad.Attributes["step"] = "1";
                        }
                        if (Convert.ToBoolean(dt.Rows[i]["nota_credito"]) == true)
                        {
                            txtprecio.Attributes["onfocus"] = "this.blur();";
                            txtprecioreal.Attributes["onfocus"] = "this.blur();";
                            txtprecio.Attributes.Remove("onclik");
                            txtprecioreal.Attributes.Remove("onclik");
                            txtprecio.Attributes.Remove("onblur");
                            txtprecioreal.Attributes.Remove("onblur");
                            lblnc.Visible = true;
                        }
                        else
                        {
                            txtprecio.Attributes["onfocus"] = "this.blur();";
                            txtprecioreal.Attributes["onfocus"] = "this.blur();";
                            lblnc.Visible = false;
                        }
                    }
                }
            }
        }

        public bool validar_si_existe(int idc_articulo)
        {
            DataTable dt = new DataTable();
            dt = Session["dt_productos_lista"] as DataTable;
            bool existe = false;
            if ((dt != null))
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (idc_articulo == Convert.ToInt32(dt.Rows[i]["idc_articulo"].ToString().Trim()))
                    {
                        existe = true;
                    }
                }
            }
            return existe;
        }

        public void datos(int idc_articulo, DataRow row)
        {
            DataRow rowprincipal = Session["row_prod"] as DataRow;

            if ((rowprincipal != null))
            {
                Session["rowprincipal"] = rowprincipal;
                int i = 0;
                i = 0;
                rowprincipal["idc_articulo"] = row["idc_articulo"];
                rowprincipal["Codigo"] = row["codigo"];
                rowprincipal["Descripcion"] = row["desart"];
                rowprincipal["UM"] = row["unimed"];
                rowprincipal["Decimales"] = row["decimales"];
                //Decimales(CBool(rowprincipal["Decimales"]))
                rowprincipal["Paquete"] = row["paquete"];
                rowprincipal["precio_libre"] = (row["precio_libre"] == null ? false : row["precio_libre"]);
                rowprincipal["comercial"] = row["comercial"];
                rowprincipal["vende_exis"] = row["vende_exis"];
                rowprincipal["minimo_venta"] = row["minimo_venta"];
                rowprincipal["Master"] = row["master"];
                rowprincipal["EXISTENCIA"] = buscar_Existencia_Articulo(Convert.ToInt32(rowprincipal["idc_articulo"]));
                rowprincipal["fecha"] = row["fecha"];
                rowprincipal["obscotiza"] = row["obscotiza"];
                rowprincipal["mensaje"] = row["mensaje"];

                bool especif = row.Table.Columns.Contains("especif");
                if (especif == true)
                {
                    rowprincipal["tiene_especif"] = row["especif"];
                }
                else
                {
                    rowprincipal["tiene_especif"] = false;
                }

                rowprincipal["especif"] = "";
                rowprincipal["num_especif"] = 0;
                rowprincipal["ids_especif"] = "";
                rowprincipal["g_especif"] = "";

                if (calculado(Convert.ToInt32(rowprincipal["idc_articulo"])) == 0)
                {
                    rowprincipal["calculado"] = false;
                    rowprincipal["porcentaje"] = 0.0;
                    buscar_precio_producto(Convert.ToInt32(row["idc_articulo"]), rowprincipal);
                    txtcodigoarticulo.Enabled = false;
                }
                else
                {
                    if (false == false)
                    {
                        rowprincipal["precio"] = Redondeo_cuatro_decimales(0.0);
                        rowprincipal["cantidad"] = 1;
                        rowprincipal["precioreal"] = Redondeo_cuatro_decimales(Convert.ToDouble(rowprincipal["precio"]));
                        rowprincipal["calculado"] = true;
                        rowprincipal["porcentaje"] = calculado(Convert.ToInt32(rowprincipal["idc_articulo"]));
                        rowprincipal["nota_credito"] = false;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_duplicado", "<script>refrescar();</script>", false);
                    }
                    else
                    {
                        //Artiuclo en lista Send Message to the user and clean the objects
                        CargarMsgBox("El Articulo Seleccioando Ya se Encuentra en la Lista.");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_duplicado", "<script>window.close();</script>", false);
                    }
                }
            }
            else
            {
                CargarMsgBox("Error al Cargar de Informacion del Articulo Seleccionado.");
            }
        }

        public void buscar_precio_producto(int codigo, DataRow rowprincipal)
        {
            rowprincipal["nota_credito"] = false;

            int vidc = codigo;
            int vidcli = Convert.ToInt32(txtid.Text.Trim());
            int vidc_clonia = Convert.ToInt32(txtcol.Text.Trim());
            dynamic dt = default(DataTable);
            dynamic dt1 = default(DataTable);
            dynamic dt2 = default(DataTable);
            DataTable dt3 = new DataTable();

            decimal vprecio_minimo = 0;
            decimal vprecio = 0;
            decimal vprecio_lista = 0;
            int vidc_listap = Convert.ToInt32(txtlistap.Text.Trim());
            int zidc_sucursal = Convert.ToInt32(Session["pxidc_sucursal"]);
            AgentesCOM com = new AgentesCOM();
            DataSet ds1 = new DataSet();
            try
            {
                ds1 = com.sp_precio_cliente_cedis_rangos1(vidc, vidcli, zidc_sucursal, 0, true, Convert.ToString(rowprincipal["ids_especif"]), Convert.ToInt32(rowprincipal["num_especif"]));
                //12-05-2015 MIC se agrego ultimos 2 parametros

                dt = ds1.Tables[0];
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Cargar Precio del Producto.  \\n \\u000b \\n Error: \\n" + ex.Message);
                return;
            }

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDecimal(dt.Rows[0]["precio"]) <= 0)
                {
                    //Limpiar campos del articulo
                    CargarMsgBox("No se Encontro el Precio de Producto. \\n");
                    return;
                }
            }
            else
            {
                //Limpiar campos del articulo
                CargarMsgBox("No se Encontro el Precio de Producto. \\n");
                return;
            }
            vprecio = Convert.ToDecimal(dt.Rows[0]["precio"].ToString());
            vprecio_lista = Convert.ToDecimal(dt.Rows[0]["precio_lista"].ToString());
            vprecio_minimo = Convert.ToDecimal(dt.Rows[0]["precio_minimo"].ToString());
            if (lblroja.Visible == true)
            {
                try
                {
                    if (zidc_sucursal > 0)
                    {
                        dt1 = funciones.ExecQuery("select dbo.fn_ver_precio_cliente_esp_cambio_lista(" + vidc.ToString() + "," + vidcli.ToString() + "," + zidc_sucursal.ToString() + ") as pxprecio");
                    }
                    else
                    {
                        dt1 = funciones.ExecQuery("select dbo.fn_ver_precio_cliente_esp_lp_SUC(" + vidc.ToString() + "," + vidcli.ToString() + "," + vidc_listap.ToString() + "," + Session["idc_sucursal"] as string + ") as pxprecio");
                    }
                    vprecio =Convert.ToDecimal(dt1.Rows[0]["pxprecio"]);
                }
                catch (Exception ex)
                {
                    CargarMsgBox("No Se Procedio a Verificar Precios. \\n- \\n" + ex.Message);
                }

                try
                {
                    dt2 = funciones.ExecQuery("select dbo.fn_ver_precio_real_cliente_esp_cambio_lista(" + vidc.ToString() + "," + vidcli.ToString() + "," + zidc_sucursal.ToString() + ") as pxprecior");
                    rowprincipal["PrecioReal"] = dt2.Rows[0]["pxprecior"];
                    Session["pprecio_real"] = dt2.Rows[0]["pxprecior"];
                    rowprincipal["descuento"] = (vprecio - Convert.ToDecimal(rowprincipal["PrecioReal"])).ToString("#.####");

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
                    CargarMsgBox("No Se procedio a Verificar Precios. \\n - \\n" + ex.Message);
                }
            }

            if (vprecio < vprecio_minimo)
            {
                vprecio_minimo = vprecio;
            }

            rowprincipal["Costo"] = dt.Rows[0]["costo"];
            rowprincipal["Costo_o"] = dt.Rows[0]["costo"];
            int viva = Convert.ToInt32(Session["NuevoIva"]);

            if (txtrfc.Text.Trim().StartsWith("*") == true)
            {
                rowprincipal["precio"] = (vprecio * (1 + (viva / 100)));
                rowprincipal["precio_lista"] = (vprecio_lista * (1 + (viva / 100)));
                rowprincipal["precio_minimo"] = (vprecio_minimo * (1 + (viva / 100)));

                rowprincipal["precio_o"] = (vprecio * (1 + (viva / 100)));
                rowprincipal["precio_lista_o"] = (vprecio_lista * (1 + (viva / 100)));
                rowprincipal["precio_minimo_o"] = (vprecio_minimo * (1 + (viva / 100)));
            }
            else
            {
                rowprincipal["precio"] = (vprecio);
                rowprincipal["precio_lista"] = (vprecio_lista);
                rowprincipal["precio_minimo"] = (vprecio_minimo);

                rowprincipal["precio_o"] = (vprecio);
                rowprincipal["precio_lista_o"] = (vprecio_lista);
                rowprincipal["precio_minimo_o"] = (vprecio_minimo);
            }

            txtpreciominimo.Text = Convert.ToDecimal(rowprincipal["precio_minimo"]).ToString("#.####");
            txtprecio.Text = rowprincipal["precio"].ToString();
            txtprecioreal.Text = rowprincipal["precio"].ToString();
            txtult_pre.Text = txtprecio.Text;
            try
            {
                DataSet ds = new DataSet();
                AgentesCOM comp = new AgentesCOM();
                DataRow row = default(DataRow);
                decimal vprecio_real = 0;
                ds = comp.SP_nc_auto_CLIENTE_articulo(codigo, Convert.ToInt32(txtid.Text.Trim()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    decimal vvcosto = default(decimal);
                    vvcosto = Convert.ToDecimal(dt.Rows[0]["costo"]);
                    rowprincipal["Costo"] = vvcosto;
                    rowprincipal["nota_credito"] = true;
                    if (txtrfc.Text.StartsWith("*"))
                    {
                        vprecio_real = Math.Round(Convert.ToDecimal(row["precio_real"]) * (1 + (Convert.ToDecimal(Session["nuevoiva"]) / 100)), 4);
                        vprecio = Math.Round(Convert.ToDecimal(row["precio"]) * (1 + (Convert.ToDecimal(Session["nuevoiva"]) / 100)), 4);
                    }
                    else
                    {
                        vprecio = Math.Round(Convert.ToDecimal(row["precio"]), 4);
                        vprecio_real = Math.Round(Convert.ToDecimal(row["precio_real"]), 4);
                    }
                    rowprincipal["descuento"] = vprecio - vprecio_real;
                    rowprincipal["Precio"] = vprecio;
                    rowprincipal["PrecioReal"] = vprecio_real;
                    txtprecio.Enabled = false;
                    txtprecioreal.Enabled = false;
                    txtprecio.Text = vprecio.ToString();
                    txtprecioreal.Text = vprecio_real.ToString();
                    txtult_pre.Text = txtprecio.Text;
                    lblnc.Visible = true;
                }
                else
                {
                    rowprincipal["nota_credito"] = false;
                    rowprincipal["descuento"] = 0;
                    lblnc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("No se procedio a buscar Nota de Credito Automatica de este Articulo. \\n \\u000B \\n" + ex.Message);
                Session["rowprincipal"] = null;
            }
            Session["rowprincipal"] = rowprincipal;
        }

        public string Redondeo_cuatro_decimales(double valor)
        {
            try
            {
                valor = Math.Round(valor, 4);
                return valor.ToString("#.####");
            }
            catch (Exception ex)
            {
                CargarMsgBox(ex.ToString());
                return "";
            }
        }

        public double calculado(int idc_articulo)
        {
            try
            {
                DataSet datos = new DataSet();
                DataRow row = default(DataRow);
                AgentesCOM com = new AgentesCOM();
                datos = com.sp_bgastos_chqseg(idc_articulo);
                if (datos.Tables[0].Rows.Count > 0)
                {
                    row = datos.Tables[0].Rows[0];
                    return Convert.ToDouble(row["porcentaje"]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox(ex.ToString());
                return 0;
            }
        }

        public double buscar_Existencia_Articulo(int idc_articulo)
        {
            DataRow row = default(DataRow);
            DataSet ds = new DataSet();
            try
            {
                AgentesCOM com = new AgentesCOM();
                ds = com.sp_bexistencia_disponible(Convert.ToInt16(Session["xidc_almacen"]), idc_articulo, 0);
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
                CargarMsgBox(ex.ToString());
                return 0;
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
                    CargarMsgBox("No puedes vender mas de la existencia, existencia: " + Convert.ToDouble(rowprincipal["Existencia"]).ToString());
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox(ex.ToString());
                return false;
            }
        }

        public bool validar_guardar(DataRow row)
        {
            //----
            if (Convert.ToBoolean(row["vende_exis"]) == true & Convert.ToBoolean(row["comercial"]) == true)
            {
                if (No_Vender_Mas_De_Existencia(Convert.ToInt32(row["idc_articulo"]), Convert.ToInt32(txtcantidad.Text)) == false)
                {
                    return false;
                }
            }
            if (validar_multiplos(Convert.ToInt32(row["idc_articulo"]), Convert.ToInt32(txtcantidad.Text)) == false)
            {
                return false;
            }
            //if (validar_numerico(txtcantidad.Text.Trim()) == false && validar_cantidad_decimales(txtcantidad.Text) == false)
            //{
            //    CargarMsgBox("Ingresar cantidad con numeros enteros y solo tres numeros decimales");
            //    return false;
            //}
            //if (validar_numerico(txtcantidad.Text.Trim()) == false)
            //{
            //    CargarMsgBox("La cantidad no es correcta, ['0' o '0.000']");
            //    return false;
            //}
            //else if (validar_numerico(txtprecio.Text) == false)
            //{
            //    CargarMsgBox("El precio no es correcto, ['1,000.0000']");
            //    return false;
            //}
            //if (validar_precio_decimales(txtprecio.Text) == false)
            //{
            //    CargarMsgBox("Precio incorrecto, ingresar numeros enteros y cuatro decimales");
            //    return false;
            //}
            //else if (validar_precio_decimales(txtprecioreal.Text) == false)
            //{
            //    CargarMsgBox("Precio Real Incorrecto, ingresar numeros enteros y cuatro decimales");
            //    return false;
            //}

            if (Convert.ToDecimal(txtprecio.Text) == 0)
            {
                CargarMsgBox("El precio no puede ser IGUAL a Cero");
                return false;
            }
            if (Convert.ToDecimal(txtprecioreal.Text) == 0)
            {
                txtprecioreal.Text = txtprecio.Text;
            }
            if (Convert.ToDecimal(txtprecioreal.Text) > Convert.ToDecimal(txtprecio.Text) == true)
            {
                CargarMsgBox("El Precio Real no puede ser mayor al precio");
                return false;
            }
            return true;
        }

        public bool validar_numerico(string valor_numero)
        {
            try
            {
                long number1 = 0;
                bool canConvert = long.TryParse(valor_numero, out number1);
                return canConvert;
            }
            catch (Exception ex)
            {
                CargarMsgBox(ex.ToString());
                return false;
            }
        }

        public bool validar_cantidad_decimales(string cantidad)
        {
            string[] valor = null;
            try
            {
                bool ret = false;
                valor = System.Text.RegularExpressions.Regex.Split(cantidad, ".");
                if (valor.Length > 2)
                {
                    CargarMsgBox("Ingresar cantidad con numeros enteros y solo tres numeros decimales");
                    ret = false;
                }
                else if (valor.Length == 2)
                {
                    if (valor[1].Length > 3)
                    {
                        CargarMsgBox("Ingresar cantidad con numeros enteros y solo tres numeros decimales");
                        ret = false;
                    }
                    else
                    {
                        ret = true;
                    }
                }
                else if (valor.Length == 1)
                {
                    ret = true;
                }
                return ret;
            }
            catch (Exception ex)
            {
                CargarMsgBox(ex.ToString());
                return false;
            }
        }

        public bool validar_precio_decimales(string precio)
        {
            string[] preciod = null;
            try
            {
                bool ret = false;
                preciod = System.Text.RegularExpressions.Regex.Split(precio, ".");
                if (preciod.Length > 2)
                {
                    ret = false;
                }
                else if (preciod.Length == 1)
                {
                    ret = true;
                }
                else if (preciod.Length == 2)
                {
                    if (preciod[1].Length > 4)
                    {
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
                CargarMsgBox(ex.ToString());
                return false;
            }
        }

        public bool validar_multiplos(int idc_articulo, int cantidad)
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
                        CargarMsgBox("Cantidad invalida...Solo multiplos de: " + row["rconversion"].ToString());
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
                CargarMsgBox(ex.ToString());
                return false;
            }
        }

        public bool Validar_Campos()
        {
            bool functionReturnValue = false;

            if (string.IsNullOrEmpty(txtcodigoarticulo.Text))
            {
                CargarMsgBox("Indicar el codigo del articulo");
                return false;
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(txtdescripcion.Text))
            {
                CargarMsgBox("Indicar la descricpción del articulo");
                return false;
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(txtum.Text))
            {
                CargarMsgBox("Indicar Unidad de Medida del articulo");
                return false;
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(txtcantidad.Text))
            {
                CargarMsgBox("Es necesario indicar la cantidad");
                return false;
                return functionReturnValue;
            }
            else if (validar_cantidad_decimales(txtcantidad.Text) == false)
            {
                return false;
                return functionReturnValue;
            }
            else if (txtcantidad.Text == 0.ToString())
            {
                CargarMsgBox("La cantidad no puede ser Cero...");
                return false;
                return functionReturnValue;
            }
            else if (string.IsNullOrEmpty(txtprecio.Text))
            {
                CargarMsgBox("Es necesario capturar el precio del articulo");
                return false;
                return functionReturnValue;
            }
            else if (txtprecio.Text == 0.ToString())
            {
                CargarMsgBox("El precio debera ser mayor a Cero...");
                return false;
                return functionReturnValue;
            }
            else
            {
                return true;
            }
            return functionReturnValue;
        }
        public void precio_guardar(int idc_articulo, decimal cantidad)
        {
            try
            {
                DataSet ds = new DataSet();
                decimal precio = 0;
                decimal precioreal = 0;
                AgentesCOM com = new AgentesCOM();
                ds = com.sp_precio_cliente_cedis_rangos1(idc_articulo, Convert.ToInt32(txtid.Text.Trim()), Convert.ToInt32(Session["pxidc_sucursal"]), cantidad, true,
                    "",0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    precio = Convert.ToDecimal(ds.Tables[0].Rows[0]["precio"]);
                    precioreal = Convert.ToDecimal(ds.Tables[0].Rows[0]["precio"]);
                    if (txtrfc.Text.StartsWith("*"))
                    {
                        precio = Math.Round(precio * (1 + (Convert.ToDecimal(Session["nuevoiva"]) / 100)), 4);
                        precioreal = Math.Round(precioreal * (1 + (Convert.ToDecimal(Session["nuevoiva"]) / 100)), 4);
                        if (txtprecio.Text != precio.ToString() | (txtprecioreal.Text.Trim() != precioreal.ToString() & precio != precioreal))
                        {
                            txtprecio.Text = precio.ToString();
                            txtprecioreal.Text = precioreal.ToString();
                            txtult_pre.Text = txtprecio.Text;
                        }
                    }
                    else
                    {
                        precio = precio;
                        precioreal = precioreal;
                        if (txtprecio.Text != precio.ToString() | (txtprecioreal.Text.Trim() != precioreal.ToString() & precio != precioreal))
                        {
                            txtprecio.Text = precio.ToString();
                            txtprecioreal.Text = precioreal.ToString();
                            txtult_pre.Text = txtprecio.Text;
                        }
                        //Return precio
                    }
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["nota"]) == true)
                    {
                        txtprecio.Attributes.Remove("onclick");
                        txtprecioreal.Attributes.Remove("onclick");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void cant_sug(int idc_articulo, decimal cantidad)
        {
            DataSet ds = new DataSet();
            decimal sugerida = default(decimal);
            try
            {
                AgentesCOM COM = new AgentesCOM();
                ds = COM.SP_CONVERSION_ARTICULO(idc_articulo, cantidad);
                if (ds.Tables[1].Rows.Count > 0)
                {
                    sugerida = Convert.ToDecimal(ds.Tables[1].Rows[0]["sugerida"]);
                    if (!(cantidad == sugerida))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "<script>cant_sugerida(" + ds.Tables[1].Rows[0]["entero"].ToString() + "," 
                            + ds.Tables[1].Rows[0]["sugerida"].ToString() + "," + (Convert.ToDecimal(ds.Tables[0].Rows[0]["cantidad"]) * 1000).ToString("#.####") + "," 
                            + ds.Tables[0].Rows[0]["factor"].ToString() + ");</script>", false);
                    }
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Validar Cantidad Sugerida.\\n \\u000B \\nError:\\n" + ex.Message);
            }
        }

        protected void btnbuscarprecio_Click(object sender, System.EventArgs e)
        {
            try
            {
                int edit = Convert.ToInt32(Request.QueryString["edit"]);
                int idc_articulo = Convert.ToInt32(Request.QueryString["cdi"]);
                bool suger = false;

                if (edit == 1)
                {
                    DataTable dt = new DataTable();
                    dt = Session["dt_productos_lista"] as DataTable;
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        if (dt.Rows[i]["idc_articulo"].ToString() == idc_articulo.ToString() & Convert.ToBoolean(dt.Rows[i]["nota_credito"]) == false)
                        {
                            suger = aplica_sug(idc_articulo);
                            if ((suger == true))
                            {
                                cant_sug(idc_articulo, Convert.ToDecimal(txtcantidad.Text.Trim()));
                            }
                            else
                            {
                                txtaplicar.Text = "";
                            }
                            precio_guardar(idc_articulo, Convert.ToDecimal(txtcantidad.Text.Trim()));
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }
                else
                {
                    DataRow row = default(DataRow);
                    row = Session["rowprincipal"] as DataRow;
                    if ((row != null))
                    {
                        if (row["idc_articulo"].ToString() == idc_articulo.ToString() & Convert.ToBoolean(row["nota_credito"]) == false)
                        {
                            suger = aplica_sug(idc_articulo);
                            if ((suger == true))
                            {
                                cant_sug(idc_articulo, Convert.ToDecimal(txtcantidad.Text.Trim()));
                            }
                            else
                            {
                                txtaplicar.Text = "";
                            }
                            precio_guardar(idc_articulo, Convert.ToDecimal(txtcantidad.Text.Trim()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Buscar Precio del Articulo. \\n \\u000B \\n Error: \\n" + ex.Message);
            }

        }

        protected void btnaceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cantidad_sugerida() == false)
                {
                    return;
                }
                if (Request.QueryString["edit"] == "1")
                {
                    DataTable dt = new DataTable();
                    int idc_articulo = Convert.ToInt32(Request.QueryString["cdi"]);
                    dt = Session["dt_productos_lista"] as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            if (dt.Rows[i]["idc_articulo"].ToString() == idc_articulo.ToString())
                            {

                                if (!(validar_guardar(dt.Rows[i]) == false))
                                {
                                    dt.Rows[i]["precio"] = txtprecio.Text.Trim();
                                    // FormatNumber(txtprecio.Text.Trim, 4) 'precio_guardar(dt.Rows[i]["idc_articulo"), txtcantidad.Text)
                                    dt.Rows[i]["cantidad"] = txtcantidad.Text.Trim();
                                    //dt.Rows[i]["importe") = dt.Rows[i]["cantidad") * dt.Rows[i]["precio")
                                    dt.Rows[i]["precioreal"] = txtprecioreal.Text.Trim();
                                    //FormatNumber(txtprecioreal.Text.Trim, 4)  'dt.Rows[i]["precio")
                                    Session["dt_productos_lista"] = dt;

                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>cerrar_guardar_edit();</script>", false);
                                }
                            }
                        }
                    }
                }
                else
                {
                    DataRow row = Session["rowprincipal"] as DataRow;
                    int edit = Convert.ToInt32(Request.QueryString["edit"]);
                    if (!(validar_guardar(row) == false))
                    {
                        if (Convert.ToBoolean(row["nota_credito"]) == true)
                        {
                            row["cantidad"] = txtcantidad.Text.Trim();
                            row["Calculado"] = false;
                        }
                        else
                        {
                            row["precio"] = txtprecio.Text.Trim();
                            row["cantidad"] = txtcantidad.Text.Trim();
                            row["precioreal"] = txtprecioreal.Text.Trim();
                            row["Calculado"] = false;
                        }
                        Session["rowprincipal"] = row;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "", "<script>refrescar();</script>", false);
                    }
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Guardar Articulo. \\n \\u000B \\n Error: \\n" + ex.Message);
            }

        }
    }
}