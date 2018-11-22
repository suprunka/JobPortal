using JobPortal.Model;
using Repository.DbConnection;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using BookedDate = Repository.DbConnection.BookedDates;
using Saleline = Repository.DbConnection.Saleline;

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

        public OrderTable CreateOrder(Users u, IList<KeyValuePair<ServiceOffer, JobPortal.Model.BookedDate>> choosenServices)
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
                        AspNetUsers = _context.GetTable<AspNetUsers>().FirstOrDefault(x => x.Id == u.Logging_ID),
                        OrderStatus_ID = 1,
                        TotalPrice= 0,
                        Date = DateTime.Now,
                    };
                    _context.GetTable<OrderTable>().InsertOnSubmit(newOrder);
                    _context.SubmitChanges();
                    //add to existiing order in for loop
                    foreach (var item in choosenServices)
                    {
                        AddToExistingOrder(newOrder, item.Key, item.Value);
                    }
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

        public OrderTable AddToExistingOrder(OrderTable o, ServiceOffer s, JobPortal.Model.BookedDate date)//DateTime date, TimeSpan from, TimeSpan to)
        {
            OrderTable result = null;
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                if (_context.GetTable<WorkingDates>().Where(t => t.ServiceOffer_ID == s.ID).Any(t=> t.NameOfDay == date.Day.DayOfWeek.ToString()) &&  o.OrderStatus_ID != 2)
                {
                    try
                    {
                        var span = date.HoursTo - date.HoursFrom;
                        var numberOfHours = span.Hours;

                        var timeIsBooked = _context.GetTable<DbConnection.Saleline>().Where(x => x.ServiceOffer_ID == s.ID).Select(x => x).Where(x => x.BookedDates.BookedDate == date.Day).Select(x => x.BookedDates).Where(x => new TimeRange(x.HourFrom, x.HourTo).Clashes(new TimeRange(date.HoursFrom, date.HoursTo))).Select(x => x).ToArray();
                        //select salelines for service and then select saleline  date(I mean day i.e '22.02'), selects the dates and checks if the hours aren't already booked
                        if (timeIsBooked.Length > 0)
                        {
                            throw new BookedTimeException(s.ID, s.Employee_ID);
                        }
                        BookedDate dates = new BookedDate
                        {
                            NumberOfHours = numberOfHours,
                            BookedDate = date.Day,
                            HourFrom = date.HoursFrom,
                            HourTo = date.HoursTo + new TimeSpan(0, 30, 0),
                        };
                        _context.GetTable<BookedDate>().InsertOnSubmit(dates);
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
       
        public OrderTable PayForOrder(OrderTable o)
        {
            var orderToFinish = _context.GetTable<OrderTable>().Single(x => x.ID == o.ID);
            orderToFinish.OrderStatus_ID = 2;
            _context.SubmitChanges();
            return orderToFinish;
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
    }


