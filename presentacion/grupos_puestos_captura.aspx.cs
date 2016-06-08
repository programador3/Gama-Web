using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class grupos_puestos_captura : System.Web.UI.Page
    {
        private List<int> ds = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack && Request.QueryString["edit"] == null)//NUEVO
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_puesto");
                dt.Columns.Add("puesto");
                Session["tabla_puesto"] = dt;
                CargaGrid("");
            }
            if (!IsPostBack && Request.QueryString["edit"] != null)//EDICION
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_puesto");
                dt.Columns.Add("puesto");
                Session["tabla_puesto"] = dt;
                CargaGridEdicion();
            }
            //  lblSeleccionados.Text = TotalCadena().ToString();
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
            CheckValues();
            int count = ds.Tables[0].Rows.Count;
            lblContentRepeat.Text = count.ToString();
            btnDeseleccionarTodos.Visible = false;
            btnSeleccionarTodos.Visible = true;
        }

        /// <summary>
        /// Marca los valores ya obtenidos
        /// </summary>
        public void CargaGridEdicion()
        {
            Grupos_PuestosENT entidad = new Grupos_PuestosENT();
            Grupos_PuestosCOM componente = new Grupos_PuestosCOM();
            entidad.Idc_puesto_gpo = Convert.ToInt32(Session["idc_puesto_gpo_edit"].ToString());
            DataSet ds = componente.CargaGruposEditar(entidad);
            repeat_puestos.DataSource = ds.Tables[1];
            repeat_puestos.DataBind();
            txtNombre.Text = ds.Tables[0].Rows[0]["descripcion"].ToString();
            AddValuesforEdit(ds.Tables[1]);
            CheckValues();
            int count = ds.Tables[1].Rows.Count;
            lblContentRepeat.Text = count.ToString();
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
                    new_row["puesto"] = puesto;
                    dt.Rows.Add(new_row);
                }
            }
            int count = dt.Rows.Count;
            lblContentRepeat.Text = count.ToString();
            Session["tabla_puesto"] = dt;
        }

        /// <summary>
        /// Verifica si existe el valor de un item en la tabla de session y checkea
        /// </summary>
        private void CheckValues()
        {
            //verificamos si existe un registro en tabla
            DataTable dt = (DataTable)Session["tabla_puesto"];

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
        }

        /// <summary>
        /// Regresa Cadena con Idc_puesto seleccionados
        /// </summary>
        /// <returns></returns>
        public string CadenaPuestos()
        {
            string cadena = "";
            DataTable dt = (DataTable)Session["tabla_puesto"];
            foreach (DataRow row in dt.Rows)
            {
                cadena = cadena + row["idc_puesto"].ToString() + ";";
            }
            return cadena;
        }

        /// <summary>
        /// Regresa Total de Cadena de Puestos
        /// </summary>
        /// <returns></returns>
        public int TotalCadena()
        {
            int total = 0;
            DataTable dt = (DataTable)Session["tabla_puesto"];
            total = dt.Rows.Count;

            return total;
        }

        /// <summary>
        /// Agrega valor a tabla de session
        /// </summary>
        /// <param name="value"></param>
        private void AddTable(string value, string value_des)
        {
            DataTable dt = (DataTable)Session["tabla_puesto"];
            int ccount = dt.Rows.Count;
            DataRow new_row = dt.NewRow();
            new_row["idc_puesto"] = value;
            new_row["puesto"] = value_des;
            dt.Rows.Add(new_row);
            ccount = ccount + 1;
            Session["tabla_puesto"] = dt;
        }

        private int Count()
        {
            DataTable dt = (DataTable)Session["tabla_puesto"];
            int ccount = dt.Rows.Count;
            return ccount;
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
        private void DeleteValuefromTable(string value)
        {
            DataTable dt = (DataTable)Session["tabla_puesto"];
            foreach (DataRow row in dt.Rows)
            {
                if (row["idc_puesto"].ToString().Equals(value))
                {
                    row.Delete();
                    break;
                }
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
        /// Retorn si existe un error
        /// </summary>
        /// <returns></returns>
        public new bool Error()
        {
            bool error = false;
            lblErrorNombre.Visible = false;
            if (txtNombre.Text == "") { error = true; lblErrorNombre.Visible = true; }
            if (TotalCadena() == 0) { error = true; Alert.ShowAlertError("Debe seleccionar al menos 1 puesto para guardar.", this); }
            return error;
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            Grupos_PuestosENT entidad = new Grupos_PuestosENT();
            Grupos_PuestosCOM componente = new Grupos_PuestosCOM();
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
            entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
            entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
            entidad.Pusuariopc = funciones.GetUserName();//usuario pc
            DataSet ds = new DataSet();
            switch (Confirma_a)
            {
                case "Cancelar":
                    Response.Redirect("catalogo_grupos_puestos.aspx");
                    break;

                case "Guardar":
                    entidad.Descripcion = txtNombre.Text.ToUpper();
                    entidad.Num_cadena = Convert.ToInt32(Session["Total"].ToString());
                    entidad.CadenaPuestos = (string)Session["Cadena"];
                    ds = componente.AgregarGrupo(entidad);
                    break;

                case "Editar":
                    entidad.Descripcion = txtNombre.Text.ToUpper();
                    entidad.Idc_puesto_gpo = Convert.ToInt32(Session["idc_puesto_gpo_edit"].ToString());
                    entidad.Num_cadena = Convert.ToInt32(Session["Total"].ToString());
                    entidad.CadenaPuestos = (string)Session["Cadena"];
                    ds = componente.EditarGrupo(entidad);
                    break;
            }
            string mensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
            if (mensaje == "")//no hay errores retornamos true
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('El Grupo " + txtNombre.Text + " fue guardado correctamente.','catalogo_grupos_puestos.aspx');", true);
            }
            else
            {//mostramos error
                Alert.ShowAlertError(mensaje, this);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Alert.ShowAlert(CadenaPuestos()+"  total "+TotalCadena().ToString(),"",this);
            Session["Cadena"] = CadenaPuestos();
            Session["Total"] = TotalCadena();
            if (Error() == false && Request.QueryString["edit"] == null)//GUARDAR
            {
                Session["Caso_Confirmacion"] = "Guardar";
                string total = TotalCadena().ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea guardar este Grupo con un Total de " + total + " Puestos?');", true);
            }
            if (Error() == false && Request.QueryString["edit"] != null)//ACTUALIZAR
            {
                Session["Caso_Confirmacion"] = "Editar";
                string total = TotalCadena().ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Actualizar este Grupo con un Total de " + total + " Puestos?');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Cancelar";
            string total = TotalCadena().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Cancelar?');", true);
        }

        protected void btnSeleccionarTodos_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repeat_puestos.Items)
            {
                Label lblValue = (Label)item.FindControl("lblValue");
                CheckBox cbxCheck = (CheckBox)item.FindControl("cbxCheck");
                DeleteValuefromTable(lblValue.Text);
                AddTable(lblValue.Text, cbxCheck.Text);
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
                DeleteValuefromTable(lblValue.Text);
                cbxCheck.Checked = false;
            }
            btnSeleccionarTodos.Visible = true;
            btnDeseleccionarTodos.Visible = false;
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
                        DeleteValuefromTable(lblValue.Text);
                        AddTable(lblValue.Text, cbxCheck.Text);
                    }
                    else
                    {//si no esta seleccionado elimino
                        DeleteValuefromTable(lblValue.Text);
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
                        DeleteValuefromTable(lblValue.Text);
                        cbxCheck.Checked = false;
                    }
                    else
                    {//si no esta seleccionado elimino
                        DeleteValuefromTable(lblValue.Text);
                        AddTable(lblValue.Text, cbxCheck.Text);
                        cbxCheck.Checked = true;
                    }
                }
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            CargaGrid(txtFiltro.Text);
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
    }
}