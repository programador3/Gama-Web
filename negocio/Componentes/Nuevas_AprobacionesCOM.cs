using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Nuevas_AprobacionesCOM
    {
        /// <summary>
        /// Regresa un DataSet con los datos idc_puesto,nombre(puesto), departamento de la tabla puestos
        /// </summary>
        public DataSet getDataPuestos(Nuevas_AprobacionesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_empleado_completo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Inserta o actualiza una aprobacion, La entidad recibe el idc_aprobacion = 0 si es nueva
        /// </summary>
        public DataSet InsertAprobacion(Nuevas_AprobacionesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_aprobacion", SqlDbType = SqlDbType.Int, Value = entidad.Idc_aprobacion });
            listparameters.Add(new SqlParameter() { ParameterName = "@nombre_aprobacion", SqlDbType = SqlDbType.VarChar, Value = entidad.Nombre });
            listparameters.Add(new SqlParameter() { ParameterName = "@comentario_aprobacion", SqlDbType = SqlDbType.VarChar, Value = entidad.Descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@minimo_votos", SqlDbType = SqlDbType.Int, Value = entidad.Minimo_firmas });
            listparameters.Add(new SqlParameter() { ParameterName = "@totalcadena_puestos", SqlDbType = SqlDbType.Int, Value = entidad.Total_Cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@cadena_puestos", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_a_aprobaciones", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Regresa un DataSet con datos de prueba
        /// </summary>
        public DataSet datamenu(Nuevas_AprobacionesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_prueba_humberto_menu", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}