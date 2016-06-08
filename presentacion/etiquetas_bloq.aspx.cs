using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class etiquetas_bloq : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_opcion = 1799;  //pertenece al modulo de grupos backend
                if (funciones.permiso(idc_usuario, idc_opcion) == false)
                {
                    Response.Redirect("menu.aspx");
                    return;
                }
                if (Request.QueryString["idc_perfiletiq"] == null)
                {
                    Response.Redirect("grupos_backend.aspx");
                }
                else
                {
                    //recibimos por url el id
                    int idc_etiq = Convert.ToInt32(Request.QueryString["idc_perfiletiq"]);
                    ocidc_perfiletiq.Value = Request.QueryString["idc_perfiletiq"].ToString();
                    cargar_grid(idc_etiq);
                }
            }
        }

        private void cargar_grid(int idc_perfiletiq)
        {
            try
            {
                //declaramos la entidad
                Etiquetas_EN entidad = new Etiquetas_EN();
                entidad.Idc_perfilgpoetiq = idc_perfiletiq;
                //llamamos al componente
                DataSet ds = new DataSet();
                Etiquetas_CM componente = new Etiquetas_CM();
                ds = componente.Bloqueos(entidad);
                grid_etiquetas_bloq.DataSource = ds.Tables[0];
                grid_etiquetas_bloq.DataBind();
                //llenamos el cbox de opciones de la etiqueta
                cboxopcionesetiq.DataSource = ds.Tables[1];
                cboxopcionesetiq.DataTextField = "descripcion";
                cboxopcionesetiq.DataValueField = "idc_perfiletiq_opc";
                cboxopcionesetiq.DataBind();
                cboxopcionesetiq.Items.Insert(0, new ListItem("Seleccionar", "0"));
                //llenamos el cbox de etiquetas
                cboxetiq.DataSource = ds.Tables[2];
                cboxetiq.DataTextField = "nombre";
                cboxetiq.DataValueField = "idc_perfiletiq";
                cboxetiq.DataBind();
                cboxetiq.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message, this);
            }
        }

        protected void btnaddbolqueo_Click(object sender, EventArgs e)
        {
            if (!validar_campos())
            {
                return;
            }

            //entidad
            Etiquetas_EN entidad = new Etiquetas_EN();
            string cadena = generarCadena(0, 0);
            if (cadena != "")
            {
                entidad.Cadena_bloq = cadena;
                entidad.Cadena_bloq_total = 1;
            }
            else
            {
                entidad.Cadena_bloq = "";
                entidad.Cadena_bloq_total = 0;
            }
            try
            {
                //componente
                Etiquetas_CM componente = new Etiquetas_CM();
                DataSet ds = new DataSet();
                ds = componente.updateBloqueos(entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    //todo bien
                    msgbox.show("Bloqueo agregado correctamente", this.Page);
                    Response.Redirect("etiquetas_bloq.aspx?idc_perfiletiq=" + ocidc_perfiletiq.Value.ToString());
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

        protected bool validar_campos()
        {
            if (cboxetiq.SelectedIndex == 0 || cboxopcionesetiq.SelectedIndex == 0)
            {
                Alert("Debe seleccionar una opción y una etiqueta.", "Advertencia");
                return false;
            }
            return true;
        }

        //Funcion que ejecuta alerta tipo Información. Se hereda de SweetAlert JS y se ejecuta desde el servidor con ScriptManager
        // @Mensaje: Type String.  Cuerpo del Mensaje
        // @Titulo:  Tyoe String.  Titulo del Mensaje
        public void Alert(String Mensaje, String Titulo)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('" + Titulo + "','" + Mensaje + "')",
                true);
        }

        private string generarCadena(int id, int borrado)
        {
            string cadena;
            if (id > 0 & borrado == 1)
            {
                cadena = id + ";" + cboxopcionesetiq.SelectedValue.ToString() + ";" + cboxetiq.SelectedValue.ToString() + ";1;";
            }
            else if (id == 0 & borrado == 0)
            {
                cadena = "0;" + cboxopcionesetiq.SelectedValue.ToString() + ";" + cboxetiq.SelectedValue.ToString() + ";0;";
            }
            else
            {
                cadena = "";
            }
            return cadena;
        }

        protected void grid_etiquetas_bloq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //me causaba error cuando usaba el link ver..
            //recuperamos el indice del registro seleccionado del grid de Telefono
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid_etiquetas_bloq.DataKeys[index].Value);
            //Session["sidc_perfilgpo"] = Convert.ToInt32(vidc.ToString().Trim());
            switch (e.CommandName)
            {
                case "eliminar":
                    deleteBloqueo(vidc);
                    break;
            }
        }

        private void deleteBloqueo(int idc_bloqueo)
        {
            //entidad
            Etiquetas_EN entidad = new Etiquetas_EN();
            string cadena = generarCadena(idc_bloqueo, 1);
            if (cadena != "")
            {
                entidad.Cadena_bloq = cadena;
                entidad.Cadena_bloq_total = 1;
            }
            else
            {
                entidad.Cadena_bloq = "";
                entidad.Cadena_bloq_total = 0;
            }

            try
            {
                //componente
                Etiquetas_CM componente = new Etiquetas_CM();
                DataSet ds = new DataSet();
                ds = componente.updateBloqueos(entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    //todo bien
                    msgbox.show("Bloqueo eliminado correctamente", this.Page);
                    Response.Redirect("etiquetas_bloq.aspx?idc_perfiletiq=" + ocidc_perfiletiq.Value.ToString());
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
    }
}