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
    public partial class empleados_faltas_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                int idc_empleado_falta = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleado_falta"]));
                CargarFaltas(idc_empleado_falta);
                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("descripcion");
                papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("extension");
                papeleria.Columns.Add("nombre");
                Session["papeleria"] = papeleria;
            }
        }

        private void CargarFaltas(int idc_empleado_falta)
        {
            try
            {
                FaltasENT entidad = new FaltasENT();
                FaltasCOM componente = new FaltasCOM();
                entidad.Pidc_empleado_falta = idc_empleado_falta;
                entidad.Pidc_puesto = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
                DataSet ds = componente.CargaPrep(entidad);
                lblEmpleado.Text = ds.Tables[0].Rows[0]["empleado"].ToString();
                lblPuesto.Text = ds.Tables[0].Rows[0]["puesto"].ToString();
                lblfecha.Text = ds.Tables[0].Rows[0]["fecha"].ToString();
                string rutaimagen = funciones.GenerarRuta("fot_emp", "rw_carpeta");
                var domn = Request.Url.Host;
                if (domn == "localhost")
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
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void lbkasistencia_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            lnk.CssClass = lnk.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
        }

        protected void gridPapeleria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridPapeleria.DataKeys[index].Values["ruta"].ToString();
            string nombre = gridPapeleria.DataKeys[index].Values["nombre"].ToString();
            string extension = gridPapeleria.DataKeys[index].Values["extension"].ToString();
            string descripcion = gridPapeleria.DataKeys[index].Values["descripcion"].ToString();
            DataTable papeleria = (DataTable)Session["papeleria"];
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
                    Session["papeleria"] = papeleria;
                    gridPapeleria.DataSource = papeleria;
                    gridPapeleria.DataBind();

                    break;

                case "Descargar":
                    Download(ruta, nombre);
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
                        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/perfiles/"));//path local
                        string mensaje = AddPapeleriaToTable(dirInfo + randomNumber.ToString() + fupPapeleria.FileName, fupPapeleria.FileName, Path.GetExtension(fupPapeleria.FileName).ToString(), txtNombreArchivo.Text.ToUpper());
                        if (mensaje.Equals(string.Empty))
                        {
                            bool pape = UploadFile(fupPapeleria, dirInfo + randomNumber.ToString() + fupPapeleria.FileName);
                            if (pape == false)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + lnkGuardarPape.ClientID.ToString() + "');", true);
                                //Alert.ShowAlert("Archivo Subido Correctamente", "Mensaje del Sistema", this);
                                // Alert.ShowGiftRedirect("Estamos subiendo el archivo al servidor.","Espere un Momento","","imagenes/loading.gif","2000","Archivo Guardardo Correctamente",this);
                                Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "2000", "Archivo Guardardo Correctamente", this);
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
            }
        }

        /// <summary>
        /// Agrega filas tabla de papeleria global (INCLUYE ELECTOR Y LICENCIA)
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="nombre"></param>
        public string AddPapeleriaToTable(string ruta, string nombre, string extension, string descripcion)
        {
            string mensaje = "";
            bool exists = false;
            DataTable papeleria = new DataTable();
            papeleria.Columns.Add("descripcion");
            papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
            papeleria.Columns.Add("ruta");
            papeleria.Columns.Add("extension");
            papeleria.Columns.Add("nombre");
            Session["papeleria"] = papeleria;

            if (exists == false)
            {
                DataRow new_row = papeleria.NewRow();
                new_row["nombre"] = nombre;
                new_row["ruta"] = ruta;
                new_row["extension"] = extension;
                new_row["descripcion"] = descripcion;
                papeleria.Rows.Add(new_row);
                gridPapeleria.DataSource = papeleria;
                gridPapeleria.DataBind();
                Session["papeleria"] = papeleria;
                txtNombreArchivo.Text = "";
            }
            return mensaje;
        }

        /// <summary>
        /// Descarga un archivo
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file_name"></param>
        public void Download(string path, string file_name)
        {
            if (!File.Exists(path))
            {
                Alert.ShowAlertError("No tiene archivo relacionado", this);
            }
            else
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

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                PuestosENT entidad = new PuestosENT();
                entidad.Pidc_empleado_pmd = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_empleado_falta"]));
                entidad.Pobservaciones = txtobservaciones.Text.ToUpper();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Pasistencia = lbkasistencia.CssClass == "btn btn-default btn-block" ? false : true;
                entidad.Pjustificante = lnkjustifica.CssClass == "btn btn-default btn-block" ? false : true;
                DataTable dtr = (DataTable)Session["papeleria"];
                entidad.Pextension = dtr.Rows.Count <= 0 ? "" : dtr.Rows[0]["extension"].ToString();
                PuestosCOM componente = new PuestosCOM();
                DataSet ds = new DataSet();
                string vmensaje = "";
                switch (caso)
                {
                    case "Guardar":
                        ds = componente.EmpleadosFaltas(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;
                }
                if (vmensaje == "")
                {
                    DataTable dt = (DataTable)Session["papeleria"];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row_archi in dt.Rows)
                        {
                            bool correct = true;
                            string ruta = "";
                            ruta = funciones.GenerarRuta("FAL_REV", "unidad");//BORRADOR
                            correct = CopiarArchivos(row_archi["ruta"].ToString(), ruta + ds.Tables[0].Rows[0]["idc_pmd"].ToString() + Path.GetExtension(row_archi["ruta"].ToString()));
                        }
                        Alert.ShowGiftMessage("Estamos procesando la cantidad de 1 archivo(s) al Servidor.", "Espere un Momento", "empleados_faltas.aspx", "imagenes/loading.gif", "2000", "Acción Guardada Correctamente", this);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "AlertGO('Acción Guardada Correctamente','empleados_faltas.aspx');", true);
                    }
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
                    Alert.ShowAlertError("No existe la ruta " + sourcefilename, this);
                    correct = false;
                }
                if (File.Exists(sourcefilename) && sourcefilename != destfilename)
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["papeleria"];
            bool error = false;
            if (dt.Rows.Count <= 0 && lnkjustifica.CssClass == "btn btn-success btn-block")
            {
                Alert.ShowAlertError("Debe ingresar un justificante si maracara esta FALTA como JUSTIFICADA", this);
                error = true;
            }
            if (txtobservaciones.Text == "")
            {
                Alert.ShowAlertError("Debe ingresar observaciones", this);
                error = true;
            }
            if (error == false)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Desea Guardar los cambios de la falta del empleado: " + lblEmpleado.Text.ToUpper() + "');", true);
            }
        }

        protected void lnkcancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("empleados_faltas.aspx");
        }
    }
}