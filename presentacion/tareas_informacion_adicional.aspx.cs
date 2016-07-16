using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class tareas_informacion_adicional : System.Web.UI.Page
    {
        public int contador = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            { CargarTareas(); }
        }

        private void CargarTareas()
        {
            try
            {
                TareasENT entidad = new TareasENT();
                TareasCOM componente = new TareasCOM();
                entidad.Pidc_tipoi = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_tipoi"]));
                entidad.Pidc_proceso = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_proceso"]));
                DataSet ds = componente.InformacionAdicional(entidad);
                Session["datasetiad"] = ds;
                DataTable tbl = new DataTable();
                tbl.Columns.Add("number");
                int i = 0;
                foreach (DataTable dt in ds.Tables)
                {
                    DataRow row = tbl.NewRow();
                    row["number"] = i;
                    tbl.Rows.Add(row);
                    i++;
                }
                repeat.DataSource = tbl;
                repeat.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void repeat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            GridView grid = (GridView)e.Item.FindControl("grid");
            Label lbltextname = (Label)e.Item.FindControl("lbltextname");
            Label lblname = (Label)e.Item.FindControl("lblgridname");
            TextBox txt = (TextBox)e.Item.FindControl("txtdesc");
            Panel gridview = (Panel)e.Item.FindControl("gridview");
            Panel textbox = (Panel)e.Item.FindControl("textbox");
            HiddenField cursor = (HiddenField)e.Item.FindControl("cursornumber");
            DataSet ds = (DataSet)Session["datasetiad"];
            DataTable dt = ds.Tables[contador];
            if (dt.Rows.Count > 0)
            {
                string type_cursor = dt.Rows[0]["type_cursor"].ToString();
                string title = dt.Rows[0]["name_table"].ToString();
                switch (type_cursor)
                {
                    case "textbox":
                        textbox.Visible = true;
                        lbltextname.Text = dt.Rows[0]["name_table"].ToString();
                        txt.Text = dt.Rows[0]["contenido"].ToString();
                        break;

                    case "gridview":
                        gridview.Visible = true;
                        //copiamos tabla para eliminar columnas
                        DataTable copyDataTable;
                        copyDataTable = dt.Copy();
                        //eliminamos columnas inecesarias, al eliminar la 0, la 1 se convierte en 0
                        copyDataTable.Columns.RemoveAt(0);// nombre de tabla
                        copyDataTable.Columns.RemoveAt(0);// tipo de cursor
                        lblname.Text = dt.Rows[0]["name_table"].ToString();
                        grid.DataSource = copyDataTable;
                        grid.DataBind();
                        cursor.Value = contador.ToString();
                        break;
                }
            }

            contador = contador + 1;
        }

        protected void lnkexport_Click(object sender, EventArgs e)
        {
            LinkButton send = sender as LinkButton;
            foreach (RepeaterItem item in repeat.Items)
            {
                LinkButton lnk = (LinkButton)item.FindControl("lnkexport");
                HiddenField cursor = (HiddenField)item.FindControl("cursornumber");
                Label lblname = (Label)item.FindControl("lblgridname");
                if (lnk == send)
                {
                    DataSet ds = (DataSet)Session["datasetiad"];
                    DataTable dt = ds.Tables[Convert.ToInt32(cursor.Value)];
                    dt.Columns.RemoveAt(0);// nombre de tabla
                    dt.Columns.RemoveAt(0);// tipo de cursor
                    Export Export = new Export();
                    //array de DataTables
                    List<DataTable> ListaTables = new List<DataTable>();
                    ListaTables.Add(dt);
                    //array de nombre de sheets
                    string[] Nombres = new string[] { lblname.Text };
                    if (dt.Rows.Count == 0)
                    {
                        Alert.ShowAlertInfo("Este Puesto no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
                    }
                    else
                    {
                        string mensaje = Export.toExcel(lblname.Text, XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                                           XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 1,
                                           "ex" + cursor.Value + ".xlsx", Page.Response);
                        if (mensaje != "")
                        {
                            Alert.ShowAlertError(mensaje, this);
                        }
                    }
                }
            }
        }
    }
}