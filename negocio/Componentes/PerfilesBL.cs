using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class PerfilesBL
    {
        public DataSet perfiles(PerfilesE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Usuario });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_puestos_perfil_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet perfiles_pendientes(PerfilesE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfil", SqlDbType = SqlDbType.VarChar, Value = entidad.Idc_perfil });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_puestos_perfil_pendientes", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet perfiles_pendientes_acciones(PerfilesE entidad, string pcomando)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil_borr", SqlDbType = SqlDbType.VarChar, Value = entidad.Idc_puestoperfil_borr });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcomando", SqlDbType = SqlDbType.VarChar, Value = pcomando });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Usuario });
            try
            {
                ds = data.enviar("sp_puestos_perfil_pendientes_acciones", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet perfiles_datos(PerfilesE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil", SqlDbType = SqlDbType.VarChar, Value = entidad.Idc_perfil });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_perfiles_datos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet perfiles_datos_borr(PerfilesE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil_borr", SqlDbType = SqlDbType.VarChar, Value = entidad.Idc_perfil });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_perfiles_datos_borrador", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //insert perfil
        public DataSet Insertarperfil(PerfilesE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil", SqlDbType = SqlDbType.Int, Value = entidad.Idc_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = entidad.Nombre });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_gpo_lib", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_gpo_lib });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_total_gpo_lib", SqlDbType = SqlDbType.Int, Value = entidad.Cad_total_gpo_lib });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_gpo_opc", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_gpo_opc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_total_gpo_opc", SqlDbType = SqlDbType.Int, Value = entidad.Cad_total_gpo_opc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_etiq_lib", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_etiq_lib });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_total_etiq_lib", SqlDbType = SqlDbType.Int, Value = entidad.Cad_total_etiq_lib });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_etiq_opc", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_etiq_opc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_total_etiq_opc", SqlDbType = SqlDbType.Int, Value = entidad.Cad_total_etiq_opc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_perfil_docs", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_perfil_docs });//add 02-12-2015
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_total_perfil_docs", SqlDbType = SqlDbType.Int, Value = entidad.Cad_perfil_docs_tot });//add 02-12-2015
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_archi", SqlDbType = SqlDbType.VarChar, Value = entidad.Pcadena_archi });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_archi", SqlDbType = SqlDbType.Int, Value = entidad.Ptotal_cadena_archi });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_examenes", SqlDbType = SqlDbType.VarChar, Value = entidad.Pcadena_examenes });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_examenes", SqlDbType = SqlDbType.Int, Value = entidad.Ptotal_examenes });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadenah", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_h });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadenah", SqlDbType = SqlDbType.Int, Value = entidad.Cad_total_h });
            try
            {
                ds = data.enviar("sp_aperfiles_captura", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //insert perfil
        public DataSet ValidaArchivo(PerfilesE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfil", SqlDbType = SqlDbType.Int, Value = entidad.Idc_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pruta", SqlDbType = SqlDbType.VarChar, Value = entidad.Pcadena_examenes });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.VarChar, Value = entidad.Ptipo });
            try
            {
                ds = data.enviar("sp_valida_modificacion_archivoshtml", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //insert perfil
        public DataSet Insertarperfil_borrador(PerfilesE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil", SqlDbType = SqlDbType.Int, Value = entidad.Idc_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = entidad.Nombre });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_gpo_lib", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_gpo_lib });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_total_gpo_lib", SqlDbType = SqlDbType.Int, Value = entidad.Cad_total_gpo_lib });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_gpo_opc", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_gpo_opc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_total_gpo_opc", SqlDbType = SqlDbType.Int, Value = entidad.Cad_total_gpo_opc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_etiq_lib", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_etiq_lib });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_total_etiq_lib", SqlDbType = SqlDbType.Int, Value = entidad.Cad_total_etiq_lib });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_etiq_opc", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_etiq_opc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_total_etiq_opc", SqlDbType = SqlDbType.Int, Value = entidad.Cad_total_etiq_opc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_perfil_docs", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_perfil_docs });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcad_total_perfil_docs", SqlDbType = SqlDbType.Int, Value = entidad.Cad_perfil_docs_tot });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_archi", SqlDbType = SqlDbType.VarChar, Value = entidad.Pcadena_archi });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_archi", SqlDbType = SqlDbType.Int, Value = entidad.Ptotal_cadena_archi });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_examenes", SqlDbType = SqlDbType.VarChar, Value = entidad.Pcadena_examenes });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_examenes", SqlDbType = SqlDbType.Int, Value = entidad.Ptotal_examenes });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadenah", SqlDbType = SqlDbType.VarChar, Value = entidad.Cadena_h });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadenah", SqlDbType = SqlDbType.Int, Value = entidad.Cad_total_h });
            try
            {
                ds = data.enviar("sp_aperfiles_captura_borrador", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet eliminarPerfil(PerfilesE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil", SqlDbType = SqlDbType.VarChar, Value = entidad.Idc_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pborrador", SqlDbType = SqlDbType.Bit, Value = entidad.Borrador });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bpuestos_perfil", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaPuestosPerfil(PerfilesE Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_perfil });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_perfil", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaSucursalPuesto(PerfilesE Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_perfil });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_direccion_sucursal_puesto", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Verifica si existe un borrador, si no existe lo crea
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet VerificaPerfilBorrador(PerfilesE Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfil", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            if (Entidad.Nombre != "")
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pnombre", SqlDbType = SqlDbType.VarChar, Value = Entidad.Nombre });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aperfiles_produccion-borrador", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// VERFICA LOS ARCHIVOS CON ETIQUETA RELACIONDA
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet UpdateEtiquetas(PerfilesE Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@total_cadena", SqlDbType = SqlDbType.Int, Value = Entidad.Cad_total_etiq_lib });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfil", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@cadena_archivos", SqlDbType = SqlDbType.Int, Value = Entidad.Cadena_etiq_lib });
            listparameters.Add(new SqlParameter() { ParameterName = "@produccion", SqlDbType = SqlDbType.Int, Value = Entidad.Borrador });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_mpuestos_perfil_d_eti_lib", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Verifica si existe un borrador, si no existe lo crea
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns></returns>
        public DataSet CopiaBorrador(PerfilesE Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_perfil_borr", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });
            if (Entidad.Nombre != "")
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@nombre", SqlDbType = SqlDbType.VarChar, Value = Entidad.Nombre });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aperfiles_borrador_borrador", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //validar nombre de perfil
        public DataSet perfil_validar(PerfilesE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil", SqlDbType = SqlDbType.Int, Value = entidad.Idc_perfil });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = entidad.Nombre });
            //11-09-2015 borrador
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoperfil_borr", SqlDbType = SqlDbType.Int, Value = entidad.Idc_puestoperfil_borr });
            listparameters.Add(new SqlParameter() { ParameterName = "@pborrador", SqlDbType = SqlDbType.Bit, Value = entidad.Borrador });
            try
            {
                ds = data.enviar("sp_puestos_perfil_validar", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SolicitarAutorizacion(PerfilesE Entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@id_puestoperfil_borr", SqlDbType = SqlDbType.Int, Value = Entidad.Idc_puestoperfil_borr });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Entidad.Usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Entidad.Pusuariopc });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_solicitar_autorizacion", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //add 02-12-2015
        public DataSet tipo_examenes(PerfilesE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.VarChar, Value = entidad.Ptipo });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_examenes", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet horarios(PerfilesE entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_horarios_perfiles", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet tipo_documentos_pre_empleados_lista()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_tipo_documentos_pre_empleados_lista", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}