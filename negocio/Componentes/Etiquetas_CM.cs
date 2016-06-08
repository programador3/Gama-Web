using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Etiquetas_CM
    {
        public DataSet Etiquetas(Etiquetas_EN Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfilgpo", SqlDbType = SqlDbType.Int, Value = Etiqueta.Grupo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_perfiles_etiquetas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Opciones(Etiquetas_EN Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfiletiq", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Idc_perfilgpoetiq });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_perfiles_etiquetas_opciones", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Bloqueos(Etiquetas_EN Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfiletiq", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Idc_perfilgpoetiq });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_perfiles_etiquetas_bloqueo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet InsertarEtiquetas(Etiquetas_EN entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfiletiq", SqlDbType = SqlDbType.Int, Value = entidad.Idc_perfilgpoetiq });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfilgpo", SqlDbType = SqlDbType.Int, Value = entidad.Grupo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre", SqlDbType = SqlDbType.VarChar, Value = entidad.Etiqueta });
            listparameters.Add(new SqlParameter() { ParameterName = "@pminimo_sel", SqlDbType = SqlDbType.SmallInt, Value = entidad.Minimo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmaximo_sel", SqlDbType = SqlDbType.SmallInt, Value = entidad.Maximo });
            listparameters.Add(new SqlParameter() { ParameterName = "@plibre", SqlDbType = SqlDbType.Bit, Value = entidad.Tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_opciones", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_opc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_total", SqlDbType = SqlDbType.Int, Value = entidad.Cadena_opc_total });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@POrden", SqlDbType = SqlDbType.Int, Value = entidad.POrden });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aperfiles_gpo_etiqueta", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet ActializarEtiquetas(Etiquetas_EN entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfiletiq", SqlDbType = SqlDbType.Int, Value = entidad.Idc_perfilgpoetiq });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfilgpo", SqlDbType = SqlDbType.Int, Value = entidad.Grupo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre", SqlDbType = SqlDbType.VarChar, Value = entidad.Etiqueta });
            listparameters.Add(new SqlParameter() { ParameterName = "@pminimo_sel", SqlDbType = SqlDbType.SmallInt, Value = entidad.Minimo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmaximo_sel", SqlDbType = SqlDbType.SmallInt, Value = entidad.Maximo });
            listparameters.Add(new SqlParameter() { ParameterName = "@plibre", SqlDbType = SqlDbType.Bit, Value = entidad.Tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_opciones", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_opc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_total", SqlDbType = SqlDbType.Int, Value = entidad.Cadena_opc_total });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@POrden", SqlDbType = SqlDbType.Int, Value = entidad.POrden });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aperfiles_gpo_etiqueta", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet EliminarEtiquetas(Etiquetas_EN entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfiletiq", SqlDbType = SqlDbType.Int, Value = entidad.Idc_perfilgpoetiq });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bperfiles_etiquetas", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet updateBloqueos(Etiquetas_EN Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_opciones", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Cadena_bloq });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_total", SqlDbType = SqlDbType.Int, Value = Etiqueta.Cadena_bloq_total });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aperfiles_etiqueta_bloq", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}