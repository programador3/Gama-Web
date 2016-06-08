using negocio.Componentes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class cursos_historial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //recibimos mediante url si mostrar los empleados o pre empleados
                if (Request.QueryString["uempleado"] != null)// si el request viene vacio iniciamos en borrador
                {
                    //mostramos los datos
                    bool vempleado = Convert.ToBoolean(Request.QueryString["uempleado"].ToString());
                    oc_empleado.Value = vempleado.ToString();
                    cargar_info(vempleado);

                    btnempleado.CssClass = (vempleado) ? "btn btn-success" : "btn btn-default";
                    btnpreempleado.CssClass = (vempleado == false) ? "btn btn-success" : "btn btn-default";
                }
                else
                {
                    btnempleado.CssClass = "btn btn-default";
                    btnpreempleado.CssClass = "btn btn-default";
                }
            }
            else
            { //botones postback
            }
        }

        protected void cargar_info(bool pempleado)
        {
            try
            {
                //componente
                Cursos_HistorialCOM ComCursoHist = new Cursos_HistorialCOM();
                //ds
                DataSet ds = new DataSet();
                ds = ComCursoHist.cursos_historial(pempleado);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    panel_mensaje.Visible = true;
                }
                //llenar en session
                Session.Add("TablaCursoHistorial", ds.Tables[0]);
                //llenar grid view

                grid_cursos_historial.DataSource = ds.Tables[0];
                grid_cursos_historial.DataBind();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        protected void btnempleado_Click(object sender, EventArgs e)
        {
            Response.Redirect("cursos_historial.aspx?uempleado=True", true);
        }

        protected void btnpreempleado_Click(object sender, EventArgs e)
        {
            Response.Redirect("cursos_historial.aspx?uempleado=False", true);
        }

        protected void grid_cursos_historial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                Label persona = (Label)e.Row.FindControl("lblpersona");
                Label aprob_capacitacion = (Label)e.Row.FindControl("lblaprob_capacitacion");
                Label aprob_gerencia = (Label)e.Row.FindControl("lblaprob_gerencia");
                Label estatus = (Label)e.Row.FindControl("lblestatus");
                //validamos el valor de las aprobaciones
                if (rowView["aprobado_capacitacion"] is DBNull)
                {
                    aprob_capacitacion.Text = "Sin Revisar";
                }
                else
                {
                    aprob_capacitacion.Text = (Convert.ToBoolean(rowView["aprobado_capacitacion"])) ? "Si" : "No";
                }

                if (rowView["aprobado_gerencia"] is DBNull)
                {
                    aprob_gerencia.Text = "Sin Revisar";
                }
                else
                {
                    aprob_gerencia.Text = (Convert.ToBoolean(rowView["aprobado_gerencia"])) ? "Si" : "No";
                }
                //estatus
                switch (rowView["estatus"].ToString())
                {
                    case "T":
                        estatus.Text = "Terminado";
                        break;

                    case "P":
                        estatus.Text = "Pendiente";
                        break;

                    case "C":
                        estatus.Text = "Cancelado";
                        break;

                    default:
                        estatus.Text = "Error";
                        break;
                }
                //validamos segun si estamos en empleado o pre empleado
                bool vempleado = Convert.ToBoolean(oc_empleado.Value);
                if (vempleado)
                { // es empleado
                    persona.Text = rowView["empleado"].ToString();
                }
                else
                {
                    //es pre empleado (candidato)
                    persona.Text = rowView["pre_empleado"].ToString();
                }
            }
        }

        protected void btnprogramarcurso_Click(object sender, EventArgs e)
        {
            Response.Redirect("cursos_historial_captura.aspx");
        }

        protected void grid_cursos_historial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //panel_comentarios.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid_cursos_historial.DataKeys[index].Value);
            bool empleado = Convert.ToBoolean(oc_empleado.Value);
            // oc_modal_idc_curso_historial.Value = vidc.ToString();
            switch (e.CommandName)
            {
                case "editregistro":
                    Response.Redirect("cursos_historial_captura.aspx?uidc_curso_historial=" + vidc + "&uemp=" + empleado);
                    break;
            }
        }
    }
}