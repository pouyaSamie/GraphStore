Namespace Controllers

    Public Class SearchRequestModel

        Public Property PageSize As Integer = 20
        Public Property PageNumber As Integer = 1
        Public Property SortFields() As New List(Of String)
        Public Property SortDirections() As New List(Of String)
        Public Property SearchTerm() As String = ""


    End Class

    Public Class IdRequestModel

        Public Property Id As Integer


    End Class

End Namespace

