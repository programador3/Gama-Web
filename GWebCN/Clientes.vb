Public Class Clientes
    Public Function tareas_cliente_detalles(ByVal idc_cliente As Integer, ByVal fecha As String) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pfecha"}
        Dim valores() As Object = {idc_cliente, fecha}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_ver_tareas_cliente_detalles", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function aclientes_articulos(ByVal idc_articulo As Integer, ByVal idc_cliente As Integer, ByVal tipo As Integer, ByVal fecha As String, _
                                        ByVal idc_usuario As Integer, ByVal ip As String, ByVal pc As String, ByVal usuario As String, ByVal opcion As String, _
                                        ByVal pver As Boolean) As DataSet
        Dim parametros() As String = {"@pidc_articulo", "@pidc_cliente", "@ptipo ", "@pfecha", "@pidc_usuario", "@pdirecip", "@pnombrepc", "@pusuariopc", "@popcion", "@pver"}
        Dim valores() As Object = {idc_articulo, idc_cliente, tipo, fecha, idc_usuario, ip, pc, usuario, opcion, pver}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_aclientes_articulos1", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function facturas_articulo_cliente(ByVal idc_cliente As Integer, ByVal idc_articulo As Integer, ByVal idc_sucursal As Integer, ByVal fechai As String, ByVal fechaf As String, ByVal grupo As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pidc_articulo", "@PIDC_SUCURSAL", "@pfechai", "@pfechaf", "@pgrupo"}
        Dim valores() As Object = {idc_cliente, idc_articulo, idc_sucursal, fechai, fechaf, grupo}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_rep_facturas_cliente_ARTICULO", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function clientes_articulos_vents(ByVal idc_cliente As Integer, ByVal grupo As Integer) As DataSet
        Dim parametros() As String = {"@PIDC_CLIENTE", "@pgrupo"}
        Dim valores() As Object = {idc_cliente, grupo}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_datos_clientes_articulos", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function clientes_visitas(ByVal idc_agente As Integer, ByVal fechai As Date, ByVal fechaf As Date, ByVal pacti As Boolean, ByVal aldia As Boolean, ByVal dia As Integer)
        Dim parametros() As String = {"@pidc_agente", "@pfechai", "@pfechaf", "@pacti", "@aldia", "@pd"}
        Dim valores() As Object = {idc_agente, fechai, fechaf, pacti, aldia, dia}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_FN_efectividad_agentes_mo", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function precios_cotizados(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@PIDC_CLIENTE"}
        Dim valores() As Object = {idc_cliente}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_ARTICULOS_CLIENTE_DESC_cedis", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ubicacion(ByVal idc_cliente As Integer) As DataSet
        Dim ds As New DataSet
        Dim paramtros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_cliente_ubicacion", paramtros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function clientes_promocion(ByVal idc_articulo As Integer, ByVal idc_cliente As Integer, ByVal cantidad As Integer, ByVal idc_lista As Integer) As DataSet
        Dim ds As New DataSet
        Dim parametros() As String = {"@pidc_articulo", "@pidc_cliente", "@pcantidad", "@pidc_listap"}
        Dim valores() As Object = {idc_articulo, idc_cliente, cantidad, idc_lista}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_promocion_articulo_cliente", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function datos_Cuentas_x_Cobrar(ByVal cadena As String) As DataTable
        Dim dt As New DataTable
        Try
            dt = GWebCD.clsConexion.EjecutaConsulta(cadena)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function alta_pre_checkplus(ByVal idc_cuenta As Integer, ByVal monto As Decimal, ByVal dias As Integer, ByVal clave As String, ByVal atendio As String, ByVal calle As String, ByVal numero As String, ByVal idc_colonia As Integer, _
                                       ByVal folio As String, ByVal idc_usuario As Integer, ByVal ip As String, ByVal pc As String, ByVal usuariopc As String, ByVal tipo As String, ByVal cambios As String, ByVal sfolio As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cuentacli", "@pmonto", "@pdias", "@pclave_elector", "@patendio", "@pcalle", "@pnumero", "@pidc_colonia", _
                                      "@pfolio_elector", "@pidc_usuario", "@pdirecip", "@pnombrepc", "@pusuariopc", "@ptipom", "@pcambios", "@SFOLIO"}
        Dim valores() As Object = {idc_cuenta, monto, dias, clave, atendio, calle, numero, idc_colonia, _
                                        folio, idc_usuario, ip, pc, usuariopc, tipo, cambios, sfolio}
        Dim ds As New DataSet

        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_achek_plus_pre", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function clientes_checkplus(ByVal idc_cliente As Integer, ByVal idc_dirchekplus As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pidc_dirchekplus"}
        Dim valores() As Object = {idc_cliente, idc_dirchekplus}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_clientes_chekplus", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function alta_clientes_checkplus(ByVal idc_usuario As Integer, ByVal ip As String, ByVal pc As String, ByVal usuariopc As String, ByVal cambios As String, ByVal nombre As String, ByVal folio As String, ByVal clave As String, _
                                            ByVal calle As String, ByVal numero As String, ByVal idc_colonia As Integer, ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_usuario", "@pdirecip", "@pnombrepc", "@pusuariopc", "@pcambios", "@pnombre", "@pfolio_elector", "@pcve_elector", _
                                      "@pcalle", "@pnumero", "@pidc_colonia", "@pidc_cliente"}
        Dim valores() As Object = {idc_usuario, ip, pc, usuariopc, cambios, nombre, folio, clave, _
                                   calle, numero, idc_colonia, idc_cliente}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_aclientes_chekplus", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function guardar_cuenta_cliente(ByVal idc_cliente As Integer, ByVal idc_banco As Integer, ByVal num_cuenta As String, _
                                           ByVal idc_usuario As Integer, ByVal ip As String, ByVal pc As String, ByVal usuariopc As String, _
                                           ByVal tipo As Char, ByVal cambios As String, ByVal SFOLIO As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pidc_banco", "@pnum_cuenta", "@pidc_usuario", _
                                   "@pdirecip", "@pnombrepc", "@pusuariopc", "@ptipom", "@pcambios", "@SFOLIO"}
        Dim valores() As Object = {idc_cliente, idc_banco, num_cuenta, idc_usuario, _
                                    ip, pc, usuariopc, tipo, cambios, SFOLIO}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_acuentasclien", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function cuentas_cliente(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_cuentascli", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Clientes_Ubicacion_LatLong(ByVal idc_cliente As Integer) As DataTable
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("SP_Ubicacion_Cliente", parametros, valores)
            If ds.Tables.Count > 0 Then
                Return ds.Tables(0)
            Else
                Return ds.Tables(0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Clientes_Tipo_Croquis(ByVal cadena As String) As String
        Dim dt As New DataTable
        Dim resultado As String = ""
        Dim gweb As New GWebCD.clsConexion
        Try
            dt = gweb.EjecutaConsulta(cadena)
            If dt.Rows.Count > 0 Then
                resultado = dt.Rows(0).Item(0)
                Return resultado
            Else
                Return resultado
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function contactos_cliente(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@idc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_correos_lista_precios_cliente", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function actualiza_inconvenientes(ByVal idc_cliente As Integer, ByVal idc_usuario As Integer, ByVal inconveniente As String, ByVal ip As String, ByVal pc As String, ByVal usuariopc As String) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pinconveniente ", "@pidc_usuario", "@pdirecip", "@pnombrepc", "@pusuariopc"}
        Dim valores() As Object = {idc_cliente, inconveniente, idc_usuario, ip, pc, usuariopc}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_aclientes_inconvenientes", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Inconvenientes(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_clientes_inconvenientes", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Actualizar_Obs_Publicidad(ByVal idc_cliente As Integer, ByVal obs As String, ByVal cambios As String, ByVal idc_usuario As Integer, ByVal ip As String, _
                                              ByVal pc As String, ByVal usuariopc As String) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pobs", "@pcambios", "@pidc_usuario", "@pdirecip", "@pnombrepc", "@pusuariopc"}
        Dim valores() As Object = {idc_cliente, obs, cambios, idc_usuario, ip, pc, usuariopc}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_aclientes_publicidad", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Observaciones_Publicidad(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_clientes_publicidad", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function buscar_precio_art_cot(ByVal idc_articulo As Integer, ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_articulo", "@pidc_cliente"}
        Dim valores() As Object = {idc_articulo, idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("SP_Precios_Cotiza_Art", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function buscar_agregar_art_cot(ByVal valor As String, ByVal tipo As Integer) As DataSet
        Dim parametros() As String = {"@pvalor", "@ptipo"}
        Dim valores() As Object = {valor, tipo}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_buscar_art_cot_add", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function gurdar_cotizacion_cliente(ByVal idc_usuario As Integer, ByVal ip As String, ByVal pc As String, ByVal usupc As String, ByVal total As Integer, ByVal cadena As String, ByVal idagente As String, ByVal idcliente As Integer, ByVal folio As Integer, ByVal vfecha As String, ByVal fecha_salida As String)
        Dim parametros() As String = {"@pidc_usuario", "@pdirecip", "@pnombrepc", "@pusuariopc", "@ptotal2", "@pcadena2", "@vida", "@vidcli", "@vfolioa", "@vfecha", "@vfecha_salida", "@pver"}
        Dim valores() As Object = {idc_usuario, ip, pc, usupc, total, cadena, idagente, idcliente, folio, vfecha, fecha_salida, True}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_aagentes_act_cotizacion_nueva", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function clientes_por_agente(ByVal pidc_agente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_agente"}
        Dim valores() As Object = {pidc_agente}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_clientes_x_agente_visitas_individual", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            valores = Nothing
            parametros = Nothing
            pidc_agente = Nothing
        End Try
        Return ds
    End Function

    Public Function clientes_x_agente_visitas_individual_x_dia(ByVal idc_agente As Integer, ByVal dia As String, ByVal igual As Boolean) As DataSet
        Dim parametros() As String = {"@pidc_agente", "@dia", "@igual"}
        Dim valores() As Object = {idc_agente, dia, igual}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_clientes_x_agente_visitas_individual_x_dia1", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ver_ficha_cliente(ByVal pidc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {pidc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_ver_fichacliente", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
            pidc_cliente = Nothing
        End Try
    End Function

    Public Function ver_productos_master_cliente(ByVal pidc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {pidc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_clientes_articulos", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
            pidc_cliente = Nothing
        End Try
    End Function

    Public Function Cuentas_X_Cobrar(ByVal pidc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {pidc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_rep_cuentas_x_cobrar1_FTP", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            valores = Nothing
            parametros = Nothing
            pidc_cliente = Nothing
        End Try
    End Function

    Public Function Ver_Grupo_cliente(ByVal pidc_gpocli As Integer) As DataSet
        Dim parametros() As String = {"@pidc_gpocli"}
        Dim valores() As Object = {pidc_gpocli}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_ver_gpocli", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function

    Public Function Ver_Historial_Ventas_6meses(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As String = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_venxfac_arti_ult6", parametros, valores)

        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try

    End Function

    Public Function Ver_Reporte_Cuentas_Por_Cobrar(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As String = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_ver_cuentasxcobrar2", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function

    Public Function buscarclientes() As DataSet
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_buscar_cliente", Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

    Public Function buscarclientes(ByVal nombre As String) As DataSet
        Dim parametros() As String = {"@pvalor"}
        Dim valores() As Object = {nombre}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_bclientes_ventas", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function

    Public Function cargarcombo_prueba() As DataSet
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_combo_tipos_rfc", Nothing, Nothing)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Ver_Proyectos_Cliente(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("SP_VER_PROYECTOS", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function

    Public Function ver_confirmacion_pago(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_cliente_confirmacion_pago", parametros, valores)

        Catch ex As Exception
            Throw ex
        Finally
            valores = Nothing
            parametros = Nothing
        End Try
    End Function

    Public Function ver_datos_cliente(ByVal pidc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {pidc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_datos_cliente", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
            pidc_cliente = Nothing
        End Try
    End Function

    Public Function Carga_Lista_Master_Cot(ByVal idc_cliente As Integer, ByVal idc_almacen As Integer, ByVal embarques As Boolean, ByVal idc_sucursal As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pidc_almacen", "@pembarques", "@PIDC_SUCURSAL"}
        Dim valores() As Object = {idc_cliente, idc_almacen, embarques, idc_sucursal}
        Try
            Return GWebCD.clsConexion.EjecutaSP("SP_clientes_articulos_individuales_almacen_rangos", parametros, valores)
            'sp_clientes_articulos_individuales_almacen_rangos()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Carga_Lista_Master_Cot_nueva(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_articulos_master_cliente", parametros, valores)
            'sp_clientes_articulos_individuales_almacen_rangos()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Carga_Lista_Master_Ped2(ByVal idc_cliente As Integer, ByVal idc_almacen As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pidc_almacen"}
        Dim valores() As Object = {idc_cliente, idc_almacen}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_clientes_articulos_individuales_almacen2", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function




    Public Function Carga_Lista_Master_Ped(ByVal idc_cliente As Integer, ByVal idc_almacen As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("SP_clientes_articulos_master", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'funcion que manda a articulos_a_vender.aspx MIC add 29-05-2015
    Public Function art_a_vender(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_clientes_articulos_vender", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    

End Class
