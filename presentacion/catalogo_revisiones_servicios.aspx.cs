using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class catalogo_revisiones_servicios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            CargaGrid();
        }

        /// <summary>
        /// Carga Grid Pirncipal
        /// </summary>
        public void CargaGrid()
        {
            Revisiones_ServiciosENT entidad = new Revisiones_ServiciosENT();
            Revisiones_ServciosCOM componente = new Revisiones_ServciosCOM();
            DataSet ds = componente.CargaServicios(entidad);
            gridAvisos.DataSource = ds.Tables[0];
            gridAvisos.DataBind();
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

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            string idc_revisionser = Session["idc_revisionser_edit"].ToString();
            switch (Confirma_a)
            {
                case "Editar":
                    Response.Redirect("revisiones_servicios_captura.aspx?edit=true");
                    break;

                case "Eliminar":
                    Revisiones_ServiciosENT entidad = new Revisiones_ServiciosENT();
                    Revisiones_ServciosCOM componente = new Revisiones_ServciosCOM();
                    entidad.Pidc_revisionser = Convert.ToInt32(idc_revisionser);
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    DataSet ds = componente.EliminaRevisiones(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('Revision Eliminada Correctamente.','catalogo_revisiones_servicios.aspx');", true);
                    }
                    else
                    {
                        Alert.ShowAlertError(mensaje, this);
                    }
                    break;
            }
        }

        protected void gridAvisos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_revisionser_edit"] = Convert.ToInt32(gridAvisos.DataKeys[index].Values["idc_revisionser"].ToString());

            switch (e.CommandName)
            {
                case "Editar":
                    Session["Caso_Confirmacion"] = "Editar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Editar la Revision " + gridAvisos.DataKeys[index].Values["descripcion"].ToString() + "?');", true);
                    break;

                case "Eliminar":
                    Session["Caso_Confirmacion"] = "Eliminar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta seguro de Eliminar la Revision de " + gridAvisos.DataKeys[index].Values["descripcion"].ToString() + "');", true);
                    break;
            }
        }
    }
}