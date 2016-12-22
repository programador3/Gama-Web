Public Class Promociones
    Public Function promociones_precios()
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_costos_promociones", Nothing, Nothing)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Promociones_Por_Terminar(ByVal idc_usuario As Integer) As DataSet
        Dim ds As New DataSet
        Dim parametros() As String = {"@pidc_usuario"}
        Dim valores() As Object = {idc_usuario}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_datos_promocion_arti_terminar2", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Promociones_Cliente(ByVal idc_cliente As Integer, ByVal idc_listap As Integer) As DataSet
        Dim ds As New DataSet
        Dim parametros() As String = {"@pidc_cliente", "@vidc_listap"}
        Dim valores() As Object = {idc_cliente, idc_listap}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_promociones_cliente", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Promociones_Cliente_existen(ByVal idc_cliente As Integer, ByVal idc_listap As Integer) As Boolean
        Dim ds As New DataSet
        Dim parametros() As String = {"@pidc_cliente", "@vidc_listap"}
        Dim valores() As Object = {idc_cliente, idc_listap}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_promociones_cliente", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        Finally
            ds.Dispose()
        End Try
    End Function
End Class
