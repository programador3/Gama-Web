Imports System.Data.SqlTypes
Imports System.Runtime.InteropServices

Public Class Productos
    Public Function articulo_acabados(ByVal idc_articulo As Integer) As DataSet
        Dim parametros() As String = {"@pidc_articulo"}
        Dim valores() As Object = {idc_articulo}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_especificaciones_arti_rel", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function buscar_articulo_sencillo(ByVal valor As String) As DataSet
        Dim ds As New DataSet
        Dim parametros() As String = {"@pvalor"}
        Dim valores() As Object = {valor}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_buscar_articulos_sencillo_UNIMED", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function grupo_producto(ByVal idc_articulo As Integer) As DataSet
        Dim ds As New DataSet
        Try
            ds.Tables.Add(GWebCD.clsConexion.EjecutaConsulta("select * from v_gpo_arti_Activos with(nolock) where idc_gpoart=" & idc_articulo))
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function datos_articulo(ByVal idc_articulo As Integer) As DataSet
        Dim ds As New DataSet
        Dim parametros() As String = {"@pidc_articulo"}
        Dim valores() As Object = {idc_articulo}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_datos_articulo", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function cantidad_sugerida(ByVal idc_articulo As Integer, ByVal cantidad As Decimal) As DataSet
        ''SP_CONVERSION_ARTICULO @PIDC_ARTICULOD=96, @PCANTIDAD=.200
        Dim ds As New DataSet
        Dim parametros() As String = {"@PIDC_ARTICULOD", "@PCANTIDAD"}
        Dim valores() As Object = {idc_articulo, cantidad}
        Try
            ds = GWebCD.clsConexion.EjecutaSP("SP_CONVERSION_ARTICULO", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function consulta(ByVal idc As Integer, ByVal idc_ced As Integer, ByVal fecha As Date) As DataTable
        Dim dt As New DataTable()
        Dim puerto As Integer = 0
        Try
            dt = GWebCD.clsConexion.EjecutaConsulta("select dbo.fn_cambio_costo_arti_cedis_fecha (" & idc & "," & idc_ced & "," & fecha & ") as cambio")
            'Datos("SELECT TOP 1 puerto FROM correo_puerto WITH (nolock)")
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function codigos_producto(ByVal idc_articulo As Integer) As DataSet
        Dim parametros() As String = {"@pidc_articulo"}
        Dim valores() As Object = {idc_articulo}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_vercodigos", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ficha_tecnica(ByVal idc_articulo As Integer)
        Dim parametros() As String = {"@pidc_articulo"}
        Dim valores() As Object = {idc_articulo}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_bficha_tecnica", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function buscar_ultimo_precio_cliente_prod(ByVal idc_cliente As Integer, ByVal idc_articulo As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pidc_articulo"}
        Dim valores() As Object = {idc_cliente, idc_articulo}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_ultimo_precio_cliente ", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function buscar_productos(ByVal codigo As Object, ByVal tipo As String, ByVal idc_sucursal As Integer, ByVal idc_usuario As Integer) As DataSet
        Dim parametros() As String = {"@pvalor", "@ptipo", "@pidc_sucursal", "@pidc_usuario"}
        Dim valores() As String = {codigo.ToString.Trim, tipo, idc_sucursal, idc_usuario}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_buscar_articulo_VENTAS_existencias", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing

        End Try
    End Function
    Public Function buscar_productosCliente(ByVal codigo As Object, ByVal tipo As String, ByVal idc_sucursal As Integer, ByVal idc_usuario As Integer, ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pvalor", "@ptipo", "@pidc_sucursal", "@pidc_usuario", "@pidc_cliente"}
        Dim valores() As String = {codigo.ToString.Trim, tipo, idc_sucursal, idc_usuario, idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_buscar_articulo_VENTAS_existencias", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing

        End Try
    End Function
    Public Function buscar_precio_producto(ByVal codigo As Integer, ByVal idc_cliente As Integer, ByVal idc_sucursal As Integer) As DataSet
        Dim parametros() As String = {"@pidc_articulo", "@pidc_cliente", "@pidc_sucursal"}
        Dim valores() As Object = {codigo, idc_cliente, idc_sucursal}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_precio_cliente_cedis", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function

    Public Function buscar_precio_producto_nuevo(ByVal codigo As Integer, ByVal idc_cliente As Integer, ByVal idc_sucursal As Integer, ByVal cantidad As Decimal, ByVal cambiolista As Boolean) As DataSet
        Dim parametros() As String = {"@pidc_articulo", "@pidc_cliente", "@pidc_sucursal", "@pcantidad", "@pcambiolista"}
        Dim valores() As Object = {codigo, idc_cliente, idc_sucursal, cantidad, cambiolista}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_precio_cliente_cedis_rangos", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function
    Public Function buscar_precio_producto_nuevo1(ByVal codigo As Integer, ByVal idc_cliente As Integer, ByVal idc_sucursal As Integer, ByVal cantidad As Decimal, ByVal cambiolista As Boolean, ByVal especif As String, ByVal num_especif As Integer) As DataSet
        Dim parametros() As String = {"@pidc_articulo", "@pidc_cliente", "@pidc_sucursal", "@pcantidad", "@pcambiolista", "@pespecificaciones", "@pnum_especif"}
        Dim valores() As Object = {codigo, idc_cliente, idc_sucursal, cantidad, cambiolista, especif, num_especif}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_precio_cliente_cedis_rangos1", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function
    Public Function buscar_existencia_articulo(ByVal idc_alamacen As Integer, ByVal idc_articulo As Integer, <Out()> ByRef exif As Decimal)
        Dim smallint As SqlInt16
        smallint = SqlInt16.Parse(idc_alamacen)
        Dim parametros() As String = {"@pidc_almacen", "@pidc_articulo", "@pexif"}

        Dim valores() As Object = {idc_alamacen, idc_articulo, exif}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_bexistencia_disponible", parametros, valores)

        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function

    Public Function validar_multiplos(ByVal idc_aticulo As Integer, ByVal cantidad As Integer) As DataSet
        Dim parametros() As String = {"@pidc_articulo", "@pcantidad"}
        Dim valores() As Object = {idc_aticulo, cantidad}

        Try
            Return GWebCD.clsConexion.EjecutaSP("SP_arti_conv_int", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function

    Public Function Articulo_calculado(ByVal idc_articulo As Integer) As DataSet
        Dim parametros() As String = {"@pidc_articulo"}
        Dim valores() As Object = {idc_articulo}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_bgastos_chqseg", parametros, valores)

        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function

    Public Function Nota_Credito_Automatica(ByVal idc_cliente As Integer, ByVal idc_articulo As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pidc_articulo"}
        Dim valores() As Object = {idc_cliente, idc_articulo}
        Try
            Return GWebCD.clsConexion.EjecutaSP("SP_nc_auto_CLIENTE_articulo", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            parametros = Nothing
            valores = Nothing
        End Try
    End Function

    Public Function Credito_Disponible_Cliente(ByVal idc_cliente As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_credito_disponible", parametros, valores)
        Catch ex As Exception
            Throw ex
        Finally
            valores = Nothing
            parametros = Nothing
        End Try
    End Function

    Public Function Articulos_Cantidad_Permitida(ByVal cadena As String, ByVal idc_cliente As Integer, ByVal tot As Integer, ByVal idc_alamacen As Integer) As Object
        Dim parametros() As String = {"@pcadena", "@pidc_cliente", "@ptot", "@PIDC_ALMACEN"}
        Dim valores() As Object = {cadena, idc_cliente, tot, idc_alamacen}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("SP_Articulos_Cantidad_Permitida", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                Return CBool(ds.Tables(0).Rows(0).Item(0))
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Articulos_Entrega_Directa(ByVal cadenapeso As String, ByVal idc_cliente As Integer, ByVal totalarticulos As Double) As Boolean
        Dim paramtros() As String = {"@pcadena", "@pidc_cliente", "@ptot"}
        Dim valores() As Object = {cadenapeso, idc_cliente, totalarticulos}
        Dim ds As DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("SP_Articulos_Entrega_Directa", paramtros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                Return CBool(ds.Tables(0).Rows(0).Item(0))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Monto_Minimo_Venta(ByVal idc_cliente As Integer) As Double
        Dim parametros() As String = {"@pidc_cliente"}
        Dim valores() As Object = {idc_cliente}
        Dim ds As DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("SP_Monto_Minimo_Venta", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                Return CDbl(ds.Tables(0).Rows(0).Item(0))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Volumen_Carga(ByVal cadenapeso As String, ByVal tipocamion As Integer, ByVal totalarticulos As Integer) As Double
        Dim parametros() As String = {"@pcadena", "@pidc_tipocam", "@ptot"}
        Dim valores() As Object = {cadenapeso, tipocamion, totalarticulos}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("Sp_Carga_Volmumen", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                Return CDbl(ds.Tables(0).Rows(0).Item(0))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Cambio_Precios(ByVal cadena As String, ByVal totalarticulos As Integer, ByVal idc_cliente As Integer, ByVal idc_sucursal As Integer, ByVal vcambios_lista As Boolean) As Boolean
        Dim parametros() As String = {"@pdarti", "@pnum", "@PIDC_CLIENTE", "@PIDC_SUCURSAL", "@pcambios"}
        Dim valores() As Object = {cadena, totalarticulos, idc_cliente, idc_sucursal, vcambios_lista}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("SP_Cambio_Precios", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                Return CBool(ds.Tables(0).Rows(0).Item(0))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Unica_Venta(ByVal cadena As String, ByVal totalarticulos As Integer) As Boolean
        Dim parametros() As String = {"@PARTIUV", "@PTOTA"}
        Dim valores() As Object = {cadena, totalarticulos}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("SP_VALIDAR_UVENCOM", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                Return CBool(ds.Tables(0).Rows(0).Item(0))
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Cliente_Bloqueado(ByVal idc_cliente As Integer) As Boolean
        Dim parametros() As String = {"@pidc_Cliente"}
        Dim valores() As Object = {idc_cliente}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("SP_Cliente_Bloqueado", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Verificar_Limite(ByVal idc_cliente As Integer, ByVal total As Double) As Boolean
        Dim parametros() As String = {"@pidc_cliente", "@PMONTO"}
        Dim valores() As Object = {idc_cliente, total}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("SP_Saldo_Total_Cliente", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
