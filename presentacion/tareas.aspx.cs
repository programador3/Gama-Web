using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class tareas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                CargarTareasAsigne(0, 0);
                CargarTareasAsignadas(0, 0);
            }
        }

        /// <summary>
        /// Carga las tareas pendientes
        /// </summary>
        private void CargarTareasAsigne(int idc_puesto, int idc_depto)
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pidc_puesto_asigna = Convert.ToInt32(Session["sidc_puesto_login"]);
                entidad.Pidc_puesto = idc_puesto;
                entidad.Pidc_depto = idc_depto;
                DataSet ds = componente.CargarTareasAsigne(entidad);
                //Session["tabla_pendientes"] = ds.Tables[1];
                //repeat_pendientes.DataSource = ds.Tables[0];
                //repeat_pendientes.DataBind();
                if (idc_puesto > 0)
                {
                    Session["tabla_asiganadas"] = ds.Tables[1];
                    repeat_asignadas.DataSource = ds.Tables[0];
                    repeat_asignadas.DataBind();
                    panel_detalles.Visible = true;
                    panel_puestos.Visible = false;
                    panel_deptos.Visible = false;
                }
                if (idc_depto > 0)
                {
                    repeat_puestos.DataSource = ds.Tables[1];
                    repeat_puestos.DataBind();
                    panel_detalles.Visible = false;
                    panel_puestos.Visible = true;
                    panel_deptos.Visible = false;
                }
                if (idc_puesto == 0 && idc_depto == 0)
                {
                    repeat_deptos.DataSource = ds.Tables[1];
                    repeat_deptos.DataBind();
                    panel_detalles.Visible = false;
                    panel_puestos.Visible = false;
                    panel_deptos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga las tareas pendientes
        /// </summary>
        private void CargarTareasAsignadas(int idc_puesto_asigna, int idc_depto)
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pidc_puesto_asigna = idc_puesto_asigna;
                entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
                entidad.Pidc_depto = idc_depto;
                DataSet ds = componente.CargarTareasAsignadas(entidad);
                //Session["tabla_pendientes"] = ds.Tables[1];
                //repeat_pendientes.DataSource = ds.Tables[0];
                //repeat_pendientes.DataBind();
                if (idc_puesto_asigna > 0)
                {
                    Session["tabla_pendientes"] = ds.Tables[1];
                    repeat_pendientes.DataSource = ds.Tables[0];
                    repeat_pendientes.DataBind();
                    panel_detalles_mias.Visible = true;
                    panel_puestos_mias.Visible = false;
                    panel_deptos_mias.Visible = false;
                }
                if (idc_depto > 0)
                {
                    repeat_puestos_mias.DataSource = ds.Tables[1];
                    repeat_puestos_mias.DataBind();
                    panel_detalles_mias.Visible = false;
                    panel_puestos_mias.Visible = true;
                    panel_deptos_mias.Visible = false;
                }
                if (idc_puesto_asigna == 0 && idc_depto == 0)
                {
                    repeat_deptos_mias.DataSource = ds.Tables[1];
                    repeat_deptos_mias.DataBind();
                    panel_detalles_mias.Visible = false;
                    panel_puestos_mias.Visible = false;
                    panel_deptos_mias.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
        }

        protected void lnkmistarea_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string value = funciones.deTextoa64(lnk.CommandName.ToString());
            Response.Redirect("tareas_detalles.aspx?LECTURA=1&termina=1&idc_tarea=" + value);
        }

        protected void lnkmistarea_Click1(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string value = funciones.deTextoa64(lnk.CommandName.ToString());
            Response.Redirect("tareas_detalles.aspx?LECTURA=1&acepta=1&idc_tarea=" + value);
        }

        protected void repeat_asignadas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            Repeater repeat_mis_tareas_asignadas = (Repeater)e.Item.FindControl("repeat_mis_tareas_asignadas");
            DataTable dt = (DataTable)Session["tabla_asiganadas"];
            DataView view = dt.DefaultView;
            string tipo = (DataBinder.Eval(dbr, "tipo").ToString());
            view.RowFilter = "tipo_tarea = '" + tipo + "'";
            repeat_mis_tareas_asignadas.DataSource = view.ToTable();
            repeat_mis_tareas_asignadas.DataBind();
            HtmlGenericControl h3 = (HtmlGenericControl)e.Item.FindControl("descri");
            h3.Visible = view.Count == 0 ? false : true;
        }

        protected void repeat_pendientes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;

            Repeater repeat_mis_tareas = (Repeater)e.Item.FindControl("repeat_mis_tareas");
            DataTable dt = (DataTable)Session["tabla_pendientes"];
            DataView view = dt.DefaultView;
            string tipo = (DataBinder.Eval(dbr, "tipo").ToString());
            view.RowFilter = "tipo_tarea = '" + tipo + "'";
            repeat_mis_tareas.DataSource = view.ToTable();
            repeat_mis_tareas.DataBind();
            HtmlGenericControl h3 = (HtmlGenericControl)e.Item.FindControl("des");
            h3.Visible = view.Count == 0 ? false : true;
        }

        protected void lnkdeptos_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            int idc_depto = Convert.ToInt32(lnk.CommandName);
            CargarTareasAsigne(0, idc_depto);
            lnkregresa.CommandName = idc_depto.ToString();
        }

        protected void lnkpuestos_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            int idc_puesto = Convert.ToInt32(lnk.CommandName);
            CargarTareasAsigne(idc_puesto, 0);
        }

        protected void lnkregresa_Click(object sender, EventArgs e)
        {
            int idc_depto = Convert.ToInt32(lnkregresa.CommandName);
            CargarTareasAsigne(0, idc_depto);
        }

        protected void nlkreturn_Click(object sender, EventArgs e)
        {
            CargarTareasAsigne(0, 0);
        }

        protected void lnkdeptos_mias_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            int idc_depto = Convert.ToInt32(lnk.CommandName);
            CargarTareasAsignadas(0, idc_depto);
            lnkregresa_mias.CommandName = idc_depto.ToString();
        }

        protected void lnkreturn_mias_Click(object sender, EventArgs e)
        {
            CargarTareasAsignadas(0, 0);
        }

        protected void lnkpuestos_mias_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            int idc_puesto = Convert.ToInt32(lnk.CommandName);
            CargarTareasAsignadas(idc_puesto, 0);
        }

        protected void lnkregresa_mias_Click(object sender, EventArgs e)
        {
            int idc_depto = Convert.ToInt32(lnkregresa_mias.CommandName);
            CargarTareasAsignadas(0, idc_depto);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
                entidad.Pidc_puesto_asigna = 0;
                entidad.Pidc_depto = 0;
                entidad.preporte = true;
                DataSet ds = componente.CargarTareasAsignadas(entidad);
                DataTable dt = ds.Tables[1];
                if (dt.Rows.Count > 0)
                {
                    string attachment = "attachment; filename=lista.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.ms-excel;";
                    Response.ContentEncoding = System.Text.Encoding.Unicode;
                    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                    string tab = "";
                    foreach (DataColumn dc in dt.Columns)
                    {
                        Response.Write(tab + dc.ColumnName);
                        tab = "\t";
                    }
                    Response.Write("\n");
                    int i;
                    foreach (DataRow dr in dt.Rows)
                    {
                        tab = "";
                        for (i = 0; i < dt.Columns.Count; i++)
                        {
                            Response.Write(tab + dr[i].ToString());
                            tab = "\t";
                        }
                        Response.Write("\n");
                    }
                    Response.End();
                }
                else {
                    Alert.ShowAlertError("NO HAY DATOS",this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pidc_puesto_asigna = Convert.ToInt32(Session["sidc_puesto_login"]);
                entidad.Pidc_puesto = 0;
                entidad.Pidc_depto = 0;
                entidad.preporte = true;
                DataSet ds = componente.CargarTareasAsigne(entidad);
                DataTable dt = ds.Tables[1];
                if (dt.Rows.Count > 0)
                {
                    string attachment = "attachment; filename=lista.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.ms-excel;";
                    Response.ContentEncoding = System.Text.Encoding.Unicode;
                    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                    string tab = "";
                    foreach (DataColumn dc in dt.Columns)
                    {
                        Response.Write(tab + dc.ColumnName);
                        tab = "\t";
                    }
                    Response.Write("\n");
                    int i;
                    foreach (DataRow dr in dt.Rows)
                    {
                        tab = "";
                        for (i = 0; i < dt.Columns.Count; i++)
                        {
                            Response.Write(tab + dr[i].ToString());
                            tab = "\t";
                        }
                        Response.Write("\n");
                    }
                    Response.End();
                }
                else
                {
                    Alert.ShowAlertError("NO HAY DATOS", this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }

        }
    }
}