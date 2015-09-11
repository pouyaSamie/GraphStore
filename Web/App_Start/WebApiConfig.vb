Imports System.Web.Http
Imports Microsoft.Owin.Security.OAuth
Imports System.Net.Http.Headers
Imports Newtonsoft.Json.Serialization


Public Module WebApiConfig
    Public Sub Register(config As HttpConfiguration)
        ' Web API configuration and services
        ' Configure Web API to use only bearer token authentication.
        config.SuppressDefaultHostAuthentication()
        config.Filters.Add(New HostAuthenticationFilter(OAuthDefaults.AuthenticationType))

        ' Web API routes
        config.MapHttpAttributeRoutes()
        config.Routes.MapHttpRoute(
                 name:="DefaultApi",
                 routeTemplate:="api/{controller}/{action}/{id}",
                 defaults:=New With {.id = RouteParameter.Optional}
             )

        
        config.Formatters.JsonFormatter.SupportedMediaTypes.Add(New MediaTypeHeaderValue("text/html"))
        config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented
        config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = New CamelCasePropertyNamesContractResolver()
        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore

    End Sub
End Module