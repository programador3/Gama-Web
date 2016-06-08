using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Solicitud_PrebajaCOM
    {
        /// <summary>
        /// Carga empleado
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaEmpleados(Solicitud_PrebajaENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_USUARIO", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_organigrama_usuario_aut", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Inserta una nueva Pre Baja
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet InsertarPrebaja(Solicitud_PrebajaENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@apto_reingreso", SqlDbType = SqlDbType.Bit, Value = Entidad.Apto_reingreso });
            listparameters.Add(new SqlParameter() { ParameterName = "@honesto", SqlDbType = SqlDbType.Bit, Value = Entidad.Honesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@trabajador", SqlDbType = SqlDbType.Bit, Value = Entidad.Trabajador });
            listparameters.Add(new SqlParameter() { ParameterName = "@drogas", SqlDbType = SqlDbType.Bit, Value = Entidad.Drogas });
            listparameters.Add(new SqlParameter() { ParameterName = "@alcohol", SqlDbType = SqlDbType.Bit, Value = Entidad.Alcohol });
            listparameters.Add(new SqlParameter() { ParameterName = "@robo", SqlDbType = SqlDbType.Bit, Value = Entidad.Robo });
            listparameters.Add(new SqlParameter() { ParameterName = "@carta_rec", SqlDbType = SqlDbType.Bit, Value = Entidad.Carta_recomendacion });
            listparameters.Add(new SqlParameter() { ParameterName = "@contratar", SqlDbType = SqlDbType.Bit, Value = Entidad.Contratar });
            listparameters.Add(new SqlParameter() { ParameterName = "@motivo", SqlDbType = SqlDbType.VarChar, Value = Entidad.Motivo });
            listparameters.Add(new SqlParameter() { ParameterName = "@especificar", SqlDbType = SqlDbType.VarChar, Value = Entidad.Especificar });
            listparameters.Add(new SqlParameter() { ParameterName = "@fecha_baja", SqlDbType = SqlDbType.DateTime, Value = Entidad.Fecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prebaja", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_Prebaja });
            listparameters.Add(new SqlParameter() { ParameterName = "@prenuncia", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_Prebaja });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apre_bajas_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}