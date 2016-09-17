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
    public partial class hallazgos_incumplidos_m : System.Web.UI.Page
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
            }
        }

        public void CargarHallazgos(int idc_sucursal)
        {
            try
            {
                HallazgosENT entidad = new HallazgosENT();
                HallazgosCOM componente = new HallazgosCOM();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = componente.CargarHallazgosSinTerminar(entidad);
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

        private String ReturnUser(string filtro)
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
            idc_halla.Text = vidc;
            lbltipo.Text = gridhallazgos.DataKeys[index].Values["tipo"].ToString();
            lbltipoh.Text = gridhallazgos.DataKeys[index].Values["tipoh"].ToString();
            correo_capturo.Text = gridhallazgos.DataKeys[index].Values["correo_sol"].ToString().Replace(";", "");
            reviso.Text = gridhallazgos.DataKeys[index].Values["reviso"].ToString().Replace(";", "");
            diverror.Visible = false;
            lblerror.Text = "";
            txtcomentarios.Text = "";
            lblsucursal.Text = gridhallazgos.DataKeys[index].Values["sucursal"].ToString();
            switch (e.CommandName)
            {
                case "Ver":
                    //img.ImageUrl=ruta+vidc
                    string[] allFiles = System.IO.Directory.GetFiles(ruta);//Change path to yours
                    foreach (string file in allFiles)
                    {
                        if (Path.GetFileNameWithoutExtension(file) == vidc)
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/img/"));//path local
                            funciones.CopiarArchivos(ruta + vidc + Path.GetExtension(file), dirInfo + vidc + Path.GetExtension(file), this);
                            img.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["server"] + "/temp/img/" + vidc + Path.GetExtension(file);
                        }
                    }
                    txthallazgo.Text = gridhallazgos.DataKeys[index].Values["observaciones"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessageimg", "ModalConfirmimg('Detalles del Hallazgo','modal fade modal-info');", true);
                    break;

                case "Revisar":
                    txthallazgo_revi.Text = gridhallazgos.DataKeys[index].Values["observaciones"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm();", true);
                    break;
            }
        }

        protected void RevisarHallazgo(object sender, EventArgs e)
        {
            try
            {
                string subject = "";
                string cod = "";
                string correos = "";
                if (lbltipoh.Text == "S")
                {
                    subject = "ACTUALIZACION DE HALLAZGO DE SUCURSAL";
                    correos = funciones.cuentas_correo("select dbo.fn_hallazgos_correos_avisos(143) as correos").Replace(";", ",");
                    if (lbltipo.Text == "1")
                    {
                        cod = "RSC";
                    }

                    if (lbltipo.Text == "2")
                    {
                        cod = "RSCVEH";
                    }

                    if (lbltipo.Text == "3")
                    {
                        cod = "RSCMOD";
                    }
                }
                else if (lbltipoh.Text == "G")
                {
                    subject = "ACTUALIZACION DE HALLAZGO";
                    correos = (correo_capturo.Text == "" ? "" : correo_capturo.Text + ",") + (reviso.Text == "" ? "" : reviso.Text + ",");
                    cod = "HALLAZT";
                }
                else if (lbltipoh.Text == "V")
                {
                    subject = "ACTUALIZACION DE HALLAZGO DE VEHICULO";
                    correos = funciones.cuentas_correo("select dbo.fn_hallazgos_correos_avisos(144) as correos").Replace(";", ",");
                    cod = "HALLVTE";
                }

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
                    entidad.pfecha = Convert.ToDateTime(txtfecha_revi.Text);
                    DataSet ds = new DataSet();
                    string vmensaje = "";
                    string path = funciones.GenerarRuta(cod, "unidad");
                    switch (lbltipoh.Text)
                    {
                        case "G":
                            ds = componente.RevisarHallazgoGIncm(entidad);
                            vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                            break;

                        case "S":
                            entidad.pidc_sucursal = Convert.ToInt16(lbltipo.Text);
                            ds = componente.RevisarHallazgoSIncumplido(entidad);
                            vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                            break;

                        case "V":
                            ds = componente.RevisarHallazgoViNCUMP(entidad);
                            vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                            break;
                    }
                    if (vmensaje == "")
                    {
                        int total = Convert.ToInt32(ds.Tables[0].Rows[0]["incumplidas"]);
                        List<string> Adjuntos = new List<string>();
                        string url = "";
                        string ruta = funciones.GenerarRuta("HALLAZ", "unidad");
                        string[] allFiles2 = System.IO.Directory.GetFiles(ruta);//Change path to yours
                        foreach (string file in allFiles2)
                        {
                            if (Path.GetFileNameWithoutExtension(file) == idc_halla.Text)
                            {
                                url = path + idc_halla.Text.Trim() + Path.GetExtension(file);
                                Adjuntos.Add(url);
                                break;
                            }
                        }
                        bool incumplido = false;
                        if (total > 3) { subject = "(HALLAZGO INCUMPLIDO) " + subject; incumplido = true; }
                        string message = total > 3 ? "SUPERO EL LIMITE DE INTENTO INCUMPLIDOS, EL HALLAZGO TERMINO COMO INCUMPLIDO" : "El Hallazgo fue Actualizado de Manera Correcta";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessagecamb", "AlertGO('" + message + "','hallazgos_incumplidos_m.aspx');", true);

                        string mess = EnviarCorreo(path, subject, correos, incumplido);
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

        private string EnviarCorreo(string path, string subjectt, string correos, bool incumplido)
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
                          "<strong>SUCURSAL: </strong>" + " " + lblsucursal.Text + "<br/><br/>" +
                            "<strong>NUEVA FECHA: </strong>" + " " + Convert.ToDateTime(txtfecha_revi.Text).ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")) + "<br/><br/>" +
                            "<strong>MOTIVO DE CAMBIO DE FECHA: </strong>" + " " + txtcomentarios.Text.Trim() + "</p>";
                    if (path != "" && File.Exists(path))
                    {
                        listadeadjuntos.Add(path);
                        body = body + @"<br/><br/><img id='img1' src='" + path + "'/><br/><br/>";
                    }
                    if (incumplido == true)
                    {
                        body = "<h2><strong>HALLAZGO INCUMPLIDO Y NO TERMINADO </strong></h2><br/><br/>" + body;
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
    }
}