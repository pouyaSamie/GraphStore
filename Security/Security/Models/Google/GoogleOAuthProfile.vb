Imports System.Collections.Generic
Imports Newtonsoft.Json

Namespace Satellizer.Models.Google
    Public Class GoogleOAuthProfile
        <JsonProperty("kind")> _
        Public Property Kind() As String
            Get
                Return m_Kind
            End Get
            Set(value As String)
                m_Kind = Value
            End Set
        End Property
        Private m_Kind As String
        <JsonProperty("gender")> _
        Public Property Gender() As String
            Get
                Return m_Gender
            End Get
            Set(value As String)
                m_Gender = Value
            End Set
        End Property
        Private m_Gender As String

        <JsonProperty("emails")> _
        Public Property Emails() As List(Of GoogleOAuthProfileEmails)
            Get
                Return m_Emails
            End Get
            Set(value As List(Of GoogleOAuthProfileEmails))
                m_Emails = Value
            End Set
        End Property
        Private m_Emails As List(Of GoogleOAuthProfileEmails)
        <JsonProperty("id")> _
        Public Property Id() As String
            Get
                Return m_Id
            End Get
            Set(value As String)
                m_Id = Value
            End Set
        End Property
        Private m_Id As String

        <JsonProperty("displayName")> _
        Public Property DisplayName() As String
            Get
                Return m_DisplayName
            End Get
            Set(value As String)
                m_DisplayName = Value
            End Set
        End Property
        Private m_DisplayName As String

        <JsonProperty("name")> _
        Public Property Name() As GoogleOAuthProfileName
            Get
                Return m_Name
            End Get
            Set(value As GoogleOAuthProfileName)
                m_Name = Value
            End Set
        End Property
        Private m_Name As GoogleOAuthProfileName
        <JsonProperty("image")> _
        Public Property Image() As GoogleOAuthProfileImage
            Get
                Return m_Image
            End Get
            Set(value As GoogleOAuthProfileImage)
                m_Image = Value
            End Set
        End Property
        Private m_Image As GoogleOAuthProfileImage
    End Class
End Namespace
