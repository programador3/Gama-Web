using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class OrganigramaBL
    {
        public DataSet sp_ajax(string name_sp, string[] parametros, object[] valores, bool tran)
        {
            DataSet ds = new DataSet();
            Datos data = new Datos();

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar_ajax(name_sp, parametros, valores, tran);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaPath(UsuariosE Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pcod_archivo", SqlDbType = SqlDbType.Char, Value = Etiqueta.Cod_arch });
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
    }
}