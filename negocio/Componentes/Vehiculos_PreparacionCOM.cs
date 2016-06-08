using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Vehiculos_PreparacionCOM
    {
        /// <summary>
        /// Carga Herramientas por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaVehPrep(Vehiculos_PreparacionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoprep", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puestoprep });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_preparacion_vehiculos_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Inserta una Preparacion
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet InsertarPrep(Vehiculos_PreparacionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADVEH", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadveh });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMVEH", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadveh });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADVEH_HERR", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadveh_herr });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMVEH_HERR", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadveh_herr });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apreparar_vehiculos_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Puestos_PrepararCancelar(Vehiculos_PreparacionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmotivo", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pmotivo });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apuestos_preparar_can", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}