using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class lugares_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null || Request.QueryString["idc_area"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["idc_lugartedit"] = null;
                Session["nombre_lugar"] = null;
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_lugart");
                dt.Columns.Add("alias");
                dt.Columns.Add("nombre");
                dt.Columns.Add("ruta");
                dt.Columns.Add("img");
                dt.Columns.Add("lugar");
                Session["tabla_lugares"] = dt;
                int idc_area = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_area"]));
                TablaDatosAreas(idc_area);
            }
        }

        /// <summary>
        /// Carga los datos de SQL
        /// </summary>
        /// <param name="idc_puesto"></param>
        /// <param name="idc_area"></param>
        private void TablaDatosAreas(int idc_area)
        {
            LugaresENT entidades = new LugaresENT();
            LugaresCOM com = new LugaresCOM();
            entidades.Pidc_area = idc_area;
            DataSet ds = com.CargaAreas(entidades);
            DataRow row = ds.Tables[0].Rows[0];
            CopyTofolder(ds.Tables[0], "~/imagenes/areas/", "idc_area");
            lbltitle.Text = "Lugares de Trabajo del Area '" + row["nombre"].ToString() + "'";
            lblareaname.Text = row["nombre"].ToString();
            string url = System.Configuration.ConfigurationManager.AppSettings["server"] + "/imagenes/areas/" + row["idc_area"].ToString() + ".jpg";
            imgarea.ImageUrl = url;
            foreach (DataRow rows in ds.Tables[1].Rows)
            {
                AddToTable(Convert.ToInt32(rows["idc_lugart"]), rows["alias"].ToString(), rows["nombre"].ToString(), rows["ruta"].ToString(), rows["img"].ToString(), Convert.ToInt32(rows["lugar"]));
            }
        }

        /// <summary>
        /// Comprueba si existe un lugar con nombres y alias similares
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        private bool ExistsinTable(string nombre, string alias)
        {
            DataTable tbl = (DataTable)Session["tabla_lugares"];
            DataView view = tbl.DefaultView;
            view.RowFilter = "nombre = '" + nombre + "' OR alias = '" + alias + "'";
            if (view.ToTable().Rows.Count > 0)
            {
                return true;//existe
            }
            else
            {
                return false;//no existe
            }
        }

        /// <summary>
        /// Agrega Registro a tabla
        /// </summary>
        private void AddToTable(int idc_lugart, string alias, string nombre, string ruta, string img, int lugar)
        {
            DataTable tbl = (DataTable)Session["tabla_lugares"];
            foreach (DataRow rows in tbl.Rows)
            {
                //if (Convert.ToInt32(rows["idc_lugart"]) == idc_lugart && idc_lugart > 0)
                //{
                //    rows.Delete();
                //    break;
                //}
                if (Session["nombre_lugar"] != null && rows["nombre"].ToString() == (string)Session["nombre_lugar"])
                {
                    rows.Delete();
                    break;
                }
            }
            DataRow row = tbl.NewRow();
            row["idc_lugart"] = idc_lugart;
            row["alias"] = alias;
            row["nombre"] = nombre;
            row["ruta"] = ruta;
            row["img"] = img;
            row["lugar"] = lugar;
            tbl.Rows.Add(row);
            CopyTofolder(tbl, "~/imagenes/lugares/captura/", "idc_lugart");
            cargargrid();
        }

        /// <summary>
        /// Elimina registro de tabla
        /// </summary>
        /// <param name="idc_lugart"></param>
        private void DeleteToTable(int idc_lugart)
        {
            DataTable tbl = (DataTable)Session["tabla_lugares"];
            foreach (DataRow row in tbl.Rows)
            {
                if (Convert.ToInt32(row["idc_lugart"]) == idc_lugart)
                {
                    row.Delete();
                    break;
                }
            }
            Session["tabla_lugares"] = tbl;
            cargargrid();
            Alert.ShowAlert("Lugar eliminado correctamente", "Mensaje del sistema", this);
        }

        /// <summary>
        /// Carga el grid de lugares
        /// </summary>
        private void cargargrid()
        {
            DataTable dt = (DataTable)Session["tabla_lugares"];
            DataTable ntble = new DataTable();
            ntble.Columns.Add("idc_lugart");
            ntble.Columns.Add("alias");
            ntble.Columns.Add("nombre");
            ntble.Columns.Add("ruta");
            ntble.Columns.Add("img");
            ntble.Columns.Add("lugar");
            foreach (DataRow row in dt.Rows)
            {
                DataRow newr = ntble.NewRow();
                newr["idc_lugart"] = row["idc_lugart"];
                newr["alias"] = row["alias"];
                newr["nombre"] = row["nombre"];
                newr["img"] = row["img"];
                newr["ruta"] = row["ruta"];
                newr["lugar"] = row["lugar"];
                ntble.Rows.Add(newr);
            }
            gridlugares.DataSource = ntble;
            gridlugares.DataBind();
        }

        /// <summary>
        /// Copia las imagenes a la ruta local
        /// </summary>
        /// <param name="tbl"></param>
        /// <param name="folder"></param>
        private void CopyTofolder(DataTable tbl, string folder, string idc)
        {
            try
            {
                DirectoryInfo dirInfo2 = new DirectoryInfo(Server.MapPath("~/imagenes/lugares/"));//path local
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath(folder));//path local
                foreach (DataRow row in tbl.Rows)
                {
                    String RUTA = row["ruta"].ToString();
                    String RUTA_LOCAL = row[idc].ToString() + ".jpg";
                    if (File.Exists(RUTA))
                    {                     //   funciones.CopiarArchivos(dirInfo2 + "lugar.png", dirInfo + RUTA_LOCAL, this);
                        funciones.CopiarArchivos(RUTA, dirInfo + RUTA_LOCAL, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// Regresa la cadena de luagres
        /// </summary>
        /// <returns></returns>
        private String Cadena()
        {
            string cadena = "";
            DataTable tbl = (DataTable)Session["tabla_lugares"];
            foreach (DataRow row in tbl.Rows)
            {
                cadena = cadena + row["idc_lugart"].ToString() + ";" + row["nombre"].ToString() + ";" + row["alias"].ToString() + ";" + row["ruta"].ToString() + ";";
            }
            return cadena;
        }

        /// <summary>
        /// Regresa el total de la cadena de lugares
        /// </summary>
        /// <returns></returns>
        private int TotalCadena()
        {
            DataTable tbl = (DataTable)Session["tabla_lugares"];
            return tbl.Rows.Count;
        }

        protected void gridlugares_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Image ocupado = (Image)e.Row.FindControl("ocupado");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string img = rowView["img"].ToString();
                ocupado.ImageUrl = img;
            }
        }

        protected void gridlugares_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc = Convert.ToInt32(gridlugares.DataKeys[index].Values["idc_lugart"]);
            int ocupado = Convert.ToInt32(gridlugares.DataKeys[index].Values["lugar"]);
            string nombre = gridlugares.DataKeys[index].Values["nombre"].ToString();
            string alias = gridlugares.DataKeys[index].Values["alias"].ToString();
            string ruta = gridlugares.DataKeys[index].Values["ruta"].ToString();
            switch (e.CommandName)
            {
                case "Editar":
                    Session["idc_lugartedit"] = idc;
                    Session["nombre_lugar"] = nombre;
                    txtalias.Text = alias;
                    txtnombre.Text = nombre;
                    break;

                case "Borrar":
                    if (ocupado == 1)
                    {
                        Alert.ShowAlertError("El lugar esta ocupado.", this);
                    }
                    else
                    {
                        DeleteToTable(idc);
                    }
                    break;

                case "Ver":
                    string file = Path.GetFileName(ruta);
                    string url = System.Configuration.ConfigurationManager.AppSettings["server"] + "/imagenes/lugares/captura/" + file;
                    imgmodal.ImageUrl = url;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalImgc('" + nombre + "','modal fade modal-info');", true);
                    break;
            }
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            String FileExtension = fupPapeleria.HasFile == true ? System.IO.Path.GetExtension(fupPapeleria.FileName) : "";
            if (!fupPapeleria.HasFile)
            {
                Alert.ShowAlertError("Ingrese la Imagen del Lugar", this);
            }
            else if (FileExtension != ".jpg")
            {
                Alert.ShowAlertInfo("Solo se permiten formatos de imagen JPG || PNG", "Mensaje del sistema", this);
            }
            else if (txtalias.Text == "")
            {
                Alert.ShowAlertInfo("Escriba un Alias para el lugar", "Mensaje del sistema", this);
            }
            else if (txtnombre.Text == "")
            {
                Alert.ShowAlertInfo("Escriba el nombre completo del lugar", "Mensaje del sistema", this);
            }
            else if (ExistsinTable(txtnombre.Text, txtalias.Text) == true)
            {
                Alert.ShowAlertError("Ya existe un  Lugar con el nombre o alias.", this);
            }
            else
            {
                int idc_lugart = Session["idc_lugartedit"] == null ? 0 : Convert.ToInt32(Session["idc_lugartedit"]);
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/imagenes/lugares/captura/"));//path local
                AddToTable(idc_lugart, txtalias.Text, txtnombre.Text, dirInfo + fupPapeleria.FileName, "imagenes/btn/inchecked.png", 0);
                bool pape = funciones.UploadFile(fupPapeleria, dirInfo + fupPapeleria.FileName, this.Page);
                if (pape == true)
                {
                    Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "2000", "Lugar Guardardo Correctamente", this);
                    Session["idc_lugartedit"] = null;
                    Session["nombre_lugar"] = null;
                    txtnombre.Text = "";
                    txtalias.Text = "";
                }
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                switch (caso)
                {
                    case "Cancelar":
                        Response.Redirect("areas.aspx");
                        break;

                    case "Guardar":
                        LugaresENT entidad = new LugaresENT();
                        int idc_area = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_area"]));
                        entidad.Pidc_area = idc_area;
                        entidad.Pcadena = Cadena();
                        entidad.Ptotalcadea = TotalCadena();
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        LugaresCOM componente = new LugaresCOM();
                        DataSet ds = componente.AgregarLugares(entidad);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            DataTable tabla_archivos = ds.Tables[1];
                            bool correct = true;
                            foreach (DataRow row_archi in tabla_archivos.Rows)
                            {
                                string ruta_det = row_archi["ruta_destino"].ToString();
                                string ruta_origen = row_archi["ruta_origen"].ToString();
                                if (ruta_det != ruta_origen)
                                {
                                    correct = funciones.CopiarArchivos(ruta_origen, ruta_det, this.Page);
                                }
                                if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo " + ruta_origen + "a la ruta " + ruta_det, this); }
                            }
                            Alert.ShowGiftMessage("Estamos procesando la cantidad de " + tabla_archivos.Rows.Count.ToString() + " archivo(s) al Servidor.", "Espere un Momento", "areas.aspx", "imagenes/loading.gif", "4000", "Movimiento realizado correctamente", this);
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
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar los Cambios?','modal fade modal-info');", true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?','modal fade modal-info');", true);
        }
    }
}