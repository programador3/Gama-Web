using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class HtmlCOM
    {
        public DataSet AgregarHTML(HtmlENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pcontenido", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Content });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptitulo", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Titulo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ahtml", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Editar(HtmlENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pcontenido", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Content });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptitulo", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Titulo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_html", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_html });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ahtml", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Eliminar(HtmlENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pcontenido", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Content });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptitulo", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Titulo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_html", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_html });
            listparameters.Add(new SqlParameter() { ParameterName = "@delete", SqlDbType = SqlDbType.Bit, Value = 1 });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ahtml", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SelectHTML(HtmlENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_select_htmlfiles", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SelectHTMLEspecifico(HtmlENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_html", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_html });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_select_htmlfiles", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}