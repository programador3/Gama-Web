using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class AvisosCOM
    {
        /// <summary>
        /// Carga Servicios por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaAvisos(AvisosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_avisos_nuevo", listparameters, false);
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
        public DataSet OcultaAvisos(AvisosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_avisoweb", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_avisoweb });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ocultaavisos_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}