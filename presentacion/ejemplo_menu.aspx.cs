using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class ejemplo_menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MenuPadres_carga();
            Random rnd = new Random();
            int user_int = rnd.Next(1, 1000);
            int user_int2 = rnd.Next(1000, 2000);
            string user = "TEMPUSER" + user_int.ToString() + user_int2.ToString();
            lblName.Text = getCookie("UserSettings", "user", user);
        }

        /// <summary>
        /// Crea una cookie con expiracion de 7 dias
        /// </summary>
        /// <param name="data"></param>
        private void setCookie(string cookie_name, string cookie_value, string cookie_valuedata)
        {
            HttpCookie myCookie = new HttpCookie(cookie_name);
            myCookie[cookie_value] = cookie_valuedata;
            myCookie.Expires = DateTime.Now.AddDays(7d);
            Response.Cookies.Add(myCookie);
        }

        /// <summary>
        /// Devuelve el valor actual de una cookie, si no existe la crea con expiracion de 7 dias
        /// </summary>
        /// <returns></returns>
        public string getCookie(string cookie_name, string cookie_value, string cookie_valuedata)
        {
            string userSettings = "";
            if (Request.Cookies[cookie_name] == null)
            {
                setCookie(cookie_name, cookie_value, cookie_valuedata);
            }
            if (Request.Cookies[cookie_name][cookie_value] == null)
            {
                setCookie(cookie_name, cookie_value, cookie_valuedata);
            }
            userSettings = Request.Cookies[cookie_name][cookie_value];
            return userSettings;
        }

        /// <summary>
        /// Expira el valor de una cookie
        /// </summary>
        private void delCookie(string cookie_name)
        {
            if (Request.Cookies[cookie_name] != null)
            {
                HttpCookie myCookie = new HttpCookie(cookie_name);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
                Response.Redirect("ejemplo_menu.aspx");
            }
        }

        public void MenuPadres_carga()
        {
            //tabla con los id padres
            DataTable tabla_temp_padre = new DataTable();
            tabla_temp_padre = GetDataMenu(0);
            MenuPadres.DataSource = tabla_temp_padre;
            MenuPadres.DataBind();
        }

        public DataTable GetDataMenu(int number_table)
        {
            DataTable table = new DataTable();
            Nuevas_AprobacionesENT ent = new Nuevas_AprobacionesENT();
            Nuevas_AprobacionesCOM comp = new Nuevas_AprobacionesCOM();
            table = comp.datamenu(ent).Tables[number_table];
            return table;
        }

        protected void MenuPadres_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView dbr = (DataRowView)e.Item.DataItem;
            //tbajamos tablas hijos
            DataTable tabla_temp_hijo = new DataTable();
            tabla_temp_hijo = GetDataMenu(1);
            //tbajamos tablas padres
            DataTable tabla_temp_padre = new DataTable();
            tabla_temp_padre = GetDataMenu(0);
            string padre = Convert.ToString(DataBinder.Eval(dbr, "nombre"));
            DataRow[] result = tabla_temp_padre.Select("nombre = '" + padre + "'");
            DataRow row = result[0];
            int id_padre = Convert.ToInt32(row["idc_categoria_padre"].ToString());
            DataTable resultado = new DataTable();
            resultado.Columns.Add("hijo");
            foreach (DataRow roww in tabla_temp_hijo.Rows)
            {
                int hijo = Convert.ToInt32(roww["idc_categoria_padre"].ToString());
                if (hijo == id_padre)
                {
                    DataRow new_row = resultado.NewRow();
                    new_row["hijo"] = roww["nombre"].ToString();
                    resultado.Rows.Add(new_row);
                }
            }
            Repeater MneuHijo = (Repeater)e.Item.FindControl("MenuHijo");
            MneuHijo.DataSource = resultado;
            MneuHijo.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            delCookie("UserSettings");
        }
    }
}