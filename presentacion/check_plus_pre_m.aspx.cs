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
    public partial class check_plus_pre_m : System.Web.UI.Page
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
                if (Request.QueryString["idc_cliente"] == null)
                {
                    //btnguardarcuenta.Attributes["onclick"] = "return check(" & gridcuentas.ClientID & ");"
                    lnkbuscarcliente.Visible = true;
                    btnguardarcuenta.Attributes["onclick"] = "return guardar_cuenta();";
                    txtidc_cliente.Attributes["onchange"] = "return cuentas_cliente();";
                    txtmonto.Attributes["onkeydown"] = "return soloNumeros2(event,true);";
                    txtmonto.Attributes["onblur"] = "return validarnumero(this);";
                    txtmonto.Attributes["onclick"] = "this.select();";
                    imgnuevo.Attributes["onclick"] = "return false;";
                    cargar_folio_gama();
                    cargar_bancos();
                    txtcuenta.Attributes["onkeydown"] = "return soloNumeros(event);";
                    txtcuenta.Attributes["onkeyup"] = "return validar_length(this,15);";
                    btnguardar.Attributes["onclick"] = "return guardar();";
                    
                }
                else
                {
                    txtidc_cliente.Text = funciones.de64aTexto(Request.QueryString["idc_cliente"]);
                    int idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
                    datos_cliente(idc_cliente);
                    btncuentas_Click(null, EventArgs.Empty);
                    cargar_bancos();
                    cargar_folio_gama();
                    txtcuenta.Attributes["onkeydown"] = "return soloNumeros(event);";
                    txtcuenta.Attributes["onkeyup"] = "return validar_length(this,15);";
                    btnguardar.Attributes["onclick"] = "return guardar();";

                    //btnguardarcuenta.Attributes["onclick"] = "return check(" & gridcuentas.ClientID & ");"

                    lnkbuscarcliente.Visible = false;
                    btnguardarcuenta.Attributes["onclick"] = "return guardar_cuenta();";
                    txtidc_cliente.Attributes["onchange"] = "return cuentas_cliente();";
                    txtmonto.Attributes["onkeydown"] = "return soloNumeros2(event,true);";
                    txtmonto.Attributes["onblur"] = "return validarnumero(this);";
                    txtmonto.Attributes["onclick"] = "this.select();";
                    imgnuevo.Attributes["onclick"] = "return datos();";
                }
                
            }
        }
        public void limpiar_datos_controles()
        {
            if (Session["idc_cliente"] == null)
            {
                imgnuevo.Attributes["onclick"] = "return false;";
                lnkbuscarcliente.Visible = true;
                txtcuenta.Enabled = false;
                cbobancos.Enabled = false;
                txtmonto.Enabled = false;
                cbonombres.Items.Clear();
                btnguardar.Enabled = false;
                btncancelar.Enabled = false;
                btnguardarcuenta.Enabled = false;
                txtrfc.Text = "";
                txtcve.Text = "";
                txtcliente.Text = "";
                txtclave.Text = "";
                txtidc_cliente.Text = "";
                //gridcuentas.DataSource = Nothing
                //gridcuentas.DataBind()
                txtcuenta.Attributes.Remove("onclick");
                txtmonto.Attributes.Remove("onclick");
            }
            else
            {

                lnkbuscarcliente.Visible = true;
                if (cbonombres.Items.Count > 0)
                {
                    cbonombres.SelectedIndex = 0;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(),"dededed", "<script>datos_persona();</script>", false);
                btncuentas_Click(null, EventArgs.Empty);
            }
            txtnombrepersona.Text = "";
            txtcalle.Text = "";
            txtnumero.Text = "";
            txtfolio2.Text = "";
            txtidc_colonia.Text = "";
            txtmonto.Text = "";
            txtcuenta.Text = "";
        }
        private void datos_cliente(int idc_cliente)
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
                    txtrfc.Text = ds.Tables[0].Rows[0]["rfccliente"].ToString();
                    txtcve.Text = ds.Tables[0].Rows[0]["cveadi"].ToString();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void cargar_bancos()
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_bancos();
                if ((ds.Tables[0].Rows.Count > 0))
                {
                    cbobancos.DataSource = ds.Tables[0];
                    cbobancos.DataValueField = "idc_banco";
                    cbobancos.DataTextField = "nom_corto";
                    cbobancos.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void cargar_folio_gama()
        {
            string folio_gama = "";
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_folios(703);
                folio_gama = ds.Tables[0].Rows[0]["no_folio"].ToString();
                txtfolio.Text = Convert.ToString(Convert.ToInt32(folio_gama) + 1);
            }
            catch (Exception ex)
            {
                txtfolio.Text = "0";
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void btncuentas_Click(object sender, EventArgs e)
        {
            validar_existencia_cuenta(Convert.ToInt32(txtidc_cliente.Text.Trim()), 0, 1);

            lnkbuscarcliente.Visible = false;
            imgnuevo.Attributes["onclick"] = "return datos();";
            btnguardarcuenta.Enabled = true;
            btnguardar.Enabled = true;
            btncancelar.Enabled = true;
            txtcuenta.Enabled = true;
            cbobancos.Enabled = true;
            txtmonto.Enabled = true;
            cargar_cliente_checkplus(Convert.ToInt32(txtidc_cliente.Text.Trim()));
        }

        public Boolean validar_existencia_cuenta(int idc_cliente, int idc_banco, int tipo)
        {           
            DataView dv = new DataView();
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_cuentascli(idc_cliente);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (tipo == 2)
                    {
                        dv.Table = ds.Tables[0];
                        dv.RowFilter = "num_cuenta like '" + txtcuenta.Text.Trim() + "' and idc_banco=" + cbobancos.SelectedValue.ToString();
                        if (dv.ToTable().Rows.Count > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        ds.Tables[0].Columns.Add("descripcion");
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            ds.Tables[0].Rows[i]["descripcion"] = ds.Tables[0].Rows[i]["nom_corto"].ToString() + " " + ds.Tables[0].Rows[i]["num_cuenta"].ToString();
                        }
                        cbocuentas.DataSource = ds.Tables[0];
                        cbocuentas.DataTextField = "descripcion";
                        cbocuentas.DataValueField = "idc_cuentacli";
                        cbocuentas.DataBind();
                        return true;
                    }

                }
                else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
                return false;
            }
        }

        public void cargar_cliente_checkplus(int idc_cliente)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_clientes_chekplus(idc_cliente,0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbonombres.DataSource = ds.Tables[0];
                    cbonombres.DataTextField = "nombre";
                    cbonombres.DataValueField = "idc_dirchekplus";
                    cbonombres.DataBind();
                    //btnselec.Attributes("onclick") = "return seleccionar();"
                    cbonombres.Attributes["onchange"] = "return datos_persona();";

                    gridnombres.DataSource = ds.Tables[0];
                    gridnombres.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ededed", "<script>datos_persona();</script>", false);
                }
                else
                {
                    cbonombres.Items.Clear();
                    //btnselec.Attributes.Remove("onclick")
                    gridnombres.DataSource = null;
                    gridnombres.DataBind();
                    cbonombres.Attributes.Remove("onchange");
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            string ulr = Request.QueryString["idc_cliente"] == null ? "check_plus_pre_m.aspx" : "check_plus_pre_m.aspx?idc_cliente=" + Request.QueryString["idc_cliente"];
            Response.Redirect(ulr);
        }

        protected void btng_Click(object sender, EventArgs e)
        {                      
            try
            {
                DataSet ds = new DataSet();
                if (cbocuentas.Items.Count <= 0)
                {
                    Alert.ShowAlertError("Seleccione Cuenta", this);
                    return;
                }
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                ds = com.sp_achek_plus_pre_nuevo(Convert.ToInt32(cbocuentas.SelectedValue), Convert.ToDecimal(txtmonto.Text.Trim()), 0, txtclave.Text.Trim(),
                    txtnombrepersona.Text.Trim(), txtcalle.Text.Trim(), txtnumero.Text.Trim(), Convert.ToInt32(txtidc_colonia.Text.Trim()), 
                    txtfolio2.Text.Trim(),Convert.ToInt32(Session["sidc_usuario"]),
                      funciones.GetLocalIPAddress(), funciones.GetPCName(), funciones.GetUserName(), "A", " ", 0);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje == "")
                {
                   
                    string ulr = Request.QueryString["idc_cliente"] == null ? "check_plus_pre_m.aspx" : "check_plus_pre_m.aspx?idc_cliente=" + Request.QueryString["idc_cliente"];
                        Alert.ShowGiftMessage("Estamos Guardando los Datos", 
                        "Espere un Momento", ulr, "imagenes/loading.gif", "1000",
                        "Se Guardo la Pre-Autorización del Cheque con Exito. \\n \\u000B \\n" + "No. Folio: " +
                        ds.Tables[0].Rows[0]["folio"].ToString().Trim(), this);

                }
                else {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }

        }

        protected void btnsalir_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["idc_cliente"] == null)
            {
                Response.Redirect("menu.aspx");
            }
            else
            {
                Response.Redirect("ficha_cliente_m.aspx");
            }

        }

        protected void btnrefresh_Click(object sender, EventArgs e)
        {
            cargar_cliente_checkplus(Convert.ToInt32(txtidc_cliente.Text.Trim()));
        }

        protected void btnguarda_cuenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtcuenta.Text.Trim()))
                {
                    if (txtcuenta.Text.Trim().Length > 15)
                    {
                        Alert.ShowAlertError("La Cuenta No Es Correcta.", this.Page);
                        return;
                    }
                }
                else
                {
                    Alert.ShowAlertError("La Cuenta No Es Correcta.", this.Page);
                    return;
                }

                bool existe = false;
                existe = validar_existencia_cuenta(Convert.ToInt32(txtidc_cliente.Text.Trim()), Convert.ToInt32(cbobancos.SelectedValue), 2);

                if (existe == true)
                {
                    Alert.ShowAlertError("La Cuenta Ya Existe en la Base de Datos.", this.Page);
                    return;
                }
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_acuentasclien_nuevo(Convert.ToInt32(txtidc_cliente.Text.Trim()), Convert.ToInt32(cbobancos.SelectedValue), txtcuenta.Text.Trim(),
                    Convert.ToInt32(Session["sidc_usuario"]), funciones.GetLocalIPAddress(), funciones.GetPCName(), funciones.GetUserName(), 'A', " ", 0);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje == "")
                {
                    validar_existencia_cuenta(Convert.ToInt32(txtidc_cliente.Text.Trim()), 0, 1);
                    txtcuenta.Text = "";
                    cbobancos.SelectedIndex = 0;
                    Alert.ShowAlert("Se Guardo la cuenta con Exito", "Mensaje del Sistema", this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void lnkbuscarcliente_Click(object sender, EventArgs e)
        {
            div_buscar.Visible = true;
            txtbuscarcliente.Focus();
        }
        protected void lnkbuscar_Click(object sender, EventArgs e)
        {
            buscar_datos_cliente(txtbuscarcliente.Text.Trim());
        }
        protected void lnkbuscaraceptar_Click(object sender, EventArgs e)
        {
            if (cboclientes.Items.Count > 0)
            {
                limpiar_datos_controles();
                div_buscar.Visible = false;
                lnkbuscarcliente.Visible = false;
                int idc_cliente = Convert.ToInt32(cboclientes.SelectedValue);
                txtidc_cliente.Text = idc_cliente.ToString();
                
                datos_cliente(idc_cliente);
                btncuentas_Click(null, EventArgs.Empty);
                cargar_bancos();
                cargar_folio_gama();
                txtcuenta.Attributes["onkeydown"] = "return soloNumeros(event);";
                txtcuenta.Attributes["onkeyup"] = "return validar_length(this,15);";
                btnguardar.Attributes["onclick"] = "return guardar();";

                //btnguardarcuenta.Attributes["onclick"] = "return check(" & gridcuentas.ClientID & ");"

                lnkbuscarcliente.Visible = false;
                btnguardarcuenta.Attributes["onclick"] = "return guardar_cuenta();";
                txtidc_cliente.Attributes["onchange"] = "return cuentas_cliente();";
                txtmonto.Attributes["onkeydown"] = "return soloNumeros2(event,true);";
                txtmonto.Attributes["onblur"] = "return validarnumero(this);";
                txtmonto.Attributes["onclick"] = "this.select();";
                imgnuevo.Attributes["onclick"] = "return datos();";
            }
            else
            {
                Alert.ShowAlertError("Seleccione un valor valido", this);
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

    }
}