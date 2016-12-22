Public Class Sucursales
    Public Function datos_sucursal(ByVal idc_sucursal As Integer) As DataSet
        Try
            Dim parametros() As String = {"@pidc_sucursal"}
            Dim valores() As Object = {idc_sucursal}

            Return GWebCD.clsConexion.EjecutaSP("sp_datosticketsuc", parametros, valores)

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function suc_cercana(ByVal idc_colonia As Integer) As Integer
        Dim dt As New DataTable
        Try
            dt = GWebCD.clsConexion.EjecutaConsulta("SELECT DBO.fn_sucursal_mas_cercana(" & idc_colonia & ") AS sucent")
            If dt.Rows.Count > 0 Then
                Return CInt(dt.Rows(0).Item("sucent"))
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Sucursales_Entrega() As DataSet
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_sucursales_combo_entregas", Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function sucursales() As DataSet
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_sucursales_combo", Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function succedis(ByVal idc_sucursal As Integer) As Integer
        Try
            Dim dt As New DataTable
            dt = GWebCD.clsConexion.EjecutaConsulta("select dbo.fn_cedis_sucursal (" & idc_sucursal & ") as px_idcs")
            If dt.Rows.Count > 0 Then
                Return CInt(dt.Rows(0).Item("px_idcs"))
            Else
                Return 1
            End If
        Catch ex As Exception
            Return 1
        End Try
    End Function

    Public Function cedis_suc_prg(ByVal idc_sucursal As Integer) As Integer
        Dim dt As New DataTable
        Dim pxid As Integer = 0
        Try
            dt = GWebCD.clsConexion.EjecutaConsulta("select dbo.fn_cedis_sucursal(" & idc_sucursal & ") as pxid")
            If dt.Rows.Count > 0 Then
                pxid = dt.Rows(0).Item("pxid")
                Return pxid
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
