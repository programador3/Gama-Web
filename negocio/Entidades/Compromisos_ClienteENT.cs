using System;


namespace negocio.Entidades
{
    public class Compromisos_ClienteENT
    {

        private int pidc_cliente;
        private string pcompromiso;
        private int pidc_tipocomcli;
        private string pcliente;
        private string prfc;
        private string pclv;
        

        private int pidc_clicompromiso;
        private bool pcompletada;
        private string pobserv;

        public int Pidc_cliente
        {
            get { return pidc_cliente; }
            set { pidc_cliente = value; }
        }

        public string Pcompromiso
        {
            get { return pcompromiso; }
            set { pcompromiso = value; }
        }

        public int Pidc_tipocomcli
        {
            get { return pidc_tipocomcli; }
            set { pidc_tipocomcli = value; }
        }

        public string Pcliente
        {
            get { return pcliente; }
            set { pcliente = value; }
        }

        public string Prfc
        {
            get { return prfc; }
            set { prfc = value; }
        }

        public string Pclv
        {
            get { return pclv; }
            set { pclv = value; }
        }

        public int Pidc_clicompromiso
        {
            get { return pidc_clicompromiso; }
            set { pidc_clicompromiso = value; }
        }

        public bool Pcompletada
        {
            get { return pcompletada; }
            set { pcompletada = value; }
        }

        public string Pobserv
        {
            get { return pobserv; }
            set { pobserv = value; }
        }
    }
}
