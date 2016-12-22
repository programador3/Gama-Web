Public Class Grupos
    Public Function grupos()
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_combo_proytmk_gpo_usuarios", Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
