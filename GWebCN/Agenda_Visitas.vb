Public Class Agenda_Visitas
    Function Agenda_de_visitas(ByVal idc_agente As Integer, ByVal dia As Integer) As DataSet
        Try
            Dim parametros() As String = {"@pidc_agente", "@pdia"}
            Dim valores() As Object = {idc_agente, dia}
            Return GWebCD.clsConexion.EjecutaSP("sp_reporte_agenda_visitas", parametros, valores)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
