Imports System.Data.SqlClient
Imports System.Data
Partial Class generar_embarque_de_pre_embarques_m
    Inherits System.Web.UI.Page

    Protected connection As New DBConnection()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            btndown.Attributes("onclick") = "return grid2(false,0)"
            btnup.Attributes("onclick") = "return grid2(true,0)"
            If Session("sidc_usuario") Is Nothing Then
                Response.Redirect("login.aspx")
            End If

            InitializeDataTable2()
            ViewState("SortExpression") = "fecha ASC"
            InitializeDataTable()
            ViewState("SortExpression2") = "codigo ASC"

            BindGridView2()
            llenar_grid1()

            If cboembarques.Items.Count = 0 Then
                Label1.Visible = True
                div_contenido.Visible = False

            Else
                Label1.Visible = False
                div_contenido.Visible = True
            End If

            If cboembarques.Items.Count = 0 Then
                Label1.Visible = True
                disponibilidad.Visible = False
            End If

        End If

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        Dim tbox_revision1 As TextBox = DirectCast(e.Row.FindControl("TextBox3"), TextBox)
        Dim tbox_revision2 As TextBox = DirectCast(e.Row.FindControl("TextBox2"), TextBox)

        If tbox_revision1 IsNot Nothing Then

            If tbox_revision1.Text = True Then
                tbox_revision1.BackColor = Drawing.Color.Green
                tbox_revision1.ForeColor = Drawing.Color.Green
            Else
                tbox_revision1.BackColor = Drawing.Color.Red
                tbox_revision1.ForeColor = Drawing.Color.Red
            End If

        End If

    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound

    End Sub

    Protected Sub Gridview1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.SelectedIndexChanged, GridView2.SelectedIndexChanged

        Dim temp1 As String
        temp1 = GridView1.SelectedDataKey.Value.ToString()

        BindGridView(temp1)

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Response.Redirect("menu.aspx")

    End Sub

    Private Sub InitializeDataTable2()

        Dim dtpre_embarques As New DataTable()
        Dim column As DataColumn = Nothing

        dtpre_embarques.Columns.Add("idc_preemb")
        dtpre_embarques.Columns.Add("fecha")
        dtpre_embarques.Columns.Add("fecha_entrega")
        dtpre_embarques.Columns.Add("tipo_camion")
        dtpre_embarques.Columns.Add("observ")
        dtpre_embarques.Columns.Add("tiempo")
        dtpre_embarques.Columns.Add("tiempo_entrega")
        dtpre_embarques.Columns.Add("suc_entrega")
        dtpre_embarques.Columns.Add("nivel")
        dtpre_embarques.Columns.Add("proveedor")
        dtpre_embarques.Columns.Add("usuario")
        dtpre_embarques.Columns.Add("tipo_entrega")
        dtpre_embarques.Columns.Add("camion")
        dtpre_embarques.Columns.Add("desc")

        ' Set ArticleID column as the primary key.
        Dim dcKeys As DataColumn() = New DataColumn(0) {}
        dcKeys(0) = dtpre_embarques.Columns("idc_preemb")
        dtpre_embarques.PrimaryKey = dcKeys

        ' Store the DataTable in ViewState.
        ViewState("dtpre_embarques") = dtpre_embarques

    End Sub

    Private Sub InitializeDataTable()

        Dim dtarticulos As New DataTable()
        Dim column As DataColumn = Nothing

        dtarticulos.Columns.Add("idc_preemb")
        dtarticulos.Columns.Add("idc_articulo")
        dtarticulos.Columns.Add("codigo")
        dtarticulos.Columns.Add("desart")
        dtarticulos.Columns.Add("unimed")

        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Decimal")
        column.ColumnName = "cantidad"
        dtarticulos.Columns.Add(column)

        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Decimal")
        column.ColumnName = "pedido"
        dtarticulos.Columns.Add(column)

        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Decimal")
        column.ColumnName = "existencia"
        dtarticulos.Columns.Add(column)

        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Decimal")
        column.ColumnName = "cantidad_oc"
        dtarticulos.Columns.Add(column)

        ' Store the DataTable in ViewState.
        ViewState("dtarticulos") = dtarticulos

    End Sub

    Private Sub BindGridView2()

        If ViewState("dtpre_embarques") IsNot Nothing Then

            ' Get the DataTable from ViewState.
            Dim dtpre_embarques As DataTable = DirectCast(ViewState("dtpre_embarques"), DataTable)

            ' Convert the DataTable to DataView.
            Dim dvpre_embarques As New DataView(dtpre_embarques)

            ' Set the sort column and sort order.
            dvpre_embarques.Sort = ViewState("SortExpression").ToString()

            ' Bind the GridView control.
            'GridView1.DataSource = dvpre_embarques
            'GridView1.DataBind()

            cboembarques.DataSource = dvpre_embarques
            cboembarques.DataTextField = "desc"
            cboembarques.DataValueField = "idc_preemb"
            cboembarques.DataBind()

            If cboembarques.Items.Count > 0 Then
                cboembarques_SelectedIndexChanged(Nothing, EventArgs.Empty)
            End If

        End If

    End Sub

    Private Sub BindGridView(ByVal pidc_preemb As Integer)

        If ViewState("dtarticulos") IsNot Nothing Then

            ' Get the DataTable from ViewState.
            Dim dtarticulos As DataTable = DirectCast(ViewState("dtarticulos"), DataTable)

            ' Convert the DataTable to DataView.
            Dim dvarticulos As New DataView(dtarticulos)

            ' Set the sort column and sort order.

            dvarticulos.Sort = ViewState("SortExpression2").ToString()

            dvarticulos.RowFilter = "idc_preemb=" + pidc_preemb.ToString

            ' Bind the GridView control.
            GridView2.DataSource = dvarticulos
            GridView2.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), Guid.NewGuid.ToString(), "<script>grid2(false,1);</script>", False)

        End If

    End Sub

    Private Sub llenar_grid1()

        ''Elementos a Revisar
        Dim dtpre_embarques As DataTable = DirectCast(ViewState("dtpre_embarques"), DataTable)
        Dim dtarticulos As DataTable = DirectCast(ViewState("dtarticulos"), DataTable)

        dtpre_embarques.Clear()

        Dim dspre_embarques As DataSet = connection.getpre_embarques_pendientes(Convert.ToInt32(Session("sidc_usuario")))
        Dim dt_cursor1 As DataTable = dspre_embarques.Tables(0)

        Dim vidc_preemb As Integer
        Dim vfecha As String
        Dim vfecha_entrega As String
        Dim vtipo_camion As String
        Dim vobserv As String
        Dim vtiempo As String
        Dim vtiempo_entrega As String
        Dim vsuc_entrega As String
        Dim vnivel As Integer
        'Dim vnivelc As String
        Dim vproveedor As String
        Dim vusuario As String
        Dim vtipo_entrega As String
        Dim vcamion As Boolean
        Dim desc As String = ""
        For Each row As DataRow In dt_cursor1.Rows

            vidc_preemb = Convert.ToInt32(row("idc_preemb"))
            vfecha = Convert.ToString(row("fecha")).Trim()
            vfecha_entrega = Convert.ToString(Left(row("fecha_entrega"), 10)).Trim()
            vtipo_camion = Convert.ToString(row("descripcion")).Trim()
            vobserv = Convert.ToString(row("observaciones")).Trim()
            vtiempo = connection.tiempo_dias_corto(Convert.ToInt32(row("tiempo_carga") * 60))
            vtiempo_entrega = connection.tiempo_dias_corto(Convert.ToInt32(row("tiempo_entrega") * 60))
            vsuc_entrega = Convert.ToString(row("sucursale")).Trim()
            vnivel = Convert.ToInt32(row("nivel"))
            desc = CStr(vidc_preemb).Trim & " * " & CStr(vtipo_camion).Trim
            'If vnivel = 1 Then
            '    vnivelc = "Y  "
            'Else
            '    If vnivel = 2 Then
            '        vnivelc = "YY "
            '    Else
            '        vnivelc = "YYY"
            '    End If
            'End If

            vproveedor = Convert.ToString(row("provzona")).Trim()
            vusuario = Convert.ToString(row("usuario")).Trim()
            vtipo_entrega = Convert.ToString(row("tipo_entrega")).Trim()
            vcamion = False 'Convert.ToBoolean(row("camion"))

            dtpre_embarques.Rows.Add(vidc_preemb, vfecha, vfecha_entrega, vtipo_camion, vobserv, vtiempo, vtiempo_entrega, vsuc_entrega, vnivel, vproveedor, vusuario, vtipo_entrega, vcamion, desc)

        Next

        ''Tabla Articulos
        Dim dt_cursor2 As DataTable = dspre_embarques.Tables(2)

        Dim vidc_articulo As Integer
        Dim vcodigo As String
        Dim vdescripcion As String
        Dim vcantidad As Decimal
        Dim vcantidad_oc As Decimal
        Dim vpedido As Decimal
        Dim vexistencia As Decimal
        Dim vunimed As String

        For Each row As DataRow In dt_cursor2.Rows

            vidc_preemb = Convert.ToInt32(row("idc_preemb"))
            vidc_articulo = Convert.ToInt32(row("idc_articulo"))
            vcodigo = Convert.ToString(row("codigo"))
            vdescripcion = Convert.ToString(row("desart"))
            vcantidad = Convert.ToDecimal(row("cantidad"))
            vunimed = Convert.ToString(row("nom_corto"))
            vpedido = 0
            vcantidad_oc = Convert.ToDecimal(row("cantidad_oc"))
            vexistencia = Convert.ToDecimal(row("existencia"))

            dtarticulos.Rows.Add(vidc_preemb, vidc_articulo, vcodigo, vdescripcion, vunimed, vcantidad, vpedido, vexistencia, vcantidad_oc)

        Next

        ''BindGridView2()
        BindGridView2()

    End Sub

    Private Sub llenar_grid2(ByVal pidc_preemb As Integer)

        ' ''Elementos a Revisar
        'Dim dtarticulos As DataTable = DirectCast(ViewState("dtarticulos"), DataTable)

        '' Convert the DataTable to DataView.
        'Dim dvarticulos As New DataView(dtarticulos)

        '' Set the sort column and sort order.
        'dvarticulos.Sort = ViewState("SortExpression").ToString()
        'dvarticulos.RowFilter = "idc_preemb=" + pidc_preemb.ToString + ";"

        ' '' Dim dspre_embarques As DataSet = connection.getpre_embarques_pendientes(Convert.ToInt32(Session("xidc_usuerio")), Convert.ToInt32(Session("xidc_sucursal"))
        'Dim dsarticulos As DataSet = connection.getpre_embarques_pendientes(200, 1)

        'Dim dt_cursor1 As DataTable = dspre_embarques.Tables(0)

        'Dim vidc_preemb As Integer
        'Dim vfecha As String
        'Dim vfecha_entrega As String
        'Dim vtipo_camion As String
        'Dim vobserv As String
        'Dim vtiempo As String
        'Dim vtiempo_entrega As String
        'Dim vsuc_entrega As String
        'Dim vnivel As Integer
        'Dim vproveedor As String
        'Dim vusuario As String
        'Dim vtipo_entrega As String
        'Dim vcamion As Boolean

        'For Each row As DataRow In dt_cursor1.Rows
        '    vidc_preemb = Convert.ToInt32(row("idc_preemb"))
        '    vfecha = Convert.ToString(Left(row("fecha"), 10)).Trim()
        '    vfecha_entrega = Convert.ToString(Left(row("fecha_entrega"), 10)).Trim()
        '    vtipo_camion = Convert.ToString(row("descripcion")).Trim()
        '    vobserv = Convert.ToString(row("observaciones")).Trim()
        '    vtiempo = connection.tiempo_dias_corto(Convert.ToInt32(row("tiempo_carga") * 60))
        '    vtiempo_entrega = connection.tiempo_dias_corto(Convert.ToInt32(row("tiempo_entrega") * 60))
        '    vsuc_entrega = Convert.ToString(row("sucursale")).Trim()
        '    vnivel = Convert.ToInt32(row("nivel"))
        '    vproveedor = Convert.ToString(row("provzona")).Trim()
        '    vusuario = Convert.ToString(row("usuario")).Trim()
        '    vtipo_entrega = Convert.ToString(row("tipo_entrega")).Trim()
        '    vcamion = Convert.ToBoolean(row("camion"))

        '    dtpre_embarques.Rows.Add(vidc_preemb, vfecha, vfecha_entrega, vtipo_camion, vobserv, vtiempo, vtiempo_entrega, vsuc_entrega, vnivel, vproveedor, vusuario, vtipo_entrega, vcamion)

        'Next

        'BindGridView()

    End Sub

    Protected Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub cboembarques_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboembarques.SelectedIndexChanged
        Dim dt As New DataTable
        dt = ViewState("dtpre_embarques")
        Dim rows() As DataRow

        If dt.Rows.Count > 0 Then
            rows = dt.Select("idc_preemb=" & cboembarques.SelectedValue)
            If rows.Length > 0 Then
                txtfecha.Text = rows(0)("fecha")
                txtfecha_e.Text = rows(0)("fecha_entrega")
                txttipocamion.Text = rows(0)("tipo_camion")
                txtobs.Text = rows(0)("observ")
                txttiempo.Text = rows(0)("tiempo")
                txttiempoe.Text = rows(0)("tiempo_entrega")
                txtsuc_e.Text = rows(0)("suc_entrega")
                txtnivel.Text = rows(0)("nivel")
                txtproveedor.Text = rows(0)("proveedor")
                txtusuario.Text = rows(0)("usuario")
                txttipoe.Text = rows(0)("tipo_entrega")
                If rows(0)("camion") = True Then
                    txttipocamion.ForeColor = Drawing.Color.White
                    txttipocamion.BackColor = Drawing.Color.Green
                Else
                    txttipocamion.ForeColor = Drawing.Color.White
                    txttipocamion.BackColor = Drawing.Color.Red
                End If
                BindGridView(cboembarques.SelectedValue)
            End If
        End If
    End Sub
End Class