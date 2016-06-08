using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class herramientas_catalogo : System.Web.UI.Page
    {
        public string rutaimagen = "";//para controles dinamicos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            int idc_puesto = 0;
            int idc_empleado = 0;
            if (Request.QueryString["idc_puesto"] != null)//Si variable request es nulla
            {
                idc_puesto = Convert.ToInt32(Request.QueryString["idc_puesto"].ToString());
            }
            if (Request.QueryString["idc_empleado"] != null)//Si variable request es nulla
            {
                idc_empleado = Convert.ToInt32(Request.QueryString["idc_empleado"].ToString());
            }
            CargaGridPrincipal(idc_puesto);
            CargarDatosEmpleado(idc_puesto);
            CargarGridVehiculos(idc_puesto);
            CargarCelularesInformacion(idc_puesto);
            GenerarRuta(idc_empleado, "fot_emp");
        }

        /// <summary>
        /// Carga el grid principal desde un origen de datos
        /// </summary>
        /// <param name="idc_puesto"></param>
        public void CargaGridPrincipal(int idc_puesto)
        {
            try
            {
                herramientasENT entidad = new herramientasENT();
                entidad.Idc_puesto = idc_puesto;
                herramientasCOM componente = new herramientasCOM();
                DataSet ds = componente.CargaHerramientasCatalogo(entidad);
                Session["Tabla_HerramientasDetalle"] = ds.Tables[1];
                Session["Tabla_HerramientasGlobal"] = ds.Tables[0];
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    PanelSinHerramientas.Visible = true;
                    PanelConHerramientas.Visible = false;
                }
                gridHerramientas.DataSource = ds.Tables[0];
                gridHerramientas.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga el grid de vehiculos
        /// </summary>
        /// <param name="idc_puesto"></param>
        public void CargarGridVehiculos(int idc_puesto)
        {
            try
            {
                herramientasENT entidad = new herramientasENT();
                entidad.Idc_puesto = idc_puesto;
                herramientasCOM componente = new herramientasCOM();
                DataSet ds = componente.CargaVehiculos(entidad);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    PanelConVehiculo.Visible = false;
                    PanelSInVehiculo.Visible = true;
                }
                Session["Tabla_Vehiculos"] = ds.Tables[0];
                Session["Tabla_Vehiculos_Herramientas"] = ds.Tables[1];
                RepeatVehiculos.DataSource = ds.Tables[0];
                RepeatVehiculos.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga informacion de celulares
        /// </summary>
        /// <param name="idc_puesto"></param>
        public void CargarCelularesInformacion(int idc_puesto)
        {
            try
            {
                herramientasENT entidad = new herramientasENT();
                entidad.Idc_puesto = idc_puesto;
                herramientasCOM componente = new herramientasCOM();
                DataSet ds = componente.CargaDatosCelulares(entidad);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    PanelsinCel.Visible = true;
                    PanelConCel.Visible = false;
                }
                Session["Tabla_Celulares"] = ds.Tables[0];
                Session["Tabla_AccesoriosCelular"] = ds.Tables[1];
                repeatCelulares.DataSource = ds.Tables[0];
                repeatCelulares.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga el grid de gerramientas por vehiculo
        /// </summary>
        /// <param name="idc_puesto"></param>
        public void CargarGridVehiculosHerramientas(int idc_vehiculo)
        {
            try
            {
                herramientasENT entidad = new herramientasENT();
                entidad.Idc_vehiculo = idc_vehiculo;
                herramientasCOM componente = new herramientasCOM();
                DataSet ds = componente.CargaHerramientasVehculo(entidad);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    PanelconHVehiculo.Visible = false;
                    PanelsinHVehiculo.Visible = true;
                }
                Session["Tabla_HerramientasVehiculo"] = ds.Tables[0];
                gridHerramientasVehiculo.DataSource = ds.Tables[0];
                gridHerramientasVehiculo.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga DATOS DEL EMPLEADO
        /// </summary>
        public void CargarDatosEmpleado(int idc_puesto)
        {
            try
            {
                PuestosENT entidad = new PuestosENT();
                entidad.Idc_Puesto = idc_puesto;//indicamosm que queremos datos de empleado
                PuestosCOM componente = new PuestosCOM();
                DataSet ds = componente.CargaCatologoPuestos(entidad);
                DataRow row = ds.Tables[1].Rows[0];
                lblPuesto.Text = row["puesto"].ToString();
                lblNombre.Text = row["nombre"].ToString();
                lblDepto.Text = row["depto"].ToString();
                lblucursal.Text = row["sucursal"].ToString();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Genera ruta de imagen
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
                            ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                        }
                        else
                        {
                            var url = "http://" + domn + carpeta + id_comprobar + ".jpg";
                            ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                        }
                        break;

                    case "VEHIC"://fotos de vehiculos
                        if (domn == "localhost")//si es localhost generamos imagen default
                        {
                            var url = "imagenes/btn/car_default.png";
                            rutaimagen = url;
                        }
                        else
                        {//si pagina esta en srvidor
                            var url = "http://" + domn + carpeta + id_comprobar + ".jpg";
                            rutaimagen = url;
                        }
                        break;

                    case "MODCEL"://celulares
                        if (domn == "localhost")//si es localhost
                        {
                            if (id_comprobar == 0)//si no tiene celular asignado
                            {
                                var url = "imagenes/btn/ntphone.png";
                                rutaimagen = url;
                            }
                            else
                            {
                                var url = "imagenes/btn/ntphone.png";
                                rutaimagen = url;
                            }
                        }
                        else
                        {//servidor
                            if (id_comprobar == 0)//si no tiene celular asginado
                            {
                                var url = "imagenes/btn/ntphone.png";
                                rutaimagen = url;
                            }
                            else
                            {
                                var url = "http://" + domn + carpeta + id_comprobar + ".jpg";
                                rutaimagen = url;
                            }
                        }
                        break;
                }
            }
        }

        protected void gridHerramientas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER MEDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            int id_categoria = Convert.ToInt32(gridHerramientas.DataKeys[index].Values["idc_actscategoria"].ToString());
            int id_folio = Convert.ToInt32(gridHerramientas.DataKeys[index].Values["folio"].ToString());
            string subcat = gridHerramientas.DataKeys[index].Values["subcat"].ToString();
            switch (e.CommandName)
            {
                case "Eliminar":
                    Session["Action_Confirm"] = "Eliminar_herramienta";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta seguro de Eliminar este Activo?');", true);
                    break;

                case "Ver Activo"://Ver Modal con detalles
                    lblMSubcat.Text = subcat;
                    lblMDetalles.Text = id_folio.ToString();
                    CargarGridDetalles(id_categoria);
                    ScriptManager.RegisterStartupScript(this, GetType(), "viewgerr", "ModalPreview();", true);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Carga Modal con grid detalles filtrado por idc_actscategoria
        /// </summary>
        public void CargarGridDetalles(int idc_actscategoria)
        {
            DataTable tabla_detalles = (DataTable)Session["Tabla_HerramientasDetalle"];
            DataTable tabla_temp = new DataTable();
            tabla_temp.Columns.Add("idc_actscategoria");
            tabla_temp.Columns.Add("descripcion");
            tabla_temp.Columns.Add("observaciones");
            tabla_temp.Columns.Add("valor");
            //sacamos los datos que sean del mismo id
            foreach (DataRow row_details in tabla_detalles.Rows)
            {
                if (Convert.ToInt32(row_details["idc_actscategoria"]) == idc_actscategoria)
                {
                    DataRow new_row = tabla_temp.NewRow();
                    new_row["idc_actscategoria"] = row_details["idc_actscategoria"].ToString();
                    new_row["descripcion"] = row_details["descripcion"].ToString() + ":";
                    new_row["observaciones"] = row_details["observaciones"].ToString();
                    new_row["valor"] = row_details["valor"].ToString();
                    tabla_temp.Rows.Add(new_row);
                }
            }
            //cargamos repeat
            gridHerramientasDetalles.DataSource = tabla_temp;
            gridHerramientasDetalles.DataBind();
        }

        protected void gridHerramientas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            System.Web.UI.WebControls.Image area_comun = (System.Web.UI.WebControls.Image)e.Row.FindControl("area_comun");
            //FILTRO LOS DATOS DE TIPO CONTROL
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                if (Convert.ToBoolean(rowView["area_comun"]) == true)//si es 1 si es area comun, MUESTRO ETIQUETA CORRESPONDIENTE
                {
                    area_comun.ImageUrl = "imagenes/btn/checked.png";
                }
            }
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        protected void btnCelular_Click(object sender, EventArgs e)
        {
            lnkActivo.CssClass = "btn btn-link";
            lnkCelular.CssClass = "btn btn-primary active";
            lnkAuto.CssClass = "btn btn-link";
            PanelActivos.Visible = false;
            PanelVehiculos.Visible = false;
            PanelCelulares.Visible = true;
            PanelHerramientasVehiculo.Visible = false;
            lblSelectedHerramientas.Text = "Celulares <i class='fa fa-mobile'></i>";
        }

        protected void btnVehiculo_Click(object sender, EventArgs e)
        {
            lnkActivo.CssClass = "btn btn-link";
            lnkCelular.CssClass = "btn btn-link";
            lnkAuto.CssClass = "btn btn-primary active";
            PanelActivos.Visible = false;
            PanelVehiculos.Visible = true;
            PanelCelulares.Visible = false;
            lblSelectedHerramientas.Text = "Vehiculos <i class='fa fa-car'></i>";
        }

        protected void btnActivo_Click(object sender, EventArgs e)
        {
            lnkActivo.CssClass = "btn btn-primary active";
            lnkCelular.CssClass = "btn btn-link ";
            lnkAuto.CssClass = "btn btn-link ";
            PanelActivos.Visible = true;
            PanelVehiculos.Visible = false;
            PanelCelulares.Visible = false;
            PanelHerramientasVehiculo.Visible = false;
            lblSelectedHerramientas.Text = "Herramientas <i class='fa fa-cubes'></i>";
        }

        protected void RepeatVehiculos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //buscamos los controles dentro de repeat para asignar valores
            LinkButton lnkVerHVehiculos = (LinkButton)e.Item.FindControl("lnkVerHVehiculos");
            Image img = (Image)e.Item.FindControl("imgVehiculos");
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string idc_vehiculo = Convert.ToString(DataBinder.Eval(dbr, "idc_vehiculo"));
            //idc_behiculo => boton editar
            lnkVerHVehiculos.CommandName = idc_vehiculo;
            //generamos ruta de imagen
            GenerarRuta(Convert.ToInt32(idc_vehiculo), "VEHIC");
            img.ImageUrl = rutaimagen;
            rutaimagen = "";//limpiamos variable global
        }

        protected void lnkVerHVehiculos_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            //BAJO ID DEL ELEMENTO
            string idc_vehiculo = btn.CommandName.ToString();
            CargarGridVehiculosHerramientas(Convert.ToInt32(idc_vehiculo));
            PanelHerramientasVehiculo.Visible = true;
        }

        protected void gridHerramientasVehiculo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Eliminar":
                    Session["Action_Confirm"] = "Eliminar_herramienta_vehiculo";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta seguro de Eliminar este Activo?');", true);
                    break;

                default:
                    break;
            }
        }

        protected void repeatCelulares_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //buscamos los controles dentro de repeat para signar valores
            LinkButton lnkEditarCel = (LinkButton)e.Item.FindControl("lnkEditarCel");
            Image img = (Image)e.Item.FindControl("imgCel");
            Panel panelsin = (Panel)e.Item.FindControl("PanelsinAccesorios");
            Panel panelcon = (Panel)e.Item.FindControl("PanelconAccesorios");
            Repeater repeatAccesorios = (Repeater)e.Item.FindControl("repeatAccesorios");
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string idc_modcel = Convert.ToString(DataBinder.Eval(dbr, "idc_modelocel"));
            string idc_lineacel = Convert.ToString(DataBinder.Eval(dbr, "idc_lineacel"));
            //generamos ruta de iamgen
            GenerarRuta(Convert.ToInt32(idc_modcel), "MODCEL");
            img.ImageUrl = rutaimagen;
            rutaimagen = "";//limpiamos variable global
                            //llenamos grid de accesorios para cada presentacion

            if (Convert.ToInt32(idc_modcel) == 0)//no tine accesorios
            {
                panelcon.Visible = false;
                panelsin.Visible = true;
            }
            else
            {//tiene accesorios
                panelcon.Visible = true;
                panelsin.Visible = false;
                DataTable tabla_accesorios = (DataTable)Session["Tabla_AccesoriosCelular"];
                DataTable tabla_grid_accesorios = new DataTable();
                tabla_grid_accesorios.Columns.Add("idc_celular");
                tabla_grid_accesorios.Columns.Add("idc_puesto");
                tabla_grid_accesorios.Columns.Add("descripcion");
                tabla_grid_accesorios.Columns.Add("costo");
                //buscamos los accesorios con el id del celular
                foreach (DataRow row in tabla_accesorios.Rows)
                {
                    if (Convert.ToInt32(row["idc_lineacel"]) == Convert.ToInt32(idc_lineacel))
                    {
                        DataRow row_new = tabla_grid_accesorios.NewRow();
                        row_new["idc_celular"] = row["idc_celular"];
                        row_new["idc_puesto"] = row["idc_puesto"];
                        row_new["descripcion"] = row["descripcion"];
                        row_new["costo"] = row["costo"];
                        tabla_grid_accesorios.Rows.Add(row_new);
                    }
                }
                repeatAccesorios.DataSource = tabla_grid_accesorios;
                repeatAccesorios.DataBind();
            }
        }

        protected void lnkHeraamientasExport_Click(object sender, EventArgs e)
        {
            DataTable tableherramientas = (DataTable)Session["Tabla_HerramientasGlobal"];
            tableherramientas.Columns.Remove("idc_activo");
            tableherramientas.Columns.Remove("area_comun");
            tableherramientas.Columns.Remove("idc_actscategoria");
            tableherramientas.Columns.Remove("idc_areaact");
            tableherramientas.Columns.Remove("numero");
            tableherramientas.Columns["cat"].ColumnName = "Categoria";
            tableherramientas.Columns["subcat"].ColumnName = "Activo";
            Export Export = new Export();
            //array de DataTables
            List<DataTable> ListaTables = new List<DataTable>();
            ListaTables.Add(tableherramientas);
            //array de nombre de sheets
            string[] Nombres = new string[] { "Herramientas" };
            if (tableherramientas.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("Este Puesto no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
            }
            else
            {
                string mensaje = Export.toExcel("Herramientas de " + lblNombre.Text, XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                               XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 1,
                               "herramientas_" + lblPuesto.Text + ".xlsx", Page.Response);
                if (mensaje != "")
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }

        protected void lnkGuardarTodo_Click(object sender, EventArgs e)
        {
            DataTable tableherramientas = (DataTable)Session["Tabla_HerramientasGlobal"];
            tableherramientas.Columns.Remove("idc_activo");
            tableherramientas.Columns.Remove("area_comun");
            tableherramientas.Columns.Remove("idc_areaact");
            tableherramientas.Columns.Remove("idc_actscategoria");
            tableherramientas.Columns.Remove("numero");
            tableherramientas.Columns["cat"].ColumnName = "Categoria";
            tableherramientas.Columns["subcat"].ColumnName = "Activo";
            DataTable tablevehiculos = (DataTable)Session["Tabla_Vehiculos"];
            tablevehiculos.Columns.Remove("idc_vehiculo");
            tablevehiculos.Columns.Remove("idc_puesto");
            DataTable tablevehiculos_herr = (DataTable)Session["Tabla_Vehiculos_Herramientas"];
            tablevehiculos_herr.Columns["descripcion"].ColumnName = "Herramienta";
            tablevehiculos_herr.Columns["cantidad"].ColumnName = "Cantidad";
            tablevehiculos_herr.Columns["costo"].ColumnName = "Costo Unitario";
            DataTable tablecelulares = (DataTable)Session["Tabla_Celulares"];
            tablecelulares.Columns.Remove("idc_puesto");
            tablecelulares.Columns.Remove("idc_lineacel");
            tablecelulares.Columns.Remove("idc_modelocel");
            Export Export = new Export();
            //array de DataTables
            List<DataTable> ListaTables = new List<DataTable>();
            ListaTables.Add(tableherramientas);
            ListaTables.Add(tablevehiculos);
            ListaTables.Add(tablevehiculos_herr);
            ListaTables.Add(tablecelulares);
            //array de nombre de sheets
            string[] Nombres = new string[] { "Herramientas", "Vehiculos", "Herramientas de Vehiculos", "Celulares" };
            if (tableherramientas.Rows.Count == 0 & tablevehiculos.Rows.Count == 0 & tablevehiculos_herr.Rows.Count == 0 & tablecelulares.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("Este Puesto no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
            }
            else
            {
                string mensaje = Export.toExcel("Herramientas de " + lblNombre.Text, XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                               XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 4,
                               "herramientas_" + lblPuesto.Text + ".xlsx", Page.Response);
                if (mensaje != "")
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }

        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            DataTable tableherramientas = (DataTable)Session["Tabla_HerramientasGlobal"];
            tableherramientas.Columns.Remove("idc_activo");
            tableherramientas.Columns.Remove("area_comun");
            tableherramientas.Columns.Remove("idc_actscategoria");
            tableherramientas.Columns.Remove("idc_areaact");
            tableherramientas.Columns.Remove("numero");
            tableherramientas.Columns["cat"].ColumnName = "Categoria";
            tableherramientas.Columns["subcat"].ColumnName = "Activo";
            DataTable tablevehiculos = (DataTable)Session["Tabla_Vehiculos"];
            tablevehiculos.Columns.Remove("idc_vehiculo");
            tablevehiculos.Columns.Remove("idc_puesto");
            DataTable tablevehiculos_herr = (DataTable)Session["Tabla_Vehiculos_Herramientas"];
            tablevehiculos_herr.Columns["descripcion"].ColumnName = "Herramienta";
            tablevehiculos_herr.Columns["cantidad"].ColumnName = "Cantidad";
            tablevehiculos_herr.Columns["costo"].ColumnName = "Costo Unitario";
            DataTable tablecelulares = (DataTable)Session["Tabla_Celulares"];
            tablecelulares.Columns.Remove("idc_puesto");
            tablecelulares.Columns.Remove("idc_lineacel");
            tablecelulares.Columns.Remove("idc_modelocel");
            Export Export = new Export();
            //array de DataTables
            List<DataTable> ListaTables = new List<DataTable>();
            ListaTables.Add(tableherramientas);
            ListaTables.Add(tablevehiculos);
            ListaTables.Add(tablevehiculos_herr);
            ListaTables.Add(tablecelulares);
            //array de nombre de sheets
            string[] Nombres = new string[] { "Herramientas", "Vehiculos", "Herramientas de Vehiculos", "Celulares" };
            if (tableherramientas.Rows.Count == 0 & tablevehiculos.Rows.Count == 0 & tablevehiculos_herr.Rows.Count == 0 & tablecelulares.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("Este Puesto no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
            }
            else
            {
                string mensaje = Export.ToPdf("Herramientas de " + lblNombre.Text, ListaTables, 4, Nombres, Page.Response);
                if (mensaje != "")
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }
    }
}