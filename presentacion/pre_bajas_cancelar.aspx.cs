using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class pre_bajas_cancelar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!Page.IsPostBack)
            {
                //iniciamos valores del switch
                Session["apto_reingreso"] = 0;
                Session["status"] = 1;
                txtFecha.Text = DateTime.Today.ToString();
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_opcion = 1280;  //pertenece al modulo de pre_bajas
                if (funciones.permiso(idc_usuario, idc_opcion) == false)
                {
                    Response.Redirect("menu.aspx");
                    return;
                }
            }
            Session["idc_usuario"] = Convert.ToInt32(Session["sidc_usuario"].ToString());
            GenerarDatos();
        }

        public void GenerarDatos()
        {
            BajasENT entidad = new BajasENT();
            BajasCOM componente = new BajasCOM();
            DataSet ds = componente.CargaBajasCancelar(entidad);
            DataTable table = ds.Tables[0];
            gridEmpleados.DataSource = table;
            gridEmpleados.DataBind();
            if (ds.Tables[0].Rows.Count == 0) { Noempleados.Visible = true; }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    string strHostName = Dns.GetHostName();
                    IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                    BajasENT entidad = new BajasENT();
                    BajasCOM componente = new BajasCOM();
                    entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString()); //USUARIO QUE REALIZA LA PREBAJA
                    entidad.Ip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Nombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Usuariopc = funciones.GetUserName();//usuario pc
                    entidad.Pidc_prebaja = Convert.ToInt32(Session["idc_prebaja"].ToString());
                    entidad.Motivo = txtMotivo.Text;
                    DataSet ds = componente.CancelarBaja(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    //verificamos que no existan errores
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")//no hay errores retornamos true
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La Pre-Baja fue cancelada correctamente.','pre_bajas_cancelar.aspx','success');", true);
                    }
                    else
                    {//mostramos error
                        Alert.ShowAlertError(mensaje, this);
                    }
                    break;
            }
        }

        protected void btnACeptarPrebaja_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (txtMotivoCancelacion.Text == string.Empty) { Alert.ShowAlertError("Debe especificar el motivo de la cancelación.", this); error = true; }
            if (error == false)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de Cancelar la Baja de " + lblEmpleadoName.Text + ". Esta Seguro de Continuar?');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("pre_bajas_cancelar.aspx");
        }

        protected void gridEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER MEDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            int num_nomina = Convert.ToInt32(gridEmpleados.DataKeys[index].Values["num_nomina"].ToString());
            int idc_empleado = Convert.ToInt32(gridEmpleados.DataKeys[index].Values["idc_empleado"].ToString());
            int idc_prebaja = Convert.ToInt32(gridEmpleados.DataKeys[index].Values["idc_prebaja"].ToString());
            int idc_puesto = Convert.ToInt32(gridEmpleados.DataKeys[index].Values["idc_puesto"].ToString());
            Session["fecha_baja"] = gridEmpleados.DataKeys[index].Values["fecha_baja"];
            int capacitacion = Convert.ToInt32(gridEmpleados.DataKeys[index].Values["idc_cap"]);
            cbxHonesto.Checked = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["honesto"]);
            cbxDrogas.Checked = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["drogas"]);
            cbxTrabajador.Checked = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["trabajador"]);
            cbxAlcol.Checked = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["alcohol"]);
            cbxCartaRec.Checked = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["carta_recomendacion"]);
            bool contratar = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["contratar"]);
            bool apto_reingreso = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["apto_reingreso"]);
            if (capacitacion == 0)
            {
                Session["capacitacion"] = false;
            }
            else
            {
                Session["capacitacion"] = true;
            }
            if (contratar == false)
            {
                lnkVacanteC.CssClass = "btn btn-link";
                lnkVacanteNO.CssClass = "btn btn-primary active";
            }
            else
            {
                lnkVacanteNO.CssClass = "btn btn-link";
                lnkVacanteC.CssClass = "btn btn-primary active";
            }
            if (apto_reingreso == false)
            {
                lnkAptoReingresoSI.CssClass = "btn btn-link";
                lnkAptoReingresoNO.CssClass = "btn btn-primary active";
            }
            else
            {
                lnkAptoReingresoNO.CssClass = "btn btn-link";
                lnkAptoReingresoSI.CssClass = "btn btn-primary active";
            }
            Session["num_nomina"] = num_nomina;
            Session["idc_empleado"] = idc_empleado;
            Session["idc_puesto"] = idc_puesto;
            Session["idc_prebaja"] = idc_prebaja;
            Session["contratar"] = contratar;
            lblEmpleadoName.Text = gridEmpleados.DataKeys[index].Values["empleado"].ToString();
            lblPuesto.Text = gridEmpleados.DataKeys[index].Values["descripcion"].ToString();
            txtFecha.Text = gridEmpleados.DataKeys[index].Values["fecha_baja"].ToString();
            txtMotivo.Text = gridEmpleados.DataKeys[index].Values["motivo"].ToString();
            txtEspecificar.Text = gridEmpleados.DataKeys[index].Values["especificar"].ToString();

            Noempleados.Visible = false;
            row_grid.Visible = false;
            PanelPreBaja.Visible = true;
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
            if (Session["Previus"] == null)
            {
                Response.Redirect("pre_bajas_cancelar.aspx");
            }
        }
    }
}