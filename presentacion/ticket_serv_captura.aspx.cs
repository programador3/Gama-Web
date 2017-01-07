using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Collections;


namespace presentacion
{
    public partial class ticket_serv_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                txtguid.Text = Guid.NewGuid().ToByteArray().ToString();
                ViewState[txtguid.Text + "dt_serv"] = null;
                ViewState[txtguid.Text + "dt_serv_perso"] = null;
                CargarCombo(0,"");
            }
            
        }

        void CargarCombo(int idc_puesto, string filtro)
        {
            try
            {

                TicketsCapturaCOM componente = new TicketsCapturaCOM();
                DataSet ds = componente.sp_tareas_servicios_puestos(idc_puesto);
                DataTable dt = ds.Tables[0];
                DataView dv = dt.DefaultView;
                dv.RowFilter = filtro;
                DataTable dtr = dv.ToTable();
                if (dtr.Rows.Count == 0)
                {
                    Alert.ShowAlertInfo("No se encontro ningun servicio", "Mensaje del Sistema", this);
                }
                ddltiposervicios.DataTextField = "desc_corta";
                ddltiposervicios.DataValueField = "idc_tareaser";
                ddltiposervicios.DataSource = dtr;
                ddltiposervicios.DataBind();
                if (filtro == "")
                {
                    ddltiposervicios.Items.Insert(0, new ListItem("--Seleccione un Servicio", "0"));
                }
                ViewState[txtguid.Text + "dt_serv"] = dtr;
                ViewState[txtguid.Text + "dt_serv_perso"] = ds.Tables[1];
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string query = TextBox1.Text == "" ? "" : "desc_corta like '%" + TextBox1.Text + "%'";
            CargarCombo(0, query);
        }

        protected void lnkbuscar_Click(object sender, EventArgs e)
        {
            string query = TextBox1.Text == "" ? "" : "desc_corta like '%" + TextBox1.Text + "%'";
            CargarCombo(0, query);
        }

        protected void lnkinfo_Click(object sender, EventArgs e)
        {
            if (ddltiposervicios.Items.Count > 0)
            {
                int idc_ = Convert.ToInt32(ddltiposervicios.SelectedValue);
                if (idc_ == 0)
                {
                    Alert.ShowAlertInfo("Seleccione un Servicio para ver su información", "Mensaje del Sistema", this);
                }
                else {

                    DataTable dt = ViewState[txtguid.Text + "dt_serv"] as DataTable;
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "idc_tareaser = "+idc_+"";
                    DataTable dtr = dv.ToTable();
                    if (dtr.Rows.Count > 0)
                    {
                        txtdesc.Text = dtr.Rows[0]["descripcion"].ToString();
                        DataTable dtp = ViewState[txtguid.Text + "dt_serv_perso"] as DataTable;
                        DataView dvp = dtp.DefaultView;
                        dvp.RowFilter = "idc_tareaser = " + idc_ + "";
                        grdipersona.DataSource = dvp.ToTable();
                        grdipersona.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                            "Modalinfo();", true);
                    }


                }
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso = (string)Session["Caso_Confirmacion"];        
            try
            {
                string url_archivo = "";
                string extension = "";
                if (fuparchivos.HasFile)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(0, 100000);
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/files/"));//path local         
                    url_archivo = dirInfo + randomNumber.ToString() + fuparchivos.FileName;
                    bool subida_correcta = funciones.UploadFile(fuparchivos, url_archivo, this.Page);
                    extension = Path.GetExtension(url_archivo);
                    if (!subida_correcta)
                    {
                        return;
                    }
                }
                switch (caso)
                {
                    case "Guardar":
                        TicketsCapturaCOM componente = new TicketsCapturaCOM();
                        int idc_tareaser = Convert.ToInt32(ddltiposervicios.SelectedValue);
                        string obser = txtobservaciones.Text;
                        int idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
                        int idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        DataSet ds = componente.sp_aticketserv(idc_puesto,idc_tareaser,obser,idc_usuario, extension);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            txtguid.Text = Guid.NewGuid().ToString();
                            txtobservaciones.Text = "";
                            ViewState[txtguid.Text + "dt_serv"] = null;
                            CargarCombo(0, "");
                            if (url_archivo != "")
                            {
                                string ruta_destino = ds.Tables[0].Rows[0]["ruta_destino"].ToString();
                                funciones.CopiarArchivos(url_archivo,ruta_destino,this.Page);
                            }
                            Alert.ShowAlertInfo("Ticket de Servicio Guardado Correctamente", "Mensaje del Sistema", this);
                        }
                        else {

                            Alert.ShowAlertInfo(vmensaje, "Mensaje del Sistema", this);
                        }
                        break;
                    case "Cancelar":
                        string url = Session["burl"] == null ? "menu.aspx" : Session["burl"] as string;
                        Response.Redirect(url);
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
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar el llenado de este formulario?','modal fade modal-danger');", true);

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idc_ = Convert.ToInt32(ddltiposervicios.SelectedValue);
            if (idc_ == 0)
            {
                Alert.ShowAlertInfo("Seleccione un Servicio", "Mensaje del Sistema", this);
            }
            else if (txtobservaciones.Text=="")
            {
                Alert.ShowAlertInfo("Son Necesarias las Observaciones.", "Mensaje del Sistema", this);
            }
            else if (txtobservaciones.Text.Length > 249)
            {
                Alert.ShowAlertInfo("Para la descripcion del Ticket solo se permiten 250 caracteres", "Mensaje del Sistema", this);
            }
            else {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar este Ticket de Servicio?','modal fade modal-info');", true);

            }
        }
    }
}