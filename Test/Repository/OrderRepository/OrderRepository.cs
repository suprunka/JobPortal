using JobPortal.Model;
using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.OrderRepository
{
    public class OrderRepository : Repository<OrderTable>, IOrderRepository
    {
        private DataContext _context;
        private SqlTransaction sql = null;
        private readonly string connection = "Data Source=JAKUB\\SQLEXPRESS;Initial Catalog=JobPortalTestDB;Integrated Security=True";



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
                try
                {
                    OrderTable newOrder = new OrderTable
                    {
                        Account_ID = _context.GetTable<Account>().FirstOrDefault(x => x.PhoneNumber == u.PhoneNumber).ID,
                        OrderStatus_ID = 1,
                        TotalPrice= 0,
                        Date = DateTime.Now,
                    };
                    _context.GetTable<OrderTable>().InsertOnSubmit(newOrder);
                    _context.SubmitChanges();
                    result = newOrder;
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
            return result;
        }

        public OrderTable AddToExistingOrder(OrderTable o, ServiceOffer s, DateTime date, TimeSpan from, TimeSpan to)
        {
            OrderTable result = null;
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                if (_context.GetTable<WorkingDates>().Where(t => t.ServiceOffer_ID == s.ID).Any(t=> t.NameOfDay == date.DayOfWeek.ToString()) && 
                    HoursAvailable(s,date,from,to) &&
                    o.OrderStatus_ID != 2)
                {
                    try
                    {
                        var span = to - from;
                        var numberOfHours = span.Hours;
                        
                        BookedDates dates = new BookedDates
                        {
                            NumberOfHours = numberOfHours,
                            BookedDate = date,
                            HourFrom = from,
                            HourTo = to + new TimeSpan(0,30,0),
                        };
                        _context.GetTable<BookedDates>().InsertOnSubmit(dates);
                        _context.SubmitChanges();

                        Saleline saleline = new Saleline
                        {
                            ServiceOffer_ID = _context.GetTable<ServiceOffer>().FirstOrDefault(x => x.ID == s.ID).ID,
                            Order_ID = o.ID,
                            BookedDates_ID = dates.ID,
                        };
                        _context.GetTable<Saleline>().InsertOnSubmit(saleline);
                        _context.SubmitChanges();


                        var order = _context.GetTable<OrderTable>().FirstOrDefault(x => x.ID == o.ID);
                        order.TotalPrice += s.RatePerHour * numberOfHours;
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
        public OrderTable DeleteFromExistingOrder(OrderTable o, ServiceOffer s, DateTime date, TimeSpan from, TimeSpan to)
        {
            OrderTable result = null;
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {
                    var saleLineToDelete = _context.GetTable<Saleline>().FirstOrDefault(x => x.ServiceOffer_ID == s.ID && x.Order_ID == o.ID && x.BookedDates.BookedDate == date &&
                    x.BookedDates.HourFrom == from && x.BookedDates.HourTo == to + new TimeSpan(0, 30, 0));
                    var order = _context.GetTable<OrderTable>().FirstOrDefault(x => x.ID == o.ID);
                    var bookedDateToDelete = _context.GetTable<BookedDates>().Single(x => x.ID == saleLineToDelete.BookedDates_ID);
                    order.TotalPrice -= (s.RatePerHour * saleLineToDelete.BookedDates.NumberOfHours);
                    _context.GetTable<Saleline>().DeleteOnSubmit(saleLineToDelete);
                    _context.GetTable<BookedDates>().DeleteOnSubmit(bookedDateToDelete);
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
            return result;
        }
        public OrderTable PayForOrder(OrderTable o)
        {
            var orderToFinish = _context.GetTable<OrderTable>().Single(x => x.ID == o.ID);
            orderToFinish.OrderStatus_ID = 2;
            _context.SubmitChanges();
            return orderToFinish;
        }

        public bool HoursAvailable(ServiceOffer s, DateTime date, TimeSpan from, TimeSpan to)
        {
            TimeRange timeRange = new TimeRange(from, to);
            IList<TimeRange> times = new List<TimeRange>();
            var salelines = _context.GetTable<Saleline>().Where(x => x.ServiceOffer_ID == s.ID);

            foreach (var t in salelines)
            {
                if (t.BookedDates.BookedDate.ToShortDateString() == date.ToShortDateString())
                {
                    times.Add(new TimeRange(t.BookedDates.HourFrom, t.BookedDates.HourTo));
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

  
    }
}

