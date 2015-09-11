Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web
Imports System.Web.Http
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin

Namespace Satellizer.Controllers
    <Authorize> _
    <RoutePrefix("api/me")> _
    Public Class MeController
        Inherits ApiController
        Private _userManager As ApplicationUserManager

        Public Sub New()
        End Sub

        Public Sub New(userManager__1 As ApplicationUserManager)
            UserManager = userManager__1
        End Sub

        Public Property UserManager() As ApplicationUserManager
            Get
                Return If(_userManager, HttpContext.Current.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
            End Get
            Private Set(value As ApplicationUserManager)
                _userManager = value
            End Set
        End Property

        ' GET api/Me
        <Route("")> _
        Public Function GetValues() As HttpResponseMessage
            Dim result = New GetViewModel()

            Dim user__1 = UserManager.FindById(User.Identity.GetUserId())

            result.DisplayName = user__1.DisplayName
            result.Picture = user__1.Picture
            result.Id = user__1.Id
            result.Email = user__1.Email

            Dim logins = user__1.Logins
            For Each login In logins
                If login.LoginProvider = "Google" Then
                    result.Google = login.ProviderKey
                End If
            Next

            Return Request.CreateResponse(HttpStatusCode.OK, result)
        End Function

        <Route("")> _
        <HttpPost>
        Public Async Function Update(viewModel As GetViewModel) As Task(Of HttpResponseMessage)
            Dim user__1 = UserManager.FindById(User.Identity.GetUserId())

            user__1.DisplayName = viewModel.DisplayName
            user__1.Email = viewModel.Email

            Await UserManager.UpdateAsync(user__1)

            Return Request.CreateResponse(HttpStatusCode.OK)
        End Function

        <Route("~/me/UploadPic")>
  <HttpPost>
        Public Async Function Upload() As Task(Of HttpResponseMessage)

            ' Check if the request contains multipart/form-data.
            If Not Request.Content.IsMimeMultipartContent() Then
                Throw New HttpResponseException(HttpStatusCode.UnsupportedMediaType)
            End If

            'todo: change path 
            Dim root As String = HttpContext.Current.Server.MapPath("~/data/ProfilePictures/")

            Try
                Dim fileGuid = Guid.NewGuid.ToString
                Dim provider As New CustomMultipartFormDataStreamProvider(root, fileGuid)
                Await Request.Content.ReadAsMultipartAsync(provider)

                If provider.FileData.Count = 0 Then
                    Throw New HttpResponseException(HttpStatusCode.BadRequest)
                End If

                Dim file = provider.FileData(0)

                

                Dim user__1 = UserManager.FindById(User.Identity.GetUserId())
                user__1.Picture = fileGuid
                Await UserManager.UpdateAsync(user__1)
                Return Request.CreateResponse(HttpStatusCode.OK)

            Catch ex As Exception

                Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex)

            End Try

        End Function


    End Class


    Public Class GetViewModel
        Public Property DisplayName() As String
            Get
                Return m_DisplayName
            End Get
            Set(value As String)
                m_DisplayName = Value
            End Set
        End Property
        Private m_DisplayName As String
        Public Property Email() As String
            Get
                Return m_Email
            End Get
            Set(value As String)
                m_Email = Value
            End Set
        End Property
        Private m_Email As String
        Public Property Picture() As String
            Get
                Return m_Picture
            End Get
            Set(value As String)
                m_Picture = Value
            End Set
        End Property
        Private m_Picture As String
        Public Property Google() As String
            Get
                Return m_Google
            End Get
            Set(value As String)
                m_Google = Value
            End Set
        End Property
        Private m_Google As String
        Public Property Id() As String
            Get
                Return m_Id
            End Get
            Set(value As String)
                m_Id = Value
            End Set
        End Property
        Private m_Id As String
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

End Namespace
