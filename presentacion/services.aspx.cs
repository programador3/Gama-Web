using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;

namespace presentacion
{
    public partial class services : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            slack_pendientes();
        }

        private void slack_pendientes()
        {
            try
            {
                NotificacionesENT ent = new NotificacionesENT();
                NotificacionesCOM com = new NotificacionesCOM();
                DataTable dt = com.CargaSlack(ent).Tables[0];
                int t = 0;
                foreach (DataRow row in dt.Rows)
                {
                    t = t + 1;
                    string url = row["url"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "no" + t.ToString(), "$.ajax({type: 'POST',  url: '" + url + "'}); ", true);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
    }
}