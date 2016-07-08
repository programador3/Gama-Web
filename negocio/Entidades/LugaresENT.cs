namespace negocio.Entidades
{
    public class LugaresENT
    {
        private string nombre;
        private string alias;
        private int idc_puesto;
        private int idc_area;
        private int idc_lugar;
        private int idc_sucursal;
        private string cadena;
        private int total_cadena;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_usuario;

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
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
            get { return idc_puesto; }
            set { idc_puesto = value; }
        }

        public int Pidc_area
        {
            get { return idc_area; }
            set { idc_area = value; }
        }

        public int pidc_sucursal
        {
            get { return idc_sucursal; }
            set { idc_sucursal = value; }
        }

        public int Pidc_lugar
        {
            get { return idc_lugar; }
            set { idc_lugar = value; }
        }

        public int Ptotalcadea
        {
            get { return total_cadena; }
            set { total_cadena = value; }
        }

        public string Pcadena
        {
            get { return cadena; }
            set { cadena = value; }
        }

        public string Pnombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Palias
        {
            get { return alias; }
            set { alias = value; }
        }
    }
}