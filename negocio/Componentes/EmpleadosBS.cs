using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace negocio.Componentes
{
    public class EmpleadosBL
    {
        public DataSet checadas(Entidades.nominaE nom)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pnum_nomina", SqlDbType = SqlDbType.Int, Value = nom.Num_nomina });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aasistencia_foto_try", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return ds;
        }
    }
}
