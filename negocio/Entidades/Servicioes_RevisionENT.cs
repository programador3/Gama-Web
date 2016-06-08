using System.Data.SqlTypes;

namespace negocio.Entidades
{
    public class Servicioes_RevisionENT
    {
        private int pidc_puestorevisa;
        private int pidc_puestoprebaja;
        private int idc_prebaja;
        private int cadtotal;
        private int idc_usuario;
        private int pidc_revisionser;
        private string cadobservaciones;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private string observaciones;
        private SqlMoney monto;

        public SqlMoney Monto
        {
            get { return monto; }
            set { monto = value; }
        }

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
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

        public int Pidc_puesto_prerevisa
        {
            get { return pidc_puestorevisa; }
            set { pidc_puestorevisa = value; }
        }

        public int Pidc_revisionser
        {
            get { return pidc_revisionser; }
            set { pidc_revisionser = value; }
        }

        public int Pidc_puestoprebaja
        {
            get { return pidc_puestoprebaja; }
            set { pidc_puestoprebaja = value; }
        }

        public int Idc_prebaja
        {
            get { return idc_prebaja; }
            set { idc_prebaja = value; }
        }

        public int Cadtotal
        {
            get { return cadtotal; }
            set { cadtotal = value; }
        }

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }

        public string Cadobservciones
        {
            get { return cadobservaciones; }
            set { cadobservaciones = value; }
        }
    }
}