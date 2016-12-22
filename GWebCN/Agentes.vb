Public Class Agentes
    Public Function atareas_clientes(ByVal idc_cliente As Integer, ByVal fecha As String, ByVal meta As Decimal, ByVal obs_venta As String,
                                     ByVal articulos As String, ByVal num_articulos As Integer, ByVal idc_usuario As Integer,
                                     ByVal ip As String, ByVal pc As String, ByVal usuario As String, ByVal idc_agente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pfecha", "@pmeta_venta", "@pobs_venta",
                                      "@particulos", "@pnum_articulos", "@pidc_usuario",
                                      "@pdirecip", "@pnombrepc", "@pusuariopc", "@pidc_agente"}
        Dim valores() As Object = {idc_cliente, fecha, meta, obs_venta,
                                      articulos, num_articulos, idc_usuario,
                                      ip, pc, usuario, idc_agente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_atareas_clientes", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function cargar_articulos(ByVal idc_cliente As Integer, ByVal idc_agente As Integer, ByVal FECHA As DateTime) As DataSet
        Dim parametros() As String = {"@pidc_agente", "@pfecha", "@pidc_cliente"}
        Dim valores() As Object = {idc_agente, FECHA, idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("SP_VER_TAREAS_CLIENTE_DETALLES_TODO", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function cargar_articulos_nuevos(ByVal idc_cliente As Integer, ByVal filtro As String) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pmaster", "@pfiltro"}
        Dim valores() As Object = {idc_cliente, 1, filtro}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_articulos_agregar_precio", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function cargar_cliente(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_datos_cliente", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function agregar_tarea(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_datos_cliente", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function revisar_tarea(ByVal idc_agente As Integer, ByVal fecha As String, ByVal articulos As String, ByVal num_articulos As Integer, ByVal idc_usuario As Integer, ByVal ip As String, ByVal pc As String, ByVal usuario As String) As DataSet
        Dim parametros() As String = {"@pidc_agente", "@pfecha", "@particulos", "@pnum_articulos", "@pidc_usuario", "@pdirecip", "@pnombrepc", "@pusuariopc"}
        Dim valores() As Object = {idc_agente, fecha, articulos, num_articulos, idc_usuario, ip, pc, usuario}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_atareas_clientes_rev", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function detalles_tareas(ByVal idc_agente As Integer, ByVal fecha As String) As DataSet
        Dim parametros() As String = {"@pidc_agente", "@pfecha"}
        Dim valores() As Object = {idc_agente, fecha}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_ver_tareas_agente_detalles", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function visitas_reporte(ByVal idc_agente As Integer, ByVal fecha As DateTime, ByVal fechafin As DateTime) As DataSet
        Dim parametros() As String = {"@pidc_agente", "@pfecha", "@pfechafin"}
        Dim valores() As Object = {idc_agente, fecha, fechafin}
        Try
            Return GWebCD.clsConexion.EjecutaSP("SP_Coo_Visitas_age", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function acti_agentes(ByVal idc_usuario As Integer, ByVal ip As String, ByVal pc As String, ByVal usuario As String, ByVal idc_cliente As Integer, ByVal idc_agente As Integer, ByVal idc_actiage As Integer, ByVal latitud As Double, ByVal longitud As Double)
        Dim parametros() As String = {"@pidc_usuario", "@pdirecip", "@pnombrepc", "@pusuariopc", "@pidc_cliente", "@pidc_agente", "@pidc_actiage", "@platitud", "@plongitud"}
        Dim valores() As Object = {idc_usuario, ip, pc, usuario, idc_cliente, idc_agente, idc_actiage, latitud, longitud}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_aagentes_act_movil", parametros, valores)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function agentes_vs_usuarios(ByVal idc_usuario) As DataSet
        Dim parametros() As String = {"@pidc_usuario"}
        Dim valores() As Object = {idc_usuario}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_combo_agentes_usu", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function comisiones_agente(ByVal mes As Integer, ByVal año As Integer, ByVal idc_agente As Integer, ByVal aldia As Boolean)
        Dim parametros() As String = {"@pmes", "@paño", "@pagente", "@aldia"}
        Dim valores() As Object = {mes, año, idc_agente, aldia}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_comisiones_nueva13", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class