using System;

namespace negocio.Entidades
{
    public class VacacionesENT
    {
        private DateTime fecha;
        private string cadena_fechas;
        private int total_cadena_fechas;
        private int idc_empleados;
        private int tomadas;
        private int pagadas;
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

        public int Pidc_empleados
        {
            get { return idc_empleados; }
            set { idc_empleados = value; }
        }

        public int Ptomadas
        {
            get { return tomadas; }
            set { tomadas = value; }
        }

        public int Ppagadas
        {
            get { return pagadas; }
            set { pagadas = value; }
        }

        public int Ptotal_cadena_fechas
        {
            get { return total_cadena_fechas; }
            set { total_cadena_fechas = value; }
        }

        public string Pcadena_fechas
        {
            get { return cadena_fechas; }
            set { cadena_fechas = value; }
        }

        public DateTime Pfecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
    }
}