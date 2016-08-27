using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;


namespace presentacion
{
    public partial class consulta_quejas_m : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (Request.QueryString["cdi"] != null)
            {
                if (!IsPostBack)
                {
                    int cdi = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["cdi"]));
                    CargarQuejas(cdi);
                }
            }
            else {
                txtnoqueja.ReadOnly = false;
            }
        }

        private void CargarQuejas(int cdi)
        {
            QuejasENT entidad = new QuejasENT();
            QuejasCOM componente = new QuejasCOM();
            entidad.Pidc_queja = cdi;
            DataSet ds = componente.SDatosQuejas(entidad);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                txtnoqueja.Text = cdi.ToString();
                txtcliente.Text = row["nombre"].ToString();
                txtfrc.Text = row["rfccliente"].ToString();
                txtcveadi.Text = row["cveadi"].ToString();
                txtqueja.Text = row["problema"].ToString();
                txtfactura.Text = row["codfac"].ToString();
                txtcaptura.Text = row["usuario"].ToString();
                txtCalle.Text = row["calle"].ToString();
                txtcolonia.Text = row["colonia"].ToString();
                txtnumero.Text = row["numero"].ToString();
                txtobservaciones.Text = row["observaciones"].ToString();
                txttkm.Text = row["nombre_tmk"].ToString();
                txtagente.Text = row["nombre_age"].ToString();
                txtcomprador.Text = row["nombre_comprador"].ToString();
                txtcontacto.Text = row["contacto_obra"].ToString();
                txttelcomprador.Text = row["tel_comprador"].ToString();
                txttelcontacto.Text = row["tel_contacto"].ToString();
                txtteñtkm.Text = row["tel_tmk"].ToString();
                txttelefonoagente.Text = row["tel_age"].ToString();
                txthoravisita.Text = row["hora_visita"].ToString();
                txtfecha.Text = Convert.ToDateTime(row["fecha"]).ToString("dd MMMM, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX"));
                txtmunicipio.Text = row["municipio"].ToString() + ", " + row["estado"].ToString() + ", " + row["pais"].ToString();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    griddatos.DataSource = ds.Tables[1];
                    griddatos.DataBind();
                }
                if (Convert.ToBoolean(row["pendiente"]) == false)
                {
                    Alert.ShowAlertInfo("La tarea fue Resuelta", "Mensaje del sistema", this);
                    cancelada.Visible = true;
                    lblcance.Text = "Resuelta".ToUpper();
                    lblcance.Visible = true;
                }
                else
                {
                    if (Convert.ToBoolean(row["cancelada"]) == true)
                    {
                        Alert.ShowAlertInfo("La Tarea fue cancelada", "Mensaje del sistema", this);
                        cancelada.Visible = true;
                        lblcance.Text = "CANCELADA";
                        lblcance.Visible = true;
                    }
                    else
                    {
                        lblcance.Visible = false;
                    }
                }
            }
            else {
                txtnoqueja.Text = cdi.ToString();
                txtcliente.Text = "";
                txtfrc.Text = "";
                txtcveadi.Text = "";
                txtqueja.Text = "";
                txtfactura.Text = "";
                txtcaptura.Text = "";
                txtCalle.Text = "";
                txtcolonia.Text = "";
                txtnumero.Text = "";
                txtobservaciones.Text = "";
                txttkm.Text = "";
                txtagente.Text = "";
                txtcomprador.Text = "";
                txtcontacto.Text = "";
                txttelcomprador.Text = "";
                txttelcontacto.Text = "";
                txtteñtkm.Text = "";
                txttelefonoagente.Text = "";
                txthoravisita.Text = "";
                txtfecha.Text = "";
                txtmunicipio.Text = "";
                griddatos.DataSource = null;
                griddatos.DataBind();
                Alert.ShowAlertInfo("No se encontraron resultados","Mensaje del sistema",this);
            }
        }

        protected void txtnoqueja_TextChanged(object sender, EventArgs e)
        {
            if (txtnoqueja.Text != "")
            {
                CargarQuejas(Convert.ToInt32(txtnoqueja.Text));
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("consulta_quejas_m.aspx");
        }
    }
}