Imports System.Runtime.CompilerServices

Public Module Extentions
    ''' <summary>
    ''' Not String.IsNullOrWhiteSpace(s)
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns>Return Not String.IsNullOrWhiteSpace(s)</returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function HasText(ByVal s As String) As Boolean
        Return Not String.IsNullOrWhiteSpace(s)
    End Function

    <Extension()>
    Public Function GetString(row As DataRow, columnName As String) As String

        If row.IsNull(columnName) Then
            Return String.Empty
        End If

        Return row(columnName).ToString

    End Function

    <Extension()>
    Public Function GetValue(Of T As Structure)(row As DataRow, columnName As String) As T?

        If row.IsNull(columnName) Then
            Return Nothing
        End If

        Return DirectCast(row(columnName), T?)

    End Function

    <Extension()>
    Public Function GetBoolean(row As DataRow, columnName As String) As Boolean

        If row.IsNull(columnName) Then
            Return Nothing
        End If

        If row(columnName) = 0 Then
            Return False
        Else
            Return True
        End If

    End Function


    '<Extension()>
    'Public Function GetInteger(value As Object) As Integer?

    '    If value Is DBNull.Value Then
    '        Return Nothing
    '    End If

    '    Dim parsedValue As Integer
    '    If Integer.TryParse(value.ToString, parsedValue) Then
    '        Return parsedValue
    '    Else
    '        Return Nothing
    '    End If


    'End Function


    '<Extension()>
    'Public Function GetLong(value As Object) As Long?

    '    If value Is DBNull.Value Then
    '        Return Nothing
    '    End If

    '    Dim parsedValue As Long
    '    If Long.TryParse(value.ToString, parsedValue) Then
    '        Return parsedValue
    '    Else
    '        Return Nothing
    '    End If


    'End Function

    '<Extension()>
    'Public Function GetDecimal(value As Object) As Decimal?

    '    If value Is DBNull.Value Then
    '        Return Nothing
    '    End If

    '    Dim parsedValue As Long
    '    If Decimal.TryParse(value.ToString, parsedValue) Then
    '        Return parsedValue
    '    Else
    '        Return Nothing
    '    End If


    'End Function

    '<Extension()>
    'Public Function GetBoolean(value As Object) As boolean?

    '    If value Is DBNull.Value Then
    '        Return Nothing
    '    End If

    '    Dim parsedValue As Integer
    '    If Not Integer.TryParse(value.ToString, parsedValue) Then
    '        Return Nothing
    '    End If

    '    If parsedValue = 0 Then
    '        Return False
    '    Else
    '        Return True
    '    End If

    'End Function
End Module
