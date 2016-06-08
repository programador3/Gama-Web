using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;

namespace presentacion
{
    public partial class pendientes_tareas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TareasENT entidad = new TareasENT();
            TareasCOM componente = new TareasCOM();
            entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
            DataSet ds = componente.CargarPendientes(entidad);
            Repeater3.DataSource = ds.Tables[0];
            Repeater3.DataBind();
        }
    }
}