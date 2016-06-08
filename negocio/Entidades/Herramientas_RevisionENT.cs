using System.Data.SqlTypes;

namespace negocio.Entidades
{
    public class Herramientas_RevisionENT
    {
        private int idc_usuario;
        private int idc_puestorevi;
        private int idc_puestoprebaja;
        private int pidc_prebaja;
        private int pNUMTEL;
        private SqlMoney pMONTO;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private string pCADTEL;

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

        public string PCADTEL
        {
            get { return pCADTEL; }
            set { pCADTEL = value; }
        }

        public int PIDC_prebaja
        {
            get { return pidc_prebaja; }
            set { pidc_prebaja = value; }
        }

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }

        public int PNUMTEL
        {
            get { return pNUMTEL; }
            set { pNUMTEL = value; }
        }

        public SqlMoney PMONTO
        {
            get { return pMONTO; }
            set { pMONTO = value; }
        }

        public int Idc_puestorevi
        {
            get { return idc_puestorevi; }
            set { idc_puestorevi = value; }
        }

        public int Idc_puestoprebaja
        {
            get { return idc_puestoprebaja; }
            set { idc_puestoprebaja = value; }
        }
    }
}