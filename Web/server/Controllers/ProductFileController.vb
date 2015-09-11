Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http
Imports System.Net
Imports Web.Controllers
Imports System.Data.Entity

<RoutePrefix("api/files")>
Public Class ProductFileController
    Inherits BaseController

    <Route("~/api/UploadFile")>
    <HttpPost>
    Public Async Function Upload(productID As Integer) As Task(Of HttpResponseMessage)

        ' Check if the request contains multipart/form-data.
        If Not Request.Content.IsMimeMultipartContent() Then
            Throw New HttpResponseException(HttpStatusCode.UnsupportedMediaType)
        End If

        'todo: change path 
        Dim root As String = HttpContext.Current.Server.MapPath("~/data/ProductFiles/")

        Try
            Dim fileGuid = Guid.NewGuid.ToString
            Dim provider As New CustomMultipartFormDataStreamProvider(root, fileGuid)
            Await Request.Content.ReadAsMultipartAsync(provider)

            If provider.FileData.Count = 0 Then
                Throw New HttpResponseException(HttpStatusCode.BadRequest)
            End If

            Dim file = provider.FileData(0)

            Dim uploadedFile As New ProductFile
            With uploadedFile
                .GUID = fileGuid
                .ProductID = productID
                .FileName = file.Headers.ContentDisposition.FileName.Replace("""", "")
                .Capacity = file.Headers.ContentDisposition.Size
            End With

            Me.ProductFilesRepository.Add(uploadedFile)
            Me.ProductFilesRepository.Commit()

            Return Request.CreateResponse(HttpStatusCode.OK, New List(Of ProductFile)({uploadedFile}))

        Catch ex As Exception

            Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex)

        End Try

    End Function

    <Route("~/api/RemoveFile")>
  <HttpPost>
    Public Function Remove(fileViewModel As DtoFile) As HttpResponseMessage

        If fileViewModel Is Nothing Then
            Return Request.CreateResponse(HttpStatusCode.NotFound)
        End If

        Dim file = (From p In Me.ProductFilesRepository.GetAll Where p.GUID = fileViewModel.Guid AndAlso p.FileName = fileViewModel.Name).SingleOrDefault
        If file Is Nothing Then
            Return Request.CreateResponse(HttpStatusCode.Gone)
        End If

        Dim projectFile = (From p In Me.ProductsRepository.GetAll Where p.ProductFiles.Any(Function(x) x.GUID = fileViewModel.Guid)).SingleOrDefault

        If projectFile IsNot Nothing Then
            Me.ProductsRepository.Remove(projectFile)
        End If





        Me.ProductFilesRepository.Remove(file)
        Me.ProductFilesRepository.Commit()

        Dim filePath As String = HttpContext.Current.Server.MapPath("~/data/ProductFiles/" & fileViewModel.Guid)
        If System.IO.File.Exists(filePath) Then
            System.IO.File.Delete(filePath)
        End If

        Return Request.CreateResponse(HttpStatusCode.OK)

    End Function

    <Route("{token}")>
    <HttpGet>
    Public Function GetByGuid(token As String) As HttpResponseMessage


        Dim downloadLink = (From p In Me.DownloadLinksRepository.GetAll.Include(Function(x) x.OrderDetail).Include(Function(x) x.OrderDetail.Product).Include(Function(x) x.OrderDetail.Product.ProductFiles)
                    Where p.DownloadToken = token _
                    AndAlso p.ExpiryDate > Now _
                    AndAlso p.UserID = CurrentUserId _
                    AndAlso p.OrderDetail.Order.UserID = CurrentUserId _
                    Select p).FirstOrDefault



        ' Dim file = (From p In Me.ProductFilesRepository.GetAll Where p.GUID = id).SingleOrDefault
        If downloadLink Is Nothing Then
            Return Request.CreateResponse(HttpStatusCode.Gone)
        End If

        Dim file = downloadLink.OrderDetail.Product.ProductFiles.FirstOrDefault

        If file Is Nothing Then
            Return Request.CreateResponse(HttpStatusCode.Gone)
        End If

        'todo: change path 
        Dim filePath As String = HttpContext.Current.Server.MapPath("~/data/ProductFiles/" & file.GUID)


        If Not System.IO.File.Exists(filePath) Then
            Return Request.CreateResponse(HttpStatusCode.Gone)
        End If

        Dim result As HttpResponseMessage

        result = Request.CreateResponse(HttpStatusCode.OK)
        result.Content = New StreamContent(New System.IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read))
        result.Content.Headers.ContentDisposition = New System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        result.Content.Headers.ContentDisposition.FileName = file.FileName

        Return result

    End Function


    <Route("GetFree/{FreeToken}")>
       <HttpGet>
    Public Function GetFreeByGuid(freeToken As String) As HttpResponseMessage
        Return Request.CreateResponse("BBBBBBBBB")
        Dim file = (From p In Me.ProductFilesRepository.GetAll Where p.GUID = freeToken).SingleOrDefault
        If file Is Nothing Then
            Return Request.CreateResponse(HttpStatusCode.Gone)
        End If

        'todo: change path 
        Dim filePath As String = HttpContext.Current.Server.MapPath("~/data/ProductFiles/" & file.GUID)

        If Not System.IO.File.Exists(filePath) Then
            Return Request.CreateResponse(HttpStatusCode.Gone)
        End If

        Dim result As HttpResponseMessage

        result = Request.CreateResponse(HttpStatusCode.OK)
        result.Content = New StreamContent(New System.IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read))
        result.Content.Headers.ContentDisposition = New System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        result.Content.Headers.ContentDisposition.FileName = file.FileName

        Return result

    End Function


End Class

Public Class CustomMultipartFormDataStreamProvider
    Inherits MultipartFormDataStreamProvider

    Private _fileName As String

    Public Sub New(rootPath As String, bufferSize As Integer, fileName As String)
        MyBase.New(rootPath, bufferSize)
        _fileName = fileName
    End Sub

    Public Sub New(rootPath As String, fileName As String)
        MyBase.New(rootPath)
        _fileName = fileName
    End Sub

    Public Overrides Function GetLocalFileName(headers As Headers.HttpContentHeaders) As String
        Return _fileName
        'Return MyBase.GetLocalFileName(headers)
    End Function

End Class
