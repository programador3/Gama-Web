using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;

namespace presentacion
{
    public partial class arbol_tareas : System.Web.UI.Page
    {
        private static int idc_tarea = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            idc_tarea = Convert.ToInt32(Request.QueryString["idc_tarea"]);
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
    }
}