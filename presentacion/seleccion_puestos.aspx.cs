using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class seleccion_puestos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            GenerarData();
        }

        public void GenerarData()
        {
            CandidatosENT entidad = new CandidatosENT();
            CandidatosCOM componente = new CandidatosCOM();
            entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
            DataSet ds = componente.DatosSeleccion(entidad);
            int total = ds.Tables[0].Rows.Count;
            if (total > 0)
            {
                repeatpendientes.DataSource = ds.Tables[0];
                repeatpendientes.DataBind();
                NoPende.Visible = false;
            }
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        protected void lnkGO_Click(object sender, EventArgs e)
        {
            LinkButton lnkGO = (LinkButton)sender;
            //PanelDetalles.Visible = true;
            // DataPrebajasDetalles(Convert.ToInt32(lnkGO.CommandName.ToString()), Convert.ToInt32(lnkGO.CommandArgument.ToString()));
            // lnklista.Visible = true;
            Response.Redirect("seleccion_candidato.aspx?idc_puesto=" + lnkGO.CommandArgument.ToString() + "&idc_prepara=" + lnkGO.CommandName.ToString());
        }

        protected void repeatpendientes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            //Panel Panel = (Panel)e.Item.FindControl("panel_revision");
            string idc_puesto = Convert.ToString(DataBinder.Eval(dbr, "idc_puesto"));
            string idc_prepara = Convert.ToString(DataBinder.Eval(dbr, "idc_prepara"));
            LinkButton lnkGOdET = (LinkButton)e.Item.FindControl("lnkGOdET");
            LinkButton lnkGO = (LinkButton)e.Item.FindControl("lnkGO");
            //asiganmos datos
            lnkGO.CommandArgument = idc_puesto;
            lnkGOdET.CommandArgument = idc_puesto;
            lnkGO.CommandName = idc_prepara;
            lnkGOdET.CommandName = idc_prepara;
        }
    }
}