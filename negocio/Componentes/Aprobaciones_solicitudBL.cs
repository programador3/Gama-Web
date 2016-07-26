using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Aprobaciones_solicitudBL
    {
        public DataSet nueva_solicitud_aprobacion(Aprobaciones_solicitudE entidad, int idc_puestoperfil = 0)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_aprobacion", SqlDbType = SqlDbType.Int, Value = entidad.Idc_aprobacion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_registro", SqlDbType = SqlDbType.Int, Value = entidad.Idc_registro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_aprobacion_soli", SqlDbType = SqlDbType.Int, Value = entidad.Idc_aprobacion_soli });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            if (idc_puestoperfil > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil", SqlDbType = SqlDbType.Int, Value = idc_puestoperfil });
            }
            try
            {
                ds = data.enviar("sp_aaprobaciones_solicitud", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet solicitud_aprobacion_adicional(Aprobaciones_solicitudE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_aprobacion_soli", SqlDbType = SqlDbType.Int, Value = entidad.Idc_aprobacion_soli });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil", SqlDbType = SqlDbType.Int, Value = entidad.Idc_registro });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aaprobaciones_firmas_adicionales", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarGrupos(Aprobaciones_solicitudE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil_borr", SqlDbType = SqlDbType.Int, Value = entidad.Idc_registro });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_grupos_relacionados_perfil_borr", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Cargarpp(Aprobaciones_solicitudE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfilsol", SqlDbType = SqlDbType.Int, Value = entidad.Idc_registro });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_solicitudes_perfiles_similares", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet CargarSubProcesos(Aprobaciones_solicitudE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PROCESO_BORR", SqlDbType = SqlDbType.Int, Value = entidad.Idc_registro });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_subprocesos_relacionados", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet solicitud_aprobacion_adicional_cursos(Aprobaciones_solicitudE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_aprobacion_soli", SqlDbType = SqlDbType.Int, Value = entidad.Idc_aprobacion_soli });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso_borr", SqlDbType = SqlDbType.Int, Value = entidad.Idc_registro });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_aprobaciones_adicionales", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet solicitud_aprobacion_adicional_ligarperfil(Aprobaciones_solicitudE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_aprobacion_soli", SqlDbType = SqlDbType.Int, Value = entidad.Idc_aprobacion_soli });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_registro });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ligarperfil_aprobaciones_adicionales", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet solicitud_aprobacion_terminada(Aprobaciones_solicitudE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_aprobacion_soli", SqlDbType = SqlDbType.Int, Value = entidad.Idc_aprobacion_soli });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aprobaciones_completa", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// metodo que recupera la fila de un registro segun el idc_aprobacion_soli
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DataSet solicitud_aprobacion_detalle(Aprobaciones_solicitudE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_aprobacion_soli", SqlDbType = SqlDbType.Int, Value = entidad.Idc_aprobacion_soli });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aprobacion_solicitud_detalle", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet solicitud_aprobacion_cancelar(Aprobaciones_solicitudE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_aprobacion_soli", SqlDbType = SqlDbType.Int, Value = entidad.Idc_aprobacion_soli });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcomentarios", SqlDbType = SqlDbType.VarChar, Value = entidad.Comentarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            try
            {
                ds = data.enviar("sp_aprobacion_solicitud_cancelar", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}