using ClosedXML.Excel;
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
    public partial class catalogo_procesos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["idc_proceso"] = null;
                CargarProcesos("B");
            }
        }

        private void CargarProcesos(string tipo)
        {
            try
            {
                ProcesosENT enti = new ProcesosENT();
                enti.Ptioo = tipo;
                enti.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                ProcesosCOM com = new ProcesosCOM();
                DataSet ds = tipo == "B" ? com.CatalogoProcesosbORR(enti) : com.CatalogoProcesos(enti);
                gridprocesos.DataSource = ds.Tables[0];
                gridprocesos.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void gridprocesos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string idc_proceso = gridprocesos.DataKeys[index].Values["idc_proceso"].ToString();
            string descripcion = gridprocesos.DataKeys[index].Values["descripcion"].ToString();
            string tipo = lnkborrador.Visible == true ? "P" : "B";
            switch (e.CommandName)
            {
                case "Subprocesos":
                case "Editar":
                    Response.Redirect("subprocesos_captura.aspx?type=" + tipo + "&idc_proceso=" + funciones.deTextoa64(idc_proceso));
                    break;

                case "Eliminar":
                    Session["idc_proceso"] = idc_proceso;
                    Session["Caso_Confirmacion"] = "Eliminar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Eliminar elManual de Procesos "+ descripcion + "','modal fade modal-info');", true);
                    break;
            }
        }

        protected void lnkborrador_Click(object sender, EventArgs e)
        {
            lnkproduccion.Visible = true;
            lnkborrador.Visible = false;
            lnltipo.CssClass = "btn btn-primary";
            lnltipo.Text = "Tipo Borrador";
            CargarProcesos("B");
        }

        protected void lnkproduccion_Click(object sender, EventArgs e)
        {
            lnkproduccion.Visible = false;
            lnkborrador.Visible = true;
            lnltipo.CssClass = "btn btn-success";
            lnltipo.Text = "Tipo Produccion";
            CargarProcesos("P");
        }

        protected void lbknuevoprocesos_Click(object sender, EventArgs e)
        {
            string tipo = lnkborrador.Visible == true ? "P" : "B";
            Response.Redirect("subprocesos_captura.aspx?type=" + tipo + "&idc_proceso=" + funciones.deTextoa64(0.ToString()));
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string value = (string)Session["Caso_Confirmacion"];
            try
            {
                ProcesosENT entidad = new ProcesosENT();
                ProcesosCOM com = new ProcesosCOM();
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pidc_proceso = Convert.ToInt32(Session["idc_proceso"]);
                entidad.Pborrador = lnkproduccion.Visible;
                DataSet ds = new DataSet();
                string vmensaje = "";
                switch (value)
                {
                    case "Eliminar":
                        ds = com.EliminarProcesos(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;
                }
                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos procesando la SOLICITUD.", "Espere un Momento", "catalogo_procesos.aspx", "imagenes/loading.gif", "2000", "Proceso eliminado correctamente ", this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
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