using JobPortal.Model;
using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public interface IOrderRepository: IRepository<OrderTable>
    {
        OrderTable CreateOrder(string u);
        OrderTable AddToExistingOrder(OrderTable o, ShoppingCart cart);
        OrderTable PayForOrder(OrderTable o);
        bool DeleteFromCart(ShoppingCart cart);
        bool AddToCart(ShoppingCart cart);
        List<ShoppingCart> GetShoppingCart(string id);
        bool CancelOrder(Order o);
        IList<TimeSpan> GetHoursFrom(int serviceId, DateTime date);
        IList<TimeSpan> GetHoursTo(int serviceId, DateTime date, TimeSpan from);
        bool CleanCart(string userID);
        IQueryable<Salelines> GetJobCalendar(DateTime date, string employeeId);
        IQueryable<Salelines> GetAllSalelines();
        IQueryable<OrderTable> GetAllOrders();

    }
}
