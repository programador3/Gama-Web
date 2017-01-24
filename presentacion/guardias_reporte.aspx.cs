using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class guardias_reporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["dt_guardias_rep"] = null;
            }
        }

        protected void lnkejecutarreporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfecha.Text == "")
                {
                    Alert.ShowAlertError("SELECCIONE UNA FECHA", this);
                }
                else {
                    TareasCOM componente = new TareasCOM();
                    DateTime fecha = Convert.ToDateTime(txtfecha.Text);
                    DataSet ds = componente.sp_reporte_guardias(fecha);
                    ViewState["dt_guardias_rep"] = null;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["dt_guardias_rep"] = ds.Tables[0];
                        grid_reporte.DataSource = ds.Tables[0];
                        grid_reporte.DataBind();
                    }
                    else
                    {
                        Alert.ShowAlertError("LA BUSQUEDA NO ENCONTRO RESULTADOS. \\nPUEDE INTENTARLO CON OTRA FECHA.", this);
                    }
                }
            }
            catch (Exception EX)
            {
                Alert.ShowAlertError(EX.ToString(), this);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt_guardias_rep"];
            try
            {
                string attachment = "attachment; filename=guardias_reporte.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel;";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }
    }
}