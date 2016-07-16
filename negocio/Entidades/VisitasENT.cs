using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class VisitasENT
    {
        private DateTime fi;
        private DateTime fs;
        private string nombre;
        private string nombreempresa;
        private string motivo;
        private int idc_visitap;
        private int idc_visitareg;
        private int idc_visitaemp;
        private int idc_empleado;
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

        public String Pnombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public String Pnombreempresa
        {
            get { return nombreempresa; }
            set { nombreempresa = value; }
        }

        public String Pmotivo
        {
            get { return motivo; }
            set { motivo = value; }
        }

        public int Pidc_visitap
        {
            get { return idc_visitap; }
            set { idc_visitap = value; }
        }

        public int Pidc_visitaemp
        {
            get { return idc_visitaemp; }
            set { idc_visitaemp = value; }
        }

        public int Pidc_empleado
        {
            get { return idc_empleado; }
            set { idc_empleado = value; }
        }

        public int Pidc_visitareg
        {
            get { return idc_visitareg; }
            set { idc_visitareg = value; }
        }

        public DateTime pfi { get { return fi; } set { fi = value; } }
        public DateTime pf2 { get { return fs; } set { fs = value; } }
    }
}