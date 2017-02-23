using negocio;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Web;
using System.Web.UI;

namespace presentacion
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            lblfooter.Text = version;
            lbklimpiarsession.Visible = Session["sidc_usuario"] != null;
            if (Request.QueryString["u"] != null && Request.QueryString["c"] != null)
            {
                Login(funciones.de64aTexto(Request.QueryString["u"]), funciones.de64aTexto(Request.QueryString["c"]), false);
            }
           
        }
        void DeleteCookie(string cookie_name)
        {
            if (Request.Cookies[cookie_name] != null)
            {
                Response.Cookies[cookie_name].Expires = DateTime.Now.AddDays(-1);
            }
        }

        [System.Web.Services.WebMethod]
        public static string return_name()
        {
            String mess = "Today is " + DateTime.Now.ToString();
            return mess;
        }

        protected void btnaceptar_Click(object sender, EventArgs e)
        {
            string user, pass;
            bool remember = false;
            user = txtuser.Text.Trim().ToUpper();
            pass = txtpass.Text.Trim().ToUpper();
            Login(user, pass, remember);
        }

      
        /// <summary>
        /// Crea una cookie con expiracion de 7 dias
        /// </summary>
        /// <param name="data"></param>
        private void setCookie(string cookie_name, string value)
        {
            Response.Cookies[cookie_name].Value = value;
            Response.Cookies[cookie_name].Expires = DateTime.Now.AddDays(7);
        }

        /// <summary>
        /// Devuelve el valor actual de una cookie, si no existe la crea con expiracion de 7 dias
        /// </summary>
        /// <returns></returns>
        public string getCookie(string cookie_name)
        {
            string userSettings = Request.Cookies[cookie_name].Value;
            return userSettings;
        }

        bool ExistCokie(string cookie_name)
        {
            return Request.Cookies[cookie_name] != null;
        }
        private void Login(string user, string pass, bool remember)
        {
            UsuariosE EntUsuario = new UsuariosE();
            UsuariosBL CompUsuario = new UsuariosBL();
            DataSet ds = new DataSet();
            int idc_usuario;
            //recuperamos los inputs
            //creamos la entidad Usuario y la llenamos.
            EntUsuario.Usuario = user;
            EntUsuario.Contraseña = pass;

            //control try catch
            try
            {
                ds = CompUsuario.validar_usuarios(EntUsuario);  //funcion que valida la existencia del usuario
                //recuperamos el id del usuario
                idc_usuario = Convert.ToInt32(ds.Tables[0].Rows[0]["idc_usuario"]);
                //validamos que no sea cero el id
                if (Session["sidc_usuario"] != null && Convert.ToInt32(Session["sidc_usuario"]) != idc_usuario)//si hay session
                {
                    Alert.ShowAlertInfo("Tiene una Sesión iniciada por el usuario: " + (string)Session["nombre"] + ". Debe Cerrar esta Sesion para continuar.", "Mensaje del Sistema", this);
                }
                else
                {
                    if (idc_usuario > 0)
                    {
                        string nombre_usuario = "";
                        string puesto_perfil = "";
                        //recuperamos datos
                        nombre_usuario = (ds.Tables[0].Rows[0]["nombre"].ToString());
                        puesto_perfil = (ds.Tables[0].Rows[0]["puesto"].ToString());
                        //pasamos aminusculas
                        nombre_usuario = nombre_usuario.ToLower();
                        puesto_perfil = puesto_perfil.ToLower();
                        //pasamos a estilos title
                        String nombre_user = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(nombre_usuario);
                        String puesto_user = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(puesto_perfil);
                       
                        //Char delimiter = ' ';
                        //String[] substrings = nombre_user.Split(delimiter);
                        //nombre_user = "";
                        //int i = 0;
                        //foreach (var substring in substrings)
                        //{
                        //    if (i < substrings.Length - 1)
                        //    {
                        //        nombre_user = nombre_user + substring + delimiter;
                        //        i++;

                        //    }
                        //}
                        //subimos a sesion
                        Session["nombre"] = nombre_user;
                        Session["puesto_login"] = puesto_user;
                        Session["sidc_empleado"] = (ds.Tables[0].Rows[0]["idc_empleado"].ToString());
                        //creamos la sesion y lo redireccionamos al menu de opciones.
                        Session["sidc_usuario"] = idc_usuario;
                        Session["idc_usuario"] = idc_usuario;
                        Session["login_idc_perfil"] = ds.Tables[0].Rows[0]["idc_puestoperfil"].ToString();
                        Session["login_perfil"] = ds.Tables[0].Rows[0]["perfil"].ToString();
                        Session["susuario"] = user;
                        Session["xusuario"] = user;
                        Session["idc_sucursal"]= ds.Tables[0].Rows[0]["idc_sucursal"].ToString();
                        Session["Xiva"] = ds.Tables[0].Rows[0]["IVA"];
                        Session["Xidc_iva"] = ds.Tables[0].Rows[0]["IDC_iva"];
                        Session["xidc_almacen"] = ds.Tables[0].Rows[0]["IDC_ALMACEN"];
                        ////idc_puesto
                        string idc_puesto = ds.Tables[0].Rows[0]["idc_puesto"].ToString();
                        Session.Add("sidc_puesto_login", idc_puesto);
                        if (remember == true)
                        {
                            setCokie("User_Data", "username", user);
                            setCokie("Pass_Data", "password", pass);
                        }
                        Session["timed_value"] = DateTime.Now;
                        Session["primer_carga"] = true;
                        //si traeTIM el parametro de la pagina logeamos en esa pagina
                        string us = funciones.deTextoa64(txtuser.Text);
                        string con = funciones.deTextoa64(txtpass.Text);
                        string response = "menu.aspx";
                        if (ValidarPantalla(65) == true && RevisionesBasicasPendientes() > 0)
                        {
                            response = "revision_basica_de_herramientas_pendientes_m.aspx";
                        }
                        response = Request.QueryString["p"] == null ? response : funciones.de64aTexto(Request.QueryString["p"]);
                        setCookie("username", user);
                        setCookie("name", nombre_user);
                        Response.Redirect(response);
                    }
                    else
                    {
                        Alert.ShowAlertError("Acceso Denegado...Verifique el Usuario y la Contraseña", this);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        private int HallazgosPendientes()
        {
            HallazgosENT ENTIDAD = new HallazgosENT();
            ENTIDAD.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            HallazgosCOM componente = new HallazgosCOM();
            DataSet ds = componente.CargarHallazgosSinTerminar(ENTIDAD);
            return ds.Tables[0].Rows.Count;
        }

        private bool ValidarPantalla(int idc_pantalla)
        {
            HallazgosENT ENTIDAD = new HallazgosENT();
            ENTIDAD.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            ENTIDAD.pidc_hallazgo = idc_pantalla;
            HallazgosCOM componente = new HallazgosCOM();
            DataSet ds = componente.ValidarPantalla(ENTIDAD);
            return Convert.ToBoolean(ds.Tables[0].Rows[0]["resultado"]);
        }

        private int RevisionesBasicasPendientes()
        {
            Vehiculos_RevisionENT entidad = new Vehiculos_RevisionENT();
            Vehiculos_RevisionCOM com = new Vehiculos_RevisionCOM();
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            DataSet ds = com.CargarInfoPendienteCatalogo(entidad);
            return ds.Tables[0].Rows.Count;
        }

        /// <summary>
        /// Comprueba que exista una Cookie
        /// </summary>
        public bool Cookie_Exist(string CookieName)
        {
            bool status = false;
            if (Request.Cookies[CookieName] != null)
            {
                status = true;
            }
            return status;
        }

        /// <summary>
        /// Crea una cookie con expiracion de 7 dias
        /// </summary>
        private void setCokie(string CookieName, string CookieVar, string CookieVal)
        {
            HttpCookie myCookie = new HttpCookie(CookieName);
            myCookie[CookieVar] = CookieVal;
            myCookie.Expires = DateTime.Now.AddDays(7d);
            Response.Cookies.Add(myCookie);
        }

        /// <summary>
        /// Devuelve el valor actual de una cookie, si no existe la crea
        /// </summary>
        public string getCokie(string CookieName, string CookieVar)
        {
            string userSettings = "";
            userSettings = Request.Cookies[CookieName][CookieVar];
            return userSettings;
        }

        /// <summary>
        /// Expira el valor de una cookie
        /// </summary>
        private void delCokie(string CookieName)
        {
            if (Request.Cookies[CookieName] != null)
            {
                HttpCookie myCookie = new HttpCookie(CookieName);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
        }

        protected void lnkVerContraseña_Click(object sender, EventArgs e)
        {
            //txtpass.TextMode = lnkVerContraseña.Text == "Mostrar" ? TextBoxMode.SingleLine : TextBoxMode.Password;
            //lnkVerContraseña.Text = lnkVerContraseña.Text == "Mostrar" ? "Ocultar" : "Mostrar";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        protected void lnlotracuenta_Click(object sender, EventArgs e)
        {
            DeleteCookie("username");
            DeleteCookie("name");
            Response.Redirect("login.aspx");
        }

        protected void lbklimpiarsession_Click(object sender, EventArgs e)
        {
            Session["sidc_usuario"] = null;
            Response.Redirect("login.aspx");
        }

    }
}