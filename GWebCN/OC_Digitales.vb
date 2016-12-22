Public Class OC_Digitales
    Public Function Alta_OC(ByVal idc_cliente As Integer, ByVal no_occ As String, ByVal idc_usuario As Integer, ByVal ip As String, ByVal pc As String, ByVal usuariopc As String, ByVal enviar As Boolean) As DataSet
        Dim parametros() As String = {"@PIDC_CLIENTE", "@PNO_OCC", "@pidc_usuario", "@pdirecip", "@pnombrepc", "@pusuariopc", "@penviar_aviso"}
        Dim valores() As String = {idc_cliente, no_occ, idc_usuario, ip, pc, usuariopc, enviar}

        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_AOC_CLIENTES", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function OC_Digitales_Pendientes_cliente(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@PIDC_CLIENTE"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_oc_clientes2", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function

    Public Function buscar_existe_OC_Cliente(ByVal idc_cliente As Integer, ByVal no_occ As Integer) As Boolean
        Dim parametros() As String = {"@pidc_cliente", "@pno_occ"}
        Dim valores() As Object = {idc_cliente, no_occ}
        Dim ds As New DataSet
        Dim row As DataRow
        Dim existe As Boolean
        Try
            ds = GWebCD.clsConexion.EjecutaSP("SP_EXISTE_OCC", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                row = ds.Tables(0).Rows(0)
                existe = row("existe")
                Return existe
            End If
        Catch ex As Exception
            Throw ex
        Finally
            valores = Nothing
            parametros = Nothing
            ds = Nothing
            row = Nothing
        End Try
    End Function
    ''' <summary>
    ''' Devuelve valor Boolean, True si el numero de OC es correcto.
    ''' </summary>
    ''' <param name="idc_occli">Numero de Orden de Compra </param>
    ''' <returns>Retorna Boolean</returns>
    ''' <remarks>/////</remarks>
    Public Function validar_oc_cliente(ByVal idc_occli As Integer) As Boolean
        Dim parametros() As String = {"@pidc_occli"}
        Dim valores() As Object = {idc_occli}
        Dim ds As New Data.DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_occ_valida_cliente", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                Return CBool(ds.Tables(0).Rows(0).Item(0))
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Checar_Captura_Oc() As Boolean
        Dim ds As New Data.DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("", Nothing, Nothing)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Regresa si para el pedido se tiene que ligar una OC y un Croquis
    ''' </summary>
    ''' <param name="idc_cliente">Numero del cliente</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Requiere_OC_Croquis(ByVal idc_cliente As Integer) As DataRow
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_req_oc_croquis", parametros, valores)
            If ds.Tables(0).Rows.Count Then
                Return ds.Tables(0).Rows(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
