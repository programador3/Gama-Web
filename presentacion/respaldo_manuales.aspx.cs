using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace presentacion
{
    public partial class respaldo_manuales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                archivos();
            }
        }

        private DataTable archivos()
        {            
            try
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/temp/html_files/"));
                DataTable dt = new DataTable();
                dt.Columns.Add("archivo");
                dt.Columns.Add("fecha");
                dt.Columns.Add("ruta");
                foreach (string filePath in filePaths)
                {
                    DataRow row = dt.NewRow();
                    row["ruta"] = filePath;
                    row["archivo"] = (Path.GetFileName(filePath));
                    row["fecha"] = File.GetCreationTime(filePath).ToString("dd MMMM, yyyy H:mm:ss", 
                        System.Globalization.CultureInfo.CreateSpecificCulture("es-MX"));
                    //row["fecha"] = Convert.ToDateTime(File.GetCreationTime(filePath).ToString("dd MMMM, yyyy H:mm:ss",
                    //   System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")));
                    dt.Rows.Add(row);
                }
                grid_archivos.DataSource = dt;
                grid_archivos.DataBind();
                return dt;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(),this);
                return new DataTable();
            }
        }

        protected void grid_archivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string url = grid_archivos.DataKeys[index].Values["ruta"].ToString();
            string archivo = grid_archivos.DataKeys[index].Values["archivo"].ToString();
            switch (e.CommandName)
            {
                case "Solicitar":
                    if (File.Exists(url))
                    {
                        url = funciones.ConvertStringToHex(url);
                        String urlt = HttpContext.Current.Request.Url.AbsoluteUri;
                        String path_actual = urlt.Substring(urlt.LastIndexOf("/") + 1);
                        urlt = urlt.Replace(path_actual, "");
                        urlt = urlt + "view_files.aspx?file=" + url;
                        ScriptManager.RegisterStartupScript(this, GetType(), "noti533wswswsW3", "window.open('" + urlt + "');", true);
                    }
                    break;
                case "Descargar":
                    funciones.Download(url, archivo,this);
                    break;
            }
            
        }
    }
}