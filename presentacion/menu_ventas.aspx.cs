using negocio.Componentes;
using System;
using System.Data;

namespace presentacion
{
    public partial class menu_ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                CargarOpciones("");
            }
        }

        private void CargarOpciones(string filtro )
        {
            try
            {
                OpcionesBL opciones = new OpcionesBL();
                DataSet ds = opciones.sp_menu_opciones_tipos(1, Convert.ToInt32(Session["sidc_usuario"]));
                if (filtro == "")
                {
                    repeat_menu.DataSource = ds.Tables[0];
                    repeat_menu.DataBind();
                }
                else {
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = "descripcion like '%"+filtro+"%'";
                    repeat_menu.DataSource = dv.ToTable();
                    repeat_menu.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        protected void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            CargarOpciones(txtbuscar.Text.Trim());
        }
    }
}