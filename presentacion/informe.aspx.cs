using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio.Componentes;
namespace presentacion
{
    public partial class informe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["sidc_usuario"] == null)
                {
                    Response.Redirect("login.aspx");
                    return;
                }
                //valida si tiene permiso de ver esta pagina//
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
               
                try
                {
                   string indice = Request.QueryString["indice"];
                    ReportViewer2.SizeToReportContent = true;
                    //ReportViewer1.BackColor = System.Drawing.Color.Gainsboro;
                    ReportViewer2.ProcessingMode = ProcessingMode.Remote;
                    ServerReport serverReport = ReportViewer2.ServerReport;

                    serverReport.ReportServerUrl = new Uri(variables.reporting_services);
                    serverReport.ReportPath = (string)Session["reportpath"];

                    List<ReportParameter> parametros = new List<ReportParameter>();
                    
                    parametros = (List<ReportParameter>)Session[indice];

                    //string cs = System.Configuration.ConfigurationManager.AppSettings["cs"];
                    //string cadena;
                    //if (cs == "P")
                    //    cadena = variables.cad_conexion;
                    //else
                    //    cadena = variables.cad_conexion_respa;

                    //if(parametros.Exists(x=>x.Name=="cadconexion")){ //esta validacion se aplica porque cuando se abre el informe y se le da refresh al mismo duplica este parametro.
                    //    parametros.Add(new ReportParameter("cadconexion", cadena));
                    //}
                    ReportViewer2.ServerReport.SetParameters(parametros);
                    ReportViewer2.ServerReport.Refresh();

                }
                catch (Exception ex)
                {
                    //throw ex;
                    //  Console.Write(ex.Message);
                    msgbox.show(ex.Message, this.Page);
                    
                }

            }
        }
    }
}