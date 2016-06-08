using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Grupos_backendBL
    {
        public DataSet grupos(Grupos_backendE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfilgpo", SqlDbType = SqlDbType.VarChar, Value = entidad.Idc_perfilgpo });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_perfiles_grupos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet grupos_tablas(Grupos_backendE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfilgpo", SqlDbType = SqlDbType.VarChar, Value = entidad.Idc_perfilgpo });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_perfil_grupos_tablas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet cargar_droplist()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_perfiles_gpo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet InsertarGrupos(Grupos_backendE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfilgpo", SqlDbType = SqlDbType.SmallInt, Value = entidad.Idc_perfilgpo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pgrupo", SqlDbType = SqlDbType.VarChar, Value = entidad.Grupo });
            listparameters.Add(new SqlParameter() { ParameterName = "@plibre", SqlDbType = SqlDbType.Bit, Value = entidad.Libre });
            listparameters.Add(new SqlParameter() { ParameterName = "@pminimo_libre", SqlDbType = SqlDbType.SmallInt, Value = entidad.Minimo_libre });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmaximo_libre", SqlDbType = SqlDbType.SmallInt, Value = entidad.Maximo_libre });
            listparameters.Add(new SqlParameter() { ParameterName = "@popciones", SqlDbType = SqlDbType.Bit, Value = entidad.Opciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pminimo_opc", SqlDbType = SqlDbType.SmallInt, Value = entidad.Minimo_opc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmaximo_opc", SqlDbType = SqlDbType.SmallInt, Value = entidad.Maximo_opc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_opciones", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_total", SqlDbType = SqlDbType.Int, Value = entidad.Cadena_Total });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@porden", SqlDbType = SqlDbType.Int, Value = entidad.Orden });
            //add 21-01-2016
            listparameters.Add(new SqlParameter() { ParameterName = "@pexterno", SqlDbType = SqlDbType.Bit, Value = entidad.Externo });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aperfiles_gpo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Eliminar grupo segun el id
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DataSet eliminarGrupos(Grupos_backendE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfilgpo", SqlDbType = SqlDbType.VarChar, Value = entidad.Idc_perfilgpo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bperfiles_grupos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}