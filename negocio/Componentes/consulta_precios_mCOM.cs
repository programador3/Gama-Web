using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class consulta_precios_mCOM
    {
        public DataSet agentes_vs_usuarios(Datos_Usuario_logENT dul)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", Value = dul.Pidc_usuario });
            try
            {
                ds = data.enviar("sp_combo_agentes_usu", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet clientes_por_agente(consulta_precios_mENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_agente", Value = ent.Pidc_agente });
            try {
                ds = data.enviar("sp_clientes_x_agente_visitas_individual", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds; 
        }
        
        public DataSet Carga_Lista_Master_Cot_nueva(consulta_precios_mENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", Value = ent.Pidc_cliente });
            try
            {
                ds = data.enviar("sp_articulos_master_cliente", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet buscar_precio_producto_nuevo(consulta_precios_mENT ent)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_articulo", Value = ent.Pidc_articulo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_cliente", Value = ent.Pidc_cliente });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", Value = ent.Pidc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcantidad", Value = ent.Pcantidad });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcambiolista", Value = ent.Pcambiolista });
                        
            try
            {
                ds = data.enviar("sp_precio_cliente_cedis_rangos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;

        }

        public DataSet buscar_productos(consulta_precios_mENT ent, Datos_Usuario_logENT dul)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            
            listparameters.Add(new SqlParameter() { ParameterName = "@pvalor", Value = ent.Pvalor });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo", Value = ent.Ptipo });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_sucursal", Value = ent.Pidc_sucursal });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", Value = dul.Pidc_usuario });

            try
            {
                ds = data.enviar("sp_buscar_articulo_VENTAS_existencias", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /*
        Public Function buscar_productos(ByVal codigo As Object, ByVal tipo As String, ByVal idc_sucursal As Integer, ByVal idc_usuario As Integer) As DataSet
        Dim parametros() As String = {"@pvalor", "@ptipo", "@pidc_sucursal", "@pidc_usuario"}
        Dim valores() As String = {codigo.ToString.Trim, tipo, idc_sucursal, idc_usuario}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_buscar_articulo_VENTAS_existencias", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing

        End Try
    End Function
        
        */
    }
}
