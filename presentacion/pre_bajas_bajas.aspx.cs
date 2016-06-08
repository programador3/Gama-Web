using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class pre_bajas_bajas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!Page.IsPostBack)
            {
                //iniciamos valores del switch
                Session["apto_reingreso"] = 0;
                Session["status"] = 1;
                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("nombre");
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("extension");
                Session["papeleria_perfil"] = papeleria;
            }
            Session["idc_usuario"] = Convert.ToInt32(Session["sidc_usuario"].ToString());
            GenerarDatos();
        }

        public void GenerarDatos()
        {
            BajasENT entidad = new BajasENT();
            BajasCOM componente = new BajasCOM();
            DataSet ds = componente.CargaBajas(entidad);
            DataTable table = ds.Tables[0];
            gridEmpleados.DataSource = table;
            gridEmpleados.DataBind();
            if (ds.Tables[0].Rows.Count == 0) { Noempleados.Visible = true; }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    string strHostName = Dns.GetHostName();
                    IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                    BajasENT entidad = new BajasENT();
                    BajasCOM componente = new BajasCOM();
                    entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString()); //USUARIO QUE REALIZA LA PREBAJA
                    entidad.Ip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Nombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Usuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_puesto = Convert.ToInt32(Session["idc_puesto"].ToString());
                    entidad.Pidc_empleado = Convert.ToInt32(Session["idc_empleado"].ToString());
                    entidad.Pidc_prebaja = Convert.ToInt32(Session["idc_prebaja"].ToString());
                    entidad.Motivo = txtMotivo.Text;
                    entidad.Contratar = Convert.ToBoolean(Session["contratar"]);
                    entidad.Capacitacion = Convert.ToBoolean(Session["capacitacion"]);
                    SqlDateTime fecha = Convert.ToDateTime(txtFecha.Text.ToString());
                    entidad.Fecha = fecha;
                    entidad.Pidc_cheque = ddlCheque.SelectedValue == "1" ? Convert.ToInt32(txtcheque.Text) : 0;
                    DataSet ds = componente.InsertarRevision(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    //verificamos que no existan errores
                    string mensaje = row["mensaje"].ToString();
                    DataTable papeleria = (DataTable)Session["papeleria_perfil"];
                    if (mensaje == "" && papeleria.Rows.Count == 0)//no hay errores ni archivos
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('El empleado " + lblEmpleadoName.Text + " fue dado de baja correctamente.','pre_bajas_bajas.aspx','success');", true);
                    }
                    if (mensaje == "" && papeleria.Rows.Count > 0)//no hay errores y si hay archivos
                    {
                        DataRow row_archi = papeleria.Rows[0];
                        string extension = row_archi["extension"].ToString();
                        string ruta = row_archi["ruta"].ToString();
                        string new_ruta = funciones.GenerarRuta("encsal", "unidad");
                        new_ruta = new_ruta + Session["idc_prebaja"].ToString() + extension;
                        File.Copy(ruta, new_ruta);
                        Alert.ShowGiftMessage("Estamos procesando la encuesta de salida al Servidor.", "Espere un Momento", "pre_bajas_bajas.aspx", "imagenes/loading.gif", "4000", "La Baja fue Guardada Correctamente", this);
                    }
                    if (mensaje != "")
                    {//mostramos error
                        Alert.ShowAlertError(mensaje, this);
                    }
                    break;

                case "Editar":
                    Session["edit_prebaja"] = 1;
                    string value = (Convert.ToInt32(Session["idc_empleado"]).ToString());
                    Response.Redirect("pre_bajas.aspx?idc_empleado=" + value);
                    break;
            }
        }

        protected void btnACeptarPrebaja_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (ddlCheque.SelectedValue == "1")
            {
                error = validar_cheque() > 0 ? false : true;
                lblerrorcheque.Visible = validar_cheque() > 0 ? false : true;
            }
            if (!error == true)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de dar Baja definitiva a " + lblEmpleadoName.Text + ". Esta Seguro de Continuar?');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("pre_bajas_bajas.aspx");
        }

        protected void lnkVacanteC_Click(object sender, EventArgs e)
        {
        }

        protected void lnkVacanteNO_Click(object sender, EventArgs e)
        {
        }

        protected void lnkAptoReingresoSI_Click(object sender, EventArgs e)
        {
        }

        protected void lnkAptoReingresoNO_Click(object sender, EventArgs e)
        {
        }

        protected void gridEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER MEDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            int num_nomina = Convert.ToInt32(gridEmpleados.DataKeys[index].Values["num_nomina"].ToString());
            int idc_empleado = Convert.ToInt32(gridEmpleados.DataKeys[index].Values["idc_empleado"].ToString());
            int idc_prebaja = Convert.ToInt32(gridEmpleados.DataKeys[index].Values["idc_prebaja"].ToString());
            int idc_puesto = Convert.ToInt32(gridEmpleados.DataKeys[index].Values["idc_puesto"].ToString());
            Session["fecha_baja"] = gridEmpleados.DataKeys[index].Values["fecha_baja"];
            int capacitacion = Convert.ToInt32(gridEmpleados.DataKeys[index].Values["idc_cap"]);
            bool tipo_prebaja = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["renuncia"]);
            ddlTipoBaja.SelectedValue = tipo_prebaja == true ? "1" : "0";
            cbxHonesto.Checked = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["honesto"]);
            cbxDrogas.Checked = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["drogas"]);
            cbxTrabajador.Checked = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["trabajador"]);
            cbxAlcol.Checked = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["alcohol"]);
            cbxCartaRec.Checked = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["carta_recomendacion"]);
            bool contratar = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["contratar"]);
            bool apto_reingreso = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["apto_reingreso"]);
            if (capacitacion == 0)
            {
                Session["capacitacion"] = false;
            }
            else
            {
                Session["capacitacion"] = true;
            }
            if (contratar == false)
            {
                lnkVacanteC.CssClass = "btn btn-link";
                lnkVacanteNO.CssClass = "btn btn-primary active";
            }
            else
            {
                lnkVacanteNO.CssClass = "btn btn-link";
                lnkVacanteC.CssClass = "btn btn-primary active";
            }
            if (apto_reingreso == false)
            {
                lnkAptoReingresoSI.CssClass = "btn btn-link";
                lnkAptoReingresoNO.CssClass = "btn btn-primary active";
            }
            else
            {
                lnkAptoReingresoNO.CssClass = "btn btn-link";
                lnkAptoReingresoSI.CssClass = "btn btn-primary active";
            }
            Session["honesto"] = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["honesto"]);
            Session["drogas"] = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["drogas"]);
            Session["trabajador"] = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["trabajador"]);
            Session["alcohol"] = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["alcohol"]);
            Session["carta_recomendacion"] = Convert.ToBoolean(gridEmpleados.DataKeys[index].Values["carta_recomendacion"]);
            Session["num_nomina"] = num_nomina;
            Session["idc_empleado"] = idc_empleado;
            Session["idc_puesto"] = idc_puesto;
            Session["idc_prebaja"] = idc_prebaja;
            Session["contratar"] = contratar;
            Session["apto_reingreso"] = apto_reingreso;
            lblEmpleadoName.Text = gridEmpleados.DataKeys[index].Values["empleado"].ToString();
            lblPuesto.Text = gridEmpleados.DataKeys[index].Values["descripcion"].ToString();
            txtFecha.Text = gridEmpleados.DataKeys[index].Values["fecha_baja"].ToString();
            txtMotivo.Text = gridEmpleados.DataKeys[index].Values["motivo"].ToString();
            txtEspecificar.Text = gridEmpleados.DataKeys[index].Values["especificar"].ToString();
            Session["fecha"] = txtFecha.Text;
            Session["especificar"] = txtEspecificar.Text;
            Session["motivo"] = txtMotivo.Text;

            switch (e.CommandName)
            {
                case "Solicitar":
                    Noempleados.Visible = false;
                    row_grid.Visible = false;
                    PanelPreBaja.Visible = true;
                    break;

                case "Editar":
                    Session["Caso_Confirmacion"] = "Editar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea editar la Pre-Baja de  " + lblEmpleadoName.Text + ". Esta Seguro de Continuar?');", true);

                    break;
            }
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
            if (Session["Previus"] == null)
            {
                Response.Redirect("pre_bajas_bajas.aspx");
            }
        }

        protected void txtcheque_TextChanged(object sender, EventArgs e)
        {
            if (ddlCheque.SelectedValue == "1")
            {
                lblerrorcheque.Visible = validar_cheque() > 0 ? false : true;
            }
        }

        protected void rdbCheque_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_cheque.Visible = ddlCheque.SelectedValue == "1" ? true : false;
        }

        private int validar_cheque()
        {
            BajasENT entidad = new BajasENT();
            BajasCOM componente = new BajasCOM();
            entidad.Pidc_empleado = Convert.ToInt32(Session["idc_empleado"].ToString());
            entidad.Pidc_cheque = txtcheque.Text == "" ? 0 : Convert.ToInt32(txtcheque.Text);
            DataSet ds = componente.ComprobarCheque(entidad);
            DataRow row = ds.Tables[0].Rows[0];
            //verificamos que no existan errores
            int idc_cheque = Convert.ToInt32(row["idc_cheque"].ToString());
            return idc_cheque;
        }

        protected void lnkGuardarPape_Click(object sender, EventArgs e)
        {
            bool error = Path.GetExtension(fupPapeleria.FileName).ToString() == ".pdf" ? false : true;
            if (fupPapeleria.HasFile && error == false)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/entrevista_salida/"));//path local
                string mensaje = AddPapeleriaToTable(dirInfo + fupPapeleria.FileName, fupPapeleria.FileName, Path.GetExtension(fupPapeleria.FileName).ToString());
                if (mensaje.Equals(string.Empty))
                {
                    bool pape = funciones.UploadFile(fupPapeleria, dirInfo + fupPapeleria.FileName, this);
                    if (pape == false)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + lnkGuardarPape.ClientID.ToString() + "');", true);
                        Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "5000", "Archivo Guardardo Correctamente", this);
                        fupPapeleria.Visible = true;
                    }
                }
                else
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
            else
            {
                Alert.ShowAlertError("Deb ser un archivo PDF.", this);
            }
        }

        /// <summary>
        /// Agrega filas tabla de papeleria global (INCLUYE ELECTOR Y LICENCIA)
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="nombre"></param>
        public string AddPapeleriaToTable(string ruta, string nombre, string extension)
        {
            string mensaje = "";
            bool exists = false;
            DataTable papeleria = (DataTable)Session["papeleria_perfil"];
            papeleria.Rows.Clear();
            if (exists == false)
            {
                DataRow new_row = papeleria.NewRow();
                new_row["nombre"] = nombre;
                new_row["ruta"] = ruta;
                new_row["extension"] = extension;
                papeleria.Rows.Add(new_row);
                gridPapeleria.DataSource = papeleria;
                gridPapeleria.DataBind();
                Session["papeleria_perfil"] = papeleria;
            }
            return mensaje;
        }

        protected void gridPapeleria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridPapeleria.DataKeys[index].Values["ruta"].ToString();
            string nombre = gridPapeleria.DataKeys[index].Values["nombre"].ToString();
            string extension = gridPapeleria.DataKeys[index].Values["extension"].ToString();
            DataTable papeleria = (DataTable)Session["papeleria_perfil"];
            int papeleriat = papeleria.Rows.Count;
            switch (e.CommandName)
            {
                case "Eliminar":
                    List<DataRow> rowsToDelete = new List<DataRow>();
                    foreach (DataRow row in papeleria.Rows)
                    {
                        if (row["nombre"].ToString().Equals(nombre))
                        {
                            rowsToDelete.Add(row);
                        }
                    }
                    foreach (DataRow rowde in rowsToDelete)
                    {
                        papeleria.Rows.Remove(rowde);
                    }
                    Session["papeleria_perfil"] = papeleria;
                    gridPapeleria.DataSource = papeleria;
                    gridPapeleria.DataBind();

                    break;

                case "Descargar":
                    Download(ruta, nombre);
                    break;
            }
        }

        /// <summary>
        /// Descarga un archivo
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file_name"></param>
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
    }
}