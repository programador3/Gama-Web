using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class cuestionarios_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString[""] == null)
            {
                Session["tabla_preguntas"] = null;
                Session["tabla_respuestas"] = null;
                Session["idc_pregunta"] = null;
                Session["idc_respuesta"] = null;
                Session["nuevo_pregunta"] = null;
                Session["idc_cuestionario"] = null;
                CreateTablePreguntas();
                CreateTableRespuestas();
                CargarComboTipo();
                CargarComboTipo_Cuestionario();
                txtorden.Text = (gridPreguntas.Rows.Count + 1).ToString();
            }
            if (!IsPostBack && Request.QueryString["idc_cuestionario"] != null)
            {
                Session["idc_cuestionario"] = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cuestionario"]));
                Session["tabla_preguntas"] = null;
                Session["tabla_respuestas"] = null;
                Session["idc_pregunta"] = null;
                Session["idc_respuesta"] = null;
                Session["nuevo_pregunta"] = null;
                CreateTablePreguntas();
                CreateTableRespuestas();
                CargarComboTipo();
                CargarComboTipo_Cuestionario();
                CargarDatosEdit();
                gridPreguntas.DataSource = filtragriddinamico();
                gridPreguntas.DataBind();
                txtorden.Text = (gridPreguntas.Rows.Count + 1).ToString();
            }
            gridPreguntas.DataSource = filtragriddinamico();
            gridPreguntas.DataBind();
        }

        private void CargarDatosEdit()
        {
            CuestionariosENT entidad = new CuestionariosENT();
            entidad.Pidc_cuestionario = Convert.ToInt32(Session["idc_cuestionario"]);
            CuestionariosCOM comp = new CuestionariosCOM();
            DataSet ds = comp.CargarDatosEditar(entidad);
            txtnombrecuestiuonario.Text = ds.Tables[0].Rows[0]["descripcion"].ToString();
            ddltipo_cuestionario.SelectedValue = ds.Tables[0].Rows[0]["idc_cuestionario_tipo"].ToString();
            //tabla_preguntas
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                AddTablePreguntas(row["idc_pregunta"].ToString(), row["descripcion"].ToString(), Convert.ToInt32(row["idc_tipopregunta"].ToString()), row["tipo"].ToString(), 0, Convert.ToInt32(row["orden"].ToString()), row["tipo_entrada"].ToString(), Convert.ToBoolean(row["primer_visita"]) == true ? 1 : 0);
            }
            //tabla_respuestas
            foreach (DataRow row in ds.Tables[2].Rows)
            {
                int t = Convert.ToInt32(row["idc_respuesta"]);
                AddTableRespuestas(row["idc_pregunta"].ToString(), row["idc_respuesta"].ToString(), row["descripcion"].ToString(), 0, row["grupo"].ToString(), Convert.ToBoolean(row["entrada"]), row["nombre_entrada"].ToString(), row["tipo_entrada"].ToString());
            }
        }

        /// <summary>
        /// Carga el tipo de preguntas en un combo
        /// </summary>
        private void CargarComboTipo()
        {
            CuestionariosENT entidad = new CuestionariosENT();
            CuestionariosCOM comp = new CuestionariosCOM();
            DataSet ds = comp.CargaTipoPreguntas(entidad);
           
            ddlTipoPregunta.DataTextField = "descripcion";
            ddlTipoPregunta.DataValueField = "idc_tipopregunta";
            ddlTipoPregunta.DataSource = funciones.FiltrarDataTable(ds.Tables[0], "tipo <> 'S' and tipo <> 'T'");
            ddlTipoPregunta.DataBind();
            ddlTipoPregunta.Items.Insert(0, new ListItem("Seleccione uno", "0"));
            Session["tipo_respuestas"] = ds.Tables[0];
        }

        /// <summary>
        /// Carga el tipo de preguntas en un combo
        /// </summary>
        private void CargarComboTipo_Cuestionario()
        {
            CuestionariosENT entidad = new CuestionariosENT();
            CuestionariosCOM comp = new CuestionariosCOM();
            DataSet ds = comp.CargaTipoCuestionarios(entidad);
            ddltipo_cuestionario.DataTextField = "tipo";
            ddltipo_cuestionario.DataValueField = "idc_cuestionario_tipo";
            ddltipo_cuestionario.DataSource = ds.Tables[0];
            ddltipo_cuestionario.DataBind();
            ddltipo_cuestionario.Items.Insert(0, new ListItem("Seleccione uno", "0"));
        }

        /// <summary>
        /// Carga las rerspuestas de una pregunta
        /// </summary>
        /// <param name="idc_pregunta"></param>
        protected void FiltrarRespuestas(string idc_pregunta)
        {
            DataTable tabla_respuestas = (DataTable)Session["tabla_respuestas"];
            DataView dv = tabla_respuestas.DefaultView;
            dv.RowFilter = "idc_pregunta=" + idc_pregunta.ToString();
            gridrespuestas.DataSource = dv.ToTable();
            gridrespuestas.DataBind();
        }

        /// <summary>
        /// Inicia la tabla de preguntas
        /// </summary>
        private void CreateTablePreguntas()
        {
            DataTable tabla_preguntas = new DataTable();
            tabla_preguntas.Columns.Add("idc_pregunta");
            tabla_preguntas.Columns.Add("descripcion");
            tabla_preguntas.Columns.Add("tipo");
            tabla_preguntas.Columns.Add("idc_tipopregunta");
            tabla_preguntas.Columns.Add("nuevo");
            tabla_preguntas.Columns.Add("orden");
            tabla_preguntas.Columns.Add("tipo_entrada");
            tabla_preguntas.Columns.Add("primer_visita");
            Session["tabla_preguntas"] = tabla_preguntas;
        }

        /// <summary>
        /// Inicia la tabla de respuestas
        /// </summary>
        private void CreateTableRespuestas()
        {
            DataTable tabla_respuestas = new DataTable();
            tabla_respuestas.Columns.Add("idc_pregunta");
            tabla_respuestas.Columns.Add("idc_respuesta");
            tabla_respuestas.Columns.Add("descripcion");
            tabla_respuestas.Columns.Add("nuevo");
            tabla_respuestas.Columns.Add("grupo");
            tabla_respuestas.Columns.Add("entrada");
            tabla_respuestas.Columns.Add("nombre_entrada");
            tabla_respuestas.Columns.Add("tipo_entrada");
            Session["tabla_respuestas"] = tabla_respuestas;
        }

        /// <summary>
        /// Agrega fila a la tabla de respuestas
        /// </summary>
        /// <param name="idc_pregunta"></param>
        /// <param name="idc_respuesta"></param>
        /// <param name="descripcion"></param>
        /// <param name="nuevo"></param>
        private void AddTableRespuestas(string idc_pregunta, string idc_respuesta, string descripcion, int nuevo, string grupo, bool entrada, string nombre_entrada, string tipo_entrada)
        {
            DataTable tabla_preguntas = (DataTable)Session["tabla_respuestas"];
            DataRow row_new = tabla_preguntas.NewRow();
            row_new["idc_pregunta"] = idc_pregunta;
            row_new["descripcion"] = descripcion;
            row_new["idc_respuesta"] = idc_respuesta;
            row_new["nuevo"] = nuevo;
            row_new["grupo"] = grupo;
            row_new["entrada"] = entrada;
            row_new["nombre_entrada"] = nombre_entrada;
            row_new["tipo_entrada"] = tipo_entrada;
            tabla_preguntas.Rows.Add(row_new);
            Session["tabla_respuestas"] = tabla_preguntas;
            Session["idc_respuesta"] = null;
        }

        /// <summary>
        /// Agrega filas a la tabla preguntas
        /// </summary>
        /// <param name="idc_pregunta"></param>
        /// <param name="descripcion"></param>
        private void AddTablePreguntas(string idc_pregunta, string descripcion, int tipo, string tipo_char, int nuevo, int orden, string tipo_entrada, int primer_visita)
        {
            DataTable tabla_preguntas = (DataTable)Session["tabla_preguntas"];
            DataRow row_new = tabla_preguntas.NewRow();
            row_new["idc_pregunta"] = idc_pregunta;
            row_new["descripcion"] = descripcion;
            row_new["idc_tipopregunta"] = tipo;
            row_new["tipo"] = tipo_char;
            row_new["nuevo"] = nuevo;
            row_new["orden"] = orden;
            row_new["tipo_entrada"] = tipo_entrada;
            row_new["primer_visita"] = primer_visita;
            tabla_preguntas.Rows.Add(row_new);
            foreach (DataRow row in tabla_preguntas.Rows)
            {
                if (Convert.ToInt32(row["orden"].ToString()) >= orden && row["descripcion"].ToString() != descripcion)
                {
                    row["orden"] = (Convert.ToInt32(row["orden"].ToString()) + 1).ToString();
                }
            }
            Session["tabla_preguntas"] = tabla_preguntas;
            Session["idc_pregunta"] = null;
            CambiarOrden();
        }

        private void CambiarOrden()
        {
            DataTable tabla_preguntas = (DataTable)Session["tabla_preguntas"];
            DataView dt_libre;
            dt_libre = tabla_preguntas.DefaultView;
            dt_libre.RowFilter = "nuevo = 1 or nuevo =0";
            DataTable tabletemp = new DataTable();
            tabletemp = dt_libre.ToTable();
            tabletemp.DefaultView.Sort = "orden";
            tabletemp = tabletemp.DefaultView.ToTable();
            foreach (DataRow row_change in tabletemp.Rows)
            {
                int indexd = tabletemp.Rows.IndexOf(row_change);
                row_change["orden"] = indexd + 1;
            }
            Session["tabla_preguntas"] = tabletemp;
        }

        /// <summary>
        /// Regresa Cdaena de Preguntas
        /// </summary>
        /// <returns></returns>
        private string CadenaPreguntas()
        {
            string cadena = "";
            DataTable tabla_preguntas = (DataTable)Session["tabla_preguntas"];
            foreach (DataRow row in tabla_preguntas.Rows)
            {
                cadena = cadena + row["idc_pregunta"] + ";" + row["descripcion"] + ";" + row["idc_tipopregunta"] + ";" + row["nuevo"] + ";" + row["orden"] + ";" + row["tipo_entrada"] + ";" + row["primer_visita"] + ";";
            }
            return cadena;
        }

        /// <summary>
        /// Retorna el total de la cadena preguntas
        /// </summary>
        /// <returns></returns>
        private int TotalCadenaPreguntas()
        {
            DataTable tabla_preguntas = (DataTable)Session["tabla_preguntas"];
            return tabla_preguntas.Rows.Count;
        }

        /// <summary>
        /// Regresa Cdaena de Preguntas
        /// </summary>
        /// <returns></returns>
        private string CadenaRespuestas()
        {
            string cadena = "";
            DataTable tabla_respuestas = (DataTable)Session["tabla_respuestas"];
            foreach (DataRow row in tabla_respuestas.Rows)
            {
                cadena = cadena + row["idc_pregunta"] + ";" + row["idc_respuesta"] + ";" + row["descripcion"] + ";" + row["nuevo"] + ";" + row["grupo"] + ";" + row["entrada"] + ";" + row["nombre_entrada"] + ";" + row["tipo_entrada"] + ";";
            }
            return cadena;
        }

        /// <summary>
        /// Retorna el total de la cadena preguntas
        /// </summary>
        /// <returns></returns>
        private int TotalCadenaRespuestas()
        {
            DataTable tabla_respuestas = (DataTable)Session["tabla_respuestas"];
            return tabla_respuestas.Rows.Count;
        }

        /// <summary>
        /// Elimina de la tabla indicada, todas las filas que se relacionen con el valor value_compara
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <param name="value_compara"></param>
        private void DeleteTable(string table, string column, string value_compara)
        {
            DataTable tabla = (DataTable)(Session[table]);
            List<DataRow> rowsToDelete = new List<DataRow>();
            foreach (DataRow row in tabla.Rows)
            {
                if (row[column].ToString().Equals(value_compara))
                {
                    rowsToDelete.Add(row);
                }
            }
            foreach (DataRow rowde in rowsToDelete)
            {
                tabla.Rows.Remove(rowde);
            }
            Session[table] = tabla;
        }

        /// <summary>
        /// metodo que regresa el id mas alto del datatable
        /// </summary>
        /// <returns></returns>
        protected int consecutivo(string nomtabla, string primarykey)
        {
            DataTable tabla = (DataTable)(Session[nomtabla]);
            tabla.DefaultView.Sort = primarykey + " asc";
            int v = tabla.Rows.Count == 0 ? 0 : Convert.ToInt32(tabla.Rows[tabla.Rows.Count - 1][primarykey]);
            return v + 1;
        }

        protected DataTable filtragriddinamico()
        {
            DataTable tabla_preguntas = (DataTable)Session["tabla_preguntas"];
            DataView dt_libre;
            dt_libre = tabla_preguntas.DefaultView;
            dt_libre.RowFilter = "nuevo = 1 or nuevo =0";
            DataTable tabletemp = new DataTable();
            tabletemp = dt_libre.ToTable();
            tabletemp.DefaultView.Sort = "orden";
            tabletemp = tabletemp.DefaultView.ToTable();
            return tabletemp;
        }

        protected void btnAgregarPregunta_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (txtpregunta.Text == "")
            {
                Alert.ShowAlertError("Debe Ingresar El Contenido de la pregunta o Script", this);
                error = true;
            }
            if (ddlTipoPregunta.SelectedValue == "0")
            {
                Alert.ShowAlertError("Seleccione un tipo de pregunta valido", this);
                error = true;
            }
            if (ddltipoentrada.SelectedValue == "0" && panel_entradalibre.Visible == true)
            {
                Alert.ShowAlertError("Seleccione un tipo de entrada", this);
                error = true;
            }
            if (error == false)
            {
                string idc_pregunta = Session["idc_pregunta"] != null && Panel_Respuestas.Visible == false ? (string)Session["idc_pregunta"] : consecutivo("tabla_preguntas", "idc_pregunta").ToString();
                int nuevo = Session["nuevo_pregunta"] == null && btnAgregarPregunta.Text == "Agregar" ? 1 : Convert.ToInt32(Session["nuevo_pregunta"]);
                int orden = txtorden.Text == "" || txtorden.Text == "0" ? gridPreguntas.Rows.Count + 1 : Convert.ToInt32(txtorden.Text);
                DeleteTable("tabla_preguntas", "idc_pregunta", idc_pregunta);
                AddTablePreguntas(idc_pregunta, txtpregunta.Text.ToUpper(), Convert.ToInt32(ddlTipoPregunta.SelectedValue), ddlTipoPregunta.SelectedItem.ToString(), nuevo, orden, panel_entradalibre.Visible == true ? ddltipoentrada.SelectedValue : "", 0);
                txtpregunta.Text = "";
                gridPreguntas.DataSource = filtragriddinamico();
                gridPreguntas.DataBind();
                Panel_Respuestas.Visible = false;
                Session["idc_pregunta"] = null;
                Session["nuevo_pregunta"] = null;
                ddlTipoPregunta.SelectedValue = "0";
                panel_entradalibre.Visible = false;
                txtorden.Text = (gridPreguntas.Rows.Count + 1).ToString();
                Session["idc_pregunta"] = null;
                btnCancelarPreg.Visible = false;
                btnAgregarPregunta.Text = "Agregar";
            }
        }

        protected void gridPreguntas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_pregunta"] = gridPreguntas.DataKeys[index].Values["idc_pregunta"].ToString();
            Session["nuevo_pregunta"] = gridPreguntas.DataKeys[index].Values["nuevo"].ToString();
            switch (e.CommandName)
            {
                case "Editar":
                    txtpregunta.Text = gridPreguntas.DataKeys[index].Values["descripcion"].ToString();
                    txtorden.Text = gridPreguntas.DataKeys[index].Values["orden"].ToString();
                    string p = gridPreguntas.DataKeys[index].Values["primer_visita"].ToString();
                    // bool ps = Convert.ToBoolean(gridPreguntas.DataKeys[index].Values["primer_visita"]);
                    ddltipoentrada.SelectedValue = gridPreguntas.DataKeys[index].Values["tipo_entrada"].ToString() == "" ? "0" : gridPreguntas.DataKeys[index].Values["tipo_entrada"].ToString();
                    ddlTipoPregunta.SelectedValue = gridPreguntas.DataKeys[index].Values["idc_tipopregunta"].ToString();
                    panel_entradalibre.Visible = gridPreguntas.DataKeys[index].Values["tipo_entrada"].ToString() == "" ? false : true;
                    btnAgregarPregunta.Text = "Editar";
                    btnCancelarPreg.Visible = true;
                    CambiarOrden();
                    gridPreguntas.DataSource = filtragriddinamico();
                    gridPreguntas.DataBind();

                    break;

                case "Eliminar":
                    DeleteTable("tabla_preguntas", "idc_pregunta", gridPreguntas.DataKeys[index].Values["idc_pregunta"].ToString());
                    DeleteTable("tabla_respuestas", "idc_pregunta", gridPreguntas.DataKeys[index].Values["idc_pregunta"].ToString());
                    gridrespuestas.DataSource = (DataTable)Session["tabla_respuestas"];
                    gridrespuestas.DataBind();
                    CambiarOrden();
                    gridPreguntas.DataSource = filtragriddinamico();
                    gridPreguntas.DataBind();
                    txtorden.Text = (gridPreguntas.Rows.Count + 1).ToString();
                    Panel_Respuestas.Visible = false;
                    break;

                case "Respuestas":
                    lblpregunta.Text = gridPreguntas.DataKeys[index].Values["descripcion"].ToString();
                    FiltrarRespuestas(gridPreguntas.DataKeys[index].Values["idc_pregunta"].ToString());
                    Panel_Respuestas.Visible = true;
                    CambiarOrden();
                    gridPreguntas.DataSource = filtragriddinamico();
                    gridPreguntas.DataBind();
                    btnAgreagrRespuesta.Text = "Agregar";
                    txtgrupo.Text = "";
                    cbxRespuestaEntrada.Checked = false;
                    txtnombre_entrada.Text = "";
                    ddlTipoEntradRespuesta.SelectedValue = "0";
                    btnCancelar_respuesta.Visible = false;
                    break;
            }
        }

        protected void btnAgreagrRespuesta_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (txtrespuesta.Text == "")
            {
                Alert.ShowAlertError("Debe Ingresar El Contenido de la Respuesta", this);
                error = true;
            }
            if (cbxRespuestaEntrada.Checked == true && ddlTipoEntradRespuesta.SelectedValue == "0")
            {
                Alert.ShowAlertError("Debe Ingresar El Tipo de Entrada para la Respuesta", this);
                error = true;
            }
            if (error == false)
            {
                string idc_respuesta = Session["idc_respuesta"] == null && btnAgreagrRespuesta.Text == "Agregar" ? consecutivo("tabla_respuestas", "idc_respuesta").ToString() : (string)Session["idc_respuesta"];
                int nuevo = Session["nuevo_respuesta"] == null ? 1 : Convert.ToInt32(Session["nuevo_respuesta"]);
                DeleteTable("tabla_respuestas", "idc_respuesta", idc_respuesta);
                AddTableRespuestas((string)Session["idc_pregunta"], idc_respuesta, txtrespuesta.Text.ToUpper(), nuevo, txtgrupo.Text, cbxRespuestaEntrada.Checked, txtnombre_entrada.Text, ddlTipoEntradRespuesta.SelectedValue);
                txtrespuesta.Text = "";
                gridrespuestas.DataSource = (DataTable)Session["tabla_respuestas"];
                gridrespuestas.DataBind();
                btnAgreagrRespuesta.Text = "Agregar";
                txtgrupo.Text = "";
                cbxRespuestaEntrada.Checked = false;
                txtnombre_entrada.Text = "";
                ddlTipoEntradRespuesta.SelectedValue = "0";
                btnCancelar_respuesta.Visible = false;
                Session["idc_respuesta"] = null;
            }
        }

        protected void gridrespuestas_RowCommand(object sender, GridViewCommandEventArgs e)
        { //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_respuesta"] = gridrespuestas.DataKeys[index].Values["idc_respuesta"].ToString();
            Session["nuevo_respuesta"] = gridrespuestas.DataKeys[index].Values["nuevo"].ToString();
            switch (e.CommandName)
            {
                case "Editar":
                    txtrespuesta.Text = gridrespuestas.DataKeys[index].Values["descripcion"].ToString();
                    txtgrupo.Text = gridrespuestas.DataKeys[index].Values["grupo"].ToString();
                    ddlTipoEntradRespuesta.SelectedValue = gridrespuestas.DataKeys[index].Values["tipo_entrada"].ToString() == "" ? "0" : gridrespuestas.DataKeys[index].Values["tipo_entrada"].ToString();
                    txtnombre_entrada.Text = gridrespuestas.DataKeys[index].Values["nombre_entrada"].ToString();
                    cbxRespuestaEntrada.Checked = Convert.ToBoolean(gridrespuestas.DataKeys[index].Values["entrada"].ToString());
                    btnCancelar_respuesta.Visible = true;
                    btnAgreagrRespuesta.Text = "Editar";
                    break;

                case "Eliminar":
                    DeleteTable("tabla_respuestas", "idc_respuesta", gridrespuestas.DataKeys[index].Values["idc_respuesta"].ToString());
                    gridrespuestas.DataSource = (DataTable)Session["tabla_respuestas"];
                    gridrespuestas.DataBind();
                    break;
            }
        }

        protected void gridPreguntas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string tipo = rowView["idc_tipopregunta"].ToString();
                string idc_pregunta = rowView["idc_pregunta"].ToString();
                DataTable tipo_respuestas = (DataTable)Session["tipo_respuestas"];
                bool respuestas = false;
                foreach (DataRow row in tipo_respuestas.Rows)
                {
                    if (tipo == row["idc_tipopregunta"].ToString())
                    {
                        respuestas = Convert.ToBoolean(row["opciones_multiples"]);
                        break;
                    }
                }
                if (respuestas == false)
                {
                    e.Row.Cells[6].Controls.Clear();
                }
            }
        }

        protected void btncerrarrespuestas_Click(object sender, EventArgs e)
        {
            Panel_Respuestas.Visible = false;
            Session["idc_pregunta"] = null;
            Session["nuevo_pregunta"] = null;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (txtnombrecuestiuonario.Text == "")
            {
                Alert.ShowAlertError("Debe Ingresar El Nombre del Cuestionario", this);
                error = true;
            }
            if (TotalCadenaPreguntas() == 0)
            {
                Alert.ShowAlertError("Debe Ingresar Por lo Menos una Pregunta", this);
                error = true;
            }
            if (error == false)
            {
                Session["Caso_Confirmacion"] = Request.QueryString["idc_cuestionario"] == null ? "Guardar" : "Editar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar el Cuestionario " + txtnombrecuestiuonario.Text.ToUpper() + " y su contenido?','modal fade modal-info');", true);
                gridPreguntas.DataSource = filtragriddinamico();
                gridPreguntas.DataBind();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("catalogo_cuestionarios.aspx");
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso_confirm = (string)Session["Caso_Confirmacion"];
            try
            {
                CuestionariosENT entidad = new CuestionariosENT();
                CuestionariosCOM comp = new CuestionariosCOM();
                DataSet ds = new DataSet();
                string VMENSAJE = "";
                entidad.Pcadena_preguntas = CadenaPreguntas();
                entidad.Pcadena_respuestas = CadenaRespuestas();
                entidad.Ptotal_cadena_preguntas = TotalCadenaPreguntas();
                entidad.Ptotal_cadena_respuestas = TotalCadenaRespuestas();
                entidad.Pcuestionario = txtnombrecuestiuonario.Text.ToUpper();
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pidc_cuestionario_tipo = 5;
                switch (caso_confirm)
                {
                    case "Guardar":
                        ds = comp.AgregarCuestionario(entidad);
                        VMENSAJE = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Editar":
                        entidad.Pidc_cuestionario = Convert.ToInt32(Session["idc_cuestionario"]);
                        ds = comp.EditarCuestionario(entidad);
                        VMENSAJE = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;
                }
                if (VMENSAJE == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('El cuestionario " + txtnombrecuestiuonario.Text + " fue guardado correctamente.','catalogo_cuestionarios.aspx');", true);
                }
                else {
                    Alert.ShowAlertError(VMENSAJE, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
            gridPreguntas.DataSource = filtragriddinamico();
            gridPreguntas.DataBind();
        }

        protected void ddlTipoPregunta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoPregunta.SelectedValue == "")
            {
                Alert.ShowAlertError("Seleccion un tipo de Pregunta", this);
            }
            else {
                string tipo = ddlTipoPregunta.SelectedValue;
                DataTable tipo_respuestas = (DataTable)Session["tipo_respuestas"];
                bool respuestas = false;
                foreach (DataRow row in tipo_respuestas.Rows)
                {
                    if (tipo == row["idc_tipopregunta"].ToString())
                    {
                        respuestas = Convert.ToBoolean(row["respuesta_libre"]);
                        break;
                    }
                }
                panel_entradalibre.Visible = respuestas;
            }
        }

        protected void BTNCancelar_Click1(object sender, EventArgs e)
        {
            btnCancelarPreg.Visible = false;
            btnAgregarPregunta.Text = "Agregar";
            panel_entradalibre.Visible = false;
            txtpregunta.Text = "";
            CargarComboTipo();
            ddltipoentrada.SelectedValue = "0";
            gridPreguntas.DataSource = filtragriddinamico();
            gridPreguntas.DataBind();
            Session["idc_pregunta"] = null;
            Session["nuevo_pregunta"] = null;

            txtorden.Text = (gridPreguntas.Rows.Count + 1).ToString();
        }

        protected void btnCancelar_respuesta_Click(object sender, EventArgs e)
        {
            btnCancelar_respuesta.Visible = false;
            btnAgreagrRespuesta.Text = "Agregar";
            Session["idc_respuesta"] = null;
        }

        protected void ddltipo_cuestionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddltipo_cuestionario.SelectedValue == "")
            {
                Alert.ShowAlertError("Seleccion un tipo de Cuestionario", this);
            }
        }
    }
}