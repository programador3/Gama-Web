using System;


namespace negocio.Entidades
{
    public class Puesto_EquiENT
    {
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int pidc_usuario;

        private int pidc_puesto_equi;
        private string pdescripcion;
        private Boolean pactivo;
        private string pmov;

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

        public int Pidc_usuario
        {
            get { return pidc_usuario; }
            set { pidc_usuario = value; }
        }

        public int Pidc_puesto_equi
        {
            get { return pidc_puesto_equi; }
            set { pidc_puesto_equi = value; }
        }

        public string Pdescripcion
        {
            get { return pdescripcion; }
            set { pdescripcion = value; }
        }

        public Boolean Pactivo
        {
            get { return pactivo; }
            set { pactivo = value; }
        }

        public string Pmov
        {
            get { return pmov; }
            set { pmov = value; }
        }
    }
}
