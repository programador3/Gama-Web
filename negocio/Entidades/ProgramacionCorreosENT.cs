using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class ProgramacionCorreosENT
    {
        private int idc_progracorreo;
        private string OBSERVACIONES;
        private string tipo;
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

        public int Pidc_progracorreo
        {
            get { return idc_progracorreo; }
            set { idc_progracorreo = value; }
        }

        public string Pobservaciones
        {
            get { return OBSERVACIONES; }
            set { OBSERVACIONES = value; }
        }

        public string Ptipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
    }
}