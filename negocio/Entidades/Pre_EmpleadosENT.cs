using System.Data.SqlTypes;

namespace negocio.Entidades
{
    public class Pre_EmpleadosENT
    {
        private string pobservaciones;
        private int pidc_tipodoc;
        private int idc_tipodocarc;
        private string cadtel;
        private int numcadtel;
        private string cadelelic;
        private int numcadelelic;
        private string cadhijos;
        private int numcadhijos;
        private string cadhorarios;
        private int numcadhorarios;
        private int pidc_candidatos;
        private string pvalor;
        private int idc_pre_empleado;
        private int idc_candidato;
        private int idc_prepara;
        private SqlDateTime fec_nac;
        private int idc_puesto;
        private int idc_edocivil;
        private int idc_estado;
        private string sexo;
        private int idc_sucursal;
        private string titulo;
        private int idc_nzona;
        private int idc_colonia;
        private string direccion;
        private string num_imss;
        private string rfc;
        private string curp;
        private SqlMoney sueldo;
        private SqlMoney complementos;
        private bool premio_transporte;
        private string nombre_padre;
        private string nombre_madre;
        private string esposo;
        private string correo_personal;
        private bool pcapacitacion;
        private string nombreS;
        private string paterno;
        private string materno;
        private string cadsel;
        private string cadena_pape;
        private int totcadenapape;
        private int numcad;
        private int pidc_prepara;
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

        public int tot_cadena_pape
        {
            get { return totcadenapape; }
            set { totcadenapape = value; }
        }

        public string cadena_papeleria
        {
            get { return cadena_pape; }
            set { cadena_pape = value; }
        }

        public int Pidc_prepara
        {
            get { return pidc_prepara; }
            set { pidc_prepara = value; }
        }

        public string Cadsel
        {
            get { return cadsel; }
            set { cadsel = value; }
        }

        public string Pobersvaciones
        {
            get { return pobservaciones; }
            set { pobservaciones = value; }
        }

        public int Numcad
        {
            get { return numcad; }
            set { numcad = value; }
        }

        public string Nombre
        {
            get { return nombreS; }
            set { nombreS = value; }
        }

        public string Paterno
        {
            get { return paterno; }
            set { paterno = value; }
        }

        public string Materno
        {
            get { return materno; }
            set { materno = value; }
        }

        public bool Pcapacitacion
        {
            get { return pcapacitacion; }
            set { pcapacitacion = value; }
        }

        public int Numcadtel
        {
            get { return numcadtel; }
            set { numcadtel = value; }
        }

        public string Cadtel
        {
            get { return cadtel; }
            set { cadtel = value; }
        }

        public int Numcadelelic
        {
            get { return numcadelelic; }
            set { numcadelelic = value; }
        }

        public string Cadelelic
        {
            get { return cadelelic; }
            set { cadelelic = value; }
        }

        public int Numcadhijos
        {
            get { return numcadhijos; }
            set { numcadhijos = value; }
        }

        public string Cadhijos
        {
            get { return cadhijos; }
            set { cadhijos = value; }
        }

        public int Numcadhorarios
        {
            get { return numcadhorarios; }
            set { numcadhorarios = value; }
        }

        public string Cadhorarios
        {
            get { return cadhorarios; }
            set { cadhorarios = value; }
        }

        public string Pvalor
        {
            get { return pvalor; }
            set { pvalor = value; }
        }

        public int Pidc_candidato
        {
            get { return pidc_candidatos; }
            set { pidc_candidatos = value; }
        }

        public int Idc_pre_empleado
        {
            get { return idc_pre_empleado; }
            set { idc_pre_empleado = value; }
        }

        public int Pidc_tipodoc
        {
            get { return pidc_tipodoc; }
            set { pidc_tipodoc = value; }
        }

        public int Pidc_tipodocarc
        {
            get { return idc_tipodocarc; }
            set { idc_tipodocarc = value; }
        }

        public int Idc_candidato { get { return idc_candidato; } set { idc_candidato = value; } }
        public int Idc_prepara { get { return idc_prepara; } set { idc_prepara = value; } }
        public SqlDateTime Fec_nac { get { return fec_nac; } set { fec_nac = value; } }
        public int Idc_puesto { get { return idc_puesto; } set { idc_puesto = value; } }
        public int Idc_edocivil { get { return idc_edocivil; } set { idc_edocivil = value; } }
        public int Idc_estado { get { return idc_estado; } set { idc_estado = value; } }
        public string Sexo { get { return sexo; } set { sexo = value; } }
        public int Idc_sucursal { get { return idc_sucursal; } set { idc_sucursal = value; } }
        public string Titulo { get { return titulo; } set { titulo = value; } }
        public int Idc_nzona { get { return idc_nzona; } set { idc_nzona = value; } }
        public int Idc_colonia { get { return idc_colonia; } set { idc_colonia = value; } }
        public string Direccion { get { return direccion; } set { direccion = value; } }
        public string Num_imss { get { return num_imss; } set { num_imss = value; } }
        public string Rfc { get { return rfc; } set { rfc = value; } }
        public string Curp { get { return curp; } set { curp = value; } }
        public SqlMoney Sueldo { get { return sueldo; } set { sueldo = value; } }
        public SqlMoney Complementos { get { return complementos; } set { complementos = value; } }
        public bool Premio_transporte { get { return premio_transporte; } set { premio_transporte = value; } }
        public string Nombre_padre { get { return nombre_padre; } set { nombre_padre = value; } }
        public string Nombre_madre { get { return nombre_madre; } set { nombre_madre = value; } }
        public string Esposo { get { return esposo; } set { esposo = value; } }
        public string Correo_personal { get { return correo_personal; } set { correo_personal = value; } }
    }
}