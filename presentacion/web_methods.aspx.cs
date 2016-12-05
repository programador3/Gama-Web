using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class web_methods : System.Web.UI.Page
    {
        public static int idc_usuario = 0;
        public static int idc_puesto = 0;
        public static string path_user_chat = "";
        public static string content = "";
        public static DateTime? date;

        [Serializable]
        public class Id
        {
            public int id { get; set; }
        }

        public static void SearchMenu(string search, Panel panel_menus_repeat, Panel panel_search, Repeater Repeater2)
        {
            panel_menus_repeat.Visible = search == "" ? true : false;
            panel_search.Visible = search == "" ? false : true;
            DataSet ds = new DataSet();
            OpcionesE EntOpcion = new OpcionesE();
            OpcionesBL menuBL = new OpcionesBL();
            EntOpcion.Usuario_id = idc_usuario;
            EntOpcion.Search = search;
            ds = menuBL.MenuDinmaico(EntOpcion);
            Repeater2.DataSource = ds.Tables[0];
            Repeater2.DataBind();
        }

        /// <summary>
        /// Elimina un archivo en especifico
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static int DeleteFile()
        {
            if (File.Exists(path_user_chat))
            {
                File.Delete(path_user_chat);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [System.Web.Services.WebMethod]
        public static void CreateFile(string content, string pagina)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            string myip = wc.DownloadString("https://l2.io/ip");
            if (!File.Exists(path_user_chat))
            {
                StreamWriter file = new StreamWriter(path_user_chat);
                file.WriteLine(content);
                file.WriteLine(pagina);
                file.Close();
            }
        }

        /// <summary>
        /// Retorna el numero de Avisos sin leer por usuario
        /// </summary>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static int NotificationsAvisos()
        {
            NotificacionesENT ent = new NotificacionesENT();
            ent.Idc_usuario = idc_usuario;
            NotificacionesCOM com = new NotificacionesCOM();
            int total = Convert.ToInt32(com.CargaAvisos(ent).Tables[0].Rows[0]["total"].ToString());
            return total;
        }

        /// <summary>
        /// Retorna el numero de tareas pendientes por usuario
        /// </summary>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static int NotificationsPendientes()
        {
            NotificacionesENT ent = new NotificacionesENT();
            ent.Idc_usuario = idc_usuario;
            NotificacionesCOM com = new NotificacionesCOM();
            int total = com.CargaNotificaciones(ent).Tables[0].Rows.Count;
            return total;
        }

        [System.Web.Services.WebMethod]
        public string ExistFile(string path_origen, string path_tocopy)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(@path_origen, path_tocopy);
                return "";
            }
            catch (Exception ex)
            {
                return "Problem: " + ex.Message;
            }
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
            TareasCOM com = new TareasCOM();
            List<ArbolTareas> noti = new List<ArbolTareas>();
            foreach (DataRow row in com.CargarArbolTareas(ent).Tables[0].Rows)
            {
                noti.Add(new ArbolTareas
                {
                    id = Convert.ToInt32(row["idc_tarea"]),
                    id_parent = Convert.ToInt32(row["idc_tarea_padre"]),
                    descripcion = row["descripcion"].ToString(),
                    puesto = row["puesto"].ToString(),
                    empleado = row["empleado"].ToString(),
                    estado = row["tipo"].ToString(),
                    color = row["color"].ToString(),
                    f_com = "FC: " + row["fecha_compromiso"].ToString()
                });
            }
            return noti;
        }

        public class Articulo
        {
            public string id;
            public string nombre;
        }

        [System.Web.Services.WebMethod]
        public static List<Articulo> CargaArticulos(List<string> aData)
        {
            String a = aData[0];
            List<Articulo> list = new List<Articulo>();
            TareasENT entidad = new TareasENT();
            TareasCOM componente = new TareasCOM();
            entidad.Pcadena_arch = a;
            DataSet ds = componente.CargarArticulos(entidad);
            DataTable dt = ds.Tables[0];
            StringBuilder html = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Articulo
                {
                    id = row["idc_categoria"].ToString(),
                    nombre = row["nombre"].ToString()
                });
                //string nombre = row["nombre"].ToString();
                //string id = row["idc_categoria"].ToString();
                //html.Append("<a href='http://www.google.com.mx'>" + nombre + "</a><br>");
            }

            return list;
            //PlaceHolder.Controls.Add(new Literal { Text = html.ToString() });
        }

        [System.Web.Services.WebMethod]
        public static string Val()
        {
            return "helo";
        }


        /// <summary>
        /// Regresa tabla con notificaciones
        /// </summary>
        /// <param name="idc_puesto"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static List<Notificacion> GetNotificaciones(List<string> aData)
        {
            List<Notificacion> noti = new List<Notificacion>();
            DateTime now = DateTime.Now;
            if (now > date && date != null)
            {
                date = DateTime.Now.AddMinutes(4);
            }
            String a = aData[0];
            NotificacionesENT ent = new NotificacionesENT();
            ent.Idc_usuario = Convert.ToInt32(a);
            NotificacionesCOM com = new NotificacionesCOM();
            foreach (DataRow row in com.CargaNotificaciones(ent).Tables[0].Rows)
            {
                noti.Add(new Notificacion
                {
                    url = row["pagina"].ToString(),
                    Titulo = row["pendiente"].ToString(),
                    idc_usuario = row["idc_usuario"].ToString()
                });
            }
            return noti;
        }

        [System.Web.Services.WebMethod]
        public static List<Opciones> GetOpciones(List<string> aData)
        {
            String a = aData[0];
            OpcionesE entidadad = new OpcionesE();
            OpcionesBL compad = new OpcionesBL();
            entidadad.Usuario_id = Convert.ToInt32(a);
            List<Opciones> noti = new List<Opciones>();
            foreach (DataRow row in compad.AcessosDirectos(entidadad).Tables[0].Rows)
            {
                noti.Add(new Opciones
                {
                    url = row["web_form"].ToString(),
                    descripcion = row["descripcion"].ToString(),
                    icono = row["icon"].ToString()
                });
            }
            return noti;
        }

        [System.Web.Services.WebMethod]
        public string LearMsg()
        {
            string val = "";
            try
            {
                if (File.Exists(@"\\192.168.0.3\puestos_web$\temp\mensaje.txt"))
                {
                    StreamReader file = new StreamReader(@"\\192.168.0.3\puestos_web$\temp\mensaje.txt");
                    val = file.ReadToEnd().ToUpper();
                    file.Close();
                }
                else
                {
                    val = "";
                }
                return val;
            }
            catch (Exception ex)
            {
                return "Problem: " + ex.Message;
            }
        }

        public class Comentarios
        {
            public string comentario { get; set; }
        }
    }
}