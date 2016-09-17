using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class permisos_pendientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Carga();
            }
        }

        private void Carga()
        {
            Solicitud_HorarioENT entidad = new Solicitud_HorarioENT();
            SolicitudHorarioCOM componente = new SolicitudHorarioCOM();
            DataView view = componente.SolcitudDetalles(entidad).Tables[0].DefaultView;
            view.RowFilter = "status= 'P'";
            gridcelulares.DataSource = view.ToTable();
            gridcelulares.DataBind();
        }

        protected void gridcelulares_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string idc_horario_perm = gridcelulares.DataKeys[index].Values["idc_horario_perm"].ToString();
            string idc_puesto = gridcelulares.DataKeys[index].Values["idc_puesto"].ToString();
            Response.Redirect("solicitud_horario.aspx?autoriza=sajwsbciscoiwcpijspiwsIJIvHVUCUHvhvhvuHVUVUhvUHVUVUHVuhvUCAJBIWQPIJWBCPICVHVCIWHVC873V8C8CVP83VCPIWVCUVCPIV&idc_puesto=" + funciones.deTextoa64(idc_puesto) + "&idc_horario_perm=" + funciones.deTextoa64(idc_horario_perm));
        }
    }
}