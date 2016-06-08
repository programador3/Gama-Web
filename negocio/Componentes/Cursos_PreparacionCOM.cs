using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Cursos_PreparacionCOM
    {
        /// <summary>
        /// Carga Cursos por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaCursosPrep(Cursos_PreparacionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoprep", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puestoprep });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_preparacion_cursos_web", listparameters, false);
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
        public DataSet InsertarPrep(Cursos_PreparacionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADCUR", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadser });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMCUR", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadser });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_revisionser", SqlDbType = SqlDbType.Int, Value = Entidad.Totalcadser });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apreparar_cursos_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}