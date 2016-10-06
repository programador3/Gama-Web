using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class editar_contacto : System.Web.UI.Page
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
                Session["dt_tbl_contact"] = null;
                cargar_tipo_contacto();
                cargar_titulos();
                txtcliente.Text = funciones.de64aTexto(Request.QueryString["n"]);
                txtrfc.Text = funciones.de64aTexto(Request.QueryString["r"]);
                txtcve.Text = funciones.de64aTexto(Request.QueryString["c"]);
                txttipo.Text = Request.QueryString["tipo"];
                switch (txttipo.Text)
                {
                    case "0"://nuevo
                        txtid.Text = funciones.de64aTexto(Request.QueryString["idc_cliente"]);
                        cbxactivo.Checked = true;
                        cbxactivo.Visible = false;
                        break;
                    case "1"://edicion
                        int idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["cdi"]));
                        cbxactivo.Visible = true;
                        if (idc_cliente > 0)
                        {
                            cargar_datos_contacto(idc_cliente);
                            txtidc_telcli.Text = idc_cliente.ToString();
                        }
                        break;
                    case "2"://vista pendientes
                        txtnombre.ReadOnly = true;
                        txttelefono.ReadOnly = true;
                        txtext.ReadOnly = true;
                        txtemail.ReadOnly = true;
                        txtfunciones.ReadOnly = true;
                        txthobbies.ReadOnly = true;
                        txtequipo.ReadOnly = true;
                        txtcumple.ReadOnly = true;
                        cbotipo.Enabled = false;
                        cbotitulo.Enabled = false;
                        btnGuardar.Visible = false;
                        edicion.Visible = true;
                        btnCancelar.Text = "Regresar";
                        cargar_datos_contacto_pendientes(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"])));
                        break;
                }
               
            }
        }

        private void cargar_datos_contacto(int idc_telcli)
        {
           
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                entidad.pidc_telcli = idc_telcli;
                DataSet ds = com.sp_datos_editar_contacto(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = default(DataRow);
                    row = ds.Tables[0].Rows[0];
                    txtnombre.Text = row["nombre"].ToString().Trim();

                    txtcelular.Text = row["celular"].ToString().Trim();
                    txtemail.Text = row["email"].ToString().Trim();
                    txtfunciones.Text = row["funciones"].ToString().Trim();
                    txtcumple.Text = Convert.ToDateTime(row["fecha_cumple"]).ToString("yyyy-MM-dd");
                    txthobbies.Text = row["hobies"].ToString().Trim();
                    txtequipo.Text = row["equipo"].ToString().Trim();
                    cbotipo.SelectedValue = row["idc_tcontacto"].ToString().Trim();
                    cbotitulo.SelectedValue = row["idc_titulo"].ToString().Trim();

                    cbxactivo.Checked = Convert.ToBoolean(row["activo"]);
                    string[] separators = { "*" };
                    string value = row["telefono"].ToString();
                    string[] tel_ext = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    if (tel_ext.Length > 1)
                    {
                        txttelefono.Text = tel_ext[0];
                        txtext.Text = tel_ext[1];
                    }
                    else
                    {
                        txttelefono.Text = row["telefono"].ToString().Trim();
                    }
                }
                else
                {
                    CargarMsgBox("No Se Cargo Información de Contacto.");
                    return;
                }
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Cargar Titulos. \\n \\u000B \\n Error: \\n" + ex.Message);
            }
        }
        private void cargar_datos_contacto_pendientes(int idc_cliente)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                entidad.Pidc_cliente = idc_cliente;
                DataSet ds = com.sp_datos_agregar_contactos(entidad);
                gridcontactos.DataSource = ds.Tables[0];
                gridcontactos.DataBind();
                Session["dt_tbl_contact"] = ds.Tables[0];
            }
            catch (Exception ex)
            {
                CargarMsgBox("Error al Cargar Titulos. \\n \\u000B \\n Error: \\n" + ex.Message);
            }
        }
        protected void gridcontactos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string idc_solcambio = gridcontactos.DataKeys[index].Values["idc_solcambio"].ToString();
            DataTable dt = Session["dt_tbl_contact"] as DataTable;
            DataView view = dt.DefaultView;
            view.RowFilter = "idc_solcambio = "+ idc_solcambio + "";
            if (view.ToTable().Rows.Count > 0)
            {

                DataRow row = view.ToTable().Rows[0];
                txtnombre.Text = row["nombre"].ToString().Trim();

                txtcelular.Text = row["celular"].ToString().Trim();
                txtemail.Text = row["email"].ToString().Trim();
                txtfunciones.Text = row["funciones"].ToString().Trim();
                txtcumple.Text = Convert.ToDateTime(row["fecha_cumple"]).ToString("yyyy-MM-dd");
                txthobbies.Text = row["hobies"].ToString().Trim();
                txtequipo.Text = row["equipo"].ToString().Trim();
                cbotipo.Items.FindByText(row["tipo_contacto"].ToString().Trim());
                cbotitulo.Items.FindByText(row["titulo"].ToString().Trim());

                cbxactivo.Checked = Convert.ToBoolean(row["activo"]);
                string[] separators = { "*" };
                string value = row["telefono"].ToString();
                string[] tel_ext = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (tel_ext.Length > 1)
                {
                    txttelefono.Text = tel_ext[0];
                    txtext.Text = tel_ext[1];
                }
                else
                {
                    txttelefono.Text = row["telefono"].ToString().Trim();
                }
                ScriptManager.RegisterStartupScript(this, GetType(),"myfuncionnameeditar", "ScrollinEdit('nombrediv');", true);
            }
        }

        private void cargar_tipo_contacto()
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_combo_tipos_contacto(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbotipo.DataSource = ds.Tables[0];
                    cbotipo.DataTextField = "descripcion";
                    cbotipo.DataValueField = "idc_tcontacto";
                    cbotipo.DataBind();
                }
                else
                {
                    cbotipo.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void cargar_titulos()
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_combo_titulos_profesionales(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbotitulo.DataSource = ds.Tables[0];
                    cbotitulo.DataTextField = "nombre";
                    cbotitulo.DataValueField = "idc_titulo";
                    cbotitulo.DataBind();
                }
                else
                {
                    cbotitulo.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void cbxsincumple_CheckedChanged(object sender, EventArgs e)
        {
            txtcumple.Enabled = cbxsincumple.Checked == true ? false : true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string vextension = txtext.Text.Trim();
            string vdato = txttelefono.Text.Trim();
            string vemail = txtemail.Text.Trim();
            string vnombre = txtnombre.Text.Trim();
            string vcelular = txtcelular.Text.Trim();

            string vfecha = "";

            string vhobbies = txthobbies.Text.Trim();
            string vequipo = txtequipo.Text.Trim();
            int vidc_tcontacto = Convert.ToInt32(cbotipo.SelectedValue);
            string vfunciones = txtfunciones.Text.Trim();
            int vidc_titulo = Convert.ToInt32(cbotitulo.SelectedValue);
            bool vactivo = cbxactivo.Checked;

            //IF thisformset.op_contacto = 'M'
            int vidc_telcli = 0;
            string vopcion = "";
            int vidc_cliente = 0;
            if (txttipo.Text == "1")
            {
                vidc_telcli = Convert.ToInt32(txtidc_telcli.Text.Trim());
                vopcion = "M";
                vidc_cliente = 0;
            }
            else if (txttipo.Text == "0")
            {
                vidc_telcli = 0;
                vopcion = "A";
                vidc_cliente = Convert.ToInt32(txtid.Text.Trim());
            }
            bool error = false;
            if (cbxactivo.Checked == true)
            {
                if (cbotipo.SelectedIndex < 0)
                {
                    CargarMsgBox("Debes Especificar el Tipo de Contacto.");
                    error = true;
                }
                else if (cbotitulo.SelectedIndex < 0)
                {
                    CargarMsgBox("Es Necesario el Titulo Profesional.");
                    error = true;
                }
                else if (string.IsNullOrEmpty(txtnombre.Text.Trim()))
                {
                    CargarMsgBox("Es Necesario el Nombre del Contacto.");
                    error = true;
                }
                else if (string.IsNullOrEmpty(vdato.Trim()) & string.IsNullOrEmpty(vcelular.Trim()) & string.IsNullOrEmpty(vemail.Trim()))
                {
                    CargarMsgBox("Al menos debes incluir Telefono o Celular.");
                    error = true;
                }
                else if (!string.IsNullOrEmpty(vdato) & vdato.Length < 8)
                {
                    CargarMsgBox("El telefono debe contener al menos 8 Caracteres.");
                    error = true;
                }
                else if (!string.IsNullOrEmpty(vcelular.Trim()) & vcelular.Length < 10)
                {
                    CargarMsgBox("El Celular debe contener al menos 10 Caracteres");
                    error = true;
                }
                else if (vhobbies.Length > 8000)
                {
                    CargarMsgBox("Hobbies Rebasa el Numero de Caracteres Permitidos(8000).");
                    error = true;
                }
                else if (cbxsincumple.Checked == false && txtcumple.Text == "")
                {
                    CargarMsgBox("Ingrese Fecha de Cumpleaños");
                    error = true;
                }
            }
            if( error == false)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar este Contacto?','modal fade modal-info');", true);
            }
        }

        private void CargarMsgBox(string value)
        {
            Alert.ShowAlertError(value, this.Page);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ficha_cliente_m.aspx");
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string vextension = txtext.Text == "" ? "":"*" + txtext.Text.Trim();
                string vdato = txttelefono.Text.Trim();
                string vemail = txtemail.Text.Trim();
                string vnombre = txtnombre.Text.Trim();
                string vcelular = txtcelular.Text.Trim();

                string vfecha = "";

                string vhobbies = txthobbies.Text.Trim();
                string vequipo = txtequipo.Text.Trim();
                int vidc_tcontacto = Convert.ToInt32(cbotipo.SelectedValue);
                string vfunciones = txtfunciones.Text.Trim();
                int vidc_titulo = Convert.ToInt32(cbotitulo.SelectedValue);
                bool vactivo = cbxactivo.Checked;

                //IF thisformset.op_contacto = 'M'
                int vidc_telcli = 0;
                string vopcion = "";
                int vidc_cliente = 0;
                if (txttipo.Text == "1")
                {
                    vidc_telcli = Convert.ToInt32(txtidc_telcli.Text.Trim());
                    vopcion = "M";
                    vidc_cliente = 0;
                }
                else if (txttipo.Text == "0")
                {
                    vidc_telcli = 0;
                    vopcion = "A";
                    vidc_cliente = Convert.ToInt32(txtid.Text.Trim());
                }
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
                        entidad.pidc_telcli = vidc_telcli;
                        entidad.pemail = vemail;
                        entidad.pnombre = vnombre;
                        entidad.pcelular = vcelular;
                        entidad.pfecha = Convert.ToDateTime(txtcumple.Text);
                        entidad.pactivo = vactivo;
                        entidad.phobbie = vhobbies;
                        entidad.pequipo = vequipo;
                        entidad.pidc_tcontacto = vidc_tcontacto;
                        entidad.pfunciones = vfunciones;
                        entidad.pidc_titulo = vidc_titulo;
                        entidad.ptelefono = vdato + vextension;
                        entidad.Pidc_cliente = vidc_cliente;
                        entidad.popcion = vopcion;
                        DataSet ds = com.sp_aclientes_tel_solcambio_nuevo(entidad);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos procesando la información.", "Espere un Momento", "ficha_cliente_m.aspx", 
                                "imagenes/loading.gif", "1000", "Contacto Guardado Correctamente", this);
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

    }
}