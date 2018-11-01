using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                _database.Create(u);
                return u;
            }
            catch (ArgumentNullException)
            {
                return null;
            }

        }



        public bool DeleteUser(int id)
        {
            try
            {
                if (id > 0)
                {
                    _database.Delete(id);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool EditUser(User u)
        {
            //if (FindUser(u.ID) != null)
            try
            {
                bool result = _database.Update(u);
                return true;

            }
            catch
            {
                return false;
            }
            
             
        }

        public User FindUser(int id)
        {
            try
            {
                User u = _database.Get(id);
                return u;
            }
            catch
            {
                return null;

            }

        }
        public IQueryable<User> GetAll()
        {

            IQueryable<User> users =  _database.GetAll();
            return users;

        }
    }
}
