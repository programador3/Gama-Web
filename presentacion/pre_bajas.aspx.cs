using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class solicitud_prebaja : System.Web.UI.Page
    {
        private int idc_usuario = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            //si viene de una pagina externa
            if (!Page.IsPostBack && Request.QueryString["idc_empleado"] != null)//Si variable request es nulla
            {
                txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
                int idc_empleado = Convert.ToInt32(Request.QueryString["idc_empleado"]);
                PreBajaDirecta(idc_empleado);
            }
            if (!Page.IsPostBack && Request.QueryString["idc_empleado"] == null)
            {
                //iniciamos valores del switch
                Session["apto_reingreso"] = 1;
                Session["contratar"] = 1;
                txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
            idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());
            GenerarDatos(idc_usuario);
        }

        /// <summary>
        /// Se genera cuando la solicitud de prebaja viene de otra pagina
        /// </summary>
        public void PreBajaDirecta(int idc_empleado)
        {
            gridEmpleados.Visible = false;
            PanelPreBaja.Visible = true;
            Noempleados.Visible = false;
            row_grid.Visible = false;
            PanelPreBaja.Visible = true;
            //sacamps datos del empleado en prebaja
            PuestosENT entidad = new PuestosENT();
            entidad.Idc_Puesto = 0;//INDICAMOS QUE NO QUEREMOS DATOS DE EMPLEADO
            PuestosCOM componente = new PuestosCOM();
            DataSet ds = componente.CargaCatologoPuestos(entidad);
            //buscamos los datos
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(row["idc_empleado"]) == idc_empleado)
                {
                    Session["idc_puesto"] = row["idc_puesto"];
                    Session["idc_empleado"] = row["idc_empleado"];
                    lblEmpleadoName.Text = row["nombre"].ToString();
                    lblPuesto.Text = row["descripcion"].ToString();
                }
            }
            if (Convert.ToInt32(Session["edit_prebaja"]) == 1)
            {
                cbxHonesto.Checked = Convert.ToBoolean(Session["honesto"]);
                cbxDrogas.Checked = Convert.ToBoolean(Session["drogas"]);
                cbxTrabajador.Checked = Convert.ToBoolean(Session["trabajador"]);
                cbxAlcol.Checked = Convert.ToBoolean(Session["alcohol"]);
                cbxCartaRec.Checked = Convert.ToBoolean(Session["carta_recomendacion"]);
                ddlVacante.SelectedValue = Convert.ToBoolean(Session["contratar"]) == false ? "0" : "1";
                cbxApto.Checked = Convert.ToBoolean(Session["apto_reingreso"]);
                txtEspecificar.Text = (string)Session["especificar"];
                txtMotivo.Text = (string)Session["motivo"];
                DateTime fecha = (DateTime)Session["fecha_baja"];
                txtFecha.Text = fecha.ToString("yyyy-MM-dd");
            }
        }

        public void GenerarDatos(int idc_usuario)
        {
            Solicitud_PrebajaENT entidad = new Solicitud_PrebajaENT();
            Solicitud_PrebajaCOM componente = new Solicitud_PrebajaCOM();
            entidad.Pidc_usuario = idc_usuario;
            DataSet ds = componente.CargaEmpleados(entidad);
            DataTable table = ds.Tables[0];
            foreach (DataRow row in table.Rows)
            {
                if (Convert.ToInt32(row["idc_empleado"].ToString()) == 0)
                {
                    row.Delete();
                }
            }
            gridEmpleados.DataSource = table;
            gridEmpleados.DataBind();
            if (ds.Tables[0].Rows.Count == 0) { Noempleados.Visible = true; }
        }

        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            if (Session["Previus"] != null)
            {
                String PreviousPage = (String)Session["Previus"];
                Response.Redirect(PreviousPage);
            }
            if (Session["Previus"] == null)
            {
                Response.Redirect("pre_bajas.aspx");
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string Confirma_a = (string)Session["Caso_Confirmacion"];
            switch (Confirma_a)
            {
                case "Guardar":
                    string strHostName = Dns.GetHostName();
                    IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                    Solicitud_PrebajaENT entidad = new Solicitud_PrebajaENT();
                    Solicitud_PrebajaCOM componente = new Solicitud_PrebajaCOM();
                    entidad.Pidc_usuario = idc_usuario;//USUARIO QUE REALIZA LA PREBAJA
                    entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                    entidad.Idc_empleado = Convert.ToInt32(Session["idc_empleado"]);//ID DE EMPLEADO EN PREBAJA
                    entidad.Idc_puesto = Convert.ToInt32(Session["idc_puesto"]);//ID DE puesto EN PREBAJA
                    entidad.Prenuncia = ddlTipoBaja.SelectedValue == "0" ? false : true;
                    entidad.Pidc_Prebaja = (Session["idc_prebaja"]) == null ? 0 : Convert.ToInt32(Session["idc_prebaja"]);
                    int apto = 1;
                    if (cbxApto.Checked == false) { apto = 0; }
                    entidad.Apto_reingreso = apto; //apto reingreso
                    entidad.Contratar = Convert.ToInt32(ddlVacante.SelectedValue);
                    //honesto
                    int Honesto = 0;
                    if (cbxHonesto.Checked == true) { Honesto = 1; }
                    entidad.Honesto = Honesto;
                    //alcohol
                    int Alcholo = 0;
                    if (cbxAlcol.Checked == true) { Alcholo = 1; }
                    entidad.Alcohol = Alcholo;
                    //drogas
                    int drogas = 0;
                    if (cbxDrogas.Checked == true) { drogas = 1; }
                    entidad.Drogas = drogas;
                    //robo
                    int Robo = 0;
                    if (cbxRobo.Checked == true) { Robo = 1; }
                    entidad.Robo = Robo;
                    //robo
                    int Trabajador = 0;
                    if (cbxTrabajador.Checked == true) { Trabajador = 1; }
                    entidad.Trabajador = Trabajador;
                    //CARTA REC
                    int Carta_recomendacion = 0;
                    if (cbxCartaRec.Checked == true) { Carta_recomendacion = 1; }
                    entidad.Carta_recomendacion = Carta_recomendacion;
                    entidad.Motivo = txtMotivo.Text.ToUpper();//motivo
                    entidad.Especificar = txtEspecificar.Text.ToUpper();
                    SqlDateTime fecha = Convert.ToDateTime(txtFecha.Text.ToString());
                    entidad.Fecha = fecha;
                    DataSet ds = componente.InsertarPrebaja(entidad);
                    DataRow row = ds.Tables[0].Rows[0];
                    //verificamos que no existan errores
                    string mensaje = row["mensaje"].ToString();
                    if (mensaje == "")//no hay errores retornamos true
                    {
                        if (Request.QueryString["idc_empleado"] != null && Convert.ToInt32(Session["edit_prebaja"]) != 1)//Si viene de otrapagina
                        {
                            String PreviousPage = (String)Session["Previus"];//tomamos pagina anterios
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La Pre-Baja fue solicitada correctamente.','" + PreviousPage + "','success');", true);
                        }
                        if (Request.QueryString["idc_empleado"] != null && Convert.ToInt32(Session["edit_prebaja"]) == 1)//Si viene de otrapagina
                        {
                            Session["edit_prebaja"] = "";
                            ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La Pre-Baja fue Editada correctamente.','pre_bajas_bajas.aspx','success');", true);
                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "GOALERT", "AlertGO('La Pre-Baja fue solicitada correctamente.','pre_bajas.aspx','success');", true);
                    }
                    else
                    {//mostramos error
                        Alert.ShowAlertError(mensaje, this);
                    }
                    break;
            }
        }

        protected void btnACeptarPrebaja_Click(object sender, EventArgs e)
        {
            bool error = false;
            DateTime fecha_menor = DateTime.Today.AddDays(-15);
            DateTime dt1 = new DateTime();
            dt1 = Convert.ToDateTime(txtFecha.Text);
            if (dt1 < fecha_menor)
            {
                Alert.ShowAlertError("La fecha de Baja no puede ser menor a 15 dias a partir de la solicitud.", this);
                txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
                error = true;
            }
            if (ddlTipoBaja.SelectedValue == "SIN")
            {
                Alert.ShowAlertError("Seleccione Renuncia o Baja.", this);
                error = true;
            }
            if (txtFecha.Text == "" | txtFecha.Text.Equals(string.Empty))
            {
                error = true;
                Alert.ShowAlertError("Debe Seleccionar la Fecha de Pre-Baja.", this);
            }
            if (txtMotivo.Text == "" | txtMotivo.Text.Equals(string.Empty))
            {
                error = true;

                Alert.ShowAlertError("Debe colocar un motivo por la Pre-Baja.", this);
            }
            if (!error == true)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Esta a punto de guardar la Pre-Baja. Si esto sucede no podra ser modificada. Todos sus datos son correctos?');", true);
            }
        }

        protected void lnkVacanteNO_Click(object sender, EventArgs e)
        {
            //declaramos al lbl 0 --NO CONTRATAR
            lnkVacanteC.CssClass = "btn btn-link";
            lnkVacanteNO.CssClass = "btn btn-primary active";
            Session["contratar"] = 0;
        }

        protected void lnkVacanteC_Click(object sender, EventArgs e)
        {
            //declaramos al lbl 1 --CONTRATAR
            lnkVacanteNO.CssClass = "btn btn-link";
            lnkVacanteC.CssClass = "btn btn-primary active";
            Session["contratar"] = 1;
        }

        protected void gridEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //TOMO EL COMANDO Y DEFINO QUE HACER MEDIANTE SWITCH
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_empleado = Convert.ToInt32(gridEmpleados.DataKeys[index].Values["idc_empleado"].ToString());
            int idc_puesto = Convert.ToInt32(gridEmpleados.DataKeys[index].Values["idc_puesto"].ToString());
            Session["idc_puesto"] = idc_puesto;
            Session["idc_empleado"] = idc_empleado;
            lblEmpleadoName.Text = gridEmpleados.DataKeys[index].Values["empleado"].ToString();
            lblPuesto.Text = gridEmpleados.DataKeys[index].Values["descripcion"].ToString();
            int idc_puestologin = Convert.ToInt32(Session["sidc_puesto_login"].ToString());
            //no se puede dar de baja uno mismo
            if (idc_puestologin == idc_puesto)
            {
                Alert.ShowAlertError("Usted NO puede darse de baja", this);
            }
            else
            {
                Noempleados.Visible = false;
                row_grid.Visible = false;
                PanelPreBaja.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["idc_empleado"] != null)//Si viene de otrapagina
            {
                String PreviousPage = (String)Session["Previus"];//tomamos pagina anterios
                Response.Redirect(PreviousPage);
            }
            Response.Redirect("pre_bajas.aspx");
        }

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            if (txtFecha.Text != "" && !txtFecha.Text.Equals(string.Empty))
            {
                DateTime fecha_menor = DateTime.Today.AddDays(-15);
                DateTime dt1 = new DateTime();
                dt1 = Convert.ToDateTime(txtFecha.Text);
                if (dt1 < fecha_menor)
                {
                    Alert.ShowAlertError("La fecha de Baja no puede ser menor a 15 dias a partir del registro.", this);
                    txtFecha.Text = "";
                }
            }
        }

        protected void gridEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable tabla_prebajas = (DataTable)Session["Tabla_PuestosPrebaja"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                int idc_prebaja = Convert.ToInt32(rowView["idc_prebaja"]);
                if (idc_prebaja != 0)//
                {
                    e.Row.Cells[0].Controls.Clear(); //si no esta en este estado limpio ese control
                }
            }
        }

        protected void cbxApto_CheckedChanged(object sender, EventArgs e)
        {
            //cbxAlcol.Enabled = true;
            //cbxTrabajador.Enabled = true;
            //cbxDrogas.Enabled = true;
            //cbxHonesto.Enabled = true;
            //cbxRobo.Enabled = true;
            //if (cbxApto.Checked == true)
            //{
            //    cbxAlcol.Checked = false; cbxAlcol.Enabled = false;
            //    cbxTrabajador.Checked = false; cbxTrabajador.Enabled = false;
            //    cbxDrogas.Checked = false; cbxDrogas.Enabled = false;
            //    cbxHonesto.Checked = false; cbxHonesto.Enabled = false;
            //    cbxRobo.Checked = false; cbxRobo.Enabled = false;
            //}
        }
    }
}