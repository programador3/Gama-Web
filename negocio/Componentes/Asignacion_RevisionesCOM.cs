using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Asignacion_RevisionesCOM
    {
        /// <summary>
        /// Rgresa tabla de puesto
        /// </summary>
        /// <param name="Etiqueta"></param>
        /// <returns></returns>
        public DataSet CargaComboDinamicoOrgn(Asignacion_RevisionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfiltro", SqlDbType = SqlDbType.Int, Value = Etiqueta.Filtro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptipo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_puestos_abajo_organigrama", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaComboDeptos(Asignacion_RevisionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Int, Value = "T" });
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

        public DataSet CargaComboDinamicoServicios(Asignacion_RevisionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfiltro", SqlDbType = SqlDbType.Int, Value = Etiqueta.Filtro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto_revisa });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Int, Value = "T" });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_filtro_puestos_servicios", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Rgresa tabla de revisiones
        /// </summary>
        /// <param name="Etiqueta"></param>
        /// <returns></returns>
        public DataSet CargaAsignacion(Asignacion_RevisionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_select_asignacion_revisiones", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaTipoAplicacion(Asignacion_RevisionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pservicio", SqlDbType = SqlDbType.Char, Value = Etiqueta.Servici });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_select_tipo_aplicaciones", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Carga las sucursales
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaSucursales(Asignacion_RevisionesENT Entidad)
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

        /// <summary>
        /// Rgresa tabla de revisiones
        /// </summary>
        /// <param name="Etiqueta"></param>
        /// <returns></returns>
        public DataSet CargaTipoRevisiones(Asignacion_RevisionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pservicio", SqlDbType = SqlDbType.Bit, Value = Etiqueta.ServicioBool });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_select_tipo_revisiones", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Rgresa tabla de revisiones
        /// </summary>
        /// <param name="Etiqueta"></param>
        /// <returns></returns>
        public DataSet CargaDatosEducionRevision(Asignacion_RevisionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_idc_clasifrev ", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_clasifrev });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_puestosrevisiones_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Rgresa tabla de puesto
        /// </summary>
        /// <param name="Etiqueta"></param>
        /// <returns></returns>
        public DataSet CargaComboDinamico(Asignacion_RevisionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfiltro", SqlDbType = SqlDbType.Int, Value = Etiqueta.Filtro });
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

        public DataSet EliminarAsignacion(Asignacion_RevisionesENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_clasifrev", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_clasifrev });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_brevisiones", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AgregarAsignacion(Asignacion_RevisionesENT Etiqueta)
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
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_prepara", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_entrega", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto_entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            if (Etiqueta.Idc_sucursal > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_sucursal });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_arevisiones", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet EditarAsignacion(Asignacion_RevisionesENT Etiqueta)
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
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_prepara", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_entrega", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto_entrega });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_idc_clasifrev", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_clasifrev });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            if (Etiqueta.Idc_sucursal > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_sucursal });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_mrevisiones", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}