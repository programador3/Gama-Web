using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class confirmar_vehiculos : System.Web.UI.Page
    {
        public string rutaimagen = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
                CargarGridPrincipal();
        }

        /// <summary>
        /// Carga los datos en Tablas de sesion y carga el grid principal
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal()
        {
            try
            {
                Vehiculos_EntregaENT entidad = new Vehiculos_EntregaENT();
                Vehiculos_EntregarCOM componente = new Vehiculos_EntregarCOM();
                entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                DataSet ds = componente.CargarConfirmacion(entidad);
                Session["Tabla_Vehiculos"] = ds.Tables[0];
                Session["Tabla_Vehiculos_Herr"] = ds.Tables[1];
                RepeatVehiculos.DataSource = ds.Tables[0];
                RepeatVehiculos.DataBind();
                repeat_confirm_vehiculos.DataSource = ds.Tables[0];
                repeat_confirm_vehiculos.DataBind();
                repeat_herramientas.DataSource = ds.Tables[1];
                repeat_herramientas.DataBind();
                Session["idc_entrega"] = ds.Tables[0].Rows[0]["idc_entrega"];
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
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

        protected void RepeatVehiculos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //buscamos los controles dentro de repeat para asignar valores
            Image img = (Image)e.Item.FindControl("imgVehiculos");
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string idc_vehiculo = Convert.ToString(DataBinder.Eval(dbr, "idc_vehiculo"));
            //generamos ruta de imagen
            GenerarRuta(Convert.ToInt32(idc_vehiculo), "VEHIC");
            img.ImageUrl = rutaimagen;
            rutaimagen = "";//limpiamos variable global
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
                }
            }
        }

        /// <summary>
        /// Genera cadena con resultado de preparaciones
        /// </summary>
        /// <returns></returns>
        public string GeneraCadenaVehiculos()
        {
            string list = "";
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Vehiculos"];
            foreach (RepeaterItem item in repeat_confirm_vehiculos.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblvehiculo");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_entrega_veh"]) == Convert.ToInt32(lblidc.Text))
                        {
                            list = list + (lblidc.Text + ";" + cbx.Checked.ToString() + ";" + txt.Text + ";");
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Genera cadena con resultado de preparaciones
        /// </summary>
        /// <returns></returns>
        public int TotalCadenaVehiculos()
        {
            int list = 0;
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Vehiculos"];
            foreach (RepeaterItem item in repeat_confirm_vehiculos.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblvehiculo");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_entrega_veh"]) == Convert.ToInt32(lblidc.Text))
                        {
                            list = list + 1;
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Genera cadena con resultado de preparaciones
        /// </summary>
        /// <returns></returns>
        public string GeneraCadenaAccesoriosVehiculos_Herr()
        {
            string list = "";
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Vehiculos_Herr"];
            foreach (RepeaterItem item in repeat_herramientas.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones1");
                Label lblidc = (Label)item.FindControl("lblherra");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_entrega_veh_herr"]) == Convert.ToInt32(lblidc.Text))
                        {
                            list = list + (lblidc.Text + ";" + cbx.Checked.ToString() + ";" + txt.Text + ";");
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Genera cadena con resultado de preparaciones
        /// </summary>
        /// <returns></returns>
        public int TotalCadenaVehiculos_Herr()
        {
            int list = 0;
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Vehiculos_Herr"];
            foreach (RepeaterItem item in repeat_herramientas.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones1");
                Label lblidc = (Label)item.FindControl("lblherra");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_entrega_veh_herr"]) == Convert.ToInt32(lblidc.Text))
                        {
                            list = list + 1;
                        }
                    }
                }
            }
            return list;
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    Vehiculos_EntregaENT entidad = new Vehiculos_EntregaENT();
                    Vehiculos_EntregarCOM componente = new Vehiculos_EntregarCOM();
                    entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                    entidad.Cadveh = GeneraCadenaVehiculos();
                    entidad.Totalcadveh = TotalCadenaVehiculos();
                    entidad.Cadveh_herr = GeneraCadenaAccesoriosVehiculos_Herr();
                    entidad.Totalcadveh_herr = TotalCadenaVehiculos_Herr();
                    entidad.Pidc_entrega = Convert.ToInt32(Session["idc_entrega"]);
                    entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    try
                    {
                        DataSet ds = componente.InsertarConfirmacion(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        string mensaje = row["mensaje"].ToString();
                        if (mensaje == "")//no hay errores retornamos true
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La Confirmación de recibido fue guardada correctamente.','confirmacion_entrega.aspx');", true);
                        }
                        else
                        {//mostramos error
                            Alert.ShowAlertError(mensaje, this);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.ToString(), this.Page);
                        Global.CreateFileError(ex.ToString(), this);
                    }

                    break;

                case "Cancelar":
                    Response.Redirect("confirmacion_entrega.aspx");
                    break;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool error = false;
            //int CadenaCEL = TotalCadenaCelular();
            if (TotalCadenaVehiculos() == 0 && TotalCadenaVehiculos_Herr() == 0)
            {
                Alert.ShowAlertError("Para Guardar, debe Confirmar al menos el Vehiculo", this);
                error = true;
            }
            foreach (RepeaterItem item in repeat_confirm_vehiculos.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblvehiculo");
                if (txt.Text == "" && cbx.Checked == false)
                {
                    error = true;
                    Alert.ShowAlertError("Si rechaza la entrega, debe colocar una observación", this);
                }
            }
            foreach (RepeaterItem item in repeat_herramientas.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones1");
                Label lblidc = (Label)item.FindControl("lblherra");
                if (txt.Text == "" && cbx.Checked == false)
                {
                    error = true;
                    Alert.ShowAlertError("Si rechaza la entrega, debe colocar una observación", this);
                }
            }
            if (error != true)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de confirmar que recibio el vehiculo y sus herramientas marcadas. Todos sus datos son correctos?');", true);
            }
        }
    }
}