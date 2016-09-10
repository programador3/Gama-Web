using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio.Entidades
{
    public class prospectos_ventasE
    {
        int idc_prospecto;
        string direccion;
        string nombre_razon_social;
        string contacto;
        string telefono;
        string tipo_obra;
        string correo;
        string tamaño_obra;
        string etapa_obra;
        string observacion;
        int idc_usuario;
        int totalobras;
        string masobras;
        //new 20-04-2015
        string cad_con;
	    int cad_con_tot;
	    string cad_tel;
	    int cad_tel_tot;
	    string cad_cor;
        int cad_cor_tot;
        string phost;
        //new 29-04-2015
        decimal latitud;
        decimal longitud;
        //new 01-10-2015
        string cadena_famartdet;
        int cadena_famartdet_total;
        //
        string cadena_famartdet_marca;
        int cadena_famartdet_marca_total;
        //
        int idc_giroc;
        //
        int idc_tipoobra;
        //
        int idc_etapaobra;
        //
        string fechai;
        string fechaf;
        public int Idc_prospecto 
        {
            get { return idc_prospecto; }
            set { idc_prospecto = value; }
        }
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        public string Nombre_razon_social
        {
            get { return nombre_razon_social; }
            set { nombre_razon_social = value; }
        }
        public string Contacto
        {
            get { return contacto; }
            set { contacto = value; }
        }
        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        public string Tipo_obra
        {
            get { return tipo_obra; }
            set { tipo_obra = value; }
        }
        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }
        public string Tamaño_obra
        {
            get { return tamaño_obra; }
            set { tamaño_obra = value; }
        }
        public string Etapa_obra
        {
            get { return etapa_obra; }
            set { etapa_obra = value; }
        }
        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }
        public int Idc_usuario
        {
            get { return idc_usuario; }
            set { idc_usuario = value; }
        }
        public int Totalobras
        {
            get { return totalobras; }
            set { totalobras = value; }
        }
        public string Masobras
        {
            get { return masobras; }
            set { masobras = value; }
        }
        //new 20-04-2015
        public string P_host {
            get { return phost; }
            set { phost = value; }
        }
        public string Cad_con {
            get { return cad_con; }
            set { cad_con = value; }
        }
        public int Cad_con_tot{

            get { return cad_con_tot; }
            set { cad_con_tot = value; }
        }
        public string Cad_tel{

            get { return cad_tel; }
            set { cad_tel = value; }
        }
        public int Cad_tel_tot{

            get { return cad_tel_tot; }
            set { cad_tel_tot = value; }
        }
        public string Cad_cor{

            get { return cad_cor; }
            set { cad_cor = value; }
        }
        public int Cad_cor_tot
        {

            get { return cad_cor_tot; }
            set { cad_cor_tot = value; }
        }
        public decimal Latitud {
            get { return latitud; }
            set { latitud = value; }
        }
        public decimal Longitud {
            get { return longitud; }
            set { longitud = value; }
        }

        // nuevo 01-10-2015

        public string Cadena_famartdet
        {

            get { return cadena_famartdet; }
            set { cadena_famartdet = value; }
        }

        public int Cadena_famartdet_total
        {

            get { return cadena_famartdet_total; }
            set { cadena_famartdet_total = value; }
        }
        //
        public string Cadena_famartdet_marca
        {

            get { return cadena_famartdet_marca; }
            set { cadena_famartdet_marca = value; }
        }


        public int Cadena_famartdet_marca_total
        {

            get { return cadena_famartdet_marca_total; }
            set { cadena_famartdet_marca_total = value; }
        }


        public int Idc_giroc
        {

            get { return idc_giroc; }
            set { idc_giroc = value; }
        }


        public int Idc_tipoobra
        {

            get { return idc_tipoobra; }
            set { idc_tipoobra = value; }
        }


        public int Idc_etapaobra
        {

            get { return idc_etapaobra; }
            set { idc_etapaobra = value; }
        }

        public string Fechai
        {
            get { return fechai; }
            set { fechai = value; }
        }
        public string Fechaf
        {
            get { return fechaf; }
            set { fechaf = value; }
        }

    }
}
