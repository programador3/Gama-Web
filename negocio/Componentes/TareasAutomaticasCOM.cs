using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class TareasAutomaticasCOM
    {
        public DataSet AgregarTarea(TareasAutomaticasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            if (Etiqueta.Pidc_tarea_auto != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tarea_auto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tarea_auto });
            }
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pdescripcion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_asigna", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto_asigna });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto_realiza", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto_realiza });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_empieza", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha_empieza });
            if (Etiqueta.Pfecha_termina != Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_termina", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha_termina });
            }
            listparameters.Add(new SqlParameter() { ParameterName = "@phoras_terminar", SqlDbType = SqlDbType.Int, Value = Etiqueta.Phoras_terminar });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfrecuencia", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfrecuencia });
            if (Etiqueta.Pdia_mes != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pdia_mes", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pdia_mes });
            }
            if (Etiqueta.Phora_especifica != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@phora_especifica", SqlDbType = SqlDbType.Int, Value = Etiqueta.Phora_especifica });
            }
            if (Etiqueta.Pnumero_horas != 0)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pnumero_horas", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pnumero_horas });
                listparameters.Add(new SqlParameter() { ParameterName = "@pnumero_horas_comienza", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pnumero_horas_comienza });
                listparameters.Add(new SqlParameter() { ParameterName = "@pnumero_horas_termina", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pnumero_horas_termina });
            }
            listparameters.Add(new SqlParameter() { ParameterName = "@plunes", SqlDbType = SqlDbType.Int, Value = Etiqueta.Plunes });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmartes", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pmartes });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmiercoles", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pmiercole });
            listparameters.Add(new SqlParameter() { ParameterName = "@pjueves", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pjueves });
            listparameters.Add(new SqlParameter() { ParameterName = "@pviernes", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pviernes });
            listparameters.Add(new SqlParameter() { ParameterName = "@psabado", SqlDbType = SqlDbType.Int, Value = Etiqueta.Psabado });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdomingo", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pdomingo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_a_tareas_automaticas", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet SP_fn_puestos_quien_recluta()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_fn_puestos_quien_recluta", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet EliminarTarea(TareasAutomaticasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tarea_auto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tarea_auto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_e_tareas_automaticas", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaTareas(TareasAutomaticasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();

            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_tareas_automaticas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaTareasEdicion(TareasAutomaticasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tarea_auto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_tarea_auto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_tareas_automaticas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet DatosGraficas(TareasAutomaticasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoconsulta", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@PMIS_DEPTOS", SqlDbType = SqlDbType.Int, Value = Etiqueta.Psolomisdeptos });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto_realiza });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_depto", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_depto });
            if (Etiqueta.Pfecha_empieza != Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_inicio", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha_empieza });
                listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_fin", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha_termina });
            }
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_graficas_tareas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet DatosGraficasReclu(TareasAutomaticasENT Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pFECHA_INICIAL", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha_empieza });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_fin", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pfecha_termina });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTIPO", SqlDbType = SqlDbType.Int, Value = Etiqueta.Ptipofiltro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puestoreclu", SqlDbType = SqlDbType.Int, Value = Etiqueta.Pidc_puesto_realiza });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_grafica_reclutamiento", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}