using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class pendientes_tareas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["redirect_pagedet"] = "pendientes_tareas.aspx";
            TareasENT entidad = new TareasENT();
            TareasCOM componente = new TareasCOM();
            entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
            DataSet ds = componente.CargarPendientes(entidad);
            gridservicios.DataSource = ds.Tables[0];
            gridservicios.DataBind();
        }

        protected void gridservicios_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string bc = rowView["backcolor"].ToString();
                string fc = rowView["forecolor"].ToString();
                e.Row.Cells[1].ForeColor = System.Drawing.Color.FromName(fc);
                e.Row.Cells[1].BackColor = System.Drawing.Color.FromName(bc);

            }
        }
    }
}