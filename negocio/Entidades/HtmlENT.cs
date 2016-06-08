namespace negocio.Entidades
{
    public class HtmlENT
    {
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_usuario;
        private string content;
        private int idc_html;
        private string titulo;

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }

        public int Pidc_html
        {
            get { return idc_html; }
            set { idc_html = value; }
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
    }
}