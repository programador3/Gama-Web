using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Vehiculos_EntregarCOM
    {
        /// <summary>
        /// Carga Herramientas por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaVehPrep(Vehiculos_EntregaENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_entrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoentrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puestoentrega });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_entrega_vehiculos_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaFormato(Vehiculos_EntregaENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_entrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoentrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puestoentrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_VEHICULO", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_carga_formato_vehiculo_entrega", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Carga Herramientas por confirmar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargarConfirmacion(Vehiculos_EntregaENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_confirma_entrega_vehiculo_web", listparameters, false);
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
        public DataSet InsertarEntrega(Vehiculos_EntregaENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_entrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADVEH", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadveh });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMVEH", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadveh });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADVEH_HERR", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadveh_herr });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMVEH_HERR", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadveh_herr });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aentregar_vehiculos_web", listparameters, true);
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
        public DataSet InsertarConfirmacion(Vehiculos_EntregaENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADVEH", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadveh });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMVEH", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadveh });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADVEH_HERR", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadveh_herr });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMVEH_HERR", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadveh_herr });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_entrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aconfirmar_entrega_vehiculos", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}