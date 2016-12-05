using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class comidasCOM
    {
        public DataSet CargarEmpleados(comidasENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", Value = ent.Pfecha });

            try
            {
                ds = data.enviar("sp_comidas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;

        }

        public DataSet GuardarRegistro(comidasENT ent, Datos_Usuario_logENT dul)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", Value = ent.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_empleados", Value = ent.Pcadena_empleados });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena", Value = ent.Ptotal_cadena });

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = dul.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = dul.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = dul.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = dul.Pusuariopc });

            try
            {
                ds = data.enviar("sp_acomidas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;

        }

    }
}
