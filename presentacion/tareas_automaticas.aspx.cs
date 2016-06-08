using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class tareas_automaticas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["idc_tarea_auto"] = null;
                Cargatarea();
            }
        }

        /// <summary>
        /// Carga Puestos en Filtro
        /// </summary>
        public void Cargatarea()
        {
            try
            {
                TareasAutomaticasENT entidad = new TareasAutomaticasENT();
                TareasAutomaticasCOM componente = new TareasAutomaticasCOM();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = componente.CargaTareas(entidad);
                gridAsignacion.DataSource = ds.Tables[0];
                gridAsignacion.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Session["Error_Mensaje"] = ex.ToString();
            }
        }

        protected void gridAsignacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_tarea_auto"] = Convert.ToInt32(gridAsignacion.DataKeys[index].Values["idc_tarea_auto"].ToString());
            Session["descripcion"] = gridAsignacion.DataKeys[index].Values["descripcion"].ToString();
            switch (e.CommandName)
            {
                case "Editar":
                    Session["Caso_Confirmacion"] = "Editar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Editar la tarea " + gridAsignacion.DataKeys[index].Values["descripcion"].ToString() + "?','modal fade modal-info');", true);
                    break;

                case "Eliminar":
                    Session["Caso_Confirmacion"] = "Eliminar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta seguro de Eliminar la tarea " + gridAsignacion.DataKeys[index].Values["descripcion"].ToString() + "?','modal fade modal-info');", true);
                    break;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso_confirm = (string)Session["Caso_Confirmacion"];
            try
            {
                TareasAutomaticasENT entidad = new TareasAutomaticasENT();
                TareasAutomaticasCOM componente = new TareasAutomaticasCOM();
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pidc_tarea_auto = Convert.ToInt32(Session["idc_tarea_auto"]);
                DataSet ds = new DataSet();
                string VMENSAJE = "";
                switch (caso_confirm)
                {
                    case "Eliminar":
                        ds = componente.EliminarTarea(entidad);
                        VMENSAJE = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Editar":
                        string idc_tarea_auto = funciones.deTextoa64(Convert.ToInt32(Session["idc_tarea_auto"]).ToString());
                        Response.Redirect("tareas_automaticas_captura.aspx?idc_tarea_auto=" + idc_tarea_auto);
                        break;
                }
                if (VMENSAJE == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La Tarea " + (string)Session["descripcion"] + " fue eliminadA correctamente.','tareas_automaticas.aspx');", true);
                }
                else
                {
                    Alert.ShowAlertError(VMENSAJE, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Session["Error_Mensaje"] = ex.ToString();
            }
        }
    }
}