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
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public OrderService()
        {
            _unitOfWork = new UnitOfWork(new JobPortalDatabaseDataContext());
        }

        public ShoppingCard GetShoppingCart(string id)
        {
            ShoppingCard items = new ShoppingCard();

            foreach (var item in _unitOfWork.Orders.GetShoppingCart(id))
            {
                var offerDetails = _unitOfWork.Offers.Get(x => x.ID == item.Service_ID);
                items.AddToCard(new Offer
                {
                    Id = offerDetails.ID,
                    Description = offerDetails.Description,
                    RatePerHour = offerDetails.RatePerHour,
                    Title = offerDetails.Title,
                    WorkingTime = new WorkingDetails
                    {
                        Date = item.Date,
                        HoursFrom = item.HourFrom,
                        HoursTo = item.HourTo,
                        WeekDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), item.Date.DayOfWeek.ToString()),
                    },
                });
            }
            return items;
        }

        public ShoppingCard GetShoppingCartForPaypal(string id)
        {
            ShoppingCard items = new ShoppingCard();

            foreach (var item in _unitOfWork.Orders.GetShoppingCart(id))
            {
                var offerDetails = _unitOfWork.Offers.Get(x => x.ID == item.Service_ID);
                items.AddToCardPayPal(new PayPalOffer
                {
                    Id = offerDetails.ID,
                    HoursFrom = item.HourFrom,
                    HoursTo = item.HourTo,
                    RatePerHour = offerDetails.RatePerHour,
                    Title = offerDetails.Title,
                });
            }
            return items;
        }



        public Order CreateOrder(string u)
        {
            Order order = null;
            try
            {
                OrderTable o = _unitOfWork.Orders.CreateOrder(u);
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
                 (e, new FaultReason(e.GetMessage()), new FaultCode("Sender"));
            }
            return order;

        }

        public bool AddToCart(string userId, int serviceId, DateTime date, TimeSpan hourfrom, TimeSpan hourTo)
        {
            return _unitOfWork.Orders.AddToCart(new ShoppingCart
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
            return _unitOfWork.Orders.DeleteFromCart(new ShoppingCart
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
            if (_unitOfWork.Orders.PayForOrder(new OrderTable { ID = o.ID}) != null)
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
           return _unitOfWork.Orders.GetHoursFrom(serviceId, date);
        }
        public IEnumerable<TimeSpan> GetHoursTo(int serviceId, DateTime date, TimeSpan from)
        {
            return _unitOfWork.Orders.GetHoursTo(serviceId, date, from);

        }

        public bool CleanCart(string userId)
        {
            return _unitOfWork.Orders.CleanCart(userId);
        }
    }
}
