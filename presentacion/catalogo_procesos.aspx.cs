using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class catalogo_procesos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["url"] = null;
                Session["idc_proceso"] = null;
                Session["type"] = "B";
                CargarProcesos("B");
            }
        }

        private void CargarProcesos(string tipo)
        {
            try
            {
                ProcesosENT enti = new ProcesosENT();
                enti.Ptioo = tipo;
                enti.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                ProcesosCOM com = new ProcesosCOM();
                DataSet ds = tipo == "B" ? com.CatalogoProcesosbORR(enti) : com.CatalogoProcesos(enti);
                gridprocesos.DataSource = ds.Tables[0];
                gridprocesos.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void gridprocesos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string idc_proceso = gridprocesos.DataKeys[index].Values["idc_proceso"].ToString();
            string IDC_proceso_borr = gridprocesos.DataKeys[index].Values["idc_proceso_borr"].ToString();
            string descripcion = gridprocesos.DataKeys[index].Values["descripcion"].ToString();
            string tipo = lnkborrador.Visible == true ? "P" : "B";
            int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
            PanelNuevoBorrador.Visible = false;
            Yes.Visible = true;
            btnSinLigar.Visible = false;
            btnGuardarSinLigar.Visible = false;
            btncrearborrador.Visible = false;
            Yes.Text = "Si";
            txtNombrePerfil.Text = "";
            switch (e.CommandName)
            {
                case "preview":
                    Session["Caso_Confirmacion"] = "preview";
                    Session["url"] = tipo == "P" ? "subprocesos_captura.aspx?preview=VSBIUVSBXOJQWSBXOIWJSBXIWJSBXOIJBQSIOXBQIXSBQSX&type=" + tipo + "&idc_proceso=" + funciones.deTextoa64(idc_proceso) : "subprocesos_captura.aspx?preview=VSBIUVSBXOJQWSBXOIWJSBXIWJSBXOIJBQSIOXBQIXSBQSX&type=" + tipo + "&idc_proceso=" + funciones.deTextoa64(IDC_proceso_borr);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Visualizar el Manual de Procesos " + descripcion + "','modal fade modal-info');", true);
                    break;

                case "Editar":
                    Session["Caso_Confirmacion"] = "Editar";
                    Session["idc_proceso"] = tipo == "P" ? idc_proceso : IDC_proceso_borr;
                    Session["url"] = tipo == "P" ? "subprocesos_captura.aspx?type=" + tipo + "&idc_proceso=" + funciones.deTextoa64(idc_proceso) : "subprocesos_captura.aspx?type=" + tipo + "&idc_proceso=" + funciones.deTextoa64(IDC_proceso_borr);
                    if (tipo == "B")
                    {
                        btnSinLigar.Visible = true;
                        Yes.Text = "Editar Este Perfil";
                    }
                    if (tipo == "P")
                    {
                        btncrearborrador.Visible = true;
                        btnSinLigar.Visible = true;
                        Yes.Text = "Editar Este Perfil";
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea editar " + descripcion + "? Tambien puede crear un Nuevo borrador sin estar ligado a este Manual.','modal fade modal-info');", true);
                    break;

                case "Eliminar":
                    Session["idc_proceso"] = tipo == "P" ? idc_proceso : IDC_proceso_borr;
                    Session["Caso_Confirmacion"] = "Eliminar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Eliminar el Manual de Procesos " + descripcion + "','modal fade modal-info');", true);
                    break;

                case "Solicitar":
                    if (tipo == "B")
                    {
                        Session["idc_proceso"] = tipo == "P" ? idc_proceso : IDC_proceso_borr;
                        Session["Caso_Confirmacion"] = "Solicitar";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Solicitar la Autorizacion el Manual de Procesos " + descripcion + "','modal fade modal-info');", true);
                    }
                    else
                    {
                        CargarProcesos(tipo);
                    }
                    break;
            }
            CargarProcesos(tipo);
        }

        protected void lnkborrador_Click(object sender, EventArgs e)
        {
            lnkproduccion.Visible = true;
            lnkborrador.Visible = false;
            lnltipo.CssClass = "btn btn-primary";
            lnltipo.Text = "Tipo Borrador";
            CargarProcesos("B");
            Session["type"] = "B";
            lbknuevoprocesos.Visible = true;
        }

        protected void lnkproduccion_Click(object sender, EventArgs e)
        {
            lnkproduccion.Visible = false;
            lnkborrador.Visible = true;
            lnltipo.CssClass = "btn btn-success";
            lnltipo.Text = "Tipo Produccion";
            Session["type"] = "P";
            CargarProcesos("P");
            int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
            lbknuevoprocesos.Visible = funciones.autorizacion(idc_usuario, 368);
        }

        protected void lbknuevoprocesos_Click(object sender, EventArgs e)
        {
            string tipo = lnkborrador.Visible == true ? "P" : "B";
            Response.Redirect("subprocesos_captura.aspx?type=" + tipo + "&idc_proceso=" + funciones.deTextoa64(0.ToString()));
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string value = (string)Session["Caso_Confirmacion"];
            try
            {
                ProcesosENT entidad = new ProcesosENT();
                ProcesosCOM com = new ProcesosCOM();
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pidc_proceso = Convert.ToInt32(Session["idc_proceso"]);
                entidad.Pborrador = lnkproduccion.Visible;
                string url = Session["url"] as string;
                DataSet ds = new DataSet();
                string vmensaje = "";
                switch (value)
                {
                    case "Eliminar":
                        ds = com.EliminarProcesos(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Editar":
                        Response.Redirect(url);
                        break;

                    case "preview":
                        Response.Redirect(url);
                        break;

                    case "Solicitar":
                        ds = com.SolictarProcesos(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;
                }
                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos procesando la SOLICITUD.", "Espere un Momento", "catalogo_procesos.aspx", "imagenes/loading.gif", "2000", "Proceso eliminado correctamente ", this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void btnGuardarSinLigar_Click(object sender, EventArgs e)
        {
            //borrado/prod - borrador sin ligar
            try
            {
                if (txtNombrePerfil.Text == "") { lblerror.Visible = true; }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                    CrearProduccionBorrador();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
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

        protected void gridprocesos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                if (Convert.ToBoolean(rowView["pendiente"]) == true)
                {
                    //nuevo_registro.ImageUrl = "imagenes/btn/new_register.png";
                    e.Row.Cells[gridprocesos.Columns.Count - 1].Controls.Clear();
                    string tipo = Session["type"] as String;
                    if (tipo == "B")
                    {
                        e.Row.Cells[0].Controls.Clear();
                        e.Row.Cells[1].Controls.Clear();
                    }
                    if (tipo == "P" && funciones.autorizacion(idc_usuario, 369) == false)//PRODUCCION SIN PERMISOS
                    {
                        e.Row.Cells[0].Controls.Clear();
                        e.Row.Cells[1].Controls.Clear();
                    }
                }
            }
        }

        protected void btncrearborrador_Click(object sender, EventArgs e)
        {
            CrearProduccionBorrador();
        }

        /// <summary>
        /// Crea un borrado de produccion
        /// </summary>
        private void CrearProduccionBorrador()
        {
            //produccion a borrador ligado
            try
            {
                ProcesosENT entidad = new ProcesosENT();
                ProcesosCOM com = new ProcesosCOM();
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pidc_proceso = Convert.ToInt32(Session["idc_proceso"]);
                entidad.Pdescripcion = txtNombrePerfil.Text;
                DataSet ds = new DataSet();
                string vmensaje = "";
                string tipo = Session["type"] as String;
                ds = tipo == "P" ? com.ProduccionaBorrador(entidad) : com.BorradorBorrador(entidad);
                vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje == "")
                {
                    string id = ds.Tables[0].Rows[0]["idc_cons"].ToString();
                    DataTable tabla_archivos = ds.Tables[1];
                    bool correct = true;
                    foreach (DataRow row_archi in tabla_archivos.Rows)
                    {
                        string ruta_det = row_archi["ruta_destino"].ToString();
                        string ruta_origen = row_archi["ruta_origen"].ToString();
                        correct = funciones.CopiarArchivos(ruta_origen, ruta_det, this.Page);
                        if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo " + ruta_origen + "a la ruta " + ruta_det, this); }
                    }
                    int total = (((tabla_archivos.Rows.Count) * 1) + 1) * 1000;
                    string t = total.ToString();
                    string url_back = "subprocesos_captura.aspx?type=B&idc_proceso=" + funciones.deTextoa64(id);
                    Alert.ShowGiftMessage("Estamos procesando la cantidad de " + tabla_archivos.Rows.Count.ToString() + " archivo(s) al Servidor.", "Espere un Momento", url_back, "imagenes/loading.gif", t, "Borrador Creado Correctamente", this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
    }
}