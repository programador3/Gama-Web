using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class empleados_faltas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            CargarFaltas();
        }

        private void CargarFaltas()
        {
            try
            {
                FaltasENT entidad = new FaltasENT();
                FaltasCOM componente = new FaltasCOM();
                entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                DataSet ds = componente.CargaPrep(entidad);
                gridservicios.DataSource = ds.Tables[0];
                gridservicios.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void lnkir_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string value = funciones.deTextoa64(lnk.CommandName);
            Response.Redirect("empleados_faltas_captura.aspx?idc_empleado_falta=" + value);
        }

        protected void gridservicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_empleado_falta = Convert.ToInt32(gridservicios.DataKeys[index].Values["idc_empleado_falta"].ToString());
           int idc_empleado = Convert.ToInt32(gridservicios.DataKeys[index].Values["idc_empleado"].ToString());
            switch (e.CommandName)
            {
                case "Editar":
                    string url = "empleados_faltas_captura.aspx?idc_empleado_falta="+funciones.deTextoa64(idc_empleado_falta.ToString().Trim());
                    Response.Redirect(url);
                    break;

            }
        }
    }
}