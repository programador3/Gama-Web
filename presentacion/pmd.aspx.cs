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
    public partial class pmd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                CargarDatosEmpleado(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"])));

                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("descripcion");
                papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("extension");
                papeleria.Columns.Add("nombre");
                Session["papeleria"] = papeleria;
            }
        }

        /// <summary>
        /// Carga DATOS DEL EMPLEADO
        /// </summary>
        public void CargarDatosEmpleado(int idc_puesto)
        {
            PuestosENT entidad = new PuestosENT();
            entidad.Idc_Puesto = idc_puesto;//indicamosm que queremos datos de empleado
            PuestosCOM componente = new PuestosCOM();
            DataSet ds = componente.CargaCatologoPuestos(entidad);
            DataRow row = ds.Tables[1].Rows[0];
            lblPuesto.Text = row["puesto"].ToString();
            lblNombre.Text = row["nombre"].ToString();
            lblDepto.Text = row["depto"].ToString();
            lblucursal.Text = row["sucursal"].ToString();
            Session["pidc_empleado_pmd"] = Convert.ToInt32(row["idc_empleado"]);
            string tipopmd = row["tipo_pmd"].ToString();
            Session["tipo_pmd"] = tipopmd;
            GenerarRuta(Convert.ToInt32(row["idc_empleado"]), "fot_emp");
            switch (tipopmd)
            {
                case "R":
                    lbltipo.Text = "El proceso creara un RECONOCIMIENTO";
                    break;

                case "F":
                    lbltipo.Text = "El proceso creara una FALTA";
                    break;

                case "A":
                    lbltipo.Text = "El proceso creara un ACTA ADMINISTRATIVA";
                    break;
            }
        }

        /// <summary>
        /// Genera ruta de imagen
        /// </summary>
        public void GenerarRuta(int id_comprobar, string codigo_imagen)
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
                    case "fot_emp"://fotos de empleados
                        if (domn == "localhost")
                        {
                            var url = "imagenes/btn/default_employed.png";
                            myImage.Src = url;
                            ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                        }
                        else
                        {
                            var url = "http://" + domn + carpeta + id_comprobar + ".jpg";
                            myImage.Src = url;
                            ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                        }
                        break;
                }
            }
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (txtobservaciones.Text == "")
            {
                error = true;
                Alert.ShowAlertError("Debe escribir un motivo", this);
            }
            if (error == false)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Desea Guardar esta Solicitud');", true);
            }
        }

        protected void lnkcancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("puestos_catalogo.aspx");
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                PuestosENT entidad = new PuestosENT();
                entidad.Pidc_pre_empleado = Convert.ToInt32(Session["pidc_empleado_pmd"]);
                entidad.Pobservaciones = txtobservaciones.Text.ToUpper();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Ptipo = (string)Session["tipo_pmd"];

                PuestosCOM componente = new PuestosCOM();
                DataSet ds = new DataSet();
                string vmensaje = "";
                switch (caso)
                {
                    case "Guardar":
                        ds = componente.SolicitudPDM(entidad);
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
                            ruta = funciones.GenerarRuta("PMD", "unidad");//BORRADOR
                            correct = CopiarArchivos(row_archi["ruta"].ToString(), ruta + ds.Tables[0].Rows[0]["idc_pmd"].ToString() + Path.GetExtension(row_archi["ruta"].ToString()));
                        }
                        Alert.ShowGiftMessage("Estamos procesando la cantidad de 1 archivo(s) al Servidor.", "Espere un Momento", "puestos_catalogo.aspx", "imagenes/loading.gif", "2000", "La Solicitud de PDM fue Guardada Correctamente", this);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "AlertGO('Solicitud PMD Guardada correctamente','puestos_catalogo.aspx');", true);
                    }
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
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

        protected void gridPapeleria_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
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
    }
}