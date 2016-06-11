using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class OpcionesBL
    {
        public DataSet AcessosDirectos(Entidades.OpcionesE opcion)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = opcion.Usuario_id });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo_apli", SqlDbType = SqlDbType.Int, Value = "|5|" });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_opciones_usuario", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //29-07-2015
        public DataSet opciones_menu(Entidades.OpcionesE opcion)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu1", SqlDbType = SqlDbType.VarChar, Value = opcion.Menu1 });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu2", SqlDbType = SqlDbType.VarChar, Value = opcion.Menu2 });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu3", SqlDbType = SqlDbType.VarChar, Value = opcion.Menu3 });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu4", SqlDbType = SqlDbType.VarChar, Value = opcion.Menu4 });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu5", SqlDbType = SqlDbType.VarChar, Value = opcion.Menu5 });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu6", SqlDbType = SqlDbType.VarChar, Value = opcion.Menu6 });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnivel", SqlDbType = SqlDbType.Int, Value = opcion.Nivel });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = opcion.Usuario_id });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo_aplicacion", SqlDbType = SqlDbType.Int, Value = opcion.Tipo_apli });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_opciones_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet MenuDinmaico(Entidades.OpcionesE opcion)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = opcion.Usuario_id });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.Int, Value = opcion.Search });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_menu_dinamico_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataTable preparar_funcion(string funcion)
        {
            Datos data = new Datos();
            DataTable dt = new DataTable();

            try
            {
                dt = data.enviar_funcion(funcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}