using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class IMG_Croquis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string id_croquis = funciones.de64aTexto(Request.QueryString["croquis"]);
                int tipo = Convert.ToInt16(Request.QueryString["tipo"]);
                if (tipo == 1)
                {
                    cargar_croquis(id_croquis);
                }
                else if (tipo == 2)
                {
                    cargar_croquis_uni_archiv(id_croquis);
                }

            }
        }
        public void cargar_croquis_uni_archiv(string croquis)
        {
            string[] ext = new string[4];
            bool existe = false;
            ext[0] = ".JPG";
            ext[1] = ".GIF";
            ext[2] = ".DIB";
            ext[3] = ".BMP";
            for (int i = 0; i <= ext.Length - 1; i++)
            {
                string ruta = funciones.GenerarRuta("proye", "UNIDAD");
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/files/"));
                if (File.Exists(ruta + croquis + ext[i]))
                {
                    funciones.CopiarArchivos(ruta + croquis + ext[i], dirInfo + croquis + ext[i],this);
                    imgcroquis.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["server"] + "temp/files/" + croquis + ext[i];
                    existe = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            if (existe == false)
            {
                Response.Write("<Script> alert('El croquis no existe.'); </script>");
                Response.Write("<Script>window.close();</script>");
            }
        }
        public void cargar_croquis(string croquis)
        {
            string[] ext = new string[4];
            bool existe = false;
            ext[0] = ".JPG";
            ext[1] = ".GIF";
            ext[2] = ".DIB";
            ext[3] = ".BMP";
            for (int i = 0; i <= ext.Length - 1; i++)
            {
                
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/files/"));
                if (File.Exists(dirInfo.ToString() + croquis + ext[i]))
                {
                    imgcroquis.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["server"] +"temp/files/" + croquis + ext[i];
                    existe = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            if (existe == false)
            {
                Response.Write("<Script> alert('El croquis no existe.'); </script>");
                Response.Write("<Script>window.close();</script>");
            }
        }
    }
}