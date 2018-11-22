using JobPortal.Model;
using Repository.DbConnection;
using System;
using System.Collections.Generic;

namespace Repository.OrderRepository
{
    public interface IOrderRepository: IRepository<OrderTable>
    {
        OrderTable CreateOrder(Users u, IList<KeyValuePair<ServiceOffer, JobPortal.Model.BookedDate>> choosenServices);
        OrderTable AddToExistingOrder(OrderTable o, ServiceOffer s, JobPortal.Model.BookedDate date);
        OrderTable DeleteFromExistingOrder(OrderTable o, ServiceOffer s, DateTime date, TimeSpan from, TimeSpan to);
        OrderTable PayForOrder(OrderTable o);
    }
}
