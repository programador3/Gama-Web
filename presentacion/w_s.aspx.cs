using negocio.Componentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;

//

namespace presentacion
{
    public partial class w_s : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack != true)
            {
                try
                {
                    string tipo = Request.QueryString["tipo"];
                    //int tot_param = int.Parse(Request.QueryString["tot"]);
                    if (tipo == "1")
                    {
                        string sp = Request.QueryString["sp"];
                        string pname = Request.QueryString["pname"];
                        string pvalue = Request.QueryString["pvalue"];

                        object[] p_v = parametros_value(pvalue);

                        string[] p_n = parametros_name(pname);
                        resp_sp(sp, p_n, p_v);
                    }
                }
                catch (Exception ex)
                {
                    msgbox.show(ex.Message, this.Page);
                }
            }
        }

        public string[] parametros_name(string parameters)
        {
            string del = "|";
            string[] p = parameters.Split(del.ToCharArray(), StringSplitOptions.None);
            //string[] p = parameters.Split(new string[] { "|" });
            return p;
        }

        public object[] parametros_value(string parameters)
        {
            string del = "|";
            string[] p = parameters.Split(del.ToCharArray(), StringSplitOptions.None);
            //object[] p = parameters.Split(new string[] { "|" });
            return p;
        }

        private void resp_sp(string sp, string[] p_name, object[] p_value)
        {
            DataSet ds = new DataSet();

            OrganigramaBL w = new OrganigramaBL();
            Response.Clear();
            Response.ContentType = "";
            Response.Charset = "UTF-8";
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            //using (SqlConnection cn = new SqlConnection("Data Source=192.168.0.5;Initial Catalog=GM;Persist Security Info=True;User ID=conexion;Password=GaMa90")) //change as needed
            string cadena = System.Configuration.ConfigurationManager.AppSettings["strDeConexion"];
            string resultados = "";
            JavaScriptSerializer j = new JavaScriptSerializer();
            try
            {
                ds = w.sp_ajax(sp, p_name, p_value, false);
                foreach (DataTable x in ds.Tables)
                {
                    if (x.Rows.Count > 0)
                    {
                        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                        foreach (DataRow row in x.Rows)
                        {
                            Dictionary<string, object> dict = new Dictionary<string, object>();
                            foreach (DataColumn col in x.Columns)
                            {
                                dict[col.ColumnName] = row[col].ToString().Trim();
                            }
                            list.Add(dict);
                        }
                        j.MaxJsonLength = Int32.MaxValue;
                        //resultados = resultados + j.Serialize(list.ToArray());
                        resultados = resultados + (resultados == "" ? "" : ",") + j.Serialize(list.ToArray());
                        //Response.Headers.Add("Access-Control-Allow-Origin", "*");
                        //Response.Write(resultados.ToString());
                        //Response.Flush();
                        //resultados = "{" + resultados + "}";
                    }
                    else
                    {
                        resultados = resultados + (resultados == "" ? "[]" : ",[]");
                    }
                }

                resultados = "[" + resultados + "]";
                Response.Write(resultados.ToString());

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                //    foreach (DataRow row in ds.Tables[0].Rows)
                //    {
                //        Dictionary<string, object> dict = new Dictionary<string, object>();
                //        foreach (DataColumn col in ds.Tables[0].Columns)
                //        {
                //            dict[col.ColumnName] = row[col].ToString().Trim();
                //        }
                //        list.Add(dict);
                //    }
                //    JavaScriptSerializer j = new JavaScriptSerializer();
                //    j.MaxJsonLength = Int32.MaxValue;
                //    resultados = j.Serialize(list.ToArray());
                //    //Response.Headers.Add("Access-Control-Allow-Origin", "*");
                //    Response.Write(resultados.ToString());
                //    //Response.Flush();
                //}
            }
            catch (Exception ex)
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 500;
                Response.Write("Ocurrio un Error.");
                Response.Write(ex.ToString());
            }
            Response.End();
        }
    }
}