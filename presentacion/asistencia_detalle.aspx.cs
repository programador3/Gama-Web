using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class asistencia_detalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["num_nomina"] == null || Request.QueryString["idc_empleado"] == null)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "KWDOIQNDW9929929H",
                        "window.close();", true);
                    return;
                }
                else {
                    int idc_empleado = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleado"]));
                    int top = Request.QueryString["top"]!= null? Convert.ToInt32(funciones.de64aTexto(Request.QueryString["top"])):0;
                    CargarGridPrincipal(idc_empleado);
                    CargarFaltas(top);
                }
                
            }
        }
        public void CargarGridPrincipal(int idc_empleado)
        {
            try
            {
                PuestosServiciosENT entidad = new PuestosServiciosENT();
                PuestosServiciosCOM componente = new PuestosServiciosCOM();
                entidad.Pidc_pre_empleado = idc_empleado;
                entidad.PReves = false;
                DataSet ds = componente.CargaPuestos(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblPuesto.Text = ds.Tables[0].Rows[0]["descripcion"].ToString();
                    lblEmpleado.Text = ds.Tables[0].Rows[0]["empleado"].ToString();
                    lbldepto.Text = ds.Tables[0].Rows[0]["depto"].ToString();
                    Session["idc_empleado"] = Convert.ToInt32(ds.Tables[0].Rows[0]["idc_empleado"]);
                    string rutaimagen = funciones.GenerarRuta("fot_emp", "rw_carpeta");
                    var domn = Request.Url.Host;
                    if (domn == "localhost" || Convert.ToInt32(ds.Tables[0].Rows[0]["idc_empleado"]) == 0)
                    {
                        var url = "imagenes/btn/default_employed.png";
                        imgEmpleado.ImageUrl = url;
                    }
                    else
                    {
                        var url = "http://" + domn + rutaimagen + ds.Tables[0].Rows[0]["idc_empleado"].ToString() + ".jpg";
                        imgEmpleado.ImageUrl = url;
                    }

                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }
        public void CargarFaltas(int top)
        {
            try
            {
                AsistenciaCOM componente = new AsistenciaCOM();
                int num_nomina= Convert.ToInt32(funciones.de64aTexto(Request.QueryString["num_nomina"]));
                DataSet ds = componente.sp_ver_asistencia(num_nomina);
                DataTable dt = ds.Tables[0];
                DataView view = dt.DefaultView;
                view.RowFilter = "";
                if (view.ToTable().Rows.Count > 0)
                {
                    if (top > 0)
                    {
                        DataTable df = funciones.TopDataRow(dt,top);
                        gridservicios.Visible = true;
                        gridservicios.DataSource = df;
                        gridservicios.DataBind();
                    }
                    else {
                        gridservicios.Visible = true;
                        gridservicios.DataSource = dt;
                        gridservicios.DataBind();
                    }
                }
                else {

                    gridservicios.Visible = false;
                    Alert.ShowAlertInfo("N0 hay Datos de Asistencia","Mensaje del Sistema", this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void gridservicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int num_nomina = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["num_nomina"]));
            DateTime fecha = Convert.ToDateTime(gridservicios.DataKeys[index].Values["fecha"].ToString());
            try
            {
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
                    "ModalConfirms('" + estatus + "');", true);

            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
    }
}