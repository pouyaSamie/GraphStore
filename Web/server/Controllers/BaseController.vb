Imports System.Web.Http
Imports Web.Repositories
Imports Web.Repositories.EF

Namespace Controllers

    <Authorize>
    Public Class BaseController
        Inherits NotAuthBaseController

    End Class

    Public Class NotAuthBaseController
        Inherits ApiController

        Public ReadOnly Property CurrentUserId As Integer?
            Get
                Return If(Application.GetCurrentUserId = 0, Nothing, Application.GetCurrentUserId)
            End Get
        End Property





        Public Property ProductsRepository As IGenericRepository(Of Product, Integer)
        Public Property UsersRepository As IGenericRepository(Of User, Integer)
        Public Property ProductsCategoryRepository As IGenericRepository(Of ProductCategory, Integer)
        Public Property OrdersRepository As IGenericRepository(Of Order, Integer)
        Public Property ProductPicsRepository As IGenericRepository(Of ProductPic, Integer)
        Public Property SentMailsRepository As IGenericRepository(Of SentMail, Integer)
        Public Property ProductTagsRepository As IGenericRepository(Of ProductTag, Integer)
        Public Property SlidePhotosRepository As IGenericRepository(Of SlidePhoto, Integer)
        Public Property ProductFilesRepository As IGenericRepository(Of ProductFile, Integer)
        Public Property TagsRepository As IGenericRepository(Of Tag, Integer)
        Public Property UserProductCategoryFavoritesRepository As IGenericRepository(Of UserProductCategoryFavorite, Integer)
        Public Property OrderDetailsRepository As IGenericRepository(Of OrderDetail, Integer)
        Public Property MenuRepository As IGenericRepository(Of Menu, Integer)
        'Public Property Logger As Logger
        Public Property UserFavoritesRepository As IGenericRepository(Of UserFavorite, Integer)
        Public Property DownloadLinksRepository As IGenericRepository(Of DownloadLink, Integer)
        Public Sub New()

            Dim dbcontext As New DefaultContext
            ProductsRepository = New GenericRepository(Of Product, Integer)(dbcontext)
            ProductFilesRepository = New GenericRepository(Of ProductFile, Integer)(dbcontext)
            UsersRepository = New GenericRepository(Of User, Integer)(dbcontext)
            ProductsCategoryRepository = New GenericRepository(Of ProductCategory, Integer)(dbcontext)
            OrdersRepository = New GenericRepository(Of Order, Integer)(dbcontext)
            ProductPicsRepository = New GenericRepository(Of ProductPic, Integer)(dbcontext)
            ProductTagsRepository = New GenericRepository(Of ProductTag, Integer)(dbcontext)
            SentMailsRepository = New GenericRepository(Of SentMail, Integer)(dbcontext)
            SlidePhotosRepository = New GenericRepository(Of SlidePhoto, Integer)(dbcontext)
            TagsRepository = New GenericRepository(Of Tag, Integer)(dbcontext)
            UserProductCategoryFavoritesRepository = New GenericRepository(Of UserProductCategoryFavorite, Integer)(dbcontext)
            OrderDetailsRepository = New GenericRepository(Of OrderDetail, Integer)(dbcontext)
            MenuRepository = New GenericRepository(Of Menu, Integer)(dbcontext)
            'Logger = New Logger(dbcontext)
            UserFavoritesRepository = New GenericRepository(Of UserFavorite, Integer)(dbcontext)

            DownloadLinksRepository = New GenericRepository(Of DownloadLink, Integer)(dbcontext)
        End Sub


    End Class
End Namespace

