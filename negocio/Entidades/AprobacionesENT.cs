namespace negocio.Entidades
{
    public class AprobacionesENT
    {
        private int idc_aprobacion;
        private int pborrado;

        //mic 03-10-2015
        //private int idc_puestoperfil;
        private string pdirecip;

        private string pnombrepc;
        private string pusuariopc; private int idc_puesto;

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }

        private int idc_usuario;

        public int Pidc_puesto
        {
            get { return idc_puesto; }
            set { idc_puesto = value; }
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

        public int Idc_aprobacion
        {
            get { return idc_aprobacion; }
            set { idc_aprobacion = value; }
        }

        public int Borrado
        {
            get { return pborrado; }
            set { pborrado = value; }
        }

        //mic 03-10-2015

        //public int Idc_puestoperfil
        //{
        //    get { return idc_puestoperfil; }
        //    set { idc_puestoperfil = value; }
        //}
    }
}