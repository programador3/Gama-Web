using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using datos;

namespace negocio.Componentes
{
    public class Prospectos_ventasBL
    {
        #region Methods
        public DataSet alta_prospectos_ventas(Entidades.prospectos_ventasE prospectos_ventas)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prospecto", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Idc_prospecto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pdireccion", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Direccion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnombre_razon_social", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Nombre_razon_social });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcontacto", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Contacto });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptelefono", SqlDbType = SqlDbType.Char, Value = prospectos_ventas.Telefono });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptipo_obra", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Tipo_obra });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcorreo", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Correo });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptamaño_obra", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Tamaño_obra });
            listparameters.Add(new SqlParameter() { ParameterName = "@petapa_obra", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Etapa_obra});
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservacion", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Observacion });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@ptotalobras", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Totalobras });
            listparameters.Add(new SqlParameter() { ParameterName = "@pmasobras", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Masobras });
            //new 20-04-2015
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadcontot", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Cad_con_tot });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadcon", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Cad_con });

            listparameters.Add(new SqlParameter() { ParameterName = "@pcadteltot", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Cad_tel_tot });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadtel", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Cad_tel });

             listparameters.Add(new SqlParameter() { ParameterName = "@pcadcortot", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Cad_cor_tot });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadcor", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Cad_cor });
            //new 29-04-2014
            if (prospectos_ventas.Latitud != 0 && prospectos_ventas.Longitud != 0) {
                listparameters.Add(new SqlParameter() { ParameterName = "@platitud", SqlDbType = SqlDbType.Real, Value = prospectos_ventas.Latitud });
                listparameters.Add(new SqlParameter() { ParameterName = "@plongitud", SqlDbType = SqlDbType.Real, Value = prospectos_ventas.Longitud });
            }

            //new 01-10-2015
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_famartdet", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Cadena_famartdet });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_famartdet_tot", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Cadena_famartdet_total });
            //
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_famartdetmarca", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.Cadena_famartdet_marca });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena_famartdetmarca_tot", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Cadena_famartdet_marca_total });
            //
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_giroc", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Idc_giroc });
            //
            if (prospectos_ventas.Idc_tipoobra > 0) {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_tipoobra", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Idc_tipoobra });
            }
            //
            if(prospectos_ventas.Idc_etapaobra>0){
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_etapaobra", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Idc_etapaobra });
            }
            
            //
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aprospectos_ventas", listparameters, true);
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;

        }

        public DataSet datos_prospectos_ventas(Entidades.prospectos_ventasE prospectos_ventas)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
    
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prospecto", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Idc_prospecto });
            listparameters.Add(new SqlParameter() { ParameterName = "@phost", SqlDbType = SqlDbType.VarChar, Value = prospectos_ventas.P_host });
            listparameters.Add(new SqlParameter() { ParameterName = "@pcatalogoweb", SqlDbType = SqlDbType.Bit, Value =1 });
            
            if(prospectos_ventas.Fechai !=""){
                listparameters.Add(new SqlParameter() { ParameterName = "@pfechai", SqlDbType = SqlDbType.Date, Value = prospectos_ventas.Fechai });
            }
            if (prospectos_ventas.Fechaf != "") {
                listparameters.Add(new SqlParameter() { ParameterName = "@pfechaf", SqlDbType = SqlDbType.Date, Value = prospectos_ventas.Fechaf });
            }
            
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_prospectos_ventas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;

        }

        //funcion que recupera el dataset con el datalle de las obras de n prospecto
        public DataSet datos_prospectos_ventas_mas_obras(Entidades.prospectos_ventasE prospectos_ventas)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prospecto", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Idc_prospecto });


            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_prospectos_ventas_mas_obras", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;

        }

        //funcion que recupera el dataset con el datalle de las obras de n prospecto
        public DataSet familiaArticulos_cbox()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prospecto", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Idc_prospecto });


            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_prospectos_ventas_fam_art", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;

        }

        //funcion que recupera el dataset con el datalle de tipos_obras
        public DataSet tipos_obras_cbox()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prospecto", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Idc_prospecto });


            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_tipos_obras", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;

        }

        //funcion que recupera el dataset con el datalle de tipos_obras
        public DataSet etapas_obras_cbox()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            //listparameters.Add(new SqlParameter() { ParameterName = "@pidc_prospecto", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Idc_prospecto });


            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_etapa_obras", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;

        }

        /// <summary>
        /// add 13-11-2015
        /// </summary>
        /// <param name="prospectos_ventas"></param>
        /// <returns></returns>
        public DataSet reporte_comisiones_prospectos(Entidades.prospectos_ventasE prospectos_ventas)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();

            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_usuario", SqlDbType = SqlDbType.Int, Value = prospectos_ventas.Idc_usuario });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfechai", SqlDbType = SqlDbType.Date, Value = prospectos_ventas.Fechai });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfechaf", SqlDbType = SqlDbType.Date, Value = prospectos_ventas.Fechaf });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_reporte_comisiones_prospectos", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
        #endregion
}
