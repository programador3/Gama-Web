using System.Data.SqlTypes;

namespace negocio.Entidades
{
    public class Revision_FinalENT
    {
        private int pidc_puestorevi;
        private int pidc_puestoprebaja;
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
        private SqlDateTime fecha;

        public int Monto
        {
            get { return monto; }
            set { monto = value; }
        }

        public SqlDateTime Fecha
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

        public int Pidc_puestorevi
        {
            get { return pidc_puestorevi; }
            set { pidc_puestorevi = value; }
        }

        public int Pidc_puestoprebaja
        {
            get { return pidc_puestoprebaja; }
            set { pidc_puestoprebaja = value; }
        }
    }
}