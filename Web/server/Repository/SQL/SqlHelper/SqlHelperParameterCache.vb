Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Data.Common

Namespace Repositories

    Friend NotInheritable Class SqlHelperParameterCache

        Private Shared paramCache As Hashtable = Hashtable.Synchronized(New Hashtable)

        Private Shared Function CloneParameters(ByVal ParamArray originalParameters() As SqlParameter) As SqlParameter()
            Dim i As Integer
            Dim j As Integer = originalParameters.Length - 1
            Dim clonedParameters(j) As SqlParameter
            For i = 0 To j
                clonedParameters(i) = CType(CType(originalParameters(i), ICloneable).Clone, SqlParameter)
            Next
            Return clonedParameters
        End Function

        Private Shared Function DiscoverSpParameterSet(ByVal spName As String, ByVal connectionString As String)
            Dim cn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand(spName, cn)
            cmd.CommandTimeout = 60
            cmd.CommandType = CommandType.StoredProcedure
            cn.Open()
            SqlCommandBuilder.DeriveParameters(cmd)
            cn.Close()
            cmd.Parameters.RemoveAt(0)
            DiscoverSpParameterSet = New SqlParameter(cmd.Parameters.Count - 1) {}
            cmd.Parameters.CopyTo(DiscoverSpParameterSet, 0)
            For Each discoveredParameter As SqlParameter In DiscoverSpParameterSet
                discoveredParameter.Value = DBNull.Value
            Next
            cmd.Dispose()
            cn.Dispose()
        End Function

        Public Shared Function GetSpParameterSet(ByVal spName As String, ByVal connectionString As String) As SqlParameter()

            Dim cachedParameters() As SqlParameter = CType(paramCache(spName & connectionString), SqlParameter())

            If (cachedParameters Is Nothing) Then
                Dim spParameters() As SqlParameter = DiscoverSpParameterSet(spName, connectionString)
                paramCache(spName & connectionString) = spParameters
                cachedParameters = spParameters
            End If

            Return CloneParameters(cachedParameters)

        End Function

        Private Sub New()
        End Sub

    End Class

End Namespace