using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Catalogo_DocumentosCOM
    {
        /// <summary>
        /// Carga Servicios por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaDocumentos(Catalogo_DocumentosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tipodoc", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_tipodoc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_catalogo_documentos_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Elimina documento
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet EliminaDocumento(Catalogo_DocumentosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tipodoc", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_tipodoc });
            listparameters.Add(new SqlParameter() { ParameterName = "@eliminar", SqlDbType = SqlDbType.Bit, Value = true });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_adocumentos_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Elimina documento
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet Agregar(Catalogo_DocumentosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@caddocs", SqlDbType = SqlDbType.VarChar, Value = Entidad.Caddocs });
            listparameters.Add(new SqlParameter() { ParameterName = "@numcad", SqlDbType = SqlDbType.Int, Value = Entidad.Numcad });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.Int, Value = Entidad.Descripcion });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_adocumentos_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Elimina documento
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet Editar(Catalogo_DocumentosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@caddocs", SqlDbType = SqlDbType.VarChar, Value = Entidad.Caddocs });
            listparameters.Add(new SqlParameter() { ParameterName = "@numcad", SqlDbType = SqlDbType.Int, Value = Entidad.Numcad });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = Entidad.Descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tipodoc", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_tipodoc });
            listparameters.Add(new SqlParameter() { ParameterName = "@editar", SqlDbType = SqlDbType.Bit, Value = true });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_adocumentos_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}