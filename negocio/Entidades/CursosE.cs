namespace negocio.Entidades
{
    public class CursosE
    {
        private int idc_curso;
        private int idc_curso_borr;
        private string descripcion;
        private bool borrador;

        private bool produccion;
        private int idc_usuario;

        //cadenas
        private string cad_curso_perfil;

        private int cad_curso_perfil_tot;
        private string cad_curso_archivo;
        private int cad_curso_archivo_tot;

        // borrador
        private string observaciones;

        private bool pendiente;
        private bool aprobado;

        private string pdirecip;
        private string pnombrepc;
        private string pusuariopc;

        //
        private char tipo_curso;

        private string cadena_examenes;
        private int total_cadena_examenes;

        public int Ptotal_examenes
        {
            get { return total_cadena_examenes; }
            set { total_cadena_examenes = value; }
        }

        public string Pcadena_examenes
        {
            get { return cadena_examenes; }
            set { cadena_examenes = value; }
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

        public int Idc_curso
        {
            get { return idc_curso; }
            set { idc_curso = value; }
        }

        public int Idc_curso_borr
        {
            get { return idc_curso_borr; }
            set { idc_curso_borr = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public bool Borrador
        {
            get { return borrador; }
            set { borrador = value; }
        }

        public bool Pendiente
        {
            get { return pendiente; }
            set { pendiente = value; }
        }

        public bool Produccion
        {
            get { return produccion; }
            set { produccion = value; }
        }

        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }

        public string Cad_curso_perfil
        {
            get { return cad_curso_perfil; }
            set { cad_curso_perfil = value; }
        }

        public int Cad_curso_perfil_tot
        {
            get { return cad_curso_perfil_tot; }
            set { cad_curso_perfil_tot = value; }
        }

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        public bool Aprobado
        {
            get { return aprobado; }
            set { aprobado = value; }
        }

        public string Cad_curso_archivo
        {
            get { return cad_curso_archivo; }
            set { cad_curso_archivo = value; }
        }

        public int Cad_curso_archivo_tot
        {
            get { return cad_curso_archivo_tot; }
            set { cad_curso_archivo_tot = value; }
        }

        public char Tipo_curso
        {
            get { return tipo_curso; }
            set { tipo_curso = value; }
        }
    }
}