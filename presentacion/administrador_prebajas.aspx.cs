using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class administrador_prebajas : System.Web.UI.Page
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
                AdministradorPrebajasENT entidad = new AdministradorPrebajasENT();
                AdministradorPrebajasCOM componente = new AdministradorPrebajasCOM();
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

        public void DataPrebajas()
        {
            try
            {
                AdministradorPrebajasENT entidad = new AdministradorPrebajasENT();
                AdministradorPrebajasCOM componente = new AdministradorPrebajasCOM();
                entidad.Pidc_prebaja = 0;
                entidad.Pidc_puesto_prebaja = 0;
                entidad.Pconsutla = Request.QueryString["pconsulta"] != null ? true : false;
                DataSet ds = componente.CargaPreBajas(entidad);
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

        public void DataPrebajasDetalles(int idc_prebaja, int idc_puestoprebaja)
        {
            try
            {
                AdministradorPrebajasENT entidad = new AdministradorPrebajasENT();
                AdministradorPrebajasCOM componente = new AdministradorPrebajasCOM();
                entidad.Pidc_prebaja = idc_prebaja;
                entidad.Pidc_puesto_prebaja = idc_puestoprebaja;
                entidad.Pconsutla = Request.QueryString["pconsulta"] != null ? true : false;
                DataSet ds = componente.CargaPreBajas(entidad);
                Session["Tabla_TotalRevisiones"] = ds.Tables[1];
                gridprebajasdetalles.DataSource = ds.Tables[1];
                gridprebajasdetalles.DataBind();
                lblfecha_proceso.Text = ds.Tables[2].Rows[0]["fecha_proceso"].ToString();
                //sacamos porcentajes de avances
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

                //generamos los datos del empleado
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (Convert.ToInt32(row["idc_puesto"]) == idc_puestoprebaja)
                    {
                        lblusuario.Text = row["usuario"].ToString();
                        lblNombre.Text = row["empleado"].ToString();
                        lblPuesto.Text = row["descripcion"].ToString();
                        lblnomina.Text = row["num_nomina"].ToString();
                        lblmotivo.Text = row["motivo"].ToString();
                        lblfecha.Text = row["fecha"].ToString();
                        if (row["renuncia"] is DBNull)
                        {
                        }
                        else
                        {
                            lbltipo_baja.Text = Convert.ToBoolean(row["renuncia"]) == true ? "Renuncia" : "Despido";
                        }
                        GenerarRuta(Convert.ToInt32(row["idc_empleado"].ToString()), "fot_emp");
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Genera ruta de archivos
        /// </summary>
        public void GenerarRuta(int id_comprobar, string codigo_imagen)
        {
            var Entidad = new UsuariosE();
            Entidad.Cod_arch = codigo_imagen;
            var Componente = new OrganigramaBL();
            var ds = new DataSet();
            ds = Componente.CargaPath(Entidad);
            if (ds.Tables[0].Rows.Count != 0)
            {
                var tablaGrupos = new DataTable();
                //Paso dataset ala tabla
                tablaGrupos = ds.Tables[0];
                var row = tablaGrupos.Rows[0];
                var carpeta = row["rw_carpeta"].ToString();
                var domn = Request.Url.Host;
                switch (codigo_imagen)
                {
                    case "fot_emp"://fotos de empleados
                        if (domn == "localhost")
                        {
                            var url = "imagenes/btn/default_employed.png";
                            imgEmpleado.ImageUrl = url;
                        }
                        else
                        {
                            var url = "http://" + domn + carpeta + id_comprobar + ".jpg";
                            ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                            imgEmpleado.ImageUrl = url;
                        }
                        break;
                }
            }
        }

        protected void repeatpendientes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            //Panel Panel = (Panel)e.Item.FindControl("panel_revision");
            string idc_puesto = Convert.ToString(DataBinder.Eval(dbr, "idc_puesto"));
            string idc_prebaja = Convert.ToString(DataBinder.Eval(dbr, "idc_prebaja"));
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
            //DataTable tabla = (DataTable)Session["Tabla_TotalRevisionesFaltante"];
            //if (tabla.Rows.Count==0)
            //{
            //    Alert.ShowAlertInfo("Para terminar la Pre-Baja el PERSONAL ASIGNADO debe generar la Baja definitiva en el sistema.","La Pre-Baja esta completa.",this);
            //}
        }

        protected void gridprebajasdetalles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER MEDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_puestorevi = Convert.ToInt32(gridprebajasdetalles.DataKeys[index].Values["idc_puesto"].ToString());
            int idc_puestoprebaja = Convert.ToInt32(gridprebajasdetalles.DataKeys[index].Values["idc_puestoprebaja"].ToString());
            string tipo_rev = gridprebajasdetalles.DataKeys[index].Values["tipo_rev"].ToString();
            DataTable tabla = (DataTable)Session["Tabla_TotalRevisiones"];
            switch (e.CommandName)
            {
                case "Ver":
                    Session["Caso_Confirmacion"] = tipo_rev;
                    Session["idc_puestorevi"] = idc_puestorevi;
                    Session["idc_puestoprebaja"] = idc_puestoprebaja;
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
                string tipo_rev = rowView["tipo_rev"].ToString();
                string color = rowView["color"].ToString();
                e.Row.Cells[5].ForeColor = color == "#F4FA58" ? Color.FromName("#000000") : Color.FromName("#FFFFFF");
                e.Row.Cells[5].BackColor = Color.FromName(color);
            }
        } 

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            string idc_puestoprebaja = Session["idc_puestoprebaja"].ToString();
            string idc_puestorevi = Session["idc_puestorevi"].ToString();
            switch (Confirma_a)
            {
                case "HERRAMIENTAS Y ACTIVOS":
                    Session["Previus"] = "administrador_prebajas.aspx";
                    Response.Redirect("herramientas_revision.aspx?preview=1&idc_puestorev=" + idc_puestorevi + "&idc_puestoprebaja=" + idc_puestoprebaja);
                    break;

                case "CELULARES":
                    Session["Previus"] = "administrador_prebajas.aspx";
                    Response.Redirect("celulares_revision.aspx?preview=1&idc_puestorev=" + idc_puestorevi + "&idc_puestoprebaja=" + idc_puestoprebaja);
                    break;

                case "VEHICULOS":
                    Session["Previus"] = "administrador_prebajas.aspx";
                    Response.Redirect("vehiculos_revision.aspx?preview=1&idc_puestorev=" + idc_puestorevi + "&idc_puestoprebaja=" + idc_puestoprebaja);
                    break;

                case "REVISIÓN":
                    Session["Previus"] = "administrador_prebajas.aspx";
                    Response.Redirect("servicios_revisiones.aspx?preview=1&idc_puestorev=" + idc_puestorevi + "&idc_puestoprebaja=" + idc_puestoprebaja);
                    break;

                case "REVISION COMPLETA":
                    Session["Previus"] = "administrador_prebajas.aspx";
                    Response.Redirect("revision_final.aspx?preview=1&idc_puestorev=" + funciones.deTextoa64(idc_puestorevi) + "&idc_puestoprebaja=" + funciones.deTextoa64(idc_puestoprebaja));
                    break;
            }
        }

        protected void lnklista_Click(object sender, EventArgs e)
        {
            string url = Request.QueryString["pconsulta"] != null ? "administrador_prebajas.aspx?pconsulta=1" : "administrador_prebajas.aspx";
            Response.Redirect(url);
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