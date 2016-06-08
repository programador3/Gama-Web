using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class examenes_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && Request.QueryString["edit"] == null)
            {
                Session["descripcion"] = null;
                Session["caso_guarda"] = null;
                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("nombre");
                papeleria.Columns.Add("descripcion");
                papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("extension");
                papeleria.Columns.Add("id_archi");
                Session["papeleria_perfil"] = papeleria;
            }
            if (!Page.IsPostBack && Request.QueryString["edit"] != null)
            {
                Session["descripcion"] = null;
                Session["caso_guarda"] = null;
                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("nombre");
                papeleria.Columns.Add("descripcion");
                papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("extension");
                papeleria.Columns.Add("id_archi");
                Session["papeleria_perfil"] = papeleria;
                CargaDatosEdicion();
            }
        }

        private void CargaDatosEdicion()
        {
            ExamenesENT entidad = new ExamenesENT();
            ExamenesCOM componente = new ExamenesCOM();
            entidad.Pidc_examen = Convert.ToInt32(Session["idc_examen"]);
            DataSet ds = componente.CargaExamenesEdicion(entidad);
            txtNombre.Text = ds.Tables[0].Rows[0]["descripcion"].ToString();
            ddlTipo.SelectedValue = ds.Tables[0].Rows[0]["tipo"].ToString();
            if (ds.Tables[1] != null)
            {
                DataTable papeleria = (DataTable)Session["papeleria_perfil"];
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    DataRow new_row = papeleria.NewRow();
                    new_row["id_archi"] = row["id_archi"];
                    new_row["extension"] = row["extension"];
                    new_row["ruta"] = row["ruta"];
                    new_row["descripcion"] = row["descripcion"];
                    new_row["nombre"] = row["nombre"];
                    papeleria.Rows.Add(new_row);
                }
                Session["papeleria_perfil"] = papeleria;
                gridPapeleria.DataSource = papeleria;
                gridPapeleria.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (txtNombre.Text == "") { error = true; Alert.ShowAlertError("Escriba un nombre o descripcion para el examen", this); }
            if (ddlTipo.SelectedValue == "0") { error = true; Alert.ShowAlertError("Seleccione un Tipo de Examen", this); }
            if (TotalCadenaDocumentos() == 0) { error = true; Alert.ShowAlertError("Deebe subir al menos 1 archivo", this); }
            if (Request.QueryString["edit"] == null)
            {
                Session["Caso_Confirmacion"] = "Guardar";
            }
            if (Request.QueryString["edit"] != null)
            {
                Session["Caso_Confirmacion"] = "Editar";
            }
            if (error == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea guardar el examen " + txtNombre.Text + "?');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("catalogo_examenes.aspx");
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            ExamenesENT entidad = new ExamenesENT();
            ExamenesCOM componente = new ExamenesCOM();
            entidad.Descripcion = txtNombre.Text.ToUpper();
            entidad.Tipo = ddlTipo.SelectedValue;
            entidad.Cadena = GeneraCadenaDocumentos();
            entidad.Total_cadena = TotalCadenaDocumentos();
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
            entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
            entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
            entidad.Pusuariopc = funciones.GetUserName();//usuario pc
            DataSet ds = new DataSet();
            string mensaje = "";
            switch (Confirma_a)
            {
                case "Guardar":
                    ds = componente.Agregar(entidad);
                    mensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    break;

                case "Editar":
                    entidad.Pidc_examen = Convert.ToInt32(Session["idc_examen"]);
                    ds = componente.Editar(entidad);
                    mensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    break;
            }
            if (mensaje == "")
            {
                if (ds.Tables.Count > 0)
                {
                    bool correct = true;
                    DataTable archivos = ds.Tables[1];
                    int total = ((archivos.Rows.Count * 1) + 1) * 1000;
                    string t = total.ToString();
                    foreach (DataRow row in archivos.Rows)
                    {
                        string ruta_orige = row["ruta_origen"].ToString();
                        string ruta_destino = row["ruta_destino"].ToString();
                        correct = CopiarArchivos(ruta_orige, ruta_destino);
                        if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo " + ruta_orige + " Verifiquelo con el Departamento de Sistemas", this); }
                    }
                    if (correct == true)
                    {
                        Alert.ShowGiftMessage("Estamos procesando la cantidad de " + archivos.Rows.Count + " archivo(s) al Servidor.", "Espere un Momento", "catalogo_examenes.aspx", "imagenes/loading.gif", t, "El Examen fue Guardado Correctamente", this);
                    }
                }
            }
            else
            {
                Alert.ShowAlertError(mensaje, this);
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
            bool correct = true;
            try
            {
                if (!File.Exists(sourcefilename))
                {
                    Alert.ShowAlertError("No existe la ruta " + sourcefilename + ", verifiquelo en el departamento de sistemas.", this);
                    correct = false;
                }
                if (File.Exists(sourcefilename) && !File.Exists(destfilename))
                {
                    File.Copy(sourcefilename, destfilename, true);
                    correct = true;
                }

                return correct;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
                correct = false;
                return correct;
            }
        }

        protected void gridPapeleria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridPapeleria.DataKeys[index].Values["ruta"].ToString();
            string nombre = gridPapeleria.DataKeys[index].Values["nombre"].ToString();
            string extension = gridPapeleria.DataKeys[index].Values["extension"].ToString();
            string descripcion = gridPapeleria.DataKeys[index].Values["descripcion"].ToString();
            string id_archi = gridPapeleria.DataKeys[index].Values["id_archi"].ToString();
            Session["id_archivo"] = id_archi;
            Session["descripcion"] = descripcion;
            Session["id_archi"] = id_archi;
            DataTable papeleria = (DataTable)Session["papeleria_perfil"];
            int papeleriat = papeleria.Rows.Count;
            switch (e.CommandName)
            {
                case "Eliminar":
                    List<DataRow> rowsToDelete = new List<DataRow>();
                    foreach (DataRow row in papeleria.Rows)
                    {
                        if (row["nombre"].ToString().Equals(nombre) || row["descripcion"].ToString().Equals(descripcion))
                        {
                            rowsToDelete.Add(row);
                        }
                    }
                    foreach (DataRow rowde in rowsToDelete)
                    {
                        papeleria.Rows.Remove(rowde);
                    }
                    Session["papeleria_perfil"] = papeleria;
                    gridPapeleria.DataSource = papeleria;
                    gridPapeleria.DataBind();

                    break;

                case "Descargar":
                    Download(ruta, nombre);
                    break;

                case "Editar":
                    // List<DataRow> rowsToDelete2 = new List<DataRow>();
                    foreach (DataRow row in papeleria.Rows)
                    {
                        if (row["nombre"].ToString().Equals(nombre) || row["descripcion"].ToString().Equals(descripcion))
                        {
                            //rowsToDelete2.Add(row);
                            txtNombreArchivo.Text = row["descripcion"].ToString();
                            break;
                        }
                    }
                    //foreach (DataRow rowde in rowsToDelete2)
                    //{
                    //    papeleria.Rows.Remove(rowde);
                    //}
                    lnkGuardarPape.Text = "Editar  <i class='fa fa-plus - circle'></i>";
                    Session["caso_guarda"] = "Edit";
                    break;
            }
        }

        protected void lnkGuardarPape_Click(object sender, EventArgs e)
        {
            bool error = false;

            string id_archi = "0";
            if (Session["id_archi"] != null) { id_archi = (string)Session["id_archi"]; }
            if (txtNombreArchivo.Text == "") { error = true; Alert.ShowAlertError("Ingrese una descripcion para el documento.", this); }
            string caso_guarda = "";
            if (!fupPapeleria.HasFile)
            {
                error = true; Alert.ShowAlertError("Debe Seleccionar un archivo.", this);
            }
            if (Session["caso_guarda"] != null)
            {
                caso_guarda = (string)Session["caso_guarda"];
            }
            Random random = new Random();
            int randomNumber = random.Next(0, 1000);
            switch (caso_guarda)
            {
                case "":
                    if (fupPapeleria.HasFile && error == false)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/examenes/"));//path local
                        string mensaje = AddPapeleriaToTable(dirInfo + randomNumber.ToString() + fupPapeleria.FileName, fupPapeleria.FileName, Path.GetExtension(fupPapeleria.FileName).ToString(), txtNombreArchivo.Text.ToUpper(), id_archi);
                        if (mensaje.Equals(string.Empty))
                        {
                            bool pape = UploadFile(fupPapeleria, dirInfo + randomNumber.ToString() + fupPapeleria.FileName);
                            if (pape == false)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + lnkGuardarPape.ClientID.ToString() + "');", true);
                                //Alert.ShowAlert("Archivo Subido Correctamente", "Mensaje del Sistema", this);
                                // Alert.ShowGiftRedirect("Estamos subiendo el archivo al servidor.","Espere un Momento","","imagenes/loading.gif","2000","Archivo Guardardo Correctamente",this);
                                Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "5000", "Archivo Guardardo Correctamente", this);
                                //agregamos a tabla global de papelera
                                fupPapeleria.Visible = true;
                            }
                        }
                        else
                        {
                            Alert.ShowAlertError(mensaje, this);
                        }
                    }
                    break;

                case "Edit":
                    string id_archivo = (string)Session["id_archivo"];
                    if (fupPapeleria.HasFile && error == false)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/examenes/"));//path local
                        string mensaje = AddPapeleriaToTable(dirInfo + randomNumber.ToString() + fupPapeleria.FileName, fupPapeleria.FileName, Path.GetExtension(fupPapeleria.FileName).ToString(), txtNombreArchivo.Text.ToUpper(), id_archi);
                        if (mensaje.Equals(string.Empty))
                        {
                            bool pape = UploadFile(fupPapeleria, dirInfo + randomNumber.ToString() + fupPapeleria.FileName);
                            if (pape == false)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + lnkGuardarPape.ClientID.ToString() + "');", true);
                                Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "5000", "Archivo Guardardo Correctamente", this);
                                fupPapeleria.Visible = true;
                            }
                        }
                        else
                        {
                            Alert.ShowAlertError(mensaje, this);
                        }
                    }
                    Session["descripcion"] = null;
                    lnkGuardarPape.Text = "Guardar <i class='fa fa-plus - circle'></i>";
                    break;
            }
        }

        /// <summary>
        /// Agrega filas tabla de papeleria global (INCLUYE ELECTOR Y LICENCIA)
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="nombre"></param>
        public string AddPapeleriaToTable(string ruta, string nombre, string extension, string descripcion, string id_archi)
        {
            string mensaje = "";
            bool exists = false;
            DataTable papeleria = (DataTable)Session["papeleria_perfil"];
            string descripcion_session = null;
            if (Session["descripcion"] != null)
            {
                descripcion_session = Session["descripcion"].ToString();
            }

            if (descripcion_session == null)//si no es edicion
            {
                foreach (DataRow check in papeleria.Rows)
                {
                    if (check["nombre"].Equals(nombre) && check["id_archi"].Equals(id_archi))
                    {
                        exists = true;
                        mensaje = check["descripcion"].ToString() + " existente. Elimine el anterior si desea actualizarlo.";
                        break;
                    }
                }
                if (exists == false)
                {
                    DataRow new_row = papeleria.NewRow();
                    new_row["nombre"] = nombre;
                    new_row["ruta"] = ruta;
                    new_row["extension"] = extension;
                    new_row["descripcion"] = descripcion;
                    new_row["id_archi"] = id_archi;
                    papeleria.Rows.Add(new_row);
                    gridPapeleria.DataSource = papeleria;
                    gridPapeleria.DataBind();
                    Session["papeleria_perfil"] = papeleria;
                    txtNombreArchivo.Text = "";
                }
            }
            else//mientra sea una edicion
            {
                foreach (DataRow check in papeleria.Rows)
                {
                    if (check["nombre"].Equals(nombre) && check["id_archi"].Equals(id_archi))
                    {
                        exists = true;
                        mensaje = check["descripcion"].ToString() + " existente. Elimine el anterior si desea actualizarlo.";
                        break;
                    }
                }
                foreach (DataRow row in papeleria.Rows)
                {
                    if (row["descripcion"].Equals(descripcion_session))
                    {
                        row["nombre"] = nombre;
                        row["ruta"] = ruta;
                        row["extension"] = extension;
                        row["descripcion"] = descripcion;
                        row["id_archi"] = id_archi;
                        gridPapeleria.DataSource = papeleria;
                        gridPapeleria.DataBind();
                        Session["papeleria_perfil"] = papeleria;
                        txtNombreArchivo.Text = "";
                    }
                }
            }
            Session["descripcion"] = null;
            Session["id_archi"] = null;
            return mensaje;
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
                return false;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
                return true;
            }
        }

        /// <summary>
        /// Retorna la cadena de la tabla de documentos
        /// </summary>
        /// <returns></returns>
        private string GeneraCadenaDocumentos()
        {
            string cadena = "";
            DataTable papeleria = (DataTable)Session["papeleria_perfil"];
            foreach (DataRow row in papeleria.Rows)
            {
                cadena = cadena + row["descripcion"] + ";" + row["extension"] + ";" + row["id_archi"] + ";" + row["ruta"] + ";";
            }
            return cadena;
        }

        /// <summary>
        /// Retorna el total de la cadena de documentos
        /// </summary>
        /// <returns></returns>
        private int TotalCadenaDocumentos()
        {
            DataTable papeleria = (DataTable)Session["papeleria_perfil"];
            return papeleria.Rows.Count;
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
            // Response.End();
        }
    }
}