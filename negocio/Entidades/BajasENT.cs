using System.Data.SqlTypes;

namespace negocio.Entidades
{
    public class BajasENT
    {
        private int pidc_empleado;
        private SqlDateTime fecha;
        private int pidc_usuario;
        private string ip;
        private string nombrepc;
        private string usuariopc;
        private string motivo;
        private int pidc_prebaja;
        private bool contratar;
        private int idc_puesto;
        private bool capacitacion;
        private int idc_cheque;

        public int Pidc_cheque
        {
            get { return idc_cheque; }
            set { idc_cheque = value; }
        }

        public bool Capacitacion
        {
            get { return capacitacion; }
            set { capacitacion = value; }
        }

        public SqlDateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public string Nombrepc
        {
            get { return nombrepc; }
            set { nombrepc = value; }
        }

        public string Usuariopc
        {
            get { return usuariopc; }
            set { usuariopc = value; }
        }

        public string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }

        public int Pidc_empleado
        {
            get { return pidc_empleado; }
            set { pidc_empleado = value; }
        }

        public int Pidc_usuario
        {
            get { return pidc_usuario; }
            set { pidc_usuario = value; }
        }

        public int Pidc_prebaja
        {
            get { return pidc_prebaja; }
            set { pidc_prebaja = value; }
        }

        public bool Contratar
        {
            get { return contratar; }
            set { contratar = value; }
        }

        public int Idc_puesto
        {
            get { return idc_puesto; }
            set { idc_puesto = value; }
        }
    }
}