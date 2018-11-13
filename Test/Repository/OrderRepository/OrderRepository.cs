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
    public class OrderRepository: Repository<OrderTable>, IOrderRepository
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

        public Order Create(Order obj)
        {
            throw new NotImplementedException();
        }

        public Order Get(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(Order obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        IQueryable<Order> IRepository<Order>.GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> List(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}

