Public Class clsInicioSesion

    Public Function InicioSesion(ByVal usuario As String, ByVal contrasena As String) As DataSet

        Dim ds As New DataSet
        Dim parametros() As String = {"@pUSUARIO", "@pCONTRASEÑA"}
        Dim valores() As String = {usuario, contrasena}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("iniciosesion", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            valores = Nothing
            parametros = Nothing
            usuario = Nothing
            contrasena = Nothing
        End Try
        Return ds
    End Function


End Class
