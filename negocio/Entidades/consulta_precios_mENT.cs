namespace negocio.Entidades
{
   public class consulta_precios_mENT
    {
        private int pidc_agente;
        private int pidc_cliente;
        private int pidc_articulo;
        private int pidc_sucursal;
        private int pcantidad;
        private bool pcambiolista;
        private string pvalor;
        private string ptipo;


        public int Pidc_agente
        {
            get { return pidc_agente; }
            set { pidc_agente=value; }
        }

        public int Pidc_cliente
        {
            get { return pidc_cliente; }
            set { pidc_cliente = value; }
        }

        public int Pidc_articulo
        {
            get { return pidc_articulo; }
            set { pidc_articulo = value; }
        }

        public int Pidc_sucursal
        {
            get { return pidc_sucursal; }
            set { pidc_sucursal = value; }
        }

        public int Pcantidad
        {
            get { return pcantidad; }
            set { pcantidad = value; }
        }

        public bool Pcambiolista
        {
            get { return pcambiolista; }
            set { pcambiolista = value; }
        }

        public string Pvalor
        {
            get { return pvalor; }
            set { pvalor = value; }
        }

        public string Ptipo
        {
            get { return ptipo; }
            set { ptipo = value; }
        }
    }
}
