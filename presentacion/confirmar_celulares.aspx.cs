using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class confirmar_celulares : System.Web.UI.Page
    {
        private string rutaimagen = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack) { CargarGridPrincipal(); }
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
                Celulares_EntregarENT entidad = new Celulares_EntregarENT();
                Celulares_EntregarCOM componente = new Celulares_EntregarCOM();
                entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                DataSet ds = componente.CargaConfirmaciones(entidad);
                Session["Tabla_Celulares"] = ds.Tables[0];
                Session["Tabla_Celulares_acc"] = ds.Tables[1];
                repeatCelulares.DataSource = ds.Tables[0];
                repeatCelulares.DataBind();
                repeatEquipoCelular.DataSource = ds.Tables[0];
                repeatEquipoCelular.DataBind();
                repeat_accesorios.DataSource = ds.Tables[1];
                repeat_accesorios.DataBind();
                Session["idc_entrega"] = ds.Tables[0].Rows[0]["idc_entrega"];
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Genera cadena con resultado de preparaciones
        /// </summary>
        /// <returns></returns>
        public string GeneraCadenaCelular()
        {
            string list = "";
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Celulares"];
            foreach (RepeaterItem item in repeatEquipoCelular.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblcelular1");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_entrega_cel"]) == Convert.ToInt32(lblidc.Text))
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
        public int TotalCadenaCelular()
        {
            int list = 0;
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Celulares"];
            foreach (RepeaterItem item in repeatEquipoCelular.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblcelular1");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_entrega_cel"]) == Convert.ToInt32(lblidc.Text))
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
        public string GeneraCadenaAccesorios()
        {
            string list = "";
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Celulares_acc"];
            foreach (RepeaterItem item in repeat_accesorios.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones1");
                Label lblidc = (Label)item.FindControl("lblaccesorio");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_entrega_celacc"]) == Convert.ToInt32(lblidc.Text))
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
        public int TotalCadenaAccesorios()
        {
            int list = 0;
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Celulares_acc"];
            foreach (RepeaterItem item in repeat_accesorios.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones1");
                Label lblidc = (Label)item.FindControl("lblaccesorio");
                if (cbx.Enabled == true && cbx.Checked == true)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_entrega_celacc"]) == Convert.ToInt32(lblidc.Text))
                        {
                            list = list + 1;
                        }
                    }
                }
            }
            return list;
        }

        protected void repeatCelulares_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
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
                DataTable tabla_accesorios = (DataTable)Session["Tabla_Celulares_acc"];
                DataTable tabla_grid_accesorios = new DataTable();
                tabla_grid_accesorios.Columns.Add("idc_celular");
                tabla_grid_accesorios.Columns.Add("idc_puesto");
                tabla_grid_accesorios.Columns.Add("descripcion");
                //buscamos los accesorios con el id del celular
                foreach (DataRow row in tabla_accesorios.Rows)
                {
                    if (Convert.ToInt32(row["idc_lineacel"]) == Convert.ToInt32(idc_lineacel))
                    {
                        DataRow row_new = tabla_grid_accesorios.NewRow();
                        row_new["idc_celular"] = row["idc_celular"];
                        row_new["idc_puesto"] = row["idc_puesto"];
                        row_new["descripcion"] = row["descripcion"];
                        tabla_grid_accesorios.Rows.Add(row_new);
                    }
                }
                repeatAccesorios.DataSource = tabla_grid_accesorios;
                repeatAccesorios.DataBind();
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

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    try
                    {
                        Celulares_EntregarENT entidad = new Celulares_EntregarENT();
                        Celulares_EntregarCOM componente = new Celulares_EntregarCOM();
                        entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                        entidad.Cadcel = GeneraCadenaCelular();
                        entidad.Totalcadcel = TotalCadenaCelular();
                        entidad.Cadcel_acc = GeneraCadenaAccesorios();
                        entidad.Totalcadcel_acc = TotalCadenaAccesorios();
                        entidad.Pidc_entrega = Convert.ToInt32(Session["idc_entrega"]);
                        entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
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
            if (TotalCadenaCelular() == 0 && TotalCadenaAccesorios() == 0)
            {
                Alert.ShowAlertError("Para Guardar, debe Confirmar el equipo Celular", this);
                error = true;
            }
            foreach (RepeaterItem item in repeatEquipoCelular.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones");
                Label lblidc = (Label)item.FindControl("lblcelular1");
                if (txt.Text == "" && cbx.Checked == false)
                {
                    error = true;
                    Alert.ShowAlertError("Debe colocar una observacion si rechaza la entrega", this);
                }
            }
            foreach (RepeaterItem item in repeat_accesorios.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                TextBox txt = (TextBox)item.FindControl("txtObservaciones1");
                Label lblidc = (Label)item.FindControl("lblaccesorio");
                if (txt.Text == "" && cbx.Checked == false)
                {
                    error = true;
                    Alert.ShowAlertError("Debe colocar una observacion si rechaza la entrega", this);
                }
            }
            if (error != true)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de confirmar que recibio los celulares y/o accesorios marcados. Todos sus datos son correctos?');", true);
            }
        }
    }
}