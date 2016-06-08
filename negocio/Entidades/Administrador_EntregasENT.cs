namespace negocio.Entidades
{
    public class Administrador_EntregasENT
    {
        private int pidc_entrega;
        private int pidc_puesto;
        private int año;

        public int Pyear
        {
            get { return año; }
            set { año = value; }
        }

        public int Pidc_entrega
        {
            get { return pidc_entrega; }
            set { pidc_entrega = value; }
        }

        public int Pidc_puesto
        {
            get { return pidc_puesto; }
            set { pidc_puesto = value; }
        }
    }
}