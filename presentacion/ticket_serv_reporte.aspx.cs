using ClosedXML.Excel;
using iTextSharp.text;
using System;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using negocio.Componentes;
using negocio.Entidades;
using System.Drawing;
using System.Globalization;

namespace presentacion
{
    public partial class ticket_serv_reporte : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                txtfechainicio.Text = DateTime.Now.ToString("yyyy-MM-01");
                txtfechafin.Text = DateTime.Now.ToString("yyyy-MM-dd");
                CargaDeptos();
                CargaPuestos("", 0);
                //Cargar_Grid(0);
                lnkExportarExcel.Enabled = false;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), h_strScript.Value, true);
        }

        public void CargaPuestos(string filtro, int idc_depto)
        {
            try
            {
                ticket_servENT ent = new ticket_servENT();
                ticket_servCOM com = new ticket_servCOM();
                filtro = filtro.Trim();
                ent.Pfiltro = filtro == "" ? "%" : filtro;
                ent.Pidc_depto_aten = idc_depto;
                DataSet ds = com.CargaComboPueatos(ent);
                ddlPuestoAsigna.DataValueField = "idc_puesto";
                ddlPuestoAsigna.DataTextField = "descripcion_puesto_completa";
                ddlPuestoAsigna.DataSource = ds.Tables[0];
                ddlPuestoAsigna.DataBind();


                if (filtro != "")
                {
                    if (ddlPuestoAsigna.Items.Count > 0)
                    {
                        ddlPuestoAsigna.SelectedIndex = 0;
                        ddlPuestoAsigna.Focus();
                    }
                    ddldeptos.SelectedIndex = 0;
                }
                else
                {
                    ddlPuestoAsigna.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un Puesto", "0")); //updated code}
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void CargaDeptos()
        {
            try
            {
                ticket_servENT ent = new ticket_servENT();
                ticket_servCOM com = new ticket_servCOM();
                DataSet ds = com.CargaComboDeptos(ent);
                ddldeptos.DataValueField = "idc_depto";
                ddldeptos.DataTextField = "nombre";
                ddldeptos.DataSource = ds.Tables[0];
                ddldeptos.DataBind();
                ddldeptos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos los Departamentos", "0"));  //updated code}
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void ddlPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            div_grafica.Visible = false;
            gridReporte.Visible = false;
            lnkExportarExcel.Enabled = false;
            ddlPuestoAsigna.Focus();
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            CargaPuestos(txtpuesto_filtro.Text, 0);
            

        }

        protected void ddldeptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaPuestos("", Convert.ToInt32(ddldeptos.SelectedValue));
            txtpuesto_filtro.Text = "";
            ddlPuestoAsigna.Focus();
        }

        protected void imgbtn_Click(object sender, EventArgs e)
        {

            string str_modal;
            str_modal = string.Format("ModalConfirm('Mensaje del Sistema','Elige un tipo de Filtro, en relacion del puesto y  tareas.','{0}');", "modal fade modal-info");
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", str_modal, true);
        }

        public void Cargar_Grid(int id)
        {
            ticket_servCOM com = new ticket_servCOM();
            ticket_servENT ent = new ticket_servENT();

            ent.PfechaInicio = Convert.ToDateTime(txtfechainicio.Text);
            ent.Pfechafin = Convert.ToDateTime(txtfechafin.Text);

            ent.Pidc_depto_aten = ddldeptos.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddldeptos.SelectedValue);
            ent.Pidc_puesto = ddlPuestoAsigna.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddlPuestoAsigna.SelectedValue);

            DataSet ds = com.ticket_serv_reporte(ent);

            gridReporte.DataSource = ds.Tables[0];

            gridReporte.DataBind();

            NO_Hay.Visible = (ds.Tables[0].Rows.Count == 0);// ? true : false;
            div_grafica.Visible = (ds.Tables[0].Rows.Count != 0);
            gridReporte.Visible = true;
            lnkExportarExcel.Enabled = true;

            ViewState["EMPLEADOS" + txtfechainicio.Text + txtfechafin.Text] = ds.Tables[0];
            //Session["ds_ticket_serv_rep"] = ds.Tables[0];
            cargarGrafica(ds.Tables[1], ds.Tables[2]);

        }

        private void cargarGrafica(DataTable td_GRAF, DataTable td_TABLA_GRAF)
        {
            string ATEN_CAN = td_GRAF.Rows[0]["ATEN_CAN"].ToString();
            string CAN = td_GRAF.Rows[0]["CAN"].ToString();
            string ESP = td_GRAF.Rows[0]["ESP"].ToString();
            string Pend_D = td_GRAF.Rows[0]["Pend_D"].ToString();
            string Pend_F = td_GRAF.Rows[0]["Pend_F"].ToString();

            string NOMBRE = td_GRAF.Rows[0]["nombre"].ToString();
            string B_result = td_GRAF.Rows[0]["B_result"].ToString();
            string M_result = td_GRAF.Rows[0]["M_result"].ToString();
            string TOTAL = td_GRAF.Rows[0]["TOTAL"].ToString();
            string Efectividad = td_GRAF.Rows[0]["Efectividad"].ToString();

            H_strDpto.Value = (td_GRAF.Rows[0]["DEPTO"].ToString().Trim()).Trim();
            H_strPuesto.Value = (td_GRAF.Rows[0]["NOMBRE"].ToString().Trim()).Trim();

            gridResultados.DataSource = td_TABLA_GRAF;
            gridResultados.DataBind();
            CultureInfo cf = new CultureInfo("es-MX");
            string strScript = string.Format("Grafica('Resultados del {0} al {1}', {2},{3});",
                Convert.ToDateTime(txtfechainicio.Text).ToString("dddd, d - MMMM - yyyy", cf),
                Convert.ToDateTime(txtfechafin.Text).ToString("dddd, d - MMMM - yyyy", cf),
                Efectividad, M_result);
            h_strScript.Value = strScript;
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), strScript, true);
        }

        private void tem_ds(DataTable dt)
        {

            if (dt.Columns.Contains("des_corta"))
            {
                dt.Columns.Remove("des_corta");
            }
            if (dt.Columns.Contains("status_color"))
            {
                dt.Columns.Remove("status_color");
            }
            if (dt.Columns.Contains("OBSERVACIONES_TICKET_CORTA"))
            {
                dt.Columns.Remove("OBSERVACIONES_TICKET_CORTA");
            }
            if (dt.Columns.Contains("MOTIVO_CAN_CORTA"))
            {
                dt.Columns.Remove("MOTIVO_CAN_CORTA");
            }
            if (dt.Columns.Contains("MOTIVO_ATEN_CAN_CORTA"))
            {
                dt.Columns.Remove("MOTIVO_ATEN_CAN_CORTA");
            }
            if (dt.Columns.Contains("OBSERVACIONES_TERM_CORTA"))
            {
                dt.Columns.Remove("OBSERVACIONES_TERM_CORTA");
            }
            if (dt.Columns.Contains("STATUS_TICKET"))
            {
                dt.Columns.Remove("STATUS_TICKET");
            }
            if (dt.Columns.Contains("PENDIENTE_ATEN"))
            {
                dt.Columns.Remove("PENDIENTE_ATEN");
            }
            if (dt.Columns.Contains("PENDIENTE"))
            {
                dt.Columns.Remove("PENDIENTE");
            }

        }

        private void limpiar()
        {
            lblDescr.Text = "";
            lblObser.Text = "";
            lblEmple_rep.Text = "";
            lblDepto_rep.Text = "";
            lblFecha_rep.Text = "";

            lblEmple_aten.Text = "";
            lblDepto_aten.Text = "";
            lblFecha_aten.Text = "";
            //
            lblEmple_aten.Text = "";
            lblDepto_aten.Text = "";
            lblFecha_aten.Text = "";
            //
            lblEmple_term.Text = "";
            lblDepto_term.Text = "";
            lblFecha_term.Text = "";
            lblComet_term.Text = "";

            div_aten.Visible = false;
            div_can.Visible = false;
            div_term.Visible = false;
            div_aten_can.Visible = false;
            div_progresbar.Visible = false;
        }

        protected void gridReporte_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            limpiar();
            int index = Convert.ToInt32(e.CommandArgument);

            lblDescr.Text = gridReporte.DataKeys[index].Values["descripcion"].ToString();
            lblObser.Text = gridReporte.DataKeys[index].Values["OBSERVACIONES_TICKET"].ToString();
            lblEmple_rep.Text = gridReporte.DataKeys[index].Values["EMPLEADO_REP"].ToString();
            lblDepto_rep.Text = gridReporte.DataKeys[index].Values["DEPTO_REP"].ToString();
            lblFecha_rep.Text = gridReporte.DataKeys[index].Values["FECHA_CREADO"].ToString();

            string msgStatus = "";
            string clase = "";
            string tipo = gridReporte.DataKeys[index].Values["STATUS"].ToString();

            if (gridReporte.DataKeys[index].Values["FECHA_ATEN_CAN"].ToString().Trim() != "")
            {
                tipo = "ATEN_CAN";
            }


            switch (tipo)
            {
                case "A":
                    lblEmple_aten.Text = gridReporte.DataKeys[index].Values["EMPLEADO_ATEN"].ToString();//EMPLEADO_REP
                    lblDepto_aten.Text = gridReporte.DataKeys[index].Values["DEPTO_ATEN"].ToString(); //DEPTO_REP
                    lblFecha_aten.Text = gridReporte.DataKeys[index].Values["FECHA_ATEN"].ToString();//FECHA_ATEN
                    div_aten.Visible = true;
                    div_progresbar.Visible = true;
                    msgStatus = "El Ticket esta siendo Atendido.";
                    clase = "panel-warning";
                    break;
                case "C":
                    lblEmple_can.Text = gridReporte.DataKeys[index].Values["EMPLEADO_CAN"].ToString();//EMPLEADO_CAN
                    lblDepto_can.Text = gridReporte.DataKeys[index].Values["DEPTO_CAN"].ToString(); //DEPTO_CAN
                    lblFecha_can.Text = gridReporte.DataKeys[index].Values["FECHA_CAN"].ToString();//FECHA_CAN
                    lblMotivoCan.Text = gridReporte.DataKeys[index].Values["MOTIVO_CAN"].ToString();//FECHA_CAN
                    div_can.Visible = true;
                    msgStatus = "El Ticket fue Cancelado.";
                    clase = "panel-danger";
                    break;
                case "E":
                    msgStatus = "El Ticket esta en Espera.";
                    clase = "panel-info";
                    break;
                case "T":
                    lblEmple_term.Text = gridReporte.DataKeys[index].Values["EMPLEADO_ATEN"].ToString();//EMPLEADO_term
                    lblDepto_term.Text = gridReporte.DataKeys[index].Values["DEPTO_ATEN"].ToString(); //DEPTO_term
                    lblFecha_term.Text = gridReporte.DataKeys[index].Values["FECHA_TERM"].ToString();//FECHA_term
                    lblComet_term.Text = gridReporte.DataKeys[index].Values["OBSERVACIONES_TERM"].ToString();//observ_term
                    div_term.Visible = true;
                    div_progresbar.Visible = true;
                    msgStatus = "El Ticket a sido Terminado.";
                    clase = "panel-success";
                    break;
                case "ATEN_CAN":
                    lblEmple_Aten_Can.Text = gridReporte.DataKeys[index].Values["EMPLEADO_ATEN_CAN"].ToString();//EMPLEADO_ATEN_CAN
                    lblDepto_Aten_Can.Text = gridReporte.DataKeys[index].Values["DEPTO_ATEN_CAN"].ToString(); //DEPTO_ATEN_CAN
                    lblFecha_Aten_Can.Text = gridReporte.DataKeys[index].Values["FECHA_ATEN_CAN"].ToString();//FECHA_ATEN_CAN
                    lblMotivoAten_Can.Text = gridReporte.DataKeys[index].Values["MOTIVO_ATEN_CAN"].ToString();//FECHA_ATEN_CAN
                    div_aten_can.Visible = true;
                    msgStatus = "El Ticket se Atendio y Cancelo.";
                    clase = "panel-danger";
                    break;
            }




            string str_Alert;
            str_Alert = string.Format("ModalMostrar('Mensaje del Sistema','Detalles del Ticket {0}','{1}',{2},{3},'{4}');",
            gridReporte.DataKeys[index].Values["descripcion"].ToString(),
                msgStatus, gridReporte.DataKeys[index].Values["TIEMPO_ESTIMADO"].ToString(),
                gridReporte.DataKeys[index].Values["TIEMPO_RESPUESTA"].ToString(), clase);

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", str_Alert, true);

        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            /*fechas no valida*/
            if (!validar_fechas()) { return; }
            Cargar_Grid(0);
        }

        private bool validar_fechas()
        {
            if (Convert.ToDateTime(txtfechainicio.Text) > Convert.ToDateTime(txtfechafin.Text))
            {
                Alert.ShowAlertInfo("La fecha inicio debe ser menor o igual a fecha fin.", "Mensaje del Sistema", this);
                return false;
            }
            if (txtfechainicio.Text == "" || txtfechafin.Text == "")
            {
                Alert.ShowAlertInfo("Debe seleccionar una fecha de inicio y una fecha fin", "Mensaje del Sistema", this);
                return false;
            }
            return true;
        }

        protected void lnkexport_Click(object sender, EventArgs e)
        {
            if (NO_Hay.Visible)
            {
                Alert.ShowAlertInfo("No hay datos, debe seleccionar un rango de fechas valida.", "Mensaje del Sistema", this);
                return;
            }
            Export Export = new Export();
            List<DataTable> ListaTables = new List<DataTable>();
            //DataTable dt = (DataTable)Session["ds_ticket_serv_rep"];
            DataTable dt = (DataTable)ViewState["EMPLEADOS" + txtfechainicio.Text + txtfechafin.Text];
            tem_ds(dt);
            ListaTables.Add(dt);
            //array de nombre de sheets
            string[] Nombres = new string[] { "TicketServ" };
            if (dt.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("Esta Peticion no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
            }
            else
            {
                CultureInfo cf = new CultureInfo("es-MX");
                string titulo = string.Format("Ticket de Servicio de {1} de {0} de las fechas {2} al {3}",
                    H_strDpto.Value == "" ? "Todos los departamentos" : H_strDpto.Value,
                    H_strPuesto.Value == "" ? "Todos los Puestos" : H_strPuesto.Value,
                    Convert.ToDateTime(txtfechainicio.Text).ToString("dddd, d - MMMM - yyyy", cf),
                Convert.ToDateTime(txtfechafin.Text).ToString("dddd, d - MMMM - yyyy", cf));
                string nomArchivo = string.Format("TicketServ_de_{0}_de_{1}_{2}_{3}_{4}.xlsx",
                    H_strDpto.Value == "" ? "Todos los departamentos" : H_strDpto.Value,
                    H_strPuesto.Value == "" ? "Todos los Puestos" : H_strPuesto.Value,
                    Convert.ToDateTime(txtfechainicio.Text).ToString("dd-MM-yy", cf),
                    Convert.ToDateTime(txtfechafin.Text).ToString("dd-MM-yy", cf),
                    DateTime.Now.ToString("yyMMddHHmmss", cf));
                nomArchivo = nomArchivo.Replace(" ", "_");
                string mensaje = Export.toExcel(titulo, XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")), XLColor.White,
                                   XLColor.Black, 10, ListaTables, XLColor.Gray, XLColor.White, Nombres, 1,
                                   nomArchivo, Page.Response);
                if (mensaje != "")
                {
                    //
                    Alert.ShowAlertError(mensaje, this);
                }
            }

        }

        protected void lnkir_Click(object sender, EventArgs e)
        {
            if (!validar_fechas()) { return; }

            Response.Redirect(string.Format("ticket_serv_rgrafica.aspx?Pfechai={0}&pfechaf={1}",
                funciones.deTextoa64(txtfechainicio.Text),
                funciones.deTextoa64(txtfechafin.Text)
                ));
        }

        protected void gridResultados_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string f_hex = rowView["F_color"].ToString();
                string b_hex = rowView["B_color"].ToString();
                if (b_hex != "" && f_hex != "")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml(b_hex);
                    e.Row.ForeColor = ColorTranslator.FromHtml(f_hex);
                }

            }
        }


    }
}