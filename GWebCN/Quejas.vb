Public Class Quejas

    Public Function direccion_material(ByVal idc_factura As Integer) As DataSet
        Dim ds As New DataSet
        Dim parametros() As String = {"@pidc_factura"}
        Dim valores() As Object = {idc_factura}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_consignado_factura_todo", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function info_queja(ByVal idc_queja As Integer) As DataSet
        Dim parametros() As String = {"@pidc_queja"}
        Dim valores() As Object = {idc_queja}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_quejas_imp", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function alta_queja(ByVal idc_factura As Integer, ByVal agente As String, ByVal comprador As String, ByVal contacto As String, ByVal tmk As String, _
                        ByVal tel_age As String, ByVal tel_comprador As String, ByVal tel_contacto As String, ByVal tel_tmk As String, ByVal obs As String, _
                        ByVal problema As String, ByVal idc_colonia As Integer, ByVal calle As String, ByVal numero As String, ByVal hora_visita As String, _
                        ByVal idc_usuario As Integer, ByVal ip As String, ByVal pc As String, ByVal usuariopc As String, ByVal tipom As String, _
                        ByVal cambios As String, ByVal folio As Integer, ByVal cadena As String, ByVal totart As Integer) As DataSet
        Dim parametros() As String = {"@PIDC_FACTURA", "@PAGENTE", "@PCOMPRADOR", "@PCONTACTO", "@PTMK", _
                                      "@PTEL_AGE", "@PTEL_COMPRADOR", "@PTEL_CONTACTO", "@PTEL_TMK", "@POBSERVACIONES", _
                                      "@PPROBLEMA", "@PIDC_COLONIA", "@PCALLE", "@PNUMERO", "@PHORA_VISITA", _
                                      "@PIDC_USUARIO", "@PDIRECIP", "@PNOMBREPC", "@PUSUARIOPC", "@PTIPOM", _
                                      "@PCAMBIOS", "@SFOLIO", "@PCADENA", "@PTOTART"}
        Dim valores() As Object = {idc_factura, agente, comprador, contacto, tmk, _
                         tel_age, tel_comprador, tel_contacto, tel_tmk, obs, _
                         problema, idc_colonia, calle, numero, hora_visita, _
                         idc_usuario, ip, pc, usuariopc, tipom, _
                         cambios, folio, cadena, totart}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_aquejas_nuevo", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function folio(ByVal idc_tabla As Integer) As DataTable
        Dim dt As New DataTable
        Try
            dt = GWebCD.clsConexion.EjecutaConsulta("select no_folio from folios where idc_tabla=" & idc_tabla)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Datos_Factura(ByVal codfac As String) As DataSet
        Dim ds As New DataSet
        Dim parametros() As String = {"@pcodfac"}
        Dim valores() As Object = {codfac}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_datosfac_canfac", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Devoluciones_Factura(ByVal idc_factura As Integer)
        Dim ds As New DataSet
        Dim parametros() As String = {"@pidc_factura"}
        Dim valores() As Object = {idc_factura}

        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_devoluciones_facturas", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
