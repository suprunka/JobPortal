using JobPortal.Model;
using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using BookedDate = Repository.DbConnection.BookedDate;
using Saleline = Repository.DbConnection.Salelines;

namespace Repository
{
    public class OrderRepository : Repository<OrderTable>, IOrderRepository
    {
        private DataContext _context;
        private SqlTransaction sql = null;
        private readonly string connection = "Data Source=kraka.ucn.dk;Initial Catalog=dmai0917_1067677;User ID=dmai0917_1067677;Password=Password1!";



        public OrderRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public OrderRepository()
        {
        }

        public bool CancelOrder(Order o)
        {
            try
            {
                var found = _context.GetTable<Salelines>().Where(x => x.Order_ID == o.ID);
                foreach (var i in found)
                {
                    if (i.OrderTable.OrderStatus_ID == 1)
                    {
                        _context.GetTable<BookedDate>().DeleteOnSubmit(i.BookedDate);
                    }
                    else
                    {
                        return false;
                    }
                   
                }
                _context.GetTable<OrderTable>().DeleteOnSubmit(_context.GetTable<OrderTable>().Single(x => x.ID == o.ID));
                _context.SubmitChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        
        public List<ShoppingCart> GetShoppingCart(string id)
        {
            List<ShoppingCart> orderedServices = new List<ShoppingCart>();
            var shoppingCardInDB = _context.GetTable<ShoppingCart>().Where(x => x.User_ID == id);
            foreach (var i in shoppingCardInDB)
            {
                var so = _context.GetTable<ServiceOffer>().Single(x => x.ID == i.Service_ID);
                orderedServices.Add(new ShoppingCart
                {
                    Service_ID = i.Service_ID,
                    HourFrom = i.HourFrom,
                    HourTo = i.HourTo,
                    Date = i.Date,
                });
            }

            return orderedServices;
        }

        public OrderTable CreateOrder(string Logging_ID)
        {
            OrderTable result = null;
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                var txOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.Serializable
                };

                objConn.Open();
                using (var myTran = new TransactionScope(TransactionScopeOption.Required,txOptions))
                {
                    ShoppingCart cart = null;
                    try
                    {
                        ShoppingCart[] choosenServices = _context.GetTable<ShoppingCart>().
                            Where(x => x.User_ID == Logging_ID).Select(x => x).ToArray();

                        OrderTable newOrder = new OrderTable
                        {
                            AspNetUsers = _context.GetTable<AspNetUsers>().FirstOrDefault(x => x.Id == Logging_ID),
                            OrderStatus_ID = 1,
                            TotalPrice = 0,
                            Date = DateTime.Now,
                        };
                        _context.GetTable<OrderTable>().InsertOnSubmit(newOrder);
                        _context.SubmitChanges();
                        //add to existiing order in for loop
                        foreach (var item in choosenServices)
                        {
                            cart = item;
                            AddToExistingOrder(newOrder, item);
                        }
                        result = newOrder;
                        myTran.Complete();
                    }
                    catch (BookedTimeException e)
                    {
                        result = null;
                        _context.GetTable<ShoppingCart>().DeleteOnSubmit(cart);
                        _context.SubmitChanges();
                        throw e;

                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        myTran.Dispose();
                        objConn.Close();
                    }
                }
            }
            return result;
        }

