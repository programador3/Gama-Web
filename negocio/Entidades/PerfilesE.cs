namespace negocio.Entidades
{
    public class PerfilesE
    {
        private int idc_perfil;
        private string nombre;
        private string cadena_gpo_lib;
        private int cad_total_gpo_lib;

        //11-09-2015 borrador
        private int idc_puestoperfil_borr;

        private bool borrador;
        private string pcadena_archi;
        private int ptotal_cadena_archi;
        private string cadena_gpo_opc;

        private int cad_total_gpo_opc;
        private string tipo;

        //
        private string cadena_etiq_lib;

        private int cad_total_etiq_lib;
        private string cadena_etiq_opc;
        private int cad_total_etiq_opc;
        private int usuario;
        private string cadena_perfil_docs; //add 02-12-2015
        private int cad_perfil_docs_tot; //add 02-12-2015
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_usuario;
        private string cadena_examenes;
        private int total_cadena_examenes;

        public int Ptotal_examenes
        {
            get { return total_cadena_examenes; }
            set { total_cadena_examenes = value; }
        }

        public string Pcadena_examenes
        {
            get { return cadena_examenes; }
            set { cadena_examenes = value; }
        }

        public int Ptotal_cadena_archi
        {
            get { return ptotal_cadena_archi; }
            set { ptotal_cadena_archi = value; }
        }

        public string Pcadena_archi
        {
            get { return pcadena_archi; }
            set { pcadena_archi = value; }
        }

        public string Ptipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

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

        public int Idc_perfil
        {
            get { return idc_perfil; }
            set { idc_perfil = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Cadena_gpo_lib
        {
            get { return cadena_gpo_lib; }
            set { cadena_gpo_lib = value; }
        }

        public int Cad_total_gpo_lib
        {
            get { return cad_total_gpo_lib; }
            set { cad_total_gpo_lib = value; }
        }

        //
        public string Cadena_gpo_opc
        {
            get { return cadena_gpo_opc; }
            set { cadena_gpo_opc = value; }
        }

        public int Cad_total_gpo_opc
        {
            get { return cad_total_gpo_opc; }
            set { cad_total_gpo_opc = value; }
        }

        //
        //3er cadena
        public string Cadena_etiq_lib
        {
            get { return cadena_etiq_lib; }
            set { cadena_etiq_lib = value; }
        }

        public int Cad_total_etiq_lib
        {
            get { return cad_total_etiq_lib; }
            set { cad_total_etiq_lib = value; }
        }

        //4ta cadena
        public string Cadena_etiq_opc
        {
            get { return cadena_etiq_opc; }
            set { cadena_etiq_opc = value; }
        }

        public int Cad_total_etiq_opc
        {
            get { return cad_total_etiq_opc; }
            set { cad_total_etiq_opc = value; }
        }

        public int Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        //11-09-2015 borrador
        public int Idc_puestoperfil_borr
        {
            get { return idc_puestoperfil_borr; }
            set { idc_puestoperfil_borr = value; }
        }

        public bool Borrador
        {
            get { return borrador; }
            set { borrador = value; }
        }

        public string Cadena_perfil_docs
        {
            get { return cadena_perfil_docs; }
            set { cadena_perfil_docs = value; }
        }

        public int Cad_perfil_docs_tot
        {
            get { return cad_perfil_docs_tot; }
            set { cad_perfil_docs_tot = value; }
        }
    }
}