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
    public partial class programacion_correos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["id_correoprog"] = null;
                CargarProg();
            }
        }

        private void CargarProg()
        {
            try
            {
                ProgramacionCorreosENT entidad = new ProgramacionCorreosENT();
                ProgramacionCorreosCOM componente = new ProgramacionCorreosCOM();
                gridtareas.DataSource = componente.CargaProgn(entidad).Tables[0];
                gridtareas.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        protected void gridtareas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string id = gridtareas.DataKeys[index].Values["idc_progracorreo"].ToString();
            Session["Caso_Confirmacion"] = "Ir";
            Session["id_correoprog"] = funciones.deTextoa64(id);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Ver los Detalles de esta Programación de Correo?','modal fade modal-info');", true);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso = (string)Session["Caso_Confirmacion"];
            switch (caso)
            {
                case "Ir":
                    Response.Redirect("programacion_correos_d.aspx?idc_progracorreo=" + (string)Session["id_correoprog"]);
                    break;
            }
        }
    }
}