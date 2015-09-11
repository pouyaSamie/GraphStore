Imports System.Diagnostics
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports System.Web.Http
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security.OAuth
Imports Newtonsoft.Json.Linq
Imports Security.Satellizer.Models
Imports Security
Imports Security.Satellizer

Namespace Satellizer.Controllers
    <RoutePrefix("auth")> _
    Public Class AuthController
        Inherits ApiController

#Region "Owin Bits"

        Private ReadOnly Property UserManager() As ApplicationUserManager
            Get
                Return Request.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            End Get
        End Property

#End Region

        Public Function PostFacebook(code As String) As HttpResponseMessage
            Return Request.CreateResponse(HttpStatusCode.NotImplemented, New HttpError("Have not written code yet"))
        End Function

        <Route("signup")> _
        <HttpPost> _
        Public Async Function PostSignUp(signUpInfo As SignUpInfo) As Task(Of HttpResponseMessage)
            Dim user = Await UserManager.FindByEmailAsync(signUpInfo.Email)

            Dim hasRegistered As Boolean = user IsNot Nothing

            If Not hasRegistered Then
                Dim appUser = New ApplicationUser() With { _
                    .UserName = signUpInfo.Email, _
                    .Email = signUpInfo.Email, _
                    .DisplayName = signUpInfo.DisplayName _
                }

                Dim result = UserManager.Create(appUser, signUpInfo.Password)

                If result.Succeeded Then
                    Dim accessTokenResponse = GenerateLocalAccessTokenResponse(appUser)

                    Return Request.CreateResponse(HttpStatusCode.OK, accessTokenResponse)
                End If

                Return Request.CreateResponse(HttpStatusCode.BadRequest, New HttpError(result.Errors.First()))
            End If

            Return Request.CreateResponse(HttpStatusCode.BadRequest)

        End Function

        <Route("login")> _
        <HttpPost> _
        Public Async Function PostLogin(loginInfo As LoginInfo) As Task(Of HttpResponseMessage)
            Dim user = Await UserManager.FindAsync(loginInfo.Email, loginInfo.Password)

            If user IsNot Nothing Then
                Dim accessTokenResponse = GenerateLocalAccessTokenResponse(user)

                Return Request.CreateResponse(HttpStatusCode.OK, accessTokenResponse)
            End If
            Return Request.CreateResponse(DirectCast(422, HttpStatusCode), New HttpError("Bad username or password"))
        End Function

        <Route("unlink/{loginProvider}")> _
        <Authorize> _
        Public Async Function GetUnlink(loginProvider As String) As Task(Of HttpResponseMessage)
            Dim appUser = Await UserManager.FindByIdAsync(User.Identity.GetUserId())

            Dim userLoginInfo = appUser.Logins.FirstOrDefault(Function(x) x.LoginProvider.Equals(loginProvider, StringComparison.InvariantCultureIgnoreCase))
            If userLoginInfo IsNot Nothing Then
                Dim result = Await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), New UserLoginInfo(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey))

                Return If(result.Succeeded, Request.CreateResponse(HttpStatusCode.NoContent), Request.CreateResponse(HttpStatusCode.BadRequest, New HttpError(result.Errors.FirstOrDefault())))
            End If

            Return Request.CreateResponse(HttpStatusCode.NotFound, New HttpError("Login providor not found"))
        End Function

        <Route("Google")> _
        <HttpPost> _
        Public Async Function PostGoogle(externalInfo As ExternalInfo) As Task(Of HttpResponseMessage)
            Dim auth2AuthenticationHandler As New GoogleOAuth2Handler(New HttpClient())

            Dim googleOAuthProfile = Await auth2AuthenticationHandler.ProcessToken(externalInfo.Code, externalInfo.RedirectUri)

            Dim clameIdentity = googleOAuthProfile.Id
            Dim email = googleOAuthProfile.Emails.First().Email

            Dim appUser As ApplicationUser = Await UserManager.FindAsync(New UserLoginInfo("Google", clameIdentity))

            Dim hasRegisteredWithAccount As Boolean = appUser IsNot Nothing

            If Not hasRegisteredWithAccount Then
                Dim info = New ExternalLoginInfo() With { _
                    .DefaultUserName = email, _
                    .Login = New UserLoginInfo("Google", clameIdentity) _
                }

                Try
                    If User.Identity.IsAuthenticated Then
                        appUser = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
                        If String.IsNullOrEmpty(appUser.Picture) Then
                            appUser.Picture = googleOAuthProfile.Image.Url
                            Await UserManager.UpdateAsync(appUser)
                        End If
                    Else
                        appUser = New ApplicationUser() With { _
                            .UserName = email, _
                            .Email = email, _
                            .DisplayName = googleOAuthProfile.DisplayName,
                            .Picture = googleOAuthProfile.Image.Url _
                        }
                        UserManager.Create(appUser)
                    End If

                    UserManager.AddLogin(appUser.Id, info.Login)
                Catch exception As Exception
                    Return Request.CreateResponse(HttpStatusCode.InternalServerError, New HttpError(exception, True))
                End Try
            End If

            Dim accessTokenResponse = GenerateLocalAccessTokenResponse(appUser)

            Return Request.CreateResponse(HttpStatusCode.OK, accessTokenResponse)
        End Function

        Public Function PostLinkedin(code As String) As HttpResponseMessage
            Return Request.CreateResponse(HttpStatusCode.NotImplemented, New HttpError("Have not written code yet"))
        End Function

        Public Function GetTwitter(oauth As String) As HttpResponseMessage
            Return Request.CreateResponse(HttpStatusCode.NotImplemented, New HttpError("Have not written code yet"))
        End Function

        Private Shared Function GenerateLocalAccessTokenResponse(applicationUser As ApplicationUser) As JObject
            Dim identity = New ClaimsIdentity(OAuthDefaults.AuthenticationType)

            identity.AddClaim(New Claim(ClaimTypes.Name, applicationUser.UserName))
            identity.AddClaim(New Claim(ClaimTypes.NameIdentifier, applicationUser.Id))
            identity.AddClaim(New Claim("sub", applicationUser.UserName))
            identity.AddClaim(New Claim(ClaimTypes.Role, "user"))

            Return Startup.OAuthBearerOptions.CreateAuthResponse(identity, applicationUser.UserName)
        End Function

    End Class
End Namespace