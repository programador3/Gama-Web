using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class uni_archiE
    {
        string cod_archivo;
        int idc_usuario;


        public string Cod_archivo
        {
            get { return cod_archivo; }
            set { cod_archivo = value; }
        }

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }
    }
}
