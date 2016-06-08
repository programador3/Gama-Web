using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class lugares_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null || Request.QueryString["idc_area"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["idc_area_lcaptura"] = funciones.ConvertHexToString(Request.QueryString["idc_area"]);
                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("nombre");
                papeleria.Columns.Add("folio");
                papeleria.Columns.Add("descripcion");
                Session["tabla_lugares"] = papeleria;
                TablaDatosAreas(Convert.ToInt32(funciones.ConvertHexToString(Request.QueryString["idc_area"])));
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/imagenes/areas/"));//path local
                File.Copy(funciones.GenerarRuta("areas", "unidad") + funciones.ConvertHexToString(Request.QueryString["idc_area"]) + ".jpg", dirInfo + funciones.ConvertHexToString(Request.QueryString["idc_area"]) + ".jpg", true);
                var domn = Request.Url.Host;
                string server = System.Configuration.ConfigurationManager.AppSettings["server"];
                img_area.Attributes["href"] = domn == "localhost" ? "/imagenes/areas/" + funciones.ConvertHexToString(Request.QueryString["idc_area"]) + ".jpg" : server + "/imagenes/areas/" + funciones.ConvertHexToString(Request.QueryString["idc_area"]) + ".jpg";
            }
        }

        /// <summary>
        /// Carga los datos de SQL
        /// </summary>
        /// <param name="idc_puesto"></param>
        /// <param name="idc_area"></param>
        private void TablaDatosAreas(int idc_area)
        {
            LugaresENT entidades = new LugaresENT();
            LugaresCOM com = new LugaresCOM();
            entidades.Pidc_area = idc_area;
            DataSet ds = com.CargaAreas(entidades);
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                DateTime localDate = DateTime.Now;
                string date = localDate.ToString();
                date = date.Replace("/", "_");
                date = date.Replace(":", "_");
                AddPapeleriaToTable(row["ruta"].ToString(), Path.GetFileName(row["ruta"].ToString()), row["folio"].ToString(), row["descripcion"].ToString());
            }
            DataTable papeleria = (DataTable)Session["tabla_lugares"];
            gridPapeleria.DataSource = papeleria;
            gridPapeleria.DataBind();
        }

        protected void lnkGuardarPape_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (txtdescripcion.Text == "") { error = true; Alert.ShowAlertError("Escriba una descripcion para el lugar", this); }
            if (txtfolio.Text == "") { error = true; Alert.ShowAlertError("Escriba un folio para el lugar", this); }
            if (fupPapeleria.HasFile && error == false)
            {
                DateTime localDate = DateTime.Now;
                string date = localDate.ToString();
                date = date.Replace("/", "_");
                date = date.Replace(":", "_");
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/lugares_captura/"));//path local
                string mensaje = AddPapeleriaToTable(dirInfo + date + "_" + fupPapeleria.FileName, date + "_" + fupPapeleria.FileName, txtfolio.Text, txtdescripcion.Text);
                if (mensaje.Equals(string.Empty))
                {
                    bool pape = funciones.UploadFile(fupPapeleria, dirInfo + date + "_" + fupPapeleria.FileName, this);
                    if (pape == true)
                    {
                        REV.Enabled = gridPapeleria.Rows.Count == 0 ? true : false;
                        Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "5000", "Archivo Guardardo Correctamente", this);
                        fupPapeleria.Visible = true;
                        txtdescripcion.Text = "";
                        txtfolio.Text = "";
                        gridPapeleria.DataSource = (DataTable)Session["tabla_lugares"]; ;
                        gridPapeleria.DataBind();
                    }
                }
                else
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }

        public string AddPapeleriaToTable(string ruta, string nombre, string folio, string descripcion)
        {
            string mensaje = "";
            DataTable papeleria = (DataTable)Session["tabla_lugares"];
            foreach (DataRow row in papeleria.Rows)
            {
                if (folio == row["folio"].ToString())
                {
                    row.Delete();
                    break;
                }
            }
            DataRow new_row = papeleria.NewRow();
            new_row["nombre"] = nombre;
            new_row["ruta"] = ruta;
            new_row["folio"] = folio;
            new_row["descripcion"] = descripcion;
            papeleria.Rows.Add(new_row);
            Session["tabla_lugares"] = papeleria;
            return mensaje;
        }

        protected void gridPapeleria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridPapeleria.DataKeys[index].Values["ruta"].ToString();
            string nombre = gridPapeleria.DataKeys[index].Values["nombre"].ToString();
            string descripcion = gridPapeleria.DataKeys[index].Values["descripcion"].ToString();
            string folio = gridPapeleria.DataKeys[index].Values["folio"].ToString();
            DataTable papeleria = (DataTable)Session["tabla_lugares"];
            switch (e.CommandName)
            {
                case "Eliminar":
                    foreach (DataRow row in papeleria.Rows)
                    {
                        if (folio == row["folio"].ToString())
                        {
                            row.Delete();
                            break;
                        }
                    }
                    Session["tabla_lugares"] = papeleria;
                    gridPapeleria.DataSource = papeleria;
                    gridPapeleria.DataBind();
                    break;

                case "Editar":
                    txtdescripcion.Text = descripcion;
                    txtfolio.Text = folio;
                    break;

                case "Descargar":
                    Download(ruta, nombre);
                    break;
            }
        }

        // <summary>
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

        protected void gridPapeleria_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HtmlAnchor a_img = (HtmlAnchor)e.Row.FindControl("a_img");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string ruta = rowView["ruta"].ToString();
                string nombre = rowView["nombre"].ToString();
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/lugares_captura/"));//path local
                if (ruta != dirInfo + nombre)
                {
                    File.Copy(ruta, dirInfo + nombre, true);
                }
                var domn = Request.Url.Host;
                string server = System.Configuration.ConfigurationManager.AppSettings["server"];
                a_img.Attributes["href"] = domn == "localhost" ? "/temp/lugares_captura/" + nombre : server + "/temp/lugares_captura/" + nombre;
            }
        }
    }
}