using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class PuestosCOM
    {
        /// <summary>
        /// Carga una lista de todos los puestos de la empresa
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DataSet CargaCatologoPuestos(PuestosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_Puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_USUARIO", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_catalogo_puestos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaPMD(PuestosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado_pmd", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_empleado_pmd });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_pmd_pendientes", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaFaltas(PuestosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_empleado_pmd });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_inicio", SqlDbType = SqlDbType.Int, Value = entidad.Pfecha_inicio });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_fin", SqlDbType = SqlDbType.Int, Value = entidad.Pfecha_fin });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_empleados_faltas_detalle", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Cambia el status de un puesto
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DataSet CambiarStatus(PuestosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_Puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pstatus", SqlDbType = SqlDbType.Int, Value = entidad.Pstatus });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Bit, Value = entidad.Pidc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Bit, Value = entidad.Pidc_pre_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_astatus_puestos_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SolicitudRemplazo(PuestosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_Puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.Int, Value = entidad.Pobservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_pre_empleado });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_asolicitud_reemplazo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SolicitudPDM(PuestosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.Int, Value = entidad.Pobservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_pre_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Int, Value = entidad.Ptipo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aempleado_pdm", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet EmpleadosFaltas(PuestosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.Int, Value = entidad.Pobservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pjustificacion", SqlDbType = SqlDbType.Int, Value = entidad.Pjustificante });
            listparameters.Add(new SqlParameter() { ParameterName = "@pasistencia", SqlDbType = SqlDbType.Int, Value = entidad.Pasistencia });
            listparameters.Add(new SqlParameter() { ParameterName = "@pextension", SqlDbType = SqlDbType.Int, Value = entidad.Pextension });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado_falta", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_empleado_pmd });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_eempleado_faltas", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SolicitudPDMACUERDO(PuestosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@acuerdo", SqlDbType = SqlDbType.Int, Value = entidad.Pobservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado_pmd", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_empleado_pmd });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_eempleado_pdm", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SolicitudPDMACUERDOSuspension(PuestosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@acuerdo", SqlDbType = SqlDbType.Int, Value = entidad.Pobservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado_pmd", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_empleado_pmd });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_inicio", SqlDbType = SqlDbType.Int, Value = entidad.Pfecha_inicio });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_fin", SqlDbType = SqlDbType.Int, Value = entidad.Pfecha_fin });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_eempleado_pdm", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// muestra los puestos activos para el modulo de puestos.aspxs
        /// </summary>
        /// <returns></returns>
        public DataSet puestos()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@idc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_Puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_puestos_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// muestra los puestos activos para el modulo de puestos.aspxs
        /// </summary>
        /// <returns></returns>
        public DataSet combo_puestos_perfil()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@idc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_Puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_puestos_perfil", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet puesto_perfil_temp_captura(PuestosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_Puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil", SqlDbType = SqlDbType.Int, Value = entidad.Idc_puestoperfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apuesto_perfil_temp", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// actualiza la columna de la tabla puestos(idc_puestoperfil) segun el valor relacionado en puesto_perfil_temp
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DataSet puesto_perfil_temp_update(PuestosENT entidad, bool aprobada)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_Puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@paprobada", SqlDbType = SqlDbType.Bit, Value = aprobada });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_puesto_perfil_update", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet puesto_aprob_detalle(PuestosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_Puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_puesto_aprob_detalle", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}