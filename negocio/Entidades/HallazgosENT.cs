using System;

namespace negocio
{
    public class HallazgosENT
    {
        private DateTime fecha;
        private string hallazgo;
        private int idc_sucursal;
        private int idc_vehiculo;
        private int idc_hallazgo;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_usuario;
        private int idc_usuario_sol;

        public int Idc_usuario_sol
        {
            get { return idc_usuario_sol; }
            set { idc_usuario_sol = value; }
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

        public string Phallazgo
        {
            get { return hallazgo; }
            set { hallazgo = value; }
        }

        public int Pidc_vehiculo
        {
            get { return idc_vehiculo; }
            set { idc_vehiculo = value; }
        }

        public int pidc_sucursal
        {
            get { return idc_sucursal; }
            set { idc_sucursal = value; }
        }

        public int pidc_hallazgo
        {
            get { return idc_hallazgo; }
            set { idc_hallazgo = value; }
        }

        public DateTime pfecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
    }
}