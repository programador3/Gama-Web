namespace negocio.Entidades
{
    public class Etiquetas_EN
    {
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_usuario;
        private int orden;

        public int POrden
        {
            get { return orden; }
            set { orden = value; }
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

        private int idc_perfilgpoetiq;

        public int Idc_perfilgpoetiq
        {
            get { return idc_perfilgpoetiq; }
            set { idc_perfilgpoetiq = value; }
        }

        private int grupo;

        public int Grupo
        {
            get { return grupo; }
            set { grupo = value; }
        }

        private string etiqueta;

        public string Etiqueta
        {
            get { return etiqueta; }
            set { etiqueta = value; }
        }

        private int minimo;

        public int Minimo
        {
            get { return minimo; }
            set { minimo = value; }
        }

        private int maximo;

        public int Maximo
        {
            get { return maximo; }
            set { maximo = value; }
        }

        private bool tipo;

        public bool Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private int Usuario;

        public int usuario
        {
            get { return Usuario; }
            set { Usuario = value; }
        }

        private string cadena_opc;

        public string Cadena_opc
        {
            get { return cadena_opc; }
            set { cadena_opc = value; }
        }

        private int cadena_opc_total;

        public int Cadena_opc_total
        {
            get { return cadena_opc_total; }
            set { cadena_opc_total = value; }
        }

        private string cadena_bloq;

        public string Cadena_bloq
        {
            get { return cadena_bloq; }
            set { cadena_bloq = value; }
        }

        private int cadena_bloq_total;

        public int Cadena_bloq_total
        {
            get { return cadena_bloq_total; }
            set { cadena_bloq_total = value; }
        }
    }
}