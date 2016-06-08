using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class PuestosServiciosCOM
    {
        /// <summary>
        /// Carga listado de pendientes
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaPuestos(PuestosServiciosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_Puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Int, Value = "P" });
           listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_reves", SqlDbType = SqlDbType.Int, Value = Entidad.PReves });
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

        public DataSet AgregarPuesto(PuestosServiciosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_Puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pcadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotalcadena", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptodos", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptodos });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_reves", SqlDbType = SqlDbType.Int, Value = Etiqueta.PReves });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apuestos_servicios", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}