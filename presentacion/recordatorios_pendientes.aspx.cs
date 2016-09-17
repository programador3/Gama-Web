using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class recordatorios_pendientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["post"] = "recordatorios_pendientes.aspx";
                Session["idc_recor"] = null;
                Session["dt_recor"] = null;
                Session["fecha_record"] = null;
                CargarPendientes(Convert.ToInt32(Session["sidc_usuario"]));
            }
        }

        private void CargarPendientes(int idc_usuario)
        {
            QuejasENT entidad = new QuejasENT();
            QuejasCOM com = new QuejasCOM();
            entidad.Idc_usuario = idc_usuario;
            DataSet ds = com.CargarRecordatorio(entidad);
            DataView view = ds.Tables[0].DefaultView;
            view.RowFilter = "fecha_hora <= '" + DateTime.Now + "'";
            Session["dt_recor"] = view.ToTable();
            repeat.DataSource = view.ToTable();
            repeat.DataBind();
        }

        private void CargarHistorial(int idc)
        {
            QuejasENT entidad = new QuejasENT();
            QuejasCOM com = new QuejasCOM();
            entidad.Pidc_queja = idc;
            DataSet ds = com.CargarHistorial(entidad);
            gridgistorial.DataSource = ds.Tables[0];
            gridgistorial.DataBind();
            historial.Visible = ds.Tables[0].Rows.Count == 0 ? false : true;
        }

        protected void lnktodas_Click(object sender, EventArgs e)
        {
            recor.Visible = false;
            Yes.Visible = true;
            cambio.Visible = false;
            txtobsr.Visible = true;
            historial.Visible = false;
            Session["Caso_Confirmacion"] = "Descartar Todo";
            DataTable dt = (DataTable)Session["dt_recor"];
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessreerdcdcage", "ModalConfirm('Mensaje del Sistema','Desea Descartar los " + dt.Rows.Count.ToString() + " recordatorios? ','modal fade modal-info');", true);
        }

        protected void btnver_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int idc = Convert.ToInt32(lnk.CommandArgument);
            Session["idc_recor"] = idc;
            DataTable dt = (DataTable)Session["dt_recor"];
            DataView view = dt.DefaultView;
            view.RowFilter = "idc_avisogen = " + idc + "";
            DataRow row = view.ToTable().Rows[0];
            lbldescripcion.Text = row["descripcion"].ToString();
            lnlcorreo.Text = row["correo_relacionado"].ToString() == "" ? "No relaciono Correo" : row["correo_relacionado"].ToString();
            string asunto = row["descripcion"].ToString().Replace(" ", "%20");
            correoto.HRef = row["correo_relacionado"].ToString() == "" ? "" : "mailto:" + row["correo_relacionado"].ToString() + "&subject=SEGUIMIENTO%20A%20LA%20TAREA%20'" + asunto + "'";
            Session["fecha_record"] = Convert.ToDateTime(row["fecha_hora"]);
            Yes.Visible = false;
            recor.Visible = true;
            txtobsr.Visible = false;
            CargarHistorial(idc);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessdcdcage", "ModalConfirm('Mensaje del Sistema','" + row["asunto"].ToString() + "','modal fade modal-info');", true);
        }

        protected void Fecha_Click(object sender, EventArgs e)
        {
            recor.Visible = false;
            Yes.Visible = true;
            txtobsr.Visible = false;
            cambio.Visible = true;
            historial.Visible = true;
            Session["Caso_Confirmacion"] = "Cambio";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessreerdcdcage", "ModalConfirm('Mensaje del Sistema','Ingrese el tiempo que se pospondra,'modal fade modal-info');", true);
        }

        protected void pospo_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int idc = Convert.ToInt32(lnk.CommandArgument);
            Session["idc_recor"] = idc;
            recor.Visible = false;
            Yes.Visible = true;
            txtobsr.Visible = true;
            cambio.Visible = true;
            CargarHistorial(idc);
            Session["Caso_Confirmacion"] = "Cambio";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessreerdcdcage", "ModalConfirm('Mensaje del Sistema','Ingrese el tiempo que se pospondra','modal fade modal-info');", true);
        }

        protected void pospomulti_Click(object sender, EventArgs e)
        {
            recor.Visible = false;
            Yes.Visible = true;
            txtobsr.Visible = true;
            cambio.Visible = true;
            historial.Visible = false;
            Session["Caso_Confirmacion"] = "Cambio Todo";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessreerdcdcage", "ModalConfirm('Mensaje del Sistema','Ingrese el tiempo que se pospondra','modal fade modal-info');", true);
        }

        protected void historial_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int idc = Convert.ToInt32(lnk.CommandArgument);
            Session["idc_recor"] = idc;
            recor.Visible = false;
            Yes.Visible = false;
            txtobsr.Visible = false;
            cambio.Visible = false;
            CargarHistorial(idc);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessreerdcdcage", "ModalConfirm('Mensaje del Sistema','Historial de Cambios','modal fade modal-info');", true);
        }

        protected void btndesc_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int idc = Convert.ToInt32(lnk.CommandArgument);
            Session["idc_recor"] = idc;
            recor.Visible = false;
            Yes.Visible = true;
            txtobsr.Visible = true;
            cambio.Visible = false;
            CargarHistorial(idc);
            Session["Caso_Confirmacion"] = "Descartar";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessreerdcdcage", "ModalConfirm('Mensaje del Sistema','Desea Descartar este Recordatorio?','modal fade modal-info');", true);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                DataSet ds = new DataSet();
                string vmensaje = "";
                QuejasENT entidad = new QuejasENT();
                QuejasCOM com = new QuejasCOM();
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                string cadena_uno = Convert.ToInt32(Session["idc_recor"]).ToString() + ";";
                string cadena = "";
                int total = 0;
                bool error = false;
                foreach (RepeaterItem item in repeat.Items)
                {
                    LinkButton lnk = (LinkButton)item.FindControl("lnksolucionar");
                    cadena = cadena + lnk.CommandArgument.ToString() + ";";
                    total++;
                }
                switch (caso)
                {
                    case "Descartar Todo":
                        entidad.Pobservaciones = txtobsr.Text;
                        entidad.Pobservaciones_satisfecho = cadena;
                        entidad.Pidc_queja = total;
                        ds = com.DescartarRecordatorio(entidad, 2, 0, 0, 0);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Descartar":
                        entidad.Pobservaciones = txtobsr.Text;
                        entidad.Pobservaciones_satisfecho = cadena_uno;
                        entidad.Pidc_queja = 1;
                        ds = com.DescartarRecordatorio(entidad, 2, 0, 0, 0);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;

                    case "Cambio":
                        if (txtnum.Text == "" || txtnum.Text == "0")
                        {
                            error = true;
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessreerdcdcage", "ModalConfirm('Mensaje del Sistema','Ingrese un valor mayor a 0(cero)','modal fade modal-info');", true);
                        }
                        else
                        {
                            entidad.Pobservaciones = txtobsr.Text;
                            entidad.Pobservaciones_satisfecho = cadena_uno;
                            entidad.Pidc_queja = 1;
                            ds = com.DescartarRecordatorio(entidad, 1, 0, Convert.ToInt32(txtnum.Text), Convert.ToInt32(ddltipo.SelectedValue));
                            vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        }
                        break;

                    case "Cambio Todo":
                        if (txtnum.Text == "" || txtnum.Text == "0")
                        {
                            error = true;
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessreerdcdcage", "ModalConfirm('Mensaje del Sistema','Ingrese un valor mayor a 0(cero)','modal fade modal-info');", true);
                        }
                        else
                        {
                            entidad.Pobservaciones = txtobsr.Text;
                            entidad.Pobservaciones_satisfecho = cadena;
                            entidad.Pidc_queja = total;
                            ds = com.DescartarRecordatorio(entidad, 1, 0, Convert.ToInt32(txtnum.Text), Convert.ToInt32(ddltipo.SelectedValue));
                            vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        }
                        break;
                }
                if (error == false)
                {
                    if (vmensaje == "")
                    {
                        Alert.ShowGiftMessage("Estamos procesando la solicitud", "Espere un Momento", "recordatorios_pendientes.aspx", "imagenes/loading.gif", "2000", "Recordatorio Procesado Correctamente", this);
                    }
                    else
                    {
                        Alert.ShowAlertError(vmensaje, this.Page);
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
            }
        }
    }
}