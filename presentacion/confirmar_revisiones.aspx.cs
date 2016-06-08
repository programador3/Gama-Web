using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class confirmar_revisiones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
                CargarGridPrincipal();
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        /// <summary>
        /// Carga los datos en Tablas de sesion y carga el grid principal
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal()
        {
            try
            {
                Servicios_EntregaENT entidad = new Servicios_EntregaENT();
                Revisones_EntregaCOM componente = new Revisones_EntregaCOM();
                entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                DataSet ds = componente.CargaConfirmacion(entidad);
                repeatServicios.DataSource = ds.Tables[0];
                repeatServicios.DataBind();
                Session["idc_entrega"] = ds.Tables[0].Rows[0]["idc_entrega"];
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":

                    Servicios_EntregaENT entidad = new Servicios_EntregaENT();
                    Revisones_EntregaCOM componente = new Revisones_EntregaCOM();
                    entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                    entidad.Cadser = GeneraCadena();
                    entidad.Totalcadser = TotalCadena();
                    entidad.Pidc_entrega = Convert.ToInt32(Session["idc_entrega"]);
                    entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    try
                    {
                        DataSet ds = componente.InsertarConfirm(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        string mensaje = row["mensaje"].ToString();
                        if (mensaje == "")//no hay errores retornamos true
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La Confirmación de recibido fue guardada correctamente.','confirmacion_entrega.aspx');", true);
                        }
                        else
                        {//mostramos error
                            Alert.ShowAlertError(mensaje, this);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.ToString(), this.Page);
                        Global.CreateFileError(ex.ToString(), this);
                    }

                    break;

                case "Cancelar":
                    Response.Redirect("confirmacion_entrega.aspx");
                    break;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            bool error = false;
            //int CadenaCEL = TotalCadenaCelular();
            if (TotalCadena() == 0)
            {
                Alert.ShowAlertError("Para Guardar, debe Confirmar al menos un Servicio", this);
                error = true;
            }
            foreach (RepeaterItem item in repeatServicios.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblidc_revisionser");
                if (txt.Text == "" && cbx.Checked == false)
                {
                    error = true;
                    Alert.ShowAlertError("Si rechaza la entrega, debe colocar una observación", this);
                }
            }
            if (error != true)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de confirmar que recibio estos servicios. Todos sus datos son correctos?');", true);
            }
        }

        public int TotalCadena()
        {
            int total = 0;
            foreach (RepeaterItem item in repeatServicios.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblidc_revisionser");
                if (cbx.Checked == true)
                {
                    total = total + 1;
                }
            }
            return total;
        }

        public string GeneraCadena()
        {
            string total = "";
            foreach (RepeaterItem item in repeatServicios.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblidc_revisionser");
                if (cbx.Checked == true)
                {
                    total = total + lblidc.Text + ";" + cbx.Checked.ToString() + ";" + txt.Text.ToUpper() + ";";
                }
            }
            return total;
        }
    }
}