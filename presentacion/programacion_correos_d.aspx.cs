using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class programacion_correos_d : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null || Request.QueryString["idc_progracorreo"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                CargarProg(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_progracorreo"])));
            }
        }

        private void CargarProg(int id)
        {
            try
            {
                ProgramacionCorreosENT entidad = new ProgramacionCorreosENT();
                entidad.Pidc_progracorreo = id;
                ProgramacionCorreosCOM componente = new ProgramacionCorreosCOM();
                DataSet ds = componente.CargaProgn(entidad);
                //copiamos las imagenes a la ruta local
                DataTable tbl_img = ds.Tables[5];
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/img/"));//path local
                foreach (DataRow row in tbl_img.Rows)
                {
                    funciones.CopiarArchivos(row["ruta_origen"].ToString(), dirInfo.ToString() + row["name"].ToString(), this);
                }
                lblasunto.Text = ds.Tables[0].Rows[0]["asunto"].ToString();
                lblfecha.Text = ds.Tables[0].Rows[0]["fecha_solicitud"].ToString();
                lblusuario.Text = ds.Tables[0].Rows[0]["usuario"].ToString();
                Literal1.Text = ds.Tables[0].Rows[0]["mensaje"].ToString();
                lblnommostr.Text = ds.Tables[0].Rows[0]["nombre_mostrar"].ToString();
                lblcorreo.Text = ds.Tables[0].Rows[0]["correo"].ToString();
                repeat_archivos.DataSource = ds.Tables[4];
                repeat_archivos.DataBind();
                archivos.Visible = ds.Tables[4].Rows.Count > 0 ? true : false; repeat_archivos.DataSource = ds.Tables[2];
                repeatcorreos.DataSource = ds.Tables[2];
                repeatcorreos.DataBind();
                lbltitlecorreo.Visible = ds.Tables[3].Rows.Count > 0 ? true : false;
                lbltitlecorreo.Text = ds.Tables[3].Rows.Count > 0 ? ds.Tables[3].Rows[0]["descripcion"].ToString() : "";
                if (ds.Tables[2].Rows.Count > 0)
                {
                    correos.Visible = true;
                }
                else
                {
                    correos.Visible = false;
                }
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["todos"]) == true)
                {
                    correos.Visible = false;
                }
                repeatfechas.DataSource = ds.Tables[1];
                repeatfechas.DataBind();
                repeatfechas.Visible = ds.Tables[1].Rows.Count > 0 ? true : false;
                todos.Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["todos"]);
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void lnk_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            if (!File.Exists(lnk.CommandName))
            {
                Alert.ShowAlertError("No tiene archivo relacionado", this);
            }
            else
            {
                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lnk.CommandArgument.ToString());
                // Escribimos el fichero a enviar
                Response.WriteFile(lnk.CommandName);
                // volcamos el stream
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
                // Response.End();
            }
        }

        protected void lnkrehaza_Click(object sender, EventArgs e)
        {
            if (txtobsr.Text == "")
            {
                Alert.ShowAlertError("Para Rechazar debe colocar Observaciones", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Rechazado";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿ Desea Rechazar el envio del Correo?','modal fade modal-danger');", true);
            }
        }

        protected void lnkautorizar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Autorizado";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿ Desea Autorizar el envio del Correo?','modal fade modal-success');", true);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                ProgramacionCorreosENT entidad = new ProgramacionCorreosENT();
                entidad.Pidc_progracorreo = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_progracorreo"]));
                entidad.Pobservaciones = txtobsr.Text;
                entidad.Ptipo = (string)Session["Caso_Confirmacion"] == "Autorizado" ? "A" : "C";
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                ProgramacionCorreosCOM componente = new ProgramacionCorreosCOM();
                DataSet ds = componente.AutorizarCorreo(entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos procesando la solicitud.", "Espere un Momento", "programacion_correos.aspx", "imagenes/loading.gif", "2000", "El Correo fue" + (string)Session["Caso_Confirmacion"] + " correctamente.", this);
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
    }
}