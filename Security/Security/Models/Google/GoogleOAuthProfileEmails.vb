Imports Newtonsoft.Json

Namespace Satellizer.Models.Google
    Public Class GoogleOAuthProfileEmails
        <JsonProperty("value")> _
        Public Property Email() As String
            Get
                Return m_Email
            End Get
            Set(value As String)
                m_Email = Value
            End Set
        End Property
        Private m_Email As String
        <JsonProperty("type")> _
        Public Property EmailType() As String
            Get
                Return m_EmailType
            End Get
            Set(value As String)
                m_EmailType = Value
            End Set
        End Property
        Private m_EmailType As String
    End Class
End Namespace
