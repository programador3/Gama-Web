Imports System.Data

Partial Class pre_embarques_2_elite_m
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sidc_usuario") = Nothing Then
            Response.Redirect("menu.aspx")
        End If
        If Not Page.IsPostBack Then
            btndown.Attributes("onclick") = "return grid2(false,0)"
            btnup.Attributes("onclick") = "return grid2(true,0)"
            cargar_datos(0, Session("sidc_usuario"), 1)


        End If
    End Sub

    Sub cargar_datos(ByVal idc_preemb As Integer, ByVal idc_usuario As Integer, ByVal elite As Boolean)

        Dim parametros() As String = {"@pidc_preemb", "@PIDC_USUARIO", "@pelite"}
        Dim valores() As Object = {idc_preemb, idc_usuario, elite}
        Dim ds As New DataSet
        Dim cnn As New DBConnection
        Try
            ds = cnn.Ejecuta_SP("sp_ver_pedidos_pendientes_revisado_preemb11", parametros, valores)
            If ds.Tables(0).Rows.Count > 0 Then
                ds.Tables(0).Columns.Add("desc", GetType(String), "idc_pedido + ' / ' + nombre")
                Dim dv As New DataView
                dv = ds.Tables(0).DefaultView().ToTable(True, "idc_pedido", "desc", "nombre").DefaultView()
                dv.Sort = "nombre,idc_pedido"
                cboembarques.DataSource = dv.ToTable()
                cboembarques.DataValueField = "idc_pedido"
                cboembarques.DataTextField = "desc"
                cboembarques.DataBind()

                'GridView1.DataSource = ds.Tables(0)
                'GridView1.DataBind()

                ds.Tables(1).Columns.Add("pendiente", GetType(Decimal), "cantidad - cancelado - surtido")
                ds.Tables(1).Columns.Add("disponible", GetType(Decimal), "existencia - transito")

                GridView2.DataSource = ds.Tables(1)
                GridView2.DataBind()

                ViewState("dt_pedidos") = ds.Tables(0)
                ViewState("dt_det") = ds.Tables(1)
                cboembarques_SelectedIndexChanged(Nothing, EventArgs.Empty)
            End If
        Catch ex As Exception
            CargarMsgBox("Error al Cargar Información.")
        End Try
    End Sub

    Sub CargarMsgBox(ByVal mensaje As String)
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>alert(' " & mensaje.Replace("'", "") & " ');</script>", False)
    End Sub

    Protected Sub cboembarques_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboembarques.SelectedIndexChanged
        Dim dv As New DataView
        dv = DirectCast(ViewState("dt_pedidos"), DataTable).DefaultView()

        Dim dv_det As New DataView
        dv_det = DirectCast(ViewState("dt_det"), DataTable).DefaultView()

        dv.RowFilter = "idc_pedido = " & cboembarques.SelectedValue

        For Each row As DataRow In dv.ToTable().Rows
            txtobservaciones.Text = row("observ")
            txtsaldo.Text = FormatNumber(row("saldo"), 2)
            txtpor_pagado.Text = Math.Round(row("por_saldo"), 2)
            txtfecha.Text = row("fecha")
            txtfecha_pactada.Text = row("fechap")
            txtfecha_e.Text = row("fecha_entrega")
            txtconsignado.Text = IIf(row("idc_colonia") = 0, "", row("calle").ToString().Trim() & " " & row("numero").ToString().Trim() & ", " & row("colonia").ToString().Trim() & ", " & row("mpio").ToString().Trim() & ", " & row("edo").ToString().Trim() & ", " & row("pais").ToString().Trim())
            txtusu.Text = row("usuario")
        Next

        dv_det.RowFilter = "idc_pedido = " & cboembarques.SelectedValue
        GridView2.DataSource = dv_det.ToTable()
        GridView2.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), Guid.NewGuid.ToString(), "<script>grid2(false,1);</script>", False)
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("menu.aspx")
    End Sub
End Class
