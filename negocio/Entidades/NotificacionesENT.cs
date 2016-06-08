namespace negocio.Entidades
{
    public class NotificacionesENT
    {
        private int idc_puesto;
        private int idc_usuario;
        private string cadena;
        private string mensaje;
        private string asunto;

        public int Idc_puesto
        {
            get { return idc_puesto; }
            set { idc_puesto = value; }
        }

        public string pasunto
        {
            get { return asunto; }
            set { asunto = value; }
        }

        public string ptexto
        {
            get { return mensaje; }
            set { mensaje = value; }
        }

        public string ppara
        {
            get { return cadena; }
            set { cadena = value; }
        }

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }
    }
}