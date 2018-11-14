using JobPortal.Model;
using Repository;
using Repository.DbConnection;
using ServiceLibrary.ServiceInterfaces;
using System;
using System.ServiceModel;

namespace ServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                 ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class OrderService : IOrderService
    {
        private readonly IOrderService _database;

        public OrderService(IOrderService database)
        {
            _database = database;
        }

        

        public Order AddToExistingOrder(Offer o)
        {
            throw new NotImplementedException();
        }

        public bool CancelOrder(Order o)
        {
            throw new NotImplementedException();
        }

        public Order CreateOrder(Users u)
        {
            lock (typeof(OrderService))
            {
                return _database.CreateOrder(u);
            }
 
        }

        public Order DeleteFromExistingOrder(Offer o)
        {
            throw new NotImplementedException();
        }

        public bool PayForOrder(Order o)
        {
            throw new NotImplementedException();
        }
    }
}
