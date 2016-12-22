Public Class Encripta
    Public Function des_encripta() As DataSet
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_encripta", Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
