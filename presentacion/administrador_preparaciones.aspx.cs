using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class administrador_puestos_preparacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!Page.IsPostBack)
            {
                DataPrep();
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

        /// <summary>
        /// Carga los puestos pendientes de preparar
        /// </summary>
        public void DataPrep()
        {
            try
            {
                Administrador_PreparacionesENT entidad = new Administrador_PreparacionesENT();
                Administrador_PreparacionCOM componente = new Administrador_PreparacionCOM();
                entidad.Pidc_prepara = 0;
                entidad.Pidc_puesto = 0;
                DataSet ds = componente.CargaPendientes(entidad);
                repeatpendientes.DataSource = ds.Tables[0];
                repeatpendientes.DataBind();
                if (ds.Tables[0].Rows.Count == 0) { Noempleados.Visible = true; }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga los detalle del puesto pendiente
        /// </summary>
        /// <param name="idc_prebaja"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void DataPrebajasDetalles(int idc_prepara, int idc_puesto)
        {
            try
            {
                Administrador_PreparacionesENT entidad = new Administrador_PreparacionesENT();
                Administrador_PreparacionCOM componente = new Administrador_PreparacionCOM();
                entidad.Pidc_prepara = idc_prepara;
                entidad.Pidc_puesto = idc_puesto;
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
                    if (Convert.ToInt32(row["idc_puesto"]) == idc_puesto)
                    {
                        lblPuesto.Text = row["descripcion"].ToString();
                        lblfecha.Text = row["fecha_registro"].ToString();
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

        protected void repeatprebajas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            //Panel Panel = (Panel)e.Item.FindControl("panel_revision");
            string idc_puesto = Convert.ToString(DataBinder.Eval(dbr, "idc_puesto"));
            string idc_prebaja = Convert.ToString(DataBinder.Eval(dbr, "idc_prepara"));
            LinkButton lnkGOdET = (LinkButton)e.Item.FindControl("lnkGOdET");
            LinkButton lnkGO = (LinkButton)e.Item.FindControl("lnkGO");
            //asiganmos datos
            lnkGO.CommandArgument = idc_puesto;
            lnkGO.CommandName = idc_prebaja;
            lnkGOdET.CommandArgument = idc_puesto;
            lnkGOdET.CommandName = idc_prebaja;
        }

        protected void lnkGO_Click(object sender, EventArgs e)
        {
            LinkButton lnkGO = (LinkButton)sender;
            PanelPrebajas.Visible = false;
            PanelDetalles.Visible = true;
            DataPrebajasDetalles(Convert.ToInt32(lnkGO.CommandName.ToString()), Convert.ToInt32(lnkGO.CommandArgument.ToString()));
            lnklista.Visible = true;
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

        protected void gridprebajasdetalles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER MEDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_puestorevi = Convert.ToInt32(gridprebajasdetalles.DataKeys[index].Values["idc_puesto"].ToString());
            int idc_puestoprebaja = Convert.ToInt32(gridprebajasdetalles.DataKeys[index].Values["idc_puestobaja"].ToString());
            int idc_prepara = Convert.ToInt32(gridprebajasdetalles.DataKeys[index].Values["idc_prepara"].ToString());
            string tipo_rev = gridprebajasdetalles.DataKeys[index].Values["tipo_prep"].ToString();
            DataTable tabla = (DataTable)Session["Tabla_TotalRevisiones"];
            switch (e.CommandName)
            {
                case "Ver":
                    Session["Caso_Confirmacion"] = tipo_rev;
                    Session["idc_puesto"] = idc_puestorevi;
                    Session["idc_puestobaja"] = idc_puestoprebaja;
                    Session["idc_prepara"] = idc_prepara;
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

        protected void lnklista_Click(object sender, EventArgs e)
        {
            Response.Redirect("administrador_preparaciones.aspx");
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            string idc_puestobaja = Session["idc_puestobaja"].ToString();
            string idc_puesto = Session["idc_puesto"].ToString();
            string idc_prepara = Session["idc_prepara"].ToString();
            Session["Previus"] = HttpContext.Current.Request.Url.LocalPath.ToString();
            switch (Confirma_a)
            {
                case "HERRAMIENTAS Y ACTIVOS":
                    Response.Redirect("herramientas_preparacion.aspx?preview=1&idc_puesto=" + idc_puesto + "&idc_puestobaja=" + idc_puestobaja);
                    break;

                case "CELULARES":
                    Response.Redirect("celulares_preparacion.aspx?preview=1&idc_puesto=" + idc_puesto + "&idc_puestobaja=" + idc_puestobaja);
                    break;

                case "VEHICULOS":
                    Response.Redirect("vehiculos_preparacion.aspx?preview=1&idc_puesto=" + idc_puesto + "&idc_puestobaja=" + idc_puestobaja);
                    break;

                case "REVISIÓN":
                    Response.Redirect("revisiones_preparacion.aspx?preview=1&idc_puesto=" + idc_puesto + "&idc_puestobaja=" + idc_puestobaja);
                    break;

                case "CURSOS":
                    Response.Redirect("cursos_preparacion.aspx?preview=1&idc_puesto=" + idc_puesto + "&idc_puestobaja=" + idc_puestobaja);
                    break;

                case "RECLUTAMIENTO":
                    Response.Redirect("candidatos_preparar_captura.aspx?view=true&idc_puesto=" + funciones.deTextoa64(idc_puestobaja) + "&idc_prepara=" + funciones.deTextoa64(idc_prepara) + "&idc_puesto_reclutador=" + funciones.deTextoa64(idc_puesto));
                    break;
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
    }
}