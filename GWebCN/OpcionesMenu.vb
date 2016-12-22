Public Class OpcionesMenu

    Public Function opciones_usuario(ByVal idc_usuario As Integer) As DataTable
        Try
            Return GWebCD.clsConexion.EjecutaConsulta("select idc_opcion from opciones_usuarios where idc_usuario =" & idc_usuario & "and activo = 1")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function opciones_usuarios(ByVal idc_usuario As Integer, ByVal opciones_menu As Integer()) As DataTable
        Dim dt As New DataTable()
        Dim row As DataRow
        dt.Columns.Add("idc_opcion")
        dt.Columns.Add("descripcion")
        dt.Columns.Add("web_form")
        dt.Columns.Add("web_imagen") ' add 13-01-2016

        Dim dt2 As New DataTable

        For Each c As Integer In opciones_menu
            dt2 = GWebCD.clsConexion.EjecutaConsulta("select opciones.idc_opcion,opciones.descripcion,opciones.web_form, opciones.web_imagen from opciones_usuarios inner join opciones on opciones_usuarios.idc_opcion=opciones.idc_opcion where opciones_usuarios.idc_opcion=" & c.ToString() & " and opciones_usuarios.idc_usuario=" & idc_usuario.ToString() & " and opciones_usuarios.activo=1 and opciones.activo = 1")
            If dt2.Rows.Count > 0 Then
                For i As Integer = 0 To dt2.Rows.Count - 1
                    row = dt.NewRow()
                    row(0) = dt2.Rows(i).Item(0)
                    row(1) = dt2.Rows(i).Item(1)
                    row(2) = dt2.Rows(i).Item(2)
                    row(3) = dt2.Rows(i).Item(3)
                    dt.Rows.Add(row)
                Next
            End If
        Next
        Return dt
    End Function


    Function SubMenuUtilerias(ByVal idc_usuario As Integer) As DataTable
        Try
            Dim dt As New DataTable
            dt.Columns.Add("idc_opcion")
            dt.Columns.Add("descripcion")
            dt.Columns.Add("web_form")
            Dim intopciones() As Integer = {1093}
            Dim i As Integer = 0
            Dim row As DataRow
            Dim row2 As DataRow
            Dim ds As New DataSet
            Dim parametros() As String = {"@idc_opcion", "@idc_usuario"}
            Dim valores(1) As Object
            valores(1) = idc_usuario
            For Each x As Integer In intopciones
                valores(0) = x
                ds = GWebCD.clsConexion.EjecutaSP("Sp_MenuOpciones", parametros, valores)
                If ds.Tables(0).Rows.Count Then
                    row2 = ds.Tables(0).Rows(0)
                    row = dt.NewRow
                    row.Item(0) = row2(0)
                    row.Item(1) = row2(1)
                    row.Item(2) = row2(2)
                    dt.Rows.Add(row)
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function SubMenuAvisos(ByVal idc_usuario As Integer) As DataTable
        Try
            Dim dt As New DataTable
            dt.Columns.Add("idc_opcion")
            dt.Columns.Add("descripcion")
            dt.Columns.Add("web_form")
            Dim intopciones() As Integer = {133, 132}
            Dim i As Integer = 0
            Dim row As DataRow
            Dim row2 As DataRow
            Dim ds As New DataSet
            Dim parametros() As String = {"@idc_opcion", "@idc_usuario"}
            Dim valores(1) As Object
            valores(1) = idc_usuario
            For Each x As Integer In intopciones
                valores(0) = x
                ds = GWebCD.clsConexion.EjecutaSP("Sp_MenuOpciones", parametros, valores)
                If ds.Tables(0).Rows.Count Then
                    row2 = ds.Tables(0).Rows(0)
                    row = dt.NewRow
                    row.Item(0) = row2(0)
                    row.Item(1) = row2(1)
                    row.Item(2) = row2(2)
                    dt.Rows.Add(row)
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Function SubMenuVentas(ByVal idc_usuario As Integer) As DataTable
        Try
            Dim dt As New DataTable
            dt.Columns.Add("idc_opcion")
            dt.Columns.Add("descripcion")
            dt.Columns.Add("web_form")
            Dim intopciones() As Integer = {1163, 148, 66, 1421, 1072}
            Dim i As Integer = 0
            Dim row As DataRow
            Dim row2 As DataRow
            Dim ds As New DataSet
            Dim parametros() As String = {"@idc_opcion", "@idc_usuario"}
            Dim valores(1) As Object
            valores(1) = idc_usuario
            For Each x As Integer In intopciones
                valores(0) = x
                ds = GWebCD.clsConexion.EjecutaSP("Sp_MenuOpciones", parametros, valores)
                If ds.Tables(0).Rows.Count Then
                    row2 = ds.Tables(0).Rows(0)
                    row = dt.NewRow
                    row.Item(0) = row2(0)
                    row.Item(1) = row2(1)
                    row.Item(2) = row2(2)
                    dt.Rows.Add(row)
                End If

                'For Each item As Object In row2.ItemArray
                '    row = dt.NewRow
                '    row.Item(0) = row2(0)
                '    row.Item(1) = row2(1)
                '    row.Item(2) = row2(2)
                '    dt.Rows.Add(row)
                '    Exit For
                'Next
                'If IsDBNull(row2) = True Then
                'Else
                '    row = dt.NewRow
                '    row.Item(0) = row2(0)
                '    row.Item(1) = row2(1)
                '    row.Item(2) = row2(2)
                '    dt.Rows.Add(row)
                'End If
            Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function MenuOpciones(ByVal idc_usuario As Integer) As DataTable

        Try
            Dim dt As New DataTable
            dt.Columns.Add("idc_opcion")
            dt.Columns.Add("descripcion")
            dt.Columns.Add("web_form")
            Dim intopciones() As Integer = {1143, 257, 1559}
            Dim i As Integer = 0
            Dim row As DataRow
            Dim row2 As DataRow

            Dim parametros() As String = {"@idc_opcion", "@idc_usuario"}
            Dim valores(1) As Object
            valores(1) = idc_usuario
            For Each x As Integer In intopciones
                valores(0) = x
                row2 = GWebCD.clsConexion.EjecutaSP_Row("Sp_MenuOpciones", parametros, valores)
                If Not row2 Is Nothing Then

                    row = dt.NewRow
                    row.Item(0) = row2(0)
                    row.Item(1) = row2(1)
                    row.Item(2) = row2(2)
                    dt.Rows.Add(row)
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function MostrarbtnPromociones(ByVal idc_usuario As Integer) As Boolean
        Try
            Dim resultado As Boolean = False
            Dim gweb As New GWebCN.Promociones
            Dim ds As New DataSet

            ds = gweb.Promociones_Por_Terminar(idc_usuario)
            If ds.Tables(0).Rows.Count > 0 Then
                resultado = True
            Else
                resultado = False
            End If
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function MostrarbtnVentas(ByVal idc_usuario As Integer) As Boolean

        Try
            Dim opcionesventas() As Integer = {1163, 1072}
            Dim resultado As Boolean = False
            Dim parametros() As String = {"@idc_opcion", "@idc_usuario"}
            Dim valores(1) As Object
            valores(1) = idc_usuario
            Dim row As DataRow
            For Each x As Integer In opcionesventas
                valores(0) = x
                row = GWebCD.clsConexion.EjecutaSP_Row("Sp_MenuOpciones", parametros, valores)
                If Not row Is Nothing Then
                    resultado = True
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Function SubMenuVentasUsuario(ByVal idc_usuario As Integer) As DataTable
        Try
            Dim opcionesventas() As Integer = {618, 257, 1163}
            Dim dt As New DataTable
            dt.Columns.Add("idc_opcion")
            dt.Columns.Add("descripcion")
            Dim i As Integer = 0
            Dim row As DataRow
            row = dt.NewRow
            Dim parametros() As String = {"@idc_opcion", "@idc_usuario"}
            Dim valores(2) As Object
            valores(1) = idc_usuario
            For Each x As Integer In opcionesventas
                valores(0) = x
                row = GWebCD.clsConexion.EjecutaSP_Row("Sp_MenuOpciones", parametros, valores)
                If Not row Is Nothing Then
                    dt.Rows.Add(row)
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Function SubMenuReportes(ByVal idc_usuario As Integer) As DataTable
        Try
            Dim opcionesventas() As Integer = {283}
            Dim dt As New DataTable
            dt.Columns.Add("idc_opcion")
            dt.Columns.Add("descripcion")
            dt.Columns.Add("web_form")
            Dim i As Integer = 0
            Dim row As DataRow
            row = dt.NewRow
            Dim parametros() As String = {"@idc_opcion", "@idc_usuario"}
            Dim valores(2) As Object
            valores(1) = idc_usuario
            For Each x As Integer In opcionesventas
                valores(0) = x
                row = GWebCD.clsConexion.EjecutaSP_Row("Sp_MenuOpciones", parametros, valores)
                If Not row Is Nothing Then
                    dt.ImportRow(row)
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function





End Class
