﻿using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)
            {
                Response.Redirect("login.aspx");
            }
            lnkmenuventas.Visible = TieneOpcionesdeVentas();
            lnkverpromo.Visible = false;
            if (funciones.permiso(Convert.ToInt32(Session["sidc_usuario"]), 1163))
            {
                UsuariosBL componente = new UsuariosBL();
                DataSet ds = componente.sp_datos_promocion_arti_terminar2(Convert.ToInt32(Session["sidc_usuario"]));
                lnkverpromo.Visible = ds.Tables[0].Rows.Count > 0;
            }
            // dinamic_menudrop();
            Session["redirect_pagedet"] = "menu.aspx";
            if (Request.QueryString["value"] == null)
            {
                if (!Page.IsPostBack && Session["menu1"] == null)
                {
                    int idusuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                    string menu = "";
                    menuPrincipal(menu, idusuario, 5, "0");
                }
                else
                {
                    sessionCreada();
                    string menu = Request.QueryString["menu"];
                    string nivel = Request.QueryString["nivel"];
                    int idusuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                    menuPrincipal(menu, idusuario, 5, nivel);
                }
            }
            if (Request.QueryString["value"] != null && !Page.IsPostBack)
            {
                string search = funciones.de64aTexto(Request.QueryString["value"]);
                panel_menus_repeat.Visible = search == "" ? true : false;
                panel_search.Visible = search == "" ? false : true;
                dinamic_menu(search);
            }
            int idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
            Session["Previus"] = HttpContext.Current.Request.Url.AbsoluteUri;
            if (idc_puesto > 0)
            {
                CargaTareas();
                CargaTareasAsignadas();
                CargaTareasDeMisEmpleados();
                lblpendientes.Text = MisTareas();
                lblasignadas.Text = MisTareasAsignadas();
                //CargarOpcionesRapida();
            }
        }
        //private void CargarOpcionesRapida()
        //{
        //    try
        //    {
        //        OpcionesE entidadad = new OpcionesE();
        //        OpcionesBL compad = new OpcionesBL();
        //        entidadad.Usuario_id = Convert.ToInt32(Convert.ToInt32(Session["sidc_usuario"]));
        //        DataSet ds = compad.AcessosDirectos(entidadad);
        //        repeat_accesos.DataSource = ds.Tables[0];
        //        repeat_accesos.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        Alert.ShowAlertError(ex.ToString(), this);
        //    }
        //}


        private bool TieneOpcionesdeVentas()
        {
            try
            {
                OpcionesBL opciones = new OpcionesBL();
                DataSet ds = opciones.sp_menu_opciones_tipos(1,Convert.ToInt32(Session["sidc_usuario"]));
                return ds.Tables[0].Rows.Count > 0;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(),this);
                return false;
            }
        }
        /// <summary>
        /// Obtiene la tareas que me asignaron
        /// </summary>
        private void CargaTareas()
        {
            TareasENT entidad = new TareasENT();
            TareasCOM componente = new TareasCOM();
            entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
            DataSet ds = componente.CargarPendientesHoy(entidad);
            repeat_tareas.DataSource = ds.Tables[0];
            repeat_tareas.DataBind();
            lbltotaltt.Text = "(" + ds.Tables[0].Rows.Count.ToString() + ")";
            if (ds.Tables[0].Rows.Count == 0)
            {
                notareas.Visible = true;
            }
        }
        /// <summary>
        /// Obtiene tareas de mis subordinados
        /// </summary>
        private void CargaTareasDeMisEmpleados()
        {
            TareasENT entidad = new TareasENT();
            TareasCOM componente = new TareasCOM();
            entidad.Pidc_puesto_asigna = Convert.ToInt32(Session["sidc_puesto_login"]);
            entidad.Pmisempleados = true;
            entidad.Pcorrecto = true;
            entidad.Parchivo = false;
            DataSet ds = componente.CargarPendientesHoy(entidad);
            lnktareas_mis_empleados.Text = "<i class='fa fa-random' aria-hidden='true'></i> Ver Tareas de Mis Subordinados (" + ds.Tables[0].Rows.Count.ToString() + ") ";
            div_tareas_mis_empleados.Visible = ds.Tables[0].Rows.Count > 0;
        }
        /// <summary>
        /// Obtiene las tareas que yo asigne
        /// </summary>
        private void CargaTareasAsignadas()
        {
            TareasENT entidad = new TareasENT();
            TareasCOM componente = new TareasCOM();
            entidad.Pidc_puesto_asigna = Convert.ToInt32(Session["sidc_puesto_login"]);
            DataSet ds = componente.CargarPendientesHoy(entidad);
            repeatasignadas.DataSource = ds.Tables[0];
            repeatasignadas.DataBind();
            lblasi.Text = "(" + ds.Tables[0].Rows.Count.ToString() + ")";
            if (ds.Tables[0].Rows.Count == 0)
            {
                tareasasig.Visible = true;
            }
        }

        private void OpcionesUsadas(int PID_OPCION)
        {
            try
            {
                DataSet ds = new DataSet();
                OpcionesE EntOpcion = new OpcionesE();
                OpcionesBL menuBL = new OpcionesBL();
                EntOpcion.Idc_user = Convert.ToInt32(Session["sidc_usuario"]);
                EntOpcion.Idc_opcion = PID_OPCION;
                ds = menuBL.OpcionFavorita(EntOpcion);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje != "")
                {
                    Alert.ShowAlertError(vmensaje, this);
                    Global.CreateFileError(vmensaje, this.Page);
                }
                else
                {
                    String path_actual = Request.Url.Segments[Request.Url.Segments.Length - 1];
                    String queyrstring = Request.Url.Query;
                    ScriptManager.RegisterStartupScript(this, GetType(), "noti5qsqsq33W3", "AlertaOkRedirecciona('Pagina Agregada a Favoritos','" + path_actual + queyrstring + "');", true);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
                Global.CreateFileError(ex.ToString(), this.Page);
            }
        }

        protected void menuPrincipal(string menu, int usuario_id, int tipo_apli, string nivel)
        {
            try
            {
                DataSet ds = new DataSet();
                OpcionesE EntOpcion = new OpcionesE();
                OpcionesBL menuBL = new OpcionesBL();
                string niv;

                niv = nivel;

                //vemos que nivel estamos
                switch (niv)
                //switch (nivel)
                {
                    case "1":
                        ocmenu1.Value = menu;

                        link1.PostBackUrl = "menu.aspx?menu=" + menu + "&nivel=1";
                        Session["menu1"] = menu;
                        Session["menu1_link"] = "menu.aspx?menu=" + menu + "&nivel=1";
                        break;

                    case "2":
                        ocmenu2.Value = menu;
                        link2.PostBackUrl = "menu.aspx?menu=" + menu + "&nivel=2";
                        Session["menu2"] = menu;
                        Session["menu2_link"] = "menu.aspx?menu=" + menu + "&nivel=2";
                        break;

                    case "3":
                        ocmenu3.Value = menu;
                        link3.PostBackUrl = "menu.aspx?menu=" + menu + "&nivel=3";
                        Session["menu3"] = menu;
                        Session["menu3_link"] = "menu.aspx?menu=" + menu + "&nivel=3";
                        break;

                    case "4":
                        ocmenu4.Value = menu;
                        link4.PostBackUrl = "menu.aspx?menu=" + menu + "&nivel=4";
                        Session["menu4"] = menu;
                        Session["menu4_link"] = "menu.aspx?menu=" + menu + "&nivel=4";
                        break;

                    case "5":
                        ocmenu5.Value = menu;
                        link5.PostBackUrl = "menu.aspx?menu=" + menu + "&nivel=5";
                        Session["menu5"] = menu;
                        Session["menu5_link"] = "menu.aspx?menu=" + menu + "&nivel=5";
                        break;

                    case "6":
                        ocmenu6.Value = menu;
                        link6.PostBackUrl = "menu.aspx?menu=" + menu + "&nivel=6";
                        Session["menu6"] = menu;
                        Session["menu6_link"] = "menu.aspx?menu=" + menu + "&nivel=6";
                        break;
                }
                cleanBarMenu(niv);

                EntOpcion.Menu1 = ocmenu1.Value;

                if (ocmenu2.Value != "")
                {
                    EntOpcion.Menu2 = ocmenu2.Value;
                }
                if (ocmenu3.Value != "")
                {
                    EntOpcion.Menu3 = ocmenu3.Value;
                }
                if (ocmenu4.Value != "")
                {
                    EntOpcion.Menu4 = ocmenu4.Value;
                }
                if (ocmenu5.Value != "")
                {
                    EntOpcion.Menu5 = ocmenu5.Value;
                }
                if (ocmenu6.Value != "")
                {
                    EntOpcion.Menu6 = ocmenu6.Value;
                }
                EntOpcion.Nivel = Convert.ToInt32(niv);
                EntOpcion.Usuario_id = usuario_id;
                EntOpcion.Tipo_apli = tipo_apli;
                ds = menuBL.opciones_menu(EntOpcion);

                Repeater3.DataSource = ds.Tables[0];
                Repeater3.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Session["Error_Mensaje"] = ex.ToString();
            }
        }

        protected void cleanBarMenu(string nivel)
        {
            switch (nivel)
            {
                case "1":
                    ocmenu2.Value = "";
                    ocmenu3.Value = "";
                    ocmenu4.Value = "";
                    ocmenu5.Value = "";
                    ocmenu6.Value = "";

                    //sesiones
                    Session["menu2"] = "";
                    Session["menu2_link"] = "";
                    Session["menu3"] = "";
                    Session["menu3_link"] = "";
                    Session["menu4"] = "";
                    Session["menu4_link"] = "";
                    Session["menu5"] = "";
                    Session["menu5_link"] = "";
                    Session["menu6"] = "";
                    Session["menu6_link"] = "";
                    break;

                case "2":
                    ocmenu3.Value = "";
                    ocmenu4.Value = "";
                    ocmenu5.Value = "";
                    ocmenu6.Value = "";
                    //sesiones
                    Session["menu3"] = "";
                    Session["menu3_link"] = "";
                    Session["menu4"] = "";
                    Session["menu4_link"] = "";
                    Session["menu5"] = "";
                    Session["menu5_link"] = "";
                    Session["menu6"] = "";
                    Session["menu6_link"] = "";
                    break;

                case "3":
                    ocmenu4.Value = "";
                    ocmenu5.Value = "";
                    ocmenu6.Value = "";
                    //sessiones
                    Session["menu4"] = "";
                    Session["menu4_link"] = "";
                    Session["menu5"] = "";
                    Session["menu5_link"] = "";
                    Session["menu6"] = "";
                    Session["menu6_link"] = "";
                    break;

                case "4":
                    ocmenu5.Value = "";
                    ocmenu6.Value = "";
                    //sessiones
                    Session["menu5"] = "";
                    Session["menu5_link"] = "";
                    Session["menu6"] = "";
                    Session["menu6_link"] = "";
                    break;

                case "5":
                    ocmenu6.Value = "";
                    //sessiones
                    Session["menu6"] = "";
                    Session["menu6_link"] = "";
                    break;

                case "6":
                    break;

                default:
                    ocmenu1.Value = "";
                    ocmenu2.Value = "";
                    ocmenu3.Value = "";
                    ocmenu4.Value = "";
                    ocmenu5.Value = "";
                    ocmenu6.Value = "";
                    //sesiones
                    Session["menu1"] = "";
                    Session["menu1_link"] = "";
                    Session["menu2"] = "";
                    Session["menu2_link"] = "";
                    Session["menu3"] = "";
                    Session["menu3_link"] = "";
                    Session["menu4"] = "";
                    Session["menu4_link"] = "";
                    Session["menu5"] = "";
                    Session["menu5_link"] = "";
                    Session["menu6"] = "";
                    Session["menu6_link"] = "";
                    break;
            }
        }

        protected void sessionCreada()
        {
            if (Session["menu1"] != null)
            {
                ocmenu1.Value = Session["menu1"].ToString();
            }
            if (Session["menu2"] != null)
            {
                ocmenu2.Value = Session["menu2"].ToString();
            }
            if (Session["menu3"] != null)
            {
                ocmenu3.Value = Session["menu3"].ToString();
            }
            if (Session["menu4"] != null)
            {
                ocmenu4.Value = Session["menu4"].ToString();
            }
            if (Session["menu5"] != null)
            {
                ocmenu5.Value = Session["menu5"].ToString();
            }
            if (Session["menu6"] != null)
            {
                ocmenu6.Value = Session["menu6"].ToString();
            }
        }

        /// <summary>
        /// Regresa tabla con notificaciones
        /// </summary>
        /// <param name="idc_puesto"></param>
        /// <returns></returns>
        public DataTable Notificaciones(int idc_puesto)
        {
            NotificacionesENT ent = new NotificacionesENT();
            ent.Idc_puesto = idc_puesto;
            NotificacionesCOM com = new NotificacionesCOM();
            return com.CargaNotificaciones(ent).Tables[0];
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtsearch.Text;
            panel_search.Visible = search == "" ? false : true;
            panel_menus_repeat.Visible = search == "" ? true : false;
            dinamic_menu(search);
        }

        private void dinamic_menu(string search)
        {
            try
            {
                DataSet ds = new DataSet();
                OpcionesE EntOpcion = new OpcionesE();
                OpcionesBL menuBL = new OpcionesBL();
                EntOpcion.Usuario_id = Convert.ToInt32(Session["sidc_usuario"].ToString());
                EntOpcion.Search = search;
                ds = menuBL.MenuDinmaico(EntOpcion);
                Repeater2.DataSource = ds.Tables[0];
                Repeater2.DataBind();
                txtsearch.Text = "";
                NoResultados.Visible = ds.Tables[0].Rows.Count == 0 ? true : false;
                Encontramos.Visible = true;
                lblenc.Text = ds.Tables[0].Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                int idc_opcion = Convert.ToInt32(lnk.CommandName);
                OpcionesUsadas(idc_opcion);

            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private String MisTareas()
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
                entidad.Pcorrecto = true;
                entidad.Parchivo = Request.QueryString["lectura"] != null ? true : false;
                DataSet ds = componente.CargarPendientesHoy(entidad);
                DataTable dt = ds.Tables[0];
                return "Ver Todas Mis Tareas(" + dt.Rows.Count.ToString() + ")";
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
                return "";
            }
        }

        private String MisTareasAsignadas()
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pidc_puesto_asigna = Convert.ToInt32(Session["sidc_puesto_login"]);
                entidad.Pcorrecto = true;
                entidad.Parchivo = false;
                DataSet ds = componente.CargarPendientesHoy(entidad);
                DataTable dt = ds.Tables[0];
                return "Ver Todas Mis Tareas(" + dt.Rows.Count.ToString() + ")";
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
                return "";
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            cardmias.Visible = cardmias.Visible == true ? false : true;
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            cardasignadas.Visible = cardasignadas.Visible == true ? false : true;
        }

        protected void lnkverpromo_Click(object sender, EventArgs e)
        {

        }

        protected void lnkmenuventas_Click(object sender, EventArgs e)
        {

        }
    }
}