/*
    Autor       : Humberto De la Rosa
    Date        : 23/07/2015
    Description : CODEBEHIND Logica Pagina Etiquetas
*/

using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //Tablas y Filas para Etiquetas
        //System.Data.DataTable tableglobal;
        //System.Data.DataRow rowgloblal;
        //Tablas y Filas para Opciones
        private System.Data.DataTable table;

        private System.Data.DataRow row;

        //Tablas y Filas para Bloquear Etiquetas con Opciones
        private System.Data.DataTable tablebloqueo;

        private System.Data.DataRow rowbloqueo;

        //Tablas y Filas para Etiquetas agregadas
        private System.Data.DataTable tableetiquetas;

        private System.Data.DataRow rowetiquetas;

        //Boleanos para comprobación
        private Boolean status = false;

        private Boolean statusbloqueo = false;
        private Boolean statusetiqueta = false;

        //REQUEST INICIALES
        public static string RequestMensaje = null;

        public static string RequestGrupo = null;
        public static int RequestID = 0;
        public static string NombreEtiquetaGloblal = null;
        public static int IndexUpdate = 0;

        private void Start()
        {
            ///funcion que inicializa datatable en session, se usa para cargar la pagina por primera vez y para reinciar con el boton cancelar
            //Tablas y Filas para Opciones
            table = new System.Data.DataTable();
            table.Columns.Add("idc_perfiletiq_opc", typeof(System.Int32));
            table.Columns.Add("Descripcion", typeof(System.String));
            table.Columns.Add("Nombre", typeof(System.String));
            table.Columns.Add("borrado", typeof(System.Boolean));
            table.Columns.Add("nuevo", typeof(System.Boolean));
            Session.Add("Tabla", table);
            //Tablas y Filas para Bloquear Etiquetas con Opciones
            tablebloqueo = new System.Data.DataTable();
            tablebloqueo.Columns.Add("idc_perfiletiq_bloq", typeof(System.Int32));
            tablebloqueo.Columns.Add("Nombre", typeof(System.String));
            tablebloqueo.Columns.Add("Descripcion", typeof(System.String));
            Session.Add("Tablabloqueo", tablebloqueo);
            //Tablas y Filas para Etiquetas
            tableetiquetas = new System.Data.DataTable();
            tableetiquetas.Columns.Add("idc_perfil", typeof(System.Int32));
            tableetiquetas.Columns.Add("Grupo", typeof(System.String));
            tableetiquetas.Columns.Add("Nombre", typeof(System.String));
            tableetiquetas.Columns.Add("libre", typeof(System.Int32));
            tableetiquetas.Columns.Add("Minimo_sel", typeof(System.String));
            tableetiquetas.Columns.Add("Maximo_sel", typeof(System.String));
            Session.Add("TablaEtiquetas", tableetiquetas);

            txtNombreEtiqueta.Focus();
            getCheckVisible(rbtnTipoEntrada, Etiquetalibre, EtiquetaOpciones);
        }

        private void ControlAddHtml()
        {
            txtMaximoLibre.Attributes.Add("OnFocus", "LimpiarTexto(this);");
            txtMaximoOpciones.Attributes.Add("OnFocus", "LimpiarTexto(this);");
            txtMinimoOpciones.Attributes.Add("OnFocus", "LimpiarTexto(this);");
            txtMinimoLibre.Attributes.Add("OnFocus", "LimpiarTexto(this);");
            getCheckVisible(rbtnTipoEntrada, Etiquetalibre, EtiquetaOpciones);
            getVisible(rblLibre, PanelEtiquetaLibre);
            getVisible(rblOpciones, PanelOpciones);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["opc_enedicion"] = false;
                Session["opc_name"] = null;
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                //int idc_opcion = 1799;  //pertenece al modulo de grupos backend
                //if (funciones.permiso(idc_usuario, idc_opcion) == false)
                //{
                //    Response.Redirect("menu.aspx");
                //    return;
                //}
                //SimplePropertyEntry la pagina no es OpstBack Inicializa las tablas en session con funcion Start
                if (Request.QueryString["grupo"] == null | Request.QueryString["id"] == null)
                {
                    //    Response.Redirect("grupos_backend.aspx");
                    Response.Redirect("grupos_backend.aspx");
                }
                else
                {
                    Start();
                    RequestGrupo = Request.QueryString["grupo"];
                    txtNombreGrupo.Text = RequestGrupo;
                    RequestID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    CargarTablaEtiquetas(Convert.ToInt32(Request.QueryString["id"].ToString()));
                    CargarTablaOpciones(Convert.ToInt32(Request.QueryString["id"].ToString()));
                    //CargarTablaBloqueo();
                    //metemos el id del gpo en un campo oculto
                    ocidcperfilgpo.Value = Request.QueryString["id"];
                    //el usuario tambn
                    ocidcusuario.Value = Session["sidc_usuario"].ToString();
                }
            }

            //Se le agrega a los textbox atributos de estilo html en lcada carga
            ControlAddHtml();
        }

        //FUNCION QUE FMUESTRA EN LA TABLA OPCIONE UN FILTRO SEGUN LA ETIQUETA
        //@Filtro Type: String   Valor del nombre de la etiqueta
        public void FiltroOpciones(String Filtro)
        {
            //BAJAMOS A TABLA TEMPORAL LA TABLA DE OPCIONES
            table = (System.Data.DataTable)(Session["Tabla"]);
            //filtramos y llenamos el GridView
            DataView dv = new DataView(table);
            //HACEMOS FILTRO
            dv.RowFilter = "Nombre = '" + Filtro + "' and borrado = 0";
            //CARGAMOS DATOS
            Session.Add("Tabla", table);
            gridOpciones.DataSource = dv;
            gridOpciones.DataBind();
        }

        ////////Funciones para insertar datos en Grid, con DataTable de SESSION
        //Tabla de Etiquetas
        public void getDataTableEtiquetas(Int32 Entrada, TextBox txtMinimo, TextBox txtMaximo)
        {
            try
            {
                //Igualo la tabla con tabla session
                tableetiquetas = (System.Data.DataTable)(Session["TablaEtiquetas"]);
                //Igualo parametro ROW CON tabla.row
                rowetiquetas = tableetiquetas.NewRow();
                //Inserto los datos en cada columna
                rowetiquetas["Grupo"] = txtNombreGrupo.Text;
                rowetiquetas["Nombre"] = txtNombreEtiqueta.Text;
                rowetiquetas["Libre"] = Entrada;
                rowetiquetas["Minimo_sel"] = txtMinimo.Text;
                rowetiquetas["Maximo_sel"] = txtMaximo.Text;
                //Funcion que comprueba que no se repita
                ComprobarTableEtiquetas(txtNombreEtiqueta.Text);
                //SI STATUS SIGUE EN FALSO
                if (statusetiqueta == false)
                {
                    //AGREGO LOS DATOS A LA TABLA EN TABLA SESSION
                    tableetiquetas.Rows.Add(rowetiquetas);
                    ////Agrego ETIQUETA A GRIDPRINCIPAL
                    TotalEtiquetas.DataSource = tableetiquetas;
                    TotalEtiquetas.DataBind();
                    //AGREGO LAS ETIQUETAS EN EL DROPDOWNLIST DE ETIQUETAS
                    dlEtiquetaBloqueada.Items.Add(txtNombreEtiqueta.Text);
                    dlEtiquetaBloqueada.DataBind();
                    //AGREGO EL DATATABLE A SESSION
                    Session.Add("TablaEtiquetas", tableetiquetas);
                    txtNombreEtiqueta.Text = "";
                }
            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message + "jccsdbcwjcs", this);
            }
        }

        //TablaOpcionesDeOpciones
        public void getDataTableOpciones()
        {
            try
            {
                //Igualo la tabla con tabla session
                table = (System.Data.DataTable)(Session["Tabla"]);
                if (Session["opc_enedicion"] != null && Convert.ToBoolean(Session["opc_enedicion"]) == false)
                {
                    //Igualo parametro ROW CON tabla.row
                    row = table.NewRow();
                    //Inserto los datos en cada columna
                    row["Descripcion"] = txtNombreOpcion.Text;
                    row["Nombre"] = txtNombreEtiqueta.Text;
                    row["borrado"] = 0;
                    row["nuevo"] = 1;
                    //Funcion que comprueba que no se repita
                    ComprobarTableOneColumn(txtNombreEtiqueta.Text, txtNombreOpcion.Text);
                    //SI STATUS SIGUE EN FALSO
                    if (status == false)
                    {
                        //AGREGO LOS DATOS A LA TABLA EN TABLA SESSION
                        table.Rows.Add(row);
                    }
                }
                else
                {
                    //Paso valores de columnas a variables
                    table = (System.Data.DataTable)(Session["Tabla"]);
                    foreach (DataRow roww in table.Rows)
                    {
                        if (roww["Descripcion"].ToString() == (string)Session["opc_name"] && roww["Nombre"].ToString() == txtNombreEtiqueta.Text)
                        {
                            roww["Descripcion"] = txtNombreOpcion.Text;
                            break;
                        }
                    }
                    Session["opc_enedicion"] = false;
                }
                gridOpciones.DataSource = table;
                dlEtiqueta.Items.Add(txtNombreOpcion.Text);
                dlEtiqueta.DataBind();
                //Agrego el datatable al GRID
                gridOpciones.DataBind();
                //AGREGO DATATABLE A SESSION
                Session.Add("Tabla", table);
                txtNombreOpcion.Text = "";
                FiltroOpciones(txtNombreEtiqueta.Text);
            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message, this);
            }
        }

        //TablaBloqueos
        public void getDataTableBloqueo()
        {
            try
            {
                //Igualo la tabla con tabla session
                tablebloqueo = (System.Data.DataTable)(Session["Tablabloqueo"]);
                //Igualo parametro ROW CON tabla.row
                rowbloqueo = tablebloqueo.NewRow();
                //Inserto los datos en cada columna
                rowbloqueo["Nombre"] = dlEtiquetaBloqueada.SelectedItem.ToString();
                rowbloqueo["Descripcion"] = dlEtiqueta.SelectedItem.ToString();
                //Funcion que comprueba que no se repita
                ComprobarTableTwoColum(dlEtiquetaBloqueada.SelectedItem.ToString(), dlEtiqueta.SelectedItem.ToString());
                //SI STATUS SIGQUE EN FALSO
                if (statusbloqueo == false)
                {
                    //AGREGO LOS DATOS A LA TABLA EN TABLA SESSION
                    tablebloqueo.Rows.Add(rowbloqueo);
                    //AGREGO LOS DATOS AL GRID
                    gridBloqueos.DataSource = tablebloqueo;
                    gridBloqueos.DataBind();
                    //AGREGO LA TABLA A SESSION
                    Session.Add("Tablabloqueo", tablebloqueo);
                    //REINCIA EL DROPDOWNLIST
                    dlEtiqueta.SelectedValue = "select";
                    dlEtiquetaBloqueada.SelectedValue = "select";
                }
            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message, this);
            }
        }

        //Función que comprueba si existe una row y la elimina
        //@Item: Type String. Nombre de la row a comprobar
        public void DeleteTablaBloqueos(String Item)
        {
            //Mando llamar la tabla desde SESSION
            tablebloqueo = (System.Data.DataTable)(Session["Tablabloqueo"]);
            //COMIENZA CICLO, RECORREINDO EL TOTAL DE LAS FILAS - 1

            for (int i = 1; i < tablebloqueo.Rows.Count; i++)
            {
                //IGUALO UNA VARIABLE ROW CON LA PROPIEDAD DE LA TABLA
                DataRow dr = tablebloqueo.Rows[i];
                //SI ENCUENTRO UNA IGUAL SE ELIMINA
                if (dr["Descripcion"].Equals(Item))
                {
                    //dr.Delete();
                    dr["borrado"] = 1;
                }
            }

            //INGRESO DATOS RESULTANTE DE A TABLA EN GRID
            gridBloqueos.DataSource = tablebloqueo;
            gridBloqueos.DataBind();
            //AGREGO LA TABLA A SESSION
            Session.Add("Tablabloqueo", tablebloqueo);
            //REINICIO DROPDOWNLIST
            dlEtiqueta.SelectedValue = "select";
            dlEtiquetaBloqueada.SelectedValue = "select";
        }

        //Función que comprueba si existe una row y la elimina
        //@Item: Type String. Nombre de la row a comprobar
        public void DeleteTablaBloqueosEtiqueta(String Item)
        {
            //Mando llamar la tabla desde SESSION
            tablebloqueo = (System.Data.DataTable)(Session["Tablabloqueo"]);
            //COMIENZA CICLO, RECORREINDO EL TOTAL DE LAS FILAS - 1

            for (int i = 0; i < tablebloqueo.Rows.Count; i++)
            {
                //IGUALO UNA VARIABLE ROW CON LA PROPIEDAD DE LA TABLA
                DataRow dr = tablebloqueo.Rows[i];
                //SI ENCUENTRO UNA IGUAL SE ELIMINA
                if (dr["Nombre"].Equals(Item))
                {
                    dr.Delete();
                }
            }
            //INGRESO DATOS RESULTANTE DE A TABLA EN GRID
            gridBloqueos.DataSource = tablebloqueo;
            gridBloqueos.DataBind();
            //AGREGO LA TABLA A SESSION
            Session.Add("Tablabloqueo", tablebloqueo);
            //REINICIO DROPDOWNLIST
            dlEtiqueta.SelectedValue = "select";
            dlEtiquetaBloqueada.SelectedValue = "select";
        }

        /*
        * ComprobarTableOneColum
        * Ciclo que toma el valores de una columna de la tabla, y la compara con un parametros
        * Parametros @Item1:  String
        * Si ambos valores son iguales, para el ciclo(BREAK) y muestra un mensaje donde no se
         * puede agregar ese valor y cambio el STATUS A TRUE para comparar
        *
         */

        public void ComprobarTableEtiquetas(String Item)
        {
            int l = tableetiquetas.Rows.Count;
            foreach (DataRow roww in table.Rows)
            {
                if (roww["nombre"].ToString() == Item)
                { 
                    //CAMBIO  STATUS(BOOL) A TRUE
                    Alert.ShowAlertError("No se pueden repetir Etiquetas", this.Page);
                    statusetiqueta = true;
                    //SE PARA EL CICLO
                    break;
                }
            }
        }

        /*
        * ComprobarTableOneColum
        * Ciclo que toma el valores de una columna de la tabla, y la compara con un parametros
        * Parametros @Item1:  String
        * Si ambos valores son iguales, para el ciclo(BREAK) y muestra un mensaje donde no se
         * puede agregar ese valor y cambio el STATUS A TRUE para comparar
        *
         */

        public void ComprobarTableOneColumn(String Item1, String Item2)
        {
            int l = table.Rows.Count;
            foreach (DataRow roww in table.Rows)
            {
                if (roww["Descripcion"].ToString() == Item2 && roww["nombre"].ToString() == Item1 && Convert.ToBoolean(roww["borrado"]) == false)
                {
                    Alert.ShowAlertError("No se pueden repetir Opciones en una misma etiqueta", this.Page);

                    //CAMBIO A TRUE STATUS(BOOL)
                    status = true;
                    //SE PARA EL CICLO
                    break;
                }
            }          
        }

        /*
         * ComprobarTableTwoColum
         * Ciclo que toma los valores de dos columnas de la tabla, las combina y las compara con dos paramteros
         * Parametros @Item1:  String               @Item2:  String
         * Si ambos valores son iguales, para el ciclo(BREAK) y muestra un mensaje donde no se
         * puede agregar ese valor y cambio STATUSBLOQUEO a TRUE para comprobar
         */

        public void ComprobarTableTwoColum(String Item1, String Item2)
        {
            int l = tablebloqueo.Rows.Count;
           
            for (int i = 0; i < l; i++)
            {
                /*Tomo el indice iterado por el ciclo*/
                DataRow row = tablebloqueo.Rows[i];
                /*Tomo el valor de las columnas  en el INDICE I y las combino*/
                String combinacion = row["Nombre"].ToString() + row["Descripcion"].ToString();
                //Combino mis valores ITEM1 ITEM2
                String Items = Item1 + Item2;
                //SI MI COMBINACION DE COLUMNAS ES IGUAL A MI COMBINACIÓN DE ITEMS
                if (combinacion.Equals(Items))
                {
                    Alert.ShowAlertError("No se pueden repetir combinaciones", this);
                    //CAMBIO A TRUE STATUS(BOOL)
                    statusbloqueo = true;
                    //SE PARA EL CICLO
                    break;
                }
            }
        }

        /*
          Evento del lado cliente que verifica el estado de CheckBox y cambia la propiedad del Panel
            @Check  Type:CheckBox
            @Panel  Tyoe:Panel
            */

        public void getCheckVisible(RadioButtonList RadioList, Panel Panel1, Panel Panel2)
        {
            switch (RadioList.SelectedValue.ToString())
            {
                case "1":
                    Panel1.Visible = true;
                    Panel2.Visible = false;
                    break;

                case "0":
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                    break;
            }
        }

        /* Evento nivel servidor que toma el parametro de RBL y segun sea el caso cambia la propiedad
                visible de un Panel
                @RadioList Type:RadioButtonList
                @Panel     Type:Panel
            */

        public void getVisible(RadioButtonList RadioList, Panel Panel)
        {
            switch (RadioList.SelectedItem.Text)
            {
                case "Si":
                    Panel.Visible = true;
                    break;

                case "No":
                    Panel.Visible = false;
                    break;
            }
        }

        protected void rbtnTipoEntrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCheckVisible(rbtnTipoEntrada, Etiquetalibre, EtiquetaOpciones);
        }

        //EVENTO SELECTEDINDEX SE HEREDA DE RADIOBUTTON
        protected void rblLibre_SelectedIndexChanged(object sender, EventArgs e)
        {
            getVisible(rblLibre, PanelEtiquetaLibre);
            txtMaximoLibre.Text = "0";
            txtMinimoLibre.Text = "0";
        }

        //EVENTO SELECTEDINDEX SE HEREDA DE RADIOBUTTON
        protected void rblOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            getVisible(rblOpciones, PanelOpciones);
            txtMaximoOpciones.Text = "0";
            txtMinimoOpciones.Text = "0";
        }

        //EVENTO CLICK SE HEREDA DE BUTTON
        protected void btnAgregarOpcion_Click(object sender, EventArgs e)
        {
            //COMPRUEBO SI TEXTBOX ESTA VACIO
            if (txtNombreEtiqueta.Text == string.Empty)
            {
                //SI ESTA VACIO MANDO ALERTA DE ERROR
                Alert.ShowAlertError("Escriba un Nombre para la Etiqueta", this);
            }
            else
            {
                if (txtNombreOpcion.Text == string.Empty)
                {
                    //SI ESTA VACIO MANDO ALERTA DE ERROR
                    Alert.ShowAlertError("Escriba un Nombre para la Opción", this);
                }
                else
                {
                    // SI CONTIENE DATOS MANDO LLAMAR FUNCION PARA INSERTAR DATOS
                    getDataTableOpciones();
                }
            }
        }

        //EVENTO SELECTEDINDEXCHANGED SE HEREDA DE DROPDOWNLIST
        protected void dlEtiquetaBloqueada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SI AMBOS DORPDOWNLIST NO TIENE SELECCIONADO NINGUNA OPCION
            if (dlEtiqueta.SelectedValue.ToString().Equals("select") |
                dlEtiquetaBloqueada.SelectedValue.ToString().Equals("select"))
            {
                // MANDO LLAMAR ALERTA DE ERROR
                Alert.ShowAlertError("Seleccione una Opción por favor", this);
            }
            else
            {
                // MANDO LLAMAR FUNCION PARA INSERTAR DATOS DE BLOQUEOS
                getDataTableBloqueo();
            }
        }

        //EVENTO ROWDELETING SE HEREDA DE GRIDVIEW
        //@SENDER:  Type Object.  Parametro vacio con objeto(GRID)
        //@GridDeleteEventArgs:    Type Event:  Se ehjecuta despues de presiona Template command de Grid
        protected void gridOpciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //INSERTO EL INDEX DE LA FILA SELECCIONADA EN VARIABLES INT
            int id = e.RowIndex; //iterador de la fila
            //Busco el contenido de la fila con el index
            GridViewRow row = gridOpciones.Rows[id];
            //inserto el valor en un string
            string item = Server.HtmlDecode(row.Cells[2].Text);
            string itemetiqueta = Server.HtmlDecode(row.Cells[3].Text);
            //Mando llamar funcion para eliminar parametro de otra tabla enlzada

            //Alert(item, item);
            try
            {
                //Paso valores de columnas a variables
                table = (System.Data.DataTable)(Session["Tabla"]);
                foreach (DataRow roww in table.Rows)
                {
                    if (roww["Descripcion"].ToString() == item)
                    {
                        roww["borrado"] = true;
                    }
                }
                Session.Add("Tabla", table);
                FiltroOpciones(itemetiqueta);
                //subimos a session nuestra tabla actualizada
                //ELIMINAMOS DE LA TABLA BLOQUEOS CULQUIER ITEM CON ESTE NOMBRE

                //agrego tabla a session
                txtNombreOpcion.Text = "";
            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message, this);
            }
        }

        protected void gridOpciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //INSERTO EL INDEX DE LA FILA SELECCIONADA EN VARIABLES INT
            int id = Convert.ToInt32(e.CommandArgument);
            //Busco el contenido de la fila con el index
            GridViewRow row = gridOpciones.Rows[id];
            //inserto el valor en un string
            string item = gridOpciones.DataKeys[id].Values["descripcion"].ToString();
            string itemetiqueta = gridOpciones.DataKeys[id].Values["nombre"].ToString();
            //Mando llamar funcion para eliminar parametro de otra tabla enlzada
            table = (System.Data.DataTable)(Session["Tabla"]);
            switch (e.CommandName)
            {
                case "Eliminar":
                    //Paso valores de columnas a variables
                    foreach (DataRow roww in table.Rows)
                    {
                        if (roww["Descripcion"].ToString() == item && roww["nombre"].ToString() == itemetiqueta && Convert.ToBoolean(roww["borrado"])==false)
                        {
                            roww["borrado"] = true;
                            break;
                        }
                    }
                    break;

                case "Editar":
                    txtNombreOpcion.Text = item;
                    txtNombreEtiqueta.Text = itemetiqueta;
                    Session["opc_name"] = item;
                    Session["opc_enedicion"] = true;
                    break;
            }
            //Alert(item, item);
            try
            {
                Session.Add("Tabla", table);
                FiltroOpciones(itemetiqueta);
            }
            catch (Exception ex)
            {
                msgbox.show(ex.Message, this);
            }
        }

        //EVENTO ROWDELETING SE HEREDA DE GRIDVIEW
        //@SENDER:  Type Object.  Parametro vacio con objeto(GRID)
        //@GridDeleteEventArgs:    Type Event:  Se ehjecuta despues de presiona Template command de Grid
        protected void gridBloqueos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //INSERTO EL INDEX DE LA FILA SELECCIONADA EN VARIABLES INT
            int id = e.RowIndex; //iterador de la fila
            GridViewRow row = gridBloqueos.Rows[id];

            try
            {
                //LLAMO TABLA DESDE SESSION
                tablebloqueo = (System.Data.DataTable)(Session["Tablabloqueo"]);
                // ELIMINO FILA CON EL INDEX
                tablebloqueo.Rows.RemoveAt(id);
                //ACTUALIZO GRID
                gridBloqueos.DataSource = tablebloqueo;
                gridBloqueos.DataBind();
                //AGREGO TABLA A SESSION
                Session.Add("Tablabloqueo", tablebloqueo);
            }
            catch (Exception ex)
            {
            }
        }

        //Evento Click Boton Guardar Formulario SE HEREDA DE BUTTON
        protected void btnGuardarFormulario_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar " + txtNombreGrupo.Text + "?');", true);
        }

        protected void btnActualizarFormulario_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Editar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Actualizar " + txtNombreGrupo.Text + "?');", true);
        }

        //Funcion que actualiza datos
        public void ActualizarFormulario()
        {
            try
            {
                int totalcadena = GeneraTotal("opcion", txtNombreEtiqueta.Text);
                int totaldeclarado = Convert.ToInt32(txtMinimoOpciones.Text);
                if (totalcadena >= totaldeclarado)
                {
                    tableetiquetas = (System.Data.DataTable)(Session["Tablaetiquetas"]);
                    String Etiqueta = null;
                    //Vraiable row heredada de Grid
                    DataRow rowg = tableetiquetas.Rows[IndexUpdate];
                    //Paso valores de columnas a variables
                    Etiqueta = Convert.ToString(rowg["Nombre"].ToString());
                    table = (System.Data.DataTable)(Session["Tabla"]);
                    foreach (DataRow row in table.Rows)
                    {
                        if (row["Nombre"].ToString() == Etiqueta)
                        {
                            row["Nombre"] = txtNombreEtiqueta.Text;
                        }
                    }
                    Session.Add("Tabla", table);
                    gridOpciones.DataSource = table;
                    gridOpciones.DataBind();

                    //Valido que se cumpla funcion Update
                    Boolean Update = UpdateTablaEtiquetas();
                    //Valido tipo de entrada
                    if (rbtnTipoEntrada.SelectedItem.ToString().Equals("Libre"))
                    {
                        //si no es falso
                        if (!Update == false)
                        {
                            Alert.ShowAlert("Sus datos fueron actualizados correctamente", "Mensaje del sistema", this);
                            btnActualizarFormulario.Visible = false;
                            btnGuardarFormulario.Visible = true;
                            //limpio todos los textbox
                            txtNombreEtiqueta.Text = "";
                            txtNombreEtiqueta.Focus();
                            txtMaximoLibre.Text = "0";
                            txtMinimoLibre.Text = "0";
                            txtMaximoOpciones.Text = "0";
                            txtMinimoOpciones.Text = "0";
                            txtNombreOpcion.Text = "";//aqui

                            txtOrden.Text = "";
                            rbtnTipoEntrada.SelectedIndex = 0;
                            Etiquetalibre.Visible = true;
                            EtiquetaOpciones.Visible = false;
                            //Cargotabla principal
                            CargarTablaEtiquetas(Convert.ToInt32(Request.QueryString["id"].ToString()));
                            CargarTablaOpciones(Convert.ToInt32(Request.QueryString["id"].ToString()));
                        }
                    }
                    else
                    {
                        //SI ES OPCIONES

                        ////TablaGlobal
                        if (!Update == false)
                        {
                            Alert.ShowAlert("Sus datos fueron actualizados correctamente", "Mensaje del sistema", this.Page);
                            btnActualizarFormulario.Visible = false;
                            btnGuardarFormulario.Visible = true;
                            //limpio todos los textbox
                            txtNombreEtiqueta.Text = "";
                            txtNombreEtiqueta.Focus();
                            txtMaximoLibre.Text = "0";
                            txtMinimoLibre.Text = "0";
                            txtMaximoOpciones.Text = "0";
                            txtMinimoOpciones.Text = "0";
                            txtNombreOpcion.Text = "";
                            rbtnTipoEntrada.SelectedIndex = 0;
                            Etiquetalibre.Visible = true;
                            EtiquetaOpciones.Visible = false;
                            //Cargotabla principal
                            CargarTablaEtiquetas(Convert.ToInt32(Request.QueryString["id"].ToString()));
                            CargarTablaOpciones(Convert.ToInt32(Request.QueryString["id"].ToString()));
                        }
                    }
                }
                else
                {
                    Alert.ShowAlertError("Debe ingresar un minimo de " + txtMinimoOpciones.Text + " opciones en la etiqueta " +
                               txtNombreEtiqueta.Text + ".", this.Page);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        //Evento acerca de informacion
        protected void btnInfoTipoDato_Click(object sender, EventArgs e)
        {
            Alert.ShowAlertInfo(
                "En cada nueva etiqueta, puede elegir el tipo de datos con los que pueden ser llenadas \\n\\n LIBRE: Eliga esta opción si desea que se ingrese un campo de texto libremente \\n CON OPCIONES: Eliga esta opción si desea que las etiquetas contengan opciones predefinidas para elegir. \\n\\n NOTA: Puede usar ambos tipos de entrada.",
                "Ayuda Acerca de Tipos de Entrada de Datos", this.Page);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Alert.ShowAlertInfo(
                "Coloque un numero Minimo y Maximo de Opciones con entrada de texto libre. \\n NOTA: Para definir sin limites también puede colocar 0 en Minimo y Maximo",
                "Ayuda Acerca de Manejar Limite", this.Page);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Alert.ShowAlertInfo(
                "Coloque un numero Minimo y Maximo de Opciones. \\n NOTA: Para definir sin limites también puede colocar 0 en Minimo y Maximo",
                "Ayuda Acerca de Manejar Limite", this.Page);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Alert.ShowAlertInfo(
                "Puede definir que una Opcion bloque a una etiqueta externa. \\n NOTA: Debe seleccionar en ambas listas una Opción y una Etiqueta.",
                "Ayuda Acerca de Bloqueo de Etiquetas", this.Page);
        }

        protected void btnCancelarFormulario_Click(object sender, EventArgs e)
        {
            //Se regresa a la pagina de grupos
            Response.Redirect("grupos_backend.aspx");
        }

        public void Editing(int i)
        {
            //Declaro String para depositar los valores de las columna seleccionadas
            String Grupo, Etiqueta, Tipo, Minimo, Maximo;
            //Vraiable row heredada de Grid
            DataTable tablatemp = (System.Data.DataTable)(Session["TablaEtiquetas"]);
            DataRow rowg = tablatemp.Rows[i];
            //Paso valores de columnas a variables
            ocidcgpoetiqueta.Value = Convert.ToString(rowg["idc_perfiletiq"].ToString());
            Grupo = Convert.ToString(rowg["Grupo"].ToString());
            Etiqueta = Convert.ToString(rowg["Nombre"].ToString());
            Tipo = Convert.ToString(rowg["Libre"].ToString());
            Minimo = Convert.ToString(rowg["Minimo_sel"].ToString());
            Maximo = Convert.ToString(rowg["Maximo_sel"].ToString());
            txtOrden.Text = rowg["orden"].ToString();
            //Paso variables a controles
            txtNombreGrupo.Text = Grupo;
            txtNombreEtiqueta.Text = Etiqueta;
            NombreEtiquetaGloblal = Etiqueta;
            //Compruebo el tipo de entrada
            if (Tipo.Equals("True"))
            {
                //rbtnTipoEntrada LIBRE
                rbtnTipoEntrada.SelectedIndex = 0;
                getCheckVisible(rbtnTipoEntrada, Etiquetalibre, EtiquetaOpciones);
                txtMaximoLibre.Text = Maximo;
                txtMinimoLibre.Text = Minimo;
            }
            else
            {
                CargarTablaOpciones(Convert.ToInt32(ocidcgpoetiqueta.Value));
                //rbtnTipoEntrada CON OPCIONES
                rbtnTipoEntrada.SelectedIndex = 1;
                getCheckVisible(rbtnTipoEntrada, Etiquetalibre, EtiquetaOpciones);
                txtMaximoOpciones.Text = Maximo;
                txtMinimoOpciones.Text = Minimo;
            }
            FiltroOpciones(Etiqueta);
        }

        //Evento Editing que permite psar los dtos de la fila seleccionada a los controles

        protected void TotalEtiquetas_SelectedIndexChanged1(object sender, EventArgs e)
        {
        }

        ////SE CARGAN LOS DATATABLES PARA MANIPULARLOS Y LLENAR GRIDS

        //Grid GLOBAL
        public void CargarTablaEtiquetas(int Variable)
        {
            //ESTANCIO ENTIDAD
            Etiquetas_EN entidad = new Etiquetas_EN();
            //LE PASO PARAMETRO
            entidad.Grupo = Variable;
            //INSTANCIO COMPOMENETE
            negocio.Componentes.Etiquetas_CM componente = new negocio.Componentes.Etiquetas_CM();
            //DECLARO DATASER Y LE PASO PARAMETRO DE COMPONENTE CON ENTIDAD
            DataSet ds = componente.Etiquetas(entidad);
            //IGUALO DATATABLE A SESSIOM
            tableetiquetas = (System.Data.DataTable)(Session["TablaEtiquetas"]);
            //INSERTO EN DATATABLE TABLAS DEL DATASET
            tableetiquetas = ds.Tables[0];

            //CICLO QUE PASA COLUMNAS A DROPDOWNLIST
            //DECLARO CABTIDAD DE ROWS
            int L = tableetiquetas.Rows.Count;
            for (int k = 0; k < L; k++)
            {
                DataRow row = tableetiquetas.Rows[k];
                //TODAS LAS ROWS QUE ENCUENTRE EN LA COLUMNA NOMBRE SE AGREGAN AL DROPDOWNLIST
                dlEtiquetaBloqueada.Items.Add(row["Nombre"].ToString());
            }
            //CARGO DATOS AL DROP
            dlEtiquetaBloqueada.DataBind();
            //CARGO DATOS AL GRID
            TotalEtiquetas.DataSource = tableetiquetas;
            TotalEtiquetas.DataBind();
            //SUBO TABLA A SESSION
            Session.Add("TablaEtiquetas", tableetiquetas);
        }

        public void CargarTablaOpciones(int idc_perfiletiq)
        {
            //INSTANCIO MI ENTIDAD
            Etiquetas_EN entidad = new Etiquetas_EN();
            entidad.Idc_perfilgpoetiq = idc_perfiletiq;
            //INSTANCIO MI COMPONENTE
            negocio.Componentes.Etiquetas_CM componente = new negocio.Componentes.Etiquetas_CM();
            //DECLARO DATATSET PASANDO PARAMETROS COMPONENTE QUE NECESITA PARAMETROS ENTIDAD
            DataSet ds = componente.Opciones(entidad);
            //BAJO TABLA DE SESSION
            table = (System.Data.DataTable)(Session["Tabla"]);
            //CARGO TABLA CON DATASET
            table = ds.Tables[0];
            //SUBO TABLA SESSION
            Session.Add("Tabla", table);
        }

        //public void CargarTablaBloqueo()
        //{
        //    //INSTANCIO ENTIDAD
        //    Etiquetas_EN entidad = new Etiquetas_EN();
        //    //INSTANCIO ENTIDAD
        //    negocio.Componentes.Etiquetas_CM componente = new negocio.Componentes.Etiquetas_CM();
        //    //DECLARODATASET PASANDO COMPONENETE QUE NECESITA ENTIDAD
        //    DataSet ds = componente.Bloqueos(entidad);
        //    //BAJO TABLA DE SESSION
        //    tablebloqueo = (System.Data.DataTable) (Session["Tablabloqueo"]);
        //    //PASO DATASET A TABLA
        //    tablebloqueo = ds.Tables[0];
        //    //CARGO GRID
        //    gridBloqueos.DataSource = tablebloqueo;
        //    gridBloqueos.DataBind();
        //    //SUBO TABLA A SESSION
        //    Session.Add("Tablabloqueo", tablebloqueo);
        //}
        //public void InsertarEtiqueta(String Grupo, String Etiqueta, int Minimo, int Maximo, int Tipo)
        //{
        //    //INSTANCIO ENTIDAD
        //    Etiquetas_EN entidad = new Etiquetas_EN();
        //    //INSTANCIO ENTIDAD
        //    entidad.Grupo = Grupo;
        //    entidad.Etiqueta = Etiqueta;
        //    entidad.Minimo = Minimo;
        //    entidad.Maximo = Maximo;
        //    entidad.Tipo = Tipo;
        //    //INSTANCIO COMPOMENETE
        //    negocio.Componentes.Etiquetas_CM componente = new negocio.Componentes.Etiquetas_CM();
        //    //DECLARO DATASER Y LE PASO PARAMETRO DE COMPONENTE CON ENTIDAD
        //    DataSet ds = componente.Etiquetas(entidad);
        //    tableetiquetas = (System.Data.DataTable)(Session["TablaEtiquetas"]);
        //    //INSERTO EN DATATABLE TABLAS DEL DATASET
        //    tableetiquetas = ds.Tables[0];

        //    //CICLO QUE PASA COLUMNAS A DROPDOWNLIST
        //    //DECLARO CABTIDAD DE ROWS
        //    int L = tableetiquetas.Rows.Count;
        //    for (int k = 0; k < L; k++)
        //    {
        //        DataRow row = tableetiquetas.Rows[k];
        //        //TODAS LAS ROWS QUE ENCUENTRE EN LA COLUMNA NOMBRE SE AGREGAN AL DROPDOWNLIST
        //        dlEtiquetaBloqueada.Items.Add(row["Nombre"].ToString());
        //    }
        //    //CARGO DATOS AL DROP
        //    dlEtiquetaBloqueada.DataBind();
        //    //CARGO DATOS AL GRID
        //    TotalEtiquetas.DataSource = tableetiquetas;
        //    TotalEtiquetas.DataBind();
        //    //SUBO TABLA A SESSION
        //    Session.Add("TablaEtiquetas", tableetiquetas);
        //}

        /// <summary>
        /// metodo para generar las cadenas de contactos, telefonos y correos
        /// </summary>
        /// <param name=tabla>segun que cadena formaremos</param>
        /// <returns>la cadena formada id;desc</returns>
        protected string GeneraCadena(string tabla, string etiqueta)
        {
            Boolean borrado, nuevo;
            tabla = tabla.ToString().ToLower();
            string cadena = "";
            switch (tabla)
            {
                case "opcion":
                    table = (System.Data.DataTable)(Session["Tabla"]);
                    DataView dv = new DataView(table);
                    //HACEMOS FILTRO
                    dv.RowFilter = "Nombre = '" + etiqueta + "'";
                    foreach (DataRowView fila in dv)
                    {
                        //revisar que tipo de registro es, si es nuevo o no
                        borrado = Convert.ToBoolean(fila["borrado"]);
                        nuevo = Convert.ToBoolean(fila["nuevo"]);
                        //AGREGAR
                        if (borrado == false & nuevo == true)
                        {
                            cadena = cadena + "0" + ";" + fila["descripcion"].ToString().Replace(";", ",") + ";" + "0" + ";";
                        }
                        //ELIMINAR
                        else if (borrado == true & nuevo == false)
                        {
                            cadena = cadena + fila["idc_perfiletiq_opc"] + ";" + fila["descripcion"].ToString().Replace(";", ",") + ";" + "1" + ";";
                        }
                        //ACTUALIZAR
                        else
                        {
                            cadena = cadena + fila["idc_perfiletiq_opc"] + ";" + fila["descripcion"].ToString().Replace(";", ",") + ";" + "0" + ";";
                        }
                    }
                    break;
            }

            return cadena;
        }

        /// <summary>
        ///  este metodo regresa el total de registros de una tabla (contactos, telefonos o correos).
        /// </summary>
        /// <param name=tabla>de que tabla queremos el resultado</param>
        /// <returns>entero numero de registros en total de n tabla</returns>
        protected int GeneraTotal(string tabla, string etiqueta)
        {
            tabla = tabla.ToString().ToLower();
            int totrow = 0;
            switch (tabla)
            {
                case "opcion":
                    table = (System.Data.DataTable)(Session["Tabla"]);
                    DataView dv = new DataView(table);
                    //HACEMOS FILTRO
                    dv.RowFilter = "Nombre = '" + etiqueta + "'";
                    totrow = dv.Count;
                    break;
            }

            return totrow;
        }

        /// <summary>
        /// Funcion que actualiza tabla de etiquetas
        /// </summary>
        public Boolean UpdateTablaEtiquetas()
        {
            Boolean statusf;
            try
            {
                //recibir valores
                int idc_perfilgpo = Convert.ToInt32(ocidcperfilgpo.Value);
                string nometiqueta = txtNombreEtiqueta.Text;
                int minimo = 0;
                int maximo = 0;
                bool libre;
                int idc_usuario = Convert.ToInt32(ocidcusuario.Value);
                if (rbtnTipoEntrada.SelectedItem.ToString().Equals("Libre"))
                {
                    libre = true;
                    minimo = Convert.ToInt32(txtMinimoLibre.Text);
                    maximo = Convert.ToInt32(txtMaximoLibre.Text);
                }
                else
                {
                    libre = false;
                    minimo = Convert.ToInt32(txtMinimoOpciones.Text);
                    maximo = Convert.ToInt32(txtMaximoOpciones.Text);
                }

                //llenamos la entidad
                Etiquetas_EN Entidad = new Etiquetas_EN();
                Entidad.Grupo = idc_perfilgpo;
                Entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                Entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                Entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                Entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                Entidad.Etiqueta = nometiqueta;
                Entidad.Minimo = minimo;
                Entidad.Maximo = maximo;
                Entidad.Tipo = libre;
                Entidad.usuario = idc_usuario;//aqui
                Entidad.POrden = txtOrden.Text == "" ? 0 : Convert.ToInt32(txtOrden.Text);

                Entidad.Idc_perfilgpoetiq = Convert.ToInt32(ocidcgpoetiqueta.Value);
                //SI ESTA CHEACADA LA OPCION METERA EN LA ENTIDAD LOS TOTALES Y LAS CADENAS CON OPCIONES
                if (!rbtnTipoEntrada.SelectedItem.ToString().Equals("Libre")) //con opciones
                {
                    //cadenaopcs = cadenaopcs.Replace(";", "");
                    string cadena = GeneraCadena("opcion", nometiqueta);
                    int tot_cadena = GeneraTotal("opcion", nometiqueta);
                    Entidad.Cadena_opc = cadena;
                    Entidad.Cadena_opc_total = tot_cadena;
                    // msgbox.show(Entidad.Grupo + " " + Entidad.Etiqueta + " " + Entidad.Minimo + " " + Entidad.Maximo + " " + Entidad.Tipo + " " + Entidad.usuario + " " + Entidad.Idc_perfilgpoetiq + " " + Entidad.Cadena_opc + " " + Entidad.Cadena_opc_total, this);
                }
                else
                {
                    Entidad.Cadena_opc = "";
                    Entidad.Cadena_opc_total = 0;
                    // msgbox.show(Entidad.Grupo + " " + Entidad.Etiqueta + " " + Entidad.Minimo + " " + Entidad.Maximo + " " + Entidad.Tipo + " " + Entidad.usuario + " " + Entidad.Idc_perfilgpoetiq + " " + Entidad.Cadena_opc + " " + Entidad.Cadena_opc_total, this);
                }
                Etiquetas_CM Componente = new Etiquetas_CM();
                DataSet ds = new DataSet();
                ds = Componente.ActializarEtiquetas(Entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    statusf = true;
                }
                else
                {
                    //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                    msgbox.show(vmensaje, this.Page);
                    statusf = false;
                }
                //LIMPIO ENTIDADES
                Entidad.Grupo = 0;
                Entidad.Etiqueta = "";
                Entidad.Minimo = 0;
                Entidad.Maximo = 0;
                Entidad.Tipo = false;
                Entidad.usuario = 0;
                Entidad.Idc_perfilgpoetiq = 0;
                Entidad.Cadena_opc = "";
                Entidad.Cadena_opc_total = 0;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
                statusf = false;
            }

            return statusf;
        }

        protected void txtMaximoLibre_TextChanged(object sender, EventArgs e)
        {
            MinimoNOTMaximo(txtMinimoLibre, txtMaximoLibre);
        }

        protected void txtMaximoOpciones_TextChanged(object sender, EventArgs e)
        {
            MinimoNOTMaximo(txtMinimoOpciones, txtMaximoOpciones);
        }

        /// <summary>
        /// Funcion ue verifica que exista coherencia en los minimos y maximo
        /// </summary>
        /// <param name="minimo"></param>
        /// <param name="maximo"></param>
        private void MinimoNOTMaximo(TextBox minimo, TextBox maximo)
        {
            if (minimo.Text == "" | minimo.Text == string.Empty | maximo.Text == "" | maximo.Text == string.Empty)
            {
            }
            else
            {
                int min = Convert.ToInt32(minimo.Text);
                if (Convert.ToInt32(maximo.Text) < min)
                {
                    Alert.ShowAlertError("Verifique que el numero minimo de opciones, NO SOBREPASE AL MAXIMO.", this.Page);
                    minimo.Text = "0";
                    maximo.Text = "0";
                }
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    try
                    {
                        //Tomamos el total minimo que nos da la cadena
                        int totalcadena = GeneraTotal("opcion", txtNombreEtiqueta.Text);
                        int totaldeclarado = 0;//declaramos entero
                        if (rbtnTipoEntrada.SelectedIndex == 0)//si radiobutton esta con el index en 0(libre)
                        {
                            totaldeclarado = Convert.ToInt32(txtMinimoLibre.Text);//igualo entero al minimo de opciones de la caja libre
                        }
                        else
                        {//igualo entero al minimo de opciones de la caja opciones
                            totaldeclarado = Convert.ToInt32(txtMinimoOpciones.Text);
                        }

                        if (totalcadena >= totaldeclarado)//si las opciones minimas insertadas son mayores o iguales a las declaradas
                        {
                            if (txtNombreEtiqueta.Text == "")
                            {
                                Alert.ShowAlertError("Verifique que el formulario este completo.", this.Page);
                                return;
                            }

                            //recibir valores
                            int idc_perfilgpo = Convert.ToInt32(ocidcperfilgpo.Value);
                            string nometiqueta = txtNombreEtiqueta.Text;
                            int minimo = 0;
                            int maximo = 0;
                            bool libre;
                            int idc_usuario = Convert.ToInt32(ocidcusuario.Value);
                            if (rbtnTipoEntrada.SelectedItem.ToString().Equals("Libre"))
                            {
                                libre = true;
                                minimo = Convert.ToInt32(txtMinimoLibre.Text);
                                maximo = Convert.ToInt32(txtMaximoLibre.Text);
                            }
                            else
                            {
                                libre = false;
                                minimo = Convert.ToInt32(txtMinimoOpciones.Text);
                                maximo = Convert.ToInt32(txtMaximoOpciones.Text);
                            }

                            //llenamos la entidad
                            Etiquetas_EN Entidad = new Etiquetas_EN();

                            Entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                            Entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                            Entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                            Entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                            Entidad.Grupo = idc_perfilgpo;
                            Entidad.Etiqueta = nometiqueta;
                            Entidad.Minimo = minimo;
                            Entidad.Maximo = maximo;
                            Entidad.Tipo = libre;
                            Entidad.usuario = idc_usuario;
                            Entidad.POrden = txtOrden.Text == "" ? 0 : Convert.ToInt32(txtOrden.Text);
                            Entidad.Idc_perfilgpoetiq = Convert.ToInt32(ocidcgpoetiqueta.Value);
                            //SI ESTA CHEACADA LA OPCION METERA EN LA ENTIDAD LOS TOTALES Y LAS CADENAS CON OPCIONES
                            if (rbtnTipoEntrada.SelectedValue == "0") //con opciones
                            {
                                //cadenaopcs = cadenaopcs.Replace(";", "");
                                string cadena = GeneraCadena("opcion", nometiqueta);
                                int tot_cadena = GeneraTotal("opcion", nometiqueta);
                                Entidad.Cadena_opc = cadena;
                                Entidad.Cadena_opc_total = tot_cadena;
                            }
                            else
                            {
                                Entidad.Cadena_opc = "";
                                Entidad.Cadena_opc_total = 0;
                            }
                            //creamos el componente
                            Etiquetas_CM Componente = new Etiquetas_CM();
                            DataSet ds = new DataSet();
                            ds = Componente.InsertarEtiquetas(Entidad);
                            string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                            if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                            {
                                //todo bien
                                rbtnTipoEntrada.SelectedIndex = 0;
                                Etiquetalibre.Visible = true;
                                EtiquetaOpciones.Visible = false;
                                txtNombreEtiqueta.Text = "";
                                txtOrden.Text = "";
                                //Cargotabla principal
                                CargarTablaEtiquetas(Convert.ToInt32(Request.QueryString["id"].ToString()));
                                CargarTablaOpciones(Convert.ToInt32(Request.QueryString["id"].ToString()));
                                Alert.ShowAlert("Sus datos fueron guardados correctamente.", "Mensaje del sistema.", this.Page);
                            }
                            else
                            {
                                //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                                msgbox.show(vmensaje, this.Page);
                                return;
                            }
                        }
                        else
                        {
                            Alert.ShowAlertError("Debe ingresar un minimo de " + txtMinimoOpciones.Text + " opciones en la etiqueta " + txtNombreEtiqueta.Text + ".", this);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.Message, this);
                    }
                    break;

                case "Editar":
                    ActualizarFormulario();
                    break;

                case "Eliminar":
                    int ID = Convert.ToInt32(Session["idc_perfiletiq"]);
                    try
                    {
                        Etiquetas_EN Entidad = new Etiquetas_EN();
                        Entidad.Idc_perfilgpoetiq = ID;
                        Entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                        Entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        Entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        Entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                        Etiquetas_CM Componente = new Etiquetas_CM();
                        DataSet ds = new DataSet();
                        ds = Componente.EliminarEtiquetas(Entidad);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                        {
                            Alert.ShowAlert("Sus datos fueron eliminados correctamente.", "Mensaje del sistema.", this);

                            CargarTablaEtiquetas(Convert.ToInt32(Request.QueryString["id"].ToString()));
                        }
                        else
                        {
                            //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                            msgbox.show(vmensaje, this.Page);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.Message, this);
                    }
                    break;

                case "Editar Etiqueta":
                    Editing(Convert.ToInt32(Session["index"]));
                    break;
            }
        }

        protected void TotalEtiquetas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string item = (TotalEtiquetas.DataKeys[index].Values["nombre"].ToString());
            string itemetiqueta = (TotalEtiquetas.DataKeys[index].Values["idc_perfiletiq"].ToString());
            switch (e.CommandName)
            {
                case "Select":
                    Session["Caso_Confirmacion"] = "Editar Etiqueta";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Editar " + item + "?');", true);
                    btnActualizarFormulario.Visible = true;
                    btnGuardarFormulario.Visible = false;
                    int i = TotalEtiquetas.SelectedIndex;
                    IndexUpdate = index;
                    Session["index"] = index;

                    break;

                case "Eliminar":

                    //Mando llamar funcion para eliminar parametro de otra tabla enlzada
                    Session["item"] = item;
                    Session["idc_perfiletiq"] = itemetiqueta;
                    Session["Caso_Confirmacion"] = "Eliminar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Eliminar " + item + "?');", true);

                    //Paso valores de columnas a variables
                    //DeleteTablaOpcionesGlobal(item);
                    //Mando llamar funcion para eliminar parametro de otra tabla enlzada
                    DeleteTablaBloqueosEtiqueta(item);
                    break;
            }
        }
    }
}