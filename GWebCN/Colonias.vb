Public Class Colonias
    Public Function Buscar_Colonias(ByVal valor As String) As DataSet
        Dim parametros() As String = {"@pvalor"}
        Dim valores() As Object = {valor}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_bcolonias", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function

    Public Function validar_colonias(ByVal idc_colonia As Integer) As String
        Dim dt As New DataTable
        Dim vmencol As String = ""
        Try
            dt = GWebCD.clsConexion.EjecutaConsulta("select dbo.fn_validar_entrega_colonia(" & idc_colonia & ") as mensacol")
            If dt.Rows.Count > 0 Then
                vmencol = dt.Rows(0).Item(0).ToString.Trim
            End If
        Catch ex As Exception
            vmencol = "No se Pudo Validar la entrega en la Colonia.  \n-\n" & ex.Message
        End Try
        Return vmencol
    End Function
End Class
