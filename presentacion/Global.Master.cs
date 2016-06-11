using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class Global : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            hdnidc_usuario.Value = Convert.ToInt32(Session["sidc_usuario"]).ToString();
            String path_actual = Request.Url.Segments[Request.Url.Segments.Length - 1];
            String path_actual_COMPLETO = HttpContext.Current.Request.Url.AbsoluteUri;
            path_actual_COMPLETO = path_actual_COMPLETO.Replace("%20", "+");
            path_actual_COMPLETO = path_actual_COMPLETO.Replace("%20", "+");
            path_actual_COMPLETO = funciones.ChangeValue(path_actual_COMPLETO);
            string PreviousPage = Request.ServerVariables["HTTP_REFERER"];
            int user_id = Convert.ToInt32(Session["sidc_usuario"]);
            string Puesto = (String)(Session["puesto_login"]) == "" ? "Sin puesto Asignado" : (String)(Session["puesto_login"]);
            string Usuario_Name = (String)(Session["nombre"]);
            if (Session["sidc_usuario"] == null && Session["lista"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                ////COMENTADO PARA PODER PROGRAMAR
                ////Validamos que no se la pagina menu para que no genere un bucle
                if (user_id != 314 && user_id != 127)
                {
                    if (!path_actual.Equals("menu.aspx"))
                    {
                        //    si es nula quiere decir que tecleo manualmente la URL
                        if (PreviousPage == null) { Response.Redirect("menu.aspx"); }
                    }
                }
            }
            if (!IsPostBack && Session["lista"] == null)
            {
                List<String> listas_url = new List<String>();
                Session["lista"] = listas_url;
            }

            if (PreviousPage != path_actual_COMPLETO)
            {
                List<String> listas_url = (List<String>)Session["lista"];
                if (listas_url.Count > 0)
                {
                    int index = listas_url.Count;
                    String url = listas_url[index - 1];
                    if (url == null || url == "") { url = "menu.aspx"; }
                    url = url.Replace("%20", "+");
                    if (url == path_actual_COMPLETO)
                    {
                        listas_url.RemoveAt(index - 1);
                        Session["lista"] = listas_url;
                    }
                    else
                    {
                        listas_url.Add(PreviousPage);
                        Session["lista"] = listas_url;
                    }
                }
                else
                {
                    listas_url.Add(PreviousPage);
                    Session["lista"] = listas_url;
                }
            }
            lblUserName.Text = Usuario_Name;
            lblpuestos2.Text = Puesto;
            lbluser2.Text = Usuario_Name;
            CargarHerramientasMenu();
            web_methods.idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            web_methods.idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/errores/"));//path local
            Session["error_path"] = dirInfo.ToString();
        }

        public static void CreateFileError(string content, Page page)
        {
            DateTime localDate = DateTime.Now;
            string date = localDate.ToString();
            date = date.Replace("/", "_");
            date = date.Replace(":", "_");
            content = "Nombre: " + (String)(page.Session["nombre"]) + System.Environment.NewLine + "PC: " + funciones.GetPCName() + System.Environment.NewLine + "Usuario-PC: " + funciones.GetUserName() + System.Environment.NewLine + "IP: " + funciones.GetLocalIPAddress() + System.Environment.NewLine + content;
            funciones.CreateFile((string)page.Session["error_path"] + date + ".gama", content);
        }

        private void CargarHerramientasMenu()
        {
            NotificacionesENT ent = new NotificacionesENT();
            ent.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            NotificacionesCOM com = new NotificacionesCOM();
            repeat_herr.DataSource = com.CargaMenuHerr(ent).Tables[0];
            repeat_herr.DataBind();
        }

        protected void lnkHerramientas_Click(object sender, EventArgs e)
        {
            LinkButton lnkHerramientas = (LinkButton)sender;
            string pagina = lnkHerramientas.CommandName;
            bool nueva_ventana = Convert.ToBoolean(lnkHerramientas.CommandArgument);
            String path_actual2 = Request.Url.Segments[Request.Url.Segments.Length - 1];
            if (path_actual2 == "view_files.aspx")
            {
                Alert.ShowAlertError("Debe cerrar esta ventana o puede perder los cambios realizados.", this.Page);
            }
            else if (path_actual2 == "rendimiento_tareas_detalles.aspx" && Request.QueryString["tipofiltro"] != null)
            {
                Alert.ShowAlertError("Debe cerrar esta pestaña para poder navegar por el sistema.", this.Page);
            }
            else
            {
                if (nueva_ventana == false)
                {
                    Response.Redirect(pagina);
                }
                else
                {
                    String url = HttpContext.Current.Request.Url.AbsoluteUri;
                    String path_actual = url.Substring(url.LastIndexOf("/") + 1);
                    url = url.Replace(path_actual, "");
                    url = url + pagina;
                    ScriptManager.RegisterStartupScript(this, GetType(), "noti533W3", "window.open('" + url + "');", true);
                }
            }
        }

        protected void lnkSalir_Click(object sender, EventArgs e)
        {
            web_methods.idc_usuario = 0;
            web_methods.idc_puesto = 0;
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        protected void lnkBackUrl_Click(object sender, EventArgs e)
        {
            List<String> listas_url = (List<String>)Session["lista"];
            int index = listas_url.Count;
            String url = listas_url[index - 1];
            String path_actual2 = Request.Url.Segments[Request.Url.Segments.Length - 1];
            if (path_actual2 == "view_files.aspx")
            {
                Alert.ShowAlertError("Debe cerrar esta ventana o puede perder los cambios realizados.", this.Page);
            }
            else
            {
                if (url == null || url == "") { url = "menu.aspx"; }
                url = url.Replace("%20", "+");
                String path_actual_COMPLETO = HttpContext.Current.Request.Url.AbsoluteUri;
                string PreviousPage = Request.ServerVariables["HTTP_REFERER"];
                path_actual_COMPLETO = path_actual_COMPLETO.Replace("%20", "+");
                if (url == path_actual_COMPLETO)
                {
                    listas_url.RemoveAt(index - 1);
                    Session["lista"] = listas_url;
                    index = listas_url.Count;
                    url = listas_url[index - 1];
                    if (url == null || url == "") { url = "menu.aspx"; }
                    url = url.Replace("%20", "+");
                    Response.Redirect(url);
                }
                else
                {
                    Response.Redirect(url);
                }
            }
        }
    }
}