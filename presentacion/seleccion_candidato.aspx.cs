using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class seleccion_candidato : System.Web.UI.Page
    {
        public string ruta = "";
        public bool encurso = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (Request.QueryString["idc_puesto"] == null)
            {
                Response.Redirect("revisones_preparacion.aspx");
            }
            //bajo valores
            int idc_puesto = 0;
            int idc_usuario = 0;
            int idc_puestobaja = 0;
            int idc_prepara = 0;
            if (Request.QueryString["preview"] == null)//si no viene del administrador
            {
                idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_puestobaja = Convert.ToInt32(Request.QueryString["idc_puesto"].ToString());
                idc_prepara = Convert.ToInt32(Request.QueryString["idc_prepara"].ToString());
            }
            else
            {
                idc_puesto = Convert.ToInt32(Request.QueryString["idc_puesto"].ToString());
                idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_puestobaja = Convert.ToInt32(Request.QueryString["idc_puestobaja"].ToString());
                idc_prepara = Convert.ToInt32(Request.QueryString["idc_prepara"].ToString());

                Alert.ShowAlertInfo("Esta pagina esta en modo informativo, NO PODRA guardar la Revisión. Si desea hacerlo, verifique el apartado de notificaciones", "Mensaje del sistema", this);
            }
            if (!Page.IsPostBack)
            {
                DataTable tablanum = new DataTable();
                tablanum.Columns.Add("rango");
                Session["numero_orden"] = tablanum;
                //cargo datos a tablas
                DataPrep(idc_puestobaja, idc_prepara, 0);
            }
        }

        /// <summary>
        /// Genera ruta de archivos
        /// </summary>
        public void GenerarRuta(string codigo_imagen)
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
                    case "CANDID":
                        ruta = carpeta;
                        break;
                }
            }
        }

        /// <summary>
        /// Regresa una cadena con los TotalCadenaCandidatos
        /// </summary>
        /// <returns></returns>
        public string CadenaCandidatos()
        {
            string cadena = "";
            foreach (RepeaterItem Item in repeat_candidatos.Items)
            {
                Label lblidc_pre_empleado = (Label)Item.FindControl("lblidc_pre_empleado");
                CheckBox cbxSelected = (CheckBox)Item.FindControl("cbxSelected");
                TextBox txt = (TextBox)Item.FindControl("txtObservaciones");
                DropDownList ddlorden = (DropDownList)Item.FindControl("ddlorden");
                if (cbxSelected.Checked == true)
                {
                    cadena = cadena + lblidc_pre_empleado.Text + ";" + cbxSelected.Checked + ";" + txt.Text.ToUpper() + ";" + ddlorden.SelectedValue + ";";
                }
                if (cbxSelected.Checked == false)
                {
                    cadena = cadena + lblidc_pre_empleado.Text + ";" + cbxSelected.Checked + ";" + txt.Text.ToUpper() + ";" + "0;";
                }
            }
            return cadena;
        }

        /// <summary>
        /// Regresa una cadena con los TotalCadenaCandidatos
        /// </summary>
        /// <returns></returns>
        public int TotalCadenaCandidatos()
        {
            int cadena = 0;
            foreach (RepeaterItem Item in repeat_candidatos.Items)
            {
                Label lblidc_pre_empleado = (Label)Item.FindControl("lblidc_pre_empleado");
                CheckBox cbxSelected = (CheckBox)Item.FindControl("cbxSelected");
                TextBox txt = (TextBox)Item.FindControl("txtObservaciones");
                DropDownList ddlorden = (DropDownList)Item.FindControl("ddlorden");
                cadena = cadena + 1;
            }
            return cadena;
        }

        /// <summary>
        /// Carga los puestos pendientes de preparar
        /// </summary>
        public void DataPrep(int idc_puestosbaja, int idc_prepara, int idc_pre_empleado)
        {
            CandidatosENT entidad = new CandidatosENT();
            CandidatosCOM componente = new CandidatosCOM();
            if (idc_puestosbaja != 0)
            {
                entidad.Pidc_puesto = idc_puestosbaja;
                entidad.Pidc_prepara = idc_prepara;
                entidad.Pidc_pre_empleado = idc_pre_empleado;
                DataSet ds = componente.CargaCandidatos(entidad);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    nohay.Visible = true;
                    btnGuardar.Visible = false;
                }
                else
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    string puesto = row["descripcion"].ToString().ToLower();
                    //pasamos a estilos title
                    lblNombrePuesto.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(puesto);
                }
                int total_num = ds.Tables[0].Rows.Count;
                DataTable orden = (DataTable)Session["numero_orden"];
                for (int i = 0; i < total_num; i++)
                {
                    DataRow new_row = orden.NewRow();
                    new_row["rango"] = i + 1;
                    orden.Rows.Add(new_row);
                }

                Session["numero_orden"] = orden;
                repeat_candidatos.DataSource = ds.Tables[0];
                repeat_candidatos.DataBind();
            }
            if (idc_pre_empleado != 0)
            {
                entidad.Pidc_puesto = idc_puestosbaja;
                entidad.Pidc_prepara = idc_prepara;
                entidad.Pidc_pre_empleado = idc_pre_empleado;
                DataSet ds = componente.CargaCandidatos(entidad);
                txtobservaciones2.Text = ds.Tables[1].Rows[0]["observaciones"].ToString();
                gridDetalles.DataSource = ds.Tables[1];
                gridDetalles.DataBind();
                repeat_telefonos.DataSource = ds.Tables[2];
                repeat_telefonos.DataBind();
                repeat_papeleria.DataSource = ds.Tables[3];
                repeat_papeleria.DataBind();
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
            Pre_EmpleadosENT entidad = new Pre_EmpleadosENT();
            Pre_EmpleadosCOM componente = new Pre_EmpleadosCOM();
            switch (Confirma_a)
            {
                case "Guardar":
                    entidad.Cadsel = CadenaCandidatos();
                    entidad.Numcad = TotalCadenaCandidatos();
                    entidad.Pidc_prepara = Convert.ToInt32(Request.QueryString["idc_prepara"].ToString());
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    DataSet ds = componente.GuardaSeleccion(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La Selección fue Guardada Correctamente.','menu.aspx','success');", true);
                    }
                    else
                    {
                        Alert.ShowAlertConfirm(mensaje, "No puede continuar", "administrador_preparaciones.aspx", this);
                    }
                    break;

                case "Cancelar":
                    Response.Redirect("menu.aspx");
                    break;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        protected void repeat_candidatos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataTable orden = (DataTable)Session["numero_orden"];
            DropDownList ddlorden = (DropDownList)e.Item.FindControl("ddlorden");
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            LinkButton lnkver = (LinkButton)e.Item.FindControl("lnkver");
            int idc_prepara = Convert.ToInt32(DataBinder.Eval(dbr, "idc_prepara"));
            int idc_pre_empleado = Convert.ToInt32(DataBinder.Eval(dbr, "idc_pre_empleado"));
            lnkver.CommandArgument = idc_prepara.ToString();
            lnkver.CommandName = idc_pre_empleado.ToString();
            ddlorden.DataTextField = "rango";
            ddlorden.DataValueField = "rango";
            ddlorden.DataSource = orden;
            ddlorden.DataBind();
        }

        protected void cbxSelected_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbx = (CheckBox)sender;
            foreach (RepeaterItem Item in repeat_candidatos.Items)
            {
                DataTable orden = (DataTable)Session["numero_orden"];
                DropDownList ddlorden = (DropDownList)Item.FindControl("ddlorden");
                Panel panelorden = (Panel)Item.FindControl("panelorden");
                CheckBox cbxSelected = (CheckBox)Item.FindControl("cbxSelected");
                System.Web.UI.WebControls.Image imgYes = (System.Web.UI.WebControls.Image)Item.FindControl("imgYes");
                System.Web.UI.WebControls.Image imgNo = (System.Web.UI.WebControls.Image)Item.FindControl("imgNo");
                Label lblacepted = (Label)Item.FindControl("lblacepted");
                Panel panelobsr = (Panel)Item.FindControl("panelobsr");
                panelobsr.Visible = true;
                panelorden.Visible = false;
                imgNo.Visible = true;
                imgYes.Visible = false;
                lblacepted.Text = "Rechazado";
                lblacepted.CssClass = "label label-default";
                if (cbxSelected.Checked == true)
                {
                    panelorden.Visible = true; panelobsr.Visible = false; imgNo.Visible = false;
                    imgYes.Visible = true; lblacepted.Text = "Aceptado"; lblacepted.CssClass = "label label-info";
                }
            }
        }

        protected void ddlorden_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool chechedant = false;
            List<String> list_values = new List<String>();
            foreach (RepeaterItem Item in repeat_candidatos.Items)
            {
                CheckBox cbxSelected = (CheckBox)Item.FindControl("cbxSelected");
                Label lblerrorobs = (Label)Item.FindControl("lblerrorobs");
                Label lblerrororden = (Label)Item.FindControl("lblerrororden");
                TextBox txt = (TextBox)Item.FindControl("txtObservaciones");
                DropDownList ddlorden = (DropDownList)Item.FindControl("ddlorden");
                Panel panelorden = (Panel)Item.FindControl("panelorden");
                lblerrorobs.Visible = false;
                lblerrororden.Visible = false;
                if (cbxSelected.Checked == false && txt.Text == string.Empty)
                {
                    lblerrorobs.Visible = true; lblerrorobs.Text = "DEBE COLOCAR UNA OBSERVACION";
                }
                if (list_values.Exists(w => w.EndsWith(ddlorden.SelectedValue)) && panelorden.Visible == true && cbxSelected.Checked == true && chechedant == true)
                {
                    lblerrororden.Visible = true; lblerrororden.Text = "DEBE COLOCAR UN ORDEN CORRECTO SIN REPETIR";
                }

                chechedant = cbxSelected.Checked;
                list_values.Add(ddlorden.SelectedValue);
            }
        }

        protected void btnGuardar_Click1(object sender, EventArgs e)
        {
            bool error = false;
            List<String> list_values = new List<String>();
            bool chechedant = false;
            bool algunactivo = false;
            foreach (RepeaterItem Item in repeat_candidatos.Items)
            {
                CheckBox cbxSelected = (CheckBox)Item.FindControl("cbxSelected");
                Label lblerrorobs = (Label)Item.FindControl("lblerrorobs");
                Label lblerrororden = (Label)Item.FindControl("lblerrororden");
                TextBox txt = (TextBox)Item.FindControl("txtObservaciones");
                DropDownList ddlorden = (DropDownList)Item.FindControl("ddlorden");
                lblerrorobs.Visible = false;
                lblerrororden.Visible = false;
                if (cbxSelected.Checked == false && txt.Text == string.Empty)
                {
                    lblerrorobs.Visible = true; lblerrorobs.Text = "DEBE COLOCAR UNA OBSERVACION"; error = true;
                }
                if (list_values.Exists(w => w.EndsWith(ddlorden.SelectedValue)) && cbxSelected.Checked == true && chechedant == true)
                {
                    lblerrororden.Visible = true; lblerrororden.Text = "DEBE COLOCAR UN ORDEN CORRECTO SIN REPETIR"; error = true;
                }
                if (cbxSelected.Checked == true)
                {
                    algunactivo = true;
                }
                chechedant = cbxSelected.Checked;
                list_values.Add(ddlorden.SelectedValue);
            }
            Session["Caso_Confirmacion"] = "Guardar";
            if (error == false && algunactivo == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿La Selección de Empleados se Guardara. Esta Seguro de Continuar?');", true);
            }
            if (error == false && algunactivo == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea rechazar a todos los candidatos?, Esto se notificara a Reclutamiento para la busqueda de nuevos candidatos');", true);
            }
        }

        protected void btnCancelar_Click1(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        protected void lnkver_Click(object sender, EventArgs e)
        {
            LinkButton lnkver = (LinkButton)sender;
            int idc_prepara = Convert.ToInt32(lnkver.CommandArgument);
            int idc_pre_empleado = Convert.ToInt32(lnkver.CommandName);
            DataPrep(0, idc_prepara, idc_pre_empleado);
            ScriptManager.RegisterStartupScript(this, GetType(), "preherr", "ModalPreBaja();", true);
        }

        protected void lnkdownload_Click(object sender, EventArgs e)
        {
            LinkButton lnkdownload = (LinkButton)sender;
            string ruta = lnkdownload.CommandName.ToString();
            string archivo = lnkdownload.CommandArgument.ToString();
            Download(ruta, archivo);
        }

        protected void repeat_papeleria_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            LinkButton lnkdownload = (LinkButton)e.Item.FindControl("lnkdownload");
            string ruta = Convert.ToString(DataBinder.Eval(dbr, "ruta"));
            string archivo = Convert.ToString(DataBinder.Eval(dbr, "archivo"));
            lnkdownload.CommandName = ruta;
            lnkdownload.CommandArgument = archivo;
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

        protected void txtObservaciones_TextChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem Item in repeat_candidatos.Items)
            {
                CheckBox cbxSelected = (CheckBox)Item.FindControl("cbxSelected");
                Label lblerrorobs = (Label)Item.FindControl("lblerrorobs");
                TextBox txt = (TextBox)Item.FindControl("txt");
                if (txt.Text != string.Empty)
                {
                    lblerrorobs.Visible = false;
                }
            }
        }

        protected void lnkInfoOrden_Click(object sender, EventArgs e)
        {
            Alert.ShowAlertInfo("Si selecciona 1 o mas candidatos, puede elegir el orden de prioridad para cada uno.", "Mensaje del Sistema", this);
        }
    }
}