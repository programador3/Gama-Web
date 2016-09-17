using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class prospectos_detalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["sidc_usuario"] == null)
                {
                    Response.Redirect("acceso.aspx");
                    return;
                }
                //valida si tiene permiso de ver esta pagina//
                int idc_usuario = Convert.ToInt32(Session["sidc_usuario"].ToString());

                //fin
                if (Request.QueryString["sidc_prospecto"] != null)
                {
                    int vidc;

                    vidc = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["sidc_prospecto"]));
                    //cargar datos
                    prospectos_ventasE llenar_datos = new prospectos_ventasE();

                    llenar_datos.Idc_prospecto = vidc;
                    //para saber de donde recuperar la ruta de la imagen.
                    //string cs = System.Configuration.ConfigurationManager.AppSettings["cs"];
                    //string phost;
                    //if (cs == "P"){
                    //    phost = variables.servidor_ima;
                    //}else{
                    //    phost = variables.servidor_ima_resp;
                    //}
                    string phost = funciones.obten_cadena_con("phost");
                    llenar_datos.P_host = phost;
                    DataSet ds = new DataSet();

                    Prospectos_ventasBL pros = new Prospectos_ventasBL();
                    try
                    {
                        ds = pros.datos_prospectos_ventas(llenar_datos);
                        int total = ds.Tables[0].Rows.Count;

                        if (total == 0)
                        {
                            msgbox.show("No se Encontraron Datos del Prospecto", this.Page);
                        }
                        else
                        {
                            lblid.Text = Convert.ToString(ds.Tables[0].Rows[0]["idc_prospecto"]);
                            lbldir.Text = Convert.ToString(ds.Tables[0].Rows[0]["direccion"]);
                            lbletapaob.Text = Convert.ToString(ds.Tables[0].Rows[0]["etapa_obra"]);
                            lblrsocial.Text = Convert.ToString(ds.Tables[0].Rows[0]["nombre_razon_social"]);
                            lblobs.Text = Convert.ToString(ds.Tables[0].Rows[0]["observacion"]);
                            lbltamobra.Text = Convert.ToString(ds.Tables[0].Rows[0]["tamaño_obra"]).Trim();
                            lbltipobra.Text = Convert.ToString(ds.Tables[0].Rows[0]["tipo_obra"]);
                            lblregistro.Text = Convert.ToString(ds.Tables[0].Rows[0]["fecha_registro"]);
                            lblusuario.Text = Convert.ToString(ds.Tables[0].Rows[0]["usuario"]);
                            //mas obras

                            GridObras.DataSource = ds.Tables[1];
                            GridObras.DataBind();

                            //contactos

                            GridContacto.DataSource = ds.Tables[2];
                            GridContacto.DataBind();
                            //GridTelefono.DataSource = ds.Tables[3];
                            //GridTelefono.DataBind();
                            //GridCorreo.DataSource = ds.Tables[4];
                            //GridCorreo.DataBind();
                            //creamos
                            DataTable tbl_contactos = new System.Data.DataTable();
                            DataTable tbl_telefonos = new System.Data.DataTable();
                            DataTable tbl_correos = new System.Data.DataTable();
                            //llenamos
                            tbl_contactos = ds.Tables[2];
                            tbl_telefonos = ds.Tables[3];
                            tbl_correos = ds.Tables[4];
                            //subimos
                            Session.Add("TablaContacto", tbl_contactos);
                            Session.Add("TablaTelefono", tbl_telefonos);
                            Session.Add("TablaCorreo", tbl_correos);
                            //IMAGENES
                            //miniatura
                            img1small.ImageUrl = Convert.ToString(ds.Tables[0].Rows[0]["ima1"]);
                            img2small.ImageUrl = Convert.ToString(ds.Tables[0].Rows[0]["ima2"]);
                            //tamaño original
                            img1.ImageUrl = Convert.ToString(ds.Tables[0].Rows[0]["ima1"]);
                            img2.ImageUrl = Convert.ToString(ds.Tables[0].Rows[0]["ima2"]);

                            //latitud y longitud
                            string latitud, longitud;
                            latitud = Convert.ToString(ds.Tables[0].Rows[0]["latitud"]);
                            longitud = Convert.ToString(ds.Tables[0].Rows[0]["longitud"]);
                            if (latitud != "" && longitud != "")
                            { //no estan vacios
                                lbllatitud.Visible = true;
                                lbllongitud.Visible = true;
                                lbllatitudval.Text = latitud;
                                lbllongitudval.Text = longitud;
                                lbllatitudval.Visible = true;
                                lbllongitudval.Visible = true;
                                btnimgmap.Visible = true;
                            }
                            else
                            {
                                lbllatitud.Visible = false;
                                lbllongitud.Visible = false;
                                lbllatitudval.Text = "";
                                lbllongitudval.Text = "";
                                lbllatitudval.Visible = false;
                                lbllongitudval.Visible = false;
                                btnimgmap.Visible = false;
                            }
                            //familia de articulos add 13-10-2015
                            // codigo add 01-10-2015  llennar la familia art detalle y marca/distribucion
                            DataTable tbl_famart_det = ds.Tables[5];
                            DataTable tbl_famart_det_mar = ds.Tables[6];
                            //subimos
                            Session["TablaFamartDet"] = tbl_famart_det;
                            Session["TablaFamartDetmar"] = tbl_famart_det_mar;
                            //cargamos grid

                            grid_famart_det.DataSource = tbl_famart_det;
                            grid_famart_det.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        msgbox.show(ex.Message, this.Page);
                    }
                }
            }
        }

        protected void GridContacto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //recuperamos el id del registro seleccionado del grid de Contacto
            int index = Convert.ToInt32(e.CommandArgument);
            int contacto_id = Convert.ToInt32(GridContacto.DataKeys[index].Value);
            switch (e.CommandName)
            {
                case "verTelefono":
                    verDetalle(contacto_id);
                    //pintamos el grid
                    GridContacto.Rows[index].BackColor = Color.FromName("#D7E8D7");
                    break;
            }
        }

        //funciones definidas por el usuario
        protected void verDetalle(int contacto_id)
        {
            //pintamos el grid
            //GridContacto.SelectedRow.BackColor = Color.FromName("#D7E8D7");

            //cargamos el datagrid de telefono
            DataTable tbl_telefonos = (System.Data.DataTable)(Session["TablaTelefono"]);
            //filtramos y llenamos el GridView
            DataView dv = new DataView(tbl_telefonos);
            dv.RowFilter = "contacto_id = " + contacto_id;
            GridTelefono.DataSource = dv;
            GridTelefono.DataBind();
            //CODIGO DE CORREO
            //cargamos el datagrid de telefono
            DataTable tbl_correos = (System.Data.DataTable)(Session["TablaCorreo"]);
            //filtramos y llenamos el GridView
            DataView dvcorreo = new DataView(tbl_correos);
            dvcorreo.RowFilter = "contacto_id = " + contacto_id;
            GridCorreo.DataSource = dvcorreo;
            GridCorreo.DataBind();
            //FIN
        }

        protected void btnimgmap_Click1(object sender, ImageClickEventArgs e)
        {
            string pagina;
            pagina = "mapa.aspx?longitud=" + funciones.deTextoa64(lbllongitudval.Text) + "&latitud=" + funciones.deTextoa64(lbllatitudval.Text);
            Response.Redirect(pagina);
        }

        //add 13-10-2015
        protected void grid_famart_det_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //me causaba error cuando usaba el link ver..
            //recuperamos el indice del registro seleccionado del grid de Telefono
            int index = Convert.ToInt32(e.CommandArgument);
            int vidc = Convert.ToInt32(grid_famart_det.DataKeys[index].Value);
            //Session["sidc_perfilgpo"] = Convert.ToInt32(vidc.ToString().Trim());
            switch (e.CommandName)
            {
                case "clic_famartdet":
                    //pintamos el grid
                    //ocultamos el index del grid
                    oc_gridfamartdet.Value = index.ToString();
                    grid_famart_det.Rows[index].BackColor = Color.FromName("#D7E8D7");

                    refreshGrid_FamartMarca();

                    break;
            }
        }

        protected void refreshGrid_FamartMarca()
        {
            int id_filtro = 0;
            if (grid_famart_det.Rows.Count > 0)
            {
                int index = Convert.ToInt32(oc_gridfamartdet.Value);
                if (index > -1)
                {
                    int seleccionado = Convert.ToInt32(grid_famart_det.DataKeys[index].Value);
                    if (seleccionado > 0)
                    {
                        id_filtro = seleccionado;
                    }
                }
            }

            DataTable tbl_famartdetmar = (DataTable)(Session["TablaFamartDetmar"]);
            DataView dv = tbl_famartdetmar.DefaultView;
            dv.RowFilter = "idc_prospecto_famartd=" + id_filtro;
            grid_famart_detmar.DataSource = dv;
            grid_famart_detmar.DataBind();
        }
    }
}