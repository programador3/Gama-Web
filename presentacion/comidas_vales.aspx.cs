using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class comidas_vales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ViewState["dt_comidas"] = null;
        }

        protected void lnk_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuparchivos.HasFile)
                {
                    grid_comidas.DataSource = null;
                    grid_comidas.DataBind();
                    string randomNumber = Convert.ToInt32(Session["sidc_usuario"]).ToString();
                    randomNumber = randomNumber + "_";
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/excel/"));//path local
                    string name = dirInfo + randomNumber.Trim() + date + Path.GetExtension(fuparchivos.FileName);
                    funciones.UploadFile(fuparchivos, name.Trim(), this.Page);
                    DataTable dt = SpreadsheetDocument.Open(name, false).GetDataTableFromSpreadSheet(2);
                    if (dt.Rows.Count == 0)
                    {
                        Alert.ShowAlertError("EL ARCHIVO DE EXCEL NO PUDO SER LEIDO. UTILIZE EL FORMATO CORRECTO.", this);
                    }
                    else if (!dt.Columns.Contains("NOMINA") 
                        || !dt.Columns.Contains("DESCUENTO")) {
                        Alert.ShowAlertError("EL ARCHIVO DE EXCEL NO PUDO SER LEIDO. UTILIZE EL FORMATO CORRECTO.", this);
                    }
                    else {
                        foreach (DataRow row in dt.Rows)
                        {
                            string desc = row["DESCUENTO"].ToString();
                            row["DESCUENTO"] = desc == "" ? 0 : row["DESCUENTO"];
                        }
                        ViewState["dt_comidas"] = dt;
                        CargarGrid("");
                        tabla.Visible = true;
                        tabla_errores.Visible = false;
                        GenerarTotal();
                    }
                }
                else
                {
                    Alert.ShowAlertError("SELECCIONE UN ARCHIVO DE EXCEL", this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        private void CargarGrid(string filtro)
        {
            DataTable dt1 = ViewState["dt_comidas"] as DataTable;
            DataTable dt = new DataTable();
            if (filtro == "")
            {
                dt = dt1;
            }
            else {
                DataView dv = dt1.DefaultView;
                dv.RowFilter = "NOMINA LIKE '%"+filtro+ "%'";
                dt = dv.ToTable();
            }
            DataTable dtCloned = dt.Clone();
            dtCloned.Columns[0].DataType = typeof(Int32);
            foreach (DataRow row in dt.Rows)
            {
                dtCloned.ImportRow(row);
            }
            DataView dv2 = dtCloned.DefaultView;
            dv2.Sort = "NOMINA asc";
            DataTable sortedDT = dv2.ToTable();
            grid_comidas.DataSource = sortedDT;
            grid_comidas.DataBind();
        }
        private void GenerarTotal()
        {
            try
            {
                DataTable dt = ViewState["dt_comidas"] as DataTable;
                int total = 0;
                foreach (DataRow row in dt.Rows)
                { 
                    string desc = row["DESCUENTO"].ToString();
                    total = total + (desc == ""?0:Convert.ToInt32(desc));
                }
                lbltotal.Text ="$ "+ total.ToString();
                
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        private int GenerarTotalPorDias(string dia)
        {
            int total = 0;
            try
            {
                DataTable dt1 = ViewState["dt_comidas"] as DataTable;
                string[] selectedColumns = new[] { dia };

                DataTable dt = new DataView(dt1).ToTable(false, selectedColumns);
                foreach (DataRow row in dt.Rows)
                {
                    total = total + (row[dia].ToString() == "" ? 0 : Convert.ToInt32(row[dia]));
                }

                return total;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
                return 0;
            }
        }

        private String Cadena()
        {
            try
            {
                string ret = "";
                DataTable dt1 = ViewState["dt_comidas"] as DataTable;
                DataView dv = dt1.DefaultView;
                dv.RowFilter = "DESCUENTO > 0";
                DataTable dt = dv.ToTable();
                foreach (DataRow row in dt.Rows)
                {
                    ret =ret + row["NOMINA"].ToString() + ";" + row["DESCUENTO"].ToString() + ";";
                }
                return ret;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(),this);
                return "";
            }
        }
        private int TotalCadena()
        {
            try
            {
                DataTable dt1 = ViewState["dt_comidas"] as DataTable;
                DataView dv = dt1.DefaultView;
                dv.RowFilter = "DESCUENTO > 0";
                DataTable dt = dv.ToTable();
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
                return 0;
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/"));//path local
            string ruta = dirInfo + "EJEMPLO_VALES_COMIDA.xlsx";
            funciones.Download(ruta, Path.GetFileName(ruta), this);
        }

        protected void lnksubir_Click(object sender, EventArgs e)
        {
            pintar();
            if (TotalCadena() == 0)
            {
                Alert.ShowAlertError("NO SE HA ENCONTRADO NINGUN DATO. INTENTE CARGAR EL ARCHIVO DE EXCEL NUEVAMENTE", this);
            }
            else {
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ModalConfirm('Mensaje del Sistema','¿Desea Generar los Vales Correspondientes?','modal fade modal-info');", true);
            }
        }
        private void pintar() {
            foreach (GridViewRow gridrow in grid_comidas.Rows)
            {
                int totalt = Convert.ToInt32(gridrow.Cells[1].Text);
                gridrow.BackColor = totalt > 0 ? System.Drawing.Color.FromName("#ffffff") : System.Drawing.Color.FromName("#ffcdd2");
            }
        }
        protected void lnklimpirar_Click(object sender, EventArgs e)
        {
            Response.Redirect("comidas_vales.aspx");
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                tabla_errores.Visible = false;
                   comidasCOM componente = new comidasCOM();
                DataSet ds;
                ds = componente.sp_importar_vales_comida(Cadena(), TotalCadena(), Convert.ToInt32(Session["sidc_usuario"]));

                string vmensaje = "";
                vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (ds.Tables.Count > 1)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        tabla.Visible = false;
                        tabla_errores.Visible = true;
                        grid_errores.DataSource = ds.Tables[1];
                        grid_errores.DataBind();
                        vmensaje = "SE GENERARON ERRORES EN ALGUNOS VALES, PERO LOS DEMAS SE GUARDARON CORRECTAMENTE";
                    }
                }
                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos Generando "+TotalCadena().ToString()+" VALES DE COMIDA.", "Espere un Momento", "comidas_vales.aspx",
                               "imagenes/loading.gif", "3000", TotalCadena().ToString() + " Vales Generados Correctamente. ", this);
                }
                else {

                    Alert.ShowAlertError(vmensaje, this.Page);
                }
              
                
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void grid_errores_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int num_nomina = Convert.ToInt32(grid_comidas.DataKeys[index].Values["# EMPLEADO"]);
            //DataRow row = row_empleado(num_nomina);
            //ImageButton imgbutton = (ImageButton)e.CommandSource;
            //string value_APLICA = "";
            //int total = Convert.ToInt32(row["DESCUENTO"]);
            //string ImageUrl = "";
            //imgbutton.ImageUrl = imgbutton.ImageUrl== "imagenes/btn/inchecked.png" ? "imagenes/btn/checked.png" : "imagenes/btn/inchecked.png";
            //ImageUrl = imgbutton.ImageUrl;
            //total = ImageUrl == "imagenes/btn/inchecked.png" ? (total - 25) : (total + 25);
            //value_APLICA = ImageUrl == "imagenes/btn/inchecked.png" ? "" : "1";            
            //grid_comidas.Rows[index].Cells[9].Text=total.ToString();
            //ChangeValues(num_nomina,value_APLICA,total,e.CommandName);
            //GenerarTotal();
            //grid_comidas.Rows[index].BackColor = total >  0 ? System.Drawing.Color.FromName("#ffffff") :System.Drawing.Color.FromName("#ffcdd2");
            //pintar();
        }

        private void ChangeValues(int num_nomina , string value_APLICA , int total , string day)
        {
            DataTable dt1 = ViewState["dt_comidas"] as DataTable;
            foreach (DataRow rows in dt1.Rows)
            {
                int num_nom = Convert.ToInt32(rows["NOMINA]"]);
                if (num_nom == num_nomina)
                {
                    rows[day] = value_APLICA;
                    rows["DESCUENTO"] = total;
                    break;
                }
            }
            ViewState["dt_comidas"] = dt1;
        }
        private DataRow row_empleado(int num_nomina)
        {
            DataTable dt1 = ViewState["dt_comidas"] as DataTable;
            DataView dv = dt1.DefaultView;
            dv.RowFilter = "[NOMINA] = " + num_nomina+"";
            DataTable dt = dv.ToTable();
            return dt.Rows[0];
        }

        protected void txtfiltro_TextChanged(object sender, EventArgs e)
        {
            CargarGrid(txtfiltro.Text.Trim());
        }

        protected void grid_comidas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                int total = Convert.ToInt32(rowView["DESCUENTO"]);
                e.Row.BackColor= total > 0 ? System.Drawing.Color.FromName("#ffffff") : System.Drawing.Color.FromName("#ffcdd2");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            pintar();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["TABLA_ERRORES"];
            try
            {
                string attachment = "attachment; filename=lista.xls";
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
                Alert.ShowAlertError(ex.ToString(),this);
            }
        }
    }
}