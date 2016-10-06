using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class Area_Trabajo : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Combo_Sucursales();
                Grid_Principal(0);
               
            }
        }

        public void Grid_Principal(int idc)
        {
            Area_TrabajoENT entidad = new Area_TrabajoENT();
            Area_TrabajoCOM  componente = new Area_TrabajoCOM();
             
            entidad.Pidc_Sucursal = idc;
            //DataSet ds = componente.Cargar_Relacion_Socursal_Grid(entidad);
            DataSet ds = componente.Grid_Areas(entidad);
            gridRelacionAreaTrabajo.DataSource = ds.Tables[0];
            gridRelacionAreaTrabajo.DataBind();

            if (ds.Tables[0].Rows.Count == 0) { NO_Hay.Visible = true; } else { NO_Hay.Visible = false; }

        }

        private void Combo_Sucursales()
        {
            Area_TrabajoENT entidad = new Area_TrabajoENT();
            Area_TrabajoCOM componente = new Area_TrabajoCOM();
            DataSet ds = componente.Cargar_Sucursales_Combo(entidad);
            ddlSucursal.DataValueField = "idc_sucursal";
            ddlSucursal.DataTextField = "nombre";
            ddlSucursal.DataSource = ds.Tables[0];
            ddlSucursal.DataBind();
            ddlSucursal.Items.Insert(0, new ListItem("--Seleccione un Sucursal", "0"));
            ddlSucursal.ClearSelection();
            ddlSucursal.SelectedValue = Convert.ToString(0);            
        }
        
        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc = ddlSucursal.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSucursal.SelectedValue);
            if (idc == 0)
            {
                Alert.ShowAlertError("Seleccione una Sucursal", this);
            }
            else
            {
                Grid_Principal(idc);
            }
            
        }
               
        protected void gridAsignacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_area_edit"] = Convert.ToInt32(gridRelacionAreaTrabajo.DataKeys[index].Values["idc_area"].ToString());
            Session["nombre_edit"] = gridRelacionAreaTrabajo.DataKeys[index].Values["nombre"].ToString();
            
            switch (e.CommandName)
            {
                case "Editar":
                    Session["Caso_Confirmacion"] = "Editar";
                    txtDescripcion.Text = gridRelacionAreaTrabajo.DataKeys[index].Values["nombre"].ToString();
                    idActivo.Checked = Convert.ToBoolean(gridRelacionAreaTrabajo.DataKeys[index].Values["activo"].ToString());
                    //Combo_Cedis(Convert.ToInt32(Session["idc_area_edit"]));                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalEditar('Mensaje del Sistema','Editar Area de trabajo','modal fade modal-info');", true);
                    break;

            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            int idc = ddlSucursal.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSucursal.SelectedValue);
            if (idc > 0)
            {
                txtDescripcion.Text = "" ;
                Session["idc_sucursal"] = idc;
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalEditar('Mensaje del Sistema','Nueva Area de Trabajo.','modal fade modal-info');", true);
            }
            else
            {
                Alert.ShowAlertError("Seleccione una Sucursal", this);
            }
            
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            //int idc = ddlCedis.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCedis.SelectedValue);
            if (txtDescripcion.Text != "")
            {
                Guardar(txtDescripcion.Text);
            }
            else
            {
                Alert.ShowAlertError("La descripcion esta vacia.", this);

            }

        }

        protected void Guardar(string descripcion)
        { 
            string caso = (string)Session["Caso_Confirmacion"];

            try
            {

                Area_TrabajoENT entidad = new Area_TrabajoENT();
                Area_TrabajoCOM componente = new Area_TrabajoCOM();
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"]);

                entidad.Pactivo = idActivo.Checked;
                DataSet ds = new DataSet();
                string vmensaje = "";
                switch (caso)
                {
                    case "Guardar":
                        entidad.Pdescripcion = descripcion;
                        entidad.Pidc_Sucursal = Convert.ToInt32(ddlSucursal.SelectedValue);
                        ds = componente.AltaRegistro(entidad);
                        break;

                    case "Editar":
                        entidad.Pidc_Area = Convert.ToInt32(Session["idc_area_edit"]);
                        entidad.Pdescripcion = descripcion;
                        entidad.Pidc_Sucursal = Convert.ToInt32(ddlSucursal.SelectedValue);
                        ds = componente.EditarRegistro(entidad);
                        Session["nombre_edit"] = null;
                        Session["idc_area_edit"] = null;

                        break;
                        

                }
                vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();

                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos procesando el registro.", "Espere un Momento", "Area_Trabajo.aspx", "imagenes/loading.gif", "2000", "Procesada Correctamente", this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        

    }
}