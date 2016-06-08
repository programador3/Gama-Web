using System;

namespace negocio.Entidades
{
    public class FaltasENT
    {
        private int pidc_empleado_falta;
        private int pidc_puesto;
        private int idc_prebaja;
        private int idc_usuario;
        private string cadobservaciones;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private string concepto;
        private bool consulta;

        public bool Pconsulta
        {
            get { return consulta; }
            set { consulta = value; }
        }

        public string Concepto
        {
            get { return concepto; }
            set { concepto = value; }
        }

        private int monto;
        private DateTime fecha;

        public int Monto
        {
            get { return monto; }
            set { monto = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
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

        public int Idc_prebaja
        {
            get { return idc_prebaja; }
            set { idc_prebaja = value; }
        }

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }

        public string Observaciones
        {
            get { return cadobservaciones; }
            set { cadobservaciones = value; }
        }

        public int Pidc_empleado_falta
        {
            get { return pidc_empleado_falta; }
            set { pidc_empleado_falta = value; }
        }

        public int Pidc_puesto
        {
            get { return pidc_puesto; }
            set { pidc_puesto = value; }
        }
    }
}