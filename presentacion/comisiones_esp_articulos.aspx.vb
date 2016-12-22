Imports System.Data
Imports System.Drawing
Imports presentacion

Partial Class comisiones_esp_articulos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'cargamos el cbox y el grid
            cargar_comisiones_art()
           
            'pruebas
            Dim tot As Integer = cbox_semana_articulos.Items.Count
            If tot > 0 Then
                cbox_semana_articulos.SelectedIndex = tot - 1
                filtrar_tabla(cbox_semana_articulos.SelectedValue)
                sumar_cantidad_aportacion(cbox_semana_articulos.SelectedValue)
            End If
            


        End If
    End Sub

    Protected Sub cargar_comisiones_art()
        'Dim gweb As New GWebCN.
        Dim gweb As New GWebCN.Comisiones
        Dim ds As New DataSet
        Try
            Dim fechahoy As String = DateTime.Now.ToString("dd-MM-yyyy")
            Dim idc_usuario As Integer = Session("sidc_usuario")
            ds = gweb.comisiones_esp_articulos(fechahoy, fechahoy, idc_usuario)
            If ds.Tables(0).Rows.Count > 0 Then
                'cargar la tabla 
                Session("TablaComEspecialArticulo") = ds.Tables(0)
                'cbox
                cbox_semana_articulos.DataSource = ds.Tables(1)
                cbox_semana_articulos.DataTextField = "texto"
                cbox_semana_articulos.DataValueField = "semana_id"
                'add item at the beginning of the list 
                cbox_semana_articulos.DataBind()
                cbox_semana_articulos.Items.Insert(0, New ListItem("Todos", "0"))
               
            Else
               
                CargarMsgBox("No se encontraron datos.")
            End If

        Catch ex As Exception
            CargarMsgBox("Error al Cargar Información.\n \u000b \nError:\n" & ex.Message)
        End Try
    End Sub

    Sub CargarMsgBox(ByVal msj As String)
        Alert.ShowAlertError(msj.Replace("'", ""), Me.Page)
    End Sub

    Protected Sub cbox_semana_articulos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_semana_articulos.SelectedIndexChanged
        Dim semana As Integer
        semana = cbox_semana_articulos.SelectedValue
        filtrar_tabla(semana)
        sumar_cantidad_aportacion(semana)
    End Sub

    Protected Sub filtrar_tabla(ByVal semana As Integer)
        Dim tabla_esp_articulos As DataTable
        tabla_esp_articulos = Session("TablaComEspecialArticulo")
        If tabla_esp_articulos Is Nothing Then
            'no hacer nada
        Else
            'creamos el dataview
            Dim dv As New DataView(tabla_esp_articulos)
            'filtramos
            If semana > 0 Then 'si es cero quiere ver todos
                dv.RowFilter = "semana=" & semana
            End If

            grid_comision_esp_articulos.DataSource = dv
            grid_comision_esp_articulos.DataBind()
        End If
        
    End Sub

    Protected Sub sumar_cantidad_aportacion(ByVal semana As Integer)
        Dim tabla_esp_articulos As DataTable
        tabla_esp_articulos = Session("TablaComEspecialArticulo")
        If tabla_esp_articulos Is Nothing Then
            txtcantidad.Text = 0
            txtaportacuin.Text = 0.ToString("C")
        Else
            If tabla_esp_articulos.Rows.Count > 0 Then
                Dim filas() As DataRow
                If semana > 0 Then
                    filas = tabla_esp_articulos.Select("semana=" & semana)
                Else
                    filas = tabla_esp_articulos.Select("")
                End If

                Dim i As Integer
                Dim cantidad As Integer = 0
                Dim aportacion As Integer = 0

                For i = 0 To filas.Length - 1
                    cantidad = cantidad + filas(i)("cantidad")
                    aportacion = aportacion + filas(i)("aportacion")
                Next

                'despues los mostramos en pantalla
                txtcantidad.Text = cantidad
                txtaportacuin.Text = aportacion.ToString("C")
            Else
                txtcantidad.Text = 0
                txtaportacuin.Text = 0.ToString("C")
            End If

        End If
       
    End Sub
End Class
