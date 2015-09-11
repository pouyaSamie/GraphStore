Imports System.Linq.Expressions

Module Helper

    <System.Runtime.CompilerServices.Extension>
    Public Function OrderBy(Of T)(source As IQueryable(Of T), ordering As String, ParamArray values As Object()) As IQueryable(Of T)
        Dim type = GetType(T)
        Dim prop = type.GetProperty(ordering)
        Dim parameter = Expression.Parameter(type, "p")
        Dim propertyAccess = Expression.MakeMemberAccess(parameter, prop)
        Dim orderByExp = Expression.Lambda(propertyAccess, parameter)
        Dim resultExp As MethodCallExpression = Expression.Call(GetType(Queryable), "OrderBy", New Type() {type, prop.PropertyType}, source.Expression, Expression.Quote(orderByExp))
        Return source.Provider.CreateQuery(Of T)(resultExp)
    End Function

    <System.Runtime.CompilerServices.Extension>
    Public Function OrderByDescending(Of T)(source As IQueryable(Of T), ordering As String, ParamArray values As Object()) As IQueryable(Of T)
        Dim type = GetType(T)
        Dim prop = type.GetProperty(ordering, Reflection.BindingFlags.IgnoreCase)
        Dim parameter = Expression.Parameter(type, "p")
        Dim propertyAccess = Expression.MakeMemberAccess(parameter, prop)
        Dim orderByExp = Expression.Lambda(propertyAccess, parameter)
        Dim resultExp As MethodCallExpression = Expression.Call(GetType(Queryable), "OrderByDescending", New Type() {type, prop.PropertyType}, source.Expression, Expression.Quote(orderByExp))
        Return source.Provider.CreateQuery(Of T)(resultExp)
    End Function

End Module
