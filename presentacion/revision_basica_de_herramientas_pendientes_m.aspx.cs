using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class revision_basica_de_herramientas_pendientes_m : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            Session["url_back"] = "revision_basica_de_herramientas_pendientes_m.aspx";
            CargarPendiente();
        }
        private void CargarPendiente()
        {
            Vehiculos_RevisionENT entidad = new Vehiculos_RevisionENT();
            Vehiculos_RevisionCOM com = new Vehiculos_RevisionCOM();
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            DataSet ds = com.CargarInfoPendienteCatalogo(entidad);
            gridpendientes.DataSource = ds.Tables[0];
            gridpendientes.DataBind();
        }
        protected void gridpendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string id = funciones.deTextoa64(gridpendientes.DataKeys[index].Values["idc_revbasherr"].ToString());
            Response.Redirect("revision_herramientas.aspx?tipo=P&rb=1&idc_pendiente="+id);

        }
    }
}