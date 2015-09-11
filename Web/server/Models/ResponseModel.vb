Imports System.Net

Public Class ResponseModel
    Public Property Data As Object
    Public Property Message As String = ""
    Public Property StatusCode As HttpStatusCode

    Public Shared Function Create(_statusCode As HttpStatusCode, Optional _data As Object = Nothing, Optional _message As String = "") As ResponseModel

        Dim out As New ResponseModel

        With out
            .Data = _data
            .Message = _message
            .StatusCode = _statusCode
        End With

        Return out

    End Function

End Class
