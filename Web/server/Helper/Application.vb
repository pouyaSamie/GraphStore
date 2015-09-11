Imports System.Web
Imports Microsoft.AspNet.Identity

Public Class Application

    Private Shared locker As New Object

    Public Shared Function GetCurrentUserId() As Integer
        Return HttpContext.Current.User.Identity.GetUserId
    End Function

End Class