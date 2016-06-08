using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Celulares_RevisionesCOM
    {
        /// <summary>
        /// Carga Herramientas por revision
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaHerramientasRevision(Celulares_RevisionesENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestorevi", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puestorevi });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoprebaja", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puestoprebaja });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_revision_celulares", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CelualresDispo(Celulares_RevisionesENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_celulares_disponibilidad", listparameters, false);
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
        public DataSet InsertarRevisionCelular(Celulares_RevisionesENT Entidad)
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
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADACC", SqlDbType = SqlDbType.VarChar, Value = Entidad.PCADACC });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMACC", SqlDbType = SqlDbType.Int, Value = Entidad.PNUMACC });
            listparameters.Add(new SqlParameter() { ParameterName = "@PMONTO", SqlDbType = SqlDbType.Money, Value = Entidad.PMONTO });
            listparameters.Add(new SqlParameter() { ParameterName = "@POBSERVACIONES", SqlDbType = SqlDbType.VarChar, Value = Entidad.Comentario });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apre_bajas_rev_herramientas_celulares_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}