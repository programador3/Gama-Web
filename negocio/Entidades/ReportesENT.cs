using System;

namespace negocio.Entidades
{
    public class ReportesENT
    {
        bool reasigna;
        string cerrado;
        private int idc_tiporep;
        private int idc_empleado;
        private int idc_empleadorep;
        private int idc_empleadoalta;
        private int idc_puesto;
        private DateTime fecha;
        private DateTime fechafin;
        private string Observaciones;
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
        public string PCERRADO
        {
            get { return cerrado; }
            set { cerrado = value; }
        }
        public bool preasigna
        {
            get { return reasigna; }
            set { reasigna = value; }
        }
        public string Pcadena
        {
            get { return cadena; }
            set { cadena = value; }
        }

        public string PObservaciones
        {
            get { return Observaciones; }
            set { Observaciones = value; }
        }

        public int Pidc_empleado
        {
            get { return idc_empleado; }
            set { idc_empleado = value; }
        }

        public int Pidc_empleadorep
        {
            get { return idc_empleadorep; }
            set { idc_empleadorep = value; }
        }

        public int Pidc_tiporep
        {
            get { return idc_tiporep; }
            set { idc_tiporep = value; }
        }

        public int Ptotal_cadena
        {
            get { return total_cadena; }
            set { total_cadena = value; }
        }
        
             public int Pidc_empleadoalta
        {
            get { return idc_empleadoalta; }
            set { idc_empleadoalta = value; }
        }
        public int Pidc_puesto
        {
            get { return idc_puesto; }
            set { idc_puesto = value; }
        }
        public DateTime Pfecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        public DateTime Pfechafin
        {
            get { return fechafin; }
            set { fechafin = value; }
        }
    }
}