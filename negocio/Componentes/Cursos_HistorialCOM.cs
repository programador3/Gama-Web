using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Cursos_HistorialCOM
    {
        /// <summary>
        /// retorna los pre empleados que ocupan curso y no se a dado una evaluación.
        /// </summary>
        /// <returns></returns>
        public DataSet cursos_mandar_capacitar(int idc_puesto)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = idc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_mandar_capacitar", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// consulta que candidatos debe revisar el jefe
        /// </summary>
        /// <param name="pidc_usuario"></param>
        /// <returns></returns>
        public DataSet cursos_revisar_gerencia(int pidc_usuario, bool pempleado)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pempleado", SqlDbType = SqlDbType.Int, Value = pempleado });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_revisar_gerencia", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet cursos_historial(bool pempleado)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pempleado", SqlDbType = SqlDbType.Bit, Value = pempleado });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_historial", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// muestra el detalle de un registro de cursos historial por ejemplo en la edicion
        /// </summary>
        /// <param name="pempleado"></param>
        /// <returns></returns>
        public DataSet cursos_historial_detalle(Cursos_HistorialENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pempleado", SqlDbType = SqlDbType.Bit, Value = entidad.Empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso_historial", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso_historial });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_historial_detalle", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //add
        public DataSet cursosHistorialCaptura(Cursos_HistorialENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //si no trae datos la entidad no debo mandar el parametro porque no manda null y en unos casos ocupo que se quede en null
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso_historial", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso_historial });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso });
            if (entidad.Idc_pre_empleado > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_pre_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Idc_pre_empleado });
            }
            //if (entidad.Idc_empleado > 0) {
            //    listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Idc_empleado });
            //}

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pestatus", SqlDbType = SqlDbType.Char, Value = entidad.Estatus });
            if (!String.IsNullOrEmpty(entidad.Aprobado))
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@paprobado", SqlDbType = SqlDbType.Bit, Value = Convert.ToBoolean(entidad.Aprobado) });
            }
            if (!String.IsNullOrEmpty(entidad.Aprobado_jefe))
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@paprobado_jefe", SqlDbType = SqlDbType.Bit, Value = Convert.ToBoolean(entidad.Aprobado_jefe) });
            }

            listparameters.Add(new SqlParameter() { ParameterName = "@presultado", SqlDbType = SqlDbType.Int, Value = entidad.Resultado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.VarChar, Value = entidad.Observaciones });
            //CADENAS
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_curso_exam_archivo", SqlDbType = SqlDbType.VarChar, Value = entidad.Cad_curso_exam_archivo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_curso_exam_archivo_tot", SqlDbType = SqlDbType.Int, Value = entidad.Cad_curso_exam_archivo_tot });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_historial_captura", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet cursos_programar_capturar(Cursos_HistorialENT entidad, string correorh, string correocandidato, string obsrvaciones, string guid)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso_historial", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso_historial });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_pre_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Idc_pre_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Idc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pestatus", SqlDbType = SqlDbType.Char, Value = entidad.Estatus });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_tentativa", SqlDbType = SqlDbType.DateTime, Value = entidad.Fecha_tentativa });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcorreorh", SqlDbType = SqlDbType.VarChar, Value = correorh });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcorreocandidato", SqlDbType = SqlDbType.VarChar, Value = correocandidato });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.VarChar, Value = obsrvaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pguid", SqlDbType = SqlDbType.VarChar, Value = guid });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_programar_capturar", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet cursos_aprobar_revision_gerencia(Cursos_HistorialENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //@pidc_usuario int,
            //@pobservaciones varchar(250)
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_pre_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Idc_pre_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@paprobado_jefe", SqlDbType = SqlDbType.Bit, Value = Convert.ToBoolean(entidad.Aprobado_jefe) });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.VarChar, Value = entidad.Observaciones });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_aprobar_revision_gerencia", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// se hizo un segundo sp para no interferir con el proceso de pre empleados porque el otro sp de pre empleados lleva validaciones especificas
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DataSet cursos_aprobar_revision_gerencia_empleado(Cursos_HistorialENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //@pidc_usuario int,
            //@pobservaciones varchar(250)
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Idc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@paprobado_jefe", SqlDbType = SqlDbType.Bit, Value = Convert.ToBoolean(entidad.Aprobado_jefe) });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.VarChar, Value = entidad.Observaciones });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_aprobar_revision_gerencia_empleado", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// trae el listado de empleados
        /// </summary>
        /// <param name="pempleado"></param>
        /// <returns></returns>
        public DataSet cursos_empleados_cbox()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_empleados", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet cursos_puestos_cbox()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

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

        /// <summary>
        /// en base al idc curso regresa los examenes ligados a ese curso
        /// </summary>
        /// <returns></returns>
        public DataSet cursos_examenes(int pidc_curso)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            try
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso", SqlDbType = SqlDbType.Int, Value = pidc_curso });
                ds = data.enviar("sp_cursos_examenes", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// regresa la info de un pre empleado, telefonos cursos que debe llevar, correo, puesto, etc
        /// </summary>
        /// <returns></returns>
        public DataSet cursos_mandar_capacitar_detalle(int pidc_pre_empleado)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            try
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_pre_empleado", SqlDbType = SqlDbType.Int, Value = pidc_pre_empleado });
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_mandar_capacitar_detalle", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet cursos_cancelar_capacitacion(Cursos_HistorialENT entidad, int pidc_pre_empleado)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_pre_empleado", SqlDbType = SqlDbType.Int, Value = pidc_pre_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.VarChar, Value = entidad.Observaciones });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_cancelar_capacitacion", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// trae el detalle del pre empleado que cursos presento etc
        /// </summary>
        /// <param name="pidc_pre_empleado"></param>
        /// <returns></returns>
        public DataSet cursos_revisar_gerencia_detalle(Cursos_HistorialENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Idc_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_pre_empleado", SqlDbType = SqlDbType.Int, Value = entidad.Idc_pre_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pempleado", SqlDbType = SqlDbType.Bit, Value = entidad.Empleado });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_revisar_gerencia_detalle", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}