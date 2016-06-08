namespace negocio.Entidades
{
    public class Administrador_PreparacionesENT
    {
        private int pidc_prepara;
        private int pidc_puesto;
        private int año;

        public int Pyear
        {
            get { return año; }
            set { año = value; }
        }

        public int Pidc_prepara
        {
            get { return pidc_prepara; }
            set { pidc_prepara = value; }
        }

        public int Pidc_puesto
        {
            get { return pidc_puesto; }
            set { pidc_puesto = value; }
        }
    }
}