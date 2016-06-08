using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class herramientasCOM
    {
        /// <summary>
        /// Devuelve DataSet con lista de herramientas por puesto
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DataSet CargaHerramientasCatalogo(herramientasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_activos_x_puesto", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Devuelve DataSet con lista de herramientas por vehiculo
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DataSet CargaHerramientasVehculo(herramientasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_vehiculo", SqlDbType = SqlDbType.Int, Value = entidad.Idc_vehiculo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_vehiculos_herramientas_x_gpo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Devuelve DataSet con lista de VEHICULOS POR PUESTO
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DataSet CargaVehiculos(herramientasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_vehiculos_x_puesto", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Devuelve DataSet con lista de VEHICULOS POR PUESTO
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DataSet CargaDatosCelulares(herramientasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_celulares_x_puesto", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}