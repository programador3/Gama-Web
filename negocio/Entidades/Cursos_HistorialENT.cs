using System.Data.SqlTypes;

namespace negocio.Entidades
{
    public class Cursos_HistorialENT
    {
        private int idc_curso_historial;
        private int idc_curso;
        private int idc_pre_empleado;
        private int idc_empleado;
        private int idc_puesto;
        private char estatus;
        private string aprobado;
        private string aprobado_jefe;
        private int resultado;
        private string observaciones;
        private bool empleado;
        private SqlDateTime fecha_tentativa;
        private string cad_curso_exam_archivo;
        private int cad_curso_exam_archivo_tot;
        private int idc_usuario;
        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;

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

        public int Idc_curso_historial
        {
            get { return idc_curso_historial; }
            set { idc_curso_historial = value; }
        }

        public int Idc_curso
        {
            get { return idc_curso; }
            set { idc_curso = value; }
        }

        public int Idc_pre_empleado
        {
            get { return idc_pre_empleado; }
            set { idc_pre_empleado = value; }
        }

        public int Idc_empleado
        {
            get { return idc_empleado; }
            set { idc_empleado = value; }
        }

        public int Idc_puesto
        {
            get { return idc_puesto; }
            set { idc_puesto = value; }
        }

        public char Estatus
        {
            get { return estatus; }
            set { estatus = value; }
        }

        public string Aprobado
        {
            get { return aprobado; }
            set { aprobado = value; }
        }

        public string Aprobado_jefe
        {
            get { return aprobado_jefe; }
            set { aprobado_jefe = value; }
        }

        public int Resultado
        {
            get { return resultado; }
            set { resultado = value; }
        }

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public bool Empleado
        {
            get { return empleado; }
            set { empleado = value; }
        }

        public SqlDateTime Fecha_tentativa
        {
            get { return fecha_tentativa; }
            set { fecha_tentativa = value; }
        }

        public string Cad_curso_exam_archivo
        {
            get { return cad_curso_exam_archivo; }
            set { cad_curso_exam_archivo = value; }
        }

        public int Cad_curso_exam_archivo_tot
        {
            get { return cad_curso_exam_archivo_tot; }
            set { cad_curso_exam_archivo_tot = value; }
        }

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }
    }
}