
Partial Class detalles_agente_cot
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid.ToString(), "<script>divs();</script>", False)
        ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid.ToString(), "<script>cargar_detalles();</script>", False)
    End Sub
End Class
