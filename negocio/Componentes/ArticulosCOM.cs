using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace negocio.Componentes
{
    public class ArticulosCOM
    {
        public DataSet CargaArticulos(ArticulosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_articulo });           
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ejemplo_articulo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

    }
}
