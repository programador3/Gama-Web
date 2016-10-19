using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class areas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["idc_area"] = null;
                Session["idc_areaed"] = null;
                Session["url_origen"] = null;
                CargaAreas();
            }
        }

        private void CargaAreas()
        {
            try
            {
                LugaresENT enti = new LugaresENT();
                LugaresCOM com = new LugaresCOM();
                gridareas.DataSource = com.CargaAreas(enti).Tables[0];
                gridareas.DataBind();
                CopyTofolder(com.CargaAreas(enti).Tables[0], "~/imagenes/areas/", "idc_area");
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
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
        /// Carga las sucursales
        /// </summary>
        private void Sucursales()
        {
            try
            {
                LugaresENT entidad = new LugaresENT();
                LugaresCOM comp = new LugaresCOM();
                DataSet ds = comp.CargaSucursales(entidad);
                ddldeptos.DataValueField = "idc_sucursal";
                ddldeptos.DataTextField = "nombre";
                ddldeptos.DataSource = ds.Tables[0];
                ddldeptos.DataBind();
                ddldeptos.Items.Insert(0, new ListItem("--Seleccione una Sucursal", "0")); //updated code}
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void gridareas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_area = Convert.ToInt32(gridareas.DataKeys[index].Values["idc_area"].ToString());
            int idc_sucursal = Convert.ToInt32(gridareas.DataKeys[index].Values["idc_sucursal"].ToString());
            string nombre = gridareas.DataKeys[index].Values["nombre"].ToString();
            string ruta = gridareas.DataKeys[index].Values["ruta"].ToString();
            string url = System.Configuration.ConfigurationManager.AppSettings["server"] + "imagenes\\areas\\" + Path.GetFileName(ruta);
            string path = System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
            Session["idc_area"] = idc_area.ToString();
            Session["idc_areaed"] = idc_area;
            switch (e.CommandName)
            {
                case "Lugares":
                    Session["confirma"] = "Lugares";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Editar los Lugares de Trabajo del area " + gridareas.DataKeys[index].Values["nombre"].ToString() + "?');", true);
                    break;

                case "Editar":
                    Sucursales();
                    txtnombre.Text = nombre;
                    ddldeptos.SelectedValue = idc_sucursal.ToString();
                    imgUpdate.ImageUrl =  url;                    
                    imgUpdate.Visible = (File.Exists( path + url));                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalA('Edición del Area " + nombre + "');", true);
                    break;

                case "Ver":
                    imgmodal.ImageUrl = url;
                    imgmodal.Visible = (File.Exists(path + url));
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "aleecececcecrtMessage", "ModalImgc('" + nombre + "','modal fade modal-info');", true);
                    break;

                case "Borrar":
                    Session["confirma"] = "Borrar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Eliminar el area " + gridareas.DataKeys[index].Values["nombre"].ToString() + "?');", true);
                    break;
            }
        }

       

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string confirma = (string)Session["confirma"];
                LugaresENT entidad = new LugaresENT();
                int idc_area = Session["idc_areaed"] == null ? 0 : Convert.ToInt32(Session["idc_areaed"]);
                entidad.Pidc_area = idc_area;
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                LugaresCOM componente = new LugaresCOM();
                DataSet ds = new DataSet();
                string vmensaje = "";
                switch (confirma)
                {
                    case "Lugares":
                        string url = "lugares_captura.aspx?idc_area=" + funciones.deTextoa64(idc_area.ToString().Trim());
                        Response.Redirect(url, false);
                        Context.ApplicationInstance.CompleteRequest();
                        break;

                    case "Area":
                        entidad.Pnombre = txtnombre.Text;
                        entidad.pidc_sucursal = Convert.ToInt32(ddldeptos.SelectedValue);
                        ds = componente.AgregarArea(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            bool correct = true;
                            string ruta_det = ds.Tables[1].Rows[0]["ruta_destino"].ToString();
                            string ruta_origen = (string)Session["url_origen"];
                            if (ruta_origen != null && ruta_origen != "")
                            {
                                correct = funciones.CopiarArchivos(ruta_origen, ruta_det, this);
                            }
                           
                            if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo " + ruta_origen + "a la ruta " + ruta_det, this); }
                            Session["idc_areaed"] = null;
                            txtnombre.Text = "";
                            lblmensajeerror.Text = "";
                            error.Visible = false;
                            Session["url_origen"] = null;
                            Alert.ShowGiftMessage("Estamos procesando la cantidad de 1 archivo(s) al Servidor.", "Espere un Momento", "areas.aspx", "imagenes/loading.gif", "2000", "Movimiento realizado correctamente", this);
                        }
                        else
                        {
                            error.Visible = true;
                            lblmensajeerror.Text = vmensaje;
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalA('Ágregar Nueva Area');", true);
                        }
                        break;

                    case "Borrar":
                        ds = componente.EliminarArea(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Session["idc_areaed"] = null;
                            txtnombre.Text = "";
                            lblmensajeerror.Text = "";
                            error.Visible = false;
                            Session["url_origen"] = null;
                            Alert.ShowGiftMessage("Estamos procesando la solicitud", "Espere un Momento", "areas.aspx", "imagenes/loading.gif", "2000", "Movimiento realizado correctamente", this);
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

        protected void lnk_Click(object sender, EventArgs e)
        {
            Sucursales();
            imgUpdate.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalA('Ágregar Nueva Area');", true);
        }

        protected void ddldeptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int depto = Convert.ToInt32(ddldeptos.SelectedValue);
            if (depto == 0)
            {
                error.Visible = true;
                lblmensajeerror.Text = "Seleccione una Sucursal";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalA('Ágregar Nueva Area');", true);
            }
        }

        protected void aceptar_Click(object sender, EventArgs e)
        {
            error.Visible = false;
            lblmensajeerror.Text = "";
            if (txtnombre.Text == "")
            {
                error.Visible = true;
                lblmensajeerror.Text = "ingrese el Nombre del Area";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalA('Ágregar Nueva Area');", true);
                //Alert.ShowAlertError("ingrese el Nombre del Area", this);
            }
            else if (ddldeptos.SelectedValue == "0")
            {
                error.Visible = true;
                lblmensajeerror.Text = "Seleccione una Sucursal";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalA('Ágregar Nueva Area');", true);
                // Alert.ShowAlertError("Seleccione una Sucursal", this);
            }                        
            else
            {
                
                if (fupPapeleria.HasFile)
                {
                    if (Path.GetExtension(fupPapeleria.FileName) != ".jpg")
                    {
                        error.Visible = true;             
                        lblmensajeerror.Text = "La Imagen debe ser en formato JPG";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalA('Ágregar Nueva Area');", true);
                    }
                    else
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/imagenes/areas/captura/"));//path local
                        funciones.UploadFile(fupPapeleria, dirInfo + fupPapeleria.FileName, this.Page);
                        Session["url_origen"] = dirInfo + fupPapeleria.FileName;
                    }
                    
                }
                
                Session["confirma"] = "Area";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar los Cambios para esta Area?');", true);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session["idc_areaed"] = null;
            txtnombre.Text = "";
            lblmensajeerror.Text = "";
            error.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMesedededsage", "ModalClose();", true);
        }
    }
}