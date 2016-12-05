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
        public DataSet sp_ver_PREped(int idc_preped)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_preped", SqlDbType = SqlDbType.Int, Value = idc_preped });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_ver_PREped", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_preped_x_autorizar(int idc_usuario, int idc_cliente, int idc_agente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_agente", SqlDbType = SqlDbType.Int, Value = idc_agente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_preped_x_autorizar", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_combo_agentes_usu(int idc_usuario)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
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
        public DataSet sp_fn_validar_fechas_pagada(DateTime fecha)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.Int, Value = fecha });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_fn_validar_fechas_pagada", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_promociones_cliente(int idc_cliente, int vidc_listap)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@vidc_listap", SqlDbType = SqlDbType.Int, Value = vidc_listap });
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
        public DataSet sp_lista_precios_x_familia(int idc_cliente, int pformato, int pprecios, int pdescuentos, int ppremin, int idc_sucursal, bool separar)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pformato", SqlDbType = SqlDbType.Int, Value = pformato });
            listparameters.Add(new SqlParameter() { ParameterName = "@pprecios", SqlDbType = SqlDbType.Int, Value = pprecios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdescuentos", SqlDbType = SqlDbType.Int, Value = pdescuentos });
            listparameters.Add(new SqlParameter() { ParameterName = "@ppremin", SqlDbType = SqlDbType.Int, Value = ppremin });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.Int, Value = idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pweb", SqlDbType = SqlDbType.Int, Value = separar });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_lista_precios_x_familia", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet sp_oc_clientes2(int idc_cliente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_oc_clientes2", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_combo_bancos()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_bancos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_fn_porcentaje_comision(int tipo, decimal distancia)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", SqlDbType = SqlDbType.Int, Value = tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdistancia", SqlDbType = SqlDbType.Int, Value = distancia });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_fn_porcentaje_comision", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_bcolonias(string valor)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pvalor", SqlDbType = SqlDbType.Int, Value = valor });
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

        public DataSet sp_aclientes_chekplus_nuevo(int idc_usuario, string ip, string pc, string usuariopc, string cambios, string nombre, string folio, string clave, string calle, string numero,
        int idc_colonia, int idc_cliente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = ip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = pc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = usuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcambios", SqlDbType = SqlDbType.Int, Value = cambios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre", SqlDbType = SqlDbType.Int, Value = nombre });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfolio_elector", SqlDbType = SqlDbType.Int, Value = folio });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcve_elector", SqlDbType = SqlDbType.Int, Value = clave });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcalle", SqlDbType = SqlDbType.Int, Value = calle });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnumero", SqlDbType = SqlDbType.Int, Value = numero });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_colonia", SqlDbType = SqlDbType.Int, Value = idc_colonia });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aclientes_chekplus_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_acuentasclien_nuevo(int idc_cliente, int idc_banco, string num_cuenta, int idc_usuario,
            string ip, string pc, string usuariopc, char tipo, string cambios, int SFOLIO)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = ip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = pc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = usuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipom", SqlDbType = SqlDbType.Int, Value = tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcambios", SqlDbType = SqlDbType.Int, Value = cambios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_banco", SqlDbType = SqlDbType.Int, Value = idc_banco });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnum_cuenta", SqlDbType = SqlDbType.Int, Value = num_cuenta });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_acuentasclien_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_achek_plus_pre_nuevo(int idc_cuenta, decimal monto, int dias, string clave, string atendio, string calle, string numero, int idc_colonia,
            string folio, int idc_usuario, string ip, string pc, string usuariopc, string tipo, string cambios, int sfolio)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.VarChar, Value = ip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.VarChar, Value = pc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.VarChar, Value = usuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cuentacli", SqlDbType = SqlDbType.Int, Value = idc_cuenta });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmonto", SqlDbType = SqlDbType.Int, Value = monto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdias", SqlDbType = SqlDbType.Int, Value = dias });
            listparameters.Add(new SqlParameter() { ParameterName = "@pclave_elector", SqlDbType = SqlDbType.Int, Value = clave });
            listparameters.Add(new SqlParameter() { ParameterName = "@patendio", SqlDbType = SqlDbType.Int, Value = atendio });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcalle", SqlDbType = SqlDbType.Int, Value = calle });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnumero", SqlDbType = SqlDbType.Int, Value = numero });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_colonia", SqlDbType = SqlDbType.Int, Value = idc_colonia });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfolio_elector", SqlDbType = SqlDbType.Int, Value = folio });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipom", SqlDbType = SqlDbType.Int, Value = tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcambios", SqlDbType = SqlDbType.Int, Value = cambios });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_achek_plus_pre_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_clientes_chekplus(int idc_cliente, int idc_dirchekplus)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_dirchekplus", SqlDbType = SqlDbType.Int, Value = idc_dirchekplus });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_clientes_chekplus", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_cuentascli(int idc_cliente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cuentascli", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_folios(int idc_tabla)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tabla", SqlDbType = SqlDbType.Int, Value = idc_tabla });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_folios", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_bancos()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bancos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_bgastos_chqseg(int codigo)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = codigo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bgastos_chqseg", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SP_nc_auto_CLIENTE_articulo(int codigo, int idc_cliente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = codigo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.VarChar, Value = idc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_nc_auto_CLIENTE_articulo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_codigo_articulo(int idc_articulo)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = idc_articulo });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_codigo_articulo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SP_arti_conv_int(decimal cantidad, int idc_articulo)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = idc_articulo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcantidad", SqlDbType = SqlDbType.VarChar, Value = cantidad });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_arti_conv_int", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_fn_cambio_costo_arti_cedis_fecha(int idc_ced, int idc_articulo, DateTime fecha)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = idc_articulo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_ced", SqlDbType = SqlDbType.VarChar, Value = idc_ced });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha", SqlDbType = SqlDbType.VarChar, Value = fecha });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_fn_cambio_costo_arti_cedis_fecha", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_bexistencia_disponible(int idc_alamacen, int idc_articulo, decimal exif)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = idc_articulo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_almacen", SqlDbType = SqlDbType.VarChar, Value = idc_alamacen });
            listparameters.Add(new SqlParameter() { ParameterName = "@pexif", SqlDbType = SqlDbType.VarChar, Value = exif });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bexistencia_disponible", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_precio_cliente_cedis(int codigo, int idc_cliente, int idc_sucursal)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = codigo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.VarChar, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.VarChar, Value = idc_sucursal });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_precio_cliente_cedis", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_precio_cliente_cedis_rangos1(int codigo, int idc_cliente, int idc_sucursal, decimal cantidad, bool cambiolista, string especif, int num_especif)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = codigo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.VarChar, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.VarChar, Value = idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcantidad", SqlDbType = SqlDbType.VarChar, Value = cantidad });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcambiolista", SqlDbType = SqlDbType.VarChar, Value = cambiolista });
            listparameters.Add(new SqlParameter() { ParameterName = "@pespecificaciones", SqlDbType = SqlDbType.VarChar, Value = especif });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnum_especif", SqlDbType = SqlDbType.VarChar, Value = num_especif });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_precio_cliente_cedis_rangos1", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SP_Cadenas_Fletes_Preped(string cadena1, int total, int tipo_camion, int idc_sucursal, int idc_colonia, int idc_cliente,
            bool DesIVA, int iva, string cadena2)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena", SqlDbType = SqlDbType.VarChar, Value = cadena1 });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptot", SqlDbType = SqlDbType.VarChar, Value = total });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tipocam", SqlDbType = SqlDbType.VarChar, Value = tipo_camion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.VarChar, Value = idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_colonia", SqlDbType = SqlDbType.VarChar, Value = idc_colonia });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_CLIENTE", SqlDbType = SqlDbType.VarChar, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@DESIVA", SqlDbType = SqlDbType.VarChar, Value = DesIVA });
            listparameters.Add(new SqlParameter() { ParameterName = "@IVA", SqlDbType = SqlDbType.VarChar, Value = iva });
            listparameters.Add(new SqlParameter() { ParameterName = "@PCAD2", SqlDbType = SqlDbType.VarChar, Value = cadena2 });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_Cadenas_Fletes_Preped", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_sucursales_combo_entregas()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_sucursales_combo_entregas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SP_CONVERSION_ARTICULO(int codigo, decimal idc_cliente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = codigo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcantidad", SqlDbType = SqlDbType.VarChar, Value = idc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_CONVERSION_ARTICULO", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

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
            listparameters.Add(new SqlParameter() { ParameterName = "@vidcli", SqlDbType = SqlDbType.VarChar, Value = entidad.Pidc_cliente });
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

        public DataSet sp_apreped_CAMBIO_PRECIOS3_nuevo(int idc_cliente, double monto, int idc_sucursal, bool desiva, int idc_iva, int idc_usuario, int idc_almacen, string direcip, string nombrepc, string usuariopc,
                char tipom, string cambios, string darti, int totart, System.DateTime fecha_ent, bool proye, int idpro, string occ, int idc_colonia, string calle,
                string numero, int cod_postal, string obs, int IDC_FOLIOPRECP, int sipasa, int entdir, bool bitcro, bool bitocc, string dsinexi, string DCOSBAJO,
                int folio, bool BITLLA, string MENSAJE, int SIPASA2, char tipo, double FLETE, string recoge, int idc_occli, int idc_sucursal_recoge, int Vtipop,
                int idc_banco, System.DateTime fecha_deposito, double monto_pago, string observaciones_pago, bool confirmar_pago, string cadena_acti, string total_acti, int total2_acti, string cadena2_acti, string vdarti2_nueva,
                int VTOTA_NUEVO, string detalles, decimal fleteaut)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmonto", SqlDbType = SqlDbType.Int, Value = monto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.Int, Value = idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdesiva", SqlDbType = SqlDbType.Int, Value = desiva });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_iva", SqlDbType = SqlDbType.Int, Value = idc_iva });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_almacen", SqlDbType = SqlDbType.Int, Value = idc_almacen });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.Int, Value = direcip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.Int, Value = nombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.Int, Value = usuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipom", SqlDbType = SqlDbType.Int, Value = tipom });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcambios", SqlDbType = SqlDbType.Int, Value = cambios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdarti", SqlDbType = SqlDbType.Int, Value = darti });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotart", SqlDbType = SqlDbType.Int, Value = totart });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_ent", SqlDbType = SqlDbType.Int, Value = fecha_ent });
            listparameters.Add(new SqlParameter() { ParameterName = "@pproye", SqlDbType = SqlDbType.Int, Value = proye });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidpro", SqlDbType = SqlDbType.Int, Value = idpro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pocc", SqlDbType = SqlDbType.Int, Value = occ });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_colonia", SqlDbType = SqlDbType.Int, Value = idc_colonia });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcalle", SqlDbType = SqlDbType.Int, Value = calle });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnumero", SqlDbType = SqlDbType.Int, Value = numero });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcod_postal", SqlDbType = SqlDbType.Int, Value = cod_postal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobs", SqlDbType = SqlDbType.Int, Value = obs });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_FOLIOPRECP", SqlDbType = SqlDbType.Int, Value = IDC_FOLIOPRECP });
            listparameters.Add(new SqlParameter() { ParameterName = "@PSIPASA", SqlDbType = SqlDbType.Int, Value = sipasa });
            listparameters.Add(new SqlParameter() { ParameterName = "@pentdir", SqlDbType = SqlDbType.Int, Value = entdir });
            listparameters.Add(new SqlParameter() { ParameterName = "@pbitocc", SqlDbType = SqlDbType.Int, Value = bitocc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pbitcro", SqlDbType = SqlDbType.Int, Value = bitcro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdsinexi", SqlDbType = SqlDbType.Int, Value = dsinexi });
            listparameters.Add(new SqlParameter() { ParameterName = "@PDCOSBAJO", SqlDbType = SqlDbType.Int, Value = DCOSBAJO });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfolio", SqlDbType = SqlDbType.Int, Value = folio });
            listparameters.Add(new SqlParameter() { ParameterName = "@PBITLLA", SqlDbType = SqlDbType.Int, Value = BITLLA });
            listparameters.Add(new SqlParameter() { ParameterName = "@PMENSAJE", SqlDbType = SqlDbType.Int, Value = MENSAJE });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTIPO", SqlDbType = SqlDbType.Int, Value = tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@PSIPASA2", SqlDbType = SqlDbType.Int, Value = SIPASA2 });
            listparameters.Add(new SqlParameter() { ParameterName = "@PFLETE", SqlDbType = SqlDbType.Int, Value = FLETE });
            listparameters.Add(new SqlParameter() { ParameterName = "@precoge", SqlDbType = SqlDbType.Int, Value = recoge });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_occli", SqlDbType = SqlDbType.Int, Value = idc_occli });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal_recoge", SqlDbType = SqlDbType.Int, Value = idc_sucursal_recoge });
            listparameters.Add(new SqlParameter() { ParameterName = "@Vtipop", SqlDbType = SqlDbType.Int, Value = Vtipop });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_banco", SqlDbType = SqlDbType.Int, Value = idc_banco });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_deposito", SqlDbType = SqlDbType.Int, Value = fecha_deposito });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmonto_pago", SqlDbType = SqlDbType.Int, Value = monto_pago });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones_pago", SqlDbType = SqlDbType.Int, Value = observaciones_pago });
            listparameters.Add(new SqlParameter() { ParameterName = "@Pconfirmar_pago", SqlDbType = SqlDbType.Int, Value = confirmar_pago });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_acti", SqlDbType = SqlDbType.Int, Value = cadena_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_acti", SqlDbType = SqlDbType.Int, Value = total_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal2_acti", SqlDbType = SqlDbType.Int, Value = total2_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena2_acti", SqlDbType = SqlDbType.Int, Value = cadena2_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdarti_NUEVA", SqlDbType = SqlDbType.Int, Value = vdarti2_nueva });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTOTART_NUEVO", SqlDbType = SqlDbType.Int, Value = VTOTA_NUEVO });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdetalles", SqlDbType = SqlDbType.Int, Value = detalles });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfleteaut", SqlDbType = SqlDbType.Int, Value = fleteaut });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apreped_CAMBIO_PRECIOS3_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_apreped_creditos4_nuevo(int idc_cliente, double monto, int idc_sucursal, bool desiva, int idc_iva, int idc_usuario, int idc_almacen, string direcip, string nombrepc, string usuariopc,
        char tipom, string cambios, string darti, int totart, System.DateTime fecha_ent, bool proye, int idpro, string occ, int idc_colonia, string calle,
        string numero, int cod_postal, string obs, int IDC_FOLIOPRECP, int sipasa, int entdir, bool bitcro, bool bitocc, string dsinexi, string DCOSBAJO,
        int folio, bool BITLLA, string MENSAJE, char tipo, int SIPASA2, double FLETE, string recoge, int idc_occli, int idc_sucursal_recoge, int Vtipop,
        int idc_banco, System.DateTime fecha_deposito, double monto_pago, string observaciones_pago, bool confirmar_pago, string cadena_acti, string total_acti, int total2_acti, string cadena2_acti, string vdarti2_nueva,
        int VTOTA_NUEVO)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmonto", SqlDbType = SqlDbType.Int, Value = monto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.Int, Value = idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdesiva", SqlDbType = SqlDbType.Int, Value = desiva });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_iva", SqlDbType = SqlDbType.Int, Value = idc_iva });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_almacen", SqlDbType = SqlDbType.Int, Value = idc_almacen });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.Int, Value = direcip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.Int, Value = nombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.Int, Value = usuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipom", SqlDbType = SqlDbType.Int, Value = tipom });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcambios", SqlDbType = SqlDbType.Int, Value = cambios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdarti", SqlDbType = SqlDbType.Int, Value = darti });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotart", SqlDbType = SqlDbType.Int, Value = totart });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_ent", SqlDbType = SqlDbType.Int, Value = fecha_ent });
            listparameters.Add(new SqlParameter() { ParameterName = "@pproye", SqlDbType = SqlDbType.Int, Value = proye });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidpro", SqlDbType = SqlDbType.Int, Value = idpro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pocc", SqlDbType = SqlDbType.Int, Value = occ });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_colonia", SqlDbType = SqlDbType.Int, Value = idc_colonia });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcalle", SqlDbType = SqlDbType.Int, Value = calle });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnumero", SqlDbType = SqlDbType.Int, Value = numero });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcod_postal", SqlDbType = SqlDbType.Int, Value = cod_postal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobs", SqlDbType = SqlDbType.Int, Value = obs });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_FOLIOPRECP", SqlDbType = SqlDbType.Int, Value = IDC_FOLIOPRECP });
            listparameters.Add(new SqlParameter() { ParameterName = "@PSIPASA", SqlDbType = SqlDbType.Int, Value = sipasa });
            listparameters.Add(new SqlParameter() { ParameterName = "@pentdir", SqlDbType = SqlDbType.Int, Value = entdir });
            listparameters.Add(new SqlParameter() { ParameterName = "@pbitocc", SqlDbType = SqlDbType.Int, Value = bitocc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pbitcro", SqlDbType = SqlDbType.Int, Value = bitcro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdsinexi", SqlDbType = SqlDbType.Int, Value = dsinexi });
            listparameters.Add(new SqlParameter() { ParameterName = "@PDCOSBAJO", SqlDbType = SqlDbType.Int, Value = DCOSBAJO });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfolio", SqlDbType = SqlDbType.Int, Value = folio });
            listparameters.Add(new SqlParameter() { ParameterName = "@PBITLLA", SqlDbType = SqlDbType.Int, Value = BITLLA });
            listparameters.Add(new SqlParameter() { ParameterName = "@PMENSAJE", SqlDbType = SqlDbType.Int, Value = MENSAJE });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTIPO", SqlDbType = SqlDbType.Int, Value = tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@PSIPASA2", SqlDbType = SqlDbType.Int, Value = SIPASA2 });
            listparameters.Add(new SqlParameter() { ParameterName = "@PFLETE", SqlDbType = SqlDbType.Int, Value = FLETE });
            listparameters.Add(new SqlParameter() { ParameterName = "@precoge", SqlDbType = SqlDbType.Int, Value = recoge });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_occli", SqlDbType = SqlDbType.Int, Value = idc_occli });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal_recoge", SqlDbType = SqlDbType.Int, Value = idc_sucursal_recoge });
            listparameters.Add(new SqlParameter() { ParameterName = "@Vtipop", SqlDbType = SqlDbType.Int, Value = Vtipop });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_banco", SqlDbType = SqlDbType.Int, Value = idc_banco });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_deposito", SqlDbType = SqlDbType.Int, Value = fecha_deposito });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmonto_pago", SqlDbType = SqlDbType.Int, Value = monto_pago });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones_pago", SqlDbType = SqlDbType.Int, Value = observaciones_pago });
            listparameters.Add(new SqlParameter() { ParameterName = "@Pconfirmar_pago", SqlDbType = SqlDbType.Int, Value = confirmar_pago });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_acti", SqlDbType = SqlDbType.Int, Value = cadena_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_acti", SqlDbType = SqlDbType.Int, Value = total_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal2_acti", SqlDbType = SqlDbType.Int, Value = total2_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena2_acti", SqlDbType = SqlDbType.Int, Value = cadena2_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdarti_NUEVA", SqlDbType = SqlDbType.Int, Value = vdarti2_nueva });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTOTART_NUEVO", SqlDbType = SqlDbType.Int, Value = VTOTA_NUEVO });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apreped_creditos4_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_apedidos5_esp_mod_web_nuevo(int idc_cliente, double monto, int idc_sucursal, bool desiva, int idc_iva, int idc_usuario, int idc_almacen, string direcip, string nombrepc, string usuariopc,
                char tipom, string cambios, string darti, int totart, System.DateTime fecha_ent, bool proye, int idpro, string occ, int idc_colonia, string calle,
                string numero, int cod_postal, string obs, int IDC_FOLIOPRECP, int sipasa, int entdir, bool bitocc, bool bitcro, string dsinexi, string DCOSBAJO,
                int folio, bool BITLLA, string MENSAJE, int SIPASA2, double FLETE, string recoge, int idc_general, int tipo, int idc_occli, int idc_sucursal_recoge,
                int Vtipop, int idc_banco, System.DateTime fecha_deposito, double monto_pago, string observaciones_pago, bool confirmar_pago, string cadena_acti, string total_acti, int total2_acti, string cadena2_acti,
                string vdarti2_nueva, int VTOTA_NUEVO)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmonto", SqlDbType = SqlDbType.Int, Value = monto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.Int, Value = idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdesiva", SqlDbType = SqlDbType.Int, Value = desiva });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_iva", SqlDbType = SqlDbType.Int, Value = idc_iva });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_almacen", SqlDbType = SqlDbType.Int, Value = idc_almacen });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.Int, Value = direcip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.Int, Value = nombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.Int, Value = usuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipom", SqlDbType = SqlDbType.Int, Value = tipom });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcambios", SqlDbType = SqlDbType.Int, Value = cambios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdarti", SqlDbType = SqlDbType.Int, Value = darti });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotart", SqlDbType = SqlDbType.Int, Value = totart });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_ent", SqlDbType = SqlDbType.Int, Value = fecha_ent });
            listparameters.Add(new SqlParameter() { ParameterName = "@pproye", SqlDbType = SqlDbType.Int, Value = proye });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidpro", SqlDbType = SqlDbType.Int, Value = idpro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pocc", SqlDbType = SqlDbType.Int, Value = occ });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_colonia", SqlDbType = SqlDbType.Int, Value = idc_colonia });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcalle", SqlDbType = SqlDbType.Int, Value = calle });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnumero", SqlDbType = SqlDbType.Int, Value = numero });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcod_postal", SqlDbType = SqlDbType.Int, Value = cod_postal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobs", SqlDbType = SqlDbType.Int, Value = obs });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_FOLIOPRECP", SqlDbType = SqlDbType.Int, Value = IDC_FOLIOPRECP });
            listparameters.Add(new SqlParameter() { ParameterName = "@PSIPASA", SqlDbType = SqlDbType.Int, Value = sipasa });
            listparameters.Add(new SqlParameter() { ParameterName = "@pentdir", SqlDbType = SqlDbType.Int, Value = entdir });
            listparameters.Add(new SqlParameter() { ParameterName = "@pbitocc", SqlDbType = SqlDbType.Int, Value = bitocc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pbitcro", SqlDbType = SqlDbType.Int, Value = bitcro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdsinexi", SqlDbType = SqlDbType.Int, Value = dsinexi });
            listparameters.Add(new SqlParameter() { ParameterName = "@PDCOSBAJO", SqlDbType = SqlDbType.Int, Value = DCOSBAJO });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfolio", SqlDbType = SqlDbType.Int, Value = folio });
            listparameters.Add(new SqlParameter() { ParameterName = "@PBITLLA", SqlDbType = SqlDbType.Int, Value = BITLLA });
            listparameters.Add(new SqlParameter() { ParameterName = "@PMENSAJE", SqlDbType = SqlDbType.Int, Value = MENSAJE });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTIPO", SqlDbType = SqlDbType.Int, Value = tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@PSIPASA2", SqlDbType = SqlDbType.Int, Value = SIPASA2 });
            listparameters.Add(new SqlParameter() { ParameterName = "@PFLETE", SqlDbType = SqlDbType.Int, Value = FLETE });
            listparameters.Add(new SqlParameter() { ParameterName = "@precoge", SqlDbType = SqlDbType.Int, Value = recoge });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_occli", SqlDbType = SqlDbType.Int, Value = idc_occli });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal_recoge", SqlDbType = SqlDbType.Int, Value = idc_sucursal_recoge });
            listparameters.Add(new SqlParameter() { ParameterName = "@Vtipop", SqlDbType = SqlDbType.Int, Value = Vtipop });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_banco", SqlDbType = SqlDbType.Int, Value = idc_banco });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_deposito", SqlDbType = SqlDbType.Int, Value = fecha_deposito });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmonto_pago", SqlDbType = SqlDbType.Int, Value = monto_pago });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones_pago", SqlDbType = SqlDbType.Int, Value = observaciones_pago });
            listparameters.Add(new SqlParameter() { ParameterName = "@Pconfirmar_pago", SqlDbType = SqlDbType.Int, Value = confirmar_pago });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_acti", SqlDbType = SqlDbType.Int, Value = cadena_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_acti", SqlDbType = SqlDbType.Int, Value = total_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal2_acti", SqlDbType = SqlDbType.Int, Value = total2_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena2_acti", SqlDbType = SqlDbType.Int, Value = cadena2_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdarti_NUEVA", SqlDbType = SqlDbType.Int, Value = vdarti2_nueva });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTOTART_NUEVO", SqlDbType = SqlDbType.Int, Value = VTOTA_NUEVO });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apreped_creditos4_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_apreped_creditos_especial_nc3_ESP_nuevo(int idc_cliente, double monto, int idc_sucursal, bool desiva, int idc_iva, int idc_usuario, int idc_almacen, string direcip, string nombrepc, string usuariopc,
            char tipom, string cambios, string darti, int totart, System.DateTime fecha_ent, bool proye, int idpro, string occ, int idc_colonia, string calle,
            string numero, int cod_postal, string obs, int IDC_FOLIOPRECP, int sipasa, int entdir, bool bitcro, bool bitocc, string dsinexi, string DCOSBAJO,
            int folio, bool BITLLA, string MENSAJE, char tipo, int SIPASA2, double FLETE, string recoge, int plazo, int tipopago, string otro,
            string canminima, string contacto, string telefono, string mail, int idc_occli, int idc_sucursal_recoge, string cadena_acti, string total_acti, int total2_acti, string cadena2_acti,
            string vdarti2_nueva, int VTOTA_NUEVO)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmonto", SqlDbType = SqlDbType.Int, Value = monto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.Int, Value = idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdesiva", SqlDbType = SqlDbType.Int, Value = desiva });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_iva", SqlDbType = SqlDbType.Int, Value = idc_iva });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_almacen", SqlDbType = SqlDbType.Int, Value = idc_almacen });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdirecip", SqlDbType = SqlDbType.Int, Value = direcip });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombrepc", SqlDbType = SqlDbType.Int, Value = nombrepc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pusuariopc", SqlDbType = SqlDbType.Int, Value = usuariopc });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipom", SqlDbType = SqlDbType.Int, Value = tipom });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcambios", SqlDbType = SqlDbType.Int, Value = cambios });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdarti", SqlDbType = SqlDbType.Int, Value = darti });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotart", SqlDbType = SqlDbType.Int, Value = totart });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfecha_ent", SqlDbType = SqlDbType.Int, Value = fecha_ent });
            listparameters.Add(new SqlParameter() { ParameterName = "@pproye", SqlDbType = SqlDbType.Int, Value = proye });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidpro", SqlDbType = SqlDbType.Int, Value = idpro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pocc", SqlDbType = SqlDbType.Int, Value = occ });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_colonia", SqlDbType = SqlDbType.Int, Value = idc_colonia });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcalle", SqlDbType = SqlDbType.Int, Value = calle });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnumero", SqlDbType = SqlDbType.Int, Value = numero });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcod_postal", SqlDbType = SqlDbType.Int, Value = cod_postal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobs", SqlDbType = SqlDbType.Int, Value = obs });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_FOLIOPRECP", SqlDbType = SqlDbType.Int, Value = IDC_FOLIOPRECP });
            listparameters.Add(new SqlParameter() { ParameterName = "@PSIPASA", SqlDbType = SqlDbType.Int, Value = sipasa });
            listparameters.Add(new SqlParameter() { ParameterName = "@pentdir", SqlDbType = SqlDbType.Int, Value = entdir });
            listparameters.Add(new SqlParameter() { ParameterName = "@pbitcro", SqlDbType = SqlDbType.Int, Value = bitcro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pbitocc", SqlDbType = SqlDbType.Int, Value = bitocc });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdsinexi", SqlDbType = SqlDbType.Int, Value = dsinexi });
            listparameters.Add(new SqlParameter() { ParameterName = "@PDCOSBAJO", SqlDbType = SqlDbType.Int, Value = DCOSBAJO });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfolio", SqlDbType = SqlDbType.Int, Value = folio });
            listparameters.Add(new SqlParameter() { ParameterName = "@PBITLLA", SqlDbType = SqlDbType.Int, Value = BITLLA });
            listparameters.Add(new SqlParameter() { ParameterName = "@PMENSAJE", SqlDbType = SqlDbType.Int, Value = MENSAJE });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTIPO", SqlDbType = SqlDbType.Int, Value = tipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@PSIPASA2", SqlDbType = SqlDbType.Int, Value = SIPASA2 });
            listparameters.Add(new SqlParameter() { ParameterName = "@PFLETE", SqlDbType = SqlDbType.Int, Value = FLETE });
            listparameters.Add(new SqlParameter() { ParameterName = "@precoge", SqlDbType = SqlDbType.Int, Value = recoge });
            listparameters.Add(new SqlParameter() { ParameterName = "@pplazo", SqlDbType = SqlDbType.Int, Value = plazo });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipopago", SqlDbType = SqlDbType.Int, Value = tipopago });
            listparameters.Add(new SqlParameter() { ParameterName = "@potro", SqlDbType = SqlDbType.Int, Value = otro });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcanminima", SqlDbType = SqlDbType.Int, Value = canminima });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcontacto", SqlDbType = SqlDbType.Int, Value = contacto });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptelefono", SqlDbType = SqlDbType.Int, Value = telefono });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmail", SqlDbType = SqlDbType.Int, Value = mail });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_occli", SqlDbType = SqlDbType.Int, Value = idc_occli });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal_recoge", SqlDbType = SqlDbType.Int, Value = idc_sucursal_recoge });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_acti", SqlDbType = SqlDbType.Int, Value = cadena_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal_acti", SqlDbType = SqlDbType.Int, Value = total_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotal2_acti", SqlDbType = SqlDbType.Int, Value = total2_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena2_acti", SqlDbType = SqlDbType.Int, Value = cadena2_acti });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdarti_NUEVA", SqlDbType = SqlDbType.Int, Value = vdarti2_nueva });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTOTART_NUEVO", SqlDbType = SqlDbType.Int, Value = VTOTA_NUEVO });

            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_apreped_creditos_especial_nc3_ESP_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SP_Cliente_Bloqueado(int idc_cliente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_Cliente_Bloqueado", listparameters, false);
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

        public DataSet sp_fn_maximo_programar_vendedor()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_fn_maximo_programar_vendedor", listparameters, false);
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

        public DataSet sp_datosticketsuc(int idc_sucursal)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.Int, Value = idc_sucursal });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datosticketsuc", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_sucursales_combo()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_sucursales_combo", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_bclientes_ventas(string valor)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pvalor", SqlDbType = SqlDbType.Int, Value = valor });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_bclientes_ventas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_correos_lista_precios_cliente(int idc_cliente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@idC_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_correos_lista_precios_cliente", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet sp_correo_contraseña(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_USUARIO", SqlDbType = SqlDbType.Int, Value = entidad.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@PTIPO", SqlDbType = SqlDbType.Int, Value = 4 });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_correo_contraseña", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet sp_exec_query_web(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PQUERY", SqlDbType = SqlDbType.Int, Value = entidad.Pcadenaarti });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_exec_query_web", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet SP_VER_PROYECTOS(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_VER_PROYECTOS", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet sp_selecciona_consignado_cliente(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_selecciona_consignado_cliente", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet SP_Saldo_Total_Cliente(int idc_cliente, double total)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmonto", SqlDbType = SqlDbType.Int, Value = total });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_Saldo_Total_Cliente", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet SP_Cambio_Precios(string cadena, int totalarticulos, int idc_cliente, int idc_sucursal, bool vcambios_lista)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pdarti", SqlDbType = SqlDbType.Int, Value = cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnum", SqlDbType = SqlDbType.Int, Value = totalarticulos });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_CLIENTE", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_SUCURSAL", SqlDbType = SqlDbType.Int, Value = idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcambios", SqlDbType = SqlDbType.Int, Value = vcambios_lista });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_Cambio_Precios", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet SP_Monto_Minimo_Venta(int idc_cliente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_Monto_Minimo_Venta", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet SP_Articulos_Entrega_Directa(string cadenapeso, int idc_cliente, double totalarticulos)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena", SqlDbType = SqlDbType.Int, Value = cadenapeso });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptot", SqlDbType = SqlDbType.Int, Value = totalarticulos });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_Articulos_Entrega_Directa", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet sp_occ_valida_cliente(int idc_occli)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena", SqlDbType = SqlDbType.Int, Value = idc_occli });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_occ_valida_cliente", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet SP_FechaMaxima()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_FechaMaxima", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet sp_fn_validar_entrega_colonia(int idc_colonia)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_colonia", SqlDbType = SqlDbType.Int, Value = idc_colonia });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_fn_validar_entrega_colonia", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet SP_Articulos_Cantidad_Permitida(string cadena, int idc_cliente, int tot, int idc_alamacen)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena", SqlDbType = SqlDbType.Int, Value = cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptot", SqlDbType = SqlDbType.Int, Value = tot });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_almacen", SqlDbType = SqlDbType.Int, Value = idc_alamacen });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_Articulos_Cantidad_Permitida", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet SP_VSp_Carga_VolmumenALIDAR_UVENCOM(string cadena, int tipo_camion, int total_articulos)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena", SqlDbType = SqlDbType.Int, Value = cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tipocam", SqlDbType = SqlDbType.Int, Value = tipo_camion });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptot", SqlDbType = SqlDbType.Int, Value = total_articulos });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("Sp_Carga_Volmumen", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet SP_VALIDAR_UVENCOM(string cadena, int total)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PARTIUV", SqlDbType = SqlDbType.Int, Value = cadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptota", SqlDbType = SqlDbType.Int, Value = total });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_VALIDAR_UVENCOM", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet sp_datos_colonia(int IDC_COLONIA)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@PIDC_COLONIA", SqlDbType = SqlDbType.Int, Value = IDC_COLONIA });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_datos_colonia", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet sp_consignado_df(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_consignado_df", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet sp_listap_cliente(int idc_clciente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_clciente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_listap_cliente", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet sp_fn_cliente_tipo_croquis(int idc_clciente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_clciente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_req_oc_croquis", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet sp_req_oc_croquis(int idc_clciente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_clciente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_req_oc_croquis", listparameters, false);
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

        public DataSet sp_fn_sucursal_mas_cercana(int idc_colonia)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_colonia", SqlDbType = SqlDbType.Int, Value = idc_colonia });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_fn_sucursal_mas_cercana", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_folio_preped_pedidos()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_folio_preped_pedidos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SP_Cargar_ChekPlus()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_Cargar_ChekPlus", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_cliente_confirmacion_pago(int idc_cliente)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cliente_confirmacion_pago", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_fn_cedis_sucursal(int idc_sucursal)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.Int, Value = idc_sucursal });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_fn_cedis_sucursal", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_cambiar_iva_frontera(int idc_sucursal, int idc_colonia)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", SqlDbType = SqlDbType.Int, Value = idc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_colonia", SqlDbType = SqlDbType.Int, Value = idc_colonia });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_cambiar_iva_frontera", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_promocion_articulo_cliente(int idc_articulo, int idc_cliente, int cantidad, int idc_lista)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", SqlDbType = SqlDbType.Int, Value = idc_articulo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcantidad", SqlDbType = SqlDbType.Int, Value = cantidad });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_listap", SqlDbType = SqlDbType.Int, Value = idc_lista });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_promocion_articulo_cliente", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_comprobar_chekplus_PRE(int folio, int idc_cliente, int monto)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_folioprecp", SqlDbType = SqlDbType.Int, Value = folio });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = idc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmonto", SqlDbType = SqlDbType.Int, Value = monto });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_comprobar_chekplus_PRE", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet SP_clientes_articulos_master(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("SP_clientes_articulos_master", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sp_clientes_clasif_idc_lista(AgentesENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_cliente });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_clientes_clasif_idc_lista", listparameters, false);
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

        public DataSet sp_reporte_agenda_visitas(int idc_agente, int dia)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_agente", SqlDbType = SqlDbType.Int, Value = idc_agente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdia", SqlDbType = SqlDbType.Int, Value = dia });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_reporte_agenda_visitas", listparameters, false);
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