Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Data.Common

Namespace Repositories

    Public NotInheritable Class SqlHelper

        Public Function PrepArray(Of T)(data As List(Of T)) As String
            Dim xs As New System.Xml.Serialization.XmlSerializer(GetType(List(Of T)))
            Dim ms As New IO.MemoryStream
            xs.Serialize(ms, data)
            Dim out = Text.UTF8Encoding.UTF8.GetString(ms.ToArray)
            Return out
        End Function

        Public Shared Function Instance() As SqlHelper
            Return New SqlHelper
        End Function

        ''' <summary>
        ''' Gets or sets the timeout value, in seconds. 
        ''' the default value is 60 seconds
        ''' </summary>
        ''' <value>Timeout seconds</value>
        Public Property CommandTimeout As Long = 60

        Public Property ConnectionString() As String

        ''' <summary>
        ''' Create a  new instance of SqlHelper with default (0) connection string
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            'If cnn Is Nothing Then cnn = cnns(0)
            Dim cnn = System.Configuration.ConfigurationManager.ConnectionStrings(0)
            Me.ConnectionString = cnn.ConnectionString
            If Me.ConnectionString.Contains("meta") Then
                Dim bulder = New Entity.Core.EntityClient.EntityConnectionStringBuilder(Me.ConnectionString)
                Me.ConnectionString = bulder.ProviderConnectionString
            End If
        End Sub

        Public Sub New(ByVal connectionString As String)
            Me.ConnectionString = connectionString
        End Sub

        Private Function GetConnection() As SqlConnection
            Return New SqlConnection(Me.ConnectionString)
        End Function

        Private Function GetCommand() As SqlCommand
            Dim out As New SqlCommand
            out.CommandTimeout = Me.CommandTimeout
            Return out
        End Function

        ''' <summary>
        ''' Get database schema model
        ''' </summary>
        ''' <param name="schema">filter schema</param>
        ''' <param name="tableName">filter table name</param>
        ''' <param name="dataType">filter data type</param>
        ''' <param name="columnName">filter column name</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSchema(Optional schema As String = "", Optional tableName As String = "", Optional dataType As String = "", Optional columnName As String = "") As SchemaModelCollection

            Dim commandText = "SELECT TABLE_CATALOG as DataBaseName,table_schema as SchemaName,table_name as TableName,DATA_TYPE as DataType,column_name as ColumnName " &
                              "FROM information_schema.columns WHERE 1=1 "
            If tableName.HasText Then commandText &= String.Format("AND Table_Name LIKE '{0}' ", tableName)
            If schema.HasText Then commandText &= String.Format("AND Table_Schema LIKE '{0}' ", schema)
            If dataType.HasText Then commandText &= String.Format("AND Data_Type LIKE '{0}' ", dataType)
            If columnName.HasText Then commandText &= String.Format("AND Column_Name LIKE '{0}' ", columnName)

            Dim _reader = Reader(commandText)
            Dim out As New SchemaModelCollection
            While _reader.Read
                Dim model As New SchemaModel
                With model
                    .DataBaseName = _reader("DataBaseName")
                    .SchemaName = _reader("SchemaName")
                    .TableName = _reader("TableName")
                    .DataType = _reader("DataType")
                    .ColumnName = _reader("ColumnName")
                End With
                out.Add(model)
            End While
            Return out

        End Function

        Private Function DicToSqlParam(dic As Dictionary(Of String, Object)) As SqlParameter()
            Dim result As New List(Of SqlParameter)
            For Each item In dic
                If item.Value IsNot Nothing Then
                    result.Add(New SqlParameter(item.Key, item.Value))
                End If
            Next
            Return result.ToArray
        End Function

#Region "Execute"

        Public Function Execute(ByVal command As SqlCommand) As Integer
            command.Connection.Open()
            Execute = command.ExecuteNonQuery
            command.Connection.Close()
        End Function

        Public Function Execute(ByVal spName As String, ByVal ParamArray parameterValues() As Object) As Integer
            Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(spName, Me.ConnectionString)
            SqlHelperUtility.AssignParameterValues(commandParameters, parameterValues)
            Execute = Me.ExecuteComplete(spName, commandParameters)
        End Function

        Public Function Execute(ByVal spName As String, parameterValues As Dictionary(Of String, Object)) As Integer
            Execute = Me.ExecuteComplete(spName, Me.DicToSqlParam(parameterValues))
        End Function

        Public Function ExecuteComplete(ByVal spName As String, ByVal commandParameters() As SqlParameter) As Integer

            Dim out

            Dim cmd As SqlCommand = Me.GetCommand
            cmd.Connection = Me.GetConnection
            cmd.CommandText = spName
            cmd.CommandType = CommandType.StoredProcedure

            Using cn = Me.GetConnection
                For Each sqlParameter In commandParameters
                    cmd.Parameters.Add(sqlParameter)
                Next
                out = Me.Execute(cmd)
            End Using

            Return out

        End Function

        Public Function Execute(ByVal commandText As String) As Integer
            Dim out

            Dim cmd As SqlCommand = Me.GetCommand
            Using cn = Me.GetConnection
                SqlHelperUtility.PrepareCommand(cmd, cn, CommandType.Text, commandText, Nothing)
                out = Me.Execute(cmd)
            End Using

            Return out

        End Function

#End Region

