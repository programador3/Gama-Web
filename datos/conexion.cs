using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace datos
{
    internal class conexion
    {
        public static SqlConnection conectar()
        {
            SqlConnection connection = new SqlConnection();
            string cadena = obten_cadena_con();
            connection.ConnectionString = cadena;
            return connection;
        }

        public static DataSet execute_sp(string query, List<SqlParameter> ListParameters)
        {
            SqlCommand command;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    string cadena = obten_cadena_con();
                    connection.ConnectionString = cadena;
                    connection.Open();
                    command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter item in ListParameters)
                    {
                        command.Parameters.AddWithValue(item.ParameterName, item.SqlDbType).Value = item.Value;
                    }
                    adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command = null;
                adapter = null;
            }
            return ds;
        }

        //ejecuta una funcion y lo regresa en un datatable
        public static DataTable execute_funcion(string query)
        {
            SqlCommand command;
            SqlDataAdapter adapter;
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    string cadena = obten_cadena_con();
                    connection.ConnectionString = cadena;
                    connection.Open();
                    command = new SqlCommand(query, connection);

                    adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command = null;
                adapter = null;
            }
            return dt;
        }

        //obtener cadena de conexion segun el parametro de  System.Configuration.ConfigurationManager.AppSettings["cs"];
        public static string obten_cadena_con()
        {
            string cadena;
            try
            {
                string cs = System.Configuration.ConfigurationManager.AppSettings["cs"];

                if (cs == "P")
                    cadena = recursos.cadena_conexion;
                else
                    cadena = recursos.cadena_conexion_respa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cadena;
        }

        //ejecuta sp enviado desde ajax
        public static DataSet execute_sp_ajax(string sp, string[] parametros, object[] valores)
        {
            SqlCommand command;
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    string cadena = obten_cadena_con();
                    connection.ConnectionString = cadena;
                    connection.Open();
                    command = new SqlCommand(sp, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 60000;
                    if (parametros != null)
                    {
                        for (int i = 0; i <= (parametros.Length - 1); i++)
                        {
                            command.Parameters.AddWithValue(parametros[i], valores[i]);
                        }
                    }
                    adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command = null;
                adapter = null;
            }
            return ds;
        }
    }
}