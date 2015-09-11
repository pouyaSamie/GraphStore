Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Routing
Imports System.Web.Http

Public Class MvcApplication
    Inherits System.Web.HttpApplication
    Protected Sub Application_Start()

        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)

    End Sub

 
 
End Class