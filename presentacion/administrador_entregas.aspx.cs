using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class administrador_entregas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!Page.IsPostBack)
            {
                DataPrebajas();
            }
            lblfechaactual.Text = DateTime.Now.ToString();
            DateTime Time = DateTime.Now;
            int AÑO = Time.Year;
            GenerarDatosGraficas(AÑO);
            lblaño.Text = AÑO.ToString();
        }

        private void GenerarDatosGraficas(int año)
        {
            try
            {
                Administrador_PreparacionesENT entidad = new Administrador_PreparacionesENT();
                Administrador_PreparacionCOM componente = new Administrador_PreparacionCOM();
                entidad.Pyear = año;
                DataSet ds = componente.CargaGrafica(entidad);
                grid_proc.DataSource = ds.Tables[0];
                grid_proc.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void DataPrebajas()
        {
            try
            {
                Administrador_EntregasENT entidad = new Administrador_EntregasENT();
                Administrador_EntregasCOM componente = new Administrador_EntregasCOM();
                entidad.Pidc_entrega = 0;
                entidad.Pidc_puesto = 0;
                DataSet ds = componente.CargaPendientes(entidad);
                repeatprebajas.DataSource = ds.Tables[0];
                repeatprebajas.DataBind();
                if (ds.Tables[0].Rows.Count == 0) { Noempleados.Visible = true; }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void lnkVerContraseña_Click(object sender, EventArgs e)
        {
            DateTime Time = DateTime.Now;
            int AÑO = Time.Year;
            int año = txtaño.Text == "" ? AÑO : Convert.ToInt32(txtaño.Text);
            GenerarDatosGraficas(año);
            lblaño.Text = año.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertSMessage", "ModalGraph();", true);
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

        public void DataPrebajasDetalles(int idc_entrega, int idc_puestoprebaja)
        {
            try
            {
                Administrador_EntregasENT entidad = new Administrador_EntregasENT();
                Administrador_EntregasCOM componente = new Administrador_EntregasCOM();
                entidad.Pidc_entrega = idc_entrega;
                entidad.Pidc_puesto = idc_puestoprebaja;
                DataSet ds = componente.CargaPendientes(entidad);
                Session["Tabla_TotalRevisiones"] = ds.Tables[1];
                DataView dv_gpo_lib = ds.Tables[1].DefaultView;
                //dv_gpo_lib.RowFilter = "idc_perfilgpo = " + DropGpos.SelectedValue;
                int faltante = 0;
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    if (row["fecha_revision"].ToString() == "") { faltante = faltante + 1; }
                }
                decimal revisadostotal = ds.Tables[1].Rows.Count;
                decimal revisadosfaltantes = faltante;
                if (revisadosfaltantes != 0 || revisadostotal != 0)
                {
                    decimal porcentaje_avanze = ((revisadostotal - revisadosfaltantes) / revisadostotal) * 100;
                    decimal incorrect = 100 - porcentaje_avanze;
                    string avanze = porcentaje_avanze.ToString("#.##") + "%";
                    string incorr = incorrect.ToString("#.##") + "%";
                    if (porcentaje_avanze == 0)
                    {
                        avanze = "0%";
                    }
                    if (incorrect == 0)
                    {
                        incorr = "0%";
                    }
                    //ejecutamos script para llenar progress
                    ScriptManager.RegisterStartupScript(this, GetType(), "progressbar", "ProgressBar('" + avanze + "','" + incorr + "');", true);
                    lblporcentajepregress.Text = avanze;
                    Session["Avanze"] = porcentaje_avanze;
                }
                else
                {
                    decimal porcentaje_avanze = 100;
                    decimal incorrect = 100 - porcentaje_avanze;
                    string avanze = porcentaje_avanze.ToString("#.##") + "%";
                    string incorr = incorrect.ToString("#.##") + "%";
                    if (porcentaje_avanze == 0)
                    {
                        avanze = "0%";
                    }
                    if (incorrect == 0)
                    {
                        incorr = "0%";
                    }
                    //ejecutamos script para llenar progress
                    ScriptManager.RegisterStartupScript(this, GetType(), "progressbar", "ProgressBar('" + avanze + "','" + incorr + "');", true);
                    lblporcentajepregress.Text = avanze;
                    Session["Avanze"] = porcentaje_avanze;
                }
                gridprebajasdetalles.DataSource = ds.Tables[1];
                gridprebajasdetalles.DataBind();
                //generamos los datos del empleado
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (Convert.ToInt32(row["idc_puesto"]) == idc_puestoprebaja)
                    {
                        lblPuesto.Text = row["descripcion"].ToString();
                        lblEmpleado.Text = row["empleado"].ToString();
                    }
                }

                lblfecha_proceso.Text = ds.Tables[2].Rows[0]["fecha_proceso"].ToString();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void lnkGO_Click(object sender, EventArgs e)
        {
            LinkButton lnkGO = (LinkButton)sender;
            PanelPrebajas.Visible = false;
            PanelDetalles.Visible = true;
            DataPrebajasDetalles(Convert.ToInt32(lnkGO.CommandName.ToString()), Convert.ToInt32(lnkGO.CommandArgument.ToString()));
            lnklista.Visible = true;
        }

        protected void lnklista_Click(object sender, EventArgs e)
        {
            Response.Redirect("administrador_entregas.aspx");
        }

        protected void gridprebajasdetalles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER MEDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_puesto = Convert.ToInt32(gridprebajasdetalles.DataKeys[index].Values["idc_puesto"].ToString());
            int idc_entrega = Convert.ToInt32(gridprebajasdetalles.DataKeys[index].Values["idc_entrega"].ToString());
            string tipo_prep = gridprebajasdetalles.DataKeys[index].Values["tipo_prep"].ToString();
            DataTable tabla = (DataTable)Session["Tabla_TotalRevisiones"];
            switch (e.CommandName)
            {
                case "Ver":
                    Session["Caso_Confirmacion"] = tipo_prep;
                    Session["idc_puesto"] = idc_puesto;
                    Session["idc_entrega"] = idc_entrega;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Ver el listado COMPLETO de pendiente por revisar?');", true);
                    break;
            }

            gridprebajasdetalles.DataSource = tabla;
            gridprebajasdetalles.DataBind();
        }

        protected void gridprebajasdetalles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string empleado = rowView["empleado"].ToString();
                string tipo_rev = rowView["tipo_prep"].ToString();
                string color = rowView["color"].ToString();
                e.Row.Cells[5].ForeColor = color == "#F4FA58" ? Color.FromName("#000000") : Color.FromName("#FFFFFF");
                e.Row.Cells[5].BackColor = Color.FromName(color);
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            string idc_entrega = Session["idc_entrega"].ToString();
            string idc_puesto = Session["idc_puesto"].ToString();
            Session["Previus"] = "administrador_entregas.aspx";
            switch (Confirma_a)
            {
                case "HERRAMIENTAS Y ACTIVOS":
                    Response.Redirect("herramientas_entrega.aspx?preview=1&idc_entrega=" + idc_entrega + "&idc_puestoent=" + idc_puesto);
                    break;

                case "CELULARES":
                    Response.Redirect("celulares_entrega.aspx?preview=1&idc_entrega=" + idc_entrega + "&idc_puestoent=" + idc_puesto);
                    break;

                case "VEHICULOS":
                    Response.Redirect("vehiculos_entrega.aspx?preview=1&idc_entrega=" + idc_entrega + "&idc_puestoent=" + idc_puesto);
                    break;

                case "REVISIÓN":
                    Response.Redirect("revisiones_entrega.aspx?preview=1&idc_entrega=" + idc_entrega + "&idc_puestoent=" + idc_puesto);
                    break;
            }
        }

        protected void repeatprebajas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            //Panel Panel = (Panel)e.Item.FindControl("panel_revision");
            string idc_puesto = Convert.ToString(DataBinder.Eval(dbr, "idc_puesto"));
            string idc_prebaja = Convert.ToString(DataBinder.Eval(dbr, "idc_entrega"));
            LinkButton lnkGOdET = (LinkButton)e.Item.FindControl("lnkGOdET");
            LinkButton lnkGO = (LinkButton)e.Item.FindControl("lnkGO");
            //asiganmos datos
            lnkGO.CommandArgument = idc_puesto;
            lnkGO.CommandName = idc_prebaja;
            lnkGOdET.CommandArgument = idc_puesto;
            lnkGOdET.CommandName = idc_prebaja;
        }
    }
}