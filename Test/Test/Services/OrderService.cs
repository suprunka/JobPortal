using JobPortal.Model;
using Repository;
using Repository.DbConnection;
using Repository.OrderRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace ServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults =true)]
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _database;
        private readonly IOfferRepository _offerRepository;

        public OrderService(IOrderRepository database, IOfferRepository offerRepository)
        {
            _database = database;
            _offerRepository = offerRepository;
        }

        public OrderService()
        {
            _database = new OrderRepository(new JobPortalDatabaseDataContext());
            _offerRepository = new OfferRepository(new JobPortalDatabaseDataContext());
        }

        public ShoppingCard GetShoppingCard(string id)
        {
            ShoppingCard items = new ShoppingCard();

            foreach (var item in _database.GetShoppingCart(id))
            {
                var offerDetails = _offerRepository.Get(x => x.ID == item.Service_ID);
                items.AddToCard(new OrderedOffer
                {
                    Id = offerDetails.ID,
                    HoursFrom = item.HourFrom,
                    Description = offerDetails.Description,
                    HoursTo = item.HourTo,
                    RatePerHour = offerDetails.RatePerHour,
                    Title = offerDetails.Title,
                    WeekDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), item.Date.DayOfWeek.ToString()),
                    Date = item.Date,
                });
            }
            return items;
        }



        public Order CreateOrder(string u)
        {
            Order order = null;
            try
            {
                OrderTable o = _database.CreateOrder(u);
                List<JobPortal.Model.Saleline> salelines = new List<JobPortal.Model.Saleline>();
                foreach (var item in o.Salelines)
                {
                    salelines.Add(new JobPortal.Model.Saleline { Id = item.ID, ServiceOfferId = item.ServiceOffer_ID });
                }
                order = new Order { ID = o.ID, TotalPrice = o.TotalPrice, OrderStatus = o.OrderStatus.Order_status, User_ID = o.Users_ID, Salelines = salelines };
            }
            catch (BookedTimeException e)
            {
                throw new FaultException<BookedTimeException>
                 (e, new FaultReason(e.Message), new FaultCode("Sender"));
            }
            return order;

        }

        public bool AddToCart(string userId, int serviceId, DateTime date, TimeSpan hourfrom, TimeSpan hourTo)
        {
            return _database.AddToCart(new ShoppingCart
            {
                User_ID = userId,
                Service_ID = serviceId,
                Date = date,
                HourFrom = hourfrom,
                HourTo = hourTo

            });
        }
        public bool DeleteFromCart(string userId, int serviceId, DateTime date, TimeSpan hourfrom, TimeSpan hourTo)
        {
            return _database.DeleteFromCart(new ShoppingCart
            {
                User_ID = userId,
                Service_ID = serviceId,
                Date = date,
                HourFrom = hourfrom,
                HourTo = hourTo,
            });
        }

        public bool CancelOrder(Order o)
        {
            throw new NotImplementedException();
        }

        public bool PayForOrder(Order o)
        {
            if (_database.PayForOrder(new OrderTable { ID = o.ID}) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<TimeSpan> GetHoursFrom(int serviceId, DateTime date)
        {
           return _database.GetHoursFrom(serviceId, date);
        }
        public IEnumerable<TimeSpan> GetHoursTo(int serviceId, DateTime date, TimeSpan from)
        {
            return _database.GetHoursTo(serviceId, date, from);

        }

        public bool CleanCart(string userId)
        {
            return _database.CleanCart(userId);
        }
    }
}
