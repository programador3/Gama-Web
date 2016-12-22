Public Class Fechas
    Public Function Fecha_Maxima_Entrega() As Date
        Dim fecha_maxima As Date
        Dim ds As New DataSet
        Dim row As DataRow
        Try
            ds = GWebCD.clsConexion.EjecutaSP("SP_FechaMaxima", Nothing, Nothing)
            If ds.Tables(0).Rows.Count Then
                row = ds.Tables(0).Rows(0)
                fecha_maxima = row.Item(0)
                Return fecha_maxima
            End If
        Catch ex As Exception
            Throw ex
        Finally
            ds = Nothing
            row = Nothing
        End Try
    End Function


    Public Function Fechas_Vendedor()
        Try
            Dim dt As New DataTable
            dt = GWebCD.clsConexion.EjecutaConsulta("select * from dbo.fn_maximo_programar_vendedor()")
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
