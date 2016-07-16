using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class VisitasCOM
    {
        public DataSet CargaPerfiles(VisitasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfiltro", SqlDbType = SqlDbType.Int, Value = entidad.Pnombre });
            try
            {
                ds = data.enviar("sp_visitas_personas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CragarVisitas(VisitasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_empleado });
            try
            {
                ds = data.enviar("sp_visitas_registros", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CragarVisitasReporte(VisitasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfechainicio", SqlDbType = SqlDbType.Int, Value = entidad.pfi });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfechafin", SqlDbType = SqlDbType.Int, Value = entidad.pf2 });
            try
            {
                ds = data.enviar("sp_visitas_registros", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaeMPRESA(VisitasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfiltro", SqlDbType = SqlDbType.Int, Value = entidad.Pnombre });
            try
            {
                ds = data.enviar("sp_visitas_empresa", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AgregarVisita(VisitasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrevisita", SqlDbType = SqlDbType.Int, Value = entidad.Pnombre });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombremepresa", SqlDbType = SqlDbType.Int, Value = entidad.Pnombreempresa });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@PMOTIVO", SqlDbType = SqlDbType.Int, Value = entidad.Pmotivo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_vistap", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_visitap });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_vistaemp", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_visitaemp });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            try
            {
                ds = data.enviar("sp_a_registros_visitas", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet TerminarVisita(VisitasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_visitareg", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_visitareg });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.VarChar, Value = entidad.Pmotivo });
            try
            {
                ds = data.enviar("sp_terminar_registros_visitas", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}