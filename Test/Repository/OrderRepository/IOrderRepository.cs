using JobPortal.Model;
using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.OrderRepository
{
    public interface IOrderRepository: IRepository<OrderTable>
    {
        OrderTable CreateOrder(Users u);
        OrderTable AddToExistingOrder(OrderTable o, ShoppingCart cart);
        OrderTable PayForOrder(OrderTable o);
        bool DeleteFromCart(ShoppingCart cart);
        bool AddToCart(ShoppingCart cart);
        List<ShoppingCart> GetShoppingCart(string id);
    }
}
