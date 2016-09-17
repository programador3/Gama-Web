using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class OpcionesBL
    {
        public DataSet menu_general()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_menu_general", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet OpcionFavorita(OpcionesE Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_user });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_opcion", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_opcion });
            try
            {
                //ds = data.datos_Clientes(listparameters);sp_ausuario_opcion_reciente
                ds = data.enviar("sp_ausuario_opcion_reciente", listparameters, true);
                //ds = data.enviar("sp_notificaciones", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet EliminarOpcionFavorita(OpcionesE Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_user });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_opcion", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_opcion });
            try
            {
                //ds = data.datos_Clientes(listparameters);sp_ausuario_opcion_reciente
                ds = data.enviar("sp_eusuario_opcion_reciente", listparameters, true);
                //ds = data.enviar("sp_notificaciones", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet OpcionFavoritaCargar(OpcionesE Etiqueta)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = Etiqueta.Idc_user });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_usuarios_opciones_usadas", listparameters, false);
                //ds = data.enviar("sp_notificaciones", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet get_menu2(Entidades.OpcionesE opciones)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = opciones.Idc_user });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_menu_dinamico", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public string ir(Entidades.OpcionesE opciones)
        {
            string destino = "";
            try
            {
                //recupero mi tabla de menu
                DataSet ds = new DataSet();
                ds = menu_general();
                //metemos en un datatable para el filtrado  recuperacion de datos
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //buscamos el id de la opcion
                    string condicion = "idc_opcion=" + opciones.Idc_opcion;
                    //Aqui guardamos el resultado
                    DataRow[] findrow;
                    findrow = dt.Select(condicion);
                    if (findrow != null)
                    {
                        //recupero que accion ejecuta esta opcion
                        destino = findrow[0]["web_form"].ToString();
                    }
                    else
                    {
                        destino = "menu3.aspx";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return destino;
        }

        public DataTable preparar_funcion(string funcion)
        {
            Datos data = new Datos();
            DataTable dt = new DataTable();

            try
            {
                dt = data.enviar_funcion(funcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataSet AcessosDirectos(Entidades.OpcionesE opcion)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = opcion.Usuario_id });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo_apli", SqlDbType = SqlDbType.Int, Value = "|5|" });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_opciones_usuario", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //29-07-2015
        public DataSet opciones_menu(Entidades.OpcionesE opcion)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu1", SqlDbType = SqlDbType.VarChar, Value = opcion.Menu1 });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu2", SqlDbType = SqlDbType.VarChar, Value = opcion.Menu2 });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu3", SqlDbType = SqlDbType.VarChar, Value = opcion.Menu3 });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu4", SqlDbType = SqlDbType.VarChar, Value = opcion.Menu4 });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu5", SqlDbType = SqlDbType.VarChar, Value = opcion.Menu5 });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmenu6", SqlDbType = SqlDbType.VarChar, Value = opcion.Menu6 });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnivel", SqlDbType = SqlDbType.Int, Value = opcion.Nivel });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = opcion.Usuario_id });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo_aplicacion", SqlDbType = SqlDbType.Int, Value = opcion.Tipo_apli });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_opciones_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet MenuDinmaico(Entidades.OpcionesE opcion)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = opcion.Usuario_id });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescripcion", SqlDbType = SqlDbType.Int, Value = opcion.Search });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_menu_dinamico_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet MenuDinmaico2(Entidades.OpcionesE opcion)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = opcion.Usuario_id });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_menu_drop_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}