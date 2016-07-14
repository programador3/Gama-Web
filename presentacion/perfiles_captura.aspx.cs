using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class perfiles_captura2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)
            {
                Response.Redirect("menu.aspx");
            }

            if (!Page.IsPostBack)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 1000000);
                lblsession.Text = randomNumber.ToString();
                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("nombre");
                papeleria.Columns.Add("descripcion");
                papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("extension");
                papeleria.Columns.Add("id_archi");
                Session[lblsession.Text + "papeleria_perfil"] = papeleria;
                DataTable archivos_etiquetas = new DataTable();
                archivos_etiquetas.Columns.Add("ruta");
                archivos_etiquetas.Columns.Add("etiqueta");
                archivos_etiquetas.Columns.Add("idc");
                archivos_etiquetas.Columns.Add("nombre");
                Session[lblsession.Text + "archivos_etiquetas"] = archivos_etiquetas;
                ////valida si tiene permiso de ver esta pagina//
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                //int idc_opcion = 1798;
                int idc_opcion = 1806;
                int idc_tipo_aut = 317;
                //if (funciones.permiso(idc_usuario, idc_opcion) == false)
                //{
                //    Response.Redirect("menu.aspx");
                //    return;
                //}
                //fin
                //validacion de opcion
                //fin

                int idcperfil;
                int uborrador = 8; //valor equis el 8 no tiene relevancia solo el 1 y 0

                if (!string.IsNullOrEmpty(Request.QueryString["uidc_puestoperfil"]))
                {
                    idcperfil = Convert.ToInt32(Request.QueryString["uidc_puestoperfil"]);
                }
                else
                {
                    idcperfil = 0;
                }
                //borrador
                if (!string.IsNullOrEmpty(Request.QueryString["uborrador"]))
                {
                    uborrador = Convert.ToInt32(Request.QueryString["uborrador"]);
                }

                if (uborrador == 1)
                {
                    check_borr_prod.Checked = true;
                }
                else if (uborrador == 0)
                {
                    check_borr_prod.Checked = false;
                    //TIENE PERMISO PARA PRODUCCION O NO 18-09-2015
                    //if (funciones.autorizacion(idc_usuario, idc_tipo_aut) == false)
                    //{
                    //    Response.Redirect("perfiles.aspx?borrador=1");
                    //}
                    //FIN
                }
                else
                { //valor erroneo
                    Response.Redirect("perfiles.aspx");
                    return;
                }

                // en base a si es borrador o no cambiar las llaves primarias
                if (check_borr_prod.Checked)
                {
                    //borrador
                    llave_puestoperfil.Value = "idc_puestoperfil_borr";
                    llave_d_eti_lib.Value = "idc_perfiletiq_opc_dato_lib_borr";
                    llave_d_eti_opc.Value = "idc_perfiletiq_opc_dato_borr";
                    llave_d_gpo_lib.Value = "idc_perfilgpod_dato_lib_borr";
                    llave_d_gpo_opc.Value = "idc_perfilgpod_dato_borr";
                    //string[] llaves = { "idc_perfiletiq_opc_dato_lib_borr" };
                    //gridgpo_lib.DataKeyNames = llaves;
                    lblmensaje.Text = "Borrador";
                    lblmensaje.CssClass = "btn btn-primary";
                }
                else
                {
                    //produccion
                    llave_puestoperfil.Value = "idc_puestoperfil";
                    llave_d_eti_lib.Value = "idc_perfiletiq_opc_dato_lib";
                    llave_d_eti_opc.Value = "idc_perfiletiq_opc_dato";
                    llave_d_gpo_lib.Value = "idc_perfilgpod_dato_lib";
                    llave_d_gpo_opc.Value = "idc_perfilgpod_dato";

                    //string[] llaves = { "idc_perfiletiq_opc_dato_lib" };
                    //gridgpo_lib.DataKeyNames = llaves;
                    lblmensaje.Text = "Produccion";
                    lblmensaje.CssClass = "btn btn-success";
                }

                if (idcperfil == 0)
                { //nuevo
                    this.lit_titulo.Text = "Captura de Perfil";
                }
                else
                {
                    //
                    this.lit_titulo.Text = "Edición de Perfil";
                    oc_idc_puestoperfil.Value = idcperfil.ToString();
                }
                //cargo la lista de documentos add 02-12-2015
                //metodo
                cargar_documentos_lista();
                GenerarTablasDatos(idcperfil);
                Grupos_backendBL Componente = new Grupos_backendBL();
                DataSet ds = new DataSet();
                ds = Componente.cargar_droplist();
                repite_menu_grupos.DataSource = ds.Tables[0];
                repite_menu_grupos.DataBind();
                //recorreMenu();

                //add 18-09-2015
                if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_REFERER"]))
                {
                    oc_paginaprevia.Value = Request.ServerVariables["HTTP_REFERER"].ToString();
                }
                else
                {
                    oc_paginaprevia.Value = "perfiles.aspx";
                }

                //una vez utilizada la reseteo
                Session["id_puestogpo_detalle"] = 0;
                //tabla papeleria
            }
            gridgpo_lib.DataKeyNames = new string[] { llave_d_gpo_lib.Value.ToString() };
        }

        protected void cargar_formulario(int idc_perfilgpo)
        {
            DataSet ds = new DataSet();
            //declaro la entidad
            Grupos_backendE EntGrupo = new Grupos_backendE();
            EntGrupo.Idc_perfilgpo = idc_perfilgpo;
            //declaramos el componente
            Grupos_backendBL ComGrupo = new Grupos_backendBL();
            try
            {
                //recuperamos las tablas donde vienen las configuraciones para generar el formulario de captura.
                ds = ComGrupo.grupos_tablas(EntGrupo);
                DataTable tbl_perfiles_gpo = ds.Tables[0];
                DataTable tbl_perfiles_gpo_d = ds.Tables[1];
                DataTable tbl_perfiles_etiq = ds.Tables[2];
                DataTable tbl_perfiles_etiq_opc = ds.Tables[3];
                Session.Add(lblsession.Text + "TablaPerfilEtiqOpc", tbl_perfiles_etiq_opc);
                Session.Add(lblsession.Text + "TablaPerfilGpoD", tbl_perfiles_gpo_d);
                Session.Add(lblsession.Text + "TablaPerfilGpo", tbl_perfiles_gpo);
                Session.Add(lblsession.Text + "tbl_perfiles_etiq", tbl_perfiles_etiq);
                //revisamos el registro de la primer tabla
                //ETAPA DE GRUPOS
                DataRow fila = tbl_perfiles_gpo.Rows[0];
                lblgrupotitulo.Text = "<i class='fa fa-list-alt' aria-hidden='true'></i>" + " " + fila["grupo"].ToString();
                bool libre = Convert.ToBoolean(fila["libre"].ToString());
                if (libre)
                { //construye una caja de texto
                    //mostrarlo
                    oc_idcperfilgpo_lib.Value = fila["idc_perfilgpo"].ToString();
                    lblgpolibre.Text = fila["grupo"].ToString();
                    //mensaje de validacion
                    int min = Convert.ToInt32(fila["minimo_libre"]);
                    int max = Convert.ToInt32(fila["maximo_libre"]);
                    lblmax.Text = max.ToString();
                    lit_mensaje_gpo_lib.Text = "[" + mensaje_validacion(min, max) + "]";

                    //fin mensaje de validacion
                    panel_gpo_libre.Visible = true;
                    panel_grupo_detalle.Visible = true;
                    //entradaGpoLibre();
                }
                else
                {
                    panel_gpo_libre.Visible = false;
                    oc_idcperfilgpo_lib.Value = "";
                }
                bool opciones = Convert.ToBoolean(fila["opciones"].ToString());
                if (opciones)
                { //construye un droplist
                    lblgpoopc.Text = fila["grupo"].ToString();
                    //mensaje de validacion
                    int min_opc = Convert.ToInt32(fila["minimo_opc"]);
                    int max_opc = Convert.ToInt32(fila["maximo_opc"]);
                    lit_mensaje_gpo_opc.Text = "[" + mensaje_validacion(min_opc, max_opc) + "]";
                    //fin de mensaje de validacion
                    int idc_gpo = Convert.ToInt32(fila["idc_perfilgpo"].ToString());
                    lblmax.Text = max_opc.ToString();
                    lblmin.Text = min_opc.ToString();
                    check_gpo_opc.DataSource = buscarOpcGpo(idc_gpo);
                    check_gpo_opc.DataTextField = "descripcion";
                    check_gpo_opc.DataValueField = "idc_perfilgpod";
                    check_gpo_opc.DataBind();
                    //revisa si tiene datos la tabla para checkearlos
                    check_opciones_gpo();
                    //gridgpo_opc.DataSource = buscarOpcGpo(idc_gpo);
                    //gridgpo_opc.DataBind();

                    panel_gpo_opc.Visible = true;
                    panel_grupo_detalle.Visible = true;
                    //entradaGpoOpcion(tbl_perfiles_gpo_d);
                }
                else
                {
                    //limpiamos el checkboxlist
                    check_gpo_opc.Items.Clear();
                    panel_gpo_opc.Visible = false;
                }
                if (libre == false & opciones == false)
                {
                    panel_gpo_libre.Visible = false;
                    panel_grupo_detalle.Visible = false;
                    panel_gpo_opc.Visible = false;
                    panel_grupo_detalle.Visible = false;
                    gridgpo_lib.Visible = false;
                    gridgpo_opc.Visible = false;
                }
                else
                {
                    gridgpo_lib.Visible = true;
                    //gridgpo_opc.Visible = true;
                }
                //FIN DE ETAPA DE GRUPOS

                ////ETAPA DE ETIQUETAS
                int totrow = tbl_perfiles_etiq.Rows.Count;
                if (totrow > 0)
                {
                    panel_etiquetas.Visible = true;
                    //gridetiq_lib.Visible = true;
                    //gridetiq_opc.Visible = true;
                    repite_etiqueta_controles.DataSource = tbl_perfiles_etiq;
                    repite_etiqueta_controles.DataBind();
                    //procesarEtiquetas(tbl_perfiles_etiq, tbl_perfiles_etiq_opc);
                }
                else
                { //sin etiquetas
                    panel_etiquetas.Visible = false;
                    //gridetiq_lib.Visible = false;
                    //gridetiq_opc.Visible = false;
                    // PanelOpciones.Visible = false;
                    //PanelLibre.Visible = true;
                }
                ////FIN DE ETAPA DE ETIQUETAS
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void repite_etiqueta_controles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView dbr = (DataRowView)e.Item.DataItem;
                if (Convert.ToString(DataBinder.Eval(dbr, "libre")) == "False")
                { //lleva opciones
                    //llenar el check
                    int idc_perfiletiq = Convert.ToInt32(DataBinder.Eval(dbr, "idc_perfiletiq"));
                    CheckBoxList check = (CheckBoxList)e.Item.FindControl("check_etiqopc");
                    check.DataSource = buscarOpcetiqueta(idc_perfiletiq);
                    check.DataTextField = "descripcion";
                    check.DataValueField = "idc_perfiletiq_opc";
                    check.DataBind();
                    //bajar la tabla de session
                    DataTable tbl_puestos_perfil_d_eti_opc = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiOpc"];
                    if (tbl_puestos_perfil_d_eti_opc.Rows.Count > 0)
                    {
                        //DataRow[] filas = tbl_puestos_perfil_d_eti_opc.Select("idc_perfiletiq = " + idc_perfiletiq);
                        foreach (DataRow fila in tbl_puestos_perfil_d_eti_opc.Rows)
                        {
                            //string bug = fila["borrado"].ToString();
                            if (fila["borrado"].ToString() == "False")
                            { //check
                                foreach (ListItem lista in check.Items)
                                {
                                    if (lista.Value.ToString() == fila["idc_perfiletiq_opc"].ToString())
                                    {
                                        lista.Selected = true; ;
                                    }
                                }
                            }
                        }
                    }
                    //lleva opciones}
                    Label lblopc = (Label)e.Item.FindControl("lbletiquetaopcion");
                    lblopc.Text = DataBinder.Eval(dbr, "nombre").ToString();
                    //validacion de mensaje
                    int min_opc = Convert.ToInt32(DataBinder.Eval(dbr, "minimo_sel"));
                    int max_opc = Convert.ToInt32(DataBinder.Eval(dbr, "maximo_sel"));
                    Literal lit_mensaje_opc = (Literal)e.Item.FindControl("lit_mensaje_etiq_opc");
                    lit_mensaje_opc.Text = "[" + mensaje_validacion(min_opc, max_opc) + "]";
                    //fin de validacion
                }
                else
                {
                    //es etiqueta libre
                    Label lbllibre = (Label)e.Item.FindControl("lbletiquetalibre");
                    lbllibre.Text = DataBinder.Eval(dbr, "nombre").ToString();
                    //mensaje de validacion
                    int min_lib = Convert.ToInt32(DataBinder.Eval(dbr, "minimo_sel"));
                    int max_lib = Convert.ToInt32(DataBinder.Eval(dbr, "maximo_sel"));
                    Literal lit_mensaje = (Literal)e.Item.FindControl("lit_mensaje_etiq");
                    lit_mensaje.Text = "[" + mensaje_validacion(min_lib, max_lib) + "]";
                    //fin de mensaje de validacion
                    //revisamos en session si hay items de esta etiqueta
                    int idc_perfiletiq = Convert.ToInt32(DataBinder.Eval(dbr, "idc_perfiletiq"));
                    DataTable tbl_puestos_perfil_d_eti_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiLib"];
                    if (tbl_puestos_perfil_d_eti_lib.Rows.Count > 0)
                    {
                        DataView dt_libre = tbl_puestos_perfil_d_eti_lib.DefaultView;
                        dt_libre.RowFilter = "idc_perfiletiq = " + idc_perfiletiq + " and borrado=0";
                        if (dt_libre.Count > 0)
                        {
                            //genera el grid aqui estamos
                            GridView grid_etiq_lib = (GridView)e.Item.FindControl("grid_dinamico_etiqlibre");
                            grid_etiq_lib.DataKeyNames = new string[] { llave_d_eti_lib.Value.ToString(), "texto", "idc_perfiletiq" };
                            DataTable tabletemp = new DataTable();
                            tabletemp = dt_libre.ToTable();
                            tabletemp.DefaultView.Sort = "orden";
                            tabletemp = tabletemp.DefaultView.ToTable();
                            grid_etiq_lib.DataSource = tabletemp;
                            grid_etiq_lib.DataBind();
                        }
                    }
                }
                GridView grid_dinamico_etiqlibre = (GridView)e.Item.FindControl("grid_dinamico_etiqlibre");
                int dataval = Convert.ToInt32(DataBinder.Eval(dbr, "archivo"));
                if (dataval == 0)
                {
                    grid_dinamico_etiqlibre.Columns[3].Visible = false;
                    grid_dinamico_etiqlibre.Columns[4].Visible = false;
                }
            }
        }

        /// <summary>
        /// recibe el id de la etiqueta y recupera las opciones correspondientes a la misma
        /// </summary>
        /// <param name="idc_etiqueta"></param>
        /// <returns></returns>
        protected DataView buscarOpcetiqueta(int idc_etiqueta)
        {
            DataTable tbl_perfiles_etiq_opc = (DataTable)Session[lblsession.Text + "TablaPerfilEtiqOpc"];
            DataView dv = tbl_perfiles_etiq_opc.DefaultView;
            dv.RowFilter = "idc_perfiletiq=" + idc_etiqueta.ToString();
            return dv;
        }

        protected DataView buscarOpcGpo(int idc_gpo)
        {
            DataTable tbl_perfiles_gpo_d = (DataTable)Session[lblsession.Text + "TablaPerfilGpoD"];
            DataView dv = tbl_perfiles_gpo_d.DefaultView;
            dv.RowFilter = "idc_perfilgpo=" + idc_gpo.ToString();
            return dv;
        }

        protected void GenerarTablasDatos(int idc_puestoperfil)
        {
            try
            {
                DataSet ds = new DataSet();
                //entidad
                PerfilesE entidad = new PerfilesE();
                entidad.Idc_perfil = idc_puestoperfil;
                //componente
                PerfilesBL componente = new PerfilesBL();
                if (check_borr_prod.Checked)
                {
                    ds = componente.perfiles_datos_borr(entidad);
                }
                else
                {
                    ds = componente.perfiles_datos(entidad);
                }
                //ds = componente.perfiles_datos(entidad);
                DataTable tbl_puestos_perfil = ds.Tables[0];
                Session.Add(lblsession.Text + "TablaPuestoPerfil", tbl_puestos_perfil);
                if (tbl_puestos_perfil.Rows.Count > 0)
                {
                    txtnomperfil.Text = tbl_puestos_perfil.Rows[0]["descripcion"].ToString();
                }
                //
                DataTable tbl_puestos_perfil_d_gpo_lib = ds.Tables[1];
                Session.Add(lblsession.Text + "TablaPuestoPerfilDGpoLib", tbl_puestos_perfil_d_gpo_lib);
                //
                DataTable tbl_puestos_perfil_d_gpo_opc = ds.Tables[2];
                tbl_puestos_perfil_d_gpo_opc.Columns.Add("grupo");
                //tbl_puestos_perfil_d_gpo_opc.Columns.Add("idc_perfilgpo");
                Session.Add(lblsession.Text + "TablaPuestoPerfilDGpoOpc", tbl_puestos_perfil_d_gpo_opc);
                //
                DataTable tbl_puestos_perfil_d_eti_lib = ds.Tables[3];
                //tbl_puestos_perfil_d_eti_lib.Columns.Add("idc_perfilgpo");
                Session.Add(lblsession.Text + "TablaPuestoPerfilDEtiLib", tbl_puestos_perfil_d_eti_lib);
                //
                DataTable tbl_puestos_perfil_d_eti_opc = ds.Tables[4];
                tbl_puestos_perfil_d_eti_opc.Columns.Add("etiqueta");
                //tbl_puestos_perfil_d_eti_opc.Columns.Add("idc_perfilgpo");
                Session.Add(lblsession.Text + "TablaPuestoPerfilDEtiOpc", tbl_puestos_perfil_d_eti_opc);
                // tabla para llenar los documentos previamente seleccionados add 02-12-2015
                DataTable tbl_perfiles_docs = ds.Tables[5];
                seleccionar_checks_perfil_docs(tbl_perfiles_docs);
                //tablas para los archivos
                DataTable papeleria = new DataTable();
                papeleria.Columns.Add("nombre");
                papeleria.Columns.Add("descripcion");
                papeleria.PrimaryKey = new DataColumn[] { papeleria.Columns["descripcion"] };
                papeleria.Columns.Add("ruta");
                papeleria.Columns.Add("extension");
                papeleria.Columns.Add("id_archi");

                string clave = "PPA_BOR";
                if (Convert.ToInt32(Request.QueryString["uborrador"]) == 0) { clave = "PPA_PRO"; }
                if (ds.Tables[6] != null)
                {
                    foreach (DataRow row in ds.Tables[6].Rows)
                    {
                        DataRow new_row = papeleria.NewRow();
                        new_row["nombre"] = row["id_archi"];
                        new_row["nombre"] = row["nombre"];
                        new_row["descripcion"] = row["descripcion"];
                        new_row["ruta"] = GenerarRuta(clave) + row["ruta"];
                        new_row["extension"] = row["extension"];
                        new_row["id_archi"] = row["id_archi"];
                        papeleria.Rows.Add(new_row);
                    }
                }
                Session[lblsession.Text + "papeleria_perfil"] = papeleria;
                gridPapeleria.DataSource = papeleria;
                gridPapeleria.DataBind();
                //aqui esta

                carga_examenes();
                if (idc_puestoperfil != 0)
                {
                    DataTable tabla_examenes = ds.Tables[7];
                    foreach (ListItem item in chxExamenes.Items)
                    {
                        foreach (DataRow row in tabla_examenes.Rows)
                        {
                            if (item.Value == row["idc_examen"].ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }

                DataTable archivos_etiquetas = new DataTable();
                archivos_etiquetas.Columns.Add("ruta");
                archivos_etiquetas.Columns.Add("etiqueta");
                archivos_etiquetas.Columns.Add("nombre");
                archivos_etiquetas.Columns.Add("idc");
                gridPapeleriaEtiquetas.DataSource = ds.Tables[8];
                gridPapeleriaEtiquetas.DataBind();
                foreach (DataRow ROW in ds.Tables[8].Rows)
                {
                    DataRow new_row = archivos_etiquetas.NewRow();
                    new_row["ruta"] = ROW["ruta"];
                    new_row["etiqueta"] = ROW["etiqueta"];
                    new_row["nombre"] = ROW["nombre"];
                    new_row["idc"] = ROW["idc"];
                    archivos_etiquetas.Rows.Add(new_row);
                }

                Session[lblsession.Text + "archivos_etiquetas"] = archivos_etiquetas;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// metodo que regresa el id mas alto del datatable
        /// </summary>
        /// <returns></returns>
        protected int consecutivo(string nomtabla, string primarykey)
        {
            int idmax = 0;
            //bajamos nuestra tabla de la session
            DataTable tabla = (DataTable)(Session[nomtabla]);
            if (tabla.Rows.Count > 0)
            {
                //revisamos

                DataRow[] rows = tabla.Select("", primarykey + " DESC");
                idmax = Convert.ToInt32(rows[0][primarykey]);
            }
            else
            {
                idmax = 0;
            }
            return idmax + 1;
        }

        protected bool duplicado(string nomtabla, string columna, string valor)
        {
            DataTable tabla = (DataTable)(Session[nomtabla]);
            if (tabla.Rows.Count > 0)
            {
                //revisamos
                int total;
                DataRow[] rows = tabla.Select(columna + " = " + "'" + valor + "' and borrado=0");
                total = rows.Length;
                if (total > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// metodo que revisa cuando se edita un registro que la informacion modiicada no sea igual a otro registro ya existente
        /// </summary>
        /// <param name="nomtabla"></param>
        /// <param name="columna"></param>
        /// <param name="valor"></param>
        /// <param name="adicional"> puedes meter condiciones adicionales empezando con and  u or</param>
        /// <returns></returns>
        protected bool duplicadoEdicion(string nomtabla, string columna, string valor, string adicional)
        {
            DataTable tabla = (DataTable)(Session[nomtabla]);
            if (tabla.Rows.Count > 0)
            {
                //revisamos
                int total;
                DataRow[] rows = tabla.Select(columna + " = " + "'" + valor + "' and borrado=0 " + adicional);
                total = rows.Length;
                if (total > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void limpiarControles()
        {
            //los grupos
            lblgpolibre.Text = "";
            txtgpolibre.Text = "";
            //
            lblgpoopc.Text = "";
            //cboxgpoopc.Items.Clear();
        }

        protected void filtrarGrids()
        {
            //bajamos las tablas de session
            DataTable tbl_puestos_perfil_d_gpo_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoLib"];
            int totbug = tbl_puestos_perfil_d_gpo_lib.Rows.Count;
            DataView dv_gpo_lib = tbl_puestos_perfil_d_gpo_lib.DefaultView;
            //dv_gpo_lib.RowFilter = "idc_perfilgpo = " + DropGpos.SelectedValue;
            dv_gpo_lib.RowFilter = "idc_perfilgpo = " + ocgpoidmenu.Value + " and borrado=0";
            DataTable tabletemp = new DataTable();
            tabletemp = dv_gpo_lib.ToTable();
            tabletemp.DefaultView.Sort = "orden";
            tabletemp = tabletemp.DefaultView.ToTable();
            gridgpo_lib.DataSource = tabletemp;
            gridgpo_lib.DataBind();
            DataTable tbl_puestos_perfil_d_gpo_opc = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoOpc"];
            DataView dv_gpo_opc = tbl_puestos_perfil_d_gpo_opc.DefaultView;
            //dv_gpo_opc.RowFilter = "idc_perfilgpo = " + DropGpos.SelectedValue;
            dv_gpo_opc.RowFilter = "idc_perfilgpo = " + ocgpoidmenu.Value + " and borrado=0";
            gridgpo_opc.DataSource = dv_gpo_opc;
            gridgpo_opc.DataBind();
        }

        protected void gridgpo_lib_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //recuperamos el indice del registro seleccionado del grid de Telefono
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(gridgpo_lib.DataKeys[index].Value);
            GridView GRID = (GridView)sender;
            DataTable tbl_puestos_perfil_d_gpo_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoLib"];
            int VALUE_GRID = GRID.Rows.Count;
            int columns = GRID.Columns.Count;
            int ROWS = GRID.Rows.Count;
            for (int i = 0; i < columns; i++)
            {
                for (int J = 0; J < ROWS; J++)
                {
                    // GRID.Rows[index].Cells[i].BackColor = Color.FromName("#A9F5A9");
                    GRID.Rows[J].Cells[i].BackColor = Color.FromName("#FFFFFF");
                }
            }
            //Session["sidc_perfilgpo"] = Convert.ToInt32(vidc.ToString().Trim());
            switch (e.CommandName)
            {
                case "up":
                    int index3 = Convert.ToInt32(e.CommandArgument) - 1;
                    for (int i = 0; i < columns; i++)
                    {
                        GRID.Rows[index3].Cells[i].BackColor = Color.FromName("#A9F5A9");
                        //GRID.Rows[index3].Cells[i].BackColor = Color.FromName("#FFFFFF");
                    }
                    if (index3 < 0) { }
                    else
                    {
                        int vidc3 = Convert.ToInt32(gridgpo_lib.DataKeys[index3].Value);
                        DataRow[] row_down_change = tbl_puestos_perfil_d_gpo_lib.Select(llave_d_gpo_lib.Value.ToString() + " = " + vidc3);
                        int orden = Convert.ToInt32(row_down_change[0]["orden"].ToString());
                        foreach (DataRow row_change in tbl_puestos_perfil_d_gpo_lib.Rows)
                        {
                            if (row_change[llave_d_gpo_lib.Value.ToString()].ToString() == vidc.ToString())
                            {
                                row_change["orden"] = orden;
                                break;
                            }
                        }
                        foreach (DataRow row_change in tbl_puestos_perfil_d_gpo_lib.Rows)
                        {
                            int orden_cambioi = Convert.ToInt32(row_change["orden"].ToString());
                            if (row_change[llave_d_gpo_lib.Value.ToString()].ToString() != vidc.ToString() && orden_cambioi >= orden)
                            {
                                row_change["orden"] = Convert.ToInt32(row_change["orden"]) + 1;
                            }
                        }

                        DataView dv_gpo_lib = tbl_puestos_perfil_d_gpo_lib.DefaultView;
                        dv_gpo_lib.RowFilter = "idc_perfilgpo = " + ocgpoidmenu.Value + " and borrado=0";
                        DataTable temp = dv_gpo_lib.ToTable();
                        temp.DefaultView.Sort = "orden";
                        temp = temp.DefaultView.ToTable();
                        foreach (DataRow row_change in temp.Rows)
                        {
                            int indexd = temp.Rows.IndexOf(row_change);
                            row_change["orden"] = indexd + 1;
                        }
                        foreach (DataRow row_change in tbl_puestos_perfil_d_gpo_lib.Rows)
                        {
                            foreach (DataRow row_change2 in temp.Rows)
                            {
                                if (row_change["idc_perfilgpo"].ToString() == row_change2["idc_perfilgpo"].ToString() && row_change["texto"].ToString() == row_change2["texto"].ToString())
                                {
                                    row_change["orden"] = row_change2["orden"];
                                }
                            }
                        }

                        //subir
                        Session.Add(lblsession.Text + "TablaPuestoPerfilDGpoLib", tbl_puestos_perfil_d_gpo_lib);
                        //actualiza
                        ////actualizar grids
                        filtrarGrids();
                        //AQUI SE QUEDO
                    }
                    break;

                case "down":

                    int index2 = Convert.ToInt32(e.CommandArgument) + 1;
                    for (int i = 0; i < columns; i++)
                    {
                        GRID.Rows[index2].Cells[i].BackColor = Color.FromName("#A9F5A9");
                        //GRID.Rows[index3].Cells[i].BackColor = Color.FromName("#FFFFFF");
                    }
                    if (index2 >= VALUE_GRID)
                    {
                    }
                    else
                    {
                        int vidc2 = Convert.ToInt32(gridgpo_lib.DataKeys[index2].Value);
                        DataRow[] row_down_change = tbl_puestos_perfil_d_gpo_lib.Select(llave_d_gpo_lib.Value.ToString() + " = " + vidc2);
                        int orden = Convert.ToInt32(row_down_change[0]["orden"].ToString());
                        foreach (DataRow row_change in tbl_puestos_perfil_d_gpo_lib.Rows)
                        {
                            if (row_change[llave_d_gpo_lib.Value.ToString()].ToString() == vidc.ToString())
                            {
                                row_change["orden"] = orden + 1;
                                break;
                            }
                        }
                        foreach (DataRow row_change in tbl_puestos_perfil_d_gpo_lib.Rows)
                        {
                            int orden_cambioi = Convert.ToInt32(row_change["orden"].ToString());
                            string llave_actual = row_change[llave_d_gpo_lib.Value.ToString()].ToString();
                            string llave_cambio = row_change[llave_d_gpo_lib.Value.ToString()].ToString();
                            if (llave_actual != vidc2.ToString() && llave_cambio != vidc.ToString() && orden_cambioi >= orden)
                            {
                                row_change["orden"] = Convert.ToInt32(row_change["orden"]) + 1;
                            }
                        }
                        DataView dv_gpo_lib = tbl_puestos_perfil_d_gpo_lib.DefaultView;
                        dv_gpo_lib.RowFilter = "idc_perfilgpo = " + ocgpoidmenu.Value + " and borrado=0";
                        DataTable temp = dv_gpo_lib.ToTable();
                        temp.DefaultView.Sort = "orden";
                        temp = temp.DefaultView.ToTable();
                        foreach (DataRow row_change in temp.Rows)
                        {
                            int indexd = temp.Rows.IndexOf(row_change);
                            row_change["orden"] = indexd + 1;
                        }
                        foreach (DataRow row_change in tbl_puestos_perfil_d_gpo_lib.Rows)
                        {
                            foreach (DataRow row_change2 in temp.Rows)
                            {
                                if (row_change["idc_perfilgpo"].ToString() == row_change2["idc_perfilgpo"].ToString() && row_change["texto"].ToString() == row_change2["texto"].ToString())
                                {
                                    row_change["orden"] = row_change2["orden"];
                                }
                            }
                        }

                        //subir
                        Session.Add(lblsession.Text + "TablaPuestoPerfilDGpoLib", tbl_puestos_perfil_d_gpo_lib);
                        //actualiza
                        ////actualizar grids
                        filtrarGrids();
                        //AQUI SE QUEDO
                    }
                    break;

                case "editgpolibre":
                    //bajar la info y colocarla en el textbox
                    DataRow[] row = tbl_puestos_perfil_d_gpo_lib.Select(llave_d_gpo_lib.Value.ToString() + " = " + vidc);
                    txtgpolibre.Text = row[0]["texto"].ToString();
                    oc_edit_idgpolib.Value = row[0][llave_d_gpo_lib.Value.ToString()].ToString();
                    //ocultamos boton de agregar
                    btnaddgpolib.Visible = false;
                    //mostramos el de actualizar y cancelar
                    btneditgpolib.Visible = true;
                    btncanceleditgpolib.Visible = true;

                    break;

                case "deletegpolibre":
                    eliminaGpoLibre(vidc);
                    break;
            }
        }

        protected void grid_dinamico_etiqlibre_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //aqui hay otro
            GridView grid = sender as GridView;
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid.DataKeys[index].Value);
            int vidc_et = Convert.ToInt32(grid.DataKeys[index].Values["idc_perfiletiq"]);
            string texto = grid.DataKeys[index].Values["texto"].ToString();
            int VALUE_GRID = grid.Rows.Count;
            DataTable tbl_puestos_perfil_d_eti_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiLib"];
            Session[lblsession.Text + "idc_etiqueta_htmlfile"] = vidc;
            //AQUI
            switch (e.CommandName)
            {
                case "view_file":
                    grid.DataSource = filtragriddinamico(vidc_et);
                    grid.DataBind();
                    DataTable archivos_etiquetas = (DataTable)Session[lblsession.Text + "archivos_etiquetas"];
                    string path = "";
                    foreach (DataRow rows in archivos_etiquetas.Rows)
                    {
                        if (rows["etiqueta"].ToString() == texto || Convert.ToInt32(rows["idc"]) == vidc)
                        {
                            path = @rows["ruta"].ToString();
                        }
                    }
                    @path = funciones.ConvertStringToHex(@path);
                    String url = HttpContext.Current.Request.Url.AbsoluteUri;
                    String path_actual = url.Substring(url.LastIndexOf("/") + 1);
                    url = url.Replace(path_actual, "");
                    url = url + "view_files.aspx?file=" + @path;
                    DataView dv_etiquetae = archivos_etiquetas.DefaultView;
                    dv_etiquetae.RowFilter = "etiqueta='" + texto + "' or idc =" + vidc + "";
                    int t = dv_etiquetae.ToTable().Rows.Count;
                    if (t == 0)
                    {
                        Alert.ShowAlertError("No hay Archivo Relacionado.", this);
                    }
                    if (t > 0)
                    {
                        string ruta = dv_etiquetae.ToTable().Rows[0]["ruta"].ToString();
                        if (!File.Exists(ruta))
                        {
                            Alert.ShowAlertError("No hay Archivo Relacionado.", this);
                            grid.DataSource = filtragriddinamico(vidc_et);
                            grid.DataBind();
                        }
                        else
                        {
                            grid.DataSource = filtragriddinamico(vidc_et);
                            grid.DataBind();
                            ScriptManager.RegisterStartupScript(this, GetType(), "noti5wdwdwdwd33W3", "window.open('" + url + "');", true);
                        }
                    }

                    break;

                case "add_file":
                    Session["etiqueta"] = grid.DataKeys[index].Values["texto"].ToString();
                    Session["Caso_Confirmacion"] = "Archivo";
                    DataTable archivos_etiqueta = (DataTable)Session[lblsession.Text + "archivos_etiquetas"];
                    gridPapeleriaEtiquetas.DataSource = archivos_etiqueta;
                    gridPapeleriaEtiquetas.DataBind();
                    Session["texto_modal"] = texto;
                    grid.DataSource = filtragriddinamico(vidc_et);
                    grid.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessagearchivo", "ModalArchi('" + texto + "');", true);

                    break;

                case "up":
                    //SACAMOS EL INDEX ANTERIOR
                    int index3 = Convert.ToInt32(e.CommandArgument) - 1;
                    if (index3 < 0) { }//SI ES MENOR QUE 0 ENTCS NO HACEMOS NADA
                    else
                    {
                        int vidc3 = Convert.ToInt32(grid.DataKeys[index3].Values[llave_d_eti_lib.Value.ToString()]);
                        int vidcp = Convert.ToInt32(grid.DataKeys[index].Values[llave_d_eti_lib.Value.ToString()]);
                        DataRow[] row_down_change = tbl_puestos_perfil_d_eti_lib.Select(llave_d_eti_lib.Value.ToString() + " = " + vidc3);
                        int orden = Convert.ToInt32(row_down_change[0]["orden"].ToString());
                        foreach (DataRow row_change in tbl_puestos_perfil_d_eti_lib.Rows)
                        {
                            if (row_change[llave_d_eti_lib.Value.ToString()].ToString() == vidcp.ToString())
                            {
                                row_change["orden"] = orden;
                                break;
                            }
                        }
                        foreach (DataRow row_change in tbl_puestos_perfil_d_eti_lib.Rows)
                        {
                            int orden_cambioi = Convert.ToInt32(row_change["orden"].ToString());
                            if (row_change[llave_d_eti_lib.Value.ToString()].ToString() != vidcp.ToString() && orden_cambioi >= orden)
                            {
                                row_change["orden"] = Convert.ToInt32(row_change["orden"]) + 1;
                            }
                        }
                        DataView dv_etiqueta = tbl_puestos_perfil_d_eti_lib.DefaultView;
                        dv_etiqueta.RowFilter = "idc_perfiletiq=" + vidc_et + " and borrado=0";
                        DataTable temp = dv_etiqueta.ToTable();
                        temp.DefaultView.Sort = "orden";
                        temp = temp.DefaultView.ToTable();
                        foreach (DataRow row_change in temp.Rows)
                        {
                            int indexd = temp.Rows.IndexOf(row_change);
                            row_change["orden"] = indexd + 1;
                        }

                        foreach (DataRow row_change in tbl_puestos_perfil_d_eti_lib.Rows)
                        {
                            foreach (DataRow row_change2 in temp.Rows)
                            {
                                if (row_change["texto"].ToString() == row_change2["texto"].ToString())
                                {
                                    row_change["orden"] = row_change2["orden"];
                                }
                            }
                        }

                        Session.Add(lblsession.Text + "TablaPuestoPerfilDEtiLib", tbl_puestos_perfil_d_eti_lib);
                        grid.DataSource = filtragriddinamico(vidc_et);
                        grid.DataBind();
                        grid.Visible = true;
                        //AQUI SE QUEDO
                    }
                    break;

                case "down":
                    int index2 = Convert.ToInt32(e.CommandArgument) + 1;
                    if (index2 >= VALUE_GRID) { }
                    else
                    {
                        int vidc11 = Convert.ToInt32(grid.DataKeys[index].Values[llave_d_eti_lib.Value.ToString()]);
                        int vidc2 = Convert.ToInt32(grid.DataKeys[index2].Values[llave_d_eti_lib.Value.ToString()]);
                        int vidcp = Convert.ToInt32(grid.DataKeys[index].Values[llave_d_eti_lib.Value.ToString()]);
                        DataRow[] row_down_change = tbl_puestos_perfil_d_eti_lib.Select(llave_d_eti_lib.Value.ToString() + " = " + vidc2);
                        int orden = Convert.ToInt32(row_down_change[0]["orden"].ToString());
                        foreach (DataRow row_change in tbl_puestos_perfil_d_eti_lib.Rows)
                        {
                            if (row_change[llave_d_eti_lib.Value.ToString()].ToString() == vidcp.ToString())
                            {
                                row_change["orden"] = orden + 1;
                                break;
                            }
                        }
                        foreach (DataRow row_change in tbl_puestos_perfil_d_eti_lib.Rows)
                        {
                            int orden_cambioi = Convert.ToInt32(row_change["orden"].ToString());
                            string llave_actual = row_change[llave_d_eti_lib.Value.ToString()].ToString();
                            string llave_cambio = row_change[llave_d_eti_lib.Value.ToString()].ToString();
                            if (llave_actual != vidc2.ToString() && llave_cambio != vidcp.ToString() && orden_cambioi >= orden)
                            {
                                row_change["orden"] = Convert.ToInt32(row_change["orden"]) + 1;
                            }
                        }

                        DataView dv_etiqueta = tbl_puestos_perfil_d_eti_lib.DefaultView;
                        dv_etiqueta.RowFilter = "idc_perfiletiq=" + vidc_et + " and borrado=0";
                        DataTable temp = dv_etiqueta.ToTable();
                        temp.DefaultView.Sort = "orden";
                        temp = temp.DefaultView.ToTable();
                        foreach (DataRow row_change in temp.Rows)
                        {
                            int indexd = temp.Rows.IndexOf(row_change);
                            row_change["orden"] = indexd + 1;
                        }

                        foreach (DataRow row_change in tbl_puestos_perfil_d_eti_lib.Rows)
                        {
                            foreach (DataRow row_change2 in temp.Rows)
                            {
                                if (row_change["texto"].ToString() == row_change2["texto"].ToString())
                                {
                                    row_change["orden"] = row_change2["orden"];
                                }
                            }
                        }

                        Session.Add(lblsession.Text + "TablaPuestoPerfilDEtiLib", tbl_puestos_perfil_d_eti_lib);
                        //  int vidc_et = Convert.ToInt32(grid.DataKeys[index].Values["idc_perfiletiq"]);
                        grid.DataSource = filtragriddinamico(vidc_et);
                        grid.DataBind();
                        grid.Visible = true;
                    }
                    break;

                case "updateetiqlibre":
                    DataTable tdtd = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiLib"];
                    foreach (DataRow rowww in tdtd.Rows)
                    {
                        string str = rowww[llave_d_eti_lib.Value.ToString()].ToString();
                        str = str.ToString();
                    }
                    int vidic = Convert.ToInt32(grid.DataKeys[index].Values[llave_d_eti_lib.Value.ToString()]);

                    Session["idc_etiquetalibre_edit"] = vidic;
                    //bajar la info y colocarla en el textbox
                    DataRow[] row = tbl_puestos_perfil_d_eti_lib.Select(llave_d_eti_lib.Value.ToString() + " = " + vidic);
                    int idc_perfiletiq = Convert.ToInt32(row[0]["idc_perfiletiq"]);
                    //recorremos el repeater
                    foreach (RepeaterItem item in this.repite_etiqueta_controles.Items)
                    {
                        if (item.ItemType != ListItemType.Item || item.ItemType != ListItemType.AlternatingItem)
                        {
                            HiddenField oc_idcperfiletiq = (HiddenField)item.FindControl("oc_idc_perfiletiqlibre");
                            if (Convert.ToInt32(oc_idcperfiletiq.Value) == idc_perfiletiq)
                            {
                                //sabemos que de esta etiqueta hay que modificar
                                TextBox txt = (TextBox)item.FindControl("txtetiquetalibre");
                                txt.Text = row[0]["texto"].ToString();
                                //escondo el btn agregar
                                ImageButton btnadd = (ImageButton)item.FindControl("btnadd_etiq_lib");
                                btnadd.Visible = false;
                                //muestro los botones de editar y regresar
                                ImageButton btnedit = (ImageButton)item.FindControl("btnedit_etiq_lib");
                                btnedit.Visible = true;
                                ImageButton btncancel = (ImageButton)item.FindControl("btncancel_etiq_lib");
                                btncancel.Visible = true;
                                //metemos el id del registro a editar
                                HiddenField oc_edit_idcperfiletiq_opc_dato_lib = (HiddenField)item.FindControl("oc_edit_idcperfiletiq_opc_dato_lib");
                                oc_edit_idcperfiletiq_opc_dato_lib.Value = vidc.ToString();
                            }
                        }
                    }

                    break;

                case "deleteetiqlibre":
                    DataTable tbl_puestos_perfil_d_eti_lib2 = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiLib"];
                    foreach (DataRow row2 in tbl_puestos_perfil_d_eti_lib2.Rows)
                    {
                        string value = row2["texto"].ToString();
                        if (value == texto)
                        {
                            row2["borrado"] = 1;
                        }
                    }
                    DataTable papeleria = (DataTable)Session[lblsession.Text + "archivos_etiquetas"];
                    int PAP = papeleria.Rows.Count;
                    List<DataRow> rowsToDelete = new List<DataRow>();
                    foreach (DataRow row3 in papeleria.Rows)
                    {
                        if (row3["etiqueta"].ToString().Equals(texto) || Convert.ToInt32(row3["idc"]) == vidc)
                        {
                            row3.Delete();
                            break;
                        }
                    }
                    //subir
                    Session[lblsession.Text + "archivos_etiquetas"] = papeleria;
                    gridPapeleriaEtiquetas.DataSource = papeleria;
                    gridPapeleriaEtiquetas.DataBind();
                    Session.Add(lblsession.Text + "TablaPuestoPerfilDEtiLib", tbl_puestos_perfil_d_eti_lib2);
                    grid.DataSource = filtragriddinamico(vidc_et);
                    grid.DataBind();
                    grid.Visible = true;
                    break;
            }
        }

        protected void eliminaGpoLibre(int id)
        {
            DataTable tbl_puestos_perfil_d_gpo_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoLib"];
            DataRow[] row = tbl_puestos_perfil_d_gpo_lib.Select(llave_d_gpo_lib.Value.ToString() + " = " + id);

            row[0]["borrado"] = 1;
            //subir
            Session.Add(lblsession.Text + "TablaPuestoPerfilDGpoLib", tbl_puestos_perfil_d_gpo_lib);
            //actualiza
            ////actualizar grids
            filtrarGrids();
        }

        /// <summary>
        /// este metodo recupera el valor a editar y lo coloca en la caja de texto.
        /// </summary>
        /// <param name="id"></param>
        protected void editarGpoLib(int id)
        {
            try
            {
                DataTable tbl_puestos_perfil_d_gpo_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoLib"];
                DataRow[] row = tbl_puestos_perfil_d_gpo_lib.Select(llave_d_gpo_lib.Value.ToString() + " = " + id);
                txtgpolibre.Text = row[0]["texto"].ToString();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// revisa en las opciones de grupo si existe el elemento
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected bool existe_ItemGpoOpc(int item)
        {
            //
            //REPETIDO
            DataTable tbl_puestos_perfil_d_gpo_opc = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoOpc"];
            DataView dv_gpo_opc = tbl_puestos_perfil_d_gpo_opc.DefaultView;
            dv_gpo_opc.RowFilter = "idc_perfilgpod = " + item;
            int tot = dv_gpo_opc.Count;
            if (tot > 0)
            {
                return true;
            }
            //FIN REPETIDO
            return false;
        }

        protected bool existe_ItemEtiqOpc(int item)
        {
            DataTable tbl_puestos_perfil_d_eti_opc = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiOpc"];
            DataView dv_etiq_opc = tbl_puestos_perfil_d_eti_opc.DefaultView;
            dv_etiq_opc.RowFilter = "idc_perfiletiq_opc = " + item;
            int tot = dv_etiq_opc.Count;
            if (tot > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// metodo para generar las cadenas de contactos, telefonos y correos
        /// </summary>
        /// <param name=tabla>segun que cadena formaremos</param>
        /// <returns>la cadena formada id;desc</returns>
        protected string[] GeneraCadena(string tabla)
        {
            Boolean borrado, nuevo;
            string cadena = "";
            int contador = 0;
            string[] resultado = new string[2];
            switch (tabla)
            {
                case "TablaPuestoPerfilDGpoLib":
                    DataTable tbl_puestos_perfil_d_gpo_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoLib"];
                    DataView dv_gpo_lib = tbl_puestos_perfil_d_gpo_lib.DefaultView;
                    //dv_gpo_lib.RowFilter = "idc_perfilgpo = " + DropGpos.SelectedValue;
                    dv_gpo_lib.RowFilter = "borrado=1 or borrado=0";
                    DataTable tabletemp = new DataTable();
                    tabletemp = dv_gpo_lib.ToTable();
                    tabletemp.DefaultView.Sort = "orden";
                    tabletemp = tabletemp.DefaultView.ToTable();
                    int totbug = tabletemp.Rows.Count;
                    foreach (DataRow fila in tabletemp.Rows)
                    {
                        //revisar que tipo de registro es, si es nuevo o no
                        borrado = Convert.ToBoolean(fila["borrado"]);
                        nuevo = Convert.ToBoolean(fila["nuevo"]);
                        //AGREGAR
                        if (borrado == false & nuevo == true)
                        {
                            cadena = cadena + "0" + ";" + fila["idc_perfilgpo"] + ";" + fila["texto"].ToString().Replace(";", ",") + ";" + "0" + ";";
                            contador = contador + 1;
                        }
                        //ELIMINAR
                        else if (borrado == true & nuevo == false)
                        {
                            cadena = cadena + fila[llave_d_gpo_lib.Value.ToString()] + ";" + fila["idc_perfilgpo"] + ";" + fila["texto"].ToString().Replace(";", ",") + ";" + "1" + ";";
                            contador = contador + 1;
                        }
                        //ACTUALIZAR
                        else
                        {
                            cadena = cadena + fila[llave_d_gpo_lib.Value.ToString()] + ";" + fila["idc_perfilgpo"] + ";" + fila["texto"].ToString().Replace(";", ",") + ";" + "0" + ";";
                            contador = contador + 1;
                        }
                    }
                    break;

                case "TablaPuestoPerfilDGpoOpc":
                    DataTable tbl_puestos_perfil_d_gpo_opc = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoOpc"];

                    foreach (DataRow fila in tbl_puestos_perfil_d_gpo_opc.Rows)
                    {
                        //revisar que tipo de registro es, si es nuevo o no
                        borrado = Convert.ToBoolean(fila["borrado"]);
                        nuevo = Convert.ToBoolean(fila["nuevo"]);
                        //AGREGAR
                        if (borrado == false & nuevo == true)
                        {
                            cadena = cadena + "0" + ";" + fila["idc_perfilgpod"] + ";" + "0" + ";";
                            contador = contador + 1;
                        }
                        //ELIMINAR
                        else if (borrado == true & nuevo == false)
                        {
                            cadena = cadena + fila[llave_d_gpo_opc.Value.ToString()] + ";" + fila["idc_perfilgpod"] + ";" + "1" + ";";
                            contador = contador + 1;
                        }
                        //ACTUALIZAR
                        else if (borrado == false & nuevo == false)
                        {
                            cadena = cadena + fila[llave_d_gpo_opc.Value.ToString()] + ";" + fila["idc_perfilgpod"] + ";" + "0" + ";";
                            contador = contador + 1;
                        }
                    }

                    break;

                case "TablaPuestoPerfilDEtiLib":
                    DataTable tbl_puestos_perfil_d_eti_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiLib"];
                    DataView dv_eti_lib = tbl_puestos_perfil_d_eti_lib.DefaultView;
                    //dv_gpo_lib.RowFilter = "idc_perfilgpo = " + DropGpos.SelectedValue;
                    dv_eti_lib.RowFilter = "borrado=1 or borrado=0";
                    DataTable tabletemp2 = new DataTable();
                    tabletemp2 = dv_eti_lib.ToTable();
                    tabletemp2.DefaultView.Sort = "orden";
                    tabletemp2 = tabletemp2.DefaultView.ToTable();
                    foreach (DataRow fila in tabletemp2.Rows)
                    {
                        //revisar que tipo de registro es, si es nuevo o no
                        borrado = Convert.ToBoolean(fila["borrado"]);
                        nuevo = Convert.ToBoolean(fila["nuevo"]);
                        //AGREGAR
                        if (borrado == false & nuevo == true)
                        {
                            cadena = cadena + "0" + ";" + fila["idc_perfiletiq"] + ";" + fila["texto"].ToString().Replace(";", ",") + ";" + "0" + ";";
                            contador = contador + 1;
                        }
                        //ELIMINAR
                        else if (borrado == true & nuevo == false)
                        {
                            cadena = cadena + fila[llave_d_eti_lib.Value.ToString()] + ";" + fila["idc_perfiletiq"] + ";" + fila["texto"].ToString().Replace(";", ",") + ";" + "1" + ";";
                            contador = contador + 1;
                        }
                        //ACTUALIZAR
                        else
                        {
                            cadena = cadena + fila[llave_d_eti_lib.Value.ToString()] + ";" + fila["idc_perfiletiq"] + ";" + fila["texto"].ToString().Replace(";", ",") + ";" + "0" + ";";
                            contador = contador + 1;
                        }
                    }
                    break;

                case "TablaPuestoPerfilDEtiOpc":
                    DataTable tbl_puestos_perfil_d_eti_opc = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiOpc"];
                    foreach (DataRow fila in tbl_puestos_perfil_d_eti_opc.Rows)
                    {
                        //revisar que tipo de registro es, si es nuevo o no
                        borrado = Convert.ToBoolean(fila["borrado"]);
                        nuevo = Convert.ToBoolean(fila["nuevo"]);
                        //AGREGAR
                        if (borrado == false & nuevo == true)
                        {
                            cadena = cadena + "0" + ";" + fila["idc_perfiletiq_opc"] + ";" + "0" + ";";
                            contador = contador + 1;
                        }
                        //ELIMINAR
                        else if (borrado == true & nuevo == false)
                        {
                            cadena = cadena + fila[llave_d_eti_opc.Value.ToString()] + ";" + fila["idc_perfiletiq_opc"] + ";" + "1" + ";";
                            contador = contador + 1;
                        }
                        //ACTUALIZAR
                        else if (borrado == false & nuevo == false)
                        {
                            cadena = cadena + fila[llave_d_eti_opc.Value.ToString()] + ";" + fila["idc_perfiletiq_opc"] + ";" + "0" + ";";
                            contador = contador + 1;
                        }
                    }
                    break;
            }
            resultado[0] = cadena;
            resultado[1] = contador.ToString();
            return resultado;
        }

        protected void btnGuardarTodo_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea guardar el perfil " + txtnomperfil.Text + "?');", true);
        }

        protected void btnCancelarTodo_Click(object sender, EventArgs e)
        {
            //Response.Redirect("perfiles.aspx?sborrador="+interruptor());
            Response.Redirect(oc_paginaprevia.Value);
        }

        protected void repite_menu_grupos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int idgpo = 0;
            if (Session["id_puestogpo_detalle"] != null)
            {
                idgpo = Convert.ToInt32(Session["id_puestogpo_detalle"]);
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView dbr = (DataRowView)e.Item.DataItem;
                //boton
                Button btn = (Button)e.Item.FindControl("btnmenugpo");
                if (statusGpo(Convert.ToInt32(DataBinder.Eval(dbr, "idc_perfilgpo"))))
                {
                    //colorea
                    btn.CssClass = "btn btn-info";
                }
                else
                {
                    btn.CssClass = "btn btn-default";
                }

                if (idgpo == Convert.ToInt32(DataBinder.Eval(dbr, "idc_perfilgpo")))
                {
                    inicio.Visible = false;
                    btn.CssClass = "btn btn-warning";
                }

                btn.Text = Convert.ToString(DataBinder.Eval(dbr, "grupo"));
                btn.CommandName = Convert.ToString(DataBinder.Eval(dbr, "idc_perfilgpo"));
            }
        }

        protected void repite_menu_grupos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //guardar los cambios en los checkboxlist antes de cargar otro grupo
            if (Page.IsPostBack)
            {
                guardaCambiosCheckboxlist();
            }
            inicio.Visible = false;
            limpiarControles();
            int idc_perfilgpo = Convert.ToInt32(e.CommandName);
            //lo guardamos en el hidden
            ocgpoidmenu.Value = idc_perfilgpo.ToString();
            cargar_formulario(idc_perfilgpo);

            //filtrar los grids
            filtrarGrids();
        }

        protected void btnaddgpolib_Click(object sender, ImageClickEventArgs e)
        {
            //AQUI
            //bajamos las tablas de session
            DataTable tbl_puestos_perfil_d_gpo_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoLib"];
            //consecutivos aqui
            DataRow row_gpo_lib; //= tbl_puestos_perfil_d_eti_lib.NewRow();
            int id_gpo_lib = consecutivo(lblsession.Text + "TablaPuestoPerfilDGpoLib", llave_d_gpo_lib.Value.ToString());
            int orden = consecutivo(lblsession.Text + "TablaPuestoPerfilDGpoLib", "orden");
            //recuperar los valores
            //string valorlib = funciones.CleanInput(txtgpolibre.Text);
            string valorlib = txtgpolibre.Text;
            int total = tbl_puestos_perfil_d_gpo_lib.Rows.Count;
            int maximo = Convert.ToInt32(lblmax.Text);
            if (maximo != 0 && total >= maximo)
            {
                Alert.ShowAlertError("Solo puede ingresar " + maximo.ToString() + " opciones.", this);
            }
            else
            {
                if (valorlib != "")
                { //guarda
                    if (!duplicado(lblsession.Text + "TablaPuestoPerfilDGpoLib", "texto", valorlib))
                    {
                        row_gpo_lib = tbl_puestos_perfil_d_gpo_lib.NewRow();
                        row_gpo_lib[llave_d_gpo_lib.Value.ToString()] = id_gpo_lib;
                        row_gpo_lib[llave_puestoperfil.Value.ToString()] = 0;
                        row_gpo_lib["idc_perfilgpo"] = oc_idcperfilgpo_lib.Value;
                        row_gpo_lib["grupo"] = lblgpolibre.Text;
                        string text = txtgpolibre.Text;
                        text = text.Replace("•", "");
                        text = text.Replace(";", ",");
                        row_gpo_lib["texto"] = text;
                        row_gpo_lib["nuevo"] = 1;
                        row_gpo_lib["borrado"] = 0;
                        row_gpo_lib["orden"] = orden;
                        tbl_puestos_perfil_d_gpo_lib.Rows.Add(row_gpo_lib);
                    }
                }
            }
            //actualizar tablas
            Session.Add(lblsession.Text + "TablaPuestoPerfilDGpoLib", tbl_puestos_perfil_d_gpo_lib);

            ////actualizar grids
            filtrarGrids();
            //limpiamos la caja de texto
            txtgpolibre.Text = "";
        }

        protected void btnaddgpoopc()
        {
            DataTable tbl_puestos_perfil_d_gpo_opc = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoOpc"];
            //consecutivos
            DataRow row_gpo_opc;// = tbl_puestos_perfil_d_eti_opc.NewRow();
            int id_gpo_opc = consecutivo(lblsession.Text + "TablaPuestoPerfilDGpoOpc", llave_d_gpo_opc.Value.ToString());
            //recuperar los valores

            //opciones
            for (int i = 0; i < check_gpo_opc.Items.Count; i++)
            {
                int idc_perfilgpod = Convert.ToInt32(check_gpo_opc.Items[i].Value);
                if (check_gpo_opc.Items[i].Selected)
                {
                    //chequeado y no existe agregalo

                    if (!existe_ItemGpoOpc(idc_perfilgpod))
                    {
                        //si esta checkeado y no existe en la tabla agregalo
                        row_gpo_opc = tbl_puestos_perfil_d_gpo_opc.NewRow();
                        row_gpo_opc[llave_d_gpo_opc.Value.ToString()] = id_gpo_opc;
                        row_gpo_opc[llave_puestoperfil.Value.ToString()] = 0;
                        row_gpo_opc["opcion"] = check_gpo_opc.Items[i].Text;
                        row_gpo_opc["idc_perfilgpod"] = check_gpo_opc.Items[i].Value;
                        row_gpo_opc["grupo"] = lblgpoopc.Text;
                        //row_gpo_opc["idc_perfilgpo"] = DropGpos.SelectedValue;
                        row_gpo_opc["idc_perfilgpo"] = Convert.ToInt32(ocgpoidmenu.Value);
                        row_gpo_opc["nuevo"] = 1;
                        row_gpo_opc["borrado"] = 0;
                        tbl_puestos_perfil_d_gpo_opc.Rows.Add(row_gpo_opc);
                        id_gpo_opc = id_gpo_opc + 1;
                    }
                    else
                    {
                        //activalo de nuevo
                        DataRow[] row = tbl_puestos_perfil_d_gpo_opc.Select("idc_perfilgpod = " + idc_perfilgpod);
                        row[0]["borrado"] = 0;
                    }
                }
                else
                {
                    if (existe_ItemGpoOpc(idc_perfilgpod))
                    {
                        //actualizalo a borrado
                        DataRow[] row = tbl_puestos_perfil_d_gpo_opc.Select("idc_perfilgpod = " + idc_perfilgpod);
                        row[0]["borrado"] = 1;
                    }
                }
            }

            //actualizar tablas
            Session.Add(lblsession.Text + "TablaPuestoPerfilDGpoOpc", tbl_puestos_perfil_d_gpo_opc);
            ////actualizar grids
            ////
            DataView dv_gpo_opc = tbl_puestos_perfil_d_gpo_opc.DefaultView;
            //dv_gpo_opc.RowFilter = "idc_perfilgpo = " + DropGpos.SelectedValue;
            if (dv_gpo_opc.Count > 0)
            {
                dv_gpo_opc.RowFilter = "idc_perfilgpo = " + ocgpoidmenu.Value;
                gridgpo_opc.DataSource = dv_gpo_opc;
                gridgpo_opc.DataBind();
            }
        }

        /// <summary>
        /// funcion que revisa la tabla en session de opciones de gpo y palomea los checks correspondientes
        /// </summary>
        protected void check_opciones_gpo()
        {
            //bajar la tabla de session
            DataTable tbl_puestos_perfil_d_gpo_opc = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoOpc"];
            if (tbl_puestos_perfil_d_gpo_opc.Rows.Count > 0)
            { //si tiene registros continuamos
                foreach (DataRow fila in tbl_puestos_perfil_d_gpo_opc.Rows)
                {
                    if (fila["borrado"].ToString() == "False")
                    { //check
                        foreach (ListItem lista in check_gpo_opc.Items)
                        {
                            if (lista.Value.ToString() == fila["idc_perfilgpod"].ToString())
                            {
                                lista.Selected = true; ;
                            }
                        }
                    }
                }
            }
        }

        protected void repite_etiqueta_controles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lblmin = e.Item.FindControl("lblmin") as Label;
            Label lblmax = e.Item.FindControl("lblmax") as Label;
            int min = Convert.ToInt32(lblmin.Text);
            int max = Convert.ToInt32(lblmax.Text);
            if (e.CommandName == "add_valor_lib_etiq")
            {
                //GridView grid = (GridView)e.Item.FindControl("grid_dinamico_etiqlibre");
                //agregar valores de etiqueta en el gridview d eetiquetas
                //bajamos las tablas
                DataTable tbl_puestos_perfil_d_eti_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiLib"];
                Label etiqueta = e.Item.FindControl("lbletiquetalibre") as Label;
                //HUMBERTO VERIFICAMOS QUE SEA EL PERMITIDO
                string expression;
                expression = "etiqueta = '" + etiqueta.Text + "' and borrado=0";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = tbl_puestos_perfil_d_eti_lib.Select(expression);

                int total_rows = foundRows.Length;

                if (total_rows >= max && max != 0)
                {
                    Alert.ShowAlertError("Solo puede agregar hasta " + max.ToString() + " opcion(es)", this);
                }
                else
                {
                    DataRow row_etq_lib; //= tbl_puestos_perfil_d_eti_lib.NewRow();
                                         //consecutivos
                    int id_lib = consecutivo(lblsession.Text + "TablaPuestoPerfilDEtiLib", llave_d_eti_lib.Value.ToString());
                    int orden = consecutivo(lblsession.Text + "TablaPuestoPerfilDEtiLib", "orden");
                    HiddenField ocidc_perfiletiq = e.Item.FindControl("oc_idc_perfiletiqlibre") as HiddenField;
                    TextBox txtlibreetiq = e.Item.FindControl("txtetiquetalibre") as TextBox;
                    //DropDownList cboxopcetiq = item.FindControl("cboxetiquetaopc") as DropDownList;

                    if (txtlibreetiq.Text != "")
                    { //guarda
                      //EVITAR DUPLICADOS
                      //subir a datatable
                        if (!duplicado(lblsession.Text + "TablaPuestoPerfilDEtiLib", "texto", txtlibreetiq.Text))
                        {
                            string vllave_d_eti_lib = llave_d_eti_lib.Value.ToString();
                            string vllave_puestoperfil = llave_puestoperfil.Value.ToString();
                            row_etq_lib = tbl_puestos_perfil_d_eti_lib.NewRow();
                            row_etq_lib[vllave_d_eti_lib] = id_lib;
                            row_etq_lib[vllave_puestoperfil] = 0;
                            row_etq_lib["idc_perfiletiq"] = Convert.ToInt32(ocidc_perfiletiq.Value);
                            row_etq_lib["etiqueta"] = etiqueta.Text;
                            string text = txtlibreetiq.Text;
                            text = text.Replace("•", "");
                            text = text.Replace(";", ",");
                            row_etq_lib["texto"] = text;
                            //row_etq_lib["idc_perfilgpo"] = DropGpos.SelectedValue;
                            row_etq_lib["idc_perfilgpo"] = Convert.ToInt32(ocgpoidmenu.Value);
                            row_etq_lib["nuevo"] = 1;
                            row_etq_lib["borrado"] = 0;
                            row_etq_lib["orden"] = orden;
                            tbl_puestos_perfil_d_eti_lib.Rows.Add(row_etq_lib);

                            //darle el foco
                        }
                    }
                    //LIMPIAMOS
                    txtlibreetiq.Text = "";
                    //ACTUALIZAR LAS TABLAS DE SESSION
                    Session.Add(lblsession.Text + "TablaPuestoPerfilDEtiLib", tbl_puestos_perfil_d_eti_lib);
                    //revisamos en session si hay items de esta etiqueta
                    int idc_perfiletiq = Convert.ToInt32(ocidc_perfiletiq.Value);
                    if (tbl_puestos_perfil_d_eti_lib.Rows.Count > 0)
                    {
                        DataView dt_libre = tbl_puestos_perfil_d_eti_lib.DefaultView;
                        dt_libre.RowFilter = "idc_perfiletiq = " + idc_perfiletiq + " and borrado=0";
                        if (dt_libre.Count > 0)
                        {
                            //genera el grid
                            GridView grid_etiq_lib = (GridView)e.Item.FindControl("grid_dinamico_etiqlibre");
                            grid_etiq_lib.DataKeyNames = new string[] { llave_d_eti_lib.Value.ToString(), "idc_perfiletiq", "texto", "orden" };
                            grid_etiq_lib.DataSource = filtragriddinamico(idc_perfiletiq);
                            grid_etiq_lib.DataBind();
                        }
                    }
                }
            }

            if (e.CommandName == "edit_valor_lib_etiq")
            {
                //recuperamos los valores el id y el texto
                HiddenField oc_idc_perfiletiq_opc_lib = (HiddenField)e.Item.FindControl("oc_edit_idcperfiletiq_opc_dato_lib");
                int id = Convert.ToInt32(oc_idc_perfiletiq_opc_lib.Value);
                TextBox txttexto = (TextBox)e.Item.FindControl("txtetiquetalibre");
                //que no esten vacios
                if (string.IsNullOrEmpty(txttexto.Text) || id == 0)
                {
                    //cancelar operacion
                    Alert.ShowAlert("No puede haber campos vacíos", "Alerta", this.Page);
                    return;
                }
                HiddenField ocidc_perfiletiq = e.Item.FindControl("oc_idc_perfiletiqlibre") as HiddenField;
                int idc_perfiletiq = Convert.ToInt32(ocidc_perfiletiq.Value);
                if (!duplicadoEdicion(lblsession.Text + "TablaPuestoPerfilDEtiLib", "texto", txttexto.Text, "and idc_perfiletiq =" + idc_perfiletiq + " and " + llave_d_eti_lib.Value.ToString() + " <>" + id))
                {
                    //proseguimos aqui se quedo chin
                    DataTable tbl_puestos_perfil_d_eti_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiLib"];
                    //
                    int NEW_VALUE = Convert.ToInt32(Session["idc_etiquetalibre_edit"]);
                    DataRow[] row_etq_lib = tbl_puestos_perfil_d_eti_lib.Select(llave_d_eti_lib.Value.ToString() + " = " + NEW_VALUE);
                    row_etq_lib[0]["texto"] = txttexto.Text;

                    //ACTUALIZAR LAS TABLAS DE SESSION
                    Session.Add(lblsession.Text + "TablaPuestoPerfilDEtiLib", tbl_puestos_perfil_d_eti_lib);
                    //revisamos en session si hay items de esta etiqueta

                    if (tbl_puestos_perfil_d_eti_lib.Rows.Count > 0)
                    {
                        DataView dt_libre = tbl_puestos_perfil_d_eti_lib.DefaultView;
                        dt_libre.RowFilter = "idc_perfiletiq = " + idc_perfiletiq + " and borrado=0";
                        if (dt_libre.Count > 0)
                        {
                            //genera el grid
                            GridView grid_etiq_lib = (GridView)e.Item.FindControl("grid_dinamico_etiqlibre");
                            grid_etiq_lib.DataSource = filtragriddinamico(idc_perfiletiq);
                            grid_etiq_lib.DataBind();
                        }
                    }
                    //ocultamos botones de actualizar y borrar
                    ImageButton btnedit = (ImageButton)e.Item.FindControl("btnedit_etiq_lib");
                    btnedit.Visible = false;
                    ImageButton btncancel = (ImageButton)e.Item.FindControl("btncancel_etiq_lib");
                    btncancel.Visible = false;
                    //mostramos el boton de agregar
                    ImageButton btnadd = (ImageButton)e.Item.FindControl("btnadd_etiq_lib");
                    btnadd.Visible = true;
                    //limpiamos
                    oc_idc_perfiletiq_opc_lib.Value = "0";
                    txttexto.Text = "";
                    Session["idc_etiquetalibre_edit"] = null;
                    AlertIcon("Actualización correcta", "Bien");
                }
                else
                {
                    AlertError("Ya existe un registro con esa información.");
                }
            }

            if (e.CommandName == "cancel_valor_lib_etiq")
            {
                //ocultamos botones de actualizar y borrar
                ImageButton btnedit = (ImageButton)e.Item.FindControl("btnedit_etiq_lib");
                btnedit.Visible = false;
                ImageButton btncancel = (ImageButton)e.Item.FindControl("btncancel_etiq_lib");
                btncancel.Visible = false;
                //mostramos el boton de agregar
                ImageButton btnadd = (ImageButton)e.Item.FindControl("btnadd_etiq_lib");
                btnadd.Visible = true;
                //limpiamos
                HiddenField oc_idc_perfiletiq_opc_lib = (HiddenField)e.Item.FindControl("oc_edit_idcperfiletiq_opc_dato_lib");
                oc_idc_perfiletiq_opc_lib.Value = "0";
                TextBox txttexto = (TextBox)e.Item.FindControl("txtetiquetalibre");
                txttexto.Text = "";
                //le damos el foco
            }
        }

        protected string mensaje_validacion(int min, int max)
        {
            //
            string mensaje = "Error en el mensaje de validación.";
            if (min == 0 & max == 0)
            {
                mensaje = "Puedes agregar las opciones que quieras";
            }
            else if (min == 0 & max > 0)
            {
                mensaje = "Puedes agregar hasta máximo " + max + "opcion(es)";
            }
            else if (min < max)
            {
                mensaje = "Debes agregar un mínimo de " + min + " y un máximo de " + max + " opcion(es).";
            }
            else if (min > max)
            {
                mensaje = "Debes agregar como mínimo " + min + " opcion(es)";
            }
            else if (min == max)
            {
                mensaje = "Solo puedes agregar " + min + " opcion(es)";
            }
            return mensaje;
        }

        protected void guardaCambiosCheckboxlist()
        {
            //opciones de gpos
            btnaddgpoopc();
            //opciones de etiquetas
            btnaddetiqopc();
            recorreMenu();
        }

        protected void btnaddetiqopc()
        {
            //agregar valores de etiqueta en el gridview d eetiquetas
            //bajamos las tablas
            DataTable tbl_puestos_perfil_d_eti_opc = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiOpc"];
            DataRow row_etq_opc;// = tbl_puestos_perfil_d_eti_opc.NewRow();
            //consecutivos
            int id_opc = consecutivo(lblsession.Text + "TablaPuestoPerfilDEtiOpc", llave_d_eti_opc.Value.ToString());
            foreach (RepeaterItem item in this.repite_etiqueta_controles.Items)
            {
                if (item.ItemType != ListItemType.Item || item.ItemType != ListItemType.AlternatingItem)
                {
                    //DropDownList cboxopcetiq = item.FindControl("cboxetiquetaopc") as DropDownList;
                    CheckBoxList check_opcetiq = item.FindControl("check_etiqopc") as CheckBoxList;
                    Label etiqueta_opc = item.FindControl("lbletiquetaopcion") as Label;

                    //subir a datatable
                    //EVITAR DUPLICADOS
                    for (int i = 0; i < check_opcetiq.Items.Count; i++)
                    {
                        int idc_perfiletiq_opc = Convert.ToInt32(check_opcetiq.Items[i].Value);
                        if (check_opcetiq.Items[i].Selected)
                        {
                            //chequeado y no existe agregalo

                            if (!existe_ItemEtiqOpc(idc_perfiletiq_opc))
                            {
                                row_etq_opc = tbl_puestos_perfil_d_eti_opc.NewRow();
                                row_etq_opc[llave_d_eti_opc.Value.ToString()] = id_opc;
                                row_etq_opc[llave_puestoperfil.Value.ToString()] = 0;
                                row_etq_opc["etiqueta"] = etiqueta_opc.Text;
                                row_etq_opc["opcion"] = check_opcetiq.Items[i].Text;
                                row_etq_opc["idc_perfiletiq_opc"] = Convert.ToInt32(check_opcetiq.Items[i].Value);
                                //row_etq_opc["idc_perfilgpo"] = DropGpos.SelectedValue;
                                row_etq_opc["idc_perfilgpo"] = Convert.ToInt32(ocgpoidmenu.Value);
                                row_etq_opc["nuevo"] = 1;
                                row_etq_opc["borrado"] = 0;
                                tbl_puestos_perfil_d_eti_opc.Rows.Add(row_etq_opc);
                                id_opc = id_opc + 1;
                            }
                            else
                            {
                                //existe y se vuelve a habilitar
                                DataRow[] fila = tbl_puestos_perfil_d_eti_opc.Select("idc_perfiletiq_opc = " + idc_perfiletiq_opc);
                                fila[0]["borrado"] = 0;
                            }
                        }
                        else
                        {
                            //no esta check pero esta agregado ponle status borrado
                            if (existe_ItemEtiqOpc(idc_perfiletiq_opc))
                            {
                                DataRow[] fila = tbl_puestos_perfil_d_eti_opc.Select("idc_perfiletiq_opc = " + idc_perfiletiq_opc);

                                fila[0]["borrado"] = 1;
                            }
                        }
                    }
                }
            }

            //ACTUALIZAR LAS TABLAS DE SESSION
            Session.Add(lblsession.Text + "TablaPuestoPerfilDEtiOpc", tbl_puestos_perfil_d_eti_opc);
        }

        /// <summary>
        /// revisa las tablas de session en base al id del gpo para ver si tiene algun dato capturado y poder colorear el grpo en el menu
        /// </summary>
        protected bool statusGpo(int idc_perfilgpo)
        {
            bool datos = false;
            //Convert.ToInt32(ocgpoidmenu.Value);
            //bajar las tablas
            DataTable tbl_puestos_perfil_d_gpo_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoLib"];
            //filtra
            DataView dv_gpo_lib = tbl_puestos_perfil_d_gpo_lib.DefaultView;
            dv_gpo_lib.RowFilter = "idc_perfilgpo = " + idc_perfilgpo + " and borrado=0";
            if (dv_gpo_lib.Count > 0)
            {
                //hay datos
                datos = true;
            }
            //
            DataTable tbl_puestos_perfil_d_gpo_opc = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoOpc"];
            //filtra
            DataView dv_gpo_opc = tbl_puestos_perfil_d_gpo_opc.DefaultView;
            dv_gpo_opc.RowFilter = "idc_perfilgpo = " + idc_perfilgpo + " and borrado=0";
            if (dv_gpo_opc.Count > 0)
            {
                //hay datos
                datos = true;
            }
            //
            DataTable tbl_puestos_perfil_d_eti_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiLib"];
            //filtra
            DataView dv_etiq_lib = tbl_puestos_perfil_d_eti_lib.DefaultView;
            dv_etiq_lib.RowFilter = "idc_perfilgpo =" + idc_perfilgpo + " and borrado=0";
            if (dv_etiq_lib.Count > 0)
            {
                //hay datos
                datos = true;
            }
            //
            DataTable tbl_puestos_perfil_d_eti_opc = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiOpc"];
            //filtra
            DataView dv_etiq_opc = tbl_puestos_perfil_d_eti_opc.DefaultView;
            dv_etiq_opc.RowFilter = "idc_perfilgpo =" + idc_perfilgpo + " and borrado=0";
            if (dv_etiq_opc.Count > 0)
            {
                //hay datos
                datos = true;
            }

            return datos;
        }

        /// <summary>
        /// metodo que revisa si se capturaron datos en cierto grupo y se pone en verde o si ya no capturo nada se pone en blanco (por default)
        /// </summary>
        protected void recorreMenu()
        {
            foreach (RepeaterItem item in this.repite_menu_grupos.Items)
            {
                if (item.ItemType != ListItemType.Item || item.ItemType != ListItemType.AlternatingItem)
                {
                    HiddenField gpoid = (HiddenField)item.FindControl("oc_gpoid");
                    int id_gpo = Convert.ToInt32(gpoid.Value);
                    Button btn = (Button)item.FindControl("btnmenugpo");
                    if (statusGpo(id_gpo))
                    {
                        btn.CssClass = "btn btn-info";
                    }
                    else
                    {
                        btn.CssClass = "btn btn-default";
                    }
                }
            }
        }

        protected int eliminaEtiqLibre(int id)
        {
            DataTable tbl_puestos_perfil_d_eti_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiLib"];
            foreach (DataRow row in tbl_puestos_perfil_d_eti_lib.Rows)
            {
                int value = Convert.ToInt32(row["idc_perfiletiq"]);
                if (value == id)
                {
                    row["borrado"] = 1;
                }
            }

            //subir
            Session.Add(lblsession.Text + "TablaPuestoPerfilDEtiLib", tbl_puestos_perfil_d_eti_lib);
            //actualiza
            ////actualizar grids

            return id;
        }

        protected DataTable filtragriddinamico(int id)
        {
            DataTable tbl_puestos_perfil_d_eti_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiLib"];
            DataView dt_libre;
            dt_libre = tbl_puestos_perfil_d_eti_lib.DefaultView;
            dt_libre.RowFilter = "idc_perfiletiq = " + id + " and borrado=0";
            DataTable tabletemp = new DataTable();
            tabletemp = dt_libre.ToTable();
            tabletemp.DefaultView.Sort = "orden";
            tabletemp = tabletemp.DefaultView.ToTable();
            int t = dt_libre.Count;
            return tabletemp;
        }

        protected void btncanceleditgpolib_Click(object sender, ImageClickEventArgs e)
        {
            limpiaGpoLibre();
        }

        protected void limpiaGpoLibre()
        {
            //cancelamos la operacion de actualizar un grupo libre
            //borramos datos del campo oculto y caja de texto
            oc_edit_idgpolib.Value = "0";
            txtgpolibre.Text = "";
            //mostramos el boton agregar
            btnaddgpolib.Visible = true;
            //ocultamos actualizar y cancelar
            btneditgpolib.Visible = false;
            btncanceleditgpolib.Visible = false;
        }

        protected void btneditgpolib_Click(object sender, ImageClickEventArgs e)
        {
            //recuperamos los valores
            string texto = txtgpolibre.Text;
            int id = Convert.ToInt32(oc_edit_idgpolib.Value);
            //revisar que no este vacio los campos
            if (string.IsNullOrEmpty(texto) || id == 0)
            {
                //cancelar operacion
                Alert.ShowAlert("No puede haber campos vacíos", "Alerta", this.Page);
                return;
            }
            if (!duplicadoEdicion(lblsession.Text + "TablaPuestoPerfilDGpoLib", "texto", texto, "and " + llave_d_gpo_lib.Value.ToString() + " <>" + id))
            {
                DataTable tbl_puestos_perfil_d_gpo_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoLib"];
                DataRow[] row = tbl_puestos_perfil_d_gpo_lib.Select(llave_d_gpo_lib.Value.ToString() + " = " + id);

                row[0]["texto"] = texto;
                //subir
                Session.Add(lblsession.Text + "TablaPuestoPerfilDGpoLib", tbl_puestos_perfil_d_gpo_lib);
                //actualiza
                ////actualizar grids
                filtrarGrids();
                //limpiamos
                limpiaGpoLibre();
                AlertIcon("Actualización correcta", "Bien");
            }
            else
            {
                limpiaGpoLibre();
                AlertError("Ya existe un registro con esa información.");
            }
        }

        //Funcion que ejecuta alerta tipo Información. Se hereda de SweetAlert JS y se ejecuta desde el servidor con ScriptManager
        // @Mensaje: Type String.  Cuerpo del Mensaje
        // @Titulo:  Tyoe String.  Titulo del Mensaje
        // @URL:     Type String.  URL del icono
        public void AlertIcon(String Mensaje, String Titulo)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "swal('" + Titulo + "', '" + Mensaje + "', 'success')", true);
        }

        //Funcion que ejecuta alerta tipo Error. Se hereda de SweetAlert JS y se ejecuta desde el servidor con ScriptManager
        // @Mensaje: Type String.  Cuerpo del Mensaje
        public void AlertError(String Mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                "sweetAlert({    title: 'Oops!', text: '" + Mensaje + "', type: 'error'});", true);
        }

        protected bool validar_campos()
        {
            string nom_perfil = txtnomperfil.Text;
            nom_perfil = nom_perfil.ToLower();
            int idc_puesto_perfil = Convert.ToInt32(oc_idc_puestoperfil.Value);
            if (string.IsNullOrEmpty(nom_perfil))
            {
                Alert.ShowAlertError("Ingrese el nombre del Perfil", this.Page);
                return false;
            }
            //si no esta vacio revisar que no se repita
            //entidad
            PerfilesE entidad = new PerfilesE();
            entidad.Nombre = nom_perfil;
            if (check_borr_prod.Checked)
            { //borrador
                entidad.Idc_perfil = 0;
                entidad.Idc_puestoperfil_borr = idc_puesto_perfil;
                entidad.Borrador = true;
            }
            else
            { //produccion
                entidad.Idc_perfil = idc_puesto_perfil;
                entidad.Idc_puestoperfil_borr = 0;
                entidad.Borrador = false;
            }

            //componente
            try
            {
                PerfilesBL componente = new PerfilesBL();
                DataSet ds = new DataSet();
                ds = componente.perfil_validar(entidad);

                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    //todo bien
                    return true;
                }
                else
                {
                    //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                    Alert.ShowAlertError(vmensaje, this.Page);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
                return false;
            }
        }

        private int interruptor()
        {
            if (check_borr_prod.Checked)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// metodo que regresa el id y la descripcion de los documentos de la tabla tipo_documentos_pre_empleados
        /// y llena el control checklist_docs create 02-12-2015
        /// </summary>
        protected void cargar_documentos_lista()
        {
            DataSet ds = new DataSet();
            PerfilesBL componente = new PerfilesBL();
            ds = componente.tipo_documentos_pre_empleados_lista();
            //llenamos el checklist
            checklist_docs.DataTextField = "descripcion";
            checklist_docs.DataValueField = "idc_tipodoc";

            checklist_docs.DataSource = ds.Tables[0];
            checklist_docs.DataBind();
        }

        private void carga_examenes()
        {
            DataSet ds = new DataSet();
            PerfilesBL componente = new PerfilesBL();
            PerfilesE entidad = new PerfilesE();
            entidad.Ptipo = "R";
            ds = componente.tipo_examenes(entidad);
            //llenamos el checklist
            chxExamenes.DataTextField = "descripcion";
            chxExamenes.DataValueField = "idc_examen";

            chxExamenes.DataSource = ds.Tables[0];
            chxExamenes.DataBind();
        }

        /// <summary>
        /// metodo que revisa que elementos fueron seleccionados del control checklist_docs y genera la cadena con el id y la descripcion y el id del perfil create 02-12-2015
        /// </summary>
        protected string[] genera_cadena_docs()
        {
            string cadena = "";
            int contador = 0;
            string[] resultado = new string[2];
            foreach (ListItem li in checklist_docs.Items)
            {
                if (li.Selected == true)
                {
                    cadena = cadena + li.Value + ";";
                    contador = contador + 1;
                }
            }

            resultado[0] = cadena;
            resultado[1] = contador.ToString();
            return resultado;
        }

        /// <summary>
        /// metodo que selecciona los checksbox cuando exista en el registro en la tabla
        /// </summary>
        protected void seleccionar_checks_perfil_docs(DataTable tabla)
        {
            foreach (DataRow fila in tabla.Rows) //tabla
            {
                foreach (ListItem li in checklist_docs.Items) //checkboxlist
                {
                    if (li.Value == fila["idc_tipodoc"].ToString())
                    {
                        li.Selected = true;
                    }
                }
            }
        }

        protected void check_etiqopc_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repite_etiqueta_controles.Items)
            {
                Label lblmin = item.FindControl("lblmin") as Label;
                Label lblmax = item.FindControl("lblmax") as Label;
                CheckBoxList check_etiqopc = (CheckBoxList)item.FindControl("check_etiqopc");
                int min = Convert.ToInt32(lblmin.Text);
                int max = Convert.ToInt32(lblmax.Text);
                int checked_value = 0;
                foreach (ListItem check in check_etiqopc.Items)
                {
                    if (check.Selected == true) { checked_value = checked_value + 1; }
                }
                if (checked_value > max && max != 0)
                {
                    string lastSelectedValue = string.Empty;

                    foreach (ListItem listitem in check_etiqopc.Items)
                    {
                        listitem.Selected = false;
                    }
                    Alert.ShowAlertError("Solo puede seleccionar " + max.ToString() + " opcion(es).", this);
                }
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    try
                    {
                        //este metodo guarda los cambios realizados en los checkboxlist de la pantalla
                        //para que se actualicen en las tablas de session. (guardaCambioscheckboxlist();)
                        guardaCambiosCheckboxlist();
                        if (!validar_campos())
                        {
                            return;
                        }
                        string[] res_gpo_lib = GeneraCadena("TablaPuestoPerfilDGpoLib");
                        string[] res_gpo_opc = GeneraCadena("TablaPuestoPerfilDGpoOpc");
                        string[] res_etiq_lib = GeneraCadena("TablaPuestoPerfilDEtiLib");
                        string[] res_etiq_opc = GeneraCadena("TablaPuestoPerfilDEtiOpc");
                        string[] res_perf_doc = genera_cadena_docs();//add 02-12-2015
                                                                     //declaramos la entidad
                        PerfilesE entidad = new PerfilesE();
                        entidad.Pcadena_examenes = GenerarCadenaExamenes();
                        entidad.Ptotal_examenes = TotalCadenaExamenes();
                        entidad.Idc_perfil = Convert.ToInt32(oc_idc_puestoperfil.Value);
                        entidad.Nombre = txtnomperfil.Text.ToUpper();
                        entidad.Usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        entidad.Cadena_gpo_lib = res_gpo_lib[0];
                        entidad.Cad_total_gpo_lib = Convert.ToInt32(res_gpo_lib[1]);
                        //2da cadena
                        entidad.Cadena_gpo_opc = res_gpo_opc[0];
                        entidad.Cad_total_gpo_opc = Convert.ToInt32(res_gpo_opc[1]);
                        //3er cadena
                        entidad.Cadena_etiq_lib = res_etiq_lib[0];
                        entidad.Cad_total_etiq_lib = Convert.ToInt32(res_etiq_lib[1]);
                        //4ta cadena
                        entidad.Cadena_etiq_opc = res_etiq_opc[0];
                        entidad.Cad_total_etiq_opc = Convert.ToInt32(res_etiq_opc[1]);
                        // cadena de documentos add 02-12-2015
                        entidad.Cadena_perfil_docs = res_perf_doc[0];
                        entidad.Cad_perfil_docs_tot = Convert.ToInt32(res_perf_doc[1]);
                        entidad.Pcadena_archi = GeneraCadenaDocumentos();
                        entidad.Ptotal_cadena_archi = TotalCadenaDocumentos();

                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        //parametro adicional borrador o produccion

                        //componente
                        PerfilesBL componente = new PerfilesBL();
                        DataSet ds = new DataSet();
                        if (check_borr_prod.Checked)
                        {
                            ds = componente.Insertarperfil_borrador(entidad);
                        }
                        else
                        {
                            ds = componente.Insertarperfil(entidad);
                        }
                        DataTable papeleria = (DataTable)Session[lblsession.Text + "papeleria_perfil"];
                        int totalP = ((papeleria.Rows.Count * 1) + 1) * 1000;
                        string tP = totalP.ToString();
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        int pidc_perfil = 0;
                        if (!string.IsNullOrEmpty(Request.QueryString["uidc_puestoperfil"]))
                        {
                            pidc_perfil = Convert.ToInt32(Request.QueryString["uidc_puestoperfil"]);
                        }
                        else
                        {
                            pidc_perfil = Convert.ToInt32(ds.Tables[0].Rows[0]["folio"].ToString()); ;
                        }

                        //if (string.IsNullOrEmpty(vmensaje) && ds.Tables.Count >=1) // si esta vacio todo bien
                        //{
                        //    Alert.ShowGiftMessage("Estamos procesando la cantidad de "+papeleria.Rows.Count.ToString()+" archivo(s) al Servidor.", "Espere un Momento", "perfiles.aspx", "imagenes/loading.gif", tP, "El perfil fue Guardado Correctamente", this);

                        //}
                        if (string.IsNullOrEmpty(vmensaje) && ds.Tables[1] != null) // si esta vacio todo bien
                        {
                            DataTable tabla_archivos = ds.Tables[1];

                            DataTable tabla_archivos_etiquetas = ds.Tables[2];
                            bool correct = true;
                            string ruta = "";
                            if (Convert.ToInt32(Request.QueryString["uborrador"]) == 1)
                            {
                                ruta = GenerarRuta("PPA_BOR");//BORRADOR
                            }
                            else
                            {
                                ruta = GenerarRuta("PPA_PRO");//PRODUCCION
                            }
                            //todo bien
                            //Response.Redirect("perfiles.aspx?sborrador="+interruptor());
                            //Response.Redirect(oc_paginaprevia.Value);
                            string cadena = "";
                            int TOTAL_CAD = 0;
                            foreach (DataRow row in papeleria.Rows)
                            {
                                foreach (DataRow row_archi in tabla_archivos.Rows)
                                {
                                    if (row["descripcion"].ToString() == row_archi["descripcion"].ToString())
                                    {
                                        string new_ruta = ruta + row_archi["name"].ToString() + row_archi["extension"];
                                        if (File.Exists(row["ruta"].ToString()))
                                        {
                                            correct = CopiarArchivos(row["ruta"].ToString(), new_ruta);
                                        }
                                        if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo " + row["ruta"].ToString(), this); }
                                    }
                                }
                            }
                            DataTable archivos_etiquetas = (DataTable)Session[lblsession.Text + "archivos_etiquetas"];
                            foreach (DataRow row in archivos_etiquetas.Rows)
                            {
                                foreach (DataRow row_archi in tabla_archivos_etiquetas.Rows)
                                {
                                    if (row["etiqueta"].ToString() == row_archi["texto"].ToString() || Convert.ToInt32(row["idc"]) == Convert.ToInt32(row_archi["idc"]))
                                    {
                                        if (File.Exists(row["ruta"].ToString()))
                                        {
                                            correct = CopiarArchivos(row["ruta"].ToString(), row_archi["ruta"].ToString());
                                            string path = row_archi["ruta"].ToString();
                                            cadena = cadena + Path.GetFileNameWithoutExtension(path) + ";";
                                            TOTAL_CAD = TOTAL_CAD + 1;
                                        }
                                        if (correct != true) { Alert.ShowAlertError("Hubo un error al subir el archivo " + row["ruta"].ToString(), this); }
                                    }
                                }
                            }
                            int t_archivos_eti = 0;

                            foreach (DataRow row in archivos_etiquetas.Rows)
                            {
                                string path = row["ruta"].ToString();
                                t_archivos_eti = t_archivos_eti + (File.Exists(path) ? 1 : 0);//si existe suma uno si no 0
                            }

                            if (correct == true)
                            {
                                int total = (((tabla_archivos.Rows.Count + t_archivos_eti) * 1) + 1) * 1000;
                                string t = total.ToString();
                                int archivos_procesados = t_archivos_eti + papeleria.Rows.Count;
                                entidad.Cadena_etiq_lib = cadena;
                                entidad.Cad_total_etiq_lib = TOTAL_CAD;
                                entidad.Idc_perfil = pidc_perfil;
                                entidad.Borrador = Convert.ToInt32(Request.QueryString["uborrador"]) == 1 ? false : true; //false = produccion
                                DataSet DS_ARCHI = componente.UpdateEtiquetas(entidad);
                                string vm_mensaje = DS_ARCHI.Tables[0].Rows[0]["mensaje"].ToString();
                                if (vm_mensaje == "")
                                {
                                    Alert.ShowGiftMessage("Estamos procesando la cantidad de " + archivos_procesados.ToString() + " archivo(s) al Servidor.", "Espere un Momento", "perfiles.aspx", "imagenes/loading.gif", t, "El perfil fue Guardado Correctamente", this);
                                }
                                else
                                {
                                    Alert.ShowAlertError(vm_mensaje, this);
                                }
                            }
                        }
                        else
                        {
                            //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                            msgbox.show(vmensaje, this.Page);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.ToString(), this.Page);
                    }
                    break;
            }
        }

        /// <summary>
        /// Copia un archivo de una ruta especifica a otra, si todo fue correcto devuelve un TRUE
        /// </summary>
        /// <param name="sourcefilename"></param>
        /// <param name="destfilename"></param>
        /// <returns></returns>
        public bool CopiarArchivos(string sourcefilename, string destfilename)
        {
            bool correct = true;
            try
            {
                if (!File.Exists(sourcefilename))
                {
                    Alert.ShowAlertError("No existe la ruta " + sourcefilename, this);
                    correct = false;
                }
                if (File.Exists(sourcefilename) && sourcefilename != destfilename)
                {
                    File.Copy(sourcefilename, destfilename, true);
                    correct = true;
                }

                return correct;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
                correct = false;
                return correct;
            }
        }

        public string GenerarRuta(string codigo_imagen)
        {
            string rutaarch = "";
            var Entidad = new UsuariosE();
            Entidad.Cod_arch = codigo_imagen;
            var Componente = new OrganigramaBL();
            var ds = new DataSet();
            ds = Componente.CargaPath(Entidad);
            if (ds.Tables[0].Rows.Count != 0)
            {
                var tablaGrupos = new DataTable();
                //Paso dataset ala tabla
                tablaGrupos = ds.Tables[0];
                var row = tablaGrupos.Rows[0];
                var carpeta = row["unidad"].ToString();
                var domn = Request.Url.Host;
                var url_upload = carpeta;
                rutaarch = url_upload;
            }
            return rutaarch;
        }

        protected void check_gpo_opc_SelectedIndexChanged(object sender, EventArgs e)
        {
            int max = Convert.ToInt32(lblmax.Text);
            int checked_value = 0;
            foreach (ListItem check in check_gpo_opc.Items)
            {
                if (check.Selected == true) { checked_value = checked_value + 1; }
            }
            if (checked_value > max && max != 0)
            {
                int lastSelectedIndex = 0;
                string lastSelectedValue = string.Empty;

                foreach (ListItem listitem in check_gpo_opc.Items)
                {
                    listitem.Selected = false;
                }
                Alert.ShowAlertError("Solo puede seleccionar " + max.ToString() + " opcion(es).", this);
            }
        }

        /// <summary>
        /// Sube archivos a ruta, retorna en forma de Bool si hubo un error
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public bool UploadFile(FileUpload FileUPL, String ruta)
        {
            try
            {
                FileUPL.PostedFile.SaveAs(ruta);
                return false;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
                return true;
            }
        }

        protected void lnkGuardarPape_Click(object sender, EventArgs e)
        {
            bool error = false;

            string id_archi = "0";
            if (Session["id_archi"] != null) { id_archi = (string)Session["id_archi"]; }
            if (txtNombreArchivo.Text == "") { error = true; Alert.ShowAlertError("Ingrese una descripcion para el documento.", this); }
            string caso_guarda = "";
            if (Session["caso_guarda"] != null)
            {
                caso_guarda = (string)Session["caso_guarda"];
            }
            Random random = new Random();
            int randomNumber = random.Next(0, 1000);
            switch (caso_guarda)
            {
                case "":
                    if (fupPapeleria.HasFile && error == false)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/perfiles/"));//path local
                        string mensaje = AddPapeleriaToTable(dirInfo + randomNumber.ToString() + fupPapeleria.FileName, fupPapeleria.FileName, Path.GetExtension(fupPapeleria.FileName).ToString(), txtNombreArchivo.Text.ToUpper(), id_archi);
                        if (mensaje.Equals(string.Empty))
                        {
                            bool pape = UploadFile(fupPapeleria, dirInfo + randomNumber.ToString() + fupPapeleria.FileName);
                            if (pape == false)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + lnkGuardarPape.ClientID.ToString() + "');", true);
                                //Alert.ShowAlert("Archivo Subido Correctamente", "Mensaje del Sistema", this);
                                // Alert.ShowGiftRedirect("Estamos subiendo el archivo al servidor.","Espere un Momento","","imagenes/loading.gif","2000","Archivo Guardardo Correctamente",this);
                                Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "5000", "Archivo Guardardo Correctamente", this);
                                //agregamos a tabla global de papelera
                                fupPapeleria.Visible = true;
                            }
                        }
                        else
                        {
                            Alert.ShowAlertError(mensaje, this);
                        }
                    }
                    break;

                case "Edit":
                    string id_archivo = (string)Session["id_archivo"];
                    DataTable papeleria = (DataTable)Session[lblsession.Text + "papeleria_perfil"];
                    foreach (DataRow row in papeleria.Rows)
                    {
                        if (row["id_archi"].ToString() == id_archivo)
                        {
                            row["descripcion"] = txtNombreArchivo.Text;
                            txtNombreArchivo.Text = "";
                            break;
                        }
                    }
                    gridPapeleria.DataSource = papeleria;
                    gridPapeleria.DataBind();
                    Session[lblsession.Text + "papeleria_perfil"] = papeleria;
                    break;
            }
        }

        /// <summary>
        /// Agrega filas tabla de papeleria global (INCLUYE ELECTOR Y LICENCIA)
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="nombre"></param>
        public string AddPapeleriaToTable(string ruta, string nombre, string extension, string descripcion, string id_archi)
        {
            string mensaje = "";
            bool exists = false;
            DataTable papeleria = (DataTable)Session[lblsession.Text + "papeleria_perfil"];
            foreach (DataRow check in papeleria.Rows)
            {
                if (check["nombre"].Equals(nombre) && check["id_archi"].Equals(id_archi))
                {
                    exists = true;
                    mensaje = check["descripcion"].ToString() + " existente. Elimine el anterior si desea actualizarlo.";
                    break;
                }
            }
            if (exists == false)
            {
                DataRow new_row = papeleria.NewRow();
                new_row["nombre"] = nombre;
                new_row["ruta"] = ruta;
                new_row["extension"] = extension;
                new_row["descripcion"] = descripcion;
                new_row["id_archi"] = id_archi;
                papeleria.Rows.Add(new_row);
                gridPapeleria.DataSource = papeleria;
                gridPapeleria.DataBind();
                Session[lblsession.Text + "papeleria_perfil"] = papeleria;
                txtNombreArchivo.Text = "";
            }
            return mensaje;
        }

        /// <summary>
        /// Agrega filas tabla de papeleria global (INCLUYE ELECTOR Y LICENCIA)
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="nombre"></param>
        public string AddPapeleriaToTableEtiquetas(string ruta, string nombre, string etiqueta, int idc)
        {
            string mensaje = "";
            bool exists = false;
            DataTable papeleria = (DataTable)Session[lblsession.Text + "archivos_etiquetas"];
            foreach (DataRow check in papeleria.Rows)
            {
                if (check["etiqueta"].Equals(etiqueta) || Convert.ToInt32(check["idc"]) == idc)
                {
                    exists = true;
                    mensaje = check["etiqueta"].ToString() + " existente. Elimine el anterior si desea actualizarlo.";
                    break;
                }
            }
            if (exists == false)
            {
                DataRow new_row = papeleria.NewRow();
                new_row["nombre"] = nombre;
                new_row["ruta"] = ruta;
                new_row["etiqueta"] = etiqueta;
                new_row["idc"] = idc;
                papeleria.Rows.Add(new_row);
                gridPapeleriaEtiquetas.DataSource = papeleria;
                gridPapeleriaEtiquetas.DataBind();
                Session[lblsession.Text + "archivos_etiquetas"] = papeleria;
            }
            return mensaje;
        }

        /// <summary>
        /// Retorna la cadena de la tabla de documentos
        /// </summary>
        /// <returns></returns>
        private string GeneraCadenaDocumentos()
        {
            string cadena = "";
            DataTable papeleria = (DataTable)Session[lblsession.Text + "papeleria_perfil"];
            foreach (DataRow row in papeleria.Rows)
            {
                cadena = cadena + row["descripcion"] + ";" + row["extension"] + ";" + row["id_archi"] + ";";
            }
            return cadena;
        }

        /// <summary>
        /// Retorna el total de la cadena de documentos
        /// </summary>
        /// <returns></returns>
        private int TotalCadenaDocumentos()
        {
            DataTable papeleria = (DataTable)Session[lblsession.Text + "papeleria_perfil"];
            return papeleria.Rows.Count;
        }

        protected void gridPapeleria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridPapeleria.DataKeys[index].Values["ruta"].ToString();
            string nombre = gridPapeleria.DataKeys[index].Values["nombre"].ToString();
            string extension = gridPapeleria.DataKeys[index].Values["extension"].ToString();
            string descripcion = gridPapeleria.DataKeys[index].Values["descripcion"].ToString();
            string id_archi = gridPapeleria.DataKeys[index].Values["id_archi"].ToString();
            Session["id_archivo"] = id_archi;
            DataTable papeleria = (DataTable)Session[lblsession.Text + "papeleria_perfil"];
            int papeleriat = papeleria.Rows.Count;
            switch (e.CommandName)
            {
                case "Eliminar":
                    List<DataRow> rowsToDelete = new List<DataRow>();
                    foreach (DataRow row in papeleria.Rows)
                    {
                        if (row["nombre"].ToString().Equals(nombre) || row["descripcion"].ToString().Equals(descripcion))
                        {
                            rowsToDelete.Add(row);
                        }
                    }
                    foreach (DataRow rowde in rowsToDelete)
                    {
                        papeleria.Rows.Remove(rowde);
                    }
                    Session[lblsession.Text + "papeleria_perfil"] = papeleria;
                    gridPapeleria.DataSource = papeleria;
                    gridPapeleria.DataBind();

                    break;

                case "Descargar":
                    Download(ruta, nombre);
                    break;

                case "Editar":
                    // List<DataRow> rowsToDelete2 = new List<DataRow>();
                    foreach (DataRow row in papeleria.Rows)
                    {
                        if (row["nombre"].ToString().Equals(nombre) || row["descripcion"].ToString().Equals(descripcion))
                        {
                            //rowsToDelete2.Add(row);
                            txtNombreArchivo.Text = row["descripcion"].ToString();
                            break;
                        }
                    }
                    //foreach (DataRow rowde in rowsToDelete2)
                    //{
                    //    papeleria.Rows.Remove(rowde);
                    //}
                    Session["caso_guarda"] = "Edit";
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

        public string GenerarCadenaExamenes()
        {
            string cadena = "";
            foreach (ListItem item in chxExamenes.Items)
            {
                if (item.Selected == true)
                {
                    cadena = cadena + item.Value + ";";
                }
            }
            return cadena;
        }

        public int TotalCadenaExamenes()
        {
            int total = 0;
            foreach (ListItem item in chxExamenes.Items)
            {
                if (item.Selected == true)
                {
                    total = total + 1;
                }
            }
            return total;
        }

        protected void grid_dinamico_etiqlibre_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView grid = (GridView)sender;
            TextBox txt = (TextBox)e.Row.FindControl("txtOrder");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string texto = rowView["texto"].ToString();
                txt.Text = (e.Row.RowIndex + 1).ToString();
                DataTable tbl_puestos_perfil_d_eti_lib = (DataTable)Session[lblsession.Text + "archivos_etiquetas"];
                DataView dv_etiqueta = tbl_puestos_perfil_d_eti_lib.DefaultView;
                dv_etiqueta.RowFilter = "etiqueta='" + texto + "'";
                int t = dv_etiqueta.ToTable().Rows.Count;
            }
        }

        protected void gridPapeleriaEtiquetas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ruta = gridPapeleriaEtiquetas.DataKeys[index].Values["ruta"].ToString();
            string nombre = gridPapeleriaEtiquetas.DataKeys[index].Values["nombre"].ToString();
            string etiqueta = gridPapeleriaEtiquetas.DataKeys[index].Values["etiqueta"].ToString();
            DataTable papeleria = (DataTable)Session[lblsession.Text + "archivos_etiquetas"];
            int papeleriat = papeleria.Rows.Count;
            switch (e.CommandName)
            {
                case "Eliminar":
                    List<DataRow> rowsToDelete = new List<DataRow>();
                    foreach (DataRow row in papeleria.Rows)
                    {
                        if (row["nombre"].ToString().Equals(nombre) && row["etiqueta"].ToString().Equals(etiqueta))
                        {
                            rowsToDelete.Add(row);
                        }
                    }
                    foreach (DataRow rowde in rowsToDelete)
                    {
                        papeleria.Rows.Remove(rowde);
                    }
                    Session[lblsession.Text + "archivos_etiquetas"] = papeleria;
                    gridPapeleriaEtiquetas.DataSource = papeleria;
                    gridPapeleriaEtiquetas.DataBind();

                    break;

                case "Descargar":
                    Download(ruta, nombre);
                    break;
            }
        }

        protected void lnkaddfileetiqueta_Click(object sender, EventArgs e)
        {
            bool error = false;
            Random random = new Random();
            string extension = Path.GetExtension(filearchivoetiqueta.FileName);
            error = extension != ".html" ? true : false;
            int randomNumber = random.Next(0, 1000);
            if (filearchivoetiqueta.HasFile && error == false)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/archivos_etiquetas/"));//path local
                string etiqueta = (string)Session["etiqueta"];
                string mensaje = AddPapeleriaToTableEtiquetas(dirInfo + randomNumber.ToString() + filearchivoetiqueta.FileName, filearchivoetiqueta.FileName, etiqueta, Convert.ToInt32(Session[lblsession.Text + "idc_etiqueta_htmlfile"]));
                if (mensaje.Equals(string.Empty))
                {
                    bool pape = UploadFile(filearchivoetiqueta, dirInfo + randomNumber.ToString() + filearchivoetiqueta.FileName);
                    if (pape == false)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "DE", "GoSection('" + "#" + lnkaddfileetiqueta.ClientID.ToString() + "');", true);
                        //Alert.ShowAlert("Archivo Subido Correctamente", "Mensaje del Sistema", this);
                        // Alert.ShowGiftRedirect("Estamos subiendo el archivo al servidor.","Espere un Momento","","imagenes/loading.gif","2000","Archivo Guardardo Correctamente",this);
                        Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "3000", "Archivo Guardardo Correctamente", this);
                        //agregamos a tabla global de papelera
                        filearchivoetiqueta.Visible = true;
                    }
                }
                else
                {
                    Alert.ShowAlertError(mensaje, this);
                }
                Session["etiqueta"] = null;
            }
            else
            {
                Alert.ShowAlertError("Tipo de archivo no permitido. El archivo debe ser .HTML y debe tener contenido.", this);
            }
        }

        protected void lnkEliminar_Click(object sender, EventArgs e)
        {
            DataTable papeleria = (DataTable)Session[lblsession.Text + "archivos_etiquetas"];
            string etiqueta = (string)Session["texto_modal"];
            int I = 0;
            foreach (DataRow row in papeleria.Rows)
            {
                if (row["etiqueta"].Equals(etiqueta) || Convert.ToInt32(Session[lblsession.Text + "idc_etiqueta_htmlfile"]) == Convert.ToInt32(row["IDC"]))
                {
                    if (File.Exists(row["ruta"].ToString()))
                    {
                        I = I + 1;
                        row.Delete();
                        Alert.ShowAlert("Archivo Eliminado", "Mensaje del sistema", this);
                    }

                    break;
                }
            }
            if (I == 0) { Alert.ShowAlertError("NO HAY ARCHIVO RELACIONADO", this); }
            Session[lblsession.Text + "archivos_etiquetas"] = papeleria;
            Session["texto_modal"] = null;
        }

        protected void lnkDescargarArchi_Click(object sender, EventArgs e)
        {
            DataTable papeleria = (DataTable)Session[lblsession.Text + "archivos_etiquetas"];
            string etiqueta = (string)Session["texto_modal"];
            string ruta = "";
            string nombre = "";
            int I = 0;
            foreach (DataRow row in papeleria.Rows)
            {
                if (row["etiqueta"].Equals(etiqueta) || Convert.ToInt32(Session[lblsession.Text + "idc_etiqueta_htmlfile"]) == Convert.ToInt32(row["IDC"]))
                {
                    I = I + 1;
                    nombre = row["nombre"].ToString();
                    ruta = row["ruta"].ToString();
                    break;
                }
            }
            Session[lblsession.Text + "archivos_etiquetas"] = papeleria;
            Session["texto_modal"] = null;

            if (I == 0) { Alert.ShowAlertError("NO HAY ARCHIVO RELACIONADO", this); }
            else
            {
                Download(ruta, nombre);
            }
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            DataTable papeleria = (DataTable)Session[lblsession.Text + "archivos_etiquetas"];
            string etiqueta = (string)Session["texto_modal"];
            string ruta = "";
            string nombre = "";
            int I = 0;
            int idcv = Convert.ToInt32(Session[lblsession.Text + "idc_etiqueta_htmlfile"]);
            foreach (DataRow row in papeleria.Rows)
            {
                //AQUI ME QUEDE 25/04/16
                if (row["etiqueta"].Equals(etiqueta) || Convert.ToInt32(row["IDC"]) == idcv)
                {
                    I = I + 1;
                    nombre = row["nombre"].ToString();
                    ruta = row["ruta"].ToString();
                    break;
                }
            }
            Session[lblsession.Text + "archivos_etiquetas"] = papeleria;
            Session["texto_modal"] = null;

            string queryurl = "&edit_htmlfile=" + funciones.deTextoa64(ruta) + "&etiqueta_edit=" + funciones.deTextoa64(etiqueta) + "&idc_html=" + funciones.deTextoa64(Convert.ToInt32(Session[lblsession.Text + "idc_etiqueta_htmlfile"]).ToString());
            String url = HttpContext.Current.Request.Url.AbsoluteUri;
            String path_actual = url.Substring(url.LastIndexOf("/") + 1);
            url = url.Replace(path_actual, "");
            url = url + "html.aspx?edit_live=true&dinamic_id=" + funciones.deTextoa64(lblsession.Text) + queryurl;
            ScriptManager.RegisterStartupScript(this, GetType(), "noti533W3", "window.open('" + url + "');", true);
        }

        protected void txtOrder_TextChanged(object sender, EventArgs e)
        {
            GridView grid = (GridView)((TextBox)sender).Parent.Parent.Parent.Parent.Parent.Parent;
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;
            TextBox txt = (TextBox)currentRow.FindControl("txtOrder");
            int index = Convert.ToInt32(currentRow.RowIndex);
            int vidc_et = Convert.ToInt32(grid.DataKeys[index].Values["idc_perfiletiq"]);
            string texto = grid.DataKeys[index].Values["texto"].ToString();
            int VALUE_GRID = grid.Rows.Count;
            int orden = Convert.ToInt32(txt.Text);
            DataTable tbl_puestos_perfil_d_eti_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDEtiLib"];
            int vidcp = Convert.ToInt32(grid.DataKeys[index].Values[llave_d_eti_lib.Value.ToString()]);
            foreach (DataRow row_change in tbl_puestos_perfil_d_eti_lib.Rows)
            {
                string value_row_change = row_change[llave_d_eti_lib.Value.ToString()].ToString();
                string texto_etiqueta = row_change["texto"].ToString();
                if (value_row_change == vidcp.ToString() || texto_etiqueta == texto)
                {
                    row_change["orden"] = orden;
                    break;
                }
            }
            foreach (DataRow row_change in tbl_puestos_perfil_d_eti_lib.Rows)
            {
                int orden_cambioi = Convert.ToInt32(row_change["orden"].ToString());
                if (row_change[llave_d_eti_lib.Value.ToString()].ToString() != vidcp.ToString() && orden_cambioi >= orden)
                {
                    row_change["orden"] = Convert.ToInt32(row_change["orden"]) + 1;
                }
            }

            DataView dv_etiqueta = tbl_puestos_perfil_d_eti_lib.DefaultView;
            dv_etiqueta.RowFilter = "idc_perfiletiq=" + vidc_et + " and borrado=0";
            DataTable temp = dv_etiqueta.ToTable();
            temp.DefaultView.Sort = "orden";
            temp = temp.DefaultView.ToTable();
            foreach (DataRow row_change in temp.Rows)
            {
                int index2 = temp.Rows.IndexOf(row_change);
                row_change["orden"] = index2 + 1;
            }

            foreach (DataRow row_change in tbl_puestos_perfil_d_eti_lib.Rows)
            {
                foreach (DataRow row_change2 in temp.Rows)
                {
                    if (row_change["texto"].ToString() == row_change2["texto"].ToString())
                    {
                        row_change["orden"] = row_change2["orden"];
                    }
                }
            }

            Session.Add(lblsession.Text + "TablaPuestoPerfilDEtiLib", tbl_puestos_perfil_d_eti_lib);
            grid.DataSource = filtragriddinamico(vidc_et);
            grid.DataBind();
            grid.Visible = true;
        }

        protected void txtOrderGrupo_TextChanged(object sender, EventArgs e)
        {
            GridView grid = (GridView)((TextBox)sender).Parent.Parent.Parent.Parent;
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox txt = (TextBox)currentRow.FindControl("txtOrderGrupo");
            int index = Convert.ToInt32(currentRow.RowIndex);
            int vidc = Convert.ToInt32(gridgpo_lib.DataKeys[index].Value);
            int VALUE_GRID = grid.Rows.Count;
            int orden = Convert.ToInt32(txt.Text);
            DataTable tbl_puestos_perfil_d_gpo_lib = (DataTable)Session[lblsession.Text + "TablaPuestoPerfilDGpoLib"];
            foreach (DataRow row_change in tbl_puestos_perfil_d_gpo_lib.Rows)
            {
                if (row_change[llave_d_gpo_lib.Value.ToString()].ToString() == vidc.ToString())
                {
                    row_change["orden"] = orden;
                    break;
                }
            }
            foreach (DataRow row_change in tbl_puestos_perfil_d_gpo_lib.Rows)
            {
                int orden_cambioi = Convert.ToInt32(row_change["orden"].ToString());
                if (row_change[llave_d_gpo_lib.Value.ToString()].ToString() != vidc.ToString() && orden_cambioi >= orden)
                {
                    row_change["orden"] = Convert.ToInt32(row_change["orden"]) + 1;
                }
            }
            DataView dv_gpo_lib = tbl_puestos_perfil_d_gpo_lib.DefaultView;
            dv_gpo_lib.RowFilter = "idc_perfilgpo = " + ocgpoidmenu.Value + " and borrado=0";
            DataTable temp = dv_gpo_lib.ToTable();
            temp.DefaultView.Sort = "orden";
            temp = temp.DefaultView.ToTable();
            foreach (DataRow row_change in temp.Rows)
            {
                int index2 = temp.Rows.IndexOf(row_change);
                row_change["orden"] = index2 + 1;
            }

            foreach (DataRow row_change in tbl_puestos_perfil_d_gpo_lib.Rows)
            {
                foreach (DataRow row_change2 in temp.Rows)
                {
                    if (row_change["idc_perfilgpo"].ToString() == row_change2["idc_perfilgpo"].ToString() && row_change["texto"].ToString() == row_change2["texto"].ToString())
                    {
                        row_change["orden"] = row_change2["orden"];
                    }
                }
            }
            //subir
            Session.Add(lblsession.Text + "TablaPuestoPerfilDGpoLib", tbl_puestos_perfil_d_gpo_lib);
            //actualiza
            ////actualizar grids
            filtrarGrids();
            //AQUI SE QUEDO
        }

        protected void gridgpo_lib_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void gridgpo_lib_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            GridView grid = (GridView)sender;
            TextBox txt = (TextBox)e.Row.FindControl("txtOrderGrupo");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                txt.Text = (e.Row.RowIndex + 1).ToString();
            }
        }
    }
}