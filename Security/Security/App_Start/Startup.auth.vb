Imports Microsoft.Owin.Cors
Imports Owin
Imports Security.Satellizer.Models
Imports Security.Satellizer.Providers
Imports System.Runtime.CompilerServices
Imports Security.Satellizer

Public Module Startup
    Public Const TOKEN_SERVICE_URL As String = "/api/Token"
    Public Const REGISTER_SERVICE_URL As String = "/api/Account/ExternalLogin"

    Public Property OAuthBearerOptions() As SatellizerJwtBearerOptions
        Get
            Return m_OAuthBearerOptions
        End Get
        Private Set(value As SatellizerJwtBearerOptions)
            m_OAuthBearerOptions = value
        End Set
    End Property
    Private m_OAuthBearerOptions As SatellizerJwtBearerOptions

    Private Property PublicClientId() As String
        Get
            Return m_PublicClientId
        End Get
        Set(value As String)
            m_PublicClientId = value
        End Set
    End Property
    Private m_PublicClientId As String

    ' For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
    <Extension>
    Public Sub ConfigureAuth(app As IAppBuilder)
        ' Configure the db context and user manager to use a single instance per request
        app.CreatePerOwinContext(AddressOf ApplicationDbContext.Create)
        app.CreatePerOwinContext(Of ApplicationUserManager)(AddressOf ApplicationUserManager.Create)

        app.UseCors(CorsOptions.AllowAll)

        PublicClientId = "self"

        OAuthBearerOptions = New SatellizerJwtBearerOptions("localhost", PublicClientId, "+UX5jSMcWqjNVJED2t4JLjtwCkcqxA7al3M5APPLtNK=", TimeSpan.FromHours(1))

        app.UseJwtBearerAuthentication(OAuthBearerOptions)

        GoogleOAuth2Handler.ClientId = "631036554609-v5hm2amv4pvico3asfi97f54sc51ji4o.apps.googleusercontent.com"
        GoogleOAuth2Handler.ClientSecret = "Google Client Secret"

    End Sub
End Module

