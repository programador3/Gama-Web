Imports presentacion
Imports System.Data
Imports System.IO
Partial Class Pedidos_m2
    Inherits System.Web.UI.Page
    Dim contador As Integer

    Sub aportaciones()
        Try
            Dim sbt As String = txttotal.Text
            Dim txtflete As String = txtmaniobras.Text
            Dim tx_pedido As New DataSet
            Dim tx_comi, tx_comiv, tx_comifin, tx_maniobras, tx_ped As New DataTable

            If Not Session("dt_productos_lista") Is Nothing Then
                tx_pedido.Tables.Add(DirectCast(Session("dt_productos_lista"), DataTable).Copy())
            End If

            tx_comi.Columns.Add("codigo", GetType(String))
            tx_comi.Columns.Add("desart", GetType(String))
            tx_comi.Columns.Add("vendedor", GetType(Decimal))
            tx_comi.Columns.Add("tmk", GetType(Decimal))
            tx_comi.Columns.Add("margen", GetType(Decimal))
            tx_comi.Columns.Add("venmastmk", GetType(Decimal))
            tx_comi.Columns.Add("precio_lista", GetType(Decimal))
            tx_comi.Columns.Add("apovenlis", GetType(Decimal))
            tx_comi.Columns.Add("aportacionven", GetType(Decimal))
            tx_comi.Columns.Add("ven", GetType(Decimal))
            tx_comi.Columns.Add("venlis", GetType(Decimal))




            If Not tx_pedido Is Nothing Then
                tx_ped = tx_pedido.Tables(0).DefaultView.ToTable(False, "idc_articulo", "cantidad", "precioreal", "costo", "descripcion", "codigo", "comercial", "precio_lista")

                Dim vidc_articulo As Decimal = Nothing
                Dim vcantidad As Double = Nothing
                Dim VPRECIO As Decimal = Nothing
                Dim vcosto As Decimal = Nothing
                Dim vdesart As String = String.Empty
                Dim vcodigo As String = String.Empty
                Dim vcomercial As Boolean = Nothing
                Dim vventaart As Decimal = Decimal.Zero
                Dim vxcosto As Decimal = Decimal.Zero
                Dim vmargen As Decimal = Decimal.Zero
                Dim vmargentmk As Decimal = Decimal.Zero
                Dim vmargenven As Decimal = Decimal.Zero
                Dim vpl As Decimal = Decimal.Zero
                Dim vprecio_lista As Decimal = Decimal.Zero
                Dim vventasub As Decimal = Decimal.Zero
                Dim vgastooperativo As Decimal = Session("")
                Dim vventaartlis As Decimal = Decimal.Zero
                Dim vxventa As Decimal = Decimal.Zero
                Dim vxventalis As Decimal = Decimal.Zero
                'Dim vgastooperativo As Decimal = 0
                Dim vmargenlis As Decimal = Decimal.Zero
                Dim vmargencompartido As Decimal = Decimal.Zero
                Dim vmargencompartidolis As Decimal = Decimal.Zero
                Dim vaportacionven As Decimal = Decimal.Zero
                Dim vaportacionvenlis As Decimal = Decimal.Zero
                Dim vmargenvenlis As Decimal = Decimal.Zero
                '15-05-2015
                Dim vdistancia As Decimal = Session("")

                Dim datos_clientes() As Object = Session("datos_clientes_pedidos")

                Dim rfccliente As String = ""
                If Not datos_clientes Is Nothing Then
                    rfccliente = datos_clientes(1)
                End If

                vventasub = Math.Round(sbt / (1 + (Session("nuevoiva") / 100)), 2)

                If rfccliente.StartsWith("*") Then
                    If Not Session("gastooperativo") = Nothing Then
                        vgastooperativo = Session("gastooperativo")
                        vgastooperativo = Math.Round(vgastooperativo / (1 + (Session("nuevoiva") / 100)), 4)
                    Else
                        vgastooperativo = 0
                    End If

                Else

                    If Not Session("gastooperativo") = Nothing Then
                        vgastooperativo = Session("gastooperativo")
                    Else
                        vgastooperativo = 0
                    End If

                End If

                If Not Session("tipo_de_entrega") = 1 Then
                    vgastooperativo = 0
                End If


                '11-05-2015
                If Not Session("distanciaentrega") = Nothing Then
                    vdistancia = Session("distanciaentrega")
                Else
                    vdistancia = 1000 'para que salga poca comision
                End If


                'obtener el porcentaje de aportacion segun la distancia
                Dim dt_rpc As DataTable
                Dim gweb As New GWebCN.Fletes
                dt_rpc = gweb.porcentaje_comision(1, vdistancia) ' 1 es vendedor directo


                Dim vporcecomi As Decimal
                If dt_rpc.Rows.Count > 0 Then
                    vporcecomi = dt_rpc.Rows(0).Item("porcentaje")
                Else
                    vporcecomi = 0
                End If
                'hasta aqui 11-05-2015

                'dt.Rows(i).Item(5) / (1 + (iva_ant / 100))
                Dim row_tx_comi As DataRow
                If tx_ped.Rows.Count > 0 Then
                    For i As Integer = 0 To tx_ped.Rows.Count - 1
                        If tx_ped.Rows(i).Item("comercial") = True Then
                            vidc_articulo = tx_ped.Rows(i).Item("idc_articulo").ToString()
                            vcantidad = tx_ped.Rows(i).Item("cantidad").ToString()
                            vpl = tx_ped.Rows(i).Item("precio_lista")

                            If rfccliente.StartsWith("*") Then
                                VPRECIO = FormatNumber(tx_ped.Rows(i).Item("precioreal") / (1 + (Session("NuevoIva") / 100)), 4)
                                vprecio_lista = FormatNumber(vpl / (1 + (Session("NuevoIva") / 100)), 4)
                            Else
                                VPRECIO = FormatNumber(tx_ped.Rows(i).Item("precioreal"), 4)
                                vprecio_lista = vpl
                            End If

                            vcosto = tx_ped.Rows(i).Item("costo").ToString()
                            vdesart = tx_ped.Rows(i).Item("descripcion").ToString()
                            vcodigo = tx_ped.Rows(i).Item("codigo").ToString()
                            vcomercial = tx_ped.Rows(i).Item("comercial").ToString()
                            vventaart = Math.Round(vcantidad * VPRECIO, 2)
                            vventaartlis = Math.Round(vcantidad * vprecio_lista, 2)

                            '11-05-2015 quite linea siguiente para que no sume el costo operativo
                            'vcosto = Math.Round(vcosto + ((vgastooperativo * (vventaart / vventasub)) / vcantidad), 4)
                            vcosto = Math.Round(vcosto, 4)
                            vxventa = vxventa + Math.Round(vcantidad * VPRECIO, 4)

                            vxventalis = vxventalis + Math.Round(vcantidad * vprecio_lista, 4)

                            vxcosto = vxcosto + Math.Round(vcantidad * vcosto, 4)

                            vmargen = Math.Round((1 - (vcosto / VPRECIO)) * 100, 2)

                            vmargenlis = Math.Round((1 - (vcosto / vprecio_lista)) * 100, 2)

                            vmargen = IIf(vmargen < 4, vmargen, IIf(vmargen < 6, 4, IIf(vmargen < 8, 6, IIf(vmargen < 10, 8, IIf(vmargen < 12, 10, vmargen)))))
                            vmargenlis = IIf(vmargenlis < 4, vmargenlis, IIf(vmargenlis < 6, 4, IIf(vmargenlis < 8, 6, IIf(vmargenlis < 10, 8, IIf(vmargenlis < 12, 10, vmargenlis)))))

                            vmargencompartido = Math.Round(vmargen * vporcecomi, 4)

                            vmargencompartidolis = Math.Round(vmargenlis * vporcecomi, 4)



                            If vmargen >= 12 Then
                                vmargenven = Math.Round(vmargencompartido * 1, 4)
                            ElseIf vmargen >= 10 Then
                                vmargenven = Math.Round(vmargencompartido * 0.75, 4)
                            ElseIf vmargen >= 8 Then
                                vmargenven = Math.Round(vmargencompartido * 0.5, 4)
                            ElseIf vmargen >= 6 Then
                                vmargenven = Math.Round(vmargencompartido * 0.25, 4)
                            ElseIf vmargen < 6 Then
                                vmargenven = Math.Round(vmargencompartido * 0.1, 4)
                            End If


                            If vmargenlis >= 12 Then
                                vmargenvenlis = Math.Round(vmargencompartidolis * 1, 4)
                            ElseIf vmargenlis >= 10 Then
                                vmargenvenlis = Math.Round(vmargencompartidolis * 0.75, 4)
                            ElseIf vmargenlis >= 8 Then
                                vmargenvenlis = Math.Round(vmargencompartidolis * 0.5, 4)
                            ElseIf vmargenlis >= 6 Then
                                vmargenvenlis = Math.Round(vmargencompartidolis * 0.25, 4)
                            Else
                                vmargenvenlis = Math.Round(vmargencompartidolis * 0.1, 4)
                            End If

                            vaportacionven = Math.Round(vventaart * vmargenven / 100, 2)
                            vaportacionvenlis = Math.Round(vventaartlis * vmargenvenlis / 100, 2)

                            'INSERT INTO tx_comi (codigo,desart,vendedor,tmk,margen,venmastmk,aportaciontmk,apotmklis,precio_lista) VALUES 
                            '                    (vcodigo,vdesart,vmargenven ,vmargentmk,vmargen,vmargenven +vmargentmk,vaportaciontmk,vaportaciontmklis,vpl )
                            row_tx_comi = tx_comi.NewRow
                            row_tx_comi("codigo") = vcodigo
                            row_tx_comi("desart") = vdesart
                            row_tx_comi("vendedor") = vmargenven
                            row_tx_comi("tmk") = vmargentmk
                            row_tx_comi("margen") = vmargen
                            row_tx_comi("venmastmk") = vmargentmk + vmargenven
                            row_tx_comi("aportacionven") = vaportacionven
                            row_tx_comi("apovenlis") = vaportacionvenlis
                            row_tx_comi("precio_lista") = vpl
                            row_tx_comi("venlis") = vmargenvenlis
                            row_tx_comi("ven") = vmargenven
                            tx_comi.Rows.Add(row_tx_comi)
                        Else
                            tx_ped.Rows.RemoveAt(i)
                        End If
                        If tx_ped.Rows(i).Item("idc_articulo") = 4454 Then
                            tx_maniobras.ImportRow(tx_ped.Rows(i))
                        End If
                    Next

                    Dim aportacionven As Decimal = Decimal.Zero
                    Dim apovenlis As Decimal = Decimal.Zero
                    If tx_comi.Rows.Count > 0 Then
                        For i As Integer = 0 To tx_comi.Rows.Count - 1
                            aportacionven = Math.Round(aportacionven + tx_comi.Rows(i).Item("aportacionven"), 4)
                            apovenlis = Math.Round(apovenlis + tx_comi.Rows(i).Item("apovenlis"), 4)
                        Next
                        txtaportacion.Text = FormatNumber(aportacionven, 4)
                        txtaportacionl.Text = FormatNumber(apovenlis, 4)
                    End If

                    ''Dim vmargensc As Decimal = Math.Round((1 - (vxcosto / vxventa)) * 100, 4)

                    ''gridprod.DataSource = tx_comi
                    ''gridprod.DataBind()
                End If
            Else

            End If
        Catch ex As Exception
            WriteMsgBox("Error al Calcular Aportaciones.\n \u000B \nError:\n" & ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Response.AppendHeader("P3P", "CP=\CAO PSA OUR\")
        btnconsignado.Attributes("onclick") = "return popup_consignado();"
        HttpContext.Current.Response.AddHeader("P3P", "CP=""CAO PSA OUR""")
        If CStr(Session("sidc_usuario")) = "" Then
            Response.Redirect("login.aspx")
        Else
            Me.txtcantidad.Attributes.Add("onkeydown", "return cambiaFoco(event);")
            'txtfolioCHP.Attributes("onkeydown") = "return validar_chekplus2(event);"
            txtbuscar.Attributes("onkeydown") = "return buscar_cliente(event);"
            'btngenerarprepedido.Attributes("onClick") = "abremodal();"
            'Contentbusquedacliente.Style.Add(HtmlTextWriterStyle.Display, "none")
            txtcodigoarticulo.Attributes("onkeydown") = "return buscarart(event);"
            txtFolio.Attributes("onChange") = "cambiotexto();"
            If Not Page.IsPostBack Then
                Session("dt_productos_lista") = Nothing
                imgupcroquis.Attributes("onClick") = "return up_files(1);"
                imgupllamada.Attributes("onClick") = "return up_files(2);"



                'RadioButtons
                estado_rd(True)


                'Cambio de Precios Sucursales
                Session("pxidc_sucursal") = Session("idc_sucursal")
                ivasucursal()
                fecha_txt()
                'Fecha inicial
                'If txtfecha.Text = "" Then

                '    'txtfecha.Text = CStr(FormatDateTime(Now, 2))
                '    'txtfecha.Attributes("onchange") = "return validar_fecha(" & txtfecha.ClientID & "," & txtfecha_max.ClientID & ");"
                'End If


                'Para CEDIS Corresponde
                cedis()


                'Para Cambios de IVA/Frontera
                Session("ivaant") = Session("Xiva")
                Session("idc_ivaant") = Session("Xidc_iva")
                Session("NuevoIva") = Session("Xiva")
                Session("nuevo_idc_iva") = Session("Xidc_iva")
                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*








                txtcreditodisponible.Text = "0.00"
                Dim tipo As Integer
                tipo = Request.QueryString("tipo")
                cargar_consecutivo_folio()
                txtprecio.Attributes("onkeydown") = "return agregararticulo(event)"
                '/TabContainer1.ActiveTabIndex = 0
                CargarMsgbox("", "", True, 2)
                If tipo = 1 Then
                    txtid.Text = Session("idc_cliente")
                    ViewState("dt") = Session("dt_pedido_lista")
                    'Productos_Calculados()
                    'Calcular_Valores_DataTable()
                    'carga_productos_seleccionadas()
                    'cargar_subtotal_iva_total(Session("NuevoIva"))
                    'limpiar_campos()
                    Estado_controles_captura(False)
                    buscar_confirmacion_lista()
                    'formar_cadenas()
                    tbnguardarPP.Enabled = True
                    btnnuevoprepedido.Enabled = True
                    etiqueta_cheque()
                    'txtbuscar.Focus()
                    'botones_pedido()
                    'carga_productos_seleccionadas()
                    'Calcular_Valores_DataTable()
                    'cargar_subtotal_iva_total(Session("NuevoIva"))
                    estado_rd(True)
                    agregar_columnas_tabla_promociones()
                Else
                    'cargar_datos_cliente() 'En caso de que se haga con el Response.Redirect
                    agregar_columnas_dataset()
                    agregar_columnas_tabla_promociones()
                    'btnnuevo.Attributes.Add("onclick", "validarcampos();")
                    '*///  Variables de Session(Remover al Finalizar)//*'
                    'botones_pedido()
                    'Session("idc_sucursal") = 30
                    'Session("idc_usuario") = 327
                    'Session("Xiva") = 14
                    'Session("Xidc_iva") = 4
                    'Session("xidc_almacen") = 1 ''Cambiar al Loggin
                    'Session("Clave_Adi") = Session("Clave_Adi")
                    '///**********************************************///
                    txtcodigoarticulo.Enabled = False
                    'btnvercroquis.Attributes("onClick") = "FileU();"
                    'TabContainer1.Enabled = False
                    etiqueta_cheque()
                    txtbuscar.Focus()
                    botones_pedido()

                End If
                If CStr(Session("idc_cliente")) <> "" Then
                    Dim gweb As New GWebCN.Clientes
                    Dim ds As New DataSet
                    Dim row As DataRow
                    ds = gweb.ver_datos_cliente(Session("idc_cliente"))
                    If ds.Tables(0).Rows.Count Then
                        row = ds.Tables(0).Rows(0)
                        txtid.Text = Session("idc_cliente")
                        txtrfc.Text = row("rfccliente")
                        txtnombre.Text = row("nombre")
                        'cargar_credito_disponible(txtid.Text)
                        Session("Clave_Adi") = row("cveadi")
                        txtstatus.Text = row("idc_bloqueoc")
                        colores(txtstatus.Text)
                        txtbuscar.Enabled = False
                        'txtcodigoarticulo.Enabled = True
                        lblconfirmacion.Visible = Confirmacion_de_Pago()
                        btnOC.Attributes.Add("onclick", "window.open('OC_Digitales_Pendientes.aspx?idc_cliente=" & txtid.Text & "');")
                        btnOC.Enabled = True
                        lkverdatoscliente.NavigateUrl = "window.open('Ficha_cliente_m.aspx?idc_cliente=" & txtid.Text.Trim & "&b=1');"
                        lkverdatoscliente.Enabled = True
                        txtcodigoarticulo.Focus()
                        etiqueta_Iva(Session("NuevoIva"))
                        requiere_oc_croquis()
                        '/cargar_proyectos_cliente(txtid.Text.Trim)
                        'btnnuevoprepedido.Enabled = True
                        tipo_croquis_cliente()

                    End If

                    'Para la Lista de Precios cliente
                    lista_p(Session("idc_cliente"))
                    controles_busqueda_cliente_cancel_selecc(False)
                    controles_busqueda_cliente(False)
                    lblbuscar_cliente.Visible = False
                    Div1.Visible = False


                Else
                    txtbuscar.Enabled = True
                    txtbuscar.Focus()


                End If
                cboentrega.Attributes("onchange") = "return tipo_entrega(this);"
                tbnguardarPP.Attributes("onclick") = "return confirmacion_pago();"
            End If
            lblrestriccion.Text = txtrestriccion.Text.Trim
        End If
    End Sub


    Public Function validar_opcion(ByVal idc_opcion As Integer) As Boolean
        Dim dt As New DataTable
        dt = Session("dt_opciones_usuario")
        Dim rows() As DataRow
        rows = dt.Select("idc_opcion=" & idc_opcion)
        If rows.Length > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Sub tipo_croquis_cliente()
        If txtid.Text.Trim <> "" Then
            Dim gwebcorquis As New GWebCN.Clientes
            Dim tipoC As String = ""
            Dim consulta As String = "select dbo.fn_cliente_tipo_croquis(" & txtid.Text.Trim & ")"
            tipoC = gwebcorquis.Clientes_Tipo_Croquis(consulta)
            If tipoC.Trim = "P" Then
                fucroquis.Visible = False
            Else
                fucroquis.Visible = True
            End If
        End If
    End Sub
    Sub cargar_consecutivo_folio()
        Dim gweb As New GWebCN.Folios
        Dim folio As Object
        folio = gweb.Obtener_Consecutivo_Folio
        If Not CStr(folio) = "" Then
            lblfolio.Text = folio
        End If
    End Sub
    Sub etiqueta_cheque()
        Dim gweb As New GWebCN.CheckPlus
        Dim visible As Boolean
        Try
            visible = gweb.Revisar_Cargar_CheckPlus
            lblcheckplus.Visible = visible
        Catch ex As Exception
            CargarMsgbox("", ex.Message, False, 1)
        End Try
    End Sub

    Protected Sub txtcodigoarticulo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcodigoarticulo.TextChanged
        'Me.txtdescripcion.Text = txtcodigoarticulo.Text
        'txtdescripcion.Focus()

        Try
            Dim ds As New DataSet
            Dim gweb As New GWebCN.Productos
            Dim row As DataRow
            If (txtcodigoarticulo.Text.Trim.Length < 3) Then
                WriteMsgBox("Ingrese Minimo 3 Caracteres Para Realizar la Busqueda.")
                Return
            End If
            If IsNumeric(txtcodigoarticulo.Text) Then
                ds = gweb.buscar_productos(txtcodigoarticulo.Text, "A", Session("idc_sucursal"), Session("idc_usuario"))
                If ds.Tables(0).Rows.Count > 0 Then

                    'row = ds.Tables(0).Rows(0)
                    'cargar_datos_productob_x_codigo(row)
                    'ver_observaciones_articulo(row)
                    'txtcodigoarticulo.Enabled = False
                    '-----------------------------

                    If ds.Tables(0).Rows.Count > 0 Then
                        cboproductos.DataSource = ds.Tables(0)
                        cboproductos.DataTextField = "desart"
                        cboproductos.DataValueField = "idc_articulo"
                        cboproductos.DataBind()



                        'txtcodigoarticulo.Visible = False
                        txtcodigoarticulo.Text = ""

                        cboproductos.Attributes("style") = "width:100%"
                        cboproductos.Visible = True
                        ViewState("dt_productos") = ds.Tables(0)
                        Session("dt_productos_busqueda") = ds.Tables(0)

                        'rowr = ds.Tables(0).Rows(0)
                        ''cargar_datos_productob_x_codigo(row)
                        ''ver_observaciones_articulo(row)
                        ''txtcodigoarticulo.Enabled = False

                        ''Dim index As Integer = e.Item.ItemIndex
                        'Dim dt As New DataTable
                        'dt = ViewState("dt")
                        ''Dim row As DataRow
                        'Dim rows() As DataRow
                        'row = dt.NewRow
                        'Session("rowprincipal") = row
                        'row("idc_articulo") = rowr("idc_articulo")
                        'rows = dt.Select("idc_articulo=" & row("idc_articulo"))
                        'If rows.Length > 0 Then
                        '    WriteMsgBox("El Articulo ya Esta Capturado.")
                        '    txtcodigoarticulo.Text = ""
                        '    txtcodigoarticulo.Focus()
                        '    Return
                        'End If
                        ''row("minimo_venta") = buscar_Existencia_Articulo(row("idc_articulo"))
                        'row("existencia") = buscar_Existencia_Articulo(row("idc_articulo"))
                        'Session("rowprincipal") = row
                        'buscar_precio_producto(row("idc_articulo"))
                        ''/Dim datos() As Object
                        ''/datos = buscar_precio(row("idc_articulo"))
                        'row("Codigo") = rowr("Codigo")
                        'row("Descripcion") = rowr("desart")
                        'row("UM") = rowr("unimed")
                        ''row("Cantidad") = gridresultadosbusqueda.Items(index).Cells(0).Text.Trim
                        ''/row("Precio") = datos(0)
                        ''row("Importe") = gridresultadosbusqueda.Items(index).Cells(0).Text.Trim
                        ''/row("PrecioReal") = datos(4) 'gridresultadosbusqueda.Items(index).Cells(0).Text.Trim 
                        ''/row("Descuento") = datos(2) 'gridresultadosbusqueda.Items(index).Cells(0).Text.Trim 
                        'row("Decimales") = rowr("decimales")
                        'row("Paquete") = rowr("paquete")
                        'row("precio_libre") = rowr("precio_libre")
                        'row("comercial") = rowr("comercial")
                        'row("fecha") = rowr("fecha")
                        'row("obscotiza") = rowr("obscotiza")
                        'row("vende_exis") = rowr("vende_exis")
                        'row("minimo_venta") = rowr("minimo_venta")
                        'row("minimo_venta") = buscar_Existencia_Articulo(row("idc_articulo"))
                        'row("Master") = True
                        'row("mensaje") = rowr("mensaje")
                        'row("Porcentaje") = calculado(row("idc_articulo")) 'gridresultadosbusqueda.Items(index).Cells(0).Text.Trim
                        'If row("Porcentaje") > 0 Then
                        '    row("Calculado") = True 'gridresultadosbusqueda.Items(index).Cells(0).Text.Trim
                        'Else
                        '    row("Calculado") = False
                        'End If
                        ''/row("Nota_Credito") = datos(3) 'gridresultadosbusqueda.Items(index).Cells(0).Text.Trim 
                        'row("Anticipo") = rowr("anticipo")
                        ''/row("Costo") = datos(1) 'gridresultadosbusqueda.Items(index).Cells(0).Text.Trim 
                        ''row("Existencia") = rowr("")
                        'If row("Calculado") = True Then
                        '    row("Cantidad") = 1
                        '    row("Existencia") = 1
                        '    dt.Rows.Add(row)
                        '    ViewState("dt") = dt
                        '    'calcular_valores()
                        '    'Articulos_Calculados()
                        '    grdproductos2.DataSource = ViewState("dt")
                        '    grdproductos2.DataBind()
                        '    'limpiar_controles()

                        '    tbnguardarPP.Enabled = True
                        '    btnnuevoprepedido.Enabled = True
                        '    controles_busqueda_prod(True)
                        '    controles_busqueda_prod_sel_cancel(False)
                        '    txtcodigoarticulo.Attributes.Remove("onfocus")
                        'Else
                        '    'lbl_idc.Text = row("idc_articulo")
                        '    'txtdesc.Text = row("Descripcion")
                        '    'txtcodigoarticulo.Text = row("Codigo")
                        '    'txtum.Text = row("UM")
                        '    txtcodigoarticulo.Attributes("onfocus") = "this.blur()"
                        '    If row("Decimales") = True Then
                        '        'txtcantidad.Attributes("onkeydown") = "agregar_articulo(event);soloNumeros(event,'true');"
                        '        'txtcantidad.Attributes("onkeydown") = "return agregar_articulo(event);"
                        '    Else
                        '        'txtcantidad.Attributes("onkeydown") = "agregar_articulo(event);soloNumeros(event,'false');"
                        '        'txtcantidad.Attributes("onkeydown") = "return agregar_articulo(event);"
                        '    End If


                        '    Session("rowprincipal") = row
                        '    ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>editar_precios_cantidad();</script>", False)

                        '    'dt.Rows.Add(row)
                        '    'ViewState("dt_c_correo") = dt
                        '    'botones_captura(True)
                        '    'txtcantidad.Attributes.Remove("onfocus")
                        '    'txtcantidad.Focus()
                        '    'Aki mandar llamar la pantalla de cantidad.
                        'End If
                    Else
                        ds = gweb.buscar_productos(txtcodigoarticulo.Text, "D", Session("idc_sucursal"), Session("idc_usuario"))
                        If ds.Tables(0).Rows.Count > 0 Then
                            row = ds.Tables(0).Rows(0)
                            'cargar_datos_productob_x_codigo(row)
                            'ver_observaciones_articulo(row)
                            'txtcodigoarticulo.Enabled = False


                        Else
                            WriteMsgBox("No se encontro articulo con esa descripción")
                            txtcodigoarticulo.Focus()
                        End If
                    End If





                    '-------------------------------------








                Else
                    ds = gweb.buscar_productos(txtcodigoarticulo.Text, "D", Session("idc_sucursal"), Session("idc_usuario"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        'row = ds.Tables(0).Rows(0)
                        'cargar_datos_productob_x_codigo(row)
                        'ver_observaciones_articulo(row)
                        'txtcodigoarticulo.Enabled = False



                        cboproductos.DataSource = ds.Tables(0)
                        cboproductos.DataTextField = "desart"
                        cboproductos.DataValueField = "idc_articulo"
                        cboproductos.DataBind()



                        'txtcodigoarticulo.Visible = False
                        txtcodigoarticulo.Text = ""

                        cboproductos.Attributes("style") = "width:100%"
                        cboproductos.Visible = True
                        ViewState("dt_productos") = ds.Tables(0)
                        Session("dt_productos_busqueda") = ds.Tables(0)


                    Else
                        WriteMsgBox("No se Encontro Articulo con esa Descripción.")
                    End If
                End If
            Else
                Dim dt As New DataTable
                ds = gweb.buscar_productos(txtcodigoarticulo.Text, "C", Session("idc_sucursal"), Session("idc_usuario"))
                If ds.Tables(0).Rows.Count > 0 Then
                    'gridresultadosbusqueda.DataSource = ds
                    'gridresultadosbusqueda.DataBind()
                    dt = ds.Tables(0)
                    dt.Columns.Add("desart2")
                    For i As Integer = 0 To dt.Rows.Count - 1
                        dt.Rows(i).Item("desart2") = dt.Rows(i).Item("desart") & "  ||  " & dt.Rows(i).Item("unimed")
                    Next
                    'txtcodigoarticulo.Visible = False
                    txtcodigoarticulo.Text = ""

                    cboproductos.Attributes("style") = "width:100%"
                    cboproductos.Visible = True
                    cboproductos.DataSource = dt
                    cboproductos.DataTextField = "desart2"
                    cboproductos.DataValueField = "idc_articulo"
                    cboproductos.DataBind()
                    ViewState("dt_productos") = ds.Tables(0)
                    'mpeSeleccion.Show()
                Else
                    ds = gweb.buscar_productos(txtcodigoarticulo.Text, "B", Session("idc_sucursal"), Session("idc_usuario"))
                    If ds.Tables(0).Rows.Count > 0 Then

                        dt = ds.Tables(0)
                        dt.Columns.Add("desart2")
                        For i As Integer = 0 To dt.Rows.Count - 1
                            dt.Rows(i).Item("desart2") = dt.Rows(i).Item("desart") & "  ||  " & dt.Rows(i).Item("unimed")
                        Next
                        'txtcodigoarticulo.Visible = False
                        txtcodigoarticulo.Text = ""

                        cboproductos.DataSource = dt
                        cboproductos.DataTextField = "desart2"
                        cboproductos.DataValueField = "idc_articulo"
                        cboproductos.DataBind()
                        cboproductos.Visible = True
                        ViewState("dt_productos") = ds.Tables(0)
                        'gridresultadosbusqueda.DataSource = ds
                        'gridresultadosbusqueda.DataBind()
                        'mpeSeleccion.Show()
                    Else
                        WriteMsgBox("No se Encontro Articulo con esa Descripción.")
                    End If
                End If
                Session("dt_productos_busqueda") = ViewState("dt_productos")
            End If
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "ceerar_busq", "<script>myStopFunction_busq();</script>", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "ceerar_busq", "<script>myStopFunction_busq();</script>", False)
            Throw ex
        Finally
        End Try
    End Sub

    Protected Sub txtbuscar_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbuscar.TextChanged
        'Busqueda de la informacion del cliente
        'Response.Redirect("buscar_cliente.aspx?nombre=" & txtbuscar.Text)
        cargarbusquedaclientes()
    End Sub

    'Protected Sub btngenerarprepedido_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btngenerarprepedido.Click
    '    'Dim dt As New DataTable
    '    'dt = ViewState("dt")
    '    'If dt.Rows.Count > 0 Then
    '    '    'Dim dataset As New DataSet
    '    '    'dataset.Tables.Add(dt)
    '    '    'grdproductos2.DataSource = dataset.Tables(0)
    '    '    'grdproductos2.DataBind()
    '    '    btnescucharll.Attributes("onClick") = "reproducir_llamada();"
    '    '    txtobservaciones.Attributes.Add("onkeypress", " validarmaxlength(this, 100);")
    '    '    txtobservaciones.Attributes.Add("onkeyup", " validarmaxlength(this, 100);")
    '    '    btnseleccionarOC.Attributes("OnClick") = "AbreHija();"  ' Para llamar a la pantalla de OC Pendientes del Cliente.
    '    '    btnverOc.Attributes("OnClick") = "AbreIMGOC();"         ' Para ver la Orden de Compra seleccionada.
    '    '    Panel3.Enabled = True
    '    '    fucroquis.Enabled = True
    '    '    fullamada.Enabled = True
    '    '    cargar_proyectos_cliente(txtid.Text.Trim)
    '    '    txtfolioCHP.Attributes("onblur") = "return validar_chekplus();"
    '    '    AgregarJS()
    '    '    controlesG(False)
    '    '    buscar_confirmacion_lista()
    '    'Else
    '    '    CargarMsgbox("", "La lista de Articulos esta vacía", False, 2)
    '    'End If
    'End Sub

    Sub botones_pedido()
        'Dim dataset As New DataSet
        'dataset.Tables.Add(dt)
        'grdproductos2.DataSource = dataset.Tables(0)
        'grdproductos2.DataBind()
        btnescucharll.Attributes("onClick") = "reproducir_llamada();"
        txtobservaciones.Attributes.Add("onkeypress", " validarmaxlength(this, 100);")
        txtobservaciones.Attributes.Add("onkeyup", " validarmaxlength(this, 100);")
        btnseleccionarOC.Attributes("OnClick") = "AbreHija();"  ' Para llamar a la pantalla de OC Pendientes del Cliente.
        btnverOc.Attributes("OnClick") = "return AbreIMGOC();"         ' Para ver la Orden de Compra seleccionada.
        'Panel3.Enabled = True
        fucroquis.Enabled = True
        fullamada.Enabled = True
        'cargar_proyectos_cliente(txtid.Text.Trim)
        txtfolioCHP.Attributes("onblur") = "return validar_chekplus();"
        'AgregarJS()
        controlesG(False)
        'buscar_confirmacion_lista()
    End Sub
    Sub controlesG(ByVal estado As Boolean)
        txtcodigoarticulo.Enabled = estado
        'btnsalir.Enabled = estado
        'btnnuevoprepedido.Enabled = estado
        'grdproductos2.Enabled = estado
    End Sub
    'Sub cargar_proyectos_cliente(ByVal idc_cliente As Integer)
    '    Dim ds As New DataSet
    '    Dim gweb As New GWebCN.Clientes

    '    Try
    '        ds = gweb.Ver_Proyectos_Cliente(idc_cliente)
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            ViewState("XProyectos") = ds.Tables(0)
    '            cboproyectos.DataSource = ds
    '            cboproyectos.DataValueField = "idc_proyec"
    '            cboproyectos.DataTextField = "nombre"
    '            cboproyectos.DataBind()
    '            cboproyectos.Items.Insert(0, "-Seleccionar Proyecto-")
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOC.Click
        'Session("x") = fileUpload2.FileName
        'CargarMsgbox("", Session("x"), False, 2)
    End Sub
    'Protected Sub gridresultadosbusqueda_ItemCommand(ByVal source As Object, ByVal e As  _
    '                                                 System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridresultadosbusqueda.ItemCommand
    '    'Dim tspan As New TimeSpan
    '    'Dim inicio, final As DateTime
    '    'inicio = Now()
    '    '/////
    '    Dim rowprincipal As DataRow
    '    Dim dt As New DataTable
    '    dt = ViewState("dt")
    '    Try
    '        If e.CommandName = "Seleccionar" Then
    '            '/*/*/////Add columns to the DataRow/////*/*'
    '            rowprincipal = dt.NewRow()
    '            Session("rowprincipal") = rowprincipal
    '            Dim i As Integer
    '            i = e.Item.ItemIndex
    '            rowprincipal("idc_articulo") = gridresultadosbusqueda.Items(i).Cells(1).Text.Trim
    '            rowprincipal("Codigo") = gridresultadosbusqueda.Items(i).Cells(4).Text.Trim
    '            rowprincipal("Descripcion") = gridresultadosbusqueda.Items(i).Cells(2).Text.Trim
    '            rowprincipal("UM") = gridresultadosbusqueda.Items(i).Cells(3).Text.Trim
    '            rowprincipal("Decimales") = gridresultadosbusqueda.Items(i).Cells(5).Text.Trim
    '            Decimales(CBool(rowprincipal("Decimales")))
    '            rowprincipal("Paquete") = gridresultadosbusqueda.Items(i).Cells(6).Text.Trim
    '            rowprincipal("precio_libre") = gridresultadosbusqueda.Items(i).Cells(7).Text.Trim
    '            rowprincipal("comercial") = gridresultadosbusqueda.Items(i).Cells(8).Text.Trim
    '            rowprincipal("vende_exis") = gridresultadosbusqueda.Items(i).Cells(12).Text.Trim
    '            rowprincipal("minimo_venta") = gridresultadosbusqueda.Items(i).Cells(14).Text.Trim
    '            rowprincipal("Master") = gridresultadosbusqueda.Items(i).Cells(13).Text.Trim
    '            rowprincipal("EXISTENCIA") = buscar_Existencia_Articulo(rowprincipal("idc_articulo"))
    '            If gridresultadosbusqueda.Items(i).Cells(9).Text.Trim = "&nbsp;" Then
    '                rowprincipal("fecha") = DBNull.Value
    '            Else
    '                rowprincipal("fecha") = gridresultadosbusqueda.Items(i).Cells(9).Text.Trim
    '            End If

    '            If gridresultadosbusqueda.Items(i).Cells(11).Text.Trim = "&nbsp;" Then
    '                rowprincipal("obscotiza") = ""
    '            Else
    '                rowprincipal("obscotiza") = gridresultadosbusqueda.Items(i).Cells(11).Text.Trim
    '            End If

    '            If gridresultadosbusqueda.Items(i).Cells(10).Text.Trim = "&nbsp;" Then
    '                rowprincipal("mensaje") = ""
    '            Else
    '                rowprincipal("mensaje") = gridresultadosbusqueda.Items(i).Cells(10).Text.Trim
    '            End If

    '            If calculado(rowprincipal("idc_articulo")) = Nothing Then
    '                rowprincipal("calculado") = False
    '                rowprincipal("porcentaje") = 0.0
    '                txtdescripcion.Text = gridresultadosbusqueda.Items(i).Cells(2).Text.Trim
    '                txtcodigoarticulo.Text = gridresultadosbusqueda.Items(i).Cells(4).Text.Trim
    '                txtUM.Text = gridresultadosbusqueda.Items(i).Cells(3).Text.Trim
    '                buscar_precio_producto(gridresultadosbusqueda.Items(i).Cells(1).Text)
    '                ver_observaciones_articulo(rowprincipal)
    '                txtcantidad.Focus()
    '                txtcodigoarticulo.Enabled = False
    '                txtcodigoarticulo.Enabled = False
    '                Estado_controles_captura(True)
    '                If rowprincipal("nota_credito") = True Then
    '                    txtprecio.Enabled = False
    '                End If
    '            Else
    '                If buscar_articulos_duplicados(rowprincipal("idc_articulo")) = False Then
    '                    rowprincipal("precio") = Redondeo_cuatro_decimales(0.0)
    '                    rowprincipal("cantidad") = 1
    '                    rowprincipal("precioreal") = Redondeo_cuatro_decimales(rowprincipal("precio"))
    '                    rowprincipal("calculado") = True
    '                    rowprincipal("porcentaje") = calculado(rowprincipal("idc_articulo"))
    '                    rowprincipal("nota_credito") = False
    '                    dt.Rows.Add(rowprincipal)
    '                    limpiar_campos()
    '                    rowprincipal = Nothing
    '                    Estado_controles_captura(False)
    '                    txtcodigoarticulo.Enabled = True
    '                    Productos_Calculados()
    '                    Calcular_Valores_DataTable()
    '                    cargar_subtotal_iva_total(Session("NuevoIva"))
    '                    carga_productos_seleccionadas()
    '                Else
    '                    limpiar_campos()
    '                    rowprincipal = Nothing
    '                    Estado_controles_captura(False)
    '                    txtcodigoarticulo.Enabled = True
    '                End If
    '            End If
    '        Else
    '            Estado_controles_captura(False)
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    '    mpeSeleccion.Hide() 'Remover de ser necesario
    '    'final = Now()
    '    'tspan = final.Subtract(inicio).Duration()
    '    'CargarMsgbox("", tspan.Duration.ToString(), False, 2)
    'End Sub
    'Protected Sub btncancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancelar.Click
    '    txtcodigoarticulo.Text = ""
    'End Sub



    Protected Sub txtprecio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtprecio.TextChanged
        'Dim Monto As String
        'If Val(txtprecio.Text) > 0 Then
        '    Monto = FormatCurrency(txtprecio.Text, , , TriState.True, TriState.True)
        '    txtprecio.Text = Monto
        'Else
        '    txtprecio.Text = "0.00"
        'End If
        ''If Not txtcodigoarticulo.Text = "" Or txtdescripcion.Text = "" Or txtUM.Text = "" Or txtcantidad.Text = "" Or txtprecio.Text = "" Then
        ''    rowPrincipal = dt.NewRow()
        ''    rowPrincipal("Codigo") = txtcodigoarticulo.Text.Trim
        ''    rowPrincipal("Descripcion") = txtdescripcion.Text.Trim
        ''    rowPrincipal("UM") = txtUM.Text.Trim
        ''    rowPrincipal("Cantidad") = txtcantidad.Text.Trim
        ''    rowPrincipal("Precio") = txtprecio.Text.Trim
        ''    rowPrincipal("Importe") = 50
        ''    rowPrincipal("PrecioReal") = 80
        ''    rowPrincipal("Descuento") = 20
        ''    dt.Rows.Add(rowPrincipal)
        ''    carga_productos_seleccionadas()
        ''    limpiar_campos()
        ''Else
        ''End If
    End Sub


    Protected Sub btnsalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        ViewState.Remove("dt")
        If Not Session("idc_cliente") Is Nothing Then
            Response.Redirect("ficha_cliente_m.aspx")
        Else
            Response.Redirect("menu ventas.aspx")
        End If
    End Sub


    Protected Sub btnagregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnagregar.Click
        'Dim tspan As New TimeSpan
        'Dim inicio, final As DateTime
        'inicio = Now()
        Try
            If Validar_Campos() = True Then ''Que todos los TextBox tengan datos.
                Dim rowprincipal As DataRow
                rowprincipal = Session("rowprincipal")
                If buscar_articulos_duplicados(rowprincipal("idc_articulo")) = True Then
                    WriteMsgBox("Ya Haz Seleccionado Este Articulo.")
                    rowprincipal = Nothing
                    limpiar_campos()
                    Exit Sub
                End If
                If validar_multiplos(rowprincipal("idc_articulo"), txtcantidad.Text) = False Then
                    txtcantidad.Text = ""
                    txtcantidad.Focus()
                    Exit Sub
                End If
                If validar_numerico(txtcantidad.Text.Trim) = False Then
                    CargarMsgbox("", "La cantidad no es correcta, ['0' o '0.000']", False, 2)
                    Exit Sub
                ElseIf validar_numerico(txtprecio.Text) = False Then
                    CargarMsgbox("", "El precio no es correcto, ['1,000.0000']", False, 2)
                    Exit Sub
                End If

                'valida que la cantidad contenga solo los decimales permitidos
                If validar_cantidad_decimales(txtcantidad.Text) = False Then
                    Exit Sub
                End If

                'Valida que el precio sea correcto, solo numeros enteros y cuatro valores decimales...
                If validar_precio_decimales(txtprecio.Text) = False Then
                    CargarMsgbox("", "Precio Incorrecto, debera contener numeros enteros y solo 4 decimales", False, 2)
                    Exit Sub
                End If
                'Checar la existencia...
                If rowprincipal("vende_exis") = True And rowprincipal("comercial") = True Then
                    If No_Vender_Mas_De_Existencia(rowprincipal("idc_articulo"), txtcantidad.Text) = False Then
                        With txtcantidad
                            .Text = ""
                            .Focus()
                        End With
                        Exit Sub
                    End If
                ElseIf rowprincipal("vende_exis") = False And rowprincipal("comercial") = True Then
                    Dim gweb As New GWebCN.Productos
                    Dim row As DataRow
                    Dim ds As New DataSet
                    Try
                        ds = gweb.buscar_existencia_articulo(Session("xidc_almacen"), rowprincipal("idc_articulo"), 0)
                        row = ds.Tables(0).Rows(0)
                        If row("EXISTENCIA_DISPONIBLE") < txtcantidad.Text Then
                            Session("Continuar") = True
                            CargarMsgbox("", "Solo hay en existencia: " & row("EXISTENCIA_DISPONIBLE") & "<br/>" & "¿Desea Continuar?", True, 1)
                        End If
                    Catch ex As Exception
                        CargarMsgbox("", ex.Message, False, 1)
                    Finally
                        gweb = Nothing
                        row = Nothing
                        ds = Nothing
                    End Try
                End If


                'Validar articulo repetido en el pedido

                'If dt.Rows.Count > 0 Then
                '    Dim row As DataRow
                '    For i As Integer = 0 To dt.Rows.Count - 1
                '        row = dt.Rows(i)
                '        If row("idc_articulo") = rowprincipal("idc_articulo") Then
                '            CargarMsgbox("", "Ya haz seleccionado este articulo.", False, 2)
                '            rowprincipal = Nothing
                '            limpiar_campos()
                '            Exit Sub
                '        End If
                '    Next
                'End If

                'Agregar articulo en la TABLA TEMPORAL
                rowprincipal("Codigo") = txtcodigoarticulo.Text.Trim
                rowprincipal("Descripcion") = txtdescripcion.Text.Trim
                rowprincipal("UM") = txtUM.Text.Trim
                rowprincipal("Cantidad") = txtcantidad.Text.Trim


                If Not rowprincipal("nota_credito") = False Then
                    If rowprincipal("Precio") <> Math.Round(CDbl(txtprecio.Text.Trim), 4) Then
                        rowprincipal("precioreal") = Math.Round(CDbl(txtprecio.Text), 4)
                    End If
                Else
                    rowprincipal("precioreal") = Math.Round(CDbl(txtprecio.Text), 4)
                End If

                rowprincipal("Precio") = Redondeo_cuatro_decimales(txtprecio.Text.Trim)
                rowprincipal("Importe") = Redondeo_cuatro_decimales(rowprincipal("precio") * rowprincipal("cantidad"))
                'rowprincipal("Importe") = CDec(rowprincipal("precio")) * CDec(rowprincipal("cantidad"))
                'If rowprincipal("nota_credito") = False Then
                '    rowprincipal("PrecioReal") = rowprincipal("precio")
                'End If
                rowprincipal("Descuento") = Redondeo_cuatro_decimales(rowprincipal("precio") - rowprincipal("PrecioReal"))
                Dim dt As DataTable
                dt = ViewState("dt")
                Session("rowprincipal") = rowprincipal
                'Dim row2 As DataRow
                'row2 = dt.NewRow
                ''row2 = rowprincipal
                'For i As Integer = 0 To rowprincipal.ItemArray.Count - 1
                '    row2.Item(i) = rowprincipal.Item(i)
                'Next
                dt.Rows.Add(rowprincipal.ItemArray)
                'dt.Rows.Add(row2)
                ViewState("dt") = dt
                Productos_Calculados()
                Calcular_Valores_DataTable()
                carga_productos_seleccionadas()
                cargar_subtotal_iva_total(Session("NuevoIva"))
                rowprincipal = Nothing
                limpiar_campos()
                Estado_controles_captura(False)
                buscar_confirmacion_lista()
                formar_cadenas()
                tbnguardarPP.Enabled = True
                btnnuevoprepedido.Enabled = True
            End If

        Catch ex As Exception
            CargarMsgbox("", ex.Message, False, 1)
        End Try
        'final = Now()
        'tspan = final.Subtract(inicio).Duration()
        'CargarMsgbox("", tspan.Duration.ToString(), False, 2)
    End Sub

    Protected Sub btncancelararticulo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancelararticulo.Click
        'Cancela el agregado de un articulo a la DataTable (Pedido)
        Session("rowprincipal") = Nothing
        limpiar_campos()
        txtcodigoarticulo.Enabled = True
        txtcodigoarticulo.Focus()
        Estado_controles_captura(False)
    End Sub

    'Protected Sub gridbuscar_clientes_ItemCommand(ByVal source As Object, ByVal e As  _
    '                                              System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridbuscar_clientes.ItemCommand
    '    If e.CommandName = "Seleccionar" Then
    '        Dim index As Integer
    '        index = e.Item.ItemIndex
    '        txtid.Text = gridbuscar_clientes.Items(index).Cells(1).Text
    '        'cargar_credito_disponible(txtid.Text)//// Motivo de Comentarizar: Tarda +6 segundos en cargar datos Cliente.
    '        txtnombre.Text = gridbuscar_clientes.Items(index).Cells(4).Text
    '        txtrfc.Text = gridbuscar_clientes.Items(index).Cells(2).Text
    '        Session("Clave_Adi") = gridbuscar_clientes.Items(index).Cells(3).Text
    '        txtstatus.Text = gridbuscar_clientes.Items(index).Cells(5).Text
    '        'Session("cad_prod") = gridbuscar_clientes.Items(index).Cells(6).Text
    '        'Session("credito") = gridbuscar_clientes.Items(index).Cells(7).Text
    '        colores(txtstatus.Text)
    '        'txtbuscar.Enabled = False
    '        'Estado_controles_captura(True)
    '        'txtcodigoarticulo.Enabled = True
    '        lblconfirmacion.Visible = Confirmacion_de_Pago()
    '        btnOC.Attributes.Add("onclick", "javascript:my_window=window.open('OC_Digitales_Pendientes.aspx?idc_cliente=" & txtid.Text & " ',null,'height=500, width=800,left=120,top=150,status=yes,resizable= no, scrollbars=yes, toolbar=no, menubar=no');my_window.focus();")
    '        btnOC.Enabled = True
    '        lkverdatoscliente.NavigateUrl = "javascript:my_window=window.open('Ficha_cliente.aspx?idc_cliente=" & txtid.Text.Trim & "&b=1','Datos del Cliente','height=500, width=800,left=120,top=150,status=yes, resizable=no, scrollbars=yes, toolbar=no, menubar=no'); my_window.focus();"
    '        lkverdatoscliente.Enabled = True
    '        'txtcodigoarticulo.Focus()
    '        etiqueta_Iva(Session("NuevoIva"))
    '        requiere_oc_croquis()
    '        '/cargar_proyectos_cliente(txtid.Text.Trim)
    '        btnnuevoprepedido.Enabled = True
    '        tbnguardarPP.Enabled = True
    '        txtbuscar.Text = ""
    '        tipo_croquis_cliente()


    '        'Para la Lista de Precios cliente
    '        lista_p(txtid.Text.Trim)
    '        Session("cedisprecios") = gridbuscar_clientes.Items(index).Cells(9).Text
    '        'AgregarJS()
    '        estado_rd(True)
    '    Else
    '        Estado_controles_captura(False)
    '        txtcodigoarticulo.Enabled = False
    '    End If
    'End Sub
    Sub requiere_oc_croquis()
        'Para revisar si es necesario cargar OC y Croquis....
        Dim gweb As New GWebCN.OC_Digitales
        Dim row As DataRow
        Try
            row = gweb.Requiere_OC_Croquis(txtid.Text.Trim)
            If row.ItemArray.Count > 0 Then
                oc.Text = row.Item(0)
                croquis.Text = row.Item(1)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub etiqueta_Iva(ByVal iva As String)
        If txtrfc.Text.StartsWith("*") Then
            lbliva.Text = "I.V.A."
        Else
            lbliva.Text = "I.V.A.(" + CInt(iva).ToString + "%)"
        End If
    End Sub
    Sub buscar_confirmacion_lista()
        Dim dt As New DataTable
        dt = ViewState("dt")
        Dim count As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1
            If Not IsDBNull(dt.Rows(i).Item(22)) Then
                count = count + 1
            End If
        Next
        If count > 0 Or Confirmacion_de_Pago() = True Then
            lblconfirmacion.Visible = True
            btnconfirmar.Visible = lblconfirmacion.Visible
        ElseIf count <= 0 And Confirmacion_de_Pago() = False Then
            lblconfirmacion.Visible = False
            btnconfirmar.Visible = lblconfirmacion.Visible
        End If
    End Sub

    Public Function Confirmacion_de_Pago() As Boolean
        Dim gweb As New GWebCN.Clientes
        Dim ds As New DataSet
        Dim row As DataRow
        Try
            ds = gweb.ver_confirmacion_pago(txtid.Text.Trim)
            If ds.Tables(0).Rows.Count Then
                row = ds.Tables(0).Rows(0)
                Return row("confirmacion")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            gweb = Nothing
            ds = Nothing
        End Try
    End Function
    Sub cargar_credito_disponible(ByVal idc_cliente As Integer)
        Dim gweb As New GWebCN.Productos
        Dim ds As New DataSet
        Dim row As DataRow
        Try
            ds = gweb.Credito_Disponible_Cliente(idc_cliente)
            If ds.Tables(0).Rows.Count > 0 Then
                row = ds.Tables(0).Rows(0)
                txtcreditodisponible.Text = Redondeo_Dos_Decimales(row("disponible"))
                If row("disponible") < 0 Then
                    txtcreditodisponible.BackColor = Drawing.Color.Red
                    txtcreditodisponible.ForeColor = Drawing.Color.White
                Else
                    txtcreditodisponible.BackColor = Drawing.Color.Green
                    txtcreditodisponible.ForeColor = Drawing.Color.White
                End If
            End If
        Catch ex As Exception
            CargarMsgbox("", ex.Message, False, 4)
        Finally
            gweb = Nothing
            ds = Nothing
            row = Nothing
        End Try

    End Sub

    Protected Sub grdproductos2_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdproductos2.DataBinding

    End Sub

    Protected Sub grdproductos2_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdproductos2.EditCommand
        Dim dt As New DataTable
        dt = ViewState("dt")
        Dim rowprincipal As DataRow
        Session("EditandoGrid") = e.Item.ItemIndex
        Session("PrecioAntes") = dt.Rows(e.Item.ItemIndex).Item(5)
        grdproductos2.EditItemIndex = e.Item.ItemIndex
        carga_productos_seleccionadas()
        'grdproductos2.Items(grdproductos2.EditItemIndex).Cells(1).Enabled = False
        'grdproductos2.Items(grdproductos2.EditItemIndex).Cells(2).Enabled = False
        rowprincipal = dt.Rows(e.Item.ItemIndex)
        Dim txtprecioreal As TextBox = grdproductos2.Items(e.Item.ItemIndex).FindControl("txtprecioreal")
        Dim preciogrid As TextBox = grdproductos2.Items(e.Item.ItemIndex).FindControl("txtpreciogrid")
        Dim txtcantidadgrid As TextBox = grdproductos2.Items(e.Item.ItemIndex).FindControl("txtcantidadgrid")
        'preciogrid.Attributes.Add("onblur", "LostFocus();")
        'txtcantidadgrid.Attributes.Add("onblur", "LostFocus();")
        'txtprecioreal.Attributes.Add("onblur", "LostFocus();")
        Session("Precio") = preciogrid.Text
        Session("PrecioReal") = txtprecioreal.Text
        Session("Cantidad") = txtcantidadgrid.Text
        Dim filtrocantidad As AjaxControlToolkit.FilteredTextBoxExtender = grdproductos2.Items(e.Item.ItemIndex).FindControl("filtrocantidad")
        If dt.Rows(e.Item.ItemIndex).Item(19) = True Then
            grdproductos2.Items(grdproductos2.EditItemIndex).Cells(4).Enabled = False
            grdproductos2.Items(grdproductos2.EditItemIndex).Cells(6).Enabled = False
        End If
        If dt.Rows(e.Item.ItemIndex).Item(9) = True Then
            filtrocantidad.ValidChars = "."
            txtcantidad.MaxLength = 11
        Else
            txtcantidadgrid.MaxLength = 7
            filtrocantidad.FilterType = AjaxControlToolkit.FilterTypes.Numbers
        End If
        If dt.Rows(e.Item.ItemIndex).Item(21) = True Then
            txtprecioreal.Enabled = False
            preciogrid.Enabled = False
        End If
    End Sub

    Protected Sub grdproductos2_ItemCommand(ByVal source As Object, ByVal e As  _
                                            System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdproductos2.ItemCommand
        Dim dt As New DataTable
        dt = ViewState("dt")


        If e.CommandName = "Editar" Then
            Dim lbl As New Label
            lbl = e.Item.FindControl("lblid")
            Session("dt_productos_lista") = ViewState("dt")
            txtidc_articulo.Text = lbl.Text.Trim
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "editar_articulo", "editar_precios_cantidad_1(" & txtidc_articulo.Text.Trim & ");", True)
        End If



        If e.CommandName = "eliminar" Then
            Dim index As Integer
            index = e.Item.ItemIndex
            Dim id As Integer = IIf(IsDBNull(dt.Rows(index).Item("idc_promocion")), 0, dt.Rows(index).Item("idc_promocion"))
            eliminar_promocion(id)
            dt.Rows(index).Delete()
            Session("dt_productos_lista") = dt
            carga_productos_seleccionadas()
            cargar_subtotal_iva_total(Session("NuevoIva"))
            formar_cadenas()
            If Not dt.Columns.Count > 0 Then
                tbnguardarPP.Enabled = False
            End If
        End If

        If e.CommandName = "Guardar" Then

            Dim index As Integer = e.Item.ItemIndex   ' obtiene el numero del Row que se esta editando.
            Dim row As DataRow
            Dim cantidad As TextBox
            Dim precio As TextBox
            Dim precioreal As TextBox
            cantidad = (e.Item.Cells(3).FindControl("txtcantidadgrid"))
            precio = e.Item.Cells(4).FindControl("txtpreciogrid")
            precioreal = e.Item.Cells(6).FindControl("txtprecioreal")
            row = dt.Rows(index)

            If precioreal.Text = Session("PrecioAntes") Then
                precioreal.Text = precio.Text
            End If

            'Si la cantidad=0 eliminar articulo de la lista 
            If cantidad.Text = 0 Then
                dt.Rows(e.Item.ItemIndex).Delete()
                grdproductos2.EditItemIndex = -1
                cargar_subtotal_iva_total(Session("NuevoIva"))
                buscar_confirmacion_lista() ' Checa si el cliente requiere conficmacion de pago.
                Productos_Calculados()
                carga_productos_seleccionadas()
                formar_cadenas()
                Exit Sub
            End If

            '----
            If row("vende_exis") = True And row("comercial") = True Then
                If No_Vender_Mas_De_Existencia(row("idc_articulo"), cantidad.Text) = False Then
                    Exit Sub
                End If
            End If
            If validar_multiplos(row("idc_articulo"), cantidad.Text) = False Then
                Exit Sub
            End If
            If validar_cantidad_decimales(cantidad.Text) = False Then
                CargarMsgbox("", "Ingresar cantidad con numeros enteros y solo tres numeros decimales", False, 2)
                Exit Sub
            End If
            If validar_numerico(cantidad.Text.Trim) = False Then
                CargarMsgbox("", "La cantidad no es correcta, ['0' o '0.000']", False, 2)
                Exit Sub
            ElseIf validar_numerico(precio.Text) = False Then
                CargarMsgbox("", "El precio no es correcto, ['1,000.0000']", False, 2)
                Exit Sub
            End If
            If validar_precio_decimales(precio.Text) = False Then
                CargarMsgbox("", "Precio incorrecto, ingresar numeros enteros y cuatro decimales", False, 2)
                Exit Sub
            ElseIf validar_precio_decimales(precioreal.Text) = False Then
                CargarMsgbox("", "Precio Real Incorrecto, ingresar numeros enteros y cuatro decimales", False, 2)
                Exit Sub
            End If

            If CDec(precio.Text) = 0 Then
                CargarMsgbox("", "El precio no puede ser IGUAL a Cero", False, 2)
                Exit Sub
            End If
            If CDec(precioreal.Text) = 0 Then
                precioreal.Text = precio.Text
            End If
            If CDec(precioreal.Text) > CDec(precio.Text) = True Then
                CargarMsgbox("", "El Precio Real no puede ser mayor al precio", False, 2)
                Exit Sub
            End If
            For i As Integer = 0 To dt.Rows.Count - 1
                If row("idc_articulo") = row("idc_articulo") Then
                    row("precio") = Redondeo_cuatro_decimales(precio.Text)
                    row("cantidad") = cantidad.Text
                    row("precioreal") = Redondeo_cuatro_decimales(precioreal.Text)
                    'rowprincipal("descuento") = Redondeo_cuatro_decimales(precio.Text - precioreal.Text)
                    'rowprincipal("importe") = Redondeo_cuatro_decimales(cantidad.Text * precio.Text)
                End If
            Next
            grdproductos2.EditItemIndex = -1
            Calcular_Valores_DataTable()
            Productos_Calculados()
            cargar_subtotal_iva_total(Session("NuevoIva"))
            carga_productos_seleccionadas()
            formar_cadenas()
        End If




        If e.CommandName = "Cancelar" Then
            dt.Rows(Session("EditandoGrid")).Item(4) = Session("cantidad")
            dt.Rows(Session("EditandoGrid")).Item(7) = Session("PrecioReal")
            dt.Rows(Session("EditandoGrid")).Item(5) = Session("Precio")
            Calcular_Valores_DataTable()
            cargar_subtotal_iva_total(Session("NuevoIva"))
            grdproductos2.EditItemIndex = -1
            carga_productos_seleccionadas()
            formar_cadenas()
        End If
    End Sub

    Protected Sub btnnuevoprepedido_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnuevoprepedido.Click
        'limpiar_campos_cliente()
        'Estado_controles_captura(False)
        'txtcodigoarticulo.Enabled = False
        '''''btnc
        'btnsalir.Enabled = True
        'btnnuevoprepedido.Enabled = True
        'txtidc_colonia.Text = ""
        'txtcalle.Text = ""
        'txtnumero.Text = ""
        'txtCP.Text = ""
        'txtestado.Text = ""
        'txtpais.Text = ""
        'txtmunicipio.Text = ""

        'txtcolonia.Text = ""
        Session("dt_productos_lista") = Nothing
        txtcreditodisponible.BackColor = Drawing.Color.White
        'imgstatus.Visible = False
        If CStr(Session("idc_cliente")) = "" Then
            CleanControls(Me.Controls)
            txtbuscar.Enabled = 1
            txtmaniobras.Text = "0.00"
            txtfolioCHP.Text = ""
            txtCP.Text = 0
            txtcminima.Text = 0
            'txtfecha.Text = CStr(FormatDateTime(Now, 2))
            txtbuscar.Focus()
            txtcodigoarticulo.Enabled = False
            txt_consignado.Text = 0
            txtnombre.BackColor = Drawing.Color.Transparent
            txtstatus.BackColor = Drawing.Color.Transparent
        Else
            CleanControls(Me.Controls)
            'txtbuscar.Enabled = 1
            txtcodigoarticulo.Enabled = False
            tbnguardarPP.Enabled = False
            txtmaniobras.Text = "0.00"
            txtfolioCHP.Text = ""
            txtCP.Text = 0
            txtcminima.Text = 0
            'txtfecha.Text = CStr(FormatDateTime(Now, 2))
            Dim gweb As New GWebCN.Clientes
            Dim ds As New DataSet
            Dim row As DataRow
            ds = gweb.ver_datos_cliente(Session("idc_cliente"))
            If ds.Tables(0).Rows.Count Then
                row = ds.Tables(0).Rows(0)
                txtid.Text = Session("idc_cliente")
                txtrfc.Text = row("rfccliente")
                txtnombre.Text = row("nombre")
                'cargar_credito_disponible(txtid.Text)
                Session("Clave_Adi") = row("cveadi")
                txtstatus.Text = row("idc_bloqueoc")
                colores(txtstatus.Text)
                txtbuscar.Enabled = False
                'txtcodigoarticulo.Enabled = True
                lblconfirmacion.Visible = Confirmacion_de_Pago()
                btnconfirmar.Visible = lblconfirmacion.Visible
                btnOC.Attributes.Add("onclick", "javascript:my_window=window.open('OC_Digitales_Pendientes.aspx?idc_cliente=" & txtid.Text & " ',null,'height=1030, width=1020,status=yes,resizable= no, scrollbars=yes, toolbar=no, menubar=no');my_window.focus();")
                btnOC.Enabled = True
                lkverdatoscliente.NavigateUrl = "javascript:my_window=window.open('Ficha_cliente_m.aspx?idc_cliente=" & txtid.Text.Trim & "','Datos del Cliente','height=1100, width=1020, status=yes, resizable=no, scrollbars=yes, toolbar=no, menubar=no'); my_window.focus();"
                lkverdatoscliente.Enabled = True
                txtcodigoarticulo.Focus()
                etiqueta_Iva(Session("NuevoIva"))
                requiere_oc_croquis()
                '/cargar_proyectos_cliente(txtid.Text.Trim)
                txt_consignado.Text = 0
                'With txtcodigoarticulo
                '    .Enabled = True
                '    .Focus()
                'End With
                lista_p(Session("idc_cliente"))
            End If
        End If
        agregar_columnas_dataset()
        agregar_columnas_tabla_promociones()
        'limpiar_campos_cliente()
        txtidc_colonia.Text = "0"
        tbnguardarPP.Enabled = False
        'btnnuevoprepedido.Enabled = False
        cboentrega.SelectedValue = 1
        btnconsignado.Enabled = True
        btncaptArt.Enabled = True
        estado_rd(True)
        txtproy.Text = 0
        txtsucursalr.Text = "0"
        controles_busqueda_cliente(True)
        controles_busqueda_cliente_cancel_selecc(False)
        controles_busqueda_prod(False)
        controles_busqueda_master(False)
        controles_busqueda_prod_sel_cancel(False)
        btnmaster.Visible = False
        btnbuscar_codigo.Visible = False
        txtcodigoarticulo.Visible = True
        txtcodigoarticulo.Attributes("onfocus") = "this.blur();"
        ViewState("dt_clientes") = Nothing
        ViewState("dt_productos") = Nothing
        ViewState("tx_pedido_gratis") = Nothing ' no veo donde se utilice
        imgpromocion.Attributes("style") = "display:none;"
        imgpromocion.Attributes.Remove("onclick") 'fin 01-06-2015
        Session("pidc_iva") = Session("lidc_iva")
        Session("NuevoIva") = Session("xiva")
        labeliva.Visible = False
    End Sub

    Public Sub CleanControls(ByVal controles As ControlCollection)
        For Each control As Control In controles
            If TypeOf control Is TextBox Then
                DirectCast(control, TextBox).Text = String.Empty
            ElseIf TypeOf control Is DropDownList Then
                DirectCast(control, DropDownList).ClearSelection()
            ElseIf TypeOf control Is RadioButtonList Then
                DirectCast(control, RadioButtonList).ClearSelection()
            ElseIf TypeOf control Is CheckBoxList Then
                DirectCast(control, CheckBoxList).ClearSelection()
            ElseIf TypeOf control Is RadioButton Then
                DirectCast(control, RadioButton).Checked = False
            ElseIf TypeOf control Is CheckBox Then
                DirectCast(control, CheckBox).Checked = False
            ElseIf TypeOf control Is DataGrid Then
                DirectCast(control, DataGrid).DataSource = Nothing
                DirectCast(control, DataGrid).DataBind()
            ElseIf TypeOf control Is GridView Then
                DirectCast(control, GridView).DataSource = Nothing
                DirectCast(control, GridView).DataBind()
            ElseIf control.HasControls() Then
                CleanControls(control.Controls)
            End If
        Next
    End Sub


#Region "Funciones de Conversión y Redondeo "

    ''' <summary>
    ''' Convierte a formato moneda segun las configuraciones regionales del equipo.
    ''' </summary>
    ''' <param name="valor"> Valor a convertir </param>
    ''' <returns>Regresa el valor a formato moneda(separando por ',' los miles y por '.' los decimales )</returns>
    ''' <remarks></remarks>
    ''' 
    Public Function Formato_moneda(ByVal valor As Double) As String
        Try
            'Return FormatNumber(valor, 2, -2, -2, -1)
            valor = Math.Round(valor, 2)
            Return FormatCurrency(valor, 2)
        Catch ex As Exception
            Throw ex
        Finally
            valor = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Redondea un valor con fracciones decimales (2 digitos decimales)
    ''' </summary>
    ''' <param name="valor">Valor a Redondear</param>
    ''' <returns>Valor redondeado a dos digitos</returns>
    ''' <remarks></remarks>
    Public Function Redondeo_Dos_Decimales(ByVal valor As Decimal) As String

        Try
            valor = Math.Round(valor, 2)
            Return FormatNumber(valor, 2)
        Catch ex As Exception
            Throw ex
        Finally
            valor = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Regresa un numero entero con cuatro digitos decimales
    ''' </summary>
    ''' <param name="valor">Valor a convertir con cuatro digitos decimales</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Redondeo_cuatro_decimales(ByVal valor As Double) As String
        Try
            valor = Math.Round(valor, 4)
            Return FormatNumber(valor, 4)
        Catch ex As Exception
            Throw ex
        Finally
            valor = Nothing
        End Try
    End Function

#End Region

#Region "Validaciones de Pedido"

    'Función para validar que no se venda mas de la existencia...
    Public Function No_Vender_Mas_De_Existencia(ByVal idc_articulo As Integer, ByVal cantidad As Double) As Boolean
        Dim rowprincipal As DataRow = Session("rowprincipal")

        Dim gweb As New GWebCN.Productos
        Dim row As DataRow
        Dim ds As New DataSet
        Try
            ds = gweb.buscar_existencia_articulo(Session("xidc_almacen"), idc_articulo, 0)
            row = ds.Tables(0).Rows(0)
            rowprincipal("Existencia") = row("EXISTENCIA_DISPONIBLE")
            If (cantidad <= rowprincipal("Existencia")) = False Then
                CargarMsgbox("", "No puedes vender mas de la existencia, existencia: " & row("EXISTENCIA_DISPONIBLE"), False, 2)
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Session("rowprincipal") = rowprincipal
            ds = Nothing
            row = Nothing
            gweb = Nothing
        End Try
    End Function

    'Valida multiplos de venta del articulo
    Public Function validar_multiplos(ByVal idc_articulo As Integer, ByVal cantidad As Integer) As Boolean
        Dim gweb As New GWebCN.Productos
        Dim ds As New DataSet
        Dim row As DataRow
        Try
            ds = gweb.validar_multiplos(idc_articulo, cantidad)
            If ds.Tables(0).Rows.Count > 0 Then
                row = ds.Tables(0).Rows(0)
                If row("pconv") = False Then
                    CargarMsgbox("", "Cantidad invalida...Solo multiplos de: " & row("rconversion"), False, 4)
                    Return False
                Else
                    Return True
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            gweb = Nothing
            ds = Nothing
            row = Nothing
        End Try
    End Function

    'Validar que los TextBox tengan texto
    Public Function Validar_Campos() As Boolean

        If txtcodigoarticulo.Text = "" Then
            CargarMsgbox("", "Indicar el codigo del articulo", False, 2)
            Return False
            Exit Function
        ElseIf txtdescripcion.Text = "" Then
            CargarMsgbox("", "Indicar la descricpción del articulo", False, 2)
            Return False
            Exit Function
        ElseIf txtUM.Text = "" Then
            CargarMsgbox("", "Indicar Unidad de Medida del articulo", False, 2)
            Return False
            Exit Function
        ElseIf txtcantidad.Text = "" Then
            CargarMsgbox("", "Es necesario indicar la cantidad", False, 2)
            Return False
            Exit Function
        ElseIf validar_cantidad_decimales(txtcantidad.Text) = False Then
            Return False
            Exit Function
        ElseIf txtcantidad.Text = 0 Then
            CargarMsgbox("", "La cantidad no puede ser Cero...", False, 1)
            Return False
            Exit Function
        ElseIf txtprecio.Text = "" Then
            CargarMsgbox("", "Es necesario capturar el precio del articulo", True, 2)
            Return False
            Exit Function
        ElseIf txtprecio.Text = 0 Then
            CargarMsgbox("", "El precio debera ser mayor a Cero...", False, 2)
            Return False
            Exit Function
        Else
            Return True
        End If
    End Function

    'Validar que un campo sea numerico
    Public Function validar_numerico(ByVal valor_numero As String) As Boolean
        Try
            If IsNumeric(valor_numero) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            valor_numero = Nothing
        End Try

    End Function
    Public Function validar_cantidad_decimales(ByVal cantidad As String) As Boolean
        Dim valor() As String
        Try
            valor = Split(cantidad, ".")
            If valor.Length > 2 Then
                CargarMsgbox("", "Ingresar cantidad con numeros enteros y solo tres numeros decimales", False, 2)
                Return False
            ElseIf valor.Length = 2 Then
                If valor(1).Length > 3 Then
                    CargarMsgbox("", "Ingresar cantidad con numeros enteros y solo tres numeros decimales", False, 2)
                    Return False
                Else
                    Return True
                End If
            ElseIf valor.Length = 1 Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            valor = Nothing
            cantidad = Nothing
        End Try
    End Function

    Public Function validar_precio_decimales(ByVal precio As String) As Boolean
        Dim preciod() As String

        Try
            preciod = Split(precio, ".")
            If preciod.Length > 2 Then
                Return False
            ElseIf preciod.Length = 1 Then
                Return True
            ElseIf preciod.Length = 2 Then
                If preciod(1).Length > 4 Then
                    Return False
                Else
                    Return True
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            precio = Nothing
            preciod = Nothing

        End Try
    End Function

#End Region

#Region "Funciones MsgBOX"
    ''' <summary>
    ''' Funcion que muestra el MsgBox, recibe como parametros el [titulo] que se ubica en la parte supreior izq.,
    ''' [Mensaje] sera el mensaje a desplegar, [Condicion] ||True=(Boton SI-No)|||False= (Boton Aceptar)||,[Tipo] Sera el numero de 
    ''' icono a mostrar...
    ''' </summary>
    ''' <param name="Titulo">Titulo, Tipo String</param>
    ''' <param name="Mesaje">Mensaje, Tipo String</param>
    ''' <param name="Condicion">Condicion [False-True] </param>
    ''' <param name="tipo">Tipo, Integer, icono a mostrar,1)Alerta, 2)Información, 3)Interrogación, 4)Error , 5) Correcto</param>
    ''' <remarks> </remarks>

    Private Sub CargarMsgbox(ByVal Titulo As String, ByVal Mesaje As String, ByVal Condicion As Boolean, ByVal tipo As Integer)
        If Mesaje IsNot "" Then
            presentacion.Alert.ShowAlertError(Mesaje, Me.Page)
        End If
    End Sub


    Sub Ajustes_iva(ByVal nuevo_iva As Decimal, ByVal iva_ant As Decimal)
        Dim dt As New DataTable
        dt = ViewState("dt")
        If txtrfc.Text.StartsWith("*") Then
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i).Item(5) = dt.Rows(i).Item(5) / (1 + (iva_ant / 100)) * (1 + (nuevo_iva / 100))
                dt.Rows(i).Item(6) = Redondeo_Dos_Decimales(dt.Rows(i).Item(5) * dt.Rows(i).Item(4))
                dt.Rows(i).Item(7) = Redondeo_cuatro_decimales(dt.Rows(i).Item(5) - dt.Rows(i).Item(8))
            Next



            'For i As Integer = 0 To dt.Rows.Count - 1
            '    dt.Rows(i).Item(5) = Redondeo_cuatro_decimales(dt.Rows(i).Item(5) / ((1 + (Session("Xiva") / 100)))) ' Le quita el iva anterior al precio
            '    dt.Rows(i).Item(6) = Redondeo_Dos_Decimales(dt.Rows(i).Item(5) * dt.Rows(i).Item(4))                            ' Calcula el nuevo importe.
            '    dt.Rows(i).Item(7) = Redondeo_cuatro_decimales(dt.Rows(i).Item(5) - dt.Rows(i).Item(8))                          ' Ajusta el precio real.
            'Next
            'For i As Integer = 0 To dt.Rows.Count - 1
            '    dt.Rows(i).Item(5) = Redondeo_cuatro_decimales((dt.Rows(i).Item(5) + ((dt.Rows(i).Item(5) * (Session("Nuevoiva") / 100)))))
            '    dt.Rows(i).Item(6) = Redondeo_Dos_Decimales(dt.Rows(i).Item(5) * dt.Rows(i).Item(4))
            '    dt.Rows(i).Item(7) = Redondeo_cuatro_decimales((dt.Rows(i).Item(5) - dt.Rows(i).Item(8)))
            'Next
            cargar_subtotal_iva_total(nuevo_iva)
        Else
            cargar_subtotal_iva_total(nuevo_iva)
        End If
        'Dim cambio(2) As Object
        'cambio(0) = True
        'cambio(1) = nuevo_iva
        ViewState("dt") = dt
        carga_productos_seleccionadas()
    End Sub
    Sub Agregar_maniobras()
        Dim gweb As New GWebCN.Productos
        Dim ds As New DataSet
        Dim dt As New DataTable
        dt = ViewState("dt")
        Dim row As DataRow
        Dim rowdt As DataRow = dt.NewRow
        Try
            Dim existe As Boolean
            Dim item As Integer
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item(0) = 4454 Then
                    existe = True
                    item = i
                    Exit For
                End If
            Next
            If existe = True Then
                dt.Rows(item).Item(5) = Redondeo_cuatro_decimales(dt.Rows(item).Item(5) + CDbl(txtmaniobras.Text.Trim))
                dt.Rows(item).Item(7) = Redondeo_cuatro_decimales(dt.Rows(item).Item(7) + CDbl(txtmaniobras.Text.Trim))


                ViewState("dt") = dt
                Productos_Calculados()
                Calcular_Valores_DataTable()
                cargar_subtotal_iva_total(Session("NuevoIva"))
                carga_productos_seleccionadas()
            Else
                Dim codigo As String
                Dim gweb2 As New GWebCD.clsConexion
                Dim dt2 As New DataTable
                dt2 = gweb2.EjecutaConsulta("Select codigo from articulos where idc_articulo=4454")
                If dt2.Rows.Count > 0 Then
                    codigo = dt2.Rows(0).Item(0)
                Else
                    Return
                End If
                ds = gweb.buscar_productos(codigo, "A", Session("idc_sucursal"), Session("idc_usuario"))
                If ds.Tables(0).Rows.Count > 0 Then
                    'cargar_datos_productob_x_codigo(ds.Tables(0).Rows(0))
                    row = ds.Tables(0).Rows(0)
                    rowdt("idc_articulo") = row("idc_articulo")
                    rowdt("Codigo") = row("codigo")
                    rowdt("Descripcion") = row("desart")
                    rowdt("UM") = row("unimed")
                    rowdt("Decimales") = row("decimales")
                    rowdt("Paquete") = row("paquete")
                    rowdt("precio_libre") = row("precio_libre")
                    rowdt("comercial") = row("comercial")
                    rowdt("fecha") = row("fecha")
                    rowdt("obscotiza") = row("obscotiza")
                    rowdt("vende_exis") = row("vende_exis")
                    rowdt("minimo_venta") = row("minimo_venta")
                    rowdt("costo") = costo_maniobras()
                    rowdt("calculado") = False
                    rowdt("porcentaje") = 0
                    rowdt("Anticipo") = row("Anticipo")
                    rowdt("Precio") = Redondeo_cuatro_decimales(txtmaniobras.Text.Trim)
                    rowdt("Importe") = Redondeo_Dos_Decimales(txtmaniobras.Text.Trim)
                    rowdt("PrecioReal") = Redondeo_cuatro_decimales(txtmaniobras.Text.Trim)
                    rowdt("nota_credito") = False
                    rowdt("descuento") = Redondeo_cuatro_decimales("0.00")
                    rowdt("Cantidad") = 1
                    rowdt("Existencia") = existencia_maniobras()
                    dt.Rows.InsertAt(rowdt, dt.Rows.Count)
                    ViewState("dt") = dt
                    carga_productos_seleccionadas()
                    cargar_subtotal_iva_total(Session("NuevoIva"))
                    Calcular_Valores_DataTable()
                    Productos_Calculados()
                End If
            End If
        Catch ex As Exception
            CargarMsgbox("", ex.Message, False, 1)
        End Try
    End Sub
    Function existencia_maniobras() As Double
        Dim gweb As New GWebCN.Productos
        Dim ds As New DataSet
        Dim row As DataRow
        Dim existencia As Double
        Try
            ds = gweb.buscar_existencia_articulo(Session("xidc_almacen"), 4454, 0)

            If ds.Tables(0).Rows.Count > 0 Then
                row = ds.Tables(0).Rows(0)
                existencia = row("EXISTENCIA_DISPONIBLE")
                Return existencia
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function costo_maniobras() As Double
        Dim gweb As New GWebCN.Productos
        Dim costo As Double
        Dim ds As New DataSet
        Dim row As DataRow
        Try
            ds = gweb.buscar_precio_producto(4454, txtid.Text.Trim, Session("idc_sucursal"))
            If ds.Tables(0).Rows.Count > 0 Then
                row = ds.Tables(0).Rows(0)
                costo = row("COSTO")
                Return costo
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Procedimientos Controles"

    'Pone vacios los TextBox de captura
    Sub limpiar_campos()
        txtcodigoarticulo.Text = Nothing
        txtdescripcion.Text = Nothing
        txtprecio.Text = Nothing
        txtUM.Text = Nothing
        txtcantidad.Text = Nothing
        txtcodigoarticulo.Enabled = True
    End Sub

    '**//Carga Grid que contiene los articulos seleccionados\\**
    Sub carga_productos_seleccionadas()

        Dim dt As New DataTable
        dt = ViewState("dt")
        'Dim dataset As New DataSet
        'dataset.Tables.Add(dt)
        If dt.Rows.Count >= 1 Then
            grdproductos2.DataSource = dt
            grdproductos2.DataBind()
            'txttotalarticulos.Text = dt.Rows.Count

        Else
            grdproductos2.DataSource = Nothing
            grdproductos2.DataBind()
            tbnguardarPP.Enabled = False
            'txttotalarticulos.Text = ""
        End If

    End Sub


    'Mostrar Observaciones de cotización, mensajes, referentes al articulo
    Sub ver_observaciones_articulo(ByRef row As DataRow)
        'Try
        '    If Not (row("mensaje")) = "" Then
        '        CargarMsgbox("", "el articulo tiene un mensaje: <BR/>" & row("mensaje"), False, 2)
        '    End If
        '    If Not IsDBNull((row("fecha"))) Then
        '        Dim fecha As Date
        '        fecha = CDate(row("fecha"))
        '        fecha = Format(fecha, "Long Date")
        '        CargarMsgbox("", "El articulo estara disponible hasta: " & fecha, False, 2)
        '    End If
        '    If Not row("obscotiza") = "" Then
        '        CargarMsgbox("", row("obscotiza"), False, 2)
        '    End If
        '    txtcantidad.Focus()
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub


    'Calcular el SubTotal, IVA y Total...
    Sub cargar_subtotal_iva_total(ByVal t_iva As Double)
        t_iva = t_iva / 100  'Se divide para sacar el %.
        Dim dt As New DataTable
        dt = ViewState("dt")
        If Not txtrfc.Text.StartsWith("*") Then
            Dim subtotal(2) As Decimal
            Dim iva(2) As Decimal
            Dim total(2) As Decimal
            Dim row As DataRow
            For i As Integer = 0 To dt.Rows.Count - 1
                row = dt.Rows(i)
                subtotal(0) = subtotal(0) + CDec(row("Importe"))
                subtotal(1) = subtotal(1) + (CDec(row("Descuento")) * row("cantidad"))
            Next
            subtotal(1) = Math.Round(subtotal(1), 2, MidpointRounding.AwayFromZero) 'Redondeo a dos decimales
            subtotal(0) = Math.Round(subtotal(0), 2, MidpointRounding.AwayFromZero) 'Redondeo a dos decimales
            subtotal(2) = subtotal(0) - subtotal(1)

            iva(0) = subtotal(0) * t_iva
            iva(0) = Math.Round(iva(0), 2)           'Redondeo a dos decimales
            iva(1) = subtotal(1) * t_iva
            iva(1) = Math.Round(iva(1), 2)           'Redondeo a dos decimales
            iva(2) = iva(0) - iva(1)

            total(0) = subtotal(0) + iva(0)
            total(0) = Math.Round(total(0), 2, MidpointRounding.AwayFromZero)       'Redondeo a dos decimales

            total(1) = subtotal(1) + iva(1)
            total(1) = Math.Round(total(1), 2, MidpointRounding.AwayFromZero)       'Redondeo a dos decimales
            total(2) = subtotal(2) + iva(2)

            txtsubt.Text = Formato_moneda(subtotal(0))
            txtsubtotaldescuento.Text = Formato_moneda(subtotal(1))
            txtsubtotal.Text = Formato_moneda(subtotal(2))
            txtsubiva.Text = Formato_moneda(iva(0))
            txtivadescuento.Text = Formato_moneda(iva(1))
            txtiva.Text = Formato_moneda(iva(2))
            txtpretotal.Text = Formato_moneda(total(0))
            txttotaldescuento.Text = Formato_moneda(total(1))
            txttotal.Text = Formato_moneda(total(2))
        Else

            Dim total As Decimal
            Dim descuento As Decimal
            Dim row As DataRow
            For i As Integer = 0 To dt.Rows.Count - 1
                row = dt.Rows(i)
                total = total + CDec(row("Importe"))
                descuento = descuento + (CDec(row("Descuento")) * row("cantidad"))
            Next
            total = Math.Round(total, 2, MidpointRounding.AwayFromZero)
            descuento = Decimal.Round(descuento, 2, MidpointRounding.AwayFromZero)
            txtsubt.Text = Formato_moneda(total)
            txtsubtotaldescuento.Text = Formato_moneda(descuento)                       '"$   " & descuento
            txtsubtotal.Text = Formato_moneda(total - descuento)              '"$   " & total - descuento
            txtsubiva.Text = Formato_moneda(0.0)  '"$   0.00"
            txtivadescuento.Text = Formato_moneda(0.0)          ' "$   0.00"
            txtiva.Text = Formato_moneda(0.0)                   '"$   0.00"
            txtpretotal.Text = Formato_moneda(total)                         '"$   " & total
            txttotaldescuento.Text = Formato_moneda(descuento)                  '"$   " & descuento
            txttotal.Text = Formato_moneda(total - descuento)                  '"$   " & (total - descuento)
        End If

    End Sub

    Sub agregar_columnas_tabla_promociones()
        Dim dt As New DataTable
        dt.Columns.Add("idc_articulo", GetType(Integer))
        dt.Columns.Add("cantidad", GetType(Decimal))
        dt.Columns.Add("codigo")
        dt.Columns.Add("unimed")
        dt.Columns.Add("desart")
        dt.Columns.Add("idc_promociond", GetType(Integer))
        dt.Columns.Add("idc_promocion", GetType(Integer))
        Session("tx_pedido_gratis") = dt
    End Sub

    'Agregar las columnas a la Tabla Temporal (DataTable)
    Sub agregar_columnas_dataset()
        If contador = 0 Then
            Dim dt As New DataTable
            dt.Columns.Add("idc_articulo", GetType(Integer))     '0
            dt.Columns.Add("Codigo")           '1
            dt.Columns.Add("Descripcion")      '2
            dt.Columns.Add("UM")               '3
            dt.Columns.Add("Cantidad")         '4
            dt.Columns.Add("Precio", GetType(Decimal))           '5
            dt.Columns.Add("Importe")                            '6
            dt.Columns.Add("PrecioReal", GetType(Decimal))       '7
            dt.Columns.Add("Descuento", GetType(Decimal))        '8
            dt.Columns.Add("Decimales")        '9
            dt.Columns.Add("Paquete")          '10
            dt.Columns.Add("precio_libre")     '11
            dt.Columns.Add("comercial")        '12
            dt.Columns.Add("fecha")            '13
            dt.Columns.Add("obscotiza")        '14
            dt.Columns.Add("vende_exis")       '15 
            dt.Columns.Add("minimo_venta")     '16 
            dt.Columns.Add("Master")           '17 
            dt.Columns.Add("mensaje")          '18 
            dt.Columns.Add("Calculado")        '19 
            dt.Columns.Add("Porcentaje")       '20 
            dt.Columns.Add("Nota_Credito")     '21 
            dt.Columns.Add("Anticipo")         '22
            dt.Columns.Add("Costo")            '23
            dt.Columns.Add("Existencia")       '24
            dt.Columns.Add("idc_promocion", GetType(Integer))   '25
            dt.Columns.Add("precio_lista", GetType(Decimal))    '26
            dt.Columns.Add("precio_minimo", GetType(Double))    '27 
            dt.Columns.Add("tiene_especif", GetType(Boolean))   '28 
            dt.Columns.Add("especif", GetType(String))          '29  
            dt.Columns.Add("num_especif", GetType(Integer))     '30 
            dt.Columns.Add("ids_especif", GetType(String))      '31
            dt.Columns.Add("g_especif", GetType(String))        '32 
            dt.Columns.Add("costo_o", GetType(Decimal))
            dt.Columns.Add("precio_o", GetType(Decimal))
            dt.Columns.Add("precio_lista_o", GetType(Decimal))
            dt.Columns.Add("precio_minimo_o", GetType(Decimal))
            contador = 1
            ViewState("dt") = dt
        End If
    End Sub

    Sub Productos_Calculados()
        Dim dt As New DataTable
        dt = ViewState("dt")
        Dim valor_calculado As Double
        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item(19) = True Then
                For x As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(x).Item(19) = False Then
                        valor_calculado = valor_calculado + dt.Rows(x).Item(6)
                    End If
                Next
                valor_calculado = valor_calculado * (dt.Rows(i).Item(20) / 100)
                dt.Rows(i).Item(5) = Redondeo_cuatro_decimales(valor_calculado)
                dt.Rows(i).Item(6) = Redondeo_Dos_Decimales(dt.Rows(i).Item(4) * (dt.Rows(i).Item(5)))
                dt.Rows(i).Item(7) = Redondeo_cuatro_decimales(dt.Rows(i).Item(5))
            End If
        Next
    End Sub

    'Definir los colores del TextBox Status y Nombre del Cliente
    Sub colores(ByVal valor As String)
        Select Case valor
            Case 0 'Verde
                txtstatus.BackColor = Drawing.Color.Green
                'txtstatus.Attributes("style") = "background-color:#009245;border-color:#009245;"
                txtstatus.ForeColor = Drawing.Color.White
                'txtnombre.Attributes("style") = "background-color:#009245;border-color:#009245;"
                txtnombre.BackColor = Drawing.Color.Green
                txtnombre.ForeColor = Drawing.Color.White
                'imgstatus.ImageUrl = "~/Iconos/sverde.png"
                'imgstatus.Visible = True
                txtcodigoarticulo.Attributes.Remove("onfocus")
                txtbuscar.Enabled = False
            Case 4 'Amarillo
                txtstatus.BackColor = Drawing.Color.Yellow
                'txtstatus.Attributes("style") = "background-color:#fcee21;border-color:#fcee21;"
                txtstatus.ForeColor = Drawing.Color.Black
                'txtnombre.Attributes("style") = "background-color:#fcee21;border-color:#fcee21;"
                txtnombre.BackColor = Drawing.Color.Yellow
                txtnombre.ForeColor = Drawing.Color.Black
                'imgstatus.ImageUrl = "~/Iconos/samarillo.png"
                'imgstatus.Visible = True
                txtcodigoarticulo.Attributes("onfocus") = "this.blur();"
                txtbuscar.Enabled = True
                'ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "<script>alert('" + ex.Message.Replace("'", "") + "');</script>", false);
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>alert('El Cliente Esta Bloqueado por Cheques Devueltos'); </script>", False)
                'El Cliente Esta Bloqueado por Cheques Devueltos...
                txtbuscar.Focus()
                txtbuscar.Enabled = True
            Case 1, 2, 3 'Rojo
                txtstatus.BackColor = Drawing.Color.Red
                'txtstatus.Attributes("style") = "background-color:#c1272d;border-color:#c1272d;"
                txtstatus.ForeColor = Drawing.Color.White
                'txtnombre.Attributes("style") = "background-color:#c1272d;border-color:#c1272d;"
                txtnombre.BackColor = Drawing.Color.Red
                txtnombre.ForeColor = Drawing.Color.White
                'imgstatus.ImageUrl = "~/Iconos/srojo.png"
                'imgstatus.Visible = True
                txtcodigoarticulo.Attributes.Remove("onfocus")
                txtbuscar.Enabled = False
        End Select
    End Sub

    'Vacio por el momento
    Sub cargar_datos_cliente()
        '    'Buscar cliente y cargar infomrmacion...
        '    txtnombre.Text = Session("nombre")
        '    txtstatus.Text = Session("Bloqueo")
        '    txtrfc.Text = Session("rfc")
        '    txtid.Text = Session("idc_cliente")
        '    colores(Session("Bloqueo"))
        '    Session("nombre") = Nothing
        '    Session("Bloqueo") = Nothing
        '    Session("rfc") = Nothing
        '    Session("idc_cliente") = Nothing
        '    Session("Bloqueo") = Nothing
    End Sub

    'Modifica el modo de captura del TextBox cantidad [Decimales-Enteros]
    'Public Sub Decimales(ByVal Decimales As Boolean)
    '    If Decimales = True Then
    '        Filtrocantidad0.FilterType = AjaxControlToolkit.FilterTypes.Custom
    '        Filtrocantidad0.ValidChars = "1234567890."
    '    Else
    '        Filtrocantidad0.FilterType = AjaxControlToolkit.FilterTypes.Numbers
    '    End If
    'End Sub


    'Agrega los datos de un articulo seleccionado de una busqueda a un Row y 
    'posteriormente este Row al DataTable

    Sub cargar_datos_productob_x_codigo(ByRef row As DataRow)
        'Recopilar la informacion de un producto seleccionado
        Dim dt As New DataTable
        dt = ViewState("dt")
        Dim rowprincipal As DataRow
        rowprincipal = dt.NewRow()
        Session("rowprincipal") = rowprincipal
        Try
            If calculado(row("idc_articulo")) = Nothing Then
                buscar_precio_producto(row("idc_articulo"))
                txtcodigoarticulo.Text = row("codigo")
                txtdescripcion.Text = row("desart")
                txtUM.Text = row("unimed")
                'Decimales(CBool(row("decimales")))
                rowprincipal("idc_articulo") = row("idc_articulo")
                rowprincipal("Codigo") = row("codigo")
                rowprincipal("Descripcion") = row("desart")
                rowprincipal("UM") = row("unimed")
                rowprincipal("Decimales") = row("decimales")
                rowprincipal("Paquete") = row("paquete")
                rowprincipal("precio_libre") = row("precio_libre")
                rowprincipal("comercial") = row("comercial")
                rowprincipal("fecha") = row("fecha")
                rowprincipal("obscotiza") = row("obscotiza")
                rowprincipal("vende_exis") = row("vende_exis")
                rowprincipal("minimo_venta") = row("minimo_venta")
                rowprincipal("calculado") = False
                rowprincipal("porcentaje") = 0
                rowprincipal("Anticipo") = row("Anticipo")
                rowprincipal("Existencia") = buscar_Existencia_Articulo(rowprincipal("idc_articulo"))
                Estado_controles_captura(True)
                If rowprincipal("Decimales") = True Then
                    txtcantidad.MaxLength = 11
                Else
                    txtcantidad.MaxLength = 7
                End If
                txtcantidad.Focus()
                If rowprincipal("nota_credito") = True Then
                    txtprecio.Enabled = False
                End If
            Else    'Es un articulo calculado, se agrega a la lista automaticamente...
                rowprincipal("idc_articulo") = row("idc_articulo")
                rowprincipal("Codigo") = row("codigo")
                rowprincipal("Descripcion") = row("desart")
                rowprincipal("UM") = row("unimed")
                rowprincipal("Decimales") = row("decimales")
                rowprincipal("Paquete") = row("paquete")
                rowprincipal("precio_libre") = row("precio_libre")
                rowprincipal("comercial") = row("comercial")
                rowprincipal("fecha") = row("fecha")
                rowprincipal("obscotiza") = row("obscotiza")
                rowprincipal("vende_exis") = row("vende_exis")
                rowprincipal("minimo_venta") = row("minimo_venta")
                rowprincipal("calculado") = True
                rowprincipal("Porcentaje") = calculado(row("idc_articulo"))
                rowprincipal("cantidad") = 1
                rowprincipal("precioreal") = Redondeo_cuatro_decimales(0)
                rowprincipal("precio") = Redondeo_cuatro_decimales(0)
                dt.Rows.Add(rowprincipal)
                limpiar_campos()
                Productos_Calculados()
                Calcular_Valores_DataTable()
                Productos_Calculados()
                cargar_subtotal_iva_total(Session("NuevoIva"))
                carga_productos_seleccionadas()
                rowprincipal = Nothing
                With txtcodigoarticulo
                    .Enabled = True
                    .Focus()
                End With
                Estado_controles_captura(False)
            End If
            Session("rowprincipal") = rowprincipal
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function buscar_Existencia_Articulo(ByVal idc_articulo As Integer) As Double
        Dim gweb As New GWebCN.Productos
        Dim row As DataRow
        Dim ds As New DataSet
        Try
            ds = gweb.buscar_existencia_articulo(Session("xidc_almacen"), idc_articulo, 0)
            If ds.Tables(0).Rows.Count > 0 Then
                row = ds.Tables(0).Rows(0)
                Return row("EXISTENCIA_DISPONIBLE")
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        Finally

            ds = Nothing
            row = Nothing
            gweb = Nothing
        End Try
    End Function


    'Buscar el precio de un articulo y mostrarlo en el control correspondiente.
    Sub buscar_precio_producto(ByVal codigo As Integer)
        'If txtidc_colonia.Text = "0" Or txtidc_colonia.Text.Trim = "" Then
        '    Session("pxidc_sucursal") = Session("idc_sucursal")
        'End If
        'Dim row As DataRow
        Dim rowprincipal As DataRow
        rowprincipal = Session("rowprincipal")
        rowprincipal("nota_credito") = False

        Dim vidc As Integer = codigo
        Dim vidcli As Integer = txtid.Text.Trim
        Dim vidc_clonia As Integer = txtidc_colonia.Text.Trim
        Dim dt, dt1, dt2, dt3 As New DataTable
        Dim vprecio As Decimal = 0

        Dim vidc_listap As Integer = txtlistap.Text.Trim
        Dim zidc_sucursal As Integer = Session("pxidc_sucursal")
        

        'Cambios



        '///

        Try
            dt = consulta("exec sp_precio_cliente_cedis @pidc_articulo = " & vidc & ",@pidc_cliente=" & vidcli & ",@pidc_sucursal=" & Session("idc_sucursal"))

        Catch ex As Exception
            WriteMsgBox("Error al Cargar Precio del Producto.  \n \u000b \n Error: \n" & ex.Message)
            Return
        End Try


        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("precio") <= 0 Then
                'Limpiar campos del articulo
                WriteMsgBox("No se Encontro el Precio de Producto. \n")
                Return
            End If
        Else
            'Limpiar campos del articulo
            WriteMsgBox("No se Encontro el Precio de Producto. \n")
            Return
        End If
        vprecio = dt.Rows(0).Item("precio")


        If lblroja.Visible = True Then
            Try
                If zidc_sucursal > 0 Then
                    dt1 = consulta("select dbo.fn_ver_precio_cliente_esp_cambio_lista(" & vidc & "," & vidcli & "," & zidc_sucursal & ") as pxprecio")
                Else
                    dt1 = consulta("select dbo.fn_ver_precio_cliente_esp_lp_SUC(" & vidc & "," & vidcli & "," & vidc_listap & "," & Session("idc_sucursal") & ") as pxprecio")
                End If
                vprecio = dt1.Rows(0).Item("pxprecio")
            Catch ex As Exception
                WriteMsgBox("No Se Procedio a Verificar Precios. \n- \n" & ex.Message)
            End Try

            Try


                dt2 = consulta("select dbo.fn_ver_precio_real_cliente_esp_cambio_lista(" & vidc & "," & vidcli & "," & zidc_sucursal & ") as pxprecior")
                rowprincipal("PrecioReal") = dt2.Rows(0).Item("pxprecior")
                Session("pprecio_real") = dt2.Rows(0).Item("pxprecior")
                rowprincipal("descuento") = Math.Round(vprecio - rowprincipal("PrecioReal"), 4)

                If rowprincipal("descuento") > 0 Then
                    rowprincipal("nota_credito") = True
                Else
                    rowprincipal("nota_credito") = False
                End If


            Catch ex As Exception
                WriteMsgBox("No Se procedio a Verificar Precios. \n - \n" & ex.Message)
            End Try
        End If




        rowprincipal("Costo") = dt.Rows(0).Item("costo")
        Dim viva As Integer = Session("NuevoIva")

        If txtrfc.Text.StartsWith("*") Then
            rowprincipal("precio") = Redondeo_cuatro_decimales(vprecio * (1 + (viva / 100)))
        Else
            rowprincipal("precio") = Redondeo_cuatro_decimales(vprecio)
        End If




        Try
            Dim gweb As New GWebCN.Productos
            Dim ds As New DataSet
            Dim row As DataRow
            Dim vprecio_real As Decimal = 0
            ds = gweb.Nota_Credito_Automatica(txtid.Text.Trim, codigo)
            If ds.Tables(0).Rows.Count > 0 Then
                row = ds.Tables(0).Rows(0)
                If lblroja.Visible = False Then
                    vprecio_real = row("precio_real")
                Else
                    vprecio_real = Session("pprecio_real")
                End If
                txtprecio.Enabled = False
                rowprincipal("Costo") = row.Item(8)
                rowprincipal("descuento") = row("descuento")
                rowprincipal("nota_credito") = True
                rowprincipal("PrecioReal") = Redondeo_cuatro_decimales(CDec(rowprincipal("precio")) - row("descuento"))
                If txtrfc.Text.StartsWith("*") Then
                    vprecio_real = Math.Round(vprecio_real * (1 + (Session("nuevoiva") / 100)), 4)
                    vprecio = Math.Round(row("precio") * (1 + (Session("nuevoiva") / 100)), 4)
                Else
                    vprecio = Math.Round(row("precio"), 4)
                End If
                rowprincipal("Precio") = vprecio
            Else
                rowprincipal("nota_credito") = False
                'rowprincipal("PrecioReal") = txtprecio.Text
                'rowprincipal("Precio") = vprecio
            End If

        Catch ex As Exception
            WriteMsgBox("No se procedio a buscar Nota de Credito Automatica de este Articulo. \n \u000B \n" & ex.Message)
            'btncancelar_Click(Nothing, EventArgs.Empty)
            Session("rowprincipal") = Nothing
        End Try
        Session("rowprincipal") = rowprincipal

        'Try
        '    Dim gweb As New GWebCN.Productos
        '    Dim ds As New DataSet
        '    Dim row As DataRow
        '    'Dim rowprincipal As DataRow
        '    'rowprincipal = Session("rowprincipal")
        '    ds = gweb.Nota_Credito_Automatica(txtid.Text.Trim, codigo)
        '    If ds.Tables(0).Rows.Count > 0 Then
        '        row = ds.Tables(0).Rows(0)
        '        If txtrfc.Text.StartsWith("*") Then
        '            txtprecio.Text = Redondeo_cuatro_decimales(row("precio") * ((Session("Xiva") / 100) + 1))
        '            'txtprecio.Text = Math.Round(CDec(txtprecio.Text), 4)
        '        Else
        '            txtprecio.Text = Redondeo_cuatro_decimales(row("precio"))
        '        End If
        '        txtprecio.Enabled = False
        '        rowprincipal("Costo") = row.Item(8)
        '        rowprincipal("descuento") = row("descuento")
        '        rowprincipal("nota_credito") = True
        '        rowprincipal("PrecioReal") = Redondeo_cuatro_decimales(CDec(txtprecio.Text) - row("descuento"))
        '    Else
        '        ds = gweb.buscar_precio_producto(codigo, txtid.Text.Trim(), Session("idc_sucursal")) '/*Cambiar 1 por la Var Session("")*/
        '        If ds.Tables(0).Rows.Count > 0 Then
        '            row = ds.Tables(0).Rows(0)
        '            If txtrfc.Text.StartsWith("*") Then
        '                txtprecio.Text = Redondeo_cuatro_decimales(row("precio") * ((Session("Xiva") / 100) + 1))
        '                'txtprecio.Text = Math.Round(CDec(txtprecio.Text), 4)
        '            Else
        '                txtprecio.Text = Redondeo_cuatro_decimales(row("precio"))
        '            End If
        '            rowprincipal("nota_credito") = False
        '            rowprincipal("Costo") = row.Item(1)
        '        End If
        '    End If
        '    Session("rowprincipal") = rowprincipal
        'Catch ex As Exception
        '    WriteMsgBox("No se procedio a buscar Nota de Credito Automatica de este Articulo. \n \u000B \n" & ex.Message)
        '    btncancelar_Click(Nothing, EventArgs.Empty)
        '    Session("rowprincipal") = Nothing
        'End Try


    End Sub

    'Carga Grid con clientes resultado de una busqueda.
    Sub cargarbusquedaclientes()
        Dim ds As New DataSet
        Dim gweb As New GWebCN.Clientes
        Try
            ds = gweb.buscarclientes(txtbuscar.Text.Trim())
            If ds.Tables(0).Rows.Count >= 1 Then
                '//gridbuscar_clientes.DataSource = ds
                '//gridbuscar_clientes.DataBind()
                ds.Tables(0).Columns.Add("nombre2")
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    ds.Tables(0).Rows(i).Item("nombre2") = ds.Tables(0).Rows(i).Item("nombre") & " || " & ds.Tables(0).Rows(i).Item("cveadi")
                Next
                cboclientes.DataSource = ds.Tables(0)
                cboclientes.DataValueField = "idc_cliente"
                cboclientes.DataTextField = "nombre2"
                cboclientes.DataBind()

                txtbuscar.Text = ""
                'txtbuscar.Visible = False
                'txtbuscar.Attributes("style") = "display:none;"

                cboclientes.Visible = True
                cboclientes.Attributes("style") = "display:inline;"
                cboclientes.Attributes("style") = "width:100%;"
                btnacep_bus.Visible = True
                btncan_bus.Visible = True
                btnbuscarcliente.CssClass = "Ocultar"
                cboclientes.Focus()
                '//seleccioncliente.Show()
                ViewState("dt_clientes") = ds.Tables(0)
            Else
                CargarMsgbox("", "No se encontro cliente con esa descripcion.", False, 2)
                txtbuscar.Text = ""
                txtbuscar.Focus()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            ds = Nothing
        End Try
    End Sub

    'Habilita los campos de datos del cliente para la captura de un nuevo pedido...
    Sub limpiar_campos_cliente()
        txtsucursalr.Text = "0"
        txtidOc.Text = "0"
        txtbuscar.Enabled = True
        txtbuscar.Text = ""
        txtstatus.BackColor = Drawing.Color.White
        txtnombre.BackColor = Drawing.Color.White
        txtrfc.Text = ""
        txtnombre.Text = ""
        txtstatus.Text = ""
        txtid.Text = ""
        'txttotalarticulos.Text = ""
        limpiar_campos()
        Dim dt As New DataTable
        dt = ViewState("dt")
        dt.Rows.Clear()
        ViewState("dt") = dt
        Dim rowprincipal As DataRow
        rowprincipal = Session("rowprincipal")
        rowprincipal = Nothing
        Session("rowprincipal") = rowprincipal
        carga_productos_seleccionadas()





    End Sub

    Sub Estado_controles_captura(ByVal estado As Boolean)
        'txtcodigoarticulo.Enabled = estado
        btnagregar.Enabled = estado
        'btncancelar.Enabled = estado
        txtprecio.Enabled = estado
        txtcantidad.Enabled = estado
        btncancelararticulo.Enabled = estado
    End Sub
#End Region

    Protected Sub grdproductos2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdproductos2.ItemDataBound
        'If (e.Item.ItemType <> ListItemType.Header And e.Item.ItemType And ListItemType.Footer) Then
        '    Dim txtpreciogrid As New TextBox
        '    txtpreciogrid = e.Item.FindControl("txtpreciogrid")
        '    txtpreciogrid.Attributes.Add("onblur", "LostFocus();")
        'End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblid As New Label
            lblid = e.Item.FindControl("lblid")
            e.Item.Cells(1).Attributes("onclick") = "return ver_ficha(" & lblid.Text & ");"
            e.Item.Cells(1).Attributes("onmouseover") = "cursor(this);"
            e.Item.Cells(1).Attributes("onmouseout") = "cursor_fuera(this);"

            Dim btnmobile As New ImageButton
            btnmobile = e.Item.FindControl("imgmobile")
            btnmobile.Attributes("onclick") = "return editar_precios_cantidad_1(" & lblid.Text.Trim & ");"

            If e.Item.Cells(10).Text = "True" Then
                e.Item.Attributes("style") = "color:red;"

            End If
        End If
    End Sub


    'Protected Sub grdproductos2_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdproductos2.UpdateCommand
    '    If e.CommandName = "Guardar" Then
    '        CargarMsgbox("", "Paso por Aki", False, 5)
    '    End If
    'End Sub

    Protected Sub txtcantidadgrid_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'grdproductos2.Items(Session("EditandoGrid")).Cells(4).FindControl("")
        'Dim lblimporte As Label = grdproductos2.Items(Session("Editandogrid")).Cells(5).FindControl("lblimporte")
        'Dim lbldescuento As Label = grdproductos2.Items(Session("Editandogrid")).Cells(7).FindControl("lbldescuento")
        'Dim txtcantidadgrid As TextBox = grdproductos2.Items(Session("Editandogrid")).Cells(4).FindControl("txtcantidadgrid")
        'Dim txtpreciogrid As TextBox = grdproductos2.Items(Session("Editandogrid")).Cells(4).FindControl("txtpreciogrid")
        'Dim txtprecioreal As TextBox = grdproductos2.Items(Session("EditandoGrid")).Cells(6).FindControl("txtprecioreal")
        'If validar_multiplos(dt.Rows(Session("Editandogrid")).Item(0), txtcantidadgrid.Text) = False Then
        '    Exit Sub
        'ElseIf dt.Rows(Session("Editandogrid")).Item(15) = True Then
        '    If No_Vender_Mas_De_Existencia(dt.Rows(Session("Editandogrid")).Item(0), CDec(txtcantidad.Text)) = False Then
        '        Exit Sub
        '    End If
        'End If



        'lblimporte.Text = Redondeo_cuatro_decimales((txtpreciogrid.Text) * (txtcantidadgrid.Text))
        'lbldescuento.Text = Redondeo_cuatro_decimales(txtpreciogrid.Text - txtprecioreal.Text)


        'dt.Rows(Session("Editandogrid")).Item(4) = txtcantidadgrid.Text
        'dt.Rows(Session("Editandogrid")).Item(5) = txtpreciogrid.Text
        'dt.Rows(Session("Editandogrid")).Item(6) = lblimporte.Text
        'dt.Rows(Session("Editandogrid")).Item(7) = txtprecioreal.Text
        'dt.Rows(Session("Editandogrid")).Item(8) = lbldescuento.Text
        'cargar_subtotal_iva_total()

        'Dim MaskCantidadGrid As New AjaxControlToolkit.MaskedEditExtender
        'MaskCantidadGrid = grdproductos2.Items(e.Item.ItemIndex).FindControl("maskcantidadgrid")
    End Sub

    Protected Sub txtpreciogrid_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim lblimporte As Label = grdproductos2.Items(Session("Editandogrid")).Cells(5).FindControl("lblimporte")
        'Dim lbldescuento As Label = grdproductos2.Items(Session("Editandogrid")).Cells(7).FindControl("lbldescuento")
        'Dim txtcantidadgrid As TextBox = grdproductos2.Items(Session("Editandogrid")).Cells(4).FindControl("txtcantidadgrid")
        'Dim txtpreciogrid As TextBox = grdproductos2.Items(Session("Editandogrid")).Cells(4).FindControl("txtpreciogrid")
        'txtpreciogrid.Text = Redondeo_cuatro_decimales(txtpreciogrid.Text)
        'Dim txtprecioreal As TextBox = grdproductos2.Items(Session("EditandoGrid")).Cells(6).FindControl("txtprecioreal")
        'If CDec(txtpreciogrid.Text) <> CDec(txtprecioreal.Text) Then
        '    txtprecioreal.Text = Redondeo_cuatro_decimales(txtpreciogrid.Text)
        'End If
        'lblimporte.Text = Redondeo_cuatro_decimales((CDec(txtpreciogrid.Text) * (txtcantidadgrid.Text)))
        'lbldescuento.Text = Redondeo_cuatro_decimales(txtpreciogrid.Text - txtprecioreal.Text)

        'dt.Rows(Session("Editandogrid")).Item(4) = txtcantidadgrid.Text
        'dt.Rows(Session("Editandogrid")).Item(5) = txtpreciogrid.Text
        'dt.Rows(Session("Editandogrid")).Item(6) = lblimporte.Text
        'dt.Rows(Session("Editandogrid")).Item(7) = txtprecioreal.Text
        'dt.Rows(Session("Editandogrid")).Item(8) = lbldescuento.Text
        'cargar_subtotal_iva_total()
    End Sub

    Protected Sub txtprecioreal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim lbldescuento As Label = grdproductos2.Items(Session("Editandogrid")).Cells(7).FindControl("lbldescuento")
        'Dim txtpreciogrid As TextBox = grdproductos2.Items(Session("Editandogrid")).Cells(4).FindControl("txtpreciogrid")
        'Dim txtprecioreal As TextBox = grdproductos2.Items(Session("EditandoGrid")).Cells(6).FindControl("txtprecioreal")
        'txtprecioreal.Text = Redondeo_cuatro_decimales(txtprecioreal.Text)
        'If CDec(txtpreciogrid.Text) >= CDec(txtprecioreal.Text) = True Then
        '    lbldescuento.Text = Redondeo_cuatro_decimales(CDec(txtpreciogrid.Text) - CDec(txtprecioreal.Text))
        'Else
        '    CargarMsgbox("", "El Precio Real no puede ser mayor al Precio.", False, 2)
        '    Exit Sub
        'End If
        'dt.Rows(Session("Editandogrid")).Item(7) = txtprecioreal.Text
        'dt.Rows(Session("Editandogrid")).Item(8) = lbldescuento.Text
        'cargar_subtotal_iva_total()
    End Sub


    ''''Para saber el valor antiguo de txtprecio y compararlo con txtprecioreal, almacenarlo en una variable
    'de session al inicio de el evento edit, y en el textchanged se recupera dicho valor y se hace la comparación
    ' `-´ ^_^ ^-^ ñ_ñ U_U

    ''Ademas de checar los valores, IVA, subiva, total, que se cambien al formato Moneda (FormatCurrency) utilizar la 
    'función "Formato_Moneda" la cual regresa una cadena de tipo String...

    Protected Sub btnfocus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfocus.Click
        'Dim txtpreciogrid As New TextBox
        'Dim txtprecioreal As New TextBox
        'Dim txtcantidadgrid As New TextBox
        'Dim lbldescuento As New Label
        'Dim lblimporte As New Label
        'lblimporte = grdproductos2.Items(Session("EditandoGrid")).FindControl("lblimporte")
        'lbldescuento = grdproductos2.Items(Session("EditandoGrid")).FindControl("lbldescuento")
        'txtcantidadgrid = grdproductos2.Items(Session("EditandoGrid")).FindControl("txtcantidadgrid")
        'txtpreciogrid = grdproductos2.Items(Session("EditandoGrid")).FindControl("txtpreciogrid")
        'txtprecioreal = grdproductos2.Items(Session("EditandoGrid")).FindControl("txtprecioreal")
        'If dt.Rows(Session("EditandoGrid")).Item(5) = True Then
        '    If validar_cantidad_decimales(txtcantidadgrid.Text) = False Then
        '        Exit Sub
        '    End If
        'End If
        'If validar_multiplos(dt.Rows(Session("EditandoGrid")).Item(0), txtcantidadgrid.Text) = False Then
        '    txtcantidadgrid.Text = ""
        '    txtcantidadgrid.Focus()
        '    Exit Sub
        'End If
        'If dt.Rows(Session("EditandoGrid")).Item(12) = True Then
        '    If dt.Rows(Session("EditandoGrid")).Item(15) = True Then
        '        If No_Vender_Mas_De_Existencia(dt.Rows(Session("EditandoGrid")).Item(0), txtcantidadgrid.Text) = False Then
        '            Exit Sub
        '        End If
        '    End If
        'End If

        'If Session("PrecioAntes") = txtprecioreal.Text Then
        '    txtprecioreal.Text = Redondeo_cuatro_decimales(CDec(txtpreciogrid.Text))
        '    Session("PrecioAntes") = txtpreciogrid.Text
        'ElseIf CDec(txtpreciogrid.Text) < CDec(txtprecioreal.Text) Then
        '    'CargarMsgbox("", "El Precio Real no puede ser mayor al Precio ", False, 4)
        '    txtprecioreal.Text = Redondeo_cuatro_decimales(txtpreciogrid.Text)
        '    Session("PrecioAntes") = txtpreciogrid.Text
        'Else
        '    txtprecioreal.Text = Redondeo_cuatro_decimales(txtprecioreal.Text)
        '    Session("PrecioAntes") = Redondeo_cuatro_decimales(txtpreciogrid.Text)
        'End If

        'txtpreciogrid.Text = Redondeo_cuatro_decimales(CDec(txtpreciogrid.Text))
        'lblimporte.Text = Redondeo_cuatro_decimales(txtcantidadgrid.Text * txtpreciogrid.Text)
        'lbldescuento.Text = Redondeo_cuatro_decimales(txtpreciogrid.Text - txtprecioreal.Text)
        'dt.Rows(Session("EditandoGrid")).Item(6) = Redondeo_cuatro_decimales(txtcantidadgrid.Text * txtpreciogrid.Text)
        'dt.Rows(Session("EditandoGrid")).Item(8) = Redondeo_cuatro_decimales(txtpreciogrid.Text - txtprecioreal.Text)
        'dt.Rows(Session("EditandoGrid")).Item(4) = txtcantidadgrid.Text
        'dt.Rows(Session("EditandoGrid")).Item(7) = txtprecioreal.Text
        'dt.Rows(Session("EditandoGrid")).Item(5) = txtpreciogrid.Text
        'Validar multiplos de articulo
        'Calcular_Valores_DataTable()
        'cargar_subtotal_iva_total()
    End Sub

    Sub Calcular_Valores_DataTable()
        Dim dt As New DataTable
        dt = ViewState("dt")
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i).Item(6) = Redondeo_Dos_Decimales(dt.Rows(i).Item(4) * dt.Rows(i).Item(5)) 'Importe = Cantidad * Precio
            If dt.Rows(i).Item("nota_credito") = False Then
                dt.Rows(i).Item(8) = dt.Rows(i).Item("precio") - dt.Rows(i).Item(7)
                dt.Rows(i).Item(8) = FormatNumber(dt.Rows(i).Item(8), 4)
            End If
        Next
    End Sub
    Public Function calculado(ByVal idc_articulo As Integer) As Double

        Try
            Dim gweb As New GWebCN.Productos
            Dim datos As New DataSet
            Dim row As DataRow
            datos = gweb.Articulo_calculado(idc_articulo)
            If datos.Tables(0).Rows.Count > 0 Then
                row = datos.Tables(0).Rows(0)
                Return row("porcentaje")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function buscar_articulos_duplicados(ByVal idc_articulo As Integer) As Boolean
        Dim dt As New DataTable
        dt = ViewState("dt")
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            For i As Integer = 0 To dt.Rows.Count - 1
                row = dt.Rows(i)
                If row("idc_articulo") = idc_articulo Then
                    Return True
                    Exit Function
                End If
            Next
            Return False
        End If
    End Function



    '/Proyectos [Combo]

    'Protected Sub cboproyectos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboproyectos.SelectedIndexChanged
    '    Dim table As New DataTable
    '    Dim row() As DataRow
    '    Dim idc_colonia As Integer
    '    If cboproyectos.SelectedIndex > 0 Then
    '        table = ViewState("XProyectos")
    '        row = table.Select("idc_proyec=" & cboproyectos.SelectedValue)
    '        idc_colonia = row(0)(2)
    '        cargar_datos_colonia_proyecto(idc_colonia)
    '        formar_cadenas()
    '        imgcroquis.Attributes("onClick") = "return verCroquis2();"
    '        validar_cambio_iva_Frontera()
    '        actualizar_precios(txtid.Text.Trim, txtidc_colonia.Text.Trim)
    '    Else
    '        'txtcolonia.Text = ""
    '        'txtestado.Text = ""
    '        'txtmunicipio.Text = ""
    '        'txtpais.Text = ""
    '        'txtidc_colonia.Text = ""
    '        'chkton.Checked = False
    '        'txttoneladas.Text = ""
    '        'txtCP.Text = ""
    '        'lblrestriccion.Text = ""
    '        limpiar_datos_consignado()
    '    End If
    'End Sub
    Sub cargar_datos_colonia_proyecto(ByVal idc_colonia As Integer)
        Dim ds As New DataSet
        Dim gweb As New GWebCN.Croquis_Direcciones
        Dim row As DataRow
        Try
            ds = gweb.Datos_Colonia(idc_colonia)
            If ds.Tables(0).Rows.Count > 0 Then
                row = ds.Tables(0).Rows(0)
                txtidc_colonia.Text = row("idc_colonia")
                txtmunicipio.Text = row("mpio")
                txtestado.Text = row("edo")
                txtpais.Text = row("pais")
                chkton.Checked = row("capacidad_maxima")
                txttoneladas.Text = row("toneladas")
                txtCP.Text = row("cod_postal")
                txtcolonia.Text = row("nombre")
                lblrestriccion.Text = row("restriccion")
            Else
                CargarMsgbox("", "La colonia no existe", False, 1)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnvercroquis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnvercroquis.Click
        'lblruta.Visible = True
        'lblruta.Text = fucroquis.FileName
        'lblruta.Text = System.IO.Path.GetExtension(fucroquis.FileName)
        'CargarMsgbox("", "La Extension es: " & lblruta.Text, False, 2)
        'If fucroquis.HasFile = True Then
        '    fucroquis.SaveAs(Request.PhysicalApplicationPath & "\" & "Croquis\")
        '    Request.PhysicalApplicationPath & "\" & "OC_Pendientes" & "\" & idc_occliente & ext(i)
        '    CargarMsgbox("", fucroquis.FileName, False, 2)
        'Else
        '    CargarMsgbox("", "Ya se perdio el archivo con el UpdatePanel", False, 2)
        'End If
        'fucroquis.Visible = False
        'Response.Write("<script>open('opener2.aspx?id=','NewWindow','top=0,left=0,width=0,height=0,status=yes,resizable=yes,scrollbars=yes');</script>")
        'Dim script As String = "<script>open('IMG_Croquis.aspx?id=" & lblruta.Text & "','NewWindow','top=0,left=0,width=0,height=0,status=yes,resizable=yes,scrollbars=yes');</script>"
        'Page.RegisterStartupScript("x", script)
    End Sub

    Protected Sub btncambiarcroquis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncambiarcroquis.Click
        'fucroquis.Visible = True ' Muestra FileUpload...
        fucroquis.Attributes("style") = "display:inline;"
        'imgupcroquis.Attributes("style") = "display:;"
        With lblruta                                '
            .Attributes("style") = "Display:none"   'Oculta Label con el nombre img croquis
            .Text = ""                              '     
        End With
        btncambiarcroquis.Attributes("style") = "Display:none" 'Oculta el botón "Cambiar Croquis"
        btnvercroquis.Attributes("style") = "Display:none"     'Oculta el botón "Cambiar Croquis"
    End Sub

    Protected Sub btnagregarcroquis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnagregarcroquis.Click
        If txtid.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>alert('Es Necesario Seleccionar Cliente'); </script>", False)
            Return
        End If
        If validar_tipo_archivo_img(System.IO.Path.GetExtension(fucroquis.FileName)) = True Then
            lblruta.Attributes("Style") = "Display:"
            lblruta.Text = fucroquis.FileName
            Dim ext As String = System.IO.Path.GetExtension(lblruta.Text).ToUpper()
            If fucroquis.HasFile = True Then
                Dim ext2(3) As String
                Dim existe As Boolean = False
                ext2(0) = ".JPG"
                ext2(1) = ".GIF"
                ext2(2) = ".DIB"
                ext2(3) = ".BMP"

                'Busca la imagen en la carpeta "Croquis" del proyecto, si existe la reemplaza. 
                For i As Integer = 0 To 3
                    If File.Exists(Request.PhysicalApplicationPath & "\" & "temp\files\" & txtid.Text.Trim & ext2(i)) Then
                        File.Delete(Request.PhysicalApplicationPath & "\" & "temp\files\" & txtid.Text.Trim & ext2(i))
                        guardar_temp_croquis(ext)
                        imgloading.Attributes("Style") = "display:none"
                        existe = True
                        Exit For
                    End If
                Next

                'Si no existe la imagen ntncs la guarda...
                If existe = False Then
                    guardar_temp_croquis(ext)
                    imgloading.Attributes("Style") = "display:none"
                End If
                btnvercroquis.Enabled = True
                btnvercroquis.Attributes("OnClick") = "return verCroquis();"
            End If

        Else
            CargarMsgbox("", "El tipo de imagen seleccionado no es valido.", False, 4)
        End If

    End Sub

    Sub guardar_temp_croquis(ByVal ext As String)
        Try
            fucroquis.SaveAs(Request.PhysicalApplicationPath & "\" & "temp\files\" & txtid.Text.Trim & ext)
            imgupcroquis.Attributes("style") = "display:none;"
            btncambiarcroquis.Attributes("style") = "display:"
            btnvercroquis.Attributes("style") = "display:"
            fucroquis.Attributes("style") = "display:none;"
        Catch ex As Exception
            Throw ex
        Finally
            ext = Nothing
        End Try

    End Sub
    Public Function validar_tipo_archivo_img(ByVal ext As String) As Boolean
        'Funcion utilizada para validar que la imagen del croquis tenga un formato valido...
        Dim extensiones_validas(5) As String
        Try
            extensiones_validas(0) = ".JPG"
            extensiones_validas(1) = ".BMP"
            extensiones_validas(2) = ".DIB"
            extensiones_validas(3) = ".GIF"
            extensiones_validas(4) = ".JPEG"
            extensiones_validas(5) = ".PNG"
            For i As Integer = 0 To extensiones_validas.Length - 1
                If extensiones_validas(i) Like ext.ToUpper() Then
                    Return True
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw ex
        Finally
            ext = Nothing
            extensiones_validas = Nothing
        End Try
    End Function

    Protected Sub fucroquis_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles fucroquis.Init

    End Sub

    Protected Sub btnagregarllamada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnagregarllamada.Click
        If txtid.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>alert('Es Necesario Seleccionar Cliente'); </script>", False)
            Return
        End If
        Dim extension As String = Path.GetExtension(fullamada.FileName.ToUpper())
        If validar_tipo_archivo_audio(Path.GetExtension(extension)) = True Then

            If fullamada.HasFile Then
                Dim ext(2) As String
                ext(0) = ".MP3"
                ext(1) = ".ACC"
                ext(2) = ".WMA"
                For i As Int16 = 0 To ext.Length - 1
                    If File.Exists(Request.PhysicalApplicationPath & "temp\files\" & txtid.Text & ext(i)) Then
                        File.Delete(Request.PhysicalApplicationPath & "temp\files\" & txtid.Text & ext(i))
                    End If
                Next
                fullamada.SaveAs(Request.PhysicalApplicationPath & "temp\files\" & txtid.Text.Trim & extension)
                fullamada.Attributes("style") = "display:none"
                imgupllamada.Attributes("style") = "display:none"
                btnescucharll.Enabled = True
                lblllamada.Attributes("style") = "display:"
                lblllamada.Text = fullamada.FileName
                btnquitarll.Attributes("Style") = "display:"
                btnescucharll.Attributes("Style") = "display:"
            End If

        Else
            CargarMsgbox("", "El tipo de archivo seleccionado no es valido.", False, 2)
        End If
    End Sub

    Public Function validar_tipo_archivo_audio(ByVal extension As String) As Boolean

        'Función utilizada para validar que la extension del archivo de llamada sea un formato valido.
        Dim ext(2) As String
        ext(0) = ".MP3"
        ext(1) = ".WMA"
        ext(2) = ".ACC"
        Try
            For i As Integer = 0 To ext.Length - 1
                If extension = ext(i) Then
                    Return True
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw ex
        Finally
            extension = Nothing
            ext = Nothing
        End Try

    End Function

    Protected Sub tbnguardarPP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbnguardarPP.Click
        'System.Threading.Thread.Sleep(500)
        'UpdateProgress5.Visible = True
        Try
            If txtstatus.Text = "4" Then
                WriteMsgBox("El Cliente esta Bloqueado Por Cheques Devueltos.")
                Return
            End If
            Dim sipasa2 As Integer
            Dim vrecoge As String = ""
            If cboentrega.SelectedValue = 1 Then   'Si es una entrega a Almacén.
                If validar_campos_direccion() = True Then
                    'Valida que la fecha este correcta...
                    'If validar_fecha_entrega(cbofechas.SelectedValue) = False Then
                    '    Exit Sub
                    'End If

                    'Valida si se cobraran maniobras...

                    If txtmaniobras.Text <> "0.00" And txtFolio.Text = "-1" Then
                        'CargarMsgbox("", "Es necesario cobrar las maniobras. <BR/> ¿Deseas capturar el Folio de Autorización?", True, 2)
                        'Session("Folio") = True
                        sipasa2 = 1
                        'ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                        'Return
                    Else
                        sipasa2 = 0
                    End If

                    'If txtFolio.Text <> "" Then
                    '    sipasa2 = txtFolio.Text.Trim()
                    'End If

                    'Valida el cambio de IVA
                    'Dim cambioIVA As Boolean = validar_cambio_iva_Frontera()
                    'If cambioIVA = True Then
                    '    If Not Session("cambioiva_a") = Session("NuevoIva") Then
                    '        CargarMsgbox("", "El IVA no corresponde a la zona de entrega, se recalculara a:" + CStr(CInt(Session("NuevoIva"))) + " % <BR/> ¿Estas de Acuerdo?", True, 2)
                    '        Session("cambiodeiva") = True       'Para identificar la operación solamente...
                    '        Return
                    '    End If
                    'End If

                Else
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                    Exit Sub

                End If
            ElseIf cboentrega.SelectedValue = 2 Then
                'Poner codigo para esta seccción...
            ElseIf cboentrega.SelectedValue = 3 Then  'Si el cliente recoge.
                If txtnombrerecoge.Text = "" Or txtpaternor.Text = "" Or txtsucursalr.Text = "0" Then
                    'CargarMsgbox("", "Falta Completar Datos de Donde va Recoger el Cliente.", False, 2)
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>alert('Falta Completar Datos de Donde va Recoger el Cliente.'); </script>", False)
                    'ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script> checarRDRC(); </script>", False)
                    'Dim mensaje2 As String = " <script> window.showModalDialog('Folio_Autorizacion.aspx?tipo=95','argumentos', 'dialogHeight: 40px; dialogWidth: 100px; dialogTop: 250px; dialogLeft: 250px; center: yes; resizable: no; status: no; location: no; scrolls=no;');</script>"
                    'ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrarpagina", "<script>RecogeCliente();</script>", False)  'Llamar la pantalla de folios.
                    'ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script> document.getElementById('<%=tbnguardarPP.ClientID%>').click(); </script>", False) 'Al cerrar la pantalla de folios se da click al btn generar pp again...
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                    Return
                Else
                    vrecoge = txtnombrerecoge.Text.Trim & Space(1) & txtpaternor.Text.Trim & Space(1) & txtmaternor.Text.Trim
                End If
            ElseIf cboentrega.SelectedValue = 4 Then ' Validar que los campos tengan datos...("campo otro cuando este seleccionado el RDOtro.")
                If txtformaP.Text <> "" And txtformaP.Text.Trim = "1" Then
                    If txtplazo.Text.Trim = "" Or txttelefono.Text.Trim = "" Or txtcontacto.Text.Trim = "" Or txtcminima.Text.Trim = "" Then
                        WriteMsgBox("Falta Completar Datos del Pre-Pedido Especial.")
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>alert('Falta Completar Datos del Pre-Pedido Especial.'); </script>", False)
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrarpagina", "<script>PedidoEspecial();</script>", False)  'Llamar la pantalla de folios.
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                        Return
                    End If
                Else
                    If txtplazo.Text.Trim = "" Or txttelefono.Text.Trim = "" Or txtcontacto.Text.Trim = "" Or txtcminima.Text.Trim = "" Or txtotro.Text.Trim = "" Then
                        WriteMsgBox("Falta Completar Datos del Pre-Pedido Especial.")
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>alert('Falta Completar Datos del Pre-Pedido Especial.'); </script>", False)
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrarpagina", "<script>PedidoEspecial();</script>", False)  'Llamar la pantalla de folios.
                        ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                        Return
                    End If
                End If
            End If


            '*aki*
            'Dim sitio_checkplus As New GWebCN.CheckPlus
            'Dim msj_check As String = ""
            'If Not (txtfolioCHP.Text = "") Then
            '    msj_check = sitio_checkplus.comprobar_chekplus(txtfolioCHP.Text, CDec(txttotal.Text), txtid.Text)
            '    If (msj_check <> "") Then
            '        WriteMsgBox(msj_check)
            '        Return
            '    End If
            'End If





            Dim mensaje As String = ""
            Dim mensaje_bd As String = String.Empty
            If oc.Text <> "" Then
                If oc.Text = True Then  'Si la orden de compra es obligatoria para realiza el pedido.
                    If txtnumeroOC.Text = Nothing Then 'Si no hay un numero de OC
                        mensaje = "* Es Requerido Ingresar la Orden de Compra del Cliente, Es necesario tambien Anexar la O.C. \n"
                        mensaje_bd = "* Es Requerido Ingresar la Orden de Compra del Cliente, Es necesario tambien Anexar la O.C." & vbCrLf
                    Else                               'Si se capturo un numero de OC
                        If validar_occli() = False Then  'Valida que el num de OC aun este pendiente("False").
                            WriteMsgBox("* La Orden de Compra YA NO ESTA PENDIENTE, Verifica el Numero de la Orden de Compra.")
                            ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                            Return
                        End If
                    End If
                End If
            End If

            If croquis.Text <> "" Then
                If croquis.Text = True Then 'Si el croquis es neceseario para el pedido.
                    If lblruta.Text = "" Then
                        mensaje = mensaje & "* Es Requerido Anexar el Croquis. \n"
                        mensaje_bd = mensaje_bd & "Es Requerido Anexar el Croquis." & vbCrLf
                    End If
                End If
            End If

            'Validar Check-Plus
            If txtfolioCHP.Text <> "" Then
                If ValidarCHP(True) = False Then
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                    Return
                End If
            End If

            '-------------------------------------------------------------------------------------------

            Dim ventdir As Integer
            If cboentrega.SelectedValue = 1 Then
                ventdir = 1
            ElseIf cboentrega.SelectedValue = 2 Then
                ventdir = 2
            ElseIf cboentrega.SelectedValue = 3 Then
                ventdir = 3
            Else
                ventdir = 4
            End If


            'Crear las Cadenas
            Dim dt As New DataTable
            dt = ViewState("dt")
            If Not dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                Return
            End If
            Dim vcadena_check As String = ""
            Dim vdarti As String = "", vdarti_nueva = ""
            Dim vdarti2 As String = "", vdarti2_nueva = ""
            Dim vtotal As Integer = 0
            Dim vartiuv As String = ""
            Dim vprer As Double = 0
            Dim vprecio As Double = 0
            Dim vid As String = 0
            Dim vcadenapeso As String = ""
            Dim vcantidad As String = ""
            Dim vexistencia As Double = 0
            Dim vcodigo As String = ""
            Dim vdescart As String = ""
            Dim vsin_exis As String = ""
            Dim vunimed As String = ""
            Dim vpre As Double = 0
            Dim vcosto As Double = 0
            Dim row As DataRow
            Dim vcomercial As Boolean
            'Dim v_IVA As Double
            Dim vcosbajo As String = ""
            Dim vztotal As Double
            Dim num_especif As Integer = 0
            Dim especif As String = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                row = dt.Rows(i)
                vtotal = vtotal + 1
                vdescart = row("Descripcion")
                vunimed = row("UM")
                vid = row("idc_articulo")
                vcantidad = row("cantidad")
                vprecio = row("precio")
                vcosto = IIf(IsDBNull(row("costo")), 0, row("costo"))
                vexistencia = IIf(IsDBNull(row("EXISTENCIA")), 0, row("EXISTENCIA"))
                vcodigo = row("codigo")
                vcadenapeso = vcadenapeso + vcadenapeso + Trim(CStr(vid)) + ";" + Trim(CStr(vcantidad)) + ";"
                vcomercial = CBool(row("comercial"))
                vprer = row("PrecioReal")
                vpre = row("precio")
                especif = IIf(IsDBNull(row("g_especif")), "", row("g_especif"))
                num_especif = IIf(IsDBNull(row("num_especif")), 0, row("num_especif"))

                '**** Aviso Material sin existencia...

                If vexistencia < vcantidad And vcomercial = True Then
                    vsin_exis = vsin_exis + vcodigo + "  " + Left(vdescart, 40) + "  " + Left(vunimed, 3) + "  " + CStr(vcantidad) + Chr(13)
                End If

                '--------------------------------------------


                '****Verificar si el precio es muy bajo
                If txtrfc.Text.StartsWith("*") Then
                    If Session("NuevoIva") <> 0 Then
                        'v_IVA = Session("NuevoIva")
                        vprecio = row("Precio") / (1 + (Session("NuevoIva") / 100))
                    Else
                        vprecio = row("precio") / (1 + (Session("Xiva") / 100))
                    End If
                Else
                    vprecio = row("precio")
                End If


                Dim vporciento As Double = (((vprecio - vcosto) / vprecio) * 100)
                If vporciento < 5 Then
                    vcosbajo = vcosbajo + vcodigo + "  " + Left(vdescart, 30) + "  " + Left(vunimed, 3) + Chr(13) + "Precio:" + CStr(vprecio) + "Costo:" + CStr(vcosto) + "  " + Trim(CStr(vporciento)) + "%" + Chr(13) + Chr(13)
                End If
                '
                'If txtrfc.Text.StartsWith("*") Then
                '    vdarti = vdarti + Trim(CStr(vid)) + ";" + Trim(CStr(vcantidad)) + ";" + Trim(CStr(vprecio)) + ";" + Trim(CStr(vprer)) + ";"
                'Else
                vdarti = vdarti + Trim(CStr(vid)) + ";" + Trim(CStr(vcantidad)) + ";" + Trim(CStr(vpre)) + ";" + Trim(CStr(vprer)) + ";"

                'End If

                vdarti_nueva = vdarti_nueva & Trim(CStr(vid)) + ";" + Trim(CStr(vcantidad)) + ";" + Trim(CStr(vpre)) + ";" + Trim(Str(vprer)) + ";0;" & especif.Replace(";", "&") & ";" & num_especif & ";"


                vdarti2 = vdarti2 + Trim(CStr(vid)) + ";" + Trim(CStr(vcantidad)) + ";" + Trim(CStr(vpre)) + ";"
                vdarti2_nueva = vdarti2_nueva + Trim(CStr(vid)) + ";" + Trim(CStr(vcantidad)) + ";" + Trim(CStr(vpre)) + ";0;" & especif.Replace(";", "&") & ";" & num_especif & ";"
                'vdarti2_nueva = vdarti2_nueva + ALLTRIM(Str(vid)) + ";" + ALLTRIM(Str(vcan, 14, 4)) + ";" + ALLTRIM(Str(vpre, 14, 4)) + ";0;"

                vcadena_check = vcadena_check + Trim(CStr(vid)) + ";" + Trim(vdescart) + ";" + Trim(CStr(vcantidad)) + ";" + Trim(CStr(vpre)) + ";"

                vartiuv = vartiuv + Trim(CStr(vid)) + ";"

            Next
            '-------------------------------------------------------------------------------------------


            Dim vdarti_prom = ""
            Dim vnum_prom = 0, VTOTA_NUEVO = 0
            Dim tx_pedido_gratis As New DataTable
            tx_pedido_gratis = Session("tx_pedido_gratis")

            If Not tx_pedido_gratis Is Nothing Then
                If tx_pedido_gratis.Rows.Count > 0 Then
                    For i As Integer = 0 To tx_pedido_gratis.Rows.Count - 1
                        vdarti_nueva = vdarti_nueva & Trim(CStr(tx_pedido_gratis.Rows(i).Item("idc_articulo"))) & ";" & Trim(CStr(tx_pedido_gratis.Rows(i).Item("cantidad"))) & ";0;0;" & Trim(CStr(tx_pedido_gratis.Rows(i).Item("idc_promociond"))) & ";" & ";" & "0;"
                        vdarti2_nueva = vdarti2_nueva & Trim(CStr(tx_pedido_gratis.Rows(i).Item("idc_articulo"))) & ";" & Trim(CStr(tx_pedido_gratis.Rows(i).Item("cantidad"))) & ";0;" & Trim(CStr(tx_pedido_gratis.Rows(i).Item("idc_promociond"))) & ";" & ";" & "0;"
                        VTOTA_NUEVO = VTOTA_NUEVO + 1
                    Next
                End If
            End If








            '****Articulos con limite de venta...
            ''Continuar aki, el Metodo esta abajito :))))

            If articulos_limite_de_venta(vcadenapeso, txtid.Text.Trim, vtotal, Session("xidc_almacen")) = False Then
                WriteMsgBox("Capturaste Articulos que ya NO estan Permitidos para Venta.")
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                Return
            End If
            '-------------------------------------------------------------------------------------------
            Dim vziva = Session("NuevoIva")
            vztotal = IIf(txtrfc.Text.Trim.StartsWith("*"), txtsubtotal.Text.Trim / (1 + vziva / 100), txtsubtotal.Text.Trim)

            '******Entrega Directa******
            If articulos_entrega_directa(vcadenapeso, txtid.Text.Trim, vtotal) = True And Not cboentrega.SelectedValue = 2 And vztotal < 5000 And txtFolioOc.Text = "" Then
                mensaje = mensaje & "* Hay Articulos que Tienen la Condición de Entrega Directa de Proveedor y el Monto Minimo es de $5,000 pesos. \n"
                mensaje_bd = mensaje_bd & "* Hay Articulos que Tienen la Condición de Entrega Directa de Proveedor y el Monto Minimo es de $5,000 pesos." & vbCrLf
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                Return
            End If
            '-------------------------------------------------------------------------------------------

            '******Monto Minimo de Ventas*******
            Dim monto_minimo As Double = Monto_Minimo_Venta(txtid.Text.Trim)
            Dim capacidad As Double
            If monto_minimo < 0 Then
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                Return
            End If
            If vztotal < monto_minimo And cboentrega.SelectedValue = 1 Then
                capacidad = volumen_carga(vcadenapeso, 11, vtotal)
                If capacidad = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                    Return
                End If
                If capacidad < 90 Then
                    'CargarMsgbox("", "", False, 1)
                    mensaje = mensaje & "* El Monto Minimo de Venta para este Cliente es de: " & Formato_moneda(monto_minimo) & "Pesos + I.V.A. o bien carga completa. \n"
                    mensaje_bd = mensaje_bd & "* El Monto Minimo de Venta para este Cliente es de: " & Formato_moneda(monto_minimo) & "Pesos + I.V.A. o bien carga completa." & vbCrLf
                End If
            End If
            '-------------------------------------------------------------------------------------------




            '***************
            Dim vtipom As String = "A"
            Dim vcambios As String = ""
            Dim vdesiva As Boolean = IIf(txtrfc.Text.Trim.StartsWith("*"), False, True)
            Dim vFechaEntrega As Date = DateTime.ParseExact(cbofechas.SelectedValue, "dd/MM/yyyy", Nothing)
            Dim vocc As String = txtnumeroOC.Text.Trim
            Dim vidpro As Integer
            Dim vpro As Boolean

            If txtproy.Text.Trim > 0 Then
                vidpro = txtproy.Text.Trim   ' idc_proyeco
                vpro = True
            Else
                vidpro = 0
                vpro = False
            End If


            Dim idc_colonia As Integer = txtidc_colonia.Text.Trim
            Dim calle As String = txtcalle.Text.Trim
            Dim numero As String = txtnumero.Text.Trim
            Dim cp As String = txtCP.Text.Trim
            Dim observaciones As String = txtobservaciones.Text.Trim
            Dim folioCHP As Integer = 0
            If txtfolioCHP.Text <> "" Then
                folioCHP = CInt(txtfolioCHP.Text.Trim)
            End If
            Dim direntrega As Boolean = IIf(cboentrega.SelectedValue = 1, True, False)  'rdEA.Checked
            Dim bitocc As Boolean = IIf(txtnumeroOC.Text.Trim <> "", True, False)
            Dim bitcroquis As Boolean = IIf(lblruta.Text.Trim <> "", True, False)
            Dim bitllamada As Boolean = IIf(lblllamada.Text.Trim <> "", True, False)

            If vidpro = 0 And cboentrega.SelectedValue = 1 Then
                Dim vfalta As Boolean = False
                If calle.Trim = Nothing Then
                    vfalta = True
                End If
                If bitocc = False Then
                    'vfalta = True
                End If
                If idc_colonia = Nothing Then
                    vfalta = True
                End If
                If vfalta = True Then
                    WriteMsgBox("Faltan de Completar Datos en el Consignado...Es Obligatorio")
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                    Return
                End If
            End If
            '-------------------------------------------------------------------------------------------
            ''Poner Aki Articulo de UnicaVenta
            ''///////////////////////////////////////////////////////////////////////////////////////////////
            ''///////////////////////////////////////////////////////////////////////////////////////////////
            ''///////////////////////////////////////////////////////////////////////////////////////////////
            ''///////////////////////////////////////////////////////////////////////////////////////////////
            Dim unicaventa As Boolean
            unicaventa = Unica_Venta(vartiuv, vtotal)
            If unicaventa = False Then
                WriteMsgBox("Capturaste un Producto de Unica Venta, Que ya Fue Utilizado en Otra Venta")
                Return
            End If
            '-------------------------------------------------------------------------------------------


            '**Verfificar Limite....
            Dim limite As Boolean = Verificar_Limite(txtid.Text.Trim, CDbl(txttotal.Text.Trim))
            Dim pasa_limite As Boolean
            If limite = Nothing Then
                pasa_limite = True ' True si SobrePasa el limite.
                'CargarMsgbox("", "El monto de este Pedido Sobrepasa el Limite de Credito Permitido para el Cliente <BR/> No se Puede Generar el Pre-Pedido...Verificalo con Creditos...", False, 1)
                'Return
            ElseIf limite = False Then
                pasa_limite = True ' True si SobrePasa el limite.
                'CargarMsgbox("", "El monto de este Pedido Sobrepasa el Limite de Credito Permitido para el Cliente <BR/> No se Puede Generar el Pre-Pedido...Verificalo con Creditos...", False, 1)
                'Return
            Else
                pasa_limite = False 'False si no Sobrepasa el limite.
            End If
            '-------------------------------------------------------------------------------------------


            '**Checar donde va entrar
            Dim vidc_listap As Integer = txtlistap.Text.Trim
            Dim vcambio_lista As Boolean = IIf(cboentrega.SelectedValue = 2, False, True)


            Dim ventrar As Integer
            Dim camb_precios As Boolean
            'camb_precios = Cambios_Precios(vdarti, vtotal, txtid.Text.Trim, Session("pxidc_sucursal"), vcambio_lista)
            camb_precios = False
            'If camb_precios =   Then
            '    CargarMsgbox("", "No se procedio a Verificar si el Cliente esta Bloqueado", False, 1)
            '    Return
            'End If
            If camb_precios = True Then
                ventrar = 1
            Else
                Dim vblo As Boolean = Cliente_Bloqueado(txtid.Text.Trim)
                If (vblo = True Or pasa_limite = True) And txtfolioCHP.Text = "" Then 'Aquiiiii le falta "Pasa"
                    ventrar = 2
                Else
                    ventrar = 3
                End If
            End If
            '-------------------------------------------------------------------------------------------


            ''
            If cboentrega.SelectedValue = 4 Then
                ventrar = 4
            End If
            Dim sipasa As Integer
            ' And txtFolioOc.Text = "" 
            If mensaje <> "" Then
                'CargarMsgbox("", "El Pedido Tiene Varios Detalles: <BR/>" & mensaje & "<BR/><strong>Completa la Información Necesaria o Pide un Folio de Autorización</strong> <BR/> ¿Deseas Capturar el Folio de Autorización?", True, 1)
                WriteMsgBox2("El Pedido Tiene Varios Detalles: \n \u000B \n" & mensaje & "\n \u000B \n El Pre-Pedido Requerira Folio de Autorización.")
                'Session("FolioOC") = True
                'Session("cargarFlete") = False
                'Session("TipoAutorizacion") = IIf(ventrar = 3, 4, 15)
                'ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                'Return
                sipasa = 1
                txtFolioOc.Text = "1"
            Else
                sipasa = 0
            End If
            'If txtFolioOc.Text <> "" Then
            '    sipasa = txtFolioOc.Text
            'End If
            If ventrar <= 4 And (txtformapago.Text = "" And (txtfolioCHP.Text = "" Or txtfolioCHP.Text = "0")) Then
                If lblconfirmacion.Visible = True Then
                    'ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>window.open('confirmacion_de_pago_mobile.aspx','null', 'width=360px,height=308px,left=300,top=250,Menubar=no,Scrollbars=no,location=no'); </script>", False)
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>alert('Es Necesario Confirmar Pago.')</script>", False)
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                    Return
                End If
            End If

            Dim formapago() As String
            Dim tipopago As Integer = 0
            Dim idc_banco As Integer = 0
            Dim fecha_deposito As Date = Nothing
            Dim monto As Double = 0
            Dim observacionesP As String = ""
            Dim vconfirmar_pago As Boolean = lblconfirmacion.Visible

            If vconfirmar_pago = True And ventrar = 2 Then
                ventrar = 3
            End If

            If txtfolioCHP.Text <> "" Then
                txtformapago.Text = ""
            End If

            If txtformapago.Text <> "" And lblconfirmacion.Visible = True Then
                formapago = Split(txtformapago.Text, "%")
                tipopago = formapago(0)
                idc_banco = formapago(1)
                monto = formapago(2)
                fecha_deposito = formapago(3)
                observacionesP = formapago(4)
            Else
                fecha_deposito = Now
            End If
            If txtidOc.Text = "" Then
                txtidOc.Text = "0"
            End If
            If txtsucursalr.Text = "" Then
                txtsucursalr.Text = "0"
            End If
            Dim intentos As Integer = 0
            Dim vtip As String = ""
            Dim vleyo As String = IIf(ventrar = 3, "Pedido", "Pre-Pedido")

            ''Guardar el pedido
            Dim gweb As New GWebCN.Guardar_Pedido
            Dim ds As New DataSet
            Dim ip As String = Request.ServerVariables("REMOTE_ADDR")
            Dim pc As String = "" 'System.Net.Dns.GetHostEntry(Request.ServerVariables("remote_addr")).HostName
            Dim usuariopc As String = Request.ServerVariables("LOGON_USER")
            Dim resultado As String = ""
            Dim vcadena As String
            Dim vfecha As String
            Dim date1 As Date = Now()
            Dim vagenda As Boolean = False
            Dim dateOffset As New DateTimeOffset(date1, TimeZoneInfo.Local.GetUtcOffset(date1))
            vfecha = date1.ToString("o")
            vfecha = vfecha.Replace("T", " ")
            vfecha = vfecha.Split(".")(0) & "." & vfecha.Split(".")(1).Substring(0, 3)
            Dim vtacti As Integer


            If Session("actividad_agente") <> Nothing Then
                vcadena = Session("actividad_agente") & ";" & vfecha & ";" & IIf(vagenda, 1, 0) & ";" & 1 & ";;;" & Session("vfecha2") & ";"
                vtacti = 1
            Else
                vcadena = ""
                vtacti = 0
            End If
            Dim vtipog As String = ""
            vtipog = IIf(ventrar = 2 Or ventrar = 4, "C", "V")
            vtipog = IIf(ventrar = 3, "", vtipog)



            'Para lo de Promociones

            VTOTA_NUEVO = VTOTA_NUEVO + vtotal

            Dim pdetalles As String = String.Empty
            Dim pfleteaut As Decimal = 0
            If sipasa2 = 1 Then
                ventrar = 1
                pfleteaut = CDec(txtmaniobras.Text.Trim)
                sipasa2 = 0
                vleyo = "Pre-Pedido"
                vtipog = "V"
            End If

            If sipasa = 1 Then
                ventrar = 1
                pdetalles = CStr(mensaje_bd)
                sipasa = 0
                vleyo = "Pre-Pedido"
                vtipog = "V"
            End If
            Dim ID As Integer = 0
            If ExistenArchivos(bitocc, bitcroquis) Then
                Select Case ventrar
                    Case 1
                        ds = gweb.apreped_cambios_precios1(txtid.Text.Trim, txtpretotal.Text.Trim, Session("idc_sucursal"), vdesiva, Session("Xidc_iva"), Session("idc_usuario"),
                                                           Session("xidc_almacen"), ip, pc, usuariopc, vtipom, vcambios,
                                                           vdarti, vtotal, DateTime.ParseExact(cbofechas.SelectedValue, "dd/MM/yyyy", Nothing), vpro, vidpro, txtnumeroOC.Text.Trim _
                                                           , idc_colonia, calle, numero, cp, txtobservaciones.Text.Trim, folioCHP, sipasa, ventdir,
                                                           bitcroquis, bitocc, vsin_exis, vcosbajo, 0, bitllamada, mensaje, sipasa2, vtipog,
                                                           CDbl(txtmaniobras.Text.Trim), vrecoge, txtidOc.Text, txtsucursalr.Text.Trim, tipopago, idc_banco,
                                                           fecha_deposito, monto, observacionesP, vconfirmar_pago, vcadena, vtacti, 0, Nothing, vdarti_nueva,
                                                           VTOTA_NUEVO, IIf(pdetalles <> "", pdetalles, ""), IIf(pfleteaut > 0, pfleteaut, Nothing))

                    Case 2
                        ds = gweb.sp_apreped_creditos1(txtid.Text.Trim, txtpretotal.Text.Trim, Session("idc_sucursal"), vdesiva, Session("Xidc_iva"), Session("idc_usuario"), Session("xidc_almacen"),
                                                       ip, pc, usuariopc, vtipom, vcambios, vdarti2, vtotal,
                                                       DateTime.ParseExact(cbofechas.SelectedValue, "dd/MM/yyyy", Nothing), vpro, vidpro, txtnumeroOC.Text.Trim, idc_colonia, calle, numero, cp, observaciones, folioCHP, sipasa, ventdir, bitcroquis, bitocc, vsin_exis, vcosbajo, 0, bitllamada, mensaje, vtipog, sipasa2, CDbl(txtmaniobras.Text.Trim), vrecoge, txtidOc.Text, txtsucursalr.Text.Trim, tipopago, idc_banco, fecha_deposito, monto, observacionesP, vconfirmar_pago, vcadena, vtacti, 0, Nothing, vdarti_nueva, VTOTA_NUEVO)

                    Case 3
                        ds = gweb.guardarP4(txtid.Text.Trim, txtpretotal.Text.Trim, Session("idc_sucursal"), vdesiva, Session("Xidc_iva"), Session("idc_usuario"),
                                            Session("xidc_almacen"), ip, pc, usuariopc, vtipom, vcambios,
                                            vdarti2, vtotal, DateTime.ParseExact(cbofechas.SelectedValue, "dd/MM/yyyy", Nothing), vpro, vidpro, Me.txtnumeroOC.Text.Trim,
                                            idc_colonia, calle, numero, cp, observaciones, folioCHP, sipasa, ventdir, bitocc, bitcroquis, vsin_exis, vcosbajo, 0, bitllamada, mensaje, sipasa2, CDbl(txtmaniobras.Text.Trim), vrecoge, 0, 0, txtidOc.Text.Trim, txtsucursalr.Text.Trim, tipopago, idc_banco, DateTime.ParseExact(cbofechas.SelectedValue, "dd/MM/yyyy", Nothing), monto, observacionesP, vconfirmar_pago, vcadena, vtacti, 0, Nothing, vdarti_nueva, VTOTA_NUEVO)  'vdarti2_nueva 10-08-2015, se cambio para que reciba el precio real y se guarde directamente en pedidos


                    Case 4
                        ds = gweb.apreped_creditos_especial_NC1(txtid.Text.Trim, txtpretotal.Text.Trim, Session("idc_sucursal"), vdesiva, Session("Xidc_iva"), Session("idc_usuario"),
                                                                Session("xidc_almacen"), ip, pc, usuariopc, vtipom, vcambios,
                                                                vdarti, vtotal, DateTime.ParseExact(cbofechas.SelectedValue, "dd/MM/yyyy", Nothing), vpro, vidpro, txtnumeroOC.Text.Trim,
                                                                idc_colonia, calle, numero, cp, observaciones, folioCHP, sipasa, ventdir, bitcroquis, bitocc, vsin_exis, vcosbajo, 0, bitllamada, mensaje, vtipog, sipasa2, CDbl(txtmaniobras.Text.Trim), vrecoge, txtplazo.Text.Trim, txtformaP.Text.Trim, txtotro.Text.Trim, txtcminima.Text.Trim, txtcontacto.Text.Trim, txttelefono.Text.Trim, txtcorreo.Text.Trim, txtidOc.Text.Trim, txtsucursalr.Text.Trim, vcadena, vtacti, 0, Nothing, vdarti_nueva, VTOTA_NUEVO)

                End Select

                If ds.Tables(0).Rows.Count > 0 Then
                    resultado = ds.Tables(0).Rows(0).Item(0).ToString().Trim().Replace("'", "")
                    ID = ds.Tables(0).Rows(0)("CODIGOP")
                End If
            Else
                resultado = "LA RUTA DE UN ARCHIVO(ORDEN DE COMPRA O CROQUIS) NO FUE ENCONTRADA, COMUNIQUESE AL DEPARTAMENTO DE SISTEMAS"
            End If
            'UpdateProgress5.Visible = False

            If resultado = "" Then
                GuardarArchivos(ID, bitllamada, bitocc, bitcroquis, vleyo)
                cargar_consecutivo_folio()
                colores_clear()
                btnnuevoprepedido_Click(Nothing, EventArgs.Empty)
                cboentrega.SelectedValue = 1
                If Request.QueryString("tipo") = 1 Then
                    Session("actividad_agente") = Nothing
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>alert('Se Guardo el " & vleyo & " con Exito \n No. " & vleyo & ": " & ID & "'); </script>", False)
                    'ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script> return redirecting(); </script>", False)
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrarpagina", "<script>redirecting();</script>", False)
                    'Response.Redirect("ficha_cliente.aspx")
                    'Threading.Thread.Sleep(0)
                    'Response.AddHeader("REFRESH", "10;URL=ficha_cliente.aspx")
                Else
                    'CargarMsgbox("", "Se Guardo el " & vleyo & " con Exito <BR/> No. " & vleyo & ": " & resultado, False, 2)
                    Dim msj As String = "Se Guardo el " & vleyo & " con Exito \n \u000b \n No. " & vleyo & ": " & ID
                    WriteMsgBox(msj)
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)
                End If
            Else
                presentacion.Alert.ShowAlertError(resultado, Me.Page)
            End If
        Catch ex As Exception
            'CargarMsgbox("", ex.Message, False, 4)
            WriteMsgBox2("Error: \n" & ex.Message.Replace("'", ""))
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_guardando", "<script>myStopFunction_guard();</script>", False)

        End Try
    End Sub
    Function ExistenArchivos(ByVal bitoc As Boolean, ByVal bitcroquis As Boolean) As Boolean
        Try
            Dim ext2(5) As String   'Arreglo .ext imagenes.
            ext2(0) = ".JPG"
            ext2(1) = ".GIF"
            ext2(2) = ".DIB"
            ext2(3) = ".BMP"
            ext2(4) = ".JPEG"
            ext2(5) = ".PNG"

            Dim ext(2) As String   'Arreglo .ext audio.
            ext(0) = ".MP3"
            ext(1) = ".ACC"
            ext(2) = ".WMA"
            Dim ruta As String
            Dim unidad As String = ""
            Dim ret As Boolean = False

            If txtnumeroOC.Text <> "" And bitoc = True Then

                For i As Integer = 0 To 3
                    ruta = Request.PhysicalApplicationPath & "\temp\files\" & txtidOc.Text & ext2(i)
                    If File.Exists(ruta) Then
                        ret = True
                        Exit For
                    End If
                Next
            End If


            If lblruta.Text <> "" Then
                For i As Integer = 0 To ext.Length - 1
                    ruta = Request.PhysicalApplicationPath & "\temp\files\" & txtid.Text & ext2(i)
                    If File.Exists(ruta) Then
                        ret = True
                        Exit For
                    End If
                Next
            End If

            If bitcroquis = False And bitcroquis = False Then
                ret = True
            End If
            Return ret
        Catch ex As Exception
            WriteMsgBox2(ex.ToString())
            Return False
        End Try
    End Function
    Sub GuardarArchivos(ByVal NoPedido As Integer, ByVal bitllamada As Boolean, ByVal bitoc As Boolean, ByVal bitcroquis As Boolean, ByVal tipop As String)
        Try
            Dim ext2(5) As String   'Arreglo .ext imagenes.
            ext2(0) = ".JPG"
            ext2(1) = ".GIF"
            ext2(2) = ".DIB"
            ext2(3) = ".BMP"
            ext2(4) = ".JPEG"
            ext2(5) = ".PNG"

            Dim ext(2) As String   'Arreglo .ext audio.
            ext(0) = ".MP3"
            ext(1) = ".ACC"
            ext(2) = ".WMA"
            Dim ruta As String
            Dim unidad As String = ""

            If txtnumeroOC.Text <> "" And bitoc = True Then

                For i As Integer = 0 To 3
                    ruta = Request.PhysicalApplicationPath & "\temp\files\" & txtidOc.Text & ext2(i)
                    If File.Exists(ruta) Then
                        If tipop = "Pedido" Then
                            unidad = unidades("PED_OC")
                        Else
                            unidad = unidades("PPED_OC")
                        End If
                        If unidad <> "" Then
                            unidad = unidad & NoPedido & ext2(i)
                            My.Computer.FileSystem.MoveFile(ruta, unidad, True)
                            unidad = ""
                            Exit For
                        End If

                    End If
                Next
            End If


            If lblllamada.Text <> "" And bitllamada = True Then
                For i As Integer = 0 To ext.Length - 1
                    ruta = Request.PhysicalApplicationPath & "\temp\files\" & txtid.Text & ext(i)
                    If File.Exists(ruta) Then
                        If tipop = "Pedido" Then
                            unidad = unidades("PED_LLA")
                        Else
                            unidad = unidades("PPD_LLA")
                        End If
                        If unidad <> "" Then
                            unidad = unidad & NoPedido & ext(i)
                            My.Computer.FileSystem.MoveFile(ruta, unidad, True)
                            unidad = ""
                            Exit For
                        End If
                    End If
                Next
            End If

            If lblruta.Text <> "" Then
                For i As Integer = 0 To ext.Length - 1
                    ruta = Request.PhysicalApplicationPath & "\temp\files\" & txtid.Text & ext2(i)
                    If File.Exists(ruta) Then
                        If tipop = "Pedido" Then
                            unidad = unidades("PED_CRO")
                        Else
                            unidad = unidades("PPED_CR")
                        End If
                        If unidad <> "" Then
                            unidad = unidad & NoPedido & ext2(i)
                            My.Computer.FileSystem.MoveFile(ruta, unidad, True)
                            unidad = ""
                            Exit For
                        End If
                    End If
                Next
                fucroquis.Attributes("style") = "display:inline;"
                With lblruta                                '
                    .Attributes("style") = "Display:none"   'Oculta Label con el nombre img croquis
                    .Text = ""                              '     
                End With
                btncambiarcroquis.Attributes("style") = "Display:none" 'Oculta el botón "Cambiar Croquis"
                btnvercroquis.Attributes("style") = "Display:none"     'Oculta el botón "Cambiar Croquis"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function unidades(ByVal clave As String) As String
        Dim gweb As New GWebCN.Unidades
        Dim unidad As String
        Dim ds As New DataSet
        Try
            ds = gweb.Unidad_Archivos(clave)
            If ds.Tables(0).Rows.Count > 0 Then
                unidad = ds.Tables(0).Rows(0).Item("unidad")
                Return unidad
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Verificar_Limite(ByVal idc_cliente As Integer, ByVal monto As Double) As Boolean
        Dim gweb As New GWebCN.Productos
        Try
            Return gweb.Verificar_Limite(idc_cliente, monto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Cliente_Bloqueado(ByVal idc_cliente As Integer) As Boolean
        Dim gweb As New GWebCN.Productos
        Try
            Return gweb.Cliente_Bloqueado(idc_cliente)
        Catch ex As Exception
            CargarMsgbox("", ex.Message, False, 1)
        End Try
    End Function
    Public Function Unica_Venta(ByVal cadena As String, ByVal totalarticulos As Integer) As Boolean
        Dim gweb As New GWebCN.Productos
        Try
            Return CBool(gweb.Unica_Venta(cadena, totalarticulos))
        Catch ex As Exception
            CargarMsgbox("", ex.Message, False, 1)
        End Try
    End Function
    Public Function Cambios_Precios(ByVal cadena As String, ByVal totalarticulos As Integer, ByVal idc_cliente As Integer, ByVal idc_sucursal As Integer, ByVal vcambio_lista As Boolean) As Boolean
        Dim gweb As New GWebCN.Productos
        Dim resutado As Boolean
        Try
            resutado = gweb.Cambio_Precios(cadena, totalarticulos, idc_cliente, idc_sucursal, vcambio_lista)
            Return resutado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function volumen_carga(ByVal cadenapeso As String, ByVal tipocamion As Integer, ByVal totalarticulos As Integer) As Double
        Dim gweb As New GWebCN.Productos
        Try
            Return gweb.Volumen_Carga(cadenapeso, tipocamion, totalarticulos)
        Catch ex As Exception
            CargarMsgbox("", ex.Message, False, 1)
        End Try
    End Function
    Public Function Monto_Minimo_Venta(ByVal idc_cliente As Integer) As Double
        Dim gweb As New GWebCN.Productos
        Try
            Return gweb.Monto_Minimo_Venta(idc_cliente)
        Catch ex As Exception
            CargarMsgbox("", ex.Message, False, 1)
        End Try

    End Function
    Public Function articulos_entrega_directa(ByVal peso As String, ByVal idc_cliente As Integer, ByVal totalarticulos As Double) As Boolean
        Dim gweb As New GWebCN.Productos
        Try
            Return gweb.Articulos_Entrega_Directa(peso, idc_cliente, totalarticulos)
        Catch ex As Exception
            CargarMsgbox("", ex.Message, False, 1)
        End Try
    End Function
    Public Function articulos_limite_de_venta(ByVal cadena As String, ByVal idc_cliente As Integer, ByVal total As Integer, ByVal idc_alamacen As Integer) As Boolean
        Dim gweb As New GWebCN.Productos
        Try
            Return gweb.Articulos_Cantidad_Permitida(cadena, idc_cliente, total, idc_alamacen)
        Catch ex As Exception
            CargarMsgbox("", ex.Message, False, 1)
        End Try
    End Function
    Public Function validar_occli() As Boolean
        Dim gweb As New GWebCN.OC_Digitales
        Try
            Return gweb.validar_oc_cliente(txtidOc.Text.Trim)
        Catch ex As Exception
            CargarMsgbox("", ex.Message, False, 1)
        End Try
    End Function
    Public Function validar_cambio_iva_Frontera() As Boolean
        If txtidc_colonia.Text.Trim <> "" And txtidc_colonia.Text.Trim <> "0" Then
            Dim gweb As New GWebCN.IVA
            Dim row As DataRow
            Try
                row = gweb.validar_cambio_IVA_frontera(txtidc_colonia.Text, Session("idc_sucursal"))
                If row.Item(0) = False Then
                    If Not Session("NuevoIva") = row.Item(2) Then
                        Session("ivaant") = Session("NuevoIva")
                        Session("idc_ivaant") = Session("nuevo_idc_iva")
                        Session("NuevoIva") = row.Item(2)
                        Session("nuevo_idc_iva") = row.Item(1)
                        Session("pidc_iva") = row.Item(2)
                        WriteMsgBox("El IVA no Corresponde a la Zona de Entrega se Recalculara el IVA al " & CInt(Session("NuevoIva")).ToString & "%")
                        Ajustes_iva(Session("NuevoIva"), Session("ivaant"))
                        etiqueta_Iva(Session("NuevoIva"))
                        Return True
                    End If
                ElseIf row.Item(0) = True Then
                    If Session("lidc_iva") <> Session("pidc_iva") Then
                        Session("pidc_iva") = Session("lidc_iva")
                        Session("ivaant") = Session("NuevoIva")
                        Session("idc_ivaant") = Session("nuevo_idc_iva")
                        Session("NuevoIva") = row.Item(2).ToString
                        Session("nuevo_idc_iva") = row.Item(1).ToString
                        WriteMsgBox("El IVA no Corresponde a la Zona de Entrega se Recalculara el IVA al " & CInt(Session("NuevoIva")).ToString & "%")
                        Ajustes_iva(Session("NuevoIva"), Session("ivaant"))
                        etiqueta_Iva(Session("NuevoIva"))
                        Return True
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
        Else
            If Not Session("NuevoIva") = Session("Xiva") Then
                Session("ivaant") = Session("NuevoIva")
                Session("NuevoIva") = Session("Xiva")
                WriteMsgBox("El IVA no Corresponde a la Zona de Entrega se Recalculara el IVA al " & CInt(Session("NuevoIva")).ToString & "%")
                Ajustes_iva(Session("NuevoIva"), Session("ivaant"))
                etiqueta_Iva(Session("NuevoIva"))
            End If

        End If
    End Function
    Public Function validar_campos_direccion() As Boolean
        If txtidc_colonia.Text = "0" Then
            CargarMsgbox("", "Faltan de Completar Datos en el Consignado...Es Obligatorio.", False, 1)
            Return False
        ElseIf cbofechas.SelectedIndex < 0 Then
            CargarMsgbox("", "Es Necesario Ingresar la Fecha de Entrega", False, 1)
            Return False
        Else
            Return True
        End If
    End Function
    Public Function validar_fecha_entrega(ByVal fecha_seleccionada As Date) As Boolean
        Dim gweb As New GWebCN.Fechas
        Dim fecha_maxima As Date
        Try
            fecha_maxima = gweb.Fecha_Maxima_Entrega
            If fecha_maxima > fecha_seleccionada And fecha_seleccionada >= Today Then
                Return True
            Else
                If fecha_seleccionada <= Today Then
                    CargarMsgbox("", "La Fecha debe ser mayor o igual al Día de Hoy", False, 2)
                ElseIf fecha_seleccionada > fecha_maxima Then
                    CargarMsgbox("", "La Fecha Maxima de Entrega es: " & CStr(FormatDateTime(fecha_maxima, 2)), False, 2)
                End If
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            gweb = Nothing
            fecha_maxima = Nothing
            fecha_seleccionada = Nothing
        End Try
    End Function

    Sub fecha_txt()
        Dim gweb As New GWebCN.Fechas
        Dim dt_fechas_vendedores As DataTable

        Try
            dt_fechas_vendedores = gweb.Fechas_Vendedor
            If dt_fechas_vendedores.Rows.Count > 0 Then
                cbofechas.DataSource = dt_fechas_vendedores
                cbofechas.DataTextField = "fechamostrar"
                cbofechas.DataValueField = "fecha"
                cbofechas.DataBind()
            End If
        Catch ex As Exception
            WriteMsgBox("Error al Cargar Fechas. \n \u000B \n Error: \n" & ex.Message)
        End Try
    End Sub

    Protected Sub btnvalidaChP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnvalidaChP.Click
        ValidarCHP(False)
    End Sub
    Public Function ValidarCHP(ByVal agregar As Boolean) As Boolean
        Dim gweb As New GWebCN.CheckPlus
        Dim resultado As String
        Try
            If txtfolioCHP.Text <> "0" And txttotal.Text.Trim <> "" Then
                resultado = gweb.comprobar_chekplus(txtfolioCHP.Text.Trim, txttotal.Text.Trim, txtid.Text.Trim)
                If Not resultado = "" Then
                    'CargarMsgbox("", resultado, False, 1)  ' Valida el check Plus y muestra msj en caso de que lo contenga.
                    WriteMsgBox2(resultado)
                    txtfolioCHP.Text = IIf(agregar = True, txtfolioCHP.Text, "")
                    txtfolioCHP.Focus()
                    Return False
                Else
                    'Agregar el Check Plus a la lista de productos, y hacer los calculos necesarios.
                    If agregar = True Then
                        Agregar_CheckPlus()
                    Else
                        WriteMsgBox2("No. Preautorizacion Correcto.")
                    End If
                    Return True
                End If
            End If
        Catch ex As Exception
            WriteMsgBox2("Error al Validar Check Plus. \n \u000B \n Error: \n" & ex.Message)
        End Try
    End Function
    Sub Agregar_CheckPlus()
        If buscar_articulos_duplicados(4406) = True Then
            'True cuando ya este agregado el cargo por pago con cheque...y termina el ciclo.
            Exit Sub
        Else
            Dim codigo As String
            Dim dt2 As New DataTable
            dt2 = GWebCD.clsConexion.EjecutaConsulta("Select codigo from articulos where idc_articulo=4406")
            If dt2.Rows.Count > 0 Then
                codigo = dt2.Rows(0).Item(0)
            Else
                Return
            End If




            'False cuando no se encuentre en la lista de productos, se agrega.
            Dim gweb As New GWebCN.Productos
            Dim ds As New DataSet
            Dim dt As New DataTable
            dt = ViewState("dt")
            Dim row As DataRow
            Dim rowdt As DataRow = dt.NewRow
            Try
                ds = gweb.buscar_productos(codigo, "A", Session("idc_sucursal"), Session("idc_usuario"))
                If ds.Tables(0).Rows.Count > 0 Then
                    'cargar_datos_productob_x_codigo(ds.Tables(0).Rows(0))
                    row = ds.Tables(0).Rows(0)
                    rowdt("idc_articulo") = row("idc_articulo")
                    rowdt("Codigo") = row("codigo")
                    rowdt("Descripcion") = row("desart")
                    rowdt("UM") = row("unimed")
                    rowdt("Decimales") = row("decimales")
                    rowdt("Paquete") = row("paquete")
                    rowdt("precio_libre") = row("precio_libre")
                    rowdt("comercial") = row("comercial")
                    rowdt("fecha") = row("fecha")
                    rowdt("obscotiza") = row("obscotiza")
                    rowdt("vende_exis") = row("vende_exis")
                    rowdt("minimo_venta") = row("minimo_venta")
                    rowdt("calculado") = True
                    rowdt("porcentaje") = calculado(row("idc_articulo"))
                    rowdt("Anticipo") = row("Anticipo")
                    'rowdt("Precio") = Redondeo_cuatro_decimales(txtmaniobras.Text.Trim)
                    'rowdt("Importe") = Redondeo_Dos_Decimales(txtmaniobras.Text.Trim)
                    'rowdt("PrecioReal") = Redondeo_cuatro_decimales(txtmaniobras.Text.Trim)
                    rowdt("nota_credito") = False
                    'rowdt("descuento") = Redondeo_cuatro_decimales("0.00")
                    rowdt("Cantidad") = 1
                    dt.Rows.InsertAt(rowdt, dt.Rows.Count)
                    ViewState("dt") = dt
                    Productos_Calculados()
                    Calcular_Valores_DataTable()
                    cargar_subtotal_iva_total(Session("NuevoIva"))
                    carga_productos_seleccionadas()
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub


    'Sub AgregarJS()
    '    RDRC.Attributes("OnClick") = "return RecogeCliente();"
    '    rdSF.Attributes("OnClick") = "return PedidoEspecial();"
    'End Sub
    'Sub removeJS()
    '    RDRC.Attributes.Remove("OnClick")
    '    rdSF.Attributes.Remove("OnClick")
    'End Sub

    'Protected Sub btnDDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDDF.Click
    '    'Datos_Direccion_Fiscal
    '    If txtid.Text <> "" Then
    '        If cboproyectos.Items.Count > 0 Then
    '            cboproyectos.SelectedIndex = 0
    '        End If
    '        Dim ds As New DataSet
    '        Dim row As DataRow
    '        Dim gweb As New GWebCN.Croquis_Direcciones
    '        Try
    '            ds = gweb.Datos_Direccion_Fiscal(txtid.Text.Trim())
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                row = ds.Tables(0).Rows(0)
    '                txtidc_colonia.Text = row("idc_colonia").ToString.Trim
    '                txtmunicipio.Text = row("mpio").ToString.Trim
    '                txtestado.Text = row("estado").ToString.Trim
    '                txtpais.Text = row("pais").ToString.Trim
    '                chkton.Checked = row("capacidad_maxima").ToString.Trim
    '                txttoneladas.Text = row("toneladas").ToString.Trim
    '                txtCP.Text = row("cod_postal").ToString.Trim
    '                txtcolonia.Text = row("colonia").ToString.Trim
    '                lblrestriccion.Text = row("restriccion").ToString.Trim
    '                txtcalle.Text = row("calle").ToString.Trim
    '                txtnumero.Text = row("numero").ToString.Trim
    '                txtzm.Text = row("zm").ToString.Trim
    '                formar_cadenas()
    '                imgubicacion.Visible = True
    '                imgubicacion.Attributes("onclick") = "return Ubicacion_Cliente('" & txtnombre.Text.Trim & "','" & txtcalle.Text.Trim & "  " & txtnumero.Text.Trim & "," & txtcolonia.Text & "," & txtmunicipio.Text & "');" ' "," & txtestado.Text & 
    '                'ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrarpagina", "<script>Ubicacion_Cliente('" & txtnombre.Text.Trim & "','" & direccion & "');</script>", False)
    '                validar_cambio_iva_Frontera()
    '                actualizar_precios(txtid.Text, txtidc_colonia.Text.Trim)
    '            End If
    '        Catch ex As Exception
    '            Throw ex
    '        Finally
    '            ds = Nothing
    '            gweb = Nothing
    '        End Try
    '    End If
    'End Sub

    '/
    'Protected Sub btnQC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQC.Click
    '    limpiar_datos_consignado()
    '    validar_cambio_iva_Frontera()
    '    actualizar_precios(txtid.Text.Trim, txtidc_colonia.Text.Trim)
    'End Sub
    Sub limpiar_datos_consignado()
        txtidc_colonia.Text = "0"
        txtmunicipio.Text = Nothing
        txtestado.Text = Nothing
        txtpais.Text = Nothing
        chkton.Checked = False
        txttoneladas.Text = "0.00"
        txtCP.Text = 0
        txtcolonia.Text = Nothing
        lblrestriccion.Text = Nothing
        txtcalle.Text = Nothing
        txtnumero.Text = Nothing
        txtzm.Text = Nothing
        '/imgcroquis.Attributes.Remove("onClick")  'Remove image´s croquis on Tabcontainer.
        txtmaniobras.Text = "0.00"
    End Sub

    Sub txt_codigo()
        txtcodigoarticulo.Enabled = True
        txtcodigoarticulo.Focus()
    End Sub

    Sub estado_rd(ByVal estado As Boolean)
        If estado = True Then
            'Me.rdEA.Attributes.Remove("onclick")
            'Me.rdEP.Attributes.Remove("onclick")
            'RDRC.Attributes.Remove("onclick")
            'rdSF.Attributes.Remove("onclick")
            cboentrega.Enabled = True
            'RDRC.Attributes("OnClick") = "RecogeCliente();"
            'rdSF.Attributes("OnClick") = "PedidoEspecial();"
        Else
            'Me.rdEA.Attributes("OnClick") = "if(this.checked==true){this.checked=true;return false;}else{this.checked=false;return false;}"
            'Me.rdEP.Attributes("OnClick") = "if(this.checked==true){this.checked=true;return false;}else{this.checked=false;return false;}"
            'Me.RDRC.Attributes("OnClick") = "if(this.checked==true){this.checked=true;return false;}else{this.checked=false;return false;}"
            'Me.rdSF.Attributes("OnClick") = "if(this.checked==true){this.checked=true;return false;}else{this.checked=false;return false;}"
            'Me.rdEA.Attributes("OnClick") = "return false;"
            'Me.rdEP.Attributes("OnClick") = "return false;"
            'Me.RDRC.Attributes("OnClick") = "return false;"
            'Me.rdSF.Attributes("OnClick") = "return false;"
            cboentrega.Enabled = False

        End If

    End Sub
    Sub limpiar_pedido_especial()
        txtformaP.Text = ""
        txtplazo.Text = ""
        txtotro.Text = ""
        txtcminima.Text = ""
        txtcontacto.Text = ""
        txttelefono.Text = ""
        txtcorreo.Text = ""
    End Sub





    Sub limpiar_recoge_cliente()
        txtnombrerecoge.Text = ""
        txtpaternor.Text = ""
        txtmaternor.Text = ""
        txtsucursalr.Text = "0"
    End Sub



    Protected Sub btnrc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrc.Click
        'rdEA.ForeColor = Drawing.Color.Black
        'rdEP.ForeColor = Drawing.Color.Black
        'RDRC.ForeColor = Drawing.Color.Blue
        'rdSF.ForeColor = Drawing.Color.Black
        'cboproyectos.Enabled = False
        'TabContainer1.Enabled = False
        'If cboproyectos.Items.Count > 0 Then
        '    cboproyectos.SelectedIndex = 0
        'End If
        'limpiar_datos_consignado()
        'txtidc_colonia.Text = 0
        'txtCP.Text = 0
        'txtmaniobras.Text = "0.00"
        ''actualizar_precios(txtid.Text.Trim, txtidc_colonia.Text.Trim)
    End Sub

    Protected Sub btnsf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsf.Click
        'rdEA.ForeColor = Drawing.Color.Black
        'rdEP.ForeColor = Drawing.Color.Black
        'RDRC.ForeColor = Drawing.Color.Black
        'rdSF.ForeColor = Drawing.Color.Blue
        'cboproyectos.Enabled = False
        'TabContainer1.Enabled = True
        ''cboproyectos.SelectedIndex = -1
        ''limpiar_datos_consignado()
        ''txtidc_colonia.Text = 0
        ''txtCP.Text = 0
        ''txtmaniobras.Text = "0.00"
    End Sub

    Sub formar_cadenas()
        If (cboentrega.SelectedValue = 1 Or cboentrega.SelectedValue = 4) And txtidc_colonia.Text <> "" Then
            Dim dt As New DataTable
            dt = ViewState("dt")
            Dim cadena1 As String = ""
            Dim cadena2 As String = ""
            Dim gweb As New GWebCN.Fletes
            Dim dt_r As DataTable
            Dim row As DataRow
            Dim total As Integer
            For i As Integer = 0 To dt.Rows.Count - 1
                row = dt.Rows(i)
                'se agrego & row("ids_especif") & ";" & row("num_especif") & ";" a la cadena 12-05-2015 MIC
                cadena1 = cadena1 & row("idc_articulo") & ";" & row("Cantidad") & ";" & row("Precio") & ";" & row("PrecioReal") & ";" & row("ids_especif") & ";" & row("num_especif") & ";"
                cadena2 = cadena2 & row("idc_articulo") & ";" & row("Cantidad") & ";"
                total = total + 1
            Next
            Dim desg_iva As Boolean
            If txtrfc.Text.StartsWith("*") = True Then
                desg_iva = False
            Else
                desg_iva = True
            End If
            Try
                dt_r = gweb.Flete2(cadena1, total, 18, Session("pxidc_sucursal"), txtidc_colonia.Text, txtid.Text, desg_iva, Session("NuevoIva"), cadena2)

                If dt_r.Rows.Count > 0 Then
                    If txtrfc.Text.Trim.StartsWith("*") Then
                        txtmaniobras.Text = Redondeo_Dos_Decimales(dt_r.Rows(0).Item("flete") * ((Session("NuevoIva") / 100) + 1))
                    Else
                        txtmaniobras.Text = Redondeo_Dos_Decimales(dt_r.Rows(0).Item("flete"))
                    End If
                    Session("gastooperativo") = dt_r.Rows(0).Item("gastos")
                    '11-05-2015 agergue variable de sesion distanciaentrega
                    Session("distanciaentrega") = dt_r.Rows(0).Item("DISTANCIA")
                Else
                    txtmaniobras.Text = "0.00"
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub

    'Protected Sub imgmaniobras_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgmaniobras.Click
    '    formar_cadenas()
    'End Sub

    Protected Sub btnbuscarcol_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnbuscarcol.Click
        If txtid.Text.Trim <> "" Then
            formar_cadenas()
        End If
    End Sub

    Protected Sub btnbuscarcliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbuscarcliente.Click
        If txtid.Text = "" And txtbuscar.Text <> "" Then
            cargarbusquedaclientes()
        End If
    End Sub

    Protected Sub btnbuscarart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbuscarart.Click
        If txtcodigoarticulo.Text.Trim <> "" Then
            txtcodigoarticulo_TextChanged(Nothing, EventArgs.Empty)
        End If
    End Sub

    Protected Sub btnquitarll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnquitarll.Click
        fullamada.Attributes("style") = "display:none" 'Muestra FileUpload.
        imgupllamada.Attributes("style") = "display:"
        With lblllamada                               '
            .Attributes("style") = "Display:none"   'Oculta Label con el nombre de audio.
            .Text = ""                              '     
        End With
        btnquitarll.Attributes("style") = "Display:none" 'Oculta el botón "Quitar".
        btnescucharll.Attributes("style") = "Display:none"
    End Sub

    Protected Sub btnredirecting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnredirecting.Click
        Response.Redirect("ficha_cliente_m.aspx")
    End Sub

    Public Function validar_colonia() As Boolean
        Dim gweb As New GWebCN.Colonias
        Dim valida As String = gweb.validar_colonias(txtidc_colonia.Text)
        If valida <> "" Then
            WriteMsgBox("Colonia No Permitida.")
            Return False
        Else
            Return True
        End If
    End Function
    Sub WriteMsgBox(ByVal mensaje As String)
        ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid().ToString(), "<script>alert(' " & mensaje.Replace("'", "") & " ');</script>", False)
    End Sub

    Sub WriteMsgBox2(ByVal mensaje As String)
        Alert.ShowAlertError(mensaje.Replace("'", ""), Me.Page)
    End Sub

    Protected Sub btncalc_iva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncalc_iva.Click
        validar_cambio_iva_Frontera()
        actualizar_precios(txtid.Text.Trim, txtidc_colonia.Text.Trim)
        formar_cadenas()
    End Sub

    Sub actualizar_precios(ByVal idc_cliente As Integer, ByVal idc_colonia As Integer)
        'txtid
        'txtidc_colonia
        Dim vidc_listap As Integer = 0
        Dim vetiqueta As Boolean = False
        Dim vmotivo As String = ""
        Dim vccambio As Boolean = False
        Dim voriginal As Boolean = False
        Dim iva_rec_suc As Integer = CInt(txtsucursalr.Text)
        Dim vidc_sucursal As Integer = 0
        If txtidc_colonia.Text = "" Or txtidc_colonia.Text = 0 Then

            vidc_sucursal = Session("idc_sucursal")

            If Session("pxidc_sucursal") <> vidc_sucursal Then
                vmotivo = "(Regresa a su Lista de Precios.)"
            End If



            If txtsucursalr.Text = 0 Then
                Session("pxidc_sucursal") = Session("idc_sucursal")
                voriginal = True
            Else
                Session("pxidc_sucursal") = txtsucursalr.Text.Trim
            End If




            vidc_sucursal = Session("pxidc_sucursal")

            Dim cedis_sucursal As Integer = cedis_prg(vidc_sucursal)
            If cedis_sucursal <> Session("cedisprecios") And CInt(txtsucursalr.Text) > 0 Then
                vetiqueta = True
                vmotivo = "(La Sucursal de Entrega no corresponde a la Lista de Precios del Cliente.)"
            End If



            'Hacer Cambio
            '         If cedis_sucursal(vidc_sucursal, vsd) <> thisformset.cedisprecios And thisformset.pidc_sucursal_recoge > 0 Then
            'vetiqueta=.t.
            '             vmotivo = "(La Sucursal de Entrega no corresponde a la Lista de Precios del Cliente.)"
            '         Else

            '         End If





            vccambio = True


        Else
            Dim pxsuc As Integer = 0
            If CInt(txtlistap.Text) > 0 Then
                pxsuc = m_pxsuc("select dbo.fn_corresponde_colonia_cliente_sin_publico(" & txtid.Text & "," & txtidc_colonia.Text.Trim & ") as pxsuc")
            Else
                pxsuc = m_pxsuc("select dbo.fn_corresponde_colonia_cliente_publico(" & Session("idc_sucursal") & "," & txtidc_colonia.Text.Trim & ") as pxsuc")
            End If
            vidc_sucursal = pxsuc

            If vidc_sucursal > 0 Then
                Session("pxidc_sucursal") = vidc_sucursal
                vccambio = True
            Else
                If lblroja.Visible = True Then
                    Session("pxidc_sucursal") = Session("idc_sucursal")
                    vccambio = True
                Else
                    Session("pxidc_sucursal") = suc_cercana()
                End If
            End If
            If vidc_sucursal > 0 Then
                vetiqueta = True
                vmotivo = "(La Sucursal de Entrega no corresponde a la Lista de Precios del Cliente.)"
            Else
                vmotivo = "(Regresa a su Lista de Precios.)"
            End If

        End If
        Dim varticulos As String = ""
        Dim vnum As Integer = 0
        Dim dt As New DataTable
        dt = ViewState("dt")
        If dt.Rows.Count > 0 Then

            For i As Integer = 0 To dt.Rows.Count - 1
                vnum = vnum + 1
                varticulos = varticulos & dt.Rows(i).Item("idc_articulo") & ";"
            Next
        End If

        If vccambio = False Then
            Return
        End If

        Dim dt_precios As New DataTable
        If vidc_sucursal > 0 Then

            If voriginal = False Then
                dt_precios = c_precios_art("select * from dbo.fn_precios_articulos_LISTA('" & varticulos & "'," & vnum & "," & vidc_sucursal & "," & txtid.Text.Trim & ")")
            Else
                dt_precios = c_precios_art("select * from dbo.fn_precios_articulos_LISTA_lp_suc('" & varticulos & "'," & vnum & "," & txtlistap.Text.Trim & "," & txtid.Text.Trim & "," & Session("idc_sucursal") & ")")
            End If

        Else
            vidc_listap = txtlistap.Text
            If vidc_listap = 0 Then
                dt_precios = c_precios_art("select * from dbo.fn_precios_articulos_LISTA_lp_suc('" & varticulos & "'," & vnum & "," & vidc_listap & "," & txtid.Text.Trim & "," & Session("idc_sucursal") & ")")


            Else
                dt_precios = c_precios_art("select * from dbo.fn_precios_articulos_LISTA_lp('" & varticulos & "'," & vnum & "," & vidc_listap & "," & txtid.Text.Trim & ")")

            End If
        End If

        If dt_precios.Rows.Count > 0 Then
            Dim piva As Integer = Session("NuevoIva")
            If txtrfc.Text.StartsWith("*") Then
                For i As Integer = 0 To dt_precios.Rows.Count - 1
                    dt_precios.Rows(i).Item("precio") = Math.Round(dt_precios.Rows(i).Item("precio") * (1 + piva / 100), 4)
                    dt_precios.Rows(i).Item("precio_real") = Math.Round(dt_precios.Rows(i).Item("precio_real") * (1 + piva / 100), 4)
                Next
            End If
        End If

        Dim actualizar As Boolean = False
        Dim rows() As DataRow
        If dt_precios.Rows.Count > 0 Then
            For i As Integer = 0 To dt_precios.Rows.Count - 1
                rows = dt.Select("idc_articulo=" & dt_precios(i).Item("idc_articulo"))
                If rows.Length > 0 Then
                    If rows(0).Item("precio") <> dt_precios.Rows(i).Item("precio") Then
                        WriteMsgBox("Se Actualizarán los Precios... \n-\n " & vmotivo)
                        actualizar = True
                        rows = Nothing
                        Exit For
                    End If
                End If
                rows = Nothing
            Next
        End If

        If actualizar = True Then
            For i As Integer = 0 To dt_precios.Rows.Count - 1

                For ii As Integer = 0 To dt.Rows.Count - 1

                    If dt.Rows(ii).Item("idc_articulo") = dt_precios.Rows(i).Item("idc_articulo") Then

                        If dt.Rows(ii).Item("precio") <> dt_precios.Rows(i).Item("precio") Then
                            dt.Rows(ii).Item("precio") = dt_precios.Rows(i).Item("precio")
                            dt.Rows(ii).Item("precioreal") = dt_precios.Rows(i).Item("precio_real")
                            If dt.Rows(ii).Item("precioreal") = dt.Rows(ii).Item("precio") Then
                                dt.Rows(ii).Item("Nota_Credito") = False
                            Else
                                dt.Rows(ii).Item("Nota_Credito") = True
                            End If
                        End If

                        Exit For
                    End If

                Next

            Next
        End If
        ViewState("dt") = dt
        lblroja.Visible = vetiqueta
        Calcular_Valores_DataTable()
        Productos_Calculados()
        Ajustes_iva(Session("NuevoIva"), Session("ivaant"))
        cargar_subtotal_iva_total(Session("NuevoIva"))
    End Sub

    Public Function suc_cercana() As Integer
        Dim gweb As New GWebCN.Sucursales
        Dim sucent As Integer
        Try
            sucent = gweb.suc_cercana(txtidc_colonia.Text)
            Return sucent
        Catch ex As Exception
            WriteMsgBox("No se ha procedido a Verificar Sucursal mas Cercana \n \u000B \n" & ex.Message & "\n \u000B \n")
        End Try
    End Function

    Public Function c_precios_art(ByVal consulta As String) As DataTable
        Dim dt As New DataTable
        Try
            dt = GWebCD.clsConexion.EjecutaConsulta(consulta)
            Return dt
        Catch ex As Exception
            WriteMsgBox("No se Procedio a Consultar Precios. \n-\n" & ex.Message)
            Return dt
        End Try
    End Function


    Public Function m_pxsuc(ByVal consulta As String) As Integer
        Dim dt As New DataTable
        Try
            dt = GWebCD.clsConexion.EjecutaConsulta(consulta)
            If dt.Rows.Count > 0 Then
                Return CInt(dt.Rows(0).Item(0))
            Else
                Return 0
            End If
        Catch ex As Exception
            WriteMsgBox("No se Procedio a Validar la Colonia. \n-\n" & ex.Message)
        End Try
    End Function


    Sub lista_p(ByVal idc_cliente As Integer)
        Dim dt As New DataTable
        Try
            dt = GWebCD.clsConexion.EjecutaConsulta("select idc_listap from clientes_clasif where idc_cliente =" & idc_cliente)
            If dt.Rows.Count > 0 Then
                txtlistap.Text = dt.Rows(0).Item(0)
            End If
        Catch ex As Exception
            txtlistap.Text = 0
            Exit Sub
        End Try
    End Sub

    Public Function consulta(ByVal str As String) As DataTable
        Dim dt As New DataTable
        Try
            dt = GWebCD.clsConexion.EjecutaConsulta(str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function cedis() As Integer
        Dim gweb As New GWebCN.Sucursales
        Dim vidc_cedis As Integer = 0

        Try
            vidc_cedis = gweb.succedis(Session("idc_sucursal"))
            Session("pidc_cedis") = vidc_cedis
            Session("lidc_cedis") = vidc_cedis
            Session("sucursalprecios") = Session("idc_sucursal")
            Session("cedisprecios") = vidc_cedis
        Catch ex As Exception
            WriteMsgBox("Error al Cargar ID Cedis. \n \u000B \n" & ex.Message)
        End Try
    End Function

    Sub ivasucursal()
        Dim ds As New DataSet
        Dim gweb As New GWebCN.Sucursales

        Try
            ds = gweb.datos_sucursal(Session("idc_sucursal"))
            If ds.Tables(0).Rows.Count > 0 Then
                Session("lidc_iva") = ds.Tables(0).Rows(0).Item("idc_iva")
                Session("liva") = ds.Tables(0).Rows(0).Item("iva")


                Session("pidc_iva") = ds.Tables(0).Rows(0).Item("idc_iva")
                Session("piva") = ds.Tables(0).Rows(0).Item("iva")

            End If
        Catch ex As Exception
            Throw ex
        End Try

        'Select c_temiva
        'GO top
        'vidc_iva = idc_iva
        'thisformset.pidc_iva=vidc_iva 
        'thisformset.piva = iva

        'thisformset.lidc_iva=vidc_iva 
        'thisformset.liva = iva

        'thisformset.pxidc_sucursal = xidc_sucursal
    End Sub

    Public Function cedis_prg(ByVal idc_sucursal As Integer) As Integer
        Dim gweb As New GWebCN.Sucursales
        Dim pxid As Integer = 0
        Try
            pxid = gweb.cedis_suc_prg(idc_sucursal)
            Return pxid
        Catch ex As Exception
            WriteMsgBox("Error al Tratar de Buscar la Informacion de Cedis de Sucursal. \n \u000B \n" & ex.Message)
            Return pxid
        End Try
    End Function

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncaptArt.Click
        Try
            If txtid.Text.Trim = "" Then
                WriteMsgBox("Es Necesario Seleccionar el Cliente.")
                Return
            End If

            If cboentrega.SelectedValue = 1 Then
                If validar_datos_dir() = False Then
                    WriteMsgBox("Faltan de Completar Datos en el Consignado...Es Obligatorio.")
                    'ScriptManager.RegisterStartupScript(Me, GetType(Page), "JS_con", "<script>popup_consignado();</script>", False)
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "js_clear", "<script>myStopFunction();</script>", False)
                    txtcodigoarticulo.Text = ""
                    Return
                End If
                'ElseIf rdEP.Checked = True Then
                '    If validar_datos_dir() = False Then
                '        WriteMsgBox("Faltan de Completar Datos en el Consignado...Es Obligatorio.")
                '        txtcodigoarticulo.Text = ""
                '        ScriptManager.RegisterStartupScript(Me, GetType(Page), "JS_con", "<script>popup_consignado();</script>", False)
                '        Return
                '    End If
            ElseIf cboentrega.SelectedValue = 3 Then
                If txtsucursalr.Text = "0" Or txtsucursalr.Text = "" Then
                    WriteMsgBox("Falta Completar Datos de Donde va Recoger el Cliente.")
                    txtcodigoarticulo.Text = ""
                    'ScriptManager.RegisterStartupScript(Me, GetType(Page), "JS_con", "<script>RecogeCliente();</script>", False)
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "js_clear", "<script>myStopFunction();</script>", False)
                    Return
                End If
            ElseIf cboentrega.SelectedValue = 4 Then
                If validar_pedido_especial() = False Then
                    WriteMsgBox("Falta Completar Datos del Pre-Pedido Especial.")
                    txtcodigoarticulo.Text = ""
                    'ScriptManager.RegisterStartupScript(Me, GetType(Page), "JS_con", "<script>PedidoEspecial();</script>", False)
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "js_clear", "<script>myStopFunction();</script>", False)
                    Return
                End If
            End If

            'ScriptManager.RegisterStartupScript(Me, GetType(Page), "JS_con", "<script>mostrar_procesar();</script>", False)


            Dim tipo As Integer
            tipo = Request.QueryString("tipo")

            If tipo = 1 Then

                txtid.Text = Session("idc_cliente")
                ViewState("dt") = proces_ped_lista(Session("dt_pedido_lista"))
                Session("dt_productos_lista") = ViewState("dt")
                Productos_Calculados()
                Calcular_Valores_DataTable()
                carga_productos_seleccionadas()
                cargar_subtotal_iva_total(Session("NuevoIva"))
                limpiar_campos()
                Estado_controles_captura(False)
                buscar_confirmacion_lista()
                formar_cadenas()
                tbnguardarPP.Enabled = True
                btnnuevoprepedido.Enabled = True
                etiqueta_cheque()
                txtbuscar.Focus()
                botones_pedido()
                carga_productos_seleccionadas()
                Calcular_Valores_DataTable()
                cargar_subtotal_iva_total(Session("NuevoIva"))
                'estado_rd(True)
                'estado_rd(False)

            End If

            estado_rd(False)
            txt_codigo()
            btncaptArt.Enabled = False
            txt_consignado.Text = 1
            controles_busqueda_prod(False)
            carga_productos_seleccionadas()
            controles_busqueda_master(True)
            prep_cargar_grid_prod_master_cliente(txtid.Text.Trim)
            validar_cambio_iva_Frontera()
            actualizar_precios(txtid.Text, txtidc_colonia.Text.Trim)




            Dim datos_clientes_pedidos() As Object = {txtid.Text.Trim, txtrfc.Text.Trim, txtlistap.Text.Trim, lblroja.Visible, IIf(txtidc_colonia.Text.Trim <> "", txtidc_colonia.Text, "0")}
            Session("datos_clientes_pedidos") = datos_clientes_pedidos
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "js_clear", "<script>myStopFunction();</script>", False)
            labeliva.Visible = True
            labeliva.Text = "IVA: " & CInt(Session("NuevoIva")) & "%"

            Session("tipo_de_entrega") = cboentrega.SelectedValue
        Catch ex As Exception
            WriteMsgBox("Error \n \u000b \n" & ex.Message)
        End Try

    End Sub

    Public Function proces_ped_lista(ByVal dt As DataTable) As DataTable
        Dim row As DataRow
        If Not dt Is Nothing Then
            For i As Integer = 0 To dt.Rows.Count - 1
                row = dt.Rows(i)
                buscar_precio_producto_lista(row)
                'dt.Rows.Add(row)
                'dt.Rows.RemoveAt(i)
            Next
            Return dt
        Else

            Return Nothing
        End If
    End Function

    Sub buscar_precio_producto_lista(ByRef rowprincipal As DataRow)
        'If txtidc_colonia.Text = "0" Or txtidc_colonia.Text.Trim = "" Then
        '    Session("pxidc_sucursal") = Session("idc_sucursal")
        'End If
        'Dim row As DataRow

        'rowprincipal = Session("rowprincipal")
        rowprincipal("nota_credito") = False

        Dim vidc As Integer = rowprincipal("idc_articulo")
        Dim vidcli As Integer = txtid.Text.Trim
        Dim vidc_clonia As Integer = txtidc_colonia.Text.Trim
        Dim dt, dt1, dt2, dt3 As New DataTable

        Dim vprecio As Decimal = 0

        Dim vidc_listap As Integer = txtlistap.Text.Trim
        Dim zidc_sucursal As Integer = Session("pxidc_sucursal")
        Dim gweb1 As New GWebCN.Productos 'Cambios el 15/02/2013
        Dim ds1 As New DataSet 'Cambios el 15/02/2013


        'Cambios



        '///





        Try
            'dt = consulta("exec sp_precio_cliente_cedis @pidc_articulo = " & vidc & ",@pidc_cliente=" & vidcli & ",@pidc_sucursal=" & Session("idc_sucursal")) 'Cambios el 15/02/2013
            'ds1 = gweb1.buscar_precio_producto_nuevo(vidc, vidcli, Session("idc_sucursal"), rowprincipal("cantidad"), 1) 'Cambios el 15/02/2013
            '11-05-2015
            'cambie por sp nuevo
            ds1 = gweb1.buscar_precio_producto_nuevo1(vidc, vidcli, Session("idc_sucursal"), rowprincipal("cantidad"), 1, rowprincipal("ids_especif"), rowprincipal("num_especif")) 'MIC 12-05-2015
            dt = ds1.Tables(0)
        Catch ex As Exception
            WriteMsgBox("Error al Cargar Precio del Producto.  \n \u000b \n Error: \n" & ex.Message)
            Return
        End Try


        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("precio") <= 0 Then
                'Limpiar campos del articulo
                WriteMsgBox("No se Encontro el Precio de Producto. \n")
                Return
            End If
        Else
            'Limpiar campos del articulo
            WriteMsgBox("No se Encontro el Precio de Producto. \n")
            Return
        End If
        rowprincipal("precio") = dt.Rows(0).Item("precio") 'Nuevo


        If lblroja.Visible = True Then
            Try
                If zidc_sucursal > 0 Then
                    dt1 = consulta("select dbo.fn_ver_precio_cliente_esp_cambio_lista(" & vidc & "," & vidcli & "," & zidc_sucursal & ") as pxprecio")
                Else
                    dt1 = consulta("select dbo.fn_ver_precio_cliente_esp_lp_SUC(" & vidc & "," & vidcli & "," & vidc_listap & "," & Session("idc_sucursal") & ") as pxprecio")
                End If
                vprecio = dt1.Rows(0).Item("pxprecio")
            Catch ex As Exception
                WriteMsgBox("No Se Procedio a Verificar Precios. \n- \n" & ex.Message)
            End Try

            Try


                dt2 = consulta("select dbo.fn_ver_precio_real_cliente_esp_cambio_lista(" & vidc & "," & vidcli & "," & zidc_sucursal & ") as pxprecior")
                rowprincipal("PrecioReal") = dt2.Rows(0).Item("pxprecior")
                Session("pprecio_real") = dt2.Rows(0).Item("pxprecior")
                rowprincipal("descuento") = Math.Round(vprecio - rowprincipal("PrecioReal"), 4)

                If rowprincipal("descuento") > 0 Then
                    rowprincipal("nota_credito") = True
                Else
                    rowprincipal("nota_credito") = False
                End If


            Catch ex As Exception
                WriteMsgBox("No Se procedio a Verificar Precios. \n - \n" & ex.Message)
            End Try
        End If




        rowprincipal("Costo") = dt.Rows(0).Item("costo")
        Dim viva As Integer = Session("NuevoIva")
        vprecio = CDec(rowprincipal("precio").ToString.Trim)
        If txtrfc.Text.Trim.StartsWith("*") Then '1.-Si rfc_cliente inicia con *  y 2.- En Caso Contrario
            rowprincipal("precio") = FormatNumber(vprecio * (1 + (viva / 100)), 4)
            rowprincipal("precioreal") = rowprincipal("precio")
        Else
            rowprincipal("precio") = FormatNumber(vprecio, 4)
            rowprincipal("precioreal") = rowprincipal("precio")
        End If


        ''txtprecio.Text = FormatNumber(rowprincipal("precio"), 4)
        ''txtprecioreal.Text = FormatNumber(rowprincipal("precio"), 4)

        Try
            Dim gweb As New GWebCN.Productos
            Dim ds As New DataSet
            Dim row As DataRow
            Dim vprecio_real As Decimal = 0
            ds = gweb.Nota_Credito_Automatica(txtid.Text.Trim, vidc)
            If ds.Tables(0).Rows.Count > 0 Then
                row = ds.Tables(0).Rows(0)


                'If lblroja.Visible = False Then
                '    vprecio_real = row("precio_real")
                'Else
                '    vprecio_real = Session("pprecio_real")
                'End If



                rowprincipal("Costo") = row.Item(8)
                rowprincipal("descuento") = row("descuento")
                rowprincipal("nota_credito") = True
                If txtrfc.Text.StartsWith("*") Then
                    vprecio_real = Math.Round(vprecio_real * (1 + (Session("nuevoiva") / 100)), 4)
                    vprecio = Math.Round(row("precio") * (1 + (Session("nuevoiva") / 100)), 4)
                Else
                    vprecio = Math.Round(row("precio"), 4)
                End If
                rowprincipal("Precio") = vprecio
                rowprincipal("PrecioReal") = FormatNumber(CDec(rowprincipal("precio")), 4)
                txtprecio.Enabled = False
                ''txtprecioreal.Enabled = False
                txtprecio.Text = vprecio
                ''txtprecioreal.Text = vprecio
            Else
                rowprincipal("nota_credito") = False
                rowprincipal("descuento") = 0
            End If

        Catch ex As Exception
            WriteMsgBox("No se procedio a buscar Nota de Credito Automatica de este Articulo. \n \u000B \n" & ex.Message)
            'btncancelar_Click(Nothing, EventArgs.Empty)
            Session("rowprincipal") = Nothing
        End Try
        Session("rowprincipal") = rowprincipal

    End Sub

    Public Function validar_datos_dir() As Boolean
        If txtcalle.Text.Trim = "" And (txtproy.Text = "" Or txtproy.Text <= 0) Then
            Return False
        ElseIf txtnumero.Text.Trim = "" And (txtproy.Text = "" Or txtproy.Text <= 0) Then
            Return False
        ElseIf txtidc_colonia.Text.Trim = "" And (txtproy.Text = "" Or txtproy.Text <= 0) Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function validar_pedido_especial() As Boolean
        If txtplazo.Text = "" Then
            Return False
        ElseIf txtformaP.Text = "" Then
            Return False
        ElseIf txtotro.Text = "" Then
            Return False
        ElseIf txtcminima.Text = "" Then
            Return False
        ElseIf txtcontacto.Text = "" Then
            Return False
        ElseIf txttelefono.Text = "" Then
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub btncan_bus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncan_bus.Click
        'btncan_bus.Visible = False
        'txtbuscar.Visible = True
        'btnacep_bus.Visible = False
        'btnbuscarcliente.Visible = True
        'cboclientes.Items.Clear()
        'cboclientes.Visible = False
        controles_busqueda_cliente(True)
        controles_busqueda_cliente_cancel_selecc(False)
        cboclientes.Items.Clear()
        cboclientes.Visible = True
    End Sub

    Sub controles_busqueda_cliente_cancel_selecc(ByVal estado As Boolean)
        btncan_bus.Visible = estado
        btnacep_bus.Visible = estado
        cboclientes.Visible = estado
    End Sub
    Sub controles_busqueda_cliente(ByVal estado As Boolean)
        txtbuscar.Visible = estado
        btnbuscarcliente.Visible = estado
    End Sub

    Protected Sub btnacep_bus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnacep_bus.Click
        If cboclientes.Items.Count <= 0 Then
            Return
        End If

        Dim dt As New DataTable
        Dim rows() As DataRow
        Try
            dt = ViewState("dt_clientes")
            rows = dt.Select("idc_cliente=" & cboclientes.SelectedValue)
            If rows.Length > 0 Then
                txtrfc.Text = rows(0)("rfccliente")
                txtnombre.Text = rows(0)("nombre")
                txtid.Text = rows(0)("idc_cliente")
                txtstatus.Text = rows(0)("idc_bloqueoc")
                colores(txtstatus.Text)
                controles_busqueda_cliente(True)
                controles_busqueda_cliente_cancel_selecc(False)
                Dim index As Integer
                index = 1

                'cargar_credito_disponible(txtid.Text)//// Motivo de Comentarizar: Tarda +6 segundos en cargar datos Cliente.

                Session("Clave_Adi") = rows(0).Item("cveadi")
                'Session("cad_prod") = gridbuscar_clientes.Items(index).Cells(6).Text
                'Session("credito") = gridbuscar_clientes.Items(index).Cells(7).Text
                colores(txtstatus.Text)
                'txtbuscar.Enabled = False
                'Estado_controles_captura(True)
                'txtcodigoarticulo.Enabled = True
                lblconfirmacion.Visible = Confirmacion_de_Pago()
                btnconfirmar.Visible = lblconfirmacion.Visible
                btnOC.Attributes.Add("onclick", "window.open('OC_Digitales_Pendientes.aspx?idc_cliente=" & txtid.Text & "');return false;")
                btnOC.Enabled = True
                lkverdatoscliente.Attributes("onclick") = "window.open('Ficha_cliente_m.aspx?idc_cliente=" & txtid.Text.Trim & "&b=1'); return false;"
                lkverdatoscliente.Enabled = True
                'txtcodigoarticulo.Focus()
                etiqueta_Iva(Session("NuevoIva"))
                requiere_oc_croquis()
                '/cargar_proyectos_cliente(txtid.Text.Trim)
                btnnuevoprepedido.Enabled = True
                tbnguardarPP.Enabled = True
                tipo_croquis_cliente()
                'Para la Lista de Precios cliente
                lista_p(txtid.Text.Trim)
                Session("cedisprecios") = rows(0).Item("idc_cedis")
                'AgregarJS()
                estado_rd(True)
                'RDRC.Attributes("onclick") = "RecogeCliente();"
                'rdSF.Attributes("onclick") = "PedidoEspecial();"
                'cboentrega.Attributes("onchange") = "return tipo_entrega(this);"

            Else
                WriteMsgBox("Error al Cargar Informacion del Cliente Seleccionado.")
            End If
        Catch ex As Exception
            WriteMsgBox("Error al Cargar Informacion del Cliente Seleccionado. \n \u000B \n" & ex.Message)
        End Try
    End Sub


    'Codigo ImgAceptar
    'Protected Sub imgaceptar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgaceptar.Click
    '    'controles_busqueda_prod_sel_cancel(False)
    '    'controles_busqueda_prod(True)

    '    Dim dt_productos, dt As New DataTable
    '    Dim rows() As DataRow
    '    dt_productos = ViewState("dt_productos")
    '    dt = ViewState("dt")

    '    rows = dt.Select("idc_articulo=" & cboproductos.SelectedValue)

    '    If rows.Length > 0 Then
    '        WriteMsgBox("El Articulo Seleccionado Ya Se Encuentra En La Lista.")
    '        Return
    '    End If

    '    rows = dt_productos.Select("idc_articulo=" & cboproductos.SelectedValue)

    '    If rows.Length > 0 Then
    '        'Dim tspan As New TimeSpan
    '        'Dim inicio, final As DateTime
    '        'inicio = Now()
    '        '/////
    '        Dim rowprincipal As DataRow


    '        '/*/*/////Add columns to the DataRow/////*/*'
    '        rowprincipal = dt.NewRow()
    '        Session("rowprincipal") = rowprincipal
    '        Dim i As Integer
    '        i = 0
    '        rowprincipal("idc_articulo") = rows(0).Item("idc_articulo")
    '        rowprincipal("Codigo") = rows(0).Item("codigo")
    '        rowprincipal("Descripcion") = rows(0).Item("desart")
    '        rowprincipal("UM") = rows(0).Item("unimed")
    '        rowprincipal("Decimales") = rows(0).Item("decimales")
    '        Decimales(CBool(rowprincipal("Decimales")))
    '        rowprincipal("Paquete") = rows(0).Item("paquete")
    '        rowprincipal("precio_libre") = rows(0).Item("precio_libre")
    '        rowprincipal("comercial") = rows(0).Item("comercial")
    '        rowprincipal("vende_exis") = rows(0).Item("vende_exis")
    '        rowprincipal("minimo_venta") = rows(0).Item("minimo_venta")
    '        rowprincipal("Master") = rows(0).Item("master")
    '        rowprincipal("EXISTENCIA") = buscar_Existencia_Articulo(rowprincipal("idc_articulo"))
    '        rowprincipal("fecha") = rows(0).Item("fecha")
    '        rowprincipal("obscotiza") = rows(0).Item("obscotiza")
    '        rowprincipal("mensaje") = rows(0).Item("mensaje")


    '        If calculado(rowprincipal("idc_articulo")) = Nothing Then
    '            rowprincipal("calculado") = False
    '            rowprincipal("porcentaje") = 0.0
    '            '/txtdescripcion.Text = rows(0).Item("desart")
    '            '/txtcodigoarticulo.Text = rows(0).Item("codigo")
    '            '/txtUM.Text = rows(0).Item("unimed")
    '            buscar_precio_producto(rows(0).Item("idc_articulo"))
    '            '///////ver_observaciones_articulo(rowprincipal)
    '            '/txtcantidad.Focus()
    '            'controles_busqueda_prod(True)
    '            'controles_busqueda_prod_sel_cancel(False)
    '            txtcodigoarticulo.Enabled = False
    '            Estado_controles_captura(True)
    '            If rowprincipal("nota_credito") = True Then
    '                txtprecio.Enabled = False
    '            End If
    '            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alta_prod", "<script>editar_precios_cantidad();</script>", False)
    '            'imgaceptar.Attributes("onclick") = "return false;"
    '            'imgcancelar.Attributes("onclick") = "return false;"
    '        Else
    '            If buscar_articulos_duplicados(rowprincipal("idc_articulo")) = False Then
    '                rowprincipal("precio") = Redondeo_cuatro_decimales(0.0)
    '                rowprincipal("cantidad") = 1
    '                rowprincipal("precioreal") = Redondeo_cuatro_decimales(rowprincipal("precio"))
    '                rowprincipal("calculado") = True
    '                rowprincipal("porcentaje") = calculado(rowprincipal("idc_articulo"))
    '                rowprincipal("nota_credito") = False
    '                dt.Rows.Add(rowprincipal)
    '                limpiar_campos()
    '                rowprincipal = Nothing
    '                Estado_controles_captura(False)
    '                controles_busqueda_prod(True)
    '                controles_busqueda_prod_sel_cancel(False)
    '                txtcodigoarticulo.Enabled = True
    '                Productos_Calculados()
    '                Calcular_Valores_DataTable()
    '                cargar_subtotal_iva_total(Session("NuevoIva"))
    '                carga_productos_seleccionadas()
    '                imgaceptar.Attributes("onclick") = "return false;"
    '            Else
    '                imgaceptar.Attributes("onclick") = "return false;"
    '                limpiar_campos()
    '                rowprincipal = Nothing
    '                Estado_controles_captura(False)
    '                'controles_busqueda_prod(True)
    '                'controles_busqueda_prod_sel_cancel(False)
    '                txtcodigoarticulo.Enabled = True
    '            End If
    '        End If
    '        'final = Now()
    '        'tspan = final.Subtract(inicio).Duration()
    '        'CargarMsgbox("", tspan.Duration.ToString(), False, 2)

    '    Else
    '        WriteMsgBox("Error al Cargar de Informacion del Articulo Seleccionado.")
    '    End If
    'End Sub

    'Sub controles_busqueda_prod(ByVal estado As Boolean)
    '    txtcodigoarticulo.Visible = estado
    'End Sub

    Sub controles_busqueda_prod_sel_cancel(ByVal estado As Boolean)
        cboproductos.Visible = estado
    End Sub

    Protected Sub Button5_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnref.Click
        Try
            Dim row As DataRow = Session("rowprincipal")
            Dim dt_row As DataRow
            Dim dt As DataTable
            dt = ViewState("dt")
            dt_row = dt.NewRow
            For i As Integer = 0 To row.Table.Columns.Count - 1
                dt_row(i) = row(i)
            Next
            dt.Rows.Add(dt_row)
            ViewState("dt") = dt
            Session("dt_productos_lista") = dt
            promociones_cliente(1, 0)
            Productos_Calculados()
            Calcular_Valores_DataTable()
            carga_productos_seleccionadas()
            cargar_subtotal_iva_total(Session("NuevoIva"))
            limpiar_campos()
            Estado_controles_captura(False)
            buscar_confirmacion_lista()
            formar_cadenas()
            tbnguardarPP.Enabled = True
            btnnuevoprepedido.Enabled = True
            Session("rowprincipal") = Nothing
            'imgcancelar.Attributes.Remove("onclick")
            'imgaceptar.Attributes.Remove("onclick")
            If Not cbomaster.Visible = True Then
                txtcodigoarticulo.Attributes.Remove("onfocus")
                txtcodigoarticulo.Text = ""
                txtcodigoarticulo.Focus()
                controles_busqueda_prod(True)
                controles_busqueda_prod_sel_cancel(False)
                cboproductos.Visible = True
                cboproductos.Items.Clear()
            End If
            aportaciones()
        Catch ex As Exception
            WriteMsgBox("Error al Refrescar Lista de Articulos. \n \u000B \n Error: \n" & ex.Message)
        Finally
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_loading", "<script>myStopFunction_grid();</script>", False)
        End Try
    End Sub

    Protected Sub btneditar_art_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btneditar_art.Click
        'Session("dt") = ViewState("dt")
        'ScriptManager.RegisterStartupScript(Me, GetType(Page), "editar_art", "<script>editar_precios_cantidad_1('" & txtidc_articulo.Text.Trim & "');</script>", False)
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "editar_articulo", "editar_precios_cantidad_1(" & txtidc_articulo.Text.Trim & ");", True)
    End Sub

    Protected Sub btnguardar_edit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguardar_edit.Click
        Try
            ViewState("dt") = Session("dt_productos_lista")
            promociones_cliente(2, txtidc_articulo.Text.Trim)
            Productos_Calculados()
            Calcular_Valores_DataTable()
            carga_productos_seleccionadas()
            cargar_subtotal_iva_total(Session("NuevoIva"))
            limpiar_campos()
            Estado_controles_captura(False)
            buscar_confirmacion_lista()
            formar_cadenas()
            tbnguardarPP.Enabled = True
            btnnuevoprepedido.Enabled = True
            aportaciones()
            If Not cbomaster.Visible = True Then
                txtcodigoarticulo.Attributes.Remove("onfocus")
                txtcodigoarticulo.Text = ""
                txtcodigoarticulo.Focus()
                controles_busqueda_prod(True)
                controles_busqueda_prod_sel_cancel(False)
            End If
        Catch ex As Exception
            WriteMsgBox("Error al Actualizar Lista de Articulos. \n \u000B \n Error: \n" & ex.Message)
        Finally
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_loading2", "<script>myStopFunction_grid();</script>", False)
        End Try
    End Sub

    'Protected Sub imgcancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgcancelar.Click
    '    controles_busqueda_prod(True)
    '    controles_busqueda_prod_sel_cancel(False)
    '    cboclientes.Items.Clear()
    'End Sub

    Protected Sub btncancelar_edit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancelar_edit.Click
        'imgaceptar.Attributes.Remove("onclick")
        'imgcancelar.Attributes.Remove("onclick")
        If Not cbomaster.Visible = True Then
            txtcodigoarticulo.Attributes.Remove("onfocus")
            txtcodigoarticulo.Text = ""
            txtcodigoarticulo.Focus()
            controles_busqueda_prod(True)
            controles_busqueda_prod_sel_cancel(False)
        End If
    End Sub

    Protected Sub cboentrega_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboentrega.SelectedIndexChanged
        Select Case cboentrega.SelectedValue
            Case 1
                btnconsignado.Enabled = True
                'rdEA.ForeColor = Drawing.Color.Blue
                'rdEP.ForeColor = Drawing.Color.Black
                'RDRC.ForeColor = Drawing.Color.Black
                'rdSF.ForeColor = Drawing.Color.Black
                'txt_codigo()
                'estado_rd(False)
                '/cboproyectos.Enabled = True
                '/TabContainer1.Enabled = True


                'If txtid.Text <> "" Then
                '    actualizar_precios(txtid.Text.Trim, txtidc_colonia.Text.Trim)
                'End If
            Case 3
                btnconsignado.Enabled = True
                limpiar_pedido_especial()
                'rdEA.ForeColor = Drawing.Color.Black
                'rdEP.ForeColor = Drawing.Color.Blue
                'RDRC.ForeColor = Drawing.Color.Black
                'rdSF.ForeColor = Drawing.Color.Black
                'txt_codigo()
                'estado_rd(False)
                '/cboproyectos.Enabled = True
                '/TabContainer1.Enabled = True
                'txtmaniobras.Text = "0.00"
                'validar_cambio_iva_Frontera()
            Case 2
                'estado_rd(False)
                '/ScriptManager.RegisterStartupScript(Me, GetType(Page), "JS", "<script>RecogeCliente();</script>", False)
                btnconsignado.Enabled = False
                limpiar_recoge_cliente()
                limpiar_pedido_especial()
                'txt_codigo()
                'rdEA.ForeColor = Drawing.Color.Black
                'rdEP.ForeColor = Drawing.Color.Black
                'RDRC.ForeColor = Drawing.Color.Blue
                'rdSF.ForeColor = Drawing.Color.Black
                'cboproyectos.Enabled = False
                'TabContainer1.Enabled = False
                'cboproyectos.SelectedIndex = 0
                'limpiar_datos_consignado()
                'actualizar_precios(txtid.Text.Trim, txtidc_colonia.Text.Trim)
                limpiar_datos_consignado()
            Case 4
                limpiar_recoge_cliente()
                btnconsignado.Enabled = True
                'estado_rd(False)
                '/ScriptManager.RegisterStartupScript(Me, GetType(Page), "JS", "<script>PedidoEspecial();</script>", False)
                'txtcodigoarticulo.Enabled = True
                'rdEA.ForeColor = Drawing.Color.Black
                'rdEP.ForeColor = Drawing.Color.Black
                'RDRC.ForeColor = Drawing.Color.Black
                'rdSF.ForeColor = Drawing.Color.Blue
                'cboproyectos.Enabled = False
                'TabContainer1.Enabled = False
                'limpiar_datos_consignado()
            Case Else
                WriteMsgBox("¿Que Fregados Estas Haciendo? \n \u000B \n ¿Que Seleccionaste?")
        End Select
    End Sub

    Protected Sub btnmaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmaster.Click
        prep_cargar_grid_prod_master_cliente(txtid.Text)
        controles_busqueda_master(True)
        controles_busqueda_prod(False)
        controles_busqueda_prod_sel_cancel(False)
    End Sub

    Sub prep_cargar_grid_prod_master_cliente(ByVal idc_cliente As Integer)
        Dim gweb As New GWebCN.Clientes
        Dim ds As New DataSet

        Try
            ds = gweb.Carga_Lista_Master_Ped(idc_cliente, Session("xidc_almacen"))

            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim row As DataRow
                    Dim dtrow As DataRow
                    Dim dt As New DataTable
                    dt = agregar_columnas_dataset2()
                    dt.Columns.Add("nombre2")
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        row = ds.Tables(0).Rows(i)
                        dtrow = dt.NewRow
                        dtrow("nombre2") = row("desart") & " || " & row("unimed")
                        dtrow("Codigo") = row("codigo")
                        dtrow("Descripcion") = row("desart")
                        '/dtrow("incluir") = False
                        '/dtrow("precio_modelo") = row("precio_modelo")
                        dtrow("precio") = row("precio_cliente")
                        '/dtrow("dias") = row("dias")
                        dtrow("Master") = row("Master")
                        '/dtrow("id_grupo") = row("id_grupo")
                        dtrow("decimales") = row("decimales")
                        If Not IsDBNull(row("precio_real")) Then
                            If row("precio_real") > 0 Then
                                dtrow("PrecioReal") = row("precio_real")
                                dtrow("Descuento") = dtrow("precio") - row("precio_real")
                            Else
                                dtrow("PrecioReal") = dtrow("precio_cliente")
                                dtrow("Descuento") = 0
                            End If
                        Else
                            dtrow("PrecioReal") = dtrow("precio")
                            dtrow("Descuento") = 0
                        End If
                        dtrow("idc_articulo") = row("idc_articulo")
                        '/dtrow("precio") = row("precio_cliente")
                        dtrow("UM") = row("unimed")
                        dtrow("costo") = row("costo")
                        '/dtrow("precio") = row("precio_cliente")
                        dtrow("Paquete") = row("paquete")
                        dtrow("precio_libre") = 0
                        dtrow("comercial") = row("comercial")
                        dtrow("fecha") = row("fecha")
                        dtrow("obscotiza") = row("obscotiza")
                        dtrow("vende_exis") = row("vende_exis")
                        dtrow("minimo_venta") = 0
                        dtrow("mensaje") = row("mensaje")
                        dtrow("porcentaje") = row("porcentaje")

                        If row("porcentaje") > 0 Then
                            dtrow("calculado") = True
                        Else
                            dtrow("calculado") = False
                        End If

                        dtrow("Anticipo") = row("anticipo")
                        dtrow("Existencia") = 0

                        If row("decimales") = True Then
                            dtrow("cantidad") = "0.00"
                        Else
                            dtrow("cantidad") = "0"
                        End If
                        dt.Rows.Add(dtrow)
                    Next
                    cbomaster.DataSource = dt
                    cbomaster.DataTextField = "nombre2"
                    cbomaster.DataValueField = "idc_articulo"
                    cbomaster.DataBind()
                    cbomaster.Attributes("style") = "width:100%;"
                    controles_busqueda_prod(False)
                    controles_busqueda_prod_sel_cancel(False)
                    controles_busqueda_master(True)
                    txtcodigoarticulo.Visible = False
                    dt.Columns.Remove("nombre2")
                    ViewState("dt_master_cotizacion") = dt
                    Session("dt_productos_busqueda") = ds.Tables(0)
                Else
                    cbomaster.Attributes("style") = "width:100%;"
                End If
            End If
            btn_seleccionar_master.Attributes("onclick") = "return editar_precios_cantidad(2);"
        Catch ex As Exception
            WriteMsgBox(ex.Message)
        End Try
    End Sub

    Function agregar_columnas_dataset2() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("idc_articulo", GetType(Integer))     '0
        dt.Columns.Add("Codigo")           '1
        dt.Columns.Add("Descripcion")      '2
        dt.Columns.Add("UM")               '3
        dt.Columns.Add("Cantidad")         '4
        dt.Columns.Add("Precio", GetType(Double))           '5
        dt.Columns.Add("Importe", GetType(Double))          '6
        dt.Columns.Add("PrecioReal", GetType(Double))       '7
        dt.Columns.Add("Descuento", GetType(Double))        '8
        dt.Columns.Add("Decimales")        '9
        dt.Columns.Add("Paquete")          '10
        dt.Columns.Add("precio_libre")     '11
        dt.Columns.Add("comercial")        '12
        dt.Columns.Add("fecha")            '13
        dt.Columns.Add("obscotiza")        '14
        dt.Columns.Add("vende_exis")       '15 
        dt.Columns.Add("minimo_venta")     '16 
        dt.Columns.Add("Master")           '17 
        dt.Columns.Add("mensaje")          '18 
        dt.Columns.Add("Calculado")        '19 
        dt.Columns.Add("Porcentaje")       '20 
        dt.Columns.Add("Nota_Credito")     '21 
        dt.Columns.Add("Anticipo", GetType(Double))         '22
        dt.Columns.Add("Costo", GetType(Double))            '23
        dt.Columns.Add("Existencia")       '24
        dt.Columns.Add("precio_minimo", GetType(Double))      '25
        'dt.Columns.Add("ultm_precio")      '26
        'dt.Columns.Add("fecha_ult_precio") '27
        dt.Columns.Add("tiene_especif", GetType(Boolean))     '28 
        dt.Columns.Add("especif", GetType(String))     '29  
        dt.Columns.Add("num_especif", GetType(Integer))     '30 
        dt.Columns.Add("ids_especif", GetType(Integer))     '30 
        dt.Columns.Add("g_especif", GetType(Integer))     '30 
        dt.Columns.Add("costo_o", GetType(Decimal))
        dt.Columns.Add("precio_o", GetType(Decimal))
        dt.Columns.Add("precio_lista_o", GetType(Decimal))
        dt.Columns.Add("precio_minimo_o", GetType(Decimal))
        Return dt
    End Function

    Sub controles_busqueda_prod(ByVal estado As Boolean)
        txtcodigoarticulo.Visible = estado
        txtcodigoarticulo.Enabled = estado
        If estado = True Then
            btnbuscar_codigo.Visible = False
        Else
            btnbuscar_codigo.Visible = True
        End If
    End Sub

    Sub controles_busqueda_master(ByVal estado As Boolean)
        cbomaster.Visible = estado
        btn_seleccionar_master.Visible = estado
        If estado = True Then
            btnmaster.Visible = False
        Else
            btnmaster.Visible = True
        End If
    End Sub

    Protected Sub btnbuscar_codigo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbuscar_codigo.Click
        controles_busqueda_master(False)
        controles_busqueda_prod(True)
        txtcodigoarticulo.Enabled = True
        txtcodigoarticulo.Attributes.Remove("onfocus")
        txtcodigoarticulo.Focus()
        cboproductos.Items.Clear()
        cboproductos.Visible = True
        btn_seleccionar_master.Visible = True
        btn_seleccionar_master.Attributes("onclick") = "return editar_precios_cantidad(1);"

    End Sub

    Protected Sub btn_seleccionar_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_seleccionar_master.Click
        If cbomaster.Items.Count <= 0 Then
            Return
        End If
        Dim dt, dt2 As New DataTable
        dt = ViewState("dt_master_cotizacion")
        Dim rows() As DataRow
        Dim sel As String = ""



        sel = "idc_articulo=" & cbomaster.SelectedValue

        dt2 = ViewState("dt")
        rows = dt2.Select(sel)

        If rows.Length > 0 Then
            WriteMsgBox("El Articulo Seleccionado Ya Se Encuentra En La Lista.")
            Return
        End If

        rows = dt.Select(sel)
        If rows.Length > 0 Then
            'Session("rowprincipal") = ""




            'Dim tspan As New TimeSpan
            'Dim inicio, final As DateTime
            'inicio = Now()
            '/////
            Dim rowprincipal As DataRow
            '/*/*/////Add columns to the DataRow/////*/*'
            rowprincipal = dt.NewRow()
            'Dim i As Integer
            'i = 0
            rowprincipal("idc_articulo") = rows(0).Item("idc_articulo")
            rowprincipal("Codigo") = rows(0).Item("codigo")
            rowprincipal("Descripcion") = rows(0).Item("descripcion")
            rowprincipal("UM") = rows(0).Item("um")
            rowprincipal("Decimales") = rows(0).Item("decimales")
            '/Decimales(CBool(rowprincipal("Decimales")))
            rowprincipal("Paquete") = rows(0).Item("paquete")
            rowprincipal("precio_libre") = rows(0).Item("precio_libre")
            rowprincipal("comercial") = rows(0).Item("comercial")
            rowprincipal("vende_exis") = rows(0).Item("vende_exis")
            rowprincipal("minimo_venta") = rows(0).Item("minimo_venta")
            rowprincipal("Master") = rows(0).Item("master")
            '/rowprincipal("EXISTENCIA") = buscar_Existencia_Articulo(rowprincipal("idc_articulo"))
            rowprincipal("fecha") = rows(0).Item("fecha")
            rowprincipal("obscotiza") = rows(0).Item("obscotiza")
            rowprincipal("mensaje") = rows(0).Item("mensaje")
            rowprincipal("existencia") = buscar_Existencia_Articulo(rowprincipal("idc_articulo"))

            'Dim datos() As Object
            'datos = buscar_precio(rowprincipal("idc_articulo"))

            'rowprincipal("Precio") = datos(0)
            'rowprincipal("PrecioReal") = datos(4)
            'rowprincipal("Descuento") = datos(2)
            'rowprincipal("Nota_Credito") = datos(3)
            'rowprincipal("Costo") = datos(1)


            If calculado(rowprincipal("idc_articulo")) = Nothing Then
                rowprincipal("calculado") = False
                rowprincipal("porcentaje") = 0.0
                '/txtdescripcion.Text = rows(0).Item("desart")
                '/txtcodigoarticulo.Text = rows(0).Item("codigo")
                '/txtUM.Text = rows(0).Item("unimed")
                Session("rowprincipal") = rowprincipal
                buscar_precio_producto(rows(0).Item("idc_articulo"))
                ver_observaciones_articulo(rowprincipal)

                '/txtcantidad.Focus()
                'controles_busqueda_prod(True)
                'controles_busqueda_prod_sel_cancel(False)
                txtcodigoarticulo.Enabled = False
                Estado_controles_captura(True)
                'If rowprincipal("nota_credito") = True Then
                '    txtprecio.Enabled = False
                'End If
                'dt.Rows.Add(rowprincipal)
                Session("rowprincipal") = rowprincipal
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "selecciona_mast", "<script>editar_precios_cantidad();</script>", False)
                'imgaceptar.Attributes("onclick") = "return false;"
                'imgcancelar.Attributes("onclick") = "return false;"
            Else
                rowprincipal("precio") = Redondeo_cuatro_decimales(0.0)
                rowprincipal("cantidad") = 1
                rowprincipal("precioreal") = Redondeo_cuatro_decimales(rowprincipal("precio"))
                rowprincipal("calculado") = True
                rowprincipal("porcentaje") = calculado(rowprincipal("idc_articulo"))
                rowprincipal("nota_credito") = False
                dt.Rows.Add(rowprincipal)
                ViewState("dt") = dt
                rowprincipal = Nothing
                Estado_controles_captura(False)
                controles_busqueda_prod(True)
                controles_busqueda_prod_sel_cancel(False)
                txtcodigoarticulo.Enabled = True
                '/calcular_valores()
                cargar_subtotal_iva_total(Session("NuevoIva"))
                'imgaceptar.Attributes("onclick") = "return false;"

                '/Articulos_Calculados()
                '/cargar_subtotal_iva_total(Session("Xiva"))
                'btncancelar.Enabled = False
                ' btnagregar.Enabled = False
                'txtcantidad.Attributes("onfocus") = "this.blur()"
                txtcodigoarticulo.Attributes.Remove("onfocus")
                '/limpiar_controles()
                txtcodigoarticulo.Focus()
                '/lbl_idc.Text = ""
                '/btnenviar.Enabled = True
                '/btncapturar.Enabled = True
                formar_cadenas()
                'cargar_proyectos_cliente(txtid.Text.Trim)
                controles_busqueda_prod(True)
                controles_busqueda_prod_sel_cancel(False)
                'imgaceptar.Attributes.Remove("onclick")
                'imgcancelar.Attributes.Remove("onclick")
                grdproductos2.DataSource = ViewState("dt")
                grdproductos2.DataBind()
                tbnguardarPP.Enabled = True
                btnnuevoprepedido.Enabled = True
            End If
            'final = Now()
            'tspan = final.Subtract(inicio).Duration()
            'CargarMsgbox("", tspan.Duration.ToString(), False, 2)
        Else
            WriteMsgBox("Error al Cargar Informacion del Producto Seleccionado.")
            Return
        End If
    End Sub
    Sub promociones_cliente(ByVal tipo As Integer, ByVal idc_articulo As Integer)
        'Se le Quito lo de Convenio :
        'Table de Promociones.
        Try

       
        Dim dt As New DataTable
        dt = Session("tx_pedido_gratis")


        'Table de Articulos Capturados.
        Dim dt_lista As New DataTable
        dt_lista = ViewState("dt")


        Dim ds As New DataSet
        Dim gweb As New GWebCN.Clientes

        Dim cantidad As Integer = 0

        Dim row As DataRow
        If tipo = 1 Then
            idc_articulo = dt_lista.Rows(dt_lista.Rows.Count - 1).Item("idc_articulo")
            cantidad = dt_lista.Rows(dt_lista.Rows.Count - 1).Item("cantidad")
            ds = gweb.clientes_promocion(idc_articulo, txtid.Text.Trim, cantidad, txtlistap.Text.Trim)
            If ds.Tables(0).Rows.Count > 0 Then
                eliminar_promocion(ds.Tables(0).Rows(0).Item("idc_promocion")) 'porque esta esto aqui MIC 01-06-2015
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1


                    row = dt.NewRow
                    row("idc_articulo") = ds.Tables(0).Rows(i).Item("idc_articulo")
                    row("cantidad") = ds.Tables(0).Rows(i).Item("cantidad")
                    row("codigo") = ds.Tables(0).Rows(i).Item("codigo")
                    row("unimed") = ds.Tables(0).Rows(i).Item("unimed")
                    row("desart") = ds.Tables(0).Rows(i).Item("desart")
                    row("idc_promociond") = ds.Tables(0).Rows(i).Item("idc_promociond")
                    row("idc_promocion") = ds.Tables(0).Rows(i).Item("idc_promocion")
                    dt.Rows.Add(row)
                Next
                dt_lista.Rows(dt_lista.Rows.Count - 1).Item("idc_promocion") = ds.Tables(0).Rows(0).Item("idc_promocion")
            End If
        ElseIf tipo = 2 Then
            Dim rows2() As DataRow
                rows2 = dt_lista.Select("idc_articulo=" & idc_articulo)
            If IsDBNull(rows2(0)("idc_promocion")) Then
                rows2(0)("idc_promocion") = 0
            End If
            If rows2.Length > 0 Then
                cantidad = rows2(0)("cantidad")
                ds = gweb.clientes_promocion(idc_articulo, txtid.Text.Trim, cantidad, txtlistap.Text.Trim)
                If ds.Tables(0).Rows.Count Then

                    If rows2(0)("idc_promocion") > 0 Then
                        eliminar_promocion(rows2(0)("idc_promocion"))
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                            row = dt.NewRow
                            row("idc_articulo") = ds.Tables(0).Rows(0).Item("idc_articulo")
                            row("cantidad") = ds.Tables(0).Rows(i).Item("cantidad")
                            row("codigo") = ds.Tables(0).Rows(i).Item("codigo")
                            row("unimed") = ds.Tables(0).Rows(i).Item("unimed")
                            row("desart") = ds.Tables(0).Rows(i).Item("desart")
                            row("idc_promociond") = ds.Tables(0).Rows(i).Item("idc_promociond")
                            row("idc_promocion") = ds.Tables(0).Rows(i).Item("idc_promocion")
                            dt.Rows.Add(row)
                        Next


                    Else

                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            row = dt.NewRow
                            row("idc_articulo") = ds.Tables(0).Rows(i).Item("idc_articulo")
                            row("cantidad") = ds.Tables(0).Rows(i).Item("cantidad")
                            row("codigo") = ds.Tables(0).Rows(i).Item("codigo")
                            row("unimed") = ds.Tables(0).Rows(i).Item("unimed")
                            row("desart") = ds.Tables(0).Rows(i).Item("desart")
                            row("idc_promociond") = ds.Tables(0).Rows(i).Item("idc_promociond")
                            row("idc_promocion") = ds.Tables(0).Rows(i).Item("idc_promocion")
                            dt.Rows.Add(row)
                        Next


                        For i As Integer = 0 To dt_lista.Rows.Count - 1
                            If dt_lista.Rows(i).Item("idc_articulo") = idc_articulo Then
                                    'dt_lista.Rows(i).Item("idc_promocion") = ds.Tables(0).Rows(0).Item("idc_promociond")
                                    dt_lista.Rows(i).Item("idc_promocion") = ds.Tables(0).Rows(0).Item("idc_promocion") 'modificada MIC 03-06-2015
                                Exit For
                            End If
                        Next
                    End If
                Else
                    If Not IsDBNull(rows2(0)("idc_promocion")) Then
                        If rows2(0)("idc_promocion") > 0 Then
                            eliminar_promocion(rows2(0)("idc_promocion"))
                            For i As Integer = 0 To dt_lista.Rows.Count - 1
                                    If Not IsDBNull(dt_lista.Rows(i).Item("idc_promocion")) Then 'if nuevo 02-06-2015 MIC
                                        If dt_lista.Rows(i).Item("idc_promocion") = rows2(0)("idc_promocion") Then
                                            dt_lista.Rows(i).Item("idc_promocion") = 0
                                            Exit For
                                        End If
                                    End If

                            Next
                        End If
                    End If
                End If
            End If
        End If

        If dt.Rows.Count > 0 Then
            imgpromocion.Attributes("onclick") = "window.open('productos_g.aspx');"
            imgpromocion.Attributes("style") = "display:inline;"
        Else
            imgpromocion.Attributes.Remove("onclick")
            imgpromocion.Attributes("style") = "display:none;"
        End If

        'Table de Promociones
        Session("tx_pedido_gratis") = dt


        'Table de Articulos Capturados.
        ViewState("dt") = dt_lista
        Catch ex As Exception
            WriteMsgBox("Error en promociones clientes. \n \u000B \n Error: \n" & ex.Message)
        End Try
    End Sub

    'nuevo MIC 01-06-2015
    Sub eliminar_promocion(ByVal idc_promocion As Integer)
        Dim dt As New DataTable
        Try
            dt = Session("tx_pedido_gratis")
            'Table de Articulos Capturados.
            'Dim dt_lista As New DataTable
            'dt_lista = ViewState("dt")
            '----------------------------
            If dt.Rows.Count > 0 Then
                For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                    If idc_promocion = dt.Rows(i).Item("idc_promocion") Then
                        dt.Rows(i).Delete()
                    End If
                Next

                If dt.Rows.Count = 0 Then
                    imgpromocion.Attributes("style") = "display:none;"
                    imgpromocion.Attributes.Remove("onclick")
                End If
            End If
            '--------------------------
            'Dim registro() As DataRow
            'registro = dt_lista.Select("idc_promocion=" + idc_promocion)
            'If registro.Count = 1 Then
            '    registro(0)("idc_promocion") = 0
            'End If
            '-------------------------
            'subimos a session de nuevo 
            'Table de Promociones
            Session("tx_pedido_gratis") = dt


            'Table de Articulos Capturados.
            'ViewState("dt") = dt_lista
        Catch ex As Exception
            WriteMsgBox("Error al Tratar de Eliminar Articulo Gratis x Promocion.")
        End Try
    End Sub

    'Sub eliminar_promocion(ByVal idc_promocion As Integer)
    '    Dim dt As New DataTable
    '    Try
    '        dt = Session("tx_pedido_gratis")
    '        If dt.Rows.Count > 0 Then
    '            For i As Integer = dt.Rows.Count - 1 To 0 Step -1
    '                If idc_promocion = dt.Rows(i).Item("idc_promocion") Then
    '                    dt.Rows(i).Delete()
    '                End If
    '            Next
    '            If Not dt.Rows.Count > 0 Then
    '                imgpromocion.Attributes("style") = "display:none;"
    '                imgpromocion.Attributes.Remove("onclick")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        WriteMsgBox("Error al Tratar de Eliminar Articulo Gratis x Promocion.")
    '    End Try
    'End Sub

    Sub colores_clear()
        txtnombre.Attributes.Remove("style")
        txtstatus.Attributes.Remove("style")
    End Sub

    Protected Sub btncargarflete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncargarflete.Click
        Agregar_maniobras()
        formar_cadenas()
        WriteMsgBox("Se Cargo Monto del Flete Correctamente. \n \u000B \n Para Continuar de Click en Generar Pre-Pedido.")
    End Sub

    Protected Sub btnref_especif_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnref_especif.Click
        If Not Session("dt_productos_lista") Is Nothing Then
            Try
                ViewState("dt") = Session("dt_productos_lista")
                Productos_Calculados()
                Calcular_Valores_DataTable()
                carga_productos_seleccionadas()
                cargar_subtotal_iva_total(Session("NuevoIva"))
                limpiar_campos()
                Estado_controles_captura(False)
                buscar_confirmacion_lista()
                formar_cadenas()
                tbnguardarPP.Enabled = True
                btnnuevoprepedido.Enabled = True
                aportaciones()
                If Not cbomaster.Visible = True Then
                    txtcodigoarticulo.Attributes.Remove("onfocus")
                    txtcodigoarticulo.Text = ""
                    txtcodigoarticulo.Focus()
                    controles_busqueda_prod(True)
                    controles_busqueda_prod_sel_cancel(False)
                End If
            Catch ex As Exception
                WriteMsgBox("Error al Actualizar Lista de Articulos. \n \u000B \n Error: \n" & ex.Message)
            Finally
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "cerrar_loading2", "<script>myStopFunction_grid();</script>", False)
            End Try
        End If
    End Sub

    
End Class



