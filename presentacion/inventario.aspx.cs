using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class inventario : System.Web.UI.Page
    {
        private static int idc_puesto = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombosPuestos("");
                CargarCombosCat("");
                CargarCombosAreasComun("");
                if (idc_puesto > 0)
                {
                    ddlPuesto.SelectedValue = idc_puesto.ToString();
                    HiddenField.Value = idc_puesto.ToString();
                }
            }
        }

        /// <summary>
        /// Carga combo CargarComboTipoRev
        /// </summary>
        private void CargarCombosPuestos(string filtro)
        {
            Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
            Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
            entidad.Filtro = filtro;
            DataSet ds = componente.CargaComboDinamico(entidad);
            ddlPuesto.DataValueField = "idc_puesto";
            ddlPuesto.DataTextField = "descripcion_puesto_completa";
            ddlPuesto.DataSource = ds.Tables[0];
            ddlPuesto.DataBind();
            if (filtro == "")
            {
                ddlPuesto.Items.Insert(0, new ListItem("-- Seleccione un Puesto", "0")); //updated code}
            }
        }

        /// <summary>
        /// Carga combo CargarComboTipoRev
        /// </summary>
        private void CargarCombosCat(string filtro)
        {
            InventariosENT entidad = new InventariosENT();
            InventariosCOM componente = new InventariosCOM();
            DataSet ds = componente.CargarCat(entidad);
            ddlcategorias.DataValueField = "idc_actscategoria";
            ddlcategorias.DataTextField = "descr";
            if (filtro == "")
            {
                ddlcategorias.DataSource = ds.Tables[0];
                ddlcategorias.DataBind();

                ddlcategorias.Items.Insert(0, new ListItem("-- Seleccione una Categoria", "0")); //updated code}
            }
            else
            {
                DataView view = ds.Tables[0].DefaultView;
                view.RowFilter = "descr LIKE '%" + filtro + "%'";
                ddlcategorias.DataSource = view.ToTable();
                ddlcategorias.DataBind();
            }
        }

        /// <summary>
        /// Carga combo CargarComboTipoRev
        /// </summary>
        private void CargarCombosSub(int idc)
        {
            InventariosENT entidad = new InventariosENT();
            InventariosCOM componente = new InventariosCOM();
            entidad.Pidc_actscategoria = idc;
            DataSet ds = componente.CargarSubCat(entidad);
            //ddlsubcat.DataValueField = "idc_actespec";
            //ddlsubcat.DataTextField = "desesp";
            repeatreractivos.DataSource = ds.Tables[0];
            repeatreractivos.DataBind();
        }

        private void CargarCombosAreasComun(string filtro)
        {
            InventariosENT entidad = new InventariosENT();
            InventariosCOM componente = new InventariosCOM();
            DataSet ds = componente.CargarArea(entidad);
            ddlarea.DataValueField = "idc_areaact";
            ddlarea.DataTextField = "descripcion";
            ddlarea.DataSource = ds.Tables[0];
            ddlarea.DataBind();
            ddlarea.Items.Insert(0, new ListItem("-- Seleccione una Categoria", "0")); //updated code}
        }

        protected void ddlPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPuesto.SelectedValue == "0")
            {
                Alert.ShowAlertError("Seleccione un Puesto Valido", this);
            }
            else
            {
                idc_puesto = Convert.ToInt32(ddlPuesto.SelectedValue);
                HiddenField.Value = idc_puesto.ToString();
            }
        }

        protected void txtpuesto_filtro_TextChanged(object sender, EventArgs e)
        {
            CargarCombosPuestos(txtpuesto_filtro.Text);
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            CargarCombosPuestos(txtpuesto_filtro.Text);
        }

        protected void txtfiltrocat_TextChanged(object sender, EventArgs e)
        {
            CargarCombosCat(txtfiltrocat.Text);
        }

        protected void lnkbuscarcat_Click(object sender, EventArgs e)
        {
            CargarCombosCat(txtfiltrocat.Text);
        }

        protected void ddlcategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlcategorias.SelectedValue == "0")
            {
                Alert.ShowAlertError("Seleccione una Categoria Valida", this);
            }
            else
            {
                CargarCombosSub(Convert.ToInt32(ddlcategorias.SelectedValue));
            }
        }

        protected void ddlarea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlarea.SelectedValue == "0")
            {
                Alert.ShowAlertError("Seleccione una SubCategoria Valida Valido", this);
            }
        }

        protected void lnkarea_Click(object sender, EventArgs e)
        {
            lnkarea.CssClass = lnkarea.CssClass == "btn btn-default btn-block" ? "btn btn-info btn-block" : "btn btn-default btn-block";
            ddlarea.Enabled = lnkarea.CssClass == "btn btn-default btn-block" ? false : true;
        }

        protected void lnkaplica_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            btn.CssClass = btn.CssClass == "btn btn-default btn-block" ? "btn btn-info btn-block" : "btn btn-default btn-block";
            int id = Convert.ToInt32(btn.CommandName);
            foreach (RepeaterItem item in repeatreractivos.Items)
            {
                TextBox txtespec = (TextBox)item.FindControl("txtespec");
                TextBox txtNumero = (TextBox)item.FindControl("txtNumero");
                TextBox txtvaloract = (TextBox)item.FindControl("txtvaloract");
                if (id.ToString() == txtNumero.Text)
                {
                    txtvaloract.ReadOnly = btn.CssClass == "btn btn-default btn-block" ? false : true;
                }
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string case_session = (string)Session["Caso_Confirmacion"];
            switch (case_session)
            {
                case "Cancelar":
                    idc_puesto = 0;
                    Response.Redirect("inventario.aspx");
                    break;

                case "Guardar":
                    break;
            }
        }

        protected void btngiuardar_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            confirmValue = confirmValue.Substring(confirmValue.LastIndexOf(",") + 1);
            if (confirmValue == "Yes")
            {
                if (lnkarea.CssClass == "btn btn-info btn-block" && ddlarea.SelectedValue == "0")
                {
                    Alert.ShowAlertError("Seleccione un Area Comun", this);
                }
                else if (ddlPuesto.SelectedValue == "0")
                {
                    Alert.ShowAlertError("Seleccione un Puesto", this);
                }
                else if (ddlcategorias.SelectedValue == "0")
                {
                    Alert.ShowAlertError("Seleccione una Categoria", this);
                }
                else if (txtfolio.Text == "")
                {
                    Alert.ShowAlertError("Escriba el Folio", this);
                }
                else
                {
                    InventariosENT entidad = new InventariosENT();
                    InventariosCOM componente = new InventariosCOM();
                    entidad.Pfolio = Convert.ToInt32(txtfolio.Text);
                    entidad.Pobservaciones = txtObservaciones.Text;
                    entidad.Pidc_puesto = Convert.ToInt32(ddlPuesto.SelectedValue);
                    entidad.Pidc_actscategoria = Convert.ToInt32(ddlcategorias.SelectedValue);
                    entidad.Parea = lnkarea.CssClass == "btn btn-info btn-block" ? true : false;
                    entidad.Pidc_areaact = Convert.ToInt32(ddlarea.SelectedValue);
                    entidad.Ptotal_cadena = TotalCadena();
                    entidad.Pcadena = Cadena();
                    DataSet ds = componente.AgregarFormulario(entidad);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    if (vmensaje == "")
                    {
                        Alert.ShowGiftMessage("Estamos procesando la solicitud.", "Espere un Momento", "inventario.aspx", "imagenes/loading.gif", "3000", "Formulario Guardado Correctamente", this);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje, this);
                    }
                }
            }
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value_can"];
            confirmValue = confirmValue.Substring(confirmValue.LastIndexOf(",") + 1);
            if (confirmValue == "Yes")
            {
                idc_puesto = 0;
                Response.Redirect("inventario.aspx");
            }
        }

        private String Cadena()
        {
            string cadena = "";
            foreach (RepeaterItem item in repeatreractivos.Items)
            {
                TextBox txtespec = (TextBox)item.FindControl("txtespec");
                TextBox txtNumero = (TextBox)item.FindControl("txtNumero");
                TextBox txtvaloract = (TextBox)item.FindControl("txtvaloract");
                LinkButton lnkaplica = (LinkButton)item.FindControl("lnkaplica");
                if (lnkaplica.CssClass == "btn btn-default btn-block")
                {
                    cadena = cadena + txtNumero.Text + ";" + txtvaloract.Text + ";";
                }
            }
            return cadena;
        }

        private int TotalCadena()
        {
            int cadena = 0;
            foreach (RepeaterItem item in repeatreractivos.Items)
            {
                TextBox txtespec = (TextBox)item.FindControl("txtespec");
                TextBox txtNumero = (TextBox)item.FindControl("txtNumero");
                TextBox txtvaloract = (TextBox)item.FindControl("txtvaloract");
                LinkButton lnkaplica = (LinkButton)item.FindControl("lnkaplica");
                if (lnkaplica.CssClass == "btn btn-default btn-block")
                {
                    cadena = cadena + 1;
                }
            }
            return cadena;
        }
    }
}