        public override OrderTable Get(Expression<Func<OrderTable, bool>> predicate)
        {
            try
            {
                return _context.GetTable<OrderTable>().Where(predicate).Single(x => x.OrderStatus_ID == 1);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public OrderTable AddToExistingOrder(OrderTable o, ShoppingCart cart)
        {
            OrderTable result = null;
           
            if (_context.GetTable<WorkingDates>().Where(t => t.ServiceOffer_ID == cart.Service_ID)
                    .Any(t => t.NameOfDay == cart.Date.DayOfWeek.ToString()) && o.OrderStatus_ID != 2)
            {
                try
                {
                    var span = cart.HourTo - cart.HourFrom;
                    var numberOfHours = span.Hours;
                    IList<BookedDate> list = new List<BookedDate>();
                    //bool isAvailable = HoursAvailable(cart.Service_ID, cart.Date, cart.HourFrom, cart.HourTo);
                    var timeIsBooked = _context.GetTable<DbConnection.Salelines>().Where(x => x.ServiceOffer_ID == cart.Service_ID)
                        .Select(x => x).Where(x => x.BookedDate.BookedDate1 == cart.Date).Select(x => x.BookedDate);
                    foreach(var t in timeIsBooked)
                    {
                        if(new TimeRange(t.HourFrom, t.HourTo).Clashes(new TimeRange(cart.HourFrom, cart.HourTo)))
                        {
                            list.Add(t);
                        }
                    }
                    //select salelines for service and then select saleline  date(I mean day i.e '22.02'),
                    //selects the dates and checks if the hours aren't already booked
                    if (list.Count() > 0)
                    {
                        throw new BookedTimeException(cart.Service_ID, cart.User_ID, cart.HourFrom, cart.HourTo, cart.Date, cart.ServiceOffer.Title);
                    }
                    BookedDate dates = new BookedDate
                    {
                        NumberOfHours = numberOfHours,
                        BookedDate1 = cart.Date,
                        HourFrom = cart.HourFrom,
                        HourTo = cart.HourTo + new TimeSpan(0, 30, 0),
                    };
                    _context.GetTable<BookedDate>().InsertOnSubmit(dates);
                    _context.SubmitChanges();

                    Saleline saleline = new Saleline
                    {
                        ServiceOffer = _context.GetTable<ServiceOffer>().FirstOrDefault(x => x.ID == cart.Service_ID),
                        Order_ID = o.ID,
                        BookedDates_ID = dates.ID,
                    };

                    _context.GetTable<Saleline>().InsertOnSubmit(saleline);
                    _context.SubmitChanges();


                    var order = _context.GetTable<OrderTable>().FirstOrDefault(x => x.ID == o.ID);
                    var service = _context.GetTable<ServiceOffer>().FirstOrDefault(x => x.ID == cart.Service_ID);
                    order.TotalPrice += service.RatePerHour * numberOfHours;
                    _context.SubmitChanges();
                    result = o;
                    // sql.Commit();
                }
                catch (Exception e)
                {
                    // sql.Rollback();
                    result = null;
                    throw e;
                }
                finally
                {

                }
            }
            else
            {
                result = null;
            }
            // }
            return result;
        }

        public OrderTable PayForOrder(OrderTable o)
        {
            var orderToFinish = _context.GetTable<OrderTable>().Single(x => x.ID == o.ID);
            orderToFinish.OrderStatus_ID = 2;
            _context.SubmitChanges();
            return orderToFinish;
        }


        public bool AddToCart(ShoppingCart cart)
        {

            bool result = false;
            if (HoursAvailable(cart.Service_ID, cart.Date, cart.HourFrom, cart.HourTo))
            {
                using (SqlConnection objConn = new SqlConnection(connection))
                {
                    objConn.Open();
                    try
                    {
                        ShoppingCart myCart = new ShoppingCart
                        {
                            User_ID = cart.User_ID,
                            Service_ID = cart.Service_ID,
                            Date = DateTime.Parse(cart.Date.ToShortDateString()),
                            HourFrom = cart.HourFrom,
                            HourTo = cart.HourTo,
                        };
                        _context.GetTable<ShoppingCart>().InsertOnSubmit(myCart);
                        _context.SubmitChanges();
                        result = true;

                    }

                    catch (SqlException e)
                    {
                        return false;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        objConn.Close();
                    }
                }
            }
            return result;
        }


        public bool DeleteFromCart(ShoppingCart cart)
        {
            bool result = false;
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                objConn.Open();
                try
                {
                    var found = _context.GetTable<ShoppingCart>().Single(x => x.User_ID == cart.User_ID && x.HourTo == cart.HourTo && x.HourFrom == cart.HourFrom && x.Service_ID == cart.Service_ID && x.Date == cart.Date);

                    _context.GetTable<ShoppingCart>().DeleteOnSubmit(found);
                    _context.SubmitChanges();
                    result = true;
                }
                catch
                {
                    throw new InvalidOperationException();
                }
                finally
                {
                    objConn.Close();
                }
            }
            return result;
        }
        public bool HoursAvailable(int serviceOfferId, DateTime date, TimeSpan from, TimeSpan to)
        {
            ServiceOffer s = _context.GetTable<ServiceOffer>().FirstOrDefault(x => x.ID == serviceOfferId);
            TimeRange timeRange = new TimeRange(from, to);
            IList<TimeRange> times = new List<TimeRange>();
            var salelines = _context.GetTable<Saleline>().Where(x => x.ServiceOffer_ID == s.ID);

            foreach (var t in salelines)
            {
                if (t.BookedDate.BookedDate1.ToShortDateString() == date.ToShortDateString())
                {
                    times.Add(new TimeRange(t.BookedDate.HourFrom, t.BookedDate.HourTo));
                }
            }

            foreach (var w in times)
            {
                if (timeRange.Clashes(w))
                {
                    return false;
                }
            }
            return true;
        }

        public IList<TimeSpan> GetHoursFrom(int serviceId, DateTime date)
        {
            try
            {
                IList<TimeSpan> hourTo = new List<TimeSpan>();
                DayOfWeek day = date.DayOfWeek;
                var workingdates = _context.GetTable<WorkingDates>().Where(x => x.NameOfDay == day.ToString() && x.ServiceOffer_ID == serviceId).OrderBy(x=> x.HourFrom);
                var unavailable = _context.GetTable<Saleline>().Where(x => x.ServiceOffer_ID == serviceId).Select(x => x.BookedDate).Where(x => x.BookedDate1 == date).ToArray();
                foreach(var t in workingdates)
                {
                    for (TimeSpan from = t.HourFrom; from < t.HourTo; from = +from.Add(new TimeSpan(01, 00, 00)))
                    {
                        TimeSpan time = from;
                        TimeRange timeRange = new TimeRange(time, time.Add(new TimeSpan(01, 30, 00)));
                        bool result = true;
                        foreach (var i in unavailable)
                        {
                            TimeRange range = new TimeRange(i.HourFrom, i.HourTo);
                            if (!range.Clashes(timeRange, true))
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                                break;
                            }

                        }
                        if (result)
                        {
                            hourTo.Add(time);
                        }

                    }
                }
                return hourTo.ToList();

            }
            catch (InvalidOperationException)
            {
                throw new Exception();

            }
        }

