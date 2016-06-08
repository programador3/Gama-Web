using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class pmd_rh : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            CargarDatosEmpleado();
        }

        /// <summary>
        /// Carga DATOS DEL EMPLEADO
        /// </summary>
        public void CargarDatosEmpleado()
        {
            PuestosENT entidad = new PuestosENT();
            PuestosCOM componente = new PuestosCOM();
            DataSet ds = componente.CargaPMD(entidad);
            repeatpendientes.DataSource = ds.Tables[0];
            repeatpendientes.DataBind();
        }

        protected void lnkGO_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string idc_empleado_pmd = funciones.deTextoa64(lnk.CommandName);
            Response.Redirect("pmd_rh_detalles.aspx?idc_empleado_pmd=" + idc_empleado_pmd);
        }
    }
}