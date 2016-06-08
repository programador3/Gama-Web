using System;

namespace presentacion
{
    public partial class page_error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Error_Mensaje"] == null)
            {
                lblerror.Text = "Hubo un error desconocido o la pagina que busca ya no se encuentra.";
            }
            else
            {
                lblerror.Text = (string)Session["Error_Mensaje"];
            }
        }
    }
}