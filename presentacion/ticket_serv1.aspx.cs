using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;

namespace presentacion
{
    public partial class ticket_serv1 : System.Web.UI.Page
    {
        private ticket_servCOM com = new ticket_servCOM();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["all"] == null)
                {
                    grids.Visible = true;
                    sidc_puesto_h.Value = Session["sidc_puesto_login"].ToString();
                    Cargar_Grid(Convert.ToInt32(Session["sidc_puesto_login"]));
                }
            }

        }

        private void Cargar_Grid(int idpuesto)
        {
            ticket_servENT ent = new ticket_servENT();
            ent.Pidc_puesto = idpuesto;
            ent.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"]);

            DataSet ds = com.ticket_serv(ent);
            grid_E.DataSource = ds.Tables[0];
            grid_E.DataBind();
            if (ds.Tables[0].Rows.Count == 0) { NO_Hay_E.Visible = true; }
            grid_A.DataSource = ds.Tables[1];
            grid_A.DataBind();
            if (ds.Tables[1].Rows.Count == 0) { NO_Hay_A.Visible = true; }
        }

        private void Cargar_ddlUsuarios(int idpuesto)
        {
            ticket_servENT ent = new ticket_servENT();
            ent.Pidc_puesto = idpuesto;
            DataSet ds = com.Usuarios_puesto(ent);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlUsuarios.DataValueField = "idc_usuario";
                ddlUsuarios.DataTextField = "usuario";

                ddlUsuarios.DataSource = ds.Tables[0];
                ddlUsuarios.DataBind();
                ddlUsuarios.Items.Insert(0, new ListItem("--Elige un usuario", "0"));
            }
            else
            {
                ddlUsuarios.Items.Insert(0, new ListItem("--No Existen usuarios para este Puesto", "0"));
            }
        }

        protected void grid_E_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            //idc_ticketserv,idc_tareaser,descripcion
            idc_ticketserv_h.Value = grid_E.DataKeys[index].Values["idc_ticketserv"].ToString();
            idc_tareaser_h.Value = grid_E.DataKeys[index].Values["idc_tareaser"].ToString();
            string descripcion = grid_E.DataKeys[index].Values["descripcion"].ToString();
            descripcion_h.Value = descripcion;

            string ARCHIVO = grid_E.DataKeys[index].Values["ARCHIVO"].ToString();
            lblDescr.Text = descripcion;
            lblObser.Text = grid_E.DataKeys[index].Values["observaciones"].ToString();
            lblFecha.Text = grid_E.DataKeys[index].Values["fecha"].ToString();
            lblEmple.Text = grid_E.DataKeys[index].Values["empleado"].ToString();
            lblDepto.Text = grid_E.DataKeys[index].Values["DEPTO"].ToString();
            lblempleaten.Text = grid_E.DataKeys[index].Values["EMPLEADO_ATIENDE"].ToString(); 
            lblat.Visible = false;
            lblAten.Visible = false;
            yes.Visible = true;
            Session["Caso_Confirmacion"] = e.CommandName.ToString();
            switch (e.CommandName)
            {
                case "Descargar":
                    System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(Server.MapPath("~/temp/files/"));//path local
                    string Domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host;
                    string extension = Path.GetExtension(ARCHIVO);
                    string pageName = HttpContext.Current.Request.ApplicationPath + "/";
                    if (extension.ToUpper() == ".PDF" || extension.ToUpper() == ".JPG")
                    {
                        File.Copy(ARCHIVO, dirInfo + Path.GetFileName(ARCHIVO), true);
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "window.open('" + pageName + "temp/files/" + Path.GetFileName(ARCHIVO) + "');", true);
                    }
                    else
                    {
                        funciones.Download(ARCHIVO, System.IO.Path.GetFileName(ARCHIVO), this);
                    }
                    break;
                case "Atender":
                    div2.Visible = false;


                    string str_Alert;
                    str_Alert = string.Format("ModalConfirm('Mensaje del Sistema','¿Esta seguro  de atender el siguiente Ticket: {0}?');", descripcion);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", str_Alert, true);
                    break;

                case "Cancelar":
                    div2.Visible = true;
                    div_pass.Visible = false;

                    txtDescripcion.Text = "";
                    lblDescripcion.Text = "Motivo";
                    string str_modal;
                    str_modal = string.Format("ModalConfirm('Mensaje del Sistema','¿Esta seguro de Cancelar el Ticket:  {0}? ');", descripcion);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", str_modal, true);
                    break;
                case "preview":
                    div2.Visible = false;
                    div_pass.Visible = false;
                    yes.Visible = false;
                    string str_modal2 = string.Format("ModalConfirm('Mensaje del Sistema','Información Principal del Ticket');", descripcion);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", str_modal2, true);
                    break;
            }

            Cargar_Grid(Convert.ToInt32(Session["sidc_puesto_login"]));
        }

        protected void grid_A_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            idc_ticketserv_h.Value = grid_A.DataKeys[index].Values["idc_ticketserv"].ToString();
            idc_ticketserva_h.Value = grid_A.DataKeys[index].Values["idc_ticketserva"].ToString();
            idc_tareaser_h.Value = grid_A.DataKeys[index].Values["idc_tareaser"].ToString();
            string descripcion  = grid_A.DataKeys[index].Values["descripcion"].ToString();
            string ARCHIVO = grid_A.DataKeys[index].Values["ARCHIVO"].ToString();
            descripcion_h.Value = descripcion;
            idc_usuario_rep_h.Value =grid_A.DataKeys[index].Values["idc_usuario"].ToString();           
            lblDescr.Text = descripcion;
            lblObser.Text = grid_A.DataKeys[index].Values["observaciones"].ToString();
            lblFecha.Text = grid_A.DataKeys[index].Values["fecha"].ToString();
            lblEmple.Text = grid_A.DataKeys[index].Values["empleado"].ToString();
            lblDepto.Text = grid_A.DataKeys[index].Values["DEPTO"].ToString();
            lblempleaten.Text = grid_A.DataKeys[index].Values["EMPLEADO_ATIENDE"].ToString();
            //grid_A.DataKeys[index].Values["idc_puesto"].ToString();
            lblAten.Visible = true;
            yes.Visible = true;
            Session["Caso_Confirmacion"] = e.CommandName.ToString();
            switch (e.CommandName)
            {
                case "Descargar":
                    System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(Server.MapPath("~/temp/files/"));//path local
                    string Domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host;
                    string extension = Path.GetExtension(ARCHIVO);
                    string pageName = HttpContext.Current.Request.ApplicationPath + "/";
                    if (extension == ".PDF" || extension == ".JPG")
                    {
                        File.Copy(ARCHIVO, dirInfo + Path.GetFileName(ARCHIVO), true);
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "window.open('" + pageName + "temp/files/" + Path.GetFileName(ARCHIVO) + "');", true);
                    }
                    else
                    {
                        funciones.Download(ARCHIVO, System.IO.Path.GetFileName(ARCHIVO), this);
                    }
                    break;
                case "Terminar":
                    div2.Visible = true;
                    div_pass.Visible = (idc_usuario_rep_h.Value != Session["sidc_usuario"].ToString());
                    //div_detalles.Visible = false;
                    Cargar_ddlUsuarios(Convert.ToInt32(grid_A.DataKeys[index].Values["idc_puesto"].ToString()));

                    txtDescripcion.Text = "";
                    lblDescripcion.Text = "Observaciones";
                    string str_Alert;
                    str_Alert = string.Format("ModalConfirm('Mensaje del Sistema','¿Esta seguro  de terminar el siguiente Ticket: {0}?');", descripcion);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", str_Alert, true);
                    break;

                case "Aten_Cancelar":
                    div2.Visible = true;
                    div_pass.Visible = false;

                    txtDescripcion.Text = "";
                    lblDescripcion.Text = "Motivo";

                    string str_modal = string.Format("ModalConfirm('Mensaje del Sistema','¿Esta seguro de Cancelar el Ticket:  {0}? ');", descripcion);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", str_modal, true);
                    break;
                case "preview":
                    div2.Visible = false;
                    div_pass.Visible = false;
                    yes.Visible = false;
                    string str_modal2 = string.Format("ModalConfirm('Mensaje del Sistema','Información Principal del Ticket');", descripcion);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", str_modal2, true);
                    break;
            }

            Cargar_Grid(Convert.ToInt32(Session["sidc_puesto_login"]));
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                
                string caso = Session["Caso_Confirmacion"].ToString();
                ticket_servENT ent = new ticket_servENT();
                Datos_Usuario_logENT dul = new Datos_Usuario_logENT();

                dul.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                dul.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                dul.Pusuariopc = funciones.GetUserName();//usuario pc
                dul.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                ent.Pmotivo = "";
                DataSet ds = new DataSet();
                switch (caso)
                {
                    case "Atender":
                        ent.Pidc_ticketserv = Convert.ToInt32(idc_ticketserv_h.Value);
                        ent.Pidc_puesto = Convert.ToInt32(sidc_puesto_h.Value);
                        ds = com.ticket_serv_Espera(ent, dul);
                        break;

                    case "Cancelar":
                        ent.Pidc_ticketserv = Convert.ToInt32(idc_ticketserv_h.Value);
                        ent.Pidc_puesto = Convert.ToInt32(sidc_puesto_h.Value);
                        ent.Pmotivo = txtDescripcion.Text;
                        if (txtDescripcion.Text == "" || txtDescripcion.Text.Length < 10)
                        {
                            Alert.ShowAlertInfo("AGREGA EL MOTIVO DE LA CANCELACION DEL TICKET DE SERVICIO \n(Minimo 10 caracteres", "Mensaje del Sistema", this);
                            return;
                        }
                        ds = com.ticket_serv_Espera(ent, dul);
                        break;

                    case "Terminar":

                        if (ddlUsuarios.SelectedValue == "0" && idc_usuario_rep_h.Value != Session["sidc_usuario"].ToString())
                        {
                            Alert.ShowAlertInfo("ELIGE UN USUARIO", "Mensaje del Sistema", this);
                            return;
                        }
                        ent.Pidc_usuario_term = (idc_usuario_rep_h.Value != Session["sidc_usuario"].ToString()) ? Convert.ToInt32(ddlUsuarios.SelectedValue) : Convert.ToInt32(idc_usuario_rep_h.Value);
                        ent.Pcontraseña = txtContraseña.Text;
                        ent.Pidc_ticketserva = Convert.ToInt32(idc_ticketserva_h.Value);
                        ent.Pidc_puesto = Convert.ToInt32(sidc_puesto_h.Value);
                        ent.Pobservaciones = txtDescripcion.Text;
                        ds = com.ticket_serv_aten(ent, dul);
                        break;

                    case "Aten_Cancelar":
                        if (txtDescripcion.Text == "" || txtDescripcion.Text.Length < 10)
                        {
                            Alert.ShowAlertInfo("AGREGA EL MOTIVO DE LA CANCELACION DEL TICKET DE SERVICIO ATENDIDO \n(Minimo 10 caracteres)", "Mensaje del Sistema", this);
                            return;
                        }
                        ent.Pidc_ticketserva = Convert.ToInt32(idc_ticketserva_h.Value);
                        ent.Pidc_puesto = Convert.ToInt32(sidc_puesto_h.Value);
                        ent.Pmotivo = txtDescripcion.Text;
                        ent.Pobservaciones = "";
                        ent.Pcontraseña = "";
                        ent.Pidc_usuario_term = 0;
                        ds = com.ticket_serv_aten(ent, dul);
                        break;
                }

                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos procesando la solicitud.", "Espere un Momento", "ticket_serv.aspx", "imagenes/loading.gif", "1000", "Información Procesada Correctamente", this);
                }
                else
                {
                    string strScript = "AlertGO('" + vmensaje + "','ticket_serv.aspx');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", strScript, true);
                    // Alert.ShowAlertInfo(vmensaje, "Mensaje del Sistema", this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }

        }

        protected void grid_E_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                bool APLICA = Convert.ToBoolean(rowView["APLICA"]);
                string ARCHIVO = rowView["ARCHIVO"].ToString();
                if (!APLICA)
                {
                    e.Row.Cells[0].Controls.Clear();
                    e.Row.Cells[1].Controls.Clear();
                }
                if (ARCHIVO == "")
                {
                    e.Row.Cells[2].Controls.Clear();
                }
            }
        }

        protected void grid_A_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                bool APLICA = Convert.ToBoolean(rowView["APLICA"]);
                string ARCHIVO = rowView["ARCHIVO"].ToString();
                if (!APLICA)
                {
                    e.Row.Cells[0].Controls.Clear();
                    e.Row.Cells[1].Controls.Clear();
                }
                if (ARCHIVO == "")
                {
                    e.Row.Cells[2].Controls.Clear();
                }
            }
        }
    }
}