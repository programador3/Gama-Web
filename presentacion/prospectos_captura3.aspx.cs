using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class prospectos_captura3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!Page.IsPostBack)
            {
                if (Session["sidc_usuario"] == null)
                {
                    Response.Redirect("login.aspx");
                    return;
                }
                //valida si tiene permiso de ver esta pagina//
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());

                lblsesion.Text = Session["sidc_usuario"].ToString();
                if (Session["spagina"] != null)
                {
                    rutaoculta.Value = Session["spagina"].ToString();
                }
                else
                {
                    rutaoculta.Value = "menu.aspx";
                }
                int vidc;
                //validacion porque desde el menu dinamico no puedo mandar parametros.
                if (Request.QueryString["sidc_prospecto"] != null)
                {
                    vidc = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["sidc_prospecto"]));
                }
                else
                {
                    vidc = 0;
                }

                lblprospecto.Text = vidc.ToString().Trim();

                //cargar drop list de giros 30-09-2015
                cargarListaGiros();
                cargarListaFamiliaArt();
                cargarListaTipoDeObra();
                cargarListaEtapaDeObra();
                if (vidc == 0)
                {
                    lblenca.Text = "Captura de Prospectos";
                    //CODIGO NUEVO 17-04-2015
                    //creamos las tablas en memoria de nuestros GridView (contactos, telefonos, correos)
                    //tabla de contactos

                    CreaDatatable("contactos");

                    //tabla de telefonos

                    CreaDatatable("telefonos");

                    //tabla de correos

                    CreaDatatable("correos");

                    //add 30-09-2015
                    //tabla famart_det
                    CreaDatatable("famart_det");
                    //tabla famart_detmar
                    CreaDatatable("famart_detmar");

                    //esconder elementos del telefono y correo
                    OcultaElementos(true);
                    //FIN CODIGO NUEVO
                }
                else
                {
                    lblenca.Text = "Edición de Prospectos";
                    lblima1.Visible = false;
                    lblima2.Visible = false;
                    filimagen.Visible = false;
                    filimagen2.Visible = false;

                    //cargar datos
                    prospectos_ventasE llenar_datos = new prospectos_ventasE();
                    string vidc_string;
                    vidc_string = vidc.ToString();//Request.QueryString["idc_cliente"];
                    llenar_datos.Idc_prospecto = vidc;
                    DataSet ds = new DataSet();
                    Prospectos_ventasBL pros = new Prospectos_ventasBL();

                    try
                    {
                        ds = pros.datos_prospectos_ventas(llenar_datos);
                        int total = ds.Tables[0].Rows.Count;

                        if (total == 0)
                        {
                            //msgbox.show("No se Encontraron Datos del Prospecto", this.Page);
                            ScriptManager.RegisterStartupScript(this, GetType(), "tit", "mensaje_java('No se Encontraron Datos del Prospecto');", true);
                        }
                        else
                        {
                            txtcontacto.Text = Convert.ToString(ds.Tables[0].Rows[0]["contacto"]);
                            txtcorreo.Text = Convert.ToString(ds.Tables[0].Rows[0]["correo"]);
                            txtdireccion.Text = Convert.ToString(ds.Tables[0].Rows[0]["direccion"]);
                            //txtetapadeobra.Text = Convert.ToString(ds.Tables[0].Rows[0]["etapa_obra"]);
                            txtnombre.Text = Convert.ToString(ds.Tables[0].Rows[0]["nombre_razon_social"]);
                            txtobservaciones.Text = Convert.ToString(ds.Tables[0].Rows[0]["observacion"]);
                            //txttamanodeobra.Text = Convert.ToString(ds.Tables[0].Rows[0]["tamaño_obra"]).Trim();
                            txttelefono.Text = Convert.ToString(ds.Tables[0].Rows[0]["telefono"]).Trim();
                            txttipodeobra.Text = Convert.ToString(ds.Tables[0].Rows[0]["tipo_obra"]);

                            //mas obras
                            //agregar mas obras

                            int totalto;
                            string vto;
                            totalto = ds.Tables[1].Rows.Count;

                            for (int i = 0; i < totalto; i++)
                            {
                                vto = Convert.ToString(ds.Tables[1].Rows[i]["descripcion"]);
                                lsttipoobra.Items.Add(vto);
                            }
                            lsttipoobra.DataBind();
                            //new codigo 23-04-2015 :SSSSSS
                            //codigo add 01-10-2015
                            cboxgirocli.SelectedValue = ds.Tables[0].Rows[0]["idc_giroc"].ToString();
                            cboxtipodeobra.SelectedValue = ds.Tables[0].Rows[0]["idc_tipoobra"].ToString();
                            string tamañoobra = ds.Tables[0].Rows[0]["tamaño_obra"].ToString();
                            if (tamañoobra == "")
                            {
                                cboxobra_tam.SelectedIndex = 0;
                                cboxobra_tipotam.SelectedIndex = 0;
                            }
                            else
                            {
                                //descomponemos el string
                                //revisamos que tenga los corchetes
                                if (tamañoobra.Contains("[") & tamañoobra.Contains("]")) //esto pasa porque antes la entrada era libre y cuando quiere buscar los valores en los cbox marca el error.
                                {
                                    char[] delimiterChars = { '[' };
                                    string[] res = tamañoobra.Split(delimiterChars);
                                    cboxobra_tam.SelectedValue = res[0];
                                    cboxobra_tipotam.SelectedValue = res[1].Replace("]", "");
                                }
                                else
                                { //ponerlos en Seleccionar
                                    cboxobra_tam.SelectedIndex = 0;
                                    cboxobra_tipotam.SelectedIndex = 0;
                                }
                            }

                            //etapa de la obra
                            cboxetapaobra.SelectedValue = ds.Tables[0].Rows[0]["idc_etapaobra"].ToString();
                            //fin 01-10-2015
                            //creamos
                            DataTable tbl_contactos = new System.Data.DataTable();
                            DataTable tbl_telefonos = new System.Data.DataTable();
                            DataTable tbl_correos = new System.Data.DataTable();
                            //llenamos
                            tbl_contactos = ds.Tables[2];
                            tbl_telefonos = ds.Tables[3];
                            tbl_correos = ds.Tables[4];
                            //subimos
                            Session.Add("TablaContacto", tbl_contactos);
                            Session.Add("TablaTelefono", tbl_telefonos);
                            Session.Add("TablaCorreo", tbl_correos);
                            //cargamos GridViews
                            GridContacto.DataSource = tbl_contactos;
                            GridContacto.DataBind();

                            // codigo add 01-10-2015  llennar la familia art detalle y marca/distribucion
                            DataTable tbl_famart_det = ds.Tables[5];
                            DataTable tbl_famart_det_mar = ds.Tables[6];
                            //subimos
                            Session["TablaFamartDet"] = tbl_famart_det;
                            Session["TablaFamartDetmar"] = tbl_famart_det_mar;
                            //cargamos grid

                            grid_famart_det.DataSource = tbl_famart_det;
                            grid_famart_det.DataBind();
                            //ocultamos campos de telefono y correo
                            OcultaElementos(true);
                        }
                    }
                    catch (Exception ex)
                    {
                        //msgbox.show(ex.Message, this.Page);
                        ScriptManager.RegisterStartupScript(this, GetType(), "tit", "mensaje_java('" + ex.Message + "El Telefono debe Contener Solo Digitos (0-9), Y debe ser de 8 Digitos o 10 Digitos');", true);
                    }
                }
            }
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Guardar este Prospecto?','modal fade modal-info');", true);
        }

        protected bool validar()
        {
            if (cboxgirocli.SelectedIndex == 0)
            {
                Alert.ShowAlertError("Debe seleccionar un giro.", this.Page);
                return false;
            }

            if (txtdireccion.Text.Trim() == "")
            {
                Alert.ShowAlertError("Campo Dirección es Requerido", this.Page);
                txtdireccion.Focus();
                return false;
            }

            //new 21-04-2015
            if (!validaGrid())
            {
                Alert.ShowAlertError("Debe agregar por lo menos un contacto con un teléfono.", this.Page);
                return false;
            }

            //add 28-10-2015 validar tamaño de la obra
            if ((cboxobra_tam.SelectedIndex > 0 & cboxobra_tipotam.SelectedIndex == 0) || (cboxobra_tipotam.SelectedIndex > 0 & cboxobra_tam.SelectedIndex == 0))
            {
                Alert.ShowAlertError("Debe seleccionar el tipo de unidad y el tamaño de la obra por ejemplo Metros Cuadrados 600-650", this.Page);
                return false;
            }

            string vimagen = filimagen.FileName.ToString().ToUpper();
            if ((vimagen == "" | substraer.derecha(vimagen, 3) != "JPG") & filimagen.Visible == true)
            {
                Alert.ShowAlertError("Imagen de la Obra 1 es Requerido en Formato JPG", this.Page);
                return false;
            }

            string vimagen2 = filimagen2.FileName.ToString().ToUpper();
            if ((vimagen2 == "" | substraer.derecha(vimagen2, 3) != "JPG") & filimagen2.Visible == true)
            {
                Alert.ShowAlertError("Imagen de la Obra 2 es Requerido en Formato JPG", this.Page);
                return false;
            }

            return true;
        }

        protected void btnlimpiar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["sidc_prospecto"] != null)
            {
                Response.Redirect("prospectos_captura3.aspx?sidc_prospecto=" + Request.QueryString["sidc_prospecto"]);
            }
            else
            {
                Response.Redirect("prospectos_captura3.aspx");
            }
        }

        protected void limpiar()
        {
            txtdireccion.Text = "";
            txtnombre.Text = "";
            txtcontacto.Text = "";
            txttelefono.Text = "";
            txttipodeobra.Text = "";
            txtcorreo.Text = "";
            txtobservaciones.Text = "";

            ocidcontacto.Value = "";
            ocindicegridcontacto.Value = "";
            oc_gridfamartdet.Value = "-1";
            int titem = lsttipoobra.Items.Count;
            for (int x = 0; x < titem; x++)
            {
                lsttipoobra.Items.RemoveAt(lsttipoobra.Items.Count - 1);
            }

            lsttipoobra.DataBind();
            //new 23-04-2015
            //limpiamos sessiones del datatable
            //eliminamos tablas de session y las volvemos a crear
            EliminaDatatable("contactos");
            EliminaDatatable("telefonos");
            EliminaDatatable("correos");
            //creamos las tablas en memoria de nuestros GridView (contactos, telefonos, correos)
            CreaDatatable("contactos");
            CreaDatatable("telefonos");
            CreaDatatable("correos");
            //limpiar los gridview
            //limpiar los GridView
            DataTable table = new DataTable("TablaVacia");
            //contacto
            GridContacto.DataSource = table;
            GridContacto.DataBind();
            //telefono
            GridTelefono.DataSource = table;
            GridTelefono.DataBind();
            //correo
            GridCorreo.DataSource = table;
            GridCorreo.DataBind();
            //caja de texto de telefono y correo
            OcultaElementos(true);
            //latitud, longitud
            oclatitud.Value = "";
            oclongitud.Value = "";

            //combobox
            cboxgirocli.SelectedIndex = 0;
            cbox_famart.SelectedIndex = 0;
            cboxtipodeobra.SelectedIndex = 0;
            cboxobra_tam.SelectedIndex = 0;
            cboxobra_tipotam.SelectedIndex = 0;
            cboxetapaobra.SelectedIndex = 0;
            //grids
            grid_famart_det.DataSource = table;
            grid_famart_det.DataBind();
            //
            grid_famart_detmar.DataSource = table;
            grid_famart_detmar.DataBind();
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            Session["sidc_prospecto"] = null;
            string pagina = rutaoculta.Value;
            Response.Redirect(pagina);
        }

        protected void btnaddcontacto_Click(object sender, EventArgs e)
        {
            //recibimos el valor del campo de texto y reemplazamos
            string valtxtcontacto = txtcontacto.Text.Replace(';', ',');
            if (valtxtcontacto == "")
            {
                Alert.ShowAlertError("Ingrese un nombre para el contacto", this);
            }
            else
            {
                //agregamos un registro a nuestra tabla en memoria
                //bajamos nuestra tabla de la session
                DataTable tbl_contactos = (System.Data.DataTable)(Session["TablaContacto"]);
                //validar que no este repetido el campo
                bool duplicado = contactoNoRepetido(valtxtcontacto);
                if (!duplicado)
                {
                    DataRow row = tbl_contactos.NewRow();
                    int total_rows = tbl_contactos.Rows.Count;
                    row["contacto_id"] = IdMaxContacto() + 1;
                    row["contacto"] = valtxtcontacto;

                    tbl_contactos.Rows.Add(row);

                    GridContacto.DataSource = tbl_contactos;
                    GridContacto.DataBind();
                    //subimos a session nuestra tabla actualizada
                    Session.Add("TablaContacto", tbl_contactos);
                    //limpiamos la caja de text
                    txtcontacto.Text = "";
                }
                else
                {
                    Alert.ShowAlertError("No Puede haber contactos duplicados", this);
                    return;
                }
                //limpiar los GridView
                DataTable table = new DataTable("TablaVacia");
                GridTelefono.DataSource = table;
                GridTelefono.DataBind();
                //correo
                GridCorreo.DataSource = table;
                GridCorreo.DataBind();

                //oculto elementos y limpio
                OcultaElementos(true);
                txttelefono.Text = "";
                txtcorreo.Text = "";
            }
        }

        protected void SeleccionarContacto(int contacto_id)
        {
            //cargamos el datagrid de telefono
            DataTable tbl_telefonos = (System.Data.DataTable)(Session["TablaTelefono"]);
            //filtramos y llenamos el GridView
            DataView dv = new DataView(tbl_telefonos);
            dv.RowFilter = "contacto_id = " + contacto_id;
            GridTelefono.DataSource = dv;
            GridTelefono.DataBind();
            //CODIGO DE CORREO
            //cargamos el datagrid de telefono
            DataTable tbl_correos = (System.Data.DataTable)(Session["TablaCorreo"]);
            //filtramos y llenamos el GridView
            DataView dvcorreo = new DataView(tbl_correos);
            dvcorreo.RowFilter = "contacto_id = " + contacto_id;
            GridCorreo.DataSource = dvcorreo;
            GridCorreo.DataBind();
            //FIN
            //mostramos elementos del telefono y correo
            OcultaElementos(false);
        }

        protected void btnaddtel_Click(object sender, EventArgs e)
        {
            int contacto_id = 0;
            //pintamos de nuevo el registro seleccionado porque se refresca y lo despinta.
            //GridViewRow row = GridContacto.SelectedRow;
            //contacto_id = Convert.ToInt32(GridContacto.DataKeys[row.RowIndex].Value);
            int row = Convert.ToInt32(ocindicegridcontacto.Value);
            contacto_id = Convert.ToInt32(GridContacto.DataKeys[row].Value);
            //GridContacto.SelectedRow.BackColor = Color.FromName("#D7E8D7");
            //pintamos el grid
            GridContacto.Rows[row].BackColor = Color.FromName("#D7E8D7");
            //recibimos el telefono
            string valtxttelefono = txttelefono.Text.Replace(';', ',');
            //revisamos que no este vacio
            if (string.IsNullOrEmpty(valtxttelefono))
            {
                Alert.ShowAlertError("Ingrese un teléfono", this);
                txttelefono.Focus();
                return;
            }

            if (!validaTel(valtxttelefono))
            {
                Alert.ShowAlertError("El Telefono debe Contener Solo Digitos (0-9), Y debe ser de 8 Digitos o 10 Digitos", this);
                //ScriptManager.RegisterStartupScript(this, GetType(), "tit", "alert('El Telefono debe Contener Solo Digitos (0-9), Y debe ser de 8 Digitos o 10 Digitos')", true);
                txttelefono.Focus();
                return;
            }

            //agregamos un registro a nuestra tabla en memoria
            //bajamos nuestra tabla de la session
            DataTable tbl_telefonos = (System.Data.DataTable)(Session["TablaTelefono"]);

            //validar que no este repetido el campo
            bool duplicado = telefonoNoRepetido(valtxttelefono);

            if (duplicado)
            {
                Alert.ShowAlertError("No puede haber teléfonos duplicados.", this.Page);
                txttelefono.Focus();
                return;
            }

            //recuperamos el id y le sumamos uno
            int telefono_id = 0;
            if (tbl_telefonos.Rows.Count > 0)
            {
                DataRow ultimorow = tbl_telefonos.Rows[tbl_telefonos.Rows.Count - 1];
                telefono_id = Convert.ToInt32(ultimorow["telefono_id"]);
            }
            DataRow row_tel = tbl_telefonos.NewRow();
            //int total_rows = tbl_telefonos.Rows.Count;
            row_tel["telefono_id"] = telefono_id + 1;
            row_tel["contacto_id"] = contacto_id;
            row_tel["telefono"] = valtxttelefono;

            tbl_telefonos.Rows.Add(row_tel);
            //subimos a session nuestra tabla actualizada
            Session.Add("TablaTelefono", tbl_telefonos);

            //filtramos el datatable con RowFilter y llenamos el Grid
            DataView dv = new DataView(tbl_telefonos);
            dv.RowFilter = "contacto_id = " + contacto_id;
            GridTelefono.DataSource = dv;
            GridTelefono.DataBind();
            //limpiamos caja de texto
            txttelefono.Text = "";
        }

        protected void btnaddcorreo_Click(object sender, EventArgs e)
        {
            //recibimos el valor del correo
            string valtxtcorreo = txtcorreo.Text.Replace(';', ',');
            int contacto_id = 0;
            //pintamos de nuevo el registro seleccionado porque se refresca y lo despinta.
            //GridViewRow row = GridContacto.SelectedRow;
            //contacto_id = Convert.ToInt32(GridContacto.DataKeys[row.RowIndex].Value);
            int row = Convert.ToInt32(ocindicegridcontacto.Value);
            contacto_id = Convert.ToInt32(GridContacto.DataKeys[row].Value);
            //pintamos el grid
            GridContacto.Rows[row].BackColor = Color.FromName("#D7E8D7");
            //revisamos que no este vacio
            if (string.IsNullOrEmpty(valtxtcorreo))
            {
                Alert.ShowAlertError("Ingrese un correo", this.Page);
                txtcorreo.Focus();
                return;
            }

            if (!validaCorreo(valtxtcorreo))
            {
                // msgbox.show("Correo Invalido (Estructura Incorrecta)", this.Page);
                ScriptManager.RegisterStartupScript(this, GetType(), "tit", "mensaje_java('Correo Invalido (Estructura Incorrecta)');", true);
                txtcorreo.Focus();
                return;
            }
            //agregamos un registro a nuestra tabla en memoria
            //bajamos nuestra tabla de la session
            DataTable tbl_correos = (System.Data.DataTable)(Session["TablaCorreo"]);
            //validar que no este repetido el campo
            bool duplicado = false;
            if (tbl_correos.Rows.Count > 0)
            {
                foreach (DataRow fila in tbl_correos.Rows)
                {
                    if (string.Compare(txtcorreo.Text.Trim().ToLower(), fila["correo"].ToString().Trim().ToLower()) == 0)
                    {
                        duplicado = true;
                    }
                }
            }
            if (duplicado)
            {
                //msgbox.show("No puede haber correos duplicados.", this.Page);
                ScriptManager.RegisterStartupScript(this, GetType(), "tit", "mensaje_java('No Puede haber correos duplicados');", true);
                txtcorreo.Focus();
                return;
            }
            //recuperamos el id y le sumamos uno
            int correo_id = 0;
            if (tbl_correos.Rows.Count > 0)
            {
                DataRow ultimorow = tbl_correos.Rows[tbl_correos.Rows.Count - 1];
                correo_id = Convert.ToInt32(ultimorow["correo_id"]);
            }
            DataRow row_correo = tbl_correos.NewRow();

            row_correo["correo_id"] = correo_id + 1;
            row_correo["contacto_id"] = contacto_id;
            row_correo["correo"] = valtxtcorreo;

            tbl_correos.Rows.Add(row_correo);
            //subimos a session nuestra tabla actualizada
            Session.Add("TablaCorreo", tbl_correos);
            //filtramos el datatable con RowFilter y llenamos e Grid
            DataView dv = new DataView(tbl_correos);
            dv.RowFilter = "contacto_id = " + contacto_id;
            GridCorreo.DataSource = dv;
            GridCorreo.DataBind();
            //limpiamos caja de texto
            txtcorreo.Text = "";
        }

        //funciones definidas por el usuario
        protected void OcultaElementos(bool oculta)
        {
            if (oculta)
            {
                //esconde elementos del telefono
                lbltel.Visible = false;
                txttelefono.Visible = false;
                btnaddtel.Visible = false;
                //esconde elementos de correo
                lblcorreo.Visible = false;
                txtcorreo.Visible = false;
                btnaddcorreo.Visible = false;
            }
            else
            {
                //mostramos elementos del telefono
                lbltel.Visible = true;
                txttelefono.Visible = true;
                btnaddtel.Visible = true;
                //mostramos elementos del correo
                lblcorreo.Visible = true;
                txtcorreo.Visible = true;
                btnaddcorreo.Visible = true;
            }
        }

        /// <summary>
        /// metodo para generar las cadenas de contactos, telefonos y correos
        /// </summary>
        /// <param name="tabla">segun que cadena formaremos</param>
        /// <returns>la cadena formada id;desc</returns>
        protected string GeneraCadena(string tabla)
        {
            tabla = tabla.ToString().ToLower();
            string cadena = "";
            switch (tabla)
            {
                case "contacto":
                    DataTable tbl_contacto = (System.Data.DataTable)(Session["TablaContacto"]);

                    foreach (DataRow fila in tbl_contacto.Rows)
                    {
                        cadena = cadena + fila["contacto_id"].ToString() + ";" + fila["contacto"].ToString() + ";";
                    }
                    break;

                case "telefono":
                    DataTable tbl_telefono = (System.Data.DataTable)(Session["TablaTelefono"]);

                    foreach (DataRow fila in tbl_telefono.Rows)
                    {
                        cadena = cadena + fila["contacto_id"].ToString() + ";" + fila["telefono"].ToString() + ";";
                    }
                    break;

                case "correo":
                    DataTable tbl_correo = (System.Data.DataTable)(Session["TablaCorreo"]);

                    foreach (DataRow fila in tbl_correo.Rows)
                    {
                        cadena = cadena + fila["contacto_id"].ToString() + ";" + fila["correo"].ToString() + ";";
                    }
                    break;
            }

            return cadena;
        }

        /// <summary>
        ///  este metodo regresa el total de registros de una tabla (contactos, telefonos o correos).
        /// </summary>
        /// <param name="tabla">de que tabla queremos el resultado</param>
        /// <returns>entero numero de registros en total de n tabla</returns>
        protected int GeneraTotal(string tabla)
        {
            tabla = tabla.ToString().ToLower();
            int totrow = 0;
            switch (tabla)
            {
                case "contactos":
                    DataTable tbl_contacto = (System.Data.DataTable)(Session["TablaContacto"]);
                    totrow = tbl_contacto.Rows.Count;

                    break;

                case "telefonos":
                    DataTable tbl_telefono = (System.Data.DataTable)(Session["TablaTelefono"]);
                    totrow = tbl_telefono.Rows.Count;

                    break;

                case "correos":
                    DataTable tbl_correo = (System.Data.DataTable)(Session["TablaCorreo"]);
                    totrow = tbl_correo.Rows.Count;

                    break;
            }

            return totrow;
        }

        /// <summary>
        /// este metodo valida el formato de un telefono
        /// </summary>
        /// <param name="telefono">string telefono</param>
        /// <returns>true en caso de correcto y false en caso contrario</returns>
        protected bool validaTel(string telefono)
        {
            if ((telefono.Trim() == "") | (telefono.Trim().Length != 8 & telefono.Trim().Length != 10))
            // | (solo_digitos.checar(txttelefono.Text.Trim()) == false))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// este metodo valida el formato del correo
        /// </summary>
        /// <param name="correo">string correo</param>
        /// <returns>true en caso de correcto y false en caso contrario</returns>
        protected bool validaCorreo(string correo)
        {
            if (correo.Trim().Length > 0)
            {
                Match match;
                match = validar_expresiones_regulares.comparar(correo.Trim(), 2);
                if (match.Success)
                {
                    return true;
                }
            }
            return false;
        }

        protected bool validaGrid()
        {
            //para agregar un telefono, primero tiene que agregar un contacto
            int totcont = GeneraTotal("contactos");
            if (totcont > 0)
            {
                //validamos que por lo menos haya un telefono
                int tottel = GeneraTotal("telefonos");
                if (tottel > 0)
                {
                    //genial
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// metodo que revisa en el datatable que no exista otro contacto con el nombre que se le pasa
        /// </summary>
        /// <param name="contacto">string</param>
        /// <returns>booleano</returns>
        protected bool contactoNoRepetido(string contacto)
        {
            DataTable tbl_contactos = (System.Data.DataTable)(Session["TablaContacto"]);
            //validar que no este repetido el campo
            bool duplicado = false;
            if (tbl_contactos.Rows.Count > 0)
            {
                foreach (DataRow fila in tbl_contactos.Rows)
                {
                    if (string.Compare(contacto.Trim().ToLower(), fila["contacto"].ToString().Trim().ToLower()) == 0)
                    {
                        duplicado = true;
                    }
                }
            }
            return duplicado;
        }

        /// <summary>
        /// metodo que revisa en el datatable que no exista otro telefono con el que se le pasa
        /// </summary>
        /// <param name="telefono">int</param>
        /// <returns>booleano</returns>
        protected bool telefonoNoRepetido(string telefono)
        {
            DataTable tbl_telefonos = (System.Data.DataTable)(Session["TablaTelefono"]);
            bool duplicado = false;
            if (tbl_telefonos.Rows.Count > 0)
            {
                foreach (DataRow fila in tbl_telefonos.Rows)
                {
                    if (string.Compare(telefono.Trim().ToLower(), fila["telefono"].ToString().Trim().ToLower()) == 0)
                    {
                        duplicado = true;
                    }
                }
            }
            return duplicado;
        }

        /// <summary>
        /// crea un datatable en session segun la tabla
        /// </summary>
        /// <param name="tabla"> nombre de la tabla</param>
        private void CreaDatatable(string tabla)
        {
            switch (tabla)
            {
                case "contactos":
                    DataTable tbl_contactos = new System.Data.DataTable();
                    tbl_contactos.Columns.Add("contacto_id", typeof(System.Int32));
                    tbl_contactos.Columns.Add("contacto", typeof(System.String));
                    Session.Add("TablaContacto", tbl_contactos);
                    break;

                case "telefonos":
                    //tabla de telefonos
                    DataTable tbl_telefonos = new System.Data.DataTable();
                    tbl_telefonos.Columns.Add("telefono_id", typeof(System.Int32));
                    tbl_telefonos.Columns.Add("contacto_id", typeof(System.Int32));
                    tbl_telefonos.Columns.Add("telefono", typeof(System.String));
                    Session.Add("TablaTelefono", tbl_telefonos);
                    break;

                case "correos":
                    //tabla de correos
                    DataTable tbl_correos = new System.Data.DataTable();
                    tbl_correos.Columns.Add("correo_id", typeof(System.Int32));
                    tbl_correos.Columns.Add("contacto_id", typeof(System.Int32));
                    tbl_correos.Columns.Add("correo", typeof(System.String));
                    Session.Add("TablaCorreo", tbl_correos);
                    break;
                //add 30-09-2015
                case "famart_det":
                    DataTable tbl_famart_det = new System.Data.DataTable();
                    tbl_famart_det.Columns.Add("idc_prospecto_famartd", typeof(System.Int32));
                    tbl_famart_det.Columns.Add("idc_prospecto_famart", typeof(System.Int32));
                    //esta columna no pertenece a la tabla es solo para mostrar el nombre del registro del catalogo
                    tbl_famart_det.Columns.Add("descripcion", typeof(System.String));
                    tbl_famart_det.Columns.Add("nuevo", typeof(System.Boolean));
                    //

                    Session.Add("TablaFamartDet", tbl_famart_det);
                    break;

                case "famart_detmar":
                    DataTable tbl_famart_detmar = new System.Data.DataTable();
                    tbl_famart_detmar.Columns.Add("idc_prospecto_famartdm", typeof(System.Int32));
                    tbl_famart_detmar.Columns.Add("idc_prospecto_famartd", typeof(System.Int32));
                    tbl_famart_detmar.Columns.Add("marca", typeof(System.String));
                    tbl_famart_detmar.Columns.Add("distribuidor", typeof(System.String));
                    tbl_famart_detmar.Columns.Add("nuevo", typeof(System.Boolean));
                    tbl_famart_detmar.Columns.Add("precio", typeof(System.Decimal));
                    Session.Add("TablaFamartDetmar", tbl_famart_detmar);
                    break;
            }
        }

        /// <summary>
        /// elimina la session donde esta guardada un datatable
        /// </summary>
        /// <param name="tabla">nombre de la tabla</param>
        private void EliminaDatatable(string tabla)
        {
            switch (tabla)
            {
                case "contactos":
                    Session.Remove("TablaContacto");
                    break;

                case "telefonos":
                    Session.Remove("TablaTelefono");
                    break;

                case "correos":
                    Session.Remove("TablaCorreo");
                    break;
            }
        }

        /// <summary>
        /// metodo que regresa el id mas alto del datatable de contacto
        /// </summary>
        /// <returns></returns>
        protected int IdMaxContacto()
        {
            int idmax = 0;
            //bajamos nuestra tabla de la session
            DataTable tbl_contactos = (System.Data.DataTable)(Session["TablaContacto"]);
            if (tbl_contactos.Rows.Count > 0)
            {
                //revisamos
                int idval;
                foreach (DataRow fila in tbl_contactos.Rows)
                {
                    idval = Convert.ToInt32(fila["contacto_id"]);
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

        /// <summary>
        /// este metodo aplica para cuando se de clic en cancelar contacto y lo mismo haga cuando termine de actualizar
        /// </summary>
        protected void cancelarContacto()
        {
            //limpiamos el texbox
            txtcontacto.Text = "";
            //limpiamos el campo oculto por si las dudas
            ocidcontacto.Value = "";
            ocindicegridcontacto.Value = "";
            //ocultamos este boton cancelar
            btncancelcontacto.Visible = false;
            //ocultamos el boton de actualizar
            btneditacontacto.Visible = false;
            //mostramos el boton de agregar contacto
            btnaddcontacto.Visible = true;
            //ocultamos las cajas de texto tel y correo
            OcultaElementos(true);
            txttelefono.Text = "";
            txtcorreo.Text = "";
        }

        protected void btnaddobras_Click(object sender, EventArgs e)
        {
            string tipodeobra;
            tipodeobra = txtaddtipoobra.Text.Trim().ToUpper();
            if (tipodeobra == "")
            {
                //msgbox.show("Necesitas Capturar un valor", this.Page);
                ScriptManager.RegisterStartupScript(this, GetType(), "tit", "mensaje_java('Necesita Capturar un Valor');", true);
                txtaddtipoobra.Focus();
                return;
            }
            lsttipoobra.Items.Add(tipodeobra);
            lsttipoobra.DataBind();
            txtaddtipoobra.Text = "";
            btndelobra.Visible = true;
        }

        protected void btndelobra_Click(object sender, EventArgs e)
        {
            if (lsttipoobra.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "tit", "mensaje_java('Necesita Seleccionar un Valor');", true);
                lsttipoobra.Focus();
                return;
            }
            else
            {
                lsttipoobra.Items.RemoveAt(lsttipoobra.SelectedIndex);
                lsttipoobra.DataBind();

                if (lsttipoobra.Items.Count == 0)
                { btndelobra.Visible = false; }
            }
        }

        protected void GridContacto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //recuperamos el id del registro seleccionado del grid de Contacto
            int index = Convert.ToInt32(e.CommandArgument);

            int contacto_id = Convert.ToInt32(GridContacto.DataKeys[index].Value);

            //bajamos nuestra tabla de la session
            DataTable tbl_contactos = (System.Data.DataTable)(Session["TablaContacto"]);
            //buscamos el registro
            DataRow[] fila = tbl_contactos.Select("contacto_id=" + contacto_id);
            switch (e.CommandName)
            {
                case "clicContacto":
                case "editarContacto":
                    //mi codigo
                    //guardamos el indice en un lbloculto
                    ocindicegridcontacto.Value = index.ToString();
                    txtcontacto.Text = fila[0]["contacto"].ToString();
                    //guardamos el id del registro en un campo oculto
                    ocidcontacto.Value = Convert.ToString(contacto_id);
                    SeleccionarContacto(contacto_id);
                    //mostramos el boton de actualizar
                    btneditacontacto.Visible = true;
                    //escondemos el de agregar
                    btnaddcontacto.Visible = false;
                    //mostramos el de cancelar contacto
                    btncancelcontacto.Visible = true;
                    //pintamos el grid
                    GridContacto.Rows[index].BackColor = Color.FromName("#D7E8D7");
                    break;

                case "eliminarContacto":
                    //mi codigo
                    tbl_contactos.Rows.Remove(fila[0]);
                    GridContacto.DataSource = tbl_contactos;
                    GridContacto.DataBind();
                    //subimos a session nuestra tabla actualizada
                    Session.Add("TablaContacto", tbl_contactos);

                    //ELIMINAR LOS TELEFONOS DEL CONTACTO
                    //bajamos nuestra tabla de la session
                    DataTable tbl_telefonos = (System.Data.DataTable)(Session["TablaTelefono"]);
                    //buscamos el registro
                    DataRow[] fila_tel = tbl_telefonos.Select("contacto_id=" + contacto_id);
                    for (int i = 0; i < fila_tel.Count(); i++)
                    {
                        tbl_telefonos.Rows.Remove(fila_tel[i]);
                    }
                    //subimos a session nuestra tabla actualizada
                    Session.Add("TablaTelefono", tbl_telefonos);
                    //filtramos y llenamos el GridView
                    DataView dv = new DataView(tbl_telefonos);
                    dv.RowFilter = "contacto_id = " + contacto_id;
                    GridTelefono.DataSource = dv;
                    GridTelefono.DataBind();

                    //ELIMINAR LOS CORREOS DEL CONTACTO
                    //bajamos nuestra tabla de la session
                    DataTable tbl_correos = (System.Data.DataTable)(Session["TablaCorreo"]);
                    //buscamos el registro
                    DataRow[] fila_correo = tbl_correos.Select("contacto_id=" + contacto_id);
                    for (int i = 0; i < fila_correo.Count(); i++)
                    {
                        tbl_correos.Rows.Remove(fila_correo[i]);
                    }
                    //subimos a session nuestra tabla actualizada
                    Session.Add("TablaCorreo", tbl_correos);
                    //filtramos y llenamos el GridView
                    DataView dvcorreo = new DataView(tbl_correos);
                    dvcorreo.RowFilter = "contacto_id = " + contacto_id;
                    GridCorreo.DataSource = dvcorreo;
                    GridCorreo.DataBind();

                    //por ultimo revisamos que si ya no hay contactos entonces oculte telefono y correo
                    if (tbl_contactos.Rows.Count == 0)
                    {
                        OcultaElementos(true);
                        //y limpiamos las cajas de texto
                        txttelefono.Text = "";
                        txtcorreo.Text = "";
                    }
                    break;
            }
        }

        protected void btncancelcontacto_Click(object sender, EventArgs e)
        {
            cancelarContacto();
            //limpiar los GridView
            DataTable table = new DataTable("TablaVacia");
            GridTelefono.DataSource = table;
            GridTelefono.DataBind();
            //correo
            GridCorreo.DataSource = table;
            GridCorreo.DataBind();
        }

        protected void btneditacontacto_Click(object sender, EventArgs e)
        {
            //recibimos la info
            string valcontacto = txtcontacto.Text; //contacto
            int idvalcontacto = Convert.ToInt32(ocidcontacto.Value); //id que se guardo en un campo oculto.

            DataTable tbl_contactos = (System.Data.DataTable)(Session["TablaContacto"]);
            //validar que no este repetido el campo
            bool duplicado = false;
            foreach (DataRow fila in tbl_contactos.Rows)
            {
                //revisamos que no este repetido y que no se este comparando asi mismo.
                if (string.Compare(valcontacto.Trim().ToLower(), fila["contacto"].ToString().Trim().ToLower()) == 0 && Convert.ToInt32(fila["contacto_id"].ToString()) != idvalcontacto)
                {
                    duplicado = true;
                }
            }
            if (!duplicado)
            {
                //si no esta duplicado continuamos.
                for (int i = 0; i < tbl_contactos.Rows.Count; i++)
                { //revisar si hay una mejor forma de actualizar que hacer este for :|
                    if (Convert.ToInt32(tbl_contactos.Rows[i]["contacto_id"]) == idvalcontacto)
                    {
                        tbl_contactos.Rows[i]["contacto"] = valcontacto; //aqui cambiamos por el nuevo valor
                    }
                }

                GridContacto.DataSource = tbl_contactos;
                GridContacto.DataBind();
                //subimos a session nuestra tabla actualizada
                Session.Add("TablaContacto", tbl_contactos);
                //este metodo lo usamos para limpiar todo
                cancelarContacto(); //no cancela la accion solo limpia.
            }
            else
            {
                //msgbox.show("No puede haber contactos duplicados.", this.Page);
                ScriptManager.RegisterStartupScript(this, GetType(), "tit", "mensaje_java('No Puede haber contactos duplicados');", true);
                return;
            }
            //limpiar los GridView
            DataTable table = new DataTable("TablaVacia");
            GridTelefono.DataSource = table;
            GridTelefono.DataBind();
            //correo
            GridCorreo.DataSource = table;
            GridCorreo.DataBind();
        }

        protected void GridTelefono_GridTelefono_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //recuperamos el indice del registro seleccionado del grid de Telefono
            int index = Convert.ToInt32(e.CommandArgument);
            int telefono_id = Convert.ToInt32(GridTelefono.DataKeys[index].Value);

            //bajamos nuestra tabla de la session
            DataTable tbl_telefonos = (System.Data.DataTable)(Session["TablaTelefono"]);
            //buscamos el registro
            DataRow[] fila = tbl_telefonos.Select("telefono_id=" + telefono_id);
            switch (e.CommandName)
            {
                case "eliminarTelefono":
                    //mi codigo
                    tbl_telefonos.Rows.Remove(fila[0]);
                    //subimos a session nuestra tabla actualizada
                    Session.Add("TablaTelefono", tbl_telefonos);
                    //filtramos y llenamos el GridView
                    //recuperamos el id del registro seleccionado del grid de Contacto
                    int row = Convert.ToInt32(ocindicegridcontacto.Value);
                    int contacto_id = Convert.ToInt32(GridContacto.DataKeys[row].Value);
                    DataView dv = new DataView(tbl_telefonos);
                    dv.RowFilter = "contacto_id = " + contacto_id;
                    GridTelefono.DataSource = dv;
                    GridTelefono.DataBind();
                    break;
            }
        }

        protected void GridCorreo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //recuperamos el indice del registro seleccionado del grid del Correo
            int index = Convert.ToInt32(e.CommandArgument);
            int correo_id = Convert.ToInt32(GridCorreo.DataKeys[index].Value);

            //bajamos nuestra tabla de la session
            DataTable tbl_correos = (System.Data.DataTable)(Session["TablaCorreo"]);
            //buscamos el registro
            DataRow[] fila = tbl_correos.Select("correo_id=" + correo_id);
            switch (e.CommandName)
            {
                case "eliminarCorreo":
                    //mi codigo
                    tbl_correos.Rows.Remove(fila[0]);
                    //subimos a session nuestra tabla actualizada
                    Session.Add("TablaCorreo", tbl_correos);
                    //filtramos y llenamos el GridView
                    //recuperamos el id del registro seleccionado del grid de Contacto
                    int row = Convert.ToInt32(ocindicegridcontacto.Value);
                    int contacto_id = Convert.ToInt32(GridContacto.DataKeys[row].Value);
                    DataView dv = new DataView(tbl_correos);
                    dv.RowFilter = "contacto_id = " + contacto_id;
                    GridCorreo.DataSource = dv;
                    GridCorreo.DataBind();
                    break;
            }
        }

        protected void GridContacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //recuperamos el id del registro seleccionado del grid de Contacto
            GridViewRow row = GridContacto.SelectedRow;
            int contacto_id = Convert.ToInt32(GridContacto.DataKeys[row.RowIndex].Value);
            SeleccionarContacto(contacto_id);
            //limpiamos el texbox
            txtcontacto.Text = "";
            //limpiamos el campo oculto por si las dudas
            ocidcontacto.Value = "";
            //ocultamos este boton cancelar
            btncancelcontacto.Visible = false;
            //ocultamos el boton de actualizar
            btneditacontacto.Visible = false;
            //mostramos el boton de agregar contacto
            btnaddcontacto.Visible = true;
        }

        protected void linkhome_Click(object sender, EventArgs e)
        {
            Response.Redirect("menu3.aspx");
        }

        protected void btnregresar_Click(object sender, ImageClickEventArgs e)
        {
            string pag = rutaoculta.Value;
            Response.Redirect(pag);
        }

        protected void cargarListaGiros()
        {
            try
            {
                //componente
                Giros_cliBL componente = new Giros_cliBL();
                DataSet ds = new DataSet();
                ds = componente.giro_cli_cbox(true);
                //cargar el cbox
                cboxgirocli.DataSource = ds.Tables[0];
                cboxgirocli.DataValueField = "idc_giroc";
                cboxgirocli.DataTextField = "descripcion";
                cboxgirocli.DataBind();
                cboxgirocli.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            catch (Exception ex)
            {
                //msgbox.show(ex.Message, this.Page);
                ScriptManager.RegisterStartupScript(this, GetType(), "tit", "mensaje_java('" + ex.Message + "');", true);
            }
        }

        protected void cargarListaFamiliaArt()
        {
            try
            {
                //componente
                Prospectos_ventasBL componente = new Prospectos_ventasBL();
                DataSet ds = new DataSet();
                ds = componente.familiaArticulos_cbox();
                //cargar el cbox

                cbox_famart.DataSource = ds.Tables[0];
                cbox_famart.DataValueField = "idc_prospecto_famart";
                cbox_famart.DataTextField = "descripcion";
                cbox_famart.DataBind();
                cbox_famart.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            catch (Exception ex)
            {
                //msgbox.show(ex.Message, this.Page);
                ScriptManager.RegisterStartupScript(this, GetType(), "tit", "mensaje_java('" + ex.Message + "');", true);
            }
        }

        protected void cargarListaTipoDeObra()
        {
            try
            {
                //componente
                Prospectos_ventasBL componente = new Prospectos_ventasBL();
                DataSet ds = new DataSet();
                ds = componente.tipos_obras_cbox();
                //cargar el cbox

                cboxtipodeobra.DataSource = ds.Tables[0];
                cboxtipodeobra.DataValueField = "idc_tipoobra";
                cboxtipodeobra.DataTextField = "descripcion";
                cboxtipodeobra.DataBind();
                cboxtipodeobra.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            catch (Exception ex)
            {
                //msgbox.show(ex.Message, this.Page);
                ScriptManager.RegisterStartupScript(this, GetType(), "tit", "mensaje_java('" + ex.Message + "');", true);
            }
        }

        protected void cargarListaEtapaDeObra()
        {
            try
            {
                //componente
                Prospectos_ventasBL componente = new Prospectos_ventasBL();
                DataSet ds = new DataSet();
                ds = componente.etapas_obras_cbox();
                //cargar el cbox

                cboxetapaobra.DataSource = ds.Tables[0];
                cboxetapaobra.DataValueField = "idc_etapaobra";
                cboxetapaobra.DataTextField = "descripcion";
                cboxetapaobra.DataBind();
                cboxetapaobra.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            catch (Exception ex)
            {
                //msgbox.show(ex.Message, this.Page);
                ScriptManager.RegisterStartupScript(this, GetType(), "tit", "mensaje_java('" + ex.Message + "');", true);
            }
        }

        protected void cbox_famart_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshGrid_Famart();
        }

        protected void btnaddartdet_Click(object sender, EventArgs e)
        {
            //recuperar valores del texbox
            string marca = txtmarca.Text;
            string distribuidor = txtdistribuidor.Text;
            string precio = (txtprecio.Text == "") ? "0.00" : txtprecio.Text.Replace("$", "").Replace(",", "");
            //int idc_prospecto_famart = Convert.ToInt32(cbox_famart.SelectedValue);
            int index = Convert.ToInt32(oc_gridfamartdet.Value);

            int idc_prospecto_famart = Convert.ToInt32(grid_famart_det.DataKeys[index].Value);
            //que no esten vacios
            if (marca.Trim() == "" & distribuidor.Trim() == "" & precio == "0.00")
            {
                grid_famart_det.Rows[index].BackColor = Color.FromName("#D7E8D7");
                panel_art.Visible = false;
                return;
            }
            //bajamos la tabla
            DataTable tbl_famartdetmar = (DataTable)(Session["TablaFamartDetmar"]);
            //agregamos el registro
            DataRow nuevafila = tbl_famartdetmar.NewRow();
            nuevafila["idc_prospecto_famartdm"] = consecutivo("TablaFamartDetmar", "idc_prospecto_famartdm");
            nuevafila["idc_prospecto_famartd"] = idc_prospecto_famart;//idRegistroSeleccionado();//este ahorita lo recuper
            nuevafila["marca"] = marca;
            nuevafila["distribuidor"] = distribuidor;
            nuevafila["precio"] = precio;
            nuevafila["nuevo"] = 1;

            tbl_famartdetmar.Rows.Add(nuevafila);
            tbl_famartdetmar.AcceptChanges();
            //subimos a session
            Session["TablaFamartDetmar"] = tbl_famartdetmar;
            //actualizamos el grid
            refreshGrid_FamartMarca();
            //limpio las cajas de texto
            txtmarca.Text = "";
            txtdistribuidor.Text = "";
            txtprecio.Text = "";
            // }
            grid_famart_det.Rows[index].BackColor = Color.FromName("#D7E8D7");
            panel_art.Visible = false;
        }

        /// <summary>
        /// agregado el 30-09-2015
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

        /// <summary>
        /// esta funcion revisa que valor esta seleccionado en el combobox de famart y busca en la tabla temporal
        /// el id (PK) segun el valor seleccionado en el combobox
        /// </summary>
        protected int idRegistroSeleccionado()
        {
            int idregistro = 0;
            if (cbox_famart.SelectedIndex > 0)
            { //quiere decir que hay uno seleciconado que no es "Seleccionar"
                //bajamos la tabla
                DataTable tbl_famartdet = (DataTable)(Session["TablaFamartDet"]);
                //tiene registros ?
                if (tbl_famartdet.Rows.Count > 0)
                {
                    //buscamos
                    int id_famart = Convert.ToInt32(cbox_famart.SelectedValue);
                    DataRow[] fila = tbl_famartdet.Select("idc_prospecto_famart=" + id_famart);
                    if (fila.Length == 1)
                    {
                        idregistro = Convert.ToInt32(fila[0]["idc_prospecto_famartd"]);
                    }
                }
            }
            return idregistro;
        }

        protected void grid_famart_detmar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //me causaba error cuando usaba el link ver..
            //recuperamos el indice del registro seleccionado del grid de Telefono
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid_famart_detmar.DataKeys[index].Value);
            //Session["sidc_perfilgpo"] = Convert.ToInt32(vidc.ToString().Trim());
            switch (e.CommandName)
            {
                case "eliminamarca":
                    refreshGrid_FamartMarca();
                    eliminaMarca(vidc);
                    //actualiza grid
                    refreshGrid_FamartMarca();
                    int index_famartdet = Convert.ToInt32(oc_gridfamartdet.Value);
                    grid_famart_det.Rows[index_famartdet].BackColor = Color.FromName("#D7E8D7");
                    break;
            }
        }

        protected void grid_famart_det_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //me causaba error cuando usaba el link ver..
            //recuperamos el indice del registro seleccionado del grid de Telefono
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid_famart_det.DataKeys[index].Value);
            //Session["sidc_perfilgpo"] = Convert.ToInt32(vidc.ToString().Trim());
            switch (e.CommandName)
            {
                case "clic_famartdet":
                    //pintamos el grid
                    //ocultamos el index del grid
                    oc_gridfamartdet.Value = index.ToString();
                    grid_famart_det.Rows[index].BackColor = Color.FromName("#D7E8D7");
                    panel_art.Visible = true;
                    refreshGrid_FamartMarca();
                    break;

                case "eliminafamarticulo":
                    eliminaFamArt(vidc);
                    //actualiza grid
                    oc_gridfamartdet.Value = "-1";
                    refreshGrid_Famart();
                    refreshGrid_FamartMarca();
                    //cualquier eliminacion ocultamos la captura
                    panel_art.Visible = false;
                    break;
            }
        }

        protected void eliminaMarca(int id)
        {
            try
            {
                //bajamos la tabla de session
                DataTable tbl_famartdetmar = (DataTable)(Session["TablaFamartDetmar"]);
                //buscamos el registro
                DataRow[] fila = tbl_famartdetmar.Select("idc_prospecto_famartdm=" + id);

                fila[0].Delete();
                fila[0].AcceptChanges();

                //subimos a session
                Session["TablaFamartDetmar"] = tbl_famartdetmar;
            }
            catch (Exception ex)
            {
                //msgbox.show(ex.Message, this.Page);
                ScriptManager.RegisterStartupScript(this, GetType(), "tit", "mensaje_java('" + ex.Message + "');", true);
            }
        }

        protected void eliminaFamArt(int id)
        {
            try
            {
                //bajamos la tabla
                DataTable tbl_famartdet = (DataTable)(Session["TablaFamartDet"]);
                //buscamos el registro
                DataRow[] fila = tbl_famartdet.Select("idc_prospecto_famartd=" + id);
                // eliminarlo completamente del datatable
                fila[0].Delete();
                fila[0].AcceptChanges();
                //subimos a session
                Session["TablaFamartDet"] = tbl_famartdet;
                //ahora borramos los hijos que son la de marca/ditr
                //bajamos la tabla de session
                DataTable tbl_famartdetmar = (DataTable)(Session["TablaFamartDetmar"]);
                if (tbl_famartdetmar.Rows.Count > 0)
                {
                    //buscamos el registro
                    DataRow[] fila_hijo = tbl_famartdetmar.Select("idc_prospecto_famartd=" + id);
                    if (fila_hijo.Length > 0)
                    {
                        //recorremos el ciclo
                        for (int i = 0; i < fila_hijo.Length; i++)
                        {
                            fila_hijo[i].Delete();
                            fila_hijo[i].AcceptChanges();
                        }

                        //subimos a session
                        Session["TablaFamartDetmar"] = tbl_famartdetmar;
                    }
                }
            }
            catch (Exception ex)
            {
                //msgbox.show(ex.Message, this.Page);
                ScriptManager.RegisterStartupScript(this, GetType(), "tit", "mensaje_java('" + ex.Message + "');", true);
            }
        }

        /// <summary>
        /// refresca el grid correspondiente primero revisa en tabla en session despues filtra que no este borrado
        /// y se llena el grid
        /// </summary>
        protected void refreshGrid_Famart()
        {
            DataTable tbl_famartdet = (DataTable)(Session["TablaFamartDet"]);
            //DataView dv = tbl_famartdet.DefaultView;
            //dv.RowFilter= "borrado=false";
            grid_famart_det.DataSource = tbl_famartdet;
            grid_famart_det.DataBind();
        }

        protected void refreshGrid_FamartMarca()
        {
            int id_filtro = 0;
            if (grid_famart_det.Rows.Count > 0)
            {
                int index = Convert.ToInt32(oc_gridfamartdet.Value);
                if (index > -1)
                {
                    int seleccionado = Convert.ToInt32(grid_famart_det.DataKeys[index].Value);
                    if (seleccionado > 0)
                    {
                        id_filtro = seleccionado;
                    }
                }
            }
            DataTable tbl_famartdetmar = (DataTable)(Session["TablaFamartDetmar"]);
            DataView dv = tbl_famartdetmar.DefaultView;
            dv.RowFilter = "idc_prospecto_famartd=" + id_filtro;
            grid_famart_detmar.DataSource = dv;
            grid_famart_detmar.DataBind();
        }

        /// <summary>
        /// metodo para generar las cadenas  01-10-2015
        /// </summary>
        /// <param name=tabla>segun que cadena formaremos</param>
        /// <returns>la cadena formada id;desc</returns>
        protected string[] GeneraCadenaMejorada(string tabla)
        {
            string cadena = "";
            int contador = 0;
            string[] resultado = new string[2];
            switch (tabla)
            {
                case "TablaFamartDet":
                    DataTable tbl_famartdet = (DataTable)(Session["TablaFamartDet"]);

                    foreach (DataRow fila in tbl_famartdet.Rows)
                    {
                        cadena = cadena + fila["idc_prospecto_famartd"] + ";" + fila["idc_prospecto_famart"] + ";" + fila["nuevo"] + ";";
                        contador = contador + 1;
                    }
                    break;

                case "TablaFamartDetmar":
                    DataTable tbl_famartdetmar_2 = (DataTable)(Session["TablaFamartDetmar"]);
                    foreach (DataRow fila in tbl_famartdetmar_2.Rows)
                    {
                        cadena = cadena + fila["idc_prospecto_famartdm"] + ";" + fila["idc_prospecto_famartd"] + ";" + fila["marca"].ToString().Replace(";", ",") + ";" + fila["distribuidor"].ToString().Replace(";", ",") + ";" + fila["nuevo"] + ";" + fila["precio"].ToString() + ";";
                        contador = contador + 1;
                    }
                    break;
            }

            resultado[0] = cadena;
            resultado[1] = contador.ToString();
            return resultado;
        }

        protected void btnaddfamart_Click(object sender, EventArgs e)
        {
            if (cbox_famart.SelectedIndex > 0)
            {
                //agregamos el registro al grid principal
                //bajar la tabla de session
                DataTable tbl_famartdet = (DataTable)(Session["TablaFamartDet"]);
                DataRow[] rowvalidar = tbl_famartdet.Select("idc_prospecto_famart=" + cbox_famart.SelectedValue);
                if (rowvalidar.Length == 0)
                {
                    //actualizar el datatable
                    DataRow nuevafila = tbl_famartdet.NewRow();
                    nuevafila["idc_prospecto_famartd"] = consecutivo("TablaFamartDet", "idc_prospecto_famartd");
                    nuevafila["idc_prospecto_famart"] = cbox_famart.SelectedValue;
                    nuevafila["descripcion"] = cbox_famart.SelectedItem;
                    nuevafila["nuevo"] = 1;

                    tbl_famartdet.Rows.Add(nuevafila);
                    tbl_famartdet.AcceptChanges();
                    //subir la tabla a session
                    Session["TablaFamartDet"] = tbl_famartdet;
                }

                // panel_art.Visible = true;
            }
            else
            {
                panel_art.Visible = false;
            }

            //update al gridview
            refreshGrid_Famart();
            oc_gridfamartdet.Value = "-1";
            //y de los hijos
            refreshGrid_FamartMarca();
        }

        protected void btncancelartdet_Click(object sender, EventArgs e)
        {
            panel_art.Visible = false;
            txtmarca.Text = "";
            txtdistribuidor.Text = "";
            txtprecio.Text = "";
            int index = Convert.ToInt32(oc_gridfamartdet.Value);
            grid_famart_det.Rows[index].BackColor = Color.FromName("#D7E8D7");
        }

        /// <summary>
        /// recupera el id del ultimo registro que no sea nuevo de familia articulos detalle
        /// </summary>
        protected int ultimoid()
        {
            //bajamos la tabla de session de famart detalle
            int id = 0;
            DataTable tbl_famartdet = (DataTable)(Session["TablaFamartDet"]);
            if (tbl_famartdet.Rows.Count > 0)
            {
                DataRow[] row = tbl_famartdet.Select("", "idc_prospecto_famartd DESC");
                if (row.Length > 0)
                {
                    id = Convert.ToInt32(row[0]["idc_prospecto_famartd"]);
                }
            }

            return id;
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            if (validar() == false)
            { return; }
            //si todo paso bien, ahora a guardar
            string vdireccion, vnombre, vcontacto, vtelefono, vtipo_obra, vcorreo, vtamañoobra, vetapaobra, vobservacion;
            vdireccion = txtdireccion.Text.Trim().ToUpper();
            vnombre = txtnombre.Text.Trim().ToUpper();
            vcontacto = txtcontacto.Text.Trim();
            vtelefono = txttelefono.Text.Trim().ToUpper();
            vtipo_obra = txttipodeobra.Text.Trim().ToUpper();
            vcorreo = txtcorreo.Text.Trim().ToLower();
            //vtamañoobra = txttamanodeobra.Text.Trim().ToUpper();
            vetapaobra = txtetapadeobra.Text.Trim().ToUpper();
            vobservacion = txtobservaciones.Text.Trim().ToUpper();
            //01-10-2015
            //add 28-10-2015
            if (cboxobra_tam.SelectedIndex == 0 & cboxobra_tipotam.SelectedIndex == 0)
            {
                vtamañoobra = ""; //ira vacia
            }
            else
            {
                vtamañoobra = cboxobra_tam.SelectedValue + "[" + cboxobra_tipotam.SelectedValue + "]";
            }

            //mas obras
            int titem;
            string cadobras;
            cadobras = "";
            titem = lsttipoobra.Items.Count;
            for (int i = 0; i < titem; i++)
            {
                string vtex = lsttipoobra.Items[i].Text.Replace(";", ",");
                cadobras = cadobras + vtex + ";";
            }
            // cadena de contacto, telefono y correo 20-04-2015
            string cadcontacto, cadtelefono, cadcorreo;
            cadcontacto = GeneraCadena("contacto");
            cadtelefono = GeneraCadena("telefono");
            cadcorreo = GeneraCadena("correo");
            int totcontactos, tottelefonos, totcorreos;
            totcontactos = GeneraTotal("contactos");
            tottelefonos = GeneraTotal("telefonos");
            totcorreos = GeneraTotal("correos");
            // cadena de contacto, telefono y correo
            //si no puede obtener las coordenadas manda null
            string latitud, longitud;
            bool convertir;
            if (oclatitud.Value.ToString() != "" && oclongitud.Value.ToString() != "")
            {
                latitud = oclatitud.Value.ToString();
                longitud = oclongitud.Value.ToString();
                convertir = true;
            }
            else
            {
                latitud = null;
                longitud = null;
                convertir = false;
            }

            // add 01-10-2015
            int idgiroc = Convert.ToInt32(cboxgirocli.SelectedValue);
            int idctipoobra = Convert.ToInt32(cboxtipodeobra.SelectedValue);
            int idetapaobra = Convert.ToInt32(cboxetapaobra.SelectedValue);
            //msgbox.show(cadobras,this.Page);

            prospectos_ventasE llenar_datos = new prospectos_ventasE();

            llenar_datos.Idc_usuario = Convert.ToInt32(lblsesion.Text);
            llenar_datos.Direccion = vdireccion;
            llenar_datos.Nombre_razon_social = vnombre;
            //llenar_datos.Contacto = vcontacto;
            llenar_datos.Contacto = "";
            //llenar_datos.Telefono = vtelefono;
            llenar_datos.Telefono = "";
            llenar_datos.Tipo_obra = vtipo_obra;
            //llenar_datos.Correo = vcorreo;
            llenar_datos.Correo = "";
            llenar_datos.Tamaño_obra = vtamañoobra;
            llenar_datos.Etapa_obra = vetapaobra;
            llenar_datos.Observacion = vobservacion;
            llenar_datos.Idc_prospecto = Convert.ToInt32(lblprospecto.Text);
            llenar_datos.Totalobras = titem;
            llenar_datos.Masobras = cadobras;
            //new 20-04-2015
            llenar_datos.Cad_con = cadcontacto;
            llenar_datos.Cad_con_tot = totcontactos;
            llenar_datos.Cad_tel = cadtelefono;
            llenar_datos.Cad_tel_tot = tottelefonos;
            llenar_datos.Cad_cor = cadcorreo;
            llenar_datos.Cad_cor_tot = totcorreos;
            //new 29-04-2015
            if (lblprospecto.Text == "0" && convertir == true)
            {
                llenar_datos.Latitud = Convert.ToDecimal(latitud);
                llenar_datos.Longitud = Convert.ToDecimal(longitud);
            }

            //new 01-10-2015
            string[] res_famartdet = GeneraCadenaMejorada("TablaFamartDet");
            string[] res_famartdetmarca = GeneraCadenaMejorada("TablaFamartDetmar");
            //
            llenar_datos.Cadena_famartdet = res_famartdet[0];
            llenar_datos.Cadena_famartdet_total = Convert.ToInt32(res_famartdet[1]);
            //
            llenar_datos.Cadena_famartdet_marca = res_famartdetmarca[0];
            llenar_datos.Cadena_famartdet_marca_total = Convert.ToInt32(res_famartdetmarca[1]);
            // add 01-10-2015
            llenar_datos.Idc_giroc = idgiroc;
            llenar_datos.Idc_tipoobra = idctipoobra;
            //
            llenar_datos.Idc_etapaobra = idetapaobra;

            //llenar_datos.Cadena_famartdet = GeneraCadena("");

            DataSet ds = new DataSet();

            Prospectos_ventasBL prosp = new Prospectos_ventasBL();

            try
            {
                ds = prosp.alta_prospectos_ventas(llenar_datos);

                int total;
                total = ds.Tables[0].Rows.Count;

                string vmensaje;
                vmensaje = Convert.ToString(ds.Tables[0].Rows[0]["mensaje"]);

                if (vmensaje == "")
                {
                    string pagina = rutaoculta.Value;
                    if (filimagen.Visible == true) // grabar si es visible objeto imagen
                    {
                        string vruta, vruta2;
                        vruta = Convert.ToString(ds.Tables[0].Rows[0]["ruta"]);
                        vruta2 = Convert.ToString(ds.Tables[0].Rows[0]["ruta2"]);
                        //COPIAR FOtO
                        filimagen.SaveAs(vruta);
                        filimagen2.SaveAs(vruta2);
                        pagina = "prospectos_captura3.aspx";
                    }
                    Alert.ShowGiftMessage("Estamos Detectando su Ubicación y Subiendo el Prospecto...",
                        "Espere un Momento", pagina, "imagenes/loading.gif", "3000", "Datos del Propecto Guardados Correctamente.", this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this.Page);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                //  Console.Write(ex.Message);
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }
    }
}