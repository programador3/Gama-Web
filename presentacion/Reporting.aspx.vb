Imports System.Data
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Partial Class Reporting
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            'Dim Encriptado As TSHAK.Components.SecureQueryString
            'Encriptado = New TSHAK.Components.SecureQueryString(New Byte() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 8}, Request("data"))
            'Dim vrfc As String = Encriptado("rfc")
            'Dim vid As Integer = CInt(Encriptado("id"))
            'Dim vcliente As String = Encriptado("cliente")
            'Dim gweb As New GWebCN.Reportes
            'Dim conv As New GWebCN.Conversiones
            'Dim ds As New Data.DataSet
            'Dim nombre, ruta, vruta As String
            'Session("xreportserver") = "http://192.168.0.4/ReportServer/Pages/ReportViewer.aspx?"
            'Session("xcadconexion") = "Data Source=192.168.0.4;Initial Catalog=gm"
            'ds = gweb.ruta_nombre_reporte(49)
            'If ds.Tables(0).Rows.Count > 0 Then
            '    nombre = ds.Tables(0).Rows(0).Item("nombre")
            '    ruta = ds.Tables(0).Rows(0).Item("ruta")
            '    vruta = Session("xreportserver") & conv.UrlEncoding(ruta).Trim + nombre
            '    vruta = vruta & "&rs:Command=Render"
            '    vruta = vruta & "&pidc_cliente=" & vid
            '    vruta = vruta & "&pformato=0"
            '    vruta = vruta & "&pprecios=0"
            '    vruta = vruta & "&pdescuentos=1"
            '    vruta = vruta & "&pcadena="
            '    vruta = vruta & "&plogo=1"
            '    vruta = vruta & "&pcliente=" & conv.UrlEncoding(vcliente).Trim
            '    vruta = vruta & "&prfc=" & conv.UrlEncoding(vrfc).Trim
            '    vruta = vruta + "&cadconexion=" + conv.UrlEncoding(Session("xcadconexion"))
            '    vruta = vruta + "&rc:parameters=false"
            '    vruta = vruta + "&rc:UniqueGuid=<newguid>"
            '    Me.Reporting.Attributes("src") = vruta
            'Else
            '    Return
            'End If




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


            serverreport.ReportPath = report_path(idc_reporting)
            'Create the sales order number report parameter
            'Dim cnn As New SqlClient.SqlConnection(System.Configuration.ConfigurationSettings.AppSettings("strDeConexion"))
            Dim cadconexion As String = System.Configuration.ConfigurationManager.AppSettings("strDeConexion")


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
        Dim gweb As New GWebCN.Reportes
        Dim ds As New DataSet
        Dim ruta_nombre As String = ""
        Try
            ds = gweb.ruta_nombre_reporte(idc_reponting)
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


    'Try


    '    ReportViewer1.SizeToReportContent = True
    '    ReportViewer1.BackColor = Drawing.Color.Gainsboro

    '    ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
    '    Dim serverreport As ServerReport
    '    serverreport = ReportViewer1.ServerReport
    '    serverreport.ReportServerUrl = New Uri("http://192.168.0.4/reportserver/")
    '    serverreport.ReportPath = "/Informes/Almacen/suc_dif_cercana"


    '    'Create the sales order number report parameter
    '    Dim params As New ReportParameter()
    '    params.Name = "pfechai"
    '    params.Values.Add("2012/10/30 12:00:00 AM")

    '    Dim params2 As New ReportParameter()
    '    params2.Name = "pfechaf"
    '    params2.Values.Add("2012/11/01 12:00:00 AM")

    '    Dim params3 As New ReportParameter()
    '    params3.Name = "cadconexion"
    '    params3.Values.Add("Data Source=192.168.0.5;Initial Catalog=gm")

    '    '&=&=Data+Source%3D192%2E168%2E0%2E5%3BInitial+Catalog%3Dgm

    '    'Set the report parameters for the report
    '    Dim parameters() As ReportParameter = {params, params2, params3}
    '    serverreport.SetParameters(parameters)
    '    ReportViewer1.ServerReport.Refresh()
    '    'Dim td As New HtmlTableCell
    '    'td = DirectCast(ReportViewer1.FindControl("oReportCell"), HtmlTableCell)
    '    'td.Width = 100%




    '    'ReportViewer1.SizeToReportContent = True
    '    'ReportViewer1.BackColor = Drawing.Color.Gainsboro

    '    'ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
    '    'Dim serverreport As ServerReport
    '    'serverreport = ReportViewer1.ServerReport
    '    'serverreport.ReportServerUrl = New Uri("http://192.168.0.4/reportserver")
    '    'serverreport.ReportPath = "/Informes/Ventas/venxped_ped_det"


    '    ''Create the sales order number report parameter
    '    'Dim params As New ReportParameter()
    '    'params.Name = "pfechai"
    '    'params.Values.Add("2012/10/30 12:00:00 AM")

    '    'Dim params2 As New ReportParameter()
    '    'params2.Name = "pfechaf"
    '    'params2.Values.Add("2012/11/01 12:00:00 AM")

    '    'Dim params3 As New ReportParameter()
    '    'params3.Name = "cadconexion"
    '    'params3.Values.Add("Data Source=192.168.0.5;Initial Catalog=gm")

    '    'Dim params4 As New ReportParameter()
    '    'params4.Name = "pidc_sucursal"
    '    'params4.Values.Add("1")


    '    'Dim params5 As New ReportParameter()
    '    'params5.Name = "pidc_cliente"
    '    'params5.Values.Add("0")

    '    'Dim params6 As New ReportParameter()
    '    'params6.Name = "pidc_usuario"
    '    'params6.Values.Add("127")

    '    'Dim params7 As New ReportParameter()
    '    'params7.Name = "pgrupo"
    '    'params7.Values.Add("[TODOS]")

    '    'Dim params8 As New ReportParameter()
    '    'params8.Name = "pcliente"
    '    'params8.Values.Add("TODOS")

    '    'Dim params9 As New ReportParameter()
    '    'params9.Name = "psucursal"
    '    'params9.Values.Add("CEDIS MONTERREY")



    '    ''&=&=Data+Source%3D192%2E168%2E0%2E5%3BInitial+Catalog%3Dgm

    '    ''Set the report parameters for the report
    '    'Dim parameters() As ReportParameter = {params, params2, params3, params4, params5, params6, params7, params8, params9}
    '    'serverreport.SetParameters(parameters)
    '    'ReportViewer1.ServerReport.Refresh()
    '     Catch ex As Exception
    '    Throw ex
    'End Try



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
