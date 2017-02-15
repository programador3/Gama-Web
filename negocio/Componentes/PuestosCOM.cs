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
        public DataSet SP_COMBO_TABULADORES_SUELDOS()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_COMBO_TABULADORES_SUELDOS", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_combo_horariosg()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_horariosg", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_pre_puestos_autorizar(string cadena, int total_cadena, int idc_usuario, string observaciones)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena", SqlDbType = SqlDbType.Int, Value = cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena", SqlDbType = SqlDbType.Int, Value = total_cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.Int, Value = observaciones });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_pre_puestos_autorizar", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_status_pre_puestos(int idc_pre_puesto, string status, int idc_usuario)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_pre_puesto", SqlDbType = SqlDbType.Int, Value = idc_pre_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pstatus", SqlDbType = SqlDbType.Int, Value = status });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_status_pre_puestos", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_e_pre_puestos(int idc_pre_puesto, string status, int idc_usuario)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_pre_puesto", SqlDbType = SqlDbType.Int, Value = idc_pre_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pstatus", SqlDbType = SqlDbType.Int, Value = status });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_e_pre_puestos", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_pre_puestos(int idc_pre_puesto, int tipo)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_pre_puesto", SqlDbType = SqlDbType.Int, Value = idc_pre_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo_catalogo", SqlDbType = SqlDbType.Int, Value = tipo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_pre_puestos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_mpre_puestos(int idc_pre_puesto, string descripcion, string clave, int idc_puesto_jefe, int idc_depto, int idc_sucursal, int idc_uniforme, int idc_usuario, string cadena, int total_cadena, 
            int idc_perfil,            int idc_horariog, int idc_tabulador, int dias_reclu)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_pre_puesto", SqlDbType = SqlDbType.Int, Value = idc_pre_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@PDESCRIPCION", SqlDbType = SqlDbType.Int, Value = descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCLAVE", SqlDbType = SqlDbType.Int, Value = clave });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PUESTO_JEFE", SqlDbType = SqlDbType.Int, Value = idc_puesto_jefe });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_DEPTO", SqlDbType = SqlDbType.Int, Value = idc_depto });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_SUCURSAL", SqlDbType = SqlDbType.Int, Value = idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tabulador", SqlDbType = SqlDbType.Int, Value = idc_tabulador });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_horariog", SqlDbType = SqlDbType.Int, Value = idc_horariog });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_UNIFORME", SqlDbType = SqlDbType.Int, Value = idc_uniforme });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_USUARIO", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfil", SqlDbType = SqlDbType.Int, Value = idc_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADENA", SqlDbType = SqlDbType.Int, Value = cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTOTAL_CADENA", SqlDbType = SqlDbType.Int, Value = total_cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdias_reclutar", SqlDbType = SqlDbType.Int, Value = dias_reclu });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_mpre_puestos", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_apre_puestos(string descripcion,string clave, int idc_puesto_jefe,int  idc_depto,int idc_sucursal, int idc_uniforme, int idc_usuario, string cadena, int total_cadena, int idc_perfil ,
            int idc_horariog, int idc_tabulador, int dias_reclu)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PDESCRIPCION", SqlDbType = SqlDbType.Int, Value = descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCLAVE", SqlDbType = SqlDbType.Int, Value = clave });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PUESTO_JEFE", SqlDbType = SqlDbType.Int, Value = idc_puesto_jefe });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_DEPTO", SqlDbType = SqlDbType.Int, Value = idc_depto });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_SUCURSAL", SqlDbType = SqlDbType.Int, Value = idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_UNIFORME", SqlDbType = SqlDbType.Int, Value = idc_uniforme });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tabulador", SqlDbType = SqlDbType.Int, Value = idc_tabulador });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_horariog", SqlDbType = SqlDbType.Int, Value = idc_horariog });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_USUARIO", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfil", SqlDbType = SqlDbType.Int, Value = idc_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADENA", SqlDbType = SqlDbType.Int, Value = cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTOTAL_CADENA", SqlDbType = SqlDbType.Int, Value = total_cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdias_reclutar", SqlDbType = SqlDbType.Int, Value = dias_reclu });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apre_puestos", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_combo_puestos_clasificacion()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_puestos_clasificacion", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_combo_puestos(string filtro)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfiltro", SqlDbType = SqlDbType.Int, Value = filtro});
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_puestos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_combo_tipos_uniformes()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_tipos_uniformes", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_sucursales_combo()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_sucursales_combo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SP_COMBO_DEPTOS()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_COMBO_DEPTOS", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

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

        public DataSet CargarReemplazo(PuestosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_Puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_reemplazo_existente", listparameters, false);
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

        public DataSet CancelarSolicitud(PuestosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_Puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_pre_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmotivocancel", SqlDbType = SqlDbType.Int, Value = entidad.Pobservaciones });
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
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena", SqlDbType = SqlDbType.VarChar, Value = entidad.Pcadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotalcadena", SqlDbType = SqlDbType.VarChar, Value = entidad.Ptotalcadena });
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