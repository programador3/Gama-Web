using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class perfiles : System.Web.UI.Page
    {
        public static int id_borradorinexistente = 0;
        public static int index_grid = 0;
        public static int id_produccion_grid = 0;
        public static int id_borrador_grid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }

            //<--MAURICIO
            int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
            if (!IsPostBack)
            {
                //FIN
                try
                {
                    CargaGrid();
                }
                catch (Exception ex)
                {
                    msgbox.show("eere" + ex.Message, this.Page);
                }
            }
            ////valida si tiene permiso de ver esta pagina//
            if (Request.QueryString["sborrador"] != null)// si el request viene vacio iniciamos en borrador
            {
                int sborrador = 0;
                sborrador = Convert.ToInt32(Request.QueryString["sborrador"]);
                if (sborrador == 0)
                {
                    lblmensaje.Text = " De Producción";
                    lblmensaje.CssClass = "btn btn-success";
                    cbxTipo.Checked = true;
                }
                else
                {
                    lblmensaje.Text = " De Borrador";
                    lblmensaje.CssClass = "btn btn-primary";
                    cbxTipo.Checked = false;
                }
            }
            //TIENE PERMISO PARA PRODUCCION O NO 18-09-2015
            ////valida si tiene permiso de ver esta pagina//
            int sborradores = Request.QueryString["sborrador"] == null ? 1 : Convert.ToInt32(Request.QueryString["sborrador"]);
            if (funciones.autorizacion(idc_usuario, 317) == false && sborradores == 0)
            {
                //si entro quiere decir que no tiene permisos de produccion y por ende se activa el borrador
                lnknuevo.Visible = false;
            }
        }

        /// <summary>
        /// Elimina una fila del grid
        /// </summary>
        /// <param name="idc_puestoperfil"></param>
        /// <param name="borrador"></param>
        protected void deletePerfil(int idc_puestoperfil, Boolean borrador)
        {
            //Entidad
            PerfilesE entidad = new PerfilesE();
            entidad.Idc_perfil = idc_puestoperfil;
            entidad.Borrador = borrador;
            entidad.Usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());

            entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
            entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
            entidad.Pusuariopc = funciones.GetUserName();//usuario pc
            //componente
            PerfilesBL componente = new PerfilesBL();
            DataSet ds = new DataSet();
            try
            {
                ds = componente.eliminarPerfil(entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    ///todo bien
                    CargaGrid();
                    Alert.ShowGiftMessage("Estamos procesando la Solicitud", "Espere un Momento", "perfiles.aspx", "imagenes/loading.gif", "2000", "Perfil Eliminado Correctamente", this);
                }
                else
                {
                    //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                    Alert.ShowAlertError(vmensaje, this.Page);
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
            }
        }

        /// <summary>
        /// Elimina un perfil
        /// </summary>
        public void DeletePerfil()
        {
            //verificamos estado de switch
            if (cbxTipo.Checked == true)//produccion
            {
                deletePerfil(id_produccion_grid, false);//llamamos funcion
            }
            if (cbxTipo.Checked == false)//borrador
            {
                deletePerfil(id_borrador_grid, true);
            }
            id_borrador_grid = 0;
            id_produccion_grid = 0;
        }

        /// <summary>
        /// Autoriza un borrador, requiere id de borrador, si no existe lo crea
        /// </summary>
        public void AutorizarBorrador()
        {
            int idUsuario = (int)Session["sidc_usuario"];
            PerfilesE Entidad = new PerfilesE();
            Entidad.Idc_puestoperfil_borr = id_borrador_grid;
            Entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
            Entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
            Entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
            Entidad.Pusuariopc = funciones.GetUserName();//usuario pc
            PerfilesBL Componente = new PerfilesBL();
            DataTable resultado = (Componente.SolicitarAutorizacion(Entidad)).Tables[0];
            DataRow mensaje_resultado = resultado.Rows[0];
            string mensaje = mensaje_resultado["mensaje"].ToString();
            if (mensaje.Equals("existe"))
            {
                Alert.ShowAlert("Usted ya tiene un Borrador de perfil pendiente", "Mensaje del sistema", this);
            }
            else
            {
                if (mensaje.Equals(string.Empty))
                {
                    Alert.ShowGiftMessage("Estamos procesando la Solicitud", "Espere un Momento", "perfiles.aspx", "imagenes/loading.gif", "2000", "La autorización fue solicitada correctamente", this);
                }
                else
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
            id_borrador_grid = 0;
            id_produccion_grid = 0;
        }

        /// <summary>
        /// Carga gridperfiles
        /// </summary>
        public void CargaGrid()
        {
            int idUsuario = (int)Session["sidc_usuario"];
            PerfilesE entidad = new PerfilesE();
            entidad.Usuario = idUsuario;
            PerfilesBL componente = new PerfilesBL();
            DataSet ds = new DataSet();
            ds = componente.perfiles(entidad);
            gridperfiles.DataSource = ds.Tables[0];
            gridperfiles.DataBind();
        }

        /// <summary>
        /// Desbloquea un borrador
        /// </summary>
        /// <param name="id"></param>
        private void regresaBorrador()
        {
            try
            {
                PerfilesE entidad = new PerfilesE();
                entidad.Idc_puestoperfil_borr = id_borrador_grid;
                entidad.Usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = new DataSet();
                PerfilesBL componente = new PerfilesBL();
                ds = componente.perfiles_pendientes_acciones(entidad, "regresar");
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    //todo bien
                    CargaGrid();

                    Alert.ShowGiftMessage("Estamos procesando la Solicitud", "Espere un Momento", "perfiles.aspx", "imagenes/loading.gif", "2000", "Listo borrador desbloqueado", this);
                }
                else
                {
                    //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                    Alert.ShowAlertError(vmensaje, this.Page);
                    return;
                }
                id_borrador_grid = 0;
            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message, this.Page);
            }
        }

        /// <summary>
        /// Comprueba si existe un borrador, si no existe crea uno.
        /// </summary>
        /// <param name="idPerfil"></param>
        public void TablasBorrador(int idPerfil)
        {
            //Bajo id de usuario de session
            int idUsuario = (int)Session["sidc_usuario"];
            //declaro entidad
            PerfilesE EntidadB = new PerfilesE();
            EntidadB.Idc_perfil = idPerfil;
            EntidadB.Usuario = idUsuario;
            EntidadB.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
            EntidadB.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
            EntidadB.Pnombrepc = funciones.GetPCName();//nombre pc usuario
            EntidadB.Pusuariopc = funciones.GetUserName();//usuario pc
            //declaro componente
            PerfilesBL Componente = new PerfilesBL();
            //cargo dataset desde store
            DataSet data = Componente.VerificaPerfilBorrador(EntidadB);
            //bajo tabla de mensajes de errores
            DataRow rowr = data.Tables[0].Rows[0];
            string mensaje = rowr["mensaje"].ToString();
            string vmensaje = data.Tables[0].Rows[0]["mensaje"].ToString();
            if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
            {
                DataTable tabla_archivos = data.Tables[1];
                bool correct = true;
                string ruta = GenerarRuta("PPA_BOR");//BORRADOR
                                                     //todo bien
                                                     //Response.Redirect("perfiles.aspx?sborrador="+interruptor());
                                                     //Response.Redirect(oc_paginaprevia.Value);
                foreach (DataRow row_archi in tabla_archivos.Rows)
                {
                    correct = CopiarArchivos(row_archi["ruta_origen"].ToString(), row_archi["ruta_destino"].ToString());
                    if (correct != true) { Alert.ShowAlertError("Hubo un error al subir uno o mas archivos", this); }
                }

                if (correct == true)
                {
                    int t_archivos_eti = 0;
                    foreach (DataRow row in tabla_archivos.Rows)
                    {
                        string path = row["ruta_origen"].ToString();
                        t_archivos_eti = t_archivos_eti + (File.Exists(path) ? 1 : 0);
                    }
                    int total = ((t_archivos_eti * 1) + 1) * 1000;
                    string t = total.ToString();
                    string idc_puestoperfil_borr = rowr["Resultado"].ToString();
                    Alert.ShowGiftRedirect("Estamos Generando el Borrador y copiando los archivos anexos.", "Espere un Momento", "imagenes/loading.gif", t, "perfiles_captura.aspx?uidc_puestoperfil=" + idc_puestoperfil_borr + "&uborrador=1", this);
                }
            }
            else
            {
                //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                msgbox.show(vmensaje, this.Page);
                return;
            }
        }

        public string GenerarRuta(string codigo_imagen)
        {
            string rutaarch = "";
            var Entidad = new UsuariosE();
            Entidad.Cod_arch = codigo_imagen;
            var Componente = new OrganigramaBL();
            var ds = new DataSet();
            ds = Componente.CargaPath(Entidad);
            if (ds.Tables[0].Rows.Count != 0)
            {
                var tablaGrupos = new DataTable();
                //Paso dataset ala tabla
                tablaGrupos = ds.Tables[0];
                var row = tablaGrupos.Rows[0];
                var carpeta = row["unidad"].ToString();
                var domn = Request.Url.Host;
                var url_upload = carpeta;
                rutaarch = url_upload;
            }
            return rutaarch;
        }

        public bool CopiarArchivos(string sourcefilename, string destfilename)
        {
            try
            {
                if (File.Exists(sourcefilename) && sourcefilename != destfilename)
                {
                    File.Copy(sourcefilename, destfilename, true);
                    return true;
                }

                return true;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
                return false;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            //verifico que tipo de perfil es
            if (cbxTipo.Checked == true)//si es produccion mando directo a pantalla con parammetro 0
            {
                Response.Redirect("perfiles_captura.aspx?uborrador=0");
            }
            if (cbxTipo.Checked == false)//si es borrador mando parametro1
            {
                Response.Redirect("perfiles_captura.aspx?uborrador=1");
            }
        }

        protected void gridperfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gridperfiles.SelectedIndex;
            String valor = gridperfiles.DataKeys[index].Values["id_perfilproduccion"].ToString();
            String valorborrador = gridperfiles.DataKeys[index].Values["id_perfilborrador"].ToString();
            String perfil = gridperfiles.DataKeys[index].Values["descripcion"].ToString();
            perfil = perfil.TrimStart();
            perfil = perfil.Replace(System.Environment.NewLine, "");
            int vidc = Convert.ToInt32(valor);
            int vidc_borr = Convert.ToInt32(valorborrador);

            int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());

            //verificamos estado de switch
            if (cbxTipo.Checked == true && funciones.autorizacion(idc_usuario, 317) == true)//produccion con permisos
            {
                Session["Caso_Confirmacion"] = "Editar Produccion";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','Desea Editar el perfil de " + perfil + "?');", true);
                Session["url_value"] = "perfiles_captura.aspx?uidc_puestoperfil=" + vidc.ToString() + "&uborrador=0";
            }
            //verificamos estado de switch
            if (cbxTipo.Checked == true && funciones.autorizacion(idc_usuario, 317) == false)//produccion sin permisos
            {
                Session["Caso_Confirmacion"] = "Editar Produccion";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','Desea Editar el perfil de " + perfil + "?');", true);
                Session["url_value"] = "perfiles_captura.aspx?uidc_puestoperfil=" + vidc_borr.ToString() + "&uborrador=1";
            }
            if (cbxTipo.Checked == false)//borrador
            {
                if (vidc_borr == 0)
                {
                    Session["tipo_creado_borrado"] = "PRODUCCION-BORRADOR";
                    Session["Caso_Confirmacion"] = "Crear Borrador-Produccion";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','Desea crear un borrador de " + perfil + "? Tambien puede crear un Nuevo borrador sin estar ligado a este perfil.');", true);
                    Session["value"] = vidc;
                    btnSinLigar.Visible = true;
                    Yes.Text = "Si, ligado a perfil";
                }
                else
                {
                    Session["tipo_creado_borrado"] = "BORRADOR-BORRADOR";
                    Session["value"] = vidc_borr;
                    btnSinLigar.Visible = true;
                    Yes.Text = "Editar Este Perfil";
                    Session["Caso_Confirmacion"] = "Editar Produccion";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','Desea Editar el perfil de " + perfil + "? O puede crear un nuevo borrador sin ligar a este perfil');", true);
                    Session["url_value"] = "perfiles_captura.aspx?uidc_puestoperfil=" + vidc_borr.ToString() + "&uborrador=1";
                }
            }
        }

        protected void gridperfiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //INSTANCIO OBJETOS DE ASP  COMO ACCESIBLES
            System.Web.UI.WebControls.Image pendiente = (System.Web.UI.WebControls.Image)e.Row.FindControl("pendiente");
            System.Web.UI.WebControls.Image produccion = (System.Web.UI.WebControls.Image)e.Row.FindControl("produccion");
            System.Web.UI.WebControls.Image borrador = (System.Web.UI.WebControls.Image)e.Row.FindControl("borrador");
            System.Web.UI.WebControls.Image nuevo_registro = (System.Web.UI.WebControls.Image)e.Row.FindControl("Nuevo_Registro");
            //FILTRO LOS DATOS DE TIPO CONTROL
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                if (Convert.ToInt32(rowView["id_perfilproduccion"]) == 0)//si es 0 SIGNIFICA QUE ES NUEVO, MUESTRO ETIQUETA CORRESPONDIENTE
                {
                    //nuevo_registro.ImageUrl = "imagenes/btn/new_register.png";
                    e.Row.Cells[7].BackColor = Color.FromName("#81F79F");
                }
                if (cbxTipo.Checked == false)//si el request viene vacio ejecuto el modo borrador
                {
                    if (Convert.ToBoolean(rowView["pendiente"]) == true)
                    {//si es true check es trueprimeros dos controles vacios
                        e.Row.Cells[1].Controls.Clear();
                        e.Row.Cells[2].Controls.Clear();

                        e.Row.Cells[9].Controls.Clear();
                        pendiente.ImageUrl = "imagenes/btn/checked.png";
                    }
                    else
                    {
                        e.Row.Cells[10].Controls.Clear();
                    }
                    if (Convert.ToBoolean(rowView["produccion"]) == true)//si es true check es true
                    {
                        produccion.ImageUrl = "imagenes/btn/checked.png";
                    }
                    if (Convert.ToBoolean(rowView["borrador"]) == true)//si es true check es true
                    {
                        borrador.ImageUrl = "imagenes/btn/checked.png";
                    }
                    if (Convert.ToInt32(rowView["id_perfilborrador"]) == 0)//no tiene borrador(NO PUEDE BORRAR)
                    {
                        e.Row.Cells[1].Controls.Clear();
                    }
                }
                else
                {
                    e.Row.Cells[9].Controls.Clear();
                    int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                    int idc_tipo_aut = 317;
                    int idc_opcion = 1798;
                    ////valida si tiene permiso de ver esta pagina//
                    if (funciones.autorizacion(idc_usuario, 345) == true)
                    {
                        //si entro quiere decir que no tiene permisos de produccion y por ende se activa el borrador
                        e.Row.Cells[0].Controls.Clear();
                        e.Row.Cells[1].Controls.Clear();
                    }
                    if (funciones.autorizacion(idc_usuario, idc_tipo_aut) == false)
                    {
                        //si entro quiere decir que no tiene permisos de produccion y por ende se activa el borrador
                        e.Row.Cells[0].Controls.Clear();
                        e.Row.Cells[1].Controls.Clear();
                    }

                    if (Convert.ToBoolean(rowView["pendiente"]) == true)
                    {//si es true check es true                        {
                        pendiente.ImageUrl = "imagenes/btn/checked.png";
                    }
                    else
                    {
                        e.Row.Cells[10].Controls.Clear();
                    }
                    if (Convert.ToBoolean(rowView["produccion"]) == true)//si es true check es true
                    {
                        produccion.ImageUrl = "imagenes/btn/checked.png";
                    }
                    if (Convert.ToBoolean(rowView["borrador"]) == true)//si es true check es true
                    {
                        borrador.ImageUrl = "imagenes/btn/checked.png";
                    }

                    if (Convert.ToInt32(rowView["id_perfilproduccion"]) == 0)//no tiene produccion(NO PUEDE BORRAR)
                    {
                        e.Row.Cells[1].Controls.Clear();
                    }
                }
            }

            ////valida si tiene permiso de ver esta pagina//
            if (funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"].ToString()), 345) == true && cbxTipo.Checked == false) //borrador
            {
                //si entro quiere decir que no tiene permisos de produccion y por ende se activa el borrador
                e.Row.Cells[0].Controls.Clear();
                e.Row.Cells[1].Controls.Clear();
            }
            if (funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"].ToString()), 317) == false && cbxTipo.Checked == true)//produ
            {
                //si entro quiere decir que no tiene permisos de produccion y por ende se activa el borrador
                e.Row.Cells[0].Controls.Clear();
                e.Row.Cells[1].Controls.Clear();
            }
        }

        protected void gridperfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER
            int index = Convert.ToInt32(e.CommandArgument);
            id_produccion_grid = Convert.ToInt32(gridperfiles.DataKeys[index].Values["id_perfilproduccion"].ToString());
            id_borrador_grid = Convert.ToInt32(gridperfiles.DataKeys[index].Values["id_perfilborrador"].ToString());
            String perfil = gridperfiles.DataKeys[index].Values["descripcion"].ToString();

            perfil = perfil.TrimStart();
            perfil = perfil.Replace(System.Environment.NewLine, "");
            switch (e.CommandName)
            {
                case "Desbloquear"://Desboquear Autorizacion
                    //Paso parametro a SWICTH
                    Session["Caso_Confirmacion"] = "Desbloquear";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Esta seguro de desbloquear el borrador de " + perfil + "?, tome en cuenta que se habilitaran varias opciones de edición.');", true);
                    break;

                case "Solicitar"://SOLICITO AUTORIZACION
                    if (id_borrador_grid == 0)//si no hay borrador
                    {//Paso parametro a SWICTH
                        //Mandamos script confirmAcion con parametros  TITUTLO y MENSAJE
                        Session["Caso_Confirmacion"] = "Crear Borrador";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','No existe ningun BORRADOR relacionado a " + perfil + ". ¿Desea crear un BORRADOR para este perfil?');", true);
                        id_borradorinexistente = id_produccion_grid;
                    }
                    else
                    {//SI HAY BORRADOR
                        Session["Caso_Confirmacion"] = "Solicitar Autorizacion";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Esta seguro de solicitar la autorización del borrador de " + perfil + "?, tome en cuenta que se deshabilitaran varias opciones de edición.');", true);
                    }
                    break;

                case "Borrar"://BORRAR PERFIL
                    //SI SOLO TENGO BORRADOR O PRODUCCION Y QUIERO ELIMINAR UNO, DONDE NO EXISTE, MADO ALERTA Y TERMINO EJECUCION
                    if (cbxTipo.Checked == true)
                    {
                        if (id_produccion_grid == 0)
                        {
                            Alert.ShowAlertError("No existe un Perfil de " + perfil + " en Producción para ser eliminado.", this);
                            break;
                        }
                    }
                    if (cbxTipo.Checked == false)
                    {
                        if (id_borrador_grid == 0)
                        {
                            Alert.ShowAlertError("No existe un Perfil de " + perfil + " en modo BORRADOR para ser eliminado.", this);
                            break;
                        }
                    }
                    Session["Caso_Confirmacion"] = "Borrar Perfil";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Desea eliminar el perfil de " + perfil + "?, tome en cuenta que se eliminara todo lo relacionado con este perfil.');", true);
                    break;

                case "Vista"://link para vista previa

                    if (cbxTipo.Checked == true)//MODO PRODUCCION
                    {
                        if (id_produccion_grid == 0)//SI NO HAY NADA EN PRODUCCION
                        {
                            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
                            Response.Redirect("perfiles_detalle.aspx?perfiles=true&borrador=1&uidc_puestoperfil_borr=" + id_borrador_grid);
                        }
                        else
                        {
                            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
                            Response.Redirect("perfiles_detalle.aspx?perfiles=true&borrador=0&uidc_puestoperfil=" + id_produccion_grid);
                        }
                    }
                    else
                    {
                        if (id_borrador_grid == 0)//MODO BORRADOR, SI NO HAY NADA EN BORRADOR
                        {
                            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
                            Response.Redirect("perfiles_detalle.aspx?perfiles=true&borrador=0&uidc_puestoperfil=" + id_produccion_grid);
                        }
                        else
                        {
                            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
                            Response.Redirect("perfiles_detalle.aspx?perfiles=true&borrador=1&uidc_puestoperfil_borr=" + id_borrador_grid);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Evento agregado de forma manual, se encadena cuando el usuario confirma modal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Editar Produccion":
                    String url = (String)Session["url_value"];
                    Session["Previus"] = HttpContext.Current.Request.Url.AbsoluteUri;
                    Response.Redirect(url);
                    break;

                case "Crear Borrador-Produccion"://veveve
                    int value = Convert.ToInt32(Session["value"]);
                    TablasBorrador(value);//vavava
                    break;

                case "Borrar Perfil":
                    DeletePerfil();
                    break;

                case "Solicitar Autorizacion":
                    AutorizarBorrador();
                    break;

                case "Crear Borrador":
                    TablasBorrador(id_borradorinexistente);
                    break;

                case "Desbloquear":
                    regresaBorrador();
                    break;

                default:

                    break;
            }
        }

        protected void btnSinLigar_Click(object sender, EventArgs e)
        {
            PanelNuevoBorrador.Visible = true;
            Yes.Visible = false;
            btnSinLigar.Visible = false;
            btnGuardarSinLigar.Visible = true;
            Yes.Text = "Si";
        }

        protected void btnGuardarSinLigar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombrePerfil.Text == "") { lblerror.Visible = true; }
                else
                {
                    string tipo_case = (string)Session["tipo_creado_borrado"];
                    btnGuardarSinLigar.Visible = false;
                    lblerror.Visible = false;
                    //Bajo id de usuario de session
                    int idUsuario = (int)Session["sidc_usuario"];
                    //declaro entidad
                    PerfilesE EntidadB = new PerfilesE();
                    EntidadB.Nombre = txtNombrePerfil.Text.ToUpper();

                    EntidadB.Usuario = idUsuario;
                    EntidadB.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                    EntidadB.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    EntidadB.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    EntidadB.Pusuariopc = funciones.GetUserName();//usuario pc
                    PerfilesBL Componente = new PerfilesBL();
                    DataSet data = new DataSet();

                    ScriptManager.RegisterStartupScript(this, GetType(), "DWWWWWWWE", "ModalClose();", true);
                    switch (tipo_case)
                    {
                        case "PRODUCCION-BORRADOR":
                            EntidadB.Idc_perfil = id_borradorinexistente;
                            data = Componente.VerificaPerfilBorrador(EntidadB);
                            break;

                        case "BORRADOR-BORRADOR":

                            EntidadB.Idc_perfil = Convert.ToInt32(Session["value"]);
                            data = Componente.CopiaBorrador(EntidadB);
                            break;
                    }
                    DataRow rowr = data.Tables[0].Rows[0];
                    string mensaje = rowr["mensaje"].ToString();
                    string vmensaje = data.Tables[0].Rows[0]["mensaje"].ToString();
                    DataTable tabla_archivos = data.Tables[1];
                    if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                    {
                        bool correct = true;
                        foreach (DataRow row_archi in tabla_archivos.Rows)
                        {
                            correct = CopiarArchivos(row_archi["ruta_origen"].ToString(), row_archi["ruta_destino"].ToString());
                            if (correct != true) { Alert.ShowAlertError("Hubo un error al subir uno o mas archivos", this); }
                        }

                        if (correct == true)
                        {
                            int t_archivos_eti = 0;
                            foreach (DataRow row in tabla_archivos.Rows)
                            {
                                string path = row["ruta_origen"].ToString();
                                t_archivos_eti = t_archivos_eti + (File.Exists(path) ? 1 : 0);
                            }
                            int total = ((t_archivos_eti * 1) + 1) * 1000;
                            string t = total.ToString();
                            string idc_puestoperfil_borr = rowr["Resultado"].ToString();
                            Alert.ShowGiftRedirect("Estamos Generando el Borrador y copiando los archivos anexos.", "Espere un Momento", "imagenes/loading.gif", "8000", "perfiles_captura.aspx?uidc_puestoperfil=" + idc_puestoperfil_borr + "&uborrador=1", this);
                        }
                    }
                    else
                    {
                        //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                        lblerror.Visible = true;
                        lblerror.Text = vmensaje;
                        Alert.ShowAlertError(vmensaje, this);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void txtNombrePerfil_TextChanged(object sender, EventArgs e)
        {
            lblerror.Visible = false;
            if (txtNombrePerfil.Text == "") { lblerror.Visible = true; }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("perfiles.aspx");
        }

        protected void lnknuevo_Click(object sender, EventArgs e)
        {
            //verifico que tipo de perfil es
            if (cbxTipo.Checked == true)//si es produccion mando directo a pantalla con parammetro 0
            {
                Response.Redirect("perfiles_captura.aspx?uborrador=0");
            }
            if (cbxTipo.Checked == false)//si es borrador mando parametro1
            {
                Response.Redirect("perfiles_captura.aspx?uborrador=1");
            }
        }

        protected void lnkproduccion_Click(object sender, EventArgs e)
        {
            lnkproduccion.Visible = false;
            lnkborrador.Visible = true;
            cbxTipo.Checked = true;
            lblmensaje.Text = " De Producción";
            lblmensaje.CssClass = "btn btn-success";
            lnknuevo.CssClass = "btn btn-success btn-block";
            //FIN
            try
            {
                CargaGrid();
            }
            catch (Exception ex)
            {
                msgbox.show("eere" + ex.Message, this.Page);
            }
        }

        protected void lnkborrador_Click(object sender, EventArgs e)
        {
            lnkproduccion.Visible = true;
            lnkborrador.Visible = false;
            cbxTipo.Checked = false;
            lblmensaje.Text = " De Borrador";
            lblmensaje.CssClass = "btn btn-primary";
            lnknuevo.CssClass = "btn btn-primary btn-block";
            //FIN
            try
            {
                CargaGrid();
            }
            catch (Exception ex)
            {
                msgbox.show("eere" + ex.Message, this.Page);
            }
        }
    }
}