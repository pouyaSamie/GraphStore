Imports Microsoft.Owin
Imports Owin
Imports System.Web.Hosting
Imports System.Web.Http
Imports Microsoft.Owin.Security.OAuth
Imports Security
Imports Newtonsoft.Json

<Assembly: OwinStartup("DefaultStartup", GetType(Startup))> 
Public Class Startup

    Public Sub Configuration(app As IAppBuilder)


        app.ConfigureAuth()

        Dim config As New HttpConfiguration()
        config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
        config.MapHttpAttributeRoutes()
        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{action}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )

        app.UseWebApi(config)
        config.SuppressDefaultHostAuthentication()
        config.Filters.Add(New HostAuthenticationFilter(OAuthDefaults.AuthenticationType))

        Dim jsonFormatter = config.Formatters.OfType(Of Net.Http.Formatting.JsonMediaTypeFormatter).First()
        jsonFormatter.SerializerSettings.ContractResolver = New Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver
        config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore

        'Test Date UTC
        jsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local

    End Sub

End Class