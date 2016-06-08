using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web.UI;

namespace presentacion
{
    public partial class organigrama : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerarRuta();
            if (!string.IsNullOrEmpty(Request.QueryString["ptipo"]))
            {
                cbotipos.SelectedValue = Request.QueryString["ptipo"];
            }
        }

        public void GenerarRuta()
        {
            var Entidad = new UsuariosE();
            Entidad.Cod_arch = "fot_emp";
            var Componente = new OrganigramaBL();
            var ds = new DataSet();
            ds = Componente.CargaPath(Entidad);
            if (ds.Tables[0].Rows.Count == 0)
            {
            }
            else
            {
                var tablaGrupos = new DataTable();
                //Paso dataset ala tabla
                tablaGrupos = ds.Tables[0];
                var row = tablaGrupos.Rows[0];
                var carpeta = row["rw_carpeta"].ToString();
                var domn = Request.Url.Host;
                var url = "http://" + domn + carpeta;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "organigrama('" + url + "');", true);
            }
        }
    }
}