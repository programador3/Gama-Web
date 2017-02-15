using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class Pre_EmpleadosCOM
    {
       

        /// <summary>
        /// Carga La informacion
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaInformacion(Pre_EmpleadosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_candidato });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_captura_preempleado_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet CargaInformacion_prepara_sinpuesto(Pre_EmpleadosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_candidato });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_captura_preempleado_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_informacion_solicitud_preempleados(int guid)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PRE_EMPLEADO", SqlDbType = SqlDbType.Char, Value = guid });
            try
            {
                ds = data.enviar("sp_informacion_solicitud_preempleados", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaInformacionBasica(Pre_EmpleadosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_candidato });
            listparameters.Add(new SqlParameter() { ParameterName = "@pbasica", SqlDbType = SqlDbType.Int, Value = true });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_captura_preempleado_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        /// <summary>
        /// Carga La informacion de Colonias
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CargaInformacionColonia(Pre_EmpleadosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pvalor", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pvalor });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bcolonias", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet EditarPreEmpleas(Pre_EmpleadosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@nombres", SqlDbType = SqlDbType.Int, Value = Entidad.Nombre });
            listparameters.Add(new SqlParameter() { ParameterName = "@paterno", SqlDbType = SqlDbType.Int, Value = Entidad.Paterno });
            listparameters.Add(new SqlParameter() { ParameterName = "@materno", SqlDbType = SqlDbType.Int, Value = Entidad.Materno });
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_pre_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_pre_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@fec_nac ", SqlDbType = SqlDbType.DateTime, Value = Entidad.Fec_nac });
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puesto });
            if (Entidad.Idc_edocivil > 0) { listparameters.Add(new SqlParameter() { ParameterName = "@idc_edocivil", SqlDbType = SqlDbType.TinyInt, Value = Entidad.Idc_edocivil }); }
            if (Entidad.Idc_estado > 0) { listparameters.Add(new SqlParameter() { ParameterName = "@idc_estado", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_estado }); }
            if (Entidad.Idc_colonia > 0) { listparameters.Add(new SqlParameter() { ParameterName = "@idc_colonia", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_colonia }); }


            listparameters.Add(new SqlParameter() { ParameterName = "@sexo", SqlDbType = SqlDbType.Char, Value = Entidad.Sexo });
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_sucursal", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@titulo", SqlDbType = SqlDbType.Char, Value = Entidad.Titulo });
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_nzona", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_nzona });

            listparameters.Add(new SqlParameter() { ParameterName = "@direccion", SqlDbType = SqlDbType.VarChar, Value = Entidad.Direccion });
            listparameters.Add(new SqlParameter() { ParameterName = "@num_imss", SqlDbType = SqlDbType.VarChar, Value = Entidad.Num_imss });
            listparameters.Add(new SqlParameter() { ParameterName = "@rfc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Rfc });
            listparameters.Add(new SqlParameter() { ParameterName = "@curp", SqlDbType = SqlDbType.VarChar, Value = Entidad.Curp });
            listparameters.Add(new SqlParameter() { ParameterName = "@nombre_padre", SqlDbType = SqlDbType.VarChar, Value = Entidad.Nombre_padre });
            listparameters.Add(new SqlParameter() { ParameterName = "@nombre_madre", SqlDbType = SqlDbType.VarChar, Value = Entidad.Nombre_madre });
            listparameters.Add(new SqlParameter() { ParameterName = "@esposo", SqlDbType = SqlDbType.VarChar, Value = Entidad.Esposo });
            listparameters.Add(new SqlParameter() { ParameterName = "@correo_personal", SqlDbType = SqlDbType.VarChar, Value = Entidad.Correo_personal });
            listparameters.Add(new SqlParameter() { ParameterName = "@NUMCADTEL", SqlDbType = SqlDbType.Int, Value = Entidad.Numcadtel });
            listparameters.Add(new SqlParameter() { ParameterName = "@CADTEL", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadtel });
            listparameters.Add(new SqlParameter() { ParameterName = "@NUMCADHIJOS", SqlDbType = SqlDbType.Int, Value = Entidad.Numcadhijos });
            listparameters.Add(new SqlParameter() { ParameterName = "@CADHIJOS", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadhijos });
            listparameters.Add(new SqlParameter() { ParameterName = "@NUMCADHORARIOS", SqlDbType = SqlDbType.Int, Value = 0 });
            listparameters.Add(new SqlParameter() { ParameterName = "@CADHORARIOS", SqlDbType = SqlDbType.VarChar, Value = "" });
            listparameters.Add(new SqlParameter() { ParameterName = "@NUMCADENAELECTORLICENCIA", SqlDbType = SqlDbType.Int, Value = Entidad.Numcadelelic });
            listparameters.Add(new SqlParameter() { ParameterName = "@CADELECTORLICENCIA", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadelelic });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcapacitacion", SqlDbType = SqlDbType.Bit, Value = Entidad.Pcapacitacion });
            listparameters.Add(new SqlParameter() { ParameterName = "@CADENA_PAPELERIA", SqlDbType = SqlDbType.VarChar, Value = Entidad.cadena_papeleria });
            listparameters.Add(new SqlParameter() { ParameterName = "@NUMCADENAPAPELERIA", SqlDbType = SqlDbType.Int, Value = Entidad.tot_cadena_pape });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pobersvaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_horariogpo", SqlDbType = SqlDbType.Int, Value = Entidad.Numcadhorarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadenah", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadhorarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cad_ref", SqlDbType = SqlDbType.Int, Value = Entidad.tot_cadena_REF });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_ref", SqlDbType = SqlDbType.VarChar, Value = Entidad.cadena_ref });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_mpre_empleado_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        /// <summary>
        /// Guarda un Pre-Empleado
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet GuardarPreEmpleas(Pre_EmpleadosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@nombres", SqlDbType = SqlDbType.Int, Value = Entidad.Nombre });
            listparameters.Add(new SqlParameter() { ParameterName = "@paterno", SqlDbType = SqlDbType.Int, Value = Entidad.Paterno });
            listparameters.Add(new SqlParameter() { ParameterName = "@materno", SqlDbType = SqlDbType.Int, Value = Entidad.Materno });
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@fec_nac ", SqlDbType = SqlDbType.DateTime, Value = Entidad.Fec_nac });
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puesto });
            if (Entidad.Idc_edocivil > 0) { listparameters.Add(new SqlParameter() { ParameterName = "@idc_edocivil", SqlDbType = SqlDbType.TinyInt, Value = Entidad.Idc_edocivil }); }
            if (Entidad.Idc_estado > 0) { listparameters.Add(new SqlParameter() { ParameterName = "@idc_estado", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_estado }); }
            if (Entidad.Idc_colonia > 0) { listparameters.Add(new SqlParameter() { ParameterName = "@idc_colonia", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_colonia }); }

            

            listparameters.Add(new SqlParameter() { ParameterName = "@sexo", SqlDbType = SqlDbType.Char, Value = Entidad.Sexo });
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_sucursal", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@titulo", SqlDbType = SqlDbType.Char, Value = Entidad.Titulo });
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_nzona", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_nzona });
            
            listparameters.Add(new SqlParameter() { ParameterName = "@direccion", SqlDbType = SqlDbType.VarChar, Value = Entidad.Direccion });
            listparameters.Add(new SqlParameter() { ParameterName = "@num_imss", SqlDbType = SqlDbType.VarChar, Value = Entidad.Num_imss });
            listparameters.Add(new SqlParameter() { ParameterName = "@rfc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Rfc });
            listparameters.Add(new SqlParameter() { ParameterName = "@curp", SqlDbType = SqlDbType.VarChar, Value = Entidad.Curp });
            listparameters.Add(new SqlParameter() { ParameterName = "@sueldo ", SqlDbType = SqlDbType.Money, Value = Entidad.Sueldo });
            listparameters.Add(new SqlParameter() { ParameterName = "@complementos", SqlDbType = SqlDbType.Money, Value = Entidad.Complementos });
            listparameters.Add(new SqlParameter() { ParameterName = "@premio_transporte", SqlDbType = SqlDbType.Bit, Value = Entidad.Premio_transporte });
            listparameters.Add(new SqlParameter() { ParameterName = "@nombre_padre", SqlDbType = SqlDbType.VarChar, Value = Entidad.Nombre_padre });
            listparameters.Add(new SqlParameter() { ParameterName = "@nombre_madre", SqlDbType = SqlDbType.VarChar, Value = Entidad.Nombre_madre });
            listparameters.Add(new SqlParameter() { ParameterName = "@esposo", SqlDbType = SqlDbType.VarChar, Value = Entidad.Esposo });
            listparameters.Add(new SqlParameter() { ParameterName = "@correo_personal", SqlDbType = SqlDbType.VarChar, Value = Entidad.Correo_personal });
            listparameters.Add(new SqlParameter() { ParameterName = "@NUMCADTEL", SqlDbType = SqlDbType.Int, Value = Entidad.Numcadtel });
            listparameters.Add(new SqlParameter() { ParameterName = "@CADTEL", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadtel });
            listparameters.Add(new SqlParameter() { ParameterName = "@NUMCADHIJOS", SqlDbType = SqlDbType.Int, Value = Entidad.Numcadhijos });
            listparameters.Add(new SqlParameter() { ParameterName = "@CADHIJOS", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadhijos });
            listparameters.Add(new SqlParameter() { ParameterName = "@NUMCADHORARIOS", SqlDbType = SqlDbType.Int, Value = 0 });
            listparameters.Add(new SqlParameter() { ParameterName = "@CADHORARIOS", SqlDbType = SqlDbType.VarChar, Value = "" });
            listparameters.Add(new SqlParameter() { ParameterName = "@NUMCADENAELECTORLICENCIA", SqlDbType = SqlDbType.Int, Value = Entidad.Numcadelelic });
            listparameters.Add(new SqlParameter() { ParameterName = "@CADELECTORLICENCIA", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadelelic });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcapacitacion", SqlDbType = SqlDbType.Bit, Value = Entidad.Pcapacitacion });
            listparameters.Add(new SqlParameter() { ParameterName = "@CADENA_PAPELERIA", SqlDbType = SqlDbType.VarChar, Value = Entidad.cadena_papeleria });
            listparameters.Add(new SqlParameter() { ParameterName = "@NUMCADENAPAPELERIA", SqlDbType = SqlDbType.Int, Value = Entidad.tot_cadena_pape });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pobersvaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadenah", SqlDbType = SqlDbType.VarChar, Value = Entidad.Cadhorarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cad_ref", SqlDbType = SqlDbType.Int, Value = Entidad.tot_cadena_REF });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_ref", SqlDbType = SqlDbType.VarChar, Value = Entidad.cadena_ref });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apre_empleado_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Guarda Papeleria de Pre-Empleado
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet GuardarPapeleria(Pre_EmpleadosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_pre_empleado", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_pre_empleado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tipodoc", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_tipodoc });
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_tipodocarc", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_tipodocarc });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apre_empleado_documentos_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Carga La informacion
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet GuardaSeleccion(Pre_EmpleadosENT Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prepara", SqlDbType = SqlDbType.Int, Value = Entidad.Pidc_prepara });
            listparameters.Add(new SqlParameter() { ParameterName = "@CADSELCAND", SqlDbType = SqlDbType.Int, Value = Entidad.Cadsel });
            listparameters.Add(new SqlParameter() { ParameterName = "@NUMCAD", SqlDbType = SqlDbType.Int, Value = Entidad.Numcad });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aseleccion_preempleado_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}