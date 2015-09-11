Imports System.Globalization
Imports System.IdentityModel.Tokens
Imports System.Security.Claims
Imports Microsoft.Owin.Security.Jwt
Imports Newtonsoft.Json.Linq

Namespace Satellizer.Providers
    Public Class SatellizerJwtBearerOptions
        Inherits JwtBearerAuthenticationOptions
        Private ReadOnly _issuer As String
        Private ReadOnly _audience As String
        Private ReadOnly _expireTimeSpan As TimeSpan
        Private ReadOnly _key As Byte()
        Private Const SignatureAlgorithm As String = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"
        Private Const DigestAlgorithm As String = "http://www.w3.org/2001/04/xmlenc#sha256"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="issuer"></param>
        ''' <param name="audience"></param>
        ''' <param name="base64Secret">The symmetric key a JWT is signed with</param>
        ''' <param name="expireTimeSpan"></param>
        Public Sub New(issuer As String, audience As String, base64Secret As String, expireTimeSpan As TimeSpan)
            _issuer = issuer
            _audience = audience
            _expireTimeSpan = expireTimeSpan

            _key = Convert.FromBase64String(base64Secret)

            AllowedAudiences = {audience}.AsEnumerable
            IssuerSecurityTokenProviders = {New SymmetricKeyIssuerSecurityTokenProvider(issuer, _key)}
        End Sub

        Private Function Protect(identity As ClaimsIdentity) As String
            Dim signingCredentials = New SigningCredentials(New InMemorySymmetricSecurityKey(_key), SignatureAlgorithm, DigestAlgorithm)
            Dim token = New JwtSecurityToken(_issuer, _audience, identity.Claims, DateTime.UtcNow, TokenExpireDateTime.UtcDateTime, signingCredentials)
            Return New JwtSecurityTokenHandler().WriteToken(token)
        End Function

        Private ReadOnly Property TokenExpireDateTime() As DateTimeOffset
            Get
                Return DateTimeOffset.UtcNow.Add(_expireTimeSpan)
            End Get
        End Property

        Public Function CreateAuthResponse(identity As ClaimsIdentity, userName As String) As JObject
            Return New JObject(New JProperty("userName", userName), New JProperty("token", Protect(identity)), New JProperty("token_type", "bearer"), New JProperty("expires_in", _expireTimeSpan.TotalSeconds.ToString(CultureInfo.InvariantCulture)), New JProperty(".issued", DateTimeOffset.UtcNow.ToString()), New JProperty(".expires", TokenExpireDateTime.ToString()))

        End Function
    End Class
End Namespace
