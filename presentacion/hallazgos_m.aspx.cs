using negocio;
using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class hallazgos_m : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                CargaSucursales();
                cargar_usuarios();
            }
        }

        public void CargaSucursales()
        {
            try
            {
                HallazgosENT entidad = new HallazgosENT();
                HallazgosCOM componente = new HallazgosCOM();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = componente.CargarSucursal(entidad);
                ddlsucursal.DataValueField = "idc_sucursal";
                ddlsucursal.DataTextField = "nombre";
                ddlsucursal.DataSource = ds.Tables[0];
                ddlsucursal.DataBind();
                ddlsucursal.Items.Insert(0, new ListItem("--Seleccione una Sucursal", "0")); //updated code}
            }
            catch (Exception ex)
            {
                funciones.EnviarError(ex.ToString());
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        public void cargar_usuarios()
        {
            try
            {
                HallazgosENT entidad = new HallazgosENT();
                HallazgosCOM componente = new HallazgosCOM();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = componente.CargarUsuarios(entidad);
                ddlusu.DataValueField = "idc_usuario";
                ddlusu.DataTextField = "usuario";
                ddlusu.DataSource = ds.Tables[0];
                ddlusu.DataBind();
                ddlusu.Items.Insert(0, new ListItem("--Seleccione un Usuario", "0")); //updated code}
                Session["usuarios_hall"] = ds.Tables[0];
            }
            catch (Exception ex)
            {
                funciones.EnviarError(ex.ToString());
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        public void cargar_vehiculo(string filtro)
        {
            try
            {
                HallazgosENT entidad = new HallazgosENT();
                HallazgosCOM componente = new HallazgosCOM();
                entidad.Pdirecip = filtro;
                DataSet ds = componente.CargaVeh(entidad);
                ddlvehiculo.DataValueField = "idc_vehiculo";
                ddlvehiculo.DataTextField = "descripcion";
                ddlvehiculo.DataSource = ds.Tables[0];
                ddlvehiculo.DataBind();
                if (filtro == "")
                {
                    ddlvehiculo.Items.Insert(0, new ListItem("--Seleccione un Vehiculo", "0")); //updated code}
                }
            }
            catch (Exception ex)
            {
                funciones.EnviarError(ex.ToString());
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void ddlusu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = ddlusu.SelectedValue;
            if (id == "0")
            {
                Alert.ShowAlertError("Seleccione un usuario", this.Page);
            }
            else
            {
                DataTable dt = Session["usuarios_hall"] as DataTable;
                DataView view = dt.DefaultView;
                view.RowFilter = "idc_usuario = " + id.Trim() + "";
                DataRow row = view.ToTable().Rows[0];
                int vehiculo = Convert.ToInt32(row["requiere_veh"]);
                vehi.Visible = false;
                if (vehiculo == 1)
                {
                    vehi.Visible = true;
                    cargar_vehiculo("");
                }
            }
        }

        protected void lnkbuscar_Click(object sender, EventArgs e)
        {
            cargar_vehiculo(txtbiscar.Text);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                Yes.Visible = false;
                if (!fuparchivos.HasFile)
                {
                    Alert.ShowAlertError("Debe Anexar una Imagen", this.Page);
                }
                else if (getValidatedExtension(Path.GetExtension(fuparchivos.FileName).ToLower()) == false)
                {
                    Alert.ShowAlertError("Extension de archivo no valido", this.Page);
                }
                else if (ddlsucursal.SelectedValue == "0")
                {
                    Alert.ShowAlertError("Seleccione una Sucursal", this.Page);
                }
                else if (ddlusu.SelectedValue == "0")
                {
                    Alert.ShowAlertError("Seleccione un Usuario", this.Page);
                }
                else if (txtdetalles.Text == "")
                {
                    Alert.ShowAlertError("Ingrese una Descripción del Hallazgo", this.Page);
                }
                else
                {
                    string extension = Path.GetExtension(fuparchivos.FileName).ToLower().Trim();

                    HallazgosENT entidad = new HallazgosENT();
                    HallazgosCOM componente = new HallazgosCOM();
                    entidad.Idc_usuario_sol = Convert.ToInt32(Session["sidc_usuario"]);
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_usuario = Convert.ToInt32(ddlusu.SelectedValue);
                    entidad.pidc_sucursal = Convert.ToInt32(ddlsucursal.SelectedValue);
                    entidad.Pidc_vehiculo = vehi.Visible == true ? Convert.ToInt32(ddlvehiculo.SelectedValue) : 0;
                    entidad.Phallazgo = txtdetalles.Text.ToUpper();
                    DataSet ds = componente.AgregarHallazgo(entidad);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (vmensaje == "")
                    {
                        string path = ds.Tables[0].Rows[0]["ruta"].ToString() + extension;
                        funciones.UploadFile(fuparchivos, path, this);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessagecamb", "AlertGO('El Hallazgo fue Guardado de Manera Correcta','hallazgos_m.aspx');", true);
                        string mess = EnviarCorreo(path);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje, this.Page);
                    }
                }

                Yes.Visible = true;
            }
            catch (Exception ex)
            {
                Yes.Visible = true;
                funciones.EnviarError(ex.ToString());
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        private string EnviarCorreo(string path)
        {
            string usuario = "";
            string contraseña = "";
            string body = "";
            string hostnamesmtp = "";
            int portsmtp = 0;
            bool useSsl = false;
            string subject = "NUEVO HALLAZGO GENERAL";
            string to = funciones.cuentas_correo("select dbo.fn_hallazgos_correos_avisos(142) as correos").Replace(";", ",");
            List<string> listadeadjuntos = new List<string>();
            DataTable dt = funciones.ExecQuery("SELECT top 1 LTRIM(RTRIM(CORREO)) correo,DBO.fn_desencripta(correos_gama.CONTRASEÑA) contraseña,correo_puerto.PUERTO,CORREO_PUERTO.ssl,LTRIM(RTRIM(CORREO_PUERTO.smtp)) smtp,LTRIM(RTRIM(CORREO_PUERTO.pop)) pop FROM correos_gama WITH (NOLOCK) INNER JOIN	correo_puerto WITH (NOLOCK) ON CORREOS_GAMA.idc_correopuerto = correo_puerto.idc_correopuerto inner join usuarios with(nolock) on usuarios.idc_correo = correos_gama.idc_correo where idc_usuario = " + Convert.ToInt32(Session["sidc_usuario"]).ToString());
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                usuario = row["correo"].ToString();
                contraseña = row["contraseña"].ToString();
                hostnamesmtp = row["smtp"].ToString();
                portsmtp = Convert.ToInt32(row["puerto"]);
                useSsl = Convert.ToBoolean(row["ssl"]);
                if (to != "" && usuario != "" && contraseña != "" && hostnamesmtp != "" && portsmtp != 0)
                {
                    body = "<strong>HALLAZGO: </strong>" + txtdetalles.Text + "<br/><br/>" + "<strong>SUCURSAL: </strong>" + Convert.ToString(this.ddlsucursal.SelectedValue) + " - " + Convert.ToString(ddlsucursal.SelectedItem.Text).Trim();
                    if (vehi.Visible == true)
                    {
                        body = body + "<br/><br/><strong>UNIDAD: </strong>" + ddlvehiculo.SelectedItem.Text;
                    }

                    if (File.Exists(path))
                    {
                        listadeadjuntos.Add(path);
                        body = body + @"<br/><br/><img id='img1' src='" + path + "'/><br/><br/>";
                    }
                    return funciones.EnviarCorreo(usuario, contraseña, hostnamesmtp, portsmtp, useSsl, subject, body, to, listadeadjuntos, true);
                }
                else
                {
                    return "Faltan Datos";
                }
            }
            else
            {
                return "Faltan Datos";
            }
        }

        public bool getValidatedExtension(string tempExt)
        {
            if (!(tempExt.Equals(".jpg")) && !(tempExt.Equals(".png")) && !(tempExt.Equals(".bmp")) &&
                !(tempExt.Equals(".3gp")) && !(tempExt.Equals(".wmv")))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void blksucursal_Click(object sender, EventArgs e)
        {
            string id = ddlsucursal.SelectedValue;
            if (id == "0")
            {
                Alert.ShowAlertError("Seleccione una Sucursal", this.Page);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "DE", "window.open('consulta_hallazgos_pendientes_m.aspx?s=" + funciones.deTextoa64(id) + "');", true);
            }
        }
    }
}