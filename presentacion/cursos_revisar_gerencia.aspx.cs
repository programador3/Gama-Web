using negocio.Componentes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class cursos_revisar_gerencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                string llave = "";
                //recibimos mediante url si mostrar los empleados o pre empleados
                if (Request.QueryString["uempleado"] != null)// si el request viene vacio iniciamos en borrador
                {
                    //mostramos los datos
                    bool vempleado = Convert.ToBoolean(Request.QueryString["uempleado"].ToString());
                    oc_empleado.Value = vempleado.ToString();
                    if (vempleado)
                    { //es empleado
                        llave = "idc_empleado";
                    }
                    else
                    {
                        //es pre empleado
                        llave = "idc_pre_empleado";
                    }
                    grid_cursos_revisar_gerencia.DataKeyNames = new string[] { llave.ToString() };
                    cargar_info(idc_usuario, vempleado);
                    btnempleado.CssClass = (vempleado) ? "btn btn-success" : "btn btn-default";
                    btnpreempleado.CssClass = (vempleado == false) ? "btn btn-success" : "btn btn-default";
                }
                else
                {
                    btnempleado.CssClass = "btn btn-default";
                    btnpreempleado.CssClass = "btn btn-default";
                }
            }
        }

        protected void cargar_info(int pidc_usuario, bool pempleado)
        {
            try
            {
                //componente
                Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
                //ds
                DataSet ds = new DataSet();
                ds = ComCursoHist.cursos_revisar_gerencia(pidc_usuario, pempleado);
                //llenar en session
                Session.Add("TablaCursoRevGer", ds.Tables[0]);
                //llenar grid view
                if (ds.Tables[0].Rows.Count == 0)
                {
                    panel_mensaje.Visible = true;
                }
                grid_cursos_revisar_gerencia.DataSource = ds.Tables[0];
                grid_cursos_revisar_gerencia.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        protected void grid_cursos_revisar_gerencia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //panel_comentarios.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid_cursos_revisar_gerencia.DataKeys[index].Value);

            switch (e.CommandName)
            {
                case "ver_detalle":
                    //llenar gridviews

                    Response.Redirect("cursos_revisar_gerencia_detalle.aspx?uidc=" + vidc + "&tipoemp=" + oc_empleado.Value);

                    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Mensaje del Sistema','¿Desea aprobar este candidato?.');", true);
                    break;
            }
        }

        protected void grid_cursos_revisar_gerencia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                Label persona = (Label)e.Row.FindControl("lblpersona");
                //validamos segun si estamos en empleado o pre empleado
                bool vempleado = Convert.ToBoolean(oc_empleado.Value);
                if (vempleado)
                { // es empleado
                    persona.Text = rowView["empleado"].ToString();
                }
                else
                {
                    //es pre empleado (candidato)
                    persona.Text = rowView["candidato"].ToString();
                }
            }
        }

        protected void btnempleado_Click(object sender, EventArgs e)
        {
            Response.Redirect("cursos_revisar_gerencia.aspx?uempleado=True", true);
        }

        protected void btnpreempleado_Click(object sender, EventArgs e)
        {
            Response.Redirect("cursos_revisar_gerencia.aspx?uempleado=False", true);
        }
    }
}