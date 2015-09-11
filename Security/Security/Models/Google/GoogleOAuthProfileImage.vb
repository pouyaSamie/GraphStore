Imports Newtonsoft.Json

Namespace Satellizer.Models.Google
    Public Class GoogleOAuthProfileImage
        <JsonProperty("isDefault")> _
        Public Property IsDefault() As Boolean
            Get
                Return m_IsDefault
            End Get
            Set(value As Boolean)
                m_IsDefault = Value
            End Set
        End Property
        Private m_IsDefault As Boolean
        <JsonProperty("url")> _
        Public Property Url() As String
            Get
                Return m_Url
            End Get
            Set(value As String)
                m_Url = Value
            End Set
        End Property
        Private m_Url As String
    End Class
End Namespace
