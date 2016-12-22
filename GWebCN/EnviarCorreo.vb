Imports System.Net.Mail
Imports System.Net
Imports System.Net.Mime
Public Class EnviarCorreo
    Public Sub Enviar_Correo(ByVal correo As MailMessage, ByVal idc_usuario As Integer, ByVal tipo As Integer)
        Dim ds As New DataSet()
        Dim parametros As String() = {"@pidc_usuario", "@PTIPO"}
        Dim valores As Object() = {idc_usuario, tipo}
        Dim nombre_mostrar As String = ""
        Dim cuenta As String = ""
        Dim contraseña As String = ""
        Dim puerto As Integer = 0
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_correo_contraseña", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                nombre_mostrar = Convert.ToString(ds.Tables(0).Rows(0)(2))
                cuenta = Convert.ToString(ds.Tables(0).Rows(0)(0))
                contraseña = desencripta(Convert.ToString(ds.Tables(0).Rows(0)(1)))
            Else

                If ds.Tables(1).Rows.Count > 0 Then
                    nombre_mostrar = Convert.ToString(ds.Tables(1).Rows(0)(2))
                    cuenta = Convert.ToString(ds.Tables(1).Rows(0)(0))
                    contraseña = desencripta(Convert.ToString(ds.Tables(1).Rows(0)(1)))
                End If
            End If

            If cuenta <> "" Then
                puerto = correo_puerto()
                correo.From = New MailAddress(cuenta, nombre_mostrar, System.Text.Encoding.UTF8)
                correo.Bcc.Add("sistemas@gamamateriales.com.mx," & cuenta)
                correo.IsBodyHtml = True
                Dim BasicAuthenticationInfo As New NetworkCredential(cuenta, contraseña)
                Dim smtp As New SmtpClient("smtp.gamamateriales.com.mx")
                smtp.UseDefaultCredentials = True
                smtp.Credentials = BasicAuthenticationInfo
                smtp.Port = puerto
                smtp.EnableSsl = False
                smtp.Timeout = 500000
                smtp.Send(correo)
                correo.Attachments.Dispose()
                correo.Dispose()
                correo = Nothing

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function desencripta(ByVal contra As String) As String
        Dim gw As New GWebCN.Encripta
        Dim ds As New DataSet()
        Dim vclave2 As String = ""
        Dim vclave1 As String = ""
        Dim vcar As String = Nothing
        Dim vbus As Integer = 0
        Dim vncar As String = ""
        Try
            ds = gw.des_encripta()

            If ds.Tables(0).Rows.Count > 0 Then
                vclave2 = Convert.ToString(ds.Tables(0).Rows(0)(0))
                vclave1 = Convert.ToString(ds.Tables(0).Rows(0)(1))
            End If
            For i As Integer = 0 To (contra.Length) - 1
                vcar = contra.Substring(i, 1)
                vbus = vclave1.IndexOf(vcar)
                vncar = vncar & vclave2.Substring(vbus, 1)
            Next

            Return vncar.Trim()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function correo_puerto() As Integer
        Dim dt As New DataTable()
        Dim puerto As Integer = 0
        Try
            dt = GWebCD.clsConexion.EjecutaConsulta("SELECT TOP 1 puerto FROM correo_puerto WITH (nolock)")
            'Datos("SELECT TOP 1 puerto FROM correo_puerto WITH (nolock)")
            If dt.Rows.Count > 0 Then
                puerto = Convert.ToInt32(dt.Rows(0)(0))
            End If

            Return puerto
        Catch ex As Exception
            Throw ex
        End Try


    End Function
End Class
