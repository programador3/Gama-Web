using ClosedXML.Excel;
using iTextSharp.text;
using System;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using negocio.Componentes;
using negocio.Entidades;
using System.Drawing;
using System.Globalization;

namespace presentacion
{
    public partial class ticketserv_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                DataTable dt = Cargar_Grid(0).Tables[0];
                gridReporte.DataSource = dt;
                gridReporte.DataBind();
                NO_Hay.Visible = (dt.Rows.Count == 0);
                gridReporte.Visible = true;
            }
        }
        public DataSet Cargar_Grid(int Pidc_tareaser)
        {
            ticket_servCOM com = new ticket_servCOM();
            ticket_servENT ent = new ticket_servENT();            
            ent.Pidc_puesto =  Convert.ToInt32(Session["sidc_puesto_login"]);
            ent.Pidc_tareaser = Pidc_tareaser;
            DataSet ds = com.ticketserv_pendiente(ent);
            return ds;
        }

        protected void gridReporte_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            limpiar();
            int index = Convert.ToInt32(e.CommandArgument);
            string status = gridReporte.DataKeys[index].Values["status"].ToString();
            int idc_tareaser = Convert.ToInt32(gridReporte.DataKeys[index].Values["idc_tareaser"].ToString());
            int idc_ticketserv = Convert.ToInt32(gridReporte.DataKeys[index].Values["idc_ticketserv"].ToString());

            DataSet ds = Cargar_Grid(idc_tareaser);

            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "idc_ticketserv = " + idc_ticketserv;
            DataRow row = dv.ToTable().Rows[0];

            lblDescr.Text = row["descripcion"].ToString();
            lblObser.Text = row["OBSERVACIONES_TICKET"].ToString();
            lblEmple_rep.Text = row["EMPLEADO_REP"].ToString();
            lblDepto_rep.Text = row["DEPTO_REP"].ToString();
            lblFecha_rep.Text = row["FECHA_REP_TEXT"].ToString();
             
            string msgStatus = "";
            string clase = "";

            switch (status)
            {
                case "A":
                    lblEmple_aten.Text = row["EMPLEADO_ATEN"].ToString();//EMPLEADO_REP
                    lblDepto_aten.Text = row["DEPTO_ATEN"].ToString(); //DEPTO_REP
                    lblFecha_aten.Text = row["FECHA_ATEN_TEXT"].ToString();//FECHA_ATEN_TEXT
                    H_idc_puesto_aten.Value= row["idc_puesto_aten"].ToString();
                    div_aten.Visible = true;
                    
                    msgStatus = "El Ticket esta siendo Atendido.";
                    clase = "panel-warning";
                    break;

                case "E":
                    msgStatus = "El Ticket esta en Espera.";
                    clase = "panel-info";
                    break;
            }
            
            string str_Alert;
            str_Alert = string.Format("ModalMostrar('Mensaje del Sistema','Detalles del Ticket {0}','{1}',{2},{3},'{4}');",
            lblDescr.Text,
                msgStatus, 
                row["TIEMPO_ESTIMADO"].ToString(),
                row["TIEMPO_RESPUESTA"].ToString(), clase);

            gridEmpleados.DataSource = ds.Tables[1];
            gridEmpleados.DataBind();

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", str_Alert, true);

        }

        private void limpiar()
        {
            H_idc_puesto_aten.Value = "";
            lblDescr.Text = "";
            lblObser.Text = "";
            lblEmple_rep.Text = "";
            lblDepto_rep.Text = "";
            lblFecha_rep.Text = "";
            /*/\*/
            lblEmple_aten.Text = "";
            lblDepto_aten.Text = "";
            lblFecha_aten.Text = "";
            /**/
            lblEmple_aten.Text = "";
            lblDepto_aten.Text = "";
            lblFecha_aten.Text = "";
            
            div_aten.Visible = false;
            
           
            
        }

         

        protected void gridEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // H_idc_puesto_aten
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                
                int idc_puesto = Convert.ToInt32(rowView["idc_puesto"].ToString());

                e.Row.BackColor = idc_puesto.ToString() == H_idc_puesto_aten.Value ? ColorTranslator.FromHtml("#91d5fb")/*azul*/ :  ColorTranslator.FromHtml("#ffffff");

            }
        }
    }
}
