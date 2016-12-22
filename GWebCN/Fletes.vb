Public Class Fletes
    Public Function Flete2(ByVal cadena1 As String, ByVal total As Integer, ByVal tipo_camion As Integer, ByVal idc_sucursal As Integer, ByVal idc_colonia As Integer, ByVal idc_cliente As Integer, ByVal DesIVA As Boolean, ByVal iva As Integer, ByVal cadena2 As String) As DataTable
        Dim paramateros() As String = {"@pcadena", "@ptot", "@pidc_tipocam", "@pidc_sucursal", "@pidc_colonia", "@PIDC_CLIENTE", "@DESIVA", "@IVA", "@PCAD2"}
        Dim valores() As Object = {cadena1, total, tipo_camion, idc_sucursal, idc_colonia, idc_cliente, DesIVA, iva, cadena2}
        Try
            'fn_cadenas_fletes_preped_sumar_pedidos_tabla
            'fn_cadenas_fletes_preped_sumar_pedidos_tabla_1_esp
            Return GWebCD.clsConexion.EjecutaConsulta("select * from dbo.fn_cadenas_fletes_preped_sumar_pedidos_tabla_1_esp('" & cadena1 _
                                                      & "'," & total & "," & idc_sucursal & "," & idc_colonia & _
                                                      "," & idc_cliente & "," & IIf(DesIVA = True, 1, 0) & "," & iva & ",'" & cadena2 & "'," & 0 & ")")

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function porcentaje_comision(ByVal tipo As Integer, ByVal distancia As Decimal) As DataTable
        Dim paramateros() As String = {"@ptipo", "@pdistancia"}
        Dim valores() As Object = {tipo, distancia}
        Try
            Return GWebCD.clsConexion.EjecutaConsulta("select porcentaje from DBO.fn_porcentaje_comision(" & tipo _
                                                      & "," & distancia & _
                                                      ")")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Flete(ByVal cadena1 As String, ByVal total As Integer, ByVal tipo_camion As Integer, ByVal idc_sucursal As Integer, ByVal idc_colonia As Integer, ByVal idc_cliente As Integer, ByVal DesIVA As Boolean, ByVal iva As Integer, ByVal cadena2 As String) As DataSet
        Dim paramateros() As String = {"@pcadena", "@ptot", "@pidc_tipocam", "@pidc_sucursal", "@pidc_colonia", "@PIDC_CLIENTE", "@DESIVA", "@IVA", "@PCAD2"}
        Dim valores() As Object = {cadena1, total, tipo_camion, idc_sucursal, idc_colonia, idc_cliente, DesIVA, iva, cadena2}
        Try
            Return GWebCD.clsConexion.EjecutaSP("SP_Cadenas_Fletes_Preped", paramateros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Validar_folio_Autorizacion(ByVal tipoFolio As Integer, ByVal folio As Integer) As DataRow
        Dim parametros() As String = {"@pidc_tipo_aut", "@pFOLIO"}
        Dim valores() As Object = {tipoFolio, folio}
        Dim row As DataRow
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_checar_folio_AUTORIZACION", parametros, valores)
            If ds.Tables(0).Rows.Count Then
                row = ds.Tables(0).Rows(0)
                Return row
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Descripcion_Autorizacion(ByVal TipoFolio As Integer) As String
        Dim parametros() As String = {"@pidc_tipo_aut"}
        Dim valores() As String = {TipoFolio}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_descripcion_AUTORIZACION", parametros, valores)
            If ds.Tables(0).Rows.Count Then
                Return ds.Tables(0).Rows(0).Item(0).ToString
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
