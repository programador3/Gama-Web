using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class PreparacionesCOM
    {
        /// <summary>
        /// Carga Herramientas por preparar
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaHerramientasRevision(PreparacionesENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoprep", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puestoprep });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("pendientes_preparar_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}