Imports System.Net
Imports System.Web.Http
Imports Web.Controllers
Imports System.Data.Entity

<RoutePrefix("api/Basket")>
Public Class OrderController
    Inherits BaseController



    Public Class SelectedList
        Public Property productIds As List(Of Integer)

    End Class

    <Route("")>
<HttpPost>
    Public Function AddOrder(<FromBody> productIds As SelectedList) As ResponseModel

        Dim ids = (From p In productIds.productIds Select p).ToList
        If productIds Is Nothing OrElse productIds.productIds.Count = 0 Then
            Return ResponseModel.Create(HttpStatusCode.BadRequest)
        End If


        Dim order As New Order
        With order
            .IsPaied = False
            .TrakingNumber = Guid.NewGuid.ToString
            .UserID = CurrentUserId
        End With

        Dim orderdetails = (From p In Me.ProductsRepository.GetAll.Include(Function(x) x.ProductFiles)
                             Where ids.Contains(p.ProductId)).ToList

        Dim totalAmount = orderdetails.Sum(Function(x) x.Price)
        order.Amount = totalAmount

        If totalAmount = 0 Then
            order.IsPaied = True
        End If

        For Each item In orderdetails
            Dim orderDetail = New OrderDetail
            With orderDetail
                .Price = item.Price
                .ProductId = item.ProductId
                .Quantity = 1

                For Each download In item.ProductFiles
                    Dim downloadLink = New DownloadLink
                    With downloadLink
                        .Downloaded = False
                        .DownloadToken = Guid.NewGuid.ToString()
                        .ExpireAfterDownload = False
                        .ExpiryDate = Now.AddYears(1)
                        .Hits = 0
                        .Url = download.GUID
                        .UserID = CurrentUserId
                    End With

                    .DownloadLinks.Add(downloadLink)

                Next

            End With
            order.OrderDetails.Add(orderDetail)
        Next

        Me.OrdersRepository.Add(order)
        Me.OrdersRepository.Commit()
        Return ResponseModel.Create(HttpStatusCode.OK, Nothing, order.TrakingNumber)

    End Function

    <Route("")>
    <HttpGet>
    Public Function GetMyOrders(<FromUri> filtering As FilterModel) As List(Of DtoOrder)

        Dim q = (From p In Me.OrdersRepository.GetAll.Include(Function(x) x.OrderDetails).Include(Function(x) x.OrderDetails.Select(Function(d) d.DownloadLinks)).Where((Function(x) x.OrderDetails.Any(Function(o) o.Order.UserID = CurrentUserId AndAlso o.Order.IsPaied AndAlso o.DownloadLinks.Any(Function(d) d.ExpiryDate > Now))))
                  Where p.UserID = CurrentUserId
                  Select p).ToList

        Return (From p In q Select New DtoOrder(p)).ToList


    End Function


End Class

