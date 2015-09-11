Public Class BaseEnumDescriptor(Of T)


    Private Property dic As Dictionary(Of T, String)
    Public Sub New()
        Me.dic = New Dictionary(Of T, String)
        Dim enumType = GetType(T)
        If Not enumType.IsEnum Then
            Throw New NotSupportedException(String.Format("{0} Is not a Enum ", enumType.FullName))
        End If

        Dim enumArray = [Enum].GetValues(enumType)

        For Each item In enumArray
            Dim enumValue = DirectCast([Enum].Parse(enumType, item.ToString), T)
            Dim EnumText = Me.GetEnumDescriptor(enumValue)
            If Me.dic.ContainsKey(enumValue) Then
                Throw New Exception(String.Format("Enum {0} and {1} have Same Value : {2}", EnumText, dic(enumValue), enumValue))
            End If
            dic.Add(enumValue, EnumText)
        Next
    End Sub

    Private Function GetEnumDescriptor(item As T) As String
        Dim type = item.GetType
        Dim value = type.GetField(type.ToString())
        Dim descriptor = TryCast(value.GetCustomAttributes(GetType(EnumDescriptorAttribute), False), EnumDescriptorAttribute())
        If descriptor Is Nothing Then
            Throw New Exception(String.Format("Description For Enum {0}.{1} Not Found", type, value))
        End If

        If descriptor.Length <> 1 Then
            Throw New Exception(String.Format("Invalid Enum! Enum : {0}.{1}", type, value))
        End If

        Return descriptor(0).Description
    End Function

End Class

Public Class EnumDescriptorAttribute
    Inherits Attribute

    Private Property _description As String
    Public ReadOnly Property Description As String
        Get
            Return Me._description
        End Get
    End Property
    Public Sub New(description As String)
        Me._description = description
    End Sub

End Class

