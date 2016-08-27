﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class CombustibleENT
    {
        string valor;
        bool todos;
        int idc_sucursal;
        int tipofolio;
        int folio;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_usuario;

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }

        public string Pdirecip
        {
            get { return pdirecip; }
            set { pdirecip = value; }
        }

        public string Pnombrepc
        {
            get { return pnombrepc; }
            set { pnombrepc = value; }
        }

        public string Pusuariopc
        {
            get { return pusuariopc; }
            set { pusuariopc = value; }
        }
        public string Pvalor
        {
            get { return valor; }
            set { valor = value; }
        }
        public Boolean Ptodos
        {
            get { return todos; }
            set { todos = value; }
        }

        public int Pidc_sucursal
        {
            get { return idc_sucursal; }
            set { idc_sucursal = value; }
        }

        public int Ptipofolio
        {
            get { return tipofolio; }
            set { idc_sucursal = value; }
        }
        public int Pfolio
        {
            get { return folio; }
            set { folio = value; }
        }
    }
}