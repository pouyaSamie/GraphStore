Imports System.Data.SqlClient

Public Class ProductService

    Public Function GetGallery(userId As Integer?, productCateguryId As Integer?, productId As Integer?) As GalleryTables
        Return Me.GetGallery(userId, productCateguryId, productId, Nothing, Nothing, Nothing, Nothing)
    End Function

    Public Function GetGallery(userId As Integer?, productCateguryId As Integer?, productId As Integer?, filtering As FilterModel) As GalleryTables
        Return Me.GetGallery(userId, productCateguryId, productId, filtering.PageSize, filtering.PageNumber, filtering.SortField, filtering.SortDirection)
    End Function

    Public Function GetGallery(userId As Integer?, productCateguryId As Integer?, productId As Integer?,
                               pageSize As Integer?, pageNumber As Integer?, sortExpression As String, sortDirection As String) As GalleryTables

        Dim catID = New SqlParameter("@CatID", productCateguryId)
        Dim userIDParameter = New SqlParameter("@userID", userId)
        Dim ProductIDParameter = New SqlParameter("@productID", productId)

        Dim pageSizeParameter = New SqlParameter("@pageSize", pageSize)
        Dim pageNumberParameter = New SqlParameter("@pageNumber", pageNumber)
        Dim sortExpressionParameter = New SqlParameter("@sortExpression", sortExpression)
        Dim sortDirectionParameter = New SqlParameter("@sortDirection", sortDirection)

        Dim ds = Repositories.SqlHelper.Instance.Query("GetGallery", catID, userIDParameter, ProductIDParameter, pageSizeParameter, pageNumberParameter, sortExpressionParameter, sortDirectionParameter)

        Return New GalleryTables(ds)
    End Function
    Public Function GetUserFavorites(userId As Integer?, filtering As FilterModel) As GalleryTables
        Return Me.GetUserFavorites(userId, filtering.PageSize, filtering.PageNumber, filtering.SortField, filtering.SortDirection)
    End Function

    Public Function GetUserFavorites(userId As Integer?, pageSize As Integer?, pageNumber As Integer?, sortExpression As String, sortDirection As String) As GalleryTables


        Dim userIDParameter = New SqlParameter("@userID", userId)
        Dim pageSizeParameter = New SqlParameter("@pageSize", pageSize)
        Dim pageNumberParameter = New SqlParameter("@pageNumber", pageNumber)
        Dim sortExpressionParameter = New SqlParameter("@sortExpression", sortExpression)
        Dim sortDirectionParameter = New SqlParameter("@sortDirection", sortDirection)

        Dim ds = Repositories.SqlHelper.Instance.Query("GetUserFavorited", userIDParameter, pageSizeParameter, pageNumberParameter, sortExpressionParameter, sortDirectionParameter)

   

        Return New GalleryTables(ds)
    End Function

    Public Shared Function Instance() As ProductService
        Return New ProductService
    End Function

End Class

Public Class GalleryTables

    Public Property Products As DataTable

    Public Property productPics As DataTable
    Public Property Tags As DataTable

    Public Sub New()
        Me.Products = New DataTable
        Me.productPics = New DataTable
        Me.Tags = New DataTable
    End Sub

    Public Sub New(ds As DataSet)

        Me.Products = ds.Tables(0)
        Me.productPics = ds.Tables(1)
        Me.Tags = ds.Tables(2)

    End Sub

 

End Class