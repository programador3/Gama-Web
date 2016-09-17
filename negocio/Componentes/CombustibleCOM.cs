using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class CombustibleCOM
    {
        public DataSet CargaQuejas(CombustibleENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pvalor", SqlDbType = SqlDbType.Int, Value = Entidad.Pvalor });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptodos", SqlDbType = SqlDbType.Int, Value = Entidad.Ptodos });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("selecciona_vehiculo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet ComprobarFolio(CombustibleENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pFOLIO", SqlDbType = SqlDbType.Int, Value = Entidad.Pfolio });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tipo_aut", SqlDbType = SqlDbType.Int, Value = Entidad.Ptipofolio });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_checar_folio_AUTORIZACION", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AgregarCarga(string[] parametros, object[] valores)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            for (int i = 0; i < parametros.Length; i++)
            {
                listparameters.Add(new SqlParameter() { ParameterName = parametros[i], SqlDbType = SqlDbType.Int, Value = valores[i] });
            }

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_acarga_combustible3_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}