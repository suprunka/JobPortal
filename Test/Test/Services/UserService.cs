using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Repositories;
using Repository.DbConnection;
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
            _database = new Repository<User>(new JobPortalDatabaseDataContext());
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
                    _database.Delete(t => t.ID == id);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool EditUser(Users u)
        {
            _database.Update(u);
            return true;
        }

        public Users FindUser(int id)
        {
            try
            {
                Users u = _database.Get(t=> t.ID == id);
                return u;
            }
            catch
            {
                return null;

            }

        }


        public IQueryable<User> GetAll()
        {
            return _database.GetAll();
        }

        public Models.Users CreateUser(Models.Users u)
        {
            throw new NotImplementedException();
        }

        Models.Users IUserService.FindUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool EditUser(Models.Users u)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Models.Users> IUserService.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
