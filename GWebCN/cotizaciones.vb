Public Class cotizaciones
    Public Function cotizaciones_agentes(ByVal idc_sucursal As Integer, ByVal idc_agente As Integer, ByVal fechai As Date, ByVal fechaf As Date) As DataSet
        Dim parametros() As String = {"@pidc_sucursal", "@pidc_agente", "@pfechai", "@pfechaf"}
        Dim valores() As Object = {idc_sucursal, idc_agente, fechai, fechaf}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_agentes_act_cotizacion_agente_periodo", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function cotizaciones_guardadas(ByVal idc_agente As Integer, ByVal fecha As DateTime) As DataSet
        Dim parametros() As String = {"@pidc_agente", "@pfecha"}
        Dim valores() As Object = {idc_agente, fecha}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_cotizaciones_guardadas", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Agendar_Llamada(ByVal fecha As Date, ByVal obsllamada As String, ByVal idc_cliente As Integer, ByVal idc_usuario As Integer, ByVal ip As String, ByVal pc As String, ByVal usuariopc As String)
        Dim parametros() As String = {"@vfechaa", "@VOBSLLA", "@pidc_cliente", "@pidc_usuario", "@pdirecip", "@pnombrepc", "@pusuariopc"}
        Dim valores() As Object = {fecha, obsllamada, idc_cliente, idc_usuario, ip, pc, usuariopc}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_arellamar_ven", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function acotizacion(ByVal idc_cliente As Integer, ByVal idc_tipoentfac As Integer, ByVal maniobras As Decimal, ByVal desiva As Boolean, _
                                ByVal idc_iva As Integer, ByVal idc_usuario As Integer, ByVal idc_sucursal As Integer, ByVal ip As String, ByVal pc As String, _
                                ByVal usuario As String, ByVal cadenaarti As String, ByVal totart As Integer, ByVal proye As Boolean, ByVal idpro As Integer, _
                                ByVal idc_colonia As Integer, ByVal calle As String, ByVal numero As String, ByVal cod_postal As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pidc_tipoentfac", "@pmaniobras", "@pdesiva", "@pidc_iva", "@pidc_usuario" _
                                      , "@pidc_sucursal", "@pdirecip", "@pnombrepc", "@pusuariopc", "@pcadenaarti", "@ptotart", "@pproye", _
                                      "@pidpro", "@pidc_colonia", "@pcalle", "@pnumero", "@pcod_postal"}
        Dim valores() As Object = {idc_cliente, idc_tipoentfac, maniobras, desiva, idc_iva, idc_usuario, _
                                   idc_sucursal, ip, pc, usuario, cadenaarti, totart, proye, _
                                   idpro, idc_colonia, calle, numero, cod_postal}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_acotizacion", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
