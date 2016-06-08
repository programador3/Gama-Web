using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class celulares_revision : System.Web.UI.Page
    {
        public string rutaimagen = "";//para controles dinamicos
        public int idc_usuarioprebaja = 0;
        public int idc_prebaja = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            //si nop trae valores regreso
            if (Request.QueryString["idc_puestoprebaja"] == null)
            {
                Response.Redirect("revisiones.aspx");
            }
            if (Request.QueryString["preview"] == null)//si no viene del administrador
            {
                //bajo valores
                int idc_puestoprebaja = Convert.ToInt32(Request.QueryString["idc_puestoprebaja"].ToString());
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_usuarioprebaja = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_puestorev = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                if (!Page.IsPostBack)
                {
                    //cargo datos a tablas
                    CargarGridPrincipal(idc_usuario, idc_puestorev, idc_puestoprebaja);
                }
                //genero datos del empleado
                GenerarDatosEmpleado(idc_puestoprebaja);
                SumaCosto();
            }
            else
            {//SI VIENE DEL DMINISTRADOR
                //bajo valores
                int idc_puestoprebaja = Convert.ToInt32(Request.QueryString["idc_puestoprebaja"].ToString());
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_usuarioprebaja = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_puestorev = Convert.ToInt32(Request.QueryString["idc_puestorev"].ToString());
                if (!Page.IsPostBack)
                {
                    //cargo datos a tablas
                    CargarGridPrincipal(idc_usuario, idc_puestorev, idc_puestoprebaja);
                    Alert.ShowAlertInfo("Esta pagina esta en modo informativo, NO PODRA guardar la Revisión. Si desea hacerlo, verifique el apartado de notificaciones", "Mensaje del sistema", this);
                }
                //genero datos del empleado
                GenerarDatosEmpleado(idc_puestoprebaja);
                btnGuardar.Visible = false;
                lnkRevision.Visible = false;
                lnkDetalles.Visible = false;
            }
        }

        /// <summary>
        /// Carga los datos en Tablas de sesion y carga el grid principal
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal(int idc_usuario, int idc_puestorevi, int idc_puestoprebaja)
        {
            try
            {
                Celulares_RevisionesENT entidad = new Celulares_RevisionesENT();
                Celulares_RevisionesCOM componente = new Celulares_RevisionesCOM();
                entidad.Idc_puestorevi = idc_puestorevi;
                entidad.Idc_puestoprebaja = idc_puestoprebaja;
                DataSet ds = componente.CargaHerramientasRevision(entidad);
                Session["Tabla_DatosEmpleado"] = ds.Tables[0];
                Session["Tabla_Celulares"] = ds.Tables[1];
                Session["Tabla_Celulares_acc"] = ds.Tables[2];
                repeatCelulares.DataSource = ds.Tables[1];
                repeatCelulares.DataBind();
                repeatEquipoCelular.DataSource = ds.Tables[1];
                repeatEquipoCelular.DataBind();
                repeat_accesorios.DataSource = ds.Tables[2];
                repeat_accesorios.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Genera los datos personales del empleado actual(prebaja)
        /// </summary>
        /// <param name="idc_puestoprebaja"></param>
        private void GenerarDatosEmpleado(int idc_puestoprebaja)
        {
            DataTable tabla = (DataTable)Session["Tabla_DatosEmpleado"];
            //si el id de empleado es igaul saco los datos
            foreach (DataRow row in tabla.Rows)
            {
                if (Convert.ToInt32(row["IDC_PUESTO"]) == idc_puestoprebaja)
                {
                    lblNombre.Text = row["empleado"].ToString();
                    lblPuesto.Text = row["descripcion"].ToString();
                    lblnomina.Text = row["num_nomina"].ToString();
                    lblmotivo.Text = row["motivo"].ToString();
                    idc_prebaja = Convert.ToInt32(row["idc_prebaja"].ToString());
                    GenerarRuta(Convert.ToInt32(row["idc_empleado"].ToString()), "fot_emp");
                }
            }
        }

        /// <summary>
        /// Genera el costo total acumulado
        /// </summary>
        public Decimal SumaCosto()
        {
            Decimal Total = 0;
            foreach (RepeaterItem item in repeatEquipoCelular.Items)
            {
                TextBox txtMoney = (TextBox)item.FindControl("txtMoney1");
                if (txtMoney.Text != "")
                {
                    Decimal cantidad = Convert.ToDecimal(txtMoney.Text);
                    if (cantidad < 0)
                    {
                        Alert.ShowAlertError("El Costo NO PUEDE TENER un valor negativo.", this);
                    }
                    Total = Total + cantidad;
                }
            }
            foreach (RepeaterItem item in repeat_accesorios.Items)
            {
                TextBox txtMoney = (TextBox)item.FindControl("txtMoney");
                if (txtMoney.Text != "")
                {
                    Decimal cantidad = Convert.ToDecimal(txtMoney.Text);
                    if (cantidad < 0)
                    {
                        Alert.ShowAlertError("El Costo NO PUEDE TENER un valor negativo.", this);
                    }
                    Total = Total + cantidad;
                }
            }
            txtTotal.Text = Total.ToString();
            return Total;
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
                            imgEmpleado.ImageUrl = url;
                        }
                        else
                        {
                            var url = "http://" + domn + carpeta + id_comprobar + ".jpg";
                            ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                            imgEmpleado.ImageUrl = url;
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
                    DataTable tabla_cel = (DataTable)Session["Tabla_Celulares"];
                    DataTable tabla_acc = (DataTable)Session["Tabla_Celulares_acc"];
                    Celulares_RevisionesENT entidad = new Celulares_RevisionesENT();
                    Celulares_RevisionesCOM componente = new Celulares_RevisionesCOM();
                    entidad.PIDC_prebaja = idc_prebaja;//IDC DE LA PREBAJA
                    entidad.Idc_usuario = idc_usuarioprebaja;
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.PCADTEL = GeneraCadena(tabla_cel);//cadena de equipos celulares
                    entidad.PNUMTEL = tabla_cel.Rows.Count;//cantidad de filas de la cadena
                    entidad.PCADACC = GeneraCadenaAccesorios(tabla_acc);//cadena de accesorios
                    entidad.PNUMACC = tabla_acc.Rows.Count;//filas de la cadena
                    SqlMoney costo = SqlMoney.Parse(SumaCosto().ToString());
                    entidad.PMONTO = costo;//monto total
                    entidad.Comentario = txtComentarios.Text.ToUpper();//comentarios
                    try
                    {
                        DataSet ds = componente.InsertarRevisionCelular(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        string mensaje = row["mensaje"].ToString();
                        if (mensaje == "")//no hay errores retornamos true
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La revisión fue guardada correctamente.','revisiones.aspx');", true);
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

                default:
                    break;
            }
        }

        protected void repeatEquipoCelular_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string idc_activo = Convert.ToString(DataBinder.Eval(dbr, "idc_celular1"));
            Label lblidc = (Label)e.Item.FindControl("lblcelular1");
            lblidc.Text = idc_activo;
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
                DataTable tabla_accesorios = (DataTable)Session["Tabla_Celulares_acc"];
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

        protected void lnkRevision_Click(object sender, EventArgs e)
        {
            lblSelectedCel.Text = "Revisión de Celulares y Lineas <i class='fa fa-check-square-o'></i>";
            lnkRevision.CssClass = "btn btn-primary active";
            lnkDetalles.CssClass = "btn btn-link";
            PanelDetallesCelular.Visible = false;
            PanelRevisaCelulares.Visible = true;
        }

        protected void lnkDetalles_Click(object sender, EventArgs e)
        {
            lblSelectedCel.Text = "Detalles de Celulares y Lineas <i class='fa fa-list-alt'></i>";
            lnkDetalles.CssClass = "btn btn-primary active";
            lnkRevision.CssClass = "btn btn-link";
            PanelDetallesCelular.Visible = true;
            PanelRevisaCelulares.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            DataTable tabla = (DataTable)Session["Tabla_DatosEmpleado"];
            DataTable tabla_herramientas = (DataTable)Session["Tabla_Herramientas"];
            int idc_puestoprebaja = Convert.ToInt32(Request.QueryString["idc_puestoprebaja"].ToString());
            DataRow[] result = tabla.Select("idc_puesto = " + idc_puestoprebaja + "");
            //VALIDAMOS QUE SI NO ENTREGO, TENGA UN COSTO ASIGNADO
            Boolean error = false;
            foreach (RepeaterItem item in repeatEquipoCelular.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                TextBox txtMoney = (TextBox)item.FindControl("txtMoney1");
                if (cbx.Checked == false)
                {
                    double cantidad = Convert.ToDouble(txtMoney.Text);
                    if (cantidad <= 0.0 | cantidad == 0)
                    {
                        Alert.ShowAlertError("Si no entrego el equipo Celular, debe tener un costo de aproximación MAYOR A 0 CERO", this);

                        error = true;
                    }
                }
            }
            foreach (RepeaterItem item in repeat_accesorios.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txtMoney = (TextBox)item.FindControl("txtMoney");
                if (cbx.Checked == false)
                {
                    double cantidad = Convert.ToDouble(txtMoney.Text);
                    if (cantidad <= 0.0 | cantidad == 0)
                    {
                        Alert.ShowAlertError("Si no entrego el Accesorio debe tener un costo de aproximación MAYOR A 0 CERO", this);

                        error = true;
                    }
                }
            }
            if (!error == true)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                string d = SumaCosto().ToString();
                string mensaje = SumaCosto().ToString() == "0.00" ? "¿Esta a punto de guardar la Revisión. Si esto sucede no podra ser modificada. Todos sus datos son correctos?" : "¿Esta a punto de guardar la Revisión Y Generar un vale por un total de " + SumaCosto().ToString() + ". Si esto sucede no podra ser modificada. Todos sus datos son correctos?";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','" + mensaje + "');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            int idc_puestoprebaja = Convert.ToInt32(Request.QueryString["idc_puestoprebaja"].ToString());
            Response.Redirect("celulares_revision.aspx?idc_puestoprebaja=" + idc_puestoprebaja);
            PanelDetallesCelular.Visible = true;
            PanelRevisaCelulares.Visible = false;
        }

        protected void cbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbxsend = (CheckBox)sender;
            foreach (RepeaterItem item in repeatEquipoCelular.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtMoney1");
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                Label lblcosto = (Label)item.FindControl("lblcosto1");
                if (cbx.Checked == true) { txt.ReadOnly = true; txt.Text = "0.00"; }
                if (cbx.Checked == false) { txt.Text = lblcosto.Text; txt.ReadOnly = false; }
            }
        }

        protected void cbx_CheckedChangedAcce(object sender, EventArgs e)
        {
            CheckBox cbxsend = (CheckBox)sender;
            foreach (RepeaterItem item in repeat_accesorios.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtMoney");
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                Label lblcosto = (Label)item.FindControl("lblcosto");
                if (cbx.Checked == true) { txt.ReadOnly = true; txt.Text = "0.00"; }
                if (cbx.Checked == false) { txt.Text = lblcosto.Text; txt.ReadOnly = false; }
            }
        }

        protected void txtMoney_TextChanged(object sender, EventArgs e)
        {
            TextBox txtMoney = (TextBox)sender;
            if (txtMoney.Text != "")
            {
                double cantidad = Convert.ToDouble(txtMoney.Text);
                if (cantidad < 0.0)
                {
                    Alert.ShowAlertError("El Costo NO PUEDE TENER un valor negativo.", this);
                }
            }
        }

        protected void lbkseltodo_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeatEquipoCelular.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtMoney1");
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                cbx.Checked = true;
                txt.ReadOnly = true;
                txt.Text = "0.00";
            }
            foreach (RepeaterItem item in repeat_accesorios.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtMoney");
                CheckBox cbx = (CheckBox)item.FindControl("cbx");

                cbx.Checked = true;
                txt.ReadOnly = true;
                txt.Text = "0.00";
            }
            lbkseltodo.Visible = false;
            lnkDes.Visible = true;
            SumaCosto();
        }

        protected void lbkdestodo_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeatEquipoCelular.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtMoney1");
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                Label lblcosto = (Label)item.FindControl("lblcosto1");
                txt.Text = lblcosto.Text;
                cbx.Checked = false;
                txt.ReadOnly = false;
            }
            foreach (RepeaterItem item in repeat_accesorios.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtMoney");
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                Label lblcosto = (Label)item.FindControl("lblcosto");
                txt.Text = lblcosto.Text;
                cbx.Checked = false;
                txt.ReadOnly = false;
            }
            lbkseltodo.Visible = true;
            lnkDes.Visible = false;
            SumaCosto();
        }

        /// <summary>
        /// Genera cadena con resultado de Equipos Celulares
        /// </summary>
        /// <returns></returns>
        public string GeneraCadena(DataTable table)
        {
            string list = null;
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = table;
            foreach (RepeaterItem item in repeatEquipoCelular.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx1");
                TextBox txt = (TextBox)item.FindControl("txtMoney1");
                Label lblidc = (Label)item.FindControl("lblcelular1");
                foreach (DataRow row in tabla.Rows)
                {
                    if (Convert.ToInt32(row["idc_celular"]) == Convert.ToInt32(lblidc.Text))
                    {
                        int resultcbx = 0;
                        if (cbx.Checked == true) { resultcbx = 1; }
                        list = list + (lblidc.Text + ";" + resultcbx.ToString() + ";" + txt.Text + ";");
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Genera cadena con resultado de Equipos Celulares
        /// </summary>
        /// <returns></returns>
        public string GeneraCadenaAccesorios(DataTable table)
        {
            string list = null;
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = table;
            foreach (RepeaterItem item in repeat_accesorios.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                TextBox txt = (TextBox)item.FindControl("txtMoney");
                Label lblidc = (Label)item.FindControl("lblcelular");
                Label lblidaccesorio = (Label)item.FindControl("lblaccesorio");
                int resultcbx = 0;
                if (cbx.Checked == true) { resultcbx = 1; }
                list = list + (lblidc.Text + ";" + lblidaccesorio.Text + ";" + resultcbx.ToString() + ";" + txt.Text + ";");
            }
            return list;
        }
    }
}