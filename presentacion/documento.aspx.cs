using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;

namespace presentacion
{
    public partial class documento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idc_puestoent = Convert.ToInt32(Request.QueryString["idc_puestoent"].ToString());
            int idc_entrega = Convert.ToInt32(Request.QueryString["idc_entrega"].ToString());
            int idc_vehi = Convert.ToInt32(Request.QueryString["idc_vehiculo"].ToString());
            CargarGridPrincipal(idc_entrega, idc_puestoent, idc_vehi);
        }

        // <summary>
        /// Carga los datos en Tablas de sesion y carga el grid principal
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal(int idc_entrega, int idc_puestoent, int IDC_VE)
        {
            Vehiculos_EntregaENT entidad = new Vehiculos_EntregaENT();
            Vehiculos_EntregarCOM componente = new Vehiculos_EntregarCOM();
            entidad.Pidc_puestoentrega = idc_puestoent;
            entidad.Pidc_entrega = idc_entrega;
            entidad.Pidc_puesto = IDC_VE;
            DataSet ds = componente.CargaFormato(entidad);
            RepeatVehiculos.DataSource = ds.Tables[1];
            RepeatVehiculos.DataBind();
            gridHerramientasVehiculo.DataSource = ds.Tables[2];
            gridHerramientasVehiculo.DataBind();
            DataRow row = ds.Tables[0].Rows[0];
            lblPuesto.Text = row["descripcion"].ToString();
            lblEmpleado.Text = row["empleado"].ToString();
            lbldireccion.Text = row["direccion"].ToString();
            string rutaimagen = funciones.GenerarRuta("imarev", "unidad");
            //var domn = Request.Url.Host;
            // var url = rutaimagen + ds.Tables[1].Rows[0]["idc_formatorev"].ToString() + ".jpg";
            //imgVehiculos.ImageUrl = url;
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/vehiculos_formato/"));//path local
            funciones.CopyFolder(rutaimagen, dirInfo.ToString(), this);
            imav.Src = "/temp/vehiculos_formato/" + ds.Tables[1].Rows[0]["idc_formatorev"].ToString() + ".jpg";
            int t = Convert.ToInt32(ds.Tables[1].Rows[0]["idc_formatorev"]);
            imav.Visible = Convert.ToInt32(ds.Tables[1].Rows[0]["idc_formatorev"]) == 0 ? false : true;
        }

        protected void RepeatVehiculos_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
        }
    }
}