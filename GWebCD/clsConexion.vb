
Imports System.Data.SqlClient
Imports System.Configuration
Imports datos

Public Class clsConexion
    Public Shared Function EjecutaSP(ByVal nomSP As String, ByVal parametros() As String, ByVal resultados() As Object) As DataSet

        Dim cnn As New SqlClient.SqlConnection
        Try
            cnn = Conectar()
            cnn.Open()
            Dim daSolicitud As New SqlClient.SqlDataAdapter
            daSolicitud.SelectCommand = New SqlCommand(nomSP, cnn)
            daSolicitud.SelectCommand.CommandTimeout = 6000
            daSolicitud.SelectCommand.CommandType = CommandType.StoredProcedure
            If Not (parametros Is Nothing) Then
                For l As Integer = 0 To parametros.Length - 1
                    daSolicitud.SelectCommand.Parameters.AddWithValue(parametros(l), resultados(l))
                Next
            End If
            Dim ds As New DataSet("EjecutaSP")
            daSolicitud.Fill(ds)
            Return ds
        Catch ex As Exception
            Dim mensaje As String = ex.Message
            Throw ex  '"Ha ocurrido el siguiente error, " & ex.Message & " favor de verificarlo", "E R R O R", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cnn.Close()
        End Try
    End Function
    Public Shared Function EjecutaSP_obj(ByVal nomSP As String, ByVal parametros() As String, ByVal resultados() As Object) As Object

        Dim cnn As New SqlClient.SqlConnection
        Try
            cnn = Conectar()
            cnn.Open()
            Dim daSolicitud As New SqlClient.SqlDataAdapter
            daSolicitud.SelectCommand = New SqlCommand(nomSP, cnn)
            daSolicitud.SelectCommand.CommandTimeout = 6000
            daSolicitud.SelectCommand.CommandType = CommandType.StoredProcedure
            If Not (parametros Is Nothing) Then
                For l As Integer = 0 To parametros.Length - 1
                    daSolicitud.SelectCommand.Parameters.AddWithValue(parametros(l), resultados(l))
                Next
            End If
            Dim ParametroDeRegreso As SqlParameter
            ParametroDeRegreso = New SqlParameter("ValorDeRegreso", SqlDbType.Int)
            ParametroDeRegreso.Direction = ParameterDirection.ReturnValue
            daSolicitud.SelectCommand.Parameters.Add(ParametroDeRegreso)

            Dim ds As New DataSet("EjecutaSP")
            daSolicitud.Fill(ds)
            Return daSolicitud.SelectCommand.Parameters("ValorDeRegreso").Value

        Catch ex As Exception
            ' Dim mensaje As String = ex.Message
            ' Return mensaje '"Ha ocurrido el siguiente error, " & ex.Message & " favor de verificarlo", "E R R O R", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            cnn.Close()
        End Try
    End Function

    Public Shared Function EjecutaSP_Scalar(ByVal nomSP As String, ByVal parametros() As String, ByVal resultados() As Object) As Object
        Dim cnn As New SqlClient.SqlConnection
        Dim result As Object

        Try
            cnn = Conectar()
            cnn.Open()
            Dim command As New SqlCommand(nomSP, cnn)
            command.CommandTimeout = 6000
            command.CommandType = CommandType.StoredProcedure
            If Not (parametros Is Nothing) Then
                For l As Integer = 0 To parametros.Length - 1
                    command.Parameters.AddWithValue(parametros(l), resultados(l))
                Next
            End If
            result = command.ExecuteScalar()
            Return result
        Catch ex As Exception
            Dim msj As String = ex.Message
            Return msj
        Finally
            cnn.Close()
        End Try

    End Function

    Public Shared Function EjecutaSP_Row(ByVal nomSP As String, ByVal parametros() As String, ByVal resultados() As Object) As DataRow

        Dim cnn As New SqlClient.SqlConnection
        Try
            cnn = Conectar()
            cnn.Open()
            Dim daSolicitud As New SqlClient.SqlDataAdapter
            daSolicitud.SelectCommand = New SqlCommand(nomSP, cnn)
            daSolicitud.SelectCommand.CommandTimeout = 6000
            daSolicitud.SelectCommand.CommandType = CommandType.StoredProcedure
            If Not (parametros Is Nothing) Then
                For l As Integer = 0 To parametros.Length - 1
                    daSolicitud.SelectCommand.Parameters.AddWithValue(parametros(l), resultados(l))
                Next
            End If
            Dim ds As New DataSet("EjecutaSP")
            Dim row As DataRow
            daSolicitud.Fill(ds)
            If ds.Tables(0).Rows.Count > 0 Then
                row = ds.Tables(0).Rows(0)
                Return row
            Else
                row = Nothing
                Return row
            End If
        Catch ex As Exception
            'Dim mensaje As String = ex.Message
            'Return Nothing '"Ha ocurrido el siguiente error, " & ex.Message & " favor de verificarlo", "E R R O R", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            cnn.Close()
        End Try
    End Function

    Public Shared Function EjecutaConsulta(ByVal consulta As String) As DataTable
        Dim cnn As New SqlClient.SqlConnection
        Try
            cnn = Conectar()
            cnn.Open()
            Dim daSolicitud As SqlDataReader
            Dim comando As SqlCommand = New SqlCommand(consulta, cnn)
            daSolicitud = comando.ExecuteReader(CommandBehavior.CloseConnection)
            Dim dt As New DataTable()
            If daSolicitud.HasRows Then
                dt.Load(daSolicitud)
            End If
            Return dt
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex  '"Ha ocurrido el siguiente error, " & ex.Message & " favor de verificarlo", "E R R O R", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cnn.Close()
        End Try
    End Function


#Region " Funciones Privadas "
    Private Shared Function Conectar() As SqlClient.SqlConnection
        Try
            'Se instancía y se le asigna el valor a la variable cnn de conexion
            'en el cual se le ponen los parametros de la misma
            ' Dim cnn As New SqlClient.SqlConnection("Data Source=" & pServidor & "; Initial Catalog=" & pCatalogo & _
            '"; User ID=" & pUser & "; Password=" & pPassword & " ; Connect Timeout= 6000")
            Dim tipo As String = ConfigurationManager.AppSettings("cs")
            Dim con As New SqlClient.SqlConnection(IIf(tipo = "P", datos.recursos.cadena_conexion, datos.recursos.cadena_conexion_respa))
            Return con
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region
End Class