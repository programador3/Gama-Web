using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Celulares_PreparacionCOM
    {
        /// <summary>
        /// Carga Herramientas por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaHerramientasPrep(Celulares_PreparacionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoprep", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puestoprep });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_preparacion_celulares", listparameters, false);
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
        public DataSet InsertarPrep(Celulares_PreparacionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADCEL", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadcel });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMCEL", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadcel });
            listparameters.Add(new SqlParameter() { ParameterName = "PCADCEL_ACC", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadcel_acc });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMCEL_ACC", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadcel_acc });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apreparar_celulares_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}