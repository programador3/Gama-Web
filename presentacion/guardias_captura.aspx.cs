using DocumentFormat.OpenXml.Packaging;
using negocio.Componentes;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace presentacion
{
    public partial class guardias_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["dt_descansos"] = null;
                ViewState["dt_errores"] = null;

            }
        }

        bool mExiste(DataTable dt2)
        {
            bool exist = false;

            foreach (DataRow row in dt2.Rows)
            {
                string fecha = row["fecha"].ToString();
                string idc_empleado = row["nomina"].ToString();
                DataRow[] guardias = dt2.Select("nomina = '" + idc_empleado.Trim() + "' and fecha = '" + fecha.Trim() + "'");               
                if (guardias.Length > 1)
                {
                    exist = true;
                    break;
                }

            }
            return exist;
        }
        protected void lnksubirarchivo_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuparchivo.HasFile)
                {
                    grid_descansos.DataSource = null;
                    grid_descansos.DataBind();
                    errores.Visible = false;
                    ViewState["dt_errores"] = null;
                    string randomNumber = Convert.ToInt32(Session["sidc_usuario"]).ToString();
                    randomNumber = randomNumber + "_";
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/excel/"));//path local
                    string name = dirInfo + "guardias_" + randomNumber.Trim() + date + Path.GetExtension(fuparchivo.FileName);
                    funciones.UploadFile(fuparchivo, name.Trim(), this.Page);
                    DataTable dt = SpreadsheetDocument.Open(name, false).GetDataTableFromSpreadSheet(3);
                    bool error = mExiste(dt);
                    if (error)
                    {
                         Alert.ShowAlertError("EL ARCHIVO DE EXCEL CONTIENE UN REGISTRO DUPLICADO. \\nRECUERDE QUE NO PUEDE EXISTIR DOS REGISTROS DE UN MISMO EMPLEADO EN UNA MISMA FECHA \\n UTILIZE EL FORMATO CORRECTO.", this);
                        
                    }
                    else if(dt.Rows.Count == 0)
                    {
                        Alert.ShowAlertError("EL ARCHIVO DE EXCEL NO PUDO SER LEIDO. UTILIZE EL FORMATO CORRECTO.", this);
                    }

                    else if (!dt.Columns.Contains("NOMINA")
                        || !dt.Columns.Contains("FECHA")
                         || !dt.Columns.Contains("TIPO"))
                    {
                        Alert.ShowAlertError("EL ARCHIVO DE EXCEL NO PUDO SER LEIDO. UTILIZE EL FORMATO CORRECTO.", this);
                    }
                    else
                    {
                        dt.Columns.Add("fecha_str");
                        dt.Columns.Add("tipo_str");
                        foreach (DataRow row in dt.Rows)
                        {
                            DateTime vdate = Convert.ToDateTime(row["FECHA"]);
                            row["fecha"] = vdate.ToString("dddd dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
                            row["fecha_str"] = vdate.ToString("yyyy-dd-MM");
                            row["tipo_str"] = row["tipo"].ToString().ToUpper();
                            string tipo = "";
                            switch (row["tipo"].ToString().ToUpper())
                            {
                                case "D":
                                case "DESCANSA":
                                    tipo = "DESCANSA";
                                    break;

                                case "P":
                                case "PENALIZACIÓN":
                                    tipo = "PENALIZACIÓN";
                                    break;
                            }
                            row["tipo"] = tipo;
                        }
                        ViewState["dt_descansos"] = dt;
                        grid_descansos.DataSource = dt;
                        grid_descansos.DataBind();
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

        private String Cadena()
        {
            try
            {
                string cadena = "";
                DataTable dt = ViewState["dt_descansos"] as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    cadena = cadena + row["nomina"].ToString().Trim() + ";"
                        + row["fecha_str"].ToString().Trim() + ";"
                        + row["tipo_str"].ToString().Trim() + ";";
                }
                return cadena;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
                return "";
            }
        }

        private int TotalCadena()
        {
            try
            {
                DataTable dt = ViewState["dt_descansos"] == null ? new DataTable() : ViewState["dt_descansos"] as DataTable;
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
                return 0;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                AgentesCOM componente = new AgentesCOM();
                string cadena = Cadena();
                int total = TotalCadena();
                DataSet ds = componente.sp_anomina_asistencia_guardias(Convert.ToInt32(Session["sidc_usuario"]), total,cadena);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                grid_descansos.DataSource = null;
                grid_descansos.DataBind();
                ViewState["dt_descansos"] = null;
                ViewState["dt_errores"] = null;
                DataTable dt = ds.Tables[1];
                if (dt.Rows.Count > 0)
                {
                    vmensaje = "ALGUNOS REGISTROS NO SE GUARDARON. \\n VERIFIQUE LA TABLA SIGUIENTE:";
                }
                if (vmensaje == "")
                {
                    errores.Visible = false;

                    Alert.ShowAlert("Todos los registros fueron guardados correctamente.","Mensaje del Sistema",this);
                }
                else
                {
                    dt.Columns.Add("fecha_str");
                    foreach (DataRow row in dt.Rows)
                    {
                        DateTime vdate = Convert.ToDateTime(row["FECHA"]);
                        string var = vdate.ToString("yyyy-MM-dd");
                        row["fecha_str"] = @var;
                    }
                    errores.Visible = true;
                    griderrores.DataSource = dt;
                    griderrores.DataBind();
                    dt.Columns.Remove("FECHA");
                    dt.Columns["fecha_str"].ColumnName = "FECHA";
                    ViewState["dt_errores"] = dt;
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void lnksubir_Click(object sender, EventArgs e)
        {
            if (TotalCadena() == 0)
            {
                Alert.ShowAlertError("EL SISTEMA NO DETECTA NINGUN REGISTRO PARA SUBIR \\nIntente subir el archivo nuevamente o comuniquese al depto de sistemas.", this);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                    "ModalConfirm('Mensaje del Sistema','¿Desea Guardar el Listado?','modal fade modal-info');",
                    true);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt_errores"];
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
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void Linkdown_Click(object sender, EventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/"));//path local
            string ruta = dirInfo + "ejemplo_guardias.xlsx";
            funciones.Download(ruta, Path.GetFileName(ruta), this);
        }

    }
}