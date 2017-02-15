using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class validar_conteo_inventario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["dtart"] = null;
                Session["tabla_modulos"] = null;
                CargarSucursal();
                CargarModulo();
            }
        }

        private void CargarSucursal()
        {
            InventariosENT entidad = new InventariosENT();
            InventariosCOM componente = new InventariosCOM();
            DataSet ds = componente.CargarSucursalesAlmacen(entidad);
            ddlsucursal.DataValueField = "idc_almacen";
            ddlsucursal.DataTextField = "nombre";
            ddlsucursal.DataSource = ds.Tables[0];
            ddlsucursal.DataBind();
            ddlsucursal.Items.Insert(0, new ListItem("--Seleccione una Sucursal", "0")); //updated code}
        }

        private void CargarModulo()
        {
            InventariosENT entidad = new InventariosENT();
            InventariosCOM componente = new InventariosCOM();
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            DataSet ds = componente.CargarModulosfn(entidad);
            if (ds.Tables[1].Rows.Count > 0)
            {
                Session["tabla_modulos"] = ds.Tables[0];
                int id = Convert.ToInt32(ds.Tables[1].Rows[0]["idc_almacen"]);
                ddlsucursal.SelectedValue = id.ToString();
                ddlsucursal.Enabled = false;
                txtalmacen.Text = ddlsucursal.SelectedItem.ToString();
                cbxtodo.Enabled = false;
                CargarModulofl(id);
            }
        }

        private void CargarModulofl(int idc_almacen)
        {
            InventariosENT entidad = new InventariosENT();
            InventariosCOM componente = new InventariosCOM();
            entidad.Pidc_almacen = idc_almacen;
            DataSet ds = componente.CargarModulos(entidad);
            DataTable dt = new DataTable();
            dt.Columns.Add("idc_modulo");
            dt.Columns.Add("nombre");
            if (Session["tabla_modulos"] != null)
            {
                DataTable dt2 = (DataTable)Session["tabla_modulos"];
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int id = Convert.ToInt32(row["idc_modulo"]);
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        int id2 = Convert.ToInt32(row2["idc_modulo"]);
                        if (id == id2)
                        {
                            DataRow newrow = dt.NewRow();
                            newrow["idc_modulo"] = row["idc_modulo"];
                            newrow["nombre"] = row["nombre"];
                            dt.Rows.Add(newrow);
                        }
                    }
                }

                ddlmodulo.DataValueField = "idc_modulo";
                ddlmodulo.DataTextField = "nombre";
                ddlmodulo.DataSource = dt;
                ddlmodulo.DataBind();
            }
            else
            {
                ddlmodulo.DataValueField = "idc_modulo";
                ddlmodulo.DataTextField = "nombre";
                ddlmodulo.DataSource = ds.Tables[0];
                ddlmodulo.DataBind();
            }
            ddlmodulo.Items.Insert(0, new ListItem("--Seleccione un Modulo", "0")); //updated code}
        }

        private void CargarArticulos(int idc_modulo, string filtro)
        {
            InventariosENT entidad = new InventariosENT();
            InventariosCOM componente = new InventariosCOM();
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            entidad.Pidc_modulo = idc_modulo;
            entidad.Pidc_almacen = Convert.ToInt32(ddlsucursal.SelectedValue);
            entidad.Pcadena = filtro;
            entidad.Parea = cbxtodo.Checked;
            DataSet ds = componente.CargarArticulos(entidad);
            gridprocesos.DataSource = ds.Tables[0];
            gridprocesos.DataBind();
            lbltotal.Text = ds.Tables[0].Rows.Count.ToString();
            Session["dtart"] = ds.Tables[0];
        }

        protected void ddlsucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlsucursal.SelectedValue);
            if (id != 0)
            {
                txtalmacen.Text = ddlsucursal.SelectedItem.ToString();
                CargarModulofl(id);
            }
        }

        protected void ddlmodulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlmodulo.SelectedValue);
            if (id != 0)
            {
                CargarArticulos(id, "");
            }
        }

        protected void lbkbuscar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlmodulo.SelectedValue);
            int id2 = Convert.ToInt32(ddlsucursal.SelectedValue);
            if (id == 0)
            {
                Alert.ShowAlertInfo("Seleccione un Modulo para filtrar", "Mensaje del Sistema", this);
            }
            else if (id2 == 0)
            {
                Alert.ShowAlertInfo("Seleccione un Almacen para filtrar", "Mensaje del Sistema", this);
            }
            else
            {
                CargarArticulos(id, txtbuscar.Text);
            }
        }

        protected void gridprocesos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            TextBox txt = (TextBox)e.Row.FindControl("txtconteo");
            LinkButton lnkir = (LinkButton)e.Row.FindControl("lnkir");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                int decimales = Convert.ToInt32(rowView["decimales"]);
                int conteo_total = Convert.ToInt32(rowView["conteo_total"]);
                string estado = rowView["estado"].ToString();
                if (estado.Trim() != "EN PROCESO" || conteo_total > 3)
                {
                    txt.Text = conteo_total.ToString();
                    txt.Enabled = false;
                    lnkir.Visible = false;
                }
            }
        }

        protected void gridprocesos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void txtconteo_TextChanged(object sender, EventArgs e)
        {
            GridView grid = (GridView)((LinkButton)sender).Parent.Parent.Parent.Parent;
            GridViewRow currentRow = (GridViewRow)((LinkButton)sender).Parent.Parent;
            TextBox txt = (TextBox)currentRow.FindControl("txtconteo");
            int index = Convert.ToInt32(currentRow.RowIndex);
            int idc_artimod = Convert.ToInt32(gridprocesos.DataKeys[index].Values["idc_artimod"]);
            DataTable dt = (DataTable)Session["dtart"];
            DataView view = dt.DefaultView;
            view.RowFilter = "idc_artimod = " + idc_artimod + "";
            if (view.ToTable().Rows.Count > 0 && txt.Text != "")
            {
                DataRow row = view.ToTable().Rows[0];
                int idC_articulo = Convert.ToInt32(row["idc_articulo"]);
                int idc_artimodprog = Convert.ToInt32(row["idc_artimodprog"]);
                int conteo_total = Convert.ToInt32(row["conteo_total"]);
                bool decimales = Convert.ToBoolean(row["decimales"]);
                string estado = row["estado"].ToString();
                string valuetext = txt.Text;
                if (estado.Trim() == "EN PROCESO" && conteo_total < 4)
                {
                    string mensaje = "";
                    string reg = "";
                    switch (decimales)
                    {
                        case false:
                            mensaje = "No Ingrese Decimales.";
                            reg = @"^\d+$";
                            break;
                        case true:
                            mensaje = "Ingrese solo la cantidad de 3 decimales.";
                            reg = @"^\d+([,\.]\d{1,3})?$";
                            break;
                    }

                    if (Regex.IsMatch(valuetext, reg))//se cumple
                    {
                        Validar();
                        int id = Convert.ToInt32(ddlmodulo.SelectedValue);
                        if (id != 0)
                        {
                            InventariosENT entidad = new InventariosENT();
                            InventariosCOM componente = new InventariosCOM();
                            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                            entidad.Pidc_modulo = idC_articulo;
                            entidad.Pidc_actscategoria = idc_artimodprog;
                            entidad.Pidc_almacen = Convert.ToInt32(ddlsucursal.SelectedValue);
                            entidad.Pfolio2 = Convert.ToDecimal(valuetext);
                            entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                            entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                            entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                            DataSet ds;
                            ds = componente.Articulo(entidad);
                            string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                            int bien = Convert.ToInt32(ds.Tables[0].Rows[0]["bien"]);
                            if (vmensaje == "") { vmensaje = bien == 1 ? "" : "Conteo Incorrecto"; }
                            if (vmensaje != "")
                            {

                                CargarArticulos(id, "");
                                Alert.ShowAlertError(vmensaje, this);
                            }
                            else
                            {
                                Alert.ShowAlert("Conteo Correcto","Mensaje del Sistema", this);
                                CargarArticulos(id, "");
                            }
                        }
                    }
                    else
                    {
                        txt.Text = "";
                        Alert.ShowAlertError("", this.Page);
                    }
                }
            }
        }

        private Boolean Validar()
        {
            bool validar = false;
            return validar;
        }
    }
}