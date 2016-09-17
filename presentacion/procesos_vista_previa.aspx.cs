using System;

namespace presentacion
{
    public partial class procesos_vista_previa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["sidc_usuario"] == null)//si no hay session logeamos
            //{
            //    Response.Redirect("login.aspx");
            //}
            //if (!IsPostBack)
            //{
            //    Random random = new Random();
            //    int randomNumber = random.Next(0, 1000000000);
            //    hiddenvalue.Value = randomNumber.ToString();
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add("idc_subproceso");
            //    dt.Columns.Add("descripcion");
            //    dt.Columns.Add("url");
            //    dt.Columns.Add("orden");
            //    DataTable dt2 = new DataTable();
            //    dt2.Columns.Add("idc_subproceso");
            //    dt2.Columns.Add("descripcion");
            //    dt2.Columns.Add("idc_puestoperfil");
            //    DataTable dt3 = new DataTable();
            //    dt3.Columns.Add("idc_proceso");
            //    dt3.Columns.Add("observaciones");
            //    dt3.Columns.Add("url");
            //    dt3.Columns.Add("extension");
            //    dt3.Columns.Add("idc_procesosarc");
            //    Session[hiddenvalue.Value + "table_files"] = dt3;
            //    Session[hiddenvalue.Value + "table_perfiles"] = dt2;
            //    Session[hiddenvalue.Value + "table_subprocesos"] = dt;
            //    Session[hiddenvalue.Value + "idc_subproceso"] = null;
            //    Session[hiddenvalue.Value + "descripcion_subp"] = null;
            //    Session[hiddenvalue.Value + "orden_subp"] = null;
            //    Session[hiddenvalue.Value + "url_subp"] = null;
            //    Session[hiddenvalue.Value + "idc_proceso"] = null;
            //    int idc_proceso = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_proceso"]));
            //    string type = funciones.de64aTexto(Request.QueryString["type"]);
            //    CargarProcesos(idc_proceso, type);
            //}
        }

        //private void CargarProcesos(int idc_proceso, string tipo)
        //{
        //    try
        //    {
        //        ProcesosENT enti = new ProcesosENT();
        //        enti.Pidc_proceso = idc_proceso;
        //        ProcesosCOM com = new ProcesosCOM();
        //        DataSet ds = tipo == "B" ? com.CatalogoProcesosbORR(enti) : com.CatalogoProcesos(enti);
        //        lnltipo.Text = ds.Tables[0].Rows[0]["descripcion"].ToString();
        //        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/html_files/"));//path local

        //        Random random_edit = new Random();
        //        foreach (DataRow row in ds.Tables[1].Rows)
        //        {
        //            int randomNumber_live = random_edit.Next(0, 100000000);
        //            int randomNumber_live2 = random_edit.Next(0, 100000000);
        //            int randomNumber_live3 = random_edit.Next(0, 100000000);
        //            DateTime localDate = DateTime.Now;
        //            string date = localDate.ToString();
        //            date = date.Replace("/", "_");
        //            date = date.Replace(":", "_");
        //            string file = row["url"].ToString() != "" ? dirInfo + randomNumber_live.ToString() + randomNumber_live2.ToString() + randomNumber_live3.ToString() + date + ".html" : "";
        //            if (file != "")
        //            {
        //                funciones.CopiarArchivos(row["url"].ToString(), file, this);
        //            }
        //            AddtoTable(Convert.ToInt32(row["idc_subproceso"]), row["descripcion"].ToString(), Convert.ToInt32(row["orden"]), file);
        //        }
        //        foreach (DataRow row in ds.Tables[3].Rows)
        //        {
        //            AddtoTablePerfiles(Convert.ToInt32(row["idc_subproceso"]), row["descripcion"].ToString(), Convert.ToInt32(row["idc_puestoperfil"]));
        //        }
        //        foreach (DataRow row in ds.Tables[2].Rows)
        //        {
        //            AddtoTableFiles(Convert.ToInt32(row["idc_procesosarc"]), Convert.ToInt32(row["idc_proceso"]), row["observaciones"].ToString(), row["extension"].ToString(), row["url"].ToString());
        //        }
        //        CargarGrid();
        //        CargarGridFiles();
        //    }
        //    catch (Exception ex)
        //    {
        //        Alert.ShowAlertError(ex.ToString(), this.Page);
        //        Global.CreateFileError(ex.ToString(), this);
        //    }
        //}

        ///// <summary>
        ///// Descarga un archivo
        ///// </summary>
        ///// <param name="path"></param>
        ///// <param name="file_name"></param>
        //public void Download(string path, string file_name)
        //{
        //    if (!File.Exists(path))
        //    {
        //        Alert.ShowAlertError("No tiene archivo relacionado", this);
        //    }
        //    else
        //    {
        //        // Limpiamos la salida
        //        Response.Clear();
        //        // Con esto le decimos al browser que la salida sera descargable
        //        Response.ContentType = "application/octet-stream";
        //        // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
        //        Response.AddHeader("Content-Disposition", "attachment; filename=" + file_name);
        //        // Escribimos el fichero a enviar
        //        Response.WriteFile(path);
        //        // volcamos el stream
        //        Response.Flush();
        //        // Enviamos todo el encabezado ahora
        //        Response.End();
        //        // Response.End();
        //    }
        //}

        ///// <summary>
        ///// Carga grid de subprocesos
        ///// </summary>
        //private void CargarGrid()
        //{
        //    ChangeOrden();
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
        //    DataTable tablecopy = dt.Copy();
        //    grid_subprocesos.DataSource = tablecopy;
        //    grid_subprocesos.DataBind();
        //}

        ///// <summary>
        ///// Carga grid de archivos
        ///// </summary>
        //private void CargarGridFiles()
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_files"];
        //    DataTable tablecopy = dt.Copy();
        //    gridarchivos.DataSource = tablecopy;
        //    gridarchivos.DataBind();
        //}

        ///// <summary>
        ///// Muestra la lista de perfiles por subproceso
        ///// </summary>
        ///// <param name="idc_subproceso"></param>
        ///// <param name="descripcion"></param>
        //private void CargarListaPerfiles(int idc_subproceso, string descripcion)
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
        //    DataView view = dt.DefaultView;
        //    //filtramos por subproceso
        //    view.RowFilter = "idc_subproceso = " + idc_subproceso + " and descripcion = '" + descripcion + "'";

        //    foreach (DataRow row in view.ToTable().Rows)
        //    {
        //        int idc_puestoperfil = Convert.ToInt32(row["idc_puestoperfil"]);
        //        foreach (ListItem item in cbxlperfiles.Items)
        //        {
        //            int idc_pp = Convert.ToInt32(item.Value);
        //            if (idc_pp == idc_puestoperfil)
        //            {
        //                item.Selected = true;
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// Agrega fila a archivos
        ///// </summary>
        ///// <param name="idc_procesosarc"></param>
        ///// <param name="idc_proceso"></param>
        ///// <param name="observaciones"></param>
        ///// <param name="extension"></param>
        ///// <param name="url"></param>
        //private void AddtoTableFiles(int idc_procesosarc, int idc_proceso, string observaciones, string extension, string url)
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_files"];
        //    DeleteToTableFiles(observaciones, url);
        //    DataRow row = dt.NewRow();
        //    row["idc_proceso"] = idc_proceso;
        //    row["observaciones"] = observaciones;
        //    row["extension"] = extension;
        //    row["idc_procesosarc"] = idc_procesosarc;
        //    row["url"] = url;
        //    dt.Rows.Add(row);
        //    Session[hiddenvalue.Value + "table_files"] = dt;
        //}

        ///// <summary>
        ///// Elimina fila de tabla archivos
        ///// </summary>
        ///// <param name="observaciones"></param>
        ///// <param name="url"></param>
        //private void DeleteToTableFiles(string observaciones, string url)
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_files"];
        //    foreach (DataRow rows in dt.Rows)
        //    {
        //        string value = rows["observaciones"].ToString();
        //        string valueidc = rows["url"].ToString();
        //        //para nuevos registros
        //        if (valueidc == url && observaciones == value)
        //        {
        //            rows.Delete();
        //            break;
        //        }
        //    }
        //    Session[hiddenvalue.Value + "table_files"] = dt;
        //}

        ///// <summary>
        ///// Agrega fila a tabla, si existe un registro similar lo reemplaza
        ///// </summary>
        ///// <param name="idc_subproceso"></param>
        ///// <param name="descripcion"></param>
        ///// <param name="orden"></param>
        ///// <param name="url"></param>
        //private void AddtoTable(int idc_subproceso, string descripcion, int orden, string url)
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
        //    DeleteToTable(idc_subproceso, descripcion);
        //    DataRow row = dt.NewRow();
        //    row["idc_subproceso"] = idc_subproceso;
        //    row["descripcion"] = descripcion;
        //    row["orden"] = orden;
        //    row["url"] = url;
        //    dt.Rows.Add(row);
        //    Session[hiddenvalue.Value + "table_subprocesos"] = dt;
        //}

        ///// <summary>
        ///// Agrega fila atabla perfiles
        ///// </summary>
        ///// <param name="idc_subproceso"></param>
        ///// <param name="descripcion"></param>
        ///// <param name="orden"></param>
        ///// <param name="url"></param>
        //private void AddtoTablePerfiles(int idc_subproceso, string descripcion, int idc_puestoperfil)
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
        //    DeleteToTablePerfiles(idc_subproceso, descripcion, idc_puestoperfil);
        //    DataRow row = dt.NewRow();
        //    row["idc_subproceso"] = idc_subproceso;
        //    row["descripcion"] = descripcion;
        //    row["idc_puestoperfil"] = idc_puestoperfil;
        //    dt.Rows.Add(row);
        //    Session[hiddenvalue.Value + "table_perfiles"] = dt;
        //}

        ///// <summary>
        ///// Elimina registro de tabla
        ///// </summary>
        ///// <param name="idc_subproceso"></param>
        ///// <param name="descripcion"></param>
        //private void DeleteToTable(int idc_subproceso, string descripcion)
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
        //    foreach (DataRow rows in dt.Rows)
        //    {
        //        string value = rows["descripcion"].ToString();
        //        int valueidc = Convert.ToInt32(rows["idc_subproceso"]);
        //        //para nuevos registros
        //        if (descripcion == value && idc_subproceso == 0)
        //        {
        //            rows.Delete();
        //            break;
        //        }
        //        //para registros anteriores
        //        if (idc_subproceso == valueidc && idc_subproceso > 0)
        //        {
        //            rows.Delete();
        //            break;
        //        }
        //    }
        //    Session[hiddenvalue.Value + "table_subprocesos"] = dt;
        //}

        ///// <summary>
        ///// Elimina fial d etabla perfiles por id
        ///// </summary>
        ///// <param name="idc_subproceso"></param>
        ///// <param name="descripcion"></param>
        //private void DeleteToTablePerfiles(int idc_subproceso, string descripcion, int idc_puestoperfil)
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
        //    foreach (DataRow rows in dt.Rows)
        //    {
        //        string value = rows["descripcion"].ToString();
        //        int valueidc = Convert.ToInt32(rows["idc_subproceso"]);
        //        int idc_pp = Convert.ToInt32(rows["idc_puestoperfil"]);
        //        //para nuevos registros
        //        if (descripcion == value && idc_subproceso == 0 && idc_pp == idc_puestoperfil)
        //        {
        //            rows.Delete();
        //            break;
        //        }
        //        //para registros anteriores
        //        if (idc_subproceso == valueidc && idc_subproceso > 0 && idc_pp == idc_puestoperfil)
        //        {
        //            rows.Delete();
        //            break;
        //        }
        //    }
        //    Session[hiddenvalue.Value + "table_perfiles"] = dt;
        //}

        ///// <summary>
        ///// elimina todo sobre id
        ///// </summary>
        ///// <param name="idc_subproceso"></param>
        ///// <param name="descripcion"></param>
        ///// <param name="idc_puestoperfil"></param>
        //private void DeleteToTablePerfilesTodas(int idc_subproceso, string descripcion)
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
        //    List<DataRow> rowsToDelete = new List<DataRow>();
        //    foreach (DataRow rows in dt.Rows)
        //    {
        //        string value = rows["descripcion"].ToString();
        //        int valueidc = Convert.ToInt32(rows["idc_subproceso"]);
        //        int idc_pp = Convert.ToInt32(rows["idc_puestoperfil"]);
        //        //para nuevos registros
        //        if (descripcion == value && idc_subproceso == 0)
        //        {
        //            rowsToDelete.Add(rows);
        //        }
        //        //para registros anteriores
        //        if (idc_subproceso == valueidc && idc_subproceso > 0)
        //        {
        //            rowsToDelete.Add(rows);
        //        }
        //    }
        //    foreach (DataRow row in rowsToDelete)
        //    {
        //        dt.Rows.Remove(row);
        //    }
        //    Session[hiddenvalue.Value + "table_perfiles"] = dt;
        //}

        //private void UpdateTablePerfiles(int idc_subproceso, string descripcion, int idc_puestoperfil)
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
        //    foreach (DataRow rows in dt.Rows)
        //    {
        //        string value = rows["descripcion"].ToString();
        //        int valueidc = Convert.ToInt32(rows["idc_subproceso"]);
        //        //para nuevos registros
        //        if (descripcion == value && idc_subproceso == 0)
        //        {
        //            rows["idc_subproceso"] = descripcion;
        //            rows["idc_puestoperfil"] = idc_puestoperfil;
        //            rows["descripcion"] = descripcion;
        //            break;
        //        }
        //        //para registros anteriores
        //        if (idc_subproceso == valueidc && idc_subproceso > 0)
        //        {
        //            rows["idc_subproceso"] = descripcion;
        //            rows["idc_puestoperfil"] = idc_puestoperfil;
        //            rows["descripcion"] = descripcion;
        //            break;
        //        }
        //    }
        //    Session[hiddenvalue.Value + "table_perfiles"] = dt;
        //}

        ///// <summary>
        ///// Actualiza url registro de tabla
        ///// </summary>
        ///// <param name="idc_subproceso"></param>
        ///// <param name="descripcion"></param>
        //private void UpdateURLHTMLToTable(int idc_subproceso, string descripcion, string url)
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
        //    foreach (DataRow rows in dt.Rows)
        //    {
        //        string value = rows["descripcion"].ToString();
        //        int valueidc = Convert.ToInt32(rows["idc_subproceso"]);
        //        //para nuevos registros
        //        if (descripcion == value && idc_subproceso == 0)
        //        {
        //            rows["url"] = url;
        //            break;
        //        }
        //        //para registros anteriores
        //        if (idc_subproceso == valueidc && idc_subproceso > 0)
        //        {
        //            rows["url"] = url;
        //            break;
        //        }
        //    }
        //    Session[hiddenvalue.Value + "table_subprocesos"] = dt;
        //}

        ///// <summary>
        ///// Cambia el orden de la tabla segun sun row number
        ///// </summary>
        //private void ChangeOrden()
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
        //    DataView view = dt.DefaultView;
        //    view.Sort = "orden asc";
        //    DataTable sortedDT = view.ToTable();
        //    int i = 0;
        //    int index = 0;
        //    foreach (DataRow row in sortedDT.Rows)
        //    {
        //        index = i;
        //        row["orden"] = index + 1;
        //        i++;
        //    }
        //    Session[hiddenvalue.Value + "table_subprocesos"] = sortedDT;
        //}

        ///// <summary>
        ///// Eegresa el valor maximo d ela tabla
        ///// </summary>
        ///// <returns></returns>
        //private int GetValueMax()
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
        //    DataView view = dt.DefaultView;
        //    view.Sort = "orden desc";
        //    DataTable sortedDT = view.ToTable();
        //    return sortedDT.Rows.Count > 0 ? Convert.ToInt32(sortedDT.Rows[0]["orden"]) : 0;
        //}

        //protected void gridarchivos_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    string ruta = gridarchivos.DataKeys[index].Values["url"].ToString();
        //    string observaciones = gridarchivos.DataKeys[index].Values["observaciones"].ToString();
        //    string nombre = Path.GetFileName(ruta);
        //    switch (e.CommandName)
        //    {
        //        case "Descargar":
        //            Download(ruta, nombre);
        //            break;

        //        case "eliminar":
        //            DeleteToTableFiles(observaciones, ruta);
        //            CargarGridFiles();
        //            Alert.ShowAlert("Elemento eliminado correctamente", "Mensaje del Sistema", this);
        //            break;
        //    }
        //}

        ///// <summary>
        ///// Cadena de subprocesos
        ///// </summary>
        ///// <returns></returns>
        //private String CadenaSubProcesos()
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
        //    string cadena = "";
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        cadena = cadena + row["idc_subproceso"].ToString() + ";" + row["descripcion"].ToString() + ";" + row["orden"].ToString() + ";" + row["url"].ToString() + ";";
        //    }
        //    return cadena;
        //}

        ///// <summary>
        ///// Cadena perfil-subproceso
        ///// </summary>
        ///// <returns></returns>
        //private String CadenaPerfiles()
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
        //    string cadena = "";
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        cadena = cadena + row["idc_subproceso"].ToString() + ";" + row["descripcion"].ToString() + ";" + row["idc_puestoperfil"].ToString() + ";";
        //    }
        //    return cadena;
        //}

        ///// <summary>
        ///// Cadena de archivos del proceso principal
        ///// </summary>
        ///// <returns></returns>
        //private String Cadena()
        //{
        //    string cadena = "";

        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_files"];
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        cadena = cadena + row["idc_procesosarc"].ToString() + ";" + row["extension"].ToString() + ";" + row["observaciones"].ToString() + ";" + row["url"].ToString() + ";";
        //    }
        //    return cadena;
        //}

        ///// <summary>
        ///// YTotal cadena de archivos del proceso principal
        ///// </summary>
        ///// <returns></returns>
        //private int TotalCadena()
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_files"];
        //    return dt.Rows.Count;
        //}

        ///// <summary>
        ///// YTotal cadena subprocesos
        ///// </summary>
        ///// <returns></returns>
        //private int TotalCadenaSubProcesos()
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_subprocesos"];
        //    return dt.Rows.Count;
        //}

        ///// <summary>
        ///// Total cadena relacion perfiles-subprocesos
        ///// </summary>
        ///// <returns></returns>
        //private int TotalCadenaPerfiles()
        //{
        //    DataTable dt = (DataTable)Session[hiddenvalue.Value + "table_perfiles"];
        //    return dt.Rows.Count;
        //}
    }
}