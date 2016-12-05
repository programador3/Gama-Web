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
    public partial class comidas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }


            if (!IsPostBack)
            {

                if (Request.QueryString["f"] == null)
                {
                    txtfecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtfecha.Text = Request.QueryString["f"].ToString();
                }


                CargarGrid_Empleados(Convert.ToDateTime(txtfecha.Text));

            }


        }
        private void CargarGrid_Empleados(DateTime FECHA)
        {
            try
            {
                comidasENT ent = new comidasENT();
                comidasCOM com = new comidasCOM();
                ent.Pfecha = FECHA;
                DataSet ds = com.CargarEmpleados(ent);
                DataColumn column = new DataColumn("mod", typeof(Int32));
                ds.Tables[0].Columns.Add(column);
                ViewState["EMPLEADOS" + txtfecha.Text] = ds.Tables[0];
                gridEmpleados.DataSource = ds.Tables[0];
                gridEmpleados.DataBind();

                DataView dv = ds.Tables[0].DefaultView;
                dv.RowFilter = "borrado = 0";
                NUM_CHK.Text = dv.ToTable().Rows.Count.ToString();
                //paginado(ds.Tables[0].DefaultView);
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }

        }



        protected void gridEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_empleado = Convert.ToInt32(gridEmpleados.DataKeys[index].Values["idc_empleado"]);
            
            int borrado = 1 - Convert.ToInt32(gridEmpleados.DataKeys[index].Values["BORRADO"]);
            string IMG = gridEmpleados.DataKeys[index].Values["img"].ToString().Trim();
            string IMG_new = IMG == "~/imagenes/btn/inchecked.png" ? "~/imagenes/btn/checked.png" : "~/imagenes/btn/inchecked.png";
            string NOM_SUCURSAL = gridEmpleados.DataKeys[index].Values["NOM_SUCURSAL"].ToString();
            DataTable dt = ViewState["EMPLEADOS" + txtfecha.Text] as DataTable;

            foreach (DataRow row in dt.Rows)
            {
                string idc = row["idc_empleado"].ToString();
                if (idc_empleado.ToString() == idc.Trim())
                {
                    /*dt.Rows.Remove(row);*/
                    row["BORRADO"] = borrado;
                    row["img"] = IMG_new;
                    row["mod"] = row["mod"] == DBNull.Value ? 1 : 1 + Convert.ToInt32(row["mod"].ToString());
                    break;
                }
            }
            ViewState["EMPLEADOS" + txtfecha.Text] = dt;
            DataView dv = dt.DefaultView;
            DataView dv2 = dt.DefaultView;
            dv.RowFilter = string.Format("Convert(num_nomina, 'System.String') like '%{0}%'  or EMPLEADO like '%{0}%'  or NOM_SUCURSAL like '%{0}%' ", H_F_BUSQUEDA.Value.Trim());

            gridEmpleados.DataSource = dt;
            gridEmpleados.DataBind();
            dv2.RowFilter = "borrado = 0";
            NUM_CHK.Text = dv2.ToTable().Rows.Count.ToString();
          //  ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "tabulador();", true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            DataTable dtprinipal = ViewState["EMPLEADOS" + txtfecha.Text] as DataTable;
            DataView dv = dtprinipal.DefaultView;
            dv.RowFilter = "idc_comidas > 0 and borrado = 1 and mod=1";
            DataTable dt_a = dv.ToTable();
            DataView dv2 = dtprinipal.DefaultView;
            dv2.RowFilter = "borrado = 0 and mod=1";// "idc_comidas = 0 and borrado = 0  ";
            DataTable dt_b = dv2.ToTable();
            gridRegistrado.DataSource = dt_a;
            gridRegistrado.DataBind();
            div_registrado.Visible = (dt_a.Rows.Count > 0);
            GridRegistrar.DataSource = dt_b;
            GridRegistrar.DataBind();
            div_registrar.Visible = (dt_b.Rows.Count > 0);
            H3_msg.Visible = !(div_registrar.Visible || div_registrado.Visible);
            //div_yes.Visible= !(H3_msg.Visible);
            yes.Visible = !(H3_msg.Visible);

            string str_Alert = string.Format("ModalMostrar('Mensaje del Sistema','Detalles de Registros');");
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", str_Alert, true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string str_Alert;
            str_Alert = string.Format("ModalConfirmar('Mensaje del Sistema','Al aceptar se perdera todos los cambios realizados. ¿Desea Continuar?');");

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", str_Alert, true);
        }

        protected void yes_Click(object sender, EventArgs e)
        {
            try
            {
                comidasENT ent = new comidasENT();
                Datos_Usuario_logENT dul = new Datos_Usuario_logENT();
                comidasCOM com = new comidasCOM();
                DataSet ds = new DataSet();
                string vmensaje = "";

                dul.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                dul.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                dul.Pusuariopc = funciones.GetUserName();//usuario pc
                dul.Pidc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                ent.Pfecha = Convert.ToDateTime(txtfecha.Text);

                /*tabla registrados*/
                DataTable dt = (DataTable)ViewState["EMPLEADOS" + txtfecha.Text];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "borrado = 0";
                ent.Pcadena_empleados = cadena(dv.ToTable());
                ent.Ptotal_cadena = dv.ToTable().Rows.Count;
                /*tabla registrar*/
                ds = com.GuardarRegistro(ent, dul);
                vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                if (vmensaje == "")
                {
                    Alert.ShowGiftMessage("Estamos procesando el registro.", "Espere un Momento", "comidas.aspx?f=" + txtfecha.Text, "imagenes/loading.gif", "2000", "Procesada Correctamente", this);
                }
                else
                {
                    Alert.ShowAlertError(vmensaje, this);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this);
            }
        }

        string cadena(DataTable dt)
        {
            string ret = "";
            foreach (DataRow row in dt.Rows)
            {
                string idc_empleado = row["idc_empleado"].ToString();
                ret = string.Format("{1};{0}", ret, idc_empleado);
            }
            return ret;
        }

        protected void gridEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                Boolean borrado = !Convert.ToBoolean(rowView["borrado"].ToString());
                int idc_com = Convert.ToInt32(rowView["idc_comidas"].ToString());

                e.Row.BackColor = borrado ? (idc_com > 0 ? ColorTranslator.FromHtml("#91d5fb")/*azul*/ : ColorTranslator.FromHtml("#99e9d9"))/*verde es nuevo*/  : ColorTranslator.FromHtml("#ffffff");

            }
        }

        protected void lnkCargar_Click(object sender, EventArgs e)
        {
            CargarGrid_Empleados(Convert.ToDateTime(txtfecha.Text));
            //Response.Redirect("comidas.aspx?f=" + txtfecha.Text);
        }

        private void filter(string str)
        {
            txtsearch.Text = "";
            H_F_BUSQUEDA.Value = "%";
            H_COL_FILTER.Value = str;
            DataTable dt = ViewState["EMPLEADOS" + txtfecha.Text] as DataTable;
            DataView dv = dt.DefaultView;
            dv.Sort = str;

            ViewState["EMPLEADOS" + txtfecha.Text] = dv.ToTable();
            gridEmpleados.DataSource = dv.ToTable();//dt;//ds.Tables[0];
            gridEmpleados.DataBind();
            dv.RowFilter = "borrado = 0";
            NUM_CHK.Text = dv.ToTable().Rows.Count.ToString();
            //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "tabulador();", true);
        }
        protected void lnk_filter_nom_Click(object sender, EventArgs e)
        {
            filter("num_nomina " + H_F_nom.Value);
            H_F_nom.Value = H_F_nom.Value != "DESC" ? "DESC" : "ASC";

        }

        protected void lnk_filter_emp_Click(object sender, EventArgs e)
        {
            filter("EMPLEADO " + H_F_emp.Value);
            H_F_emp.Value = H_F_emp.Value != "DESC" ? "DESC" : "ASC";

        }

        protected void lnl_filter_suc_Click(object sender, EventArgs e)
        {
            filter("NOM_SUCURSAL " + H_F_suc.Value + ", num_nomina ASC, EMPLEADO ASC");
            H_F_suc.Value = H_F_suc.Value != "DESC" ? "DESC" : "ASC";
        }

        protected void lnk_filter_chk_Click(object sender, EventArgs e)
        {
            filter("BORRADO " + H_F_chk.Value + ", num_nomina ASC, EMPLEADO ASC");
            H_F_chk.Value = H_F_chk.Value != "DESC" ? "DESC" : "ASC";

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            H_F_BUSQUEDA.Value = txtsearch.Text.Trim();
            DataTable dt = ViewState["EMPLEADOS" + txtfecha.Text] as DataTable;//0
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format("Convert(num_nomina, 'System.String') like '%{0}%'  or EMPLEADO like '%{0}%'  or NOM_SUCURSAL like '%{0}%' ", H_F_BUSQUEDA.Value.Trim());
            gridEmpleados.DataSource = dv.ToTable();//dt;//ds.Tables[0];
            gridEmpleados.DataBind();
            //ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "tabulador();", true);

        }
    }
}