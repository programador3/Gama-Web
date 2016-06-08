namespace negocio.Entidades
{
    public class ExamenesENT
    {
        private int pidc_examen;
        private int total_cadena;
        private string descripcion;
        private string cadena;
        private string tipo;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_usuario;

        public int Pidc_examen
        {
            get { return pidc_examen; }
            set { pidc_examen = value; }
        }

        public int Total_cadena
        {
            get { return total_cadena; }
            set { total_cadena = value; }
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

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

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
    }
}