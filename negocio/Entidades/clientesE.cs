namespace negocio.Entidades
{
    public class clientesE
    {
        private int idc_cliente;
        private string nombre;
        private string rfc;
        private string telefono;
        private string correo;
        private bool borrado;

        public int Idc_cliente
        {
            get { return idc_cliente; }
            set { idc_cliente = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Rfc
        {
            get { return rfc; }
            set { rfc = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        public bool Borrado
        {
            get { return borrado; }
            set { borrado = value; }
        }
    }
}