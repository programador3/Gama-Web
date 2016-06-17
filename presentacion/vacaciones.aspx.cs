using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class vacaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            //SI ES UN ALTA
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("fecha");
                dt.Columns.Add("fecha_texto");
                Session["tabla_fechas_vaca"] = dt;
                Session["IDC_EMPLEADO_VACA"] = null;
                txtpagadas.Text = "0";
                txttomadas.Text = "0";
                CargarGridPrincipal(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"])));
            }
        }

        public void CargarGridPrincipal(int idc_puesto)
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
                    Session["IDC_EMPLEADO_VACA"] = Convert.ToInt32(ds.Tables[0].Rows[0]["idc_empleado"]);
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
                DataSet ds2 = componente.CargarInfoVacaciones(entidad);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    int total = Convert.ToInt32(ds2.Tables[0].Rows[0]["final"]);
                    if (total < 0)
                    {
                        lbldiasdisp.Text = "0";
                        Alert.ShowAlertInfo("NO tiene dias de vacaciones disponibles", "Mensaje del Sistema", this);
                        btnGuardar.Visible = false;
                    }
                    else
                    {
                        lbldiasdisp.Text = ds2.Tables[0].Rows[0]["final"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void txttomadas_TextChanged(object sender, EventArgs e)
        {
            if (ValidarDias() == true)
            {
                txttomadas.Text = "0";
                txtpagadas.Text = "0";
                Alert.ShowAlertInfo("No puede sobrepasar los dias de vacaciones disponibles(" + lbldiasdisp.Text + ")", "Mensaje del sistema", this);
            }
        }

        private Boolean ValidarDias()
        {
            bool error = false;
            int total_Dias = Convert.ToInt32(lbldiasdisp.Text);
            int dias1 = txtpagadas.Text == "" ? 0 : Convert.ToInt32(txtpagadas.Text);
            int dias2 = txttomadas.Text == "" ? 0 : Convert.ToInt32(txttomadas.Text);
            if ((dias1 + dias2) > total_Dias)
            {
                error = true;
            }
            return error;
        }

        protected void gridfechas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            DateTime fecha = Convert.ToDateTime(gridfechas.DataKeys[index].Values["fecha"]);
            DataTable dt = (DataTable)Session["tabla_fechas_vaca"];
            foreach (DataRow row in dt.Rows)
            {
                DateTime dat = Convert.ToDateTime(row["fecha"]);
                if (fecha == dat)
                {
                    row.Delete();
                    break;
                }
            }
            Session["tabla_fechas_vaca"] = dt;
            gridfechas.DataSource = dt;
            gridfechas.DataBind();
        }

        private String cadena()
        {
            string cadena = "";
            DataTable dt = (DataTable)Session["tabla_fechas_vaca"];
            foreach (DataRow row in dt.Rows)
            {
                cadena = cadena + Convert.ToDateTime(row["fecha"]).ToString("yyyy/dd/MM") + ";";
            }
            return cadena;
        }

        private int total()
        {
            DataTable dt = (DataTable)Session["tabla_fechas_vaca"];
            return dt.Rows.Count;
        }

        private string ValidarFechas(DateTime date)
        {
            string mensaje = "";
            DataTable dt = (DataTable)Session["tabla_fechas_vaca"];
            int tomadas = txttomadas.Text == "" ? 0 : Convert.ToInt32(txttomadas.Text);
            foreach (DataRow row in dt.Rows)
            {
                DateTime dat = Convert.ToDateTime(row["fecha"]);
                if (date == dat)
                {
                    mensaje = "Ya existe la Fecha " + dat.ToString("dd, MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
                }
            }
            if (dt.Rows.Count >= tomadas)
            {
                mensaje = "Ya sobrepaso el total de " + tomadas.ToString() + " dia(s) disponible(s) para vacaciones marcados como TOMADOS";
            }
            VacacionesENT entidad = new VacacionesENT();
            VacacionesCOM componente = new VacacionesCOM();
            entidad.Pidc_empleados = Convert.ToInt32(Session["IDC_EMPLEADO_VACA"]);
            entidad.Pfecha = date;
            DataSet ds = componente.Validacion(entidad);
            if (ds.Tables[0].Rows[0]["mensaje"].ToString() != "")
            {
                mensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
            }
            return mensaje;
        }

        private void AddToTable(DateTime date)
        {
            DataTable dt = (DataTable)Session["tabla_fechas_vaca"];
            DataRow row = dt.NewRow();
            row["fecha"] = date;
            row["fecha_texto"] = date.ToString("dd, MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
            dt.Rows.Add(row);
            Session["tabla_fechas_vaca"] = dt;
            gridfechas.DataSource = dt;
            gridfechas.DataBind();
        }

        protected void lnkaddd_Click(object sender, EventArgs e)
        {
            if (txtfecha.Text == "")
            {
                Alert.ShowAlertError("Escriba una fecha", this);
            }
            else if (Convert.ToDateTime(txtfecha.Text) <= DateTime.Today)
            {
                Alert.ShowAlertError("Escriba una fecha mayor a hoy", this);
            }
            else if (ValidarFechas(Convert.ToDateTime(txtfecha.Text)) != "")
            {
                Alert.ShowAlertError(ValidarFechas(Convert.ToDateTime(txtfecha.Text)), this);
            }
            else
            {
                AddToTable(Convert.ToDateTime(txtfecha.Text));
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso = (string)Session["Caso_Confirmacion"];
            switch (caso)
            {
                case "Guardar":
                    try
                    {
                        VacacionesENT entidad = new VacacionesENT();
                        VacacionesCOM componente = new VacacionesCOM();
                        entidad.Pidc_empleados = Convert.ToInt32(Session["IDC_EMPLEADO_VACA"]);
                        entidad.Ptomadas = Convert.ToInt32(txttomadas.Text);
                        entidad.Ppagadas = Convert.ToInt32(txtpagadas.Text);
                        entidad.Pcadena_fechas = cadena();
                        entidad.Ptotal_cadena_fechas = total();
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        DataSet ds = componente.AgregarSolicitud(entidad);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos procesando la solicitud.", "Espere un Momento", "puestos_catalogo.aspx", "imagenes/loading.gif", "2000", "La Solicitud fue Guardada correctamente ", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.ToString(), this.Page);
                        Global.CreateFileError(ex.ToString(), this);
                    }

                    break;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int tomadas = txttomadas.Text == "" ? 0 : Convert.ToInt32(txttomadas.Text);
            int pagadas = txtpagadas.Text == "" ? 0 : Convert.ToInt32(txtpagadas.Text);
            if (txtpagadas.Text == "")
            {
                Alert.ShowAlertError("Ingrese un valor para los Dias Pagados", this);
            }
            else if (txttomadas.Text == "")
            {
                Alert.ShowAlertError("Ingrese un valor para los Dias Tomados", this);
            }
            else if (tomadas > 0 && tomadas > total())
            {
                Alert.ShowAlertError("Ingrese las fechas correspondientes para los dias tomados", this);
            }
            else if (tomadas <= 0 && pagadas <= 0)
            {
                Alert.ShowAlertError("Para solicitar debe ingresar por lo menos 1 dia", this);
            }
            else if (total() > tomadas)
            {
                Alert.ShowAlertError("Tiene " + total().ToString() + " fecha(s) ingresada(s) para tomar, ingrese el numero de dias tomados.", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar esta Solicitud?','modal fade modal-info');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("puestos_catalogo.aspx");
        }
    }
}