namespace negocio.Entidades
{
    public class Nuevas_AprobacionesENT
    {
        private int idc_aprobacion;
        private int minimos_firmas;
        private int total_cadena;
        private string nombre;
        private string descripcion;
        private string cadena;
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

        public int Idc_aprobacion
        {
            get { return idc_aprobacion; }
            set { idc_aprobacion = value; }
        }

        public int Minimo_firmas
        {
            get { return minimos_firmas; }
            set { minimos_firmas = value; }
        }

        public int Total_Cadena
        {
            get { return total_cadena; }
            set { total_cadena = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string Cadena
        {
            get { return cadena; }
            set { cadena = value; }
        }
    }
}