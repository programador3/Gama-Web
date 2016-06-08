using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Servicios_RevisionCOM
    {
        /// <summary>
        /// Carga  por revision
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaVehiculosRevision(Servicioes_RevisionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestorevi", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto_prerevisa });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoprebaja", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puestoprebaja });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_revisiones_servicios_web", listparameters, false);
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
        public DataSet InsertarRevision(Servicioes_RevisionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prebaja", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_prebaja });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            //listparameters.Add(new SqlParameter() { ParameterName = "@CADTOTAL", SqlDbType = SqlDbType.Int, Value = Entidad.Cadtotal });
            //listparameters.Add(new SqlParameter() { ParameterName = "@CADREVISIONES", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadobservciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@monto", SqlDbType = SqlDbType.Money, Value = Entidad.Monto });
            listparameters.Add(new SqlParameter() { ParameterName = "@observaciones", SqlDbType = SqlDbType.VarChar, Value = Entidad.Observaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_revisionser", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_revisionser });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apre_bajas_rev_servicio_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}