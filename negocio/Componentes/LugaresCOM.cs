using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class LugaresCOM
    {
        /// <summary>
        /// Regresa un seleccion de areas, si se asigna el idc de area, regresa los lugares del area
        /// </summary>
        /// <param name="Etiqueta"></param>
        /// <returns></returns>
        public DataSet CargaAreas(LugaresENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            if (Etiqueta.Pidc_puesto > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto });
            }
            if (Etiqueta.Pidc_area > 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_area", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_area });
            }
            try
            {
                ds = data.enviar("sp_areas_sucursales", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaSucursales(LugaresENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                ds = data.enviar("sp_sucursales_combo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}