using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class comisiones_mCOM
    {

        public DataSet agentes_vs_usuarios(Datos_Usuario_logENT dul)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", Value = dul.Pidc_usuario });
            try
            {
                ds = data.enviar("sp_combo_agentes_usu", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet comisiones_agente(comisiones_mENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pagente", SqlDbType = SqlDbType.Int, Value = ent.Pidc_agente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmes", SqlDbType = SqlDbType.Int, Value = ent.Pmes });
            listparameters.Add(new SqlParameter() { ParameterName = "@paño", SqlDbType = SqlDbType.Int, Value = ent.Panio });
            listparameters.Add(new SqlParameter() { ParameterName = "@aldia", SqlDbType = SqlDbType.Int, Value = ent.Paldia });
            try
            {
                ds = data.enviar("sp_comisiones_nueva13", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet comisiones_esp_articulos(Datos_Usuario_logENT dul,comisiones_mENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfechai", SqlDbType = SqlDbType.Int, Value = ent.Pfechai });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfechaf", SqlDbType = SqlDbType.Int, Value = ent.Pfechaf });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = dul.Pidc_usuario });
            try
            {
                ds = data.enviar("sp_reporte_comision_vales_articulo_nuevo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
            
        }

        public DataSet comisiones_esp_activaciones(Datos_Usuario_logENT dul, comisiones_mENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfechai", SqlDbType = SqlDbType.Int, Value = ent.Pfechai });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfechaf", SqlDbType = SqlDbType.Int, Value = ent.Pfechaf });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = dul.Pidc_usuario });
            try
            {
                ds = data.enviar("sp_reporte_comision_vales_articulo_activacion", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
            
        }

    }
}
