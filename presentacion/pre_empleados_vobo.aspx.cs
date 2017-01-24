using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class pre_empleados_vobo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Cargaro();
            }
        }
        private void Cargaro()
        {
            try
            {
                CandidatosCOM componente = new CandidatosCOM();
                DataSet ds = componente.sp_pre_empleado_falta_vbno();
                gridCatalogo.DataSource = ds.Tables[0];
                gridCatalogo.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(),this);
            }
        }

        protected void gridCatalogo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER EMDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            Session["idc_prepara"] = Convert.ToInt32(gridCatalogo.DataKeys[index].Values["idc_prepara"].ToString());
            int idc_pre = Convert.ToInt32(gridCatalogo.DataKeys[index].Values["idc_pre_empleado"].ToString());
            string CORREO_PERSONAL = gridCatalogo.DataKeys[index].Values["CORREO_PERSONAL"].ToString();
            bool aplica_enviar_correo = Convert.ToBoolean(gridCatalogo.DataKeys[index].Values["SE_ENVIARA_CORREO"]);
            string nombre = gridCatalogo.DataKeys[index].Values["nombre"].ToString();
            Session["idc_pre_empleado"] = idc_pre.ToString();
            cambiar_fecha.Visible = false;

            switch (e.CommandName)
            {
                case "OK":
                    Session["Caso_Confirmacion"] = "vobo";
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ModalConfirm('Mensaje del Sistema','¿Desea dar el Visto Bueno al Candidato "+ nombre + "? \\nEsto terminara el proceso de preparación y podra dar de alta al candidato como Empleado.');", true);
                    break;
                case "RegenerarGUID":

                    txtmotivo.Text = "";
                    cambiar_fecha.Visible = true;
                    if (aplica_enviar_correo)
                    {
                        lbltextocorreo.Text = "Se enviara un correo al candidato, con un link al sistema de RECLUTAMIENTO, para que este complete su informacion y documentos. SE RECOMIENDA REALIZAR UNA LLAMADA DE AVISO AL CANDIDATO.";
                        
                        txtcorreo.Text = CORREO_PERSONAL;
                        txtcorreo.ReadOnly = CORREO_PERSONAL != "";
                        ViewState["correo_a_cand"] = true;
                    }
                    else
                    {
                        ViewState["correo_a_cand"] = false;
                        string query = "select top 1 ltrim(rtrim(correo)) as correo,a.idc_correo from usuarios with(nolock) inner join correos_gama as a on a.idc_correo = usuarios.idc_correo where a.borrado = 0 and usuarios.idc_usuario = " + Convert.ToInt32(Session["sidc_usuario"]).ToString().Trim() + "";
                        DataTable dt_correos_reclu = funciones.ExecQuery(query);
                        lbltextocorreo.Text = "Se enviara un link a su correo externo, para que USTED complete la informacion y documentos del candidato.";
                        txtcorreo.ReadOnly = Convert.ToInt32(dt_correos_reclu.Rows[0]["idc_correo"]) > 0 ? true : false;
                        txtcorreo.Text = Convert.ToInt32(dt_correos_reclu.Rows[0]["idc_correo"]) > 0 ? dt_correos_reclu.Rows[0]["correo"].ToString().Trim() : "";
                    }

                    Session["Caso_Confirmacion"] = "RegenerarGUID";
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ModalConfirm('Mensaje del Sistema','¿Desea Regenerar el Link a este candidato.?');", true);
                    break;
                case "Puesto":
                    int idc_prepara = Convert.ToInt32(gridCatalogo.DataKeys[index].Values["idc_prepara"].ToString());
                    int idc_pre_empleado = Convert.ToInt32(gridCatalogo.DataKeys[index].Values["idc_pre_empleado"].ToString());
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                        "window.open('pre_empleados_info.aspx?idc_pre_empleado=" + funciones.deTextoa64(idc_pre_empleado.ToString().Trim()) + "&idc_prepara=" + funciones.deTextoa64(idc_prepara.ToString().Trim()) + "');", true);
                    break;
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string Confirma_a = (string)Session["Caso_Confirmacion"];
                CandidatosENT entidad = new CandidatosENT();
                CandidatosCOM componente = new CandidatosCOM();
                DataTable tabla_archivos = (DataTable)Session["tabla_archivos"];
                entidad.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                int idc_pre_empleado = Convert.ToInt32(Session["idc_pre_empleado"]);
                int idc_prepara = Convert.ToInt32(Session["idc_prepara"]);
                switch (Confirma_a)
                {
                    case "RegenerarGUID":
                        string correo =  "";
                        string correorh = "";
                        if (Convert.ToBoolean(ViewState["correo_a_cand"]) == false)
                        {
                            correorh = txtcorreo.Text.Trim();
                        }
                        else
                        {
                            correo = txtcorreo.Text.Trim();
                        }

                        string motivo = txtmotivo.Text;
                        //verificamos que no existan errores
                        string mensaje333 = "";
                        if (motivo == "")
                        {
                            mensaje333 = "Escriba el motivo";
                        }
                        else if (correo == "")
                        {
                            mensaje333 = "Escriba un correo para enviar el link";
                        }
                        else
                        {
                            DataSet ds_comp = componente.sp_REGENERAR_GUID(idc_prepara, Convert.ToInt32(Session["sidc_usuario"]), idc_pre_empleado, motivo, correo, correorh);
                            DataRow row333 = ds_comp.Tables[0].Rows[0];
                            mensaje333 = row333["mensaje"].ToString();
                        }
                       
                        if (mensaje333 != "")
                        {
                            Alert.ShowAlertError(mensaje333, this);
                        }
                        else
                        {//mostramos error
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('El link fue REGENERADO de manera correcta.','" + HttpContext.Current.Request.Url.AbsoluteUri + "','success');", true);
                        }
                        break;

                    case "vobo":
                        DataSet ds = componente.SP_PRE_EMPLEADO_VOBO(idc_prepara,Convert.ToInt32(Session["sidc_usuario"]), idc_pre_empleado);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString(); ;
                        if (vmensaje != "")
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                        else
                        {//mostramos error
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('Los datos del Candidato fueron aceptados. \\nYa puede dar de alta al Empleado desde el Sistema GAMA.','" + HttpContext.Current.Request.Url.AbsoluteUri + "','success');", true);
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

    }
}