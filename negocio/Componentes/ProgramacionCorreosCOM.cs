using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class ProgramacionCorreosCOM
    {
        public DataSet CargaProgn(ProgramacionCorreosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pfiltro", SqlDbType = SqlDbType.Int, Value = Etiqueta.Filtro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_progracorreo", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_progracorreo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_programacion_correo_d", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AutorizarCorreo(ProgramacionCorreosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pfiltro", SqlDbType = SqlDbType.Int, Value = Etiqueta.Filtro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_progracorreo", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_progracorreo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pobservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pstatus", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptipo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aprogramacion_correo_sta", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}