using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class quejas_espera_m: System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                CargarQuejas("");
            }
        }

        private void CargarQuejas(string filtro)
        {
            QuejasENT entidad = new QuejasENT();
            QuejasCOM componente = new QuejasCOM();
            DataSet ds = componente.CargaQuejas(entidad);
            if (filtro == "")
            {
                repeat.DataSource = ds.Tables[0];
                repeat.DataBind();
            }
            else
            {
                DataView view = ds.Tables[0].DefaultView;
                int i = 0;
                bool result = int.TryParse(filtro, out i); //i now = 108
                string query = result == true ? "idc_queja = " + filtro + " or problema like '%" + filtro + "%' or cliente like '%" + filtro + "%'" : "problema like '%" + filtro + "%' or cliente like '%" + filtro + "%'";
                view.RowFilter = query;
                repeat.DataSource = view.ToTable();
                repeat.DataBind();
            }
        }

        protected void lnksolu_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            string comman = lnk.CommandName;
            int cdi = Convert.ToInt32(lnk.CommandArgument);
            Response.Redirect("quejas_acciones.aspx?accion=" + comman + "&cdi=" + funciones.deTextoa64(cdi.ToString()));
        }

        protected void lnkinfo_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            string comman = lnk.CommandName;
            int cdi = Convert.ToInt32(lnk.CommandArgument);
            Response.Redirect("consulta_quejas_m.aspx?cdi=" + funciones.deTextoa64(cdi.ToString()));
        }

        protected void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtbuscar.Text;
            CargarQuejas(filtro);
        }
    }
}