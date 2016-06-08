using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Catalogo_AvisosCOM
    {
        /// <summary>
        /// Carga Servicios por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaAvisos(Catalogo_AvisosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_taviso", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_taviso });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_tipo_avisos_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Carga Servicios por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet AgregaAviso(Catalogo_AvisosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.Int, Value = Entidad.Descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@cadavisos", SqlDbType = SqlDbType.Int, Value = Entidad.Cadaviso });
            listparameters.Add(new SqlParameter() { ParameterName = "@numcad", SqlDbType = SqlDbType.Int, Value = Entidad.NumCad });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aavisos_tipo_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Carga Servicios por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet EditarAviso(Catalogo_AvisosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.Int, Value = Entidad.Descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@cadavisos", SqlDbType = SqlDbType.Int, Value = Entidad.Cadaviso });
            listparameters.Add(new SqlParameter() { ParameterName = "@numcad", SqlDbType = SqlDbType.Int, Value = Entidad.NumCad });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_taviso", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_taviso });
            listparameters.Add(new SqlParameter() { ParameterName = "@editar", SqlDbType = SqlDbType.Bit, Value = true });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aavisos_tipo_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Carga Servicios por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet EliminaAviso(Catalogo_AvisosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_taviso", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_taviso });
            listparameters.Add(new SqlParameter() { ParameterName = "@eliminar", SqlDbType = SqlDbType.Bit, Value = true });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aavisos_tipo_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}