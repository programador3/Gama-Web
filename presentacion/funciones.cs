using negocio.Componentes;
using negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion
{
    public class funciones
    {

        public static DBConnection conexion = new DBConnection();
        public static string GUID()
        {
            Random random_edit = new Random();
            int randomNumber_live = random_edit.Next(0, 1000000);
            DateTime localDate = DateTime.Now;
            string date = localDate.ToString();
            date = date.Replace("/", "");
            date = date.Replace(":", "");

            return date + randomNumber_live.ToString();
        }
        public static string NombreMes(string strOutput)
        {
            strOutput = strOutput.Replace("Monday", "Lunes");
            strOutput = strOutput.Replace("Tuesday", "Martes");
            strOutput = strOutput.Replace("Wednesday", "Miércoles");
            strOutput = strOutput.Replace("Thursday", "Jueves");
            strOutput = strOutput.Replace("Friday", "Viernes");
            strOutput = strOutput.Replace("Saturday", "Sábado");
            strOutput = strOutput.Replace("Sunday", "Domingo");
            strOutput = strOutput.Replace("January", "Enero");
            strOutput = strOutput.Replace("February", "Febrero");
            strOutput = strOutput.Replace("March", "Marzo");
            strOutput = strOutput.Replace("April", "Abril");
            strOutput = strOutput.Replace("May", "Mayo");
            strOutput = strOutput.Replace("June", "Junio");
            strOutput = strOutput.Replace("July", "Julio");
            strOutput = strOutput.Replace("August", "Agosto");
            strOutput = strOutput.Replace("September", "Septiembre");
            strOutput = strOutput.Replace("October", "Octubre");
            strOutput = strOutput.Replace("November", "Noviembre");
            strOutput = strOutput.Replace("December", "Diciembre");
            return strOutput;
        }
        String FormatNumber(decimal valor, int decimales)
        {
            string total_decimale = "";
            for (int i = 0; i < decimales; i++)
            {
                total_decimale = total_decimale + "#";
            }

            return valor.ToString("#,###."+total_decimale);
        }

        /// <summary>
        /// Devuelve meses
        /// </summary>
        /// <returns></returns>
        public static DataTable meses()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value", typeof(Int32));
            dt.Columns.Add("name");
            for (int i = 1; i <= 12; i++)
            {
                DataRow row = dt.NewRow();
                string fullMonthName = new DateTime(2015, i, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es")).ToUpper();
                row["value"] = i;
                row["name"] = fullMonthName;
                dt.Rows.Add(row);
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "value";
            return dv.ToTable();
        }

        public static DataTable años()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value");
            dt.Columns.Add("name");

            for (int i = 2000; i <= DateTime.Today.Year; i++)
            {
                DataRow row = dt.NewRow();
                row["value"] = i;
                row["name"] = i.ToString().Trim();
                dt.Rows.Add(row);
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "value";
            return dv.ToTable();
        }
        public static DataTable TopDataRow(DataTable dt, int count)
        {
            DataTable dtn = dt.Clone();
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (i < count)
                {
                    dtn.ImportRow(row);
                    i++;
                }
                if (i > count)
                    break;
            }
            return dtn;
        }


        /// <summary>
        /// Regresa el filtro de una tabla
        /// </summary>
        /// <param name="idc_pregunta"></param>
        public static DataTable FiltrarDataTable(DataTable dt, string query)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = query;
            return dv.ToTable();
        }

        /// <summary>
        /// Traduce una fecha al español (BETHA)
        /// </summary>
        /// <returns></returns>
        public static string TradFecha(string fecha)
        {
            string val = "";
            val = fecha.Replace("January", "Enero");
            val = fecha.Replace("February", "Fecbrero");
            val = fecha.Replace("March ", "Marzo");
            val = fecha.Replace("April", "Abril");
            val = fecha.Replace("May", "Mayp");
            val = fecha.Replace("June", "Junio");
            val = fecha.Replace("July", "Julio");
            val = fecha.Replace("August", "Agosto");
            val = fecha.Replace("September", "Septiembre");
            val = fecha.Replace("October", "Octubre");
            val = fecha.Replace("November", "Noviembre");
            val = fecha.Replace("December", "Diciembre");
            val = fecha.Replace(",", "");
            val = fecha.Replace(".", "");
            return val;
        }

        public static string Redondeo_Dos_Decimales(decimal valor)
        {
            try
            {
                valor = Math.Round(valor, 2);
                return valor.ToString("#.##");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool isNumeric(string s)
        {
            decimal i = 0;
            return decimal.TryParse(s, out i);
        }
        public static string ContenidoArchivo(string ruta)
        {
            string value = "";
            try
            {
                if (File.Exists(ruta))
                {
                    StreamReader file = new StreamReader(ruta);
                    value = file.ReadToEnd();
                    file.Close();
                }
                return value;
            }
            catch (Exception)
            {
                return "";
            }
        }
        
        public static string cuentas_correo(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = funciones.conexion.Datos(query);
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToString(dt.Rows[0][0]);
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        public static DataTable ExecQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                query = query.Replace("\t", " ");
                AgentesENT enti = new AgentesENT();
                enti.Pcadenaarti = query;
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_exec_query_web(enti);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                funciones.EnviarError(ex.ToString());
                string t = ex.ToString();
                return dt;
            }
        }
        public static DataTable CorreoReclutador(int idc_pre_empleado)
        {
            DataTable dt = new DataTable();
            try
            {
                AgentesENT enti = new AgentesENT();
                AgentesCOM com = new AgentesCOM();
                DataSet ds = com.sp_comprobar_correo_reclutador(idc_pre_empleado);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                funciones.EnviarError(ex.ToString());
                string t = ex.ToString();
                return dt;
            }
        }
        public static string EnviarCorreo(string username, string password, string hostnamesmtp, int portsmtp, bool useSsl,
            string subject, string body, string to, List<string> listadeadjuntos, bool ishtml)
        {
            try
            {
                if (to[to.Length - 1].ToString() == ",")
                {
                    to = to.Remove(to.Length - 1);
                }
                MailMessage message = new MailMessage();
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = ishtml;
                message.To.Add(to);
                message.From = new MailAddress(username, username, System.Text.Encoding.UTF8);
                SmtpClient mailer = new SmtpClient(hostnamesmtp, portsmtp);
                mailer.Credentials = new NetworkCredential(username, password);
                mailer.EnableSsl = useSsl;
                foreach (string adjunto in listadeadjuntos)
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(adjunto);
                    attachment.Name = Path.GetFileName(adjunto);
                    message.Attachments.Add(attachment);
                }
                mailer.Send(message);
                message.Attachments.Dispose();
                message.Dispose();
                return "";
            }
            catch (Exception ex)
            {
                funciones.EnviarError(ex.ToString());
                return ex.ToString();
            }
        }

        public static void EnviarError(string bodycontent)
        {
           
        }

        /// <summary>
        /// Descarga un archivo
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file_name"></param>
        public static void Download(string path, string file_name, Page page)
        {
            if (!File.Exists(path))
            {
                Alert.ShowAlertError("No tiene archivo relacionado", page);
            }
            else
            {
                // Limpiamos la salida
                page.Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                page.Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                page.Response.AddHeader("Content-Disposition", "attachment; filename=" + file_name);
                // Escribimos el fichero a enviar
                page.Response.WriteFile(path);
                // volcamos el stream
                page.Response.Flush();
                // Enviamos todo el encabezado ahora
                page.Response.End();
                // Response.End();
            }
        }

        public static string obten_cadena_con(string tipo)
        {
            string cadena;
            try
            {
                string cs = System.Configuration.ConfigurationManager.AppSettings["cs"];
                if (tipo == "conexion")
                {
                    if (cs == "P")
                        cadena = variables.cad_conexion;
                    else
                        cadena = variables.cad_conexion_respa;
                }
                else if (tipo == "phost")
                {
                    if (cs == "P")
                    {
                        cadena = variables.servidor_ima;
                    }
                    else
                    {
                        cadena = variables.servidor_ima_resp;
                    }
                }
                else
                {
                    cadena = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cadena;
        }

        /// <summary>
        /// Convierte un Objeto tipo DataTable en FORMATO HTML
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="table_name"></param>
        /// <returns></returns>
        public static StringBuilder TableDinamic(DataTable dt, string table_name)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<table id='" + table_name + "'  class='dt table table-responsive table-bordered- table-condensed'>");
            html.Append("<thead>");
            html.Append("<tr>");
            foreach (DataColumn columna in dt.Columns)
            {
                html.Append("<th>");
                html.Append(columna.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");
            html.Append("</thead>");
            html.Append("<tbody>");
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn columna in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[columna.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }
            html.Append("</tbody>");
            html.Append("</table>");
            return html;
        }

        /// <summary>
        /// Convierte el formato string(HH:MM) a minutos en entero
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ReturnMinutesfromString(string value)
        {
            string hour = "";
            foreach (char val in value)
            {
                if (val != ':')
                {
                    hour = hour + val;
                }
                else
                {
                    value = value.Replace(hour + val.ToString(), "");
                    break;
                }
            }
            return Convert.ToInt32(value) + (Convert.ToInt32(hour) * 60);
        }

        /// <summary>
        /// Regresa un TRUE si la fecha esta dentro de un horario laboral valido, en caso contrario regresa un FALSE
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static Boolean FechaCorrecta(DateTime time)
        {
            bool correcta = true;
            int minutes = ((time.Hour) * 60) + time.Minute;
            if (time.DayOfWeek == DayOfWeek.Sunday)
            {
                correcta = false;
            }
            if (time.DayOfWeek == DayOfWeek.Saturday)
            {
                if (minutes >= 781 || minutes <= 541)
                {
                    correcta = false;
                }
            }
            if (time.DayOfWeek != DayOfWeek.Saturday && time.DayOfWeek != DayOfWeek.Sunday)
            {
                if (minutes >= 1081 || minutes <= 541)
                {
                    correcta = false;
                }
            }
            return correcta;
        }

        /// <summary>
        /// Copia todos los archivos de una ruta a otra
        /// </summary>
        /// <param name="sourceFolder"></param>
        /// <param name="destFolder"></param>
        /// <param name="page"></param>
        public static void CopyFolder(string sourceFolder, string destFolder, Page page)
        {
            try
            {
                if (!Directory.Exists(destFolder))
                    Directory.CreateDirectory(destFolder);
                string[] files = Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(destFolder, name);
                    File.Copy(file, dest, true);
                }
                string[] folders = Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders)
                {
                    string name = Path.GetFileName(folder);
                    string dest = Path.Combine(destFolder, name);
                    CopyFolder(folder, dest, page);
                }
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), page);
            }
        }

        /// <summary>
        /// Copia un archivo de una ruta especifica a otra, si todo fue correcto devuelve un TRUE
        /// </summary>
        /// <param name="sourcefilename"></param>
        /// <param name="destfilename"></param>
        /// <returns></returns>
        public static bool CopiarArchivos(string sourcefilename, string destfilename, Page pag)
        {
            bool correct = true;
            try
            {
                if (!File.Exists(sourcefilename))
                {
                    Alert.ShowAlertError("No existe la ruta " + sourcefilename, pag);
                    correct = false;
                }
                if (File.Exists(sourcefilename))
                {
                    File.Copy(sourcefilename, destfilename, true);
                    correct = true;
                }
                return correct;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.Message, pag);
                correct = false;
                return correct;
            }
        }

        /// <summary>
        /// Cambia los valores tipo URL por string (NO USAR EN TEXTO GRANDE)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ChangeValue(string value)
        {
            value = value.Replace("%f0", "ð");
            value = value.Replace("%f1", "ñ");
            value = value.Replace("%f2", "ò");
            value = value.Replace("%f3", "ó");
            value = value.Replace("%f4", "ô");
            value = value.Replace("%f5", "õ");
            value = value.Replace("%f6", "ö");
            value = value.Replace("%f7", "÷");
            value = value.Replace("%f8", "ø");
            value = value.Replace("%f9", "ù");
            value = value.Replace("%fa", "ú");
            value = value.Replace("%fb", "û");
            value = value.Replace("%fc", "ü");
            value = value.Replace("%fd", "ý");
            value = value.Replace("%fe", "þ");
            value = value.Replace("%ff", "ÿ");
            value = value.Replace("%1e", "!");
            value = value.Replace("20%", "#");
            value = value.Replace("21%", "$");
            value = value.Replace("22%", "%");
            value = value.Replace("23%", "&");
            value = value.Replace("24%", "'");
            value = value.Replace("25%", "(");
            value = value.Replace("26%", ")");
            value = value.Replace("27%", "*");
            value = value.Replace("28%", "+");
            value = value.Replace("29%", ",");
            value = value.Replace("%2a", "-");
            value = value.Replace("%2b", ".");
            value = value.Replace("%2c", "/");
            value = value.Replace("%2d", "/");
            value = value.Replace("%2e", "/");
            value = value.Replace("%2f", "/");
            value = value.Replace("30%", "0");
            value = value.Replace("31%", "1");
            value = value.Replace("32%", "2");
            value = value.Replace("33%", "3");
            value = value.Replace("34%", "4");
            value = value.Replace("35%", "5");
            value = value.Replace("36%", "6");
            value = value.Replace("37%", "7");
            value = value.Replace("38%", "8");
            value = value.Replace("39%", "9");
            value = value.Replace("%3a", ":");
            value = value.Replace("%3b", ";");
            value = value.Replace("%3c", "<");
            value = value.Replace("%3d", "=");
            value = value.Replace("%3e", ">");
            value = value.Replace("%3f", "?");
            value = value.Replace("40%", "@");
            value = value.Replace("41%", "A");
            value = value.Replace("42%", "B");
            value = value.Replace("43%", "C");
            value = value.Replace("44%", "D");
            value = value.Replace("45%", "E");
            value = value.Replace("46%", "F");
            value = value.Replace("47%", "G");
            value = value.Replace("48%", "H");
            value = value.Replace("49%", "I");
            value = value.Replace("%4a", "J");
            value = value.Replace("%4b", "K");
            value = value.Replace("%4c", "L");
            value = value.Replace("%4d", "M");
            value = value.Replace("%4e", "N");
            value = value.Replace("%4f", "O");
            value = value.Replace("50%", "P");
            value = value.Replace("51%", "Q");
            value = value.Replace("52%", "R");
            value = value.Replace("53%", "S");
            value = value.Replace("54%", "T");
            value = value.Replace("55%", "U");
            value = value.Replace("56%", "V");
            value = value.Replace("57%", "W");
            value = value.Replace("58%", "X");
            value = value.Replace("59%", "Y");
            value = value.Replace("%5a", "Z");
            value = value.Replace("%5b", "[");
            value = value.Replace("%5c", @"\");
            value = value.Replace("%5d", "]");
            value = value.Replace("%5e", "^");
            value = value.Replace("%5f", "_");
            value = value.Replace("60%", "`");
            value = value.Replace("61%", "a");
            value = value.Replace("62%", "b");
            value = value.Replace("63%", "c");
            value = value.Replace("64%", "d");
            value = value.Replace("65%", "e");
            value = value.Replace("66%", "f");
            value = value.Replace("67%", "g");
            value = value.Replace("68%", "h");
            value = value.Replace("69%", "i");
            value = value.Replace("%6a", "j");
            value = value.Replace("%6b", "k");
            value = value.Replace("%6c", "l");
            value = value.Replace("%6d", "m");
            value = value.Replace("%6e", "n");
            value = value.Replace("%6f", "o");
            value = value.Replace("70%", "p");
            value = value.Replace("71%", "q");
            value = value.Replace("72%", "r");
            value = value.Replace("73%", "s");
            value = value.Replace("74%", "t");
            value = value.Replace("75%", "u");
            value = value.Replace("76%", "v");
            value = value.Replace("77%", "w");
            value = value.Replace("78%", "x");
            value = value.Replace("79%", "y");
            value = value.Replace("%7a", "z");
            value = value.Replace("%7b", "{");
            value = value.Replace("%7c", "|");
            value = value.Replace("%7d", "}");
            value = value.Replace("%7e", "~");
            value = value.Replace("%7f", "");
            value = value.Replace("80%", "â?¬");
            value = value.Replace("81%", "");
            value = value.Replace("82%", "‚");
            value = value.Replace("83%", "ƒ");
            value = value.Replace("84%", "„");
            value = value.Replace("85%", "…");
            value = value.Replace("86%", "†");
            value = value.Replace("87%", "‡");
            value = value.Replace("88%", "ˆ");
            value = value.Replace("89%", "‰");
            value = value.Replace("%8a", "Š");
            value = value.Replace("%8b", "‹");
            value = value.Replace("%8c", "Œ");
            value = value.Replace("%8d", "");
            value = value.Replace("%8e", "Å½");
            value = value.Replace("91%", "‘");
            value = value.Replace("92%", "’");
            value = value.Replace("93%", "“");
            value = value.Replace("94%", "”");
            value = value.Replace("95%", "•");
            value = value.Replace("96%", "–");
            value = value.Replace("97%", "—");
            value = value.Replace("98%", "˜");
            value = value.Replace("99%", "™");
            value = value.Replace("%9a", "š");
            value = value.Replace("%9b", "›");
            value = value.Replace("%9c", "œ");
            value = value.Replace("%9d", "");
            value = value.Replace("%9e", "Å¾");
            value = value.Replace("%9f", "Ÿ");
            value = value.Replace("%a0", "");
            value = value.Replace("%a1", "¡");
            value = value.Replace("%a2", "¢");
            value = value.Replace("%a3", "£");
            value = value.Replace("%a4", "");
            value = value.Replace("%a5", "¥");
            value = value.Replace("%a6", "|");
            value = value.Replace("%a7", "§");
            value = value.Replace("%a8", "¨");
            value = value.Replace("%a9", "©");
            value = value.Replace("%aa", "ª");
            value = value.Replace("%ab", "«");
            value = value.Replace("%ac", "¬");
            value = value.Replace("%ad", "¯");
            value = value.Replace("%ae", "®");
            value = value.Replace("%af", "¯");
            value = value.Replace("%b0", "°");
            value = value.Replace("%b1", "±");
            value = value.Replace("%b2", "²");
            value = value.Replace("%b3", "³");
            value = value.Replace("%b4", "'");
            value = value.Replace("%b5", "µ");
            value = value.Replace("%b6", "¶");
            value = value.Replace("%b7", "·");
            value = value.Replace("%b8", "¸");
            value = value.Replace("%b9", "¹");
            value = value.Replace("%ba", "º");
            value = value.Replace("%bb", "»");
            value = value.Replace("%bc", "¼");
            value = value.Replace("%bd", "½");
            value = value.Replace("%be", "¾");
            value = value.Replace("%bf", "¿");
            value = value.Replace("%c0", "À");
            value = value.Replace("%c1", "Á");
            value = value.Replace("%c2", "Â");
            value = value.Replace("%c3", "Ã");
            value = value.Replace("%c4", "Ä");
            value = value.Replace("%c5", "Å");
            value = value.Replace("%c6", "Æ");
            value = value.Replace("%c7", "Ç");
            value = value.Replace("%c8", "È");
            value = value.Replace("%c9", "É");
            value = value.Replace("%ca", "Ê");
            value = value.Replace("%cb", "Ë");
            value = value.Replace("%cc", "Ì");
            value = value.Replace("%cd", "Í");
            value = value.Replace("%ce", "Î");
            value = value.Replace("%cf", "Ï");
            value = value.Replace("%d0", "Ð");
            value = value.Replace("%d1", "Ñ");
            value = value.Replace("%d2", "Ò");
            value = value.Replace("%d3", "Ó");
            value = value.Replace("%d4", "Ô");
            value = value.Replace("%d5", "Õ");
            value = value.Replace("%d6", "Ö");
            value = value.Replace("%d7", "");
            value = value.Replace("%d8", "Ø");
            value = value.Replace("%d9", "Ù");
            value = value.Replace("%da", "Ú");
            value = value.Replace("%db", "Û");
            value = value.Replace("%dc", "Ü");
            value = value.Replace("%dd", "Ý");
            value = value.Replace("%de", "Þ");
            value = value.Replace("%df", "ß");
            value = value.Replace("%e0", "à");
            value = value.Replace("%e1", "á");
            value = value.Replace("%e2", "â");
            value = value.Replace("%e3", "ã");
            value = value.Replace("%e4", "ä");
            value = value.Replace("%e5", "å");
            value = value.Replace("%e6", "æ");
            value = value.Replace("%e7", "ç");
            value = value.Replace("%e8", "è");
            value = value.Replace("%e9", "é");
            value = value.Replace("%ea", "ê");
            value = value.Replace("%eb", "ë");
            value = value.Replace("%ec", "ì");
            value = value.Replace("%ed", "í");
            value = value.Replace("%ee", "î");
            value = value.Replace("%ef", "ï");
            return value;
        }

        public static bool GetFolder(string folder)
        {
            return Directory.Exists(folder);
        }

        public static bool GetFile(string file)
        {
            return File.Exists(file);
        }

        public static void CreateFolder(string folder)
        {
            Directory.CreateDirectory(folder);
        }

        public static string CreateFile(string path, string content)
        {
            try
            {
                if (!File.Exists(path))
                {
                    StreamWriter file = new StreamWriter(path);
                    file.Write(content);
                    file.Close();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static List<string> ListDirectory(string directori)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(@directori);
                DirectoryInfo[] directories = directory.GetDirectories();
                //FileInfo[] files = directory.GetFiles("*.*");
                //for (int i = 0; i < files.Length; i++)
                //{
                //    Console.WriteLine(((FileInfo)files[i]).FullName);
                //}
                List<string> list = new List<string>();
                for (int i = 0; i < directories.Length; i++)
                {
                    list.Add(directories[i].ToString());
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable ReadFiles(string path)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("contenido");
                string line;
                StreamReader file = new StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    DataRow row = dt.NewRow();
                    row["contenido"] = line;
                    dt.Rows.Add(row);
                }
                file.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void WriteFile_Chat(string path, string new_content)
        {
            if (!File.Exists(path))
            {
                StreamWriter file = new StreamWriter(path);
                file.Close();
            }
            StreamReader read = new StreamReader(path);
            string content = read.ReadToEnd() + new_content + System.Environment.NewLine;
            read.Close();
            File.Delete(path);
            StreamWriter file2 = new StreamWriter(path);
            file2.Write(content);
            file2.Close();
        }

        public static DataTable ListFiles(string directori)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("nombre");
                dt.Columns.Add("idc_usuario");
                DirectoryInfo directory = new DirectoryInfo(@directori);
                DirectoryInfo[] directories = directory.GetDirectories();

                List<string> list = new List<string>();
                for (int d = 0; d < directories.Length; d++)
                {
                    string dir = directories[d].FullName;
                    DirectoryInfo director = new DirectoryInfo(@dir + "\\");
                    FileInfo[] files = director.GetFiles("*.gama*");
                    for (int i = 0; i < files.Length; i++)
                    {
                        DataRow row = dt.NewRow();
                        string stri = files[i].FullName;
                        StreamReader file = new StreamReader(stri);
                        string read = file.ReadToEnd();
                        string name = Path.GetFileNameWithoutExtension(stri);
                        name = name.Replace("_online", "");
                        list.Add(read);
                        row["nombre"] = read;
                        row["idc_usuario"] = name;
                        dt.Rows.Add(row);
                        file.Close();
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Sube archivos a ruta
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public static bool UploadFile(FileUpload FileUPL, String ruta, Page page)
        {
            try
            {
                FileUPL.PostedFile.SaveAs(ruta);
                return true;
            }
            catch (Exception ex)
            {
                Alert.ShowAlertError(ex.ToString(), page);
                return false;
            }
        }

        /// <summary>
        /// Retorna una cadena con ruta especifica, necesita la clave de ruta y el nombre de la columna a regresar(UNIDAD,RW_CARPETA)
        /// </summary>
        /// <param name="codigo_imagen"></param>
        /// <param name="column_name"></param>
        /// <returns></returns>
        public static string GenerarRuta(string codigo_imagen, string column_name)
        {
            string rutaarch = "";
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
                rutaarch = @row[column_name].ToString();
            }
            return @rutaarch;
        }

        public static DataTable ValidarFolio(int idc_tipo, int folio)
        {
            DataTable dt = new DataTable();
            try
            {
                CombustibleENT entidad = new CombustibleENT();
                CombustibleCOM componente = new CombustibleCOM();
                entidad.Pfolio = folio;
                entidad.Ptipofolio = idc_tipo;
                DataSet ds = componente.ComprobarFolio(entidad);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        /// <summary>
        /// Crea un arhcivo de texto en la carpeta temporal de windows
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool WriteFile(string path, string content)
        {
            // crear el path
            //var archivo = "C:\\folder\\fichero.txt";
            try
            {
                var archivo = path;
                // eliminar el fichero si ya existe
                if (File.Exists(archivo))
                    File.Delete(archivo);

                // crear el fichero
                using (var fileStream = File.Create(archivo))
                {
                    var texto = new UTF8Encoding(true).GetBytes(content);
                    fileStream.Write(texto, 0, texto.Length);
                    fileStream.Flush();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string id_aleatorio(int longitudnuevacadena = 5)
        {
            Random obj = new Random();
            string posibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int longitud = posibles.Length;
            char letra;
            string nuevacadena = "";
            for (int i = 0; i < longitudnuevacadena; i++)
            {
                letra = posibles[obj.Next(longitud)];
                nuevacadena += letra.ToString();
            }
            return nuevacadena;
        }

        public static Boolean EsFecha(String fecha)
        {
            try
            {
                DateTime.Parse(fecha);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Boolean permiso(int usuario, int opcion)
        {
            DataTable dt = new DataTable();
            OpcionesBL opcsBL = new OpcionesBL();
            Boolean val = false;
            try
            {
                string user = Convert.ToString(usuario);
                string opcs = Convert.ToString(opcion);

                string[,] parametros = new string[,] { { "numero", user }, { "numero", opcs } };
                string query = build_funcion("fn_existe_opcion_usuario", parametros, 2, "permiso");

                dt = opcsBL.preparar_funcion(query);
                DataRow row = dt.Rows[0];
                Boolean res = Convert.ToBoolean(row["permiso"].ToString());
                if (res == true || usuario == 314 || usuario == 127)
                {
                    val = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return val;
        }

        public static string build_funcion(string nombre, string[,] parametros, int longparametros, string alias)
        {
            string consulta;
            if (longparametros != parametros.Length / 2)
            {
                consulta = "";
                return consulta;
            }
            string param;
            // read elements in a multidimensional array
            if (parametros.Length > 0)
            {
                param = "("; //inicio
                for (int i = 0; i < longparametros; i++)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        //System.Console.Write(parametros[i,j]);
                        //evaluamos que tipo de dato es
                        switch (parametros[i, 0])
                        {
                            case "texto":
                                param = param + "'" + parametros[i, 1] + "'"; //se encierra entre comillas simples
                                break;

                            case "numero":
                                param = param + parametros[i, 1]; //no se encierra entre comillas
                                break;

                            default:
                                param = param + "'" + parametros[i, 1] + "'"; //se encierra entre comillas simples
                                break;
                        } //fin switch
                        param = param + ",";
                    }
                }
                //quitamos la ultima coma
                param = param.Remove(param.Length - 1, 1);

                param = param + ")"; //cierre
            }
            else
            {
                param = "()";
            }
            consulta = "SELECT dbo." + nombre + " " + param + " as " + alias;
            return consulta;
        }

        /// <summary>
        ///  valida que un campo de texto o bien la cadena no sea cero
        /// </summary>
        /// <param name="campo"></param>
        /// <returns> true si esta vacío y false si no esta vacío</returns>
        public static Boolean campo_vacio(string campo)
        {
            Boolean validate = true; //es campo vacio por defecto ;
            string valor = campo.Trim();
            if (valor.Length > 0)
            {
                validate = false; //decimos que no esta vacio
            }
            return validate;
        }

        public static string de64aTexto(string cadena)
        {
            string enviar;
            var base641 = cadena;
            var data = Convert.FromBase64String(base641);
            enviar = Encoding.UTF8.GetString(data);
            return enviar;
        }

        public static string deTextoa64(string cadena)
        {
            string enviar;
            var bytes = Encoding.UTF8.GetBytes(cadena);
            enviar = Convert.ToBase64String(bytes);
            return enviar;
        }

        public static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters,
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// revisa en la base de datos si un usuario dado tiene autorizacion segun el id de la autorizacion
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="tipo_autorizacion"></param>
        /// <returns></returns>
        public static Boolean autorizacion(int usuario, int tipo_aut)
        {
            DataTable dt = new DataTable();
            OpcionesBL opcsBL = new OpcionesBL();
            Boolean val = false;
            try
            {
                string user = Convert.ToString(usuario);
                string aut = Convert.ToString(tipo_aut);

                string[,] parametros = new string[,] { { "numero", aut }, { "numero", user } };
                string query = build_funcion("fn_autorizacion_usu", parametros, 2, "autorizado");

                dt = opcsBL.preparar_funcion(query);
                DataRow row = dt.Rows[0];
                Boolean res = Convert.ToBoolean(row["autorizado"].ToString());
                if (res == true || usuario == 314)
                {
                    val = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return val;
        }

        /// <summary>
        /// devuelve string en hexadecimal
        /// </summary>
        /// <param name="asciiString"></param>
        /// <returns></returns>
        public static string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }

        /// <summary>
        /// devuelve hexadecimal en string
        /// </summary>
        /// <param name="HexValue"></param>
        /// <returns></returns>
        public static string ConvertHexToString(string HexValue)
        {
            string StrValue = "";
            while (HexValue.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }
            return StrValue;
        }

        /// <summary>
        /// Retorna la IP desde donde se esta ejecutando el sistema
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("");
        }

        /// <summary>
        /// Retorna el nombre de PC desde donde se esta ejecutando el sistema, si es de un equipos celular, retorna
        /// eso.
        /// </summary>
        /// <returns></returns>
        public static string GetPCName()
        {
            try
            {
                string Name = Environment.MachineName.ToString();
                if (Name == null | Name.Equals(string.Empty))
                {
                    Name = "With SmartPhone";
                }
                return Name;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Retorna el nombre de Usuario de Red que esta ejecutando el sistema, si es de un equipos celular, retorna
        /// eso.
        /// </summary>
        /// <returns></returns>
        public static string GetUserName()
        {
            try
            {
                string Name = Environment.UserName.ToString();
                if (Name == null | Name.Equals(string.Empty))
                {
                    Name = "With SmartPhone";
                }
                return Name;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}