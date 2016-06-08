namespace negocio.Entidades
{
    public class Aprobaciones_solicitudE
    {
        private int idc_aprobacion_soli;
        private int idc_aprobacion;
        private int idc_usuario;
        private int idc_registro;
        private string estatus;
        private string comentarios;

        //add 22-01-2016
        private string pdirecip;

        private string pnombrepc;
        private string pusuariopc;

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

        public int Idc_aprobacion_soli
        {
            get { return idc_aprobacion_soli; }
            set { idc_aprobacion_soli = value; }
        }

        public int Idc_aprobacion
        {
            get { return idc_aprobacion; }
            set { idc_aprobacion = value; }
        }

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }

        public int Idc_registro
        {
            get { return idc_registro; }
            set { idc_registro = value; }
        }

        public string Estatus
        {
            get { return estatus; }
            set { estatus = value; }
        }

        public string Comentarios
        {
            get { return comentarios; }
            set { comentarios = value; }
        }
    }
}