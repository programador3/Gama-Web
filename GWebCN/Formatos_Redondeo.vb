Public Class Formatos_Redondeo
    ''' <summary>
    ''' Convierte a formato moneda segun las configuraciones regionales del equipo.
    ''' </summary>
    ''' <param name="valor"> Valor a convertir </param>
    ''' <returns>Regresa el valor a formato moneda(separando por ',' los miles y por '.' los decimales )</returns>
    ''' <remarks></remarks>
    ''' 
    Public Shared Function Formato_moneda(ByVal valor As Double) As String
        Try
            'Return FormatNumber(valor, 2, -2, -2, -1)
            valor = Math.Round(valor, 2)
            Return FormatCurrency(valor, 2)
        Catch ex As Exception
            Throw ex
        Finally
            valor = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Redondea un valor con fracciones decimales (2 digitos decimales)
    ''' </summary>
    ''' <param name="valor">Valor a Redondear</param>
    ''' <returns>Valor redondeado a dos digitos</returns>
    ''' <remarks></remarks>
    Public Shared Function Redondeo_Dos_Decimales(ByVal valor As Decimal) As String

        Try
            valor = Math.Round(valor, 2)
            Return FormatNumber(valor, 2)
        Catch ex As Exception
            Throw ex
        Finally
            valor = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Regresa un numero entero con cuatro digitos decimales
    ''' </summary>
    ''' <param name="valor">Valor a convertir con cuatro digitos decimales</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Redondeo_cuatro_decimales(ByVal valor As Double) As String
        Try
            valor = Math.Round(valor, 4)
            Return FormatNumber(valor, 4)
        Catch ex As Exception
            Throw ex
        Finally
            valor = Nothing
        End Try
    End Function

End Class
