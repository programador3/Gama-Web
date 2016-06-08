using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class cambiar_fechas_compromiso : System.Web.UI.Page
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
        }

        /// <summary>
        /// Carga los puestos pendientes de preparar
        /// </summary>
        public void DataPrep()
        {
            CandidatosENT entidad = new CandidatosENT();
            CandidatosCOM componente = new CandidatosCOM();
            entidad.Pidc_puesto = 0;
            entidad.Pidc_prepara = 0;
            entidad.Pidc_puestobaja = Convert.ToInt32(Session["sidc_puesto_login"]);
            DataSet ds = componente.CargaPuestos(entidad);
            Repeater1.DataSource = ds.Tables[0];
            Repeater1.DataBind();
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            CandidatosENT entidad = new CandidatosENT();
            CandidatosCOM componente = new CandidatosCOM();
            DataTable tabla_archivos = (DataTable)Session["tabla_archivos"];
            entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
            entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
            entidad.Pusuariopc = funciones.GetUserName();//usuario pc
            switch (Confirma_a)
            {
                case "Eliminar Candidato":
                    entidad.Pidc_candidato = Convert.ToInt32(Session["idc_pre_empleado"]);
                    entidad.Pidc_prepara = Convert.ToInt32(Session["idc_prepara"]);
                    entidad.Pidc_puestobaja = Convert.ToInt32(Session["idc_puesto"].ToString());
                    try
                    {
                        DataSet ds = componente.BorrarCandidato(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        string mensaje = row["mensaje"].ToString();
                        if (mensaje != "")//no hay errores retornamos true
                        {
                            Alert.ShowAlertError(mensaje, this);
                        }
                        else
                        {//mostramos error
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('El candidato fue Eliminado correctamente.','" + HttpContext.Current.Request.Url.AbsoluteUri + "','success');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.Message, this);
                    }
                    break;

                case "Capacitar":
                    entidad.Pidc_prepara = Convert.ToInt32(Session["idc_prepara"]);
                    entidad.Pidc_candidato = Convert.ToInt32(Session["idc_candidato"].ToString());
                    entidad.Pidc_puestobaja = Convert.ToInt32(Session["idc_puesto"].ToString());
                    try
                    {
                        DataSet ds = componente.TodoPreparado(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        bool listo = Convert.ToBoolean(row["listo"]);
                        bool capacitacion = Convert.ToBoolean(row["capacitacion"]);
                        string mensaje = row["mensaje"].ToString();
                        if (listo == false)//no hay errores retornamos true
                        {
                            Session["Previus"] = HttpContext.Current.Request.Url.AbsoluteUri;
                            Alert.ShowAlertConfirm(mensaje, "No puede continuar", "administrador_preparaciones.aspx", this);
                        }
                        else
                        {
                            Session["Previus"] = HttpContext.Current.Request.Url.AbsoluteUri;
                            Response.Redirect("pre_empleados_captura.aspx?idc_candidato=" + Session["idc_candidato"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.Message, this);
                    }
                    break;

                case "Compromiso":
                    try
                    {
                        entidad.Cadarch = CadenaFechas();
                        entidad.Totalcadsarch = TotalCadenaFechas();
                        DataSet ds = componente.CambiarFechaCompromisocADENA(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores

                        string mensaje = row["mensaje"].ToString();
                        if (mensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos procesando las preparaciones.", "Espere un Momento", "candidatos_preparar.aspx", "imagenes/loading.gif", "3000", "Los cambios de fecha fueron guardados correctamente", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(mensaje, this);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.Message, this);
                    }
                    break;
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            TextBox txtnueva_fecha = (TextBox)e.Item.FindControl("txtnueva_fecha");
            //Panel Panel = (Panel)e.Item.FindControl("panel_revision");
            DateTime fecfecha_compromiso_recluha = Convert.ToDateTime(DataBinder.Eval(dbr, "fecha_compromiso_reclu"));
            txtnueva_fecha.Text = fecfecha_compromiso_recluha.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
        }

        private string CadenaFechas()
        {
            string cadena = "";
            foreach (RepeaterItem item in Repeater1.Items)
            {
                TextBox txtnueva_fecha = (TextBox)item.FindControl("txtnueva_fecha");
                TextBox txtmotivo = (TextBox)item.FindControl("txtmotivo");
                Label lblid = (Label)item.FindControl("lblid");
                cadena = cadena + lblid.Text + ";" + Convert.ToDateTime(txtnueva_fecha.Text).ToString("yyyy/dd/MM HH:mm:ss").Replace('T', ' ') + ";" + txtmotivo.Text + ";";
            }
            return cadena;
        }

        private int TotalCadenaFechas()
        {
            int cadena = 0;
            foreach (RepeaterItem item in Repeater1.Items)
            {
                cadena = cadena + 1;
            }
            return cadena;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Compromiso";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Desea Guardar Los Cambios');", true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("candidatos_preparar.aspx");
        }
    }
}