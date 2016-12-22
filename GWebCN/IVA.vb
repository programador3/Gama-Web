Public Class IVA
    Public Function validar_cambio_IVA_frontera(ByVal idc_colonia As Integer, ByVal idc_sucursal As Integer) As DataRow
        Dim parametros() As String = {"@pidc_sucursal", "@pidc_colonia"}
        Dim valores() As Object = {idc_sucursal, idc_colonia}
        Dim ds As New Data.DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_cambiar_iva_frontera", parametros, valores)
            If ds.Tables(0).Rows.Count Then
                Return ds.Tables(0).Rows(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
