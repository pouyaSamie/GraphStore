Imports System.Collections.Concurrent
Imports System.Web.Http.ModelBinding
Imports System.Web.Http.ValueProviders


Public Interface IFilterModel
    Property PageSize As Integer?
    Property PageNumber As Integer?
    Property SortField As String
    Property SortDirection As String
    Property SearchTerm As String
End Interface

Public Class FilterModel
    Implements IFilterModel



    Public Property PageNumber As Integer? = Nothing Implements IFilterModel.PageNumber

    Public Property PageSize As Integer? = Nothing Implements IFilterModel.PageSize

    Public Property SearchTerm As String = "" Implements IFilterModel.SearchTerm

    Public Property SortDirection As String = "" Implements IFilterModel.SortDirection

    Public Property SortField As String = "" Implements IFilterModel.SortField

    Public Sub New(pageNumber As Integer, PageSize As Integer)
        Me.PageSize = PageSize
        Me.PageNumber = pageNumber
    End Sub

    Public Sub New()
        Me.PageSize = 16
        Me.PageNumber = 1
    End Sub

End Class
Public Class FilteringModelBinder
    Implements IModelBinder


    '/ List of known locations.
    Private Shared _filters As Concurrent.ConcurrentDictionary(Of String, FilterModel) = New ConcurrentDictionary(Of String, FilterModel)(StringComparer.OrdinalIgnoreCase)

    Public Function BindModel(actionContext As Http.Controllers.HttpActionContext, bindingContext As ModelBindingContext) As Boolean Implements IModelBinder.BindModel
        If (bindingContext.ModelType IsNot GetType(FilterModel)) Then
            Return False
        End If

        Dim val As ValueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName)
        If val Is Nothing Then
            Return False
        End If

        Dim [key] = val.RawValue
        If [key] Is Nothing Then

            bindingContext.ModelState.AddModelError(
            bindingContext.ModelName, "Wrong value type")
            Return False

        End If

        Dim result As New FilterModel

        If _filters.TryGetValue(key, result) Then
            bindingContext.Model = result
            Return True
        End If
        bindingContext.ModelState.AddModelError(
                            bindingContext.ModelName, "Cannot convert value to Location")

        Return False
    End Function






End Class
