﻿using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class UsuariosBL
    {
        public DataSet CargaInformacionInicial()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                ds = data.enviar("sp_informacion_checkmail", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet Informacion_Correo(int IDC_USUARIO)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Char, Value = IDC_USUARIO });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_informacion_correo_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet validar_usuarios(Entidades.UsuariosE datos)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pusuario", SqlDbType = SqlDbType.Char, Value = datos.Usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcontraseña", SqlDbType = SqlDbType.Char, Value = datos.Contraseña });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_validar_usuario", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //public DataSet usuario_detalle(Entidades.UsuariosE datos)
        //{
        //    DataSet ds = new DataSet();
        //    List<SqlParameter> listparameters = new List<SqlParameter>();
        //    Datos data = new Datos();

        //    listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Char, Value = datos.Usuario });

        //    try
        //    {
        //        //ds = data.datos_Clientes(listparameters);
        //        ds = data.enviar("sp_usuario_detalle", listparameters, false);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return ds;

        //}
    }
}