using JobPortal.Model;
using Repository;
using Repository.DbConnection;
using Repository.OrderRepository;
using ServiceLibrary.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceLibrary.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                 ConcurrencyMode = ConcurrencyMode.Single)]
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _database;

        public OrderService(IOrderRepository database)
        {
            _database = database;
        }



        public bool CancelOrder(Order o)
        {
            throw new NotImplementedException();
        }

        public Order CreateOrder(Users u)
        {
            Order order = null;
            try {
               OrderTable o =  _database.CreateOrder(u);
                List<JobPortal.Model.Saleline> salelines = new List<JobPortal.Model.Saleline>();
                foreach (var item in o.Salelines)
                {
                    salelines.Add( new JobPortal.Model.Saleline {Id=  item.ID, ServiceOfferId= item.ServiceOffer_ID });
                }
                order = new Order { ID = o.ID, TotalPrice = o.TotalPrice, OrderStatus = o.OrderStatus.Order_status, User_ID = o.Users_ID, Salelines = salelines };
            }
            catch(BookedTimeException e)
            {
                throw new FaultException<BookedTimeException>
                 (e, new FaultReason(e.Message), new FaultCode("Sender"));
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
       public bool AddToCart(string userId, int serviceId, DateTime date, TimeSpan hourfrom, TimeSpan hourTo)
       {
           return _database.AddToCart(new ShoppingCart
            {
                User_ID= userId,
                Service_ID = serviceId,
                Date = date,
                HourFrom = hourfrom,
                HourTo = hourTo

            });
       }
    }
}
