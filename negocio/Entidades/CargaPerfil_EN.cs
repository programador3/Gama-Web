namespace negocio.Entidades
{
    public class CargaPerfil_EN
    {
        private int idc_puestoperfil;
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public int Idc_puesto
        {
            get { return idc_puestoperfil; }
            set { idc_puestoperfil = value; }
        }
    }
}