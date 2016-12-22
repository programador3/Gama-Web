Public Class Comisiones
    Public Function comisiones_esp_articulos(ByVal fechai As String, ByVal fechaf As String, ByVal idc_usuario As Integer) As DataSet
        Dim parametros() As String = {"@pfechai", "@pfechaf", "@PIDC_USUARIO"}
        Dim valores() As Object = {fechai, fechaf, idc_usuario}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_reporte_comision_vales_articulo_nuevo", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function comisiones_esp_activaciones(ByVal fechai As String, ByVal fechaf As String, ByVal idc_usuario As Integer) As DataSet
        Dim parametros() As String = {"@pfechai", "@pfechaf", "@PIDC_USUARIO"}
        Dim valores() As Object = {fechai, fechaf, idc_usuario}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_reporte_comision_vales_articulo_activacion", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
