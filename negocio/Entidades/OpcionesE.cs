namespace negocio.Entidades
{
    public class OpcionesE
    {
        private string menu1;
        private string menu2;
        private string menu3;
        private string menu4;
        private string menu5;
        private string menu6;
        private int nivel;
        private int usuario_id;
        private int tipo_apli;
        private string search;

        int idc_opcion;
        int idc_user;

        public int Idc_opcion
        {
            get { return idc_opcion; }
            set { idc_opcion = value; }
        }

        public int Idc_user
        {
            get { return idc_user; }
            set { idc_user = value; }
        }
        public string Search
        {
            get { return search; }
            set { search = value; }
        }

        public string Menu1
        {
            get { return menu1; }
            set { menu1 = value; }
        }

        public string Menu2
        {
            get { return menu2; }
            set { menu2 = value; }
        }

        public string Menu3
        {
            get { return menu3; }
            set { menu3 = value; }
        }

        public string Menu4
        {
            get { return menu4; }
            set { menu4 = value; }
        }

        public string Menu5
        {
            get { return menu5; }
            set { menu5 = value; }
        }

        public string Menu6
        {
            get { return menu6; }
            set { menu6 = value; }
        }

        public int Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }

        public int Usuario_id
        {
            get { return usuario_id; }
            set { usuario_id = value; }
        }

        public int Tipo_apli
        {
            get { return tipo_apli; }
            set { tipo_apli = value; }
        }
    }
}