using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class captura_actividades_agentes2_m : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                Session["clientes"] = null;
                lblusuario.Text = Session["xusuario"] as string;
                cargar_agentes_vs_usurios();
            }
        }

        private void cargar_agentes_vs_usurios()
        {
            try
            {
                AgentesENT entidad = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                DataSet ds = com.cargar_agentesusuarios(entidad);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    row["nombre3"] = row["idc_agente"].ToString() + " .- " + row["nombre"].ToString();
                }
                cboagente.DataSource = ds.Tables[0];
                cboagente.DataValueField = "idc_agente";
                cboagente.DataTextField = "nombre3";
                cboagente.DataBind();
                clientes.Visible = ds.Tables[0].Rows.Count > 1 ? true : false;
                if (ds.Tables[0].Rows.Count == 1)
                {
                    cargar_grid(Convert.ToInt32(ds.Tables[0].Rows[0]["idc_agente"]),true, 0);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }
        void cargar_grid(int idc_agente, bool aldia, int dia_int)
        {
            DateTime fechai = DateTime.Now;
            DateTime fechaf = DateTime.Now.AddDays(1);
            AgentesCOM com = new AgentesCOM();
            DataSet ds = com.clientes_visitas(idc_agente, fechai, fechaf, true, aldia, dia_int);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("dia");
                ds.Tables[0].Columns.Add("P");
                ds.Tables[0].Columns.Add("T");

                DataView dv = new DataView();
                dv = ds.Tables[0].DefaultView;

                int day = (int)fechai.DayOfWeek;
                string dia = fechai.DayOfWeek.ToString();
                string p = "";
                string t = "";
                switch (day)
                {
                    case 1:
                        dia = "L";
                        p = "P1";
                        t = "T1";
                        break;
                    case 2:
                        dia = "M";
                        p = "P2";
                        t = "T2";
                        break;
                    case 3:
                        dia = "MI";
                        p = "P3";
                        t = "T3";
                        break;
                    case 4:
                        dia = "J";
                        p = "P4";
                        t = "T4";
                        break;
                    case 5:
                        dia = "V";
                        p = "P5";
                        t = "T5";
                        break;
                    case 6:
                        dia = "S";
                        p = "P6";
                        t = "T6";
                        break;
                }

                for (int i = 0; i <= dv.ToTable().Rows.Count - 1; i++)
                {
                    dv[i]["dia"] = dv[i][dia];
                    dv[i]["P"] = dv[i][p];
                    dv[i]["T"] = dv[i][t];
                }
                Session["clientes"] = dv.ToTable();
                grdclientes.DataSource = dv;
                grdclientes.DataBind();
                grdclientes.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                grdclientes.DataSource = null;
                grdclientes.DataBind();
            }

        }
        protected void grdclientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "Page"))
            {
                if (e.CommandName == "Update")
                {
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    Session["xusuario"] = lblusuario.Text;
                    string val = grdclientes.DataKeys[row.RowIndex].Values["IDC_CLIENTE"].ToString().Trim();
                    Session["idc_cliente"] = val;
                    Session["num_grupo"] = (grdclientes.Rows[row.RowIndex].Cells[6].Text.Trim() == "&nbsp;" ? "0" : grdclientes.Rows[row.RowIndex].Cells[6].Text.Trim());
                    Session["idc_agente"] = cboagente.SelectedValue;
                    Response.Redirect("Ficha_cliente_m.aspx");
                }
            }

        }

        protected void grdclientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                CheckBox chkact =  e.Row.FindControl("chkact") as CheckBox;
                string tiempo = rowView["t"].ToString();
                string dia = rowView["dia"].ToString();
                int visito_hoy = Convert.ToInt32(rowView["visito_hoy"]);
                if (dia == "True" | dia == "False")
                {
                    chkact.Visible = visito_hoy > 0;
                    if (!string.IsNullOrEmpty(tiempo))
                    {
                        if (Convert.ToInt32(tiempo) == 0)
                        {
                            e.Row.Cells[10].Attributes["style"] = "background-color:White;";
                        }
                        else if (Convert.ToInt32(tiempo) >= 1 & Convert.ToInt32(tiempo) <= 5)
                        {
                            e.Row.Cells[10].Attributes["style"] = "background-color:rgb(243, 156, 75);";
                        }
                        else if (Convert.ToInt32(tiempo) >= 6 & Convert.ToInt32(tiempo) <= 14)
                        {
                            e.Row.Cells[10].Attributes["style"] = "background-color:rgb(0, 204, 255);";
                        }
                        else if (Convert.ToInt32(tiempo) > 15)
                        {
                            e.Row.Cells[10].Attributes["style"] = "background-color:rgb(133,181,32);";
                        }
                    }
                }
                else
                {
                    if ((chkact != null))
                    {
                        chkact.Visible = false;
                    }
                    e.Row.Cells[10].BackColor = System.Drawing.Color.White;
                }
                string ult_compra = rowView["ult_compra"].ToString();
                if (ult_compra == "&nbsp;" || ult_compra == "")
                {
                    e.Row.Cells[2].Attributes["style"] = "background-color:gainsboro;color:black;";
                }
                else
                {
                    if (Convert.ToInt32(ult_compra) >= 31 & Convert.ToInt32(ult_compra) <= 60)
                    {
                        e.Row.Cells[2].Attributes["style"] = "background-color:red;color:white;";
                    }
                    else if (Convert.ToInt32(ult_compra) > 60)
                    {
                        e.Row.Cells[2].Attributes["style"] = "background-color:yellow;color:black;";
                    }
                }
            }

        }

        protected void cbodias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboagente.Items.Count > 0)
            {
                bool igual = (cbodias.SelectedIndex == 0 | cbodias.SelectedIndex > 1 ? true : false);
                int dia_int = (cbodias.SelectedIndex < 1 ? 0 : cbodias.SelectedIndex - 1);
                cargar_grid(Convert.ToInt32(cboagente.SelectedValue), igual, dia_int);
            }

        }

        protected void cboagente_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool igual = (cbodias.SelectedIndex == 0 | cbodias.SelectedIndex > 1 ? true : false);
            int dia_int = (cbodias.SelectedIndex < 1 ? 0 : cbodias.SelectedIndex - 1);
            cargar_grid(Convert.ToInt32(cboagente.SelectedValue), igual, dia_int);
        }
    }


}