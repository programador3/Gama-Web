using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class catalogo_examenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack) { Session["idc_examen"] = null; }
            CargarGridPrincipal();
        }

        /// <summary>
        /// Carga los datos en repeats
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal()
        {
            ExamenesENT entidad = new ExamenesENT();
            ExamenesCOM componente = new ExamenesCOM();
            DataSet ds = componente.CargaExamenes(entidad);
            gridAsignacion.DataSource = ds.Tables[0];
            gridAsignacion.DataBind();
            if (ds.Tables[0].Rows.Count == 0) { NOHay.Visible = true; }
        }

        protected void gridAsignacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_examen"] = Convert.ToInt32(gridAsignacion.DataKeys[index].Values["idc_examen"].ToString());

            switch (e.CommandName)
            {
                case "Editar":
                    Session["Caso_Confirmacion"] = "Editar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Editar el examen " + gridAsignacion.DataKeys[index].Values["descripcion"].ToString() + "?');", true);
                    break;

                case "Eliminar":
                    Session["Caso_Confirmacion"] = "Eliminar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta seguro de Eliminar el examen " + gridAsignacion.DataKeys[index].Values["descripcion"].ToString() + "?');", true);
                    break;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Editar":
                    Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();

                    Response.Redirect("examenes_captura.aspx?edit=true");
                    break;

                case "Eliminar":
                    ExamenesENT entidad = new ExamenesENT();
                    ExamenesCOM componente = new ExamenesCOM();
                    entidad.Pidc_examen = Convert.ToInt32(Session["idc_examen"].ToString());
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    DataSet ds = componente.BorrarExamen(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('Examen Eliminado Correctamente.','catalogo_examenes.aspx');", true);
                    }
                    else
                    {
                        Alert.ShowAlertError(mensaje, this);
                    }
                    break;
            }
        }
    }
}