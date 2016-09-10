using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace presentacion
{
    public class substraer
    {
        public static string derecha(string cadena,int total)
        {
            string resultado = "";
            int vlen;
            vlen = cadena.Trim().Length;

            if (vlen >0)
            {resultado = cadena.Substring(vlen - total, total);}
            
            return resultado;
        }
    }
}