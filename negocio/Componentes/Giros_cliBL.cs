using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Giros_cliBL
    {
        public DataSet giro_cli_cbox(bool web)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pweb", SqlDbType = SqlDbType.Bit, Value = web });

            try
            {
                ds = data.enviar("sp_combo_giros_cli", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}