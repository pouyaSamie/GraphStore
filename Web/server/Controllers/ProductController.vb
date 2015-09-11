Imports System.Net
Imports System.Web.Http
Imports Web.Controllers

Public Class ProductController
    Inherits NotAuthBaseController

    Public Function GetProductInfo(id As Integer) As Object

        Dim ds = ProductService.Instance.GetGallery(CurrentUserId, Nothing, id)

        Dim product = (From p In ds.Products
                        Select p).SingleOrDefault

        If product Is Nothing Then
            Return Nothing
        End If

        Dim tags = (From p In ds.Tags
                    Select p).ToList

        Dim pics = (From p In ds.productPics
                     Select p).ToList

        Return New DTOProduct(product, tags, pics)
    End Function

    <HttpGet>
    Public Function GetList(id As Integer?, <FromUri> filtering As FilterModel) As List(Of DTOProduct)

        Dim ds = ProductService.Instance.GetGallery(CurrentUserId, id, Nothing, filtering)

        Dim product = (From p In ds.Products
                        Select p).ToList

        If product Is Nothing Then
            Return Nothing
        End If


        Dim productList = New List(Of DTOProduct)

        For Each item In ds.Products.Rows
            Dim tags = (From p In ds.Tags Where p("ProductId") = item("ProductId")).ToList
            Dim productPics = (From p In ds.productPics Where p("ProductId") = item("ProductId")).ToList

            productList.Add(New DTOProduct(item, tags, productPics))
        Next

        Return productList


    End Function

End Class