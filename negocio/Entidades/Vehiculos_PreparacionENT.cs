namespace negocio.Entidades
{
    public class Vehiculos_PreparacionENT
    {
        private int pidc_puesto;
        private int pidc_puestoprep;
        private string pmotivo;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_prepara;
        private string cadveh;
        private int totalcadveh;
        private string cadveh_herr;
        private int totalcadveh_herr;
        private int pidc_usuario;

        public int Pidc_usuario
        {
            get { return pidc_usuario; }
            set { pidc_usuario = value; }
        }

        public int Totalcadveh
        {
            get { return totalcadveh; }
            set { totalcadveh = value; }
        }

        public string Cadveh
        {
            get { return cadveh; }
            set { cadveh = value; }
        }

        public string Pmotivo
        {
            get { return pmotivo; }
            set { pmotivo = value; }
        }

        public int Totalcadveh_herr
        {
            get { return totalcadveh_herr; }
            set { totalcadveh_herr = value; }
        }

        public string Cadveh_herr
        {
            get { return cadveh_herr; }
            set { cadveh_herr = value; }
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