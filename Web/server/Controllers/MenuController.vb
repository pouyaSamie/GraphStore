Imports System.Net
Imports System.Web.Http
Imports Web.Controllers

Public Class MenuController
    Inherits NotAuthBaseController

    Public Function GetAll() As IList
        Dim x = (From p In Me.MenuRepository.GetAll.ToList
                Where p.ParentId Is Nothing
                Select p.MenuId,
                        p.ParentId,
                        p.Title, p.Childs
                        ).ToList
        Return x
    End Function
End Class