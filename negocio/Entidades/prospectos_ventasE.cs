namespace negocio.Entidades
{
    public class prospectos_ventasE
    {
        private int idc_prospecto;
        private string direccion;
        private string nombre_razon_social;
        private string contacto;
        private string telefono;
        private string tipo_obra;
        private string correo;
        private string tamaño_obra;
        private string etapa_obra;
        private string observacion;
        private int idc_usuario;
        private int totalobras;
        private string masobras;

        //new 20-04-2015
        private string cad_con;

        private int cad_con_tot;
        private string cad_tel;
        private int cad_tel_tot;
        private string cad_cor;
        private int cad_cor_tot;
        private string phost;

        //new 29-04-2015
        private decimal latitud;

        private decimal longitud;

        //new 01-10-2015
        private string cadena_famartdet;

        private int cadena_famartdet_total;

        //
        private string cadena_famartdet_marca;

        private int cadena_famartdet_marca_total;

        //
        private int idc_giroc;

        //
        private int idc_tipoobra;

        //
        private int idc_etapaobra;

        //
        private string fechai;

        private string fechaf;

        public int Idc_prospecto
        {
            get { return idc_prospecto; }
            set { idc_prospecto = value; }
        }

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public string Nombre_razon_social
        {
            get { return nombre_razon_social; }
            set { nombre_razon_social = value; }
        }

        public string Contacto
        {
            get { return contacto; }
            set { contacto = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Tipo_obra
        {
            get { return tipo_obra; }
            set { tipo_obra = value; }
        }

        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        public string Tamaño_obra
        {
            get { return tamaño_obra; }
            set { tamaño_obra = value; }
        }

        public string Etapa_obra
        {
            get { return etapa_obra; }
            set { etapa_obra = value; }
        }

        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }

        public int Totalobras
        {
            get { return totalobras; }
            set { totalobras = value; }
        }

        public string Masobras
        {
            get { return masobras; }
            set { masobras = value; }
        }

        //new 20-04-2015
        public string P_host
        {
            get { return phost; }
            set { phost = value; }
        }

        public string Cad_con
        {
            get { return cad_con; }
            set { cad_con = value; }
        }

        public int Cad_con_tot
        {
            get { return cad_con_tot; }
            set { cad_con_tot = value; }
        }

        public string Cad_tel
        {
            get { return cad_tel; }
            set { cad_tel = value; }
        }

        public int Cad_tel_tot
        {
            get { return cad_tel_tot; }
            set { cad_tel_tot = value; }
        }

        public string Cad_cor
        {
            get { return cad_cor; }
            set { cad_cor = value; }
        }

        public int Cad_cor_tot
        {
            get { return cad_cor_tot; }
            set { cad_cor_tot = value; }
        }

        public decimal Latitud
        {
            get { return latitud; }
            set { latitud = value; }
        }

        public decimal Longitud
        {
            get { return longitud; }
            set { longitud = value; }
        }

        // nuevo 01-10-2015

        public string Cadena_famartdet
        {
            get { return cadena_famartdet; }
            set { cadena_famartdet = value; }
        }

        public int Cadena_famartdet_total
        {
            get { return cadena_famartdet_total; }
            set { cadena_famartdet_total = value; }
        }

        //
        public string Cadena_famartdet_marca
        {
            get { return cadena_famartdet_marca; }
            set { cadena_famartdet_marca = value; }
        }

        public int Cadena_famartdet_marca_total
        {
            get { return cadena_famartdet_marca_total; }
            set { cadena_famartdet_marca_total = value; }
        }

        public int Idc_giroc
        {
            get { return idc_giroc; }
            set { idc_giroc = value; }
        }

        public int Idc_tipoobra
        {
            get { return idc_tipoobra; }
            set { idc_tipoobra = value; }
        }

        public int Idc_etapaobra
        {
            get { return idc_etapaobra; }
            set { idc_etapaobra = value; }
        }

        public string Fechai
        {
            get { return fechai; }
            set { fechai = value; }
        }

        public string Fechaf
        {
            get { return fechaf; }
            set { fechaf = value; }
        }
    }
}