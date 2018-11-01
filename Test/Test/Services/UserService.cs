using System;
using System.Collections.Generic;
using System.ServiceModel;
using Repositories;
using ServiceLibrary.Models;

namespace ServiceLibrary
{

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class UserService : IUserService
    {
        private readonly IRepository<User> _database;

        public UserService(IRepository<User> database)
        {
            _database = database;
        }
        public UserService()
        {
           
        }

        public User CreateUser(User u)
        {
            if (u != null)
            {
                try
                {
                    _database.Create(u);
                    return u;
                }catch(ArgumentNullException)
                {
                   
                }
            }
            return null;
        }

        public bool DeleteUser(int id)
        {
            if (id> 0)
            {
                _database.Delete(id);
                return true;
            }
            return false;
        }

        public bool EditUser(User u)
        {
            throw new NotImplementedException();
        }

        public User FindUser(int id)
        {
            User u = _database.Get(id);
            return u;
        }
        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();

        }
    }
}
