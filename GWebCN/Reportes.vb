Public Class Reportes
    Public Function reporte_ventas_x_pedidos(ByVal fechai As Date, ByVal fechaf As Date, ByVal idc_sucursal As Integer, ByVal idc_cliente As Integer, ByVal idc_usuario As Integer, ByVal grupo As String)
        Dim parametros() As String = {"@PFECHAI", "@PFECHAF", "@PIDC_SUCURSAL", "@PIDC_CLIENTE", "@PIDC_USUARIO", "@pgrupo"}
        Dim valores() As Object = {fechai, fechaf, idc_sucursal, idc_cliente, idc_usuario, grupo}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_reporte_venxped_usuage_movil", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ruta_nombre_reporte(ByVal idc_reporting As Integer) As DataSet
        Dim parametros() As String = {"@pidc_reporting"}
        Dim valores() As Object = {idc_reporting}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_reporting", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function lista_precios_x_familia(ByVal idc_cliente As Integer, ByVal pformato As Integer, ByVal pprecios As Integer, ByVal pdescuentos As Integer, ByVal ppremin As Integer, ByVal idc_sucursal As Integer, ByVal separar As Boolean) As DataTable
        Dim parametros() As String = {"@pidc_cliente", "@pformato", "@pprecios", "@pdescuentos", "@ppremin", "@pidc_sucursal", "@pweb"}
        Dim valores() As Object = {idc_cliente, pformato, pprecios, pdescuentos, ppremin, idc_sucursal, separar}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_lista_precios_x_familia", parametros, valores)
            If ds.Tables.Count > 0 Then
                Return ds.Tables(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
