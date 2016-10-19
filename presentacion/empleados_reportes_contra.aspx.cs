
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace presentacion
{
    public partial class empleados_reportes_pendientes_contraparte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            Session["backurl"] = "empleados_reportes_contra.aspx";
            CargarPendientes();
        }
        /// <summary>
        /// Carga los pendientes de mi puesto
        /// </summary>
        private void CargarPendientes()
        {
            try
            {
                ReportesENT entidad = new ReportesENT();
                entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
                ReportesCOM componente = new ReportesCOM();
                DataTable DT = componente.CargaJefe(entidad).Tables[0];
                //filtro los que estan pendientes
                DataView view = DT.DefaultView;
                view.RowFilter = "estado LIKE'%A%'";
                gridservicios.DataSource = view.ToTable();
                gridservicios.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
        protected void gridservicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_empleadorep = Convert.ToInt32(gridservicios.DataKeys[index].Values["idc_empleadorep"].ToString());
            ViewState["idc_empleadorep"] = idc_empleadorep;
            ViewState["idc_empleado"] = Convert.ToInt32(gridservicios.DataKeys[index].Values["idc_empleado"].ToString());
            switch (e.CommandName)
            {
                case "Editar":
                    Session["Caso_Confirmacion"] = e.CommandName;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Desea Terminar el Reporte de " + gridservicios.DataKeys[index].Values["empleado"].ToString() + "?','modal fade modal-info');", true);

                    break;
              
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso = (string)Session["Caso_Confirmacion"];
            int idc = Convert.ToInt32(ViewState["idc_empleadorep"]);
            int idc2 = Convert.ToInt32(ViewState["idc_empleado"]);
            switch (caso)
            {
                case "Editar":
                    string url = "empleados_reportes.aspx?termina=KJBKJBWQOWJBOQKBWDOQBOWKOKQNKOOKBAOBQDOKQND&idc_empleadorep=" + funciones.deTextoa64(idc.ToString()) + "&idc_empleado=" + funciones.deTextoa64(idc2.ToString());
                    Response.Redirect(url);
                    break;
            }
        }
    }
}