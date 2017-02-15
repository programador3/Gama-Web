using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class perfiles_relacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idc_puestoperfil");
                dt.Columns.Add("idc_puesto");
                dt.Columns.Add("puesto");
                dt.Columns.Add("perfil");
                Session["tabla_pp"] = dt;
                CargarPerfiles("");
                CargaPuestos("");
            }
        }

        /// <summary>
        /// Carga Puestos en Filtro
        /// </summary>
        public void CargaPuestos(string filtro)
        {
            try
            {
                Asignacion_RevisionesENT entidad = new Asignacion_RevisionesENT();
                Asignacion_RevisionesCOM componente = new Asignacion_RevisionesCOM();
                entidad.Filtro = filtro;
                entidad.Idc_puesto_revisa = Convert.ToInt32(Session["sidc_puesto_login"]);
                DataSet ds = componente.CargaCombos(entidad);
                repeat_pues.DataSource = ds.Tables[0];
                repeat_pues.DataBind();
                Session["tabla_puestos"] = ds.Tables[0];
                CargarPuestos();
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), this.Page);
                Global.CreateFileError(ex.ToString(), this);
            }
        }

        /// <summary>
        /// cARGA LOS PERFILES EN PRODUCCION CON FILTRO
        /// </summary>
        /// <param name="filtro"></param>
        private void CargarPerfiles(string filtro)
        {
            ProcesosENT enti = new ProcesosENT();
            enti.Ptioo = filtro;
            ProcesosCOM com = new ProcesosCOM();
            DataSet ds = com.CargaPerfiles(enti);
            ddlperfil.DataTextField = "descripcion";
            ddlperfil.DataValueField = "idc_puestoperfil";
            ddlperfil.DataSource = ds.Tables[0];
            ddlperfil.DataBind();
            ddlperfil.Items.Insert(0, new ListItem("--Seleccione un Perfil", "0")); //updated code}
        }

        /// <summary>
        /// CARGAR PERFILE SPOR PUESTO
        /// </summary>
        /// <param name="idc_puestoperfil"></param>
        private void CargarPerfilesRel(int idc_puestoperfil)
        {
            ProcesosENT enti = new ProcesosENT();
            ProcesosCOM com = new ProcesosCOM();
            enti.Pidc_proceso = idc_puestoperfil;
            DataSet ds = com.CargaPerfilespUESTOS(enti);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                AddToTable(Convert.ToInt32(row["idc_puestoperfil"]), Convert.ToInt32(row["idc_puesto"]), row["puesto"].ToString(), row["perfil"].ToString());
            }
        }

        /// <summary>
        /// ELIMINA FILA DE TABLA DE PERFILES TEMPORAL
        /// </summary>
        /// <param name="idc_puestoperfil"></param>
        /// <param name="idc_puesto"></param>
        private void DeleteToTable(int idc_puestoperfil, int idc_puesto)
        {
            DataTable dt = (DataTable)Session["tabla_pp"];
            foreach (DataRow row in dt.Rows)
            {
                int idc_puestoperfilr = Convert.ToInt32(row["idc_puestoperfil"]);
                int idc_puestor = Convert.ToInt32(row["idc_puesto"]);
                if (idc_puesto == idc_puestor && idc_puestoperfilr == idc_puestoperfil)
                {
                    row.Delete();
                    break;
                }
            }
            Session["tabla_pp"] = dt;
        }

        private void CargarPuestos()
        {
            DataTable dt = (DataTable)Session["tabla_pp"];
            foreach (RepeaterItem item in repeat_pues.Items)
            {
                Button lnk = (Button)item.FindControl("lnkpuesto");
                int idc_puesto = Convert.ToInt32(lnk.CommandName);
                foreach (DataRow row in dt.Rows)
                {
                    int idc_puestoperfilr = Convert.ToInt32(row["idc_puestoperfil"]);
                    int idc_puestor = Convert.ToInt32(row["idc_puesto"]);
                    if (idc_puesto == idc_puestor)
                    {
                        lnk.CssClass = "btn btn-success btn-block";
                    }
                }
            }
        }

        /// <summary>
        /// AGREGA FILA A TABLA DE PERFILES TEMPORAL
        /// </summary>
        /// <param name="idc_puestoperfil"></param>
        /// <param name="idc_puesto"></param>
        /// <param name="puesto"></param>
        /// <param name="perfil"></param>
        private void AddToTable(int idc_puestoperfil, int idc_puesto, string puesto, string perfil)
        {
            DataTable dt = (DataTable)Session["tabla_pp"];
            DeleteToTable(idc_puestoperfil, idc_puesto);
            DataRow row = dt.NewRow();
            row["idc_puesto"] = idc_puesto;
            row["idc_puestoperfil"] = idc_puestoperfil;
            row["puesto"] = puesto;
            row["perfil"] = perfil;
            dt.Rows.Add(row);
            Session["tabla_pp"] = dt;
        }

        /// <summary>
        /// CARGA GRID DE RELACIONES ACTUALES TEMPORALES (POR SOLICITAR)
        /// </summary>
        private void CargarGrid()
        {
            DataTable dt = (DataTable)Session["tabla_pp"];
            DataTable datacopy = dt.Copy();
            gridperfil.DataSource = datacopy;
            gridperfil.DataBind();
        }

        /// <summary>
        /// CADENA DE RELACIONES PP
        /// </summary>
        /// <returns></returns>
        private String Cadena()
        {
            string cadena = "";
            DataTable dt = (DataTable)Session["tabla_pp"];
            foreach (DataRow row in dt.Rows)
            {
                cadena = cadena + row["idc_puestoperfil"].ToString() + ";" + row["idc_puesto"].ToString() + ";" + row["perfil"].ToString() + ";" + row["puesto"].ToString() + ";";
            }
            return cadena;
        }

        /// <summary>
        /// TOTAL DE CADENA CADENA
        /// </summary>
        /// <returns></returns>
        private int TotalCadena()
        {
            int cadena = 0;
            DataTable dt = (DataTable)Session["tabla_pp"];
            return dt.Rows.Count;
        }

        protected void lnkbuscarpuestos_Click(object sender, EventArgs e)
        {
            CargaPuestos(txtpuesto_filtro.Text);
        }

        protected void lnkadd_Click(object sender, EventArgs e)
        {
        }

        protected void ddlperfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dpl = sender as DropDownList;
            int value = Convert.ToInt32(dpl.SelectedValue);
            if (value == 0)
            {
                Alert.ShowAlertError("Seleccione un valor Correcto", this);
            }
        }

        protected void lnkbuscarperfil_Click(object sender, EventArgs e)
        {
            CargarPerfiles(txtperfil_filtro.Text);
        }

        protected void gridperfil_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idc_puestoperfil = Convert.ToInt32(gridperfil.DataKeys[index].Values["idc_puestoperfil"]);
            int idc_puesto = Convert.ToInt32(gridperfil.DataKeys[index].Values["idc_puesto"]);
            switch (e.CommandName)
            {
                case "eliminar":
                    DeleteToTable(idc_puestoperfil, idc_puesto);
                    CargarGrid();
                    CargaPuestos("");
                    Alert.ShowAlert("Elemento eliminado correctamente", "Mensaje del Sistema", this);
                    break;
            }
        }

        protected void ddlPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dpl = sender as DropDownList;
            int value = Convert.ToInt32(dpl.SelectedValue);
            if (value == 0)
            {
                Alert.ShowAlertError("Seleccione un valor Correcto", this);
            }
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            string caso = (string)Session["Caso_Confirmacion"];
            switch (caso)
            {
                case "Guardar":
                    try
                    {
                        PuestosENT EntPuesto = new PuestosENT();
                        EntPuesto.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                        EntPuesto.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                        EntPuesto.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                        EntPuesto.Pusuariopc = funciones.GetUserName();//usuario pc
                        EntPuesto.Pcadena = Cadena();
                        EntPuesto.Ptotalcadena = TotalCadena();
                        PuestosCOM ComPuesto = new PuestosCOM();
                        DataSet ds = new DataSet();
                        ds = ComPuesto.puesto_perfil_temp_captura(EntPuesto);
                        string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        if (vmensaje == "")
                        {
                            Alert.ShowGiftMessage("Estamos procesando las solicitudes de autorizacion.", "Espere un Momento", "perfiles_relacion.aspx", "imagenes/loading.gif", "1000", "Las Solicitudes de Autorizacion fueron guardados correctamente. ", this);
                        }
                        else
                        {
                            Alert.ShowAlertError(vmensaje, this);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.ShowAlertError(ex.ToString(), this.Page);
                        Global.CreateFileError(ex.ToString(), this);
                    }

                    break;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (TotalCadena() == 0)
            {
                Alert.ShowAlertError("Debe existir una solicitud", this);
            }
            else
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','¿Desea Solicitar estos Cambios','modal fade modal-info');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("menu.aspx");
        }

        protected void lnkpuesto_Click(object sender, EventArgs e)
        {
            Button linkbtn = sender as Button;
            linkbtn.CssClass = linkbtn.CssClass == "btn btn-success btn-block" ? "btn btn-default btn-block" : "btn btn-success btn-block";
            string css = linkbtn.CssClass;
            int idc_pp = Convert.ToInt32(ddlperfil.SelectedValue);
            switch (css)
            {
                case "btn btn-success btn-block":
                    if (idc_pp == 0)
                    {
                        Alert.ShowAlertError("Seleccione un Perfil", this);
                        linkbtn.CssClass = linkbtn.CssClass == "btn btn-success btn-block" ? "btn btn-default btn-block" : "btn btn-success btn-block";
                    }
                    else
                    {
                        foreach (RepeaterItem item in repeat_pues.Items)
                        {
                            Button lnk = (Button)item.FindControl("lnkpuesto");
                            if (lnk == linkbtn)
                            {
                                int idc_p = Convert.ToInt32(lnk.CommandName);
                                DataTable dt = (DataTable)Session["tabla_puestos"];
                                DataView view = dt.DefaultView;
                                view.RowFilter = "idc_puesto =" + idc_p + "";
                                string name = view.ToTable().Rows[0]["descripcion"].ToString() + " | " + view.ToTable().Rows[0]["empleado"].ToString();
                                DeleteToTable(idc_pp, idc_p);
                                AddToTable(idc_pp, idc_p, name, ddlperfil.SelectedItem.ToString());
                                break;
                            }
                        }
                        CargarGrid();
                    }
                    break;

                case "btn btn-default btn-block":
                    foreach (RepeaterItem item in repeat_pues.Items)
                    {
                        Button lnk = (Button)item.FindControl("lnkpuesto");
                        if (lnk == linkbtn)
                        {
                            int idc_p = Convert.ToInt32(lnk.CommandName);
                            DeleteToTable(idc_pp, idc_p);
                        }
                    }

                    CargarGrid();
                    break;
            }
        }
    }
}