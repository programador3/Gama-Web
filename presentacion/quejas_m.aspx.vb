Imports System.Data
Imports System.IO

Partial Class queja_m
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sidc_usuario") = Nothing Then
            Response.Redirect("login.aspx")
        End If
        If Not Page.IsPostBack Then
            cargar_consecutivo()

            btnguardar.Attributes("onclick") = "position(" & txtfactura.ClientID & ");alert('Seleccione Factura.');return false;"
            'generar_html()
            For i As Integer = 0 To 12
                Dim list As New ListItem
                If i = 0 Then
                    list.Text = "00"
                    list.Value = "00"
                ElseIf i > 0 And i < 10 Then
                    list.Text = "0" & CStr(i)
                    list.Value = "0" & CStr(i)
                Else
                    list.Text = CStr(i)
                    list.Value = CStr(i)
                End If
                cbohoras.Items.Add(list)
            Next

            For i As Integer = 0 To 59
                Dim list As New ListItem
                If i = 0 Then
                    list.Text = "00"
                    list.Value = "00"
                ElseIf i > 0 And i < 10 Then
                    list.Text = "0" & CStr(i)
                    list.Value = "0" & CStr(i)
                Else
                    list.Text = CStr(i)
                    list.Value = CStr(i)
                End If
                cbominutos.Items.Add(list)
            Next

            Dim fecha As DateTimeOffset
            fecha = Now()

            If fecha.Hour > 12 Then
                Dim hora As Integer = (fecha.Hour - 12)
                If hora < 10 And hora > 0 Then
                    cbohoras.SelectedValue = ("0" & CStr(hora))
                Else
                    cbohoras.SelectedValue = hora
                End If
            Else

                If fecha.Hour < 10 Then
                    cbohoras.SelectedValue = CStr("0" & fecha.Hour)
                Else
                    cbohoras.SelectedValue = fecha.Hour
                End If
            End If

            Dim minutos As Integer = fecha.Minute
            If minutos < 10 And minutos > 0 Then
                cbominutos.SelectedValue = CStr("0" & minutos)
            Else
                cbominutos.SelectedValue = fecha.Minute
            End If

            
            If fecha.Hour > 12 Then
                cbomeridiano.SelectedValue = "PM"
            Else
                cbomeridiano.SelectedValue = "AM"
            End If


            Dim imgregresar As New ImageButton
            imgregresar = Master.FindControl("imgregresar")
            If Not imgregresar Is Nothing Then
                imgregresar.PostBackUrl = "menu_quejas.aspx"
            End If

        End If
    End Sub

    Sub cargar_consecutivo()
        Dim dt As New DataTable
        Dim gweb As New GWebCN.Quejas
        Try
            dt = gweb.folio(413)
            If dt.Rows.Count > 0 Then
                txtnum.Text = CStr(CInt(dt.Rows(0).Item("no_folio").ToString.Trim) + 1)
            Else
                txtnum.Text = 0
            End If
        Catch ex As Exception
            CargarMsgBox("Error Al Cargar Folio.\n \u000B \nError:\n" & ex.Message)
        End Try
    End Sub
    Sub CargarMsgBox(ByVal msj As String)
        msj = msj.Replace("'", "")
        presentacion.Alert.ShowAlertError(msj, Me.Page)
    End Sub

   
    Sub datos_factura(ByVal codfac As String)
        Dim ds As New DataSet
        Dim gweb As New GWebCN.Quejas
        Try
            ds = gweb.Datos_Factura(codfac)
            If ds.Tables(0).Rows.Count > 0 Then
                txtfactura.Text = ds.Tables(0).Rows(0).Item("codfac")
                txtrfc.Text = ds.Tables(0).Rows(0).Item("rfccliente")
                txtcve.Text = ds.Tables(0).Rows(0).Item("cveadi")
                cargar_productos_fact(ds.Tables(0).Rows(0).Item("idc_factura"))
                txtidc_factura.Text = ds.Tables(0).Rows(0).Item("idc_factura")
                txtcliente.Text = ds.Tables(0).Rows(0).Item("nombre")
                btnguardar.Attributes("onclick") = "return guardar();"
                imgcolonia.Attributes("onclick") = "return colonia();"
                txtfactura.Attributes("onfocus") = "this.blur();"
                txtfactura.Attributes("style") = "color:blue !important;"
            End If
        Catch ex As Exception
            CargarMsgBox("Error al Cargar Información de Factura.\n \u000B \nError:\n" & ex.Message)
        End Try

    End Sub

    Sub cargar_productos_fact(ByVal idc_factura As Integer)
        Dim ds As New DataSet
        Dim gweb As New GWebCN.Quejas
        Try
            ds = gweb.Devoluciones_Factura(idc_factura)
            If ds.Tables(0).Rows.Count > 0 Then
                griddatos.DataSource = ds.Tables(0)
                griddatos.DataBind()
            Else
                griddatos.DataSource = Nothing
                griddatos.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub griddatos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles griddatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim dc As Integer
            If e.Item.Cells(6).Text = "False" Then
                dc = 0
            Else
                dc = 2
            End If
            Dim txtcantidad As New TextBox
            txtcantidad = e.Item.FindControl("txtcantidad")
            If Not txtcantidad Is Nothing Then
                txtcantidad.Attributes("onblur") = "return cant();"
                'coment 14-01-2016 mic
                'txtcantidad.Attributes("onclick") = "window.open('teclado.aspx?ctrl=" & txtcantidad.ClientID & "&dc=" & dc & "&fn=cant');"
                'txtcantidad.Attributes("onfocus") = "this.blur();"
            End If
        End If
    End Sub

    Protected Sub btnotra_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnotra.Click
        limpiar_campos()
        cargar_consecutivo()
    End Sub

    Sub limpiar_campos()
        txtidc_colonia.Text = ""
        txtfactura.Text = ""
        txtrfc.Text = ""
        txtcve.Text = ""
        txtcliente.Text = ""
        txtproblema.Text = ""
        griddatos.DataSource = Nothing
        griddatos.DataBind()
        txtagente.Text = ""
        txttel_a.Text = ""
        txttmk.Text = ""
        txttel_t.Text = ""
        txtcomprador.Text = ""
        txttel_c.Text = ""
        txtobra.Text = ""
        txttel_o.Text = ""
        txtobs.Text = ""
        txtcolonia.Text = ""
        cbohoras.SelectedIndex = 0
        cbominutos.SelectedIndex = 0
        cbomeridiano.SelectedIndex = 0
        txtcalle.Text = ""
        txtnumero.Text = ""
        txtmun.Text = ""

        btnguardar.Attributes("onclick") = "position(" & txtfactura.ClientID & ");alert('Seleccione Factura.');return false;"
        txtfactura.Attributes.Remove("onfocus")
        txtfactura.Attributes.Remove("style")
        txtfactura.Attributes("onkeypress") = "return factura(event,this);"
        cargar_consecutivo()
    End Sub

    Protected Sub btnguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguardar.Click
        If txtproblema.Text.Trim = "" Then
            CargarMsgBox("El Problema o Queja no Puede quedar Vacia")
            Return
        End If




        '        IF thisform.edit1.Enabled = .T.
        '            If EMPTY(thisform.edit1.value) Then
        '                MESSAGEBOX("", 48, "Descripcion")
        '                thisform.edit1.SetFocus()
        '                Return
        '            End If
        '        End If


        'SELECT * FROM TX_MOVART WHERE DEVOLVER>0 INTO CURSOR C_S
        'SELECT C_S
        'COUNT TO Q
        'IF Q=0
        'MESSAGEBOX("Debes Introducir la Cantidad Dañada del Articulo",48,"Mensaje")
        'return
        'ENDIF

        If txtagente.Text = "" Then
            CargarMsgBox("El Agente no puede quedar vacio.")
            Return
        End If
        If txtagente.Text.Length < 10 Then
            CargarMsgBox("El Nombre del Agente no puede ser menor a 10 Caracteres")
            Return
        End If

        'vagente=ALLTRIM(thisform.txtagente.Value)
        'IF EMPTY(VAGENTE)
        'MESSAGEBOX("El Agente no puede quedar vacio",48,"Mensaje")
        'THISFORM.TXTagente.SetFocus  
        'return
        'ENDIF

        'IF LEN(vagente)<=10
        'MESSAGEBOX("El Nombre del Agente no puede ser menor a 10 Caracteres",48,"Mensaje")
        'THISFORM.TXTagente.SetFocus  
        'return
        'endif

        If txtcomprador.Text.Trim = "" Then
            CargarMsgBox("El Comprador no puede quedar vacio")
            Return
        End If


        'vcomprador=ALLTRIM(thisform.txtCOMPRADOR.Value)
        'IF EMPTY(VCOMPRADOR)
        'MESSAGEBOX("",48,"Mensaje")
        'THISFORM.TXTCOMPRADOR.SetFocus  
        'return
        'ENDIF

        If txtcomprador.Text.Length < 10 Then
            CargarMsgBox("El Nombre del Comprador no puede ser menor a 10 Caracteres")
            Return
        End If
        'IF LEN(vcomprador)<=10
        'MESSAGEBOX("El Nombre del Comprador no puede ser menor a 10 Caracteres",48,"Mensaje")
        'THISFORM.TXTCOMPRADOR.SetFocus  
        'return
        'endif

        If txtobra.Text.Trim = "" Then
            CargarMsgBox("El Contacto no puede quedar vacio.")
            Return
        End If
        'vcontacto=ALLTRIM(thisform.txtcontacto.Value)
        'IF EMPTY(VCONTACTO)
        'MESSAGEBOX("El Contacto no puede quedar vacio",48,"Mensaje")
        'THISFORM.TXTcontacto.SetFocus  
        'return
        'ENDIF

        If txtobra.Text.Length < 10 Then
            CargarMsgBox("El Nombre del Contacto no puede ser menor a 10 Caracteres")
            Return
        End If
        'IF LEN(vcontacto)<=10
        'MESSAGEBOX("El Nombre del Contacto no puede ser menor a 10 Caracteres",48,"Mensaje")
        'THISFORM.TXTcontacto.SetFocus  
        'return
        'endif

        If txttmk.Text.Trim = "" Then
            CargarMsgBox("El TMK no puede quedar vacio.")
            Return
        End If


        'vtmk=ALLTRIM(thisform.txttmk.Value )
        'IF EMPTY(VTMK)
        'MESSAGEBOX("El TMK no puede quedar vacio",48,"Mensaje")
        'THISFORM.TXTtmk.SetFocus  
        'return
        'ENDIF

        If txttmk.Text.Length < 10 Then
            CargarMsgBox("El Nombre del TMK no puede ser menor a 10 Caracteres.")
            Return
        End If
        'IF LEN(vtmk)<=10
        'MESSAGEBOX("El Nombre del TMK no puede ser menor a 10 Caracteres",48,"Mensaje")
        'THISFORM.TXTtmk.SetFocus  
        'return
        'endif

        If txttel_a.Text.Trim = "" Then
            CargarMsgBox("El Telefono del Agente no puede quedar vacio")
            Return
        End If
        'vtelage=ALLTRIM(thisform.txtagetel.Value)
        'IF EMPTY(Vtelage)
        'MESSAGEBOX("El Telefono del Agente no puede quedar vacio",48,"Mensaje")
        'thisform.txtagetel.SetFocus 
        'return
        'ENDIF

        If txttel_a.Text.Length < 8 Then
            CargarMsgBox("El Telefono del Agente no puede ser menor a 8 Caracteres")
            Return
        End If

        'IF LEN(vtelage)<8
        'MESSAGEBOX("El Telefono del Agente no puede ser menor a 8 Caracteres",48,"Mensaje")
        'thisform.txtagetel.SetFocus 
        'return
        'endif

        If txttel_c.Text.Trim = "" Then
            CargarMsgBox("El Telefono del Comprador no puede quedar vacio")
            Return
        End If
        'vtelcomprador=ALLTRIM(thisform.txtcompradortel.Value)
        'IF EMPTY(Vtelcomprador)
        'MESSAGEBOX("El Telefono del Comprador no puede quedar vacio",48,"Mensaje")
        'thisform.txtcompradorteL.SetFocus 
        'return
        'ENDIF

        If txttel_c.Text.Length < 8 Then
            CargarMsgBox("El Telefono del Comprador no puede ser menor a 8 Caracteres")
            Return
        End If

        'IF LEN(vtelcomprador)<8
        'MESSAGEBOX("El Telefono del Comprador no puede ser menor a 8 Caracteres",48,"Mensaje")
        'thisform.txtcompradorteL.SetFocus 
        'return
        'endif

        If txttel_o.Text.Trim = "" Then
            CargarMsgBox("El Telefono del Contacto no puede quedar vacio.")
            Return
        End If

        'vtelcontacto=alltrim(thisform.txtcontactotel.Value)
        'IF EMPTY(Vtelcontacto)
        'MESSAGEBOX("El Telefono del Contacto no puede quedar vacio",48,"Mensaje")
        'thisform.txtcontactotel.SetFocus 
        'return
        'ENDIF

        If txttel_o.Text.Length < 8 Then
            CargarMsgBox("El Telefono del Contacto no puede ser menor a 8 Caracteres")
            Return
        End If

        'IF LEN(vtelcontacto)<8
        'MESSAGEBOX("El Telefono del Contacto no puede ser menor a 8 Caracteres",48,"Mensaje")
        'thisform.txtcontactotel.SetFocus 
        'return
        'endif

        If txttmk.Text.Trim = "" Then
            CargarMsgBox("El Telefono del TMK no puede quedar vacio")
            Return
        End If

        'vteltmk=ALLTRIM(thisform.txttmktel.Value)
        'IF EMPTY(VTELTMK)
        'MESSAGEBOX("El Telefono del TMK no puede quedar vacio",48,"Mensaje")
        'thisform.txttmktel.SetFocus 
        'return
        'ENDIF


        If txttmk.Text.Length < 8 Then
            CargarMsgBox("El Telefono del TMK no puede ser menor a 8 Caracteres")
            Return
        End If
        'IF LEN(vtelTMK)<8
        'MESSAGEBOX("El Telefono del TMK no puede ser menor a 8 Caracteres",48,"Mensaje")
        'thisform.txttmktel.SetFocus 
        'return
        'endif


        'vobservaciones=ALLTRIM(thisform.edit2.Value)
        'vqueja=ALLTRIM(thisform.edit1.Value)
        'vhora=PADL(ALLTRIM(STR(HOUR(thisform.olecontrol1._Value))),2,"0")+":"+PADL(ALLTRIM(STR(MINUTE(thisform.olecontrol1._Value))),2,"0")
        'vidc_colonia=thisform.txtidc_colonia.Value

        If txtidc_colonia.Text.Trim = "" Or txtidc_colonia.Text = "0" Then
            CargarMsgBox("La Colonia no puede quedar vacia.")
            Return
        End If
        'IF vidc_colonia<0
        'MESSAGEBOX("La Colonia no puede quedar vacia",48,"Mensaje")
        'return
        'endif

        If txtcalle.Text.Trim = "" Then
            CargarMsgBox("La Calle no puede quedar vacia.")
            Return
        End If
        'vcalle=thisform.txtcalle.Value
        'IF EMPTY(vcalle)
        'MESSAGEBOX("La Calle no puede quedar vacia",48,"Mensaje")
        'THISFORM.COMmand1.Click  
        'return
        'endif

        If txtnumero.Text = "" Then
            CargarMsgBox("El numero no puede quedar vacio")
            Return
        End If
        'vnumero=thisform.txtnumero.Value
        'IF EMPTY(vnumero)
        'MESSAGEBOX("El numero no puede quedar vacio",48,"Mensaje")
        'THISFORM.TXTnumero.SetFocus  
        'return
        'endif

        Dim vhora As String = cbohoras.SelectedItem.Text & ":" & cbominutos.SelectedItem.Text & ":00 " & cbomeridiano.SelectedItem.Text



        'vidfac= thisformSET.px_idfac

        Dim vcadena As String = ""
        Dim vtotart As Integer = 0
        Dim txtcantidad As String = ""
        Dim vdevolv As String = ""
        Dim vcodigo As Decimal = 0
        Dim vcantidadc2 As Decimal = 0
        For i As Integer = 0 To griddatos.Items.Count - 1
            txtcantidad = DirectCast(griddatos.Items(i).FindControl("txtcantidad"), TextBox).Text
            If txtcantidad <> "" Then
                If txtcantidad > 0 Then
                    vtotart = vtotart + 1
                    vcodigo = griddatos.Items(i).Cells(8).Text.Trim
                    vcantidadc2 = griddatos.Items(i).Cells(9).Text.Trim
                    vdevolv = IIf(vcodigo > 0, txtcantidad / vcantidadc2, txtcantidad)
                    vcadena = vcadena & vdevolv.ToString.Trim & ";" & vcodigo.ToString.Trim & ";" & _
                    txtcantidad.ToString.Trim & ";" & griddatos.Items(i).Cells(10).Text.Trim & ";" & _
                    griddatos.Items(i).Cells(11).Text.Trim & ";"
                End If
            End If
        Next
        '*FORMAR LA CADENA
        'VCADENA = ""
        'VCANCELADO = ""
        'VTOTART = 0
        'SELECT TX_MOVART
        'GO TOP
        'DO WHILE !EOF()
        '   IF DEVOLVER > 0
        '      VTOTART = VTOTART + 1
        '      VDEVOL = IIF(CODIGO2>0,DEVOLVER/CANTIDADC2,DEVOLVER)

        '      VCADENA = VCADENA +ALLTRIM(STR(VDEVOL,14,4))+";"+ALLTRIM(STR(CODIGO2))+";"+;
        '      ALLTRIM(STR(DEVOLVER,14,4))+";"+ALLTRIM(STR(IDC_FACTURAD))+";"+;
        '      ALLTRIM(STR(IDC_PAQD))+";"

        '   ENDIF
        '   SKIP
        'ENDDO   

        Dim intentos As Integer = 0
        Dim guardado As Boolean = False
        Dim vtipom As String = "A"
        Dim vcambios As String = ""
        Dim sfolio As Integer = 0
        Dim ds As New DataSet
        Dim gweb As New GWebCN.Quejas
        Try
            ds = gweb.alta_queja(txtidc_factura.Text.Trim, txtagente.Text.Trim, txtcomprador.Text.Trim, txtobra.Text.Trim, txttmk.Text.Trim, txttel_a.Text.Trim, txttel_c.Text.Trim,
                                     txttel_o.Text.Trim, txttel_t.Text.Trim, txtobs.Text.Trim, txtproblema.Text.Trim, txtidc_colonia.Text.Trim, txtcalle.Text.Trim, txtnumero.Text.Trim, vhora,
                                     Session("idc_usuario"), Session("ip"), Session("pc"), Session("usuariopc"), vtipom, vcambios, sfolio, vcadena, vtotart)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim vmensaje As String = ds.Tables(0).Rows(0).Item("mensaje").ToString.Trim
                If vmensaje Is "" Then
                    sfolio = ds.Tables(0).Rows(0).Item("Folio").ToString.Trim
                    presentacion.Alert.ShowAlert("Se Registro la Queja con Exito.\n \u000B \nQueja No. " & sfolio, "Mensaje del Sistema", Me.Page)
                    limpiar_campos()
                Else
                    CargarMsgBox(vmensaje)
                End If
            End If
        Catch ex As Exception
            CargarMsgBox(ex.Message & "\n T\u000B \nIntenta mas Tarde.")
        End Try

    End Sub


    Sub generar_html()
        Dim dt As New DataTable
        dt.Columns.Add("idc_queja")
        Dim ro As DataRow
        ro = dt.NewRow
        ro(0) = 356

        dt.Rows.Add(ro)
        Dim sb As StringBuilder = New StringBuilder()

        sb.AppendLine("<html>")
        sb.AppendLine("<head>")
        sb.AppendLine("<title>Gama Materiales y Aceros</title>")
        sb.AppendLine("<meta http-equiv='X-UA-Compatible' content='IE=edge' />")
        sb.AppendLine("<meta http-equiv='Content-Language' content='Spanish'/>")
        sb.AppendLine("<style type='text/css'>")
        sb.AppendLine("input[type='text']")
        sb.AppendLine("{")
        sb.AppendLine("border: solid 1px black;")
        sb.AppendLine("height:30px;")
        sb.AppendLine("font-size:medium;")
        sb.AppendLine("font-family:Arial;")
        sb.AppendLine("font-weight:normal;")
        sb.AppendLine("border-radius:3px;")
        sb.AppendLine("}")
        sb.AppendLine("#TextArea1")
        sb.AppendLine("{")
        sb.AppendLine("border: solid 1px black;")
        sb.AppendLine("font-size:medium;")
        sb.AppendLine("font-family:Arial;")
        sb.AppendLine("font-weight:normal;")
        sb.AppendLine("width:100%;")
        sb.AppendLine("border-radius:3px;")
        sb.AppendLine("resize:none;")
        sb.AppendLine("}")
        sb.AppendLine("</style>")
        sb.AppendLine("</head>")
        sb.AppendLine("<body style='font-family:Arial;'>")
        sb.AppendLine("<div style='width:100%;position:relative;top:0px;left:0px;'>")
        sb.AppendLine("<table style='width: 100%;'>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td>")
        sb.AppendLine("<table style='width:100%;'>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='width:15%;'>")
        sb.AppendLine("<img alt='' style='width:100%;' src='data:image/jpeg;base64,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' />")
        sb.AppendLine("</td>")
        sb.AppendLine("<td style='width:70%;' align='center' valign='middle' >")
        sb.AppendLine("<label style='font-size:medium;font-weight:bold;'>Gama Materiales y Aceros, S.A. de C.V.</label><br />")
        sb.AppendLine("<label style='font-size:small;'>Informe de Quejas Pendientes</label>")
        sb.AppendLine("</td>")
        sb.AppendLine("<td style='width:15%;' valign='middle' align='center'>")
        sb.AppendLine("<label style='font-size:small;'>" & Now() & "</label>")
        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")
        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td>")
        sb.AppendLine("<table style='width:100%;'>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Num.</label></td>")
        sb.AppendLine("<td style='width:35%' align='left'>")
        sb.AppendLine("<input id='Text1' style='width: 100%;' type='text' value='" & dt.Rows(0).Item("idc_queja") & "' /></td>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Fecha:</label>")
        sb.AppendLine("</td>")
        sb.AppendLine("<td style='width:35%' align='left'>")
        sb.AppendLine("<input id='Text4' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Cliente:</label></td>")
        sb.AppendLine("<td style='width:35%' align='left'>")
        sb.AppendLine("<input id='Text2' style='width:100%;' type='text' /></td>")


        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>RFC:</label></td>")
        sb.AppendLine("<td style='width:35%' align='left'>")
        sb.AppendLine("<input id='Text5' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Cve.:</label></td>")
        sb.AppendLine("<td style='width:35%' align='left'>")
        sb.AppendLine("<input id='Text3' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Factura:</label></td>")
        sb.AppendLine("<td style='width:35%' align='left'>")
        sb.AppendLine("<input id='Text6' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")
        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td>")
        sb.AppendLine("<table style='width:100%;'>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='width:15%;' align='right' valign='top'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Problema:</label>")
        sb.AppendLine("</td>")
        sb.AppendLine("<td style='width:85%;'>")
        sb.AppendLine("<textarea id='TextArea1' name='S1' rows='2' style='width:100%' cols='20'>")
        sb.AppendLine("</textarea>")
        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")
        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='padding-left:5%;padding-top:15px;'>")

        sb.AppendLine("<label style='font-size:small;font-style:italic;'>Artilculos Da&ntilde;ados</label></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td align='right'>")

        sb.AppendLine("<table style='width:90%;font-weight:bold;'>")
        sb.AppendLine("<tr style='background-color:gainsboro;'>")
        sb.AppendLine("<td align='center'>")
        sb.AppendLine("<label style='font-size:small;width:20%;'>CODIGO</label></td>")
        sb.AppendLine("<td align='center'>")
        sb.AppendLine("<label style'font-size:small;width:20%;'>DA&Ntilde;ADO</label></td>")
        sb.AppendLine("<td align='center'>")
        sb.AppendLine("<label style='font-size:small;width:20%;'>U.M.</label></td>")
        sb.AppendLine("<td align='left'>")
        sb.AppendLine("<label style='font-size:small;width:40%;'>DESCRIPCION</label></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")

        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td>")

        sb.AppendLine("<table style='width:100%;'>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td align='right' style='width: 15%'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Agente:</label></td>")
        sb.AppendLine("<td>")
        sb.AppendLine("<input id='Text7' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("<td align='right' style='width: 15%'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Tel.</label></td>")
        sb.AppendLine("<td style='width: 20%'>")
        sb.AppendLine("<input id='Text15' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td align='right' style='width: 15%'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Tmk:</label></td>")
        sb.AppendLine("<td>")
        sb.AppendLine("<input id='Text8' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("<td align='right' style='width: 15%'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Tel.</label></td>")
        sb.AppendLine("<td style='width: 20%'>")
        sb.AppendLine("<input id='Text14' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td align='right' style='width: 15%'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Comprador:</label></td>")
        sb.AppendLine("<td>")
        sb.AppendLine("<input id='Text9' style='width: 100%; 'type='text' /></td>")
        sb.AppendLine("<td align='right' style='width: 15%;'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Tel.</label></td>")
        sb.AppendLine("<td style='width: 20%'>")
        sb.AppendLine("<input id='Text13' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td align='right' style='width: 15%'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Contacto:</label></td>")
        sb.AppendLine("<td>")
        sb.AppendLine("<input id='Text10' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("<td align='right' style='width: 15%;'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Tel.</label></td>")
        sb.AppendLine("<td style='width: 20%'>")
        sb.AppendLine("<input id='Text12' style='width: 100%;' type='text'/></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td align='right' style='width: 15%'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Hora Visita:</label></td>")
        sb.AppendLine("<td>")
        sb.AppendLine("<input id='Text11' style='width: 100%;' type='text'/></td>")
        sb.AppendLine("<td align='right' style='width: 15%'>")
        sb.AppendLine("</td>")
        sb.AppendLine("<td style='width: 20%'>")
        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")
        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td>")

        sb.AppendLine("<table style='width:100%;margin-top:15px;'>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='width:15%' align='right' valign='top'>")

        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>Observaciones:</label></td>")
        sb.AppendLine("<td style='width:85%'>")

        sb.AppendLine("<input id='Text16' style='width: 100%; margin-bottom:15px;' type='text' /></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")
        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='padding-left:5%;padding-top:15px;'>")

        sb.AppendLine("<label style='font-size:small;font-style:italic;'>Direccion (Ubicacion del Material)</label></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td>")

        sb.AppendLine("<table style='width:100%;'>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>")
        sb.AppendLine("Calle:</label></td>")
        sb.AppendLine("<td style='width:35%'>")
        sb.AppendLine("<input id='Text17' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'> Num.:</label></td>")

        sb.AppendLine("<td style='width:35%'>")
        sb.AppendLine("<input id='Text20' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>")
        sb.AppendLine("Colonia:</label></td>")
        sb.AppendLine("<td style='width:35%'>")
        sb.AppendLine("<input id='Text18' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>")
        sb.AppendLine("Municipio:</label></td>")
        sb.AppendLine("<td style='width:35%'>")
        sb.AppendLine("<input id='Text21' style='width:100%;' type='text' /></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='width:15%;padding-top:15px;' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>")
        sb.AppendLine("Fecha Factura:</label></td>")
        sb.AppendLine("<td style='width:35%;padding-top:15px;'>")
        sb.AppendLine("<input id='Text19' style='width:100%;' type='text' /></td>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("</td>")
        sb.AppendLine("<td style='width:35%'>")
        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>")
        sb.AppendLine("Fecha Regreso:</label></td>")
        sb.AppendLine("<td style='width:35%'>")
        sb.AppendLine("<input id='Text22' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("</td>")
        sb.AppendLine("<td style='width:35%'>")
        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>")
        sb.AppendLine("No. Pedido:</label></td>")
        sb.AppendLine("<td style='width:35%'>")
        sb.AppendLine("<input id='Text23' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("<td style='width:15%' align='right'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>")
        sb.AppendLine("O.C.:</label></td>")
        sb.AppendLine("<td style='width:35%'>")
        sb.AppendLine("<input id='Text24' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")
        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td style='padding-top:15px;padding-left:5%;'>")

        sb.AppendLine("<label style='font-size:small;font-weight:bold;color:Gray'>")
        sb.AppendLine("Datos de Chofer y Camion</label></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td>")


        sb.AppendLine("<table style='width:100%;'>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td align='right' style='width: 15%'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>")
        sb.AppendLine("No. Nomina:</label></td>")
        sb.AppendLine("<td>")
        sb.AppendLine("<input id='Text25' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("<td align='right' style='width: 15%'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold'>")
        sb.AppendLine("Num. Economico:</label></td>")
        sb.AppendLine("<td>")
        sb.AppendLine("<input id='Text26' style='width: 100%;' type='text' /></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")

        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td>")

        sb.AppendLine("<table style='width:100%;'>")
        sb.AppendLine("<tr>")
        sb.AppendLine("<td align='right' style='width: 15%'>")

        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>")
        sb.AppendLine("<label style='font-size:small;font-weight:bold;'>")
        sb.AppendLine("Nombre</label></label></td>")
        sb.AppendLine("<td>")

        sb.AppendLine("<label style='font-size:small;font-weight:bold;color:Gray;'>")
        sb.AppendLine("<input id='Text27' style='width: 100%;' type='text' /></label></td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")
        sb.AppendLine("</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")
        sb.AppendLine("</div>")

        sb.AppendLine("</body>")
        sb.AppendLine("</html>")


        Using outfile As New StreamWriter("c:\hallazgos" & "\AllTxtFiles.html")
            outfile.Write(sb.ToString())
        End Using
    End Sub

    Protected Sub btndireccion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndireccion.Click
        If Not txtidc_factura.Text = "" And Not txtidc_factura.Text = "0" Then
            Dim gweb As New GWebCN.Quejas
            Dim ds As New DataSet
            Try
                ds = gweb.direccion_material(txtidc_factura.Text.Trim)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtcolonia.Text = ds.Tables(0).Rows(0).Item("nombre").ToString.Trim
                    txtcalle.Text = ds.Tables(0).Rows(0).Item("calle").ToString.Trim
                    txtnumero.Text = ds.Tables(0).Rows(0).Item("numero").ToString.Trim
                    txtmun.Text = ds.Tables(0).Rows(0).Item("municipio").ToString.Trim
                    txtedo.Text = ds.Tables(0).Rows(0).Item("estado").ToString.Trim
                    txtpais.Text = ds.Tables(0).Rows(0).Item("pais").ToString.Trim
                    txtidc_colonia.Text = ds.Tables(0).Rows(0).Item("idc_colonia").ToString.Trim
                End If
            Catch ex As Exception
                CargarMsgBox("Error al Cargar Direccion.\n \u000B \nError:\n" & ex.Message)

            End Try
        Else
            Return
        End If
        ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid.ToString(), "<script>position(" & txtcalle.ClientID & ");</script>", False)
    End Sub





    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        If txtfactura.Text <> "" Then
            datos_factura(txtfactura.Text.Trim)
        End If
    End Sub
    Protected Sub btnsalir_Click(sender As Object, e As EventArgs) Handles btnsalir.Click
        Response.Redirect("menu.aspx")
    End Sub
End Class
