using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;

using System.Globalization;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class comisiones_m : Page
    {
        private CultureInfo Cultuture_Info = new CultureInfo("es-MX");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                cargar_combo_agentes_usuario((int)Session["sidc_usuario"]);
                //cargar_combo_agentes_usuario(127);
                Cargar_Boton_Mes();
                LimpiarCampos("0.00");

            }
        }

        private void cargar_combo_agentes_usuario(int idc_usuario)
        {
            try
            {
                comisiones_mCOM comp = new comisiones_mCOM();
                Datos_Usuario_logENT dul = new Datos_Usuario_logENT();
                dul.Pidc_usuario = idc_usuario;
                DataSet ds = comp.agentes_vs_usuarios(dul);
                ddlAgente.DataValueField = "idc_agente";
                ddlAgente.DataTextField = "nombre3"; //nombre
                ddlAgente.DataSource = ds.Tables[0];
                ddlAgente.DataBind();

                Alert.ShowAlertAutoCloseTimer("loading...", "", "1000", false, "imagenes/horizontal-loader.gif", this.Page);

            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void Cargar_Boton_Mes()
        {

            DateTime mes = DateTime.Today;
            lblVer.Text = mes.ToString("MMMM yyyy", Cultuture_Info).ToUpper();
            mes = mes.AddMonths(-1);
            lblMes.Text = mes.ToString("MMMM yyyy", Cultuture_Info).ToUpper();

        }

        private void LimpiarCampos(string valor)
        {

            txtventa.Text = valor;
            txtventa1.Text = valor;
            txtventa2.Text = valor;
            txtmargen.Text = valor;
            txtaportacion.Text = valor;
            txttotalgastos.Text = valor;
            txtdif_apo.Text = valor;
            txtdiferencia.Text = valor;
            txtdiferencia.ForeColor = valor == "0.00" ? Color.Red : Color.Green;
            txtbono1.Text = valor;
            txtbono2.Text = valor;
            txtmargen_r.Text = valor;
            txtapo.Text = valor;
            h_mes.Value = "";
        }

        /*  botones de mes*/
        protected void btnVer_OnClick(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Today;
            Cargar_Grafica(Convert.ToInt32(ddlAgente.SelectedValue.ToString()), fecha, true);
            //bonos
            lblperiodo.Text = fecha.ToString("MMMM yyyy", Cultuture_Info).ToUpper();

            lblcaab1.Text = "Comisión al Dia con Aportación Adicional y Bono 1";
            lblcaab2.Text = "Comisión al Dia con Aportación Adicional y Bonos(1,2)";
            lblcomision.Text = "Comisión al Dia";
            h_mes.Value = "ver"; ////informativo para validar que se a dado click en boton
            //* Alert.ShowAlertAutoCloseTimer("loading...", "", "5000", false, "imagenes/horizontal-loader.gif", this.Page);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "fn_sweetAlert_close();", true);

        }

        protected void btnMes_OnClick(object sender, EventArgs e)
        {

            DateTime fecha = DateTime.Today.AddMonths(-1);
            Cargar_Grafica(Convert.ToInt32(ddlAgente.SelectedValue.ToString()), fecha, false);
            lblperiodo.Text = fecha.ToString("MMMM yyyy", Cultuture_Info).ToUpper();
            lblcaab1.Text = "Comisión Final con Aportación Adicional y Bono 1";
            lblcaab2.Text = "Comisión Final con Aportación Adicional y Bonos(1,2)";
            lblcomision.Text = "Comisión Final";
            h_mes.Value = "mes"; //informativo para validar que se a dado click en boton
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "fn_sweetAlert_close();", true);

        }

        /*--------------*/
        private void Cargar_Grafica(int idc_agente, DateTime fecha, bool actual)
        {
            try
            {
                comisiones_mCOM comp = new comisiones_mCOM();
                comisiones_mENT ent = new comisiones_mENT();
                ent.Pidc_agente = idc_agente;
                ent.Pmes = fecha.Month;
                ent.Panio = fecha.Year;
                ent.Paldia = actual;
                DataSet ds = comp.comisiones_agente(ent);

                Decimal aportacion = 0, aportacion12 = 0, venta = 0, totalgastos = 0, margen = 0, bono1 = 0, bono2 = 0;
                Decimal diferencia = 0, comision = 0, apo = 0;

                bool VQUITADIF = ((DateTime.Today.Month >= 9 & DateTime.Today.Year == 2015) | (DateTime.Today.Year >= 2016)) ? true : false;


                if (ds.Tables[0].Rows.Count > 0)
                {
                    //Convert.ToDecimal()
                    aportacion12 = Convert.ToDecimal(ds.Tables[0].Rows[0]["aportacion12"].ToString());
                    aportacion = Convert.ToDecimal(ds.Tables[0].Rows[0]["aportacion"].ToString());
                    venta = Convert.ToDecimal(ds.Tables[0].Rows[0]["venta"].ToString());
                    totalgastos = Convert.ToDecimal(ds.Tables[0].Rows[0]["totalgastos"].ToString());
                    margen = Convert.ToDecimal(ds.Tables[0].Rows[0]["margen"].ToString());
                    bono1 = Convert.ToDecimal(ds.Tables[0].Rows[0]["bono1"].ToString());
                    bono2 = Convert.ToDecimal(ds.Tables[0].Rows[0]["bono2"].ToString());
                    comision = Convert.ToDecimal(ds.Tables[0].Rows[0]["comision"].ToString());

                    txtventa.Text = venta.ToString("#,0.00");
                    txtventa1.Text = venta.ToString("#,0.00");
                    txtventa2.Text = venta.ToString("#,0.00");

                    txtmargen_r.Text = margen.ToString();
                    txtbono1.Text = bono1.ToString("#,0.00");
                    txtbono2.Text = bono2.ToString("#,0.00");


                    if (margen >= 12)
                    {
                        diferencia = (aportacion12 - totalgastos);
                        margen = (aportacion12 > 0 || venta > 0) ? ((aportacion12 / venta) * 100) : 0;
                        txtaportacion.Text = aportacion12.ToString("#,0.00");
                        //apo = (aportacion12-aportacion);
                        Container_com.Visible = false;

                    }
                    else
                    {
                        diferencia = (aportacion - totalgastos);
                        margen = (aportacion > 0 || venta > 0) ? ((aportacion / venta) * 100) : 0;
                        txtaportacion.Text = aportacion.ToString("#,0.00");
                        Container_com.Visible = true;
                    }

                    apo = (aportacion12 - aportacion);
                    txtapo.Text = apo.ToString("#,0.00");
                    txtmargen.Text = margen.ToString("0.0000");

                    diferencia = (VQUITADIF) ? comision : diferencia;
                    txtdiferencia.Text = diferencia.ToString("#,0.00");

                    txtdif_apo.Text = (apo - diferencia).ToString("#,0.00");
                    txttotalgastos.Text = totalgastos.ToString("#,0.00");
                    txtdiferencia.ForeColor = diferencia < 1 ? Color.Red : Color.Green;
                    /*LLENAR GRID*/
                    grid(ds.Tables[1], margen);
                }
                else
                {
                    LimpiarCampos("0.00");

                }
                txtcom1.Text = txtmargen.Text;
                txtcom2.Text = txtmargen.Text;
                Decimal caab1 = Convert.ToDecimal(txtmargen_r.Text.ToString()) > 12 ? Convert.ToDecimal(txtdiferencia.Text.ToString()) : (Convert.ToDecimal(txtdif_apo.Text.ToString()) + Convert.ToDecimal(txtbono1.Text.ToString())); /*dif_apo_1 + bono1_1*/
                txtcaab1.Text = caab1.ToString("#,0.00");
                txtcaab2.Text = (caab1 + Convert.ToDecimal(txtbono2.Text.ToString())).ToString("#,0.00");

                /* grafica */
                Grafica((margen >= 12 ? aportacion12 : aportacion), totalgastos);
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void Grafica(Decimal Aportacion, Decimal TotalGasto)
        {
            SeriesCollection cserie = Chart1.Series;
            cserie[0].Points.Clear();
            cserie["Series1"].ChartType = SeriesChartType.Column;
            Chart1.Titles[0].Text = ddlAgente.SelectedValue.ToString();

            cserie[0].Points.AddY(Aportacion);
            cserie[0].Points.AddY(TotalGasto);
            cserie[0].Font = new Font("Areal", 0.2F, FontStyle.Regular);
            cserie[0].Points[0].AxisLabel = "Aportacion \r\n $ " + Aportacion.ToString("#,0.00");
            cserie[0].Points[1].AxisLabel = "Anticipo de Sueldos \r\n  y Gastos \r\n $ " + TotalGasto.ToString("#,0.00");
            cserie[0].Font = new Font("Arial", 0.5F, FontStyle.Regular);//Font(,   FontStyle.Regular);
            cserie[0].Points[0].Color = Color.Green;
            cserie[0].Points[1].Color = Color.Red;

            cserie[0].Points[0].Label = (cserie[0].Points[0].YValues[0] == 0 ? "$ 0" : "");
            cserie["Series1"]["PixelPointWidth"] = "60";
            cserie["Ventas"]["PixelPointWidth"] = "60";

            /* label style*/
            cserie["Series1"]["BarLabelStyle"] = "Center";

            /* show 3D*/
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            /* draw as 3D Cykinder*/
            cserie["Series1"]["DrawingStyle"] = "Cylinder";

            h_aportacion.Value = Aportacion.ToString("#,0.00");
        }

        private void grid(DataTable dt_det, decimal vmargen)
        {
            //Dim apo_x_factura As Object
            if (dt_det.Rows.Count > 0)
            {
                dt_det.Columns.Add("comi", typeof(decimal));
                dt_det.Columns.Add("apo", typeof(decimal));
                dt_det.Columns.Add("pedscg", typeof(string));


                DataView dv = new DataView();
                dv = dt_det.DefaultView;
                //dv.ToTable.Rows.Count
                for (int i = 0; i <= dv.Count - 1; i++)
                {
                    dv[i]["comi"] = (vmargen >= 12) ? dv[i]["comifin"] : dv[i]["comiini"];
                    dv[i]["apo"] = (vmargen >= 12) ? dv[i]["apofin"] : dv[i]["apoini"];

                }

                DataTable dt_distinct = new DataTable();


                object venta = dv.Table.Compute("SUM(venta)", "venta>=0");
                object apo = dv.Table.Compute("SUM(apo)", "apo>=0");

                dt_distinct = dv.ToTable(true, "idc_factura", "codfac", "directa");
                dt_distinct.Columns.Add("tipod", typeof(int));
                dt_distinct.Columns.Add("venta", typeof(decimal));
                dt_distinct.Columns.Add("apo", typeof(decimal));
                //dt_distinct.Columns.Add("directa", GetType(Boolean))
                dt_distinct.Columns.Add("pedscg", typeof(string));


                int index = 0;
                dv.Sort = "idc_factura";
                for (int i = 0; i <= dt_distinct.Rows.Count - 1; i++)
                {
                    dt_distinct.Rows[i]["venta"] = dv.Table.Compute("SUM(venta)", "idc_factura=" + dt_distinct.Rows[i]["idc_factura"].ToString());
                    dt_distinct.Rows[i]["apo"] = dv.Table.Compute("SUM(apo)", "idc_factura=" + dt_distinct.Rows[i]["idc_factura"].ToString());
                    index = dv.Find(dt_distinct.Rows[i]["idc_factura"]);

                    if (index >= 0)
                    {
                        dt_distinct.Rows[i]["tipod"] = dv[index]["tipod"];
                    }
                    dt_distinct.Rows[i]["pedscg"] = ((int)dt_distinct.Rows[i]["tipod"] != 1 ? dt_distinct.Rows[i]["idc_factura"] : "");
                }

                dt_det = dv.ToTable(true, "idc_factura", "tipod", "codfac", "venta", "apo", "desart", "cantidad", "comi", "directa");
                dt_distinct.Columns.Add("tot_ven", typeof(decimal), venta.ToString());
                dt_distinct.Columns.Add("tot_apo", typeof(decimal), apo.ToString());
                dv = dt_det.DefaultView;

                //if (!(outputParam is DBNull)) { 
                //    DataTO.Id = Convert.ToInt64(outputParam);
                //}

                decimal v_directa = (dv.Table.Compute("SUM(Venta)", "directa=true").Equals(System.DBNull.Value)) ? 0 : Convert.ToDecimal(dv.Table.Compute("SUM(Venta)", "directa=true"));
                decimal v_compartida = (dv.Table.Compute("SUM(Venta)", "directa=false").Equals(System.DBNull.Value)) ? 0 : Convert.ToDecimal(dv.Table.Compute("SUM(Venta)", "directa=false"));

                decimal a_directa = (dv.Table.Compute("SUM(apo)", "directa=true").Equals(System.DBNull.Value)) ? 0 : Convert.ToDecimal(dv.Table.Compute("SUM(apo)", "directa=true"));
                decimal a_compartida = (dv.Table.Compute("SUM(apo)", "directa=false").Equals(System.DBNull.Value)) ? 0 : Convert.ToDecimal(dv.Table.Compute("SUM(apo)", "directa=false"));

                txtaportacion_d_h.Value = a_directa.ToString("#,0.00");
                txtaportacion_c_h.Value = a_compartida.ToString("#,0.00");

                txtventa_c_h.Value = v_compartida.ToString("#,0.00");
                txtventa_d_h.Value = v_directa.ToString("#,0.00");

                //Se quito FormatNumber
                txtcomequi.Text = ((((a_compartida / Convert.ToDecimal(0.7)) + a_directa) / (v_compartida + v_directa)) * 100).ToString("0.0000");

                txtcomision_c_h.Value = (v_compartida > 0) ? ((a_compartida / v_compartida) * 100).ToString("0.0000") : "0.0000";
                txtcomision_d_h.Value = (v_directa > 0) ? ((a_directa / v_directa) * 100).ToString("0.0000") : "0.0000";

                DataView dv_distinct = new DataView();
                dv_distinct = dt_distinct.DefaultView;
                dv_distinct.Sort = "idc_factura,tipod,codfac";

                dv.Sort = "idc_factura,tipod,codfac";

                Session["dv_com"] = dv.Table;
                //grid1.DataSource = dv_distinct;
                //grid1.DataBind();

                Session["ds"] = dv_distinct;
                //gridv1.DataSource = dv_distinct;
                //gridv1.DataBind();
                //gridv1.HeaderRow.TableSection = System.Web.UI.WebControls.TableRowSection.TableHeader;


            }

        }

        /*  Detalle de Aportaciones  */
        protected void lkbDetalles_OnClick(object sender, EventArgs e)
        {
            //txtaportacion_d.Text = txtaportacion_d_h.Value;
            //txtaportacion_c.Text = txtaportacion_c_h.Value;
            if (h_mes.Value != "")
            {

                string url = string.Format("comisiones_detalles.aspx?p_ventad={0}&p_ventac={1}&p_comisiond={2}&p_comisionc={3}&apo={4}",
                    funciones.deTextoa64(txtventa_d_h.Value),
                    funciones.deTextoa64(txtventa_c_h.Value),
                    funciones.deTextoa64(txtcomision_d_h.Value),
                    funciones.deTextoa64(txtcomision_c_h.Value),
                    funciones.deTextoa64(h_aportacion.Value));

                Response.Redirect(url, false);
                Context.ApplicationInstance.CompleteRequest();


            }
            else
            {
                //Alert.ShowAlert("Elige el mes para ver los detalles.", "Mensaje del sistema", this);
                Alert.ShowAlertInfo("Elige el mes para ver los detalles.", "Mensaje del sistema", this);
            }


        }




        /* -- *************** --*/
        protected void lnk_esp_articulo_OnClick(object sender, EventArgs e)
        {

            h_axion_esp.Value = "Articulos";
            cargar_comisiones_art();
            int total = ddlSemanaComisiones.Items.Count;
            if (total > 0)
            {
                ddlSemanaComisiones.SelectedIndex = total - 1;
                filtrar_tabla_Art(Convert.ToInt32(ddlSemanaComisiones.SelectedValue));
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalComisiones_Esp('Comisiones Especiales Articulos');", true);
        }

        /* -- botones parte 3 --*/
        protected void lnk_esp_activacion_OnClick(object sender, EventArgs e)
        {
            h_axion_esp.Value = "Activacion";
            cargar_comisiones_activaciones();
            int total = ddlSemanaComisiones.Items.Count;
            if (total > 0)
            {
                ddlSemanaComisiones.SelectedIndex = total - 1;
                filtrar_tabla_Art(Convert.ToInt32(ddlSemanaComisiones.SelectedValue));
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalComisiones_Esp('Comisiones Especiales Activaciones');", true);
        }

        protected void cargar_comisiones_art()
        {
            try
            {

                string fechahoy = DateTime.Now.ToString("dd-MM-yyyy");

                Datos_Usuario_logENT dul = new Datos_Usuario_logENT();
                comisiones_mENT ent = new comisiones_mENT();
                comisiones_mCOM comp = new comisiones_mCOM();

                dul.Pidc_usuario = (int)Session["sidc_usuario"];
                ent.Pfechai = fechahoy;
                ent.Pfechaf = fechahoy;
                DataSet ds = comp.comisiones_esp_articulos(dul, ent);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //cargar la tabla 
                    Session["TablaComEspecialActivacion"] = ds.Tables[0];
                    ddlSemanaComisiones.DataSource = ds.Tables[1];
                    ddlSemanaComisiones.DataTextField = "texto";
                    ddlSemanaComisiones.DataValueField = "semana";
                    ddlSemanaComisiones.DataBind();
                    ddlSemanaComisiones.Items.Insert(0, new ListItem("Todos", "0"));
                }
                else {
                    VacioGrid_modal.Visible = true;
                    div_ddl_Semana.Visible = false;
                }
            }
            catch (Exception ex)
            {
                VacioGrid_modal.Visible = true;
                div_ddl_Semana.Visible = false;
                lbl_VacioGrid.Text = "Error al Cargar Información.\\n \\u000b \\nError:\\n" + ex.ToString();
                //Alert.ShowAlertError(, this.Page);
            }
        }

        protected void cargar_comisiones_activaciones()
        {
            try
            {
                string fechahoy = DateTime.Now.ToString("dd-MM-yyyy");
                Datos_Usuario_logENT dul = new Datos_Usuario_logENT();
                comisiones_mENT ent = new comisiones_mENT();
                comisiones_mCOM comp = new comisiones_mCOM();
                dul.Pidc_usuario = (int)Session["sidc_usuario"];
                ent.Pfechai = fechahoy;
                ent.Pfechaf = fechahoy;
                DataSet ds = comp.comisiones_esp_activaciones(dul, ent);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //cargar la tabla 
                    Session["TablaComEspecialActivacion"] = ds.Tables[0];
                    ddlSemanaComisiones.DataSource = ds.Tables[1];
                    ddlSemanaComisiones.DataTextField = "texto";
                    ddlSemanaComisiones.DataValueField = "semana";
                    ddlSemanaComisiones.DataBind();
                    ddlSemanaComisiones.Items.Insert(0, new ListItem("Todos", "0"));
                }
                else {
                    VacioGrid_modal.Visible = true;
                    div_ddl_Semana.Visible = false;
                }
            }
            catch (Exception ex)
            {
                VacioGrid_modal.Visible = true;
                div_ddl_Semana.Visible = false;
                lbl_VacioGrid.Text = "Error al Cargar Información.\\n \\u000b \\nError:\\n" + ex.ToString();
                //Alert.ShowAlertError(, this.Page);
            }
        }

        protected void ddldeptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string semana = ddlSemanaComisiones.SelectedValue;
            filtrar_tabla_Act(Convert.ToInt32(semana));
        }

        private void filtrar_tabla_Art(int semana)
        {

            if (Session["TablaComEspecialArticulo"] != null)
            {
                DataTable dt = (DataTable)Session["TablaComEspecialArticulo"];
                DataView dv = new DataView(dt);
                DataRow[] dr = null;
                // si la semana es mayor a sero vemos solo semana espesifica si es sero se ve todo
                if (semana > 0)
                {
                    dv.RowFilter = "semana=" + semana;
                    dr = dt.Select("semana=" + semana);
                }
                object cantidad = dt.Compute("Sum(cantidad)", "semana=" + semana);
                object aportacion = dt.Compute("Sum(aportacion)", "semana=" + semana);
                grid_comision_esp_activaciones.DataSource = dv;
                grid_comision_esp_activaciones.DataBind();
                txtmonto_m.Text = cantidad.ToString();
                txtaportacion_m.Text = aportacion.ToString();

            }
            else
            {
                txtmonto_m.Text = "0";
                txtaportacion_m.Text = "0";

            }


        }

        protected void filtrar_tabla_Act(int semana)
        {
            if (Session["TablaComEspecialActivacion"] != null)
            {
                DataTable dt = (DataTable)Session["TablaComEspecialActivacion"];

                DataView dv = new DataView(dt);
                DataRow[] dr = null;
                // si la semana es mayor a sero vemos solo semana espesifica si es sero se ve todo
                if (semana > 0)
                {
                    dv.RowFilter = "semana=" + semana;
                    dr = dt.Select("semana=" + semana);

                }

                object monto = dt.Compute("Sum(monto)", "semana=" + semana);
                object aportacion = dt.Compute("Sum(aportacion)", "semana=" + semana);
                grid_comision_esp_activaciones.DataSource = dv;
                grid_comision_esp_activaciones.DataBind();
                txtmonto_m.Text = monto.ToString();
                txtmonto_m.Text = aportacion.ToString();
            }
            else
            {
                txtmonto_m.Text = "0";
                txtaportacion_m.Text = "0";
            }

        }

        protected void ddlSemanaComisiones_SelectedIndexChanged(object sender, EventArgs e)
        {

            int semana = Convert.ToInt32(ddlSemanaComisiones.SelectedValue);
            switch (h_axion_esp.Value)
            {
                case "Activacion":
                    filtrar_tabla_Act(semana);
                    break;

                case "Articulos":
                    filtrar_tabla_Art(semana);
                    break;

            }

        }


    }
}