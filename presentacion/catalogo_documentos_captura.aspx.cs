using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class catalogo_documentos_captura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!Page.IsPostBack)
            {
                DataTable extensiones = new DataTable();
                extensiones.Columns.Add("descripcion");
                extensiones.Columns.Add("tipo_archivo");
                Session["extensiones"] = extensiones;
                LLnearCombos();
                if (Request.QueryString["idc_tipodoc"] != null)//ES EDICION
                {
                    CargaGrid(Convert.ToInt32(Request.QueryString["idc_tipodoc"].ToString()));
                }
            }
        }

        /// <summary>
        /// Carga Grid con datos del documento
        /// </summary>
        public void CargaGrid(int IDC_TIPODOC)
        {
            Catalogo_DocumentosENT entidad = new Catalogo_DocumentosENT();
            Catalogo_DocumentosCOM componente = new Catalogo_DocumentosCOM();
            entidad.Pidc_tipodoc = IDC_TIPODOC;
            DataSet ds = componente.CargaDocumentos(entidad);
            DataRow row = ds.Tables[0].Rows[0];
            txtNombre.Text = row["descripcion"].ToString();
            DataTable tabladatos = ds.Tables[1];
            //llenamos las extensiones exsistentes
            DataTable extensiones = (DataTable)Session["extensiones"];
            foreach (DataRow row2 in tabladatos.Rows)
            {
                DataRow newrow = extensiones.NewRow();
                newrow["descripcion"] = row2["descripcion"];
                newrow["tipo_archivo"] = row2["tipo_archivo"];
                extensiones.Rows.Add(newrow);
            }
            gridDocumentos.DataSource = extensiones;
            gridDocumentos.DataBind();
            Session["extensiones"] = extensiones;
        }

        public void LLnearCombos()
        {
            DataTable tipo_archivo = new DataTable();
            tipo_archivo.Columns.Add("tipo_archivo");
            List<String> tipos = new List<string>();
            tipos.Add(".PDF");
            tipos.Add(".JPG");
            tipos.Add(".DOC");
            tipos.Add(".XLS");
            tipos.Add(".PPT");
            tipos.Add(".TXT");
            tipos.Add(".PNG");
            tipos.Add(".JPEG");
            tipos.Add(".ZIP");
            tipos.Add(".RAR");
            tipos.Add(".HTML");
            foreach (string item in tipos)
            {
                DataRow row = tipo_archivo.NewRow();
                row["tipo_archivo"] = item;
                tipo_archivo.Rows.Add(row);
            }

            DataTable descripciob = new DataTable();
            descripciob.Columns.Add("descripcion");
            List<String> descripcion = new List<string>();
            descripcion.Add("IMAGEN");
            descripcion.Add("DOCUMENTO ESCANEADO");
            descripcion.Add("ARCHIVO COMPRIMIDO");
            descripcion.Add("DOCUMENTO DIGITAL");
            descripcion.Add("DOCUMENTO WEB");

            foreach (string item in descripcion)
            {
                DataRow rowW = descripciob.NewRow();
                rowW["descripcion"] = item;
                descripciob.Rows.Add(rowW);
            }
            //descripciob
            ddlTipo_Archivo.DataTextField = "descripcion";
            ddlTipo_Archivo.DataValueField = "descripcion";
            ddlTipo_Archivo.DataSource = descripciob;
            ddlTipo_Archivo.DataBind();
            //extension
            ddlExtension.DataTextField = "tipo_archivo";
            ddlExtension.DataValueField = "tipo_archivo";
            ddlExtension.DataSource = tipo_archivo;
            ddlExtension.DataBind();
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)(Session["Caso_Confirmacion_Grid"]);
            Catalogo_DocumentosENT entidad = new Catalogo_DocumentosENT();
            Catalogo_DocumentosCOM comp = new Catalogo_DocumentosCOM();
            switch (Confirma_a)
            {
                case "Guardar":
                    entidad.Descripcion = txtNombre.Text;
                    entidad.Caddocs = GeneraCadena();
                    entidad.Numcad = TotalCadena();
                    DataSet ds = comp.Agregar(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('El Documento fue Guardado Correctamente.','catalogo_documentos.aspx');", true);
                    }
                    else
                    {
                        Alert.ShowAlertError(mensaje, this);
                    }
                    break;

                case "Editar":
                    entidad.Descripcion = txtNombre.Text.ToUpper();
                    entidad.Caddocs = GeneraCadena();
                    entidad.Numcad = TotalCadena();
                    entidad.Pidc_tipodoc = Convert.ToInt32(Request.QueryString["idc_tipodoc"].ToString());
                    DataSet ds2 = comp.Editar(entidad);
                    DataRow row2 = ds2.Tables[0].Rows[0];
                    string mensaje2 = row2["mensaje"].ToString();
                    if (mensaje2 == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('El Documento fue Editado Correctamente.','catalogo_documentos.aspx');", true);
                    }
                    else
                    {
                        Alert.ShowAlertError(mensaje2, this);
                    }
                    break;
            }
        }

        protected void gridDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string descripcion = gridDocumentos.DataKeys[index].Values["descripcion"].ToString();
            string tipo = gridDocumentos.DataKeys[index].Values["tipo_archivo"].ToString();
            DataTable extensiones = (DataTable)Session["extensiones"];
            DataRow row = extensiones.NewRow();
            foreach (DataRow row_ant in extensiones.Rows)
            {
                if (row_ant["descripcion"].ToString() == descripcion && row_ant["tipo_archivo"].ToString() == tipo)
                {
                    row_ant.Delete();
                    break;
                }
            }
            gridDocumentos.DataSource = extensiones;
            gridDocumentos.DataBind();
            Session["extensiones"] = extensiones;
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        protected void imgAddExtension_Click(object sender, ImageClickEventArgs e)
        {
            DataTable extensiones = (DataTable)Session["extensiones"];
            DataRow row = extensiones.NewRow();
            bool existe = false;
            foreach (DataRow row_ant in extensiones.Rows)
            {
                if (row_ant["descripcion"].ToString() == ddlTipo_Archivo.SelectedValue && row_ant["tipo_archivo"].ToString() == ddlExtension.SelectedValue)
                { existe = true; }
            }
            if (existe == false)
            {
                row["descripcion"] = ddlTipo_Archivo.SelectedValue;
                row["tipo_archivo"] = ddlExtension.SelectedValue;
                extensiones.Rows.Add(row);
                gridDocumentos.DataSource = extensiones;
                gridDocumentos.DataBind();
            }
            Session["extensiones"] = extensiones;
        }

        /// <summary>
        /// Retorna total de extension agregadas
        /// </summary>
        /// <returns></returns>
        public int TotalCadena()
        {
            DataTable extensiones = (System.Data.DataTable)(Session["extensiones"]);
            return extensiones.Rows.Count;
        }

        /// <summary>
        /// Regresa una cadena de la tabla extensiones
        /// </summary>
        /// <returns></returns>
        public string GeneraCadena()
        {
            string cadena = "";
            DataTable extensiones = (System.Data.DataTable)(Session["extensiones"]);

            foreach (DataRow row in extensiones.Rows)
            {
                cadena = cadena + row["descripcion"].ToString() + ";" + row["tipo_archivo"].ToString() + ";";
            }
            return cadena;
        }

        protected void btnGuardarForm_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (TotalCadena() == 0)
            {
                Alert.ShowAlertError("Debe Agregar al menos 1 extension", this); error = true;
            }
            if (txtNombre.Text == "") { error = true; Alert.ShowAlertError("Escrba el Nombre del Documentos", this); }
            if (error == false)
            {
                if (Request.QueryString["idc_tipodoc"] == null)//Si no existe el request, quiere decir que es nueva captura
                {
                    Session["Caso_Confirmacion_Grid"] = "Guardar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','Se guardara este Documento ¿Todos sus datos son correctos?');", true);
                }
                else
                {
                    Session["Caso_Confirmacion_Grid"] = "Editar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','Se actualizara este Documento ¿Todos sus datos son correctos?');", true);
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("catalogo_documentos.aspx");
        }
    }
}