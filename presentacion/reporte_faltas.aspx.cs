using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class reporte_faltas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                txtfechafin.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                txtfechainicio.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }
        }

        private void CargarDatos(int idc_empleado)
        {
            try
            {
                PuestosENT entidad = new PuestosENT();
                PuestosCOM componente = new PuestosCOM();
                entidad.Pfecha_inicio = Convert.ToDateTime(txtfechainicio.Text);
                entidad.Pidc_empleado_pmd = idc_empleado;
                entidad.Pfecha_fin = Convert.ToDateTime(txtfechafin.Text);
                DataSet ds = componente.CargaFaltas(entidad);
                grid_faltas.DataSource = ds.Tables[0];
                grid_faltas.DataBind();
                row_princ.Visible = true;
                Session["tabla_faltas_empleados"] = ds.Tables[0];
                Session["tabla_faltas_empleados_det"] = ds.Tables[1];
                row_det.Visible = idc_empleado > 0 ? true : false;
                grid_detalles.DataSource = ds.Tables[1];
                grid_detalles.DataBind();
                Session["faltas_det"] = ds.Tables[1];
                Session["faltas"] = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void lnkexcel_Click(object sender, EventArgs e)
        {
            Export Export = new Export();
            //array de DataTables
            List<DataTable> ListaTables = new List<DataTable>();
            DataTable dt = (DataTable)Session["faltas"];
            dt.Columns.Remove("idc_empleado");
            dt.Columns.Remove("activo");
            DataTable dt2 = (DataTable)Session["faltas_det"];
            dt2.Columns.Remove("idc_empleado");
            ListaTables.Add(dt);
            ListaTables.Add(dt2);
            string mensaje = "";
            string[] Nombres = new string[] { "Faltas", "Detalles de Faltas" };
            mensaje = Export.toExcel("Reporte de Faltas por Empleado", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")), XLColor.White,
                              XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 2,
                              "Listado_de_Faltas.xlsx", Page.Response);

            if (mensaje != "")
            {
                Alert.ShowAlertError(mensaje, this);
            }
        }

        protected void lnkfiltro_Click(object sender, EventArgs e)
        {
            if (txtfechafin.Text == "" || txtfechainicio.Text == "")
            {
                Alert.ShowAlertError("Ingrese un rango de fecha valido", this);
            }
            else
            {
                lnkexcel.Visible = true;
                CargarDatos(0);
            }
        }

        protected void grid_faltas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_empleado = Convert.ToInt32(grid_faltas.DataKeys[index].Values["idc_empleado"].ToString());
            if (txtfechafin.Text == "" || txtfechainicio.Text == "")
            {
                Alert.ShowAlertError("Ingrese un rango de fecha valido", this);
            }
            else
            {
                lnkexcel.Visible = false;
                CargarDatos(idc_empleado);
            }
        }

        protected void grid_detalles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            string path = grid_detalles.DataKeys[index].Values["justificante"].ToString();
            string file_name = Path.GetFileName(path);
            if (!File.Exists(path) || path == "")
            {
                Alert.ShowAlertError("No tiene archivo relacionado", this);
            }
            else
            {
                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file_name);
                // Escribimos el fichero a enviar
                Response.WriteFile(path);
                // volcamos el stream
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
                // Response.End();
            }
        }

        protected void grid_detalles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                if (rowView["justificante"].ToString() == "")//si es 0 SIGNIFICA QUE ES NUEVO, MUESTRO ETIQUETA CORRESPONDIENTE
                {
                    //nuevo_registro.ImageUrl = "imagenes/btn/new_register.png";
                    e.Row.Cells[5].Controls.Clear();
                }
            }
        }

        protected void grid_faltas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Image activo = (Image)e.Row.FindControl("activo");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string usuario_value = rowView["activo"].ToString();
                activo.ImageUrl = usuario_value;
            }
        }
    }
}