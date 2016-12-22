Public Class Pedidos_Pendientes_Agentes
    Public Function Datos_Prepedido(ByVal idc_prepedido As Integer) As DataSet
        Dim parametros() As String = {"@pidc_preped"}
        Dim valores() As Object = {idc_prepedido}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_ver_PREped", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function PP_Agentes(ByVal idc_agente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_agente"}
        Dim valores() As Object = {idc_agente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_pedidos_status_transito_agente", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function pedidos_datos_entregando(ByVal idc_pedido As Integer)
        Dim parametros() As String = {"@pidc_pedido"}
        Dim valores() As Object = {idc_pedido}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_pedidos_datos_entregando", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function prepedidos_x_autorizar(ByVal idc_cliente As Integer, ByVal idc_usuario As Integer, ByVal idc_agente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pidc_usuario", "@pidc_agente"}
        Dim valores() As Object = {idc_cliente, idc_usuario, idc_agente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_preped_x_autorizar", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
