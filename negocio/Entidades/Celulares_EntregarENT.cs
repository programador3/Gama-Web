namespace negocio.Entidades
{
    public class Celulares_EntregarENT
    {
        private int pidc_puesto;
        private int pidc_empleado;
        private int pidc_entrega;
        private int pidc_puestosentrega;
        private string cadcel;
        private int totalcadcel;
        private string cadcel_acc;
        private int totalcadcel_acc;
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

        public int Pidc_empleado
        {
            get { return pidc_empleado; }
            set { pidc_empleado = value; }
        }

        public int Pidc_puestoentrega
        {
            get { return pidc_puestosentrega; }
            set { pidc_puestosentrega = value; }
        }
    }
}