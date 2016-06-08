using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class catalogo_avisos : System.Web.UI.Page
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
            Catalogo_AvisosENT entidad = new Catalogo_AvisosENT();
            Catalogo_AvisosCOM componente = new Catalogo_AvisosCOM();
            entidad.Pidc_taviso = 0;
            DataSet ds = componente.CargaAvisos(entidad);
            gridAvisos.DataSource = ds.Tables[0];
            gridAvisos.DataBind();
        }

        protected void gridAvisos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_taviso"] = Convert.ToInt32(gridAvisos.DataKeys[index].Values["idc_taviso"].ToString());

            switch (e.CommandName)
            {
                case "Editar":
                    Session["Caso_Confirmacion"] = "Editar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Editar este Aviso?');", true);
                    break;

                case "Eliminar":
                    Session["Caso_Confirmacion"] = "Eliminar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta seguro de Eliminar este Aviso?');", true);
                    break;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            string idc_taviso = Session["idc_taviso"].ToString();
            switch (Confirma_a)
            {
                case "Editar":
                    Response.Redirect("catalogo_avisos_captura.aspx?idc_taviso=" + idc_taviso);
                    break;

                case "Eliminar":
                    Catalogo_AvisosENT entidad = new Catalogo_AvisosENT();
                    Catalogo_AvisosCOM compo = new Catalogo_AvisosCOM();
                    entidad.Pidc_taviso = Convert.ToInt32(idc_taviso);
                    DataSet ds = compo.EliminaAviso(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('Aviso Eliminado Correctamente.','catalogo_avisos.aspx');", true);
                    }
                    else
                    {
                        Alert.ShowAlertError(mensaje, this);
                    }
                    break;
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

        protected void lnkNuevoAviso_Click(object sender, EventArgs e)
        {
            Response.Redirect("catalogo_avisos_captura.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
        }
    }
}