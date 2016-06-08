using System;

namespace negocio.Entidades
{
    public class CandidatosENT
    {
        private DateTime fecha;
        private int pidc_puesto;
        private int pidc_candidato;
        private int pidc_puestoprep;
        private int pidc_pre_empleado;
        private int pidc_revisionser;
        private string motivo;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_prepara;
        private string cadser;
        private int totalcadser;
        private int pidc_usuario;
        private string nombreS;
        private string paterno;
        private string materno;

        public int Pidc_pre_empleado
        {
            get { return pidc_pre_empleado; }
            set { pidc_pre_empleado = value; }
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

        public int Pidc_revisionser
        {
            get { return pidc_revisionser; }
            set { pidc_revisionser = value; }
        }

        public int Pidc_usuario
        {
            get { return pidc_usuario; }
            set { pidc_usuario = value; }
        }

        public int Pidc_candidato
        {
            get { return pidc_candidato; }
            set { pidc_candidato = value; }
        }

        public int Totalcadsarch
        {
            get { return totalcadser; }
            set { totalcadser = value; }
        }

        public string Cadarch
        {
            get { return cadser; }
            set { cadser = value; }
        }

        public int Pidc_prepara
        {
            get { return idc_prepara; }
            set { idc_prepara = value; }
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

        public int Pidc_puesto
        {
            get { return pidc_puesto; }
            set { pidc_puesto = value; }
        }

        public int Pidc_puestobaja
        {
            get { return pidc_puestoprep; }
            set { pidc_puestoprep = value; }
        }

        public DateTime Pfecha { get { return fecha; } set { fecha = value; } }

        public string Pmotivo
        {
            get { return motivo; }
            set { motivo = value; }
        }
    }
}