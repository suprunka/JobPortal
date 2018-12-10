using JobPortal.Model;
using Repository;
using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace ServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true)]
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;


        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userService = new UserService();
        }

        public OrderService()
        {
            _unitOfWork = new UnitOfWork(new JobPortalDatabaseDataContext());
            _userService = new UserService();
        }

        public ShoppingCard GetShoppingCart(string id)
        {
            ShoppingCard items = new ShoppingCard();

            foreach (var item in _unitOfWork.Orders.GetShoppingCart(id))
            {
                var offerDetails = _unitOfWork.Offers.Get(x => x.ID == item.Service_ID);
                items.AddToCard(new Offer
                {
                    AuthorId = offerDetails.Employee_ID,
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
            if (_unitOfWork.Orders.CancelOrder(o))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PayForOrder(Order o)
        {
            if (_unitOfWork.Orders.PayForOrder(new OrderTable { ID = o.ID }) != null)
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
        public IEnumerable<JobOffer> GetJobCallendar(DateTime date, string employeeId)
        {
            IList<JobOffer> joboffers = new List<JobOffer>();
            var listofSalelines = _unitOfWork.Orders.GetJobCalendar(date, employeeId);
            foreach (var saleline in listofSalelines)
            {
                var customerId = saleline.OrderTable.Users_ID;
                WorkingDetails workingDetailsTime = new WorkingDetails() { Date = date, HoursFrom = saleline.BookedDate.HourFrom, HoursTo = saleline.BookedDate.HourTo, WeekDay = date.DayOfWeek };
                int workingtime = (workingDetailsTime.HoursTo - workingDetailsTime.HoursFrom).Hours;

                Offer o = new Offer()
                {
                    Id = saleline.ServiceOffer_ID,
                    AuthorId = saleline.ServiceOffer.Employee_ID,
                    Category = (JobPortal.Model.Category)Enum.Parse(typeof(JobPortal.Model.Category), saleline.ServiceOffer.SubCategory.Category.Name.ToString()),
                    Subcategory = (JobPortal.Model.SubCategory)Enum.Parse(typeof(JobPortal.Model.SubCategory), saleline.ServiceOffer.SubCategory.Name.ToString()),
                    Description = saleline.ServiceOffer.Description,
                    RatePerHour = saleline.ServiceOffer.RatePerHour,
                    Title = saleline.ServiceOffer.Title,
                    WorkingTime = workingDetailsTime,
                };
                var joboffer = new JobOffer() { Customer = _userService.FindUser(customerId), Customer_ID = customerId, Offer = o, TotalPrice = o.RatePerHour * workingtime };
                joboffers.Add(joboffer);
            }

            return joboffers.AsEnumerable<JobOffer>().OrderBy(x => x.Offer.WorkingTime.HoursFrom);

        }

        public Order FindOrder(string id)
        {
            if (_unitOfWork.Orders.Get(x => x.Users_ID == id) == null)
            {
                return null;
            }
            else
            {
                return new Order
                {
                    ID = _unitOfWork.Orders.Get(x => x.Users_ID == id).ID,
                };
            }

        }
        public IQueryable<Saleline> GetAllSalelines()
        {
          return   _unitOfWork.Orders.GetAllSalelines().Select(x=> new Saleline { Id = x.ID, ServiceOfferId = x.ServiceOffer_ID , AuthorId = x.ServiceOffer.Employee_ID, Date = x.BookedDate.BookedDate1});
        }
        public IQueryable<Order> GetAllOrders()

        {
            IQueryable < Order > orders = _unitOfWork.Orders.GetAllOrders().Select(x=> new Order { ID = x.ID, TotalPrice = x.TotalPrice, OrderStatus = x.OrderStatus_ID.ToString(),
                    User_ID = x.Users_ID, Salelines = x.Salelines.Select(s=> new Saleline {Date = s.BookedDate.BookedDate1, ServiceOfferId = s.ServiceOffer_ID, Id = s.ID })
            });
            return orders;
        }




    }
}

