using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class catalogo_cuestionarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["title_page"] = "Catalogo Cuestionarios";
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            CargarDatos();
        }

        /// <summary>
        /// Carga los datos de la tabla
        /// </summary>
        private void CargarDatos()
        {
            CuestionariosENT entidad = new CuestionariosENT();
            CuestionariosCOM comp = new CuestionariosCOM();
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            DataSet ds = comp.CargaCuestionarios(entidad);
            gridAsignacion.DataSource = ds.Tables[0];
            gridAsignacion.DataBind();
        }

        protected void gridAsignacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_cuestionario"] = Convert.ToInt32(gridAsignacion.DataKeys[index].Values["idc_cuestionario"].ToString());
            Session["descripcion"] = gridAsignacion.DataKeys[index].Values["descripcion"].ToString();
            switch (e.CommandName)
            {
                case "Editar":
                    Session["Caso_Confirmacion"] = "Editar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Editar el Cuestionario " + gridAsignacion.DataKeys[index].Values["descripcion"].ToString() + "?','modal fade modal-info');", true);
                    break;

                case "Eliminar":
                    Session["Caso_Confirmacion"] = "Eliminar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta seguro de Eliminar el Cuestionario " + gridAsignacion.DataKeys[index].Values["descripcion"].ToString() + "?','modal fade modal-info');", true);
                    break;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso_confirm = (string)Session["Caso_Confirmacion"];
            try
            {
                CuestionariosENT entidad = new CuestionariosENT();
                CuestionariosCOM comp = new CuestionariosCOM();
                entidad.Pidc_cuestionario = Convert.ToInt32(Session["idc_cuestionario"]);
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = new DataSet();
                string VMENSAJE = "";
                switch (caso_confirm)
                {
                    case "Eliminar":
                        ds = comp.EliminarCuestionario(entidad);
                        VMENSAJE = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Editar":
                        string idc_cuestionario = funciones.deTextoa64(Convert.ToInt32(Session["idc_cuestionario"]).ToString());
                        Response.Redirect("cuestionarios_captura.aspx?idc_cuestionario=" + idc_cuestionario);
                        break;
                }
                if (VMENSAJE == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('El cuestionario " + (string)Session["descripcion"] + " fue eliminado correctamente.','catalogo_cuestionarios.aspx');", true);
                }
                else {
                    Alert.ShowAlertError(VMENSAJE, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }
    }
}