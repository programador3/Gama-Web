using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.Script.Serialization;
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

        public List<String> meses = new List<string>();
        public List<int> valores = new List<int>();

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
                int contador = 1;
                table.Controls.Add(new Literal { Text = funciones.TableDinamic(ds.Tables[4], "t1").ToString() });
                ScriptManager.RegisterStartupScript(this, GetType(), "noti533W3" + contador.ToString(), "DataTa" + contador.ToString() + "('#t" + contador.ToString() + "');", true);
                Session["ds.Tables[4]"] = ds.Tables[4];
                DataTable dtmeses = ds.Tables[5];
                List<string> meses1 = new List<string>();
                List<int> valores1 = new List<int>();
                foreach (DataRow rw in dtmeses.Rows)
                {
                    meses.Add(rw["mes"].ToString());
                    valores.Add(Convert.ToInt32(rw["valor"]));
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        public static class JavaScript
        {
            public static string Serialize(object o)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(o);
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

        protected void gridsistema_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Redirect_sistema(index);
        }

        protected void grisusuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Redirect(index);
        }

        private void Redirect(int index)
        {
            string fi = funciones.deTextoa64(Request.QueryString["fecha_inicio"]);
            string ff = funciones.deTextoa64(Request.QueryString["fecha_fin"]);
            string idc_puesto = Request.QueryString["idc_puesto"];
            string pidc_depto = Request.QueryString["pidc_depto"];
            string tipofiltro = "";
            switch (index)
            {
                case 0://buenos resultados
                    tipofiltro = "B";
                    break;

                case 1://malos resultados
                    tipofiltro = "M";
                    break;

                case 2:
                    tipofiltro = "";
                    break;

                case 5:
                case 4://canceladas
                    tipofiltro = "P";
                    break;

                case 6://canceladas
                    tipofiltro = "C";
                    break;
            }
            Response.Redirect("rendimiento_tareas_detalles.aspx?pidc_puesto=" + idc_puesto + "&pidc_depto=" + pidc_depto + "&inicio=" + fi + "&fin=" + ff + "&tipofiltro=" + tipofiltro);
        }

        private void Redirect_sistema(int index)
        {
            string fi = funciones.deTextoa64(Request.QueryString["fecha_inicio"]);
            string ff = funciones.deTextoa64(Request.QueryString["fecha_fin"]);
            string idc_puesto = Request.QueryString["idc_puesto"];
            string pidc_depto = Request.QueryString["pidc_depto"];
            string tipofiltro = "";
            switch (index)
            {
                case 0://buenos resultados
                    tipofiltro = "B";
                    break;

                case 1://malos resultados
                    tipofiltro = "M";
                    break;

                case 2:
                    tipofiltro = "";
                    break;

                case 5:
                case 4://canceladas
                    tipofiltro = "P";
                    break;

                case 6://canceladas
                    tipofiltro = "C";
                    break;
            }
            Response.Redirect("rendimiento_tareas_detalles.aspx?pidc_puesto=" + idc_puesto + "&pidc_depto=" + pidc_depto + "&inicio=" + fi + "&fin=" + ff + "&tipofiltrosistema=" + tipofiltro);
        }

        protected void gridconcentrado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void lnkexport_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["ds.Tables[4]"];
            Export Export = new Export();
            //array de DataTables
            List<DataTable> ListaTables = new List<DataTable>();
            ListaTables.Add(dt);
            //array de nombre de sheets
            string[] Nombres = new string[] { "Tareas" };
            if (dt.Rows.Count == 0)
            {
                Alert.ShowAlertInfo("Esta Peticion no cuenta con ningun dato para crear un reporte, verifique con el departamento de Sistemas.", "", this);
            }
            else
            {
                string mensaje = Export.toExcel("Tareas", XLColor.White, XLColor.Black, 18, true, DateTime.Now.ToString(), XLColor.White,
                                   XLColor.Black, 10, ListaTables, XLColor.Orange, XLColor.White, Nombres, 1,
                                   "Tareas.xlsx", Page.Response);
                if (mensaje != "")
                {
                    Alert.ShowAlertError(mensaje, this);
                }
            }
        }
    }
}