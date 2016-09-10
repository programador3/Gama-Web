using negocio.Componentes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class Carga_de_Combustibles_m : System.Web.UI.Page
    {
        private DBConnection conexion = new DBConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "visible_ob();", true);
                if (Page.IsPostBack != true)
                {
                    buscar_folio();
                    DateTime dd = DateTime.Now;
                    txtfecha.Text = dd.ToShortDateString();
                    cargar_combo_comb();
                    btnagregarchofer.Visible = false;
                    //txtnombrebuscar.Attributes["onkeydown"] = "return clickb(event ," + btnbuscarempleado.ClientID + ");";
                    //Session["sidc_usuario"] = 15;
                    txtkmactual.Attributes["onblur"] = "return calculo();";
                    txtcombustibleutilizado.Attributes["onblur"] = "validaFloat(this);return calculoPc();";
                    txtdistanciarecorrida.Attributes["onblur"] = "return calculoPc();";
                    txtutilizadorelenti.Attributes["onblur"] = "validaFloat(this);calculoPc();";
                    //txtcombustibleutilizado.Attributes["onfocus"] = "return seleccionar(" + txtcombustibleutilizado.ClientID + ");";
                    txtcombustibleutilizado.Attributes["onclick"] = "seleccionar(this);";
                    txtkmactual.Attributes["onclick"] = "return seleccionar(" + txtkmactual.ClientID + ");";
                    txtcantidadlitros.Attributes["onchange"] = "calculo();calculoPc();";
                    //calculo();
                    //txtidc_vehiculo.Attributes["onblur"] = "return validaFloat(this);";
                    txttiemporelenti.Attributes["onblur"] = "return ValidarTiempo(this);";
                    txttiemporelenti.Attributes["onkeydown"] = "return validarmaxlength(this,5);";
                    btnguardar.Attributes["onClick"] = "return confirmar(1);";
                    txtcantidadlitros.Attributes["onkeydown"] = "return x(event);";
                    txttiemporelenti.Attributes["onkeydown"] = "return x(event);";
                    txtkmactual.Attributes["onkeydown"] = "return x(event);";
                    txtcombustibleutilizado.Attributes["onkeydown"] = "return x(event);";
                    txtdistanciarecorrida.Attributes["onkeydown"] = "return x(event);";
                    txttiemporelenti.Attributes["onkeydown"] = "return x(event)";
                    txtutilizadorelenti.Attributes["onkeydown"] = "return x(event);";
                    txtcantidadlitros.Attributes["onblur"] = "return validarcantidad(this);";
                    txtbchofer.Attributes["onclick"] = "return seleccionar(this);";
                    txtbveh.Attributes["onclick"] = "return seleccionar(this);";
                    chkreportebc.Attributes["onclick"] = "return visible_ob();";
                    txtbveh.Attributes["onkeydown"] = "return buscar_veh(event);";
                    txtbchofer.Attributes["onkeydown"] = "return buscar_emp(event);";
                    cargar_todos_veh("", false);

                    ImageButton imgregresar = new ImageButton();
                    imgregresar = (ImageButton)Master.FindControl("imgregresar");
                    if (imgregresar != null)
                    {
                        imgregresar.PostBackUrl = "login.aspx";
                    }
                }
            }
        }

        public void buscar_folio()
        {
            DataSet ds = new DataSet();
            ds = conexion.Cargar_Combustible_Folio(416);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtnocarga.Text = Convert.ToString(Convert.ToInt64(ds.Tables[0].Rows[0].ItemArray[0]) + 1);
                }
            }
        }

        public void cargar_todos_veh(string valor, Boolean busqueda)
        {
            try
            {
                int idc_sucursal = Convert.ToInt32(Session["idc_sucursal"]);
                bool todos = true;
                if (busqueda == true)
                {
                    idc_sucursal = 0;
                    todos = false;
                }
                string x;
                DataSet ds = new DataSet();
                string[] parametros = { "@pvalor", "@ptodos", "@pidc_sucursal" };
                object[] valores = { valor, todos, idc_sucursal };

                ds = conexion.Ejecuta_SP("selecciona_vehiculo", parametros, valores);
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    x = Convert.ToString(ds.Tables[0].Rows[i].ItemArray[0]);
                    ds.Tables[0].Rows[i]["descripcion"] = ds.Tables[0].Rows[i]["num_economico"] + ".-" + ds.Tables[0].Rows[i]["descripcion"];
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbovehiculos.DataSource = ds.Tables[0];
                    cbovehiculos.DataTextField = "descripcion";
                    cbovehiculos.DataValueField = "idc_vehiculo";
                    cbovehiculos.DataBind();
                    if (busqueda == true)
                    {
                        cbovehiculos.Items.Insert(0, "Ver Todos");
                        cbovehiculos.SelectedIndex = 1;
                        txtbveh.Text = "";
                    }
                    int idc_vehiculo = Convert.ToInt32(cbovehiculos.SelectedValue);
                    buscar_chofer(idc_vehiculo);
                    seleccionar_tipo_comb(idc_vehiculo);
                    ver_kilometraje_anterior(idc_vehiculo);
                    etiquetas_rendimiento();
                    ver_datos_tanque(idc_vehiculo);
                    combo_tanques(Convert.ToInt32(Session["sidc_usuario"]));
                    tipo_camion();
                    txtidc_vehiculo.Text = Convert.ToString(idc_vehiculo);
                }
                else
                {
                    //cbovehiculos.Items.Clear();
                    //cbochoferes.Items.Clear();
                    CargarMsgBox("No Se Encontraron Vehiculos.");
                    txtbchofer.Text = "";
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Realizar Busqueda.\\n \\u000b \\nError:\\n" + ex.Message);
            }
        }

        public void tipo_camion()
        {
            DataSet ds = new DataSet();
            ds = conexion.Buscar_Vehiculos(Convert.ToInt32(cbovehiculos.SelectedValue), true);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["idc_tipocam"] != System.DBNull.Value)
                {
                    int idc_camion = Convert.ToInt32(ds.Tables[0].Rows[0]["idc_tipocam"]);
                    if (idc_camion == 7 || idc_camion == 8)
                    {
                        tipoCamion(true);
                    }
                    else
                    {
                        tipoCamion(false);
                        txttiemporelenti.Text = "00:00";
                    }
                }
                else
                {
                    tipoCamion(false);
                    txttiemporelenti.Text = "00:00";
                    txt_tipo_camion.Text = "";
                }
            }
        }

        protected void imgbuscarcamion_Click(object sender, ImageClickEventArgs e)
        {
            //DataSet ds = new DataSet();
            //ds = conexion.Buscar_Vehiculos();
            //if (ds.Tables.Count > 0)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        this.gridresultadosbusqueda.DataSource = ds;
            //        gridresultadosbusqueda.DataBind();
            //        ModalPopupExtender2.Hide();
            //        mpeSeleccion.Show();

            //    }

            //}
        }

        //**Grid de Vehiculos**
        //protected void gridresultadosbusqueda_ItemCommand(object source, DataGridCommandEventArgs e)
        //{
        //    if (e.CommandName == "Seleccionar")
        //    {
        //        txtcamion.Text =gridresultadosbusqueda.Items[e.Item.ItemIndex].Cells[2].Text  + ".-" + gridresultadosbusqueda.Items[e.Item.ItemIndex].Cells[3].Text;
        //        int idc_vehiculo = Convert.ToInt32(gridresultadosbusqueda.Items[e.Item.ItemIndex].Cells[1].Text);
        //        txtidc_vehiculo.Text = Convert.ToString( idc_vehiculo);
        //        buscar_chofer(idc_vehiculo);
        //        seleccionar_tipo_comb(idc_vehiculo);
        //        ver_kilometraje_anterior(idc_vehiculo);
        //        etiquetas_rendimiento();
        //        ver_datos_tanque(idc_vehiculo);
        //        combo_tanques(Convert.ToInt32(Session["sidc_usuario"]));
        //        if (gridresultadosbusqueda.Items[e.Item.ItemIndex].Cells[5].Text != "&nbsp;")
        //        {
        //            int idc_tipocam = Convert.ToInt32(gridresultadosbusqueda.Items[e.Item.ItemIndex].Cells[5].Text);
        //            txt_tipo_camion.Text = Convert.ToString(idc_tipocam);
        //            if (idc_tipocam == 8 || idc_tipocam == 7)
        //            {
        //                tipoCamion(true);
        //            }
        //            else
        //            {
        //                tipoCamion(false);
        //                txttiemporelenti.Text = "00:00";
        //            }
        //        }
        //        else
        //        {
        //            tipoCamion(false);
        //            txttiemporelenti.Text = "00:00";
        //            txt_tipo_camion.Text = "";
        //        }

        //    }
        //}
        protected void tipoCamion(Boolean estado)
        {
            txtdistanciarecorrida.Enabled = estado;
            txtcombustibleutilizado.Enabled = estado;
            txttiemporelenti.Enabled = estado;
            txtutilizadorelenti.Enabled = estado;
            if (estado == true)
            {
                Panel1.Attributes["style"] = "display:";
            }
            else
            {
                Panel1.Attributes["style"] = "display:none";
            }
        }

        public void seleccionar_tipo_comb(int idc_vehiculo)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = conexion.Vehiculo_tipo_combustible(idc_vehiculo);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row;
                        row = ds.Tables[0].Rows[0];
                        string valor = Convert.ToString(row.ItemArray[0]);
                        cbotipocombustible.SelectedValue = valor;
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
            finally
            {
            }
        }

        public void cargar_combo_comb()
        {
            DataSet ds = new DataSet();
            ds = conexion.Cargar_Combo_combustible();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbotipocombustible.DataSource = ds;
                    cbotipocombustible.DataTextField = "descripcion";
                    cbotipocombustible.DataValueField = "idc_tcombustible";
                    cbotipocombustible.DataBind();
                    cbotipocombustible.Items.Insert(0, "--Seleccionar--");
                }
            }
        }

        public void buscar_chofer(int idc_vehiculo)
        {
            DataSet ds = new DataSet();
            ds = conexion.Buscar_Chofer(idc_vehiculo);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int idc_chofer = Convert.ToInt32(ds.Tables[0].Rows[0]["idc_empleado"]);
                    txtidc_empleado.Text = Convert.ToString(idc_chofer);
                    if (idc_chofer > 0)
                    {
                        //txtchofer.Text = Convert.ToString(ds.Tables[0].Rows[0]["num_nomina"]);
                        //btnagregarchofer.Visible = false;
                        cbochoferes.DataSource = ds;
                        cbochoferes.DataValueField = "idc_empleado";
                        cbochoferes.DataTextField = "chofer";
                        cbochoferes.DataBind();
                        txtbchofer.Text = "";
                        txtbchofer.Enabled = false;
                    }
                    else
                    {
                        //btnagregarchofer.Visible = true;
                        txtbchofer.Enabled = true;
                        cbochoferes.Items.Clear();
                        txtbchofer.Text = "";
                        // btnagregarchofer.Attributes["OnClick"] = "return pantalla_busc_chofer();";
                    }
                }
                else
                {
                }
            }
            else
            {
                btnagregarchofer.Visible = true;
                txtchofer.Text = "";
            }
        }

        protected void btnbuscarempleado_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtnombrebuscar.Text != "")
            //    {
            //        DataSet ds = new DataSet();
            //        ds = conexion.Buscar_Chofer_Vehiculo(Convert.ToString(txtnombrebuscar.Text));
            //        if (ds.Tables.Count > 0)
            //        {
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                gridempleados.DataSource = ds;
            //                gridempleados.DataBind();
            //                txtnombrebuscar.Text = "";

            //            }
            //            else
            //            {
            //                // Mostrar Label con el error
            //            }

            //        }
            //        ModalPopupExtender2.Show();

            //    }
            //    else
            //        ModalPopupExtender2.Show();

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected void btnagregarchofer_Click(object sender, ImageClickEventArgs e)
        {
            //ModalPopupExtender2.Show();
        }

        //protected void txtnombrebuscar_TextChanged(object sender, EventArgs e)
        //{
        //    btnbuscarempleado_Click(null, EventArgs.Empty);
        //}
        //protected void gridempleados_ItemCommand(object source, DataGridCommandEventArgs e)
        //{
        //    if (e.CommandName == "Seleccionar")
        //    {
        //        txtchofer.Text = gridempleados.Items[e.Item.ItemIndex].Cells[3].Text + ".-" + gridempleados.Items[e.Item.ItemIndex].Cells[2].Text;
        //        txtidc_empleado.Text = Convert.ToString(gridempleados.Items[e.Item.ItemIndex].Cells[1].Text);
        //        gridempleados.DataSource = null;
        //        gridempleados.DataBind();
        //        txtnombrebuscar.Text = null;
        //    }
        //}

        protected void ver_kilometraje_anterior(int idc_vehiculo)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = conexion.Kilometraje_anterior(idc_vehiculo);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = ds.Tables[0].Rows[0];
                        txtkmanterior.Text = Convert.ToString(row.ItemArray[2]);
                        txtlitrosanterior.Text = Convert.ToString(row.ItemArray[3]);
                        if (ds.Tables[0].Rows.Count > 1)
                        {
                            DataRow row2 = ds.Tables[0].Rows[1];
                            txtrendimientoanterior.Text = redondeo_dos_decimales((Convert.ToDouble(row2.ItemArray[2]) - Convert.ToDouble(row.ItemArray[2])) / Convert.ToDouble(row.ItemArray[3])); //Convert.ToString((Convert.ToDouble(row.ItemArray[2]) - Convert.ToDouble(row2.ItemArray[2])) / Convert.ToDouble(row.ItemArray[3]));
                        }
                        else
                        {
                            txtrendimientoanterior.Text = "0.00";
                        }
                    }
                    else
                    {
                        txtkmanterior.Text = "0";
                        txtlitrosanterior.Text = "0";
                        txtrendimientoanterior.Text = "0.00";
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected string redondeo_dos_decimales(double cadena)
        {
            try
            {
                cadena = Math.Round(cadena, 2);
                return Convert.ToString(cadena);
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                return "";
            }
        }

        protected void etiquetas_rendimiento()
        {
            try
            {
                double kmactual;
                if (txtkmactual.Text == "")
                {
                    kmactual = 0;
                }
                else
                {
                    kmactual = Convert.ToDouble(txtkmactual.Text);
                }

                double kmanterior = Convert.ToDouble(txtkmanterior.Text);
                double litros = Convert.ToDouble(txtcantidadlitros.Text);

                double rendimiento = (kmactual - kmanterior) / litros;
                txtrn.Text = redondeo_dos_decimales(rendimiento);
                txtdistancia.Text = Convert.ToString(Convert.ToDouble(kmactual) - Convert.ToDouble(txtkmanterior.Text));
                if (Convert.ToDouble(txtrn.Text) < Convert.ToDouble(txtrendimientoanterior.Text))
                {
                    lblbajor.Attributes["display"] = "";
                    lblaltor.Attributes["display"] = "none";
                }
                else
                {
                    lblbajor.Attributes["display"] = "none";
                    lblaltor.Attributes["display"] = "";
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Cargar Informacion del Vehiculo." + ex.Message);
            }
        }

        protected void ver_datos_tanque(int idc_vehiculo)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = conexion.Datos_Tanque_Vehiculo(idc_vehiculo);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = ds.Tables[0].Rows[0];
                        if (Convert.ToBoolean(row.ItemArray[1]) == true)
                        {
                            chkfrenomotor.Visible = true;
                        }
                        else
                            chkfrenomotor.Visible = false;
                        chkespuma.Text = "Espuma de Seguridad " + (row.ItemArray[2] != "" ? "( " + row.ItemArray[2] + " )" : "");
                        chkcalibracion.Text = "Calibración de Llantas Adecuada " + (Convert.ToInt32(row.ItemArray[3]) > 0 ? "( " + row.ItemArray[3] + " )" : "");
                        chkcalibracion.Checked = false;
                        chkcandado.Checked = false;
                        chkcincho.Checked = false;
                        chkespuma.Checked = false;
                        chkfrenomotor.Checked = false;
                        chkespuma.ToolTip = this.chkespuma.Text;
                        chkcalibracion.ToolTip = this.chkcalibracion.Text;
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        //protected void chkreportebc_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkreportebc.Checked == true)
        //    {
        //        lblobservaciones.Visible = true;
        //        txtobservaciones.Visible = true;
        //    }
        //    else
        //    {
        //        lblobservaciones.Visible = false;
        //        txtobservaciones.Visible = false;
        //    }
        //}

        protected void combo_tanques(int idc_usuario)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = conexion.Tanque_Sucursal(idc_usuario);
                
                cbotanque.DataTextField = "descripcion";
                cbotanque.DataValueField = "idc_tanque";
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        cbotanque.DataSource = ds;
                        cbotanque.DataBind();
                        ViewState["ds"] = ds.Tables[0];
                    }
                }
                cbotanque.Items.Insert(0, "---Seleccionar---");
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void cbotanque_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbotanque.SelectedIndex == 0)
                {
                    return;
                }

                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["ds"];
                DataRow[] idc_tanque = dt.Select("idc_tanque=" + cbotanque.SelectedValue);
                Boolean fisico = Convert.ToBoolean(idc_tanque[0].ItemArray[4]);
                if (fisico == false)
                {
                    chkvirtual.Checked = true;
                }
                else
                    chkvirtual.Checked = false;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void txtlitrosanterior_TextChanged(object sender, EventArgs e)
        {
        }

        //protected void btnguardar_Click(object sender, ImageClickEventArgs e)
        //{
        //    ////ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert ('Probando Script');", true);

        //    //if (validar_campos() == true)
        //    //{
        //    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert ('Funciona');", true);

        //    //    if (txt_tipo_camion.Text == "8" || txt_tipo_camion.Text == "7")
        //    //    {
        //    //        if (txtcombustibleutilizado.Text == "0")
        //    //        {
        //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert ('Es Requerido Ingresar el Combustible Utilizado.'); ", true);
        //    //            return;
        //    //        }
        //    //        else if (txtdistanciarecorrida.Text == "0" || txtdistanciarecorrida.Text == "")
        //    //        {
        //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert ('Es Requerido Ingresar la Distancia Recorrida.'); ", true);
        //    //            return;
        //    //        }
        //    //        else if (txttiemporelenti.Text == "00:00" || txttiemporelenti.Text == "")
        //    //        {
        //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert ('Es Requerido Ingresar el Tiempo RELENTI.'); ", true);
        //    //            return;
        //    //        }
        //    //        else if (txtutilizadorelenti.Text == "0.00" || txtutilizadorelenti.Text == "")
        //    //        {
        //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert ('Es Requerido Ingresar el Combustible Utilizado en RELENTI.'); ", true);
        //    //            return;
        //    //        }
        //    //    }

        //    //    if (chkreportebc.Checked == true)
        //    //    {
        //    //        if (txtobservaciones.Text.Length < 10)
        //    //        {
        //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert ('Las Observaciones del Reporte de Rendimiento debe tener Minimo 10 Caracteres.'); ", true);
        //    //            return;
        //    //        }
        //    //    }
        //    //    int vidc_tanque = Convert.ToInt32(cbotanque.SelectedValue);
        //    //    string vobs = txtobservaciones.Text;

        //    //    if (Convert.ToBoolean(Session["primera_guardar"]) == true)
        //    //    {
        //    //        CargarMsgBox("Cargar", "¿Deseas Guardar el Registro?", true, 1);
        //    //        Session["cargar"] = true;
        //    //        Session["guardar"] = false;
        //    //    }
        //    //    if (Convert.ToBoolean(Session["guardar"]) == true)
        //    //    {
        //    //        if ((Convert.ToDouble(txtkmactual.Text) < Convert.ToDouble(txtkmanterior.Text)) && txtidc_Folio.Text == "" && Convert.ToBoolean(Session["Folio"]) ==false)
        //    //        {
        //    //            CargarMsgBox2("Verificar el Kilometraje", "El Nuevo Kilometraje no Puede ser Menor al Kilometraje Anterior... <BR/> ¿Deseas Capturar el Folio de Autorizacion?", true, 1);
        //    //            Session["Folio"] = true;

        //    //        }

        //    //    }

        //    //    string ip= Request.ServerVariables["REMOTE_ADDR"];
        //    //    string pc = System.Net.Dns.GetHostEntry(ip).HostName;
        //    //    string usuariopc = Request.ServerVariables["LOGON_USER"];
        //    //    int idc_camion = Convert.ToInt32(txtidc_vehiculo.Text);
        //    //    int vchofer = Convert.ToInt32(txtidc_empleado.Text);
        //    //    DateTime vfecha = Convert.ToDateTime(txtfecha.Text);
        //    //    double vkm = Convert.ToDouble(txtkmactual.Text);
        //    //    int vlitros = Convert.ToInt32(txtcantidadlitros.Text);
        //    //    int vtc = Convert.ToInt32(cbotipocombustible.SelectedValue);

        //    //    double vcombustible;
        //    //    if (txtcombustibleutilizado.Text == "")
        //    //    {
        //    //        vcombustible = 0;
        //    //    }
        //    //    else
        //    //    {
        //    //        vcombustible = Convert.ToDouble(txtcombustibleutilizado.Text);
        //    //    }

        //    //    int vdistancia;
        //    //    if (txtdistanciarecorrida.Text == "")
        //    //    {
        //    //        vdistancia = 0;
        //    //    }
        //    //    else
        //    //    {
        //    //        vdistancia = Convert.ToInt32(txtdistanciarecorrida.Text);
        //    //    }

        //    //    string vtr = txttiemporelenti.Text;
        //    //    double vcr;
        //    //    if (txtutilizadorelenti.Text == "")
        //    //    {
        //    //        vcr = 0;
        //    //    }
        //    //    else
        //    //    {
        //    //        vcr = Convert.ToDouble(txtutilizadorelenti.Text);
        //    //    }
        //    //    if (Convert.ToBoolean(Session["Folio"]) == true && txtidc_Folio.Text.trim="")
        //    //    {
        //    //    }

        //    //    bool vcanseg= chkcandado.Checked;
        //    //    bool vcinseg = chkcincho.Checked;
        //    //    bool espseg = chkespuma.Checked;
        //    //    bool fremot = chkfrenomotor.Checked;
        //    //    bool vcallla = chkcalibracion.Checked;
        //    //    int intentos = 0;
        //    //    bool dem_int = false;

        //    //    string [] parametros= {"@pidc_usuario","@pdirecip","@pnombrepc","@pusuariopc" ,"@pidc_tcombustible","@pidc_vehiculo","@pidc_empleado","@pfecha","@pkilometraje","@plitros","@pcombustible",
        //    //                              "@pdistancia","@ptiempo_relenti","@pcomb_relenti","@psipasa","@pidc_tanque","@POBSERVACIONES","@pcandado_seguridad","@PCINCHO_SEGURIDAD","@PESPUMA_SEGURIDAD","@PFRENO_MOTOR","@PCALIBRACION_LLANTAS"};
        //    //    object[] valores = { Session["sidc_usuario"],ip,pc,usuariopc,vtc,idc_camion,vchofer,vfecha,vkm,vlitros,vcombustible,vdistancia,vtr,vcr,0,vidc_tanque,vobs,vcanseg,vcinseg,espseg,fremot,vcallla,};

        //    //    DBConnection CONEXION = new DBConnection();
        //    //    DataSet ds = new DataSet();

        //    //    if (Convert.ToBoolean(Session["guardar"])== true)
        //    //    {
        //    //        while (dem_int == false)
        //    //        {
        //    //            ds = CONEXION.Guardar_Carga_Combustible(parametros, valores);
        //    //            if (ds.Tables.Count > 0)
        //    //            {
        //    //                if (ds.Tables[0].Rows.Count > 0)
        //    //                {
        //    //                    CargarMsgBox2("Correcto", "Se Guardo el Registro con Exito. <BR/> No.Folio: " + Convert.ToString(ds.Tables[0].Rows[0].ItemArray[0]), false, 1);
        //    //                    break;
        //    //                }
        //    //                else
        //    //                {
        //    //                    intentos = intentos + 1;
        //    //                }
        //    //            }
        //    //            else
        //    //            {
        //    //                intentos = intentos + 1;
        //    //            }
        //    //        }

        //    //    }

        //    //}
        //    //else
        //    //{
        //    //    ScriptManager.GetCurrent(this.Page).SetFocus(txtkmactual);
        //    //}

        //}
        private void Msgbox2_click()
        {
            //if (Msgbox2.ValorSi_No() == true)
            //{
            //    if (Convert.ToBoolean(Session["Folio"]) == true)
            //    {
            //        string mensaje = "<script>window.open('Folios_Autorizacion.aspx?tipo=58','null', 'width=250px,height=200px,left=300,top=250,Menubar=no,Scrollbars=no,location=no'); </script>";
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), mensaje, false);
            //        Session["Folio"] = false;
            //        //btnguardar_Click(null, null);
            //    }
            //}
            //else
            //{
            //    if (Convert.ToBoolean(Session["Folio"]) == true)
            //    {
            //        Session["Folio"] = false;
            //        Session["primera_guardar"] = true;
            //        Session["cargar"] = false;
            //        Session["guardar"] = false;
            //    }

            //}
        }

        private void Msgbox1_Click()
        {
            //if (Msgbox1.ValorSi_No()==true)
            //{
            //    if (Convert.ToBoolean(Session["cargar"])==true)
            //    {
            //        Session["primera_guardar"] = false;
            //        Session["cargar"] = false;
            //        Session["guardar"] = true;
            //        if (Convert.ToDouble(txtkmactual.Text) < Convert.ToDouble(txtkmanterior.Text))
            //        {
            //            CargarMsgBox2("Verificar el Kilometraje", "El Nuevo Kilometraje no Puede ser Menor al Kilometraje Anterior... <BR/> ¿Deseas Capturar el Folio de Autorizacion?", true, 1);
            //            Session["Folio"] = true;
            //        }
            //        else
            //        {
            //            btnguardar_Click(null, null);
            //        }
            //    }

            //}
            //else
            //{
            //    if(Convert.ToBoolean(Session["cargar"])==true)
            //    {
            //        Session["primera_guardar"] = true;
            //        Session["cargar"] = false;
            //        Session["guardar"] = false;
            //    }

            //}
        }

        //Protected Sub Msgbox_Click() Handles Msgbox.Click
        //    If Msgbox.ValorSi_No = True Then
        //        'Para eliminar una Área.
        //    End If
        //End Sub

        protected Boolean validar_campos()
        {
            if (cbotipocombustible.SelectedIndex <= 0)
            {
                CargarMsgBox("Selecciona un Tipo de Combustible.");
              
                return false;
            }
            else if (txtidc_vehiculo.Text == "")
            {
                CargarMsgBox("Selecciona un Vehiculo.");
                return false;
            }
            else if (txtidc_empleado.Text == "" || txtidc_empleado.Text == "0")
            {
                CargarMsgBox("Debes Seleccionar el Chofer.");
                return false;
            }
            else if (txtkmactual.Text == "0" || txtkmactual.Text == "")
            {
                CargarMsgBox("Debes Capturar el Kilometraje.");
                ScriptManager.GetCurrent(this.Page).SetFocus(txtkmactual);
                txtkmactual.Focus();
                return false;
            }
            else if ((txtcantidadlitros.Text == "0" || txtcantidadlitros.Text == ""))
            {
                CargarMsgBox("Debes Capturar la Cantidad de Litros.");
                return false;
            }
            else if (cbotanque.SelectedIndex <= 0)
            {
                CargarMsgBox("Debes Seleccionar el Tanque");
                return false;
            }
            else
            {
                return true;
            }
        }

        //*****************************************************************************************************************
        protected void btng_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert ('Probando Script');", true);
        }

        //*****************************************************************************************************************
        protected void btnnuevo_Click(object sender, ImageClickEventArgs e)
        {
            limpiar_campos();
        }

        protected void limpiar_campos()
        {
            tipoCamion(false);
            txtidc_Folio.Text = "";
            cbotipocombustible.SelectedIndex = 0;
            txtcombustibleutilizado.Text = "";
            txtdistancia.Text = "";
            txtidc_vehiculo.Text = "";
            txtidc_empleado.Text = "";
            txt_tipo_camion.Text = "";
            txtcamion.Text = "";
            txtchofer.Text = "";
            txtkmactual.Text = "";
            txtrn.Text = "0.00";
            txtcantidadlitros.Text = "1";
            txtlitrosanterior.Text = "0";
            txtrendimientoanterior.Text = "0.00";
            btnagregarchofer.Visible = false;
            txtdistanciarecorrida.Text = "";
            txtrendimientopc.Text = "";
            txtutilizadorelenti.Text = "";
            txtfaltante.Text = "";
            chkvirtual.Checked = false;
            if (cbotanque.Items.Count > 0)
            {
                cbotanque.Items.Clear();
            }
            chkespuma.Text = "Espuma de Seguridad";
            chkespuma.Checked = false;
            chkcandado.Checked = false;
            chkcincho.Checked = false;
            chkfrenomotor.Checked = false;
            chkcalibracion.Text = "Calibración de Llantas Adecuada";
            chkcalibracion.Checked = false;
            txtobservaciones.Text = "";
            //txtobservaciones.Visible = false;
            chkreportebc.Checked = false;
            txtrn.Text = "0.00";
            txtdistancia.Text = "";
            txtkmanterior.Text = "0";
            txtrn.Text = "0.00";
            buscar_folio();
            cargar_todos_veh("", false);
            txtbveh.Text = "";
        }

        protected void btnsalir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("menu_principal_m.aspx");
        }

        protected void cbovehiculos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbovehiculos.Text != "Ver Todos")
                {
                    int idc_vehiculo = Convert.ToInt32(cbovehiculos.SelectedValue);
                    tipo_camion();
                    buscar_chofer(idc_vehiculo);
                    seleccionar_tipo_comb(idc_vehiculo);
                    ver_kilometraje_anterior(idc_vehiculo);
                    etiquetas_rendimiento();
                    ver_datos_tanque(idc_vehiculo);
                    combo_tanques(Convert.ToInt32(Session["sidc_usuario"]));
                    tipo_camion();
                    txtidc_vehiculo.Text = Convert.ToString(idc_vehiculo);
                }
                else
                {
                    cargar_todos_veh("", false);
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error: \\n \\u000B \\n" + ex.Message);
            }
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
        }

        protected void cbochoferes_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtidc_empleado.Text = Convert.ToString(cbovehiculos.SelectedValue);
        }

        protected void btnb_veh_Click(object sender, EventArgs e)
        {
            if (txtbveh.Text.Trim() != "")
            {
                cargar_todos_veh(txtbveh.Text, true);
            }
        }

        protected void btnb_emp_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = conexion.Buscar_Chofer_Vehiculo(Convert.ToString(txtbchofer.Text));
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            ds.Tables[0].Rows[i]["nombre"] = ds.Tables[0].Rows[i]["num_nomina"] + ".- " + ds.Tables[0].Rows[i]["nombre"];
                        }
                        cbochoferes.DataSource = ds;
                        cbochoferes.DataTextField = "nombre";
                        cbochoferes.DataValueField = "idc_empleado";
                        cbochoferes.DataBind();
                        txtidc_empleado.Text = Convert.ToString(cbovehiculos.SelectedValue);
                    }
                    else
                    {
                        CargarMsgBox("No se encontro Chofer.");
                        cbochoferes.Items.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Realizar Busqueda. \\n \\u000b \\n Error: \\n" + ex.Message);
            }
        }

        public void CargarMsgBox(string msj)
        {
            Alert.ShowAlertError(msj, this);
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar?','modal fade modal-info');", true);
        }
        protected void Yesfolio_Click(object sender, EventArgs e)
        {
            if (txtfolio.Text == "")
            {
                Alert.ShowAlertError("Ingrese el folio",this);
            }
            else {
                int folio = Convert.ToInt32(txtfolio.Text);
                DataTable dt = funciones.ValidarFolio(58,folio);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    if (Convert.ToBoolean(row["verificar"]) == true)
                    {
                        txtidc_Folio.Text = row["id_autoriza"].ToString();
                        txtfolio.Text = "";
                        Alert.ShowAlert("Folio Correcto, Ya puede Guardar.","Mensaje del Sistema",this);
                    }
                    else
                    {
                        txtfolio.Text = "";
                        txtfolio.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", " ModalConfirmf('Folio no Valido');", true);
                    }
                }
            }
        }
        protected void lnknuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Carga_de_Combustibles_m.aspx");
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string value = Session["Caso_Confirmacion"] as string;
                switch (value)
                {
                    case "Guardar":
                        Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        private void Guardar()
        {
            if (validar_campos() == true)
            {
                try
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert ('Funciona');", true);

                    if (txt_tipo_camion.Text == "8" || txt_tipo_camion.Text == "7")
                    {
                        if (txtcombustibleutilizado.Text == "0")
                        {

                            CargarMsgBox("Es Requerido Ingresar el Combustible Utilizado.");
                            return;
                        }
                        else if (txtdistanciarecorrida.Text == "0" || txtdistanciarecorrida.Text == "")
                        {
                            CargarMsgBox("Es Requerido Ingresar la Distancia Recorrida.");
                            return;
                        }
                        else if (txttiemporelenti.Text == "00:00" || txttiemporelenti.Text == "")
                        {
                            CargarMsgBox("Es Requerido Ingresar el Tiempo RELENTI.");
                            return;
                        }
                        else if (txtutilizadorelenti.Text == "0.00" || txtutilizadorelenti.Text == "")
                        {
                            CargarMsgBox("Es Requerido Ingresar el Combustible Utilizado en RELENTI.");
                            return;
                        }
                    }

                    if (chkreportebc.Checked == true)
                    {
                        if (txtobservaciones.Text.Length < 10)
                        {
                            CargarMsgBox("Las Observaciones del Reporte de Rendimiento debe tener Minimo 10 Caracteres.");
                            return;
                        }
                    }
                    int vidc_tanque = Convert.ToInt32(cbotanque.SelectedValue);
                    string vobs = txtobservaciones.Text;
                    
                    if ((Convert.ToDouble(txtkmactual.Text) <= Convert.ToDouble(txtkmanterior.Text)) && txtidc_Folio.Text == "" && Convert.ToBoolean(Session["Folio"]) == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "confirmar(2); ", true);
                        return;
                    }

                    //}
                    string ip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    string pc = funciones.GetPCName();//nombre pc usuario
                    string usuariopc = funciones.GetUserName();//usuario pc
                    int idc_camion = Convert.ToInt32(txtidc_vehiculo.Text);
                    int vchofer = Convert.ToInt32(txtidc_empleado.Text);
                    DateTime vfecha = Convert.ToDateTime(txtfecha.Text);
                    double vkm;
                    if (txtkmactual.Text == "")
                    {
                        vkm = 0;
                    }
                    else
                    {
                        vkm = Convert.ToDouble(txtkmactual.Text);
                    }

                    int vlitros = Convert.ToInt32(txtcantidadlitros.Text);
                    int vtc = Convert.ToInt32(cbotipocombustible.SelectedValue);

                    double vcombustible;
                    if (txtcombustibleutilizado.Text == "")
                    {
                        vcombustible = 0;
                    }
                    else
                    {
                        vcombustible = Convert.ToDouble(txtcombustibleutilizado.Text);
                    }

                    int vdistancia;
                    if (txtdistanciarecorrida.Text == "")
                    {
                        vdistancia = 0;
                    }
                    else
                    {
                        vdistancia = Convert.ToInt32(txtdistanciarecorrida.Text);
                    }

                    string vtr = txttiemporelenti.Text;
                    double vcr;
                    if (txtutilizadorelenti.Text == "")
                    {
                        vcr = 0;
                    }
                    else
                    {
                        vcr = Convert.ToDouble(txtutilizadorelenti.Text);
                    }

                    bool vcanseg = chkcandado.Checked;
                    bool vcinseg = chkcincho.Checked;
                    bool espseg = chkespuma.Checked;
                    bool fremot = chkfrenomotor.Checked;
                    bool vcallla = chkcalibracion.Checked;
                    int intentos = 0;
                    bool dem_int = false;

                    string[] parametros = {"@pidc_usuario","@pdirecip","@pnombrepc","@pusuariopc" ,"@pidc_tcombustible","@pidc_vehiculo","@pidc_empleado","@pfecha","@pkilometraje","@plitros","@pcombustible",
                                      "@pdistancia","@ptiempo_relenti","@pcomb_relenti","@psipasa","@pidc_tanque","@POBSERVACIONES","@pcandado_seguridad","@PCINCHO_SEGURIDAD","@PESPUMA_SEGURIDAD","@PFRENO_MOTOR","@PCALIBRACION_LLANTAS"};
                    object[] valores = { Session["sidc_usuario"], ip, pc, usuariopc, vtc, idc_camion, vchofer, vfecha, Convert.ToInt32(vkm), vlitros, vcombustible, vdistancia, vtr, vcr, 0, vidc_tanque, vobs, vcanseg, vcinseg, espseg, fremot, vcallla };
                    
                    DataSet ds = new DataSet();
                    CombustibleCOM componente = new CombustibleCOM();
                    ds = componente.AgregarCarga(parametros,valores);
                    DataRow row = ds.Tables[0].Rows[0];
                    string msg = row["mensaje"].ToString();
                    if (msg == "")
                    {
                        limpiar_campos();
                        string mensaje = "Se Guardo el Registro con Exito.\\n \\u000B \\nNo.Folio:\\n" + Convert.ToString(row["folio"].ToString());
                        Alert.ShowGiftMessage("Estamos procesando la solicitud.", "Espere un Momento", "Carga_de_Combustibles_m.aspx", "imagenes/loading.gif", "2000", mensaje, this);
                    }
                    else {

                        CargarMsgBox(msg);
                    }

                }
                catch (Exception ex)
                {
                    CargarMsgBox(ex.Message);
                }
            }
        }
    }
}