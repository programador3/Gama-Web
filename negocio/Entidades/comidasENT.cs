using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class comidasENT
    {
        private DateTime pfecha;
        private string pcadena_empleados;
        private int ptotal_cadena;

        public DateTime Pfecha { get { return pfecha; } set { pfecha = value; } }
        public string Pcadena_empleados { get { return pcadena_empleados; } set { pcadena_empleados = value; } }
        public int Ptotal_cadena { get { return ptotal_cadena; } set { ptotal_cadena = value; } }
    }
}
