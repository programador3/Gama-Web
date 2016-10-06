using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
   public class Area_TrabajoCOM
    {
        DataSet ds = new DataSet();
        Datos data = new Datos();
        List<SqlParameter> listparameters = new List<SqlParameter>();

        public DataSet AltaRegistro(Area_TrabajoENT Entidad)
        {
            ds = new DataSet();
            data = new Datos();
            listparameters = new List<SqlParameter>();
            //idc_area,idc_sucursal,nombre,activo
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_SUCURSAL", Value = Entidad.Pidc_Sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@PDESCRIPCION", Value = Entidad.Pdescripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@PACTIVO", Value = Entidad.Pactivo });
            //listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_AREA", Value = Entidad.Pidc_Area });

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", Value = Entidad.Pusuariopc });

            try
            {
                ds = data.enviar("sp_aArea_Trabajo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet EditarRegistro(Area_TrabajoENT Entidad)
        {
            ds = new DataSet();
            data = new Datos();
            listparameters = new List<SqlParameter>();

            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_SUCURSAL", Value = Entidad.Pidc_Sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@PDESCRIPCION", Value = Entidad.Pdescripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@PACTIVO", Value = Entidad.Pactivo });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_AREA", Value = Entidad.Pidc_Area });

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", Value = Entidad.Pusuariopc });

            try
            {
                ds = data.enviar("sp_aArea_Trabajo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        
        public DataSet Cargar_Sucursales_Combo(Area_TrabajoENT Entidad)
        {
            ds = new DataSet();
            listparameters = new List<SqlParameter>();

            data = new Datos();
             try
            {
                ds = data.enviar("SP_SUCURSALES_COMBO_CEDIS", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        
        public DataSet Cargar_Relacion_Socursal_Grid(Area_TrabajoENT Entidad)
        {
            ds = new DataSet();
            listparameters = new List<SqlParameter>();

            data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_sucursal", Value = Entidad.Pidc_Sucursal });
            try
            {
                ds = data.enviar("SP_SUCURSALES_CEDIS_ALMACEN", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Cargar_Combo_Cedis(Area_TrabajoENT Entidad)
        {
            ds = new DataSet();
            listparameters = new List<SqlParameter>();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cedis",Value = Entidad.Pidc_cedis});
            data = new Datos();
            try
            {
                ds = data.enviar("sp_socursal_ced", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Grid_Areas(Area_TrabajoENT Entidad)
        {
            ds = new DataSet();
            listparameters = new List<SqlParameter>();

            data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", Value = Entidad.Pidc_Sucursal });
            try
            {
                ds = data.enviar("sp_area_Trabajo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
