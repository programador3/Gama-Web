using System;

namespace negocio.Entidades
{
    public class TareasAutomaticasENT
    {
        private int tipo_filtro;
        private int idc_depto;
        private int idc_tarea_auto;
        private string descripcion;
        private int idc_puesto_asigna;
        private int idc_puesto_realiza;
        private DateTime fecha_empieza;
        private DateTime fecha_termina;
        private int horas_terminar;
        private string tipo;
        private int frecuencia;
        private int dia_mes;
        private int hora_especifica;
        private int numero_horas;
        private int numero_horas_comienza;
        private int numero_horas_termina;
        private bool lunes;
        private bool martes;
        private bool miercole;
        private bool jueves;
        private bool viernes;
        private bool sabado;
        private bool domingo;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;
        private int idc_usuario;
        public int Ptipofiltro
        {
            get { return tipo_filtro; }
            set { tipo_filtro = value; }
        }
        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }

        public String Pdirecip
        {
            get { return pdirecip; }
            set { pdirecip = value; }
        }

        public String Pnombrepc
        {
            get { return pnombrepc; }
            set { pnombrepc = value; }
        }

        public String Pusuariopc
        {
            get { return pusuariopc; }
            set { pusuariopc = value; }
        }

        public int Pidc_depto { get { return idc_depto; } set { idc_depto = value; } }
        public int Pidc_tarea_auto { get { return idc_tarea_auto; } set { idc_tarea_auto = value; } }
        public String Pdescripcion { get { return descripcion; } set { descripcion = value; } }
        public int Pidc_puesto_asigna { get { return idc_puesto_asigna; } set { idc_puesto_asigna = value; } }
        public int Pidc_puesto_realiza { get { return idc_puesto_realiza; } set { idc_puesto_realiza = value; } }
        public DateTime Pfecha_empieza { get { return fecha_empieza; } set { fecha_empieza = value; } }
        public DateTime Pfecha_termina { get { return fecha_termina; } set { fecha_termina = value; } }
        public int Phoras_terminar { get { return horas_terminar; } set { horas_terminar = value; } }
        public String Ptipo { get { return tipo; } set { tipo = value; } }
        public int Pfrecuencia { get { return frecuencia; } set { frecuencia = value; } }
        public int Pdia_mes { get { return dia_mes; } set { dia_mes = value; } }
        public int Phora_especifica { get { return hora_especifica; } set { hora_especifica = value; } }
        public int Pnumero_horas { get { return numero_horas; } set { numero_horas = value; } }
        public int Pnumero_horas_comienza { get { return numero_horas_comienza; } set { numero_horas_comienza = value; } }
        public int Pnumero_horas_termina { get { return numero_horas_termina; } set { numero_horas_termina = value; } }
        public bool Plunes { get { return lunes; } set { lunes = value; } }
        public bool Pmartes { get { return martes; } set { martes = value; } }
        public bool Pmiercole { get { return miercole; } set { miercole = value; } }
        public bool Pjueves { get { return jueves; } set { jueves = value; } }
        public bool Pviernes { get { return viernes; } set { viernes = value; } }
        public bool Psabado { get { return sabado; } set { sabado = value; } }
        public bool Pdomingo { get { return domingo; } set { domingo = value; } }
    }
}