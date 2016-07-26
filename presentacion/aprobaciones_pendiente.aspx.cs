using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class aprobaciones_pendiente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["sidc_puestoperfil_borr"] = null;
                CargarGrid_aprob_pendientes();
            }
        }

        /// <summary>
        /// Carga los datos del grid desde una base de datos SQL
        /// </summary>
        public void CargarGrid_aprob_pendientes()
        {
            int idc_puesto = 0;
            if (Session["sidc_puesto_login"] != null)
            {
                idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
            }
            AprobacionesENT entidad = new AprobacionesENT();
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            entidad.Pidc_puesto = idc_puesto;
            AprobacionesCOM Componente = new AprobacionesCOM();
            DataSet ds = Componente.aprobaciones_pendientes(entidad);
            //meterlo a session
            Session.Add("TablaAprobacionPendiente", ds.Tables[0]);
            gridaprobacionespendientes.DataSource = ds.Tables[0];
            gridaprobacionespendientes.DataBind();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                lblTablaVacia.Visible = true;
            }
        }

        protected void gridaprobacionespendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(gridaprobacionespendientes.DataKeys[index].Values["idc_aprobacion_reg"].ToString());
            String pagina = gridaprobacionespendientes.DataKeys[index].Values["pagina"].ToString();
            String comentarios = gridaprobacionespendientes.DataKeys[index].Values["comentarios"].ToString();
            String descripcion = gridaprobacionespendientes.DataKeys[index].Values["descorta"].ToString();
            String nombre = gridaprobacionespendientes.DataKeys[index].Values["nombre"].ToString();
            String puesto = gridaprobacionespendientes.DataKeys[index].Values["des_puesto"].ToString();
            String fecha_movimiento = gridaprobacionespendientes.DataKeys[index].Values["fecha_movimiento"].ToString();
            String nombre_solic = gridaprobacionespendientes.DataKeys[index].Values["nombre_soli"].ToString();
            Session["sidc_puestoperfil_borr"] = gridaprobacionespendientes.DataKeys[index].Values["idc_registro"].ToString();
            String idc_aprobacion = gridaprobacionespendientes.DataKeys[index].Values["idc_aprobacion"].ToString();
            //llamamos la entidad
            Aprobaciones_solicitudE entidad = new Aprobaciones_solicitudE();
            DataSet ds = new DataSet();
            Aprobaciones_solicitudBL componente = new Aprobaciones_solicitudBL();
            //recuperamos el valor de la fila
            DataRow[] row = buscarFila(vidc);

            switch (e.CommandName)
            {
                case "aprobar":
                    if (row.Length > 0)
                    {
                        //
                        modal_ocidc_aprobacion_reg.Value = row[0]["idc_aprobacion_reg"].ToString();
                        oc_idc_aprobacion_soli.Value = row[0]["idc_aprobacion_soli"].ToString();
                    }
                    modal_ocaprobado.Value = "True";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Aprobar','');", true);
                    break;

                case "no_aprobar":
                    modal_ocidc_aprobacion_reg.Value = row[0]["idc_aprobacion_reg"].ToString();
                    oc_idc_aprobacion_soli.Value = row[0]["idc_aprobacion_soli"].ToString();
                    modal_ocaprobado.Value = "False";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('No aprobar','');", true);
                    break;

                case "detalle":
                    Response.Redirect(pagina);
                    break;

                case "Vista":
                    Response.Redirect(pagina);
                    break;

                case "firma_grupal":
                    modal_ocaprobado.Value = "False";
                    Session.Add("sidc_aprobacion_soli", row[0]["idc_aprobacion_soli"].ToString());
                    switch (idc_aprobacion)
                    {
                        case "1":
                            try
                            {
                                entidad.Idc_registro = vidc;
                                //llamamos al componente
                                ds = componente.CargarGrupos(entidad);
                                Repeater1.DataSource = ds.Tables[0];
                                Repeater1.DataBind();
                                lnktodo.Text = "Mostrar Toda la Información";
                                Repeater1.Visible = true;
                                Session["scomando"] = "aprobar perfil";
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ReturnGr('Mensaje del Sistema','¿Está seguro que desea autorizar este borrador? Seleccione la informacion que desea que sea publicada');", true);
                            }
                            catch (Exception ex)
                            {
                                Alert.ShowAlertError(ex.Message, this.Page);
                            }
                            break;

                        case "3":
                            entidad.Idc_registro = vidc;
                            //llamamos al componente
                            ds = componente.Cargarpp(entidad);
                            REPEATPERFILPUESTO.DataSource = ds.Tables[0];
                            REPEATPERFILPUESTO.DataBind();
                            lnktodo.Text = "Autorizar Todos";
                            REPEATPERFILPUESTO.Visible = true;
                            foreach (RepeaterItem item in REPEATPERFILPUESTO.Items)
                            {
                                int ID = Convert.ToInt32(gridaprobacionespendientes.DataKeys[index].Values["idc_registro"]);
                                LinkButton lnk = (LinkButton)item.FindControl("lnkgrupo");
                                if (ID.ToString() == lnk.CommandName)
                                {
                                    lnk.CssClass = "btn btn-success btn-block";
                                }
                            }
                            Session["scomando"] = "aprobar perfil-p";
                            if (ds.Tables[0].Rows.Count > 1)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ReturnGr('Mensaje del Sistema','Existen varias solicitudes para el perfil " + ds.Tables[0].Rows[0]["perfil"].ToString().ToUpper() + ". Seleccione los puestos que quiere relacionar a este perfil.');", true);
                            }
                            else
                            {
                                Response.Redirect("aprobaciones_firma.aspx");
                            }
                            break;

                        case "4":
                            entidad.Idc_registro = Convert.ToInt32(gridaprobacionespendientes.DataKeys[index].Values["idc_registro"]);
                            ds = componente.CargarSubProcesos(entidad);
                            Repeater3.DataSource = ds.Tables[0];
                            Repeater3.DataBind();
                            lnktodo.Text = "Autorizar Todo";
                            Repeater3.Visible = true;
                            Session["scomando"] = "aprobar proceso";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ReturnGr('Mensaje del Sistema','¿Está seguro que desea autorizar este borrador? Seleccione la informacion que desea que sea publicada');", true);

                            break;

                        default:
                            Response.Redirect("aprobaciones_firma.aspx");
                            break;
                    }
                    break;

                case "Comentarios":
                    lblNombreComentarios.Text = nombre;
                    lblPuestoComentarios.Text = puesto;
                    lblPuestoSolicito.Text = nombre_solic;
                    txtAprobacionComentarios.Text = (comentarios == "" ? "SIN COMENTARIOS" : comentarios);
                    txtDescripcionComentarios.Text = descripcion;
                    lblFecha.Text = (fecha_movimiento == null || fecha_movimiento == "" ? "AUN EN PROCESO" : fecha_movimiento);
                    CargarGrid_aprob_pendientes();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", " modal_comentarios();", true);
                    break;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Your Void() or Event()
            int id = Convert.ToInt32(Session["sidc_puestoperfil_borr"]);
            string comando = Session["scomando"].ToString();
            string res = "";
            int sidc_aprobacion_soli = Convert.ToInt32(Session["sidc_aprobacion_soli"]);
            switch (comando)
            {
                case "aprobar perfil":
                    //levantamos la solicitud.

                    res = solicitudAprobacion(Convert.ToInt32(Session["sidc_puestoperfil_borr"]), 1, sidc_aprobacion_soli);
                    if (TotalCadena(Repeater1) == 0 && lnktodo.CssClass == "btn btn-default btn-block")
                    {
                        Alert.ShowAlertError("Seleccione una opcion", this.Page);
                    }
                    else if (string.IsNullOrEmpty(res.Trim().Replace(" ", "")))
                    {
                        if (TotalCadena(Repeater1) > 0)
                        {
                            Response.Redirect("aprobaciones_firma.aspx?cadena=" + funciones.deTextoa64(Cadena(Repeater1)) + "&total=" + TotalCadena(Repeater1).ToString());
                        }
                        else
                        {
                            Response.Redirect("aprobaciones_firma.aspx?cadena=&total=" + TotalCadena(Repeater1).ToString());
                        }
                    }
                    else if (res != "")
                    {
                        Alert.ShowAlertError(res, this.Page);
                    }
                    break;

                case "aprobar perfil-p":
                    //levantamos la solicitud
                    res = solicitudAprobacion(Convert.ToInt32(Session["sidc_puestoperfil_borr"]), 3, sidc_aprobacion_soli);
                    if (TotalCadena(REPEATPERFILPUESTO) == 0 && lnktodo.CssClass == "btn btn-default btn-block")
                    {
                        Alert.ShowAlertError("Seleccione una opcion", this.Page);
                    }
                    else if (string.IsNullOrEmpty(res.Trim().Replace(" ", "")))
                    {
                        if (TotalCadena(REPEATPERFILPUESTO) > 0)
                        {
                            Response.Redirect("aprobaciones_firma.aspx?cadena=" + funciones.deTextoa64(Cadena(REPEATPERFILPUESTO)) + "&total=" + TotalCadena(REPEATPERFILPUESTO).ToString());
                        }
                        else
                        {
                            Response.Redirect("aprobaciones_firma.aspx?cadena=&total=" + TotalCadena(REPEATPERFILPUESTO).ToString());
                        }
                    }
                    else if (res != "")
                    {
                        Alert.ShowAlertError(res, this.Page);
                    }
                    break;

                case "aprobar proceso":
                    res = solicitudAprobacion(Convert.ToInt32(Session["sidc_puestoperfil_borr"]), 4, sidc_aprobacion_soli);
                    //levantamos la solicitud
                    if (TotalCadena(Repeater3) == 0 && lnktodo.CssClass == "btn btn-default btn-block")
                    {
                        Alert.ShowAlertError("Seleccione una opcion", this.Page);
                    }
                    else if (string.IsNullOrEmpty(res.Trim().Replace(" ", "")))
                    {
                        if (TotalCadena(Repeater3) > 0)
                        {
                            Response.Redirect("aprobaciones_firma.aspx?cadena=" + funciones.deTextoa64(Cadena(Repeater3)) + "&total=" + TotalCadena(Repeater3).ToString());
                        }
                        else
                        {
                            Response.Redirect("aprobaciones_firma.aspx?cadena=&total=" + TotalCadena(Repeater3).ToString());
                        }
                    }
                    else if (res != "")
                    {
                        Alert.ShowAlertError(res, this.Page);
                    }
                    break;
            }
            //reset a las sessiones
            Session["sidc_puestoperfil_borr"] = 0;
            Session["scomando"] = "";
        }

        private String Cadena(Repeater Repeater)
        {
            string cadena = "";
            if (lnktodo.CssClass != "btn btn-info btn-block")
            {
                foreach (RepeaterItem item in Repeater.Items)
                {
                    LinkButton lnk = (LinkButton)item.FindControl("lnkgrupo");
                    if (lnk.CssClass == "btn btn-success btn-block")
                    {
                        cadena = cadena + lnk.CommandName + ";";
                    }
                }
            }
            return cadena;
        }

        private int TotalCadena(Repeater Repeater)
        {
            int cadena = 0;
            if (lnktodo.CssClass != "btn btn-info btn-block")
            {
                foreach (RepeaterItem item in Repeater.Items)
                {
                    LinkButton lnk = (LinkButton)item.FindControl("lnkgrupo");
                    if (lnk.CssClass == "btn btn-success btn-block")
                    {
                        cadena = cadena + 1;
                    }
                }
            }
            return cadena;
        }

        protected void btnFirmar_Click(object sender, EventArgs e)
        {
            try
            {
                //recuperamos los valores
                int vidc_aprobacion_reg = Convert.ToInt32(modal_ocidc_aprobacion_reg.Value);
                string vusuario = Session["susuario"].ToString(); //MEDIANTE SESSION
                bool vaprobado = Convert.ToBoolean(modal_ocaprobado.Value);
                string vcontraseña = txtpass.Text.ToUpper();
                string vcomentarios = txtobs.Text;
                int Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                string Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                string Pnombrepc = funciones.GetPCName();//nombre pc usuario
                string Pusuariopc = funciones.GetUserName();//usuario pc
                //llamamos al componente
                AprobacionesCOM componente = new AprobacionesCOM();
                DataSet ds = new DataSet();
                ds = componente.validar_firma(vusuario, vcontraseña, vaprobado, vidc_aprobacion_reg, vcomentarios, Idc_usuario, Pdirecip, Pnombrepc, Pusuariopc, "", 0);
                //mesaje del sp
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();

                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    //refrescamos el grid
                    CargarGrid_aprob_pendientes();
                    limpiarModal();

                    Alert.ShowAlert("Movimiento Correcto", "Mensaje importante", this.Page);
                }
                else
                {
                    limpiarModal();
                    Alert.ShowAlertError(vmensaje, this.Page);
                    return;
                }
            }
            catch (Exception ex)
            {
                //msgbox.show(ex.Message, this.Page);
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        protected void limpiarModal()
        {
            modal_ocaprobado.Value = "";
            modal_ocidc_aprobacion_reg.Value = "0";
            oc_idc_aprobacion_soli.Value = "0";
            txtpass.Text = "";
            txtobs.Text = "";
        }

        protected DataRow[] buscarFila(int primarykey)
        {
            //bajamos la tabla de session
            DataTable tbl_aprobaciones_pend = (DataTable)Session["TablaAprobacionPendiente"];
            DataRow[] fila = tbl_aprobaciones_pend.Select("idc_aprobacion_reg=" + primarykey);

            return fila;
        }

        protected void No_Click(object sender, EventArgs e)
        {
            limpiarModal();
        }

        protected void gridaprobacionespendientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //revisar el campo aprobado
            //si es null darle opciones para firmar, en caso que ya lo hizo bloquear las opciones y poner la leyenda de aprobado o no aprobado

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView fila = (DataRowView)e.Row.DataItem;
                //boton que usamos como mensaje
                ImageButton btnaprobar = (ImageButton)e.Row.FindControl("imgbtnaprobar");
                ImageButton btn_noaprobar = (ImageButton)e.Row.FindControl("imgbtn_noaprobar");
                Button lblestat = (Button)e.Row.FindControl("lblestatus");
                //evaluamos la columna aprobado
                string color = fila["color"].ToString();
                e.Row.Cells[0].BackColor = Color.FromName(color);
                if (Convert.ToBoolean(fila["aprobado"]) == false && fila["comentarios"].ToString() == "")
                { //permite aprobar o no aprobar, el mensaje es pendiente
                    btnaprobar.Visible = true;
                    btn_noaprobar.Visible = true;
                    //
                    lblestat.Text = "Pendiente";
                    lblestat.CssClass = "btn btn-warning";
                }
                if (Convert.ToBoolean(fila["aprobado"]) == true)
                { //se bloquea las opciones y el mensaje es aprobado en verde
                    btnaprobar.Visible = false;
                    btn_noaprobar.Visible = false;
                    //
                    lblestat.Text = "Aprobado";
                    lblestat.CssClass = "btn btn-success";
                }
                if (Convert.ToBoolean(fila["aprobado"]) == false && fila["comentarios"].ToString() != "")
                { //quiere decir que no esta aprobado se bloquea las opciones y el mensaje en rojo
                    btnaprobar.Visible = false;
                    btn_noaprobar.Visible = false;
                    //
                    lblestat.Text = "No Aprobado";
                    lblestat.CssClass = "btn btn-danger";
                }

                Button salajuntas = (Button)e.Row.FindControl("btnfirmagrupal");
                salajuntas.CssClass = "btn btn-success";
                salajuntas.CommandName = "firma_grupal";
            }
        }

        protected void lnktodo_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            lnk.CssClass = lnk.CssClass == "btn btn-default btn-block" ? "btn btn-info btn-block" : "btn btn-default btn-block";
            Repeater1.Visible = lnk.CssClass == "btn btn-default btn-block" ? true : false;
            REPEATPERFILPUESTO.Visible = lnk.CssClass == "btn btn-default btn-block" ? true : false;
            Repeater3.Visible = lnk.CssClass == "btn btn-default btn-block" ? true : false;
        }

        protected void lnkgrupo_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            lnk.CssClass = lnk.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
        }

        protected string solicitudAprobacion(int id_row, int idc_aprobacion, int idc_aprobacion_soli)
        {
            try
            {
                //recuperar el valor del datatable
                //ALERTA REVISAR ESTA SESISON
                Session.Add("sidc_aprobacion_soli", idc_aprobacion_soli);
                //SI NO TIENE SOLICITUD SE CREA
                if (idc_aprobacion_soli == 0)
                { //proseguimos a insertar la solicitud
                  //nevesitamos mandar los sig datos
                    int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString()); //que usuario hace la solicitud
                    int idc_registro = id_row; // el id del registro que se quiere aprobar EN ESTE CASO EL ID DEL BORRADOR

                    //llamamos la entidad
                    Aprobaciones_solicitudE entidad = new Aprobaciones_solicitudE();
                    entidad.Idc_aprobacion = idc_aprobacion;
                    entidad.Idc_usuario = idc_usuario;
                    entidad.Idc_registro = idc_registro;
                    entidad.Idc_aprobacion_soli = idc_aprobacion_soli; // SE MANDA CERO NO PASA NADA
                                                                       //llamamos al componente
                    DataSet ds = new DataSet();
                    Aprobaciones_solicitudBL componente = new Aprobaciones_solicitudBL();
                    ds = componente.nueva_solicitud_aprobacion(entidad, idc_registro);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    int vfolio = Convert.ToInt32(ds.Tables[0].Rows[0]["folio"].ToString()); //id de la solicitud de aprobacion
                                                                                            //ALERTA REVISAR ESTA SESISON
                    Session.Add("sidc_aprobacion_soli", vfolio);

                    return vmensaje;// +" " + vmensaje_no2;
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
    }
}