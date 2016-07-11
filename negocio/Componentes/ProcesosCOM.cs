using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class ProcesosCOM
    {
        public DataSet CargaPerfiles(ProcesosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfiltro", SqlDbType = SqlDbType.Int, Value = entidad.Ptioo });
            try
            {
                ds = data.enviar("sp_combo_puestos_perfil", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CatalogoProcesos(ProcesosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_proceso", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_proceso });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_USUARIO", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            try
            {
                ds = data.enviar("sp_catalogo_procesos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CatalogoProcesosbORR(ProcesosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_proceso", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_proceso });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_USUARIO", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            try
            {
                ds = data.enviar("sp_catalogo_procesos_borr", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AgregarSubProcesos(ProcesosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PROCESO_borr", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_proceso });
            listparameters.Add(new SqlParameter() { ParameterName = "@PDESCRIPCION_PROCESO", SqlDbType = SqlDbType.Int, Value = entidad.Pdescripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena", SqlDbType = SqlDbType.Int, Value = entidad.Pcadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotalcadena", SqlDbType = SqlDbType.Int, Value = entidad.Ptotalcadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_sub", SqlDbType = SqlDbType.Int, Value = entidad.Pcadenasubpro });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotalcadena_sub", SqlDbType = SqlDbType.Int, Value = entidad.Ptotalcadenasub });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_perfil", SqlDbType = SqlDbType.Int, Value = entidad.Pcadenaperf });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotalcadena_perfil", SqlDbType = SqlDbType.Int, Value = entidad.Ptotalcadenaperf });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            try
            {
                ds = data.enviar("sp_a_subprocesos_borr", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet EliminarProcesos(ProcesosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PROCESO", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_proceso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@PBORRADOR", SqlDbType = SqlDbType.VarChar, Value = entidad.Pborrador });
            try
            {
                ds = data.enviar("sp_e_subprocesos_borr_PROD", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}