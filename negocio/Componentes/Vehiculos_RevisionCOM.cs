using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Vehiculos_RevisionCOM
    {
        /// <summary>
        /// Carga Vehiculos por revision
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaVehiculosRevision(Vehiculos_RevisionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestorevi", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puestorevi });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoprebaja", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puestoprebaja });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_revisiones_vehiculo_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Inserta un Revision de Pre Baja
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet InsertarRevisionVehiculos(Vehiculos_RevisionENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prebaja", SqlDbType = SqlDbType.Int, Value = Entidad.PIDC_prebaja });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCADHERR", SqlDbType = SqlDbType.VarChar, Value = Entidad.PCADHERR });
            listparameters.Add(new SqlParameter() { ParameterName = "@PNUMHERR", SqlDbType = SqlDbType.Int, Value = Entidad.PNUMHERR });
            listparameters.Add(new SqlParameter() { ParameterName = "@PMONTO", SqlDbType = SqlDbType.Money, Value = Entidad.PMONTO });
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_vehiculo", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_vehiculo });
            listparameters.Add(new SqlParameter() { ParameterName = "@buenas_condiciones", SqlDbType = SqlDbType.Bit, Value = Entidad.Buenas_condiciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pintura", SqlDbType = SqlDbType.Bit, Value = Entidad.Pintura });
            listparameters.Add(new SqlParameter() { ParameterName = "@pintura_costo", SqlDbType = SqlDbType.Money, Value = Entidad.Pintura_costo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pintura_obs", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pintura_obs });
            listparameters.Add(new SqlParameter() { ParameterName = "@llantas", SqlDbType = SqlDbType.Bit, Value = Entidad.Llantas });
            listparameters.Add(new SqlParameter() { ParameterName = "@llantas_costo", SqlDbType = SqlDbType.Money, Value = Entidad.Llantas_costo });
            listparameters.Add(new SqlParameter() { ParameterName = "@llantas_obs", SqlDbType = SqlDbType.VarChar, Value = Entidad.Llantas_obs });
            listparameters.Add(new SqlParameter() { ParameterName = "@accesorios", SqlDbType = SqlDbType.Bit, Value = Entidad.Accesorios });
            listparameters.Add(new SqlParameter() { ParameterName = "@accesorios_costo", SqlDbType = SqlDbType.Money, Value = Entidad.Accesorios_costo });
            listparameters.Add(new SqlParameter() { ParameterName = "@accesorios_obs", SqlDbType = SqlDbType.VarChar, Value = Entidad.Accesorios_obs });
            listparameters.Add(new SqlParameter() { ParameterName = "@carroceria", SqlDbType = SqlDbType.Bit, Value = Entidad.Carroceria });
            listparameters.Add(new SqlParameter() { ParameterName = "@carroceria_costo", SqlDbType = SqlDbType.Money, Value = Entidad.Carroceria_costo });
            listparameters.Add(new SqlParameter() { ParameterName = "@carroceria_obs", SqlDbType = SqlDbType.VarChar, Value = Entidad.Carroceria_obs });
            listparameters.Add(new SqlParameter() { ParameterName = "@interior", SqlDbType = SqlDbType.Bit, Value = Entidad.Interior });
            listparameters.Add(new SqlParameter() { ParameterName = "@interior_costo", SqlDbType = SqlDbType.Money, Value = Entidad.Interior_costo });
            listparameters.Add(new SqlParameter() { ParameterName = "@interior_obs", SqlDbType = SqlDbType.VarChar, Value = Entidad.Interior_obs });
            listparameters.Add(new SqlParameter() { ParameterName = "@vidrios", SqlDbType = SqlDbType.Bit, Value = Entidad.Vidrios });
            listparameters.Add(new SqlParameter() { ParameterName = "@vidrios_costo", SqlDbType = SqlDbType.Money, Value = Entidad.Vidrios_costo });
            listparameters.Add(new SqlParameter() { ParameterName = "@vidrios_obs", SqlDbType = SqlDbType.VarChar, Value = Entidad.Vidrios_obs });
            listparameters.Add(new SqlParameter() { ParameterName = "@focos", SqlDbType = SqlDbType.Bit, Value = Entidad.Focos });
            listparameters.Add(new SqlParameter() { ParameterName = "@focos_costo", SqlDbType = SqlDbType.Money, Value = Entidad.Focos_costo });
            listparameters.Add(new SqlParameter() { ParameterName = "@focos_obs", SqlDbType = SqlDbType.VarChar, Value = Entidad.Focos_obs });
            listparameters.Add(new SqlParameter() { ParameterName = "@POBSERVACIONES", SqlDbType = SqlDbType.VarChar, Value = Entidad.POBSERVACIONES });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apre_bajas_rev_vehiculos_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}