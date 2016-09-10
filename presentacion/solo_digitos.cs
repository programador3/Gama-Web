using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace presentacion
{
    public class solo_digitos
    {
        public static bool checar(string cadena)
        {
            bool resultado;
            try
            {
                int result = int.Parse(cadena);
                resultado = true;
            }
            catch
            {
                resultado = false;
            }

            return resultado;
        }

    }
}