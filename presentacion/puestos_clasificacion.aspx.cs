using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.Web;

namespace presentacion
{
    public partial class relacion_reclu_puestos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
        }


        protected void lnksubir_Click(object sender, EventArgs e)
        {
            if (fuparchivo.HasFile)
            {
                try
                {
                    string randomNumber = Convert.ToInt32(Session["sidc_usuario"]).ToString();
                    randomNumber = randomNumber + "_";
                    DateTime localDate = DateTime.Now;
                    string date = localDate.ToString();
                    date = date.Replace("/", "_");
                    date = date.Replace(":", "_");
                    date = date.Replace(" ", "");
                    DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/excel/"));//path local
                    string name = dirInfo + randomNumber.Trim()+ date + Path.GetExtension(fuparchivo.FileName);
                    funciones.UploadFile(fuparchivo, name.Trim(), this.Page);
                    DataTable dt = ReadXL.ReadAsDataTable(name);

                    string value = ddlaccion.SelectedValue.ToString().Trim();
                    if (!dt.Columns.Contains("idc_puesto") || !dt.Columns.Contains("descripcion") || !dt.Columns.Contains("clasificacion"))
                    {
                        Alert.ShowAlertError("El Archivo de Excel NO CONTIENE EL FORMATO CORRESPONDIENTE. Veriquelo e Intentelo de Nuevo", this);
                    }
                    else if (dt.Rows.Count <= 0)
                    {
                        Alert.ShowAlertError("El Archivo de Excel NO CONTIENE DATOS", this);
                    }
                    else if (value == "0")
                    {
                        Alert.ShowAlertError("Seleccione una accion valida", this);
                    }
                    else if (value == "A" && cbxbtodo.Checked == false && cbxactuali.Checked == false)
                    {
                        Alert.ShowAlertError("Seleccione si desea borrar todos y dejar el excel, o solo actualiza el excel.", this);
                    }
                    else
                    {
                        grid.DataSource = null;
                        grid.DataBind();
                        lnkexport.Visible = false;
                        error.Visible = false;
                        TareasENT entidad = new TareasENT();
                        TareasCOM componente = new TareasCOM();
                        bool borrar = cbxbtodo.Checked;
                        string Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        string Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        string Pusuariopc = funciones.GetUserName();//usuario pc
                        int Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        string cadena = Cadena(dt);
                        int totalcad = TotalCadena(dt);
                        DataSet ds = componente.sp_arealcion_reclutadores_puestos(cadena, totalcad, Idc_usuario, Pdirecip, Pnombrepc, Pusuariopc, value, borrar);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();

                        if (vmensaje == "")
                        {
                            if (ds.Tables.Count > 1)
                            {
                                DataTable dt2 = ds.Tables[1];
                                if (dt2.Rows.Count > 0)
                                {
                                    error.Visible = true;
                                    lnkexport.Visible = true;
                                    lblerror.Text = "Algunos Datos se Actualizaron  de Manera Correcta. Pero se encontraron errores";
                                    Alert.ShowAlertError("Algunos Datos se Actualizaron de Manera Correcta. Pero se encontraron errores", this);
                                    ViewState["dt2_error"] = dt2;
                                    grid.DataSource = dt2;
                                    grid.DataBind();
                                }
                                else
                                {

                                    Alert.ShowAlert("Datos Actualizados de Manera Correcta", "Mensaje del Sistema", this);
                                }
                            }
                            else
                            {

                                Alert.ShowAlert("Datos Actualizados de Manera Correcta", "Mensaje del Sistema", this);
                            }
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                    }
                }
                catch (Exception ex)
                {
                    error.Visible = true;
                    lblerror.Text = ex.ToString();
                    Alert.ShowAlertError(ex.ToString(), this);
                }
            }
            else {
                Alert.ShowAlertInfo("Seleccione un archivo", "Mensaje del sistema", this);
            }


        }

        void ExportDataTable(DataTable dt)
        {
            Export Export = new Export();
            //array de DataTables
            List<DataTable> ListaTables = new List<DataTable>();
            ListaTables.Add(dt);
            //array de nombre de sheets
            string[] Nombres = new string[] { "Relacion Reclutadores" };
            if (dt.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("Esta Tabla no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
            }
            else
            {
                string mensaje = Export.toExcel("Relacion Reclutadores", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                                   XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 1,
                                   "reclu.xlsx", Page.Response);
                if (mensaje != "")
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }
        string Cadena(DataTable dt)
        {
            string cadena = "";
            foreach (DataRow row in dt.Rows)
            {
                string idc_puesto = row["idc_puesto"].ToString();
                string descripcion = row["descripcion"].ToString();
                string clasificacion = row["clasificacion"].ToString();
                if (idc_puesto != "" && descripcion != "" && clasificacion != "")
                {

                    cadena = cadena + row["idc_puesto"].ToString().Trim() + ";" + row["descripcion"].ToString().Trim() + ";" + row["clasificacion"].ToString().Trim() + ";";
                }
            }
            return cadena;
        }
        int TotalCadena(DataTable dt)
        {
            int cadena = 0;
            foreach (DataRow row in dt.Rows)
            {
                string idc_puesto = row["idc_puesto"].ToString();
                string descripcion = row["descripcion"].ToString();
                string clasificacion = row["clasificacion"].ToString();
                if (idc_puesto != "" && descripcion != "" && clasificacion != "")
                {

                    cadena = cadena + 1;
                }
            }
            return cadena;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DataTable dt2 = ViewState["dt2_error"] as DataTable;
            ExportDataTable(dt2);
        }

        protected void ddlaccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = ddlaccion.SelectedValue.ToString().Trim();
            if (value == "0")
            {
                Alert.ShowAlertError("Seleccione una opcion valida", this);
            }
            else if (value == "A") {
                borrartodo.Visible = true;
                cbxactuali.Checked = false;
                cbxbtodo.Checked = false;

            }
            else if (value == "B")
            {
                cbxactuali.Checked = true;
                cbxbtodo.Checked = false;
                borrartodo.Visible = false;
            }
        }

        protected void cbxbtodo_CheckedChanged(object sender, EventArgs e)
        {
            cbxactuali.Checked = cbxbtodo.Checked == true ? false : true;
        }

        protected void cbxactuali_CheckedChanged(object sender, EventArgs e)
        {
            cbxbtodo.Checked = cbxactuali.Checked == true ? false : true;
        }

        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/"));//path local
            string ruta = dirInfo +"ejemplo.xlsx";
            funciones.Download(ruta, Path.GetFileName(ruta),this);
        }
    }
}