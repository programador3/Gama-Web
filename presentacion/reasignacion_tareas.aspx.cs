using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class reasignacion_tareas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["url_r"] = null;
                Session["tipo_reasignacion"] = null;
                Session["tabla_tareas_r"] = null;
                Session["opcion"] = null;
                Session["idc_realiza"] = null;
                Session["idc_asigna"] = null;
                Session["idc_tarea_r"] = null;
                Session["CAMBIO_MASIVO"] = false;
                Cargar();
            }
        }

        private void Cargar()
        {
            TareasENT entidad = new TareasENT();
            TareasCOM componente = new TareasCOM();
            DataSet ds = componente.CargarSinEmpleado(entidad);
            Repeater3.DataSource = ds.Tables[0];
            Repeater3.DataBind();
            Session["tabla_tareas_r"] = ds.Tables[0];
            ddlpuestosmasivo.DataTextField = "puesto";
            ddlpuestosmasivo.DataValueField = "idc_puesto";
            ddlpuestosmasivo.DataSource = ds.Tables[1];
            ddlpuestosmasivo.DataBind();
            ddlpuestosmasivo.Items.Insert(0, new ListItem("-- Seleccione un Puesto", "0")); //updated code}
            Session["tabla_tareas_masivo"] = ds.Tables[1];
        }

        /// <summary>
        /// Carga Puestos en Filtro
        /// </summary>
        public DataSet CargaPuestos(string filtro)
        {
            DataTable dt = new DataTable();
            try
            {
                Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
                Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
                entidad.Filtro = filtro;
                entidad.Idc_puesto_revisa = Convert.ToInt32(Session["sidc_puesto_login"]);
                DataSet ds = componente.CargaComboDinamico(entidad);
                return ds;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
                return null;
            }
        }

        /// <summary>
        /// Realiza un filtro a una tabla en epsecifico, regresa una fila completa, si no hay datos regresa NULL
        /// </summary>
        /// <param name="table"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private DataRow RowFilter(DataTable table, string query)
        {
            DataRow row = null;
            try
            {
                DataView view = table.DefaultView;
                view.RowFilter = query;
                row = view.ToTable().Rows[0];
                return row;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
                return null;
            }
        }

        private void IniciarCombosPuestos()
        {
            ddlPuesto.DataValueField = "idc_puesto";
            ddlPuesto.DataTextField = "descripcion_puesto_completa";
            ddlPuesto.DataSource = CargaPuestos("").Tables[0];
            ddlPuesto.DataBind();
            ddlPuesto.Items.Insert(0, new ListItem("-- Seleccione un Puesto", "0")); //updated code}
            ddlpuestorealiza.DataValueField = "idc_puesto";
            ddlpuestorealiza.DataTextField = "descripcion_puesto_completa";
            ddlpuestorealiza.DataSource = CargaPuestos("").Tables[0];
            ddlpuestorealiza.DataBind();
            ddlpuestorealiza.Items.Insert(0, new ListItem("-- Seleccione un Puesto", "0")); //updated code}
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            ddlPuesto.DataValueField = "idc_puesto";
            ddlPuesto.DataTextField = "descripcion_puesto_completa";
            ddlPuesto.DataSource = CargaPuestos(txtpuesto_filtro.Text).Tables[0];
            ddlPuesto.DataBind();
            //si no hay filtro insertamos una etiqueta inicial
            if (txtpuesto_filtro.Text == "")
            {
                ddlPuesto.Items.Insert(0, new ListItem("-- Seleccione un Puesto", "0")); //updated code}
            }
        }

        protected void txtfiltrorealiza_TextChanged(object sender, EventArgs e)
        {
            ddlpuestorealiza.DataValueField = "idc_puesto";
            ddlpuestorealiza.DataTextField = "descripcion_puesto_completa";
            ddlpuestorealiza.DataSource = CargaPuestos(txtfiltrorealiza.Text).Tables[0];
            ddlpuestorealiza.DataBind();
            //si no hay filtro insertamos una etiqueta inicial
            if (txtfiltrorealiza.Text == "")
            {
                ddlpuestorealiza.Items.Insert(0, new ListItem("-- Seleccione un Puesto", "0")); //updated code}
            }
        }

        protected void ddlpuestorealiza_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc_puesto = Convert.ToInt32(ddlpuestorealiza.SelectedValue);
            if (idc_puesto == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione un Puesto Valido, o intente buscando uno.", this);
            }
            else if (idc_puesto == Convert.ToInt32(Session["idc_realiza"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione un Puesto diferente al Original.", this);
            }
        }

        protected void ddlPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc_puesto = Convert.ToInt32(ddlPuesto.SelectedValue);
            if (idc_puesto == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione un Puesto Valido, o intente buscando uno.", this);
            }
            else if (idc_puesto == Convert.ToInt32(Session["idc_asigna"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione un Puesto diferente al Original.", this);
            }
        }

        protected void btnReasignar_Click(object sender, EventArgs e)
        {
            cancelar.Visible = false;
            reasignar.Visible = true;
            realiza.Visible = false;
            asigna.Visible = false;
            string tipo = (string)Session["tipo_reasignacion"];
            tipo = tipo.TrimEnd().TrimStart();
            Session["opcion"] = "Reasignar";
            //si es tipo asignacion
            if (tipo == "A")
            {
                realiza.Visible = false;
                asigna.Visible = true;
            }
            //si es tipo realiza
            else if (tipo == "R")
            {
                realiza.Visible = true;
                asigna.Visible = false;
            }
            //si son ambos
            else if (tipo == "AR" || tipo == "RA")
            {
                realiza.Visible = true;
                asigna.Visible = true;
            }
            IniciarCombosPuestos();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Reasignación de Tarea','modal fade modal-info');", true);
        }

        protected void btncancelartarea_Click(object sender, EventArgs e)
        {
            cancelar.Visible = true;
            reasignar.Visible = false;
            realiza.Visible = false;
            asigna.Visible = false;
            Session["opcion"] = "Cancelar";
            IniciarCombosPuestos();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Cancelación de Tarea','modal fade modal-danger');", true);
        }

        protected void btnir_Click(object sender, EventArgs e)
        {
            Response.Redirect((string)Session["url_r"]);
        }

        protected void lnkmistarea_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            Session["url_r"] = lnk.CommandName;
            DataRow row = RowFilter((DataTable)Session["tabla_tareas_r"], "idc_tarea = " + lnk.CommandArgument.ToString() + "");
            if (row != null)
            {
                Session["idc_asigna"] = Convert.ToInt32(row["idc_puesto_asigna"]);
                Session["idc_realiza"] = Convert.ToInt32(row["idc_puesto"]);
                Session["idc_tarea_r"] = Convert.ToInt32(row["idc_tarea"]);
                Session["fo"] = Convert.ToDateTime(row["fo"]);
                Session["tipo_reasignacion"] = row["tipo"].ToString();
                string mensaje = "¿Que acción desea realizar con la tarea " + row["desc_completa"].ToString() + "?. Asignada por el puesto: " + row["puesto_asigna"].ToString() + ", que Realizara el puesto: " + row["puesto"].ToString() + "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirmOpciones('" + mensaje + "');", true);
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                if (Convert.ToBoolean(Session["CAMBIO_MASIVO"]) == false)
                {
                    entidad.Pidc_tarea = Convert.ToInt32(Session["idc_tarea_r"]);
                    entidad.Pfecha = Convert.ToDateTime(Session["fo"]);
                    entidad.Pidc_puesto = Convert.ToInt32(ddlpuestorealiza.SelectedValue);
                    entidad.Pidc_puesto_asigna = Convert.ToInt32(ddlPuesto.SelectedValue);
                }
                else {
                    entidad.Pidc_tarea = 0;
                    entidad.Pfecha = DateTime.Today;
                    entidad.Pidc_puesto = 0;
                    entidad.Pidc_puesto_asigna = 0;
                }
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                string tipo = (string)Session["tipo_reasignacion"];
                tipo = tipo.TrimEnd().TrimStart();
                entidad.Ptipo_cambio_tarea = tipo;
                entidad.PAPLICAR_CAMBIOTODOS = Convert.ToBoolean(Session["CAMBIO_MASIVO"]);
                entidad.Ptotal_cadena_arch = TotalCadena();
                entidad.Pcadena_arch = Cadena();
                DataSet ds;
                String vmensaje = "";
                switch (caso)
                {
                    case "Reasignar":
                        entidad.Ptipo = "G";
                        entidad.Pdescripcion = "CAMBIO DE PUESTO EN TAREA";
                        ds = componente.ReasignarTarea(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Cancelar":
                        entidad.Ptipo = "Q";
                        entidad.Pcorrecto = true;
                        entidad.Pdescripcion = "CANCELACION DE TAREA DEBIDO A QUE SE DIO DE BAJA UN EMPLEADO RELACIONADO. " + txtcance.Text;
                        ds = componente.AgregarMovimiento(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;
                }
                if (vmensaje == "")
                {
                    txtcance.Text = "";
                    Alert.ShowGiftMessage("Estamos guardando los cambios", "Espere un Momento", "reasignacion_tareas.aspx", "imagenes/loading.gif", "2000", "El movimiento fue Guardado Correctamente", this);
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

        protected void YesSE_Click(object sender, EventArgs e)
        {
            //confirmacion es igual al caso: CANCELAR O REASIGNAR
            if (realiza.Visible == true && ddlpuestorealiza.SelectedValue == "0")
            {
                Alert.ShowAlertError("Seleccione Un Puesto que Realizara la Tarea", this);
            }
            else if (realiza.Visible == true && ddlpuestorealiza.SelectedValue == Convert.ToInt32(Session["idc_realiza"]).ToString())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione un Puesto que Realiza diferente al Original.", this);
            }
            else if (asigna.Visible == true && ddlPuesto.SelectedValue == Convert.ToInt32(Session["idc_puesto"]).ToString())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione un Puesto que Revisara diferente al Original.", this);
            }
            else if (asigna.Visible == true && ddlPuesto.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione un Puesto diferente al Original.", this);
            }
            else if (cancelar.Visible == true && txtcance.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Indique Observaciones para cancelar.", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = (string)Session["opcion"];
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessDFDFDFage", "ModalConfirmC();", true);
            }
        }

        protected void lnkmasivo_Click(object sender, EventArgs e)
        {
            ddlpuestocambmasivo.DataValueField = "idc_puesto";
            ddlpuestocambmasivo.DataTextField = "descripcion_puesto_completa";
            ddlpuestocambmasivo.DataSource = CargaPuestos("").Tables[0];
            ddlpuestocambmasivo.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessDFDFDFage", "ModalMasivo();", true);
        }

        protected void yesmasivp_Click(object sender, EventArgs e)
        {
            int idc_puesto = Convert.ToInt32(ddlpuestocambmasivo.SelectedValue);
            int idc_puestoca = Convert.ToInt32(ddlpuestosmasivo.SelectedValue);
            if (idc_puesto == 0 && MASIVO.Visible == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione un Puesto Valido, o intente buscando uno.", this);
            }
            else if (idc_puestoca == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione un Puesto para realizar el cambio", this);
            }
            else if (idc_puesto == Convert.ToInt32(ddlpuestosmasivo.SelectedValue) && MASIVO.Visible == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione un Puesto diferente al Original.", this);
            }
            else if (ddlaccionmasivo.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione una Acción.", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = ddlaccionmasivo.SelectedValue == "Q" ? "Cancelar" : "Reasignar";
                Session["CAMBIO_MASIVO"] = true;
                Session["tipo_reasignacion"] = "AR";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessDFDFDFage", "ModalConfirmC();", true);
            }
        }

        protected void ddlpuestocambmasivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc_puesto = Convert.ToInt32(ddlpuestocambmasivo.SelectedValue);
            if (idc_puesto == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione un Puesto Valido, o intente buscando uno.", this);
            }
            else if (idc_puesto == Convert.ToInt32(Session["idc_asigna"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione un Puesto diferente al Original.", this);
            }
        }

        protected void txtcambiomas_TextChanged(object sender, EventArgs e)
        {
            ddlpuestocambmasivo.DataValueField = "idc_puesto";
            ddlpuestocambmasivo.DataTextField = "descripcion_puesto_completa";
            ddlpuestocambmasivo.DataSource = CargaPuestos(txtcambiomas.Text).Tables[0];
            ddlpuestocambmasivo.DataBind();
            //si no hay filtro insertamos una etiqueta inicial
            if (txtcambiomas.Text == "")
            {
                ddlpuestocambmasivo.Items.Insert(0, new ListItem("-- Seleccione un Puesto", "0")); //updated code}
            }
        }

        protected void ddlaccionmasivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = ddlaccionmasivo.SelectedValue;
            if (value.Equals("0"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                Alert.ShowAlertError("Seleccione una Acción.", this);
            }
            else if (value.Equals("Q"))
            {
                MASIVO.Visible = false;
            }
            else if (value.Equals("G"))
            {
                MASIVO.Visible = true;
            }
        }

        private String Cadena()
        {
            string cadena = "";
            int idc_puesto_ori = Convert.ToInt32(ddlpuestosmasivo.SelectedValue);
            int idc_puesto_nuevo = Convert.ToInt32(ddlpuestocambmasivo.SelectedValue);
            DataTable dt = (DataTable)Session["tabla_tareas_r"];
            foreach (DataRow row in dt.Rows)
            {
                int idc_tarea = Convert.ToInt32(row["idc_tarea"]);
                int idc_puesto = Convert.ToInt32(row["idc_puesto"]);
                int idc_puesto_asigna = Convert.ToInt32(row["idc_puesto_asigna"]);
                if (idc_puesto == idc_puesto_ori && idc_puesto_asigna != idc_puesto_ori)//solo puesto
                {
                    cadena = cadena + idc_tarea.ToString() + ";" + idc_puesto_nuevo.ToString() + ";" + idc_puesto_asigna.ToString() + ";";
                }
                else if (idc_puesto != idc_puesto_ori && idc_puesto_asigna == idc_puesto_ori)//solo puesto asigna
                {
                    cadena = cadena + idc_tarea.ToString() + ";" + idc_puesto.ToString() + ";" + idc_puesto_nuevo.ToString() + ";";
                }
                else if (idc_puesto == idc_puesto_ori && idc_puesto_asigna == idc_puesto_ori)//ambos
                {
                    cadena = cadena + idc_tarea.ToString() + ";" + idc_puesto_nuevo.ToString() + ";" + idc_puesto_nuevo.ToString() + ";";
                }
            }
            return cadena;
        }

        private int TotalCadena()
        {
            int cadena = 0;
            int idc_puesto_ori = Convert.ToInt32(ddlpuestosmasivo.SelectedValue);
            DataTable dt = (DataTable)Session["tabla_tareas_r"];
            foreach (DataRow row in dt.Rows)
            {
                int idc_tarea = Convert.ToInt32(row["idc_tarea"]);
                int idc_puesto = Convert.ToInt32(row["idc_puesto"]);
                int idc_puesto_asigna = Convert.ToInt32(row["idc_puesto_asigna"]);
                if (idc_puesto == idc_puesto_ori && idc_puesto_asigna != idc_puesto_ori)//solo puesto
                {
                    cadena = cadena + 1;
                }
                else if (idc_puesto != idc_puesto_ori && idc_puesto_asigna == idc_puesto_ori)//solo puesto asigna
                {
                    cadena = cadena + 1;
                }
                else if (idc_puesto == idc_puesto_ori && idc_puesto_asigna == idc_puesto_ori)//ambos
                {
                    cadena = cadena + 1;
                }
            }
            return cadena;
        }
    }
}