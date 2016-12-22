Public Class Proyecciones
    Public Function edicion_proyeccion(ByVal idc_proytmk As Integer, ByVal idc_horaproy As Integer, ByVal vpreproy As Double, ByVal idc_usu As Integer, ByVal idc_usuario As Integer, ByVal fecha As Date, ByVal ip As String, ByVal pc As String, ByVal usuario As String) As DataSet
        Dim ds As New DataSet
        Dim parametros() As String = {"@pidc_proytmk", "@pidc_horaproy", "@pproyeccion", "@pidc_usu", "@pidc_usuario", "@pfecha", "@pdirecip", "@pnombrepc", "@pusuariopc"}
        Dim valores() As Object = {idc_proytmk, idc_horaproy, vpreproy, idc_usu, idc_usuario, fecha, ip, pc, usuario}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_mproytmk", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'comentado 14-01-2016
    'Public Function edicion_proyeccion(ByVal idc_proytmk As Integer, ByVal idc_horaproy As Integer, ByVal vpreal As Double, ByVal idc_usu As Integer, ByVal idc_usuario As Integer, ByVal fecha As Date, ByVal ip As String, ByVal pc As String, ByVal usuario As String) As DataSet
    '    Dim ds As New DataSet
    '    Dim parametros() As String = {"@pidc_proytmk", "@pidc_horaproy", "@pproyeccion", "@pidc_usu", "@pidc_usuario", "@pfecha", "@pdirecip", "@pnombrepc", "@pusuariopc"}
    '    Dim valores() As Object = {idc_proytmk, idc_horaproy, vpreal, idc_usu, idc_usuario, fecha, ip, pc, usuario}
    '    Try
    '        ds = GWebCD.clsConexion.EjecutaSP("sp_mproytmk", parametros, valores)
    '        Return ds
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Function proy_ventas(ByVal fecha As String, ByVal hora As Integer, ByVal idc_usuario As Integer, ByVal grupo As String, ByVal idc_usu As Integer) As DataSet
        Try
            Dim ds As New DataSet
            Dim parametros() As String = {"@pfecha", "@phora", "@pidc_usuario", "@pgrupo", "@PIDC_USU"}
            Dim valores() As String = {fecha, hora, idc_usuario, grupo, idc_usu}
            ds = GWebCD.clsConexion.EjecutaSP("sp_proytmk_consulta", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function cargar_horas() As DataSet
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_combo_proytmk_horas", Nothing, Nothing)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function cargar_grupos() As DataSet
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_combo_proytmk_gpo_usuarios", Nothing, Nothing)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function cargar_usuarios_tmk() As DataSet
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_combo_usuarios_tmk_pv", Nothing, Nothing)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function validar_visible(ByVal idc_usuario As Integer, ByVal tipo_aut As Integer) As DataSet
        Try
            Dim ds As New DataSet
            Dim parametros() As String = {"@pidc_usuario", "@pidc_tipo_aut"}
            Dim valores() As Object = {idc_usuario, tipo_aut}
            ds = GWebCD.clsConexion.EjecutaSP("sp_checar_autorizacion", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try

    End Function
End Class
