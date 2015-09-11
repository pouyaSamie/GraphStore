Public Class SchemaModel

    Public Property DataBaseName As String
    Public Property SchemaName As String
    Public Property TableName As String
    Public Property DataType As String
    Public Property ColumnName As String

End Class

Public Class SchemaModelCollection
    Inherits List(Of SchemaModel)

End Class
