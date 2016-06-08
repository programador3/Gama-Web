using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class grupos_backend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                //int idc_opcion = 1814;  //pertenece al modulo de grupos backend
                //if (funciones.permiso(idc_usuario, idc_opcion) == false)
                //{
                //    Response.Redirect("menu.aspx");
                //    return;
                //}

                //cargar gridview
                cargar_datos();
            }
        }

        protected void btnaddgpo_Click(object sender, EventArgs e)
        {
            Response.Redirect("grupos_backend_captura.aspx");
        }

        /// <summary>
        /// metodo que consulta la bd y carga los grupos dados de alta
        /// </summary>
        protected void cargar_datos()
        {
            //creo dataset
            DataSet ds = new DataSet();
            //entidad
            Grupos_backendE Entidad = new Grupos_backendE();
            //Componente
            Grupos_backendBL Componente = new Grupos_backendBL();
            ds = Componente.grupos(Entidad);

            //llenamos el gridview
            gridview_gruposbackend.DataSource = ds.Tables[0];
            gridview_gruposbackend.DataBind();
        }

        protected void gridview_gruposbackend_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //me causaba error cuando usaba el link ver..
            //recuperamos el indice del registro seleccionado del grid de Telefono
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(gridview_gruposbackend.DataKeys[index].Value);
            string grupo = gridview_gruposbackend.DataKeys[index].Values["grupo"].ToString();
            //Session["sidc_perfilgpo"] = Convert.ToInt32(vidc.ToString().Trim());
            switch (e.CommandName)
            {
                case "editar":
                    Session["Caso_Confirmacion"] = "Editar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Editar el grupo " + grupo + "?');", true);

                    Session["url_grupos"] = "grupos_backend_captura.aspx?uidc_perfilgpo=" + vidc.ToString();
                    break;

                case "eliminar":
                    Session["Caso_Confirmacion"] = "Eliminar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Eliminar el " + grupo + "?');", true);
                    Session["vidc"] = Convert.ToInt32(gridview_gruposbackend.DataKeys[index].Value);
                    break;
            }
        }

        protected void deleteGpo(int idc_perfilgpo)
        {
            //Entidad
            Grupos_backendE Entidad = new Grupos_backendE();
            Entidad.Idc_perfilgpo = idc_perfilgpo;
            Entidad.Usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
            //componente
            Grupos_backendBL Componente = new Grupos_backendBL();
            DataSet ds = new DataSet();
            try
            {
                ds = Componente.eliminarGrupos(Entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    //todo bien
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "AlertOK('grupos_backend.aspx');", true);
                }
                else
                {
                    //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                    msgbox.show(vmensaje, this.Page);
                    return;
                }
            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message, this);
            }
        }

        protected void gridview_gruposbackend_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            System.Web.UI.WebControls.Image Libre = (System.Web.UI.WebControls.Image)e.Row.FindControl("Libre");
            System.Web.UI.WebControls.Image Opciones = (System.Web.UI.WebControls.Image)e.Row.FindControl("Opciones");
            System.Web.UI.WebControls.Image Publicar = (System.Web.UI.WebControls.Image)e.Row.FindControl("Publicar");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                if (Convert.ToBoolean(rowView["externo"]) == true)//si es 0 SIGNIFICA QUE ES NUEVO, MUESTRO ETIQUETA CORRESPONDIENTE
                {
                    Publicar.ImageUrl = "imagenes/btn/checked.png";
                }
                if (Convert.ToBoolean(rowView["opciones"]) == true)//si es 0 SIGNIFICA QUE ES NUEVO, MUESTRO ETIQUETA CORRESPONDIENTE
                {
                    Opciones.ImageUrl = "imagenes/btn/checked.png";
                }
                if (Convert.ToBoolean(rowView["libre"]) == true)//si es 0 SIGNIFICA QUE ES NUEVO, MUESTRO ETIQUETA CORRESPONDIENTE
                {
                    Libre.ImageUrl = "imagenes/btn/checked.png";
                }
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Eliminar":
                    int vidc = Convert.ToInt32(Session["vidc"]);
                    deleteGpo(vidc);
                    break;

                case "Editar":
                    string url = (string)Session["url_grupos"];
                    Response.Redirect(url);
                    break;
            }
        }
    }
}