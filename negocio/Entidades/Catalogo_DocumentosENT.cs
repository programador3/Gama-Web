namespace negocio.Entidades
{
    public class Catalogo_DocumentosENT
    {
        private int pidc_tipodoc;
        private string caddocs;
        private int numcad;
        private string descripcion;

        public int Pidc_tipodoc
        {
            get { return pidc_tipodoc; }
            set { pidc_tipodoc = value; }
        }

        public int Numcad
        {
            get { return numcad; }
            set { numcad = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string Caddocs
        {
            get { return caddocs; }
            set { caddocs = value; }
        }
    }
}