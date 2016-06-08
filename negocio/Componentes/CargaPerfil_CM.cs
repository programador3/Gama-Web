using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class CargaPerfil_CM
    {
        public DataSet CargaPuestosPerfil(CargaPerfil_EN Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_perfil", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaArchivosHTML(PerfilesE Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_perfilborr", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puestoperfil_borr });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_etiquetas_borr_archivo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaArchivosHTMLPRODUCCION(PerfilesE Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_perfil", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_perfil });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_etiquetas_pro_archivo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaPuestosPerfil_Descripcion(CargaPerfil_EN Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@id_descripcion", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Descripcion });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_perfil", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet ComparaPerfiles(PerfilesE Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil_borr", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puestoperfil_borr });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_perfil", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaPuestosPerfilBorrador(PerfilesE Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil_borr", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puestoperfil_borr });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_perfil", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}