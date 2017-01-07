using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    
    public class AsistenciaCOM
    {
        public DataSet sp_ver_asistencia(int num_nomina)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pnum_nomina", SqlDbType = SqlDbType.Int, Value = num_nomina });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ver_asistencia", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_ver_asistencia_detalle(int num_nomina, DateTime fecha)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pnum_nomina", SqlDbType = SqlDbType.Int, Value = num_nomina });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = fecha });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ver_asistencia_detalle", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds; 
          }

        public DataSet sp_status_incidencia_dia_numnomina(int num_nomina, DateTime fecha)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pnum_nomina", SqlDbType = SqlDbType.Int, Value = num_nomina });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = fecha });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_status_incidencia_dia_numnomina", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_solicitudes_pendientes_asistencia()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_solicitudes_pendientes_asistencia", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds; 
        }
        public DataSet sp_autorizar_asistencias_varios_nuevo(string tipomov, string cadena, int totalcadena,
            string observaciones, int idc_usuario)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@POBSERVACIONES", SqlDbType = SqlDbType.Int, Value = observaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@Ptipomov", SqlDbType = SqlDbType.Int, Value = tipomov });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena", SqlDbType = SqlDbType.Int, Value = cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnum", SqlDbType = SqlDbType.Int, Value = totalcadena });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_autorizar_asistencias_varios_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_masistencia_observ_nuevo(DateTime pfecha,string pfechac, int idc_usuario, int idc_empleado, bool trabajo, string observ, 
            bool llegada_tempr, bool aviso)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfechac", SqlDbType = SqlDbType.Int, Value = pfechac });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = idc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptrabajo", SqlDbType = SqlDbType.Int, Value = trabajo });
            listparameters.Add(new SqlParameter() { ParameterName = "@POBSERVACIONES", SqlDbType = SqlDbType.Int, Value = observ });
            listparameters.Add(new SqlParameter() { ParameterName = "@PLLEGADA_TEMPRANO", SqlDbType = SqlDbType.Int, Value = llegada_tempr });
            listparameters.Add(new SqlParameter() { ParameterName = "@PAVISO126", SqlDbType = SqlDbType.Int, Value = aviso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_masistencia_observ_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_reporte_asistencia_web(DateTime pfecha, int idc_usuario, int idc_puesto)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = idc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_reporte_asistencia_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
