namespace negocio.Entidades
{
    public class Herramientas_PreparacionENT
    {
        private int pidc_puesto;
        private int pidc_puestoprep;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_prepara;
        private string cadherr;
        private int totalcadherr;
        private int pidc_usuario;

        public int Pidc_usuario
        {
            get { return pidc_usuario; }
            set { pidc_usuario = value; }
        }

        public int Totalcadherr
        {
            get { return totalcadherr; }
            set { totalcadherr = value; }
        }

        public string Cadherr
        {
            get { return cadherr; }
            set { cadherr = value; }
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