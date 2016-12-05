
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace presentacion
{
    public partial class puestos_equi : System.Web.UI.Page
    {
        Puesto_EquiENT entidad = new Puesto_EquiENT();
        Puesto_EquiCOM componente = new Puesto_EquiCOM();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                CargarGridPrincipal();
                
            }
        }

        
        public void CargarGridPrincipal()
        {
            entidad = new Puesto_EquiENT();
            componente = new Puesto_EquiCOM();
            ds = componente.CargarPuestos(entidad);
            gridEquivalente.DataSource = ds.Tables[0];
            gridEquivalente.DataBind();

            if (ds.Tables[0].Rows.Count == 0) { NO_Hay.Visible = true; }

        }

        public void Cargar_Grid_Modal(int id)
        {
            entidad = new Puesto_EquiENT();
            componente = new Puesto_EquiCOM();
            entidad.Pidc_puesto_equi = id;
            ds = componente.CargarPuestos(entidad);
            GridPuestoRelacionado.DataSource = ds.Tables[0];
            GridPuestoRelacionado.DataBind();
            if (ds.Tables[0].Rows.Count == 0) { VacioGrid_modal.Visible = true; }
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
            int index = Convert.ToInt32(e.CommandArgument);            
            txt_pidc_puestoequi_h.Value = gridEquivalente.DataKeys[index].Values["pidc_puestoequi"].ToString();
            txt_pdescripcion_h.Value = gridEquivalente.DataKeys[index].Values["pdescripcion"].ToString();

            switch (e.CommandName)
            {
                case "Editar":
                    txtCaso_h.Value = "Editar";
                    txtDescripcion.Text = txt_pdescripcion_h.Value;//gridEquivalente.DataKeys[index].Values["pdescripcion"].ToString();   
                    string str_Alert ;
                    str_Alert = string.Format("ModalEditar('Mensaje del Sistema','¿Desea Editar la Asignacion {0}?','{1}');", txt_pdescripcion_h.Value, "modal fade modal-info");                
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", str_Alert , true);
                    break;

                case "Eliminar":
                    txtCaso_h.Value = "Eliminar";
                    string str_modal;
                    str_modal = string.Format("ModalConfirm('Mensaje del Sistema','¿Esta seguro de Eliminar la Descripcion: {0}?  Al borrar este registro tambien se borraran las relaciones que existan con los puestos ','{1}');", txt_pdescripcion_h.Value, "modal fade modal-info");
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",str_modal , true);
                    break;
                case "puestos_relacionados":
                    Cargar_Grid_Modal(Convert.ToInt32(txt_pidc_puestoequi_h.Value));
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalGrid('Mensaje del Sistema','Puestos Relacionados.','modal fade modal-info');", true);

                    break;
            }
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            string caso = txtCaso_h.Value;
            try
            {
                entidad = new Puesto_EquiENT();
                componente = new Puesto_EquiCOM();

                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pactivo = true;
                ds = new DataSet();
                string vmensaje = "";
                switch (caso)
                {
                    case "Guardar":
                        entidad.Pdescripcion = txtDescripcion.Text.ToUpper();  
                        ds = componente.AltaPuestos_Equi(entidad);
                        break;

                    case "Editar":

                        entidad.Pdescripcion = txtDescripcion.Text.ToUpper();
                        entidad.Pidc_puesto_equi = Convert.ToInt32(txt_pidc_puestoequi_h.Value);                        
                        ds = componente.ModificarDatos(entidad);

                        break;

                    case "Eliminar":

                        entidad.Pdescripcion = txtDescripcion.Text.ToUpper();
                        
                        entidad.Pactivo = false;
                        entidad.Pidc_puesto_equi = Convert.ToInt32(txt_pidc_puestoequi_h.Value);
                        ds = componente.ModificarDatos(entidad);
                        
                        break;


                }
                vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();

                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos procesando el registro.", "Espere un Momento", "puestos_equi.aspx", "imagenes/loading.gif", "2000", "Procesada Correctamente", this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            txtDescripcion.Text = "";
            txtCaso_h.Value = "Guardar";
            txtDescripcion.Focus();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalEditar('Mensaje del Sistema','Nueva Descripcion','modal fade modal-info');", true);
           
        }
        
    }
}