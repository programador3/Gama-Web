using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class CuestionariosCOM
    {
        public DataSet CargaCuestionarios(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cuestionarios", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaCategoriasArticuloss(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_categorias_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaTipoPreguntas(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combotipopreguntas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaTipoCuestionarios(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_empleado", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combotipocuestionario", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AgregarCuestionario(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_preguntas", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_preguntas });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_respuestas", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_respuestas });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_respuestas", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_respuestas });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_preguntas", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_preguntas });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcuestionario", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcuestionario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cuestionario_tipo", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_cuestionario_tipo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_acuestionarios", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet EditarCuestionario(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_preguntas", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_preguntas });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_respuestas", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_respuestas });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_respuestas", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_respuestas });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_preguntas", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_preguntas });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcuestionario", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcuestionario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cuestionario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_cuestionario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cuestionario_tipo", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_cuestionario_tipo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_mcuestionarios", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet EliminarCuestionario(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cuestionario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_cuestionario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ecuestionarios", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarDatosEditar(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cuestionario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_cuestionario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cargadatoscuestionario", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarDatosEncuesta(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            if (Etiqueta.Ppidc_pregunta != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PREGUNTA", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ppidc_pregunta });
            }
            if (Etiqueta.Pacepta_correo == true)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@psegunda_visita", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pacepta_correo });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cargar_encuesta", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarVendedores(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            if (Etiqueta.Ptipo != null)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptipo });
            }
            if (Etiqueta.Ptotal_cadena_categorias != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_especiales", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_categorias });
                listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_especiales", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_categorias });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_vendedores_disponibles", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet IngresarEncuesta(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_respuestas", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_respuestas });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_categorias", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_categorias });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_vendedores", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_vendedores });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_vendedores", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_vendedores });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_categorias", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_categorias });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_respuestas", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_respuestas });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombres });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptelefono", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Ptelefono });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcorreo", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcorreo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cuestionario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_cuestionario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tipocliente", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tipocliente });
            if (Etiqueta.Pidc_clienteh != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_clienteh", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_clienteh });
            }
            if (Etiqueta.Pidc_cliente != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente_elite", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_cliente });
            }
            if (Etiqueta.Pidc_cuestionario_cotizacion != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cuestionario_cotizacion", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_cuestionario_cotizacion });
            }
            if (Etiqueta.Pacepta_correo == true)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@Pgenerar_cotizacion", SqlDbType = SqlDbType.Bit, Value = Etiqueta.Pacepta_correo });
            }

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_acuestionarios_cliente", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet IngresarColaDeTrabajo(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_categorias", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_categorias });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_vendedores", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_vendedores });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_vendedores", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_vendedores });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_categorias", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_categorias });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_clienteh", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_clienteh });
            if (Etiqueta.Primer_ecnuesta == true)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@primer_encuesta", SqlDbType = SqlDbType.Bit, Value = Etiqueta.Primer_ecnuesta });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_acuestionarios_cliente_colatrabajo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet TerminarEncuesta(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_clienteh", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_clienteh });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cancelar_visita_elite", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AgregarCliente(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombres });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aclientes_elite", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CancelarrEncuesta(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombres });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptelefono", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Ptelefono });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcorreo", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcorreo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pacepta", SqlDbType = SqlDbType.Bit, Value = Etiqueta.Pacepta_correo });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aclientes_elite", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CambiarNombreCliente(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombres });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aclientes_elite", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarEncuestaHistorial(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            if (Etiqueta.Pidc_clienteh != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_clienteh", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_clienteh });
            }
            if (Etiqueta.Pidc_cliente != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_cliente });
            }
            if (Etiqueta.Idc_usuario != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            }
            if (!Etiqueta.Pinicio.IsNull)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pinicio });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cargar_encuestacontestada", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaTipoClientes(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_elite_clientes_clasif", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarClientes(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            listparameters.Add(new SqlParameter() { ParameterName = "@pvar", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pcadena_vendedores });
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_elite_cliente_filtrar", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarCuestionariosPendientes(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_clienteh", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_clienteh });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_encuestas_pendientes", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaVisitasReporte(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_inicio", SqlDbType = SqlDbType.DateTime, Value = Etiqueta.Pinicio });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_fin", SqlDbType = SqlDbType.DateTime, Value = Etiqueta.Pfin });

            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_reporte_visitas_elite", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarCotizaciones(CuestionariosENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            listparameters.Add(new SqlParameter() { ParameterName = "@pIDC_COTIZACIONELI", SqlDbType = SqlDbType.DateTime, Value = Etiqueta.Pidc_coti });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });

            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_detalle_cotizacion_elite", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}