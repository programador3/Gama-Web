using System;

namespace negocio.Entidades
{
    public class AgentesENT
    {
        private int idc_telcli;
        private string email;
        private string nombre;
        private string celular;
        private bool activo;
        private string hobbie;
        private string equipo;
        private int idc_tcontacto;
        private string funciones;
        private int idc_titulo;
        private string telefono;
        private int idc_listap;
        private decimal meta_venta;
        private String obs_venta;
        private String cadena_arti;
        private int total_cadenaarti;
        private int tipo;
        private int periodo;
        private string opcion;
        private String cadena_cliente;
        private int total_cadenacliente;
        private DateTime fecha;
        private int idc_cliente;
        private int idc_agente;
        private int idc_actiage;
        private Single latitud;
        private Single longitud;
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

        public int Pidc_cliente
        {
            get { return idc_cliente; }
            set { idc_cliente = value; }
        }

        public int Pidc_agente
        {
            get { return idc_agente; }
            set { idc_agente = value; }
        }

        public int Pidc_actiage
        {
            get { return idc_actiage; }
            set { idc_actiage = value; }
        }

        public int vidc_listap
        {
            get { return idc_listap; }
            set { idc_listap = value; }
        }

        public DateTime pfecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public Single Plat
        {
            get { return latitud; }
            set { latitud = value; }
        }

        public Single Plon
        {
            get { return longitud; }
            set { longitud = value; }
        }

        public String Pobsr
        {
            get { return obs_venta; }
            set { obs_venta = value; }
        }

        public String Pcadenaarti
        {
            get { return cadena_arti; }
            set { cadena_arti = value; }
        }

        public int Ptotalcadenaarti
        {
            get { return total_cadenaarti; }
            set { total_cadenaarti = value; }
        }

        public String Pcadenacliente
        {
            get { return cadena_cliente; }
            set { cadena_cliente = value; }
        }

        public String popcion
        {
            get { return opcion; }
            set { opcion = value; }
        }

        public int ptipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public int pperiodo
        {
            get { return periodo; }
            set { periodo = value; }
        }

        public int Ptotalcadenacliente
        {
            get { return total_cadenacliente; }
            set { total_cadenacliente = value; }
        }

        public Decimal pmeta_venta
        {
            get { return meta_venta; }
            set { meta_venta = value; }
        }

        public int pidc_telcli { get { return idc_telcli; } set { idc_telcli = value; } }
        public string pemail { get { return email; } set { email = value; } }
        public string pnombre { get { return nombre; } set { nombre = value; } }
        public string pcelular { get { return celular; } set { celular = value; } }
        public bool pactivo { get { return activo; } set { activo = value; } }
        public string phobbie { get { return hobbie; } set { hobbie = value; } }
        public string pequipo { get { return equipo; } set { equipo = value; } }
        public int pidc_tcontacto { get { return idc_tcontacto; } set { idc_tcontacto = value; } }
        public string pfunciones { get { return funciones; } set { funciones = value; } }
        public int pidc_titulo { get { return idc_titulo; } set { idc_titulo = value; } }
        public string ptelefono { get { return telefono; } set { telefono = value; } }
    }
}