﻿using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class catalogo_documentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            CargaGrid();
        }

        /// <summary>
        /// Carga Grid Pirncipal
        /// </summary>
        public void CargaGrid()
        {
            Catalogo_DocumentosENT entidad = new Catalogo_DocumentosENT();
            Catalogo_DocumentosCOM componente = new Catalogo_DocumentosCOM();
            entidad.Pidc_tipodoc = 0;
            DataSet ds = componente.CargaDocumentos(entidad);
            gridAvisos.DataSource = ds.Tables[0];
            gridAvisos.DataBind();
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            String Confirma_a = (string)Session["Caso_Confirmacion"];
            string idc_tipodoc = Session["idc_tipodoc"].ToString();
            switch (Confirma_a)
            {
                case "Editar":
                    Response.Redirect("catalogo_documentos_captura.aspx?idc_tipodoc=" + idc_tipodoc);
                    break;

                case "Eliminar":
                    Catalogo_DocumentosENT entidad = new Catalogo_DocumentosENT();
                    Catalogo_DocumentosCOM componente = new Catalogo_DocumentosCOM();
                    entidad.Pidc_tipodoc = Convert.ToInt32(idc_tipodoc);
                    DataSet ds = componente.EliminaDocumento(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('Documento Eliminado Correctamente.','catalogo_documentos.aspx');", true);
                    }
                    else
                    {
                        Alert.ShowAlertError(mensaje, this);
                    }
                    break;
            }
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
        }

        protected void lnkNuevoAviso_Click(object sender, EventArgs e)
        {
            Response.Redirect("catalogo_documentos_captura.aspx");
        }

        protected void gridAvisos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_tipodoc"] = Convert.ToInt32(gridAvisos.DataKeys[index].Values["idc_tipodoc"].ToString());

            switch (e.CommandName)
            {
                case "Editar":
                    Session["Caso_Confirmacion"] = "Editar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Editar este Documento?');", true);
                    break;

                case "Eliminar":
                    Session["Caso_Confirmacion"] = "Eliminar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta seguro de Eliminar este Documento?');", true);
                    break;
            }
        }
    }
}