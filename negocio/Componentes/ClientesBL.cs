using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class ClientesBL
    {
        #region methods

        public DataSet datos_clientes(Entidades.clientesE clientes)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = clientes.Idc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_clientes", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet alta_clientes(Entidades.clientesE clientes)

        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = clientes.Idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre", SqlDbType = SqlDbType.VarChar, Value = clientes.Nombre });
            listparameters.Add(new SqlParameter() { ParameterName = "@prfc", SqlDbType = SqlDbType.VarChar, Value = clientes.Rfc });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptelefono", SqlDbType = SqlDbType.VarChar, Value = clientes.Telefono });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcorreo", SqlDbType = SqlDbType.VarChar, Value = clientes.Correo });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aclientes", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet borrar_clientes(Entidades.clientesE clientes)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = clientes.Idc_cliente });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bclientes", listparameters, true);
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