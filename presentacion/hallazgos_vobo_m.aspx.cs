﻿using negocio;
using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class hallazgos_vobo_m : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
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
                DataSet ds = componente.CargarHallazgosVBNO(entidad);
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
            string tipoh = gridhallazgos.DataKeys[index].Values["tipoh"].ToString();
            string tipo = gridhallazgos.DataKeys[index].Values["tipo"].ToString();
            string ruta = funciones.GenerarRuta("HALLAZ", "unidad");
            idc_principal.Text = gridhallazgos.DataKeys[index].Values["idc_revsuccheck"].ToString();
            correo_capturo.Text = gridhallazgos.DataKeys[index].Values["correo_termino"].ToString().Replace(";", "");
            fecha.Text = gridhallazgos.DataKeys[index].Values["fecha_string"].ToString();
            observaciones.Text = gridhallazgos.DataKeys[index].Values["observaciones"].ToString();
            idc_halla.Text = vidc;
            lbltipoh.Text = tipoh;
            lbltipo.Text = tipo;
            diverror.Visible = false;
            lblerror.Text = "";
            txtcomentarios.Text = "";
            string cod = "";

            if (lbltipoh.Text == "S")
            {
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
                cod = "HALLAZT";
            }
            else if (lbltipoh.Text == "V")
            {
                cod = "HALLVTE";
            }
            string path = funciones.GenerarRuta(cod, "unidad");
            switch (e.CommandName)
            {
                case "Ver":

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
                    string[] allFiles2 = System.IO.Directory.GetFiles(path);//Change path to yours
                    foreach (string file in allFiles2)
                    {
                        if (Path.GetFileNameWithoutExtension(file) == idc_principal.Text)
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/img/"));//path local
                            funciones.CopiarArchivos(path + idc_principal.Text + Path.GetExtension(file), dirInfo + idc_principal.Text + "_2" + Path.GetExtension(file), this);
                            img2.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["server"] + "/temp/img/" + idc_principal.Text + "_2" + Path.GetExtension(file);
                        }
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessageimg", "ModalConfirmimg('Detalles del Hallazgo','modal fade modal-info');", true);
                    break;

                case "Revisar":
                    txtsolucion.Text = gridhallazgos.DataKeys[index].Values["obs_cumplida"].ToString();
                    txtsucursal.Text = gridhallazgos.DataKeys[index].Values["sucursal"].ToString();
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
                    subject = "VISTO BUENO DE HALLAZGO DE SUCURSAL";
                    correos = (correo_capturo.Text == "" ? "" : correo_capturo.Text + ",");
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
                    subject = "VISTO BUENO DE HALLAZGO";
                    correos = (correo_capturo.Text == "" ? "" : correo_capturo.Text + ",");
                    cod = "HALLAZT";
                }
                else if (lbltipoh.Text == "V")
                {
                    subject = "VISTO BUENO DE HALLAZGO DE VEHICULO";
                    correos = (correo_capturo.Text == "" ? "" : correo_capturo.Text + ",");
                    cod = "HALLVTE";
                }

                diverror.Visible = false;
                if (txtcomentarios.Text == "")
                {
                    diverror.Visible = true;
                    lblerror.Text = "Ingrese Comentarios.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessagecamb", "ModalConfirm();", true);
                }
                else
                {
                    HallazgosENT entidad = new HallazgosENT();
                    HallazgosCOM componente = new HallazgosCOM();
                    entidad.pidc_hallazgo = Convert.ToInt32(idc_halla.Text);
                    entidad.Phallazgo = txtcomentarios.Text;
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    DataSet ds = new DataSet();
                    string vmensaje = "";
                    string path = funciones.GenerarRuta(cod, "unidad");
                    switch (lbltipoh.Text)
                    {
                        case "G":
                            ds = componente.RevisarHallazgoGvb(entidad);
                            vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                            break;

                        case "S":
                            entidad.pidc_sucursal = Convert.ToInt16(lbltipo.Text);
                            ds = componente.RevisarHallazgoSvbno(entidad);
                            vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                            break;

                        case "V":
                            ds = componente.RevisarHallazgoVbno(entidad);
                            vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                            break;
                    }

                    if (vmensaje == "")
                    {
                        List<string> Adjuntos = new List<string>();
                        string url = "";
                        string[] allFiles = System.IO.Directory.GetFiles(path);//Change path to yours
                        foreach (string file in allFiles)
                        {
                            if (Path.GetFileNameWithoutExtension(file) == idc_principal.Text)
                            {
                                url = path + idc_principal.Text.Trim() + Path.GetExtension(file);
                                Adjuntos.Add(url);
                                break;
                            }
                        }
                        string ruta = funciones.GenerarRuta("HALLAZ", "unidad");
                        string[] allFiles2 = System.IO.Directory.GetFiles(ruta);//Change path to yours
                        foreach (string file in allFiles2)
                        {
                            if (Path.GetFileNameWithoutExtension(file) == idc_principal.Text)
                            {
                                url = ruta + idc_principal.Text.Trim() + Path.GetExtension(file);
                                Adjuntos.Add(url);
                                break;
                            }
                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessagecamb", "AlertGO('El Hallazgo fue Terminado de Manera Correcta','hallazgos_vobo_m.aspx');", true);
                        string mess = EnviarCorreo(Adjuntos, subject, correos);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessagecamb", "ModalConfirm();", true);
                funciones.EnviarError(ex.ToString());
            }
        }

        private string EnviarCorreo(List<string> path, string subjectt, string correos)
        {
            string usuario = "";
            string contraseña = "";
            string body = "";
            string hostnamesmtp = "";
            int portsmtp = 0;
            bool useSsl = false;
            string subject = subjectt;
            string to = correos;
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
                    if (lbltipoh.Text == "S")
                    {
                        body = "<p style='font-family:arial;'><strong>HALLAZGO DE SUCURSAL: " + idc_halla.Text + " : </strong>" + txthallazgo_revi.Text + "<br/><br/>" +
                       "<strong>Sucursal: </strong>" + lblsucursal.Text + "<br/><br/>" +
                           "<strong>FECHA DE SOLUCIÓN: </strong>" + " " + fecha + "<br/><br/>" +
                           "<strong>OBSERVACIONES DE SOLUCIÓN: </strong>" + " " + observaciones + "</p>" +
                           "<strong>OBSERVACIONES DE VISTO BUENO: </strong>" + " " + txtcomentarios.Text.Trim() + "</p><br/><br/><b>LA ACTIVIDAD TIENE EL Vo.Bo.</b>";
                    }
                    else if (lbltipoh.Text == "G")
                    {
                        body = "<p style='font-family:arial;'><strong>HALLAZGO: " + idc_halla.Text + " : </strong>" + txthallazgo_revi.Text + "<br/><br/>" +
                         "<strong>Sucursal: </strong>" + lblsucursal.Text + " <br/><br/>" +
                             "<strong>FECHA DE SOLUCIÓN: </strong>" + " " + fecha + "<br/><br/>" +
                           "<strong>OBSERVACIONES DE SOLUCIÓN: </strong>" + " " + observaciones + "</p>" +
                           "<strong>OBSERVACIONES DE VISTO BUENO: </strong>" + " " + txtcomentarios.Text.Trim() + "</p><br/><br/><b>LA ACTIVIDAD TIENE EL Vo.Bo.</b>";
                    }
                    else if (lbltipoh.Text == "V")
                    {
                        body = "<p style='font-family:arial;'><strong>HALLAZGO DE VEHICULO: " + idc_halla.Text + " : </strong>" + txthallazgo_revi.Text + "<br/><br/>" +
                      "<strong>VEHICULO: </strong>" + LBLVEH.Text + " <br/><br/>" +
                          "<strong>FECHA DE SOLUCIÓN: </strong>" + " " + fecha + "<br/><br/>" +
                           "<strong>OBSERVACIONES DE SOLUCIÓN: </strong>" + " " + observaciones + "</p>" +
                           "<strong>OBSERVACIONES DE VISTO BUENO: </strong>" + " " + txtcomentarios.Text.Trim() + "</p><br/><br/><b>LA ACTIVIDAD TIENE EL Vo.Bo.</b>";
                    }
                    int i = 1;
                    foreach (string item in path)
                    {
                        body = body + @"<br/><br/><img id='img" + i.ToString() + "' src='" + item + "'/><br/><br/>";
                        i++;
                    }

                    return funciones.EnviarCorreo(usuario, contraseña, hostnamesmtp, portsmtp, useSsl, subject, body, to, path, true);
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