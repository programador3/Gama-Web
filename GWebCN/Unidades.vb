Public Class Unidades
    Public Function Unidad_Archivos(ByVal cod_archi As String) As DataSet
        Dim parametros() As String = {"@pcod_archivo"}
        Dim valores() As Object = {cod_archi}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_uni_archi", parametros, valores)

        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function
    Public Function Unidad_Archivos_2(ByVal cod_archi As String) As String
        Dim parametros() As String = {"@pcod_archivo"}
        Dim valores() As Object = {cod_archi}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_uni_archi", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("unidad")
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function
End Class
