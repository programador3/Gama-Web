using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class reproductor_llamadasENT
    {
        private int pidc_llamada;
        private int pidc_llamadamarcar;
        private string pObservaciones;
        private string ptipo;
        private bool pborrado;
        

        public int Pidc_llamada
        {
            get { return pidc_llamada; }
            set { pidc_llamada = value; }
        }

        public int Pidc_llamadamarcar
        {
            get { return pidc_llamadamarcar; }
            set { pidc_llamadamarcar = value; }
        }        

        public string PObservaciones
        {
            get { return pObservaciones; }
            set { pObservaciones = value; }
        }

        public string Ptipo
        {
            get { return ptipo; }
            set { ptipo = value; }
        }

        public bool Pborrado
        {
            get { return pborrado; }
            set { pborrado = value; }
        }
    }
}
