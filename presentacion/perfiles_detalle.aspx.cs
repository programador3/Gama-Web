using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class perfiles_detalle : System.Web.UI.Page
    {
        public static int RequestId = 0;
        public static int RequestIdBorr = 0;
        public string ValueLabel = "";
        public Boolean comparacion = false;
        public static DataTable TablaPanelesOcultos = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["borrador"] == null)//comprobamos que no venga de pantalla perfiles
                {
                    if (Request.QueryString["uidc_puestoperfil_borr"] == null)//Si variable request es nulla
                    {
                        RequestIdBorr = 0;
                    }
                    else
                    {
                        RequestIdBorr = Convert.ToInt32(Request.QueryString["uidc_puestoperfil_borr"].ToString());
                    }
                    if (Request.QueryString["uidc_puestoperfil"] == null)//Si variable request es nulla
                    {
                        RequestId = 0;
                    }
                    else
                    {
                        RequestId = Convert.ToInt32(Request.QueryString["uidc_puestoperfil"].ToString());
                    }
                    ImageButton1.Visible = false;
                    ImageButton2.Visible = false; ddlTipo.Visible = false;
                    ddlTipo.Visible = false;
                    ImageButton3.Visible = false;
                    ImageButton4.Visible = false;
                    ImageButton5.Visible = false;
                    PanelBorrador.Visible = true;
                    PanelProduccion.Visible = true;
                    lblComparacion.Visible = true;
                    Panelbtns.Visible = true;
                    if (RequestId == 0)
                    {
                        CargarDatosPerfilBorrador_Nuevo(RequestIdBorr);//Borrador Nuevo
                    }
                    else
                    {
                        CompararPerfiles(RequestId, RequestIdBorr);//Comparacion
                        ocultar_Paneles(rpCompara, "btnOcultar_BodyPanel", "Produccion_Panel_Individual");
                        ocultar_Paneles(rpBorrador, "btnOcultar_BodyPanel_Borrador", "Borrador_Panel_Indiv");
                    }
                }
                else
                {//viene de pantalla perfiles para visualizar un perfil
                    int Tipo_Vista = Convert.ToInt32(Request.QueryString["borrador"].ToString());//tomamos el valor booleano de borrador
                    if (Request.QueryString["uidc_puestoperfil"] == null)//Si variable request es nulla
                    {
                        int RequestIDV = Convert.ToInt32(Request.QueryString["uidc_puestoperfil_borr"].ToString());//Tipo Borrador
                        CargarDatosPerfilBorrador(RequestIDV);
                        lbltipo.Text = "BORRADOR";
                        lbltipo.CssClass = "label label-primary";
                        lbltipo.Visible = true;
                    }
                    else
                    {
                        int RequestIDV = Convert.ToInt32(Request.QueryString["uidc_puestoperfil"].ToString());//Tipo Produccion
                        CargarDatosPerfil(RequestIDV);
                        lbltipo.Text = "PRODUCCION";
                        lbltipo.CssClass = "label label-success";
                        lbltipo.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Funcion que toma nombre de pagina anterior con variable de servidor y la manda a una
        /// funcion javascript
        /// </summary>
        private void ReturnPrevius(string Mensaje)
        {
            string PreviousPage = Request.ServerVariables["HTTP_REFERER"];
            if (PreviousPage == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('" + Mensaje + "','perfiles.aspx');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('" + Mensaje + "','" + PreviousPage + "');", true);
            }
        }

        /// <summary>
        /// Funcion que carga los datos de produccion con un request
        /// </summary>
        /// <param name="ID">ID DEL PUESTO</param>
        public void CargarDatosPerfil(int ID)
        {
            try
            {
                PerfilesE Entidad = new PerfilesE();
                //tomo el valor del ID
                Entidad.Idc_perfil = ID;
                //Creo mi componente
                PerfilesBL Componente = new PerfilesBL();
                //Cargo mi componenet e en un dataset
                DataSet ds = new DataSet();
                ds = Componente.CargaPuestosPerfil(Entidad);
                CargaPerfil_CM Componente2 = new CargaPerfil_CM();
                Session["etiquetas_archivos"] = Componente2.CargaArchivosHTMLPRODUCCION(Entidad).Tables[0];
                //comprobamos que el dataset no este vacio, si esta vacio es que no hay ningun perfil relacionado
                if (ds.Tables[0].Rows.Count == 0)
                {
                    ReturnPrevius("Solo tiene capturado el nombre del perfil");
                    ImageButton1.Visible = false;
                    ImageButton2.Visible = false; ddlTipo.Visible = false; ddlTipo.Visible = false;
                    ImageButton3.Visible = false;
                    ImageButton4.Visible = false;
                    ImageButton5.Visible = false;
                }
                else
                {
                    //Creo una tabla para filtrar los datos
                    DataTable tablaGrupos = new DataTable();
                    //Paso dataset ala tabla
                    tablaGrupos = ds.Tables[0];
                    Session.Add("TablaFiltros", tablaGrupos);
                    DataRow row = tablaGrupos.Rows[0];
                    txttitulo.Text = row["Puesto"].ToString();
                    //filtramos para evitar datos repetidos
                    DataTable TablaTemporal = tablaGrupos.DefaultView.ToTable(true, "Grupo");
                    DataView view = TablaTemporal.DefaultView;
                    if (Request.QueryString["vp"] != null)
                    {
                        view.RowFilter = "Grupo like '%Descripción de Puesto%'";
                        RepeatDataPuesto.DataSource = view.ToTable();
                        RepeatDataPuesto.DataBind();
                    }
                    else
                    {
                        RepeatDataPuesto.DataSource = TablaTemporal;
                        RepeatDataPuesto.DataBind();
                    }
                    //agregamos tabla global a session
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
            }
        }

        /// <summary>
        /// Funcion que carga los datos de borrador con un request
        /// </summary>
        /// <param name="ID">ID DEL PUESTO</param>
        public void CargarDatosPerfilBorrador(int ID)
        {
            try
            {
                PerfilesE Entidad = new PerfilesE();
                //tomo el valor del ID
                Entidad.Idc_puestoperfil_borr = ID;
                //Creo mi componente
                CargaPerfil_CM Componente = new CargaPerfil_CM();
                //Cargo mi componenet e en un dataset
                DataSet ds = new DataSet();
                ds = Componente.CargaPuestosPerfilBorrador(Entidad);
                //Creo una tabla para filtrar los datos
                DataTable tablaGrupos = new DataTable();
                //Paso dataset ala tabla
                tablaGrupos = ds.Tables[1];

                Session["etiquetas_archivos"] = Componente.CargaArchivosHTML(Entidad).Tables[0];
                //comprobamos que el dataset no este vacio, si esta vacio es que no hay ningun perfil relacionado
                if (tablaGrupos.Rows.Count == 0)
                {
                    ReturnPrevius("Solo tiene capturado el nombre del perfil");
                    ImageButton1.Visible = false;
                    ImageButton2.Visible = false; ddlTipo.Visible = false;
                    ImageButton3.Visible = false;
                    ImageButton4.Visible = false;
                    ImageButton5.Visible = false;
                }
                else
                {
                    Session.Add("TablaFiltros", tablaGrupos);
                    DataRow row = tablaGrupos.Rows[0];
                    txttitulo.Text = row["Puesto"].ToString();
                    //filtramos para evitar datos repetidos
                    DataTable TablaTemporal = tablaGrupos.DefaultView.ToTable(true, "Grupo");
                    RepeatDataPuesto.DataSource = TablaTemporal;
                    RepeatDataPuesto.DataBind();
                    //agregamos tabla global a session
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
            }
        }

        /// <summary>
        /// Funcion que carga los datos de borrador NUEVO con un request
        /// </summary>
        /// <param name="ID">ID DEL PUESTO</param>
        public void CargarDatosPerfilBorrador_Nuevo(int ID)
        {
            try
            {
                PerfilesE Entidad = new PerfilesE();
                //tomo el valor del ID
                Entidad.Idc_puestoperfil_borr = ID;
                //Creo mi componente
                CargaPerfil_CM Componente = new CargaPerfil_CM();
                //Cargo mi componenet e en un dataset
                DataSet ds = new DataSet();
                ds = Componente.CargaPuestosPerfilBorrador(Entidad);
                //Creo una tabla para filtrar los datos
                DataTable tablaGrupos = new DataTable();
                //Paso dataset ala tabla
                tablaGrupos = ds.Tables[1];
                Session["etiquetas_archivos"] = Componente.CargaArchivosHTML(Entidad).Tables[0];
                //comprobamos que el dataset no este vacio, si esta vacio es que no hay ningun perfil relacionado
                if (tablaGrupos.Rows.Count == 0)
                {
                    ReturnPrevius("Solo tiene capturado el nombre del perfil");
                    ImageButton1.Visible = false;
                    ImageButton2.Visible = false; ddlTipo.Visible = false;
                    ImageButton3.Visible = false;
                    ImageButton4.Visible = false;
                    ImageButton5.Visible = false;
                }
                else
                {
                    Session.Add("TablaFiltros_borr", tablaGrupos);
                    DataRow row = tablaGrupos.Rows[0];
                    txttitulo.Text = row["Puesto"].ToString();
                    //filtramos para evitar datos repetidos
                    DataTable TablaTemporal = tablaGrupos.DefaultView.ToTable(true, "Grupo");
                    rpBorrador_sinProduccion.DataSource = TablaTemporal;
                    rpBorrador_sinProduccion.DataBind();
                    //agregamos tabla global a session
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
            }
        }

        /// <summary>
        /// Funcion que carga los datos para comparar con un request
        /// </summary>
        /// <param name="ID">ID DEL PUESTO</param>
        public void CompararPerfiles(int ID, int ID_BORR)
        {
            try
            {
                PerfilesE Entidad = new PerfilesE();
                //tomo el valor del ID
                Entidad.Idc_perfil = ID;
                Entidad.Idc_puestoperfil_borr = ID_BORR;
                //Creo mi componente
                CargaPerfil_CM Componente = new CargaPerfil_CM();
                //Cargo mi componenet e en un dataset
                DataSet ds = new DataSet();
                ds = Componente.ComparaPerfiles(Entidad);
                //comprobamos que el dataset no este vacio, si esta vacio es que no hay ningun perfil relacionado
                //Creo una tabla para filtrar los datos
                DataTable tablaproduccion = new DataTable();
                DataTable tablaborrador = new DataTable();
                //Paso dataset ala tabla
                DataTable TablaDiferencias = new DataTable();
                tablaproduccion = ds.Tables[0];
                tablaborrador = ds.Tables[1];
                TablaDiferencias = ds.Tables[2];
                Session["etiquetas_archivos"] = Componente.CargaArchivosHTML(Entidad).Tables[0];
                Session["etiquetas_archivos_produccion"] = Componente.CargaArchivosHTMLPRODUCCION(Entidad).Tables[0];
                Session.Add("TablaDiferencias", TablaDiferencias);
                //verificamos que tablas no esten vacias
                if (tablaborrador.Rows.Count == 0)
                {
                    ReturnPrevius("Solo tiene capturado el nombre del perfil en el perfil de Borrador");
                }
                Session.Add("TablaFiltros", tablaproduccion);
                Session.Add("TablaFiltros_borr", tablaborrador);
                DataRow row = tablaproduccion.Rows[0];
                txttitulo.Text = row["Puesto"].ToString();
                //filtramos para evitar datos repetidos
                DataTable tabla_temp = tablaproduccion.DefaultView.ToTable(true, "Grupo");
                //filtramos datos visibles
                DataTable TablaTemporal = tablaproduccion.DefaultView.ToTable(true, "Grupo");
                DataTable TablaTemporal_B = tablaborrador.DefaultView.ToTable(true, "Grupo");
                //CARGAMOS DATOS A LOS REPEATS
                rpCompara.DataSource = TablaTemporal;
                rpCompara.DataBind();
                rpBorrador.DataSource = TablaTemporal_B;
                rpBorrador.DataBind();
                //agregamos tabla global a session
            }
            catch (Exception ex)
            {
                //Alert.ShowAlertError(ex.Message, this);
            }
        }

        //===================================================================================================================
        //=====================EVENTOS DE CARGAS DE REPEATS==================================================================
        //===================================================================================================================
        //===================================================================================================================
        /// <summary>
        /// EVENTO HEREDADO DEL AGREGADO DE UN ITEM, SE ACTIVA CADA VEZ QUE UN REPEAT AGREGA UN ITEM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeatDataPuesto_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //tomamos el valor del item agregado para filtrar la tabla
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            DataTable tbl = (System.Data.DataTable)(Session["TablaFiltros"]);
            string Grupo = Convert.ToString(DataBinder.Eval(dbr, "Grupo"));
            //filtramos la tabla para mostrar solo las filas crfon el grupo del item que se agrego

            DataView dv = new DataView(tbl);
            dv.RowFilter = "Grupo = '" + Grupo + "'";
            Repeater RepeaterVariables = (Repeater)e.Item.FindControl("RepeaterChild");
            Session.Add("TablaLabel", dv.ToTable());
            RepeaterVariables.DataSource = dv;
            RepeaterVariables.DataBind();
        }

        /// <summary>
        /// EVENTO HEREDADO DEL AGREGADO DE UN ITEM, SE ACTIVA CADA VEZ QUE UN REPEAT AGREGA UN ITEM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeatProduccion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //tomamos el valor del item agregado para filtrar la tabla
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            DataTable tbl = (System.Data.DataTable)(Session["TablaFiltros"]);
            DataTable TablaDiferencias = (System.Data.DataTable)(Session["TablaDiferencias"]);
            Panel Panel = (Panel)e.Item.FindControl("Produccion_Panel_Individual");
            LinkButton btn = (LinkButton)e.Item.FindControl("btnOcultar_BodyPanel");
            string Grupo = Convert.ToString(DataBinder.Eval(dbr, "Grupo"));
            btn.CommandArgument = e.Item.ItemIndex.ToString();
            string Nombre = dbr["Grupo"].ToString();
            //filtramos la tabla para mostrar solo las filas con el grupo del item que se agrego
            DataView dv = new DataView(tbl);
            dv.RowFilter = "Grupo = '" + Grupo + "'";
            //creamos un repeat y lbuscamos un control en html con ese mismo nombre
            Repeater RepeaterVariables = (Repeater)e.Item.FindControl("RepeaterChild");
            Session.Add("TablaLabel", dv.ToTable());
            RepeaterVariables.DataSource = dv;
            RepeaterVariables.DataBind();
        }

        /// <summary>
        /// EVENTO HEREDADO DEL AGREGADO DE UN ITEM, SE ACTIVA CADA VEZ QUE UN REPEAT AGREGA UN ITEM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpBorrador_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //tomamos el valor del item agregado para filtrar la tabla
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            DataTable tbl = (System.Data.DataTable)(Session["TablaFiltros_borr"]);
            DataTable tbl_produccion = (System.Data.DataTable)(Session["TablaFiltros"]);
            string Grupo = Convert.ToString(DataBinder.Eval(dbr, "Grupo"));
            string Nombre = dbr["Grupo"].ToString();
            //tomamos id para linkbutton
            DataView dv_b = new DataView(tbl);
            dv_b.RowFilter = "Grupo = '" + Grupo + "'";
            string id_Grupo = dv_b[0].Row["idc_perfilgpo"].ToString();
            Panel Panel = (Panel)e.Item.FindControl("panel_comprobar_diferencia");
            Panel PanelPrincipal = (Panel)e.Item.FindControl("Borrador_Panel_Individual");
            LinkButton LinkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
            LinkButton LinkVer = (LinkButton)e.Item.FindControl("btnOcultar_BodyPanel_Borrador");
            LinkVer.CommandArgument = e.Item.ItemIndex.ToString();
            Label Tipo = (Label)e.Item.FindControl("lblTipoDiferencia");
            DataTable TablaDiferencias = (System.Data.DataTable)(Session["TablaDiferencias"]);
            LinkEdit.CommandName = id_Grupo;
            int id_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            bool visible_edit = funciones.autorizacion(id_usuario, 329);////AQUI cambia
            if (TablaDiferencias.Rows.Count == 0)
            {
                Panel.CssClass = "panel-heading p-success";
                LinkEdit.CssClass = "btn btn-success";
            }
            else
            {
                foreach (DataRow row in TablaDiferencias.Rows)
                {
                    if (row["Grupo"].ToString().Equals(Nombre))
                    {
                        if (row["Tipo"].ToString().Equals("nuevo"))
                        {
                            Panel.CssClass = "panel-heading p-danger";
                            Tipo.Text = "Nuevo Grupo";
                            LinkEdit.CssClass = "btn btn-danger";
                            LinkVer.CssClass = "btn btn-danger btn-xs";
                        }
                        else
                        {
                            Panel.CssClass = "panel-heading p-info";
                            Tipo.Text = "Grupo Editado";
                            LinkEdit.CssClass = "btn btn-info";
                            LinkVer.CssClass = "btn btn-primary btn-xs";
                        }
                        break;
                    }
                    else
                    {
                        Panel.CssClass = "panel-heading p-success";
                        LinkEdit.CssClass = "btn btn-success";
                    }
                }
            }
            //filtramos la tabla para mostrar solo las filas con el grupo del item que se agrego
            DataView dv = new DataView(tbl);
            dv.RowFilter = "Grupo = '" + Grupo + "'";
            Repeater RepeaterVariables = (Repeater)e.Item.FindControl("RepeaterChild_B");
            Session.Add("TablaLabel_b", dv.ToTable());
            LinkEdit.Visible = visible_edit;
            RepeaterVariables.DataSource = dv;
            RepeaterVariables.DataBind();
        }

        /// <summary>
        /// EVENTO HEREDADO DEL AGREGADO DE UN ITEM, SE ACTIVA CADA VEZ QUE UN REPEAT AGREGA UN ITEM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpBorradorNuevo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //tomamos el valor del item agregado para filtrar la tabla
            DataRowView dbr = (DataRowView)e.Item.DataItem;

            //COMIENZA CICLO, RECORREINDO EL TOTAL DE LAS FILAS - 1
            DataTable tbl = (System.Data.DataTable)(Session["TablaFiltros_borr"]); //filtramos la tabla para mostrar solo las filas con el grupo del item que se agrego
            string Grupo = Convert.ToString(DataBinder.Eval(dbr, "Grupo"));
            string Nombre = dbr["Grupo"].ToString();
            //tomamos id para linkbutton
            DataView dv_b = new DataView(tbl);
            dv_b.RowFilter = "Grupo = '" + Grupo + "'";
            //Construyo un Label
            //Si la etiqueta esta vacia
            string id_Grupo = dv_b[0].Row["idc_perfilgpo"].ToString();
            Panel Panel = (Panel)e.Item.FindControl("panel_comprobar_diferencia");
            LinkButton LinkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
            LinkEdit.CommandName = id_Grupo;
            Panel.CssClass = "panel-heading p-danger";
            LinkEdit.CssClass = "btn btn-danger";
            //creamos un repeat y lbuscamos un control en html con ese mismo nombre
            Repeater RepeaterVariables = (Repeater)e.Item.FindControl("RepeaterChild_B_Nuevo");
            Session.Add("TablaLabel", dv_b.ToTable());
            RepeaterVariables.DataSource = dv_b;
            RepeaterVariables.DataBind();
        }

        //===================================================================================================================
        //=====================EVENTOS DE CARGAS DE REPEATS HIJOS============================================================
        //===================================================================================================================
        //===================================================================================================================

        /// <summary>
        /// Eevento que se genera al crear un label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeatChild_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Bajo el valor del label actual
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            String EtiquetaActual = dbr["Etiqueta"].ToString();
            String ValorActual = dbr["Valor"].ToString();
            String grupo = dbr["Grupo"].ToString();
            //Construyo un Label
            Button btndescarga = (Button)e.Item.FindControl("btnIRSw");
            Button BtnVer = (Button)e.Item.FindControl("BtnVers");
            Button btneditar = (Button)e.Item.FindControl("btneditar");
            //Construyo un Label
            Label lblEtiqueta = (Label)e.Item.FindControl("lblEtiqueta");
            Label lblValor = (Label)e.Item.FindControl("Label1");
            //Si la etiqueta esta vacia
            DataTable etiquetas_archivos = (DataTable)Session["etiquetas_archivos"];
            foreach (DataRow row_archi in etiquetas_archivos.Rows)
            {
                string ruta = row_archi["ruta"].ToString();
                string texto = row_archi["texto"].ToString();
                if (texto == ValorActual)
                {
                    int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                    int idc_tipo_aut = 317;
                    int idc_tipo_aut2 = 343;
                    BtnVer.Visible = true;
                    BtnVer.CommandName = ruta;
                    BtnVer.CommandArgument = "html";//es html
                    BtnVer.Text = "Ver Documentos";
                    if (Request.QueryString["perfiles"] != null)
                    {
                        btneditar.Visible = true;
                        btneditar.CommandName = ruta;
                        if (Request.QueryString["borrador"] == "1")
                        {
                            btneditar.Visible = funciones.autorizacion(idc_usuario, idc_tipo_aut2) == true ? true : false;
                        }
                        if (Request.QueryString["borrador"] == "0")
                        {
                            btneditar.Visible = funciones.autorizacion(idc_usuario, idc_tipo_aut) == true ? true : false;
                        }
                    }
                }
            }
            if (EtiquetaActual.Equals(""))
            {
                lblValor.Text = " <i class='fa fa-fw fa-check'></i>" + ValorActual;
            }
            else
            {
                if (EtiquetaActual.Equals(ValueLabel))//SI LA ETIQUETA ES IGUAL A LA ANTERIOE
                {
                    //LE PONGO ESPACIOS EN BLANCO PARA AJUSTAR
                    lblEtiqueta.Text = "&nbsp;";
                }
                else
                {
                    //Si no, subo el valor al string global
                    ValueLabel = dbr["Etiqueta"].ToString();
                    lblEtiqueta.Text = " <i class='fa fa-fw fa-check'></i>" + EtiquetaActual;
                    //Lo pongo en negrita
                    lblEtiqueta.Font.Bold = true;
                }
            }
            if (grupo == "Anexos" || grupo == "Examenes" || grupo == "Documentos")
            {
                lblValor.Visible = false;
                btndescarga.Visible = true;
                btndescarga.CommandName = ValorActual;
                if (ValorActual == "NO HAY DOCUMENTO ASIGANDO")
                {
                    lblValor.Visible = true;
                    btndescarga.Visible = false;
                }
                if (grupo == "Documentos")
                {
                    btndescarga.Visible = false;
                    lblValor.Visible = true;
                }
            }
        }

        protected void RepeatChildProduccion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Bajo el valor del label actual
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            String EtiquetaActual = dbr["Etiqueta"].ToString();
            String ValorActual = dbr["Valor"].ToString();
            String grupo = dbr["Grupo"].ToString();
            //Construyo un Label
            Button btndescarga = (Button)e.Item.FindControl("btnIRSe");
            Button BtnVer = (Button)e.Item.FindControl("BtnVere");
            //Construyo un Label
            Label lblEtiqueta = (Label)e.Item.FindControl("lblEtiqueta");
            Label lblValor = (Label)e.Item.FindControl("Label1");
            //Si la etiqueta esta vacia
            DataTable etiquetas_archivos = (DataTable)Session["etiquetas_archivos_produccion"];
            foreach (DataRow row_archi in etiquetas_archivos.Rows)
            {
                string ruta = row_archi["ruta"].ToString();
                string texto = row_archi["texto"].ToString();
                if (texto == ValorActual)
                {
                    BtnVer.Visible = true;
                    BtnVer.CommandName = ruta;
                    BtnVer.CommandArgument = "html";//es html
                    BtnVer.Text = "Ver Documento";
                }
            }
            if (EtiquetaActual.Equals(""))
            {
                lblValor.Text = " <i class='fa fa-fw fa-check'></i>" + ValorActual;
            }
            else
            {
                if (EtiquetaActual.Equals(ValueLabel))//SI LA ETIQUETA ES IGUAL A LA ANTERIOE
                {
                    //LE PONGO ESPACIOS EN BLANCO PARA AJUSTAR
                    lblEtiqueta.Text = "&nbsp;";
                }
                else
                {
                    //Si no, subo el valor al string global
                    ValueLabel = dbr["Etiqueta"].ToString();
                    lblEtiqueta.Text = " <i class='fa fa-fw fa-check'></i>" + EtiquetaActual;
                    //Lo pongo en negrita
                    lblEtiqueta.Font.Bold = true;
                }
            }
            if (grupo == "Anexos" || grupo == "Examenes" || grupo == "Documentos")
            {
                lblValor.Visible = false;
                btndescarga.Visible = true;
                btndescarga.CommandName = ValorActual;
                if (ValorActual == "NO HAY DOCUMENTO ASIGANDO")
                {
                    lblValor.Visible = true;
                    btndescarga.Visible = false;
                }
                if (grupo == "Documentos")
                {
                    btndescarga.Visible = false;
                    lblValor.Visible = true;
                }
            }
        }

        /// <summary>
        /// Eevento que se genera al crear un label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeatChildB_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Bajo el valor del label actual
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            String EtiquetaActual = dbr["Etiqueta"].ToString();
            String ValorActual = dbr["Valor"].ToString();
            String grupo = dbr["Grupo"].ToString();
            //Construyo un Label
            Label lblEtiqueta = (Label)e.Item.FindControl("lblEtiquetaB");
            Label lblValor = (Label)e.Item.FindControl("Label1B");
            Button btndescarga = (Button)e.Item.FindControl("btnIRd");
            Button BtnVer = (Button)e.Item.FindControl("BtnVerWd");
            DataTable TablaDiferencias = (System.Data.DataTable)(Session["TablaDiferencias"]);
            HyperLink hplnkGO = (HyperLink)e.Item.FindControl("hplnkGO");
            DataTable etiquetas_archivos = (DataTable)Session["etiquetas_archivos"];
            foreach (DataRow row_archi in etiquetas_archivos.Rows)
            {
                string ruta = row_archi["ruta"].ToString();
                string texto = row_archi["texto"].ToString();
                if (texto == ValorActual)
                {
                    BtnVer.Visible = true;
                    BtnVer.CommandName = ruta;
                    BtnVer.CommandArgument = "html";//es html
                    BtnVer.Text = "Ver Documento";
                }
            }
            string ItemEdicion = "";
            if (grupo == "Anexos" || grupo == "Examenes" || grupo == "Documentos")
            {
                lblValor.Visible = false;
                btndescarga.Visible = true;
                btndescarga.CommandName = ValorActual;
                if (ValorActual == "NO HAY DOCUMENTO ASIGANDO")
                {
                    lblValor.Visible = true;
                    btndescarga.Visible = false;
                }
                if (grupo == "Documentos")
                {
                    btndescarga.Visible = false;
                    lblValor.Visible = true;
                }
            }
            //buscamos etqieuta que hayan tenido ediciones
            foreach (DataRow row in TablaDiferencias.Rows)
            {
                if (row["Etiqueta"].ToString().Equals(EtiquetaActual))
                {
                    if (row["Valor"].ToString().Equals(ValorActual))
                    {
                        if (row["tipo"].ToString().Equals("cambio"))
                        {
                            ItemEdicion = "   <span class='badge'>Valor Editado</span>";
                        }

                        break;
                    }
                }
            }
            //Si la etiqueta esta vacia
            if (EtiquetaActual.Equals(""))
            {
                lblValor.Text = " <i class='fa fa-fw fa-check'></i>" + ValorActual + ItemEdicion;
            }
            else
            {
                if (EtiquetaActual.Equals(ValueLabel))//SI LA ETIQUETA ES IGUAL A LA ANTERIOE
                {
                    //LE PONGO ESPACIOS EN BLANCO PARA AJUSTAR
                    lblEtiqueta.Text = "&nbsp;";
                    lblValor.Text = ValorActual + ItemEdicion;
                }
                else
                {
                    //Si no, subo el valor al string global
                    ValueLabel = dbr["Etiqueta"].ToString();
                    lblEtiqueta.Text = " <i class='fa fa-fw fa-check'></i>" + EtiquetaActual;
                    lblValor.Text = ValorActual + ItemEdicion;
                    //Lo pongo en negrita
                    lblEtiqueta.Font.Bold = true;
                }
            }
        }

        /// <summary>
        /// Eevento que se genera al crear un label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeatChildB_Nuevo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Bajo el valor del label actual
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            String EtiquetaActual = dbr["Etiqueta"].ToString();
            String ValorActual = dbr["Valor"].ToString();
            String grupo = dbr["Grupo"].ToString();
            //Construyo un Label
            Button btndescarga = (Button)e.Item.FindControl("btnIRS");
            Button BtnVer = (Button)e.Item.FindControl("BtnVerC");
            DataTable etiquetas_archivos = (DataTable)Session["etiquetas_archivos"];
            foreach (DataRow row_archi in etiquetas_archivos.Rows)
            {
                string ruta = row_archi["ruta"].ToString();
                string texto = row_archi["texto"].ToString();
                if (texto == ValorActual)
                {
                    BtnVer.Visible = true;
                    BtnVer.CommandName = ruta;
                    BtnVer.CommandArgument = "html";//es html
                    BtnVer.Text = "Ver Documento";
                }
            }
            //Construyo un Label
            Label lblEtiqueta = (Label)e.Item.FindControl("Label2");
            Label lblValor = (Label)e.Item.FindControl("Label3");
            if (EtiquetaActual.Equals(""))
            {
                lblValor.Text = " <i class='fa fa-fw fa-check'></i>" + ValorActual;
            }
            else
            {
                if (EtiquetaActual.Equals(ValueLabel))//SI LA ETIQUETA ES IGUAL A LA ANTERIOE
                {
                    //LE PONGO ESPACIOS EN BLANCO PARA AJUSTAR
                    lblEtiqueta.Text = "&nbsp;";
                }
                else
                {
                    //Si no, subo el valor al string global
                    ValueLabel = dbr["Etiqueta"].ToString();
                    lblEtiqueta.Text = " <i class='fa fa-fw fa-check'></i>" + EtiquetaActual;
                    //Lo pongo en negrita
                    lblEtiqueta.Font.Bold = true;
                }
            }
            if (grupo == "Anexos" || grupo == "Examenes" || grupo == "Documentos")
            {
                lblValor.Visible = false;
                btndescarga.Visible = true;
                btndescarga.CommandName = ValorActual;
                if (ValorActual == "NO HAY DOCUMENTO ASIGANDO")
                {
                    lblValor.Visible = true;
                    btndescarga.Visible = false;
                }
                if (grupo == "Documentos")
                {
                    btndescarga.Visible = false;
                    lblValor.Visible = true;
                }
            }
        }

        /// <summary>
        /// Evento clic del boton guardar WORD, en base a ciclos escribe en HTML la estructura del word y lo exporta a este
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //Bajo tabla de la consulta a session
            DataTable tbl = (System.Data.DataTable)(Session["TablaFiltros"]);
            //Creo tabla con filtro DISTINCT pata que repita los grupos
            DataView dvr = new DataView(tbl);
            switch (ddlTipo.SelectedValue)
            {
                case "todo":
                    dvr.RowFilter = "externo = 1 or externo = 0";
                    break;

                case "occ":

                    dvr.RowFilter = "externo = 1";
                    break;

                case "excp":

                    dvr.RowFilter = "externo = 0";
                    break;
            }

            //Creo tabla con filtro DISTINCT pata que repita los grupos
            DataTable TablaTemporal = dvr.ToTable(true, "Grupo");
            //Saco vista de filas de la tabla de la consulta
            DataRow row = tbl.Rows[0];
            //saco el vaor del puesto o perfil y lo paso a variable
            string Titulo = row["Puesto"].ToString();
            //CREO STRINGBUILDER PARA LLENARLO CON CODIGO HTML
            StringBuilder content = new StringBuilder();
            //EMPIEZO CODIGO HTML
            content.Append("<meta http-equiv='Content-Type' content='text/html' charset='UTF-8'/>");
            content.Append(System.Environment.NewLine);
            content.Append("<html>");
            content.Append("<body>");
            //COLOCO ENCABEZADO = NOMBRE DEL PERFIL, Y CONCATENO VARIABLE QUE CONTIENE EL NOMBRE
            content.Append("<div style='text-align: center;'><FONT FACE='calibri' SIZE=5> <b>" + " " + Titulo + "</b></FONT></div><br>");
            //CICLO QIE RECORRE TODO LOS GRUPOS EXISTENTES PARA ESTE PERFIL
            for (int i = 0; i < TablaTemporal.Rows.Count; i++)
            {
                //REALIZO FILTRO DETODO LOS GRUPOS EXISTENTES PARA ESTE PERFIL
                DataRow rowj = TablaTemporal.Rows[i];
                //TOMO VALOR DEL NOMBRE DEL GRUPO Y LO METO A VARIABLE
                string Grupo = rowj["Grupo"].ToString();
                //LLENO STRINGBUILDER CON CODIGO HTML Y CONCATENADO EL NOMBRE DEL GRUPO
                content.Append("<div><b><FONT FACE='calibri'>" + Grupo + ":</FONT></b></div>");
                //REALIZO FILTRO EN TABLA DELA CONSULTA PRINCIPAL PARA VER QUE VALORES PERTENECEN A ESTE GRUPO
                DataView dv = new DataView(tbl);
                dv.RowFilter = "Grupo = '" + Grupo + "'";
                //CREO UNA TABLA PARA LLENAR LOS VALORES DEL GRUPO
                content.Append("<table>");
                //METO EL FILTRO A UNA TABLA PARA REUSARLA
                DataTable filter = dv.ToTable();
                //CREO CICLO PARA RECORRER TODOS LOS VALORES DE ESTE GRUPO
                for (int j = 0; j < dv.Count; j++)
                {
                    //REALIZO FILTRO DE TODOS LOS VALORES EXISTENTE EN ESTE GRUPO
                    DataRow rowk = filter.Rows[j];
                    //METO EL VALOR A UNA VARIABLE
                    string EtiquetaV = rowk["Etiqueta"].ToString();
                    string Valor = rowk["Valor"].ToString();
                    if (EtiquetaV.Equals(ValueLabel))
                    {
                        //METO A STRINGBUILDER CODIGO HTML CON EL VALOR OBTENIDO
                        content.Append("<tr><td></td><td></td><td><FONT FACE='calibri'>" + Valor + "</FONT></td></tr>");
                    }
                    else
                    {
                        ValueLabel = EtiquetaV;
                        //METO A STRINGBUILDER CODIGO HTML CON EL VALOR OBTENIDO
                        content.Append("<tr><td></td><td></td><td><FONT FACE='calibri'><strong>" + EtiquetaV + "</strong>" + Valor + "</FONT></td></tr>");
                    }
                }//TERMINA CICLO CON TODAS LA VARIABLES DEL GRUPO
                //CIERRO TABLA
                content.Append("</table></FONT><br><br>");
            }//TERMINA CICLO CON TODOS LOS GRUPOS DE ESTE PERFIL
            //CIERRO CODIGO HTML EN EL STRINGBUILDER
            content.Append("</body></html>");
            //TOMO VARIABLE DEL NOMBRE DE PERFIL Y REMPLAZO ESPACIOS POR GUINES BAJO PARA METERLO COMO NOMBRE DE ARCHIVO
            Titulo = Titulo.Replace(" ", "_");
            //CREO STRING CON EL NOMBRE DEL ARCHIVO QUE SE GENERARA
            string fileName = "" + Titulo + "" + ".doc";
            //ASIGNO CONTEXTOS
            Response.AppendHeader("Content-Type", "application/msword");
            Response.AppendHeader("Content-disposition", "attachment; filename=" + fileName);
            //CREO ARCHIVO EN UN RESPONSE PARA QUE PAGINA ME LO ARROJE
            Response.Write(content.ToString());
        }

        /// <summary>
        /// Evento clic del boton ver codigo html, en base a ciclos, crea codigo html para mostrarlo  al usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            RepeatDataPuesto.Visible = false;
            PanelHTML.Visible = true;
            VistaPrevia.Visible = false;
            ImageButton3.Visible = true;
            //Bajo tabla de la consulta a session
            DataTable tbl = (System.Data.DataTable)(Session["TablaFiltros"]);
            //Creo tabla con filtro DISTINCT pata que repita los grupos
            DataView dvr = new DataView(tbl);

            switch (ddlTipo.SelectedValue)
            {
                case "todo":
                    dvr.RowFilter = "externo = 1 or externo = 0";
                    break;

                case "occ":

                    dvr.RowFilter = "externo = 1";
                    break;

                case "excp":

                    dvr.RowFilter = "externo = 0";
                    break;
            }
            //Creo tabla con filtro DISTINCT pata que repita los grupos
            DataTable TablaTemporal = dvr.ToTable(true, "Grupo");
            //Saco vista de filas de la tabla de la consulta
            DataRow row = tbl.Rows[0];
            //saco el vaor del puesto o perfil y lo paso a variable
            string Titulo = row["Puesto"].ToString();
            //CREO STRINGBUILDER PARA LLENARLO CON CODIGO HTML
            StringBuilder content = new StringBuilder();
            //EMPIEZO CODIGO HTML
            content.Append("&lt;meta http-equiv='Content-Type' content='text/html' charset='UTF-8'/&gt;");
            content.Append(System.Environment.NewLine);
            content.Append("&lt;html&gt;");
            content.Append(System.Environment.NewLine);
            content.Append("&lt;body&gt;");
            content.Append(System.Environment.NewLine);
            //COLOCO ENCABEZADO = NOMBRE DEL PERFIL, Y CONCATENO VARIABLE QUE CONTIENE EL NOMBRE
            content.Append(System.Environment.NewLine + "&lt;div style='text-align: center;'&gt;&lt;FONT FACE='calibri' SIZE=5&gt; &lt;b&gt;" + " " + Titulo + "&lt;/b&gt;&lt;/FONT&gt;" + System.Environment.NewLine + "&lt;/div&gt;&lt;br&gt;" + System.Environment.NewLine);
            //CICLO QIE RECORRE TODO LOS GRUPOS EXISTENTES PARA ESTE PERFIL
            for (int i = 0; i < TablaTemporal.Rows.Count; i++)
            {
                //REALIZO FILTRO DETODO LOS GRUPOS EXISTENTES PARA ESTE PERFIL
                DataRow rowj = TablaTemporal.Rows[i];
                //TOMO VALOR DEL NOMBRE DEL GRUPO Y LO METO A VARIABLE
                string Grupo = rowj["Grupo"].ToString();
                //LLENO STRINGBUILDER CON CODIGO HTML Y CONCATENADO EL NOMBRE DEL GRUPO
                content.Append(System.Environment.NewLine + "&lt;div&gt;&lt;b&gt;&lt;FONT FACE='calibri'&gt;" + Grupo + ":&lt;/FONT&gt;&lt;/b&gt;" + System.Environment.NewLine + "&lt;/div&gt;" + System.Environment.NewLine);
                //REALIZO FILTRO EN TABLA DELA CONSULTA PRINCIPAL PARA VER QUE VALORES PERTENECEN A ESTE GRUPO
                DataView dv = new DataView(tbl);
                dv.RowFilter = "Grupo = '" + Grupo + "'";
                //CREO UNA TABLA PARA LLENAR LOS VALORES DEL GRUPO
                content.Append("&lt;table&gt;" + System.Environment.NewLine);
                //METO EL FILTRO A UNA TABLA PARA REUSARLA
                DataTable filter = dv.ToTable();
                //CREO CICLO PARA RECORRER TODOS LOS VALORES DE ESTE GRUPO
                for (int j = 0; j < dv.Count; j++)
                {
                    //REALIZO FILTRO DE TODOS LOS VALORES EXISTENTE EN ESTE GRUPO
                    DataRow rowk = filter.Rows[j];
                    //METO EL VALOR A UNA VARIABLE
                    string EtiquetaV = rowk["Etiqueta"].ToString();
                    string Valor = rowk["Valor"].ToString();
                    if (EtiquetaV.Equals(ValueLabel))
                    {
                        //METO A STRINGBUILDER CODIGO HTML CON EL VALOR OBTENIDO
                        content.Append("&lt;tr&gt;&lt;td&gt;&lt;/td&gt;&lt;td&gt;&lt;/td&gt;&lt;td&gt;&lt;FONT FACE='calibri'&gt;" + Valor + "&lt;/FONT&gt;&lt;/td&gt;&lt;/tr&gt;");
                    }
                    else
                    {
                        ValueLabel = EtiquetaV;
                        //METO A STRINGBUILDER CODIGO HTML CON EL VALOR OBTENIDO
                        content.Append("&lt;tr&gt;&lt;td&gt;&lt;/td&gt;&lt;td&gt;&lt;/td&gt;&lt;td&gt;&lt;FONT FACE='calibri'&gt;&lt;strong&gt;" + EtiquetaV + "&lt;/strong&gt;" + Valor + "&lt;/FONT&gt;&lt;/td&gt;&lt;/tr&gt;");
                    }
                }//TERMINA CICLO CON TODAS LA VARIABLES DEL GRUPO
                //CIERRO TABLA
                content.Append(System.Environment.NewLine + "&lt;/table&gt;&lt;/FONT&gt;&lt;br&gt;&lt;br&gt;" + System.Environment.NewLine);
            }//TERMINA CICLO CON TODOS LOS GRUPOS DE ESTE PERFIL
            //CIERRO CODIGO HTML EN EL STRINGBUILDER
            content.Append(System.Environment.NewLine + "&lt;/body&gt;" + System.Environment.NewLine + "&lt;/html&gt;");
            txthtml.Text = content.ToString();
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            PanelHTML.Visible = false;
            VistaPrevia.Visible = true;
            //Bajo tabla de la consulta a session
            DataTable tbl = (System.Data.DataTable)(Session["TablaFiltros"]);
            //Creo tabla con filtro DISTINCT pata que repita los grupos
            DataView dvr = new DataView(tbl);
            switch (ddlTipo.SelectedValue)
            {
                case "todo":
                    dvr.RowFilter = "externo = 1 or externo = 0";
                    break;

                case "occ":

                    dvr.RowFilter = "externo = 1";
                    break;

                case "excp":

                    dvr.RowFilter = "externo = 0";
                    break;
            }
            //Creo tabla con filtro DISTINCT pata que repita los grupos
            DataTable TablaTemporal = dvr.ToTable(true, "Grupo");
            //Saco vista de filas de la tabla de la consulta
            DataRow row = tbl.Rows[0];
            //saco el vaor del puesto o perfil y lo paso a variable
            string Titulo = row["Puesto"].ToString();
            //CREO STRINGBUILDER PARA LLENARLO CON CODIGO HTML
            StringBuilder content = new StringBuilder();
            //EMPIEZO CODIGO HTML
            content.Append("<meta http-equiv='Content-Type' content='text/html' charset='UTF-8'/>");
            content.Append(System.Environment.NewLine);
            content.Append("<html>");
            content.Append(System.Environment.NewLine);
            content.Append("<body>");
            content.Append(System.Environment.NewLine);
            //COLOCO ENCABEZADO = NOMBRE DEL PERFIL, Y CONCATENO VARIABLE QUE CONTIENE EL NOMBRE
            content.Append(System.Environment.NewLine + "<div style='text-align: center;'><FONT FACE='calibri' SIZE=5> <b>" + " " + Titulo + "</b></FONT>" + System.Environment.NewLine + "</div><br>" + System.Environment.NewLine);
            //CICLO QIE RECORRE TODO LOS GRUPOS EXISTENTES PARA ESTE PERFIL
            for (int i = 0; i < TablaTemporal.Rows.Count; i++)
            {
                //REALIZO FILTRO DETODO LOS GRUPOS EXISTENTES PARA ESTE PERFIL
                DataRow rowj = TablaTemporal.Rows[i];
                //TOMO VALOR DEL NOMBRE DEL GRUPO Y LO METO A VARIABLE
                string Grupo = rowj["Grupo"].ToString();
                //LLENO STRINGBUILDER CON CODIGO HTML Y CONCATENADO EL NOMBRE DEL GRUPO
                content.Append(System.Environment.NewLine + "<div><b><FONT FACE='calibri'>" + Grupo + ":</FONT></b>" + System.Environment.NewLine + "</div>" + System.Environment.NewLine);
                //REALIZO FILTRO EN TABLA DELA CONSULTA PRINCIPAL PARA VER QUE VALORES PERTENECEN A ESTE GRUPO
                DataView dv = new DataView(tbl);
                dv.RowFilter = "Grupo = '" + Grupo + "'";
                //CREO UNA TABLA PARA LLENAR LOS VALORES DEL GRUPO
                content.Append("<table>" + System.Environment.NewLine);
                //METO EL FILTRO A UNA TABLA PARA REUSARLA
                DataTable filter = dv.ToTable();
                //CREO CICLO PARA RECORRER TODOS LOS VALORES DE ESTE GRUPO
                for (int j = 0; j < dv.Count; j++)
                {
                    //REALIZO FILTRO DE TODOS LOS VALORES EXISTENTE EN ESTE GRUPO
                    DataRow rowk = filter.Rows[j];
                    string EtiquetaV = rowk["Etiqueta"].ToString();
                    string Valor = rowk["Valor"].ToString();
                    if (EtiquetaV.Equals(ValueLabel))
                    {
                        //METO A STRINGBUILDER CODIGO HTML CON EL VALOR OBTENIDO
                        content.Append("<tr><td></td><td></td><td><FONT FACE='calibri'>" + Valor + "</FONT></td></tr>");
                    }
                    else
                    {
                        ValueLabel = EtiquetaV;
                        //METO A STRINGBUILDER CODIGO HTML CON EL VALOR OBTENIDO
                        content.Append("<tr><td></td><td></td><td><FONT FACE='calibri'><strong>" + EtiquetaV + "</strong>" + Valor + "</FONT></td></tr>");
                    }
                }//TERMINA CICLO CON TODAS LA VARIABLES DEL GRUPO
                //CIERRO TABLA
                content.Append(System.Environment.NewLine + "</table></FONT><br><br>" + System.Environment.NewLine);
            }//TERMINA CICLO CON TODOS LOS GRUPOS DE ESTE PERFIL
            //CIERRO CODIGO HTML EN EL STRINGBUILDER
            content.Append(System.Environment.NewLine + "</body>" + System.Environment.NewLine + "</html>");
            lbVistaPrevia.Text = content.ToString();
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string parameters = Request.Url.Query;
            Response.Redirect(path + parameters);
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            String PreviousPage = (String)Session["Previus"];
            Response.Redirect(PreviousPage);
        }

        protected void btnPerfilesP_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("perfiles_pendiente.aspx");
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            //    string url = HttpContext.Current.Request.Url.AbsoluteUri;
            //    Session.Add("surl_comparar", url);
            LinkButton lnk = (LinkButton)sender;
            string idGrupo = lnk.CommandName.ToString();
            Session.Add("id_puestogpo_detalle", idGrupo);
            Response.Redirect("perfiles_captura.aspx?uborrador=1&uidc_puestoperfil=" + RequestIdBorr);
        }

        protected void btnDinamico_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            //BAJO INDEX DEL ELEMENTO
            int index = Convert.ToInt32(btn.CommandArgument.ToString());
            //Alert.ShowAlertError(index,this);
            //BUSCO PANEL EN BASE AL INDEX
            Panel Panel = (Panel)rpCompara.Items[index].FindControl("Produccion_Panel_Individual");
            if (btn.Text.Equals("Ver"))
            {
                btn.Text = "Ocultar";
                Panel.Visible = true;
            }
            else
            {
                btn.Text = "Ver";
                Panel.Visible = false;
            }
        }

        protected void btnDinamicoBorrador_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            //BAJO INDEX DEL ELEMENTO
            int index = Convert.ToInt32(btn.CommandArgument.ToString());
            //Alert.ShowAlertError(index,this);
            //BUSCO PANEL EN BASE AL INDEX
            Panel Panel = (Panel)rpBorrador.Items[index].FindControl("Borrador_Panel_Indiv");
            if (btn.Text.Equals("Ver"))
            {
                btn.Text = "Ocultar";
                Panel.Visible = true;
            }
            else
            {
                btn.Text = "Ver";
                Panel.Visible = false;
            }
        }

        /// <summary>
        /// Funcion que oculta paneles e inicia botones para mostrar
        /// </summary>
        /// <param name="repeater">Origen de datos</param>
        /// <param name="Control_btn">Control Tipo LinkButton</param>
        /// <param name="Control_panel">Control tipo Panel</param>
        public void ocultar_Paneles(Repeater repeater, string Control_btn, string Control_panel)
        {
            //BUSCO DENTRO DE LOS ITEM DEL REPEATER
            foreach (RepeaterItem item in repeater.Items)
            {
                LinkButton btn = (LinkButton)item.FindControl(Control_btn);
                Panel Panel = (Panel)item.FindControl(Control_panel);
                btn.Text = "Ver";
                Panel.Visible = false;
            }
        }

        protected void btnSinCambios_Click(object sender, EventArgs e)
        {
            //COMPRUEBO EL ESTADO DEL BOTON
            Boolean Visible = true;
            if (btnSinCambios.Text == "Ver Sin Cambios")
            {
                btnSinCambios.Text = "Ocultar Sin Cambios";
                Visible = true;
            }
            else
            {
                btnSinCambios.Text = "Ver Sin Cambios";
                Visible = false;
            }
            //BUSCO VONTROLES PARA OCULTARLOS
            foreach (RepeaterItem item in rpBorrador.Items)
            {
                LinkButton btn = (LinkButton)item.FindControl("btnOcultar_BodyPanel_Borrador");
                Panel Panel = (Panel)item.FindControl("Borrador_Panel_Indiv");
                Label lbl = (Label)item.FindControl("lblTipoDiferencia");
                if (lbl.Text.Equals(string.Empty))
                {
                    if (Visible == true)
                    {
                        btn.Text = "Ocultar";
                        Panel.Visible = true;
                        Alert.ShowAlertInfo("Este color, indica que el Grupo no a sido editado ni a sufrido cambios en su contenido.", "Sin Cambios", this);
                    }
                    else
                    {
                        btn.Text = "Ver";
                        Panel.Visible = false;
                    }
                }
            }
        }

        protected void btnEditado_Click(object sender, EventArgs e)
        {
            //ESTADO DEL BOTON
            Boolean Visible = true;
            if (btnEditado.Text == "Ver Grupo Editado")
            {
                btnEditado.Text = "Ocultar Grupo Editado";
                Visible = true;
            }
            else
            {
                btnEditado.Text = "Ver Grupo Editado";
                Visible = false;
            }
            //BUSCO CONTROLES DENTRO DEL REPEAT
            foreach (RepeaterItem item in rpBorrador.Items)
            {
                LinkButton btn = (LinkButton)item.FindControl("btnOcultar_BodyPanel_Borrador");
                Panel Panel = (Panel)item.FindControl("Borrador_Panel_Indiv");
                Label lbl = (Label)item.FindControl("lblTipoDiferencia");
                if (lbl.Text.Equals("Grupo Editado"))
                {
                    if (Visible == true)
                    {
                        btn.Text = "Ocultar";
                        Panel.Visible = true;
                        Alert.ShowAlertInfo("Este color, indica que el Grupo a sido editado o a sufrido cambios en su contenido.", "Grupo Editado", this);
                    }
                    else
                    {
                        btn.Text = "Ver";
                        Panel.Visible = false;
                    }
                }
            }
        }

        protected void btnNuevoGrupo_Click(object sender, EventArgs e)
        {
            //ESTADO DEL BOTON
            Boolean Visible = true;
            if (btnNuevoGrupo.Text == "Ver Nuevo Grupo")
            {
                btnNuevoGrupo.Text = "Ocultar Nuevo Grupo";
                Visible = true;
            }
            else
            {
                btnNuevoGrupo.Text = "Ver Nuevo Grupo";
                Visible = false;
            }
            //BUSCO CONTROLES DENTRO DEL REPEAT
            foreach (RepeaterItem item in rpBorrador.Items)
            {
                LinkButton btn = (LinkButton)item.FindControl("btnOcultar_BodyPanel_Borrador");
                Panel Panel = (Panel)item.FindControl("Borrador_Panel_Indiv");
                Label lbl = (Label)item.FindControl("lblTipoDiferencia");
                if (lbl.Text.Equals("Nuevo Grupo"))
                {
                    if (Visible == true)
                    {
                        btn.Text = "Ocultar";
                        Panel.Visible = true;
                        Alert.ShowAlertInfo("Este color, indica que el Grupo no se encuentra actualmente en el Perfil de Producción.", "Nuevo Grupo", this);
                    }
                    else
                    {
                        btn.Text = "Ver";
                        Panel.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Funcion que muestra el tipo de panel seleccionado
        /// </summary>
        /// <param name="TipoDiferencia">Valor con el tipo de diferencia: STRING.EMPTY = SIN DIFERENCIA</param>
        public void BotonesComprobar(string TipoDiferencia)
        {
            foreach (RepeaterItem item in rpBorrador.Items)
            {
                LinkButton btn = (LinkButton)item.FindControl("btnOcultar_BodyPanel_Borrador");
                Panel Panel = (Panel)item.FindControl("Borrador_Panel_Indiv");
                Label lbl = (Label)item.FindControl("lblTipoDiferencia");
                if (lbl.Text.Equals(TipoDiferencia))
                {
                    btn.Text = "Ocultar";
                    Panel.Visible = true;
                }
                else
                {
                    btn.Text = "Ver";
                    Panel.Visible = false;
                }
            }
        }

        protected void btnVerPanelesProduccion_Click(object sender, EventArgs e)
        {
            //ESTADO DEL BOTON
            Boolean Visible = true;
            if (btnVerPanelesProduccion.Text == "Ver Todos Publicados")
            {
                btnVerPanelesProduccion.Text = "Ocultar Todos Publicados";
                Visible = true;
            }
            else
            {
                btnVerPanelesProduccion.Text = "Ver Todos Publicados";
                Visible = false;
            }
            //BUSCO CONTROLES DENTRO DEL REPEAT
            foreach (RepeaterItem item in rpCompara.Items)
            {
                LinkButton btn = (LinkButton)item.FindControl("btnOcultar_BodyPanel");
                Panel Panel = (Panel)item.FindControl("Produccion_Panel_Individual");

                if (Visible == true)
                {
                    btn.Text = "Ocultar";
                    Panel.Visible = true;
                }
                else
                {
                    btn.Text = "Ver";
                    Panel.Visible = false;
                }
            }
        }

        protected void lnkEditarPerfil_Click(object sender, EventArgs e)
        {
            int Tipo_Vista = Convert.ToInt32(Request.QueryString["borrador"].ToString());//tomamos el valor booleano de borrador
            if (Tipo_Vista == 1)//Si variable request es 1 mandamos editar borrado
            {
                int RequestIDV = Convert.ToInt32(Request.QueryString["uidc_puestoperfil_borr"].ToString());//Tipo Borrador
                Response.Redirect("perfiles_captura.aspx?uidc_puestoperfil=" + RequestIDV + "&uborrador=1");
            }
            else
            {//no existe creamos borrador
                RequestId = Convert.ToInt32(Request.QueryString["uidc_puestoperfil"].ToString());//Tipo Produccion
                ComprobarBorrador(RequestId);
            }
        }

        /// <summary>
        /// Comprueba si existe un borrador, si no existe crea uno y redirecciona a pagina de edicion.
        /// </summary>
        /// <param name="idPerfil"></param>
        public void ComprobarBorrador(int idPerfil)
        {
            //Bajo id de usuario de session
            int idUsuario = (int)Session["sidc_usuario"];
            //declaro entidad
            PerfilesE EntidadB = new PerfilesE();
            EntidadB.Idc_perfil = idPerfil;
            EntidadB.Usuario = idUsuario;
            //declaro componente
            PerfilesBL Componente = new PerfilesBL();
            //cargo dataset desde store
            DataSet data = Componente.VerificaPerfilBorrador(EntidadB);
            //bajo tabla de mensajes de errores

            //bajo tabla de resultados
            DataTable result = data.Tables[0];
            DataRow rowr = result.Rows[0];
            string resultado = rowr["Resultado"].ToString();
            //si hay algun mensaje
            if (resultado.Equals(string.Empty))
            {
                DataTable mensaje_de_error = data.Tables[1];
                DataRow rowerror = mensaje_de_error.Rows[0];
                string mensaje = rowerror["mensaje"].ToString();
                Alert.ShowAlertError(mensaje, this);
            }
            else
            {//si no hay errores mando llamar pagina de ediciones con parametro uborrador en true(1)
                DataRow row = result.Rows[0];
                string idc_puestoperfil_borr = row["Resultado"].ToString();
                Response.Redirect("perfiles_captura.aspx?uidc_puestoperfil=" + idc_puestoperfil_borr + "&uborrador=1");
            }
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        protected void lnkReturn_Click1(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        protected void btnIR_Click(object sender, EventArgs e)
        {
            Button go = (Button)sender;
            string url = go.CommandName.ToString();
            string tipo = Path.GetExtension(url);
            if (!File.Exists(url))
            {
                Alert.ShowAlertError("El documento no se encuentra en el servidor o no esta disponible. Intentelo nuevamento o comuniquese con el depto de sistemas.", this);
            }
            if (File.Exists(url) && tipo != ".html")
            {
                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                Response.AddHeader("Content-Disposition", "attachment; filename=" + url);
                // Escribimos el fichero a enviar
                Response.WriteFile(url);
                // volcamos el stream
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
                // Response.End();
            }
            if (File.Exists(url) && tipo == ".html")
            {
                url = funciones.ConvertStringToHex(url);
                String urlt = HttpContext.Current.Request.Url.AbsoluteUri;
                String path_actual = urlt.Substring(urlt.LastIndexOf("/") + 1);
                urlt = urlt.Replace(path_actual, "");
                urlt = urlt + "view_files.aspx?file=" + url;
                ScriptManager.RegisterStartupScript(this, GetType(), "noti533wswswsW3", "window.open('" + urlt + "');", true);
            }
        }

        protected void btneditar_Click(object sender, EventArgs e)
        {
            Button go = (Button)sender;
            string url = go.CommandName.ToString();
            string tipo = Path.GetExtension(url); String urle = HttpContext.Current.Request.Url.AbsoluteUri;
            String path_actual = urle.Substring(urle.LastIndexOf("/") + 1);
            if (File.Exists(url) && tipo == ".html")
            {
                Session["page_backhtml"] = path_actual;
                string tipos = Request.QueryString["borrador"] == "1" ? "B" : "P";
                string id = "";
                if (Request.QueryString["uidc_puestoperfil_borr"] != null)//Si variable request es nulla
                {
                    id = Request.QueryString["uidc_puestoperfil_borr"];
                }
                if (Request.QueryString["uidc_puestoperfil"] != null)//Si variable request es nulla
                {
                    id = Request.QueryString["uidc_puestoperfil"];
                }
                string T = funciones.deTextoa64(@url);
                ScriptManager.RegisterStartupScript(this, GetType(), "noti533wswswsW3", "window.open('html.aspx?edit_live=true&url=" + T + "&tipo=" + tipos + "&idc_perfil=" + id + "');", true);
                // Response.Redirect("html.aspx?edit_live=true&url=" + T + "&tipo=" + tipos + "&idc_perfil=" + id);
            }
            else
            {
                Alert.ShowAlertError("No existe el archivo", this);
            }
        }

        //protected void btnVerPanelesBorrador_Click(object sender, EventArgs e)
        //{
        //    foreach (RepeaterItem item in rpBorrador.Items)
        //    {
        //        LinkButton btn = (LinkButton)item.FindControl("btnOcultar_BodyPanel_Borrador");
        //        Panel Panel = (Panel)item.FindControl("Borrador_Panel_Indiv");

        //        if (btn.Text.Equals("Ver"))
        //        {
        //            btn.Text = "Ocultar";
        //            Panel.Visible = true;
        //            btnVerPanelesBorrador.Text = "Ocultar Todos de Borrador";
        //            btnSinCambios.Text = "Ocultar Sin Cambios";
        //            btnEditado.Text = "Ocultar Grupo Editado";
        //            btnNuevoGrupo.Text = "Ocultar Nuevo Grupo";
        //        }
        //        else
        //        {
        //            btn.Text = "Ver";
        //            Panel.Visible = false;
        //            btnVerPanelesBorrador.Text = "Ver Todos Borrador";
        //            btnSinCambios.Text = "Ver Sin Cambios";
        //            btnEditado.Text = "Ver Grupo Editado";
        //            btnNuevoGrupo.Text = "Ver Nuevo Grupo";
        //        }
        //    }
        //}
    }
}