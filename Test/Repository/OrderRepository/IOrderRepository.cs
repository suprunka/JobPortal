using JobPortal.Model;
using Repository.DbConnection;
using System;

namespace Repository.OrderRepository
{
    public interface IOrderRepository: IRepository<OrderTable>
    {
        OrderTable CreateOrder(Users u);
        OrderTable AddToExistingOrder(OrderTable o, ServiceOffer s, DateTime date, TimeSpan from, TimeSpan to);
        OrderTable DeleteFromExistingOrder(OrderTable o, ServiceOffer s, DateTime date, TimeSpan from, TimeSpan to);
        OrderTable PayForOrder(OrderTable o);
    }
}
