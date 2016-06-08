using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class aprobaciones_captura : System.Web.UI.Page
    {
        //Variables globales
        public int idc_aprobacion = 0;

        public int idc_aprobacion_detalle = 0;
        public bool nuevo_registro_aprobacion = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)//En a primer carga
            {
                DataTable tabla_temp = new DataTable();
                //Cargo el dropdown con los datos del dataset
                ddlDepartamento.DataTextField = "depto";
                ddlDepartamento.DataValueField = "depto";
                //filtramos para no repetir datos de columnas
                ddlDepartamento.DataSource = TablaGlobal().DefaultView.ToTable(true, "depto");
                ddlDepartamento.DataBind();
                if (Request.QueryString["idc_aprobacion"] == null)//Si no existe el request, quiere decir que es nueva captura
                {
                    idc_aprobacion = 0;
                    if (Session["TablaEmpleado"] == null)//si no existe tabla la creamos
                    {
                        tabla_temp.Columns.Add("idc_aprobacion_det");
                        tabla_temp.Columns.Add("idc_puesto");
                        tabla_temp.Columns.Add("puesto");
                        tabla_temp.Columns.Add("nuevo");
                        tabla_temp.Columns.Add("borrado");
                        Session.Add("TablaEmpleado", tabla_temp);//subimos a sesion
                    }
                    FiltroOpciones(tabla_temp);
                }
                else//tiene request, es una actualizacion
                {
                    //si no existe tabla la creamos
                    if (Session["TablaEmpleado"] == null)
                    {
                        //bajamos parametro y se lo asigamos a variable
                        //convertimos parametro a string ya que viene en hexadecimal
                        string idc_apro = funciones.ConvertHexToString(Request.QueryString["idc_aprobacion"].ToString());
                        idc_aprobacion = Convert.ToInt32(idc_apro);
                        //creamos tabla
                        tabla_temp.Columns.Add("idc_aprobacion_det");
                        tabla_temp.Columns.Add("idc_puesto");
                        tabla_temp.Columns.Add("puesto");
                        tabla_temp.Columns.Add("nuevo");
                        tabla_temp.Columns.Add("borrado");
                        //le insertamos datos con consulta sql
                        DataTable table_temp = TablaUpdate();
                        foreach (DataRow row in table_temp.Rows)
                        {
                            DataRow new_row = tabla_temp.NewRow();
                            new_row["idc_aprobacion_det"] = row["idc_aprobacion_det"].ToString();
                            new_row["idc_puesto"] = row["idc_puesto"].ToString();
                            new_row["puesto"] = row["puesto"].ToString();
                            new_row["nuevo"] = false;
                            new_row["borrado"] = Convert.ToBoolean(row["borrado"].ToString());
                            tabla_temp.Rows.Add(new_row);
                            Session.Add("TablaEmpleado", tabla_temp);
                            //filtramos
                            FiltroOpciones(tabla_temp);
                            //verificamos firmas faltantes
                            RegistrosFaltantes();
                        }
                        //pasamos parametros a controles
                        DataRow content = table_temp.Rows[0];
                        txtNombre.Text = content["nombre"].ToString();
                        txtComentarios.Text = content["comentarios"].ToString();
                        txtMinimoRequeridos.Text = content["minimo_votos"].ToString();
                    }
                }
                //subimos tabla a sesion
                tabla_temp = (System.Data.DataTable)(Session["TablaEmpleado"]);
                FiltroOpciones(tabla_temp);
                RegistrosFaltantes();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["idc_aprobacion"]))
            {
                string idc_apro = funciones.ConvertHexToString(Request.QueryString["idc_aprobacion"].ToString());
                idc_aprobacion = Convert.ToInt32(idc_apro);
            }
            RegistrosFaltantes();
        }

        /// <summary>
        /// Devuele todos los datos de la tabla puestos sin filtrar
        /// </summary>
        /// <returns></returns>
        public DataTable TablaGlobal()
        {
            DataTable table = new DataTable();
            Nuevas_AprobacionesENT entidad = new Nuevas_AprobacionesENT();
            Nuevas_AprobacionesCOM componente = new Nuevas_AprobacionesCOM();
            try
            {
                table = componente.getDataPuestos(entidad).Tables[0];
                return table;
            }
            catch (Exception EX)
            {
                Alert.ShowAlertError(EX.ToString(), this);
            }
            return table;
        }

        /// <summary>
        /// Devuele los datos de una aprobacion existente
        /// </summary>
        /// <returns></returns>
        public DataTable TablaUpdate()
        {
            DataTable table = new DataTable();
            AprobacionesENT entidad = new AprobacionesENT();
            entidad.Idc_aprobacion = idc_aprobacion;
            AprobacionesCOM componente = new AprobacionesCOM();
            try
            {
                table = componente.CargaCatalogoAprobaciones(entidad).Tables[0];
                return table;
            }
            catch (Exception EX)
            {
                Alert.ShowAlertError(EX.ToString(), this);
            }
            return table;
        }

        /// <summary>
        /// Comprueba que no se repitan datos
        /// </summary>
        /// <param name="table"></param>
        /// <param name="Item"></param>
        /// <returns></returns>
        public bool ComprobarTable(DataTable table, String Item)
        {
            Boolean status = true;
            foreach (DataRow row in table.Rows)
            {
                String columna = row["idc_puesto"].ToString();
                bool borrado = Convert.ToBoolean(row["borrado"].ToString());
                if (columna.Equals(Item))//si existe la columna
                {
                    if (borrado == true)//si la columna existe y esta en borrado: tomamos su id, eliminamos la fila e insertamos la nueva fila con el id anterior
                    {
                        idc_aprobacion_detalle = Convert.ToInt32(row["idc_aprobacion_det"].ToString());
                        nuevo_registro_aprobacion = false;
                        row.Delete();
                        Session.Add("TablaEmpleado", table);//actualizamos tabla en sesion
                        status = true;
                        break;//termina ciclo
                    }
                    else
                    {
                        //CAMBIO  STATUS(BOOL) A TRUE
                        status = false;
                    }
                }
            }
            return status;
        }

        //boton que muestra la lista de empleados
        protected void btnEmpleado_Click(object sender, EventArgs e)
        {
            lblDptoSeeleccionado.Text = "Seleccione los puesto(s) del departamento " + ddlDepartamento.SelectedValue.ToString();
            //cargamos tabla
            DataTable table_employeed = TablaGlobal();
            //filtramos para que solo nos muestre el departamento sin repetir
            DataRow[] result = table_employeed.Select("depto = '" + ddlDepartamento.SelectedValue.ToString() + "'");
            //limpiamos lista
            cblPuestos.Items.Clear();
            //lleno lista con puestos
            foreach (DataRow row in result)
            {
                string puesto = row["puesto"].ToString();
                string empleado = row["empleado"].ToString();
                empleado = empleado.ToLower();
                empleado = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(empleado);
                //agrego item a lista
                cblPuestos.Items.Add(new ListItem(puesto + ": " + empleado, row["idc_puesto"].ToString()));
            }
            //verificamos los empleados que ya estan seleccionados y los marcamos
            //bajamos tabla de empleados seleccionados de session
            DataTable tabla_temp = (System.Data.DataTable)(Session["TablaEmpleado"]);
            //Ciclo de la lista
            foreach (ListItem item in cblPuestos.Items)
            {
                //ciclo de la tabla
                foreach (DataRow row in tabla_temp.Rows)
                {
                    string value_list = item.Value.ToString();
                    string value_row = row["idc_puesto"].ToString();
                    bool borrado = Convert.ToBoolean(row["borrado"].ToString());
                    //si existe un empleado y no esta borrado
                    if (value_list.Equals(value_row) & borrado == false)
                    {
                        //item se selecciona
                        item.Selected = true;
                    }
                }
            }
            //Mostramos Modal con lista de empleados
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ViewListEmpleados();", true);
        }

        //boton dentro de modal donde seleccionamos empleados
        protected void btnAceptarEmpleado_Click(object sender, EventArgs e)
        {
            //bajamos tabla de empleado de sesion
            DataTable tabla_temp = (System.Data.DataTable)(Session["TablaEmpleado"]);
            //ciclo de la lista
            foreach (ListItem item in cblPuestos.Items)
            {
                //si el item esta seleccionado
                if (item.Selected == true)
                {
                    //creamos nueva fila
                    DataRow new_row = tabla_temp.NewRow();
                    //verificamos que puesto no exista en la tabla de empleados
                    bool status = ComprobarTable(tabla_temp, item.Value.ToString());
                    //agregamos parametros a nueva fila
                    new_row["idc_aprobacion_det"] = idc_aprobacion_detalle;
                    new_row["idc_puesto"] = item.Value.ToString();
                    new_row["puesto"] = item.Text.ToString();
                    new_row["nuevo"] = nuevo_registro_aprobacion;
                    new_row["borrado"] = false;
                    //si no existe, insertamos la fila
                    if (!status == false)
                    {
                        tabla_temp.Rows.Add(new_row);
                    }
                }
            }
            //cargamos grid
            FiltroOpciones(tabla_temp);
            //actualizamos tabla de sesion
            Session.Add("TablaEmpleado", tabla_temp);
        }

        //evento que sucede al dar clic en un boton del grid de empleados/puestos
        protected void gridEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //tomamos index de fila seleccionada
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_puesto"] = gridEmpleados.DataKeys[index].Values["idc_puesto"].ToString();//subimos id a session
            switch (e.CommandName)//verificamos que boton esta seleccionado
            {
                case "Borrar"://borrar fila
                    Session["Caso_Confirmacion_Grid"] = "Borrar";//ndicamos en sesion que se borrara una fila
                    //mandamos alerta de confirmacion
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Desea eliminar la referencia a este empleado?.');", true);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Evento agregado de forma manual, se encadena cuando el usuario confirma modal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            int id_puesto = Convert.ToInt32(Session["idc_puesto"]);//bajamos paraemtros de sesion
            string Confirma_a = (string)(Session["Caso_Confirmacion_Grid"]);
            switch (Confirma_a)
            {
                case "Borrar"://se esta borrando una fila de un grid
                    DeleteAprobacion(id_puesto.ToString());//llamamos funcion
                    break;

                case "Guardar"://se guardara o actualizara el formulario completo
                    bool status = InsertarAprobacion(idc_aprobacion);//comprobamos que todo este correcto
                    if (!status == false)
                    {//mientras todo este correctp
                        //limpiamos tabla
                        Session["TablaEmpleado"] = null;
                        //llamamos confirmacion
                        DataTable tabla_temp = new DataTable();
                        tabla_temp.Columns.Add("idc_aprobacion_det");
                        tabla_temp.Columns.Add("idc_puesto");
                        tabla_temp.Columns.Add("puesto");
                        tabla_temp.Columns.Add("nuevo");
                        tabla_temp.Columns.Add("borrado");
                        Session.Add("TablaEmpleado", tabla_temp);//subimos a sesion
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "AlertOK('aprobaciones.aspx');", true);
                    }
                    break;

                default:

                    break;
            }
        }

        /// <summary>
        /// Elimina Row de tabla en sesion y carga grid
        /// </summary>
        /// <param name="idc_puesto"></param>
        private void DeleteAprobacion(string idc_puesto)
        {
            //bajamos tablla de session
            DataTable tabla_temp = (System.Data.DataTable)(Session["TablaEmpleado"]);
            //recorremos tabla
            for (int i = tabla_temp.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = tabla_temp.Rows[i];
                if (dr["idc_puesto"].ToString() == idc_puesto)//si existe
                    dr["borrado"] = true;//cambiamos status a borrado
            }
            Session.Add("TablaEmpleado", tabla_temp);//actualizamos tabla de sesion
            FiltroOpciones(tabla_temp);//filtramos
        }

        /// <summary>
        /// Comprueba que los registros concuerden con el minimo y muestra mensaje
        /// </summary>
        public void RegistrosFaltantes()
        {
            string getvalue = txtMinimoRequeridos.Text;
            //cada vez que cambio el texto del text_minimofirmas se verifica que haya concordancia
            if (getvalue.Equals(""))
            {
                Alert.ShowAlertError("Debe existir un minimo de 1 firma.", this);
                txtMinimoRequeridos.Text = "1";
            }
            else
            {
                int declarado = Convert.ToInt32(txtMinimoRequeridos.Text);//minimo ew   uerido
                int total = gridEmpleados.Rows.Count;//cantidad actual
                                                     //si se marco como minimo de firmas 0 o menos que 0
                if (declarado <= 0)
                {
                    Alert.ShowAlertError("Debe tener al menos 1 firma para crear la aprobación", this);
                    txtMinimoRequeridos.Text = "1";
                }
                //NO hay registros
                if (total == 0)
                {
                    lblTipo_Cantidad.Text = "Faltan";
                    lbl_Numero_Cantidad.Text = declarado.ToString() + " puesto(s)";
                    lbl_Numero_Cantidad.CssClass = "label label-danger";
                }
                //Son los mismos
                if (declarado == total)
                {
                    lblTipo_Cantidad.Text = "Ya estan los";
                    lbl_Numero_Cantidad.Text = declarado.ToString() + " puesto(s)";
                    lbl_Numero_Cantidad.CssClass = "label label-success";
                }//el faltan
                if (declarado > total)
                {
                    lblTipo_Cantidad.Text = "Faltan";
                    int resultado = declarado - total;
                    lbl_Numero_Cantidad.Text = resultado.ToString() + " puesto(s)";
                    lbl_Numero_Cantidad.CssClass = "label label-danger";
                }
                //Sobran
                if (declarado < total)
                {
                    lblTipo_Cantidad.Text = "Sobran";
                    int resultado = total - declarado;
                    lbl_Numero_Cantidad.Text = resultado.ToString() + " puesto(s)";
                    lbl_Numero_Cantidad.CssClass = "label label-primary";
                }
            }
        }

        /// <summary>
        /// Valida que los datos del formulario esten correctos, devuelve true y todo esta bien
        /// </summary>
        /// <returns></returns>
        public bool validation()
        {
            bool status = true;
            if (txtComentarios.Text.Equals(string.Empty))
            {
                Alert.ShowAlertError("Debe colocar una breve descripción sobre la aprobación.", this);
                txtComentarios.Focus();
                status = false;
            }
            if (txtNombre.Text.Equals(string.Empty))
            {
                Alert.ShowAlertError("Escriba un nombre para la aprobación.", this);
                txtNombre.Focus();
                status = false;
            }
            if (txtMinimoRequeridos.Text.Equals(string.Empty))
            {
                Alert.ShowAlertError("Debe colocar un minimo de firmas sobre la aprobación.", this);
                txtMinimoRequeridos.Focus();
                status = false;
            }
            int declarado = Convert.ToInt32(txtMinimoRequeridos.Text);//minimo ew   uerido
            int total = gridEmpleados.Rows.Count;//cantidad actual
            if (total < declarado)
            {
                Alert.ShowAlertError("Debe colocar un minimo de " + declarado.ToString() + " de puestos para firmar.", this);
                txtMinimoRequeridos.Focus();
                status = false;
            }
            return status;
        }

        protected void gridEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //cada vez que se carga el grid verificamos cuantas firmas faltan
            RegistrosFaltantes();
        }

        //boton guradra formulario
        protected void btnGuardarForm_Click(object sender, EventArgs e)
        {
            //validamos formularios
            bool status = validation();
            if (!status == false)//si todo es correcto
            {
                //EN CASO DE QUE SE ELIGA FIRMAS OBLIGATORIAS
                //DataTable tabla_temp = (System.Data.DataTable)(Session["TablaEmpleado"]);
                ////antes de llenar lista la limpio para asegurar no haya errores
                //cblObligatorio.Items.Clear();
                ////lleno lista con puestos
                //foreach (DataRow row in tabla_temp.Rows)
                //{
                //    string puesto = row["puesto"].ToString();
                //    cblObligatorio.Items.Add(new ListItem(puesto,row["idc_puesto"].ToString()));
                //}
                //MANDO A SESSION QUE VOY A GUARDAR Y LLAMO CONFIRM
                Session["Caso_Confirmacion_Grid"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','Se guardara esta aprobación ¿Todos sus datos son correctos?');", true);
                //EN CASO DE QUE SE ELIGA FIRMAS OBLIGATORIAS
                //muestro modal para elegir las firmas que son obligatorias
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ViewListEmpleadosSelected();", true);
            }
        }

        //cancelamos formulario
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["TablaEmpleado"] = null;
            Response.Redirect("aprobaciones.aspx");
        }

        protected void txtMinimoRequeridos_TextChanged(object sender, EventArgs e)
        {
            RegistrosFaltantes();
        }

        //EN CASO DE QUE SE REQUIERA ELEGIR LAS FIRMAS OBLIGATORIAS
        //protected void btnAcpetarObligatorio_Click(object sender, EventArgs e)
        //{
        //    Session["Caso_Confirmacion_Grid"] = "Guardar";
        //    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','Se guardara esta aprobación ¿Todos sus datos son correctos?.');", true);

        //}

        /// <summary>
        /// metodo para generar las cadenas de contactos, telefonos y correos
        /// </summary>
        /// <param name=tabla>segun que cadena formaremos</param>
        /// <returns>la cadena formada id;desc</returns>
        protected string GeneraCadena(DataTable table)
        {
            Boolean borrado, nuevo;
            DataView dv = new DataView(table);
            //HACEMOS FILTRO
            string cadena = "";
            foreach (DataRow fila in table.Rows)
            {
                //revisar que tipo de registro es, si es nuevo o no
                borrado = Convert.ToBoolean(fila["borrado"]);
                nuevo = Convert.ToBoolean(fila["nuevo"]);
                //AGREGAR
                if (borrado == false & nuevo == true)
                {
                    cadena = cadena + "0" + ";" + fila["idc_puesto"].ToString().Replace(";", ",") + ";" + "1" + ";" + "0" + ";";
                }
                //ELIMINAR
                else if (borrado == true & nuevo == false)
                {
                    cadena = cadena + fila["idc_aprobacion_det"] + ";" + fila["idc_puesto"].ToString().Replace(";", ",") + ";" + "1" + ";" + "1" + ";";
                }
                //ACTUALIZAR
                else
                {
                    cadena = cadena + fila["idc_aprobacion_det"] + ";" + fila["idc_puesto"].ToString().Replace(";", ",") + ";" + "0" + ";" + borrado.ToString() + ";";
                }
            }
            return cadena;
        }

        /// <summary>
        /// Carga Grid con filtro de borrado
        /// </summary>
        /// <param name="Filtro"></param>
        public void FiltroOpciones(DataTable table)
        {
            //filtramos y llenamos el GridView
            DataView dv = new DataView(table);
            //HACEMOS FILTRO
            dv.RowFilter = "borrado = false";
            //CARGAMOS DATOS
            gridEmpleados.DataSource = dv;
            gridEmpleados.DataBind();
        }

        /// <summary>
        /// Inserta/Actualiza en SQL una aprobacion, devuelve true si todo fue correcto
        /// </summary>
        /// <returns></returns>
        public bool InsertarAprobacion(int idc_aprobacion_temp)
        {
            //bajamos tabla de sesion
            DataTable tabla_temp = (System.Data.DataTable)(Session["TablaEmpleado"]);
            //cargamos entidad
            Nuevas_AprobacionesENT Entidad = new Nuevas_AprobacionesENT();
            Nuevas_AprobacionesCOM Componente = new Nuevas_AprobacionesCOM();
            //pasamos parametros a entidad
            Entidad.Idc_aprobacion = idc_aprobacion_temp;//indicamos que es nueva o que es actualizacion, si idc esta en 0 es nueva
            Entidad.Nombre = txtNombre.Text.ToUpper();
            Entidad.Descripcion = txtComentarios.Text.ToUpper();
            Entidad.Minimo_firmas = Convert.ToInt32(txtMinimoRequeridos.Text);
            Entidad.Total_Cadena = tabla_temp.Rows.Count;
            Entidad.Cadena = GeneraCadena(tabla_temp);
            Entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
            Entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
            Entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
            Entidad.Pusuariopc = funciones.GetUserName();//usuario pc
            try
            {
                //cargamos componente
                DataSet ds = Componente.InsertAprobacion(Entidad);
                DataRow row = ds.Tables[0].Rows[0];
                //verificamos que no existan errores
                string mensaje = row["mensaje"].ToString();
                if (mensaje.Equals(string.Empty))//no hay errores retornamos true
                {
                    return true;
                }
                else
                {//mostramos error
                    Alert.ShowAlertError(mensaje, this);
                    return false;
                }
            }
            catch (Exception ex)
            {//excpecion nivel vista
                return false;
                Alert.ShowAlertError(ex.Message, this);
            }
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string getvaluecombo = ddlDepartamento.SelectedItem.Text;
            if (getvaluecombo.Equals("DIRECCION           "))//se dejaron espacios por que combo no los elimina
            {
                Alert.ShowAlertInfo("Recuerde que solamente es necesaria 1 firma de DIRECCION", "Mensaje del sistema", this);
            }
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] == null)
            {
                Response.Redirect("administrador_prebajas.aspx");
            }
            else
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }
    }
}