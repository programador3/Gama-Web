namespace negocio.Entidades
{
    public class AdministradorPrebajasENT
    {
        private int pidc_prebaja;
        private int pidc_puesto_prebaja;
        private int año;
        private bool pconsulta;

        public bool Pconsutla
        {
            get { return pconsulta; }
            set { pconsulta = value; }
        }

        public int Pyear
        {
            get { return año; }
            set { año = value; }
        }

        public int Pidc_prebaja
        {
            get { return pidc_prebaja; }
            set { pidc_prebaja = value; }
        }

        public int Pidc_puesto_prebaja
        {
            get { return pidc_puesto_prebaja; }
            set { pidc_puesto_prebaja = value; }
        }
    }
}