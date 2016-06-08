using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class entregas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
            int idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
            CargarGridPrincipal(idc_puesto);
        }

        /// <summary>
        /// Carga los datos en repeats
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal(int idc_puesto)
        {
            Puestos_EntregarENT entidad = new Puestos_EntregarENT();
            Puestos_EntregarCOM componente = new Puestos_EntregarCOM();
            entidad.Pidc_puesto = idc_puesto;
            DataSet ds = componente.CargaEntrega(entidad);
            if (ds.Tables[0].Rows.Count == 0)
            {
                NoPende.Visible = true;
            }
            repeatpendientes.DataSource = ds.Tables[0];
            repeatpendientes.DataBind();
            //RepeatMOdoLista.DataSource = ds.Tables[0];
            //RepeatMOdoLista.DataBind();
            //GeneraTablaReporte(ds.Tables[0]);
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        protected void repeatpendientes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton LNKGO = (LinkButton)e.Item.FindControl("lnkGO");
            LinkButton lnkIcon = (LinkButton)e.Item.FindControl("lnkIcon");
            Label lblTipo = (Label)e.Item.FindControl("lblTipoEnt");
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            Panel Panel = (Panel)e.Item.FindControl("PanelRevisionP");
            string idc_entrega = Convert.ToString(DataBinder.Eval(dbr, "idc_entrega"));
            string tipo_revision = Convert.ToString(DataBinder.Eval(dbr, "tipo_ent"));
            LNKGO.CommandName = idc_entrega;
            lnkIcon.CommandName = idc_entrega;
            if (tipo_revision.Equals("Vehiculos"))
            {
                Panel.CssClass = "small-box bg-red";
                lblTipo.Text = tipo_revision;
                lnkIcon.Text = "<i class='ion ion-model-s'></i>";
                lnkIcon.CommandName = idc_entrega;
                lnkIcon.CommandArgument = "V";
                LNKGO.CommandArgument = "V";
            }
            if (tipo_revision.Equals("Equipos Celular"))
            {
                Panel.CssClass = "small-box bg-yellow";
                LNKGO.CommandName = idc_entrega;
                lblTipo.Text = tipo_revision;
                lnkIcon.Text = "<i class='ion ion-iphone'></i>";
                lnkIcon.CommandName = idc_entrega;
                lnkIcon.CommandArgument = "C";
                LNKGO.CommandArgument = "C";
            }
            if (tipo_revision.Equals("Herramientas y Activos"))
            {
                LNKGO.CommandName = idc_entrega;
                lblTipo.Text = tipo_revision;
                lnkIcon.Text = "<i class='ion ion-laptop'></i>";
                lnkIcon.CommandName = idc_entrega;
                lnkIcon.CommandArgument = "H";
                LNKGO.CommandArgument = "H";
            }
            if (tipo_revision.Equals("Revisiones"))
            {
                Panel.CssClass = "small-box bg-blue";
                LNKGO.CommandName = idc_entrega;
                lblTipo.Text = tipo_revision;
                lnkIcon.Text = "<i class='ion ion-wrench'></i>";
                lnkIcon.CommandName = idc_entrega;
                lnkIcon.CommandArgument = "S";
                LNKGO.CommandArgument = "S";
            }
            if (tipo_revision.Equals("Cursos"))
            {
                Panel.CssClass = "small-box bg-navy";
                LNKGO.CommandName = idc_entrega;
                lblTipo.Text = tipo_revision;
                lnkIcon.Text = "<i class='ion ion-ios7-compose'></i>";
                lnkIcon.CommandName = idc_entrega;
                lnkIcon.CommandArgument = "U";
                LNKGO.CommandArgument = "U";
            }
        }

        protected void lnkIcon_Click(object sender, EventArgs e)
        {
            LinkButton lnkgo = (LinkButton)sender;
            string idc_entrega = lnkgo.CommandName.ToString();
            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
            if (lnkgo.CommandArgument.Equals("H"))
            {
                Response.Redirect("herramientas_entrega.aspx?idc_entrega=" + idc_entrega);
            }
            if (lnkgo.CommandArgument.Equals("C"))
            {
                Response.Redirect("celulares_entrega.aspx?idc_entrega=" + idc_entrega);
            }
            if (lnkgo.CommandArgument.Equals("V"))
            {
                Response.Redirect("vehiculos_entrega.aspx?idc_entrega=" + idc_entrega);
            }
            if (lnkgo.CommandArgument.Equals("S"))
            {
                Response.Redirect("revisiones_entrega.aspx?idc_entrega=" + idc_entrega);
            }
            if (lnkgo.CommandArgument.Equals("U"))
            {
                Response.Redirect("cursos_entrega.aspx?idc_entrega=" + idc_entrega);
            }
        }
    }
}