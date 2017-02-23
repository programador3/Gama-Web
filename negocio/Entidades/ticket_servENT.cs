using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class ticket_servENT
    {
        private int pidc_tareaser;
        private int pidc_ticketserv;
        private int pidc_ticketserva;
        private int pidc_puesto;
        private string pmotivo;
        private string pobservaciones;
        private string pcontraseña;
        private int pidc_usuario_term;
        private bool todo;
        private int idc_puesto;
        private bool misdeptos;
        private DateTime pfechaInicio;
        private DateTime pfechafin;
        private int pidc_depto_aten;
        private int pidc_depto_rep;
        private string pfiltro;
        private string ptipo;
        private int pidc_puesto_revisa;
        private int pidc_puesto_prepara;
        private int pidc_puesto_entrega;

        private int pidc_usuario;

        public int Pidc_puesto { get { return idc_puesto; } set { idc_puesto = value; } }
        public bool Psolomisdeptos { get { return misdeptos; } set { misdeptos = value; } }
        public int Pidc_tareaser { get { return pidc_tareaser; } set { pidc_tareaser = value; } }
        public int Pidc_ticketserv { get { return pidc_ticketserv; } set { pidc_ticketserv = value; } }
        public int Pidc_ticketserva { get { return pidc_ticketserva; } set { pidc_ticketserva = value; } }
        public int Pidc_puestomira { get { return pidc_puesto; } set { pidc_puesto = value; } }
        public string Pmotivo { get { return pmotivo; } set { pmotivo = value; } }
        public string Pobservaciones { get { return pobservaciones; } set { pobservaciones = value; } }
        public string Pcontraseña { get { return pcontraseña; } set { pcontraseña = value; } }
        public int Pidc_usuario_term { get { return pidc_usuario_term; } set { pidc_usuario_term = value; } }

        public DateTime PfechaInicio { get { return pfechaInicio; } set { pfechaInicio = value; } }
        public DateTime Pfechafin { get { return pfechafin; } set { pfechafin = value; } }
        public int Pidc_depto_aten { get { return pidc_depto_aten; } set { pidc_depto_aten = value; } }
        public int Pidc_depto_rep { get { return pidc_depto_rep; } set { pidc_depto_rep = value; } }

        public string Pfiltro { get { return pfiltro; } set { pfiltro = value; } }
        public string Ptipo { get { return ptipo; } set { ptipo = value; } }
        public int Pidc_puesto_entrega { get { return pidc_puesto_entrega; } set { pidc_puesto_entrega = value; } }
        public int Pidc_puesto_prepara { get { return pidc_puesto_prepara; } set { pidc_puesto_prepara = value; } }
        public int Pidc_puesto_revisa { get { return pidc_puesto_revisa; } set { pidc_puesto_revisa = value; } }

        public int Pidc_usuario { get { return pidc_usuario; } set { pidc_usuario = value; } }
        public bool Ptodo { get { return todo; } set { todo = value; } }

    }
}
