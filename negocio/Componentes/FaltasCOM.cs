using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class FaltasCOM
    {
        public DataSet CargaPrep(FaltasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_EMPLEADO_FALTA", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleado_falta });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PUESTO", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_empleados_faltas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}