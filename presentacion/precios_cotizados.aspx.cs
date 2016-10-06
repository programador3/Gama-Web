using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace presentacion
{
    public partial class precios_cotizados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                int idc_cliente = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_cliente"]));
                cargar_datos_cliente(idc_cliente);
            }
        }

        private void cargar_datos_cliente(int idc_cliente)
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                entidad.Pidc_cliente = idc_cliente;
                DataSet ds = com.sp_ARTICULOS_CLIENTE_DESC_cedis(entidad);
                DataView dv = new DataView();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("comi", typeof(decimal));
                    ds.Tables[0].Columns.Add("dias", typeof(int));
                    ds.Tables[0].Columns.Add("uf", typeof(string));
                    dv = ds.Tables[0].DefaultView;
                    decimal costo = decimal.Zero;
                    decimal precio = default(decimal);
                    decimal porc = default(decimal);
                    decimal margen = default(decimal);
                    decimal margencompartido = default(decimal);
                    decimal vventaart = default(decimal);


                    for (int i = 0; i <= dv.ToTable().Rows.Count - 1; i++)
                    {
                        precio = Convert.ToDecimal(dv[i]["precio_cliente"]);
                        costo = Convert.ToDecimal(dv[i]["costo2"]);
                        vventaart = Convert.ToDecimal(dv[i]["precio_cliente"]);
                        margen = (1 - (costo / precio)) * 100;
                        margen = (margen < 4 ? margen : (margen < 6 ? 4 : (margen < 8 ? 6 : (margen < 10 ? 8 : (margen < 12 ? 10 : margen)))));
                        porc = (((Convert.ToDecimal(2.5) / 22) / 100) * margen) * 100;
                        margencompartido = margen * Convert.ToDecimal(0.1136);
                        if (margen >= 12)
                        {
                            porc = margencompartido * Convert.ToDecimal(1);
                        }
                        else if (margen >= 10)
                        {
                            porc = margencompartido * Convert.ToDecimal(0.75);
                        }
                        else if (margen >= 8)
                        {
                            porc = margencompartido * Convert.ToDecimal(0.5);
                        }
                        else if (margen >= 6)
                        {
                            porc = margencompartido * Convert.ToDecimal(0.25);
                        }                       
                        dv[i]["comi"] = porc.ToString("#.####");
                        dv[i]["dias"] = (Convert.ToDateTime(dv[i]["ultima_venta"]) - DateTime.Now).TotalDays;
                        dv[i]["uf"] = Convert.ToDateTime(dv[i]["ultima_venta"]).ToString("dd MMMM, yyyy H:mm:ss", CultureInfo.CreateSpecificCulture("es-MX"));
                    }

                    gridproductos.DataSource = dv.ToTable();
                    gridproductos.DataBind();
                }
                else
                {
                    gridproductos.DataSource = null;
                    gridproductos.DataBind();
                    Alert.ShowAlertError("No se Encontraron Datos", this.Page);
                }

        

            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
    }
}