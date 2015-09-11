Public Class AppSettingHelper

    Public Shared Function GetProductFilePath() As String
        Return ConfigurationManager.AppSettings("ProductFilePath")
    End Function

    Public Shared Function GetSlidePhotosPath() As String
        Return ConfigurationManager.AppSettings("SlidePhotoPath")
    End Function


    Public Shared Function GetProductPath(itemPath As String) As String
        Return IO.Path.Combine(AppSettingHelper.GetProductFilePath(), itemPath)
    End Function

    Public Shared Function GetSlidePhotoPath(itemPath As String) As String
        Return IO.Path.Combine(AppSettingHelper.GetSlidePhotosPath(), itemPath)
    End Function

End Class
