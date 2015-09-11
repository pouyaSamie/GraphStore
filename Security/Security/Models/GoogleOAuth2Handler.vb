Imports System.Collections.Generic
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Security.Satellizer.Models.Google

Namespace Satellizer.Models
    Public Class GoogleOAuth2Handler

        Public Shared Property ClientId() As String
            Get
                Return m_ClientId
            End Get
            Set(value As String)
                m_ClientId = value
            End Set
        End Property
        Private Shared m_ClientId As String
        Public Shared Property ClientSecret() As String
            Get
                Return m_ClientSecret
            End Get
            Set(value As String)
                m_ClientSecret = value
            End Set
        End Property
        Private Shared m_ClientSecret As String

        Private Const TokenEndpoint As String = "https://accounts.google.com/o/oauth2/token"
        Private Const UserInfoEndpoint As String = "https://www.googleapis.com/plus/v1/people/me"

        Private ReadOnly _httpClient As HttpClient
        Public Sub New(httpClient As HttpClient)
            _httpClient = httpClient
        End Sub

        Public Async Function ProcessToken(code As String, redirectUri As String) As Task(Of GoogleOAuthProfile)
            Dim body = New List(Of KeyValuePair(Of String, String))()
            body.Add(New KeyValuePair(Of String, String)("grant_type", "authorization_code"))
            body.Add(New KeyValuePair(Of String, String)("code", code))
            body.Add(New KeyValuePair(Of String, String)("redirect_uri", redirectUri))

            body.Add(New KeyValuePair(Of String, String)("client_id", ClientId))
            body.Add(New KeyValuePair(Of String, String)("client_secret", ClientSecret))

            ' Request the token
            Dim tokenResponse As HttpResponseMessage = Await _httpClient.PostAsync(TokenEndpoint, New FormUrlEncodedContent(body))
            tokenResponse.EnsureSuccessStatusCode()
            Dim text As String = Await tokenResponse.Content.ReadAsStringAsync()

            ' Deserializes the token response
            Dim response As JObject = JObject.Parse(text)
            Dim accessToken As String = response.Value(Of String)("access_token")

            If String.IsNullOrWhiteSpace(accessToken) Then
                Return Nothing
            End If

            ' Get the Google user info
            Dim request As New HttpRequestMessage(HttpMethod.[Get], UserInfoEndpoint)
            request.Headers.Authorization = New AuthenticationHeaderValue("Bearer", accessToken)
            Dim graphResponse As HttpResponseMessage = Await _httpClient.SendAsync(request)
            graphResponse.EnsureSuccessStatusCode()
            text = Await graphResponse.Content.ReadAsStringAsync()
            Return JsonConvert.DeserializeObject(Of GoogleOAuthProfile)(text)
        End Function
    End Class
End Namespace
