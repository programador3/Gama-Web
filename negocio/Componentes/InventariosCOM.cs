﻿using datos;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace negocio.Componentes
{
    public class InventariosCOM
    {
        public DataSet CargarCat(InventariosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_activos_categorias", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarArea(InventariosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_activos_areas", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet CargarSubCat(InventariosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_actscategoria", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_actscategoria });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_combo_activos_subcat_esp", listparameters, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AgregarFormulario(InventariosENT entidad)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> listparameters = new List<SqlParameter>();
            Datos data = new Datos();
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_puesto", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_puesto });
            listparameters.Add(new SqlParameter() { ParameterName = "@pidc_actscategoria", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_actscategoria });
            listparameters.Add(new SqlParameter() { ParameterName = "@pfolio", SqlDbType = SqlDbType.Int, Value = entidad.Pfolio });
            listparameters.Add(new SqlParameter() { ParameterName = "@parea_comun", SqlDbType = SqlDbType.Int, Value = entidad.Parea });
            listparameters.Add(new SqlParameter() { ParameterName = "@pobservaciones", SqlDbType = SqlDbType.Int, Value = entidad.Pobservaciones });
            if (entidad.Parea == true)
            {
                listparameters.Add(new SqlParameter() { ParameterName = "@pidc_areaact", SqlDbType = SqlDbType.Int, Value = entidad.Pidc_areaact });
            }
            listparameters.Add(new SqlParameter() { ParameterName = "@pcadena", SqlDbType = SqlDbType.Int, Value = entidad.Pcadena });
            listparameters.Add(new SqlParameter() { ParameterName = "@pnumreg", SqlDbType = SqlDbType.Int, Value = entidad.Ptotal_cadena });
            try
            {
                //ds = data.datos_Clientes(listparameters);
                ds = data.enviar("sp_aactivos_nuevo", listparameters, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}