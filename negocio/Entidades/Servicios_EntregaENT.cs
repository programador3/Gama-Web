namespace negocio.Entidades
{
    public class Servicios_EntregaENT
    {
        private int pidc_puesto;
        private int pidc_empleado;
        private int pidc_puestoentrega;
        private int idc_entrega;
        private string cadser;
        private int totalcadser;
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

        public int Pidc_empleado
        {
            get { return pidc_empleado; }
            set { pidc_empleado = value; }
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

        public int Pidc_entrega
        {
            get { return idc_entrega; }
            set { idc_entrega = value; }
        }

        public int Pidc_puesto
        {
            get { return pidc_puesto; }
            set { pidc_puesto = value; }
        }

        public int Pidc_puestoentrega
        {
            get { return pidc_puestoentrega; }
            set { pidc_puestoentrega = value; }
        }
    }
}