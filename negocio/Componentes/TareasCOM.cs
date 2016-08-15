using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class TareasCOM
    {
        public DataSet CargarArbolTareas(TareasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tarea", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_tarea });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_tareas_arbol", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaCatologoPuestos(TareasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@idc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_catalogo_puestos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarPendientes(TareasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_puesto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_pendientes_tareas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarSinEmpleado(TareasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_tareas_sin_empleados", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarProveedores(TareasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_proveedores_tareas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarPendientesHoy(TareasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_asigna", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_puesto_asigna });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptodo", SqlDbType = SqlDbType.Int, Value = entidad.Pcorrecto });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptodos_puestos", SqlDbType = SqlDbType.Int, Value = entidad.Parchivo });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_tareas_pendientes_hoy", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarArticulos(TareasENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@vfiltro", SqlDbType = SqlDbType.Int, Value = entidad.Pcadena_arch });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_buscar_cat_prueba", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AgregarTarea(TareasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_arch });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_archi", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_arch });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdescripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoasigna", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto_asigna });
            if (Etiqueta.Pidc_tarea != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tarea", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tarea });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_a_tareas", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AgregarTareaMulti(TareasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_arch });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_archi", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_arch });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotalcadena_puestos", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_pro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_puestos", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_pro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdescripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoasigna", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto_asigna });
            if (Etiqueta.Pidc_tarea != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tarea", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tarea });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_a_tareas_multi", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AgregarMovimiento(TareasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tarea", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tarea });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdescripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Date, Value = Etiqueta.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Char, Value = Etiqueta.Ptipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_TAREA_HISTORIAL", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tarea_h });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_cadena_pro", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptotal_cadena_pro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_pro", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcadena_pro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcorrecto", SqlDbType = SqlDbType.Bit, Value = Etiqueta.Pcorrecto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcomentarios", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pcomentarios });
            listparameters.Add(new SqlParameter() { ParameterName = "@PAPLICAR_CAMBIOTODOS", SqlDbType = SqlDbType.Bit, Value = Etiqueta.PAPLICAR_CAMBIOTODOS });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_cambio", SqlDbType = SqlDbType.Bit, Value = Etiqueta.Pcadena_arch });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTOTAL_CADENA_cambio", SqlDbType = SqlDbType.Bit, Value = Etiqueta.Ptotal_cadena_arch });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_a_cambios_tareas_historial", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet ReasignarTarea(TareasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tarea", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tarea });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdescripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Char, Value = Etiqueta.Ptipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo_cambio_tarea", SqlDbType = SqlDbType.Char, Value = Etiqueta.Ptipo_cambio_tarea });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PUESTO_NUEVO", SqlDbType = SqlDbType.Char, Value = Etiqueta.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PUESTO_asigna_NUEVO", SqlDbType = SqlDbType.Char, Value = Etiqueta.Pidc_puesto_asigna });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Date, Value = Etiqueta.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@PAPLICAR_CAMBIOTODOS", SqlDbType = SqlDbType.Bit, Value = Etiqueta.PAPLICAR_CAMBIOTODOS });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_cambio", SqlDbType = SqlDbType.Bit, Value = Etiqueta.Pcadena_arch });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTOTAL_CADENA_cambio", SqlDbType = SqlDbType.Bit, Value = Etiqueta.Ptotal_cadena_arch });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_a_cambios_tareas_historial", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AgregarComentario(TareasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tarea", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tarea });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdescripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Char, Value = Etiqueta.Ptipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_TAREA_archivo", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tarea_h });
            listparameters.Add(new SqlParameter() { ParameterName = "@pextension", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pextension });
            listparameters.Add(new SqlParameter() { ParameterName = "@parchivo", SqlDbType = SqlDbType.Int, Value = Etiqueta.Parchivo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_a_comentarios_tareas", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AgregarAvance(TareasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tarea", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tarea });
            listparameters.Add(new SqlParameter() { ParameterName = "@pavance", SqlDbType = SqlDbType.VarChar, Value = Etiqueta.Pavance });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aavance_tarea", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarTareas(TareasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            if (Etiqueta.Pidc_depto != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_depto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_depto });
            }
            if (Etiqueta.Pidc_puesto != 0)
            {
                listparameters.Add(new SqlParameter()
                {
                    ParameterName = "@pidc_puesto",
                    SqlDbType = SqlDbType.Int,
                    Value = Etiqueta.Pidc_puesto
                });
            }
            if (Etiqueta.Pidc_puesto_asigna != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_asigna", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto_asigna });
            }
            if (Etiqueta.Pidc_tarea != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tarea", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tarea });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_tareas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarTareasAsignadas(TareasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            if (Etiqueta.Pidc_depto != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_depto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_depto });
            }
            if (Etiqueta.Pidc_puesto != 0)
            {
                listparameters.Add(new SqlParameter()
                {
                    ParameterName = "@pidc_puesto",
                    SqlDbType = SqlDbType.Int,
                    Value = Etiqueta.Pidc_puesto
                });
            }
            if (Etiqueta.Pidc_puesto_asigna != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_asigna", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto_asigna });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_mis_tareas_asignadas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarTareasAsigne(TareasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            if (Etiqueta.Pidc_depto != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_depto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_depto });
            }
            if (Etiqueta.Pidc_puesto != 0)
            {
                listparameters.Add(new SqlParameter()
                {
                    ParameterName = "@pidc_puesto",
                    SqlDbType = SqlDbType.Int,
                    Value = Etiqueta.Pidc_puesto
                });
            }
            if (Etiqueta.Pidc_puesto_asigna != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_asigna", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto_asigna });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_mis_tareas_asigne", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet ValidarFechas(TareasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_validar_fechas_tareas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet InformacionAdicional(TareasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tipoi", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tipoi });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_proceso", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_proceso });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_tareas_info_adicional", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet InformacionAdicionalFechas(TareasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tipoi", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tipoi });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_proceso", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_proceso });
            listparameters.Add(new SqlParameter() { ParameterName = "@PFECHA1", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@PFECHA2", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha_fin });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_tareas_info_adicional", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet TareasResultadoDetalles(TareasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_PUESTO_ASIGNA", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto_asigna });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_USUARIO", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@PFECHA_INICIO", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@PFECHA_FIN", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha_fin });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipofiltro", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptipof });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipofiltro_sistema", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptipofs });
            if (Etiqueta.Pidc_tarea != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tarea", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tarea });
            }
            if (Etiqueta.Pidc_depto != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_depto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_depto });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_tareas_puestos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}