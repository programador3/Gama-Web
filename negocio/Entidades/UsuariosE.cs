namespace negocio.Entidades
{
    public class UsuariosE
    {
        private string usuario;
        private string contraseña;
        private int idc_usuario;

        //se usa en organigrama
        private string cod_archivo;

        public string Cod_arch
        {
            get { return cod_archivo; }
            set { cod_archivo = value; }
        }

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public string Contraseña
        {
            get { return contraseña; }
            set { contraseña = value; }
        }
    }
}