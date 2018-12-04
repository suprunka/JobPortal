using System.Data.Linq;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;


        public UnitOfWork(DataContext context)
        {
            _context = context;
            Users = new  UsersRepository(_context);
            Offers = new OfferRepository(_context);
            Orders = new OrderRepository(_context);
        }


        public IUserRepository Users { get; private set; }

        public IOfferRepository Offers { get; private set; }

        public IOrderRepository Orders { get; private set; }


    

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
