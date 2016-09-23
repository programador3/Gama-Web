using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class Global : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["date_noti"] == null)
            {
                Session["date_noti"] = DateTime.Now.AddMinutes(-1);
            }
            DateTime datevaluesession = Convert.ToDateTime(Session["date_noti"]);
            if (DateTime.Now > datevaluesession)
            {
                Session["date_noti"] = DateTime.Now.AddMinutes(0);
                ScriptManager.RegisterStartupScript(this, GetType(), "ded", "ValidarNotificaciones();", true);
            }
            Page.MaintainScrollPositionOnPostBack = true;
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }

            string cs = System.Configuration.ConfigurationManager.AppSettings["cs"];
            ScriptManager.RegisterStartupScript(this, GetType(), "dedchangeedddededed", "ChangeCss('" + cs + "');", true);
            tareas_pendi.Visible = cs == "P" ? false : true;
            lnkperfil.CommandName = Convert.ToInt32(Session["login_idc_perfil"]).ToString();
            lnkperfil.Text = (string)Session["login_perfil"];
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
                if (user_id != 314 && user_id != 127 && user_id != 255 && user_id != 210)
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

            lblUserName.Text = (String)(Session["nombre"]);
            lblpuestos2.Text = (String)(Session["puesto_login"]) == "" ? "Sin puesto Asignado" : (String)(Session["puesto_login"]);
            lbluser2.Text = (String)(Session["nombre"]);
            CargarHerramientasMenu();
            web_methods.idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            web_methods.idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/errores/"));//path local
            Session["error_path"] = dirInfo.ToString();
            OpcionesUsadas();          
        }

        public void OpcionesUsadas()
        {
            try
            {
                DataSet ds = new DataSet();
                OpcionesE EntOpcion = new OpcionesE();
                OpcionesBL menuBL = new OpcionesBL();
                EntOpcion.Idc_user = Convert.ToInt32(Session["sidc_usuario"]);
                ds = menuBL.OpcionFavoritaCargar(EntOpcion);
                repeat_favoritos.DataSource = ds.Tables[0];
                repeat_favoritos.DataBind();
                lbltotalfav.Text = ds.Tables[0].Rows.Count.ToString();
                LBLTOTALFAVORI.Text = ds.Tables[0].Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                Global.CreateFileError(ex.ToString(), this.Page);
            }
        }

        private void OpcionesUsadasEliminar(int PID_OPCION)
        {
            try
            {
                DataSet ds = new DataSet();
                OpcionesE EntOpcion = new OpcionesE();
                OpcionesBL menuBL = new OpcionesBL();
                EntOpcion.Idc_user = Convert.ToInt32(Session["sidc_usuario"]);
                EntOpcion.Idc_opcion = PID_OPCION;
                ds = menuBL.EliminarOpcionFavorita(EntOpcion);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje != "")
                {
                    Alert.ShowAlertError(vmensaje, this.Page);
                    Global.CreateFileError(vmensaje, this.Page);
                }
                else
                {
                    String path_actual = Request.Url.Segments[Request.Url.Segments.Length - 1];
                    String queyrstring = Request.Url.Query;

                    ScriptManager.RegisterStartupScript(this, GetType(), "noti5qsqsq33W3", "AlertaOkRedirecciona('Pagina Eliminada de Favoritos','" + path_actual + queyrstring + "');", true);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this.Page);
            }
        }

        private void dinamic_menudrop()
        {
        //    DataSet ds = new DataSet();
        //    OpcionesE EntOpcion = new OpcionesE();
        //    OpcionesBL menuBL = new OpcionesBL();
        //    EntOpcion.Usuario_id = Convert.ToInt32(Session["sidc_usuario"].ToString());
        //    ds = menuBL.MenuDinmaico(EntOpcion);
        //    Session["menudrop"] = ds.Tables[0];
        //    DataView view = new DataView(ds.Tables[0]);
        //    DataTable distinctValues = view.ToTable(true, "menu1");
        //    repeatmenu1.DataSource = Distinct(distinctValues);
        //    repeatmenu1.DataBind();
        }

        public int contador = 1;

        private DataTable Distinct(DataTable ds)
        {
            ds.Columns.Add("idc_opcion");
            foreach (DataRow row in ds.Rows)
            {
                row["idc_opcion"] = contador;
                contador++;
            }
            return ds;
        }

        private DataTable TableMenu(string query)
        {
            DataTable dt = new DataTable();
            DataTable ds = Session["menudrop"] as DataTable;
            DataView view = ds.DefaultView;
            view.RowFilter = query;
            dt = view.ToTable();
            return dt;
        }

        protected void repeat_menu1(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string menu1 = DataBinder.Eval(dbr, "menu1").ToString();
            DataTable link = TableMenu("menu1 = '" + menu1 + "' and menu2=''");
            DataTable menu2 = TableMenu("menu1 = '" + menu1 + "' and menu2 <> ''");
            Repeater replink = (Repeater)e.Item.FindControl("repeatmenu1d");
            Repeater Repearepeatmenu2 = (Repeater)e.Item.FindControl("Repearepeatmenu2");
            replink.DataSource = link;
            replink.DataBind();
            DataView view = new DataView(menu2);
            DataTable distinctValues = view.ToTable(true, "menu2");
            Repearepeatmenu2.DataSource = Distinct(distinctValues);
            Repearepeatmenu2.DataBind();
        }

        protected void repeat_menu2(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string menu1 = DataBinder.Eval(dbr, "menu2").ToString();
            DataTable link = TableMenu("menu2 = '" + menu1 + "' and menu3=''");
            DataTable menu2 = TableMenu("menu2 = '" + menu1 + "' and menu3 <> ''");
            Repeater replink = (Repeater)e.Item.FindControl("repeatmenu2d");
            Repeater Repearepeatmenu3 = (Repeater)e.Item.FindControl("Repearepeatmenu3");
            replink.DataSource = link;
            replink.DataBind();
            if (menu2.Rows.Count > 0)
            {
                DataView view = new DataView(menu2);
                DataTable distinctValues = view.ToTable(true, "menu3");
                Repearepeatmenu3.DataSource = Distinct(distinctValues);
                Repearepeatmenu3.DataBind();
                var rm4 = (HtmlGenericControl)e.Item.FindControl("rm3");
                rm4.Visible = true;
            }
        }

        protected void repeat_menu3(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string menu1 = DataBinder.Eval(dbr, "menu3").ToString();
            DataTable link = TableMenu("menu3 = '" + menu1 + "' and menu4=''");
            DataTable menu2 = TableMenu("menu3 = '" + menu1 + "' and not menu4 = ''");
            Repeater replink = (Repeater)e.Item.FindControl("repeatmenu3d");
            Repeater Repearepeatmenu3 = (Repeater)e.Item.FindControl("Repearepeatmenu4");
            replink.DataSource = link;
            replink.DataBind();
            if (menu2.Rows.Count > 0)
            {
                DataView view = new DataView(menu2);
                DataTable distinctValues = view.ToTable(true, "menu4");
                Repearepeatmenu3.DataSource = Distinct(distinctValues);
                Repearepeatmenu3.DataBind();
                var rm4 = (HtmlGenericControl)e.Item.FindControl("rm4");
                rm4.Visible = true;
            }
        }

        protected void repeat_menu4(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string menu1 = DataBinder.Eval(dbr, "menu4").ToString();
            DataTable link = TableMenu("menu4 = '" + menu1 + "' and menu5=''");
            DataTable menu2 = TableMenu("menu4 = '" + menu1 + "' and menu5 <> ''");
            Repeater replink = (Repeater)e.Item.FindControl("repeatmenu4d");
            Repeater Repearepeatmenu3 = (Repeater)e.Item.FindControl("Repearepeatmenu5");
            replink.DataSource = link;
            replink.DataBind();
            if (menu2.Rows.Count > 0)
            {
                DataView view = new DataView(menu2);
                DataTable distinctValues = view.ToTable(true, "menu5");
                Repearepeatmenu3.DataSource = Distinct(distinctValues);
                Repearepeatmenu3.DataBind();
                var rm4 = (HtmlGenericControl)e.Item.FindControl("rm5");
                rm4.Visible = true;
            }
        }

        protected void repeat_menu5(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string menu1 = DataBinder.Eval(dbr, "menu5").ToString();
            DataTable link = TableMenu("menu5 = '" + menu1 + "' and menu6=''");
            DataTable menu2 = TableMenu("menu5 = '" + menu1 + "' and menu6 <> ''");
            Repeater replink = (Repeater)e.Item.FindControl("repeatmenu5d");
            Repeater Repearepeatmenu3 = (Repeater)e.Item.FindControl("Repearepeatmenu6");
            replink.DataSource = link;
            replink.DataBind();
            if (menu2.Rows.Count > 0)
            {
                DataView view = new DataView(menu2);
                DataTable distinctValues = view.ToTable(true, "menu6");
                Repearepeatmenu3.DataSource = Distinct(distinctValues);
                Repearepeatmenu3.DataBind();
                var rm4 = (HtmlGenericControl)e.Item.FindControl("rm6");
                rm4.Visible = true;
            }
        }

        protected void repeat_menu6(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string menu1 = DataBinder.Eval(dbr, "menu6").ToString();
            DataTable link = TableMenu("menu6 = '" + menu1 + "' and menu7=''");
            DataTable menu2 = TableMenu("menu6 = '" + menu1 + "' and menu7 <> ''");
            Repeater replink = (Repeater)e.Item.FindControl("repeatmenu6d");
            Repeater Repearepeatmenu3 = (Repeater)e.Item.FindControl("Repearepeatmenu7");
            replink.DataSource = link;
            replink.DataBind();
            var rm4 = (HtmlGenericControl)e.Item.FindControl("rm7");
            if (menu2.Rows.Count > 0)
            {
                DataView view = new DataView(menu2);
                DataTable distinctValues = view.ToTable(true, "menu7");
                Repearepeatmenu3.DataSource = Distinct(distinctValues);
                Repearepeatmenu3.DataBind();
                rm4.Visible = true;
            }
        }

        protected void repeat_menu7(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string menu1 = DataBinder.Eval(dbr, "menu6").ToString();
            DataTable link = TableMenu("menu7 = '" + menu1 + "'");
            Repeater replink = (Repeater)e.Item.FindControl("repeatmenu7d");
            replink.DataSource = link;
        }

        public static void CreateFileError(string content, Page page)
        {
            DateTime localDate = DateTime.Now;
            string date = localDate.ToString();
            date = date.Replace("/", "_");
            date = date.Replace(":", "_");
            content = (String)(page.Session["nombre"]) + System.Environment.NewLine + "PC: " + funciones.GetPCName() + System.Environment.NewLine + "Usuario-PC: " + funciones.GetUserName() + System.Environment.NewLine + "IP: " + funciones.GetLocalIPAddress() + System.Environment.NewLine + content;
            funciones.CreateFile((string)page.Session["error_path"] + date + ".gama", content);

            string cs = System.Configuration.ConfigurationManager.AppSettings["cs"];
            if (cs == "P")
            {
                funciones.EnviarError(content);
            }
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
            else if (path_actual2 == "consulta_hallazgos_pendientes_m.aspx" && Request.QueryString["s"] != null)
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int idc_opcion = Convert.ToInt32(lnk.CommandName);
            OpcionesUsadasEliminar(idc_opcion);
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
            if (path_actual2 == "view_files.aspx" || path_actual2 == "tareas_informacion_adicional.aspx")
            {
                Alert.ShowAlertError("Debe cerrar esta ventana o puede perder los cambios realizados.", this.Page);
            }
            else if (path_actual2 == "consulta_hallazgos_pendientes_m.aspx" && Request.QueryString["s"] != null)
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

        protected void lnkperfil_Click(object sender, EventArgs e)
        {
            if (lnkperfil.CommandName == "0")
            {
                Alert.ShowAlertInfo("No cuentas con un perfil.", "Mensaje del Sistema", this.Page);
            }
            else
            {
                Response.Redirect("perfiles_detalle.aspx?vp=1&perfiles=true&borrador=0&uidc_puestoperfil=" + lnkperfil.CommandName + "&idc_puesto=" + funciones.deTextoa64(Convert.ToInt32(Session["sidc_puesto_login"]).ToString()));
            }
        }
    }
}