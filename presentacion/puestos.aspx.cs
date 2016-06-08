using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class puestos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargar_grid();
            }
        }

        protected void cargar_grid()
        {
            try
            {
                //componente
                PuestosCOM ComPuesto = new PuestosCOM();
                //ds
                DataSet ds = new DataSet();
                ds = ComPuesto.puestos();
                //llenar grid view
                grid_puestos.DataSource = ds.Tables[0];
                grid_puestos.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        protected void grid_puestos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                Button perfil = (Button)e.Row.FindControl("btnperfil");
                //
                if (rowView["idc_puestoperfil"] is DBNull)
                {
                    if (rowView["idc_aprobacion_soli"] is DBNull)
                    {
                        perfil.Text = "asignar";
                        perfil.CssClass = "btn btn-info";
                        perfil.CommandName = "modal_add";
                    }
                    else
                    {
                        perfil.Text = "aprobación en progreso";
                        perfil.CssClass = "btn btn-warning";
                    }
                }
                else
                {
                    if (rowView["idc_aprobacion_soli"] is DBNull)
                    {
                        perfil.Text = rowView["perfil"].ToString();
                        perfil.CssClass = "btn btn-success";
                        perfil.CommandName = "modal_add";
                    }
                    else
                    {
                        perfil.Text = "aprobación en progreso";
                        perfil.CssClass = "btn btn-warning";
                    }
                }
            }
        }

        protected void grid_puestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //panel_comentarios.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid_puestos.DataKeys[index].Value);

            switch (e.CommandName)
            {
                case "modal_add":
                    GridViewRow row = grid_puestos.Rows[index];
                    // Calculate the new price.
                    Label desc = (Label)row.FindControl("lbldesc");

                    modal_lblpuesto.Text = "Puesto: " + desc.Text;
                    cbox_puestos_perfil();
                    oc_idc_puesto.Value = vidc.ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Asignar Perfil','¿Desea aprobar este candidato?.');", true);
                    break;
            }
        }

        protected void cbox_puestos_perfil()
        {
            try
            {
                //componente
                PuestosCOM ComPuesto = new PuestosCOM();
                //ds
                DataSet ds = new DataSet();
                ds = ComPuesto.combo_puestos_perfil();
                //llenar cbox
                modal_cboxperfiles.DataSource = ds.Tables[0];
                modal_cboxperfiles.DataTextField = "descripcion";
                modal_cboxperfiles.DataValueField = "idc_puestoperfil";
                modal_cboxperfiles.DataBind();
                modal_cboxperfiles.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            //revisamos que haya seleccionado un perfil
            if (modal_cboxperfiles.SelectedIndex == 0)
            {
                Alert.ShowAlertError("Debe seleccionar un perfil", this.Page);
                return;
            }
            //continuamos
            //ingresamos los valores en la tabla temp
            int id_puesto = Convert.ToInt32(oc_idc_puesto.Value);
            //entidad
            PuestosENT EntPuesto = new PuestosENT();
            EntPuesto.Idc_Puesto = id_puesto;
            EntPuesto.Idc_puestoperfil = Convert.ToInt32(modal_cboxperfiles.SelectedValue);
            //componente
            PuestosCOM ComPuesto = new PuestosCOM();
            DataSet ds = new DataSet();
            ds = ComPuesto.puesto_perfil_temp_captura(EntPuesto);
            string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
            if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
            {
                //VAMOS POR LA APROBACION
                string mensaje = solicitudAprobacion(id_puesto);
                if (string.IsNullOrEmpty(mensaje)) // si esta vacio todo bien
                {
                    Alert.ShowAlert("Solicitud de aprobación correcta", "Mensaje", this.Page);
                }
                else
                {
                    Alert.ShowAlertError(mensaje, this.Page);
                }
            }
            else
            {
                Alert.ShowAlertError(vmensaje, this.Page);
            }

            cargar_grid();
        }

        protected string solicitudAprobacion(int id_row)
        {
            try
            {
                int idc_aprobacion_soli = 0;
                //ALERTA REVISAR ESTA SESISON
                //Session.Add("sidc_aprobacion_soli", idc_aprobacion_soli);
                //SI NO TIENE SOLICITUD SE CREA
                if (idc_aprobacion_soli == 0)
                { //proseguimos a insertar la solicitud
                    //nevesitamos mandar los sig datos
                    int idc_aprobacion = 3; //antes 11 que aprobacion queremos solicitar
                    int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString()); //que usuario hace la solicitud
                    int idc_registro = id_row; // el id del registro que se quiere aprobar EN ESTE CASO EL ID puesto

                    //llamamos la entidad
                    Aprobaciones_solicitudE entidad = new Aprobaciones_solicitudE();
                    entidad.Idc_aprobacion = idc_aprobacion;
                    entidad.Idc_usuario = idc_usuario;
                    entidad.Idc_registro = idc_registro;
                    entidad.Idc_aprobacion_soli = idc_aprobacion_soli; // SE MANDA CERO NO PASA NADA
                    //llamamos al componente
                    DataSet ds = new DataSet();
                    Aprobaciones_solicitudBL componente = new Aprobaciones_solicitudBL();
                    ds = componente.nueva_solicitud_aprobacion(entidad);
                    string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    int vfolio = Convert.ToInt32(ds.Tables[0].Rows[0]["folio"].ToString()); //id de la solicitud de aprobacion
                    //ALERTA REVISAR ESTA SESISON
                    //Session.Add("sidc_aprobacion_soli", vfolio);
                    string vmensaje_no2 = "";
                    if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                    {
                        //ahora sigue los adicionales
                        //debe mandarse el id del puesto para saber el jefe d ese puesto al que se le asignara el perfil
                        int id_row_puesto = id_row;
                        vmensaje_no2 = solicitudAprobacionAdicional(vfolio, id_row_puesto);
                        //todo bien en teoria aqui no se valida el mensaje vacio sino donde se llama.
                    }
                    if (vmensaje == "" & vmensaje_no2 == "")
                    {
                        //si los dos estan vacios pues manda vacio
                        return "";
                    }
                    else
                    {
                        return vmensaje + " " + vmensaje_no2;
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                //Alert.ShowAlertError(ex.Message,this.Page);
                return ex.Message;
            }
        }

        protected string solicitudAprobacionAdicional(int idc_aprobacion_soli, int id_row)
        {
            //nevesitamos mandar los sig datos

            try
            {
                //llamamos la entidad
                Aprobaciones_solicitudE entidad = new Aprobaciones_solicitudE();
                entidad.Idc_aprobacion_soli = idc_aprobacion_soli;
                entidad.Idc_registro = id_row;
                //llamamos al componente
                DataSet ds = new DataSet();
                Aprobaciones_solicitudBL componente = new Aprobaciones_solicitudBL();
                ds = componente.solicitud_aprobacion_adicional_ligarperfil(entidad);
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();

                //todo bien
                return vmensaje;
            }
            catch (Exception ex)
            {
                //Alert.ShowAlertError(ex.Message,this.Page);
                return ex.Message;
            }
        }
    }
}