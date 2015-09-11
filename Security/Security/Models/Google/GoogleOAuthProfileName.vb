Imports Newtonsoft.Json

Namespace Satellizer.Models.Google
    Public Class GoogleOAuthProfileName
        <JsonProperty("familyName")> _
        Public Property FamilyName() As String
            Get
                Return m_FamilyName
            End Get
            Set(value As String)
                m_FamilyName = Value
            End Set
        End Property
        Private m_FamilyName As String
        <JsonProperty("givenName")> _
        Public Property GivenName() As String
            Get
                Return m_GivenName
            End Get
            Set(value As String)
                m_GivenName = Value
            End Set
        End Property
        Private m_GivenName As String
        <JsonProperty("middleName")> _
        Public Property MiddleName() As String
            Get
                Return m_MiddleName
            End Get
            Set(value As String)
                m_MiddleName = Value
            End Set
        End Property
        Private m_MiddleName As String
    End Class
End Namespace
