using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class empleados_reportes_pendientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
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
                view.RowFilter = "status LIKE'%PENDIENTE%'";
                repeat.DataSource = view.ToTable();
                repeat.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void lnkgo_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            string value = lnk.CommandName;
            string value2 = lnk.CommandArgument.ToString();
            Response.Redirect("empleados_reportes.aspx?autoriza=KJBKJBWQOWJBOQKBWDOQBOWKOKQNKOOKBAOBQDOKQND&idc_empleadorep=" + funciones.deTextoa64(value) + "&idc_empleado=" + funciones.deTextoa64(value2));
        }
    }
}