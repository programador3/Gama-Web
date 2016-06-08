using System.Web.UI;

namespace presentacion
{
    public static class msgbox
    {
        public static void show(string mensaje, Page mypage)
        {
            mensaje = mensaje.Replace("'", "");
            char cr = (char)13;
            char lf = (char)10;
            char tab = (char)9;
            mensaje = mensaje.Replace(cr.ToString(), "");
            mensaje = mensaje.Replace(lf.ToString(), "");
            mensaje = mensaje.Replace(tab.ToString(), "");

            //mensaje = mensaje.Replace("'","\'");
            Alert.ShowAlertInfo(mensaje, "Mensaje del sistema", mypage);
        }
    }
}