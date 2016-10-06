using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Compromisos_ClienteCOM
    {

        public DataSet Carga_T_Compromisos()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                ds = data.enviar("sp_combo_tipos_compromisos_cli", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //informacion de cliente  sp_datos_cliente
        public DataSet Datos_Cliente(Compromisos_ClienteENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listeparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listeparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", Value= ent.Pidc_cliente});
            try
            {
                ds = data.enviar("sp_datos_cliente", listeparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }
        
        public DataSet Clientes_Compromisos(Compromisos_ClienteENT ent, Datos_Usuario_logENT dul)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listeparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //@pidc_usuario
            listeparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", Value = dul.Pidc_usuario });
            listeparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", Value = ent.Pidc_cliente });
            try
            {
                ds = data.enviar("sp_clientes_compromisos", listeparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        
        public DataSet guardar_compromiso_cliente(Compromisos_ClienteENT ent, Datos_Usuario_logENT dul)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario",    Value = dul.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip",        Value = dul.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc",       Value = dul.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc",      Value = dul.Pusuariopc });

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente",    Value = ent.Pidc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tipocomcli", Value = ent.Pidc_tipocomcli });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcompromiso",     Value = ent.Pcompromiso });
            
            try
            {                   //sp_aclientes_compromisos
                ds = data.enviar("sp_aclientes_compromisos_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        //clientes_compromisos_revisar
        public DataSet clientes_compromisos_revisar(Compromisos_ClienteENT ent, Datos_Usuario_logENT dul)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario",    Value = dul.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip",        Value = dul.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc",       Value = dul.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc",      Value = dul.Pusuariopc });

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_clicompromiso",  Value = ent.Pidc_clicompromiso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobserv",             Value = ent.Pobserv });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcompletada",         Value = ent.Pcompletada });

            try
            {                   //sp_aclientes_compromisos_rev
                ds = data.enviar("sp_aclientes_compromisos_rev_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

    }
}
