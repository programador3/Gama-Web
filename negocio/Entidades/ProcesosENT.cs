using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class ProcesosENT
    {
        private bool borrador;
        private string tipo;
        private string descripcion;
        private int pidc_proceso;
        private string cadenasubpro;
        private string cadena;
        private string cadenaperf;
        private int totalcadenasub;
        private int totalcadena;
        private int totalcadenaperf;
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

        public int Pidc_proceso { get { return pidc_proceso; } set { pidc_proceso = value; } }
        public int Ptotalcadenaperf { get { return totalcadenaperf; } set { totalcadenaperf = value; } }
        public int Ptotalcadena { get { return totalcadena; } set { totalcadena = value; } }
        public int Ptotalcadenasub { get { return totalcadenasub; } set { totalcadenasub = value; } }
        public String Pcadenaperf { get { return cadenaperf; } set { cadenaperf = value; } }
        public String Pcadenasubpro { get { return cadenasubpro; } set { cadenasubpro = value; } }
        public String Pcadena { get { return cadena; } set { cadena = value; } }
        public String Ptioo { get { return tipo; } set { tipo = value; } }
        public String Pdescripcion { get { return descripcion; } set { descripcion = value; } }
        public Boolean Pborrador { get { return borrador; } set { borrador = value; } }
    }
}