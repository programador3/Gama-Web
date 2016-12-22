Imports System.Data
Imports System.Drawing
Imports presentacion
Partial Class comisiones_esp_activaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            cargar_comisiones_activaciones()
            
            'pruebas
            Dim tot As Integer = cbox_semana_comisiones_activaciones.Items.Count
            If tot > 0 Then
                cbox_semana_comisiones_activaciones.SelectedIndex = tot - 1
                filtrar_tabla(cbox_semana_comisiones_activaciones.SelectedValue)
                sumar_monto_aportacion(cbox_semana_comisiones_activaciones.SelectedValue)
            End If
            
        End If
    End Sub

    Protected Sub cargar_comisiones_activaciones()
        'Dim gweb As New GWebCN.
        Dim gweb As New GWebCN.Comisiones
        Dim ds As New DataSet
        Try
            Dim fechahoy As String = DateTime.Now.ToString("dd-MM-yyyy")
            Dim idc_usuario As Integer = Session("idc_usuario")
            ds = gweb.comisiones_esp_activaciones(fechahoy, fechahoy, idc_usuario)
            If ds.Tables(0).Rows.Count > 0 Then
                'cargar la tabla 
                Session("TablaComEspecialActivacion") = ds.Tables(0)
                'cbox
                cbox_semana_comisiones_activaciones.DataSource = ds.Tables(1)
                cbox_semana_comisiones_activaciones.DataTextField = "texto"
                cbox_semana_comisiones_activaciones.DataValueField = "semana_id"
                'add item at the beginning of the list 
                cbox_semana_comisiones_activaciones.DataBind()
                cbox_semana_comisiones_activaciones.Items.Insert(0, New ListItem("Todos", "0"))

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

    Protected Sub filtrar_tabla(ByVal semana As Integer)
        Dim tabla_esp_activaciones As DataTable
        tabla_esp_activaciones = Session("TablaComEspecialActivacion")
        If tabla_esp_activaciones Is Nothing Then
            'no hacer nada
        Else
            'creamos el dataview
            Dim dv As New DataView(tabla_esp_activaciones)
            'filtramos
            If semana > 0 Then 'si es cero quiere ver todos
                dv.RowFilter = "semana=" & semana
            End If

            grid_comision_esp_activaciones.DataSource = dv
            grid_comision_esp_activaciones.DataBind()
        End If
       
    End Sub

    Protected Sub sumar_monto_aportacion(ByVal semana As Integer)
        Dim tabla_esp_activaciones As DataTable
        tabla_esp_activaciones = Session("TablaComEspecialActivacion")
        If tabla_esp_activaciones Is Nothing Then
            txtcantidad.Text = 0
            txtaportacuin.Text = 0.ToString("C")
        Else
            If tabla_esp_activaciones.Rows.Count > 0 Then
                Dim filas() As DataRow
                If semana > 0 Then
                    filas = tabla_esp_activaciones.Select("semana=" & semana)
                Else
                    filas = tabla_esp_activaciones.Select("")
                End If

                Dim i As Integer
                Dim monto As Integer = 0
                Dim aportacion As Integer = 0

                For i = 0 To filas.Length - 1
                    monto = monto + filas(i)("monto")
                    aportacion = aportacion + filas(i)("aportacion")
                Next

                'despues los mostramos en pantalla
                txtcantidad.Text = monto
                txtaportacuin.Text = aportacion.ToString("C")
            Else
                txtcantidad.Text = 0
                txtaportacuin.Text = 0.ToString("C")
            End If
        End If
       

    End Sub

    Protected Sub cbox_semana_comisiones_activaciones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_semana_comisiones_activaciones.SelectedIndexChanged
            Dim semana As Integer
            semana = cbox_semana_comisiones_activaciones.SelectedValue
            filtrar_tabla(semana)
            sumar_monto_aportacion(semana)
    End Sub
End Class
