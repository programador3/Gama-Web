using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class cursos_preparacion : System.Web.UI.Page
    {
        public string ruta = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            //si nop trae valores regreso
            if (Request.QueryString["idc_puesto"] == null)
            {
                Response.Redirect("revisiones.aspx");
            }
            if (!IsPostBack)
            {
                Session["tabla_examenes"] = null;
            }
            //bajo valores
            int idc_puesto = 0;
            int idc_usuario = 0;
            int idc_puestobaja = 0;
            if (Request.QueryString["preview"] == null)//si no viene del administrador
            {
                idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_puestobaja = Convert.ToInt32(Request.QueryString["idc_puesto"].ToString());
            }
            else
            {
                idc_puesto = Convert.ToInt32(Request.QueryString["idc_puesto"].ToString());
                idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                idc_puestobaja = Convert.ToInt32(Request.QueryString["idc_puestobaja"].ToString());
                Alert.ShowAlertInfo("Esta pagina esta en modo informativo, NO PODRA guardar la Revisión. Si desea hacerlo, verifique el apartado de notificaciones", "Mensaje del sistema", this);
                btnCancelar.Visible = false;
                btnGuardar.Visible = false;
            }
            if (!Page.IsPostBack)
            {
                //cargo datos a tablas
                CargarGridPrincipal(idc_puesto, idc_puestobaja);
            }
        }

        /// <summary>
        /// Carga los datos en Tablas de sesion y carga el grid principal
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal(int idc_puestoprep, int idc_puesto)
        {
            Cursos_PreparacionENT entidad = new Cursos_PreparacionENT();
            Cursos_PreparacionCOM componente = new Cursos_PreparacionCOM();
            entidad.Pidc_puesto = idc_puesto;
            entidad.Pidc_puestoprep = idc_puestoprep;
            DataSet ds = componente.CargaCursosPrep(entidad);
            Session["Tabla_DatosPuesto"] = ds.Tables[0];
            Session["Tabla_Cursos"] = ds.Tables[1];
            Session["Tabla_archivos"] = ds.Tables[2];
            repeat_cursos.DataSource = ds.Tables[1];
            repeat_cursos.DataBind();
            if (ds.Tables[2].Rows.Count != 0)
            {
                panel_archvios.Visible = true;
                repeater_archi.DataSource = ds.Tables[2];
                repeater_archi.DataBind();
            }
            DataRow row = ds.Tables[0].Rows[0];
            lblPuesto.Text = row["descripcion"].ToString();
            lblfechasoli.Text = row["fecha_registro"].ToString();
            Session["idc_prepara"] = Convert.ToInt32(row["idc_prepara"].ToString());
            Session["idc_revisionser"] = Convert.ToInt32(row["idc_revisionser"].ToString());
        }

        /// <summary>
        /// Genera cadena con resultado de preparaciones
        /// </summary>
        /// <returns></returns>
        public string GeneraCadena()
        {
            string list = "";
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Cursos"];
            foreach (RepeaterItem item in repeat_cursos.Items)
            {
                TextBox txtObservaciones = (TextBox)item.FindControl("txtObservaciones");
                TextBox txttipo = (TextBox)item.FindControl("txttipo");
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                Label lblidc = (Label)item.FindControl("lblidc_curso");
                if (txtObservaciones.ReadOnly == false)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        bool externo = true;
                        if (txttipo.Text.Equals("Interno")) { externo = false; }
                        if (Convert.ToInt32(row["idc_curso"]) == Convert.ToInt32(lblidc.Text))
                        {
                            list = list + (lblidc.Text + ";" + externo.ToString() + ";" + cbx.Checked.ToString() + ";" + txtObservaciones.Text + ";");
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
        public int TotalCadena()
        {
            int list = 0;
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            DataTable tabla = (DataTable)Session["Tabla_Cursos"];
            foreach (RepeaterItem item in repeat_cursos.Items)
            {
                TextBox txtObservaciones = (TextBox)item.FindControl("txtObservaciones");
                TextBox txttipo = (TextBox)item.FindControl("txttipo");
                CheckBox cbx = (CheckBox)item.FindControl("cbx");
                Label lblidc = (Label)item.FindControl("lblidc_curso");
                if (txtObservaciones.ReadOnly == false)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        if (Convert.ToInt32(row["idc_curso"]) == Convert.ToInt32(lblidc.Text))
                        {
                            list = list + 1;
                        }
                    }
                }
            }
            return list;
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
                var carpeta = row["unidad"].ToString();
                var domn = Request.Url.Host;
                switch (codigo_imagen)
                {
                    case "WCURPRO"://imagen scaneo formato revision
                        var url_upload = carpeta;
                        ruta = url_upload;
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

        /// <summary>
        /// Genera una copia de una rchvio en el path local
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GenerarDoc(string filename)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/prep_cursos/"));
            if (File.Exists(ruta + filename))
            {
                File.Copy(ruta + filename, dirInfo + filename, true);
                return dirInfo + filename;
            }
            else
            {
                Alert.ShowAlertError("No existe el formato. Verifique esto con el departamento de sistemas.", this);
                return "";
            }
        }

        protected void repeat_cursos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            TextBox txtObservaciones = (TextBox)e.Item.FindControl("txtObservaciones");
            TextBox txttipo = (TextBox)e.Item.FindControl("txttipo");
            CheckBox cbx = (CheckBox)e.Item.FindControl("cbx");

            string tipo = DataBinder.Eval(dbr, "tipo_curso").ToString();
            int revisado = Convert.ToInt32(DataBinder.Eval(dbr, "revisado"));
            int idc_curso = Convert.ToInt32(DataBinder.Eval(dbr, "idc_curso"));
            cursos_examenes(idc_curso);
            if (revisado == 1)
            {
                cbx.Checked = true;
                cbx.Enabled = false;
                cbx.Text = "Preparado desde: " + Convert.ToString(DataBinder.Eval(dbr, "fecha")).ToUpper();
                txtObservaciones.Text = Convert.ToString(DataBinder.Eval(dbr, "observaciones")).ToUpper();
                txtObservaciones.ReadOnly = true;
            }
            if (tipo.Equals("E"))
            {
                txttipo.Text = "Externo";
                cbx.Checked = false;
                cbx.Enabled = true;
            }
        }

        protected void cursos_examenes(int idc_curso)
        {
            //componente
            Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
            DataSet ds = new DataSet();
            ds = ComCursoHist.cursos_examenes(idc_curso);
            if (Session["tabla_examenes"] == null)
            {
                Session["tabla_examenes"] = ds.Tables[0];
            }
            else
            {
                DataTable table_sess = (DataTable)Session["tabla_examenes"];
                table_sess.Merge(ds.Tables[0]);
                Session["tabla_examenes"] = table_sess;
            }
            gridExamenes.DataSource = (DataTable)Session["tabla_examenes"];
            gridExamenes.DataBind();
        }

        protected void lnkDescargarFormato_Click(object sender, EventArgs e)
        {
            LinkButton lnkDescargarFormato = (LinkButton)sender;
            string path = lnkDescargarFormato.CommandName.ToString();
            string file = lnkDescargarFormato.CommandArgument.ToString();
            Download(path, file);
        }

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
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    Cursos_PreparacionENT entidad = new Cursos_PreparacionENT();
                    Cursos_PreparacionCOM componente = new Cursos_PreparacionCOM();
                    entidad.Idc_prepara = Convert.ToInt32(Session["idc_prepara"]);
                    entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                    entidad.Pidc_puestoprep = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                    entidad.Pidc_revisionser = Convert.ToInt32(Session["idc_revisionser"].ToString());
                    entidad.Cadser = GeneraCadena();
                    entidad.Totalcadser = TotalCadena();
                    try
                    {
                        DataSet ds = componente.InsertarPrep(entidad);
                        DataRow row = ds.Tables[0].Rows[0];
                        //verificamos que no existan errores
                        string mensaje = row["mensaje"].ToString();
                        if (mensaje == "")//no hay errores retornamos true
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La Preparación Total o Parcial fue guardada correctamente.','preparaciones.aspx');", true);
                        }
                        else
                        {//mostramos error
                            Alert.ShowAlertError(mensaje, this);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.Message, this);
                    }

                    break;

                case "Cancelar":
                    Response.Redirect("preparaciones.aspx");
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
            //if (TotalCadenaVehiculos() == 0 && TotalCadenaVehiculos_Herr() == 0)
            //{
            //    Alert.ShowAlertError("Para Guardar, debe Preparar al menos un Activo.", this);
            //    error = true;
            //}
            if (error != true)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de guardar la Preparacion Completa. Todos sus datos son correctos?');", true);
            }
        }

        protected void repeater_archi_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            string archivo = DataBinder.Eval(dbr, "archivo").ToString();
            string descripcion = DataBinder.Eval(dbr, "descripcion").ToString();
            string extencion = DataBinder.Eval(dbr, "extension").ToString();
            LinkButton lnkDescargarFormato = (LinkButton)e.Item.FindControl("lnkDescargarFormato");
            GenerarRuta("WCURPRO");
            lnkDescargarFormato.CommandName = GenerarDoc(archivo);
            lnkDescargarFormato.CommandArgument = descripcion + extencion;
        }

        protected void gridExamenes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridExamenes.DataKeys[index].Values["RUTA"].ToString();
            string name = Path.GetFileName(ruta);
            DescargarArchivo(ruta, name);
        }

        protected void DescargarArchivo(string path_archivo, string nombre_archivo)
        {
            if (File.Exists(path_archivo))
            {
                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                Response.AddHeader("Content-Disposition", "attachment; filename=" + nombre_archivo);//Revision.pptx");
                                                                                                    // Escribimos el fichero a enviar
                Response.WriteFile(path_archivo);
                // volcamos el stream
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
            }
            else
            {
                Alert.ShowAlertError("NO EXISTE EL ARCHIVO " + nombre_archivo, this);
            }
        }
    }
}