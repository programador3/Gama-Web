using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Celulares_EntregarCOM
    {
        /// <summary>
        /// Carga CargaCelualres por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaCelualres(Celulares_EntregarENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_entrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoentrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puestoentrega });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_entrega_celulares", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Carga CargaCelualres por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaConfirmaciones(Celulares_EntregarENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_confirma_entrega_celulares_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Inserta una Entrega
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet InsertarEntrega(Celulares_EntregarENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_entrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADCEL", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadcel });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMCEL", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadcel });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADCEL_ACC", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadcel_acc });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMCEL_ACC", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadcel_acc });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aentregar_celulares_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Inserta una Entrega
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet InsertarConfirmacion(Celulares_EntregarENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADCEL", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadcel });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMCEL", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadcel });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADCEL_ACC", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadcel_acc });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMCEL_ACC", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadcel_acc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_entrega", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aconfirmar_entrega_celulares", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}