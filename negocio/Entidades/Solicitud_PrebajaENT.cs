using System.Data.SqlTypes;

namespace negocio.Entidades
{
    public class Solicitud_PrebajaENT
    {
        private int pidc_usuario;
        private int idc_empleado;
        private int idc_puesto;
        private int apto_reingreso;
        private int honesto;
        private int trabajador;
        private int drogas;
        private int alcohol;
        private int robo;
        private int carta_recomendacion;
        private int contratar;
        private bool renuncia;
        private string motivo;
        private string especificar;
        private SqlDateTime fecha;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int pidc_prebaja;

        public bool Prenuncia
        {
            get { return renuncia; }
            set { renuncia = value; }
        }

        public int Pidc_Prebaja
        {
            get { return pidc_prebaja; }
            set { pidc_prebaja = value; }
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

        public SqlDateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public string Especificar
        {
            get { return especificar; }
            set { especificar = value; }
        }

        public string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }

        public int Contratar
        {
            get { return contratar; }
            set { contratar = value; }
        }

        public int Drogas
        {
            get { return drogas; }
            set { drogas = value; }
        }

        public int Alcohol
        {
            get { return alcohol; }
            set { alcohol = value; }
        }

        public int Robo
        {
            get { return robo; }
            set { robo = value; }
        }

        public int Carta_recomendacion
        {
            get { return carta_recomendacion; }
            set { carta_recomendacion = value; }
        }

        public int Honesto
        {
            get { return honesto; }
            set { honesto = value; }
        }

        public int Trabajador
        {
            get { return trabajador; }
            set { trabajador = value; }
        }

        public int Pidc_usuario
        {
            get { return pidc_usuario; }
            set { pidc_usuario = value; }
        }

        public int Idc_empleado
        {
            get { return idc_empleado; }
            set { idc_empleado = value; }
        }

        public int Idc_puesto
        {
            get { return idc_puesto; }
            set { idc_puesto = value; }
        }

        public int Apto_reingreso
        {
            get { return apto_reingreso; }
            set { apto_reingreso = value; }
        }
    }
}