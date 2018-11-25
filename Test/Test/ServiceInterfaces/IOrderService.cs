using JobPortal.Model;
using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceLibrary.ServiceInterfaces
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        [FaultContract(typeof(BookedTimeException))]
        Order CreateOrder(Users u);

        [OperationContract]
        bool CancelOrder(Order o);


        [OperationContract]
        bool PayForOrder(Order o);

        [OperationContract]
        bool AddToCart(string userId, int serviceId, DateTime date, TimeSpan hourfrom, TimeSpan hourTo);


        [OperationContract]
        bool DeleteFromCart(string userId, int serviceId, DateTime date, TimeSpan hourfrom, TimeSpan hourTo);
    }
}
