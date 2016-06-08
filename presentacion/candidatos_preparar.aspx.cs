using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class candidatos_preparar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!Page.IsPostBack)
            {
                DataPrep();
            }
            H1.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"].ToString()), 348);
        }

        /// <summary>
        /// Carga los puestos pendientes de preparar
        /// </summary>
        public void DataPrep()
        {
            CandidatosENT entidad = new CandidatosENT();
            CandidatosCOM componente = new CandidatosCOM();
            entidad.Pidc_puesto = 0;
            entidad.Pidc_prepara = 0;
            entidad.Pidc_puestobaja = Convert.ToInt32(Session["sidc_puesto_login"]);
            DataSet ds = componente.CargaPuestos(entidad);
            repeatpendientes.DataSource = ds.Tables[0];
            repeatpendientes.DataBind();
            if (ds.Tables[0].Rows.Count == 0) { Noempleados.Visible = true; }
        }

        protected void lnkGO_Click(object sender, EventArgs e)
        {
            LinkButton lnkGO = (LinkButton)sender;
            //PanelDetalles.Visible = true;
            // DataPrebajasDetalles(Convert.ToInt32(lnkGO.CommandName.ToString()), Convert.ToInt32(lnkGO.CommandArgument.ToString()));
            // lnklista.Visible = true;
            Response.Redirect("candidatos_preparar_captura.aspx?idc_puesto=" + lnkGO.CommandArgument.ToString() + "&idc_prepara=" + lnkGO.CommandName.ToString());
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

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] == null)
            {
                Response.Redirect("administrador_prebajas.aspx");
            }
            else
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }
    }
}