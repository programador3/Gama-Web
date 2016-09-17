using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class ReportingBL
    {
        #region Methods

        public DataSet path_reporte(Entidades.reportingE reporting)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_reporting", SqlDbType = SqlDbType.Int, Value = reporting.Idc_reporting });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_reporting", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        #endregion Methods
    }
}