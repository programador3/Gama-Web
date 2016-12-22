Public Class Croquis_Direcciones
    Public Function Req_OC_Croquis(ByVal idc_cliente As Integer) As DataSet
        Dim valores() As Object = {idc_cliente}
        Dim parametros() As String = {"@pidc_cliente"}
        Try
            Return GWebCD.clsConexion.EjecutaSP("SP_REQ_OC_CROQUIS", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function

    Public Function Datos_Direccion_Fiscal(ByVal idc_cliente As Integer) As DataSet
        Dim valores() As Object = {idc_cliente}
        Dim parametros() As String = {"@pidc_cliente"}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_consignado_df", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            valores = Nothing
            parametros = Nothing
        End Try
    End Function
    Public Function g_roji_colonia(ByVal idc_colonia As Integer) As DataSet
        Dim parametros() As String = {"@pidc_colonia"}
        Dim valores() As Object = {idc_colonia}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_groji_colonia", parametros, valores)

        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function
    Public Function Datos_Colonia(ByVal idc_colonia As Integer) As DataSet
        Dim parametros() As String = {"@pidc_colonia"}
        Dim valores() As Object = {idc_colonia}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_datos_colonia", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function
End Class