#Region " Scalar "

        Public Function Scalar(ByVal command As SqlCommand) As Object
            command.Connection.Open()
            Scalar = command.ExecuteScalar
            command.Connection.Close()
        End Function

        Public Function ExecuteScalarComplete(ByVal spName As String, ByVal commandParameters() As SqlParameter) As Integer

            Dim out

            Dim cmd As SqlCommand = Me.GetCommand
            cmd.Connection = Me.GetConnection
            cmd.CommandText = spName
            cmd.CommandType = CommandType.StoredProcedure

            Using cn = Me.GetConnection
                For Each sqlParameter In commandParameters
                    cmd.Parameters.Add(sqlParameter)
                Next
                out = Me.ExecuteScalar(cmd)
            End Using

            Return out

        End Function

        Public Function ExecuteScalar(ByVal command As SqlCommand) As Integer
            command.Connection.Open()
            ExecuteScalar = command.ExecuteScalar
            command.Connection.Close()
        End Function

        Public Function Scalar(ByVal spName As String, ByVal ParamArray parameterValues() As Object) As Object
            Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(spName, Me.ConnectionString)
            SqlHelperUtility.AssignParameterValues(commandParameters, parameterValues)
            Scalar = Me.ExecuteScalarComplete(spName, commandParameters)
        End Function

        Public Function Scalar(ByVal spName As String, parameterValues As Dictionary(Of String, Object)) As Object
            Scalar = Me.ExecuteScalarComplete(spName, Me.DicToSqlParam(parameterValues))
        End Function

        Public Function Scalar(ByVal commandText As String) As Object
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = 60
            Dim cn As New SqlConnection(Me.ConnectionString)
            SqlHelperUtility.PrepareCommand(cmd, cn, CommandType.Text, commandText, Nothing)
            Scalar = Me.Scalar(cmd)
            cmd.Dispose()
            cn.Dispose()
        End Function

#End Region

#Region "SingleRow"

        Public Function SingleRow(ByVal command As SqlCommand) As SqlDataReader
            command.Connection.Open()
            SingleRow = command.ExecuteReader(CommandBehavior.SingleRow And CommandBehavior.CloseConnection)
        End Function

        Public Function SingleRow(ByVal spName As String, ByVal ParamArray parameterValues() As Object) As SqlDataReader
            Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(spName, Me.ConnectionString)
            SqlHelperUtility.AssignParameterValues(commandParameters, parameterValues)
            SingleRow = Me.SingleRow(spName, commandParameters)
        End Function

        Public Function SingleRow(ByVal commandText As String) As SqlDataReader
            Dim cmd As SqlCommand = Me.GetCommand
            cmd.Connection = Me.GetConnection
            Dim cn As New SqlConnection(Me.ConnectionString)
            SqlHelperUtility.PrepareCommand(cmd, cn, CommandType.Text, commandText, Nothing)
            SingleRow = Me.SingleRow(cmd)
        End Function

#End Region

#Region "Reader"

        Public Function Reader(ByVal command As SqlCommand) As SqlDataReader
            Try
                command.Connection.Open()
                Reader = command.ExecuteReader(CommandBehavior.CloseConnection)
            Catch ex As Exception
                Throw New Exception("ConnectionString: " & command.Connection.ConnectionString, ex)
            End Try
        End Function

        Public Function Reader(ByVal spName As String, ByVal ParamArray parameterValues() As Object) As SqlDataReader
            Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(spName, Me.ConnectionString)
            SqlHelperUtility.AssignParameterValues(commandParameters, parameterValues)
            Dim cmd As SqlCommand = Me.GetCommand
            cmd.Connection = Me.GetConnection
            SqlHelperUtility.PrepareCommand(cmd, cmd.Connection, CommandType.StoredProcedure, spName, commandParameters)
            Reader = Me.Reader(cmd)
        End Function

        Public Function Reader(ByVal commandText As String) As DbDataReader
            Dim cmd As SqlCommand = Me.GetCommand
            cmd.Connection = Me.GetConnection
            SqlHelperUtility.PrepareCommand(cmd, cmd.Connection, CommandType.Text, commandText, Nothing)
            Reader = Me.Reader(cmd)
        End Function

#End Region

#Region "Query"

        Public Function Query(ByVal command As SqlCommand) As DataSet
            Dim dataAdatpter As New SqlDataAdapter(command)
            Query = New DataSet
            dataAdatpter.Fill(Query)
            dataAdatpter.Dispose()
        End Function

        Public Function Query(ByVal spName As String, parameterValues As Dictionary(Of String, Object)) As DataSet

            Dim cmd As SqlCommand = Me.GetCommand
            cmd.CommandTimeout = Me.CommandTimeout
            cmd.Connection = Me.GetConnection
            cmd.CommandText = spName
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddRange(Me.DicToSqlParam(parameterValues))

            Using cn = Me.GetConnection
                cn.Open()
                Return Me.Query(cmd)
            End Using

        End Function

        Public Function Query(ByVal spName As String, ByVal ParamArray parameterValues() As Object) As DataSet
            Dim commandParameters As SqlParameter() = SqlHelperParameterCache.GetSpParameterSet(spName, Me.ConnectionString)
            SqlHelperUtility.AssignParameterValues(commandParameters, parameterValues)

            Dim cmd As SqlCommand = Me.GetCommand
            cmd.CommandTimeout = Me.CommandTimeout
            cmd.Connection = Me.GetConnection
            cmd.CommandText = spName
            cmd.CommandType = CommandType.StoredProcedure

            Using cn = Me.GetConnection
                cn.Open()
                For Each sqlParameter In commandParameters
                    cmd.Parameters.Add(sqlParameter)
                Next
                Return Me.Query(cmd)
            End Using

        End Function

        Public Function Query(ByVal commandText As String) As DataSet
            Dim cmd As SqlCommand = Me.GetCommand
            cmd.Connection = Me.GetConnection
            SqlHelperUtility.PrepareCommand(cmd, cmd.Connection, CommandType.Text, commandText, Nothing)
            Query = Me.Query(cmd)
            cmd.Dispose()
        End Function

#End Region

    End Class

End Namespace