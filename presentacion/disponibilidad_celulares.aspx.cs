using ClosedXML.Excel;
using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class disponibilidad_celulares : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Carga();
            }
        }

        private void Carga()
        {
            Celulares_RevisionesENT entidad = new Celulares_RevisionesENT();
            Celulares_RevisionesCOM com = new Celulares_RevisionesCOM();
            gridcelulares.DataSource = com.CelualresDispo(entidad).Tables[0];
            gridcelulares.DataBind();
        }

        protected void gridcelulares_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string color = rowView["css_class"].ToString();
                e.Row.BackColor = Color.FromName(color);
            }
        }
    }
}