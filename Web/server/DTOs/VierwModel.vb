#Region " File "

Public Class DtoFile
    Public Property Name As String
    Public Property Guid As String

    Public Property EntityType As EntityTypes

    Public Shared Function CreateInstance(file As File, entityType As EntityTypes) As DtoFile
        Dim out As New DtoFile

        With out
            .Name = file.Name
            .Guid = file.Guid
            .EntityType = entityType
        End With

        Return out
    End Function

End Class

#End Region

#Region " User "

Public Class DtoUser
    Public Property Fullname As String
    Public Property DisplayName As String
    Public Property Email As String
    Public Property SecondaryEmail As String
    Public Property Phone As String
    Public Property Home As String
    Public Property Mobile As String
    Public Property Username As String
    Public Property UserId As Integer
    Public Property Picture As String


    Public Shared Function CreateInstance(user As User) As DtoUser

        Dim out As New DtoUser

        With out
            .DisplayName = user.DisplayName
            .Home = user.Home
            .Mobile = user.Mobile
            .Phone = user.PhoneNumber
            .SecondaryEmail = user.SecondaryEmail
            .Username = user.UserName
            .Fullname = user.FullName
            .Email = user.Email
            .UserId = user.Id
            If String.IsNullOrEmpty(user.Picture) Then
                .Picture = user.Picture
            End If

        End With

        Return out

    End Function

    Public Sub FillUser(user As User)

        With user

            .DisplayName = DisplayName
            .Home = Home
            .Mobile = Mobile
            .PhoneNumber = Phone
            .SecondaryEmail = SecondaryEmail
            .UserName = Username
            .FullName = Fullname
            .Email = Email
            .Id = UserId
            .Picture = Picture


        End With

    End Sub

End Class

Public Class DtoUsers
    Inherits List(Of DtoUser)

    Public Sub New()
    End Sub

    Public Sub New(users As IEnumerable(Of User))

        For Each user In users
            Me.Add(DtoUser.CreateInstance(user))
        Next

    End Sub

End Class

Public Class DtoUserMinimal
    Public Property DisplayName As String
    Public Property Email As String
    Public Property UserId As Integer

    Public Shared Function CreateInstance(user As User) As DtoUserMinimal
        Dim out As New DtoUserMinimal

        With out
            .DisplayName = user.DisplayName
            .Email = user.Email
            .UserId = user.Id
        End With

        Return out

    End Function

End Class

Public Class DtoUsersMinimal
    Inherits List(Of DtoUserMinimal)

    Public Sub New()
    End Sub

    Public Sub New(users As IEnumerable(Of User))

        For Each user In users
            Me.Add(DtoUserMinimal.CreateInstance(user))
        Next

    End Sub

End Class


Public Class DtoOrder
    Public Property Amount As Decimal
    Public Property TrakingNumber As String

    Public Property IsPaied As Boolean

    Public Property orderDetails As List(Of DtoOrderDetails)

    Public Sub New(order As Order)

        If order Is Nothing Then
            Exit Sub
        End If


        With order
            Me.Amount = .Amount
            Me.TrakingNumber = .TrakingNumber
            Me.IsPaied = .IsPaied
            Me.IsPaied = .IsPaied
            Me.orderDetails = (From p In order.OrderDetails Select New DtoOrderDetails(p)).ToList
        End With


    End Sub
End Class

Public Class DtoOrderDetails

    Public Property ProductId As Integer
    Public Property Price As Decimal

    Public Property DownloadToken As String
    Public Sub New(orderDetail As OrderDetail)

        If orderDetail Is Nothing Then
            Exit Sub
        End If

        With orderDetail
            Me.ProductId = orderDetail.ProductId
            Me.Price = orderDetail.Price

            If orderDetail.DownloadLinks IsNot Nothing AndAlso orderDetail.DownloadLinks.Count > 0 Then
                Me.DownloadToken = orderDetail.DownloadLinks.First.DownloadToken
            End If

        End With

    End Sub

End Class

#End Region


