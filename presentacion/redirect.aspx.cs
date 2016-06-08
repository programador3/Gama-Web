using System;

namespace presentacion
{
    public partial class redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //esta página fue creada para hacer la solicitud a una pagina como si fuera tecleada desde el navegador
            string url = Request.QueryString["url"];
            Response.Redirect(url);
        }
    }
}