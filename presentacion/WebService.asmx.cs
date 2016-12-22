using negocio.Componentes;
using negocio.Entidades;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Services;

namespace presentacion
{
    /// <summary>
    /// Descripción breve de WebService
    /// </summary>
    [WebService(Namespace = "http://gama.website/puestos/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        ///////////////////////////////////////////////////
        //utilizado para ws
        private string username = "";

        //password
        private string password = "";

        //el puerto para pop de gmail es el 995
        private int port;

        private int portsmtp;

        //el host de pop de gmail es pop.gmail.com
        private string hostname = "";

        private string hostnamesmtp = "";

        //esta opción debe ir en true
        private bool useSsl = false;

        //correo quien recibira copias
        private string correo_copia = "";

        //correo quien recibira copias si hay un error
        private string correo_error = "programador3@gamamateriales.com.mx";

        private string path = "";

        private List<string> ccerror = new List<string>();

        public DataTable tabla_extensiones = new DataTable();

    



        [WebMethod]
        public string EjecutarCheckMail()
        {
            CargaDatos();
            return CheckMail();
        }

        [System.Web.Services.WebMethod]
        public int NotificationsPendientes(int idc_usuario)
        {
            NotificacionesENT ent = new NotificacionesENT();
            ent.Idc_usuario = idc_usuario;
            NotificacionesCOM com = new NotificacionesCOM();
            DataTable dt = com.CargaNotificaciones(ent).Tables[0];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "pendiente not like '%Ticket de Servicio en Espera%'";
            int total = dv.ToTable().Rows.Count;
            return total;
        }

        [System.Web.Services.WebMethod]
        public int TicketsPendientes(int idc_usuario)
        {
            NotificacionesENT ent = new NotificacionesENT();
            ent.Idc_usuario = idc_usuario;
            NotificacionesCOM com = new NotificacionesCOM();
            DataTable dt = com.CargaNotificaciones(ent).Tables[0];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "pendiente like '%Ticket de Servicio en Espera%'";
            int total = dv.ToTable().Rows.Count;
            return total;
        }
        [WebMethod]
        public List<String> TicketNotificaciones(int idc_puesto, int idc_usuario)
        {
            List<String> ret = new List<String>();
            DataSet ds = new DataSet();
            try
            {
                ret.Add("");
                TicketsCapturaCOM componente = new TicketsCapturaCOM();
                ds = componente.sp_tickets_sinleer(idc_puesto, idc_usuario); //funcion que valida la existencia del usuario
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ret[0] = ("");
                    foreach (DataRow row in dt.Rows)
                    {
                        ret.Add(row["idc_tareaser"].ToString().Trim());
                        ret.Add(row["tipo"].ToString().Trim());
                        ret.Add(row["fecha"].ToString().Trim());
                    }

                }                
                return ret;
            }
            catch (Exception ex)
            {
                ret[0] = ("ERROR EN WEBSERVICE " + ex.ToString().Trim());
                return ret;
            }

        }

        [WebMethod]
        public List<String> InfotTicketsPendientes(int idc_puesto)
        {
            List<String> ret = new List<String>();
            UsuariosBL CompUsuario = new UsuariosBL();
            DataSet ds = new DataSet();
            try
            {
                ret.Add("");
                ds = CompUsuario.TicketsPendietes(idc_puesto);  //funcion que valida la existencia del usuario
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        ret[0] = ("");
                        ret.Add(row["idc_ticketserv"].ToString().Trim());
                        ret.Add(row["empleado"].ToString().Trim());
                        ret.Add(row["fecha"].ToString().Trim());
                        ret.Add(row["descripcion"].ToString().Trim());
                        ret.Add(row["imgempleado"].ToString().Trim());
                        i++;
                    }
                }
                else
                {
                    ret[0] = ("NO HAY DATOS");
                }
                return ret;
            }
            catch (Exception ex)
            {
                ret[0] = ("ERROR EN WEBSERVICE " + ex.ToString().Trim());
                return ret;
            }

        }
        [WebMethod]
        public List<String> InfotTickets(int idc_ticketserv)
        {
            List<String> ret = new List<String>();
            UsuariosBL CompUsuario = new UsuariosBL();
            DataSet ds = new DataSet();
            try
            {
                ret.Add("");
                ds = CompUsuario.TicketsiNFO(idc_ticketserv);  //funcion que valida la existencia del usuario
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ret[0] = ("");
                    DataRow row = dt.Rows[0];
                    ret.Add(row["idc_ticketserv"].ToString().Trim());
                    ret.Add(row["descripcion"].ToString().Trim());
                    ret.Add(row["fecha"].ToString().Trim());
                    ret.Add(row["url"].ToString().Trim());
                    ret.Add(row["imgempleado"].ToString().Trim());
                    ret.Add(row["empleado"].ToString().Trim());
                    ret.Add(row["depto"].ToString().Trim());
                    ret.Add(row["status"].ToString().Trim());
                    ret.Add(row["empleado_atiende"].ToString().Trim());
                    ret.Add(row["depto_atiende"].ToString().Trim());
                    ret.Add(row["idc_puesto_solicito"].ToString().Trim());
                    ret.Add(row["idc_ticketserva"].ToString().Trim());
                    ret.Add(row["tipo"].ToString().Trim());
                }
                else
                {
                    ret[0] = ("NO HAY DATOS");
                }
                return ret;
            }
            catch (Exception ex)
            {
                ret[0] = ("ERROR EN WEBSERVICE " + ex.ToString().Trim());
                return ret;
            }

        }
        [WebMethod]
        public List<String> TareasServiciosDrop(int idc_puesto)
        {
            List<String> ret = new List<String>();
            DataSet ds = new DataSet();
            try
            {
                ret.Add("");
                TicketsCapturaCOM componente = new TicketsCapturaCOM();
                ds = componente.sp_tareas_servicios_puestos(idc_puesto); //funcion que valida la existencia del usuario
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ret[0] = ("");
                    foreach (DataRow row in dt.Rows)
                    {
                        ret.Add(row["idc_tareaser"].ToString().Trim());
                        ret.Add(row["desc_corta"].ToString().Trim());
                        ret.Add(row["descripcion"].ToString().Trim());
                    }

                }
                return ret;
            }
            catch (Exception ex)
            {
                ret[0] = ("ERROR EN WEBSERVICE " + ex.ToString().Trim());
                return ret;
            }

        }


        [WebMethod]
        public List<String> InfotTicketsAll(int idc_puesto)
        {
            List<String> ret = new List<String>();
            UsuariosBL CompUsuario = new UsuariosBL();
            DataSet ds = new DataSet();
            try
            {
                ret.Add("");
                ds = CompUsuario.TicketsiNFOALL(idc_puesto);  //funcion que valida la existencia del usuario
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ret[0] = ("");
                    foreach (DataRow row in dt.Rows)
                    {
                        ret.Add(row["idc_ticketserv"].ToString().Trim());
                        ret.Add(row["empleado"].ToString().Trim());
                        ret.Add(row["fecha"].ToString().Trim());
                        ret.Add(row["descripcion"].ToString().Trim());
                        ret.Add(row["imgempleado"].ToString().Trim());
                        ret.Add(row["tipo"].ToString().Trim());
                    }

                }
                else
                {
                    ret[0] = ("SIN DATOS");
                }
                return ret;
            }
            catch (Exception ex)
            {
                ret[0] = ("ERROR EN WEBSERVICE " + ex.ToString().Trim());
                return ret;
            }

        }
        [WebMethod]
        public List<String> InfotTicketsHistorialk(int idc_puesto)
        {
            List<String> ret = new List<String>();
            UsuariosBL CompUsuario = new UsuariosBL();
            DataSet ds = new DataSet();
            try
            {
                ret.Add("");
                ds = CompUsuario.TicketsiNFOHistorial(idc_puesto);  //funcion que valida la existencia del usuario
                DataTable dt = ds.Tables[0];
                DataView dv = dt.DefaultView;
                dv.Sort = "idc_ticketserv asc";
                DataTable sortedDT = dv.ToTable();
                if (sortedDT.Rows.Count > 0)
                {
                    ret[0] = ("");
                    foreach (DataRow row in sortedDT.Rows)
                    {
                        ret.Add(row["idc_ticketserv"].ToString().Trim());
                        ret.Add(row["empleado"].ToString().Trim());
                        ret.Add(row["fecha"].ToString().Trim());
                        ret.Add(row["descripcion"].ToString().Trim());
                        ret.Add(row["imgempleado"].ToString().Trim());
                        ret.Add(row["tipo"].ToString().Trim());
                        ret.Add(row["estado"].ToString().Trim());
                    }

                }
                return ret;
            }
            catch (Exception ex)
            {
                ret[0] = ("ERROR EN WEBSERVICE " + ex.ToString().Trim());
                return ret;
            }

        }
        [WebMethod]
        public List<String> TerminarTicket(int idpuesto, string username, string pass,string observaciones, int idc_ticketserva)
        {
            List<String> ret = new List<String>();
            string vmensaje;
            try
            {
                ticket_servCOM com = new ticket_servCOM();
                ticket_servENT ent = new ticket_servENT();
                ent.Pidc_puesto = idpuesto;
                DataSet ds = com.Usuarios_puesto(ent);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "usuario ='"+username.Trim()+"'";
                    DataTable dtr = dv.ToTable();
                    if (dtr.Rows.Count > 0)
                    {
                        int IDC_USUARIO = Convert.ToInt32(dtr.Rows[0]["idc_usuario"]);
                        ent.Pidc_ticketserva = idc_ticketserva;
                        ent.Pidc_puesto = idpuesto;
                        Datos_Usuario_logENT dul = new Datos_Usuario_logENT();
                        dul.Pidc_usuario = IDC_USUARIO;
                        ent.Pobservaciones = observaciones;
                        ent.Pcontraseña = pass;
                        ent.Pidc_usuario_term = IDC_USUARIO;
                        ds = com.ticket_serv_aten(ent, dul);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    }
                    else
                    {
                        vmensaje = ("USUARIO "+username+" NO RELACIONADO AL TICKET");
                    }
                }
                else
                {
                    vmensaje = ("NO HAY USUARIOS PARA ESTE PUESTO");
                }
                ret.Add(vmensaje);
                return ret;
            }
            catch (Exception ex)
            {
                vmensaje = ("ERROR EN WEBSERVICE " + ex.ToString().Trim());
                ret.Add(vmensaje);
                return ret;
            }
        }
        [WebMethod]
        public List<String> GuardarrTicket(int idc_puesto, int idc_tareaser,string obser, int idc_usuario)
        {
            List<String> ret = new List<String>();
            string vmensaje;
            try
            {
                TicketsCapturaCOM componente = new TicketsCapturaCOM();
                DataSet ds = componente.sp_aticketserv(idc_puesto, idc_tareaser, obser, idc_usuario,"");
                vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                ret.Add(vmensaje);
                return ret;
            }
            catch (Exception ex)
            {
                vmensaje = ("ERROR EN WEBSERVICE " + ex.ToString().Trim());
                ret.Add(vmensaje);
                return ret;
            }
        }


        [WebMethod]
        public List<String> CancelarTicketAtenido(int Pidc_ticketserva, int idc_puesto, int IDC_USUARIO, string motivo)
        {
            List<String> ret = new List<String>();
            string vmensaje="";
            try
            {
                if (IDC_USUARIO == 0)
                {
                    vmensaje = "EL USUARIO ESTA EN 0 "+IDC_USUARIO.ToString();
                }
                else {
                    ticket_servENT ent = new ticket_servENT();
                    Datos_Usuario_logENT dul = new Datos_Usuario_logENT();
                    ticket_servCOM com = new ticket_servCOM();
                    dul.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    dul.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    dul.Pusuariopc = funciones.GetUserName();//usuario pc
                    dul.Pidc_usuario = IDC_USUARIO;
                    ent.Pmotivo = motivo;
                    ent.Pidc_ticketserva = Pidc_ticketserva;
                    ent.Pidc_puesto = idc_puesto;
                    ent.Pobservaciones = "";
                    ent.Pcontraseña = "";
                    ent.Pidc_usuario_term = 0;
                    DataSet ds = com.ticket_serv_aten(ent, dul);
                    vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                }
                ret.Add(vmensaje);
                return ret;
            }
            catch (Exception ex)
            {
                vmensaje = ("ERROR EN WEBSERVICE " + ex.ToString().Trim());
                ret.Add(vmensaje);
                return ret;
            }
        }

        [WebMethod]
        public List<String> CancelarTicket(int Pidc_ticketserv, int idc_puesto, int IDC_USUARIO, string motivo)
        {
            List<String> ret = new List<String>();
            string vmensaje = "";
            try
            {
                if (IDC_USUARIO == 0)
                {
                    vmensaje = "EL USUARIO ESTA EN 0 " + IDC_USUARIO.ToString();
                }
                else
                {
                    ticket_servENT ent = new ticket_servENT();
                    Datos_Usuario_logENT dul = new Datos_Usuario_logENT();
                    ticket_servCOM com = new ticket_servCOM();
                    dul.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                    dul.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                    dul.Pusuariopc = funciones.GetUserName();//usuario pc
                    dul.Pidc_usuario = IDC_USUARIO;
                    ent.Pmotivo = motivo;
                    ent.Pidc_ticketserv = Pidc_ticketserv;
                    ent.Pidc_puesto = idc_puesto;
                    ent.Pobservaciones = "";
                    ent.Pcontraseña = "";
                    ent.Pidc_usuario_term = 0;
                    DataSet ds = new DataSet();
                    if (motivo.Length < 10)
                    {
                        vmensaje = "AGREGA EL MOTIVO DE LA CANCELACION DEL TICKET DE SERVICIO (Minimo 10 caracteres";

                    }
                    else {
                        ds = com.ticket_serv_Espera(ent, dul);
                        vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    }
                }
                ret.Add(vmensaje);
                return ret;
            }
            catch (Exception ex)
            {
                vmensaje = ("ERROR EN WEBSERVICE " + ex.ToString().Trim());
                ret.Add(vmensaje);
                return ret;
            }
        }

        [WebMethod]
        public List<String>  TomarTicket(int idc_ticketserv, int idc_puesto, int IDC_USUARIO)
        {
            List<String> ret = new List<String>();
            string vmensaje;
            try
            {
                ticket_servENT ent = new ticket_servENT();
                Datos_Usuario_logENT dul = new Datos_Usuario_logENT();
                ticket_servCOM com = new ticket_servCOM();
                dul.Pdirecip = funciones.GetLocalIPAddress(); //direccion ip de usuario
                dul.Pnombrepc = funciones.GetPCName();//nombre pc usuario
                dul.Pusuariopc = funciones.GetUserName();//usuario pc
                dul.Pidc_usuario = IDC_USUARIO;
                ent.Pmotivo = "";
                ent.Pidc_ticketserv = idc_ticketserv;
                ent.Pidc_puesto = idc_puesto;
                DataSet ds = com.ticket_serv_Espera(ent, dul);
                vmensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                ret.Add(vmensaje);
                return ret;
            }
            catch (Exception ex)
            {
                vmensaje = ("ERROR EN WEBSERVICE " + ex.ToString().Trim());
                ret.Add(vmensaje);
                return ret;
            }
        }
        [WebMethod]
        public List<String> VerificarAsistencia(int num_nomina)
        {
            List<String> ret = new List<String>();
            string vmensaje;
            try
            {
                AsistenciaCOM componentes = new AsistenciaCOM();
                if (num_nomina > 0)
                {
                    DataSet ds = componentes.sp_status_incidencia_dia_numnomina(num_nomina, DateTime.Today);
                    vmensaje = ds.Tables[0].Rows[0]["estatus"].ToString();
                }
                else {
                    vmensaje = "EL PUESTO NO TIENE EMPLEADO";
                }
                ret.Add(vmensaje);
                return ret;
            }
            catch (Exception ex)
            {
                vmensaje = ("ERROR EN WEBSERVICE " + ex.ToString().Trim());
                ret.Add(vmensaje);
                return ret;
            }
        }
        [WebMethod]        
        public string[] IniciarSesion(string usuario, string contra)
        {
            string[] ret = new string[6];
            UsuariosE EntUsuario = new UsuariosE();
            UsuariosBL CompUsuario = new UsuariosBL();
            EntUsuario.Usuario = usuario;
            EntUsuario.Contraseña = contra;
            DataSet ds = new DataSet();
            try
            {
                ret[0] = ("");
                ret[1] = ("0");
                ret[2] = ("0");
                ret[3] = ("0");
                ret[4] = ("0");
                ret[5] = ("0");
                ds = CompUsuario.validar_usuarios(EntUsuario);  //funcion que valida la existencia del usuario
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["idc_usuario"]) > 0)
                    {
                        ret[0] = ("");
                        ret[1] = (dt.Rows[0]["nombre"].ToString().Trim());
                        ret[2] = (dt.Rows[0]["puesto"].ToString().Trim());
                        ret[3] = (dt.Rows[0]["idc_puesto"].ToString().Trim());
                        ret[4] = (dt.Rows[0]["idc_usuario"].ToString().Trim());
                        ret[5] = (dt.Rows[0]["idc_empleado"].ToString().Trim());
                    }
                    else {
                        ret[0] = ("Usuario o Contraseña Invalidos");
                    }
                }
                else {
                    ret[0] = ("Usuario o Contraseña Invalidos");
                }
                return ret;
            }
            catch (Exception ex)
            {
                ret[0] = ("ERROR EN WEBSERVICE "+ex.ToString().Trim());
                return ret;
            }

        }

        public class LoginSession {
            public string mess; 
            public string idc_usuario;
            public string idc_puesto;
            public string nombre;
            public string puesto;
        }

        public void CargaDatos()
        {
            try
            {
                UsuariosBL componente = new UsuariosBL();
                DataSet ds = componente.CargaInformacionInicial();
                DataRow row = ds.Tables[0].Rows[0];
                username = row["USERNAME"].ToString();
                string[] CCId = row["ERRORCC"].ToString().Split(';');
                foreach (string CCEmail in CCId)
                {
                    ccerror.Add(CCEmail);
                }
                correo_error = row["USERNAME"].ToString();
                password = row["PASS"].ToString();
                hostname = row["HOST"].ToString();
                hostnamesmtp = row["HOSTSMTP"].ToString();
                path = row["RUTA"].ToString();
                port = Convert.ToInt32(row["PORT"]);
                portsmtp = Convert.ToInt32(row["PORTSMTP"]);
                useSsl = Convert.ToBoolean(row["USESSL"]);
                tabla_extensiones = ds.Tables[1];
            }
            catch (Exception ex)
            {
                SendMail("Error en CheckMail el dia " + DateTime.Now.ToString(), "LA APLICACIÓN CHECKMAIL REALIZO UNA EJECUCION ARROJANDO LA SIGUIENTE EXCEPCIÓN;" + System.Environment.NewLine + ex.ToString(), correo_error, new List<string>());
            }
        }

        public int Exist(string filtro)
        {
            int total = 0;
            try
            {
                int i = tabla_extensiones.Rows.Count;
                DataView view = tabla_extensiones.DefaultView;
                view.RowFilter = "V1 LIKE '%" + filtro + "%'";
                total = view.ToTable().Rows.Count;
                return total;
            }
            catch (Exception ex)
            {
                SendMail("Error en CheckMail el dia " + DateTime.Now.ToString(), "LA APLICACIÓN CHECKMAIL REALIZO UNA EJECUCION ARROJANDO LA SIGUIENTE EXCEPCIÓN;" + System.Environment.NewLine + ex.ToString(), correo_error, new List<string>());
                return 0;
            }
        }

        /// <summary>
        /// Envia un correo
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="to"></param>
        public void SendMail(string subject, string body, string to, List<string> listadeadjuntos)
        {
            try
            {
                MailMessage message = new MailMessage(username, to);
                message.Subject = subject;
                message.Body = body;
                SmtpClient mailer = new SmtpClient(hostnamesmtp, portsmtp);
                mailer.Credentials = new NetworkCredential(username, password);
                mailer.EnableSsl = useSsl;
                if (ccerror.Count > 0)
                {
                    foreach (string cc in ccerror)
                    {
                        if (cc != "")
                        {
                            message.CC.Add(cc);
                        }
                    }
                }
                message.CC.Add("programador3@gamamateriales.com.mx");
                foreach (string adjunto in listadeadjuntos)
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(adjunto);
                    attachment.Name = Path.GetFileName(adjunto);
                    message.Attachments.Add(attachment);
                }
                mailer.Send(message);
            }
            catch (Exception ex)
            {
                SendMail("Error en CheckMail el dia " + DateTime.Now.ToString(), "LA APLICACIÓN CHECKMAIL REALIZO UNA EJECUCION ARROJANDO LA SIGUIENTE EXCEPCIÓN;" + System.Environment.NewLine + ex.ToString(), correo_error, new List<string>());

            }
        }

        /// <summary>
        /// Descarga listado de correos y los reenvia
        /// </summary>
        public string CheckMail()
        {
            try
            {
                using (Pop3Client client = new Pop3Client())
                {
                    // conectamos al servidor
                    client.Connect(hostname, port, useSsl);

                    // Autentificación
                    client.Authenticate(username, password);
                    // Obtenemos los Uids mensajes
                    if (client.Connected)
                    {
                        int messageCount = client.GetMessageCount();
                        if (messageCount > 0)
                        {
                            for (int i = messageCount; i > 0; i--)
                            {
                                OpenPop.Mime.Message msg = client.GetMessage(i, null);
                                string value = msg.Headers.Subject;
                                var att = msg.FindAllAttachments();
                                foreach (var ado in att)
                                {
                                    string resultado = ado.FileName.ToString().Replace("/", " ");
                                    resultado = resultado.Replace(@"\", " ");
                                    resultado = resultado.Replace(":", " ");
                                    resultado = resultado.Replace("*", " ");
                                    resultado = resultado.Replace("?", " ");
                                    resultado = resultado.Replace(@"|", " ");
                                    resultado = resultado.Replace("<", " ");
                                    resultado = resultado.Replace(">", " ");
                                    resultado = resultado.Trim();
                                    string extension = Path.GetExtension(resultado);
                                    if (Exist(extension) > 0)
                                    {
                                        ado.Save(new System.IO.FileInfo(System.IO.Path.Combine(path, resultado)));
                                    }
                                }
                                client.DeleteMessage(i);
                            }
                        }
                        return "";
                    }
                    else
                    {
                        return "El Cliente no Pudo conectarse a CORREO:" + username;
                    }
                }
            }
            catch (Exception ex)
            {
                SendMail("Error en CheckMail el dia " + DateTime.Now.ToString(), "LA APLICACIÓN CHECKMAIL REALIZO UNA EJECUCION ARROJANDO LA SIGUIENTE EXCEPCIÓN;" + System.Environment.NewLine + ex.ToString(), correo_error, new List<string>());
                return ex.ToString();
            }
        }

















        /// <summary>
        /// utlizado para ajax
        /// </summary>
        /// <param name="aData"></param>

        [System.Web.Services.WebMethod(EnableSession = true)]
        public void SetSessionValue(List<string> aData)
        {
            String Value = aData[0];
            Session["date_noti"] = Value;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetSessionValue()
        {
            string sessionVal = String.Empty;
            if (Session["date_noti"] != null)
            {
                sessionVal = Session["date_noti"].ToString();
            }
            return sessionVal;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public void SetSessionValueHTML(List<string> aData)
        {
            String Value = aData[0];
            Session["list_not"] = "";
            Session["list_not"] = Value;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetSessionValueHTML()
        {
            string sessionVal = String.Empty;
            if (Session["list_not"] != null)
            {
                sessionVal = Session["list_not"].ToString();
            }
            return sessionVal;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public void SetSessionValueTotal(List<string> aData)
        {
            String Value = aData[0];
            Session["tot_not"] = Value;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetSessionValueTotal()
        {
            string sessionVal = String.Empty;
            if (Session["tot_not"] != null)
            {
                sessionVal = Session["tot_not"].ToString();
            }
            return sessionVal;
        }
    }
}