using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Winthusiasm.HtmlEditor;

namespace presentacion
{
    public partial class html : System.Web.UI.Page
    {
        protected string initialHtml = @"<h1>SISTEMA WEB GAMA MATERIALES </h1>
<p>Esta herramienta funciona para generar archivos html.</p><p><b>Este texto esta en negrita</b>, <span style='text-style: italic'>este en cursiva</span> y <span style='text-decoration: underline'>este subrayado</span>.</p>
<p><span style='font-family: Arial'>Este texto es Arial</span>, &nbsp;<span style='font-family: Garamond'>este&nbsp;Garamond</span>, <span style='font-family: Verdana'>y este&nbsp;Verdana</span>.</p><p>Esta es una lista</p>
<ul>
<li>Item 1 </li>
<li>Item 2</li>
</ul>";

        protected bool InternetExplorer
        {
            get { return Request.Browser.Browser.Equals("IE"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);
            sm.RegisterAsyncPostBackControl(SaveButton);
            sm.RegisterAsyncPostBackControl(ClearButton);
            Random random = new Random();
            int randomNumber = random.Next(0, 1000000);
            if (!IsPostBack && Request.QueryString["edit_live"] == null)//si no trae request significa que no es edicio
            {
                lblsession_h.Text = randomNumber.ToString();
                Editor.Text = initialHtml;
                CargarGrid();
                btnGuardarEdicionLive.Visible = false;
                PanelTitulo.Visible = true;
                SaveButton.Visible = true;
            }
            if (!IsPostBack && Request.QueryString["edit_live"] != null)//si tare request siginfica que es edicion tipo perfiles
            {
                lblsession_h.Text = randomNumber.ToString();

                if (Request.QueryString["url"] != null)
                {
                    string ruta = funciones.de64aTexto(Request.QueryString["url"]);
                    CargarGrid();
                    CargarDatosEdicion(ruta);
                    SaveButton.Visible = false;
                    PanelTitulo.Visible = false;
                    btnGuardarEdicionLive.Visible = false;
                    btnsave_detalles.Visible = true;
                }
                else
                {
                    lblsession.Text = funciones.de64aTexto(Request.QueryString["dinamic_id"]);
                    int id = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_html"]));
                    Session[lblsession.Text + "idc_etiqueta_htmlfile"] = id.ToString();
                    btnGuardarEdicionLive.Visible = true;
                    SaveButton.Visible = false;
                    PanelTitulo.Visible = false;
                    CargarGrid();
                    CargarDatosEdicionLocal();
                }
            }
        }

        /// <summary>
        /// Carga los datos d eun archvio fisico o local
        /// </summary>
        private void CargarDatosEdicionLocal()
        {
            string ruta = funciones.de64aTexto(Request.QueryString["edit_htmlfile"]);
            if (File.Exists(ruta))
            {
                StreamReader file = new StreamReader(ruta);
                Editor.Text = file.ReadToEnd();
                file.Close();
            }
        }

        /// <summary>
        /// Carga los datos d eun archvio fisico o local
        /// </summary>
        private void CargarDatosEdicion(string ruta)
        {
            string path = Path.GetDirectoryName(ruta);
            string extension = Path.GetExtension(ruta);
            string name = Path.GetFileNameWithoutExtension(ruta);
            DateTime localDate = DateTime.Now;
            string date = localDate.ToString();
            date = date.Replace("/", "_");
            date = date.Replace(":", "_");
            File.Copy(ruta, path + @"\backup_" + date + name + extension, true);
            if (File.Exists(ruta))
            {
                StreamReader file = new StreamReader(ruta);
                Editor.Text = file.ReadToEnd();
                file.Close();
            }
        }

        private void CargarGrid()
        {
            txtTitulo.Text = "";
            Editor.Text = initialHtml;
            HtmlENT entidad = new HtmlENT();
            HtmlCOM componente = new HtmlCOM();
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            DataSet ds = componente.SelectHTML(entidad);
            ddlhistorial.DataTextField = "titulo";
            ddlhistorial.DataValueField = "idc_html";
            ddlhistorial.DataSource = ds.Tables[0];
            ddlhistorial.DataBind();
            ddlhistorial.Items.Insert(0, new ListItem("Sus Archivos HTML Recientes", "0")); //updated code}
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            if (!IsPostBack)
            {
                string toggleMode = this.Request.QueryString["ToggleMode"];
                if (toggleMode != null)
                    Editor.ToggleMode = GetToggleMode(toggleMode);

                string colorScheme = this.Request.QueryString["ColorScheme"];
                if (colorScheme != null)
                    Editor.ColorScheme = GetColorScheme(colorScheme);

                string noToolstripBackgroundImage = this.Request.QueryString["NoToolstripBackgroundImage"];
                if (noToolstripBackgroundImage != null)
                    Editor.NoToolstripBackgroundImage = noToolstripBackgroundImage == "true";

                string xhtml = this.Request.QueryString["XHTML"];
                if (xhtml != null)
                    Editor.OutputXHTML = xhtml == "true";

                string deprecated = this.Request.QueryString["Deprecated"];
                if (deprecated != null)
                    Editor.ConvertDeprecatedSyntax = deprecated == "true";

                string paragraphs = this.Request.QueryString["Paragraphs"];
                if (paragraphs != null)
                    Editor.ConvertParagraphs = paragraphs == "true";
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);
        }

        protected HtmlEditor.ToggleModeType GetToggleMode(string toggleMode)
        {
            HtmlEditor.ToggleModeType toggleModeType;

            switch (toggleMode)
            {
                case "Tabs":
                    toggleModeType = HtmlEditor.ToggleModeType.Tabs;
                    break;

                case "ToggleButton":
                    toggleModeType = HtmlEditor.ToggleModeType.ToggleButton;
                    break;

                case "Buttons":
                    toggleModeType = HtmlEditor.ToggleModeType.Buttons;
                    break;

                case "None":
                    toggleModeType = HtmlEditor.ToggleModeType.None;
                    break;

                default:
                    toggleModeType = HtmlEditor.ToggleModeType.Tabs;
                    break;
            }

            return toggleModeType;
        }

        protected HtmlEditor.ColorSchemeType GetColorScheme(string colorScheme)
        {
            HtmlEditor.ColorSchemeType colorSchemeType;

            switch (colorScheme)
            {
                case "Custom":
                    colorSchemeType = HtmlEditor.ColorSchemeType.Custom;
                    break;

                case "VisualStudio":
                    colorSchemeType = HtmlEditor.ColorSchemeType.VisualStudio;
                    break;

                default:
                    colorSchemeType = HtmlEditor.ColorSchemeType.Default;
                    break;
            }

            return colorSchemeType;
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            Editor.Text = String.Empty;
        }

        protected void XHTMLBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            Editor.OutputXHTML = box.Checked;
            Editor.Revert();
            UpdatePanel1.Update();
        }

