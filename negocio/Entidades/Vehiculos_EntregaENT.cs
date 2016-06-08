namespace negocio.Entidades
{
    public class Vehiculos_EntregaENT
    {
        private int pidc_puesto;
        private int pidc_empleado;
        private int pidc_entrega;
        private int pidc_puestosentrega;
        private string cadveh;
        private int totalcadveh;
        private string cadveh_herr;
        private int totalcadveh_herr;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;

        private int pidc_usuario;

        public int Pidc_usuario
        {
            get { return pidc_usuario; }
            set { pidc_usuario = value; }
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

        public int Pidc_puestoentrega
        {
            get { return pidc_puestosentrega; }
            set { pidc_puestosentrega = value; }
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

        public int Pidc_entrega
        {
            get { return pidc_entrega; }
            set { pidc_entrega = value; }
        }

        public int Pidc_empleado
        {
            get { return pidc_empleado; }
            set { pidc_empleado = value; }
        }

        public int Pidc_puesto
        {
            get { return pidc_puesto; }
            set { pidc_puesto = value; }
        }
    }
}