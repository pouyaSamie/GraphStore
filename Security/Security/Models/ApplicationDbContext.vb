Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports System.Data.Entity
Imports System.Data.Entity.Validation
Imports System.Web
Imports Security.Satellizer.Models

Public Class ApplicationDbContext
    Inherits IdentityDbContext(Of ApplicationUser, ApplicationRole, Integer, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim)

    Public Sub New()
        MyBase.New("DefaultConnection")
        MyBase.Configuration.LazyLoadingEnabled = False
        MyBase.Configuration.ProxyCreationEnabled = False
        Database.SetInitializer(New AuthDBInitializer)
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As Entity.DbModelBuilder)

        MyBase.OnModelCreating(modelBuilder)

        modelBuilder.Entity(Of ApplicationUser).ToTable("Users")
        modelBuilder.Entity(Of ApplicationRole).ToTable("Roles")
        modelBuilder.Entity(Of ApplicationUserRole)().ToTable("UserRoles")
        modelBuilder.Entity(Of ApplicationUserLogin)().ToTable("UserLogins")
        modelBuilder.Entity(Of ApplicationUserClaim)().ToTable("UserClaims")

    End Sub

    Public Shared Function Create() As ApplicationDbContext
        Return New ApplicationDbContext()
    End Function

End Class

Public Class AuthDBInitializer
    Inherits NullDatabaseInitializer(Of ApplicationDbContext)


End Class