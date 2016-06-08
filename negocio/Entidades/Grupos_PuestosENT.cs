namespace negocio.Entidades
{
    public class Grupos_PuestosENT
    {
        private int pidc_puesto_gpo;
        private int num_cadpuestos;
        private string cadpuestos;
        private string descripion;
        private string filtro;
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

        public int Idc_puesto_gpo
        {
            get { return pidc_puesto_gpo; }
            set { pidc_puesto_gpo = value; }
        }

        public int Num_cadena
        {
            get { return num_cadpuestos; }
            set { num_cadpuestos = value; }
        }

        public string Descripcion
        {
            get { return descripion; }
            set { descripion = value; }
        }

        public string Filtro
        {
            get { return filtro; }
            set { filtro = value; }
        }

        public string CadenaPuestos
        {
            get { return cadpuestos; }
            set { cadpuestos = value; }
        }
    }
}