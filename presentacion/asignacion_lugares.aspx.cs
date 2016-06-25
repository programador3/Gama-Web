using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class asignacion_lugares : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["idc_lugart"] = null;
                Session["tbl_lugares_list"] = null;
                DataTable tbl = new DataTable();
                tbl.Columns.Add("idc_lugart");
                tbl.Columns.Add("area");
                tbl.Columns.Add("sucursal");
                tbl.Columns.Add("nombre");
                Session["tbl_area"] = tbl;
                Sucursales();
                int idc_puesto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"]));
                lugaresPuestos(idc_puesto);
                CargarPrincipal(idc_puesto);
            }
        }

        /// <summary>
        /// Carga el grid de lugares
        /// </summary>
        private void cargargrid()
        {
            DataTable dt = (DataTable)Session["tbl_area"];
            DataTable ntble = new DataTable();
            ntble.Columns.Add("idc_lugart");
            ntble.Columns.Add("area");
            ntble.Columns.Add("sucursal");
            ntble.Columns.Add("nombre");
            foreach (DataRow row in dt.Rows)
            {
                DataRow newr = ntble.NewRow();
                newr["idc_lugart"] = row["idc_lugart"];
                newr["area"] = row["area"];
                newr["sucursal"] = row["sucursal"];
                newr["nombre"] = row["nombre"];
                ntble.Rows.Add(newr);
            }
            grid_puestpos.DataSource = ntble;
            grid_puestpos.DataBind();
        }

        /// <summary>
        /// Colorea el repeat segun el estado de cada lugar
        /// </summary>
        private void cargarrepeat_lugares()
        {
            DataTable dt = (DataTable)Session["tbl_area"];
            DataTable dt2 = (DataTable)Session["tbl_lugares_list"];
            DataView view = dt2.DefaultView;
            foreach (RepeaterItem item in repeater_puestos.Items)
            {
                LinkButton btnLugar = (LinkButton)item.FindControl("btnLugar");
                int lugar = Convert.ToInt32(btnLugar.CommandArgument);
                string value = btnLugar.CommandName;
                btnLugar.CssClass = "btn btn-default btn-block";
                foreach (DataRow row in dt.Rows)
                {
                    if (value == row["idc_lugart"].ToString())
                    {
                        btnLugar.CssClass = "btn btn-warning btn-block";
                    }
                }
                view.RowFilter = "idc_lugart = " + value + " and lugar = 1";
                if (view.ToTable().Rows.Count > 0)
                {
                    btnLugar.CssClass = "btn btn-warning btn-block";
                }
            }
        }

        /// <summary>
        /// Carga los datos del empleado
        /// </summary>
        /// <param name="idc_puesto"></param>
        public void CargarPrincipal(int idc_puesto)
        {
            try
            {
                PuestosServiciosENT entidad = new PuestosServiciosENT();
                PuestosServiciosCOM componente = new PuestosServiciosCOM();
                entidad.Idc_Puesto = idc_puesto;
                entidad.PReves = false;
                DataSet ds = componente.CargaPuestos(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblPuesto.Text = ds.Tables[0].Rows[0]["descripcion"].ToString();
                    lblEmpleado.Text = ds.Tables[0].Rows[0]["empleado"].ToString();
                    lbldepto.Text = ds.Tables[0].Rows[0]["depto"].ToString();
                    Session["pidc_empleado_solic_horario"] = Convert.ToInt32(ds.Tables[0].Rows[0]["idc_empleado"]);
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
                        ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                        imgEmpleado.ImageUrl = url;
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
        /// Carga los lugares ocupados pór el puesto
        /// </summary>
        /// <param name="idc_puesto"></param>
        private void lugaresPuestos(int idc_puesto)
        {
            try
            {
                LugaresENT entidad = new LugaresENT();
                LugaresCOM comp = new LugaresCOM();
                entidad.Pidc_puesto = idc_puesto;
                DataSet ds = comp.CargaLugaresPuestos(entidad);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    AddToTable(Convert.ToInt32(row["idc_lugart"]), row["sucursal"].ToString(), row["area"].ToString(), row["nombre"].ToString());
                }
                cargargrid();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga las sucursales
        /// </summary>
        private void Sucursales()
        {
            try
            {
                LugaresENT entidad = new LugaresENT();
                LugaresCOM comp = new LugaresCOM();
                DataSet ds = comp.CargaSucursales(entidad);
                ddldeptos.DataValueField = "idc_sucursal";
                ddldeptos.DataTextField = "nombre";
                ddldeptos.DataSource = ds.Tables[0];
                ddldeptos.DataBind();
                ddldeptos.Items.Insert(0, new ListItem("Seleccione una Sucursal", "0")); //updated code}
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga las areas
        /// </summary>
        private void Areas()
        {
            try
            {
                LugaresENT entidad = new LugaresENT();
                LugaresCOM comp = new LugaresCOM();
                entidad.pidc_sucursal = ddldeptos.SelectedValue == "0" ? 0 : Convert.ToInt32(ddldeptos.SelectedValue);
                DataSet ds = comp.CargaAreas(entidad);
                ddlareas.DataValueField = "IDC_AREA";
                ddlareas.DataTextField = "nombre";
                ddlareas.DataSource = ds.Tables[0];
                ddlareas.DataBind();
                ddlareas.Items.Insert(0, new ListItem("Seleccione un Area", "0")); //updated code}
                CopyTofolder(ds.Tables[0], "~/imagenes/areas/");
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga los lugares
        /// </summary>
        /// <param name="idc_area"></param>
        private void Lugares(int idc_area)
        {
            try
            {
                LugaresENT entidad = new LugaresENT();
                LugaresCOM comp = new LugaresCOM();
                entidad.Pidc_area = idc_area;
                entidad.pidc_sucursal = ddldeptos.SelectedValue == "0" ? 0 : Convert.ToInt32(ddldeptos.SelectedValue);
                DataSet ds = comp.CargaAreas(entidad);
                CopyTofolder(ds.Tables[1], "~/imagenes/lugares/");
                repeater_puestos.DataSource = ds.Tables[1];
                repeater_puestos.DataBind();
                Session["tbl_lugares_list"] = ds.Tables[1];
                cargarrepeat_lugares();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Agrega una fila a la tabla temporal de lugares-puesto
        /// </summary>
        /// <param name="idc_lugart"></param>
        /// <param name="sucursal"></param>
        /// <param name="area"></param>
        /// <param name="nombre"></param>
        private void AddToTable(int idc_lugart, string sucursal, string area, string nombre)
        {
            try
            {
                DataTable dt = (DataTable)Session["tbl_area"];
                DataRow row = dt.NewRow();
                row["idc_lugart"] = idc_lugart;
                row["area"] = area;
                row["sucursal"] = sucursal;
                row["nombre"] = nombre;
                dt.Rows.Add(row);
                Session["tbl_area"] = dt;
                cargargrid();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// verufuca si existe una consulta en una tabla
        /// </summary>
        /// <param name="tbl"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private bool existintable(DataTable tbl, string query)
        {
            bool exist = false;
            DataView vie = tbl.DefaultView;
            vie.RowFilter = query;
            return vie.ToTable().Rows.Count > 0 ? true : false;
        }

        /// <summary>
        /// Copia las imagenes a la ruta local
        /// </summary>
        /// <param name="tbl"></param>
        /// <param name="folder"></param>
        private void CopyTofolder(DataTable tbl, string folder)
        {
            try
            {
                DirectoryInfo dirInfo2 = new DirectoryInfo(Server.MapPath("~/imagenes/lugares/"));//path local
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath(folder));//path local
                foreach (DataRow row in tbl.Rows)
                {
                    if (!File.Exists(row["ruta"].ToString()))
                    {
                        funciones.CopiarArchivos(dirInfo2 + "lugar.png", dirInfo + row["ruta_local"].ToString(), this);
                    }
                    else
                    {
                        funciones.CopiarArchivos(row["ruta"].ToString(), dirInfo + row["ruta_local"].ToString(), this);
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private String CadenaLugares()
        {
            string cadena = "";
            DataTable dt = (DataTable)Session["tbl_area"];
            foreach (DataRow row in dt.Rows)
            {
                cadena = cadena + row["idc_lugart"].ToString() + ";";
            }
            return cadena;
        }

        private int TotalCadena()
        {
            DataTable dt = (DataTable)Session["tbl_area"];
            return dt.Rows.Count;
        }

        protected void ddldeptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int depto = Convert.ToInt32(ddldeptos.SelectedValue);
            if (depto == 0)
            {
                Alert.ShowAlertError("Seleccione una Sucursal", this);
            }
            else
            {
                Areas();
            }
        }

        protected void ddlareas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int depto = Convert.ToInt32(ddlareas.SelectedValue);
            if (depto == 0)
            {
                Alert.ShowAlertError("Seleccione un Area", this);
            }
            else
            {
                Lugares(depto);
                divimgarea.Visible = true;
                lblareaname.Text = ddlareas.SelectedItem.ToString();
                imgarea.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["server"] + "/imagenes/areas/" + depto + ".jpg";
            }
        }

        protected void btnLugar_Click(object sender, EventArgs e)
        {
            LinkButton btnLugar_sender = (LinkButton)sender;
            //buscamos el control seleccionado en el repeat
            foreach (RepeaterItem item in repeater_puestos.Items)
            {
                LinkButton btnLugar = (LinkButton)item.FindControl("btnLugar");
                int lugar = Convert.ToInt32(btnLugar.CommandArgument);
                string value = btnLugar.CommandName;
                if (btnLugar == btnLugar_sender && btnLugar.CssClass == "btn btn-default btn-block")//si es igual y no esta asignado
                {
                    Session["idc_lugart"] = value;
                    btnLugar.CssClass = "btn btn-success btn-block";
                    imgarea.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["server"]+ "/imagenes/lugares/" + value + ".jpg";
                    DataTable dt = (DataTable)Session["tbl_lugares_list"];
                    DataView view = dt.DefaultView;
                    view.RowFilter = "idc_lugart = " + value + "";
                    lblareaname.Text = view.ToTable().Rows[0]["nombre"].ToString();
                }
                if (btnLugar == btnLugar_sender && btnLugar.CssClass == "btn btn-warning btn-block")//si esta asignado
                {
                    Session["idc_lugart"] = null;
                    Alert.ShowAlertInfo("El lugar " + btnLugar.Text + " ya esta ocupado", "Mensaje del Sistema", this);
                }
                if (btnLugar != btnLugar_sender && btnLugar.CssClass != "btn btn-warning btn-block")//limpiamos los demas
                {
                    btnLugar.CssClass = "btn btn-default btn-block";
                }
            }
        }

        protected void lnkagrgar_Click(object sender, EventArgs e)
        {
            if (existintable((DataTable)Session["tbl_area"], "idc_lugart = " + Convert.ToInt32(Session["idc_lugart"]) + "") == true)
            {
                Session["idc_lugart"] = null;
                Alert.ShowAlertError("Ya existe una relacion", this);
            }
            else if (Session["idc_lugart"] == null)
            {
                Alert.ShowAlertError("Seleccione un lugar", this);
            }
            else
            {
                string name = "";
                int lugar = 0;
                DataTable dt = (DataTable)Session["tbl_lugares_list"];
                DataView view = dt.DefaultView;
                view.RowFilter = "idc_lugart = " + Convert.ToInt32(Session["idc_lugart"]) + "";
                name = view.ToTable().Rows[0]["nombre"].ToString();
                AddToTable(Convert.ToInt32(Session["idc_lugart"]), ddldeptos.SelectedItem.ToString(), ddlareas.SelectedItem.ToString(), name);
                cargargrid();
                cargarrepeat_lugares();
                Session["idc_lugart"] = null;
            }
        }

        protected void gridlugares_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc = Convert.ToInt32(grid_puestpos.DataKeys[index].Values["idc_lugart"]);
            DataTable dt = (DataTable)Session["tbl_area"];
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt32(row["idc_lugart"]) == idc)
                {
                    row.Delete();
                    break;
                }
            }
            Session["tbl_area"] = dt;
            cargargrid();
            cargarrepeat_lugares();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar los cambios?','modal fade modal-info');", true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?','modal fade modal-info');", true);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                switch (caso)
                {
                    case "Guardar":
                        LugaresENT entidad = new LugaresENT();
                        LugaresCOM comp = new LugaresCOM();
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        entidad.Pcadena = CadenaLugares();
                        entidad.Ptotalcadea = TotalCadena();
                        entidad.Pidc_puesto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"]));
                        DataSet ds = comp.ModificarRelacionPuestosLugares(entidad);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos procesando los cambios", "Espere un Momento", "puestos_catalogo.aspx", "imagenes/loading.gif", "2000", "Movimientos Guardados Correctamente.", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                        break;

                    case "Cancelar":
                        Response.Redirect("puestos_catalogo.aspx");
                        break;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
    }
}