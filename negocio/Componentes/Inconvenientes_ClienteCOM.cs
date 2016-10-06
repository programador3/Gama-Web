using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Inconvenientes_ClienteCOM
    {

        public DataSet Datos_Cliente(Inconvenientes_ClienteENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listeparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listeparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", Value = ent.Pidc_cliente });
            try
            {
                ds = data.enviar("sp_datos_cliente", listeparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet Inconvenientes(Inconvenientes_ClienteENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listeparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listeparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", Value = ent.Pidc_cliente });
            try
            {
                ds = data.enviar("sp_clientes_inconvenientes", listeparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet guardar_compromiso_cliente(Inconvenientes_ClienteENT ent, Datos_Usuario_logENT dul)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", Value = dul.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", Value = dul.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", Value = dul.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", Value = dul.Pusuariopc });

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", Value = ent.Pidc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pinconveniente", Value = ent.Pinconveniente });

            try
            {                   //sp_aclientes_inconvenientes
                ds = data.enviar("sp_aclientes_inconvenientes_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        
    }
}
