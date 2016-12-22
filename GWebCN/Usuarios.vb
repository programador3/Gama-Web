Public Class Usuarios
    Public Function Usuarios() As DataSet
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_usu_avisos", Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
End Class
