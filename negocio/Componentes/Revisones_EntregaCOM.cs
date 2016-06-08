using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Revisones_EntregaCOM
    {
        /// <summary>
        /// Carga Servicios por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaPrep(Servicios_EntregaENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_entrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoentrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puestoentrega });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_entrega_servicios_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Carga Servicios por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaConfirmacion(Servicios_EntregaENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_confirma_entrega_servicios_web", listparameters, false);
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
        public DataSet InsertarEntrrega(Servicios_EntregaENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_entrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADSER", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadser });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMSER", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadser });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aentregar_servicios_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet InsertarConfirm(Servicios_EntregaENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADSER", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadser });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMSER", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadser });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_entrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aconfirmar_entrega_servicios", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}