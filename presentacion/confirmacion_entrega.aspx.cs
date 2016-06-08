using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class confirmacion_entrega : System.Web.UI.Page
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
            try
            {
                Puestos_EntregarENT entidad = new Puestos_EntregarENT();
                Puestos_EntregarCOM componente = new Puestos_EntregarCOM();
                entidad.Pidc_puesto = idc_puesto;
                DataSet ds = componente.CargaConfirmaciones(entidad);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    NoPende.Visible = true;
                }
                repeatpendientes.DataSource = ds.Tables[0];
                repeatpendientes.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
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

        protected void repeatpendientes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton LNKGO = (LinkButton)e.Item.FindControl("lnkGO");
            LinkButton lnkIcon = (LinkButton)e.Item.FindControl("lnkIcon");
            Label lblTipo = (Label)e.Item.FindControl("lblTipoEnt");
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            Panel Panel = (Panel)e.Item.FindControl("PanelRevisionP");
            string tipo_revision = Convert.ToString(DataBinder.Eval(dbr, "tipo_ent"));
            if (tipo_revision.Equals("Vehiculos"))
            {
                Panel.CssClass = "small-box bg-red";
                lblTipo.Text = tipo_revision;
                lnkIcon.Text = "<i class='ion ion-model-s'></i>";
                lnkIcon.CommandArgument = "V";
                LNKGO.CommandArgument = "V";
            }
            if (tipo_revision.Equals("Equipos Celular"))
            {
                Panel.CssClass = "small-box bg-yellow";
                lblTipo.Text = tipo_revision;
                lnkIcon.Text = "<i class='ion ion-iphone'></i>";
                lnkIcon.CommandArgument = "C";
                LNKGO.CommandArgument = "C";
            }
            if (tipo_revision.Equals("Herramientas y Activos"))
            {
                lblTipo.Text = tipo_revision;
                lnkIcon.Text = "<i class='ion ion-calculator'></i>";
                lnkIcon.CommandArgument = "H";
                LNKGO.CommandArgument = "H";
            }
            if (tipo_revision.Equals("Revisiones"))
            {
                Panel.CssClass = "small-box bg-blue";
                lblTipo.Text = tipo_revision;
                lnkIcon.Text = "<i class='ion ion-laptop'></i>";
                lnkIcon.CommandArgument = "S";
                LNKGO.CommandArgument = "S";
            }
            if (tipo_revision.Equals("Cursos"))
            {
                Panel.CssClass = "small-box bg-navy";
                lblTipo.Text = tipo_revision;
                lnkIcon.Text = "<i class='ion ion-ios7-compose'></i>";
                lnkIcon.CommandArgument = "U";
                LNKGO.CommandArgument = "U";
            }
        }

        protected void lnkIcon_Click(object sender, EventArgs e)
        {
            LinkButton lnkgo = (LinkButton)sender;
            int idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
            if (lnkgo.CommandArgument.Equals("H"))
            {
                Response.Redirect("confirmar_herramientas.aspx?idc_puesto=" + idc_puesto);
            }
            if (lnkgo.CommandArgument.Equals("C"))
            {
                Response.Redirect("confirmar_celulares.aspx?idc_puesto=" + idc_puesto);
            }
            if (lnkgo.CommandArgument.Equals("V"))
            {
                Response.Redirect("confirmar_vehiculos.aspx?idc_puesto=" + idc_puesto);
            }
            if (lnkgo.CommandArgument.Equals("S"))
            {
                Response.Redirect("confirmar_revisiones.aspx?idc_puesto=" + idc_puesto);
            }
            if (lnkgo.CommandArgument.Equals("U"))
            {
                Response.Redirect("cursos_entrega.aspx?idc_puesto=" + idc_puesto);
            }
        }
    }
}