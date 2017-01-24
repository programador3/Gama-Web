Imports System.Data
Imports System
Imports System.IO
Imports System.Collections
Partial Class Ficha_Tecnica
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim idc_articulo As Integer = Request.QueryString("idc")
            cargar_datos(idc_articulo)
        End If
    End Sub
    Sub cargar_datos(ByVal idc_articulo As Integer)
        Dim gweb As New GWebCN.Productos
        Dim ds As New DataSet
        Try
            ds = gweb.ficha_tecnica(idc_articulo) 'Request.QueryString("idc_articulo"))
            If ds.Tables(0).Rows.Count > 0 Then
                lblproducto.Text = ds.Tables(0).Rows(0).Item("producto")
                lblcaracteristicas.Text = ds.Tables(0).Rows(0).Item("caracteristica")
                lblunidad.Text = ds.Tables(0).Rows(0).Item("medida")
                lblcodigo.Text = ds.Tables(0).Rows(0).Item("codigo")
                lblproveedor.Text = ds.Tables(0).Rows(0).Item("proveedor")
                lblfamilia.Text = ds.Tables(0).Rows(0).Item("familia")
                lblsubfamilia.Text = ds.Tables(0).Rows(0).Item("subfamilia")
                lblventa.Text = ds.Tables(0).Rows(0).Item("compra_min")
                lblmultiplos.Text = ds.Tables(0).Rows(0).Item("multiplos")
                lblvolumen.Text = ds.Tables(0).Rows(0).Item("volumen")
                lbltiempo.Text = ds.Tables(0).Rows(0).Item("entrega")
                lblgarantia.Text = ds.Tables(0).Rows(0).Item("garantia")
                lblvida.Text = ds.Tables(0).Rows(0).Item("caducidad")
                txt2(idc_articulo)
                cargar_imagen(idc_articulo)
                'codigos(idc_articulo) ***Codigos Anteriores Articulos.
                cargar_pdf(idc_articulo)
            Else
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>alert('El Producto No Tiene Ficha Tecnica.');window.close();</script>", False)
            End If
        Catch ex As Exception
            CargarMsgBox("Error al Cargar la Información. \n \u000B \n " & "Error: " & ex.Message)
        End Try

    End Sub

    Sub txt2(ByVal idc_articulo As Integer)
        Dim gweb As New GWebCN.Unidades
        Dim ds As New DataSet
        Try
            ds = gweb.Unidad_Archivos("FT_TXT")
            If ds.Tables(0).Rows.Count > 0 Then
                If IO.File.Exists(ds.Tables(0).Rows(0).Item(1) & idc_articulo & ".txt") Then
                    Dim ruta_arch As String = ds.Tables(0).Rows(0).Item(1) & idc_articulo & ".txt"
                    Dim BagelStreamReader As New StreamReader(ruta_arch, Encoding.Default, True)
                    txtdescripcion.Text = BagelStreamReader.ReadToEnd
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub cargar_imagen(ByVal idc_articulo As Integer)
        Dim gweb As New GWebCN.Unidades
        Dim ds As New DataSet
        Try
            ds = gweb.Unidad_Archivos("ARTICU")
            If ds.Tables(0).Rows.Count > 0 Then
                If IO.File.Exists(ds.Tables(0).Rows(0).Item(1) & idc_articulo & ".jpg") Then
                    If Not IO.File.Exists(Server.MapPath("~/Temp/files/" & idc_articulo & ".JPG")) Then
                        IO.File.Copy(ds.Tables(0).Rows(0).Item(1) & idc_articulo & ".jpg", Server.MapPath("~/Temp/files/" & idc_articulo & ".JPG"))
                        Image1.ImageUrl = "~/Temp/files/" & idc_articulo & ".JPG"
                    Else
                        Image1.ImageUrl = "~/Temp/files/" & idc_articulo & ".JPG"
                    End If
                    'Dim ruta_arch As String = ds.Tables(0).Rows(0).Item(1) & idc_articulo & ".jpg"
                    'Image1.ImageUrl = ds.Tables(0).Rows(0).Item(1) & idc_articulo & ".jpg"

                Else
                    Image1.ImageUrl = "~/Temp/files/SIN_FOTO.JPG"
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub cargar_pdf(ByVal idc_articulo As String)
        Dim ds As New DataSet
        Dim gweb As New GWebCN.Unidades
        Try
            ds = gweb.Unidad_Archivos("f_tec")
            If ds.Tables(0).Rows.Count > 0 Then
                If IO.File.Exists(ds.Tables(0).Rows(0)(1) & idc_articulo & ".PDF") Then
                    If Not IO.File.Exists(Server.MapPath("Temp\files\PDF\" & idc_articulo & ".PDF")) Then
                        IO.File.Copy(ds.Tables(0).Rows(0)(1) & idc_articulo & ".PDF", Server.MapPath("Temp\files\PDF\" & idc_articulo & ".PDF"))
                        txtruta.Text = "Temp/files/PDF/" & idc_articulo & ".PDF"
                    Else
                        txtruta.Text = "Temp/files/PDF/" & idc_articulo & ".PDF"
                    End If
                    btnenviar.Visible = True
                    btnver.Visible = True
                    lblficha.Visible = True
                Else
                    txtruta.Text = ""
                    btnenviar.Visible = False
                    btnver.Visible = False
                    lblficha.Visible = False
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub codigos(ByVal idc_articulo As Integer)
        Dim gweb As New GWebCN.Productos
        Dim ds As New DataSet
        Try
            ds = gweb.codigos_producto(idc_articulo)
            If ds.Tables(0).Rows.Count > 0 Then
                'gridcodigos.DataSource = ds.Tables(0)
                'gridcodigos.DataBind()
            Else
                'gridcodigos.DataSource = ds.Tables(0)
                'gridcodigos.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click

    End Sub

    Protected Sub btncorreo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncorreo.Click
        Dim gwebcorreo As New GWebCN.EnviarCorreo
        Dim mail As New Net.Mail.MailMessage
        Try
            mail.Subject = "Ficha Tecnica"
            mail.IsBodyHtml = True
            mail.Body = "<FONT FACE=arial><b>Ficha Tecnica de: </b>" & " " & lblproducto.Text.Trim & "</FONT>"
            mail.Attachments.Add(New Net.Mail.Attachment(Server.MapPath(txtruta.Text)))
            mail.To.Add(txtcorreo.Text.Trim)
            gwebcorreo.Enviar_Correo(mail, Session("idc_usuario"), 3)
            CargarMsgBox("Se Envio Correctamente.")
        Catch ex As Exception
            CargarMsgBox("Error al enviar Correo. \n \u000B \n" & "Error: " & ex.Message)
        End Try
    End Sub

    Sub CargarMsgBox(ByVal mensaje As String)
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "", "<script>alert(' " & mensaje.Replace("'", "") & " ');</script>", False)
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, GetType(Page), Guid.NewGuid.ToString(), "<script>divs();</script>", False)
    End Sub
End Class
