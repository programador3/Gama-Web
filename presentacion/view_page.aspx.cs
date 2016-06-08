using System;

namespace presentacion
{
    public partial class view_page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["url"] == null)
            {
                Response.Redirect("menu.aspx");
            }
            else
            {
                string url = Request.QueryString["url"];
                url = funciones.ConvertHexToString(url);
                iframe.Attributes["src"] = url;
            }
        }
    }
}