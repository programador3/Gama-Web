using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public partial class prueba : System.Web.UI.Page
    {
        public decimal total = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            if (fuArchivos.HasFiles)
            {
                foreach (var archivo in fuArchivos.PostedFiles)
                {
                    ListItem li = new ListItem();
                    var informacionDelArchivo = String.Format("{0} es de tipo {1} y tiene un tamaño de {2} KB.", archivo.FileName, Path.GetExtension(archivo.FileName), archivo.ContentLength / 1024);
                    li.Text = informacionDelArchivo;
                    blist.Items.Add(li);
                    total = total + archivo.ContentLength / 1024;
                    Label1.Text = total.ToString();
                    if (total > 2000)
                    {
                        Alert.ShowAlertInfo("A sobrepasado el limite de tamaño de archivos", "Mensaje del Sistema", this);
                    }
                }
            }
        }
    }
}