using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class cursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ////SI el reuqest viene de un evento via doPost por un boton
            //if (Request.Form["__EVENTTARGET"] == "btnNew")
            //{   //llamamos el metodo que queremos ejecutar, en este caso el evento onclick del boton btnNew
            //    btnNew_Click(this, new EventArgs());
            //}
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["sborrador"] == null)// si el request viene vacio iniciamos en borrador
                {
                    cbxTipo.Checked = true;
                }
                else
                {//tomamos parametros y comprobamos
                    int sborrador = 0;
                    sborrador = Convert.ToInt32(Request.QueryString["sborrador"]);
                    if (sborrador == 0)
                    {
                        cbxTipo.Checked = false; //produccion
                    }
                    else
                    {
                        cbxTipo.Checked = true; // borrador
                    }
                }

                //llenar grid
                bool borrador = cbxTipo.Checked;
                //coloreamos el boton
                btnnuevocurso.CssClass = (borrador == true) ? "borrador" : "produccion";
                cargar_grid(borrador);
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_tipo_aut = 317;//PERMISO
                int sborradores = Request.QueryString["sborrador"] == null ? 1 : Convert.ToInt32(Request.QueryString["sborrador"]);
                //if (funciones.autorizacion(idc_usuario, idc_tipo_aut) == false && sborradores == 0)
                //{
                //    //si entro quiere decir que no tiene permisos de produccion y por ende se activa el borrador
                //    btnnuevocurso.Visible = false;
                //}
            }
        }

        protected void cargar_grid(bool borrador)
        {
            try
            {
                //entidad
                CursosE EntCursos = new CursosE();
                EntCursos.Borrador = borrador;
                //componente
                CursosCOM ComCursos = new CursosCOM();
                //ds
                DataSet ds = new DataSet();
                ds = ComCursos.cursos(EntCursos);
                //llenar grid view
                grid_cursos.DataSource = ds.Tables[0];
                grid_cursos.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        protected void grid_cursos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                //estatus produccion, borrador, pendiente
                Image iconBorr = (Image)e.Row.FindControl("imgborr");
                Image iconProd = (Image)e.Row.FindControl("imgprod");
                Image iconPendiente = (Image)e.Row.FindControl("imgpendiente");
                //produccion
                bool produccion = Convert.ToBoolean(rowView["produccion"]);
                if (produccion)
                {
                    iconProd.ImageUrl = "imagenes/btn/checked.png";
                }
                //borrador
                bool borrador = Convert.ToBoolean(rowView["borrador"]);
                if (borrador)
                {
                    iconBorr.ImageUrl = "imagenes/btn/checked.png";
                }
                //pendiente
                bool pendiente = (rowView["pendiente"] is DBNull) ? false : Convert.ToBoolean(rowView["pendiente"]);
                if (pendiente)
                {
                    iconPendiente.ImageUrl = "imagenes/btn/checked.png"; ;
                }

                //borrador nuevo
                bool nuevoborrador = (rowView["idc_curso_p"] is DBNull) ? true : false;
                if (nuevoborrador)
                {
                    Image iconNuevoBorr = (Image)e.Row.FindControl("imgnuevoborr");
                    iconNuevoBorr.ImageUrl = "imagenes/btn/new_register.png";
                }

                //MODO BORRADOR ********************************
                if (cbxTipo.Checked == true)
                {
                    //texto del curso
                    //si existe borrador usa el nombre del borrador
                    Label titcurso = (Label)e.Row.FindControl("lblcurso");
                    Label tipocurso = (Label)e.Row.FindControl("lbltipo_curso");
                    if (rowView["idc_curso_borr"] is DBNull)
                    { //utiliza el nombre de produccion
                        titcurso.Text = rowView["descripcion_p"].ToString();
                        tipocurso.Text = (rowView["tipo_curso_p"].ToString() == "I") ? "Interno" : "Externo";
                    }
                    else
                    { //utiliza el nombre del borrador
                        titcurso.Text = rowView["descripcion_b"].ToString();
                        tipocurso.Text = (rowView["tipo_curso_b"].ToString() == "I") ? "Interno" : "Externo";
                    }
                    //ocultar editar, eliminar, solicitar y borrar si tiene una solicitud en proceso
                    ImageButton btnedit = (ImageButton)e.Row.FindControl("imgbtneditcurso");
                    ImageButton btndelete = (ImageButton)e.Row.FindControl("imgbtndeletecurso");
                    ImageButton imgbtnsolicitar = (ImageButton)e.Row.FindControl("imgbtnsolicitar");
                    ImageButton imgbtndesbloq = (ImageButton)e.Row.FindControl("imgbtndesbloquear");

                    if (rowView["solicitud"] is DBNull)
                    { //no tiene solicitud
                        //esta pendiente?
                        if (pendiente == false)
                        {
                            btnedit.Visible = true;
                            btndelete.Visible = true;
                            //
                            imgbtnsolicitar.Visible = true;
                            //
                            imgbtndesbloq.Visible = false;
                        }
                        else
                        {
                            btnedit.Visible = false;
                            btndelete.Visible = false;
                            //
                            imgbtnsolicitar.Visible = false;
                            //
                            imgbtndesbloq.Visible = true;
                        }

                        //puede que no exista el borrador por ende no debe estar habilitado solicitar
                        if (rowView["idc_curso_borr"] is DBNull)
                        {
                            imgbtnsolicitar.Visible = false;
                        }
                    }
                    else
                    { //TIENE SOLICITUD
                        btnedit.Visible = false;
                        btndelete.Visible = false;
                        //
                        imgbtnsolicitar.Visible = false;
                        //
                        imgbtndesbloq.Visible = false;
                    }
                }
                else
                { //MODO PRODUCCION ***********************
                  //la consulta debe ser distinta
                    e.Row.Cells[5].Text = "";
                    Label titcurso = (Label)e.Row.FindControl("lblcurso");
                    titcurso.Text = rowView["descripcion_p"].ToString();
                    //
                    Label tipocur = (Label)e.Row.FindControl("lbltipo_curso");
                    tipocur.Text = (rowView["tipo_curso_p"].ToString() == "I") ? "Interno" : "Externo";
                    //se quita la opcion de solicitar y desbloquear
                    ImageButton imgbtnsolicitar = (ImageButton)e.Row.FindControl("imgbtnsolicitar");
                    imgbtnsolicitar.Visible = false;
                    //
                    ImageButton imgbtndesbloq = (ImageButton)e.Row.FindControl("imgbtndesbloquear");
                    imgbtndesbloq.Visible = false;
                    int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                    int idc_tipo_aut = 317;//PERMISO
                    ////valida si tiene permiso de ver esta pagina//
                    //if (funciones.autorizacion(idc_usuario, idc_tipo_aut) == false)
                    //{
                    //    //si entro quiere decir que no tiene permisos de produccion y por ende se activa el borrador
                    //    e.Row.Cells[2].Controls.Clear();
                    //    e.Row.Cells[3].Controls.Clear();
                    //}
                }
            }
        }

        protected void grid_cursos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //me causaba error cuando usaba el link ver..
            //recuperamos el indice del registro seleccionado del grid de Telefono
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_curso_p = (grid_cursos.DataKeys[index].Values["idc_curso_p"] is DBNull) ? 0 : Convert.ToInt32(grid_cursos.DataKeys[index].Values["idc_curso_p"]);
            int idc_curso_borr = (grid_cursos.DataKeys[index].Values["idc_curso_borr"] is DBNull) ? 0 : Convert.ToInt32(grid_cursos.DataKeys[index].Values["idc_curso_borr"]);
            Session["idc_curso_p"] = idc_curso_p;
            Session["idc_curso_borr"] = idc_curso_borr;
            //Session["sidc_perfilgpo"] = Convert.ToInt32(vidc.ToString().Trim());
            switch (e.CommandName)
            {
                case "editcurso":
                    //revisar el check prod/borr
                    if (cbxTipo.Checked)
                    { //borrador
                        //revisamos que tenga id > 0
                        if (idc_curso_borr > 0)
                        { //prosigue
                            Response.Redirect("cursos_captura.aspx?borrador=1&uidc_curso_borr=" + idc_curso_borr);
                        }
                        else
                        {
                            //cancela
                            modal_oc_idc_curso.Value = idc_curso_p.ToString(); //es el id de produccion
                            modal_oc_accion.Value = "crearBorrador";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','No cuenta con un curso en modo borrador ¿Desea crear uno?.');", true);
                            break;
                        }
                    }
                    else
                    {     //PRODUCCION
                        //revisamos que el id sea mayor a cero
                        if (idc_curso_p > 0)
                        {
                            //prosigue
                            Response.Redirect("cursos_captura.aspx?borrador=0&uidc_curso_p=" + idc_curso_p);
                        }
                        else
                        {
                            //cancela
                            Alert.ShowAlertError("No cuenta con un curso en modo producción.", this.Page);
                            break;
                        }
                    }

                    break;

                case "deletecurso":
                    //revisar el check prod/borr
                    if (cbxTipo.Checked)
                    { //borrador
                        //revisamos que tenga id > 0
                        if (idc_curso_borr > 0)
                        { //prosigue
                            //pregunta
                            modal_oc_idc_curso.Value = idc_curso_borr.ToString();
                            modal_oc_accion.Value = "eliminaBorrador";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Desea eliminar este borrador?.');", true);
                        }
                        else
                        {
                            //cancela
                            Alert.ShowAlertError("No cuenta con un curso en modo borrador.", this.Page);
                            break;
                        }
                    }
                    else
                    {     //PRODUCCION
                        int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                        int idc_tipo_aut = 317;//PERMISO
                                               //revisamos que el id sea mayor a cero
                        if (idc_curso_p > 0)
                        {
                            modal_oc_idc_curso.Value = idc_curso_p.ToString();
                            modal_oc_accion.Value = "eliminaProduccion";
                            //pregunta
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Desea eliminar este curso de producción?.');", true);
                        }
                        else
                        {
                            //cancela
                            Alert.ShowAlertError("No cuenta con un curso en modo producción.", this.Page);
                            break;
                        }
                    }
                    break;

                case "solicitar":
                    modal_oc_idc_curso.Value = idc_curso_borr.ToString();
                    modal_oc_accion.Value = "notificarSolicitud";
                    //pregunta
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Desea notificar que se apruebe este borrador?.');", true);
                    break;

                case "desbloquear":
                    modal_oc_idc_curso.Value = idc_curso_borr.ToString();
                    modal_oc_accion.Value = "desbloquearSolicitud";
                    //pregunta
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Desea abortar la  notificación para que se apruebe este borrador?.');", true);
                    break;
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //recuperamos el valor oculto
            int idc = Convert.ToInt32(modal_oc_idc_curso.Value);
            string accion = modal_oc_accion.Value;
            DataSet ds = new DataSet();

            CursosCOM ComCurso = new CursosCOM();
            CursosE EntCurso = new CursosE();
            switch (accion)
            {
                case "eliminaBorrador":
                    EntCurso.Idc_curso = Convert.ToInt32(Session["idc_curso_borr"]);
                    EntCurso.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    EntCurso.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    EntCurso.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    EntCurso.Pusuariopc = funciones.GetUserName();//usuario pc
                                                                  //ds

                    //componente
                    ds = ComCurso.DeleteBorrador(EntCurso);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    DataRow rowr = ds.Tables[0].Rows[0];
                    if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                    {
                        cargar_grid(false);
                        Alert.ShowAlert("El Curso fue eliminado Correctamente", "Mensaje del Sistema", this);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje, this);
                    }

                    break;

                case "eliminaProduccion":

                    EntCurso.Idc_curso = Convert.ToInt32(Session["idc_curso_p"]);
                    EntCurso.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    EntCurso.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    EntCurso.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    EntCurso.Pusuariopc = funciones.GetUserName();//usuario pc
                    //componente
                    ds = ComCurso.DeleteBorrador(EntCurso);
                    string vmensaje2 = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (string.IsNullOrEmpty(vmensaje2)) // si esta vacio todo bien
                    {
                        cargar_grid(false);
                        Alert.ShowAlert("El Curso fue eliminado Correctamente", "Mensaje del Sistema", this);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje2, this);
                    }
                    break;

                case "notificarSolicitud":
                    EntCurso.Idc_curso = Convert.ToInt32(Session["idc_curso_borr"]);
                    EntCurso.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    EntCurso.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    EntCurso.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    EntCurso.Pusuariopc = funciones.GetUserName();//usuario pc
                    //componente
                    ds = ComCurso.EnviarSolicitud(EntCurso);
                    string vmensaje3 = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (string.IsNullOrEmpty(vmensaje3)) // si esta vacio todo bien
                    {
                        cargar_grid(false);
                        Alert.ShowAlert("La Solcitud fue enviada Correctamente", "Mensaje del Sistema", this);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje3, this);
                    }
                    break;

                case "desbloquearSolicitud":
                    EntCurso.Idc_curso = Convert.ToInt32(Session["idc_curso_borr"]);
                    EntCurso.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    EntCurso.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    EntCurso.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    EntCurso.Pusuariopc = funciones.GetUserName();//usuario pc
                    //componente
                    ds = ComCurso.CancelarSolicitud(EntCurso);
                    string vmensaje4 = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (string.IsNullOrEmpty(vmensaje4)) // si esta vacio todo bien
                    {
                        cargar_grid(false); Alert.ShowAlert("El Curso fue Desbloqueado Correctamente", "Mensaje del Sistema", this);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje4, this);
                    }
                    break;

                case "crearBorrador":
                    //necesitamos el usuario
                    CrearBorrador(idc, false, Convert.ToInt32(Session["sidc_usuario"]));
                    break;
            }
            limpiar();
        }

        /// <summary>
        /// este metodo recibe como parametro el idc_curso que se quiere pasar a borrador
        /// el segundo parametro es 1(es de un borrador a produccion) y 0 ( es de produccion a borrador)
        /// y el tecer parametro es el id del usuario que quiere el borrador.
        /// </summary>
        /// <param name="idc_curso"></param>
        /// <param name="produccion"></param>
        /// <param name="idc_usuario"></param>
        protected void CrearBorrador(int idc_curso, bool produccion, int idc_usuario)
        {
            try
            {
                //entidad
                CursosE EntCurso = new CursosE();
                EntCurso.Idc_curso = idc_curso;
                EntCurso.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                EntCurso.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                EntCurso.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                EntCurso.Pusuariopc = funciones.GetUserName();//usuario pc
                //ds
                DataSet ds = new DataSet();
                //componente
                CursosCOM ComCurso = new CursosCOM();
                ds = ComCurso.cursosVaciar(EntCurso);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                DataRow rowr = ds.Tables[0].Rows[0];
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    DataTable tabla_archivos = ds.Tables[1];
                    bool correct = true;
                    //BORRADOR
                    //todo bien
                    //Response.Redirect("perfiles.aspx?sborrador="+interruptor());
                    //Response.Redirect(oc_paginaprevia.Value);
                    foreach (DataRow row_archi in tabla_archivos.Rows)
                    {
                        correct = CopiarArchivos(row_archi["origen"].ToString(), row_archi["destino"].ToString());
                        if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo " + row_archi["origen"].ToString() + ", verifiquelo con el Departamento de Sistemas", this); }
                    }
                    if (correct == true)
                    {
                        int total = ((tabla_archivos.Rows.Count * 1) + 1) * 1000;
                        string t = total.ToString();
                        string idc_puestoperfil_borr = rowr["folio"].ToString();
                        Alert.ShowGiftRedirect("Estamos Generando el Borrador y copiando los archivos anexos.", "Espere un Momento", "imagenes/loading.gif", t, "cursos_captura.aspx?borrador=1&uidc_curso_borr=" + idc_puestoperfil_borr + "", this);
                    }
                }
                else
                {
                    //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                    msgbox.show(vmensaje, this.Page);
                    return;
                }
                cargar_grid(cbxTipo.Checked);
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
            }
        }

        protected void limpiar()
        {
            modal_oc_idc_curso.Value = "";
            modal_oc_accion.Value = "";
            modal_oc_pendiente.Value = "";
        }

        public bool CopiarArchivos(string sourcefilename, string destfilename)
        {
            try
            {
                if (File.Exists(sourcefilename))
                {
                    File.Copy(sourcefilename, destfilename, true);
                    return true;
                }
                else
                {
                    Alert.ShowAlertError("No se puede copiar el archivo " + sourcefilename + ", verifiquelo con el depto de sistemas.", this);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
                return false;
            }
        }

        protected void EliminarCurso(int idc_curso, bool borrador)
        {
            try
            {
                //entidad
                CursosE EntCurso = new CursosE();
                EntCurso.Idc_curso = (borrador == false) ? idc_curso : 0;
                EntCurso.Idc_curso_borr = (borrador == true) ? idc_curso : 0;
                EntCurso.Borrador = borrador;
                //componente
                CursosCOM ComCurso = new CursosCOM();
                //ds
                DataSet ds = new DataSet();
                ds = ComCurso.cursosEliminar(EntCurso);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    cargar_grid(cbxTipo.Checked);
                    Alert.ShowAlert("Curso eliminado correctamente", "Mensaje del sistema", this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this.Page);
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
            }
        }

        protected void ActualizarPendiente(int idc_curso_borr, bool pendiente)
        {
            try
            {
                //entidad
                CursosE EntCurso = new CursosE();
                EntCurso.Idc_curso_borr = idc_curso_borr;
                EntCurso.Pendiente = pendiente;
                //ds
                DataSet ds = new DataSet();
                //componente
                CursosCOM ComCurso = new CursosCOM();
                ds = ComCurso.cursosPendiente(EntCurso);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    string mensaje = (pendiente == true) ? "notificación para aprobación realizada" : "borrador desbloqueado";
                    cargar_grid(cbxTipo.Checked);
                    Alert.ShowAlert(mensaje, "Mensaje del sistema", this.Page);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this.Page);
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
            }
        }

        protected void btnnuevocurso_Click(object sender, EventArgs e)
        {
            //verifico que tipo de curso es
            if (cbxTipo.Checked == true)//si es borrador mando directo a pantalla con parammetro 1
            {
                Response.Redirect("cursos_captura.aspx?borrador=1&uidc_curso_borr=0");
            }
            if (cbxTipo.Checked == false)//si es produccion mando parametro 0
            {
                Response.Redirect("cursos_captura.aspx?borrador=0&uidc_curso_p=0");
            }
        }

        protected void btnGuardarSinLigar_Click(object sender, EventArgs e)
        {
        }

        protected void btnSinLigar_Click(object sender, EventArgs e)
        {
        }

        protected void txtNombreCurso_TextChanged(object sender, EventArgs e)
        {
        }

        protected void No_Click(object sender, EventArgs e)
        {
            Response.Redirect("cursos.aspx");
        }
    }
}