using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
   public class Area_TrabajoENT
    {
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int pidc_usuario;

        private int pidc_Sucursal;
        private int pidc_Area;
        private int pidc_cedis;
        private string pdescripcion;
        private Boolean pactivo;

        

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

        public int Pidc_usuario
        {
            get { return pidc_usuario; }
            set { pidc_usuario = value; }
        }

        public int Pidc_Sucursal
        {
            get { return pidc_Sucursal; }
            set { pidc_Sucursal = value; }
        }

        public int Pidc_Area
        {
            get { return pidc_Area; }
            set { pidc_Area = value; }
        }

        public int Pidc_cedis
        {
            get { return pidc_cedis; }
            set { pidc_cedis = value; }
        }

        public string Pdescripcion
        {
            get { return pdescripcion; }
            set { pdescripcion = value; }
        }

        public Boolean Pactivo
        {
            get { return pactivo; }
            set { pactivo = value; }
        }

        

    }
}
