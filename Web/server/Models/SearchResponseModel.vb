Imports System.Net

Namespace Controllers

    Public Class SearchResponseModel
        Inherits ResponseModel

        Public Property ItemsCount As Integer

        Public Overloads Shared Function Create(_statusCode As HttpStatusCode, _itemsCount As Integer, Optional _data As Object = Nothing, Optional _message As String = "") As SearchResponseModel

            Dim out As New SearchResponseModel

            With out
                .Data = _data
                .Message = _message
                .StatusCode = _statusCode
                .ItemsCount = _itemsCount
            End With

            Return out

        End Function
    End Class

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

End Namespace

