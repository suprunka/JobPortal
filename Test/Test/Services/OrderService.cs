using JobPortal.Model;
using ServiceLibrary.ServiceInterfaces;
using System;
using System.ServiceModel;

namespace ServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                 ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class OrderService : IOrderService
    {
        public Order AddToExistingOrder(Offer o)
        {
            throw new NotImplementedException();
        }

        public bool CancelOrder(Order o)
        {
            throw new NotImplementedException();
        }

        public Order CreateOrder(User u, Offer o, int hours)
        {
            Order order = new Order(u);
            lock (typeof(OrderService))
            {
                order.AddOffer(o, hours);
            }
            return order;
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
