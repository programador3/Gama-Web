using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class avisos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            int sidc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
            if (!IsPostBack)
                CargarGridPrincipal(sidc_usuario);
        }

        /// <summary>
        /// Carga los datos en repeats
        /// </summary>
        /// <param name="idc_usuario"></param>
        /// <param name="idc_puestorevi"></param>
        /// <param name="idc_puestoprebaja"></param>
        public void CargarGridPrincipal(int idc_usuario)
        {
            try
            {
                AvisosENT entidad = new AvisosENT();
                AvisosCOM componente = new AvisosCOM();
                entidad.Pidc_puesto = idc_usuario;
                DataSet ds = componente.CargaAvisos(entidad);
                repeat_avisos.DataSource = ds;
                repeat_avisos.DataBind();
                Session["tabla_contenido"] = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
                Session["Error_Mensaje"] = ex.ToString();
            }
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }
        
      
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            int idc_aviso = Convert.ToInt32(lnk.CommandName);
            DataTable dt = (DataTable)Session["tabla_contenido"];
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt32(row["idc_aviso"]) == idc_aviso)
                {
                    lblNombre.Text = (row["nombre"].ToString());
                    lblFecha.Text = " " + (row["fecha_form"].ToString());
                    string contenido = "";
                    contenido = row["descripcion"].ToString();
                    if (Convert.ToString(row["descripcion"]) == "")
                    {

                        string ruta = funciones.GenerarRuta("AVIHTML", "UNIDAD");
                        ruta = ruta + row["idc_aviso"].ToString().Trim() + ".html";
                        contenido = funciones.ContenidoArchivo(ruta);
                    }
                    plccontenido.Controls.Add(new Literal { Text = contenido });
                    break;
                }
            }
            repeat_avisos.DataSource = dt;
            repeat_avisos.DataBind();
            int sidc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
            ScriptManager.RegisterStartupScript(this, GetType(), "preherr", "ModalPreviewHeramienta();", true);
        }

        /// <summary>
        /// Genera ruta de imagen
        /// </summary>
        public string GenerarRuta(int id_comprobar, string codigo_imagen)
        {
            string ret = "";
            var Entidad = new UsuariosE();
            Entidad.Cod_arch = codigo_imagen;
            var Componente = new OrganigramaBL();
            var ds = new DataSet();
            ds = Componente.CargaPath(Entidad);
            if (ds.Tables[0].Rows.Count != 0)
            {
                var tablaGrupos = new DataTable();
                //Paso dataset ala tabla
                tablaGrupos = ds.Tables[0];
                var row = tablaGrupos.Rows[0];
                var carpeta = row["rw_carpeta"].ToString();
                var domn = Request.Url.Host;
                switch (codigo_imagen)
                {
                    case "fot_emp"://fotos de empleados
                        if (domn == "localhost")
                        {
                            var url = "imagenes/btn/default_employed.png";
                            ret = url;
                            ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                        }
                        else
                        {
                            var url = "http://" + domn + carpeta + id_comprobar + ".jpg";
                            ret = url;
                            ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                        }
                        break;
                }
            }
            return ret;
        }

        protected void repeat_avisos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            int idc_empleado = Convert.ToInt32(DataBinder.Eval(dbr, "idc_empleado"));
            Image img = (Image)e.Item.FindControl("img_profile");
            img.ImageUrl = GenerarRuta(idc_empleado, "fot_emp");
        }
    }
}