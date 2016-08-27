using System;

namespace negocio.Entidades
{
    public class QuejasENT
    {
        
        private int idc_queja;
        private int idc_aviso;
        string observaciones;
        string observaciones_satisfecho;
        string encargado;
        bool satisfecho;
        DateTime fecha;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_usuario;

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }

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
        public string Pobservaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
        public string Pobservaciones_satisfecho
        {
            get { return observaciones_satisfecho; }
            set { observaciones_satisfecho = value; }
        }
        public string Pencargado
        {
            get { return encargado; }
            set { encargado = value; }
        }
        public bool Psatisfecho
        {
            get { return satisfecho; }
            set { satisfecho = value; }
        }
        public DateTime Pfecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        public int Idc_aviso
        {
            get { return idc_aviso; }
            set { idc_aviso = value; }
        }
        public int Pidc_queja { get { return idc_queja; } set { idc_queja = value; } }
    }
}