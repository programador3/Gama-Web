using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class CursosCOM
    {
        public DataSet cursos(CursosE Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_curso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso_borr", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_curso_borr });
            listparameters.Add(new SqlParameter() { ParameterName = "@pborrador", SqlDbType = SqlDbType.Bit, Value = Entidad.Borrador });
            try
            {
                ds = data.enviar("sp_cursos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //add
        public DataSet cursosAgregar(CursosE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = entidad.Descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_curso_per", SqlDbType = SqlDbType.VarChar, Value = entidad.Cad_curso_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_curso_per_tot", SqlDbType = SqlDbType.Int, Value = entidad.Cad_curso_perfil_tot });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_curso_arch", SqlDbType = SqlDbType.VarChar, Value = entidad.Cad_curso_archivo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_curso_arch_tot", SqlDbType = SqlDbType.Int, Value = entidad.Cad_curso_archivo_tot });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo_curso", SqlDbType = SqlDbType.Char, Value = entidad.Tipo_curso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_examenes", SqlDbType = SqlDbType.VarChar, Value = entidad.Pcadena_examenes });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_examenes", SqlDbType = SqlDbType.Int, Value = entidad.Ptotal_examenes });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_acursos", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //add borrador
        public DataSet cursosAgregarBorrador(CursosE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso_borr", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso_borr });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = entidad.Descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.VarChar, Value = entidad.Observaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@paprobado", SqlDbType = SqlDbType.Bit, Value = entidad.Aprobado });
            listparameters.Add(new SqlParameter() { ParameterName = "@ppendiente", SqlDbType = SqlDbType.Bit, Value = entidad.Pendiente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_curso_per", SqlDbType = SqlDbType.VarChar, Value = entidad.Cad_curso_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_curso_per_tot", SqlDbType = SqlDbType.Int, Value = entidad.Cad_curso_perfil_tot });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_curso_arch", SqlDbType = SqlDbType.VarChar, Value = entidad.Cad_curso_archivo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_curso_arch_tot", SqlDbType = SqlDbType.Int, Value = entidad.Cad_curso_archivo_tot });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo_curso", SqlDbType = SqlDbType.Char, Value = entidad.Tipo_curso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_examenes", SqlDbType = SqlDbType.VarChar, Value = entidad.Pcadena_examenes });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_examenes", SqlDbType = SqlDbType.Int, Value = entidad.Ptotal_examenes });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_acursos_borr", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet DeleteProduccion(CursosE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bcursos", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet DeleteBorrador(CursosE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso_borr", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bcursos_borr", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CancelarSolicitud(CursosE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso_borr", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_mcursos_borr_sol_reg", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet EnviarSolicitud(CursosE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso_borr", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_mcursos_borr_sol", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //delete
        public DataSet cursosEliminar(CursosE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso_borr", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso_borr });
            listparameters.Add(new SqlParameter() { ParameterName = "@pborrador", SqlDbType = SqlDbType.Bit, Value = entidad.Borrador });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_eliminar", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //update
        public DataSet cursosPendiente(CursosE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso_borr", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso_borr });
            listparameters.Add(new SqlParameter() { ParameterName = "@ppendiente", SqlDbType = SqlDbType.Bit, Value = entidad.Pendiente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_pendiente", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //vaciar
        public DataSet cursosVaciar(CursosE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_curso", SqlDbType = SqlDbType.Int, Value = entidad.Idc_curso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_produccion_a_borrador", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet cursos_perfiles_cbox()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            // listparameters.Add(new SqlParameter() { ParameterName = "@pborrador", SqlDbType = SqlDbType.Bit, Value = Entidad.Borrador });

            try
            {
                ds = data.enviar("sp_cursos_perfiles_cbox", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //cursos por aprobar
        public DataSet cursos_pendientes_por_aprobar(CursosE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfil", SqlDbType = SqlDbType.VarChar, Value = entidad.Idc_perfil });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_por_aprobar", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// para llenar cualquier cbox de cursos
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public DataSet cursos_cbox()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfil", SqlDbType = SqlDbType.VarChar, Value = entidad.Idc_perfil });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cursos_cbox", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}