using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Puesto_EquiCOM
    {
        DataSet ds = new DataSet();
        Datos data = new Datos();
        List<SqlParameter> listparameters = new List<SqlParameter>();


        public DataSet CargarPuestos(Puesto_EquiENT Entidad)
        {
            ds = new DataSet();
            listparameters = new List<SqlParameter>();
            data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PUESTOEQUI", Value = Entidad.Pidc_puesto_equi });
            try
            {
                ds = data.enviar("sp_Puestos_Equi", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AltaPuestos_Equi(Puesto_EquiENT Entidad)
        {
            ds = new DataSet();
            data = new Datos();
            listparameters = new List<SqlParameter>();

            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PUESTOEQUI",  Value = Entidad.Pidc_puesto_equi });
            listparameters.Add(new SqlParameter() { ParameterName = "@PDESCRIPCION",  Value = Entidad.Pdescripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@PACTIVO",  Value = Entidad.Pactivo });
            //listparameters.Add(new SqlParameter() { ParameterName = "@PMOV",  Value = Entidad.Pmov });

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", Value = Entidad.Pusuariopc });

            try
            {
                ds = data.enviar("sp_aPuestos_Equi", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet ModificarDatos(Puesto_EquiENT Entidad)
        {
            ds = new DataSet();
            data = new Datos();
            listparameters = new List<SqlParameter>();

            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PUESTOEQUI", Value = Entidad.Pidc_puesto_equi });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion",  Value = Entidad.Pdescripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pactivo", Value = Entidad.Pactivo });
            //listparameters.Add(new SqlParameter() { ParameterName = "@pmov", Value = Entidad.Pmov });
            
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", Value = Entidad.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", Value = Entidad.Pusuariopc });
            try
            {
                ds = data.enviar("sp_aPuestos_Equi", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
