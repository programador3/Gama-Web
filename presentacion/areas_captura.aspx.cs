using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class areas_captura : System.Web.UI.Page
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
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("nombre");
                Session["papeleria_areas"] = papeleria;
                cargarDatos();
            }
        }

        private void cargarDatos()
        {
            LugaresENT entidades = new LugaresENT();
            LugaresCOM com = new LugaresCOM();
            DataSet ds = com.CargaSucursales(entidades);
            ddlsucursales.DataTextField = "nombre";
            ddlsucursales.DataValueField = "idc_sucursal";
            ddlsucursales.DataSource = ds.Tables[0];
            ddlsucursales.DataBind();
            ddlsucursales.Items.Insert(0, new ListItem("Seleccione uno", "0")); //updated code}
        }

        protected void ddlsucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            int value_index = Convert.ToInt32(ddlsucursales.SelectedValue);
            if (value_index == 0)
            {
                Alert.ShowAlertError("Seleccione una Sucursal", this);
            }
        }

        protected void lnkGuardarPape_Click(object sender, EventArgs e)
        {
            if (fupPapeleria.HasFile)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 1000);
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/areas_captura/"));//path local
                string mensaje = AddPapeleriaToTable(dirInfo + randomNumber.ToString() + fupPapeleria.FileName, fupPapeleria.FileName);
                if (mensaje.Equals(string.Empty))
                {
                    bool pape = funciones.UploadFile(fupPapeleria, dirInfo + randomNumber.ToString() + fupPapeleria.FileName, this);
                    if (pape == true)
                    {
                        REV.Enabled = gridPapeleria.Rows.Count == 0 ? true : false;
                        Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "5000", "Archivo Guardardo Correctamente", this);
                        fupPapeleria.Visible = true;
                    }
                }
                else
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }

        public string AddPapeleriaToTable(string ruta, string nombre)
        {
            string mensaje = "";
            bool exists = false;
            DataTable papeleria = (DataTable)Session["papeleria_areas"];
            if (papeleria.Rows.Count > 0)
            {
                mensaje = "Elimine el anterior si desea actualizarlo.";
            }
            if (papeleria.Rows.Count == 0)
            {
                DataRow new_row = papeleria.NewRow();
                new_row["nombre"] = nombre;
                new_row["ruta"] = ruta;
                papeleria.Rows.Add(new_row);
                gridPapeleria.DataSource = papeleria;
                gridPapeleria.DataBind();
                Session["papeleria_areas"] = papeleria;
            }
            return mensaje;
        }

        protected void gridPapeleria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridPapeleria.DataKeys[index].Values["ruta"].ToString();
            string nombre = gridPapeleria.DataKeys[index].Values["nombre"].ToString();
            DataTable papeleria = new DataTable();
            switch (e.CommandName)
            {
                case "Eliminar":
                    papeleria.Columns.Add("ruta");
                    papeleria.Columns.Add("nombre");
                    Session["papeleria_areas"] = papeleria;
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

        protected void Yes_Click(object sender, EventArgs e)
        {
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            bool error = false;
            if (txtdescripcion.Text == "" || ddlsucursales.SelectedValue == "")
            {
                error = true;
                Alert.ShowAlertError("Ingrese el Nombre del Area.", this);
            }
            if (ddlsucursales.SelectedValue == "0")
            {
                error = true;
                Alert.ShowAlertError("Seleccione una Sucursal.", this);
            }
            if (gridPapeleria.Rows.Count == 0)
            {
                error = true;
                Alert.ShowAlertError("Debe Ingresar el Area.", this);
            }
            if (error == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea guardar el perfil " + txtdescripcion.Text + "?');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("catalogo_areas.aspx");
        }
    }
}