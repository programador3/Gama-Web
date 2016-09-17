using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class avisos_gen_m : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_usuario");
                dt.Columns.Add("usuario_nombre");
                Session["dt_usuarios"] = dt;
                CargarUsuarios("");
            }
        }

        private void CargarGrid()
        {
            DataTable dt = Session["dt_usuarios"] as DataTable;
            DataTable ds = dt.Copy();
            gridperfiles.DataSource = ds;
            gridperfiles.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "notwssswswsi5eded33W3", "DataTa1();", true);
        }

        private void InsertInTable(string idc, string usuario)
        {
            DataTable dt = Session["dt_usuarios"] as DataTable;
            DataView view = dt.DefaultView;
            view.RowFilter = "idc_usuario = " + idc.Trim() + "";
            if (view.ToTable().Rows.Count > 0)
            {
                Alert.ShowAlertError("El usuario " + usuario + " ya esta en la lista.", this);
            }
            else
            {
                DataRow row = dt.NewRow();
                row["idc_usuario"] = idc;
                row["usuario_nombre"] = usuario;
                dt.Rows.Add(row);
                Session["dt_usuarios"] = dt;
            }
        }

        private void DeleteTable(string idc)
        {
            DataTable dt = Session["dt_usuarios"] as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                string id = row["idc_usuario"].ToString();
                if (id.Trim() == idc.Trim())
                {
                    row.Delete();
                    break;
                }
            }
            Session["dt_usuarios"] = dt;
        }

        private void AddAll()
        {
            try
            {
                AvisosENT entidad = new AvisosENT();
                AvisosCOM com = new AvisosCOM();
                DataSet ds = com.ComboUsuario(entidad);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    InsertInTable(row["idc_usuario"].ToString(), row["usuario_nombre"].ToString());
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        private void DeleteAll()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("idc_usuario");
            dt.Columns.Add("usuario_nombre");
            Session["dt_usuarios"] = dt;
        }

        private void CargarUsuarios(string filtro)
        {
            try
            {
                AvisosENT entidad = new AvisosENT();
                AvisosCOM com = new AvisosCOM();
                DataSet ds = com.ComboUsuario(entidad);
                ddlusuarios.DataValueField = "idc_usuario";
                ddlusuarios.DataTextField = "usuario_nombre";
                if (filtro == "")
                {
                    ddlusuarios.DataSource = ds.Tables[0];
                    ddlusuarios.DataBind();
                    ddlusuarios.Items.Insert(0, new ListItem("--Seleccione un Usuario"));
                }
                else
                {
                    DataView view = ds.Tables[0].DefaultView;
                    view.RowFilter = "usuario_nombre like '%" + filtro + "%'";
                    ddlusuarios.DataSource = view.ToTable();
                    ddlusuarios.DataBind();
                    if (view.ToTable().Rows.Count == 0)
                    {
                        Alert.ShowAlertInfo("La busqueda no arrojo resultados, INTENTELO NUEVAMENTE", "Mensaje del Sistema", this);
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void lnkagregar_Click(object sender, EventArgs e)
        {
            string idc = ddlusuarios.SelectedValue;
            string usuario = ddlusuarios.SelectedItem.ToString();
            if (idc == "0")
            {
                Alert.ShowAlertError("Seleccione un Usuario", this.Page);
            }
            else
            {
                InsertInTable(idc, usuario);
                CargarGrid();
            }
        }

        private string Cadena()
        {
            string cadena = "";
            DataTable dt = Session["dt_usuarios"] as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                string id = row["idc_usuario"].ToString();
                cadena = cadena + id + ";";
            }
            return cadena;
        }

        protected void lnkagregarTodo_Click(object sender, EventArgs e)
        {
            string cssclas = lnkagregarTodo.CssClass;
            lnkagregarTodo.CssClass = cssclas == "btn btn-default btn-block" ? "btn btn-success btn-block" : "btn btn-default btn-block";
            cssclas = lnkagregarTodo.CssClass;
            DeleteAll();
            if (cssclas == "btn btn-success btn-block")
            {
                AddAll();
            }
            CargarGrid();
        }

        protected void gridperfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string idc_usuario = gridperfiles.DataKeys[index].Values["idc_usuario"].ToString();
            DeleteTable(idc_usuario);
            CargarGrid();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Cadena() == "")
            {
                Alert.ShowAlertError("Agrege un usuario para enviar el aviso", this);
            }
            else if (txtaviso.Text == "")
            {
                Alert.ShowAlertError("Agrege un Contenido para el Aviso", this);
            }
            else if (txtasunto.Text == "")
            {
                Alert.ShowAlertError("Agrege un Asunto", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Enviar este Aviso?','modal fade modal-info');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("menu.aspx");
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                AvisosENT entidad = new AvisosENT();
                entidad.Pasunto = txtasunto.Text;
                entidad.Ptexto = txtaviso.Text;
                entidad.Ppara = Cadena();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                AvisosCOM com = new AvisosCOM();
                DataSet ds = com.EnviarAviso(entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos Enviando el Aviso.", "Espere un Momento", "menu.aspx", "imagenes/loading.gif", "3000", "El Aviso fue Enviado a todos lops destinatarios", this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }

        protected void lnkbuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtbuscar.Text;
            CargarUsuarios(filtro);
        }
    }
}