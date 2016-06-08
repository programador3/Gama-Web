using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class NotificacionesCOM
    {
        public DataSet CargaNotificaciones(NotificacionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_notificaciones_usuario", listparameters, false);
                //ds = data.enviar("sp_notificaciones", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaSlack(NotificacionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_enviar_slack_pendientes", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetName(NotificacionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pweb_form", SqlDbType = SqlDbType.Int, Value = Etiqueta.ppara });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_getname_webform", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaMenuHerr(NotificacionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_USUARIO", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cargamenu_herramientas_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaUsuarios(NotificacionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_usuarios", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet NuevoMensajeGeneral(NotificacionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptexto", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.ptexto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pasunto", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.pasunto });
            listparameters.Add(new SqlParameter() { ParameterName = "@ppara", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.ppara });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aavisos_gen_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaAvisos(NotificacionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_checa_avisos_nuevos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}