using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;

namespace presentacion
{
    public partial class consulta_precios_m : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                cargar_combo_agentes_usuario((int)Session["sidc_usuario"]);
                //cargar_combo_agentes_usuario(127);
                divBuscar.Visible = false;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ddlProductos_Master.SelectedValue == "" || ddlClientes.SelectedValue == "")
            {
                Alert.ShowAlertError("Elige una opcion...", this.Page);
            }
            else
            {
                llenarCampos();
            }
        }


        private void llenarCampos()
        { 

            consulta_precios_mCOM comp = new consulta_precios_mCOM();
            consulta_precios_mENT ent = new consulta_precios_mENT();
            
            ent.Pidc_articulo =   Convert.ToInt32(ddlProductos_Master.SelectedValue) ;
            ent.Pidc_cliente = Convert.ToInt32(ddlClientes.SelectedValue);
            ent.Pidc_sucursal = Convert.ToInt32(Session["idc_sucursal"].ToString());
            ent.Pcantidad = 1;
            ent.Pcambiolista = false;
            DataSet ds = comp.buscar_precio_producto_nuevo(ent);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Limpiar_Campos();
                txtPrecio.Text = string.Format("$ {0:0,0.00}", Convert.ToDouble(ds.Tables[0].Rows[0]["precio"].ToString()));
                //txtidc_Articulo.Text = ds.Tables[0].Rows[0]["idc_articulo"].ToString();
                txtCodigo_Articulo.Text = ds.Tables[0].Rows[0]["codigo"].ToString();
                txtPrecio_Lista.Text = string.Format("$ {0:0,0.00}", Convert.ToDouble(ds.Tables[0].Rows[0]["precio_lista"].ToString()));
                txtPrecio_Minimo.Text = string.Format("$ {0:0,0.00}", Convert.ToDouble(ds.Tables[0].Rows[0]["precio_minimo"].ToString()));
                txtPrecio_Real.Text = string.Format("$ {0:0,0.00}", Convert.ToDouble(ds.Tables[0].Rows[0]["precio_real"].ToString()));
                DataTable dt = (DataTable)ViewState["productos"];
                string str = string.Format("idc_articulo ={0}", ddlProductos_Master.SelectedValue.ToString());
                if (dt != null)
                {
                    DataRow[] dr = dt.Select(str);
                    if (dr.Length > 0)
                    {
                        txtUM.Text = dr[0]["nom_corto"].ToString();
                        txtDescripcion.Text = dr[0]["desart"].ToString();
                        
                    }
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    CultureInfo Cultuture_Info = new CultureInfo("es-MX"); //CultureInfo.CreateSpecificCulture("es-MX");
                    bool cambio = Convert.ToBoolean( ds.Tables[1].Rows[0]["cambio"].ToString()) ;

                    double precio = Convert.ToDouble(ds.Tables[1].Rows[0]["precio"].ToString());//.ToString("C",Cultuture_Info)
                    DateTime fecha = Convert.ToDateTime(ds.Tables[1].Rows[0]["FECHA"].ToString());
                    double Nota_C = Convert.ToDouble(ds.Tables[1].Rows[0]["precio_real"].ToString());//.ToString("C", Cultuture_Info);
                    
                    txtUlt_Precio_Fac.Text = string.Format("$ {0:0,0.00}  {1}", precio, (cambio ? "***" : ""));
                    txtFecha.Text =           fecha.ToString("D",Cultuture_Info);                   
                    txtNota_Credito.Text =  string.Format((Nota_C != precio ? "$ {0:0,0.00} ": "" ), Nota_C);
                }
                Alert.ShowAlertAutoCloseTimer("loading...", "", "2000", false, "imagenes/horizontal-loader.gif", this.Page);
            }
        }

        
        protected void btnCancelarTodo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ficha_cliente_m.aspx");
        }

        private void cargar_combo_agentes_usuario(int idc_usuario)
        {
            try
            {
                consulta_precios_mCOM comp = new consulta_precios_mCOM();
                Datos_Usuario_logENT dul = new Datos_Usuario_logENT();
                dul.Pidc_usuario = idc_usuario;
                DataSet ds = comp.agentes_vs_usuarios(dul);
                ddlAgente.DataValueField = "idc_agente";
                ddlAgente.DataTextField = "nombre3";
                ddlAgente.DataSource = ds.Tables[0];
                ddlAgente.DataBind();

                Alert.ShowAlertAutoCloseTimer("loading...", "", "2000", false, "imagenes/horizontal-loader.gif", this.Page);

            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void ddlAgente_Changed(Object sender, EventArgs e)
        {
            try
            {
                consulta_precios_mCOM comp = new consulta_precios_mCOM();
                consulta_precios_mENT ent = new consulta_precios_mENT();     
               
                ent.Pidc_agente = Convert.ToInt32(ddlAgente.SelectedValue.ToString());
                DataSet ds = comp.clientes_por_agente(ent);
                ddlClientes.Items.Clear();
                ddlProductos_Master.Items.Clear();
                ddlTipo_Producto.SelectedIndex = 0;
                divBuscar.Visible = false;
                Limpiar_Campos();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlClientes.DataSource = ds.Tables[0];
                    ddlClientes.DataValueField = "idc_cliente";
                    ddlClientes.DataTextField = "nombre";
                    ddlClientes.DataBind();
                    ViewState["clientes"] = ds.Tables[0];
                    ddlClientes.Items.Insert(0, new ListItem("--Elige una Opción", "0")); //updated code}

                    //gridclientes.DataSource = ds.Tables[0];
                    //gridclientes.DataBind();
                    Alert.ShowAlertAutoCloseTimer("loading...", "", "2000", false, "imagenes/horizontal-loader.gif", this.Page);
                }
                else
                {
                    ddlClientes.Items.Insert(0, new ListItem("--No Existen datos--", "0"));
                    Alert.ShowAlertAutoCloseTimer("loading...", "", "500", false, "imagenes/horizontal-loader.gif", this.Page);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void ddlClientes_Changed(Object sender, EventArgs e)
        {
            try
            {
                consulta_precios_mCOM comp = new consulta_precios_mCOM();
                consulta_precios_mENT ent = new consulta_precios_mENT();

                ent.Pidc_cliente = ddlClientes.SelectedValue != "" ? Convert.ToInt32(ddlClientes.SelectedValue.ToString()) : 0;
                DataSet ds = comp.Carga_Lista_Master_Cot_nueva(ent);
                ddlProductos_Master.Items.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlProductos_Master.DataSource = ds.Tables[0];
                    ddlProductos_Master.DataValueField = "idc_articulo";
                    ddlProductos_Master.DataTextField = "desart";
                    ddlProductos_Master.DataBind();
                    ViewState["productos"] = ds.Tables[0];
                    Alert.ShowAlertAutoCloseTimer("loading...", "", "2000", false, "imagenes/horizontal-loader.gif", this.Page);
                }
                else
                {                    
                    ddlProductos_Master.Items.Insert(0, new ListItem("--No Existen datos--", "0")); //updated code}  
                    Alert.ShowAlertAutoCloseTimer("loading...", "", "500", false, "imagenes/horizontal-loader.gif", this.Page);
                }
                
                //
                
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void ddlTipo_Producto_Changed(Object sender, EventArgs e)
        {
            if (ddlTipo_Producto.SelectedValue == "1")
            {
                divBuscar.Visible = true;
                lblProducto.Text = "Producto";
                //h4Producto.Visible = true;
            }
            else
            {
                divBuscar.Visible = false;
                lblProducto.Text = "Producto Master";
            }
        }

        protected void ddlProductos_Master_Changed(Object sender, EventArgs e)
        {
            llenarCampos();
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            //No Existen Articulos con esa Descripcion.
            string search = txtsearch.Text;

            try
            {
                consulta_precios_mCOM comp = new consulta_precios_mCOM();
                consulta_precios_mENT ent = new consulta_precios_mENT();
                Datos_Usuario_logENT dul = new Datos_Usuario_logENT();

                ent.Pvalor = txtsearch.Text.Trim();
                ent.Ptipo = "c";
                ent.Pidc_sucursal = Convert.ToInt32(Session["idc_sucursal"].ToString()); 
                dul.Pidc_usuario = (int)Session["sidc_usuario"];

                DataSet ds = comp.buscar_productos(ent,dul);
                ddlProductos_Master.Items.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlProductos_Master.DataSource = ds.Tables[0];
                    ddlProductos_Master.DataValueField = "idc_articulo";
                    ddlProductos_Master.DataTextField = "desart";
                    ddlProductos_Master.DataBind();
                    ViewState["productos"] = ds.Tables[0];
                    Alert.ShowAlertAutoCloseTimer("loading...", "", "2000", false, "imagenes/horizontal-loader.gif", this.Page);
                }
                else
                {
                    ddlProductos_Master.Items.Insert(0, new ListItem("--No Existen datos--", "0")); //updated code}  
                    txtsearch.Text = "";
                    Alert.ShowAlertError("No Existen Articulos con esa Descripcion.", this.Page);
                }

                //

            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }

        }

        protected void btnAceptar1_Click(object sender,EventArgs e)
        {
            
                //if ((dt != null))
                //{
                //    //DataRow dr =
                //        dt.Select("idc_articulo =" + ddlProductos_Master.SelectedValue))
                    
                //    if ((rows.Length > 0))
                //    {
                //        txtum.Text = rows(0)("nom_corto").ToString().Trim();
                //        txtdescripcion.Text = rows(0)("desart").ToString().Trim();
                //    }
                //}
                /*
                dt = ViewState("productos")
            If Not dt Is Nothing Then
                rows = dt.Select("idc_articulo =" & cbomaster.SelectedValue)
                If (rows.Length > 0) Then
                    txtum.Text = rows(0)("nom_corto").ToString().Trim()
                    txtdescripcion.Text = rows(0)("desart").ToString().Trim()
                End If
            End If
            ultimo_precio(ds.Tables(0).Rows(0).Item("idc_articulo"))
            
                */

           



        }


        private void Limpiar_Campos()
        {
            txtDescripcion.Text = "";
            txtCodigo_Articulo.Text = "";
            txtUM.Text = "";
            txtPrecio.Text = "";
            txtPrecio_Minimo.Text = "";
            txtPrecio_Lista.Text = "";
            txtPrecio_Real.Text = "";
            txtUlt_Precio_Fac.Text = "";
            txtNota_Credito.Text = "";
            txtFecha.Text = "";
        }
    }
}