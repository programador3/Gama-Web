using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class revision_herramientas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("descripcion");
                papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("extension");
                Session["papeleria"] = papeleria;
                DataTable herramienta = new DataTable();
                herramienta.Columns.Add("descripcion");
                herramienta.Columns.Add("idc_tipoherramienta");
                herramienta.Columns.Add("costo");
                herramienta.Columns.Add("cantidad");
                herramienta.Columns.Add("revision");
                herramienta.Columns.Add("observaciones");
                herramienta.Columns.Add("gpo");
                Session["herramientas"] = herramienta;
                string tipo = Request.QueryString["tipo"];//N normal P pendiete
                switch (tipo)
                {
                    case "N":
                        BuscarVehiculos("");
                        break;

                    case "P":
                        txtbuscarvehiculo.Visible = false;
                        lnkbuscarvehiculo.Visible = false;
                        ddlvehiculo.Enabled = false;
                        if (Request.QueryString["idc_pendiente"] == null)
                        {
                            Alert.ShowAlertInfo("Hay un error, no se detecta el parametro de pendiente. Verifiquelo.", "Mensaje del Sistema", this);
                        }
                        else
                        {
                            BuscarVehiculosEmpleadoPendiente(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_pendiente"])));
                        }
                        break;
                }
            }
        }

        private void BuscarVehiculosEmpleadoPendiente(int idc_pendiete)
        {
            Vehiculos_RevisionENT entidad = new Vehiculos_RevisionENT();
            Vehiculos_RevisionCOM com = new Vehiculos_RevisionCOM();
            entidad.Pidc_revbasherr = idc_pendiete;
            DataSet ds = com.CargarInfoPendiente(entidad);
            DataRow row = ds.Tables[0].Rows[0];
            int idc_vehiculo = Convert.ToInt32(row["idc_vehiculo"]);
            BuscarVehiculos("");
            ddlvehiculo.SelectedValue = idc_vehiculo.ToString();
            BuscarEmpleadoPorVehiculo(idc_vehiculo);
            HerramientasRevision(idc_vehiculo);
        }

        private void BuscarVehiculos(string filtro)
        {
            Vehiculos_RevisionENT entidad = new Vehiculos_RevisionENT();
            Vehiculos_RevisionCOM com = new Vehiculos_RevisionCOM();
            DataSet ds = com.CargaVehiculos(filtro);
            DataTable dt = new DataTable();
            dt.Columns.Add("idc_vehiculo");
            dt.Columns.Add("desc");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DataRow newrow = dt.NewRow();
                newrow["idc_vehiculo"] = row["idc_vehiculo"].ToString();
                newrow["desc"] = row["num_economico"].ToString() + ".- " + row["descripcion"].ToString();
                dt.Rows.Add(newrow);
            }
            ddlvehiculo.DataValueField = "idc_vehiculo";
            ddlvehiculo.DataTextField = "desc";

            ddlvehiculo.DataSource = dt;
            ddlvehiculo.DataBind();
            ddlvehiculo.Items.Insert(0, new ListItem("--Seleccione un Vehiculo"));
            if (dt.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("No se encontraron resultados, intentelo de nuevo", "Mensaje del sistema", this);
            }
        }

        private void BuscarEmpleadoPorVehiculo(int idc_vehiculo)
        {
            Vehiculos_RevisionENT entidad = new Vehiculos_RevisionENT();
            entidad.Idc_vehiculo = idc_vehiculo;
            Vehiculos_RevisionCOM com = new Vehiculos_RevisionCOM();
            DataSet ds = com.CargaEmpleado(entidad);
            DataRow row = ds.Tables[0].Rows[0];
            txtempleado.Text = row["chofer"].ToString();
            idc_empleado.Text = row["idc_empleado"].ToString();
            lnkreporte.Visible = Convert.ToInt32(row["idc_empleado"]) > 0;
            div_Reporte.Visible = false;
            if (Convert.ToInt32(row["idc_empleado"]) > 0) { CargarReportes(Convert.ToInt32(row["idc_empleado"])); }
        }

        private void HerramientasRevision(int idc_vehiculo)
        {
            Vehiculos_RevisionENT entidad = new Vehiculos_RevisionENT();
            entidad.Idc_vehiculo = idc_vehiculo;
            Vehiculos_RevisionCOM com = new Vehiculos_RevisionCOM();
            DataSet ds = com.CargarHerramientasRevisionBasica(entidad, Request.QueryString["rb"] == "1" ? true : false);
            //gridherramientas.DataSource = ds.Tables[0];
            //gridherramientas.DataBind();
            DataView view = new DataView(ds.Tables[0]);
            DataTable distinctValues = view.ToTable(true, "gpo");
            foreach (DataRow row in distinctValues.Rows)
            {
                string gpo = row["gpo"].ToString();
                InsertIntoHerramientas(row["gpo"].ToString(), "", "", "", "", "", "");
                view.RowFilter = "gpo like '%" + gpo + "%'";
                DataTable filter2 = view.ToTable();
                foreach (DataRow row2 in filter2.Rows)
                {
                    InsertIntoHerramientas("", row2["descripcion"].ToString(), row2["idc_tipoherramienta"].ToString(), row2["costo"].ToString(), row2["cantidad"].ToString(), row2["cantidad"].ToString(), "");
                }
            }
            CargaGridHerr();
        }

        private void CargaGridHerr()
        {
            DataTable herramientas = (DataTable)Session["herramientas"];
            gridherramientas.DataSource = herramientas;
            gridherramientas.DataBind();
        }

        private void InsertIntoHerramientas(string grupo, string descripcion, string idc_tipoherramienta, string costo, string cantidad, string revision, string observaciones)
        {
            DataTable herramientas = (DataTable)Session["herramientas"];
            DataRow row = herramientas.NewRow();
            row["gpo"] = grupo;
            row["descripcion"] = descripcion;
            row["idc_tipoherramienta"] = idc_tipoherramienta;
            row["costo"] = costo;
            row["cantidad"] = cantidad;
            row["revision"] = revision;
            row["observaciones"] = observaciones;
            herramientas.Rows.Add(row);
            Session["herramientas"] = herramientas;
        }

        private void UpdateIntoHerramientas(string idc_tipoherramienta, string revision, string observaciones)
        {
            DataTable herramientas = (DataTable)Session["herramientas"];
            foreach (DataRow row in herramientas.Rows)
            {
                if (row["idc_tipoherramienta"].ToString() == idc_tipoherramienta)
                {
                    row["revision"] = revision;
                    row["observaciones"] = observaciones;
                }
            }
            Session["herramientas"] = herramientas;
        }

        private int CalibraLlanta(int idc_vehiculo)
        {
            Vehiculos_RevisionENT entidad = new Vehiculos_RevisionENT();
            entidad.Idc_vehiculo = idc_vehiculo;
            Vehiculos_RevisionCOM com = new Vehiculos_RevisionCOM();
            DataSet ds = com.ValidaCalibracion(entidad);
            return Convert.ToInt32(ds.Tables[0].Rows[0]["calibra"]);
        }

        protected void lnkbuscarvehiculo_Click(object sender, EventArgs e)
        {
            BuscarVehiculos(txtbuscarvehiculo.Text);
            txtbuscarvehiculo.Text = "";
        }

        protected void ddlvehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc = Convert.ToInt32(ddlvehiculo.SelectedValue);
            if (idc == 0)
            {
                Alert.ShowAlertError("Seleccione un Vehiculo", this);
            }
            else
            {
                BuscarEmpleadoPorVehiculo(idc);
                HerramientasRevision(idc);
            }
        }

        protected void Yesh_Click(object sender, EventArgs e)
        {
            int cantidad = Convert.ToInt32(txtcantidad.Text);
            int revision = txtentrego.Text == "" ? 0 : Convert.ToInt32(txtentrego.Text);
            if (revision > cantidad) { revision = cantidad; }
            int diferencia = cantidad - revision;
            decimal costo = Convert.ToDecimal(txtcosto.Text);
            decimal costovale = diferencia * costo;
            lblerror.Visible = false;
            if (revision < 0)
            {
                lblerror.Text = "No puede entregar menor a 0";
                lblerror.Visible = true;
                txtentrego.Text = txtcantidad.Text;
                txtcostovale.Text = "0";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirmh('');", true);
            }
            else
            {
                UpdateIntoHerramientas(lblidc_herramienta.Text, revision.ToString(), txtobservaciones.Text);
                txtcantidad.Text = "";
                txtentrego.Text = "";
                txtcosto.Text = "";
                lblidc_herramienta.Text = "";
                lblerror.Visible = false;
                SumaVale();
                CargaGridHerr();
            }
        }

        protected void texcosto_Click(object sender, EventArgs e)
        {
            int cantidad = Convert.ToInt32(txtcantidad.Text);
            int revision = txtentrego.Text == "" ? 0 : Convert.ToInt32(txtentrego.Text);
            if (revision > cantidad) { revision = cantidad; }
            decimal costo = Convert.ToDecimal(txtcosto.Text);
            decimal costovale = (cantidad - revision) * costo;
            lblerror.Visible = false;
            if (revision < 0)
            {
                lblerror.Text = "No puede entregar menor a 0";
                lblerror.Visible = true;
                txtentrego.Text = txtcantidad.Text;
                txtcostovale.Text = "0";
            }
            else
            {
                txtcostovale.Text = costovale.ToString();
                CargaGridHerr();
            }
        }

        private void SumaVale()
        {
            DataTable herramientas = (DataTable)Session["herramientas"];
            decimal totalvale = 0;
            foreach (DataRow row in herramientas.Rows)
            {
                string id = row["idc_tipoherramienta"].ToString();
                if (id != "")
                {
                    int cantidad = Convert.ToInt32(row["cantidad"]);
                    int revision = Convert.ToInt32(row["revision"]);
                    if (revision > cantidad) { revision = cantidad; }
                    decimal costo = Convert.ToDecimal(row["costo"]);
                    decimal costovale = (cantidad - revision) * costo;
                    totalvale = totalvale + costovale;
                }
            }
            txttotalvale.Text = totalvale.ToString();
        }

        protected void lnkver_Click(object sender, EventArgs e)
        {
            lnkocultar.Visible = true;
            lnkver.Visible = false;
            repeat_herramientas.Visible = true;
        }

        protected void lnkocultar_Click(object sender, EventArgs e)
        {
            lnkocultar.Visible = false;
            lnkver.Visible = true;
            repeat_herramientas.Visible = false;
        }

        protected void gridPapeleria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridPapeleria.DataKeys[index].Values["ruta"].ToString();
            string extension = gridPapeleria.DataKeys[index].Values["extension"].ToString();
            string descripcion = gridPapeleria.DataKeys[index].Values["descripcion"].ToString();
            DataTable papeleria = (DataTable)Session["papeleria"];
            int papeleriat = papeleria.Rows.Count;
            switch (e.CommandName)
            {
                case "Descargar":
                    Download(ruta, Path.GetFileName(ruta));
                    break;

                case "Eliminar":
                    List<DataRow> rowsToDelete = new List<DataRow>();
                    foreach (DataRow row in papeleria.Rows)
                    {
                        if (row["ruta"].ToString().Equals(ruta) || row["descripcion"].ToString().Equals(descripcion))
                        {
                            rowsToDelete.Add(row);
                        }
                    }
                    foreach (DataRow rowde in rowsToDelete)
                    {
                        papeleria.Rows.Remove(rowde);
                    }
                    Session["papeleria"] = papeleria;
                    gridPapeleria.DataSource = papeleria;
                    gridPapeleria.DataBind();
                    break;
            }
        }

        protected void lnkGuardarPape_Click(object sender, EventArgs e)
        {
            bool error = false;

            string id_archi = "0";
            id_archi = Session["id_archi"] != null ? (string)Session["id_archi"] : "0";
            if (txtcomenthallazgo.Text == "") { error = true; Alert.ShowAlertError("Debe ingresar un comentario.", this); }
            if (!fuparchivo.HasFile) { error = true; Alert.ShowAlertError("Debe ingresar una imagen", this); }
            Random random = new Random();
            int randomNumber = random.Next(0, 1000);
            if (fuparchivo.HasFile && error == false)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/tareas/"));//path local
                string mensaje = AddPapeleriaToTable(dirInfo + randomNumber.ToString() + fuparchivo.FileName, Path.GetExtension(fuparchivo.FileName).ToString(), txtcomenthallazgo.Text.ToUpper());
                if (mensaje.Equals(string.Empty))
                {
                    bool pape = funciones.UploadFile(fuparchivo, dirInfo + randomNumber.ToString() + fuparchivo.FileName, this.Page);
                    if (pape == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + lnkGuardarPape.ClientID.ToString() + "');", true);
                        Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "2000", "Golpe Guardardo Correctamente", this);
                        //agregamos a tabla global de papelera
                        fuparchivo.Visible = true;
                        Session["id_archi"] = null;
                    }
                }
                else
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }

        public void Download(string path, string file_name)
        {
            if (!File.Exists(path))
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

        public string AddPapeleriaToTable(string ruta, string extension, string descripcion)
        {
            string mensaje = "";
            bool exists = false;
            try
            {
                DataTable papeleria = (DataTable)Session["papeleria"];
                foreach (DataRow check in papeleria.Rows)
                {
                    if (check["descripcion"].Equals(descripcion))
                    {
                        check.Delete();
                        break;
                    }
                }
                DataRow new_row = papeleria.NewRow();
                new_row["ruta"] = ruta;
                new_row["extension"] = extension;
                new_row["descripcion"] = descripcion;
                papeleria.Rows.Add(new_row);
                gridPapeleria.DataSource = papeleria;
                gridPapeleria.DataBind();
                Session["papeleria"] = papeleria;
                txtcomenthallazgo.Text = "";
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }

            return mensaje;
        }

        private string CadenaHerramientas()
        {
            string cadena = "";
            DataTable herramientas = (DataTable)Session["herramientas"];
            decimal totalvale = 0;
            foreach (DataRow row in herramientas.Rows)
            {
                string id = row["idc_tipoherramienta"].ToString();
                if (id != "")
                {
                    string observaciones = row["observaciones"].ToString();
                    int cantidad = Convert.ToInt32(row["cantidad"]);
                    int revision = Convert.ToInt32(row["revision"]);
                    if (revision > cantidad) { revision = cantidad; }
                    int faltante = cantidad - revision;
                    decimal costo = Convert.ToDecimal(row["costo"]);
                    decimal costovale = faltante * costo;
                    totalvale = totalvale + costovale;
                    cadena = cadena + id + ";" + faltante.ToString() + ";" + observaciones + ";" + costo.ToString() + ";";
                }
            }
            return cadena;
        }

        private int TotalCadenaHerramientas()
        {
            int cadena = 0;
            DataTable herramientas = (DataTable)Session["herramientas"];
            foreach (DataRow row in herramientas.Rows)
            {
                string id = row["idc_tipoherramienta"].ToString();
                if (id != "")
                {
                    cadena++;
                }
            }
            return cadena;
        }

        private string CadenaHallazgos()
        {
            string cadena = "";
            DataTable papeleria = (DataTable)Session["papeleria"];
            foreach (DataRow check in papeleria.Rows)
            {
                cadena = cadena + check["descripcion"].ToString() + ";" + check["ruta"].ToString() + ";" + check["extension"].ToString() + ";";
            }
            return cadena;
        }

        private bool falta()
        {
            bool cadena = false;
            if (txttotalvale.Text != "" && txttotalvale.Text != "0" && txttotalvale.Text != "0.00")
            {
                cadena = true;
            }
            return cadena;
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                DataTable papeleria = (DataTable)Session["papeleria"];
                DataSet ds = new DataSet();
                string vmensaje = "";
                switch (caso)
                {
                    case "Guardar":
                        Vehiculos_RevisionENT entidad = new Vehiculos_RevisionENT();
                        Vehiculos_RevisionCOM com = new Vehiculos_RevisionCOM();
                        if (div_Reporte.Visible)
                        {
                            entidad.Pidc_tiporep = Convert.ToInt32(ddltiporeporte.SelectedValue);
                            entidad.POBSERVACIONES = txtcomentarios.Text.ToUpper();
                            entidad.Pidc_Empleadomio = Convert.ToInt32(Session["sidc_empleado"]);
                        }
                        entidad.Pidc_empleado = Convert.ToInt32(idc_empleado.Text);
                        entidad.Idc_vehiculo = Convert.ToInt32(ddlvehiculo.SelectedValue);
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        entidad.Pcadena = CadenaHerramientas();
                        entidad.Pnumcad = TotalCadenaHerramientas();
                        entidad.Pacedna2 = CadenaHallazgos();
                        entidad.Pnumcad2 = papeleria.Rows.Count;
                        entidad.Pmonto = Convert.ToDecimal(txtporsemana.Text);
                        entidad.Ptotal = Convert.ToDecimal(txttotalvale.Text);
                        entidad.Pidc_revbasherr = Request.QueryString["idc_pendiente"] == null ? 0 : Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_pendiente"]));
                        entidad.Pbasica = Request.QueryString["rb"] == "1" ? true : false;
                        entidad.Pfalta = falta();
                        ds = com.GuardarRevision(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            DataTable tabla_archivos = ds.Tables[1];
                            bool correct = true;
                            foreach (DataRow row_archi in tabla_archivos.Rows)
                            {
                                string ruta_det = row_archi["destino"].ToString();
                                string ruta_origen = row_archi["origen"].ToString();
                                correct = funciones.CopiarArchivos(ruta_origen, ruta_det, this.Page);
                                if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo " + ruta_origen + "a la ruta " + ruta_det, this); }
                            }
                            int total = (((tabla_archivos.Rows.Count) * 1) + 1) * 1000;
                            string t = total.ToString();
                            string url_back = Session["url_back"] != null ? (string)Session["url_back"] : "menu.aspx";
                            Alert.ShowGiftMessage("Estamos procesando la cantidad de " + tabla_archivos.Rows.Count.ToString() + " archivo(s) al Servidor.", "Espere un Momento", url_back, "imagenes/loading.gif", t, "La Revision fue Guardada Correctamente", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        private string error_vale()
        {
            decimal totalvale = Convert.ToDecimal(txttotalvale.Text);
            decimal descontar = txtporsemana.Text == "" ? 0 : Convert.ToDecimal(txtporsemana.Text);
            string ret = "";
            if (totalvale <= 50 && descontar < 50)
            {
                ret = "La Cantidad a Descontar por Semana Debe ser Igual al Total del Faltante de Herramienta.";
            }
            if (descontar == 0)
            {
                ret = "Debe colocar el descuento por semana.";
            }
            if (descontar > totalvale)
            {
                ret = "La Cantidad a Descontar por Semana No Debe ser Mayor al Total del Faltante de Herramienta.";
            }
            if (descontar < 0)
            {
                ret = "La Cantidad a Descontar por Semana Debe ser Mayor a 0.";
            }
            return ret;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool faltastr = falta();
            string error_valestr = error_vale();
            if (blkcalibracion.CssClass == "btn btn-default btn-block" && CalibraLlanta(Convert.ToInt32(ddlvehiculo.SelectedValue)) > 0)
            {
                Alert.ShowAlertError("Debe Revisar Calibracion de Llantas", this);
            }
            else if (faltastr == true && error_valestr != "")
            {
                Alert.ShowAlertError(error_valestr, this);
            }
            else if (div_Reporte.Visible==true && ddltiporeporte.SelectedValue == "0" )
            {
                Alert.ShowAlertError("Seleccione un tipo de reporte para asignarlo.", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar esta revision?','modal fade modal-info');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string url = Session["url_back"] == null ? "menu.aspx" : Session["url_back"] as string;
            Response.Redirect(url);
        }

        protected void blkcalibracion_Click(object sender, EventArgs e)
        {
            blkcalibracion.CssClass = blkcalibracion.CssClass == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
        }

        protected void cerr_Click(object sender, EventArgs e)
        {
            txtcantidad.Text = "";
            txtentrego.Text = "";
            txtcosto.Text = "";
            lblidc_herramienta.Text = "";
            SumaVale();
            CargaGridHerr();
            lblerror.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMeededssage", "ModalClose();", true);
            CargaGridHerr();
        }

        protected void txtporsemana_TextChanged(object sender, EventArgs e)
        {
            string error_valestr = error_vale();
            if (falta() == true && error_valestr != "")
            {
                Alert.ShowAlertError(error_valestr, this);
            }
        }

        protected void gridherramientas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string idc_tipoherramienta = gridherramientas.DataKeys[index].Values["idc_tipoherramienta"].ToString();
            string descripcion = gridherramientas.DataKeys[index].Values["descripcion"].ToString();
            string cantidad = gridherramientas.DataKeys[index].Values["cantidad"].ToString();
            string revision = gridherramientas.DataKeys[index].Values["revision"].ToString();
            string observaciones = gridherramientas.DataKeys[index].Values["observaciones"].ToString();
            string costo = gridherramientas.DataKeys[index].Values["costo"].ToString();
            lblidc_herramienta.Text = idc_tipoherramienta;
            txtcantidad.Text = cantidad;
            txtcosto.Text = costo;
            txtcostovale.Text = "0";
            txtentrego.Text = revision;
            txtobservaciones.Text = observaciones;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirmh('" + descripcion + "');", true);
            CargaGridHerr();
        }

        protected void gridherramientas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string idc_tipo = rowView["idc_tipoherramienta"].ToString();
                if (idc_tipo == "")
                {
                    e.Row.BackColor = Color.FromName("#eee");
                    e.Row.ForeColor = Color.FromName("#000");
                    e.Row.Cells[7].Controls.Clear();
                }
            }
        }

        protected void lnkreporte_Click(object sender, EventArgs e)
        {
            lnkreporte.CssClass = lnkreporte.CssClass == "btn btn-default btn-block" ? "btn btn-info btn-block" : "btn btn-default btn-block";
            div_Reporte.Visible = lnkreporte.CssClass == "btn btn-info btn-block";
        }

        private void CargarReportes(int IDC_EMPLEADO)
        {
            try
            {
                ReportesENT entidad = new ReportesENT();
                entidad.Pidc_empleado = IDC_EMPLEADO;
                ReportesCOM componentes = new ReportesCOM();
                ddltiporeporte.DataValueField = "idc_tiporep";
                ddltiporeporte.DataTextField = "descripcion";
                ddltiporeporte.DataSource = componentes.Carga(entidad);
                ddltiporeporte.DataBind();
                ddltiporeporte.Items.Insert(0, new ListItem("--Seleccione un Reporte", "0")); //updated code}
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
    }
}