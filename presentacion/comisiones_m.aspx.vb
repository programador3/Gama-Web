Imports System.Web.UI.DataVisualization.Charting
Imports System.Drawing
Imports System.Data
Imports presentacion
Imports System.Globalization
Imports negocio.Componentes

Partial Class comisiones_m
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sidc_usuario") = Nothing Then
            Response.Redirect("login.aspx")
        End If
        If Not Page.IsPostBack Then
            Try
                botones_mes()
                cargar_combo_agentes_usuario()
            Catch ex As Exception
                CargarMsgBox("Error al Cargar Información.\n \u000b \nError:\n" & ex.Message)
            End Try

            h_mes.Value = ""
            btnver.Attributes("onclick") = "return ver_info();"
            btnmes.Attributes("onclick") = "return ver_info();"
            'add 14-10-2015 MIC
            btn_esparticulo.Attributes("onclick") = "window.open('comisiones_esp_articulos.aspx');return false;"
            btn_espactivacion.Attributes("onclick") = "window.open('comisiones_esp_activaciones.aspx');return false;"
            btncomisiones.Visible = False
        End If
    End Sub

    Private Function AplicaBono(ByVal mes As Integer, ByVal año As Integer, ByVal idc_usuario As Integer) As Boolean
        Try
            Dim componente As New AgentesCOM
            Dim ds As New DataSet
            ds = componente.sp_detalle_comision_presupuesto(idc_usuario, año, mes)
            Dim dt As New DataTable
            dt = ds.Tables(0)
            If (dt.Rows.Count = 0) Then
                btncomisiones.CommandName = ""
                Return False
            Else
                Dim alcanzo_bono As Boolean = Convert.ToBoolean(dt.Rows(0)("alcanzo_bono"))
                txtnumagente.Text = idc_usuario.ToString()
                txtpresupuesto.Text = IIf(True, Convert.ToDecimal(dt.Rows(0)("presupuesto")).ToString("C"), "$ 0.00")
                txtventa_modal.Text = IIf(True, Convert.ToDecimal(dt.Rows(0)("venta")).ToString("C"), "$ 0.00")
                txtbono_presupuesto.Text = IIf(True, Convert.ToDecimal(dt.Rows(0)("bono_presupuesto")).ToString("C"), "$ 0.00")
                Dim bcolor As String = IIF(alcanzo_bono, "#00897b", "#e53935")
                Dim url As String = "bono_presupuesto.aspx?color=" + funciones.deTextoa64(bcolor) + "&agente=" + funciones.deTextoa64(txtnumagente.Text) + "&presupuesto=" + funciones.deTextoa64(txtpresupuesto.Text) + "&venta=" + funciones.deTextoa64(txtventa_modal.Text) + "&bono=" + funciones.deTextoa64(txtbono_presupuesto.Text)
                btncomisiones.CommandName = url
                Return True
            End If
        Catch ex As Exception
            Alert.ShowAlertError(ex.ToString(), Me)
            Return False
        End Try
    End Function


    Sub botones_mes()
        Dim mes As String
        mes = MonthName(Now.Month)
        mes = funciones.NombreMes(mes)
        mes = mes.Substring(0, 1).ToUpper & mes.Substring(1, mes.Length - 1)
        btnver.Text = mes & " " & Now.Year

        Dim month_ant As Integer = Now.Month - 1
        Dim year As Integer = Now.Year
        If month_ant <= 0 Then
            month_ant = 12
            year = year - 1
        End If
        mes = MonthName(month_ant)
        mes = funciones.NombreMes(mes)
        mes = mes.Substring(0, 1).ToUpper & mes.Substring(1, mes.Length - 1)
        btnmes.Text = mes & " " & year

    End Sub

    Sub color_control(ByVal txt As TextBox, ByVal color As Drawing.Color)
        txt.BackColor = color
        txt.ForeColor = Drawing.Color.White
    End Sub

    Sub CargarMsgBox(ByVal msj As String)
        Alert.ShowAlertError(msj.Replace("'", ""), Me.Page)
    End Sub

    'Sub bonos()
    '    Dim venta As Decimal = txtventa.Text
    '    Dim margen As Decimal = txtmargen.Text
    '    If venta > 0 And margen > 0 Then
    '        If margen > 12 Then
    '            color_control(txtmargenb1, Color.Green)
    '        Else
    '            color_control(txtmargenb1, Color.Red)
    '        End If

    '        If venta > 1200000 Then
    '            color_control(txtventasb1, Color.Green)
    '            color_control(txtventasb2, Color.Green)
    '        Else
    '            color_control(txtventasb1, Color.Red)
    '            color_control(txtventasb2, Color.Red)
    '        End If

    '        If margen > 15 Then
    '            color_control(txtmargenb2, Color.Green)
    '        Else
    '            color_control(txtmargenb2, Color.Red)
    '        End If

    '        If margen >= 12 And venta >= 1200000 Then
    '            imgb1.Visible = True
    '        Else
    '            imgb1.Visible = False
    '        End If

    '        If margen >= 15 And venta >= 1200000 Then
    '            imgb2.Visible = True
    '        Else
    '            imgb2.Visible = False
    '        End If


    '    Else


    '        If Not txtventast.Text = "" Then

    '            If txtventast.Text > 1200000 Then
    '                color_control(txtventasb1, Color.Green)
    '                color_control(txtventasb2, Color.Green)
    '            Else
    '                color_control(txtventasb1, Color.Red)
    '                color_control(txtventasb2, Color.Red)
    '            End If

    '            If txtmargent.Text > 12 Then
    '                color_control(txtmargenb1, Color.Green)
    '            Else
    '                color_control(txtmargenb1, Color.Red)
    '            End If

    '            If txtmargent.Text > 15 Then
    '                color_control(txtmargenb2, Color.Green)
    '            Else
    '                color_control(txtmargenb2, Color.Red)
    '            End If



    '            If txtventast.Text > 1200000 And txtmargent.Text > 12 Then
    '                imgb1.Visible = True
    '            Else
    '                imgb1.Visible = False
    '            End If

    '            If txtventast.Text > 1200000 And txtmargent.Text > 15 Then
    '                imgb2.Visible = True
    '            Else
    '                imgb2.Visible = False
    '            End If
    '        End If

    '    End If


    'End Sub


    Sub bonos()
        Dim venta As Decimal = txtventa.Text
        Dim margen As Decimal = txtmargen_r.Text
        If venta > 0 And margen > 0 Then
            If venta >= 1200000 And margen > 12 Then
                bono1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini green"
                txtcaab1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini green"
                txtventa1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_green f_green"
                txtcom1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_green f_green"
            Else
                If margen > 12 Then
                    txtcom1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_green f_green"
                Else
                    txtcom1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_red f_red"
                End If
                If venta > 1200000 Then
                    txtventa1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_green f_green"
                Else
                    txtventa1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_red f_red"
                End If
                bono1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini red"
                txtcaab1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini red"
            End If



            If venta >= 1200000 And margen > 15 Then
                bono2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini green"
                txtcaab2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini green"
                txtventa2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_green f_green"
                txtcom2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_green f_green"
            Else
                If margen > 15 Then
                    txtcom2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_green f_green"
                Else
                    txtcom2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_red f_red"
                End If

                If venta > 1200000 Then
                    txtventa2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_green f_green"
                Else
                    txtventa2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_red f_red"
                End If
                bono2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini red"
                txtcaab2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini red"


            End If
        Else
            bono1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini red"
            bono2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini red"
            txtcaab2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini red"
            txtcaab1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini red"
            txtventa2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_red f_red"
            txtcom2.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_red f_red"
            txtventa1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_red f_red"
            txtcom1.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_red f_red"
        End If


    End Sub

    Sub cargar_grafica(ByVal idc_agente As Integer, ByVal actual As Boolean)

        Dim gweb As New GWebCN.Agentes
        Dim ds As New DataSet
        Dim aportacion, totalgastos, aportacion12, margen As Double
        Dim dv_rows As New DataView
        Dim year As Integer = Now.Year
        Dim month As Integer = Now.Month
        If actual = False Then
            month = month - 1
            If month <= 0 Then
                month = 12
                year = year - 1
            End If
        End If
        Try
            ds = gweb.comisiones_agente(month, year, idc_agente, actual)
            btncomisiones.Visible = AplicaBono(month, year, idc_agente)
            '31-08-2015
            Dim VQUITADIF As Boolean
            VQUITADIF = False

            If (month >= 9 And year = 2015) Or (year >= 2016) Then
                VQUITADIF = True
            End If
            '31-08-2015 hasta aqui

            If ds.Tables(0).Rows.Count > 0 Then
                dv_rows = ds.Tables(0).DefaultView
                dv_rows.RowFilter = "idc_agente=" & cboagente.SelectedValue
                aportacion12 = dv_rows(0).Item("aportacion12")
                aportacion = dv_rows(0).Item("aportacion")
                txtventa.Text = FormatNumber(dv_rows(0).Item("venta").ToString.Trim, 2)
                txtventa1.Text = txtventa.Text
                txtventa2.Text = txtventa.Text
                totalgastos = dv_rows(0).Item("totalgastos")  'calcular_gastos()
                'txtmargen.Text = FormatNumber(ds.Tables(0).Rows(0).Item("margen").ToString.Trim, 2)
                'txtmargent.Text = FormatNumber(ds.Tables(0).Rows(0).Item("margen").ToString.Trim, 2)
                margen = dv_rows(0).Item("margen").ToString.Trim
                txtmargen_r.Text = margen
                'text_aportacion(margen)
                bono1.Text = FormatNumber(dv_rows(0).Item("bono1"), 2)
                bono2.Text = FormatNumber(dv_rows(0).Item("bono2"), 2)



                If margen >= 12 Then
                    txtdiferencia.Text = FormatNumber(aportacion12 - dv_rows(0).Item("totalgastos").ToString.Trim, 2)
                    'txtmargen.Text = FormatNumber((aportacion12 / ds.Tables(0).Rows(0).Item("venta").ToString.Trim) * 100, 4)
                    iconos_aportaciones(aportacion12, dv_rows(0).Item("venta").ToString.Trim)
                    'txtmargent.Text = txtmargen.Text
                    txtaportacion.Text = FormatNumber(aportacion12, 2)
                    txtapo.Text = FormatNumber(aportacion12 - aportacion, 2)
                    Container_com.Visible = False
                    txtapo.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini green b_green"
                    txtdif_apo.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini green b_green"
                Else
                    txtdiferencia.Text = FormatNumber(aportacion - dv_rows(0).Item("totalgastos").ToString.Trim, 2)
                    'txtmargen.Text = FormatNumber((aportacion / ds.Tables(0).Rows(0).Item("venta").ToString.Trim) * 100, 4)
                    iconos_aportaciones(aportacion, dv_rows(0).Item("venta").ToString.Trim)
                    'txtmargent.Text = txtmargen.Text
                    txtaportacion.Text = FormatNumber(aportacion, 2)
                    txtapo.Text = FormatNumber(aportacion12 - aportacion, 2)
                    Container_com.Visible = True
                    txtapo.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini red b_red"
                    txtdif_apo.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini red b_red"
                End If

                '31-08-2015
                If VQUITADIF Then
                    txtdiferencia.Text = dv_rows(0).Item("comision")
                    txtdiferencia.Text = FormatNumber(dv_rows(0).Item("comision"), 2)
                End If
                'hasta aqui 31-08-2015

                txtdif_apo.Text = FormatNumber(CDbl(txtapo.Text) + CDbl(txtdiferencia.Text.Trim), 2)
                'txtaportacion.Text = FormatNumber(aportacion, 2)
                txttotalgastos.Text = FormatNumber(totalgastos, 2)
                'txtaportacion_bono.Text = FormatNumber(aportacion12, 2)




                'txtdiferencia.Text = FormatNumber(ds.Tables(0).Rows(0).Item("comision_x_margen").ToString.Trim, 2)
                If (txtdiferencia.Text < 0) Then
                    txtdiferencia.ForeColor = Color.Red
                    txtdiferencia.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_red"
                Else
                    txtdiferencia.ForeColor = Color.Green
                    txtdiferencia.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_green"
                End If
                grid(ds.Tables(1), margen)
            Else
                aportacion = 0
                totalgastos = 0
                txtventa.Text = FormatNumber(0, 2)
                txtventa1.Text = FormatNumber(0, 2)
                txtventa2.Text = FormatNumber(0, 2)
                txtmargen.Text = FormatNumber(0, 2)
                'txtmargent.Text = FormatNumber(0, 2)
                txtaportacion.Text = FormatNumber(aportacion, 2)
                txttotalgastos.Text = FormatNumber(totalgastos, 2)
                txtdif_apo.Text = FormatNumber(0, 2)
                Dim diferencia As Decimal
                diferencia = aportacion - totalgastos
                txtdiferencia.Text = FormatNumber(diferencia, 2)
                If (diferencia < 0) Then
                    txtdiferencia.ForeColor = Color.Red
                Else
                    txtdiferencia.ForeColor = Color.Green
                End If
                'txtventast.Text = FormatNumber(0, 2)
                'txtmargent.Text = FormatNumber(0, 2)
                bono1.Text = FormatNumber(0, 2)
                bono2.Text = FormatNumber(0, 2)
                txtmargen_r.Text = 0
                txtapo.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini red b_red"
                txtapo.Text = FormatNumber(0, 2)

                txtdiferencia.Text = FormatNumber(0, 2)
                txtdiferencia.ForeColor = Color.Red
                txtdiferencia.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini b_red"
            End If
            txtcom1.Text = txtmargen.Text
            txtcom2.Text = txtmargen.Text
            txtcaab1.Text = IIf(txtmargen_r.Text > 12, CDbl(txtdiferencia.Text.Trim), CDbl(txtdif_apo.Text.Trim)) + CDbl(bono1.Text)
            txtcaab1.Text = FormatNumber((txtcaab1.Text), 2)
            txtcaab2.Text = FormatNumber(CDbl(txtcaab1.Text) + CDbl(bono2.Text), 2)
            grafica(IIf(margen >= 12, aportacion12, aportacion), totalgastos)

            '31-08-2015
            If VQUITADIF Then
                txtcaab2.Text = ""
            End If
            'hasta aqui 31-08-2015

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub grid(ByVal dt_det As DataTable, ByVal vmargen As Double)
        'Dim apo_x_factura As Object
        If dt_det.Rows.Count > 0 Then
            dt_det.Columns.Add("comi", GetType(Decimal))
            dt_det.Columns.Add("apo", GetType(Decimal))
            dt_det.Columns.Add("pedscg", GetType(String))


            Dim dv As New DataView
            dv = dt_det.DefaultView

            For i As Integer = 0 To dv.ToTable.Rows.Count - 1
                'If dv(i)("tipod") = 1 Then
                '    dv(i)("codfac") = dv(i)("codfac")
                'Else
                '    dv(i)("codfac") = dv(i)("idc_factura")
                'End If
                If vmargen >= 12 Then
                    dv(i)("comi") = dv(i)("comifin")
                Else
                    dv(i)("comi") = dv(i)("comiini")
                End If

                If vmargen >= 12 Then
                    dv(i)("apo") = dv(i)("apofin")
                Else
                    dv(i)("apo") = dv(i)("apoini")
                End If
            Next


            Dim dt_distinct As New DataTable

            Dim venta As Object = dv.ToTable.Compute("SUM(Venta)", "venta>=0")
            Dim apo As Object = dv.ToTable.Compute("SUM(apo)", "apo>=0")

            dt_distinct = dv.ToTable(True, "idc_factura", "codfac", "directa")
            dt_distinct.Columns.Add("tipod", GetType(Integer))
            dt_distinct.Columns.Add("venta", GetType(Decimal))
            dt_distinct.Columns.Add("apo", GetType(Decimal))
            'dt_distinct.Columns.Add("directa", GetType(Boolean))
            dt_distinct.Columns.Add("pedscg", GetType(String))


            Dim index As Integer
            dv.Sort = "idc_factura"
            For i As Integer = 0 To dt_distinct.Rows.Count - 1
                dt_distinct.Rows(i)("venta") = dv.ToTable.Compute("SUM(Venta)", "idc_factura=" & dt_distinct.Rows(i)("idc_factura"))
                dt_distinct.Rows(i)("apo") = dv.ToTable.Compute("SUM(apo)", "idc_factura=" & dt_distinct.Rows(i)("idc_factura"))
                index = dv.Find(dt_distinct.Rows(i)("idc_factura"))

                If index >= 0 Then
                    dt_distinct.Rows(i)("tipod") = dv(index)("tipod")
                End If
                dt_distinct.Rows(i)("pedscg") = IIf(dt_distinct.Rows(i)("tipod") <> 1, dt_distinct.Rows(i)("idc_factura"), "")
            Next

            dt_det = dv.ToTable(True, "idc_factura", "tipod", "codfac", "venta", "apo", "desart", "cantidad", "comi", "directa")
            dt_distinct.Columns.Add("tot_ven", GetType(Decimal), venta)
            dt_distinct.Columns.Add("tot_apo", GetType(Decimal), apo)
            dv = dt_det.DefaultView

            Dim v_directa As Decimal = IIf(IsDBNull(dv.ToTable.Compute("SUM(Venta)", "directa=true")), 0, dv.ToTable.Compute("SUM(Venta)", "directa=true"))
            Dim v_compartida As Decimal = IIf(IsDBNull(dv.ToTable.Compute("SUM(Venta)", "directa=false")), 0, dv.ToTable.Compute("SUM(Venta)", "directa=false"))

            Dim a_directa As Decimal = IIf(IsDBNull(dv.ToTable.Compute("SUM(apo)", "directa=true")), 0, dv.ToTable.Compute("SUM(apo)", "directa=true"))
            Dim a_compartida As Decimal = IIf(IsDBNull(dv.ToTable.Compute("SUM(apo)", "directa=false")), 0, dv.ToTable.Compute("SUM(apo)", "directa=false"))

            txtventa_c_h.Value = v_compartida.ToString("#,0.0000")
            txtventa_d_h.Value = v_directa.ToString("#,0.0000")
            txtcomision_c_h.Value = If((v_compartida > 0), ((a_compartida / v_compartida) * 100).ToString("0.0000"), "0.0000")
            txtcomision_d_h.Value = If((v_directa > 0), ((a_directa / v_directa) * 100).ToString("0.0000"), "0.0000")

            txtaportacion_d.Text = FormatNumber(a_directa, 2)
            txtaportacion_c.Text = FormatNumber(a_compartida, 2)

            txtventa_c.Text = FormatNumber(v_compartida, 2)
            txtventa_d.Text = FormatNumber(v_directa, 2)

            'Se quito FormatNumber
            txtcomequi.Text = Fix(((((a_compartida / 0.7) + a_directa) / (v_compartida + v_directa)) * 100) * 10000) / 10000


            If v_compartida > 0 Then
                txtcomision_c.Text = FormatNumber((a_compartida / v_compartida) * 100, 4)
            Else
                txtcomision_c.Text = FormatNumber(0, 4)
            End If

            If v_directa > 0 Then
                txtcomision_d.Text = FormatNumber((a_directa / v_directa) * 100, 4)
            Else
                txtcomision_d.Text = FormatNumber(0, 4)
            End If



            Dim dv_distinct As New DataView
            dv_distinct = dt_distinct.DefaultView
            dv_distinct.Sort = "idc_factura,tipod,codfac"

            dv.Sort = "idc_factura,tipod,codfac"


            Session("dv_com") = dv.ToTable
            Session("ds") = dv_distinct.ToTable
            grid1.DataSource = dv_distinct
            grid1.DataBind()

            gridv1.DataSource = dv_distinct
            gridv1.DataBind()
            gridv1.HeaderRow.TableSection = TableRowSection.TableHeader


        End If

    End Sub

    Sub iconos_aportaciones(ByVal aportacion As Double, ByVal venta As Double)
        Dim margen As Double
        If aportacion > 0 Or venta > 0 Then
            margen = (aportacion / venta) * 100
            margen = (Fix(margen * 10000)) / 10000
            txtmargen.Text = margen
        Else
            txtmargen.Text = FormatNumber(0, 4)
        End If

    End Sub

    Sub text_aportacion(ByVal margen As Double)
        If margen >= 12 Then
            txtaportacion.ForeColor = Color.Red
            'txtaportacion_bono.ForeColor = Color.Green
            'txtaportacion_bono.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini green"
            txtaportacion.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini"
        Else
            txtaportacion.ForeColor = Color.Green
            'txtaportacion_bono.ForeColor = Color.Red
            txtaportacion.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini green"
            'txtaportacion_bono.CssClass = "ui-input-text ui-body-c ui-corner-all ui-shadow-inset ui-mini red"
        End If
    End Sub



    Sub diferencia()
        If txtdiferencia.Text.Trim > 0 Then
            Dim margen As Decimal = txtmargen.Text
            Select Case margen
                Case Is >= 12
                    txtdiferencia.Text = CStr(CDec(txtdiferencia.Text * 1))
                Case Is >= 10
                    txtdiferencia.Text = CStr(CDec(txtdiferencia.Text * 0.75))
                Case Is >= 8
                    txtdiferencia.Text = CStr(CDec(txtdiferencia.Text * 0.5))
                Case Is >= 6
                    txtdiferencia.Text = CStr(CDec(txtdiferencia.Text * 0.25))
                Case Is < 6
                    txtdiferencia.Text = CStr(CDec(txtdiferencia.Text * 0))
            End Select
            txtaportacion.Text = FormatNumber(CDec(txttotalgastos.Text) + CDec(txtdiferencia.Text), 2)
        End If
        txtdiferencia.Text = FormatNumber(txtdiferencia.Text, 2)
    End Sub

    Sub grafica(ByVal aportacion As Double, ByVal totalgastos As Double)

        Chart1.Series(0).Points.Clear()

        Chart1.Series("Series1").ChartType = SeriesChartType.Column
        Chart1.Titles(0).Text = (cboagente.SelectedValue)


        'Chart1.ChartAreas("ChartArea1").AlignmentOrientation = AreaAlignmentOrientations.Vertical


        'Show data points labels

        'Chart1.Series("Series1").IsValueShownAsLabel = True
        'Chart1.Series("Ventas").IsValueShownAsLabel = True
        'Chart1.Series(0).LabelFormat = "{$#,###.##}"
        Chart1.Series(0).Points.AddY(CDec(aportacion))
        Chart1.Series(0).Points.AddY(CDec(totalgastos))
        'Chart1.Series(0).Points.AddY(CDec(aportacion2))
        Chart1.Series(0).Font = New Font("Arial", 0.2, FontStyle.Regular)
        Chart1.Series(0).Points.Item(0).AxisLabel = "Aportacion" & vbCrLf & CStr(FormatCurrency(aportacion, 2))
        Chart1.Series(0).Points.Item(1).AxisLabel = "Anticipo de Sueldos " & vbCrLf & " y Gastos " & vbCrLf & CStr(FormatCurrency(totalgastos, 2))

        Chart1.Series(0).Font = New Font("Arial", 0.5, FontStyle.Regular)
        'Chart1.Series(0).Points.Item(2).AxisLabel = "Aportacion + Bono" & vbCrLf & CStr(FormatCurrency(aportacion2, 2))
        'Chart1.Series(0).Points(0).Name = "Ventas"
        'Chart1.Series(1).Points(0).Name = "Gastos"
        Chart1.Series(0).Points(0).Color = Color.Green
        Chart1.Series(0).Points(1).Color = Color.Red
        'Chart1.Series(0).Points(2).Color = Color.Yellow


        'Chart1.Series(0).EmptyPointStyle.AxisLabel = "$0.00"

        If Chart1.Series(0).Points(0).YValues(0) = 0 Then
            Chart1.Series(0).Points(0).Label = "$ 0"
        End If


        If Chart1.Series(0).Points(1).YValues(0) = 0 Then
            'Chart1.Series(0).Points.Item(1).Label = 
            Chart1.Series(0).Points(1).Label = "$ 0"
        End If


        'If Chart1.Series(0).Points(2).YValues(0) = 0 Then
        '    'Chart1.Series(0).Points.Item(1).Label = 
        '    Chart1.Series(0).Points(2).Label = "$ 0"
        'End If


        ' Set series point width
        Chart1.Series("Series1")("PixelPointWidth") = "60"
        Chart1.Series("Ventas")("PixelPointWidth") = "60"
        'Chart1.Series("AB")("PixelPointWidth") = "60"
        'Chart1.Series("Series1")("PointWidth") = "0.1"
        'Chart1.Series("Ventas")("PointWidth") = "0.1"


        ' Set data points label style
        Chart1.Series("Series1")("BarLabelStyle") = "Center"

        ' Show as 3D
        Chart1.ChartAreas("ChartArea1").Area3DStyle.Enable3D = False

        ' Draw as 3D Cylinder
        Chart1.Series("Series1")("DrawingStyle") = "Cylinder"

        h_aportacion.Value = aportacion.ToString("#,0.0000")
    End Sub

    Sub cargar_combo_agentes_usuario()
        Dim gweb As New GWebCN.Agentes
        Dim ds As New DataSet
        Try
            ds = gweb.agentes_vs_usuarios(Session("idc_usuario"))

            If ds.Tables(0).Rows.Count > 0 Then
                ds.Tables(0).Columns.Add("nombre2")
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    ds.Tables(0).Rows(i).Item("nombre2") = ds.Tables(0).Rows(i).Item("idc_agente") & " .- " & ds.Tables(0).Rows(i).Item("nombre")
                Next
                If ds.Tables(0).Rows.Count = 1 Then
                    cboagente.DataSource = ds.Tables(0)
                    cboagente.DataValueField = "idc_agente"
                    cboagente.DataTextField = "nombre2"
                    cboagente.DataBind()
                    'cargar_grid(cboagente.SelectedValue, True)
                    'cargar_grafica(cboagente.SelectedValue)
                ElseIf ds.Tables(0).Rows.Count > 1 Then
                    cboagente.DataSource = ds.Tables(0)
                    cboagente.DataValueField = "idc_agente"
                    cboagente.DataTextField = "nombre2"
                    cboagente.DataBind()
                    'cargar_grafica(cboagente.SelectedValue)
                End If
            Else
                cboagente.Items.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub cboagente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboagente.SelectedIndexChanged

    End Sub

    Protected Sub btnver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnver.Click
        Try
            lblnumagente.Text = cboagente.Items(cboagente.SelectedIndex).Text.Trim
            cargar_grafica(cboagente.SelectedValue, True)
            'calcular_tendencia(True)
            bonos()
            'diferencia()
            lblperiodo.Text = funciones.NombreMes(MonthName(Now.Month)).ToUpper & " " & Now.Year
            'btnmes.Text = "Ver Mes Anterior"
            lblcaab1.Text = "Comisión al Dia con Aportación Adicional y Bono 1"
            lblcaab2.Text = "Comisión al Dia con Aportación Adicional y Bonos(1,2)"
            lblcomision.Text = "Comisión al Dia"
            h_mes.Value = "ver" ''informativo para validar que se a dado click en boton
        Catch ex As Exception
            CargarMsgBox("Error al Cargar Información.\n \u000b \nError:\n" & ex.Message)
        Finally
            ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid.ToString(), "<script>myStopFunction_guard();</script>", False)
        End Try
    End Sub
    Public Function calcular_gastos(ByVal gastos As Decimal) As Decimal
        Dim dt As New DataTable
        Dim gweb As New GWebCN.Consulta
        Dim fecha_hoy As Date = FormatDateTime(Now, DateFormat.ShortDate)
        Dim ultimo_dia As Integer = Nothing
        Dim dias_habiles As Integer = Nothing
        Dim dias_habiles_hoy As Integer = Nothing
        Try

            dt = gweb.ejecuta_consulta("select dbo.fn_ultimo_dia_mes(" & fecha_hoy.Month & "," & fecha_hoy.Year & ")")
            If dt.Rows.Count > 0 Then
                ultimo_dia = dt.Rows(0).Item(0)
                dt = Nothing
                dt = gweb.ejecuta_consulta("select dbo.fn_dias_habiles_ls_fecha(" & fecha_hoy.Month & "," & fecha_hoy.Year & ",'" & ultimo_dia & "/" & fecha_hoy.Month & "/" & fecha_hoy.Year & "')")
                If dt.Rows.Count > 0 Then
                    dias_habiles = dt.Rows(0).Item(0)

                    dt = Nothing
                    dt = gweb.ejecuta_consulta("select dbo.fn_dias_habiles_ls_fecha(" & fecha_hoy.Month & "," & fecha_hoy.Year & ",'" & fecha_hoy & "')")

                    If dt.Rows.Count > 0 Then
                        dias_habiles_hoy = dt.Rows(0).Item(0)
                    End If
                    'txttotalgastos.Text = FormatNumber(CStr(gastos / dias_habiles * dias_habiles_hoy), 2)
                    Dim promedio As Decimal
                    Dim tendencia As Decimal
                    If txtventa.Text > 0 Then
                        promedio = txtventa.Text / dias_habiles_hoy
                        tendencia = promedio * dias_habiles
                        Session("tendencia") = tendencia
                    End If

                End If
                Return FormatNumber(CStr(gastos / dias_habiles * dias_habiles_hoy), 2)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Protected Sub btnmes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmes.Click
        'If btnmes.Text = "Ver Mes Anterior" Then
        Try
            lblnumagente.Text = cboagente.Items(cboagente.SelectedIndex).Text.Trim
            cargar_grafica(cboagente.SelectedValue, False) 'Aqui Cambie por mes actual...
            'calcular_tendencia(False)
            bonos()
            'btnmes.Text = "Ver Mes Actual"
            Dim month_ As Integer = Now.Month - 1
            Dim year_ As Integer = Now.Year
            If month_ <= 0 Then
                month_ = 12
                year_ = year_ - 1
            End If
            lblperiodo.Text = funciones.NombreMes(MonthName(month_)).ToUpper & " " & year_
            lblcaab1.Text = "Comisión Final con Aportación Adicional y Bono 1"
            lblcaab2.Text = "Comisión Final con Aportación Adicional y Bonos(1,2)"
            lblcomision.Text = "Comisión Final"
            h_mes.Value = "mes"
        Catch ex As Exception
            CargarMsgBox("Error al Cargar Información.\n \u000b \nError:\n" & ex.Message)
        Finally
            ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid.ToString(), "<script>myStopFunction_guard();</script>", False)
        End Try
    End Sub

    Protected Sub grid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grid1.ItemCommand

    End Sub

    Protected Sub grid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grid1.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            e.Item.Attributes("onclick") = "return detalle_factura('" & e.Item.ItemIndex & "')"
        End If
    End Sub

    Protected Sub gridv1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridv1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim chkdirecta As New CheckBox
            chkdirecta = e.Row.FindControl("chkdirecta")
            If Not chkdirecta Is Nothing Then
                chkdirecta.Attributes("onclick") = "return false;"
            End If

            e.Row.Attributes("onclick") = "return detalle_factura(" & e.Row.Cells(4).Text & "," & e.Row.RowIndex & ");"
        End If
    End Sub

    Protected Sub txtdiferencia_TextChanged(sender As Object, e As EventArgs) Handles txtdiferencia.TextChanged

    End Sub

    Protected Sub bono2_TextChanged(sender As Object, e As EventArgs) Handles bono2.TextChanged

    End Sub

    Protected Sub bono1_TextChanged(sender As Object, e As EventArgs) Handles bono1.TextChanged

    End Sub


    Protected Sub btndetalles_Click(sender As Object, e As EventArgs) Handles btndetalles.Click
        If h_mes.Value <> "" Then

            Dim url As String = String.Format("comisiones_detalles.aspx?p_ventad={0}&p_ventac={1}&p_comisiond={2}&p_comisionc={3}&apo={4}",
                                              funciones.deTextoa64(txtventa_d_h.Value),
                                              funciones.deTextoa64(txtventa_c_h.Value),
                                              funciones.deTextoa64(txtcomision_d_h.Value),
                                              funciones.deTextoa64(txtcomision_c_h.Value),
                                              funciones.deTextoa64(h_aportacion.Value))

            'Response.Redirect(url, False)
            'Context.ApplicationInstance.CompleteRequest()
            ScriptManager.RegisterStartupScript(Me, [GetType](), Guid.NewGuid().ToString(), "window.open('" + url + "')", True)
        Else
            Alert.ShowAlertInfo("Elige el mes para ver los detalles.", "Mensaje del sistema", Me)
        End If
    End Sub
    'Protected Sub btncomisiones_Click(sender As Object, e As EventArgs) Handles btncomisiones.Click
    '    'ScriptManager.RegisterStartupScript(Me, [GetType](), Guid.NewGuid().ToString(),
    '    '"ModalConfirm('Información del BONO DE PRESUPUESTO','modal fade modal-info');", True)
    'End Sub
    Protected Sub btncomisiones_Click(sender As Object, e As EventArgs) Handles btncomisiones.Click
        Dim url As String = btncomisiones.CommandName
        If url IsNot "" Then
            ScriptManager.RegisterStartupScript(Me, [GetType](), Guid.NewGuid.ToString(), "window.open('" + url + "');", True)
        End If
    End Sub
End Class
