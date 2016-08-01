using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class tareas_arbol : System.Web.UI.Page
    {
        private static int idc_tarea = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            idc_tarea = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tarea"]));
            String url = HttpContext.Current.Request.Url.AbsoluteUri;
            String path_actual = url.Substring(url.LastIndexOf("/") + 1);
            url = url.Replace(path_actual, "");
            HiddenField.Value = url;
        }

        /// <summary>
        /// Regresa tabla con notificaciones
        /// </summary>
        /// <param name="idc_puesto"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static List<ArbolTareas> GetTareas()
        {
            TareasENT ent = new TareasENT();
            ent.Pidc_tarea = idc_tarea;
            TareasCOM com = new TareasCOM();
            List<ArbolTareas> noti = new List<ArbolTareas>();
            foreach (DataRow row in com.CargarArbolTareas(ent).Tables[0].Rows)
            {
                noti.Add(new ArbolTareas
                {
                    id = Convert.ToInt32(row["idc_tarea"]),
                    id_parent = Convert.ToInt32(row["idc_tarea_padre"]),
                    redirect = Convert.ToInt32(row["redirect"]),
                    descripcion = row["descripcion"].ToString(),
                    puesto = row["puesto"].ToString(),
                    empleado = row["empleado"].ToString(),
                    estado = row["tipo"].ToString(),
                    color = row["color"].ToString(),
                    f_com = "FC: " + row["fecha_compromiso"].ToString(),
                    idc_tarea_url = row["idc_tarea_url"].ToString(),
                    tipo = row["externo"].ToString(),
                    color_grupo = row["color_group"].ToString()
                });
            }
            return noti;
        }

        /// <summary>
        /// Carga las tareas pendientes
        /// </summary>
        private void CargarTareas(int idc_tarea)
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pidc_tarea = idc_tarea;
                DataSet ds = componente.CargarTareas(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbltarea.Text = ds.Tables[0].Rows[0]["descripcion"].ToString();
                }
                gridPapeleria.DataSource = ds.Tables[2];
                gridPapeleria.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void gridPapeleria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridPapeleria.DataKeys[index].Values["ruta"].ToString();
            string extension = gridPapeleria.DataKeys[index].Values["extension"].ToString();
            string descripcion = gridPapeleria.DataKeys[index].Values["descripcion"].ToString();
            string id_archi = gridPapeleria.DataKeys[index].Values["idc_tarea_archivo"].ToString();
            bool archivo = Convert.ToBoolean(gridPapeleria.DataKeys[index].Values["archivo"]);
            Session["id_archivo"] = id_archi;
            DataTable papeleria = (DataTable)Session["papeleria"];
            int papeleriat = papeleria.Rows.Count;
            switch (e.CommandName)
            {
                case "Descargar":
                    if (archivo == true)
                    {
                        Download(ruta, Path.GetFileName(ruta));
                    }
                    else
                    {
                        Alert.ShowAlertInfo("Este comentario no contiene archivo", "Mensaje del sistema", this);
                    }
                    break;
            }
        }

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            string url = HiddenFieldurl.Value;
            Response.Redirect(url);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            int idc_tarea = Convert.ToInt32(funciones.de64aTexto(HiddenFieldidctarea.Value));
            CargarTareas(idc_tarea);
        }

        protected void gridPapeleria_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                bool archi = Convert.ToBoolean(rowView["archivo"]);
                if (archi == false)
                {
                    e.Row.Cells[0].Controls.Clear();
                }
            }
        }
    }
}