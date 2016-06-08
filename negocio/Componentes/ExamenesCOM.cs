using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class ExamenesCOM
    {
        /// <summary>
        /// Regresa tabla de examenes
        /// </summary>
        /// <param name="Etiqueta"></param>
        /// <returns></returns>
        public DataSet CargaExamenes(ExamenesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_examenes", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaExamenesEdicion(ExamenesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_examen", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_examen });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_examenes_edicion", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Elimina un registro
        /// </summary>
        /// <param name="Etiqueta"></param>
        /// <returns></returns>
        public DataSet BorrarExamen(ExamenesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_examen", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_examen });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bexamenes", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Elimina un registro
        /// </summary>
        /// <param name="Etiqueta"></param>
        /// <returns></returns>
        public DataSet Agregar(ExamenesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@cadena_archivos", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@total_cadena", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Total_cadena });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aexamenes", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Editar(ExamenesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_examen", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_examen });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@cadena_archivos", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@total_cadena", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Total_cadena });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_mexamenes", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}