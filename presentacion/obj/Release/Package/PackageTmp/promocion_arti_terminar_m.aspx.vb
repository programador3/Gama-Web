Imports System.Data

Partial Class promocion_arti_terminar_m
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sidc_usuario") = Nothing Then
            Response.Redirect("login.aspx")
        End If
        If Not Page.IsPostBack Then
            cargar_datos(Session("idc_usuario"))
            btnanterior.Attributes("onclick") = "return datos(0);"
            btnsiguiente.Attributes("onclick") = "return datos(1);"
            btnanterior1.Attributes("onclick") = "return datos(0);"
            btnsiguiente1.Attributes("onclick") = "return datos(1);"

        End If

    End Sub

    Sub cargar_datos(ByVal idc_usuario As Integer)
        Dim ds As New DataSet
        Dim gweb As New GWebCN.Promociones
        Try
            ds = gweb.Promociones_Por_Terminar(idc_usuario)
            If ds.Tables(0).Rows.Count > 0 Then
                griddatos.DataSource = ds.Tables(0)
                griddatos.DataBind()


                DataGrid1.DataSource = ds.Tables(1)
                DataGrid1.DataBind()

                DataGrid2.DataSource = ds.Tables(3)
                DataGrid2.DataBind()

                DataGrid3.DataSource = ds.Tables(2)
                DataGrid3.DataBind()

                txtindex.Text = 0
                ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid.ToString, "<script>datos();</script>", False)
            Else
                CargarMsgBox("No Existen Promociones Por Terminar.")
            End If
        Catch ex As Exception
            CargarMsgBox("Error al Cargar Datos. \n \u000B \n Error: \n" & ex.Message)
        End Try
    End Sub
    Sub CargarMsgBox(ByVal mensaje As String)
        ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid.ToString, "<script>alert('" & mensaje.Replace("'", "") & "');</script>", False)
    End Sub
    Protected Sub DataGrid1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles DataGrid1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rowView As DataRowView = DirectCast(e.Row.DataItem, DataRowView)
            Dim imagen As String = rowView("imagen").ToString()
            Dim img As ImageButton = TryCast(e.Row.FindControl("img"), ImageButton)
            img.ImageUrl = imagen
            img.OnClientClick = "return Go('" + imagen + "');"
        End If
    End Sub
End Class
