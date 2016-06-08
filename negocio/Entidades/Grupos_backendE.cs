namespace negocio.Entidades
{
    public class Grupos_backendE
    {
        private int idc_perfilgpo;
        private bool libre;
        private int minimo_libre;
        private int maximo_libre;
        private bool opciones;
        private int minimo_opc;
        private int maximo_opc;
        private int cadena_total;
        private string cadena;
        private string grupo;
        private int usuario;
        private int orden;
        private bool externo;

        public int Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public string Grupo
        {
            get { return grupo; }
            set { grupo = value; }
        }

        public int Idc_perfilgpo
        {
            get { return idc_perfilgpo; }
            set { idc_perfilgpo = value; }
        }

        public bool Libre
        {
            get { return libre; }
            set { libre = value; }
        }

        public int Minimo_libre
        {
            get { return minimo_libre; }
            set { minimo_libre = value; }
        }

        public int Maximo_libre
        {
            get { return maximo_libre; }
            set { maximo_libre = value; }
        }

        public bool Opciones
        {
            get { return opciones; }
            set { opciones = value; }
        }

        public int Minimo_opc
        {
            get { return minimo_opc; }
            set { minimo_opc = value; }
        }

        public int Maximo_opc
        {
            get { return maximo_opc; }
            set { maximo_opc = value; }
        }

        public int Cadena_Total
        {
            get { return cadena_total; }
            set { cadena_total = value; }
        }

        public string Cadena
        {
            get { return cadena; }
            set { cadena = value; }
        }

        public int Orden
        {
            get { return orden; }
            set { orden = value; }
        }

        public bool Externo
        {
            get { return externo; }
            set { externo = value; }
        }
    }
}