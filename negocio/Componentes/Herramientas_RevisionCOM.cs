using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Herramientas_RevisionCOM
    {
        /// <summary>
        /// Carga Herramientas por revision
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaHerramientasRevision(Herramientas_RevisionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestorevi", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puestorevi });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoprebaja", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puestoprebaja });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_revision_activos", listparameters, false);
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
        public DataSet InsertarRevision(Herramientas_RevisionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prebaja", SqlDbType = SqlDbType.Int, Value = Entidad.PIDC_prebaja });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADTEL", SqlDbType = SqlDbType.VarChar, Value = Entidad.PCADTEL });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMTEL", SqlDbType = SqlDbType.Int, Value = Entidad.PNUMTEL });
            listparameters.Add(new SqlParameter() { ParameterName = "@PMONTO", SqlDbType = SqlDbType.Money, Value = Entidad.PMONTO });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apre_bajas_rev_activo_nuevo_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}