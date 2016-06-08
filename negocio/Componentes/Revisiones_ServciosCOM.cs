using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Revisiones_ServciosCOM
    {
        /// <summary>
        /// Rgresa tabla de revisiones servicios
        /// </summary>
        /// <param name="Etiqueta"></param>
        /// <returns></returns>
        public DataSet CargaServicios(Revisiones_ServiciosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_revisiones_servicios", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaServiciosEditar(Revisiones_ServiciosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_revisionser", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_revisionser });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_select_revisiones_servicios_edit", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Elimina revisiones servicios
        /// </summary>
        /// <param name="Etiqueta"></param>
        /// <returns></returns>
        public DataSet EliminaRevisiones(Revisiones_ServiciosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_revisionser", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_revisionser });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_brevisiones_servicios", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AgregarAsignacion(Revisiones_ServiciosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@tipo_rev", SqlDbType = SqlDbType.Char, Value = Etiqueta.Tipo_rev });
            listparameters.Add(new SqlParameter() { ParameterName = "@cadpuestos", SqlDbType = SqlDbType.Int, Value = Etiqueta.Cad_puestos });
            listparameters.Add(new SqlParameter() { ParameterName = "@num_cadpuestos", SqlDbType = SqlDbType.Int, Value = Etiqueta.Numcadpuesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@cadgrupos", SqlDbType = SqlDbType.Int, Value = Etiqueta.Cad_gpos });
            listparameters.Add(new SqlParameter() { ParameterName = "@num_cadgrupos", SqlDbType = SqlDbType.Int, Value = Etiqueta.Numcadgrupos });
            listparameters.Add(new SqlParameter() { ParameterName = "@tipo_aplica", SqlDbType = SqlDbType.Int, Value = Etiqueta.Tipo_aplica });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_revisa", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto_revisa });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfinal", SqlDbType = SqlDbType.Bit, Value = Etiqueta.Final });
            listparameters.Add(new SqlParameter() { ParameterName = "@pgenera_vales", SqlDbType = SqlDbType.Bit, Value = Etiqueta.Genera_Vales });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_arevisiones_servicios", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet EditarAsignacion(Revisiones_ServiciosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@tipo_rev", SqlDbType = SqlDbType.Char, Value = Etiqueta.Tipo_rev });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_revisionser", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_revisionser });
            listparameters.Add(new SqlParameter() { ParameterName = "@cadpuestos", SqlDbType = SqlDbType.Int, Value = Etiqueta.Cad_puestos });
            listparameters.Add(new SqlParameter() { ParameterName = "@num_cadpuestos", SqlDbType = SqlDbType.Int, Value = Etiqueta.Numcadpuesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@cadgrupos", SqlDbType = SqlDbType.Int, Value = Etiqueta.Cad_gpos });
            listparameters.Add(new SqlParameter() { ParameterName = "@num_cadgrupos", SqlDbType = SqlDbType.Int, Value = Etiqueta.Numcadgrupos });
            listparameters.Add(new SqlParameter() { ParameterName = "@tipo_aplica", SqlDbType = SqlDbType.Int, Value = Etiqueta.Tipo_aplica });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_revisa", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto_revisa });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Descripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfinal", SqlDbType = SqlDbType.Bit, Value = Etiqueta.Final });
            listparameters.Add(new SqlParameter() { ParameterName = "@vales", SqlDbType = SqlDbType.Bit, Value = Etiqueta.Genera_Vales });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_mrevisiones_servicios", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}