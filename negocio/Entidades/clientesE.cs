using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio.Entidades
{
    public class clientesE
    {
        int idc_cliente;
        string nombre;
        string rfc;
        string telefono;
        string correo;
        bool borrado;

        public int Idc_cliente
        {
            get { return idc_cliente; }
            set { idc_cliente = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Rfc
        {
            get { return rfc; }
            set { rfc = value; }
        }
        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }
        public bool Borrado
        {
            get { return borrado; }
            set { borrado = value; }
        }

    }


}
