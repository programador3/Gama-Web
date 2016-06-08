using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class AprobacionesCOM
    {
        /// <summary>
        /// Carga lista de Aprobaciones. Retorna DataSet
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaCatalogoAprobaciones(AprobacionesENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_aprobacion", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_aprobacion });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_catalogo_aprobaciones", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// LLama SP que elimina Aprobacion. Retorna DataSet
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet EliminarAprobaciones(AprobacionesENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_aprobacion", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_aprobacion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pborrador", SqlDbType = SqlDbType.Bit, Value = Entidad.Borrado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            try
            {
                ds = data.enviar("sp_baprobaciones", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //======================================================  MIC 03-10-2015  ==================================================================
        /// <summary>
        /// firma por parte del usuario
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DataSet usuarios_firma(int pidc_aprobacion_soli)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_perfil", SqlDbType = SqlDbType.Int, Value = entidad.Idc_puestoperfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_aprobacion_soli", SqlDbType = SqlDbType.Int, Value = pidc_aprobacion_soli });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                //ds = data.enviar("sp_puestos_padre_usuario", listparameters, false);
                ds = data.enviar("sp_puestos_por_firmar", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>

        public DataSet validar_firma(string usuario, string password, bool aprobado, int idc_aprobacion_reg, string comentarios, int Idc_usuario, string Pdirecip, string Pnombrepc, string Pusuariopc)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@puser", SqlDbType = SqlDbType.Char, Value = usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@ppassword", SqlDbType = SqlDbType.Char, Value = password });
            listparameters.Add(new SqlParameter() { ParameterName = "@paprobado", SqlDbType = SqlDbType.Bit, Value = aprobado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcomentarios", SqlDbType = SqlDbType.Bit, Value = comentarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_aprobacion_reg", SqlDbType = SqlDbType.Int, Value = idc_aprobacion_reg });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Pusuariopc });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_validar_firmas", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet aprobaciones_pendientes(AprobacionesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aprobaciones_pendientes", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}