using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
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
                Session["temp_table_id_reclu"] = null;
                DataPrep();
            }
            lnkcambiarfecha.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"].ToString()), 348);
            H1.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"].ToString()), 348);
            LNKREP.Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"].ToString()), 376);
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
            gridreclu.DataSource = ds.Tables[0];
            gridreclu.DataBind();
            Session["temp_table_id_reclu"] = ds.Tables[0];
            lblto.Text = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count == 0) { Noempleados.Visible = true; }
            gridreclu.Columns[1].Visible = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 403);
        }

        protected void lnkGO_Click(object sender, EventArgs e)
        {
            LinkButton lnkGO = (LinkButton)sender;
            //PanelDetalles.Visible = true;
            // DataPrebajasDetalles(Convert.ToInt32(lnkGO.CommandName.ToString()), Convert.ToInt32(lnkGO.CommandArgument.ToString()));
            // lnklista.Visible = true;
            Response.Redirect("candidatos_preparar_captura.aspx?idc_puesto=" + funciones.deTextoa64(lnkGO.CommandArgument.ToString()) + "&idc_prepara=" + funciones.deTextoa64(lnkGO.CommandName.ToString()));
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

        protected void lnkexcel_Click(object sender, EventArgs e)
        {
            Export Export = new Export();
            //array de DataTables
            List<DataTable> ListaTables = new List<DataTable>();
            DataTable dt = (DataTable)Session["temp_table_id_reclu"];
            dt.Columns.Remove("idc_puesto");
            dt.Columns.Remove("idc_prepara");
            dt.Columns.Remove("num_total");
            dt.Columns.Remove("css_class");
            dt.Columns.Remove("fecha_compromiso_reclu");
            dt.Columns["descripcion"].ColumnName = "Puesto";
            dt.Columns["fecha_registro"].ColumnName = "Fecha de Solicitud";
            dt.Columns["fecha_compromiso_reclutamiento"].ColumnName = "Fecha de Compromiso";
            ListaTables.Add(dt);
            string mensaje = "";
            string[] Nombres = new string[] { "Detalles de Reclutamiento" };
            mensaje = Export.toExcel("Reporte de Reclutamiento", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")), XLColor.White,
                              XLColor.Black, 10, ListaTables, XLColor.Gray, XLColor.White, Nombres, 1,
                              "Listado_de_Reclutamiento.xlsx", Page.Response);

            if (mensaje != "")
            {
                Alert.ShowAlertError(mensaje, this);
            }
        }

        protected void gridreclu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string idc_puesto = gridreclu.DataKeys[index].Values["idc_puesto"].ToString();
            string idc_prepara = gridreclu.DataKeys[index].Values["idc_prepara"].ToString();
            string reclutador = gridreclu.DataKeys[index].Values["reclutador"].ToString();
            div_addobsr.Visible = false;
            div_viewobsr.Visible = false;
            Button1.Visible = false;
            switch (e.CommandName)
            {
                case "preview":
                    Response.Redirect("candidatos_preparar_captura.aspx?rec=" + funciones.deTextoa64(reclutador) + "&idc_puesto=" + funciones.deTextoa64(idc_puesto) + "&idc_prepara=" + funciones.deTextoa64(idc_prepara));

                    break;
                case "obsr_add":
                    div_addobsr.Visible = true;
                    Button1.Visible = true;
                    txtidc_prepara.Text = idc_prepara.Trim();
                    ScriptManager.RegisterStartupScript(this, GetType(),Guid.NewGuid().ToString(), "myModalObserv('modal fade modal-info');", true);
                    break;
                case "view_add":
                    div_viewobsr.Visible = true;
                    txtidc_prepara.Text = idc_prepara.Trim();
                    CandidatosCOM componente = new CandidatosCOM();
                    DataSet ds = componente.sp_puestosprepara_observaciones(Convert.ToInt32(idc_prepara));
                    grid_observaciones.DataSource = ds.Tables[0];
                    grid_observaciones.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "myModalObserv('modal fade modal-info');", true);
                    break;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            if (txtf1.Text == "" || txtf2.Text == "")
            {
                Alert.ShowAlertError("Ingrese las Fechas", this);
            }
            else
            {
                string f1 = txtf1.Text;
                string f2 = txtf2.Text;
                String url = HttpContext.Current.Request.Url.AbsoluteUri;
                String path_actual = url.Substring(url.LastIndexOf("/") + 1);
                url = url.Replace(path_actual, "");
                url = url + "tareas_informacion_adicional.aspx?idc_proceso=" + funciones.deTextoa64("0") + "&idc_tipoi=" + funciones.deTextoa64("5") + "&f1=" + funciones.deTextoa64(f1) + "&f2=" + funciones.deTextoa64(f2);
                ScriptManager.RegisterStartupScript(this, GetType(), "noti533W3", "window.open('" + url + "');", true);
            }
        }

        protected void Yes2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtobservaciones.Text == "")
                {
                    Alert.ShowAlertError("Ingrese Observaciones", this);
                }
                else
                {
                    CandidatosCOM componente = new CandidatosCOM();
                    DataSet ds = componente.sp_mpuestos_preparar_obs(Convert.ToInt32(txtidc_prepara.Text), txtobservaciones.Text, Convert.ToInt32(Session["sidc_usuario"]));
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (vmensaje == "")
                    {
                        txtidc_prepara.Text = "";
                        txtobservaciones.Text = "";
                        Alert.ShowAlert( "Observaciones Agregadas correctamente", "Mensaje del Sistema", this);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Ingrese las Fecha para filtrar el reporte','modal fade modal-info');", true);
        }
    }
}