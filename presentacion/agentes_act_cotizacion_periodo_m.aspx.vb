Imports System.Data
Imports presentacion

Partial Class agentes_act_cotizacion_periodo_m
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("sidc_usuario") = Nothing Then
                Response.Redirect("login.aspx")
            End If
            cargar_combo_agentes_usuario()
            txtfinicial.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")
            txtffinal.Text = DateTime.Now.ToString("yyyy-MM-dd")
            btnejecutar.Attributes("onclick") = "return cargar_grid();"
            cboclientes.Attributes("onchange") = "return read_grid();"
            div_cboclientes.Visible = False
        End If
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
                    cboagentes.DataSource = ds.Tables(0)
                    cboagentes.DataValueField = "idc_agente"
                    cboagentes.DataTextField = "nombre2"
                    cboagentes.DataBind()
                    'cargar_grid(cboagentes.SelectedValue, True)
                Else
                    cboagentes.DataSource = ds.Tables(0)
                    cboagentes.DataValueField = "idc_agente"
                    cboagentes.DataTextField = "nombre2"
                    cboagentes.DataBind()
                End If

            Else
                cboagentes.Items.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnejecutar_Click(sender As Object, e As EventArgs) Handles btnejecutar.Click
        Dim gweb As New GWebCN.cotizaciones
        Dim ds As New DataSet

        Dim fechai As String = txtfinicial.Text

        Dim fechaf As String = txtffinal.Text

        If fechai = "" Then
            CargarMsgBox("Fecha Inicial Incorrecta")
            Return
        End If

        If fechaf = "" Then
            CargarMsgBox("Fecha Final Incorrecta")
            Return
        End If


        Try
            ds = gweb.cotizaciones_agentes(Session("idc_sucursal"), cboagentes.SelectedValue, Convert.ToDateTime(fechai), Convert.ToDateTime(fechaf))
            If ds.Tables(0).Rows.Count > 0 Then
                griddatos.DataSource = ds.Tables(0)
                griddatos.DataBind()

                Dim dt As New DataTable
                dt = ds.Tables(0).DefaultView.ToTable("dt", True, "nombre", "idc_cliente")

                If (dt.Rows.Count > 0) Then
                    cboclientes.DataSource = dt
                    cboclientes.DataTextField = "nombre"
                    cboclientes.DataValueField = "idc_cliente"
                    cboclientes.DataBind()
                    div_cboclientes.Visible = True
                Else
                    div_cboclientes.Visible = False
                End If
                ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid.ToString(), "<script>read_grid();</script>", False)
            Else
                div_cboclientes.Visible = False
                griddatos.DataSource = Nothing
                griddatos.DataBind()
                CargarMsgBox("No Se Encontraron Datos.")
            End If
        Catch ex As Exception
            CargarMsgBox("Error al Cargar Información \n\u000B\nError:\n" & ex.Message)
        Finally
            ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid.ToString(), "<script>myStopFunction_guard();</script>", False)
        End Try
    End Sub
    Protected Sub btng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btng.Click

    End Sub

    Sub CargarMsgBox(ByVal msj As String)
        Alert.ShowAlertError(msj.Replace("'", ""), Me.Page)
    End Sub

    Protected Sub griddatos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles griddatos.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If IsNumeric(e.Item.Cells(11).Text.Trim) Then
                'e.Item.Cells(1).Attributes("style") = "color:blue;"
                e.Item.Cells(1).ForeColor = Drawing.Color.Blue
            End If
            If CDec(e.Item.Cells(3).Text) > CDec(e.Item.Cells(6).Text) Then
                e.Item.Cells(3).ForeColor = Drawing.Color.Red
            End If

            If e.Item.Cells(4).Text.Trim = "NO AUTORIZADO" Then
                e.Item.Cells(4).ForeColor = Drawing.Color.Red
            End If


            Dim imgdet As New ImageButton
            imgdet = e.Item.FindControl("imgdet")
            If Not imgdet Is Nothing Then
                imgdet.Attributes("onclick") = " return detalles(" & e.Item.ItemIndex + 1 & ");"
            End If

            e.Item.Cells(1).Attributes("onclick") = "return cargar_obs(" & e.Item.ItemIndex + 1 & ");"
        End If
    End Sub
End Class
