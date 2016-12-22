Public Class Folios

    Public Function Obtener_Consecutivo_Folio() As Object
        Return GWebCD.clsConexion.EjecutaSP_Scalar("sp_folio_preped_pedidos", Nothing, Nothing)
    End Function


    Public Function Folio_Gama(ByVal tabla As Integer) As String
        Dim ds As New DataSet
        Dim parametros() As String = {"@pidc_tabla"}
        Dim valores() As Object = {tabla}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_folios", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                Return CStr(ds.Tables(0).Rows(0)(1))
            Else
                Return "0"
            End If
        Catch ex As Exception
            Return "0"
        End Try
    End Function
End Class
