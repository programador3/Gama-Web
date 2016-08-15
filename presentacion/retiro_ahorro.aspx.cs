using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class retiro_ahorro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            //SI ES UNA AUTORIZACION
            if (!IsPostBack)
            {
                Session["pidc_empleado"] = null;
                CargarGridPrincipal(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"])));
                CargarInfo(Convert.ToInt32(Session["pidc_empleado"]));
            }
        }

        /// <summary>
        /// Carga los datos en Tablas de sesion y carga el grid principal
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
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
                    Session["pidc_empleado"] = Convert.ToInt32(ds.Tables[0].Rows[0]["idc_empleado"]);
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

        public void CargarInfo(int idc_empleado)
        {
            try
            {
                PuestosServiciosENT entidad = new PuestosServiciosENT();
                PuestosServiciosCOM componente = new PuestosServiciosCOM();
                entidad.Pidc_pre_empleado = idc_empleado;
                entidad.PReves = false;
                DataSet ds = componente.cARGARAHORROINFO(entidad);
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["fecval"]) == false)
                {
                    Alert.ShowAlertInfo(" Ya no estan Autorizados los Retiros", "Mensaje del Sistema", this);
                    txtretiro.ReadOnly = true;
                    solicita.Visible = false;
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[1].Rows[0];
                    txtsaldo.Text = Convert.ToDecimal(row["saldo_ahorro"]).ToString("#.##");
                    txtnumretiroa.Text = row["numret"].ToString();
                    txtsugerido.Text = Convert.ToDecimal(row["monto_sugerido"]).ToString("#.##");
                    if (Convert.ToDecimal(row["saldo_ahorro"]) == 0 || Convert.ToDecimal(row["retiro_aplicado"]) > 0 || Convert.ToBoolean(row["total"]) == true)
                    {
                        solicita.Visible = false;
                    }
                    if (Convert.ToBoolean(row["total"]) == true)
                    {
                        solicitud.Visible = false;
                        solicita.Visible = false;
                        tiene.Visible = true;
                    }
                    numret.Visible = false;
                    if (Convert.ToInt32(row["numret"]) >= 2)
                    {
                        numret.Visible = true;
                        txtretiro.Text = txtsaldo.Text;
                        txtretiro.ReadOnly = true;
                    }
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    DataRow rows = ds.Tables[2].Rows[0];
                    solicitud.Visible = false;
                    solicita.Visible = false;
                    tiene.Visible = true;
                    txtfechasolicitud.Text = rows["fecha"].ToString();
                    txtusuario.Text = rows["usuario"].ToString();
                    txtmonto_existe.Text = Convert.ToDecimal(rows["monto_retiro"]).ToString("#.##");
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                PuestosServiciosENT entidad = new PuestosServiciosENT();
                PuestosServiciosCOM componente = new PuestosServiciosCOM();
                entidad.Pidc_pre_empleado = Convert.ToInt32(Session["pidc_empleado"]);
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pretiro = Convert.ToDecimal(txtretiro.Text);
                DataSet ds = componente.SolicitarRetiro(entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje == "")
                {
                    string MESS = funciones.autorizacion(Convert.ToInt32(Session["sidc_usuario"]), 363) == true ? "Estamos Autorizando Automaticamente la Solicitud" : "Estamos Guardando los cambios.";
                    Alert.ShowGiftMessage(MESS, "Espere un Momento", "puestos_catalogo.aspx", "imagenes/loading.gif", "2000", "Solicitud Guardada correctamente ", this);
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
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            int retiro = txtretiro.Text == "" ? 0 : Convert.ToInt32(txtretiro.Text);
            int saldo = txtsaldo.Text == "" ? 0 : Convert.ToInt32(txtsaldo.Text);
            if (retiro > saldo)
            {
                Alert.ShowAlertError("El Monto a retirar excede el saldo disponible", this);
            }
            else if (retiro == 0)
            {
                Alert.ShowAlertError("Debe retirar un monto mayor a 0 pesos", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar esta Solicitud de Retiro de Ahorro?','modal fade modal-info');", true);
            }
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("puestos_catalogo.aspx");
        }

        protected void txtretiro_TextChanged(object sender, EventArgs e)
        {
            int retiro = txtretiro.Text == "" ? 0 : Convert.ToInt32(txtretiro.Text);
            int saldo = txtsaldo.Text == "" ? 0 : Convert.ToInt32(txtsaldo.Text);
            retirod.Visible = saldo == retiro ? true : false;
            if (retiro > saldo)
            {
                txtretiro.Text = "";
                Alert.ShowAlertError("El Monto a retirar excede el saldo disponible", this);
            }
            else if (retiro == 0)
            {
                txtretiro.Text = "";
                Alert.ShowAlertError("Debe retirar un monto mayor a 0 pesos", this);
            }
        }
    }
}