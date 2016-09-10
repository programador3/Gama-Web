using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace presentacion
{
    /// <summary>
    /// Descripción breve de WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [System.Web.Services.WebMethod(EnableSession = true)]
        public void SetSessionValue(List<string> aData)
        {
            String Value = aData[0];
            Session["date_noti"] = Value;
        }


        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetSessionValue()
        {
            string sessionVal = String.Empty;
            if (Session["date_noti"] != null)
            {
                sessionVal = Session["date_noti"].ToString();
            }
            return sessionVal;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public void SetSessionValueHTML(List<string> aData)
        {
            String Value = aData[0];
            Session["list_not"] = "";
            Session["list_not"] = Value;
        }



        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetSessionValueHTML()
        {
            string sessionVal = String.Empty;
            if (Session["list_not"] != null)
            {
                sessionVal = Session["list_not"].ToString();
            }
            return sessionVal;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public void SetSessionValueTotal(List<string> aData)
        {
            String Value = aData[0];
            Session["tot_not"] = Value;
        }



        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetSessionValueTotal()
        {
            string sessionVal = String.Empty;
            if (Session["tot_not"] != null)
            {
                sessionVal = Session["tot_not"].ToString();
            }
            return sessionVal;
        }
    }
}
