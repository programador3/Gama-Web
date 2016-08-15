namespace negocio.Entidades
{
    public class PuestosServiciosENT
    {
        private int idc_puesto;
        private int totalcadena;
        private string cadena;
        private int status;
        private bool TODOS;
        private bool volteado;
        private int idc_puestoperfil;
        private int pidc_prepara;
        private int pidc_pre_empleado;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_usuario;
        private decimal retiro;
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

        public int Pidc_pre_empleado
        {
            get { return pidc_pre_empleado; }
            set { pidc_pre_empleado = value; }
        }

        public int Pidc_prepara
        {
            get { return pidc_prepara; }
            set { pidc_prepara = value; }
        }

        public decimal Pretiro
        {
            get { return retiro; }
            set { retiro = value; }
        }

        public bool Ptodos
        {
            get { return TODOS; }
            set { TODOS = value; }
        }

        public bool PReves
        {
            get { return volteado; }
            set { volteado = value; }
        }

        public int Ptotal_cadena
        {
            get { return totalcadena; }
            set { totalcadena = value; }
        }

        public string Pcadena
        {
            get { return cadena; }
            set { cadena = value; }
        }

        public int Pstatus
        {
            get { return status; }
            set { status = value; }
        }

        public int Idc_Puesto
        {
            get { return idc_puesto; }
            set { idc_puesto = value; }
        }

        public int Idc_puestoperfil
        {
            get { return idc_puestoperfil; }
            set { idc_puestoperfil = value; }
        }
    }
}