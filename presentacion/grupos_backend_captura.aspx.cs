using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class grupos_backend_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int idcperfilgpo;

                if (!string.IsNullOrEmpty(Request.QueryString["uidc_perfilgpo"]))
                {
                    idcperfilgpo = Convert.ToInt32(Request.QueryString["uidc_perfilgpo"]);
                }
                else
                {
                    idcperfilgpo = 0;
                }
                //ocultamos el valor
                ocidc_perfilgpo.Value = idcperfilgpo.ToString();
                //if (Session["sidc_perfilgpo"] != null)
                //{
                //    idcperfilgpo = Convert.ToInt32(Session["sidc_perfilgpo"].ToString().Trim());
                //}
                //else
                //{
                //    idcperfilgpo = 0;
                //}
                //saber si vamos a editar o agregar un registro
                //ocultar por default
                //ocultamos los paneles
                panel_libre.Visible = false;
                panel_opciones.Visible = false;
                //ocultamos subpaneles (contienen las cajas de textos)
                gpominmaxlibre.Visible = false;
                gpominmaxopciones.Visible = false;
                rbtnlimitelib.Items[1].Selected = true;
                rbtnlimitopciones.Items[1].Selected = true;
                CreaDatatable("opciones");
                if (idcperfilgpo == 0)
                { //nuevo
                    txttitulo.Text = "Alta de Grupos";
                }
                else
                {
                    txttitulo.Text = "Edición de Grupos";
                    //cargamos datos
                    DataSet ds = new DataSet();
                    //creamos la entidad
                    Grupos_backendE Entidad = new Grupos_backendE();
                    Entidad.Idc_perfilgpo = idcperfilgpo;
                    //llamamos el componente
                    Grupos_backendBL Componente = new Grupos_backendBL();
                    try
                    {
                        ds = Componente.grupos(Entidad);
                        int total = ds.Tables[0].Rows.Count;

                        if (total == 0)
                        {
                            msgbox.show("No se Encontraron Datos del Grupo", this.Page);
                        }
                        else
                        {
                            DataRow registro = ds.Tables[0].Rows[0];
                            //llenamos los controles
                            txtnombregpo.Text = registro["grupo"].ToString();
                            txtorden.Text = registro["orden"].ToString();
                            checklibre.Checked = Convert.ToBoolean(registro["libre"].ToString());
                            checkopciones.Checked = Convert.ToBoolean(registro["opciones"].ToString());
                            rbtnexterno.SelectedValue = registro["externo"].ToString();
                            if (checklibre.Checked)
                            {
                                panel_libre.Visible = true;
                                //llenar los datos internos
                                int min, max;
                                min = Convert.ToInt32(registro["minimo_libre"].ToString());
                                max = Convert.ToInt32(registro["maximo_libre"].ToString());
                                if (min > 0 || max > 0)
                                { // si maneja algun limite minimo maximo
                                    rbtnlimitelib.Items[1].Selected = false;
                                    rbtnlimitelib.Items[0].Selected = true;
                                    gpominmaxlibre.Visible = true;
                                    txtminlib.Text = min.ToString();
                                    txtmaxlib.Text = max.ToString();
                                }
                            }
                            //panel con opciones
                            if (checkopciones.Checked)
                            {
                                panel_opciones.Visible = true;
                                //llenar los datos internos
                                int min2, max2;
                                min2 = Convert.ToInt32(registro["minimo_opc"].ToString());
                                max2 = Convert.ToInt32(registro["maximo_opc"].ToString());
                                if (min2 > 0 || max2 > 0)
                                {//si maneja algun limite
                                    rbtnlimitopciones.Items[1].Selected = false;
                                    rbtnlimitopciones.Items[0].Selected = true;
                                    gpominmaxopciones.Visible = true;
                                    txtminopcs.Text = min2.ToString();
                                    txtmaxopcs.Text = max2.ToString();
                                }

                                //cargar tabla de opciones
                                DataTable tbl_opciones = ds.Tables[1];
                                DataView dv = new DataView(tbl_opciones);
                                dv.RowFilter = "borrado=0";
                                GridOpciones.DataSource = dv;
                                GridOpciones.DataBind();
                                //subimos a sesison
                                Session.Add("TablaOpcionGpo", tbl_opciones);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        msgbox.show(ex.Message, this.Page);
                    }
                }
            }
        }

        protected void checklibre_CheckedChanged(object sender, EventArgs e)
        {
            if (checklibre.Checked)
            {
                //mostramos el panel
                panel_libre.Visible = true;
            }
            else
            {
                //ocultamos el panel
                panel_libre.Visible = false;
            }
        }

        protected void checkopciones_CheckedChanged(object sender, EventArgs e)
        {
            if (checkopciones.Checked)
            {
                //mostramos el panel
                panel_opciones.Visible = true;
            }
            else
            {
                //ocultamos el panel
                panel_opciones.Visible = false;
            }
        }

        protected void rbtnlimitelib_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnlimitelib.SelectedValue == "1")
            {
                //mostramos panel
                gpominmaxlibre.Visible = true;
            }
            else if (rbtnlimitelib.SelectedValue == "0")
            {
                //ocultamos panel
                gpominmaxlibre.Visible = false;
            }
        }

        protected void rbtnlimitopciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnlimitopciones.SelectedValue == "1")
            {
                //mostramos panel
                gpominmaxopciones.Visible = true;
            }
            else if (rbtnlimitopciones.SelectedValue == "0")
            {
                //ocultamos panel
                gpominmaxopciones.Visible = false;
            }
        }

        //procedimientos creados por el usuario
        /// <summary>
        /// crea un datatable en session segun la tabla
        /// </summary>
        /// <param name="tabla"> nombre de la tabla</param>
        private void CreaDatatable(string tabla)
        {
            switch (tabla)
            {
                case "opciones":
                    DataTable tbl_opciones = new System.Data.DataTable();
                    tbl_opciones.Columns.Add("idc_perfilgpod", typeof(System.Int32));
                    tbl_opciones.Columns.Add("descripcion", typeof(System.String));
                    tbl_opciones.Columns.Add("borrado", typeof(System.Boolean));
                    tbl_opciones.Columns.Add("nuevo", typeof(System.Boolean));
                    Session.Add("TablaOpcionGpo", tbl_opciones);
                    break;
            }
        }

        protected void btnaceptaropcs_Click(object sender, EventArgs e)
        {
            if (txtopcsvalor.Text == string.Empty)
            {
                AlertError("Escriba un nombre por favor");
                return;
            }
            //recupero el valor del texbox
            string varopcion = txtopcsvalor.Text.Replace(';', ',');
            //agregamos un registro a nuestra tabla en memoria
            //bajamos nuestra tabla de la session
            DataTable tbl_opciones = (System.Data.DataTable)(Session["TablaOpcionGpo"]);
            //validar que no este repetido el campo

            bool duplicado = duplicadoEdicion("TablaOpcionGpo", "descripcion", varopcion, "");
            if (!duplicado && Session["opcion_id"] == null)
            {
                DataRow row = tbl_opciones.NewRow();
                int total_rows = tbl_opciones.Rows.Count;
                row["idc_perfilgpod"] = IdMaxOpcion() + 1;
                row["descripcion"] = txtopcsvalor.Text;
                row["nuevo"] = 1;
                row["borrado"] = 0;
                tbl_opciones.Rows.Add(row);
                DataView dv = new DataView(tbl_opciones);
                dv.RowFilter = "borrado=0";
                GridOpciones.DataSource = dv;
                GridOpciones.DataBind();
                //subimos a session nuestra tabla actualizada
                Session.Add("TablaOpcionGpo", tbl_opciones);
                //limpiamos la caja de text
                txtopcsvalor.Text = "";
            }
            if (Session["opcion_id"] != null)
            {
                foreach (DataRow row_old in tbl_opciones.Rows)
                {
                    if (row_old["descripcion"].ToString() == Session["descripcion"].ToString())
                    {
                        row_old["descripcion"] = txtopcsvalor.Text;
                        break;
                    }
                }

                DataView dv = new DataView(tbl_opciones);
                dv.RowFilter = "borrado=0";
                GridOpciones.DataSource = dv;
                GridOpciones.DataBind();
                //subimos a session nuestra tabla actualizada
                Session.Add("TablaOpcionGpo", tbl_opciones);
                //limpiamos la caja de text
                txtopcsvalor.Text = "";
                Session["opcion_id"] = null;
                Session["descripcion"] = null;
            }
        }

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
                    // msgbox.show("No puede haber opciones duplicadas.", this.Page);
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
        /// metodo que regresa el id mas alto del datatable de contacto
        /// </summary>
        /// <returns></returns>
        protected int IdMaxOpcion()
        {
            int idmax = 0;
            //bajamos nuestra tabla de la session
            DataTable tbl_opciones = (System.Data.DataTable)(Session["TablaOpcionGpo"]);
            if (tbl_opciones.Rows.Count > 0)
            {
                //revisamos
                int idval;
                foreach (DataRow fila in tbl_opciones.Rows)
                {
                    idval = Convert.ToInt32(fila["idc_perfilgpod"]);
                    if (idval > idmax)
                    {
                        idmax = idval;
                    }
                }
            }
            else
            {
                idmax = 0;
            }
            return idmax;
        }

        protected void GridOpciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //recuperamos el indice del registro seleccionado del grid de opciones
            int index = Convert.ToInt32(e.CommandArgument);
            int opcion_id = Convert.ToInt32(GridOpciones.DataKeys[index].Value);
            string descripcion = GridOpciones.DataKeys[index].Values["descripcion"].ToString();
            //bajamos nuestra tabla de la session
            DataTable tbl_opciones = (System.Data.DataTable)(Session["TablaOpcionGpo"]);
            //buscamos el registro
            DataRow[] fila = tbl_opciones.Select("idc_perfilgpod=" + opcion_id);
            switch (e.CommandName)
            {
                case "eliminarOpcion":
                    //mi codigo
                    //tbl_opciones.Rows.Remove(fila[0]);
                    fila[0]["borrado"] = 1;
                    //subimos a session nuestra tabla actualizada
                    Session.Add("TablaOpcionGpo", tbl_opciones);
                    //filtramos y llenamos el GridView
                    DataView dv = new DataView(tbl_opciones);
                    dv.RowFilter = "borrado=0";
                    GridOpciones.DataSource = dv;
                    GridOpciones.DataBind();
                    break;

                case "editar":
                    txtopcsvalor.Text = descripcion;
                    Session["opcion_id"] = opcion_id;
                    Session["descripcion"] = descripcion;
                    break;
            }
        }

        public void AlertError(String Mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "sweetAlert({    title: 'Oops!', text: '" + Mensaje + "', type: 'error'});", true);
        }

        /// <summary>
        /// GUARDAR GRUPO
        /// </summary>

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            //string total = TotalCadena("tabla_puesto").ToString();
            //string totalgpos= TotalCadena("tabla_grupos").ToString();
            //Alert.ShowAlertError("PUESTOS: "+total+" GPOS: "+totalgpos,this);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea guardar el Grupo " + txtnombregpo.Text + "?');", true);
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("grupos_backend.aspx");
        }

        /// <summary>
        /// metodo para generar las cadenas de contactos, telefonos y correos
        /// </summary>
        /// <param name=tabla>segun que cadena formaremos</param>
        /// <returns>la cadena formada id;desc</returns>
        protected string GeneraCadena(string tabla)
        {
            Boolean borrado, nuevo;
            tabla = tabla.ToString().ToLower();
            string cadena = "";
            switch (tabla)
            {
                case "opcion":
                    DataTable tbl_opciones = (System.Data.DataTable)(Session["TablaOpcionGpo"]);

                    foreach (DataRow fila in tbl_opciones.Rows)
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
                            cadena = cadena + fila["idc_perfilgpod"] + ";" + fila["descripcion"].ToString().Replace(";", ",") + ";" + "1" + ";";
                        }
                        //ACTUALIZAR
                        else
                        {
                            cadena = cadena + fila["idc_perfilgpod"] + ";" + fila["descripcion"].ToString().Replace(";", ",") + ";" + "0" + ";";
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
        protected int GeneraTotal(string tabla)
        {
            tabla = tabla.ToString().ToLower();
            int totrow = 0;
            switch (tabla)
            {
                case "opcion":
                    DataTable tbl_opciones = (System.Data.DataTable)(Session["TablaOpcionGpo"]);
                    totrow = tbl_opciones.Rows.Count;

                    break;
            }

            return totrow;
        }

        protected bool validar_campos()
        {
            //bool salida = true;
            if (string.IsNullOrEmpty(txtnombregpo.Text))
            {
                //mensaje
                Alert("Ingrese el nombre del Grupo", "Mensaje");
                //salida= false;
                return false;
            }

            if (checklibre.Checked & rbtnlimitelib.SelectedValue == "1")
            {
                //validar campos min max
                if (string.IsNullOrEmpty(txtminlib.Text) || string.IsNullOrEmpty(txtmaxlib.Text))
                {
                    Alert("Los valores mínimo y máximo no pueden estar vacios en ese caso debe poner cero", "Mensaje");
                    //salida = false;
                    return false;
                }
            }
            if (checkopciones.Checked & rbtnlimitopciones.SelectedValue == "1")
            {
                //validar campos min max
                if (string.IsNullOrEmpty(txtminopcs.Text) || string.IsNullOrEmpty(txtmaxopcs.Text))
                {
                    Alert("Los valores mínimo y máximo no pueden estar vacios en ese caso debe poner cero", "Mensaje");
                    //salida = false;
                    return false;
                }
            }

            //si tiene el checkbox opciones que tenga minimo una opcion y que el numero de opciones sea igual o mayo al limite maximo si es que maneja
            if (checkopciones.Checked)
            {
                int maxopc = txtmaxopcs.Text == "" ? 0 : Convert.ToInt32(txtmaxopcs.Text);
                int minopc = txtminopcs.Text == "" ? 0 : Convert.ToInt32(txtminopcs.Text);
                //if(maxopc>0){
                //cuantas opciones hay en la tabla
                //bajamos nuestra tabla de la session
                DataTable tbl_opciones = (System.Data.DataTable)(Session["TablaOpcionGpo"]);
                int totregistros = (tbl_opciones == null) ? 0 : tbl_opciones.Rows.Count;
                if (totregistros == 0)
                {
                    Alert("Debe tener mínimo una opción dada de alta.", "Mensaje");
                    //salida = false;
                    return false;
                }
                if (rbtnlimitopciones.SelectedValue == "1")
                {
                    //toma en cuenta los limites
                    if (maxopc > 0)
                    {
                        if (totregistros < maxopc)
                        {
                            Alert("El número de opciones debe ser igual o mayor al limite máximo de opciones", "Mensaje");
                            return false;
                        }
                    }
                }
                //
                //}
            }

            //validar minimos y maxim
            return true;
        }

        //Funcion que ejecuta alerta tipo Información. Se hereda de SweetAlert JS y se ejecuta desde el servidor con ScriptManager
        // @Mensaje: Type String.  Cuerpo del Mensaje
        // @Titulo:  Tyoe String.  Titulo del Mensaje
        // @URL:     Type String.  URL del icono
        public void AlertIcon(String Mensaje, String Titulo)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalerticon",
                "swal('" + Titulo + "', '" + Mensaje + "', 'success')", true);
        }

        //Funcion que ejecuta alerta tipo Información. Se hereda de SweetAlert JS y se ejecuta desde el servidor con ScriptManager
        // @Mensaje: Type String.  Cuerpo del Mensaje
        // @Titulo:  Tyoe String.  Titulo del Mensaje
        public void Alert(String Mensaje, String Titulo)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('" + Titulo + "','" + Mensaje + "')",
                true);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":

                    if (!validar_campos())
                    {
                        return;
                    }
                    //  recibimos valores
                    DataTable tbl_opciones = (System.Data.DataTable)(Session["TablaOpcionGpo"]);
                    bool libre = false;
                    bool opciones = false;
                    string cadenaopcs = "";
                    int minlibre, maxlibre, minopcs, maxopcs, totalopciones;
                    minlibre = 0;
                    maxlibre = 0;
                    minopcs = 0;
                    maxopcs = 0;
                    totalopciones = 0;
                    //  nombre
                    string nombre = txtnombregpo.Text;

                    //libre
                    if (checklibre.Checked)
                    { //si lo selecciono que tome en cuenta los valores dentro de ese panel
                        libre = true;
                        if (rbtnlimitelib.SelectedValue == "1")
                        { // quiere decir que llevara un limite y tomar encuenta los valores min max
                            minlibre = Convert.ToInt32(txtminlib.Text);
                            maxlibre = Convert.ToInt32(txtmaxlib.Text);
                            //si minimo es mayor a maximo a exepcion de cero se resetea a cero
                            if (minlibre > maxlibre & maxlibre > 0)
                            {
                                maxlibre = 0;
                            }
                        }
                    }

                    // opciones
                    if (checkopciones.Checked)
                    { //si lo selecciono que tome en cuenta los valores dentro del panel
                        opciones = true;
                        if (rbtnlimitopciones.SelectedValue == "1")
                        { //quiere decir que llevara un limite
                            minopcs = Convert.ToInt32(txtminopcs.Text);
                            maxopcs = Convert.ToInt32(txtmaxopcs.Text);
                            //si minimo es mayor a maximo a exepcion de cero se resetea a cero
                            if (minopcs > maxopcs & maxopcs > 0)
                            {
                                maxopcs = 0;
                            }
                        }

                        // llenado de opciones
                        cadenaopcs = GeneraCadena("opcion");
                        //total de opciones
                        totalopciones = GeneraTotal("opcion");
                    }

                    //Llenar la entidad
                    Grupos_backendE Entidad = new Grupos_backendE();
                    int id_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                    Entidad.Usuario = id_usuario;
                    Entidad.Idc_perfilgpo = Convert.ToInt32(ocidc_perfilgpo.Value);
                    Entidad.Grupo = txtnombregpo.Text;
                    Entidad.Libre = libre;
                    Entidad.Minimo_libre = minlibre;
                    Entidad.Maximo_libre = maxlibre;
                    Entidad.Opciones = opciones;
                    Entidad.Minimo_opc = minopcs;
                    Entidad.Maximo_opc = maxopcs;
                    Entidad.Cadena_Total = tbl_opciones.Rows.Count;
                    Entidad.Orden = txtorden.Text == "" ? 0 : Convert.ToInt32(txtorden.Text);
                    Entidad.Externo = Convert.ToBoolean(rbtnexterno.SelectedValue);
                    //SI ESTA CHEACADA LA OPCION METERA EN LA ENTIDAD LOS TOTALES Y LAS CADENAS CON OPCIONES
                    if (checkopciones.Checked)
                    {
                        //cadenaopcs = cadenaopcs.Replace(";", "");
                        Entidad.Cadena = cadenaopcs;
                        Entidad.Cadena_Total = totalopciones;
                    }
                    else
                    {
                        Entidad.Cadena = " ";
                        Entidad.Cadena_Total = 0;
                    }
                    //AlertIcon("", id_usuario.ToString() + " " + txtnombregpo.Text + " " + libre.ToString() + " " + minlibre.ToString() + " " + maxlibre.ToString() + " " + opciones.ToString() + " " + minopcs.ToString() + " " + maxopcs.ToString() + " " + tbl_opciones.Rows.Count.ToString()+" "+Entidad.Cadena+" "+Entidad.Cadena_Total.ToString());

                    //crear el componente
                    Grupos_backendBL Componente = new Grupos_backendBL();
                    DataSet ds = new DataSet();
                    ds = Componente.InsertarGrupos(Entidad);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                    {
                        //todo bien

                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "AlertOK('grupos_backend.aspx');", true);
                    }
                    else
                    {
                        //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                        msgbox.show(vmensaje, this.Page);
                        return;
                    }

                    break;
            }
        }
    }
}