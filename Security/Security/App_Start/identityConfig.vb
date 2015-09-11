
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Security.Satellizer.Models

Namespace Satellizer
    ' Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    Public Class ApplicationUserManager
        Inherits UserManager(Of ApplicationUser, Integer)
        Public Sub New(store As IUserStore(Of ApplicationUser, Integer))
            MyBase.New(store)
        End Sub

        Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationUserManager), context As IOwinContext) As ApplicationUserManager
            Dim manager = New ApplicationUserManager(New UserStore(Of ApplicationUser, ApplicationRole, Integer, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim)(context.[Get](Of ApplicationDbContext)()))
            ' Configure validation logic for usernames
            manager.UserValidator = New UserValidator(Of ApplicationUser, Integer)(manager) With { _
                .AllowOnlyAlphanumericUserNames = False, _
                .RequireUniqueEmail = True _
            }
            ' Configure validation logic for passwords
            manager.PasswordValidator = New PasswordValidator() With { _
                .RequiredLength = 6, _
                .RequireNonLetterOrDigit = True, _
                .RequireDigit = False, _
                .RequireLowercase = False, _
                .RequireUppercase = False _
            }
            Dim dataProtectionProvider = options.DataProtectionProvider
            If dataProtectionProvider IsNot Nothing Then
                manager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser, Integer)(dataProtectionProvider.Create("ASP.NET Identity"))
            End If
            Return manager
        End Function
    End Class
End Namespace


