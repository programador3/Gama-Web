using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class cursos_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                carga_examenes();
                Session["vidc"] = null;
                //eliminamos la carpeta temporal de este usuario
                DeleteDirectory(Path.Combine(Server.MapPath("~/temp/cursos/"), Session["sidc_usuario"].ToString()));
                llenar_grid_perfiles();
                if (Request.QueryString["borrador"] == null)// si el request viene vacio iniciamos en borrador
                {
                    cbxTipo.Checked = true;
                }
                else
                {//tomamos parametros y comprobamos
                    int uborrador = 0;
                    uborrador = Convert.ToInt32(Request.QueryString["borrador"]);
                    if (uborrador == 0)
                    {
                        cbxTipo.Checked = false; //produccion
                    }
                    else
                    {
                        cbxTipo.Checked = true; // borrador
                    }
                }

                //SEGUNDO PARAMETRO DE LA URL
                int uidc_curso_p = 0;
                int uidc_curso_borr = 0;
                if (cbxTipo.Checked)
                { //borrador recupera su parametro
                    if (!string.IsNullOrEmpty(Request.QueryString["uidc_curso_borr"]))
                    {
                        uidc_curso_borr = Convert.ToInt32(Request.QueryString["uidc_curso_borr"]);
                        //ponemos el titulo y los colores correspondientes
                        lit_titulo.Text = (uidc_curso_borr == 0) ? "Nuevo Curso" : "Edición de Curso";
                        lblmensaje.Text = "Borrador";
                        lblmensaje.CssClass = "label label-primary";
                        //mostramos el campo de observaciones
                        panel_obs.Visible = true;
                        //llenar el formulario para editar
                        if (uidc_curso_borr > 0)
                        {
                            oc_idc_curso_borr.Value = uidc_curso_borr.ToString();
                            //llamar la entidad
                            CursosE EntCurso = new CursosE();
                            EntCurso.Idc_curso_borr = uidc_curso_borr;
                            EntCurso.Borrador = true;
                            //ds
                            DataSet ds = new DataSet();

                            try
                            {
                                //componente
                                CursosCOM ComCurso = new CursosCOM();
                                ds = ComCurso.cursos(EntCurso);
                                int total = ds.Tables[0].Rows.Count;

                                if (total == 0)
                                {
                                    Alert.ShowAlertError("No se encontraron datos.", this.Page);
                                }
                                else
                                {
                                    DataRow registro = ds.Tables[0].Rows[0];
                                    //llenamos los controles

                                    txtdesc.Text = registro["descripcion"].ToString();
                                    txtobservaciones.Text = registro["observaciones"].ToString();
                                    cbox_tipocurso.SelectedValue = registro["tipo_curso"].ToString();
                                    //recuperamos el datatable
                                    DataTable tbl_cursos_perfil = ds.Tables[1];
                                    foreach (DataRow fila in tbl_cursos_perfil.Rows)
                                    {
                                        foreach (ListItem lista in check_curso_perfil.Items)
                                        {
                                            if (lista.Value.ToString() == fila["idc_puestoperfil"].ToString())
                                            {
                                                lista.Selected = true;
                                            }
                                        }
                                    }

                                    //llenamos el grid view

                                    Session["TablaCursoArc"] = ds.Tables[2];
                                    grid_cursos_archivos.DataSource = Session["TablaCursoArc"];//ds.Tables[2];
                                    grid_cursos_archivos.DataBind();
                                    carga_examenes();
                                    DataTable tabla_examenes = ds.Tables[3];
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
                            }
                            catch (Exception ex)
                            {
                                Alert.ShowAlertError(ex.Message, this.Page);
                            }
                        }
                        else
                        {
                            crearTabla();
                        }
                    }
                }
                else
                { //produccion
                    if (!string.IsNullOrEmpty(Request.QueryString["uidc_curso_p"]))
                    {
                        uidc_curso_p = Convert.ToInt32(Request.QueryString["uidc_curso_p"]);
                        //ponemos el titulo y los colores correspondientes
                        lit_titulo.Text = (uidc_curso_p == 0) ? "Nuevo Curso" : "Edición de Curso";
                        lblmensaje.Text = "Producción";
                        lblmensaje.CssClass = "label label-success";
                        //ocultamos las observaciones
                        panel_obs.Visible = false;
                        if (uidc_curso_p > 0)
                        {
                            //es edicion
                            //campo oculto
                            oc_idc_curso.Value = uidc_curso_p.ToString();
                            //llamamos a la entidad
                            CursosE EntCurso = new CursosE();
                            EntCurso.Idc_curso = uidc_curso_p;
                            EntCurso.Borrador = false;
                            //ds
                            DataSet ds = new DataSet();
                            try
                            {
                                //componente
                                CursosCOM ComCurso = new CursosCOM();
                                ds = ComCurso.cursos(EntCurso);
                                int total = ds.Tables[0].Rows.Count;

                                if (total == 0)
                                {
                                    Alert.ShowAlertError("No se encontraron datos.", this.Page);
                                }
                                else
                                {
                                    DataRow registro = ds.Tables[0].Rows[0];
                                    //llenamos los controles

                                    txtdesc.Text = registro["descripcion"].ToString();
                                    cbox_tipocurso.SelectedValue = registro["tipo_curso"].ToString();
                                    //recuperamos el datatable
                                    DataTable tbl_cursos_perfil = ds.Tables[1];
                                    foreach (DataRow fila in tbl_cursos_perfil.Rows)
                                    {
                                        foreach (ListItem lista in check_curso_perfil.Items)
                                        {
                                            if (lista.Value.ToString() == fila["idc_puestoperfil"].ToString())
                                            {
                                                lista.Selected = true;
                                            }
                                        }
                                    }

                                    //llenamos el grid view
                                    Session["TablaCursoArc"] = ds.Tables[2];
                                    grid_cursos_archivos.DataSource = Session["TablaCursoArc"];//ds.Tables[2];
                                    grid_cursos_archivos.DataBind();
                                    carga_examenes();
                                    DataTable tabla_examenes = ds.Tables[3];
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
                            }
                            catch (Exception ex)
                            {
                                Alert.ShowAlertError(ex.Message, this.Page);
                            }
                        }
                        else
                        {
                            crearTabla();
                        }
                    }
                }

                //add 18-09-2015
                if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_REFERER"]))
                {
                    oc_paginaprevia.Value = Request.ServerVariables["HTTP_REFERER"].ToString();
                }
                else
                {
                    oc_paginaprevia.Value = "cursos.aspx";
                }
            }
        }

        private void carga_examenes()
        {
            DataSet ds = new DataSet();
            PerfilesBL componente = new PerfilesBL();
            PerfilesE entidad = new PerfilesE();
            entidad.Ptipo = "C";
            ds = componente.tipo_examenes(entidad);
            //llenamos el checklist
            chxExamenes.DataTextField = "descripcion";
            chxExamenes.DataValueField = "idc_examen";

            chxExamenes.DataSource = ds.Tables[0];
            chxExamenes.DataBind();
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

        protected void llenar_grid_perfiles()
        {
            //dataset
            DataSet ds = new DataSet();
            //componente
            CursosCOM ComCurso = new CursosCOM();
            ds = ComCurso.cursos_perfiles_cbox();
            check_curso_perfil.DataSource = ds.Tables[0];
            check_curso_perfil.DataValueField = "idc_puestoperfil";
            check_curso_perfil.DataTextField = "descripcion";
            check_curso_perfil.DataBind();
            //grid_cursos_perfiles.DataSource = ds.Tables[0];
            //grid_cursos_perfiles.DataBind();
        }

        protected void btnGuardarTodo_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar el Curso ?');", true);
        }

        protected void guardar_borrador()
        {
            //validar
            if (validar() == false)
            {
                return;
            }
            //proseguimos
            string desc = txtdesc.Text;
            char tipo_curso = Convert.ToChar(cbox_tipocurso.SelectedValue.ToString());
            //perfiles
            string[] curso_per = cadena_curso_perfil_borr();
            string cad_curso_per = curso_per[0];
            int cad_curso_per_total = Convert.ToInt32(curso_per[1]);
            //archivos
            string[] curso_arch = cadena_curso_archivos_borr();
            string cad_curso_arch = curso_arch[0];
            int cad_curso_arch_total = Convert.ToInt32(curso_arch[1]);
            //entidad
            CursosE EntCurso = new CursosE();
            EntCurso.Idc_curso_borr = Convert.ToInt32(oc_idc_curso_borr.Value);
            EntCurso.Descripcion = desc;
            EntCurso.Tipo_curso = tipo_curso;
            EntCurso.Observaciones = txtobservaciones.Text;
            EntCurso.Aprobado = false;
            EntCurso.Pendiente = false;
            EntCurso.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            EntCurso.Cad_curso_perfil = cad_curso_per;
            EntCurso.Cad_curso_perfil_tot = cad_curso_per_total;
            EntCurso.Ptotal_examenes = TotalCadenaExamenes();
            EntCurso.Pcadena_examenes = GenerarCadenaExamenes();
            EntCurso.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
            EntCurso.Pnombrepc = funciones.GetPCName();//nombre pc usuario
            EntCurso.Pusuariopc = funciones.GetUserName();//usuario pc
            //
            EntCurso.Cad_curso_archivo = cad_curso_arch;
            EntCurso.Cad_curso_archivo_tot = cad_curso_arch_total;
            try
            {
                //dataset
                DataSet ds = new DataSet();
                //componente
                CursosCOM ComCurso = new CursosCOM();
                ds = ComCurso.cursosAgregarBorrador(EntCurso);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    int total = 0;
                    //GUARDAMOS LOS ARCHIVOS
                    //recuperos el datatable
                    if (ds.Tables.Count > 1)
                    {
                        DataTable tabla_bd = ds.Tables[1]; //este viene de la base de datos
                        DataTable tabla_temp = (DataTable)Session["TablaCursoArc"]; //este viene de la interfaz
                        DataRow[] fila_temp = tabla_temp.Select("nuevo=1");
                        //en teoria deben ser los mismos registros ordenados
                        //recorremos un datatable de la BD
                        int indice = 0;
                        total = tabla_temp.Rows.Count;
                        foreach (DataRow fila in tabla_bd.Rows)
                        {
                            //string fileName = tabla_temp.Rows[indice]["path"].ToString(); //archivo que se va a copiar se saca de la tabla temporal
                            string fileName = fila_temp[indice]["path"].ToString(); //archivo que se va a copiar se saca de la tabla temporal
                            string sourcePath = Path.Combine(Server.MapPath("~/temp/cursos/"), Session["sidc_usuario"].ToString());// Server.MapPath("~/temp/cursos/"); //carpeta temporal
                            string targetPath = fila["ruta"].ToString(); //carpeta de red de destino este lo retorna la BD
                            if (!System.IO.Directory.Exists(sourcePath))
                            {
                                Alert.ShowAlertError("no existe el path temporal", this.Page);
                                return;
                            }
                            if (!System.IO.Directory.Exists(targetPath))
                            {
                                Alert.ShowAlertError("no existe el path de destino", this.Page);
                                return;
                            }
                            //
                            // Use Path class to manipulate file and directory paths.
                            string fileNameNuevo = fila["idc_curso_arc"].ToString() + fila["extension"].ToString(); //el nombre de archivo de destino en la unidad de red es el id del registro+ la extension

                            string sourceFile = Path.Combine(sourcePath, fileName);
                            string destFile = Path.Combine(targetPath, fileNameNuevo);
                            //se copia el archivo
                            try
                            {
                                File.Copy(sourceFile, destFile, true);
                                indice = indice + 1;
                            }
                            catch (IOException copyError)
                            {
                                Alert.ShowAlertError(copyError.Message, this.Page);
                                return;
                            }

                            //fin
                        }
                    }
                    //es borrador
                    if (oc_idc_curso_borr.Value == "0")
                    { // es nuevo
                        int ter = (total * 1000) + 1000;
                        string t = ter.ToString();
                        Alert.ShowGiftMessage("Estamos procesando la cantidad de " + total.ToString() + " archivo(s) al Servidor.", "Espere un Momento", "cursos.aspx", "imagenes/loading.gif", t, "El Curso fue Guardado Correctamente", this);

                        limpiar();
                    }
                    else
                    { //es edicion
                        limpiar();
                        int ter = (total * 1000) + 1000;
                        string t = ter.ToString();
                        Alert.ShowGiftMessage("Estamos procesando la cantidad de " + total.ToString() + " archivo(s) al Servidor.", "Espere un Momento", "cursos.aspx", "imagenes/loading.gif", t, "El Curso fue Guardado Correctamente", this);
                    }
                }
                else
                {
                    limpiar();
                    Alert.ShowAlertError(vmensaje, this.Page);
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        /// <summary>
        /// este metodo es para guardar en produccion la variante es que llama un sp diferente
        /// </summary>
        protected void guardar_produccion()
        {
            //validar
            if (validar() == false)
            {
                return;
            }
            //proseguimos
            string desc = txtdesc.Text.ToUpper();
            char tipo_curso = Convert.ToChar(cbox_tipocurso.SelectedValue.ToString());
            //perfiles
            string[] curso_per = cadena_curso_perfil();
            string cad_curso_per = curso_per[0];
            int cad_curso_per_total = Convert.ToInt32(curso_per[1]);
            //entidad
            CursosE EntCurso = new CursosE();
            EntCurso.Idc_curso = Convert.ToInt32(oc_idc_curso.Value);
            EntCurso.Descripcion = desc;
            EntCurso.Tipo_curso = tipo_curso;
            EntCurso.Cad_curso_perfil = cad_curso_per;
            EntCurso.Cad_curso_perfil_tot = cad_curso_per_total;
            EntCurso.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            EntCurso.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
            EntCurso.Pnombrepc = funciones.GetPCName();//nombre pc usuario
            EntCurso.Pusuariopc = funciones.GetUserName();//usuario pc
            //
            EntCurso.Ptotal_examenes = TotalCadenaExamenes();
            EntCurso.Pcadena_examenes = GenerarCadenaExamenes();
            string[] curso_arc = cadena_curso_archivos();
            string cad_curso_arch = curso_arc[0];
            int cad_curso_arch_total = Convert.ToInt32(curso_arc[1]);
            EntCurso.Cad_curso_archivo = cad_curso_arch;
            EntCurso.Cad_curso_archivo_tot = cad_curso_arch_total;
            try
            {
                //dataset
                DataSet ds = new DataSet();
                //componente
                CursosCOM ComCurso = new CursosCOM();
                ds = ComCurso.cursosAgregar(EntCurso);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    //GUARDAMOS LOS ARCHIVOS
                    //recuperos el datatable
                    int total = 0;
                    if (ds.Tables.Count > 1)
                    {
                        DataTable tabla_bd = ds.Tables[1]; //este viene de la base de datos
                        DataTable tabla_temp = (DataTable)Session["TablaCursoArc"]; //este viene de la interfaz
                        //en teoria deben ser los mismos registros ordenados
                        //recorremos un datatable de la BD
                        int indice = 0;

                        total = tabla_temp.Rows.Count;
                        foreach (DataRow fila in tabla_bd.Rows)
                        {
                            string fileName = tabla_temp.Rows[indice]["path"].ToString(); //archivo que se va a copiar se saca de la tabla temporal
                            string sourcePath = Path.Combine(Server.MapPath("~/temp/cursos/"), Session["sidc_usuario"].ToString());// Server.MapPath("~/temp/cursos/"); //carpeta temporal
                            string targetPath = fila["ruta"].ToString(); //carpeta de red de destino este lo retorna la BD
                            if (!System.IO.Directory.Exists(sourcePath))
                            {
                                Alert.ShowAlertError("no existe el path temporal", this.Page);
                                return;
                            }
                            if (!System.IO.Directory.Exists(targetPath))
                            {
                                Alert.ShowAlertError("no existe el path de destino", this.Page);
                                return;
                            }
                            //
                            // Use Path class to manipulate file and directory paths.
                            string fileNameNuevo = fila["idc_curso_arc"].ToString() + fila["extension"].ToString(); //el nombre de archivo de destino en la unidad de red es el id del registro+ la extension

                            string sourceFile = Path.Combine(sourcePath, fileName);
                            string destFile = Path.Combine(targetPath, fileNameNuevo);
                            //se copia el archivo
                            try
                            {
                                File.Copy(sourceFile, destFile, true);
                                indice = indice + 1;
                            }
                            catch (IOException copyError)
                            {
                                Alert.ShowAlertError(copyError.Message, this.Page);
                                return;
                            }

                            //fin
                        }
                    }
                    if (oc_idc_curso.Value == "0")
                    {
                        int ter = (total * 1000) + 1000;
                        string t = ter.ToString();
                        Alert.ShowGiftMessage("Estamos procesando la cantidad de " + total.ToString() + " archivo(s) al Servidor.", "Espere un Momento", "cursos.aspx", "imagenes/loading.gif", t, "El Curso fue Guardado Correctamente", this);

                        limpiar();
                    }
                    else
                    {
                        limpiar();
                        int ter = (total * 1000) + 1000;
                        string t = ter.ToString();
                        Alert.ShowGiftMessage("Estamos procesando la cantidad de " + total.ToString() + " archivo(s) al Servidor.", "Espere un Momento", "cursos.aspx", "imagenes/loading.gif", t, "El Curso fue Guardado Correctamente", this);
                    }
                }
                else
                {
                    limpiar();
                    Alert.ShowAlertError(vmensaje, this.Page);
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        protected void limpiar()
        {
            txtdesc.Text = "";
            txtdescarchivo_1.Text = "";
            txtobservaciones.Text = "";
            DeleteDirectory(Path.Combine(Server.MapPath("~/temp/cursos/"), Session["sidc_usuario"].ToString()));
            DataTable tbl = (DataTable)Session["TablaCursoArc"];
            tbl.Clear();
            Session["TablaCursoArc"] = tbl;
            //volver a cargar en grid de perfiles
            llenar_grid_perfiles();
            update_grid_archivos();
            oc_idc_curso.Value = "0";
            oc_idc_curso_borr.Value = "0";
            //oc_paginaprevia.Value = "";
        }

        /// <summary>
        /// este metodo recorre el grid view de perfiles y genera la cadena correspondiente y el numero d elementos que selecciono
        /// mediante un array en la primer posicion esta la cedena y en el segundo el total
        /// </summary>
        /// <returns></returns>
        protected string[] cadena_curso_perfil()
        {
            //recorrer el gridview y ver cuantos fueron seleccionados
            string[] array = new string[2];
            string cadena = "";
            int total = 0;
            for (int i = 0; i < check_curso_perfil.Items.Count; i++)
            {
                //int idc_perfiletiq_opc = Convert.ToInt32(check_opcetiq.Items[i].Value);
                if (check_curso_perfil.Items[i].Selected)
                {
                    cadena = cadena + "0;0;" + check_curso_perfil.Items[i].Value.ToString() + ";0;";
                    total = total + 1;
                }
            }

            array[0] = cadena;
            array[1] = total.ToString();
            return array;
        }

        protected string[] cadena_curso_perfil_borr()
        {
            //recorrer el gridview y ver cuantos fueron seleccionados
            string[] array = new string[2];
            string cadena = "";
            int total = 0;
            for (int i = 0; i < check_curso_perfil.Items.Count; i++)
            {
                if (check_curso_perfil.Items[i].Selected)
                {
                    cadena = cadena + "0;0;0;0;" + check_curso_perfil.Items[i].Value.ToString() + ";0;";
                    total = total + 1;
                }
            }

            array[0] = cadena;
            array[1] = total.ToString();
            return array;
        }

        protected string[] cadena_curso_archivos_borr()
        {
            //recorrer el gridview y ver cuantos fueron seleccionados
            string[] array = new string[2];
            string cadena = "";
            int total = 0;
            //recorrer el datatable
            DataTable tbl_cursos_arc = (DataTable)Session["TablaCursoArc"];
            if (tbl_cursos_arc.Rows.Count > 0)
            {
                foreach (DataRow fila in tbl_cursos_arc.Rows)
                {
                    cadena = cadena + fila["id_archi"].ToString() + ";0;0;" + fila["descripcion"].ToString() + ";" + fila["extension"].ToString() + ";0;" + fila["nuevo"] + ";"; //modificar sp de add borraddor
                    total = total + 1;
                }
            }

            array[0] = cadena;
            array[1] = total.ToString();
            return array;
        }

        protected string[] cadena_curso_archivos()
        {
            //recorrer el gridview y ver cuantos fueron seleccionados
            string[] array = new string[2];
            string cadena = "";
            int total = 0;
            //recorrer el datatable
            DataTable tbl_cursos_arc = (DataTable)Session["TablaCursoArc"];
            if (tbl_cursos_arc.Rows.Count > 0)
            {
                foreach (DataRow fila in tbl_cursos_arc.Rows)
                {
                    cadena = cadena + fila["id_archi"] + ";0;" + fila["descripcion"].ToString() + ";" + fila["extension"].ToString() + ";0;" + fila["nuevo"] + ";";
                    total = total + 1;
                }
            }

            array[0] = cadena;
            array[1] = total.ToString();
            return array;
        }

        protected bool validar()
        {
            //descripcion no vacia
            if (txtdesc.Text == "")
            {
                Alert.ShowAlertError("La descripción del curso es obligatorío", this.Page);
                return false;
            }

            if (cbox_tipocurso.SelectedIndex == 0)
            {
                Alert.ShowAlertError("Debe seleccionar el tipo de curso", this.Page);
                return false;
            }

            return true;
        }

        protected void btnCancelarTodo_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect(oc_paginaprevia.Value);
        }

        protected void btnaddfile_Click(object sender, EventArgs e)
        {
            if (Session["vidc"] == null)
            {
                string nombre_archivo = subirArchivo();
                if (nombre_archivo != "")
                {
                    string desc_archivo = txtdescarchivo_1.Text;
                    //lo agregamos al datatable
                    DataTable tbl_cursos_arc = (DataTable)Session["TablaCursoArc"];
                    DataRow nuevafila = tbl_cursos_arc.NewRow();
                    if (cbxTipo.Checked)
                    { //borrador
                        nuevafila["id_archi"] = IdMaxOpcion() + 1;
                    }
                    else
                    {
                        nuevafila["id_archi"] = IdMaxOpcion() + 1;
                    }

                    nuevafila["path"] = nombre_archivo;
                    nuevafila["descripcion"] = txtdescarchivo_1.Text;
                    nuevafila["extension"] = Path.GetExtension(nombre_archivo);
                    nuevafila["nuevo"] = true;
                    nuevafila["borrado"] = false;
                    tbl_cursos_arc.Rows.Add(nuevafila);
                    tbl_cursos_arc.AcceptChanges();
                    Alert.ShowGift("Estamos subiendo el archivo.", "Espere un Momento", "imagenes/loading.gif", "4000", "Archivo Guardardo Correctamente", this);
                    //subimos a session
                    Session["TablaCursoArc"] = tbl_cursos_arc;
                    //actualizar el grid
                    update_grid_archivos();
                }

                txtdescarchivo_1.Text = "";
                irSeccion(txtdescarchivo_1.ClientID);
            }
            else
            {
                int id = Convert.ToInt32(Session["vidc"]);
                fileup_curso_arch_1.Enabled = true;
                DataTable tbl_cursos_arc = (DataTable)Session["TablaCursoArc"];
                foreach (DataRow row in tbl_cursos_arc.Rows)
                {
                    if (Convert.ToInt32(row["id_archi"]) == id)
                    {
                        row["descripcion"] = txtdescarchivo_1.Text.ToUpper();
                    }
                }
                Session["TablaCursoArc"] = tbl_cursos_arc;
                //actualizar el grid
                update_grid_archivos();
                lnkaddfile.Text = "Agregar";
                txtdescarchivo_1.Text = "";

                Alert.ShowGift("Estamos modificando el archivo.", "Espere un Momento", "imagenes/loading.gif", "4000", "Archivo Guardardo Correctamente", this);
                irSeccion(txtdescarchivo_1.ClientID);
                Session["vidc"] = null;
            }
        }

        protected void update_grid_archivos()
        {
            //
            grid_cursos_archivos.DataSource = (DataTable)Session["TablaCursoArc"];
            grid_cursos_archivos.DataBind();
        }

        protected string subirArchivo()
        {
            Boolean fileOK = false;
            // String path = Server.MapPath("~/temp/cursos/");

            if (fileup_curso_arch_1.HasFile)
            {
                //String fileExtension =
                //    System.IO.Path.GetExtension(fileup_curso_arch_1.FileName).ToLower();
                //String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                //for (int i = 0; i < allowedExtensions.Length; i++)
                //{
                //    if (fileExtension == allowedExtensions[i])
                //    {
                fileOK = true;
                //    }
                //}
            }

            if (fileOK)
            {
                try
                {
                    //creamos la carpeta para guardar las imagenes
                    DirectoryInfo path = new DirectoryInfo(Path.Combine(Server.MapPath("~/temp/cursos/"), Session["sidc_usuario"].ToString()));

                    //Path.Combine(Server.MapPath("~/temp/cursos/"));//, Session["sidc_usuario"].ToString()));
                    if (!path.Exists)
                    {
                        path.Create();
                    }
                    //Directory.CreateDirectory(path);
                    //revisar que no exista ya
                    if (File.Exists(path + Session["sidc_usuario"].ToString() + "_" + fileup_curso_arch_1.FileName))
                    {
                        //si existe no guardes
                        Alert.ShowAlertError("El archivo ya existe. Renombre el archivo  y vuelva a intentarlo.", this.Page);
                        return "";
                    }
                    else
                    {
                        fileup_curso_arch_1.PostedFile.SaveAs(path + "/" + Session["sidc_usuario"].ToString() + "_" + fileup_curso_arch_1.FileName);

                        return Session["sidc_usuario"].ToString() + "_" + fileup_curso_arch_1.FileName;
                    }
                }
                catch (Exception ex)
                {
                    Alert.ShowAlertError(ex.Message, this.Page);
                    return "";
                }
            }
            else
            {
                Alert.ShowAlertError("El archivo no cumple con el tipo de extensión válida", this.Page);
                return "";
            }
        }

        protected void crearTabla()
        {
            DataTable workTable = new DataTable("cursos_archivos");
            workTable.Columns.Add("id_archi", typeof(String));
            workTable.Columns.Add("path", typeof(String));
            workTable.Columns.Add("descripcion", typeof(String));
            workTable.Columns.Add("extension", typeof(String));
            workTable.Columns.Add("nuevo", typeof(Boolean));
            workTable.Columns.Add("borrado", typeof(Boolean));
            workTable.Columns.Add("dir_descarga", typeof(String));
            workTable.Columns.Add("dir_descarga_borr", typeof(String));
            //Session
            Session.Add("TablaCursoArc", workTable);
        }

        /// <summary>
        /// metodo que regresa el id mas alto del datatable de contacto
        /// </summary>
        /// <returns></returns>
        protected int IdMaxOpcion()
        {
            int idmax = 0;
            //bajamos nuestra tabla de la session
            DataTable tbl_opciones = (System.Data.DataTable)(Session["TablaCursoArc"]);
            if (tbl_opciones.Rows.Count > 0)
            {
                //revisamos
                int idval;
                foreach (DataRow fila in tbl_opciones.Rows)
                {
                    if (cbxTipo.Checked)
                    {
                        idval = Convert.ToInt32(fila["id_archi"]);
                    }
                    else
                    {
                        idval = Convert.ToInt32(fila["id_archi"]);
                    }
                    //idval = Convert.ToInt32(fila["idc_curso_arc"]);
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

        protected void grid_cursos_archivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //me causaba error cuando usaba el link ver..
            //recuperamos el indice del registro seleccionado del grid de Telefono
            int index = Convert.ToInt32(e.CommandArgument);
            string llave = (cbxTipo.Checked == true) ? "idc_curso_arc_borr" : "idc_curso_arc";
            int vidc = Convert.ToInt32(grid_cursos_archivos.DataKeys[index].Values["id_archi"]);
            string descripcion = grid_cursos_archivos.DataKeys[index].Values["descripcion"].ToString();
            Session["vidc"] = vidc;
            //Session["sidc_perfilgpo"] = Convert.ToInt32(vidc.ToString().Trim());
            switch (e.CommandName)
            {
                case "deletearchivo":
                    //eliminar archivo de temp y de datatable
                    eliminaCursoArchivo(vidc);
                    update_grid_archivos();

                    break;

                case "metodo_descarga":
                    // Retrieve the row that contains the button clicked
                    // by the user from the Rows collection.
                    GridViewRow row = grid_cursos_archivos.Rows[index];

                    LinkButton linkeo = (LinkButton)row.FindControl("linkdescarga");
                    string ruta_descarga = linkeo.Attributes["ruta_descarga"].ToString();
                    string nombre_archivo = linkeo.Attributes["nombre_archivo"].ToString();
                    DescargarArchivo(ruta_descarga, nombre_archivo);
                    break;

                case "Editar":
                    txtdescarchivo_1.Text = descripcion.ToUpper();
                    lnkaddfile.Text = "Actualizar";
                    fileup_curso_arch_1.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// elimina los archivos y el directorio especificado en la carpeta temporal.
        /// </summary>
        /// <param name="target_dir"></param>
        public static void DeleteDirectory(string target_dir)
        {
            if (Directory.Exists(target_dir))
            {
                string[] files = Directory.GetFiles(target_dir);
                string[] dirs = Directory.GetDirectories(target_dir);

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                }

                foreach (string dir in dirs)
                {
                    DeleteDirectory(dir);
                }

                Directory.Delete(target_dir, false);
            }
        }

        public void eliminaCursoArchivo(int id)
        {
            try
            {
                //bajamos el datatable
                DataTable tbl_cursos_arc = (DataTable)Session["TablaCursoArc"];
                //buscamos el registro
                string llave = (cbxTipo.Checked == true) ? "id_archi" : "id_archi";
                DataRow[] fila = tbl_cursos_arc.Select(llave + "=" + id);
                //eliminamos la imagen
                string ruta = Path.Combine(Server.MapPath("~/temp/cursos/"), Session["sidc_usuario"].ToString()) + "/" + fila[0]["path"].ToString();

                fila[0].Delete();
                fila[0].AcceptChanges();

                //subimos a session
                Session["TablaCursoArc"] = tbl_cursos_arc;
                irSeccion(grid_cursos_archivos.ClientID);
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        protected void irSeccion(string control_id)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "seccion", "irSeccion('#" + control_id + "');", true);
        }

        //protected void btnprueba_Click(object sender, EventArgs e)
        //{
        //    //EJECUTAMOS EL METODO DE VACIADO DE REGISTRO
        //    metodos Metodo = new metodos();
        //    Metodo.idc_aprobacion = 10;// Convert.ToInt32(tbl_aprobacionsolicitud.Rows[0]["idc_aprobacion"]);
        //    Metodo.idc_registro = 3; //Convert.ToInt32(tbl_aprobacionsolicitud.Rows[0]["idc_registro"]);
        //    Metodo.parametros = "aprobada;True;452";
        //    string vmensaje_3 = Metodo.ejecutar_metodo();
        //}

        protected void grid_cursos_archivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idc_curso_arc_borr = 0;
                DataRowView fila = (DataRowView)e.Row.DataItem;
                int idc_curso_arc = (fila["id_archi"] is DBNull) ? 0 : Convert.ToInt32(fila["id_archi"]);
                //si estamos en produccion la consulta no trae idc_curso_arc_borr
                //esta solo aplica cuando estamos en modo borrador
                if (cbxTipo.Checked)
                {
                    idc_curso_arc_borr = (fila["id_archi"] is DBNull) ? 0 : Convert.ToInt32(fila["id_archi"]);
                }

                string extension = fila["extension"].ToString();
                string archivo = "";
                string dir_descarga = "";
                if (fila["nuevo"].ToString() == "0")
                {
                    if (idc_curso_arc > 0)
                    {
                        dir_descarga = fila["dir_descarga"].ToString();
                        //utiliza la referencia del id del archivo que esta en produccion
                        archivo = idc_curso_arc.ToString() + extension;
                    }
                    else
                    {
                        dir_descarga = fila["dir_descarga_borr"].ToString();
                        archivo = idc_curso_arc_borr.ToString() + extension;
                    }
                }
                else
                {
                    dir_descarga = Path.Combine(Server.MapPath("~/temp/cursos/"), Session["sidc_usuario"].ToString()) + "/" + fila["path"].ToString();
                }

                //instanciamos el link
                //HyperLink link = (HyperLink)e.Row.FindControl("linkdescarga");
                LinkButton link = (LinkButton)e.Row.FindControl("linkdescarga");
                link.Attributes["ruta_descarga"] = dir_descarga;
                link.Attributes["nombre_archivo"] = archivo;
                //link.NavigateUrl = "http:"+dir_descarga+"\\"+archivo;
            }
        }

        protected void DescargarArchivo(string path_archivo, string nombre_archivo)
        {
            // Limpiamos la salida
            Response.Clear();
            // Con esto le decimos al browser que la salida sera descargable
            Response.ContentType = "application/octet-stream";
            // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(path_archivo));//Revision.pptx");
            // Escribimos el fichero a enviar
            Response.WriteFile(path_archivo);
            // volcamos el stream
            Response.Flush();
            // Enviamos todo el encabezado ahora
            Response.End();
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    if (cbxTipo.Checked)
                    { //esta en borrador
                        guardar_borrador();
                    }
                    else
                    { //produccion
                        guardar_produccion();
                    }
                    break;
            }
        }
    }
}