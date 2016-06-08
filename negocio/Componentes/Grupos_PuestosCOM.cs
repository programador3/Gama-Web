using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Grupos_PuestosCOM
    {
        /// <summary>
        /// Carga Puestos Activos
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaPuesto(Grupos_PuestosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfiltro", SqlDbType = SqlDbType.VarChar, Value = Entidad.Filtro });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_gpopuestos_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Carga Puestos Activos
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaGruposEditar(Grupos_PuestosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_gpo", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puesto_gpo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_gpopuestos_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Carga Grupos
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaGrupos_Puestos(Grupos_PuestosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_grupos_puestos_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Elimina Grupos
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet EliminarGrupo(Grupos_PuestosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_gpo", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puesto_gpo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bpuestos_gpo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Agrega Grupos
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet AgregarGrupo(Grupos_PuestosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@cadpuestos", SqlDbType = SqlDbType.Int, Value = Entidad.CadenaPuestos });
            listparameters.Add(new SqlParameter() { ParameterName = "@num_cadpuestos", SqlDbType = SqlDbType.VarChar, Value = Entidad.Num_cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@pgrupo", SqlDbType = SqlDbType.VarChar, Value = Entidad.Descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apuestosgpo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Edita Grupos
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet EditarGrupo(Grupos_PuestosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@cadpuestos", SqlDbType = SqlDbType.Int, Value = Entidad.CadenaPuestos });
            listparameters.Add(new SqlParameter() { ParameterName = "@num_cadpuestos", SqlDbType = SqlDbType.VarChar, Value = Entidad.Num_cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@pgrupo", SqlDbType = SqlDbType.VarChar, Value = Entidad.Descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_gpo", SqlDbType = SqlDbType.VarChar, Value = Entidad.Idc_puesto_gpo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_mpuestos_gpo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}