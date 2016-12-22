Public Class Guardar_Pedido
    Dim ds As New DataSet
    'Public Function guardarP3(ByVal idc_cliente As Integer, ByVal monto As Double, ByVal idc_sucursal As Integer, ByVal desiva As Boolean, ByVal idc_iva As Integer, _
    '                          ByVal idc_usuario As Integer, ByVal idc_almacen As Integer, ByVal direcip As String, ByVal nombrepc As String, _
    '                          ByVal usuariopc As String, ByVal tipom As Char, ByVal cambios As String, ByVal darti As String, ByVal totart As Integer, _
    '                          ByVal fecha_ent As Date, ByVal proye As Boolean, ByVal idpro As Integer, ByVal occ As String, _
    '                          ByVal idc_colonia As Integer, ByVal calle As String, ByVal numero As String, ByVal cod_postal As Integer, ByVal obs As String, _
    '                          ByVal IDC_FOLIOPRECP As Integer, ByVal sipasa As Integer, ByVal entdir As Integer, ByVal bitocc As Boolean, ByVal bitcro As Boolean, _
    '                          ByVal dsinexi As String, ByVal DCOSBAJO As String, ByVal folio As Integer, ByVal BITLLA As Boolean, ByVal MENSAJE As String, ByVal SIPASA2 As Integer, _
    '                          ByVal FLETE As Double, ByVal recoge As String, ByVal idc_general As Integer, ByVal tipo As Integer, ByVal idc_occli As Integer, ByVal idc_sucursal_recoge As Integer, _
    '                          ByVal Vtipop As Integer, ByVal idc_banco As Integer, ByVal fecha_deposito As Date, ByVal monto_pago As Double, ByVal observaciones_pago As String, ByVal confirmar_pago As Boolean, _
    '                          ByVal cadena_acti As String, ByVal total_acti As String, ByVal total2_acti As Integer, ByVal cadena2_acti As String) As Object

    '    Dim parametros() As String = {"@pidc_cliente", "@pmonto", "@pidc_sucursal", "@pdesiva", "@pidc_iva", "@pidc_usuario", _
    '                                  "@pidc_almacen", "@pdirecip", "@pnombrepc", "@pusuariopc", "@ptipom", "@pcambios", "@pdarti", "@ptotart", "@pfecha_ent", _
    '                                  "@pproye", "@pidpro", "@pocc", "@pidc_colonia", "@pcalle", "@pnumero", "@pcod_postal", "@pobs", "@PIDC_FOLIOPRECP", "@PSIPASA", "@pentdir", "@pbitocc", _
    '                                  "@pbitcro", "@pdsinexi", "@PDCOSBAJO", "@pfolio", "@PBITLLA", "@PMENSAJE", "@PSIPASA2", "@PFLETE", "@precoge", "@pidc_general", "@ptipo", "@pidc_occli", "@pidc_sucursal_recoge", _
    '                                  "@Vtipop", "@pidc_banco", "@pfecha_deposito", "@pmonto_pago", "@pobservaciones_pago", "@Pconfirmar_pago", "@pcadena_acti", _
    '                                  "@ptotal_acti", "@ptotal2_acti", "@pcadena2_acti"}
    '    Dim valores() As Object = {idc_cliente, monto, idc_sucursal, desiva, idc_iva, idc_usuario, idc_almacen, _
    '                               direcip, nombrepc, usuariopc, tipom, cambios, darti, totart, fecha_ent, _
    '                               proye, idpro, occ, idc_colonia, calle, numero, cod_postal, obs, IDC_FOLIOPRECP, sipasa, entdir, bitocc, _
    '                               bitcro, dsinexi, DCOSBAJO, folio, BITLLA, MENSAJE, SIPASA2, FLETE, recoge, idc_general, tipo, idc_occli, idc_sucursal_recoge, _
    '                               Vtipop, idc_banco, fecha_deposito, monto_pago, observaciones_pago, confirmar_pago, cadena_acti, _
    '                               total_acti, total2_acti, cadena2_acti}

    '    Try
    '        Return GWebCD.clsConexion.EjecutaSP("sp_apedidos3", parametros, valores)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function


    'Public Function apreped_cambios_precios(ByVal idc_cliente As Integer, ByVal monto As Double, ByVal idc_sucursal As Integer, ByVal desiva As Boolean, ByVal idc_iva As Integer, _
    '                          ByVal idc_usuario As Integer, ByVal idc_almacen As Integer, ByVal direcip As String, ByVal nombrepc As String, _
    '                          ByVal usuariopc As String, ByVal tipom As Char, ByVal cambios As String, ByVal darti As String, ByVal totart As Integer, _
    '                          ByVal fecha_ent As Date, ByVal proye As Boolean, ByVal idpro As Integer, ByVal occ As String, _
    '                          ByVal idc_colonia As Integer, ByVal calle As String, ByVal numero As String, ByVal cod_postal As Integer, ByVal obs As String, _
    '                          ByVal IDC_FOLIOPRECP As Integer, ByVal sipasa As Integer, ByVal entdir As Integer, ByVal bitcro As Boolean, ByVal bitocc As Boolean, _
    '                          ByVal dsinexi As String, ByVal DCOSBAJO As String, ByVal folio As Integer, ByVal BITLLA As Boolean, ByVal MENSAJE As String, ByVal SIPASA2 As Integer, _
    '                          ByVal tipo As Char, ByVal FLETE As Double, ByVal recoge As String, ByVal idc_occli As Integer, ByVal idc_sucursal_recoge As Integer, _
    '                          ByVal Vtipop As Integer, ByVal idc_banco As Integer, ByVal fecha_deposito As Date, ByVal monto_pago As Double, ByVal observaciones_pago As String, ByVal confirmar_pago As Boolean, _
    '                          ByVal cadena_acti As String, ByVal total_acti As String, ByVal total2_acti As Integer, ByVal cadena2_acti As String) As DataSet
    '    Dim parametros() As String = {"@pidc_cliente", "@pmonto", "@pidc_sucursal", "@pdesiva", "@pidc_iva", "@pidc_usuario", _
    '                                  "@pidc_almacen", "@pdirecip", "@pnombrepc", "@pusuariopc", "@ptipom", "@pcambios", "@pdarti", "@ptotart", "@pfecha_ent", _
    '                                  "@pproye", "@pidpro", "@pocc", "@pidc_colonia", "@pcalle", "@pnumero", "@pcod_postal", "@pobs", "@PIDC_FOLIOPRECP", "@PSIPASA", "@pentdir", "@pbitocc", _
    '                                  "@pbitcro", "@pdsinexi", "@PDCOSBAJO", "@pfolio", "@PBITLLA", "@PMENSAJE", "@PTIPO", "@PSIPASA2", "@PFLETE", "@precoge", "@pidc_occli", "@pidc_sucursal_recoge", _
    '                                  "@Vtipop", "@pidc_banco", "@pfecha_deposito", "@pmonto_pago", "@pobservaciones_pago", "@Pconfirmar_pago", "@pcadena_acti", _
    '                                  "@ptotal_acti", "@ptotal2_acti", "@pcadena2_acti"}
    '    Dim valores() As Object = {idc_cliente, monto, idc_sucursal, desiva, idc_iva, idc_usuario, idc_almacen, _
    '                               direcip, nombrepc, usuariopc, tipom, cambios, darti, totart, fecha_ent, _
    '                               proye, idpro, occ, idc_colonia, calle, numero, cod_postal, obs, IDC_FOLIOPRECP, sipasa, entdir, bitocc, _
    '                               bitcro, dsinexi, DCOSBAJO, folio, BITLLA, MENSAJE, tipo, SIPASA2, FLETE, recoge, idc_occli, idc_sucursal_recoge, _
    '                               Vtipop, idc_banco, fecha_deposito, monto_pago, observaciones_pago, confirmar_pago, cadena_acti, _
    '                               total_acti, total2_acti, cadena2_acti}
    '    Dim ds As New DataSet
    '    Try
    '        ds = GWebCD.clsConexion.EjecutaSP("sp_apreped_cambio_precios", parametros, valores)
    '        Return ds
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    'Public Function sp_apreped_creditos(ByVal idc_cliente As Integer, ByVal monto As Double, ByVal idc_sucursal As Integer, ByVal desiva As Boolean, ByVal idc_iva As Integer, _
    '                          ByVal idc_usuario As Integer, ByVal idc_almacen As Integer, ByVal direcip As String, ByVal nombrepc As String, _
    '                          ByVal usuariopc As String, ByVal tipom As Char, ByVal cambios As String, ByVal darti As String, ByVal totart As Integer, _
    '                          ByVal fecha_ent As Date, ByVal proye As Boolean, ByVal idpro As Integer, ByVal occ As String, _
    '                          ByVal idc_colonia As Integer, ByVal calle As String, ByVal numero As String, ByVal cod_postal As Integer, ByVal obs As String, _
    '                          ByVal IDC_FOLIOPRECP As Integer, ByVal sipasa As Integer, ByVal entdir As Integer, ByVal bitcro As Boolean, ByVal bitocc As Boolean, _
    '                          ByVal dsinexi As String, ByVal DCOSBAJO As String, ByVal folio As Integer, ByVal BITLLA As Boolean, ByVal MENSAJE As String, ByVal tipo As Char, ByVal SIPASA2 As Integer, _
    '                          ByVal FLETE As Double, ByVal recoge As String, ByVal idc_occli As Integer, ByVal idc_sucursal_recoge As Integer, _
    '                          ByVal Vtipop As Integer, ByVal idc_banco As Integer, ByVal fecha_deposito As Date, ByVal monto_pago As Double, ByVal observaciones_pago As String, ByVal confirmar_pago As Boolean, _
    '                          ByVal cadena_acti As String, ByVal total_acti As String, ByVal total2_acti As Integer, ByVal cadena2_acti As String) As DataSet
    '    Dim paramteros() As String = {"@pidc_cliente", "@pmonto", "@pidc_sucursal", "@pdesiva", "@pidc_iva", "@pidc_usuario", _
    '                                  "@pidc_almacen", "@pdirecip", "@pnombrepc", "@pusuariopc", "@ptipom", "@pcambios", "@pdarti", "@ptotart", "@pfecha_ent", _
    '                                  "@pproye", "@pidpro", "@pocc", "@pidc_colonia", "@pcalle", "@pnumero", "@pcod_postal", "@pobs", "@PIDC_FOLIOPRECP", "@PSIPASA", "@pentdir", _
    '                                  "@pbitcro", "@pbitocc", "@pdsinexi", "@PDCOSBAJO", "@pfolio", "@PBITLLA", "@PMENSAJE", "@PTIPO", "@PSIPASA2", "@PFLETE", "@precoge", "@pidc_occli", "@pidc_sucursal_recoge", _
    '                                  "@Vtipop", "@pidc_banco", "@pfecha_deposito", "@pmonto_pago", "@pobservaciones_pago", "@Pconfirmar_pago", "@pcadena_acti", _
    '                                  "@ptotal_acti", "@ptotal2_acti", "@pcadena2_acti"}
    '    Dim valores() As Object = {idc_cliente, monto, idc_sucursal, desiva, idc_iva, idc_usuario, idc_almacen, _
    '                               direcip, nombrepc, usuariopc, tipom, cambios, darti, totart, fecha_ent, _
    '                               proye, idpro, occ, idc_colonia, calle, numero, cod_postal, obs, IDC_FOLIOPRECP, sipasa, entdir, bitcro, _
    '                               bitocc, dsinexi, DCOSBAJO, folio, BITLLA, MENSAJE, tipo, SIPASA2, FLETE, recoge, idc_occli, idc_sucursal_recoge, _
    '                               Vtipop, idc_banco, fecha_deposito, monto_pago, observaciones_pago, confirmar_pago, cadena_acti, _
    '                               total_acti, total2_acti, cadena2_acti}
    '    Try
    '        Return GWebCD.clsConexion.EjecutaSP("sp_apreped_creditos", paramteros, valores)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Public Function apreped_creditos_especial_NC(ByVal idc_cliente As Integer, ByVal monto As Double, ByVal idc_sucursal As Integer, ByVal desiva As Boolean, ByVal idc_iva As Integer, _
    '                          ByVal idc_usuario As Integer, ByVal idc_almacen As Integer, ByVal direcip As String, ByVal nombrepc As String, _
    '                          ByVal usuariopc As String, ByVal tipom As Char, ByVal cambios As String, ByVal darti As String, ByVal totart As Integer, _
    '                          ByVal fecha_ent As Date, ByVal proye As Boolean, ByVal idpro As Integer, ByVal occ As String, _
    '                          ByVal idc_colonia As Integer, ByVal calle As String, ByVal numero As String, ByVal cod_postal As Integer, ByVal obs As String, _
    '                          ByVal IDC_FOLIOPRECP As Integer, ByVal sipasa As Integer, ByVal entdir As Integer, ByVal bitcro As Boolean, ByVal bitocc As Boolean, _
    '                          ByVal dsinexi As String, ByVal DCOSBAJO As String, ByVal folio As Integer, ByVal BITLLA As Boolean, ByVal MENSAJE As String, ByVal tipo As Char, ByVal SIPASA2 As Integer, _
    '                          ByVal FLETE As Double, ByVal recoge As String, ByVal plazo As Integer, ByVal tipopago As Integer, ByVal otro As String, ByVal canminima As String, _
    '                          ByVal contacto As String, ByVal telefono As String, ByVal mail As String, ByVal idc_occli As Integer, ByVal idc_sucursal_recoge As Integer, _
    '                          ByVal cadena_acti As String, ByVal total_acti As String, ByVal total2_acti As Integer, ByVal cadena2_acti As String) As DataSet
    '    Dim parametros() As String = {"@pidc_cliente", "@pmonto", "@pidc_sucursal", "@pdesiva", "@pidc_iva", "@pidc_usuario", _
    '                                  "@pidc_almacen", "@pdirecip", "@pnombrepc", "@pusuariopc", "@ptipom", "@pcambios", "@pdarti", "@ptotart", "@pfecha_ent", _
    '                                  "@pproye", "@pidpro", "@pocc", "@pidc_colonia", "@pcalle", "@pnumero", "@pcod_postal", "@pobs", "@PIDC_FOLIOPRECP", "@PSIPASA", "@pentdir", _
    '                                  "@pbitcro", "@pbitocc", "@pdsinexi", "@PDCOSBAJO", "@pfolio", "@PBITLLA", "@PMENSAJE", "@PTIPO", "@PSIPASA2", "@PFLETE", "@precoge", "@pplazo", _
    '                                  "@ptipopago", "@potro", "@pcanminima", "@pcontacto", "@ptelefono", "@pmail", "@pidc_occli", "@pidc_sucursal_recoge", "@pcadena_acti", _
    '                                  "@ptotal_acti", "@ptotal2_acti", "@pcadena2_acti"}
    '    Dim valores() As Object = {idc_cliente, monto, idc_sucursal, desiva, idc_iva, idc_usuario, idc_almacen, _
    '                               direcip, nombrepc, usuariopc, tipom, cambios, darti, totart, fecha_ent, _
    '                               proye, idpro, occ, idc_colonia, calle, numero, cod_postal, obs, IDC_FOLIOPRECP, sipasa, entdir, bitcro, _
    '                               bitocc, dsinexi, DCOSBAJO, folio, BITLLA, MENSAJE, tipo, SIPASA2, FLETE, recoge, plazo, tipopago, otro, canminima, contacto, telefono, _
    '                               mail, idc_occli, idc_sucursal_recoge, cadena_acti, total_acti, total2_acti, cadena2_acti}
    '    Try
    '        Return GWebCD.clsConexion.EjecutaSP("sp_apreped_creditos_especial_NC", parametros, valores)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function






    Public Function guardarP4(ByVal idc_cliente As Integer, ByVal monto As Double, ByVal idc_sucursal As Integer, ByVal desiva As Boolean, ByVal idc_iva As Integer, ByVal idc_usuario As Integer, _
                              ByVal idc_almacen As Integer, ByVal direcip As String, ByVal nombrepc As String, ByVal usuariopc As String, ByVal tipom As Char, ByVal cambios As String, _
                              ByVal darti As String, ByVal totart As Integer, ByVal fecha_ent As Date, ByVal proye As Boolean, ByVal idpro As Integer, ByVal occ As String, _
                              ByVal idc_colonia As Integer, ByVal calle As String, ByVal numero As String, ByVal cod_postal As Integer, ByVal obs As String, _
                              ByVal IDC_FOLIOPRECP As Integer, ByVal sipasa As Integer, ByVal entdir As Integer, ByVal bitocc As Boolean, ByVal bitcro As Boolean, _
                              ByVal dsinexi As String, ByVal DCOSBAJO As String, ByVal folio As Integer, ByVal BITLLA As Boolean, ByVal MENSAJE As String, ByVal SIPASA2 As Integer, _
                              ByVal FLETE As Double, ByVal recoge As String, ByVal idc_general As Integer, ByVal tipo As Integer, ByVal idc_occli As Integer, ByVal idc_sucursal_recoge As Integer, _
                              ByVal Vtipop As Integer, ByVal idc_banco As Integer, ByVal fecha_deposito As Date, ByVal monto_pago As Double, ByVal observaciones_pago As String, ByVal confirmar_pago As Boolean, _
                              ByVal cadena_acti As String, ByVal total_acti As String, ByVal total2_acti As Integer, ByVal cadena2_acti As String, ByVal vdarti2_nueva As String, ByVal VTOTA_NUEVO As Integer) As Object

        Dim parametros() As String = {"@pidc_cliente", "@pmonto", "@pidc_sucursal", "@pdesiva", "@pidc_iva", "@pidc_usuario", _
                                      "@pidc_almacen", "@pdirecip", "@pnombrepc", "@pusuariopc", "@ptipom", "@pcambios", _
                                      "@pdarti", "@ptotart", "@pfecha_ent", "@pproye", "@pidpro", "@pocc", _
                                      "@pidc_colonia", "@pcalle", "@pnumero", "@pcod_postal", "@pobs", "@PIDC_FOLIOPRECP", "@PSIPASA", "@pentdir", "@pbitocc", _
                                      "@pbitcro", "@pdsinexi", "@PDCOSBAJO", "@pfolio", "@PBITLLA", "@PMENSAJE", "@PSIPASA2", "@PFLETE", "@precoge", "@pidc_general", "@ptipo", "@pidc_occli", "@pidc_sucursal_recoge", _
                                      "@Vtipop", "@pidc_banco", "@pfecha_deposito", "@pmonto_pago", "@pobservaciones_pago", "@Pconfirmar_pago", "@pcadena_acti", _
                                      "@ptotal_acti", "@ptotal2_acti", "@pcadena2_acti", "@pdarti_NUEVA", "@PTOTART_NUEVO"}
        Dim valores() As Object = {idc_cliente, monto, idc_sucursal, desiva, idc_iva, idc_usuario, _
                                   idc_almacen, direcip, nombrepc, usuariopc, tipom, cambios, _
                                   darti, totart, fecha_ent, proye, idpro, occ, _
                                   idc_colonia, calle, numero, cod_postal, obs, IDC_FOLIOPRECP, sipasa, entdir, bitocc, _
                                   bitcro, dsinexi, DCOSBAJO, folio, BITLLA, MENSAJE, SIPASA2, FLETE, recoge, idc_general, tipo, idc_occli, idc_sucursal_recoge, _
                                   Vtipop, idc_banco, fecha_deposito, monto_pago, observaciones_pago, confirmar_pago, cadena_acti, _
                                   total_acti, total2_acti, cadena2_acti, vdarti2_nueva, VTOTA_NUEVO}

        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_apedidos5_esp_mod_web_nuevo", parametros, valores) ' sp_apedidos5_esp 10-08-2015 se cambie este por sp_apedidos5_esp_mod_web
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function apreped_cambios_precios1(ByVal idc_cliente As Integer, ByVal monto As Double, ByVal idc_sucursal As Integer, ByVal desiva As Boolean, ByVal idc_iva As Integer, _
                              ByVal idc_usuario As Integer, ByVal idc_almacen As Integer, ByVal direcip As String, ByVal nombrepc As String, _
                              ByVal usuariopc As String, ByVal tipom As Char, ByVal cambios As String, ByVal darti As String, ByVal totart As Integer, _
                              ByVal fecha_ent As Date, ByVal proye As Boolean, ByVal idpro As Integer, ByVal occ As String, _
                              ByVal idc_colonia As Integer, ByVal calle As String, ByVal numero As String, ByVal cod_postal As Integer, ByVal obs As String, _
                              ByVal IDC_FOLIOPRECP As Integer, ByVal sipasa As Integer, ByVal entdir As Integer, ByVal bitcro As Boolean, ByVal bitocc As Boolean, _
                              ByVal dsinexi As String, ByVal DCOSBAJO As String, ByVal folio As Integer, ByVal BITLLA As Boolean, ByVal MENSAJE As String, ByVal SIPASA2 As Integer, _
                              ByVal tipo As Char, ByVal FLETE As Double, ByVal recoge As String, ByVal idc_occli As Integer, ByVal idc_sucursal_recoge As Integer, _
                              ByVal Vtipop As Integer, ByVal idc_banco As Integer, ByVal fecha_deposito As Date, ByVal monto_pago As Double, ByVal observaciones_pago As String, ByVal confirmar_pago As Boolean, _
                              ByVal cadena_acti As String, ByVal total_acti As String, ByVal total2_acti As Integer, ByVal cadena2_acti As String, ByVal vdarti2_nueva As String, ByVal VTOTA_NUEVO As Integer, ByVal detalles As String, ByVal fleteaut As Decimal) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pmonto", "@pidc_sucursal", "@pdesiva", "@pidc_iva", "@pidc_usuario", _
                                      "@pidc_almacen", "@pdirecip", "@pnombrepc", "@pusuariopc", "@ptipom", "@pcambios", "@pdarti", "@ptotart", "@pfecha_ent", _
                                      "@pproye", "@pidpro", "@pocc", "@pidc_colonia", "@pcalle", "@pnumero", "@pcod_postal", "@pobs", "@PIDC_FOLIOPRECP", "@PSIPASA", "@pentdir", "@pbitocc", _
                                      "@pbitcro", "@pdsinexi", "@PDCOSBAJO", "@pfolio", "@PBITLLA", "@PMENSAJE", "@PTIPO", "@PSIPASA2", "@PFLETE", "@precoge", "@pidc_occli", "@pidc_sucursal_recoge", _
                                      "@Vtipop", "@pidc_banco", "@pfecha_deposito", "@pmonto_pago", "@pobservaciones_pago", "@Pconfirmar_pago", "@pcadena_acti", _
                                      "@ptotal_acti", "@ptotal2_acti", "@pcadena2_acti", "@pdarti_NUEVA", "@PTOTART_NUEVO", "@pdetalles", "@pfleteaut"}
        Dim valores() As Object = {idc_cliente, monto, idc_sucursal, desiva, idc_iva, idc_usuario, idc_almacen, _
                                   direcip, nombrepc, usuariopc, tipom, cambios, darti, totart, fecha_ent, _
                                   proye, idpro, occ, idc_colonia, calle, numero, cod_postal, obs, IDC_FOLIOPRECP, sipasa, entdir, bitocc, _
                                   bitcro, dsinexi, DCOSBAJO, folio, BITLLA, MENSAJE, tipo, SIPASA2, FLETE, recoge, idc_occli, idc_sucursal_recoge, _
                                   Vtipop, idc_banco, fecha_deposito, monto_pago, observaciones_pago, confirmar_pago, cadena_acti, _
                                   total_acti, total2_acti, cadena2_acti, vdarti2_nueva, VTOTA_NUEVO, detalles, fleteaut}
        Dim ds As New DataSet
        Try
            ds = GWebCD.clsConexion.EjecutaSP("sp_apreped_CAMBIO_PRECIOS3_nuevo", parametros, valores)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function sp_apreped_creditos1(ByVal idc_cliente As Integer, ByVal monto As Double, ByVal idc_sucursal As Integer, ByVal desiva As Boolean, ByVal idc_iva As Integer, _
                              ByVal idc_usuario As Integer, ByVal idc_almacen As Integer, ByVal direcip As String, ByVal nombrepc As String, _
                              ByVal usuariopc As String, ByVal tipom As Char, ByVal cambios As String, ByVal darti As String, ByVal totart As Integer, _
                              ByVal fecha_ent As Date, ByVal proye As Boolean, ByVal idpro As Integer, ByVal occ As String, _
                              ByVal idc_colonia As Integer, ByVal calle As String, ByVal numero As String, ByVal cod_postal As Integer, ByVal obs As String, _
                              ByVal IDC_FOLIOPRECP As Integer, ByVal sipasa As Integer, ByVal entdir As Integer, ByVal bitcro As Boolean, ByVal bitocc As Boolean, _
                              ByVal dsinexi As String, ByVal DCOSBAJO As String, ByVal folio As Integer, ByVal BITLLA As Boolean, ByVal MENSAJE As String, ByVal tipo As Char, ByVal SIPASA2 As Integer, _
                              ByVal FLETE As Double, ByVal recoge As String, ByVal idc_occli As Integer, ByVal idc_sucursal_recoge As Integer, _
                              ByVal Vtipop As Integer, ByVal idc_banco As Integer, ByVal fecha_deposito As Date, ByVal monto_pago As Double, ByVal observaciones_pago As String, ByVal confirmar_pago As Boolean, _
                              ByVal cadena_acti As String, ByVal total_acti As String, ByVal total2_acti As Integer, ByVal cadena2_acti As String, ByVal vdarti2_nueva As String, ByVal VTOTA_NUEVO As Integer) As DataSet
        Dim paramteros() As String = {"@pidc_cliente", "@pmonto", "@pidc_sucursal", "@pdesiva", "@pidc_iva", "@pidc_usuario", _
                                      "@pidc_almacen", "@pdirecip", "@pnombrepc", "@pusuariopc", "@ptipom", "@pcambios", "@pdarti", "@ptotart", "@pfecha_ent", _
                                      "@pproye", "@pidpro", "@pocc", "@pidc_colonia", "@pcalle", "@pnumero", "@pcod_postal", "@pobs", "@PIDC_FOLIOPRECP", "@PSIPASA", "@pentdir", _
                                      "@pbitcro", "@pbitocc", "@pdsinexi", "@PDCOSBAJO", "@pfolio", "@PBITLLA", "@PMENSAJE", "@PTIPO", "@PSIPASA2", "@PFLETE", "@precoge", "@pidc_occli", "@pidc_sucursal_recoge", _
                                      "@Vtipop", "@pidc_banco", "@pfecha_deposito", "@pmonto_pago", "@pobservaciones_pago", "@Pconfirmar_pago", "@pcadena_acti", _
                                      "@ptotal_acti", "@ptotal2_acti", "@pcadena2_acti", "@pdarti_NUEVA", "@PTOTART_NUEVO"}
        Dim valores() As Object = {idc_cliente, monto, idc_sucursal, desiva, idc_iva, idc_usuario, idc_almacen, _
                                   direcip, nombrepc, usuariopc, tipom, cambios, darti, totart, fecha_ent, _
                                   proye, idpro, occ, idc_colonia, calle, numero, cod_postal, obs, IDC_FOLIOPRECP, sipasa, entdir, bitcro, _
                                   bitocc, dsinexi, DCOSBAJO, folio, BITLLA, MENSAJE, tipo, SIPASA2, FLETE, recoge, idc_occli, idc_sucursal_recoge, _
                                   Vtipop, idc_banco, fecha_deposito, monto_pago, observaciones_pago, confirmar_pago, cadena_acti, _
                                   total_acti, total2_acti, cadena2_acti, vdarti2_nueva, VTOTA_NUEVO}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_apreped_creditos4_nuevo", paramteros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function apreped_creditos_especial_NC1(ByVal idc_cliente As Integer, ByVal monto As Double, ByVal idc_sucursal As Integer, ByVal desiva As Boolean, ByVal idc_iva As Integer, _
                              ByVal idc_usuario As Integer, ByVal idc_almacen As Integer, ByVal direcip As String, ByVal nombrepc As String, _
                              ByVal usuariopc As String, ByVal tipom As Char, ByVal cambios As String, ByVal darti As String, ByVal totart As Integer, _
                              ByVal fecha_ent As Date, ByVal proye As Boolean, ByVal idpro As Integer, ByVal occ As String, _
                              ByVal idc_colonia As Integer, ByVal calle As String, ByVal numero As String, ByVal cod_postal As Integer, ByVal obs As String, _
                              ByVal IDC_FOLIOPRECP As Integer, ByVal sipasa As Integer, ByVal entdir As Integer, ByVal bitcro As Boolean, ByVal bitocc As Boolean, _
                              ByVal dsinexi As String, ByVal DCOSBAJO As String, ByVal folio As Integer, ByVal BITLLA As Boolean, ByVal MENSAJE As String, ByVal tipo As Char, ByVal SIPASA2 As Integer, _
                              ByVal FLETE As Double, ByVal recoge As String, ByVal plazo As Integer, ByVal tipopago As Integer, ByVal otro As String, ByVal canminima As String, _
                              ByVal contacto As String, ByVal telefono As String, ByVal mail As String, ByVal idc_occli As Integer, ByVal idc_sucursal_recoge As Integer, _
                              ByVal cadena_acti As String, ByVal total_acti As String, ByVal total2_acti As Integer, ByVal cadena2_acti As String, ByVal vdarti2_nueva As String, ByVal VTOTA_NUEVO As Integer) As DataSet
        Dim parametros() As String = {"@pidc_cliente", "@pmonto", "@pidc_sucursal", "@pdesiva", "@pidc_iva", "@pidc_usuario", _
                                      "@pidc_almacen", "@pdirecip", "@pnombrepc", "@pusuariopc", "@ptipom", "@pcambios", "@pdarti", "@ptotart", "@pfecha_ent", _
                                      "@pproye", "@pidpro", "@pocc", "@pidc_colonia", "@pcalle", "@pnumero", "@pcod_postal", "@pobs", "@PIDC_FOLIOPRECP", "@PSIPASA", "@pentdir", _
                                      "@pbitcro", "@pbitocc", "@pdsinexi", "@PDCOSBAJO", "@pfolio", "@PBITLLA", "@PMENSAJE", "@PTIPO", "@PSIPASA2", "@PFLETE", "@precoge", "@pplazo", _
                                      "@ptipopago", "@potro", "@pcanminima", "@pcontacto", "@ptelefono", "@pmail", "@pidc_occli", "@pidc_sucursal_recoge", "@pcadena_acti", _
                                      "@ptotal_acti", "@ptotal2_acti", "@pcadena2_acti", "@pdarti_NUEVA", "@PTOTART_NUEVO"}
        Dim valores() As Object = {idc_cliente, monto, idc_sucursal, desiva, idc_iva, idc_usuario, idc_almacen, _
                                   direcip, nombrepc, usuariopc, tipom, cambios, darti, totart, fecha_ent, _
                                   proye, idpro, occ, idc_colonia, calle, numero, cod_postal, obs, IDC_FOLIOPRECP, sipasa, entdir, bitcro, _
                                   bitocc, dsinexi, DCOSBAJO, folio, BITLLA, MENSAJE, tipo, SIPASA2, FLETE, recoge, plazo, tipopago, otro, canminima, contacto, telefono, _
                                   mail, idc_occli, idc_sucursal_recoge, cadena_acti, total_acti, total2_acti, cadena2_acti, vdarti2_nueva, VTOTA_NUEVO}
        Try
            Return GWebCD.clsConexion.EjecutaSP("sp_apreped_creditos_especial_nc3_ESP_nuevo", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
