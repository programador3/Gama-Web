﻿using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class aprobaciones_pendiente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarGrid_aprob_pendientes();
            }
        }

        /// <summary>
        /// Carga los datos del grid desde una base de datos SQL
        /// </summary>
        public void CargarGrid_aprob_pendientes()
        {
            int idc_puesto = 0;
            if (Session["sidc_puesto_login"] != null)
            {
                idc_puesto = Convert.ToInt32(Session["sidc_puesto_login"]);
            }
            AprobacionesENT entidad = new AprobacionesENT();
            entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
            entidad.Pidc_puesto = idc_puesto;
            AprobacionesCOM Componente = new AprobacionesCOM();
            DataSet ds = Componente.aprobaciones_pendientes(entidad);
            //meterlo a session
            Session.Add("TablaAprobacionPendiente", ds.Tables[0]);
            gridaprobacionespendientes.DataSource = ds.Tables[0];
            gridaprobacionespendientes.DataBind();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                lblTablaVacia.Visible = true;
            }
        }

        protected void gridaprobacionespendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(gridaprobacionespendientes.DataKeys[index].Values["idc_aprobacion_reg"].ToString());
            String pagina = gridaprobacionespendientes.DataKeys[index].Values["pagina"].ToString();
            String comentarios = gridaprobacionespendientes.DataKeys[index].Values["comentarios"].ToString();
            String descripcion = gridaprobacionespendientes.DataKeys[index].Values["descorta"].ToString();
            String nombre = gridaprobacionespendientes.DataKeys[index].Values["nombre"].ToString();
            String puesto = gridaprobacionespendientes.DataKeys[index].Values["des_puesto"].ToString();
            String fecha_movimiento = gridaprobacionespendientes.DataKeys[index].Values["fecha_movimiento"].ToString();
            String nombre_solic = gridaprobacionespendientes.DataKeys[index].Values["nombre_soli"].ToString();
            //recuperamos el valor de la fila
            DataRow[] row = buscarFila(vidc);
            switch (e.CommandName)
            {
                case "aprobar":
                    if (row.Length > 0)
                    {
                        //
                        modal_ocidc_aprobacion_reg.Value = row[0]["idc_aprobacion_reg"].ToString();
                        oc_idc_aprobacion_soli.Value = row[0]["idc_aprobacion_soli"].ToString();
                    }
                    modal_ocaprobado.Value = "True";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('Aprobar','');", true);
                    break;

                case "no_aprobar":
                    modal_ocidc_aprobacion_reg.Value = row[0]["idc_aprobacion_reg"].ToString();
                    oc_idc_aprobacion_soli.Value = row[0]["idc_aprobacion_soli"].ToString();
                    modal_ocaprobado.Value = "False";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "Return('No aprobar','');", true);
                    break;

                case "detalle":
                    Response.Redirect(pagina);
                    break;

                case "Vista":
                    Response.Redirect(pagina);
                    break;

                case "firma_grupal":
                    Session.Add("sidc_aprobacion_soli", row[0]["idc_aprobacion_soli"].ToString());
                    Response.Redirect("aprobaciones_firma.aspx");
                    break;

                case "Comentarios":
                    lblNombreComentarios.Text = nombre;
                    lblPuestoComentarios.Text = puesto;
                    lblPuestoSolicito.Text = nombre_solic;
                    txtAprobacionComentarios.Text = (comentarios == "" ? "SIN COMENTARIOS" : comentarios);
                    txtDescripcionComentarios.Text = descripcion;
                    lblFecha.Text = (fecha_movimiento == null || fecha_movimiento == "" ? "AUN EN PROCESO" : fecha_movimiento);
                    CargarGrid_aprob_pendientes();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", " modal_comentarios();", true);

                    break;
            }
        }

        protected void btnFirmar_Click(object sender, EventArgs e)
        {
            try
            {
                //recuperamos los valores
                int vidc_aprobacion_reg = Convert.ToInt32(modal_ocidc_aprobacion_reg.Value);
                string vusuario = Session["susuario"].ToString(); //MEDIANTE SESSION
                bool vaprobado = Convert.ToBoolean(modal_ocaprobado.Value);
                string vcontraseña = txtpass.Text.ToUpper();
                string vcomentarios = txtobs.Text;
                int Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);//USUARIO QUE REALIZA LA PREBAJA
                string Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                string Pnombrepc = funciones.GetPCName();//nombre pc usuario
                string Pusuariopc = funciones.GetUserName();//usuario pc
                //llamamos al componente
                AprobacionesCOM componente = new AprobacionesCOM();
                DataSet ds = new DataSet();
                ds = componente.validar_firma(vusuario, vcontraseña, vaprobado, vidc_aprobacion_reg, vcomentarios, Idc_usuario, Pdirecip, Pnombrepc, Pusuariopc, "", 0);
                //mesaje del sp
                string vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();

                if (string.IsNullOrEmpty(vmensaje)) // si esta vacio todo bien
                {
                    //refrescamos el grid
                    CargarGrid_aprob_pendientes();
                    limpiarModal();

                    Alert.ShowAlert("Movimiento Correcto", "Mensaje importante", this.Page);
                }
                else
                {
                    limpiarModal();
                    Alert.ShowAlertError(vmensaje, this.Page);
                    return;
                }
            }
            catch (Exception ex)
            {
                //msgbox.show(ex.Message, this.Page);
                Alert.ShowAlertError(ex.Message, this.Page);
            }
        }

        protected void limpiarModal()
        {
            modal_ocaprobado.Value = "";
            modal_ocidc_aprobacion_reg.Value = "0";
            oc_idc_aprobacion_soli.Value = "0";
            txtpass.Text = "";
            txtobs.Text = "";
        }

        protected DataRow[] buscarFila(int primarykey)
        {
            //bajamos la tabla de session
            DataTable tbl_aprobaciones_pend = (DataTable)Session["TablaAprobacionPendiente"];
            DataRow[] fila = tbl_aprobaciones_pend.Select("idc_aprobacion_reg=" + primarykey);

            return fila;
        }

        protected void No_Click(object sender, EventArgs e)
        {
            limpiarModal();
        }

        protected void gridaprobacionespendientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //revisar el campo aprobado
            //si es null darle opciones para firmar, en caso que ya lo hizo bloquear las opciones y poner la leyenda de aprobado o no aprobado

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView fila = (DataRowView)e.Row.DataItem;
                //boton que usamos como mensaje
                ImageButton btnaprobar = (ImageButton)e.Row.FindControl("imgbtnaprobar");
                ImageButton btn_noaprobar = (ImageButton)e.Row.FindControl("imgbtn_noaprobar");
                Button lblestat = (Button)e.Row.FindControl("lblestatus");
                //evaluamos la columna aprobado
                string color = fila["color"].ToString();
                e.Row.Cells[0].BackColor = Color.FromName(color);
                if (Convert.ToBoolean(fila["aprobado"]) == false && fila["comentarios"].ToString() == "")
                { //permite aprobar o no aprobar, el mensaje es pendiente
                    btnaprobar.Visible = true;
                    btn_noaprobar.Visible = true;
                    //
                    lblestat.Text = "Pendiente";
                    lblestat.CssClass = "btn btn-warning";
                }
                if (Convert.ToBoolean(fila["aprobado"]) == true)
                { //se bloquea las opciones y el mensaje es aprobado en verde
                    btnaprobar.Visible = false;
                    btn_noaprobar.Visible = false;
                    //
                    lblestat.Text = "Aprobado";
                    lblestat.CssClass = "btn btn-success";
                }
                if (Convert.ToBoolean(fila["aprobado"]) == false && fila["comentarios"].ToString() != "")
                { //quiere decir que no esta aprobado se bloquea las opciones y el mensaje en rojo
                    btnaprobar.Visible = false;
                    btn_noaprobar.Visible = false;
                    //
                    lblestat.Text = "No Aprobado";
                    lblestat.CssClass = "btn btn-danger";
                }

                Button salajuntas = (Button)e.Row.FindControl("btnfirmagrupal");
                salajuntas.CssClass = "btn btn-success";
                salajuntas.CommandName = "firma_grupal";
            }
        }
    }
}