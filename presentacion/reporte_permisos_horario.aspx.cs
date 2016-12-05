using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class solicitudes_pendientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                ViewState["dt"] = null;
                txtfinicio.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                txtffin.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }


   
        private void Carga(string filtro)
        {
            if (txtfinicio.Text == "" || txtffin.Text == "")
            {
                Alert.ShowAlertInfo("Ingrese ambas fecha para realizar ejecutar un reporte", "Mensaje del Sistema", this);
            }
            else
            {
                Solicitud_HorarioENT entidad = new Solicitud_HorarioENT();
                SolicitudHorarioCOM componente = new SolicitudHorarioCOM();
                DateTime f1 = Convert.ToDateTime(txtfinicio.Text);
                DateTime f2 = Convert.ToDateTime(txtffin.Text);
                DataTable dt = componente.sp_solicitud_permiso_horarios_reporte(f1, f2).Tables[0];
                if (filtro == "")
                {
                    gridcelulares.DataSource = dt;
                    gridcelulares.DataBind();
                    lbltotal.Text = dt.Rows.Count.ToString();
                    ViewState["dt"] = dt;
                }
                else
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "estado like '%" + filtro + "%' or incidencia like '%" + filtro + "%' or empleado like '%" + filtro + "%' OR fecha like '%" + filtro + "%' OR empleado_solicito like '%" + filtro + "%' OR observaciones like '%" + filtro + "%'";
                    gridcelulares.DataSource = dv.ToTable();
                    gridcelulares.DataBind();
                    lbltotal.Text = dv.ToTable().Rows.Count.ToString();

                    ViewState["dt"] = dv.ToTable();
                }
            }
            
        }

        protected void gridcelulares_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int num_nomina = Convert.ToInt32(gridcelulares.DataKeys[index].Values["num_nomina"].ToString());
            int idc_horario_perm = Convert.ToInt32(gridcelulares.DataKeys[index].Values["idc_horario_perm"].ToString());
            int idc_empleado = Convert.ToInt32(gridcelulares.DataKeys[index].Values["idc_empleado"].ToString());
            DateTime fecha = Convert.ToDateTime(gridcelulares.DataKeys[index].Values["fecha_normal"].ToString());
            string empleado = gridcelulares.DataKeys[index].Values["empleado"].ToString();
            try
            {
                txtidc_empleado.Text = idc_empleado.ToString().Trim();
                txtnumeronomina.Text = num_nomina.ToString().Trim();
                txtempleado.Text = empleado;
                grid.Visible = true;
                nohay.Visible = false;

                txtincid.Visible = false;
                string estatus = "modal fade modal-success";
                txtstaus.Text = "Asistencia";
                txtstaus.BackColor = System.Drawing.Color.FromName("#1ABC9C");
                txtstaus.ForeColor = System.Drawing.Color.FromName("#fff");
                AsistenciaCOM componente = new AsistenciaCOM();
                string date = fecha.ToString("MM/dd/yyyy");

                DataSet ds = componente.sp_status_incidencia_dia_numnomina(num_nomina, fecha);
                DataTable dtstatus = ds.Tables[0];
                string color = dtstatus.Rows[0]["color"].ToString();
                if (color.ToString() == "255")
                {
                    estatus = "modal fade modal-danger";
                    txtstaus.Text = dtstatus.Rows[0]["estatus"].ToString();
                    txtstaus.BackColor = System.Drawing.Color.FromName("#FA2A00");
                    txtstaus.ForeColor = System.Drawing.Color.FromName("#fff");
                }
                if (dtstatus.Rows.Count > 1)
                {
                    txtincid.Visible = true;
                    txtincid.Text = dtstatus.Rows[1]["estatus"].ToString();
                    txtincid.BackColor = System.Drawing.Color.FromName("#FA2A00");
                    txtincid.ForeColor = System.Drawing.Color.FromName("#fff");
                }
                DataSet ds2 = componente.sp_ver_asistencia_detalle(num_nomina, fecha);
                DataTable dt = ds2.Tables[0];
                grid.DataSource = dt;
                grid.DataBind();
                if (dt.Rows[0]["fecha_rnd"].ToString() == "")
                {
                    grid.Visible = false;
                    nohay.Visible = true;
                }
                DataTable dtdetalles = ViewState["dt"] as DataTable;
                DataView dvdet = dtdetalles.DefaultView;
                dvdet.RowFilter = "idc_horario_perm ="+ idc_horario_perm + "";
                if (dvdet.ToTable().Rows.Count > 0)
                {
                    griddetalles_permiso.DataSource = dvdet.ToTable();
                    griddetalles_permiso.DataBind();
                    cbxnocomida.Checked = Convert.ToBoolean(dvdet.ToTable().Rows[0]["no_comida"]);
                    cbxnosalida.Checked = Convert.ToBoolean(dvdet.ToTable().Rows[0]["no_salida"]);
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMe223222eee334333ssage",
                    "ModalConfirms('" + estatus + "');", true);
                Carga(txtfiltrar.Text);

            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
             
        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            Carga("");
        }

        protected void gridcelulares_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string fcolor = rowView["fcolor"].ToString();
                string bcolor = rowView["bcolor"].ToString();
                e.Row.Cells[gridcelulares.Columns.Count - 1].ForeColor =System.Drawing.Color.FromName(fcolor);
                e.Row.Cells[gridcelulares.Columns.Count - 1].BackColor = System.Drawing.Color.FromName(bcolor);
            }
        }

        protected void lbkbuscar_Click(object sender, EventArgs e)
        {

            Carga(txtfiltrar.Text);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (ViewState["dt"] == null)
            {
                Alert.ShowAlertError("NO SE ENCONTRO NINGUN DATO. CONTACTE AL DEPTO DE SISTEMAS.", this);
                return;
            }
            DataTable dt = ViewState["dt"] as DataTable;

            if (dt.Rows.Count > 0)
            {
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(0);               
                dt.Columns["no_comida"].ColumnName = "No Marcara Incidencia en Comida";
                dt.Columns["no_salida"].ColumnName = "No Marcara Incidencia en Salida";
                Export Export = new Export();
                // Lista de DT
                System.Collections.Generic.List<DataTable> ListaTables = new System.Collections.Generic.List<DataTable>();
                ListaTables.Add(dt);
                //array de nombre de sheets
                string[] Nombres = new string[] { "Lista de Permisos" };
                if (dt.Rows.Count == 0)
                {
                    Alert.ShowAlertInfo("Este Puesto no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
                }
                else
                {
                    string mensaje = Export.toExcel("Solicitudes de Cambio Horario", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString("dd MMMM, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")), XLColor.White,
                                    XLColor.Black, 10, ListaTables, XLColor.Gray, XLColor.White, Nombres, 1,
                                    "Listado_De_solicitudes.xlsx", Page.Response);
                    if (mensaje != "")
                    {
                        Alert.ShowAlertError(mensaje, this);
                    }
                }
            }
            else
            {
                Alert.ShowAlertError("NO SE ENCONTRO NINGUN DATO. CONTACTE AL DEPTO DE SISTEMAS.",this);
            }

        }
    }
}