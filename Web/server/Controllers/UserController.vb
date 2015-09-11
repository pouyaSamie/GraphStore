Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports Web.Repositories.EF
Imports Microsoft.Owin
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.AspNet.Identity
Imports System.Linq.Expressions
Imports System.Data.Entity

Namespace Controllers

    Public Class UserController
        Inherits BaseController

#Region " Private Members "

        Private Property UserRepository As Object

        Private Function GetAvailableQuery(Optional criteria As Expression(Of Func(Of User, Boolean)) = Nothing) As IQueryable(Of User)

            Dim query = From p In Me.UsersRepository.GetAll

            'including profile picture
            'query = From p In query.Include(Function(x) x.File)

            'applying filter
            If criteria IsNot Nothing Then
                query = From p In query.Where(criteria)
            End If

            Return query.Distinct

        End Function

        Private Function GetAvailableDto(Optional criteria As Expression(Of Func(Of User, Boolean)) = Nothing) As List(Of DtoUser)

            Return New DtoUsers(GetAvailableQuery(criteria).ToList)

        End Function

#End Region

#Region " CRUD "

        Public Function Update(data As DtoUser) As ResponseModel

            If data.UserId <> CurrentUserId Then
                Return ResponseModel.Create(HttpStatusCode.Forbidden)
            End If

            Dim user As User = (From p In GetAvailableQuery() Where p.Id = data.UserId).SingleOrDefault

            If user Is Nothing Then
                Return ResponseModel.Create(HttpStatusCode.NotFound)
            End If

            data.FillUser(user)

            Me.UserRepository.Update(user)
            Me.UserRepository.Commit()

            'Me.Logger.Log(user, UserActions.Update)

            Return ResponseModel.Create(HttpStatusCode.NoContent)

        End Function


#End Region

#Region " Report "

        Public Function GetAllMinimal() As ResponseModel

            Return ResponseModel.Create(HttpStatusCode.OK, GetAvailableDto)

        End Function

        Public Function GetById(id As Integer) As ResponseModel

            Dim criteria As Expression(Of Func(Of User, Boolean)) = Function(x) x.Id = id
            Dim data As DtoUser = (From p In GetAvailableDto(criteria)).SingleOrDefault

            If data Is Nothing Then
                Return ResponseModel.Create(HttpStatusCode.NotFound)
            End If

            Return ResponseModel.Create(HttpStatusCode.OK, data)


        End Function

        Public Function GetCurrent() As ResponseModel

            Dim criteria As Expression(Of Func(Of User, Boolean)) = Function(x) x.Id = CurrentUserId
            Dim data As DtoUser = (From p In GetAvailableDto(criteria)).SingleOrDefault

            If data Is Nothing Then
                Return ResponseModel.Create(HttpStatusCode.NotFound)
            End If

            Return ResponseModel.Create(HttpStatusCode.OK, data)


        End Function


#End Region

    End Class

End Namespace