using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace presentacion
{
    public partial class Oc_Digitales_Pendientes_2 : System.Web.UI.Page
    {
        public AgentesCOM componente = new AgentesCOM();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["XUnidad"] = null;
                Orden_Compra.Src = System.Configuration.ConfigurationManager.AppSettings["server"] + "/imagenes/sin_OC.png";
                cargar_combo_oc_cliente(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["id"])));
                btnseleccionar.Attributes["onclick"] = "return pick();";
            }
        }

        public void cargar_combo_oc_cliente(int idc_cliente)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = componente.sp_oc_clientes2(idc_cliente);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        cboOC.DataSource = ds.Tables[0];
                        cboOC.DataTextField = ("no_occ_fecha");
                        cboOC.DataValueField = ("idc_occli");
                        cboOC.DataBind();
                        cboOC.Items.Insert(0, "   ---Seleccionar---");
                    }
                    else
                    {
                        Alert.ShowAlertInfo("El Cliente no Cuenta con OC.", "Mensaje del Sistema", this);
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DataRow row = default(DataRow);
                        row = ds.Tables[1].Rows[0];
                        txtrfc.Text = row["rfccliente"].ToString();
                        txtclave.Text = row["cveadi"].ToString();
                        txtcliente.Text = row["nombre"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                CargarMsgBox(ex.ToString());
            }

        }

        protected void cboOC_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DataSet ds = new DataSet();
            string ruta = null;
            string rutadestino = null;
            DataRow row = default(DataRow);
            try
            {

                if (cboOC.SelectedIndex > 0)
                {
                    string unidad = funciones.GenerarRuta("OCCLI", "UNIDAD");
                    string[] ext = new string[5];
                    ext[0] = ".JPG";
                    ext[1] = ".BMP";
                    ext[2] = ".DIB";
                    ext[4] = ".GIF";
                    for (Int16 i = 0; i <= ext.Length - 1; i++)
                    {
                        ruta = unidad + "\\" + cboOC.SelectedValue + ext[i].ToString();
                        if (File.Exists(ruta))
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/temp/files/"));
                            rutadestino = dirInfo + cboOC.SelectedValue + ext[i].ToString();
                            if (!File.Exists(rutadestino))
                            {
                                File.Copy(ruta, rutadestino);
                                Orden_Compra.Src = System.Configuration.ConfigurationManager.AppSettings["server"] + "/temp/files/" + cboOC.SelectedValue + ext[i].ToString();
                                break; // TODO: might not be correct. Was : Exit For
                            }
                            else
                            {
                                Orden_Compra.Src = System.Configuration.ConfigurationManager.AppSettings["server"] + "/temp/files/" + cboOC.SelectedValue + ext[i].ToString();
                            }
                        }
                    }

                }
                else
                {
                    Orden_Compra.Src = System.Configuration.ConfigurationManager.AppSettings["server"] + "/imagenes/sin_OC.png";
                }

            }
            catch (Exception ex)
            {
                CargarMsgBox(ex.ToString());
            }
        }


        public void CargarMsgBox(string msg)
        {
            Alert.ShowAlertError(msg, this);
        }
        public void datos()
        {
            string[] valor = null;
            valor = cboOC.SelectedItem.ToString().Split(new string[] { "||" }, StringSplitOptions.None);
            if (valor.Length == 2)
            {
                txtno_oc.Text = valor[0].Trim();
                txtidoc.Text = cboOC.SelectedValue;

            }
            else if (valor.Length == 0)
            {
            }
        }

        protected void Page_LoadComplete(object sender, System.EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), Guid.NewGuid().ToString(), "<script>divs();</script>", false);
        }
    }
}