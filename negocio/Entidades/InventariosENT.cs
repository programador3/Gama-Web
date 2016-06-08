using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class InventariosENT
    {
        private int idc_actscategoria;
        private int idc_puesto;
        private int folio;
        private bool area;
        private string observaciones;
        private int idc_areaact;
        private string cadena;
        private int total_cadena;

        public int Pidc_actscategoria
        {
            get { return idc_actscategoria; }
            set { idc_actscategoria = value; }
        }

        public int Pidc_puesto
        {
            get { return idc_puesto; }
            set { idc_puesto = value; }
        }

        public int Pfolio
        {
            get { return folio; }
            set { folio = value; }
        }

        public int Pidc_areaact
        {
            get { return idc_areaact; }
            set { idc_areaact = value; }
        }

        public int Ptotal_cadena
        {
            get { return total_cadena; }
            set { total_cadena = value; }
        }

        public string Pcadena
        {
            get { return cadena; }
            set { cadena = value; }
        }

        public string Pobservaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public bool Parea
        {
            get { return area; }
            set { area = value; }
        }
    }
}