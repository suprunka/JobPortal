using JobPortal.Model;
using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using BookedDate = Repository.DbConnection.BookedDate;
using Saleline = Repository.DbConnection.Salelines;

namespace Repository.OrderRepository
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

        public OrderTable CreateOrder(Users u)
        {
            OrderTable result = null;
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                ShoppingCart cart =null;
                try
                {
                    ShoppingCart[] choosenServices = _context.GetTable<ShoppingCart>().Where(x => x.User_ID == u.Logging_ID).Select(x => x).ToArray();

                    OrderTable newOrder = new OrderTable
                    {
                        AspNetUsers = _context.GetTable<AspNetUsers>().FirstOrDefault(x => x.Id == u.Logging_ID),
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
                    sql.Commit();
                }
                catch (BookedTimeException)
                {
                    sql.Rollback();
                    result = null;
                    _context.GetTable<ShoppingCart>().DeleteOnSubmit(cart);
                    _context.SubmitChanges();

                }
                catch (Exception e)
                {
                    sql.Rollback();
                   
                    throw e;
                }
                finally
                {
                    objConn.Close();
                }
            }
            return result;
        }

        public OrderTable AddToExistingOrder(OrderTable o, ShoppingCart cart)//DateTime date, TimeSpan from, TimeSpan to)
        {
            OrderTable result = null;
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                if (_context.GetTable<WorkingDates>().Where(t => t.ServiceOffer_ID == cart.Service_ID).Any(t=> t.NameOfDay == cart.Date.DayOfWeek.ToString()) &&  o.OrderStatus_ID != 2)
                {
                    try
                    {
                        var span = cart.HourTo - cart.HourFrom;
                        var numberOfHours = span.Hours;

                        var timeIsBooked = _context.GetTable<DbConnection.Salelines>().Where(x => x.ServiceOffer_ID == cart.Service_ID).Select(x => x).Where(x => x.BookedDate.BookedDate1 == cart.Date).Select(x => x.BookedDate).Where(x => new TimeRange(x.HourFrom, x.HourTo).Clashes(new TimeRange(cart.HourFrom, cart.HourTo))).Select(x => x).ToArray();
                        //select salelines for service and then select saleline  date(I mean day i.e '22.02'), selects the dates and checks if the hours aren't already booked
                        if (timeIsBooked.Length > 0)
                        {
                            throw new BookedTimeException(cart.Service_ID,cart.User_ID);
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
                        sql.Commit();
                    }
                    catch (Exception e)
                    {
                        sql.Rollback();
                        result = null;
                        throw e;
                    }
                    finally
                    {
                        objConn.Close();
                    }
                }
                else
                {
                    result = null;
                }
            }
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
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                objConn.Open();
                try
                {
                    ShoppingCart myCart = new ShoppingCart
                    {
                        User_ID = cart.User_ID,
                        Service_ID = cart.Service_ID,
                        Date = cart.Date,
                        HourFrom = cart.HourFrom,
                        HourTo = cart.HourTo,
                    };
                    objConn.Open();
                    _context.GetTable<ShoppingCart>().InsertOnSubmit(cart);
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
        }
/*
        public bool HoursAvailable(ServiceOffer s, DateTime date, TimeSpan from, TimeSpan to)
        {
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
        }*/


  
    }
    


