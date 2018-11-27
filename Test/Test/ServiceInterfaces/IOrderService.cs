using JobPortal.Model;
using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceLibrary
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        [FaultContract(typeof(BookedTimeException))]
        Order CreateOrder(string Logging_ID);

        [OperationContract]
        bool CancelOrder(Order o);

        [OperationContract]
        ShoppingCard GetShoppingCard(string id);

        [OperationContract]
        bool PayForOrder(Order o);

        [OperationContract]
        bool CleanCart(string userId);

        [OperationContract]
        bool AddToCart(string userId, int serviceId, DateTime date, TimeSpan hourfrom, TimeSpan hourTo);

        [OperationContract]
        bool DeleteFromCart(string userId, int serviceId, DateTime date, TimeSpan hourfrom, TimeSpan hourTo);

        [OperationContract]
        IEnumerable<TimeSpan> GetHoursFrom(int serviceId, DateTime date);

        [OperationContract]
        IEnumerable<TimeSpan> GetHoursTo(int serviceId, DateTime date, TimeSpan from);

        [OperationContract]
        ShoppingCard GetShoppingCardForPaypal(string id);


    }
}
