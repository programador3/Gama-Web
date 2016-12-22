Imports System.Data
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports negocio.Componentes

Partial Class Reporting
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then



            Dim idc_reporting As Integer = Request.QueryString("idc")
            lbltitulo.Text = Request.QueryString("caption")

            If idc_reporting = Nothing Then
                'cerrar reporte
            Else
                cargar_reporte(idc_reporting)
            End If

            ReportViewer1.Attributes("onclick") = "return rezoom();"
            Dim url As String
            If Request.UrlReferrer Is Nothing Then
                url = "menu.aspx"
            Else
                url = Request.UrlReferrer.ToString()
            End If


            imcerrar.Attributes("urlanterior") = url




        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>reporte();</script>", False)
    End Sub

    Sub cargar_reporte(ByVal idc_reporting As Integer)
        Try
            Dim parametros() As ReportParameter = Session("reporte_parametros")

            ReportViewer1.SizeToReportContent = True
            ReportViewer1.BackColor = Drawing.Color.Gainsboro

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
            Dim serverreport As ServerReport
            serverreport = ReportViewer1.ServerReport
            serverreport.ReportServerUrl = New Uri("http://192.168.0.4/reportserver/")
            Dim cadconexion As String
            Dim cs As String = System.Configuration.ConfigurationManager.AppSettings("cs")
            If cs = "P" Then
                cadconexion = datos.recursos.cadena_conexion
            Else
                cadconexion = datos.recursos.cadena_conexion_respa
            End If
            serverreport.ReportPath = report_path(idc_reporting)
            'Create the sales order number report parameter
            'Dim cnn As New SqlClient.SqlConnection(System.Configuration.ConfigurationSettings.AppSettings("strDeConexion"))
            'Dim cadconexion As String = System.Configuration.ConfigurationManager.AppSettings("strDeConexion")


            Dim params3 As New ReportParameter()
            params3.Name = "cadconexion"
            'params3.Values.Add("Data Source=192.168.0.4;Initial Catalog=gm")
            params3.Values.Add(cadconexion)
            Dim parameters As ReportParameter() = {params3}
            If Not parametros Is Nothing Then
                Array.Resize(parameters, parametros.Length + 1)
                For i As Integer = 1 To parametros.Length
                    parameters(i) = parametros(i - 1)
                Next
            End If
            serverreport.SetParameters(parameters)
            ReportViewer1.ServerReport.Refresh()
        Catch ex As Exception
            CargarMsgBox("Error al Cargar Informe. \n  \u000B \n" & ex.Message)
        End Try
    End Sub

    Public Function report_path(ByVal idc_reponting As Integer) As String
        Dim gweb As New AgentesCOM
        Dim ds As New DataSet
        Dim ruta_nombre As String = ""
        Try
            ds = gweb.sp_reporting(idc_reponting)
            If ds.Tables(0).Rows.Count > 0 Then
                ruta_nombre = ds.Tables(0).Rows(0).Item("ruta").ToString.Trim & ds.Tables(0).Rows(0).Item("nombre").ToString.Trim
                Return ruta_nombre
            Else
                Return ruta_nombre
            End If
        Catch ex As Exception
            Return ruta_nombre
        End Try
    End Function

    Sub CargarMsgBox(ByVal mensaje As String)
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>alert(' " & mensaje.Replace("'", "") & " ');</script>", False)
    End Sub
    Protected Sub ReportViewer1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReportViewer1.Load
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>reporte();</script>", False)
    End Sub

    Protected Sub imcerrar_Click(sender As Object, e As ImageClickEventArgs) Handles imcerrar.Click
        Dim url As String = imcerrar.Attributes("urlanterior")
        If url <> "" Then
            Response.Redirect(url)
        End If
    End Sub
End Class
