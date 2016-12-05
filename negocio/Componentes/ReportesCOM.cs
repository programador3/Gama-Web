using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class ReportesCOM
    {
        public DataSet SP_REPORTE_EVALUACION_CHOFERES(int mes, int año)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pmes", SqlDbType = SqlDbType.Int, Value = mes });
            listparameters.Add(new SqlParameter() { ParameterName = "@paño", SqlDbType = SqlDbType.Int, Value = año });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_REPORTE_EVALUACION_CHOFERES", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Carga(ReportesENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleado });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_tipos_reportes_empleados", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_reporte_incidencias_concentrado(ReportesENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleados });
            //listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = Entidad.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_inicio", SqlDbType = SqlDbType.Int, Value = Entidad.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_fin", SqlDbType = SqlDbType.Int, Value = Entidad.Pfechafin });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_reporte_incidencias_concentrado", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet CargaJefe(ReportesENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleados });
            //listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = Entidad.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleadorep", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleadorep });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_reportes_falta_vobo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet CargaJefeFechas(ReportesENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleados });
            //listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = Entidad.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleadorep", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleadorep });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_inicio", SqlDbType = SqlDbType.Int, Value = Entidad.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_fin", SqlDbType = SqlDbType.Int, Value = Entidad.Pfechafin });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_reportes_falta_vobo", listparameters, false);
            }
            catch (Exception ex)
            {          
                throw ex;
            }
            return ds;
        }
        public DataSet AgregarReporte(ReportesENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tiporep", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_tiporep });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.Int, Value = Entidad.PObservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleadomio", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pidc_empleadoalta });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADENA", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pcadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotalcad", SqlDbType = SqlDbType.VarChar, Value = Entidad.Ptotal_cadena });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_areportes_empleados", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet TerminarReporte(ReportesENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleadorep", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleadorep });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tiporep", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_tiporep });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.Int, Value = Entidad.PObservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleadomio", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pidc_empleadoalta });
            listparameters.Add(new SqlParameter() { ParameterName = "@preasigna", SqlDbType = SqlDbType.VarChar, Value = Entidad.preasigna });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTIPOTERMINA", SqlDbType = SqlDbType.VarChar, Value = "T" });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcerrado", SqlDbType = SqlDbType.Char, Value =Entidad.PCERRADO });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADENA", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pcadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotalcad", SqlDbType = SqlDbType.VarChar, Value = Entidad.Ptotal_cadena });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_areportes_empleados", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet VoboReporte(ReportesENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleadovobo", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleadorep", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleadorep });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.Int, Value = Entidad.PObservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcerrado", SqlDbType = SqlDbType.Char, Value = Entidad.PCERRADO });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_areportes_empleados", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}