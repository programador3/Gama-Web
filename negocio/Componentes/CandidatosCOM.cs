using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class CandidatosCOM
    {
        /// <summary>
        /// Carga puestos por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaPuestos(CandidatosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_reclutador", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puestobaja });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_prepara });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_puestos_candidatos_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Carga puestos por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaCandidatos(CandidatosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_pre_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_pre_empleado });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_puestos_candidatos_detalles_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Verifica que todo este preparado, para la entrada de un candidato
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet TodoPreparado(CandidatosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_prepara });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_capacitacion_candidatos_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CambiarFechaCompromiso(CandidatosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = Entidad.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmotivo", SqlDbType = SqlDbType.Int, Value = Entidad.Pmotivo });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_efecha_compromiso_reclutamiento", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CambiarFechaCompromisocADENA(CandidatosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADENA", SqlDbType = SqlDbType.Int, Value = Entidad.Cadarch });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTOTAL_CADENA", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadsarch });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_efecha_compromiso_reclutamiento", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Inserta una Preparacion
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet InsertarPrep(CandidatosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestobaja", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puestobaja });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombres", SqlDbType = SqlDbType.VarChar, Value = Entidad.Nombre });
            listparameters.Add(new SqlParameter() { ParameterName = "@paterno", SqlDbType = SqlDbType.VarChar, Value = Entidad.Paterno });
            listparameters.Add(new SqlParameter() { ParameterName = "@materno", SqlDbType = SqlDbType.VarChar, Value = Entidad.Materno });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apreparar_candidatos_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Inserta una Preparacion
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet InsertarArchivosCandidato(CandidatosENT Entidad)
        {
            DataSet ds = new DataSet();
            Datos data = new Datos();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_candidato", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADARCH", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadarch });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMARCH", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadsarch });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apreparar_candidatos_archivos_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Borra una Preparacion
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet BorrarCandidato(CandidatosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_pre_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_candidato });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_epre_empleados", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Pendientes por seleccionar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet DatosSeleccion(CandidatosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            try
            {
                ds = data.enviar("sp_puestos_candidatos_pendientes_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}