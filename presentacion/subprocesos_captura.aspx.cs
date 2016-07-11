using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class subprocesos_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 1000000000);
                hiddenvalue.Value = randomNumber.ToString();
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_subproceso");
                dt.Columns.Add("descripcion");
                dt.Columns.Add("url");
                dt.Columns.Add("orden");
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("idc_subproceso");
                dt2.Columns.Add("descripcion");
                dt2.Columns.Add("idc_puestoperfil");
                DataTable dt3 = new DataTable();
                dt3.Columns.Add("idc_proceso");
                dt3.Columns.Add("observaciones");
                dt3.Columns.Add("url");
                dt3.Columns.Add("extension");
                dt3.Columns.Add("idc_procesosarc");
                Session[hiddenvalue.Value + "table_files"] = dt3;
                Session[hiddenvalue.Value + "table_perfiles"] = dt2;
                Session[hiddenvalue.Value + "table_subprocesos"] = dt;
                Session[hiddenvalue.Value + "idc_subproceso"] = null;
                Session[hiddenvalue.Value + "descripcion_subp"] = null;
                Session[hiddenvalue.Value + "orden_subp"] = null;
                Session[hiddenvalue.Value + "url_subp"] = null;
                Session[hiddenvalue.Value + "idc_proceso"] = null;
                int idc_proceso = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_proceso"]));
                Session[hiddenvalue.Value + "idc_proceso"] = idc_proceso;
                string tipo = Request.QueryString["type"];
                CargarPerfiles("");
                if (idc_proceso > 0)
                {
                    CargarProcesos(idc_proceso, tipo);
                }
                lnltipo.Text = tipo == "B" ? "Borrador" : "Produccion";
            }
        }

        private void CargarPerfiles(string filtro)
        {
            ProcesosENT enti = new ProcesosENT();
            enti.Ptioo = filtro;
            ProcesosCOM com = new ProcesosCOM();
            DataSet ds = com.CargaPerfiles(enti);
            cbxlperfiles.DataTextField = "descripcion";
            cbxlperfiles.DataValueField = "idc_puestoperfil";
            cbxlperfiles.DataSource = ds.Tables[0];
            cbxlperfiles.DataBind();
        }

        private void CargarProcesos(int idc_proceso, string tipo)
        {
            try
            {
                ProcesosENT enti = new ProcesosENT();
                enti.Pidc_proceso = idc_proceso;
                ProcesosCOM com = new ProcesosCOM();
                DataSet ds = tipo == "B" ? com.CatalogoProcesosbORR(enti) : com.CatalogoProcesos(enti);
                lblname.Text = "Sub Procesos";
                txtdescproceso.Text = ds.Tables[0].Rows[0]["descripcion"].ToString();
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/html_files/"));//path local

                Random random_edit = new Random();
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    int randomNumber_live = random_edit.Next(0, 100000000);
                    int randomNumber_live2 = random_edit.Next(0, 100000000);
                    int randomNumber_live3 = random_edit.Next(0, 100000000);
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    string file = row["url"].ToString() != "" ? dirInfo + randomNumber_live.ToString() + randomNumber_live2.ToString() + randomNumber_live3.ToString() + date + ".html" : "";
                    if (file != "")
                    {
                        funciones.CopiarArchivos(row["url"].ToString(), file, this);
                    }
                    AddtoTable(Convert.ToInt32(row["idc_subproceso"]), row["descripcion"].ToString(), Convert.ToInt32(row["orden"]), file);
                }
                foreach (DataRow row in ds.Tables[3].Rows)
                {
                    AddtoTablePerfiles(Convert.ToInt32(row["idc_subproceso"]), row["descripcion"].ToString(), Convert.ToInt32(row["idc_puestoperfil"]));
                }
                foreach (DataRow row in ds.Tables[2].Rows)
                {
                    AddtoTableFiles(Convert.ToInt32(row["idc_procesosarc"]), Convert.ToInt32(row["idc_proceso"]), row["observaciones"].ToString(), row["extension"].ToString(), row["url"].ToString());
                }
                CargarGrid();
                CargarGridFiles();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
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

        /// <summary>
        /// Carga grid de subprocesos
        /// </summary>
        private void CargarGrid()
        {
            ChangeOrden();
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
            DataTable tablecopy = dt.Copy();
            grid_subprocesos.DataSource = tablecopy;
            grid_subprocesos.DataBind();
        }

        /// <summary>
        /// Carga grid de archivos
        /// </summary>
        private void CargarGridFiles()
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_files"];
            DataTable tablecopy = dt.Copy();
            gridarchivos.DataSource = tablecopy;
            gridarchivos.DataBind();
        }

        /// <summary>
        /// Muestra la lista de perfiles por subproceso
        /// </summary>
        /// <param name="idc_subproceso"></param>
        /// <param name="descripcion"></param>
        private void CargarListaPerfiles(int idc_subproceso, string descripcion)
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
            DataView view = dt.DefaultView;
            //filtramos por subproceso
            view.RowFilter = "idc_subproceso = " + idc_subproceso + " and descripcion = '" + descripcion + "'";

            foreach (DataRow row in view.ToTable().Rows)
            {
                int idc_puestoperfil = Convert.ToInt32(row["idc_puestoperfil"]);
                foreach (ListItem item in cbxlperfiles.Items)
                {
                    int idc_pp = Convert.ToInt32(item.Value);
                    if (idc_pp == idc_puestoperfil)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        /// <summary>
        /// Agrega fila a archivos
        /// </summary>
        /// <param name="idc_procesosarc"></param>
        /// <param name="idc_proceso"></param>
        /// <param name="observaciones"></param>
        /// <param name="extension"></param>
        /// <param name="url"></param>
        private void AddtoTableFiles(int idc_procesosarc, int idc_proceso, string observaciones, string extension, string url)
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_files"];
            DeleteToTableFiles(observaciones, url);
            DataRow row = dt.NewRow();
            row["idc_proceso"] = idc_proceso;
            row["observaciones"] = observaciones;
            row["extension"] = extension;
            row["idc_procesosarc"] = idc_procesosarc;
            row["url"] = url;
            dt.Rows.Add(row);
            Session[hiddenvalue.Value + "table_files"] = dt;
        }

        /// <summary>
        /// Elimina fila de tabla archivos
        /// </summary>
        /// <param name="observaciones"></param>
        /// <param name="url"></param>
        private void DeleteToTableFiles(string observaciones, string url)
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_files"];
            foreach (DataRow rows in dt.Rows)
            {
                string value = rows["observaciones"].ToString();
                string valueidc = rows["url"].ToString();
                //para nuevos registros
                if (valueidc == url && observaciones == value)
                {
                    rows.Delete();
                    break;
                }
            }
            Session[hiddenvalue.Value + "table_files"] = dt;
        }

        /// <summary>
        /// Agrega fila a tabla, si existe un registro similar lo reemplaza
        /// </summary>
        /// <param name="idc_subproceso"></param>
        /// <param name="descripcion"></param>
        /// <param name="orden"></param>
        /// <param name="url"></param>
        private void AddtoTable(int idc_subproceso, string descripcion, int orden, string url)
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
            DeleteToTable(idc_subproceso, descripcion);
            DataRow row = dt.NewRow();
            row["idc_subproceso"] = idc_subproceso;
            row["descripcion"] = descripcion;
            row["orden"] = orden;
            row["url"] = url;
            dt.Rows.Add(row);
            Session[hiddenvalue.Value + "table_subprocesos"] = dt;
        }

        /// <summary>
        /// Agrega fila atabla perfiles
        /// </summary>
        /// <param name="idc_subproceso"></param>
        /// <param name="descripcion"></param>
        /// <param name="orden"></param>
        /// <param name="url"></param>
        private void AddtoTablePerfiles(int idc_subproceso, string descripcion, int idc_puestoperfil)
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
            DeleteToTablePerfiles(idc_subproceso, descripcion, idc_puestoperfil);
            DataRow row = dt.NewRow();
            row["idc_subproceso"] = idc_subproceso;
            row["descripcion"] = descripcion;
            row["idc_puestoperfil"] = idc_puestoperfil;
            dt.Rows.Add(row);
            Session[hiddenvalue.Value + "table_perfiles"] = dt;
        }

        /// <summary>
        /// Elimina registro de tabla
        /// </summary>
        /// <param name="idc_subproceso"></param>
        /// <param name="descripcion"></param>
        private void DeleteToTable(int idc_subproceso, string descripcion)
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
            foreach (DataRow rows in dt.Rows)
            {
                string value = rows["descripcion"].ToString();
                int valueidc = Convert.ToInt32(rows["idc_subproceso"]);
                //para nuevos registros
                if (descripcion == value && idc_subproceso == 0)
                {
                    rows.Delete();
                    break;
                }
                //para registros anteriores
                if (idc_subproceso == valueidc && idc_subproceso > 0)
                {
                    rows.Delete();
                    break;
                }
            }
            Session[hiddenvalue.Value + "table_subprocesos"] = dt;
        }

        /// <summary>
        /// Elimina fial d etabla perfiles por id
        /// </summary>
        /// <param name="idc_subproceso"></param>
        /// <param name="descripcion"></param>
        private void DeleteToTablePerfiles(int idc_subproceso, string descripcion, int idc_puestoperfil)
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
            foreach (DataRow rows in dt.Rows)
            {
                string value = rows["descripcion"].ToString();
                int valueidc = Convert.ToInt32(rows["idc_subproceso"]);
                int idc_pp = Convert.ToInt32(rows["idc_puestoperfil"]);
                //para nuevos registros
                if (descripcion == value && idc_subproceso == 0 && idc_pp == idc_puestoperfil)
                {
                    rows.Delete();
                    break;
                }
                //para registros anteriores
                if (idc_subproceso == valueidc && idc_subproceso > 0 && idc_pp == idc_puestoperfil)
                {
                    rows.Delete();
                    break;
                }
            }
            Session[hiddenvalue.Value + "table_perfiles"] = dt;
        }

        /// <summary>
        /// elimina todo sobre id
        /// </summary>
        /// <param name="idc_subproceso"></param>
        /// <param name="descripcion"></param>
        /// <param name="idc_puestoperfil"></param>
        private void DeleteToTablePerfilesTodas(int idc_subproceso, string descripcion)
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
            List<DataRow> rowsToDelete = new List<DataRow>();
            foreach (DataRow rows in dt.Rows)
            {
                string value = rows["descripcion"].ToString();
                int valueidc = Convert.ToInt32(rows["idc_subproceso"]);
                int idc_pp = Convert.ToInt32(rows["idc_puestoperfil"]);
                //para nuevos registros
                if (descripcion == value && idc_subproceso == 0)
                {
                    rowsToDelete.Add(rows);
                }
                //para registros anteriores
                if (idc_subproceso == valueidc && idc_subproceso > 0)
                {
                    rowsToDelete.Add(rows);
                }
            }
            foreach (DataRow row in rowsToDelete)
            {
                dt.Rows.Remove(row);
            }
            Session[hiddenvalue.Value + "table_perfiles"] = dt;
        }

        private void UpdateTablePerfiles(int idc_subproceso, string descripcion, int idc_puestoperfil)
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
            foreach (DataRow rows in dt.Rows)
            {
                string value = rows["descripcion"].ToString();
                int valueidc = Convert.ToInt32(rows["idc_subproceso"]);
                //para nuevos registros
                if (descripcion == value && idc_subproceso == 0)
                {
                    rows["idc_subproceso"] = descripcion;
                    rows["idc_puestoperfil"] = idc_puestoperfil;
                    rows["descripcion"] = descripcion;
                    break;
                }
                //para registros anteriores
                if (idc_subproceso == valueidc && idc_subproceso > 0)
                {
                    rows["idc_subproceso"] = descripcion;
                    rows["idc_puestoperfil"] = idc_puestoperfil;
                    rows["descripcion"] = descripcion;
                    break;
                }
            }
            Session[hiddenvalue.Value + "table_perfiles"] = dt;
        }

        /// <summary>
        /// Actualiza url registro de tabla
        /// </summary>
        /// <param name="idc_subproceso"></param>
        /// <param name="descripcion"></param>
        private void UpdateURLHTMLToTable(int idc_subproceso, string descripcion, string url)
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
            foreach (DataRow rows in dt.Rows)
            {
                string value = rows["descripcion"].ToString();
                int valueidc = Convert.ToInt32(rows["idc_subproceso"]);
                //para nuevos registros
                if (descripcion == value && idc_subproceso == 0)
                {
                    rows["url"] = url;
                    break;
                }
                //para registros anteriores
                if (idc_subproceso == valueidc && idc_subproceso > 0)
                {
                    rows["url"] = url;
                    break;
                }
            }
            Session[hiddenvalue.Value + "table_subprocesos"] = dt;
        }

        /// <summary>
        /// Cambia el orden de la tabla segun sun row number
        /// </summary>
        private void ChangeOrden()
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
            DataView view = dt.DefaultView;
            view.Sort = "orden asc";
            DataTable sortedDT = view.ToTable();
            int i = 0;
            int index = 0;
            foreach (DataRow row in sortedDT.Rows)
            {
                index = i;
                row["orden"] = index + 1;
                i++;
            }
            Session[hiddenvalue.Value + "table_subprocesos"] = sortedDT;
        }

        /// <summary>
        /// Eegresa el valor maximo d ela tabla
        /// </summary>
        /// <returns></returns>
        private int GetValueMax()
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
            DataView view = dt.DefaultView;
            view.Sort = "orden desc";
            DataTable sortedDT = view.ToTable();
            return sortedDT.Rows.Count > 0 ? Convert.ToInt32(sortedDT.Rows[0]["orden"]) : 0;
        }

        protected void gridarchivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridarchivos.DataKeys[index].Values["url"].ToString();
            string observaciones = gridarchivos.DataKeys[index].Values["observaciones"].ToString();
            string nombre = Path.GetFileName(ruta);
            switch (e.CommandName)
            {
                case "Descargar":
                    Download(ruta, nombre);
                    break;

                case "eliminar":
                    DeleteToTableFiles(observaciones, ruta);
                    CargarGridFiles();
                    Alert.ShowAlert("Elemento eliminado correctamente", "Mensaje del Sistema", this);
                    break;
            }
        }

        /// <summary>
        /// Cadena de subprocesos
        /// </summary>
        /// <returns></returns>
        private String CadenaSubProcesos()
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
            string cadena = "";
            foreach (DataRow row in dt.Rows)
            {
                cadena = cadena + row["idc_subproceso"].ToString() + ";" + row["descripcion"].ToString() + ";" + row["orden"].ToString() + ";" + row["url"].ToString() + ";";
            }
            return cadena;
        }

        /// <summary>
        /// Cadena perfil-subproceso
        /// </summary>
        /// <returns></returns>
        private String CadenaPerfiles()
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
            string cadena = "";
            foreach (DataRow row in dt.Rows)
            {
                cadena = cadena + row["idc_subproceso"].ToString() + ";" + row["descripcion"].ToString() + ";" + row["idc_puestoperfil"].ToString() + ";";
            }
            return cadena;
        }

        /// <summary>
        /// Cadena de archivos del proceso principal
        /// </summary>
        /// <returns></returns>
        private String Cadena()
        {
            string cadena = "";

            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_files"];
            foreach (DataRow row in dt.Rows)
            {
                cadena = cadena + row["idc_procesosarc"].ToString() + ";" + row["extension"].ToString() + ";" + row["observaciones"].ToString() + ";" + row["url"].ToString() + ";";
            }
            return cadena;
        }

        /// <summary>
        /// YTotal cadena de archivos del proceso principal
        /// </summary>
        /// <returns></returns>
        private int TotalCadena()
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_files"];
            return dt.Rows.Count;
        }

        /// <summary>
        /// YTotal cadena subprocesos
        /// </summary>
        /// <returns></returns>
        private int TotalCadenaSubProcesos()
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
            return dt.Rows.Count;
        }

        /// <summary>
        /// Total cadena relacion perfiles-subprocesos
        /// </summary>
        /// <returns></returns>
        private int TotalCadenaPerfiles()
        {
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
            return dt.Rows.Count;
        }

        protected void lnkagregar_Click(object sender, EventArgs e)
        {
            if (txtsubproceso.Text == "")
            {
                Alert.ShowAlertError("Escriba una descripcion para el subproceso", this);
            }
            else
            {
                int idc_subproceso = Session[hiddenvalue.Value + "idc_subproceso"] == null ? 0 : Convert.ToInt32(Session[hiddenvalue.Value + "idc_subproceso"]);
                string descrp_anterior = Session[hiddenvalue.Value + "descripcion_subp"] == null ? "" : (string)Session[hiddenvalue.Value + "descripcion_subp"];
                int orden = Session[hiddenvalue.Value + "orden_subp"] == null ? GetValueMax() + 1 : Convert.ToInt32(Session[hiddenvalue.Value + "orden_subp"]);
                string url = Session[hiddenvalue.Value + "url_subp"] == null ? "" : (string)Session[hiddenvalue.Value + "url_subp"];
                DeleteToTable(idc_subproceso, descrp_anterior);
                AddtoTable(idc_subproceso, txtsubproceso.Text, orden, url);
                DeleteToTablePerfilesTodas(idc_subproceso, descrp_anterior);
                foreach (ListItem item in cbxlperfiles.Items)
                {
                    int idc_puestoperfil = Convert.ToInt32(item.Value);
                    if (item.Selected == true)
                    {
                        AddtoTablePerfiles(idc_subproceso, txtsubproceso.Text, idc_puestoperfil);
                    }
                }
                txtsubproceso.Text = "";
                Session[hiddenvalue.Value + "idc_subproceso"] = null;
                Session[hiddenvalue.Value + "descripcion_subp"] = null;
                Session[hiddenvalue.Value + "orden_subp"] = null;
                Session[hiddenvalue.Value + "url_subp"] = null;
                CargarGrid();
                CargarPerfiles("");
                Alert.ShowAlert("Elemento Guardado Correctamente", "Mensaje del Sistema", this);
            }
        }

        protected void grid_subprocesos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_subproceso = Convert.ToInt32(grid_subprocesos.DataKeys[index].Values["idc_subproceso"]);
            string descripcion = grid_subprocesos.DataKeys[index].Values["descripcion"].ToString();
            string orden = grid_subprocesos.DataKeys[index].Values["orden"].ToString();
            string url = grid_subprocesos.DataKeys[index].Values["url"].ToString();
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
            foreach (DataRow rows in dt.Rows)
            {
                string value = rows["descripcion"].ToString();
                int valueidc = Convert.ToInt32(rows["idc_subproceso"]);
                //para nuevos registros
                if (descripcion == value && idc_subproceso == valueidc)
                {
                    Session[hiddenvalue.Value + "idc_subproceso"] = rows["idc_subproceso"].ToString();
                    Session[hiddenvalue.Value + "descripcion_subp"] = rows["descripcion"].ToString();
                    Session[hiddenvalue.Value + "orden_subp"] = rows["orden"].ToString();
                    Session[hiddenvalue.Value + "url_subp"] = rows["url"].ToString();
                    descripcion = rows["descripcion"].ToString();
                    orden = rows["orden"].ToString();
                    url = rows["url"].ToString();
                    break;
                }
            }
            switch (e.CommandName)
            {
                case "eliminar":
                    DeleteToTable(idc_subproceso, descripcion);
                    Session[hiddenvalue.Value + "idc_subproceso"] = null;
                    Session[hiddenvalue.Value + "descripcion_subp"] = null;
                    Session[hiddenvalue.Value + "orden_subp"] = null;
                    Session[hiddenvalue.Value + "url_subp"] = null;
                    CargarGrid();
                    Alert.ShowAlert("Elemento eliminado correctamente", "Mensaje del Sistema", this);
                    break;

                case "editar":
                    CargarPerfiles("");
                    txtsubproceso.Text = descripcion;
                    CargarListaPerfiles(idc_subproceso, descripcion);
                    break;

                case "view_file":
                    if (url == "")
                    {
                        Alert.ShowAlertError("No hay archivo relacionado", this);
                    }
                    else
                    {
                        string @path = funciones.ConvertStringToHex(url);
                        String urlgo = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                        String path_actual = urlgo.Substring(urlgo.LastIndexOf("/") + 1);
                        urlgo = urlgo.Replace(path_actual, "");
                        urlgo = urlgo + "view_files.aspx?file=" + @path;
                        ScriptManager.RegisterStartupScript(this, GetType(), "noti5wdwdwdwd33W3", "window.open('" + urlgo + "');", true);
                    }
                    Session[hiddenvalue.Value + "idc_subproceso"] = null;
                    Session[hiddenvalue.Value + "descripcion_subp"] = null;
                    Session[hiddenvalue.Value + "orden_subp"] = null;
                    Session[hiddenvalue.Value + "url_subp"] = null;
                    break;

                case "add_file":

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessagearchivo", "ModalArchi('" + descripcion + "');", true);
                    break;
            }
        }

        protected void txtOrder_TextChanged(object sender, EventArgs e)
        {
            GridView grid = (GridView)((TextBox)sender).Parent.Parent.Parent.Parent.Parent.Parent;
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;
            TextBox txt = (TextBox)currentRow.FindControl("txtOrder");
            int index = Convert.ToInt32(currentRow.RowIndex);
            int idc_subproceso = Convert.ToInt32(grid.DataKeys[index].Values["idc_subproceso"]);
            string descripcion = grid.DataKeys[index].Values["descripcion"].ToString();
            DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
            int new_value = Convert.ToInt32(txt.Text);
            //cambiasmos el registro
            foreach (DataRow rows in dt.Rows)
            {
                string value = rows["descripcion"].ToString();
                int valueidc = Convert.ToInt32(rows["idc_subproceso"]);
                //para nuevos registros
                if (descripcion == value && idc_subproceso == valueidc)
                {
                    rows["orden"] = new_value;
                    break;
                }
            }
            //cambiamos los demas registros si son mayores o iguales
            foreach (DataRow rows in dt.Rows)
            {
                string value = rows["descripcion"].ToString();
                int valueidc = Convert.ToInt32(rows["idc_subproceso"]);
                int orden = Convert.ToInt32(rows["orden"]);
                if (orden >= new_value && descripcion != value)
                {
                    rows["orden"] = orden + 1;
                    break;
                }
            }
            Session[hiddenvalue.Value + "table_subprocesos"] = dt;
            CargarGrid();
        }

        protected void grid_subprocesos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView grid = (GridView)sender;
            TextBox txt = (TextBox)e.Row.FindControl("txtOrder");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string texto = rowView["orden"].ToString();
                txt.Text = texto;
            }
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            int idc_subproceso = Session[hiddenvalue.Value + "idc_subproceso"] == null ? 0 : Convert.ToInt32(Session[hiddenvalue.Value + "idc_subproceso"]);
            string descripcion = Session[hiddenvalue.Value + "descripcion_subp"] == null ? "" : (string)Session[hiddenvalue.Value + "descripcion_subp"];
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/html_files/"));//path local
            string url = Session[hiddenvalue.Value + "url_subp"] == null ? "" : (string)Session[hiddenvalue.Value + "url_subp"];
            if (url == "")
            {
                Random random_edit = new Random();
                int randomNumber_live = random_edit.Next(0, 1000);
                DateTime localDate = DateTime.Now;
                string date = localDate.ToString();
                date = date.Replace("/", "_");
                date = date.Replace(":", "_");
                StreamWriter file_edit = new StreamWriter(dirInfo.ToString() + randomNumber_live.ToString() + date + ".html");
                file_edit.Write("");
                file_edit.Close();
                UpdateURLHTMLToTable(idc_subproceso, descripcion, dirInfo.ToString() + randomNumber_live.ToString() + date + ".html");
                url = dirInfo.ToString() + randomNumber_live.ToString() + date + ".html";
            }

            string queryurl = "&edit_htmlfile=" + funciones.deTextoa64(url) + "&descripcion=" + funciones.deTextoa64(descripcion) + "&idc_subproceso=" + funciones.deTextoa64(idc_subproceso.ToString());
            String urlgo = HttpContext.Current.Request.Url.AbsoluteUri;
            String path_actual = urlgo.Substring(urlgo.LastIndexOf("/") + 1);
            urlgo = urlgo.Replace(path_actual, "");
            urlgo = urlgo + "html_dinamico.aspx?edit_live=true&dinamic_id=" + funciones.deTextoa64(hiddenvalue.Value) + queryurl;
            ScriptManager.RegisterStartupScript(this, GetType(), "noti533W3", "window.open('" + urlgo + "');", true);
        }

        private void CreateFile(string name, string path)
        {
            try
            {
                File.Create(path + name);
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void lnkDescargarArchi_Click(object sender, EventArgs e)
        {
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                ProcesosENT entidad = new ProcesosENT();
                ProcesosCOM com = new ProcesosCOM();
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pidc_proceso = Convert.ToInt32(Session[hiddenvalue.Value + "idc_proceso"]);
                entidad.Pcadenaperf = CadenaPerfiles();
                entidad.Ptotalcadenaperf = TotalCadenaPerfiles();
                entidad.Pcadenasubpro = CadenaSubProcesos();
                entidad.Ptotalcadenasub = TotalCadenaSubProcesos();
                entidad.Pdescripcion = txtdescproceso.Text.ToUpper();
                entidad.Pcadena = Cadena();
                entidad.Ptotalcadena = TotalCadena();
                DataSet ds = new DataSet();
                ds = com.AgregarSubProcesos(entidad);
                string vmensaje = "";
                vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje == "")
                {
                    DataTable tabla_archivos = ds.Tables[1];
                    bool correct = true;
                    foreach (DataRow row_archi in tabla_archivos.Rows)
                    {
                        string ruta_det = row_archi["ruta_destino"].ToString();
                        string ruta_origen = row_archi["ruta_origen"].ToString();
                        correct = funciones.CopiarArchivos(ruta_origen, ruta_det, this.Page);
                        if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo " + ruta_origen + "a la ruta " + ruta_det, this); }
                    }
                    int total = (((tabla_archivos.Rows.Count) * 1) + 1) * 1000;
                    string t = total.ToString();
                    string url_back = "catalogo_procesos.aspx";
                    Alert.ShowGiftMessage("Estamos procesando la cantidad de " + tabla_archivos.Rows.Count.ToString() + " archivo(s) al Servidor.", "Espere un Momento", url_back, "imagenes/loading.gif", t, "Los SubProcesos fueron guardados correctamente", this);
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar este Manual de Proceso','modal fade modal-info');", true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("catalogo_procesos.aspx");
        }

        protected void lnkaddfile_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fupPapeleria.HasFile)
                {
                    Alert.ShowAlertError("Debe elegir un archivo", this);
                }
                else if (txtobsrarchivo.Text == "")
                {
                    Alert.ShowAlertError("Debe escribir una observacion para el archivo", this);
                }
                else
                {
                    Random random = new Random();
                    int randomNumber = random.Next(0, 100000000);
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/tareas/"));//path local
                    AddtoTableFiles(0, Convert.ToInt32(Session[hiddenvalue.Value + "idc_proceso"]), txtobsrarchivo.Text, Path.GetExtension(fupPapeleria.FileName), dirInfo + randomNumber.ToString() + Path.GetExtension(fupPapeleria.FileName));
                    bool pape = funciones.UploadFile(fupPapeleria, dirInfo + randomNumber.ToString() + Path.GetExtension(fupPapeleria.FileName), this.Page);
                    if (pape == true)
                    {
                        CargarGridFiles();
                        txtobsrarchivo.Text = "";
                        Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "2000", "Comentario Guardardo Correctamente", this);
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
    }
}