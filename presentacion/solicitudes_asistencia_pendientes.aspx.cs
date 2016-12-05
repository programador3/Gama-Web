using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class solicitudes_asistencia_pendientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IDC_SOLICITUDASI");
                dt.Columns.Add("IDC_EMPLEADO");
                dt.Columns.Add("fecha");
                dt.Columns.Add("TEMPRANO");
                dt.Columns.Add("empleado");
                dt.Columns.Add("num_nomina");
                ViewState["dt_pendi_auto"] = dt;
                CargarFaltas("");
                if (Request.QueryString["filtro"] != null)
                {
                    txtfiltrar.Text = funciones.de64aTexto(Request.QueryString["filtro"]);
                    CargarFaltas(txtfiltrar.Text);
                }
            }
        }

        string cadena()
        {
            string ret = "";
            DataTable dt = ViewState["dt_pendi_auto"] as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                string idc_sol = row["IDC_SOLICITUDASI"].ToString();
                string IDC_EMPLEADO = row["IDC_EMPLEADO"].ToString();
                DateTime fecha = Convert.ToDateTime(row["fecha"]);
                string TEMPRANO = Convert.ToBoolean(row["TEMPRANO"])?"1":"0";
                string nombre = row["empleado"].ToString();
                string num_nomina = row["num_nomina"].ToString();
                ret = ret + idc_sol + ";" + IDC_EMPLEADO + ";" + fecha.ToString("yyyy-dd-MM HH:mm:ss") + ";" + TEMPRANO + ";" +
                    nombre + ";" + num_nomina + ";"; 
            }
            return ret;
        }
        int totalcadena()
        {
            DataTable dt = ViewState["dt_pendi_auto"] as DataTable;           
            return dt.Rows.Count;
        }
        void adddata(string IDC_SOLICITUDASI, string IDC_EMPLEADO, DateTime fecha, string TEMPRANO, string nombre, string num_nomina)
        {
            DataTable dt = ViewState["dt_pendi_auto"] as DataTable;
            DataRow row = dt.NewRow();
            row["idc_solicitudasi"] = IDC_SOLICITUDASI;
            row["idc_empleado"] = IDC_EMPLEADO;
            row["fecha"] = fecha;
            row["temprano"] = TEMPRANO;
            row["empleado"] = nombre;
            row["num_nomina"] = num_nomina;
            dt.Rows.Add(row);
            ViewState["dt_pendi_auto"] = dt;
        }


        void deletedata(string IDC_SOLICITUDASI)
        {

            DataTable dt = ViewState["dt_pendi_auto"] as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                string idc_sol = row["IDC_SOLICITUDASI"].ToString();
                if (IDC_SOLICITUDASI.Trim() == idc_sol.Trim())
                {
                    row.Delete();
                    break;
                }
            }
            ViewState["dt_pendi_auto"] = dt;
        }
        bool Exists(string query)
        {
            DataTable dt = ViewState["dt_pendi_auto"] as DataTable;
            DataView dv = dt.DefaultView;
            dv.RowFilter = query;
            int total = dv.ToTable().Rows.Count;
            return (total > 0);
        }

        public void CargarFaltas(string value)
        {
            try
            {
                AsistenciaCOM componente = new AsistenciaCOM();
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                int idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
                DataSet ds = componente.sp_solicitudes_pendientes_asistencia();
                DataView dv = ds.Tables[0].DefaultView;
                string QUERY = "empleado like '%" + value + "%' OR  PUESTO like '%" + value +
                    "%' OR  DEPTO like '%" + value + "%'OR USUARIO like '%" + value + "%'OR HORA_CHECK like '%" + value + "%'";
                if (funciones.isNumeric(value))
                {
                    QUERY = QUERY + "or num_nomina = "+value+"";
                }
                dv.RowFilter =QUERY;
                DataTable dt = dv.ToTable();
                gridservicios.DataSource = dt;
                gridservicios.DataBind();
                lbltotal.Text = dt.Rows.Count.ToString();
                cbxselecttodos.Visible = true;
                lnkauto.Visible = true;
                lnkcanclar.Visible = true;
                if (dt.Rows.Count == 0)
                {
                    cbxselecttodos.Visible = false;
                    lnkauto.Visible = false;
                    lnkcanclar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void cbxselecttodos_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbx = sender as CheckBox;
            if (cbx.Checked)
            {
                try
                {
                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("idc_solicitudasi");
                    dt2.Columns.Add("idc_empleado");
                    dt2.Columns.Add("fecha");
                    dt2.Columns.Add("temprano");
                    dt2.Columns.Add("empleado");
                    dt2.Columns.Add("num_nomina");
                    ViewState["dt_pendi_auto"] = dt2;
                    AsistenciaCOM componente = new AsistenciaCOM();
                    int idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    int idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
                    DataSet ds = componente.sp_solicitudes_pendientes_asistencia();
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        int IDC_SOLICITUDASI = Convert.ToInt32(row["idc_solicitudasi"]);
                        //string idc_sol = row["IDC_SOLICITUDASI"].ToString();
                        string IDC_EMPLEADO = row["idc_empleado"].ToString();
                        DateTime fecha = Convert.ToDateTime(row["fecha"]);
                        string TEMPRANO = row["temprano"].ToString();
                        string nombre = row["empleado"].ToString();
                        string num_nomina = row["num_nomina"].ToString();
                        adddata(IDC_SOLICITUDASI.ToString().Trim(), IDC_EMPLEADO.ToString(), fecha, TEMPRANO, nombre, num_nomina.ToString());
                    }
                    lbltotal.Text = dt.Rows.Count.ToString();
                }
                catch (Exception ex)
                {
                    Alert.ShowAlertError(ex.ToString(), this.Page);
                    Global.CreateFileError(ex.ToString(), this);
                }
            }
            else
            {
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("idc_solicitudasi");
                dt2.Columns.Add("idc_empleado");
                dt2.Columns.Add("fecha");
                dt2.Columns.Add("temprano");
                dt2.Columns.Add("empleado");
                dt2.Columns.Add("num_nomina");
                ViewState["dt_pendi_auto"] = dt2;
            }
            txtfiltrar.Text = "";
            CargarFaltas("");
        }
        protected void cbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbx = sender as CheckBox;
            GridView grid = (GridView)((CheckBox)sender).Parent.Parent.Parent.Parent;
            GridViewRow currentRow = (GridViewRow)((CheckBox)sender).Parent.Parent;
            int index = Convert.ToInt32(currentRow.RowIndex);
            int IDC_SOLICITUDASI = Convert.ToInt32(gridservicios.DataKeys[index].Values["idc_solicitudasi"]);
            int idc_empleado = Convert.ToInt32(gridservicios.DataKeys[index].Values["idc_empleado"]);
            int num_nomina = Convert.ToInt32(gridservicios.DataKeys[index].Values["num_nomina"]);
            string TEMPRANO = gridservicios.DataKeys[index].Values["temprano"].ToString();
            string nombre = gridservicios.DataKeys[index].Values["empleado"].ToString().Trim();
            DateTime fecha = Convert.ToDateTime(gridservicios.DataKeys[index].Values["fecha"]);
            deletedata(IDC_SOLICITUDASI.ToString().Trim());
            if (cbx.Checked)
            {
                adddata(IDC_SOLICITUDASI.ToString().Trim(), idc_empleado.ToString(),fecha,TEMPRANO,nombre,num_nomina.ToString());
            }
        }
        protected void gridservicios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                CheckBox cbx = e.Row.FindControl("cbxselected") as CheckBox;
                cbx.Checked = cbxselecttodos.Checked;
                string IDC_SOLICITUDASI = rowView["IDC_SOLICITUDASI"].ToString();
                cbx.Checked = Exists("idc_solicitudasi = "+IDC_SOLICITUDASI.Trim()+"");
            }
        }

        protected void txtfiltrar_TextChanged(object sender, EventArgs e)
        {

            CargarFaltas(txtfiltrar.Text);
        }

        protected void lbkbuscar_Click(object sender, EventArgs e)
        {

            CargarFaltas(txtfiltrar.Text);
        }

        protected void lnkauto_Click(object sender, EventArgs e)
        {
            cadena();
            if (totalcadena() == 0)
            {
                Alert.ShowAlertInfo("Seleccione minimo una solicitud para Autorizar","Mensaje del Sistema",this);
            }
            else {
                error_modal.Visible = false;
                lblerror.Text = "";
                txtobservaciones.Text = "";
                Session["Caso_Confirmacion"] = "Auto";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Desea Autorizar las Solicitudes Seleccionadas(" + totalcadena().ToString() + ") ','modal fade modal-success');", true);
            }
        }

        protected void lnkcanclar_Click(object sender, EventArgs e)
        {
            cadena();
            if (totalcadena() == 0)
            {
                Alert.ShowAlertInfo("Seleccione minimo una solicitud para Cancelar", "Mensaje del Sistema", this);
            } 
            else
            {
                error_modal.Visible = false;
                lblerror.Text = "";
                txtobservaciones.Text = "";
                Session["Caso_Confirmacion"] = "Cancelar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Desea Cancelar las Solicitudes Seleccionadas(" +totalcadena().ToString()+ ") ','modal fade modal-danger');", true);

            }

        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                string tipo = "";
                string msg = "";
                switch (caso)
                {
                    case "Auto":
                        tipo = "A";
                        msg = "Solicitudes Autorizadas";
                        break;
                    case "Cancelar":
                        tipo = "C";
                        msg = "Solicitudes Canceladas";
                        break;
                }

                if (tipo == "C" && txtobservaciones.Text == "")
                {
                    error_modal.Visible = true;
                    lblerror.Text = "Ingrese Observaciones para Cancelar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMeededssage", "ModalClose();", true);

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMeeee334333ssage", "ModalConfirm('Mensaje del Sistema','Desea Cancelar las Solicitudes Seleccionadas(" +totalcadena().ToString()+ ") ','modal fade modal-danger');", true);

                }
                else {
                    AsistenciaCOM componente = new AsistenciaCOM();
                    DataSet ds = componente.sp_autorizar_asistencias_varios_nuevo(tipo, cadena(), totalcadena(), txtobservaciones.Text.Trim().ToUpper(),
                        Convert.ToInt32(Session["sidc_usuario"]));
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (vmensaje == "")
                    {
                        error_modal.Visible = false;
                        lblerror.Text = "";
                        cbxselecttodos.Checked = false;
                        txtobservaciones.Text = "";
                        CargarFaltas("");

                        ScriptManager.RegisterStartupScript(this, GetType(), "alewswedededsrtMessage",
                         "ModalClose();", true);
                        string url = txtfiltrar.Text==""? "solicitudes_asistencia_pendientes.aspx" : "solicitudes_asistencia_pendientes.aspx?filtro=" + funciones.deTextoa64(txtfiltrar.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                         "AlertGO('"+ msg + " Correctamente','" + url + "');", true);
                    }
                    else
                    {
                        error_modal.Visible = true;
                        lblerror.Text = vmensaje;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMeededssage", "ModalClose();", true);

                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMeeee334333ssage", "ModalConfirm('Mensaje del Sistema','Desea Cancelar las Solicitudes Seleccionadas(" +totalcadena().ToString()+ ") ','modal fade modal-danger');", true);

                    }
                }
                
            }
            catch (Exception ex)
            {
                error_modal.Visible = true;
                lblerror.Text = ex.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMeededssage", "ModalClose();", true);

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMeeee334333ssage", "ModalConfirm('Mensaje del Sistema','Desea Cancelar las Solicitudes Seleccionadas(" +totalcadena().ToString()+ ") ','modal fade modal-danger');", true);

                Global.CreateFileError(ex.ToString(), this);
            }
            
        }

        protected void gridservicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int num_nomina = Convert.ToInt32(gridservicios.DataKeys[index].Values["num_nomina"].ToString());
            int idc_empleado = Convert.ToInt32(gridservicios.DataKeys[index].Values["idc_empleado"].ToString());
            DateTime fecha = Convert.ToDateTime(gridservicios.DataKeys[index].Values["fecha"].ToString());
            string empleado = gridservicios.DataKeys[index].Values["empleado"].ToString();
            try
            {
                txtidc_empleado.Text = idc_empleado.ToString().Trim();
                txtnumeronomina.Text = num_nomina.ToString().Trim();
                txtempleado.Text = empleado;
                grid.Visible = true;
                nohay.Visible = false;
                string estatus = "modal fade modal-success";
                txtstaus.Text = "Asistencia";
                txtstaus.BackColor = System.Drawing.Color.FromName("#1ABC9C");
                txtstaus.ForeColor = System.Drawing.Color.FromName("#fff");
                AsistenciaCOM componente = new AsistenciaCOM();
                string date = fecha.ToString("MM/dd/yyyy");
                
                DataSet ds = componente.sp_status_incidencia_dia_numnomina(num_nomina, Convert.ToDateTime(date));
                DataTable dtstatus = ds.Tables[0];
                string color = dtstatus.Rows[0]["color"].ToString();
                if (color.ToString() == "255")
                {
                    estatus = "modal fade modal-danger";
                    txtstaus.Text = dtstatus.Rows[0]["estatus"].ToString();
                    txtstaus.BackColor = System.Drawing.Color.FromName("#FA2A00");
                    txtstaus.ForeColor = System.Drawing.Color.FromName("#fff");
                }

                txtincid.Visible = false;
                if (dtstatus.Rows.Count > 1)
                {
                    txtincid.Visible = true;
                    txtincid.Text = dtstatus.Rows[1]["estatus"].ToString();
                    txtincid.BackColor = System.Drawing.Color.FromName("#FA2A00");
                    txtincid.ForeColor = System.Drawing.Color.FromName("#fff");
                }
                DataSet ds2 = componente.sp_ver_asistencia_detalle(num_nomina, Convert.ToDateTime(date));
                DataTable dt = ds2.Tables[0];
                grid.DataSource = dt;
                grid.DataBind();
                if (dt.Rows[0]["fecha_rnd"].ToString() == "")
                {
                    grid.Visible = false;
                    nohay.Visible = true;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMe223222eee334333ssage",
                    "ModalConfirms('" + estatus+"');", true);

            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            
            string url = "asistencia_detalle.aspx?top="+funciones.deTextoa64("15")+"&num_nomina=" + funciones.deTextoa64(txtnumeronomina.Text.Trim()) +
                "&idc_empleado=" + funciones.deTextoa64(txtidc_empleado.Text.Trim());
            ScriptManager.RegisterStartupScript(this, GetType(), "KWDOIQNDW9929929H",
                "window.open('" + url + "');", true);
        }
    }
}