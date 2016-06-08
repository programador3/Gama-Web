using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class confirmar_herramientas : System.Web.UI.Page
    {
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
                Herramientas_EntregasENT entidad = new Herramientas_EntregasENT();
                Herramientas_EntregasCOM componente = new Herramientas_EntregasCOM();
                entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                DataSet ds = componente.ConfirmaHerr(entidad);
                DataRow row = ds.Tables[0].Rows[0];
                repeat_listado.DataSource = ds.Tables[0];
                repeat_listado.DataBind();
                Session["idc_entrega_eq"] = Convert.ToInt32(row["idc_entrega_eq"].ToString());
                Session["idc_entrega"] = ds.Tables[0].Rows[0]["idc_entrega"];
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool error = false;
            // if (TotalCadena() < 1) { error = true; Alert.ShowAlertError("Debe Guardar al menos 1 Activo", this); }
            foreach (RepeaterItem item in repeat_listado.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txtObservaciones = (TextBox)item.FindControl("txtObservaciones");
                Label lblError = (Label)item.FindControl("lblError");
                lblError.Visible = false;
                if (cbx.Checked == false && txtObservaciones.Text == "") { error = true; lblError.Visible = true; lblError.Text = "Debe colocar observaciones"; }
            }
            if (TotalCadena() == 0) { error = true; Alert.ShowAlertError("Debe confirmar al menos un activo para guardar", this); }
            if (error == false)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Confirma que se le entregaron estos Activos?');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        /// <summary>
        /// Retorna cadena de entregas
        /// </summary>
        /// <returns></returns>
        public string GeneraCadena()
        {
            string cadena = "";
            foreach (RepeaterItem Item in repeat_listado.Items)
            {
                TextBox txtObservaciones = (TextBox)Item.FindControl("txtObservaciones");
                CheckBox cbx = (CheckBox)Item.FindControl("cbx");
                Label lblactivo = (Label)Item.FindControl("lblactivo");
                if (cbx.Checked == true)
                {
                    cadena = cadena + lblactivo.Text + ";" + cbx.Checked.ToString() + ";" + txtObservaciones.Text.ToUpper() + ";";
                }
            }
            return cadena;
        }

        public int TotalCadena()
        {
            int total = 0;
            foreach (RepeaterItem Item in repeat_listado.Items)
            {
                TextBox txtObservaciones = (TextBox)Item.FindControl("txtObservaciones");
                CheckBox cbx = (CheckBox)Item.FindControl("cbx");
                Label lblactivo = (Label)Item.FindControl("lblactivo");
                total = total + 1;
            }
            return total;
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    try
                    {
                        Herramientas_EntregasENT entidad = new Herramientas_EntregasENT();
                        Herramientas_EntregasCOM componente = new Herramientas_EntregasCOM();
                        entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                        entidad.Cadherr = GeneraCadena();
                        entidad.Numcadherr = TotalCadena();
                        entidad.Pidc_Entrega = Convert.ToInt32(Session["idc_entrega"]);
                        entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        DataSet ds = componente.InsertaConfirmActivos(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        string mensaje = row["mensaje"].ToString();
                        if (mensaje == "")//no hay errores retornamos true
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('Los datos han sido guardados correctamente.','confirmacion_entrega.aspx');", true);
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

        protected void cbx_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void txtObservaciones_TextChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeat_listado.Items)
            {
                TextBox txtObservaciones = (TextBox)item.FindControl("txtObservaciones");
                Label lblError = (Label)item.FindControl("lblError");
                lblError.Visible = false;
            }
        }

        protected void lbkseltodo_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeat_listado.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                cbx.Checked = true;
            }
            lbkseltodo.Visible = false;
            lnkDes.Visible = true;
        }

        protected void lnkDes_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeat_listado.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                cbx.Checked = false;
            }
            lbkseltodo.Visible = true;
            lnkDes.Visible = false;
        }
    }
}