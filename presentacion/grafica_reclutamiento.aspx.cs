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
    public partial class grafica_reclutamiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["fecha_inicio"] != null)
                {
                    DateTime fi = Convert.ToDateTime(Request.QueryString["fecha_inicio"]);
                    DateTime ff = Convert.ToDateTime(Request.QueryString["fecha_fin"]);
                    //int idc_puesto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"]));
                    GenerarDatos(fi, ff);
                }
            }
        }

        public List<String> meses = new List<string>();
        public List<int> valores = new List<int>();

        private void GenerarDatos(DateTime fecha_i, DateTime fecha_f)
        {
            try
            {
                TareasAutomaticasENT entidad = new TareasAutomaticasENT();
                //entidad.Pidc_puesto_realiza = idc_puesto;
                entidad.Pfecha_empieza = fecha_i;
                entidad.Pfecha_termina = fecha_f;
                entidad.Ptipofiltro = 1;
               // entidad.Pidc_depto = IDC_DEPTO;
                TareasAutomaticasCOM componente = new TareasAutomaticasCOM();
                DataSet ds = componente.DatosGraficasReclu(entidad);
                string nombre = " Reclutamiento";
                string total = ds.Tables[0].Rows[0]["total"].ToString();
                string bien_sistema = ds.Tables[1].Rows[0]["bien_sistema"].ToString();
                string mal_sistema = ds.Tables[1].Rows[0]["mal_sistema"].ToString();
                gridsistema.DataSource = ds.Tables[0];
                gridsistema.DataBind();
                div_reporte.Visible = true;
                LBLTITLE.Text = "Reporte de Rendimiento de " + nombre;
                lblrango.Text = (Convert.ToDateTime(txtfechainicio.Text).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"))).ToString() 
                    + " a " + (Convert.ToDateTime(txtfechafin.Text).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("es-MX"))).ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "GraficaSistema(" + bien_sistema + "," + mal_sistema + ");", true);
               
               
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
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
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                if (txtfechafin.Text == "" || txtfechainicio.Text == "")
                {
                    Alert.ShowAlertError("Ingrese Ambas Fechas", this);
                }
                else
                {
                    DateTime fi = Convert.ToDateTime(txtfechainicio.Text);
                    DateTime ff = Convert.ToDateTime(txtfechafin.Text);
                    //int idc_puesto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"]));
                    GenerarDatos(fi, ff);
                    int tipo = Convert.ToInt32(gridsistema.DataKeys[index].Values["tipo_filtro"]);
                    if (tipo > 0)
                    {
                        TareasAutomaticasENT entidad = new TareasAutomaticasENT();
                        //entidad.Pidc_puesto_realiza = idc_puesto;
                        entidad.Pfecha_empieza = fi;
                        entidad.Pfecha_termina = ff;
                        entidad.Ptipofiltro = tipo;
                        // entidad.Pidc_depto = IDC_DEPTO;
                        TareasAutomaticasCOM componente = new TareasAutomaticasCOM();
                        DataSet ds = componente.DatosGraficasReclu(entidad);
                        PlaceHolder.Controls.Clear();
                        PlaceHolder.Controls.Add(new Literal { Text = funciones.TableDinamic(ds.Tables[2], "t1").ToString() });
                        ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "DataTa1('#t1');", true);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }


        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (txtfechafin.Text == "" || txtfechainicio.Text == "")
            {
                Alert.ShowAlertError("Ingrese Ambas Fechas",this);
            }
            else {
                DateTime fi = Convert.ToDateTime(txtfechainicio.Text);
                DateTime ff = Convert.ToDateTime(txtfechafin.Text);
                //int idc_puesto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"]));
                GenerarDatos(fi, ff);
            }
        }
    }
}