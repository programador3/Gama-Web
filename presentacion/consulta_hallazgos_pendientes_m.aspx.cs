using negocio;
using negocio.Componentes;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class consulta_hallazgos_pendientes_m : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {int idc_sucursal = Request.QueryString["s"]==null?0: Convert.ToInt32(funciones.de64aTexto(Request.QueryString["s"]));
                lnk.Visible = idc_sucursal == 0 ? false : true;
                CargaSucursales(idc_sucursal);
            }
        }
        public void CargaSucursales(int idc_sucursal)
        {
            try
            {
                HallazgosENT entidad = new HallazgosENT();
                HallazgosCOM componente = new HallazgosCOM();
                entidad.pidc_sucursal = idc_sucursal;
                entidad.Pidc_vehiculo = 0;
                DataSet ds = componente.CargarHallazgo(entidad);
                gridhallazgos.DataSource = ds.Tables[0];
                gridhallazgos.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void gridhallazgos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string vidc = gridhallazgos.DataKeys[index].Values["idc"].ToString();
            string ruta = funciones.GenerarRuta("HALLAZ", "unidad");
            //img.ImageUrl=ruta+vidc
            string[] allFiles = System.IO.Directory.GetFiles(ruta);//Change path to yours
            foreach (string file in allFiles)
            {
                if (Path.GetFileNameWithoutExtension(file)==vidc)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/img/"));//path local
                    funciones.CopiarArchivos(ruta + vidc + Path.GetExtension(file), dirInfo + vidc + Path.GetExtension(file),this);
                    img.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["server"]+"/temp/img/" + vidc + Path.GetExtension(file);
                    
                }
            }
            txthallazgo.Text = gridhallazgos.DataKeys[index].Values["observaciones"].ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Detalles del Hallazgo','modal fade modal-info');", true);

        }

        protected void lnk_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMesswwwwage", "window.close();", true);
            
        }
    }
}