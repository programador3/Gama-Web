using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion.temp
{
    public partial class catalogo_grupos_puestos : System.Web.UI.Page
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
            Grupos_PuestosENT entidad = new Grupos_PuestosENT();
            Grupos_PuestosCOM componente = new Grupos_PuestosCOM();
            DataSet ds = componente.CargaGrupos_Puestos(entidad);
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
            string idc_puesto_gpo = Session["idc_puesto_gpo_edit"].ToString();
            switch (Confirma_a)
            {
                case "Editar":
                    Response.Redirect("grupos_puestos_captura.aspx?edit=true");
                    break;

                case "Eliminar":
                    Grupos_PuestosENT entidad = new Grupos_PuestosENT();
                    Grupos_PuestosCOM componente = new Grupos_PuestosCOM();
                    entidad.Idc_puesto_gpo = Convert.ToInt32(idc_puesto_gpo);
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    DataSet ds = componente.EliminarGrupo(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('Grupo Eliminado Correctamente.','catalogo_grupos_puestos.aspx');", true);
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
            Session["idc_puesto_gpo_edit"] = Convert.ToInt32(gridAvisos.DataKeys[index].Values["idc_puesto_gpo"].ToString());

            switch (e.CommandName)
            {
                case "Editar":
                    Session["Caso_Confirmacion"] = "Editar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Editar el Grupo " + gridAvisos.DataKeys[index].Values["descripcion"].ToString() + "?');", true);
                    break;

                case "Eliminar":
                    Session["Caso_Confirmacion"] = "Eliminar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta seguro de Eliminar el Grupo " + gridAvisos.DataKeys[index].Values["idc_puesto_gpo"].ToString() + "');", true);
                    break;
            }
        }
    }
}