using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelClasses;

namespace ServiceLibrary.Repository
{
    public class UserRepository : IUserRepository
    {
        protected Table<User> dataTable;

        public UserRepository(DataContext dataContext)
        {
            dataTable = dataContext.GetTable<User>();
        }

        public User CreateUser(User u)
        {
            
            dataTable.InsertOnSubmit(u);
            return u;
        }
    }
}
