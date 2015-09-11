Imports System.Runtime.Serialization

<Serializable>
Public Class DTOProduct

    Public Property ProductId As Integer
    Public Property ProductCategoryTitle As String
    Public Property ProductCategoryId As Integer
    Public Property Price As Decimal
    Public Property images As List(Of String)
    Public Property Name As String
    Public Property Description As String
    Public Property Tags As List(Of DtoTag)
    Public Property isFavorite As Boolean
    Public Property IsNew As Boolean
    Public Property DimentionD As Decimal
    Public Property DimentionH As Decimal
    Public Property DimentionS As Decimal
    Public Property DimentionW As Decimal
    Public Property DownloadToken As String
    Public Property FreeToken As String

    Public Sub New(product As DataRow, tags As List(Of DataRow), productPics As List(Of DataRow))

        With product
            Me.ProductId = product.GetValue(Of Integer)("ProductId")
            Me.ProductCategoryTitle = product.GetString("ProductCategoryTitle")
            Me.ProductCategoryId = product.GetValue(Of Integer)("ProductCategoryId")
            Me.Price = product.GetValue(Of Decimal)("Price")
            Me.Name = product.GetString("Name")
            Me.Description = product.GetString("Description")
            Me.isFavorite = product.GetBoolean("isFavorite")
            Me.IsNew = product.GetBoolean("IsNew")
            Me.DimentionD = product.GetValue(Of Decimal)("DimentionD")
            Me.DimentionH = product.GetValue(Of Decimal)("DimentionH")
            Me.DimentionS = product.GetValue(Of Decimal)("DimentionS")
            Me.DimentionW = product.GetValue(Of Decimal)("DimentionW")
            Me.DownloadToken = product.GetString("DownloadToken")
            Me.FreeToken = product.GetString("FreeToken")
        End With

        If tags.Count > 0 Then
            Me.Tags = (From p In tags Select New DtoTag(p.GetString("TagName").ToString, p.GetValue(Of Integer)("TagID"))).ToList
        End If

        If productPics.Count > 0 Then
            Me.images = (From p In productPics Select PicPath = p.GetString("PicPath")).ToList
        End If


    End Sub
    Public Sub New(product As Product, favorit As UserFavorite, downloadLinks As IEnumerable(Of DownloadLink))
        If product Is Nothing Then
            Exit Sub
        End If

        With product
            Me.ProductId = .ProductId

            If .Menu IsNot Nothing Then
                Me.ProductCategoryTitle = .Menu.Title
            End If

            Me.ProductCategoryId = .ProductCategoryId
            Me.Price = .Price
            Me.images = .ProductPics.Select(Function(x) x.PicPath).ToList
            Me.Description = .Description
            Me.Name = .Name
            Me.Tags = (From p In .ProductTags Select New DtoTag(p.Tag)).ToList
            Me.isFavorite = favorit IsNot Nothing
            Me.IsNew = False
            Me.DimentionD = .DimentionD
            Me.DimentionH = .DimentionH
            Me.DimentionS = .DimentionS
            Me.DimentionW = .DimentionW

            If downloadLinks IsNot Nothing Then
                For Each dl In downloadLinks
                    If dl IsNot Nothing Then
                        If dl.ExpiryDate > Now Then
                            Me.DownloadToken = dl.DownloadToken
                        End If
                    End If
                Next

            End If
            


            If product.ProductFiles IsNot Nothing AndAlso product.ProductFiles.Count > 0 AndAlso product.Price = 0 Then
                Me.FreeToken = product.ProductFiles.First.GUID
            End If

        End With

    End Sub



End Class
<Serializable>
Public Class DtoTag
    Public TagName As String
    Public TagID As Integer
    Public Sub New(tag As Tag)

        If tag Is Nothing Then
            Exit Sub
        End If

        With tag
            Me.TagID = .TagID
            Me.TagName = .Title
        End With

    End Sub


    Public Sub New(tagName As String, tagID As Integer?)

        If Not tagID.HasValue OrElse Not tagName.HasText Then
            Exit Sub
        End If

        Me.TagID = tagID
        Me.TagName = tagName
    End Sub

End Class

