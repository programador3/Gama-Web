using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class AdministradorPrebajasCOM
    {
        /// <summary>
        /// Carga listado de prebajas
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaPreBajas(AdministradorPrebajasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prebaja", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_prebaja });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoprebaja", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto_prebaja });
            listparameters.Add(new SqlParameter() { ParameterName = "@pconsulta", SqlDbType = SqlDbType.Bit, Value = Entidad.Pconsutla });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_administrador_revision_prebajas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Carga listado de prebajas
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaGrafica(AdministradorPrebajasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pyear", SqlDbType = SqlDbType.Int, Value = Entidad.Pyear });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_graficas_adminprebajas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}