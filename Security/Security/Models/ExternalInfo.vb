Namespace Satellizer.Models
    Public Class ExternalInfo
        Public Property Code() As String
            Get
                Return m_Code
            End Get
            Set(value As String)
                m_Code = Value
            End Set
        End Property
        Private m_Code As String
        Public Property ClientId() As String
            Get
                Return m_ClientId
            End Get
            Set(value As String)
                m_ClientId = Value
            End Set
        End Property
        Private m_ClientId As String
        Public Property RedirectUri() As String
            Get
                Return m_RedirectUri
            End Get
            Set(value As String)
                m_RedirectUri = Value
            End Set
        End Property
        Private m_RedirectUri As String
    End Class

    Public Class SignUpInfo
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
        Public Property Password() As String
            Get
                Return m_Password
            End Get
            Set(value As String)
                m_Password = Value
            End Set
        End Property
        Private m_Password As String
    End Class

    Public Class LoginInfo
        Public Property Email() As String
            Get
                Return m_Email
            End Get
            Set(value As String)
                m_Email = Value
            End Set
        End Property
        Private m_Email As String
        Public Property Password() As String
            Get
                Return m_Password
            End Get
            Set(value As String)
                m_Password = Value
            End Set
        End Property
        Private m_Password As String
    End Class
End Namespace
