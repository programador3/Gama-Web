Imports System.Data
Imports System.IO

Partial Class especificaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("xiva") Is Nothing Then
            ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid().ToString(), "<script>close_no_session();</script>", False)
        End If
        If Not Page.IsPostBack Then
            cargar_parametros()
            Dim idc_articulo As String = Request.QueryString("cdi")
            Dim idc_cotizacion As String = Request.QueryString("cot")
            Dim cd As Integer = Request.QueryString("cd")
            txtcd.Value = cd
            txttype.Value = Request.QueryString("type")
            If txttype.Value = "2013" Then
                txttotal_n.Attributes("onfocus") = "this.blur();"
            End If
            txtcantidad.Text = IIf(Request.QueryString("can") = "", txtcantidad.Text, Request.QueryString("can"))
            If IsNumeric(idc_articulo) Then
                cargar_selec(idc_articulo)
                cargar_detalle_arti(idc_articulo)
                txttot.Value = Math.Round(CDec(txtprecio.Value), 4)
                txtiva.Value = Session("xiva")

                Dim rfc As String = "*"
                Dim iva As Decimal = 1 ' + txtiva.Value / 100.0
                Dim precio_sin_iva As Decimal = Math.Round(CDec(txttot.Value) / iva, 4)
                Dim pr As Decimal = txtprecio.Value
                Dim cos As Decimal = txtcosto.Value
                If (pr > 0) And cos > 0 Then
                    txtp.Value = (((CDec(pr) / iva) / CDec(cos)) - 1) * 100.0
                End If

                img_principal(idc_articulo)
                cargar_imagen(idc_articulo)
                cargar_acabados(idc_articulo)
                If IsNumeric(idc_cotizacion) And (idc_cotizacion > 0) Then
                    btnguardar.Attributes.Add("onclick", "return guardar();")
                Else
                    If Request.QueryString("type") = "" Then
                        'btnguardar.Attributes.Add("onclick", "return add_cliente(" & idc_articulo & ");")
                        btnguardar.Attributes.Add("onclick", "return guardar();")
                    Else
                        btnguardar.Attributes.Add("onclick", "alert('La Sesión ha expirado.\n \u000b \nInicie Sesión Nuevamente.');")
                    End If
                End If
                btncancelar.Attributes.Add("onclick", "cerrar();return false;")
                txtred.Value = Request.QueryString("red")
                Dim idc_cotizaciond As Integer = Request.QueryString("ind")
                Dim idc_cotizacionarti As Integer = Request.QueryString("ina")
                Dim fila As Integer = Request.QueryString("fl")
                txtfila.Value = fila

                If IsNumeric(idc_cotizaciond) Then
                    If idc_cotizaciond > 0 Then
                        cargar_selec(idc_articulo)
                        txtidc_cot.Value = idc_cotizaciond
                    End If
                End If
                If IsNumeric(idc_cotizacionarti) Then
                    If idc_cotizacionarti >= 0 And txtfila.Value > 0 Then
                        cargar_especific_arti(idc_cotizacionarti)
                        'txtidc_cot.Value = idc_cotizaciond.
                    End If
                    txtidc_cotarti.Value = idc_cotizacionarti
                End If
                'Exec_JS("finishes_selected();")
                Dim pre_min As Decimal, pre As Decimal
                pre_min = CDec(txtpre_min.Value)
                pre = CDec(txtprecio.Value)
                pre = (pre / pre_min)
                txt_pm.Value = pre
            End If

            Dim read As String = Request.QueryString("read")
            If read <> "" Then
                txtcantidad_nueva.Enabled = False
                btnguardar.Enabled = False
                btnguardar.Attributes.Remove("onclick")
                btncancelar.Text = "Cerrar"
            End If
        End If
    End Sub

    Sub cargar_detalle_arti(ByVal idc_articulo As Integer)
        'Dim sitio As New SitioCN.productos
        'Dim ds As New DataSet
        'Try
        '    ds = sitio.Especificaciones(idc_articulo, Session("idc_usuario"), 0, Session("idc_sucursal"), True, True)
        '    If ds.Tables(1).Rows.Count > 0 Then
        '        txtcosto.Value = ds.Tables(1).Rows(0).Item("costo").ToString().Trim()
        '        txtpre_min.Value = ds.Tables(1).Rows(0).Item("precio_minimo").ToString().Trim()
        '        txtprecio.Value = ds.Tables(1).Rows(0).Item("precio").ToString().Trim()
        '    Else
        txtcosto.Value = 0
        txtpre_min.Value = 0
        txtprecio.Value = 0
        '    End If
        'Catch ex As Exception
        '    CargarMsgBox("Error al Cargar Especificaciones.\n \u000B \nError:\n" & ex.Message)
        'End Try


        Dim dt As New DataTable
        dt = Session("dt_productos_lista")
        If dt Is Nothing Then
            Return
        End If
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("idc_articulo") = idc_articulo Then
                    div_desart.InnerHtml = "<b>Articulo: </b>" & dt.Rows(i).Item("Descripcion") & "</br>"
                    div_desart.InnerHtml = div_desart.InnerHtml & "<b>UM: </b>" & dt.Rows(i).Item("um") & "</br>"
                    div_desart.InnerHtml = div_desart.InnerHtml & "<b>Codigo: </b>" & dt.Rows(i).Item("codigo")
                    'txtum.Text = dt.Rows(i).Item("um")
                    'txtprecio.Text = FormatNumber(dt.Rows(i).Item("precio"), 4)
                    'txtprecioreal.Text = FormatNumber(dt.Rows(i).Item("precioreal"), 4)
                    'txtpreciominimo.Text = FormatNumber(dt.Rows(i).Item("precio_minimo"), 4)
                    txtcantidad.Text = dt.Rows(i).Item("Cantidad")
                    txtcantidad_nueva.Text = dt.Rows(i).Item("Cantidad")
                    'txtcodigoarticulo.Text = dt.Rows(i).Item("codigo")
                    'If dt.Rows(i).Item("Decimales") = True Then
                    '    txtcantidad.Attributes("onkeydown") = "return soloNumeros(event,'true');"
                    '    txtcantidad.Attributes("onclick") = "window.open('teclado.aspx?ctrl=" & txtcantidad.ClientID & "&dc=3&fn=precio_cantidad');"
                    'Else
                    '    txtcantidad.Attributes("onkeydown") = "return soloNumeros(event,'false');"
                    '    txtcantidad.Attributes("onclick") = "window.open('teclado.aspx?ctrl=" & txtcantidad.ClientID & "&dc=0&fn=precio_cantidad');"
                    'End If
                    'If dt.Rows(i).Item("nota_credito") = True Then
                    '    txtprecio.Attributes("onfocus") = "this.blur();"
                    '    txtprecioreal.Attributes("onfocus") = "this.blur();"
                    '    txtprecio.Attributes.Remove("onclik")
                    '    txtprecioreal.Attributes.Remove("onclik")
                    '    txtprecio.Attributes.Remove("onblur")
                    '    txtprecioreal.Attributes.Remove("onblur")
                    '    lblnc.Visible = True
                    'Else
                    '    txtprecio.Attributes("onfocus") = "this.blur();"
                    '    txtprecioreal.Attributes("onfocus") = "this.blur();"
                    '    txtprecio.Attributes("onclick") = "window.open('teclado.aspx?ctrl=" & txtprecio.ClientID & "&dc=4&fn=validarPrecio');"
                    '    txtprecioreal.Attributes("onclick") = "window.open('teclado.aspx?ctrl=" & txtprecioreal.ClientID & "&dc=4&fn=validarPrecior');"
                    '    lblnc.Visible = False
                    'End If

                    txtcosto.Value = dt.Rows(i).Item("costo_o").ToString().Trim()
                    txtpre_min.Value = dt.Rows(i).Item("precio_minimo_o").ToString().Trim()
                    txtprecio.Value = dt.Rows(i).Item("precio_o").ToString().Trim()

                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Para Editar desde la Pantalla de Agregar al Carrito
    ''' </summary>
    ''' <param name="idc_articulo"></param>
    Sub cargar_selec(ByVal idc_articulo As Integer)
        Dim dt As New DataTable
        dt = Session("dt_productos_lista")
        Try
            If Not dt Is Nothing Then
                txtselected.Value = ""
                For Each row As DataRow In dt.Rows
                    If (row("idc_articulo") = idc_articulo) Then
                        txtselected.Value = row("especif")
                    End If
                Next
            End If
        Catch ex As Exception
            CargarMsgBox("Error al Cargar Especificaciones Seleccionadas")
        End Try
    End Sub
    ''' <summary>
    ''' Parametros de Configuracion
    ''' </summary>
    ''' <remarks></remarks>
    Sub cargar_parametros()
        txtparametros_decimales.Value = 4 'Session("xdecimales")
        txtparametros_precioneto.Value = False 'Session("xprecio_neto")
    End Sub
    ''' <summary>
    ''' Para Editar Desde la Pantalla de Cotizaciones Pendientes
    ''' </summary>
    ''' <param name="idc_cotizacionarti"></param>
    ''' <remarks></remarks>
    Sub cargar_especific_arti(ByVal idc_cotizacionarti As Integer)
        'Dim sitio As New SitioCN.productos
        'Dim ds As New DataSet
        Try
            Dim dt As New DataTable
            Dim type_form As Integer = 0
            If (Not Session("dt_detalles_edit_cot_copy") Is Nothing) Then
                dt = Session("dt_detalles_edit_cot_copy")
                type_form = 1
            End If

            If (Not Session("dt_detalles_edit_cot_copy_2") Is Nothing) Then
                dt = Session("dt_detalles_edit_cot_copy_2")
                type_form = 2
            End If
            If type_form = 1 Then
                If dt.Rows.Count > 0 Then
                    For Each row As DataRow In dt.Rows
                        If row("idc_cotizacionarti") = idc_cotizacionarti And row("id_fila") = txtfila.Value Then
                            Dim especif As String = row("especif").ToString().Trim()
                            txtselected.Value = especif
                            'especif.Substring(2, especif.Length - 2)
                            Exit For
                        End If
                    Next
                End If
            End If

            If type_form = 2 Then
                If dt.Rows.Count > 0 Then
                    For Each row As DataRow In dt.Rows
                        If row("idc_cotizaciond") = idc_cotizacionarti And row("id_fila") = txtfila.Value Then
                            Dim especif As String = row("especif").ToString().Trim()
                            txtselected.Value = especif
                            'especif.Substring(2, especif.Length - 2)
                            Exit For
                        End If
                    Next
                End If
            End If

            'ds = sitio.especific_arti(idc_cotizacionarti)
            'If ds.Tables(0).Rows.Count > 0 Then

            'End If
        Catch ex As Exception
            CargarMsgBox("Error al Cargar Especificaciones Seleccionadas")
        End Try
    End Sub
    Sub costos_adicionale(ByRef ds As DataSet)
        For Each row As DataRow In ds.Tables(0).Rows
            If Not IsDBNull(row("por_opc")) Then
                If row("por_opc") = True Then
                    row("costo_adi_total_mxn") = (txtcosto.Value / row("tipo_cambio") * (row("costo_adicional_opc") / 100.0)) * row("tipo_cambio")
                End If
            End If

            If Not IsDBNull(row("por_rel")) Then
                If row("por_rel") = True Then
                    row("costo_adi_total_mxn") = (txtcosto.Value / row("tipo_cambio") * (row("costo_adicional") / 100.0)) * row("tipo_cambio")
                End If
            End If
        Next
    End Sub

    Sub cargar_acabados(ByVal idc_articulo As Integer)
        Dim ds As New DataSet
        Dim sitio As New GWebCN.Productos
        Dim existe As Boolean = False
        Dim adi_text As String = ""
        Dim cont As Integer = 0
        Dim data_desc As String = ""
        Dim l_v_p As String = ""
        Dim unidad As String = uni_archi("esparti")
        Try
            ds = sitio.articulo_acabados(idc_articulo)
            If ds.Tables(0).Rows.Count > 0 Then
                costos_adicionale(ds)
                ds.Tables(0).Columns.Add("desc", GetType(String), "") '"trim(descripcion_opc) + ' | ' + trim(clave) + ' | ' + iif(costo_adi_total_mxn>0,'*','')")
                ds.Tables(0).Columns.Add("tot_adi", GetType(Decimal), "costo_adi_total_mxn * (1+(" & txtp.Value / 100 & ")) * " & 1) '=H2*(1+(C5/100))*1.16
                ''ds.Tables(0).Columns.Add("", GetType(Decimal), "Decimal.Round(costo_adi_total_mxn * (1+(" & txtp.Value / 100 & ")) * " & 1 + (txtiva.Value / 100) & ") ,4) ")
                'cargar_img_acabados(ds.Tables(0))
                Dim dv_especif As New DataView
                dv_especif = ds.Tables(0).DefaultView.ToTable(True, "descripcion_arti").DefaultView
                For Each rows As DataRow In dv_especif.ToTable().Rows
                    Dim div_title As New HtmlGenericControl("div")
                    div_title.Attributes("id") = "div_title"
                    div_title.InnerText = rows("descripcion_arti")
                    div_acabados.Controls.Add(div_title)



                    Dim ul As New HtmlGenericControl("ul")

                    Dim grupo As String = ""
                    Dim grupo_actual As String = ""
                    Dim cbooptions As New DropDownList()

                    Dim div_options As New HtmlGenericControl("div")

                    Dim idc_artiespeci As Integer = 0

                    For Each row As DataRow In ds.Tables(0).Rows
                        If row("descripcion_arti").ToString().Trim() = rows("descripcion_arti").ToString().Trim() Then
                            div_title.Attributes("class") = "tit_" & row("idc_artiespeci") & " title"
                            Dim tot_adi As String = IIf(row("tot_adi") > 0, (Math.Floor(row("tot_adi") * 10000) / 10000), "0.00")


                            Dim dv_lock As New DataView
                            dv_lock = ds.Tables(1).Copy.DefaultView()
                            Dim str_lock As String = ""
                            If dv_lock.ToTable().Rows.Count > 0 Then
                                dv_lock.RowFilter = "idc_artiespeciopc = " & row("idc_artiespeciopc")
                                If dv_lock.ToTable().Rows.Count > 0 Then
                                    For Each row_lock As DataRow In dv_lock.ToTable().Rows
                                        str_lock = str_lock & row_lock("idc_artiespeci") & ","
                                        Dim iindex As Integer = l_v_p.IndexOf(row_lock("idc_artiespeci"))
                                        If (Not iindex >= 0) Then
                                            l_v_p = l_v_p & row_lock("idc_artiespeci") & ","
                                        End If
                                    Next
                                End If
                            End If

                            '*/*
                            If row("idc_artiespeciopc") = 174 Then
                                l_v_p = "21,"
                                str_lock = "21,"
                            End If
                            '*/*

                            If row("foto") = True Then
                                cont = cont + 1
                                grupo = row("grupo")
                                If grupo <> "" Then
                                    If Not grupo = grupo_actual Then
                                        Dim span As New HtmlGenericControl("span")
                                        span.Attributes("class") = "span_ul"
                                        span.InnerHtml = grupo
                                        ul.Controls.Add(span)
                                    End If
                                    grupo_actual = grupo
                                End If

                                ul.Attributes("class") = "type_" & row("idc_artiespeci")

                                Dim li As New HtmlGenericControl("li")
                                li.ID = row("idc_artiespeciopc").ToString().Trim()
                                li.Attributes("c-adi") = row("costo_adi_total_mxn")
                                li.Attributes("grp") = row("idc_artiespeci")

                                li.Attributes("l_v") = str_lock
                                li.Attributes("l") = IIf(str_lock = "", "0", "1")


                                Dim a As New HtmlGenericControl("a")
                                Dim img As New HtmlGenericControl("img")
                                img.Attributes("src") = unidad & row("idc_artiespeciopc").ToString().Trim() & "_chica.jpg"
                                '"finishes/" & row("idc_artiespeciopc").ToString().Trim() & "_chica.jpg"
                                img.Attributes("alt") = row("descripcion_opc").ToString().Trim()
                                img.Attributes("onerror") = "this.src='finishes/GaleriaNoDisponible.jpg';"
                                img.Attributes("image-src") = "1"
                                'img.Attributes("onerror") = "this.src='';this.style.width='auto';this.setAttribute('image-src','0');"
                                a.Controls.Add(img)
                                li.Attributes("onclick") = "return zoom_image2(" & row("idc_artiespeciopc").ToString().Trim() & ",this," & IIf(row("adicional").ToString().Trim() = "", "0", "1") & ");" '"alert('" & row("idc_artiespeciopc").ToString().Trim() & "');"
                                'a.Attributes("onmouseover") = "return zoom_image(" & row("idc_artiespeciopc").ToString().Trim() & ",this);" '"alert('" & row("idc_artiespeciopc").ToString().Trim() & "');"
                                'a.Attributes("onmouseout") = "return zoom_image_out();" '"alert('" & row("idc_artiespeciopc").ToString().Trim() & "');"
                                a.Attributes.Add("data-info", row("descripcion_opc").ToString().Trim() & IIf(CDec(tot_adi) > 0, " (+" & CStr(FormatNumber(Math.Floor(tot_adi * 100) / 100, 2)) & ")", ""))
                                a.Attributes("data-info2") = row("descripcion_opc").ToString().Trim()
                                If Not row("adicional").ToString().Trim() = "" Then
                                    adi_text = row("adicional").ToString().Trim()
                                    existe = True
                                    li.Attributes.Add("data-adi", "1")
                                    li.Attributes("especif") = row("idc_artiespeci").ToString().Trim()
                                    data_desc = row("descripcion_arti").ToString().Trim()
                                    idc_artiespeci = row("idc_artiespeci")
                                Else
                                    li.Attributes.Add("data-adi", "0")
                                End If
                                Dim div_cve As New HtmlGenericControl("div")
                                div_cve.InnerHtml = IIf(row("clave").ToString().Trim() = "", " ", row("clave").ToString().Trim()) '& IIf(CDec(tot_adi) > 0, " (+" & CStr(FormatNumber(CDec(tot_adi), 4)) & ")", "")
                                div_cve.Attributes("class") = "span_cve"
                                If row("costo_adi_total_mxn") > 0 Then
                                    div_cve.Attributes("class") = "span_cve adi"
                                    div_cve.InnerHtml = IIf(CDec(tot_adi) > 0, " +" & CStr(FormatNumber(Math.Floor(tot_adi * 100) / 100, 2)) & "<br>", "<br>") & div_cve.InnerHtml
                                Else
                                    div_cve.InnerHtml = " " & "<br>" & div_cve.InnerHtml
                                End If
                                li.Controls.Add(a)
                                li.Controls.Add(div_cve)
                                'If cont = 1 Then
                                '    Dim span As New HtmlGenericControl("span")
                                '    li.Controls.Add(span)
                                '    li.Attributes.Add("class", "selected")
                                'End If
                                ul.Attributes("l_v_p") = l_v_p
                                ul.Controls.Add(li)
                            Else
                                Dim item As New ListItem
                                item.Value = row("idc_artiespeciopc").ToString().Trim()
                                item.Text = row("descripcion_opc").ToString().Trim()
                                item.Attributes("c-adi") = row("costo_adi_total_mxn")
                                item.Attributes("data-info") = row("descripcion_opc").ToString().Trim() & IIf(CDec(tot_adi) > 0, " (+" & CStr(FormatNumber(CDec(tot_adi), 4)) & ")", "")
                                item.Attributes("grp") = row("idc_artiespeci")
                                If Not row("adicional").ToString().Trim() = "" Then
                                    adi_text = row("adicional").ToString().Trim()
                                    existe = True
                                    item.Attributes.Add("data-adi", "1")
                                    item.Attributes("especif") = row("idc_artiespeci").ToString().Trim()
                                    data_desc = row("descripcion_arti").ToString().Trim()

                                Else
                                    item.Attributes.Add("data-adi", "0")
                                End If
                                cbooptions.Items.Add(item)
                                cbooptions.Attributes("grp") = row("idc_artiespeci")

                                'div_options.Attributes("class") = "type_" & row("idc_artiespeci")
                                Dim div_item As New HtmlGenericControl("div")
                                div_item.ID = row("idc_artiespeciopc").ToString().Trim()
                                div_item.Attributes("class") = "item_opt"
                                Dim span_desc, span_cos As New HtmlGenericControl("span"), kbd_cve As New HtmlGenericControl("kbd")
                                span_desc.Attributes("class") = "f_l"
                                span_cos.Attributes("class") = "f_r"

                                div_item.Attributes("l_v") = str_lock
                                div_item.Attributes("l") = IIf(str_lock = "", "0", "1")

                                span_desc.InnerHtml = row("descripcion_opc")
                                kbd_cve.InnerHtml = IIf(Not row("clave") = "", "[" & row("clave").ToString().Trim() & "]", "")

                                'span_cos.InnerHtml = "<b>+</b> " & IIf(row("tot_adi") > 0, (Math.Floor(row("tot_adi") * 10000) / 10000), "0.00")
                                span_cos.InnerHtml = IIf(CDec(tot_adi) > 0, "<b>+</b> " & FormatNumber(Math.Floor(tot_adi * 100) / 100, 2), "")
                                'Math.Truncate(row("tot_adi") * 1000 / 1000)
                                'IIf(row("tot_adi") > 0, "<b>+</b> " & Math.Round(row("tot_adi"), 2), " ")

                                div_item.Attributes("c-adi") = row("costo_adi_total_mxn")
                                'div_item.Attributes("data-info") = row("descripcion_opc").ToString().Trim() & IIf(CDec(tot_adi) > 0, "(" & FormatNumber(Math.Floor(tot_adi * 100) / 100, 2) & ")", "")
                                div_item.Attributes("data-info") = IIf(CDec(tot_adi) > 0, "(+" & FormatNumber(Math.Floor(tot_adi * 100) / 100, 2) & ")", "")
                                div_item.Attributes("onclick") = "opts(this);"
                                div_item.Attributes("grp") = row("idc_artiespeci")

                                If Not row("adicional").ToString().Trim() = "" Then
                                    adi_text = row("adicional").ToString().Trim()
                                    existe = True
                                    div_item.Attributes.Add("data-adi", "1")
                                    data_desc = row("descripcion_arti").ToString().Trim()
                                    div_item.Attributes("especif") = row("idc_artiespeci").ToString().Trim()
                                    idc_artiespeci = row("idc_artiespeci")
                                Else
                                    div_item.Attributes.Add("data-adi", "0")
                                End If

                                div_item.Controls.Add(span_desc)
                                div_item.Controls.Add(kbd_cve)
                                div_item.Controls.Add(span_cos)
                                div_options.Attributes("l_v_p") = l_v_p
                                div_options.Controls.Add(div_item)
                            End If

                        End If
                    Next
                    l_v_p = ""

                    'Quitar "cbooptions" , se cambio por "ListView"
                    If cbooptions.Items.Count > 0 Then
                        cbooptions.Items.Insert(0, New ListItem("", ""))
                        cbooptions.Attributes("class") = "drop"
                        div_options.Attributes("class") = "options"
                        'div_acabados.Controls.Add(div_options)
                        div_acabados.Controls.Add(cbooptions)
                    Else
                        Dim li_zoom As New HtmlGenericControl("li")
                        li_zoom.Attributes("class") = "zoom"
                        li_zoom.Attributes("data-adi") = "0"
                        li_zoom.Attributes("c-adi") = "0.0000"
                        Dim img_li As New System.Web.UI.WebControls.Image
                        img_li.Attributes("src") = "images/search_icon.png"
                        img_li.Attributes("style") = "width:auto;height:auto;"
                        li_zoom.Controls.Add(img_li)
                        ul.Controls.Add(li_zoom)
                        div_acabados.Controls.Add(ul)
                    End If


                    cont = 0
                    'div_acabados.Controls.Add(ul)
                    'Agregar lo de Adicional...
                    If existe = True Then
                        ul.Attributes.Add("data", "1")
                        ul.Attributes.Add("data-desc", data_desc)
                        agregar_div_adicionales(adi_text, data_desc, idc_artiespeci)
                        existe = False
                        data_desc = ""
                    End If
                Next
            Else
                CargarMsgBox("No se Encontraron Acabados Disponibles Para Este Producto.")
            End If

            'Dim ds2 As New DataSet
            'ds2 = sitio.articulo_acabados_info(idc_articulo)

            'If ds2.Tables(1).Rows.Count > 0 Then
            '    div_desart.InnerText = ds2.Tables(1).Rows(0).Item("desart").ToString.Trim()
            'End If
        Catch ex As Exception
            CargarMsgBox("Error al Cargar Acabados.\n \u000b \nError:\n" & ex.Message)
        End Try
    End Sub
    Sub agregar_div_adicionales(ByVal adi As String, ByVal data_desc As String, ByVal idc_artiespeci As Integer)
        If adi.Trim() <> "" Then
            Dim spl() As String = adi.Split(",")
            Dim cboadi As New DropDownList
            cboadi.Attributes.Add("id", "cboadi")
            cboadi.Attributes.Add("onchange", "finishes_selected();")
            cboadi.Attributes("especif") = idc_artiespeci
            If spl.Length > 0 Then
                For Each valor As String In spl
                    Dim item As New ListItem
                    item.Value = valor.Trim
                    item.Text = valor.Trim
                    cboadi.Items.Add(item)
                Next
                cboadi.Items.Insert(0, "")

                Dim lbl As New Label
                lbl.Text = "ADICIONAL DE " & data_desc.Trim()

                Dim div As New HtmlGenericControl("div")
                div.ID = "div_adicional"
                div.Attributes.Add("class", "Ocultar " & idc_artiespeci)


                div.Controls.Add(lbl)
                'div.Controls.Add(New HtmlGenericControl("br"))
                div.Controls.Add(cboadi)
                div_acabados.Controls.Add(div)
            End If


            'Dim ul As New HtmlGenericControl("ul")
            'For Each valor As String In spl
            '    Dim li As New HtmlGenericControl("li")
            '    li.ID = valor.ToString().Trim()
            '    Dim a As New HtmlGenericControl("a")
            '    Dim img As New HtmlGenericControl("img")
            '    img.Attributes("src") = "" ' "finishes/" & row("idc_artiespeciopc").ToString().Trim() & "_chica.jpg"
            '    img.Attributes("onerror") = "this.src='';"
            '    a.Controls.Add(img)
            '    a.Attributes("onclick") = "return zoom_image(0,this,0) & );" '"alert('" & row("idc_artiespeciopc").ToString().Trim() & "');"
            '    a.Attributes("onmouseover") = "return zoom_image(0,this);" '"alert('" & row("idc_artiespeciopc").ToString().Trim() & "');"
            '    a.Attributes("onmouseout") = "return zoom_image_out();" '"alert('" & row("idc_artiespeciopc").ToString().Trim() & "');"
            '    a.Attributes.Add("data-info", row("desc2").ToString().Trim())
            '    If Not row("adicional").ToString().Trim() = "" Then
            '        adi_text = row("adicional").ToString().Trim()
            '        existe = True
            '        li.Attributes.Add("data-adi", "1")
            '    Else
            '        li.Attributes.Add("data-adi", "0")
            '    End If
            '    li.Controls.Add(a)
            '    'If cont = 1 Then
            '    '    Dim span As New HtmlGenericControl("span")
            '    '    li.Controls.Add(span)
            '    '    li.Attributes.Add("class", "selected")
            '    'End If
            '    ul.Controls.Add(li)
            'Next
        End If
    End Sub

    'Sub cargar_img_acabados(ByVal dt As DataTable)
    '    Try
    '        Dim ruta_ac As String = ""
    '        ruta_ac = uni_archi("esparti")
    '        Dim ruta_l As String = Request.PhysicalApplicationPath & "finishes\"
    '        If dt.Rows.Count > 0 Then
    '            If Not ruta_ac = "" Then



    '                For Each row As DataRow In dt.Rows
    '                    If Not File.Exists(ruta_l & row("idc_artiespeciopc").ToString().Trim() & ".jpg") Then
    '                        If File.Exists(ruta_ac & row("idc_artiespeciopc").ToString().Trim() & ".jpg") Then
    '                            File.Copy(ruta_ac & row("idc_artiespeciopc").ToString().Trim() & ".jpg", ruta_l & row("idc_artiespeciopc").ToString().Trim() & ".jpg")
    '                        End If
    '                    End If
    '                    If Not File.Exists(ruta_l & row("idc_artiespeciopc").ToString().Trim() & "_chica.jpg") Then
    '                        If File.Exists(ruta_ac & row("idc_artiespeciopc").ToString().Trim() & "_chica.jpg") Then
    '                            File.Copy(ruta_ac & row("idc_artiespeciopc").ToString().Trim() & "_chica.jpg", ruta_l & row("idc_artiespeciopc").ToString().Trim() & "_chica.jpg")
    '                        End If
    '                    End If
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Sub img_principal(ByVal idc_articulo As Integer)
        Dim ruta As String = uni_archi("ARTICU")
        Dim ruta_d As String = Request.PhysicalApplicationPath & "img_art\" & idc_articulo & ".jpg"
        imgart.Attributes("onerror") = "this.src='img_art/GaleriaNoDisponible.jpg';"
        imgart.ImageUrl = ruta & idc_articulo & ".jpg"

        'If File.Exists(ruta_d) Then
        '    imgart.ImageUrl = "img_art/" & idc_articulo & ".jpg"
        'Else
        '    If Not ruta = "" Then
        '        If File.Exists(ruta & idc_articulo & ".jpg") Then
        '            File.Copy(ruta & idc_articulo & ".jpg", ruta_d)
        '            imgart.ImageUrl = "img_art/" & idc_articulo & ".jpg"
        '        End If
        '    End If
        'End If
    End Sub


    'Sub cargar_imagen2(ByVal idc_articulo As Integer)
    '    Dim ruta As String = uni_archi("ARTICU") & idc_articulo & ".JPG"
    '    Dim ruta_d As String = Request.PhysicalApplicationPath & "img_art/" & idc_articulo & ".JPG"
    '    If File.Exists(ruta) Then
    '        If Not File.Exists(ruta_d) Then
    '            File.Copy(ruta, ruta_d)
    '            imgart.ImageUrl = "img_art/" & idc_articulo & ".jpg"
    '            'div_img.Attributes("style") = "background-image:url('img_art/" & idc_articulo & ".jpg');background-repeat: no-repeat;background-position: center top;background-size:contain;"
    '        Else
    '            'div_img.Attributes("style") = "background-image:url('img_art/" & idc_articulo & ".jpg');background-repeat: no-repeat;background-position: center top;background-size:contain;"
    '            imgart.ImageUrl = "img_art/" & idc_articulo & ".jpg"
    '        End If
    '    Else

    '        If File.Exists(ruta_d) Then
    '            'div_img.Attributes("style") = "background-image:url('img_art/" & idc_articulo & ".jpg');background-repeat: no-repeat;background-position: center top;background-size:contain;"
    '            imgart.ImageUrl = "img_art/" & idc_articulo & ".jpg"
    '        Else
    '            'div_img.Attributes("style") = "background-image:url('img_art/GaleriaNoDisponible.jpg');background-repeat: no-repeat;background-position: center top;background-size:contain;"
    '            imgart.ImageUrl = "img_art/GaleriaNoDisponible.jpg"
    '        End If

    '    End If
    'End Sub

    'Public Function uni_archi(ByVal cod As String) As String
    '    Dim sitio As New SitioCN.unidades_archivos
    '    Dim ruta As String = ""
    '    Try
    '        ruta = sitio.uni_archi_str(cod)
    '        Return ruta
    '    Catch ex As Exception
    '        Return ""
    '    End Try
    'End Function
    Public Function uni_archi(ByVal cod As String) As String
        Dim sitio As New GWebCN.Unidades
        Dim ds As DataSet
        Dim unidad As String = ""
        Try
            ds = sitio.Unidad_Archivos(cod)
            If ds.Tables(0).Rows.Count > 0 Then
                unidad = IIf(IsDBNull(ds.Tables(0).Rows(0)("ruta_web")), "", ds.Tables(0).Rows(0)("ruta_web"))
                Return unidad
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub btnguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguardar.Click
        Try
            If Not txtselected.Value = "" Then
                Dim total As Integer = 0
                Dim spl_acabados() As String = txtselected.Value.Split(";")
                total = (spl_acabados.Length - 1) / 2

                Dim idc_articulo As Integer = Request.QueryString("cdi")
                Dim precio As Decimal
                'Request.QueryString("pr")
                If Not txttotal_n.Text = "" Then
                    If txttotal_n.Text > 0 Then
                        precio = txttotal_n.Text.Replace(",", "")
                    Else
                        precio = txttotal.Text.Replace(",", "")
                    End If
                Else
                    precio = txttotal.Text.Replace(",", "")
                End If

                Dim cantidad As Decimal = 0
                If Not txtcantidad_nueva.Text = "" Then
                    If txtcantidad_nueva.Text > 0 Then
                        cantidad = txtcantidad_nueva.Text
                    End If
                Else
                    cantidad = txtcantidad.Text
                End If




                Dim edit As Boolean = IIf(Request.QueryString("ed") = 1, True, False)
                Dim rfc As Boolean = IIf(Request.QueryString("r") = 1, True, False)



                Dim dt As New DataTable
                dt = Session("dt_productos_lista")

                Dim pre_min As Decimal, factor As Decimal, pr As Decimal
                pre_min = CDec(txtpre_min.Value)
                pr = CDec(txtprecio.Value) '/ (1 + CDec(Session("xiva") / 100)))
                factor = pr / pre_min

                If (Not dt Is Nothing) Then
                    For Each row As DataRow In dt.Rows
                        If idc_articulo = row("idc_articulo") Then
                            row("num_especif") = total
                            row("especif") = txtselected.Value
                            row("costo") = txtcosto_final.Value
                            row("precio_minimo") = txtpremin_final.Value
                            row("ids_especif") = txtvcade2.Value
                            row("precio") = txttotal.Text
                            row("g_especif") = txtvcade.Value
                            row("PrecioReal") = txttotal.Text
                            row("precio_lista") = txttotal.Text
                        End If
                    Next

                End If
                Exec_JS("refrescar()")
            Else
                CargarMsgBox("Es Necesario Seleccionar Cada Una de las Opciones Disponibles.")
            End If
        Catch ex As Exception
            CargarMsgBox("Error al Agregar Articulo.\n \u000b \n" & ex.Message)
            Exec_JS("window.close();")
        End Try
    End Sub

    Sub CargarMsgBox(ByVal msj As String)
        msj = msj.Replace("'", "")
        'msj = msj.Replace("\", "\\")
        ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid().ToString(), "<script>alert('" & msj & "');</script>", False)
    End Sub

    Sub Exec_JS(ByVal script As String)
        ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid().ToString(), "<script>" & script & "</script>", False)
    End Sub

    Sub cargar_imagen(ByVal idc_articulo As Integer)

        'Imagen Tamaño Normal
        Dim ruta As String = uni_archi("ARTICU") & idc_articulo & ".JPG"
        Dim ruta_d As String = Request.PhysicalApplicationPath & "img_art/" & idc_articulo & ".JPG"

        'Imagen Tamaño Pequeño
        Dim ruta_ch As String = uni_archi("ARTICU") & idc_articulo & "_chica.JPG"
        Dim ruta_d_ch As String = Request.PhysicalApplicationPath & "img_art/" & idc_articulo & "_chica.JPG"

        Dim ul_content As String = ""
        'div_fotorama.InnerHtml = "<a href=""" & ruta & """><img src=""" & ruta & """ alt="" class=""image" & -1 & """></a>"
        div_fotorama.InnerHtml = "<img src=""" & ruta & """ alt="" class=""image" & -1 & """>"
    End Sub
End Class

