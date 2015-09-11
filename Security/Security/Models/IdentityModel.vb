Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Data.Entity

Namespace Satellizer.Models
    ' You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    'Public Class ApplicationUser
    '    Inherits IdentityUser
    '    Public Async Function GenerateUserIdentityAsync(manager As UserManager(Of ApplicationUser), authenticationType As String) As Task(Of ClaimsIdentity)
    '        ' Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    '        Dim userIdentity = Await manager.CreateIdentityAsync(Me, authenticationType)
    '        ' Add custom user claims here
    '        Return userIdentity
    '    End Function

    '    Public Function GenerateUserIdentity(manager As UserManager(Of ApplicationUser), authenticationType As String) As ClaimsIdentity
    '        ' Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    '        Dim userIdentity = manager.CreateIdentity(Me, authenticationType)
    '        ' Add custom user claims here
    '        Return userIdentity
    '    End Function

    '    Public Property DisplayName As String = ""
    '    Public Property FullName As String = ""
    '    Public Property PictureFileId As Integer?
    '    Public Property Description As String = ""
    '    Public Property Mobile As String = ""
    '    Public Property Home As String = ""
    '    Public Property SecondaryEmail As String = ""
    '    Public Property Address As String = ""
    '    Public Property Status As Short = 1
    '    Public Property RecruitmentStartDate As DateTime = Now
    '    Public Property RecruitmentEndDate As DateTime?
    '    Public Property CreateDate As DateTime = Now
    '    Public Property LastUpdateDate As DateTime = Now
    'End Class

    
End Namespace
