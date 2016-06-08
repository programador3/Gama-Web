using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class catalogo_avisos_captura : System.Web.UI.Page
    {//Variables globales
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
                ddlDepartamento.DataSource = TablaGlobal().DefaultView.ToTable(true, "depto"); ;
                ddlDepartamento.DataBind();
                if (Request.QueryString["idc_taviso"] == null)//Si no existe el request, quiere decir que es nueva captura
                {
                    if (Session["TablaEmpleado"] == null)//si no existe tabla la creamos
                    {
                        tabla_temp.Columns.Add("idc_taviso");
                        tabla_temp.Columns.Add("idc_puesto");
                        tabla_temp.Columns.Add("puesto");
                        tabla_temp.Columns.Add("nuevo");
                        tabla_temp.Columns.Add("borrado");
                        Session.Add("TablaEmpleado", tabla_temp);//subimos a sesion
                    }
                }
                else
                {
                    //bajamos parametro y se lo asigamos a variable
                    idc_aprobacion = Convert.ToInt32(Request.QueryString["idc_taviso"].ToString());
                    //creamos tabla
                    tabla_temp.Columns.Add("idc_taviso");
                    tabla_temp.Columns.Add("idc_puesto");
                    tabla_temp.Columns.Add("puesto");
                    tabla_temp.Columns.Add("nuevo");
                    tabla_temp.Columns.Add("borrado");
                    //le insertamos datos con consulta sql
                    DataTable table_temp = TablaUpdate(idc_aprobacion);
                    foreach (DataRow row in table_temp.Rows)
                    {
                        DataRow new_row = tabla_temp.NewRow();
                        new_row["idc_taviso"] = row["idc_taviso"].ToString();
                        new_row["idc_puesto"] = row["idc_puesto"].ToString();
                        new_row["puesto"] = row["puesto"].ToString();
                        new_row["nuevo"] = true;
                        new_row["borrado"] = Convert.ToBoolean(row["borrado"].ToString());
                        tabla_temp.Rows.Add(new_row);
                        Session.Add("TablaEmpleado", tabla_temp);
                    }
                    //pasamos parametros a controles
                    DataRow content = table_temp.Rows[0];
                    txtNombre.Text = content["descripcion"].ToString();
                    //subimos tabla a sesion
                    tabla_temp = (System.Data.DataTable)(Session["TablaEmpleado"]);
                    FiltroOpciones(tabla_temp);
                }
                //subimos tabla a sesion
                tabla_temp = (System.Data.DataTable)(Session["TablaEmpleado"]);
                FiltroOpciones(tabla_temp);
            }
        }

        /// <summary>
        /// Devuele los datos de una aprobacion existente
        /// </summary>
        /// <returns></returns>
        public DataTable TablaUpdate(int idc_aprobacion)
        {
            DataTable table = new DataTable();
            Catalogo_AvisosENT entidad = new Catalogo_AvisosENT();
            Catalogo_AvisosCOM componente = new Catalogo_AvisosCOM();
            entidad.Pidc_taviso = idc_aprobacion;
            try
            {
                table = componente.CargaAvisos(entidad).Tables[0];
                return table;
            }
            catch (Exception EX)
            {
                Alert.ShowAlertError(EX.ToString(), this);
            }
            return table;
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

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

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
                        idc_aprobacion_detalle = Convert.ToInt32(row["idc_taviso"].ToString());
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
                    new_row["idc_taviso"] = idc_aprobacion_detalle;
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
        /// metodo para generar las cadenas de contactos, telefonos y correos
        /// </summary>
        /// <param name=tabla>segun que cadena formaremos</param>
        /// <returns>la cadena formada id;desc</returns>
        protected int TotalCadena(DataTable table)
        {
            Boolean borrado, nuevo;
            DataView dv = new DataView(table);
            //HACEMOS FILTRO
            int cadena = 0;
            foreach (DataRow fila in table.Rows)
            {
                //revisar que tipo de registro es, si es nuevo o no
                borrado = Convert.ToBoolean(fila["borrado"]);
                nuevo = Convert.ToBoolean(fila["nuevo"]);
                //AGREGAR
                if (borrado == false & nuevo == true)
                {
                    cadena = cadena + 1;
                }
            }
            return cadena;
        }

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
                    cadena = cadena + fila["idc_puesto"].ToString().Replace(";", ",") + ";";
                }
            }
            return cadena;
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string id_puesto = (string)(Session["idc_puesto"]);//bajamos paraemtros de sesion
            string Confirma_a = (string)(Session["Caso_Confirmacion_Grid"]);
            DataTable tabla_temp = (System.Data.DataTable)(Session["TablaEmpleado"]);
            Catalogo_AvisosENT entidad = new Catalogo_AvisosENT();
            Catalogo_AvisosCOM comp = new Catalogo_AvisosCOM();
            switch (Confirma_a)
            {
                case "Borrar"://se esta borrando una fila de un grid
                    DeleteAprobacion(id_puesto);//llamamos funcion
                    break;

                case "Guardar":

                    entidad.Descripcion = txtNombre.Text;
                    entidad.Cadaviso = GeneraCadena(tabla_temp);
                    entidad.NumCad = TotalCadena(tabla_temp);
                    DataSet ds = comp.AgregaAviso(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('El Aviso fue Guardado Correctamente.','catalogo_avisos.aspx');", true);
                    }
                    else
                    {
                        Alert.ShowAlertError(mensaje, this);
                    }
                    break;

                case "Editar":
                    entidad.Descripcion = txtNombre.Text;
                    entidad.Cadaviso = GeneraCadena(tabla_temp);
                    entidad.NumCad = TotalCadena(tabla_temp);
                    entidad.Pidc_taviso = Convert.ToInt32(Request.QueryString["idc_taviso"].ToString());
                    DataSet ds2 = comp.EditarAviso(entidad);
                    DataRow row2 = ds2.Tables[0].Rows[0];
                    string mensaje2 = row2["mensaje"].ToString();
                    if (mensaje2 == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('El Aviso fue Guardado Correctamente.','catalogo_avisos.aspx');", true);
                    }
                    else
                    {
                        Alert.ShowAlertError(mensaje2, this);
                    }
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

        protected void btnGuardarForm_Click(object sender, EventArgs e)
        {
            bool error = false;
            DataTable tabla_temp = (System.Data.DataTable)(Session["TablaEmpleado"]);
            if (TotalCadena(tabla_temp) == 0)
            {
                Alert.ShowAlertError("Debe Agregar al menos 1 puesto", this);
                error = true;
            }
            if (txtNombre.Text == "") { error = true; Alert.ShowAlertError("Escrba el Nombre del Aviso", this); }
            if (error == false)
            {
                if (Request.QueryString["idc_taviso"] == null)//Si no existe el request, quiere decir que es nueva captura
                {
                    Session["Caso_Confirmacion_Grid"] = "Guardar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','Se guardara este aviso ¿Todos sus datos son correctos?');", true);
                }
                else
                {
                    Session["Caso_Confirmacion_Grid"] = "Editar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','Se actualizara este aviso ¿Todos sus datos son correctos?');", true);
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("catalogo_avisos.aspx");
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }
    }
}