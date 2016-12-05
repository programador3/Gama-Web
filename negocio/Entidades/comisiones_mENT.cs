using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class comisiones_mENT
    {
        //ByVal mes As Integer, ByVal año As Integer, ByVal idc_agente As Integer, ByVal aldia As Boolean
        private int pidc_agente;
        private int pmes;
        private int panio;
        private bool paldia;

        private string pfechai;
        private string pfechaf;

        public int Pidc_agente
        {
            get { return pidc_agente; }
            set { pidc_agente = value; }
        }
        
        public int Pmes
        { 
            get{ return pmes; }
            set{ pmes = value;}
        }

        public int Panio
        {
            get { return panio; }
            set { panio = value; }
        }

        public bool Paldia
        {
            get { return paldia; }
            set { paldia = value; }
        }

        public string Pfechai
        {
            get { return pfechai; }
            set { pfechai = value; }
        }

        public string Pfechaf
        {
            get { return pfechaf; }
            set { pfechaf = value; }
        }
    }
}
