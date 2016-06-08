using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;

namespace presentacion
{
    public partial class solicitar_reemplazo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sidc_usuario"] == null)//si no hay session logeamos
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                CargarDatosEmpleado(Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"])));
            }
        }

        /// <summary>
        /// Carga DATOS DEL EMPLEADO
        /// </summary>
        public void CargarDatosEmpleado(int idc_puesto)
        {
            PuestosENT entidad = new PuestosENT();
            entidad.Idc_Puesto = idc_puesto;//indicamosm que queremos datos de empleado
            PuestosCOM componente = new PuestosCOM();
            DataSet ds = componente.CargaCatologoPuestos(entidad);
            DataRow row = ds.Tables[1].Rows[0];
            lblPuesto.Text = row["puesto"].ToString();
            lblNombre.Text = row["nombre"].ToString();
            lblDepto.Text = row["depto"].ToString();
            lblucursal.Text = row["sucursal"].ToString();
            Session["pidc_empleado_reemplazo"] = Convert.ToInt32(row["idc_empleado"]);
            GenerarRuta(Convert.ToInt32(row["idc_empleado"]), "fot_emp");
        }

        /// <summary>
        /// Genera ruta de imagen
        /// </summary>
        public void GenerarRuta(int id_comprobar, string codigo_imagen)
        {
            var Entidad = new UsuariosE();
            Entidad.Cod_arch = codigo_imagen;
            var Componente = new OrganigramaBL();
            var ds = new DataSet();
            ds = Componente.CargaPath(Entidad);
            if (ds.Tables[0].Rows.Count != 0)
            {
                var tablaGrupos = new DataTable();
                //Paso dataset ala tabla
                tablaGrupos = ds.Tables[0];
                var row = tablaGrupos.Rows[0];
                var carpeta = row["rw_carpeta"].ToString();
                var domn = Request.Url.Host;
                switch (codigo_imagen)
                {
                    case "fot_emp"://fotos de empleados
                        if (domn == "localhost")
                        {
                            var url = "imagenes/btn/default_employed.png";
                            myImage.Src = url;
                            ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                        }
                        else
                        {
                            var url = "http://" + domn + carpeta + id_comprobar + ".jpg";
                            myImage.Src = url;
                            ScriptManager.RegisterStartupScript(this, GetType(), "img", "getImage('" + url + "');", true);
                        }
                        break;
                }
            }
        }

        protected void lnkguardar_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (txtobservaciones.Text == "")
            {
                error = true;
                Alert.ShowAlertError("Debe escribir un motivo", this);
            }
            if (error == false)
            {
                Session["Caso_Confirmacion"] = "Guardar";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ModalConfirm('Mensaje del Sistema','Desea Guardar esta Solicitud');", true);
            }
        }

        protected void lnkcancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("puestos_catalogo.aspx");
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                string caso = (string)Session["Caso_Confirmacion"];
                PuestosENT entidad = new PuestosENT();
                entidad.Idc_Puesto = Convert.ToInt32(funciones.de64aTexto(Request.QueryString["idc_puesto"]));
                entidad.Pobservaciones = txtobservaciones.Text.ToUpper();
                entidad.Idc_usuario = Convert.ToInt32(Session["sidc_usuario"]);
                entidad.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                entidad.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                entidad.Pusuariopc = funciones.GetUserName();//usuario pc
                entidad.Pidc_pre_empleado = Convert.ToInt32(Session["pidc_empleado_reemplazo"]);
                PuestosCOM componente = new PuestosCOM();
                DataSet ds = new DataSet();
                string vmensaje = "";
                switch (caso)
                {
                    case "Guardar":
                        ds = componente.SolicitudRemplazo(entidad);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                        break;
                }
                if (vmensaje == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "AlertGO('Solicitud Guardada correctamente','puestos_catalogo.aspx');", true);
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
    }
}