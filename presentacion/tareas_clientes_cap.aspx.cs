using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class tareas_clientes_cap : System.Web.UI.Page
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
                Session["fecha_filtro"] = null;
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_articulo");
                dt.Columns.Add("codigo");
                dt.Columns.Add("desart");
                dt.Columns.Add("um");
                dt.Columns.Add("tipo");
                dt.Columns.Add("sel");
                dt.Columns.Add("cantidad");
                dt.Columns.Add("meta");
                dt.Columns.Add("precio");
                dt.Columns.Add("decimales");
                dt.Columns.Add("observ");
                dt.Columns.Add("fecha");
                Session["dt_arti"] = dt;

                txtfecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                int idc_cliente = 0;
                idc_cliente = Convert.ToInt32(Session["idc_cliente"]);
                Cargar_datos_cliente(idc_cliente);

                cargar_tareas(idc_cliente, Convert.ToInt32(Session["idc_agente"]), DateTime.Today);
                btnver.Visible = true;
                btnocultar.Visible = false;
                repeat.Visible = false;
            }
        }

        private void Cargar_datos_cliente(int idc_cliente)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                entidad.Pidc_cliente = idc_cliente;
                DataSet ds = com.sp_datos_cliente(entidad);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtcliente.Text = dt.Rows[0]["nombre"].ToString().Trim();
                    txtrfc.Text = dt.Rows[0]["rfccliente"].ToString().Trim();
                    txtcve.Text = dt.Rows[0]["cveadi"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void UpdatetoToTable(string idc_articulo, string cantidad, string meta, string observ)
        {
            DataTable dt = default(DataTable);
            dt = (DataTable)Session["dt_arti"];
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = "idc_articulo=" + idc_articulo + "";
            if (dv.ToTable().Rows.Count > 0)
            {
                foreach (DataRow rows in dt.Rows)
                {
                    int idc = 0;
                    idc = Convert.ToInt32(rows["idc_articulo"]);
                    if (idc.ToString() == idc_articulo)
                    {
                        rows["idc_articulo"] = idc_articulo;
                        rows["cantidad"] = cantidad;
                        rows["meta"] = meta;
                        rows["observ"] = observ;
                        rows["fecha"] = DateTime.Now;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
            Session["dt_arti"] = dt;
        }

        public void AddtoToTable(string idc_articulo, string codigo, string desart, string um, string tipo, string sel, string cantidad, string meta, string precio, string decimales,
        string observ)
        {
            DataTable dt = default(DataTable);
            dt = (DataTable)Session["dt_arti"];
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = "idc_articulo=" + idc_articulo + "";
            if (dv.ToTable().Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["idc_articulo"] = idc_articulo;
                dr["codigo"] = codigo;
                dr["desart"] = desart;
                dr["um"] = um;
                dr["tipo"] = tipo;
                dr["sel"] = sel;
                dr["cantidad"] = cantidad;
                dr["meta"] = meta;
                dr["precio"] = precio;
                dr["decimales"] = decimales;
                dr["observ"] = observ;
                dr["fecha"] = DateTime.Now;
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow rows in dt.Rows)
                {
                    int idc = 0;
                    idc = Convert.ToInt32(rows["idc_articulo"]);
                    if (idc.ToString() == idc_articulo)
                    {
                        rows["idc_articulo"] = idc_articulo;
                        rows["codigo"] = codigo;
                        rows["desart"] = desart;
                        rows["um"] = um;
                        rows["tipo"] = tipo;
                        rows["sel"] = sel;
                        rows["cantidad"] = cantidad;
                        rows["meta"] = meta;
                        rows["precio"] = precio;
                        rows["decimales"] = decimales;
                        rows["observ"] = observ;
                        rows["fecha"] = DateTime.Now;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
            Session["dt_arti"] = dt;
        }

        public void DeleteToTable(int idc_articulo)
        {
            DataTable dt = default(DataTable);
            dt = (DataTable)Session["dt_arti"];
            foreach (DataRow rows in dt.Rows)
            {
                int idc = 0;
                idc = Convert.ToInt32(rows["idc_articulo"]);
                if (idc == idc_articulo)
                {
                    dt.Rows.Remove(rows);
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            Session["dt_arti"] = dt;
        }

        public void cargar_nuevos_articulos(int idc_cliente, string filtro)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                entidad.Pidc_cliente = idc_cliente;
                entidad.Pnombrepc = filtro;
                DataSet ds = com.sp_articulos_agregar_precio(entidad);
                DataTable dt = ds.Tables[0];
                Session["dt_art_nuevos"] = dt;
                cbarticulos.DataValueField = "idc_articulo";
                cbarticulos.DataTextField = "desart";
                cbarticulos.DataSource = dt;
                cbarticulos.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void CargarRepeat()
        {
            DataTable dt = default(DataTable);
            DataTable dtcopy = default(DataTable);
            dt = (DataTable)Session["dt_arti"];
            dtcopy = dt.Copy();
            DataView dv = dtcopy.DefaultView;
            dv.Sort = "fecha desc,sel desc";
            DataTable sortedDT = dv.ToTable();
            repeat.DataSource = sortedDT;
            repeat.DataBind();
        }

        private void SumaMeta()
        {
            decimal meta = default(decimal);
            foreach (RepeaterItem item in repeat.Items)
            {
                Label lblprecio = item.FindControl("lblprecio") as Label;
                TextBox txtcantidad = item.FindControl("txtcantidad") as TextBox;
                TextBox txtmeta = item.FindControl("txtmeta") as TextBox;
                TextBox txtobservaciones = item.FindControl("txtobservaciones") as TextBox;
                decimal cantidad = default(decimal);
                decimal precio = default(decimal);
                if (txtcantidad.Text == "" | string.IsNullOrEmpty(txtcantidad.Text))
                {
                    txtcantidad.Text = "0";
                }
                cantidad = Convert.ToDecimal(txtcantidad.Text);
                precio = Convert.ToDecimal(lblprecio.Text);
                meta = meta + (precio * cantidad);
            }
            txtmetaglobal.Text = decimal.Round(meta, 2).ToString();
        }

        private void cargar_tareas(int idc_cliente, int idc_agente, DateTime fecha)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                entidad.Pidc_cliente = idc_cliente;
                entidad.Pidc_agente = idc_agente;
                entidad.pfecha = fecha;
                DataSet ds = com.SP_VER_TAREAS_CLIENTE_DETALLES_TODO(entidad);
                DataTable dt = default(DataTable);
                dt = ds.Tables[1];
                foreach (DataRow row in dt.Rows)
                {
                    AddtoToTable(row["idc_articulo"].ToString(), row["codigo"].ToString(), row["desart"].ToString(), row["um"].ToString(),
                        row["tipo"].ToString(), row["sel"].ToString(), row["cantidad"].ToString(), row["meta"].ToString(), row["precio"].ToString(),
                        row["decimales"].ToString(), row["observ"].ToString());
                }
                CargarRepeat();
                SumaMeta();
                Session["fecha_filtro"] = fecha;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void btnver2_Click(object sender, EventArgs e)
        {
            btnver.Visible = true;
            btnocultar.Visible = false;
            repeat.Visible = false;
        }

        protected void btnver_Click(object sender, EventArgs e)
        {
            int idc_agente = 0;
            int idc_cliente = 0;
            DateTime fecha = default(DateTime);
            DateTime fecha2 = default(DateTime);

            DataTable dt = new DataTable();
            idc_agente = Convert.ToInt32(Session["idc_agente"]);
            idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
            if (idc_agente > 0 & idc_cliente > 0)
            {
                if (!string.IsNullOrEmpty(txtfecha.Text) & Session["fecha_filtro"] == null)
                {
                    dt.Columns.Add("idc_articulo");
                    dt.Columns.Add("codigo");
                    dt.Columns.Add("desart");
                    dt.Columns.Add("um");
                    dt.Columns.Add("tipo");
                    dt.Columns.Add("sel");
                    dt.Columns.Add("cantidad");
                    dt.Columns.Add("meta");
                    dt.Columns.Add("precio");
                    dt.Columns.Add("decimales");
                    dt.Columns.Add("observ");
                    dt.Columns.Add("fecha");
                    Session["dt_arti"] = dt;
                    fecha = Convert.ToDateTime(txtfecha.Text);
                    cargar_tareas(idc_cliente, idc_agente, fecha);
                    btnver.Visible = false;
                    btnocultar.Visible = true;
                    repeat.Visible = true;
                }
                else if (!string.IsNullOrEmpty(txtfecha.Text) & (Session["fecha_filtro"] != null))
                {
                    fecha = (DateTime)Session["fecha_filtro"];
                    fecha2 = Convert.ToDateTime(txtfecha.Text);
                    if (!(fecha == fecha2))
                    {
                        dt.Columns.Add("idc_articulo");
                        dt.Columns.Add("codigo");
                        dt.Columns.Add("desart");
                        dt.Columns.Add("um");
                        dt.Columns.Add("tipo");
                        dt.Columns.Add("sel");
                        dt.Columns.Add("cantidad");
                        dt.Columns.Add("meta");
                        dt.Columns.Add("precio");
                        dt.Columns.Add("decimales");
                        dt.Columns.Add("observ");
                        dt.Columns.Add("fecha");
                        Session["dt_arti"] = dt;
                        fecha = fecha2;
                    }
                    cargar_tareas(idc_cliente, idc_agente, fecha);
                    btnver.Visible = false;
                    btnocultar.Visible = true;
                    repeat.Visible = true;
                }
                else
                {
                    Alert.ShowAlertError("Seleccione fecha", this);
                    return;
                }
            }
        }

        protected void txtbuscararticulo_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtbuscararticulo.Text;
            int idc_cliente = 0;
            idc_cliente = Convert.ToInt32(Session["idc_cliente"]);
            cargar_nuevos_articulos(idc_cliente, filtro);
            aded.Visible = true;
        }

        protected void btnaddarticulo_Click(object sender, EventArgs e)
        {
            int id = 0;
            id = string.IsNullOrEmpty(cbarticulos.SelectedValue) ? 0 : Convert.ToInt32(cbarticulos.SelectedValue);
            if (id == 0)
            {
                Alert.ShowAlertError("Seleccione un valor valido", this);
            }
            else
            {
                DataTable dt = default(DataTable);
                dt = (DataTable)Session["dt_art_nuevos"];
                DataView dv = new DataView();
                dv = dt.DefaultView;
                dv.RowFilter = "idc_articulo=" + id.ToString() + "";
                foreach (DataRow row in dv.ToTable().Rows)
                {
                    AddtoToTable(row["idc_articulo"].ToString(), row["codigo"].ToString(), row["desart"].ToString(), row["unimed"].ToString(),
                        row["tipo"].ToString(), row["sel"].ToString(), row["cantidad"].ToString(), row["meta"].ToString(), row["precio"].ToString(),
                        row["decimales"].ToString(), row["observ"].ToString());
                }
                repeat.Visible = true;
                btnocultar.Visible = true;
                btnver.Visible = false;
                CargarRepeat();
                SumaMeta();
                aded.Visible = false;
                txtbuscararticulo.Text = "";
                foreach (RepeaterItem item in repeat.Items)
                {
                    Label lblid = item.FindControl("lblid") as Label;
                    Label lbltipo = item.FindControl("lbltipo") as Label;
                    Label lblprecio = item.FindControl("lblprecio") as Label;
                    Label lblerror = item.FindControl("lblerror") as Label;
                    TextBox txtcantidad = item.FindControl("txtcantidad") as TextBox;
                    TextBox txtmeta = item.FindControl("txtmeta") as TextBox;
                    TextBox txtobservaciones = item.FindControl("txtobservaciones") as TextBox;
                    Button Button1 = item.FindControl("Button1") as Button;
                    Panel panelcantidad = item.FindControl("panelcantidad") as Panel;
                    decimal cantidad = default(decimal);
                    decimal precio = default(decimal);
                    lblerror.Text = "";
                    lblerror.Visible = false;
                    if (lblid.Text == id.ToString())
                    {
                        Button1.CssClass = "btn btn-success btn-block";
                        panelcantidad.Visible = true;
                    }
                }
                Alert.ShowAlert("Articulo Agregado Correctamente", "Mensaje del Sistema", this);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.CssClass = btn.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
            int id = 0;
            id = Convert.ToInt32(btn.CommandName);
            foreach (RepeaterItem item in repeat.Items)
            {
                Label lblid = item.FindControl("lblid") as Label;
                Panel panelcantidad = item.FindControl("panelcantidad") as Panel;
                TextBox txtcantidad = item.FindControl("txtcantidad") as TextBox;
                TextBox txtmeta = item.FindControl("txtmeta") as TextBox;
                TextBox txtobservaciones = item.FindControl("txtobservaciones") as TextBox;
                if (lblid.Text == id.ToString())
                {
                    panelcantidad.Visible = btn.CssClass == "btn btn-default btn-block" ? false : true;
                    if (btn.CssClass == "btn btn-default btn-block")
                    {
                        UpdatetoToTable(lblid.Text, "0", "0", txtobservaciones.Text);
                    }
                    if (btn.CssClass == "btn btn-info btn-block")
                    {
                        UpdatetoToTable(lblid.Text, txtcantidad.Text, txtmeta.Text, txtobservaciones.Text);
                    }
                    txtmeta.Text = "0.00";
                    txtcantidad.Text = "";
                    txtobservaciones.Text = "";
                }
            }
            SumaMeta();
        }

        public static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        protected void txtcantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            object testVar = null;
            testVar = txt.Text;
            if (IsNumeric(testVar) == false)
            {
                txt.Text = "";
                txt.Focus();
                Alert.ShowAlertError("Ingrese un valor numerico", this);
                txt.Focus();
            }
            else
            {
                foreach (RepeaterItem item in repeat.Items)
                {
                    Label lblid = item.FindControl("lblid") as Label;
                    Label lblprecio = item.FindControl("lblprecio") as Label;
                    Panel panelcantidad = item.FindControl("panelcantidad") as Panel;
                    TextBox txtcantidad = item.FindControl("txtcantidad") as TextBox;
                    TextBox txtmeta = item.FindControl("txtmeta") as TextBox;
                    TextBox txtobservaciones = item.FindControl("txtobservaciones") as TextBox;
                    Button Button1 = item.FindControl("Button1") as Button;
                    decimal cantidad = default(decimal);
                    decimal precio = default(decimal);
                    if (object.ReferenceEquals(txt, txtcantidad))
                    {
                        if (txtcantidad.Text == "" || string.IsNullOrEmpty(txtcantidad.Text))
                        {
                            txtcantidad.Text = "0";
                        }
                        cantidad = Convert.ToDecimal(txtcantidad.Text);
                        precio = Convert.ToDecimal(lblprecio.Text);
                        txtmeta.Text = decimal.Round((precio * cantidad), 2).ToString();
                        UpdatetoToTable(lblid.Text, txtcantidad.Text, txtmeta.Text, txtobservaciones.Text);
                    }
                    SumaMeta();
                }
            }
        }

        private bool ErrorVal()
        {
            bool cadena_r = false;
            cadena_r = false;
            foreach (RepeaterItem item in repeat.Items)
            {
                Label lblid = item.FindControl("lblid") as Label;
                Label lbltipo = item.FindControl("lbltipo") as Label;
                Label lblprecio = item.FindControl("lblprecio") as Label;
                Label lblerror = item.FindControl("lblerror") as Label;
                TextBox txtcantidad = item.FindControl("txtcantidad") as TextBox;
                TextBox txtmeta = item.FindControl("txtmeta") as TextBox;
                TextBox txtobservaciones = item.FindControl("txtobservaciones") as TextBox;
                Button Button1 = item.FindControl("Button1") as Button;
                decimal cantidad = default(decimal);
                decimal precio = default(decimal);
                lblerror.Text = "";
                lblerror.Visible = false;
                object testVar = null;
                testVar = txtcantidad.TemplateControl;
                if (Button1.CssClass == "btn btn-success btn-block")
                {
                    if (string.IsNullOrEmpty(txtcantidad.Text))
                    {
                        lblerror.Text = "Ingrese un valor numerico";
                        lblerror.Visible = true;
                        cadena_r = true;
                    }
                }
            }

            return cadena_r;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((txtmetaglobal.Text == "0" | txtmetaglobal.Text == "0.00" | string.IsNullOrEmpty(txtmetaglobal.Text)) & string.IsNullOrEmpty(txtobservaciones_.Text))
            {
                Alert.ShowAlertError("Si el Monto es 0 DEBE INGRESAR OBSERVACIONES", this);
            }
            else if (ErrorVal() == true)
            {
                Alert.ShowAlertError("Verifique los errores marcados con rojo", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar esta Tarea.?','modal fade modal-info');", true);
            }
        }
        private int TotalCadena()
        {
            int cadena_r = 0;
            cadena_r = 0;
            foreach (RepeaterItem item in repeat.Items)
            {
                Button Button1 = item.FindControl("Button1") as Button;
                if (Button1.CssClass == "btn btn-success btn-block")
                {
                    cadena_r = cadena_r + 1;
                }
            }

            return cadena_r;
        }
        private string Cadena()
        {
            string cadena_r = null;
            cadena_r = "";
            foreach (RepeaterItem item in repeat.Items)
            {
                Label lblid = item.FindControl("lblid") as Label;
                Label lbltipo = item.FindControl("lbltipo") as Label;
                Label lblprecio = item.FindControl("lblprecio") as Label;
                TextBox txtcantidad = item.FindControl("txtcantidad") as TextBox;
                TextBox txtmeta = item.FindControl("txtmeta") as TextBox;
                TextBox txtobservaciones = item.FindControl("txtobservaciones") as TextBox;
                Button Button1 = item.FindControl("Button1") as Button;
                decimal cantidad = default(decimal);
                decimal precio = default(decimal);
                if (txtcantidad.Text == "" | string.IsNullOrEmpty(txtcantidad.Text))
                {
                    txtcantidad.Text = "0";
                }
                cantidad = Convert.ToDecimal(txtcantidad.Text);
                precio = Convert.ToDecimal(lblprecio.Text);
                if (Button1.CssClass == "btn btn-success btn-block" & cantidad > 0)
                {
                    int vtipon = 0;
                    if (lbltipo.Text == "V")
                    {
                        vtipon = 1;
                    }
                    if (lbltipo.Text == "N")
                    {
                        vtipon = 2;
                    }
                    if (lbltipo.Text == "P")
                    {
                        vtipon = 3;
                    }

                    //'vtipon = ICASE(vtipo = "V", 1, vtipo = "N", 2, vtipo = "P", 3)
                    cadena_r = cadena_r + lblid.Text + ";" + vtipon.ToString() + ";" + txtobservaciones.Text + ";" + txtcantidad.Text + ";" + txtmeta.Text + ";";
                }
            }

            return cadena_r;
        }

   
        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                switch (caso)
                {
                    case "Guardar":
                        decimal vmetagloblal = (string.IsNullOrEmpty(txtmetaglobal.Text) ? 0 : Convert.ToDecimal(txtmetaglobal.Text));
                        string vcadena = Cadena();
                        int vtotalcadena = TotalCadena();
                        AgentesENT entidad = new AgentesENT();
                        AgentesCOM com = new AgentesCOM();
                        entidad.Pidc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
                        entidad.Pidc_agente = Convert.ToInt32(Session["idc_agente"]);
                        entidad.pfecha = Convert.ToDateTime(txtfecha.Text);
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        entidad.pmeta_venta = vmetagloblal;
                        entidad.Pcadenaarti = vcadena;
                        entidad.Ptotalcadenaarti = vtotalcadena;
                        entidad.Pobsr = txtobservaciones_.Text.ToUpper();
                        DataSet ds = com.sp_atareas_clientes_nuevo(entidad);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos procesando la tarea.", "Espere un Momento", "ficha_cliente_m.aspx", "imagenes/loading.gif", "2000", "La tarea fue Guardada Correctamente", this);
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