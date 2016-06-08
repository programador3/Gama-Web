using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class grafica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime fi = Convert.ToDateTime(Request.QueryString["fecha_inicio"]);
                DateTime ff = Convert.ToDateTime(Request.QueryString["fecha_fin"]);
                int idc_puesto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"]));

                int pidc_depto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["pidc_depto"]));
                GenerarDatos(fi, ff, idc_puesto, pidc_depto);
            }
        }

        private void GenerarDatos(DateTime fecha_i, DateTime fecha_f, int idc_puesto, int IDC_DEPTO)
        {
            try
            {
                TareasAutomaticasENT entidad = new TareasAutomaticasENT();
                entidad.Pidc_puesto_realiza = idc_puesto;
                entidad.Pfecha_empieza = fecha_i;
                entidad.Pfecha_termina = fecha_f;
                entidad.Pidc_depto = IDC_DEPTO;
                TareasAutomaticasCOM componente = new TareasAutomaticasCOM();
                DataSet ds = componente.DatosGraficas(entidad);
                string nombre = ds.Tables[0].Rows[0]["empleado"].ToString();
                string total = ds.Tables[0].Rows[0]["total"].ToString();
                string bien_sistema = ds.Tables[3].Rows[0]["bien_sistema"].ToString();
                string mal_sistema = ds.Tables[3].Rows[0]["mal_sistema"].ToString();
                string bien_usuario = ds.Tables[3].Rows[0]["bien_usuario"].ToString();
                string mal_usuario = ds.Tables[3].Rows[0]["mal_usuario"].ToString();
                gridsistema.DataSource = ds.Tables[1];
                gridsistema.DataBind();
                grisusuario.DataSource = ds.Tables[2];
                grisusuario.DataBind();
                LBLTITLE.Text = "Reporte de Rendimiento de " + nombre;
                lblrango.Text = (Convert.ToDateTime(Request.QueryString["fecha_inicio"]).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"))).ToString() + " a " + (Convert.ToDateTime(Request.QueryString["fecha_fin"]).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"))).ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "GraficaSistema(" + bien_sistema + "," + mal_sistema + ");", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT2", "GraficaUsuario(" + bien_usuario + "," + mal_usuario + ");", true);
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        private String ReturnCadenaGrafica(DataTable dt)
        {
            string cadena = "";
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt32(row["mostrar_grafica"]) == 1)
                {
                    cadena = cadena + "{name:'" + row["name"].ToString() + "',y:" + row["total"].ToString() + ",color:'" + row["b_color"] + "'}";
                }
            }
            cadena = cadena.Remove(cadena.Length - 1);
            return cadena;
        }

        protected void gridsistema_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string f_color = rowView["f_color"].ToString();
                string b_color = rowView["b_color"].ToString();
                if (b_color != "" && f_color != "")
                {
                    e.Row.BackColor = Color.FromName(b_color);
                    e.Row.ForeColor = Color.FromName(f_color);
                }
            }
        }
    }
}