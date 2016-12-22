using negocio;
using negocio.Componentes;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class oc_clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["ruta_img_oc"] = null;
                Session["tb_cl"] = null;
                ddlcliente.Items.Insert(0, new ListItem("--Seleccione un Cliente", "0"));
                cbxenviar.Checked = true;
                cbxenviar.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 210);
                txtxantidad.Text = "1";
            }
            enviar.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 210);
        }

        private void BuscarCliente(string filtro)
        {
            HallazgosENT entidad = new HallazgosENT();
            entidad.Phallazgo = filtro;
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            HallazgosCOM compo = new HallazgosCOM();
            DataSet ds = compo.CargarClientes(entidad);
            foreach (DataRow ROW in ds.Tables[0].Rows)
            {
                ROW["nombre"] = ROW["nombre"].ToString().Trim() + " | " + ROW["rfccliente"].ToString().Trim() + " | " + ROW["cveadi"].ToString().Trim();
            }
            ddlcliente.DataValueField = "idc_cliente";
            ddlcliente.DataTextField = "nombre";
            ddlcliente.DataSource = ds.Tables[0];
            ddlcliente.DataBind();
            if (filtro == "")
            {
                ddlcliente.Items.Insert(0, new ListItem("--Seleccione un Cliente", "0"));
            }
            else
            {
                int idc = ds.Tables[0].Rows.Count > 0 ? Convert.ToInt32(ddlcliente.SelectedValue) : 0;
                if (idc > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataView view = dt.DefaultView;
                    view.RowFilter = "idc_cliente = " + idc + "";
                    DataTable dt2 = view.ToTable();
                    if (dt2.Rows.Count > 0)
                    {
                        DataRow row = dt2.Rows[0];
                        txtcliente.Text = "";
                        txtfrc.Text = row["rfccliente"].ToString();
                        txtcve.Text = row["cveadi"].ToString();
                    }
                }
            }
            Session["tb_cl"] = ds.Tables[0];
        }

        protected void lnksubir_Click(object sender, EventArgs e)
        {
            string extension = fuparchivo.HasFile == true ? Path.GetExtension(fuparchivo.FileName) : "";
            if (!fuparchivo.HasFile)
            {
                Alert.ShowAlertError("Debe seleccionar un  archivo", this);
            }
            else if (extension != ".bmp" && extension != ".gif" &&
               extension != ".jpg" && extension != ".dib" &&
               extension != ".jpeg")
            {
                Alert.ShowAlertError("Formato de archivo invalido. Solo se permiten los formato de IMAGEN: .bmp, .gif, .jpg, .dib", this);
            }
            else
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/tareas/"));//path local
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                string new_filw = dirInfo + randomNumber.ToString() + fuparchivo.FileName;
                Session["ruta_img_oc"] = new_filw;
                bool pape = funciones.UploadFile(fuparchivo, new_filw, this.Page);
                if (pape == true)
                {
                    string server = System.Configuration.ConfigurationManager.AppSettings["server"];
                    img.ImageUrl = server + "/temp/tareas/" + randomNumber.ToString() + fuparchivo.FileName;
                    img.Attributes["onclick"] = "window.open('" + img.ImageUrl.ToString().Trim() + "');";
                    Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "2000", "Comentario Guardardo Correctamente", this);
                }
                else
                {
                    Session["ruta_img_oc"] = null;
                }
            }
        }

        protected void lnkbuscar_Click(object sender, EventArgs e)
        {
            BuscarCliente(txtcliente.Text);
        }

        protected void ddlcliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc = Convert.ToInt32(ddlcliente.SelectedValue);
            if (idc == 0)
            {
                Alert.ShowAlertError("Debe seleccionar un cliente", this);
            }
            else
            {
                DataTable dt = Session["tb_cl"] as DataTable;
                DataView view = dt.DefaultView;
                view.RowFilter = "idc_cliente = " + idc + "";
                DataTable dt2 = view.ToTable();
                if (dt2.Rows.Count > 0)
                {
                    DataRow row = dt2.Rows[0];
                    txtcliente.Text = "";
                    txtfrc.Text = row["rfccliente"].ToString();
                    txtcve.Text = row["cveadi"].ToString();
                }
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                switch (caso)
                {
                    case "Guardar":
                        HallazgosENT entidad = new HallazgosENT();
                        HallazgosCOM compo = new HallazgosCOM();
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        entidad.pidc_hallazgo = Convert.ToInt32(ddlcliente.SelectedValue);
                        entidad.Phallazgo = txtox.Text;
                        entidad.pidc_sucursal = Convert.ToInt16(txtxantidad.Text);
                        entidad.penvia = cbxenviar.Checked;
                        DataSet ds = compo.AddOCClientes(entidad);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            bool correct = true;
                            string ruta_origen = Session["ruta_img_oc"] as string;
                            string ruta_det = ds.Tables[0].Rows[0]["RUTA_DESTINO"].ToString() + Path.GetExtension(ruta_origen);
                            correct = funciones.CopiarArchivos(ruta_origen, ruta_det, this.Page);
                            if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo ", this); }
                            Alert.ShowGiftMessage("Estamos procesando los archivo(s) al Servidor.", "Espere un Momento", "menu.aspx", "imagenes/loading.gif", "2000", "La OC fue Guardada Correctamente", this);
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                HallazgosENT entidad = new HallazgosENT();
                HallazgosCOM compo = new HallazgosCOM();
                DataSet ds = compo.MaximoOXClientes(entidad);
                int maximo = Convert.ToInt32(ds.Tables[0].Rows[0]["maximo"]);
                if (ddlcliente.SelectedValue == "0")
                {
                    Alert.ShowAlertError("Debe seleccionar un cliente", this);
                }
                else if (txtox.Text == "")
                {
                    Alert.ShowAlertError("Debe escribir el orden de compra", this);
                }
                else if (txtxantidad.Text == "")
                {
                    Alert.ShowAlertError("Debe ingresar una cantidad", this);
                }
                else if (Convert.ToInt32(txtxantidad.Text) > maximo)
                {
                    Alert.ShowAlertError("La Cantidad Maxima es " + maximo.ToString(), this);
                }
                else if (Session["ruta_img_oc"] == null)
                {
                    Alert.ShowAlertError("Ingrese una Imagen", this);
                }
                else if (!Directory.Exists(funciones.GenerarRuta("OCCLI", "UNIDAD")))
                {
                    Alert.ShowAlertError("la carpeta destino no se encuentra disponible, reportelo al departamento de sistemas".ToUpper(), this);
                }
                else
                {
                    Session["Caso_Confirmacion"] = "Guardar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar esta OC?','modal fade modal-info');", true);
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
            Response.Redirect("menu.aspx");
        }

        protected void txtxantidad_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = funciones.ExecQuery("select dbo.fn_maximo_oc_clientes() as maximo");
            int maximo = Convert.ToInt32(funciones.ExecQuery("select dbo.fn_maximo_oc_clientes() as maximo").Rows[0][0]);
            int numero_marcado = txtxantidad.Text == "" ? 0 : Convert.ToInt32(txtxantidad.Text.Trim());
            if (numero_marcado < 1 || numero_marcado > maximo)
            {
                txtxantidad.Text = "1";
                Alert.ShowAlertError("LA CANTIDAD DEBE ESTAR ENTRE 1 Y "+maximo.ToString(),this);
            }
        }
    }
}