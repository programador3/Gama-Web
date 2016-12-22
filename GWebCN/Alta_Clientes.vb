Public Class Alta_Clientes
    Public Function tipo_rfc() As DataSet
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_combo_tipos_rfc_PUBLICO", Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function estados() As DataSet
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_selecciona_estado", Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function validar_rfc(ByVal rfc As String) As Boolean
        Dim ds As New DataSet
        Dim parametros() As String = {"@prfc"}
        Dim valores() As String = {rfc}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_validar_clientes", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("Existe")
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function alta_cliente(ByVal nombre As String, ByVal nombrepf As String, ByVal ppaterno As String, ByVal pmaterno As String, ByVal idc_usuario As Integer, ByVal ip As String, _
                          ByVal pc As String, ByVal usuariopc As String, ByVal tipom As Char, ByVal cambios As String, ByVal rfc As String, ByVal cve As String, _
                          ByVal tiporfc As Integer, ByVal conrfc As Boolean, ByVal fecha As Date, ByVal curp As String, ByVal estado As Integer, ByVal sexo As Char, _
                          ByVal calle As String, ByVal numero As String, ByVal cp As Integer, ByVal idc_colonia As Integer, ByVal idc_sucursal As Integer, _
                          ByVal folio As Integer, ByVal telefono As String, ByVal referencia As String) As DataSet
        Dim ds As New DataSet
        Dim parametros() As String = {"@pnombre", "@pnompf", "@ppaterno", "@pmaterno", "@pidc_usuario", "@pdirecip", _
                                      "@pnombrepc", "@pusuariopc", "@ptipom", "@pcambios", "@PRFC", "@PCVE", _
                                      "@PTIPORFC", "@PCONRFC", "@PFECHA", "@PCURP", "@PESTADO", "@PSEXO", _
                                      "@PCALLE", "@PNUMERO", "@PCP", "@PIDC_COLONIA", "@PIDC_SUCURSAL", _
                                     "@SFOLIO", "@PTELEFONO", "@PREFERENCIA"}
        Dim valores() As Object = {nombre, nombrepf, ppaterno, pmaterno, idc_usuario, ip, _
                           pc, usuariopc, tipom, cambios, rfc, cve, _
                           tiporfc, conrfc, fecha, curp, estado, sexo, _
                           calle, numero, cp, idc_colonia, idc_sucursal, _
                           folio, telefono, referencia}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_aclientes_ventas_publico", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
