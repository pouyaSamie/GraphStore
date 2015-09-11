Imports System.Net
Imports System.Web.Http
Imports Web.Controllers
Imports System.Data.Entity

<RoutePrefix("api/favorite")>
Public Class UserFavoriteController
    Inherits BaseController

    <Route("")>
    <HttpPost>
    Public Function AddToFavorit(productID As Integer) As ResponseModel

        If Not CurrentUserId.HasValue Then
            Return ResponseModel.Create(HttpStatusCode.NotAcceptable)
        End If

        Dim item = (From p In Me.UserFavoritesRepository.GetAll
             Where p.UserID = CurrentUserId _
             AndAlso p.ProductId = productID).SingleOrDefault
        If item IsNot Nothing Then
            Return ResponseModel.Create(HttpStatusCode.NotAcceptable)
        End If

        Dim userFavorite = New UserFavorite
        With userFavorite
            .UserID = CurrentUserId
            .ProductId = productID
        End With

        Me.UserFavoritesRepository.Add(userFavorite)
        Me.UserFavoritesRepository.Commit()
        Return ResponseModel.Create(HttpStatusCode.OK)
    End Function
    <Route("~/api/Unfavorite")>
    <HttpPost>
    Public Function DeleteByProductID(productID As Integer) As ResponseModel

        If Not CurrentUserId.HasValue Then
            Return ResponseModel.Create(HttpStatusCode.NotAcceptable)
        End If

        Dim item = (From p In Me.UserFavoritesRepository.GetAll
                Where p.UserID = CurrentUserId _
                AndAlso p.ProductId = productID).SingleOrDefault

        If item Is Nothing Then
            Return ResponseModel.Create(HttpStatusCode.OK)
        End If

        Me.UserFavoritesRepository.Remove(item)
        Return ResponseModel.Create(HttpStatusCode.OK)
    End Function
    <Route("~/api/MyFavorites")>
    <HttpGet>
    Public Function GetcurrentUserFavorites() As List(Of DTOProduct)

        Dim filtering = New FilterModel
        filtering.SortField = filtering.SortField
        Dim favoriteTables = ProductService.Instance.GetUserFavorites(CurrentUserId, filtering)

        Dim productList As New List(Of DTOProduct)

        For Each Product As DataRow In favoriteTables.Products.Rows

            Dim tags = (From p In favoriteTables.Tags Where p("ProductId") = Product("ProductId")).ToList
            Dim productPics = (From p In favoriteTables.productPics Where p("ProductId") = Product("ProductId")).ToList

            productList.Add(New DTOProduct(Product, tags, productPics))
        Next

        Return productList

    End Function

End Class