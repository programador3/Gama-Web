using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Herramientas_PreparacionCOM
    {
        /// <summary>
        /// Carga Herramientas por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaHerramientasPrep(Herramientas_PreparacionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoprep", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puestoprep });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_preparacion_activos", listparameters, false);
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
        public DataSet InsertarPrep(Herramientas_PreparacionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADHERR", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadherr });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMHERR", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadherr });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apreparar_herramientas_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}