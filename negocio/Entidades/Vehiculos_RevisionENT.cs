using System.Data.SqlTypes;

namespace negocio.Entidades
{
    public class Vehiculos_RevisionENT
    {
        private int idc_revbasherr;
        private int idc_puestorevi;
        private int IDC_EMPLEADO;
        private int idc_puestoprebaja;
        private int idc_usuario;
        private int pidc_prebaja;
        private int pNUMHERR;
        private int idc_vehiculo;
        private int buenas_condiciones;
        private SqlMoney pMONTO;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private string pCADHERR;
        private string pobservaciones;
        private int pintura;
        private SqlMoney pintura_costo;
        private string pintura_obs;
        private int llantas;
        private SqlMoney llantas_costo;
        private string llantas_obs;
        private int accesorios;
        private SqlMoney accesorios_costo;
        private string accesorios_obs;
        private int carroceria;
        private SqlMoney carroceria_costo;
        private string carroceria_obs;
        private int interior;
        private SqlMoney interior_costo;
        private string interior_obs;
        private int vidrios;
        private SqlMoney vidrios_costo;
        private string vidrios_obs;
        private int focos;
        private SqlMoney focos_costo;
        private string focos_obs;
        private string cadena;
        private string cadena2;
        private int numcad;
        private int numcad2;
        private bool basica;
        private bool falta;
        private decimal total;
        private decimal monto;

        public string Pcadena
        {
            get { return cadena; }
            set { cadena = value; }
        }

        public string Pacedna2
        {
            get { return cadena2; }
            set { cadena2 = value; }
        }

        public int Pnumcad
        {
            get { return numcad; }
            set { numcad = value; }
        }

        public int Pnumcad2
        {
            get { return numcad2; }
            set { numcad2 = value; }
        }

        public bool Pbasica
        {
            get { return basica; }
            set { basica = value; }
        }

        public bool Pfalta
        {
            get { return falta; }
            set { falta = value; }
        }

        public decimal Pmonto
        {
            get { return monto; }
            set { monto = value; }
        }

        public decimal Ptotal
        {
            get { return total; }
            set { total = value; }
        }

        public int Focos
        {
            get { return focos; }
            set { focos = value; }
        }

        public SqlMoney Focos_costo
        {
            get { return focos_costo; }
            set { focos_costo = value; }
        }

        public string Focos_obs
        {
            get { return focos_obs; }
            set { focos_obs = value; }
        }

        public int Vidrios
        {
            get { return vidrios; }
            set { vidrios = value; }
        }

        public SqlMoney Vidrios_costo
        {
            get { return vidrios_costo; }
            set { vidrios_costo = value; }
        }

        public string Vidrios_obs
        {
            get { return vidrios_obs; }
            set { vidrios_obs = value; }
        }

        public int Interior
        {
            get { return interior; }
            set { interior = value; }
        }

        public SqlMoney Interior_costo
        {
            get { return interior_costo; }
            set { interior_costo = value; }
        }

        public string Interior_obs
        {
            get { return interior_obs; }
            set { interior_obs = value; }
        }

        public int Carroceria
        {
            get { return carroceria; }
            set { carroceria = value; }
        }

        public int Pidc_revbasherr
        {
            get { return idc_revbasherr; }
            set { idc_revbasherr = value; }
        }

        public SqlMoney Carroceria_costo
        {
            get { return carroceria_costo; }
            set { carroceria_costo = value; }
        }

        public string Carroceria_obs
        {
            get { return carroceria_obs; }
            set { carroceria_obs = value; }
        }

        public int Accesorios
        {
            get { return accesorios; }
            set { accesorios = value; }
        }

        public SqlMoney Accesorios_costo
        {
            get { return accesorios_costo; }
            set { accesorios_costo = value; }
        }

        public string Accesorios_obs
        {
            get { return accesorios_obs; }
            set { accesorios_obs = value; }
        }

        public int Llantas
        {
            get { return llantas; }
            set { llantas = value; }
        }

        public int Pidc_empleado
        {
            get { return IDC_EMPLEADO; }
            set { IDC_EMPLEADO = value; }
        }

        public SqlMoney Llantas_costo
        {
            get { return llantas_costo; }
            set { llantas_costo = value; }
        }

        public string Llantas_obs
        {
            get { return llantas_obs; }
            set { llantas_obs = value; }
        }

        public int Pintura
        {
            get { return pintura; }
            set { pintura = value; }
        }

        public SqlMoney Pintura_costo
        {
            get { return pintura_costo; }
            set { pintura_costo = value; }
        }

        public string Pintura_obs
        {
            get { return pintura_obs; }
            set { pintura_obs = value; }
        }

        public int Buenas_condiciones
        {
            get { return buenas_condiciones; }
            set { buenas_condiciones = value; }
        }

        public string POBSERVACIONES
        {
            get { return pobservaciones; }
            set { pobservaciones = value; }
        }

        public string PCADHERR
        {
            get { return pCADHERR; }
            set { pCADHERR = value; }
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

        public int PIDC_prebaja
        {
            get { return pidc_prebaja; }
            set { pidc_prebaja = value; }
        }

        public int Idc_vehiculo
        {
            get { return idc_vehiculo; }
            set { idc_vehiculo = value; }
        }

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }

        public int PNUMHERR
        {
            get { return pNUMHERR; }
            set { pNUMHERR = value; }
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