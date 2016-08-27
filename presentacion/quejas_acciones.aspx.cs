using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;

namespace presentacion
{
    public partial class quejas_acciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (Request.QueryString["accion"] == "sol")
            {
                lnksoli.Visible = true;
                solucionar.Visible = true;
                lbltitle.Text = "Solución de Quejas";
                if (!IsPostBack)
                {
                    Session["url_file"] = null;
                    txtfecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
                }
            }
            if (Request.QueryString["accion"] == "can")
            {
                lnkcance.Visible = true;
                cancelar.Visible = true;
                lbltitle.Text = "Cancelación de Quejas";
            }
            if (Request.QueryString["accion"] == "add")
            {
                lnkcomentario.Visible = true;
                comentario.Visible = true;
                lbltitle.Text = "Agregar Comentarios";
            }
            if (Request.QueryString["cdi"] != null)
            {
                if (!IsPostBack)
                {
                    Session["url_file"] = null;
                    txtfecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
                    int cdi = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["cdi"]));
                    CargarQuejas(cdi);
                }
            }
            else
            {
                txtnoqueja.ReadOnly = false;
            }
           
        }

        private void CargarQuejas(int cdi)
        {
            QuejasENT entidad = new QuejasENT();
            QuejasCOM componente = new QuejasCOM();
            DataSet ds = componente.CargaQuejas(entidad);
            DataView view = ds.Tables[0].DefaultView;

            string query = "idc_queja = " + cdi + "";
            view.RowFilter = query;
            if (view.ToTable().Rows.Count > 0)
            {
                DataRow row = view.ToTable().Rows[0];
                txtcliente.Text = row["cliente"].ToString();
                txtproblema.Text = row["problema"].ToString();
                txtnoqueja.Text = row["idc_queja"].ToString();
            }
        }

        protected void lnksatisf_Click(object sender, EventArgs e)
        {
            lnksatisf.CssClass = lnksatisf.CssClass == "btn btn-info btn-block" ? "btn btn-default btn-block" : "btn btn-info btn-block";
            bool cheched = lnksatisf.CssClass == "btn btn-info btn-block" ? false : true;
            txtdescripcionsatis.Visible = cheched;
        }

        private string ErrorSolucion(){
            if (txtsolucion.Text == "")
            {
                return "La solucion no puede quedar Vacia.";
            }
            else if (txtdescripcionsatis.Text == "" && lnksatisf.CssClass=="btn btn-default btn-block")
            {
                return "Describe aqui el motivo por el cual el cliente no quedo satisfecho.";
            }
            else if (txtencargado.Text == "")
            {
                return "El encargado no puede quedar vacio.";
            }
            else if (txtfecha.Text == "")
            {
                return "La Fecha no puede quedar vacia.";
            }
            else {
                return "";
            }
        }
        private string ErrorCancelar()
        {
            if (txtobservaciones_can.Text == "")
            {
                return "Es necesario agregar una observacion.";            
            }
            else
            {
                return "";
            }
        }

        private string ErrorComentario()
        {
            if (txtcomentario.Text == "")
            {
                return "Es necesario agregar un comentario.";
            }
            else
            {
                return "";
            }
        }
        protected void lnkcerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("quejas_espera_.aspx");
        }

        protected void lnkcomentario_Click(object sender, EventArgs e)
        {
            string error = ErrorComentario();
            if (error != "")
            {
                Alert.ShowAlertError(error, this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Comentario";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Agregar el Comentario a la queja?','modal fade modal-info');", true);
            }
        }

        protected void lnksoli_Click(object sender, EventArgs e)
        {
            string error = ErrorSolucion();
            if (error != "")
            {
                Alert.ShowAlertError(error,this);
            }
            else {
                if (fupimagen.HasFile)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(0, 1000);
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/"));//path local
                    string val = dirInfo + randomNumber .ToString()+ fupimagen.FileName;
                    funciones.UploadFile(fupimagen, val,this);
                    Session["url_file"] = val;
                }
                Session["Caso_Confirmacion"] = "Solucionar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Solucionar la queja?','modal fade modal-info');", true);
            }
        }

        protected void lnkcance_Click(object sender, EventArgs e)
        {

            string error = ErrorCancelar();
            if (error != "")
            {
                Alert.ShowAlertError(error, this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Cancelar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar la queja?','modal fade modal-info');", true);
            }
        }


        protected void TesClick_Click(object sender, EventArgs e)
        {
            try
            {
                string value = Session["Caso_Confirmacion"] as string;
                QuejasENT entidad = new QuejasENT();
                QuejasCOM componente = new QuejasCOM();
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pidc_queja = Convert.ToInt32(txtnoqueja.Text.Trim());
                string vmensaje = "";
                DataSet ds = new DataSet();
                bool copiar_archivo = false;
                string proceso = "";
                string ruta = "";
                switch (value)
                {
                    case "Cancelar":
                        entidad.Pobservaciones = txtobservaciones_can.Text;
                        ds = componente.CancelarQuejas(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        proceso = "Queja Cancelada Correctamente";
                        break;
                    case "Comentario":
                        entidad.Pobservaciones = txtcomentario.Text;
                        ds = componente.ComentarioQuejas(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        proceso = "Comentario Agregado Correctamente";
                        break;
                    case "Solucionar":
                        entidad.Pfecha = Convert.ToDateTime(txtfecha.Text);
                        entidad.Pencargado = txtencargado.Text;
                        entidad.Psatisfecho = lnksatisf.CssClass == "btn btn-info btn-block" ? true : false;
                        entidad.Pobservaciones_satisfecho = txtdescripcionsatis.Text;
                        entidad.Pobservaciones = txtsolucion.Text;
                        ds = componente.SolucionarQuejas(entidad);

                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        ruta = ds.Tables[0].Rows[0]["ruta"].ToString();
                        proceso = "Queja Solucionada Correctamente";
                        copiar_archivo = true;
                        break;
                }
                if (vmensaje == "")
                {
                    if (copiar_archivo == true)
                    {
                        string origen = Session["url_file"] as string;
                        string destino = ruta + Path.GetExtension(origen);
                        File.Copy(origen,destino,true);
                    }
                    Alert.ShowGiftMessage("Estamos procesando la solicitud.", "Espere un Momento", "quejas_espera_m.aspx", "imagenes/loading.gif", "2000", proceso, this);
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

        protected void txtnoqueja_TextChanged(object sender, EventArgs e)
        {
            if (txtnoqueja.Text != "")
            {
                CargarQuejas(Convert.ToInt32(txtnoqueja.Text));
            }
        }

        protected void lnkagregarcomentario_Click(object sender, EventArgs e)
        {
            Response.Redirect("quejas_acciones.aspx?accion="+Request.QueryString["accion"]);
        }
    }
}