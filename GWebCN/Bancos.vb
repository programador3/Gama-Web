Public Class Bancos
    Public Function Bancos() As DataSet
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_combo_bancos", Nothing, Nothing)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Bancos_Check() As DataSet
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_bancos", Nothing, Nothing)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
