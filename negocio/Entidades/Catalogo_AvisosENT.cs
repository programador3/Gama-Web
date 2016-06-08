namespace negocio.Entidades
{
    public class Catalogo_AvisosENT
    {
        private int pidc_taviso;
        private int num_cad;
        private string cadaviso;
        private string descripcion;
        private bool eliminar;
        private bool editar;

        public bool Editar
        {
            get { return editar; }
            set { editar = value; }
        }

        public bool Eliminar
        {
            get { return eliminar; }
            set { eliminar = value; }
        }

        public int Pidc_taviso
        {
            get { return pidc_taviso; }
            set { pidc_taviso = value; }
        }

        public int NumCad
        {
            get { return num_cad; }
            set { num_cad = value; }
        }

        public string Cadaviso
        {
            get { return cadaviso; }
            set { cadaviso = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
    }
}