Imports System.Linq.Expressions

Namespace Repositories

    Public Interface IGenericRepository(Of TEntity As Class, TKey)
        Inherits IRepository

        Function GetAll() As IQueryable(Of TEntity)
        Function GetById(id As TKey) As TEntity
        Sub Add(entity As TEntity)
        Sub Update(entity As TEntity)
        Sub Remove(id As TKey)
        Sub Remove(entity As TEntity)
        Function Search(criteria As Expression(Of Func(Of TEntity, Boolean)), sortFields As IList(Of String), sortDirections As IList(Of String), pageSize As Integer?, pageNumber As Integer?, ByRef totalItems As Integer) As IQueryable(Of TEntity)
        Function Search(data As IQueryable(Of TEntity), criteria As Expression(Of Func(Of TEntity, Boolean)), sortFields As IList(Of String), sortDirections As IList(Of String), pageSize As Integer?, pageNumber As Integer?, ByRef totalItems As Integer) As IQueryable(Of TEntity)

        Sub Commit()

    End Interface

    Public Interface IGenericRepository(Of TEntity As Class)
        Inherits IGenericRepository(Of TEntity, Long)

    End Interface

End Namespace