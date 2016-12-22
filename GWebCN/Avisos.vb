Public Class Avisos
    Public Function checar_avisos_nuevos(ByVal idc_usuario As Integer) As DataSet
        Dim parametros() As String = {"@pidc_usuario"}
        Dim valores() As Object = {idc_usuario}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_checa_avisos_nuevos", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function avisos_nuevos(ByVal idc_usuario As Integer) As DataSet
        Dim parametros() As String = {"@pidc_usuario"}
        Dim valores() As Object = {idc_usuario}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_avisos_nuevo", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Enviar_Aviso(ByVal asunto As String, ByVal mensaje As String, ByVal para As String, ByVal total As Integer, ByVal idc_usuario As Integer) As DataSet
        Dim parametros() As String = {"@pasunto", "@ptexto", "@ppara", "@ptotal", "@pidc_usuario"}
        Dim valores() As Object = {asunto, mensaje, para, total, idc_usuario}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_aavisos_gen", parametros, valores)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function Avisos_Todos(ByVal idc_usuario As Integer, ByVal top As Integer) As DataSet

    '    Dim parametros() As String = {"@pidc_usuario", "@ptop"}
    '    Dim valores() As Object = {idc_usuario, top}
    '    Try
    '        Return GWebCD.clsConexion.EjecutaSP("sp_avisos", parametros, valores)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Function Avisos_Todos(ByVal idc_usuario As Integer, ByVal top As Integer, ByVal tipo As Integer, ByVal nombre As String) As DataSet

        Dim parametros() As String = {"@pidc_usuario", "@ptop", "@PTIPO", "@PNOMBRE"}
        Dim valores() As Object = {idc_usuario, top, tipo, nombre}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_avisos_WEB", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try

        ' 127,50,1,''
    End Function
End Class
