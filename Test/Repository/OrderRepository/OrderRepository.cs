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
                        Payment = new Payment
                        {
                            TotalPrice = 0,
                            PaymentType = 1,
                        },
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

        public OrderTable AddToExistingOrder(OrderTable o, ServiceOffer s, int quantity)
        {
            OrderTable result = null;
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {
                    Saleline saleline = new Saleline
                    {
                       Quantity = quantity,
                       ServiceOffer_ID = _context.GetTable<ServiceOffer>().FirstOrDefault(x=>x.ID == s.ID).ID,
                       Order_ID = o.ID,
                    };
                    _context.GetTable<Saleline>().InsertOnSubmit(saleline);
                    _context.SubmitChanges();

                    var order= _context.GetTable<OrderTable>().FirstOrDefault(x => x.ID == o.ID);
                    order.Payment.TotalPrice += s.RatePerHour * quantity;
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

        public OrderTable DeleteFromExistingOffer(OrderTable o, ServiceOffer s)
        {
            OrderTable result = null;
            using (SqlConnection objConn = new SqlConnection(connection))
            {
                objConn.Open();
                sql = objConn.BeginTransaction();
                try
                {
                    var filterListDelete = _context.GetTable<Saleline>().Where(x => x.ServiceOffer_ID == s.ID);
                    var toDelete = filterListDelete.FirstOrDefault(x => x.Order_ID == o.ID);
                    int quantity = toDelete.Quantity;
                    _context.GetTable<Saleline>().DeleteOnSubmit(toDelete);
                    _context.SubmitChanges();
                    result = o;

                    var order = _context.GetTable<OrderTable>().FirstOrDefault(x => x.ID == o.ID);
                    order.Payment.TotalPrice -= s.RatePerHour * quantity;
                    _context.SubmitChanges();
                    result = o;
                    sql.Commit();
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





    }
}

