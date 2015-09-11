'Imports System.Net.Http
'Imports System.Threading.Tasks
'Imports System.Web.Http
'Imports System.Net
'Imports Web.Repositories.EF
'Imports Web.Repositories

'Namespace Controllers

'    Public Class FileController
'        Inherits BaseController

'        Public Async Function Upload() As Task(Of HttpResponseMessage)

'            ' Check if the request contains multipart/form-data.
'            If Not Request.Content.IsMimeMultipartContent() Then
'                Throw New HttpResponseException(HttpStatusCode.UnsupportedMediaType)
'            End If

'            'todo: change path 
'            Dim root As String = HttpContext.Current.Server.MapPath("~/App_Data/Files/")

'            Try
'                Dim fileGuid = Guid.NewGuid.ToString
'                Dim provider As New CustomMultipartFormDataStreamProvider(root, fileGuid)
'                Await Request.Content.ReadAsMultipartAsync(provider)

'                If provider.FileData.Count = 0 Then
'                    Throw New HttpResponseException(HttpStatusCode.BadRequest)
'                End If

'                Dim file = provider.FileData(0)

'                Dim uploadedFile As New File
'                With uploadedFile
'                    .CreateById = Application.GetCurrentUserId
'                    .CreateDate = Now
'                    .Guid = fileGuid
'                    .Name = file.Headers.ContentDisposition.FileName.Replace("""", "")
'                    .Size = file.Headers.ContentDisposition.Size
'                End With

'                Me.FileRepository.Add(uploadedFile)
'                Me.FileRepository.Commit()

'                Return Request.CreateResponse(HttpStatusCode.OK, New List(Of File)({uploadedFile}))

'            Catch ex As Exception

'                Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex)

'            End Try

'        End Function

'        Public Function Remove(fileViewModel As DtoFile) As HttpResponseMessage

'            If fileViewModel Is Nothing Then
'                Return Request.CreateResponse(HttpStatusCode.NotFound)
'            End If

'            Dim file = (From p In Me.FileRepository.GetAll Where p.Guid = fileViewModel.Guid AndAlso p.Name = fileViewModel.Name).SingleOrDefault
'            If file Is Nothing Then
'                Return Request.CreateResponse(HttpStatusCode.Gone)
'            End If

'            Select Case fileViewModel.EntityType

'                Case EntityTypes.Project
'                    Dim projectFile = (From p In Me.ProjectFilesRepository.GetAll Where p.File.Guid = fileViewModel.Guid).SingleOrDefault
'                    If projectFile IsNot Nothing Then
'                        Me.ProjectFilesRepository.Remove(projectFile)
'                    End If


'            End Select


'            Me.FileRepository.Remove(file)
'            Me.FileRepository.Commit()

'            Dim filePath As String = HttpContext.Current.Server.MapPath("~/App_Data/Files/" & fileViewModel.Guid)
'            If System.IO.File.Exists(filePath) Then
'                System.IO.File.Delete(filePath)
'            End If

'            Return Request.CreateResponse(HttpStatusCode.OK)

'        End Function

'        Public Function GetByGuid(id As String) As HttpResponseMessage

'            Dim file = (From p In Me.FileRepository.GetAll Where p.Guid = id).SingleOrDefault
'            If file Is Nothing Then
'                Return Request.CreateResponse(HttpStatusCode.Gone)
'            End If

'            'todo: change path 
'            Dim filePath As String = HttpContext.Current.Server.MapPath("~/App_Data/Files/" & file.Guid)

'            If Not System.IO.File.Exists(filePath) Then
'                Return Request.CreateResponse(HttpStatusCode.Gone)
'            End If

'            Dim result As HttpResponseMessage

'            result = Request.CreateResponse(HttpStatusCode.OK)
'            result.Content = New StreamContent(New System.IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read))
'            result.Content.Headers.ContentDisposition = New System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
'            result.Content.Headers.ContentDisposition.FileName = file.Name

'            Return result

'        End Function

'    End Class

'    Public Class CustomMultipartFormDataStreamProvider
'        Inherits MultipartFormDataStreamProvider

'        Private _fileName As String

'        Public Sub New(rootPath As String, bufferSize As Integer, fileName As String)
'            MyBase.New(rootPath, bufferSize)
'            _fileName = fileName
'        End Sub

'        Public Sub New(rootPath As String, fileName As String)
'            MyBase.New(rootPath)
'            _fileName = fileName
'        End Sub

'        Public Overrides Function GetLocalFileName(headers As Headers.HttpContentHeaders) As String
'            Return _fileName
'            'Return MyBase.GetLocalFileName(headers)
'        End Function

'    End Class

'End Namespace