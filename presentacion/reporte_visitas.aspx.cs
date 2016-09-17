using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class reporte_visitas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                txtfechafin.Text = DateTime.Now.ToString("yyyy-MM-dd").Replace(' ', 'T');
                txtfechainicio.Text = DateTime.Now.ToString("yyyy-MM-dd").Replace(' ', 'T');
                CargaPuestos("");
            }
        }

        /// <summary>
        /// Carga Puestos en Filtro
        /// </summary>
        public void CargaPuestos(string filtro)
        {
            try
            {
                Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
                Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
                entidad.Filtro = filtro;
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = componente.CargaComboDinamicoOrgn(entidad);
                ddlPuestoAsigna.DataValueField = "idc_empleado";
                ddlPuestoAsigna.DataTextField = "descripcion_puesto_completa";
                ddlPuestoAsigna.DataSource = ds.Tables[0];
                ddlPuestoAsigna.DataBind();
                if (filtro == "")
                {
                    ddlPuestoAsigna.Items.Insert(0, new ListItem("--Seleccione un Empleado", "0")); //updated code}
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void CargarVisitas(int idc_empleado, DateTime dt1, DateTime dt2)
        {
            try
            {
                VisitasENT entidad = new VisitasENT();
                VisitasCOM componente = new VisitasCOM();
                entidad.Pidc_empleado = idc_empleado;
                entidad.pfi = dt1;
                entidad.pf2 = dt2;
                DataSet ds = componente.CragarVisitasReporte(entidad);
                gridvisitas.DataSource = ds.Tables[0];
                gridvisitas.DataBind();
                Session["tblexcel"] = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void ddlPuestoAsigna_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc = ddlPuestoAsigna.SelectedValue == "" ? 0 : Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            CargaPuestos(txtpuesto_filtro.Text);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int idc = ddlPuestoAsigna.SelectedValue == "" ? 0 : Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
            CargarVisitas(idc, Convert.ToDateTime(txtfechainicio.Text), Convert.ToDateTime(txtfechafin.Text));
        }

        protected void lnkurladicinal_Click(object sender, EventArgs e)
        {
            if (Session["tblexcel"] != null)
            {
                int idc = ddlPuestoAsigna.SelectedValue == "" ? 0 : Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
                CargarVisitas(idc, Convert.ToDateTime(txtfechainicio.Text), Convert.ToDateTime(txtfechafin.Text));
                DataTable dt = (DataTable)Session["tblexcel"];
                dt.Columns.RemoveAt(0);// nombre de tabla
                dt.Columns.RemoveAt(7);// nombre de tabla
                dt.Columns.RemoveAt(7);// nombre de tabla
                Export Export = new Export();
                //array de DataTables
                List<DataTable> ListaTables = new List<DataTable>();
                ListaTables.Add(dt);
                //array de nombre de sheets
                string[] Nombres = new string[] { "Visitas" };
                if (dt.Rows.Count == 0)
                {
                    Alert.ShowAlertInfo("Este Puesto no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
                }
                else
                {
                    string mensaje = Export.toExcel("Visitas", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                                       XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 1,
                                       "visita.xlsx", Page.Response);
                    if (mensaje != "")
                    {
                        Alert.ShowAlertError(mensaje, this);
                    }
                }
            }
        }
    }
}