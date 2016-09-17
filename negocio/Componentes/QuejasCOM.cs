using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class QuejasCOM
    {
        public DataSet AgregarRecordatorio(QuejasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptexto", SqlDbType = SqlDbType.Int, Value = Entidad.Pobservaciones_satisfecho });
            listparameters.Add(new SqlParameter() { ParameterName = "@pasunto", SqlDbType = SqlDbType.Int, Value = Entidad.Pobservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcorreo", SqlDbType = SqlDbType.Int, Value = Entidad.Pencargado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = Entidad.Pfecha });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aavisos_generales_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet DescartarRecordatorio(QuejasENT Entidad, int tipo, int idc_avisogen, int num, int tiempo)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotreg", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_queja });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmotivo", SqlDbType = SqlDbType.Int, Value = Entidad.Pobservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena", SqlDbType = SqlDbType.Int, Value = Entidad.Pobservaciones_satisfecho });
            listparameters.Add(new SqlParameter() { ParameterName = "@Ptipo", SqlDbType = SqlDbType.Int, Value = tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUM", SqlDbType = SqlDbType.Int, Value = num });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTIEMPO", SqlDbType = SqlDbType.Int, Value = tiempo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bavisos_generales_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarHistorial(QuejasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_avisogen", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_queja });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aviso_generales_historial", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarRecordatorio(QuejasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_CONSULTA_AVISOS_GENERALES_PEND", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaQuejas(QuejasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_empleados });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_quejas_espera", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CancelarQuejas(QuejasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_queja", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_queja });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmotivo", SqlDbType = SqlDbType.Int, Value = Entidad.Pobservaciones });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aquejas_can_nuevo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet ComentarioQuejas(QuejasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_queja", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_queja });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcomentario", SqlDbType = SqlDbType.Int, Value = Entidad.Pobservaciones });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aquejas_com_nuevo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SolucionarQuejas(QuejasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_queja", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_queja });
            listparameters.Add(new SqlParameter() { ParameterName = "@pencargado", SqlDbType = SqlDbType.Int, Value = Entidad.Pencargado });
            listparameters.Add(new SqlParameter() { ParameterName = "@psolucion", SqlDbType = SqlDbType.Int, Value = Entidad.Pobservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_registro ", SqlDbType = SqlDbType.Int, Value = Entidad.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pno_satisfecho", SqlDbType = SqlDbType.Int, Value = Entidad.Pobservaciones_satisfecho });
            listparameters.Add(new SqlParameter() { ParameterName = "@psatisfecho", SqlDbType = SqlDbType.Int, Value = Entidad.Psatisfecho });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aquejas_solucion_nuevo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SDatosQuejas(QuejasENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_queja", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_queja });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_queja", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}