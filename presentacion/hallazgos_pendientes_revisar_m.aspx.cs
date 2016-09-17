using negocio;
using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class hallazgos_pendientes_revisar_m : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                txtfecha_revi.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                CargarHallazgos(0);
                CargarUsuarios("");
            }
        }

        public void CargarHallazgos(int idc_sucursal)
        {
            try
            {
                HallazgosENT entidad = new HallazgosENT();
                HallazgosCOM componente = new HallazgosCOM();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = componente.CargarHallazgos(entidad);
                DataView view = new DataView(ds.Tables[0]);
                DataTable distinctValues = view.ToTable(true, "idc_sucursal", "sucursal");
                ddlsucursal.DataValueField = "idc_sucursal";
                ddlsucursal.DataTextField = "sucursal";
                ddlsucursal.DataSource = distinctValues;
                ddlsucursal.DataBind();
                ddlsucursal.Items.Insert(0, new ListItem("--Seleccione una Sucursal", "-1"));
                ddlsucursal.Items.Insert(1, new ListItem("**Ver Todas las Sucursales", "0"));
                if (idc_sucursal == 0)
                {
                    gridhallazgos.DataSource = ds.Tables[0];
                    gridhallazgos.DataBind();
                }
                else
                {
                    DataView viewt = ds.Tables[0].DefaultView;
                    viewt.RowFilter = "idc_sucursal = " + idc_sucursal + "";
                    gridhallazgos.DataSource = viewt.ToTable();
                    gridhallazgos.DataBind();
                }
            }
            catch (Exception ex)
            {
                funciones.EnviarError(ex.ToString());
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        public void CargarUsuarios(string filtro)
        {
            try
            {
                HallazgosENT entidad = new HallazgosENT();
                HallazgosCOM componente = new HallazgosCOM();
                DataSet ds = componente.Usuarios(entidad);
                ddlusuario.DataValueField = "idc_usuario";
                ddlusuario.DataTextField = "usuario_nombre";
                if (filtro == "")
                {
                    ddlusuario.DataSource = ds.Tables[0];
                    ddlusuario.DataBind();
                }
                else
                {
                    DataView viewt = ds.Tables[0].DefaultView;
                    viewt.RowFilter = "usuario_nombre like '%" + filtro + "%'";
                    ddlusuario.DataSource = viewt.ToTable();
                    ddlusuario.DataBind();
                }
            }
            catch (Exception ex)
            {
                funciones.EnviarError(ex.ToString());
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        private String ReturnUser2(string filtro)
        {
            try
            {
                HallazgosENT entidad = new HallazgosENT();
                HallazgosCOM componente = new HallazgosCOM();
                DataSet ds = componente.Usuarios(entidad);
                DataView viewt = ds.Tables[0].DefaultView;
                viewt.RowFilter = "usuario_nombre like '%" + filtro + "%'";
                if (viewt.ToTable().Rows.Count > 0)
                {
                    return viewt.ToTable().Rows[0]["usuario_nombre"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                funciones.EnviarError(ex.ToString());
                Alert.ShowAlertError(ex.ToString(), this.Page);
                return "";
            }
        }

        private String ReturnUser(string filtro)
        {
            try
            {
                HallazgosENT entidad = new HallazgosENT();
                HallazgosCOM componente = new HallazgosCOM();
                DataSet ds = componente.Usuarios(entidad);
                DataView viewt = ds.Tables[0].DefaultView;
                viewt.RowFilter = "idc_usuario = " + filtro + "";
                if (viewt.ToTable().Rows.Count > 0)
                {
                    return viewt.ToTable().Rows[0]["usuario_nombre"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                funciones.EnviarError(ex.ToString());
                Alert.ShowAlertError(ex.ToString(), this.Page);
                return "";
            }
        }

        protected void ddlsucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc = Convert.ToInt32(ddlsucursal.SelectedValue);
            if (idc == -1)
            {
                Alert.ShowAlertError("Seleccione una Sucursal", this);
            }
            else
            {
                CargarHallazgos(idc);
            }
        }

        protected void gridhallazgos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string vidc = gridhallazgos.DataKeys[index].Values["idc"].ToString();
            string ruta = funciones.GenerarRuta("HALLAZ", "unidad");
            string tipoh = gridhallazgos.DataKeys[index].Values["tipoh"].ToString();
            string tipo = gridhallazgos.DataKeys[index].Values["tipo"].ToString();
            idc_principal.Text = gridhallazgos.DataKeys[index].Values["idc_revsuccheck"].ToString();
            lbltipoh.Text = tipoh;
            lbltipo.Text = tipo;
            idc_halla.Text = vidc;
            usuario_sol.Text = gridhallazgos.DataKeys[index].Values["usuario_sol"].ToString().Trim();
            correo_capturo.Text = gridhallazgos.DataKeys[index].Values["correo_capturo"].ToString().Replace(";", "");
            reviso.Text = gridhallazgos.DataKeys[index].Values["reviso"].ToString().Replace(";", "");
            diverror_cam.Visible = false;
            lblerror_camb.Text = "";
            diverror.Visible = false;
            lblerror.Text = "";
            txtcomentarios.Text = "";
            txtsucursal_camb.Text = gridhallazgos.DataKeys[index].Values["sucursal"].ToString();
            switch (e.CommandName)
            {
                case "Ver":
                    //img.ImageUrl=ruta+vidc
                    string[] allFiles = System.IO.Directory.GetFiles(ruta);//Change path to yours
                    foreach (string file in allFiles)
                    {
                        if (Path.GetFileNameWithoutExtension(file) == idc_principal.Text)
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/img/"));//path local
                            funciones.CopiarArchivos(ruta + idc_principal.Text + Path.GetExtension(file), dirInfo + idc_principal.Text + Path.GetExtension(file), this);
                            img.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["server"] + "/temp/img/" + idc_principal.Text + Path.GetExtension(file);
                        }
                    }
                    txthallazgo.Text = gridhallazgos.DataKeys[index].Values["observaciones"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessageimg", "ModalConfirmimg('Detalles del Hallazgo','modal fade modal-info');", true);
                    break;

                case "Cambiar":
                    txthallazgo_camb.Text = gridhallazgos.DataKeys[index].Values["observaciones"].ToString();
                    txtsucursal_camb.Text = gridhallazgos.DataKeys[index].Values["sucursal"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessageimg", "ModalConfirmcamb();", true);
                    break;

                case "Revisar":
                    txthallazgo_revi.Text = gridhallazgos.DataKeys[index].Values["observaciones"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm();", true);
                    break;
            }
        }

        protected void GuardarUsuario(object sender, EventArgs e)
        {
            try
            {
                diverror_cam.Visible = false;
                if (ddlusuario.SelectedValue == "")
                {
                    diverror_cam.Visible = true;
                    lblerror_camb.Text = "Seleccione un Nuevo Usuario.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirmcamb();", true);
                }
                else
                {
                    HallazgosENT entidad = new HallazgosENT();
                    HallazgosCOM componente = new HallazgosCOM();
                    entidad.pidc_hallazgo = Convert.ToInt32(idc_halla.Text);
                    entidad.Idc_usuario_sol = Convert.ToInt32(ddlusuario.SelectedValue);
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    DataSet ds = componente.CambiarUsuarioSolucion(entidad);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (vmensaje == "")
                    {
                        string usuario_realizara = ReturnUser(ddlusuario.SelectedValue.Trim());
                        usuario_sol.Text = usuario_realizara;
                        string query = "select correos_gama.correo from usuarios with(nolock) inner join correos_gama on correos_gama.idc_correo = usuarios.idc_correo where idc_usuario = " + ddlusuario.SelectedValue.Trim() + " ";

                        string cod = "HALLAZ";

                        string ruta = funciones.GenerarRuta(cod, "unidad");
                        string path = "";
                        string[] allFiles = System.IO.Directory.GetFiles(ruta);//Change path to yours
                        foreach (string file in allFiles)
                        {
                            if (Path.GetFileNameWithoutExtension(file) == idc_principal.Text)
                            {
                                path = ruta + idc_principal.Text.Trim() + Path.GetExtension(file);
                                break;
                            }
                        }

                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessagecamb", "AlertGO('El Hallazgo fue Actualizado de Manera Correcta','hallazgos_pendientes_revisar_m.aspx');", true);
                        string mess = EnviarCorreo(path, "CAMBIO DE USUARIO DE SOLUCION EN HALLAZGO", (correo_capturo.Text == "" ? "" : correo_capturo.Text + ",") + (reviso.Text == "" ? "" : reviso.Text + ","));
                        string mess2 = EnviarCorreo(path, "CAMBIO DE USUARIO DE SOLUCION EN HALLAZGO", funciones.cuentas_correo(query) + ",");
                    }
                    else
                    {
                        diverror_cam.Visible = true;
                        lblerror_camb.Text = vmensaje;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirmcamb();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                diverror_cam.Visible = true;
                lblerror_camb.Text = ex.ToString();
                funciones.EnviarError(ex.ToString());
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirmcamb();", true);
            }
        }

        protected void RevisarHallazgo(object sender, EventArgs e)
        {
            try
            {
                diverror.Visible = false;
                if (txtcomentarios.Text == "")
                {
                    diverror.Visible = true;
                    lblerror.Text = "Ingrese Comentarios.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessagecamb", "ModalConfirm();", true);
                }
                else if (txtfecha_revi.Text == "")
                {
                    diverror.Visible = true;
                    lblerror.Text = "Ingrese la Fecha de Revisión.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessagecamb", "ModalConfirm();", true);
                }
                else
                {
                    HallazgosENT entidad = new HallazgosENT();
                    HallazgosCOM componente = new HallazgosCOM();
                    entidad.pidc_hallazgo = Convert.ToInt32(idc_halla.Text);
                    entidad.pfecha = Convert.ToDateTime(txtfecha_revi.Text);
                    entidad.Phallazgo = txtcomentarios.Text;
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    DataSet ds = componente.RevisarHallazgo(entidad);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (vmensaje == "")
                    {
                        string usuario_realizara = ReturnUser2(usuario_sol.Text);
                        usuario_sol.Text = usuario_realizara;
                        string ruta = funciones.GenerarRuta("HALLAZ", "unidad");
                        string path = "";
                        string[] allFiles = System.IO.Directory.GetFiles(ruta);//Change path to yours
                        foreach (string file in allFiles)
                        {
                            if (Path.GetFileNameWithoutExtension(file) == idc_principal.Text)
                            {
                                path = ruta + idc_principal.Text.Trim() + Path.GetExtension(file);
                                break;
                            }
                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessagecamb", "AlertGO('El Hallazgo fue Revisado de Manera Correcta','hallazgos_pendientes_revisar_m.aspx');", true);
                        string mess = EnviarCorreo(path, "REVISION DE HALLAZGO", (correo_capturo.Text == "" ? "" : correo_capturo.Text + ",") + (reviso.Text == "" ? "" : reviso.Text + ","));
                    }
                    else
                    {
                        diverror.Visible = true;
                        lblerror.Text = vmensaje;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessagecamb", "ModalConfirm();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                diverror.Visible = true;
                lblerror.Text = ex.ToString();
                funciones.EnviarError(ex.ToString());
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessagecamb", "ModalConfirm();", true);
            }
        }

        private string EnviarCorreo(string path, string subjectt, string correos)
        {
            string usuario = "";
            string contraseña = "";
            string body = "";
            string hostnamesmtp = "";
            int portsmtp = 0;
            bool useSsl = false;
            string subject = subjectt;
            string to = correos;
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
                    body = "<p style='font-family:arial;'><strong>HALLAZGO " + idc_halla.Text + " : </strong>" + txthallazgo_revi.Text + "<br/><br/>" +
                            "<strong>SUCURSAL: </strong>" + " " + txtsucursal_camb.Text + "<br/><br/>" +
                            "<strong>FECHA DE SOLUCIÓN: </strong>" + " " + Convert.ToDateTime(txtfecha_revi.Text).ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")) + "<br/><br/>" +
                            "<strong>OBSERVACIONES: </strong>" + " " + txtcomentarios.Text.Trim() + "</p>" + "</br><strong>USUARIO SOLUCION: </strong>" + usuario_sol.Text;
                    if (path != "" && File.Exists(path))
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

        protected void buscarusuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarUsuarios(txtbuscarusuario.Text);
        }
    }
}