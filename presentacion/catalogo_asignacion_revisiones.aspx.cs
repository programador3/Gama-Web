using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class catalogo_asignacion_revisiones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
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
            Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
            Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
            DataSet ds = componente.CargaAsignacion(entidad);
            gridAsignacion.DataSource = ds.Tables[0];
            gridAsignacion.DataBind();
            if (ds.Tables[0].Rows.Count == 0) { NOHay.Visible = true; }
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        protected void gridAsignacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_clasifrev_edit"] = Convert.ToInt32(gridAsignacion.DataKeys[index].Values["idc_clasifrev"].ToString());

            switch (e.CommandName)
            {
                case "Editar":
                    Session["Caso_Confirmacion"] = "Editar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Editar la Asignacion " + gridAsignacion.DataKeys[index].Values["tipo_rev"].ToString() + "?');", true);
                    break;

                case "Eliminar":
                    Session["Caso_Confirmacion"] = "Eliminar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta seguro de Eliminar la Asignación " + gridAsignacion.DataKeys[index].Values["tipo_rev"].ToString() + "?');", true);
                    break;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            string idc_puesto_gpo = Session["idc_clasifrev_edit"].ToString();
            switch (Confirma_a)
            {
                case "Editar":
                    Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
                    Response.Redirect("asignacion_revisiones_captura.aspx?edit=true");
                    break;

                case "Eliminar":
                    Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
                    Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
                    entidad.Idc_clasifrev = Convert.ToInt32(idc_puesto_gpo);
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    DataSet ds = componente.EliminarAsignacion(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('Asignacion Eliminada Correctamente.','catalogo_asignacion_revisiones.aspx');", true);
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