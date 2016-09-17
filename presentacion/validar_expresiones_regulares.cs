using System.Text.RegularExpressions;

namespace presentacion
{
    public class validar_expresiones_regulares
    {
        public static Match comparar(string cadena, int tipo)
        {
            string patron, patron_rfc, patron_correo;
            patron_rfc = "[A-ZÑ&]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z0-9]?[A-Z0-9]?[0-9A-Z]?";
            patron_correo = "^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";

            Match match;

            if (tipo == 1) // rfc
            {
                patron = patron_rfc;
            }
            else
            {
                patron = patron_correo;
            }

            match = Regex.Match(cadena, patron);
            return match;
        }
    }
}