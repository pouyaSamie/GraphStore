Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq.Expressions

Namespace Repositories.EF

    Public Class GenericRepository(Of TEntity As Class, TKey)
        Implements IGenericRepository(Of TEntity, TKey)

        Public Property DbContext As DbContext
        Public Property DbSet As IDbSet(Of TEntity)

        Public Sub New(dbContext As DbContext)

            If dbContext Is Nothing Then
                Throw New ArgumentNullException("Null Entity Framework DbContext")
            End If

            Me.DbContext = dbContext
            Me.DbSet = dbContext.Set(Of TEntity)()

        End Sub

        Public Overridable Sub Add(entity As TEntity) Implements IGenericRepository(Of TEntity, TKey).Add

            Dim dbEntityEntry As DbEntityEntry = DbContext.Entry(entity)
            If dbEntityEntry.State <> EntityState.Detached Then
                dbEntityEntry.State = EntityState.Added
            Else
                DbSet.Add(entity)
            End If

        End Sub

        Public Overridable Function GetAll() As IQueryable(Of TEntity) Implements IGenericRepository(Of TEntity, TKey).GetAll
            Return DbSet
        End Function

        Public Overridable Sub Update(entity As TEntity) Implements IGenericRepository(Of TEntity, TKey).Update

            Dim dbEntityEntry As DbEntityEntry = DbContext.Entry(entity)

            If dbEntityEntry.State = EntityState.Detached Then
                DbSet.Attach(entity)
            End If

            dbEntityEntry.State = EntityState.Modified

        End Sub

        Public Overridable Function GetById(id As TKey) As TEntity Implements IGenericRepository(Of TEntity, TKey).GetById
            Return DbSet.Find(id)
        End Function

        Public Overridable Sub Remove(id As TKey) Implements IGenericRepository(Of TEntity, TKey).Remove

            Dim entity = GetById(id)

            If entity Is Nothing Then
                Exit Sub
            End If

            Remove(entity)

        End Sub

        Public Overridable Sub Remove(entity As TEntity) Implements IGenericRepository(Of TEntity, TKey).Remove

            Dim dbEntityEntry As DbEntityEntry = DbContext.Entry(entity)

            If dbEntityEntry.State <> EntityState.Deleted Then
                dbEntityEntry.State = EntityState.Deleted
            Else
                DbSet.Attach(entity)
                DbSet.Remove(entity)
            End If

        End Sub

        Public Overridable Function Search(data As IQueryable(Of TEntity), criteria As Expression(Of Func(Of TEntity, Boolean)), sortFields As IList(Of String), sortDirections As IList(Of String), pageSize As Integer?, pageNumber As Integer?, ByRef totalItems As Integer) As IQueryable(Of TEntity) Implements IGenericRepository(Of TEntity, TKey).Search

            Dim out = From p In data

            'filtering
            If criteria IsNot Nothing Then
                out = out.Where(criteria)
            End If

            ' page count
            totalItems = out.Count()

            'sorting
            If sortFields IsNot Nothing Then

                For i As Integer = 0 To sortFields.Count - 1

                    If sortDirections(i).ToLower = "desc" Then
                        out = out.OrderByDescending(sortFields(i))
                    Else
                        out = out.OrderBy(sortFields(i))
                    End If

                Next

            End If


            'paging
            If Not pageNumber.HasValue Then
                pageNumber = 1
            End If

            If pageSize.HasValue Then
                out = out.Skip((pageNumber - 1) * pageSize).Take(pageSize)
            End If

            Return out

        End Function

        Public Overridable Function Search(criteria As Expression(Of Func(Of TEntity, Boolean)), sortFields As IList(Of String), sortDirections As IList(Of String), pageSize As Integer?, pageNumber As Integer?, ByRef totalItems As Integer) As IQueryable(Of TEntity) Implements IGenericRepository(Of TEntity, TKey).Search
            Return Search(DbSet, criteria, sortFields, sortDirections, pageSize, pageNumber, totalItems)
        End Function

        Public Overridable Sub Commit() Implements IGenericRepository(Of TEntity, TKey).Commit
            DbContext.SaveChanges()
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    DbSet = Nothing
                    DbContext.Dispose()
                    DbContext = Nothing
                End If

            End If
            Me.disposedValue = True
        End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

        
    End Class

    Public Class GenericRepository(Of TEntity As Class)
        Inherits GenericRepository(Of TEntity, Long)
        Implements IGenericRepository(Of TEntity)

        Public Sub New(dbContext As DbContext)
            MyBase.New(dbContext)
        End Sub


    End Class

End Namespace
