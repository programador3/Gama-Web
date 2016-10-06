using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
  public  class Inconvenientes_ClienteENT
    {
        private int pidc_cliente;
        private string pinconveniente;

        /*******************/
        public int Pidc_cliente
        {
            get { return pidc_cliente; }
            set { pidc_cliente = value; }
        }

        public string Pinconveniente
        {
            get { return pinconveniente; }
            set { pinconveniente = value; }
        }

       

    }
}
