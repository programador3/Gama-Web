using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Uni_archiBL
    {
        #region methods

        public DataSet datos_uni_archi(Entidades.uni_archiE uni)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pcod_archivo", SqlDbType = SqlDbType.Int, Value = uni.Cod_archivo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_uni_archi", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        #endregion methods
    }
}