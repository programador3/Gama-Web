using System;

namespace negocio.Entidades
{
    public class TareasENT
    {
        private int idc_tarea;
        private int idc_tipoi;
        private int idc_proceso;
        private int avance;
        private int idc_tareaser;
        private string tipofiltro;
        private string tipofiltros;
        private int idc_tarea_h;
        private int idc_puesto;
        private int idc_depto;
        private int idc_puesto_asigna;
        private int total_cadena_arch;
        private string cadena_arch;
        private int total_cadena_pro;
        private string cadena_pro;
        private string pdescripcion;
        private string tipo;
        private string tipo_cambio_tarea;
        private string extension;
        private bool parchivo;
        private bool correcto;
        private bool APLICAR_CAMBIOTODOS;
        private bool OTRO;
        private bool cambio_fecha_ori;
        private string comentarios;
        private DateTime fecha;
        private DateTime fecha_fin;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_usuario;
        private int pcasoFiltro; /* PARAMETRO PARA EL FILTRO EN RELACION PUESTO TAREAS*/

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

        public Boolean PAPLICAR_CAMBIOTODOS { get { return APLICAR_CAMBIOTODOS; } set { APLICAR_CAMBIOTODOS = value; } }

        public int Pidc_tarea
        {
            get { return idc_tarea; }
            set { idc_tarea = value; }
        }

        public int Pidc_depto
        {
            get { return idc_depto; }
            set { idc_depto = value; }
        }

        public int Pavance
        {
            get { return avance; }
            set { avance = value; }
        }

        public bool Parchivo
        {
            get { return parchivo; }
            set { parchivo = value; }
        }

        public bool Pfecha_cambio_original
        {
            get { return cambio_fecha_ori; }
            set { cambio_fecha_ori = value; }
        }

        public bool Pcorrecto
        {
            get { return correcto; }
            set { correcto = value; }
        }
        public bool POTROPUESTO
        {
            get { return OTRO; }
            set { OTRO = value; }
        }

        public int Pidc_tarea_h
        {
            get { return idc_tarea_h; }
            set { idc_tarea_h = value; }
        }
        public int Pidc_tareaser
        {
            get { return idc_tareaser; }
            set { idc_tareaser = value; }
        }
        public int Pidc_puesto
        {
            get { return idc_puesto; }
            set { idc_puesto = value; }
        }

        public int Pidc_puesto_asigna
        {
            get { return idc_puesto_asigna; }
            set { idc_puesto_asigna = value; }
        }

        public int Pidc_tipoi
        {
            get { return idc_tipoi; }
            set { idc_tipoi = value; }
        }

        public int Pidc_proceso
        {
            get { return idc_proceso; }
            set { idc_proceso = value; }
        }

        public int Ptotal_cadena_arch
        {
            get { return total_cadena_arch; }
            set { total_cadena_arch = value; }
        }

        public int Ptotal_cadena_pro
        {
            get { return total_cadena_pro; }
            set { total_cadena_pro = value; }
        }

        public string Pcomentarios
        {
            get { return comentarios; }
            set { comentarios = value; }
        }

        public string Pcadena_arch
        {
            get { return cadena_arch; }
            set { cadena_arch = value; }
        }

        public string Pcadena_pro
        {
            get { return cadena_pro; }
            set { cadena_pro = value; }
        }

        public string Pextension
        {
            get { return extension; }
            set { extension = value; }
        }

        public string Ptipof
        {
            get { return tipofiltro; }
            set { tipofiltro = value; }
        }

        public string Ptipo_cambio_tarea
        {
            get { return tipo_cambio_tarea; }
            set { tipo_cambio_tarea = value; }
        }

        public string Ptipofs
        {
            get { return tipofiltros; }
            set { tipofiltros = value; }
        }

        public string Pdescripcion
        {
            get { return pdescripcion; }
            set { pdescripcion = value; }
        }

        public string Ptipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public DateTime Pfecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public DateTime Pfecha_fin
        {
            get { return fecha_fin; }
            set { fecha_fin = value; }
        }

        public int PcasoFiltro
        {
            get { return pcasoFiltro; }
            set { pcasoFiltro = value; }
        }
    }
}