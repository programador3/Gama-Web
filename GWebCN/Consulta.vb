Public Class Consulta
    Public Function ejecuta_consulta(ByVal consulta As String) As DataTable
        Try
            Return GWebCD.clsConexion.EjecutaConsulta(consulta)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
