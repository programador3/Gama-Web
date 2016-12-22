Public Class Titulos
    Public Function Titulos() As DataSet
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_combo_titulos_profesionales", Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
