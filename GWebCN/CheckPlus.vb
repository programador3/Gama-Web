Public Class CheckPlus
    'Verifica si se tiene que cobrar un % si se paga con Cheque.
    Public Function Revisar_Cargar_CheckPlus() As Boolean
        Dim ds As New Data.DataSet
        Dim validar As Boolean
        Try
            ds = GWebCD.clsConexion.EjecutaSP("SP_Cargar_ChekPlus", Nothing, Nothing)
            If ds.Tables(0).Rows.Count > 0 Then
                validar = CBool(ds.Tables(0).Rows(0).Item(0))
                Return validar
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Se usa para validar folio de CheckPlus.
    Public Function comprobar_chekplus(ByVal folio As Integer, ByVal monto As Integer, ByVal idc_cliente As Integer) As String
        Dim parametros() As String = {"@pidc_folioprecp", "@PMONTO", "@pidc_cliente"}
        Dim valores() As Object = {folio, monto, idc_cliente}
        Dim ds As New DataSet
        Dim row As DataRow
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_comprobar_chekplus_PRE", parametros, valores)

            If ds.Tables(0).Rows.Count > 0 Then
                row = ds.Tables(0).Rows(0)
                Return row("mensaje")
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
            ds = Nothing
        End Try
    End Function

End Class
