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
    public partial class cotizacion_clientes2_m : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                txtfecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
                int idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
                int idc_agente = Request.QueryString["idc_agente"] == null ? Convert.ToInt32(funciones.de64aTexto(Request.QueryString["IDA"])) : Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_agente"]));
                txtagente.Text = idc_agente.ToString();
                txtid.Text = idc_cliente.ToString();
                cargar_datos_cliente(idc_cliente);
                cargar_lista_master(idc_cliente);
                master_t(true);
                buscar(false);
                lnkbuscar.Visible = true;
                lnkmaster.Visible = false;
                controles_captura(false);
                Crear_dt();
                cargar_periodo();
            }
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
                    txtcliente.Text = ds.Tables[0].Rows[0]["nombre"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Cargar Datos del Cliente.");
            }
        }
        public void cargar_busqueda_directa(string idc_articulo)
        {
            DataSet ds = new DataSet();
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                ds = com.sp_buscar_articulo_VENTAS_existencias(idc_articulo, "c", Convert.ToInt32(Session["idc_sucursal"]), Convert.ToInt32(Session["sidc_usuario"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbomaster.DataSource = ds.Tables[0];
                    cbomaster.DataTextField = "desart";
                    cbomaster.DataValueField = "idc_articulo";
                    cbomaster.DataBind();
                    txtbuscar.Text = "";
                    lnkseleccionar_Click(null, EventArgs.Empty);
                }
                else
                {
                    txtbuscar.Text = "";
                    CargarMsgBox("No Existen Articulos con esa Descripcion.");
                    cbomaster.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Cargar Productos \\n \\u000b \\n " + ex.Message);
            }
        }
        public void controles_captura(bool estado)
        {
            if (estado == false)
            {
                txtcantidad.Attributes["style"] = "display:none;";
                txtprecio.Attributes["style"] = "display:none;";
               // btnagregar.Attributes["style"] = "display:none;";
                lblcantidad.Attributes["style"] = "display:none;";
                lblprecio.Attributes["style"] = "display:none;";
                txtprecio_minimo.Attributes["style"] = "display:none;";
                txtprecio_lista.Attributes["style"] = "display:none;";
                lblpreciolista.Attributes["style"] = "display:none;";
                lblpreciominimo.Attributes["style"] = "display:none;";
                lblporcentajelista.Attributes["style"] = "display:none;";
                lblporcentajeminimo.Attributes["style"] = "display:none;";
                lblporcentajeprecio.Attributes["style"] = "display:none;";
                txtporc_lista.Attributes["style"] = "display:none;";
                txtporc_minimo.Attributes["style"] = "display:none;";
                txtporc_precio.Attributes["style"] = "display:none;";
                rdnegociado.Attributes["style"] = "display:none;";
                rdrequerido.Attributes["style"] = "display:none;";
                lblfecha_comp.Attributes["style"] = "display:none;";
                lblperiodo.Attributes["style"] = "display:none;";
                cboperiodo.Attributes["style"] = "display:none;";
                txtfecha.Attributes["style"] = "display:none;";
                rdnegociado.Checked = true;
                rdrequerido.Checked = false;
            }
            else
            {
                txtcantidad.Attributes["style"] = "display:;";
                txtprecio.Attributes["style"] = "display:inline;";
               // btnagregar.Attributes["style"] = "display:;";
                lblprecio.Attributes["style"] = "display:;";
                lblcantidad.Attributes["style"] = "display:;";
                txtprecio_minimo.Attributes["style"] = "display:inline;";
                txtprecio_lista.Attributes["style"] = "display:inline;";
                lblpreciolista.Attributes["style"] = "display:;";
                lblpreciominimo.Attributes["style"] = "display:;";
                lblporcentajelista.Attributes["style"] = "display:;";
                lblporcentajeminimo.Attributes["style"] = "display:;";
                lblporcentajeprecio.Attributes["style"] = "display:;";
                txtporc_lista.Attributes["style"] = "display:inline;";
                txtporc_minimo.Attributes["style"] = "display:inline;";
                txtporc_precio.Attributes["style"] = "display:inline;";
                rdnegociado.Attributes["style"] = "display:;";
                rdrequerido.Attributes["style"] = "display:;";
                lblfecha_comp.Attributes["style"] = "display:;";
                lblperiodo.Attributes["style"] = "display:;";
                cboperiodo.Attributes["style"] = "display:;";
                txtfecha.Attributes["style"] = "display:;";

            }

            negociado.Visible = estado;
            requerido.Visible = estado;
            lnkultimo.Visible = estado;
            lbldesart.Visible = estado;
            btnagregar.Visible = estado;
        }

        public void Crear_dt()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("desart");
                dt.Columns.Add("cantidad");
                dt.Columns.Add("idc_articulo");
                dt.Columns.Add("precio");
                dt.Columns.Add("PrecioCompetencia");
                dt.Columns.Add("Vol");
                dt.Columns.Add("Competencia");
                dt.Columns.Add("ObsCompetencia");
                dt.Columns.Add("ultm_precio");
                dt.Columns.Add("fecha_ult_precio");
                dt.Columns.Add("porc");
                dt.Columns.Add("costo");
                dt.Columns.Add("ven");
                dt.Columns.Add("precio_lista");
                dt.Columns.Add("ult_precio_nc");
                dt.Columns.Add("requerido");
                dt.Columns.Add("fecha_compromiso");
                dt.Columns.Add("periodo");
                dt.Columns.Add("base64");
                dt.Columns.Add("tipoprecio");
                dt.Columns.Add("preciominimo");
                Session["dt_cotizacion"] = dt;
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error: \\n \\u000b \\n");
            }
        }
        
        private void cargar_lista_master(int idc_cliente)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                entidad.Pidc_cliente = idc_cliente;
                DataSet ds = com.sp_articulos_master_cliente(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbomaster.DataSource = ds.Tables[0];
                    cbomaster.DataTextField = "desart";
                    cbomaster.DataValueField = "idc_articulo";
                    cbomaster.DataBind();
                }
                else
                {
                    CargarMsgBox("No Existen Articulos.");
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Cargar Lista Master. \\n \\u000b \\n Error: \\n" + ex.Message);
            }
        }

        public void cargar_periodo()
        {
            for (int i = 1; i <= 100; i++)
            {
                cboperiodo.Items.Insert(i - 1, new ListItem(i.ToString(),i.ToString()));
            }
            cboperiodo.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        private void master_t(bool estado)
        {
            if (estado == true)
            {
                lbltipo.Text = "Master";
                lnkmaster.Visible = false;
            }
            else
            {
                lbltipo.Text = "Buscar";
                lnkmaster.Visible = true;
            }
        }

        private void buscar(bool estado)
        {
            if (estado == true)
            {
                txtbuscar.Visible = estado;
                lnkbuscar.Visible = false;
            }
            else
            {
                txtbuscar.Visible = estado;
                lnkbuscar.Visible = true;
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
        protected void lnkseleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbomaster.Items.Count <= 0)
                {
                    return;
                }
                if (Convert.ToInt32(cbomaster.SelectedValue) > 0)
                {
                    DataTable dt = new DataTable();
                    dt = Session["dt_cotizacion"] as DataTable;
                    DataView vw = dt.DefaultView;
                    vw.RowFilter = "idc_articulo = " + cbomaster.SelectedValue + "";
                    if (vw.ToTable().Rows.Count > 0)
                    {
                        alert("El Articulo " + cbomaster.SelectedItem.ToString() + " ya fue agregado.");
                        limpiar_controles_cap();
                    }
                    else {
                        AgentesENT entidad = new AgentesENT();
                        AgentesCOM com = new AgentesCOM();
                        controles_captura(true);
                        txtcantidad.Text = 1.ToString();
                        DataSet ds = new DataSet();
                        ds = com.sp_precio_cliente_cedis_rangos(Convert.ToInt32(cbomaster.SelectedValue), Convert.ToInt32(txtid.Text.Trim()), Convert.ToInt32(Session["idc_sucursal"]), 1, false);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lbldesart.Text = cbomaster.SelectedItem.ToString();
                            txtprecio.Text = ds.Tables[0].Rows[0]["precio"].ToString();
                            txtprecio_ch.Text = ds.Tables[0].Rows[0]["precio"].ToString();
                            txtcosto.Text = ds.Tables[0].Rows[0]["costo"].ToString();
                            txtprecio_lista.Text = ds.Tables[0].Rows[0]["precio_lista"].ToString();
                            txtprecio_minimo.Text = ds.Tables[0].Rows[0]["precio_minimo"].ToString();
                            txtult_pre.Text = ds.Tables[0].Rows[0]["precio"].ToString();
                            if (Convert.ToDecimal(txtprecio.Text) < Convert.ToDecimal(txtprecio_minimo.Text))
                            {
                                txtprecio_minimo.Text = txtprecio.Text;
                            }
                            aportacion_lista();
                            aportacion_minimo();
                            aportacion_precio();
                        }
                        else
                        {
                            txtprecio.Text = "";
                            txtprecio_lista.Text = "";
                        }
                        txtprecio.Focus();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Cargar Lista Master. \\n \\u000b \\n Error: \\n" + ex.Message);
            }
        }
        public void aportacion_lista()
        {
            decimal precio = Convert.ToDecimal(txtprecio_lista.Text);
            decimal porc = default(decimal);
            decimal margen = default(decimal);
            decimal costo = default(decimal);
            decimal margencompartido = default(decimal);
            decimal vventaart = default(decimal);
            costo = Convert.ToDecimal(txtcosto.Text);
            vventaart = Convert.ToDecimal(txtcantidad.Text) * precio;

            margen = (1 - (costo / precio)) * 100;

            margen = (margen < 4 ? margen : (margen < 6 ? 4 : (margen < 8 ? 6 : (margen < 10 ? 8 : (margen < 12 ? 10 : margen)))));
            //'vmargenlis = IIf(vmargenlis < 4, vmargenlis, IIf(vmargenlis < 6, 4, IIf(vmargenlis < 8, 6, IIf(vmargenlis < 10, 8, IIf(vmargenlis < 12, 10, vmargenlis)))))

            margen = Convert.ToDecimal(margen);
            porc = (((Convert.ToDecimal(2.5) / 22) / 100) * margen) * 100;
            margencompartido = margen * Convert.ToDecimal(0.1136);
            if (margen >= 12)
            {
                porc = Convert.ToDecimal(margencompartido * 1);
            }
            else if (margen >= 10)
            {
                porc = Convert.ToDecimal(margencompartido * Convert.ToDecimal(0.75));
            }
            else if (margen >= 8)
            {
                porc = Convert.ToDecimal(margencompartido * Convert.ToDecimal(0.5));
            }
            else if (margen >= 6)
            {
                porc = Convert.ToDecimal(margencompartido * Convert.ToDecimal(0.25));
            }
            else if (margen < 6)
            {
                porc = Convert.ToDecimal(0);
            }
            txtporc_lista.Text = porc.ToString("#.####");
        }

        public void aportacion_minimo()
        {
            decimal precio = Convert.ToDecimal(txtprecio_lista.Text);
            decimal porc = default(decimal);
            decimal margen = default(decimal);
            decimal costo = default(decimal);
            decimal margencompartido = default(decimal);
            decimal vventaart = default(decimal);
            costo = Convert.ToDecimal(txtcosto.Text);
            vventaart = Convert.ToDecimal(txtcantidad.Text) * precio;

            margen = (1 - (costo / precio)) * 100;

            margen = (margen < 4 ? margen : (margen < 6 ? 4 : (margen < 8 ? 6 : (margen < 10 ? 8 : (margen < 12 ? 10 : margen)))));
            //'vmargenlis = IIf(vmargenlis < 4, vmargenlis, IIf(vmargenlis < 6, 4, IIf(vmargenlis < 8, 6, IIf(vmargenlis < 10, 8, IIf(vmargenlis < 12, 10, vmargenlis)))))

            margen = Convert.ToDecimal(margen);
            porc = (((Convert.ToDecimal(2.5) / 22) / 100) * margen) * 100;
            margencompartido = margen * Convert.ToDecimal(0.1136);
            if (margen >= 12)
            {
                porc = Convert.ToDecimal(margencompartido * 1);
            }
            else if (margen >= 10)
            {
                porc = Convert.ToDecimal(margencompartido * Convert.ToDecimal(0.75));
            }
            else if (margen >= 8)
            {
                porc = Convert.ToDecimal(margencompartido * Convert.ToDecimal(0.5));
            }
            else if (margen >= 6)
            {
                porc = Convert.ToDecimal(margencompartido * Convert.ToDecimal(0.25));
            }
            else if (margen < 6)
            {
                porc = Convert.ToDecimal(0);
            }
            txtporc_minimo.Text = porc.ToString("#.####");
        }

        public void aportacion_precio()
        {
            decimal precio = Convert.ToDecimal(txtprecio_lista.Text);
            decimal porc = default(decimal);
            decimal margen = default(decimal);
            decimal costo = default(decimal);
            decimal margencompartido = default(decimal);
            decimal vventaart = default(decimal);
            costo = Convert.ToDecimal(txtcosto.Text);
            vventaart = Convert.ToDecimal(txtcantidad.Text) * precio;

            margen = (1 - (costo / precio)) * 100;

            margen = (margen < 4 ? margen : (margen < 6 ? 4 : (margen < 8 ? 6 : (margen < 10 ? 8 : (margen < 12 ? 10 : margen)))));
            //'vmargenlis = IIf(vmargenlis < 4, vmargenlis, IIf(vmargenlis < 6, 4, IIf(vmargenlis < 8, 6, IIf(vmargenlis < 10, 8, IIf(vmargenlis < 12, 10, vmargenlis)))))

            margen = Convert.ToDecimal(margen);
            porc = (((Convert.ToDecimal(2.5) / 22) / 100) * margen) * 100;
            margencompartido = margen * Convert.ToDecimal(0.1136);
            if (margen >= 12)
            {
                porc = Convert.ToDecimal(margencompartido * 1);
            }
            else if (margen >= 10)
            {
                porc = Convert.ToDecimal(margencompartido * Convert.ToDecimal(0.75));
            }
            else if (margen >= 8)
            {
                porc = Convert.ToDecimal(margencompartido * Convert.ToDecimal(0.5));
            }
            else if (margen >= 6)
            {
                porc = Convert.ToDecimal(margencompartido * Convert.ToDecimal(0.25));
            }
            else if (margen < 6)
            {
                porc = Convert.ToDecimal(0);
            }
            txtporc_precio.Text = porc.ToString("#.####");
        }
        protected void lnkbuscar_Click(object sender, EventArgs e)
        {
            //controles_captura(false);
            master_t(false);
            buscar(true);
            limpiar_controles_cap();
            controles_captura(false);
            txtbuscar.Focus();
        }

        protected void lnkmaster_Click(object sender, EventArgs e)
        {
            master_t(true);
            buscar(false);
            limpiar_controles_cap();
            controles_captura(false);
            cargar_lista_master(Convert.ToInt32(txtid.Text.Trim()));
        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            try
            {
                bool error = false;
                decimal pmin = txtprecio_minimo.Text == "" ? 0 : Convert.ToDecimal(txtprecio_minimo.Text);
                decimal prec = txtprecio.Text == "" ? 0 : Convert.ToDecimal(txtprecio.Text);
                bool requeridov = requerido.Attributes["class"] == "btn btn-info" ? true : false;
                DataTable dt = new DataTable();
                dt = Session["dt_cotizacion"] as DataTable;
                DataView vw = dt.DefaultView;
                vw.RowFilter = "idc_articulo = "+cbomaster.SelectedValue +"";
                if (vw.ToTable().Rows.Count > 0)
                {
                    alert("El Articulo "+cbomaster.SelectedItem.ToString()+" ya existe.");
                    error = true;

                }
                if (txtcantidad.Text == "" || Convert.ToDecimal(txtcantidad.Text) <= 0)
                {
                    alert("Ingrese Cantidad Mayor a 0.");
                    error = true;
                }
                else if (txtprecio.Text == "" || Convert.ToDecimal(txtprecio.Text) <= 0)
                {
                    alert("Ingrese Precio Mayor a 0.");
                    error = true;
                }
                else if (txtmovio.Text == "1")
                {
                    if (requeridov == false)
                    {
                        if (pmin > prec)
                        {
                            alert("El Precio Debe ser Mayor o Igual al Precio Minimo.");
                            error = true;
                        }
                    }
                    else
                    {
                        if (pmin < prec)
                        {
                            alert("El Precio Debera Ser Menor al Precio Minimo.");
                            error = true;
                        }
                    }
                }
                else if (txtfecha.Text == "")
                {
                    alert("Ingrese Fecha de Compromiso.");
                    error = true;
                }
                else if (Convert.ToDateTime(txtfecha.Text) < DateTime.Today)
                {
                    alert("Fecha de Compromiso Debe Ser Mayor o Igual al Dia de Hoy.");
                    error = true;
                }
                if (requeridov == true)
                {
                    if (pmin < prec)
                    {
                        alert("El Precio Requerido Debera Ser Menor al Precio Minimo.");
                        error = true;
                    }

                    if (txtprecio_comp.Text == "") {
                        alert("Ingrese Precio de Competencia");
                        error = true;
                    }
                    if (txtvolumen.Text == "") {
                        alert("Ingrese Volumén de Compra.");
                        error = true;
                    }
                    if (txtnombre_comp.Text == "") {
                        alert("Ingrese Nombre de la Competencia.");
                        error = true;
                    }

                }


                if (error == false)
                {
                    AgregarArticulo();
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Cargar Lista Master. \\n \\u000b \\n Error: \\n" + ex.Message);
            }
        }

        public void actualizar_precios()
        {
            DataTable dt = new DataTable();
            dt = Session["dt_cotizacion"] as DataTable;
            TextBox txtcantidad_grid = new TextBox();
            TextBox txtprecio_grid = new TextBox();
            decimal porc = 0;
            decimal margen = 0;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int x = 0; x <= gridcot.Items.Count - 1; x++)
                {
                    txtcantidad_grid = gridcot.Items[i].FindControl("txtcantidad") as TextBox;
                    txtprecio_grid = gridcot.Items[i].FindControl("txtprecio_grid") as TextBox;
                    if ((txtcantidad_grid != null))
                    {
                        if (dt.Rows[i]["idc_articulo"].ToString() == gridcot.Items[x].Cells[0].Text)
                        {
                            dt.Rows[i]["cantidad"] = (txtcantidad_grid.Text=="" ? 0 : Convert.ToDecimal(txtcantidad_grid.Text));
                            dt.Rows[i]["precio"] = (string.IsNullOrEmpty(txtprecio_grid.Text) ? 0 : Convert.ToDecimal(txtprecio_grid.Text));

                            margen = (1 - (Convert.ToDecimal(dt.Rows[i]["costo"]) / Convert.ToDecimal(dt.Rows[i]["precio"]))) *100;
                            porc = (((Convert.ToDecimal(2.5) / Convert.ToDecimal(22)) / 100) * margen) * 100;



                            if ((margen >= 12))
                            {
                                porc = porc * 1;
                            }
                            else if ((margen >= 10))
                            {
                                porc = porc *Convert.ToDecimal(0.75);
                            }
                            else if ((margen >= 8))
                            {
                                porc = porc * Convert.ToDecimal(0.5);
                            }
                            else if ((margen >= 6))
                            {
                                porc = porc * Convert.ToDecimal(0.25);
                            }
                            else if ((margen < 6))
                            {
                                porc = porc * Convert.ToDecimal(0.1);
                            }

                            porc = Convert.ToDecimal(dt.Rows[i]["precio"]) * Convert.ToDecimal(dt.Rows[i]["cantidad"]) * (porc / 100);
                            dt.Rows[i]["porc"] = porc;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }
            }
            Session["dt_cotizacion"] = dt;
        }

        public bool cambio_ult_precio(int idc, int ced, System.DateTime fecha)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                string query = "select dbo.fn_cambio_costo_arti_cedis_fecha (" + idc + "," + ced + "," + fecha + ") as cambio";
                DataTable dt = com.sp_cambio_costo_arti_cedis_fecha(idc,ced,fecha).Tables[0]; 
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
                CargarMsgBox("Error al Agregar Articulo a la Lista. \\n \\u000B \\n Error: \\n" + ex.Message);
                return false;
            }
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
                CargarMsgBox("Error al Agregar Articulo a la Lista. \\n \\u000B \\n Error: \\n" + ex.Message);
                return ufecha_precio;
            }
        }

        private void subir_facturas()
        {
            if (fufactura.HasFile)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/tareas/"));//path local
                string path = dirInfo + randomNumber.ToString() + fufactura.FileName;
                bool pape = funciones.UploadFile(fufactura,path, this.Page);
                txtbase64.Value = funciones.deTextoa64(path);
            }
        }
        private void AgregarArticulo()
        {
            try
            {                
                actualizar_precios();
                subir_facturas();
                bool requeridov = requerido.Attributes["class"] == "btn btn-info" ? true : false;
                DataTable dt = new DataTable();
                dt = Session["dt_cotizacion"] as DataTable;
                DataRow row = default(DataRow);
                row = dt.NewRow();
                row["idc_articulo"] = cbomaster.SelectedValue;
                row["desart"] = cbomaster.SelectedItem.ToString();
                row["cantidad"] = txtcantidad.Text;
                row["precio"] = txtprecio.Text;
                row["costo"] = txtcosto.Text.Trim();
                row["ven"] =txtporc_precio.Text.Trim();
                row["precio_lista"] = txtprecio_lista.Text;
                row["base64"] = txtbase64.Value;
                row["requerido"] = (requeridov == true ? 1 : 0);
                row["fecha_compromiso"] = (string.IsNullOrEmpty(txtfecha.Text) ? "" : Convert.ToDateTime(txtfecha.Text).ToString("yyyy-dd-MM"));
                row["periodo"] = (cboperiodo.SelectedIndex > 0 ? cboperiodo.SelectedValue : "");
                row["PrecioCompetencia"] = (string.IsNullOrEmpty(txtprecio_comp.Text) ? 0 : Convert.ToDecimal(txtprecio_comp.Text));
                row["Vol"] = (string.IsNullOrEmpty(txtvolumen.Text) ? 0 : Convert.ToInt32(txtvolumen.Text));
                row["Competencia"] = txtnombre_comp.Text;
                row["ObsCompetencia"] = txtobservaciones_comp.Text;
                //add 12-01-2016 mic
                //requerido o negociado
                if (requeridov== true)
                {
                    row["tipoprecio"] = "R";
                }else { 
                    row["tipoprecio"] = "N";
                }
                row["preciominimo"] = txtprecio_minimo.Text;

                object[] ult_precios = null;
                ult_precios = ultimo_precio_cliente(Convert.ToInt32(row["idc_articulo"]));

                if (ult_precios[0] != null)
                {
                    row["ultm_precio"] = ult_precios[0].ToString().Replace("*", "");
                }

                if (ult_precios[1] != null)
                {
                    row["fecha_ult_precio"] = ult_precios[1];
                }

                if (ult_precios[2] != null)
                {
                    row["ult_precio_nc"] = (string.IsNullOrEmpty(ult_precios[2].ToString()) ? 0 : ult_precios[2]);
                }

                decimal porc = 0;
                decimal margen = 0;
                //margen = (1 - ((row["precio") / row["costo")))) * 100
                margen = Math.Round((1 - (Convert.ToDecimal(row["costo"]) / Convert.ToDecimal(row["precio"]))) * 100, 2);

                porc = (((Convert.ToDecimal(2.5) / Convert.ToDecimal(22)) / 100) * margen) * 100;

                if (margen >= 12)
                {
                    porc = porc * 1;
                }
                else if (margen >= 10)
                {
                    porc = porc *Convert.ToDecimal(0.75);
                }
                else if (margen >= 8)
                {
                    porc = porc * Convert.ToDecimal(0.5);
                }
                else if (margen >= 6)
                {
                    porc = porc * Convert.ToDecimal(0.25);
                }
                else if (margen < 6)
                {
                    porc = porc * 0;
                }
                //porc = Math.Round(porc, 2)
                row["porc"] = porc;


                dt.Rows.Add(row);
                Session["dt_cotizacion"] = dt;
                cargar_grid();
                limpiar_controles_cap();
                controles_captura(false);
                lbldesart.Text = "";
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Agregar Articulo a la Lista. \\n \\u000B \\n Error: \\n" + ex.Message);
            }

          
        }
        public void cargar_grid()
        {
            DataTable dt = Session["dt_cotizacion"] as DataTable;
            DataTable dtcopy = dt.Copy();
            if (dtcopy.Rows.Count > 0)
            {
                gridcot.DataSource = dtcopy;
                gridcot.DataBind();
            }
            else
            {
                gridcot.DataSource = null;
                gridcot.DataBind();
            }
        }
        public void limpiar_controles_cap()
        {
            lbldesart.Text = "";
            txtfecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            cboperiodo.SelectedIndex = 0;
            txtcantidad.Text = "";
            txtprecio.Text = "";
            txtprecio_comp.Text = "";
            txtvolumen.Text = "";
            txtobservaciones_comp.Text = "";
            txtbase64.Value = "";
            txtnombre_comp.Text = "";
            negociado.Attributes["class"] = "btn btn-info";
            requerido.Attributes["class"] = "btn btn-dafault";
        }

        protected void gridcot_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "eliminar")
            {
                actualizar_precios();
                DataTable dt = Session["dt_cotizacion"] as DataTable;
                if (dt.Rows.Count > 0)
                {
                    int idc_articulo = Convert.ToInt32(e.Item.Cells[0].Text.Trim());
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        if (idc_articulo == Convert.ToInt32(dt.Rows[i]["idc_articulo"]))
                        {
                            dt.Rows.RemoveAt(i);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    Session["dt_cotizacion"] = dt;
                    cargar_grid();
                }
                else
                {
                    cargar_grid();
                }
                lbldesart.Text = "";
            }
        }

        protected void gridcot_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem | e.Item.ItemType == ListItemType.Item)
            {
                TextBox txtcantidad_grid = new TextBox();
                txtcantidad_grid = e.Item.FindControl("txtcantidad") as TextBox;
                TextBox txtprecio_grid = new TextBox();
                txtprecio_grid = e.Item.FindControl("txtprecio_grid") as TextBox;
                txtcantidad_grid.Attributes["onkeydown"] = "return soloNumeros(event,'true')";
                txtprecio_grid.Attributes["onkeydown"] = "return soloNumeros(event,'true')";
                txtprecio_grid.Attributes["onblur"] = "return precio_minimo_minigrid(" + e.Item.ItemIndex + ",this)";
                txtcantidad_grid.Attributes["onblur"] = "porcentaje()";
                ImageButton imgultimo_precio = new ImageButton();
                imgultimo_precio = e.Item.FindControl("imgultimo_precio") as ImageButton;

                if ((imgultimo_precio != null))
                {
                    string label7 = null;
                    string label8 = null;
                    string label9 = null;
                    label7 = e.Item.Cells[9].Text.Trim();
                    label8 = e.Item.Cells[10].Text.Trim();
                    label9 = e.Item.Cells[12].Text.Trim();
                    if ((label7 != "&nbsp;" | label8 != "&nbsp;"))
                    {
                        string mensaje = "";
                        mensaje = (!string.IsNullOrEmpty(label7) ? "Ultimo Precio Facturado: \\n" + label7 : "");
                        mensaje = (!string.IsNullOrEmpty(label8) ? mensaje + "\\n \\u000b \\nFecha Ultimo Precio:\\n" + label8 : mensaje);
                        mensaje = (label9 != label7 ? mensaje + "\\n \\u000b \\nPrecio Real:\\n" + label9 : "");
                        imgultimo_precio.Attributes["onclick"] = "alert('" + mensaje + "'); return false;";
                    }
                    else
                    {
                        imgultimo_precio.Attributes["onclick"] = "alert('Sin Historial de Venta.');return false;";
                        imgultimo_precio.ImageUrl = "imagenes/calendar3.gif";
                    }

                }

               
                e.Item.Cells[1].Attributes["onclick"] = "return porcentaje();";

            }
        }

        protected void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            cargar_busqueda_directa(txtbuscar.Text);
        }

        protected void lnkultimo_Click(object sender, EventArgs e)
        {
            object[] ult_precios = null;
            ult_precios = ultimo_precio_cliente(Convert.ToInt32(cbomaster.SelectedValue));

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
                Alert.ShowAlertInfo(mensaje, "Mensaje del Sistema", this);
            }
            else
            {
                Alert.ShowAlertInfo("Sin Historial de Venta.", "Mensaje del Sistema", this);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["dt_cotizacion"] as DataTable;
            if (dt.Rows.Count == 0)
            {
                Alert.ShowAlertError("Debes Incluir al menos un Articulo.", this);
            } else {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar esta Cotización?','modal fade modal-info');", true);

            }
        }
        private string Cadena()
        {
            string cadena = "";
            DataTable dt = Session["dt_cotizacion"] as DataTable;
            DataRow[] rows = null;
            if (dt.Rows.Count > 0)
            {
                //rows = dt.Select("cantidad>0 and incluir<>false")
                rows = dt.Select("cantidad>0");
                if (rows.Length > 0)
                {
                    for (int i = 0; i <= rows.Length - 1; i++)
                    {
                        cadena = cadena + rows[i]["idc_articulo"].ToString().Trim() + ";" + rows[i]["precio"].ToString().Trim() + ";" +
                            rows[i]["cantidad"].ToString().Trim() + ";" + (rows[i]["vol"].ToString().Trim() != "" ? "0" : rows[i]["vol"].ToString().Trim()) + ";" +
                           (rows[i]["preciocompetencia"].ToString().Trim() != "" ? "0" : rows[i]["preciocompetencia"].ToString().Trim()) + ";" +
                           rows[i]["obscompetencia"].ToString().Trim() + ";" + rows[i]["competencia"].ToString().Trim() + ";" +
                           rows[i]["fecha_compromiso"].ToString().Trim() + ";" + rows[i]["periodo"].ToString().Trim() + ";" + rows[i]["requerido"].ToString().Trim() + ";" +
                          funciones.de64aTexto(rows[i]["base64"].ToString())+";";
                    }
                }
            }
            
            return cadena;
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                int idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
                int idc_agente = Request.QueryString["idc_agente"] == null ? Convert.ToInt32(funciones.de64aTexto(Request.QueryString["IDA"])) : Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_agente"]));
                DataTable dt = Session["dt_cotizacion"] as DataTable;
                string caso = (string)Session["Caso_Confirmacion"];
                switch (caso)
                {
                    case "Guardar":
                        AgentesENT entidad = new AgentesENT();
                        AgentesCOM com = new AgentesCOM();
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        entidad.Ptotalcadenaarti = dt.Rows.Count;
                        entidad.Pcadenaarti = Cadena();
                        entidad.Pidc_agente = idc_agente;
                        entidad.Pidc_cliente = idc_cliente;
                        DataSet ds = com.sp_aagentes_act_cotizacion_nueva_web(entidad);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            DataTable tabla_archivos = ds.Tables[1];
                            bool correct = true;
                            foreach (DataRow row_archi in tabla_archivos.Rows)
                            {
                                string ruta_det = row_archi["url_destino"].ToString();
                                string ruta_origen = row_archi["url_origen"].ToString();
                                correct = funciones.CopiarArchivos(ruta_origen, ruta_det, this.Page);
                                if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo " + ruta_origen + "a la ruta. Contactese con el Departamento de Sistemas." + ruta_det, this); }
                            }
                            int total = (((tabla_archivos.Rows.Count) * 1) + 1) * 1000;
                            string t = total.ToString();
                            string url_back = Session["Back_Page"] != null ? (string)Session["Back_Page"] : "ficha_cliente_m.aspx";
                            Alert.ShowGiftMessage("Estamos procesando la Cotización.", "Espere un Momento", 
                                url_back, "imagenes/loading.gif", t, "La Cotización fue Guardada Correctamente", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
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
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ficha_cliente_m.aspx");
        }
    }
}