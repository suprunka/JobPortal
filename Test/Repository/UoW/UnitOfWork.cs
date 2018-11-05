using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;


        public UnitOfWork(DataContext context)
        {
            _context = context;
            Users = new UsersRepository(_context);
        }
        public IUserRepository Users { get; private set; }

  
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
