namespace negocio.Entidades
{
    public class Celulares_PreparacionENT
    {
        private int pidc_puesto;
        private int pidc_puestoprep;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_prepara;
        private string cadcel;
        private int totalcadcel;
        private string cadcel_acc;
        private int totalcadcel_acc;
        private int pidc_usuario;

        public int Pidc_usuario
        {
            get { return pidc_usuario; }
            set { pidc_usuario = value; }
        }

        public int Totalcadcel
        {
            get { return totalcadcel; }
            set { totalcadcel = value; }
        }

        public string Cadcel
        {
            get { return cadcel; }
            set { cadcel = value; }
        }

        public int Totalcadcel_acc
        {
            get { return totalcadcel_acc; }
            set { totalcadcel_acc = value; }
        }

        public string Cadcel_acc
        {
            get { return cadcel_acc; }
            set { cadcel_acc = value; }
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