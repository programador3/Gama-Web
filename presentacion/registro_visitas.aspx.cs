using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using System.Collections;

namespace presentacion
{
    public partial class registro_visitas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }

            string imguri = Session["url_imagen_visitante"]==null?"": Session["url_imagen_visitante"] as string;
            
            if (!IsPostBack)
            {
                Session["url_imagen_visitante"] = null;
                Session["idc_visitap"] = null;
                Session["idc_visitaemp"] = null;
                Session["idc_visitareg"] = null;
                txtpbservaciones.Visible = false;
                CargaPuestos("");
                CargarVisitas(0);
                if (Request.InputStream.Length > 0 && imguri == "")
                {
                    using (StreamReader reader = new StreamReader(Request.InputStream))
                    {
                        string hexString = Server.UrlEncode(reader.ReadToEnd());
                        string imageName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");

                        Random random_edit = new Random();
                        int randomNumber_live = random_edit.Next(0, 100000);
                        DateTime localDate = DateTime.Now;
                        string date = localDate.ToString();
                        date = date.Replace("/", "_");
                        date = date.Replace(":", "_");
                        int idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        string name = idc_usuario.ToString().Trim() + "_" + date;
                        string imagePath = string.Format("~/temp/files/{0}.png", name);
                        File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
                        Session["CapturedImage"] = ResolveUrl(imagePath);
                        Session["url_imagen_visitante"] = imagePath;
                        txtimgurl.Text = imagePath;
                        imgCapture.Visible = true;
                    }
                }

            }
            if (imguri != "")
            {
                imgCapture.Attributes["src"] = ResolveUrl(imguri);
                imgCapture.Style.Clear();
                imgCapture.Style.Add("display", "block");
                imgCapture.Style.Add("height", "300");
            }


        }
        private static byte[] ConvertHexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        [WebMethod(EnableSession = true)]
        public static string GetCapturedImage()
        {
            string url = HttpContext.Current.Session["CapturedImage"].ToString();
            HttpContext.Current.Session["CapturedImage"] = null;
            return url;
        }
        /// <summary>
        /// Regresa tabla con coincidencias de visitantes
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public DataTable CargaPersonas(string filtro)
        {
            try
            {
                VisitasENT entidad = new VisitasENT();
                VisitasCOM componente = new VisitasCOM();
                entidad.Pnombre = filtro;
                DataSet ds = componente.CargaPerfiles(entidad);
                DataTable dt = new DataTable();
                return ds.Tables.Count > 0 ? ds.Tables[0] : dt;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
                return null;
            }
        }

        /// <summary>
        /// Regresa tabla con coincidencias de empresa
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public DataTable CargaEmpresa(string filtro)
        {
            try
            {
                VisitasENT entidad = new VisitasENT();
                VisitasCOM componente = new VisitasCOM();
                entidad.Pnombre = filtro;
                DataSet ds = componente.CargaeMPRESA(entidad);
                DataTable dt = new DataTable();
                return ds.Tables.Count > 0 ? ds.Tables[0] : dt;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
                return null;
            }
        }

        /// <summary>
        /// Carga Puestos en Filtro
        /// </summary>
        public void CargaPuestos(string filtro)
        {
            try
            {
                Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
                Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
                entidad.Filtro = filtro;
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = componente.CargaComboDinamicoOrgn(entidad);
                ddlPuestoAsigna.DataValueField = "idc_empleado";
                ddlPuestoAsigna.DataTextField = "descripcion_puesto_completa";
                ddlPuestoAsigna.DataSource = ds.Tables[0];
                ddlPuestoAsigna.DataBind();
                if (filtro == "")
                {
                    ddlPuestoAsigna.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Seleccione un Empleado", "0")); //updated code}
                }
                else
                {
                    int IDC = ds.Tables[0].Rows.Count > 0 ? Convert.ToInt32(ddlPuestoAsigna.SelectedValue) : 0;
                    CargarVisitas(IDC);
                    lbltitle.Text = ddlPuestoAsigna.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public void CargarVisitas(int idc_empleado)
        {
            try
            {
                VisitasENT entidad = new VisitasENT();
                VisitasCOM componente = new VisitasCOM();
                entidad.Pidc_empleado = idc_empleado;
                DataSet ds = componente.CragarVisitas(entidad);
                gridvisitas.DataSource = ds.Tables[0];
                gridvisitas.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void txtnombre_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtnombre.Text;
            if (filtro != "")
            {
                DataTable dt = CargaPersonas(filtro);
                if (dt.Rows.Count > 0)
                {
                    repeatpersona.DataSource = dt;
                    repeatpersona.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirmPero('Se encontraron " + dt.Rows.Count.ToString() + " coincidencia(s) del visitante " + filtro.ToUpper() + ". Seleccione uno de la lista o indique que sera un nuevo registro.','modal fade modal-info');", true);
                }
                else
                {
                    Session["idc_visitap"] = null;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                }
            }
        }

        protected void lnkpersona_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            Session["idc_visitap"] = lnk.CommandName;
            txtnombre.Text = lnk.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
        }

        protected void lnkpersonas_Click(object sender, EventArgs e)
        {
            Session["idc_visitap"] = null;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
        }

        protected void txtempresa_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtempresa.Text;
            if (filtro != "")
            {
                DataTable dt = CargaEmpresa(filtro);
                if (dt.Rows.Count > 0)
                {
                    repeatempresa.DataSource = dt;
                    repeatempresa.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirmEmpresa('Se encontraron " + dt.Rows.Count.ToString() + " coincidencia(s) de la empresa " + filtro.ToUpper() + ". Seleccione uno de la lista o indique que sera un nuevo registro.','modal fade modal-info');", true);
                }
                else
                {
                    Session["idc_visitaemp"] = null;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
                }
            }
        }

        protected void lnkempresa_Click(object sender, EventArgs e)
        {
            Session["idc_visitaemp"] = null;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
        }

        protected void lnkemp_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            Session["idc_visitaemp"] = lnk.CommandName;
            txtempresa.Text = lnk.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalClose();", true);
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            CargaPuestos(txtpuesto_filtro.Text);
        }

        protected void ddlPuestoAsigna_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idc = ddlPuestoAsigna.SelectedValue == "" ? 0 : Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
            if (idc == 0)
            {
                Alert.ShowAlertError("Seleccione un Empleado", this);
            }
            else
            {
                CargarVisitas(idc);
                lbltitle.Text = ddlPuestoAsigna.SelectedItem.ToString();
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso = (string)Session["Caso_Confirmacion"];
            try
            {
                VisitasENT entidad = new VisitasENT();
                VisitasCOM componente = new VisitasCOM();
                entidad.Pidc_empleado = Convert.ToInt32(ddlPuestoAsigna.SelectedValue);
                int idc_visitap = Session["idc_visitap"] == null ? 0 : Convert.ToInt32(Session["idc_visitap"]);
                int idc_visitaemp = Session["idc_visitaemp"] == null ? 0 : Convert.ToInt32(Session["idc_visitaemp"]);
                entidad.Pidc_visitaemp = idc_visitaemp;
                entidad.Pidc_visitap = idc_visitap;
                entidad.Pmotivo = txtmotivo.Text;
                entidad.Pnombre = txtnombre.Text;
                entidad.Pnombreempresa = txtempresa.Text;
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                string imgurl = Session["url_imagen_visitante"] == null ? "" : Session["url_imagen_visitante"] as string;
                DataSet ds = new DataSet();
                string vmensaje = "";
                string vmensajee = "";
                switch (caso)
                {
                    case "Guardar":
                        if (imgurl != "")
                        { entidad.PURL = imgurl; }
                        ds = componente.AgregarVisita(entidad);
                        break;

                    case "Terminar":
                        entidad.Pmotivo = txtpbservaciones.Text;
                        entidad.Pidc_visitareg = Convert.ToInt32(Session["idc_visitareg"]);
                        ds = componente.TerminarVisita(entidad);
                        break;
                }

                vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                bool copiar = Convert.ToBoolean(ds.Tables[0].Rows[0]["COPIAR_FOTO"]);
                if (copiar)
                {
                    DirectoryInfo dirInfo2 = new DirectoryInfo(Server.MapPath("~/temp/files/"));//path local
                    string origen = dirInfo2 + Path.GetFileName(imgurl);
                    string destino = ds.Tables[0].Rows[0]["ruta_destino"].ToString();
                    File.Copy(origen, destino,true);
                    Gaffete(dirInfo2.ToString(), origen);
                }

                vmensajee = ds.Tables[0].Rows[0]["mensaje_extra"].ToString() == "" ? "Visita Procesada Correctamente" : ds.Tables[0].Rows[0]["mensaje_extra"].ToString();
                if (vmensaje == "")
                {
                    Session["url_imagen_visitante"] = null;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMeededssage", "ModalClose();", true);
                    Alert.ShowGiftMessage("Estamos procesando la visita.", "Espere un Momento", "registro_visitas.aspx", "imagenes/loading.gif", "2000", vmensajee, this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
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
            int idc = Convert.ToInt32(ddlPuestoAsigna.SelectedValue);

            if (idc == 0)
            {
                Alert.ShowAlertError("Seleccione un Empleado", this);
            }
            else if (txtnombre.Text == "")
            {
                Alert.ShowAlertError("Escriba el Nombre del Visitante", this);
            }
            else if (txtmotivo.Text == "")
            {
                Alert.ShowAlertError("Escriba el Motivo del Visitante", this);
            }
            else
            {
                txtpbservaciones.Visible = false;
                txtpbservaciones.Text = "";
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar esta Visita.? Se le enviara un correo al Empleado " + ddlPuestoAsigna.SelectedItem + " de manera automatica.','modal fade modal-info');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("registro_visitas.aspx");
        }

        protected void gridvisitas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc = Convert.ToInt32(gridvisitas.DataKeys[index].Values["idc_visitareg"]);
            switch (e.CommandName)
            {
                case "Terminar":
                    Session["idc_visitareg"] = idc;
                    Session["Caso_Confirmacion"] = "Terminar";
                    txtpbservaciones.Visible = true;

                    txtpbservaciones.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Terminar esta Visita.? ','modal fade modal-info');", true);

                    break;
            }
        }

        protected void lnkurladicinal_Click(object sender, EventArgs e)
        {
            string pagina = "tareas_informacion_adicional.aspx?idc_tipoi=" + funciones.deTextoa64("4") + "&idc_proceso=" + funciones.deTextoa64("0");
            String url = HttpContext.Current.Request.Url.AbsoluteUri;
            String path_actual = url.Substring(url.LastIndexOf("/") + 1);
            url = url.Replace(path_actual, "");
            url = url + pagina;
            ScriptManager.RegisterStartupScript(this, GetType(), "noti533W3", "window.open('" + url + "');", true);
        }

        protected void LNKUPDATE_Click(object sender, EventArgs e)
        {
            CargarVisitas(0);
        }

        public string Gaffete(string NameDoc, string img)
        {
            try
            {
                iTextSharp.text.Font myfont = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                DateTime localDate = DateTime.Now;
                string date = localDate.ToString();
                date = date.Replace("/", "_");
                date = date.Replace(":", "_");
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                string name = idc_usuario.ToString().Trim() + "_" + date;
                FileStream fs = new FileStream(NameDoc + name + ".pdf", FileMode.Create, FileAccess.Write, FileShare.None);
                Document document = new Document(PageSize.A4, 20f, 20f, 5f, 20f);
                System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                iTextSharp.text.Font titlefont = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 7, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
                iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 6);
                document.Open();

                iTextSharp.text.Font fuente = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 38,
                    iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);//LA FUENTE DE NUESTRO TEXTO
                iTextSharp.text.Font fuente_subtitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);//LA FUENTE DE NUESTRO TEXTO
                iTextSharp.text.Font fuente_p = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12);//LA FUENTE DE NUESTRO TEXTO
                iTextSharp.text.Font fuente_F = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);//LA FUENTE DE NUESTRO TEXTO
                iTextSharp.text.Font fuente_Fp = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12);//LA FUENTE DE NUESTRO TEXTO
                Paragraph Titulo = new Paragraph("  VISITANTE", fuente);
                Titulo.Alignment = Element.ALIGN_LEFT;
                Paragraph Fecha = new Paragraph("    "+DateTime.Now.ToString("dddd dd MMMM, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper(), fuente_Fp);
                Fecha.Alignment = Element.ALIGN_LEFT;
                Paragraph espacio = new Paragraph(" ");
                Paragraph nombre = new Paragraph("    "+txtnombre.Text.ToUpper(),fuente_F);
                Paragraph EMPRESA = new Paragraph("    " + txtempresa.Text.ToUpper(), fuente_F);

                DirectoryInfo dirInfo2 = new DirectoryInfo(Server.MapPath("~/imagenes/"));//path local
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(img);
                float percentage = 1f;
                percentage = 150 / jpg.Width;
                jpg.ScalePercent(percentage * 150);
                PdfContentByte canvas = writer.DirectContentUnder;
                jpg.SetAbsolutePosition(25, 580);
                string img2 = dirInfo2+ "fondo_gaffete.png";
                iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(img2);
                float percentage2 = 1f;
                percentage2 = 150 / jpg2.Width;
                jpg2.ScalePercent(percentage2 * 150);
                jpg2.SetAbsolutePosition(25, 450);
                canvas.SaveState();
                PdfGState state = new PdfGState();
                state.FillOpacity = (.2f);
                canvas.SetGState(state);
                canvas.AddImage(jpg);
                canvas.RestoreState();
                PdfContentByte canvas2 = writer.DirectContentUnder;
                canvas2.SaveState();
                PdfGState state2 = new PdfGState();
                state.FillOpacity = (.2f);
                canvas2.SetGState(state2);
                canvas2.AddImage(jpg2);
                canvas2.RestoreState();
                document.Add(jpg);
                document.Add(jpg2);
                document.Add(Titulo);
                document.Add(Fecha);
                document.Add(espacio);
                document.Add(espacio);
                document.Add(espacio);
                document.Add(espacio);
                document.Add(espacio);
                document.Add(espacio);
                document.Add(espacio);
                document.Add(espacio);
                document.Add(espacio);
                document.Add(espacio);
                document.Add(espacio);
                document.Add(nombre);
                document.Add(EMPRESA);
                document.Close();
                string pageName = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                pageName = pageName.Replace("registro_visitas.aspx", "");

                ScriptManager.RegisterStartupScript(this, GetType(), "noti533W3", "window.open('" + pageName+ "temp/files/" + name + ".pdf" + "');", true);
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string imgurl = Session["url_imagen_visitante"] == null ? "" : Session["url_imagen_visitante"] as string;
            DirectoryInfo dirInfo2 = new DirectoryInfo(Server.MapPath("~/temp/files/"));//path local
            string origen = dirInfo2 + Path.GetFileName(imgurl);

            Gaffete(dirInfo2.ToString(), origen);
        }
    }
}