using System.Data.SqlTypes;

namespace negocio.Entidades
{
    public class CuestionariosENT
    {
        private int idc_clienteh;
        private int idc_tipocliente;
        private int idc_cuestionario;
        private int idc_cuestionario_elite;
        private int idc_cuestionario_extra;
        private int pidc_pregunta;
        private int pidc_cliente;
        private int idc_cotizacion;
        private int pidc_cuestionario_tipo;
        private int total_cadena_preguntas;
        private int total_cadena_respuestas;
        private int total_cadena_categorias;
        private int total_cadena_vendedores;
        private string cadena_vendedores;
        private string cadena_preguntas;
        private string cadena_categorias;
        private string cadena_respuestas;
        private string cuestionario;
        private string ptipo;
        private string ptiposoli;
        private string nombre;
        private string telefono;
        private string correo;
        private bool acepta_correos;
        private bool primer_encuesta;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private string dominio;
        private int idc_usuario;
        private SqlDateTime inicio;
        private SqlDateTime fin;
        bool prod_esp;
        public bool Prod_esp
        {
            get { return prod_esp; }
            set { prod_esp = value; }
        }

        public SqlDateTime Pinicio
        {
            get { return inicio; }
            set { inicio = value; }
        }

        public SqlDateTime Pfin
        {
            get { return fin; }
            set { fin = value; }
        }
        public string Pdominio
        {
            get { return dominio; }
            set { dominio = value; }
        }
        public int Pidc_coti
        {
            get { return idc_cotizacion; }
            set { idc_cotizacion = value; }
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

        public int Pidc_cuestionario
        {
            get { return idc_cuestionario; }
            set { idc_cuestionario = value; }
        }

        public int Pidc_cuestionario_cotizacion
        {
            get { return idc_cuestionario_elite; }
            set { idc_cuestionario_elite = value; }
        }
        public int Pidc_cuestionario_extra
        {
            get { return idc_cuestionario_extra; }
            set { idc_cuestionario_extra = value; }
        }
        public int Pidc_cuestionario_tipo
        {
            get { return pidc_cuestionario_tipo; }
            set { pidc_cuestionario_tipo = value; }
        }

        public int Pidc_cliente
        {
            get { return pidc_cliente; }
            set { pidc_cliente = value; }
        }

        public int Pidc_tipocliente
        {
            get { return idc_tipocliente; }
            set { idc_tipocliente = value; }
        }

        public int Ppidc_pregunta
        {
            get { return pidc_pregunta; }
            set { pidc_pregunta = value; }
        }

        public int Pidc_clienteh
        {
            get { return idc_clienteh; }
            set { idc_clienteh = value; }
        }

        public int Ptotal_cadena_preguntas
        {
            get { return total_cadena_preguntas; }
            set { total_cadena_preguntas = value; }
        }

        public int Ptotal_cadena_respuestas
        {
            get { return total_cadena_respuestas; }
            set { total_cadena_respuestas = value; }
        }

        public string Pcadena_preguntas
        {
            get { return cadena_preguntas; }
            set { cadena_preguntas = value; }
        }

        public int Ptotal_cadena_categorias
        {
            get { return total_cadena_categorias; }
            set { total_cadena_categorias = value; }
        }

        public int Ptotal_cadena_vendedores
        {
            get { return total_cadena_vendedores; }
            set { total_cadena_vendedores = value; }
        }

        public string Pcadena_vendedores
        {
            get { return cadena_vendedores; }
            set { cadena_vendedores = value; }
        }

        public string Pcadena_categorias
        {
            get { return cadena_categorias; }
            set { cadena_categorias = value; }
        }

        public string Pcadena_respuestas
        {
            get { return cadena_respuestas; }
            set { cadena_respuestas = value; }
        }

        public string Pcuestionario
        {
            get { return cuestionario; }
            set { cuestionario = value; }
        }

        public string Ptipo
        {
            get { return ptipo; }
            set { ptipo = value; }
        }
        public string Ptipo_soli
        {
            get { return ptiposoli; }
            set { ptiposoli = value; }
        }
        public string Ptelefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Pcorreo
        {
            get { return correo; }
            set { correo = value; }
        }

        public string Pnombres
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public bool Pacepta_correo
        {
            get { return acepta_correos; }
            set { acepta_correos = value; }
        }

        public bool Primer_ecnuesta
        {
            get { return primer_encuesta; }
            set { primer_encuesta = value; }
        }
    }
}