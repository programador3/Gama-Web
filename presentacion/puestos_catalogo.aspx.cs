using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class puestos_catalogo : System.Web.UI.Page
    {
        public int idc_usuario = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                cbox_puestos_perfil();
            }

            CargarGrid(Convert.ToInt32(Session["sidc_usuario"]));
        }

        /// <summary>
        /// Carga Grid con los datos del puesto
        /// </summary>
        public void CargarGrid(int idc_usuario)
        {
            PuestosENT entidad = new PuestosENT();
            entidad.Idc_Puesto = 0;//INDICAMOS QUE NO QUEREMOS DATOS DE EMPLEADO
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            PuestosCOM componente = new PuestosCOM();
            DataSet ds = componente.CargaCatologoPuestos(entidad);
            Session["Tabla_Puestos"] = ds.Tables[0];
            Session["Tabla_PuestosPrebaja"] = ds.Tables[2];
            gridPuestos.DataSource = ds.Tables[0];//indicamos que es la primer tabla
            gridPuestos.DataBind();
            //sacamos empleados asignados
            Solicitud_PrebajaENT entidaduser = new Solicitud_PrebajaENT();
            Solicitud_PrebajaCOM componenteuser = new Solicitud_PrebajaCOM();
            entidaduser.Pidc_usuario = idc_usuario;
            DataSet dsnew = componenteuser.CargaEmpleados(entidaduser);
            DataTable table = dsnew.Tables[0];
            Session["Tabla_PuestosPermitidos"] = table;
        }

        /// <summary>
        /// Carga Modal con datos del empleado
        /// </summary>
        public void CargarModal(int idc_puesto)
        {
            PuestosENT entidad = new PuestosENT();
            entidad.Idc_Puesto = idc_puesto;//indicamosm que queremos datos de empleado
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            PuestosCOM componente = new PuestosCOM();
            DataSet ds = componente.CargaCatologoPuestos(entidad);
            DataRow row = ds.Tables[1].Rows[0];
            lblMPuesto.Text = row["puesto"].ToString();
            lblMNombre.Text = row["nombre"].ToString();
            lblMFechaNac.Text = row["fecha_nac"].ToString();
            lblMFechaIngreso.Text = row["fecha_ing"].ToString();
            lblMDepto.Text = row["depto"].ToString();
            lblMSucursal.Text = row["sucursal"].ToString();
            int idc_puestoperfil = Convert.ToInt32(row["idc_puestoperfil"]);
            int idc_herramienta = Convert.ToInt32(row["idc_herramienta"]);
            lnkMPerfil.Visible = true;
            lnkMVerHerramientas.Visible = true;
            if (idc_puestoperfil == 0) { lnkMPerfil.Visible = false; }
            if (idc_herramienta == 0) { lnkMVerHerramientas.Visible = false; }
        }

        protected void gridPuestos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                int idc_status = Convert.ToInt32(rowView["idc_statuso"]);
                int idc_puestoperfil = Convert.ToInt32(rowView["idc_puestoperfil"]);
                int perfil_solicitud = Convert.ToInt32(rowView["perfil_solicitud"]);
                //asiganmos color a la  celda status
                String color = rowView["COLOR"].ToString();
                e.Row.Cells[13].BackColor = Color.FromName(color);//el numero puede cambiar
                if (idc_puestoperfil == 0)//SI NO TIENE PERFIL
                {
                    e.Row.Cells[4].Text = "SIN PERFIL RELACIONADO";
                }
                if (perfil_solicitud != 0)
                {
                    e.Row.Cells[5].BackColor = Color.Yellow;
                    e.Row.Cells[5].Text = "EN PROCESO";
                }
            }
        }

        /// <summary>
        /// Asigan color segun status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public String setColor(int id)
        {
            string color = "";
            switch (id)
            {
                case 1: //contratado
                    color = "#c6efce";
                    break;

                case 2: //capacitacion
                    color = "#F0E68C";
                    break;

                case 3: //vacante
                    color = "#FF9999";
                    break;

                case 4: //vacante no contratar por ahora
                    color = "#CCCCCC";
                    break;

                case 5: //incapacitado
                    color = "#D9B2D9";
                    break;

                default: //indefinido
                    color = "black";
                    break;
            }
            return color;
        }

        protected void gridPuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            int id_puesto = Convert.ToInt32(gridPuestos.DataKeys[index].Values["idc_puesto"].ToString());
            int idc_empleado = Convert.ToInt32(gridPuestos.DataKeys[index].Values["idc_empleado"].ToString());
            int status = Convert.ToInt32(gridPuestos.DataKeys[index].Values["idc_statuso"].ToString());
            int id_puestoperfil = Convert.ToInt32(gridPuestos.DataKeys[index].Values["idc_puestoperfil"].ToString());
            int idc_herramienta = Convert.ToInt32(gridPuestos.DataKeys[index].Values["idc_herramienta"].ToString());
            int idc_puesto_reemplazo = Convert.ToInt32(gridPuestos.DataKeys[index].Values["idc_puesto_reemplazo"].ToString());
            int idc_puesto_jefe = Convert.ToInt32(gridPuestos.DataKeys[index].Values["idc_puesto_jefe"].ToString());
            Session["idc_prepara"] = gridPuestos.DataKeys[index].Values["idc_prepara"].ToString();
            Session["puesto"] = gridPuestos.DataKeys[index].Values["descripcion"].ToString();
            Session["status"] = status;
            Session["idc_puesto"] = id_puesto;
            string IDC_EMPL = idc_empleado.ToString();
            Session["idc_empleado"] = IDC_EMPL;
            int IDC_PUESTO_LOGIN = Convert.ToInt32(Session["sidc_puesto_login"]);
            lnkMVerHerramientas.Visible = idc_herramienta == 0 ? false : true;
            lnkMPerfil.Visible = id_puestoperfil == 0 ? false : true;
            //SI es el jefe directo
            lnkservicios.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 338);
            lnkservicios_medan.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 350);
            if (idc_puesto_reemplazo == 0 || Convert.ToInt32(gridPuestos.DataKeys[index].Values["idc_prepara"]) == 0)
            {
                lnkreemplazo.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 351);
            }
            lnkprebaja.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 155);
            lnkasignarperfil.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 353);
            lnkpmd.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 352);
            if (IDC_PUESTO_LOGIN == idc_puesto_jefe)
            {
                lnkpermiso.Visible = true;
                lnkprebaja.Visible = true;
                lnkservicios.Visible = true;
                if (idc_puesto_reemplazo == 0 &&  Convert.ToInt32(gridPuestos.DataKeys[index].Values["idc_prepara"]) == 0)
                {
                    lnkreemplazo.Visible = true;
                }
                lnkpmd.Visible = true;
                lnkprebaja.Visible = true;
            }
            if (status == 4 | status == 3)//SI EL STATUS ES VACANTE O VACANTE NO CONTRATAR, EL PUESTO NO CONTIENE NINGUN EMPLEADO
            {
                lnkprebaja.Visible = false;
            }

            DataTable table = (DataTable)Session["Tabla_PuestosPermitidos"];
            DataRow[] DR;
            DR = table.Select("idc_empleado=" + idc_empleado);
            if (DR.Length == 0)
            {
                lnkprebaja.Visible = false;
            }
            switch (e.CommandName)
            {
                case "Status":
                    Session["Caso_Confirmacion"] = "Status";
                    string statustipo = "No Contratar";
                    string puesto = Session["puesto"].ToString();
                    if (status == 4) { statustipo = "Contratar"; }
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea cambiar el Status de " + gridPuestos.DataKeys[index].Values["descripcion"].ToString() + " a " + statustipo.ToUpper() + "?');", true);

                    break;

                case "Herramientas":
                    Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
                    Response.Redirect("herramientas_catalogo.aspx?idc_puesto=" + id_puesto + "&idc_empleado=" + idc_empleado);
                    break;

                case "Vista":
                    if (id_puestoperfil == 0)//SI NO HAY PERFIL RELACIONADO
                    {
                        Alert.ShowAlertError("No hay ningun Perfil Relacionado", this);
                    }
                    else
                    {
                        Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
                        Response.Redirect("perfiles_detalle.aspx?borrador=0&uidc_puestoperfil=" + id_puestoperfil);
                    }
                    break;

                case "Puesto":
                    Session["idc_puestoperfil"] = id_puestoperfil;
                    Session["idc_puesto"] = id_puesto;
                    Session["idc_empleado"] = idc_empleado;
                    CargarModal(id_puesto);
                    GenerarRuta(idc_empleado, id_puesto);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "MoadlPrevivew();", true);
                    break;
            }
        }

        protected void lnkasignarperfil_Click(object sender, EventArgs e)
        {
            modal_lblpuesto.Text = "Puesto: " + (string)Session["puesto"];
            oc_idc_puesto.Value = Convert.ToInt32(Session["idc_puesto"]).ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalPerfil();", true);
        }

        protected void lnkprebaja_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Prebaja";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta seguro de solicitar la Pre-Baja de " + (string)Session["puesto"] + "?');", true);
        }

        protected void lnkreemplazo_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Reemplazo";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea solicitar el reemplazo de " + (string)Session["puesto"] + "?');", true);
        }

        protected void lnkservicios_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Asignar Servicios";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Modificara el listados de Puestos a quienes podra dar Servicios " + (string)Session["puesto"] + "?');", true);
        }

        protected void lnkpmd_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "PMD";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea solicitar un Proceso de Mejora de Desempeño para " + (string)Session["puesto"] + "?');", true);
        }

        protected void cbox_puestos_perfil()
        {
            try
            {
                //componente
                PuestosCOM ComPuesto = new PuestosCOM();
                //ds
                DataSet ds = new DataSet();
                ds = ComPuesto.combo_puestos_perfil();
                //llenar cbox
                modal_cboxperfiles.DataSource = ds.Tables[0];
                modal_cboxperfiles.DataTextField = "descripcion";
                modal_cboxperfiles.DataValueField = "idc_puestoperfil";
                modal_cboxperfiles.DataBind();
                modal_cboxperfiles.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
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
                case "Status":
                    int status = (int)Session["status"];
                    int idc_puesto = (int)Session["idc_puesto"];
                    PuestosENT entidad = new PuestosENT();
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_Puesto = idc_puesto;
                    entidad.Pstatus = status;
                    entidad.Pidc_prepara = Convert.ToInt32(Session["idc_prepara"]);
                    PuestosCOM componente = new PuestosCOM();
                    DataSet ds = componente.CambiarStatus(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    string mensaje = row["mensaje"].ToString();
                    string statustipo = "No Contratar";
                    if (status == 4) { statustipo = "Contratar"; }
                    if (mensaje == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('Status cambiado a " + statustipo + " correctamente.','puestos_catalogo.aspx');", true);
                    }
                    else
                    {
                        Alert.ShowAlertError(mensaje, this);
                    }
                    break;

                case "Prebaja":
                    string idc_empleado = Session["idc_empleado"].ToString();
                    Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
                    Response.Redirect("pre_bajas.aspx?idc_empleado=" + idc_empleado);
                    break;

                case "Confirm Perfil":
                    //continuamos
                    //ingresamos los valores en la tabla temp
                    int value = Convert.ToInt32(Session["value_perfil"]);
                    int id_puesto = Convert.ToInt32(Session["idc_puesto"]);
                    //entidad
                    PuestosENT EntPuesto = new PuestosENT();
                    EntPuesto.Idc_Puesto = id_puesto;
                    EntPuesto.Idc_puestoperfil = Convert.ToInt32(modal_cboxperfiles.SelectedValue);
                    EntPuesto.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                    EntPuesto.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    EntPuesto.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    EntPuesto.Pusuariopc = funciones.GetUserName();//usuario pc
                    //componente
                    PuestosCOM ComPuesto = new PuestosCOM();
                    DataSet ds1 = new DataSet();
                    ds1 = ComPuesto.puesto_perfil_temp_captura(EntPuesto);
                    string vmensaje = ds1.Tables[0].Rows[0]["mensaje"].ToString();
                    if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('Solicitud de aprobación correcta','puestos_catalogo.aspx');", true);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje, this.Page);
                    }
                    break;

                case "Asignar Servicios":
                    Response.Redirect("servicios_captura.aspx?idc_puesto=" + funciones.deTextoa64((Convert.ToInt32(Session["idc_puesto"])).ToString()));
                    break;

                case "Reemplazo":
                    Response.Redirect("solicitar_reemplazo.aspx?idc_puesto=" + funciones.deTextoa64((Convert.ToInt32(Session["idc_puesto"])).ToString()));
                    break;

                case "PMD":
                    Response.Redirect("pmd.aspx?idc_puesto=" + funciones.deTextoa64((Convert.ToInt32(Session["idc_puesto"])).ToString()));
                    break;

                case "Servicios Asignados":
                    Response.Redirect("servicios_captura.aspx?val=KJBXAKJASBKjbamndvlknsclkanconbwclnwcokn&idc_puesto=" + funciones.deTextoa64((Convert.ToInt32(Session["idc_puesto"])).ToString()));
                    break;

                case "Permiso":
                    Response.Redirect("solicitud_horario.aspx?idc_puesto=" + funciones.deTextoa64((Convert.ToInt32(Session["idc_puesto"])).ToString()));
                    break;
            }
        }

        /// <summary>
        /// Genera ruta de imagen para mostrarla en modal, muestra modal con informacion del empleado
        /// </summary>
        public void GenerarRuta(int idc_empleado, int id_puesto)
        {
            var Entidad = new UsuariosE();
            Entidad.Cod_arch = "fot_emp";
            var Componente = new OrganigramaBL();
            var ds = new DataSet();
            ds = Componente.CargaPath(Entidad);
            if (ds.Tables[0].Rows.Count == 0)
            {
            }
            else
            {
                var tablaGrupos = new DataTable();
                //Paso dataset ala tabla
                tablaGrupos = ds.Tables[0];
                var row = tablaGrupos.Rows[0];
                var carpeta = row["rw_carpeta"].ToString();
                var domn = Request.Url.Host;
                if (domn == "localhost")
                {
                    var url = "imagenes/btn/default_employed.png";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "getImage('" + url + "');", true);
                }
                else
                {
                    var url = "http://" + domn + carpeta + idc_empleado + ".jpg";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "getImage('" + url + "');", true);
                }
            }
        }

        protected void lnkMVerHerramientas_Click(object sender, EventArgs e)
        {
            int idc_puesto = Convert.ToInt32(Session["idc_puesto"]);
            int idc_empleado = Convert.ToInt32(Session["idc_empleado"]);
            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
            Response.Redirect("herramientas_catalogo.aspx?idc_puesto=" + idc_puesto + "&idc_empleado=" + idc_empleado);
        }

        protected void lnkMPerfil_Click(object sender, EventArgs e)
        {
            int idc_puestoperfil = Convert.ToInt32(Session["idc_puestoperfil"]);
            if (idc_puestoperfil == 0)//SI NO HAY PERFIL RELACIONADO
            {
                Alert.ShowAlertError("No hay ningun Perfil Relacionado", this);
            }
            else
            {
                Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
                Response.Redirect("perfiles_detalle.aspx?borrador=0&uidc_puestoperfil=" + idc_puestoperfil);
            }
        }

        protected void lnkGuardarTodo_Click(object sender, EventArgs e)
        {
            DataTable tabla_puestos = (DataTable)Session["Tabla_Puestos"];
            tabla_puestos.Columns.Remove("idc_puesto");
            tabla_puestos.Columns.Remove("idc_empleado");
            tabla_puestos.Columns.Remove("idc_puestoperfil");
            tabla_puestos.Columns.Remove("idc_depto");
            tabla_puestos.Columns.Remove("idc_statuso");
            tabla_puestos.Columns.Remove("idc_sucursal");
            tabla_puestos.Columns["descripcion"].SetOrdinal(0);
            tabla_puestos.Columns["nombre"].ColumnName = "Empleado";
            tabla_puestos.Columns["descripcion"].ColumnName = "Puesto";
            tabla_puestos.Columns["depto"].ColumnName = "Departamento";
            tabla_puestos.Columns["perfil"].ColumnName = "Perfil";
            Export Export = new Export();
            //array de DataTables
            List<DataTable> ListaTables = new List<DataTable>();
            ListaTables.Add(tabla_puestos);
            //array de nombre de sheets
            string[] Nombres = new string[] { "Puestos" };
            if (tabla_puestos.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("Este Puesto no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
            }
            else
            {
                string mensaje = Export.toExcel("Lista Completo de Puestos y Empleados Actuales", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                                   XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 1,
                                   "Listado_de_Puestos.xlsx", Page.Response);
                if (mensaje != "")
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }

        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            DataTable tabla_puestos = (DataTable)Session["Tabla_Puestos"];
            tabla_puestos.Columns.Remove("idc_puesto");
            tabla_puestos.Columns.Remove("idc_empleado");
            tabla_puestos.Columns.Remove("idc_puestoperfil");
            tabla_puestos.Columns.Remove("idc_depto");
            tabla_puestos.Columns.Remove("idc_statuso");
            tabla_puestos.Columns.Remove("idc_sucursal");
            tabla_puestos.Columns.Remove("perfil");
            tabla_puestos.Columns["descripcion"].SetOrdinal(0);
            tabla_puestos.Columns["nombre"].ColumnName = "Empleado";
            tabla_puestos.Columns["descripcion"].ColumnName = "Puesto";
            tabla_puestos.Columns["depto"].ColumnName = "Departamento";
            Export Export = new Export();
            // Lista de DT
            List<DataTable> ListaTables = new List<DataTable>();
            ListaTables.Add(tabla_puestos);
            //array de nombre de sheets
            string[] Nombres = new string[] { "Lista de Puestos" };
            if (tabla_puestos.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("Este Puesto no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
            }
            else
            {
                string mensaje = Export.ToPdf("puestos", ListaTables, 1, Nombres, Page.Response);
                if (mensaje != "")
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        protected void btnAsiP_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(Session["value_perfil"]);
            if (modal_cboxperfiles.SelectedIndex == 0)
            {
                Alert.ShowAlertError("Debe seleccionar un perfil", this.Page);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Confirm Perfil";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea cambiar el perfil de " + modal_lblpuesto.Text + "?');", true);
            }
        }

        protected string solicitudAprobacion(int id_row)
        {
            try
            {
                int idc_aprobacion_soli = 0;
                //ALERTA REVISAR ESTA SESISON
                //Session.Add("sidc_aprobacion_soli", idc_aprobacion_soli);
                //SI NO TIENE SOLICITUD SE CREA
                if (idc_aprobacion_soli == 0)
                { //proseguimos a insertar la solicitud
                    //nevesitamos mandar los sig datos
                    int idc_aprobacion = 3; //antes 11 que aprobacion queremos solicitar
                    int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString()); //que usuario hace la solicitud
                    int idc_registro = id_row; // el id del registro que se quiere aprobar EN ESTE CASO EL ID puesto

                    //llamamos la entidad
                    Aprobaciones_solicitudE entidad = new Aprobaciones_solicitudE();
                    entidad.Idc_aprobacion = idc_aprobacion;
                    entidad.Idc_usuario = idc_usuario;
                    entidad.Idc_registro = idc_registro;
                    entidad.Idc_aprobacion_soli = idc_aprobacion_soli; // SE MANDA CERO NO PASA NADA
                    //llamamos al componente
                    DataSet ds = new DataSet();
                    Aprobaciones_solicitudBL componente = new Aprobaciones_solicitudBL();
                    ds = componente.nueva_solicitud_aprobacion(entidad);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    int vfolio = Convert.ToInt32(ds.Tables[0].Rows[0]["folio"].ToString()); //id de la solicitud de aprobacion
                    //ALERTA REVISAR ESTA SESISON
                    //Session.Add("sidc_aprobacion_soli", vfolio);
                    string vmensaje_no2 = "";
                    if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                    {
                        //ahora sigue los adicionales
                        //debe mandarse el id del puesto para saber el jefe d ese puesto al que se le asignara el perfil
                        int id_row_puesto = id_row;
                        vmensaje_no2 = solicitudAprobacionAdicional(vfolio, id_row_puesto);
                        //todo bien en teoria aqui no se valida el mensaje vacio sino donde se llama.
                    }
                    if (vmensaje == "" & vmensaje_no2 == "")
                    {
                        //si los dos estan vacios pues manda vacio
                        return "";
                    }
                    else
                    {
                        return vmensaje + " " + vmensaje_no2;
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                //Alert.ShowAlertError(ex.Message,this.Page);
                return ex.Message;
            }
        }

        protected string solicitudAprobacionAdicional(int idc_aprobacion_soli, int id_row)
        {
            //nevesitamos mandar los sig datos

            try
            {
                //llamamos la entidad
                Aprobaciones_solicitudE entidad = new Aprobaciones_solicitudE();
                entidad.Idc_aprobacion_soli = idc_aprobacion_soli;
                entidad.Idc_registro = id_row;
                //llamamos al componente
                DataSet ds = new DataSet();
                Aprobaciones_solicitudBL componente = new Aprobaciones_solicitudBL();
                ds = componente.solicitud_aprobacion_adicional_ligarperfil(entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();

                //todo bien
                return vmensaje;
            }
            catch (Exception ex)
            {
                //Alert.ShowAlertError(ex.Message,this.Page);
                return ex.Message;
            }
        }

        protected void modal_cboxperfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(modal_cboxperfiles.SelectedValue);
            Session["value_perfil"] = value;
        }

        protected void lnkservicios_medan_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Servicios Asignados";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Modificara el listado de quien le da servicio a " + (string)Session["puesto"] + "?');", true);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Permiso";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Solicitar un Permiso para cambiar el horario a " + (string)Session["puesto"] + "?');", true);
        }
    }
}