        public IList<TimeSpan> GetHoursTo(int serviceId, DateTime date, TimeSpan timefrom)
        {
            IList<TimeSpan> hourTo = new List<TimeSpan>();
            DayOfWeek day = date.DayOfWeek;
            var workingdates = _context.GetTable<WorkingDates>().Where(x => x.NameOfDay == day.ToString() && x.ServiceOffer_ID == serviceId && x.HourFrom <= timefrom && x.HourTo >= timefrom).OrderBy(x=> x.HourFrom);
            var unavailable = _context.GetTable<Saleline>().Where(x => x.ServiceOffer_ID == serviceId).Select(x => x.BookedDate).Where(x => x.BookedDate1 == date).ToArray();
            for (TimeSpan from = timefrom; from < workingdates.Select(x => x.HourTo).Max(); from = +from.Add(new TimeSpan(01, 00, 00)))
            {
                TimeSpan time = from;
                TimeRange timeRange = new TimeRange(time, time.Add(new TimeSpan(01, 30, 00)));

                bool result = true;
                if (unavailable.Count() != 0)
                {
                    result = false;
                }
                unavailable.OrderBy(x => x.HourFrom);
                foreach (var i in unavailable)
                {
                    TimeRange range = new TimeRange(i.HourFrom, i.HourTo);
                    if (!range.Clashes(timeRange))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                        // break;
                    }

                }
                if (result)
                {
                    hourTo.Add(time.Add(new TimeSpan(01, 00, 00)));
                }
                else
                {
                    break;
                }

            }
            return hourTo.ToList();
        }

        public bool CleanCart(string userID)
        {

            foreach (var i in _context.GetTable<ShoppingCart>().Where(x => x.User_ID == userID))
            {
                _context.GetTable<ShoppingCart>().DeleteOnSubmit(i);
            }
            _context.SubmitChanges();
            return true;
        }
        public IQueryable<Saleline> GetJobCalendar(DateTime date, string employeeId)
        {
         return _context.GetTable<Saleline>().Where(x => x.BookedDate.BookedDate1.Equals(date) && x.ServiceOffer.Employee_ID == employeeId);
        }
       public  IQueryable<Salelines> GetAllSalelines()
        {
            return _context.GetTable<Salelines>();
        }
        public IQueryable<OrderTable> GetAllOrders()
        {
            return _context.GetTable<OrderTable>();
        }

      

    }

}






