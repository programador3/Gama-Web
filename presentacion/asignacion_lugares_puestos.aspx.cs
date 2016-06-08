using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class asignacion_lugares_puestos : System.Web.UI.Page
    {
        public static string areas_value = "";//variables estatica para las imagenes de areas
        public static string lugares_value = "";//variables estatica para las imagenes de lugares

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["idc_puesto_areas"] = Request.QueryString["idc_puesto"] == null ? 0 : Convert.ToInt32(Request.QueryString["idc_puesto"]);
                //cargamos los datos iniciales
                Session["idc_lugar"] = null;

                TablaDatosAreas(Convert.ToInt32(Request.QueryString["idc_puesto"]), 0);
            }
            //detecto dominio, si es localhost quito primer subcarpeta de uni_archi
            var domn = Request.Url.Host;
            areas_value = domn == "localhost" ? "/imagenes/" : funciones.GenerarRuta("AREAS", "rw_carpeta");
            lugares_value = domn == "localhost" ? "/imagenes/" : funciones.GenerarRuta("LUG_MAR", "rw_carpeta");
        }

        /// <summary>
        /// Carga los datos de SQL
        /// </summary>
        /// <param name="idc_puesto"></param>
        /// <param name="idc_area"></param>
        private void TablaDatosAreas(int idc_puesto, int idc_area)
        {
            if (idc_puesto == 0) { Response.Redirect("menu.aspx"); }
            LugaresENT entidades = new LugaresENT();
            LugaresCOM com = new LugaresCOM();
            entidades.Pidc_puesto = idc_puesto;
            entidades.Pidc_area = idc_area;
            DataSet ds = com.CargaAreas(entidades);
            if (ds.Tables[0].Rows.Count > 0)
            {
                String sucursal = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(ds.Tables[0].Rows[0]["sucursal"].ToString());
                String puesto = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(ds.Tables[0].Rows[0]["puesto"].ToString());
                sucursal = sucursal.ToLower();
                puesto = puesto.ToLower();
                lblsucursal.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(sucursal);
                lblpuesto.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(puesto);
                //SI EL AREA NO ES CERO, CARGO LOS REPEATS Y CONTROLES
                if (idc_area != 0)
                {
                    panel_lugares.Visible = ds.Tables[1].Rows.Count > 0 ? true : false;
                    panel_sinlugares.Visible = ds.Tables[1].Rows.Count > 0 ? false : true;
                    repeater_puestos.DataSource = ds.Tables[1];
                    repeater_puestos.DataBind();
                }
                else
                {//SI ES CERO SOLO MUESTRO COMBO
                    ddlAreas.DataTextField = "descripcion";
                    ddlAreas.DataValueField = "idc_area";
                    ddlAreas.DataSource = ds.Tables[0];
                    ddlAreas.DataBind();
                    ddlAreas.Items.Insert(0, new ListItem("Seleccione uno", "0")); //updated code}
                }
            }
        }

        protected void ddlAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //VALOR SELECCIONADO
            int value = Convert.ToInt32(ddlAreas.SelectedValue);
            if (value == 0)
            {
                Alert.ShowAlertError("Seleccione una opcion valida", this);
            }
            else
            {
                //OBTENGO PATH ABSOLUTO
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/imagenes/areas/"));//path local
                //CARGO IMAGEN DEL AREA PRINCIPAL
                CopiarFile(funciones.GenerarRuta("areas", "unidad"), dirInfo.ToString(), value.ToString());
                //ASIGNO ATRIBUTOS A ELEMTO IMG
                Areaim.Attributes["src"] = "" + areas_value + "areas/" + value.ToString() + ".jpg";
                a_img.Attributes["href"] = "" + areas_value + "areas/" + value.ToString() + ".jpg";
                a_img.Attributes["title"] = "Esta es una vista previa del area " + ddlAreas.SelectedItem;
                //CARGO LOS LUGARES
                Session["idc_lugar"] = null;
                TablaDatosAreas(Convert.ToInt32(Session["idc_puesto_areas"]), value);
            }
        }

        protected void btnLugar_Click(object sender, EventArgs e)
        {
            LinkButton btnLugar_sender = (LinkButton)sender;
            //buscamos el control seleccionado en el repeat
            foreach (RepeaterItem item in repeater_puestos.Items)
            {
                LinkButton btnLugar = (LinkButton)item.FindControl("btnLugar");
                int lugar = Convert.ToInt32(btnLugar.CommandArgument);
                string url = "" + lugares_value + "lugares/";
                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/imagenes/lugares/"));//path local
                string url_ab = HttpContext.Current.Request.Url.Authority;
                if (btnLugar == btnLugar_sender && lugar == 0)//si es igual y no esta asignado
                {
                    btnLugar.CssClass = "btn btn-success btn-block";
                    string value = btnLugar.CommandName;
                    CopiarFile(funciones.GenerarRuta("LUG_MAR", "unidad"), dirInfo.ToString(), value);
                    Areaim.Attributes["src"] = url + value.ToString() + ".jpg";
                    a_img.Attributes["href"] = url + value.ToString() + ".jpg";
                    a_img.Attributes["title"] = "Esta es una vista previa del area " + ddlAreas.SelectedItem;
                    Session["idc_lugar"] = Convert.ToInt32(value);
                }
                if (btnLugar == btnLugar_sender && lugar != 0)//si esta asignado
                {
                    Alert.ShowAlertError("El lugar " + btnLugar.Text + " ya esta ocupado", this);
                    string value = btnLugar.CommandName;
                    CopiarFile(funciones.GenerarRuta("LUG_MAR", "unidad"), dirInfo.ToString(), value);
                    Areaim.Attributes["src"] = url + value.ToString() + ".jpg";
                    a_img.Attributes["href"] = url + value.ToString() + ".jpg";
                    a_img.Attributes["title"] = "Esta es una vista previa del area " + ddlAreas.SelectedItem;
                }
                if (btnLugar != btnLugar_sender && lugar == 0)//limpiamos los demas
                {
                    btnLugar.CssClass = "btn btn-default btn-block";
                }
            }
        }

        /// <summary>
        /// SI NO EXISTE UN ARCHIVO LO COPIA
        /// </summary>
        /// <param name="path_origen"></param>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        private static void CopiarFile(string path_origen, string path, string filename)
        {
            File.Copy(path_origen + filename.ToString() + ".jpg", path + filename.ToString() + ".jpg", true);
        }

        protected void repeater_puestos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            LinkButton btnLugar = (LinkButton)e.Item.FindControl("btnLugar");
            int lugar = Convert.ToInt32(DataBinder.Eval(dbr, "lugar"));
            btnLugar.CssClass = lugar == 0 ? "btn btn-default btn-block" : "btn btn-warning btn-block";//SI EL LUGAR ES 0 QUIERE DECIR QUE NO ESTA OCUPADO, EN CASO CONTRARIO LO COLOREO
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Session["Caso_Confirmacion"] = "Guardar";
            int idc_lugar = Convert.ToInt32(Session["idc_lugar"]);
            if (idc_lugar == 0)
            {
                Alert.ShowAlertError("Debe seleccionar un lugar para poder guardar.", this);
            }
            else
            {
                content_modal.Text = "Desea guardar este Lugar para " + lblpuesto.Text;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm();", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            List<String> listas_url = (List<String>)Session["lista"];
            int index = listas_url.Count;
            String url = listas_url[index - 1];

            if (url == null || url == "") { url = "menu.aspx"; }
            url = url.Replace("%20", "+");
            String path_actual_COMPLETO = HttpContext.Current.Request.Url.AbsoluteUri;
            string PreviousPage = Request.ServerVariables["HTTP_REFERER"];
            path_actual_COMPLETO = path_actual_COMPLETO.Replace("%20", "+");
            if (url == path_actual_COMPLETO)
            {
                listas_url.RemoveAt(index - 1);
                Session["lista"] = listas_url;
                index = listas_url.Count;
                url = listas_url[index - 1];
                if (url == null || url == "") { url = "menu.aspx"; }
                url = url.Replace("%20", "+");
                Response.Redirect(url);
            }
            else
            {
                Response.Redirect(url);
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso = (string)Session["Caso_Confirmacion"];
            switch (caso)
            {
                case "Guardar":
                    break;
            }
        }
    }
}