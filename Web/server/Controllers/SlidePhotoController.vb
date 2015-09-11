Imports System.Net
Imports System.Web.Http
Imports Web.Controllers

Public Class SlidePhotoController
    Inherits NotAuthBaseController

    Public Function GetAll() As List(Of SlidePhoto)
        Dim result = (From p In Me.SlidePhotosRepository.GetAll
                  Where p.IsActive
                  Select p).ToList

        Return (From p In result
                 Select New SlidePhoto With {.IsActive = p.IsActive, .SlidePhotoId = p.SlidePhotoId, .SlidePhotoPath = AppSettingHelper.GetSlidePhotoPath(p.SlidePhotoPath)}).ToList
    End Function
End Class