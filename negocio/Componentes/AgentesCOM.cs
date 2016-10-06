using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class AgentesCOM
    {
        public DataSet sp_arellamar_ven_nuevo(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@VOBSLLA", SqlDbType = SqlDbType.VarChar, Value = entidad.Pobsr });
            listparameters.Add(new SqlParameter() { ParameterName = "@vfechaa", SqlDbType = SqlDbType.VarChar, Value = entidad.pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.VarChar, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_arellamar_ven_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_aclientes_tel_solcambio_nuevo(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_telcli", SqlDbType = SqlDbType.VarChar, Value = entidad.pidc_telcli });
            listparameters.Add(new SqlParameter() { ParameterName = "@pemail", SqlDbType = SqlDbType.VarChar, Value = entidad.pemail });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre", SqlDbType = SqlDbType.VarChar, Value = entidad.pnombre });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcelular", SqlDbType = SqlDbType.VarChar, Value = entidad.pcelular });
            listparameters.Add(new SqlParameter() { ParameterName = "@PFECHA", SqlDbType = SqlDbType.VarChar, Value = entidad.pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pactivo", SqlDbType = SqlDbType.VarChar, Value = entidad.pactivo });
            listparameters.Add(new SqlParameter() { ParameterName = "@phobbies", SqlDbType = SqlDbType.VarChar, Value = entidad.phobbie });
            listparameters.Add(new SqlParameter() { ParameterName = "@pequipo", SqlDbType = SqlDbType.VarChar, Value = entidad.pequipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tcontacto", SqlDbType = SqlDbType.VarChar, Value = entidad.pidc_tcontacto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfunciones", SqlDbType = SqlDbType.VarChar, Value = entidad.pfunciones });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_titulo", SqlDbType = SqlDbType.VarChar, Value = entidad.pidc_titulo });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptelefono", SqlDbType = SqlDbType.VarChar, Value = entidad.ptelefono });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.VarChar, Value = entidad.Pidc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@popcion", SqlDbType = SqlDbType.VarChar, Value = entidad.popcion });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aclientes_tel_solcambio_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_aagentes_act_cotizacion_nueva_web(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal2", SqlDbType = SqlDbType.VarChar, Value = entidad.Ptotalcadenaarti });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena2", SqlDbType = SqlDbType.VarChar, Value = entidad.Pcadenaarti });
            listparameters.Add(new SqlParameter() { ParameterName = "@vfecha", SqlDbType = SqlDbType.VarChar, Value = DateTime.Now });
            listparameters.Add(new SqlParameter() { ParameterName = "@vfecha_salida", SqlDbType = SqlDbType.VarChar, Value = DateTime.Now });
            listparameters.Add(new SqlParameter() { ParameterName = "@vidcli", SqlDbType = SqlDbType.VarChar, Value = entidad.Pidc_cliente});
            listparameters.Add(new SqlParameter() { ParameterName = "@vida", SqlDbType = SqlDbType.VarChar, Value = entidad.Pidc_agente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aagentes_act_cotizacion_nueva_web", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_datos_editar_contacto(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_telcli", SqlDbType = SqlDbType.Int, Value = entidad.pidc_telcli });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_editar_contacto", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_combo_titulos_profesionales(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_titulos_profesionales", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_combo_tipos_contacto(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_tipos_contacto", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_articulos_master_cliente(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_articulos_master_cliente", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        
       public DataSet sp_buscar_articulo_VENTAS_existencias(object codigo, string tipo, int idc_sucursal, int idc_usuario)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Int, Value = tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.Int, Value = idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pvalor", SqlDbType = SqlDbType.Int, Value = codigo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_buscar_articulo_VENTAS_existencias", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_cambio_costo_arti_cedis_fecha(int codigo, int idc_cedis, DateTime pfecha)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cedis", SqlDbType = SqlDbType.Int, Value = idc_cedis });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = codigo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = pfecha });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cambio_costo_arti_cedis_fecha", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_ultimo_precio_cliente(int codigo, int idc_cliente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = codigo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ultimo_precio_cliente", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_precio_cliente_cedis_rangos(int codigo, int idc_cliente, int idc_sucursal, decimal cantidad, bool cambiolista)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = codigo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.Int, Value = idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcantidad", SqlDbType = SqlDbType.Int, Value = cantidad });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcambiolista", SqlDbType = SqlDbType.Int, Value = cambiolista });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_precio_cliente_cedis_rangos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_datos_cliente(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_cliente", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_ARTICULOS_CLIENTE_DESC_cedis(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ARTICULOS_CLIENTE_DESC_cedis", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_buscar_articulos_sencillo_UNIMED(string pfiltro)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pvalor", SqlDbType = SqlDbType.Int, Value = pfiltro });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_buscar_articulos_sencillo_UNIMED", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_ver_tareas_agente_detalles(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_agente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_agente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = entidad.pfecha });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ver_tareas_agente_detalles", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SP_VER_TAREAS_CLIENTE_DETALLES_TODO(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_agente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_agente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = entidad.pfecha });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_VER_TAREAS_CLIENTE_DETALLES_TODO", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_datos_agregar_contactos(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_agregar_contactos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_cliente_ubicacion(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cliente_ubicacion", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_clientes_tel_mtto1(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_clientes_tel_mtto1", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_datos_clientes_articulos(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pgrupo", SqlDbType = SqlDbType.Int, Value = 0 });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_clientes_articulos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_articulos_agregar_precio(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfiltro", SqlDbType = SqlDbType.Int, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmaster", SqlDbType = SqlDbType.Int, Value = true });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_articulos_agregar_precio", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_atareas_clientes_nuevo(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_agente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_agente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = entidad.pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmeta_venta", SqlDbType = SqlDbType.Int, Value = entidad.pmeta_venta });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobs_venta", SqlDbType = SqlDbType.Int, Value = entidad.Pobsr });
            listparameters.Add(new SqlParameter() { ParameterName = "@particulos", SqlDbType = SqlDbType.Int, Value = entidad.Pcadenaarti });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnum_articulos", SqlDbType = SqlDbType.Int, Value = entidad.Ptotalcadenaarti });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_atareas_clientes_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_aclientes_articulos_nuevo(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_actiage });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = entidad.pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Int, Value = entidad.ptipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@popcion", SqlDbType = SqlDbType.Int, Value = entidad.popcion });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aclientes_articulos_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_atareas_clientes_rev_nuevo(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_agente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_agente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = entidad.pfecha });
            listparameters.Add(new SqlParameter() { ParameterName = "@particulos", SqlDbType = SqlDbType.Int, Value = entidad.Pcadenaarti });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnum_articulos", SqlDbType = SqlDbType.Int, Value = entidad.Ptotalcadenaarti });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_atareas_clientes_rev_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet registrar_visita(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_actiage", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_actiage });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_agente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_agente });
            listparameters.Add(new SqlParameter() { ParameterName = "@platitud", SqlDbType = SqlDbType.Int, Value = entidad.Plat });
            listparameters.Add(new SqlParameter() { ParameterName = "@plongitud", SqlDbType = SqlDbType.Int, Value = entidad.Plon });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = entidad.Pdirecip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pnombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = entidad.Pusuariopc });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aagentes_act_movil_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet cargar_agentesusuarios(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_agentes_usu", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet cargar_ficha(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ver_fichacliente", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet credito_disponible(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_credito_disponible", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_promociones_cliente(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@vidc_listap", SqlDbType = SqlDbType.Int, Value = entidad.vidc_listap });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_promociones_cliente", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet clientes_visitas(int idc_agente, DateTime fechai, DateTime fechaf, bool pacti, bool aldia, int dia)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_agente", SqlDbType = SqlDbType.Int, Value = idc_agente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfechai", SqlDbType = SqlDbType.Int, Value = fechai });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfechaf", SqlDbType = SqlDbType.Int, Value = fechaf });
            listparameters.Add(new SqlParameter() { ParameterName = "@pacti", SqlDbType = SqlDbType.Int, Value = pacti });
            listparameters.Add(new SqlParameter() { ParameterName = "@aldia", SqlDbType = SqlDbType.Int, Value = aldia });
            listparameters.Add(new SqlParameter() { ParameterName = "@pd", SqlDbType = SqlDbType.Int, Value = dia });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_FN_efectividad_agentes_mo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}