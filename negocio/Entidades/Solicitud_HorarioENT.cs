using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace negocio.Entidades
{
    public class Solicitud_HorarioENT
    {
        private int idc_horario_erm;
        private int idc_empleado;
        private DateTime fecha;
        private bool no_comida;
        private bool no_salida;
        private int hora_entrada;
        private int hora_salida;
        private int hora_salida_comida;
        private int hora_entrada_comida;
        private int idc_sucursal;
        private string observaciones;
        private string status;
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

        public string Pstatus
        {
            get { return status; }
            set { status = value; }
        }

        public string Pobservaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public DateTime Pfecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public int Pidc_empleado
        {
            get { return idc_empleado; }
            set { idc_empleado = value; }
        }

        public int Pidc_horario_erm
        {
            get { return idc_horario_erm; }
            set { idc_horario_erm = value; }
        }

        public int Phora_entrada
        {
            get { return hora_entrada; }
            set { hora_entrada = value; }
        }

        public int Phora_salida
        {
            get { return hora_salida; }
            set { hora_salida = value; }
        }

        public int Phora_entrada_comida
        {
            get { return hora_entrada_comida; }
            set { hora_entrada_comida = value; }
        }

        public int Phora_salida_comida
        {
            get { return hora_salida_comida; }
            set { hora_salida_comida = value; }
        }

        public int Pidc_sucursal
        {
            get { return idc_sucursal; }
            set { idc_sucursal = value; }
        }

        public Boolean Pno_comida
        {
            get { return no_comida; }
            set { no_comida = value; }
        }

        public Boolean Pno_salida
        {
            get { return no_salida; }
            set { no_salida = value; }
        }
    }
}