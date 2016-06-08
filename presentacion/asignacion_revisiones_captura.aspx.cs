using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class asignacion_revisiones_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack && Request.QueryString["edit"] == null)
            {
                CargarComboTipoRev();
                CargarCombosPuestos(ddlPuestorevisa, "");//combo revisa
                CargarCombosPuestos(ddlPrepara, "");//combo prepara
                CargarCombosPuestos(ddlEntrega, "");//combo entrega
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_puesto");
                dt.Columns.Add("puesto");
                Session["tabla_puesto"] = dt;
                DataTable dtp = new DataTable();
                dtp.Columns.Add("idc_puesto_gpo");
                dtp.Columns.Add("grupo");
                Session["tabla_grupos"] = dtp;
                CargaGrid("");
            }
            if (!IsPostBack && Request.QueryString["edit"] != null)
            {
                CargarComboTipoRev();
                CargarCombosPuestos(ddlPuestorevisa, "");//combo revisa
                CargarCombosPuestos(ddlPrepara, "");//combo prepara
                CargarCombosPuestos(ddlEntrega, "");//combo entrega
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_puesto");
                dt.Columns.Add("puesto");
                Session["tabla_puesto"] = dt;
                DataTable dtp = new DataTable();
                dtp.Columns.Add("idc_puesto_gpo");
                dtp.Columns.Add("grupo");
                Session["tabla_grupos"] = dtp;
                CargaGridEdicion();
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

        /// <summary>
        /// Carga Grid Pirncipal
        /// </summary>
        public void CargaGrid(string filtro)
        {
            Grupos_PuestosENT entidad = new Grupos_PuestosENT();
            Grupos_PuestosCOM componente = new Grupos_PuestosCOM();
            entidad.Filtro = filtro;
            DataSet ds = componente.CargaPuesto(entidad);
            repeat_puestos.DataSource = ds.Tables[0];
            repeat_puestos.DataBind();
            int count = ds.Tables[0].Rows.Count;
            lblContentRepeat.Text = count.ToString();
            CheckValues();
            Session["tabla_total"] = ds.Tables[0];
            if (ds.Tables[0].Rows.Count == 0) { PanelPuestos.Visible = false; lblnopuestos.Visible = true; }
            else { lblnopuestos.Visible = false; PanelPuestos.Visible = true; }
            btnDeseleccionarTodos.Visible = false;
            btnSeleccionarTodos.Visible = true;
        }

        /// <summary>
        /// Marca los valores ya obtenidos
        /// </summary>
        public void CargaGridEdicion()
        {
            Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
            Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
            entidad.Idc_clasifrev = Convert.ToInt32(Session["idc_clasifrev_edit"].ToString());
            DataSet ds = componente.CargaDatosEducionRevision(entidad);
            repeat_puestos.DataSource = ds.Tables[1];
            repeat_puestos.DataBind();
            int count = ds.Tables[1].Rows.Count;
            lblContentRepeat.Text = count.ToString();
            AddValuesforEdit(ds.Tables[1]);
            repeat_grupos.DataSource = ds.Tables[2];
            repeat_grupos.DataBind();
            AddValuesforEditGpo(ds.Tables[2]);
            Session["tabla_total"] = ds.Tables[1];
            Session["tabla_total_gpo"] = ds.Tables[2];
            Session["tipo_aplica"] = ds.Tables[0].Rows[0]["tipo_aplica"].ToString();
            CheckValues();
            //cargamos valores por default a combos
            string tipo_aplica = ds.Tables[0].Rows[0]["tipo_aplica"].ToString();
            string tipo_rev = ds.Tables[0].Rows[0]["tipo_rev"].ToString();
            string idc_sucursal = ds.Tables[0].Rows[0]["idc_sucursal"].ToString();
            string preparar = ds.Tables[0].Rows[0]["idc_puesto_prepara"].ToString();
            string revisa = ds.Tables[0].Rows[0]["idc_puesto_revisa"].ToString();
            string entrega = ds.Tables[0].Rows[0]["idc_puesto_entrega"].ToString();
            ValidarDropTipoRev(tipo_rev);
            ValidarDropTipoAplicacion(tipo_aplica);
            ddlTipoaplicacion.SelectedValue = tipo_aplica;
            ddlTipoRev.SelectedValue = tipo_rev;
            ddlPrepara.SelectedValue = preparar;
            ddlEntrega.SelectedValue = entrega;
            ddlPuestorevisa.SelectedValue = revisa;
            if (idc_sucursal == "0")//no tiene sucursal
            {
                Sucur.Visible = false;
                cbxSinSucursal.Checked = true;
            }
            else
            {
                ddlSucursales.SelectedValue = idc_sucursal;
                Sucur.Visible = true;
                cbxSinSucursal.Checked = false;
            }
        }

        /// <summary>
        /// Carga combo CargarComboTipoRev
        /// </summary>
        private void CargarComboTipoRev()
        {
            Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
            entidad.Servici = "R";
            entidad.ServicioBool = true;
            Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
            DataSet ds = componente.CargaTipoRevisiones(entidad);
            ddlTipoRev.DataValueField = "valor";
            ddlTipoRev.DataTextField = "tipo_rev";
            ddlTipoRev.DataSource = ds.Tables[0];
            ddlTipoRev.DataBind();
            ddlTipoRev.Items.Insert(0, new ListItem("Seleccione uno", "0")); //updated code}
            Session["tabla_tipo_rev"] = ds.Tables[0];
            //tipo_aplicacion
            DataSet dsta = componente.CargaTipoAplicacion(entidad);
            ddlTipoaplicacion.DataValueField = "valor";
            ddlTipoaplicacion.DataTextField = "tipo_rev";
            ddlTipoaplicacion.DataSource = dsta.Tables[0];
            ddlTipoaplicacion.DataBind();
            ddlTipoaplicacion.Items.Insert(0, new ListItem("Seleccione uno", "0")); //updated code}
            Session["tabla_tipo_apli"] = dsta.Tables[0];
            //grupos
            Grupos_PuestosENT entidadgpo = new Grupos_PuestosENT();
            Grupos_PuestosCOM componentegpo = new Grupos_PuestosCOM();
            DataSet dsgpo = componentegpo.CargaGrupos_Puestos(entidadgpo);
            int total_gpo = dsgpo.Tables[0].Rows.Count;
            if (total_gpo == 0)
            {
                PuestosGroup.Visible = false;
                lblNOHayGrupos.Visible = true;
                PanelGrupos.Visible = false;
            }
            else
            {
                repeat_grupos.DataSource = dsgpo.Tables[0];
                repeat_grupos.DataBind();
                Session["tabla_total_gpo"] = dsgpo.Tables[0];
                lblNOHayGrupos.Visible = false;
                PanelGrupos.Visible = true;
            }
            //sucursales
            DataSet d2s = componente.CargaSucursales(entidad);
            ddlSucursales.DataValueField = "idc_sucursal";
            ddlSucursales.DataTextField = "nombre";
            ddlSucursales.DataSource = d2s.Tables[0];
            ddlSucursales.DataBind();
        }

        /// <summary>
        /// Carga combo CargarComboTipoRev
        /// </summary>
        private void CargarCombosPuestos(DropDownList ddl, string filtro)
        {
            Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
            Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
            entidad.Filtro = filtro;
            DataSet ds = componente.CargaComboDinamico(entidad);
            ddl.DataValueField = "idc_puesto";
            ddl.DataTextField = "descripcion_puesto_completa";
            ddl.DataSource = ds.Tables[0];
            ddl.DataBind();
            if (filtro == "")
            {
                ddl.Items.Insert(0, new ListItem("Seleccione un Puesto", "0")); //updated code}
            }
        }

        /// <summary>
        /// Regresa Cadena , BOOL EXPECIONTHIS INDICA SI ES UNA EXCEPCION
        /// </summary>
        /// <returns></returns>
        public string GeneraCadena(string tabla_session, string row_name, string session_global, bool expecionthis)
        {
            string cadena = "";
            if (expecionthis == false)
            {
                DataTable dt = (DataTable)Session[tabla_session];
                foreach (DataRow row in dt.Rows)
                {
                    cadena = cadena + row[row_name].ToString() + ";";
                }
            }
            if (expecionthis == true)
            {
                DataTable dt = (DataTable)Session[tabla_session];
                DataTable global = (DataTable)Session[session_global];
                foreach (DataRow row in global.Rows)
                {
                    string valueglobal = row[row_name].ToString();
                    if (ExistValueinTable(valueglobal) == false)//SI EL VALOR NO ESTA EN LA LISTA
                    {
                        cadena = cadena + row[row_name].ToString() + ";";
                    }
                }
            }
            return cadena;
        }

        /// <summary>
        /// Regresa Total de Cadena de Puestos
        /// </summary>
        /// <returns></returns>
        public int TotalCadena(string tabla_session, string row_name, string session_global, bool expecionthis, bool contar_total)
        {
            int total = 0;
            if (contar_total == false)//si no es todos los puestos
            {
                if (expecionthis == false)
                {
                    DataTable dt = (DataTable)Session[tabla_session];
                    total = dt.Rows.Count;
                }
                if (expecionthis == true)//trae valores de excepciones
                {
                    DataTable dt = (DataTable)Session[tabla_session];
                    DataTable global = (DataTable)Session[session_global];
                    foreach (DataRow row in global.Rows)
                    {
                        string valueglobal = row[row_name].ToString();
                        if (ExistValueinTable(valueglobal) == false)//SI EL VALOR NO ESTA EN LA LISTA
                        {
                            total = total + 1;
                        }
                    }
                }
            }
            return total;
        }

        /// <summary>
        /// Si existe lo agrega a la tabla para editar
        /// </summary>
        /// <param name="table_data"></param>
        private void AddValuesforEdit(DataTable table_data)
        {
            DataTable dt = (DataTable)Session["tabla_puesto"];
            foreach (DataRow row in table_data.Rows)
            {
                int existe = Convert.ToInt32(row["existe"].ToString());
                int idc_puesto = Convert.ToInt32(row["idc_puesto"].ToString());
                string puesto = row["descripcion"].ToString();
                if (existe != 0)
                {
                    DataRow new_row = dt.NewRow();
                    new_row["idc_puesto"] = idc_puesto.ToString();
                    new_row["puesto"] = puesto.ToString();
                    dt.Rows.Add(new_row);
                }
            }
            Session["tabla_puesto"] = dt;
        }

        /// <summary>
        /// Si existe lo agrega a la tabla para editar
        /// </summary>
        /// <param name="table_data"></param>
        private void AddValuesforEditGpo(DataTable table_data)
        {
            DataTable dt = (DataTable)Session["tabla_grupos"];
            foreach (DataRow row in table_data.Rows)
            {
                int existe = Convert.ToInt32(row["existe"].ToString());
                int idc_puesto = Convert.ToInt32(row["idc_puesto_gpo"].ToString());
                if (existe != 0)
                {
                    DataRow new_row = dt.NewRow();
                    new_row["idc_puesto_gpo"] = idc_puesto.ToString();
                    dt.Rows.Add(new_row);
                }
            }
            Session["tabla_grupos"] = dt;
        }

        /// <summary>
        /// Verifica si existe el valor de un item en la tabla de session y checkea
        /// </summary>
        private void CheckValues()
        {
            //verificamos si existe un registro en tabla
            DataTable dt = (DataTable)Session["tabla_puesto"];
            string tipo_aplica = (string)Session["tipo_aplica"];
            if (dt.Rows.Count > 0)
            {
                //si existe recorremos tabla
                foreach (DataRow row in dt.Rows)
                {
                    string valuehold = row["idc_puesto"].ToString();
                    //recorremos repeat
                    foreach (RepeaterItem item in repeat_puestos.Items)
                    {
                        Label lblValue = (Label)item.FindControl("lblValue");
                        CheckBox cbxCheck = (CheckBox)item.FindControl("cbxCheck");
                        string value = lblValue.Text;
                        //si existe el valor del repeat en la tabla checheamos check
                        if (valuehold.Equals(value))//SI EL ES IGUAL AL EVENTO
                        {
                            cbxCheck.Checked = true;
                        }
                    }
                }
            }
            //verificamos si existe un registro en tabla
            DataTable dt2 = (DataTable)Session["tabla_grupos"];

            if (dt2.Rows.Count > 0)
            {
                //si existe recorremos tabla
                foreach (DataRow row in dt2.Rows)
                {
                    string valuehold = row["idc_puesto_gpo"].ToString();
                    //recorremos repeat
                    foreach (RepeaterItem item in repeat_grupos.Items)
                    {
                        Label lblValue = (Label)item.FindControl("lblValueGpo");
                        CheckBox cbxCheck = (CheckBox)item.FindControl("cbxCheckGpo");
                        string value = lblValue.Text;
                        //si existe el valor del repeat en la tabla checheamos check
                        if (valuehold.Equals(value))//SI EL ES IGUAL AL EVENTO
                        {
                            cbxCheck.Checked = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Agrega valor a tabla de session
        /// </summary>
        /// <param name="value"></param>
        private void AddTable(string value, string tabla_session, string row_name, string value2, string row_name2)
        {
            DataTable dt = (DataTable)Session[tabla_session];
            DataRow new_row = dt.NewRow();
            new_row[row_name] = value;
            new_row[row_name2] = value2;
            dt.Rows.Add(new_row);
            Session[tabla_session] = dt;
        }

        /// <summary>
        /// Comprueba si existe un valor en una tabla de session
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ExistValueinTable(string value)
        {
            bool exist = false;
            DataTable dt = (DataTable)Session["tabla_puesto"];
            foreach (DataRow row in dt.Rows)
            {
                if (row["idc_puesto"].ToString().Equals(value))
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        /// <summary>
        /// Elimina un valor de un tabla de session
        /// </summary>
        /// <param name="value"></param>
        private void DeleteValuefromTable(string value, string tabla_session, string row_name)
        {
            DataTable dt = (DataTable)Session[tabla_session];
            foreach (DataRow row in dt.Rows)
            {
                if (row[row_name].ToString().Equals(value))
                {
                    row.Delete();
                    break;
                }
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
            Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
            entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
            entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
            entidad.Pusuariopc = funciones.GetUserName();//usuario pc
            DataSet ds = new DataSet();
            switch (Confirma_a)
            {
                case "Cancelar":
                    Response.Redirect("catalogo_asignacion_revisiones.aspx");
                    break;

                case "Guardar":

                    entidad.Tipo_rev = ddlTipoRev.SelectedValue;
                    if (PanelComboTipoApli.Visible == true)
                    {
                        entidad.Tipo_aplica = ddlTipoaplicacion.SelectedValue;
                    }
                    else
                    {
                        entidad.Tipo_aplica = (string)Session["value_tipo_apli"];
                    }
                    entidad.Numcadgrupos = Convert.ToInt32(Session["Total_Grupos"]);
                    entidad.Numcadpuesto = Convert.ToInt32(Session["Total_Puestos"]);
                    entidad.Cad_gpos = (string)Session["Cadena_Grupos"];
                    entidad.Cad_puestos = (string)Session["Cadena_Puestos"];
                    if (PanelContent.Visible == true)//SI NO ES FINAL
                    {
                        entidad.Idc_puesto_revisa = Convert.ToInt32(ddlPuestorevisa.SelectedValue);
                        entidad.Idc_puesto_prepara = Convert.ToInt32(ddlPrepara.SelectedValue);
                        entidad.Idc_puesto_entrega = Convert.ToInt32(ddlEntrega.SelectedValue);
                    }
                    else
                    {
                        entidad.Idc_puesto_revisa = Convert.ToInt32(ddlPuestorevisa.SelectedValue);
                        entidad.Idc_puesto_prepara = Convert.ToInt32(ddlPuestorevisa.SelectedValue);
                        entidad.Idc_puesto_entrega = Convert.ToInt32(ddlPuestorevisa.SelectedValue);
                    }
                    if (cbxSinSucursal.Checked != true)//SI TIENE UN VALOR DE SUCURSAL
                    {
                        entidad.Idc_sucursal = Convert.ToInt32(ddlSucursales.SelectedValue);
                    }
                    else { entidad.Idc_sucursal = 0; }
                    ds = componente.AgregarAsignacion(entidad);
                    break;

                case "Editar":
                    entidad.Idc_clasifrev = Convert.ToInt32(Session["idc_clasifrev_edit"].ToString());
                    entidad.Tipo_rev = ddlTipoRev.SelectedValue;

                    entidad.Tipo_rev = ddlTipoRev.SelectedValue;
                    if (PanelComboTipoApli.Visible == true)
                    {
                        entidad.Tipo_aplica = ddlTipoaplicacion.SelectedValue;
                    }
                    else
                    {
                        entidad.Tipo_aplica = (string)Session["value_tipo_apli"];
                    }
                    entidad.Numcadgrupos = Convert.ToInt32(Session["Total_Grupos"]);
                    entidad.Numcadpuesto = Convert.ToInt32(Session["Total_Puestos"]);
                    entidad.Cad_gpos = (string)Session["Cadena_Grupos"];
                    entidad.Cad_puestos = (string)Session["Cadena_Puestos"];
                    if (PanelContent.Visible == true)//SI NO ES FINAL
                    {
                        entidad.Idc_puesto_revisa = Convert.ToInt32(ddlPuestorevisa.SelectedValue);
                        entidad.Idc_puesto_prepara = Convert.ToInt32(ddlPrepara.SelectedValue);
                        entidad.Idc_puesto_entrega = Convert.ToInt32(ddlEntrega.SelectedValue);
                    }
                    else
                    {
                        entidad.Idc_puesto_revisa = Convert.ToInt32(ddlPuestorevisa.SelectedValue);
                        entidad.Idc_puesto_prepara = Convert.ToInt32(ddlPuestorevisa.SelectedValue);
                        entidad.Idc_puesto_entrega = Convert.ToInt32(ddlPuestorevisa.SelectedValue);
                    }
                    if (cbxSinSucursal.Checked != true)//SI TIENE UN VALOR DE SUCURSAL
                    {
                        entidad.Idc_sucursal = Convert.ToInt32(ddlSucursales.SelectedValue);
                    }
                    else { entidad.Idc_sucursal = 0; }
                    ds = componente.EditarAsignacion(entidad);
                    break;
            }
            string mensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
            if (mensaje == "")//no hay errores retornamos true
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_puesto");
                Session["tabla_puesto"] = dt;
                DataTable dtp = new DataTable();
                dtp.Columns.Add("idc_puesto_gpo");
                Session["tabla_grupos"] = dtp;
                ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La Asignación fue realizada correctamente.','catalogo_asignacion_revisiones.aspx');", true);
            }
            else
            {//mostramos error
                Alert.ShowAlertError(mensaje, this);
            }
        }

        protected void ddlTipoRev_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarDropTipoRev(ddlTipoRev.SelectedValue);
        }

        protected void txtFiltrorev_TextChanged(object sender, EventArgs e)
        {
            CargarCombosPuestos(ddlPuestorevisa, txtFiltrorev.Text);
        }

        protected void txtFiltroPrep_TextChanged(object sender, EventArgs e)
        {
            CargarCombosPuestos(ddlPrepara, txtFiltroPrep.Text);
        }

        protected void txtFiltroEntrega_TextChanged(object sender, EventArgs e)
        {
            CargarCombosPuestos(ddlEntrega, txtFiltroEntrega.Text);
        }

        protected void cbxCheck_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbxObjetc = (CheckBox)sender;//OBJETTO QUE ENVIA EL EVENTO
            //RECORREOMOS REPEAT
            foreach (RepeaterItem item in repeat_puestos.Items)
            {
                Label lblValue = (Label)item.FindControl("lblValue");
                CheckBox cbxCheck = (CheckBox)item.FindControl("cbxCheck");
                if (cbxCheck == cbxObjetc)//SI EL ES IGUAL AL EVENTO
                {
                    if (cbxCheck.Checked == true)//si esta seleccionado elimino y agrego
                    {
                        DeleteValuefromTable(lblValue.Text, "tabla_puesto", "idc_puesto");
                        AddTable(lblValue.Text, "tabla_puesto", "idc_puesto", cbxCheck.Text, "puesto");
                    }
                    else
                    {//si no esta seleccionado elimino
                        DeleteValuefromTable(lblValue.Text, "tabla_puesto", "idc_puesto");
                    }
                }
            }
        }

        protected void lnkSelected_Click(object sender, EventArgs e)
        {
            LinkButton lnkobject = (LinkButton)sender;
            foreach (RepeaterItem item in repeat_puestos.Items)
            {
                Label lblValue = (Label)item.FindControl("lblValue");
                CheckBox cbxCheck = (CheckBox)item.FindControl("cbxCheck");
                LinkButton lnkSelected = (LinkButton)item.FindControl("lnkSelected");
                if (lnkobject == lnkSelected)//SI EL ES IGUAL AL EVENTO
                {
                    if (cbxCheck.Checked == true)//si esta seleccionado elimino y agrego
                    {
                        DeleteValuefromTable(lblValue.Text, "tabla_puesto", "idc_puesto");
                        cbxCheck.Checked = false;
                    }
                    else
                    {//si no esta seleccionado elimino
                        DeleteValuefromTable(lblValue.Text, "tabla_puesto", "idc_puesto");
                        AddTable(lblValue.Text, "tabla_puesto", "idc_puesto", cbxCheck.Text, "puesto");
                        cbxCheck.Checked = true;
                    }
                }
            }
        }

        protected void btnSeleccionarTodos_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeat_puestos.Items)
            {
                Label lblValue = (Label)item.FindControl("lblValue");
                CheckBox cbxCheck = (CheckBox)item.FindControl("cbxCheck");
                //  DeleteValuefromTable(lblValue.Text, "tabla_puesto", "idc_puesto");
                AddTable(lblValue.Text, "tabla_puesto", "idc_puesto", cbxCheck.Text, "puesto");
                cbxCheck.Checked = true;
            }
            btnSeleccionarTodos.Visible = false;
            btnDeseleccionarTodos.Visible = true;
        }

        protected void btnDeseleccionarTodos_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeat_puestos.Items)
            {
                Label lblValue = (Label)item.FindControl("lblValue");
                CheckBox cbxCheck = (CheckBox)item.FindControl("cbxCheck");
                DeleteValuefromTable(lblValue.Text, "tabla_puesto", "idc_puesto");
                cbxCheck.Checked = false;
            }
            btnSeleccionarTodos.Visible = true;
            btnDeseleccionarTodos.Visible = false;
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            CargaGrid(txtFiltro.Text);
        }

        protected void ddlTipoaplicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarDropTipoAplicacion(ddlTipoaplicacion.SelectedValue);
        }

        protected void lnkSelectedGpo_Click(object sender, EventArgs e)
        {
            LinkButton lnkobject = (LinkButton)sender;
            foreach (RepeaterItem item in repeat_grupos.Items)
            {
                Label lblValue = (Label)item.FindControl("lblValueGpo");
                CheckBox cbxCheck = (CheckBox)item.FindControl("cbxCheckGpo");
                LinkButton lnkSelected = (LinkButton)item.FindControl("lnkSelectedGpo");
                if (lnkobject == lnkSelected)//SI EL ES IGUAL AL EVENTO
                {
                    if (cbxCheck.Checked == true)//si esta seleccionado elimino y agrego
                    {
                        DeleteValuefromTable(lblValue.Text, "tabla_grupos", "idc_puesto_gpo");
                        cbxCheck.Checked = false;
                    }
                    else
                    {//si no esta seleccionado elimino
                        DeleteValuefromTable(lblValue.Text, "tabla_grupos", "idc_puesto_gpo");
                        AddTable(lblValue.Text, "tabla_grupos", "idc_puesto_gpo", cbxCheck.Text, "grupo");
                        cbxCheck.Checked = true;
                    }
                }
            }
        }

        protected void cbxCheckGpo_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbxObjetc = (CheckBox)sender;//OBJETTO QUE ENVIA EL EVENTO
            //RECORREOMOS REPEAT
            foreach (RepeaterItem item in repeat_grupos.Items)
            {
                Label lblValue = (Label)item.FindControl("lblValueGpo");
                CheckBox cbxCheck = (CheckBox)item.FindControl("cbxCheckGpo");
                if (cbxCheck == cbxObjetc)//SI EL ES IGUAL AL EVENTO
                {
                    if (cbxCheck.Checked == true)//si esta seleccionado elimino y agrego
                    {
                        DeleteValuefromTable(lblValue.Text, "tabla_grupos", "idc_puesto_gpo");
                        AddTable(lblValue.Text, "tabla_grupos", "idc_puesto_gpo", cbxCheck.Text, "grupo");
                    }
                    else
                    {//si no esta seleccionado elimino
                        DeleteValuefromTable(lblValue.Text, "tabla_grupos", "idc_puesto_gpo");
                    }
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Alert.ShowAlert(CadenaPuestos()+"  total "+TotalCadena().ToString(),"",this);
            bool excpecion = false;
            bool total = false;
            if (PanelTipoApli.Visible == false) { total = true; }//si esta visible entonces es TODOS LOS PUESTOS
            //if (ddlTipoaplicacion.SelectedValue == "E") { excpecion = true; }
            Session["Cadena_Puestos"] = GeneraCadena("tabla_puesto", "idc_puesto", "tabla_total", excpecion);
            Session["Total_Puestos"] = TotalCadena("tabla_puesto", "idc_puesto", "tabla_total", excpecion, total);
            Session["Cadena_Grupos"] = GeneraCadena("tabla_grupos", "idc_puesto_gpo", "tabla_total_gpo", excpecion);
            Session["Total_Grupos"] = TotalCadena("tabla_grupos", "idc_puesto_gpo", "tabla_total_gpo", excpecion, total);
            if (Session["value_tipo_apli"] == null || PanelTipoApli.Visible == false) { Session["value_tipo_apli"] = "T"; }
            if (Error() == false && Request.QueryString["edit"] == null)//GUARDAR
            {
                Session["Caso_Confirmacion"] = "Guardar";
                //string total = TotalCadena("tabla_puesto").ToString();
                //string totalgpos= TotalCadena("tabla_grupos").ToString();
                //Alert.ShowAlertError("PUESTOS: "+total+" GPOS: "+totalgpos,this);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea guardar esta Asignacion?');", true);
            }
            if (Error() == false && Request.QueryString["edit"] != null)//ACTUALIZAR
            {
                Session["Caso_Confirmacion"] = "Editar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Editar esta Asignacion?');", true);
            }
        }

        public new bool Error()
        {
            bool error = false;
            bool excpecion = false; bool total = false;
            if (ddlTipoaplicacion.SelectedValue == "E") { excpecion = true; }
            if (ddlPuestorevisa.SelectedValue == "0") { lblRevisa.Visible = true; error = true; }
            if (PanelContent.Visible == true)
            {
                if (ddlEntrega.SelectedValue == "0") { lblEntrega.Visible = true; error = true; }
                if (ddlPrepara.SelectedValue == "0") { lblPrepara.Visible = true; error = true; }
            }
            lblrrortiporev.Visible = false;
            if (ddlTipoRev.SelectedValue == "0") { error = true; lblrrortiporev.Visible = true; }
            ddltipoaplica.Visible = false;
            if (ddlTipoaplicacion.SelectedValue == "0" && PanelComboTipoApli.Visible == true) { error = true; ddltipoaplica.Visible = true; }
            if (TotalCadena("tabla_grupos", "idc_puesto_gpo", "tabla_total_gpo", excpecion, total) == 0 && TotalCadena("tabla_puesto", "idc_puesto", "tabla_total", excpecion, total) == 0 && PanelTipoApli.Visible == true) { error = true; Alert.ShowAlertError("Para Guardar, debe seleccionar un puesto de Lista, o un Grupo de Puestos", this); }
            return error;
        }

        protected void cbxSinSucursal_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSinSucursal.Checked == true)
            {
                Sucur.Visible = false;
            }
            else
            {
                Sucur.Visible = true;
            }
        }

        protected void ddlPuestorevisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRevisa.Visible = false;
            if (ddlPuestorevisa.SelectedValue == "0")
            {
                lblRevisa.Visible = true;
            }
        }

        protected void ddlPrepara_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPrepara.Visible = false;
            if (ddlPrepara.SelectedValue == "0")
            {
                lblPrepara.Visible = true;
            }
        }

        protected void ddlEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblEntrega.Visible = false;
            if (ddlEntrega.SelectedValue == "0")
            {
                lblEntrega.Visible = true;
            }
        }

        protected void btnVerTodos_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tabla_puesto"];
            bltPuestos.DataTextField = "puesto";
            bltPuestos.DataValueField = "idc_puesto";
            bltPuestos.DataSource = dt;
            bltPuestos.DataBind();
            lblNombre.Text = "Un total de " + dt.Rows.Count.ToString() + " Puestos seleccionados.";
            if (dt.Rows.Count == 0) { PanelPuestosSelected.Visible = false; lblNOhayPuestos.Visible = true; }
            else { PanelPuestosSelected.Visible = true; lblNOhayPuestos.Visible = false; }
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalView();", true);
        }

        /// <summary>
        /// Valida el valor seleccionado de un ddl
        /// </summary>
        private void ValidarDropTipoRev(string value_selected)
        {
            PanelContent.Visible = true;
            PanelTipoApli.Visible = true;
            PanelSucursal.Visible = true;
            PanelComboTipoApli.Visible = true;
            DataTable dt = (DataTable)Session["tabla_tipo_rev"];
            foreach (DataRow row in dt.Rows)
            {
                //buscamos el valor
                string value = row["valor"].ToString();
                if (value_selected == value)
                {
                    //indica si mostrar elegir puestos
                    bool todos_los_puestos = Convert.ToBoolean(row["mostrar_los_puestos"]);
                    //indica si solo aplica revision y no prepara,entrega
                    bool solo_revision = Convert.ToBoolean(row["aplica_rev_pre_entr"]);
                    //indica si aplica sucursal
                    bool aplica_sucursal = Convert.ToBoolean(row["aplica_sucursal"]);
                    PanelSucursal.Visible = aplica_sucursal;
                    PanelContent.Visible = solo_revision;
                    PanelTipoApli.Visible = todos_los_puestos;
                    PanelComboTipoApli.Visible = todos_los_puestos;
                    ddlTipoaplicacion.SelectedValue = "0";
                    btnSeleccionarTodos.Visible = true;
                    btnDeseleccionarTodos.Visible = false;
                    if (todos_los_puestos == false)
                    {
                        foreach (RepeaterItem item in repeat_puestos.Items)
                        {
                            Label lblValue = (Label)item.FindControl("lblValue");
                            CheckBox cbxCheck = (CheckBox)item.FindControl("cbxCheck");
                            DeleteValuefromTable(lblValue.Text, "tabla_puesto", "idc_puesto");
                            cbxCheck.Checked = false;
                        }
                        foreach (RepeaterItem item in repeat_grupos.Items)
                        {
                            Label lblValue = (Label)item.FindControl("lblValueGpo");
                            CheckBox cbxCheck = (CheckBox)item.FindControl("cbxCheckGpo");
                            DeleteValuefromTable(lblValue.Text, "tabla_grupos", "idc_puesto_gpo");
                            cbxCheck.Checked = false;
                        }
                        btnSeleccionarTodos.Visible = true;
                        btnDeseleccionarTodos.Visible = false;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Valida el valor seleccionado de un ddl
        /// </summary>
        private void ValidarDropTipoAplicacion(string value_selected)
        {
            Session["value_tipo_apli"] = value_selected;
            PanelTipoApli.Visible = true;
            DataTable dt = (DataTable)Session["tabla_tipo_apli"];
            foreach (DataRow row in dt.Rows)
            {
                //buscamos el valor
                string value = row["valor"].ToString();
                if (value_selected == value)
                {
                    //indica si mostrar elegir puestos
                    bool oculta_puestos = Convert.ToBoolean(row["muestra_puestos"]);
                    PanelTipoApli.Visible = oculta_puestos;
                    if (oculta_puestos == false)
                    {
                        foreach (RepeaterItem item in repeat_puestos.Items)
                        {
                            Label lblValue = (Label)item.FindControl("lblValue");
                            CheckBox cbxCheck = (CheckBox)item.FindControl("cbxCheck");
                            DeleteValuefromTable(lblValue.Text, "tabla_puesto", "idc_puesto");
                            cbxCheck.Checked = false;
                        }
                        foreach (RepeaterItem item in repeat_grupos.Items)
                        {
                            Label lblValue = (Label)item.FindControl("lblValueGpo");
                            CheckBox cbxCheck = (CheckBox)item.FindControl("cbxCheckGpo");
                            DeleteValuefromTable(lblValue.Text, "tabla_grupos", "idc_puesto_gpo");
                            cbxCheck.Checked = false;
                        }
                        btnSeleccionarTodos.Visible = true;
                        btnDeseleccionarTodos.Visible = false;
                    }
                }
            }
        }

        protected void lnkRealacionas_Click(object sender, EventArgs e)
        {
            string value = ddlPuestorevisa.SelectedValue;
            ddlPrepara.SelectedValue = value;
            ddlEntrega.SelectedValue = value;
        }
    }
}