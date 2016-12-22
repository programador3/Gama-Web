Public Class Contactos
    Public Function contactos_nuevos_pendientes(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_datos_agregar_contactos", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function mod_contactos(ByVal idc_telcli As Integer, ByVal email As String, ByVal nombre As String, ByVal celular As String, ByVal fecha As Date, ByVal activo As Boolean, _
                                  ByVal hobbies As String, ByVal equipo As String, ByVal idc_tcontacto As Integer, ByVal funciones As String, ByVal idc_titulo As Integer, _
                                  ByVal telefono As String, ByVal idc_usuario As String, ByVal ip As String, ByVal pc As String, ByVal usuariopc As String, ByVal idc_cliente As Integer, _
                                  ByVal opcion As String) As DataSet
        Try
            Dim parametros() As String = {"@pidc_telcli", "@pemail", "@pnombre", "@pcelular", _
                                          "@pfecha", "@pactivo", "@phobbies", "@pequipo", _
                                          "@pidc_tcontacto", "@pfunciones", "@pidc_titulo", _
                                          "@ptelefono", "@pidc_usuario", "@pdirecip", _
                                          "@pnombrepc", "@pusuariopc", "@pidc_cliente", "@popcion"}
            Dim valores() As Object = {idc_telcli, email, nombre, celular, _
                                       fecha, activo, hobbies, equipo, _
                                       idc_tcontacto, funciones, idc_titulo, _
                                       telefono, idc_usuario, ip, _
                                       pc, usuariopc, idc_cliente, opcion}
            Return GWebCD.clsConexion.EjecutaSP("sp_aclientes_tel_solcambio", parametros, valores)


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Tipos_Contacto() As DataSet
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_combo_tipos_contacto", Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Datos_contacto(ByVal idc_telcli As Integer) As DataSet
        Try
            Dim parametros() As String = {"@pidc_telcli"}
            Dim valores() As Object = {idc_telcli}
            Return GWebCD.clsConexion.EjecutaSP("sp_datos_editar_contacto", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function contactos_cliente(ByVal idc_cliente As Integer) As DataSet
        Try
            Dim parametros() As String = {"@pidc_cliente"}
            Dim valores() As Object = {idc_cliente}
            Return GWebCD.clsConexion.EjecutaSP("sp_clientes_tel_mtto1", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
