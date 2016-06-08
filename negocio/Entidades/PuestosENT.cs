using System;

namespace negocio.Entidades
{
    public class PuestosENT
    {
        private string extension;
        private int idc_puesto;
        private DateTime fecha_inicio;
        private DateTime fecha_fin;
        private int idc_empleado_pmd;
        private int status;
        private int idc_puestoperfil;
        private int pidc_prepara;
        private string observaciones;
        private string ptipo;
        private bool archivo;
        private bool asistencia;
        private bool justiciante;
        private int pidc_pre_empleado;
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

        public int Pidc_pre_empleado
        {
            get { return pidc_pre_empleado; }
            set { pidc_pre_empleado = value; }
        }

        public int Pidc_empleado_pmd
        {
            get { return idc_empleado_pmd; }
            set { idc_empleado_pmd = value; }
        }

        public int Pidc_prepara
        {
            get { return pidc_prepara; }
            set { pidc_prepara = value; }
        }

        public int Pstatus
        {
            get { return status; }
            set { status = value; }
        }

        public string Pobservaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public string Pextension
        {
            get { return extension; }
            set { extension = value; }
        }

        public string Ptipo
        {
            get { return ptipo; }
            set { ptipo = value; }
        }

        public bool Parchivo
        {
            get { return archivo; }
            set { archivo = value; }
        }

        public bool Pasistencia
        {
            get { return asistencia; }
            set { asistencia = value; }
        }

        public bool Pjustificante
        {
            get { return justiciante; }
            set { justiciante = value; }
        }

        public int Idc_Puesto
        {
            get { return idc_puesto; }
            set { idc_puesto = value; }
        }

        public int Idc_puestoperfil
        {
            get { return idc_puestoperfil; }
            set { idc_puestoperfil = value; }
        }

        public DateTime Pfecha_inicio
        {
            get { return fecha_inicio; }
            set { fecha_inicio = value; }
        }

        public DateTime Pfecha_fin
        {
            get { return fecha_fin; }
            set { fecha_fin = value; }
        }
    }
}