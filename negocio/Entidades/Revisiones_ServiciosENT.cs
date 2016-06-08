namespace negocio.Entidades
{
    public class Revisiones_ServiciosENT
    {
        private int pidc_revisionser;
        private string filtro;
        private int pidc_puesto_revisa;
        private int pidc_puesto_prepara;
        private int pidc_puesto_entrega;
        private int numcadgrupos;
        private int numcadpuesto;
        private int pidc_sucursal;
        private string tipo_aplica;
        private string tipo_rev;
        private string cad_puestos;
        private string cad_gpos;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_usuario;
        private string servicio;
        private bool serviciob;
        private bool final;
        private bool genera_vale;
        private string pdescripcion;

        public string Descripcion
        {
            get { return pdescripcion; }
            set { pdescripcion = value; }
        }

        public bool Final
        {
            get { return final; }
            set { final = value; }
        }

        public bool Genera_Vales
        {
            get { return genera_vale; }
            set { genera_vale = value; }
        }

        public string Servici
        {
            get { return servicio; }
            set { servicio = value; }
        }

        public bool ServicioBool
        {
            get { return serviciob; }
            set { serviciob = value; }
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

        public string Cad_gpos
        {
            get { return cad_gpos; }
            set { cad_gpos = value; }
        }

        public string Cad_puestos
        {
            get { return cad_puestos; }
            set { cad_puestos = value; }
        }

        public string Tipo_rev
        {
            get { return tipo_rev; }
            set { tipo_rev = value; }
        }

        public string Tipo_aplica
        {
            get { return tipo_aplica; }
            set { tipo_aplica = value; }
        }

        public string Filtro
        {
            get { return filtro; }
            set { filtro = value; }
        }

        public int Idc_sucursal
        {
            get { return pidc_sucursal; }
            set { pidc_sucursal = value; }
        }

        public int Numcadgrupos
        {
            get { return numcadgrupos; }
            set { numcadgrupos = value; }
        }

        public int Numcadpuesto
        {
            get { return numcadpuesto; }
            set { numcadpuesto = value; }
        }

        public int Idc_puesto_entrega
        {
            get { return pidc_puesto_entrega; }
            set { pidc_puesto_entrega = value; }
        }

        public int Idc_puesto_prepara
        {
            get { return pidc_puesto_prepara; }
            set { pidc_puesto_prepara = value; }
        }

        public int Idc_puesto_revisa
        {
            get { return pidc_puesto_revisa; }
            set { pidc_puesto_revisa = value; }
        }

        public int Pidc_revisionser
        {
            get { return pidc_revisionser; }
            set { pidc_revisionser = value; }
        }
    }
}