using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class visualizar_perfil : System.Web.UI.Page
    {
        public static int RequestId = 0;
        public string ValueLabel = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
                int idc_opcion = 603;  //pertenece al modulo de grupos backend
                if (funciones.permiso(idc_usuario, idc_opcion) == false)
                {
                    Response.Redirect("menu.aspx");
                    return;
                }
                if (Request.QueryString["id_descripcion"] == null)
                {
                    if (Request.QueryString["id_puesto"] == null)//Si variable request es nulla
                    {
                        ReturnPrevius();
                    }
                    else
                    {
                        if (Request.QueryString["id_puesto"].ToString().Equals(""))//Si variable request es vacia
                        {
                            ImageButton1.Visible = false;
                            ImageButton2.Visible = false;
                            ImageButton3.Visible = false;
                            ImageButton4.Visible = false;
                            ImageButton5.Visible = false;
                            ReturnPrevius();
                        }
                        else//Si contiene un dato ejecuto inicio
                        {
                            RequestId = Convert.ToInt32(Request.QueryString["id_puesto"].ToString());
                            CargarDatosPuesto(RequestId);
                        }
                    }
                }
                else
                {//TOMO CADENA QUE VIENE EN FORMA HEXADECIMAL Y LA TRANSFORMO EN STRING PARA MANDARLA COMO PARAMETRO
                    String RequestIdDescripcion = Request.QueryString["id_descripcion"].ToString();
                    String ID_Descripcion = "";
                    while (RequestIdDescripcion.Length > 0)
                    {
                        ID_Descripcion += System.Convert.ToChar(System.Convert.ToUInt32(RequestIdDescripcion.Substring(0, 2), 16)).ToString();
                        RequestIdDescripcion = RequestIdDescripcion.Substring(2, RequestIdDescripcion.Length - 2);
                    }
                    //Alert.ShowAlertError(ID_Descripcion,this);
                    CargarDatosPuesto_Descripcion(ID_Descripcion);
                }
            }
        }

        /// <summary>
        /// Funcion que toma nombre de pagina anterior con variable de servidor y la manda a una
        /// funcion javascript
        /// </summary>
        private void ReturnPrevius()
        {
            String PreviousPage = Request.ServerVariables["HTTP_REFERER"];
            if (PreviousPage == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('organigrama.aspx');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('" + PreviousPage + "');", true);
            }
        }

        /// <summary>
        /// Funcion que carga los datos con un request
        /// </summary>
        /// <param name="ID">ID DEL PUESTO</param>
        public void CargarDatosPuesto(int ID)
        {
            try
            {
                CargaPerfil_EN Entidad = new CargaPerfil_EN();
                //tomo el valor del ID
                Entidad.Idc_puesto = ID;
                //Creo mi componente
                CargaPerfil_CM Componente = new CargaPerfil_CM();
                //Cargo mi componenet e en un dataset
                DataSet ds = new DataSet();
                ds = Componente.CargaPuestosPerfil(Entidad);
                //comprobamos que el dataset no este vacio, si esta vacio es que no hay ningun perfil relacionado
                if (ds.Tables[0].Rows.Count == 0)
                {
                    ReturnPrevius();
                    ImageButton1.Visible = false;
                    ImageButton2.Visible = false;
                    ImageButton3.Visible = false;
                    ImageButton4.Visible = false;
                    ImageButton5.Visible = false;
                }
                else
                {
                    //Creo una tabla para filtrar los datos
                    DataTable tablaGrupos = new DataTable();
                    //Paso dataset ala tabla
                    tablaGrupos = ds.Tables[0];
                    Session.Add("TablaFiltros", tablaGrupos);
                    DataRow row = tablaGrupos.Rows[0];
                    txttitulo.Text = row["Puesto"].ToString();
                    //filtramos para evitar datos repetidos
                    DataTable TablaTemporal = tablaGrupos.DefaultView.ToTable(true, "Grupo");
                    //cargamos datos al repeat
                    RepeatDataPuesto.DataSource = TablaTemporal;
                    RepeatDataPuesto.DataBind();
                    //agregamos tabla global a session
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
            }
        }

        /// <summary>
        /// Funcion que carga los datos con un request
        /// </summary>
        /// <param name="ID">ID DEL PUESTO</param>
        public void CargarDatosPuesto_Descripcion(string ID)
        {
            try
            {
                CargaPerfil_EN Entidad = new CargaPerfil_EN();
                //tomo el valor del ID
                Entidad.Descripcion = ID;
                //Creo mi componente
                CargaPerfil_CM Componente = new CargaPerfil_CM();
                //Cargo mi componenet e en un dataset
                DataSet ds = new DataSet();
                ds = Componente.CargaPuestosPerfil_Descripcion(Entidad);
                //comprobamos que el dataset no este vacio, si esta vacio es que no hay ningun perfil relacionado
                if (ds.Tables[0].Rows.Count == 0)
                {
                    ReturnPrevius();
                    ImageButton1.Visible = false;
                    ImageButton2.Visible = false;
                    ImageButton3.Visible = false;
                    ImageButton4.Visible = false;
                    ImageButton5.Visible = false;
                }
                else
                {
                    //Creo una tabla para filtrar los datos
                    DataTable tablaGrupos = new DataTable();
                    //Paso dataset ala tabla
                    tablaGrupos = ds.Tables[0];
                    Session.Add("TablaFiltros", tablaGrupos);
                    DataRow row = tablaGrupos.Rows[0];
                    txttitulo.Text = row["Puesto"].ToString();
                    //filtramos para evitar datos repetidos
                    DataTable TablaTemporal = tablaGrupos.DefaultView.ToTable(true, "Grupo");
                    //cargamos datos al repeat
                    RepeatDataPuesto.DataSource = TablaTemporal;
                    RepeatDataPuesto.DataBind();
                    //agregamos tabla global a session
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this);
            }
        }

        /// <summary>
        /// EVENTO HEREDADO DEL AGREGADO DE UN ITEM, SE ACTIVA CADA VEZ QUE UN REPEAT AGREGA UN ITEM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeatDataPuesto_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //tomamos el valor del item agregado para filtrar la tabla
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            DataTable tbl = (System.Data.DataTable)(Session["TablaFiltros"]);
            string Grupo = Convert.ToString(DataBinder.Eval(dbr, "Grupo"));
            //filtramos la tabla para mostrar solo las filas con el grupo del item que se agrego
            DataView dv = new DataView(tbl);
            dv.RowFilter = "Grupo = '" + Grupo + "'";
            //creamos un repeat y lbuscamos un control en html con ese mismo nombre
            Repeater RepeaterVariables = (Repeater)e.Item.FindControl("RepeaterChild");
            Session.Add("TablaLabel", dv.ToTable());
            RepeaterVariables.DataSource = dv;
            RepeaterVariables.DataBind();
        }

        /// <summary>
        /// Eevento que se genera al crear un label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeatChild_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Bajo el valor del label actual
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            String EtiquetaActual = dbr["Etiqueta"].ToString();
            String ValorActual = dbr["Valor"].ToString();
            //Construyo un Label
            Label lblEtiqueta = (Label)e.Item.FindControl("lblEtiqueta");
            Label lblValor = (Label)e.Item.FindControl("Label1");
            //Si la etiqueta esta vacia
            if (EtiquetaActual.Equals(""))
            {
                lblValor.Text = " <i class='fa fa-fw fa-check'></i>" + ValorActual;
            }
            else
            {
                if (EtiquetaActual.Equals(ValueLabel))//SI LA ETIQUETA ES IGUAL A LA ANTERIOE
                {
                    //LE PONGO ESPACIOS EN BLANCO PARA AJUSTAR
                    lblEtiqueta.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                else
                {
                    //Si no, subo el valor al string global
                    ValueLabel = dbr["Etiqueta"].ToString();
                    lblEtiqueta.Text = " <i class='fa fa-fw fa-check'></i>" + EtiquetaActual;
                    //Lo pongo en negrita
                    lblEtiqueta.Font.Bold = true;
                }
            }
        }

        /// <summary>
        /// Evento clic del boton guardar WORD, en base a ciclos escribe en HTML la estructura del word y lo exporta a este
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //Bajo tabla de la consulta a session
            DataTable tbl = (System.Data.DataTable)(Session["TablaFiltros"]);
            //Creo tabla con filtro DISTINCT pata que repita los grupos
            DataView dvr = new DataView(tbl);
            dvr.RowFilter = "externo = 1";
            //Creo tabla con filtro DISTINCT pata que repita los grupos
            DataTable TablaTemporal = dvr.ToTable(true, "Grupo");
            //Saco vista de filas de la tabla de la consulta
            DataRow row = tbl.Rows[0];
            //saco el vaor del puesto o perfil y lo paso a variable
            string Titulo = row["Puesto"].ToString();
            //CREO STRINGBUILDER PARA LLENARLO CON CODIGO HTML
            StringBuilder content = new StringBuilder();
            //EMPIEZO CODIGO HTML
            content.Append("<meta http-equiv='Content-Type' content='text/html' charset='UTF-8'/>");
            content.Append(System.Environment.NewLine);
            content.Append("<html>");
            content.Append("<body>");
            //COLOCO ENCABEZADO = NOMBRE DEL PERFIL, Y CONCATENO VARIABLE QUE CONTIENE EL NOMBRE
            content.Append("<div style='text-align: center;'><FONT FACE='calibri' SIZE=5> <b>" + " " + Titulo + "</b></FONT></div><br>");
            //CICLO QIE RECORRE TODO LOS GRUPOS EXISTENTES PARA ESTE PERFIL
            for (int i = 0; i < TablaTemporal.Rows.Count; i++)
            {
                //REALIZO FILTRO DETODO LOS GRUPOS EXISTENTES PARA ESTE PERFIL
                DataRow rowj = TablaTemporal.Rows[i];
                //TOMO VALOR DEL NOMBRE DEL GRUPO Y LO METO A VARIABLE
                string Grupo = rowj["Grupo"].ToString();
                //LLENO STRINGBUILDER CON CODIGO HTML Y CONCATENADO EL NOMBRE DEL GRUPO
                content.Append("<div><b><FONT FACE='calibri'>" + Grupo + ":</FONT></b></div>");
                //REALIZO FILTRO EN TABLA DELA CONSULTA PRINCIPAL PARA VER QUE VALORES PERTENECEN A ESTE GRUPO
                DataView dv = new DataView(tbl);
                dv.RowFilter = "Grupo = '" + Grupo + "'";
                //CREO UNA TABLA PARA LLENAR LOS VALORES DEL GRUPO
                content.Append("<table>");
                //METO EL FILTRO A UNA TABLA PARA REUSARLA
                DataTable filter = dv.ToTable();
                //CREO CICLO PARA RECORRER TODOS LOS VALORES DE ESTE GRUPO
                for (int j = 0; j < dv.Count; j++)
                {
                    //REALIZO FILTRO DE TODOS LOS VALORES EXISTENTE EN ESTE GRUPO
                    DataRow rowk = filter.Rows[j];
                    //METO EL VALOR A UNA VARIABLE
                    string EtiquetaV = rowk["Etiqueta"].ToString();
                    string Valor = rowk["Valor"].ToString();
                    if (EtiquetaV.Equals(ValueLabel))
                    {
                        //METO A STRINGBUILDER CODIGO HTML CON EL VALOR OBTENIDO
                        content.Append("<tr><td></td><td></td><td><FONT FACE='calibri'>" + Valor + "</FONT></td></tr>");
                    }
                    else
                    {
                        ValueLabel = EtiquetaV;
                        //METO A STRINGBUILDER CODIGO HTML CON EL VALOR OBTENIDO
                        content.Append("<tr><td></td><td></td><td><FONT FACE='calibri'><strong>" + EtiquetaV + "</strong>" + Valor + "</FONT></td></tr>");
                    }
                }//TERMINA CICLO CON TODAS LA VARIABLES DEL GRUPO
                //CIERRO TABLA
                content.Append("</table></FONT><br><br>");
            }//TERMINA CICLO CON TODOS LOS GRUPOS DE ESTE PERFIL
            //CIERRO CODIGO HTML EN EL STRINGBUILDER
            content.Append("</body></html>");
            //TOMO VARIABLE DEL NOMBRE DE PERFIL Y REMPLAZO ESPACIOS POR GUINES BAJO PARA METERLO COMO NOMBRE DE ARCHIVO
            Titulo = Titulo.Replace(" ", "_");
            //CREO STRING CON EL NOMBRE DEL ARCHIVO QUE SE GENERARA
            string fileName = "" + Titulo + "" + ".doc";
            //ASIGNO CONTEXTOS
            Response.AppendHeader("Content-Type", "application/msword");
            Response.AppendHeader("Content-disposition", "attachment; filename=" + fileName);
            //CREO ARCHIVO EN UN RESPONSE PARA QUE PAGINA ME LO ARROJE
            Response.Write(content.ToString());
        }

        /// <summary>
        /// Evento clic del boton ver codigo html, en base a ciclos, crea codigo html para mostrarlo  al usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            RepeatDataPuesto.Visible = false;
            PanelHTML.Visible = true;
            VistaPrevia.Visible = false;
            ImageButton3.Visible = true;
            //Bajo tabla de la consulta a session
            DataTable tbl = (System.Data.DataTable)(Session["TablaFiltros"]);
            //Creo tabla con filtro DISTINCT pata que repita los grupos
            DataView dvr = new DataView(tbl);
            dvr.RowFilter = "externo = 1";
            //Creo tabla con filtro DISTINCT pata que repita los grupos
            DataTable TablaTemporal = dvr.ToTable(true, "Grupo");
            //Saco vista de filas de la tabla de la consulta
            DataRow row = tbl.Rows[0];
            //saco el vaor del puesto o perfil y lo paso a variable
            string Titulo = row["Puesto"].ToString();
            //CREO STRINGBUILDER PARA LLENARLO CON CODIGO HTML
            StringBuilder content = new StringBuilder();
            //EMPIEZO CODIGO HTML
            content.Append("&lt;meta http-equiv='Content-Type' content='text/html' charset='UTF-8'/&gt;");
            content.Append(System.Environment.NewLine);
            content.Append("&lt;html&gt;");
            content.Append(System.Environment.NewLine);
            content.Append("&lt;body&gt;");
            content.Append(System.Environment.NewLine);
            //COLOCO ENCABEZADO = NOMBRE DEL PERFIL, Y CONCATENO VARIABLE QUE CONTIENE EL NOMBRE
            content.Append(System.Environment.NewLine + "&lt;div style='text-align: center;'&gt;&lt;FONT FACE='calibri' SIZE=5&gt; &lt;b&gt;" + " " + Titulo + "&lt;/b&gt;&lt;/FONT&gt;" + System.Environment.NewLine + "&lt;/div&gt;&lt;br&gt;" + System.Environment.NewLine);
            //CICLO QIE RECORRE TODO LOS GRUPOS EXISTENTES PARA ESTE PERFIL
            for (int i = 0; i < TablaTemporal.Rows.Count; i++)
            {
                //REALIZO FILTRO DETODO LOS GRUPOS EXISTENTES PARA ESTE PERFIL
                DataRow rowj = TablaTemporal.Rows[i];
                //TOMO VALOR DEL NOMBRE DEL GRUPO Y LO METO A VARIABLE
                string Grupo = rowj["Grupo"].ToString();
                //LLENO STRINGBUILDER CON CODIGO HTML Y CONCATENADO EL NOMBRE DEL GRUPO
                content.Append(System.Environment.NewLine + "&lt;div&gt;&lt;b&gt;&lt;FONT FACE='calibri'&gt;" + Grupo + ":&lt;/FONT&gt;&lt;/b&gt;" + System.Environment.NewLine + "&lt;/div&gt;" + System.Environment.NewLine);
                //REALIZO FILTRO EN TABLA DELA CONSULTA PRINCIPAL PARA VER QUE VALORES PERTENECEN A ESTE GRUPO
                DataView dv = new DataView(tbl);
                dv.RowFilter = "Grupo = '" + Grupo + "'";
                //CREO UNA TABLA PARA LLENAR LOS VALORES DEL GRUPO
                content.Append("&lt;table&gt;" + System.Environment.NewLine);
                //METO EL FILTRO A UNA TABLA PARA REUSARLA
                DataTable filter = dv.ToTable();
                //CREO CICLO PARA RECORRER TODOS LOS VALORES DE ESTE GRUPO
                for (int j = 0; j < dv.Count; j++)
                {
                    //REALIZO FILTRO DE TODOS LOS VALORES EXISTENTE EN ESTE GRUPO
                    DataRow rowk = filter.Rows[j];
                    //METO EL VALOR A UNA VARIABLE
                    string EtiquetaV = rowk["Etiqueta"].ToString();
                    string Valor = rowk["Valor"].ToString();
                    if (EtiquetaV.Equals(ValueLabel))
                    {
                        //METO A STRINGBUILDER CODIGO HTML CON EL VALOR OBTENIDO
                        content.Append("&lt;tr&gt;&lt;td&gt;&lt;/td&gt;&lt;td&gt;&lt;/td&gt;&lt;td&gt;&lt;FONT FACE='calibri'&gt;" + Valor + "&lt;/FONT&gt;&lt;/td&gt;&lt;/tr&gt;");
                    }
                    else
                    {
                        ValueLabel = EtiquetaV;
                        //METO A STRINGBUILDER CODIGO HTML CON EL VALOR OBTENIDO
                        content.Append("&lt;tr&gt;&lt;td&gt;&lt;/td&gt;&lt;td&gt;&lt;/td&gt;&lt;td&gt;&lt;FONT FACE='calibri'&gt;&lt;strong&gt;" + EtiquetaV + "&lt;/strong&gt;" + Valor + "&lt;/FONT&gt;&lt;/td&gt;&lt;/tr&gt;");
                    }
                }//TERMINA CICLO CON TODAS LA VARIABLES DEL GRUPO
                //CIERRO TABLA
                content.Append(System.Environment.NewLine + "&lt;/table&gt;&lt;/FONT&gt;&lt;br&gt;&lt;br&gt;" + System.Environment.NewLine);
            }//TERMINA CICLO CON TODOS LOS GRUPOS DE ESTE PERFIL
            //CIERRO CODIGO HTML EN EL STRINGBUILDER
            content.Append(System.Environment.NewLine + "&lt;/body&gt;" + System.Environment.NewLine + "&lt;/html&gt;");
            txthtml.Text = content.ToString();
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            PanelHTML.Visible = false;
            VistaPrevia.Visible = true;
            //Bajo tabla de la consulta a session
            DataTable tbl = (System.Data.DataTable)(Session["TablaFiltros"]);
            //Creo tabla con filtro DISTINCT pata que repita los grupos
            DataView dvr = new DataView(tbl);
            dvr.RowFilter = "externo = 1";
            //Creo tabla con filtro DISTINCT pata que repita los grupos
            DataTable TablaTemporal = dvr.ToTable(true, "Grupo");
            //Saco vista de filas de la tabla de la consulta
            DataRow row = tbl.Rows[0];
            //saco el vaor del puesto o perfil y lo paso a variable
            string Titulo = row["Puesto"].ToString();
            //CREO STRINGBUILDER PARA LLENARLO CON CODIGO HTML
            StringBuilder content = new StringBuilder();
            //EMPIEZO CODIGO HTML
            content.Append("<meta http-equiv='Content-Type' content='text/html' charset='UTF-8'/>");
            content.Append(System.Environment.NewLine);
            content.Append("<html>");
            content.Append(System.Environment.NewLine);
            content.Append("<body>");
            content.Append(System.Environment.NewLine);
            //COLOCO ENCABEZADO = NOMBRE DEL PERFIL, Y CONCATENO VARIABLE QUE CONTIENE EL NOMBRE
            content.Append(System.Environment.NewLine + "<div style='text-align: center;'><FONT FACE='calibri' SIZE=5> <b>" + " " + Titulo + "</b></FONT>" + System.Environment.NewLine + "</div><br>" + System.Environment.NewLine);
            //CICLO QIE RECORRE TODO LOS GRUPOS EXISTENTES PARA ESTE PERFIL
            for (int i = 0; i < TablaTemporal.Rows.Count; i++)
            {
                //REALIZO FILTRO DETODO LOS GRUPOS EXISTENTES PARA ESTE PERFIL
                DataRow rowj = TablaTemporal.Rows[i];
                //TOMO VALOR DEL NOMBRE DEL GRUPO Y LO METO A VARIABLE
                string Grupo = rowj["Grupo"].ToString();
                //LLENO STRINGBUILDER CON CODIGO HTML Y CONCATENADO EL NOMBRE DEL GRUPO
                content.Append(System.Environment.NewLine + "<div><b><FONT FACE='calibri'>" + Grupo + ":</FONT></b>" + System.Environment.NewLine + "</div>" + System.Environment.NewLine);
                //REALIZO FILTRO EN TABLA DELA CONSULTA PRINCIPAL PARA VER QUE VALORES PERTENECEN A ESTE GRUPO
                DataView dv = new DataView(tbl);
                dv.RowFilter = "Grupo = '" + Grupo + "'";
                //CREO UNA TABLA PARA LLENAR LOS VALORES DEL GRUPO
                content.Append("<table>" + System.Environment.NewLine);
                //METO EL FILTRO A UNA TABLA PARA REUSARLA
                DataTable filter = dv.ToTable();
                //CREO CICLO PARA RECORRER TODOS LOS VALORES DE ESTE GRUPO
                for (int j = 0; j < dv.Count; j++)
                {
                    //REALIZO FILTRO DE TODOS LOS VALORES EXISTENTE EN ESTE GRUPO
                    DataRow rowk = filter.Rows[j];
                    string EtiquetaV = rowk["Etiqueta"].ToString();
                    string Valor = rowk["Valor"].ToString();
                    if (EtiquetaV.Equals(ValueLabel))
                    {
                        //METO A STRINGBUILDER CODIGO HTML CON EL VALOR OBTENIDO
                        content.Append("<tr><td></td><td></td><td><FONT FACE='calibri'>" + Valor + "</FONT></td></tr>");
                    }
                    else
                    {
                        ValueLabel = EtiquetaV;
                        //METO A STRINGBUILDER CODIGO HTML CON EL VALOR OBTENIDO
                        content.Append("<tr><td></td><td></td><td><FONT FACE='calibri'><strong>" + EtiquetaV + "</strong>" + Valor + "</FONT></td></tr>");
                    }
                }//TERMINA CICLO CON TODAS LA VARIABLES DEL GRUPO
                //CIERRO TABLA
                content.Append(System.Environment.NewLine + "</table></FONT><br><br>" + System.Environment.NewLine);
            }//TERMINA CICLO CON TODOS LOS GRUPOS DE ESTE PERFIL
            //CIERRO CODIGO HTML EN EL STRINGBUILDER
            content.Append(System.Environment.NewLine + "</body>" + System.Environment.NewLine + "</html>");
            lbVistaPrevia.Text = content.ToString();
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string parameters = Request.Url.Query;
            Response.Redirect(path + parameters);
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("organigrama.aspx");
        }
    }
}