using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace negocio.Componentes
{
    public class ticket_servCOM
    {

        public DataSet Usuarios_puesto(ticket_servENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", Value = ent.Pidc_puesto });

            try
            {
                ds = data.enviar("sp_Usuarios_puesto", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet ticket_serv_todos()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_ticketserv", Value = ent.Pidc_ticketserv });
            listparameters.Add(new SqlParameter() { ParameterName = "@PVERTODO", Value = true });

            try
            {
                ds = data.enviar("sp_ticketserv", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet ticket_serv(ticket_servENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_ticketserv", Value = ent.Pidc_ticketserv });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", Value = ent.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", Value = ent.Pidc_usuario });

            try
            {
                ds = data.enviar("sp_ticketserv", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //sp_aticketserv
        public DataSet ticket_serv_Espera(ticket_servENT ent,Datos_Usuario_logENT dul)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = dul.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = dul.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = dul.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = dul.Pusuariopc });


            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_ticketserv", Value = ent.Pidc_ticketserv });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", Value = ent.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmotivo", Value = ent.Pmotivo });
            
            try
            {
                ds = data.enviar("sp_aticketserv_espera", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /*  sp_aticketserv_aten   */
        public DataSet ticket_serv_aten(ticket_servENT ent, Datos_Usuario_logENT dul)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", Value = dul.Pidc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", Value = dul.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", Value = dul.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", Value = dul.Pusuariopc });
            
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_ticketserv", Value = ent.Pidc_ticketserv });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_ticketserva", Value = ent.Pidc_ticketserva });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", Value = ent.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmotivo", Value = ent.Pmotivo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", Value = ent.Pobservaciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario_term", Value = ent.Pidc_usuario_term });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcontraseña", Value = ent.Pcontraseña });


            try
            {
                ds = data.enviar("sp_aticketserv_aten", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        /* sp_ticketserv_pendiente */
        public DataSet ticketserv_pendiente(ticket_servENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", Value = ent.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tareaser", Value = ent.Pidc_tareaser });
            
            try
            {
                ds = data.enviar("sp_ticketserv_pendiente", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /*  reporte */
        public DataSet ticket_serv_reporte(ticket_servENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_inicio", Value = ent.PfechaInicio });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_fin", Value = ent.Pfechafin });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", Value = ent.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_depto_aten", Value = ent.Pidc_depto_aten });
            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_depto_rep", Value = ent.Pidc_depto_rep });

            try
            {
                ds = data.enviar("sp_ticketserv_reporte", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
                
        public DataSet CargaComboPueatos(ticket_servENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfiltro", SqlDbType = SqlDbType.Int, Value = ent.Pfiltro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_depto", Value = ent.Pidc_depto_aten });

            try
            {
                
                ds = data.enviar("sp_puestos_activos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargaComboDeptos(ticket_servENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Int, Value = "T" });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_COMBO_DEPTOS", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        /* Grafica */
        public DataSet DatosGrafica(ticket_servENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            //listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Int, Value = "T" });
            try
            {
                
                ds = data.enviar("sp_ticketserv_Grafica", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
