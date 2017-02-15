using negocio.Componentes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class puestos_alta_autorizar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["confirma"] = null;
                CargarInformacionPrePuesto();
            }
        }

        private void CargarInformacionPrePuesto()
        {
            try
            {
                //PERFILES
                PuestosCOM ComPuesto = new PuestosCOM();
                DataSet ds = new DataSet();
                ds = ComPuesto.sp_pre_puestos(0, 2);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grid_prepuestos.DataSource = ds.Tables[0];
                    grid_prepuestos.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void grid_prepuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(grid_prepuestos.DataKeys[index].Values["idc_pre_puesto"].ToString());
            string nombre = grid_prepuestos.DataKeys[index].Values["descripcion"].ToString();
            lblid.Text = id.ToString().Trim();
            switch (e.CommandName)
            {
                case "Vista":
                    Session["confirma"] = "Vista";
                    Response.Redirect("puestos_alta_captura.aspx?aut=E5EVE1V5E1VE51VE5V1E1V252B2A22V23&view=R455465GE6G54E65E1V16W5GEWG5R4E5R5E1C5EVE5VE5&idc_pre_puesto=" + funciones.deTextoa64(id.ToString()));
                    break;
                case "Editar":
                    Session["confirma"] = "Editar";
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                   "ModalConfirm('Mensaje del Sistema','¿Desea Editar la Solicitud para el Puesto " + nombre + "','modal fade modal-info');", true);
                    break;

                case "Solicitar":
                    Session["confirma"] = "Solicitar";
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                   "ModalConfirm('Mensaje del Sistema','¿Desea Solicitar la Autorización para la Alta del Puesto: " + nombre + "','modal fade modal-info');", true);
                    break;

                case "CancelarSolicitud":
                    Session["confirma"] = "CancelarSolicitud";
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                   "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar la Solicitud de Autorización para la Alta del Puesto: " + nombre + "','modal fade modal-danger');", true);
                    break;

                case "Responder":
                    Session["confirma"] = "Responder";
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                   "ModalConfirm('Mensaje del Sistema','¿Desea Responder la Solicitud para el Puesto " + nombre + "','modal fade modal-info');", true);
                    break;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmacion = Session["confirma"] as string;
                PuestosCOM componente = new PuestosCOM();
                DataSet ds = new DataSet();
                int idc_pre_puesto = Convert.ToInt32(lblid.Text);
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                string status = "";
                string vmensaje = "";
                switch (confirmacion)
                {
                    case "Responder":
                        Response.Redirect("puestos_alta_captura.aspx?aut=J1qf561q61fa1f6w161q31wq61wq56w16q1w31fqw1fwq1fq6w1fq1fw51q5d1qw5d15wq1dwqd&idc_pre_puesto=" + funciones.deTextoa64(lblid.Text));
                        break;

                    case "Editar":
                        Response.Redirect("puestos_alta_captura.aspx?idc_pre_puesto=" + funciones.deTextoa64(lblid.Text));
                        break;

                    case "Solicitar":
                        status = "X";//STATUS EN PROCESO DE AUTORIZACION
                        ds = componente.sp_status_pre_puestos(idc_pre_puesto, status, idc_usuario);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "CancelarSolicitud":
                        status = "P";//STATUS PENDIENTE
                        ds = componente.sp_status_pre_puestos(idc_pre_puesto, status, idc_usuario);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;
                }
                if (vmensaje == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "AlertGO('Movimiento realizado de manera correcta.','puestos_alta.aspx');", true);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this.Page);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }
    }
}