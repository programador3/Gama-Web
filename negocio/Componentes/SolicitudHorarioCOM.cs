using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class SolicitudHorarioCOM
    {
        public DataSet Solcitud(Solicitud_HorarioENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pobservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pno_salida", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pno_salida });
            listparameters.Add(new SqlParameter() { ParameterName = "@pno_comida", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pno_comida });
            if (Etiqueta.Pidc_sucursal > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pidc_sucursal });
            }
            if (Etiqueta.Phora_entrada > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@phora_entrada", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Phora_entrada });
            }
            if (Etiqueta.Phora_salida > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@phora_salida", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Phora_salida });
            }
            if (Etiqueta.Phora_entrada_comida > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@phora_entrada_comida", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Phora_entrada_comida });
            }
            if (Etiqueta.Phora_salida_comida > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@phora_salida_comida", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Phora_salida_comida });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apermisos_horarios", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SolcitudEdicion(Solicitud_HorarioENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pobservaciones });
            if (Etiqueta.Pidc_sucursal > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pidc_sucursal });
            }
            if (Etiqueta.Phora_entrada > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@phora_entrada", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Phora_entrada });
            }
            if (Etiqueta.Phora_salida > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@phora_salida", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Phora_salida });
            }
            if (Etiqueta.Phora_entrada_comida > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@phora_entrada_comida", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Phora_entrada_comida });
            }
            if (Etiqueta.Phora_salida_comida > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@phora_salida_comida", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Phora_salida_comida });
            }
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_horario_perm", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pidc_horario_erm });
            listparameters.Add(new SqlParameter() { ParameterName = "@pstatus", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pstatus });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apermisos_horarios", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Autorizar(Solicitud_HorarioENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pobservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pidc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_horario_perm", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pidc_horario_erm });
            listparameters.Add(new SqlParameter() { ParameterName = "@pstatus", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pstatus });
            if (Etiqueta.Phora_entrada > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@phora_entrada", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Phora_entrada });
            }
            if (Etiqueta.Phora_salida > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@phora_salida", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Phora_salida });
            }
            if (Etiqueta.Phora_entrada_comida > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@phora_entrada_comida", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Phora_entrada_comida });
            }
            if (Etiqueta.Phora_salida_comida > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@phora_salida_comida", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Phora_salida_comida });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apermisos_horarios", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SolcitudDetalles(Solicitud_HorarioENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_horario_perm", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_horario_erm });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_solicitud_permiso_horarios", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet ComprobaciondeDia(Solicitud_HorarioENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_horario_perm", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_horario_erm });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_empleado });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_solicitud_permiso_horarios", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}