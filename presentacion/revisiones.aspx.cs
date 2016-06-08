using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class revisiones : System.Web.UI.Page
    {
        public string rutaimagen = "";//para controles dinamicos
        public int idc_usuarioprebaja = 0;
        public int idc_prebaja = 0;
        public int idc_revision = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
            int idc_puestorev = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
            CargarGridPrincipal(idc_usuario, idc_puestorev);
        }

        /// <summary>
        /// Carga los datos en repeats
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal(int idc_usuario, int idc_puestorevi)
        {
            try
            {
                RevisionesENT entidad = new RevisionesENT();
                RevisionesCOM componente = new RevisionesCOM();
                entidad.Idc_puestorevi = idc_puestorevi;
                DataSet ds = componente.CargaHerramientasRevision(entidad);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    NoPende.Visible = true;
                }
                repeatpendientes.DataSource = ds.Tables[0];
                repeatpendientes.DataBind();
                RepeatMOdoLista.DataSource = ds.Tables[0];
                RepeatMOdoLista.DataBind();
                GeneraTablaReporte(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void GeneraTablaReporte(DataTable table)
        {
            DataTable tablafinal = new DataTable();
            tablafinal.Columns.Add("Nomina");
            tablafinal.Columns.Add("Empleado");
            tablafinal.Columns.Add("Puesto");
            tablafinal.Columns.Add("Revision");
            tablafinal.Columns.Add("Fecha de pre-baja");
            foreach (DataRow row in table.Rows)
            {
                DataRow new_row = tablafinal.NewRow();
                new_row["Nomina"] = row["num_nomina"].ToString();
                new_row["Empleado"] = row["empleado"].ToString();
                new_row["Puesto"] = row["descripcion"].ToString();
                new_row["Revision"] = row["tipo_revision"].ToString();
                new_row["Fecha de pre-baja"] = row["fecha"].ToString();
                tablafinal.Rows.Add(new_row);
            }
            Session["Tabla_Reportes"] = tablafinal;
        }

        protected void repeatpendientes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton LNKGO = (LinkButton)e.Item.FindControl("lnkGO");
            LinkButton LNKVEHICULOS = (LinkButton)e.Item.FindControl("lnkVehiculos");
            LinkButton LNKGOCELULAR = (LinkButton)e.Item.FindControl("lnkCelulares");
            LinkButton lnkGoServicios = (LinkButton)e.Item.FindControl("lnkGoServicios");
            LinkButton lnkFinal = (LinkButton)e.Item.FindControl("lnkFinal");
            LinkButton lnkIcon = (LinkButton)e.Item.FindControl("lnkIcon");
            Label lblTipo = (Label)e.Item.FindControl("lblTipoRev");
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            Panel Panel = (Panel)e.Item.FindControl("PanelRevisionP");
            string idc_empleado = Convert.ToString(DataBinder.Eval(dbr, "idc_puesto"));
            string tipo_revision = Convert.ToString(DataBinder.Eval(dbr, "tipo_revision"));
            if (tipo_revision.Equals("Vehiculos"))
            {
                Panel.CssClass = "small-box bg-red";
                LNKGO.Visible = false;
                LNKVEHICULOS.Visible = true;
                LNKVEHICULOS.CommandName = idc_empleado;
                lblTipo.Text = "Vehiculos";
                lnkIcon.Text = "<i class='ion ion-model-s'></i>";
                lnkIcon.CommandName = idc_empleado;
                lnkIcon.CommandArgument = "V";
            }
            if (tipo_revision.Equals("Equipos Celular"))
            {
                Panel.CssClass = "small-box bg-yellow";
                LNKGO.Visible = false;
                LNKGOCELULAR.Visible = true;
                LNKGOCELULAR.CommandName = idc_empleado;
                lblTipo.Text = "Equipos Celulares";
                lnkIcon.Text = "<i class='ion ion-iphone'></i>";
                lnkIcon.CommandName = idc_empleado;
                lnkIcon.CommandArgument = "C";
            }
            if (tipo_revision.Equals("Herramientas y Activos"))
            {
                LNKGO.CommandName = idc_empleado;
                lblTipo.Text = "Herramientas y Activos";
                lnkIcon.Text = "<i class='ion ion-laptop'></i>";
                lnkIcon.CommandName = idc_empleado;
                lnkIcon.CommandArgument = "H";
            }
            if (tipo_revision.Equals("Revisiones"))
            {
                Panel.CssClass = "small-box bg-blue";
                LNKGO.Visible = false;
                lnkGoServicios.Visible = true;
                lnkGoServicios.CommandName = idc_empleado;
                lblTipo.Text = "Revisiones";
                lnkIcon.Text = "<i class='ion ion-wrench'></i>";
                lnkIcon.CommandName = idc_empleado;
                lnkIcon.CommandArgument = "S";
            }
            if (tipo_revision.Equals("Revisiones Finales"))
            {
                Panel.CssClass = "small-box bg-maroon";
                LNKGO.Visible = false;
                lnkGoServicios.Visible = true;
                lnkGoServicios.CommandName = idc_empleado;
                lblTipo.Text = tipo_revision;
                lnkIcon.Text = "<i class='ion ion-laptop'></i>";
                lnkIcon.CommandName = idc_empleado;
                lnkIcon.CommandArgument = "S";
            }
            if (tipo_revision.Equals("Validacion Pre-Final"))
            {
                Panel.CssClass = "small-box bg-purple";
                LNKGO.Visible = false;
                lnkFinal.Visible = true;
                lnkFinal.CommandName = idc_empleado;
                lblTipo.Text = tipo_revision;
                lnkIcon.Text = "<i class='ion ion-eye'></i>";
                lnkIcon.CommandName = idc_empleado;
                lnkIcon.CommandArgument = "F";
            }
        }

        protected void lnkGO_Click(object sender, EventArgs e)
        {
            LinkButton lnkgo = (LinkButton)sender;
            string idc_empleado = lnkgo.CommandName.ToString();
            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
            Response.Redirect("herramientas_revision.aspx?idc_puestoprebaja=" + idc_empleado);
        }

        protected void LnkGOCelulare_Click(object sender, EventArgs e)
        {
            LinkButton lnkgo = (LinkButton)sender;
            string idc_empleado = lnkgo.CommandName.ToString();
            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
            Response.Redirect("celulares_revision.aspx?idc_puestoprebaja=" + idc_empleado);
        }

        protected void lnkVehiculos_Click(object sender, EventArgs e)
        {
            LinkButton lnkgo = (LinkButton)sender;
            string idc_empleado = lnkgo.CommandName.ToString();
            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
            Response.Redirect("vehiculos_revision.aspx?idc_puestoprebaja=" + idc_empleado);
        }

        protected void lnkIcon_Click(object sender, EventArgs e)
        {
            LinkButton lnkgo = (LinkButton)sender;
            string idc_empleado = lnkgo.CommandName.ToString();

            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
            if (lnkgo.CommandArgument.Equals("H"))
            {
                Response.Redirect("herramientas_revision.aspx?idc_puestoprebaja=" + idc_empleado);
            }
            if (lnkgo.CommandArgument.Equals("C"))
            {
                Response.Redirect("celulares_revision.aspx?idc_puestoprebaja=" + idc_empleado);
            }
            if (lnkgo.CommandArgument.Equals("V"))
            {
                Response.Redirect("vehiculos_revision.aspx?idc_puestoprebaja=" + idc_empleado);
            }
            if (lnkgo.CommandArgument.Equals("S"))
            {
                Response.Redirect("servicios_revisiones.aspx?idc_puestoprebaja=" + idc_empleado);
            }
            if (lnkgo.CommandArgument.Equals("F"))
            {
                Response.Redirect("revision_final.aspx?idc_puestoprebaja=" + idc_empleado);
            }
        }

        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            DataTable tabla_reporte = (DataTable)Session["Tabla_Reportes"];
            Export Export = new Export();
            // Lista de DT
            List<DataTable> ListaTables = new List<DataTable>();
            ListaTables.Add(tabla_reporte);
            //array de nombre de sheets
            string[] Nombres = new string[] { "Pendientes de Revisión" };
            if (tabla_reporte.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("No cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
            }
            else
            {
                string mensaje = Export.ToPdf("revisiones", ListaTables, 1, Nombres, Page.Response);
                if (mensaje != "")
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }

        protected void lnkGuardarTodo_Click(object sender, EventArgs e)
        {
            DataTable tabla_reporte = (DataTable)Session["Tabla_Reportes"];
            Export Export = new Export();
            // Lista de DT
            List<DataTable> ListaTables = new List<DataTable>();
            ListaTables.Add(tabla_reporte);
            //array de nombre de sheets
            string[] Nombres = new string[] { "Pendientes de Revisión" };
            if (tabla_reporte.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("No cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
            }
            else
            {
                string mensaje = Export.toExcel("Pendientes de Revisión", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                                   XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 1,
                                   "Listado_de_Revisiones.xlsx", Page.Response);
                if (mensaje != "")
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }

        protected void lnkW8_Click(object sender, EventArgs e)
        {
            PanelModoLista.Visible = false;
            PanelModoW8.Visible = true;
            lnklista.CssClass = "btn btn-link";
            lnkW8.CssClass = "btn btn-primary active";
        }

        protected void lnklista_Click(object sender, EventArgs e)
        {
            PanelModoLista.Visible = true;
            PanelModoW8.Visible = false;
            lnkW8.CssClass = "btn btn-link";
            lnklista.CssClass = "btn btn-primary active";
        }

        protected void RepeatMOdoLista_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            Panel Panel = (Panel)e.Item.FindControl("PanelRevisionP");
            string idc_empleado = Convert.ToString(DataBinder.Eval(dbr, "idc_puesto"));
            string tipo_revision = Convert.ToString(DataBinder.Eval(dbr, "tipo_revision"));
            LinkButton lnkGORev = (LinkButton)e.Item.FindControl("lnkGORev");
            Label lblTipeIconRev = (Label)e.Item.FindControl("lblTipeIconRev");
            lnkGORev.CommandName = idc_empleado;
            if (tipo_revision.Equals("Vehiculos"))
            {
                lnkGORev.CommandArgument = "V";
                lblTipeIconRev.Text = "<i class='fa fa-car'></i> ";
            }
            if (tipo_revision.Equals("Equipos Celular"))
            {
                lnkGORev.CommandArgument = "C";
                lblTipeIconRev.Text = "<i class='fa fa-mobile'></i> ";
            }
            if (tipo_revision.Equals("Herramientas y Activos"))
            {
                lnkGORev.CommandArgument = "H";
                lblTipeIconRev.Text = "<i class='fa fa-briefcase'></i> ";
            }
            if (tipo_revision.Equals("Servicios"))
            {
                lnkGORev.CommandArgument = "S";
                lblTipeIconRev.Text = "<i class='fa fa-laptop'></i> ";
            }
        }

        protected void lnkGORev_Click(object sender, EventArgs e)
        {
            LinkButton lnkgo = (LinkButton)sender;
            string idc_empleado = lnkgo.CommandName.ToString();
            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
            if (lnkgo.CommandArgument.Equals("H"))
            {
                Response.Redirect("herramientas_revision.aspx?idc_puestoprebaja=" + idc_empleado);
            }
            if (lnkgo.CommandArgument.Equals("C"))
            {
                Response.Redirect("celulares_revision.aspx?idc_puestoprebaja=" + idc_empleado);
            }
            if (lnkgo.CommandArgument.Equals("V"))
            {
                Response.Redirect("vehiculos_revision.aspx?idc_puestoprebaja=" + idc_empleado);
            }
            if (lnkgo.CommandArgument.Equals("S"))
            {
                Response.Redirect("servicios_revisiones.aspx?idc_puestoprebaja=" + idc_empleado);
            }
        }

        protected void lnkGoServicios_Click(object sender, EventArgs e)
        {
            LinkButton lnkgo = (LinkButton)sender;
            string idc_empleado = lnkgo.CommandName.ToString();
            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
            Response.Redirect("servicios_revisiones.aspx?idc_puestoprebaja=" + idc_empleado);
        }

        protected void lnkFinal_Click(object sender, EventArgs e)
        {
            LinkButton lnkgo = (LinkButton)sender;
            string idc_empleado = lnkgo.CommandName.ToString();
            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
            Response.Redirect("revision_final.aspx?idc_puestoprebaja=" + idc_empleado);
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }
    }
}