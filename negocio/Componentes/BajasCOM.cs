using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class BajasCOM
    {
        /// <summary>
        /// Carga Prebajas listas para baja
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaBajas(BajasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_PRE_BAJAS_PENDIENTES", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Carga Prebajas listas para baja
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaBajasCancelar(BajasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_PRE_BAJAS_PENDIENTES_CANC", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Inserta un Revision de Pre Baja
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet InsertarRevision(BajasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_EMPLEADO", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@PFECHA", SqlDbType = SqlDbType.Int, Value = Entidad.Fecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_USUARIO", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@PDIRECIP", SqlDbType = SqlDbType.VarChar, Value = Entidad.Ip });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNOMBREPC", SqlDbType = SqlDbType.VarChar, Value = Entidad.Nombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@PUSUARIOPC", SqlDbType = SqlDbType.Money, Value = Entidad.Usuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@PMOTIVO", SqlDbType = SqlDbType.VarChar, Value = Entidad.Motivo });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PREBAJA", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_prebaja });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cheque", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_cheque });
            try
            {
                if (Entidad.Capacitacion == true)
                {
                    ds = data.enviar("SP_AEMPLEADOS_BAJAS_CAPACITACION_WEB", listparameters, true);
                }
                else
                {
                    ds = data.enviar("SP_AEMPLEADOS_BAJAS_WEB", listparameters, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Comprueba un cheque de liquidacion
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet ComprobarCheque(BajasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_EMPLEADO", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfolio", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_cheque });
            try
            {
                ds = data.enviar("sp_comprobar_cheque_baja", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Cancela Pre Baja
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CancelarBaja(BajasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_USUARIO", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@PDIRECIP", SqlDbType = SqlDbType.VarChar, Value = Entidad.Ip });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNOMBREPC", SqlDbType = SqlDbType.VarChar, Value = Entidad.Nombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@PUSUARIOPC", SqlDbType = SqlDbType.Money, Value = Entidad.Usuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@PMOTIVO", SqlDbType = SqlDbType.VarChar, Value = Entidad.Motivo });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PREBAJA", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_prebaja });
            try
            {
                ds = data.enviar("sp_aempleados_pre_bajas_can_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}