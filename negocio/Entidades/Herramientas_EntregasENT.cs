namespace negocio.Entidades
{
    public class Herramientas_EntregasENT
    {
        private int Pidc_entrega;
        private int pidc_puesto;
        private int pidc_puestoent;
        private int pidc_empleado;
        private int numcadherr;
        private string cadherr;
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

        public string Cadherr
        {
            get { return cadherr; }
            set { cadherr = value; }
        }

        public int Numcadherr
        {
            get { return numcadherr; }
            set { numcadherr = value; }
        }

        public int Pidc_Entrega
        {
            get { return Pidc_entrega; }
            set { Pidc_entrega = value; }
        }

        public int Pidc_puesto
        {
            get { return pidc_puesto; }
            set { pidc_puesto = value; }
        }

        public int Pidc_puestoent
        {
            get { return pidc_puestoent; }
            set { pidc_puestoent = value; }
        }
    }
}