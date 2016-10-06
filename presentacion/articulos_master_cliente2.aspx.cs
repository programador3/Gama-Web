using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class articulos_master_cliente2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["dt_art_nuevos"] = null;
                cargar_tipos();
                int idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
                cargar_productos_master_cliente(idc_cliente);
                txtidc_cliente.Value = idc_cliente.ToString();
            }
        }

        private void cargar_tipos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("tipo", typeof(string));
            dt.Columns.Add("idc_tipo", typeof(int));

            string[] tipo = {
                                "LINEA",
                                "NEGOCIADOS",
                                "PROPUESTOS",
                                "BLOQUEADOS"
                            };
            int[] idc_tipo = {
                                0,
                                1,
                                2,
                                3
                            };
            for (int i = 0; i <= 3; i++)
            {
                DataRow row_ = dt.NewRow();
                row_["tipo"] = tipo[i];
                row_["idc_tipo"] = idc_tipo[i];
                dt.Rows.Add(row_);
            }

            cbotipos.DataSource = dt;
            cbotipos.DataTextField = "tipo";
            cbotipos.DataValueField = "idc_tipo";
            cbotipos.DataBind();
        }

        private void cargar_productos_master_cliente(int idc_cliente)
        {
            AgentesENT entidad = new AgentesENT();
            entidad.Pidc_cliente = idc_cliente;
            AgentesCOM com = new AgentesCOM();
            DataSet ds = com.sp_datos_clientes_articulos(entidad);

            if (ds.Tables[1].Rows.Count > 0)
            {
                txtcliente.Text = ds.Tables[0].Rows[0]["nombre"].ToString();
                txtrfc.Text = ds.Tables[0].Rows[0]["rfccliente"].ToString();
                txtcve.Text = ds.Tables[0].Rows[0]["cveadi"].ToString();
            }
            else
            {
                return;
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                ds.Tables[2].Columns.Add("precio_cliente", typeof(decimal));
                ds.Tables[2].Columns.Add("precio_real", typeof(string));
                ds.Tables[2].Columns.Add("tipoc", typeof(string));
                ds.Tables[2].Columns.Add("idc_gpoart", typeof(int));
                ds.Tables[2].Columns.Add("grupo", typeof(string));
                ds.Tables[2].Columns.Add("dias", typeof(string));

                DataView dv_index = new DataView();
                dv_index = ds.Tables[2].Copy().DefaultView;
                dv_index.Sort = "idc_articulo";

                foreach (DataRow row_ in ds.Tables[1].Rows)
                {
                    int index = dv_index.Find(new object[] { row_["idc_articulo"] });
                    if (index >= 0)
                    {
                        var _with1 = ds.Tables[2].Rows[index];
                        _with1["precio_cliente"] = row_["precio_cliente"];
                        _with1["precio_real"] = (Convert.ToInt32(row_["precio_real"]) > 0 ? row_["precio_real"] : 0);
                        _with1["tipoc"] = row_["tipoc"];
                        _with1["idc_gpoart"] = row_["idc_gpoart"];
                        _with1["grupo"] = row_["grupo"];
                        _with1["dias"] = (Convert.ToInt32(row_["dias"]) > 0 ? row_["dias"] : "S/C");
                    }
                    else
                    {
                        DataRow row = default(DataRow);
                        row = ds.Tables[2].NewRow();
                        var _with2 = row;
                        _with2["precio_cliente"] = row_["precio_cliente"];
                        _with2["precio_real"] = (Convert.ToInt32(row_["precio_real"]) > 0 ? row_["precio_real"] : 0);
                        _with2["tipoc"] = row_["tipoc"];
                        _with2["idc_gpoart"] = row_["idc_gpoart"];
                        _with2["grupo"] = row_["grupo"];
                        _with2["dias"] = (Convert.ToInt32(row_["dias"]) > 0 ? row_["dias"] : "S/C");
                        _with2["desart"] = row_["desart"];
                        _with2["codigo"] = row_["codigo"];
                        _with2["um"] = row_["um"];
                        _with2["idc_articulo"] = row_["idc_articulo"];
                        ds.Tables[2].Rows.Add(row);
                    }
                }
            }
            else
            {
                return;
            }

            ds.Tables[2].Columns["tipoc"].SetOrdinal(4);
            ds.Tables[2].Columns["tipoc"].ColumnName = "Tipo";

            ds.Tables[2].Columns["precio_cliente"].SetOrdinal(5);
            ds.Tables[2].Columns["precio_cliente"].ColumnName = "Precio Actual";

            ds.Tables[2].Columns["dias"].SetOrdinal(6);
            ds.Tables[2].Columns["dias"].ColumnName = "U.C.";

            ds.Tables[2].Columns["precio_real"].SetOrdinal(7);
            ds.Tables[2].Columns["precio_real"].ColumnName = "Precio Real";

            ds.Tables[2].Columns["desart"].ColumnName = "Articulo";

            //Sacar los Grupos
            DataView dv = new DataView();
            dv = ds.Tables[2].DefaultView.ToTable(true, "idc_gpoart", "grupo").DefaultView;
            dv.RowFilter = "idc_gpoart > 0";

            //filtrar Solo los articulos que no tienen grupo
            DataView dv_nogroup = new DataView();
            dv_nogroup = ds.Tables[2].Copy().DefaultView;
            dv_nogroup.RowFilter = "idc_gpoart = 0";

            DataTable dt_nogroup = default(DataTable);
            dt_nogroup = dv_nogroup.ToTable();

            DataTable dt_row_group = default(DataTable);
            dt_row_group = ds.Tables[2].Clone();

            //Sumar Todos Cantidades x mes
            if (dv.ToTable().Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[2].Rows)
                {
                    int value = row[15] == null || row[15].ToString() == "" ? 0 : Convert.ToInt32(row[15]);
                    if (value > 0)
                    {
                        row[8] = ds.Tables[2].Compute("sum(" + ds.Tables[2].Columns[8].ColumnName.ToString() + ")", "idc_gpoart = " + value.ToString());
                        row[9] = ds.Tables[2].Compute("sum(" + ds.Tables[2].Columns[9].ColumnName.ToString() + ")", "idc_gpoart = " + value.ToString());
                        row[10] = ds.Tables[2].Compute("sum(" + ds.Tables[2].Columns[10].ColumnName.ToString() + ")", "idc_gpoart = " + value.ToString());
                        row[11] = ds.Tables[2].Compute("sum(" + ds.Tables[2].Columns[11].ColumnName.ToString() + ")", "idc_gpoart = " + value.ToString());
                        row[12] = ds.Tables[2].Compute("sum(" + ds.Tables[2].Columns[12].ColumnName.ToString() + ")", "idc_gpoart = " + value.ToString());
                        row[13] = ds.Tables[2].Compute("sum(" + ds.Tables[2].Columns[13].ColumnName.ToString() + ")", "idc_gpoart = " + value.ToString());
                    }
                }
            }

            //Filtrar los articulos que pertenecen a un grupo
            DataView dv_group = new DataView();
            dv_group = ds.Tables[2].Copy().DefaultView;
            dv_group.RowFilter = "idc_gpoart > 0";

            DataRow rowx = default(DataRow);
            foreach (DataRow row1 in dv.ToTable().Rows)
            {
                rowx = dt_row_group.NewRow();
                foreach (DataRow row2 in dv_group.ToTable().Rows)
                {
                    if ((row1["idc_gpoart"].ToString() == row2["idc_gpoart"].ToString()))
                    {
                        //For i As Integer = 0 To row2.ItemArray.Length - 1
                        //    rowx.Item(i) = row2.Item(i)
                        //Next
                        rowx = row2;
                        rowx["Articulo"] = row2["grupo"];
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if ((rowx != null))
                {
                    dt_row_group.ImportRow(rowx);
                }
            }

            dt_nogroup.Merge(dt_row_group);

            //Borrar Columna innecesarias
            dt_nogroup.Columns.RemoveAt(14);
            dt_nogroup.Columns.RemoveAt(15);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dt_nogroup.Rows)
                {
                    for (int i = 8; i <= 13; i++)
                    {
                        object value = row[i];
                        if (value != DBNull.Value)
                        {
                            if (row[i].ToString() == row[i].ToString())
                            {
                                row[i] = Convert.ToInt32(row[i]);
                            }
                        }
                    }
                }
                gridproductosmaster.DataSource = dt_nogroup;
                gridproductosmaster.DataBind();
            }
            else
            {
                gridproductosmaster.DataSource = null;
                gridproductosmaster.DataBind();
            }

            if (ds.Tables[4].Rows.Count > 0)
            {
                grdmeses.DataSource = ds.Tables[4];
                grdmeses.DataBind();
            }
            else
            {
                grdmeses.DataSource = null;
                grdmeses.DataBind();
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("tareas_clientes_cap.aspx?idc_cliente=" + funciones.deTextoa64(Convert.ToInt32(Session["idc_cliente"]).ToString()));
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ficha_cliente_m.aspx");
        }

        protected void gridproductosmaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes["class"] = "Ocultar";
                e.Row.Cells[14].Attributes["class"] = "Ocultar";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes["class"] = "Ocultar";
                e.Row.Cells[14].Attributes["class"] = "Ocultar";

                if (e.Row.Cells[4].Text.ToLower() == "linea")
                {
                    e.Row.Cells[1].Attributes["class"] = "linea text-align-left";
                    e.Row.Cells[2].Attributes["class"] = "linea text-align-left";
                    e.Row.Cells[3].Attributes["class"] = "linea text-align-left";
                    e.Row.Cells[4].Attributes["class"] = "linea text-align-left";
                }
                else if (e.Row.Cells[4].Text.ToLower() == "negociados")
                {
                    e.Row.Cells[1].Attributes["class"] = "negociados text-align-left";
                    e.Row.Cells[2].Attributes["class"] = "negociados text-align-left";
                    e.Row.Cells[3].Attributes["class"] = "negociados text-align-left";
                    e.Row.Cells[4].Attributes["class"] = "negociados text-align-left";
                }
                else if (e.Row.Cells[4].Text.ToLower() == "propuestos")
                {
                    e.Row.Cells[1].Attributes["class"] = "propuestos text-align-left";
                    e.Row.Cells[2].Attributes["class"] = "propuestos text-align-left";
                    e.Row.Cells[3].Attributes["class"] = "propuestos text-align-left";
                    e.Row.Cells[4].Attributes["class"] = "propuestos text-align-left";
                }

                if (!(e.Row.Cells[14].Text.ToLower() == "0"))
                {
                    e.Row.Cells[2].Attributes["class"] = e.Row.Cells[2].Attributes["class"] + " grupo";
                }

                e.Row.Cells[1].Attributes["class"] = e.Row.Cells[1].Attributes["class"] + " text-align-left";
                e.Row.Cells[2].Attributes["class"] = e.Row.Cells[2].Attributes["class"] + " text-align-left";
                e.Row.Cells[3].Attributes["class"] = e.Row.Cells[3].Attributes["class"] + " text-align-left";
                e.Row.Cells[4].Attributes["class"] = e.Row.Cells[4].Attributes["class"] + " text-align-left";

                e.Row.Cells[13].Attributes["m"] = "6";
                e.Row.Cells[12].Attributes["m"] = "5";
                e.Row.Cells[11].Attributes["m"] = "4";
                e.Row.Cells[10].Attributes["m"] = "3";
                e.Row.Cells[9].Attributes["m"] = "2";
                e.Row.Cells[8].Attributes["m"] = "1";

                e.Row.Cells[13].Attributes["gpo"] = e.Row.Cells[14].Text;
                e.Row.Cells[12].Attributes["gpo"] = e.Row.Cells[14].Text;
                e.Row.Cells[11].Attributes["gpo"] = e.Row.Cells[14].Text;
                e.Row.Cells[10].Attributes["gpo"] = e.Row.Cells[14].Text;
                e.Row.Cells[9].Attributes["gpo"] = e.Row.Cells[14].Text;
                e.Row.Cells[8].Attributes["gpo"] = e.Row.Cells[14].Text;

                e.Row.Cells[13].Attributes["id"] = e.Row.Cells[0].Text;
                e.Row.Cells[12].Attributes["id"] = e.Row.Cells[0].Text;
                e.Row.Cells[11].Attributes["id"] = e.Row.Cells[0].Text;
                e.Row.Cells[10].Attributes["id"] = e.Row.Cells[0].Text;
                e.Row.Cells[9].Attributes["id"] = e.Row.Cells[0].Text;
                e.Row.Cells[8].Attributes["id"] = e.Row.Cells[0].Text;

                e.Row.Cells[13].Attributes["t"] = "1";
                e.Row.Cells[12].Attributes["t"] = "1";
                e.Row.Cells[11].Attributes["t"] = "1";
                e.Row.Cells[10].Attributes["t"] = "1";
                e.Row.Cells[9].Attributes["t"] = "1";
                e.Row.Cells[8].Attributes["t"] = "1";
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            tablediv.Visible = false;
            divadd.Visible = true;
            LinkButton3.Visible = false;
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            tablediv.Visible = true;
            divadd.Visible = false;
            LinkButton3.Visible = true;
        }

        protected void txtbuscararticulo_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtbuscararticulo.Text;
            int idc_cliente = 0;
            idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
            cargar_nuevos_articulos(idc_cliente, filtro);
            aded.Visible = true;
        }

        public void cargar_nuevos_articulos(int idc_cliente, string filtro)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                entidad.Pidc_cliente = idc_cliente;
                entidad.Pnombrepc = filtro;
                DataSet ds = com.sp_buscar_articulos_sencillo_UNIMED(filtro);
                DataTable dt = ds.Tables[0];
                Session["dt_art_nuevos"] = dt;
                cbarticulos.DataValueField = "idc_articulo";
                cbarticulos.DataTextField = "desart";
                cbarticulos.DataSource = dt;
                cbarticulos.DataBind();
                if (dt.Rows.Count > 0)
                {
                    string id = dt.Rows[0]["idc_articulo"].ToString();
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "idc_articulo = " + id + "";
                    if (dv.ToTable().Rows.Count > 0)
                    {
                        DataRow row = dv.ToTable().Rows[0];
                        txtum.Text = row["unimed"].ToString();
                        txtcodigo.Text = row["codigo"].ToString();
                        txtfechacompromiso.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                }
                else
                {
                    Alert.ShowAlertError("No se encontraron resultados", this.Page);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void btnaddarticulo_Click(object sender, EventArgs e)
        {
            int id = 0;
            id = string.IsNullOrEmpty(cbarticulos.SelectedValue) ? 0 : Convert.ToInt32(cbarticulos.SelectedValue);
            if (id == 0)
            {
                Alert.ShowAlertError("Seleccione un valor valido", this);
            }
            else
            {
                DataTable dt = Session["dt_art_nuevos"] as DataTable;
                DataView dv = dt.DefaultView;
                dv.RowFilter = "idc_articulo = " + id + "";
                if (dv.ToTable().Rows.Count > 0)
                {
                    DataRow row = dv.ToTable().Rows[0];
                    txtum.Text = row["unimed"].ToString();
                    txtcodigo.Text = row["codigo"].ToString();
                    txtfechacompromiso.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            int id = 0;
            id = string.IsNullOrEmpty(cbarticulos.SelectedValue) ? 0 : Convert.ToInt32(cbarticulos.SelectedValue);
            if (id == 0)
            {
                Alert.ShowAlertError("Seleccione un Articulo", this);
            }
            else if (txtfechacompromiso.Text == "")
            {
                Alert.ShowAlertError("Seleccione una Fecha Compromiso", this);
            }
            else if (Convert.ToDateTime(txtfechacompromiso.Text) < DateTime.Today)
            {
                Alert.ShowAlertError("Seleccione una Fecha Compromiso Mayor a Hoy", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                string mensjae = "¿ Desea agregar el Articulo " + cbarticulos.SelectedItem + " con el tipo " + cbotipos.SelectedItem + "?";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','" + mensjae + "','modal fade modal-info');", true);
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                switch (caso)
                {
                    case "Guardar":
                        AgentesENT entidad = new AgentesENT();
                        AgentesCOM com = new AgentesCOM();
                        entidad.Pidc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
                        entidad.Pidc_actiage = Convert.ToInt32(cbarticulos.SelectedValue);
                        entidad.pfecha = Convert.ToDateTime(txtfechacompromiso.Text);
                        entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                        entidad.popcion = "A";
                        entidad.ptipo = Convert.ToInt32(cbotipos.SelectedValue);
                        DataSet ds = com.sp_aclientes_articulos_nuevo(entidad);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos procesando la solitud.", "Espere un Momento", "articulos_master_cliente2.aspx?idc_cliente=" +Request.QueryString["idc_cliente"], "imagenes/loading.gif", "1000", "El Articulo fue agregado de manera correcta", this);
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
    }
}