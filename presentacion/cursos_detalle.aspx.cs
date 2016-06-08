using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class cursos_detalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //recibir valores de la url
                int uidc_curso;
                int uidc_curso_borr;
                if (Request.QueryString["uidc_curso"] == null)// si el request viene vacio iniciamos en borrador
                {
                    uidc_curso = 0;
                }
                else
                {
                    uidc_curso = Convert.ToInt32(Request.QueryString["uidc_curso"]);
                }

                if (Request.QueryString["uidc_curso_borr"] == null)// si el request viene vacio iniciamos en borrador
                {
                    uidc_curso_borr = 0;
                }
                else
                {
                    uidc_curso_borr = Convert.ToInt32(Request.QueryString["uidc_curso_borr"]);
                }

                cargar_info(uidc_curso);
                cargar_info_borr(uidc_curso_borr);

                if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_REFERER"]))
                {
                    oc_paginaprevia.Value = Request.ServerVariables["HTTP_REFERER"].ToString();
                }
                else
                {
                    oc_paginaprevia.Value = "menu.aspx";
                }

                //valida si tiene permiso de ver esta pagina//
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_opcion = 1810;
                if (funciones.permiso(idc_usuario, idc_opcion) == true)
                {
                    //muestrra el link
                    linkpendientes.Visible = true;
                }
                else
                {
                    linkpendientes.Visible = false;
                }
            }
        }

        protected void cargar_info(int idc_curso)
        {
            if (idc_curso > 0)
            {
                //llamar la entidad
                CursosE EntCurso = new CursosE();
                EntCurso.Idc_curso = idc_curso;
                EntCurso.Borrador = false;
                //ds
                DataSet ds = new DataSet();

                try
                {
                    //componente
                    CursosCOM ComCurso = new CursosCOM();
                    ds = ComCurso.cursos(EntCurso);
                    int total = ds.Tables[0].Rows.Count;

                    if (total == 0)
                    {
                        Alert.ShowAlertError("No se encontraron datos.", this.Page);
                    }
                    else
                    {
                        DataRow registro = ds.Tables[0].Rows[0];
                        //llenamos los controles
                        lbldesc.Text = registro["descripcion"].ToString();
                        lbltipocurso.Text = (registro["tipo_curso"].ToString() == "I") ? "Interno" : "Externo";
                        listaperfiles.DataSource = ds.Tables[1];
                        listaperfiles.DataTextField = "perfil";
                        listaperfiles.DataBind();
                        //archivos
                        repit_archivos.DataSource = ds.Tables[2];
                        repit_archivos.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Alert.ShowAlertError(ex.Message, this.Page);
                }
            }
            else
            {
                bool borrador = false;
                ocultar_columna(borrador); //false se refiere a ocultar produccion
            }
        }

        protected void cargar_info_borr(int idc_curso_borr)
        {
            if (idc_curso_borr > 0)
            {
                //TIENE PERMISO PARA PRODUCCION O NO 18-09-2015
                //valida si tiene permiso de ver esta pagina//
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_tipo_aut = 321;
                if (funciones.autorizacion(idc_usuario, idc_tipo_aut) == true)
                {
                    btneditarborrador.Visible = true;
                }
                else
                {
                    btneditarborrador.Visible = false;
                }
                //llamar la entidad
                CursosE EntCurso = new CursosE();
                EntCurso.Idc_curso_borr = idc_curso_borr;
                EntCurso.Borrador = true;
                //ds
                DataSet ds = new DataSet();

                try
                {
                    //componente
                    CursosCOM ComCurso = new CursosCOM();
                    ds = ComCurso.cursos(EntCurso);
                    int total = ds.Tables[0].Rows.Count;

                    if (total == 0)
                    {
                        Alert.ShowAlertError("No se encontraron datos.", this.Page);
                    }
                    else
                    {
                        DataRow registro = ds.Tables[0].Rows[0];
                        //llenamos los controles
                        lbldesc_borr.Text = registro["descripcion"].ToString();
                        lbltipocurso_borr.Text = (registro["tipo_curso"].ToString() == "I") ? "Interno" : "Externo";
                        listaperfiles_borr.DataSource = ds.Tables[1];
                        listaperfiles_borr.DataTextField = "perfil";
                        listaperfiles_borr.DataBind();
                        //archivos
                        repit_archivos_borr.DataSource = ds.Tables[2];
                        repit_archivos_borr.DataBind();
                        //observaciones
                        lblobservaciones_borr.Text = registro["observaciones"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Alert.ShowAlertError(ex.Message, this.Page);
                }
            }
            else
            {
                bool borrador = true;
                ocultar_columna(borrador); //true se refiere a ocultar borrador
            }
        }

        protected void repit_archivos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView dbr = (DataRowView)e.Item.DataItem;
                //CheckBoxList check = (CheckBoxList)e.Item.FindControl("check_etiqopc");
                Label lbldescripcion = (Label)e.Item.FindControl("lbldesc_archivo");
                lbldescripcion.Text = DataBinder.Eval(dbr, "descripcion").ToString();
                //imagebutton
                string dir_descarga = DataBinder.Eval(dbr, "dir_descarga").ToString();
                string id_curso_arc = DataBinder.Eval(dbr, "idc_curso_arc").ToString();
                string extension = DataBinder.Eval(dbr, "extension").ToString();
                string archivo = id_curso_arc + extension;
                ImageButton btndescarga = (ImageButton)e.Item.FindControl("imgbtn_linkdescarga");
                btndescarga.Attributes["ruta_descarga"] = dir_descarga + "\\" + archivo;
                btndescarga.Attributes["nombre_archivo"] = archivo;
            }
        }

        protected void repit_archivos_borr_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView dbr = (DataRowView)e.Item.DataItem;
                //CheckBoxList check = (CheckBoxList)e.Item.FindControl("check_etiqopc");
                Label lbldescripcion_borr = (Label)e.Item.FindControl("lbldesc_archivo_borr");
                lbldescripcion_borr.Text = DataBinder.Eval(dbr, "descripcion").ToString();
                //imagebutton
                string dir_descarga = DataBinder.Eval(dbr, "dir_descarga").ToString();
                string dir_descarga_borr = DataBinder.Eval(dbr, "dir_descarga_borr").ToString();
                string id_curso_arc = DataBinder.Eval(dbr, "idc_curso_arc").ToString();
                string id_curso_arc_borr = DataBinder.Eval(dbr, "idc_curso_arc_borr").ToString();
                string extension = DataBinder.Eval(dbr, "extension").ToString();
                string archivo;
                ImageButton btndescarga_borr = (ImageButton)e.Item.FindControl("imgbtn_linkdescarga_borr");
                if (Convert.ToInt32(id_curso_arc) > 0)
                {
                    archivo = id_curso_arc + extension;
                    btndescarga_borr.Attributes["ruta_descarga"] = dir_descarga + "\\" + archivo;
                    btndescarga_borr.Attributes["nombre_archivo"] = archivo;
                }
                else
                {
                    archivo = id_curso_arc_borr + extension;
                    btndescarga_borr.Attributes["ruta_descarga"] = dir_descarga_borr + "\\" + archivo;
                    btndescarga_borr.Attributes["nombre_archivo"] = archivo;
                }
            }
        }

        protected void DescargarArchivo(string path_archivo, string nombre_archivo)
        {
            // Limpiamos la salida
            Response.Clear();
            // Con esto le decimos al browser que la salida sera descargable
            Response.ContentType = "application/octet-stream";
            // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nombre_archivo);//Revision.pptx");
            // Escribimos el fichero a enviar
            Response.WriteFile(path_archivo);
            // volcamos el stream
            Response.Flush();
            // Enviamos todo el encabezado ahora
            Response.End();
        }

        protected void repit_archivos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //Button btn = (Button)e.Item.FindControl("btnmenugpo");
            //string titulo = btn.Text;
            //lblgrupotitulo.Text = titulo;
            ImageButton btndescarga = (ImageButton)e.Item.FindControl("imgbtn_linkdescarga");
            string url = btndescarga.Attributes["ruta_descarga"].ToString();
            string nombre_archivo = btndescarga.Attributes["nombre_archivo"].ToString();
            DescargarArchivo(url, nombre_archivo);
        }

        protected void repit_archivos_borr_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ImageButton btndescarga_borr = (ImageButton)e.Item.FindControl("imgbtn_linkdescarga_borr");
            string url = btndescarga_borr.Attributes["ruta_descarga"].ToString();
            string nombre_archivo = btndescarga_borr.Attributes["nombre_archivo"].ToString();
            DescargarArchivo(url, nombre_archivo);
        }

        protected void ocultar_columna(bool borrador)
        {
            if (borrador)
            { //oculta borrador
                panel_borr_encabezado.Visible = false;
                panel_borr_cursos.Visible = false;
                panel_borr_perfiles.Visible = false;
                panel_borr_archivos.Visible = false;
                panel_borr_obs.Visible = false;
            }
            else
            { //oculta produccion
                panel_prod_encabezado.Visible = false;
                panel_prod_cursos.Visible = false;
                panel_prod_perfiles.Visible = false;
                panel_prod_archivos.Visible = false;
                panel_prod_obs.Visible = false;
            }
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(oc_paginaprevia.Value);
        }

        protected void btneditarborrador_Click(object sender, ImageClickEventArgs e)
        {
            int idc_curso_borr;
            if (Request.QueryString["uidc_curso_borr"] == null)// si el request viene vacio iniciamos en borrador
            {
                idc_curso_borr = 0;
            }
            else
            {
                idc_curso_borr = Convert.ToInt32(Request.QueryString["uidc_curso_borr"]);
            }

            if (idc_curso_borr > 0)
            {
                Response.Redirect("cursos_captura.aspx?borrador=1&uidc_curso_borr=" + idc_curso_borr);
            }
        }
    }
}