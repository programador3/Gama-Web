namespace negocio.Entidades
{
    public class AvisosENT
    {
        private int pidc_puesto;
        private int pidc_avisoweb;
        string asunto;
        string texto;
        string para;
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
        public string Pasunto
        {
            get { return asunto; }
            set { asunto = value; }
        }
        public string Ppara
        {
            get { return para; }
            set { para = value; }
        }
        public string Ptexto
        {
            get { return texto; }
            set { texto = value; }
        }
        public string Pusuariopc
        {
            get { return pusuariopc; }
            set { pusuariopc = value; }
        }
        public int Pidc_avisoweb
        {
            get { return pidc_avisoweb; }
            set { pidc_avisoweb = value; }
        }

        public int Pidc_puesto
        {
            get { return pidc_puesto; }
            set { pidc_puesto = value; }
        }
    }
}