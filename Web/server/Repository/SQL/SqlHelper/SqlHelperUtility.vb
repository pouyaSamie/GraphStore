Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Data.Common

Namespace Repositories

    Friend NotInheritable Class SqlHelperUtility

        Public Shared Sub AttachParameters(ByVal command As SqlCommand, ByVal commandParameters() As SqlParameter)
            If (command Is Nothing) Then Throw New ArgumentNullException("command")
            If (Not commandParameters Is Nothing) Then
                For Each p As SqlParameter In commandParameters
                    If (Not p Is Nothing) Then
                        If (p.Direction = ParameterDirection.InputOutput OrElse p.Direction = ParameterDirection.Input) AndAlso p.Value Is Nothing Then
                            p.Value = DBNull.Value
                        End If
                        command.Parameters.Add(p)
                    End If
                Next
            End If
        End Sub

        Public Shared Sub PrepareCommand(ByVal command As SqlCommand, ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters() As SqlParameter)

            command.Connection = connection
            command.CommandText = commandText
            command.CommandType = commandType

            If Not (commandParameters Is Nothing) Then
                AttachParameters(command, commandParameters)
            End If

        End Sub

        Public Shared Sub PrepareCommand(ByVal command As SqlCommand, ByVal connection As SqlConnection, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandTimeOut As Integer, ByVal commandParameters() As SqlParameter)
            If (command Is Nothing) Then Throw New ArgumentNullException("command")
            If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
            If (commandText Is Nothing OrElse commandText.Length = 0) Then Throw New ArgumentNullException("commandText")

            command.Connection = connection
            command.CommandTimeout = commandTimeOut
            command.CommandText = commandText

            command.CommandType = commandType

            If Not (commandParameters Is Nothing) Then
                AttachParameters(command, commandParameters)
            End If
        End Sub

        Public Shared Sub AssignParameterValues(ByVal commandParameters() As SqlParameter, ByVal parameterValues() As Object)
            Dim i As Integer
            Dim j As Integer
            If (commandParameters Is Nothing) AndAlso (parameterValues Is Nothing) Then
                Return
            End If
            If commandParameters.Length <> parameterValues.Length Then
                Throw New ArgumentException("Parameter count does not match Parameter Value count.")
            End If
            j = commandParameters.Length - 1
            For i = 0 To j
                If TypeOf parameterValues(i) Is IDbDataParameter Then
                    If (CType(parameterValues(i), IDbDataParameter).Value Is Nothing) Then
                        commandParameters(i).Value = DBNull.Value
                    Else
                        commandParameters(i).Value = CType(parameterValues(i), IDbDataParameter).Value
                    End If
                ElseIf (parameterValues(i) Is Nothing) Then
                    commandParameters(i).Value = DBNull.Value
                Else
                    commandParameters(i).Value = parameterValues(i)
                End If
            Next
        End Sub

        Private Sub New()
        End Sub

    End Class

End Namespace