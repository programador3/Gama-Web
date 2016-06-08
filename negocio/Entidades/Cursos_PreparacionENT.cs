namespace negocio.Entidades
{
    public class Cursos_PreparacionENT
    {
        private int pidc_puesto;
        private int pidc_puestoprep;
        private int pidc_revisionser;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_prepara;
        private string cadser;
        private int totalcadser;
        private int pidc_usuario;

        public int Pidc_revisionser
        {
            get { return pidc_revisionser; }
            set { pidc_revisionser = value; }
        }

        public int Pidc_usuario
        {
            get { return pidc_usuario; }
            set { pidc_usuario = value; }
        }

        public int Totalcadser
        {
            get { return totalcadser; }
            set { totalcadser = value; }
        }

        public string Cadser
        {
            get { return cadser; }
            set { cadser = value; }
        }

        public int Idc_prepara
        {
            get { return idc_prepara; }
            set { idc_prepara = value; }
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

        public int Pidc_puesto
        {
            get { return pidc_puesto; }
            set { pidc_puesto = value; }
        }

        public int Pidc_puestoprep
        {
            get { return pidc_puestoprep; }
            set { pidc_puestoprep = value; }
        }
    }
}