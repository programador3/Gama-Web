using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class catalogo_tareas_servicios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                ViewState["idc_tareaser"] = null;
                TareasServicios(0);
            }
        }
        public void TareasServicios(int idc_puesto)
        {
            try
            {
                TareasCOM componente = new TareasCOM();
                DataSet ds = componente.sp_tareas_servicios_puestos(idc_puesto);
                DataTable dt = ds.Tables[0];
                gridservicios.DataSource = dt;
                gridservicios.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void gridservicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_tareaser = Convert.ToInt32(gridservicios.DataKeys[index].Values["idc_tareaser"].ToString());
            ViewState["idc_tareaser"] = idc_tareaser;
            switch (e.CommandName)
            {
                case "Editar":
                    Session["Caso_Confirmacion"] = e.CommandName;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Desea Editar el Servicio "+ gridservicios.DataKeys[index].Values["descripcion"].ToString() + "?','modal fade modal-info');", true);

                    break;
                case "Borrar":
                    Session["Caso_Confirmacion"] = e.CommandName;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Desea Eliminar el Servicio " + gridservicios.DataKeys[index].Values["descripcion"].ToString() + "?','modal fade modal-info');", true);

                    break;
                case "Puestos":

                    Response.Redirect("tareas_servicios_captura.aspx?solo_lista=KNWODBWODBWOEBDOWDOWKDBOWEKDBEWBDOWEPOP&idc_tareaser=" + funciones.deTextoa64(Convert.ToInt32(ViewState["idc_tareaser"]).ToString().Trim()));
                    break;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso = (string)Session["Caso_Confirmacion"];
            switch (caso)
            {
                case "Editar":
                    Response.Redirect("tareas_servicios_captura.aspx?idc_tareaser="+funciones.deTextoa64(Convert.ToInt32(ViewState["idc_tareaser"]).ToString().Trim()));
                    break;
                case "Borrar":
                    try
                    {
                        TareasCOM componente = new TareasCOM();
                        DataSet ds = componente.sp_etareas_servicios(Convert.ToInt32(ViewState["idc_tareaser"]), Convert.ToInt32(Session["sidc_usuario"]),
                            funciones.GetLocalIPAddress(), funciones.GetPCName(), funciones.GetUserName());
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos procesando la solicitud.", "Espere un Momento", "catalogo_tareas_servicios.aspx", "imagenes/loading.gif", "1000", "Servicio Eliminado Correctamente", this);
                        }
                        else {
                            Alert.ShowAlertError(vmensaje,this);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.ToString(), this.Page);
                        Global.CreateFileError(ex.ToString(), this);
                    }
                    break;
            }
        }
    }
}