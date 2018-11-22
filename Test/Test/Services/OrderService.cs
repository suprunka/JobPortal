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

       
        public Order AddToExistingOrder(Offer o)
        {
            throw new NotImplementedException();
        }

        public bool CancelOrder(Order o)
        {
            throw new NotImplementedException();
        }

        public Order CreateOrder(Users u , IList<KeyValuePair<ServiceOffer, JobPortal.Model.BookedDate>> choosenServices)
        {
            Order order = null;
            try {
               OrderTable o =  _database.CreateOrder(u, choosenServices);
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
            /*
             It looks like you're missing something very important: WCF may or may not always be executing in the same process. If WCF operates in a single process and you lock on a static object, you will have a valid lock.

           If, on the other hand WCF is not operating in a single process (IIS hosting, for example, could cause this) then your lock would be worthless, as one process would have a lock, but the other process would not.

           WCF serves a very specific purpose: client-server communications. Well, there's a little more to it than that, but the vast majority of WCF programming is for SOAP or RESTful web services. When programming such 
           services, it's almost always a good idea to avoid locks because a service may be serving more than one request at a time and locks will cause the other request to pause (which may incur a timeout).
                        */
            /*
 The worst you can do here is a singleton service.

Database engines (well, most of them) are well-suited for concurrent calls. What do you want to restrict their concurrent features for?

The best way for you is a per-call, stateless WCF service.

 */
            /*
             As I ve said, when you expose WCF services over HTTP as Web services, chances are you ll be using PerCall configuration. Sessions for WCF Web services are usually better 
             facilitated by persisting data between calls to a database, rather than using an application
             session (which is not durable). That means the default concurrency mode setting of Single will 
             not reduce the potential throughput of requests to your application.
                        */
        }
        //https://stackoverflow.com/questions/2199159/wcf-service-with-concurrencymode-multiple-and-instancecontextmode-single-behavio


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
