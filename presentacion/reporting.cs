using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;

namespace presentacion

{
    public class reporting
    {
        public static string get_reporte(int idc_reporting)
        {
            DataSet ds = new DataSet();
            string path;
            //llenamos la entidad
            reportingE llenar_datos = new reportingE();
            llenar_datos.Idc_reporting = idc_reporting;
            //llamamos al componente
            ReportingBL datos = new ReportingBL();
            try
            {
                //ejecutamos metodo: recupera el path y nombre del reporte
                ds = datos.path_reporte(llenar_datos);
                //formamos la cadena
                path = ds.Tables[0].Rows[0]["ruta"].ToString() + ds.Tables[0].Rows[0]["nombre"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return path;
        }
    }
}