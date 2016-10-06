 using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class ficha_cliente_m : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (Session["idc_cliente"] == null || Session["idc_agente"] == null)//si no hay session logeamos
            {
                Response.Redirect("Captura_Actividades_Agentes2.aspx");
            }
            if (!IsPostBack)
            {
                VerificarRegistroGPS();
                CargarGrupos();
            }
        }

        private void CargarGrupos()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = funciones.ExecQuery("select * from dbo.fn_gpocli(" + Session["num_grupo"] as string + ") where idc_agente=" + Session["idc_agente"] as string);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        dt.Rows[i]["nombre"] = (!string.IsNullOrEmpty(dt.Rows[i]["cveadi"].ToString().Trim()) ? dt.Rows[i]["cveadi"].ToString() + " | " + dt.Rows[i]["nombre"].ToString() : dt.Rows[i]["nombre"].ToString());
                    }
                    cbogrupos.DataSource = dt;
                    cbogrupos.DataTextField = "nombre";
                    cbogrupos.DataValueField = "idc_cliente";
                    cbogrupos.DataBind();
                    Session["idc_cliente"] = cbogrupos.SelectedValue;
                    divgrupos.Visible = true;
                    CargarFichaTecnica(Convert.ToInt32(dt.Rows[0]["idc_cliente"]));
                }
                else
                {
                    cbogrupos.Items.Clear();
                    divgrupos.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void VerificarRegistroGPS()
        {
            try
            {
                DataTable dt = funciones.ExecQuery("select dbo.fn_actividad_registrada_age_hoy(" + Session["idc_agente"] as string + "," + Session["idc_cliente"] as string + ") as id");
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["id"]) == false)
                    {
                        lnkregistrarvisita.Visible = true;
                        lnkyaregis.Visible = false;
                    }
                    else
                    {
                        lnkyaregis.Visible = true;
                        lnkregistrarvisita.Visible = false;
                    }
                }
                else
                {
                    lnkregistrarvisita.Visible = false;
                    lnkyaregis.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void lnkregistrarvisita_Click(object sender, EventArgs e)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pidc_agente = Convert.ToInt32(Session["idc_agente"]);
                entidad.Pidc_cliente = Convert.ToInt32(Session["idc_cliente"]);
                entidad.Pidc_actiage = 4;
                entidad.Plat = Convert.ToSingle(oclatitud.Value);
                entidad.Plon = Convert.ToSingle(oclongitud.Value);
                DataSet ds = com.registrar_visita(entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos Registrando la Visita", "Espere un Momento", "ficha_cliente_m.aspx", "imagenes/loading.gif", "2000", "El registro por GPS fue guardado de manera correcta.", this);
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


        private void cargar_contactos_clientes(int idc_cliente)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                DataSet ds = new DataSet();
                entidad.Pidc_cliente = idc_cliente;
                ds = com.sp_clientes_tel_mtto1(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridcontactos.DataSource = ds.Tables[0];
                    gridcontactos.DataBind();
                    //imgmas.Visible = true;
                }
                else
                {
                    gridcontactos.DataSource = null;
                    gridcontactos.DataBind();
                    //imgmas.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
        private void CargarFichaTecnica(int idc_cliente)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pidc_cliente = idc_cliente;
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.cargar_ficha(entidad);
                if (ds.Tables.Count != 0)
                {
                    DataRow row = default(DataRow);
                    row = ds.Tables[0].Rows[0];
                    int ord_compra = row["ord_compra"] is DBNull ? 0 : Convert.ToInt32(row["ord_compra"]);
                    int ord_entrada = row["ord_entrada"] is DBNull ? 0 : Convert.ToInt32(row["ord_entrada"]);
                    //if (ord_compra > 0)
                    //{
                    //    if (Convert.ToBoolean(row["ord_compra"]) == true)
                    //    {
                    //        //txtoc.Text = "NO"
                    //        chkoc.Checked = true;
                    //        chkoc.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        //txtoc.Text = "SI"
                    //        chkoc.Checked = false;
                    //        chkoc.Visible = false;
                    //    }

                    //}
                    //else
                    //{
                    //    //txtoc.Text = "SI"
                    //    chkoc.Checked = false;
                    //    chkoc.Visible = false;
                    //}
                    //if (ord_entrada > 0)
                    //{
                    //    if (row("ord_entrada") == false)
                    //    {
                    //        txtoe.Text = "NO";

                    //    }
                    //    else
                    //    {
                    //        txtoe.Text = "SI";
                    //    }
                    //}
                    //else
                    //{
                    //    txtoe.Text = "NO";
                    //}

                    //if (!Information.IsDBNull(row("croquis")))
                    //{
                    //    if (row("croquis") == true)
                    //    {
                    //        //txtcroquis.Text = "NO"
                    //        chkcroquis.Checked = true;
                    //        chkcroquis.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        chkcroquis.Checked = false;
                    //        chkcroquis.Visible = false;
                    //    }
                    //}
                    //else
                    //{
                    //    chkcroquis.Checked = false;
                    //    chkcroquis.Visible = false;
                    //}

                    //if (!Information.IsDBNull(row("sello")))
                    //{
                    //    if (row("sello") == true)
                    //    {
                    //        //txtsello.Text = "NO"
                    //        chksello.Checked = true;
                    //        chksello.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        //txtsello.Text = "SI"
                    //        chksello.Checked = false;
                    //        chksello.Visible = false;
                    //    }
                    //}
                    //else
                    //{
                    //    chksello.Checked = false;
                    //    chksello.Visible = false;
                    //}

                    //txtultimaventa.Text = (!Information.IsDBNull(row("ult_venta")) ? row("ult_venta") : "");
                    int idc_listap = 0;
                    if (row["idc_listap"] != null)
                    {
                        if (Convert.ToInt32(row["idc_listap"]) > 0)
                        {
                            //txtmodelo.Text = row["idc_listap"].ToString() + " .- " + row["lista_precio"].ToString();
                            idc_listap = Convert.ToInt32(row["idc_listap"]);
                        }
                    }

                    if (Convert.ToBoolean(row["credito"]) == true)
                    {
                        cargar_credito_disponible(idc_cliente);
                        txtcreditodisponible.Visible = true;
                        lblcredito.Visible = true;
                        txtcredito.Text = "SI";
                    }
                    else
                    {
                        txtcreditodisponible.Visible = false;
                        lblcredito.Visible = false;
                        txtcredito.Text = "NO";
                    }

                    //|||||||||||||||||||||||||||||||||||||Ficha Dirección fiscal||||||||||||||||||||||||||||||||||||||||||||
                    status(Convert.ToInt32(row["idc_bloqueoc"]));
                    txtrfc.Text = row["rfccliente"].ToString().Trim();
                    txtcveadicional.Text = row["cveadi"].ToString().Trim();
                    txtid.Text = row["idc_cliente"].ToString().Trim();
                    txtcliente.Text = row["nombre"].ToString().Trim();
                    txtcliente.ToolTip = txtcliente.Text;
                    txttipopago.Text = row["id_corta"].ToString().Trim() + ".-" + row["tipo_pago"].ToString().Trim();
                    txtstatus.Text = row["idc_bloqueoc"].ToString().Trim();
                    txtagente.Text = row["idc_agente"].ToString().Trim();
                    if (row["nomgrupo"] is DBNull)
                    {
                        var _with1 = txtgrupocliente;
                        _with1.Text = "";
                        _with1.Visible = false;

                        var _with2 = txtnumerogrupo;
                        _with2.Text = "";
                        _with2.Visible = false;
                        var _with3 = lblgrupo;
                        _with3.Visible = false;
                    }
                    else if (row["nomgrupo"].ToString().Trim() == "SIN GRUPO")
                    {
                        var _with7 = txtgrupocliente;
                        _with7.Text = "";
                        _with7.Visible = false;

                        var _with8 = txtnumerogrupo;
                        _with8.Text = "";
                        _with8.Visible = false;
                        var _with9 = lblgrupo;
                        _with9.Visible = false;
                    }
                    else
                    {
                        var _with4 = txtgrupocliente;
                        _with4.Text = row["nomgrupo"].ToString();
                        _with4.Visible = true;

                        var _with5 = txtnumerogrupo;
                        _with5.Text = row["idc_gpocli"].ToString();
                        _with5.Visible = true;

                        var _with6 = lblgrupo;
                        _with6.Visible = true;
                    }

                    string direccion = "";
                    direccion = (string.IsNullOrEmpty(row["calle"].ToString().Trim()) ? "" : row["calle"].ToString().Trim()) + (string.IsNullOrEmpty(row["numero"].ToString().Trim()) ? "" : " #"
                        + row["numero"].ToString().Trim() + ",") + (string.IsNullOrEmpty(row["colonia"].ToString().Trim()) ? "" : " " +
                        row["colonia"].ToString().Trim() + ",") + (string.IsNullOrEmpty(row["cod_postal"].ToString().Trim()) ? "" : " " +
                        row["cod_postal"].ToString().Trim() + ",") + (string.IsNullOrEmpty(row["mpio"].ToString().Trim()) ? "" : " " +
                        row["mpio"].ToString().Trim() + ",") + (string.IsNullOrEmpty(row["edo"].ToString().Trim()) ? "" : " " +
                        row["edo"].ToString().Trim() + ",") + (string.IsNullOrEmpty(row["pais"].ToString().Trim()) ? "" : " " + row["pais"].ToString().Trim());

                    //|||||||||||||||||||||||||||||||||||||Ficha Info. de Internet||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
                    row = ds.Tables[0].Rows[0];
                    lblsitio.Text = row["pagina_web"].ToString();
                    if (!string.IsNullOrEmpty(lblsitio.Text.Trim()))
                    {
                        lblsitio.Attributes["onclick"] = "return AbSitioCliente(this);";
                    }
                    lblfacebook.Text = row["facebook"].ToString();
                    lbltwitter.Text = row["twitter"].ToString();

                    promociones_cliente(Convert.ToInt32(txtid.Text.Trim()), idc_listap);

                    cargar_contactos_clientes(Convert.ToInt32(txtid.Text.Trim()));
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void promociones_cliente(int idc_cliente, int idc_listap)
        {
            bool promociones_existen = false;
            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pidc_cliente = idc_cliente;
                entidad.vidc_listap = idc_listap;
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_promociones_cliente(entidad);
                promociones_existen = ds.Tables[0].Rows.Count > 0 ? true : false;
                if (promociones_existen == true)
                {
                    btnpromociones.Visible = true;
                    btnpromociones.Attributes["onclick"] = "window.open('promociones_cliente_m.aspx?cdi=" + idc_cliente + "&listap=" + idc_listap + "');";
                }
                else
                {
                    btnpromociones.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
                btnpromociones.Visible = false;
            }
        }

        private void cargar_credito_disponible(int idc_cliente)
        {
            DataSet ds = new DataSet();
            DataRow row = default(DataRow);
            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pidc_cliente = idc_cliente;
                AgentesCOM com = new AgentesCOM();
                ds = com.credito_disponible(entidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    txtcreditodisponible.Text = funciones.Redondeo_Dos_Decimales(Convert.ToDecimal(row["disponible"]));
                    if (Convert.ToInt32(row["disponible"]) < 0)
                    {
                        txtcreditodisponible.BackColor = System.Drawing.Color.Red;
                        txtcreditodisponible.ForeColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        txtcreditodisponible.BackColor = System.Drawing.Color.Green;
                        txtcreditodisponible.ForeColor = System.Drawing.Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private void status(int idc_bloqueo)
        {
            switch (idc_bloqueo)
            {
                case 0:
                    txtstatus.BackColor = System.Drawing.Color.Green;
                    txtstatus.ForeColor = System.Drawing.Color.White;
                    txtcliente.ForeColor = System.Drawing.Color.White;
                    txtcliente.BackColor = System.Drawing.Color.Green;
                    txtcreditodisponible.BackColor = System.Drawing.Color.Green;
                    txtcreditodisponible.ForeColor = System.Drawing.Color.White;
                    break;

                case 3:
                case 2:
                case 1:
                    txtstatus.BackColor = System.Drawing.Color.Red;
                    txtstatus.ForeColor = System.Drawing.Color.White;
                    txtcliente.ForeColor = System.Drawing.Color.White;
                    txtcliente.BackColor = System.Drawing.Color.Red;
                    txtcreditodisponible.BackColor = System.Drawing.Color.Red;
                    txtcreditodisponible.ForeColor = System.Drawing.Color.White;
                    break;

                case 4:
                    txtstatus.BackColor = System.Drawing.Color.Yellow;
                    txtstatus.ForeColor = System.Drawing.Color.Black;
                    txtcliente.ForeColor = System.Drawing.Color.Black;
                    txtcliente.BackColor = System.Drawing.Color.Yellow;
                    txtcreditodisponible.BackColor = System.Drawing.Color.Yellow;
                    txtcreditodisponible.ForeColor = System.Drawing.Color.Black;
                    //imgpedidos.Attributes("onclick") = "alert('El Cliente Esta Bloqueado por Cheques Devueltos...');return false;";
                    //imgpedidos_lista.Attributes("onclick") = "alert('El Cliente Esta Bloqueado por Cheques Devueltos...');return false;";
                    break;
            }
        }

        protected void lnkReporte_Click(object sender, EventArgs e)
        {
            string pagina = "tareas_informacion_adicional.aspx?idc_tipoi=" + funciones.deTextoa64("6") + "&idc_proceso=" + funciones.deTextoa64(txtid.Text);
            String url = HttpContext.Current.Request.Url.AbsoluteUri;
            String path_actual = url.Substring(url.LastIndexOf("/") + 1);
            url = url.Replace(path_actual, "");
            url = url + pagina + "&title=" + funciones.deTextoa64(txtcliente.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "noti533W3", "window.open('" + url + "');", true);
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            string pagina = "tareas_informaciOn_adicional.aspx?idc_tipoi=" + funciones.deTextoa64("7") + "&idc_proceso=" + funciones.deTextoa64(txtid.Text);
            String url = HttpContext.Current.Request.Url.AbsoluteUri;
            String path_actual = url.Substring(url.LastIndexOf("/") + 1);
            url = url.Replace(path_actual, "");
            url = url + pagina + "&title=" + funciones.deTextoa64(txtcliente.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "noti533W3", "window.open('" + url + "');", true);
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            string pagina = "tareas_informaciOn_adicional.aspx?idc_tipoi=" + funciones.deTextoa64("8") + "&idc_proceso=" + funciones.deTextoa64(txtid.Text);
            String url = HttpContext.Current.Request.Url.AbsoluteUri;
            String path_actual = url.Substring(url.LastIndexOf("/") + 1);
            url = url.Replace(path_actual, "");
            url = url + pagina + "&title=" + funciones.deTextoa64(txtcliente.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "noti533W3", "window.open('" + url + "');", true);

        }

        protected void lnkseleccionar_Click(object sender, EventArgs e)
        {
            string opcion = cboopciones.SelectedValue;
            switch (opcion)
            {
                case "1"://AGREGAR TAREA
                    Response.Redirect("agendar_llamada.aspx?idc_cliente=" + funciones.deTextoa64(Convert.ToInt32(Session["idc_cliente"]).ToString()));
                    break;
                case "12"://AGREGAR TAREA
                    Response.Redirect("tareas_clientes_cap.aspx?idc_cliente=" + funciones.deTextoa64(Convert.ToInt32(Session["idc_cliente"]).ToString()));
                    break;
                case "11"://REVISAR TAREA
                    Response.Redirect("tareas_clientes_rev.aspx?idc_cliente=" + funciones.deTextoa64(Convert.ToInt32(Session["idc_cliente"]).ToString()) +"&idc_agente="+ funciones.deTextoa64(Convert.ToInt32(Session["idc_agente"]).ToString()));
                    break;
                case "6"://negociacion articulos                    
                    Response.Redirect("cotizacion_clientes2_m.aspx?idc_cliente=" + funciones.deTextoa64(Convert.ToInt32(Session["idc_cliente"]).ToString()) + "&IDA=" + funciones.deTextoa64(Convert.ToInt32(Session["idc_agente"]).ToString())+
                        "&r="+funciones.deTextoa64(txtrfc.Text));

                    break;
                case "5"://compromiso cliente
                    Response.Redirect("compromisos_cliente.aspx?idc_cliente=" + funciones.deTextoa64(Convert.ToInt32(Session["idc_cliente"]).ToString()));
                    break;
                case "4"://ALTA INCONVENIENTE
                    Response.Redirect("inconvenientes_cliente.aspx?idc_cliente=" + funciones.deTextoa64(Convert.ToInt32(Session["idc_cliente"]).ToString()));
                    break;
            }
        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("articulos_master_cliente2.aspx?idc_cliente=" + funciones.deTextoa64(Convert.ToInt32(Session["idc_cliente"]).ToString()));
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Response.Redirect("editar_contacto.aspx?tipo=0&idc_cliente=" + funciones.deTextoa64(Convert.ToInt32(Session["idc_cliente"]).ToString())+
                "&n="+funciones.deTextoa64(txtcliente.Text)+"&r="+ funciones.deTextoa64(txtrfc.Text)+"&c="+funciones.deTextoa64(txtcveadicional.Text) );
        }

        protected void gridcontactos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string id_produccion_grid = gridcontactos.DataKeys[index].Values["idc_telcli"].ToString();
            string url = "editar_contacto.aspx?tipo=1" + "&cdi=" + funciones.deTextoa64(id_produccion_grid) +
                   "&n=" + funciones.deTextoa64(txtcliente.Text) + "&r=" + funciones.deTextoa64(txtrfc.Text) + "&c=" + funciones.deTextoa64(txtcveadicional.Text);
            Response.Redirect(url);
        }

        protected void imgubicacion_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pidc_cliente = Convert.ToInt32(txtid.Text); 
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_cliente_ubicacion(entidad);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string latitud = ds.Tables[0].Rows[0]["latitud"].ToString().Trim();
                    string longitud = ds.Tables[0].Rows[0]["longitud"].ToString().Trim();


                    string cliente = ds.Tables[0].Rows[0]["nombre"].ToString().Trim();
                    cliente =cliente +System.Environment.NewLine+ " DIRECCION: "+ ds.Tables[1].Rows[0]["calle"].ToString().Trim() + " " +
                    ds.Tables[1].Rows[0]["numero"].ToString().Trim() + " " +
                    ds.Tables[1].Rows[0]["mpio"].ToString().Trim() + " " +
                    ds.Tables[1].Rows[0]["edo"].ToString().Trim() +
                    ds.Tables[1].Rows[0]["pais"].ToString().Trim() + " " +
                    ds.Tables[1].Rows[0]["cod_postal"].ToString().Trim();
                    string url = "mapa.aspx?longitud=" + funciones.deTextoa64(longitud) + "&latitud=" + funciones.deTextoa64(latitud) + "&detalle=" + funciones.deTextoa64(cliente);
                    Session["back_url"] = "ficha_cliente_m.aspx";
                    Response.Redirect(url);
                }
                else {
                    Alert.ShowAlertInfo("En los Datos del Cliento no existe su Ubicación GPS","Mensaje del Sistema",this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("editar_contacto.aspx?tipo=2&idc_cliente=" + funciones.deTextoa64(Convert.ToInt32(Session["idc_cliente"]).ToString()) +
               "&n=" + funciones.deTextoa64(txtcliente.Text) + "&r=" + funciones.deTextoa64(txtrfc.Text) + "&c=" + funciones.deTextoa64(txtcveadicional.Text));
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                entidad.Pidc_cliente = Convert.ToInt32(txtid.Text);
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_cliente_ubicacion(entidad);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string latitud = ds.Tables[0].Rows[0]["latitud"].ToString().Trim();
                    string longitud = ds.Tables[0].Rows[0]["longitud"].ToString().Trim();


                    string cliente = ds.Tables[0].Rows[0]["nombre"].ToString().Trim();
                    cliente = cliente + System.Environment.NewLine + " DIRECCION: " + ds.Tables[1].Rows[0]["calle"].ToString().Trim() + " " +
                    ds.Tables[1].Rows[0]["numero"].ToString().Trim() + " " +
                    ds.Tables[1].Rows[0]["mpio"].ToString().Trim() + " " +
                    ds.Tables[1].Rows[0]["edo"].ToString().Trim() +
                    ds.Tables[1].Rows[0]["pais"].ToString().Trim() + " " +
                    ds.Tables[1].Rows[0]["cod_postal"].ToString().Trim();
                    string url = "mapa.aspx?longitud=" + funciones.deTextoa64(longitud) + "&latitud=" + funciones.deTextoa64(latitud) + "&detalle=" + funciones.deTextoa64(cliente);
                    Session["back_url"] = "ficha_cliente_m.aspx";
                    Response.Redirect(url);
                }
                else
                {
                    Alert.ShowAlertInfo("En los Datos del Cliento no existe su Ubicación GPS", "Mensaje del Sistema", this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("precios_cotizados.aspx?idc_cliente=" + funciones.deTextoa64(Convert.ToInt32(Session["idc_cliente"]).ToString()));
        }
    }
}