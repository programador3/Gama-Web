using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using negocio.Componentes;
using negocio.Entidades;

namespace presentacion
{
    public partial class prospectos_datos2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["sidc_prospecto"] = null;
                if (Session["sidc_usuario"] == null)
                {
                    Response.Redirect("login.aspx");
                    return;
                }
                //valida si tiene permiso de ver esta pagina//
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
               
              
                //cargar datos de la tabla

                DataSet ds = new DataSet();
                prospectos_ventasE llenar_datos = new prospectos_ventasE();
                Prospectos_ventasBL datos = new Prospectos_ventasBL();

                llenar_datos.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                //fecha al dia de hoy 
                DateTime hoy = DateTime.Today;

                txtfecha1.Text = hoy.AddDays(-15).ToString("yyyy-MM-dd");
                txtfecha2.Text = hoy.ToString("yyyy-MM-dd");
                try
                {
                    ds = datos.datos_prospectos_ventas(llenar_datos);
                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                }
                catch (Exception ex)
                {
                    msgbox.show(ex.Message, this.Page);
                }


            }

        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            //ahora 22-05-2015
            //de aqui mando un parametro en session
            Session["sidc_prospecto"] = 0;
            //y a donde regresar cuando de clic en cancelar osea aqui
            string url = HttpContext.Current.Request.Url.LocalPath;
            Session["spagina"] = url;
            OpcionesE llenar_opciones = new OpcionesE();
            int idc_opcion =1778; //captura de prospectos
            llenar_opciones.Idc_opcion = idc_opcion;
            llenar_opciones.Idc_user = Convert.ToInt32(Session["sidc_usuario"].ToString());
           //componente
            OpcionesBL opcs = new OpcionesBL();
            string destino = opcs.ir(llenar_opciones);
            Response.Redirect(destino); //redireccion
            //antes
            //Session["spagina"] = "prospectos_datos2.aspx";
            //Session["sidc_prospecto"] = 0;
            //Response.Redirect("prospectos_captura2.aspx");
        }

        protected void imgmenu_Click(object sender, ImageClickEventArgs e)
        {

        }

        


        protected void GridView1_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            //me causaba error cuando usaba el link ver..
            //recuperamos el indice del registro seleccionado del grid de Telefono
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(GridView1.DataKeys[index].Value);
            string idc = funciones.deTextoa64(Convert.ToInt32(vidc.ToString().Trim()).ToString());
            switch (e.CommandName)
            {
                case "editar":
                    //ahora 22-05-2015
                    
                    //y a donde regresar cuando de clic en cancelar osea aqui
                    string url = HttpContext.Current.Request.Url.LocalPath;
                    Session["spagina"] = url;
                    OpcionesE llenar_opciones = new OpcionesE();
                    int idc_opcion = 1778; //captura de prospectos
                    llenar_opciones.Idc_opcion = idc_opcion;
                    llenar_opciones.Idc_user = Convert.ToInt32(Session["sidc_usuario"].ToString());
                    //componente
                    OpcionesBL opcs = new OpcionesBL();
                    string destino = opcs.ir(llenar_opciones);
                    Response.Redirect(destino+"?sidc_prospecto="+idc); //redireccion
                    //antes
                    //Session["sidc_prospecto"] = Convert.ToInt32(vidc.ToString().Trim());
                    //Session["spagina"] = "prospectos_datos2.aspx";
                    //Response.Redirect("prospectos_captura2.aspx");
                break;
                case "detalle":
                    string  pagina;
                    pagina = "prospectos_detalle.aspx?sidc_prospecto=" + idc;
                    Response.Redirect(pagina);
                    
                   
                break;
            }

        }

        protected void btnreporte_Click(object sender, EventArgs e)
        {
            
            string url = HttpContext.Current.Request.Url.LocalPath;
            Session["spagina"] = url;
            //redireccionamos a reporte_filtro.aspx
            Response.Redirect("reporte_filtro.aspx");

        }

        protected void btnregresar_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void btnfiltrar_Click(object sender, EventArgs e)
        {

            if (funciones.EsFecha(txtfecha1.Text) == false || funciones.EsFecha(txtfecha2.Text) == false)
            {
                msgbox.show("Solo se aceptan fechas validas.", this.Page);
                return;
            }
            //cargar datos de la tabla

            DataSet ds = new DataSet();
            prospectos_ventasE llenar_datos = new prospectos_ventasE();
            Prospectos_ventasBL datos = new Prospectos_ventasBL();

            llenar_datos.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            llenar_datos.Fechai = txtfecha1.Text.Replace("-","");
            llenar_datos.Fechaf = txtfecha2.Text.Replace("-","");
            try
            {
                ds = datos.datos_prospectos_ventas(llenar_datos);
                GridView1.DataSource = ds;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message, this.Page);
            }
        }
    }
}