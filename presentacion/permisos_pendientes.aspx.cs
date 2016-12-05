using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class permisos_pendientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
            int idc_opcion = 1869;  //pertenece al modulo de grupos backend
            if (funciones.permiso(idc_usuario, idc_opcion) == false)
            {
                Response.Redirect("menu.aspx");
                return;
            }
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_horario_perm");
                ViewState["dt_pendi_auto"] = dt;
                Carga("");
                if (Request.QueryString["filtro"] != null)
                {
                    txtfiltrar.Text = funciones.de64aTexto(Request.QueryString["filtro"]);
                    Carga(txtfiltrar.Text);
                }
            }
        }
        string cadena()
        {
            string ret = "";
            DataTable dt = ViewState["dt_pendi_auto"] as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                string idc_horario_perm = row["idc_horario_perm"].ToString();
                ret = ret + idc_horario_perm + ";" ;
            }
            return ret;
        }
        int totalcadena()
        {
            DataTable dt = ViewState["dt_pendi_auto"] as DataTable;
            return dt.Rows.Count;
        }
        void adddata(string idc_horario_perm)
        {
            DataTable dt = ViewState["dt_pendi_auto"] as DataTable;
            DataRow row = dt.NewRow();
            row["idc_horario_perm"] = idc_horario_perm;
            dt.Rows.Add(row);
            ViewState["dt_pendi_auto"] = dt;
        }

        void deletedata(string idc_horario_perm)
        {

            DataTable dt = ViewState["dt_pendi_auto"] as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                string idc_sol = row["idc_horario_perm"].ToString();
                if (idc_horario_perm.Trim() == idc_sol.Trim())
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
        private void Carga(string filtro)
        {
            Solicitud_HorarioENT entidad = new Solicitud_HorarioENT();
            SolicitudHorarioCOM componente = new SolicitudHorarioCOM();
            DataView view = componente.SolcitudDetalles(entidad).Tables[0].DefaultView;
            view.RowFilter = "status= 'P'";
            DataTable dt = view.ToTable();
            if (filtro == "")
            {
                gridcelulares.DataSource = view.ToTable();
                gridcelulares.DataBind();

                lbltotal.Text = view.ToTable().Rows.Count.ToString();
            } else {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "empleado like '%"+filtro+ "%' OR fecha like '%" + filtro + "%' OR empleado_solicito like '%" + filtro + "%' OR observaciones like '%" + filtro + "%'";
                gridcelulares.DataSource = dv.ToTable();
                gridcelulares.DataBind();
                lbltotal.Text = dv.ToTable().Rows.Count.ToString();
            }
        }

        protected void gridcelulares_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string idc_horario_perm = gridcelulares.DataKeys[index].Values["idc_horario_perm"].ToString();
            string idc_puesto = gridcelulares.DataKeys[index].Values["idc_puesto"].ToString();
            Response.Redirect("solicitud_horario.aspx?autoriza=sajwsbciscoiwcpijspiwsIJIvHVUCUHvhvhvuHVUVUhvUHVUVUHVuhvUCAJBIWQPIJWBCPICVHVCIWHVC873V8C8CVP83VCPIWVCUVCPIV&idc_puesto=" + funciones.deTextoa64(idc_puesto) + "&idc_horario_perm=" + funciones.deTextoa64(idc_horario_perm));
        }

        protected void cbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbx = sender as CheckBox;
            GridView grid = (GridView)((CheckBox)sender).Parent.Parent.Parent.Parent;
            GridViewRow currentRow = (GridViewRow)((CheckBox)sender).Parent.Parent;
            int index = Convert.ToInt32(currentRow.RowIndex);
            int idc_horario_perm = Convert.ToInt32(gridcelulares.DataKeys[index].Values["idc_horario_perm"]);
            deletedata(idc_horario_perm.ToString().Trim());
            if (cbx.Checked)
            {
                adddata(idc_horario_perm.ToString().Trim());
            }
        }

        protected void gridcelulares_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                CheckBox cbx = e.Row.FindControl("cbxselected") as CheckBox;
                cbx.Checked = cbxselecttodos.Checked;
                string idc_horario_perm = rowView["idc_horario_perm"].ToString();
                cbx.Checked = Exists("idc_horario_perm = " + idc_horario_perm.Trim() + "");
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
                    dt2.Columns.Add("idc_horario_perm");
                    ViewState["dt_pendi_auto"] = dt2;
                    //Solicitud_HorarioENT entidad = new Solicitud_HorarioENT();
                    //SolicitudHorarioCOM componente = new SolicitudHorarioCOM();
                    //int idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    //int idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
                    //DataView view = componente.SolcitudDetalles(entidad).Tables[0].DefaultView;
                    //view.RowFilter = "status= 'P'";
                    //DataTable dt = view.ToTable();
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    int idc_horario_perm = Convert.ToInt32(row["idc_horario_perm"]);
                    //    adddata(idc_horario_perm.ToString().Trim());
                    //}
                    foreach (GridViewRow row in gridcelulares.Rows)
                    {
                        string idc_horario_perm = gridcelulares.DataKeys[row.RowIndex].Values["idc_horario_perm"].ToString();
                        adddata(idc_horario_perm.ToString().Trim());
                    }
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
                dt2.Columns.Add("idc_horario_perm");
                ViewState["dt_pendi_auto"] = dt2;
            }
            Carga(txtfiltrar.Text);
        }

        protected void txtfiltrar_TextChanged(object sender, EventArgs e)
        {
            Carga(txtfiltrar.Text);
        }
        protected void lnkauto_Click(object sender, EventArgs e)
        {
            cadena();
            if (totalcadena() == 0)
            {
                Alert.ShowAlertInfo("Seleccione minimo una solicitud para Autorizar", "Mensaje del Sistema", this);
            }
            else
            {
                error_modal.Visible = false;
                lblerror.Text = "";
                txtobservaciones.Text = "";
                Session["Caso_Confirmacion"] = "A";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Desea Autorizar las Solicitudes Seleccionadas(" + totalcadena().ToString() + ") ','modal fade modal-success');", true);
            }
        }

        protected void lnkcanclar_Click(object sender, EventArgs e)
        {
            cadena();
            if (totalcadena() == 0)
            {
                Alert.ShowAlertInfo("Seleccione minimo una solicitud para Rechazar", "Mensaje del Sistema", this);
            }
            else
            {
                error_modal.Visible = false;
                lblerror.Text = "";
                txtobservaciones.Text = "";
                Session["Caso_Confirmacion"] = "R";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Desea Cancelar las Solicitudes Seleccionadas(" + totalcadena().ToString() + ") ','modal fade modal-danger');", true);

            }

        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                DataSet ds = new DataSet();
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                string observaciones = txtobservaciones.Text;
                SolicitudHorarioCOM componente = new SolicitudHorarioCOM();
                if (caso == "R" && observaciones == "")
                { 
                    error_modal.Visible = true;
                    lblerror.Text = "ESCRIBA OBSERVACIONES PARA RECHAZAR";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMeededssage", "ModalClose();", true);

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMeeee334333ssage", "ModalConfirm('Mensaje del Sistema','Desea Cancelar las Solicitudes Seleccionadas(" + totalcadena().ToString() + ") ','modal fade modal-danger');", true);


                }
                else {
                    ds = componente.sp_apermisos_horarios_multi(cadena(),totalcadena(),caso,observaciones,idc_usuario);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (vmensaje == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alewswedededsrtMessage",
                        "ModalClose();", true);
                        string url = txtfiltrar.Text == "" ? "permisos_pendientes.aspx" : "permisos_pendientes.aspx?filtro=" + funciones.deTextoa64(txtfiltrar.Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                         "AlertGO('Proceso Realizado Correctamente','" + url + "');", true);
                    }
                    else
                    {
                        error_modal.Visible = true;
                        lblerror.Text = vmensaje;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMeededssage", "ModalClose();", true);

                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMeeee334333ssage", "ModalConfirm('Mensaje del Sistema','Desea Cancelar las Solicitudes Seleccionadas(" + totalcadena().ToString() + ") ','modal fade modal-danger');", true);

                    }
                }
              

            }
            catch (Exception ex)
            {
                error_modal.Visible = true;
                lblerror.Text = ex.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMeededssage", "ModalClose();", true);

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMeeee334333ssage", "ModalConfirm('Mensaje del Sistema','Desea Cancelar las Solicitudes Seleccionadas(" + totalcadena().ToString() + ") ','modal fade modal-danger');", true);

                Global.CreateFileError(ex.ToString(), this);
            }

        }

    }
}