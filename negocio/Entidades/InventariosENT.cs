namespace negocio.Entidades
{
    public class InventariosENT
    {
        private int idc_actscategoria;
        private int idc_almacen;

        private int idc_articulo;
        private int idc_modulo;

        private int idc_puesto;
        private int folio;
        private decimal folio2;
        private bool area;
        private string observaciones;
        private int idc_areaact;
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

        public int Pidc_actscategoria
        {
            get { return idc_actscategoria; }
            set { idc_actscategoria = value; }
        }

        public int Pidc_puesto
        {
            get { return idc_puesto; }
            set { idc_puesto = value; }
        }

        public int Pfolio
        {
            get { return folio; }
            set { folio = value; }
        }

        public decimal Pfolio2
        {
            get { return folio2; }
            set { folio2 = value; }
        }

        public int Pidc_areaact
        {
            get { return idc_areaact; }
            set { idc_areaact = value; }
        }

        public int Ptotal_cadena
        {
            get { return total_cadena; }
            set { total_cadena = value; }
        }

        public int Pidc_almacen
        {
            get { return idc_almacen; }
            set { idc_almacen = value; }
        }

        public int Pidc_modulo
        {
            get { return idc_modulo; }
            set { idc_modulo = value; }
        }

        public int Pidc_articulo
        {
            get { return idc_articulo; }
            set { idc_articulo = value; }
        }

        public string Pcadena
        {
            get { return cadena; }
            set { cadena = value; }
        }

        public string Pobservaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public bool Parea
        {
            get { return area; }
            set { area = value; }
        }
    }
}