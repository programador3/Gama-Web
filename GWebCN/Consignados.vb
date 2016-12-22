Public Class Consignados
    Public Function Historial_Consignados(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_selecciona_consignado_cliente", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function

End Class
