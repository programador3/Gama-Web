using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Herramientas_EntregasCOM
    {
        /// <summary>
        /// Carga Herramientas por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaHerramientas(Herramientas_EntregasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_entrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_Entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoent", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puestoent });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_entrega_activos_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }/// <summary>

        /// Carga Herramientas por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet ConfirmaHerr(Herramientas_EntregasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_confirma_entrega_equipos_web", listparameters, false);
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
        public DataSet InsertarPrep(Herramientas_EntregasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_entrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_Entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADHERR", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadherr });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMHERR", SqlDbType = SqlDbType.Int, Value = Entidad.Numcadherr });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aentregar_herramientas_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet InsertaConfirmActivos(Herramientas_EntregasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadherr });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotalcad", SqlDbType = SqlDbType.Int, Value = Entidad.Numcadherr });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_entrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_Entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aconfirmar_entrega_equipos", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}