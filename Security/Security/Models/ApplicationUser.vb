Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Security.Claims
Imports System.Threading.Tasks

Public Class ApplicationUser
    Inherits IdentityUser(Of Integer, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim)

    Public Function GenerateUserIdentity(manager As UserManager(Of ApplicationUser, Integer), authenticationType As String) As ClaimsIdentity
        ' Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        Dim userIdentity = manager.CreateIdentity(Me, authenticationType)
        ' Add custom user claims here
        Return userIdentity
    End Function

    Public Async Function GenerateUserIdentityAsync(manager As UserManager(Of ApplicationUser, Integer), authenticationType As String) As Task(Of ClaimsIdentity)
        ' Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        Dim userIdentity = Await manager.CreateIdentityAsync(Me, authenticationType)
        ' Add custom user claims here
        Return userIdentity
    End Function


    Public Property DisplayName As String = ""
    Public Property FullName As String = ""
    Public Property Picture As String
    Public Property Description As String = ""
    Public Property Mobile As String = ""
    Public Property Home As String = ""
    Public Property SecondaryEmail As String = ""
    Public Property Address As String = ""
    Public Property Status As Short = 1
    Public Property RecruitmentStartDate As DateTime = Now
    Public Property RecruitmentEndDate As DateTime?
    Public Property CreateDate As DateTime = Now
    Public Property LastUpdateDate As DateTime = Now



End Class
