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
    public partial class cotizaciones_correo : System.Web.UI.Page
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
                ViewState["dt_c_correo"] = null;
                ViewState["dt_master_cotizacion"] = null;
                Session["dt_productos_busqueda"] = null;
                Session["dt_productos_lista"] = null;
                if (Request.QueryString["idc_cliente"] != null)
                {
                    int idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
                    txtid.Text = idc_cliente.ToString().Trim();
                    cargar_datos_cliente(idc_cliente);
                    agregar_columnas_dataset();
                    lista_p(idc_cliente);
                    div_buscar.Visible = false;
                    div_info.Visible = true;
                }
                else
                {
                    div_buscar.Visible = true;
                    div_info.Visible = false;
                }
            }
        }

        private void CargarMsgBox(string value)
        {
            Alert.ShowAlertError(value, this.Page);
        }

        private void alert(string value)
        {
            Alert.ShowAlertError(value, this.Page);
        }

        private void cargar_datos_cliente(int idc_cliente)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                entidad.Pidc_cliente = idc_cliente;
                DataSet ds = com.sp_datos_cliente(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtnombre.Text = ds.Tables[0].Rows[0]["nombre"].ToString().Trim();
                    txtrfc.Text = ds.Tables[0].Rows[0]["rfccliente"].ToString();
                    txtid.Text = idc_cliente.ToString().Trim();
                    txtstatus.Text = ds.Tables[0].Rows[0]["idc_bloqueoc"].ToString().Trim();
                    colores(Convert.ToInt32(ds.Tables[0].Rows[0]["idc_bloqueoc"].ToString().Trim()));
                    txtcliente.Attributes["onfocus"] = "this.blur()";
                    div_buscar.Visible = false;
                    div_info.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void buscar_datos_cliente(string valor)
        {
            try
            {
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_bclientes_ventas(valor);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cboclientes.DataSource = ds.Tables[0];
                    cboclientes.DataTextField = "nombre";
                    cboclientes.DataValueField = "idc_cliente";
                    cboclientes.DataBind();
                }
                else
                {
                    Alert.ShowAlertError("No se Encontraron Resultados. Intentelo Nuevamente", this.Page);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void cargar_combo()
        {
            try
            {
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_sucursales_combo();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbosucursales.DataSource = ds.Tables[0];
                    cbosucursales.DataTextField = "nombre";
                    cbosucursales.DataValueField = "idc_sucursal";
                    cbosucursales.DataBind();
                    cbosucursales.Items.Insert(0, new ListItem("--Seleccione una Sucursal", "0"));
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void agregar_columnas_dataset()
        {
            //MIC estructura copiada de editar_precios_cantidad 15-05-2015 codigo :2
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
            ViewState["dt_c_correo"] = dt;
        }

        public void colores(int valor)
        {
            switch (valor)
            {
                case 0:
                    //Verde
                    txtstatus.BackColor = System.Drawing.Color.Green;
                    txtstatus.ForeColor = System.Drawing.Color.White;
                    txtnombre.BackColor = System.Drawing.Color.Green;
                    txtnombre.ForeColor = System.Drawing.Color.White;
                    break;

                case 4:
                    //Amarillo
                    txtstatus.BackColor = System.Drawing.Color.Yellow;
                    txtstatus.ForeColor = System.Drawing.Color.Black;
                    txtnombre.BackColor = System.Drawing.Color.Yellow;
                    txtnombre.ForeColor = System.Drawing.Color.Black;
                    break;

                case 1:
                case 2:
                case 3:
                    //Rojo
                    txtstatus.BackColor = System.Drawing.Color.Red;
                    txtstatus.ForeColor = System.Drawing.Color.White;
                    txtnombre.BackColor = System.Drawing.Color.Red;
                    txtnombre.ForeColor = System.Drawing.Color.White;
                    break;
            }
        }

        public bool validar_datos_dir()
        {
            if (string.IsNullOrEmpty(txtcalle.Text.Trim()) & (string.IsNullOrEmpty(txtproy.Text) | Convert.ToInt32(txtproy.Text) <= 0))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(txtnumero.Text.Trim()) & (string.IsNullOrEmpty(txtproy.Text) | Convert.ToInt32(txtproy.Text) <= 0))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(txtidc_colonia.Text.Trim()) & (string.IsNullOrEmpty(txtproy.Text) | Convert.ToInt32(txtproy.Text) <= 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void lista_p(int idc_cliente)
        {
            DataTable dt = new DataTable();
            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pidc_cliente = idc_cliente;
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_clientes_clasif_idc_lista(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtlistap.Text = ds.Tables[0].Rows[0]["idc_listap"].ToString();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void controles_busqueda_prod(bool estado)
        {
            cboproductos.Visible = estado;
            txtcodigo.Visible = estado;
            txtcodigo.Enabled = estado;

            LinkButton4.Visible = estado;
            if (estado == true)
            {
                btnbuscar_codigo.Visible = false;
            }
            else
            {
                btnbuscar_codigo.Visible = true;
            }
        }

        public void controles_busqueda_prod_sel_cancel(bool estado)
        {
            cboproductos.Visible = estado;
            //imgcancelar.Visible = estado
            //imgaceptar.Visible = estado
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

        public void prep_cargar_grid_prod_master_cliente(int idc_cliente)
        {
            DataSet ds = new DataSet();
            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pidc_cliente = idc_cliente;
                AgentesCOM com = new AgentesCOM();
                ds = com.SP_clientes_articulos_master(entidad);
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
                            dtrow["nombre2"] = row["desart"] + " || " + row["unimed"];
                            dtrow["Codigo"] = row["codigo"];
                            dtrow["Descripcion"] = row["desart"];
                            ///dtrow["incluir"] = False
                            ///dtrow["precio_modelo"] = row["precio_modelo"]
                            dtrow["precio"] = row["precio_cliente"];
                            ///dtrow["dias"] = row["dias"]
                            dtrow["Master"] = row["Master"];
                            ///dtrow["id_grupo"] = row["id_grupo"]
                            dtrow["decimales"] = row["decimales"];
                            if (row["precio_real"].ToString() != "")
                            {
                                if (Convert.ToDecimal(row["precio_real"]) > 0)
                                {
                                    dtrow["PrecioReal"] = row["precio_real"];
                                    dtrow["Descuento"] = Convert.ToDecimal(row["precio_cliente"]) - Convert.ToDecimal(row["precio_real"]);
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
                        //cbomaster.Attributes("style") = "width:85%;"
                        controles_busqueda_prod(false);
                        controles_busqueda_prod_sel_cancel(false);
                        controles_busqueda_master(true);
                        txtcodigo.Visible = false;
                        dt.Columns.Remove("nombre2");
                        ViewState["dt_master_cotizacion"] = dt;
                        Session["dt_productos_busqueda"] = ds.Tables[0];
                        btn_seleccionar_master.Attributes["onclick"] = "return editar_articulo();";
                    }
                    else
                    {
                        //cbomaster.Attributes("style") = "width:85%;"
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void estado_controles_cliente(bool estado)
        {
            cboclientes.Visible = estado;
        }

        public void estado_controles_cliente2(bool estado)
        {
            txtcliente.Visible = estado;
            lnkbuscar.Visible = estado;
        }

        public DataTable agregar_columnas_dataset2()
        {
            //MIC agregado de pedidos7_m 15-05-2015 codigo: 2
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

        // <summary>
        /// Redondea un valor con fracciones decimales (2 digitos decimales)
        /// </summary>
        /// <param name="valor">Valor a Redondear</param>
        /// <returns>Valor redondeado a dos digitos</returns>
        /// <remarks></remarks>
        public string Redondeo_Dos_Decimales(decimal valor)
        {
            try
            {
                valor = Math.Round(valor, 2);
                return valor.ToString("#.##");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Regresa un numero entero con cuatro digitos decimales
        /// </summary>
        /// <param name="valor">Valor a convertir con cuatro digitos decimales</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Redondeo_cuatro_decimales(double valor)
        {
            try
            {
                valor = Math.Round(valor, 4);
                return valor.ToString("#.####");
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public void cargar_subtotal_iva_total(double t_iva)
        {
            t_iva = t_iva / 100;
            //Se divide para sacar el %.
            DataTable dt = new DataTable();
            dt = ViewState["dt_c_correo"] as DataTable;
            double total = 0;
            double subtotal = 0;
            double iva = 0;
            double maniobras = 0;
            double iva_maniobras = 0;
            double total_maniobras = 0;

            if (txtrfc.Text.Trim().StartsWith("*"))
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    //4454= Maniobras
                    if (!(Convert.ToInt32(dt.Rows[i]["idc_articulo"]) == 4454))
                    {
                        subtotal = subtotal + Convert.ToDouble(dt.Rows[i]["Importe"]);
                    }
                    else
                    {
                        //maniobras = maniobras + dt.Rows[i]["importe")
                    }
                }
                iva = 0;
                iva_maniobras = 0;
                total = total + subtotal + iva;
                maniobras = maniobras + (maniobras * t_iva);
                total_maniobras = maniobras;
            }
            else
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (!(Convert.ToInt32(dt.Rows[i]["idc_articulo"]) == 4454))
                    {
                        subtotal = subtotal + Convert.ToDouble(dt.Rows[i]["Importe"]);
                        iva = iva + (t_iva * Convert.ToDouble(dt.Rows[i]["Importe"]));
                    }
                    else
                    {
                        maniobras = maniobras + Convert.ToDouble(dt.Rows[i]["Importe"]);
                        iva_maniobras = iva_maniobras + (t_iva * Convert.ToDouble(dt.Rows[i]["Importe"]));
                    }
                }
                maniobras = txtmaniobrassub.Text==""?0: Convert.ToDouble(txtmaniobrassub.Text.Trim());
                iva_maniobras = Convert.ToDouble(maniobras);
                total_maniobras = iva_maniobras + maniobras;
                //total_maniobras = maniobras + iva_maniobras
                total = subtotal + iva;
            }
            txtmaniobrassub.Text = maniobras.ToString("N2") ==""?"0.00":maniobras.ToString("N2");
            txtiva2.Text = iva_maniobras.ToString("N2" == ""?"0.00": iva_maniobras.ToString("N2"));
            txttotalmaniobras.Text = total_maniobras.ToString("N2") ==""?"0.00":total_maniobras.ToString("N2");

            txtsubtotal.Text = subtotal.ToString("N2") ==""?"0.00": subtotal.ToString("N2");
            txtiva1.Text = iva.ToString("N2") ==""?"0.00":iva.ToString("N2");
            txttotal1.Text = total.ToString("N2") ==""?"0.00": total.ToString("N2");

            txttotal.Text = (total_maniobras + total).ToString("N2") ==""?"0.00" : (total_maniobras + total).ToString("N2");
        }

        protected void lnkbuscaraceptar_Click(object sender, EventArgs e)
        {
            if (cboclientes.Items.Count > 0)
            {
                int idc_cliente = Convert.ToInt32(cboclientes.SelectedValue);
                cargar_datos_cliente(idc_cliente);
            }
            else
            {
                Alert.ShowAlertError("Seleccione un valor valido", this);
            }
        }

        protected void lnkbuscar_Click(object sender, EventArgs e)
        {
            buscar_datos_cliente(txtcliente.Text);
        }

        protected void cboentrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tipo = Convert.ToInt32(cboentrega.SelectedValue);
            if (tipo == 1)
            {
                lnkconsignado.Text = "Consignado";
            }
            else if (tipo == 3)
            {
                lnkconsignado.Text = "Detalles Recoge Cliente";
            }
        }

        public void WriteMsgBox(string msg)
        {
            Alert.ShowAlertError(msg, this.Page);
        }

        public void Ajustes_iva(decimal nuevo_iva, decimal iva_ant)
        {
            DataTable dt = ViewState["dt_c_correo"] as DataTable;
            if (txtrfc.Text.StartsWith("*"))
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    dt.Rows[i][5] = Convert.ToDecimal(dt.Rows[i][5]) / (1 + (iva_ant / 100)) * (1 + (nuevo_iva / 100));
                    dt.Rows[i][6] = Redondeo_Dos_Decimales(Convert.ToDecimal(dt.Rows[i][5]) * Convert.ToDecimal(dt.Rows[i][4]));
                    dt.Rows[i][7] = Redondeo_cuatro_decimales(Convert.ToDouble(dt.Rows[i][5]) - Convert.ToDouble(dt.Rows[i][8]));
                }

                cargar_subtotal_iva_total(Convert.ToDouble(nuevo_iva));
            }
            else
            {
                cargar_subtotal_iva_total(Convert.ToDouble(nuevo_iva));
            }
            dt = ViewState["dt_c_correo"] as DataTable;
        }

        public bool validar_cambio_iva_Frontera()
        {
            if (!string.IsNullOrEmpty(txtidc_colonia.Text.Trim()) & txtidc_colonia.Text.Trim() != "0")
            {
                DataRow row = default(DataRow);
                try
                {
                    AgentesCOM com = new AgentesCOM();
                    DataSet ds = com.sp_cambiar_iva_frontera(Session["idc_sucursal"] == null ? 0 : Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(txtidc_colonia.Text));
                    row = ds.Tables[0].Rows[0];
                    bool bien = Convert.ToBoolean(row["bien"]);
                    if (bien == false)
                    {
                        if (!(Session["NuevoIva"] == row[2]))
                        {
                            Session["ivaant"] = Session["NuevoIva"];
                            Session["idc_ivaant"] = Session["nuevo_idc_iva"];
                            Session["NuevoIva"] = row[2].ToString();
                            Session["nuevo_idc_iva"] = row[1].ToString();
                            Session["pidc_iva"] = row[2].ToString();
                            WriteMsgBox("El IVA no Corresponde a la Zona de Entrega se Recalculara el IVA al " + Convert.ToInt32(Session["NuevoIva"]).ToString() + "%");
                            Ajustes_iva(Convert.ToDecimal(Session["NuevoIva"]), Convert.ToDecimal(Session["ivaant"]));
                            etiqueta_Iva(Session["NuevoIva"] as string);
                            return true;
                        }
                    }
                    else if (bien == true)
                    {
                        string v1 = Session["lidc_iva"] as string;
                        string v2 = Session["pidc_iva"] as string;
                        if (v1 != v2)
                        {
                            Session["pidc_iva"] = Session["lidc_iva"];
                            Session["ivaant"] = Session["NuevoIva"];
                            Session["idc_ivaant"] = Session["nuevo_idc_iva"];
                            Session["NuevoIva"] = row[2].ToString();
                            Session["nuevo_idc_iva"] = row[1].ToString();
                            WriteMsgBox("El IVA no Corresponde a la Zona de Entrega se Recalculara el IVA al " + Convert.ToInt32(Session["NuevoIva"]).ToString() + "%");
                            Ajustes_iva(Convert.ToDecimal(Session["NuevoIva"]), Convert.ToDecimal(Session["ivaant"]));
                            etiqueta_Iva(Session["NuevoIva"] as string);
                            return true;
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Alert.ShowAlertError(ex.ToString(), this.Page);
                    Global.CreateFileError(ex.ToString(), this);
                    return false;
                }
            }
            else
            {
                if (!(Session["NuevoIva"] == Session["Xiva"]))
                {
                    Session["ivaant"] = Session["NuevoIva"];
                    Session["NuevoIva"] = Session["Xiva"];
                    Ajustes_iva(Convert.ToDecimal(Session["NuevoIva"]), Convert.ToDecimal(Session["ivaant"]));
                    etiqueta_Iva(Session["NuevoIva"] as string);

                    return false;
                }
                return false;
            }
        }

        public int cedis_prg(int idc_sucursal)
        {
            int pxid = 0;
            try
            {
                AgentesCOM com = new AgentesCOM();
                DataTable dt = com.sp_fn_cedis_sucursal(idc_sucursal).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    pxid = Convert.ToInt32(dt.Rows[0]["pxid"]);
                    return pxid;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
                return 0;
            }
        }

        public int m_pxsuc(string consulta)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = funciones.ExecQuery(consulta);
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

        public DataTable c_precios_art(string consulta)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = funciones.ExecQuery(consulta);
                return dt;
            }
            catch (Exception ex)
            {
                WriteMsgBox("No se Procedio a Consultar Precios. \\n-\\n" + ex.Message);
                return dt;
            }
        }

        public int suc_cercana()
        {
            int sucent = 0;
            try
            {
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_fn_sucursal_mas_cercana(Convert.ToInt32(txtidc_colonia.Text));
                sucent = Convert.ToInt32(ds.Tables[0].Rows[0]["sucent"]);
                return sucent;
            }
            catch (Exception ex)
            {
                WriteMsgBox("No se ha procedido a Verificar Sucursal mas Cercana \\n \\u000B \\n" + ex.Message + "\\n \\u000B \\n");
                return 0;
            }
        }

        public void Calcular_Valores_DataTable()
        {
            DataTable dt = new DataTable();
            //esto 20-05-2015
            //dt = ViewState("dt")
            //por esto 20-05-2015
            dt = ViewState["dt_c_correo"] as DataTable;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dt.Rows[i][6] = Redondeo_Dos_Decimales(Convert.ToDecimal(dt.Rows[i][4])* Convert.ToDecimal(dt.Rows[i][5]));
                //Importe = Cantidad * Precio
                if (Convert.ToBoolean(dt.Rows[i]["nota_credito"]) == false)
                {
                    dt.Rows[i][8] = Convert.ToDecimal(dt.Rows[i]["precio"]) - Convert.ToDecimal(dt.Rows[i][7]);
                    dt.Rows[i][8] = Convert.ToDecimal(dt.Rows[i][8]).ToString("#.####");
                }
            }
        }

        public void Productos_Calculados()
        {
            DataTable dt = new DataTable();
            //esto 20-05-2015
            //dt = ViewState("dt")
            //por esto 20-05-2015
            dt = ViewState["dt_c_correo"] as DataTable;
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
                    dt.Rows[i][5] = Redondeo_cuatro_decimales(valor_calculado);
                    dt.Rows[i][6] = Redondeo_Dos_Decimales(Convert.ToDecimal(dt.Rows[i][4]) * Convert.ToDecimal(dt.Rows[i][5]));
                    dt.Rows[i][7] = Redondeo_cuatro_decimales(Convert.ToDouble(dt.Rows[i][5]));
                }
            }
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
            //MIC codigo: 1
            if (string.IsNullOrEmpty(txtsucursalr.Text))
            {
                txtsucursalr.Text = "0";
            }
            int iva_rec_suc = Convert.ToInt32(txtsucursalr.Text);
            int vidc_sucursal = 0;

            if (string.IsNullOrEmpty(txtidc_colonia.Text) | txtidc_colonia.Text == 0.ToString())
            {
                vidc_sucursal = Session["idc_sucursal"] == null ? 0 : Convert.ToInt32(Session["idc_sucursal"]);

                if (Convert.ToInt32(Session["pxidc_sucursal"]) != vidc_sucursal)
                {
                    vmotivo = "(Regresa a su Lista de Precios.)";
                }

                if (txtsucursalr.Text == 0.ToString())
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
                if (cedis_sucursal != Convert.ToInt32(Session["cedisprecios"]) & Convert.ToInt32(txtsucursalr.Text) > 0)
                {
                    vetiqueta = true;
                    vmotivo = "(La Sucursal de Entrega no corresponde a la Lista de Precios del Cliente.)";
                }
                vccambio = true;
            }
            else
            {
                string idc_sucursal = Session["idc_sucursal"] as string;
                int pxsuc = 0;
                if (Convert.ToInt32(txtlistap.Text) > 0)
                {
                    pxsuc = m_pxsuc("select dbo.fn_corresponde_colonia_cliente_sin_publico(" + txtid.Text + "," + txtidc_colonia.Text.Trim() + ") as pxsuc");
                }
                else
                {
                    pxsuc = m_pxsuc("select dbo.fn_corresponde_colonia_cliente_publico(" + Session["idc_sucursal"] as string + "," + txtidc_colonia.Text.Trim() + ") as pxsuc");
                }
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
            dt = ViewState["dt_c_correo"] as DataTable;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    vnum = vnum + 1;
                    varticulos = varticulos + dt.Rows[i]["idc_articulo"] + ";";
                }
            }

            if (vccambio == false)
            {
                return;
            }

            DataTable dt_precios = new DataTable();

            if (vidc_sucursal > 0)
            {
                if (voriginal == false)
                {
                    dt_precios = c_precios_art("select * from dbo.fn_precios_articulos_LISTA('" + varticulos + "'," + vnum.ToString() + "," + vidc_sucursal + "," + txtid.Text.Trim() + ")");
                }
                else
                {
                    dt_precios = c_precios_art("select * from dbo.fn_precios_articulos_LISTA_lp_suc('" + varticulos + "'," + vnum.ToString() + "," + txtlistap.Text.Trim() + "," + txtid.Text.Trim() + "," + Session["idc_sucursal"] as string + ")");
                }
            }
            else
            {
                vidc_listap = Convert.ToInt32(txtlistap.Text);
                if (vidc_listap == 0)
                {
                    dt_precios = c_precios_art("select * from dbo.fn_precios_articulos_LISTA_lp_suc('" + varticulos + "'," + vnum.ToString() + "," + vidc_listap.ToString() + "," + txtid.Text.Trim() + "," + Session["idc_sucursal"] as string + ")");
                }
                else
                {
                    dt_precios = c_precios_art("select * from dbo.fn_precios_articulos_LISTA_lp('" + varticulos + "'," + vnum.ToString() + "," + vidc_listap.ToString() + "," + txtid.Text.Trim() + ")");
                }
            }

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
            
                        }
                    }
                }
            }

            //esto 20-05-2015
            //dt = ViewState("dt")
            //por esto 20-05-2015
            dt = ViewState["dt_c_correo"] as DataTable;
            lblroja.Visible = vetiqueta;
            Calcular_Valores_DataTable();
            Productos_Calculados();
            Ajustes_iva(Convert.ToDecimal(Session["NuevoIva"]), Convert.ToDecimal(Session["ivaant"]));
            cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
        
    }

    protected void lnkcaptura_Click(object sender, EventArgs e)
    {
        try
        {
            int tipo = Convert.ToInt32(cboentrega.SelectedValue);
            if (tipo == 1)
            {
                if (validar_datos_dir() == false)
                {
                    Alert.ShowAlertError("Faltan de Completar Datos en el Consignado...Es Obligatorio.", this);
                    txtcodigo.Text = "";
                }
                lnkconsignado.Text = "Consignado";
                lnkconsignado.Font.Strikeout = false;
            }
            else if (tipo == 3)
            {
                if (txtsucursalr.Text == "0" | string.IsNullOrEmpty(txtsucursalr.Text))
                {
                    Alert.ShowAlertError("Falta Completar Datos de Donde va Recoger el Cliente.", this);
                    txtcodigo.Text = "";
                }
                lnkconsignado.Text = "Detalle Recoge Cliente";
                lnkconsignado.Font.Strikeout = false;
            }

            //lnkcaptura.Enabled = false;
            //cboentrega.Enabled = false;
            txt_consignado.Text = 1.ToString();
            int idc_cliente = Convert.ToInt32(txtid.Text.Trim());
            lista_p(idc_cliente);
            prep_cargar_grid_prod_master_cliente(idc_cliente);
            estado_controles_cliente2(true);
            estado_controles_cliente(false);
            controles_busqueda_master(true);
            controles_busqueda_prod(false);
            controles_busqueda_prod_sel_cancel(false);
            txtcodigo.Attributes.Remove("onfocus");
            txtcodigo.Focus();
            agregar_columnas_dataset();
            validar_cambio_iva_Frontera();
            actualizar_precios(idc_cliente, txtidc_colonia.Text==""?0: Convert.ToInt32(txtidc_colonia.Text.Trim()));
            object[] datos_clientes_pedidos = {
                    txtid.Text.Trim(),
                    txtrfc.Text.Trim(),
                    txtlistap.Text.Trim(),
                    lblroja.Visible,
                    (!string.IsNullOrEmpty(txtidc_colonia.Text.Trim()) ? txtidc_colonia.Text : "0")
                };
            Session["datos_clientes_pedidos"] = datos_clientes_pedidos;
        }
        catch (Exception ex)
        {
            Alert.ShowAlertError(ex.ToString(), this.Page);
            Global.CreateFileError(ex.ToString(), this);
        }
        finally
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_process", "<script>myStopFunction_busq();</script>", false);
        }
    }

    protected void lnkconsignado_Click(object sender, EventArgs e)
    {
        try
        {
            int tipo = Convert.ToInt32(cboentrega.SelectedValue);
            if (tipo == 1)
            {
                cbosucursales.Visible = false;
                String url = "Consignado_mobile.aspx?id=" + funciones.deTextoa64(txtid.Text) + "&consignado=" + funciones.deTextoa64(txt_consignado.Text) + "";
                ScriptManager.RegisterStartupScript(this, GetType(), "swssssw", "window.open('" + url + "');", true);
            }
            else if (tipo == 3)
            {
                msgerror.Visible = false;
                lblmsg.Text = "Seleccione una Sucursal";
                cargar_combo();
                cbosucursales.Visible = true;
                Session["Caso_ConfirmacionSucursal"] = "Guardar Recoge";

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirmRecoge('Mensaje del Sistema','Seleccione Sucursal Donde Recoge Cliente','modal fade modal-info');", true);
            }
        }
        catch (Exception ex)
        {
            Alert.ShowAlertError(ex.ToString(), this.Page);
            Global.CreateFileError(ex.ToString(), this);
        }
    }

    protected void Yes2_Click(object sender, EventArgs e)
    {
        try
        {
            msgerror.Visible = false;
            string caso = (string)Session["Caso_ConfirmacionSucursal"];
            switch (caso)
            {
                case "Guardar Recoge":
                    if (cbosucursales.SelectedIndex == 0)
                    {
                        msgerror.Visible = true;
                        lblmsg.Text = "Seleccione una Sucursal";
                        Session["Caso_ConfirmacionSucursal"] = "Guardar Recoge";
                        lnkconsignado.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirmRecoge('Mensaje del Sistema','Seleccione Sucursal Donde Recoge Cliente','modal fade modal-info');", true);
                    }
                    else
                    {
                        txtsucursalr.Text = cbosucursales.SelectedValue.ToString();
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            Alert.ShowAlertError(ex.ToString(), this.Page);
            Global.CreateFileError(ex.ToString(), this);
        }
    }

        protected void btn_seleccionar_master_Click(object sender, EventArgs e)
        {

        }

        protected void btnbuscar_codigo_Click(object sender, EventArgs e)
        {
            controles_busqueda_master(false);
            controles_busqueda_prod(true);
            txtcodigo.Enabled = true;
            txtcodigo.Attributes.Remove("onfocus");
            txtcodigo.Focus();
        }

        protected void btnmaster_Click(object sender, EventArgs e)
        {
            string name = sender.ToString();
            prep_cargar_grid_prod_master_cliente(Convert.ToInt32(txtid.Text.Trim()));
            controles_busqueda_master(true);
            controles_busqueda_prod(false);
            controles_busqueda_prod_sel_cancel(false);
        }

        protected void btnguardar_edit_Click(object sender, EventArgs e)
        {
            ViewState["dt_c_correo"] = Session["dt_productos_lista"];
            calcular_valores();
            Articulos_Calculados();
            cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
            limpiar_controles();
            txtcodigo.Focus();
            lbl_idc.Text = "";
            //btncapturar.Enabled = True
            formar_cadenas();
            cargar_proyectos_cliente(Convert.ToInt32(txtid.Text.Trim()));
            gridprodcotizados.DataSource = ViewState["dt_c_correo"] as DataTable;
            gridprodcotizados.DataBind();
            if (!(cbomaster.Visible == true))
            {
                txtcodigo.Attributes.Remove("onfocus");
                txtcodigo.Text = "";
                txtcodigo.Focus();
                controles_busqueda_prod(true);
                controles_busqueda_prod_sel_cancel(false);
            }
        }

        public void Articulos_Calculados()
        {
            DataTable dt = new DataTable();
            dt = ViewState["dt_c_correo"] as DataTable;
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
                    dt.Rows[i][5] = Redondeo_cuatro_decimales(valor_calculado);
                    dt.Rows[i][6] = Redondeo_Dos_Decimales(Convert.ToDecimal(dt.Rows[i][4]) * (Convert.ToDecimal(dt.Rows[i][5])));
                    dt.Rows[i][7] = Redondeo_cuatro_decimales(Convert.ToDouble(dt.Rows[i][5]));
                }
            }
        }
        public void cargar_proyectos_cliente(int idc_cliente)
        {
            DataSet ds = new DataSet();
            try
            {
                AgentesCOM componente = new AgentesCOM();
                AgentesENT entidad = new AgentesENT();
                entidad.Pidc_cliente = idc_cliente;
                ds = componente.SP_VER_PROYECTOS(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["XProyectos"] = ds.Tables[0];
                }

            }
            catch (Exception ex)
            {
            }
        }
        public void calcular_valores()
        {
            DataTable dt =  ViewState["dt_c_correo"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    dt.Rows[i]["importe"] =Convert.ToDecimal(dt.Rows[i]["cantidad"]) * Convert.ToDecimal(dt.Rows[i]["precio"]);
                }
                ViewState["dt_c_correo"] = dt;
            }
        }
        public void limpiar_controles()
        {
            txtcodigo.Text = "";
        }
        public void formar_cadenas()
        {
            if (!string.IsNullOrEmpty(txtidc_colonia.Text))
            {
                DataTable dt = new DataTable();
                dt = ViewState["dt_c_correo"] as DataTable;
                string cadena1 = "";
                string cadena2 = "";
                DataSet ds = default(DataSet);
                DataRow row = default(DataRow);
                int total = 0;
                if (!(dt.Rows.Count > 0))
                {
                    return;
                }
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    row = dt.Rows[i];
                    cadena1 = cadena1 + row["idc_articulo"].ToString() + ";" + row["Cantidad"].ToString() + ";" + row["Precio"].ToString() + ";" + row["PrecioReal"].ToString() + ";";
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
                    AgentesCOM componente = new AgentesCOM();
                    ds = componente.SP_Cadenas_Fletes_Preped(cadena1, total, 18,Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(txtidc_colonia.Text.Trim()), 
                        Convert.ToInt32(txtid.Text.Trim()), desg_iva, Convert.ToInt32(Session["Xiva"]), cadena2);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (txtrfc.Text.Trim().StartsWith("*"))
                        {
                            txtmaniobrassub.Text = Redondeo_Dos_Decimales(Convert.ToDecimal(ds.Tables[0].Rows[0][0]) * ((Convert.ToDecimal(Session["Xiva"]) / 100) + 1));
                        }
                        else
                        {
                            txtmaniobrassub.Text = Redondeo_Dos_Decimales(Convert.ToDecimal(ds.Tables[0].Rows[0][0]))==""?"0": Redondeo_Dos_Decimales(Convert.ToDecimal(ds.Tables[0].Rows[0][0]));
                            txtiva2.Text = Redondeo_Dos_Decimales(Convert.ToDecimal(ds.Tables[0].Rows[0][0]) * ((Convert.ToDecimal(Session["Xiva"]) / 100)))==""?"0": Redondeo_Dos_Decimales(Convert.ToDecimal(ds.Tables[0].Rows[0][0]) * ((Convert.ToDecimal(Session["Xiva"]) / 100)));
                            txttotalmaniobras.Text = (Convert.ToDouble(txtmaniobrassub.Text.Trim()) + Convert.ToDouble(txtiva2.Text.Trim())).ToString("#.##")==""?"0.00": (Convert.ToDouble(txtmaniobrassub.Text.Trim()) + Convert.ToDouble(txtiva2.Text.Trim())).ToString("#.##");
                        }
                        cargar_subtotal_iva_total(Convert.ToDouble(Session["Xiva"]));
                    }
                    else
                    {
                        txtmaniobrassub.Text = "0.00";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void btnref_Click(object sender, EventArgs e)
        {

            DataRow row = Session["rowprincipal"] as DataRow;
            DataRow dt_row = default(DataRow);
            DataTable dt = default(DataTable);
            dt = ViewState["dt_c_correo"] as DataTable;
            dt_row = dt.NewRow();
            //MIC 14-05-2015 add try catch
            try
            {

                for (int i = 0; i <= row.Table.Columns.Count - 1; i++)
                {
                    dt_row[i]= row[i];
                }

                dt.Rows.Add(dt_row);
                ViewState["dt_c_correo"] = dt;
                calcular_valores();
                Articulos_Calculados();
                cargar_subtotal_iva_total(Convert.ToDouble(Session["NuevoIva"]));
                formar_cadenas();
               // btnnuevac.Enabled = true;

                Session["rowprincipal"] = null;
                gridprodcotizados.DataSource = ViewState["dt_c_correo"] as DataTable;
                gridprodcotizados.DataBind();
               // btnenviar.Enabled = true;
                if (!(cbomaster.Visible == true))
                {
                    txtcodigo.Attributes.Remove("onfocus");
                    txtcodigo.Text = "";
                    txtcodigo.Focus();
                    controles_busqueda_prod(true);
                    controles_busqueda_prod_sel_cancel(false);
                }
                Session["dt_productos_lista"] = ViewState["dt_c_correo"] as DataTable;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_loading", "<script>myStopFunction_grid();</script>", false);
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error: \\n" + ex.Message);
            }
        }

        protected void gridprodcotizados_ItemCommand(object source, DataGridCommandEventArgs e)
        {

            DataTable dt = new DataTable();
            dt = ViewState["dt_c_correo"] as DataTable;
            TextBox cantidad = new TextBox();
            if (e.CommandName == "Editar")
            {
                gridprodcotizados.EditItemIndex = e.Item.ItemIndex;
                gridprodcotizados.DataSource = dt;
                gridprodcotizados.DataBind();

                cantidad = gridprodcotizados.Items[e.Item.ItemIndex].FindControl("txtcantidad") as TextBox;
                if (Convert.ToBoolean(dt.Rows[e.Item.ItemIndex]["Decimales"]) == true)
                {
                    cantidad.Attributes["onkeydown"] = "return soloNumeros(event,'True');";
                }
                else
                {
                    cantidad.Attributes["onkeydown"] = "return soloNumeros(event,'False');";
                }
                cantidad.Focus();
            }
            else if (e.CommandName == "Eliminar")
            {
                eliminar_row_dt(e.Item.ItemIndex);
            }
            else if (e.CommandName == "Guardar")
            {
                cantidad = gridprodcotizados.Items[e.Item.ItemIndex].FindControl("txtcantidad") as TextBox;
                if (string.IsNullOrEmpty(cantidad.Text))
                {
                    CargarMsgBox("Es Necesario Ingresar la Cantidad.");
                    cantidad.Focus();
                }
                if (cantidad.Text == 0.ToString())
                {
                    eliminar_row_dt(e.Item.ItemIndex);
                    return;
                }
                dt.Rows[e.Item.ItemIndex]["Cantidad"] = cantidad.Text.Trim();
                dt.Rows[e.Item.ItemIndex]["Importe"] = Convert.ToDecimal(cantidad.Text) * Convert.ToDecimal(dt.Rows[e.Item.ItemIndex]["Precio"]);
                gridprodcotizados.EditItemIndex = -1;
                gridprodcotizados.DataSource = dt;
                gridprodcotizados.DataBind();
            }
            else if (e.CommandName == "Cancelar")
            {
                gridprodcotizados.EditItemIndex = -1;
                gridprodcotizados.DataSource = dt;
                gridprodcotizados.DataBind();
            }
            cargar_subtotal_iva_total(Convert.ToInt32(Session["Xiva"]));
            formar_cadenas();

        }
        public void eliminar_row_dt(int index)
        {
            DataTable dt = new DataTable();
            dt = ViewState["dt_c_correo"] as DataTable;
            dt.Rows[index].Delete();           
            gridprodcotizados.EditItemIndex = -1;
            gridprodcotizados.DataSource = dt;
            gridprodcotizados.DataBind();
            txtcodigo.Focus();
            Session["dt_productos_lista"] = dt;
        }
        protected void gridprodcotizados_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try {
                if (e.Item.ItemType == ListItemType.AlternatingItem | e.Item.ItemType == ListItemType.Item)
                {
                    Label lblid = new Label();
                    lblid = e.Item.Cells[9].FindControl("lblid") as Label;
                    e.Item.Cells[1].Attributes["onclick"] = "return ver_ficha(" + lblid.Text + ");";
                    e.Item.Cells[1].Attributes["onmouseover"] = "cursor(this);";
                    e.Item.Cells[1].Attributes["onmouseout"] = "cursor_fuera(this);";

                    ImageButton btnmobile = new ImageButton();
                    btnmobile = e.Item.FindControl("imgmobile") as ImageButton;
                    btnmobile.Attributes["onclick"] = "return editar_precios_cantidad_1(" + lblid.Text.Trim() + ");";


                }
            } catch (Exception ex)
            {
                WriteMsgBox(" Error: \\n" + ex.Message);

            }
        }

        protected void btncancelar_edit_Click(object sender, EventArgs e)
        {
            if (!(cbomaster.Visible == true))
            {
                txtcodigo.Attributes.Remove("onfocus");
                txtcodigo.Text = "";
                txtcodigo.Focus();
                controles_busqueda_prod(true);
                controles_busqueda_prod_sel_cancel(false);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["idc_cliente"] == null)
            {
                Response.Redirect("cotizaciones_correo.aspx");
            }
            else
            {
                Response.Redirect("cotizaciones_correo.aspx?idc_cliente=" + Request.QueryString["idc_cliente"]);
            }

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["idc_cliente"] == null)
            {
                Response.Redirect("menu.aspx");
            } else {
                Response.Redirect("ficha_cliente_m.aspx");
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            try
            {
                if (ViewState["dt_c_correo"] == null)
                {
                    Alert.ShowAlertError("Seleccione un Articulo para Cotizar", this);
                    return;
                }
                DataTable dt = new DataTable();
                DataTable dt_final = new DataTable();
                dt = ViewState["dt_c_correo"] as DataTable;
                if (dt.Rows.Count <= 0)
                {
                    Alert.ShowAlertError("Seleccione un Articulo para Cotizar",this);
                    return;
                }
                else
                {
                    dt_final = dt.DefaultView.ToTable("dt1", false, "Descripcion", "UM", "Cantidad", "precio", "Importe");
                    if (dt_final.Rows.Count > 0)
                    {
                        dt_final.Columns["descripcion"].ColumnName = "Producto";
                        dt_final.Columns["UM"].ColumnName = "UM";
                        dt_final.Columns["Cantidad"].ColumnName = "Cantidad";
                        dt_final.Columns["precio"].ColumnName = "Precio";
                        dt_final.Columns["Importe"].ColumnName = "Subtotal";
                    }
                    
                    PdfDocument myPdfDocument = new PdfDocument(PdfDocumentFormat.A4);
                    
                    PdfTable myPdfTable = myPdfDocument.NewTable(new Font("Arial", 9), dt_final.Rows.Count, 5, 4);
                    myPdfTable.ImportDataTable(dt_final);
                    myPdfTable.HeadersRow.SetColors(System.Drawing.Color.White, System.Drawing.Color.Navy);
                    myPdfTable.SetColors(Color.Black, Color.White, Color.Gainsboro);
                    myPdfTable.SetBorders(Color.Black, 1, BorderType.Rows);
                    int[] columnasancho = {
                        40,
                        15,
                        15,
                        15,
                        15
                    };


                    myPdfTable.SetColumnsWidth(columnasancho);
                    myPdfTable.SetRowHeight(15);
                    myPdfTable.SetContentAlignment(ContentAlignment.MiddleCenter);

                    myPdfTable.Columns[0].SetContentAlignment(ContentAlignment.MiddleLeft);
                    myPdfTable.Columns[1].SetContentAlignment(ContentAlignment.MiddleCenter);
                    myPdfTable.Columns[2].SetContentAlignment(ContentAlignment.MiddleCenter);
                    myPdfTable.Columns[3].SetContentAlignment(ContentAlignment.MiddleRight);
                    myPdfTable.Columns[4].SetContentAlignment(ContentAlignment.MiddleRight);




                    string obs_lista = "Por medio de la presente, nos es grato saludarle y a la vez poner a su consideracion " 
                        +System.Environment.NewLine + " la cotizacion de precios de algunos materiales que manejamos para usted(es).";
                    string aviso2 = "(LISTA DE PRECIOS SUJETO A CAMBIOS SIN PREVIO AVISO)";

                    //Here we start the loop to generate the table...
                    while (!myPdfTable.AllTablePagesCreated)
                    {
                        PdfPage newPdfPage = myPdfDocument.NewPage();
                        PdfTablePage newPdfTablePAge = myPdfTable.CreateTablePage(new PdfArea(myPdfDocument, 10, 125, 580, 710));
                        PdfTextArea rfc = new PdfTextArea(new Font("Arial", 12, FontStyle.Bold), Color.Black, new PdfArea(myPdfDocument, 10, 55, 200, 30), ContentAlignment.MiddleLeft, txtrfc.Text.Trim());
                        PdfTextArea cliente = new PdfTextArea(new Font("Arial", 12, FontStyle.Bold), Color.Black, new PdfArea(myPdfDocument, 10, 70, txtnombre.Text.Length * 10, 30), ContentAlignment.MiddleLeft, txtnombre.Text.Trim());
                        PdfTextArea obs = new PdfTextArea(new Font("Arial", 12), Color.Black, new PdfArea(myPdfDocument, 10, 90, 500, 40), ContentAlignment.MiddleLeft, obs_lista);
                        //Dim empresa As PdfTextArea = New PdfTextArea(New Font("Arial", 12, FontStyle.Bold), Color.Black, New PdfArea(myPdfDocument, (myPdfDocument.PageWidth / 4), 2, 300, 80), ContentAlignment.MiddleCenter, empresa_datos)
                        PdfTextArea fecha = new PdfTextArea(new Font("Arial", 10), Color.Black, new PdfArea(myPdfDocument, 420, 2, 200, 80), ContentAlignment.MiddleCenter, DateTime.Now.ToString("dd MMMM, yyyy H:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")));
                        //Dim aviso_1 As PdfTextArea = New PdfTextArea(New Font("Arial", 10, FontStyle.Bold), Color.Black, New PdfArea(myPdfDocument, 10, 820, 200, 20), ContentAlignment.MiddleLeft, aviso)
                        PdfTextArea aviso_2 = new PdfTextArea(new Font("Arial", 10, FontStyle.Bold), Color.Black, new PdfArea(myPdfDocument, (myPdfDocument.PageWidth / 4), 820, 600, 20), ContentAlignment.MiddleLeft, aviso2);
                    
                        newPdfPage.Add(newPdfTablePAge);
                        newPdfPage.Add(rfc);
                        newPdfPage.Add(cliente);
                        newPdfPage.Add(obs);
                        newPdfPage.Add(fecha);
                        newPdfPage.Add(aviso_2);
                        newPdfPage.SaveToDocument();
                        if (myPdfTable.AllTablePagesCreated == true)
                        {
                            double y3 = newPdfTablePAge.Area.BottomRightCornerY;
                            DataTable totales = new DataTable();
                            totales.Columns.Add("Desc");
                            totales.Columns.Add("Subtotal");
                            totales.Columns.Add("Iva");
                            totales.Columns.Add("Total");
                            DataRow row = default(DataRow);
                            for (int i = 0; i <= 2; i++)
                            {
                                row = totales.NewRow();
                                totales.Rows.Add(row);
                            }
                            totales.Columns["Desc"].ColumnName = "    ";
                            totales.Rows[1][0] = "Maniobras:";
                            totales.Rows[0][1] = txtsubtotal.Text.Trim();
                            totales.Rows[0][2] = txtiva1.Text.Trim();
                            totales.Rows[0][3] = txttotal1.Text.Trim();
                            totales.Rows[0][3] = "    ";
                            //
                            totales.Rows[1][2] = txtiva2.Text.Trim();
                            totales.Rows[1][3] = txttotalmaniobras.Text.Trim();
                            totales.Rows[1][1] = txtmaniobrassub.Text.Trim();
                            totales.Rows[2][0] = "   ";
                            //
                            totales.Rows[2][1] = "   ";
                            totales.Rows[2][2] = "   ";
                            totales.Rows[2][3] = txttotal.Text.Trim();
                            int[] columnas = {
                                25,
                                25,
                                25,
                                25
                            };
                            PdfTable mypdftable2 = myPdfDocument.NewTable(new Font("Arial", 9), 3, 4, 4);
                            mypdftable2.SetRowHeight(12);
                            mypdftable2.ImportDataTable(totales);
                            mypdftable2.HeadersRow.SetColors(Color.Black, Color.Gainsboro);
                            mypdftable2.SetColors(Color.Black, Color.White);
                            mypdftable2.Columns[3].SetContentAlignment(ContentAlignment.MiddleRight);
                            mypdftable2.SetBorders(Color.Black, 1, BorderType.Rows);
                            mypdftable2.SetColumnsWidth(columnas);
                            while (!mypdftable2.AllTablePagesCreated)
                            {
                                
                                newPdfTablePAge = mypdftable2.CreateTablePage(new PdfArea(myPdfDocument, 290, y3 + 10, 300, 200));
                                newPdfPage.Add(newPdfTablePAge);
                                //newPdfPage.SaveToDocument()
                            }
                          
                            newPdfPage.SaveToDocument();
                        }

                        // Finally we save the docuement...
                    }

                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    Random random = new Random();
                    int randomNumber = random.Next(0, 100000);
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/tareas/"));//path local
                    string ruta = dirInfo + randomNumber .ToString().Trim()+ date + txtid.Text.Trim() + ".pdf";
                    myPdfDocument.SaveToFile(ruta);

                    ////// *Envia el PDF por Correo al cliente.*

                    string cuenta = "";
                    string pss = "";
                    string nombre_mostrar = "";
                    string correos_cliente = "";
                    string smtp1 = "";
                    int puerto = 0;
                    bool ssl = false;
                    if (File.Exists(ruta))
                    {
                        //GWebCN.Correos correo = new GWebCN.Correos();
                        DataSet ds = new DataSet();
                        //GWebCN.Clientes contactos_cliente = new GWebCN.Clientes();
                        AgentesCOM com = new AgentesCOM();
                        AgentesENT enti = new AgentesENT();
                        enti.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        ds = com.sp_correo_contraseña(enti);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            cuenta = ds.Tables[0].Rows[0]["correo"].ToString();
                            pss = ds.Tables[0].Rows[0]["contra"].ToString();
                            nombre_mostrar = ds.Tables[0].Rows[0]["nombre_mostrar"].ToString();
                        }
                        else if (ds.Tables[1].Rows.Count > 0)
                        {
                            cuenta = ds.Tables[1].Rows[0]["correo"].ToString();
                            pss = ds.Tables[1].Rows[0]["contra"].ToString();
                            nombre_mostrar = ds.Tables[0].Rows[0]["nombre_mostrar"].ToString();
                        }
                        else
                        {
                            CargarMsgBox("Error de Inicio de Sesión.");
                            return;
                        }
                        ds = com.sp_correos_lista_precios_cliente(Convert.ToInt32(txtid.Text.Trim()));


                        correos_cliente = "ventas@gamamateriales.com.mx";
                        if (string.IsNullOrEmpty(correos_cliente.Trim()))
                        {
                            CargarMsgBox("El Cliente no cuenta con Correo Electronico Registrado.");
                            return;
                        }
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            puerto = Convert.ToInt32(ds.Tables[1].Rows[0]["puerto"].ToString());
                            smtp1 = ds.Tables[1].Rows[0]["smtp"].ToString();
                            ssl = Convert.ToBoolean(ds.Tables[1].Rows[0]["ssl"].ToString());
                        }
                        else
                        {
                            CargarMsgBox("No se puede mandar el correo, error en datos de inicio de sesión.");
                            return;
                        }



                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(cuenta, nombre_mostrar, Encoding.UTF8);
                        mail.To.Add(correos_cliente);
                        mail.Bcc.Add("sistemas@gamamateriales.com.mx,programador3@gamamateriales.com.mx," + cuenta);
                        mail.Subject = "Cotizacion";
                        string text = "<h3>El archivo adjunto, es una cotización de algunos de los productos que ofrecemos para usted.</h3> <br/><br/><br/> <br/>";
                        AlternateView plainView = AlternateView.CreateAlternateViewFromString(text, Encoding.UTF8, MediaTypeNames.Text.Plain);
                        string html = "<h3>El archivo adjunto, es una cotización de algunos de los productos que ofrecemos para usted.</h3> <br/><br/><br/> <br/>" + "<img src='cid:imagen'/>";
                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
                        LinkedResource img = new LinkedResource(Server.MapPath("~/imagenes/Firma_gama.jpg"), MediaTypeNames.Image.Jpeg);
                        img.ContentId = "imagen";
                        img.TransferEncoding = TransferEncoding.Base64;
                        htmlView.LinkedResources.Add(img);
                        mail.AlternateViews.Add(plainView);
                        mail.AlternateViews.Add(htmlView);
                        mail.Attachments.Add(new Attachment(ruta));
                        NetworkCredential BasicAuthenticationInfo = new NetworkCredential(cuenta, pss);
                        SmtpClient smtp = new SmtpClient(smtp1);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = BasicAuthenticationInfo;
                        smtp.Port = puerto;
                        smtp.EnableSsl = ssl;
                        smtp.Timeout = 120000;
                        smtp.Send(mail);
                        string ruta_pdf = ruta;
                        mail.Attachments.Dispose();
                        mail.Dispose();
                        mail = null;
                        string url = "";
                        if (Request.QueryString["idc_cliente"] == null)
                        {
                            url="cotizaciones_correo.aspx";
                        }
                        else
                        {
                            url="cotizaciones_correo.aspx?idc_cliente=" + Request.QueryString["idc_cliente"];
                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERwsswswsT", "AlertGO('Cotizacion Enviada Correctamente.','"+ url + "');", true);
                    }

                }
            }
            catch (Exception ex)
            {
                CargarMsgBox(ex.Message);
            }


        }

        protected void txtcodigo_TextChanged(object sender, EventArgs e)
        {
           

        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            int idc_cliente = (string.IsNullOrEmpty(txtid.Text) ? 0 : Convert.ToInt32(txtid.Text));
            
            try
            {
                if ((txtcodigo.Text.Trim().Length < 3))
                {
                    Alert.ShowAlertError("Ingrear Minimo 3 Caracteres Para Realizar la Busqueda.", this);
                    return;
                }
                DataTable dt = new DataTable();
                dt = ViewState["dt_c_correo"] as DataTable;
                DataSet ds = new DataSet();
                GWebCN.Productos gweb = new GWebCN.Productos();
                DataRow row = default(DataRow);
                DataRow rowr = default(DataRow);
                row = dt.NewRow();
                DataRow[] rows = null;
                if (funciones.isNumeric(txtcodigo.Text))
                {
                    ds = gweb.buscar_productos(txtcodigo.Text, "A", Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(Session["sidc_usuario"]));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rowr = ds.Tables[0].Rows[0];
                        row["idc_articulo"] = rowr["idc_articulo"];
                        rows = dt.Select("idc_articulo=" + row["idc_articulo"].ToString());
                        if (rows.Length > 0)
                        {
                            Alert.ShowAlertError("El Articulo ya Esta Capturado.", this);
                            txtcodigo.Text = "";
                            txtcodigo.Focus();
                            return;
                        }
                        object[] datos = null;
                        datos = buscar_precio(Convert.ToInt32(row["idc_articulo"]));
                        row["Codigo"] = rowr["Codigo"];
                        row["Descripcion"] = rowr["desart"];
                        row["UM"] = rowr["unimed"];
                        row["Precio"] = datos[0];
                        row["PrecioReal"] = datos[4];
                        row["Descuento"] = datos[2];
                        row["Decimales"] = rowr["decimales"];
                        row["Paquete"] = rowr["paquete"];
                        row["precio_libre"] = rowr["precio_libre"];
                        row["comercial"] = rowr["comercial"];
                        row["fecha"] = rowr["fecha"];
                        row["obscotiza"] = rowr["obscotiza"];
                        row["vende_exis"] = rowr["vende_exis"];
                        row["minimo_venta"] = rowr["minimo_venta"];
                        row["Master"] = true;
                        row["mensaje"] = rowr["mensaje"];
                        row["Porcentaje"] = calculado(Convert.ToInt32(row["idc_articulo"]));
                        row["Calculado"] = Convert.ToDecimal(row["Porcentaje"]) > 0;
                        row["Nota_Credito"] = datos[3];
                        row["Anticipo"] = rowr["anticipo"];
                        row["Costo"] = datos[1];
                        if (Convert.ToBoolean(row["Calculado"]))
                        {
                            row["Cantidad"] = 1;
                            row["Existencia"] = 1;
                            dt.Rows.Add(row);
                            ViewState["dt_c_correo"] = dt;
                            calcular_valores();
                            Articulos_Calculados();
                            gridprodcotizados.DataSource = ViewState["dt_c_correo"] as DataTable;
                            gridprodcotizados.DataBind();
                            limpiar_controles();

                            controles_busqueda_prod(true);
                            controles_busqueda_prod_sel_cancel(false);
                            txtcodigo.Attributes.Remove("onfocus");
                            txtcodigo.Text = string.Empty;
                        }
                        else
                        {
                            Session["dt_productos_busqueda"] = ds.Tables[0];
                            ds.Tables[0].Columns.Add("nombre2");
                            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                ds.Tables[0].Rows[i]["nombre2"] = ds.Tables[0].Rows[i]["desart"].ToString() + " || " + ds.Tables[0].Rows[i]["unimed"].ToString();
                            }
                            controles_busqueda_prod_sel_cancel(true);
                            //controles_busqueda_prod(False)
                            txtcodigo.Text = string.Empty;
                            btnbuscar_codigo.Visible = false;
                            cboproductos.DataSource = ds.Tables[0];
                            cboproductos.DataTextField = "nombre2";
                            cboproductos.DataValueField = "idc_articulo";
                            cboproductos.DataBind();
                            btn_seleccionar_master.Visible = true;
                            btn_seleccionar_master.Attributes["onclick"] = "return editar_articulo();";

                        }
                    }
                    else
                    {
                        ds = gweb.buscar_productos(txtcodigo.Text, "D", Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(Session["sidc_usuario"]));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Session["dt_productos_busqueda"] = ds.Tables[0];
                            ds.Tables[0].Columns.Add("nombre2");
                            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                ds.Tables[0].Rows[i]["nombre2"] = ds.Tables[0].Rows[i]["desart"].ToString() + " || " + ds.Tables[0].Rows[i]["unimed"].ToString();
                            }
                            controles_busqueda_prod_sel_cancel(true);
                            //controles_busqueda_prod(False)
                            txtcodigo.Text = string.Empty;
                            btnbuscar_codigo.Visible = false;
                            cboproductos.DataSource = ds.Tables[0];
                            cboproductos.DataTextField = "nombre2";
                            cboproductos.DataValueField = "idc_articulo";
                            cboproductos.DataBind();
                            btn_seleccionar_master.Visible = true;
                            btn_seleccionar_master.Attributes["onclick"] = "return editar_articulo();";
                        }
                        else
                        {
                            CargarMsgBox("No se encontro articulo con esa descripción");
                            txtcodigo.Focus();
                        }
                    }
                }
                else
                {
                    ds = gweb.buscar_productos(txtcodigo.Text, "C", Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(Session["sidc_usuario"]));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //gridresultadosbusqueda.DataSource = ds
                        //gridresultadosbusqueda.DataBind()
                        //mpeSeleccion.Show()
                        Session["dt_productos_busqueda"] = ds.Tables[0];
                        ds.Tables[0].Columns.Add("nombre2");
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            ds.Tables[0].Rows[i]["nombre2"] = ds.Tables[0].Rows[i]["desart"].ToString() + " || " + ds.Tables[0].Rows[i]["unimed"].ToString();
                    }
                        controles_busqueda_prod_sel_cancel(true);
                        //controles_busqueda_prod(False)
                        txtcodigo.Text = string.Empty;
                        btnbuscar_codigo.Visible = false;
                        //cboproductos.Attributes("style") = "width:100%"
                        cboproductos.DataSource = ds.Tables[0];
                        cboproductos.DataTextField = "nombre2";
                        cboproductos.DataValueField = "idc_articulo";
                        cboproductos.DataBind();
                        btn_seleccionar_master.Visible = true;
                        btn_seleccionar_master.Attributes["onclick"] = "return editar_articulo();";
                    }
                    else
                    {
                        ds = gweb.buscar_productos(txtcodigo.Text, "B", Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(Session["sidc_usuario"]));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //gridresultadosbusqueda.DataSource = ds
                            //gridresultadosbusqueda.DataBind()
                            //mpeSeleccion.Show()
                            Session["dt_productos_busqueda"] = ds.Tables[0];
                            ds.Tables[0].Columns.Add("nombre2");
                            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                ds.Tables[0].Rows[i]["nombre2"] = ds.Tables[0].Rows[i]["desart"].ToString() + " || " + ds.Tables[0].Rows[i]["unimed"].ToString();
                            }
                            controles_busqueda_prod_sel_cancel(true);
                            //controles_busqueda_prod(False)
                            txtcodigo.Text = string.Empty;
                            btnbuscar_codigo.Visible = false;
                            //cboproductos.Attributes("style") = "width:100%"
                            cboproductos.DataSource = ds.Tables[0];
                            cboproductos.DataTextField = "nombre2";
                            cboproductos.DataValueField = "idc_articulo";
                            cboproductos.DataBind();
                            btn_seleccionar_master.Visible = true;
                            btn_seleccionar_master.Attributes["onclick"] = "return editar_articulo();";
                        }
                        else
                        {
                            CargarMsgbox("No se encontro articulo con esa descripción");
                        }
                    }
                }
                //Session["dt_productos_busqueda_cot"]
            }
            catch (Exception ex)
            {
                CargarMsgbox(ex.Message);
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar_process", "<script>myStopFunction_busq();</script>", false);
            }
            


        }



        public double calculado(int idc_articulo)
        {
            try
            {
                GWebCN.Productos gweb = new GWebCN.Productos();
                DataSet datos = new DataSet();
                DataRow row = default(DataRow);
                datos = gweb.Articulo_calculado(idc_articulo);
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
                CargarMsgBox(ex.ToString());
                return 0;
            }
        }


        public object[] buscar_precio(int idc_articulo)
        {
            object[] datos = new object[5];
            try
            {
                // 0=Precio, 1=Costo , 2=Descuento, 3=Nota_Credito, 4=Precio_Real
                GWebCN.Productos gweb = new GWebCN.Productos();
                DataSet ds = new DataSet();
                DataRow row = default(DataRow);
                DataRow rowprincipal = default(DataRow);
                rowprincipal = Session["rowprincipal"] as DataRow;
                ds = gweb.Nota_Credito_Automatica(Convert.ToInt32(txtid.Text.Trim()), idc_articulo);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    if (txtrfc.Text.StartsWith("*"))
                    {
                        //txtprecio.Text = Redondeo_cuatro_decimales(row("precio") * ((Session["Xiva"] / 100) + 1))
                        datos[0] = Redondeo_cuatro_decimales(Convert.ToDouble(row["precio"]) * ((Convert.ToDouble(Session["Xiva"]) / 100) + 1));
                    }
                    else
                    {
                        //txtprecio.Text = Redondeo_cuatro_decimales(row("precio"))
                        datos[0] = Redondeo_cuatro_decimales(Convert.ToDouble(row["precio"]));
                    }
                    //txtprecio.Enabled = False
                    datos[1] = row[8];
                    datos[2] = Convert.ToDouble(row["descuento"]);
                    datos[3] = true;
                    datos[4] = Redondeo_cuatro_decimales(Convert.ToDouble(row["precio"]) - Convert.ToDouble(row["descuento"]));
                }
                else
                {
                    ds = gweb.buscar_precio_producto(idc_articulo, Convert.ToInt32(txtid.Text.Trim()), Convert.ToInt32(Session["idc_sucursal"]));
                    ///*Cambiar 1 por la Var Session[""]*/
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        row = ds.Tables[0].Rows[0];
                        if (txtrfc.Text.StartsWith("*"))
                        {
                            //txtprecio.Text = Redondeo_cuatro_decimales(row("precio") * ((Session["Xiva"] / 100) + 1))
                            //txtprecio.Text = Math.Round(CDec(txtprecio.Text), 4)
                            datos[0] = Redondeo_cuatro_decimales(Convert.ToDouble(row["precio"]) * ((Convert.ToDouble(Session["Xiva"]) / 100) + 1));
                        }
                        else
                        {
                            //txtprecio.Text = Redondeo_cuatro_decimales(row("precio"))
                            datos[0] = Redondeo_cuatro_decimales(Convert.ToDouble(row["precio"]));
                        }

                        datos[3] = false;
                        datos[1] = row[1];
                        datos[2] = 0;
                        datos[4] = Redondeo_cuatro_decimales(Convert.ToDouble(row["precio"]));
                    }
                }
                //Session["rowprincipal"] = rowprincipal
                if (datos.Length > 0)
                {
                    return datos;
                }
                else {
                    return datos;
                }
            }
            catch (Exception ex)
            {
                CargarMsgbox(ex.Message);
                return datos;
            }
        }
        private void CargarMsgbox(string msg)
        {
            Alert.ShowAlertError(msg,this);
        }
    }
}