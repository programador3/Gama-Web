Public Class Correos
    Public Function correos_contraseña(ByVal idc_usuario As Integer, ByVal tipo As Object) As DataSet
        Dim parametros() As String = {"@pidc_usuario", "@ptipo"}
        Dim valores() As Object = {idc_usuario, tipo}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_correo_contraseña", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
