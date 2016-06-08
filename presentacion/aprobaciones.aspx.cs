using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class aprobaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarGrid();
            }
        }

        /// <summary>
        /// Carga los datos del grid desde una base de datos SQL
        /// </summary>
        public void CargarGrid()
        {
            AprobacionesENT Entidad = new AprobacionesENT();
            Entidad.Idc_aprobacion = 0;
            AprobacionesCOM Componente = new AprobacionesCOM();
            DataSet ds = Componente.CargaCatalogoAprobaciones(Entidad);
            gridaprobaciones.DataSource = ds;
            gridaprobaciones.DataBind();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                lblTablaVacia.Visible = true;
            }
        }

        /// <summary>
        /// Devuele los datos de una aprobacion existente
        /// </summary>
        /// <returns></returns>
        public void Preview(int idc_aprobacion)
        {
            DataTable table = new DataTable();
            AprobacionesENT entidad = new AprobacionesENT();
            entidad.Idc_aprobacion = idc_aprobacion;
            AprobacionesCOM componente = new AprobacionesCOM();
            try
            {
                table = componente.CargaCatalogoAprobaciones(entidad).Tables[0];
                DataTable table_temp = new DataTable();
                table_temp.Columns.Add("puesto");
                DataRow row_title = table.Rows[0];
                lblTituloAprobacion.Text = row_title["nombre"].ToString();
                txtComentarios.Text = row_title["comentarios"].ToString();
                foreach (DataRow row in table.Rows)
                {
                    if (!Convert.ToBoolean(row["borrado"]) == true)
                    {
                        DataRow new_row = table_temp.NewRow();
                        new_row["puesto"] = row["puesto"].ToString();
                        table_temp.Rows.Add(new_row);
                    }
                }
                listaPuestos.DataSource = table_temp;
                listaPuestos.DataBind();
            }
            catch (Exception EX)
            {
                Alert.ShowAlertError(EX.ToString(), this);
            }
        }

        /// <summary>
        /// Elimina una probacion y recarga el grid
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteAprobacion(int ID)
        {
            try
            {
                AprobacionesENT Entidad = new AprobacionesENT();
                AprobacionesCOM Componente = new AprobacionesCOM();
                Entidad.Idc_aprobacion = ID;
                Entidad.Borrado = 1;
                Entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                Entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                Entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                Entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                DataSet ds = Componente.EliminarAprobaciones(Entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    ///todo bien
                    CargarGrid();
                    Alert.ShowAlert("Aprobación eliminada correctamente", "Mensaje del sistema", this);
                    //jeje
                }
                else
                {
                    //AlertError(vmensaje); //marca error por comillas el mensaje trae comillas simples y se corta cuando se concatena en la funcion javascript
                    Alert.ShowAlertInfo(vmensaje, "Mensaje del sistema", this.Page);
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void gridaprobaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_aprobacion"] = gridaprobaciones.DataKeys[index].Values["idc_aprobacion"].ToString();
            //convertimos los parametros urla hexadecimal por seguridad
            string idc_apro = funciones.ConvertStringToHex(gridaprobaciones.DataKeys[index].Values["idc_aprobacion"].ToString());
            switch (e.CommandName)
            {
                case "Borrar":
                    Session["Caso_Confirmacion_Aprobacion"] = "Borrar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Desea eliminar esta solicitud?, tome en cuenta que se eliminara todo lo relacionado con el mismo.');", true);
                    break;

                case "Editar":
                    Response.Redirect("aprobaciones_captura.aspx?idc_aprobacion=" + idc_apro);
                    break;

                case "Ver":
                    listPuestos.Items.Clear();
                    Preview(Convert.ToInt32(gridaprobaciones.DataKeys[index].Values["idc_aprobacion"].ToString()));
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ViewPre();", true);

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
            string variables = (string)(Session["idc_aprobacion"]);
            int idc_aprobacion = Convert.ToInt32(variables);
            string Confirma_a = (string)(Session["Caso_Confirmacion_Aprobacion"]);
            switch (Confirma_a)
            {
                case "Borrar":
                    //Llamamos funcion que elimina y le pasamos variable publica
                    //Alert.ShowAlertError("Borrar", this);
                    DeleteAprobacion(idc_aprobacion);
                    break;

                default:

                    break;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("aprobaciones_captura.aspx");
        }
    }
}