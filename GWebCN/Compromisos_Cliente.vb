Public Class Compromisos_Cliente
    Public Function clientes_compromisos_revisar(ByVal idc_clicompromiso As Integer, ByVal observ As String, ByVal completada As Boolean, ByVal idc_usuario As Integer, _
                                                 ByVal ip As String, ByVal pc As String, ByVal usuariopc As String)
        Dim parametros() As String = {"@pidc_clicompromiso", "@pobserv", "@pcompletada", "@pidc_usuario", "@pdirecip", "@pnombrepc", "@pusuariopc"}
        Dim valores() As Object = {idc_clicompromiso, observ, completada, idc_usuario, ip, pc, usuariopc}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_aclientes_compromisos_rev", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function tipos_compromisos_clientes() As DataSet
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_combo_tipos_compromisos_cli", Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function guardar_compromiso_cliente(ByVal idc_cliente As Integer, ByVal compromiso As String, ByVal idc_tipo As Integer, _
                                               ByVal idc_usuario As Integer, ByVal ip As String, ByVal pc As String, ByVal usuario As String) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pcompromiso", "@pidc_tipocomcli", "@pidc_usuario", "@pdirecip", "@pnombrepc", "@pusuariopc"}
        Dim valores() As Object = {idc_cliente, compromiso, idc_tipo, idc_usuario, ip, pc, usuario}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_aclientes_compromisos", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function checar_autorizado(ByVal idc_tipo_aut As Integer, ByVal idc_usuario As Integer) As DataSet
        Dim parametros() As String = {"@pidc_tipo_aut", "@pidc_usuario"}
        Dim valores() As Object = {idc_tipo_aut, idc_usuario}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_checar_autorizacion", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function datos_cliente(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_datos_cliente", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function compromisos(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_clientes_compromisos", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