        protected void DeprecatedBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            Editor.ConvertDeprecatedSyntax = box.Checked;
            Editor.Revert();
            UpdatePanel1.Update();
        }

        protected void ParagraphsBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            Editor.ConvertParagraphs = box.Checked;

            Editor.Revert();
            UpdatePanel1.Update();
        }

        protected void PreviewButton_Click(object sender, EventArgs e)
        {
        }

        protected string GetRedirectUrl()
        {
            string url = "Demo.aspx?";
            return url;
        }

        protected void Redirect_EventHandler(object sender, EventArgs e)
        {
            this.Response.Redirect(GetRedirectUrl());
        }

        protected class DataStore
        {
            public static void StoreHtml(string html)
            {
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            DataStore.StoreHtml(Editor.Text);
            string content = Editor.Text;
            string confirmValue = Request.Form["confirm_value"];
            confirmValue = confirmValue.Substring(confirmValue.LastIndexOf(",") + 1);
            if (confirmValue == "Yes")
            {
                if (content == "")
                {
                    Alert.ShowAlertError("No hay contenido que guardar", this);
                }
                if (txtTitulo.Text == "")
                {
                    Alert.ShowAlertError("Escriba un titulo para el archivo", this);
                }

                if (content != "" && txtTitulo.Text != "" && Session["value_edit_intern"] == null)
                {
                    Session["case"] = "Guardar";
                    Event_Buttons();
                }
                if (content != "" && txtTitulo.Text != "" && Session["value_edit_intern"] != null)
                {
                    Session["case"] = "Editar Interno";
                    Event_Buttons();
                }
            }
        }

        public void Event_Buttons()
        {
            string type = (string)Session["case"];
            string content = @"<meta http-equiv=" + '\u0022' + "Content-Type" + '\u0022' + "content=" + '\u0022' + "text/html;charset=utf-8" + '\u0022' + "/> " + System.Environment.NewLine + "<!--Diseñador HTML Creado por: Humberto De la Rosa para GAMA MATERIALES Y ACEROS-->" + System.Environment.NewLine + Editor.Text;
            string title = txtTitulo.Text.ToUpper();
            HtmlENT entidad = new HtmlENT();
            HtmlCOM componente = new HtmlCOM();
            switch (type)
            {
                case "Guardar":

                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    entidad.Content = content;
                    entidad.Titulo = txtTitulo.Text.ToUpper();

                    DataSet ds = componente.AgregarHTML(entidad);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                    {
                        CargarGrid();
                        Alert.ShowGift("Estamos procesando el archivo al Servidor.", "Espere un Momento", "imagenes/loading.gif", "3000", "El archivo " + title + " fue guardado en la Base De Datos correctamente", this);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje, this);
                    }
                    break;

                case "Editar Interno":

                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    entidad.Pidc_html = Convert.ToInt32(Session["value_edit_intern"]);
                    entidad.Content = content;
                    entidad.Titulo = txtTitulo.Text.ToUpper();
                    DataSet ds2 = componente.Editar(entidad);
                    string vmensaje2 = ds2.Tables[0].Rows[0]["mensaje"].ToString();
                    if (string.IsNullOrEmpty(vmensaje2)) // si esta vacio todo bien
                    {
                        Session["value_edit_intern"] = null;
                        CargarGrid();
                        Alert.ShowGift("Estamos procesando el archivo al Servidor.", "Espere un Momento", "imagenes/loading.gif", "3000", "El archivo " + title + " fue Editado en la Base De Datos correctamente", this);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje2, this);
                    }
                    break;

                case "Eliminar":

                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    entidad.Pidc_html = Convert.ToInt32(Session["value_edit_intern"]);
                    entidad.Content = content;
                    entidad.Titulo = txtTitulo.Text.ToUpper();
                    DataSet ds3 = componente.Eliminar(entidad);
                    string vmensaje3 = ds3.Tables[0].Rows[0]["mensaje"].ToString();
                    if (string.IsNullOrEmpty(vmensaje3)) // si esta vacio todo bien
                    {
                        Session["value_edit_intern"] = null;
                        CargarGrid();
                        btnEliminarrachivo.Visible = false;
                        Alert.ShowGift("Estamos Eliminando el archivo del Servidor.", "Espere un Momento", "imagenes/loading.gif", "3000", "El archivo " + title + " fue Eliminado en la Base De Datos correctamente", this);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje3, this);
                    }
                    break;

                case "Descargar":
                    Random random = new Random();
                    int randomNumber = random.Next(0, 1000);
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/html_files/"));//path local
                    StreamWriter file = new StreamWriter(dirInfo.ToString() + randomNumber.ToString() + ".html");
                    file.Write(content);
                    file.Close();
                    // Limpiamos la salida
                    Response.Clear();
                    // Con esto le decimos al browser que la salida sera descargable
                    Response.ContentType = "application/octet-stream";
                    // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + randomNumber.ToString() + ".html");
                    // Escribimos el fichero a enviar
                    Response.WriteFile(dirInfo.ToString() + randomNumber.ToString() + ".html");
                    // volcamos el stream
                    Response.Flush();
                    // Enviamos todo el encabezado ahora
                    Response.End();
                    break;

                case "Editar_live":

                    Random random_edit = new Random();
                    int randomNumber_live = random_edit.Next(0, 1000);
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    string etiqueta = funciones.de64aTexto(Request.QueryString["etiqueta_edit"]);
                    DirectoryInfo dirInfo_edit = new DirectoryInfo(Server.MapPath("~/temp/html_files/"));//path local
                    StreamWriter file_edit = new StreamWriter(dirInfo_edit.ToString() + randomNumber_live.ToString() + date + ".html");
                    file_edit.Write(content);
                    file_edit.Close();
                    string mesnaje = AddPapeleriaToTableEtiquetas(dirInfo_edit.ToString() + randomNumber_live.ToString() + date + ".html", txtTitulo.Text.ToUpper() + "html", etiqueta, Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_html"])));
                    Alert.ShowGiftCloseWindows("Estamos procesando el archivo al Servidor.", "Espere un Momento", "imagenes/loading.gif", "3000", this);
                    break;
            }
        }

        /// <summary>
        /// Agrega filas tabla de papeleria global (INCLUYE ELECTOR Y LICENCIA)
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="nombre"></param>
        public string AddPapeleriaToTableEtiquetas(string ruta, string nombre, string etiqueta, int idc)
        {
            string mensaje = "";
            bool exists = false;
            DataTable papeleria = (DataTable)Session[lblsession.Text + "archivos_etiquetas"];
            foreach (DataRow check in papeleria.Rows)
            {
                if (check["etiqueta"].Equals(etiqueta) || Convert.ToInt32(check["idc"]) == idc)
                {
                    check.Delete();
                    break;
                }
            }
            DataRow new_row = papeleria.NewRow();
            new_row["nombre"] = nombre;
            new_row["ruta"] = ruta;
            new_row["etiqueta"] = etiqueta;
            new_row["idc"] = idc;
            papeleria.Rows.Add(new_row);
            Session[lblsession.Text + "archivos_etiquetas"] = papeleria;
            return mensaje;
        }

        protected void btnDescarga_Click(object sender, EventArgs e)
        {
            Session["case"] = "Descargar";
            Event_Buttons();
        }

        protected void btnGuardarEdicionLive_Click(object sender, EventArgs e)
        {
            DataStore.StoreHtml(Editor.Text);
            string content = Editor.Text;
            string confirmValue = Request.Form["confirm_value"];
            confirmValue = confirmValue.Substring(confirmValue.LastIndexOf(",") + 1);
            if (confirmValue == "Yes")
            {
                if (content == "")
                {
                    Alert.ShowAlertError("No hay contenido que guardar", this);
                }
                if (content != "")
                {
                    Session["case"] = "Editar_live";
                    Event_Buttons();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        protected void close_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "noti53SSSSsssS3W3", "window.close();", true);
        }

        protected void ddlhistorial_TextChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(ddlhistorial.SelectedValue);
            Session["value_edit_intern"] = value;
            if (value == 0)
            {
                Alert.ShowAlertError("Seleccione otro valor", this);
            }
            else
            {
                HtmlENT entidad = new HtmlENT();
                HtmlCOM componente = new HtmlCOM();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pidc_html = value;
                DataSet ds = componente.SelectHTMLEspecifico(entidad);
                Editor.Text = ds.Tables[0].Rows[0]["contenido"].ToString();
                txtTitulo.Text = ds.Tables[0].Rows[0]["titulo"].ToString();
                btnEliminarrachivo.Visible = true;
            }
        }

        protected void btnEliminarrachivo_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            confirmValue = confirmValue.Substring(confirmValue.LastIndexOf(",") + 1);
            if (confirmValue == "Yes")
            {
                Session["case"] = "Eliminar";
                Event_Buttons();
            }
        }

        protected void btnUploadIMG_Click(object sender, EventArgs e)
        {
            if (fileimg.HasFile)
            {
                DateTime localDate = DateTime.Now;
                string date = localDate.ToString();
                date = date.Replace("/", "_");
                date = date.Replace(":", "_");
                string path_to_copy = funciones.GenerarRuta("IMAGEN", "unidad");
                string filename = File.Exists(path_to_copy + fileimg.FileName) ? Path.GetFileNameWithoutExtension(path_to_copy + fileimg.FileName) + date + Path.GetExtension(path_to_copy + fileimg.FileName) : fileimg.FileName;
                bool copyok = funciones.UploadFile(fileimg, path_to_copy + filename, this);
                if (copyok == true)
                {
                    Alert.ShowAlert(funciones.GenerarRuta("IMAGEN", "RUTA_WEB") + filename, "El enlace de su imagen es el siguiente:", this);
                }
            }
        }

        protected void btnsave_detalles_Click(object sender, EventArgs e)
        {
            DataStore.StoreHtml(Editor.Text);
            string content = Editor.Text;
            string confirmValue = Request.Form["confirm_value"];
            confirmValue = confirmValue.Substring(confirmValue.LastIndexOf(",") + 1);
            if (confirmValue == "Yes")
            {
                PerfilesE entidad = new PerfilesE();
                PerfilesBL compo = new PerfilesBL();
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Ptipo = Request.QueryString["tipo"];
                entidad.Idc_perfil = Convert.ToInt32(Request.QueryString["idc_perfil"]);
                entidad.Pcadena_examenes = funciones.de64aTexto(Request.QueryString["url"]);
                DataSet ds3 = compo.ValidaArchivo(entidad);
                string vmensaje3 = ds3.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje3)) // si esta vacio todo bien
                {
                    string ruta = funciones.de64aTexto(Request.QueryString["url"]);
                    StreamWriter file = new StreamWriter(ruta);
                    file.Write(content);
                    file.Close();
                    Alert.ShowGiftRedirect("Estamos Modificando el Archivo.", "Espere un Momento", "imagenes/loading.gif", "5000", (string)Session["page_backhtml"], this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje3, this);
                }
            }
        }
    }
}