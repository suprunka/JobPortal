using JobPortal.Model;
using Repository.DbConnection;
using System;
using System.Collections.Generic;

namespace Repository.OrderRepository
{
    public interface IOrderRepository: IRepository<OrderTable>
    {
        OrderTable CreateOrder(Users u);
        OrderTable AddToExistingOrder(OrderTable o, ShoppingCart cart);
        OrderTable PayForOrder(OrderTable o);
        bool AddToCart(ShoppingCart cart);
    }
}
