using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
   public class reproductor_llamadasCOM
    {

        public DataSet Consulta_llamada( reproductor_llamadasENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_llamada", SqlDbType = SqlDbType.Int, Value = ent.Pidc_llamada });
            try
            {
                ds = data.enviar("sp_llamadas_marcar", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;

        }

        public DataSet Marcar(Datos_Usuario_logENT dul, reproductor_llamadasENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_llamadamarcar", SqlDbType = SqlDbType.Int, Value = ent.Pidc_llamadamarcar });

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_llamada", SqlDbType = SqlDbType.Int, Value = ent.Pidc_llamada });
            listparameters.Add(new SqlParameter() { ParameterName = "@pObservaciones", SqlDbType = SqlDbType.Int, Value = ent.PObservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Int, Value = ent.Ptipo });
            //listparameters.Add(new SqlParameter() { ParameterName = "@pborrado", SqlDbType = SqlDbType.Int, Value = ent.Pborrado });

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = dul.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = dul.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = dul.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = dul.Pusuariopc });
            Datos data = new Datos();
            try
            {
                ds = data.enviar("sp_allamadas_marcar", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
