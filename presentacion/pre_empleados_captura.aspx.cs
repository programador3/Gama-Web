using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class pre_empleados_captura : System.Web.UI.Page
    {
        public string ruta = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            int idc_candidato = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"]));
            if (!Page.IsPostBack)
            {
                Session["HORARIO"] = null;
                Session["aplica_descanso_diario"] = null;
                //TABLA PARA HIJOS
                DataTable tabla_hijos = new DataTable();
                tabla_hijos.Columns.Add("nombre");
                tabla_hijos.Columns.Add("sexo");
                tabla_hijos.Columns.Add("fec_nac");
                Session["tabla_hijos"] = tabla_hijos;
                //TABLA PARA TELEFONOS
                DataTable tabla_telefonos = new DataTable();
                tabla_telefonos.Columns.Add("telefono");
                tabla_telefonos.Columns.Add("tipo");
                Session["tabla_telefonos"] = tabla_telefonos;
                //TABLA PARA ELECTOR
                DataTable elector = new DataTable();
                elector.Columns.Add("folio");
                elector.Columns.Add("vencimiento");
                elector.Columns.Add("rutafrente");
                elector.Columns.Add("rutaatras");
                elector.Columns.Add("extension");
                Session["elector"] = elector;
                //TABLA PARA licencia
                DataTable licencia = new DataTable();
                licencia.Columns.Add("num_licencia");
                licencia.Columns.Add("vencimiento");
                licencia.Columns.Add("idc_tipolicencia");
                licencia.Columns.Add("rutafrente");
                licencia.Columns.Add("rutaatras");

                licencia.Columns.Add("extension");
                Session["licencia"] = licencia;
                //tabla papeleria
                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("idc_tipodoc");
                papeleria.Columns.Add("nombre");
                papeleria.Columns.Add("idc_tipodocarc");
                papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["idc_tipodocarc"] };
                papeleria.Columns.Add("tipo");
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("extension");
                Session["papeleria"] = papeleria;

                //TABLA PARA HORARIOS
                //dias
                DataTable dias = new DataTable();
                dias.Columns.Add("dia");
                dias.Rows.Add("Lunes");
                dias.Rows.Add("Martes");
                dias.Rows.Add("Miercoles");
                dias.Rows.Add("Jueves");
                dias.Rows.Add("Viernes");
                dias.Rows.Add("Sabado");
                dias.Rows.Add("Domingo");
                Session["dias"] = dias;
                CargaInformacion(idc_candidato);

                //TABLA PARA FOTO
                //dias
                DataTable foto = new DataTable();
                foto.Columns.Add("nombre");
                foto.Columns.Add("ruta");
                foto.Columns.Add("extension");
                Session["foto"] = foto;
            }
        }

        /// <summary>
        /// Carga la informacion inicial de los controles
        /// </summary>
        /// <param name="idc_candidato"></param>
        public void CargaInformacion(int idc_candidato)
        {
            try
            {
                Pre_EmpleadosENT entidad = new Pre_EmpleadosENT();
                Pre_EmpleadosCOM componente = new Pre_EmpleadosCOM();
                entidad.Pidc_candidato = idc_candidato;
                DataSet ds = componente.CargaInformacion(entidad);

                //cargamos informacion

                //NOMBRE
                DataRow nombres = ds.Tables[0].Rows[0];
                txtPuesto.Text = nombres["descripcion"].ToString();
                lblidc_puesto.Text = nombres["idc_puesto"].ToString();
                Session["idc_puesto"] = nombres["idc_puesto"].ToString();
                Session["idc_prepara"] = nombres["idc_prepara"].ToString();
                Session["aplica_descanso_diario"] = Convert.ToBoolean(nombres["aplica_descanso_diario"]);
                int vehiculo = Convert.ToInt32(nombres["vehiculo"].ToString());
                if (vehiculo == 0 || Convert.ToInt32(nombres["idc_tipoveh"]) == 6) { panelLicencia.Visible = false; }
                Session["idc_curso"] = nombres["idc_curso"].ToString();

                DataTable elector = (DataTable)Session["elector"];
                DataTable licencia = (DataTable)Session["licencia"];
                //ESTADO CIVIL
                ddlEstadoCivil.DataTextField = "descripcion";
                ddlEstadoCivil.DataValueField = "idc_edocivil";
                ddlEstadoCivil.DataSource = ds.Tables[1];
                ddlEstadoCivil.DataBind();
                ddlEstadoCivil.Items.Insert(0, new ListItem("--Seleccione una opción", "0"));
                //PAIS
                ddlpais.DataTextField = "nombre";
                ddlpais.DataValueField = "idc_pais";
                ddlpais.DataSource = ds.Tables[2];
                ddlpais.DataBind();

                Session["tabla_estados"] = ds.Tables[3];
                //ESATDO
                DataRow pais = ds.Tables[2].Rows[0];
                string idc_pais = pais["idc_pais"].ToString();
                CargaEstado(idc_pais);

                //HORARIOS
                Session["tabla_horarios"] = ds.Tables[4];
                Session["tabla_horariosc"] = ds.Tables[5];

                DataTable dia = (DataTable)Session["dias"];
                repeatdias.DataSource = dia;
                repeatdias.DataBind();

                //LICENCIAS
                ddlTipoLic.DataTextField = "descripcion";
                ddlTipoLic.DataValueField = "idc_tipolicencia";
                ddlTipoLic.DataSource = ds.Tables[6];
                ddlTipoLic.DataBind();

                //papeleria
                repeatPapeleria.DataSource = ds.Tables[7];
                repeatPapeleria.DataBind();
                Session["documentos_detalles"] = ds.Tables[8];

                cbxhorarios.DataTextField = "descripcion";
                cbxhorarios.DataValueField = "idc_horariog";

                cbxhorarios.DataSource = ds.Tables[9];
                cbxhorarios.DataBind();

                Session["HORARIO"] = ds.Tables[9];

            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Carga Los estados del pais seleccionado
        /// </summary>
        /// <param name="idc_pais"></param>
        public void CargaEstado(string idc_pais)
        {
            try
            {
                //limpiamos combo
                ddlestado.Items.Clear();
                DataTable tabla_estados = (DataTable)Session["tabla_estados"];
                DataTable tabla_actual = new DataTable();
                tabla_actual.Columns.Add("idc_estado");
                tabla_actual.Columns.Add("nombre");
                DataRow[] result = tabla_estados.Select("idc_pais = '" + idc_pais + "'");
                //lleno lista con puestos
                foreach (DataRow row in result)
                {
                    DataRow new_row = tabla_actual.NewRow();
                    new_row["idc_estado"] = row["idc_estado"].ToString();
                    new_row["nombre"] = row["nombre"].ToString();
                    tabla_actual.Rows.Add(new_row);
                }
                //ESTADO
                ddlestado.DataTextField = "nombre";
                ddlestado.DataValueField = "idc_estado";
                ddlestado.DataSource = tabla_actual;
                ddlestado.DataBind();
                ddlestado.Items.Insert(0, new ListItem("--Seleccione una opción", "0"));
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Verifica si hay algun error, retorna TRUE si existe un error, retorn FALSE si todo es ok
        /// </summary>
        /// <returns></returns>
        public bool CompletePanelDPerso()
        {
            bool error = false;
            DataTable tabla_telefonos = (DataTable)Session["tabla_telefonos"];
            DataTable tabla_fotos = (DataTable)Session["foto"];
            if (tabla_fotos.Rows.Count == 0) { error = true; Alert.ShowAlertError("Debe Cargar la Foto del Pre Empleado", this); }
            if (tabla_telefonos.Rows.Count == 0) { error = true; Alert.ShowAlertError("Debe Ingresar al menos 1 numero telefonico", this); }
            if (txtFecNac.Text == string.Empty) { error = true; Alert.ShowAlertError("Seleccione la Fecha de Nacimiento", this); }
            if (ddlestado.SelectedValue == null) { error = true; Alert.ShowAlertError("Seleccione el Estado", this); }
            if (ddlpais.SelectedValue == null) { error = true; Alert.ShowAlertError("Seleccione el Pais", this); }
            if (ddlSexo.SelectedValue == "0") { error = true; Alert.ShowAlertError("Seleccione el Sexo", this); }
            if (ddlEstadoCivil.SelectedValue == null) { error = true; Alert.ShowAlertError("Seleccione el Estado Civil", this); }
            if (ddlEstadoCivil.SelectedValue != "2") { txtEsposo.ReadOnly = true; }
            lblCorreo.Visible = false;

            lblTelefono.Visible = false;
            if (TotalCadenaTelefonos() == 0) { lblTelefono.Visible = true; lblTelefono.Text = "DEBE INGRESAR AL MENOS UN TELEFONO"; }
            lblCalle.Visible = false;
            if (txtCalle.Text == string.Empty)
            {
                lblCalle.Text = "ESCRIBA UNA DIRECCION";
                lblCalle.Visible = true;
                error = true;
            }
            lblColonia.Visible = false;
            if (txtMunicipio.Text == string.Empty)
            {
                lblColonia.Text = "FILTRE Y SELECCIONE UNA COLONIA";
                lblColonia.Visible = true;
                error = true;
            }
            return error;
        }

        /// <summary>
        /// Verifica si hay algun error, retorna TRUE si existe un error, retorn FALSE si todo es ok
        /// </summary>
        /// <returns></returns>
        public bool CompletePanelDLab()
        {
            bool error = false;
            lblcurp.Visible = false;
            Regex pattern = new Regex("^([A-Z][A,E,I,O,U,X][A-Z]{2})(\\d{2})((01|03|05|07|08|10|12)(0[1-9]|[12]\\d|3[01])|02(0[1-9]|[12]\\d)|(04|06|09|11)(0[1-9]|[12]\\d|30))([M,H])(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)([B,C,D,F,G,H,J,K,L,M,N,Ñ,P,Q,R,S,T,V,W,X,Y,Z]{3})([0-9,A-Z][0-9])$");
            if (!pattern.IsMatch(TXTCURP.Text))
            {
                lblcurp.Text = "CURP NO VALIDO"; lblcurp.Visible = true; error = true;
            }
            lblRFC.Visible = false;
            string valid = "^(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))";
            if (TXTCURP.Text.Length != 12)
            {
                valid = "^(([A-Z]|[a-z]|\\s){1})(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))";
            }
            Regex pattern2 = new Regex(valid);
            if (!pattern2.IsMatch(txtRFC.Text))
            {
                lblRFC.Text = "RFC NO VALIDO"; lblRFC.Visible = true; error = true;
            }
            LBLIMSS.Visible = false;
            int numbers = txtIMSS.Text.Length;
            if (numbers < 11)
            {
                LBLIMSS.Text = "FALTAN " + (11 - txtIMSS.Text.Length).ToString() + " CARACTERES";
                LBLIMSS.Visible = true;
                error = true;
            }

            lblSueldo.Visible = false;
            if (txtSueldo.Text == string.Empty)
            {
                lblSueldo.Text = "SUELDO REQUERIDO";
                lblSueldo.Visible = true;
                error = true;
            }
            lblComplemento.Visible = false;
            if (txtComplementos.Text == string.Empty)
            {
                txtComplementos.Text = "0.00";
            }
            lblErrorHorarioHORARIO.Visible = false;
            DataTable dt = (DataTable)Session["HORARIO"];
            if (TotalCadenaHorarios() != 1 && dt.Rows.Count > 0)
            {
                lblErrorHorarioHORARIO.Visible = true;
                lblErrorHorarioHORARIO.Text = "SELECCIONE SOLO UN HORARIO";
                error = true;
            }
            //foreach (RepeaterItem item in repeatdias.Items)
            //{
            //    DropDownList ddlhorariodia = (DropDownList)item.FindControl("ddlhorariodia");
            //    DropDownList ddlhorariocomida = (DropDownList)item.FindControl("ddlhorariocomida");
            //    CheckBox cbxLaborables = (CheckBox)item.FindControl("cbxLaborables");
            //    Label lblErrorHorario = (Label)item.FindControl("lblErrorHorario");
            //    lblErrorHorario.Visible = false;
            //    if (ddlhorariocomida.SelectedValue == null || ddlhorariocomida.SelectedValue == "0") { lblErrorHorario.Text = "SELECCIONE UN HORARIO DE COMIDA"; error = true; }
            //    if (ddlhorariodia.SelectedValue == null || ddlhorariodia.SelectedValue == "0") { lblErrorHorario.Text = "SELECCIONE UN HORARIO"; error = true; }
            //    if (cbxLaborables.Checked == true) { TOTAL = TOTAL + 1; }
            //    if (TOTAL > 6) { error = true; lblErrorHorario.Text = "SOLO DEBEN SER 6 DIAS"; lblErrorHorario.Visible = true; }
            //}
            return error;
        }

        /// <summary>
        /// Verifica si hay algun error, retorna TRUE si existe un error, retorn FALSE si todo es ok
        /// </summary>
        /// <returns></returns>
        public bool CompletePanelDFam()
        {
            bool error = false;
            lblPadre.Visible = false;
            int numbers = txtPadre.Text.Length;
            if (numbers < 10)
            {
                lblPadre.Text = "DEBE SER EL NOMBRE COMPLETO"; lblPadre.Visible = true; error = true;
            }
            lblMadre.Visible = false;
            int numbersm = txtMadre.Text.Length;
            if (numbers < 10)
            {
                lblMadre.Text = "DEBE SER EL NOMBRE COMPLETO"; lblMadre.Visible = true; error = true;
            }
            if (ddlEstadoCivil.SelectedValue == "2")
            {
                lblEsposo.Visible = false;
                int numberse = txtPadre.Text.Length;
                if (numbers < 10)
                {
                    lblPadre.Text = "DEBE SER EL NOMBRE COMPLETO"; lblPadre.Visible = true; error = true;
                }
            }
            return error;
        }

        private String CadenaHorarios()
        {
            string cadena = "";
            foreach (ListItem item in cbxhorarios.Items)
            {
                if (item.Selected == true)
                {
                    cadena = cadena + item.Value + ";";
                }
            }
            return cadena;
        }

        private int TotalCadenaHorarios()
        {
            int cadena = 0;
            foreach (ListItem item in cbxhorarios.Items)
            {
                if (item.Selected == true)
                {
                    cadena = cadena + 1;
                }
            }
            return cadena;
        }

        /// <summary>
        /// Regresa una cadena con los nombres de los hijos
        /// </summary>
        /// <returns></returns>
        public string CadenaHijos()
        {
            string cadena = "";
            DataTable tabla_hijos = (DataTable)Session["tabla_hijos"];
            foreach (DataRow row in tabla_hijos.Rows)
            {
                DateTime myDateTime = Convert.ToDateTime(row["fec_nac"]);
                string sqlFormattedDate = myDateTime.ToString("yyyy-dd-MM HH:mm:ss");
                cadena = cadena + row["nombre"].ToString() + ";" + sqlFormattedDate + ";" + row["sexo"].ToString() + ";";
            }
            return cadena;
        }

        /// <summary>
        /// Retorna el total de filas de la cadena de hijos
        /// </summary>
        /// <returns></returns>
        public int TotalCadenaHijos()
        {
            DataTable tabla_hijos = (DataTable)Session["tabla_hijos"];
            return tabla_hijos.Rows.Count;
        }

        /// <summary>
        /// Regresa una cadena con los numeros de telefono
        /// </summary>
        /// <returns></returns>
        public string CadenaTelefonos()
        {
            string cadena = "";
            DataTable tabla_telefonos = (DataTable)Session["tabla_telefonos"];
            foreach (DataRow row in tabla_telefonos.Rows)
            {
                cadena = cadena + row["tipo"].ToString() + ";" + row["telefono"] + ";";
            }
            return cadena;
        }

        /// <summary>
        /// Retorna el total de filas de la cadena de telefonos
        /// </summary>
        /// <returns></returns>
        public int TotalCadenaTelefonos()
        {
            DataTable tabla_telefonos = (DataTable)Session["tabla_telefonos"];
            return tabla_telefonos.Rows.Count;
        }

        /// <summary>
        /// Verifica si hay algun error, retorna TRUE si existe un error, retorn FALSE si todo es ok
        /// </summary>
        /// <returns></returns>
        public bool CompletePanelDConc()
        {
            bool error = false;

            return error;
        }

        public string CadenaElectorLicencia()
        {
            string cadena = "";
            DataTable elector = (DataTable)Session["elector"];
            DataTable licencia = (DataTable)Session["licencia"];
            foreach (DataRow row in elector.Rows)
            {
                DateTime myDateTime = Convert.ToDateTime(row["vencimiento"]);
                string sqlFormattedDate = myDateTime.ToString("yyyy-dd-MM HH:mm:ss");
                cadena = cadena + row["folio"] + ";" + sqlFormattedDate + ";";
            }
            if (licencia.Rows.Count == 0)
            {
                cadena = cadena + null + ";" + null + ";" + null + ";";
            }
            foreach (DataRow row in licencia.Rows)
            {
                DateTime myDateTime = Convert.ToDateTime(row["vencimiento"]);
                string sqlFormattedDate = myDateTime.ToString("yyyy-dd-MM HH:mm:ss");
                cadena = cadena + row["num_licencia"] + ";" + sqlFormattedDate + ";" + row["idc_tipolicencia"].ToString() + ";";
            }
            return cadena;
        }

        /// <summary>
        /// Retorna total cadena LIC/ELECTOR (SIEMPRE DEBE SER UN SOLO REGISTRO)
        /// </summary>
        /// <returns></returns>
        public int TotalElectorLicencia()
        {
            int cadena = 0;
            DataTable elector = (DataTable)Session["elector"];
            DataTable licencia = (DataTable)Session["licencia"];
            foreach (DataRow row in elector.Rows)
            {
                DateTime myDateTime = Convert.ToDateTime(row["vencimiento"]);
                string sqlFormattedDate = myDateTime.ToString("yyyy-dd-MM HH:mm:ss");
                cadena = cadena + 1;
            }
            if (licencia.Rows.Count == 0)
            {
                cadena = cadena + 0;
            }
            foreach (DataRow row in licencia.Rows)
            {
                DateTime myDateTime = Convert.ToDateTime(row["vencimiento"]);
                string sqlFormattedDate = myDateTime.ToString("yyyy-dd-MM HH:mm:ss");
                cadena = cadena + 1;
            }
            return cadena;
        }

        /// <summary>
        /// Retorna una cadena de la tabla de documentos
        /// </summary>
        /// <returns></returns>
        public string CadenaPapeleria()
        {
            string cadena = "";
            DataTable papeleria = (DataTable)Session["papeleria"];

            foreach (DataRow row in papeleria.Rows)
            {
                if (!row["tipo"].ToString().Equals("elector") && !row["tipo"].ToString().Equals("licencia"))
                {
                    cadena = cadena + row["idc_tipodoc"].ToString() + ";";
                }
            }
            return cadena;
        }

        /// <summary>
        /// Retorna el total de registros de cadena de papeleria
        /// </summary>
        /// <returns></returns>
        public int TotalPapeleria()
        {
            int total = 0;
            DataTable papeleria = (DataTable)Session["papeleria"];
            foreach (DataRow row in papeleria.Rows)
            {
                if (!row["tipo"].ToString().Equals("elector") && !row["tipo"].ToString().Equals("licencia"))
                {
                    total = total + 1;
                }
            }
            return total;
        }

        /// <summary>
        /// Retorna Cadena con detalles de pepaleria
        /// </summary>
        /// <returns></returns>
        public string CadenaPapeleriaDet()
        {
            string cadena = "";
            DataTable papeleria = (DataTable)Session["papeleria"];
            DataTable documentos_detalles = (DataTable)Session["documentos_detalles"];
            foreach (DataRow row in papeleria.Rows)
            {
                foreach (DataRow row_d in documentos_detalles.Rows)
                {
                    string pap = row["idc_tipodocarc"].ToString();
                    string pap_det = row_d["idc_tipodocarc"].ToString();
                    if (pap == pap_det)
                    {
                        cadena = cadena + row_d["idc_tipodoc"].ToString() + ";" + row_d["idc_tipodocarc"].ToString() + ";";
                    }
                }
            }
            return cadena;
        }

        /// <summary>
        /// Retorna el total de cadena detalles de papeleria
        /// </summary>
        /// <returns></returns>
        public int TotalPapeleriaDet()
        {
            int total = 0;
            DataTable papeleria = (DataTable)Session["papeleria"];
            DataTable documentos_detalles = (DataTable)Session["documentos_detalles"];
            foreach (DataRow row in papeleria.Rows)
            {
                foreach (DataRow row_d in documentos_detalles.Rows)
                {
                    if (row["idc_tipodocarc"].ToString() == row_d["idc_tipodocarc"].ToString())
                    {
                        total = total + 1;
                    }
                }
            }
            return total;
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        protected void ddlpais_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaEstado(ddlpais.SelectedValue.ToString());
        }

        protected void lnkSiguientePDatosP_Click(object sender, EventArgs e)
        {
            if (CompletePanelDPerso() != true)//cambiar
            {
                Panel_Personal.Visible = false;
                PanelDatosLaborales.Visible = true;
            }
        }

        protected void lnkAnteriorPDatosL_Click(object sender, EventArgs e)
        {
            Panel_Personal.Visible = true;
            PanelDatosLaborales.Visible = false;
        }

        protected void lnkSiguientePDatosL_Click(object sender, EventArgs e)
        {
            if (CompletePanelDLab() != true)
            {
                PanelDatosLaborales.Visible = false;
                PanelDFamiliares.Visible = true;
            }
        }

        protected void lnkReturnDFam_Click(object sender, EventArgs e)
        {
            PanelDatosLaborales.Visible = true;
            PanelDFamiliares.Visible = false;
        }

        protected void lnkAdlenateDFAM_Click(object sender, EventArgs e)
        {
            if (CompletePanelDFam() != true)
            {
                PanelDConact.Visible = true;
                PanelDFamiliares.Visible = false;
            }
        }

        protected void lnkReturnDConc_Click(object sender, EventArgs e)
        {
            PanelDConact.Visible = false;
            PanelDFamiliares.Visible = true;
        }

        protected void lnkAdelanteDConc_Click(object sender, EventArgs e)
        {
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                resultados.Visible = false;
                if (txtFiltroColonias.Text != string.Empty && txtFiltroColonias.Text.Length > 3)
                {
                    DataTable table_colonias = new DataTable();
                    table_colonias.Columns.Add("idc_colonia");
                    table_colonias.Columns.Add("colonia");
                    Pre_EmpleadosENT entidad = new Pre_EmpleadosENT();
                    Pre_EmpleadosCOM comp = new Pre_EmpleadosCOM();
                    entidad.Pvalor = txtFiltroColonias.Text;
                    DataSet ds = comp.CargaInformacionColonia(entidad);
                    if (ds.Tables[0].Rows.Count == 0) { resultados.Visible = true; ddlColonia.Visible = false; }
                    else
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            DataRow new_row = table_colonias.NewRow();
                            new_row["idc_colonia"] = row["idc_colonia"];
                            new_row["colonia"] = row["nombre"].ToString() + ", #" + row["cod_postal"].ToString() + ", " + row["mpio"] + " ," + row["edo"] + " ," + row["pais"];

                            table_colonias.Rows.Add(new_row);
                        }
                        ddlColonia.Visible = true;
                        DataRow row_text = ds.Tables[0].Rows[0];
                        txtMunicipio.Text = row_text["nombre"].ToString() + ", #" + row_text["cod_postal"].ToString() + " ," + row_text["mpio"].ToString() + " ," + row_text["edo"].ToString() + " ," + row_text["pais"].ToString();
                        lblColonia.Visible = false;
                    }
                    ddlColonia.Items.Clear();
                    ddlColonia.DataTextField = "colonia";
                    ddlColonia.DataValueField = "idc_colonia";
                    ddlColonia.DataSource = table_colonias;
                    ddlColonia.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void ddlColonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMunicipio.Text = ddlColonia.SelectedItem.ToString();
            lblColonia.Visible = false;
        }

        protected void TXTCURP_TextChanged(object sender, EventArgs e)
        {
            TXTCURP.Text = TXTCURP.Text.ToUpper();
            lblcurp.Visible = false;
            Regex pattern = new Regex("^([A-Z][A,E,I,O,U,X][A-Z]{2})(\\d{2})((01|03|05|07|08|10|12)(0[1-9]|[12]\\d|3[01])|02(0[1-9]|[12]\\d)|(04|06|09|11)(0[1-9]|[12]\\d|30))([M,H])(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)([B,C,D,F,G,H,J,K,L,M,N,Ñ,P,Q,R,S,T,V,W,X,Y,Z]{3})([0-9,A-Z][0-9])$");
            if (!pattern.IsMatch(TXTCURP.Text))
            {
                lblcurp.Text = "CURP NO VALIDO"; lblcurp.Visible = true;
            }
        }

        protected void txtRFC_TextChanged(object sender, EventArgs e)
        {
            txtRFC.Text = txtRFC.Text.ToUpper();
            lblRFC.Visible = false;
            string valid = "^(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))";
            if (TXTCURP.Text.Length != 12)
            {
                valid = "^(([A-Z]|[a-z]|\\s){1})(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))";
            }
            Regex pattern = new Regex(valid);
            if (!pattern.IsMatch(txtRFC.Text))
            {
                lblRFC.Text = "RFC NO VALIDO"; lblRFC.Visible = true;
            }
        }

        protected void txtIMSS_TextChanged(object sender, EventArgs e)
        {
            LBLIMSS.Visible = false;
            int numbers = txtIMSS.Text.Length;
            if (numbers < 11)
            {
                LBLIMSS.Text = "FALTAN " + (11 - txtIMSS.Text.Length).ToString() + " CARACTERES";
                LBLIMSS.Visible = true;
            }
        }

        protected void txtCalle_TextChanged(object sender, EventArgs e)
        {
            if (txtCalle.Text != string.Empty) { lblCalle.Visible = false; }
        }

        protected void txtEsposo_TextChanged(object sender, EventArgs e)
        {
            if (ddlEstadoCivil.SelectedValue == "2")
            {
                lblEsposo.Visible = false;
                int numbers = txtEsposo.Text.Length;
                if (numbers < 10)
                {
                    lblEsposo.Text = "DEBE SER EL NOMBRE COMPLETO";
                    lblEsposo.Visible = true;
                }
            }
        }

        protected void txtMadre_TextChanged(object sender, EventArgs e)
        {
            lblMadre.Visible = false;
            int numbers = txtMadre.Text.Length;
            if (numbers < 10)
            {
                lblMadre.Text = "DEBE SER EL NOMBRE COMPLETO";
                lblMadre.Visible = true;
            }
        }

        protected void txtPadre_TextChanged(object sender, EventArgs e)
        {
            lblPadre.Visible = false;
            int numbers = txtPadre.Text.Length;
            if (numbers < 10)
            {
                lblPadre.Text = "DEBE SER EL NOMBRE COMPLETO";
                lblPadre.Visible = true;
            }
        }

        protected void imgAddHijos_Click(object sender, ImageClickEventArgs e)
        {
            bool error = false;
            DataTable tabla_hijos = (DataTable)Session["tabla_hijos"];
            lblHijos.Visible = false;
            int numbers = txtHijos.Text.Length;
            if (txtHijos.Text == string.Empty || numbers < 10)
            {
                lblHijos.Text = "ESCRIBA EL NOMBRE COMPLETO";
                lblHijos.Visible = true;
                error = true;
            }
            lblSexoHijos.Visible = false;
            if (ddlSexoHijos.SelectedValue == "0") { lblSexoHijos.Visible = true; lblSexoHijos.Text = "SELECCIONE UN GENERO"; error = true; }
            lblFcehaNacHijos.Visible = false;
            if (txtFechaNacHijos.Text == string.Empty) { lblFcehaNacHijos.Visible = true; lblFcehaNacHijos.Text = "SELECCIONE UNA FECHA"; error = true; }
            if (error != true)
            {
                DataRow[] row_select = tabla_hijos.Select("nombre = '" + txtHijos.Text.ToUpper() + "'");
                if (row_select.Length == 0)
                {
                    DataRow row = tabla_hijos.NewRow();
                    row["nombre"] = txtHijos.Text.ToUpper();
                    row["sexo"] = ddlSexoHijos.SelectedValue.ToUpper();
                    SqlDateTime fecha = SqlDateTime.Parse(txtFechaNacHijos.Text.ToUpper());
                    row["fec_nac"] = fecha;
                    tabla_hijos.Rows.Add(row);
                    gridHijos.DataSource = tabla_hijos;
                    gridHijos.DataBind();
                    txtHijos.Text = string.Empty;
                    Session["tabla_hijos"] = tabla_hijos;
                }
                else
                {
                    lblHijos.Text = "YA EXISTE ESTE NOMBRE";
                    lblHijos.Visible = true;
                }
            }
        }

        protected void gridHijos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string nombre = gridHijos.DataKeys[index].Values["nombre"].ToString();

            DataTable tabla_hijos = (DataTable)Session["tabla_hijos"];
            foreach (DataRow row in tabla_hijos.Rows)
            {
                if (row["nombre"].ToString().Equals(nombre)) { row.Delete(); break; }
            }
            Session["tabla_hijos"] = tabla_hijos;
            gridHijos.DataSource = tabla_hijos;
            gridHijos.DataBind();
        }

        protected void txtSueldo_TextChanged(object sender, EventArgs e)
        {
            lblSueldo.Visible = false;
            if (txtSueldo.Text == string.Empty) { lblSueldo.Visible = true; lblSueldo.Text = "SUELDO REQUERIDO"; }
        }

        protected void txtComplementos_TextChanged(object sender, EventArgs e)
        {
            lblComplemento.Visible = false;
            if (txtComplementos.Text == string.Empty) { txtComplementos.Text = "0.00"; }
        }

        protected void txtCorreo_TextChanged(object sender, EventArgs e)
        {
            lblCorreo.Visible = false;
            if (txtCorreo.Text == string.Empty) { lblCorreo.Visible = true; lblCorreo.Text = "CORREO REQUERIDO"; }
            Regex pattern = new Regex("[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}");
            if (!pattern.IsMatch(txtCorreo.Text))
            {
                lblCorreo.Text = "DEBE SER UN CORREO VALIDO. DEBE CONTENER UN @"; lblCorreo.Visible = true;
            }
        }

        protected void imgAddTelefono_Click(object sender, ImageClickEventArgs e)
        {
            DataTable tabla_telefonos = (DataTable)Session["tabla_telefonos"];
            lblTelefono.Visible = false;
            int numbers = txtTelefono.Text.Length;
            if (txtTelefono.Text == string.Empty || numbers < 8)
            {
                lblTelefono.Text = "ESCRIBA EL TELEFONO A 8 o 10 DIGITOS";
                lblTelefono.Visible = true;
            }
            else
            {
                DataRow[] row_select = tabla_telefonos.Select("telefono = '" + txtTelefono.Text.ToUpper() + "'");
                if (row_select.Length == 0)
                {
                    DataRow row = tabla_telefonos.NewRow();
                    row["telefono"] = txtTelefono.Text.ToUpper();
                    row["tipo"] = ddlTipoTelefono.SelectedValue.ToUpper();
                    tabla_telefonos.Rows.Add(row);
                    gridTelefonos.DataSource = tabla_telefonos;
                    gridTelefonos.DataBind();
                    txtTelefono.Text = string.Empty;
                    Session["tabla_telefonos"] = tabla_telefonos;
                }
                else
                {
                    lblTelefono.Text = "YA EXISTE ESTE TELEFONO";
                    lblTelefono.Visible = true;
                }
            }
        }

        protected void gridTelefonos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string telefono = gridTelefonos.DataKeys[index].Values["telefono"].ToString();

            DataTable tabla_telefonos = (DataTable)Session["tabla_telefonos"];
            foreach (DataRow row in tabla_telefonos.Rows)
            {
                if (row["telefono"].ToString().Equals(telefono)) { row.Delete(); break; }
            }
            Session["tabla_telefonos"] = tabla_telefonos;
            gridTelefonos.DataSource = tabla_telefonos;
            gridTelefonos.DataBind();
        }

        protected void ddlLunes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dia = (DataTable)Session["tabla_horarios"];
            int diaksks = dia.Rows.Count;
            foreach (RepeaterItem item in repeatdias.Items)
            {
                DropDownList ddlhorariodia = (DropDownList)item.FindControl("ddlhorariodia");
                DropDownList ddlhorariocomida = (DropDownList)item.FindControl("ddlhorariocomida");
                Label lblNoComida = (Label)item.FindControl("lblNoComida");
                //BUSCAMOS QUE APLIQUE COMIDA
                int diam = dia.Rows.Count;
                //INICAMOS CONTROLES
                ddlhorariocomida.Visible = false;
                lblNoComida.Visible = true;
                foreach (DataRow row in dia.Rows)
                {
                    if (ddlhorariodia.SelectedValue == row["idc_horario"].ToString())
                    {
                        if (Convert.ToBoolean(row["aplica_comida"]) == true)
                        {
                            ddlhorariocomida.Visible = true;
                            lblNoComida.Visible = false;
                        }
                    }
                }
            }
        }

        protected void repeatdias_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string dia_name = Convert.ToString(DataBinder.Eval(dbr, "dia"));
            DropDownList ddlhorariodia = (DropDownList)e.Item.FindControl("ddlhorariodia");
            DropDownList ddlhorariocomida = (DropDownList)e.Item.FindControl("ddlhorariocomida");
            CheckBox cbxLaborables = (CheckBox)e.Item.FindControl("cbxLaborables");
            CheckBox cbxDescanso = (CheckBox)e.Item.FindControl("cbxDescanso");
            DataTable dia = (DataTable)Session["tabla_horarios"];
            DataTable comida = (DataTable)Session["tabla_horariosc"];
            ddlhorariodia.DataTextField = "horario";
            ddlhorariodia.DataValueField = "idc_horario";
            ddlhorariodia.DataSource = dia;
            ddlhorariodia.DataBind();
            ddlhorariocomida.DataTextField = "horario";
            ddlhorariocomida.DataValueField = "idc_horarioc";
            ddlhorariocomida.DataSource = comida;
            ddlhorariocomida.DataBind();
            bool descanso = Convert.ToBoolean(Session["aplica_descanso_diario"]);
            cbxDescanso.Enabled = descanso;

            if (dia_name == "Domingo") { ddlhorariodia.Visible = false; cbxLaborables.Checked = false; }
        }

        protected void ddlSexoHijos_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSexoHijos.Visible = false;
            if (ddlSexoHijos.SelectedValue == "0") { lblSexoHijos.Text = "SELECCIONE UN GENERO"; lblSexoHijos.Visible = true; }
        }

        protected void txtFechaNacHijos_TextChanged(object sender, EventArgs e)
        {
            lblFcehaNacHijos.Visible = false;
            if (txtFechaNacHijos.Text == string.Empty) { lblFcehaNacHijos.Text = "SELECCIONE UNA FECHA"; lblFcehaNacHijos.Visible = true; }
        }

        protected void ddlTipoTelefono_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTipoCel.Visible = false;
        }

        /// <summary>
        /// Sube archivos a ruta, retorna en forma de Bool si hubo un error
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public bool UploadFile(FileUpload FileUPL, String ruta)
        {
            try
            {
                FileUPL.PostedFile.SaveAs(ruta);
                FileUPL.Visible = false;
                return false;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
                return true;
            }
        }

        /// <summary>
        /// Elimina todos los archivos que contenga un directorio en especifico
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public bool DeletFiles(string dir)
        {
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(@dir);
                foreach (string ficheroActual in ficherosCarpeta)
                    File.Delete(ficheroActual);
                return true;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
                return false;
            }
        }

        protected void lnkAgregarFotoPerfil_Click(object sender, EventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/pre_alta/personal/"));//path local
            if (fupFotoPerfil.HasFile)
            {
                if (UploadFile(fupFotoPerfil, dirInfo + fupFotoPerfil.FileName) == false)
                {
                    DataTable foto = (DataTable)Session["foto"];
                    DataRow row = foto.NewRow();
                    row["nombre"] = fupFotoPerfil.FileName;
                    row["ruta"] = dirInfo + fupFotoPerfil.FileName;
                    row["extension"] = Path.GetExtension(fupFotoPerfil.FileName);
                    foto.Rows.Add(row);
                    Alert.ShowGift("Estamos subiendo la foto al servidor.", "Espere un Momento", "imagenes/loading.gif", "3000", "Foto Subida Correctamente", this);
                    partupload.Visible = false;
                    REV.Enabled = false;
                    imgdeletefoto.Visible = true;
                    btnVer.Visible = true;
                }
            }
        }

        /// <summary>
        /// Copia un archivo de una ruta especifica a otra, si todo fue correcto devuelve un TRUE
        /// </summary>
        /// <param name="sourcefilename"></param>
        /// <param name="destfilename"></param>
        /// <returns></returns>
        public bool CopiarArchivos(string sourcefilename, string destfilename)
        {
            try
            {
                if (File.Exists(sourcefilename))
                {
                    File.Copy(sourcefilename, destfilename, true);
                    return true;
                }
                else
                {
                    Alert.ShowAlertError(".", this);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
                return false;
            }
        }

        protected void txtFechaVencElect_TextChanged(object sender, EventArgs e)
        {
            lblFecVenElect.Visible = false;
        }

        protected void txtFolioElector_TextChanged(object sender, EventArgs e)
        {
            lblFolioEelec.Visible = false;
        }

        protected void lnkElector_Click(object sender, EventArgs e)
        {
            bool error = false;
            lnkElector.Visible = false;
            lblFolioEelec.Visible = false;
            if (fupFrenteElector.Visible == true && fupAtrasElector.Visible == true && txtFolioElector.Text == "") { lblFolioEelec.Visible = true; lblFolioEelec.Text = "FOLIO DE ELECTOR REQUERIDO"; error = true; }
            lblFecVenElect.Visible = false;
            int numberanio = txtFechaVencElect.Text.Length;
            if (fupFrenteElector.Visible == true && fupAtrasElector.Visible == true && txtFechaVencElect.Text == "") { lblFecVenElect.Visible = true; lblFecVenElect.Text = "AÑO VENCIMIENTO ELECTOR REQUERIDO"; error = true; }
            if (fupFrenteElector.Visible == true && fupAtrasElector.Visible == true && numberanio < 4) { lblFecVenElect.Visible = true; lblFecVenElect.Text = "AÑO VENCIMIENTO DEBE SER DE 4 DIGITOS"; error = true; }
            if (error == false && fupAtrasElector.HasFile && fupFrenteElector.HasFile)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/pre_alta/documentos/elector/adelante/"));//path local
                DirectoryInfo dirInfoatras = new DirectoryInfo(Server.MapPath("~/temp/pre_alta/documentos/elector/atras/"));//path local
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                bool frente = UploadFile(fupFrenteElector, dirInfo + randomNumber.ToString() + fupFrenteElector.FileName);
                bool atras = UploadFile(fupAtrasElector, dirInfoatras + randomNumber.ToString() + fupAtrasElector.FileName);
                if (frente == false && atras == false)
                {
                    //agregamos papeleria a tabla temporal
                    DataTable elector = (DataTable)Session["elector"];
                    DataRow new_row = elector.NewRow();
                    new_row["folio"] = txtFolioElector.Text.ToUpper();

                    SqlDateTime fec_ven = SqlDateTime.Parse(txtFechaVencElect.Text);
                    new_row["vencimiento"] = fec_ven;
                    new_row["rutafrente"] = dirInfo + randomNumber.ToString() + fupFrenteElector.FileName;
                    new_row["rutaatras"] = dirInfoatras + randomNumber.ToString() + fupAtrasElector.FileName;
                    new_row["EXTENSION"] = Path.GetExtension(fupAtrasElector.FileName).ToString();
                    elector.Rows.Add(new_row);
                    //agregamos a tabla global de papelera
                    string mensaje = AddPapeleriaToTable("100", "1elec", "elector", dirInfo + fupFrenteElector.FileName, fupFrenteElector.FileName, Path.GetExtension(fupFrenteElector.FileName).ToString());
                    string mensaje2 = AddPapeleriaToTable("101", "2elect", "elector", dirInfo + fupAtrasElector.FileName, fupAtrasElector.FileName, Path.GetExtension(fupAtrasElector.FileName).ToString());
                    if (mensaje != "") { Alert.ShowAlertError(mensaje, this); }
                    if (mensaje2 != "") { Alert.ShowAlertError(mensaje2, this); }
                    Session["elector"] = elector;
                    int ele = elector.Rows.Count;
                    ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + gridPapeleria.ClientID.ToString() + "');", true);
                    Alert.ShowGift("Estamos subiendo los archivo al servidor.", "Espere un Momento", "imagenes/loading.gif", "3000", "Archivos Subidos Correctamente", this);

                    txtFolioElector.ReadOnly = true;
                    txtFechaVencElect.ReadOnly = true;
                    revFrenteElect.Enabled = false; revAtrasElect.Enabled = false;
                }
            }
        }

        protected void lnkGuardarLic_Click(object sender, EventArgs e)
        {
            bool error = false;
            lblNumLic.Visible = false;
            lnkGuardarLic.Visible = false;
            if (fupFrenteLic.Visible == true && fupAtrasLic.Visible == true && txtNumLicencia.Text == "") { lblNumLic.Visible = true; lblNumLic.Text = "NUMERO DE LICENCIA REQUERIDO"; error = true; }
            lblfecvenlic.Visible = false;
            if (fupFrenteLic.Visible == true && fupAtrasLic.Visible == true && txtFecVenLic.Text == "") { lblfecvenlic.Visible = true; lblfecvenlic.Text = "FECHA DE VENCIMIENTO REQUERIDO"; error = true; }
            if (error == false && fupFrenteLic.HasFile && fupAtrasLic.HasFile)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000);
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/pre_alta/documentos/licencia/adelante/"));//path local
                DirectoryInfo dirInfoatras = new DirectoryInfo(Server.MapPath("~/temp/pre_alta/documentos/licencia/atras/"));//path local
                bool frente = UploadFile(fupFrenteLic, dirInfo + randomNumber.ToString() + fupFrenteLic.FileName);
                bool atras = UploadFile(fupAtrasLic, dirInfoatras + randomNumber.ToString() + fupAtrasLic.FileName);
                if (frente == false && atras == false)
                {
                    //agregamos papeleria a tabla temporal
                    DataTable licencia = (DataTable)Session["licencia"];
                    DataRow new_row = licencia.NewRow();
                    new_row["num_licencia"] = txtNumLicencia.Text.ToUpper();
                    SqlDateTime fec_ven = SqlDateTime.Parse(txtFecVenLic.Text);
                    new_row["vencimiento"] = fec_ven;
                    new_row["idc_tipolicencia"] = ddlTipoLic.SelectedValue;
                    new_row["rutafrente"] = dirInfo + randomNumber.ToString() + fupFrenteLic.FileName;
                    new_row["rutaatras"] = dirInfoatras + randomNumber.ToString() + fupAtrasLic.FileName;
                    new_row["extension"] = Path.GetExtension(fupAtrasLic.FileName).ToString();
                    licencia.Rows.Add(new_row);
                    //agregamos a tabla global de papelera
                    string mensaje = AddPapeleriaToTable("200", "1lic", "licencia", dirInfo + fupFrenteLic.FileName, fupFrenteLic.FileName, Path.GetExtension(fupFrenteLic.FileName).ToString());
                    string mensaje2 = AddPapeleriaToTable("201", "2lic", "licencia", dirInfo + fupAtrasLic.FileName, fupAtrasLic.FileName, Path.GetExtension(fupAtrasLic.FileName).ToString());
                    if (mensaje != "") { Alert.ShowAlertError(mensaje, this); }
                    if (mensaje2 != "") { Alert.ShowAlertError(mensaje2, this); }
                    Session["licencia"] = licencia;
                    ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + gridPapeleria.ClientID.ToString() + "');", true);
                    revFrenteLic.Enabled = false;
                    revAtrasLic.Enabled = false;
                    Alert.ShowGift("Estamos subiendo los archivos al servidor.", "Espere un Momento", "imagenes/loading.gif", "3000", "Archivos Subidos Correctamente", this);

                    txtNumLicencia.ReadOnly = true;
                    txtFecVenLic.ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// Agrega filas tabla de papeleria global (INCLUYE ELECTOR Y LICENCIA)
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="nombre"></param>
        public string AddPapeleriaToTable(string idc_tipodocarc, string idc_tipodoc, string tipo, string ruta, string nombre, string extension)
        {
            string mensaje = "";
            bool exists = false;
            DataTable papeleria = (DataTable)Session["papeleria"];
            foreach (DataRow check in papeleria.Rows)
            {
                if (check["idc_tipodocarc"].Equals(idc_tipodocarc))
                {
                    exists = true;
                    mensaje = check["tipo"].ToString() + " existente. Elimine el anterior si desea actualizarlo.";
                    break;
                }
            }
            if (exists == false)
            {
                DataRow new_row = papeleria.NewRow();
                new_row["idc_tipodoc"] = idc_tipodoc;
                new_row["idc_tipodocarc"] = idc_tipodocarc;
                new_row["tipo"] = tipo;
                new_row["nombre"] = nombre;
                new_row["ruta"] = ruta;
                new_row["extension"] = extension;
                papeleria.Rows.Add(new_row);
                gridPapeleria.DataSource = papeleria;
                gridPapeleria.DataBind();
                Session["papeleria"] = papeleria;
            }
            return mensaje;
        }

        /// <summary>
        /// Elimina filas de tabla ELECTOR o LICENCIA segun el tipo
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="ruta"></param>
        public void DeleteElectorandLic(string tipo)
        {
            DataTable elector = (DataTable)Session["elector"];
            DataTable licencia = (DataTable)Session["licencia"];
            switch (tipo)
            {
                case "elector":
                    elector.Rows.Clear();
                    revFrenteElect.Enabled = true; revAtrasElect.Enabled = true;
                    PanelElector.Visible = true;
                    Session["elector"] = elector;
                    break;

                case "licencia":
                    licencia.Rows.Clear();
                    revFrenteLic.Enabled = false; revAtrasLic.Enabled = false;
                    panelLicencia.Visible = true;
                    Session["licencia"] = licencia;
                    break;
            }
        }

        protected void txtNumLicencia_TextChanged(object sender, EventArgs e)
        {
            lblNumLic.Visible = false;
        }

        protected void txtFecVenLic_TextChanged(object sender, EventArgs e)
        {
            lblfecvenlic.Visible = false;
        }

        protected void gridPapeleria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridPapeleria.DataKeys[index].Values["ruta"].ToString();
            string tipo = gridPapeleria.DataKeys[index].Values["tipo"].ToString();
            string nombre = gridPapeleria.DataKeys[index].Values["nombre"].ToString();
            string extension = gridPapeleria.DataKeys[index].Values["extension"].ToString();
            string idc_tipodoc = gridPapeleria.DataKeys[index].Values["idc_tipodoc"].ToString();
            string idc_tipodocarc = gridPapeleria.DataKeys[index].Values["idc_tipodocarc"].ToString();
            DataTable papeleria = (DataTable)Session["papeleria"];
            switch (e.CommandName)
            {
                case "Eliminar":
                    List<DataRow> rowsToDelete = new List<DataRow>();
                    foreach (DataRow row in papeleria.Rows)
                    {
                        if (tipo.Equals("elector"))
                        {
                            if (row["tipo"].ToString().Equals("elector"))
                            {
                                txtFolioElector.ReadOnly = false;
                                txtFechaVencElect.ReadOnly = false;
                                lnkElector.Visible = true;
                                rowsToDelete.Add(row); revFrenteElect.Enabled = true; revAtrasElect.Enabled = true; fupAtrasElector.Visible = true; fupFrenteElector.Visible = true;
                            }
                        }
                        if (tipo.Equals("licencia"))
                        {
                            if (row["tipo"].ToString().Equals("licencia"))
                            {
                                txtNumLicencia.ReadOnly = false;
                                txtFecVenLic.ReadOnly = false;
                                lnkGuardarLic.Visible = true;
                                rowsToDelete.Add(row); revFrenteLic.Enabled = true; revAtrasLic.Enabled = true; fupAtrasLic.Visible = true; fupFrenteLic.Visible = true;
                            }
                        }
                        if (!tipo.Equals("elector") && !tipo.Equals("licencia"))
                        {
                            if (row["ruta"].ToString().Equals(ruta) && row["idc_tipodocarc"].ToString().Equals(idc_tipodocarc))
                            {
                                row.Delete();
                                break;
                            }
                        }
                    }

                    foreach (DataRow rowde in rowsToDelete)
                    {
                        papeleria.Rows.Remove(rowde);
                    }
                    //eliminamos de otras tablas
                    DeleteElectorandLic(tipo);
                    Session["papeleria"] = papeleria;
                    gridPapeleria.DataSource = papeleria;
                    gridPapeleria.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + gridPapeleria.ClientID.ToString() + "');", true);

                    break;

                case "Descargar":
                    Download(ruta, nombre);
                    break;
            }
        }

        /// <summary>
        /// Descarga un archivo
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file_name"></param>
        public void Download(string path, string file_name)
        {
            // Limpiamos la salida
            Response.Clear();
            // Con esto le decimos al browser que la salida sera descargable
            Response.ContentType = "application/octet-stream";
            // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file_name);
            // Escribimos el fichero a enviar
            Response.WriteFile(path);
            // volcamos el stream
            Response.Flush();
            // Enviamos todo el encabezado ahora
            Response.End();
        }

        protected void repeatPapeleria_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lnkGuardarPape = (LinkButton)e.Item.FindControl("lnkGuardarPape");
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            RegularExpressionValidator revpapeleria = (RegularExpressionValidator)e.Item.FindControl("revpapeleria");
            string idc_tipodoc = Convert.ToString(DataBinder.Eval(dbr, "idc_tipodoc"));
            string idc_tipodocarc = Convert.ToString(DataBinder.Eval(dbr, "idc_tipodocarc"));
            revpapeleria.ValidationExpression = Convert.ToString(DataBinder.Eval(dbr, "exp_regular"));
            lnkGuardarPape.CommandName = idc_tipodoc;
            lnkGuardarPape.CommandArgument = idc_tipodocarc;
        }

        protected void lnkGuardarPape_Click(object sender, EventArgs e)
        {
            LinkButton lnkGuardarPapeSender = (LinkButton)sender;
            foreach (RepeaterItem item in repeatPapeleria.Items)
            {
                LinkButton lnkGuardarPape = (LinkButton)item.FindControl("lnkGuardarPape");
                Panel PanelPApeleriaIndv = (Panel)item.FindControl("PanelPApeleriaIndv");
                Label lblDescr = (Label)item.FindControl("lblDescr");
                Label lblerrorpapedinamico = (Label)item.FindControl("lblerrorpapedinamico");
                RegularExpressionValidator revpapeleria = (RegularExpressionValidator)item.FindControl("revpapeleria");
                if (lnkGuardarPape == lnkGuardarPapeSender)
                {
                    FileUpload fupPapeleria = (FileUpload)item.FindControl("fupPapeleria");
                    if (fupPapeleria.HasFile)
                    {
                        Random random = new Random();
                        int randomNumber = random.Next(0, 100000);
                        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/pre_alta/documentos/papeleria/"));//path local
                        string mensaje = AddPapeleriaToTable(lnkGuardarPape.CommandArgument.ToString(), lnkGuardarPape.CommandName.ToString(), lblDescr.Text.ToString(), dirInfo + randomNumber.ToString() + fupPapeleria.FileName, randomNumber.ToString() + fupPapeleria.FileName, Path.GetExtension(fupPapeleria.FileName).ToString());
                        if (mensaje.Equals(string.Empty))
                        {
                            bool pape = UploadFile(fupPapeleria, dirInfo + randomNumber.ToString() + fupPapeleria.FileName);

                            if (pape == false)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + lnkGuardarPapeSender.ClientID.ToString() + "');", true);
                                Alert.ShowGift("Estamos subiendo el archivo al servidor.", "Espere un Momento", "imagenes/loading.gif", "3000", "Archivo Subido Correctamente", this);

                                //agregamos a tabla global de papelera
                            }
                        }
                        else
                        {
                            Alert.ShowAlertError(mensaje, this);
                        }
                        fupPapeleria.Visible = true;
                        revpapeleria.Enabled = false;
                        lblerrorpapedinamico.Visible = false;
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Alert.ShowAlertError(CadenaPapeleriaDet() + "  t:" + TotalPapeleriaDet().ToString(), this);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Alert.ShowAlertError(CadenaPapeleria() + "  t:" + TotalPapeleria().ToString(), this);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Alert.ShowAlertError(CadenaElectorLicencia() + "  t:" + TotalElectorLicencia().ToString(), this);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool error = false;
            DataTable elector = (DataTable)Session["elector"];
            DataTable licencia = (DataTable)Session["licencia"];
            DataTable papeleria = (DataTable)Session["documentos_detalles"];
            DataTable papeleria_det = (DataTable)Session["papeleria"];
            foreach (RepeaterItem Item in repeatPapeleria.Items)
            {
                Label lblerrorpapedinamico = (Label)Item.FindControl("lblerrorpapedinamico");
                Label lblidc_tipodocarc = (Label)Item.FindControl("lblidc_tipodocarc");
                int idc_tipodocarc = Convert.ToInt32(lblidc_tipodocarc.Text);
                lblerrorpapedinamico.Visible = false;
                if (!papeleria_det.Rows.Contains(idc_tipodocarc))
                {
                    error = true; lblerrorpapedinamico.Visible = true; lblerrorpapedinamico.Text = "PAPELERIA OBLIGATORIA";
                }
            }
            if (elector.Rows.Count == 0) { error = true; Alert.ShowAlertError("La Credencial de Elector es obligatoria", this); }
            //solo si aplica licnecia
            if (panelLicencia.Visible == true) { if (licencia.Rows.Count == 0) { error = true; Alert.ShowAlertError("La Licencia de Manejo es obligatoria", this); } }
            if (error == false)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar este candidato?');", true);
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string Confirma_a = (string)Session["Caso_Confirmacion"];
                Pre_EmpleadosENT entidad = new Pre_EmpleadosENT();
                Pre_EmpleadosCOM componente = new Pre_EmpleadosCOM();
                switch (Confirma_a)
                {
                    case "Guardar":
                        bool capacitacion = false;
                        if (Convert.ToInt32(Session["idc_curso"]) == 0) { capacitacion = true; }
                        SqlMoney complementos = SqlMoney.Parse((txtComplementos.Text).ToString());
                        entidad.Complementos = complementos;
                        entidad.Pobersvaciones = txtobservaciones.Text.ToUpper();
                        entidad.Correo_personal = txtCorreo.Text;
                        entidad.Curp = TXTCURP.Text.ToUpper();
                        entidad.Pcapacitacion = capacitacion;
                        entidad.Direccion = txtCalle.Text.ToUpper() + " " + txtMunicipio.Text.ToUpper();
                        entidad.Esposo = txtEsposo.Text.ToUpper();
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        SqlDateTime fec_nac = SqlDateTime.Parse(txtFecNac.Text);
                        entidad.Fec_nac = fec_nac;
                        entidad.Idc_colonia = Convert.ToInt32(ddlColonia.SelectedValue.ToString());
                        entidad.Idc_edocivil = Convert.ToInt32(ddlEstadoCivil.SelectedValue.ToString());
                        entidad.Idc_estado = Convert.ToInt32(ddlestado.SelectedValue.ToString());
                        entidad.Idc_nzona = 1;
                        entidad.Idc_prepara = Convert.ToInt32(Session["idc_prepara"]);
                        entidad.Idc_puesto = Convert.ToInt32(Session["idc_puesto"]);//IDC_PUESTO
                        entidad.Idc_sucursal = 1;
                        entidad.Nombre = txtNombres.Text.ToUpper();
                        entidad.Paterno = txtPaterno.Text.ToUpper();
                        entidad.Materno = txtMaterno.Text.ToUpper();
                        entidad.Nombre_madre = txtMadre.Text.ToUpper();
                        entidad.Nombre_padre = txtPadre.Text.ToUpper();
                        entidad.Num_imss = txtIMSS.Text;
                        bool premiotranbsp = false;
                        if (cbxPremioTransporte.Checked == true) { premiotranbsp = true; }
                        entidad.Premio_transporte = premiotranbsp;
                        entidad.Rfc = txtRFC.Text;
                        entidad.Sexo = ddlSexo.SelectedValue;
                        SqlMoney sueldo = SqlMoney.Parse((txtSueldo.Text).ToString());
                        entidad.Sueldo = sueldo;
                        entidad.Titulo = ddlTitulo.SelectedValue;
                        entidad.Cadtel = CadenaTelefonos();
                        entidad.Numcadtel = TotalCadenaTelefonos();
                        entidad.Cadhijos = CadenaHijos();
                        entidad.Numcadhijos = TotalCadenaHijos();
                        entidad.Cadhorarios = CadenaHorarios();
                        int idc = 0;
                        foreach (ListItem item in cbxhorarios.Items)
                        {
                            if (item.Selected == true)
                            {
                                idc = Convert.ToInt32(item.Value);
                            }
                        }
                        entidad.Numcadhorarios = idc;
                        entidad.Cadelelic = CadenaElectorLicencia();
                        entidad.Numcadelelic = 1;
                        entidad.cadena_papeleria = CadenaPapeleriaDet();
                        entidad.tot_cadena_pape = TotalPapeleriaDet();
                        DataSet ds = componente.GuardarPreEmpleas(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        string mensaje = row["mensaje"].ToString();
                        int idc_pre_empleado = Convert.ToInt16(row["idc_pre_empleado"].ToString());
                        entidad.Idc_pre_empleado = idc_pre_empleado;
                        if (mensaje == "")//no hay errores retornamos true
                        {
                            bool correct = true;
                            DataTable elector = (DataTable)Session["elector"];
                            DataTable licencia = (DataTable)Session["licencia"];
                            DataTable papeleria = (DataTable)Session["papeleria"];
                            DataTable foto = (DataTable)Session["foto"];
                            foreach (DataRow row_pape in foto.Rows)
                            {
                                string ruta = funciones.GenerarRuta("FOT_CAN", "unidad");//FOTO CANDIDATOS
                                string new_ruta = ruta + idc_pre_empleado.ToString() + row_pape["extension"];
                                correct = CopiarArchivos(row_pape["ruta"].ToString(), new_ruta);
                                if (correct != true) { Alert.ShowAlertError("Hubo un error al subir la foto de perfil Verifiquelo con el Departamento de Sistemas", this); }
                            }
                            foreach (DataRow row_pape in elector.Rows)
                            {
                                string ruta = funciones.GenerarRuta("CRECANA", "unidad");//atras
                                string new_ruta = ruta + idc_pre_empleado.ToString() + row_pape["extension"];
                                correct = CopiarArchivos(row_pape["rutaatras"].ToString(), new_ruta);
                                string rutafre = funciones.GenerarRuta("CRECANF", "unidad");//FRENTE
                                string new_rutafre = rutafre + idc_pre_empleado.ToString() + row_pape["extension"];
                                correct = CopiarArchivos(row_pape["rutafrente"].ToString(), new_rutafre);
                                if (correct != true) { Alert.ShowAlertError("Hubo un error al subir la papeleria. Verifiquelo con el Departamento de Sistemas", this); }
                            }
                            foreach (DataRow row_pape in licencia.Rows)
                            {
                                string ruta = funciones.GenerarRuta("LICCANA", "unidad");//atras
                                string new_ruta = ruta + idc_pre_empleado.ToString() + row_pape["extension"];
                                correct = CopiarArchivos(row_pape["rutaatras"].ToString(), new_ruta);
                                string rutafre = funciones.GenerarRuta("LICCANF", "unidad");//FRENTE
                                string new_rutafre = rutafre + idc_pre_empleado.ToString() + row_pape["extension"];
                                correct = CopiarArchivos(row_pape["rutafrente"].ToString(), new_rutafre);
                                if (correct != true) { Alert.ShowAlertError("Hubo un error al subir la papeleria. Verifiquelo con el Departamento de Sistemas", this); }
                            }

                            if (ds.Tables.Count > 1)
                            {
                                DataTable Pape_dest = ds.Tables[1];
                                foreach (DataRow row_pape in papeleria.Rows)
                                {
                                    if (!row_pape["tipo"].ToString().Equals("elector") && !row_pape["tipo"].ToString().Equals("licencia"))
                                    {
                                        foreach (DataRow row_ in Pape_dest.Rows)
                                        {
                                            //verificamos que no existan errores
                                            int idc_tipodocarc = Convert.ToInt16(row_["idc_tipodocarc"].ToString());
                                            if (idc_tipodocarc == Convert.ToInt32(row_pape["idc_tipodocarc"]))
                                            {
                                                correct = CopiarArchivos(row_pape["ruta"].ToString(), row_["destino"].ToString() + Path.GetExtension(row_pape["ruta"].ToString()));
                                                if (correct != true) { Alert.ShowAlertError("Hubo un error al subir la papeleria. Verifiquelo con el Departamento de Sistemas", this); }
                                            }
                                        }
                                    }
                                }
                            }

                            if (correct == true)
                            {
                                int total = (((papeleria.Rows.Count) * 1) + 1) * 1000;
                                string t = total.ToString();
                                int archivos_procesados = papeleria.Rows.Count;
                                string url = Session["redirect"] == null ? "candidatos_preparar.aspx" : (string)Session["redirect"];
                                Alert.ShowGiftMessage("Estamos procesando la cantidad de " + archivos_procesados.ToString() + " archivo(s) al Servidor.", "Espere un Momento", url, "imagenes/loading.gif", t, "El Pre Empleado fue Guardado correctamente.", this);
                            }
                        }
                        else
                        {
                            Alert.ShowAlertError(mensaje, this);
                        }
                        break;

                    case "Cancelar":
                        Response.Redirect("candidatos_preparar.aspx");
                        break;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void imgdeletefoto_Click(object sender, ImageClickEventArgs e)
        {
            DataTable foto = (DataTable)Session["foto"];
            foto.Rows.Clear();
            Session["foto"] = foto;
            REV.Visible = true;
            REV.Enabled = true;
            imgdeletefoto.Visible = false;
            partupload.Visible = true;
            fupFotoPerfil.Visible = true;
            lnkAgregarFotoPerfil.Visible = true;
            btnVer.Visible = false;
        }

        protected void cbxLaborables_CheckedChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeatdias.Items)
            {
                DropDownList ddlhorariodia = (DropDownList)item.FindControl("ddlhorariodia");
                DropDownList ddlhorariocomida = (DropDownList)item.FindControl("ddlhorariocomida");
                CheckBox cbxLaborables = (CheckBox)item.FindControl("cbxLaborables");
                CheckBox cbxDescanso = (CheckBox)item.FindControl("cbxDescanso");
                //aqui me quede
                bool descanso = Convert.ToBoolean(Session["aplica_descanso_diario"]);
                cbxDescanso.Enabled = false;
                cbxDescanso.Checked = false;
                if (descanso == true && cbxLaborables.Checked == true)
                {
                    cbxDescanso.Enabled = true;
                }
                ddlhorariodia.Visible = true;
                if (cbxLaborables.Checked == false) { ddlhorariodia.Visible = false; }
            }
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            DataTable foto = (DataTable)Session["foto"];
            DataRow row = foto.Rows[0];
            string nombre = row["nombre"].ToString();
            string ruta = row["ruta"].ToString();
            string ext = row["extension"].ToString();
            Download(ruta, nombre);
        }

        protected void cbxhorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc = 0;
            foreach (ListItem item in cbxhorarios.Items)
            {
                if (item.Selected == true)
                {
                    idc++;
                }

                lblErrorHorarioHORARIO.Visible = false;
                if (idc > 1)
                {
                    lblErrorHorarioHORARIO.Visible = true;
                    lblErrorHorarioHORARIO.Text = "SELECCIONE SOLO UN HORARIO";
                    break;
                }
            }
        }
    }
}